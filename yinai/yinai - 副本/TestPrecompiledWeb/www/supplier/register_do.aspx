<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ITools tools = ToolsFactory.CreateTools();
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            //case "basicinfo":
            //    supplier.Supplier_Register();
            //    break;
            case "check_suppliertype":
                supplier.Check_SupplierType();
                break;
            case "checkemail":
                supplier.Check_SupplierEmail();
                break;
            case "checknickname":
                supplier.Check_Nickname();
                break;
            case "checkcompanyname":
                supplier.Check_Companyname();
                break;
            case "checkpwd":
                supplier.Check_Passwprd();
                break;
            case "checkrepwd":
                supplier.Check_rePasswprd();
                break;
            case "checkverify":
                supplier.Check_Verifycode();
                break;
            case "checkmobileverify":
                supplier.Check_MobileVerify();
                break;
            case "checkmobile":
                supplier.Check_Mobile();
                break;
            case "checkisblank":
                supplier.Check_IsBlank();
                break;
            case "checkphone":
                supplier.Check_MemberPhone();
                break;
            case "checkzip":
                supplier.Check_ZipCode();
                break;

            case "resendemailverify":
                supplier.supplier_register_resendemailverify();
                break;
            case "modifyemail":
                supplier.supplier_register_modifyemail();
                Response.End();
                break;
            case "emailverify":
                supplier.supplier_register_emailverify();
                break;
            case "checkloginmobile":
                supplier.Check_LoginMobile();
                break;
            case "check_sms_checkcode":
                supplier.Check_SMS_CheckCode();
                break;
            case "smscheckcode":
                string strmobile = tools.CheckStr(Request["phone"]);
                string verifycode = tools.CheckStr(Request["supplier_verifycode"]);
                
                if (strmobile.Length == 0)
                {
                    Response.Write("{\"result\":\"false\", \"msg\":\"请输入您的手机号\"}");
                    Response.End();
                }

                //if (verifycode == "" && verifycode.Length == 0)
                //{
                //    Response.Write("{\"result\":\"false\", \"msg\":\"请输入验证码\"}");
                //    Response.End();
                //}
                //else
                //{
                //    if (verifycode != Session["Trade_Verify"].ToString())
                //    {
                //        Response.Write("{\"result\":\"false\", \"msg\":\"验证码输入错误\"}");
                //        Response.End();
                //    }
                //}

                //Session["Trade_Verify"] = string.Empty;

                Dictionary<string, string> smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;
                
                //发送短信
                //new SMS().Send(strmobile, "register", smscheckcode["code"]);
                new SMS().Send(strmobile, smscheckcode["code"]);

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
            case "getpass_smscheckcode":

                strmobile = Convert.ToString(Session["getpass_supplier_loginmobile"]);
                smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;

                //发送短信
                //new SMS().Send(strmobile, "register", smscheckcode["code"]);
                new SMS().Send(strmobile, smscheckcode["code"].ToString());

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
            case "loginsmscheckcode":
                
                string loginname = tools.CheckStr(Request["loginname"]);
                if (loginname.Length == 0)
                {
                    Response.Write("{\"result\":\"false\", \"msg\":\"请输入用户名\"}");
                    Response.End();
                }

                strmobile = supplier.GetLoginNameMobile(loginname);
                if (strmobile.Length == 0)
                {
                    Response.Write("{\"result\":\"false\", \"msg\":\"不存在的账户\"}");
                    Response.End();
                }

                Session["Trade_Verify"] = string.Empty;
                smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", loginname);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;
                
                //发送短信
                //new SMS().Send(strmobile, "supplier_login", smscheckcode["code"]);
                new SMS().Send(strmobile,  smscheckcode["code"].ToString());

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
                
        }

    }
</script>
