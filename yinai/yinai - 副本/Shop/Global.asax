<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码

        Config config = new Config();
        config.Sys_UpdateApplication();
        config.Sys_UpdateReviewApplication();
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //供应商

        Session["shop_supplier_id"] = 0;
        Session["member_logined"] = false;
        
        //在新会话启动时运行的代码
        Response.Cookies["member_email"].Value = "";

        Session["Cur_Position"] = "";
        Session["Trade_Verify"] = "";
        Session["url_after_login"] = "";
        //临时自动登录
        Session["member_id"] = 0;
        Session["member_email"] = "";
        Session["member_nickname"] = "";
        Session["member_emailverify"] = "False";
        Session["member_logincount"] = 0;
        Session["member_lastlogin_time"] = "";
        Session["member_lastlogin_ip"] = "";
        Session["member_coinremain"] = 0;
        Session["member_coincount"] = 0;
        Session["member_grade"] = 0;
        Session["Member_AllowSysEmail"] = 0;

        Shop shop = new Shop();
        shop.Shop_Initial();
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
