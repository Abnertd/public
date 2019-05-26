<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        Config config = new Config();
        Public_Class pub = new Public_Class();
        config.Sys_UpdateApplication();
        config.Sys_UpdateReviewApplication();
        //pub.GetGoldPrice();
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
        Session["supplier_logined"] = "False";
        Session["supplier_id"] = 0;
        Session["supplier_email"] = "";
        Session["supplier_companyname"] = "";
        Session["supplier_logincount"] = 0;
        Session["supplier_lastlogin_time"] = "";
        Session["supplier_ishaveshop"] = 0;
        Session["Supplier_Isapply"] = 0;
        Session["supplier_grade"] = 0;
        Session["supplier_"] = 0;
        Session["supplier_auditstatus"] = 0;
        Session["member_logined"] = false;
        //临时！正式系统去掉

        //在新会话启动时运行的代码
        if (Request.Cookies["Member_UserName"] == null)
        {
            Response.Cookies["Member_UserName"].Value = "";
        }

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

        //客户来源
        Session["customer_source"] = "";
        if (Request.QueryString["source"] != null)
        {
            Session["customer_source"] = Request.QueryString["source"].ToString();
        }

        //物流商
        Session["Logistics_Logined"] = "False";
        Session["Logistics_ID"] = 0;
        Session["Logistics_NickName"] = "";
        Session["Logistics_CompanyName"] = "";
        Session["Logistics_Name"] = "";
        Session["Logistics_Tel"] = "";
        Session["Logistics_Status"] = 0;
        Session["supplier_lastlogin_time"] = "";
        
        //会员自动登录
        new Member().MemberAutoLogin();

        //会员登录后不再判断商家登录
        //if (Session["member_logined"] != "True" && Session["supplier_logined"] == "False")
        //{
            new Supplier().SupplierAutoLogin();
        //}
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
