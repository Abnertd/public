using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.B2C.BLL.MEM;
using System.Net;
using System.IO;

/// <summary>
///Orders 的摘要说明
/// </summary>
public class Orders
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private Public_Class pub = new Public_Class();
    private OrdersLog orderlog = new OrdersLog();
    private Credit credit;
    private IEncrypt encrypt;
    private IOrders MyOrders;
    private IOrdersLog Mylog;
    private Addr addr = new Addr();
    private IDeliveryWay MyDelivery;
    private IPayWay MyPayway;
    private IProduct MyProduct;
    private IPackage Mypackage;
    private IOrdersDelivery Myorderdelivery;
    private IPromotionFavorFee MyFavorFee;
    private IPromotionFavorCoupon MyCoupon;
    private IPromotionFavorPolicy MyPolicy;
    private IOrdersBackApply MyBack;
    private PageURL pageurl;
    private ISupplier MySupplier;
    private IDeliveryTime Mydelierytime;
    private IMember MyMember;
    private IMemberConsumption MyConsumption;
    private IMemberAccountLog MyAccountLog;
    private IPromotionFavor MyFavor;
    private IPromotionCouponRule MyCouponRule;
    private IOrdersInvoice MyInvoice;
    private IOrdersBackApplyProduct MyBackProduct;
    private IOrdersGoodsTmp Mygoodstmp;
    private IOrdersPayment Myorederspayment;
    SysMessage messageclass = new SysMessage();

    string GuaranteeAccNo;
    string GuaranteeAccNm;
    ZhongXinUtil.SendMessages sendmessages;





    public Orders()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        credit = new Credit();
        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyOrders = OrdersFactory.CreateOrders();
        Mylog = OrdersLogFactory.CreateOrdersLog();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        MyPayway = PayWayFactory.CreatePayWay();
        MyProduct = ProductFactory.CreateProduct();
        Mypackage = PackageFactory.CreatePackage();
        Myorderdelivery = OrdersDeliveryFactory.CreateOrdersDelivery();
        MyFavorFee = PromotionFavorFeeFactory.CreatePromotionFavorFee();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();
        MyBack = OrdersBackApplyFactory.CreateOrdersBackApply();
        MyPolicy = PromotionFavorPolicyFactory.CreatePromotionFavorPolicy();
        MySupplier = SupplierFactory.CreateSupplier();
        Mydelierytime = DeliveryTimeFactory.CreateDeliveryTime();
        MyMember = MemberFactory.CreateMember();
        MyConsumption = MemberConsumptionFactory.CreateMemberConsumption();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        MyAccountLog = MemberAccountLogFactory.CreateMemberAccountLog();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyCouponRule = PromotionCouponRuleFactory.CreatePromotionFavorCoupon();
        MyInvoice = OrdersInvoiceFactory.CreateOrdersInvoice();
        MyBackProduct = OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct();
        Mygoodstmp = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        Myorederspayment = OrdersPaymentFactory.CreateOrdersPayment();


        //交易保证金
        GuaranteeAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccno"];
        GuaranteeAccNm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccnm"];
        sendmessages = new ZhongXinUtil.SendMessages();
    }

    public int Member_Order_Count(int member_id, string count_type)
    {
        int Order_Count = 0;
        if (member_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", member_id.ToString()));
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.U_Orders_IsShow", "=", "0"));
            switch (count_type)
            {
                case "order_unprocessed": //待确认的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "order_processing":  //已确认的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    break;
                case "order_success":  //待评价的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                    break;
                case "order_faiture":  //交易失败的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                    break;
                case "order_nopay":  //交易失败的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "in", "0,1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
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

    public IList<OrdersGoodsTmpInfo> Get_Orders_Carts(int Orders_ID)
    {
        IList<OrdersGoodsInfo> OrderGoods = MyOrders.GetGoodsListByOrderID(Orders_ID);
        OrdersGoodsTmpInfo cart;
        IList<OrdersGoodsTmpInfo> Cartinfos = null;
        if (OrderGoods != null)
        {
            Cartinfos = new List<OrdersGoodsTmpInfo>();
            foreach (OrdersGoodsInfo goods in OrderGoods)
            {
                cart = new OrdersGoodsTmpInfo();
                cart.Orders_Goods_ID = goods.Orders_Goods_ID;
                cart.Orders_Goods_BuyerID = goods.Orders_Goods_ID;
                cart.Orders_Goods_Amount = goods.Orders_Goods_Amount;
                cart.Orders_Goods_ParentID = goods.Orders_Goods_ParentID;
                cart.Orders_Goods_Product_BrandID = goods.Orders_Goods_Product_BrandID;
                cart.Orders_Goods_Product_CateID = goods.Orders_Goods_Product_CateID;
                cart.Orders_Goods_Product_ID = goods.Orders_Goods_Product_ID;
                cart.Orders_Goods_Product_IsFavor = goods.Orders_Goods_Product_IsFavor;
                cart.Orders_Goods_Product_Price = goods.Orders_Goods_Product_Price;
                cart.Orders_Goods_Type = goods.Orders_Goods_Type;
                Cartinfos.Add(cart);
                cart = null;
            }
        }
        return Cartinfos;
    }

    public void Orders_Log(int orders_id, string log_operator, string log_action, string log_result, string log_remark)
    {
        OrdersLogInfo orderslog = new OrdersLogInfo();
        orderslog.Orders_Log_ID = 0;
        orderslog.Orders_Log_Operator = log_operator;
        orderslog.Orders_Log_OrdersID = orders_id;
        orderslog.Orders_Log_Action = log_action;
        orderslog.Orders_Log_Remark = log_remark;
        orderslog.Orders_Log_Result = log_result;
        orderslog.Orders_Log_Addtime = DateTime.Now;
        Mylog.AddOrdersLog(orderslog);
    }

    //获取会员的最后一个订单
    public void GetEndOrdersInfoByMemberID()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", Session["member_id"].ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));
        IList<OrdersInfo> entitys = MyOrders.GetOrderss(Query);
        OrdersInfo entity = null;
        if (entitys != null)
        {
            entity = entitys[0];
        }
        if (entity != null)
        {
            if (tools.NullStr(Session["Orders_Address_ID"]) == "0" || tools.NullStr(Session["Orders_Address_ID"]) == "")
            {
                Session["Orders_Address_ID"] = entity.Orders_Address_ID;
                Session["Orders_Address_State"] = entity.Orders_Address_State;
            }
            if (tools.NullStr(Session["Orders_Delivery_ID"]) == "0" || tools.NullStr(Session["Orders_Delivery_ID"]) == "")
            {
                Session["Orders_Delivery_ID"] = entity.Orders_Delivery;
            }
            if (tools.NullStr(Session["Orders_Payway_ID"]) == "0" || tools.NullStr(Session["Orders_Payway_ID"]) == "")
            {
                Session["Orders_Payway_ID"] = entity.Orders_Payway;
            }
            if (tools.NullStr(Session["Orders_DeliveryTime_ID"]) == "0" || tools.NullStr(Session["Orders_DeliveryTime_ID"]) == "")
            {
                Session["Orders_DeliveryTime_ID"] = entity.Orders_Delivery_Time_ID;
            }
        }
    }

    //获取商家的最后一个订单
    public void GetEndOrdersInfoBySupplierID()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", tools.NullStr(Session["member_id"])));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerType", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));
        IList<OrdersInfo> entitys = MyOrders.GetOrderss(Query);
        OrdersInfo entity = null;
        if (entitys != null)
        {
            entity = entitys[0];
        }
        if (entity != null)
        {
            if (tools.NullStr(Session["Orders_Address_ID"]) == "0" || tools.NullStr(Session["Orders_Address_ID"]) == "")
            {
                Session["Orders_Address_ID"] = entity.Orders_Address_ID;
                Session["Orders_Address_State"] = entity.Orders_Address_State;
            }
            if (tools.NullStr(Session["Orders_Delivery_ID"]) == "0" || tools.NullStr(Session["Orders_Delivery_ID"]) == "")
            {
                Session["Orders_Delivery_ID"] = entity.Orders_Delivery;
            }
            if (tools.NullStr(Session["Orders_Payway_ID"]) == "0" || tools.NullStr(Session["Orders_Payway_ID"]) == "")
            {
                Session["Orders_Payway_ID"] = entity.Orders_Payway;
            }
            if (tools.NullStr(Session["Orders_DeliveryTime_ID"]) == "0" || tools.NullStr(Session["Orders_DeliveryTime_ID"]) == "")
            {
                Session["Orders_DeliveryTime_ID"] = entity.Orders_Delivery_Time_ID;
            }
        }
    }


    public virtual void Order_Detail()
    {
        int order_id = tools.CheckInt(Request.QueryString["order_id"]);
        if (MyOrders.DelOrders(order_id) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    public OrdersInfo GetOrdersInfoBySN(string sn)
    {
        return MyOrders.GetOrdersBySN(sn);
    }

    public OrdersInfo GetOrdersByID(int ID)
    {
        return MyOrders.GetOrdersByID(ID);
    }

    public OrdersInfo GetOrdersBySn(string code)
    {
        return MyOrders.GetOrdersBySN(code);
    }

    //根据合同ID获取相对应的订单ID
    public int getOrdersIDByContractID(int ContractID)
    {
        int Ordersid = 0;
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_ContractID", "=", ContractID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));
        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderss(Query);
        if (ordersinfos != null)
        {
            int i = 0;
            foreach (OrdersInfo ordersinfo in ordersinfos)
            {
                i++;
                if (i == 1)
                {
                    Ordersid = ordersinfo.Orders_ID;
                }

            }
        }


        return Ordersid;


    }


    public IList<OrdersGoodsInfo> GetOrdersGoodsInfoBySN(string orders_sn)
    {
        IList<OrdersGoodsInfo> entitys = null;
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            entitys = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
        }
        return entitys;

    }

    public string GetEvaluateProductName(int product_id, int Orders_ID)
    {
        string product_name = "--";
        IList<OrdersGoodsInfo> entitys = null;
        entitys = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                if (entity.Orders_Goods_Product_ID == product_id && entity.Orders_Goods_ParentID != 2)
                {
                    product_name = "<a class=\"a_t12_blue\" href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "</a> <span class=\"t12_orange\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</span> 元";
                    break;
                }
            }
        }
        return product_name;
    }

    #region 订单相关状态

    public string OrdersStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未确认"; break;
            case 1:
                resultstr = "已确认"; break;
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
                resultstr = "已支付"; break;
        }
        return resultstr;
    }

    public string DeliveryStatus(int status)
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
                resultstr = "<img src=\"/images/icon_success.gif\" align=\"absmiddle\" border=\"0\" />已开票"; break;
            case 2:
                resultstr = "已退票"; break;
            case 3:
                resultstr = "不需要发票"; break;
        }

        return resultstr;
    }

    #endregion

    #region 信贷状态

    /// <summary>
    /// 信贷信息申请状态
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public string LoanProjectStatus(string status)
    {
        string resultStr = "";

        switch (status)
        {
            case "CHECK_WAIT":
                resultStr = "审核中";
                break;
            case "PUSH_WAIT":
                resultStr = "已审核待推进";
                break;
            case "ISSUE_WAIT":
                resultStr = "放款中";
                break;
            case "REPAY_NORMAL":
                resultStr = "正常";
                break;
            case "OVERDUE":
                resultStr = "逾期";
                break;
            case "CHECK_FAIL":
                resultStr = "已拒绝";
                break;
            case "FINISH_GIVEUP":
                resultStr = "已撤销";
                break;
            case "FINISH_NORMAL":
                resultStr = "已完成";
                break;
            case "BAD":
                resultStr = "坏账";
                break;
        }
        return resultStr;
    }

    /// <summary>
    /// 信贷分期状态
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public string LoanProjectDetailStatus(string status)
    {
        string resultStr = "";

        switch (status)
        {
            case "USING":
                resultStr = "还款中";
                break;
            case "INIT":
                resultStr = "未到期";
                break;
            case "OVERDUE":
                resultStr = "逾期";
                break;
            case "PAID":
                resultStr = "已还清";
                break;
        }
        return resultStr;
    }

    #endregion

    //获取待评价商品
    public string GetAuditingProduct(int Orders_ID)
    {
        IList<OrdersGoodsInfo> OrderGoods = MyOrders.GetGoodsListByOrderID(Orders_ID);
        string html = "";
        if (OrderGoods != null)
        {
            html += "<ul>";
            foreach (OrdersGoodsInfo entity in OrderGoods)
            {
                if (entity.Orders_Goods_Type == 0)
                {
                    html += "<li><a href=\"/product/reviews_add.aspx?product_id=" + entity.Orders_Goods_Product_ID + "&sn=" + Orders_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" style=\"width:100px; height:100px;\"/></a></li>";
                }
            }
            html += "</ul>";
        }
        return html;
    }

    //评价订单中的商品
    public void AuditingOrdersProduct(int Orders_ID, int Product_ID)
    {
        IList<OrdersGoodsInfo> OrderGoods = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (OrderGoods != null)
        {
            foreach (OrdersGoodsInfo entity in OrderGoods)
            {
                if (entity.Orders_Goods_Type == 0)
                {
                    if (entity.Orders_Goods_Product_ID == Product_ID)
                    {
                        //entity.U_Orders_Goods_ISAuditing = 1;
                        MyOrders.EditOrdersGoods(entity);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 订单页选项卡
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string Orders_TabControl(string type)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<ul class=\"yz_lst31\">");
        strHTML.Append("	<li " + (type == "all" ? "class=\"on\"" : "") + " id=\"n01\"><a href=\"order_all.aspx\">全部订单(<span>" + Member_Order_Count(member_id, "order_all") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "unprocessed" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"order_unprocessed.aspx\">未确认的订单(<span>" + Member_Order_Count(member_id, "order_unprocessed") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "processing" ? "class=\"on\"" : "") + " id=\"n03\"><a href=\"order_processing.aspx\">处理中的订单(<span>" + Member_Order_Count(member_id, "order_processing") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "success" ? "class=\"on\"" : "") + " id=\"n04\"><a href=\"order_success.aspx\">交易成功的订单(<span>" + Member_Order_Count(member_id, "order_success") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "faiture" ? "class=\"on\"" : "") + " id=\"n05\"><a href=\"order_faiture.aspx\">交易失败的订单(<span>" + Member_Order_Count(member_id, "order_faiture") + "</span>)</a></li>");
        strHTML.Append("</ul><div class=\"clear\"></div>");

        return strHTML.ToString();
    }

    public void Orders_Detail(string uses)
    {
        string orders_sn;
        string delivery_url = "";
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
            {
                if (uses == "normal")
                {
                    Response.Write("<div  class=\"status\">");
                    Response.Write("<div class=\"status1\">");
                    string Orders_Status = "";
                    if (ordersinfo.Orders_Status != 1 || ordersinfo.Orders_DeliveryStatus == 0)
                    {
                        Orders_Status = OrdersStatus(ordersinfo.Orders_Status);
                    }
                    else
                    {
                        Orders_Status = DeliveryStatus(ordersinfo.Orders_DeliveryStatus);
                    }
                    Response.Write("<div class=\"status1-1\"> <span class=\"hao\">订单号：<a href=\"/member/order_all.aspx\" class=\"hei1\">" + ordersinfo.Orders_SN + "</a> <span class=\"xia\"> 下单时间：" + ordersinfo.Orders_Addtime + " </span>    订单状态：" + Orders_Status + "</span>");
                    if (ordersinfo.Orders_Status < 2 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_Payway != 3)
                    {
                        Response.Write("<a href=\"javascript:;\" onclick=\"location.href='/pay/pay_index.aspx?sn=" + ordersinfo.Orders_SN + "&refresh=' + Math.random()\" class=\"fu\"><img src=\"/images/status-fukuan.jpg\" width=\"69\" height=\"26\" align=\"absmiddle\"/></a>"); //支付
                    }
                    //if (ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
                    //{
                    //    Response.Write("<a href=\"javascript:;\" onclick=\"location.href='order_delivery.aspx?orders_sn=" + ordersinfo.Orders_SN + "'\" class=\"fu\"><img src=\"/images/status-xiugai.jpg\" width=\"67\" height=\"27\" align=\"absmiddle\"/></a>");//修改
                    //}
                    if (ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
                    {
                        Response.Write("<a href=\"order_close.aspx?orders_sn=" + ordersinfo.Orders_SN + "&time=" + DateTime.Now.Millisecond.ToString() + "\" id=\"btn_cancel_" + ordersinfo.Orders_ID + "\"  ><img src=\"/images/status-quxiao.jpg\" width=\"68\" height=\"27\" align=\"absmiddle\"/></a><script>$('#btn_cancel_" + ordersinfo.Orders_ID + "').zxxbox();</script>"); //取消
                    }
                    if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_DeliveryStatus == 1)
                    {
                        Response.Write("<a href=\"javascript:;\" onclick=\"location.href='order_do.aspx?action=accept&orders_sn=" + ordersinfo.Orders_SN + "'\"><img src=\"/images/status-shouhuo.jpg\" width=\"87\" height=\"27\" align=\"absmiddle\"/></a>");
                    }
                    Response.Write("</div>");

                    Response.Write("<div  class=\"status1-2\">");
                    Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"  class=\"niu\">");
                    Response.Write("<tr>");
                    Response.Write("<td width=\"24\"><img src=\"/images/status.jpg\" width=\"11\" height=\"11\" /></td>");
                    Response.Write("<td width=\"160\"><img src=\"/images/status2.jpg\" width=\"147\" height=\"15\" /></td>");
                    if (ordersinfo.Orders_DeliveryStatus >= 1 && ordersinfo.Orders_DeliveryStatus != 6)
                    {
                        Response.Write("<td width=\"24\"><img src=\"/images/status.jpg\" width=\"11\" height=\"11\" /></td>");
                        Response.Write("<td width=\"160\"><img src=\"/images/status2.jpg\" width=\"147\" height=\"12\" /></td>");
                    }
                    else
                    {
                        Response.Write("<td width=\"24\"><img src=\"/images/status3.jpg\" width=\"11\" height=\"11\" /></td>");
                        Response.Write("<td width=\"160\"><img src=\"/images/status4.jpg\" width=\"147\" height=\"12\" /></td>");
                    }
                    if (ordersinfo.Orders_DeliveryStatus >= 2 && ordersinfo.Orders_DeliveryStatus != 6)
                    {
                        Response.Write("<td width=\"24\"><img src=\"/images/status.jpg\" width=\"11\" height=\"11\" /></td>");
                        Response.Write("<td width=\"160\"><img src=\"/images/status2.jpg\" width=\"147\" height=\"12\" /></td>");
                    }
                    else
                    {
                        Response.Write("<td width=\"24\"><img src=\"/images/status3.jpg\" width=\"11\" height=\"11\" /></td>");
                        Response.Write("<td width=\"180\"><img src=\"/images/status4.jpg\" width=\"147\" height=\"12 /></td>");
                    }
                    if (ordersinfo.Orders_Status == 2)
                    {
                        Response.Write("<td width=\"24\"><img src=\"/images/status.jpg\" width=\"11\" height=\"11\" /></td>");
                    }
                    else
                    {
                        Response.Write("<td width=\"24\"><img  style=\"margin-left:10px;\" src=\"/images/status3.jpg\" width=\"11\" height=\"11\" /></td>");
                    }
                    Response.Write("</tr>");
                    Response.Write("</table>");
                    Response.Write("<table width=\"730\" border=\"0\"  class=\"an\">");
                    Response.Write("<tr>");
                    Response.Write("  <td width=\"260\" height=\"20\" class=\"sp\">提交订单</td>");
                    Response.Write("  <td width=\"260\" height=\"20\" class=\"sp\">商品出库</td>");
                    Response.Write("  <td width=\"260\" height=\"20\" class=\"sp\">等待验收</td>");
                    Response.Write("  <td width=\"270\" height=\"20\" class=\"sp\">  完成</td>");
                    Response.Write("</tr>");
                    Response.Write("<tr>");
                    Response.Write("  <td height=\"20\">" + ordersinfo.Orders_Addtime + "</td>");
                    if (ordersinfo.Orders_DeliveryStatus >= 1 && ordersinfo.Orders_DeliveryStatus != 6)
                    {
                        IList<OrdersLogInfo> orderlogs = Mylog.GetOrdersLogsByOrdersID(ordersinfo.Orders_ID);
                        string time = "";
                        if (orderlogs != null)
                        {
                            foreach (OrdersLogInfo entity in orderlogs)
                            {
                                if (entity.Orders_Log_Action == "发货")
                                {
                                    time = entity.Orders_Log_Addtime.ToString();
                                    break;
                                }
                            }
                        }
                        Response.Write("  <td height=\"20\">" + time + "</td>");
                    }
                    else
                    {
                        Response.Write("  <td height=\"20\"></td>");
                    }
                    if (ordersinfo.Orders_DeliveryStatus >= 2 && ordersinfo.Orders_DeliveryStatus != 6)
                    {
                        Response.Write("  <td height=\"20\">" + ordersinfo.Orders_DeliveryStatus_Time + "</td>");
                    }
                    else
                    {
                        Response.Write("  <td height=\"20\"></td>");
                    }
                    if (ordersinfo.Orders_Status == 2)
                    {
                        IList<OrdersLogInfo> orderlogs = Mylog.GetOrdersLogsByOrdersID(ordersinfo.Orders_ID);
                        string time = "";
                        if (orderlogs != null)
                        {
                            foreach (OrdersLogInfo entity in orderlogs)
                            {
                                if (entity.Orders_Log_Action == "完成")
                                {
                                    time = entity.Orders_Log_Addtime.ToString();
                                    break;
                                }
                            }
                        }
                        Response.Write("  <td height=\"20\">" + time + "</td>");
                        Response.Write("  <td height=\"20\"></td>");
                    }
                    else
                    {
                        Response.Write("  <td height=\"20\"></td>");
                    }

                    Response.Write("</table>");
                    Response.Write("</div>");
                    Response.Write("</div>");
                    //付款信息
                    Response.Write("<div class=\"status2\">");
                    Response.Write("  <h3>付款信息</h3>");
                    Response.Write("  <div class=\"status2-1\">");
                    Response.Write("    <table width=\"881\" border=\"0\">");
                    Response.Write("      <tr><td><table width=\"881\" border=\"0\">");
                    Response.Write("        <tr>");
                    Response.Write("          <td width=\"63\">付款方式：</td>");
                    Response.Write("          <td width=\"226\">" + ordersinfo.Orders_Payway_Name + "</td>");
                    Response.Write("          <td width=\"71\">运费金额：</td>");
                    Response.Write("          <td width=\"187\">" + pub.FormatCurrency(ordersinfo.Orders_Total_Freight) + "</td>");
                    Response.Write("          <td width=\"81\" rowspan=\"3\">应支付金额：</td>");
                    Response.Write("          <td width=\"227\" rowspan=\"3\">" + pub.FormatCurrency(ordersinfo.Orders_Total_AllPrice - ordersinfo.Orders_Account_Pay - ordersinfo.Orders_Total_FreightDiscount - ordersinfo.Orders_Total_PriceDiscount) + "</td>");
                    Response.Write("        </tr>");
                    Response.Write("        <tr>");
                    Response.Write("          <td>商品金额：</td>");
                    Response.Write("          <td>" + pub.FormatCurrency(ordersinfo.Orders_Total_Price) + "</td>");
                    Response.Write("          <td>运费优惠：</td>");
                    Response.Write("          <td>" + pub.FormatCurrency(ordersinfo.Orders_Total_FreightDiscount) + "</td>");
                    Response.Write("        </tr>");
                    Response.Write("        <tr>");
                    Response.Write("          <td>优惠金额：</td>");
                    Response.Write("          <td>" + pub.FormatCurrency(ordersinfo.Orders_Total_PriceDiscount) + "</td>");
                    Response.Write("          <td>抵扣金额：</td>");
                    Response.Write("          <td>" + pub.FormatCurrency(ordersinfo.Orders_Account_Pay) + "</td>");
                    Response.Write("        </tr>");
                    Response.Write("        </table></td></tr></table>");
                    Response.Write("</div></div>");
                    //订单信息
                    Response.Write("<div class=\"status2\">");
                    Response.Write("  <h3>订单信息</h3>");
                    Response.Write("  <div class=\"status2-2\">");
                    Response.Write("    <table width=\"861\" border=\"0\" class=\"xin\">");
                    Response.Write("      <tr><td colspan=\"2\" class=\"jiacu\">收货人信息</td></tr>");
                    Response.Write("      <tr><td width=\"79\" height=\"20\">收&nbsp; 货&nbsp; 人：</td>");
                    Response.Write("      <td width=\"772\" height=\"20\">" + ordersinfo.Orders_Address_Name + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\"><span style=\"margin-right:24px;\">地</span>区：</td>");
                    Response.Write("      <td height=\"20\">" + addr.DisplayAddress(ordersinfo.Orders_Address_State, ordersinfo.Orders_Address_City, ordersinfo.Orders_Address_County) + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\">收货地址：</td><td height=\"20\">" + ordersinfo.Orders_Address_StreetAddress + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\">邮政编码：</td><td height=\"20\">" + ordersinfo.Orders_Address_Zip + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\">固定电话：</td><td height=\"20\">" + ordersinfo.Orders_Address_Phone_Number + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\">移动电话：</td><td height=\"20\">" + ordersinfo.Orders_Address_Mobile + "</td></tr>");
                    Response.Write("    </table>");
                    Response.Write("    <p class=\"xiana\"></p>");
                    Response.Write("    <table width=\"861\" border=\"0\">");
                    Response.Write("      <tr><td colspan=\"2\" class=\"jiacu\">支付及配送方式</td></tr>");
                    Response.Write("      <tr><td width=\"82\" height=\"20\">配送方式：</td><td width=\"769\" height=\"20\">" + ordersinfo.Orders_Delivery_Name + "</td></tr>");
                    Response.Write("      <tr><td width=\"82\" height=\"20\">支付方式：</td><td width=\"769\" height=\"20\">" + ordersinfo.Orders_Payway_Name + "</td></tr>");
                    Response.Write("      <tr><td height=\"20\"><span style=\"margin-right:24px;\">运</span>费：</td><td height=\"20\">" + pub.FormatCurrency(ordersinfo.Orders_Total_Freight) + "</td></tr>");
                    DeliveryTimeInfo deliverytimeinfo = Mydelierytime.GetDeliveryTimeByID(ordersinfo.Orders_Delivery_Time_ID);
                    if (deliverytimeinfo != null)
                    {
                        Response.Write("      <tr><td height=\"20\">送货时间：</td><td height=\"20\">" + deliverytimeinfo.Delivery_Time_Name + "</td></tr>");
                    }
                    else
                    {
                        Response.Write("      <tr><td height=\"20\">送货时间：</td><td height=\"20\"> -- </td></tr>");
                    }
                    Response.Write("    </table>");
                    Response.Write("    <p class=\"xiana\"></p>");

                    OrdersDeliveryInfo DEntity = Myorderdelivery.GetOrdersDeliveryByOrdersID(ordersinfo.Orders_ID, 1, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                    if (DEntity != null)
                    {
                        Random ran = new Random(10);
                        string com = pub.GetDeliveryCode(DEntity.Orders_Delivery_companyName);
                        string apiurl = "http://api.kuaidi100.com/api?id=d2fcad63758650e7&com=" + com + "&nu=" + DEntity.Orders_Delivery_Code + "&show=2&muti=1&time=" + ran.Next(100);
                        WebRequest request__1 = WebRequest.Create(apiurl);
                        WebResponse response__2 = request__1.GetResponse();
                        Stream stream = response__2.GetResponseStream();
                        Encoding encode = Encoding.UTF8;
                        StreamReader reader = new StreamReader(stream, encode);
                        string detail = reader.ReadToEnd();
                        detail = detail.Replace("<table width='520px' border='0' cellspacing='0' cellpadding='0' id='showtablecontext' style='border-collapse:collapse;border-spacing:0;'>", "");
                        detail = detail.Replace("<tr><td width='163' style='background:#64AADB;border:1px solid #75C2EF;color:#FFFFFF;font-size:14px;font-weight:bold;height:28px;line-height:28px;text-indent:15px;'>时间</td>", "");
                        detail = detail.Replace("<td width='354' style='background:#64AADB;border:1px solid #75C2EF;color:#FFFFFF;font-size:14px;font-weight:bold;height:28px;line-height:28px;text-indent:15px;'>地点和跟踪进度</td></tr>", "");
                        detail = detail.Replace("</table>", "");
                        detail = detail.Replace("width='163'", "width='180' height='22'");
                        detail = detail.Replace("border:1px", "border:0px");
                        Response.Write("    <table border=\"0\">");
                        Response.Write("      <tr><td colspan=\"2\" class=\"jiacu\">物流信息 物流公司：" + DEntity.Orders_Delivery_companyName + "&nbsp;&nbsp;物流单号：" + DEntity.Orders_Delivery_Code + "</td></tr>");
                        if (detail.IndexOf("errordiv") == -1)
                        {
                            Response.Write(detail);
                        }
                        else
                        {
                            DeliveryWayInfo WayInfo = MyDelivery.GetDeliveryWayByID(ordersinfo.Orders_Delivery, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
                            if (WayInfo != null)
                            {
                                Response.Write("      <tr><td colspan=\"2\" style=\"color:red;\">没有查到相关信息。");
                                if (WayInfo.Delivery_Way_Url != "")
                                {
                                    Response.Write("您可以 <a href=\"" + WayInfo.Delivery_Way_Url + "\" class=\"hei1\" style=\"color:red;\" target=\"_blank\">点此链接</a> 进行手工查询。");
                                }
                                Response.Write("</td></tr>");
                            }
                        }
                        Response.Write("    </table>");
                        Response.Write("    <p class=\"xiana\"></p>");
                    }

                    Response.Write("    <table width=\"861\" border=\"0\">");
                    Response.Write("      <tr><td colspan=\"2\" class=\"jiacu\">订单备注<a name=\"pinglun\"></a></td></tr>");
                    Response.Write("      <tr id=\"old_note\"><td width=\"200\">" + ordersinfo.Orders_Note + "</td><td>");
                    if (ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
                    {
                        Response.Write(" <input name=\"btn_ordernote\" type=\"button\" class=\"buttonupload\" id=\"btn_ordernote\" value=\"修改\" onclick=\"$('#old_note').hide();$('#new_note').show();\"/>");
                    }
                    Response.Write("      </td></tr>");
                    Response.Write("      <tr id=\"new_note\" style=\"display:none;\"><td width=\"200\">");
                    Response.Write("       <form id=\"frm_edit\" name=\"frm_edit\" method=\"post\" action=\"order_do.aspx\">");
                    Response.Write("       <textarea name=\"orders_note\" id=\"orders_note\" cols=\"45\" rows=\"5\">" + ordersinfo.Orders_Note + "</textarea>");
                    Response.Write("       <font color=\"#FF0000\">100个字以内</font>");
                    Response.Write("       <input name=\"orders_sn\" type=\"hidden\" id=\"orders_sn\" value=\"" + ordersinfo.Orders_SN + "\" />");
                    Response.Write("       <input name=\"action\" type=\"hidden\" id=\"action\" value=\"ordersnoteedit\" />");
                    Response.Write("       </td><td><input name=\"button\" type=\"submit\" class=\"buttonupload\" id=\"button\" value=\"保存\" /></form></td></tr>");
                    Response.Write("       ");
                    Response.Write("    </table>");
                    if (ordersinfo.Orders_Status == 3)
                    {
                        Response.Write("    <p class=\"xiana\"></p>");
                        Response.Write("    <table width=\"861\" border=\"0\">");
                        Response.Write("      <tr><td class=\"jiacu\">订单失败原因</td></tr>");
                        Response.Write("      <tr><td>");
                        if (ordersinfo.Orders_Fail_SysUserID == 0)
                        {
                            Response.Write("您");
                        }
                        else
                        {
                            Response.Write("管理员");
                        }
                        Response.Write("于" + ordersinfo.Orders_Fail_Addtime + "关闭该订单");
                        if (ordersinfo.Orders_Fail_Note.Length > 0)
                        {
                            Response.Write("<br>备注：" + ordersinfo.Orders_Fail_Note + "</td>");
                        }
                        Response.Write("      </td></tr>");
                        Response.Write("    </table>");
                    }
                    Response.Write("    <p class=\"xiana\"></p>");
                    Response.Write("    <p class=\"jiacu\">商品清单</p>");

                    Order_Detail_Goods(uses, ordersinfo.Orders_ID, ordersinfo.Orders_Total_Price, ordersinfo.Orders_Total_Freight, ordersinfo.Orders_Total_PriceDiscount, ordersinfo.Orders_Total_FreightDiscount, ordersinfo.Orders_Total_AllPrice, ordersinfo.Orders_Account_Pay);

                    Response.Write("  </div>");
                    Response.Write("</div>");
                    Response.Write("</div>");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
        }
    }
    public void OrdersIsShow_Edit(int Orders_id, bool issupplier)
    {

        OrdersInfo entity = MyOrders.GetOrdersByID(Orders_id);
        if (entity != null)
        {
            entity.Orders_IsShow = 0;
            if (MyOrders.EditOrders(entity))
            {
                if (issupplier == true)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_list.aspx");

                }

            }
            else
            {
                if (issupplier == true)
                {
                    pub.Msg("error", "错误信息", "订单删除失败", false, "/supplier/order_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "订单删除失败", false, "/member/order_list.aspx");
                }


            }
        }
        else
        {
            if (issupplier == true)
            {
                pub.Msg("error", "错误信息", "订单删除失败", false, "/supplier/order_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "订单删除失败", false, "/member/order_list.aspx");
            }

        }



    }

    //订单完成状态改变 并且保证金账号余额返回买家
    public void Orders_Complete_Edit(int Orders_id)//1、Orders_id订单编号
    {
        try
        {

            ZhongXin mycredit = new ZhongXin();
            int supplier_id = -1;
            string Supplier_Name = "";
            // ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
            ZhongXinInfo RecvAccountInfo = null;

            OrdersInfo entity = MyOrders.GetOrdersByID(Orders_id);//2、通过订单编号获取订单详细信息
            if (entity != null)
            {
                entity.Orders_Status = 2;
                //通过订单获取买家ID
                int member_id = entity.Orders_BuyerID;

                #region 计算订单总签收金额

                QueryInfo query = new QueryInfo();
                query.PageSize = 0;
                query.CurrentPage = 1;
                query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersDeliveryInfo.Orders_Delivery_OrdersID", "=", Orders_id.ToString()));
                IList<Glaer.Trade.B2C.Model.OrdersDeliveryInfo> ListOrdersDeliveryEntity =
                    Myorderdelivery.GetOrdersDeliverys(query, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                double SumPrice = 0.0;
                double EndPrice = 0.0;
                if (ListOrdersDeliveryEntity == null)
                {
                    pub.Msg("positive", "操作失败", "订单不存在", false, "/supplier/order_list.aspx");
                }
                foreach (var item in ListOrdersDeliveryEntity)
                {
                    SumPrice += item.Orders_Delivery_Amount;
                }
                Glaer.Trade.B2C.Model.OrdersPaymentInfo OrdersPaymentEntity =
                     Myorederspayment.GetOrdersPaymentByOrdersID(Orders_id, 0);
                EndPrice = OrdersPaymentEntity.Orders_Payment_Amount - SumPrice;
                #endregion
                //通过会员ID获取买家对应的卖家ID(由于每次会员注册成功后,会生成对应的商家 因此商家ID与买家ID一样)
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));//3、获取卖家账号
                if (supplierinfo != null)
                {

                    supplier_id = supplierinfo.Supplier_ID;//4、卖家ID
                    if (supplier_id > 0)
                    {
                        //通过卖家ID 查询ZhongXin 数据库表  获取相应中信账号信息
                        RecvAccountInfo = mycredit.GetZhongXinBySuppleir(member_id);//5、通过卖家ID 查询ZhongXin 数据库表  获取相应中信账号信息
                        if (RecvAccountInfo != null)
                        {
                            Supplier_Name = RecvAccountInfo.CompanyName;//6、获取卖家公司名称

                        }

                    }
                }
                if (RecvAccountInfo != null)
                {
                    //decimal accountremain1 = 0;
                    //余额账户资金
                    //accountremain1 = 
                    double accountremain = tools.NullDbl(EndPrice);
                    string Notes = "";
                    //GuaranteeAccNo 交易担保账户   SubAccount:收款方  商家子账号  CompanyName 收款方商家公司名称 accountremain 中信账号上该商家余额
                    string strResult = string.Empty;
                    //sendmessages.Transfer(GuaranteeAccNo, RecvAccountInfo.SubAccount, RecvAccountInfo.CompanyName, "", accountremain, ref strResult, "");
                    //若该买家在交易担保账户中的本订单担保余额大于0,则点击完成订单  所有的余额退回到买家
                    if (accountremain > 0)
                    {
                        ZhongXin zhongxin = new ZhongXin();
                        string GuaranteeAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccno"];
                        string GuaranteeAccNm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccnm"];
                        if (GuaranteeAccNo == null && GuaranteeAccNm == null)
                        {
                            pub.Msg("error", "错误信息", GuaranteeAccNo, false, "/supplier/order_list.aspx");
                        }
                        bool b = sendmessages.Transfer(GuaranteeAccNo, RecvAccountInfo.SubAccount, RecvAccountInfo.CompanyName, Notes, accountremain, ref strResult, GuaranteeAccNm);
                        if (b)
                        {
                            if (MyOrders.EditOrders(entity))
                            {
                                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");

                            }
                            else
                            {
                                pub.Msg("error", "错误信息", "订单完成失败", false, "/supplier/order_list.aspx");
                            }
                        }
                        else
                        {
                            //return strResult;
                            pub.Msg("error", "错误信息" + GuaranteeAccNo, "订单完成失败", false, "/supplier/order_list.aspx");
                        }


                    }
                    else if (accountremain <= 0)
                    {
                        if (MyOrders.EditOrders(entity))
                        {
                            pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");

                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "订单完成失败", false, "/supplier/order_list.aspx");
                        }
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "订单完成失败", false, "/supplier/order_list.aspx");
                    }

                }
                else
                {
                    pub.Msg("error", "错误信息", "未找到买家中信账户信息", false, "/supplier/order_list.aspx");
                }


            }
            else
            {
                pub.Msg("error", "错误信息", "订单信息不存在", false, "/supplier/order_list.aspx");
            }

        }
        catch (Exception ex)
        {
            pub.Msg("error", "错误信息", ex.Message, false, "/supplier/order_list.aspx");
            //throw ex;
        }
    }


    //退货
    public void Orders_Edit(int Orders_id, bool issupplier)
    {

        OrdersInfo entity = MyOrders.GetOrdersByID(Orders_id);
        if (entity != null)
        {
            //entity.Orders_Status = 4;
            if (MyOrders.EditOrders(entity))
            {
                if (issupplier == true)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_list.aspx");

                }

            }
            else
            {
                if (issupplier == true)
                {
                    pub.Msg("error", "错误信息", "申请退货失败", false, "/supplier/order_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "申请退货失败", false, "/member/order_list.aspx");
                }


            }
        }
        else
        {
            if (issupplier == true)
            {
                pub.Msg("error", "错误信息", "订单删除失败", false, "/supplier/order_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "订单删除失败", false, "/member/order_list.aspx");
            }

        }



    }
    //会员修改合同订单商品信息
    //订单修改
    public void Orders_Edit(int IsDetails, string typeString)
    {
        double product_total = 0;

        string OrdersID = Request["Orders_ID"];

        int Orders_ID = Convert.ToInt32(OrdersID);
        //SetOrdersGoods_To_GoodsTmp(Orders_ID);
        int Total_Coin = 0;
        double Total_Price = 0;
        double Total_MKTPrice = 0;
        double Total_Allprice = 0;
        int delivery_id, payment_id, parent_id;
        string delivery_name, payment_name, log_remark;
        double delivery_fee, price_discount, fee_discount;
        string member_name = "";
        payment_name = "";
        delivery_name = "";
        string member_mobile = "";
        string Supplier_CompanyName = "";
        parent_id = 0;
        log_remark = "商品金额由：{old_product_price}修改为：{new_product_price};";
        log_remark = log_remark + "价格优惠由：{old_favor_price}修改为：{new_favor_price};订单总金额由：{old_total_price}修改为：{new_total_price};";

        delivery_fee = tools.CheckFloat(Request.Form["Orders_Total_Freight"]);
        price_discount = tools.CheckFloat(Request.Form["Orders_Total_PriceDiscount"]);
        fee_discount = tools.CheckFloat(Request.Form["Orders_Total_FreightDiscount"]);



        delivery_id = tools.CheckInt(Request.Form["order_delivery"]);
        payment_id = tools.CheckInt(Request.Form["order_payway"]);
        string Orders_Total_PriceDiscount_Note = tools.CheckStr(Request["Orders_Total_PriceDiscount_Note"]);



        OrdersGoodsInfo ordergoods = null;
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            double Orders_Total_Freight = ordersinfo.Orders_Total_Freight;
            MemberInfo memberinfo = MyMember.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));


            //Supplier supplierinfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID,pub.CreateUserPrivilege(""));
            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierinfo != null)
            {
                Supplier_CompanyName = supplierinfo.Supplier_CompanyName;

            }
            if (memberinfo != null)
            {
                member_name = memberinfo.Member_NickName;
                member_mobile = memberinfo.Member_LoginMobile;
            }
            if (ordersinfo.Orders_Status < 1 && ordersinfo.Orders_DeliveryStatus == 0 && ordersinfo.Orders_PaymentStatus == 0)
            {


                IList<OrdersGoodsInfo> OrdersGoodsInfos = GetOrdersGoodsInfoBySN(ordersinfo.Orders_SN);
                if (OrdersGoodsInfos != null)
                {


                    foreach (OrdersGoodsInfo goods in OrdersGoodsInfos)
                    {
                        string buy_amount = tools.CheckStr(Request["buy_amount_" + goods.Orders_Goods_ID]);
                        string buy_price = tools.CheckStr(Request["buy_price_" + goods.Orders_Goods_ID]);
                        double product_buy_amount = Convert.ToDouble(buy_amount);
                        double product_buy_price = Convert.ToDouble(buy_price);
                        product_total = product_total + product_buy_amount * product_buy_price;


                        ordergoods = new OrdersGoodsInfo();
                        ordergoods.Orders_Goods_ID = goods.Orders_Goods_ID;


                        ordergoods.Orders_Goods_Type = goods.Orders_Goods_Type;
                        ordergoods.Orders_Goods_ParentID = parent_id;
                        ordergoods.Orders_Goods_Product_SupplierID = goods.Orders_Goods_Product_SupplierID;
                        ordergoods.Orders_Goods_OrdersID = Orders_ID;
                        ordergoods.Orders_Goods_Product_ID = goods.Orders_Goods_Product_ID;
                        ordergoods.Orders_Goods_Product_Code = goods.Orders_Goods_Product_Code;
                        ordergoods.Orders_Goods_Product_CateID = goods.Orders_Goods_Product_CateID;
                        ordergoods.Orders_Goods_Product_BrandID = goods.Orders_Goods_Product_BrandID;
                        ordergoods.Orders_Goods_Product_Name = goods.Orders_Goods_Product_Name;
                        ordergoods.Orders_Goods_Product_Img = goods.Orders_Goods_Product_Img;

                        ordergoods.Orders_Goods_Product_MKTPrice = goods.Orders_Goods_Product_MKTPrice;
                        ordergoods.Orders_Goods_Product_Maker = goods.Orders_Goods_Product_Maker;
                        ordergoods.Orders_Goods_Product_Spec = goods.Orders_Goods_Product_Spec;
                        ordergoods.Orders_Goods_Product_AuthorizeCode = goods.Orders_Goods_Product_AuthorizeCode;
                        ordergoods.Orders_Goods_Product_brokerage = goods.Orders_Goods_Product_brokerage;
                        ordergoods.Orders_Goods_Product_SalePrice = goods.Orders_Goods_Product_SalePrice;
                        ordergoods.Orders_Goods_Product_PurchasingPrice = goods.Orders_Goods_Product_PurchasingPrice;
                        ordergoods.Orders_Goods_Product_Coin = goods.Orders_Goods_Product_Coin;
                        ordergoods.Orders_Goods_Product_IsFavor = goods.Orders_Goods_Product_IsFavor;
                        ordergoods.Orders_Goods_Product_UseCoin = goods.Orders_Goods_Product_UseCoin;





                        ordergoods.Orders_Goods_Product_Price = product_buy_price;

                        ordergoods.Orders_Goods_Amount = product_buy_amount;

                        Orders_Log(Orders_ID, Supplier_CompanyName, "订单修改", "成功", "商品金额由:" + goods.Orders_Goods_Product_Price + " 修改为:" + product_buy_price + ",商品数量由:" + goods.Orders_Goods_Amount + "修改为:" + product_buy_amount + "");
                        MyOrders.EditOrdersGoods(ordergoods);



                    }
                    if (fee_discount > delivery_fee)
                    {
                        fee_discount = delivery_fee;
                    }
                    if (price_discount > Total_Price)
                    {
                        price_discount = Total_Price;
                    }

                    delivery_fee = tools.CheckFloat(delivery_fee.ToString("0.00"));
                    price_discount = tools.CheckFloat(price_discount.ToString("0.00"));
                    fee_discount = tools.CheckFloat(fee_discount.ToString("0.00"));


                    Total_Allprice = Total_Price + delivery_fee - price_discount - fee_discount;

                    Total_Allprice = tools.CheckFloat(Total_Allprice.ToString("0.00"));
                    Total_MKTPrice = tools.CheckFloat(Total_MKTPrice.ToString("0.00"));



                    //OrdersInfo ordersinfo1 = GetOrdersByID(Orders_ID);
                    if (ordersinfo != null)
                    {

                        ordersinfo.Orders_Total_Price = product_total;
                        ordersinfo.Orders_Total_AllPrice = product_total + ordersinfo.Orders_Total_Freight;
                        //ordersinfo.Orders_Status = 1;
                        //ordersinfo.Orders_SupplierStatus = 1;
                        ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;





                        //int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
                        //string select_price = tools.CheckStr(Request["select_price"]);
                        //string select_freight = tools.CheckStr(Request["select_freight"]);
                        //double Orders_Total_PriceDiscount = tools.NullDbl(Request["Orders_Total_PriceDiscount"]);
                        //double Orders_Total_FreightDiscount = tools.NullDbl(Request["Orders_Total_FreightDiscount"]);
                        //string Orders_Total_PriceDiscount_Note = tools.CheckStr(Request["Orders_Total_PriceDiscount_Note"]);
                        // string Orders_ContractAdd = Request["Orders_ContractAdd"];

                        //MemberInfo memInfo = null;
                        //string member_mobile = "";
                        //OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, tools.NullInt(Session["supplier_id"]));
                        //if (ordersinfo != null)
                        //{
                        //memInfo = MyMEM.GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        //if (memInfo != null)
                        //{
                        //    member_mobile = memInfo.Member_LoginMobile;
                        //}

                        if (ordersinfo.Orders_Status == 0)
                        {
                            ordersinfo.Orders_Status = 1;
                            ordersinfo.Orders_SupplierStatus = 1;
                            //if (select_price == "-")
                            //{
                            //    ordersinfo.Orders_Total_PriceDiscount = -Orders_Total_PriceDiscount;
                            //    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice - Orders_Total_PriceDiscount;
                            //}
                            //else
                            //{
                            //    ordersinfo.Orders_Total_PriceDiscount = Orders_Total_PriceDiscount;
                            //    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice + Orders_Total_PriceDiscount;
                            //}
                            //ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;

                            //if (select_freight == "-")
                            //{
                            //    ordersinfo.Orders_Total_FreightDiscount = -Orders_Total_FreightDiscount;
                            //    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice - Orders_Total_FreightDiscount;
                            //}
                            //else
                            //{
                            //    ordersinfo.Orders_Total_FreightDiscount = Orders_Total_FreightDiscount;
                            //    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_AllPrice + Orders_Total_FreightDiscount;
                            //}

                            if (ordersinfo.Orders_Payway == 2)
                            {
                                ordersinfo.Orders_ApplyCreditAmount = product_total + ordersinfo.Orders_Total_Freight;
                            }

                            //ordersinfo.Orders_ContractAdd = Orders_ContractAdd;

                            if (MyOrders.EditOrders(ordersinfo))
                            {
                                messageclass.SendMessage(1, 1, ordersinfo.Orders_BuyerID, 0, "您的订单 " + ordersinfo.Orders_SN + " 供货商已确认！");


                                //发送短信
                                //new SMS().Send(tools.NullStr(member_mobile), "supplier_confirm_orders_remind", ordersinfo.Orders_SN);
                                if (typeString == "Confirm_Your_Order")
                                {
                                    //短信推送
                                    SMS mySMS = new SMS();
                                    mySMS.Send(ordersinfo.Orders_Address_Mobile, ordersinfo.Orders_SN, "supplier_confirm_orders_remind");
                                    Orders_Log(Orders_ID, tools.NullStr(Supplier_CompanyName.ToString()), "确认", "成功", "供应商确认订单");
                                }

                            }
                        }
                        //pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
                        //}











                        // ordersinfo1.Orders_Total_FreightDiscount_Note = "123";



                        //MyOrders.EditOrders(ordersinfo1);
                    }
                }


                if (IsDetails == 1)
                {
                    Response.Redirect("/supplier/order_detail.aspx?orders_sn=" + ordersinfo.Orders_SN);
                }
                else
                {
                    Response.Redirect("/supplier/order_list.aspx");
                }



            }
            else
            {
                pub.Msg("error", "错误提示", "商品清单不能修改！", false, "{back}");
            }
        }
    }





    //将订单产品导入待编辑产品
    public void SetOrdersGoods_To_GoodsTmp(int Orders_ID)
    {
        int parent_id = 0;
        Mygoodstmp.ClearOrdersGoodsTmpByOrdersID(Orders_ID);
        OrdersGoodsTmpInfo goodstmp = null;
        IList<OrdersGoodsInfo> GoodsListAll = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (GoodsListAll != null)
        {
            IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(GoodsListAll, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                GoodsListSub = OrdersGoodsSearch(GoodsListAll, entity.Orders_Goods_ID);

                goodstmp = new OrdersGoodsTmpInfo();
                goodstmp.Orders_Goods_Type = entity.Orders_Goods_Type;
                goodstmp.Orders_Goods_BuyerID = 0;
                goodstmp.Orders_Goods_SessionID = "";
                goodstmp.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                goodstmp.Orders_Goods_ParentID = entity.Orders_Goods_ParentID;
                goodstmp.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                goodstmp.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                goodstmp.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                goodstmp.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                goodstmp.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                goodstmp.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                goodstmp.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                goodstmp.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                goodstmp.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                goodstmp.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                goodstmp.Orders_Goods_Product_DeliveryDate = entity.Orders_Goods_Product_DeliveryDate;
                goodstmp.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                goodstmp.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                goodstmp.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                goodstmp.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                goodstmp.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                goodstmp.Orders_Goods_Addtime = DateTime.Now;
                goodstmp.Orders_Goods_Product_SalePrice = entity.Orders_Goods_Product_SalePrice;
                goodstmp.Orders_Goods_Product_PurchasingPrice = entity.Orders_Goods_Product_PurchasingPrice;
                goodstmp.Orders_Goods_Product_brokerage = entity.Orders_Goods_Product_brokerage;
                goodstmp.Orders_Goods_OrdersID = entity.Orders_Goods_OrdersID;

                Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                goodstmp = null;

                if (GoodsListSub.Count > 0)
                {
                    parent_id = Mygoodstmp.Get_Orders_Goods_ParentID("", entity.Orders_Goods_Product_ID, entity.Orders_Goods_Type);
                    if (parent_id > 0)
                    {
                        foreach (OrdersGoodsInfo goods in GoodsListSub)
                        {
                            goodstmp = new OrdersGoodsTmpInfo();
                            goodstmp.Orders_Goods_Type = goods.Orders_Goods_Type;
                            goodstmp.Orders_Goods_BuyerID = 0;
                            goodstmp.Orders_Goods_SessionID = "";
                            goodstmp.Orders_Goods_Product_SupplierID = goods.Orders_Goods_Product_SupplierID;
                            goodstmp.Orders_Goods_ParentID = parent_id;
                            goodstmp.Orders_Goods_Product_ID = goods.Orders_Goods_Product_ID;
                            goodstmp.Orders_Goods_Product_Code = goods.Orders_Goods_Product_Code;
                            goodstmp.Orders_Goods_Product_CateID = goods.Orders_Goods_Product_CateID;
                            goodstmp.Orders_Goods_Product_BrandID = goods.Orders_Goods_Product_BrandID;
                            goodstmp.Orders_Goods_Product_Name = goods.Orders_Goods_Product_Name;
                            goodstmp.Orders_Goods_Product_Img = goods.Orders_Goods_Product_Img;
                            goodstmp.Orders_Goods_Product_Price = goods.Orders_Goods_Product_Price;
                            goodstmp.Orders_Goods_Product_MKTPrice = goods.Orders_Goods_Product_MKTPrice;
                            goodstmp.Orders_Goods_Product_Maker = goods.Orders_Goods_Product_Maker;
                            goodstmp.Orders_Goods_Product_Spec = goods.Orders_Goods_Product_Spec;
                            goodstmp.Orders_Goods_Product_DeliveryDate = entity.Orders_Goods_Product_DeliveryDate;
                            goodstmp.Orders_Goods_Product_AuthorizeCode = goods.Orders_Goods_Product_AuthorizeCode;
                            goodstmp.Orders_Goods_Product_Coin = goods.Orders_Goods_Product_Coin;
                            goodstmp.Orders_Goods_Product_IsFavor = goods.Orders_Goods_Product_IsFavor;
                            goodstmp.Orders_Goods_Product_UseCoin = goods.Orders_Goods_Product_UseCoin;
                            goodstmp.Orders_Goods_Amount = goods.Orders_Goods_Amount;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_Product_SalePrice = goods.Orders_Goods_Product_SalePrice;
                            goodstmp.Orders_Goods_Product_PurchasingPrice = goods.Orders_Goods_Product_PurchasingPrice;
                            goodstmp.Orders_Goods_Product_brokerage = goods.Orders_Goods_Product_brokerage;
                            goodstmp.Orders_Goods_OrdersID = goods.Orders_Goods_OrdersID;

                            Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }


            }
        }
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

    public IList<OrdersGoodsInfo> OrdersGoodsSearch(IList<OrdersGoodsInfo> GoodsList, int ParentID)
    {
        IList<OrdersGoodsInfo> OrdersGoodsList = new List<OrdersGoodsInfo>();
        foreach (OrdersGoodsInfo entity in GoodsList)
        {
            if (entity.Orders_Goods_ParentID == ParentID) { OrdersGoodsList.Add(entity); }
        }
        return OrdersGoodsList;
    }

    /// <summary>
    /// 订单详细商品显示
    /// </summary>
    /// <param name="orders_id"></param>
    /// <returns></returns>
    public string Order_Detail_Goods(int orders_id)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table class=\"table_orders_goods_list\">");
        strHTML.Append("	<tr>");
        strHTML.Append("		<th>商品编号</th>");
        strHTML.Append("		<th>商品名称</th>");
        strHTML.Append("		<th>购买价</th>");
        strHTML.Append("		<th>已优惠</th>");
        strHTML.Append("		<th>赠送积分</th>");
        strHTML.Append("		<th>商品数量</th>");
        strHTML.Append("		<th>库存状态</th>");
        strHTML.Append("	</tr>");

        double DiscountAmount = 0;
        IList<OrdersGoodsInfo> GoodsListSub = null;
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(orders_id);
        IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(entitys, 0);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                strHTML.Append("	<tr>");
                strHTML.Append("		<td>" + entity.Orders_Goods_Product_Code + "</td>");

                switch (entity.Orders_Goods_Type)
                {
                    case 1:
                        strHTML.Append("<td class=\"tit\">[赠品] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "</a></td>");
                        break;
                    case 2:
                        strHTML.Append("<td class=\"tit\">[套装] " + entity.Orders_Goods_Product_Name + "</a></td>");
                        break;
                    default:
                        strHTML.Append("<td class=\"tit\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "</a></td>");
                        break;
                }

                DiscountAmount = Math.Round(entity.Orders_Goods_Product_MKTPrice - entity.Orders_Goods_Product_Price, 2, MidpointRounding.AwayFromZero);
                if (DiscountAmount < 0) DiscountAmount = 0;

                strHTML.Append("		<td><span>" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</span></td>");
                strHTML.Append("		<td><span>" + pub.FormatCurrency(DiscountAmount) + "</span></td>");
                strHTML.Append("		<td>" + entity.Orders_Goods_Product_Coin + "</td>");
                strHTML.Append("		<td>" + entity.Orders_Goods_Amount + "</td>");
                strHTML.Append("		<td> 现货 </td>");
                strHTML.Append("	</tr>");

                GoodsListSub = OrdersGoodsSearch(entitys, entity.Orders_Goods_ID);
                foreach (OrdersGoodsInfo e in GoodsListSub)
                {
                    strHTML.Append("	<tr>");
                    strHTML.Append("		<td>" + e.Orders_Goods_Product_Code + "</td>");
                    strHTML.Append("<td class=\"tit\">[套装] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, e.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + e.Orders_Goods_Product_Name + "</a></td>");

                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>--</td>");
                    strHTML.Append("		<td>" + (e.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "×" + entity.Orders_Goods_Amount + "</td>");
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


    public void Order_Detail_Goods(string uses, int orders_id, double total_price, double Total_Freight, double Orders_Total_PriceDiscount, double Orders_Total_FreightDiscount, double Total_AllPrice, double U_Orders_Account_Pay)
    {
        int i = 0;
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(orders_id);
        string productURL = string.Empty;
        if (entitys != null)
        {
            IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(entitys, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            Response.Write("<table  width=\"868\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#cccccc\" style=\"margin-left:20px;margin-top:10px;margin-bottom:20px;\">");
            Response.Write("<tr bgcolor=\"#ffffff\">");
            Response.Write("<td width=\"107\" style=\"width:107px;\" height=\"27\" align=\"center\" >商品编号</td>");
            Response.Write("<td align=\"center\">商品名称</td>");
            Response.Write("<td style=\"width:165px;\" align=\"center\">商品价格</td>");
            Response.Write("<td style=\"width:70px;\" align=\"center\">数量</td>");
            Response.Write("<td style=\"width:160px;\"align=\"center\">合计</td>");
            Response.Write("</tr>");
            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());

                GoodsListSub = OrdersGoodsSearch(entitys, entity.Orders_Goods_ID);
                Response.Write("        <tr>");
                Response.Write("          <td height=\"27\" align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Code + "</td>");
                if (entity.Orders_Goods_Product_ID > 0)
                {
                    if (entity.Orders_Goods_Type == 2)
                    {
                        Response.Write("              <td bgcolor=\"#FFFFFF\">[套装] " + entity.Orders_Goods_Product_Name + "</td>");
                    }
                    else if (entity.Orders_Goods_Type == 1)
                    {
                        Response.Write("              <td bgcolor=\"#FFFFFF\" style=\"line-height:56px;\">[赠品] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img style=\"border:1px solid #D0D0D0;\" style=\" margin:2px 2px 2px 2px;\" align=\"absmiddle\" src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + entity.Orders_Goods_Product_Name + "\" width=\"50\" height=\"50\" /></a><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\" class=\"hei1\" style=\"padding-left:8px;position:static;+position:absolute;color:#333333;\">" + entity.Orders_Goods_Product_Name + "</a></td>");
                    }
                    else
                    {
                        Response.Write("              <td bgcolor=\"#FFFFFF\" style=\"line-height:56px;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img style=\"border:1px solid #D0D0D0;\" style=\" margin:2px 2px 2px 2px;\" align=\"absmiddle\" src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + entity.Orders_Goods_Product_Name + "\" width=\"50\" height=\"50\" /></a><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\" class=\"hei1\" style=\"padding-left:8px;position:static;+position:absolute;color:#333333;\">" + entity.Orders_Goods_Product_Name + "</a></td>");
                    }
                }
                else
                {
                    Response.Write("              <td bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Name + "</td>");
                }
                Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>");
                Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\">" + entity.Orders_Goods_Amount + "</td>");
                Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>");
                Response.Write("</tr>");

                if (GoodsListSub.Count > 0)
                {
                    foreach (OrdersGoodsInfo ent in GoodsListSub)
                    {
                        productURL = pageurl.FormatURL(pageurl.product_detail, ent.Orders_Goods_Product_ID.ToString());
                        if (ent.Orders_Goods_Type == 2 && ent.Orders_Goods_Product_ID > 0)
                        {
                            Response.Write("<tr>");
                            Response.Write("          <td height=\"27\" align=\"center\" bgcolor=\"#FFFFFF\">" + ent.Orders_Goods_Product_Code + "</td>");
                            Response.Write("          <td bgcolor=\"#FFFFFF\" style=\"line-height:56px;\">&nbsp;&nbsp;[套装] <a href=\"" + pageurl.FormatURL(pageurl.product_detail, ent.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img style=\"border:1px solid #D0D0D0;\" style=\" margin:2px 2px 2px 2px;\" align=\"absmiddle\" src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + ent.Orders_Goods_Product_Name + "\" width=\"50\" height=\"50\" /></a><a href=\"" + pageurl.FormatURL(pageurl.product_detail, ent.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\" class=\"hei1\" style=\"padding-left:8px;position:static;+position:absolute;color:#333333;\">" + ent.Orders_Goods_Product_Name + "</a></td>");
                            Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">--</td>");
                            Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\">" + (ent.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "×" + entity.Orders_Goods_Amount + "</td>");
                            Response.Write("          <td bgcolor=\"#FFFFFF\" align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">--</td>");
                            Response.Write("</tr>");
                        }
                    }
                }
            }
            Response.Write("<tr bgcolor=\"#FFFFFF\" height=\"25\">");
            Response.Write("<td colspan=\"5\" style=\"text-align:right;\">");
            Response.Write("<div style=\" width:229px;text-align:left; float:right; \">商品金额总计：<span style=\"color:#cc0000;font-size:14px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(total_price) + "</span> </td></div>");
            Response.Write("</tr>");
            Response.Write("            </table>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("<div id=\"zuidi-right\" style=\"margin-right:20px;\">");
            Response.Write("<p>商品价格：<span style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(total_price) + "</span></p>");
            if (Total_Freight > 0)
            {
                Response.Write("<p>运费：<span style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(Total_Freight) + "</span></p>");
            }
            if (Orders_Total_FreightDiscount > 0)
            {
                Response.Write("<p>运费优惠：<span style=\"color:#999999;font-size:12px;\">-" + pub.FormatCurrency(Orders_Total_FreightDiscount) + "</span></p>");
            }
            if (Orders_Total_PriceDiscount > 0)
            {
                Response.Write("<p>价格优惠：<span style=\"color:#999999;font-size:12px;\">-" + pub.FormatCurrency(Orders_Total_PriceDiscount) + "</span></p>");
            }
            if (U_Orders_Account_Pay > 0)
            {
                Response.Write("<p>抵扣金额：<span style=\"color:#999999;font-size:12px;\">-" + pub.FormatCurrency(U_Orders_Account_Pay) + "</span></p>");
            }
            Response.Write("<p style=\"border-bottom:1px solid #cccccc;\"></p>");
            Response.Write("<p style=\"color:#333333;font-weight:bolder;\">您需要为订单支付：<span style=\"color:#cc0000;font-size:16px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(Total_AllPrice - U_Orders_Account_Pay - Orders_Total_FreightDiscount - Orders_Total_PriceDiscount) + "</span> 元</p>");
            Response.Write("</div>");
            Response.Write("<div class=\"clear\"></div>");
        }
    }

    public string Order_Detail_Goods_Mail(string Orders_SN)
    {
        int orders_id = 0;
        string strHTML = "";
        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(Orders_SN);
        string productURL = string.Empty;
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
                IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(entitys, 0);
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
                    productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());

                    GoodsListSub = OrdersGoodsSearch(entitys, entity.Orders_Goods_ID);
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
                            strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"" + tools.NullStr(Application["site_url"]) + productURL + "\"><strong>" + entity.Orders_Goods_Product_Name + "</strong></a></td>";
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
                            productURL = pageurl.FormatURL(pageurl.product_detail, ent.Orders_Goods_Product_ID.ToString());

                            strHTML = strHTML + "<tr bgcolor=\"#FFFFFF\"\">";
                            strHTML = strHTML + "<td align=\"center\">" + ent.Orders_Goods_Product_Code + "</td>";
                            if (ent.Orders_Goods_Type == 1)
                            {
                                strHTML = strHTML + "          <td bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
                                strHTML = strHTML + "            <tr>";
                                if (ent.Orders_Goods_Product_ID > 0)
                                {

                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + pub.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><span class=\"t12_red\">[赠品]</span> <a class=\"a_t12_black\" href=\"" + productURL + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
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
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"" + productURL + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
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
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">商品总价 <span class=\"price_small\">" + pub.FormatCurrency(total_price) + "</span></td>";
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
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                ordersinfo.Orders_Note = orders_note;
                MyOrders.EditOrders(ordersinfo);
            }
        }
        Response.Redirect("/member/order_detail.aspx?orders_sn=" + orders_sn);
    }

    public void Orders_Close()
    {
        string orders_close_note = tools.CheckStr(Request["orders_close_note"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
        }
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
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
                    Member_Coin_AddConsume(total_usecoin, "订单" + orders_sn + "取消返还积分", tools.CheckInt(Session["member_id"].ToString()), true);
                }

                //虚拟账号余额回返
                double account_pay = 0;
                account_pay = ordersinfo.Orders_Account_Pay;
                if (account_pay > 0)
                {
                    Member_Account_Log(ordersinfo.Orders_BuyerID, account_pay, "订单" + orders_sn + "取消,抵扣金额退回");
                }

                //恢复优惠券使用
                Orders_Coupon_ReUse(ordersinfo.Orders_ID);

                //删除赠送优惠券
                Orders_SendCoupon_Action("delete", tools.NullInt(Session["member_id"]), ordersinfo.Orders_ID, "", 0);

                Orders_Log(ordersinfo.Orders_ID, "", "取消", "成功", "订单取消,取消原因：" + orders_close_note + "");
            }
        }
        //Response.Redirect("/member/order_detail.aspx?orders_sn=" + orders_sn);
        Response.Redirect("/member/order_faiture.aspx");
    }

    //订单删除
    public void Orders_Delete()
    {
        int OrderID = tools.CheckInt(Request["OrderID"]);
        OrdersInfo entity = MyOrders.GetOrdersByID(OrderID);
        if (entity != null)
        {
            if (entity.Orders_BuyerID == tools.NullInt(Session["member_id"]) && entity.Orders_Status == 3)
            {
                //entity.U_Orders_IsShow = 1;
                MyOrders.EditOrders(entity);

                Response.Write("订单删除成功！");
                Response.End();
            }
            else
            {
                Response.Write("该订单不支持此操作！");
                Response.End();
            }
        }
        Response.Write("该订单不支持此操作！");
        Response.End();
    }

    //会员积分消费
    public void Member_Coin_AddConsume(int coin_amount, string coin_reason, int member_id, bool is_return)
    {
        int Member_CoinRemain = 0;
        MemberInfo member = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (member != null)
        {
            Member_CoinRemain = member.Member_CoinRemain;
            MemberConsumptionInfo consumption = new MemberConsumptionInfo();
            consumption.Consump_ID = 0;
            consumption.Consump_MemberID = member_id;
            consumption.Consump_Coin = coin_amount;
            consumption.Consump_CoinRemain = Member_CoinRemain + coin_amount;
            consumption.Consump_Reason = coin_reason;
            consumption.Consump_Addtime = DateTime.Now;

            MyConsumption.AddMemberConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                }
                else
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                    member.Member_CoinCount = member.Member_CoinCount + coin_amount;
                }
            }
            else
            {
                member.Member_CoinRemain = Member_CoinRemain + coin_amount;
            }

            MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }
    }

    //派发优惠券操作
    public void Orders_SendCoupon_Action(string Action, int Member_ID, int Orders_ID, string Policy_ID, double total_favor_price)
    {
        switch (Action)
        {
            case "add":
                string policy_id = "0";
                int total_amount;
                IList<OrdersGoodsTmpInfo> Cartinfos;
                int SupplyID = tools.NullInt(Session["SupplierID"]);
                Cartinfos = Get_Orders_Carts(Orders_ID);
                IList<FavorDiscountInfo> Favorinfos;
                Favorinfos = MyFavor.Get_Policy_Discount(Cartinfos, tools.NullInt(Session["member_grade"]), "CN");
                if (Favorinfos != null)
                {
                    foreach (FavorDiscountInfo Favorinfo in Favorinfos)
                    {

                        policy_id = policy_id + "," + Favorinfo.Discount_Policy;
                    }
                }

                PromotionFavorCouponInfo coupon = null;
                PromotionFavorCouponBrandInfo couponbrand = null;
                PromotionFavorCouponCateInfo couponcate = null;
                PromotionFavorCouponProductInfo couponproduct = null;
                PromotionFavorCouponInfo lastinfo;
                PromotionCouponRuleInfo couponrule;
                int code_start;
                int coupon_amount;
                string Coupon_Code = "";
                QueryInfo Query = new QueryInfo();
                ProductInfo product = null;
                Query.PageSize = 0;
                Query.CurrentPage = 1;

                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", "CN"));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_ID", "in", policy_id));

                IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, pub.CreateUserPrivilege("b71d572b-93b5-462f-ad32-76f97f4fb8f4"));

                if (entitys != null)
                {
                    foreach (PromotionFavorPolicyInfo entity in entitys)
                    {
                        //添加订单优惠券使用
                        MyOrders.AddOrdersFavorPolicy(Orders_ID, entity.Promotion_Policy_ID);

                        if (entity.Promotion_Policy_Manner == 3)
                        {
                            foreach (FavorDiscountInfo Favorinfo in Favorinfos)
                            {
                                if (entity.Promotion_Policy_ID == Favorinfo.Discount_Policy)
                                {
                                    total_amount = tools.CheckInt(Favorinfo.Discount_Note);
                                    for (coupon_amount = 1; coupon_amount <= total_amount; coupon_amount++)
                                    {
                                        couponrule = MyCouponRule.GetPromotionCouponRuleByID(entity.Promotion_Policy_CouponRuleID, pub.CreateUserPrivilege("e5a32e42-426a-4202-818a-ad20a980b4cb"));
                                        if (couponrule != null)
                                        {
                                            code_start = 1;
                                            //获取当日优惠券总数
                                            lastinfo = MyCoupon.GetLastPromotionFavorCoupon(pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                                            if (lastinfo != null)
                                            {
                                                if (lastinfo.Promotion_Coupon_Code.Substring(0, 6) == DateTime.Now.ToString("yyMMdd"))
                                                {
                                                    code_start = tools.CheckInt(lastinfo.Promotion_Coupon_Code.Substring(7));
                                                    code_start = code_start + 1;
                                                }
                                                else
                                                {
                                                    code_start = 1;
                                                }
                                            }
                                            else
                                            {
                                                code_start = 1;
                                            }
                                            lastinfo = null;
                                            coupon = new PromotionFavorCouponInfo();

                                            Coupon_Code = "000000" + code_start.ToString();
                                            Coupon_Code = Coupon_Code.Substring(Coupon_Code.Length - 6);
                                            Coupon_Code = DateTime.Now.ToString("yyMMdd") + Coupon_Code;

                                            coupon.Promotion_Coupon_ID = 0;
                                            coupon.Promotion_Coupon_Title = couponrule.Coupon_Rule_Title;
                                            coupon.Promotion_Coupon_Target = couponrule.Coupon_Rule_Target;
                                            coupon.Promotion_Coupon_Payline = couponrule.Coupon_Rule_Payline;
                                            coupon.Promotion_Coupon_Manner = couponrule.Coupon_Rule_Manner;
                                            coupon.Promotion_Coupon_Price = couponrule.Coupon_Rule_Price;
                                            coupon.Promotion_Coupon_Percent = couponrule.Coupon_Rule_Percent;
                                            coupon.Promotion_Coupon_Amount = 1;
                                            coupon.Promotion_Coupon_Starttime = DateTime.Now;
                                            coupon.Promotion_Coupon_Endtime = DateTime.Now.AddDays(couponrule.Coupon_Rule_Valid);
                                            coupon.Promotion_Coupon_Member_ID = Member_ID;
                                            coupon.Promotion_Coupon_Code = Coupon_Code;
                                            coupon.Promotion_Coupon_Verifycode = pub.Createvkey().Substring(0, 8).ToUpper(); ;
                                            coupon.Promotion_Coupon_Isused = 0;
                                            coupon.Promotion_Coupon_UseAmount = 0;
                                            coupon.Promotion_Coupon_Display = 0;
                                            coupon.Promotion_Coupon_OrdersID = Orders_ID;
                                            coupon.Promotion_Coupon_Addtime = DateTime.Now;
                                            coupon.Promotion_Coupon_Site = "CN";
                                            MyCoupon.AddPromotionFavorCoupon(coupon, pub.CreateUserPrivilege("d3489f81-bf49-46dc-8222-284f1e0aabbd"));
                                            lastinfo = MyCoupon.GetLastPromotionFavorCoupon(pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                                            if (lastinfo != null)
                                            {
                                                if (lastinfo.Promotion_Coupon_Code == Coupon_Code)
                                                {
                                                    if (couponrule.PromotionCouponRuleBrands != null)
                                                    {
                                                        foreach (PromotionCouponRuleBrandInfo brandinfo in couponrule.PromotionCouponRuleBrands)
                                                        {
                                                            couponbrand = new PromotionFavorCouponBrandInfo();
                                                            couponbrand.Favor_BrandID = brandinfo.Coupon_Rule_Brand_BrandID;
                                                            couponbrand.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                            MyCoupon.AddPromotion_FavorCouponBrand(couponbrand);
                                                        }
                                                    }
                                                    if (couponrule.PromotionCouponRuleCates != null)
                                                    {
                                                        foreach (PromotionCouponRuleCateInfo cateinfo in couponrule.PromotionCouponRuleCates)
                                                        {
                                                            couponcate = new PromotionFavorCouponCateInfo();
                                                            couponcate.Favor_CateID = cateinfo.Coupon_Rule_Cate_CateID;
                                                            couponcate.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                            MyCoupon.AddPromotion_FavorCouponCate(couponcate);
                                                        }
                                                    }
                                                    if (couponrule.PromotionCouponRuleProducts != null)
                                                    {
                                                        foreach (PromotionCouponRuleProductInfo productinfo in couponrule.PromotionCouponRuleProducts)
                                                        {
                                                            couponproduct = new PromotionFavorCouponProductInfo();
                                                            couponproduct.Favor_ProductID = productinfo.Coupon_Rule_Product_ProductID;
                                                            couponproduct.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                            MyCoupon.AddPromotion_FavorCouponProduct(couponproduct);
                                                        }
                                                    }
                                                }
                                            }

                                            coupon = null;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                Query = null;
                break;
            case "valid":
                int validnum;
                TimeSpan timespan;
                Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", "CN"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", Member_ID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_OrdersID", "=", Orders_ID.ToString()));
                IList<PromotionFavorCouponInfo> coupons = MyCoupon.GetPromotionFavorCoupons(Query, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                if (coupons != null)
                {
                    foreach (PromotionFavorCouponInfo entity in coupons)
                    {
                        timespan = entity.Promotion_Coupon_Endtime.Subtract(entity.Promotion_Coupon_Starttime);
                        validnum = timespan.Days;
                        entity.Promotion_Coupon_Display = 1;
                        entity.Promotion_Coupon_Starttime = DateTime.Now;
                        entity.Promotion_Coupon_Endtime = DateTime.Now.AddDays(validnum);
                        MyCoupon.EditPromotionFavorCoupon(entity);
                    }
                }
                break;
            case "delete":
                Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", "CN"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", Member_ID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_OrdersID", "=", Orders_ID.ToString()));
                IList<PromotionFavorCouponInfo> coupones = MyCoupon.GetPromotionFavorCoupons(Query, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                if (coupones != null)
                {
                    foreach (PromotionFavorCouponInfo entity in coupones)
                    {
                        MyCoupon.DelPromotionFavorCoupon(entity.Promotion_Coupon_ID, pub.CreateUserPrivilege("d394b6b8-560a-49b9-9d20-1a356d3bf984"));
                    }
                }
                break;
        }
    }

    //优惠券恢复使用
    public void Orders_Coupon_ReUse(int Orders_ID)
    {
        string coupon_id;
        PromotionFavorCouponInfo entity;
        coupon_id = MyOrders.GetOrdersCoupons(Orders_ID);
        if (coupon_id != "")
        {
            foreach (string subcoupon in coupon_id.Split(','))
            {
                entity = MyCoupon.GetPromotionFavorCouponByID(tools.CheckInt(subcoupon), pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                if (entity != null)
                {
                    entity.Promotion_Coupon_UseAmount = entity.Promotion_Coupon_UseAmount - 1;
                    entity.Promotion_Coupon_Isused = 0;
                    MyCoupon.EditPromotionFavorCoupon(entity);
                }
            }
        }
    }

    //获取购物车商品重量
    public double Get_Orders_Weight(int supplier_id, int orders_id)
    {
        double total_weight = 0;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        IList<OrdersGoodsInfo> goodses = MyOrders.GetGoodsListByOrderID(orders_id);
        if (goodses != null)
        {
            foreach (OrdersGoodsInfo entity in goodses)
            {
                if (entity.Orders_Goods_Product_SupplierID == supplier_id)
                {
                    //统计商品重量
                    if ((entity.Orders_Goods_Type == 0 || entity.Orders_Goods_Type == 3 || entity.Orders_Goods_Type == 1))
                    {
                        goods_product = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (goods_product != null)
                        {
                            total_weight = total_weight + (goods_product.Product_Weight * entity.Orders_Goods_Amount);
                        }
                    }
                    //统计套装重量
                    if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                    {
                        goods_package = Mypackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                        if (goods_package != null)
                        {
                            total_weight = total_weight + (goods_package.Package_Weight * entity.Orders_Goods_Amount);
                        }
                    }
                }
            }
        }
        return total_weight;
    }

    //按重量计费运费价格
    public double My_Cart_weightprice(double weight, double initialweigh, double initialfee, double upweight, double upfee)
    {
        double weightnum, delivery_fee, upnum;
        delivery_fee = 0;
        weightnum = weight;
        if (weightnum == 0)
        {
            delivery_fee = 0;
        }
        else if (weightnum <= initialweigh && weightnum > 0)
        {
            delivery_fee = initialfee;
        }
        else
        {
            if ((weightnum - initialweigh) % upweight == 0)
            {
                upnum = (weightnum - initialweigh) / upweight;
            }
            else
            {
                upnum = (int)((weightnum - initialweigh) / upweight) + 1;
            }
            delivery_fee = initialfee + (upfee * upnum);
        }

        return delivery_fee;
    }

    //获取订单中指定供应商运费
    public double Get_Orders_Deliveryfee(int supplier_id, int delivery_id, double total_weight)
    {

        double delivery_fee = 0;
        DeliveryWayInfo delivery = MyDelivery.GetDeliveryWayByID(delivery_id, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (delivery != null)
        {
            if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == "CN")
            {
                if (supplier_id == 0)
                {
                    if (delivery.Delivery_Way_FeeType == 1)
                    {
                        delivery_fee = My_Cart_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
                    }
                    else
                    {
                        delivery_fee = delivery.Delivery_Way_Fee;
                    }

                }
                else
                {
                    SupplierDeliveryFeeInfo supplierdelivery = MySupplier.GetSupplierDeliveryFeeByID(supplier_id, delivery_id);
                    if (supplierdelivery != null)
                    {
                        if (supplierdelivery.Supplier_Delivery_Fee_Type == 1)
                        {
                            delivery_fee = My_Cart_weightprice(total_weight, supplierdelivery.Supplier_Delivery_Fee_InitialWeight, supplierdelivery.Supplier_Delivery_Fee_InitialFee, supplierdelivery.Supplier_Delivery_Fee_UpWeight, supplierdelivery.Supplier_Delivery_Fee_UpFee);
                        }
                        else
                        {
                            delivery_fee = supplierdelivery.Supplier_Delivery_Fee_Amount;
                        }
                    }
                }
            }
        }
        return delivery_fee;
    }

    //获取订单商品全部运费
    public double Get_Orders_FreightFee(int delivery_id, int Orders_ID)
    {
        double total_weight, total_fee;
        total_fee = 0;
        string supplier_id = "";

        IList<OrdersGoodsInfo> goodstmps = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsInfo entity in goodstmps)
            {
                if (supplier_id == "")
                {
                    supplier_id = entity.Orders_Goods_Product_SupplierID.ToString();
                    total_weight = Get_Orders_Weight(entity.Orders_Goods_Product_SupplierID, Orders_ID);
                    total_fee += Get_Orders_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                }
                else
                {
                    foreach (string substr in supplier_id.Split(','))
                    {
                        if (tools.CheckInt(substr) != entity.Orders_Goods_Product_SupplierID)
                        {
                            supplier_id += "," + entity.Orders_Goods_Product_SupplierID;
                            total_weight = Get_Orders_Weight(entity.Orders_Goods_Product_SupplierID, Orders_ID);
                            total_fee += Get_Orders_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                        }
                    }
                }
            }
        }
        return total_fee;
    }

    public void Orders_Delivery_list(int Orders_Delivery, string Address_State, string Address_City, string Address_County)
    {
        string delivery_list = "";
        delivery_list = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">";
        IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWaysByDistrict(Address_State, Address_City, Address_County, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (entitys != null)
        {

            foreach (DeliveryWayInfo entity in entitys)
            {
                if (entity.Delivery_Way_Site == "CN")
                {
                    if (entity.Delivery_Way_ID == Orders_Delivery)
                    {
                        Session["iscode"] = entity.Delivery_Way_Cod;
                        delivery_list = delivery_list + "<tr><td><input type=\"radio\" name=\"Orders_Delivery_ID\" value=\"" + entity.Delivery_Way_ID + "\" onclick=\"$('#td_payway').load('/member/order_do.aspx?action=changepay&cod=" + entity.Delivery_Way_Cod + "&fresh=' + Math.random() + '');\" checked> " + entity.Delivery_Way_Name + "</td></tr>";
                    }
                    else
                    {
                        delivery_list = delivery_list + "<tr><td><input type=\"radio\" name=\"Orders_Delivery_ID\" value=\"" + entity.Delivery_Way_ID + "\" onclick=\"$('#td_payway').load('/member/order_do.aspx?action=changepay&cod=" + entity.Delivery_Way_Cod + "&fresh=' + Math.random() + '');\"> " + entity.Delivery_Way_Name + "</td></tr>";
                    }

                }
            }
        }
        else
        {
            delivery_list = delivery_list + "<tr><td align=\"center\">暂无可用配送方式</td></tr>";
        }
        delivery_list = delivery_list + "</table>";

        Response.Write(delivery_list);
    }

    public void Orders_Payway_list(int Orders_Payway, int delivery_cod)
    {
        string payway_list = "";
        payway_list = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        if (delivery_cod == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Cod", "=", "0"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (entitys != null)
        {
            foreach (PayWayInfo entity in entitys)
            {
                if (Orders_Payway == entity.Pay_Way_ID)
                {
                    payway_list = payway_list + "<tr><td><input type=\"radio\" name=\"Orders_Payway_ID\" value=\"" + entity.Pay_Way_ID + "\" checked> " + entity.Pay_Way_Name + "</td></tr>";
                }
                else
                {
                    payway_list = payway_list + "<tr><td><input type=\"radio\" name=\"Orders_Payway_ID\" value=\"" + entity.Pay_Way_ID + "\"> " + entity.Pay_Way_Name + "</td></tr>";
                }

            }
        }
        else
        {
            payway_list = payway_list + "<tr><td align=\"center\">暂无可用支付方式</tD></tr>";
        }
        payway_list = payway_list + "</table>";

        Response.Write(payway_list);
    }


    //支付方式选择
    public void Sys_Payway_Select()
    {
        string tmp_list;
        tmp_list = "<select name=\"pay_way\" id=\"pay_way\" OnChange=\"javascript:sys_payway_select(this.options[this.selectedIndex].text);\" />";
        tmp_list = tmp_list + "<option value=\"0\" >使用常用的</option>";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (entitys != null)
        {
            foreach (PayWayInfo entity in entitys)
            {
                tmp_list = tmp_list + "<option value=\"" + entity.Pay_Way_ID + "\" >" + entity.Pay_Way_Name + "</option>";
            }
        }

        tmp_list = tmp_list + "</select>";

        Response.Write(tmp_list);
    }


    //产品单位选择
    public string OrdersDelivery_Type_Select(int Orders_Delivery_ID, string Select_DeliveryType)
    {
        string select_str = "";
        select_str += "<select onchange=\"getSelectUnit(this.id)\" name=\"" + Select_DeliveryType + "\">";
        select_str += "<option value=\"0\">请选择</option>";

        string[] DeliveryTypes = { "汽运", "火运", "船运", "空运" };

        if (Orders_Delivery_ID == 0)
        {
            foreach (string DeliveryType in DeliveryTypes)
            {
                if (DeliveryType == "汽运")
                {
                    select_str += "<option value=\"" + DeliveryType + "\" >" + DeliveryType + "</option>";
                }
                else if ((DeliveryType == "火运"))
                {
                    select_str += "<option value=\"" + DeliveryType + "\" >" + DeliveryType + "</option>";
                }
                else if ((DeliveryType == "船运"))
                {
                    select_str += "<option value=\"" + DeliveryType + "\" >" + DeliveryType + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + DeliveryType + "\" >" + "空运" + "</option>";
                }
            }
        }
        else
        {
            OrdersDeliveryInfo entity = Myorderdelivery.GetOrdersDeliveryByID(Orders_Delivery_ID, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
            if (entity != null)
            {
                foreach (string DeliveryType in DeliveryTypes)
                {
                    entity.Orders_Delivery_DriverMobile = DeliveryType;
                    //entity.Orders_Delivery_DriverMobile
                    //entity.
                    if (entity.Orders_Delivery_DriverMobile == DeliveryType)
                    {
                        select_str += "<option id=\"Orders_Delivery_TransportType\" value=\"" + DeliveryType + "\" selected  >" + DeliveryType + "</option>";
                    }
                    else
                    {
                        select_str += "<option value=\"" + DeliveryType + "\"   >" + DeliveryType + "</option>";
                    }
                }
            }
        }

        select_str += "</select>";
        return select_str;
    }

    //支付方式选择
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


    public void Orders_Delivery_Payway_Edit()
    {
        int Orders_Delivery = tools.CheckInt(Request["Orders_Delivery_ID"]);
        int Orders_Payway = tools.CheckInt(Request["Orders_Payway_ID"]);
        string delivery_name_old, delivery_name, payway_name_old, payway_name;
        CartInfo favorfee;
        int delivery_cod = 0;
        double delivery_fee = 0;
        double total_weight = 0;
        string orders_sn = tools.CheckStr(Request["orders_sn"]);

        delivery_name_old = "";
        delivery_name = "";
        payway_name_old = "";
        payway_name = "";

        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
        }
        if (Orders_Delivery == 0 || Orders_Payway == 0)
        {
            pub.Msg("error", "错误信息", "请选择配送方式和付款方式", false, "{back}");
        }



        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
            {
                //total_weight = Get_Orders_Weight(ordersinfo.Orders_ID);
                //检查配送方式并计算运费
                DeliveryWayInfo delivery = MyDelivery.GetDeliveryWayByID(Orders_Delivery, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
                if (delivery != null)
                {
                    if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == "CN")
                    {
                        delivery_cod = delivery.Delivery_Way_Cod;
                        delivery_name = delivery.Delivery_Way_Name;
                        //if (delivery.Delivery_Way_FeeType == 1)
                        //{
                        //    delivery_fee = My_Cart_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
                        //}
                        //else
                        //{
                        //    delivery_fee = delivery.Delivery_Way_Fee;
                        //}
                        delivery_fee = Get_Orders_FreightFee(Orders_Delivery, ordersinfo.Orders_ID);
                    }
                    else
                    {
                        Orders_Delivery = 0;
                    }
                }
                else
                {
                    Orders_Delivery = 0;
                }

                //检查支付方式
                PayWayInfo payway = MyPayway.GetPayWayByID(Orders_Payway, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
                if (payway != null)
                {
                    payway_name = payway.Pay_Way_Name;
                    if (delivery_cod == 0 && payway.Pay_Way_Cod == 1)
                    {
                        Orders_Payway = 0;
                    }
                }
                else
                {
                    Orders_Payway = 0;
                }

                if (Orders_Delivery == 0 || Orders_Payway == 0)
                {
                    pub.Msg("error", "错误信息", "请选择配送方式和付款方式", false, "{back}");
                }

                if (ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
                {
                    delivery_name_old = ordersinfo.Orders_Delivery_Name;
                    payway_name_old = ordersinfo.Orders_Payway_Name;


                    favorfee = Orders_Favor_Fee_Check(ordersinfo.Orders_Address_State, delivery_fee, ordersinfo.Orders_ID, Orders_Delivery, Orders_Payway);
                    ordersinfo.Orders_Delivery = Orders_Delivery;
                    ordersinfo.Orders_Delivery_Name = delivery_name;
                    ordersinfo.Orders_Payway = Orders_Payway;
                    ordersinfo.Orders_Payway_Name = payway_name;
                    ordersinfo.Orders_Total_Freight = delivery_fee;
                    ordersinfo.Orders_Total_FreightDiscount = favorfee.Total_Favor_Fee;
                    ordersinfo.Orders_Total_FreightDiscount_Note = favorfee.Favor_Fee_Note;
                    ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_Price + delivery_fee - ordersinfo.Orders_Total_PriceDiscount - ordersinfo.Orders_Total_FreightDiscount;
                    MyOrders.EditOrders(ordersinfo);
                    Orders_Log(ordersinfo.Orders_ID, "", "修改配送方式", "成功", "配送方式由：(" + delivery_name_old + ")修改为(" + delivery_name + ")");
                    Orders_Log(ordersinfo.Orders_ID, "", "修改付款方式", "成功", "付款方式由：(" + payway_name_old + ")修改为(" + payway_name + ")");
                }
            }
        }
        Response.Redirect("/member/order_detail.aspx?orders_sn=" + orders_sn);
    }

    //检查运费优惠
    public CartInfo Orders_Favor_Fee_Check(string State_ID, double Delivery_Fee, int Orders_ID, int delivery_id, int payway_id)
    {
        CartInfo cart = new CartInfo();
        cart.Total_Favor_Fee = 0;
        cart.Favor_Fee_Note = "";
        double favor_fee_price;
        favor_fee_price = 0;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        Cartinfos = Get_Orders_Carts(Orders_ID);
        FavorDiscountInfo Favorinfo;
        int member_grade, state_id;
        member_grade = tools.NullInt(Session["member_grade"]);
        state_id = tools.CheckInt(State_ID);
        Favorinfo = MyFavor.Get_DeliveryFee_Discount(Cartinfos, member_grade, payway_id, delivery_id, state_id, Delivery_Fee, "CN");
        if (Favorinfo != null)
        {
            if (Favorinfo.Discount_Amount == -1)
            {
                favor_fee_price = Delivery_Fee;
            }
            else
            {
                favor_fee_price = Favorinfo.Discount_Amount;
            }
            cart.Total_Favor_Fee = favor_fee_price;
            cart.Favor_Fee_Note = Favorinfo.Discount_Note;
        }
        return cart;
    }

    //获取购物车参加优惠商品价格
    public double Cart_Cate_Price(string cate_id, int Orders_ID)
    {
        double favor_price = 0;
        string product_cate = "";
        bool Is_Match = false;
        IList<OrdersGoodsInfo> goodstmps = MyOrders.GetGoodsListByOrderID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsInfo entity in goodstmps)
            {
                Is_Match = false;
                if (entity.Orders_Goods_Product_IsFavor == 1 && ((entity.Orders_Goods_Type != 2 && entity.Orders_Goods_ParentID == 0) || (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0)))
                {
                    product_cate = MyProduct.GetProductCategory(entity.Orders_Goods_Product_ID);
                    if (product_cate != "")
                    {
                        if (cate_id != "0")
                        {
                            foreach (string substr in cate_id.Split(','))
                            {
                                foreach (string subcate in product_cate.Split(','))
                                {
                                    if (substr == subcate)
                                    {
                                        Is_Match = true;
                                        break;
                                    }
                                }
                                if (Is_Match == true)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Is_Match = true;
                        }
                        if (Is_Match)
                        {
                            favor_price = favor_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
                        }
                    }
                }
            }
        }
        return favor_price;
    }

    public void Orders_Delivery_Accept()
    {
        string Orders_sn = tools.CheckStr(Request["orders_sn"]);
        OrdersInfo ordersinfo = GetOrdersInfoBySN(Orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
            {
                if (ordersinfo.Orders_DeliveryStatus == 1 || ordersinfo.Orders_Status == 1)
                {
                    ordersinfo.Orders_DeliveryStatus = 2;
                    ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;

                    if (MyOrders.EditOrders(ordersinfo))
                    {
                        Orders_Log(ordersinfo.Orders_ID, "", "订单签收", "成功", "订单签收");
                    }
                }
            }
        }
        Response.Redirect("/member/order_detail.aspx?orders_sn=" + Orders_sn);
        //Response.Redirect("/member/order_success.aspx");
    }

    public PayWayInfo GetPayWayByID(int ID)
    {
        return MyPayway.GetPayWayByID(ID, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
    }

    public PayInfo GetPayInfoByID(int ID)
    {
        return MyPayway.GetPayByID(ID, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
    }

    public void Orders_BackApply_Edit()
    {
        int back_id = tools.CheckInt(Request["back_id"]);
        if (back_id > 0)
        {
            string U_Orders_BackApply_CompanyName = tools.CheckStr(Request["U_Orders_BackApply_CompanyName"]);
            string U_Orders_BackApply_DeliveryCode = tools.CheckStr(Request["U_Orders_BackApply_DeliveryCode"]);
            if (U_Orders_BackApply_DeliveryCode == "")
            {
                pub.Msg("info", "提示信息", "请填写物流单号！", false, "{back}");
            }
            string U_Orders_BackApply_BankAcount = tools.CheckStr(Request["U_Orders_BackApply_BankAcount"]);
            OrdersBackApplyInfo entity = GetOrdersBackApplyByID(back_id);
            if (entity != null)
            {
                //entity.U_Orders_BackApply_BankAcount = U_Orders_BackApply_BankAcount;
                //entity.U_Orders_BackApply_CompanyName = U_Orders_BackApply_CompanyName;
                //entity.U_Orders_BackApply_DeliveryCode = U_Orders_BackApply_DeliveryCode;
                MyBack.EditOrdersBackApply(entity, pub.CreateUserPrivilege("1f9e3d6c-2229-4894-891b-13e73dd2e593"));
            }
            Response.Redirect("/member/order_backview.aspx?back_id=" + back_id);
        }
        Response.Redirect("/member/order_backlist.aspx");
    }

    public void Orders_BackApply_Add()
    {
        string order_code, apply_name, back_note, verifycode, back_account;
        int back_type, Orders_BackApply_AmountBackType, goods_amount, Orders_BackApply_DeliveryWay;
        int Orders_ID = 0;

        order_code = tools.CheckStr(Request.Form["order_code"]);
        apply_name = tools.CheckStr(Request.Form["apply_name"]);
        back_note = tools.CheckStr(Request.Form["back_note"]);
        back_account = tools.CheckStr(Request.Form["back_account"]);
        back_type = tools.CheckInt(Request.Form["back_type"]);
        verifycode = tools.CheckStr(Request.Form["verifycode"]);

        goods_amount = tools.CheckInt(Request["goods_amount"]);
        Orders_BackApply_AmountBackType = tools.CheckInt(Request.Form["Orders_BackApply_AmountBackType"]);
        Orders_BackApply_DeliveryWay = tools.CheckInt(Request.Form["Orders_BackApply_DeliveryWay"]);

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误！", false, "{back}");
        }

        if (order_code == "" || apply_name == "" || back_note == "")
        {
            pub.Msg("info", "提示信息", "请将申请信息填写完整！", false, "{back}");
        }

        OrdersInfo ordersinfo = GetOrdersInfoBySN(order_code);
        if (ordersinfo == null)
        {
            pub.Msg("info", "提示信息", "该订单不存在，请检查！", false, "{back}");
        }
        else
        {
            if (ordersinfo.Orders_BuyerID != tools.NullInt(Session["member_id"]))
                pub.Msg("info", "提示信息", "该订单不存在，请检查！", false, "{back}");
            else
            {
                if (ordersinfo.Orders_DeliveryStatus != 2 && back_type < 2)
                {
                    pub.Msg("info", "提示信息", "该订单还没有签收！", false, "{back}");
                }
                if (ordersinfo.Orders_Status == 2)
                {
                    pub.Msg("info", "提示信息", "该订单已完成！", false, "{back}");
                }
                if (ordersinfo.Orders_Status == 3)
                {
                    pub.Msg("info", "提示信息", "该订单已结束！", false, "{back}");
                }
                if (back_type != 0 && back_type != 1 && back_type != 2)
                {
                    back_type = 0;
                }

                ordersinfo.Orders_Status = 4;
                Orders_ID = ordersinfo.Orders_ID;
                int a = 0;
                int b = 0;
                for (int n = 1; n <= goods_amount; n++)
                {
                    int Orders_BackApply_Product_ProductID1 = tools.CheckInt(Request["Orders_BackApply_Product_ProductID_" + n + ""]);
                    int Orders_BackApply_Product_ApplyAmount1 = tools.CheckInt(Request["Orders_BackApply_Product_ApplyAmount_" + n + ""]);
                    if (Orders_BackApply_Product_ProductID1 > 0 && Orders_BackApply_Product_ApplyAmount1 > 0)
                    {
                        a++;
                    }

                    IList<OrdersGoodsInfo> ordersgoodsinfos = MyOrders.GetGoodsListByOrderID(Orders_ID);
                    if (ordersgoodsinfos != null)
                    {
                        foreach (OrdersGoodsInfo ordersgoodsinfo in ordersgoodsinfos)
                        {
                            if (Orders_BackApply_Product_ProductID1 == ordersgoodsinfo.Orders_Goods_ID)
                            {
                                if (Orders_BackApply_Product_ApplyAmount1 > ordersgoodsinfo.Orders_Goods_Amount)
                                {
                                    b++;
                                }
                            }
                        }
                    }
                }

                if (a == 0)
                {
                    pub.Msg("error", "错误信息", "请正确填写退货商品和数量", false, "{back}");
                }
                if (b != 0)
                {
                    pub.Msg("error", "错误信息", "请正确填写退货商品和数量", false, "{back}");
                }
                MyOrders.EditOrders(ordersinfo);
            }
        }

        OrdersBackApplyInfo backapply = new OrdersBackApplyInfo();
        backapply.Orders_BackApply_MemberID = tools.NullInt(Session["member_id"]);
        backapply.Orders_BackApply_Name = apply_name;
        backapply.Orders_BackApply_OrdersCode = order_code;
        backapply.Orders_BackApply_Type = back_type;
        backapply.Orders_BackApply_AmountBackType = Orders_BackApply_AmountBackType;
        backapply.Orders_BackApply_DeliveryWay = Orders_BackApply_DeliveryWay;
        backapply.Orders_BackApply_Note = back_note;
        backapply.Orders_BackApply_Account = back_account;
        backapply.Orders_BackApply_Status = 0;
        backapply.Orders_BackApply_Addtime = DateTime.Now;
        backapply.Orders_BackApply_AdminNote = "";
        backapply.Orders_BackApply_AdminTime = DateTime.Now;
        backapply.Orders_BackApply_SupplierNote = "";
        backapply.Orders_BackApply_SupplierTime = DateTime.Now;
        backapply.Orders_BackApply_Site = "CN";

        if (MyBack.AddOrdersBackApply(backapply, pub.CreateUserPrivilege("a0cbae74-b212-4983-b6b5-9e3e44835aa7")))
        {

            #region 退货详细处理

            //OrdersBackApplyProductInfo entity = null;
            //for (int i = 1; i <= goods_amount; i++)
            //{
            //    int Orders_BackApply_Product_ProductID = tools.CheckInt(Request["Orders_BackApply_Product_ProductID_" + i + ""]);
            //    int Orders_BackApply_Product_ApplyAmount = tools.CheckInt(Request["Orders_BackApply_Product_ApplyAmount_" + i + ""]);
            //    IList<OrdersGoodsInfo> ordersgoodsinfos = MyOrders.GetGoodsListByOrderID(Orders_ID);
            //    if (ordersgoodsinfos != null)
            //    {
            //        foreach (OrdersGoodsInfo ordersgoodsinfo in ordersgoodsinfos)
            //        {
            //            if (Orders_BackApply_Product_ProductID == ordersgoodsinfo.Orders_Goods_ID)
            //            {
            //                if (Orders_BackApply_Product_ApplyAmount > ordersgoodsinfo.Orders_Goods_Amount)
            //                {
            //                    Orders_BackApply_Product_ApplyAmount = ordersgoodsinfo.Orders_Goods_Amount;
            //                }
            //                if (Orders_BackApply_Product_ApplyAmount > 0)
            //                {
            //                    entity = new OrdersBackApplyProductInfo();
            //                    entity.Orders_BackApply_Product_ProductID = Orders_BackApply_Product_ProductID;
            //                    entity.Orders_BackApply_Product_ApplyAmount = Orders_BackApply_Product_ApplyAmount;
            //                    entity.Orders_BackApply_Product_ApplyID = backapply.Orders_BackApply_ID;
            //                    MyBackProduct.AddOrdersBackApplyProduct(entity);
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            string str = "退货";
            if (backapply.Orders_BackApply_Type == 1)
            {
                str = "换货";
            }
            if (backapply.Orders_BackApply_Type == 2)
            {
                str = "退款";
            }
            str = str + "申请。";
            Orders_Log(ordersinfo.Orders_ID, "", "退换货申请", "申请", str);
            pub.Msg("positive", "操作成功", "操作成功", true, "order_backlist.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void Order_BackApply_List()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\">订单号码</td>");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\" >退款金额</td>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">操作类型</td>");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\">申请人</td>");
        Response.Write("  <td align=\"center\" valign=\"middle\">申请时间</td>");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\">状态</td>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">操作</td>");
        Response.Write("</tr>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_MemberID", "=", member_id.ToString()));
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
                    Response.Write("<tr>");
                }
                else
                {
                    Response.Write("<tr>");
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
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>处理中</a></td>");
                }
                else if (entity.Orders_BackApply_Status == 2 || entity.Orders_BackApply_Status == 4)
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>申请失败</a></td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a>已处理</a></td>");
                }
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\" ><a href=\"order_backview.aspx?back_id=" + entity.Orders_BackApply_ID + "\" style=\"color:#006699;\">查看</a></td>");
                Response.Write("</tr>");

            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr><td height=\"35\" align=\"center\" colspan=\"7\" valign=\"middle\" ><a>没有记录</a></td></tr>");
            Response.Write("</table>");
        }

    }

    public int Order_BackApply_Count()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int result = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("OrdersBackApplyInfo.Orders_BackApply_ID", "Desc"));
        IList<OrdersBackApplyInfo> entitys = MyBack.GetOrdersBackApplys(Query, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
        if (entitys != null)
        {
            result = entitys.Count;
        }
        return result;


    }

    public OrdersBackApplyInfo GetOrdersBackApplyByID(int ID)
    {
        OrdersBackApplyInfo entity = MyBack.GetOrdersBackApplyByID(ID, pub.CreateUserPrivilege("aaa944b1-6068-42cd-82b5-d7f4841ecf45"));
        if (entity != null)
        {
            if (entity.Orders_BackApply_MemberID == tools.NullInt(Session["member_id"]))
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

    //会员虚拟账户操作日志
    public void Member_Account_Log(int Member_ID, double Amount, string Log_note)
    {
        double Member_AccountRemain = 0;
        MemberInfo member = MyMember.GetMemberByID(Member_ID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (member != null)
        {
            Member_AccountRemain = member.Member_Account;
            MemberAccountLogInfo accountLog = new MemberAccountLogInfo();
            accountLog.Account_Log_ID = 0;
            accountLog.Account_Log_MemberID = Member_ID;
            accountLog.Account_Log_Amount = Amount;
            accountLog.Account_Log_Remain = Member_AccountRemain + Amount;
            accountLog.Account_Log_Note = Log_note;
            accountLog.Account_Log_Addtime = DateTime.Now;
            accountLog.Account_Log_Site = "CN";

            MyAccountLog.AddMemberAccountLog(accountLog);

            if (Amount != 0)
            {
                member.Member_Account = Member_AccountRemain + Amount;
            }
            if (member.Member_Account >= 0)
            {
                MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
            }

        }
    }

    public OrdersInvoiceInfo GetOrdersInvoiceByOrdersID(int ID)
    {
        return MyInvoice.GetOrdersInvoiceByOrdersID(ID);
    }

    public DeliveryTimeInfo GetDeliveryTimeByID(int id)
    {
        return Mydelierytime.GetDeliveryTimeByID(id);
    }

    public void ERPInfo(int orders_id, int userid)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;

        bool extend_show = true;
        HttpCookie cookie = Request.Cookies["history_div"];
        if (cookie == null || cookie.Value.Equals("hide"))
        {
            extend_show = false;
        }


        if (orders_id > 0)
        {
            OrdersInfo entity = GetOrdersByID(orders_id);

            if (entity != null)
            {
                strHTML.Append("<div style=\"width: 350px; height: auto; margin: 0 auto;\">");
                strHTML.Append("<p>订单编号：" + entity.Orders_SN + "</p>");
                strHTML.Append("<p>下单时间：" + entity.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                strHTML.Append("<p>订单金额：" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</p>");
                strHTML.Append("<p>订单状态：" + OrdersStatus(entity.Orders_Status) + "</p>");
                strHTML.Append("<p>支付方式：" + entity.Orders_Payway_Name + "</p>");

                if ((entity.Orders_Payway == 2 || entity.Orders_Payway == 3) && entity.Orders_Loan_proj_no != "")
                {
                    QueryLoanProjectJsonInfo JsonInfo = null;

                    if (entity.Orders_Loan_proj_no != "")
                    {
                        JsonInfo = credit.QueryLoanProject("M" + entity.Orders_BuyerID, entity.Orders_Loan_proj_no, "", 0, 0);
                    }
                    else
                    {
                        JsonInfo = new QueryLoanProjectJsonInfo();
                    }

                    if (JsonInfo != null && JsonInfo.Is_success == "T")
                    {
                        IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
                        if (loanlist != null)
                        {
                            foreach (LoanlistInfo loaninfo in loanlist)
                            {
                                strHTML.Append("<p>信贷申请状态：" + LoanProjectStatus(loaninfo.Loan_status) + "</p>");
                                strHTML.Append("<p>信贷使用金额：" + pub.FormatCurrency(loaninfo.Principal_amount) + "</p>");
                                strHTML.Append("<p>信贷申请时间：" + DateTime.ParseExact(loaninfo.Submit_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</p>");
                            }
                        }
                    }
                }

                if (extend_show)
                {
                    //strHTML.Append("<a id=\"_strHref\" href=\"javascript:hidden_showdiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02_2.png\" /></span></a>");

                    strHTML.Append("<div id=\"_strDiv\" class=\"text_icon\"><a id=\"_strHref\" href=\"javascript:hidden_showdiv();\">历史订单详情</a></div>");
                }
                else
                {
                    strHTML.Append("<div id=\"_strDiv\" class=\"text_icon\"><a id=\"_strHref\"  href=\"javascript:show_hiddendiv();\">历史订单详情</a></div>");
                    //strHTML.Append("<a id=\"_strHref\" href=\"javascript:show_hiddendiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02.png\" /></span></a>");
                }


                strHTML.Append("</div>");


                QueryInfo Query = new QueryInfo();
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", entity.Orders_BuyerID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierID", "=", entity.Orders_SupplierID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_ID", "<>", entity.Orders_ID.ToString()));
                Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
                IList<OrdersInfo> listOrders = MyOrders.GetOrderss(Query);

                strHTML.Append("<div id=\"history_order\" style=\"" + (extend_show ? "" : "display: none;") + "width: 350px; height: auto; margin: 0 auto;\">");

                if (entity != null)
                {
                    foreach (OrdersInfo ordersInfo in listOrders)
                    {
                        i++;

                        strHTML.Append("<div>");
                        strHTML.Append("<p>订单编号：" + ordersInfo.Orders_SN + "</p>");
                        strHTML.Append("<p>下单时间：" + ordersInfo.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                        strHTML.Append("<p>订单金额：" + pub.FormatCurrency(ordersInfo.Orders_Total_AllPrice) + "</p>");
                        strHTML.Append("<p>订单状态：" + OrdersStatus(ordersInfo.Orders_Status) + "</p>");
                        strHTML.Append("<p>支付方式：" + ordersInfo.Orders_Payway_Name + "</p>");

                        if ((ordersInfo.Orders_Payway == 2 || ordersInfo.Orders_Payway == 3) && ordersInfo.Orders_Loan_proj_no != "")
                        {
                            QueryLoanProjectJsonInfo JsonInfo = null;

                            if (entity.Orders_Loan_proj_no != "")
                            {
                                JsonInfo = credit.QueryLoanProject("M" + ordersInfo.Orders_BuyerID, ordersInfo.Orders_Loan_proj_no, "", 0, 0);
                            }
                            else
                            {
                                JsonInfo = new QueryLoanProjectJsonInfo();
                            }

                            if (JsonInfo != null && JsonInfo.Is_success == "T")
                            {
                                IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
                                if (loanlist != null)
                                {
                                    foreach (LoanlistInfo loaninfo in loanlist)
                                    {
                                        strHTML.Append("<p>信贷申请状态：" + LoanProjectStatus(loaninfo.Loan_status) + "</p>");
                                        strHTML.Append("<p>信贷使用金额：" + pub.FormatCurrency(loaninfo.Principal_amount) + "</p>");
                                        strHTML.Append("<p>信贷申请时间：" + DateTime.ParseExact(loaninfo.Submit_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</p>");
                                    }
                                }
                            }
                        }
                        strHTML.Append("</div>");
                        strHTML.Append("<div class=\"div_hr\"></div>");
                        if (i == 10)
                        {

                            strHTML.Append("<div><a href=\"http://www.yinaifin.com/supplier/order_list.aspx\" target=\"_blank\">更多历史订单信息请去订单管理中心查看</a></div>");
                        }
                    }
                }
                else
                {
                    strHTML.Append("<div>暂无历史订单！</div>");
                }
                strHTML.Append("</div>");
            }
            else
            {
                strHTML.Append("<div style=\"width: 350px; height: auto;\">");
                strHTML.Append("<div>暂无历史订单！</div>");
                strHTML.Append("</div>");
            }
        }
        else
        {
            i = 0;
            OrdersInfo entity = MyOrders.GetMemberTopOrdersInfo(userid);
            if (entity != null)
            {
                strHTML.Append("<div style=\"width: 350px; height: auto; margin: 0 auto;\">");
                strHTML.Append("<p>订单编号：" + entity.Orders_SN + "</p>");
                strHTML.Append("<p>下单时间：" + entity.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                strHTML.Append("<p>订单金额：" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</p>");
                strHTML.Append("<p>订单状态：" + OrdersStatus(entity.Orders_Status) + "</p>");
                strHTML.Append("<p>支付方式：" + entity.Orders_Payway_Name + "</p>");

                if ((entity.Orders_Payway == 2 || entity.Orders_Payway == 3) && entity.Orders_Loan_proj_no != "")
                {
                    QueryLoanProjectJsonInfo JsonInfo = null;

                    if (entity.Orders_Loan_proj_no != "")
                    {
                        JsonInfo = credit.QueryLoanProject("M" + entity.Orders_BuyerID, entity.Orders_Loan_proj_no, "", 0, 0);
                    }
                    else
                    {
                        JsonInfo = new QueryLoanProjectJsonInfo();
                    }

                    if (JsonInfo != null && JsonInfo.Is_success == "T")
                    {
                        IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
                        if (loanlist != null)
                        {
                            foreach (LoanlistInfo loaninfo in loanlist)
                            {
                                strHTML.Append("<p>信贷申请状态：" + LoanProjectStatus(loaninfo.Loan_status) + "</p>");
                                strHTML.Append("<p>信贷使用金额：" + pub.FormatCurrency(loaninfo.Principal_amount) + "</p>");
                                strHTML.Append("<p>信贷申请时间：" + DateTime.ParseExact(loaninfo.Submit_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</p>");
                            }
                        }
                    }
                }
                //strHTML.Append("<div class=\"text_icon\"><a href=\"javascript:void(0);\" onclick=\"$('#history_order').show();\">历史订单详情</a></div>");

                if (extend_show)
                {
                    strHTML.Append("<div id=\"_strDiv\" class=\"text_icon\"><a id=\"_strHref\" href=\"javascript:hidden_showdiv();\">历史订单详情</a></div>");
                }
                else
                {
                    strHTML.Append("<div id=\"_strDiv\" class=\"text_icon\"><a id=\"_strHref\"  href=\"javascript:show_hiddendiv();\">历史订单详情</a></div>");
                }


                strHTML.Append("</div>");


                QueryInfo Query = new QueryInfo();
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", entity.Orders_BuyerID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierID", "=", entity.Orders_SupplierID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_ID", "<>", entity.Orders_ID.ToString()));
                Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
                IList<OrdersInfo> listOrders = MyOrders.GetOrderss(Query);

                strHTML.Append("<div id=\"history_order\" style=\"" + (extend_show ? "" : "display: none;") + "width: 350px; height: auto; margin: 0 auto;\">");
                if (entity != null)
                {
                    foreach (OrdersInfo ordersInfo in listOrders)
                    {
                        i++;

                        strHTML.Append("<div>");
                        strHTML.Append("<p>订单编号：" + ordersInfo.Orders_SN + "</p>");
                        strHTML.Append("<p>下单时间：" + ordersInfo.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
                        strHTML.Append("<p>订单金额：" + pub.FormatCurrency(ordersInfo.Orders_Total_AllPrice) + "</p>");
                        strHTML.Append("<p>订单状态：" + OrdersStatus(ordersInfo.Orders_Status) + "</p>");
                        strHTML.Append("<p>支付方式：" + ordersInfo.Orders_Payway_Name + "</p>");

                        if ((ordersInfo.Orders_Payway == 2 || ordersInfo.Orders_Payway == 3) && ordersInfo.Orders_Loan_proj_no != "")
                        {
                            QueryLoanProjectJsonInfo JsonInfo = null;

                            if (entity.Orders_Loan_proj_no != "")
                            {
                                JsonInfo = credit.QueryLoanProject("M" + ordersInfo.Orders_BuyerID, ordersInfo.Orders_Loan_proj_no, "", 0, 0);
                            }
                            else
                            {
                                JsonInfo = new QueryLoanProjectJsonInfo();
                            }

                            if (JsonInfo != null && JsonInfo.Is_success == "T")
                            {
                                IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
                                if (loanlist != null)
                                {
                                    foreach (LoanlistInfo loaninfo in loanlist)
                                    {
                                        strHTML.Append("<p>信贷申请状态：" + LoanProjectStatus(loaninfo.Loan_status) + "</p>");
                                        strHTML.Append("<p>信贷使用金额：" + pub.FormatCurrency(loaninfo.Principal_amount) + "</p>");
                                        strHTML.Append("<p>信贷申请时间：" + DateTime.ParseExact(loaninfo.Submit_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</p>");
                                    }
                                }
                            }
                        }
                        strHTML.Append("</div>");
                        strHTML.Append("<div class=\"div_hr\"></div>");
                        if (i == 10)
                        {
                            strHTML.Append("<div><a href=\"http://www.yinaifin.com/supplier/order_list.aspx\" target=\"_blank\">更多历史订单信息请去订单管理中心查看</a></div>");
                        }
                    }
                }
                else
                {
                    strHTML.Append("<div>暂无历史订单！</div>");
                }
                strHTML.Append("</div>");
            }
            else
            {
                strHTML.Append("<div style=\"width: 350px; height: auto;\">");
                strHTML.Append("<div>暂无历史订单！</div>");
                strHTML.Append("</div>");
            }
        }
        Response.Write(strHTML.ToString());
    }




}



