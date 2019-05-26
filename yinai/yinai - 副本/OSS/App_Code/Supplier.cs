using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.Sys;
using Glaer.Trade.Util.SQLHelper;
using System.Data.SqlClient;

/// <summary>
///Member 的摘要说明
/// </summary>
public class Supplier
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private ISupplier MyBLL;
    private ISupplierCert MyCert;
    private ISupplierCertType MyCertType;
    private ISupplierPayBackApply MyPayBackApply;
    private ISupplierCloseShopApply MyCloseShopApply;
    private ISupplierCommissionCategory MyBLLCate;
    private ISupplierConsumption MyCoinlog;
    private ISupplierMessage MyMessage;
    private IProduct MyProduct;
    private Addr addr;
    private ISupplierShopApply MyBLLShopApply;
    private ISupplierShop MyBLLShop;
    private ISupplierTag MyTag;
    private ISupplierBank Mybank;
    private ISupplierAccountLog MyAccountLog;
    private ISupplierPurchase MyPurchase;
    private ISupplierPurchaseDetail MyPurchaseDetail;
    private ISupplierPriceAsk MyPriceAsk;
    private ISupplierPriceReport MyPriceReport;
    private ISupplierPriceReportDetail MyPriceReportDetail;
    private IMail mail;
    private ISupplierPurchaseCategory MyPurchaseCategory;
    private ISupplierAgentProtocal MyAgentProtocal;
    private ISupplierCreditLimitLog myCreditlimit;
    private ISysMessage MySysMessage;
    private ISupplierMargin MyMargin;
    private ISupplierOnline MySupplierOnline;
    private IMember MyMem;
    private IZhongXin MyZhongxin;

    public Supplier()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SupplierFactory.CreateSupplier();
        encrypt = EncryptFactory.CreateEncrypt();
        MyProduct = ProductFactory.CreateProduct();
        addr = new Addr();
        MyCert = SupplierCertFactory.CreateSupplierCert();
        MyCertType = SupplierCertFactory.CreateSupplierCertType();
        MyPayBackApply = SupplierPayBackApplyFactory.CreateSupplierPayBackApply();
        MyCloseShopApply = SupplierCloseShopApplyFactory.CreateSupplierCloseShopApply();
        MyBLLCate = SupplierCommissionCategoryFactory.CreateSupplierCommissionCategory();
        MyMessage = SupplierMessageFactory.CreateSupplierMessage();
        MyBLLShopApply = SupplierShopApplyFactory.CreateSupplierShopApply();
        MyBLLShop = SupplierShopFactory.CreateSupplierShop();
        MyTag = SupplierTagFactory.CreateSupplierTag();
        Mybank = SupplierBankFactory.CreateSupplierBank();
        MyAccountLog = SupplierAccountLogFactory.CreateSupplierAccountLog();
        MyPurchase = SupplierPurchaseFactory.CreateSupplierPurchase();
        MyPurchaseDetail = SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
        MyPriceAsk = SupplierPriceAskFactory.CreateSupplierPriceAsk();
        MyPriceReport = SupplierPriceReportFactory.CreateSupplierPriceReport();
        MyPriceReportDetail = SupplierPriceReportDetailFactory.CreateSupplierPriceReportDetail();
        mail = MailFactory.CreateMail();
        MyPurchaseCategory = SupplierPurchaseCategoryFactory.CreateSupplierPurchaseCategory();
        MyAgentProtocal = SupplierAgentProtocalFactory.CreateSupplierAgentProtocal();
        MyCoinlog = SupplierConsumptionFactory.CreateSupplierConsumption();
        myCreditlimit = SupplierCreditLimitLogFactory.CreateSupplierCreditLimitLog();
        MySysMessage = SysMessageFactory.CreateSysMessage();
        MyMargin = SupplierMarginFactory.CreateSupplierMargin();
        MySupplierOnline = SupplierOnlineFactory.CreateSupplierOnline();
        MyMem = MemberFactory.CreateMember();
        MyZhongxin = ZhongXinFactory.Create();
    }

    #region 供应商管理

    public string Get_Audit_Status(int Status)
    {
        string Status_Name = "";
        switch (Status)
        {
            case 0:
                Status_Name = "未审核";
                break;
            case 1:
                Status_Name = "已审核";
                break;
            case 2:
                Status_Name = "审核未通过";
                break;
        }
        return Status_Name;
    }

    public string GetSuppliersByKeyword(string keyword)
    {
        string supplier_id = "0";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
        }

        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            foreach (SupplierInfo entity in entitys)
            {
                supplier_id = supplier_id + "," + entity.Supplier_ID;

            }
        }

        return supplier_id;
    }

    public SupplierInfo GetSupplierByID(int ID)
    {
        return MyBLL.GetSupplierByID(ID, Public.GetUserPrivilege());
    }

    //跟商家编号 获取商家QQ信息
    public SupplierOnlineInfo GetSupplierOnlineByID(int ID)
    {
        return MySupplierOnline.GetSupplierOnlineByID(ID);
    }

    //跟商家编号 获取商家QQ信息
    public IList<SupplierOnlineInfo> GetSupplierOnlinesByID(int ID)
    {
        QueryInfo Query = new QueryInfo();

        // Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Nickname", "like", keyword));
        //Query.ParamInfos.Add(new ParamInfo("OR", "str", "SupplierInfo.Supplier_Email", "like", keyword));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_SupplierID", "=", ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Site", "=", Public.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Type", "=", "QQ"));
        Query.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_ID", "Desc"));

        return MySupplierOnline.GetSupplierOnlines(Query);
    }

    //获取商家列表信息
    public string GetSuppliers()
    {
        string Supplier_Nickname = "--";
        string Supplier_CompanyName = "--";
        string Supplier_Contactman = "--";
        string Supplier_Phone = "--";

        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        int Supplier_Trash = tools.CheckInt(Request["Trash"]);
        int Supplier_Status = tools.CheckInt(Request["Status"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Nickname", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "SupplierInfo.Supplier_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
        }

        if (Supplier_Trash > -1)
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Trash", "=", Supplier_Trash.ToString()));

        if (Supplier_Status > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Status", "=", Supplier_Status.ToString()));
        }
        else
        {
            if (Audit > -1)
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_AuditStatus", "=", Audit.ToString()));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierInfo entity in entitys)
            {

                int Member_id = new Member().GetMemberID_BySupplierID(entity.Supplier_ID);
                MemberInfo memberinfo = new Member().GetMemberByID(Member_id);
                //MemberInfo memberinfo = new Member().GetMemberByEmail(entity.Supplier_Email);
                if (memberinfo != null)
                {
                    Supplier_Nickname = memberinfo.Member_NickName;
                    MemberProfileInfo memberprofileinfo = new Member().GetMemberProfileByID(memberinfo.Member_ID);
                    if (memberprofileinfo != null)
                    {
                        Supplier_CompanyName = memberprofileinfo.Member_Company;
                        Supplier_Contactman = memberprofileinfo.Member_Name;
                        Supplier_Phone = memberprofileinfo.Member_Phone_Number;
                    }
                }



                jsonBuilder.Append("{\"id\":" + entity.Supplier_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Nickname));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.Supplier_Email));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Contactman));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County)));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_Phone));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Mobile);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //if (entity.Supplier_IsHaveShop == 0)
                //{
                //    jsonBuilder.Append("未开通");
                //}
                //else
                //{
                //    jsonBuilder.Append("已开通");
                //}
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_AuditStatus == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Supplier_AuditStatus == 2)
                {
                    jsonBuilder.Append("审核不通过");
                }
                else
                {
                    jsonBuilder.Append("审核通过");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_Status == 0)
                {
                    jsonBuilder.Append("未启用");
                }
                else if (entity.Supplier_Status == 2)
                {
                    jsonBuilder.Append("已冻结");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Addtime);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Supplier_Lastlogintime);
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_LoginCount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                //if (Public.CheckPrivilege("d38ea434-9b11-4668-9fb9-2827a9d22602") && entity.Supplier_IsHaveShop == 0 && entity.Supplier_AuditStatus == 1)
                //{
                //    jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"开通店铺\\\" align=\\\"absmiddle\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmoperate('Supplier_do.aspx?action=shopadd&Supplier_id=" + entity.Supplier_ID + "')\\\" title=\\\"开通店铺\\\">开通店铺</a> ");
                //}

                if (Public.CheckPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"))
                {
                    //jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"设置合同\\\" align=\\\"absmiddle\\\"> <a href=\\\"Contract_Settings.aspx?Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"设置合同\\\">设置合同</a>");

                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_View.aspx?Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }


    public string GetzhongxinSuppliers()
    {

        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        int Supplier_Trash = tools.CheckInt(Request["Trash"]);
        int Supplier_Status = tools.CheckInt(Request["Status"]);
        int Supplier_Type = tools.CheckInt(Request["Supplier_Type"]);
        int account = tools.CheckInt(Request["account"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Type", "=", Supplier_Type.ToString()));//0卖家，1买家
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Nickname", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "SupplierInfo.Supplier_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
        }

        if (Supplier_Trash > -1)
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Trash", "=", Supplier_Trash.ToString()));

        //if (Supplier_Status > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Status", "=", Supplier_Status.ToString()));
        //}
        //else
        //{
        //    if (Audit > -1)
        //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_AuditStatus", "=", Audit.ToString()));
        //}

        string Supplier_IsJiaoFei = tools.CheckStr(Request.QueryString["Supplier_IsJiaoFei"]);
        if (new List<string> { "0", "1" }.Contains(Supplier_IsJiaoFei))
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_IsJiaoFei", "=", Supplier_IsJiaoFei));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Supplier_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<a href=\\\"Supplier_View.aspx?Supplier_Type=" + entity.Supplier_Type + "&Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"查看\\\">");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Nickname));
                jsonBuilder.Append("</a>\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.Supplier_Email));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<a href=\\\"Supplier_View.aspx?Supplier_Type=" + entity.Supplier_Type + "&Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"查看\\\">");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_CompanyName));
                jsonBuilder.Append("</a>\",");

                //jsonBuilder.Append("\"");
                //0卖家，1买家
                //if (entity.Supplier_Type == 0)
                //{
                //    jsonBuilder.Append("卖家");
                //}
                //else
                //{
                //    jsonBuilder.Append("买家");
                //}
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Contactman));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County)));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Mobile));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //if (entity.Supplier_IsHaveShop == 0)
                //{
                //    jsonBuilder.Append("未开通");
                //}
                //else
                //{
                //    jsonBuilder.Append("已开通");
                //}
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_AuditStatus == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Supplier_AuditStatus == 2)
                {
                    jsonBuilder.Append("审核不通过");
                }
                else
                {
                    jsonBuilder.Append("审核通过");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_Status == 0)
                {
                    jsonBuilder.Append("未启用");
                }
                else if (entity.Supplier_Status == 2)
                {
                    jsonBuilder.Append("已冻结");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //if (entity.Supplier_AllowOrderEmail == 0)
                //{
                //    jsonBuilder.Append("未启用");
                //}
                //else
                //{
                //    jsonBuilder.Append("启用");
                //}
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Lastlogintime);
                jsonBuilder.Append("\",");

                if (account == 1)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.DisplayCurrency(new ZhongXin().GetZhongXinAccountRemainByMemberID(entity.Supplier_ID)));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Supplier_LoginCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    //if (entity.Supplier_ImportSource == 1)
                    //{
                    //    jsonBuilder.Append("高铁网");
                    //}
                    //else
                    //{
                    jsonBuilder.Append("平台");
                    //}
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                if (account == 1)
                {
                    jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"补录\\\" align=\\\"absmiddle\\\"> <a href=\\\"SupplierZhongXinAccount_processing.aspx?Supplier_Type=" + entity.Supplier_Type + "&Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"补录\\\">补录</a>");
                }
                else
                {
                    if (Public.CheckPrivilege("d38ea434-9b11-4668-9fb9-2827a9d22602") && entity.Supplier_IsHaveShop == 0)
                    {
                        //新加只有卖家可以开通店铺（2015-3-5）
                        if (entity.Supplier_Type == 0)
                        {
                            jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"开通店铺\\\" align=\\\"absmiddle\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmoperate('Supplier_do.aspx?action=shopadd&Supplier_id=" + entity.Supplier_ID + "')\\\" title=\\\"开通店铺\\\">开通店铺</a> ");
                        }
                    }
                    if (Public.CheckPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"))
                    {
                        jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_View.aspx?Supplier_Type=" + entity.Supplier_Type + "&Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"查看\\\">查看</a>");
                    }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public SupplierBankInfo GetSupplierBankInfoBySupplierID(int ID)
    {
        return Mybank.GetSupplierBankBySupplierID(ID);
    }

    //供应商选择
    public string Product_Supplier_Select(int Supplier_ID, string Select_Name)
    {
        string select_str = "";
        select_str += "<select name=\"" + Select_Name + "\">";
        if (Supplier_ID == -1)
        {
            select_str += "<option value=\"0\" selected>系统</option>";
        }
        else
        {
            select_str += "<option value=\"0\">系统</option>";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                if (entity.Supplier_ID == Supplier_ID)
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\" selected>" + entity.Supplier_CompanyName + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\">" + entity.Supplier_CompanyName + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }


    /// <summary>
    /// 检查手机号是否使用
    /// </summary>
    /// <param name="LoginMobile"></param>
    /// <returns></returns>
    private bool CheckSupplierCompanyName(string company_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_CompanyName", "=", company_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        PageInfo page = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        if (page != null)
        {
            if (page.RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public virtual void EditSupplier()
    {
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        int Supplier_Margin_ID = tools.CheckInt(Request["Supplier_Margin_ID"]);
        int Supplier_Mode = tools.CheckInt(Request.Form["Supplier_Mode"]);

        int Supplier_Status = tools.CheckInt(Request.Form["Supplier_Status"]);
        int Supplier_Type = tools.CheckInt(Request.Form["Supplier_Type"]);
        int Supplier_FavorMonth = tools.CheckInt(Request.Form["Supplier_FavorMonth"]);
        int Supplier_AllowOrderEmail = tools.CheckInt(Request.Form["Supplier_AllowOrderEmail"]);

        double Supplier_CreditLimit = tools.CheckFloat(Request.Form["Supplier_CreditLimit"]);
        double Supplier_TempCreditLimit = tools.CheckFloat(Request.Form["Supplier_TempCreditLimit"]);
        string Supplier_TempCreditLimit_ContractSN = tools.CheckStr(Request.Form["Supplier_TempCreditLimit_ContractSN"]);
        int Supplier_CreditLimit_Expires = tools.CheckInt(Request.Form["Supplier_CreditLimit_Expires"]);
        int Supplier_TempCreditLimit_Expires = tools.CheckInt(Request.Form["Supplier_TempCreditLimit_Expires"]);

        string Supplier_CompanyName = tools.CheckStr(Request.Form["Supplier_CompanyName"]);
        string Supplier_County = tools.CheckStr(Request.Form["Supplier_County"]);
        string Supplier_City = tools.CheckStr(Request.Form["Supplier_City"]);
        string Supplier_State = tools.CheckStr(Request.Form["Supplier_State"]);

        string Supplier_Country = tools.CheckStr(Request.Form["Supplier_Country"]);
        //详细地址  Supplier_Address
        string Supplier_Address = tools.CheckStr(Request.Form["Supplier_Address"]);
        //电话    Supplier_Phone   
        string Supplier_Phone = tools.CheckStr(Request.Form["Supplier_Phone"]);
        //传真    Supplier_Fax
        string Supplier_Fax = tools.CheckStr(Request.Form["Supplier_Fax"]);
        string Supplier_Zip = tools.CheckStr(Request.Form["Supplier_Zip"]);
        //联系人   Supplier_Contactman 
        string Supplier_Contactman = tools.CheckStr(Request.Form["Supplier_Contactman"]);
        //真实姓名   Supplier_Contactman 
        string Member_RealName = tools.CheckStr(Request.Form["Member_RealName"]);
        //手机    Supplier_Mobile
        string Supplier_Mobile = tools.CheckStr(Request.Form["Supplier_Mobile"]);
        int Supplier_IsApply = tools.CheckInt(Request.Form["Supplier_IsApply"]);
        //订阅接收手机    Supplier_SysMobile
        string Supplier_SysMobile = tools.CheckStr(Request.Form["Supplier_SysMobile"]);
        //订阅接收邮箱    Supplier_SysEmail
        string Supplier_SysEmail = tools.CheckStr(Request.Form["Supplier_SysEmail"]);
        //公章图片  Supplier_SealImg
        string Supplier_SealImg = tools.CheckStr(Request.Form["Supplier_SealImg"]);
        //法定代表人姓名   Supplier_Corporate
        string Supplier_Corporate = tools.CheckStr(Request.Form["Supplier_Corporate"]);
        //法定代表人电话   Supplier_CorporateMobile
        string Supplier_CorporateMobile = tools.CheckStr(Request.Form["Supplier_CorporateMobile"]);
        //注册资金  Supplier_RegisterFunds
        double Supplier_RegisterFunds = tools.CheckFloat(Request.Form["Supplier_RegisterFunds"]);

        //营业执照副本号   Supplier_BusinessCode
        string Supplier_BusinessCode = tools.CheckStr(Request.Form["Supplier_BusinessCode"]);

        //组织机构代码证副本号    Supplier_OrganizationCode
        string Supplier_OrganizationCode = tools.CheckStr(Request.Form["Supplier_OrganizationCode"]);
        //税务登记证副本号  Supplier_TaxationCode
        string Supplier_TaxationCode = tools.CheckStr(Request.Form["Supplier_TaxationCode"]);

        //银行开户许可证号  Supplier_BankAccountCode
        //string Supplier_BankAccountCode = tools.CheckStr(Request.Form["Supplier_BankAccountCode"]);
        //是否授权品牌    Supplier_IsAuthorize
        int Supplier_IsAuthorize = tools.CheckInt(Request.Form["Supplier_IsAuthorize"]);
        //是否注册品牌或商标 Supplier_IsTrademark
        int Supplier_IsTrademark = tools.CheckInt(Request.Form["Supplier_IsTrademark"]);
        //客服电话  Supplier_ServicesPhone  商家有
        string Supplier_ServicesPhone = tools.CheckStr(Request.Form["Supplier_ServicesPhone"]);
        //经营年限  Supplier_OperateYear    商家有
        int Supplier_OperateYear = tools.CheckInt(Request.Form["Supplier_OperateYear"]);
        //联系人邮箱 Supplier_ContactEmail  商家有
        string Supplier_ContactEmail = tools.CheckStr(Request.Form["Supplier_ContactEmail"]);
        //联系人QQ   Supplier_ContactQQ
        string Supplier_ContactQQ = tools.CheckStr(Request.Form["Supplier_ContactQQ"]);
        //代理费用比率    Supplier_AgentRate
        double Supplier_AgentRate = tools.CheckFloat(Request.Form["Supplier_AgentRate"]);
        string Supplier_Category = tools.CheckStr(Request.Form["Supplier_Category"]);
        int Supplier_SaleType = tools.CheckInt(Request.Form["Supplier_SaleType"]);
        string Member_Zip = tools.CheckStr(Request.Form["Member_Zip"]);
        //统一社会代码证号
        string Member_UniformSocial_Number = tools.CheckStr(Request.Form["Member_UniformSocial_Number"]);
        string Member_Company = tools.CheckStr(Request.Form["Member_Company"]);
        string Member_Company_Contact = tools.CheckStr(Request.Form["Member_Company_Contact"]);

        string Member_Company_Introduce = tools.CheckStr(Request.Form["Member_Company_Introduce"]);
        //获取商家QQ
        string Supplier_Online_Code = tools.CheckStr(Request.Form["Supplier_Online_Code"]);
        //运费模式  Supplier_DeliveryMode
        int Supplier_DeliveryMode = tools.CheckInt(Request.Form["Supplier_DeliveryMode"]);

        if (Supplier_AgentRate < 0)
        {
            Public.Msg("info", "信息提示", "代理费用比率不可小于0", false, "{back}");
        }
        if (Supplier_CreditLimit < 0)
        {
            Public.Msg("info", "信息提示", "固定信用额度不可小于0", false, "{back}");
        }
        if (Supplier_TempCreditLimit < 0)
        {
            Public.Msg("info", "信息提示", "临时信用额度不可小于0", false, "{back}");
        }
        if (Supplier_TempCreditLimit_Expires < 0)
        {
            Public.Msg("info", "信息提示", "临时信用补充周期不可小于0", false, "{back}");
        }
        if (Supplier_CreditLimit_Expires < 0)
        {
            Public.Msg("info", "信息提示", "固定信用补充周期不可小于0", false, "{back}");
        }
        if (Member_Company.Length < 0)
        {

            Public.Msg("info", "信息提示", "公司名称不能为空", false, "{back}");
            if (CheckSupplierCompanyName(Member_Company))
            {
                Public.Msg("info", "信息提示", "公司名称已经存在", false, "{back}");

            }


        }
        SupplierInfo entity = GetSupplierByID(Supplier_ID);

        SupplierOnlineInfo SupOnlineEntity = new SupplierOnlineInfo();


        if (entity != null)
        {
            entity.Supplier_Mode = Supplier_Mode;
            entity.Supplier_DeliveryMode = Supplier_DeliveryMode;
            entity.Supplier_Status = Supplier_Status;
            entity.Supplier_Type = Supplier_Type;
            entity.Supplier_FavorMonth = Supplier_FavorMonth;
            entity.Supplier_AllowOrderEmail = Supplier_AllowOrderEmail;
            entity.Supplier_AgentRate = Supplier_AgentRate;
            entity.Supplier_CreditLimit = Supplier_CreditLimit;
            entity.Supplier_TempCreditLimit = Supplier_TempCreditLimit;
            entity.Supplier_CreditLimit_Expires = Supplier_CreditLimit_Expires;
            entity.Supplier_TempCreditLimit_ContractSN = Supplier_TempCreditLimit_ContractSN;
            entity.Supplier_TempCreditLimit_Expires = Supplier_TempCreditLimit_Expires;
            entity.Supplier_County = Supplier_County;
            entity.Supplier_City = Supplier_City;
            entity.Supplier_State = Supplier_State;
            entity.Supplier_Country = Supplier_Country;
            entity.Supplier_Address = Supplier_Address;
            entity.Supplier_Phone = Supplier_Phone;
            entity.Supplier_Fax = Supplier_Fax;
            entity.Supplier_Zip = Supplier_Zip;
            entity.Supplier_Contactman = Supplier_Contactman;
            entity.Supplier_Mobile = Supplier_Mobile;
            entity.Supplier_IsApply = Supplier_IsApply;
            entity.Supplier_SysMobile = Supplier_SysMobile;
            entity.Supplier_SysEmail = Supplier_SysEmail;
            entity.Supplier_SealImg = Supplier_SealImg;
            entity.Supplier_Corporate = Supplier_Corporate;
            entity.Supplier_CorporateMobile = Supplier_CorporateMobile;
            entity.Supplier_RegisterFunds = Supplier_RegisterFunds;
            entity.Supplier_BusinessCode = Supplier_BusinessCode;
            entity.Supplier_OrganizationCode = Supplier_OrganizationCode;
            entity.Supplier_TaxationCode = Supplier_TaxationCode;
            //entity.Supplier_BankAccountCode = Supplier_BankAccountCode;
            entity.Supplier_IsAuthorize = Supplier_IsAuthorize;
            entity.Supplier_IsTrademark = Supplier_IsTrademark;
            entity.Supplier_ServicesPhone = Supplier_ServicesPhone;
            entity.Supplier_OperateYear = Supplier_OperateYear;
            entity.Supplier_ContactEmail = Supplier_ContactEmail;
            entity.Supplier_ContactQQ = Supplier_ContactQQ;
            entity.Supplier_Category = Supplier_Category;
            entity.Supplier_SaleType = Supplier_SaleType;
            entity.Supplier_CompanyName = Member_Company;

            if (MyBLL.EditSupplier(entity, Public.GetUserPrivilege()))
            {
                Member MyMember = new Member();
                ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
                int member_id = tools.NullInt(DBHelper.ExecuteScalar("select Member_ID from Member where Member_SupplierID=" + entity.Supplier_ID + ""));
                if (member_id > 0)
                {
                    MemberProfileInfo memberprofileinfo = MyMember.GetMemberProfileByID(member_id);
                    memberprofileinfo.Member_StreetAddress = Supplier_Address;
                    memberprofileinfo.Member_Name = Supplier_Contactman;
                    memberprofileinfo.Member_Phone_Number = Supplier_Phone;
                    memberprofileinfo.Member_Mobile = Supplier_Mobile;
                    memberprofileinfo.Member_QQ = Supplier_ContactQQ;
                    memberprofileinfo.Member_Corporate = Supplier_Corporate;
                    memberprofileinfo.Member_CorporateMobile = Supplier_CorporateMobile;
                    memberprofileinfo.Member_RegisterFunds = Supplier_RegisterFunds;
                    memberprofileinfo.Member_BusinessCode = Supplier_BusinessCode;
                    memberprofileinfo.Member_OrganizationCode = Supplier_OrganizationCode;
                    memberprofileinfo.Member_TaxationCode = Supplier_TaxationCode;
                    memberprofileinfo.Member_SealImg = Supplier_SealImg;
                    memberprofileinfo.Member_Zip = Member_Zip;
                    memberprofileinfo.Member_Fax = Supplier_Fax;
                    memberprofileinfo.Member_Company = Member_Company;
                    memberprofileinfo.Member_RealName = Member_RealName;
                    memberprofileinfo.Member_UniformSocial_Number = Member_UniformSocial_Number;
                    //bool IsRight=  MyMember.EditMemberProfile(memberprofileinfo, Public.GetUserPrivilege());
                    MemberInfo member = MyMember.GetMemberByID(member_id);
                    member.Member_Company_Contact = Member_Company_Contact;
                    member.Member_Company_Introduce = Member_Company_Introduce;




                    if (MyMem.EditMember(member, Public.GetUserPrivilege()))
                    {
                        //Public.Msg("error", "错误信息", "请输入正确的QQ号", false, "{back}");

                    }
                    else
                    {
                        Public.Msg("error", "错误信息", "请输入正确的商家信息", false, "{back}");
                    }

                    if (MyMem.EditMemberProfile(memberprofileinfo, Public.GetUserPrivilege()))
                    {
                        if (Supplier_Online_Code.ToString().Length > 0)
                        {
                            int count = Supplier_Online_Code.Length;
                            if (Check_QQ(Supplier_Online_Code) && (count < 11))
                            {
                                SupOnlineEntity.Supplier_Online_SupplierID = Supplier_ID;
                                SupOnlineEntity.Supplier_Online_Type = "QQ";
                                SupOnlineEntity.Supplier_Online_Name = entity.Supplier_CompanyName;

                                SupOnlineEntity.Supplier_Online_Code = Supplier_Online_Code;
                                SupOnlineEntity.Supplier_Online_IsActive = 1;
                                SupOnlineEntity.Supplier_Online_Addtime = DateTime.Now;
                                SupOnlineEntity.Supplier_Online_Site = "CN";

                                MySupplierOnline.AddSupplierOnline(SupOnlineEntity);
                            }
                            else
                            {
                                Public.Msg("error", "错误信息", "请输入正确的QQ号", false, "{back}");
                            }

                        }




                        Supplier_Cert_Save();
                        SaveSupplierRelateTag(Supplier_ID);
                        if (Supplier_DeliveryMode == 0)
                        {
                            MyBLL.DelSupplierDeliveryFee(Supplier_ID, 0);
                        }
                        if (entity.Supplier_AuditStatus == 0)
                        {
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx?");
                        }
                        else if (entity.Supplier_AuditStatus == 1 && entity.Supplier_Status < 2 && entity.Supplier_Trash == 0)
                        {
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx?Audit=1");
                        }
                        else if (entity.Supplier_AuditStatus == 2 && entity.Supplier_Status < 2 && entity.Supplier_Trash == 0)
                        {
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx?Audit=2");
                        }
                        else if (entity.Supplier_Status == 2)
                        {
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx?status=2");
                        }
                        else if (entity.Supplier_Trash == 1)
                        {
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx?trash=1");
                        }

                    }

                }



            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }


    //检查QQ号码
    public bool Check_QQ(string check_str)
    {
        bool result = true;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\d{5,10}$");
        result = regex.IsMatch(check_str);

        return result;
    }

    /// <summary>
    /// 批量修改供应商订单邮件提醒状态
    /// </summary>
    /// <param name="Status">订单邮件提醒状态</param>
    public virtual void SupplierAllowOrdersEmail(int Status)
    {
        string supplier_id = tools.CheckStr(Request["supplier_id"]);
        if (supplier_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(supplier_id, 1) == ",") { supplier_id = supplier_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", supplier_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Asc"));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                entity.Supplier_AllowOrderEmail = Status;
                MyBLL.EditSupplier(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_list.aspx?Audit=1");

    }

    /// <summary>
    /// 批量审核供应商信息
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierAudit(int Status)
    {
        string supplier_id = tools.CheckStr(Request["supplier_id"]);
        if (supplier_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        //if (tools.Left(supplier_id, 1) == ",") { supplier_id = supplier_id.Remove(0, 1); }
        if (tools.Left(supplier_id, 1) == ",") { supplier_id = supplier_id.Remove(1, 1); } //2017-05-25 11:02:10 修改
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", supplier_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Asc"));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
            foreach (SupplierInfo entity in entitys)
            {
                if ((Status == 2 && entity.Supplier_AuditStatus == 0) || Status == 1)
                {
                    MemberInfo memberinfo = null;
                    int Member_ID = tools.NullInt(DBHelper.ExecuteScalar("select Member_ID from Member where Member_SupplierID=" + entity.Supplier_ID + ""));
                    if (Member_ID > 0)
                    {
                        memberinfo = MyMem.GetMemberByID(Member_ID, Public.GetUserPrivilege());
                    }


                    entity.Supplier_AuditStatus = Status;
                    entity.Supplier_Trash = 0;
                    entity.Supplier_Cert_Status = 2;
                    if (MyBLL.EditSupplier(entity, Public.GetUserPrivilege()))
                    {
                        if (memberinfo != null)
                        {
                            memberinfo.Member_AuditStatus = 1;
                            MyMem.EditMember(memberinfo, Public.GetUserPrivilege());
                        }


                        if (Status == 1)
                        {

                            //开通店铺
                            SupplierShopAdd(entity.Supplier_ID);
                            //发送短信
                            SMS mySMS = new SMS();
                            mySMS.Send(entity.Supplier_Mobile, entity.Supplier_CompanyName, "meber_to_supplier");
                            //new SMS().Send(entity.Supplier_Mobile, "user_audit", null);
                            Glaer.Trade.B2C.Model.ZhongXinInfo entityZhongXin = new ZhongXinInfo();
                            entityZhongXin.CompanyName = entity.Supplier_CompanyName;
                            entityZhongXin.SupplierID = entity.Supplier_ID;
                            //entityZhongXin.OpenAccountName = entity.Supplier_CompanyName;
                            entityZhongXin.Addtime = DateTime.Now;
                            if (MyZhongxin.AddZhongXin(entityZhongXin))
                            {
                                //ZhongXin myZhongXin = new ZhongXin();
                                //myZhongXin.AccountSign(entity.Supplier_ID);
                                //推送短信
                                //mySMS.Send(entity.Supplier_Mobile, entity.Supplier_CompanyName, "zhongxin_info");
                                Public.Msg("positive", "操作成功",  "操作成功", true,"{back}");
                            }
                            else
                            {
                                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                            }
                        }
                    }
                }
            }
        }
        Response.Redirect("Supplier_list.aspx?Audit=0");
    }

    public virtual void Supplier_Contract_Settings()
    {
        int supplier_id = tools.CheckInt(Request["Supplier_ID"]);
        int contract_templateid = tools.CheckInt(Request["Contract_TemplateID"]);


        SupplierInfo entity = GetSupplierByID(supplier_id);
        if (entity != null)
        {
            entity.Supplier_ContractID = contract_templateid;
            if (MyBLL.EditSupplier(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "设置默认合同模板成功", true, "/supplier/supplier_list.aspx");
            }
        }
    }

    /// <summary>
    /// 批量审核供应商信息
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierRemove(int Status)
    {
        string supplier_id = tools.CheckStr(Request["supplier_id"]);
        if (supplier_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(supplier_id, 1) == ",") { supplier_id = supplier_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", supplier_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Asc"));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                if (Status == 1)
                {
                    entity.Supplier_Trash = 1;
                    MyBLL.EditSupplier(entity, Public.GetUserPrivilege());
                }
                if (Status == 2)
                {
                    MyBLL.DelSupplier(entity.Supplier_ID, Public.GetUserPrivilege());
                }

            }
        }
        if (Status == 1)
        {
            Response.Redirect("Supplier_list.aspx?Trash=1&Audit=2");
        }
        if (Status == 2)
        {
            Response.Redirect("Supplier_list.aspx?Trash=1&Audit=2");
        }

    }

    public virtual void SaveSupplierRelateTag(int Supplier_ID)
    {
        string Supplier_Tag = tools.CheckStr(Request["Tag_ID"]);
        string[] tagArray = Supplier_Tag.Split(',');
        MyTag.DelSupplierRelateTagBySupplierID(Supplier_ID);
        SupplierRelateTagInfo entity;
        foreach (string subtag in tagArray)
        {
            if (tools.CheckInt(subtag) > 0)
            {
                entity = new SupplierRelateTagInfo();
                entity.Supplier_RelateTag_TagID = tools.CheckInt(subtag);
                entity.Supplier_RelateTag_SupplierID = Supplier_ID;
                MyTag.AddSupplierRelateTag(entity);
                entity = null;
            }
        }
    }

    public virtual string Select_Supplier(string Select_Name, int Default_ID)
    {
        string select_str = "";
        select_str += "<select name=\"" + Select_Name + "\">";
        select_str += "<option value=\"0\" selected>选择供应商</option>";
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                if (Default_ID == entity.Supplier_ID)
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\" selected>" + entity.Supplier_CompanyName + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\">" + entity.Supplier_CompanyName + "</option>";
                }

            }
        }
        select_str += "</select>";
        return select_str;
    }

    public virtual void Supplier_Product_Exchange()
    {
        int Supplier_ID, Target_Supplier;
        Supplier_ID = tools.CheckInt(Request["Supplier_ID"]);
        Target_Supplier = tools.CheckInt(Request["Target_Supplier"]);

        if (Supplier_ID == 0)
        {
            Public.Msg("error", "错误信息", "请选择源供应商", false, "{back}");
            Response.End();
        }

        if (Target_Supplier == 0)
        {
            Public.Msg("error", "错误信息", "请选择目标供应商", false, "{back}");
            Response.End();
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", Supplier_ID.ToString()));
        IList<ProductInfo> entitys = MyProduct.GetProducts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                entity.Product_SupplierID = Target_Supplier;
                entity.Product_IsAudit = 0;
                entity.Product_IsInsale = 0;
                MyProduct.EditProductInfo(entity, Public.GetUserPrivilege());
            }
        }
        Public.Msg("positive", "操作成功", "操作完成", true, "supplier_list.aspx");

    }

    /// <summary>
    /// 审核供应商信息
    /// </summary>
    /// <param name="status"></param>
    public virtual void ChangeSupplierAudit(int status)
    {
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        SupplierInfo entity = GetSupplierByID(Supplier_ID);
        if (entity != null)
        {
            if ((status == 2 && entity.Supplier_AuditStatus == 0) || status == 1)
            {
                entity.Supplier_Cert_Status = 2;
                entity.Supplier_AuditStatus = status;
                entity.Supplier_Trash = 0;
                if (MyBLL.EditSupplier(entity, Public.GetUserPrivilege()))
                {
                    //开通店铺
                    if (status == 1)
                    {

                        //开通店铺
                        SupplierShopAdd(entity.Supplier_ID);
                        //发送短信
                        SMS mySMS = new SMS();
                        mySMS.Send(entity.Supplier_Mobile, entity.Supplier_CompanyName, "meber_to_supplier");
                        //new SMS().Send(entity.Supplier_Mobile, "user_audit", null);
                        Glaer.Trade.B2C.Model.ZhongXinInfo entityZhongXin = new ZhongXinInfo();
                        entityZhongXin.CompanyName = entity.Supplier_CompanyName;
                        entityZhongXin.SupplierID = entity.Supplier_ID;
                        //entityZhongXin.OpenAccountName = entity.Supplier_CompanyName;
                        entityZhongXin.Addtime = DateTime.Now;
                        if (MyZhongxin.AddZhongXin(entityZhongXin))
                        {
                            //ZhongXin myZhongXin = new ZhongXin();
                            //myZhongXin.AccountSign(entity.Supplier_ID);
                            //推送短信
                            //mySMS.Send(entity.Supplier_Mobile, entity.Supplier_CompanyName, "zhongxin_info");
                            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_list.aspx");
                        }
                        else
                        {
                            

                        }

                    }
                    else
                    {
                        Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                    }

                    //if (MyZhongxin.AddZhongXin(entityZhongXin))
                    //{
                    //    ZhongXin myZhongXin = new ZhongXin();
                    //    myZhongXin.AccountSign(entity.Supplier_ID,entity.Supplier_Mobile);

                    //    Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_View.aspx?Supplier_ID=" + Supplier_ID);
                    //}
                    //else
                    //{
                    //    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
    
                    //}
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void ChangeSupplierCertAudit(int status)
    {
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        SupplierInfo entity = GetSupplierByID(Supplier_ID);
        if (entity != null)
        {
            entity.Supplier_Cert_Status = status;
            if (MyBLL.EditSupplier(entity, Public.GetUserPrivilege()))
            {
                if (entity.SupplierRelateCertInfos != null)
                {
                    foreach (SupplierRelateCertInfo certinfo in entity.SupplierRelateCertInfos)
                    {
                        if (status == 2)
                        {
                            certinfo.Cert_Img = certinfo.Cert_Img1;
                        }
                        certinfo.Cert_Img1 = "";
                        MyBLL.EditSupplierRelateCert(certinfo);
                    }
                }

                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Cert_View.aspx?Supplier_ID=" + Supplier_ID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }


    public string GetSupplierCerts()
    {
        string Supplier_CompanyName = "--";
        string Supplier_Contactman = "--";
        string Supplier_Region = "--";
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Cert_Status", "=", (Audit + 1).ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierInfo entity in entitys)
            {
                MemberInfo memberinfo = new Member().GetMemberByEmail(entity.Supplier_Email);

                if (memberinfo != null)
                {
                    MemberProfileInfo MemberProfileInfo = new Member().GetMemberProfileByID(memberinfo.Member_ID);
                    if (MemberProfileInfo != null)
                    {
                        Supplier_CompanyName = MemberProfileInfo.Member_Company;
                        Supplier_Contactman = MemberProfileInfo.Member_Name;
                        Supplier_Region = Public.JsonStr(addr.DisplayAddress(MemberProfileInfo.Member_State, MemberProfileInfo.Member_City, MemberProfileInfo.Member_County));
                    }
                }

                jsonBuilder.Append("{\"SupplierInfo.Supplier_ID\":" + entity.Supplier_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Contactman));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"" + Get_Cert_Type(entity.Supplier_CertType));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"" + Get_Cert_Status(entity.Supplier_Cert_Status));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("b4fd0773-e353-43ab-aa6b-3096bc0981f3"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Cert_View.aspx?Supplier_id=" + entity.Supplier_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string Get_Supplier_Cert(int Supplier_CertID, IList<SupplierRelateCertInfo> RelateCates)
    {
        string Cert_Img = "";
        if (RelateCates != null)
        {
            foreach (SupplierRelateCertInfo entity in RelateCates)
            {
                if (Supplier_CertID == entity.Cert_CertID)
                {
                    Cert_Img = entity.Cert_Img;
                    break;
                }
            }
        }
        return Cert_Img;
    }

    public string Get_Supplier_Certtmp(int Supplier_CertID, IList<SupplierRelateCertInfo> RelateCates)
    {
        string Cert_Img = "";
        if (RelateCates != null)
        {
            foreach (SupplierRelateCertInfo entity in RelateCates)
            {
                if (Supplier_CertID == entity.Cert_CertID)
                {
                    Cert_Img = entity.Cert_Img1;
                    break;
                }
            }
        }
        return Cert_Img;
    }

    public string Get_Cert_Type(int CertType)
    {
        string Cert_Type = "";
        switch (CertType)
        {
            case 0:
                Cert_Type = "普通供应商";
                break;
            case 1:
                Cert_Type = "下岗创业";
                break;
            case 2:
                Cert_Type = "大学生创业";
                break;
            case 3:
                Cert_Type = "自主创业";
                break;
        }
        return Cert_Type;
    }

    public string Get_Cert_Status(int Supplier_Cert_Status)
    {
        string status = "未上传";

        if (Supplier_Cert_Status == 0)
        {
            status = "未上传";
        }
        if (Supplier_Cert_Status == 1)
        {
            status = "待审核";
        }
        if (Supplier_Cert_Status == 2)
        {
            status = "审核通过";
        }
        if (Supplier_Cert_Status == 3)
        {
            status = "审核未通过";
        }
        return status;
    }

    //供应商选择
    public string SelectSupplier(string keyword)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierInfo.Supplier_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
        }

        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("<form method=\"post\"  id=\"frmadd\" name=\"frmadd\">");
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
            jsonBuilder.Append("    <tr class=\"list_head_bg\">");
            jsonBuilder.Append("        <td width=\"30\"></td>");
            jsonBuilder.Append("        <td width=\"80\">ID</td>");
            jsonBuilder.Append("        <td>用户名</td>");
            jsonBuilder.Append("        <td>注册邮箱</td>");
            jsonBuilder.Append("        <td>公司名称</td>");
            jsonBuilder.Append("        <td>注册时间</td>");
            jsonBuilder.Append("    </tr>");
            foreach (SupplierInfo entity in entitys)
            {
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"Supplier_Message_SupplierID\" name=\"Supplier_Message_SupplierID\" value=\"" + entity.Supplier_ID + "\" /></td>");
                jsonBuilder.Append("        <td>" + entity.Supplier_ID + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_Nickname) + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_Email) + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_CompanyName) + "</td>");
                jsonBuilder.Append("        <td>" + entity.Supplier_Addtime + "</td>");
                jsonBuilder.Append("    </tr>");
            }

            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
            jsonBuilder.Append("        <td colspan=\"8\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:supplier_add('Supplier_Message_SupplierID');\" /></td>");
            jsonBuilder.Append("    </tr>");
            jsonBuilder.Append("</table>");
            jsonBuilder.Append("</form>");

            entitys = null;
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    //展示选择供应商
    public string ShowSupplier()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"><input type=\"button\" value=\"添加\" onclick=\"SelectSupplier()\" class=\"bt_orange\"></td>");
        jsonBuilder.Append("        <td>ID</td>");

        jsonBuilder.Append("        <td>用户名</td>");
        jsonBuilder.Append("        <td>注册邮箱</td>");
        jsonBuilder.Append("        <td>公司名称</td>");
        jsonBuilder.Append("        <td>注册时间</td>");

        jsonBuilder.Append("    </tr>");

        string IDstr = "0";
        IList<SupplierInfo> entityList = (IList<SupplierInfo>)Session["MessageSupplierInfo"];
        //SupplierInfo supplierEntity = null;
        if (entityList != null)
        {
            foreach (SupplierInfo entity in entityList)
            {
                IDstr = IDstr + "," + entity.Supplier_ID;
            }
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", IDstr.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Asc"));
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"hidden\" name=\"Supplier_Message_SupplierID\" value=\"" + entity.Supplier_ID + "\"><a href=\"javascript:supplier_del(" + entity.Supplier_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                jsonBuilder.Append("        <td>" + entity.Supplier_ID + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_Nickname) + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_Email) + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Supplier_CompanyName) + "</td>");
                jsonBuilder.Append("        <td>" + entity.Supplier_Addtime + "</td>");
                jsonBuilder.Append("    </tr>");
            }

        }
        jsonBuilder.Append("</table>");
        entityList = null;

        return jsonBuilder.ToString();
    }

    public string Get_Apply_Status(int Supplier_Apply_Status)
    {
        string status = "待审核";

        if (Supplier_Apply_Status == 0)
        {
            status = "待审核";
        }
        if (Supplier_Apply_Status == 1)
        {
            status = "审核通过";
        }
        if (Supplier_Apply_Status == 2)
        {
            status = "审核不通过";
        }

        return status;
    }

    public string GetSupplierPayBackApplys()
    {

        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPayBackApplyInfo.Supplier_PayBack_Apply_SupplierID", "in", "select supplier_id from supplier where Supplier_CompanyName like '%" + keyword + "%'"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Status", "=", (Audit).ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyPayBackApply.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierPayBackApplyInfo> entitys = MyPayBackApply.GetSupplierPayBackApplys(Query, Public.GetUserPrivilege());
        string payback_type = "";
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierPayBackApplyInfo entity in entitys)
            {

                jsonBuilder.Append("{\"SupplierPayBackApplyInfo.Supplier_PayBack_Apply_ID\":" + entity.Supplier_PayBack_Apply_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_PayBack_Apply_ID);
                jsonBuilder.Append("\",");

                SupplierInfo supplierinfo = GetSupplierByID(entity.Supplier_PayBack_Apply_SupplierID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr("--"));
                    jsonBuilder.Append("\",");
                }

                if (entity.Supplier_PayBack_Apply_Type == 1)
                {
                    payback_type = "会员费";
                }
                else if (entity.Supplier_PayBack_Apply_Type == 2)
                {
                    payback_type = "保证金";
                }
                else
                {
                    payback_type = "推广费";
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(payback_type);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Supplier_PayBack_Apply_Amount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_PayBack_Apply_Note));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"" + Get_Apply_Status(entity.Supplier_PayBack_Apply_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"" + entity.Supplier_PayBack_Apply_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_payback_apply_View.aspx?apply_id=" + entity.Supplier_PayBack_Apply_ID + "\\\" title=\\\"查看\\\">查看</a>");
                if (Public.CheckPrivilege("70939c0f-4e76-4f4a-9d6c-cff9e11e27ec") && entity.Supplier_PayBack_Apply_Status == 2)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('supplier_payback_apply_Do.aspx?action=move&Supplier_PayBack_Apply_ID=" + entity.Supplier_PayBack_Apply_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// 获取指定账户退款信息
    /// </summary>
    /// <param name="Apply_ID">账户退款申请ID</param>
    /// <returns>账户退款信息</returns>
    public virtual SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int Apply_ID)
    {

        return MyPayBackApply.GetSupplierPayBackApplyByID(Apply_ID, Public.GetUserPrivilege());
    }

    //修改退款申请
    public virtual void EditSupplierPayBackApply(int Status)
    {

        int Supplier_PayBack_Apply_ID = tools.CheckInt(Request.Form["Supplier_PayBack_Apply_ID"]);
        int Supplier_PayBack_Apply_Status = Status;
        double Supplier_PayBack_Apply_AdminAmount = tools.CheckFloat(Request.Form["Supplier_PayBack_Apply_AdminAmount"]);
        string Supplier_PayBack_Apply_AdminNote = tools.CheckStr(Request.Form["Supplier_PayBack_Apply_AdminNote"]);

        SupplierPayBackApplyInfo entity = GetSupplierPayBackApplyByID(Supplier_PayBack_Apply_ID);
        if (entity != null)
        {
            entity.Supplier_PayBack_Apply_Status = Supplier_PayBack_Apply_Status;
            entity.Supplier_PayBack_Apply_AdminAmount = Supplier_PayBack_Apply_AdminAmount;
            entity.Supplier_PayBack_Apply_AdminNote = Supplier_PayBack_Apply_AdminNote;
            entity.Supplier_PayBack_Apply_AdminTime = DateTime.Now;


            if (MyPayBackApply.EditSupplierPayBackApply(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_PayBack_Apply.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除退款申请
    public virtual void DelSupplierPayBackApply()
    {
        int Supplier_PayBack_Apply_ID = tools.CheckInt(Request.QueryString["Supplier_PayBack_Apply_ID"]);

        if (MyPayBackApply.DelSupplierPayBackApply(Supplier_PayBack_Apply_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_PayBack_Apply.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //获取店铺关闭申请
    public string GetSupplierCloseShopApplys()
    {

        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCloseShopApplyInfo.CloseShop_Apply_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCloseShopApplyInfo.CloseShop_Apply_SupplierID", "in", "select supplier_id from supplier where Supplier_CompanyName like '%" + keyword + "%'"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCloseShopApplyInfo.CloseShop_Apply_Status", "=", (Audit).ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCloseShopApply.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierCloseShopApplyInfo> entitys = MyCloseShopApply.GetSupplierCloseShopApplys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierCloseShopApplyInfo entity in entitys)
            {

                jsonBuilder.Append("{\"SupplierCloseShopApplyInfo.CloseShop_Apply_ID\":" + entity.CloseShop_Apply_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.CloseShop_Apply_ID);
                jsonBuilder.Append("\",");

                SupplierInfo supplierinfo = GetSupplierByID(entity.CloseShop_Apply_SupplierID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr("--"));
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.CloseShop_Apply_Note));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"" + Get_Apply_Status(entity.CloseShop_Apply_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"" + entity.CloseShop_Apply_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"supplier_closeshop_apply_view.aspx?apply_id=" + entity.CloseShop_Apply_ID + "\\\" title=\\\"查看\\\">查看</a>");
                if (Public.CheckPrivilege("bd8d861d-dca1-4e52-84a9-013c68e3134d") && entity.CloseShop_Apply_Status > 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('supplier_closeshop_apply_Do.aspx?action=move&CloseShop_Apply_ID=" + entity.CloseShop_Apply_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    //获取店铺关闭申请
    public virtual SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID(int Apply_ID)
    {
        return MyCloseShopApply.GetSupplierCloseShopApplyByID(Apply_ID, Public.GetUserPrivilege());
    }

    //修改店铺关闭申请
    public virtual void EditSupplierCloseShopApply(int Status)
    {

        int CloseShop_Apply_ID = tools.CheckInt(Request.Form["CloseShop_Apply_ID"]);
        int CloseShop_Apply_Status = Status;
        string CloseShop_Apply_AdminNote = tools.CheckStr(Request.Form["CloseShop_Apply_AdminNote"]);

        SupplierCloseShopApplyInfo entity = GetSupplierCloseShopApplyByID(CloseShop_Apply_ID);
        if (entity != null)
        {
            entity.CloseShop_Apply_Status = CloseShop_Apply_Status;
            entity.CloseShop_Apply_AdminNote = CloseShop_Apply_AdminNote;
            entity.CloseShop_Apply_AdminTime = DateTime.Now;


            if (MyCloseShopApply.EditSupplierCloseShopApply(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "supplier_closeshop_apply.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除店铺关闭申请
    public virtual void DelSupplierCloseShopApply()
    {
        int CloseShop_Apply_ID = tools.CheckInt(Request.QueryString["CloseShop_Apply_ID"]);

        if (MyCloseShopApply.DelSupplierCloseShopApply(CloseShop_Apply_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "supplier_closeshop_apply.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //开通店铺
    public virtual void SupplierShopAdd()
    {
        int Supplier_ID = tools.CheckInt(Request["Supplier_ID"]);

        SupplierInfo supplierinfo = GetSupplierByID(Supplier_ID);
        if (supplierinfo != null)
        {
            SupplierShopInfo suppliershopinfo = new SupplierShopInfo();
            suppliershopinfo.Shop_Name = supplierinfo.Supplier_CompanyName;
            suppliershopinfo.Shop_Type = 1;
            suppliershopinfo.Shop_SupplierID = Supplier_ID;
            suppliershopinfo.Shop_Addtime = DateTime.Now;
            suppliershopinfo.Shop_Recommend = 0;
            suppliershopinfo.Shop_Evaluate = 0;
            suppliershopinfo.Shop_Code = "";
            suppliershopinfo.Shop_Banner = 1;
            suppliershopinfo.Shop_Css = 1;
            suppliershopinfo.Shop_Status = 1;
            suppliershopinfo.Shop_Banner_IsActive = 1;
            suppliershopinfo.Shop_Top_IsActive = 1;
            suppliershopinfo.Shop_Info_IsActive = 1;
            suppliershopinfo.Shop_LeftSearch_IsActive = 1;
            suppliershopinfo.Shop_LeftCate_IsActive = 1;
            suppliershopinfo.Shop_LeftSale_IsActive = 1;
            suppliershopinfo.Shop_Left_IsActive = 1;
            suppliershopinfo.Shop_Right_IsActive = 1;
            suppliershopinfo.Shop_RightProduct_IsActive = 1;

            suppliershopinfo.Shop_Domain = Get_Shop_Domain();
            suppliershopinfo.Shop_Site = Public.GetCurrentSite();
            MyBLLShop.AddSupplierShop(suppliershopinfo);

            supplierinfo.Supplier_IsHaveShop = 1;

            supplierinfo.Supplier_IsApply = 0;
            MyBLL.EditSupplier(supplierinfo, Public.GetUserPrivilege());
            SupplierMessageInfo message = new SupplierMessageInfo();
            message.Supplier_Message_ID = 0;
            message.Supplier_Message_SupplierID = Supplier_ID;
            message.Supplier_Message_Title = "您已成功开通店铺";
            message.Supplier_Message_Content = "您已成功开通店铺，请尽快对您的店铺信息进行完善！";
            message.Supplier_Message_Addtime = DateTime.Now;
            message.Supplier_Message_IsRead = 0;
            message.Supplier_Message_Site = Public.GetCurrentSite();
            MyMessage.AddSupplierMessage(message, Public.GetUserPrivilege());
            SupplierShopInfo shopinfo = GetSupplierShopBySupplierID(Supplier_ID);
            if (shopinfo != null)
            {
                Public.Msg("positive", "操作成功", "店铺开通成功", true, "/shop/Shop_View.aspx?shop_id=" + shopinfo.Shop_ID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }

    }

    public virtual void SupplierShopAdd(int Supplier_ID)
    {
        SupplierInfo supplierinfo = GetSupplierByID(Supplier_ID);
        if (supplierinfo != null)
        {
            SupplierShopInfo suppliershopinfo = new SupplierShopInfo();
            suppliershopinfo.Shop_Name = supplierinfo.Supplier_CompanyName;
            suppliershopinfo.Shop_Type = 1;
            suppliershopinfo.Shop_SupplierID = Supplier_ID;
            suppliershopinfo.Shop_Addtime = DateTime.Now;
            suppliershopinfo.Shop_Recommend = 0;
            suppliershopinfo.Shop_Evaluate = 0;
            suppliershopinfo.Shop_Code = "";
            suppliershopinfo.Shop_Banner = 0;
            suppliershopinfo.Shop_Css = 1;

            suppliershopinfo.Shop_Banner_IsActive = 1;
            suppliershopinfo.Shop_Top_IsActive = 1;
            suppliershopinfo.Shop_Info_IsActive = 1;
            suppliershopinfo.Shop_LeftSearch_IsActive = 1;
            suppliershopinfo.Shop_LeftCate_IsActive = 1;
            suppliershopinfo.Shop_LeftSale_IsActive = 1;
            suppliershopinfo.Shop_Left_IsActive = 1;
            suppliershopinfo.Shop_Right_IsActive = 1;
            suppliershopinfo.Shop_RightProduct_IsActive = 1;
            suppliershopinfo.Shop_Status = 1;
            suppliershopinfo.Shop_TopNav_IsActive = 1;

            suppliershopinfo.Shop_Domain = Get_Shop_Domain();
            suppliershopinfo.Shop_Site = Public.GetCurrentSite();

            if (MyBLLShop.AddSupplierShop(suppliershopinfo))
            {
                SysMessageInfo message = new SysMessageInfo();
                message.Message_ID = 0;
                message.Message_Type = 0;
                message.Message_ReceiveID = Supplier_ID;
                message.Message_SendID = 0;
                message.Message_UserType = 2;
                message.Message_Content = "您已成功开通店铺，请尽快对您的店铺信息进行完善！";
                message.Message_Addtime = DateTime.Now;
                message.Message_Status = 0;
                message.Message_Site = Public.GetCurrentSite();
                MySysMessage.AddSysMessage(message);
            }

            supplierinfo.Supplier_IsHaveShop = 1;
            supplierinfo.Supplier_IsApply = 0;
            MyBLL.EditSupplier(supplierinfo, Public.GetUserPrivilege());
        }
    }

    #endregion

    #region 佣金分类
    public virtual void AddSupplierCommissionCategory()
    {
        int Supplier_Commission_Cate_ID = 0;
        string Supplier_Commission_Cate_Name = tools.CheckStr(Request.Form["Supplier_Commission_Cate_Name"]);
        double Supplier_Commission_Cate_Amount = tools.CheckFloat(Request.Form["Supplier_Commission_Cate_Amount"]);
        string Supplier_Commission_Cate_SupplierID = tools.CheckStr(Request.Form["Supplier_Message_SupplierID"]);
        string Supplier_Commission_Cate_Site = Public.GetCurrentSite();
        if (Supplier_Commission_Cate_Name == "" || Supplier_Commission_Cate_SupplierID == "")
        {
            Public.Msg("error", "错误信息", "请选择供应商并填写佣金分类名称", false, "{back}");
        }
        foreach (string ids in Supplier_Commission_Cate_SupplierID.Split(','))
        {
            if (tools.CheckInt(ids) > 0)
            {
                SupplierCommissionCategoryInfo entity = new SupplierCommissionCategoryInfo();
                entity.Supplier_Commission_Cate_ID = 0;
                entity.Supplier_Commission_Cate_SupplierID = tools.CheckInt(ids);
                entity.Supplier_Commission_Cate_Name = Supplier_Commission_Cate_Name;
                entity.Supplier_Commission_Cate_Amount = Supplier_Commission_Cate_Amount;
                entity.Supplier_Commission_Cate_Site = Supplier_Commission_Cate_Site;

                if (MyBLLCate.AddSupplierCommissionCategory(entity, Public.GetUserPrivilege()))
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Commission_Cate.aspx");
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            }
        }
    }

    public virtual void EditSupplierCommissionCategory()
    {
        int Supplier_Commission_Cate_ID = tools.CheckInt(Request.Form["Supplier_Commission_Cate_ID"]);
        string Supplier_Commission_Cate_Name = tools.CheckStr(Request.Form["Supplier_Commission_Cate_Name"]);
        double Supplier_Commission_Cate_Amount = tools.CheckFloat(Request.Form["Supplier_Commission_Cate_Amount"]);
        int Supplier_Commission_Cate_SupplierID = tools.CheckInt(Request.Form["Supplier_Commission_Cate_SupplierID"]);
        string Supplier_Commission_Cate_Site = Public.GetCurrentSite();

        if (Supplier_Commission_Cate_Name == "" || Supplier_Commission_Cate_SupplierID == 0)
        {
            Public.Msg("error", "错误信息", "请选择供应商并填写佣金分类名称", false, "{back}");
        }

        SupplierCommissionCategoryInfo entity = new SupplierCommissionCategoryInfo();
        entity.Supplier_Commission_Cate_ID = Supplier_Commission_Cate_ID;
        entity.Supplier_Commission_Cate_SupplierID = Supplier_Commission_Cate_SupplierID;
        entity.Supplier_Commission_Cate_Name = Supplier_Commission_Cate_Name;
        entity.Supplier_Commission_Cate_Amount = Supplier_Commission_Cate_Amount;
        entity.Supplier_Commission_Cate_Site = Public.GetCurrentSite();

        if (MyBLLCate.EditSupplierCommissionCategory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Commission_Cate.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }


    }

    public virtual void DelSupplierCommissionCategory()
    {
        int Supplier_Commission_Cate_ID = tools.CheckInt(Request.QueryString["Cate_ID"]);
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Supplier_CommissionCateID", "=", Supplier_Commission_Cate_ID.ToString()));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys == null)
        {
            if (MyBLLCate.DelSupplierCommissionCategory(Supplier_Commission_Cate_ID, Public.GetUserPrivilege()) > 0)
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Commission_Cate.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "该佣金分类正在被使用！", false, "{back}");
        }
    }

    public string GetSupplierCommissionCategory()
    {
        string suppliers;
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {

            suppliers = GetSuppliersByKeyword(keyword);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_SupplierID", "in", suppliers));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLLCate.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierCommissionCategoryInfo> entitys = MyBLLCate.GetSupplierCommissionCategorys(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo;
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierCommissionCategoryInfo entity in entitys)
            {
                supplierinfo = MyBLL.GetSupplierByID(entity.Supplier_Commission_Cate_SupplierID, Public.GetUserPrivilege());
                jsonBuilder.Append("{\"id\":" + entity.Supplier_Commission_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Commission_Cate_ID);
                jsonBuilder.Append("\",");

                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_Email));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Commission_Cate_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Commission_Cate_Amount + "%");
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("deaa9168-3ffc-42c3-bb94-829fbf7f2e22"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Commission_Cate_edit.aspx?cate_id=" + entity.Supplier_Commission_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("07d26693-d9d7-459b-a097-b6c5e763f8f7"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_Commission_Cate_do.aspx?action=move&cate_id=" + entity.Supplier_Commission_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public SupplierCommissionCategoryInfo GetSupplierCommissionCategoryByID(int ID)
    {
        return MyBLLCate.GetSupplierCommissionCategoryByID(ID, Public.GetUserPrivilege());
    }
    #endregion

    #region 供应商资质

    /// <summary>
    /// 资质类型选择
    /// </summary>
    /// <param name="selectVlaue"></param>
    /// <returns></returns>
    public string DisplaySupplierCertType(int selectVlaue)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertTypeInfo.Cert_Type_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCertTypeInfo.Cert_Type_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierCertTypeInfo.Cert_Type_Sort", "asc"));
        IList<SupplierCertTypeInfo> entitys = MyCertType.GetSupplierCertTypes(Query);

        strHTML.Append("<select name=\"Supplier_Cert_Type\" id=\"Supplier_Cert_Type\">");
        strHTML.Append("<option value='0'>----请选择----</option>");
        if (entitys != null)
        {
            foreach (SupplierCertTypeInfo entity in entitys)
            {
                if (entity.Cert_Type_ID == selectVlaue)
                {
                    strHTML.Append("<option value='" + entity.Cert_Type_ID + "' selected>" + entity.Cert_Type_Name + "</option>");
                }
                else
                {
                    strHTML.Append("<option value='" + entity.Cert_Type_ID + "'>" + entity.Cert_Type_Name + "</option>");
                }
            }
        }
        strHTML.Append("</select>");
        return strHTML.ToString();
    }

    public string ShowSupplierCertName(int ID)
    {
        string CertName = "";

        SupplierCertTypeInfo entity = MyCertType.GetSupplierCertTypeByID(ID);

        if (entity != null)
        {
            CertName = entity.Cert_Type_Name;
        }
        else
        {
            CertName = "--";
        }

        return CertName;
    }

    public virtual SupplierCertTypeInfo GetSupplierCertTypeByID(int ID)
    {
        return MyCertType.GetSupplierCertTypeByID(ID);
    }

    public virtual void AddSupplierCert()
    {
        int Supplier_Cert_Type = tools.CheckInt(Request.Form["Supplier_Cert_Type"]);
        string Supplier_Cert_Name = tools.CheckStr(Request.Form["Supplier_Cert_Name"]);
        string Supplier_Cert_Note = tools.CheckStr(Request.Form["Supplier_Cert_Note"]);
        int Supplier_Cert_Sort = tools.CheckInt(Request.Form["Supplier_Cert_Sort"]);
        DateTime Supplier_Cert_Addtime = DateTime.Now;
        string Supplier_Cert_Site = Public.GetCurrentSite();

        if (Supplier_Cert_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写证书名称！", false, "{back}");
        }

        SupplierCertInfo entity = new SupplierCertInfo();
        entity.Supplier_Cert_Type = Supplier_Cert_Type;
        entity.Supplier_Cert_Name = Supplier_Cert_Name;
        entity.Supplier_Cert_Note = Supplier_Cert_Note;
        entity.Supplier_Cert_Sort = Supplier_Cert_Sort;
        entity.Supplier_Cert_Addtime = Supplier_Cert_Addtime;
        entity.Supplier_Cert_Site = Supplier_Cert_Site;

        if (MyCert.AddSupplierCert(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Cert_Config_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSupplierCert()
    {

        int Supplier_Cert_ID = tools.CheckInt(Request.Form["Supplier_Cert_ID"]);
        int Supplier_Cert_Type = tools.CheckInt(Request.Form["Supplier_Cert_Type"]);
        string Supplier_Cert_Name = tools.CheckStr(Request.Form["Supplier_Cert_Name"]);
        string Supplier_Cert_Note = tools.CheckStr(Request.Form["Supplier_Cert_Note"]);
        int Supplier_Cert_Sort = tools.CheckInt(Request.Form["Supplier_Cert_Sort"]);

        if (Supplier_Cert_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写证书名称！", false, "{back}");
        }

        SupplierCertInfo entity = GetSupplierCertByID(Supplier_Cert_ID);
        if (entity != null)
        {
            entity.Supplier_Cert_ID = Supplier_Cert_ID;
            entity.Supplier_Cert_Type = Supplier_Cert_Type;
            entity.Supplier_Cert_Name = Supplier_Cert_Name;
            entity.Supplier_Cert_Note = Supplier_Cert_Note;
            entity.Supplier_Cert_Sort = Supplier_Cert_Sort;

            if (MyCert.EditSupplierCert(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Cert_Config_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSupplierCert()
    {
        int Supplier_Cert_ID = tools.CheckInt(Request.QueryString["Supplier_Cert_ID"]);
        if (MyCert.DelSupplierCert(Supplier_Cert_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Cert_Config_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SupplierCertInfo GetSupplierCertByID(int cate_id)
    {
        return MyCert.GetSupplierCertByID(cate_id, Public.GetUserPrivilege());
    }

    public virtual string Get_Supplier_Type(int Supplier_Type)
    {
        string Type_Name = "";
        switch (Supplier_Type)
        {
            case 0:
                Type_Name = "普通供应商";
                break;
            case 1:
                Type_Name = "下岗创业";
                break;
            case 2:
                Type_Name = "大学生创业";
                break;
            case 3:
                Type_Name = "自主创业";
                break;
        }
        return Type_Name;
    }

    /// <summary>
    /// 获取指定供应商类型的资质文件
    /// </summary>
    /// <param name="Supplier_Type">供应商类型</param>
    /// <returns>资质文件信息</returns>
    public virtual IList<SupplierCertInfo> GetSupplierCertByType(int Supplier_Type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertInfo.Supplier_Cert_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCertInfo.Supplier_Cert_Type", "=", Supplier_Type.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCertInfo.Supplier_Cert_Sort", "Desc"));
        return MyCert.GetSupplierCerts(Query, Public.GetUserPrivilege());

    }

    public string GetSupplierCert()
    {
        string suppliers;
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertInfo.Supplier_Cert_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertInfo.Supplier_Cert_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCert.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierCertInfo> entitys = MyCert.GetSupplierCerts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierCertInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Supplier_Cert_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Cert_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Get_Supplier_Type(entity.Supplier_Cert_Type)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Cert_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Cert_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("b399de70-e5f8-4d76-b0d7-16dc38245efc"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Cert_Config_Edit.aspx?Supplier_Cert_ID=" + entity.Supplier_Cert_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("2760865e-7bac-4e14-8e54-a7de7e99fee6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_Cert_Config_Do.aspx?action=move&Supplier_Cert_ID=" + entity.Supplier_Cert_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public void Supplier_Cert_Save()
    {
        string Supplier_Cert_1, Supplier_Cert_2, Supplier_Cert_3;
        int Supplier_CertType;
        Supplier_CertType = 0;
        Supplier_Cert_1 = "";
        Supplier_Cert_2 = "";
        Supplier_Cert_3 = "";
        Supplier_CertType = tools.CheckInt(Request["Supplier_Type"]);
        int Supplier_id = tools.CheckInt(Request["Supplier_id"]);
        IList<SupplierCertInfo> certinfos = null;
        SupplierRelateCertInfo ratecate = null;
        SupplierInfo entity = GetSupplierByID(Supplier_id);
        if (entity != null)
        {
            certinfos = GetSupplierCertByType(Supplier_CertType);
            if (certinfos != null)
            {
                //删除资质文件
                MyBLL.DelSupplierRelateCertBySupplierID(entity.Supplier_ID);
                foreach (SupplierCertInfo certinfo in certinfos)
                {
                    ratecate = new SupplierRelateCertInfo();
                    ratecate.Cert_SupplierID = entity.Supplier_ID;
                    ratecate.Cert_CertID = certinfo.Supplier_Cert_ID;
                    if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]).Length == 0)
                    {
                        ratecate.Cert_Img = tools.CheckStr(Request["Supplier_cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                        ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                    }
                    else
                    {
                        ratecate.Cert_Img = tools.CheckStr(Request["Supplier_cert" + certinfo.Supplier_Cert_ID]);
                        ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                    }
                    MyBLL.AddSupplierRelateCert(ratecate);
                    ratecate = null;
                }
            }

            entity.Supplier_CertType = Supplier_CertType;
            entity.Supplier_Cert_Status = 1;
            MyBLL.EditSupplier(entity, Public.GetUserPrivilege());
        }
    }

    #endregion

    #region 供应商标签
    public virtual void AddSupplierTag()
    {
        int Supplier_Tag_ID = tools.CheckInt(Request.Form["Supplier_Tag_ID"]);
        string Supplier_Tag_Name = tools.CheckStr(Request.Form["Supplier_Tag_Name"]);
        string Supplier_Tag_Img = tools.CheckStr(Request.Form["Supplier_Tag_Img"]);
        string Supplier_Tag_Content = tools.CheckStr(Request.Form["Supplier_Tag_Content"]);
        string Supplier_Tag_Site = Public.GetCurrentSite();

        if (Supplier_Tag_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写标签名称", false, "{back}");
        }

        SupplierTagInfo entity = new SupplierTagInfo();
        entity.Supplier_Tag_ID = Supplier_Tag_ID;
        entity.Supplier_Tag_Name = Supplier_Tag_Name;
        entity.Supplier_Tag_Img = Supplier_Tag_Img;
        entity.Supplier_Tag_Content = Supplier_Tag_Content;
        entity.Supplier_Tag_Site = Supplier_Tag_Site;

        if (MyTag.AddSupplierTag(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Tag_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSupplierTag()
    {

        int Supplier_Tag_ID = tools.CheckInt(Request.Form["Supplier_Tag_ID"]);
        string Supplier_Tag_Name = tools.CheckStr(Request.Form["Supplier_Tag_Name"]);
        string Supplier_Tag_Img = tools.CheckStr(Request.Form["Supplier_Tag_Img"]);
        string Supplier_Tag_Content = tools.CheckStr(Request.Form["Supplier_Tag_Content"]);
        string Supplier_Tag_Site = Public.GetCurrentSite();

        if (Supplier_Tag_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写标签名称", false, "{back}");
        }

        SupplierTagInfo entity = new SupplierTagInfo();
        entity.Supplier_Tag_ID = Supplier_Tag_ID;
        entity.Supplier_Tag_Name = Supplier_Tag_Name;
        entity.Supplier_Tag_Img = Supplier_Tag_Img;
        entity.Supplier_Tag_Content = Supplier_Tag_Content;
        entity.Supplier_Tag_Site = Supplier_Tag_Site;


        if (MyTag.EditSupplierTag(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Tag_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSupplierTag()
    {
        int Supplier_Tag_ID = tools.CheckInt(Request.QueryString["Supplier_Tag_ID"]);
        if (MyTag.DelSupplierTag(Supplier_Tag_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Tag_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SupplierTagInfo GetSupplierTagByID(int cate_id)
    {
        return MyTag.GetSupplierTagByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetSupplierTag()
    {
        string suppliers;
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierTagInfo.Supplier_Tag_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierTagInfo.Supplier_Tag_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyTag.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierTagInfo> entitys = MyTag.GetSupplierTags(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierTagInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Supplier_Tag_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Tag_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Tag_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("a6691534-a5e6-4636-901b-88a62ea1acc1"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Tag_edit.aspx?Supplier_Tag_ID=" + entity.Supplier_Tag_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("9a2249c4-2c18-4902-b8ea-7e597084cca5"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_Tag_do.aspx?action=move&Supplier_Tag_ID=" + entity.Supplier_Tag_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string Display_Supplier_Tag(int Supplier_ID)
    {
        string Tag_Str = "";
        IList<SupplierRelateTagInfo> relates = MyTag.GetSupplierRelateTagsBySupplierID(Supplier_ID);
        bool tag_ishave = false;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierTagInfo.Supplier_Tag_Site", "=", Public.GetCurrentSite()));
        IList<SupplierTagInfo> entitys = MyTag.GetSupplierTags(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierTagInfo entity in entitys)
            {
                tag_ishave = false;
                if (relates != null)
                {
                    foreach (SupplierRelateTagInfo relate in relates)
                    {
                        if (relate.Supplier_RelateTag_TagID == entity.Supplier_Tag_ID)
                        {
                            tag_ishave = true;
                        }
                    }
                    if (tag_ishave)
                    {
                        Tag_Str += " <input type=\"checkbox\" name=\"Tag_ID\" value=\"" + entity.Supplier_Tag_ID + "\" checked> " + entity.Supplier_Tag_Name;
                    }
                    else
                    {
                        Tag_Str += " <input type=\"checkbox\" name=\"Tag_ID\" value=\"" + entity.Supplier_Tag_ID + "\"> " + entity.Supplier_Tag_Name;
                    }
                }
                else
                {
                    Tag_Str += " <input type=\"checkbox\" name=\"Tag_ID\" value=\"" + entity.Supplier_Tag_ID + "\"> " + entity.Supplier_Tag_Name;
                }
            }
        }
        return Tag_Str;
    }
    #endregion

    #region 政策通知
    public virtual void AddSupplierMessage()
    {
        string Supplier_Message_Title = tools.CheckStr(Request.Form["Supplier_Message_Title"]);
        string Supplier_Message_Content = Request.Form["Supplier_Message_Content"];
        string Supplier_Message_SupplierID = tools.CheckStr(Request.Form["Supplier_Message_SupplierID"]);
        string Supplier_Message_Site = Public.GetCurrentSite();

        if (Supplier_Message_Content == "" || Supplier_Message_Title == "")
        {
            Public.Msg("error", "错误信息", "请选择供应商并填写政策通知标题及内容", false, "{back}");
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (Supplier_Message_SupplierID.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", Supplier_Message_SupplierID));
        }
        SupplierMessageInfo entity = null;
        IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo supplierinfo in entitys)
            {
                entity = new SupplierMessageInfo();
                entity.Supplier_Message_ID = 0;
                entity.Supplier_Message_SupplierID = supplierinfo.Supplier_ID;
                entity.Supplier_Message_Title = Supplier_Message_Title;
                entity.Supplier_Message_Content = Supplier_Message_Content;
                entity.Supplier_Message_Addtime = DateTime.Now;
                entity.Supplier_Message_IsRead = 0;
                entity.Supplier_Message_Site = Supplier_Message_Site;
                MyMessage.AddSupplierMessage(entity, Public.GetUserPrivilege());
                entity = null;
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_message.aspx");
    }

    public virtual void EditSupplierMessage()
    {
        int Supplier_Message_ID = tools.CheckInt(Request.Form["Supplier_Message_ID"]);
        string Supplier_Message_Title = tools.CheckStr(Request.Form["Supplier_Message_Title"]);
        string Supplier_Message_Content = Request.Form["Supplier_Message_Content"];
        int Supplier_Message_SupplierID = tools.CheckInt(Request.Form["Supplier_Message_SupplierID"]);
        string Supplier_Message_Site = Public.GetCurrentSite();

        if (Supplier_Message_Content == "" || Supplier_Message_Title == "" || Supplier_Message_SupplierID == 0)
        {
            Public.Msg("error", "错误信息", "请选择供应商并填写政策通知标题及内容", false, "{back}");
        }

        SupplierMessageInfo entity = new SupplierMessageInfo();
        entity.Supplier_Message_ID = Supplier_Message_ID;
        entity.Supplier_Message_SupplierID = Supplier_Message_SupplierID;
        entity.Supplier_Message_Title = Supplier_Message_Title;
        entity.Supplier_Message_Content = Supplier_Message_Content;
        entity.Supplier_Message_Addtime = DateTime.Now;
        entity.Supplier_Message_IsRead = 0;
        entity.Supplier_Message_Site = Supplier_Message_Site;

        if (MyMessage.EditSupplierMessage(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_message.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSupplierMessage()
    {
        int Supplier_Message_ID = tools.CheckInt(Request.QueryString["Supplier_Message_ID"]);

        if (MyMessage.DelSupplierMessage(Supplier_Message_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_message.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public string GetSupplierMessages()
    {
        string suppliers;
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMessageInfo.Supplier_Message_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {

            suppliers = GetSuppliersByKeyword(keyword);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierMessageInfo.Supplier_Message_SupplierID", "in", suppliers));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierMessageInfo.Supplier_Message_Title", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyMessage.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierMessageInfo> entitys = MyMessage.GetSupplierMessages(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo;
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierMessageInfo entity in entitys)
            {
                supplierinfo = MyBLL.GetSupplierByID(entity.Supplier_Message_SupplierID, Public.GetUserPrivilege());
                jsonBuilder.Append("{\"id\":" + entity.Supplier_Message_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Message_ID);
                jsonBuilder.Append("\",");

                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_Email));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Message_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_Message_IsRead == 0)
                {
                    jsonBuilder.Append("未读");
                }
                else
                {
                    jsonBuilder.Append("已读");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Message_Addtime);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("b7d38ac5-000c-4d07-9ca3-46df47367554"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_message_edit.aspx?Supplier_Message_ID=" + entity.Supplier_Message_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_message_do.aspx?action=move&Supplier_Message_ID=" + entity.Supplier_Message_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public SupplierMessageInfo GetSupplierMessageByID(int ID)
    {
        return MyMessage.GetSupplierMessageByID(ID, Public.GetUserPrivilege());
    }
    #endregion

    #region 店铺开通申请



    //店铺开通申请列表
    public string GetSupplierShopApplys()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopApplyInfo.Shop_Apply_Name", "like", keyword));
        }
        int Audit = 0;
        Audit = tools.CheckInt(Request["Audit"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopApplyInfo.Shop_Apply_Status", "=", Audit.ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLLShopApply.GetPageInfo(Query);
        IList<SupplierShopApplyInfo> entitys = MyBLLShopApply.GetSupplierShopApplys(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopApplyInfo entity in entitys)
            {

                jsonBuilder.Append("{\"SupplierShopApplyInfo.Shop_Apply_ID\":" + entity.Shop_Apply_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Apply_ID);
                jsonBuilder.Append("\",");

                SupplierInfo supplierinfo = GetSupplierByID(entity.Shop_Apply_SupplierID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr("--"));
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Apply_ShopName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Apply_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Get_Audit_Status(entity.Shop_Apply_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Apply_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Shop_ApplyView.aspx?Supplier_Shop_ApplyID=" + entity.Shop_Apply_ID + "\\\" title=\\\"查看\\\">查看</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public SupplierShopApplyInfo GetSupplierShopApplyByID(int ID)
    {
        return MyBLLShopApply.GetSupplierShopApplyByID(ID);
    }

    public SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID)
    {
        return MyBLLShopApply.GetSupplierShopApplyBySupplierID(ID);
    }

    /// <summary>
    /// 获取指定供应商店铺信息
    /// </summary>
    /// <param name="Supplier_ID">供应商ID</param>
    /// <returns>店铺信息</returns>
    public SupplierShopInfo GetSupplierShopBySupplierID(int Supplier_ID)
    {
        SupplierShopInfo entity = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "Desc"));
        IList<SupplierShopInfo> entitys = MyBLLShop.GetSupplierShops(Query);
        if (entitys != null)
        {
            entity = entitys[0];
        }
        return entity;
    }

    public virtual void SupplierApplyAudit(int status)
    {
        int Supplier_Shop_ApplyID = tools.CheckInt(Request["Supplier_Shop_ApplyID"]);
        string Shop_Apply_Note = tools.CheckStr(Request["Shop_Apply_Note"]);
        SupplierShopApplyInfo entity = GetSupplierShopApplyByID(Supplier_Shop_ApplyID);
        if (entity != null)
        {
            if (entity.Shop_Apply_Status == 0)
            {
                SupplierInfo supplierinfo = GetSupplierByID(entity.Shop_Apply_SupplierID);
                if (supplierinfo != null)
                {
                    if (status == 1)
                    {
                        SupplierShopInfo suppliershopinfo = new SupplierShopInfo();
                        suppliershopinfo.Shop_Name = entity.Shop_Apply_ShopName;
                        suppliershopinfo.Shop_Type = entity.Shop_Apply_ShopType;
                        suppliershopinfo.Shop_SupplierID = entity.Shop_Apply_SupplierID;
                        suppliershopinfo.Shop_Addtime = DateTime.Now;
                        suppliershopinfo.Shop_Recommend = 0;
                        suppliershopinfo.Shop_Evaluate = 0;
                        suppliershopinfo.Shop_Code = "";
                        suppliershopinfo.Shop_Banner = 0;
                        suppliershopinfo.Shop_Css = 0;

                        suppliershopinfo.Shop_Banner_IsActive = 1;
                        suppliershopinfo.Shop_Top_IsActive = 1;
                        suppliershopinfo.Shop_Info_IsActive = 1;
                        suppliershopinfo.Shop_LeftSearch_IsActive = 1;
                        suppliershopinfo.Shop_LeftCate_IsActive = 1;
                        suppliershopinfo.Shop_LeftSale_IsActive = 1;
                        suppliershopinfo.Shop_Left_IsActive = 1;
                        suppliershopinfo.Shop_Right_IsActive = 1;
                        suppliershopinfo.Shop_RightProduct_IsActive = 1;

                        suppliershopinfo.Shop_Domain = Get_Shop_Domain();
                        suppliershopinfo.Shop_Site = Public.GetCurrentSite();
                        MyBLLShop.AddSupplierShop(suppliershopinfo);

                        supplierinfo.Supplier_IsHaveShop = 1;
                    }

                    supplierinfo.Supplier_IsApply = 0;
                    MyBLL.EditSupplier(supplierinfo, Public.GetUserPrivilege());
                }
                entity.Shop_Apply_Status = status;

            }
            entity.Shop_Apply_Note = Shop_Apply_Note;
            if (MyBLLShopApply.EditSupplierShopApply(entity))
            {
                if (status == 1)
                {
                    SupplierMessageInfo message = new SupplierMessageInfo();
                    message.Supplier_Message_ID = 0;
                    message.Supplier_Message_SupplierID = entity.Shop_Apply_SupplierID;
                    message.Supplier_Message_Title = "您的店铺开通申请已成功通过平台审核";
                    message.Supplier_Message_Content = "您的店铺开通申请已成功通过平台审核，请重新登录后查询店铺信息，并尽快对您的店铺信息进行完善！";
                    message.Supplier_Message_Addtime = DateTime.Now;
                    message.Supplier_Message_IsRead = 0;
                    message.Supplier_Message_Site = Public.GetCurrentSite();
                    MyMessage.AddSupplierMessage(message, Public.GetUserPrivilege());
                    SupplierShopInfo shopinfo = GetSupplierShopBySupplierID(entity.Shop_Apply_SupplierID);
                    if (shopinfo != null)
                    {
                        //Public.Msg("positive", "操作成功", "操作成功", true, "/shop/Shop_View.aspx?shop_id=" + shopinfo.Shop_ID);
                        Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_ApplyView.aspx?Supplier_Shop_ApplyID=" + Supplier_Shop_ApplyID);
                    }
                    else
                    {
                        Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_ApplyView.aspx?Supplier_Shop_ApplyID=" + Supplier_Shop_ApplyID);
                    }
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_ApplyView.aspx?Supplier_Shop_ApplyID=" + Supplier_Shop_ApplyID);
                }

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //生成店铺二级域名
    public string Get_Shop_Domain()
    {
        string domain = "shop";
        domain += Public.CreatevkeyNum(6);
        SupplierShopInfo shopinfo = MyBLLShop.GetSupplierShopByDomain(domain);
        while (shopinfo != null)
        {
            domain += Public.CreatevkeyNum(6);
            shopinfo = MyBLLShop.GetSupplierShopByDomain(domain);
        }
        return domain;
    }

    #endregion

    #region "虚拟账户"
    //会员积分消费
    public void Supplier_Coin_AddConsume(int coin_amount, string coin_reason, int supplier_id, bool is_return)
    {
        int Supplier_CoinRemain = 0;
        SupplierInfo supplierinfo = MyBLL.GetSupplierByID(supplier_id, Public.GetUserPrivilege());
        if (supplierinfo != null)
        {
            Supplier_CoinRemain = supplierinfo.Supplier_CoinRemain;
            SupplierConsumptionInfo consumption = new SupplierConsumptionInfo();
            consumption.Consump_ID = 0;
            consumption.Consump_SupplierID = supplier_id;
            consumption.Consump_Coin = coin_amount;
            consumption.Consump_CoinRemain = Supplier_CoinRemain + coin_amount;
            consumption.Consump_Reason = coin_reason;
            consumption.Consump_Addtime = DateTime.Now;

            MyCoinlog.AddSupplierConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                    supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
                }
                else
                {
                    supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
                    supplierinfo.Supplier_CoinCount = supplierinfo.Supplier_CoinCount + coin_amount;
                }
            }
            else
            {
                supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
            }

            MyBLL.EditSupplier(supplierinfo, Public.GetUserPrivilege());
        }
    }

    //会员虚拟账户操作日志
    public void Supplier_Account_Log(int Supplier_ID, int Account_Type, double Amount, string Log_note)
    {
        double Member_AccountRemain = 0;
        SupplierInfo supplierinfo = GetSupplierByID(Supplier_ID);
        if (supplierinfo != null)
        {
            if (Account_Type == 0)
            {
                Member_AccountRemain = supplierinfo.Supplier_Account;
            }
            else if (Account_Type == 1)
            {
                Member_AccountRemain = supplierinfo.Supplier_Security_Account;
            }
            else
            {
                Member_AccountRemain = supplierinfo.Supplier_Adv_Account;
            }
            SupplierAccountLogInfo accountLog = new SupplierAccountLogInfo();
            accountLog.Account_Log_ID = 0;
            accountLog.Account_Log_Type = Account_Type;
            accountLog.Account_Log_SupplierID = Supplier_ID;
            accountLog.Account_Log_Amount = Amount;
            accountLog.Account_Log_AmountRemain = Member_AccountRemain + Amount;
            accountLog.Account_Log_Note = Log_note;
            accountLog.Account_Log_Addtime = DateTime.Now;

            MyAccountLog.AddSupplierAccountLog(accountLog);

            if (Amount != 0)
            {
                if (Account_Type == 0)
                {
                    supplierinfo.Supplier_Account = Member_AccountRemain + Amount;
                }
                else if (Account_Type == 1)
                {
                    supplierinfo.Supplier_Security_Account = Member_AccountRemain + Amount;
                }
                else
                {
                    supplierinfo.Supplier_Adv_Account = Member_AccountRemain + Amount;
                }
            }
            if ((Member_AccountRemain + Amount) >= 0)
            {
                MyBLL.EditSupplier(supplierinfo, Public.GetUserPrivilege());
            }

        }
    }

    /// <summary>
    /// 广告服务费用
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public double GetSupplierAdvAccount(int Supplier_ID)
    {
        double CanMoney = 0;
        SupplierInfo SupplierEntity = GetSupplierByID(Supplier_ID);

        if (SupplierEntity != null)
            CanMoney = SupplierEntity.Supplier_Adv_Account;

        return CanMoney;
    }

    public string GetSupplierConsumptions()
    {

        string Supplier_IDstr = "";

        string keyword, date_start, date_end;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Supplier_IDstr = "0";
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
            IList<SupplierInfo> members = MyBLL.GetSuppliers(Query1, Public.GetUserPrivilege());
            if (members != null)
            {
                foreach (SupplierInfo ent in members)
                {
                    Supplier_IDstr = Supplier_IDstr + "," + ent.Supplier_ID;
                }
            }
            Query1 = null;
        }


        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tools.CheckInt(Request["rows"]) < 1)
        {
            Query.PageSize = 1;
        }
        if (Supplier_IDstr != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierConsumptionInfo.Consump_SupplierID", "in", Supplier_IDstr));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{SupplierConsumptionInfo.Consump_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{SupplierConsumptionInfo.Consump_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCoinlog.GetPageInfo(Query);
        IList<SupplierConsumptionInfo> entitys = MyCoinlog.GetSupplierConsumptions(Query);

        if (entitys != null)
        {
            SupplierInfo supplierinfo = null;

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierConsumptionInfo entity in entitys)
            {



                jsonBuilder.Append("{\"SupplierConsumptionInfo.Consump_ID\":" + entity.Consump_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                supplierinfo = MyBLL.GetSupplierByID(entity.Consump_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                supplierinfo = null;
                jsonBuilder.Append("\",");

                if (entity.Consump_Coin > 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_CoinRemain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Consump_Reason));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_Addtime);
                jsonBuilder.Append("\",");



                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");


            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    ////用户积分处理
    //public void Supplier_Coin_Process()
    //{
    //    string supplier_email = tools.CheckStr(Request["supplier_email"]);
    //    int coin_amount = tools.CheckInt(Request["coin_amount"]);
    //    string coin_reason = tools.CheckStr(Request["coin_reason"]);
    //    int supplier_id = 0;
    //    int coin_remain = 0;

    //    if (supplier_email == "" || coin_amount == 0 || coin_reason == "")
    //    {
    //        Public.Msg("error", "错误提示", "请将输入要操作用户名\\积分\\备注", false, "{back}");
    //        Response.End();
    //    }
    //    SupplierInfo supplierinfo = MyBLL.GetSupplierByEmail(supplier_email, Public.GetUserPrivilege());
    //    if (supplierinfo != null)
    //    {
    //        supplier_id = supplierinfo.Supplier_ID;
    //        coin_remain = supplierinfo.Supplier_CoinRemain;
    //    }
    //    supplierinfo = null;
    //    if (supplier_id == 0)
    //    {
    //        Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
    //        Response.End();
    //    }
    //    if (coin_amount < (0 - coin_remain))
    //    {
    //        Public.Msg("error", "错误提示", "扣除积分超过会员可用积分", false, "{back}");
    //        Response.End();
    //    }
    //    Supplier_Coin_AddConsume(coin_amount, coin_reason, supplier_id, false);
    //    Public.Msg("positive", "操作成功", "操作成功", true, "coin_detail.aspx");
    //}

    ////用户虚拟账户处理
    //public void Supplier_Account_Process()
    //{
    //    string supplier_email = tools.CheckStr(Request["supplier_email"]);
    //    double account_amount = tools.CheckFloat(Request["account_amount"]);
    //    string account_reason = tools.CheckStr(Request["account_reason"]);
    //    int account_Type = tools.CheckInt(Request["account_Type"]);
    //    int supplier_id = 0;
    //    double account_remain = 0;

    //    if (supplier_email == "" || account_amount == 0 || account_reason == "")
    //    {
    //        Public.Msg("error", "错误提示", "请将输入要操作用户名\\金额\\备注", false, "{back}");
    //        Response.End();
    //    }
    //    SupplierInfo supplierinfo = MyBLL.GetSupplierByEmail(supplier_email, Public.GetUserPrivilege());
    //    if (supplierinfo != null)
    //    {
    //        supplier_id = supplierinfo.Supplier_ID;
    //        account_remain = supplierinfo.Supplier_Account;
    //    }
    //    supplierinfo = null;
    //    if (supplier_id == 0)
    //    {
    //        Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
    //        Response.End();
    //    }
    //    if (account_amount < (0 - account_remain))
    //    {
    //        Public.Msg("error", "错误提示", "扣除金额超过会员虚拟账户余额", false, "{back}");
    //        Response.End();
    //    }
    //    Supplier_Account_Log(supplier_id, account_Type, account_amount, account_reason);
    //    Public.Msg("positive", "操作成功", "操作成功", true, "Account_detail.aspx");
    //}

    //虚拟账号明细
    public string GetSupplierAccountLogs()
    {

        string Supplier_IDstr = "";
        string keyword, date_start, date_end;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Supplier_IDstr = "0";
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_CompanyName", "like", keyword));
            IList<SupplierInfo> members = MyBLL.GetSuppliers(Query1, Public.GetUserPrivilege());
            if (members != null)
            {
                foreach (SupplierInfo ent in members)
                {
                    Supplier_IDstr = Supplier_IDstr + "," + ent.Supplier_ID;
                }
            }
            Query1 = null;
        }


        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tools.CheckInt(Request["rows"]) < 1)
        {
            Query.PageSize = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAccountLogInfo.Account_Log_Site", "=", Public.GetCurrentSite()));
        if (Supplier_IDstr != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAccountLogInfo.Account_Log_SupplierID", "in", Supplier_IDstr));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{SupplierAccountLogInfo.Account_Log_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{SupplierAccountLogInfo.Account_Log_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        SupplierInfo supplierinfo = null;
        PageInfo pageinfo = MyAccountLog.GetPageInfo(Query);
        IList<SupplierAccountLogInfo> entitys = MyAccountLog.GetSupplierAccountLogs(Query);
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierAccountLogInfo entity in entitys)
            {



                jsonBuilder.Append("{\"SupplierAccountLogInfo.Account_Log_ID\":" + entity.Account_Log_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                supplierinfo = MyBLL.GetSupplierByID(entity.Account_Log_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                supplierinfo = null;

                jsonBuilder.Append("\",");

                if (entity.Account_Log_Amount > 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Account_Log_Amount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Account_Log_Amount);
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_AmountRemain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Account_Log_Note));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_Addtime);
                jsonBuilder.Append("\",");



                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");


            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 采购申请

    public void AddSupplierPurchase(int Purchase_Status)
    {
        int Purchase_TypeID = tools.CheckInt(Request["Purchase_TypeID"]);
        string Purchase_Title = tools.CheckStr(Request["Purchase_Title"]);
        string Purchase_DeliveryTime = tools.CheckStr(Request["Purchase_DeliveryTime"]);

        string Purchase_Address = tools.CheckStr(Request["Purchase_Address"]);
        string Purchase_Intro = tools.NullStr(Request["Purchase_Intro"]);
        string Purchase_ValidDate = tools.CheckStr(Request["Purchase_ValidDate"]);
        string Purchase_Attachment = tools.CheckStr(Request["Purchase_Attachment"]);
        int Purchase_Amount = tools.CheckInt(Request["Purchase_Amount"]);
        string Purchase_State = tools.CheckStr(Request["s_Purchase_State"]);
        string Purchase_City = tools.CheckStr(Request["s_Purchase_City"]);
        string Purchase_County = tools.CheckStr(Request["s_Purchase_County"]);

        int Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate"]);
        if (Purchase_Cate == 0)
        {
            Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate_parent"]);
        }
        if (Purchase_Title == "")
        {
            Public.Msg("info", "提示信息", "请填写采购标题", false, "{back}");
        }

        if (Purchase_Cate == 0)
        {
            Public.Msg("info", "提示信息", "请选择采购分类", false, "{back}");
        }
        if (Purchase_DeliveryTime == "")
        {
            Public.Msg("info", "提示信息", "请选择交货时间", false, "{back}");
        }
        if (Purchase_Address == "")
        {
            Public.Msg("info", "提示信息", "请填写交货详细地址", false, "{back}");
        }
        if (Purchase_ValidDate == "")
        {
            Public.Msg("info", "提示信息", "请填写有效期", false, "{back}");
        }

        if (Purchase_County == "")
        {
            Public.Msg("info", "提示信息", "请选择交货所属地区", false, "{back}");
        }


        bool buying = false;
        for (int i = 1; i <= Purchase_Amount; i++)
        {
            string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
            string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
            int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
            double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
            if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
            {
                buying = true; break;
            }
        }
        if (!buying)
        {
            Public.Msg("info", "提示信息", "请填写采购清单信息！", false, "{back}");
        }


        SupplierPurchaseInfo entity = new SupplierPurchaseInfo();

        entity.Purchase_SupplierID = 0;
        entity.Purchase_TypeID = 1;
        entity.Purchase_Title = Purchase_Title;
        entity.Purchase_Intro = Purchase_Intro;

        entity.Purchase_DeliveryTime = tools.NullDate(Purchase_DeliveryTime);
        entity.Purchase_ValidDate = tools.NullDate(Purchase_ValidDate);
        entity.Purchase_Addtime = DateTime.Now;

        entity.Purchase_State = Purchase_State;
        entity.Purchase_City = Purchase_City;
        entity.Purchase_County = Purchase_County;
        entity.Purchase_Address = Purchase_Address;
        entity.Purchase_Attachment = Purchase_Attachment;


        entity.Purchase_Site = Public.GetCurrentSite();
        entity.Purchase_Status = Purchase_Status;
        entity.Purchase_IsActive = 1;//非挂起
        entity.Purchase_ActiveReason = "";//
        entity.Purchase_Trash = 0;//非删除

        entity.Purchase_IsRecommend = 0;//不推荐
        entity.Purchase_IsPublic = 0;//非公开
        entity.Purchase_CateID = Purchase_Cate;
        entity.Purchase_SysUserID = tools.NullInt(Session["User_ID"]);

        if (MyPurchase.AddSupplierPurchase(entity, Public.GetUserPrivilege()))
        {
            QueryInfo Query = new QueryInfo();
            Query.CurrentPage = 1;
            Query.PageSize = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SysUserID", "=", tools.NullInt(Session["User_ID"]).ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "=", Purchase_Title));

            Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
            IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                for (int i = 1; i <= 5; i++)
                {
                    string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
                    string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
                    if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
                    {
                        SupplierPurchaseDetailInfo detailinfo = new SupplierPurchaseDetailInfo();
                        detailinfo.Detail_Name = Detail_Name;
                        detailinfo.Detail_Spec = Detail_Spec;
                        detailinfo.Detail_Amount = Detail_Amount;
                        detailinfo.Detail_Price = Detail_Price;
                        detailinfo.Detail_PurchaseID = entitys[0].Purchase_ID;
                        MyPurchaseDetail.AddSupplierPurchaseDetail(detailinfo);
                    }
                }

            }
            Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/Supplier_Purchase_list.aspx?purchase_type=1");
        }
        else
        {
            Public.Msg("error", "错误信息", "添加失败！", false, "{back}");
        }


    }

    public void EditSupplierPurchase(int Purchase_Status)
    {
        int Purchase_TypeID = tools.CheckInt(Request["Purchase_TypeID"]);
        string Purchase_Title = tools.CheckStr(Request["Purchase_Title"]);
        string Purchase_DeliveryTime = tools.CheckStr(Request["Purchase_DeliveryTime"]);

        string Purchase_Address = tools.CheckStr(Request["Purchase_Address"]);
        string Purchase_Intro = tools.NullStr(Request["Purchase_Intro"]);
        string Purchase_ValidDate = tools.CheckStr(Request["Purchase_ValidDate"]);
        string Purchase_Attachment = tools.CheckStr(Request["Purchase_Attachment"]);
        int Purchase_Amount = tools.CheckInt(Request["Purchase_Amount"]);
        string Purchase_State = tools.CheckStr(Request["s_Purchase_State"]);
        string Purchase_City = tools.CheckStr(Request["s_Purchase_City"]);
        string Purchase_County = tools.CheckStr(Request["s_Purchase_County"]);

        int apply_id = tools.CheckInt(Request["apply_id"]);
        int Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate"]);
        if (Purchase_Cate == 0)
        {
            Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate_parent"]);
        }
        if (Purchase_Title == "")
        {
            Public.Msg("info", "提示信息", "请填写标题", false, "{back}");
        }
        if (Purchase_Cate == 0)
        {
            Public.Msg("info", "提示信息", "请选择采购分类", false, "{back}");
        }
        if (Purchase_DeliveryTime == "")
        {
            Public.Msg("info", "提示信息", "请选择交货时间", false, "{back}");
        }
        if (Purchase_Address == "")
        {
            Public.Msg("info", "提示信息", "请填写交货详细地址", false, "{back}");
        }
        if (Purchase_ValidDate == "")
        {
            Public.Msg("info", "提示信息", "请填写有效期", false, "{back}");
        }

        if (Purchase_County == "")
        {
            Public.Msg("info", "提示信息", "请选择交货所属地区", false, "{back}");
        }


        bool buying = false;
        for (int i = 1; i <= Purchase_Amount; i++)
        {
            string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
            string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
            int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
            double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
            if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
            {
                buying = true;
                break;
            }
        }
        if (!buying)
        {
            Public.Msg("info", "提示信息", "请填写采购清单信息！", false, "{back}");
        }


        SupplierPurchaseInfo entity = GetSupplierPurchaseByID(apply_id);
        if (entity != null)
        {
            if (entity.Purchase_SupplierID > 0)
            {
                Response.Redirect("/supplier/Supplier_Purchase_list.aspx?purchase_type=1");
            }
            entity.Purchase_Title = Purchase_Title;
            entity.Purchase_Intro = Purchase_Intro;

            entity.Purchase_DeliveryTime = tools.NullDate(Purchase_DeliveryTime);
            entity.Purchase_ValidDate = tools.NullDate(Purchase_ValidDate);

            entity.Purchase_State = Purchase_State;
            entity.Purchase_City = Purchase_City;
            entity.Purchase_County = Purchase_County;
            entity.Purchase_Address = Purchase_Address;
            entity.Purchase_Attachment = Purchase_Attachment;

            entity.Purchase_CateID = Purchase_Cate;

            entity.Purchase_Status = 1;

            if (MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege()))
            {
                MyPurchaseDetail.DelSupplierPurchaseDetailByPurchaseID(apply_id);
                for (int i = 1; i <= Purchase_Amount; i++)
                {
                    string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
                    string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
                    if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
                    {
                        SupplierPurchaseDetailInfo detailinfo = new SupplierPurchaseDetailInfo();
                        detailinfo.Detail_Name = Detail_Name;
                        detailinfo.Detail_Spec = Detail_Spec;
                        detailinfo.Detail_Amount = Detail_Amount;
                        detailinfo.Detail_Price = Detail_Price;
                        detailinfo.Detail_PurchaseID = apply_id;
                        MyPurchaseDetail.AddSupplierPurchaseDetail(detailinfo);
                    }
                }

                Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/Supplier_Purchase_list.aspx?purchase_type=1");
            }
            else
            {
                Public.Msg("error", "错误信息", "修改失败！", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "修改失败！", false, "{back}");
        }


    }

    //采购申请清单
    public string ShowSupplierPurchaseForm(int Purchase_ID)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"30\">序号</td>");
        jsonBuilder.Append("        <td>产品名称</td>");
        jsonBuilder.Append("        <td>规格/单位</td>");
        jsonBuilder.Append("        <td>采购数量</td>");
        jsonBuilder.Append("        <td>采购单价</td>");
        jsonBuilder.Append("    </tr>");
        int i = 0;
        IList<SupplierPurchaseDetailInfo> entitys = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in entitys)
            {
                i++;
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");

                jsonBuilder.Append("        <td align=\"center\">" + i + "</td>");
                jsonBuilder.Append("        <td align=\"left\"><input type=\"text\" style=\"width:200px;\" value=\"" + entity.Detail_Name + "\" name=\"Detail_Name" + i + "\" id=\"Detail_Name" + i + "\" /></td>");
                jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" value=\"" + entity.Detail_Spec + "\" name=\"Detail_Spec" + i + "\" id=\"Detail_Spec" + i + "\" /></td>");
                jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" value=\"" + entity.Detail_Amount + "\" name=\"Detail_Amount" + i + "\" id=\"Detail_Amount" + i + "\" /></td>");
                jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" value=\"" + entity.Detail_Price + "\" name=\"Detail_Price" + i + "\" id=\"Detail_Price" + i + "\" /></td>");
                jsonBuilder.Append("    </tr>");
            }

        }
        for (int jj = i + 1; jj < 6; jj++)
        {
            jsonBuilder.Append("    <tr class=\"list_td_bg\">");

            jsonBuilder.Append("        <td align=\"center\">" + jj + "</td>");
            jsonBuilder.Append("        <td align=\"left\"><input type=\"text\" style=\"width:200px;\" name=\"Detail_Name" + jj + "\" id=\"Detail_Name" + jj + "\" /></td>");
            jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" name=\"Detail_Spec" + jj + "\" id=\"Detail_Spec" + jj + "\" /></td>");
            jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" name=\"Detail_Amount" + jj + "\" id=\"Detail_Amount" + jj + "\" /></td>");
            jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" style=\"width:200px;\" name=\"Detail_Price" + jj + "\" id=\"Detail_Price" + jj + "\" /></td>");
            jsonBuilder.Append("    </tr>");
        }
        jsonBuilder.Append("</table>");

        return jsonBuilder.ToString();
    }

    public string Purchase_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_SupplierPurchaseCategory_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {

                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseCategoryInfo.Cate_ParentID", "=", cate));
                IList<SupplierPurchaseCategoryInfo> categorys = MyPurchaseCategory.GetSupplierPurchaseCategorys(Query, Public.GetUserPrivilege());
                if (categorys != null)
                {

                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"Purchase_Cate\" name=\"Purchase_Cate\" onchange=\"change_mainpurchasecate('" + div_name + "','Purchase_Cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "Purchase_Cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_mainpurchasecate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (SupplierPurchaseCategoryInfo entity in categorys)
                    {
                        if (entity.Cate_IsActive == 1)
                        {
                            if (parentid == entity.Cate_ID || cate_id == entity.Cate_ID)
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\" selected>" + entity.Cate_Name + "</option>";
                            }
                            else
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\">" + entity.Cate_Name + "</option>";
                            }
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                categorys = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }

    public string Get_SupplierPurchaseCategory_Relate(int cate_id, string cate_str)
    {

        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            SupplierPurchaseCategoryInfo category = MyPurchaseCategory.GetSupplierPurchaseCategoryByID(cate_id, Public.GetUserPrivilege());
            if (category != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_SupplierPurchaseCategory_Relate(category.Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }


    //采购申请审核状态
    public string SupplierPurchaseStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "草稿"; break;
            case 1:
                resultstr = "未审核"; break;
            case 2:
                resultstr = "已审核"; break;
            case 3:
                resultstr = "审核不通过"; break;
            default:
                resultstr = " -- "; break;
        }
        return resultstr;
    }

    //采购申请列表
    public string GetSupplierPurchases()
    {
        QueryInfo Query = new QueryInfo();
        int Trash = tools.CheckInt(Request["Trash"]);
        int Purchase_Type = tools.CheckInt(Request["Purchase_Type"]);
        int audit = tools.CheckInt(Request["audit"]);
        int isactive = tools.CheckInt(Request["isactive"]);
        int overdue = tools.CheckInt(Request["overdue"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int Purchase_CateID = tools.CheckInt(Request["Purchase_Cate"]);
        //if (Purchase_CateID == 0) { Purchase_CateID = tools.CheckInt(Request["Purchase_Cate_parent"]); }
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", Trash.ToString()));
        if (Trash == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_TypeID", "=", Purchase_Type.ToString()));
        }
        if (overdue == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", "<", DateTime.Now.ToString("yyyy-MM-dd")));
        }
        if (Purchase_CateID > 0)
        {
            string subCates = Get_All_SubSupplierPurchaseCate(Purchase_CateID);
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_CateID", "in", subCates));
        }




        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", ">", "0"));
        if (audit > 0)
        {
            if (audit > 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", audit.ToString()));
        }
        if (isactive > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToString("yyyy-MM-dd")));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsActive", "=", "0"));
        }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyPurchase.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchasesList(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo;
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                supplierinfo = null;
                if (entity.Purchase_SupplierID > 0)
                {
                    supplierinfo = MyBLL.GetSupplierByID(entity.Purchase_SupplierID, Public.GetUserPrivilege());
                }
                jsonBuilder.Append("{\"id\":" + entity.Purchase_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Purchase_ID);
                jsonBuilder.Append("\",");

                SupplierPurchaseCategoryInfo category = GetSupplierPurchaseCategoryByID(entity.Purchase_CateID);
                string cateName = " -- ";
                if (category != null)
                {
                    cateName = category.Cate_Name;
                }

                jsonBuilder.Append("\"");

                jsonBuilder.Append(cateName);

                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Purchase_Title));
                jsonBuilder.Append("\",");

                if (entity.Purchase_SupplierID > 0)
                {
                    if (supplierinfo != null)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                        jsonBuilder.Append("\",");
                    }
                    else
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append("--");
                        jsonBuilder.Append("\",");
                    }
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("易耐产业金服");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                if (entity.Purchase_TypeID == 0)
                {
                    jsonBuilder.Append("定制采购");
                }
                else
                {
                    jsonBuilder.Append("代理采购");
                }
                jsonBuilder.Append("\",");





                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Purchase_DeliveryTime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Purchase_ValidDate.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(SupplierPurchaseStatus(entity.Purchase_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Purchase_IsActive == 0)
                {
                    jsonBuilder.Append("已挂起");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Purchase_IsRecommend == 0)
                {
                    jsonBuilder.Append("未推荐");
                }
                else
                {
                    jsonBuilder.Append("已推荐");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");


                if (Public.CheckPrivilege("ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"  alt=\\\"管理报价\\\"> <a href=\\\"supplier_pricereport_list.aspx?Purchase_ID=" + entity.Purchase_ID + "\\\" title=\\\"管理报价\\\">管理报价</a>");
                }
                if (Public.CheckPrivilege("b482df13-2941-4314-9200-b64db8b358bc") && entity.Purchase_SupplierID > 0 && entity.Purchase_Trash == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/btn_add.gif\\\"  alt=\\\"创建报价\\\"> <a href=\\\"supplier_pricereport_add.aspx?Purchase_ID=" + entity.Purchase_ID + "\\\" title=\\\"创建报价\\\">创建报价</a>");
                }

                SupplierAgentProtocalInfo agentinfo = GetSupplierAgentProtocalByPurchaseID(entity.Purchase_ID);


                if (Public.CheckPrivilege("e0920e95-65fa-4e3c-9dd6-2794ccc45782") && entity.Purchase_TypeID == 1 && entity.Purchase_SupplierID > 0 && agentinfo == null && entity.Purchase_Trash == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/btn_add.gif\\\"  alt=\\\"创建协议\\\"> <a href=\\\"Agent_Protocal_add.aspx?Purchase_ID=" + entity.Purchase_ID + "\\\" title=\\\"创建协议\\\">创建协议</a>");
                }
                if (entity.Purchase_SupplierID == 0 && Public.CheckPrivilege("aa55fc69-156e-45fe-84fa-f0df964cd3e0") && entity.Purchase_Trash == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_view.gif\\\"  alt=\\\"修改\\\"> <a href=\\\"Supplier_Purchase_edit.aspx?Purchase_ID=" + entity.Purchase_ID + "\\\" title=\\\"修改\\\">修改</a> ");
                }
                jsonBuilder.Append(" <img src=\\\"/images/icon_view.gif\\\"  alt=\\\"查看\\\"> <a href=\\\"Supplier_Purchase_view.aspx?Purchase_ID=" + entity.Purchase_ID + "\\\" title=\\\"查看\\\">查看</a> ");
                if (Public.CheckPrivilege("af3d7bc9-1182-4aea-9be5-a826be6a5615") && entity.Purchase_Trash == 1)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_Purchase_do.aspx?action=trash&Purchase_ID=" + entity.Purchase_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string Get_All_SubSupplierPurchaseCate(int Cate_id)
    {
        string Cate_Arry = MyPurchaseCategory.Get_All_SubSupplierPurchaseCateID(Cate_id);
        return Cate_Arry;
    }

    //采购申请清单
    public string ShowSupplierPurchaseDetail(int Purchase_ID)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"30\">序号</td>");
        jsonBuilder.Append("        <td>产品名称</td>");
        jsonBuilder.Append("        <td>规格/单位</td>");
        jsonBuilder.Append("        <td>采购数量</td>");
        jsonBuilder.Append("        <td>采购单价</td>");
        jsonBuilder.Append("    </tr>");
        int i = 0;
        IList<SupplierPurchaseDetailInfo> entitys = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in entitys)
            {
                i++;
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");

                jsonBuilder.Append("        <td align=\"center\">" + i + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + Public.JsonStr(entity.Detail_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.JsonStr(entity.Detail_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + entity.Detail_Amount + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.DisplayCurrency(entity.Detail_Price) + "</td>");
                jsonBuilder.Append("    </tr>");
            }

        }
        jsonBuilder.Append("</table>");

        return jsonBuilder.ToString();
    }



    //编辑采购清单
    public void EditSupplierPurchase()
    {
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);
        int Purchase_IsPublic = tools.CheckInt(Request["Purchase_IsPublic"]);
        string SupplierID = tools.CheckStr(Request["Supplier_Message_SupplierID"]);
        if (Purchase_IsPublic == 0 && SupplierID.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择指定报价卖家", false, "{back}");
        }
        SupplierPurchaseInfo entity = GetSupplierPurchaseByID(Purchase_ID);
        SupplierPurchasePrivateInfo privateinfo;
        if (entity != null)
        {
            entity.Purchase_IsPublic = Purchase_IsPublic;
            if (MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege()))
            {
                MyPurchase.DelSupplierPurchasePrivateByPurchase(Purchase_ID);
                foreach (string ids in SupplierID.Split(','))
                {
                    if (tools.CheckInt(ids) > 0)
                    {
                        privateinfo = new SupplierPurchasePrivateInfo();
                        privateinfo.Purchase_Private_PurchaseID = Purchase_ID;
                        privateinfo.Purchase_Private_SupplierID = tools.CheckInt(ids);
                        MyPurchase.AddSupplierPurchasePrivate(privateinfo);
                        privateinfo = null;
                    }
                }
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Purchase_list.aspx?Purchase_Type=" + entity.Purchase_TypeID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public IList<SupplierInfo> GetSupplierPurchasePrivate(int Purchase_ID)
    {
        IList<SupplierInfo> suppliers = new List<SupplierInfo>();
        SupplierInfo supplierinfo = null;
        IList<SupplierPurchasePrivateInfo> entitys = MyPurchase.GetSupplierPurchasePrivatesByPurchase(Purchase_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchasePrivateInfo entity in entitys)
            {
                supplierinfo = new SupplierInfo();
                supplierinfo.Supplier_ID = entity.Purchase_Private_SupplierID;
                suppliers.Add(supplierinfo);
                supplierinfo = null;
            }
        }
        return suppliers;
    }

    //获取采购申请信息
    public SupplierPurchaseInfo GetSupplierPurchaseByID(int ID)
    {
        return MyPurchase.GetSupplierPurchaseByID(ID, Public.GetUserPrivilege());
    }
    //获取采购申请信息
    public SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID)
    {
        return MyAgentProtocal.GetSupplierAgentProtocalByPurchaseID(PurchaseID, Public.GetUserPrivilege());
    }

    /// <summary>
    /// 删除采购申请
    /// </summary>
    public virtual void DelSupplierPurchases()
    {
        int Supplier_Purchase_ID = tools.CheckInt(Request.QueryString["Purchase_ID"]);

        if (MyPurchase.DelSupplierPurchase(Supplier_Purchase_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Purchase_list.aspx?trash=1");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    /// <summary>
    /// 批量审核采购申请
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierPurchaseAudit(int Status)
    {
        string purchase_id = tools.CheckStr(Request["purchase_id"]);
        int purchase_type = tools.CheckInt(Request["purchase_type"]);
        if (purchase_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(purchase_id, 1) == ",") { purchase_id = purchase_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_ID", "in", purchase_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Asc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                entity.Purchase_Status = Status;
                MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_Purchase_list.aspx?audit=" + Status + "&purchase_type=" + purchase_type);

    }

    /// <summary>
    /// 批量推荐采购申请
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierPurchaseRecommend(int Status)
    {
        string purchase_id = tools.CheckStr(Request["purchase_id"]);
        int purchase_type = tools.CheckInt(Request["purchase_type"]);
        if (purchase_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(purchase_id, 1) == ",") { purchase_id = purchase_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_ID", "in", purchase_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", "2"));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Asc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                entity.Purchase_IsRecommend = Status;
                MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_Purchase_list.aspx?audit=2&purchase_type=" + purchase_type);

    }

    /// <summary>
    /// 批量挂起采购申请
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierPurchaseActive(int Status)
    {
        string purchase_id = tools.CheckStr(Request["purchase_id"]);
        int purchase_type = tools.CheckInt(Request["purchase_type"]);
        if (purchase_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(purchase_id, 1) == ",") { purchase_id = purchase_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_ID", "in", purchase_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Asc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                entity.Purchase_IsActive = Status;
                MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_Purchase_list.aspx?purchase_type=" + purchase_type);

    }

    /// <summary>
    /// 批量回收采购申请
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierPurchaseTrash(int Status)
    {
        string purchase_id = tools.CheckStr(Request["purchase_id"]);
        int purchase_type = tools.CheckInt(Request["purchase_type"]);
        if (purchase_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(purchase_id, 1) == ",") { purchase_id = purchase_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_ID", "in", purchase_id));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Asc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                entity.Purchase_Trash = Status;
                MyPurchase.EditSupplierPurchase(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_Purchase_list.aspx?trash=" + Status);

    }
    #endregion

    #region 询价信息

    public ProductInfo GetProductByID(int ID)
    {
        return MyProduct.GetProductByID(ID, Public.GetUserPrivilege());
    }

    //获取询价信息
    public SupplierPriceAskInfo GetSupplierPriceAskByID(int ID)
    {
        return MyPriceAsk.GetSupplierPriceAskByID(ID, Public.GetUserPrivilege());
    }

    //询价信息列表
    public string GetSupplierPriceAsk()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int reply = tools.CheckInt(Request["reply"]);
        int overdue = tools.CheckInt(Request["overdue"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceAskInfo.PriceAsk_ID", ">", "0"));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierPriceAskInfo.PriceAsk_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierPriceAskInfo.PriceAsk_ProductID", "in", "select product_id from product_basic where product_name like '%" + keyword + "%'"));
        }
        if (reply > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceAskInfo.PriceAsk_IsReply", "=", (reply - 1).ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyPriceAsk.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierPriceAskInfo> entitys = MyPriceAsk.GetSupplierPriceAsks(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierPriceAskInfo entity in entitys)
            {
                ProductInfo productinfo = MyProduct.GetProductByID(entity.PriceAsk_ProductID, Public.GetUserPrivilege());

                jsonBuilder.Append("{\"id\":" + entity.PriceAsk_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.PriceAsk_ID);
                jsonBuilder.Append("\",");

                if (productinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(productinfo.Product_Name));
                    jsonBuilder.Append("\",");
                }
                else
                {

                    jsonBuilder.Append("\"--\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.PriceAsk_Title));
                jsonBuilder.Append("\",");

                SupplierInfo supplierinfo = MyBLL.GetSupplierByID(entity.PriceAsk_MemberID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                if (entity.PriceAsk_IsReply == 1)
                {
                    jsonBuilder.Append("已回复");
                }
                else
                {
                    jsonBuilder.Append("未回复");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.PriceAsk_AddTime);
                jsonBuilder.Append("\",");





                jsonBuilder.Append("\"");

                jsonBuilder.Append(" <img src=\\\"/images/icon_view.gif\\\"  alt=\\\"查看\\\"> <a href=\\\"Supplier_priceask_view.aspx?priceask_id=" + entity.PriceAsk_ID + "\\\" title=\\\"查看\\\">查看</a> ");

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    //回复询价信息
    public void EditSupplierPriceAsk()
    {
        int PriceAsk_ID = tools.CheckInt(Request["PriceAsk_ID"]);
        string PriceAsk_ReplyContent = tools.CheckStr(Request["PriceAsk_ReplyContent"]);
        SupplierPriceAskInfo entity = GetSupplierPriceAskByID(PriceAsk_ID);
        if (entity != null)
        {
            entity.PriceAsk_ReplyContent = PriceAsk_ReplyContent;
            if (PriceAsk_ReplyContent.Length > 0)
            {
                entity.PriceAsk_IsReply = 1;
                entity.PriceAsk_ReplyTime = DateTime.Now;
            }
            else
            {
                entity.PriceAsk_IsReply = 0;
            }
            if (MyPriceAsk.EditSupplierPriceAsk(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "supplier_priceask_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    #endregion

    #region 报价信息

    //获取报价信息
    public SupplierPriceReportInfo GetSupplierPriceReportByID(int ID)
    {
        return MyPriceReport.GetSupplierPriceReportByID(ID, Public.GetUserPrivilege());
    }

    //报价信息列表
    public string GetSupplierPriceReport()
    {
        QueryInfo Query = new QueryInfo();
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);
        int audit = tools.CheckInt(Request["audit"]);
        int reply = tools.CheckInt(Request["reply"]);
        int overdue = tools.CheckInt(Request["overdue"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Purchase_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "=", Purchase_ID.ToString()));
        }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", "select Purchase_ID from supplier_purchase where Purchase_Title like '%" + keyword + "%'"));
        }
        if (audit > 0)
        {
            if (audit > 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", "select Purchase_ID from supplier_purchase where Purchase_ValidDate>='" + DateTime.Now.ToShortDateString() + "'"));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_AuditStatus", "=", (audit - 1).ToString()));
        }
        if (reply > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", "select Purchase_ID from supplier_purchase where Purchase_ValidDate>='" + DateTime.Now.ToShortDateString() + "'"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_IsReply", "=", (reply - 1).ToString()));
        }
        if (overdue > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", "select Purchase_ID from supplier_purchase where Purchase_ValidDate<'" + DateTime.Now.ToShortDateString() + "'"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyPriceReport.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierPriceReportInfo> entitys = MyPriceReport.GetSupplierPriceReports(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo;
        SupplierPurchaseInfo purchaseinfo;
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierPriceReportInfo entity in entitys)
            {
                purchaseinfo = GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);

                jsonBuilder.Append("{\"id\":" + entity.PriceReport_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.PriceReport_ID);
                jsonBuilder.Append("\",");

                if (purchaseinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(purchaseinfo.Purchase_Title));
                    jsonBuilder.Append("\",");

                    if (purchaseinfo.Purchase_SupplierID > 0)
                    {
                        supplierinfo = MyBLL.GetSupplierByID(purchaseinfo.Purchase_SupplierID, Public.GetUserPrivilege());
                        if (supplierinfo != null)
                        {
                            jsonBuilder.Append("\"");
                            jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                            jsonBuilder.Append("\",");
                        }
                        else
                        {
                            jsonBuilder.Append("\"");
                            jsonBuilder.Append("--");
                            jsonBuilder.Append("\",");
                        }
                    }
                    else
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append("平台");
                        jsonBuilder.Append("\",");
                    }
                }
                else
                {

                    jsonBuilder.Append("\"--\",");

                    jsonBuilder.Append("\"--\",");
                }



                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.PriceReport_Title));
                //jsonBuilder.Append("\",");

                if (entity.PriceReport_MemberID > 0)
                {
                    supplierinfo = MyBLL.GetSupplierByID(entity.PriceReport_MemberID, Public.GetUserPrivilege());
                    if (supplierinfo != null)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                        jsonBuilder.Append("\",");
                    }
                    else
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append("--");
                        jsonBuilder.Append("\",");
                    }
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("平台");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                if (entity.PriceReport_AuditStatus == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else if (entity.PriceReport_AuditStatus == 2)
                {
                    jsonBuilder.Append("审核不通过");
                }
                else
                {
                    jsonBuilder.Append("未审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.PriceReport_IsReply == 1)
                {
                    jsonBuilder.Append("已回复");
                }
                else
                {
                    jsonBuilder.Append("未回复");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.PriceReport_AddTime);
                jsonBuilder.Append("\",");





                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("9d8d62af-29b1-4302-957c-268732fc15b4") && entity.PriceReport_MemberID > 0 && purchaseinfo != null && entity.PriceReport_AuditStatus == 1)
                {
                    if (purchaseinfo.Purchase_ValidDate >= tools.NullDate(DateTime.Now.ToShortDateString()) && purchaseinfo.Purchase_Trash == 0 && purchaseinfo.Purchase_Status == 2)
                    {
                        jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\"  alt=\\\"创建采购单\\\"> <a href=\\\"/orders/purchase_orders_add.aspx?PriceReport_ID=" + entity.PriceReport_ID + "\\\" title=\\\"创建采购单\\\">创建采购单</a> ");
                    }
                }
                if (Public.CheckPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"  alt=\\\"查看\\\"> <a href=\\\"Supplier_pricereport_view.aspx?PriceReport_ID=" + entity.PriceReport_ID + "\\\" title=\\\"查看\\\">查看</a> ");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 批量审核报价信息
    /// </summary>
    /// <param name="Status">审核状态</param>
    public virtual void SupplierPriceReportAudit(int Status)
    {
        string pricereport_id = tools.CheckStr(Request["pricereport_id"]);
        string Purchase_ID = tools.CheckStr(Request["Purchase_ID"]);
        if (pricereport_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(pricereport_id, 1) == ",") { pricereport_id = pricereport_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_ID", "in", pricereport_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Asc"));
        IList<SupplierPriceReportInfo> entitys = MyPriceReport.GetSupplierPriceReports(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (SupplierPriceReportInfo entity in entitys)
            {
                entity.PriceReport_AuditStatus = Status;//##
                MyPriceReport.EditSupplierPriceReport(entity, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("Supplier_Pricereport_list.aspx?audit=" + (Status + 1) + "&purchase_id=" + Purchase_ID);

    }

    //回复报价信息
    public void EditSupplierPriceReport()
    {
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        string PriceReport_ReplyContent = tools.CheckStr(Request["PriceReport_ReplyContent"]);
        SupplierPriceReportInfo entity = GetSupplierPriceReportByID(PriceReport_ID);
        if (entity != null)
        {

            entity.PriceReport_ReplyContent = PriceReport_ReplyContent;
            if (PriceReport_ReplyContent.Length > 0)
            {
                entity.PriceReport_IsReply = 1;
                entity.PriceReport_ReplyTime = DateTime.Now;
            }
            else
            {
                entity.PriceReport_IsReply = 0;
            }
            if (MyPriceReport.EditSupplierPriceReport(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "supplier_pricereport_list.aspx?Purchase_ID=" + entity.PriceReport_PurchaseID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //报价清单
    public string ShowSupplierPriceReportDetail(int Purchase_ID, int PriceReport_ID)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"30\">序号</td>");
        jsonBuilder.Append("        <td>产品名称</td>");
        jsonBuilder.Append("        <td>规格/单位</td>");
        jsonBuilder.Append("        <td>采购数量</td>");
        jsonBuilder.Append("        <td>采购单价</td>");
        jsonBuilder.Append("        <td>报价数量</td>");
        jsonBuilder.Append("        <td>报价单价</td>");
        jsonBuilder.Append("    </tr>");
        int i = 0;
        bool iscontain = false;
        IList<SupplierPurchaseDetailInfo> entitys = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        IList<SupplierPriceReportDetailInfo> details = MyPriceReportDetail.GetSupplierPriceReportDetailsByPriceReportID(PriceReport_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in entitys)
            {
                iscontain = false;
                i++;
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");

                jsonBuilder.Append("        <td align=\"center\">" + i + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + Public.JsonStr(entity.Detail_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.JsonStr(entity.Detail_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + entity.Detail_Amount + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.DisplayCurrency(entity.Detail_Price) + "</td>");
                if (details != null)
                {
                    foreach (SupplierPriceReportDetailInfo detail in details)
                    {
                        if (detail.Detail_PurchaseDetailID == entity.Detail_ID)
                        {
                            iscontain = true;
                            jsonBuilder.Append("        <td align=\"center\">" + detail.Detail_Amount + "</td>");
                            jsonBuilder.Append("        <td align=\"center\">" + Public.DisplayCurrency(detail.Detail_Price) + "</td>");
                            break;
                        }
                    }
                }
                if (iscontain == false)
                {
                    jsonBuilder.Append("        <td align=\"center\">--</td>");
                    jsonBuilder.Append("        <td align=\"center\">--</td>");
                }
                jsonBuilder.Append("    </tr>");
            }

        }
        jsonBuilder.Append("</table>");

        return jsonBuilder.ToString();
    }

    //报价清单表单
    public string ShowSupplierPriceReportDetailForm(int Purchase_ID)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"30\">序号</td>");
        jsonBuilder.Append("        <td>产品名称</td>");
        jsonBuilder.Append("        <td>规格/单位</td>");
        jsonBuilder.Append("        <td>采购数量</td>");
        jsonBuilder.Append("        <td>采购单价</td>");
        jsonBuilder.Append("        <td>报价数量</td>");
        jsonBuilder.Append("        <td>报价单价</td>");
        jsonBuilder.Append("    </tr>");
        int i = 0;
        IList<SupplierPurchaseDetailInfo> entitys = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in entitys)
            {
                i++;
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");

                jsonBuilder.Append("        <td align=\"center\">" + i + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + Public.JsonStr(entity.Detail_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.JsonStr(entity.Detail_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + entity.Detail_Amount + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + Public.DisplayCurrency(entity.Detail_Price) + "</td>");
                jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"Detail_Amount_" + entity.Detail_ID + "\" value=\"0\" size=\"20\"></td>");
                jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"Detail_Price_" + entity.Detail_ID + "\" value=\"0\" size=\"20\"></td>");
                jsonBuilder.Append("    </tr>");
            }

        }
        jsonBuilder.Append("</table>");

        return jsonBuilder.ToString();
    }

    //创建报价信息
    public void AddSupplierPriceReport()
    {

        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);


        string PriceReport_Title = tools.CheckStr(Request["PriceReport_Title"]);
        string PriceReport_Name = tools.CheckStr(Request["PriceReport_Name"]);
        string PriceReport_Phone = tools.CheckStr(Request["PriceReport_Phone"]);
        string PriceReport_DeliveryDate = tools.CheckStr(Request["PriceReport_DeliveryDate"]);

        if (PriceReport_Title == "")
        {
            Public.Msg("info", "提示信息", "请填写报价标题", false, "{back}");
        }
        if (PriceReport_Name == "")
        {
            Public.Msg("info", "提示信息", "请填写联系人名称", false, "{back}");
        }
        if (!Public.Checkmobile(PriceReport_Phone))
        {
            Public.Msg("info", "提示信息", "手机格式不正确", false, "{back}");
        }

        if (PriceReport_DeliveryDate == "")
        {
            Public.Msg("info", "提示信息", "请选择交货时间", false, "{back}");
        }


        //采购实体
        SupplierPurchaseInfo entity = GetSupplierPurchaseByID(Purchase_ID);
        if (entity != null)
        {
            if (entity.Purchase_Status != 2)
            {
                Public.Msg("error", "错误信息", "该信息未通过审核", false, "{back}");
                Response.End();
            }
            if (entity.Purchase_IsActive != 1)
            {
                Public.Msg("error", "错误信息", "该信息已被挂起", false, "{back}");
                Response.End();
            }
            if (entity.Purchase_Trash != 0)
            {
                Public.Msg("error", "错误信息", "该信息已被移至回收站", false, "{back}");
                Response.End();
            }
            if (entity.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
            {
                Public.Msg("error", "错误信息", "该信息已过期", false, "{back}");
                Response.End();
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        }
        //采购明细
        IList<SupplierPurchaseDetailInfo> entitys = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        if (entitys != null)
        {
            foreach (SupplierPurchaseDetailInfo entity1 in entitys)
            {
                int Detail_Amount = tools.CheckInt(Request["Detail_Amount_" + entity1.Detail_ID]);
                double Detail_Price = tools.CheckFloat(Request["Detail_Price_" + entity1.Detail_ID]);

                if (Detail_Amount == 0 || Detail_Price == 0)
                {
                    Public.Msg("info", "提示信息", "商品报价信息填写不完整", false, "{back}");
                }
            }
        }
        DateTime now = DateTime.Now;

        //报价实体
        SupplierPriceReportInfo reportinfo = new SupplierPriceReportInfo();
        reportinfo.PriceReport_Title = PriceReport_Title;
        reportinfo.PriceReport_Name = PriceReport_Name;
        reportinfo.PriceReport_Phone = PriceReport_Phone;
        reportinfo.PriceReport_DeliveryDate = tools.NullDate(PriceReport_DeliveryDate);
        reportinfo.PriceReport_AddTime = now;
        reportinfo.PriceReport_ReplyTime = now;
        reportinfo.PriceReport_ReplyContent = "";
        reportinfo.PriceReport_MemberID = 0;
        reportinfo.PriceReport_IsReply = 0;
        reportinfo.PriceReport_AuditStatus = 1;
        reportinfo.PriceReport_PurchaseID = Purchase_ID;

        //报价成功
        if (MyPriceReport.AddSupplierPriceReport(reportinfo, Public.GetUserPrivilege()))
        {
            QueryInfo Query = new QueryInfo();
            Query.CurrentPage = 1;
            Query.PageSize = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_MemberID", "=", "0"));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_Title", "like", PriceReport_Title));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "=", Purchase_ID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Desc"));

            //取报价实体
            IList<SupplierPriceReportInfo> entitys1 = MyPriceReport.GetSupplierPriceReports(Query, Public.GetUserPrivilege());
            if (entitys1 != null && entitys != null)
            {
                foreach (SupplierPurchaseDetailInfo entity1 in entitys)
                {
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount_" + entity1.Detail_ID]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price_" + entity1.Detail_ID]);

                    if (Detail_Amount > 0 && Detail_Price > 0)
                    {
                        SupplierPriceReportDetailInfo reportdetailinfo = new SupplierPriceReportDetailInfo();
                        reportdetailinfo.Detail_PurchaseDetailID = entity1.Detail_ID;
                        reportdetailinfo.Detail_PurchaseID = Purchase_ID;
                        reportdetailinfo.Detail_PriceReportID = entitys1[0].PriceReport_ID;
                        reportdetailinfo.Detail_Price = Detail_Price;
                        reportdetailinfo.Detail_Amount = Detail_Amount;

                        MyPriceReportDetail.AddSupplierPriceReportDetail(reportdetailinfo, Public.GetUserPrivilege());
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/supplier_pricereport_list.aspx?Purchase_ID=" + Purchase_ID);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试！", false, "{back}");
        }
    }
    #endregion

    #region "邮件处理"
    //发送邮件处理
    public void Send_Sysemail()
    {

        string supplier_id = "";
        supplier_id = tools.CheckStr(Request["Supplier_Message_SupplierID"]);
        if (supplier_id == "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_AllowSysEmail", "=", "1"));

            Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Asc"));

            IList<SupplierInfo> entitys = MyBLL.GetSuppliers(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (SupplierInfo entity in entitys)
                {
                    if (supplier_id == "")
                    {
                        supplier_id = entity.Supplier_ID.ToString();
                    }
                    else
                    {
                        supplier_id = supplier_id + "," + entity.Supplier_ID.ToString();
                    }
                }
            }
        }

        string sysmail_title = tools.CheckStr(Request.Form["sysmail_title"]);
        string sysmail_content = tools.CheckHTML(Request.Form["sysmail_content"]);

        //FORM重复提交
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<title>管理平台</title>");
        sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
        sb.Append("<link rel=\"stylesheet\" href=\"/public/style.css\" type=\"text/css\">");
        sb.Append("</head>");
        sb.Append("<body bgcolor=\"#FFFFFF\" text=\"#000000\" onload=\"document.form1.submit();\">");
        sb.Append("<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">");
        sb.Append("  <form name=\"form1\" method=\"post\" action=\"sysemail_do.aspx\" >");
        sb.Append("\t<tr>");
        sb.Append("\t  <td>");
        sb.Append("\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>");
        sb.Append("\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >");
        sb.Append("\t <input name=\"Supplier_Message_SupplierID\" type=\"hidden\" id=\"Supplier_Message_SupplierID\" value=\"" + supplier_id + "\" >");
        sb.Append("\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >");
        sb.Append("\t  </td>");
        sb.Append("\t</tr>");
        sb.Append("  </form>");
        sb.Append("</table>");
        sb.Append("</body>");
        sb.Append("</html>");
        Response.Write(sb.ToString());
        Response.End();

    }

    //发送订阅邮件
    public void Supplier_Sysemail_Send()
    {

        //取得上一页参数
        string sysmail_title, sysmail_content, supplier_id, supplier_arry, supplier_email;

        sysmail_title = Request.Form["sysmail_title"];
        sysmail_content = Request.Form["sysmail_content"];
        supplier_id = Request.Form["Supplier_Message_SupplierID"];

        supplier_email = "";
        supplier_arry = "";

        //处理参数
        int page = 0;

        int ii = 0;
        page = tools.CheckInt(Request["page"]);
        SupplierInfo entity;


        //发送Email
        if (supplier_id.Length > 0)
        {
            foreach (string subid in supplier_id.Split(','))
            {
                if (tools.CheckInt(subid) > 0)
                {
                    entity = GetSupplierByID(tools.CheckInt(subid));
                    if (entity != null)
                    {
                        if (supplier_email != "")
                        {
                            if (supplier_arry == "")
                            {
                                supplier_arry = subid;
                            }
                            else
                            {
                                supplier_arry = supplier_arry + "," + subid;
                            }
                        }
                        if (supplier_arry == "")
                        {
                            supplier_email = entity.Supplier_Email;
                        }

                    }

                }
            }
        }

        if (supplier_email.Length > 0)
        {
            Sendmail(supplier_email, sysmail_title, sysmail_title, sysmail_content);
        }
        //FORM重复提交
        StringBuilder sb = new StringBuilder();

        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<title>管理平台</title>");
        sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
        sb.Append("<link rel=\"stylesheet\" href=\"/css/style.css\" type=\"text/css\">");
        sb.Append("</head>");

        if (supplier_id != "")
        {
            supplier_id = supplier_arry;

            sb.Append("<body style=\"margin:10px;\" onload=\"document.form1.submit();\">");
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">");
            sb.Append("  <tr> ");
            sb.Append("    <td height=\"25\" class=\"content_title\">邮件发送中……</td>");
            sb.Append("  </tr>");
            sb.Append("  <tr> ");
            sb.Append("    <td height=\"30\" class=\"t14red\">");
            sb.Append("\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">");
            sb.Append("\t  <tr> ");
            sb.Append("        <td width=\"60\" height=\"60\"></td>");
            sb.Append("        <td width=\"60\"><img src=\"/images/loading.gif\"></td>");
            sb.Append("\t\t<td align=\"left\" class=\"t14_red\">邮件发送中，请不要关闭窗口……" + supplier_email + "</td>");
            sb.Append("\t  </tr>");
            sb.Append("\t</table>");
            sb.Append("\t</td>");
            sb.Append("  </tr>");
            sb.Append("</table>");
            sb.Append("<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">");
            sb.Append("  <form name=\"form1\" method=\"post\" action=\"?\">");
            sb.Append("\t<tr>");
            sb.Append("\t  <td>");
            sb.Append("\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>");
            sb.Append("\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >");
            sb.Append("\t <input name=\"Supplier_Message_SupplierID\" type=\"hidden\" id=\"Supplier_Message_SupplierID\" value=\"" + supplier_id + "\" >");
            sb.Append("\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >");
            sb.Append("\t  </td>");
            sb.Append("\t</tr>");
            sb.Append("  </form>");
            sb.Append("</table>");
        }
        else
        {
            sb.Append("<body style=\"margin:10px;\">");
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">");
            sb.Append("  <tr> ");
            sb.Append("    <td height=\"25\" class=\"content_title\">管理平台</td>");
            sb.Append("  </tr>");
            sb.Append("  <tr> ");
            sb.Append("    <td height=\"30\" class=\"t14red\">");
            sb.Append("\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">");
            sb.Append("\t  <tr> ");
            sb.Append("        <td width=\"60\" height=\"60\"></td>");
            sb.Append("        <td width=\"60\"><img src=\"/images/icon_alert_b.gif\" width=\"50\" height=\"50\"></td>");
            sb.Append("\t\t<td align=\"left\" class=\"t14_red\">邮件发送成功！</td>");
            sb.Append("\t  </tr>");
            sb.Append("\t</table>");
            sb.Append("\t</td>");
            sb.Append("  </tr>");
            sb.Append("</table>");

        }
        sb.Append("</body>");
        sb.Append("</html>");
        Response.Write(sb.ToString());

    }

    //邮件发送处理过程
    public int Sendmail(string mailto, string mailsubject, string mailbodytitle, string mailbody)
    {

        //-------------------------------------定义邮件设置---------------------------------
        int mformat = 0;

        //-------------------------------------定义邮件模版---------------------------------
        string MailBody_Temp = null;
        MailBody_Temp = "";
        MailBody_Temp = MailBody_Temp + "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" /></head>";
        MailBody_Temp = MailBody_Temp + "<body>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailHeader><SPAN class=MailBody_title>{MailBody_title}</SPAN></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailContent>";
        MailBody_Temp = MailBody_Temp + "{MailBody_content}";
        MailBody_Temp = MailBody_Temp + "<p><br><B>{sys_config_site_name}</B><br>欲了解更多信息，请访问<a href='{sys_config_site_url}'>{sys_config_site_url}</a> 或致电{sys_config_site_tel}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailFooter><P class=comments>&copy; {sys_config_site_name}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<style type=text/css>";
        MailBody_Temp = MailBody_Temp + "P {FONT-SIZE: 14px; MARGIN: 10px 0px 5px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "td {FONT-SIZE: 12px; LINE-HEIGHT: 150%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "BODY {BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif }";
        MailBody_Temp = MailBody_Temp + "UL {MARGIN-TOP: 0px; FONT-SIZE: 14px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + ".comments {FONT-SIZE: 12px; MARGIN: 0px; COLOR: gray; LINE-HEIGHT: 130%}";
        MailBody_Temp = MailBody_Temp + ".mailHeader {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 10px; COLOR: #003366; PADDING-TOP: 10px; BORDER-BOTTOM: #7a8995 1px solid; BACKGROUND-COLOR: #ebebeb}";
        MailBody_Temp = MailBody_Temp + ".mailContent {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 23px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".mailFooter {PADDING-RIGHT: 23px; BORDER-TOP: #bbbbbb 1px solid; PADDING-LEFT: 23px; PADDING-BOTTOM: 11px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".MailBody_title {  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: bold; color: #009900}";
        MailBody_Temp = MailBody_Temp + "A:visited { COLOR: #105bac} A:hover { COLOR: orange} .img_border { border: 1px solid #E5E5E5}";
        MailBody_Temp = MailBody_Temp + ".highLight { BACKGROUND-COLOR: #FFFFCC; PADDING: 15px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif}</style>";
        MailBody_Temp = MailBody_Temp + "</body><html>";

        //------------------------------------开始发送过程------------------------------------
        string body = "";
        switch (mformat)
        {
            case 0:
                //HTML格式
                body = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" />" + MailBody_Temp;
                body = body.Replace("{MailBody_title}", mailbodytitle);
                body = body.Replace("{MailBody_content}", mailbody);
                break;
            case 1:
                //纯文本格式
                body = mailbody;
                break;
        }

        body = replace_sys_config(body);

        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            mail.From = Application["Mail_From"].ToString();
            mail.Replyto = Application["Mail_Replyto"].ToString();
            mail.FromName = Application["Mail_FromName"].ToString();
            mail.Server = Application["Mail_Server"].ToString();
            //邮件格式 0=支持HTML,1=纯文本
            mail.ServerUsername = Application["Mail_ServerUserName"].ToString(); ;
            mail.ServerPassword = Application["Mail_ServerPassWord"].ToString();
            mail.ServerPort = tools.CheckInt(Application["Mail_ServerPort"].ToString());
            if (tools.CheckInt(Application["Mail_EnableSsl"].ToString()) == 0)
            {
                mail.EnableSsl = false;
            }
            else
            {
                mail.EnableSsl = true;
            }
            mail.Encode = Application["Mail_Encode"].ToString();

            if (mail.SendEmail(mailto, mailsubject, body))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
        }



    }

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string functionReturnValue;
        functionReturnValue = replacestr;
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return functionReturnValue;
    }

    #endregion

    #region 采购分类


    public void AddSupplierPurchaseCategory()
    {
        int Cate_ParentID = tools.CheckInt(Request.Form["Cate_ParentID"]);
        string Cate_Name = tools.CheckStr(Request.Form["Cate_Name"]);
        int Cate_Sort = tools.CheckInt(Request.Form["Cate_Sort"]);
        int Cate_IsActive = tools.CheckInt(Request.Form["Cate_IsActive"]);
        string Cate_Site = Public.GetCurrentSite();

        if (Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称！", false, "{back}");
        }

        SupplierPurchaseCategoryInfo entity = new SupplierPurchaseCategoryInfo();
        entity.Cate_ParentID = Cate_ParentID;
        entity.Cate_Name = Cate_Name;
        entity.Cate_Sort = Cate_Sort;
        entity.Cate_IsActive = Cate_IsActive;
        entity.Cate_Site = Cate_Site;

        if (MyPurchaseCategory.AddSupplierPurchaseCategory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "category_add.aspx?parent_id=" + entity.Cate_ParentID);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditSupplierPurchaseCategory()
    {
        int Cate_ID = tools.CheckInt(Request.Form["Cate_ID"]);
        int Cate_ParentID = tools.CheckInt(Request.Form["Cate_ParentID"]);
        string Cate_Name = tools.CheckStr(Request.Form["Cate_Name"]);
        int Cate_Sort = tools.CheckInt(Request.Form["Cate_Sort"]);
        int Cate_IsActive = tools.CheckInt(Request.Form["Cate_IsActive"]);


        if (Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称！", false, "{back}");
        }

        SupplierPurchaseCategoryInfo entity = MyPurchaseCategory.GetSupplierPurchaseCategoryByID(Cate_ID, Public.GetUserPrivilege());
        if (entity != null)
        {
            entity.Cate_ParentID = Cate_ParentID;
            entity.Cate_Name = Cate_Name;
            entity.Cate_Sort = Cate_Sort;
            entity.Cate_IsActive = Cate_IsActive;

            if (MyPurchaseCategory.EditSupplierPurchaseCategory(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "category.aspx?cate_parentid=" + Cate_ParentID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelSupplierPurchaseCategory()
    {
        int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);

        SupplierPurchaseCategoryInfo entity = GetSupplierPurchaseCategoryByID(cate_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "该类别不存在", false, "{back}");
        }
        else
        {
            if (MyPurchaseCategory.DelSupplierPurchaseCategory(cate_id, Public.GetUserPrivilege()) > 0)
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "category.aspx?cate_parentid=" + entity.Cate_ParentID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }


    public string GetSupplierPurchaseCategorys()
    {

        int Cate_ParentID = tools.CheckInt(Request.QueryString["cate_parentid"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseCategoryInfo.Cate_Name", "like", keyword));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseCategoryInfo.Cate_ParentID", "=", Cate_ParentID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseCategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyPurchaseCategory.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<SupplierPurchaseCategoryInfo> Categorys = MyPurchaseCategory.GetSupplierPurchaseCategorys(Query, Public.GetUserPrivilege());
        if (Categorys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierPurchaseCategoryInfo entity in Categorys)
            {
                jsonBuilder.Append("{\"SupplierPurchaseCategoryInfo.Cate_ID\":" + entity.Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href='category.aspx?cate_parentid=" + entity.Cate_ID + "'>" + Public.JsonStr(entity.Cate_Name) + "</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetSupplierPurchaseSubCateAmount(entity.Cate_ID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("0bdbc767-9db5-4b38-ace4-6f886ddc285e"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"category_edit.aspx?cate_id=" + entity.Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("616c185b-5b10-4bb1-b9f8-2112cf41ae6f"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('category_do.aspx?action=move&cate_id=" + entity.Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }

    }

    public SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int cate_id)
    {
        return MyPurchaseCategory.GetSupplierPurchaseCategoryByID(cate_id, Public.GetUserPrivilege());
    }

    public int GetSupplierPurchaseSubCateAmount(int Cate_ParentID)
    {
        int Amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseCategoryInfo.Cate_ParentID", "=", Cate_ParentID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseCategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));

        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseCategoryInfo.Cate_ID", "asc"));

        IList<SupplierPurchaseCategoryInfo> Categorys = MyPurchaseCategory.GetSupplierPurchaseCategorys(Query, Public.GetUserPrivilege());
        if (Categorys != null)
        {
            Amount = Categorys.Count;
        }
        return Amount;
    }


    public string SelectSupplierPurchaseCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID)
    {
        return MyPurchaseCategory.SelectSupplierPurchaseCategoryOption(Cate_ID, selectVal, appendTag, shieldID, Public.GetCurrentSite(), Public.GetUserPrivilege());
    }

    public string SupplierPurchaseCategoryRecursion(int cate_id)
    {
        string strCateRecursion = MyPurchaseCategory.DisplaySupplierPurchaseCategoryRecursion(cate_id, "/supplier/category.aspx?cate_parentid={cate_id}", Public.GetUserPrivilege());

        return strCateRecursion;
    }





    #endregion

    #region 代理采购协议

    public SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID)
    {
        return MyAgentProtocal.GetSupplierAgentProtocalByID(ID, Public.GetUserPrivilege());
    }


    //生成代理采购协议编号
    public string Create_AgentProtocal_Code(int PurchaseID)
    {
        string sn = "XY-ZGSB-" + tools.FormatDate(DateTime.Now, "yyyy-MM") + "-";
        string sub_sn = "";
        sub_sn = "00000" + (PurchaseID).ToString();
        sub_sn = sub_sn.Substring(sub_sn.Length - 5);
        sn = sn + sub_sn;
        return sn;
    }



    public void AddSupplierAgentProtocal()
    {
        int PurchaseID = tools.CheckInt(Request["PurchaseID"]);
        int supplier_id = 0;


        SupplierAgentProtocalInfo agentprotocal = GetSupplierAgentProtocalByPurchaseID(PurchaseID);
        if (agentprotocal != null)
        {
            Public.Msg("error", "错误信息", "该采购申请已存在协议", false, "Agent_Protocal_list.aspx");
            Response.End();
        }

        SupplierPurchaseInfo purchaseinfo = GetSupplierPurchaseByID(PurchaseID);

        if (purchaseinfo != null)
        {
            supplier_id = purchaseinfo.Purchase_SupplierID;
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "Agent_Protocal_list.aspx");
        }

        string Protocal_Template = Request["Protocal_Template"];

        SupplierAgentProtocalInfo entity = new SupplierAgentProtocalInfo();
        entity.Protocal_Code = Create_AgentProtocal_Code(PurchaseID);
        entity.Protocal_PurchaseID = PurchaseID;
        entity.Protocal_SupplierID = supplier_id;
        entity.Protocal_Template = Protocal_Template;
        entity.Protocal_Addtime = DateTime.Now;
        entity.Protocal_Status = 0;
        entity.Protocal_Site = Public.GetCurrentSite();

        if (MyAgentProtocal.AddSupplierAgentProtocal(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Purchase_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditSupplierAgentProtocal(int Protocal_Status)
    {

        int Protocal_ID = tools.CheckInt(Request["Protocal_ID"]);
        string Protocal_Template = Request["Protocal_Template"];

        SupplierAgentProtocalInfo entity = GetSupplierAgentProtocalByID(Protocal_ID);
        if (entity != null)
        {
            if (Protocal_Status == 0 && entity.Protocal_Status == 0)
            {
                if (Protocal_Template == entity.Protocal_Template)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Agent_Protocal_list.aspx");
                }
                else
                {
                    entity.Protocal_Template = Protocal_Template;
                }
            }
            if (Protocal_Status == 2 && entity.Protocal_Status == 1)
            {
                entity.Protocal_Status = 2;
            }
            if (MyAgentProtocal.EditSupplierAgentProtocal(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Agent_Protocal_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }


    }




    //代理协议列表
    public string GetSupplierAgentProtocal_list()
    {
        QueryInfo Query = new QueryInfo();
        int audit = tools.CheckInt(Request["audit"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        int Protocal_Status = tools.CheckInt(Request["audit"]);


        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAgentProtocalInfo.Protocal_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAgentProtocalInfo.Protocal_PurchaseID", "in", "select Purchase_ID from Supplier_Purchase where Purchase_Title like '%" + keyword + "%'"));
        }

        switch (Protocal_Status)
        {

            case 1:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "0"));
                break;
            case 2:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "1"));
                break;
            case 3:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "2"));
                break;

        }


        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));


        IList<SupplierAgentProtocalInfo> entitys = MyAgentProtocal.GetSupplierAgentProtocals(Query, Public.GetUserPrivilege());
        PageInfo pageinfo = MyAgentProtocal.GetPageInfo(Query, Public.GetUserPrivilege());

        SupplierInfo supplierinfo;

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierAgentProtocalInfo entity in entitys)
            {
                supplierinfo = null;
                if (entity.Protocal_SupplierID > 0)
                {
                    supplierinfo = MyBLL.GetSupplierByID(entity.Protocal_SupplierID, Public.GetUserPrivilege());
                }

                jsonBuilder.Append("{\"id\":" + entity.Protocal_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Protocal_ID);
                jsonBuilder.Append("\",");

                SupplierPurchaseInfo purchaseinfo = MyPurchase.GetSupplierPurchaseByID(entity.Protocal_PurchaseID, Public.GetUserPrivilege());
                if (purchaseinfo != null)
                {

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(purchaseinfo.Purchase_Title));
                    jsonBuilder.Append("\",");

                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(" -- ");
                    jsonBuilder.Append("\",");
                }

                if (entity.Protocal_SupplierID > 0)
                {
                    if (supplierinfo != null)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                        jsonBuilder.Append("\",");
                    }
                    else
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append("--");
                        jsonBuilder.Append("\",");
                    }
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("易耐产业金服");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(ConvertAgentProtocalStatus(entity.Protocal_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Protocal_Addtime.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"  alt=\\\"查看\\\"> <a href=\\\"Agent_Protocal_view.aspx?Protocal_ID=" + entity.Protocal_ID + "\\\" title=\\\"查看\\\">查看</a> ");
                }
                if (Public.CheckPrivilege("7abc095a-d322-4312-861c-aecb6088c3bb") && entity.Protocal_Status == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_view.gif\\\"  alt=\\\"修改\\\"> <a href=\\\"Agent_Protocal_edit.aspx?Protocal_ID=" + entity.Protocal_ID + "\\\" title=\\\"修改\\\">修改</a> ");
                }

                //if (Public.CheckPrivilege("03ea4ec5-4a1d-4e02-9b91-4cca4e3b4200") && entity.Protocal_Status == 1)
                //{
                //    jsonBuilder.Append(" <img src=\\\"/images/icon_view.gif\\\"  alt=\\\"审核\\\"> <a href=\\\"Agent_Protocal_edit.aspx?Protocal_ID=" + entity.Protocal_ID + "\\\" title=\\\"审核\\\">审核</a> ");
                //}

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string ConvertAgentProtocalStatus(int status)
    {
        switch (status)
        {
            case 0:
                return "待确认";
            case 1:
                return "已确认";
            case 2:
                return "已完成";
            default:
                return " -- ";
        }
    }

    #endregion

    #region  保证金管理

    public virtual void AddSupplierMargin()
    {
        int Supplier_Margin_ID = tools.CheckInt(Request.Form["Supplier_Margin_ID"]);
        int Supplier_Margin_Type = tools.CheckInt(Request.Form["Supplier_Cert_Type"]);
        double Supplier_Margin_Amount = tools.CheckFloat(Request.Form["Supplier_Margin_Amount"]);
        string Supplier_Margin_Site = Public.GetCurrentSite();

        if (GetSupplierMarginByTypeID(Supplier_Margin_Type) != null)
        {
            Public.Msg("info", "提示信息", "该类型已添加保证金标准，请勿重复添加", false, "{back}");
        }

        SupplierMarginInfo entity = new SupplierMarginInfo();
        entity.Supplier_Margin_ID = Supplier_Margin_ID;
        entity.Supplier_Margin_Type = Supplier_Margin_Type;
        entity.Supplier_Margin_Amount = Supplier_Margin_Amount;
        entity.Supplier_Margin_Site = Supplier_Margin_Site;

        if (MyMargin.AddSupplierMargin(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Margin_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSupplierMargin()
    {

        int Supplier_Margin_ID = tools.CheckInt(Request.Form["Supplier_Margin_ID"]);
        int Supplier_Margin_Type = tools.CheckInt(Request.Form["Supplier_Cert_Type"]);
        double Supplier_Margin_Amount = tools.CheckFloat(Request.Form["Supplier_Margin_Amount"]);
        string Supplier_Margin_Site = Public.GetCurrentSite();

        SupplierMarginInfo entity = GetSupplierMarginByID(Supplier_Margin_ID);
        if (entity != null)
        {
            entity.Supplier_Margin_Amount = Supplier_Margin_Amount;

            if (MyMargin.EditSupplierMargin(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Margin_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelSupplierMargin()
    {
        int Supplier_Margin_ID = tools.CheckInt(Request.QueryString["Supplier_Margin_ID"]);
        if (MyMargin.DelSupplierMargin(Supplier_Margin_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Margin_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SupplierMarginInfo GetSupplierMarginByID(int ID)
    {
        return MyMargin.GetSupplierMarginByID(ID);
    }

    public virtual SupplierMarginInfo GetSupplierMarginByTypeID(int Type_ID)
    {
        return MyMargin.GetSupplierMarginByTypeID(Type_ID);
    }

    public string GetSupplierMargins()
    {
        QueryInfo Query = new QueryInfo();

        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMarginInfo.Supplier_Margin_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyMargin.GetPageInfo(Query);
        IList<SupplierMarginInfo> entitys = MyMargin.GetSupplierMargins(Query);

        if (entitys != null)
        {
            SupplierCertTypeInfo certTypeInfo = null;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierMarginInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Supplier_Margin_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Margin_ID);
                jsonBuilder.Append("\",");

                certTypeInfo = GetSupplierCertTypeByID(entity.Supplier_Margin_Type);
                jsonBuilder.Append("\"");
                if (certTypeInfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(certTypeInfo.Cert_Type_Name));
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Supplier_Margin_Amount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("78f37fa4-ba54-486e-9159-f3050560999d"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Supplier_Margin_Edit.aspx?Supplier_Margin_ID=" + entity.Supplier_Margin_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("d5a64ee0-f232-474e-9074-1e2dc396c2a3"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_Margin_do.aspx?action=move&move=" + entity.Supplier_Margin_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    #endregion



    public string GetSuppliersSignUpInfo()
    {
        SQLHelper DBHelper = new SQLHelper();
        int Bid_Up_ID;
        string Bid_Up_ContractMan, Bid_Up_ContractMobile, Bid_Up_ContractContent;

        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;
        //if (keyword.Length > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "FeedBackInfo.Feedback_Name", "like", keyword));
        //    Query.ParamInfos.Add(new ParamInfo("OR", "str", "FeedBackInfo.Feedback_CompanyName", "like", keyword));
        //    Query.ParamInfos.Add(new ParamInfo("OR", "str", "FeedBackInfo.Feedback_Tel", "like", keyword));
        //}
        string SqlField = "Bid_Up_ID, Bid_Up_ContractMan, Bid_Up_ContractContent,Bid_Up_ContractMobile,Bid_Up_AddTime";
        string SqlTable = "Bid_Up_Require_Quick";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);
        string SqlParam = "";
        if (keyword.Length > 0)
        {
            if ((date_start.Length > 0) && (date_end.Length > 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0 and (Bid_Up_ContractMan like '%" + keyword + "%'or Bid_Up_ContractMobile like  '%" + keyword + "%')  and (DATEDIFF(d, '" + date_start + "',Bid_Up_AddTime)>=0 and DATEDIFF(d, '" + date_end + "',Bid_Up_AddTime)<=0)";
            }
            else if ((date_start.Length < 0) && (date_end.Length > 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0 and (Bid_Up_ContractMan like '%" + keyword + "%'or Bid_Up_ContractMobile like  '%" + keyword + "%')  and DATEDIFF(d, '" + date_end + "',Bid_Up_AddTime)<=0)";
            }
            else if ((date_start.Length > 0) && (date_end.Length < 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0 and (Bid_Up_ContractMan like '%" + keyword + "%'or Bid_Up_ContractMobile like  '%" + keyword + "%')  and (DATEDIFF(d, '" + date_start + "',Bid_Up_AddTime)>=0 ";
            }
            else
            {
                SqlParam = " WHERE Bid_Up_ID > 0 and (Bid_Up_ContractMan like '%" + keyword + "%'or Bid_Up_ContractMobile like  '%" + keyword + "%')  ";
            }

        }
        else
        {
            if ((date_start.Length > 0) && (date_end.Length > 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0  and (DATEDIFF(d, '" + date_start + "',Bid_Up_AddTime)>=0 and DATEDIFF(d, '" + date_end + "',Bid_Up_AddTime)<=0)";
            }
            else if ((date_start.Length < 0) && (date_end.Length > 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0 and DATEDIFF(d, '" + date_end + "',Bid_Up_AddTime)<=0)";
            }
            else if ((date_start.Length > 0) && (date_end.Length < 0))
            {
                SqlParam = " WHERE Bid_Up_ID > 0  and (DATEDIFF(d, '" + date_start + "',Bid_Up_AddTime)>=0 ";
            }
            else
            {
                SqlParam = " WHERE Bid_Up_ID > 0";
            }
        }
        //if (date_start != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{FeedBackInfo.Feedback_Addtime})", ">=", "0"));
        //}
        //if (date_end != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{FeedBackInfo.Feedback_Addtime})", "<=", "0"));
        //}

        string SqlCount = "SELECT COUNT(Bid_Up_ID) FROM " + SqlTable + " " + SqlParam;

        SqlDataReader RdrList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            RdrList = DBHelper.ExecuteReader(SqlList);

            while (RdrList.Read())
            {
                Bid_Up_ID = tools.NullInt(RdrList["Bid_Up_ID"]);
                Bid_Up_ContractMan = tools.NullStr(RdrList["Bid_Up_ContractMan"]);
                Bid_Up_ContractMobile = tools.NullStr(RdrList["Bid_Up_ContractMobile"]);
                Bid_Up_ContractContent = tools.NullStr(RdrList["Bid_Up_ContractContent"]);

                DateTime Bid_Up_AddTime = tools.NullDate(RdrList["Bid_Up_AddTime"]);
                jsonBuilder.Append("{\"Bid_Up_ID\":" + Bid_Up_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Bid_Up_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Bid_Up_ContractMan));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Bid_Up_ContractMobile));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Bid_Up_AddTime.ToString("yyyy-MM-dd")));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(Supplier_Contact));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                //if (Public.CheckPrivilege("3306c908-ff91-4e6e-8c46-8157cd5b6e4a"))
                //{
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"Signup_view.aspx?signup_id=" + Bid_Up_ID + "\\\" title=\\\"查看\\\">查看</a>");
                //}
                //if (Public.CheckPrivilege("21de0894-85a2-4719-8088-774f1a815f43"))
                //{
                //    //jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('supplier_do.aspx?action=move&supplier_id=" + Supplier_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                //}

                jsonBuilder.Append("\"");

                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (RdrList != null)
            {
                RdrList.Close();
                RdrList = null;
            }
        }
    }
}

