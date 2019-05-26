<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ITools tools = ToolsFactory.CreateTools();
        Member member = new Member();
        string action = Request["action"];
        switch (action)
        {
            case "basicinfo":
                member.Member_Register();
                break;
            case "resendemailverify":
                member.member_register_resendemailverify();
                break;
            case "modifyemail":
                member.member_register_modifyemail();
                Response.End();
                break;
            case "emailverify":
                member.member_register_emailverify();
                break;
            case "checkmembertype":
                member.Check_MemberType();
                break;
            case "checknickname":
                member.Check_Nickname();
                break;
            case "checkemail":
                member.Check_MemberEmail();
                break;
            case "checkpwd":
                member.Check_MemberPasswprd();
                break;
            //检验用户名
            case "checkrealname":
                member.Check_Real_Name();
                break;

            //检验真实姓名
            case "checkMember_Profile_Contact":
                member.Check_Member_Profile_Contact();
                break;


            case "checkrealsuppliername":
                member.Check_Real_CompanayName();
                break;
            case "checkrepwd":
                member.Check_MemberrePasswprd();
                break;
            case "checkmobile":
                member.Check_MemberMobile();
                break;
            case "checkdrivermobile":
                member.Check_DriverMobile();
                break;
            case "checkcartmobile":
                member.CheckCart_MemberMobile();
                break;
            case "checkverify":
                member.Check_Verifycode();
                break;
            case "checkmobileverify":
                member.Check_MobileVerifycode();
                break;
            case "checkprotocal":
                member.Check_Checkprotocal();
                break;
            case "checkisblank":
                member.Check_IsBlank();
                break;
            case "checkphone":
                member.Check_MemberPhone();
                break;
            case "checkfeedbackphone":
                member.Check_Member_Feedback();
                break;


            //case "checkfeedbackphone":
            //    member.Check_Member_Feedback();
            //    break;



            case "checkfeedback_amount":
                member.Check_Member_FeedAmount();
                break;
            case "checkcompany":
                member.Check_Company();
                break;
            case "contactname":
                member.Check_ContactName();
                break;
            case "address":
                member.Check_Address();
                break;
            case "checkzip":
                member.Check_ZipCode();
                break;
            case "checkloginmobile":
                member.Check_LoginMobile();
                break;
            case "check_sms_checkcode":
                member.Check_SMS_CheckCode();
                break;
            case "smscheckcode":
                string strmobile = tools.CheckStr(Request["phone"]);
                string verifycode = tools.CheckStr(Request["verifycode"]);
                if (strmobile.Length == 0)
                {
                    Response.Write("{\"result\":\"false\", \"msg\":\"请输入您的手机号\"}");
                    Response.End();
                }

                if (verifycode == "" && verifycode.Length == 0)
                {
                    Response.Write("{\"result\":\"false\", \"msg\":\"请输入验证码\"}");
                    Response.End();
                }
                else
                {
                    if (verifycode != Session["Trade_Verify"].ToString())
                    {
                        Response.Write("{\"result\":\"false\", \"msg\":\"验证码输入错误\"}");
                        Response.End();
                    }
                }

                Session["Trade_Verify"] = string.Empty;

                Dictionary<string, string> smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                //smscheckcode.Add("code", "111111");
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;

                //发送短信
                //new SMS().Send(strmobile, "register", smscheckcode["code"]);
                new SMS().Send(strmobile, smscheckcode["code"].ToString());

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
            case "getpass_smscheckcode":

                strmobile = Convert.ToString(Session["getpass_member_loginmobile"]);
                smscheckcode = new Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                // smscheckcode.Add("code", "111111");
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

                strmobile = member.GetLoginNameMobile(loginname);
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


                //Response.Write(smscheckcode["code"].ToString());
                //发送短信
                // new SMS().Send(strmobile, "member_login", smscheckcode["code"]);
                new SMS().Send(strmobile, smscheckcode["code"].ToString());

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
        }
    }
    
    
    
</script>
