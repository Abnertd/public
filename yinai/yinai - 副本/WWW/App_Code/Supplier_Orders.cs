using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.SAL;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Glaer.Trade.Util.SQLHelper;


/// <summary>
///供应商应用类
/// </summary>
public partial class Supplier
{
    //投标保证金账户
  string  bidguaranteeaccno = System.Configuration.ConfigurationManager.AppSettings["zhongxin_bidguaranteeaccno"];
    string bidguaranteeaccnm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_bidguaranteeaccnm"];

    #region 订单相关状态

    public string OrdersType(int typeid)
    {
        string resultstr = string.Empty;
        switch (typeid)
        {
            case 1:
                resultstr = "现货采购订单"; break;
            case 2:
                resultstr = "定制采购订单"; break;
            case 3:
                resultstr = "代理采购订单"; break;
        }
        return resultstr;
    }

    public string OrdersStatus(int status, int paymentstatus, int deliverystatus)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待确认";
                break;
            case 1:
                if (deliverystatus == 0)
                {
                    resultstr = PaymentStatus(paymentstatus);
                }
                else
                {
                    resultstr = DeliveryStatus(deliverystatus);
                }
                break;
            case 2:
                resultstr = "交易成功"; break;
            case 3:
                resultstr = "交易失败"; break;
            case 4:
                resultstr = "申请退换货"; break;
            case 5:
                resultstr = "申请退款"; break;
        }
        return resultstr;
    }

    public string PaymentStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未支付"; break;
            case 1:
                resultstr = "已支付"; break;
            case 2:
                resultstr = "已退款"; break;
            case 3:
                resultstr = "退款处理中"; break;
            case 4:
                resultstr = "已到帐"; break;
        }
        return resultstr;
    }

    public string DeliveryStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待采购"; break;
            case 1:
                resultstr = "已发货"; break;
            case 2:
                resultstr = "已签收"; break;
            case 3:
                resultstr = "已拒收"; break;
            case 4:
                resultstr = "退货处理中"; break;
            case 5:
                resultstr = "已退货"; break;
            case 6:
                resultstr = "配货中"; break;

        }
        return resultstr;
    }

    public string InvoiceStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未开票"; break;
            case 1:
                resultstr = "已开票"; break;
            case 2:
                resultstr = "已退票"; break;
            case 3:
                resultstr = "不需要发票"; break;
        }

        return resultstr;
    }

    public string OrdersSupplierStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待确认"; break;
        }
        return resultstr;
    }

    public string OrdersMemberStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待买方确认"; break;
        }
        return resultstr;
    }

    public string OrdersInvoiceType(int typeid)
    {
        string resultstr = string.Empty;
        switch (typeid)
        {
            case 0:
                resultstr = "不需要发票"; break;
            case 1:
                resultstr = "普通发票"; break;
            case 2:
                resultstr = "增值税发票"; break;
        }
        return resultstr;
    }

    #endregion

    #region 合同相关状态
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

    //配送单商品签收状态
    public string ContractGoodsAcceptStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未签收"; break;
            case 1:
                resultstr = "部分签收"; break;
            case 2:
                resultstr = "已签收"; break;
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

    //获取合同确认状态
    public string ContractConfirmStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "双方未确认"; break;
            case 1:
                resultstr = "买方已确认"; break;
            case 2:
                resultstr = "卖方已确认"; break;
        }

        return resultstr;
    }
    #endregion

    #region 订单管理

    public OrdersInfo GetOrdersInfoBySN(string orders_sn, int Supplier_ID)
    {
        return MyOrders.GetSupplierOrderInfoBySN(orders_sn, Supplier_ID);
    }

    public OrdersInfo GetOrdersInfoBySN(string orders_sn)
    {
        OrdersInfo entity = MyOrders.GetOrdersBySN(orders_sn);
        if (entity != null)
        {
            if (entity.Orders_BuyerID != tools.NullInt(Session["supplier_id"]) || entity.Orders_BuyerType == 0)
            {
                entity = null;
            }
        }
        return entity;
    }


    //商家通过订单编号获取订单信息
    public OrdersInfo GetSupplierOrdersInfoBySN(string orders_sn)
    {
        OrdersInfo entity = MyOrders.GetOrdersBySN(orders_sn);
        //if (entity != null)
        //{
        //    if (entity.Orders_BuyerID != tools.NullInt(Session["supplier_id"]) || entity.Orders_BuyerType == 0)
        //    {
        //        entity = null;
        //    }
        //}
        return entity;
    }

    public void Home_Orders_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        string tmp_head, tmp_list, tmp_splitline, tmp_norecord, tmp_toolbar, tmp_toolbar_bottom, form_action;
        tmp_head = "";
        tmp_list = "";
        tmp_splitline = "";
        tmp_norecord = "";
        tmp_toolbar = "";
        tmp_toolbar_bottom = "";
        form_action = "";
        string date_start, date_end, orders_sn, info, orders_operation = "", pageurl, Orders_Status;
        int curpage = 0;
        date_start = tools.CheckStr(Request["date_start"]);
        date_end = tools.CheckStr(Request["date_end"]);
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        curpage = tools.CheckInt(Request["page"]);
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" class=\"table_padding_5\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        tmp_head = tmp_head + "   <th width=\"90\" align=\"center\" valign=\"middle\">订单类型</th>";
        tmp_head = tmp_head + "   <th height=\"30\" width=\"120\" align=\"center\" valign=\"middle\">订单号</th>";
        tmp_head = tmp_head + "   <th width=\"90\" align=\"center\" valign=\"middle\">收货人</th>";
        tmp_head = tmp_head + "   <th width=\"90\" align=\"center\" valign=\"middle\">支付方式</th>";
        tmp_head = tmp_head + "   <th valign=\"middle\" width=\"80\" align=\"center\">订单总价</th>";
        tmp_head = tmp_head + "   <th width=\"90\" align=\"center\" valign=\"middle\">订单状态</th>";
        tmp_head = tmp_head + "   <th width=\"90\" align=\"center\" valign=\"middle\">下单时间</th>";
        tmp_head = tmp_head + " </tr>";


        tmp_norecord = tmp_norecord + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"10\">";
        tmp_norecord = tmp_norecord + " <tr>";
        tmp_norecord = tmp_norecord + "   <td height=\"35\" colspan=\"8\" style=\"padding-left:25px;\" align=\"center\"><img src=\"/Images/icon_alert.gif\" width=\"16\" align=\"absmiddle\" height=\"16\" /> 没有记录</td>";
        tmp_norecord = tmp_norecord + " </tr>";
        tmp_norecord = tmp_norecord + "</table>";



        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerType", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> orderslist = MyOrders.GetOrderss(Query);
        if (orderslist != null)
        {
            foreach (OrdersInfo entity in orderslist)
            {
                if (entity.Orders_Status != 1 || entity.Orders_DeliveryStatus == 0)
                {
                    Orders_Status = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                }
                else
                {
                    Orders_Status = DeliveryStatus(entity.Orders_DeliveryStatus);
                }

                tmp_head = tmp_head + " <tr style=\"background-color:#ffffff;\">";
                tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" height=\"35\" valign=\"middle\">" + OrdersType(entity.Orders_Type) + "</td>";
                tmp_head = tmp_head + "   <td align=\"center\" width=\"120\"><a class=\"a_t12_blue\" href=\"/supplier/order_view.aspx?orders_sn=" + entity.Orders_SN + "\">" + entity.Orders_SN + "</a></td>";
                tmp_head = tmp_head + "   <td width=\"90\" height=\"35\" valign=\"middle\" align=\"center\">" + entity.Orders_Address_Name + "</td>";
                tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" height=\"35\" valign=\"middle\">" + entity.Orders_Payway_Name + "</td>";

                tmp_head = tmp_head + "   <td align=\"center\" width=\"80\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</td>";

                tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" height=\"35\" valign=\"middle\">" + Orders_Status + "</td>";
                string paystatus = Myorder.PaymentStatus(entity.Orders_PaymentStatus);
                int paywayid = entity.Orders_Payway;

                tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" class=\"info_date\" height=\"35\" valign=\"middle\">" + entity.Orders_Addtime.ToShortDateString() + "</td>";
                tmp_head = tmp_head + " </tr>";
            }
            tmp_head = tmp_head + "</table>";
            Response.Write(tmp_head);

        }
        else
        {
            tmp_head = tmp_head + "</table>";
            Response.Write(tmp_head);
            Response.Write(tmp_norecord);
        }

    }

    public int Supplier_Order_Count(int listtype, int supplier_id, string count_type)
    {

        int Order_Count = 0;
        if (supplier_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierID", "=", supplier_id.ToString()));

            switch (count_type)
            {
                case "unmemberconfirm":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_MemberStatus", "=", "0"));
                    break;
                case "unconfirm":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierStatus", "=", "0"));
                    //Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_MemberStatus", "=", "0"));
                    break;
                case "loanapply":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                    break;
                case "payment":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                    break;
                case "delivery":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                    break;
                case "accept":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "1"));
                    break;
                case "unprocessed":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "processing":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    break;
                case "success":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                    break;
                case "faiture":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                    break;
            }
            PageInfo page = MyOrders.GetPageInfo(Query);
            if (page != null)
            {
                Order_Count = page.RecordCount;
            }
        }
        return Order_Count;
    }

    /// <summary>
    /// 订单页选项卡
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string Orders_TabControl(int listtype, string type)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        string orderstype = tools.CheckStr(Request["orders_type"]);
        string date_start = tools.CheckStr(Request["date_start"]);
        string date_end = tools.CheckStr(Request["date_end"]);
        string orders_sn;
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        Session["Tab_PriceReport_ID"] = PriceReport_ID;

        string queryurl = "orders_type=" + orderstype + "&orders_sn=" + orders_sn + "&date_start=" + date_start + "&date_end=" + date_end + "&PriceReport_ID=" + PriceReport_ID;

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<ul class=\"zkw_lst31\">");
        strHTML.Append("	<li " + (type == "all" ? "class=\"on\"" : "") + " id=\"n01\"><a href=\"?" + queryurl + "\">全部订单(<span>" + Supplier_Order_Count(listtype, supplier_id, "all") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "unprocessed" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=unprocessed\">未处理的订单(<span>" + Supplier_Order_Count(listtype, supplier_id, "unprocessed") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "processing" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=processing\">处理中的订单(<span>" + Supplier_Order_Count(listtype, supplier_id, "processing") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "success" ? "class=\"on\"" : "") + " id=\"n04\"><a href=\"?" + queryurl + "&type=success\">交易成功的订单(<span>" + Supplier_Order_Count(listtype, supplier_id, "success") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "faiture" ? "class=\"on\"" : "") + " id=\"n05\"><a href=\"?" + queryurl + "&type=faiture\">交易失败的订单(<span>" + Supplier_Order_Count(listtype, supplier_id, "faiture") + "</span>)</a></li>");
        strHTML.Append("</ul><div class=\"clear\"></div>");
        Session["Tab_PriceReport_ID"] = "0";

        return strHTML.ToString();
    }

    public void Orders_List_bak(int listtype, string type)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string tmp_head, tmp_list, tmp_splitline, tmp_norecord, tmp_toolbar, tmp_toolbar_bottom, form_action;
        tmp_head = "";
        tmp_list = "";
        tmp_splitline = "";
        tmp_norecord = "";
        tmp_toolbar = "";
        tmp_toolbar_bottom = "";
        form_action = "";
        string date_start, date_end, orders_sn, info, page_url, Orders_Status;//orders_operation = ""
        int curpage = 0;
        date_start = tools.CheckStr(Request["date_start"]);
        date_end = tools.CheckStr(Request["date_end"]);
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        curpage = tools.CheckInt(Request["page"]);
        int orders_type = tools.CheckInt(Request["orders_type"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        if (curpage < 1)
        {
            curpage = 1;
        }
        page_url = "?orders_sn=" + orders_sn + "&date_start=" + date_start + "&date_end=" + date_end + "&type=" + type + "&orders_type=" + orders_type + "&PriceReport_ID=" + PriceReport_ID;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        if (listtype == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "in", supplier_id.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerType", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", supplier_id.ToString()));
        }

        if (PriceReport_ID > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PriceReportID", "=", PriceReport_ID.ToString()));

        switch (type)
        {
            case "unprocessed":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                break;
            case "processing":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                break;
            case "success":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                break;
            case "faiture":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                break;
        }
        if (orders_type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Type", "=", orders_type.ToString()));
        }
        if (orders_sn != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SN", "like", orders_sn));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> orderslist = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);

        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"zkw_19_fox\">");

        sb.Append("<div class=\"zkw_date\">");

        sb.Append("<form name=\"datescope\" method=\"post\" action = \"?type=" + type + "\">");

        sb.Append("  起始日期：<input type=\"text\" class=\"input_calendar\" name=\"date_start\" id=\"date_start\" maxlength=\"10\" readonly=\"readonly\" value=\"" + date_start + "\" />");
        sb.Append("<script>$(function() {$(\"#date_start\").datepicker({numberOfMonths:1});})</script>");
        sb.Append(" 终止日期：<input type=\"text\" class=\"input_calendar\" name=\"date_end\" id=\"date_end\" maxlength=\"10\" readonly=\"readonly\" value=\"" + date_end + "\" />");
        sb.Append("<script>$(function() {$(\"#date_end\").datepicker({numberOfMonths:1});})</script>");
        sb.Append(" 订单类型：<select name=\"orders_type\"> ");
        sb.Append("<option value=\"0\">全部订单</option> ");
        sb.Append("<option value=\"1\" " + pub.CheckSelect(orders_type.ToString(), "1") + ">现货采购订单</option> ");
        sb.Append("<option value=\"2\" " + pub.CheckSelect(orders_type.ToString(), "2") + ">定制采购订单</option> ");
        sb.Append("<option value=\"3\" " + pub.CheckSelect(orders_type.ToString(), "3") + ">代理采购订单</option> ");
        sb.Append("</select> ");
        sb.Append(" 订单号：<input type=\"text\" name=\"orders_sn\" value=\"" + orders_sn + "\" size=\"30\" /> <input name=\"search\" type=\"submit\" class=\"input10\" id=\"search\" value=\"\" />");
        sb.Append("<input name=\"type\" type=\"hidden\" class=\"input10\" id=\"type\" value=\"" + type + "\" /></form>");
        sb.Append("</div>");



        sb.Append("<div class=\"zkw_19_info\">");
        sb.Append("<ul class=\"zkw_lst32\">");
        //sb.Append("<li style=\"width: 35px;\"><input id=\"check_all\" onclick=\"check_all();\" type=\"checkbox\"  /></li>");
        sb.Append("<li style=\"width: 100px;\">订单类型</li>");
        sb.Append("<li style=\"width: 101px;\">订单编号</li>");
        //sb.Append("<li style=\"width: 265px;\">订单商品</li>");
        sb.Append("<li style=\"width: 265px;\">所属合同</li>");
        sb.Append("<li style=\"width: 85px;\">收货人</li>");
        sb.Append("<li style=\"width: 99px;\">订单金额</li>");
        sb.Append("<li style=\"width: 94px;\">下单时间</li>");
        sb.Append("<li style=\"width: 70px;\">订单状态</li>");
        sb.Append("<li style=\"width: 82px;\">操作</li>");
        sb.Append("</ul>");
        sb.Append("<div class=\"clear\">");
        sb.Append("</div>");
        sb.Append("</div>");




        form_action = "?type=" + type;

        sb.Append("<div class=\"zkw_table\">");

        if (orderslist != null)
        {

            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");

            IList<OrdersGoodsInfo> goodsList;
            ContractInfo contractinfo;
            foreach (OrdersInfo entity in orderslist)
            {
                Orders_Status = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                sb.Append("<tr>");
                //sb.Append("<td width=\"35px\"><input name=\"check\" type=\"checkbox\" value=\"" + entity.Orders_ID + "\" /></td>");
                sb.Append("<td width=\"100\" align=\"center\">" + OrdersType(entity.Orders_Type) + "</td>");
                sb.Append("<td width=\"101\" height=\"17\" align=\"center\">");
                if (listtype == 0)
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/order_view.aspx?orders_sn=" + entity.Orders_SN + "\">" + entity.Orders_SN + "</a>");
                }
                else
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "\">" + entity.Orders_SN + "</a>");
                }

                sb.Append("</td>");
                //sb.Append("<td width=\"265\">");
                //goodsList = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                //if (goodsList != null)
                //{
                //    foreach (OrdersGoodsInfo goods in goodsList)
                //    {
                //        if (goods.Orders_Goods_Type == 2 && goods.Orders_Goods_ParentID == 0)
                //        {
                //            sb.Append("[套装]" + goods.Orders_Goods_Product_Name + "");
                //            break;
                //        }
                //        if (goods.Orders_Goods_Type != 2 && goods.Orders_Goods_ParentID == 0)
                //        {
                //            sb.Append("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, goods.Orders_Goods_Product_ID.ToString()) + "\" title=\"" + goods.Orders_Goods_Product_Name + "\" target=\"_blank\"><img title=\"" + goods.Orders_Goods_Product_Name + "\" src=\"" + pub.FormatImgURL(goods.Orders_Goods_Product_Img, "thumbnail") + "\" /></a><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goods.Orders_Goods_Product_ID.ToString()) + "\" title=\"" + goods.Orders_Goods_Product_Name + "\" target=\"_blank\">" + tools.CutStr(goods.Orders_Goods_Product_Name,60) + "</a>");
                //            break;
                //        }
                //    }
                //}

                //goodsList.Clear();

                //sb.Append("</td>");
                if (entity.Orders_ContractID > 0)
                {
                    contractinfo = GetContractByID(entity.Orders_ContractID);
                    if (contractinfo != null)
                    {
                        sb.Append("<td width=\"265\" align=\"center\">" + contractinfo.Contract_SN + "</td>");
                    }
                    else
                    {
                        sb.Append("<td width=\"265\" align=\"center\">--</td>");
                    }
                }
                else
                {
                    sb.Append("<td width=\"265\" align=\"center\">--</td>");
                }
                sb.Append("<td width=\"85\" align=\"center\">" + entity.Orders_Address_Name + "</td>");
                sb.Append("<td width=\"99\" align=\"center\">");
                sb.Append("<span>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</span><br>" + ((entity.Orders_Payway_Name == "") ? "&nbsp;" : entity.Orders_Payway_Name));
                sb.Append("</td>");
                sb.Append("<td width=\"94\" align=\"center\">");
                sb.Append(entity.Orders_Addtime.ToShortDateString());
                sb.Append("</td>");
                sb.Append("<td width=\"70\" align=\"center\">");
                sb.Append(Orders_Status);
                sb.Append("</td>");
                sb.Append("<td width=\"82\" align=\"center\">");
                if (listtype == 0)
                {
                    sb.Append("<a href=\"order_view.aspx?orders_sn=" + entity.Orders_SN + "\">查看</a>");
                }
                else
                {
                    sb.Append("<a href=\"order_detail.aspx?orders_sn=" + entity.Orders_SN + "\">查看</a>");
                }

                if (entity.Orders_SupplierID == tools.NullInt(Session["supplier_id"]) && entity.Orders_Status == 1)
                {
                    sb.Append("&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"window.open('/download.aspx?Orders_ID=" + entity.Orders_ID + "')\">下载合同</a>");
                }

                sb.Append("</td>");
                sb.Append("</tr>");

            }

            sb.Append("</table>");
            Response.Write(sb.ToString());
            pub.Page(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
        }
        else
        {
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
            sb.Append("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"8\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
        }

        Response.Write("</div>");
        Response.Write("</div>");

    }


    public void Orders_List()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string tmp_head, tmp_list, tmp_splitline, tmp_norecord, tmp_toolbar, tmp_toolbar_bottom, form_action, member_name = "";
        tmp_head = "";
        tmp_list = "";
        tmp_splitline = "";
        tmp_norecord = "";
        tmp_toolbar = "";
        tmp_toolbar_bottom = "";
        form_action = "";
        string date_start, date_end, orders_sn, info, page_url, Orders_Status, orderStatus, keyword;//orders_operation = ""
        int curpage = 0, orderDate = 0;
        date_start = tools.CheckStr(Request["date_start"]);
        date_end = tools.CheckStr(Request["date_end"]);
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        orderDate = tools.CheckInt(Request["orderDate"]);
        orderStatus = tools.CheckStr(Request["orderStatus"]);
        curpage = tools.CheckInt(Request["page"]);
        int orders_type = tools.CheckInt(Request["orders_type"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        keyword = tools.CheckStr(Request["keyword"]);


        if (curpage < 1)
        {
            curpage = 1;
        }
        page_url = "?keyword=" + keyword + "&date_start=" + date_start + "&date_end=" + date_end + "&orderStatus=" + orderStatus + "&orders_type=" + orders_type + "&PriceReport_ID=" + PriceReport_ID;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "=", supplier_id.ToString()));


        if (PriceReport_ID > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PriceReportID", "=", PriceReport_ID.ToString()));

        switch (orderStatus)
        {
            case "unmemberconfirm":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_MemberStatus", "=", "0"));
                break;
            case "unconfirm":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierStatus", "=", "0"));
                //Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_MemberStatus", "=", "0"));
                break;
            case "loanapply":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                break;
            case "payment":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                break;
            case "delivery":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                break;
            case "accept":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "1"));
                break;
            case "unprocessed":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                break;
            case "processing":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                break;
            case "success":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                break;
            case "faiture":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                break;
            case "LogisticsDelivery":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "4"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                break;

        }

        if (orders_type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Type", "=", orders_type.ToString()));
        }

        DateTime dt = DateTime.Now;
        switch (orderDate)
        {
            case 1:
                date_start = dt.AddMonths(-3).ToString();
                date_end = dt.ToString();
                break;
            case 2:
                date_start = new DateTime(dt.Year, 1, 1).ToString();
                date_end = dt.ToString();
                break;
            case 2016:
                date_start = new DateTime(orderDate, 1, 1).ToString();
                date_end = new DateTime(orderDate, 12, 31).ToString();
                break;
            case 2015:
                date_start = new DateTime(orderDate, 1, 1).ToString();
                date_end = new DateTime(orderDate, 12, 31).ToString();
                break;
            case 3:
                date_end = new DateTime(2014, 12, 31).ToString();
                break;
        }

        if (keyword != "" && keyword != "订单号/商品名称/商品编号")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "OrdersInfo.Orders_SN", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_ID", "in", "select distinct Orders_Goods_OrdersID from Orders_Goods as OG where OG.Orders_Goods_Product_Name like '%" + keyword + "%' "));

            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersInfo.Orders_ID", "in", "select distinct Orders_Goods_OrdersID from Orders_Goods as OG where OG.Orders_Goods_Product_Code like '%" + keyword + "%' "));
        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> orderslist = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"blk04_sz\">");
        strHTML.Append("<select name=\"orderStatus\" id=\"orderStatus\"  onchange=\"OrderStatus()\">");
        strHTML.Append("<option value=\"normal\" " + (orderStatus == "normal" ? "selected" : "") + ">全部订单</option>");
        strHTML.Append("<option value=\"unmemberconfirm\" " + (orderStatus == "unmemberconfirm" ? "selected" : "") + ">待买家确认</option>");
        strHTML.Append("<option value=\"unconfirm\" " + (orderStatus == "unconfirm" ? "selected" : "") + ">待确认</option>");
        strHTML.Append("<option value=\"loanapply\" " + (orderStatus == "loanapply" ? "selected" : "") + ">信贷申请中</option>");
        strHTML.Append("<option value=\"payment\" " + (orderStatus == "payment" ? "selected" : "") + ">待支付</option>");
        strHTML.Append("<option value=\"delivery\" " + (orderStatus == "delivery" ? "selected" : "") + ">待发货</option>");
        strHTML.Append("<option value=\"accept\" " + (orderStatus == "accept" ? "selected" : "") + ">待签收</option>");
        strHTML.Append("<option value=\"success\" " + (orderStatus == "success" ? "selected" : "") + ">交易成功</option>");
        strHTML.Append("<option value=\"faiture\" " + (orderStatus == "faiture" ? "selected" : "") + ">交易失败</option>");
        strHTML.Append("<option value=\"LogisticsDelivery\" " + (orderStatus == "LogisticsDelivery" ? "selected" : "") + ">物流发货</option>");
        strHTML.Append("</select>");
        strHTML.Append("<select name=\"orderDate\" id=\"orderDate\" onchange=\"OrderDate()\">");
        strHTML.Append("<option value=\"1\" " + (orderDate == 1 ? "selected" : "") + ">近期订单</option>");
        strHTML.Append("<option value=\"2\" " + (orderDate == 2 ? "selected" : "") + ">今年内</option>");
        strHTML.Append("<option value=\"2014\" " + (orderDate == 2014 ? "selected" : "") + ">2014年</option>");
        strHTML.Append("<option value=\"2013\" " + (orderDate == 2013 ? "selected" : "") + ">2013年</option>");
        strHTML.Append("<option value=\"3\" " + (orderDate == 3 ? "selected" : "") + ">2012年以前</option>");
        strHTML.Append("</select>");
        strHTML.Append("<input name=\"keyword\" id=\"keyword\" type=\"text\"");
        if (keyword != "")
        {
            strHTML.Append(" value=\"" + keyword + "\"");
        }
        else
        {
            strHTML.Append(" value=\"订单号/商品名称/商品编号\"");
        }
        strHTML.Append(" onfocus=\"if (this.value==this.defaultValue) this.value=''\" onblur=\"if (this.value=='') this.value=this.defaultValue\" /><a href=\"javascript:void(0);\" onclick=\"OrderSearch('keyword');\">搜 索</a>");
        strHTML.Append("</div>");

        strHTML.Append("<div class=\"blk05_sz\">");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\" margin-left:35px;\">订单商品</li>");
        strHTML.Append("<li style=\" margin-left:230px;\">数量</li>");
        strHTML.Append("<li style=\" margin-left:80px;\">订单状态</li>");
        strHTML.Append("<li style=\" margin-left:155px;\">总金额</li>");
        strHTML.Append("<li style=\" margin-left:180px;\">操作</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</div>");

        if (orderslist != null)
        {
            IList<OrdersGoodsInfo> goodsList;
            MemberInfo memberInfo = null;

            foreach (OrdersInfo entity in orderslist)
            {
                goodsList = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                Orders_Status = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                memberInfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberInfo != null)
                {
                    member_name = memberInfo.MemberProfileInfo.Member_Company;
                }
                else
                {
                    member_name = "";
                }

                strHTML.Append("<div class=\"table_blk02\">");
                strHTML.Append("<table width=\"975\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");

                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"4\" class=\"name\"><input name=\"\" type=\"checkbox\" value=\"\" /><span><a href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">订单编号：" + entity.Orders_SN + "</a></span><span>提交时间：" + entity.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</span><a href=\"javascript:;\">买方：" + member_name + "</a></td>");
                strHTML.Append("</tr>");

                strHTML.Append(Orders_List_Goods(entity, goodsList));

                strHTML.Append("</table>");
                strHTML.Append("</div>");
            }
            Response.Write(strHTML.ToString());
            Response.Write("<div class=\"blk08\" style=\"border:0;\">");
            if (page != null)
            {
                pub.Page(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            }

            Response.Write("</div>");
        }
        else
        {
            strHTML.Append("<div class=\"table_blk02\">");
            strHTML.Append("<table width=\"975\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");

            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"6\" style=\"text-align:center;\">");
            strHTML.Append("暂无订单信息！");
            strHTML.Append("</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");

            strHTML.Append("</div>");
            Response.Write(strHTML.ToString());
        }


    }


    /// <summary>
    /// 订单列表商品
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="goodslist"></param>
    /// <returns></returns>
    public string Orders_List_Goods(OrdersInfo entity, IList<OrdersGoodsInfo> goodslist)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sub = new StringBuilder();
        int i = 0;
        int goodslistCount = 0;
        sb.Append("<tr>");
        if (goodslist != null)
        {
            goodslistCount = goodslist.Count;
            foreach (OrdersGoodsInfo goods in goodslist)
            {
                if (goods.Orders_Goods_Product_ID > 0)
                {
                    i++;
                    if (i == 1)
                    {
                        sb.Append("<td width=\"362\" class=\"td02\">");
                        sb.Append("<dl>");
                        sb.Append("<input name=\"\" type=\"checkbox\" value=\"\" />");
                        sb.Append("<dt><a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goods.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                        sb.Append("<dd>");
                        sb.Append("<a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">" + goods.Orders_Goods_Product_Name + "</a>");
                        sb.Append("</dd>");
                        sb.Append("<span>" + goods.Orders_Goods_Amount + "</span>");
                        sb.Append("<div class=\"clear\"></div>");
                        sb.Append("</dl>");
                        sb.Append("</td>");
                    }
                    else
                    {
                        sub.Append("<tr>");
                        sub.Append("<td class=\"td02\">");
                        sub.Append("<dl>");
                        sub.Append("<input name=\"\" type=\"checkbox\" value=\"\" />");
                        sub.Append("<dt><a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">");
                        sub.Append("<img src=\"" + pub.FormatImgURL(goods.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                        sub.Append("<dd>");
                        sub.Append("<a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">" + goods.Orders_Goods_Product_Name + "</a>");
                        sub.Append("</dd>");
                        sub.Append("<span>" + goods.Orders_Goods_Amount + "</span>");
                        sub.Append("<div class=\"clear\"></div>");
                        sub.Append("</dl>");
                        sub.Append("</td>");
                        sub.Append("</tr>");
                    }
                }
            }
        }

        sb.Append("<td width=\"197\" rowspan=\"" + goodslistCount + "\">");
        sb.Append("<p>" + OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus) + "</p>");
        string Orders_Delivery_Num = "select COUNT(*) from Orders_Delivery  where Orders_Delivery_OrdersID=" + entity.Orders_ID + "";

        int OrdersDeliveryNum = tools.NullInt(DBHelper.ExecuteScalar(Orders_Delivery_Num));

        sb.Append("<a href=\"/supplier/order_delivery_list.aspx?order_id=" + entity.Orders_ID + "\"><p style=\"color:#ff6600\">发货单数(" + OrdersDeliveryNum + ")个</p></a>");



        sb.Append("</td>");
        sb.Append("<td width=\"223\" style=\"text-align:center\" rowspan=\"" + goodslistCount + "\">");
        sb.Append("<p><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></p>");
        //sb.Append("<p>(含运费：" + pub.FormatCurrency(entity.Orders_Total_Freight + entity.Orders_Total_FreightDiscount) + ")</p>");
        sb.Append("<p>" + entity.Orders_Payway_Name + "</p>");
        sb.Append("</td>");

        sb.Append("<td width=\"186\" rowspan=\"" + goodslistCount + "\">");

        if (tools.NullInt(Session["supplier_id"]) == entity.Orders_SupplierID && entity.Orders_Status == 0)
        {
            sb.Append("<a href=\"/supplier/orders_confirm.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a06\">确认订单</a>");
        }

        if (tools.NullInt(Session["supplier_id"]) == entity.Orders_SupplierID && entity.Orders_PaymentStatus == 4 && entity.Orders_DeliveryStatus == 0 && entity.Orders_Status == 1)
        {
            sb.Append("<a href=\"/supplier/orders_delivery.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a06\">发货</a>");
        }


        sb.Append("<a href=\"/supplier/Order_Contract_View.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">合同预览</a>");
        sb.Append("<a href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">查看订单</a>");

        //会员没有确认签收的发货单
        int noaccept = tools.NullInt(DBHelper.ExecuteScalar("select COUNT(*) from Orders_Delivery where Orders_Delivery_ReceiveStatus!=2 AND orders_delivery_OrdersID=" + entity.Orders_ID + ""));

        int deliverys = tools.NullInt(DBHelper.ExecuteScalar("select COUNT(*) from Orders_Delivery where  orders_delivery_OrdersID=" + entity.Orders_ID + ""));


        //已经交易成功的订单
        int nocompleteordersnum = tools.NullInt(DBHelper.ExecuteScalar("select count(*) from orders where Orders_Status=2 and Orders_ID=" + entity.Orders_ID + ""));

        if (nocompleteordersnum == 0)
        {
            //若发货单
            if (noaccept > 0)
            {
                //尚存在,会员 确认签收的收货单

            }
            else
            {
                //
                if (deliverys > 0)
                {
                    //所有的收货单会员都已经确认签收
                    sb.Append("<a href=\"/supplier/orders_do.aspx?action=order_complete&Orders_ID=" + entity.Orders_ID + "\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">订单完成</a>");
                }
                else
                {
                    //若基于该订单下的发货单数量为0的话,也不显示"订单完成"按钮  
                }

            }
        }
        else
        {
            //订单为零
        }






        //只有失败的订单才能删除
        if (entity.Orders_Status == 3)
        {
            ///member/orders_do.aspx?action=orderfirm&Orders_ID=" + entity.Orders_ID + "\"
            sb.Append("<a href=\"/supplier/orders_do.aspx?action=order_delete&Orders_ID=" + entity.Orders_ID + "\" target=\"_blank\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">删除订单</a>");
        }
        sb.Append("</td>");

        sb.Append("</tr>");

        sb.Append(sub);

        return sb.ToString();
    }


    public int OrdersCountByStatus(int menu, int supplier_id)
    {
        QueryInfo Query = new QueryInfo();

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "in", supplier_id.ToString()));

        switch (menu)
        {
            case 1:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));  //未支付
                break;
            case 2:
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1")); //已支付
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_PaymentStatus", "=", "4"));
                break;
            case 3:
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1")); //待发货
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_PaymentStatus", "=", "4"));
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "6"));
                break;
            case 4:
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "1")); //已发货
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "2"));
                break;
            case 5:
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_Status", "=", "2")); //已完成
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_Status", "=", "3"));
                break;
        }
        //if (orders_sn != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SN", "=", orders_sn));
        //}
        //if (date_start != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        //}
        //if (date_end != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        //}

        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        // IList<OrdersInfo> orderslist = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);
        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }


    }

    //查看采购订单
    public void Buy_order_Detail(string orders_sn)
    {
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
        MemberInfo memberInfo = null;
        string strOrdersResponsible = string.Empty;//初始化运输责任
        strOrdersResponsible = entity.Orders_Responsible == 1 ? "买家责任" : "卖家责任";
        string memberName = "";

        if (entity != null)
        {
            memberInfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberInfo != null && memberInfo.MemberProfileInfo != null)
            {
                memberName = memberInfo.MemberProfileInfo.Member_Company;
            }
            else
            {
                memberName = "";
            }

            strHTML.Append("<div class=\"blk06\">");
            strHTML.Append("<h2><span>");




            strHTML.Append("</span>订单信息</h2>");

            strHTML.Append("<div class=\"b06_main_sz\">");
            strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
            strHTML.Append("<p><span>买方用户：</span>" + memberName + "</p>");
            strHTML.Append("<p><span>订单状态：</span>" + OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus) + "</p>");
            strHTML.Append("<p><span>订单总计：</span>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</p>");
            strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
            strHTML.Append("<p><span>支付方式：</span>" + entity.Orders_Payway_Name + "</p>");
            strHTML.Append("<p><span>运送责任：</span>" + strOrdersResponsible + "</p>");
            //strHTML.Append("<p><span>发票信息：</span>" + Orders_Detail_InvoiceInfo(entity.Orders_ID) + "</p>");
            strHTML.Append("<p><span>订单备注：</span>" + entity.Orders_Note + "</p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");

            strHTML.Append("<div class=\"blk14_1\">");
            strHTML.Append("<h2>订单进度</h2>");
            strHTML.Append("<div class=\"b06_main02_sz\">");

            strHTML.Append("<div class=\"b06_info_sz\">");
            strHTML.Append("<ul>");

            strHTML.Append("<li class=\"on\"><img src=\"/images/tu_01_2.jpg\"><i></i><em></em><p>买方提交订单<span>" + entity.Orders_Addtime.ToString("yyyy-MM-dd HH:mm") + "</span></p></li>");

            if (entity.Orders_SupplierStatus == 0)
            {
                strHTML.Append("<li><img src=\"/images/tu_02.jpg\"><i></i><em></em><p>确认订单</p></li>");
            }
            else
            {
                strHTML.Append("<li class=\"on\"><img src=\"/images/tu_02_2.jpg\"><i></i><em></em><p>确认订单<span>" + entity.Orders_SupplierStatus_Time.ToString("yyyy-MM-dd HH:mm") + "</p></li>");
            }

            //if (entity.Orders_MemberStatus == 0)
            //{
            //    strHTML.Append("<li><img src=\"/images/tu_03.jpg\"><i></i><em></em><p>买方确认</p></li>");
            //}
            //else
            //{
            //    strHTML.Append("<li class=\"on\"><img src=\"/images/tu_03_2.jpg\"><i></i><em></em><p>买方确认<span>" + entity.Orders_MemberStatus_Time.ToString("yyyy-MM-dd HH:mm") + "</p></li>");
            //}

            if (entity.Orders_PaymentStatus == 1 || entity.Orders_PaymentStatus == 2 || entity.Orders_PaymentStatus == 3 || entity.Orders_PaymentStatus == 4)
            {
                strHTML.Append("<li class=\"on\"><img src=\"/images/tu_04_2.jpg\"><i></i><em></em><p>买方支付订单<span>" + entity.Orders_PaymentStatus_Time.ToString("yyyy-MM-dd HH:mm") + "</span></p></li>");
            }
            else
            {
                strHTML.Append("<li><img src=\"/images/tu_04.jpg\"><i></i><em></em><p>买方支付订单</p></li>");
            }

            if (entity.Orders_DeliveryStatus == 1 || entity.Orders_DeliveryStatus == 2 || entity.Orders_DeliveryStatus == 2 || entity.Orders_DeliveryStatus == 2 || entity.Orders_DeliveryStatus == 2)
            {
                strHTML.Append("<li class=\"on\"><img src=\"/images/tu_05_2.jpg\"><i></i><em></em><p>商品发货<span>" + entity.Orders_DeliveryStatus_Time.ToString("yyyy-MM-dd HH:mm") + "</span></p></li>");
            }
            else
            {
                strHTML.Append("<li><img src=\"/images/tu_05.jpg\"><i></i><em></em><p>商品发货</p></li>");
            }

            if (entity.Orders_Status == 2)
            {
                strHTML.Append("<li class=\"on\"><img src=\"/images/tu_06_2.jpg\"><i></i><em></em><p>签收完成</p></li>");
            }
            else
            {
                strHTML.Append("<li><img src=\"/images/tu_06.jpg\"><i></i><em></em><p>签收完成</p></li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</div>");

            //订单日志
            strHTML.Append(Orders_Detail_Log(entity.Orders_ID));

            //商品清单
            strHTML.Append(Order_Detail_Goods(entity, 1));

            strHTML.Append("<div class=\"b06_info04_sz\">");
            strHTML.Append("<div class=\"b06_info04_left_sz\">订单费用调整原因：" + entity.Orders_Total_PriceDiscount_Note + "</div>");
            strHTML.Append("<div class=\"b06_info04_right_sz\">");
            strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_Price) + "</i><span>商品总金额：</span></p>");
            //strHTML.Append("<p><i>¥0.00</i><span>-订单优惠：</span></p>");
            //strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_Freight) + "</i><span>+运费：</span></p>");

            if (entity.Orders_Total_PriceDiscount > 0)
            {
                strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_PriceDiscount) + "</i><span>+订单调整费用：</span></p>");
            }
            else
            {
                strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_PriceDiscount) + "</i><span>-订单调整费用：</span></p>");
            }

            if (entity.Orders_Total_FreightDiscount > 0)
            {
                strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_FreightDiscount) + "</i><span>+订单运费调整：</span></p>");
            }
            else
            {
                strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_FreightDiscount) + "</i><span>-订单运费调整：</span></p>");
            }

            strHTML.Append("<p><i><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></i><span>应付金额：</span></p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");

            strHTML.Append("</div>");
            strHTML.Append("</div>");

            strHTML.Append("<div class=\"blk06\">");
            //strHTML.Append("<div class=\"b06_main03\">" + entity.Orders_ContractAdd + "</div>");
            strHTML.Append("</div>");


            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }

    //查看销售订单
    public void order_Detail(string orders_sn)
    {
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
        MemberInfo memberInfo = null;
        string memberName = "";
        string strOrdersResponsible = string.Empty;//初始化运输责任
        strOrdersResponsible = entity.Orders_Responsible == 1 ? "买家责任" : "卖家责任";
        if (entity != null)
        {
            memberInfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberInfo != null && memberInfo.MemberProfileInfo != null)
            {
                memberName = memberInfo.MemberProfileInfo.Member_Company;
            }
            else
            {
                memberName = "";
            }

            strHTML.Append("<div class=\"blk06\">");
            strHTML.Append("<h2><span>");
            if (tools.NullInt(Session["supplier_id"]) == entity.Orders_SupplierID && entity.Orders_Status == 0)
            {
                strHTML.Append("<a href=\"/supplier/orders_confirm.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a06\">确认订单</a>");
            }
            if (tools.NullInt(Session["supplier_id"]) == entity.Orders_SupplierID && entity.Orders_Status == 1)
            {
                strHTML.Append("<a href=\"/supplier/Logistics_Add.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a06\">申请物流</a>");
            }
            if (tools.NullInt(Session["supplier_id"]) == entity.Orders_SupplierID && entity.Orders_PaymentStatus == 4 && entity.Orders_Status == 1)
            {
                strHTML.Append("<a href=\"/supplier/orders_delivery.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a06\">发货</a>");
            }
            if (entity != null)
            {
                ContractInfo ContractEntity = null;
                ContractEntity = new Contract().GetContractByID(entity.Orders_ContractID);
                if (ContractEntity != null)
                {

                    //strHTML.Append("<a href=\"/member/Order_Contract.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">合同修改</a>");

                    strHTML.Append("<a href=\"/supplier/Order_Contract_View.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">合同预览</a>");


                }
            }


            strHTML.Append("</span>订单信息</h2>");

            strHTML.Append("<div class=\"b06_main_sz\">");
            strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
            strHTML.Append("<p><span>买方用户：</span>" + memberName + "</p>");
            strHTML.Append("<p><span>订单状态：</span>" + OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus) + "</p>");
            //strHTML.Append("<p><span>订单总计：</span>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "（含运费" + pub.FormatCurrency(entity.Orders_Total_Freight + entity.Orders_Total_FreightDiscount) + "）</p>");
            strHTML.Append("<p><span>订单总计：</span>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</p>");
            strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
            strHTML.Append("<p><span>支付方式：</span>" + entity.Orders_Payway_Name + "</p>");
            strHTML.Append("<p><span>运送责任：</span>" + strOrdersResponsible + "</p>");
            //strHTML.Append("<p><span>发票信息：</span>" + Orders_Detail_InvoiceInfo(entity.Orders_ID) + "</p>");
            strHTML.Append("<p><span>订单备注：</span>" + entity.Orders_Note + "</p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");


            //商品清单
            strHTML.Append(Order_Detail_Goods(entity, 1));
            strHTML.Append("<div class=\"b06_info04_sz\">");
            strHTML.Append("<div class=\"b06_info04_right_sz\">");
            strHTML.Append("<>");

            strHTML.Append("<p><i id=\"ss\">" + pub.FormatCurrency(entity.Orders_Total_Price) + "</i><span>商品总金额：</span></p>");



            strHTML.Append("<p><i  id=\"ss2\"><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></i><span>应付金额：</span></p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");




            //付款单
            IList<OrdersPaymentInfo> entitys = Mypayment.GetOrdersPaymentsByOrdersID(entity.Orders_ID);
            if (entitys != null)
            {
                strHTML.Append("<div class=\"blk14_1\">");
                strHTML.Append("<h2>付款单</h2>");
                strHTML.Append("<div class=\"b06_main02_sz\">");
                strHTML.Append(OrdersDetails_Payments(entity));
                strHTML.Append("</div>");
                strHTML.Append("</div>");
            }




            //发货单
            strHTML.Append("<div class=\"blk14_1\">");
            strHTML.Append("<h2>发货单</h2>");
            strHTML.Append("<div class=\"b06_main02_sz\">");
            strHTML.Append(OrdersDetail_Delivery_List(entity.Orders_ID, entity.Orders_Status, entity.Orders_BuyerID));
            strHTML.Append("</div>");
            strHTML.Append("</div>");




            strHTML.Append("<div class=\"blk14_1\">");
            strHTML.Append("<h2>订单进度</h2>");
            strHTML.Append("<div class=\"b06_main02_sz\">");
            //订单日志
            strHTML.Append(Orders_Detail_Log(entity.Orders_ID));
            strHTML.Append("</div>");
            strHTML.Append("</div>");





            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }


    //查看销售订单
    public void order_Details(string orders_sn)
    {
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
        MemberInfo memberInfo = null;
        string memberName = "";

        if (entity != null)
        {
            memberInfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberInfo != null && memberInfo.MemberProfileInfo != null)
            {
                memberName = memberInfo.MemberProfileInfo.Member_Company;
            }
            else
            {
                memberName = "";
            }
            //商品清单
            strHTML.Append(Order_Detail_Goods(entity, 0));
            strHTML.Append("<div class=\"b06_info04_sz\">");
            strHTML.Append("<div class=\"b06_info04_right_sz\">");
            if (entity.Orders_Status == 0)
            {
                strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#frm_batch').submit();\" class=\"a11\"></a>");
            }
            else
            {

            }
            strHTML.Append("<p><i id=\"ss\">" + pub.FormatCurrency(entity.Orders_Total_Price) + "</i><span>商品总金额：</span></p>");



            strHTML.Append("<p><i  id=\"ss2\"><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></i><span>应付金额：</span></p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");




            strHTML.Append("</div>");
            strHTML.Append("</div>");





            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }

    //商家中心付款单
    public string OrdersDetails_Payments(OrdersInfo OrderInfo)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        //strHTML.Append("<div class=\"title04\">商品清单</div>");
        //strHTML.Append("<h2>商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 160px;\">付款单号</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付金额</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付方式</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付备注</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付时间</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付状态</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersPaymentInfo> entitys = Mypayment.GetOrdersPaymentsByOrdersID(OrderInfo.Orders_ID);
        if (entitys != null)
        {
            //IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            //IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersPaymentInfo entity in entitys)
            {
                //GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                //strHTML.Append("<tr>");
                //if (goodsinfo.Orders_Goods_Product_ID > 0)
                //{
                //    strHTML.Append(" <td width=\"398\">");
                //    strHTML.Append("<dl>");
                //    strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                //    strHTML.Append("<dd>");
                //    strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong></a></p>");
                //    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                //    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Goods_Product_ID) + "</span></p>");
                //    strHTML.Append("</dd>");
                //    strHTML.Append("<div class=\"clear\"></div>");
                //    strHTML.Append("</dl>");
                //    strHTML.Append("</td>");
                //    strHTML.Append("<td width=\"191\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                //    strHTML.Append("<td width=\"194\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                //    strHTML.Append("<td width=\"190\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");
                //}
                //strHTML.Append("</tr>");

                strHTML.Append("<tr>");

                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_DocNo + "</td>");
                strHTML.Append("<td width=\"160px\">" + pub.FormatCurrency(entity.Orders_Payment_Amount) + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Name + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Note + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Addtime + "</td>");
                strHTML.Append("<td width=\"160px\">" + PaymentStatus(entity.Orders_Payment_PaymentStatus) + "</td>");





                strHTML.Append("</tr>");


            }
        }

        strHTML.Append("</table>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }


    //订单详情页面 发货单   
    public string OrdersDetail_Delivery_List(int orders_id, int Order_status, int Orders_BuyerID)
    {
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
        StringBuilder strHTML = new StringBuilder();


        //int member_id = tools.NullInt(Session["member_id"]);
        int member_id = Orders_BuyerID;
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }

        int i = 0;
        string page_url = "?action=list";
        strHTML.Append("<div class=\"b06_info03_sz\">");

        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 138px;\">发货单号</li>");
        strHTML.Append("<li style=\"width: 108px;\">运输方式</li>");
        strHTML.Append("<li style=\"width: 108px;\">签收状态</li>");
        strHTML.Append("<li style=\"width:138px;\">发货数量</li>");
        strHTML.Append("<li style=\"width:138px;\">货款金额</li>");
        strHTML.Append("<li style=\"width:138px;\">付款时间</li>");
        strHTML.Append("<li style=\"width: 190px;\">操 作</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");



        string SqlField = "OD.*, O.Orders_SN, O.Orders_Type, O.Orders_BuyerID";
        string SqlParam = "";
        if (orders_id > 0)
        {
            //SqlParam = "where  O.Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]);
            SqlParam = "where  O.Orders_ID=" + orders_id + "  AND O.Orders_BuyerID = " + member_id;
        }
        else
        {
            SqlParam = "where  O.Orders_BuyerID = " + member_id;
        }
        string SqlTable = "Orders_Delivery AS OD INNER JOIN Orders AS O ON OD.Orders_Delivery_OrdersID = O.Orders_ID";




        string SqlOrder = "ORDER BY Orders_Delivery_ID DESC";
        string SqlCount = "SELECT COUNT(OD.Orders_Delivery_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DT = null;
        try
        {
            int PageSize = 20;
            int RecordCount = tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
            int PageCount = tools.CalculatePages(RecordCount, PageSize);
            int CurrentPage = tools.DeterminePage(current_page, PageCount);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT " + SqlField + " FROM " + SqlTable + " WHERE OD.Orders_Delivery_ID IN (");
            strSQL.Append("	SELECT Orders_Delivery_ID FROM (");
            strSQL.Append("		SELECT ROW_NUMBER() OVER(" + SqlOrder + ") AS RowNum, OD.Orders_Delivery_ID FROM " + SqlTable + " " + SqlParam);
            strSQL.Append("	) T");
            strSQL.Append("	WHERE RowNum > " + ((CurrentPage - 1) * PageSize) + " AND RowNum  <= " + (CurrentPage * PageSize));
            strSQL.Append(") " + SqlOrder);
            DT = DBHelper.Query(strSQL.ToString());

            if (DT != null)
            {
                foreach (DataRow RdrList in DT.Rows)
                {
                    i++;
                    double Orders_Delivery_Goods_ProductAmount = 0;
                    int Orders_Delivery_ID = tools.NullInt(RdrList["Orders_Delivery_ID"]);

                    IList<OrdersDeliveryGoodsInfo> entitys = Mydelivery.GetOrdersDeliveryGoods(Orders_Delivery_ID);
                    if (entitys != null)
                    {
                        int i1 = 0;
                        foreach (var entity in entitys)
                        {
                            i1++;
                            if (i1 == 1)
                            {
                                Orders_Delivery_Goods_ProductAmount =entity.Orders_Delivery_Goods_ProductAmount;
                            }
                        }
                    }
                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("   <td height=\"35\" align=\"left\" style=\"width: 138px;\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\" style=\"color:#ff6600;\">" + tools.NullStr(RdrList["Orders_Delivery_DocNo"]) + "</a></span></td>");



                    strHTML.Append("   <td align=\"center\" style=\"width: 108px;\" >" + tools.NullStr(RdrList["Orders_Delivery_TransportType"]) + "</td>");


                    // 收货状态0：未签收  1、9 结算中 2：已结算
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">未签收</td>");
                    }
                    else if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 2)
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">已签收</td>");
                    }
                    else
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">结算中</td>");
                    }

                    string DeliverProductSumSQL = "select sum(Orders_Delivery_Goods_ProductAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")";
                    //if (DeliverProductSumSQL)
                    //{

                    //}
                    double OrdersDeliveryProductSum = 0;
                    //计算当前收货单--发货数量
                    object AAA = DBHelper.ExecuteScalar(DeliverProductSumSQL);
                    if ((AAA == null) || (AAA == ""))
                    {

                    }
                    else
                    {
                        OrdersDeliveryProductSum = tools.CheckFloat(AAA.ToString());
                    }

                    //  int OrdersDeliveryProductSum = 0;
                    int OrdersDeliveryReceiveSum = 0;
                    object BBB = DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")");
                    if (BBB == null)
                    {

                    }
                    else
                    {
                        OrdersDeliveryReceiveSum = tools.NullInt(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")").ToString());

                    }
                    //计算当前收货单--签收数量



                    //签收数量
                    strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + OrdersDeliveryProductSum + "</td>");


                    //发货单商品单价

                    //double Orders_Delivery_Goods_ProductPrice = tools.CheckFloat(DBHelper.ExecuteScalar("select Orders_Delivery_Goods_ProductPrice from Orders_Delivery_Goods  where Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());

                    //贷款金额 1:发货金额  2:签收金额 
                    //double ProductSum = tools.CheckFloat(DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ProductAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());
                    //double ReceiveSum = tools.CheckFloat(DBHelper.ExecuteScalar("select SUM(  Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());

                    object CCC = DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ProductAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "");
                    double ProductSum = 0;
                    if (CCC == null)
                    {

                    }
                    else
                    {
                        ProductSum = tools.NullDbl(DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ProductAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());
                    }
                    //double ProductSum = tools.NullDbl(DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ProductAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());
                    object DDD = DBHelper.ExecuteScalar("select SUM(  Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "");
                    double ReceiveSum = 0;
                    if (DDD == null)
                    {

                    }
                    else
                    {
                        ReceiveSum = tools.NullDbl(DBHelper.ExecuteScalar("select SUM(  Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());
                    }






                    //未签收显示,贷款金额(发货数量*商品单价)
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        //未签收显示 所有商品  发货金额*发货数量  累加
                        strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + ProductSum + "</td>");
                    }
                    else
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + ProductSum + "</td>");

                        //签收显示 所有商品  收货金额*发货数量  累加
                        //strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + ReceiveSum + "</td>");
                    }
                    //else
                    //{
                    //    strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" +  + "</td>"); 
                    //}


                    strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + tools.NullDate(RdrList["Orders_Delivery_Addtime"]) + "</td>");
                    strHTML.Append("   <td align=\"center\" style=\"width: 190px;\"><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" style=\"color:#ff6600;\">查看</a>");

                    //if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 1)
                    //{

                    //    strHTML.Append("   <a href=\"/order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\" style=\"color:#ff6600;\">确认结算</a>");
                    //}


                    //保存签收
                    //if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    //{                       
                    //    strHTML.Append("<a href=\"/supplier/orders_do.aspx?action=orderaccept&Delivery_ID=" + Orders_Delivery_ID + " \"  target=\"_blank\" style=\"color:#ff6600;\">申请结算</a>");

                    //}

                    //商家修改发货单状态条件(申请扣款（改单价）)  订单确认  发货状态: 已发货 签收状态:只要非签收结算都能修改价格
                    //Orders_Delivery_DeliveryStatus:发货状态
                    if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 1)
                    {
                        strHTML.Append("&nbsp;&nbsp;<a href=\"/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + " \" target=\"_blank\" class=\"a11\" style=\"color:#ff6600;\">修改发货单</a>");

                    }
                    //商家修改发货单状态条件(申请扣款（改单价）)  订单确认  发货状态: 已发货 签收状态:只要非签收结算都能修改价格
                    else if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        strHTML.Append("&nbsp;&nbsp;<a href=\"/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + " \" target=\"_blank\" class=\"a11\" style=\"color:#ff6600;\">申请结算</a>");

                    }
                    else if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 9)
                    {
                        strHTML.Append("&nbsp;&nbsp;<a href=\"/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + " \" target=\"_blank\" class=\"a11\" style=\"color:#ff6600;\">修改发货单</a>");
                    }
                    else
                    {

                    }





                    strHTML.Append(" </tr>");
                }

                strHTML.Append("</div>");
                strHTML.Append("</tbody>");
                strHTML.Append("</table>");


            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"6\" style=\"text-align:center;\">暂无发货单信息！</td>");
                strHTML.Append("</tr>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return strHTML.ToString();

    }

    //编辑订单商品清单
    public string Orders_Edit_Goods(int Orders_ID, int Orders_buyerID, bool ispreview)
    {
        string strHTML = "";
        double goods_price = 0;
        strHTML += "";
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">";
        strHTML += "    <tr class=\"goods_head\">";
        strHTML += "        <td>商品编号</td>";
        strHTML += "        <td>商品</td>";
        strHTML += "        <td>规格</td>";
        strHTML += "        <td>生产企业</td>";
        strHTML += "        <td>交货期</td>";
        strHTML += "        <td>市场价</td>";
        strHTML += "        <td>单价</td>";
        strHTML += "        <td>数量</td>";

        strHTML += "        <td>合计</td>";
        if (ispreview == false)
        {
            //strHTML += "        <td>删除</td>";
        }
        strHTML += "    </tr>";

        OrdersInfo ordersinfo = Myorder.GetOrdersByID(Orders_ID);
        if (ordersinfo == null)
        { return ""; }
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        if (goodstmps != null)
        {
            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;

            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                //子商品信息
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);

                strHTML += "    <tr class=\"goods_list\">";
                if (ispreview == false)
                {
                    if (ordersinfo.Orders_Type > 1)
                    {
                        strHTML += "        <td><input type=\"text\" size=\"20\" value=\"" + entity.Orders_Goods_Product_Code + "\" name=\"Orders_Goods_Product_Code\" onchange=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmp_edit&code='+this.value+'&goods_id=" + entity.Orders_Goods_ID + "&Orders_ID=" + Orders_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');\"></td>";
                    }
                    else
                    {
                        strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
                    }
                }
                else
                {
                    strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
                }
                strHTML += "        <td><table align=\"left\">";
                strHTML += "<tr>";
                goods_price = goods_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);

                if (entity.Orders_Goods_Type == 2)
                {
                    strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + entity.Orders_Goods_Product_Img + "\" /></td>";
                    strHTML += "    <td>" + entity.Orders_Goods_Product_Name + "<img width=\"9\" height=\"9\" src=\"/images/display_close.gif\" id=\"subicon_" + entity.Orders_Goods_ID + "\" style=\"margin-left:5px;vertical-align:middle;cursor:pointer;\" onclick=\"displaySubGoods(" + entity.Orders_Goods_ID + ");\" /></td>";
                }
                else
                {

                    strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + new Public_Class().FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
                    strHTML += "    <td>" + entity.Orders_Goods_Product_Name;
                    if (GoodsListSub.Count > 0) { strHTML += "<img width=\"20\" src=\"/images/icon_gift.gif\" style=\"margin-left:5px;vertical-align:middle;cursor:pointer;\" onclick=\"displaySubGoods(" + entity.Orders_Goods_ID + ");\" />"; }
                    strHTML += "</td>";
                }

                strHTML += "</tr>";
                strHTML += "</table></td>";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Spec + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Maker + "</td>";
                if (ispreview == false)
                {
                    strHTML += "        <td><input type=\"text\" size=\"20\" value=\"" + entity.Orders_Goods_Product_DeliveryDate + "\" name=\"Orders_Goods_Product_DeliveryDate\" onchange=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmp_edit&deliverydate='+this.value+'&goods_id=" + entity.Orders_Goods_ID + "&Orders_ID=" + Orders_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');\"></td>";
                }
                else
                {
                    strHTML += "        <td>" + entity.Orders_Goods_Product_DeliveryDate + "</td>";
                }
                strHTML += "        <td class=\"price_list\">" + Public_Class.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                if (ispreview == false)
                {
                    strHTML += "        <td><input type=\"text\" size=\"10\" value=\"" + entity.Orders_Goods_Product_Price + "\" name=\"Orders_Goods_Product_Price\" onchange=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmp_edit&price='+this.value+'&goods_id=" + entity.Orders_Goods_ID + "&Orders_ID=" + Orders_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');";
                    if (Orders_ID > 0)
                    {
                        strHTML += "MM_findObj('Orders_Total_Price').value=MM_findObj('goodsprice').value;updateorderprice();";
                    }
                    strHTML += "\"></td>";
                }
                else
                {
                    strHTML += "        <td class=\"price_list\">" + Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                }

                if (ispreview == false)
                {
                    strHTML += "        <td><input type=\"text\" size=\"5\" value=\"" + entity.Orders_Goods_Amount + "\" name=\"Orders_Goods_Amount\" onchange=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmp_edit&buyamount='+this.value+'&goods_id=" + entity.Orders_Goods_ID + "&Orders_ID=" + Orders_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');";
                    if (Orders_ID > 0)
                    {
                        strHTML += "MM_findObj('Orders_Total_Price').value=MM_findObj('goodsprice').value;updateorderprice();";
                    }
                    strHTML += "\"></td>";
                }
                else
                {
                    strHTML += "        <td>" + entity.Orders_Goods_Amount + "</tD>";
                }
                strHTML += "        <td class=\"price_list\">" + Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                if (ispreview == false)
                {
                    strHTML += "        <td align=\"center\"><input class=\"btn_01\" type=\"button\" value=\"删除\" onclick=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmp_del&goods_id=" + entity.Orders_Goods_ID + "&Orders_ID=" + Orders_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');";
                    if (Orders_ID > 0)
                    {
                        strHTML += "MM_findObj('Orders_Total_Price').value=MM_findObj('goodsprice').value;updateorderprice();";
                    }

                    strHTML += "\"/></td>";
                }
                else
                {
                    Session["total_price"] = tools.CheckFloat(Session["total_price"].ToString()) + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
                }
                strHTML += "    </tr>";

                if (GoodsListSub.Count > 0)
                {
                    strHTML += "    <tr id=\"subgoods_" + entity.Orders_Goods_ID + "\" class=\"goods_list\" style=\"display:none;\">";
                    strHTML += "        <td colspan=\"14\"><table width=\"100%\">";
                    foreach (OrdersGoodsTmpInfo goodsSub in GoodsListSub)
                    {
                        strHTML += "<tr>";
                        if (goodsSub.Orders_Goods_Type == 1)
                        {
                            strHTML += "    <td align=\"left\" class=\"goods_dotbtm\"><span class=\"t12_red\">[赠品]</span> [" + goodsSub.Orders_Goods_Product_Code + "] " + goodsSub.Orders_Goods_Product_Name + " [" + goodsSub.Orders_Goods_Product_Spec + "] [" + goodsSub.Orders_Goods_Product_Maker + "] </td>";


                            strHTML += "    <td align=\"right\" width=\"10%\" class=\"goods_dotbtm t12_red\">1*" + goodsSub.Orders_Goods_Amount + "</td>";
                            continue;
                        }
                        strHTML += "    <td align=\"left\" class=\"goods_dotbtm\">[" + goodsSub.Orders_Goods_Product_Code + "] " + goodsSub.Orders_Goods_Product_Name + " [" + goodsSub.Orders_Goods_Product_Spec + "] [" + goodsSub.Orders_Goods_Product_Maker + "] </td>";

                        strHTML += "    <td align=\"right\" width=\"10%\" class=\"goods_dotbtm t12_red\">1*" + (goodsSub.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "</td>";
                        strHTML += "</tr>";
                    }
                    strHTML += "<tr><td align=\"right\" colspan=\"3\"><img width=\"15\" src=\"/images/icon_fold.jpg\" style=\"vertical-align:middle;cursor:pointer;\" onclick=\"displaySubGoods(" + entity.Orders_Goods_ID + ")\" /></td></tr>";
                    strHTML += "</table></td>";
                    strHTML += "    </tr>";
                }
                GoodsListSub = null;

            }
        }
        strHTML += "</table>";
        strHTML += "<input type=\"hidden\" name=\"goodsprice\" id=\"goodsprice\" value=\"" + goods_price.ToString("0.00") + "\">";
        goodstmps = null;
        return strHTML;
    }




    public IList<OrdersGoodsTmpInfo> OrdersGoodstmpSearch(IList<OrdersGoodsTmpInfo> GoodsList, int ParentID)
    {
        IList<OrdersGoodsTmpInfo> OrdersGoodsList = new List<OrdersGoodsTmpInfo>();
        foreach (OrdersGoodsTmpInfo entity in GoodsList)
        {
            if (entity.Orders_Goods_ParentID == ParentID) { OrdersGoodsList.Add(entity); }
        }
        return OrdersGoodsList;
    }



    /// <summary>
    /// 订单详情
    /// </summary>
    /// <param name="orders_sn"></param>
    public void Edit_Orders_Details(string orders_sn)
    {
        ContractInfo ContractEntity = null;
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        if (entity != null)
        {
            ContractEntity = My_Contract.GetContractByID(entity.Orders_ContractID);
        }
        SupplierInfo supplierInfo = null;
        string supplierName = "";


        if (entity != null)
        {
            QueryLoanProjectJsonInfo JsonInfo = null;

            if (entity.Orders_Loan_proj_no != "")
            {
                JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
            }
            else
            {
                JsonInfo = new QueryLoanProjectJsonInfo();
            }

            supplierInfo = GetSupplierByID(entity.Orders_SupplierID);
            if (supplierInfo != null)
            {
                supplierName = supplierInfo.Supplier_CompanyName;
            }
            else
            {
                supplierName = "--";
            }


            //商品清单修改

            //Supplier_Edit_Orders_Goods_Price(orders_sn);

            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/member/orders_list.aspx?menu=1");
        }
    }
    /// <summary>
    /// 商家修改订单合同商品单价   即修改Order_Goods表
    /// </summary>
    /// <param name="orders_sn"></param>
    //public void Supplier_Edit_Orders_Goods_Price(string orders_sn)
    //{
    //    StringBuilder strHTML = new StringBuilder();
    //    OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
    //    SupplierInfo supplierInfo = null;
    //    string supplierName = "";


    //    if (entity != null)
    //    {
    //        QueryLoanProjectJsonInfo JsonInfo = null;

    //        if (entity.Orders_Loan_proj_no != "")
    //        {
    //            JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
    //        }
    //        else
    //        {
    //            JsonInfo = new QueryLoanProjectJsonInfo();
    //        }

    //        //supplierInfo = GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
    //        supplierInfo = GetSupplierByID(entity.Orders_SupplierID);
    //        if (supplierInfo != null)
    //        {
    //            supplierName = supplierInfo.Supplier_CompanyName;
    //        }
    //        else
    //        {
    //            supplierName = "--";
    //        }


    //        //商品清单
    //        strHTML.Append(Supplier_Edit_Order_Detail_Goods(entity, orders_sn));


    //        Response.Write(strHTML.ToString());
    //    }
    //    else
    //    {
    //        Response.Redirect("/member/orders_list.aspx?menu=1");
    //    }
    //}






    /// <summary>
    /// 订单详情页商品列表 未改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string Order_Detail_Goods_old(OrdersInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 190px;\">单价</li>");
        strHTML.Append("<li style=\"width: 190px;\">数量</li>");
        strHTML.Append("<li style=\"width: 190px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
        if (entitys != null)
        {
            IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersGoodsInfo goodsinfo in GoodsList)
            {
                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Goods_Product_ID > 0)
                {
                    strHTML.Append(" <td width=\"398\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></dt>");
                    strHTML.Append("<dd>");
                    strHTML.Append("<p>" + goodsinfo.Orders_Goods_Product_Name + "</p>");
                    strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    strHTML.Append("<td width=\"191\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                    strHTML.Append("<td width=\"194\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                    strHTML.Append("<td width=\"190\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }



    /// <summary>
    /// 订单详情页商品列表
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string Order_Detail_Goods(OrdersInfo entity, int IsDetails)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>商品清单</h2>");


        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 394px;\">商 品</li>");
        strHTML.Append("<li style=\"width: 190px;\">单 价</li>");
        strHTML.Append("<li style=\"width: 190px;\">数 量</li>");
        strHTML.Append("<li style=\"width: 190px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<form id=\"frm_batch\"  name=\"frm_batch\" action=\"/supplier/orders_do.aspx?Orders_ID=" + entity.Orders_ID + "&IsDetails=" + IsDetails + "\" method=\"post\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
        int i = 0;
        if (entitys != null)
        {

            IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            int GoodsNum = GoodsList.Count;
            IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersGoodsInfo goodsinfo in GoodsList)
            {
                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                //未审核
                string productURL = pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString());
                if (entity.Orders_Status == 0)
                {

                    strHTML.Append("<tr>");
                    if (goodsinfo.Orders_Goods_Product_ID > 0)
                    {
                        i++;

                        strHTML.Append(" <td width=\"398\" style=\"padding:0\">");
                        strHTML.Append("<dl>");

                        strHTML.Append("<dd style=\" text-align: center;    width: 200px;\">");
                        strHTML.Append("<a  href=\"" + productURL + "\"><p><strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong></p></a>");
                        //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                        strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Goods_Product_ID) + "</span></p>");
                        strHTML.Append("</dd>");
                        strHTML.Append("<div class=\"clear\"></div>");
                        strHTML.Append("</dl>");
                        strHTML.Append("</td>");

                        strHTML.Append("<td width=\"191\" id=\"Orders_Goods_Product_Price\" value=\"" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "\"> <input onblur=\"sum('buy_amount_" + goodsinfo.Orders_Goods_ID + "',this.id,'Orders_Goods_EveryProduct_Sum" + i + "'," + GoodsList.Count + ")\" name=\"buy_price_" + goodsinfo.Orders_Goods_ID + "\" id=\"buy_price_" + goodsinfo.Orders_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Goods_Product_Price + "\" /></td>");

                        strHTML.Append("<td width=\"194\" id=\"Orders_Goods_Amount\"><input name=\"buy_amount_" + goodsinfo.Orders_Goods_ID + "\" onblur=\"sum(this.id,'buy_price_" + goodsinfo.Orders_Goods_ID + "','Orders_Goods_EveryProduct_Sum" + i + "'," + GoodsList.Count + ")\" id=\"buy_amount_" + goodsinfo.Orders_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Goods_Amount + "\"                       /></td>");

                        strHTML.Append("<td width=\"190\" id=\"Orders_Goods_EveryProduct_Sum" + i + "\">");
                        strHTML.Append(" " + goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount + "");
                        strHTML.Append("  </td>");



                        strHTML.Append("   <input name=\"Orders_Goods_Product_Price_" + i + "\" type=\"hidden\" class=\"Orders_Goods_Product_Price_" + i + "\" id=\"Orders_Goods_Product_Price_" + i + "\" value=\"价格\" />");
                        strHTML.Append("   <input name=\"Orders_Goods_Amount_" + i + "\" type=\"hidden\" class=\"Orders_Goods_Amount_" + i + "\" id=\"Orders_Goods_Amount_" + i + "\" value=\"数量\" />");
                        strHTML.Append("   <input name=\"Orders_Goods_ID\" type=\"hidden\" class=\"Orders_Goods_ID\" id=\"Orders_Goods_ID\" value=\"" + goodsinfo.Orders_Goods_ID + "\" />");
                    }

                    strHTML.Append("</tr>");
                }
                else
                {
                    strHTML.Append("<tr>");
                    if (goodsinfo.Orders_Goods_Product_ID > 0)
                    {
                        i++;

                        strHTML.Append(" <td width=\"398\" style=\"padding:0\">");
                        strHTML.Append("<dl>");

                        strHTML.Append("<dd style=\" text-align: center;    width: 200px;\">");

                        //strHTML.Append("<p>" + goodsinfo.Orders_Goods_Product_Name + "</p>");
                        strHTML.Append("<a  href=\"" + productURL + "\"><p><strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong></p></a>");
                        //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                        strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Goods_Product_ID) + "</span></p>");
                        strHTML.Append("</dd>");
                        strHTML.Append("<div class=\"clear\"></div>");
                        strHTML.Append("</dl>");
                        strHTML.Append("</td>");
                        strHTML.Append("<td width=\"191\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                        strHTML.Append("<td width=\"194\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                        strHTML.Append("<td width=\"190\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");

                        strHTML.Append("  </td>");

                    }

                    strHTML.Append("</tr>");




                }

            }
        }
        strHTML.Append("</table>");


        strHTML.Append("  <table width=\"972px;\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" id=\"apply_1_content\" style=\"padding-top:45px;\" class=\"table_padding_5\">");
        strHTML.Append("                                      <tr   style=\"border-top:1px solid #dddddd;\">");
        strHTML.Append("                                          <td style=\"border-top: 1px solid #dddddd;\">订单编号");
        strHTML.Append("                                          </td>");
        strHTML.Append("                                          <td style=\"border-top: 1px solid #dddddd;\">商品金额");
        strHTML.Append("                                          </td>");
        strHTML.Append("                                          <td style=\"border-top: 1px solid #dddddd;\">订单金额");
        strHTML.Append("                                          </td>");
        strHTML.Append("                                          <td style=\"border-top: 1px solid #dddddd;\">价格调整说明");
        strHTML.Append("                                          </td>");





        strHTML.Append("                                      </tr>");
        strHTML.Append("                                      <tr >");
        strHTML.Append("                                          <td>" + entity.Orders_SN + "</td>");
        strHTML.Append("                                          </td>");
        strHTML.Append("                                          <td id=\"ss3\">" + pub.FormatCurrency(entity.Orders_Total_Price) + "</td>");
        strHTML.Append("                                          <td id=\"ss4\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</td>");
        if (entity.Orders_Status == 0)
        {
            strHTML.Append("                                          <td>");
            strHTML.Append("                                              <textarea id=\"Orders_Total_PriceDiscount_Note\" name=\"Orders_Total_PriceDiscount_Note\" cols=\"50\" rows=\"5\"> " + entity.Orders_Total_PriceDiscount_Note + "</textarea></td>");
        }
        else
        {
            strHTML.Append("                                          <td>");
            //strHTML.Append("                                              <textarea id=\"Orders_Total_PriceDiscount_Note\" name=\"Orders_Total_PriceDiscount_Note\" cols=\"50\" rows=\"5\"> " + entity.Orders_Total_PriceDiscount_Note + "</td>");
            strHTML.Append("                " + entity.Orders_Total_PriceDiscount_Note + "                    ");

            strHTML.Append("                                      </td>");
        }



        strHTML.Append("                                      </tr>");

        strHTML.Append("                              </table>");
        //if (entity.Orders_Status == 0)
        //{
        //    strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#frm_batch').submit();\" class=\"a11\"></a>");
        //}
        //else
        //{

        //}





        strHTML.Append("  <input name=\"action\" type=\"hidden\" id=\"action\" value=\"save_order\" />");
        strHTML.Append("</form>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="OrdersID"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    public string Order_Logistics_Goods(int OrdersID, int Type)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(OrdersID);
        if (Type == 0)
        {
            if (entitys != null)
            {
                strHTML.Append("<table width=\"1200\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");

                IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
                IList<OrdersGoodsInfo> GoodsListSub = null;
                int i = 1;
                foreach (OrdersGoodsInfo goodsinfo in GoodsList)
                {

                    GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);

                    if (goodsinfo.Orders_Goods_Product_ID > 0)
                    {
                        i++;
                        if (i % 2 == 0)
                        {
                            strHTML.Append("<tr>");
                        }
                        else
                        {
                            strHTML.Append("<tr class=\"bg\">");
                        }
                        strHTML.Append("<td width=\"220\" style=\"text-align:left;\">" + goodsinfo.Orders_Goods_Product_Name + "</td>");
                        strHTML.Append("<td width=\"140\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                        strHTML.Append("<td width=\"100\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" style=\"width:100px;height:100px;\"  /></td>");
                        strHTML.Append("<td width=\"617\" style=\"text-align:left;\">" + goodsinfo.Orders_Goods_Product_Spec + "</td>");
                        strHTML.Append("</tr>");

                    }
                }
                strHTML.Append("</table>");
            }
        }
        else
        {
            if (entitys != null)
            {
                strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");

                strHTML.Append("<tr>");
                strHTML.Append("<td width=\"350\" class=\"name\">商品名称</td>");
                strHTML.Append("<td width=\"100\" class=\"name\">数量</td>");
                strHTML.Append("<td width=\"100\" class=\"name\">缩略图</td>");
                strHTML.Append("<td width=\"423\" class=\"name\">产品规格</td>");
                strHTML.Append("</tr>");
                IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
                IList<OrdersGoodsInfo> GoodsListSub = null;

                foreach (OrdersGoodsInfo goodsinfo in GoodsList)
                {

                    GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);

                    if (goodsinfo.Orders_Goods_Product_ID > 0)
                    {

                        strHTML.Append("<tr>");
                        strHTML.Append("<td>" + goodsinfo.Orders_Goods_Product_Name + "</td>");
                        strHTML.Append("<td>" + goodsinfo.Orders_Goods_Amount + "</td>");
                        strHTML.Append("<td><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" style=\"width:100px;height:100px;\"  /></td>");
                        strHTML.Append("<td>" + goodsinfo.Orders_Goods_Product_Spec + "</td>");
                        strHTML.Append("</tr>");

                    }
                }
                strHTML.Append("</table>");
            }
        }

        return strHTML.ToString();
    }
    /// <summary>
    /// 订单详情页订单日志
    /// </summary>
    /// <param name="Orders_ID"></param>
    /// <returns></returns>
    public string Orders_Detail_Log(int Orders_ID)
    {
        int i = 0;
        StringBuilder strHTML = new StringBuilder();
        IList<OrdersLogInfo> logList = MyOrdersLog.GetOrdersLogsByOrdersID(Orders_ID);
        if (logList != null)
        {
            strHTML.Append("<div class=\"b06_info02_sz\">");
            foreach (OrdersLogInfo entity in logList)
            {
                i++;
                strHTML.Append("<p><i>" + i + "</i>" + entity.Orders_Log_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + " " + tools.CleanHTML(entity.Orders_Log_Remark) + "</p>");
            }
            strHTML.Append("</div>");
        }

        return strHTML.ToString();
    }

    public void Orders_NoteEdit()
    {
        string orders_note = tools.CheckStr(Request["orders_note"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
        }
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["supplier_id"].ToString()) && ordersinfo.Orders_BuyerType == 1 && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                ordersinfo.Orders_Note = orders_note;
                MyOrders.EditOrders(ordersinfo);
            }
        }
        Response.Redirect("/supplier/order_view.aspx?orders_sn=" + orders_sn);
    }

    public void Orders_Close()
    {
        string orders_close_note = tools.CheckStr(Request["orders_close_note"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/supplier/order_list.aspx");
        }
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["supplier_id"].ToString()) && ordersinfo.Orders_BuyerType == 1 && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                ordersinfo.Orders_Fail_Addtime = DateTime.Now;
                ordersinfo.Orders_Fail_Note = orders_close_note;
                int total_usecoin = ordersinfo.Orders_Total_UseCoin;
                ordersinfo.Orders_Fail_SysUserID = 0;
                ordersinfo.Orders_Status = 3;
                MyOrders.EditOrders(ordersinfo);
                //返还积分
                if (total_usecoin > 0)
                {
                    Myorder.Member_Coin_AddConsume(total_usecoin, "订单" + orders_sn + "取消返还积分", tools.CheckInt(Session["member_id"].ToString()), true);
                }

                //虚拟账号余额回返
                double account_pay = 0;
                account_pay = ordersinfo.Orders_Account_Pay;
                //if (account_pay > 0)
                //{
                //    Myorder.Member_Account_Log(ordersinfo.Orders_BuyerID, account_pay, "订单" + orders_sn + "取消,抵扣金额退回");
                //}

                //恢复优惠券使用
                //Myorder.Orders_Coupon_ReUse(ordersinfo.Orders_ID);

                //删除赠送优惠券
                //Myorder.Orders_SendCoupon_Action("delete", tools.NullInt(Session["member_id"]), ordersinfo.Orders_ID, "", 0);

                Myorder.Orders_Log(ordersinfo.Orders_ID, "", "取消", "成功", "订单取消,取消原因：" + orders_close_note + "");
            }
        }
        Response.Redirect("/supplier/order_list.aspx");
    }

    //订单物流状态
    public void order_Logistic(string orders_sn)
    {
        string orders_buyerid = null;
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        if (entity != null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("      <tr>");
            Response.Write("        <td height=\"10\"></td>");
            Response.Write("      </tr>");
            Response.Write("  <tr>");
            Response.Write("    <td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");
            Response.Write("      <tr>");
            Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">订单编号</td>");
            Response.Write("        <td class=\"t14_red\">" + entity.Orders_SN + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Response.Write("</td>");
            Response.Write("      </tr>");
            Response.Write("      <tr class=\"dotline_h\">");
            Response.Write("        <td height=\"10\"></td>");
            Response.Write("        <td></td>");
            Response.Write("      </tr>");

            if (entity.Orders_Status > 0 && entity.Orders_DeliveryStatus > 0)
            {
                OrdersDeliveryInfo deliveryinfo = Mydelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 1, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                if (deliveryinfo != null)
                {

                    Response.Write("      <tr>");
                    Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">物流信息</td>");
                    Response.Write("        <td >快递单号：" + deliveryinfo.Orders_Delivery_Code + "&nbsp; 物流公司：" + deliveryinfo.Orders_Delivery_companyName + "&nbsp;&nbsp;&nbsp;&nbsp;");

                    Response.Write("</td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\"></td>");
                    Response.Write("        <td class=\"t14\" style=\"line-height:30px;\" >");

                    string typeCom = "";
                    string ApiKey = "d2fcad63758650e7";

                    typeCom = pub.GetKuaiDiCom(deliveryinfo.Orders_Delivery_companyName);

                    string apiurl = "http://api.kuaidi100.com/api?id=" + ApiKey + "&com=" + typeCom + "&nu=" + deliveryinfo.Orders_Delivery_Code + "&show=2&muti=1&order=asc";
                    WebRequest request = WebRequest.Create(@apiurl);
                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    Encoding encode = Encoding.UTF8;
                    StreamReader reader = new StreamReader(stream, encode);
                    string detail = reader.ReadToEnd();
                    Response.Write(detail);
                    Response.Write("</td>");
                    Response.Write("      </tr>");
                }
            }

            Response.Write("  <tr>");
            Response.Write("    <td height=\"20\"></td>");
            Response.Write("  </tr>");
            Response.Write("</table></td>");

            Response.Write("  </tr>");
            Response.Write("</table>");
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=3");
        }
    }

    //订单商品清单  未改
    public void order_Detail_Goods_old(OrdersInfo entity)
    {
        Response.Write("        <table border=\"0\" cellspacing=\"0\" width=\"100%\" cellpadding=\"5\">");
        Response.Write("          <tr>");
        Response.Write("            <td><table cellpadding=\"0\" width=\"100%\" cellspacing=\"1\" bgcolor=\"#EEBB31\">");
        Response.Write("              <tr>");
        Response.Write("                <td align=\"center\" bgcolor=\"#fcf9c6\" height=\"28\">商品</td>");

        Response.Write("                <td align=\"center\" width=\"70\" bgcolor=\"#fcf9c6\">单价</td>");
        Response.Write("                <td align=\"center\" width=\"40\" bgcolor=\"#fcf9c6\">数量</td>");
        Response.Write("                <td align=\"center\" width=\"90\" bgcolor=\"#fcf9c6\">获赠" + Application["coin_name"] + "</td>");
        Response.Write("                <td align=\"center\" width=\"80\" bgcolor=\"#fcf9c6\">合计</td>");
        Response.Write("              </tr>");
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
        if (entitys != null)
        {
            IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersGoodsInfo goodsinfo in GoodsList)
            {
                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                Response.Write("        <tr>");
                Response.Write("          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");
                Response.Write("            <tr>");

                if (goodsinfo.Orders_Goods_Product_ID > 0)
                {
                    if (goodsinfo.Orders_Goods_Type == 2)
                    {
                        Response.Write("              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + goodsinfo.Orders_Goods_Product_Img + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>");
                        Response.Write("              <td id=\"pack" + goodsinfo.Orders_Goods_ID + "1\" style=\"display:none;\"><a href=\"javascript:void(0);\" onclick=\"show('pack" + goodsinfo.Orders_Goods_ID + "2');hide('pack" + goodsinfo.Orders_Goods_ID + "1');show('subproduct" + goodsinfo.Orders_Goods_ID + "')\"> <strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong>  <img src=\"/images/sub.gif\" border=\"0\" align=\"absmiddle\"></a></td>");
                        Response.Write("              <td id=\"pack" + goodsinfo.Orders_Goods_ID + "2\"><a href=\"javascript:void(0);\" onclick=\"hide('pack" + goodsinfo.Orders_Goods_ID + "2');show('pack" + goodsinfo.Orders_Goods_ID + "1');hide('subproduct" + goodsinfo.Orders_Goods_ID + "')\">" + goodsinfo.Orders_Goods_Product_Name + "  <img src=\"/images/subj.gif\" border=\"0\" align=\"absmiddle\"></a></td>");
                    }
                    else
                    {
                        if (GoodsListSub.Count > 0)
                        {
                            Response.Write("              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></a></td>");
                            Response.Write("              <td><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + goodsinfo.Orders_Goods_Product_Name + "</a>&nbsp; <a href=\"javascript:void(0);\" onclick=\"show('subproduct" + goodsinfo.Orders_Goods_Product_ID + "')\"><img src=\"/images/icon_gift.gif\" height=\"15\" border=\"0\" align=\"absmiddle\"></a></td>");
                        }
                        else
                        {
                            Response.Write("              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></a></td>");
                            Response.Write("              <td><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + goodsinfo.Orders_Goods_Product_Name + "</a></td>");
                        }
                    }
                }
                else
                {
                    Response.Write("              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>");
                    Response.Write("              <td>" + goodsinfo.Orders_Goods_Product_Name + "</td>");
                }
                Response.Write("            </tr>");
                Response.Write("          </table></td>");
                Response.Write("          <td align=\"center\" bgcolor=\"#FFFFFF\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                Response.Write("          <td align=\"center\" bgcolor=\"#FFFFFF\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                Response.Write("          <td align=\"center\" bgcolor=\"#FFFFFF\">" + goodsinfo.Orders_Goods_Product_Coin * goodsinfo.Orders_Goods_Amount + "</td>");

                Response.Write("          <td align=\"right\" bgcolor=\"#FFFFFF\" class=\"price_small\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");
                Response.Write("        </tr>");
                if (GoodsListSub.Count > 0)
                {
                    Response.Write("<tr id=\"subproduct" + goodsinfo.Orders_Goods_ID + "\"><td colspan=\"6\" bgcolor=\"#FFFFFF\">");
                    Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\">");
                    foreach (OrdersGoodsInfo ent in GoodsListSub)
                    {
                        if (ent.Orders_Goods_Type == 1)
                        {
                            Response.Write("<tr><td><span class=\"t12_red\">[赠品]</span> " + ent.Orders_Goods_Product_Name + "</td><td align=\"left\" class=\"t12_red\" width=\"160\">" + (ent.Orders_Goods_Amount / goodsinfo.Orders_Goods_Amount) + "×" + goodsinfo.Orders_Goods_Amount + "</td><td width=\"20\"><a href=\"javascript:void(0);\" onclick=\"hide('subproduct" + goodsinfo.Orders_Goods_ID + "')\"><img src=\"/images/ico_fold.jpg\" border=\"0\"></a></td></tr>");
                        }
                        else
                        {
                            Response.Write("<tr><td style=\"border-bottom:1px dotted #dcdcdc\" colspan=\"3\"> &nbsp;" + ent.Orders_Goods_Product_Name + "</td>");

                            Response.Write("          <td width=\"60\" style=\"border-bottom:1px dotted #dcdcdc\" align=\"center\" class=\"t12_red\">" + (ent.Orders_Goods_Amount / goodsinfo.Orders_Goods_Amount) + "×" + goodsinfo.Orders_Goods_Amount + "</td>");

                            Response.Write("</tr>");
                        }
                    }
                    Response.Write("</table>");
                    Response.Write("</td></tr>");
                }
            }
        }

        Response.Write("            </table></td>");
        Response.Write("          </tr>");
        Response.Write("          <tr>");
        Response.Write("            <td align=\"right\" class=\"t12\"><span style=\"width: 180px;text-align: right;\">商品总金额：</span><em class=\"price\">" + pub.FormatCurrency(entity.Orders_Total_Price) + "</em></td>");
        Response.Write("          </tr>");
        Response.Write("          <tr>");
        Response.Write("            <td align=\"right\" class=\"t12\"><span style=\"width: 180px;text-align: right;\">运费：</span> <em class=\"price\">" + pub.FormatCurrency(entity.Orders_Total_Freight) + "</em></td>");
        Response.Write("          </tr>");

        if (entity.Orders_Total_PriceDiscount != 0)
        {
            Response.Write("          <tr>");
            Response.Write("            <td align=\"right\" class=\"t12\"><span style=\"width: 180px;text-align: right;\">价格调整：</span> <em class=\"price\">");
            if (entity.Orders_Total_PriceDiscount > 0)
            {
                Response.Write("+" + pub.FormatCurrency(entity.Orders_Total_PriceDiscount) + "");
            }
            else
            {
                Response.Write("-" + pub.FormatCurrency(0 - entity.Orders_Total_PriceDiscount) + "");
            }
            Response.Write("</em></td>");
            Response.Write("          </tr>");
        }

        if (entity.Orders_Total_FreightDiscount != 0)
        {
            Response.Write("<tr>");
            Response.Write("            <td align=\"right\" class=\"t12\"><span style=\"width: 180px;text-align: right;\">运费调整：</span> <em class=\"price\">");
            if (entity.Orders_Total_FreightDiscount > 0)
            {
                Response.Write("+" + pub.FormatCurrency(entity.Orders_Total_FreightDiscount) + "");
            }
            else
            {
                Response.Write("-" + pub.FormatCurrency(0 - entity.Orders_Total_FreightDiscount) + "");
            }
            Response.Write("</em></td>");
            Response.Write("          </tr>");
        }
        Response.Write("          <tr>");
        Response.Write("            <td align=\"right\" class=\"t12\"> <span  style=\"width: 180px;text-align: right;\">应付总额：</span><em class=\"price\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice + entity.Orders_Total_Freight - entity.Orders_Account_Pay + entity.Orders_Total_PriceDiscount + entity.Orders_Total_FreightDiscount) + "</em></em></td>");
        Response.Write("          </tr>");
        Response.Write("        </table>");
    }



    //订单发票操作
    public void Orders_Invoice_Action(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["Orders_id"]);
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        OrdersInfo ordersinfo = MyOrders.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (("," + supplier_orders + ",").IndexOf("," + Orders_ID + ",") > 0)
            {
                switch (operate)
                {
                    case "open":
                        ordersinfo.Orders_InvoiceStatus = 1;
                        break;
                    case "cancel":
                        ordersinfo.Orders_InvoiceStatus = 2;
                        break;
                }
                if (MyOrders.EditOrders(ordersinfo))
                {
                    switch (operate)
                    {
                        case "open":
                            //记录订单日志
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "开票", "成功", "订单开票");
                            break;
                        case "cancel":
                            //记录订单日志
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "退票", "成功", "订单退票");
                            break;
                    }
                }
                Response.Redirect("/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
            }
        }
        Response.Redirect("/supplier/index.aspx");
    }

    //订单配货中
    public void Orders_Delivery_Prepare()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        OrdersInfo ordersinfo = MyOrders.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (("," + supplier_orders + ",").IndexOf("," + Orders_ID + ",") > 0)
            {
                if (ordersinfo.Orders_DeliveryStatus == 0 && ordersinfo.Orders_Status == 1)
                {
                    //orders_sn = ordersinfo.Orders_SN;
                    ordersinfo.Orders_DeliveryStatus = 6;
                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "确认", "成功", "订单配货中");
                    }
                }
                Response.Redirect("/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
            }
        }
        Response.Redirect("/supplier/index.aspx");
    }

    //订单已签收
    public void Orders_Delivery_Accept()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        OrdersInfo ordersinfo = MyOrders.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (("," + supplier_orders + ",").IndexOf("," + Orders_ID + ",") > 0)
            {
                if (ordersinfo.Orders_DeliveryStatus == 1 && ordersinfo.Orders_Status == 1)
                {
                    //orders_sn = ordersinfo.Orders_SN;
                    ordersinfo.Orders_DeliveryStatus = 2;
                    ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        //Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "订单签收", "成功", "订单签收");
                    }
                }
                Response.Redirect("/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
            }
        }
        Response.Redirect("/supplier/index.aspx");
    }



    public void Orders_Delivery_Accepts()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        int Product_ID, Goods_Amount, Goods_Type;

        int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);

        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
        }
        //OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["member_id"]));
        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["supplier_id"]));
        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx?payment=1&menu=1");
        }
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus == 0)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx?payment=1&menu=1");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;

        //检查是否包含签收数量商品
        bool isreceived = false;
        IList<OrdersDeliveryGoodsInfo> deliverygoods = Mydelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);
        if (deliverygoods != null)
        {
            foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
            {
                int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
                if (receiveamount > 0)
                {
                    isreceived = true;
                }
            }
        }


        IList<OrdersGoodsInfo> entitys = null;
        //更新签收状态
        ODEntity.Orders_Delivery_ReceiveStatus = 9;
        if (Mydelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
        {
            //更新签收数量
            if (deliverygoods != null)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
                {
                    int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "_ReceivedAmount"]);
                    if (receiveamount > 0)
                    {
                        deliverygood.Orders_Delivery_Goods_ReceivedAmount = receiveamount;
                        if (Mydelivery.EditOrdersDeliveryGoods(deliverygood))
                        {
                            OrdersDeliveryInfo entity = Mydelivery.GetOrdersDeliveryByID(deliverygood.Orders_Delivery_Goods_DeliveryID, pub.CreateUserPrivilege(""));
                            entity.Orders_Delivery_ReceiveStatus = 9;
                            Mydelivery.EditOrdersDelivery(entity, pub.CreateUserPrivilege(""));
                        }
                    }
                }
            }

            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "发货单签收", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收！");
            //if (CheckAllReceive(ordersinfo.Orders_ID))
            //{
            //    ordersinfo.Orders_DeliveryStatus = 2;
            //    ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;

            //    ordersinfo.Orders_Status = 2;
            //    if (MyOrders.EditOrders(ordersinfo))
            //    {
            //        if (ordersinfo.Orders_DeliveryStatus == 2)
            //        {
            //            // messageclass.SendMessage(0, 2, ordersinfo.Orders_SupplierID, 0, "订单" + ordersinfo.Orders_SN + "已签收");


            //            SupplierInfo supplierInfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            //            if (supplierInfo != null)
            //            {
            //                string[] content = { supplierInfo.Supplier_CompanyName, ordersinfo.Orders_SN };
            //                //推送短信到供货商
            //                //new SMS().Send(supplierInfo.Supplier_Mobile, "orders_accept", content);
            //            }
            //        }

            //        if (ordersinfo.Orders_Status == 2)
            //        {
            //            entitys = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
            //            if (entitys != null)
            //            {
            //                foreach (OrdersGoodsInfo entity in entitys)
            //                {
            //                    Product_ID = entity.Orders_Goods_Product_ID;
            //                    Goods_Amount = entity.Orders_Goods_Amount;
            //                    Goods_Type = entity.Orders_Goods_Type;

            //                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
            //                    {
            //                        MyProduct.UpdateProductSaleAmount(Product_ID, Goods_Amount);
            //                    }
            //                }
            //            }
            //            entitys = null;

            //            messageclass.SendMessage(0, 1, ordersinfo.Orders_BuyerID, 0, "订单" + ordersinfo.Orders_SN + "已完成");

            //            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "订单完成", "成功", "订单交易成功！");

            //            //推送短信到采购商
            //            //new SMS().Send(tools.NullStr(Session["member_mobile"]), "orders_success", ordersinfo.Orders_SN);
            //        }
            //    }
            //}
        }
        //pub.Msg("positive", "操作成功", "操作成功", true, "order_delivery_list.aspx");
        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "");

    }



    //检查订单产品实际库存
    public bool CheckProductStockEnough(int orders_id)
    {
        bool resultvalue = true;
        int stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        IList<OrdersGoodsInfo> goodses = MyOrders.GetGoodsListByOrderID(orders_id);
        if (goodses != null)
        {
            foreach (OrdersGoodsInfo entity in goodses)
            {
                productstockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_Amount = 0;
                if (entity.Orders_Goods_Type != 2)
                {
                    productstockinfo = Get_Productstock(entity.Orders_Goods_Product_ID);
                    stock = productstockinfo.Product_Stock_Amount;
                    if (productstockinfo.Product_Stock_IsNoStock == 0 && entity.Orders_Goods_Amount > stock)
                    {
                        resultvalue = false;
                    }
                }
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                {
                    PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                    if (package != null)
                    {
                        IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                        if (packageproducts != null)
                        {
                            productstockinfo = Get_Package_Stock(packageproducts);
                            stock = productstockinfo.Product_Stock_Amount;
                            if (productstockinfo.Product_Stock_IsNoStock == 0 && entity.Orders_Goods_Amount > stock)
                            {
                                resultvalue = false;
                            }
                        }
                        else
                        {
                            resultvalue = false;
                        }
                    }
                }
            }
        }
        return resultvalue;
    }

    //检查订单产品实际库存
    public bool CheckSelectProductStockEnough(int orders_id)
    {
        bool resultvalue = true;
        int stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        string goods_id = tools.CheckStr(Request.Form["goods_id"]);
        int ProductAmount = 0;
        if (goods_id.Length > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.CurrentPage = 1;
            Query.PageSize = 10;
            Query.OrderInfos.Add(new OrderInfo("OrdersGoodsInfo.Orders_Goods_ID", "ASC"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_OrdersID", "=", orders_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_ID", "IN", goods_id));
            IList<OrdersGoodsInfo> goodses = MyOrders.GetOrdersGoodsList(Query);
            if (goodses != null)
            {

                foreach (OrdersGoodsInfo entity in goodses)
                {
                    productstockinfo.Product_Stock_IsNoStock = 0;
                    productstockinfo.Product_Stock_Amount = 0;
                    if (entity.Orders_Goods_Type != 2)
                    {
                        productstockinfo = Get_Productstock(entity.Orders_Goods_Product_ID);
                        stock = productstockinfo.Product_Stock_Amount;
                        if (productstockinfo.Product_Stock_IsNoStock == 0 && entity.Orders_Goods_Amount > stock)
                        {
                            resultvalue = false;
                        }
                    }
                    if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                    {
                        PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                        if (package != null)
                        {
                            IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                            if (packageproducts != null)
                            {
                                productstockinfo = Get_Package_Stock(packageproducts);
                                stock = productstockinfo.Product_Stock_Amount;
                                if (productstockinfo.Product_Stock_IsNoStock == 0 && entity.Orders_Goods_Amount > stock)
                                {
                                    resultvalue = false;
                                }
                            }
                            else
                            {
                                resultvalue = false;
                            }
                        }
                    }
                }
            }
        }

        return resultvalue;
    }

    //获取产品实际库存
    public ProductStockInfo Get_Productstock(int product_id)
    {
        int product_stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        productstockinfo.Product_Stock_Amount = 0;
        productstockinfo.Product_Stock_IsNoStock = 0;
        ProductInfo product = MyProduct.GetProductByID(product_id, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (product != null)
        {
            productstockinfo.Product_Stock_IsNoStock = product.Product_IsNoStock;
            productstockinfo.Product_Stock_Amount = product.Product_StockAmount;
        }
        return productstockinfo;
    }

    //获取捆绑产品实际库存
    public ProductStockInfo Get_Package_Stock(IList<PackageProductInfo> packageproducts)
    {
        int Package_Stock = 0;
        bool IsNoStock = true;
        int cur_stock = 0;
        string Package_Product_Arry = "0";
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        foreach (PackageProductInfo obj in packageproducts)
        {
            Package_Product_Arry = Package_Product_Arry + "," + obj.Package_Product_ProductID;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Clear();
        Query.OrderInfos.Clear();
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Package_Product_Arry));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> products = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (products != null)
        {
            foreach (PackageProductInfo packproduct in packageproducts)
            {
                foreach (ProductInfo product in products)
                {
                    if (packproduct.Package_Product_ProductID == product.Product_ID)
                    {
                        //只统计非零库存产品库存
                        if (product.Product_IsNoStock == 0)
                        {
                            IsNoStock = false;
                            cur_stock = (int)(product.Product_StockAmount / packproduct.Package_Product_Amount);
                        }
                    }
                }

                if (Package_Stock > cur_stock || Package_Stock == 0)
                {
                    Package_Stock = cur_stock;
                }
            }
        }
        if (IsNoStock)
        {
            packagestockinfo.Product_Stock_IsNoStock = 1;
        }
        else
        {
            packagestockinfo.Product_Stock_IsNoStock = 0;
        }
        packagestockinfo.Product_Stock_Amount = Package_Stock;
        return packagestockinfo;
    }

    //物流公司选择
    public string Delivery_Company_Select(string select_name, string Code)
    {
        string way_list = "";
        way_list = "<select name=\"" + select_name + "\" " + Code + ">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));
        IList<DeliveryWayInfo> deliveryways = deliveryway.GetDeliveryWays(Query, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
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
    public string orders_delivery_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersDeliveryInfo deliveryinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            deliveryinfo = Mydelivery.GetOrdersDeliveryBySn(sn, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

            if (deliveryinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    //订单确认
    public void Orders_Confirm()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string select_price = tools.CheckStr(Request["select_price"]);
        string select_freight = tools.CheckStr(Request["select_freight"]);
        double Orders_Total_PriceDiscount = tools.NullDbl(Request["Orders_Total_PriceDiscount"]);
        double Orders_Total_FreightDiscount = tools.NullDbl(Request["Orders_Total_FreightDiscount"]);
        string Orders_Total_PriceDiscount_Note = tools.CheckStr(Request["Orders_Total_PriceDiscount_Note"]);
        string Orders_ContractAdd = Request["Orders_ContractAdd"];

        MemberInfo memInfo = null;
        string member_mobile = "";
        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, tools.NullInt(Session["supplier_id"]));
        if (ordersinfo != null)
        {
            memInfo = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memInfo != null)
            {
                member_mobile = memInfo.Member_LoginMobile;
            }

            if (ordersinfo.Orders_Status == 0)
            {
                ordersinfo.Orders_Status = 1;
                ordersinfo.Orders_SupplierStatus = 1;
                if (select_price == "-")
                {
                    ordersinfo.Orders_Total_PriceDiscount = -Orders_Total_PriceDiscount;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice - Orders_Total_PriceDiscount;
                }
                else
                {
                    ordersinfo.Orders_Total_PriceDiscount = Orders_Total_PriceDiscount;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice + Orders_Total_PriceDiscount;
                }
                ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;

                if (select_freight == "-")
                {
                    ordersinfo.Orders_Total_FreightDiscount = -Orders_Total_FreightDiscount;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice - Orders_Total_FreightDiscount;
                }
                else
                {
                    ordersinfo.Orders_Total_FreightDiscount = Orders_Total_FreightDiscount;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice + Orders_Total_FreightDiscount;
                }

                if (ordersinfo.Orders_Payway == 2)
                {
                    ordersinfo.Orders_ApplyCreditAmount = ordersinfo.Orders_Total_AllPrice;
                }

                ordersinfo.Orders_ContractAdd = Orders_ContractAdd;

                if (MyOrders.EditOrders(ordersinfo))
                {
                    messageclass.SendMessage(1, 1, ordersinfo.Orders_BuyerID, 0, "您的订单 " + ordersinfo.Orders_SN + " 供货商已确认！");

                    //发送短信
                    //new SMS().Send(tools.NullStr(member_mobile), "supplier_confirm_orders_remind", ordersinfo.Orders_SN);

                    Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_companyname"]), "确认", "成功", "供应商确认订单");
                }
            }
            pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
        }
        Response.Redirect("/supplier/index.aspx");
    }





    //发货单签收结算
    public void Orders_Delivery_AcceptSettle()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);
        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;
        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, tools.NullInt(Session["supplier_id"]));
        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx");
        }
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus == 1)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx");
        }
        //获取本单已结算签收金额
        double totalreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ReceiveStatus=2 and Orders_Delivery_OrdersID=" + ordersinfo.Orders_ID));
        //获取本发货单签收金额
        double curreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
        //获取本发货单签收佣金金额
        double CommissionPrice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_brokerage*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
        CommissionPrice = Math.Round(CommissionPrice, 2);
        //结算金额超过采购总金额
        if (totalreceiveprice + curreceiveprice > ordersinfo.Orders_Total_AllPrice)
        {
            int Supplier_ID1 = 0;
            string supplier_name = "";
            ZhongXin mycredit = new ZhongXin();
            MemberInfo memberinfo1 = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo1 != null)
            {
                Supplier_ID1 = memberinfo1.Member_SupplierID;

            }
            if (Supplier_ID1 > 0)
            {
                SupplierInfo supplierinfo1 = GetSupplierByID(Supplier_ID1);
                if (supplierinfo1 != null)
                {
                    supplier_name = supplierinfo1.Supplier_CompanyName;
                }


                ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
                if (accountinfo != null)
                {
                    decimal accountremain = 0;
                    accountremain = mycredit.GetAmount(accountinfo.SubAccount);
                    if (accountremain < ((decimal)curreceiveprice - ((decimal)ordersinfo.Orders_Total_AllPrice - (decimal)totalreceiveprice)))
                    {
                        pub.Msg("error", "错误信息", "买方账户余额不足，请联系买方充值入金！", false, "{back}");
                    }

                    double accountprice = curreceiveprice - (ordersinfo.Orders_Total_AllPrice - totalreceiveprice);

                    string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice - accountprice, accountprice, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

                    if (strResult == "true")
                    {

                        new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));

                        ////添加短信提醒2015-5-14
                        //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        //if (memberinfo != null)
                        //{

                        //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
                        //}
                        ODEntity.Orders_Delivery_ReceiveStatus = 2;

                        if (Mydelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
                        {
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_delivery_list.aspx");
                        //
                    }
                    else
                    {
                        //new ZhongXin().SaveZhongXinAccountLog(ordersinfo.Orders_BuyerID, curreceiveprice * -1, "[撤销][签收结算]订单编号：" + ordersinfo.Orders_SN);

                        pub.Msg("error", "错误信息", strResult, false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "您尚未开通中信账户！", false, "{back}");
                }

            }
            else
            {
                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
            }
        }
        else
        {
            int Supplier_ID1 = 0;
            string supplier_name = "";
            ZhongXin mycredit = new ZhongXin();
            MemberInfo memberinfo1 = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo1 != null)
            {
                Supplier_ID1 = memberinfo1.Member_SupplierID;

            }
            if (Supplier_ID1 > 0)
            {
                SupplierInfo supplierinfo1 = GetSupplierByID(Supplier_ID1);
                if (supplierinfo1 != null)
                {
                    supplier_name = supplierinfo1.Supplier_CompanyName;
                }

                ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
                if (accountinfo != null)
                {
                    string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice, 0, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

                    if (strResult == "true")
                    {



                        new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));

                        ////添加短信提醒2015-5-14
                        //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        //if (memberinfo != null)
                        //{

                        //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
                        //}
                        ODEntity.Orders_Delivery_ReceiveStatus = 2;

                        if (Mydelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
                        {
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_delivery_list.aspx");
                        //
                    }
                    else
                    {
                        //new ZhongXin().SaveZhongXinAccountLog(ordersinfo.Orders_BuyerID, curreceiveprice * -1, "[撤销][签收结算]订单编号：" + ordersinfo.Orders_SN);

                        pub.Msg("error", "错误信息", strResult, false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "您尚未开通中信账户！", false, "{back}");
                }

            }
            else
            {
                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
            }

        }



        Response.Redirect("/supplier/index.aspx");
    }

    //订单发货单
    public void Orders_Delivery_List(int orders_id)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierInfo supplierInfo = null;
        string supplier_name = "系统";
        PageInfo pageInfo = null;
        int member_id = tools.NullInt(Session["member_id"]);
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }
        int page_size = 20;
        int i = 0;
        string page_url = "?action=list";

        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        strHTML.Append("<tbody>");
        strHTML.Append("<tr>");
        strHTML.Append("   <td height=\"30\" class=\"name\" width=\"150\" align=\"center\">发货单号</td>");
        strHTML.Append("   <td height=\"30\" class=\"name\" width=\"150\" align=\"center\">订单号</td>");
        strHTML.Append("   <td height=\"30\" class=\"name\" width=\"150\" align=\"center\">签收状态</td>");
        //strHTML.Append("   <td align=\"center\">物流公司</td>");
        //strHTML.Append("   <td width=\"150\" align=\"center\">物流单号</td>");
        strHTML.Append("   <td align=\"center\" class=\"name\">司机电话</td>");
        //strHTML.Append("   <td width=\"150\" align=\"center\">物流单号</td>");
        strHTML.Append("   <td width=\"150\" class=\"name\" align=\"center\">发货时间</td>");
        strHTML.Append("   <td width=\"150\" class=\"name\" align=\"center\">操作</td>");
        strHTML.Append("</tr>");

        string SqlField = "OD.*, O.Orders_SN, O.Orders_Type, O.Orders_BuyerID";
        string SqlTable = "Orders_Delivery AS OD INNER JOIN Orders AS O ON OD.Orders_Delivery_OrdersID = O.Orders_ID";
        string SqlParam = "";
        if (orders_id > 0)
        {
            SqlParam = "where  O.Orders_ID=" + orders_id + "  AND O.Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]);
        }
        else
        {
            SqlParam = "where   O.Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]);
        }

        string SqlOrder = "ORDER BY Orders_Delivery_ID DESC";
        string SqlCount = "SELECT COUNT(OD.Orders_Delivery_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DT = null;
        try
        {
            int PageSize = 20;
            int RecordCount = tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
            int PageCount = tools.CalculatePages(RecordCount, PageSize);
            int CurrentPage = tools.DeterminePage(current_page, PageCount);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT " + SqlField + " FROM " + SqlTable + " WHERE OD.Orders_Delivery_ID IN (");
            strSQL.Append("	SELECT Orders_Delivery_ID FROM (");
            strSQL.Append("		SELECT ROW_NUMBER() OVER(" + SqlOrder + ") AS RowNum, OD.Orders_Delivery_ID FROM " + SqlTable + " " + SqlParam);
            strSQL.Append("	) T");
            strSQL.Append("	WHERE RowNum > " + ((CurrentPage - 1) * PageSize) + " AND RowNum  <= " + (CurrentPage * PageSize));
            strSQL.Append(") " + SqlOrder);
            DT = DBHelper.Query(strSQL.ToString());

            if (DT != null)
            {
                foreach (DataRow RdrList in DT.Rows)
                {
                    i++;

                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("   <td height=\"35\" align=\"left\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + tools.NullInt(RdrList["Orders_Delivery_ID"]) + "\" target=\"_blank\">" + tools.NullStr(RdrList["Orders_Delivery_DocNo"]) + "</a></span></td>");

                    strHTML.Append("   <td align=\"center\" ><span><a class=\"a_t12_blue\" href=\"/supplier/order_detail.aspx?orders_sn=" + tools.NullStr(RdrList["Orders_SN"]) + "\">" + tools.NullStr(RdrList["Orders_SN"]) + "</a></span></td>");
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        strHTML.Append("   <td align=\"center\">未签收</td>");
                    }
                    else if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 9)
                    {
                        strHTML.Append("   <td align=\"center\">签收未确认</td>");
                    }
                    else
                    {
                        strHTML.Append("   <td align=\"center\">已签收</td>");
                    }
                    strHTML.Append("   <td align=\"center\">" + tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]) + "</td>");
                    //strHTML.Append("   <td align=\"center\">" + tools.NullStr(RdrList["Orders_Delivery_Code"]) + "</td>");
                    strHTML.Append("   <td align=\"center\">" + tools.NullDate(RdrList["Orders_Delivery_Addtime"]) + "</td>");
                    strHTML.Append("   <td height=\"35\" align=\"left\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + tools.NullInt(RdrList["Orders_Delivery_ID"]) + "\" target=\"_blank\">查看</a></span></td>");
                    strHTML.Append(" </tr>");
                }
                Response.Write(strHTML.ToString());
                strHTML.Append("</tbody>");
                Response.Write("</table>");
                pub.Page(PageCount, current_page, page_url, PageSize, RecordCount);
            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"5\" style=\"text-align:center;\">暂无发货单信息！</td>");
                strHTML.Append("</tr>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }



    public void Orders_Delivery_AcceptEdit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        int Product_ID, Goods_Amount, Goods_Type;

        int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);


        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
        }
        //OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["member_id"]));
        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["supplier_id"]));
        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx?payment=1&menu=1");
        }
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus != 2)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_delivery_list.aspx?payment=1&menu=1");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;

        //检查是否包含签收数量商品
        //bool isreceived = false;
        //IList<OrdersDeliveryGoodsInfo> deliverygoods = MyDelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);
        //if (deliverygoods != null)
        //{
        //    foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
        //    {
        //        int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
        //        int receiveprice = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
        //        if (receiveamount > 0)
        //        {
        //            isreceived = true;
        //        }
        //    }
        //}


        IList<OrdersGoodsInfo> entitys = null;
        //更新签收状态
        //ODEntity.Orders_Delivery_ReceiveStatus = 1;
        IList<OrdersDeliveryGoodsInfo> deliverygoods = Mydelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);

        if (Mydelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
        {
            //更新签收数量
            if (deliverygoods != null)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
                {
                    double buy_price = tools.CheckFloat(Request["buy_price_" + deliverygood.Orders_Delivery_Goods_ID]);
                    double receiveamount = tools.CheckFloat(Request["buy_amount_" + deliverygood.Orders_Delivery_Goods_ID]);
                    if (receiveamount >= 0)
                    {
                        deliverygood.Orders_Delivery_Goods_ProductPrice = buy_price;
                        deliverygood.Orders_Delivery_Goods_ProductAmount = receiveamount;
                        if (Mydelivery.EditOrdersDeliveryGoods(deliverygood))
                        {
                            OrdersDeliveryInfo entity = Mydelivery.GetOrdersDeliveryByID(deliverygood.Orders_Delivery_Goods_DeliveryID, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                            entity.Orders_Delivery_ReceiveStatus = 9;
                            Mydelivery.EditOrdersDelivery(entity, pub.CreateUserPrivilege(""));
                        }
                    }
                }
            }

            //Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "发货单签收", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收！");           
        }
        //QueryInfo myQuey = new  QueryInfo();
        //               myQuey.PageSize = 1;
        //        myQuey.CurrentPage = 1;
        //myQuey.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Nickname", "=", member_realname));
        //Supplier SupplierInfo = MyBLL.GetSuppliers
        IOrders mybllOrders = Glaer.Trade.B2C.BLL.ORD.OrdersFactory.CreateOrders();
        Glaer.Trade.B2C.Model.OrdersInfo myOrdersInfo = mybllOrders.GetOrdersByID(Orders_ID);
        SMS mySMS = new SMS();
        mySMS.Send(myOrdersInfo.Orders_Address_Mobile, myOrdersInfo.Orders_SN, "supplier_dilivery_orders_remind");
        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");
    }

    //查看销售发货单
    public void order_Delivery_Detail(OrdersDeliveryInfo deliveryinfo, OrdersInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();
        MemberInfo memberInfo = null;
        string memberName = "";

        if (entity != null)
        {


            strHTML.Append("<div class=\"blk06\">");
            strHTML.Append("<h2><span>");


            strHTML.Append("</span>发货单信息</h2>");

            strHTML.Append("<div class=\"b06_main_sz\">");
            strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
            strHTML.Append("<p><span>收货人：</span>" + entity.Orders_Address_Name + "</p>");
            strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
            strHTML.Append("<p><span>配送方式：</span>" + entity.Orders_Delivery_Name + "</p>");
            if (deliveryinfo.Orders_Delivery_ReceiveStatus == 0)
            {
                strHTML.Append("<p><span>签收状态：</span>未签收</p>");
            }
            else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 1)
            {
                strHTML.Append("<p><span>签收状态：</span>已签收</p>");
            }
            else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 9)
            {
                strHTML.Append("<p><span>签收状态：</span>签收未确认</p>");
            }
            else
            {
                strHTML.Append("<p><span>签收状态：</span>已结算</p>");
            }
            strHTML.Append("<p><span>物流公司：</span>" + deliveryinfo.Orders_Delivery_companyName + "</p>");
            strHTML.Append("<p><span>物流单号：</span>" + deliveryinfo.Orders_Delivery_DocNo + "</p>");

            strHTML.Append("<p><span>司机电话：</span>" + deliveryinfo.Orders_Delivery_DriverMobile + "</p>");
            strHTML.Append("<p><span>车牌号码：</span>" + deliveryinfo.Orders_Delivery_PlateNum + "</p>");
            strHTML.Append("<p><span>运输方式：</span>" + deliveryinfo.Orders_Delivery_TransportType + "</p>");


            strHTML.Append("<p><span>物流备注：</span>" + deliveryinfo.Orders_Delivery_Note + "</p>");
            strHTML.Append("<p><span>发货时间：</span>" + deliveryinfo.Orders_Delivery_Addtime + "</p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");



            //商品清单
            strHTML.Append(Order_Delivery_Detail_Goods(entity.Orders_Status, deliveryinfo));


            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }
    //收货单商品清单
    public string Order_Delivery_Detail_Goods(int Orders_Status, OrdersDeliveryInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>商品发货单清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");

        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 142px;\">单价</li>");
        strHTML.Append("<li style=\"width: 142px;\">发货数量</li>");
        strHTML.Append("<li style=\"width: 142px;\">签收数量</li>");
        strHTML.Append("<li style=\"width: 142px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<form name=\"formadd\" id=\"formadd\" method=\"post\" action=\"/supplier/orders_do.aspx\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersDeliveryGoodsInfo> entitys = Mydelivery.GetOrdersDeliveryGoods(entity.Orders_Delivery_ID);
        if (entitys != null)
        {
            foreach (OrdersDeliveryGoodsInfo goodsinfo in entitys)
            {
                string productURL = "";
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Delivery_Goods_ProductID > 0)
                {
                    productURL = pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Delivery_Goods_ProductID.ToString());

                    strHTML.Append(" <td width=\"398\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\"  target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Delivery_Goods_ProductImg, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd>");


                    strHTML.Append(" <a href=\"" + productURL + "\"  target=\"_blank\"><p><strong>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</strong></p></a>");
                    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Delivery_Goods_ProductCode + "</span></p>");
                    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Delivery_Goods_ProductID) + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    strHTML.Append("<td width=\"192\">" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");
                    strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");
                    if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0)
                    {
                        strHTML.Append("<td width=\"142\"><input class=\"received_input\" type=\"text\" name=\"" + goodsinfo.Orders_Delivery_Goods_ID + "_ReceivedAmount\" value=\"" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount) + "\" onblur=\"$('#price_" + goodsinfo.Orders_Delivery_Goods_ID + "').html($(this).val()*" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + ")\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" /></td>");


                        strHTML.Append("<td width=\"142\" >￥<span id=\"price_" + goodsinfo.Orders_Delivery_Goods_ID + "\">" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</span></td>");

                    }
                    else
                    {
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");
                        strHTML.Append("<td width=\"142\" >￥" + (goodsinfo.Orders_Delivery_Goods_ProductAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");

                    }
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");

        //保存签收
        if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0)
        {
            //strHTML.Append("<div style=\"padding:10px;\">");
            strHTML.Append("<div style=\"display: inline; float: left; padding: 10px 0px 10px 0px;\">");
            strHTML.Append("<input type=\"hidden\" value=\"orderaccept\" name=\"action\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
            strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">申请结算</a>");
            strHTML.Append("</div>");
        }

        //商家修改发货单状态条件(申请扣款（改单价）)  订单确认  发货状态: 已发货 签收状态:只要非签收结算都能修改价格
        if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && ((entity.Orders_Delivery_ReceiveStatus == 1) || (entity.Orders_Delivery_ReceiveStatus == 9)))
        {
            strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px 20px;    \">");
            strHTML.Append("<a href=\"/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + entity.Orders_Delivery_ID + " \"  class=\"a11\"  style=\"  width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">修改发货单</a>");
            strHTML.Append("</div>");
        }


        strHTML.Append("</div>");
        strHTML.Append("</div>");
        //Response.Redirect("/supplier/order_list.aspx");
        return strHTML.ToString();
    }




    //查看销售发货单
    public void order_Delivery_Detail(OrdersDeliveryInfo deliveryinfo, OrdersInfo entity, bool IsEdit)
    {
        StringBuilder strHTML = new StringBuilder();
        //MemberInfo memberInfo = null;
        //string memberName = "";
        string strOrdersResponsible = string.Empty;//初始化运输责任
        strOrdersResponsible = entity.Orders_Responsible == 1 ? "卖家责任" : "买家责任";
        if (entity != null)
        {
            //商品清单
            if (IsEdit)
            {
                strHTML.Append(Order_Delivery_Detail_GoodsEdit(entity.Orders_Status, deliveryinfo));
            }
            else
            {
                strHTML.Append("<div class=\"blk06\">");
                strHTML.Append("<h2><span>");


                //if (entity.Orders_Status == 1 && entity.Orders_PaymentStatus == 4 && deliveryinfo.Orders_Delivery_ReceiveStatus == 1)
                //{
                //    strHTML.Append("<a href=\"/member/orders_do.aspx?action=orderacceptsettle&delivery_id=" + deliveryinfo.Orders_Delivery_ID + "\" class=\"a02\">确认签收</a>");
                //}

                strHTML.Append("</span>收货单信息</h2>");

                strHTML.Append("<div class=\"b06_main_sz\">");
                strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
                strHTML.Append("<p><span>收 货 人：</span>" + entity.Orders_Address_Name + "</p>");
                strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
                strHTML.Append("<p><span>运送责任：</span>" + strOrdersResponsible + "</p>");
                if (deliveryinfo.Orders_Delivery_ReceiveStatus == 0)
                {
                    strHTML.Append("<p><span>签收状态：</span>未签收</p>");
                }
                else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 1)
                {
                    strHTML.Append("<p><span>签收状态：</span>已签收</p>");
                }
                else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 9)
                {
                    strHTML.Append("<p><span>签收状态：</span>签收未确认</p>");
                }
                else
                {
                    strHTML.Append("<p><span>签收状态：</span>已结算</p>");
                }
                strHTML.Append("<p><span>物流公司：</span>" + deliveryinfo.Orders_Delivery_companyName + "</p>");
                strHTML.Append("<p><span>物流单号：</span>" + deliveryinfo.Orders_Delivery_DocNo + "</p>");

                strHTML.Append("<p><span>司机电话：</span>" + deliveryinfo.Orders_Delivery_DriverMobile + "</p>");
                strHTML.Append("<p><span>车牌号码：</span>" + deliveryinfo.Orders_Delivery_PlateNum + "</p>");
                strHTML.Append("<p><span>运输方式：</span>" + deliveryinfo.Orders_Delivery_Name + "</p>");


                strHTML.Append("<p><span>物流备注：</span>" + deliveryinfo.Orders_Delivery_Note + "</p>");
                strHTML.Append("<p><span>发货时间：</span>" + deliveryinfo.Orders_Delivery_Addtime + "</p>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append(Order_Delivery_Detail_Goods(entity.Orders_Status, deliveryinfo));
            }



            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }


    public string Order_Delivery_Detail_GoodsEdit(int Orders_Status, OrdersDeliveryInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>收货单商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");

        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 142px;\">发货数量</li>");
        strHTML.Append("<li style=\"width: 142px;\">单价</li>");

        strHTML.Append("<li style=\"width: 142px;\">签收数量</li>");
        strHTML.Append("<li style=\"width: 142px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");

        strHTML.Append("<form name=\"formadd\" id=\"formadd\" method=\"post\" action=\"/supplier/orders_do.aspx\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersDeliveryGoodsInfo> entitys = Mydelivery.GetOrdersDeliveryGoods(entity.Orders_Delivery_ID);
        if (entitys != null)
        {
            int i = 0;
            foreach (OrdersDeliveryGoodsInfo goodsinfo in entitys)
            {
                i++;
                string productURL = "";
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Delivery_Goods_ProductID > 0)
                {

                    productURL = pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Delivery_Goods_ProductID.ToString());

                    strHTML.Append(" <td width=\"448\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\"  target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Delivery_Goods_ProductImg, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd>");

                    //strHTML.Append("<a href=""><p>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</p></a>");
                    strHTML.Append(" <a href=\"" + productURL + "\"  target=\"_blank\"><p><strong>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</strong></p></a>");
                    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Delivery_Goods_ProductCode + "</span></p>");
                    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Delivery_Goods_ProductID) + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    //strHTML.Append("<td width=\"192\">" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");
                    strHTML.Append("<td width=\"242\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");

                    if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus != 2)
                    {


                        strHTML.Append("<td width=\"191\" id=\"Orders_Goods_Product_Price\" value=\"" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ID) + "\"> <input onblur=\"sum('buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "',this.id,'Orders_Goods_EveryProduct_Sum" + i + "'," + entitys.Count + ")\" name=\"buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "\" id=\"buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Delivery_Goods_ProductPrice + "\" /></td>");

                        strHTML.Append("<td width=\"194\" id=\"Orders_Goods_Amount\"><input name=\"buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "\" onblur=\"sum(this.id,'buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "','Orders_Goods_EveryProduct_Sum" + i + "'," + entitys.Count + ")\" id=\"buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "\"                       /></td>");



                        strHTML.Append("<td width=\"190\" id=\"Orders_Goods_EveryProduct_Sum" + i + "\">");
                        strHTML.Append(" " + goodsinfo.Orders_Delivery_Goods_ProductPrice * goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "");
                        strHTML.Append("  </td>");
                    }
                    else
                    {
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductPrice + "</td>");
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "</td>");
                        strHTML.Append("<td width=\"142\" >￥" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");

                    }
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        ////只要发货单是未签收确认之前都可以随便修改
        if ((Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && ((entity.Orders_Delivery_ReceiveStatus == 9) || (entity.Orders_Delivery_ReceiveStatus == 0))) || (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && ((entity.Orders_Delivery_ReceiveStatus == 9) || (entity.Orders_Delivery_ReceiveStatus == 1))))
        {
            strHTML.Append("<div style=\"padding:10px;\">");
            strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px;    width: 20px;\">");
            strHTML.Append("<input type=\"hidden\" value=\"orderacceptedit\" name=\"action\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_OrdersID + "\" name=\"Orders_ID\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
            strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\"></a>");
            strHTML.Append("</div>");
        }
        ////保存签收
        //if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0)
        //{
        //    //strHTML.Append("<div style=\"padding:10px;\">");
        //    strHTML.Append("<div style=\"display: inline; float: left; padding: 10px 0px 10px 0px;\">");
        //    strHTML.Append("<input type=\"hidden\" value=\"orderaccept\" name=\"action\">");
        //    strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
        //    strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">申请结算</a>");
        //    strHTML.Append("</div>");
        //}

        //商家修改发货单状态条件(申请扣款（改单价）)  订单确认  发货状态: 已发货 签收状态:只要非签收结算都能修改价格
        //if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && ((entity.Orders_Delivery_ReceiveStatus == 1) || (entity.Orders_Delivery_ReceiveStatus == 9)))
        //{
        //    //strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px 20px;    \">");
        //    //strHTML.Append("<a href=\"/supplier/order_delivery_Edit.aspx?Orders_Delivery_ID=" + entity.Orders_Delivery_ID + " \"  class=\"a11\"  style=\"  width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">修改发货单</a>");
        //    //strHTML.Append("</div>");

        //    strHTML.Append("<div style=\"padding:10px;\">");
        //    strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px;    width: 20px;\">");
        //    strHTML.Append("<input type=\"hidden\" value=\"orderacceptedit\" name=\"action\">");
        //    strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_OrdersID + "\" name=\"Orders_ID\">");
        //    strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
        //    strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">修改发货单</a>");
        //    strHTML.Append("</div>");
        //}



        strHTML.Append("</form>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }

    public OrdersDeliveryInfo GetOrdersDeliveryByID(int id)
    {

        return Mydelivery.GetOrdersDeliveryByID(id, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

    }

    public string orders_delivery_goods_select(int orders_id)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table class=\"table_list\" width=\"100%\">");
        strHTML.Append("	<tr>");
        strHTML.Append("		<th align=\"center\"><input type=\"checkbox\" onclick=\"frmcheckall('goods_id', this)\" style=\"background:none;\" /></th>");
        //strHTML.Append("		<th align=\"center\" width=\"100\"><b>货号</b></th>");
        strHTML.Append("		<th align=\"center\"><b>商品名称</b></th>");
        strHTML.Append("		<th align=\"center\" width=\"100\"><b>单价</b></th>");
        strHTML.Append("		<th align=\"center\" width=\"80\"><b>订货数</b></th>");
        strHTML.Append("		<th align=\"center\" width=\"80\"><b>已发货数量</b></th>");
        strHTML.Append("		<th align=\"center\" width=\"100\"><b>本次发货数量</b></th>");
        strHTML.Append("	</tr>");

        IList<OrdersGoodsInfo> GoodsListSub = null;
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(orders_id);
        IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                strHTML.Append("	<tr>");
                strHTML.Append("		<td style=\"align:center;padding: 10px 0 10px 10px;\"><span><input type=\"checkbox\" name=\"goods_id\" value=\"" + entity.Orders_Goods_ID + "\" /></span></td>");
                //strHTML.Append("		<td>" + entity.Orders_Goods_Product_Code + "</td>");

                switch (entity.Orders_Goods_Type)
                {
                    case 1:
                        strHTML.Append("<td class=\"tit\" style=\"text-align:center\">[赠品] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "");


                        if (entity.Orders_Goods_Product_Spec.Length > 0)
                        {
                            strHTML.Append(" [" + entity.Orders_Goods_Product_Spec + "]");
                        }




                        strHTML.Append("   </a></td>");
                        break;
                    case 2:
                        strHTML.Append("<td class=\"tit\" style=\"text-align:center\">[套装] " + entity.Orders_Goods_Product_Name + "</a></td>");
                        break;
                    default:
                        strHTML.Append("<td class=\"tit\" style=\"text-align:center\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "");
                        if (entity.Orders_Goods_Product_Spec.Length > 0)
                        {
                            strHTML.Append("    [" + entity.Orders_Goods_Product_Spec + "]");
                        }
                        strHTML.Append("</a></td>");
                        break;
                }
                //发货数量
                int deliveryamount = tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=1 and Orders_Delivery_OrdersID=" + orders_id + ")"));
                //退货数量
                deliveryamount -= tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=5 and Orders_Delivery_OrdersID=" + orders_id + ")"));
                strHTML.Append("		<td style=\"text-align:center\"><span>" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</span></td>");
                strHTML.Append("		<td style=\"text-align:center\"><span>" + entity.Orders_Goods_Amount + "</span></td>");
                strHTML.Append("		<td style=\"text-align:center\"><span>" + deliveryamount + "</span></td>");
                strHTML.Append("		<td style=\"text-align:center\"><input class=\"received_input\" onchange=\"checkInputNum(this, -" + entity.Orders_Goods_Amount + "," + (entity.Orders_Goods_Amount) + ")\" type=\"text\" name=\"" + entity.Orders_Goods_ID + "_ProductAmount\" value=\"" + (entity.Orders_Goods_Amount) + "\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" /></td>");
                strHTML.Append("	</tr>");

                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, entity.Orders_Goods_ID);
                foreach (OrdersGoodsInfo e in GoodsListSub)
                {
                    strHTML.Append("	<tr>");
                    strHTML.Append("		<td></td>");
                    strHTML.Append("		<td>" + e.Orders_Goods_Product_Code + "</td>");
                    strHTML.Append("<td class=\"tit\" align=\"left\">[套装] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, e.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + e.Orders_Goods_Product_Name + "</a></td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>" + (e.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "×" + entity.Orders_Goods_Amount + "</td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("	</tr>");
                }
                GoodsListSub.Clear();
                GoodsListSub = null;

            }
        }
        strHTML.Append("</table>");

        return strHTML.ToString();
    }

    //订单发货
    public void Orders_Delivery(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Orders_Delivery_companyName = tools.CheckStr(Request["Orders_Delivery_companyName"]);
        string Orders_Delivery_Code = tools.CheckStr(Request["Orders_Delivery_Code"]);
        string Orders_Delivery_Note = tools.CheckStr(Request["Orders_Delivery_Note"]);
        string freightverify = tools.CheckStr(Request["freightverify"]);


        string Orders_Delivery_DriverMobile = tools.CheckStr(Request["Orders_Delivery_DriverMobile"]);
        string Orders_Delivery_PlateNum = tools.CheckStr(Request["Orders_Delivery_PlateNum"]);
        string Orders_Delivery_TransportType = tools.CheckStr(Request["Orders_Delivery_TransportType"]);

        if (Orders_Delivery_TransportType == "0")
        {
            pub.Msg("info", "信息提示", "请选择运输方式！", false, "{back}");
        }


        int Orders_Delivery_DeliveryStatus = 0;
        int Orders_Buyerid = 0;
        string Orders_SN = "";
        int Orders_Delivery_ID;
        string Orders_Delivery_DocNo = orders_delivery_sn();
        string freightreason = "";
        string member_email = "";


        if (Orders_Delivery_DriverMobile.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入正确的手机号码！", false, "{back}");
        }


        if (freightverify != tools.NullStr(Session["freightverify"]))
        {
            pub.Msg("info", "信息提示", "重复的发货请求！", false, "{back}");
        }
        if (operate == "create")
        {
            Orders_Delivery_DeliveryStatus = 1;
        }
        else
        {
            Orders_Delivery_DeliveryStatus = 5;
        }
        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, tools.NullInt(Session["supplier_id"]));
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4)
            {

                #region 处理发货明细

                double ProductAmount = 0;

                List<OrdersDeliveryGoodsInfo> ODGList = new List<OrdersDeliveryGoodsInfo>();

                string goods_id = tools.CheckStr(Request.Form["goods_id"]);
                if (goods_id.Length > 0)
                {
                    QueryInfo Query = new QueryInfo();
                    Query.CurrentPage = 1;
                    Query.PageSize = 0;
                    Query.OrderInfos.Add(new OrderInfo("OrdersGoodsInfo.Orders_Goods_ID", "ASC"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_OrdersID", "=", Orders_ID.ToString()));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_ID", "IN", goods_id));
                    //Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersGoodsInfo.Orders_Goods_ParentID", "IN", goods_id));
                    IList<OrdersGoodsInfo> entitys = MyOrders.GetOrdersGoodsList(Query);
                    if (entitys != null)
                    {
                        foreach (OrdersGoodsInfo entity in entitys)
                        {
                            ProductAmount = 0;
                            if ((entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0) || entity.Orders_Goods_Type != 2)
                            {
                                ProductAmount = tools.CheckFloat(Request.Form[entity.Orders_Goods_ID + "_ProductAmount"]);
                            }

                            if (ProductAmount <= 0)
                            {
                                ODGList = null;

                                pub.Msg("info", "信息提示", entity.Orders_Goods_Product_Name + "未填写发货数量", false, "{back}");
                            }
                            int deliveryamount = tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=1 and Orders_Delivery_OrdersID=" + ordersinfo.Orders_ID + ")"));
                            deliveryamount -= tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=5 and Orders_Delivery_OrdersID=" + ordersinfo.Orders_ID + ")"));
                            if ((entity.Orders_Goods_Amount) < ProductAmount)
                            {
                                ODGList = null;

                                pub.Msg("info", "信息提示", entity.Orders_Goods_Product_Name + "的发货数量最大为" + (entity.Orders_Goods_Amount - deliveryamount), false, "{back}");
                            }

                            ODGList.Add(new OrdersDeliveryGoodsInfo()
                            {
                                Orders_Delivery_Goods_GoodsID = entity.Orders_Goods_ID,
                                Orders_Delivery_Goods_ProductCateID = entity.Orders_Goods_Product_CateID,
                                Orders_Delivery_Goods_ProductID = entity.Orders_Goods_Product_ID,
                                Orders_Delivery_Goods_ProductCode = entity.Orders_Goods_Product_Code,
                                Orders_Delivery_Goods_ProductName = entity.Orders_Goods_Product_Name,
                                Orders_Delivery_Goods_ProductImg = entity.Orders_Goods_Product_Img,
                                Orders_Delivery_Goods_ProductSpec = entity.Orders_Goods_Product_Spec,
                                Orders_Delivery_Goods_ProductPrice = entity.Orders_Goods_Product_Price,

                                Orders_Delivery_Goods_ProductAmount = ProductAmount,
                                Orders_Delivery_Goods_ReceivedAmount = 0,
                                Orders_Delivery_Goods_brokerage = entity.Orders_Goods_Product_brokerage,
                                Orders_Delivery_Goods_DeliveryID = 0
                            });
                        }
                    }
                }

                if (ODGList.Count == 0)
                {
                    pub.Msg("info", "信息提示", "请选择要发货的商品", false, "{back}");
                }
                //if (freightverify != tools.NullStr(Session["freightverify"]))
                //{
                //    pub.Msg("info", "信息提示", "重复的发货请求！", false, "{back}");
                //}

                #endregion

                Orders_SN = ordersinfo.Orders_SN;
                Orders_Buyerid = ordersinfo.Orders_BuyerID;
                if (CheckSelectProductStockEnough(Orders_ID) == false && Orders_Delivery_DeliveryStatus == 1)
                {
                    pub.Msg("info", "信息提示", "订单产品库存不足", false, "{back}");
                }
                //if (freightverify != tools.NullStr(Session["freightverify"]))
                //{
                //    pub.Msg("info", "信息提示", "重复的发货请求！", false, "{back}");
                //}
                Session["freightverify"] = pub.Createvkey(6);

                OrdersDeliveryInfo ordersdelivery = new OrdersDeliveryInfo();
                ordersdelivery.Orders_Delivery_ID = 0;
                ordersdelivery.Orders_Delivery_DeliveryStatus = Orders_Delivery_DeliveryStatus;
                ordersdelivery.Orders_Delivery_SysUserID = 0;// tools.NullInt(Session["User_ID"]);
                ordersdelivery.Orders_Delivery_OrdersID = Orders_ID;
                ordersdelivery.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
                ordersdelivery.Orders_Delivery_Name = ordersinfo.Orders_Delivery_Name;
                ordersdelivery.Orders_Delivery_companyName = Orders_Delivery_companyName;
                ordersdelivery.Orders_Delivery_Code = Orders_Delivery_Code;
                ordersdelivery.Orders_Delivery_Amount = 0;
                ordersdelivery.Orders_Delivery_Note = Orders_Delivery_Note;
                ordersdelivery.Orders_Delivery_Addtime = DateTime.Now;
                ordersdelivery.Orders_Delivery_Site = pub.GetCurrentSite();

                ordersdelivery.Orders_Delivery_DriverMobile = Orders_Delivery_DriverMobile;
                ordersdelivery.Orders_Delivery_PlateNum = Orders_Delivery_PlateNum;
                ordersdelivery.Orders_Delivery_TransportType = Orders_Delivery_TransportType;



                if (Mydelivery.AddOrdersDelivery(ordersdelivery, pub.CreateUserPrivilege("800fdc63-fa5d-44de-927e-8d4560c2f238")))
                {
                    ordersdelivery = Mydelivery.GetOrdersDeliveryBySn(Orders_Delivery_DocNo, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

                    if (ordersdelivery != null)
                    {
                        if (ODGList != null)
                        {
                            foreach (OrdersDeliveryGoodsInfo ODG in ODGList)
                            {
                                ODG.Orders_Delivery_Goods_DeliveryID = ordersdelivery.Orders_Delivery_ID;
                            }
                            Mydelivery.AddOrdersDeliveryGoods(ODGList);

                        }
                        Orders_Delivery_ID = ordersdelivery.Orders_Delivery_ID;
                        if (operate == "create")
                        {
                            //减少可用库存
                            DeliveryProductCountAction(ODGList, Orders_ID, "del");
                            //实际库存扣除
                            DeliveryProductStockAction(ODGList, Orders_ID, "del");

                            freightreason = "订单发货，发货单号{order_freight_sn}";
                            freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + Orders_Delivery_ID + "\">" + Orders_Delivery_DocNo + "</a>");
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货", "成功", freightreason);
                        }
                        else
                        {
                            //减少可用库存
                            DeliveryProductCountAction(ODGList, Orders_ID, "add");
                            //实际库存退回
                            DeliveryProductStockAction(ODGList, Orders_ID, "add");
                            freightreason = "订单退货，退货单号{order_freight_sn}";
                            freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + Orders_Delivery_ID + "\">" + Orders_Delivery_DocNo + "</a>");
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "退货", "成功", freightreason);
                        }
                        //全部发货
                        if (CheckIsALLFreight(ordersinfo.Orders_ID))
                        {
                            ordersinfo.Orders_DeliveryStatus = 1;
                            ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                            MyOrders.EditOrders(ordersinfo);
                        }
                        MemberInfo memberinfo = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        if (memberinfo != null)
                        {
                            member_email = memberinfo.Member_Email;
                        }
                        if (operate == "create")
                        {
                            //发送订单邮件
                            string mailsubject, mailbodytitle, mailbody;
                            mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单已发货";
                            mailbodytitle = mailsubject;
                            mailbody = mail_template("order_freight", "", ordersinfo.Orders_SN);
                            Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                        }

                        // 发货短信推送
                        SMS mySMS = new SMS();

                        mySMS.Send(ordersinfo.Orders_Address_Mobile, ordersinfo.Orders_SN, "supplier_dilivery_orders_remind");
                        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
                    }
                }
            }
        }
        Response.Redirect("/supplier/index.aspx");
    }

    public bool CheckIsALLFreight(int Orders_ID)
    {
        bool result = true;
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(Orders_ID);
        IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in GoodsList)
            {

                //发货数量
                int deliveryamount = tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=1 and Orders_Delivery_OrdersID=" + Orders_ID + ")"));
                //退货数量
                deliveryamount -= tools.NullInt(DBHelper.ExecuteScalar("SELECT SUM(Orders_Delivery_Goods_ProductAmount) FROM Orders_Delivery_Goods WHERE Orders_Delivery_Goods_GoodsID = " + entity.Orders_Goods_ID + " and Orders_Delivery_Goods_DeliveryID in (select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_DeliveryStatus=5 and Orders_Delivery_OrdersID=" + Orders_ID + ")"));
                if (deliveryamount < entity.Orders_Goods_Amount)
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 生成随行单编号
    /// </summary>
    /// <returns></returns>
    public string orders_accomopanying_sn()
    {
        string sn = "";
        bool ismatch = true;
        OrdersAccompanyingInfo accompanyinginfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(7);
        while (ismatch == true)
        {
            accompanyinginfo = MyAccompanying.GetOrdersAccompanyingBySN(sn);

            if (accompanyinginfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(7);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    /// <summary>
    /// 添加随行单
    /// </summary>
    public void Orders_AccompanyingAdd()
    {
        int Accompanying_ID = tools.CheckInt(Request["Accompanying_ID"]);
        int Accompanying_OrdersID = tools.CheckInt(Request["Accompanying_OrdersID"]);
        int Accompanying_DeliveryID = tools.CheckInt(Request["Accompanying_DeliveryID"]);
        string Accompanying_SN = orders_accomopanying_sn();
        string Accompanying_Name = tools.CheckStr(Request["Accompanying_Name"]);
        double Accompanying_Amount = tools.CheckFloat(Request["Accompanying_Amount"]);
        string Accompanying_Unit = tools.CheckStr(Request["Accompanying_Unit"]);
        double Accompanying_Price = tools.CheckFloat(Request["Accompanying_Price"]);
        int Accompanying_Status = tools.CheckInt(Request["Accompanying_Status"]);
        DateTime Accompanying_Addtime = DateTime.Now;
        string Accompanying_Site = tools.CheckStr(Request["Accompanying_Site"]);
        double Orders_TotalPrice = tools.CheckFloat(Request["Orders_TotalPrice"]);
        string Orders_AccompanyingImg = tools.CheckStr(Request["Orders_AccompanyingImg"]);
        string Orders_AccompanyingImg_1 = tools.CheckStr(Request["Orders_AccompanyingImg_1"]);
        string Orders_AccompanyingImg_2 = tools.CheckStr(Request["Orders_AccompanyingImg_2"]);
        string Orders_AccompanyingImg_3 = tools.CheckStr(Request["Orders_AccompanyingImg_3"]);
        string Orders_AccompanyingImg_4 = tools.CheckStr(Request["Orders_AccompanyingImg_4"]);

        string ImgPath = Orders_AccompanyingImg + "|" + Orders_AccompanyingImg_1 + "|" + Orders_AccompanyingImg_2 + "|" + Orders_AccompanyingImg_3 + "|" + Orders_AccompanyingImg_4;
        string[] Images = ImgPath.Split('|');

        string Orders_Delivery_DocNo = orders_delivery_sn();
        int Orders_Delivery_ID;
        string freightreason = "";
        string member_email = "";
        string member_mobile = "";
        int Orders_Delivery_DeliveryStatus = 0;

        if (Accompanying_Name == "")
        {
            Response.Write("请输入随行单品类");
            Response.End();
        }

        if (Accompanying_Amount == 0)
        {
            Response.Write("请输入随行单重量或件数");
            Response.End();
        }

        if (Accompanying_Price == 0)
        {
            Response.Write("请输入随行单价格");
            Response.End();
        }

        OrdersAccompanyingInfo entity = new OrdersAccompanyingInfo();
        entity.Accompanying_ID = Accompanying_ID;
        entity.Accompanying_OrdersID = Accompanying_OrdersID;
        entity.Accompanying_DeliveryID = Accompanying_DeliveryID;
        entity.Accompanying_SN = Accompanying_SN;
        entity.Accompanying_Name = Accompanying_Name;
        entity.Accompanying_Amount = Accompanying_Amount;
        entity.Accompanying_Unit = Accompanying_Unit;
        entity.Accompanying_Price = Accompanying_Price;
        entity.Accompanying_Status = Accompanying_Status;
        entity.Accompanying_Addtime = Accompanying_Addtime;
        entity.Accompanying_Site = Accompanying_Site;

        if (MyAccompanying.AddOrdersAccompanying(entity, Images))
        {
            //Orders_Accompanying_Sync(entity);

            OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Accompanying_OrdersID, tools.NullInt(Session["supplier_id"]));
            if (GetOrdersAccompanyingPrice(entity.Accompanying_OrdersID) == Orders_TotalPrice)
            {
                OrdersDeliveryInfo ordersdelivery = new OrdersDeliveryInfo();
                ordersdelivery.Orders_Delivery_ID = 0;
                ordersdelivery.Orders_Delivery_DeliveryStatus = 0;
                ordersdelivery.Orders_Delivery_SysUserID = 0;
                ordersdelivery.Orders_Delivery_OrdersID = Accompanying_OrdersID;
                ordersdelivery.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
                ordersdelivery.Orders_Delivery_Name = "";
                ordersdelivery.Orders_Delivery_companyName = "";
                ordersdelivery.Orders_Delivery_Code = "";
                ordersdelivery.Orders_Delivery_Status = 0;
                ordersdelivery.Orders_Delivery_Amount = 0;
                ordersdelivery.Orders_Delivery_Note = "";
                ordersdelivery.Orders_Delivery_Addtime = DateTime.Now;
                ordersdelivery.Orders_Delivery_Site = pub.GetCurrentSite();

                if (Mydelivery.AddOrdersDelivery(ordersdelivery, pub.CreateUserPrivilege("800fdc63-fa5d-44de-927e-8d4560c2f238")))
                {
                    ordersdelivery = Mydelivery.GetOrdersDeliveryBySn(Orders_Delivery_DocNo, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                    if (ordersdelivery != null)
                    {
                        Orders_Delivery_ID = ordersdelivery.Orders_Delivery_ID;

                        //减少可用库存
                        ProductCountAction(Accompanying_OrdersID, "del");
                        //实际库存扣除
                        ProductStockAction(Accompanying_OrdersID, "del");

                        freightreason = "订单已发货";
                        //freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + Orders_Delivery_ID + "\">" + Orders_Delivery_DocNo + "</a>");
                        Myorder.Orders_Log(Accompanying_OrdersID, tools.NullStr(Session["supplier_email"]), "发货", "成功", freightreason);

                        ordersinfo.Orders_DeliveryStatus = 1;
                        ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                        MyOrders.EditOrders(ordersinfo);
                        MemberInfo memberinfo = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        if (memberinfo != null)
                        {
                            member_email = memberinfo.Member_Email;
                            member_mobile = memberinfo.Member_LoginMobile;
                        }

                        //发送订单邮件
                        string mailsubject, mailbodytitle, mailbody;
                        mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单已发货";
                        mailbodytitle = mailsubject;
                        mailbody = mail_template("order_freight", "", ordersinfo.Orders_SN);
                        Sendmail(member_email, mailsubject, mailbodytitle, mailbody);

                        //发送短信
                        //new SMS().Send(tools.NullStr(member_mobile), "supplier_dilivery_orders_remind", ordersinfo.Orders_SN);
                        new SMS().Send(tools.NullStr(member_mobile), ordersinfo.Orders_SN.ToString());

                        Response.Write("delivery_success");
                        Response.End();
                    }
                }
            }
            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("随行单添加失败，请稍后再试......");
            Response.End();
        }
    }

    /// <summary>
    /// 随行单列表
    /// </summary>
    public void Orders_AccompanyingList()
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"  class=\"table02\">");
        strHTML.Append("<tr>");
        strHTML.Append("  <td width=\"100\"  class=\"name\">品类</td>");
        strHTML.Append("  <td width=\"150\"  class=\"name\">重量或件数</td>");
        strHTML.Append("  <td width=\"150\"  class=\"name\">金额</td>");
        strHTML.Append("  <td  class=\"name\">图片</td>");
        strHTML.Append("  <td  class=\"name\">操作</td>");
        strHTML.Append("</tr>");

        IList<OrdersAccompanyingInfo> entitys = (IList<OrdersAccompanyingInfo>)Session["Orders_Accompanying"];
        if (entitys != null)
        {
            int icount = 0;
            foreach (OrdersAccompanyingInfo entity in entitys)
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td >" + entity.Accompanying_Name + "</td>");
                strHTML.Append("<td >" + entity.Accompanying_Amount + "</td>");
                strHTML.Append("<td >" + entity.Accompanying_Price + "</td>");
                strHTML.Append("<td >");
                strHTML.Append("</td>");
                strHTML.Append("<td ><a href=\"javascript:;\" class=\"del-icon\" onclick=\"del_productpricing('" + icount + "')\">删除</a></td>");
                strHTML.Append("</tr>");

                icount++;
            }
        }
        else
        {
            strHTML.Append("<tr >");
            strHTML.Append("<td align=\"center\"  valign=\"middle\" colspan=\"5\">暂无数据</td>");
            strHTML.Append("</tr>");
        }
        strHTML.Append("</table>");
        Response.Write(strHTML.ToString());
    }

    public string[] GetAccompanyingImgs(int ID)
    {
        string[] img_arr;
        string imgstr = "";

        IList<OrdersAccompanyingImgInfo> entitys = MyAccompanying.GetOrdersAccompanyingImgsByAccompanyID(ID);
        if (entitys != null)
        {
            foreach (OrdersAccompanyingImgInfo entity in entitys)
            {
                if (entity.Img_Path != "" && entity.Img_Path != "/images/detail_no_pic.gif")
                {
                    imgstr += "," + Application["upload_server_url"] + entity.Img_Path;
                }
            }
        }
        img_arr = imgstr.TrimStart(',').Split(',');
        return img_arr;
    }

    public string GetAccompanyingImg(int ID)
    {
        string imgstr = "";

        IList<OrdersAccompanyingImgInfo> entitys = MyAccompanying.GetOrdersAccompanyingImgsByAccompanyID(ID);
        if (entitys != null)
        {
            foreach (OrdersAccompanyingImgInfo entity in entitys)
            {
                if (entity.Img_Path != "" && entity.Img_Path != "/images/detail_no_pic.gif")
                {
                    imgstr += "," + Application["upload_server_url"] + entity.Img_Path;
                }
            }
        }

        imgstr = imgstr.TrimStart(',');

        return imgstr;
    }

    public OrdersAccompanyingInfo GetAccompanyingByID(int ID)
    {
        return MyAccompanying.GetOrdersAccompanyingByID(ID);
    }

    /// <summary>
    /// 根据订单ID获取随行单价格
    /// </summary>
    /// <param name="Orders_ID"></param>
    /// <returns></returns>
    public double GetOrdersAccompanyingPrice(int Orders_ID)
    {
        double total_price = 0;
        IList<OrdersAccompanyingInfo> entitys = MyAccompanying.GetOrdersAccompanyingsByOrders(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersAccompanyingInfo entity in entitys)
            {
                total_price = total_price + entity.Accompanying_Price;
            }
        }
        return total_price;
    }

    public void Orders_Accompanying_Sync(OrdersAccompanyingInfo entity)
    {
        string url = erp_url + "/b2b/newsxd";

        string sign, sign_type = "MD5";
        SxdJsonInfo jsonInfo = null;
        SxdErrorInfo err = null;
        SxdParamInfo paramInfo = new SxdParamInfo();
        SxdGoodsInfo goodsInfo = new SxdGoodsInfo();
        paramInfo.goods = new List<SxdGoodsInfo>();

        MemberInfo memInfo = null;
        OrdersInfo ordersInfo = Myorder.GetOrdersByID(entity.Accompanying_OrdersID);
        if (ordersInfo != null)
        {
            memInfo = MyMEM.GetMemberByID(ordersInfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memInfo != null)
            {
                string goodsjson = "";
                if (entity.Accompanying_Unit == "件")
                {
                    goodsjson = "[{\"name\":\"" + entity.Accompanying_Name + "\",\"amount\":\"" + entity.Accompanying_Amount + "\",\"cost\":\"" + entity.Accompanying_Price + "\"}]";
                }
                else if (entity.Accompanying_Unit == "克")
                {
                    goodsjson = "[{\"name\":\"" + entity.Accompanying_Name + "\",\"weight\":\"" + entity.Accompanying_Amount + "\",\"cost\":\"" + entity.Accompanying_Price + "\"}]";
                }

                string pictures = JsonHelper.ObjectToJSON(GetAccompanyingImgs(entity.Accompanying_ID));

                string[] parameters = 
                {
                    "userid="+ordersInfo.Orders_BuyerID,
                    "storeid="+memInfo.Member_ERP_StoreID,
                    "orderno="+ordersInfo.Orders_SN,
                    "sxdid="+entity.Accompanying_ID,
                    "goods="+goodsjson,
                    "picture="+GetAccompanyingImg(entity.Accompanying_ID)
                };

                string[] return_parameters = pub.BubbleSort(parameters);
                StringBuilder per = new StringBuilder();
                int i = 0;
                for (i = 0; i <= return_parameters.Length - 1; i++)
                {
                    if (i == return_parameters.Length - 1)
                    {
                        per.Append(return_parameters[i]);
                    }
                    else
                    {
                        per.Append(return_parameters[i] + "&");
                    }
                }
                sign = securityUtil.signData(per.ToString(), "MD5");


                StringBuilder prestr = new StringBuilder();
                prestr.Append("{");
                prestr.Append("\"userid\":\"" + memInfo.Member_ID + "\",");
                prestr.Append("\"storeid\":\"" + memInfo.Member_ERP_StoreID + "\",");
                prestr.Append("\"orderno\":\"" + ordersInfo.Orders_SN + "\",");
                prestr.Append("\"sxdid\":\"" + entity.Accompanying_ID + "\",");
                prestr.Append("\"goods\":" + goodsjson + ",");
                prestr.Append("\"picture\":\"" + GetAccompanyingImg(entity.Accompanying_ID) + "\",");
                prestr.Append("\"sign\":\"" + sign + "\",");
                prestr.Append("\"sign_type\":\"" + sign_type + "\"");
                prestr.Append("}");



                //string prestr = JsonHelper.ObjectToJSON(paramInfo);

                CookieCollection cookies = new CookieCollection();

                string strJson = HttpHelper.GetResponseString(HttpHelper.CreatePostJsonHttpResponse(url, prestr.ToString(), 0, "", cookies));
                jsonInfo = JsonHelper.JSONToObject<SxdJsonInfo>(strJson);
                err = jsonInfo.err;

                if (jsonInfo.result == true)
                {
                    pub.AddSysInterfaceLog(5, "随行单推送", "成功", "Post参数：" + prestr.ToString() + "；待签名参数：" + per.ToString(), "订单发货并推送随行单至ERP，接口返回：" + jsonInfo.result);

                    //Response.Write("返回结果：" + jsonInfo.result + "<br/>");
                    //Response.Write("生成的签名：" + sign + "<br/>");
                    //Response.Write("待签名参数：" + per.ToString() + "<br/>");
                    //Response.Write("Post参数：" + prestr.ToString());
                    //Response.End();
                }
                else
                {
                    if (err != null)
                    {
                        pub.AddSysInterfaceLog(5, "随行单推送", "失败", "Post参数：" + prestr.ToString() + "；待签名参数：" + per.ToString(), "订单发货并推送随行单至ERP，错误信息：" + err.code + "：" + err.message);

                        //Response.Write("返回结果：" + jsonInfo.err.message + "<br/>");
                        //Response.Write("生成的签名：" + sign + "<br/>");
                        //Response.Write("待签名参数：" + per.ToString() + "<br/>");
                        //Response.Write("Post参数：" + prestr.ToString());
                        //Response.End();
                    }
                    else
                    {
                        pub.AddSysInterfaceLog(5, "随行单推送", "失败", "Post参数：" + prestr.ToString() + "；待签名参数：" + per.ToString(), "订单发货并推送随行单至ERP，错误信息：" + jsonInfo.msg);

                        //Response.Write("返回结果：" + jsonInfo.msg + "<br/>");
                        //Response.Write("生成的签名：" + sign + "<br/>");
                        //Response.Write("待签名参数：" + per.ToString() + "<br/>");
                        //Response.Write("Post参数：" + prestr.ToString());
                        //Response.End();
                    }
                }
            }
        }
    }


    //修改订单价格
    public void Update_Orders_Price()
    {
        int Orders_ID = tools.NullInt(Request["orders_id"]);
        double FreightDiscount = tools.NullDbl(Request["FreightDiscount"]);
        double PriceDiscount = tools.NullDbl(Request["PriceDiscount"]);
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        OrdersInfo ordersinfo = Myorder.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (("," + supplier_orders + ",").IndexOf("," + Orders_ID + ",") > 0)
            {
                if (ordersinfo.Orders_PaymentStatus == 0)
                {
                    string freightreason = "价格优惠由:{old_favor_price},修改为:{new_favor_price};运费优惠由:{old_favor_fee},修改为:{new_favor_fee};";
                    freightreason = freightreason.Replace("{old_favor_fee}", ordersinfo.Orders_Total_FreightDiscount.ToString());
                    freightreason = freightreason.Replace("{new_favor_fee}", FreightDiscount.ToString());
                    ordersinfo.Orders_Total_FreightDiscount = FreightDiscount;
                    freightreason = freightreason.Replace("{old_favor_price}", ordersinfo.Orders_Total_PriceDiscount.ToString());
                    freightreason = freightreason.Replace("{new_favor_price}", PriceDiscount.ToString());
                    ordersinfo.Orders_Total_PriceDiscount = PriceDiscount;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_Price + ordersinfo.Orders_Total_Freight - FreightDiscount - PriceDiscount;
                    MyOrders.EditOrders(ordersinfo);
                    Myorder.Orders_Log(Orders_ID, "商家:" + tools.NullStr(Session["supplier_companyname"]), "订单修改", "成功", freightreason);

                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
                }
                else
                {
                    pub.Msg("info", "信息提示", "该订单已支付不能修改订单价格！", true, "/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
                }
            }
        }
        Response.Redirect("/supplier/index.aspx");
    }

    //产品实际库存操作
    public void ProductStockAction(int Orders_ID, string action)
    {
        int Product_ID, Goods_Type;
        double Goods_Amount;
        string SqlAdd = "";
        int Config_DefaultSupplier = 0;
        int Config_DefaultDepot = 0;

        string SqlList = "SELECT TOP 1 * FROM SCM_Config ORDER BY Config_ID DESC";
        SqlDataReader RdrList = null;
        bool recordExist = false;
        RdrList = DBHelper.ExecuteReader(SqlList);
        if (RdrList.Read())
        {
            Config_DefaultSupplier = tools.NullInt(RdrList["Config_DefaultSupplier"]);
            Config_DefaultDepot = tools.NullInt(RdrList["Config_DefaultDepot"]);
        }
        RdrList.Close();
        RdrList = null;

        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                Product_ID = entity.Orders_Goods_Product_ID;
                Goods_Amount = entity.Orders_Goods_Amount;
                Goods_Type = entity.Orders_Goods_Type;
                switch (action)
                {
                    //退货
                    case "add":
                        if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                        {
                            ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
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
                                        MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code,(int)Math.Round( entity.Orders_Goods_Amount), 0);
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
                            ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
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
                                        MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, 0 -(int)Math.Round( entity.Orders_Goods_Amount), 0);
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
                                    MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code,(int)Math.Round( entity.Orders_Goods_Amount), 0);
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
                                    MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, 0 - (int)Math.Round(entity.Orders_Goods_Amount), 0);
                                }
                            }
                        }
                        break;
                }

            }
        }
    }

    //产品可用库存操作
    public void ProductCountAction(int Orders_ID, string action)
    {
        int Product_ID, Goods_Type;
        double Goods_Amount;

        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                Product_ID = entity.Orders_Goods_Product_ID;
                Goods_Amount = entity.Orders_Goods_Amount;
                Goods_Type = entity.Orders_Goods_Type;
                switch (action)
                {
                    case "add":
                        if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                        {
                            MyProduct.UpdateProductStockExcepNostock(Product_ID, 0, (int)Math.Round( Goods_Amount));
                        }
                        break;
                    case "del":
                        Goods_Amount = 0 - Goods_Amount;
                        if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                        {
                            MyProduct.UpdateProductStockExcepNostock(Product_ID, 0, (int)Math.Round(Goods_Amount));
                        }
                        break;
                }

            }
        }
    }

    //产品实际库存操作
    public void DeliveryProductStockAction(IList<OrdersDeliveryGoodsInfo> entitys, int Orders_ID, string action)
    {
        int Product_ID, Goods_Amount, Goods_Type;
        string SqlAdd = "";
        int Config_DefaultSupplier = 0;
        int Config_DefaultDepot = 0;

        string SqlList = "SELECT TOP 1 * FROM SCM_Config ORDER BY Config_ID DESC";
        SqlDataReader RdrList = null;
        bool recordExist = false;
        RdrList = DBHelper.ExecuteReader(SqlList);
        if (RdrList.Read())
        {
            Config_DefaultSupplier = tools.NullInt(RdrList["Config_DefaultSupplier"]);
            Config_DefaultDepot = tools.NullInt(RdrList["Config_DefaultDepot"]);
        }
        RdrList.Close();
        RdrList = null;

        IList<OrdersGoodsInfo> entitys1 = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys1 != null)
        {
            foreach (OrdersGoodsInfo entity in entitys1)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygoods in entitys)
                {
                    if (deliverygoods.Orders_Delivery_Goods_GoodsID == entity.Orders_Goods_ID || deliverygoods.Orders_Delivery_Goods_GoodsID == entity.Orders_Goods_ParentID)
                    {
                        Product_ID = entity.Orders_Goods_Product_ID;

                        Goods_Type = entity.Orders_Goods_Type;
                        if (entity.Orders_Goods_ParentID == 0)
                        {
                            Goods_Amount =(int)Math.Round( deliverygoods.Orders_Delivery_Goods_ProductAmount);
                        }
                        else
                        {
                            double parentamount = 0;
                            foreach (OrdersGoodsInfo entity1 in entitys1)
                            {
                                if (entity1.Orders_Goods_ID == entity.Orders_Goods_ParentID)
                                {
                                    parentamount = entity1.Orders_Goods_Amount;
                                    break;
                                }
                            }
                            Goods_Amount = (int)Math.Round(entity.Orders_Goods_Amount / parentamount * deliverygoods.Orders_Delivery_Goods_ProductAmount);
                        }
                    }
                    else
                    {
                        continue;
                    }
                    switch (action)
                    {
                        //退货
                        case "add":
                            if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                            {
                                ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
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
                                            MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, (int)Math.Round(entity.Orders_Goods_Amount), 0);
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
                                ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
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
                                            MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, 0 - (int)Math.Round(entity.Orders_Goods_Amount), 0);
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
                                        MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code,(int)Math.Round( entity.Orders_Goods_Amount), 0);
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
                                        MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, (int)Math.Round(0 - entity.Orders_Goods_Amount), 0);
                                    }
                                }
                            }
                            break;
                    }

                }
            }
        }
    }

    //产品可用库存操作
    public void DeliveryProductCountAction(IList<OrdersDeliveryGoodsInfo> entitys, int Orders_ID, string action)
    {
        int Product_ID, Goods_Amount, Goods_Type;

        IList<OrdersGoodsInfo> entitys1 = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys1 != null)
        {
            foreach (OrdersGoodsInfo entity in entitys1)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygoods in entitys)
                {
                    if (deliverygoods.Orders_Delivery_Goods_GoodsID == entity.Orders_Goods_ID || deliverygoods.Orders_Delivery_Goods_GoodsID == entity.Orders_Goods_ParentID)
                    {
                        Product_ID = entity.Orders_Goods_Product_ID;

                        Goods_Type = entity.Orders_Goods_Type;
                        if (entity.Orders_Goods_ParentID == 0)
                        {
                            Goods_Amount = (int)Math.Round(deliverygoods.Orders_Delivery_Goods_ProductAmount);
                        }
                        else
                        {
                            int parentamount = 0;
                            foreach (OrdersGoodsInfo entity1 in entitys1)
                            {
                                if (entity1.Orders_Goods_ID == entity.Orders_Goods_ParentID)
                                {
                                    parentamount = (int)Math.Round(entity1.Orders_Goods_Amount);
                                    break;
                                }
                            }
                            Goods_Amount = (int)Math.Round(entity.Orders_Goods_Amount / parentamount * deliverygoods.Orders_Delivery_Goods_ProductAmount);
                        }
                    }
                    else
                    {
                        continue;
                    }
                    Product_ID = entity.Orders_Goods_Product_ID;
                    Goods_Amount = (int)Math.Round(entity.Orders_Goods_Amount);
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
        }
    }

    //确认发货
    public void Confirm_Send_Goods(string orders_sn, string delivery_companyname, string delivery_no)
    {

        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);
        string gateway = "https://mapi.alipay.com/gateway.do?";
        //支付接口 
        //Dim service As String = "send_goods_confirm_by_platform"
        //服务名称，这个是识别是何接口实现何功能的标识，请勿修改 
        string sign_type = "MD5";
        //加密类型,签名方式“不用改” 
        //Dim key As String = ""
        //安全校验码，与partner是一组，获取方式是：用签约时支付宝帐号登陆支付宝网站www.alipay.com，在商家服务我的商家里即可查到。 
        //Dim partner As String = ""
        //商户ID，合作身份者ID，合作伙伴ID 
        string _input_charset = "utf-8";
        //编码类型，完全根据客户自身的项目的编码格式而定，千万不要填错。否则极其容易造成MD5加密错误。
        string trade_no = "";
        string trade_status = "";
        IList<MemberPayLogInfo> paylog = Mypayment.GetMemberPayLogByOrdersSn(orders_sn);
        if (paylog != null)
        {
            foreach (MemberPayLogInfo entity in paylog)
            {
                if (entity.Member_Pay_Log_PaywaySign == "支付宝" && entity.Member_Pay_Log_IsSuccess == 1)
                {
                    trade_no = entity.Member_Pay_Log_Detail;
                }
            }
        }

        if (trade_no != "")
        {
            trade_status = trade_no;
            if (trade_no.IndexOf("ALIPAYTRADENO:") >= 0)
            {
                trade_no = trade_no.Substring(trade_no.IndexOf("ALIPAYTRADENO:") + 13);
                if (trade_no.IndexOf("<BR>") > 0)
                {
                    trade_no = trade_no.Substring(1, trade_no.IndexOf("<BR>") - 1);
                }
            }
        }
        if (trade_no == "")
        {
            return;
        }
        //trade_no = "2010031450113838"
        //该交易号是在支付宝系统中的交易流水号，非客户自己的订单号，此交易号可从“实物标准双接口”的通知或返回页里的trade_no里获取 
        string logistics_name = delivery_companyname;
        //物流公司名称 
        string invoice_no = delivery_no;
        //物流发货单号 
        string transport_type = "EXPRESS";
        //发货时的运输类型：POST, EXPRESS, EMS 
        //支付URL生成 

        string[] para = {
		    "service=send_goods_confirm_by_platform",
		    "partner=" + alipay_partner,
		    "trade_no=" + trade_no,
		    "logistics_name=" + logistics_name,
		    "invoice_no=" + invoice_no,
		    "transport_type=" + transport_type,
		    "_input_charset=" + _input_charset
	    };
        string alipay_sign = CreatUrl(para, _input_charset, sign_type, alipay_key);
        int i = 0;

        //进行排序； 
        string[] Sortedstr = BubbleSort(para);


        //构造待md5摘要字符串 ； 

        StringBuilder prestr = new StringBuilder();

        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {

            if (i == Sortedstr.Length - 1)
            {
                prestr.Append(Sortedstr[i]);
            }
            else
            {
                prestr.Append(Sortedstr[i] + "&");

            }
        }
        string aliay_url = prestr.ToString();
        aliay_url = gateway + aliay_url + "&sign=" + alipay_sign + "&sign_type=" + sign_type;
        //Response.Write(aliay_url)
        //Response.Write(trade_status)
        //Response.Write(trade_no)

        string responseTxt = Get_Http(aliay_url, 120000);
        //Response.Write(responseTxt)
    }

    //订单产品导出
    public void Orders_Goods_Export()
    {
        string OrdersArry = tools.CheckStr(Request["Orders_id"]);
        if (OrdersArry == "")
        {
            pub.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }
        if (tools.Left(OrdersArry, 1) == ",") { OrdersArry = OrdersArry.Remove(0, 1); }
        //string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "订单号", "订单状态", "下单时间", "付款单号", "付款单状态", "付款时间", "发货单号", "发货单状态", "发货时间", "配送方式", "支付方式", "产品批号", "采购渠道", "采购数量", "采购价格", "总价", "商品总价", "运费", "商品编号", "商品名称", "单价", "数量", "商品规格", "生产企业" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;
        int Orders_ID = 0;
        QueryInfo Query = new QueryInfo();
        MemberInfo memberinfo = null;
        MemberProfileInfo memberprofile = null;
        OrdersPaymentInfo orderspayment = null;
        OrdersDeliveryInfo ordersdelivery = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_ID", "in", OrdersArry));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "in", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));

        IList<OrdersInfo> ordersinfo = MyOrders.GetOrderss(Query);
        if (ordersinfo != null)
        {
            foreach (OrdersInfo entity in ordersinfo)
            {
                IList<OrdersGoodsInfo> ordersgoodsinfo = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                if (ordersgoodsinfo != null)
                {
                    foreach (OrdersGoodsInfo goods in ordersgoodsinfo)
                    {
                        if (goods.Orders_Goods_Type != 2 || (goods.Orders_Goods_Type == 2 && goods.Orders_Goods_ParentID > 0))
                        {

                            Orders_ID = entity.Orders_ID;
                            dr = dt.NewRow();

                            dr[0] = entity.Orders_SN;
                            dr[1] = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                            dr[2] = entity.Orders_Addtime;

                            orderspayment = Mypayment.GetOrdersPaymentByOrdersID(entity.Orders_ID, 0);
                            if (orderspayment != null)
                            {
                                dr[3] = orderspayment.Orders_Payment_DocNo;
                            }
                            else
                            {
                                dr[3] = "";
                            }
                            dr[4] = PaymentStatus(entity.Orders_PaymentStatus);
                            dr[5] = entity.Orders_PaymentStatus_Time;

                            ordersdelivery = Mydelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 0, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                            if (ordersdelivery != null)
                            {
                                dr[6] = ordersdelivery.Orders_Delivery_DocNo;
                            }
                            else
                            {
                                dr[6] = "";
                            }

                            dr[7] = DeliveryStatus(entity.Orders_DeliveryStatus);
                            dr[8] = entity.Orders_DeliveryStatus_Time;

                            dr[9] = entity.Orders_Delivery_Name;
                            dr[10] = entity.Orders_Payway_Name;

                            dr[11] = goods.U_Orders_Goods_Product_BatchCode;
                            dr[12] = goods.U_Orders_Goods_Product_BuyChannel;
                            dr[13] = goods.U_Orders_Goods_Product_BuyAmount;
                            dr[14] = goods.U_Orders_Goods_Product_BuyPrice;

                            dr[15] = entity.Orders_Total_AllPrice;
                            dr[16] = entity.Orders_Total_Price;
                            dr[17] = entity.Orders_Total_Freight;

                            dr[18] = goods.Orders_Goods_Product_Code;
                            dr[19] = goods.Orders_Goods_Product_Name;
                            dr[20] = goods.Orders_Goods_Product_Price;
                            dr[21] = goods.Orders_Goods_Amount;
                            dr[22] = goods.Orders_Goods_Product_Spec;
                            dr[23] = goods.Orders_Goods_Product_Maker;

                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }
        pub.toExcel(dt);
    }

    //订单导出
    public void Orders_Export()
    {
        string OrdersArry = tools.CheckStr(Request["Orders_id"]);
        if (OrdersArry == "")
        {
            pub.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(OrdersArry, 1) == ",") { OrdersArry = OrdersArry.Remove(0, 1); }
        //string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "订单号", "订单状态", "下单时间", "付款单号", "付款单状态", "付款时间", "发货单号", "发货单状态", "发货时间", "配送方式", "支付方式", "姓名", "电话", "性别", "手机", "Email", "地址", "邮编", "收货人", "收货人电话", "收货人手机", "收货人地址", "收货人邮编", "总价", "商品总价", "运费", "商品清单", "客服名称" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        int Orders_ID = 0;
        QueryInfo Query = new QueryInfo();
        MemberInfo memberinfo = null;
        MemberProfileInfo memberprofile = null;
        OrdersPaymentInfo orderspayment = null;
        OrdersDeliveryInfo ordersdelivery = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_ID", "in", OrdersArry));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "in", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));

        IList<OrdersInfo> ordersinfo = MyOrders.GetOrderss(Query);
        if (ordersinfo != null)
        {
            foreach (OrdersInfo entity in ordersinfo)
            {


                Orders_ID = entity.Orders_ID;
                dr = dt.NewRow();

                dr[0] = entity.Orders_SN;
                dr[1] = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                dr[2] = entity.Orders_Addtime;
                orderspayment = Mypayment.GetOrdersPaymentByOrdersID(entity.Orders_ID, 0);
                if (orderspayment != null)
                {
                    dr[3] = orderspayment.Orders_Payment_DocNo;
                }
                else
                {
                    dr[3] = "";
                }
                dr[4] = PaymentStatus(entity.Orders_PaymentStatus);
                dr[5] = entity.Orders_PaymentStatus_Time;

                ordersdelivery = Mydelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 0, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                if (ordersdelivery != null)
                {
                    dr[6] = ordersdelivery.Orders_Delivery_DocNo;
                }
                else
                {
                    dr[6] = "";
                }
                dr[7] = DeliveryStatus(entity.Orders_DeliveryStatus);
                dr[8] = entity.Orders_DeliveryStatus_Time;

                dr[9] = entity.Orders_Delivery_Name;
                dr[10] = entity.Orders_Payway_Name;

                memberinfo = new MemberInfo();
                memberinfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberinfo != null)
                {
                    memberprofile = new MemberProfileInfo();
                    memberprofile = memberinfo.MemberProfileInfo;
                    if (memberprofile != null)
                    {
                        dr[11] = memberprofile.Member_Name;
                        dr[12] = memberprofile.Member_Phone_Countrycode + "-" + memberprofile.Member_Phone_Areacode + "-" + memberprofile.Member_Phone_Number;
                        if (memberprofile.Member_Sex == 1)
                        {
                            dr[13] = "女";
                        }
                        else
                        {
                            dr[13] = "男";
                        }
                        dr[14] = memberprofile.Member_Mobile;
                        dr[15] = memberinfo.Member_Email;
                        dr[16] = addr.DisplayAddress(memberprofile.Member_State, memberprofile.Member_City, memberprofile.Member_County) + " " + memberprofile.Member_StreetAddress;
                        dr[17] = memberprofile.Member_Zip;
                    }
                    else
                    {
                        dr[11] = "";
                        dr[12] = "";
                        dr[13] = "";
                        dr[14] = "";
                        dr[15] = "";
                        dr[16] = "";
                        dr[17] = "";
                    }
                }
                else
                {
                    dr[11] = "";
                    dr[12] = "";
                    dr[13] = "";
                    dr[14] = "";
                    dr[15] = "";
                    dr[16] = "";
                    dr[17] = "";
                }



                dr[18] = entity.Orders_Address_Name;
                dr[19] = entity.Orders_Address_Phone_Areacode + "-" + entity.Orders_Address_Phone_Number;
                dr[20] = entity.Orders_Address_Mobile;
                dr[21] = addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + " " + entity.Orders_Address_StreetAddress;
                dr[22] = entity.Orders_Address_Zip;

                dr[23] = entity.Orders_Total_AllPrice;
                dr[24] = entity.Orders_Total_Price;
                dr[25] = entity.Orders_Total_Freight;

                dr[26] = orders_excel_goods(entity.Orders_ID);
                dr[27] = entity.Orders_Source;

                dt.Rows.Add(dr);

            }
        }




        pub.toExcel(dt);
    }

    //订单产品
    public string orders_excel_goods(int orders_ID)
    {
        string result_value = "";
        IList<OrdersGoodsInfo> ordersgoodsinfo = MyOrders.GetGoodsListByOrderID(orders_ID);
        if (ordersgoodsinfo != null)
        {
            foreach (OrdersGoodsInfo goods in ordersgoodsinfo)
            {
                if (goods.Orders_Goods_Type != 2 || (goods.Orders_Goods_Type == 2 && goods.Orders_Goods_ParentID > 0))
                {
                    result_value = result_value + goods.Orders_Goods_Product_Name + "(数量：" + goods.Orders_Goods_Amount + "；单价：" + pub.FormatCurrency(goods.Orders_Goods_Product_Price) + "；采购渠道：" + goods.U_Orders_Goods_Product_BuyChannel + "；采购数量：" + goods.U_Orders_Goods_Product_BuyAmount + "；采购价格：" + goods.U_Orders_Goods_Product_BuyPrice + ")；";
                }
            }
        }
        return result_value;
    }


    public string Order_Detail_Goods_Mail(string Orders_SN)
    {
        int orders_id = 0;
        string strHTML = "";
        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(Orders_SN);
        if (ordersinfo != null)
        {
            orders_id = ordersinfo.Orders_ID;
            double total_price = ordersinfo.Orders_Total_Price;
            double Total_Freight = ordersinfo.Orders_Total_Freight;
            double Orders_Total_PriceDiscount = ordersinfo.Orders_Total_PriceDiscount;
            double Orders_Total_FreightDiscount = ordersinfo.Orders_Total_FreightDiscount;
            double Total_AllPrice = ordersinfo.Orders_Total_AllPrice;
            IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(orders_id);
            if (entitys != null)
            {
                IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
                IList<OrdersGoodsInfo> GoodsListSub = null;
                strHTML = strHTML + "        <table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"5\">";
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td><table cellpadding=\"2\" width=\"100%\" cellspacing=\"1\" bgcolor=\"#EEBB31\">";
                strHTML = strHTML + "              <tr>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\" height=\"20\">商品编号</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">商品名称</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">规格</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">生产厂家</td>";
                //strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">市场价</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">单价</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\" width=\"60\">数量</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">获赠" + Application["Coin_Name"] + "</td>";
                strHTML = strHTML + "                <td align=\"center\" bgcolor=\"#fcf9c6\" class=\"t12_black\">合计</td>";
                strHTML = strHTML + "              </tr>";
                foreach (OrdersGoodsInfo entity in GoodsList)
                {
                    GoodsListSub = Myorder.OrdersGoodsSearch(entitys, entity.Orders_Goods_ID);
                    strHTML = strHTML + "        <tr>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Code + "</td>";
                    strHTML = strHTML + "          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
                    strHTML = strHTML + "            <tr>";
                    if (entity.Orders_Goods_Product_ID > 0)
                    {
                        if (entity.Orders_Goods_Type == 2)
                        {
                            strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + tools.NullStr(Application["site_url"]) + entity.Orders_Goods_Product_Img + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                            strHTML = strHTML + "              <td id=\"pack" + entity.Orders_Goods_ID + "1\"> <strong>" + entity.Orders_Goods_Product_Name + "</strong></td>";
                        }
                        else
                        {
                            strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                            strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"" + tools.NullStr(Application["site_url"]) + "/product/detail.aspx?product_id=" + entity.Orders_Goods_Product_ID + "\"><strong>" + entity.Orders_Goods_Product_Name + "</strong></a></td>";
                        }
                    }
                    else
                    {
                        strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                        strHTML = strHTML + "              <td><strong>" + entity.Orders_Goods_Product_Name + "</strong></td>";
                    }
                    strHTML = strHTML + "            </tr>";
                    strHTML = strHTML + "          </table></td>";
                    strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Spec + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Maker + "</td>";
                    //strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + pub.FormatCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Amount + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Coin + "</td>";
                    strHTML = strHTML + "          <td align=\"right\" bgcolor=\"#FFFFFF\" class=\"price_small\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                    strHTML = strHTML + "</tr>";

                    if (GoodsListSub.Count > 0)
                    {
                        foreach (OrdersGoodsInfo ent in GoodsListSub)
                        {
                            strHTML = strHTML + "<tr bgcolor=\"#FFFFFF\"\">";
                            strHTML = strHTML + "<td align=\"center\">" + ent.Orders_Goods_Product_Code + "</td>";
                            if (ent.Orders_Goods_Type == 1)
                            {
                                strHTML = strHTML + "          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
                                strHTML = strHTML + "            <tr>";
                                if (ent.Orders_Goods_Product_ID > 0)
                                {

                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><span class=\"t12_red\">[赠品]</span> <a class=\"a_t12_black\" href=\"/product/detail.aspx?product_id=" + ent.Orders_Goods_Product_ID + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
                                }
                                else
                                {
                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td><span class=\"t12_red\">[赠品]</span> <strong>" + ent.Orders_Goods_Product_Name + "</strong></td>";
                                }
                                strHTML = strHTML + "            </tr>";
                                strHTML = strHTML + "          </table></td>";
                            }
                            else
                            {
                                strHTML = strHTML + "          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
                                strHTML = strHTML + "            <tr>";
                                if (ent.Orders_Goods_Product_ID > 0)
                                {

                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"/product/detail.aspx?product_id=" + ent.Orders_Goods_Product_ID + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
                                }
                                else
                                {
                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td><strong>" + ent.Orders_Goods_Product_Name + "</strong></td>";
                                }
                                strHTML = strHTML + "            </tr>";
                                strHTML = strHTML + "          </table></td>";
                            }
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + ent.Orders_Goods_Product_Spec + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + ent.Orders_Goods_Product_Maker + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + pub.FormatCurrency(ent.Orders_Goods_Product_MKTPrice) + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + pub.FormatCurrency(ent.Orders_Goods_Product_Price) + "</td>";
                            strHTML = strHTML + "          <td align=\"center\"  class=\"t12_red\">1×" + (ent.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">--</td>";
                            strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">--</td>";
                            strHTML = strHTML + "</tr>";
                        }
                    }
                }
                strHTML = strHTML + "            </table></td>";
                strHTML = strHTML + "          </tr>";
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">产品总价 <span class=\"price_small\">" + pub.FormatCurrency(total_price) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">运费 <span class=\"price_small\">" + pub.FormatCurrency(Total_Freight) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                if (Orders_Total_PriceDiscount > 0)
                {
                    strHTML = strHTML + "          <tr>";
                    strHTML = strHTML + "            <td align=\"right\" class=\"t12\">价格优惠 <span class=\"price_small\">-" + pub.FormatCurrency(Orders_Total_PriceDiscount) + "</span></td>";
                    strHTML = strHTML + "          </tr>";
                }
                if (Orders_Total_FreightDiscount > 0)
                {
                    strHTML = strHTML + "          <tr>";
                    strHTML = strHTML + "            <td align=\"right\" class=\"t12\">运费优惠 <span class=\"price_small\">-" + pub.FormatCurrency(Orders_Total_FreightDiscount) + "</span></td>";
                    strHTML = strHTML + "          </tr>";
                }
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">总价（含运费） <span class=\"price\">" + pub.FormatCurrency(Total_AllPrice) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                strHTML = strHTML + "        </table>";
            }
        }
        return strHTML;
    }

    //邮件模版
    public string mail_template(string template_name, string member_email, string orders_sn)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "order_freight":
                mailbody = "<p>感谢您通过{sys_config_site_name}购物，您的订购的商品信息如下：</p>";
                mailbody = mailbody + "<p>" + Order_Detail_Goods_Mail(orders_sn) + "</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.htm\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

        }
        mailbody = mailbody.Replace("{member_email}", member_email);
        return mailbody;
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

    public static string[] BubbleSort(string[] r)
    {
        /// <summary>
        /// 冒泡排序法
        /// </summary>
        int i, j; //交换标志 
        string temp;

        bool exchange;

        for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
        {
            exchange = false; //本趟排序开始前，交换标志应为假

            for (j = r.Length - 2; j >= i; j--)
            {
                if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                {
                    temp = r[j + 1];
                    r[j + 1] = r[j];
                    r[j] = temp;

                    exchange = true; //发生了交换，故将交换标志置为真 
                }
            }

            if (!exchange) //本趟排序未发生交换，提前终止算法 
            {
                break;
            }

        }
        return r;
    }

    public static string CreatUrl(string[] para, string _input_charset, string sign_type, string key)
    {
        int i = 0;

        //进行排序； 
        string[] Sortedstr = BubbleSort(para);


        //构造待md5摘要字符串 ； 

        StringBuilder prestr = new StringBuilder();

        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {

            if (i == Sortedstr.Length - 1)
            {
                prestr.Append(Sortedstr[i]);
            }
            else
            {
                prestr.Append(Sortedstr[i] + "&");

            }
        }

        prestr.Append(key);

        //生成Md5摘要； 
        string sign = GetMD5(prestr.ToString(), _input_charset);
        // 
        // //返回支付Url；  
        return sign;
    }

    public static string GetMD5(string s, string _input_charset)
    {
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }

    //获取远程服务器ATN结果
    public String Get_Http(String a_strUrl, int timeout)
    {
        string strResult;
        try
        {

            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
            myReq.Timeout = timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.Default);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {

            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }


    //对账单
    public void Shop_Orders_Count()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <td align=\"center\" valign=\"middle\">订单号</td>");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\">订单金额</td>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">产品金额</td>");
        Response.Write("  <td width=\"70\" align=\"center\" valign=\"middle\">价格优惠</td>");
        Response.Write("  <td width=\"70\" align=\"center\" valign=\"middle\">运费</td>");
        Response.Write("  <td width=\"70\" align=\"center\" valign=\"middle\">运费优惠</td>");
        Response.Write("  <td width=\"70\" align=\"center\" valign=\"middle\">订单佣金</td>");
        Response.Write("  <td width=\"70\" align=\"center\" valign=\"middle\">商家所得</td>");
        Response.Write("  <td width=\"50\" align=\"center\" valign=\"middle\">状态</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        //string supplier_orders = MyOrders.GetSupplierOrdersID(supplier_id);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierID", "in", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> entitys = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);
        double orders_brokerage = 0;
        IList<OrdersGoodsInfo> goodsinfos;
        if (entitys != null)
        {
            foreach (OrdersInfo entity in entitys)
            {
                i = i + 1;
                orders_brokerage = 0;
                goodsinfos = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                if (goodsinfos != null)
                {
                    foreach (OrdersGoodsInfo goodsinfo in goodsinfos)
                    {
                        orders_brokerage = orders_brokerage + goodsinfo.Orders_Goods_Product_brokerage * goodsinfo.Orders_Goods_Amount;
                    }
                }
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\"><a href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "\">" + entity.Orders_SN + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_Price) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_PriceDiscount) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_Freight) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_FreightDiscount) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(orders_brokerage) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice - orders_brokerage) + "</td>");
                if (entity.Orders_IsSettling == 2)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><span class=\"t12_green\">已结算</span></td>");
                }
                else if (entity.Orders_IsSettling == 1)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><span class=\"t12_red\">结算中</span></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a href=\"/supplier/orders_do.aspx?action=apply_settling&orders_id=" + entity.Orders_ID + "\">申请结算</a></td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"9\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    /// <summary>
    /// 商品销售对账单
    /// </summary>
    public void Sales_Statement()
    {
        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">订单号</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">商品编码</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">商品名称</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">单价</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">数量</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">佣金</th>");
        //Response.Write("  <th width=\"50\">操作</th>");
        Response.Write("</tr>");
        string PageUrl = "?list=list";

        int PageSize = 15;
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "Orders_Goods_Product_Code, Orders_Goods_Product_Name, Orders_Goods_Product_Price, Orders_Goods_Product_brokerage, Orders_Goods_Amount, Orders_SN";
        string SqlTable = "Orders_Goods INNER JOIN Orders ON Orders.Orders_ID = Orders_Goods.Orders_Goods_OrdersID";
        string SqlOrder = "ORDER BY Orders_Goods_ID DESC";
        string SqlParam = " WHERE Orders_Goods_Product_SupplierID = " + tools.NullInt(Session["supplier_id"]) + " AND Orders.Orders_Status = 2";

        string SqlCount = "SELECT COUNT(Orders_Goods_ID) FROM " + SqlTable + " " + SqlParam;
        SqlDataReader RdrList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);

            RdrList = DBHelper.ExecuteReader(SqlList);

            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    Response.Write("<tr bgcolor=\"#ffffff\">");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + tools.NullStr(RdrList["Orders_SN"]) + "</td>");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + tools.NullStr(RdrList["Orders_Goods_Product_Code"]) + "</td>");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + tools.NullStr(RdrList["Orders_Goods_Product_Name"]) + "</td>");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + pub.FormatCurrency(tools.NullDbl(RdrList["Orders_Goods_Product_Price"])) + "</td>");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + tools.NullInt(RdrList["Orders_Goods_Amount"]) + "</td>");
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + pub.FormatCurrency(tools.NullDbl(RdrList["Orders_Goods_Product_brokerage"]) * tools.NullInt(RdrList["Orders_Goods_Amount"])) + "</td>");

                    //Response.Write("<td>");
                    //Response.Write("<a href=\"/supplier/keywordbidding_edit.aspx?keywordbidding_id=" + entity.KeywordBidding_ID + "\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"修改\"></a>");
                    //Response.Write(" <a href=\"/supplier/keywordbidding_do.aspx?action=del&keywordbidding_id=" + entity.KeywordBidding_ID + "\"><img src=\"/images/btn_del.gif\" border=\"0\" alt=\"删除\"></a>");
                    //Response.Write("</td>");
                    Response.Write("</tr>");
                }

                Response.Write("</table>");
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
                pub.Page(PageCount, CurrentPage, PageUrl, PageSize, RecordCount);
                Response.Write("</div></td></tr></table>");
            }
            else
            {
                Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"8\">没有记录</td></tr>");
                Response.Write("</table>");
            }

        }
        catch (Exception ex)
        {
            throw ex;
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

    //申请结算
    public void Orders_Settling_Apply()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string orders_sn;
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        OrdersInfo ordersinfo = Myorder.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (("," + supplier_orders + ",").IndexOf("," + Orders_ID + ",") > 0)
            {
                if (ordersinfo.Orders_Status == 2 && ordersinfo.Orders_IsSettling == 0)
                {


                    orders_sn = ordersinfo.Orders_SN;

                    ordersinfo.Orders_IsSettling = 1;

                    if (MyOrders.EditOrders(ordersinfo))
                    {


                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "申请结算", "成功", "商家申请订单结算");
                        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/Supplier_orders_count.aspx");
                    }
                }
                else
                {
                    pub.Msg("error", "错误提示", "订单结算申请提交失败!", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误提示", "订单结算申请提交失败!", false, "{back}");
            }

        }
        Response.Redirect("/supplier/index.aspx");
    }


    //库存统计
    public void Shop_Product_Stock()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">产品编号</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">产品名称</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">销售状态</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">价格</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">剩余库存</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_StockAmount", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = MyProduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" valign=\"middle\" align=\"center\" >" + entity.Product_Code + "</td>");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Product_Name + "</a></td>");
                if (entity.Product_IsInsale == 0)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><span class=\"t12_red\">已下架</span></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">销售中</td>");
                }
                Response.Write("<td height=\"35\" align=\"center\"  valign=\"middle\">" + pub.FormatCurrency(entity.Product_Price) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\"  valign=\"middle\">" + entity.Product_StockAmount + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"5\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //客户数据
    public void Shop_Customer_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"972\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
        //Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"100\" class=\"name\" align=\"center\" valign=\"middle\">订单编号</th>");
        Response.Write("  <td width=\"70\" class=\"name\" align=\"center\" valign=\"middle\">订单状态</th>");
        Response.Write("  <td align=\"center\" class=\"name\" valign=\"middle\">会员名</th>");
        Response.Write("  <td width=\"70\" class=\"name\" align=\"center\" valign=\"middle\">收货人</th>");
        Response.Write("  <td align=\"center\" class=\"name\" valign=\"middle\">收货地址</th>");
        Response.Write("  <td width=\"70\" class=\"name\" align=\"center\" valign=\"middle\">邮编</th>");
        Response.Write("  <td width=\"100\" class=\"name\" align=\"center\" valign=\"middle\">联系电话</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        MemberInfo memberinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        string Orders_Status = "";
        Query.CurrentPage = curpage;
        //string supplier_orders = MyOrders.GetSupplierOrdersID(supplier_id);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierID", "in", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> entitys = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (OrdersInfo entity in entitys)
            {
                if (entity.Orders_Status != 1 || entity.Orders_DeliveryStatus == 0)
                {
                    Orders_Status = OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus);
                }
                else
                {
                    Orders_Status = DeliveryStatus(entity.Orders_DeliveryStatus);
                }
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a class=\"a_t12_blue\" href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Orders_SN + "\">" + entity.Orders_SN + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + Orders_Status + "</td>");
                memberinfo = MyMEM.GetMemberByID(entity.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberinfo != null)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + memberinfo.Member_NickName + "</td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">--</td>");
                }
                string orders_address = "" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + " " + entity.Orders_Address_StreetAddress + "";
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Orders_Address_Name + "</td>");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\" title=\"" + orders_address + "\">" + tools.CutStr(orders_address, 30) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Orders_Address_Zip + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Orders_Address_Phone_Number + " " + entity.Orders_Address_Mobile + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            //Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            //Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"7\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //销售统计
    public void Shop_Sale_Product()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">产品编号</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">产品名称</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">销售状态</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">价格</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">销售数量</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">剩余库存</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SaleAmount", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = MyProduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Product_Code + "</td>");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Product_Name + "</a></td>");
                if (entity.Product_IsInsale == 0)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><span class=\"t12_red\">已下架</span></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">销售中</td>");
                }
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + pub.FormatCurrency(entity.Product_Price) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Product_SaleAmount + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Product_StockAmount + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"6\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void Order_BackApply_List()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        int back_status = tools.CheckInt(Request["back_status"]);
        Pageurl = "?action=list&back_status=" + back_status;
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">订单号码</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\" >退款金额</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">操作类型</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">申请人</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">申请时间</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">状态</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        string supplier_orders = MyOrders.GetSupplierOrdersID(tools.NullInt(Session["supplier_id"]));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_OrdersCode", "in", "select orders_sn from orders where orders_id in (" + supplier_orders + ")"));

        if (back_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "1"));
        }

        Query.OrderInfos.Add(new OrderInfo("OrdersBackApplyInfo.Orders_BackApply_ID", "Desc"));
        IList<OrdersBackApplyInfo> entitys = MyBack.GetOrdersBackApplys(Query, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
        PageInfo page = MyBack.GetPageInfo(Query, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
        if (entitys != null)
        {
            foreach (OrdersBackApplyInfo entity in entitys)
            {
                i = i + 1;
                if (i % 2 == 0)
                {
                    Response.Write("<tr bgcolor=\"#FFF\">");
                }
                else
                {
                    Response.Write("<tr bgcolor=\"#FFF\">");
                }
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>" + entity.Orders_BackApply_OrdersCode + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>" + pub.FormatCurrency(entity.Orders_BackApply_Amount) + "</a></td>");
                if (entity.Orders_BackApply_Type == 0)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>退货</a></td>");
                }
                else if (entity.Orders_BackApply_Type == 2)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>退款</a></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>换货</a></td>");
                }

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>" + entity.Orders_BackApply_Name + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>" + entity.Orders_BackApply_Addtime.ToString() + "</a></td>");
                if (entity.Orders_BackApply_Status == 0)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>未处理</a></td>");
                }
                else if (entity.Orders_BackApply_Status == 1)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>审核通过</a></td>");
                }
                else if (entity.Orders_BackApply_Status == 2 || entity.Orders_BackApply_Status == 4)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>审核失败</a></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>平台处理完成</a></td>");
                }

                if (back_status == 2)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a href=\"orders_backproductview.aspx?back_id=" + entity.Orders_BackApply_ID + "\">查看</a></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a href=\"order_backview.aspx?back_id=" + entity.Orders_BackApply_ID + "\">查看</a></td>");
                }
                Response.Write("</tr>");

            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFF\" ><td height=\"35\" align=\"center\" colspan=\"7\" valign=\"middle\" ><a>没有记录</a></td></tr>");
            Response.Write("</table>");
        }

    }


    public OrdersBackApplyInfo GetOrdersBackApplyByID(int ID)
    {
        OrdersBackApplyInfo entity = MyBack.GetOrdersBackApplyByID(ID, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
        if (entity != null)
        {
            OrdersInfo ordersinfo = GetOrdersInfoBySN(entity.Orders_BackApply_OrdersCode, tools.NullInt(Session["supplier_id"]));
            if (ordersinfo != null)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void OrdersBackApplyEdit()
    {
        int back_id = tools.CheckInt(Request["back_id"]);
        int back_action = tools.CheckInt(Request["back_action"]);
        string back_note = tools.CheckStr(Request["back_note"]);
        if (back_note.Length == 0)
        {
            pub.Msg("info", "信息提示", "请将审核备注填写完整！", false, "{back}");
        }
        OrdersBackApplyInfo backinfo = GetOrdersBackApplyByID(back_id);
        if (backinfo != null)
        {
            if (backinfo.Orders_BackApply_Status == 0)
            {
                if (back_action == 1)
                {
                    backinfo.Orders_BackApply_Status = 1;
                }
                else
                {
                    backinfo.Orders_BackApply_Status = 2;
                }
                backinfo.Orders_BackApply_SupplierNote = back_note;
                backinfo.Orders_BackApply_SupplierTime = DateTime.Now;
                MyBack.EditOrdersBackApply(backinfo, pub.CreateUserPrivilege("1f9e3d6c-2229-4894-891b-13e73dd2e593"));
            }
        }
        Response.Redirect("order_backview.aspx?back_id=" + back_id);
    }


    public string GetOrderBackGoodsInfo(int back_id)
    {
        int i = 0;
        string return_value = "";
        IList<OrdersBackApplyProductInfo> ordersbackapplyproductinfos = OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct().GetOrdersBackApplyProductByApplyID(back_id);
        if (ordersbackapplyproductinfos != null)
        {
            return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
            return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">实退数量</td></tr>";
            foreach (OrdersBackApplyProductInfo ordersbackapplyproductinfo in ordersbackapplyproductinfos)
            {
                OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID);
                if (entity != null)
                {
                    i++;
                    return_value += "<tr><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\" align=\"center\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\"><input type=\"text\" name=\"Orders_BackApply_Product_ApplyAmount_" + i + "\" size=\"8\" value=\"" + ordersbackapplyproductinfo.Orders_BackApply_Product_ApplyAmount + "\"/><input type=\"hidden\" name=\"Orders_BackApply_Product_ProductID_" + i + "\" value=\"" + ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID + "\"/></td></tr>";
                }
            }
            return_value += "<input type=\"hidden\" name=\"goods_amount\" value=\"" + i + "\"|></table>";
        }
        return return_value;
    }

    public string GetOrderBackGoodsInfo1(int back_id)
    {
        string return_value = "";
        IList<OrdersBackApplyProductInfo> ordersbackapplyproductinfos = OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct().GetOrdersBackApplyProductByApplyID(back_id);
        if (ordersbackapplyproductinfos != null)
        {
            return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
            return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">实退数量</td></tr>";
            foreach (OrdersBackApplyProductInfo ordersbackapplyproductinfo in ordersbackapplyproductinfos)
            {
                OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID);
                if (entity != null)
                {
                    return_value += "<tr><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\" align=\"center\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\">" + ordersbackapplyproductinfo.Orders_BackApply_Product_ApplyAmount + "</td></tr>";
                }
            }
            return_value += "</table>";
        }
        return return_value;
    }

    public void Creat_Orders_Delivery()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Orders_Delivery_Name = tools.CheckStr(Request["Orders_Delivery_Name"]);
        string Orders_Delivery_companyName = tools.CheckStr(Request["Orders_Delivery_companyName"]);
        string Orders_Delivery_Code = tools.CheckStr(Request["Orders_Delivery_Code"]);
        string Orders_Delivery_Note = tools.CheckStr(Request["Orders_Delivery_Note"]);
        double Orders_Delivery_Amount = tools.CheckFloat(Request["Orders_Delivery_Amount"]);
        string Orders_BackApply_ProductID = tools.NullStr(Request["Orders_BackApply_ProductID"]);
        int back_id = tools.NullInt(Request["back_id"]);
        int goods_amount = tools.CheckInt(Request["goods_amount"]);
        int Orders_Delivery_DeliveryStatus = 0;
        string Orders_SN = "";
        int Orders_Delivery_ID;
        string Orders_Delivery_DocNo = orders_delivery_sn();
        string freightreason = "";
        string member_email = "";

        if (Orders_Delivery_Name == "")
        {
            pub.Msg("info", "信息提示", "请填写配送方式", false, "{back}");
        }

        OrdersInfo ordersinfo = MyOrders.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 4)
            {
                Orders_SN = ordersinfo.Orders_SN;

                OrdersDeliveryInfo ordersdelivery = new OrdersDeliveryInfo();
                ordersdelivery.Orders_Delivery_ID = 0;
                ordersdelivery.Orders_Delivery_DeliveryStatus = 5;
                ordersdelivery.Orders_Delivery_SysUserID = 0;
                ordersdelivery.Orders_Delivery_OrdersID = Orders_ID;
                ordersdelivery.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
                ordersdelivery.Orders_Delivery_Name = Orders_Delivery_Name;
                ordersdelivery.Orders_Delivery_companyName = Orders_Delivery_companyName;
                ordersdelivery.Orders_Delivery_Code = Orders_Delivery_Code;
                ordersdelivery.Orders_Delivery_Amount = Orders_Delivery_Amount;
                ordersdelivery.Orders_Delivery_Note = Orders_Delivery_Note;
                ordersdelivery.Orders_Delivery_Addtime = DateTime.Now;
                ordersdelivery.Orders_Delivery_Status = 0;
                ordersdelivery.Orders_Delivery_Site = pub.GetCurrentSite();
                if (Mydelivery.AddOrdersDelivery(ordersdelivery, pub.CreateUserPrivilege("800fdc63-fa5d-44de-927e-8d4560c2f238")))
                {
                    ordersdelivery = Mydelivery.GetOrdersDeliveryBySn(Orders_Delivery_DocNo, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

                    if (ordersdelivery != null)
                    {
                        Orders_Delivery_ID = ordersdelivery.Orders_Delivery_ID;
                        OrdersBackApplyProductInfo entity = null;
                        for (int i = 1; i <= goods_amount; i++)
                        {
                            int Orders_BackApply_Product_ProductID = tools.CheckInt(Request["Orders_BackApply_Product_ProductID_" + i + ""]);
                            int Orders_BackApply_Product_ApplyAmount = tools.CheckInt(Request["Orders_BackApply_Product_ApplyAmount_" + i + ""]);
                            if (Orders_BackApply_Product_ProductID != 0 && Orders_BackApply_Product_ApplyAmount > 0)
                            {
                                //entity = new OrdersBackApplyProductInfo();
                                //entity.Orders_BackApply_Product_ApplyID = Orders_BackApply_ID;
                                //entity.Orders_BackApply_Product_ProductID = Orders_BackApply_Product_ProductID;
                                //entity.Orders_BackApply_Product_ApplyAmount = Orders_BackApply_Product_ApplyAmount;
                                //MyBackProduct.AddOrdersBackApplyProduct(entity);

                            }

                        }
                        freightreason = ordersdelivery.Orders_Delivery_DocNo;
                        freightreason = "订单退货，退货单号{order_freight_sn}";
                        freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + ordersdelivery.Orders_Delivery_ID + "\">" + ordersdelivery.Orders_Delivery_DocNo + "</a>");

                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "退货", "成功", freightreason);
                        ordersinfo.Orders_DeliveryStatus = 5;
                        ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                        MyOrders.EditOrders(ordersinfo);
                        OrdersBackApplyInfo orderbackapplyinfo = MyBack.GetOrdersBackApplyByID(back_id, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
                        if (orderbackapplyinfo != null)
                        {
                            orderbackapplyinfo.Orders_BackApply_Status = 3;
                            MyBack.EditOrdersBackApply(orderbackapplyinfo, pub.CreateUserPrivilege("1f9e3d6c-2229-4894-891b-13e73dd2e593"));
                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_backlist.aspx?back_status=2&menu=7");
                    }
                }
            }
        }
        Response.Redirect("/supplier/order_backlist.aspx?back_status=2&menu=7");
    }



    //更新产品销售量
    public void Orders_Product_Update_Salecount(int Orders_ID)
    {
        int Product_ID, Goods_Type;
        int Goods_Amount;
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                Product_ID = entity.Orders_Goods_Product_ID;
                Goods_Amount = (int)Math.Round(entity.Orders_Goods_Amount);
                Goods_Type = entity.Orders_Goods_Type;

                if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                {
                    MyProduct.UpdateProductSaleAmount(Product_ID, Goods_Amount);
                }
            }
        }
    }

    #endregion

    #region 合同管理

    #region 支付方式选择
    /// <summary>
    /// 支付方式选择
    /// </summary>
    /// <param name="select_name"></param>
    /// <param name="payway"></param>
    /// <returns></returns>
    public string Payway_Select(string select_name, int payway)
    {
        string way_list = "";
        way_list = "<select name=\"" + select_name + "\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        IList<PayWayInfo> payways = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
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

    #endregion

    #region 根据合同编号获取合同信息
    /// <summary>
    /// 根据合同编号获取合同信息
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public ContractInfo GetContractByID(int ID)
    {
        return MyContract.GetContractByID(ID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
    }
    #endregion

    #region 根据合同编号获取合同信息
    /// <summary>
    /// 根据合同编号获取合同信息
    /// </summary>
    /// <param name="SN"></param>
    /// <returns></returns>
    public ContractInfo GetContractBySn(string SN)
    {
        return MyContract.GetContractBySn(SN, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
    }
    #endregion

    #region 根据合同编号获取合同附件订单信息
    /// <summary>
    /// 根据合同编号获取合同附件订单信息
    /// </summary>
    /// <param name="Contract_ID"></param>
    /// <returns></returns>
    public IList<OrdersInfo> GetOrderssByContractID(int Contract_ID)
    {
        return MyOrders.GetOrderssByContractID(Contract_ID);
    }
    #endregion

    #region 通过 sn 获取合同交货(ps:根据方法名机翻)

    /// <summary>
    /// 通过 sn 获取合同交货
    /// </summary>
    /// <param name="SN"></param>
    /// <returns></returns>
    public ContractDeliveryInfo GetContractDeliveryBySN(string SN)
    {
        return MyFreight.GetContractDeliveryBySN(SN);
    }

    #endregion

    #region 按合同 id 获取合同发票(ps:根据方法名机翻)

    /// <summary>
    /// 按合同 id 获取合同发票
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public ContractInvoiceInfo GetContractInvoiceByContractID(int ID)
    {
        return MyContract.GetContractInvoiceByContractID(ID);
    }

    #endregion

    #region 意向合同选择

    /// <summary>
    /// 意向合同选择
    /// </summary>
    /// <param name="Contract_Type"></param>
    /// <returns></returns>
    public string TmpContract_Select(int Contract_Type)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        StringBuilder Html_Str = new StringBuilder();
        Html_Str.Append("<select name=\"Contract_ID\">");
        Html_Str.Append("<option value=\"0\">选择意向合同</option>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_BuyerID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Source", "=", "0"));
        if (Contract_Type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Type", "=", Contract_Type.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Confirm_Status", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("ContractInfo.Contract_ID", "DESC"));
        IList<ContractInfo> entitys = MyContract.GetContracts(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
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

    #endregion

    #region 生成意向合同编号

    /// <summary>
    /// 生成意向合同编号
    /// </summary>
    /// <returns></returns>
    public string Create_TmpContract_SN()
    {
        string sn = "YX-ZGSB-BJXS(DS)-" + tools.FormatDate(DateTime.Now, "yyyy-MM") + "-";
        string sub_sn = "";
        int orders_amount = MyContract.GetContractAmount("0,1,2,3", tools.FormatDate(DateTime.Now, "yyyy-MM").ToString(), pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        sub_sn = "0000" + (orders_amount + 1).ToString();
        sub_sn = sub_sn.Substring(sub_sn.Length - 4);
        sn = sn + sub_sn;
        return sn;
    }

    #endregion

    #region 生成正式合同编号

    /// <summary>
    /// 生成正式合同编号
    /// </summary>
    /// <returns></returns>
    public string Create_Contract_SN()
    {
        string sn = "ZGSB-BJXS(DS)-" + tools.FormatDate(DateTime.Now, "yyyy-MM") + "-";
        string sub_sn = "";
        int orders_amount = MyContract.GetContractAmount("1,2,3", sn, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        sub_sn = "0000" + (orders_amount + 1).ToString();
        sub_sn = sub_sn.Substring(sub_sn.Length - 4);
        sn = sn + sub_sn;
        return sn;
    }

    #endregion

    #region 生成意向合同

    /// <summary>
    /// 生成意向合同
    /// </summary>
    /// <param name="Sign"></param>
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
            ordersinfo = GetOrdersInfoBySN(orders_sn);
            if (ordersinfo == null)
            {
                pub.Msg("error", "错误信息", "订单记录不存在", false, "/supplier/order_list.aspx");
            }
            else
            {
                contract_type = ordersinfo.Orders_Type;
            }
            if (ordersinfo.Orders_ContractID > 0)
            {
                pub.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "/supplier/order_list.aspx");
            }
        }

        if (Contract_Name.Length == 0)
        {
            pub.Msg("info", "提示信息", "请填写合同名称", false, "{back}");
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

        if (Template_ID > 0)
        {
            SupplierContractTemplateInfo templateinfo = GetSupplierContractTemplateByID(Template_ID);
            if (templateinfo != null)
            {
                if (templateinfo.Contract_Template_SupplierID != supplier_id)
                {
                    pub.Msg("info", "提示信息", "选择模板无效", false, "{back}");
                }
                else
                {
                    Sign = "";
                    Contract_Template = templateinfo.Contract_Template_Content;
                }
            }
        }
        if (Sign.Length > 0)
        {
            //获取意向合同模版
            ContractTemplateInfo templateinfo = MyTemplate.GetContractTemplateBySign(Sign, pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
            if (templateinfo != null)
            {
                Contract_Template = templateinfo.Contract_Template_Content;
            }
        }
        Contract_SN = Create_TmpContract_SN();
        ContractInfo entity = new ContractInfo();
        entity.Contract_Type = contract_type;
        entity.Contract_SN = Contract_SN;
        entity.Contract_BuyerID = supplier_id;
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
        entity.Contract_Site = pub.GetCurrentSite();
        entity.Contract_Status = 0;
        entity.Contract_Payment_Status = 0;
        entity.Contract_Delivery_Status = 0;
        entity.Contract_Confirm_Status = 0;
        entity.Contract_Note = "";
        entity.Contract_Addtime = DateTime.Now;
        entity.Contract_Discount = 0;
        entity.Contract_Source = 0;
        entity.Contract_IsEvaluate = 0;
        if (MyContract.AddContract(entity, pub.CreateUserPrivilege("010afb3b-1cbf-47f9-8455-c35fe5eceea7")))
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
                        tmp_contract.Contract_SupplierID = ordersinfo.Orders_SupplierID;
                        tmp_contract.Contract_Payway_ID = ordersinfo.Orders_Payway;
                        tmp_contract.Contract_Payway_Name = ordersinfo.Orders_Payway_Name;
                        //代理采购合同
                        if (tmp_contract.Contract_Type == 3)
                        {
                            SupplierInfo supplierinfo = GetSupplierByID(ordersinfo.Orders_SupplierID);
                            if (supplierinfo != null)
                            {
                                //更新代理费用
                                tmp_contract.Contract_ServiceFee = Math.Round((tmp_contract.Contract_Price * supplierinfo.Supplier_AgentRate) / 100, 2);
                                tmp_contract.Contract_AllPrice = ordersinfo.Orders_Total_AllPrice + tmp_contract.Contract_ServiceFee;
                            }
                        }
                        MyContract.EditContract(tmp_contract, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                    }
                }
                //生成意向合同日志
                Contract_Log_Add(tmp_contract.Contract_ID, "", "生成意向合同", 1, "生成意向合同，合同编号：" + Contract_SN);
                pub.Msg("positive", "操作成功", "意向合同创建成功！", true, "Contract_list.aspx");
            }
            else
            {
                pub.Msg("error", "操作失败", "意向合同创建失败！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "操作失败", "意向合同创建失败！", false, "{back}");
        }
    }

    #endregion

    #region 意向合同转换为履行中合同

    /// <summary>
    /// 意向合同转换为履行中合同
    /// </summary>
    /// <param name="entity"></param>
    public void TmpContract_To_Contract(ContractInfo entity)
    {
        string Contract_Sn;
        //验证合同状态
        if (entity.Contract_Status > 0 || entity.Contract_Confirm_Status == 0)
        {
            pub.Msg("info", "提示信息", "源意向合同无法执行此操作！", false, "{back}");
        }

        Contract_Sn = Create_Contract_SN();
        //买家最后确认
        if (entity.Contract_Confirm_Status == 1)
        {
            entity.Contract_Status = 1;
            entity.Contract_SN = Contract_Sn;
            entity.Contract_Addtime = DateTime.Now;
            if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
            {
                Contract_Log_Add(entity.Contract_ID, "", "意向合同转换正式合同", 1, "意向合同转换正式合同，新合同编号：" + Contract_Sn);
                pub.Msg("positive", "操作成功", "操作成功！", false, "contract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
            else
            {
                pub.Msg("error", "操作失败", "操作失败！", false, "{back}");
            }
        }
        else
        {
            //卖家最后确认
            entity.Contract_Status = 1;
            entity.Contract_SN = Contract_Sn;
            entity.Contract_Addtime = DateTime.Now;
            if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
            {
                Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "意向合同转换正式合同", 1, "意向合同转换正式合同，新合同编号：" + Contract_Sn);
                pub.Msg("positive", "操作成功", "操作成功！", false, "mycontract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
            else
            {
                pub.Msg("error", "操作失败", "操作失败！", false, "{back}");
            }
        }
    }

    #endregion

    #region 待评价合同

    /// <summary>
    /// 待评价合同
    /// </summary>
    /// <returns></returns>
    public int UnEvaluationContractCount()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_BuyerID", "=", Convert.ToInt32(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_IsEvaluate", "=", "0"));
        PageInfo page = MyContract.GetPageInfo(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        int EvaluationContractCount = page.RecordCount;
        page = null;

        return EvaluationContractCount;
    }

    #endregion

    #region 供应商合同计数(ps:根据机翻)

    /// <summary>
    /// 供应商合同计数
    /// </summary>
    /// <param name="listtype"></param>
    /// <param name="supplier_id"></param>
    /// <param name="count_type"></param>
    /// <returns></returns>
    public int Supplier_Contract_Count(int listtype, int supplier_id, string count_type)
    {

        int Contract_Count = 0;
        if (supplier_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            if (listtype == 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_BuyerID", "=", supplier_id.ToString()));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_SupplierID", "=", supplier_id.ToString()));
            }
            switch (count_type)
            {
                case "temp":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "0"));
                    break;
                case "processing":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "1"));
                    break;
                case "success":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "2"));
                    break;
                case "faiture":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "3"));
                    break;
            }
            PageInfo page = MyContract.GetPageInfo(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
            if (page != null)
            {
                Contract_Count = page.RecordCount;
            }
        }
        return Contract_Count;
    }

    #endregion

    #region 合同页选项卡

    /// <summary>
    /// 合同页选项卡
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string Contract_TabControl(int listtype, string type)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        string date_start = tools.CheckStr(Request["date_start"]);
        string date_end = tools.CheckStr(Request["date_end"]);
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        int contract_type = tools.CheckInt(Request["contract_type"]);
        string queryurl = "contract_type=" + contract_type + "&contract_sn=" + contract_sn + "&date_start=" + date_start + "&date_end=" + date_end;
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<ul class=\"zkw_lst31\">");
        strHTML.Append("	<li style=\"float:left;margin:12px;10px;12px;10px;font-weight:normal;\" " + (type == "all" ? "class=\"on\"" : "") + " id=\"n01\"><a href=\"?" + queryurl + "\">全部合同(<span>" + Supplier_Contract_Count(listtype, supplier_id, "all") + "</span>)</a></li>");
        strHTML.Append("	<li style=\"float:left;margin:12px;10px;12px;10px;font-weight:normal;\"" + (type == "temp" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=temp\">意向合同(<span>" + Supplier_Contract_Count(listtype, supplier_id, "temp") + "</span>)</a></li>");
        strHTML.Append("	<li style=\"float:left;margin:12px;10px;12px;10px;font-weight:normal;\"" + (type == "processing" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=processing\">履行中合同(<span>" + Supplier_Contract_Count(listtype, supplier_id, "processing") + "</span>)</a></li>");
        strHTML.Append("	<li style=\"float:left;margin:12px;10px;12px;10px;font-weight:normal;\"" + (type == "success" ? "class=\"on\"" : "") + " id=\"n04\"><a href=\"?" + queryurl + "&type=success\">交易成功合同(<span>" + Supplier_Contract_Count(listtype, supplier_id, "success") + "</span>)</a></li>");
        strHTML.Append("	<li style=\"float:left;margin:12px;10px;12px;10px;font-weight:normal;\"" + (type == "faiture" ? "class=\"on\"" : "") + " id=\"n05\"><a href=\"?" + queryurl + "&type=faiture\">交易失败合同(<span>" + Supplier_Contract_Count(listtype, supplier_id, "faiture") + "</span>)</a></li>");
        strHTML.Append("</ul><div class=\"clear\"></div>");

        return strHTML.ToString();
    }

    #endregion


    public void Contract_List(int listtype, string type)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string tmp_head, form_action;
        form_action = "";
        string date_start, date_end, contract_sn, info, page_url, Contract_Status;//orders_operation = ""
        int curpage = 0;
        date_start = tools.CheckStr(Request["date_start"]);
        date_end = tools.CheckStr(Request["date_end"]);
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        curpage = tools.CheckInt(Request["page"]);
        int contract_type = tools.CheckInt(Request["contract_type"]);
        if (curpage < 1)
        {
            curpage = 1;
        }
        page_url = "?contract_sn=" + contract_sn + "&date_start=" + date_start + "&date_end=" + date_end + "&type=" + type + "&contract_type=" + contract_type;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        if (listtype == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_BuyerID", "=", supplier_id.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_SupplierID", "=", supplier_id.ToString()));
        }
        switch (type)
        {
            case "temp":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "0"));
                break;
            case "processing":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "1"));
                break;
            case "success":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "2"));
                break;
            case "faiture":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Status", "=", "3"));
                break;
        }
        if (contract_type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_Type", "=", contract_type.ToString()));
        }
        if (contract_sn != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ContractInfo.Contract_SN", "like", contract_sn));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ContractInfo.Contract_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ContractInfo.Contract_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("ContractInfo.Contract_ID", "Desc"));
        IList<ContractInfo> entitys = MyContract.GetContracts(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        PageInfo page = MyContract.GetPageInfo(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));



        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"zkw_19_fox\">");

        sb.Append("<div class=\"blk04_sz\">");

        sb.Append("<form name=\"datescope\" method=\"post\" action = \"?type=" + type + "\">");

        sb.Append("  起始日期：<input type=\"text\" class=\"input_calendar\" name=\"date_start\" id=\"date_start\" maxlength=\"10\" readonly=\"readonly\" value=\"" + date_start + "\" />");
        sb.Append("<script>$(function() {$(\"#date_start\").datepicker({numberOfMonths:1});})</script>");
        sb.Append(" 终止日期：<input type=\"text\" class=\"input_calendar\" name=\"date_end\" id=\"date_end\" maxlength=\"10\" readonly=\"readonly\" value=\"" + date_end + "\" />");
        sb.Append("<script>$(function() {$(\"#date_end\").datepicker({numberOfMonths:1});})</script>");
        sb.Append(" 合同类型：<select name=\"contract_type\"> ");
        sb.Append("<option value=\"0\">全部合同</option> ");
        sb.Append("<option value=\"1\" " + pub.CheckSelect(contract_type.ToString(), "1") + ">现货采购合同</option> ");
        sb.Append("<option value=\"2\" " + pub.CheckSelect(contract_type.ToString(), "2") + ">定制采购合同</option> ");
        sb.Append("<option value=\"3\" " + pub.CheckSelect(contract_type.ToString(), "3") + ">代理采购合同</option> ");
        sb.Append("</select> ");
        sb.Append(" 合同编号：<input type=\"text\" name=\"contract_sn\" value=\"" + contract_sn + "\" size=\"30\" /> <input name=\"search\" type=\"submit\" class=\"input10\" id=\"search\" value=\"\" />");
        sb.Append("<input name=\"type\" type=\"hidden\" class=\"input10\" id=\"type\" value=\"" + type + "\" /></form>");
        sb.Append("</div>");


        tmp_head = "";

        tmp_head = tmp_head + " <tr style=\"background:url(/images/ping.jpg); height:30px;\">";
        tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" valign=\"middle\">合同类型</th>";
        tmp_head = tmp_head + "   <td height=\"30\"  align=\"center\" >合同编号</th>";
        tmp_head = tmp_head + "   <td width=\"100\" align=\"center\" valign=\"middle\">合同总价</th>";
        tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" valign=\"middle\">合同状态</th>";
        tmp_head = tmp_head + "   <td valign=\"middle\" width=\"90\" align=\"center\">支付状态</th>";
        tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" valign=\"middle\">发货状态</th>";
        tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" valign=\"middle\">生成时间</th>";
        tmp_head = tmp_head + "   <td width=\"90\" align=\"center\" valign=\"middle\">操作</th>";
        tmp_head = tmp_head + " </tr>";


        form_action = "?type=" + type;

        sb.Append("<div class=\"b14_1_main\">");

        if (entitys != null)
        {

            sb.Append("<table width=\"100%\" class=\"table02\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
            sb.Append(tmp_head);
            foreach (ContractInfo entity in entitys)
            {
                Contract_Status = ContractStatus(entity.Contract_Status);
                sb.Append("<tr>");
                sb.Append("<td align=\"center\">" + ContractType(entity.Contract_Type) + "</td>");
                sb.Append("<td height=\"17\" align=\"center\">");
                if (listtype == 0)
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/contract_detail.aspx?contract_sn=" + entity.Contract_SN + "\">" + entity.Contract_SN + "</a>");
                }
                else
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/mycontract_detail.aspx?contract_sn=" + entity.Contract_SN + "\">" + entity.Contract_SN + "</a>");
                }
                sb.Append("</td>");

                sb.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Contract_AllPrice) + "</td>");
                sb.Append("<td align=\"center\">" + Contract_Status + "</td>");
                sb.Append("<td align=\"center\">" + ContractPaymentStatus(entity.Contract_Payment_Status) + "</td>");
                sb.Append("<td align=\"center\">" + ContractDeliveryStatus(entity.Contract_Delivery_Status) + "</td>");
                sb.Append("<td align=\"center\">" + entity.Contract_Addtime.ToShortDateString() + "</td>");
                sb.Append("<td height=\"17\" align=\"center\">");
                if (listtype == 0)
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/contract_detail.aspx?contract_sn=" + entity.Contract_SN + "\">查看</a>");
                    if (entity.Contract_IsEvaluate == 0 && entity.Contract_Status == 2)
                    {
                        sb.Append(" <a class=\"a_t12_blue\" href=\"/supplier/contract_evaluate.aspx?contract_sn=" + entity.Contract_SN + "\">评价</a>");
                    }
                }
                else
                {
                    sb.Append("<a class=\"a_t12_blue\" href=\"/supplier/mycontract_detail.aspx?contract_sn=" + entity.Contract_SN + "\">查看</a>");
                }
                sb.Append("</td>");
                sb.Append("</tr>");

            }

            sb.Append("</table>");
            Response.Write(sb.ToString());
            pub.Page(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
        }
        else
        {
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
            sb.Append(tmp_head);
            sb.Append("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"8\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
        }

        Response.Write("</div>");
        Response.Write("</div>");

    }

    //查看合同
    public void Contract_Detail(ContractInfo entity)
    {
        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
        StringBuilder HTML_Str = new StringBuilder();
        HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("  <tr>");
        HTML_Str.Append("    <td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">合同编号</td>");
        HTML_Str.Append("        <td class=\"t14_red\">" + entity.Contract_SN + "&nbsp;&nbsp;&nbsp;&nbsp;");
        if (entity.Contract_Status == 0)
        {
            ContractInvoiceInfo CIEntity = MyContract.GetContractInvoiceByContractID(entity.Contract_ID);

            //合同取消
            if (ordersinfos == null && tools.NullInt(Session["supplier_id"]) == entity.Contract_BuyerID && entity.Contract_Source == 0)
            {
                HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"取消\" onclick=\"location.href='contract_close.aspx?contract_sn=" + entity.Contract_SN + "'\"/>");
            }
            //买方确认
            if (ordersinfos != null && tools.NullInt(Session["supplier_id"]) == entity.Contract_BuyerID && entity.Contract_Confirm_Status != 1)
            {
                HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"合同确认\" onclick=\"location.href='contract_do.aspx?action=buyer_confirm&contract_sn=" + entity.Contract_SN + "'\"/>");
            }
            //卖方确认
            if (ordersinfos != null && tools.NullInt(Session["supplier_id"]) == entity.Contract_SupplierID && entity.Contract_Confirm_Status != 2)
            {
                HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"合同确认\" onclick=\"location.href='contract_do.aspx?action=seller_confirm&contract_sn=" + entity.Contract_SN + "'\"/>");
            }
        }
        else if (entity.Contract_Status == 1)
        {
            //配货操作
            if (entity.Contract_Delivery_Status == 0 && tools.NullInt(Session["supplier_id"]) == entity.Contract_SupplierID)
            {
                HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"配货中\" onclick=\"location.href='contract_do.aspx?action=prepare&contract_sn=" + entity.Contract_SN + "'\"/>");
            }
            //发货操作
            if ((entity.Contract_Delivery_Status == 1 || entity.Contract_Delivery_Status == 2) && tools.NullInt(Session["supplier_id"]) == entity.Contract_SupplierID)
            {
                HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"发货\" onclick=\"location.href='contract_freight.aspx?contract_sn=" + entity.Contract_SN + "'\"/>");
            }
            //支付操作
            if ((entity.Contract_Payment_Status == 0 || entity.Contract_Payment_Status == 1))
            {
                HTML_Str.Append("   <input name=\"btn_pay\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"支付\" onclick=\"location.href='contract_pay.aspx?contract_sn=" + entity.Contract_SN + "'\"/>");
            }
            //交易完成操作
            if ((entity.Contract_Payment_Status == 3 && entity.Contract_Delivery_Status == 4) && tools.NullInt(Session["supplier_id"]) == entity.Contract_SupplierID)
            {
                HTML_Str.Append("   <input name=\"btn_pay\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"交易完成\" onclick=\"location.href='contract_do.aspx?action=contract_success&contract_sn=" + entity.Contract_SN + "'\"/>");
            }
        }

        //评价
        if (entity.Contract_Status == 2 && entity.Contract_IsEvaluate == 0 && tools.NullInt(Session["supplier_id"]) == entity.Contract_BuyerID)
        {
            HTML_Str.Append("   <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"合同评价\" onclick=\"location.href='contract_evaluate.aspx?contract_sn=" + entity.Contract_SN + "'\"/>");
        }
        if (entity.Contract_Status < 3)
        {
            HTML_Str.Append(" <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"合同预览\" onclick=\"window.open('contract_view.aspx?contract_sn=" + entity.Contract_SN + "')\"/>");
            HTML_Str.Append(" <input name=\"btn_delivery\" type=\"button\" class=\"buttonupload\" id=\"btn_delivery\" value=\"合同打印\" onclick=\"window.open('contract_view.aspx?action=print&contract_sn=" + entity.Contract_SN + "')\"/> ");
        }
        HTML_Str.Append("</td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">合同类型</td>");
        HTML_Str.Append("        <td class=\"t14\">" + ContractType(entity.Contract_Type) + "</td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">合同状态</td>");
        HTML_Str.Append("        <td class=\"t14\">" + ContractStatus(entity.Contract_Status) + "</td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">合同总价</td>");
        HTML_Str.Append("        <td class=\"t14\">" + pub.FormatCurrency(entity.Contract_AllPrice) + "</td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">付款状态</td>");
        HTML_Str.Append("        <td class=\"t14\">" + ContractPaymentStatus(entity.Contract_Payment_Status) + "</td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">配送状态</td>");
        HTML_Str.Append("        <td class=\"t14\">" + ContractDeliveryStatus(entity.Contract_Delivery_Status) + "</td>");
        HTML_Str.Append("      </tr>");

        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
        HTML_Str.Append("      </tr>");
        HTML_Str.Append("      <tr>");
        HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">生成时间</td>");
        HTML_Str.Append("        <td class=\"t14\">" + entity.Contract_Addtime.ToString("yyyy-MM-dd hh:mm:ss") + "</td>");
        HTML_Str.Append("      </tr>");

        #region 修改费用

        if (entity.Contract_Status == 0 && tools.NullInt(Session["supplier_id"]) == entity.Contract_SupplierID)
        {
            HTML_Str.Append("    <form name=\"frm_edit\" action=\"/supplier/contract_do.aspx\" method=\"post\">");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td align=\"center\" width=\"100\" class=\"t14_red\">合同费用</td>");
            HTML_Str.Append("        <td>订单总价：" + pub.FormatCurrency(entity.Contract_Price) + "&nbsp; ");
            HTML_Str.Append("代理服务费用：" + pub.FormatCurrency(entity.Contract_ServiceFee) + "&nbsp; ");
            HTML_Str.Append("运费金额：<input type=\"text\" name=\"Contract_Freight\" value=\"" + entity.Contract_Freight + "\"> &nbsp; ");
            HTML_Str.Append("优惠金额：<input type=\"text\" name=\"Contract_Discount\" value=\"" + entity.Contract_Discount + "\"> &nbsp; ");
            HTML_Str.Append(" <input name=\"btn_ordernote\" type=\"submit\" class=\"buttonupload\" id=\"btn_ordernote\" value=\"保存\"\"/>");
            HTML_Str.Append("    <input type=\"hidden\" name=\"contract_sn\" value=\"" + entity.Contract_SN + "\"></td>");
            HTML_Str.Append("    <input type=\"hidden\" name=\"action\" value=\"contractfeeedit\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("    </form>");
        }
        else
        {
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td align=\"center\" width=\"100\" class=\"t14_red\">合同费用</td>");
            HTML_Str.Append("        <td>订单总价：" + pub.FormatCurrency(entity.Contract_Price) + "&nbsp; ");
            HTML_Str.Append("代理服务费用：" + pub.FormatCurrency(entity.Contract_ServiceFee) + "&nbsp; ");
            HTML_Str.Append("运费金额：" + pub.FormatCurrency(entity.Contract_Freight) + " &nbsp; ");
            HTML_Str.Append("优惠金额：" + pub.FormatCurrency(entity.Contract_Discount) + " &nbsp; ");
            HTML_Str.Append("      </tr>");
        }

        #endregion


        if (entity.Contract_Status < 3)
        {
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">附件订单</td>");
            HTML_Str.Append("        <td class=\"t14\">" + Contract_Orders(ordersinfos, entity.Contract_Status, entity.Contract_Confirm_Status, "detail", entity.Contract_Source) + "</td>");
            HTML_Str.Append("      </tr>");
        }

        if (entity.Contract_Status > 0 && entity.Contract_Status < 3)
        {
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">付款单据</td>");
            HTML_Str.Append("        <td class=\"t14\">" + Contract_Payments(entity) + "</td>");
            HTML_Str.Append("      </tr>");

            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">配送单据</td>");
            HTML_Str.Append("        <td class=\"t14\">" + Contract_Deliverys(entity.Contract_ID, entity.Contract_SN) + "</td>");
            HTML_Str.Append("      </tr>");

            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td width=\"100\" align=\"center\" valign=\"middle\" class=\"t14_red\">发票申请</td>");
            HTML_Str.Append("        <td class=\"t14\">" + Contract_InvoiceApply_List(entity.Contract_ID, entity.Contract_SN, entity.Contract_SupplierID) + "</td>");
            HTML_Str.Append("      </tr>");
        }
        if (entity.Contract_Status == 0)
        {
            HTML_Str.Append("    <form name=\"frm_intent\" action=\"/supplier/contract_do.aspx\" method=\"post\">");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td align=\"center\" width=\"100\" class=\"t14_red\">修改意向</td>");
            HTML_Str.Append("        <td><table width=\"100%\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tr><td width=\"50\">内容：</td><td><textarea cols=\"40\" rows=\"5\" name=\"contract_note\"></textarea> <span class=\"t12_grey\"> 建议不超过500个字符</span></td></tr>");


            HTML_Str.Append("<tr><td>附件：</td><td><iframe id=\"iframe_upload\" src=\"" + Application["Upload_Server_URL"] + "/public/FileUpload.aspx?App=attachment&formname=frm_intent&frmelement=attachment_file&rtvalue=1&rturl=" + Application["Upload_Server_Return_WWW"] + "\" width=\"300\" height=\"22\" frameborder=\"0\" scrolling=\"no\"></iframe></td></tr>");

            HTML_Str.Append("<tr><td></td><td height=\"30\"><input name=\"btn_ordernote\" type=\"submit\" class=\"buttonupload\" id=\"btn_ordernote\" value=\"提交\"/>");

            HTML_Str.Append("<input type=\"hidden\" name=\"attachment_file\" id=\"attachment_file\" /></td></tr></table>");

            HTML_Str.Append("    <input type=\"hidden\" name=\"contract_sn\" value=\"" + entity.Contract_SN + "\"><input type=\"hidden\" name=\"action\" value=\"contract_noteedit\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("    </form>");
        }
        if (entity.Contract_Status == 1)
        {
            HTML_Str.Append("    <form name=\"frm_edit\" action=\"/supplier/contract_do.aspx\" method=\"post\">");
            HTML_Str.Append("      <tr>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("        <td height=\"10\" class=\"dotline_h\"></td>");
            HTML_Str.Append("      </tr>");
            HTML_Str.Append("      <tr>");

            HTML_Str.Append("        <td align=\"center\" width=\"100\" class=\"t14_red\">意见回复</td>");
            HTML_Str.Append("        <td><table width=\"100%\" border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tr><td width=\"50\">内容：</td><td><textarea cols=\"40\" rows=\"5\" name=\"contract_note\"></textarea> <span class=\"t12_grey\"> 建议不超过500个字符</span></td></tr>");


            HTML_Str.Append("<tr><td>附件：</td><td><iframe id=\"iframe_upload\" src=\"" + Application["Upload_Server_URL"] + "/public/uploadify.aspx?App=attachment&formname=frm_intent&frmelement=attachment_file&rtvalue=1&rturl=" + Application["Upload_Server_Return_WWW"] + "\" width=\"300\" height=\"22\" frameborder=\"0\" scrolling=\"no\"></iframe></td></tr>");

            HTML_Str.Append("<tr><td></td><td height=\"30\"><input name=\"btn_ordernote\" type=\"submit\" class=\"buttonupload\" id=\"btn_ordernote\" value=\"提交\"/>");

            HTML_Str.Append("<input type=\"hidden\" name=\"attachment_file\" id=\"attachment_file\" /></td></tr></table>");

            HTML_Str.Append("    <input type=\"hidden\" name=\"contract_sn\" value=\"" + entity.Contract_SN + "\"><input type=\"hidden\" name=\"action\" value=\"contract_noteedit\"></td>");
            HTML_Str.Append("      </tr>");


            HTML_Str.Append("    </form>");
        }


        HTML_Str.Append("    </table></td>");
        HTML_Str.Append("  </tr>");
        HTML_Str.Append("  <tr>");
        HTML_Str.Append("    <td height=\"20\"></td>");
        HTML_Str.Append("  </tr>");
        HTML_Str.Append("</table>");
        Response.Write(HTML_Str.ToString());
    }

    //合同付款单
    public string Contract_Payments(ContractInfo contractinfo)
    {
        StringBuilder HTML_Str = new StringBuilder();

        IList<ContractPaymentInfo> entitys = MyPayment.GetContractPaymentsByContractID(contractinfo.Contract_ID);

        HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
        if (entitys != null)
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
            HTML_Str.Append("<td align=\"center\" height=\"25\"><b>付款单号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付金额</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付方式</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付备注</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付时间</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付状态</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付凭据</b></td>");
            if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                HTML_Str.Append("<td align=\"center\"><b>操作</b></td>");
            }
            HTML_Str.Append("</tr>");
            foreach (ContractPaymentInfo entity in entitys)
            {
                //if (entity.Contract_Payment_PaymentStatus > 0)
                //{ 
                //}

                HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td align=\"center\" height=\"25\">" + entity.Contract_Payment_DocNo + "</td><td align=\"center\">" + pub.FormatCurrency(entity.Contract_Payment_Amount) + "</td><td align=\"center\">" + entity.Contract_Payment_Name + "</td><td align=\"center\">" + entity.Contract_Payment_Note + "</td><td align=\"center\">" + entity.Contract_Payment_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\">" + ContractPaymentsStatus(entity.Contract_Payment_PaymentStatus) + "</td>");
                if (entity.Contract_Payment_Attachment.Length > 0)
                    HTML_Str.Append("<td align=\"center\"><a href=\"" + Convert.ToString(Application["Upload_Server_URL"]).TrimEnd('/') + entity.Contract_Payment_Attachment + "\" target=\"_blank\">查看</a></td>");
                else
                    HTML_Str.Append("<td align=\"center\"></td>");

                if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                {
                    if (entity.Contract_Payment_PaymentStatus == 1)
                    {
                        HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"buttonupload\" value=\"已到帐\" onclick=\"location='contract_do.aspx?action=contract_payreach&Contract_SN=" + contractinfo.Contract_SN + "&Payment_SN=" + entity.Contract_Payment_DocNo + "';\"></td>");
                    }
                    else if (entity.Contract_Payment_PaymentStatus == 0)
                    {
                        HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"buttonupload\" value=\"已付款\" onclick=\"location='contract_do.aspx?action=contract_paid&Contract_SN=" + contractinfo.Contract_SN + "&Payment_SN=" + entity.Contract_Payment_DocNo + "';\"></td>");
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
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无付款信息</td></tr>");
        }
        HTML_Str.Append("</table>");
        return HTML_Str.ToString();
    }

    //合同配送单
    public string Contract_Deliverys(int Contract_ID, string Contract_SN)
    {
        StringBuilder HTML_Str = new StringBuilder();
        IList<ContractDeliveryInfo> entitys = MyFreight.GetContractDeliverysByContractID(Contract_ID);

        HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
        if (entitys != null)
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
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
                HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td align=\"center\" height=\"25\">" + entity.Contract_Delivery_DocNo + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_CompanyName + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Name + "</td>");
                HTML_Str.Append("<td align=\"center\">" + ContractDeliverysStatus(entity.Contract_Delivery_DeliveryStatus) + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Code + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Contract_Delivery_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\"><input name=\"btn_print\" type=\"button\" class=\"buttonupload\" value=\"查看\" onclick=\"location='/supplier/contract_freight_view.aspx?Contract_SN=" + Contract_SN + "&contract_delivery=" + entity.Contract_Delivery_DocNo + "';\">");
                //if (entity.Contract_Delivery_DeliveryStatus == 1)
                //{
                //    HTML_Str.Append(" <input name=\"btn_print\" type=\"button\" class=\"buttonupload\" value=\"签收\" onclick=\"location='contract_do.aspx?action=contract_accept&Contract_SN=" + Contract_SN + "&contract_delivery=" + entity.Contract_Delivery_DocNo + "';\">");
                //}
                HTML_Str.Append("</td>");

                HTML_Str.Append("</tr>");
            }

        }
        else
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无配送信息</td></tr>");
        }
        HTML_Str.Append("</table>");
        return HTML_Str.ToString();
    }

    //合同发票申请
    public string Contract_InvoiceApply_List(int Contract_ID, string Contract_Sn, int Contract_SupplierID)
    {
        StringBuilder HTML_Str = new StringBuilder();

        IList<ContractInvoiceApplyInfo> entitys = MyContract.GetContractInvoiceApplysByContractID(Contract_ID);

        HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
        if (entitys != null)
        {

            HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
            HTML_Str.Append("<td align=\"center\" height=\"25\"><b>编号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>申请金额</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>开票金额</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>备注</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>状态</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>申请/开具时间</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>操作</b></td>");
            HTML_Str.Append("</tr>");
            foreach (ContractInvoiceApplyInfo entity in entitys)
            {
                HTML_Str.Append("<form name=\"frm_" + entity.Invoice_Apply_ID + "\" method=\"post\" action=\"contract_do.aspx\">");
                HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
                HTML_Str.Append("<td align=\"center\" height=\"25\">" + entity.Invoice_Apply_ID + "</td>");
                HTML_Str.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Invoice_Apply_ApplyAmount) + "</td>");

                if (entity.Invoice_Apply_Status == 0 && Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                {
                    HTML_Str.Append("<td align=\"center\"><input type=\"text\" name=\"invoice_amount\" size=\"15\"></td>");
                    HTML_Str.Append("<td align=\"center\"><input type=\"text\" name=\"apply_note\" size=\"30\"></td>");
                }
                else
                {
                    HTML_Str.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Invoice_Apply_Amount) + "</td>");
                    HTML_Str.Append("<td align=\"center\">" + entity.Invoice_Apply_Note + "</td>");
                }
                HTML_Str.Append("<td align=\"center\">" + ContractInvoiceApplyStatus(entity.Invoice_Apply_Status) + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Invoice_Apply_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\">");
                if (entity.Invoice_Apply_Status == 0 && Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                {
                    HTML_Str.Append("<input name=\"btn_print\" type=\"submit\" class=\"buttonupload\" value=\"开票\" onclick=\"$('#action" + entity.Invoice_Apply_ID + "').val('apply_open');\"> <input name=\"btn_print\" type=\"submit\" class=\"buttonupload\" value=\"取消\" onclick=\"$('#action" + entity.Invoice_Apply_ID + "').val('apply_cancel');\"><input type=\"hidden\" id=\"action" + entity.Invoice_Apply_ID + "\" name=\"action\" value=\"apply_cancel\"><input type=\"hidden\" name=\"contract_sn\" value=\"" + Contract_Sn + "\"><input type=\"hidden\" name=\"apply_id\" value=\"" + entity.Invoice_Apply_ID + "\">");
                }
                if (entity.Invoice_Apply_Status == 1 && Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                {
                    HTML_Str.Append("<input name=\"btn_print\" type=\"button\" class=\"buttonupload\" onclick=\"location='contract_do.aspx?action=apply_send&contract_sn=" + Contract_Sn + "&apply_id=" + entity.Invoice_Apply_ID + "';\" class=\"btn_01\" value=\"已邮寄\">");
                }
                if (entity.Invoice_Apply_Status == 2)
                {
                    HTML_Str.Append("<input name=\"btn_print\" type=\"button\" class=\"buttonupload\" onclick=\"location='contract_do.aspx?action=apply_accept&contract_sn=" + Contract_Sn + "&apply_id=" + entity.Invoice_Apply_ID + "';\" class=\"btn_01\" value=\"已收票\">");
                }
                HTML_Str.Append("</td>");
                HTML_Str.Append("</tr>");
                HTML_Str.Append("</form>");
            }

        }
        else
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无开票申请信息</td></tr>");
        }
        HTML_Str.Append("</table>");
        return HTML_Str.ToString();
    }

    #region 意向合同处理
    //合同附加订单
    public string Contract_Orders(IList<OrdersInfo> Orders, int Contract_Status, int Confirm_Status, string use, int Contract_Source)
    {
        string HTML_Str = "";
        double Goods_Sum, Goods_Amount;
        string order_url = "/supplier/order_detail.aspx?orders_sn=";

        IList<OrdersGoodsInfo> GoodsListAll = null;
        if (use == "detail")
        {
            HTML_Str = HTML_Str + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">";
            if (Orders != null)
            {
                HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\"><td width=\"15%\" height=\"25\" align=\"center\"><b>订单编号</b></td><td width=\"20%\" align=\"center\"><b>商品种类</b></td><td width=\"15%\" align=\"center\"><b>数量</b></td><td width=\"13%\" align=\"center\"><b>订单金额</b></td><td width=\"25%\" align=\"center\"><b>下单时间</b></td><td width=\"12%\" align=\"center\"><b>操作</b></td></tr>";
                foreach (OrdersInfo entity in Orders)
                {
                    if (tools.NullInt(Session["supplier_id"]) == entity.Orders_BuyerID)
                    {
                        order_url = "/supplier/order_view.aspx?orders_sn=";
                    }

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

                    HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\"><td width=\"15%\" height=\"25\" align=\"center\"><a href=\"" + order_url + entity.Orders_SN + "\" class=\"a_t12_blue\" target=\"_blank\">" + entity.Orders_SN + "</a></td><td width=\"20%\" align=\"center\">" + Goods_Amount + "</td><td width=\"15%\" align=\"center\">" + Goods_Sum + "</td><td width=\"13%\" align=\"center\">" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</td><td width=\"25%\" align=\"center\">" + entity.Orders_Addtime + "</td>";
                    //意向合同未确认，且为买方创建的合同
                    if (Contract_Status == 0 && Confirm_Status == 0 && tools.NullInt(Session["supplier_id"]) == entity.Orders_BuyerID && Contract_Source == 0)
                    {
                        HTML_Str = HTML_Str + "<td width=\"12%\" align=\"center\"><a href=\"/supplier/contract_do.aspx?action=remove_order&orders_sn=" + entity.Orders_SN + "\" class=\"a_t12_blue\">移除</a></td>";
                    }
                    else
                    {
                        HTML_Str = HTML_Str + "<td width=\"12%\" align=\"center\"></td>";
                    }
                    HTML_Str = HTML_Str + "</tr>";
                }

            }
            else
            {
                HTML_Str = HTML_Str + "<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 您还没有添加订单信息 <a href=\"order_list.aspx\" class=\"a_t12_blue\">点此添加</a></td></tr>";
            }
            HTML_Str = HTML_Str + "</table>";
        }
        else
        {

        }
        return HTML_Str;
    }

    //获取合同发票信息
    public string Contract_Invoice(int Contract_ID, string Contract_SN, int Contract_Status, int Confirm_Status, int Contract_SupplierID, double Contract_Amount)
    {
        StringBuilder HTML_Str = new StringBuilder();
        ContractInvoiceInfo entity = MyContract.GetContractInvoiceByContractID(Contract_ID);
        if (entity != null)
        {
            HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"0\">");
            HTML_Str.Append("<tr><td colspan=\"2\">");

            if (Contract_Status == 0 && Confirm_Status == 0)
            {
                HTML_Str.Append(" <input type=\"button\" class=\"buttonupload\" value=\"重新选择发票\" onclick=\"location='contract_invoice_apply.aspx?Contract_SN=" + Contract_SN + "';\"/>  ");
                int supplier_id = tools.NullInt(Session["supplier_id"]);
                IList<SupplierInvoiceInfo> entitys = MyInvoice.GetSupplierInvoicesBySupplierID(supplier_id);
                if (entitys == null)
                {
                    HTML_Str.Append("&nbsp;&nbsp;&nbsp;您尚未维护发票信息，<a target=\"_blank\" class=\"a_t12_blue\" href=\"/supplier/account_invoice_add.aspx\">点此设置</a>");
                }
                HTML_Str.Append(" <span style=\"color:#ff0000\">必选，确认合同后发票信息不可修改</span>");
            }
            //if (Contract_Status > 0 && entity.Invoice_Status < 2 && Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
            //{
            //    HTML_Str = HTML_Str.Append("发票申请：");

            //    if (entity.Invoice_Status == 0)
            //    {
            //        HTML_Str.Append("<input type=\"button\" id=\"Button10\" class=\"buttonupload\" value=\"启用发票申请\" onclick=\"location.href='Contract_do.aspx?action=invoicestatus&invoice_id=" + entity.Invoice_ID + "&contract_sn=" + Contract_SN + "'\" />");
            //    }
            //    else if (entity.Invoice_Status == 1)
            //    {
            //        HTML_Str.Append("<input type=\"button\" id=\"Button10\" class=\"buttonupload\" value=\"关闭发票申请\" onclick=\"location.href='Contract_do.aspx?action=invoicestatus&invoice_id=" + entity.Invoice_ID + "&contract_sn=" + Contract_SN + "'\" />");
            //    }
            //}

            if (Contract_Status > 0 && entity.Invoice_Status == 1 && Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && Get_Contract_InvoiceAmount(Contract_ID) < Contract_Amount)
            {
                HTML_Str.Append("<input type=\"button\" id=\"Button10\" class=\"buttonupload\" value=\"申请开票\" onclick=\"location.href='contract_invoiceapply.aspx?contract_sn=" + Contract_SN + "'\" />");
            }
            HTML_Str.Append("</td></tr>");
            if (entity.Invoice_Type == 0)
            {
                if (entity.Invoice_Title == "单位")
                {
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">发票类型：普通发票</td><td>发票抬头：" + entity.Invoice_Title + "</td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\" colspan=\"2\">单位名称：" + entity.Invoice_FirmName + "</td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">单位地址：" + entity.Invoice_VAT_RegAddr + "</td><td>单位电话：" + entity.Invoice_VAT_RegTel + "</td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append(" <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">税号：" + entity.Invoice_VAT_Code + "</td><td>发票内容：明细</td>");
                    HTML_Str.Append("  </tr>");
                }
                else
                {
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">发票类型：普通发票</td><td></td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">发票抬头：" + entity.Invoice_Title + "</td><td>发票内容：明细</td>");
                    HTML_Str.Append("  </tr>");
                    HTML_Str.Append("  <tr>");
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">姓名：" + entity.Invoice_PersonelName + "</td><td>身份证号：" + entity.Invoice_PersonelCard + "</td>");
                    HTML_Str.Append("  </tr>");
                }
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                HTML_Str.Append("  </tr>");

            }
            else
            {
                HTML_Str.Append("  <tr>");
                if (entity.Invoice_VAT_Cert != "")
                {
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + tools.NullStr(Application["upload_server_url"]) + entity.Invoice_VAT_Cert + "\" class=\"a_t12_blue\" target=\"_blank\">查看纳税人资格证</a></td>");
                }
                else
                {
                    HTML_Str.Append("    <td width=\"50%\" height=\"25\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "</td>");
                }

                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">单位名称：" + entity.Invoice_VAT_FirmName + "</td><td>纳税人识别号：" + entity.Invoice_VAT_Code + "</td>");
                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">注册地址：" + entity.Invoice_VAT_RegAddr + "</td><td>注册电话：" + entity.Invoice_VAT_RegTel + "</td>");
                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                HTML_Str.Append("  </tr>");
                HTML_Str.Append("  <tr>");
                HTML_Str.Append("    <td width=\"50%\" height=\"25\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                HTML_Str.Append("  </tr>");
            }
            HTML_Str.Append("  </table>");
        }
        else
        {
            HTML_Str.Append("当前状态：未选择发票 ");
            if (Contract_Status == 0 && Confirm_Status == 0)
            {
                HTML_Str.Append(" <input type=\"button\" class=\"buttonupload\" value=\"选择发票\" onclick=\"location='contract_invoice_apply.aspx?Contract_SN=" + Contract_SN + "';\"/>  ");
                int supplier_id = tools.NullInt(Session["supplier_id"]);
                IList<SupplierInvoiceInfo> entitys = MyInvoice.GetSupplierInvoicesBySupplierID(supplier_id);
                if (entitys == null)
                {
                    HTML_Str.Append("&nbsp;&nbsp;&nbsp;您尚未维护发票信息，<a target=\"_blank\" class=\"a_t12_blue\" href=\"/supplier/account_invoice_add.aspx\">点此设置</a>");
                }
                HTML_Str.Append(" <span style=\"color:#ff0000\">必选，确认合同后发票信息不可修改</span>");
            }
        }
        return HTML_Str.ToString();
    }

    //合同分期付款
    public string Contract_Divided(int Contract_ID, string Contract_SN, int Contract_Status)
    {
        StringBuilder HTML_Str = new StringBuilder();
        int i = 0;
        StringBuilder selectstr = new StringBuilder();
        IList<ContractDividedPaymentInfo> entitys = MyDividedpay.GetContractDividedPaymentsByContractID(Contract_ID);

        selectstr.Append("<form name=\"frm_edit\" action=\"/supplier/contract_do.aspx\" method=\"post\">");
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

            HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td align=\"center\" height=\"25\"><b>分期名称</b></td><td align=\"center\"><b>付款金额</b></td><td align=\"center\"><b>计划付款时间</b></td><td align=\"center\"><b>付款条件</b></td></tr>");
            foreach (ContractDividedPaymentInfo entity in entitys)
            {

                HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
                if (Contract_Status > 0)
                {
                    HTML_Str.Append("<td align=\"center\" height=\"25\">" + entity.Payment_Name + "</td>");
                    HTML_Str.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Payment_Amount) + "</td>");
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
                HTML_Str.Append("<center style=\"margin-top:10px;\"><input name=\"button\" type=\"submit\" class=\"buttonSkinA\" id=\"button\" value=\"提交分期\" /></center>");
                HTML_Str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"Contract_dividedSave\" />");
                HTML_Str.Append("<input name=\"Contract_sn\" type=\"hidden\" id=\"Contract_sn\" value=\"" + Contract_SN + "\" />");
                HTML_Str.Append("</form>");
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
            HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无分期信息</td></tr>");
            HTML_Str.Append("</table>");
            if (Contract_Status == 0)
            {
                HTML_Str.Append("<center style=\"margin-top:10px;\"><input name=\"button\" type=\"submit\" class=\"buttonSkinA\" id=\"button\" value=\"提交分期\" /></center>");
                HTML_Str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"Contract_dividedSave\" />");
                HTML_Str.Append("<input name=\"Contract_sn\" type=\"hidden\" id=\"Contract_sn\" value=\"" + Contract_SN + "\" />");
                HTML_Str.Append("</form>");
            }
        }

        return HTML_Str.ToString();
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
            pub.Msg("error", "错误信息", "无效的订单号！", false, "{back}");
        }
        if (contract_id == 0)
        {
            pub.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }

        //订单检验
        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(orders_sn);
        if (ordersinfo != null)
        {
            if ((ordersinfo.Orders_BuyerID != supplier_id || ordersinfo.Orders_BuyerType != 1) || ordersinfo.Orders_PaymentStatus > 0 || ordersinfo.Orders_DeliveryStatus > 0 || ordersinfo.Orders_Status != 1)
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
            pub.Msg("error", "错误信息", "订单添加失败！", false, "{back}");
        }

        if (ordersinfo.Orders_ContractID > 0)
        {
            pub.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "{back}");
        }


        ContractInfo entity = MyContract.GetContractByID(contract_id, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        if (entity != null)
        {
            IList<OrdersInfo> ordersinfos = GetOrderssByContractID(entity.Contract_ID);
            if (ordersinfos != null && entity.Contract_SupplierID != ordersinfo.Orders_SupplierID)
            {
                pub.Msg("error", "错误信息", "同一合同不可附加不同卖家的订单！", false, "{back}");
            }
            if (entity.Contract_BuyerID != supplier_id || entity.Contract_Source != 0 || entity.Contract_Status > 0 || entity.Contract_Confirm_Status > 0)
            {
                pub.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
            }
            else
            {
                if (ordersinfo.Orders_Type != entity.Contract_Type)
                {
                    pub.Msg("error", "错误信息", "合同与订单类型不一致！", false, "{back}");
                }
                Contract_SN = entity.Contract_SN;
                //添加订单到意向合同中
                ordersinfo.Orders_ContractID = entity.Contract_ID;
                if (MyOrders.EditOrders(ordersinfo))
                {
                    //更新意向合同价格

                    entity.Contract_Price += ordersinfo.Orders_Total_AllPrice;
                    //代理采购合同
                    if (entity.Contract_Type == 3)
                    {
                        SupplierInfo supplierinfo = GetSupplierByID(ordersinfo.Orders_SupplierID);
                        if (supplierinfo != null)
                        {
                            //更新代理费用
                            entity.Contract_ServiceFee = Math.Round((entity.Contract_Price * supplierinfo.Supplier_AgentRate) / 100, 2);
                        }
                    }
                    entity.Contract_AllPrice = entity.Contract_Price + entity.Contract_ServiceFee + entity.Contract_Freight - entity.Contract_Discount;
                    entity.Contract_SupplierID = ordersinfo.Orders_SupplierID;
                    entity.Contract_Payway_ID = ordersinfo.Orders_Payway;
                    entity.Contract_Payway_Name = ordersinfo.Orders_Payway_Name;
                    MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));

                    //添加订单到意向合同日志添加
                    Contract_Log_Add(entity.Contract_ID, "", "添加订单到意向合同", 1, "添加订单到本意向合同，添加订单号：" + Orders_SN);
                    pub.Msg("positive", "操作成功", "订单已成功添加至新建意向合同[" + Contract_SN + "]中！", false, "/supplier/contract_detail.aspx?Contract_SN=" + Contract_SN);
                }
                else
                {
                    Contract_Log_Add(entity.Contract_ID, "", "添加订单到意向合同", 0, "添加订单到本意向合同，添加订单号：" + Orders_SN);
                    pub.Msg("error", "错误信息", "订单添加至新建意向合同失败！", false, "/supplier/contract_detail.aspx?Contract_SN=" + Contract_SN);
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }
    }

    //合同选择发票信息
    public void Contract_Invoice_Select()
    {
        string Contract_Sn;
        int Invoice_ID;
        Contract_Sn = tools.CheckStr(Request["Contract_Sn"]);
        Invoice_ID = tools.CheckInt(Request["Invoice_ID"]);
        if (Contract_Sn == "" || Invoice_ID == 0)
        {
            pub.Msg("error", "错误信息", "合同不支持此操作", false, "/supplier/Contract_list.aspx");
        }

        ContractInfo entity = GetContractBySn(Contract_Sn);
        if (entity != null)
        {
            if (entity.Contract_BuyerID != tools.CheckInt(Session["supplier_id"].ToString()) || entity.Contract_Status != 0 || entity.Contract_Confirm_Status != 0)
            {
                pub.Msg("error", "错误信息", "合同不支持此操作", false, "/supplier/Contract_list.aspx");
            }
            else
            {
                SupplierInvoiceInfo invoiceinfo = GetSupplierInvoiceByID(Invoice_ID);
                if (invoiceinfo != null)
                {
                    if (invoiceinfo.Invoice_SupplierID != tools.NullInt(Session["supplier_id"]))
                    {
                        pub.Msg("error", "错误信息", "无效的发票信息", false, "/supplier/Contract_list.aspx");
                    }
                    else
                    {

                        ContractInvoiceInfo contractinvoice = MyContract.GetContractInvoiceByContractID(entity.Contract_ID);
                        if (contractinvoice != null)
                        {
                            contractinvoice.Invoice_Name = invoiceinfo.Invoice_Name;
                            contractinvoice.Invoice_Type = invoiceinfo.Invoice_Type;
                            contractinvoice.Invoice_Title = invoiceinfo.Invoice_Title;
                            contractinvoice.Invoice_Content = invoiceinfo.Invoice_Content;
                            contractinvoice.Invoice_FirmName = invoiceinfo.Invoice_FirmName;
                            contractinvoice.Invoice_VAT_Code = invoiceinfo.Invoice_VAT_Code;
                            contractinvoice.Invoice_VAT_FirmName = invoiceinfo.Invoice_VAT_FirmName;
                            contractinvoice.Invoice_VAT_RegAddr = invoiceinfo.Invoice_VAT_RegAddr;
                            contractinvoice.Invoice_VAT_RegTel = invoiceinfo.Invoice_VAT_RegTel;
                            contractinvoice.Invoice_VAT_Bank = invoiceinfo.Invoice_VAT_Bank;
                            contractinvoice.Invoice_VAT_BankAccount = invoiceinfo.Invoice_VAT_BankAccount;
                            contractinvoice.Invoice_VAT_Content = invoiceinfo.Invoice_VAT_Content;
                            contractinvoice.Invoice_Address = invoiceinfo.Invoice_Address;
                            contractinvoice.Invoice_ZipCode = invoiceinfo.Invoice_ZipCode;
                            contractinvoice.Invoice_Tel = invoiceinfo.Invoice_Tel;
                            contractinvoice.Invoice_VAT_Cert = invoiceinfo.Invoice_VAT_Cert;
                            contractinvoice.Invoice_PersonelCard = invoiceinfo.Invoice_PersonelCard;
                            contractinvoice.Invoice_PersonelName = invoiceinfo.Invoice_PersonelName;
                            contractinvoice.Invoice_Status = 1;
                            MyContract.EditContractInvoice(contractinvoice);
                        }
                        else
                        {
                            contractinvoice = new ContractInvoiceInfo();
                            contractinvoice.Invoice_ContractID = entity.Contract_ID;
                            contractinvoice.Invoice_Name = invoiceinfo.Invoice_Name;
                            contractinvoice.Invoice_Type = invoiceinfo.Invoice_Type;
                            contractinvoice.Invoice_Title = invoiceinfo.Invoice_Title;
                            contractinvoice.Invoice_Content = invoiceinfo.Invoice_Content;
                            contractinvoice.Invoice_FirmName = invoiceinfo.Invoice_FirmName;
                            contractinvoice.Invoice_VAT_Code = invoiceinfo.Invoice_VAT_Code;
                            contractinvoice.Invoice_VAT_FirmName = invoiceinfo.Invoice_VAT_FirmName;
                            contractinvoice.Invoice_VAT_RegAddr = invoiceinfo.Invoice_VAT_RegAddr;
                            contractinvoice.Invoice_VAT_RegTel = invoiceinfo.Invoice_VAT_RegTel;
                            contractinvoice.Invoice_VAT_Bank = invoiceinfo.Invoice_VAT_Bank;
                            contractinvoice.Invoice_VAT_BankAccount = invoiceinfo.Invoice_VAT_BankAccount;
                            contractinvoice.Invoice_VAT_Content = invoiceinfo.Invoice_VAT_Content;
                            contractinvoice.Invoice_Address = invoiceinfo.Invoice_Address;
                            contractinvoice.Invoice_ZipCode = invoiceinfo.Invoice_ZipCode;
                            contractinvoice.Invoice_Tel = invoiceinfo.Invoice_Tel;
                            contractinvoice.Invoice_VAT_Cert = invoiceinfo.Invoice_VAT_Cert;
                            contractinvoice.Invoice_PersonelCard = invoiceinfo.Invoice_PersonelCard;
                            contractinvoice.Invoice_PersonelName = invoiceinfo.Invoice_PersonelName;
                            contractinvoice.Invoice_Status = 1;
                            MyContract.AddContractInvoice(contractinvoice);
                        }
                        Contract_Log_Add(entity.Contract_ID, "", "合同发票选择", 1, "用户选择合同开票信息");
                        pub.Msg("positive", "操作成功", "操作成功！", false, "/supplier/contract_detail.aspx?contract_sn=" + entity.Contract_SN);
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "无效的发票信息", false, "/supplier/contract_all.aspx");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "合同不支持此操作", false, "/supplier/contract_all.aspx");
        }
    }

    //提交意向合同修改意向
    public void TmpContract_NoteEdit()
    {
        string contract_note, contract_sn;
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        contract_note = tools.CheckStr(Request["contract_note"]);
        string attachment_file = tools.CheckStr(Request.Form["attachment_file"]);

        if (contract_sn == "")
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        if (contract_note == "")
        {
            pub.Msg("info", "提示信息", "请填写您要提交的信息", false, "{back}");
        }
        string suppliername = "";
        string detail_url = "/supplier/contract_detail.aspx?contract_sn=";
        ContractInfo entity = GetContractBySn(contract_sn);
        if (entity != null)
        {
            if (entity.Contract_BuyerID != tools.NullInt(Session["supplier_id"]) && entity.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Contract_list.aspx");
            }
            if (entity.Contract_Status > 1)
            {
                pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
            if (entity.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                suppliername = tools.NullStr(Session["supplier_companyname"]);
                detail_url = "/supplier/mycontract_detail.aspx?contract_sn=";
            }
            if (entity.Contract_Status == 0)
            {

                if (attachment_file != null && attachment_file.Length > 0)
                {
                    contract_note += "  合同附件：<a href=\"" + Convert.ToString(Application["Upload_Server_URL"]).TrimEnd('/') + attachment_file + "\" target=\"_blank\">点此查看</a>";
                }

                Contract_Log_Add(entity.Contract_ID, suppliername, "提交意向合同修改意向", 1, "提交意见合同修改意向：" + contract_note);
            }
            else
            {
                Contract_Log_Add(entity.Contract_ID, suppliername, "提交合同咨询", 1, "提交合同咨询：" + contract_note);
            }
            pub.Msg("positive", "操作成功", "操作成功！", false, detail_url + contract_sn);

        }
        else
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
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
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");

        }
        else
        {
            ContractInfo entity = GetContractBySn(contract_sn);
            if (entity != null)
            {
                if (entity.Contract_BuyerID != tools.NullInt(Session["supplier_id"]) || entity.Contract_Source != 0)
                {
                    pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Contract_list.aspx");
                }
                IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
                if (entity.Contract_Status == 0 && ordersinfos == null)
                {
                    entity.Contract_Status = 3;
                    MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                    Contract_Log_Add(entity.Contract_ID, "", "意向合同取消", 1, "意向合同取消，取消原因：" + contract_close_note);
                    pub.Msg("positive", "操作成功", "操作成功！", false, "/supplier/contract_detail.aspx?contract_sn=" + contract_sn);
                }
                else
                {
                    pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
        }
    }

    //合同付款分期设置
    public void Contract_PayDivided_Set(string Contract_sn, int Divided_Amount)
    {
        string payment_name, payment_note;
        double payment_amount, pay_setamount;
        DateTime paymentline;
        double price = 0;
        pay_setamount = 0;
        ContractInfo CEtity = GetContractBySn(Contract_sn);
        if (CEtity != null)
        {
            if (CEtity.Contract_BuyerID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/Contract_list.aspx");
            }
            if (CEtity.Contract_Status > 0)
            {
                pub.Msg("error", "错误信息", "合同不支持编辑", false, "{back}");
            }
            price = CEtity.Contract_AllPrice;
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/Contract_list.aspx");
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
                            pub.Msg("error", "错误信息", "分期付款金额超过合同总金额，请重新分配！", false, "{back}");
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

                Contract_Log_Add(CEtity.Contract_ID, "", "调整合同付款分期", 1, "调整合同付款分期由" + entitys.Count + "期为" + Divided_Amount + "期");
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
                        pub.Msg("error", "错误信息", "分期付款金额超过合同总金额，请重新分配！", false, "{back}");
                    }

                }
                Contract_Log_Add(CEtity.Contract_ID, "", "调整合同付款分期", 1, "调整合同付款分期信息");
            }
        }
        else
        {
            for (int i = 1; i <= Divided_Amount; i++)
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
        }
        if (CEtity.Contract_Confirm_Status > 0)
        {
            CEtity.Contract_Confirm_Status = 0;
            if (MyContract.EditContract(CEtity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
            {
                Contract_Log_Add(CEtity.Contract_ID, "", "调整合同付款分期", 1, "合同付款分期信息编辑，双方未确认");
            }
        }
    }

    //合同付款分期
    public void Contract_Divided_Set()
    {
        string Contract_sn = tools.CheckStr(Request["Contract_sn"]);
        int Contract_Divided = tools.CheckInt(Request["Contract_Divided"]);

        if (Contract_Divided < 1 || Contract_Divided > 7)
        {

            pub.Msg("error", "错误信息", "合同付款分期期数在1-7之间", false, "{back}");
            return;
        }
        Contract_PayDivided_Set(Contract_sn, Contract_Divided);
        pub.Msg("positive", "操作成功", "操作成功", true, "Contract_detail.aspx?contract_sn=" + Contract_sn);
    }

    //合同修改
    public void Contract_TemplateEdit()
    {
        string contract_template, log_remark, Contract_sn;
        log_remark = "合同条款信息编辑，双方未确认";
        contract_template = tools.CheckHTML(Request["contract_template"]);
        Contract_sn = tools.CheckStr(Request["Contract_sn"]);

        if (Contract_sn.Length == 0)
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }

        ContractInfo entity = GetContractBySn(Contract_sn);
        if (entity != null)
        {
            //if (entity.Contract_BuyerID != tools.NullInt(Session["supplier_id"]) || entity.Contract_Source > 0)

            if (entity.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/Contract_list.aspx");
            }

            //if (entity.Contract_Status > 0)
            //{
            //    pub.Msg("error", "错误信息", "合同不支持编辑", false, "{back}");
            //}

            entity.Contract_Template = contract_template;
            entity.Contract_Confirm_Status = 0;
            if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
            {
                Contract_Log_Add(entity.Contract_ID, "", "修改合同信息", 1, log_remark);
                pub.Msg("positive", "操作成功", "操作成功", true, "mycontract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
            else
            {
                Contract_Log_Add(entity.Contract_ID, "", "修改合同信息", 1, log_remark);
                pub.Msg("positive", "操作失败", "操作失败", true, "mycontract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }
    }

    //合同费用修改
    public void Contract_PriceEdit()
    {
        string Contract_sn, log_remark;
        double Contract_Freight, Contract_Service, Contract_Discount;

        log_remark = "合同费用信息编辑，双方未确认";
        Contract_sn = tools.CheckStr(Request["Contract_sn"]);

        Contract_Freight = Math.Round(tools.CheckFloat(Request["Contract_Freight"]), 2);
        Contract_Service = tools.CheckFloat(Request["Contract_Service"]);
        Contract_Discount = Math.Round(tools.CheckFloat(Request["Contract_Discount"]), 2);

        if (Contract_sn.Length == 0)
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }


        ContractInfo entity = GetContractBySn(Contract_sn);
        if (entity != null)
        {
            //if (entity.Contract_BuyerID != tools.NullInt(Session["supplier_id"])||entity.Contract_Source>0)
            //{
            //    pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/Contract_list.aspx");
            //}

            if (entity.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/mycontract_list.aspx");
            }

            if (entity.Contract_Status > 0)
            {
                pub.Msg("error", "错误信息", "合同不支持编辑", false, "{back}");
            }
            if (Contract_Discount > 0 && Contract_Discount > entity.Contract_Price)
            {
                pub.Msg("error", "错误信息", "优惠金额不能超过订单总金额", false, "{back}");
            }
            if (entity.Contract_Freight != Contract_Freight)
            {
                log_remark += "，运费由" + pub.FormatCurrency(entity.Contract_Freight) + "更改为" + pub.FormatCurrency(Contract_Freight) + "";
                entity.Contract_Freight = Contract_Freight;
            }

            if (entity.Contract_Discount != Contract_Discount)
            {
                log_remark += "，合同优惠由" + pub.FormatCurrency(entity.Contract_Discount) + "更改为" + pub.FormatCurrency(Contract_Discount) + "";
                entity.Contract_Discount = Contract_Discount;
            }
            entity.Contract_AllPrice = entity.Contract_Price + entity.Contract_ServiceFee + entity.Contract_Freight - entity.Contract_Discount;
            //检查合同总价
            if (entity.Contract_AllPrice < 0)
            {
                pub.Msg("error", "错误信息", "合同总价超过合同最小金额", false, "{back}");
            }
            entity.Contract_Confirm_Status = 0;
            if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
            {
                Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "修改合同费用信息", 1, log_remark);
                pub.Msg("positive", "操作成功", "操作成功", true, "mycontract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
            else
            {
                Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "修改合同费用信息", 0, log_remark);
                pub.Msg("positive", "操作失败", "操作失败", true, "mycontract_detail.aspx?contract_sn=" + entity.Contract_SN);
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
            return;
        }
    }

    //意向合同买方确认
    public void Buyer_TmpContract_Confirm()
    {
        string contract_sn;
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        if (contract_sn == "")
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        //检查合同信息
        ContractInfo entity = GetContractBySn(contract_sn);
        if (entity == null)
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        //检查购买人信息
        if (entity.Contract_BuyerID != supplier_id || entity.Contract_Status > 0)
        {
            pub.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }
        //检查合同状态
        if (entity.Contract_Status > 0)
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        //检查是否包括订单
        IList<OrdersInfo> ordersinfos = GetOrderssByContractID(entity.Contract_ID);
        if (ordersinfos == null)
        {
            pub.Msg("info", "提示信息", "合同中没有附加订单信息", false, "{back}");
        }
        //检查是否分期付款金额
        IList<ContractDividedPaymentInfo> dividedpayments = MyDividedpay.GetContractDividedPaymentsByContractID(entity.Contract_ID);
        if (dividedpayments == null)
        {
            pub.Msg("info", "提示信息", "您还没有选择付款分期信息", false, "{back}");
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
                pub.Msg("info", "提示信息", "合同分期金额与合同总价不一致！", false, "{back}");
            }
        }

        //检查是否选择发票信息
        ContractInvoiceInfo invoice = MyContract.GetContractInvoiceByContractID(entity.Contract_ID);
        if (invoice == null)
        {
            pub.Msg("info", "提示信息", "您还没有选择发票信息", false, "{back}");
        }

        int Confirm_Status = entity.Contract_Confirm_Status;
        entity.Contract_Confirm_Status = 1;
        if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
        {
            Contract_Log_Add(entity.Contract_ID, "", "买方意向合同确认", 1, "买方意向合同确认");
            if (Confirm_Status > 0)
            {
                TmpContract_To_Contract(entity);
            }
            pub.Msg("positive", "操作成功", "操作成功！", false, "/supplier/contract_detail.aspx?contract_sn=" + contract_sn);
        }
        else
        {
            pub.Msg("error", "提示信息", "操作失败", false, "{back}");
        }
    }

    //意向合同确认
    public void Seller_TmpContract_Confirm()
    {
        string contract_sn;
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        if (contract_sn == "")
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");

        }
        ContractInfo entity = GetContractBySn(contract_sn);
        if (entity == null)
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        if (entity.Contract_SupplierID != supplier_id)
        {
            pub.Msg("error", "错误信息", "无效的意向合同！", false, "{back}");
        }
        //检查合同状态
        if (entity.Contract_Status > 0)
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
        //检查是否包括订单
        IList<OrdersInfo> ordersinfos = GetOrderssByContractID(entity.Contract_ID);
        if (ordersinfos == null)
        {
            pub.Msg("info", "提示信息", "合同中没有附加订单信息", false, "{back}");
        }
        //检查是否分期付款金额
        IList<ContractDividedPaymentInfo> dividedpayments = MyDividedpay.GetContractDividedPaymentsByContractID(entity.Contract_ID);
        if (dividedpayments == null)
        {
            pub.Msg("info", "提示信息", "您还没有选择付款分期信息", false, "{back}");
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
                pub.Msg("info", "提示信息", "合同分期金额与合同总价不一致！", false, "{back}");
            }
        }

        //检查是否选择发票信息
        ContractInvoiceInfo invoice = MyContract.GetContractInvoiceByContractID(entity.Contract_ID);
        if (invoice == null)
        {
            pub.Msg("info", "提示信息", "您还没有选择发票信息", false, "{back}");
        }

        int Confirm_Status = entity.Contract_Confirm_Status;
        entity.Contract_Confirm_Status = 2;
        if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
        {
            Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "卖方意向合同确认", 1, "卖方意向合同确认");

            if (Confirm_Status > 0)
            {
                TmpContract_To_Contract(entity);
            }
            pub.Msg("positive", "操作成功", "操作成功！", false, "/supplier/mycontract_detail.aspx?contract_sn=" + contract_sn);
        }
        else
        {
            pub.Msg("error", "提示信息", "操作失败", false, "{back}");
        }

    }

    //意向合同订单移除
    public void TmpContract_Orders_Remove()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            ContractInfo entity = GetContractByID(ordersinfo.Orders_ContractID);
            if (entity != null)
            {
                if (entity.Contract_BuyerID == supplier_id && entity.Contract_Source == 0 && entity.Contract_Status == 0 && entity.Contract_Confirm_Status == 0)
                {
                    //更新订单金额
                    entity.Contract_Price = entity.Contract_Price - ordersinfo.Orders_Total_AllPrice;
                    if (entity.Contract_Price < 0)
                    {
                        entity.Contract_Price = 0;
                    }
                    //代理采购合同
                    if (entity.Contract_Type == 3)
                    {
                        SupplierInfo supplierinfo = GetSupplierByID(ordersinfo.Orders_SupplierID);
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
                    MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                    ordersinfo.Orders_ContractID = 0;
                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        //初始合同卖家信息
                        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
                        if (ordersinfos == null)
                        {
                            entity.Contract_SupplierID = 0;
                            MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                        }
                        Contract_Log_Add(entity.Contract_ID, "", "意向合同订单移除", 1, "意向合同订单移除，移除订单号：" + ordersinfo.Orders_SN);
                        pub.Msg("positive", "操作成功", "操作成功！", false, "/supplier/contract_detail.aspx?contract_sn=" + entity.Contract_SN);
                    }
                    else
                    {
                        Contract_Log_Add(entity.Contract_ID, "", "意向合同订单移除", 0, "意向合同订单移除，移除订单号：" + ordersinfo.Orders_SN);
                        pub.Msg("error", "操作失败", "操作失败！", false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }
    }

    #endregion

    //合同配货中
    public void Contract_Delivery_Prepare()
    {
        string contract_sn;
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        contract_sn = tools.CheckStr(Request["contract_sn"]);
        if (contract_sn == "")
        {
            pub.Msg("info", "提示信息", "合同无效", false, "{back}");

        }
        ContractInfo entity = GetContractBySn(contract_sn);
        if (entity == null)
        {
            pub.Msg("info", "提示信息", "合同无效", false, "{back}");
        }
        if (entity.Contract_SupplierID != supplier_id)
        {
            pub.Msg("error", "错误信息", "合同无效！", false, "{back}");
        }
        //检查合同状态
        if (entity.Contract_Status != 1 || entity.Contract_Delivery_Status > 0)
        {
            pub.Msg("info", "提示信息", "无法执行此操作", false, "{back}");
        }

        entity.Contract_Delivery_Status = 1;
        if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
        {
            Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同配货中", 1, "合同正在配货中");
            pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + contract_sn);
        }
        else
        {
            pub.Msg("error", "提示信息", "操作失败", false, "{back}");
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
                            freighted_amount = 0;
                            strHTML += "<tr>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + order.Orders_SN + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Code + "</td>";
                            strHTML += "<td height=\"23\" align=\"left\" class=\"cell_content\">" + entity.Orders_Goods_Product_Name + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity.Orders_Goods_Product_Spec + "</td>";
                            strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>";
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



    //邮件模版
    public string mail_template(string template_name, string member_email, string contract_sn, int delivery_id)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "contract_freight":
                mailbody = "<p>感谢您通过{sys_config_site_name}购物，发货商品信息如下：</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/supplier/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;

        }
        mailbody = mailbody.Replace("{member_email}", member_email);
        return mailbody;
    }

    //生成配送单号
    public string Contract_Delivery_SN()
    {
        string sn = "";
        bool ismatch = true;
        ContractDeliveryInfo deliveryinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            deliveryinfo = MyFreight.GetContractDeliveryBySN(sn);
            if (deliveryinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
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
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_Status != 1)
        {
            pub.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }
        if (operate == "create")
        {
            //生成发货单

            if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "合同无效！", false, "{back}");
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
                pub.Msg("error", "错误信息", "无效的配送方式", false, "{back}");
                return;
            }
            deliverywayinfo = deliveryway.GetDeliveryWayByID(delivery_id, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
            if (deliverywayinfo != null)
            {
                delivery_name = deliverywayinfo.Delivery_Way_Name;
            }
            else
            {
                pub.Msg("error", "错误信息", "无效的配送方式", false, "{back}");
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
                            double freighted_amount = 0;
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
                pub.Msg("error", "错误信息", "合同中没有需要发货的产品信息", false, "{back}");
                return;
            }

            if (iscontain_freight == false)
            {
                pub.Msg("error", "错误信息", "合同中没有需要发货的产品信息", false, "{back}");
                return;
            }





            string delivery_doc;

            delivery_doc = Contract_Delivery_SN();
            ContractDeliveryInfo entity = new ContractDeliveryInfo();
            entity.Contract_Delivery_ContractID = contractinfo.Contract_ID;
            entity.Contract_Delivery_DeliveryStatus = 1;
            entity.Contract_Delivery_SysUserID = 0;
            entity.Contract_Delivery_DocNo = delivery_doc;
            entity.Contract_Delivery_Name = delivery_name;
            entity.Contract_Delivery_Amount = freight_fee;
            entity.Contract_Delivery_Note = freight_note;
            entity.Contract_Delivery_Code = freight_code;
            entity.Contract_Delivery_CompanyName = freight_company;
            entity.Contract_Delivery_Addtime = DateTime.Now;
            entity.Contract_Delivery_AccpetNote = "";
            entity.Contract_Delivery_Site = pub.GetCurrentSite();
            if (MyFreight.AddContractDelivery(entity))
            {
                Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "生成配送单", 1, "合同生成配送单，配送单号：" + delivery_doc);
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
                        MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                        Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同已全部发货", 1, "合同全部发货完毕");
                    }
                    else
                    {
                        contractinfo.Contract_Delivery_Status = 2;
                        MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                        Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同已部分发货", 1, "合同部分发货完毕");
                    }
                    string supplier_email = "";
                    SupplierInfo supplierinfo = GetSupplierByID(contractinfo.Contract_BuyerID);
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
                        pub.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
                    }

                }
                pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
            }
            else
            {
                pub.Msg("error", "错误信息", "配送单生成失败！", false, "{back}");
                return;
            }
        }


    }

    //产品实际库存操作
    public void Contract_ProductStockAction(int Goods_ID, int Goods_Amount, string action)
    {
        int Product_ID, Goods_Type;
        string SqlAdd = "";
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
                        ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (productinfo != null)
                        {
                            if (productinfo.Product_IsNoStock == 0)
                            {
                                //添加退货单
                                //SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                //"Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                //", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                //"VALUES (3,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + entity.Orders_Goods_Amount + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,0,GETDATE(),'订单退货')";
                                //try
                                //{
                                //    DBHelper.ExecuteNonQuery(SqlAdd);

                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, Goods_Amount, 0);
                                //}
                                //catch (Exception ex) { throw ex; }
                            }
                        }

                    }
                    break;
                //发货
                case "del":
                    Goods_Amount = 0 - Goods_Amount;
                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (productinfo != null)
                        {
                            if (productinfo.Product_IsNoStock == 0)
                            {
                                //添加出库单
                                //SqlAdd = "INSERT INTO SCM_Purchasing (" +
                                //"Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                                //", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                                //"VALUES (2,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + (0 - entity.Orders_Goods_Amount).ToString() + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,0,GETDATE(),'订单发货')";
                                //try
                                //{
                                //    DBHelper.ExecuteNonQuery(SqlAdd);

                                //更新商品库存
                                MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, Goods_Amount, 0);
                                //}
                                //catch (Exception ex) { throw ex; }
                            }
                            //else
                            //{
                            //    //获取默认仓库及供应商

                            //    //添加入库单
                            //    SqlAdd = "INSERT INTO SCM_Purchasing (" +
                            //    "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                            //    ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                            //    "VALUES (1," + Config_DefaultDepot + "," + Config_DefaultSupplier + ",'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + entity.Orders_Goods_Amount + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',1,1,GETDATE(),'订单发货（零库存进货）')";
                            //    try
                            //    {
                            //        DBHelper.ExecuteNonQuery(SqlAdd);
                            //    }
                            //    catch (Exception ex) { throw ex; }
                            //    //更新商品库存
                            //    MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, entity.Orders_Goods_Amount, 0);
                            //    //添加出库单
                            //    SqlAdd = "INSERT INTO SCM_Purchasing (" +
                            //    "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
                            //    ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
                            //    "VALUES (2,0,0,'" + entity.Orders_Goods_Product_Code + "'," + entity.Orders_Goods_Product_Price + "," + (0 - entity.Orders_Goods_Amount).ToString() + "," + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + ",'','',0,1,GETDATE(),'订单发货（零库存发货）')";
                            //    try
                            //    {
                            //        DBHelper.ExecuteNonQuery(SqlAdd);
                            //    }
                            //    catch (Exception ex) { throw ex; }
                            //    //更新商品库存
                            //    MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, 0 - entity.Orders_Goods_Amount, 0);
                            //}
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

    #region 签收

    /// <summary>
    /// 签收
    /// </summary>
    public void Contract_Delivery_Accept()
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        string contract_delivery = tools.CheckStr(Request["contract_delivery"]);
        if (contract_sn == "")
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/index.aspx");
        }

        if (contract_delivery == "")
        {
            pub.Msg("error", "错误信息", "无效的发货单编号", false, "/supplier/index.aspx");
        }

        ContractInfo contractinfo = GetContractBySn(contract_sn);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && contractinfo.Contract_BuyerID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/index.aspx");
            }
        }
        else
        {
            contract_sn = "";
        }

        if (contract_sn == "")
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/index.aspx");
        }
        string suppliername = "";
        if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
        {
            suppliername = tools.NullStr(Session["supplier_companyname"]);
        }
        ContractDeliveryInfo deliveryinfo = GetContractDeliveryBySN(contract_delivery);
        if (deliveryinfo == null)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
        }
        //验证发货单与合同的一致性
        if (deliveryinfo.Contract_Delivery_ContractID != contractinfo.Contract_ID)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
        }
        //验证发货单发货状态
        if (deliveryinfo.Contract_Delivery_DeliveryStatus != 1)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
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
                            Contract_Log_Add(contractinfo.Contract_ID, suppliername, "发货单签收", 1, "产品名称：" + goods.Orders_Goods_Product_Name + "  签收数量：" + amount);
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
                Contract_Log_Add(contractinfo.Contract_ID, suppliername, "发货单全部签收", 1, "发货单：" + deliveryinfo.Contract_Delivery_DocNo + "  已全部签收");

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
                    if (MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
                    {
                        Contract_Log_Add(contractinfo.Contract_ID, suppliername, "合同全部签收", 1, "合同商品已全部签收");
                    }
                }
            }
        }
        pub.Msg("positive", "操作成功", "签收成功", true, "/supplier/Contract_freight_view.aspx?contract_delivery=" + deliveryinfo.Contract_Delivery_DocNo + "&contract_sn=" + contractinfo.Contract_SN);
    }

    #endregion

    #region 全部签收

    /// <summary>
    /// 全部签收
    /// </summary>
    public void Contract_Delivery_AllAccept()
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        string contract_delivery = tools.CheckStr(Request["contract_delivery"]);
        if (contract_sn == "")
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "/supplier/index.aspx");
        }

        if (contract_delivery == "")
        {
            pub.Msg("error", "错误信息", "无效的发货单编号", false, "/supplier/index.aspx");
        }

        ContractInfo contractinfo = GetContractBySn(contract_sn);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && contractinfo.Contract_BuyerID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/index.aspx");
            }
        }
        else
        {
            contract_sn = "";
        }

        if (contract_sn == "")
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/index.aspx");
        }
        string suppliername = "";
        if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
        {
            suppliername = tools.NullStr(Session["supplier_companyname"]);
        }
        ContractDeliveryInfo deliveryinfo = GetContractDeliveryBySN(contract_delivery);
        if (deliveryinfo == null)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
        }
        //验证发货单与合同的一致性
        if (deliveryinfo.Contract_Delivery_ContractID != contractinfo.Contract_ID)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
        }
        //验证发货单发货状态
        if (deliveryinfo.Contract_Delivery_DeliveryStatus != 1)
        {
            pub.Msg("error", "错误信息", "无效的发货单", false, "/supplier/index.aspx");
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
                        Contract_Log_Add(contractinfo.Contract_ID, suppliername, "发货单签收", 1, "产品名称：" + goods.Orders_Goods_Product_Name + "  签收数量：" + amount);
                    }
                }
            }
        }
        deliveryinfo.Contract_Delivery_DeliveryStatus = 2;
        if (MyFreight.EditContractDelivery(deliveryinfo))
        {
            bool isallaccept = true;
            Contract_Log_Add(contractinfo.Contract_ID, suppliername, "发货单全部签收", 1, "发货单：" + deliveryinfo.Contract_Delivery_DocNo + "  已全部签收");
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
                if (MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
                {
                    Contract_Log_Add(contractinfo.Contract_ID, suppliername, "合同全部签收", 1, "合同商品已全部签收");
                }
            }
        }
        pub.Msg("positive", "操作成功", "签收成功", true, "/supplier/Contract_freight_view.aspx?contract_delivery=" + deliveryinfo.Contract_Delivery_DocNo + "&contract_sn=" + contractinfo.Contract_SN);
    }

    #endregion

    #region 获取合同已支付金额

    /// <summary>
    /// 获取合同已支付金额
    /// </summary>
    /// <param name="Contract_ID"></param>
    /// <returns></returns>
    public double Get_Contract_PayedAmount(int Contract_ID)
    {
        return MyContract.Get_Contract_PayedAmount(Contract_ID);
    }

    #endregion

    #region 生成支付单号

    /// <summary>
    /// 生成支付单号
    /// </summary>
    /// <returns></returns>
    public string orders_payment_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersPaymentInfo paymentinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.CreatevkeyNum(5);
        while (ismatch == true)
        {
            paymentinfo = Mypayment.GetOrdersPaymentBySn(sn);
            if (paymentinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.CreatevkeySign(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    #endregion

    #region 订单支付操作（同中铁 支付平台）

    /// <summary>
    /// 订单支付操作（同中铁 支付平台）
    /// </summary>
    /// <param name="operate"></param>
    public void Contract_Payment_Action(string operate)
    {
        //判断是否为主账户支付
        if (tools.NullStr(Session["subPrivilege"]) == "")
        {
            //支付
            PayMent(operate);
        }
        else
        {
            //获取子帐号拥有权限
            string Member_Permissions = Session["subPrivilege"].ToString();
            List<string> oTempList = new List<string>(Member_Permissions.Split(','));
            //判断子账户是否拥有支付权限ps:支付权限 ： 6
            if (oTempList.Contains("6"))
            {
                //支付
                PayMent(operate);
            }
            else//无权限
            {
                //提示消息权限不足
                pub.Msg("error", "错误信息", "权限不足", false, "{back}");
            }
        }
    }

    #endregion

    #region 完成支付功能

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="operate">操作</param>
    private void PayMent(string operate)
    {

        int Orders_ID = tools.CheckInt(Request["orders_id"]);
        string Orders_Payment_Name = tools.CheckStr(Request["Orders_Payment_Name"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int pay_id = -1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Name", "=", Orders_Payment_Name));
        IList<PayWayInfo> payways = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payways != null)
        {
            int i = 0;
            foreach (PayWayInfo payway in payways)
            {
                i++;
                if (i == 1)
                {
                    pay_id = payway.Pay_Way_ID;
                }

            }
        }
        string Orders_Payment_Note = tools.CheckStr(Request["Orders_Payment_Note"]);
        int Orders_Payment_PaymentStatus = 0;
        int Orders_Buyerid = 0;
        int Order_Payment_ID = 0;
        string Orders_SN = "";
        string Orders_Payment_DocNo = orders_payment_sn();
        string paymentmemo = "";
        double Orders_Total_AllPrice = 0.00;


        OrdersInfo orders = Myorder.GetOrdersByID(Orders_ID);
        //是否为招标订单
        if (orders.Orders_Note.Contains("|招标订单AS56482(系统添加)|:"))
        {
            IBid MyBid = new Glaer.Trade.B2C.BLL.MEM.Bid();
            string[] BID = orders.Orders_Note.Split(':');
               Supplier MySupplier = new Supplier();


    BidInfo entity = MyBid.GetBidByID(Convert.ToInt32(BID[1]), pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
            //获取卖家ID
            int Supplier_ID = tools.NullInt(Session["supplier_id"]);

            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplier_ID);
            SupplierInfo supplier = MySupplier.GetSupplierByID();
            ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(supplier.Supplier_ID);
            ZhongXin mycredit = new ZhongXin();
            if (supplierinfo != null)
            {
                if (supplierinfo.Supplier_AuditStatus != 1)
                {
                    Response.Write("请等待资质审核");
                    Response.End();
                }
            }
            else
            {
                Response.Write("请先登录");
                Response.End();
            }
            //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterStartTime) < 0)
            //{
            //    if (entity.Bid_Type == 0)
            //    {
            //        //Response.Write("招标项目报名未开始");
            //        //Response.End();
            //    }
            //    else
            //    {
            //        //Response.Write("拍卖项目报名未开始");
            //        //Response.End();
            //    }


            //}

            //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) > 0)
            //{
            //    if (entity.Bid_Type == 0)
            //    {
            //    Response.Write("招标项目报名已结束");
            //    Response.End();
            //}
            //else
            //{
            //    Response.Write("拍卖项目报名已结束");
            //    Response.End();
            //    }


            //}

            if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1)
            {
                if (entity.Bid_Type == 0)
                {
                    Response.Write("招标项目不可报名");
                    Response.End();
                }
                else
                {
                    Response.Write("拍卖项目不可报名");
                    Response.End();
                }


            }

         

            string Supplier_CompanyName = "商家";
            if (supplierinfo != null)
            {
                Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
            }
            if (PayAccountInfo != null)
            {
                decimal accountremain = 0;
                accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
                if (accountremain < (decimal)entity.Bid_Bond)
                {
                    Response.Write("您的账户余额不足，请入金充值！");
                    Response.End();
                }
                else
                {
                    string Log_note = "";
                    //Bid_Type 0:代表招标  1:代表拍卖
                    if (entity.Bid_Type == 1)
                    {

                        string supplier_name = supplierinfo.Supplier_CompanyName;
                        string strResult = string.Empty;
                        if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "买家参与拍卖交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
                            ))
                        {
                            Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                        }
                        else
                        {
                            Response.Write("拍卖投标保证金转账扣款失败");
                            Response.End();
                        }


                    }
                    else
                    {
                        string supplier_name = supplierinfo.Supplier_CompanyName;
                        string strResult = string.Empty;
                        if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "卖家参与招标交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
                            ))
                        {
                            //Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                            Log_note = "报名[" + entity.Bid_Title + "]招标项目,扣除保证金";
                        }
                        else
                        {
                            Response.Write("招标投标保证金转账扣款失败");
                            Response.End();
                        }
                        //Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                    }

                    IBidEnter MyBidEnter = BidEnterFactory.CreateBidEnter();
                    MySupplier.Supplier_Account_Log(Supplier_ID, 1, -(entity.Bid_Bond), Log_note);
                    BidEnterInfo Bidenter = MyBidEnter.GetBidEnterBySupplierID(entity.Bid_ID, Supplier_ID);

                    Bidenter = new BidEnterInfo();

                    Bidenter.Bid_Enter_ID = 0;
                    Bidenter.Bid_Enter_BidID = entity.Bid_ID;
                    Bidenter.Bid_Enter_SupplierID = Supplier_ID;
                    Bidenter.Bid_Enter_Bond = entity.Bid_Bond;
                    Bidenter.Bid_Enter_Type = entity.Bid_Type;
                    Bidenter.Bid_Enter_IsShow = 1;
                    if (MyBidEnter.AddBidEnter(Bidenter))
                    {
                        //Response.Write("True");
                        Response.Write("success");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("报名失败,请稍后再报名");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Write("未找到该平台下" + Supplier_CompanyName + "商家的中信账号");
                Response.End();
            }
        }
        if (orders != null)
        {
            Orders_Total_AllPrice = orders.Orders_Total_AllPrice;
        }
        bool Is_All = false;
        int sys_paytype = 0;
        double creditlimit_remian = 0, tempcreditlimit_remian = 0;
        Orders_Total_AllPrice = Math.Round(Orders_Total_AllPrice, 2);

        //付款
        if (operate == "create")
        {

            if (Orders_Total_AllPrice <= 0)
            {
                pub.Msg("error", "错误信息", "请填写支付金额", false, "{back}");
                return;
            }
            if (pay_id == 0)
            {
                pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                return;
            }



            //订单状态验证
            if (orders.Orders_Status == 0)
            {
                pub.Msg("error", "错误信息", "该订单无法执行此操作", false, "{back}");
            }


            PayWayInfo payway = null;

            payway = MyPayway.GetPayWayByID(pay_id, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
            if (payway == null)
            {
                pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                return;
            }
            else
            {
                sys_paytype = payway.Pay_Way_Type;//获取是否为建行支付（10为建行支付）
            }

            //添加建行支付判断（2015-6-19）
            if (sys_paytype == 10)
            {

            }
            else if (sys_paytype == 14)
            {

                ZhongXin mycredit = new ZhongXin();

                #region 中信支付
                int Supplier_ID1 = -1;
                string supplier_name = "";

                MemberInfo memberinfo1 = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberinfo1 != null)
                {
                    Supplier_ID1 = memberinfo1.Member_SupplierID;

                }
                if (Supplier_ID1 > 0)
                {
                    SupplierInfo supplierinfo1 = GetSupplierByID(Supplier_ID1);
                    if (supplierinfo1 != null)
                    {
                        supplier_name = supplierinfo1.Supplier_CompanyName;
                    }

                    ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
                    if (accountinfo != null)
                    {
                        decimal accountremain = 0;
                        accountremain = mycredit.GetAmount(accountinfo.SubAccount);
                        if (accountremain < (decimal)Orders_Total_AllPrice)
                        {
                            pub.Msg("error", "错误信息", "中信附属账户余额不足，请入金充值！", false, "{back}");
                        }
                        else
                        {


                            //转账到担保账户
                            if (mycredit.SaveZhongXinAccountLog(orders.Orders_BuyerID, Orders_Total_AllPrice, "[转账到担保账户]订单编号：" + orders.Orders_SN))
                            {
                                string strResult = mycredit.ToGuaranteeAccount(orders.Orders_BuyerID, Orders_Total_AllPrice, "[转账到担保账户]订单编号：" + orders.Orders_SN, Supplier_ID1, supplier_name);

                                if (strResult == "true")
                                {


                                    //若为买家付款则状态，wsz：2015-12-30，支付后付款单状态直接改为已到账
                                    int PaymentStatus = 1;
                                    // if (contractinfo.Contract_BuyerID == tools.NullInt(Session["supplier_id"]) && pay_type == 0) PaymentStatus = 2;

                                    OrdersInfo ordersinfo1 = Myorder.GetOrdersByID(Orders_ID);
                                    if (ordersinfo1 != null)
                                    {

                                        //ordersinfo1.Orders_PaymentStatus = 4;
                                        //17-4-17 订单详情页 将已到账状态：4改为已支付状态：2
                                        ordersinfo1.Orders_PaymentStatus = 4;
                                        ordersinfo1.Orders_PaymentStatus_Time = DateTime.Now;
                                        //ordersinfo1.Orders_Total_AllPrice = product_total + ordersinfo1.Orders_Total_Freight;




                                        MyOrders.EditOrders(ordersinfo1);
                                    }


                                    //生成支付单
                                    Create_Orders_Payment(orders.Orders_ID, Orders_Payment_Note, payway.Pay_Way_Name, Orders_Total_AllPrice, tools.NullInt(Session["supplier_id"]), PaymentStatus, orders.Orders_BuyerID, Orders_Payment_DocNo);

                                    new Orders().Orders_Log(ordersinfo1.Orders_ID, "", "订单支付", "成功", "订单付款完成");

                                    //添加短信提醒2015-5-14
                                    MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                                    //推送短信
                                    SMS mySMS = new SMS();
                                    SupplierInfo send_system_Supplier = GetSupplierByID(orders.Orders_SupplierID);
                                    mySMS.Send(send_system_Supplier.Supplier_Mobile, send_system_Supplier.Supplier_CompanyName + "," + orders.Orders_SN, "member_pay_orders_remind");



                                    if (memberinfo != null)
                                    {
                                        pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
                                    }
                                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_view.aspx?orders_sn=" + ordersinfo1.Orders_SN);

                                }
                                else
                                {
                                    //new ZhongXin().SaveZhongXinAccountLog(orders.Orders_BuyerID, Orders_Total_AllPrice * -1, "[撤销][转账到担保账户]合同编号：" + orders.Orders_SN);

                                    pub.Msg("error", "错误信息", strResult, false, "{back}");
                                }
                            }
                            else
                            {
                                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
                            }
                        }
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "您尚未开通中信账户！", false, "{back}");
                    }

                }
                else
                {
                    pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
                }




                #endregion

            }
            else
            {
                //其他在线支付
            }
        }
    }

    #endregion

    #region 订单支付操作（同中铁 支付平台）1

    /// <summary>
    /// 订单支付操作（同中铁 支付平台）1
    /// </summary>
    /// <param name="operate"></param>
    public void Contract_Payment_Action1(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["orders_id"]);
        string Orders_Payment_Name = tools.CheckStr(Request["Orders_Payment_Name"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int pay_id = -1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Name", "=", Orders_Payment_Name));
        IList<PayWayInfo> payways = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payways != null)
        {
            int i = 0;
            foreach (PayWayInfo payway in payways)
            {
                i++;
                if (i == 1)
                {
                    pay_id = payway.Pay_Way_ID;
                }

            }
        }
        string Orders_Payment_Note = tools.CheckStr(Request["Orders_Payment_Note"]);
        int Orders_Payment_PaymentStatus = 0;
        int Orders_Buyerid = 0;
        int Order_Payment_ID = 0;
        string Orders_SN = "";
        string Orders_Payment_DocNo = orders_payment_sn();
        string paymentmemo = "";
        double Orders_Total_AllPrice = 0.00;


        OrdersInfo orders = Myorder.GetOrdersByID(Orders_ID);
        if (orders != null)
        {
            Orders_Total_AllPrice = orders.Orders_Total_AllPrice;
        }
        bool Is_All = false;
        int sys_paytype = 0;
        double creditlimit_remian = 0, tempcreditlimit_remian = 0;
        Orders_Total_AllPrice = Math.Round(Orders_Total_AllPrice, 2);

        //付款
        if (operate == "create")
        {
            //添加建行支付判断（2015-6-19）


            #region 中信支付
            int Supplier_ID1 = -1;
            string supplier_name = "";

            MemberInfo memberinfo1 = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo1 != null)
            {
                Supplier_ID1 = memberinfo1.Member_SupplierID;

            }
            if (Supplier_ID1 > 0)
            {
                SupplierInfo supplierinfo1 = GetSupplierByID(Supplier_ID1);
                if (supplierinfo1 != null)
                {
                    supplier_name = supplierinfo1.Supplier_CompanyName;
                }

            }


            if (new ZhongXin().SaveZhongXinAccountLog(orders.Orders_BuyerID, Orders_Total_AllPrice, "[转账到担保账户]订单编号：" + orders.Orders_SN))
            {
                string strResult = new ZhongXin().ToGuaranteeAccount1(memberinfo1.Member_ID);


            }
            else
            {
                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
            }

            #endregion

        }
    }

    #endregion

    #region 商家信用额度处理日志

    /// <summary>
    /// 商家信用额度处理日志
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <param name="Creditlimit_Type"></param>
    /// <param name="Amount"></param>
    /// <param name="Log_note"></param>
    public void Supplier_CreditLimit_Log(int Supplier_ID, int Creditlimit_Type, double Amount, string Log_note)
    {
        double CreditlimitRemain = 0;
        SupplierInfo supplierinfo = GetSupplierByID(Supplier_ID);
        if (supplierinfo != null)
        {
            if (Creditlimit_Type == 0)
            {
                CreditlimitRemain = supplierinfo.Supplier_CreditLimitRemain;
            }
            else
            {
                CreditlimitRemain = supplierinfo.Supplier_TempCreditLimitRemain;
            }
            SupplierCreditLimitLogInfo creditLog = new SupplierCreditLimitLogInfo();
            creditLog.CreditLimit_Log_ID = 0;
            creditLog.CreditLimit_Log_Type = Creditlimit_Type;
            creditLog.CreditLimit_Log_SupplierID = Supplier_ID;
            creditLog.CreditLimit_Log_Amount = Amount;
            creditLog.CreditLimit_Log_AmountRemain = CreditlimitRemain + Amount;
            creditLog.CreditLimit_Log_Note = Log_note;
            creditLog.CreditLimit_Log_Addtime = DateTime.Now;
            creditLog.CreditLimit_Log_PaymentStatus = 0;
            myCreditLimitLog.AddSupplierCreditLimitLog(creditLog);

            if (Amount != 0)
            {
                if (Creditlimit_Type == 0)
                {
                    supplierinfo.Supplier_CreditLimitRemain = CreditlimitRemain + Amount;
                }
                else
                {
                    supplierinfo.Supplier_TempCreditLimitRemain = CreditlimitRemain + Amount;
                }
            }
            if ((CreditlimitRemain + Amount) >= 0)
            {
                MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            }

        }
    }

    #endregion

    public void Contract_Payment_Already()
    {
        string Contract_SN = tools.CheckStr(Request["Contract_Sn"]);
        string Payment_SN = tools.CheckStr(Request["Payment_SN"]);
        if (Contract_SN.Length == 0)
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractInfo contractinfo = GetContractBySn(Contract_SN);

        if (contractinfo == null)
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractPaymentInfo paymentinfo = MyPayment.GetContractPaymentBySN(Payment_SN);
        if (paymentinfo == null)
        {
            pub.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }
        if (paymentinfo.Contract_Payment_PaymentStatus != 0 || paymentinfo.Contract_Payment_ContractID != contractinfo.Contract_ID)
        {
            pub.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }
        paymentinfo.Contract_Payment_PaymentStatus = 1;
        if (MyPayment.EditContractPayment(paymentinfo))
        {
            Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同支付已确认", 1, "付款单 " + paymentinfo.Contract_Payment_DocNo + " 支付金额：" + pub.FormatCurrency(paymentinfo.Contract_Payment_Amount) + "确认已支付");

            pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
        }
        else
        {
            pub.Msg("error", "操作失败", "操作失败，请稍后再试！", false, "{back}");
        }

    }

    //合同付款到账
    public void Contract_Payment_Reach()
    {
        string Contract_SN = tools.CheckStr(Request["Contract_Sn"]);
        string Payment_SN = tools.CheckStr(Request["Payment_SN"]);
        if (Contract_SN.Length == 0)
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractInfo contractinfo = GetContractBySn(Contract_SN);

        if (contractinfo == null)
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }
        if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{back}");
            return;
        }

        ContractPaymentInfo paymentinfo = MyPayment.GetContractPaymentBySN(Payment_SN);
        if (paymentinfo == null)
        {
            pub.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }
        if (paymentinfo.Contract_Payment_PaymentStatus != 1 || paymentinfo.Contract_Payment_ContractID != contractinfo.Contract_ID)
        {
            pub.Msg("error", "错误信息", "付款单无效无效", false, "{back}");
            return;
        }

        paymentinfo.Contract_Payment_PaymentStatus = 2;
        if (MyPayment.EditContractPayment(paymentinfo))
        {
            Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同支付已到帐", 1, "付款单 " + paymentinfo.Contract_Payment_DocNo + " 支付金额：" + pub.FormatCurrency(paymentinfo.Contract_Payment_Amount) + "确认已到帐");

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
                        MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581"));
                        Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同全部到账", 1, "合同全部支付并确认到账");
                    }
                }
            }
            pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
        }
        else
        {
            pub.Msg("error", "操作失败", "操作失败，请稍后再试！", false, "{back}");
        }
    }

    //生成支付单号
    public string Contract_payment_sn()
    {
        string sn = "";
        bool ismatch = true;
        ContractPaymentInfo paymentinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            paymentinfo = MyPayment.GetContractPaymentBySN(sn);
            if (paymentinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    //生成付款单  根据中铁修改
    public void Create_Orders_Payment(int Orders_Payment_OrdersID, string Orders_Payment_Note, string Payment_Name, double Pay_Amount, int SysUserID, int PaymentStatus, int buyerid, string Orders_Payment_DocNo)
    {
        string payment_doc;
        string suppliername = "";
        if (SysUserID > 0)
        {
            suppliername = tools.NullStr(Session["supplier_companyname"]);
        }
        payment_doc = Contract_payment_sn();
        OrdersPaymentInfo entity = new OrdersPaymentInfo();
        entity.Orders_Payment_OrdersID = Orders_Payment_OrdersID;
        entity.Orders_Payment_MemberID = buyerid;
        entity.Orders_Payment_PaymentStatus = PaymentStatus;
        entity.Orders_Payment_SysUserID = SysUserID;
        entity.Orders_Payment_DocNo = Orders_Payment_DocNo;
        entity.Orders_Payment_Name = Payment_Name;
        entity.Orders_Payment_ApplyAmount = 0.00;
        entity.Orders_Payment_Amount = Pay_Amount;
        entity.Orders_Payment_Status = PaymentStatus;
        entity.Orders_Payment_Note = Orders_Payment_Note;
        entity.Orders_Payment_Addtime = DateTime.Now;
        entity.Orders_Payment_Site = pub.GetCurrentSite();
        if (Mypayment.AddOrdersPayment(entity))
        {


        }
        else
        {

        }


    }

    //合同完成
    public void Contract_Finish()
    {
        string Contract_SN;
        Contract_SN = tools.CheckStr(Request["contract_sn"]);
        if (Contract_SN.Length == 0)
        {
            pub.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
            return;
        }

        ContractInfo entity = GetContractBySn(Contract_SN);
        if (entity == null)
        {
            pub.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        //验证合同卖家
        if (entity.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        //验证合同状态
        if (entity.Contract_Delivery_Status != 4 || entity.Contract_Payment_Status != 3 || entity.Contract_Status != 1)
        {
            pub.Msg("error", "错误信息", "该合同无法执行此操作", false, "{back}");
        }

        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(entity.Contract_ID);
        if (ordersinfos != null)
        {
            foreach (OrdersInfo ordersinfo in ordersinfos)
            {
                //赠送积分
                if (ordersinfo.Orders_Total_Coin > 0)
                {
                    Supplier_Coin_AddConsume(ordersinfo.Orders_Total_Coin, "订单" + ordersinfo.Orders_SN + "完成赠送积分", ordersinfo.Orders_BuyerID, false);
                }

                //记录订单日志
                Myorder.Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "完成", "成功", "订单交易完成");

                //更新产品销售
                Orders_Product_Update_Salecount(ordersinfo.Orders_ID);

                //更新订单状态
                ordersinfo.Orders_Status = 2;
                ordersinfo.Orders_DeliveryStatus = 2;
                ordersinfo.Orders_IsReturnCoin = 1;
                MyOrders.EditOrders(ordersinfo);
            }
        }

        //改变合同状态
        entity.Contract_Status = 2;
        if (MyContract.EditContract(entity, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
        {
            Contract_Log_Add(entity.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "合同交易完成", 1, "合同交易完成");
            pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + Contract_SN);
        }
        else
        {
            pub.Msg("error", "操作失败", "操作失败", false, "{back}");
        }
    }

    //获取合同已开票金额
    public double Get_Contract_InvoiceAmount(int Contract_ID)
    {
        double amount = 0;
        IList<ContractInvoiceApplyInfo> entitys = MyContract.GetContractInvoiceApplysByContractID(Contract_ID);
        if (entitys != null)
        {
            foreach (ContractInvoiceApplyInfo entity in entitys)
            {
                amount = amount + entity.Invoice_Apply_Amount;
            }
        }
        return amount;
    }

    //申请开票
    public void Contract_Invoice_Apply()
    {
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        double contract_allprice, payed_amount, invoice_amount, apply_amount;
        int Contract_ID = 0;
        int Contract_Invoice_ID = 0;
        contract_allprice = 0;
        payed_amount = 0;
        invoice_amount = 0;
        if (contract_sn == "")
        {
            pub.Msg("error", "错误信息", "合同不支持此操作", false, "{back}");
        }
        ContractInfo Contractinfo = GetContractBySn(contract_sn);
        if (Contractinfo != null)
        {
            if (Contractinfo.Contract_BuyerID != tools.CheckInt(Session["supplier_id"].ToString()) || Contractinfo.Contract_Status < 1)
            {
                pub.Msg("error", "错误信息", "合同不支持此操作", false, "{back}");
            }
            else
            {
                Contract_ID = Contractinfo.Contract_ID;
                contract_allprice = Contractinfo.Contract_AllPrice;

                ContractInvoiceInfo entity = GetContractInvoiceByContractID(Contract_ID);
                if (entity != null)
                {
                    if (entity.Invoice_Status != 1)
                    {
                        pub.Msg("error", "错误信息", "合同不支持此操作", false, "{back}");
                    }
                    Contract_Invoice_ID = entity.Invoice_ID;
                    payed_amount = Get_Contract_PayedAmount(Contract_ID);
                    invoice_amount = Get_Contract_InvoiceAmount(Contract_ID);
                }
                else
                {
                    pub.Msg("error", "错误信息", "您未为此合同选择发票信息", false, "{back}");
                }

            }
        }
        else
        {
            pub.Msg("error", "错误信息", "合同不支持此操作", false, "{back}");
        }
        apply_amount = tools.CheckFloat(Request["apply_amount"]);
        if (apply_amount <= 0)
        {
            pub.Msg("error", "错误信息", "请输入本次申请开票金额", false, "{back}");
        }
        if (apply_amount > Math.Round((contract_allprice - invoice_amount), 2))
        {
            pub.Msg("error", "错误信息", "本次申请开票金额不可大于可开票总金额", false, "{back}");
        }
        ContractInvoiceApplyInfo invoiceapply = new ContractInvoiceApplyInfo();
        invoiceapply.Invoice_Apply_ContractID = Contract_ID;
        invoiceapply.Invoice_Apply_InvoiceID = Contract_Invoice_ID;
        invoiceapply.Invoice_Apply_ApplyAmount = apply_amount;
        invoiceapply.Invoice_Apply_Amount = 0;
        invoiceapply.Invoice_Apply_Status = 0;
        invoiceapply.Invoice_Apply_Addtime = DateTime.Now;
        invoiceapply.Invoice_Apply_Note = "";
        if (MyContract.AddContractInvoiceApply(invoiceapply))
        {
            Contract_Log_Add(Contract_ID, "", "申请开票", 1, "用户申请开票，开票金额：" + pub.FormatCurrency(apply_amount));
            pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/contract_detail.aspx?contract_sn=" + contract_sn);
        }
        else
        {
            pub.Msg("error", "错误信息", "开票申请失败，请稍后重试！", false, "{back}");
        }
    }

    //更改发票状态
    public void UpdateInvoiceStatus()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        string contract_sn = tools.CheckStr(Request["contract_sn"]);
        ContractInfo contractinfo = GetContractBySn(contract_sn);
        if (contractinfo == null)
        {
            pub.Msg("error", "错误信息", "无效的合同编号", false, "{back}");
        }
        if (contractinfo.Contract_Status == 0 || contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "您不能对此合同执行该操作！", false, "{back}");
        }
        ContractInvoiceInfo entity = MyContract.GetContractInvoiceByID(invoice_id);
        if (entity != null)
        {
            if (entity.Invoice_ContractID != contractinfo.Contract_ID)
            {
                pub.Msg("error", "错误信息", "无效的发票信息", false, "{back}");
            }
            int status = tools.NullInt(entity.Invoice_Status);
            if (status == 2)
            {
                pub.Msg("error", "错误信息", "无效的操作", false, "{back}");
            }
            entity.Invoice_Status = status + 1;
        }
        if (MyContract.EditContractInvoice(entity))
        {
            if (entity.Invoice_Status == 1)
            {
                Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "修改发票申请状态", 1, "修改发票申请状态为：启用发票申请");
            }
            else
            {
                Contract_Log_Add(contractinfo.Contract_ID, tools.NullStr(Session["supplier_companyname"]), "修改发票申请状态", 1, "修改发票申请状态为：关闭发票申请");
            }
            pub.Msg("positive", "操作成功", "操作成功", true, "myContract_detail.aspx?contract_sn=" + contract_sn);
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
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
        ContractInvoiceApplyInfo entity = MyContract.GetContractInvoiceApplyByID(apply_id);
        if (entity != null)
        {
            contract_id = entity.Invoice_Apply_ContractID;
            ContractInfo contractinfo = GetContractByID(contract_id);
            if (contractinfo != null)
            {
                if (contractinfo.Contract_Status == 0)
                {
                    pub.Msg("error", "错误信息", "操作无效!", false, "{back}");
                }
                double contract_allprice = contractinfo.Contract_AllPrice;
                invoice_amount = Get_Contract_InvoiceAmount(contract_id);
                contract_allprice = Math.Round(contract_allprice, 2);
                invoice_amount = Math.Round(invoice_amount, 2);
                if (Apply_Amount > contract_allprice)
                {
                    pub.Msg("error", "错误信息", "开票金额不可大于未开票金额!", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "合同无效!", false, "{back}");
            }

            ContractInvoiceInfo invoiceinfo = MyContract.GetContractInvoiceByID(entity.Invoice_Apply_InvoiceID);
            if (invoiceinfo != null)
            {
                invoice_tel = invoiceinfo.Invoice_Tel;
            }
            string suppliername = "";
            if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                suppliername = tools.NullStr(Session["supplier_companyname"]);
            }
            //开票
            if (Status == 1)
            {
                if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && entity.Invoice_Apply_Status == 0)
                {
                    pub.Msg("error", "错误信息", "操作无效!", false, "{back}");
                }
                if (Apply_Amount <= 0)
                {
                    pub.Msg("error", "错误信息", "请输入开票金额!", false, "{back}");
                }
                entity.Invoice_Apply_Status = 1;
                entity.Invoice_Apply_Amount = Apply_Amount;
                entity.Invoice_Apply_Note = Apply_Note;
                entity.Invoice_Apply_Addtime = DateTime.Now;
                if (MyContract.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, suppliername, "合同开票", 1, "合同开票，开票金额：" + pub.FormatCurrency(Apply_Amount));
                    pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/mycontract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //邮寄
            if (Status == 2)
            {
                if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && entity.Invoice_Apply_Status != 1)
                {
                    pub.Msg("error", "错误信息", "操作无效!", false, "{back}");
                }
                entity.Invoice_Apply_Status = 2;
                if (MyContract.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, suppliername, "合同发票已邮寄", 1, "合同发票已邮寄，请注意查收！");
                    int outcome = 0;
                    outcome = -1;
                    //发送邮件短信
                    if (invoice_tel.Length == 11)
                    {
                        //outcome = sms.Send(invoice_tel, "您的发票已邮寄，请注意查收。" + tools.NullStr(Application["site_name"]));
                        //sms.Unbind();

                        //短信推送

                    }
                    //发送邮寄邮寄
                    if (contractinfo != null)
                    {
                        string supplier_email = "";
                        SupplierInfo supplierinfo = GetSupplierByID(contractinfo.Contract_BuyerID);
                        if (supplierinfo != null)
                        {
                            supplier_email = supplierinfo.Supplier_Email;
                        }
                        //发送订单邮件
                        string mailsubject, mailbodytitle, mailbody;
                        mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的发票已邮寄";
                        mailbodytitle = mailsubject;
                        mailbody = mail_template("invoice_send", "", contractinfo.Contract_SN);
                        pub.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
                    }
                    //if (outcome < 0)
                    //{
                    //    pub.Msg("positive", "操作成功", "操作成功，短信提示发送失败。", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                    //}
                    //else if (outcome > 0)
                    //{
                    //    pub.Msg("positive", "操作成功", "操作成功，短信提示发送成功。", true, "/contract/contract_detail.aspx?contract_id=" + contract_id);
                    //}
                    //else
                    //{
                    pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/mycontract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
                    //}
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //收票
            if (Status == 3)
            {
                if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && contractinfo.Contract_BuyerID != tools.NullInt(Session["supplier_id"]) && entity.Invoice_Apply_Status != 2)
                {
                    pub.Msg("error", "错误信息", "操作无效!", false, "{back}");
                }
                entity.Invoice_Apply_Status = 3;
                if (MyContract.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, suppliername, "合同发票已签收", 1, "合同发票已签收！");
                    if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
                    {
                        pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/contract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
                    }
                    else
                    {
                        pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/mycontract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }

            //取消
            if (Status == 4)
            {
                if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]) && entity.Invoice_Apply_Status == 0)
                {
                    pub.Msg("error", "错误信息", "操作无效!", false, "{back}");
                }
                entity.Invoice_Apply_Status = 4;
                entity.Invoice_Apply_Note = Apply_Note;
                if (MyContract.EditContractInvoiceApply(entity))
                {
                    Contract_Log_Add(contract_id, suppliername, "合同开票申请取消", 1, "合同开票申请取消，取消原因：" + Apply_Note);
                    pub.Msg("positive", "操作成功", "操作成功!", true, "/supplier/mycontract_detail.aspx?contract_sn=" + contractinfo.Contract_SN);
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍候重试!", false, "{back}");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的申请信息操作!", false, "{back}");
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
        MyContractLog.AddContractLog(entity);
    }

    //合同日志
    public string Contract_Log_List(int Contract_ID)
    {
        StringBuilder HTML_Str = new StringBuilder();
        IList<ContractLogInfo> entitys = MyContractLog.GetContractLogsByContractID(Contract_ID);
        if (entitys != null)
        {
            HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#DDDDDD\">");
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td width=\"15%\" height=\"25\" align=\"center\"><b>操作</b></td><td width=\"55%\" align=\"center\"><b>描述</b></td><td width=\"15%\" align=\"center\"><b>操作人</b></td><td width=\"15%\" align=\"center\"><b>时间</b></td></tr>");
            foreach (ContractLogInfo entity in entitys)
            {
                if (entity.Log_Result == 0)
                {
                    HTML_Str.Append("<tr bgcolor=\"#ffffff\" style=\"color:#ff0000\">");
                }
                else
                {
                    HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
                }
                HTML_Str.Append("<td width=\"15%\" height=\"25\">" + entity.Log_Action + "</td><td width=\"55%\">" + entity.Log_Remark + "</td><td width=\"15%\" align=\"center\">");
                if (entity.Log_Operator == "")
                {
                    HTML_Str.Append("买方");
                }
                else
                {
                    HTML_Str.Append("卖方");
                }
                HTML_Str.Append("</td><td width=\"15%\" align=\"center\">" + entity.Log_Addtime + "</td></tr>");
            }
            HTML_Str.Append("</table>");
        }
        return HTML_Str.ToString();
    }

    //获取合同附加订单(打印)
    public string GetPrintContractOrdersByContractsID(int Contract_ID)
    {
        string strHTML = "";
        int i = 0;
        strHTML += "<table border=\"0\"  width=\"100%\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
        strHTML += "<table border=\"0\" width=\"608\" align=\"right\" class=\"list_tab\" style=\"border:1px solid #000000;\" cellpadding=\"0\" cellspacing=\"0\">";
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
                    strHTML += "        <td align=\"left\">" + pub.FormatCurrency(ordersinfo.Orders_Total_AllPrice) + "</td>";
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
                                    strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Spec + "</td>";
                                    strHTML += "<td align=\"left\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                                    strHTML += "<td align=\"left\">" + (entity.Orders_Goods_Amount) + "</td>";
                                    strHTML += "<td align=\"left\">" + (pub.FormatCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount)) + "</td>";
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
        string Contract_sn;
        string Contract_template;
        Contract_sn = tools.CheckStr(Request["Contract_sn"]);
        if (Contract_sn == "")
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{close}");
        }
        ContractInfo entity = GetContractBySn(Contract_sn);
        if (entity != null)
        {
            if (entity.Contract_BuyerID == tools.NullInt(Session["supplier_id"].ToString()) || entity.Contract_SupplierID == tools.NullInt(Session["supplier_id"].ToString()))
            {
                if (Request["action"] == "print")
                {
                    Response.Write("<script>window.print();</script>");
                }
                // /**/
                string address = "";

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
                SupplierInfo buyerinfo = GetSupplierByID(entity.Contract_BuyerID);
                if (buyerinfo != null)
                {
                    buyer_name = buyerinfo.Supplier_CompanyName;
                    buyer_tel = buyerinfo.Supplier_Phone;
                    buyer_fax = buyerinfo.Supplier_Fax;
                    buyer_zip = buyerinfo.Supplier_Zip;

                }

                //发票信息
                ContractInvoiceInfo invoiceinfo = MyContract.GetContractInvoiceByContractID(entity.Contract_ID);
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

                Contract_template = Contract_template.Replace("{orders_list}", GetPrintContractOrdersByContractsID(entity.Contract_ID));
                Contract_template = Contract_template.Replace("{contract_signtime}", entity.Contract_Addtime.ToShortDateString());
                Contract_template = Contract_template.Replace("{contract_discount}", pub.FormatCurrency(entity.Contract_Discount));
                Contract_template = Contract_template.Replace("{contract_freight}", pub.FormatCurrency(entity.Contract_Freight));
                Contract_template = Contract_template.Replace("{contract_service}", pub.FormatCurrency(entity.Contract_ServiceFee));
                Contract_template = Contract_template.Replace("{contract_allprice}", pub.FormatCurrency(entity.Contract_AllPrice));
                Contract_template = Contract_template.Replace("{contract_allpricechinese}", MyUcase.ConvertSum((entity.Contract_AllPrice).ToString()));
                Contract_template = Contract_template.Replace("{contract_payway}", entity.Contract_Payway_Name);
                Contract_template = Contract_template.Replace("{contract_deliveryname}", entity.Contract_Delivery_Name);
                Contract_template = Contract_template.Replace("{contract_address}", address);
                Response.Write(Contract_template);
                Response.Write(Contract_Orders_Goods_Print(entity.Contract_ID));
            }
            else
            {
                pub.Msg("error", "错误信息", "合同无效", false, "{close}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "合同无效", false, "{close}");
        }
    }

    #endregion

    #region "发票管理"

    public SupplierInvoiceInfo GetSupplierInvoiceByID(int ID)
    {
        return MyInvoice.GetSupplierInvoiceByID(ID);
    }

    public IList<SupplierInvoiceInfo> GetSupplierInvoicesBySupplierID(int Supplier_ID)
    {
        return MyInvoice.GetSupplierInvoicesBySupplierID(Supplier_ID);
    }

    //会员发票信息修改
    public void Supplier_Invoice_Edit()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        SupplierInvoiceInfo entity = MyInvoice.GetSupplierInvoiceByID(invoice_id);

        if (entity != null)
        {
            if (entity.Invoice_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
            entity.Invoice_Address = tools.CheckStr(Request["Invoice_Address"]);
            entity.Invoice_Name = tools.CheckStr(Request["Invoice_Name"]);
            entity.Invoice_Tel = tools.CheckStr(Request["Invoice_Tel"]);
            entity.Invoice_ZipCode = tools.CheckStr(Request["Invoice_ZipCode"]);

            int Invoice_Type = tools.CheckInt(Request["Invoice_Type"]);
            entity.Invoice_Type = Invoice_Type;
            if (Invoice_Type == 0)
            {
                string Invoice_Title = tools.CheckStr(Request["Invoice_Title"]);
                int Invoice_Content = tools.CheckInt(Request["Invoice_Content"]);
                if (Invoice_Title == "个人")
                {
                    entity.Invoice_Title = Invoice_Title;
                    entity.Invoice_Content = Invoice_Content;
                    entity.Invoice_PersonelCard = tools.CheckStr(Request["Invoice_PersonelCard"]);
                    if (!CheckPersonelCard(entity.Invoice_PersonelCard))
                    {
                        pub.Msg("info", "信息提示", "请选择填写正确格式的身份证号!", false, "{back}");
                    }
                    entity.Invoice_PersonelName = tools.CheckStr(Request["Invoice_PersonelName"]);
                    if (entity.Invoice_PersonelName == "")
                    {
                        pub.Msg("info", "信息提示", "请选择填写姓名!", false, "{back}");
                    }
                    if (Invoice_Content == 0)
                    {
                        pub.Msg("info", "信息提示", "请选择发票内容!", false, "{back}");
                    }
                }
                else if (Invoice_Title == "单位")
                {
                    entity.Invoice_Title = Invoice_Title;
                    entity.Invoice_Content = Invoice_Content;
                    entity.Invoice_FirmName = tools.CheckStr(Request["Invoice_FirmName"]);
                    entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                    entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                    entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                    entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                    entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                    if (entity.Invoice_FirmName == "")
                    {
                        pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                    }
                    //if (entity.INVOICE_VAT_BANKACOUNT.Length < 16)
                    //{
                    //    pub.Msg("info", "提示信息", "请输入至少16位有效银行账号", false, "{back}");
                    //}
                    //if (entity.INVOICE_VAT_CODE.Length != 15)
                    //{
                    //    pub.Msg("info", "提示信息", "请输入15位有效纳税人识别号", false, "{back}");
                    //}

                }
            }
            else
            {
                entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                if (entity.Invoice_VAT_Bank == "")
                {
                    pub.Msg("info", "信息提示", "请填写开户银行!", false, "{back}");
                }
                entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                if (entity.Invoice_VAT_BankAccount.Length < 16)
                {
                    pub.Msg("info", "提示信息", "请输入至少16位有效银行账号", false, "{back}");
                }
                entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                if (entity.Invoice_VAT_Code.Length != 15)
                {
                    pub.Msg("info", "提示信息", "请输入15位有效纳税人识别号", false, "{back}");
                }
                entity.Invoice_VAT_Content = tools.CheckStr(Request["Invoice_VAT_Content"]);
                if (entity.Invoice_VAT_Content == "")
                {
                    pub.Msg("info", "信息提示", "请填写发票内容!", false, "{back}");
                }
                entity.Invoice_VAT_FirmName = tools.CheckStr(Request["Invoice_VAT_FirmName"]);
                if (entity.Invoice_VAT_FirmName == "")
                {
                    pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                }
                entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                //if (entity.INVOICE_VAT_REGADDR == "")
                //{
                //    pub.Msg("info", "信息提示", "请填写注册地址!", false, "{back}");
                //}
                entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                //if (entity.INVOICE_VAT_REGTEL == "")
                //{
                //    pub.Msg("info", "信息提示", "请填写注册电话!", false, "{back}");
                //}
                entity.Invoice_VAT_Cert = tools.CheckStr(Request["Invoice_VAT_Cert"]);

                if (entity.Invoice_Address == "")
                {
                    pub.Msg("info", "信息提示", "请填写邮寄地址!", false, "{back}");
                }
                if (entity.Invoice_Name == "")
                {
                    pub.Msg("info", "信息提示", "请填写收票人姓名!", false, "{back}");
                }
                if (entity.Invoice_Tel == "")
                {
                    pub.Msg("info", "信息提示", "请填写联系电话!", false, "{back}");
                }
                if (Check_Zip(entity.Invoice_ZipCode) == false)
                {
                    pub.Msg("info", "信息提示", "邮政编码为6位数字!", false, "{back}");
                }
            }

            if (MyInvoice.EditSupplierInvoice(entity))
            {
                pub.Msg("positive", "操作成功", "发票信息修改成功", true, "account_invoice.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }

    }

    //会员发票信息列表
    public void Supplier_Invoice()
    {
        Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");

        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());

        int icount = 0;

        if (supplier_id > 0)
        {
            IList<SupplierInvoiceInfo> entitys = MyInvoice.GetSupplierInvoicesBySupplierID(supplier_id);
            if (entitys != null)
            {
                foreach (SupplierInvoiceInfo entity in entitys)
                {
                    icount = icount + 1;
                    if (entity.Invoice_Type == 0)
                    {
                        if (entity.Invoice_Title == "单位")
                        {


                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"10%\" rowspan=\"8\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td>发票抬头：" + entity.Invoice_Title + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\" colspan=\"2\">单位名称：" + entity.Invoice_FirmName + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("<tr>");
                            Response.Write("    <td width=\"35%\">单位地址：" + entity.Invoice_VAT_RegAddr + "</td><td>单位电话：" + entity.Invoice_VAT_RegTel + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">税号：" + entity.Invoice_VAT_Code + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                        }
                        else
                        {

                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"10%\" rowspan=\"6\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td></td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">发票抬头：" + entity.Invoice_Title + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">姓名：" + entity.Invoice_PersonelName + "</td><td>身份证号：" + entity.Invoice_PersonelCard + "</td>");
                            Response.Write("  </tr>");
                        }
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"修改\" onclick=\"javascript:location.href='/supplier/account_invoice_edit.aspx?invoice_id=" + entity.Invoice_ID + "';\" /> <input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"删除\" onclick=\"javascript:if(confirm('您确定删除该发票信息吗？')==false){return false;}else{location.href='/supplier/account_invoice_do.aspx?action=moveinvoice&invoice_id=" + entity.Invoice_ID + "';}\" /></td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                        Response.Write("  </tr>");
                    }
                    else
                    {


                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"10%\" rowspan=\"7\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                        if (entity.Invoice_VAT_Cert != "")
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + tools.NullStr(Application["upload_server_url"]) + entity.Invoice_VAT_Content + "\" class=\"a_t12_blue\" target=\"_blank\">查看纳税人资格证</a></td>");
                        }
                        else
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "</td>");
                        }
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">单位名称：" + entity.Invoice_VAT_FirmName + "</td><td>纳税人识别号：" + entity.Invoice_VAT_Code + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">注册地址：" + entity.Invoice_VAT_RegAddr + "</td><td>注册电话：" + entity.Invoice_VAT_RegAddr + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"修改\" onclick=\"javascript:location.href='/supplier/account_invoice_edit.aspx?invoice_id=" + entity.Invoice_ID + "';\" /> <input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"删除\" onclick=\"javascript:if(confirm('您确定删除该发票信息吗？')==false){return false;}else{location.href='/supplier/account_invoice_do.aspx?action=moveinvoice&invoice_id=" + entity.Invoice_ID + "';}\" /></td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                        Response.Write("  </tr>");
                    }
                }
            }
            else
            {
                Response.Write("<tr>");
                Response.Write("<td height=\"50\" align=\"center\"><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 您尚未维护发票信息</td></td>");
                Response.Write("</tr>");
            }
        }
        Response.Write("</table>");
    }

    //会员发票信息选择
    public void Supplier_Invoice_Option(string Contract_SN)
    {
        Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");

        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());

        int icount = 0;

        if (supplier_id > 0)
        {
            IList<SupplierInvoiceInfo> entitys = MyInvoice.GetSupplierInvoicesBySupplierID(supplier_id);
            if (entitys != null)
            {
                foreach (SupplierInvoiceInfo entity in entitys)
                {
                    icount = icount + 1;
                    if (entity.Invoice_Type == 0)
                    {
                        if (entity.Invoice_Title == "单位")
                        {
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td>发票抬头：" + entity.Invoice_Title + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\" colspan=\"2\">单位名称：" + entity.Invoice_FirmName + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">单位地址：" + entity.Invoice_VAT_RegAddr + "</td><td>单位电话：" + entity.Invoice_VAT_RegTel + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">税号：" + entity.Invoice_VAT_Code + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                        }
                        else
                        {
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td></td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">发票抬头：" + entity.Invoice_Title + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">姓名：" + entity.Invoice_PersonelName + "</td><td>身份证号：" + entity.Invoice_PersonelCard + "</td>");
                            Response.Write("  </tr>");
                        }
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"使用该发票\" onclick=\"javascript:location.href='/supplier/contract_do.aspx?action=applyinvoice&invoice_id=" + entity.Invoice_ID + "&contract_sn=" + Contract_SN + "';\" /></td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                        Response.Write("  </tr>");
                    }
                    else
                    {
                        Response.Write("  <tr>");
                        if (entity.Invoice_VAT_Cert != "")
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + tools.NullStr(Application["upload_server_url"]) + entity.Invoice_VAT_Cert + "\" class=\"a_t12_blue\" target=\"_blank\">查看纳税人资格证</a></td>");
                        }
                        else
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "</td>");
                        }
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">单位名称：" + entity.Invoice_VAT_FirmName + "</td><td>纳税人识别号：" + entity.Invoice_VAT_Code + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">注册地址：" + entity.Invoice_VAT_RegAddr + "</td><td>注册电话：" + entity.Invoice_VAT_RegAddr + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"使用该发票\" onclick=\"javascript:location.href='/supplier/contract_do.aspx?action=applyinvoice&invoice_id=" + entity.Invoice_ID + "&contract_sn=" + Contract_SN + "';\" /></td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                        Response.Write("  </tr>");
                    }
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "请在会员界面的账号管理-发票管理中维护开票信息", true, "/supplier/account_invoice_add.aspx");
            }
        }
        Response.Write("</table>");
    }

    //检查身份证号
    public bool CheckPersonelCard(string card)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^(\\d{15}|\\d{17}[x0-9])$");
        return regex.IsMatch(card);
    }

    //添加会员发票信息
    public void Supplier_Invoice_Add()
    {
        SupplierInvoiceInfo entity = new SupplierInvoiceInfo();


        int Invoice_Type = tools.CheckInt(Request.Form["Invoice_Type"]);
        entity.Invoice_Type = Invoice_Type;
        entity.Invoice_SupplierID = tools.NullInt(Session["supplier_id"]);
        entity.Invoice_Address = tools.CheckStr(Request["Invoice_Address"]);
        entity.Invoice_Name = tools.CheckStr(Request["Invoice_Name"]);
        entity.Invoice_Tel = tools.CheckStr(Request["Invoice_Tel"]);
        entity.Invoice_ZipCode = tools.CheckStr(Request["Invoice_ZipCode"]);

        if (Invoice_Type == 0)
        {
            string Invoice_Title = tools.CheckStr(Request.Form["Invoice_Title"]);
            int Invoice_Content = tools.CheckInt(Request.Form["Invoice_Content"]);
            if (Invoice_Title == "个人")
            {
                entity.Invoice_Title = Invoice_Title;
                entity.Invoice_Content = Invoice_Content;
                entity.Invoice_PersonelCard = tools.CheckStr(Request["Invoice_PersonelCard"]);
                if (!CheckPersonelCard(entity.Invoice_PersonelCard))
                {
                    pub.Msg("info", "信息提示", "请选择填写正确格式的身份证号!", false, "{back}");
                }
                entity.Invoice_PersonelName = tools.CheckStr(Request["Invoice_PersonelName"]);
                if (entity.Invoice_PersonelName == "")
                {
                    pub.Msg("info", "信息提示", "请选择填写姓名!", false, "{back}");
                }
                if (Invoice_Content == 0)
                {
                    pub.Msg("info", "信息提示", "请选择发票内容!", false, "{back}");
                }
            }
            else if (Invoice_Title == "单位")
            {
                entity.Invoice_Title = Invoice_Title;
                entity.Invoice_Content = Invoice_Content;
                entity.Invoice_FirmName = tools.CheckStr(Request["Invoice_FirmName"]);
                entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                if (entity.Invoice_FirmName == "")
                {
                    pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                }

            }
        }
        else
        {
            entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
            if (entity.Invoice_VAT_Bank == "")
            {
                pub.Msg("info", "信息提示", "请填写开户银行!", false, "{back}");
            }
            entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
            if (entity.Invoice_VAT_BankAccount.Length < 16)
            {
                pub.Msg("info", "提示信息", "请输入至少16位有效银行账号", false, "{back}");
            }
            entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
            if (entity.Invoice_VAT_Code.Length != 15)
            {
                pub.Msg("info", "提示信息", "请输入15位有效纳税人识别号", false, "{back}");
            }
            entity.Invoice_VAT_Content = tools.CheckStr(Request["Invoice_VAT_Content"]);
            if (entity.Invoice_VAT_Content == "")
            {
                pub.Msg("info", "信息提示", "请填写发票内容!", false, "{back}");
            }
            entity.Invoice_VAT_FirmName = tools.CheckStr(Request["Invoice_VAT_FirmName"]);
            if (entity.Invoice_VAT_FirmName == "")
            {
                pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
            }
            entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
            entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
            entity.Invoice_VAT_Cert = tools.CheckStr(Request["Invoice_VAT_Cert"]);
            if (entity.Invoice_Address == "")
            {
                pub.Msg("info", "信息提示", "请填写邮寄地址!", false, "{back}");
            }

            if (entity.Invoice_Name == "")
            {
                pub.Msg("info", "信息提示", "请填写收票人姓名!", false, "{back}");
            }

            if (entity.Invoice_Tel == "")
            {
                pub.Msg("info", "信息提示", "请填写联系电话!", false, "{back}");
            }
            if (Check_Zip(entity.Invoice_ZipCode) == false)
            {
                pub.Msg("info", "信息提示", "邮政编码为6位数字!", false, "{back}");
            }
        }

        if (MyInvoice.AddSupplierInvoice(entity))
        {
            pub.Msg("positive", "操作成功", "发票信息添加成功", true, "account_invoice.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }

    //删除发票信息
    public void Supplier_Invoice_Del()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        SupplierInvoiceInfo invoiceinfo = GetSupplierInvoiceByID(invoice_id);
        if (invoiceinfo != null)
        {
            if (invoiceinfo.Invoice_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "操作失败", false, "{back}");
            }
            if (MyInvoice.DelSupplierInvoice(invoice_id) > 0)
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "account_invoice.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败", false, "{back}");
            }
        }
    }

    public string Orders_Detail_InvoiceInfo(int Orders_ID)
    {
        string invoiceStr = "";

        OrdersInvoiceInfo entity = MyOrdersInvoice.GetOrdersInvoiceByOrdersID(Orders_ID);
        if (entity != null)
        {
            invoiceStr = OrdersInvoiceType(entity.Invoice_Type) + "&nbsp;&nbsp;";

            if (OrdersInvoiceType(entity.Invoice_Type) != "增值税发票")
            {
                if (entity.Invoice_Title == "个人")
                {
                    invoiceStr += entity.Invoice_Content;
                }
                else
                {
                    invoiceStr += entity.Invoice_FirmName;
                }
            }
            else
            {
                invoiceStr += entity.Invoice_VAT_FirmName;
            }
        }
        return invoiceStr;
    }

    #endregion

    #region 手机验证码

    public void Check_LoginMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
        }
        else
        {
            if (pub.Checkmobile_back(member_mobile))
            {
                if (Check_Supplier_LoginMobile(member_mobile))
                {
                    Response.Write("<font color=\"#cc0000\">该手机号码已被使用。请使用另外一个手机号码</font>");
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
            }
        }
    }

    /// <summary>
    /// 短信效验码验证
    /// </summary>
    public void Check_SMS_CheckCode()
    {
        //string verifycode = tools.CheckStr(Request["val"]);
        //string sign = tools.CheckStr(Request["sign"]);

        //if (sign.Length == 0)
        //{
        //    if (Convert.ToInt32(Session["supplier_id"]) > 0)
        //    {
        //        sign = Convert.ToString(Session["supplier_mobile"]);
        //    }
        //}

        //Dictionary<string, string> smscheckcode = Session["sms_check"] as Dictionary<string, string>;
        //if (smscheckcode == null || smscheckcode["sign"] != sign)
        //{
        //    Response.Write("<font color=\"#cc0000\">请输入短信效验码！</font>");
        //    return;
        //}

        //if (verifycode.Length == 0 || verifycode != smscheckcode["code"])
        //{
        //    Response.Write("<font color=\"#cc0000\">请输入短信效验码！</font>");
        //    return;
        //}

        //if ((Convert.ToDateTime(smscheckcode["expiration"]) - DateTime.Now).TotalSeconds < 0)
        //{
        //    Response.Write("<font color=\"#cc0000\">短信效验码过期！</font>");
        //    return;
        //}

        //Response.Write("<font color=\"#00a226\">短信效验码正确！</font>");
    }

    /// <summary>
    /// 检查手机号是否使用
    /// </summary>
    /// <param name="LoginMobile"></param>
    /// <returns></returns>
    public bool Check_Supplier_LoginMobile(string LoginMobile)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Mobile", "=", LoginMobile));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "DESC"));
        PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    /// <summary>
    /// 获取登录名手机号
    /// </summary>
    /// <param name="loginname"></param>
    /// <returns></returns>
    public string GetLoginNameMobile(string loginname)
    {

        SupplierInfo Supplierinfo = MyBLL.SupplierLogin(loginname, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (Supplierinfo != null)
        {
            return Supplierinfo.Supplier_Mobile;
        }

        //登录子账户
        SupplierSubAccountInfo subaccountinfo = MySubAccount.SubAccountLogin(loginname);
        if (subaccountinfo != null)
        {
            return subaccountinfo.Supplier_SubAccount_Mobile;
        }
        else
        {
            return string.Empty;
        }

    }

    #endregion

}

