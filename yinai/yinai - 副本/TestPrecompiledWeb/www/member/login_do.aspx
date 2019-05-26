<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Member member = new Member();
        string action = Request["action"];

        string member_mobile = Request["member_mobile"];
        
        switch (action)
        {
            case "fastlogin":
                member.Fast_MemberLogin();
                break;
            case "login":
                member.Member_Login();
                break;
            case "indexlogin":
                //member.Member_IndexLogin();
                member.Member_Login();
                break;
            case "logout":
                member.Member_LogOut();
                break;
            case "getpass":
                member.member_getpass_sendmail();
                break;
            case "verify":
                member.member_getpass_verify();
                break;
            case "resetpass":
                member.member_getpass_resetpass(0);
                break;


            case "resetpassbymoile":
                member.member_getpass_resetpass(1);
                break;


            case "checknickname":
                member.Check_LoginNickname();
                break;
            case "checkpwd":
                member.Check_LoginMemberPasswprd();
                break;
            case "checkverify":
                member.Check_Verifycode();
                break;
                // case "smscheckcode":
                //Response.Write(member. SmscheckCode());
                //break;



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

                System.Collections.Generic.Dictionary<string, string> smscheckcode = new System.Collections.Generic.Dictionary<string, string>();
                smscheckcode.Add("sign", strmobile);
                smscheckcode.Add("code", new Public_Class().Createvkey(6));
                smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());
                Session["sms_check"] = smscheckcode;

                //发送短信
                
                new SMS().Send(strmobile, smscheckcode["code"].ToString());

                Response.Write("{\"result\":\"true\", \"msg\":\"\"}");
                break;
                
                
                //手机验证码 若成功跳转到修改密码页面
            case "MobilevalidateIsTrue":
                member.PasswordResetByMobile(member_mobile);
                break; 
        }
        
        
     
        
    }
</script>


