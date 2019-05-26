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
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;

/// <summary>
///OrdersPayment 的摘要说明
/// </summary>
public class OrdersPayment
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IOrdersPayment MyBLL;
    private IOrders Myorder;
    private OrdersLog orderlog;

    public OrdersPayment()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = OrdersPaymentFactory.CreateOrdersPayment();
        Myorder = OrdersFactory.CreateOrders();
        orderlog = new OrdersLog();
    }

    public void AddOrdersPayment()
    {
        int Orders_Payment_ID = tools.CheckInt(Request.Form["Orders_Payment_ID"]);
        int Orders_Payment_OrdersID = tools.CheckInt(Request.Form["Orders_Payment_OrdersID"]);
        int Orders_Payment_PaymentStatus = tools.CheckInt(Request.Form["Orders_Payment_PaymentStatus"]);
        int Orders_Payment_SysUserID = tools.CheckInt(Request.Form["Orders_Payment_SysUserID"]);
        string Orders_Payment_DocNo = tools.CheckStr(Request.Form["Orders_Payment_DocNo"]);
        string Orders_Payment_Name = tools.CheckStr(Request.Form["Orders_Payment_Name"]);
        double Orders_Payment_Amount = tools.CheckFloat(Request.Form["Orders_Payment_Amount"]);
        string Orders_Payment_Note = tools.CheckStr(Request.Form["Orders_Payment_Note"]);

        OrdersPaymentInfo entity = new OrdersPaymentInfo();
        entity.Orders_Payment_ID = Orders_Payment_ID;
        entity.Orders_Payment_OrdersID = Orders_Payment_OrdersID;
        entity.Orders_Payment_PaymentStatus = Orders_Payment_PaymentStatus;
        entity.Orders_Payment_SysUserID = Orders_Payment_SysUserID;
        entity.Orders_Payment_DocNo = Orders_Payment_DocNo;
        entity.Orders_Payment_Name = Orders_Payment_Name;
        entity.Orders_Payment_Amount = Orders_Payment_Amount;
        entity.Orders_Payment_Note = Orders_Payment_Note;
        entity.Orders_Payment_Addtime = DateTime.Now;
        entity.Orders_Payment_Site = Public.GetCurrentSite();

        if (MyBLL.AddOrdersPayment(entity)) {
            Public.Msg("positive", "操作成功", "操作成功", true, "orders_payment_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditOrdersPayment()
    {
        int Orders_Payment_ID = tools.CheckInt(Request.Form["Orders_Payment_ID"]);
        int Orders_Payment_OrdersID = tools.CheckInt(Request.Form["Orders_Payment_OrdersID"]);
        int Orders_Payment_PaymentStatus = tools.CheckInt(Request.Form["Orders_Payment_PaymentStatus"]);
        int Orders_Payment_SysUserID = tools.CheckInt(Request.Form["Orders_Payment_SysUserID"]);
        string Orders_Payment_DocNo = tools.CheckStr(Request.Form["Orders_Payment_DocNo"]);
        string Orders_Payment_Name = tools.CheckStr(Request.Form["Orders_Payment_Name"]);
        double Orders_Payment_Amount = tools.CheckFloat(Request.Form["Orders_Payment_Amount"]);
        string Orders_Payment_Note = tools.CheckStr(Request.Form["Orders_Payment_Note"]);

        OrdersPaymentInfo entity = new OrdersPaymentInfo();
        entity.Orders_Payment_ID = Orders_Payment_ID;
        entity.Orders_Payment_OrdersID = Orders_Payment_OrdersID;
        entity.Orders_Payment_PaymentStatus = Orders_Payment_PaymentStatus;
        entity.Orders_Payment_SysUserID = Orders_Payment_SysUserID;
        entity.Orders_Payment_DocNo = Orders_Payment_DocNo;
        entity.Orders_Payment_Name = Orders_Payment_Name;
        entity.Orders_Payment_Amount = Orders_Payment_Amount;
        entity.Orders_Payment_Note = Orders_Payment_Note;
        entity.Orders_Payment_Addtime = DateTime.Now;
        entity.Orders_Payment_Site = Public.GetCurrentSite();

        if (MyBLL.EditOrdersPayment(entity)) {
            Public.Msg("positive", "操作成功", "操作成功", true, "orders_payment_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelOrdersPayment()
    {
        int Orders_Payment_ID = tools.CheckInt(Request.QueryString["Orders_Payment_ID"]);
        if (MyBLL.DelOrdersPayment(Orders_Payment_ID) > 0) {
            Public.Msg("positive", "操作成功", "操作成功", true, "orders_payment_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public OrdersPaymentInfo GetOrdersPaymentByID(int cate_id)
    {
        return MyBLL.GetOrdersPaymentByID(cate_id);
    }

    public OrdersPaymentInfo GetOrdersPaymentByOrdersID(int Orders_Id, int Payment_Status)
    {
        return MyBLL.GetOrdersPaymentByOrdersID(Orders_Id, Payment_Status);
    }

    public OrdersPaymentInfo OrdersPaymentInfo(string SN)
    {
        return MyBLL.GetOrdersPaymentBySn(SN);
    }

    public string GetOrdersPayments()
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

        OrdersInfo ordersinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            ordersinfo = Myorder.GetOrdersBySN(keyword);
            if (ordersinfo != null)
            {
                orders_ID = ordersinfo.Orders_ID;
            }
            if (orders_ID > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_OrdersID", "=", orders_ID.ToString()));
            }
        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{OrdersPaymentInfo.Orders_Payment_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{OrdersPaymentInfo.Orders_Payment_Addtime})", "<=", "0"));
        }
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        if (listtype == "payment") { Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_PaymentStatus", "=", "1")); }
        else if (listtype == "refund")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_PaymentStatus", "=", "2"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_Status", "=", "1"));
        }

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<OrdersPaymentInfo> entitys = MyBLL.GetOrdersPayments(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersPaymentInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Orders_Payment_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ordersinfo = Myorder.GetOrdersByID(entity.Orders_Payment_OrdersID);
                if (ordersinfo != null)
                {
                    jsonBuilder.Append("<a href=\\\"/orders/orders_view.aspx?orders_id=" + entity.Orders_Payment_OrdersID + "\\\">" + ordersinfo.Orders_SN + "</a>");
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                ordersinfo = null;
                jsonBuilder.Append("\",");
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_DocNo);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<span class=\\\"t12_red\\\">");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_Payment_Amount));
                jsonBuilder.Append("</span>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"orders_payment_view.aspx?orders_payment_id=" + entity.Orders_Payment_ID + "\\\"><img src=\\\"/images/btn_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"></a>");
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

    public string GetOrdersApplyPayments()
    {
        string keyword, date_start, date_end;
        int orders_ID;
        orders_ID = 0;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        OrdersInfo ordersinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            ordersinfo = Myorder.GetOrdersBySN(keyword);
            if (ordersinfo != null)
            {
                orders_ID = ordersinfo.Orders_ID;
            }
            if (orders_ID > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_OrdersID", "=", orders_ID.ToString()));
            }

        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{OrdersPaymentInfo.Orders_Payment_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{OrdersPaymentInfo.Orders_Payment_Addtime})", "<=", "0"));
        }
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_PaymentStatus", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_Status", "=", "0"));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<OrdersPaymentInfo> entitys = MyBLL.GetOrdersPayments(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersPaymentInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Orders_Payment_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ordersinfo = Myorder.GetOrdersByID(entity.Orders_Payment_OrdersID);
                if (ordersinfo != null)
                {
                    jsonBuilder.Append("<a href=\\\"/orders/orders_view.aspx?orders_id=" + entity.Orders_Payment_OrdersID + "\\\">" + ordersinfo.Orders_SN + "</a>");
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                ordersinfo = null;
                jsonBuilder.Append("\",");
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Orders_Payment_DocNo));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<span class=\\\"t12_red\\\">");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_Payment_ApplyAmount));
                jsonBuilder.Append("</span>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Orders_Payment_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payment_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"orders_payment_applyview.aspx?orders_payment_id=" + entity.Orders_Payment_ID + "\\\"><img src=\\\"/images/btn_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"></a>");
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

    public void changeOrdersPayment()
    {
        int Orders_Payment_ID = tools.CheckInt(Request["Orders_Payment_ID"]);
        int Orders_Payment_OrdersID = tools.CheckInt(Request["Orders_Payment_OrdersID"]);
        double Orders_Payment_Amount = tools.CheckInt(Request["Orders_Payment_Amount"]);
        string paymentmemo = "";
        OrdersPaymentInfo entity = MyBLL.GetOrdersPaymentByID(Orders_Payment_ID);
        if (entity != null)
        {
            entity.Orders_Payment_Amount = Orders_Payment_Amount;
            entity.Orders_Payment_Status = 1;
            paymentmemo = entity.Orders_Payment_DocNo;
            if (MyBLL.EditOrdersPayment(entity))
            {
                orderlog.Orders_Log(Orders_Payment_OrdersID, Session["User_Name"].ToString(), "退款申请单审核", "通过", "订单号：" + paymentmemo + " 退款申请通过");
                Public.Msg("positive", "操作成功", "操作成功", true, "orders_payment_applylist.aspx");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void OrdersPayment_Export()
    {
        string payment_id = tools.CheckStr(Request.QueryString["payment_id"]);
        if (payment_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(payment_id, 1) == ",") { payment_id = payment_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        OrdersInfo ordersinfo = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_ID", "in", payment_id));
        Query.OrderInfos.Add(new OrderInfo("OrdersPaymentInfo.Orders_Payment_ID", "DESC"));
        IList<OrdersPaymentInfo> entitys = MyBLL.GetOrdersPayments(Query);

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "订单编号", "单据编码", "单据类型","金额", "支付方式", "操作时间","备注" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (OrdersPaymentInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr[0] = entity.Orders_Payment_ID;
            ordersinfo = Myorder.GetOrdersByID(entity.Orders_Payment_OrdersID);
            if (ordersinfo != null)
            {
                dr[1] = ordersinfo.Orders_SN;
            }
            else
            {
                dr[1] = "";
            }
            
            dr[2] = entity.Orders_Payment_DocNo;
            if (entity.Orders_Payment_PaymentStatus == 1)
            {
                dr[3] = "付款单";
            }
            else
            {
                dr[3] = "退款单";
            }
            dr[4] = entity.Orders_Payment_Amount;
            dr[5] = entity.Orders_Payment_Name;
            dr[6] = entity.Orders_Payment_Addtime;
            dr[7] = entity.Orders_Payment_Note;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }

}
