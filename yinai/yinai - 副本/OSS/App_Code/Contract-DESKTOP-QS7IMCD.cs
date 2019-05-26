using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
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
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
/// 合同管理类
/// </summary>
public class Contract
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private OrdersLog orderlog = new OrdersLog();
    private IContractTemplate MyTemplate;
    private IContract MyBLL;
    private ISupplier MySupplier;
    private IOrders MyOrders;
    private IContractLog Mycontractlog;
    private IPayWay MyPayway;
    private IDeliveryWay MyDelivery;
    private Addr addr;
    private IContractDividedPayment MyDividedpay;
    private IContractPayment MyPayment;
    private IContractDelivery MyFreight;
    private IProduct MyProduct;
    private Member member;
    private Orders orders;
    private ToUcase MyUcase = new ToUcase();
    private Sys sys;
    private ISQLHelper DBHelper;

    public Contract()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyTemplate = ContractTemplateFactory.CreateContractTemplate();
        MyBLL = ContractFactory.CreateContract();
        MySupplier = SupplierFactory.CreateSupplier();
        MyOrders = OrdersFactory.CreateOrders();
        Mycontractlog = ContractLogFactory.CreateContractLog();
        MyPayway = PayWayFactory.CreatePayWay();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        addr = new Addr();
        MyDividedpay = ContractDividedPaymentFactory.CreateContractDividedPayment();
        MyPayment = ContractPaymentFactory.CreateContractPayment();
        MyFreight = ContractDeliveryFactory.CreateContractDelivery();
        MyProduct = ProductFactory.CreateProduct();
        sys = new Sys();
        orders = new Orders();
        member = new Member();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
    }

    #region 合同模板
    //合同模版
    public void AddContractTemplate()
    {
        string Contract_Template_Name = tools.CheckStr(Request.Form["Contract_Template_Name"]);
        string Contract_Template_Code = tools.CheckStr(Request.Form["Contract_Template_Code"]);
        string Contract_Template_Content = Request.Form["Contract_Template_Content"];

        if (Contract_Template_Name.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板名称", false, "{back}"); return; }
        if (Contract_Template_Code.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板标识", false, "{back}"); return; }
        if (Contract_Template_Content.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板内容", false, "{back}"); return; }

        ContractTemplateInfo entity = new ContractTemplateInfo();
        entity.Contract_Template_ID = 0;
        entity.Contract_Template_Name = Contract_Template_Name;
        entity.Contract_Template_Code = Contract_Template_Code;
        entity.Contract_Template_Content = Contract_Template_Content;
        entity.Contract_Template_Site = Public.GetCurrentSite();
        entity.Contract_Template_Addtime = DateTime.Now;

        if (MyTemplate.AddContractTemplate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_Template.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditContractTemplate()
    {
        int Contract_Template_ID = tools.CheckInt(Request.Form["Contract_Template_ID"]);
        string Contract_Template_Name = tools.CheckStr(Request.Form["Contract_Template_Name"]);
        string Contract_Template_Code = tools.CheckStr(Request.Form["Contract_Template_Code"]);
        string Contract_Template_Content = Request.Form["Contract_Template_Content"];

        if (Contract_Template_Name.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板名称", false, "{back}"); return; }
        if (Contract_Template_Code.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板标识", false, "{back}"); return; }
        if (Contract_Template_Content.Length == 0)
        { Public.Msg("error", "错误信息", "请填写模板内容", false, "{back}"); return; }

        ContractTemplateInfo entity = new ContractTemplateInfo();
        entity.Contract_Template_ID = Contract_Template_ID;
        entity.Contract_Template_Name = Contract_Template_Name;
        entity.Contract_Template_Code = Contract_Template_Code;
        entity.Contract_Template_Content = Contract_Template_Content;
        entity.Contract_Template_Site = Public.GetCurrentSite();
        entity.Contract_Template_Addtime = DateTime.Now;

        if (MyTemplate.EditContractTemplate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_Template.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelContractTemplate()
    {
        int Contract_Template_ID = tools.CheckInt(Request.Form["Template_ID"]);
        if (MyTemplate.DelContractTemplate(Contract_Template_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_Template.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetContractTemplates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractTemplateInfo.Contract_Template_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyTemplate.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ContractTemplateInfo> entitys = MyTemplate.GetContractTemplates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ContractTemplateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ContractTemplateInfo.Contract_Template_ID\":" + entity.Contract_Template_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Template_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Contract_Template_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Contract_Template_Code));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Template_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("54d1aadd-ca23-4c1a-b56e-793543423a39"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"编辑\\\"> <a href=\\\"contract_template_edit.aspx?template_id=" + entity.Contract_Template_ID + "\\\" title=\\\"编辑\\\">编辑</a>");
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

    public string DisplayContractTemplate(int Template_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractTemplateInfo.Contract_Template_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ContractTemplateInfo.Contract_Template_ID", "asc"));
        IList<ContractTemplateInfo> entitys = MyTemplate.GetContractTemplates(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            foreach (ContractTemplateInfo entity in entitys)
            {
                if (Template_ID == entity.Contract_Template_ID)
                {
                    strHTML.Append("<option value=\"" + entity.Contract_Template_ID + "\" selected>" + entity.Contract_Template_Name + "</option>");
                }
                else
                {
                    strHTML.Append("<option value=\"" + entity.Contract_Template_ID + "\">" + entity.Contract_Template_Name + "</option>");
                }
            }
        }

        return strHTML.ToString();
    }

    public ContractTemplateInfo GetContractTemplateByID(int ID)
    {
        return MyTemplate.GetContractTemplateByID(ID, Public.GetUserPrivilege());
    }

    public ContractTemplateInfo GetContractTemplateBySign(string Sign)
    {
        return MyTemplate.GetContractTemplateBySign(Sign, Public.GetUserPrivilege());
    }

    #endregion

    #region 合同状态
    public string ContractType(int typeid)
    {
        string resultstr = string.Empty;
        switch (typeid)
        {
            case 1:
                resultstr = "现货采购合同"; break;
            case 2:
                resultstr = "定制采购合同"; break;
            case 3:
                resultstr = "代理采购合同"; break;
        }
        return resultstr;
    }

    //获取合同确认状态
    public string ContractConfirmStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "双方未确认"; break;
            case 1:
                resultstr = "用户已确认"; break;
            case 2:
                resultstr = "平台已确认"; break;
        }

        return resultstr;
    }

    public string ContractStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "意向合同"; break;
            case 1:
                resultstr = "履行中合同"; break;
            case 2:
                resultstr = "交易成功合同"; break;
            case 3:
                resultstr = "交易失败合同"; break;
        }

        return resultstr;
    }

    public string ContractPaymentStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未支付"; break;
            case 1:
                resultstr = "部分支付"; break;
            case 2:
                resultstr = "全部支付"; break;
            case 3:
                resultstr = "全部到账"; break;
        }

        return resultstr;
    }

    public string ContractDeliveryStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未发货"; break;
            case 1:
                resultstr = "配货中"; break;
            case 2:
                resultstr = "部分发货"; break;
            case 3:
                resultstr = "全部发货"; break;
            case 4:
                resultstr = "全部签收"; break;
        }

        return resultstr;
    }

    public string ContractDividedPaymentStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未支付"; break;
            case 1:
                resultstr = "已支付"; break;
            case 2:
                resultstr = "已到帐"; break;
        }

        return resultstr;
    }

    //发票申请状态
    public string ContractInvoiceApplyStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未开票"; break;
            case 1:
                resultstr = "已开票"; break;
            case 2:
                resultstr = "已邮寄"; break;
            case 3:
                resultstr = "已收票"; break;
            case 4:
                resultstr = "已取消"; break;
        }

        return resultstr;
    }

    //付款单支付状态
    public string ContractPaymentsStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未支付"; break;
            case 1:
                resultstr = "已支付"; break;
            case 2:
                resultstr = "已到帐"; break;
        }

        return resultstr;
    }

    //配送单配送状态
    public string ContractDeliverysStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未发货"; break;
            case 1:
                resultstr = "已发货"; break;
            case 2:
                resultstr = "已签收"; break;
        }

        return resultstr;
    }
    #endregion

    #region 合同管理

    //支付方式选择
    public string Payway_Select(string select_name, int payway)
    {
        string way_list = "";
        way_list = "<select name=\"" + select_name + "\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        IList<PayWayInfo> payways = MyPayway.GetPayWays(Query, Public.GetUserPrivilege());
        if (payways != null)
        {
            foreach (PayWayInfo entity in payways)
            {
                if (payway == entity.Pay_Way_ID)
                {
                    way_list = way_list + "  <option value=\"" + entity.Pay_Way_ID + "\" selected>" + entity.Pay_Way_Name + "</option>";
                }
                else
                {
                    way_list = way_list + "  <option value=\"" + entity.Pay_Way_ID + "\">" + entity.Pay_Way_Name + "</option>";
                }
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    //根据合同编号获取合同信息
    public ContractInfo GetContractByID(int ID)
    {
        return MyBLL.GetContractByID(ID, Public.GetUserPrivilege());
    }

    //根据合同编号获取合同信息
    public ContractInfo GetContractBySn(string SN)
    {
        return MyBLL.GetContractBySn(SN, Public.GetUserPrivilege());
    }

    //根据合同编号获取合同附件订单信息
    public IList<OrdersInfo> GetOrderssByContractID(int Contract_ID)
    {
        return MyOrders.GetOrderssByContractID(Contract_ID);
    }

    public ContractDeliveryInfo GetContractDeliveryByID(int ID)
    {
        return MyFreight.GetContractDeliveryByID(ID);
    }

    public ContractPaymentInfo GetContractPaymentByID(int ID)
    {
        return MyPayment.GetContractPaymentByID(ID);
    }

    //意向合同选择
    public string TmpContract_Select(int Contract_Type)
    {
        StringBuilder Html_Str = new StringBuilder();
        Html_Str.Append("<select name=\"Contract_ID\">");
        Html_Str.Append("<option value=\"0\">选择意向合同</option>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Source", "=", "1"));
        if (Contract_Type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Type", "=", Contract_Type.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Confirm_Status", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("ContractInfo.Contract_ID", "DESC"));
        IList<ContractInfo> entitys = MyBLL.GetContracts(Query,Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ContractInfo entity in entitys)
            {
                Html_Str.Append("<option value=\"" + entity.Contract_ID + "\">" + entity.Contract_SN + "</option>");
            }
        }
        Html_Str.Append("</select>");
        return Html_Str.ToString();
    }

    //获取配送方式
    public string GetDeliveryName(int deliveryid)
    {
        DeliveryWayInfo entity = MyDelivery.GetDeliveryWayByID(deliveryid, Public.GetUserPrivilege());
        if (entity != null)
        {
            return entity.Delivery_Way_Name;
        }
        return "";
    }

    //合同管理
    public string GetContracts()
    {
        int Contract_Status = tools.CheckInt(Request["status"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request.QueryString["keyword"]);
        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        int deliverystatus = tools.CheckInt(Request["deliverystatus"]);
        int paymentstatus = tools.CheckInt(Request["paymentstatus"]);
        int contracttype = tools.CheckInt(Request["contracttype"]);
        int confirm = tools.CheckInt(Request["confirm"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_Site", "=", Public.GetCurrentSite()));
        if (confirm > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Confirm_Status", "=", (confirm-1).ToString()));
        }
        if (contracttype > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Type", "=", (contracttype).ToString()));
        }
        if (Contract_Status == -1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_Status", "=", "0"));
        }
        if (Contract_Status > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_Status", "=", Contract_Status.ToString()));
        }
        if (deliverystatus > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_Delivery_Status", "=", (deliverystatus - 1).ToString()));
        }
        if (paymentstatus > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_Payment_Status", "=", (paymentstatus - 1).ToString()));
        }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ContractInfo.Contract_SN", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ContractInfo.Contract_BuyerID", "in", "Select supplier_id from supplier where supplier_companyname like '%" + keyword + "%' Or supplier_nickname like '%" + keyword + "%'"));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ContractInfo.Contract_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ContractInfo.Contract_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        }



        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo = null;
        SupplierInfo buyerinfo = null;

        IList<ContractInfo> entitys = MyBLL.GetContracts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ContractInfo entity in entitys)
            {
                buyerinfo = MySupplier.GetSupplierByID(entity.Contract_BuyerID, Public.GetUserPrivilege());
                supplierinfo = null;
                if (entity.Contract_SupplierID > 0)
                {
                    supplierinfo = MySupplier.GetSupplierByID(entity.Contract_SupplierID, Public.GetUserPrivilege());
                }
                IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
                jsonBuilder.Append("{\"ContractInfo.Contract_ID\":" + entity.Contract_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_SN);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(ContractType(entity.Contract_Type));
                jsonBuilder.Append("\",");

               

                jsonBuilder.Append("\"");
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("" + Public.JsonStr(supplierinfo.Supplier_CompanyName) + "</a>");
                }
                else
                {
                    jsonBuilder.Append("易耐产业金服");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (buyerinfo != null)
                {
                    jsonBuilder.Append("" + Public.JsonStr(buyerinfo.Supplier_CompanyName) + "</a>");
                }
                else
                {
                    jsonBuilder.Append(" -- ");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Contract_AllPrice));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //if (ordersinfos != null)
                //{
                //    jsonBuilder.Append(ordersinfos.Count);
                //}
                //else
                //{
                //    jsonBuilder.Append("0");
                //}
                //jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.DisplayCurrency(entity.Contract_Price));
                //jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(ContractPaymentStatus(entity.Contract_Payment_Status));
                //jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(ContractDeliveryStatus(entity.Contract_Delivery_Status));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(ContractStatus(entity.Contract_Status));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append(ContractConfirmStatus(entity.Contract_Confirm_Status));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Contract_Addtime);
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"contract_detail.aspx?contract_id=" + entity.Contract_ID + "\\\" title=\\\"查看\\\">查看</a>");
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

    //生成意向合同编号
    public string Create_TmpContract_SN()
    {
        string sn = "YX-ZGSB-BJXS(DS)-" + tools.FormatDate(DateTime.Now, "yyyy-MM") + "-";
        string sub_sn = "";
        int orders_amount = MyBLL.GetContractAmount("0,1,2,3", tools.FormatDate(DateTime.Now, "yyyy-MM").ToString(), Public.GetUserPrivilege());
        sub_sn = "0000" + (orders_amount + 1).ToString();
        sub_sn = sub_sn.Substring(sub_sn.Length - 4);
        sn = sn + sub_sn;
        return sn;
    }

    //生成正式合同编号
    public string Create_Contract_SN()
    {
        string sn = "ZGSB-BJXS(DS)-" + tools.FormatDate(DateTime.Now, "yyyy-MM") + "-";
        string sub_sn = "";
        int orders_amount = MyBLL.GetContractAmount("1,2,3", sn, Public.GetUserPrivilege());
        sub_sn = "0000" + (orders_amount + 1).ToString();
        sub_sn = sub_sn.Substring(sub_sn.Length - 4);
        sn = sn + sub_sn;
        return sn;
    }

    //生成意向合同
    public void Create_TmpContract(string Sign)
    {

        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string Contract_SN = "";
        string Contract_Template = "";
        int contract_type = tools.CheckInt(Request["contract_type"]);
        int Template_ID = tools.CheckInt(Request["Template_ID"]);
        string Contract_Name = tools.CheckStr(Request["Contract_Name"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        OrdersInfo ordersinfo = null;
        if (orders_sn.Length > 0)
        {
            ordersinfo = MyOrders.GetOrdersBySN(orders_sn);
            if (ordersinfo == null)
            {
                Public.Msg("error", "错误信息", "订单记录不存在", false, "/orders/orders_list.aspx");
            }
            else
            {
                contract_type = ordersinfo.Orders_Type;
            }
            if (ordersinfo.Orders_SupplierID > 0)
            {
                Public.Msg("error", "错误信息", "平台不能创建面向供应商采购的意向合同", false, "/orders/orders_list.aspx");
            }
            if (ordersinfo.Orders_ContractID > 0)
            {
                Public.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "/orders/orders_list.aspx");
            }
        }
        if (Contract_Name.Length == 0)
        {
            Public.Msg("info", "提示信息", "请填写合同名称", false, "{back}");
        }
        if (contract_type == 1)
        {
            Sign = "Goods_Contract_Template";
        }
        if (contract_type == 2)
        {
            Sign = "Custom_Contract_Template";
        }
        if (contract_type == 3)
        {
            Sign = "Agent_Contract_Template";
        }

        
        if (Sign.Length > 0)
        {
            //获取意向合同模版
            ContractTemplateInfo templateinfo = MyTemplate.GetContractTemplateBySign(Sign, Public.GetUserPrivilege());
            if (templateinfo != null)
            {
                Contract_Template = templateinfo.Contract_Template_Content;
            }
        }
        Contract_SN = Create_TmpContract_SN();
        ContractInfo entity = new ContractInfo();
        entity.Contract_Type = contract_type;
        entity.Contract_SN = Contract_SN;
        entity.Contract_BuyerID = 0;
        entity.Contract_AllPrice = 0;
        entity.Contract_Template = Contract_Template;
        if (entity.Contract_Template == "")
        {
            entity.Contract_Template = " ";
        }
        entity.Contract_Freight = 0;
        entity.Contract_ServiceFee = 0;
        entity.Contract_Delivery_ID = 0;
        entity.Contract_Delivery_Name = "";
        entity.Contract_Payway_ID = 0;
        entity.Contract_Payway_Name = "";
        entity.Contract_Site = Public.GetCurrentSite();
        entity.Contract_Status = 0;
        entity.Contract_Payment_Status = 0;
        entity.Contract_Delivery_Status = 0;
        entity.Contract_Confirm_Status = 0;
        entity.Contract_Note = "";
        entity.Contract_Addtime = DateTime.Now;
        entity.Contract_Discount = 0;
        entity.Contract_Source = 1;
        entity.Contract_SupplierID = 0;
        entity.Contract_IsEvaluate = 0;
        if (MyBLL.AddContract(entity, Public.GetUserPrivilege()))
        {
            ContractInfo tmp_contract = GetContractBySn(Contract_SN);
            if (tmp_contract != null)
            {
                //添加一期支付
                ContractDividedPaymentInfo dividedpayment = new ContractDividedPaymentInfo();
                dividedpayment.Payment_ContractID = tmp_contract.Contract_ID;
                dividedpayment.Payment_Name = "";
                dividedpayment.Payment_Amount = 0;
                dividedpayment.Payment_PaymentStatus = 0;
                dividedpayment.Payment_PaymentLine = DateTime.Now;
                dividedpayment.Payment_PaymentTime = DateTime.Now;
                MyDividedpay.AddContractDividedPayment(dividedpayment);

                if (ordersinfo != null)
                {

                    ordersinfo.Orders_ContractID = tmp_contract.Contract_ID;
                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        tmp_contract.Contract_Price = ordersinfo.Orders_Total_AllPrice;
                        tmp_contract.Contract_AllPrice = ordersinfo.Orders_Total_AllPrice;
                        tmp_contract.Contract_BuyerID = ordersinfo.Orders_BuyerID;
                        tmp_contract.Contract_SupplierID = ordersinfo.Orders_SupplierID;
                        tmp_contract.Contract_Payway_ID = ordersinfo.Orders_Payway;
                        tmp_contract.Contract_Payway_Name = ordersinfo.Orders_Payway_Name;
                        //代理采购合同
                        if (tmp_contract.Contract_Type == 3 && ordersinfo.Orders_SupplierID>0)
                        {
                            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID,Public.GetUserPrivilege());
                            if (supplierinfo != null)
                            {
                                //更新代理费用
                                tmp_contract.Contract_ServiceFee = Math.Round((tmp_contract.Contract_Price * supplierinfo.Supplier_AgentRate) / 100, 2);
                                tmp_contract.Contract_AllPrice = ordersinfo.Orders_Total_AllPrice + tmp_contract.Contract_ServiceFee;
                            }
                        }
                        MyBLL.EditContract(tmp_contract, Public.GetUserPrivilege());
                    }
                }
                //生成意向合同日志
                Contract_Log_Add(tmp_contract.Contract_ID, Session["User_Name"].ToString(), "生成意向合同", 1, "生成意向合同，合同编号：" + Contract_SN);
                Public.Msg("positive", "操作成功", "意向合同创建成功！", true, "Contract_list.aspx");
            }
            else
            {
                Public.Msg("error", "操作失败", "意向合同创建失败！", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "操作失败", "意向合同创建失败！", false, "{back}");
        }
    }

    //意向合同转换为履行中合同
    public void TmpContract_To_Contract(ContractInfo entity)
    {
        string Contract_Sn;
        //验证合同状态
        if (entity.Contract_Status > 0 || entity.Contract_Confirm_Status == 0)
        {
            Public.Msg("info", "提示信息", "源意向合同无法执行此操作！", false, "{back}");
        }

        Contract_Sn = Create_Contract_SN();
        entity.Contract_Status = 1;
        entity.Contract_SN = Contract_Sn;
        entity.Contract_Addtime = DateTime.Now;
        if (MyBLL.EditContract(entity, Public.GetUserPrivilege()))
        {
            Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "意向合同转换正式合同", 1, "意向合同转换正式合同，新合同编号：" + Contract_Sn);
            Public.Msg("positive", "操作成功", "操作成功！", false, "contract_detail.aspx?contract_id=" + entity.Contract_ID);
        }
        else
        {
            Public.Msg("error", "操作失败", "操作失败！", false, "{back}");
        }
    }

    //合同详细页按钮状态
    public void Contract_Detail_Button(int Contract_Status, int Contract_Confirmstatus, int Contract_ispay, int Contract_isfreight, int Contract_id, DateTime Contract_Addtime,int Contract_SupplierID, int Contract_Source)
    {
        string cancel_confirm, disable_confirm, disable_processing, disable_pay, disable_reach, disable_prepare, disable_freight, disable_accept, disable_regoods, disable_success, disable_close;

        //取消按钮
        if (Contract_Status == 0 && Contract_Confirmstatus == 0)
        {
            cancel_confirm = "";
        }
        else
        {
            cancel_confirm = "disabled=\"disabled\"";
        }

        //平台确认
        if (Contract_Status == 0 &&Contract_Confirmstatus!=2)
        {
            disable_confirm = "";
        }
        else
        {
            disable_confirm = "disabled=\"disabled\"";
        }

        //配货中按钮
        if (Contract_Status == 1 && Contract_isfreight == 0)
        {
            disable_prepare = "";
        }
        else
        {
            disable_prepare = "disabled=\"disabled\"";
        }

        //支付按钮
        if ((Contract_Status == 1 && (Contract_ispay == 0 || Contract_ispay == 1)))
        {
            disable_pay = "";
        }
        else
        {
            disable_pay = "disabled=\"disabled\"";
        }

        

        //配送按钮
        if ((Contract_Status == 1 && (Contract_isfreight == 1 || Contract_isfreight == 2)))
        {
            disable_freight = "";
        }
        else
        {
            disable_freight = "disabled=\"disabled\"";
        }

        //成功按钮
        if ((Contract_Status == 1 && Contract_isfreight == 4 && Contract_ispay == 3))
        {
            disable_success = "";
        }
        else
        {
            disable_success = "disabled=\"disabled\"";
        }

        
        //disable_close = "disabled=\"disabled\"";
        if (Public.CheckPrivilege("d00c2e8f-67b3-44e7-9ea8-8b4d9208dc4d")&&Contract_Source==1)
        {
            Response.Write("<input name=\"btn_cancel\" type=\"button\" class=\"btn_01\" " + cancel_confirm + " id=\"btn_cancel\" value=\"取消\" onclick=\"turnnewpage('contract_cancel.aspx?contract_id=" + Contract_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("a60144fc-1b68-43db-9546-74ddb0ba84e7") && Contract_SupplierID==0)
        {
            Response.Write("<input name=\"btn_confirm\" type=\"button\" class=\"btn_01\" " + disable_confirm + " id=\"btn_confirm\" value=\"平台确认\" onclick=\"turnnewpage('contract_do.aspx?action=confirm&contract_id=" + Contract_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("f0a5a5f7-c145-4a58-9780-205b406d266e") && Contract_SupplierID == 0)
        {
            Response.Write("<input name=\"btn_prepare\" type=\"button\" class=\"btn_01\" " + disable_pay + " id=\"btn_prepare\" value=\"支付\"  onclick=\"turnnewpage('contract_pay.aspx?contract_id=" + Contract_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("5b7c6861-fae3-4a5b-a72d-505bdd56b73b") && Contract_SupplierID == 0)
        {
            Response.Write("<input name=\"btn_prepare\" type=\"button\" class=\"btn_01\" " + disable_prepare + " id=\"btn_prepare\" value=\"配货中\"  onclick=\"turnnewpage('contract_do.aspx?action=prepare&contract_id=" + Contract_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("4b60b2fa-95b4-493a-a0b8-d1a06913b9b4") && Contract_SupplierID == 0)
        {
            Response.Write("<input name=\"btn_freight\" type=\"button\" class=\"btn_01\" " + disable_freight + " id=\"btn_freight\" value=\"发货\"  onclick=\"turnnewpage('contract_freight.aspx?contract_id=" + Contract_id + "')\"/> ");
        }

        Response.Write("&nbsp;&nbsp;");
        if (Public.CheckPrivilege("16770a1b-f216-453d-8210-6e22e6ac364e") && Contract_SupplierID == 0)
        {
            Response.Write("<input name=\"btn_success\" type=\"button\" class=\"btn_01\" " + disable_success + " id=\"btn_success\" value=\"完成\" onclick=\" if(confirm('确认完成订单？')){ location = 'contract_do.aspx?action=contract_success&Contract_id=" + Contract_id + "' }\" /> ");
        }

        Response.Write("<input name=\"btn_close\" type=\"button\" class=\"btn_01\" id=\"btn_close\" value=\"合同预览\" onclick=\"window.open('contract_view.aspx?contract_id=" + Contract_id + "')\" /> ");

        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    }

    //获取合同附加订单
    public string GetContractOrdersByContractsID(int Contract_ID)
    {
        string strHTML = "";
        int i = 0;
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">";
        strHTML += "    <tr class=\"goods_head\">";
        strHTML += "        <td>序号</td>";
        strHTML += "        <td>订单编号</td>";
        strHTML += "        <td>商品种类</td>";
        strHTML += "        <td>数量</td>";
        strHTML += "        <td>订单金额</td>";
        strHTML += "        <td>下单时间</td>";
        strHTML += "        <td>操作</td>";
        strHTML += "    </tr>";
        ContractInfo contractinfo = GetContractByID(Contract_ID);
        if (contractinfo == null)
        {
            strHTML += "    </table>";
            return strHTML;
        }
        int Goods_Amount = 0;
        double Goods_Sum = 0;
        IList<OrdersGoodsInfo> GoodsListAll = null;
        IList<OrdersInfo> ordersinfos = GetOrderssByContractID(Contract_ID);
        if (ordersinfos != null)
        {
            foreach (OrdersInfo entity in ordersinfos)
            {

                Goods_Sum = 0;
                Goods_Amount = 0;
                GoodsListAll = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo good in GoodsListAll)
                    {
                        if ((good.Orders_Goods_ParentID == 0 && good.Orders_Goods_Type != 2) || good.Orders_Goods_ParentID > 0)
                        {
                            Goods_Amount = Goods_Amount + 1;
                            Goods_Sum = Goods_Sum + good.Orders_Goods_Amount;
                        }
                    }

                }
                i++;
                strHTML += "    <tr class=\"goods_list\">";
                strHTML += "        <td align=\"center\">" + i + "</td>";
                strHTML += "        <td><a href=\"/orders/orders_view.aspx?orders_id=" + entity.Orders_ID + "\" target=\"_blank\">" + entity.Orders_SN + "</a></td>";
                strHTML += "        <td>" + Goods_Amount + "</td>";
                strHTML += "        <td>" + Goods_Sum + "</td>";
                strHTML += "        <td>" + Public.DisplayCurrency(entity.Orders_Total_AllPrice) + "</td>";
                strHTML += "        <td>" + entity.Orders_Addtime + "</td>";
                if (contractinfo.Contract_Confirm_Status == 0 && contractinfo.Contract_Status == 0&&contractinfo.Contract_Source==1)
                {
                    strHTML += "        <td><a href=\"contract_do.aspx?action=order_remove&orders_sn=" + entity.Orders_SN + "\">移除</a></td>";
                }
                else
                {
                    strHTML += "        <td></td>";
                }
                strHTML += "    </tr>";
            }

        }
        else
        {
            strHTML += "    <tr class=\"goods_list\"><td colspan=\"8\" height=\"30\" align=\"center\">暂无附加订单</td></tr>";
        }

        strHTML += "    </table>";
        return strHTML;
    }

    //合同分期付款
    public string GetDividedPayByContractsID(int Contract_ID, string Contract_SN, int Contract_Status)
    {
        StringBuilder HTML_Str = new StringBuilder();
        int i = 0;
        StringBuilder selectstr = new StringBuilder();
        IList<ContractDividedPaymentInfo> entitys = MyDividedpay.GetContractDividedPaymentsByContractID(Contract_ID);

        selectstr.Append("<div style=\"padding:10px;\">付款期数:<select name=\"Contract_Divided\">");

        if (entitys != null)
        {
            for (i = 1; i <= 7; i++)
            {
                if (i == entitys.Count)
                {
                    selectstr.Append("<option value=\"" + i + "\" selected>" + i + "期</option>");
                }
                else
                {
                    selectstr.Append("<option value=\"" + i + "\">" + i + "期</option>");
                }
            }
            selectstr.Append("</select></div>");
            if (Contract_Status == 0)
            {
                HTML_Str.Append(selectstr.ToString());
            }

            HTML_Str.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
            HTML_Str.Append("<tr class=\"list_head_bg\"><td align=\"center\"><b>分期名称</b></td><td align=\"center\"><b>付款金额</b></td><td align=\"center\"><b>计划付款时间</b></td><td align=\"center\"><b>付款条件</b></td></tr>");
            foreach (ContractDividedPaymentInfo entity in entitys)
            {

                HTML_Str.Append("<tr class=\"list_td_bg\">");
                if (Contract_Status > 0)
                {
                    HTML_Str.Append("<td align=\"center\" height=\"25\">" + entity.Payment_Name + "</td>");
                    HTML_Str.Append("<td align=\"center\">" + Public.DisplayCurrency(entity.Payment_Amount) + "</td>");
                    HTML_Str.Append("<td align=\"center\">" + entity.Payment_PaymentLine.ToString("yyyy-MM-dd") + "</td>");
                    HTML_Str.Append("<td align=\"center\">" + entity.Payment_Note + "</td>");
                    //HTML_Str.Append("<td align=\"center\">" + ContractDividedPaymentStatus(entity.Payment_PaymentStatus) + "</td>");
                }
                else
                {
                    HTML_Str.Append("<td align=\"center\" height=\"25\"><input type=\"text\" name=\"payment_name_" + entity.Payment_ID + "\" value=\"" + entity.Payment_Name + "\"></td>");
                    HTML_Str.Append("<td align=\"center\"><input type=\"text\" name=\"payment_amount_" + entity.Payment_ID + "\" value=\"" + entity.Payment_Amount + "\"></td>");
                    HTML_Str.Append("<td align=\"center\"><input type=\"text\" name=\"payment_paymentline_" + entity.Payment_ID + "\" value=\"" + entity.Payment_PaymentLine.ToString("yyyy-MM-dd") + "\"> 如：2011-01-01</td>");
                    HTML_Str.Append("<td align=\"center\"><input type=\"text\" name=\"payment_note_" + entity.Payment_ID + "\" value=\"" + entity.Payment_Note + "\"></td>");
                    //HTML_Str.Append("<td align=\"center\">" + ContractDividedPaymentStatus(entity.Payment_PaymentStatus) + "</td>");
                }
                HTML_Str.Append("</tr>");
            }
            HTML_Str.Append("</table>");

            if (Contract_Status == 0)
            {
                HTML_Str.Append("<center style=\"margin-top:10px;\"><input name=\"button\" type=\"submit\" class=\"bt_orange\" id=\"button\" value=\"提交分期\" /></center>");
                HTML_Str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"Contract_dividedSave\" />");
                HTML_Str.Append("<input name=\"Contract_ID\" type=\"hidden\" id=\"Contract_ID\" value=\"" + Contract_ID + "\" />");
            }
        }
        else
        {
            for (i = 1; i <= 7; i++)
            {
                selectstr.Append("<option value=\"" + i + "\">" + i + "期</option>");
            }
            selectstr.Append("</select></div>");
            if (Contract_Status == 0)
            {
                HTML_Str.Append(selectstr.ToString());
            }
            HTML_Str.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
            HTML_Str.Append("<tr class=\"list_td_bg\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无分期信息</td></tr>");
            HTML_Str.Append("</table>");
            if (Contract_Status == 0)
            {
                HTML_Str.Append("<center style=\"margin-top:10px;\"><input name=\"button\" type=\"submit\" class=\"bt_orange\" id=\"button\" value=\"提交分期\" /></center>");
                HTML_Str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"Contract_dividedSave\" />");
                HTML_Str.Append("<input name=\"Contract_ID\" type=\"hidden\" id=\"Contract_ID\" value=\"" + Contract_ID + "\" />");
            }
        }

        return HTML_Str.ToString();
    }

    //合同付款单
    public string Contract_Payments(int Contract_ID,string Contract_SN, int Contract_SupplierID)
    {
        StringBuilder HTML_Str = new StringBuilder();

        IList<ContractPaymentInfo> entitys = MyPayment.GetContractPaymentsByContractID(Contract_ID);

        HTML_Str.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        if (entitys != null)
        {
            HTML_Str.Append("<tr class=\"list_head_bg\">");
            HTML_Str.Append("<td align=\"center\"><b>付款单号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付金额</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付方式</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付备注</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付时间</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付状态</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付凭据</b></td>");
            if (Contract_SupplierID ==0)
            {
                HTML_Str.Append("<td align=\"center\"><b>操作</b></td>");
            }
            HTML_Str.Append("</tr>");
            foreach (ContractPaymentInfo entity in entitys)
            {
                //if (entity.Contract_Payment_PaymentStatus > 0)
                //{
                //}

                HTML_Str.Append("<tr class=\"list_td_bg\"><td align=\"center\" height=\"25\">" + entity.Contract_Payment_DocNo + "</td><td align=\"center\">" + Public.DisplayCurrency(entity.Contract_Payment_Amount) + "</td><td align=\"center\">" + entity.Contract_Payment_Name + "</td><td align=\"center\">" + entity.Contract_Payment_Note + "</td><td align=\"center\">" + entity.Contract_Payment_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\">" + ContractPaymentsStatus(entity.Contract_Payment_PaymentStatus) + "</td>");
                if (entity.Contract_Payment_Attachment.Length > 0)
                    HTML_Str.Append("<td align=\"center\"><a href=\"" + Convert.ToString(Application["Upload_Server_URL"]).TrimEnd('/') + entity.Contract_Payment_Attachment + "\" target=\"_blank\">查看</a></td>");
                else
                    HTML_Str.Append("<td align=\"center\"></td>");

                if (Contract_SupplierID == 0)
                {
                    if (entity.Contract_Payment_PaymentStatus == 1)
                    {
                        HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"btn_01\" value=\"已到帐\" onclick=\"location='contract_do.aspx?action=contract_payreach&Contract_SN=" + Contract_SN + "&Payment_SN=" + entity.Contract_Payment_DocNo + "';\"></td>");
                    }
                    else if (entity.Contract_Payment_PaymentStatus == 0)
                    {
                        HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"btn_01\" value=\"已付款\" onclick=\"location='contract_do.aspx?action=contract_paid&Contract_SN=" + Contract_SN + "&Payment_SN=" + entity.Contract_Payment_DocNo + "';\"></td>");
                    }
                    else
                    {
                        HTML_Str.Append("<td align=\"center\"></td>");
                    }
                }
                HTML_Str.Append("</tr>");

            }

        }
        else
        {
            HTML_Str.Append("<tr class=\"list_td_bg\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无付款信息</td></tr>");
        }
        HTML_Str.Append("</table>");
        return HTML_Str.ToString();
    }

    //合同配送单
    public string Contract_Deliverys(int Contract_ID, string Contract_SN)
    {
        StringBuilder HTML_Str = new StringBuilder();
        IList<ContractDeliveryInfo> entitys = MyFreight.GetContractDeliverysByContractID(Contract_ID);

        HTML_Str.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        if (entitys != null)
        {
            HTML_Str.Append("<tr class=\"list_head_bg\">");
            HTML_Str.Append("<td align=\"center\" height=\"25\"><b>配送单号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>物流公司</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>配送方式</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>配送状态</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>物流单号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>配送时间</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>操作</b></td>");
            HTML_Str.Append("</tr>");
            foreach (ContractDeliveryInfo entity in entitys)
            {
                HTML_Str.Append("<tr class=\"list_td_bg\"><td align=\"center\" height=\"25\">" + entity.Contract_Delivery_DocNo + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_CompanyName + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Name + "</td>");
                HTML_Str.Append("<td align=\"center\">" + ContractDeliverysStatus(entity.Contract_Delivery_DeliveryStatus) + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Code + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"btn_01\" value=\"查看\" onclick=\"location='contract_freight_view.aspx?contract_id=" + Contract_ID + "&contract_delivery=" + entity.Contract_Delivery_ID + "';\">");
                HTML_Str.Append("</td>");

                HTML_Str.Append("</tr>");
            }

        }
        else
        {
            HTML_Str.Append("<tr class=\"list_td_bg\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无配送信息</td></tr>");
        }
        HTML_Str.Append("</table>");
        return HTML_Str.ToString();
    }

    //获取合同发票信息
    public string GetContractInvoice(int Contract_ID, int Contract_Status,int Contract_SupplierID)
    {
        string HTML_Str = "";
        ContractInvoiceInfo entity = MyBLL.GetContractInvoiceByContractID(Contract_ID);
        if (entity != null)
        {
            HTML_Str = HTML_Str + "<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"0\">";
            int status = tools.NullInt(entity.Invoice_Status, 0);
            //if (Contract_Status > 0 && status < 2 && Contract_SupplierID==0)
            //{
            //    HTML_Str = HTML_Str + "<tr><td colspan=\"2\">发票申请：";

            //    if (status == 0)
            //    {
            //        if (Public.CheckPrivilege("2233c05d-4d76-43c6-b680-74df5ece66b2"))
            //        {
            //            HTML_Str = HTML_Str + "<input type=\"button\" id=\"Button10\" class=\"btn_01\" value=\"启用发票申请\" onclick=\"location.href='Contract_do.aspx?action=invoicestatus&invoice_id=" + entity.Invoice_ID + "&contract_id=" + Contract_ID + "'\" />";
            //        }
            //    }
            //    else if (status == 1)
            //    {
            //        if (Public.CheckPrivilege("2233c05d-4d76-43c6-b680-74df5ece66b2"))
            //        {
            //            HTML_Str = HTML_Str + "<input type=\"button\" id=\"Button10\" class=\"btn_01\" value=\"关闭发票申请\" onclick=\"location.href='Contract_do.aspx?action=invoicestatus&invoice_id=" + entity.Invoice_ID + "&contract_id=" + Contract_ID + "'\" />";
            //        }
            //    }
            //    HTML_Str = HTML_Str + "</td></tr>";
            //}

            if (entity.Invoice_Type == 0)
            {
                if (entity.Invoice_Title == "单位")
                {
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">发票类型：普通发票</td><td>发票抬头：" + entity.Invoice_Title + "</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\" colspan=\"2\">单位名称：" + entity.Invoice_FirmName + "</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">单位地址：" + entity.Invoice_VAT_RegAddr + "</td><td>单位电话：" + entity.Invoice_VAT_RegTel + "</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">税号：" + entity.Invoice_VAT_Code + "</td><td>发票内容：明细</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                }
                else
                {
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">发票类型：普通发票</td><td></td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">发票抬头：" + entity.Invoice_Title + "</td><td>发票内容：明细</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                    HTML_Str = HTML_Str + "  <tr>";
                    HTML_Str = HTML_Str + "    <td width=\"35%\">姓名：" + entity.Invoice_PersonelName + "</td><td>身份证号：" + entity.Invoice_PersonelCard + "</td>";
                    HTML_Str = HTML_Str + "  </tr>";
                }
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>";
                HTML_Str = HTML_Str + "  </tr>";

            }
            else
            {
                HTML_Str = HTML_Str + "  <tr>";
                if (entity.Invoice_VAT_Cert != "")
                {
                    HTML_Str = HTML_Str + "    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + tools.NullStr(Application["upload_server_url"]) + entity.Invoice_VAT_Cert + "\" style=\"color:#4378C8;\" target=\"_blank\">查看纳税人资格证</a></td>";
                }
                else
                {
                    HTML_Str = HTML_Str + "    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "</td>";
                }
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">单位名称：" + entity.Invoice_VAT_FirmName + "</td><td>纳税人识别号：" + entity.Invoice_VAT_Code + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">注册地址：" + entity.Invoice_VAT_RegAddr + "</td><td>注册电话：" + entity.Invoice_VAT_RegTel + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
                HTML_Str = HTML_Str + "  <tr>";
                HTML_Str = HTML_Str + "    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>";
                HTML_Str = HTML_Str + "  </tr>";
            }
            HTML_Str = HTML_Str + "  </table>";
        }
        else
        {
            HTML_Str += "未选择发票 ";
        }
        return HTML_Str;
    }

    //发票申请信息
    public string GetInvoiceApplyssByContractsID(int Contract_ID, string Contract_Sn,int Contract_SupplierID)
    {
        string strHTML = "";
        int i;
        string sys_name = "";
        IList<ContractInvoiceApplyInfo> entitys = MyBLL.GetContractInvoiceApplysByContractID(Contract_ID);
        if (entitys != null)
        {

            strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">";
            strHTML += "    <tr class=\"list_head_bg\">";
            strHTML += "        <td width=\"60\">编号</td>";
            strHTML += "        <td>所属合同</td>";
            strHTML += "        <td>申请金额</td>";
            strHTML += "        <td>开票金额</td>";
            strHTML += "        <td>备注</td>";
            strHTML += "        <td>状态</td>";
            strHTML += "        <td>申请/开具时间</td>";
            strHTML += "        <td>操作</td>";
            strHTML += "    </tr>";

            int ICount = 1;
            foreach (ContractInvoiceApplyInfo entity in entitys)
            {
                strHTML += "    <form name=\"frm_" + entity.Invoice_Apply_ID + "\" method=\"post\" action=\"contract_do.aspx\">";
                strHTML += "    <tr class=\"list_td_bg\">";
                strHTML += "        <td>" + entity.Invoice_Apply_ID + "</td>";
                strHTML += "        <td>" + Contract_Sn + "</td>";
                strHTML += "        <td>" + Public.DisplayCurrency(entity.Invoice_Apply_ApplyAmount) + "</td>";//dd
                if (entity.Invoice_Apply_Status == 0 && Contract_SupplierID == 0)
                {
                    strHTML += "        <td><input type=\"text\" name=\"invoice_amount\" size=\"15\"></td>";
                    strHTML += "        <td><input type=\"text\" name=\"apply_note\" size=\"30\"></td>";
                }
                else
                {
                    strHTML += "        <td>" + Public.DisplayCurrency(entity.Invoice_Apply_Amount) + "</td>";
                    strHTML += "        <td>" + entity.Invoice_Apply_Note + "</td>";
                }
                strHTML += "        <td>" + ContractInvoiceApplyStatus(entity.Invoice_Apply_Status) + "</td>";
                strHTML += "        <td>" + entity.Invoice_Apply_Addtime + "</td>";

                strHTML += "        <td align=\"left\">";
                if (entity.Invoice_Apply_Status == 0 && Public.CheckPrivilege("2233c05d-4d76-43c6-b680-74df5ece66b2") && Contract_SupplierID==0)
                {
                    strHTML += "<input name=\"btn_print\" type=\"submit\" class=\"btn_01\" onclick=\"$('#action_" + ICount + "').val('apply_open');\" value=\"开票\"> <input name=\"btn_print\" type=\"submit\" class=\"btn_01\" onclick=\"$('#action_" + ICount + "').val('apply_cancel');\" value=\"取消\"><input type=\"hidden\" id=\"action_" + ICount + "\" name=\"action\" value=\"apply_open\"><input type=\"hidden\" name=\"apply_id\" value=\"" + entity.Invoice_Apply_ID + "\">";
                }
                if (entity.Invoice_Apply_Status == 1 && Public.CheckPrivilege("2233c05d-4d76-43c6-b680-74df5ece66b2") && Contract_SupplierID == 0)
                {
                    strHTML += "<input name=\"btn_print\" type=\"submit\" class=\"btn_01\" value=\"已邮寄\"><input type=\"hidden\" name=\"action\" value=\"apply_send\"><input type=\"hidden\" name=\"apply_id\" value=\"" + entity.Invoice_Apply_ID + "\">";
                }
                if (entity.Invoice_Apply_Status == 2 && Public.CheckPrivilege("2233c05d-4d76-43c6-b680-74df5ece66b2") && Contract_SupplierID == 0)
                {
                    strHTML += "<input name=\"btn_print\" type=\"submit\" class=\"btn_01\" value=\"已收票\"><input type=\"hidden\" name=\"action\" value=\"apply_accept\"><input type=\"hidden\" name=\"apply_id\" value=\"" + entity.Invoice_Apply_ID + "\">";
                }
                strHTML += "</td>";
                strHTML += "    </tr>";
                strHTML += "    </form>";
                ICount += 1;
            }

            strHTML += "</table>";
        }
        return strHTML;
    }

    //获取合同日志
    public string GetContractLogsByContractsID(int Contract_ID)
    {
        string strHTML = "";
        string buyer_name = "用户";
        ContractInfo contractinfo = GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            SupplierInfo buyerinfo = MySupplier.GetSupplierByID(contractinfo.Contract_BuyerID, Public.GetUserPrivilege());
            if (buyerinfo != null)
            {
                buyer_name = buyerinfo.Supplier_CompanyName;
            }
            IList<ContractLogInfo> LogInfoList = Mycontractlog.GetContractLogsByContractID(Contract_ID);
            strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">";
            strHTML += "    <tr class=\"list_head_bg\">";
            strHTML += "        <td width=\"60\">序号</td>";
            strHTML += "        <td width=\"130\">时间</td>";
            strHTML += "        <td>操作</td>";
            strHTML += "        <td>操作人</td>";
            strHTML += "        <td>备注</td>";
            strHTML += "    </tr>";
            if (LogInfoList != null)
            {
                int ICount = 1;
                foreach (ContractLogInfo entity in LogInfoList)
                {
                    strHTML += "    <tr class=\"list_td_bg\">";
                    strHTML += "        <td>" + ICount + "</td>";
                    strHTML += "        <td class=\"info_date\">" + entity.Log_Addtime + "</td>";
                    strHTML += "        <td>" + entity.Log_Action + "</td>";
                    if (entity.Log_Operator == "")
                    {
                        strHTML += "        <td>" + buyer_name + "</td>";
                    }
                    else
                    {
                        strHTML += "        <td>" + entity.Log_Operator + "</td>";
                    }
                    strHTML += "        <td align=\"left\">" + entity.Log_Remark + "</td>";
                    strHTML += "    </tr>";
                    ICount += 1;
                }
            }
            strHTML += "</table>";
        }
        return strHTML;
    }

    //合同配送单产品列表
    public string Contract_Delivery_Goods(int Delivery_ID)
    {
        string HTML_Str = "";

        IList<ContractDeliveryGoodsInfo> entitys = MyFreight.GetContractDeliveryGoodssByDeliveryID(Delivery_ID);
        OrdersGoodsInfo goods = null;
        ContractDeliveryInfo DeliveryEntity = null;
        HTML_Str = HTML_Str + "<form name=\"frm_payment\" method=\"post\" action=\"Contract_do.aspx\">";
        HTML_Str = HTML_Str + "<table width=\"100%\" border=\"0\" cellpadding=\"7\" cellspacing=\"1\" bgcolor=\"#bbbbbb\">";
        if (entitys != null)
        {
            HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\">";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>订货号</b></td>";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>产品名称</b></td>";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>规格</b></td>";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>单价</b></td>";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>发货数量</b></td>";
            HTML_Str = HTML_Str + "<td align=\"center\"><b>签收数量</b></td>";
            HTML_Str = HTML_Str + "</tr>";

            foreach (ContractDeliveryGoodsInfo entity in entitys)
            {
                goods = MyOrders.GetOrdersGoodsByID(entity.Delivery_Goods_GoodsID);
                DeliveryEntity = MyFreight.GetContractDeliveryByID(entity.Delivery_Goods_DeliveryID);
                if (goods != null && DeliveryEntity != null)
                {

                    HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\"><td align=\"center\">" + goods.Orders_Goods_Product_Code + "</td><td align=\"center\">" + goods.Orders_Goods_Product_Name + "</td><td align=\"center\">" + goods.Orders_Goods_Product_Spec + "</td><td align=\"center\">" + Public.DisplayCurrency(goods.Orders_Goods_Product_Price) + "</td><td align=\"center\">" + entity.Delivery_Goods_Amount + "</td>";
                    if (entity.Delivery_Goods_Status == 0)
                    {
                        HTML_Str = HTML_Str + "<td align=\"center\"><input type=\"text\" name=\"AMOUNT" + entity.Delivery_Goods_ID + "\" style=\" width:60px;\" size=\"10\" maxlength=\"10\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" /></td>";

                        HTML_Str = HTML_Str + "</td>";
                    }
                    else if (DeliveryEntity.Contract_Delivery_DeliveryStatus == 1)
                    {
                        HTML_Str = HTML_Str + "<td align=\"center\"><input type=\"text\" name=\"AMOUNT" + entity.Delivery_Goods_ID + "\" value=\"" + entity.Delivery_Goods_AcceptAmount + "\" style=\" width:60px;\" size=\"10\" maxlength=\"10\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" /></td>";

                        HTML_Str = HTML_Str + "</td>";
                    }
                    else
                    {
                        HTML_Str = HTML_Str + "<td align=\"center\">" + entity.Delivery_Goods_AcceptAmount + "</td>";
                    }
                    HTML_Str = HTML_Str + "</tr>";

                }
            }

        }
        else
        {
            HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无配送信息</td></tr>";
        }
        HTML_Str = HTML_Str + "</table>";
        HTML_Str = HTML_Str + "<input name=\"action\" type=\"hidden\" id=\"action\" value=\"accept\"/>";
        HTML_Str = HTML_Str + "<input name=\"goods_id\" type=\"hidden\" id=\"goods_id\" value=\"0\"/>";
        HTML_Str = HTML_Str + "</form>";
        return HTML_Str;
    }

    //合同修改
    public void Contract_Edit()
    {
        string contract_template, log_remark, payway_name, delivery_name;
        int Contract_id, Contract_Divided, Contract_payway, Contract_delivery;
        double Contract_Freight, Contract_Service, Contract_Discount;

        log_remark = "合同信息编辑，双方未确认";
        contract_template = Request["contract_template"];
        Contract_id = tools.CheckInt(Request["contract_id"]);

        Contract_Freight = tools.CheckFloat(Request["Contract_Freight"]);
        Contract_Service = tools.CheckFloat(Request["Contract_Service"]);
        Contract_Discount = tools.CheckFloat(Request["Contract_Discount"]);
        int CONTRACT_AREAID = tools.CheckInt(Request["CONTRACT_AREAID"]);
        payway_name = "";
        delivery_name = "";

        if (Contract_id == 0)
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }

        

        ContractInfo entity = GetContractByID(Contract_id);
        if (entity != null)
        {
            if (entity.Contract_Status>0)
            {
                Public.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
                return;
            }
            if (Contract_Discount > 0 && Contract_Discount > entity.Contract_Price)
            {
                Public.Msg("error", "错误信息", "优惠金额不能超过订单总金额", false, "{back}");
                return;
            }

            //if (entity.Contract_Freight != Contract_Freight&&entity.Contract_Source>0)
            if (entity.Contract_Freight != Contract_Freight && entity.Contract_SupplierID == 0)
            {
                log_remark += "，运费由" + Public.DisplayCurrency(entity.Contract_Freight) + "更改为" + Public.DisplayCurrency(Contract_Freight) + "";
                entity.Contract_Freight = Contract_Freight;
            }

            if (entity.Contract_ServiceFee != Contract_Service)
            {
                log_remark += "，服务费用由" + Public.DisplayCurrency(entity.Contract_ServiceFee) + "更改为" + Public.DisplayCurrency(Contract_Service) + "";
                entity.Contract_ServiceFee = Contract_Service;
            }

            //if (entity.Contract_Discount != Contract_Discount && entity.Contract_Source>0)
            if (entity.Contract_Discount != Contract_Discount && entity.Contract_SupplierID == 0)
            {
                log_remark += "，合同优惠由" + Public.DisplayCurrency(entity.Contract_Discount) + "更改为" + Public.DisplayCurrency(Contract_Discount) + "";
                entity.Contract_Discount = Contract_Discount;
            }

            entity.Contract_Template = contract_template;

            entity.Contract_Confirm_Status = 0;
            entity.Contract_AllPrice = entity.Contract_Price + entity.Contract_ServiceFee + entity.Contract_Freight - entity.Contract_Discount;
            if (MyBLL.EditContract(entity, Public.GetUserPrivilege()))
            {
                Contract_Log_Add(Contract_id, Session["User_Name"].ToString(), "平台修改合同信息", 1, log_remark);
                Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + Contract_id);
            }
            else
            {
                Public.Msg("positive", "操作失败", "操作失败", true, "Contract_detail.aspx?contract_id=" + Contract_id);
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }
    }

    //合同添加订单
    public void Contract_Order_Add()
    {
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        int contract_id = tools.CheckInt(Request["contract_id"]);
        int orders_id = 0;
        string Contract_SN, Orders_SN;
        double Orders_Price = 0;
        Orders_SN = "";
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        if (orders_sn.Length == 0)
        {
            Public.Msg("error", "错误信息", "无效的订单号！", false, "{back}");
        }
        if (contract_id == 0)
        {
            Public.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }

        //订单检验
        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(orders_sn);
        if (ordersinfo != null)
        {
            if ((ordersinfo.Orders_SupplierID != 0) || ordersinfo.Orders_PaymentStatus > 0 || ordersinfo.Orders_DeliveryStatus > 0 || ordersinfo.Orders_Status != 1)
            {
                orders_id = 0;
            }
            else
            {
                orders_id = ordersinfo.Orders_ID;
                Orders_SN = ordersinfo.Orders_SN;
                Orders_Price = ordersinfo.Orders_Total_AllPrice;
            }
        }
        else
        {
            orders_id = 0;
        }
        if (orders_id == 0)
        {
            Public.Msg("error", "错误信息", "订单添加失败！", false, "{back}");
        }

        if (ordersinfo.Orders_ContractID > 0)
        {
            Public.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "{back}");
        }


        ContractInfo entity = GetContractByID(contract_id);
        if (entity != null)
        {
            IList<OrdersInfo> ordersinfos = GetOrderssByContractID(entity.Contract_ID);
            if (ordersinfos != null && entity.Contract_SupplierID != ordersinfo.Orders_SupplierID)
            {
                Public.Msg("error", "错误信息", "同一合同不可附加不同卖家的订单！", false, "{back}");
            }
            if (ordersinfos != null && entity.Contract_BuyerID != ordersinfo.Orders_BuyerID)
            {
                Public.Msg("error", "错误信息", "同一合同不可附加不同买家的订单！", false, "{back}");
            }
            if (entity.Contract_SupplierID>0||entity.Contract_Source==0 || entity.Contract_Status > 0 || entity.Contract_Confirm_Status > 0)
            {
                Public.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
            }
            else
            {
                if (ordersinfo.Orders_Type != entity.Contract_Type)
                {
                    Public.Msg("error", "错误信息", "合同与订单类型不一致！", false, "{back}");
                }
                Contract_SN = entity.Contract_SN;
                //添加订单到意向合同中
                ordersinfo.Orders_ContractID = entity.Contract_ID;
                if (MyOrders.EditOrders(ordersinfo))
                {
                    //更新意向合同价格

                    entity.Contract_Price += ordersinfo.Orders_Total_AllPrice;
                    //代理采购合同
                    if (entity.Contract_Type == 3 && ordersinfo.Orders_SupplierID>0)
                    {
                        SupplierInfo supplierinfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID,Public.GetUserPrivilege());
                        if (supplierinfo != null)
                        {
                            //更新代理费用
                            entity.Contract_ServiceFee = Math.Round((entity.Contract_Price * supplierinfo.Supplier_AgentRate) / 100, 2);
                        }
                    }
                    entity.Contract_AllPrice = entity.Contract_Price + entity.Contract_ServiceFee + entity.Contract_Freight - entity.Contract_Discount;
                    entity.Contract_BuyerID = ordersinfo.Orders_BuyerID;
                    entity.Contract_Payway_ID = ordersinfo.Orders_Payway;
                    entity.Contract_Payway_Name = ordersinfo.Orders_Payway_Name;
                    MyBLL.EditContract(entity, Public.GetUserPrivilege());

                    //添加订单到意向合同日志添加
                    Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "添加订单到意向合同", 1, "添加订单到本意向合同，添加订单号：" + Orders_SN);
                    Public.Msg("positive", "操作成功", "订单已成功添加至新建意向合同[" + Contract_SN + "]中！", true, "/contract/Contract_detail.aspx?contract_id=" + entity.Contract_ID);
                }
                else
                {
                    Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "添加订单到意向合同", 0, "添加订单到本意向合同，添加订单号：" + Orders_SN);
                    Public.Msg("error", "错误信息", "订单添加至新建意向合同失败！", false, "/contract/Contract_detail.aspx?contract_id=" + entity.Contract_ID);
                }
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }
    }

    //意向合同订单移除
    public void TmpContract_Orders_Remove()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(orders_sn);
        if (ordersinfo != null)
        {
            ContractInfo entity = GetContractByID(ordersinfo.Orders_ContractID);
            if (entity != null)
            {
                if (entity.Contract_Source==1&&entity.Contract_SupplierID==0 && entity.Contract_Status == 0 && entity.Contract_Confirm_Status == 0)
                {
                    //更新订单金额
                    entity.Contract_Price = entity.Contract_Price - ordersinfo.Orders_Total_AllPrice;
                    if (entity.Contract_Price < 0)
                    {
                        entity.Contract_Price = 0;
                    }
                    //代理采购合同
                    if (entity.Contract_Type == 3 && ordersinfo.Orders_SupplierID > 0)
                    {
                        SupplierInfo supplierinfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID,Public.GetUserPrivilege());
                        if (supplierinfo != null)
                        {
                            //更新代理费用
                            entity.Contract_ServiceFee = Math.Round((entity.Contract_Price * supplierinfo.Supplier_AgentRate) / 100, 2);
                        }
                    }
                    if (entity.Contract_Discount > entity.Contract_Price)
                    {
                        entity.Contract_Discount = entity.Contract_Price;
                    }
                    //更新总价
                    entity.Contract_AllPrice = entity.Contract_Price + entity.Contract_ServiceFee + entity.Contract_Price + entity.Contract_Freight - entity.Contract_Discount;
                    if (entity.Contract_AllPrice < 0)
                    {
                        entity.Contract_AllPrice = 0;
                    }
                    MyBLL.EditContract(entity, Public.GetUserPrivilege());
                    ordersinfo.Orders_ContractID = 0;
                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        //初始合同卖家信息
                        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
                        if (ordersinfos == null)
                        {
                            entity.Contract_BuyerID = 0;
                            MyBLL.EditContract(entity, Public.GetUserPrivilege());
                        }
                        Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "意向合同订单移除", 1, "意向合同订单移除，移除订单号：" + ordersinfo.Orders_SN);
                        Public.Msg("positive", "操作成功", "操作成功！", true, "/contract/Contract_detail.aspx?contract_id=" + entity.Contract_ID);
                    }
                    else
                    {
                        Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "意向合同订单移除", 0, "意向合同订单移除，移除订单号：" + ordersinfo.Orders_SN);
                        Public.Msg("error", "操作失败", "操作失败！", false, "{back}");
                    }
                }
                else
                {
                    Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
                }
            }
            else
            {
                Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
        }
        else
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
    }

    //合同付款分期设置
    public void Contract_PayDivided_Set(int Contract_ID, int Divided_Amount)
    {
        string payment_name, payment_note;
        double payment_amount, pay_setamount;
        DateTime paymentline;
        double price = 0;
        pay_setamount = 0;
        ContractInfo CEtity = GetContractByID(Contract_ID);
        if (CEtity != null)
        {
            if (CEtity.Contract_SupplierID>0||CEtity.Contract_Source==0)
            {
                Public.Msg("error", "错误信息", "无效的合同编号", false, "/contract/Contract_list.aspx");
            }
            if (CEtity.Contract_Status > 0)
            {
                Public.Msg("error", "错误信息", "合同不支持编辑", false, "{back}");
            }
            price = CEtity.Contract_AllPrice;
        }
        else
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "/contract/Contract_list.aspx");
        }
        pay_setamount = price;
        ContractDividedPaymentInfo dividedpayment = null;

        IList<ContractDividedPaymentInfo> entitys = MyDividedpay.GetContractDividedPaymentsByContractID(CEtity.Contract_ID);
        if (entitys != null)
        {
            if (entitys.Count != Divided_Amount)
            {
                double num = 0;
                bool bz = false;
                int i = 0;
                foreach (ContractDividedPaymentInfo entity in entitys)
                {
                    i = i + 1;
                    if (i > Divided_Amount)
                    {
                        MyDividedpay.DelContractDividedPayment(entity.Payment_ID);
                    }
                    else
                    {
                        payment_name = tools.CheckStr(Request["payment_name_" + entity.Payment_ID]);
                        payment_note = tools.CheckStr(Request["payment_note_" + entity.Payment_ID]);
                        payment_amount = tools.CheckFloat(Request["payment_amount_" + entity.Payment_ID]);
                        paymentline = tools.NullDate(Request["payment_paymentline_" + entity.Payment_ID], DateTime.Now.ToShortDateString());
                        pay_setamount = pay_setamount - payment_amount;
                        if (pay_setamount >= 0)
                        {
                            entity.Payment_Name = payment_name;
                            entity.Payment_Note = payment_note;
                            if (payment_amount == 0)
                            {
                                entity.Payment_Amount = pay_setamount;
                                pay_setamount = 0;
                            }
                            else
                            {
                                entity.Payment_Amount = payment_amount;
                            }
                            entity.Payment_PaymentLine = paymentline;
                            MyDividedpay.EditContractDividedPayment(entity);
                        }
                        else
                        {
                            Public.Msg("error", "错误信息", "分期付款金额超过合同总金额，请重新分配！", false, "{back}");
                        }
                    }
                }
                for (i = 1; i <= (Divided_Amount - entitys.Count); i++)
                {
                    dividedpayment = new ContractDividedPaymentInfo();
                    dividedpayment.Payment_ContractID = CEtity.Contract_ID;
                    dividedpayment.Payment_Name = "";
                    dividedpayment.Payment_Amount = pay_setamount;
                    pay_setamount = 0;
                    dividedpayment.Payment_PaymentStatus = 0;
                    dividedpayment.Payment_PaymentLine = DateTime.Now;
                    dividedpayment.Payment_Note = "";
                    dividedpayment.Payment_PaymentTime = DateTime.Now;
                    MyDividedpay.AddContractDividedPayment(dividedpayment);
                }

                Contract_Log_Add(CEtity.Contract_ID, Session["User_Name"].ToString(), "调整合同付款分期", 1, "调整合同付款分期由" + entitys.Count + "期为" + Divided_Amount + "期");
            }
            else
            {
                double num = 0;
                bool bz = false;
                foreach (ContractDividedPaymentInfo entity in entitys)
                {
                    payment_name = tools.CheckStr(Request["payment_name_" + entity.Payment_ID]);
                    payment_note = tools.CheckStr(Request["payment_note_" + entity.Payment_ID]);
                    payment_amount = tools.CheckFloat(Request["payment_amount_" + entity.Payment_ID]);
                    paymentline = tools.NullDate(Request["payment_paymentline_" + entity.Payment_ID], DateTime.Now.ToShortDateString());
                    pay_setamount = pay_setamount - payment_amount;
                    if (pay_setamount >= 0)
                    {
                        entity.Payment_Name = payment_name;
                        entity.Payment_Note = payment_note;
                        if (payment_amount == 0)
                        {
                            entity.Payment_Amount = pay_setamount;
                            pay_setamount = 0;
                        }
                        else
                        {
                            entity.Payment_Amount = payment_amount;
                        }
                        entity.Payment_PaymentLine = paymentline;
                        MyDividedpay.EditContractDividedPayment(entity);

                    }
                    else
                    {
                        Public.Msg("error", "错误信息", "分期付款金额超过合同总金额，请重新分配！", false, "{back}");
                    }

                }
                Contract_Log_Add(CEtity.Contract_ID, Session["User_Name"].ToString(), "调整合同付款分期", 1, "调整合同付款分期信息");
            }
        }
        if (CEtity.Contract_Confirm_Status > 0)
        {
            CEtity.Contract_Confirm_Status = 0;
            if (MyBLL.EditContract(CEtity, Public.GetUserPrivilege()))
            {
                Contract_Log_Add(CEtity.Contract_ID, "", "调整合同付款分期", 1, "合同付款分期信息编辑，双方未确认");
            }
        }
    }

    //合同付款分期
    public void Contract_Divided_Set()
    {
        int Contract_ID = tools.CheckInt(Request["Contract_ID"]);
        int Contract_Divided = tools.CheckInt(Request["Contract_Divided"]);

        if (Contract_Divided < 1 || Contract_Divided > 7)
        {

            Public.Msg("error", "错误信息", "合同付款分期期数在1-7之间", false, "{back}");
            return;
        }
        Contract_PayDivided_Set(Contract_ID, Contract_Divided);
        Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + Contract_ID);
    }

    //提交意向合同修改意向
    public void TmpContract_NoteEdit()
    {
        string contract_note, contract_sn;
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        contract_note = tools.CheckStr(Request["Contract_Note"]);
        string attachment_file = tools.CheckStr(Request.Form["attachment_file"]);

        if (contract_sn == "")
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        if (contract_note == "")
        {
            Public.Msg("info", "提示信息", "请填写您要提交的信息", false, "{back}");
        }
        ContractInfo entity = GetContractBySn(contract_sn);
        if (entity != null)
        {
            if (entity.Contract_SupplierID>0)
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "/Contract/Contract_list.aspx");
            }
            if (entity.Contract_Status > 1)
            {
                Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
            if (entity.Contract_Status == 0)
            {
                if (attachment_file != null && attachment_file.Length > 0)
                {
                    contract_note += "  合同附件：<a href=\"" + Convert.ToString(Application["Upload_Server_URL"]).TrimEnd('/') + attachment_file + "\" target=\"_blank\">点此查看</a>";
                }

                Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "提交意向合同修改意向", 1, "提交意见合同修改意向：" + contract_note);
            }
            else
            {
                Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "提交合同咨询", 1, "提交合同咨询：" + contract_note);
            }
            Public.Msg("positive", "操作成功", "操作成功！", true, "contract_detail.aspx?contract_id=" + entity.Contract_ID);

        }
        else
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
    }

    //合同关闭
    public void Contract_Close()
    {
        string contract_sn, contract_close_note;
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        contract_close_note = tools.CheckStr(Request["contract_close_note"]);
        if (contract_sn == "")
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");

        }
        else
        {
            ContractInfo entity = GetContractBySn(contract_sn);
            if (entity != null)
            {
                if (entity.Contract_Source == 0)
                {
                    Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "/contract/Contract_list.aspx");
                }
                IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
                if (entity.Contract_Status == 0 && ordersinfos == null)
                {
                    entity.Contract_Status = 3;
                    MyBLL.EditContract(entity, Public.GetUserPrivilege());
                    Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "意向合同取消", 1, "意向合同取消，取消原因：" + contract_close_note);
                    Public.Msg("positive", "操作成功", "操作成功！", false, "/contract/contract_detail.aspx?contract_id=" + entity.Contract_ID);
                }
                else
                {
                    Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
                }
            }
            else
            {
                Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
        }
    }

    //意向合同平台确认
    public void TmpContract_SysConfirm()
    {
        int contract_id;
        contract_id = tools.CheckInt(Request["contract_id"]);
        if (contract_id == 0)
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");

        }
        ContractInfo entity = GetContractByID(contract_id);
        if (entity == null)
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        if (entity.Contract_SupplierID > 0)
        {
            Public.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }
        //检查合同状态
        if (entity.Contract_Status > 0)
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        //检查是否包括订单
        IList<OrdersInfo> ordersinfos = GetOrderssByContractID(entity.Contract_ID);
        if (ordersinfos == null)
        {
            Public.Msg("info", "提示信息", "合同中没有附加订单信息", false, "{back}");
        }
        //检查是否分期付款金额
        IList<ContractDividedPaymentInfo> dividedpayments = MyDividedpay.GetContractDividedPaymentsByContractID(entity.Contract_ID);
        if (dividedpayments == null)
        {
            Public.Msg("info", "提示信息", "您还没有选择付款分期信息", false, "{back}");
        }
        else
        {
            double totaldivide = 0;
            foreach (ContractDividedPaymentInfo dividedpayment in dividedpayments)
            {
                totaldivide += dividedpayment.Payment_Amount;
            }
            if (totaldivide != entity.Contract_AllPrice)
            {
                Public.Msg("info", "提示信息", "合同分期金额与合同总价不一致！", false, "{back}");
            }
        }
        int Confirm_Status = entity.Contract_Confirm_Status;
        entity.Contract_Confirm_Status = 2;
        if (MyBLL.EditContract(entity, Public.GetUserPrivilege()))
        {
            Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "卖方意向合同确认", 1, "卖方意向合同确认");

            if (Confirm_Status > 0)
            {
                TmpContract_To_Contract(entity);
            }
            Public.Msg("positive", "操作成功", "操作成功！", false, "/contract/contract_detail.aspx?contract_id=" + contract_id);
        }
        else
        {
            Public.Msg("error", "提示信息", "操作失败", false, "{back}");
        }
    }

    //合同配货中
    public void Contract_Delivery_Prepare()
    {
        int contract_id;
        contract_id = tools.CheckInt(Request["contract_id"]);
        if (contract_id == 0)
        {
            Public.Msg("info", "提示信息", "合同无效", false, "{back}");

        }
        ContractInfo entity = GetContractByID(contract_id);
        if (entity == null)
        {
            Public.Msg("info", "提示信息", "合同无效", false, "{back}");
        }
        if (entity.Contract_SupplierID >0)
        {
            Public.Msg("error", "错误信息", "合同无效！", false, "{back}");
        }
        //检查合同状态
        if (entity.Contract_Status != 1 || entity.Contract_Delivery_Status > 0)
        {
            Public.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }

        entity.Contract_Delivery_Status = 1;
        if (MyBLL.EditContract(entity, Public.GetUserPrivilege()))
        {
            Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "合同配货中", 1, "合同正在配货中");
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contract_id);
        }
        else
        {
            Public.Msg("error", "提示信息", "操作失败", false, "{back}");
        }

    }

    //获取合同已支付金额
    public double Get_Contract_PayedAmount(int Contract_ID)
    {
        return MyBLL.Get_Contract_PayedAmount(Contract_ID);
    }

    //合同支付操作
    public void Contract_Payment_Action(string operate)
    {
        string Contract_SN = tools.CheckStr(Request["Contract_Sn"]);
        int paywayid = tools.CheckInt(Request["Contract_PaymentID"]);
        string Contract_Payment_Note = tools.CheckStr(Request["Contract_Payment_Note"]);
        double Contract_Payment_Amount = tools.CheckFloat(Request["Contract_Payment_Amount"]);
        bool Is_All = false;
        Contract_Payment_Amount = Math.Round(Contract_Payment_Amount, 2);
        if (Contract_SN.Length == 0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractInfo contractinfo = GetContractBySn(Contract_SN);

        if (contractinfo == null)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_SupplierID >0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }


        //付款
        if (operate == "create")
        {
            if (Contract_Payment_Amount <= 0)
            {
                Public.Msg("error", "错误信息", "请填写支付金额", false, "{back}");
                return;
            }
            if (paywayid == 0)
            {
                Public.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                return;
            }

            //合同状态验证
            if (contractinfo.Contract_Status != 1 || contractinfo.Contract_Payment_Status > 1)
            {
                Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
            }

            //支付金额验证
            double Contract_PayedAmount = Get_Contract_PayedAmount(contractinfo.Contract_ID);
            if (Contract_Payment_Amount > contractinfo.Contract_AllPrice - Contract_PayedAmount)
            {
                Public.Msg("error", "错误信息", "支付金额超过合同总价", false, "{back}");
                return;
            }

            PayWayInfo payway = MyPayway.GetPayWayByID(paywayid, Public.GetUserPrivilege());
            if (payway == null)
            {
                Public.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                return;
            }

            string StrAttachment = tools.CheckStr(Request["Contract_Payment_Attachment"]);
            if (payway.Pay_Way_Type == 0 && StrAttachment.Length == 0)
            {
                Public.Msg("error", "错误信息", "请上传支付凭据", false, "{back}");
                return;
            }

            IList<ContractDividedPaymentInfo> entitys = MyDividedpay.GetContractDividedPaymentsByContractID(contractinfo.Contract_ID);

            int PaymentStatus = 1;
            
            //生成支付单
            Create_Contract_Payment(contractinfo.Contract_ID, Contract_Payment_Note, payway.Pay_Way_Name, Contract_Payment_Amount, tools.NullInt(Session["User_ID"]), PaymentStatus, StrAttachment);

            if (entitys != null)
            {
                Contract_PayedAmount = Get_Contract_PayedAmount(contractinfo.Contract_ID);
                double total_price = 0;
                foreach (ContractDividedPaymentInfo entity1 in entitys)
                {
                    //更改分期支付状态
                    total_price += entity1.Payment_Amount;
                    if (total_price <= Contract_PayedAmount)
                    {
                        entity1.Payment_PaymentStatus = 2;
                        entity1.Payment_PaymentTime = DateTime.Now;
                        MyDividedpay.EditContractDividedPayment(entity1);
                    }
                    else
                    {
                        if ((total_price - entity1.Payment_Amount) < Contract_PayedAmount)
                        {
                            entity1.Payment_PaymentStatus = 1;
                            entity1.Payment_PaymentTime = DateTime.Now;
                            MyDividedpay.EditContractDividedPayment(entity1);
                        }
                        break;
                    }
                }
            }

            //添加支付日志
            Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同支付", 1, "支付成功，支付金额：" + Public.DisplayCurrency(Contract_Payment_Amount) + "");

            entitys = MyDividedpay.GetContractDividedPaymentsByContractID(contractinfo.Contract_ID);
            if (entitys != null)
            {
                Is_All = true;
                foreach (ContractDividedPaymentInfo entity1 in entitys)
                {
                    if (entity1.Payment_PaymentStatus < 2)
                    {
                        Is_All = false;
                    }
                }
                if (Is_All == true)
                {
                    //更新合同支付状态为全部已支付
                    contractinfo.Contract_Payment_Status = 2;
                    MyBLL.EditContract(contractinfo, Public.GetUserPrivilege());
                    Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同全部支付", 1, "合同全部支付完毕");
                }
                else
                {
                    //更新合同支付状态为部分已支付
                    contractinfo.Contract_Payment_Status = 1;
                    MyBLL.EditContract(contractinfo, Public.GetUserPrivilege());
                }

            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contractinfo.Contract_ID);
        }
    }

    public void Contract_Payment_Already()
    {
        string Contract_SN = tools.CheckStr(Request["Contract_Sn"]);
        string Payment_SN = tools.CheckStr(Request["Payment_SN"]);
        if (Contract_SN.Length == 0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractInfo contractinfo = GetContractBySn(Contract_SN);

        if (contractinfo == null)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_SupplierID > 0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractPaymentInfo paymentinfo = MyPayment.GetContractPaymentBySN(Payment_SN);
        if (paymentinfo == null)
        {
            Public.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }
        if (paymentinfo.Contract_Payment_PaymentStatus != 0 || paymentinfo.Contract_Payment_ContractID != contractinfo.Contract_ID)
        {
            Public.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }

        paymentinfo.Contract_Payment_PaymentStatus = 1;

        if (MyPayment.EditContractPayment(paymentinfo))
        {
            Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同支付已确认", 1, "付款单 " + paymentinfo.Contract_Payment_DocNo + " 支付金额：" + Public.DisplayCurrency(paymentinfo.Contract_Payment_Amount) + " 确认已支付");

            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contractinfo.Contract_ID);
        }
        else
        {
            Public.Msg("error", "操作失败", "操作失败，请稍后再试！", false, "{back}");
        }
    }

    //合同付款到账
    public void Contract_Payment_Reach()
    {
        string Contract_SN = tools.CheckStr(Request["Contract_Sn"]);
        string Payment_SN = tools.CheckStr(Request["Payment_SN"]);
        if (Contract_SN.Length == 0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractInfo contractinfo = GetContractBySn(Contract_SN);

        if (contractinfo == null)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_SupplierID>0)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractPaymentInfo paymentinfo = MyPayment.GetContractPaymentBySN(Payment_SN);
        if (paymentinfo == null)
        {
            Public.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }
        if (paymentinfo.Contract_Payment_PaymentStatus != 1 || paymentinfo.Contract_Payment_ContractID != contractinfo.Contract_ID)
        {
            Public.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }

        paymentinfo.Contract_Payment_PaymentStatus = 2;
        if (MyPayment.EditContractPayment(paymentinfo))
        {
            Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同支付已到帐", 1, "付款单 " + paymentinfo.Contract_Payment_DocNo + " 支付金额：" + Public.DisplayCurrency(paymentinfo.Contract_Payment_Amount) + "确认已到帐");

            //验证是否合同全部支付到账
            if (contractinfo.Contract_Payment_Status == 2)
            {
                bool Is_All = true;
                IList<ContractPaymentInfo> paymentinfos = MyPayment.GetContractPaymentsByContractID(contractinfo.Contract_ID);
                if (paymentinfos != null)
                {
                    foreach (ContractPaymentInfo pinfo in paymentinfos)
                    {
                        if (pinfo.Contract_Payment_PaymentStatus == 1)
                        {
                            Is_All = false;
                        }
                    }
                    if (Is_All == true)
                    {
                        contractinfo.Contract_Payment_Status = 3;
                        MyBLL.EditContract(contractinfo, Public.GetUserPrivilege());
                        Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同全部到账", 1, "合同全部支付并确认到账");
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contractinfo.Contract_ID);
        }
        else
        {
            Public.Msg("error", "操作失败", "操作失败，请稍后再试！", false, "{back}");
        }
    }


    //生成支付单号
    public string Contract_payment_sn()
    {
        string sn = "";
        bool ismatch = true;
        ContractPaymentInfo paymentinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(5);
        while (ismatch == true)
        {
            paymentinfo = MyPayment.GetContractPaymentBySN(sn);
            if (paymentinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    //生成付款单
    public void Create_Contract_Payment(int Contract_ID, string pay_note, string Payment_Name, double Pay_Amount, int SysUserID, int PaymentStatus, string StrAttachment)
    {
        string payment_doc;
        payment_doc = Contract_payment_sn();
        ContractPaymentInfo entity = new ContractPaymentInfo();
        entity.Contract_Payment_ContractID = Contract_ID;
        entity.Contract_Payment_PaymentStatus = PaymentStatus;
        entity.Contract_Payment_SysUserID = SysUserID;
        entity.Contract_Payment_DocNo = payment_doc;
        entity.Contract_Payment_Name = Payment_Name;
        entity.Contract_Payment_Amount = Pay_Amount;
        entity.Contract_Payment_Note = pay_note;
        entity.Contract_Payment_Addtime = DateTime.Now;
        entity.Contract_Payment_Site = Public.GetCurrentSite();
        entity.Contract_Payment_Attachment = StrAttachment;
        if (MyPayment.AddContractPayment(entity))
        {

            Contract_Log_Add(Contract_ID, Session["User_Name"].ToString(), "生成付款单", 1, "合同支付并生成付款单，付款单号：" + payment_doc + ",付款金额：" + Public.DisplayCurrency(Pay_Amount));
        }


    }

    //合同订单所有产品
    public string Contract_Orders_Goods(int Contract_ID)
    {
        string strHTML = "";
        IList<OrdersGoodsInfo> GoodsListAll = null;
        int freighted_amount;
        int icount = 0;
        strHTML += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cell_table\">";
        strHTML += "<tr>";
        strHTML += "<td width=\"80\" height=\"23\" align=\"center\" class=\"cell_title1\">订单号</td>";
        strHTML += "<td width=\"80\" height=\"23\" align=\"center\" class=\"cell_title1\">产品编号</td>";
        strHTML += "<td height=\"23\" align=\"center\" class=\"cell_title1\">产品名称</td>";
        strHTML += "<td width=\"70\" height=\"23\" align=\"center\" class=\"cell_title1\">规格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">价格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">未发数量</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">已发数量</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">本次发货</td>";
        strHTML += "</tr>";
        IList<OrdersInfo> ordersinfos = GetOrderssByContractID(Contract_ID);

        if (ordersinfos != null)
        {
            foreach (OrdersInfo order in ordersinfos)
            {
                GoodsListAll = MyOrders.GetGoodsListByOrderID(order.Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo entity in GoodsListAll)
                    {
                        if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                        {
                            freighted_amount = 0;
                        }
                        else
                        {
                            icount = icount + 1;
                            freighted_amount = MyFreight.Get_Orders_Goods_DeliveryAmount(entity.Orders_Goods_ID);
                            strHTML += "<tr>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + order.Orders_SN + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Code + "</td>";
                            strHTML += "<td height=\"23\" align=\"left\" class=\"cell_content\">" + entity.Orders_Goods_Product_Name + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Spec + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + (entity.Orders_Goods_Amount - freighted_amount) + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + freighted_amount + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\"><input type=\"text\" size=\"10\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo');if($(this).val()>" + (entity.Orders_Goods_Amount - freighted_amount) + "){$(this).val('0');}\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" name=\"freight_amount_" + icount + "\"><input type=\"hidden\" name=\"nofreight_amount_" + icount + "\" value=\"" + (entity.Orders_Goods_Amount - freighted_amount) + "\">";
                            strHTML += "<input type=\"hidden\" name=\"goods_id_" + icount + "\" value=\"" + entity.Orders_Goods_ID + "\"><input type=\"hidden\" name=\"product_id_" + icount + "\" value=\"" + entity.Orders_Goods_Product_ID + "\"></td>";
                            strHTML += "</tr>";
                        }
                    }
                }
            }
        }
        else
        {
            strHTML += "<tr><td height=\"30\" colspan=\"8\">合同中暂无订单信息！</td></tr>";
        }
        strHTML += "</table><input type=\"hidden\" name=\"goods_amount\" value=\"" + icount + "\">";
        return strHTML;
    }

    //合同发货单产品
    public bool Contract_Delivery_Goods_List(int Contract_ID, int Delivery_ID,int ispreview)
    {
        bool isaccept = true;
        string strHTML = "";
        int freighted_amount;
        int icount = 0;
        strHTML += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cell_table\">";
        strHTML += "<tr>";
        strHTML += "<td width=\"80\" height=\"23\" align=\"center\" class=\"cell_title1\">订单号</td>";
        strHTML += "<td width=\"80\" height=\"23\" align=\"center\" class=\"cell_title1\">产品编号</td>";
        strHTML += "<td height=\"23\" align=\"center\" class=\"cell_title1\">产品名称</td>";
        strHTML += "<td width=\"70\" height=\"23\" align=\"center\" class=\"cell_title1\">规格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">价格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">发货数量</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">签收数量</td>";

        strHTML += "</tr>";
        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
        ContractDeliveryInfo deliveryinfo = MyFreight.GetContractDeliveryByID(Delivery_ID);
        if (deliveryinfo == null)
        {
            return true;
        }
        IList<ContractDeliveryGoodsInfo> GoodsListAll = MyFreight.GetContractDeliveryGoodssByDeliveryID(Delivery_ID);
        string orders_sn;
        if (GoodsListAll != null)
        {
            foreach (ContractDeliveryGoodsInfo entity1 in GoodsListAll)
            {
                OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(entity1.Delivery_Goods_GoodsID);
                if (entity != null)
                {
                    orders_sn = "";
                    if (ordersinfos != null)
                    {
                        foreach (OrdersInfo orderinfo in ordersinfos)
                        {
                            if (orderinfo.Orders_ID == entity.Orders_Goods_OrdersID)
                            {
                                orders_sn = orderinfo.Orders_SN;
                                break;
                            }
                        }
                    }
                    freighted_amount = MyFreight.Get_Orders_Goods_DeliveryAmount(entity.Orders_Goods_ID);
                    strHTML += "<tr>";
                    strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + orders_sn + "</td>";
                    strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Code + "</td>";
                    strHTML += "<td height=\"23\" align=\"left\" class=\"cell_content\">" + entity.Orders_Goods_Product_Name + "</td>";
                    strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Spec + "</td>";
                    strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                    strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity1.Delivery_Goods_Amount + "</td>";
                    if (deliveryinfo.Contract_Delivery_DeliveryStatus == 1 && ispreview==0)
                    {
                        isaccept = false;
                        strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\"><input type=\"text\" size=\"10\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo');if($(this).val()>" + entity1.Delivery_Goods_Amount + "){$(this).val('0');}\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" name=\"accept_amount_" + entity1.Delivery_Goods_ID + "\"></td>";
                    }
                    else
                    {
                        strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity1.Delivery_Goods_AcceptAmount + "</td>";
                    }
                    strHTML += "</tr>";
                }
            }
        }
        else
        {
            strHTML += "<tr><td height=\"30\" colspan=\"8\">暂无发货信息！</td></tr>";
        }
        strHTML += "</table>";
        Response.Write(strHTML);
        return isaccept;
    }

    public string Contract_Delivery_Goods_Mail(string Contract_SN, int Delivery_ID)
    {
        string strHTML = "";
        IList<ContractDeliveryGoodsInfo> GoodsList = MyFreight.GetContractDeliveryGoodssByDeliveryID(Delivery_ID);
        if (GoodsList == null)
        {
            return "";
        }

        strHTML = strHTML + "        <table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"5\">";
        strHTML = strHTML + "          <tr>";
        strHTML = strHTML + "            <td><table cellpadding=\"2\" width=\"100%\" cellspacing=\"1\" bgcolor=\"#EEBB31\">";
        strHTML = strHTML + "              <tr>";
        strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\" height=\"20\">商品编号</td>";
        strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">商品名称</td>";
        strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">规格</td>";
        strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">价格</td>";
        strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\" width=\"60\">发货数量</td>";
        strHTML = strHTML + "              </tr>";
        foreach (ContractDeliveryGoodsInfo Goodsinfo in GoodsList)
        {
            OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(Goodsinfo.Delivery_Goods_GoodsID);
            if (entity != null)
            {
                strHTML = strHTML + "        <tr>";
                strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Code + "</td>";
                strHTML = strHTML + "          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
                strHTML = strHTML + "            <tr>";
                strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"" + tools.NullStr(Application["site_url"]) + "/product/detail.aspx?product_id=" + entity.Orders_Goods_Product_ID + "\"><strong>" + entity.Orders_Goods_Product_Name + "</strong></a></td>";
                strHTML = strHTML + "            </tr>";
                strHTML = strHTML + "          </table></td>";
                strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Spec + "</td>";
                strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + Goodsinfo.Delivery_Goods_Amount + "</td>";
                strHTML = strHTML + "</tr>";
            }
        }
        strHTML = strHTML + "            </table></td>";
        strHTML = strHTML + "          </tr>";
        strHTML = strHTML + "        </table>";

        return strHTML;
    }

    //物流公司选择
    public string Delivery_Company_Select(string select_name, string Code)
    {
        string way_list = "";
        way_list = "<select name=\"" + select_name + "\" " + Code + ">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));
        IList<DeliveryWayInfo> deliveryways = MyDelivery.GetDeliveryWays(Query, Public.GetUserPrivilege());
        if (deliveryways != null)
        {
            foreach (DeliveryWayInfo entity in deliveryways)
            {
                way_list = way_list + "  <option value=\"" + entity.Delivery_Way_ID + "\">" + entity.Delivery_Way_Name + "</option>";
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    //生成配送单号
    public string Contract_Delivery_SN()
    {
        string sn = "";
        bool ismatch = true;
        ContractDeliveryInfo deliveryinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(5);
        while (ismatch == true)
        {
            deliveryinfo = MyFreight.GetContractDeliveryBySN(sn);
            if (deliveryinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    //邮件模版
    public string mail_template(string template_name, string member_email, string contract_sn, int delivery_id)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "contract_freight":
                mailbody = "<p>感谢您通过{sys_config_site_name}购物，发货商品信息如下：</p>";
                mailbody = mailbody + "<p>" + Contract_Delivery_Goods_Mail(contract_sn, delivery_id) + "</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/supplier/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

        }
        mailbody = mailbody.Replace("{member_email}", member_email);
        return mailbody;
    }

    //合同发货操作
    public void Contract_Dilivery_Action(string operate)
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        ContractInfo contractinfo = GetContractBySn(contract_sn);
        DeliveryWayInfo deliverywayinfo = null;
        ContractDeliveryGoodsInfo deliverygoods = null;
        string delivery_name = "";
        int delivery_id, goods_amount, i;
        if (contractinfo == null)
        {
            Public.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_Status != 1)
        {
            Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }
        if (operate == "create")
        {
            //生成发货单

            if (contractinfo.Contract_SupplierID>0)
            {
                Public.Msg("error", "错误信息", "合同无效！", false, "{back}");
            }

            string freight_company, freight_code, freight_note;
            double freight_fee;
            bool iscontain_freight = false;
            delivery_id = tools.CheckInt(Request["contract_delivery"]);
            freight_company = tools.CheckStr(Request["Contract_Delivery_CompanyName"]);
            freight_note = tools.CheckStr(Request["Contract_Delivery_Note"]);
            freight_code = tools.CheckStr(Request["Contract_Delivery_Code"]);
            freight_fee = tools.CheckFloat(Request["Contract_Delivery_Amount"]);
            goods_amount = tools.CheckInt(Request["goods_amount"]);

            //检查配送方式
            if (delivery_id == 0)
            {
                Public.Msg("error", "错误信息", "无效的配送方式", false, "{back}");
                return;
            }
            deliverywayinfo = MyDelivery.GetDeliveryWayByID(delivery_id, Public.GetUserPrivilege());
            if (deliverywayinfo != null)
            {
                delivery_name = deliverywayinfo.Delivery_Way_Name;
            }
            else
            {
                Public.Msg("error", "错误信息", "无效的配送方式", false, "{back}");
            }

            //判断是否包含发货信息
            if (goods_amount > 0)
            {
                for (i = 1; i <= goods_amount; i++)
                {
                    if (tools.CheckInt(Request["goods_id_" + i]) > 0 && tools.CheckFloat(Request["freight_amount_" + i]) > 0 && tools.CheckFloat(Request["freight_amount_" + i]) <= tools.CheckFloat(Request["nofreight_amount_" + i]))
                    {
                        OrdersGoodsInfo GoodsEntity = MyOrders.GetOrdersGoodsByID(tools.CheckInt(Request["goods_id_" + i]));

                        if (GoodsEntity != null)
                        {
                            double freighted_amount = MyFreight.Get_Orders_Goods_DeliveryAmount(GoodsEntity.Orders_Goods_ID);
                            double nofreight = Math.Round(GoodsEntity.Orders_Goods_Amount - freighted_amount, 2);
                            if (nofreight >= tools.CheckFloat(Request["freight_amount_" + i]))
                            {
                                iscontain_freight = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "合同中没有需要发货的产品信息", false, "{back}");
                return;
            }

            if (iscontain_freight == false)
            {
                Public.Msg("error", "错误信息", "合同中没有需要发货的产品信息", false, "{back}");
                return;
            }





            string delivery_doc;

            delivery_doc = Contract_Delivery_SN();
            ContractDeliveryInfo entity = new ContractDeliveryInfo();
            entity.Contract_Delivery_ContractID = contractinfo.Contract_ID;
            entity.Contract_Delivery_DeliveryStatus = 1;
            entity.Contract_Delivery_SysUserID = tools.NullInt(Session["User_ID"]);
            entity.Contract_Delivery_DocNo = delivery_doc;
            entity.Contract_Delivery_Name = delivery_name;
            entity.Contract_Delivery_Amount = freight_fee;
            entity.Contract_Delivery_Note = freight_note;
            entity.Contract_Delivery_Code = freight_code;
            entity.Contract_Delivery_CompanyName = freight_company;
            entity.Contract_Delivery_Addtime = DateTime.Now;
            entity.Contract_Delivery_AccpetNote = "";
            entity.Contract_Delivery_Site = Public.GetCurrentSite();
            if (MyFreight.AddContractDelivery(entity))
            {
                Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "生成配送单", 1, "合同生成配送单，配送单号：" + delivery_doc);
                entity = MyFreight.GetContractDeliveryBySN(delivery_doc);
                if (entity != null)
                {
                    iscontain_freight = true;

                    for (i = 1; i <= goods_amount; i++)
                    {
                        if (tools.CheckInt(Request["goods_id_" + i]) > 0 && tools.CheckFloat(Request["freight_amount_" + i]) > 0)
                        {
                            //生成配送单发货产品
                            deliverygoods = new ContractDeliveryGoodsInfo();
                            deliverygoods.Delivery_Goods_DeliveryID = entity.Contract_Delivery_ID;
                            deliverygoods.Delivery_Goods_GoodsID = tools.CheckInt(Request["goods_id_" + i]);
                            deliverygoods.Delivery_Goods_Amount = tools.CheckInt(Request["freight_amount_" + i]);
                            deliverygoods.Delivery_Goods_AcceptAmount = 0;
                            deliverygoods.Delivery_Goods_Status = 0;
                            deliverygoods.Delivery_Goods_Unit = tools.CheckStr(Request["unit_" + i]);
                            if (MyFreight.AddContractDeliveryGoods(deliverygoods))
                            {
                                //实际库存扣除
                                Contract_ProductStockAction(deliverygoods.Delivery_Goods_GoodsID, deliverygoods.Delivery_Goods_Amount, "del");
                                //减少可用库存
                                Contract_ProductCountAction(deliverygoods.Delivery_Goods_GoodsID, deliverygoods.Delivery_Goods_Amount, "del");
                            }

                        }
                        //检测是否为全部发货
                        if (tools.CheckInt(Request["goods_id_" + i]) > 0 && tools.CheckFloat(Request["nofreight_amount_" + i]) > 0 && tools.CheckFloat(Request["freight_amount_" + i]) != tools.CheckFloat(Request["nofreight_amount_" + i]))
                        {
                            iscontain_freight = false;
                        }

                    }
                    //更新合同发货状态
                    if (iscontain_freight)
                    {
                        contractinfo.Contract_Delivery_Status = 3;
                        MyBLL.EditContract(contractinfo, Public.GetUserPrivilege());
                        Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同已全部发货", 1, "合同全部发货完毕");
                    }
                    else
                    {
                        contractinfo.Contract_Delivery_Status = 2;
                        MyBLL.EditContract(contractinfo, Public.GetUserPrivilege());
                        Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同已部分发货", 1, "合同部分发货完毕");
                    }
                    string supplier_email = "";
                    SupplierInfo supplierinfo = MySupplier.GetSupplierByID(contractinfo.Contract_BuyerID,Public.GetUserPrivilege());
                    if (supplierinfo != null)
                    {
                        supplier_email = supplierinfo.Supplier_Email;
                    }

                    //sms.Send(orderinfo.Orders_Address_Mobile, "您的合同编号" + contractinfo.CONTRACT_SN + "订购产品已发货，请注意查收。" + tools.NullStr(Application["site_name"]));
                    //sms.Unbind();

                    //发送订单邮件
                    if (supplier_email.Length > 0)
                    {
                        string mailsubject, mailbodytitle, mailbody;
                        mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单已发货";
                        mailbodytitle = mailsubject;
                        mailbody = mail_template("contract_freight", "", contractinfo.Contract_SN, entity.Contract_Delivery_ID);
                        member.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
                    }

                }
                Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contractinfo.Contract_ID);
            }
            else
            {
                Public.Msg("error", "错误信息", "配送单生成失败！", false, "{back}");
                return;
            }
        }
        

    }

    //产品实际库存操作
    public void Contract_ProductStockAction(int Goods_ID, int Goods_Amount, string action)
    {
        int Product_ID, Goods_Type;
        string SqlAdd = "";
        int Config_DefaultSupplier = 0;
        int Config_DefaultDepot = 0;

        string SqlList = "SELECT TOP 1 * FROM SCM_Config ORDER BY Config_ID DESC";
        System.Data.SqlClient.SqlDataReader RdrList = null;
        bool recordExist = false;
        RdrList = DBHelper.ExecuteReader(SqlList);
        if (RdrList.Read())
        {
            Config_DefaultSupplier = tools.NullInt(RdrList["Config_DefaultSupplier"]);
            Config_DefaultDepot = tools.NullInt(RdrList["Config_DefaultDepot"]);
        }
        RdrList.Close();
        RdrList = null;
        OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(Goods_ID);
        if (entity != null)
        {
            Product_ID = entity.Orders_Goods_Product_ID;
            Goods_Type = entity.Orders_Goods_Type;
            switch (action)
            {
                //退货
                case "add":
                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        ProductInfo productinfo = MyProduct.GetProductByID(Product_ID,Public.GetUserPrivilege());
                        if (productinfo != null)
                        {
                            if (productinfo.Product_IsNoStock == 0)
                            {
                                //添加退货单
                                SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                "VALUES (3,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + entity.Orders_Goods_Amount + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,0,GETDATE(),'订单退货')";
                                try
                                {
                                    DBHelper.ExecuteNonQuery(SqlAdd);

                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, Goods_Amount, 0);
                                }
                                catch (Exception ex) { throw ex; }
                            }
                        }

                    }
                    break;
                //发货
                case "del":
                    Goods_Amount = 0 - Goods_Amount;
                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, Public.GetUserPrivilege());
                        if (productinfo != null)
                        {
                            if (productinfo.Product_IsNoStock == 0)
                            {
                                //添加出库单
                                SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                "VALUES (2,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + (0 - entity.Orders_Goods_Amount).ToString() + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,0,GETDATE(),'订单发货')";
                                try
                                {
                                    DBHelper.ExecuteNonQuery(SqlAdd);

                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, Goods_Amount, 0);
                                }
                                catch (Exception ex) { throw ex; }
                            }
                            else
                            {
                                //获取默认仓库及供应商

                                //添加入库单
                                SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                "VALUES (1," + Config_DefaultDepot + "," + Config_DefaultSupplier + ",'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + entity.Orders_Goods_Amount + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',1,1,GETDATE(),'订单发货（零库存进货）')";
                                try
                                {
                                    DBHelper.ExecuteNonQuery(SqlAdd);
                                }
                                catch (Exception ex) { throw ex; }
                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, entity.Orders_Goods_Amount, 0);
                                //添加出库单
                                SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                "VALUES (2,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + (0 - entity.Orders_Goods_Amount).ToString() + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,1,GETDATE(),'订单发货（零库存发货）')";
                                try
                                {
                                    DBHelper.ExecuteNonQuery(SqlAdd);
                                }
                                catch (Exception ex) { throw ex; }
                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, 0 - entity.Orders_Goods_Amount, 0);
                            }
                        }
                    }
                    break;
            }
        }
    }

    //产品可用库存操作
    public void Contract_ProductCountAction(int Goods_ID, int Goods_Amount, string action)
    {
        int Product_ID, Goods_Type;
        OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(Goods_ID);
        if (entity != null)
        {
            Product_ID = entity.Orders_Goods_Product_ID;
            Goods_Type = entity.Orders_Goods_Type;
            switch (action)
            {
                case "add":
                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        MyProduct.UpdateProductStockExcepNostock(Product_ID, 0, Goods_Amount);
                    }
                    break;
                case "del":
                    Goods_Amount = 0 - Goods_Amount;
                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        MyProduct.UpdateProductStockExcepNostock(Product_ID, 0, Goods_Amount);
                    }
                    break;
            }
        }
    }

    public ContractDeliveryInfo GetContractDeliveryBySN(string SN)
    {
        return MyFreight.GetContractDeliveryBySN(SN);
    }

    //签收
    public void Contract_Delivery_Accept()
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        string contract_delivery = tools.CheckStr(Request["contract_delivery"]);
        if (contract_sn == "")
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/index.aspx");
        }

        if (contract_delivery == "")
        {
            Public.Msg("error", "错误信息", "无效的发货单编号", false, "/supplier/index.aspx");
        }

        ContractInfo contractinfo = GetContractBySn(contract_sn);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_SupplierID >0)
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "/supplier/index.aspx");
            }
        }
        else
        {
            contract_sn = "";
        }

        if (contract_sn == "")
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "/contract/contract_list.aspx");
        }

        ContractDeliveryInfo deliveryinfo = GetContractDeliveryBySN(contract_delivery);
        if (deliveryinfo == null)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        //验证发货单与合同的一致性
        if (deliveryinfo.Contract_Delivery_ContractID != contractinfo.Contract_ID)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        //验证发货单发货状态
        if (deliveryinfo.Contract_Delivery_DeliveryStatus != 1)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        int amount = 0;
        IList<ContractDeliveryGoodsInfo> entitys = MyFreight.GetContractDeliveryGoodssByDeliveryID(deliveryinfo.Contract_Delivery_ID);
        if (entitys != null)
        {
            foreach (ContractDeliveryGoodsInfo entity in entitys)
            {
                amount = tools.CheckInt(Request["accept_amount_" + entity.Delivery_Goods_ID]);
                if (amount > 0 && amount != entity.Delivery_Goods_AcceptAmount && amount <= entity.Delivery_Goods_Amount)
                {
                    if (amount < entity.Delivery_Goods_Amount)
                    {
                        //部分签收
                        entity.Delivery_Goods_Status = 1;
                    }
                    else
                    {
                        //全部签收
                        entity.Delivery_Goods_Status = 2;
                    }
                    entity.Delivery_Goods_AcceptAmount = amount;
                    if (MyFreight.EditContractDeliveryGoods(entity))
                    {
                        OrdersGoodsInfo goods = MyOrders.GetOrdersGoodsByID(entity.Delivery_Goods_GoodsID);
                        if (goods != null)
                        {
                            Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "发货单签收", 1, "产品名称：" + goods.Orders_Goods_Product_Name + "  签收数量：" + amount);
                        }
                    }
                }
            }
        }
        //验证是否整单签收
        bool isallaccept = true;
        entitys = MyFreight.GetContractDeliveryGoodssByDeliveryID(deliveryinfo.Contract_Delivery_ID);
        if (entitys != null)
        {
            foreach (ContractDeliveryGoodsInfo entity in entitys)
            {
                if (entity.Delivery_Goods_Status < 2)
                {
                    isallaccept = false;
                    break;
                }
            }
        }
        if (isallaccept)
        {
            deliveryinfo.Contract_Delivery_DeliveryStatus = 2;
            if (MyFreight.EditContractDelivery(deliveryinfo))
            {
                Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "发货单全部签收", 1, "发货单：" + deliveryinfo.Contract_Delivery_DocNo + "  已全部签收");

                IList<ContractDeliveryInfo> deliveryinfos = MyFreight.GetContractDeliverysByContractID(contractinfo.Contract_ID);
                if (deliveryinfos != null)
                {
                    foreach (ContractDeliveryInfo deliveryinfo1 in deliveryinfos)
                    {
                        if (deliveryinfo1.Contract_Delivery_DeliveryStatus < 2)
                        {
                            isallaccept = false;
                            break;
                        }
                    }
                }
                if (contractinfo.Contract_Delivery_Status != 3)
                {
                    isallaccept = false;
                }
                if (isallaccept)
                {
                    //更新合同签收状态
                    contractinfo.Contract_Delivery_Status = 4;
                    if (MyBLL.EditContract(contractinfo, Public.GetUserPrivilege()))
                    {
                        Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同全部签收", 1, "合同商品已全部签收");
                    }
                }
            }
        }
        Public.Msg("positive", "操作成功", "签收成功", true, "/contract/contract_freight_view.aspx?contract_id=" + contractinfo.Contract_ID+"&contract_delivery=" + deliveryinfo.Contract_Delivery_ID);
    }

    //全部签收
    public void Contract_Delivery_AllAccept()
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        string contract_delivery = tools.CheckStr(Request["contract_delivery"]);
        if (contract_sn == "")
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "/contract/contract_list.aspx");
        }

        if (contract_delivery == "")
        {
            Public.Msg("error", "错误信息", "无效的发货单编号", false, "/contract/contract_list.aspx");
        }

        ContractInfo contractinfo = GetContractBySn(contract_sn);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_SupplierID >0)
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "/contract/contract_list.aspx");
            }
        }
        else
        {
            contract_sn = "";
        }

        if (contract_sn == "")
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "/contract/contract_list.aspx");
        }

        ContractDeliveryInfo deliveryinfo = GetContractDeliveryBySN(contract_delivery);
        if (deliveryinfo == null)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        //验证发货单与合同的一致性
        if (deliveryinfo.Contract_Delivery_ContractID != contractinfo.Contract_ID)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        //验证发货单发货状态
        if (deliveryinfo.Contract_Delivery_DeliveryStatus != 1)
        {
            Public.Msg("error", "错误信息", "无效的发货单", false, "/contract/contract_list.aspx");
        }
        int amount = 0;
        IList<ContractDeliveryGoodsInfo> entitys = MyFreight.GetContractDeliveryGoodssByDeliveryID(deliveryinfo.Contract_Delivery_ID);
        if (entitys != null)
        {
            foreach (ContractDeliveryGoodsInfo entity in entitys)
            {
                amount = entity.Delivery_Goods_Amount;
                entity.Delivery_Goods_Status = amount;
                entity.Delivery_Goods_AcceptAmount = amount;
                if (MyFreight.EditContractDeliveryGoods(entity))
                {
                    OrdersGoodsInfo goods = MyOrders.GetOrdersGoodsByID(entity.Delivery_Goods_GoodsID);
                    if (goods != null)
                    {
                        Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "发货单签收", 1, "产品名称：" + goods.Orders_Goods_Product_Name + "  签收数量：" + amount);
                    }
                }
            }
        }
        deliveryinfo.Contract_Delivery_DeliveryStatus = 2;
        if (MyFreight.EditContractDelivery(deliveryinfo))
        {
            bool isallaccept = true;
            Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "发货单全部签收", 1, "发货单：" + deliveryinfo.Contract_Delivery_DocNo + "  已全部签收");
            IList<ContractDeliveryInfo> deliveryinfos = MyFreight.GetContractDeliverysByContractID(contractinfo.Contract_ID);
            if (deliveryinfos != null)
            {
                foreach (ContractDeliveryInfo deliveryinfo1 in deliveryinfos)
                {
                    if (deliveryinfo1.Contract_Delivery_DeliveryStatus < 2)
                    {
                        isallaccept = false;
                        break;
                    }
                }
            }
            if (contractinfo.Contract_Delivery_Status != 3)
            {
                isallaccept = false;
            }
            if (isallaccept)
            {
                //更新合同签收状态
                contractinfo.Contract_Delivery_Status = 4;
                if (MyBLL.EditContract(contractinfo, Public.GetUserPrivilege()))
                {
                    Contract_Log_Add(contractinfo.Contract_ID, Session["User_Name"].ToString(), "合同全部签收", 1, "合同商品已全部签收");
                }
            }
        }
        Public.Msg("positive", "操作成功", "签收成功", true, "/contract/Contract_freight_view.aspx?contract_delivery=" + deliveryinfo.Contract_Delivery_ID + "&contract_id=" + contractinfo.Contract_ID);
    }

    //合同完成
    public void Contract_Finish()
    {

        Supplier supplier = new Supplier();
        int Contract_id;
        Contract_id = tools.CheckInt(Request["Contract_id"]);
        if (Contract_id == 0)
        {
            Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
            return;
        }

        ContractInfo entity = GetContractByID(Contract_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        //验证合同卖家
        if (entity.Contract_SupplierID>0)
        {
            Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        //验证合同状态
        if (entity.Contract_Delivery_Status != 4 || entity.Contract_Payment_Status != 3 || entity.Contract_Status != 1)
        {
            Public.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
        if (ordersinfos != null)
        {
            foreach (OrdersInfo ordersinfo in ordersinfos)
            {
                //赠送积分
                if (ordersinfo.Orders_Total_Coin > 0)
                {
                    supplier.Supplier_Coin_AddConsume(ordersinfo.Orders_Total_Coin, "订单" + ordersinfo.Orders_SN + "完成赠送积分", ordersinfo.Orders_BuyerID, false);
                }

                //记录订单日志
                orderlog.Orders_Log(ordersinfo.Orders_ID, Session["User_Name"].ToString(), "完成", "成功", "订单交易完成");

                //更新产品销售
                orders.Orders_Product_Update_Salecount(ordersinfo.Orders_ID);

                //更新订单状态
                ordersinfo.Orders_Status = 2;
                ordersinfo.Orders_DeliveryStatus = 2;
                ordersinfo.Orders_IsReturnCoin = 1;
                MyOrders.EditOrders(ordersinfo);
            }
        }

        //改变合同状态
        entity.Contract_Status = 2;
        if (MyBLL.EditContract(entity, Public.GetUserPrivilege()))
        {
            Contract_Log_Add(entity.Contract_ID, Session["User_Name"].ToString(), "合同交易完成", 1, "合同交易完成");
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + entity.Contract_ID);
        }
        else
        {
            Public.Msg("error", "操作失败", "操作失败", false, "{back}");
        }
    }

    //获取合同已开票金额
    public double Get_Contract_InvoiceAmount(int Contract_ID)
    {
        double amount = 0;
        IList<ContractInvoiceApplyInfo> entitys = MyBLL.GetContractInvoiceApplysByContractID(Contract_ID);
        if (entitys != null)
        {
            foreach (ContractInvoiceApplyInfo entity in entitys)
            {
                amount = amount + entity.Invoice_Apply_Amount;
            }
        }
        return amount;
    }

    //更改发票状态
    public void UpdateInvoiceStatus()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        int contract_id = tools.CheckInt(Request["contract_id"]);
        ContractInfo contractinfo = GetContractByID(contract_id);
        if (contractinfo == null)
        {
            Public.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
        }
        if (contractinfo.Contract_Status == 0 || contractinfo.Contract_SupplierID > 0)
        {
            Public.Msg("error", "错误信息", "您不能对此合同执行该操作！", false, "{back}");
        }
        ContractInvoiceInfo entity = MyBLL.GetContractInvoiceByID(invoice_id);
        if (entity != null)
        {
            if (entity.Invoice_ContractID != contract_id)
            {
                Public.Msg("error", "错误信息", "无效的发票信息", false, "{back}");
            }
            int status = tools.NullInt(entity.Invoice_Status);
            if (status == 2)
            {
                Public.Msg("error", "错误信息", "无效的操作", false, "{back}");
            }
            entity.Invoice_Status = status + 1;
        }
        if (MyBLL.EditContractInvoice(entity))
        {
            if (entity.Invoice_Status == 1)
            {
                Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "修改发票申请状态",1, "修改发票申请状态为：启用发票申请");
            }
            else
            {
                Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "修改发票申请状态", 1, "修改发票申请状态为：关闭发票申请");
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_id=" + contract_id);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
        }
    }

    //更改发票申请状态
    public void UpdateInvoiceApplyStatus(int Status)
    {
        int apply_id = tools.CheckInt(Request["apply_id"]);
        int contract_id = 0;
        string invoice_tel = "";
        double Apply_Amount = tools.CheckFloat(Request["invoice_amount"]);
        string Apply_Note = tools.CheckStr(Request["apply_note"]);
        double invoice_amount;
        ContractInvoiceApplyInfo entity = MyBLL.GetContractInvoiceApplyByID(apply_id);
        if (entity != null)
        {
            contract_id = entity.Invoice_Apply_ContractID;
            ContractInfo contractinfo = GetContractByID(contract_id);
            if (contractinfo != null)
            {
                double contract_allprice = contractinfo.Contract_AllPrice;
                invoice_amount = Get_Contract_InvoiceAmount(contract_id);
                contract_allprice = Math.Round(contract_allprice, 2);
                invoice_amount = Math.Round(invoice_amount, 2);
                if (Apply_Amount > contract_allprice)
                {
                    Public.Msg("error", "错误信息", "开票金额不可大于未开票金额!", false, "{back}");
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "合同无效!", false, "{back}");
            }

            ContractInvoiceInfo invoiceinfo = MyBLL.GetContractInvoiceByID(entity.Invoice_Apply_InvoiceID);
            if (invoiceinfo != null)
            {
                invoice_tel = invoiceinfo.Invoice_Tel;
            }

            //开票
            if (Status == 1)
            {
                if (Apply_Amount <= 0)
                {
                    Public.Msg("error", "错误信息", "请输入开票金额!", false, "{back}");
                }
                entity.Invoice_Apply_Status = 1;
                entity.Invoice_Apply_Amount = Apply_Amount;
                entity.Invoice_Apply_Note = Apply_Note;
                entity.Invoice_Apply_Addtime = DateTime.Now;
                if (MyBLL.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "合同开票", 1, "合同开票，开票金额：" + Public.DisplayCurrency(Apply_Amount));
                    Public.Msg("positive", "操作成功", "操作成功!", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //邮寄
            if (Status == 2)
            {
                entity.Invoice_Apply_Status = 2;
                if (MyBLL.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "合同发票已邮寄", 1, "合同发票已邮寄，请注意查收！");
                    int outcome = 0;
                    outcome = -1;
                    //发送邮件短信
                    if (invoice_tel.Length == 11)
                    {
                        //outcome = sms.Send(invoice_tel, "您的发票已邮寄，请注意查收。" + tools.NullStr(Application["site_name"]));
                        //sms.Unbind();

                    }
                    //发送邮寄邮寄
                    if (contractinfo != null)
                    {
                        string supplier_email = "";
                        SupplierInfo supplierinfo = MySupplier.GetSupplierByID(contractinfo.Contract_BuyerID, Public.GetUserPrivilege());
                        if (supplierinfo != null)
                        {
                            supplier_email = supplierinfo.Supplier_Email;
                        }
                        //发送订单邮件
                        string mailsubject, mailbodytitle, mailbody;
                        mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的发票已邮寄";
                        mailbodytitle = mailsubject;
                        mailbody = orders.mail_template("invoice_send", "", contractinfo.Contract_SN);
                        orders.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
                    }
                    //if (outcome < 0)
                    //{
                    //    Public.Msg("positive", "操作成功", "操作成功，短信提示发送失败。", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                    //}
                    //else if (outcome > 0)
                    //{
                    //    Public.Msg("positive", "操作成功", "操作成功，短信提示发送成功。", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                    //}
                    //else
                    //{
                        Public.Msg("positive", "操作成功", "操作成功!", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                    //}
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //收票
            if (Status == 3)
            {
                entity.Invoice_Apply_Status = 3;
                if (MyBLL.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "合同发票已签收", 1, "合同发票已签收！");
                    Public.Msg("positive", "操作成功", "操作成功!", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //取消
            if (Status == 4)
            {
                entity.Invoice_Apply_Status = 4;
                entity.Invoice_Apply_Note = Apply_Note;
                if (MyBLL.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, Session["User_Name"].ToString(), "合同开票申请取消", 1, "合同开票申请取消，取消原因：" + Apply_Note);
                    Public.Msg("positive", "操作成功", "操作成功!", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                }
                else
                {
                    Public.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "无效的申请信息操作!", false, "{back}");
        }

    }

    //合同日志记录
    public void Contract_Log_Add(int Contract_ID, string Operator, string Action, int Result, string Remark)
    {
        ContractLogInfo entity = new ContractLogInfo();
        entity.Log_Operator = Operator;
        entity.Log_Contact_ID = Contract_ID;
        entity.Log_Action = Action;
        entity.Log_Result = Result;
        entity.Log_Remark = Remark;
        entity.Log_Addtime = DateTime.Now;
        Mycontractlog.AddContractLog(entity);
    }

    //获取合同附加订单(打印)
    public string GetPrintContractOrdersByContractsID(int Contract_ID)
    {
        string strHTML = "";
        int i = 0;
        strHTML += "<table border=\"0\"  width=\"100%\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
        strHTML += "<table border=\"0\"  width=\"608\" align=\"right\" class=\"list_tab\" style=\"border:1px solid #000000;\" cellpadding=\"0\" cellspacing=\"0\">";
        strHTML += "    <tr bgcolor=\"#ffffff\">";
        strHTML += "        <td align=\"center\" style=\"height:25px;\" width=\"50\">序号</td>";
        strHTML += "        <td align=\"center\">订单编号</td>";
        strHTML += "        <td align=\"center\">商品种类</td>";
        strHTML += "        <td align=\"center\">数量</td>";
        strHTML += "        <td align=\"center\">订单金额</td>";
        strHTML += "        <td align=\"center\">下单时间</td>";
        strHTML += "    </tr>";
        int Goods_Amount = 0;
        double Goods_Sum = 0;
        IList<OrdersGoodsInfo> GoodsListAll = null;
        ContractInfo Contract = GetContractByID(Contract_ID);
        if (Contract != null)
        {
            IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
            if (ordersinfos != null)
            {
                foreach (OrdersInfo ordersinfo in ordersinfos)
                {
                    Goods_Sum = 0;
                    Goods_Amount = 0;
                    i = i + 1;
                    GoodsListAll = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
                    if (GoodsListAll != null)
                    {
                        foreach (OrdersGoodsInfo good in GoodsListAll)
                        {
                            if ((good.Orders_Goods_ParentID == 0 && good.Orders_Goods_Type != 2) || good.Orders_Goods_ParentID > 0)
                            {
                                Goods_Amount = Goods_Amount + 1;
                                Goods_Sum = Goods_Sum + good.Orders_Goods_Amount;
                            }
                        }

                    }

                        strHTML += "    <tr bgcolor=\"#ffffff\">";
                        strHTML += "        <td align=\"left\">" + i + "</td>";
                        strHTML += "        <td align=\"left\">" + ordersinfo.Orders_SN + "</td>";
                        strHTML += "        <td align=\"left\">" + Goods_Amount + "</td>";
                        strHTML += "        <td align=\"left\">" + Goods_Sum + "</td>";
                        strHTML += "        <td align=\"left\">" + Public.DisplayCurrency(ordersinfo.Orders_Total_AllPrice) + "</td>";
                        strHTML += "        <td align=\"left\">" + ordersinfo.Orders_Addtime + "</td>";
                        strHTML += "    </tr>";
                }
            }
        }

        strHTML += "    </table></td></tr></table>";
        return strHTML;
    }

    //合同订单所有产品(打印)
    public string Contract_Orders_Goods_Print(int Contract_ID)
    {
        string strHTML = "";
        IList<OrdersGoodsInfo> GoodsListAll = null;
        int freighted_amount;
        int icount = 0;

        ContractInfo contractinfo = GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            strHTML += "<table width=\"635\" border=\"0\" align=\"center\" class=\"list_tab\" style=\"border:1px solid #000000;page-break-before:always;\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#000000\">";
            strHTML += "<tr bgcolor=\"#ffffff\">";
            strHTML += "<td align=\"center\" style=\"height:25px;\" width=\"50\">序号</td>";
            strHTML += "<td align=\"center\">订单号</td>";
            strHTML += "<td align=\"center\">订货号</td>";
            strHTML += "<td align=\"center\">产品名称</td>";
            if (contractinfo.Contract_Status == 1)
            {
                strHTML += "<td align=\"center\">规格</td>";
                strHTML += "<td align=\"center\">单价</td>";
                strHTML += "<td align=\"center\">数量</td>";
                strHTML += "<td align=\"center\">金额</td>";
            }
            else
            {
                strHTML += "<td align=\"center\">数量</td>";
            }
            strHTML += "</tr>";
            IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
            if (ordersinfos != null)
            {
                foreach (OrdersInfo ordersinfo in ordersinfos)
                {
                    GoodsListAll = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
                    if (GoodsListAll != null)
                    {
                        foreach (OrdersGoodsInfo entity in GoodsListAll)
                        {
                            if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                            {
                                freighted_amount = 0;
                            }
                            else
                            {
                                icount = icount + 1;
                                    strHTML += "<tr bgcolor=\"#ffffff\">";
                                    strHTML += "<td align=\"left\">" + icount + "</td>";
                                    strHTML += "<td align=\"left\">" + ordersinfo.Orders_SN + "</td>";
                                    strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Code + "</td>";
                                    strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Name + "</td>";
                                    if (contractinfo.Contract_Status == 1)
                                    {
                                        strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Spec + "&nbsp;</td>";
                                        strHTML += "<td align=\"left\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                                        strHTML += "<td align=\"left\">" + (entity.Orders_Goods_Amount) + "</td>";
                                        strHTML += "<td align=\"left\">" + (Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount)) + "</td>";
                                    }
                                    else
                                    {
                                        strHTML += "<td align=\"left\">" + (entity.Orders_Goods_Amount) + "</td>";
                                    }
                                    strHTML += "</tr>";
                            }
                        }
                    }
                }
            }
        }
        strHTML += "</table>";
        return strHTML;
    }

    //合同查看打印
    public void Contract_View()
    {
        int Contract_ID;
        string Contract_template;
        Contract_ID = tools.CheckInt(Request["Contract_ID"]);
        if (Contract_ID == 0)
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
        ContractInfo entity = GetContractByID(Contract_ID);
        if (entity != null)
        {
            if (Request["action"] == "print")
            {
                Response.Write("<script>window.print();</script>");
            }

            string address = "";// /**/
            IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
            if (ordersinfos != null)
            {
                address = addr.DisplayAddress(ordersinfos[0].Orders_Address_State, ordersinfos[0].Orders_Address_City, ordersinfos[0].Orders_Address_County) + " " + ordersinfos[0].Orders_Address_StreetAddress;
            }
            Contract_template = entity.Contract_Template;
            Contract_template = Contract_template.Replace("{contract_sn}", entity.Contract_SN);

            string buyer_name = "";
            string lawman = "";
            string agentman = "";
            string buyer_tel = "";
            string buyer_fax = "";
            string buyer_zip = "";
            Contract_template = entity.Contract_Template;
            Contract_template = Contract_template.Replace("{contract_sn}", entity.Contract_SN);
            SupplierInfo buyerinfo = MySupplier.GetSupplierByID(entity.Contract_BuyerID,Public.GetUserPrivilege());
            if (buyerinfo != null)
            {
                buyer_name = buyerinfo.Supplier_CompanyName;
                buyer_tel = buyerinfo.Supplier_Phone;
                buyer_fax = buyerinfo.Supplier_Fax;
                buyer_zip = buyerinfo.Supplier_Zip;

            }

            //主体信息
            ContractInvoiceInfo invoiceinfo = MyBLL.GetContractInvoiceByContractID(entity.Contract_ID);
            if (invoiceinfo != null)
            {
                if (invoiceinfo.Invoice_Type == 0)
                {
                    if (invoiceinfo.Invoice_Title == "个人")
                    {
                        Contract_template = Contract_template.Replace("{buyer}", invoiceinfo.Invoice_PersonelName);
                        Contract_template = Contract_template.Replace("{member_companyname}", "");
                        Contract_template = Contract_template.Replace("{personelcard}", "身份证号：" + invoiceinfo.Invoice_PersonelCard);
                    }
                    else
                    {
                        Contract_template = Contract_template.Replace("{buyer}", invoiceinfo.Invoice_FirmName);
                        Contract_template = Contract_template.Replace("{member_companyname}", invoiceinfo.Invoice_FirmName);
                        Contract_template = Contract_template.Replace("{personelcard}", "");
                    }
                }

                else
                {
                    Contract_template = Contract_template.Replace("{buyer}", invoiceinfo.Invoice_VAT_FirmName);
                    Contract_template = Contract_template.Replace("{member_companyname}", invoiceinfo.Invoice_VAT_FirmName);
                    Contract_template = Contract_template.Replace("{personelcard}", "");
                }

                Contract_template = Contract_template.Replace("{member_address}", invoiceinfo.Invoice_VAT_RegAddr);
                Contract_template = Contract_template.Replace("{member_lawman}", lawman);
                Contract_template = Contract_template.Replace("{member_agent}", agentman);
                Contract_template = Contract_template.Replace("{member_phone}", buyer_tel);
                Contract_template = Contract_template.Replace("{member_fax}", buyer_fax);
                Contract_template = Contract_template.Replace("{member_taxid}", invoiceinfo.Invoice_VAT_Code);
                Contract_template = Contract_template.Replace("{member_bankname}", invoiceinfo.Invoice_VAT_Bank);
                Contract_template = Contract_template.Replace("{member_bankaccount}", invoiceinfo.Invoice_VAT_BankAccount);
                Contract_template = Contract_template.Replace("{member_zipcode}", buyer_zip);
            }
            else
            {
                Contract_template = Contract_template.Replace("{buyer}", "");
                Contract_template = Contract_template.Replace("{member_companyname}", "");
                Contract_template = Contract_template.Replace("{member_address}", "");
                Contract_template = Contract_template.Replace("{member_lawman}", "");
                Contract_template = Contract_template.Replace("{member_agent}", "");
                Contract_template = Contract_template.Replace("{member_phone}", "");
                Contract_template = Contract_template.Replace("{member_fax}", "");
                Contract_template = Contract_template.Replace("{member_taxid}", "");
                Contract_template = Contract_template.Replace("{member_bankname}", "");
                Contract_template = Contract_template.Replace("{member_bankaccount}", "");
                Contract_template = Contract_template.Replace("{member_zipcode}", "");
            }

            //合同交易信息
            Contract_template = Contract_template.Replace("{orders_list}", GetPrintContractOrdersByContractsID(Contract_ID));
            Contract_template = Contract_template.Replace("{contract_signtime}", entity.Contract_Addtime.ToShortDateString());
            Contract_template = Contract_template.Replace("{contract_discount}", Public.DisplayCurrency(entity.Contract_Discount));
            Contract_template = Contract_template.Replace("{contract_freight}", Public.DisplayCurrency(entity.Contract_Freight));
            Contract_template = Contract_template.Replace("{contract_service}", Public.DisplayCurrency(entity.Contract_ServiceFee));
            Contract_template = Contract_template.Replace("{contract_allprice}", Public.DisplayCurrency(entity.Contract_AllPrice));
            Contract_template = Contract_template.Replace("{contract_allpricechinese}", MyUcase.ConvertSum((entity.Contract_AllPrice).ToString()));
            Contract_template = Contract_template.Replace("{contract_payway}", entity.Contract_Payway_Name);
            Contract_template = Contract_template.Replace("{contract_deliveryname}", entity.Contract_Delivery_Name);
            Contract_template = Contract_template.Replace("{contract_address}", address);
            Response.Write(Server.HtmlDecode(Contract_template));
            Response.Write(Contract_Orders_Goods_Print(Contract_ID));
        }
        else
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
    }
    #endregion

    #region 发货单
    //发货单列表
    public string GetContractDeliverys()
    {
        string listtype = tools.CheckStr(Request.QueryString["listtype"]);
        string keyword, date_start, date_end;

        //关键词
        keyword = tools.CheckStr(Request["keyword"]);

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractDeliveryInfo.Contract_Delivery_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractDeliveryInfo.Contract_Delivery_ContractID", "in", "select contract_id from contract where contract_sn like '%"+keyword+"%'"));

        }


        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{ContractDeliveryInfo.Contract_Delivery_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{ContractDeliveryInfo.Contract_Delivery_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));


        PageInfo pageinfo = MyFreight.GetPageInfo(Query);

        IList<ContractDeliveryInfo> entitys = MyFreight.GetContractDeliverys(Query);
        if (entitys != null)
        {
            ContractInfo contractinfo = null;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ContractDeliveryInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Contract_Delivery_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Delivery_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                contractinfo = GetContractByID(entity.Contract_Delivery_ContractID);
                if (contractinfo != null)
                {
                    jsonBuilder.Append("<a href=\\\"/contract/contract_detail.aspx?contract_id=" + entity.Contract_Delivery_ContractID + "\\\">" + contractinfo.Contract_SN + "</a>");
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                contractinfo = null;
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Delivery_DocNo);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(ContractDeliverysStatus(entity.Contract_Delivery_DeliveryStatus));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<span class=\\\"t12_red\\\">");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Contract_Delivery_Amount));
                jsonBuilder.Append("</span>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Delivery_CompanyName);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Delivery_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Delivery_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"orders_delivery_view.aspx?orders_delivery_id=" + entity.Contract_Delivery_ID + "\\\"><img src=\\\"/images/btn_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"></a>");
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

    //导出勾选发货单
    public void ContractsDelivery_Export()
    {
        string delivery_id = tools.CheckStr(Request.QueryString["delivery_id"]);
        if (delivery_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(delivery_id, 1) == ",") { delivery_id = delivery_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        ContractInfo contractinfo = null;
        Query.PageSize = 0;
        Query.CurrentPage =1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractDeliveryInfo.Contract_Delivery_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractDeliveryInfo.Contract_Delivery_ID", "in", delivery_id));
        Query.OrderInfos.Add(new OrderInfo("ContractDeliveryInfo.Contract_Delivery_ID", "DESC"));
        IList<ContractDeliveryInfo> entitys = MyFreight.GetContractDeliverys(Query);

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "合同编号", "单据编码", "配货状态", "物流费用", "物流方式", "物流公司", "物流单号", "操作时间", "备注" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (ContractDeliveryInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr[0] = entity.Contract_Delivery_ID;
            contractinfo = GetContractByID(entity.Contract_Delivery_ContractID);
            if (contractinfo != null)
            {
                dr[1] = contractinfo.Contract_SN;
            }
            else
            {
                dr[1] = "";
            }

            dr[2] = entity.Contract_Delivery_DocNo;
            dr[3] = ContractDeliverysStatus(entity.Contract_Delivery_DeliveryStatus);
            dr[4] = entity.Contract_Delivery_Amount;
            dr[5] = entity.Contract_Delivery_Name;
            dr[6] = entity.Contract_Delivery_CompanyName;
            dr[7] = entity.Contract_Delivery_Code;
            dr[8] = entity.Contract_Delivery_Addtime;
            dr[9] = entity.Contract_Delivery_Note;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }
    #endregion

    #region 付款单
    public string GetContractsPayments()
    {
        string listtype = tools.CheckStr(Request.QueryString["listtype"]);
        string keyword, date_start, date_end;
        int orders_ID;
        orders_ID = 0;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        ContractInfo contractinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractPaymentInfo.Contract_Payment_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractPaymentInfo.Contract_Payment_ContractID", "in", "select contract_id from contract where contract_sn like '%"+keyword+"%'"));

        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{ContractPaymentInfo.Contract_Payment_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{ContractPaymentInfo.Contract_Payment_Addtime})", "<=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));


        PageInfo pageinfo = MyPayment.GetPageInfo(Query);

        IList<ContractPaymentInfo> entitys = MyPayment.GetContractPayments(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ContractPaymentInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Contract_Payment_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Payment_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                contractinfo = GetContractByID(entity.Contract_Payment_ContractID);
                if (contractinfo != null)
                {
                    jsonBuilder.Append("<a href=\\\"/contract/contract_detail.aspx?contract_id=" + entity.Contract_Payment_ContractID + "\\\">" + contractinfo.Contract_SN + "</a>");
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                contractinfo = null;
                jsonBuilder.Append("\",");
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Payment_DocNo);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(ContractPaymentsStatus(entity.Contract_Payment_PaymentStatus));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<span class=\\\"t12_red\\\">");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Contract_Payment_Amount));
                jsonBuilder.Append("</span>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Payment_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Contract_Payment_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"orders_payment_view.aspx?Contract_Payment_ID=" + entity.Contract_Payment_ID + "\\\"><img src=\\\"/images/btn_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"></a>");
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

    public void ContractPayment_Export()
    {
        string payment_id = tools.CheckStr(Request.QueryString["payment_id"]);
        if (payment_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(payment_id, 1) == ",") { payment_id = payment_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        ContractInfo contractinfo = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractPaymentInfo.Contract_Payment_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractPaymentInfo.Contract_Payment_ID", "in", payment_id));
        Query.OrderInfos.Add(new OrderInfo("ContractPaymentInfo.Contract_Payment_ID", "DESC"));
        IList<ContractPaymentInfo> entitys = MyPayment.GetContractPayments(Query);

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "合同编号", "单据编码", "支付状态", "金额", "支付方式", "操作时间", "备注" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (ContractPaymentInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr[0] = entity.Contract_Payment_ID;
            contractinfo = GetContractByID(entity.Contract_Payment_ContractID);
            if (contractinfo != null)
            {
                dr[1] = contractinfo.Contract_SN;
            }
            else
            {
                dr[1] = "";
            }

            dr[2] = entity.Contract_Payment_DocNo;
            dr[3] = ContractPaymentsStatus(entity.Contract_Payment_PaymentStatus);
            dr[4] = entity.Contract_Payment_Amount;
            dr[5] = entity.Contract_Payment_Name;
            dr[6] = entity.Contract_Payment_Addtime;
            dr[7] = entity.Contract_Payment_Note;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }
    #endregion

}
