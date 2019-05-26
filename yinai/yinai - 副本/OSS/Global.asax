<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        
        //Application["upload_server_url"] = "http://img.jingyw.com/";
        //Application["upload_server_return_admin"] = "http://oss.jingyw.com/public/upload_result.aspx";
        
        //初始化系统变量
        Config config = new Config();
        config.Sys_UpdateApplication();
        config = null;

        //进销存初始化
        SCMConfig scmconfig = new SCMConfig();
        scmconfig.Config_Initialize();
        scmconfig = null;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码


        Exception ex = Server.GetLastError();

        switch (ex.InnerException.GetType().Name)
        {
            case "TradePrivilegeException":
                Server.ClearError();
                Response.Redirect("/unpower.aspx?tip=" + ex.InnerException.Message);
                break;
            default:
                throw ex;
        }
        
    }

    void Session_Start(object sender, EventArgs e) 
    {
        Session.Timeout = 60;
        //在新会话启动时运行的代码
        Response.Cookies["username"].Value = "";
        Session["UserLogin"] = "false";
        
        //设置当前站点
        Session["CurrentSite"] = "CN";

        //初始化系统变量
        Config config = new Config();
        config.Sys_UpdateApplication(Session["CurrentSite"].ToString());
        config = null;

        ////临时自动登录
        //Session["UserLogin"] = "true";
        //Session["User_ID"] = 1;
        //Session["User_GroupID"] = 1;
        //Session["User_Name"] = "admin";
        //Session["User_LastLogin"] = "2010-11-12 15:25:00";
        //Session["User_LastLoginIP"] = "127.0.0.1";
        //Session["User_Addtime"] = "2010-11-9 10:41:00";
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
