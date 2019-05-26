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
///OrdersDelivery 的摘要说明
/// </summary>
public class OrdersDelivery
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IOrdersDelivery MyBLL;
    private IOrders Myorder;

    public OrdersDelivery()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = OrdersDeliveryFactory.CreateOrdersDelivery();
        Myorder = OrdersFactory.CreateOrders();
    }


    public void AddOrdersDelivery()
    {
        int Orders_Delivery_ID = tools.CheckInt(Request.Form["Orders_Delivery_ID"]);
        int Orders_Delivery_OrdersID = tools.CheckInt(Request.Form["Orders_Delivery_OrdersID"]);
        int Orders_Delivery_DeliveryStatus = tools.CheckInt(Request.Form["Orders_Delivery_DeliveryStatus"]);
        int Orders_Delivery_SysUserID = tools.CheckInt(Request.Form["Orders_Delivery_SysUserID"]);
        string Orders_Delivery_DocNo = tools.CheckStr(Request.Form["Orders_Delivery_DocNo"]);
        string Orders_Delivery_Name = tools.CheckStr(Request.Form["Orders_Delivery_Name"]);
        double Orders_Delivery_Amount = tools.CheckFloat(Request.Form["Orders_Delivery_Amount"]);
        string Orders_Delivery_Note = tools.CheckStr(Request.Form["Orders_Delivery_Note"]);

        OrdersDeliveryInfo entity = new OrdersDeliveryInfo();
        entity.Orders_Delivery_ID = Orders_Delivery_ID;
        entity.Orders_Delivery_OrdersID = Orders_Delivery_OrdersID;
        entity.Orders_Delivery_DeliveryStatus = Orders_Delivery_DeliveryStatus;
        entity.Orders_Delivery_SysUserID = Orders_Delivery_SysUserID;
        entity.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
        entity.Orders_Delivery_Name = Orders_Delivery_Name;
        entity.Orders_Delivery_Amount = Orders_Delivery_Amount;
        entity.Orders_Delivery_Note = Orders_Delivery_Note;
        entity.Orders_Delivery_Addtime = DateTime.Now;
        entity.Orders_Delivery_Site = Public.GetCurrentSite();

        if (MyBLL.AddOrdersDelivery(entity,Public.GetUserPrivilege())) {
            Public.Msg("positive", "操作成功", "操作成功", true, "Orders_Delivery_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditOrdersDelivery()
    {
        int Orders_Delivery_ID = tools.CheckInt(Request.Form["Orders_Delivery_ID"]);
        int Orders_Delivery_OrdersID = tools.CheckInt(Request.Form["Orders_Delivery_OrdersID"]);
        int Orders_Delivery_DeliveryStatus = tools.CheckInt(Request.Form["Orders_Delivery_DeliveryStatus"]);
        int Orders_Delivery_SysUserID = tools.CheckInt(Request.Form["Orders_Delivery_SysUserID"]);
        string Orders_Delivery_DocNo = tools.CheckStr(Request.Form["Orders_Delivery_DocNo"]);
        string Orders_Delivery_Name = tools.CheckStr(Request.Form["Orders_Delivery_Name"]);
        double Orders_Delivery_Amount = tools.CheckFloat(Request.Form["Orders_Delivery_Amount"]);
        string Orders_Delivery_Note = tools.CheckStr(Request.Form["Orders_Delivery_Note"]);

        OrdersDeliveryInfo entity = new OrdersDeliveryInfo();
        entity.Orders_Delivery_ID = Orders_Delivery_ID;
        entity.Orders_Delivery_OrdersID = Orders_Delivery_OrdersID;
        entity.Orders_Delivery_DeliveryStatus = Orders_Delivery_DeliveryStatus;
        entity.Orders_Delivery_SysUserID = Orders_Delivery_SysUserID;
        entity.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
        entity.Orders_Delivery_Name = Orders_Delivery_Name;
        entity.Orders_Delivery_Amount = Orders_Delivery_Amount;
        entity.Orders_Delivery_Note = Orders_Delivery_Note;
        entity.Orders_Delivery_Addtime = DateTime.Now;
        entity.Orders_Delivery_Site = Public.GetCurrentSite();

        if (MyBLL.EditOrdersDelivery(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Orders_Delivery_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelOrdersDelivery()
    {
        int Orders_Delivery_ID = tools.CheckInt(Request.QueryString["Orders_Delivery_ID"]);
        if (MyBLL.DelOrdersDelivery(Orders_Delivery_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Orders_Delivery_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public OrdersDeliveryInfo GetOrdersDeliveryByID(int cate_id)
    {
        return MyBLL.GetOrdersDeliveryByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetOrdersDeliverys()
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersDeliveryInfo.Orders_Delivery_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            ordersinfo = Myorder.GetOrdersBySN(keyword);
            if (ordersinfo != null)
            {
                orders_ID = ordersinfo.Orders_ID;
            }
            if (orders_ID > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersDeliveryInfo.Orders_Delivery_OrdersID", "=", orders_ID.ToString()));
            }
            
        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{OrdersDeliveryInfo.Orders_Delivery_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{OrdersDeliveryInfo.Orders_Delivery_Addtime})", "<=", "0"));
        }
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        if (listtype == "shipping") { Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersDeliveryInfo.Orders_Delivery_DeliveryStatus", "=", "1")); }
        else if (listtype == "returned") { Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersDeliveryInfo.Orders_Delivery_DeliveryStatus", "=", "5")); }

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<OrdersDeliveryInfo> entitys = MyBLL.GetOrdersDeliverys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersDeliveryInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Orders_Delivery_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Delivery_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                ordersinfo = Myorder.GetOrdersByID(entity.Orders_Delivery_OrdersID);
                if (ordersinfo != null)
                {
                    jsonBuilder.Append("<a href=\\\"/orders/orders_view.aspx?orders_id=" + entity.Orders_Delivery_OrdersID + "\\\">" + ordersinfo.Orders_SN + "</a>");
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                ordersinfo = null;
                jsonBuilder.Append("\",");

                

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Delivery_DocNo);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<span class=\\\"t12_red\\\">");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_Delivery_Amount));
                jsonBuilder.Append("</span>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Delivery_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("95515ef3-e035-4400-b4fc-da4d8f5a530f"))
                {
                    if (entity.Orders_Delivery_Status == 0 && entity.Orders_Delivery_DeliveryStatus == 5)
                    {
                        jsonBuilder.Append("<a href=\\\"orders_deliverypaycancel.aspx?orders_delivery_id=" + entity.Orders_Delivery_ID + "&orders_id=" + entity.Orders_Delivery_OrdersID + "\\\">申请退款</a> ");
                    }
                }
                jsonBuilder.Append("<a href=\\\"orders_delivery_view.aspx?orders_delivery_id=" + entity.Orders_Delivery_ID + "\\\"><img src=\\\"/images/btn_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"></a>");
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

    public OrdersDeliveryInfo GetOrdersDeliveryByOrdersID(int Orders_ID, int Delivery_Status)
    {
        return MyBLL.GetOrdersDeliveryByOrdersID(Orders_ID, Delivery_Status, Public.GetUserPrivilege());
    }

    public void OrdersDelivery_Export()
    {
        string delivery_id = tools.CheckStr(Request.QueryString["delivery_id"]);
        if (delivery_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(delivery_id, 1) == ",") { delivery_id = delivery_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        OrdersInfo ordersinfo = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersDeliveryInfo.Orders_Delivery_ID", "in", delivery_id));
        Query.OrderInfos.Add(new OrderInfo("OrdersDeliveryInfo.Orders_Delivery_ID", "DESC"));
        IList<OrdersDeliveryInfo> entitys = MyBLL.GetOrdersDeliverys(Query,Public.GetUserPrivilege());

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "订单编号", "单据编码", "单据类型", "物流费用", "物流方式","物流公司","物流单号", "操作时间", "备注" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (OrdersDeliveryInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr[0] = entity.Orders_Delivery_ID;
            ordersinfo = Myorder.GetOrdersByID(entity.Orders_Delivery_OrdersID);
            if (ordersinfo != null)
            {
                dr[1] = ordersinfo.Orders_SN;
            }
            else
            {
                dr[1] = "";
            }

            dr[2] = entity.Orders_Delivery_DocNo;
            if (entity.Orders_Delivery_DeliveryStatus == 1)
            {
                dr[3] = "发货单";
            }
            else
            {
                dr[3] = "退货单";
            }
            dr[4] = entity.Orders_Delivery_Amount;
            dr[5] = entity.Orders_Delivery_Name;
            dr[6] = entity.Orders_Delivery_companyName;
            dr[7] = entity.Orders_Delivery_Code;
            dr[8] = entity.Orders_Delivery_Addtime;
            dr[9] = entity.Orders_Delivery_Note;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }



  

    //发货单产品
    public bool Delivery_Goods_List(int Orders_ID, int Delivery_ID, int ispreview)
    {
        bool isaccept = true;
        string strHTML = "";
        int freighted_amount;
        int icount = 0;
        strHTML += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cell_table\">";
        strHTML += "<tr>";
        strHTML += "<td width=\"80\" height=\"23\" align=\"center\" class=\"cell_title1\">产品编号</td>";
        strHTML += "<td height=\"23\" align=\"center\" class=\"cell_title1\">产品名称</td>";
        strHTML += "<td width=\"70\" height=\"23\" align=\"center\" class=\"cell_title1\">规格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">价格</td>";
        strHTML += "<td width=\"60\" height=\"23\" align=\"center\" class=\"cell_title1\">发货数量</td>";

        strHTML += "</tr>";
        IList<OrdersDeliveryGoodsInfo> GoodsListAll = MyBLL.GetOrdersDeliveryGoods(Delivery_ID);
        string orders_sn;
        if (GoodsListAll != null)
        {
            foreach (OrdersDeliveryGoodsInfo entity1 in GoodsListAll)
            {
                freighted_amount = (int)Math.Round(entity1.Orders_Delivery_Goods_ProductAmount);
                strHTML += "<tr>";
                strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity1.Orders_Delivery_Goods_ProductCode + "</td>";
                strHTML += "<td height=\"23\" align=\"left\" class=\"cell_content\">" + entity1.Orders_Delivery_Goods_ProductName + "</td>";
                strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity1.Orders_Delivery_Goods_ProductSpec + "</td>";
                strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + Public.DisplayCurrency(entity1.Orders_Delivery_Goods_ProductPrice) + "</td>";
                strHTML += "<td height=\"23\" align=\"center\" class=\"cell_content\">" + entity1.Orders_Delivery_Goods_ProductAmount + "</td>";

                strHTML += "</tr>";
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


}
