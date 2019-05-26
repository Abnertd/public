using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Logistics 的摘要说明
/// </summary>
public class Logistics
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ILogistics MyBLL;
    private ILogisticsLine MyLogLine;
    private ITools tools;
    IEncrypt encrypt;
    Public_Class pub = new Public_Class();
    private ISupplierLogistics MySupplierLogistics;
    Addr addr = new Addr();
    private ILogisticsTender MyLogisticsTender;
    public Logistics()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBLL = LogisticsFactory.CreateLogistics();
        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MySupplierLogistics = SupplierLogisticsFactory.CreateSupplierLogistics();
        MyLogisticsTender = LogisticsTenderFactory.CreateLogisticsTender();
        MyLogLine = LogisticsLineFactory.CreateLogisticsLine();
    }


    public void Get_Logistics_Left_HTML(int main, int sub)
    {

        int Num = LogisgicsTender_Num();


        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"menu_1\">");
        sb.Append("<h2>物流中心</h2>");
        sb.Append("<div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box')\" ><a id=\"1\"  onClick=\"switchTag(1);\">物流管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box\">");
        sb.Append("<ul>");
        sb.Append("<li " + (main == 1 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Distribution_Logistics.aspx\">发布物流</a></li>");
        sb.Append("<li " + (main == 1 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Logistics.aspx\">我的报价<span>(" + Num + ")</span></a></li>");

        sb.Append("<li " + (main == 1 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Logistics_list.aspx\">物流信息</a></li>");
        sb.Append("<li " + (main == 1 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Distribution_LogisticsList.aspx\">查看发布信息</a></li>");

        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");

        sb.Append("<div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box2')\" ><a id=\"2\"  onClick=\"switchTag(2);\">我的资料</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box2\">");
        sb.Append("<ul>");
        sb.Append("<li " + (main == 2 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Logistics_password.aspx\">修改密码</a></li>");
        sb.Append("<li " + (main == 2 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/Logistics/Logistics_profile.aspx\">修改资料</a></li>");
        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");



        sb.Append("</div>");

        Response.Write(sb.ToString());
    }
    //物流商登录
    public void Logistics_Login()
    {
        string member_name = tools.CheckStr(tools.NullStr(Request.Form["Logistics_NickName"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["Logistics_Password"]).Trim());
        member_password = encrypt.MD5(member_password);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]).Trim()).ToLower();

        if (member_name == "")
        {
            Session["Logistics_Logined"] = "False";
            Response.Redirect("/Logistics/Logistics_login.aspx?login=umsg_k");
            Response.End();
        }

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            Session["Logistics_Logined"] = "False";
            Response.Redirect("/Logistics/Logistics_login.aspx?login=vmsg");
            Response.End();
        }

        LogisticsInfo entity = MyBLL.GetLogisticsByNickName(member_name, pub.CreateUserPrivilege("8426b82b-1be6-4d27-84a7-9d45597be557"));
        if (entity != null)
        {
            if (entity.Logistics_Password != member_password)
            {
                Session["Logistics_Logined"] = "False";
                Response.Redirect("/Logistics/Logistics_login.aspx?login=pmsg");
                Response.End();
            }
            if (entity.Logistics_Status == 0)
            {
                Session["Logistics_Logined"] = "False";
                Response.Redirect("/Logistics/Logistics_login.aspx?login=smsg");
                Response.End();
            }

            entity.Logistics_Lastlogin_Time = DateTime.Now;

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

            //物流商
            Session["Logistics_Logined"] = "True";
            Session["Logistics_ID"] = entity.Logistics_ID;
            Session["Logistics_NickName"] = entity.Logistics_NickName;
            Session["Logistics_CompanyName"] = entity.Logistics_CompanyName;
            Session["Logistics_Name"] = entity.Logistics_Name;
            Session["Logistics_Tel"] = entity.Logistics_Tel;
            Session["Logistics_Status"] = entity.Logistics_Status;
            Session["supplier_lastlogin_time"] = entity.Logistics_Lastlogin_Time;

            MyBLL.EditLogistics(entity, pub.CreateUserPrivilege("bd38ff8b-f627-44ec-9275-39c9df7425e1"));

            if (Session["url_after_login"] == null)
            {
                Session["url_after_login"] = "";
            }
            if (tools.NullStr(Session["url_after_login"].ToString()) != "")
            {
                Response.Redirect(Session["url_after_login"].ToString());
            }
            else
            {
                Response.Redirect("/Logistics/Logistics.aspx");
            }

        }
        else
        {
            Session["Logistics_Logined"] = "False";
            Response.Redirect("/Logistics/Logistics_login.aspx?login=err_active");
            Response.End();
        }
    }

    //物流商退出
    public void Logistics_LogOut()
    {
        Session.Abandon();
        Session["Logistics_Logined"] = "False";
        Response.Redirect("/Logistics/Logistics_login.aspx");
    }

    public bool Check_Logistics(int LogisticsID, int SupplierLogisticsID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_LogisticsID", "=", LogisticsID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_SupplierLogisticsID", "=", SupplierLogisticsID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));
        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public LogisticsInfo GetLogisticsByID(int ID)
    {
        return MyBLL.GetLogisticsByID(ID, pub.CreateUserPrivilege("8426b82b-1be6-4d27-84a7-9d45597be557"));
    }


    public void UpdateLogisticsPassword()
    {
        int Logistics_ID = tools.NullInt(Session["Logistics_ID"]);
        string old_pwd = tools.CheckStr(tools.NullStr(Request.Form["member_oldpassword"]));
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["member_password"]));
        string member_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["member_password_confirm"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (Logistics_ID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录物流商", false, "{back}");
        }

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }

        if (old_pwd == "")
        {
            pub.Msg("info", "提示信息", "请输入6～20位原密码", false, "{back}");
        }

        if (CheckSsn(member_password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (member_password.Length < 6 || member_password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (member_password != member_password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        old_pwd = encrypt.MD5(old_pwd);
        member_password = encrypt.MD5(member_password);

        LogisticsInfo entity = GetLogisticsByID(Logistics_ID);
        if (entity != null)
        {
            string Member_Password = entity.Logistics_Password;

            entity.Logistics_Password = member_password;

            if (old_pwd != Member_Password)
            {
                pub.Msg("info", "提示信息", "原密码输入错误，请重试！", false, "{back}");
            }
            if (MyBLL.EditLogistics(entity, pub.CreateUserPrivilege("bd38ff8b-f627-44ec-9275-39c9df7425e1")))
            {
                Response.Redirect("/Logistics/Logistics_password.aspx?tip=success");
            }
            else
            {
                pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
        }

    }

    public void UpdateLogisticsProfile()
    {
        int Logistics_ID = tools.NullInt(Session["Logistics_ID"]);
        string Logistics_CompanyName = tools.CheckStr(tools.NullStr(Request.Form["Logistics_CompanyName"]));
        string Logistics_Name = tools.CheckStr(tools.NullStr(Request.Form["Logistics_Name"]));
        string Logistics_Tel = tools.CheckStr(tools.NullStr(Request.Form["Logistics_Tel"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (Logistics_ID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录物流商", false, "{back}");
        }

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }
        if (Logistics_CompanyName.Length == 0)
        {
            pub.Msg("info", "提示信息", "请填写物流商公司名", false, "{back}");
        }
        if (Logistics_Name.Length == 0)
        {
            pub.Msg("info", "提示信息", "请填写联系人", false, "{back}");
        }

        if (!pub.Checkmobile_phone(Logistics_Tel) || Logistics_Tel.Length == 0)
        {
            pub.Msg("info", "提示信息", "请填写联系电话", false, "{back}");
        }

        LogisticsInfo entity = GetLogisticsByID(Logistics_ID);
        if (entity != null)
        {
            entity.Logistics_CompanyName = Logistics_CompanyName;
            entity.Logistics_Name = Logistics_Name;
            entity.Logistics_Tel = Logistics_Tel;

            if (MyBLL.EditLogistics(entity, pub.CreateUserPrivilege("bd38ff8b-f627-44ec-9275-39c9df7425e1")))
            {
                Response.Redirect("/Logistics/Logistics_profile.aspx?tip=success");
            }
            else
            {
                pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
        }
    }
    //检查密码
    public bool CheckSsn(string strSsn)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
        return regex.IsMatch(strSsn);
    }


    public string GetLogisticsNameByID(int ID)
    {
        LogisticsInfo entity = GetLogisticsByID(ID);
        if (entity != null)
        {
            return entity.Logistics_CompanyName;
        }
        else
        {
            return "--";
        }
    }
    public LogisticsInfo GetLogisticsByID()
    {
        int supplier_id = tools.CheckInt(Session["Logistics_ID"].ToString());
        if (supplier_id > 0)
        {
            return GetLogisticsByID(supplier_id);
        }
        else
        {
            return null;
        }
    }
    //物流商登录检查
    public void Logistics_Login_Check(string url_after_login)
    {
        if (tools.NullStr(Session["Logistics_Logined"]) != "True")
        {
            Session["url_after_login"] = url_after_login;
            Response.Redirect("/Logistics/Logistics_login.aspx");
        }
    }
    public SupplierLogisticsInfo GetSupplierLogisticsByID(int ID)
    {
        return MySupplierLogistics.GetSupplierLogisticsByID(ID, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
    }
    public virtual void AddSupplierLogistics()
    {
        int Supplier_Logistics_ID = tools.CheckInt(Request.Form["Supplier_Logistics_ID"]);
        int Supplier_SupplierID = tools.CheckInt(Request.Form["Supplier_SupplierID"]);
        int Supplier_OrdersID = tools.CheckInt(Request.Form["Supplier_OrdersID"]);
        int Supplier_LogisticsID = 0;
        int Supplier_Status = 1;
        string Supplier_Orders_Address_Country = tools.CheckStr(Request.Form["Supplier_Orders_Address_Country"]);
        string Supplier_Orders_Address_State = tools.CheckStr(Request.Form["Supplier_Orders_Address_State"]);
        string Supplier_Orders_Address_City = tools.CheckStr(Request.Form["Supplier_Orders_Address_City"]);
        string Supplier_Orders_Address_County = tools.CheckStr(Request.Form["Supplier_Orders_Address_County"]);
        string Supplier_Orders_Address_StreetAddress = tools.CheckStr(Request.Form["Supplier_Orders_Address_StreetAddress"]);
        string Supplier_Address_Country = tools.CheckStr(Request.Form["Supplier_Address_Country"]);
        string Supplier_Address_State = tools.CheckStr(Request.Form["Supplier_Address_State"]);
        string Supplier_Address_City = tools.CheckStr(Request.Form["Supplier_Address_City"]);
        string Supplier_Address_County = tools.CheckStr(Request.Form["Supplier_Address_County"]);
        string Supplier_Address_StreetAddress = tools.CheckStr(Request.Form["Supplier_Address_StreetAddress"]);
        string Supplier_Logistics_Name = tools.CheckStr(Request.Form["Supplier_Logistics_Name"]);
        string Supplier_Logistics_Number = tools.CheckStr(Request.Form["Supplier_Logistics_Number"]);
        DateTime Supplier_Logistics_DeliveryTime = tools.NullDate(Request.Form["Supplier_Logistics_DeliveryTime"]);
        int Supplier_Logistics_IsAudit = 0;
        DateTime Supplier_Logistics_AuditTime = DateTime.Now;
        string Supplier_Logistics_AuditRemarks = "";
        DateTime Supplier_Logistics_FinishTime = DateTime.Now;
        int Supplier_Logistics_TenderID = 0;
        double Supplier_Logistics_Price = 0;
        if (Supplier_OrdersID <= 0)
        {
            pub.Msg("error", "错误信息", "订单有误", false, "{back}");
        }
        if (Chcek_SupplierLogistic(Supplier_OrdersID))
        {
            pub.Msg("error", "错误信息", "该订单已生成物流信息", false, "{back}");
        }
        if (Supplier_Logistics_Name.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写货物名称", false, "{back}");
        }

        if (Supplier_Logistics_Number.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写数量", false, "{back}");
        }


        if (Supplier_Address_County == "0" || Supplier_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择发货地省市区信息", false, "{back}");
        }
        if (Supplier_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将发货地地址填写完整", false, "{back}");
        }

        if (Supplier_Orders_Address_County == "0" || Supplier_Orders_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择收货地省市区信息", false, "{back}");
        }
        if (Supplier_Orders_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将收货地地址填写完整", false, "{back}");
        }

        SupplierLogisticsInfo entity = new SupplierLogisticsInfo();
        entity.Supplier_Logistics_ID = Supplier_Logistics_ID;
        entity.Supplier_SupplierID = Supplier_SupplierID;
        entity.Supplier_OrdersID = Supplier_OrdersID;
        entity.Supplier_LogisticsID = Supplier_LogisticsID;
        entity.Supplier_Status = Supplier_Status;
        entity.Supplier_Orders_Address_Country = Supplier_Orders_Address_Country;
        entity.Supplier_Orders_Address_State = Supplier_Orders_Address_State;
        entity.Supplier_Orders_Address_City = Supplier_Orders_Address_City;
        entity.Supplier_Orders_Address_County = Supplier_Orders_Address_County;
        entity.Supplier_Orders_Address_StreetAddress = Supplier_Orders_Address_StreetAddress;
        entity.Supplier_Address_Country = Supplier_Address_Country;
        entity.Supplier_Address_State = Supplier_Address_State;
        entity.Supplier_Address_City = Supplier_Address_City;
        entity.Supplier_Address_County = Supplier_Address_County;
        entity.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
        entity.Supplier_Logistics_Name = Supplier_Logistics_Name;
        entity.Supplier_Logistics_Number = Supplier_Logistics_Number;
        entity.Supplier_Logistics_DeliveryTime = Supplier_Logistics_DeliveryTime;
        entity.Supplier_Logistics_IsAudit = Supplier_Logistics_IsAudit;
        entity.Supplier_Logistics_AuditTime = Supplier_Logistics_AuditTime;
        entity.Supplier_Logistics_AuditRemarks = Supplier_Logistics_AuditRemarks;
        entity.Supplier_Logistics_FinishTime = Supplier_Logistics_FinishTime;
        entity.Supplier_Logistics_TenderID = Supplier_Logistics_TenderID;
        entity.Supplier_Logistics_Price = Supplier_Logistics_Price;

        if (MySupplierLogistics.AddSupplierLogistics(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Logistics_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //检验订单是否已生成物流信息
    public bool Chcek_SupplierLogistic(int OrdersID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_OrdersID", "=", OrdersID.ToString()));

        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_ID", "Desc"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));

        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SupplierLogistics_List()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"200\" class=\"name\">货物名称</td>";
        tmp_head = tmp_head + "<td width=\"267\" class=\"name\">发货地</td>";
        tmp_head = tmp_head + "<td width=\"266\" class=\"name\">收货地</td>";
        tmp_head = tmp_head + "<td width=\"66\" class=\"name\">数量</td>";
        tmp_head = tmp_head + "<td width=\"86\" class=\"name\">状态</td>";
        tmp_head = tmp_head + "<td width=\"106\" class=\"name\">操作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无物流信息</td></tr>";

        page_url = "?keyword=" + keyword;
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_ID", "DESC"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
        PageInfo page = MySupplierLogistics.GetPageInfo(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));

        Response.Write(tmp_head);

        if (entitys != null)
        {
            tmp_list = "";
            foreach (SupplierLogisticsInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entity.Supplier_Logistics_Name + "</td>");
                Response.Write("<td>" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "&nbsp;" + entity.Supplier_Address_StreetAddress + "</td>");
                Response.Write("<td>" + addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "&nbsp;" + entity.Supplier_Orders_Address_StreetAddress + "</td>");
                Response.Write("<td>" + entity.Supplier_Logistics_Number + "</td>");
                Response.Write("<td>" + SupplierLogisticsStatus(entity) + "</td>");

                Response.Write("<td><span><a href=\"/supplier/Logistics_view.aspx?LogisticsID=" + entity.Supplier_Logistics_ID + "\">查看</a>");

                if (entity.Supplier_Logistics_IsAudit == 1 && entity.Supplier_Status == 1)
                {
                    Response.Write("&nbsp;&nbsp;<a href=\"/supplier/Logistics_Tender.aspx?ID=" + entity.Supplier_Logistics_ID + "\">查看报价</a>");
                }
                Response.Write("</span></td>");


                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }

    public void SupplierLogistics_IndexList()
    {

        string tmp_list = "<tr><td colspan=\"6\">暂无物流信息</td></tr>";
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_ID", "DESC"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
        if (entitys != null)
        {
            foreach (SupplierLogisticsInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entity.Supplier_Logistics_Name + "</td>");
                Response.Write("<td>" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "&nbsp;" + entity.Supplier_Address_StreetAddress + "</td>");
                Response.Write("<td>" + addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "&nbsp;" + entity.Supplier_Orders_Address_StreetAddress + "</td>");
                Response.Write("<td>" + entity.Supplier_Logistics_Number + "</td>");
                Response.Write("<td>" + SupplierLogisticsStatus(entity) + "</td>");

                Response.Write("<td><span><a href=\"/supplier/Logistics_view.aspx?LogisticsID=" + entity.Supplier_Logistics_ID + "\">查看</a>");

                if (entity.Supplier_Logistics_IsAudit == 1 && entity.Supplier_Status == 1)
                {
                    Response.Write("&nbsp;&nbsp;<a href=\"/supplier/Logistics_Tender.aspx?ID=" + entity.Supplier_Logistics_ID + "\">查看报价</a>");
                }
                Response.Write("</span></td>");


                Response.Write("</tr>");
            }
        }
        else
        {
            Response.Write(tmp_list);
        }
    }
    public string SupplierLogisticsStatus(SupplierLogisticsInfo entity)
    {
        string Name = "--";
        if (entity != null)
        {
            if (entity.Supplier_Status == 0)
            {
                Name = "待发布";
            }
            else if (entity.Supplier_Status == 1)
            {
                if (entity.Supplier_Logistics_IsAudit == 0)
                {
                    Name = "待审核";
                }
                else if (entity.Supplier_Logistics_IsAudit == 1)
                {
                    if (entity.Supplier_LogisticsID == 0)
                    {
                        Name = "配货中";
                    }
                    else
                    {
                        Name = "已完成";
                    }
                }
                else
                {
                    Name = "审核不通过";
                }
            }
            else
            {
                Name = "已撤销";
            }
        }
        return Name;
    }

    public void Logistics_IndexList()
    {
        string Supplier_Address_State = tools.CheckStr(Request["Supplier_Address_State"]);
        string Supplier_Address_City = tools.CheckStr(Request["Supplier_Address_City"]);
        string Supplier_Orders_Address_State = tools.CheckStr(Request["Supplier_Orders_Address_State"]);
        string Supplier_Orders_Address_City = tools.CheckStr(Request["Supplier_Orders_Address_City"]);

        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<thead>";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"300\">发货地</td>";
        tmp_head = tmp_head + "<td width=\"300\">收货地</td>";
        tmp_head = tmp_head + "<td width=\"200\">货物名称</td>";
        tmp_head = tmp_head + "<td width=\"100\">数量</td>";
        tmp_head = tmp_head + "<td width=\"100\">发货时间</td>";
        tmp_head = tmp_head + "<td width=\"100\">状态</td>";
        tmp_head = tmp_head + "<td width=\"100\">操作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_head = tmp_head + "</thead>";

        tmp_list = tmp_list + "<tr><td colspan=\"7\">暂无物流信息</td></tr>";

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";

        if (Supplier_Address_State == "0")
        {
            Supplier_Address_State = "";
        }
        if (Supplier_Address_City == "0")
        {
            Supplier_Address_City = "";
        }
        if (Supplier_Orders_Address_State == "0")
        {
            Supplier_Orders_Address_State = "";
        }
        if (Supplier_Orders_Address_City == "0")
        {
            Supplier_Orders_Address_City = "";
        }
        page_url = "?Supplier_Address_State=" + Supplier_Address_State + "&Supplier_Address_City=" + Supplier_Address_City + "&Supplier_Orders_Address_State=" + Supplier_Orders_Address_State + "&Supplier_Orders_Address_City=" + Supplier_Orders_Address_City;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Logistics_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_LogisticsID", "=", "0"));
        if (Supplier_Address_State != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Address_State", "=", Supplier_Address_State));
        }
        if (Supplier_Address_City != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Address_City", "=", Supplier_Address_City));
        }
        if (Supplier_Orders_Address_State != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Orders_Address_State", "=", Supplier_Orders_Address_State));
        }
        if (Supplier_Orders_Address_City != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Orders_Address_City", "=", Supplier_Orders_Address_City));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_ID", "DESC"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
        PageInfo page = MySupplierLogistics.GetPageInfo(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (SupplierLogisticsInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "&nbsp;</td>";
                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "&nbsp;</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Name + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Number + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + SupplierLogisticsStatus(entity) + "</td>";
                if (entity.Supplier_LogisticsID == 0)
                {
                    if (Session["member_id"].ToString() != "0")
                    {
                        tmp_list = tmp_list + "<td><a href=\"/Logistics/view.aspx?ID=" + entity.Supplier_Logistics_ID + "\">详情>></a></td>";

                    }
                    else
                    {
                        tmp_list = tmp_list + "<td><a href=\"/login.aspx" + "\">登录查看</a></td>";

                    }
                }
                else
                {
                    tmp_list = tmp_list + "<td><a>--</a></td>";
                }

                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }

    //我有发货线路
    public void Logistics_LineList()
    {
        //string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"1200px;\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<thead>";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"200\">联系人</td>";
        tmp_head = tmp_head + "<td width=\"200\">联系电话</td>";
        tmp_head = tmp_head + "<td width=\"200\">车型</td>";
        tmp_head = tmp_head + "<td width=\"100\">发货地址</td>";
        tmp_head = tmp_head + "<td width=\"200\">收货地址</td>";
        tmp_head = tmp_head + "<td width=\"100\">发货时间</td>";
        tmp_head = tmp_head + "<td width=\"100\">发货价格</td>";
        //tmp_head = tmp_head + "<td width=\"100\">操作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_head = tmp_head + "</thead>";

        tmp_list = tmp_list + "<tr><td colspan=\"7\">暂无物流信息</td></tr>";

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";
        int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);
        //page_url = "?Supplier_Address_State=" + Supplier_Address_State + "&Supplier_Address_City=" + Supplier_Address_City + "&Supplier_Orders_Address_State=" + Supplier_Orders_Address_State + "&Supplier_Orders_Address_City=" + Supplier_Orders_Address_City;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsLineInfo.Logistics_ID", ">", "0"));

        Query.OrderInfos.Add(new OrderInfo("LogisticsLineInfo.Logistics_Line_ID", "DESC"));

        IList<LogisticsLineInfo> entitys = MyLogLine.GetLogisticsLines(Query);


        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (LogisticsLineInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Contact + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Note + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_CarType + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Delivery_Address + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Receiving_Address + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_DeliverTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Deliver_Price + "</td>";
                //tmp_list = tmp_list + "<td><a href  ></td>";

                //tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Name + "</td>";
                //tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Number + "</td>";
                //tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") + "</td>";
                //if (entity.Supplier_LogisticsID == 0)
                //{
                //    tmp_list = tmp_list + "<td><a href=\"/Logistics/view.aspx?ID=" + entity.Supplier_Logistics_ID + "\">详情>></a></td>";
                //}
                //else
                //{
                //    tmp_list = tmp_list + "<td><a>--</a></td>";
                //}

                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);


        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }



    public void Logistics_List()
    {
        string Supplier_Address_State = tools.CheckStr(Request["Supplier_Address_State"]);
        string Supplier_Address_City = tools.CheckStr(Request["Supplier_Address_City"]);
        string Supplier_Orders_Address_State = tools.CheckStr(Request["Supplier_Orders_Address_State"]);
        string Supplier_Orders_Address_City = tools.CheckStr(Request["Supplier_Orders_Address_City"]);

        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"974\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<thead>";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"250\">发货地</td>";
        tmp_head = tmp_head + "<td width=\"250\">收货地</td>";
        tmp_head = tmp_head + "<td width=\"200\">货物名称</td>";
        tmp_head = tmp_head + "<td width=\"100\">数量</td>";
        tmp_head = tmp_head + "<td width=\"100\">到货时间</td>";
        tmp_head = tmp_head + "<td width=\"100\">操作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_head = tmp_head + "</thead>";

        tmp_list = tmp_list + "<tr><td colspan=\"7\">暂无物流信息</td></tr>";

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";
        if (Supplier_Address_State == "0")
        {
            Supplier_Address_State = "";
        }
        if (Supplier_Address_City == "0")
        {
            Supplier_Address_City = "";
        }
        if (Supplier_Orders_Address_State == "0")
        {
            Supplier_Orders_Address_State = "";
        }
        if (Supplier_Orders_Address_City == "0")
        {
            Supplier_Orders_Address_City = "";
        }
        page_url = "?Supplier_Address_State=" + Supplier_Address_State + "&Supplier_Address_City=" + Supplier_Address_City + "&Supplier_Orders_Address_State=" + Supplier_Orders_Address_State + "&Supplier_Orders_Address_City=" + Supplier_Orders_Address_City;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Logistics_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_LogisticsID", "=", "0"));
        if (Supplier_Address_State != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Address_State", "=", Supplier_Address_State));
        }
        if (Supplier_Address_City != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Address_City", "=", Supplier_Address_City));
        }
        if (Supplier_Orders_Address_State != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Orders_Address_State", "=", Supplier_Orders_Address_State));
        }
        if (Supplier_Orders_Address_City != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Orders_Address_City", "=", Supplier_Orders_Address_City));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_ID", "DESC"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
        PageInfo page = MySupplierLogistics.GetPageInfo(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (SupplierLogisticsInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "</td>";
                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Name + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Number + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") + "</td>";
                if (entity.Supplier_LogisticsID == 0)
                {
                    tmp_list = tmp_list + "<td><a href=\"/Logistics/view.aspx?ID=" + entity.Supplier_Logistics_ID + "\">详情>></a></td>";
                }
                else
                {
                    tmp_list = tmp_list + "<td><a>--</a></td>";
                }

                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }
    public void IndexList()
    {
        string tmp_list = "<tr><td colspan=\"6\">暂无物流信息</td></tr>";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Logistics_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_LogisticsID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierLogisticsInfo.Supplier_Logistics_TenderID", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo("SupplierLogisticsInfo.Supplier_Logistics_FinishTime", "DESC"));

        IList<SupplierLogisticsInfo> entitys = MySupplierLogistics.GetSupplierLogisticss(Query, pub.CreateUserPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"));
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (SupplierLogisticsInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Name + "</td>";
                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "</td>";

                tmp_list = tmp_list + "<td>" + addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Supplier_Logistics_Number + "</td>";
                tmp_list = tmp_list + "<td><a href=\"/Logistics/Logistics_Business.aspx?id=" + entity.Supplier_Logistics_ID + "\">查看物流</a></td>";
                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list);
        }
        else
        {
            Response.Write(tmp_list);
        }
    }


    #region 报价
    public void AddLogisticsTender()
    {
    
        int Logistics_Tender_ID = tools.CheckInt(Request.Form["Logistics_Tender_ID"]);
        int Logistics_Tender_LogisticsID = tools.NullInt(Session["Logistics_ID"]);
        int Logistics_Tender_SupplierLogisticsID = tools.CheckInt(Request.Form["LogisticsID"]);
        int Logistics_Tender_OrderID = 0;
        double Logistics_Tender_Price = tools.CheckFloat(Request.Form["Logistics_Tender_Price"]);
        string Supplier_Logistics_AuditRemarks = tools.NullStr(Request.Form["Logistics_AuditRemarks"]);
        string Supplier_Logistics_Number = tools.NullStr(Request.Form["Logistics_Number"]);
        DateTime Logistics_Tender_AddTime = DateTime.Now;
        int Logistics_Tender_IsWin = 0;
        DateTime Logistics_Tender_FinishTime = DateTime.Now;


        

        if (Logistics_Tender_LogisticsID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录物流商", false, "{back}");
        }

        SupplierLogisticsInfo supplierlogisticsinfo = GetSupplierLogisticsByID(Logistics_Tender_SupplierLogisticsID);
        if (supplierlogisticsinfo == null)
        {
            pub.Msg("error", "错误信息", "物流信息不存在", false, "{back}");
        }
        if (supplierlogisticsinfo.Supplier_LogisticsID > 0)
        {
            pub.Msg("error", "错误信息", "物流信息已结束，不能报价", false, "{back}");
        }
        Logistics_Tender_OrderID = supplierlogisticsinfo.Supplier_OrdersID;

        if (Check_Logistics(Logistics_Tender_LogisticsID, Logistics_Tender_SupplierLogisticsID))
        {
            pub.Msg("error", "错误信息", "已报价，不能重复报价", false, "{back}");
        }

        if (Logistics_Tender_Price <= 0)
        {
            pub.Msg("error", "错误信息", "请填写报价金额", false, "{back}");
        }
        if (Supplier_Logistics_Number == null)
        {
            pub.Msg("error", "错误信息", "请填写载重", false, "{back}");
        }
        if (Supplier_Logistics_AuditRemarks ==null)
        {
            pub.Msg("error", "错误信息", "请填写车型", false, "{back}");
        }
        LogisticsTenderInfo entity = new LogisticsTenderInfo();
        entity.Logistics_Tender_ID = Logistics_Tender_ID;
        entity.Logistics_Tender_LogisticsID = Logistics_Tender_LogisticsID;
        entity.Logistics_Tender_SupplierLogisticsID = Logistics_Tender_SupplierLogisticsID;
        entity.Logistics_Tender_OrderID = Logistics_Tender_OrderID;
        entity.Logistics_Tender_Price = Logistics_Tender_Price;
        entity.Logistics_Tender_AddTime = Logistics_Tender_AddTime;
        entity.Logistics_Tender_IsWin = Logistics_Tender_IsWin;
        entity.Logistics_Tender_FinishTime = Logistics_Tender_FinishTime;
        SysMessage messageclass = new SysMessage();

        messageclass.SendMessage(1, 2, tools.NullInt(Session["member_id"]), 0, "" + tools.NullStr(Session["Member_Company"]) +"已报价 ");


        if (MyLogisticsTender.AddLogisticsTender(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Logistics.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public int LogisgicsTender_Num()
    {
        int Num = 0;
        int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);
        QueryInfo Query = new QueryInfo();
       
      

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_LogisticsID", "=", LogisgicsID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));

        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        if (entitys!=null)
        {
            Num= entitys.Count;
        }
        return Num;
    }

    public void LogisgicsTender_List()
    {
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);
        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"974\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"368\" class=\"name\">货物名称</td>";
        tmp_head = tmp_head + "<td width=\"169\" class=\"name\">报价</td>";
        tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
        tmp_head = tmp_head + "<td width=\"127\" class=\"name\">查看进度</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        tmp_list = tmp_list + "<tr><td colspan=\"4\">暂无报价</td></tr>";

        if (curpage < 1)
        {
            curpage = 1;
        }

        page_url = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_LogisticsID", "=", LogisgicsID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));

        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        PageInfo page = MyLogisticsTender.GetPageInfo(Query);
        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";

            foreach (LogisticsTenderInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + GetSupplierLogisgicsNameByID(entity.Logistics_Tender_SupplierLogisticsID) + "</td>";
                tmp_list = tmp_list + "<td>" + pub.FormatCurrency(entity.Logistics_Tender_Price) + "</td>";
                tmp_list = tmp_list + "<td>" + GetLogisgicsTenderIsWin(entity.Logistics_Tender_IsWin) + "</td>";
                tmp_list = tmp_list + "<td><span><a href=\"/Logistics/Logistics_view.aspx?ID=" + entity.Logistics_Tender_ID + "\">查看</a></span></td>";
                tmp_list = tmp_list + "</tr>";
            }
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }


    public void Supplier_LogisgicsTender_List(int SupplierLogisticsID)
    {
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);
        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"974\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"368\" class=\"name\">物流商名称</td>";
        tmp_head = tmp_head + "<td width=\"169\" class=\"name\">报价</td>";
        tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
        tmp_head = tmp_head + "<td width=\"127\" class=\"name\">查看进度</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        tmp_list = tmp_list + "<tr><td colspan=\"4\">暂无报价</td></tr>";

        if (curpage < 1)
        {
            curpage = 1;
        }

        page_url = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_SupplierLogisticsID", "=", SupplierLogisticsID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_IsWin", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));

        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        PageInfo page = MyLogisticsTender.GetPageInfo(Query);
        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";

            foreach (LogisticsTenderInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + GetLogisgicsNameByID(entity.Logistics_Tender_LogisticsID) + "</td>";
                tmp_list = tmp_list + "<td>" + pub.FormatCurrency(entity.Logistics_Tender_Price) + "</td>";
                tmp_list = tmp_list + "<td>" + GetLogisgicsTenderIsWin(entity.Logistics_Tender_IsWin) + "</td>";
                tmp_list = tmp_list + "<td><span><a href=\"/Supplier/Logistics_Tender_view.aspx?ID=" + entity.Logistics_Tender_ID + "\">查看</a></span></td>";
                tmp_list = tmp_list + "</tr>";
            }
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }
    public string GetSupplierLogisgicsNameByID(int ID)
    {
        SupplierLogisticsInfo entity = GetSupplierLogisticsByID(ID);
        if (entity != null)
        {
            return entity.Supplier_Logistics_Name;
        }
        else
        {
            return "--";
        }
    }

    public string GetLogisgicsNameByID(int ID)
    {
        LogisticsInfo entity = GetLogisticsByID(ID);
        if (entity != null)
        {
            return entity.Logistics_CompanyName;
        }
        else
        {
            return "--";
        }
    }
    public string GetLogisgicsTenderIsWin(int IsWin)
    {
        string Name = "";

        switch (IsWin)
        {
            case 0:
                Name = "--";
                break;
            case 1:
                Name = "中标";
                break;
            case 2:
                Name = "未中标";
                break;
        }
        return Name;
    }

    public LogisticsTenderInfo GetLogisticsTenderByID(int ID)
    {
        return MyLogisticsTender.GetLogisticsTenderByID(ID);
    }

    public void WinLogistics()
    {
        int Supplier_Logistics_ID = tools.CheckInt(Request.Form["Supplier_Logistics_ID"]);
        int TenderID = tools.CheckInt(Request.Form["TenderID"]);

        SupplierLogisticsInfo entity = GetSupplierLogisticsByID(Supplier_Logistics_ID);
        if (entity != null)
        {
            if (entity.Supplier_Logistics_TenderID > 0)
            {
                pub.Msg("error", "错误信息", "已选物流商，不能重复选择", false, "{back}");
                return;
            }
            LogisticsTenderInfo tender = GetLogisticsTenderByID(TenderID);
            if (tender != null)
            {
                if (tender.Logistics_Tender_IsWin == 0)
                {
                    entity.Supplier_LogisticsID = tender.Logistics_Tender_LogisticsID;
                    entity.Supplier_Logistics_TenderID = tender.Logistics_Tender_ID;
                    entity.Supplier_Logistics_Price = tender.Logistics_Tender_Price;
                    entity.Supplier_Logistics_FinishTime = DateTime.Now;

                    if (MySupplierLogistics.EditSupplierLogistics(entity, pub.CreateUserPrivilege("65632742-f14a-4e44-8f7d-64e56c866da4")))
                    {
                        tender.Logistics_Tender_IsWin = 1;
                        tender.Logistics_Tender_FinishTime = DateTime.Now;
                        MyLogisticsTender.EditLogisticsTender(tender);
                        WinLogisticsTender(entity.Supplier_Logistics_ID);
                        pub.Msg("positive", "操作成功", "操作成功", true, "Logistics_Tender_view.aspx?ID=" + TenderID);
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void WinLogisticsTender(int SupplierLogisticsID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_SupplierLogisticsID", "=", SupplierLogisticsID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsTenderInfo.Logistics_Tender_IsWin", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));

        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        if (entitys != null)
        {
            foreach (LogisticsTenderInfo entity in entitys)
            {
                entity.Logistics_Tender_IsWin = 2;
                entity.Logistics_Tender_FinishTime = DateTime.Now;
                MyLogisticsTender.EditLogisticsTender(entity);
            }
        }
    }
    #endregion



    #region 新加物流信息

    public void AddLogisticsLine()
    {
        
        string Logistics_Line_Contact = tools.CheckStr(Request.Form["Logistics_Line_Contact"]);
        string Logistics_Line_Note = tools.CheckStr(Request.Form["Logistics_Line_Note"]);
        string Logistics_Line_CarType = tools.CheckStr(Request.Form["Logistics_Line_CarType"]);
        string Logistics_Line_Delivery_Address = tools.CheckStr(Request.Form["Logistics_Line_Delivery_Address"]);
        string Logistics_Line_Receiving_Address = tools.CheckStr(Request.Form["Logistics_Line_Receiving_Address"]);
        DateTime Logistics_Line_DeliverTime = tools.NullDate(Request.Form["Logistics_Line_DeliverTime"]);
        double Logistics_Line_Deliver_Price = tools.CheckFloat(Request.Form["Logistics_Line_Deliver_Price"]);

        int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);


        if (Logistics_Line_Contact.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入联系人", false, "{back}");
        }
        if (Logistics_Line_Note.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入联系电话", false, "{back}");
        }
        if (Logistics_Line_CarType.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入车型", false, "{back}");
        }
        if (Logistics_Line_Delivery_Address.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入发货地址", false, "{back}");


        } if (Logistics_Line_Receiving_Address.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入收货地址", false, "{back}");

        }
        if (Logistics_Line_DeliverTime.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入发货时间", false, "{back}");
        }
        if (Logistics_Line_Deliver_Price.ToString().Length <= 0)
        {
            pub.Msg("error", "错误信息", "请输入运费", false, "{back}");
        }
        if (LogisgicsID<=0)
        {
              pub.Msg("error", "错误信息", "物流商不存在", false, "{back}");
        }

       

        LogisticsLineInfo entity = new LogisticsLineInfo();
        entity.Logistics_Line_Contact = Logistics_Line_Contact;
        entity.Logistics_Line_CarType = Logistics_Line_CarType;
        entity.Logistics_Line_Delivery_Address = Logistics_Line_Delivery_Address;
        entity.Logistics_Line_Receiving_Address = Logistics_Line_Receiving_Address;
        entity.Logistics_Line_DeliverTime = Logistics_Line_DeliverTime;
        entity.Logistics_Line_Deliver_Price = Logistics_Line_Deliver_Price;
        entity.Logistics_Line_Note = Logistics_Line_Note;
        entity.Logistics_ID = LogisgicsID;
        if (MyLogLine.AddLogisticsLine(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Distribution_Logistics.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    public void LogisticsLine_List()
    {
      
        //string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"976px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<thead>";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"122\">联系人</td>";
        tmp_head = tmp_head + "<td width=\"122\">联系电话</td>";
        tmp_head = tmp_head + "<td width=\"122\">车型</td>";
        tmp_head = tmp_head + "<td width=\"122\">发货地址</td>";
        tmp_head = tmp_head + "<td width=\"122\">收货地址</td>";
        tmp_head = tmp_head + "<td width=\"122\">发货时间</td>";
        tmp_head = tmp_head + "<td width=\"122\">发货价格</td>";
        tmp_head = tmp_head + "<td width=\"122\">操 作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_head = tmp_head + "</thead>";

        tmp_list = tmp_list + "<tr><td colspan=\"8\">暂无物流信息</td></tr>";

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";
           int LogisgicsID = tools.NullInt(Session["Logistics_ID"]);
        //page_url = "?Supplier_Address_State=" + Supplier_Address_State + "&Supplier_Address_City=" + Supplier_Address_City + "&Supplier_Orders_Address_State=" + Supplier_Orders_Address_State + "&Supplier_Orders_Address_City=" + Supplier_Orders_Address_City;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LogisticsLineInfo.Logistics_ID", "=", LogisgicsID.ToString()));




        Query.OrderInfos.Add(new OrderInfo("LogisticsLineInfo.Logistics_Line_ID", "DESC"));

        IList<LogisticsLineInfo> entitys = MyLogLine.GetLogisticsLines(Query);
      

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (LogisticsLineInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Contact + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Note + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_CarType + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Delivery_Address + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Receiving_Address + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_DeliverTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Logistics_Line_Deliver_Price + "</td>";
                tmp_list = tmp_list + "<td><span><a href=\"/Logistics/Logistics_do.aspx?action=LogisticMove&Logistics_Line_ID=" + entity.Logistics_Line_ID + "\">删除</a></span></td>";
              

                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

           
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }



    public virtual void DelLogistic()
    {
        int Logistics_Line_ID = tools.CheckInt(Request.QueryString["Logistics_Line_ID"]);
        if (MyLogLine.DelLogisticsLine(Logistics_Line_ID) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "/Logistics/Distribution_LogisticsList.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    #endregion

}