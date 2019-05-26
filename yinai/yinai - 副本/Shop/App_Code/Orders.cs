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
    private IOrdersInvoice MyInvoice;
    //private Glaer.Trade.B2C.DAL.U_ORD.OrdersBackApply MyBackDAL = new Glaer.Trade.B2C.DAL.U_ORD.OrdersBackApply();
    private IPromotionFavor MyFavor;
    private IPromotionCouponRule MyCouponRule;

    public Orders()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

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
                case "order_all": //待签收的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));                    
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "<", "2"));
                    Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("OR", "int", "OrdersInfo.Orders_PaymentStatus", "=", "4"));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "int", "OrdersInfo.Orders_Payway", "=", "3"));
                    break;
                case "order_unprocessed": //待付款的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "<", "3"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Payway", "<>", "3"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                    break;
                case "order_processing":  //待确认的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "order_success":  //待评价的订单
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "2"));
                    break;
                case "order_faiture":  //交易失败的订单
                    Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_Status", "=", "3"));
                    Query.ParamInfos.Add(new ParamInfo("or)", "int", "OrdersInfo.Orders_Status", "=", "4"));
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

    public void Orders_Log(int orders_id,string log_operator,string log_action,string log_result,string log_remark)
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

    public OrdersInfo GetOrdersInfoBySN(string sn)
    {
        return MyOrders.GetOrdersBySN(sn);
    }

    public OrdersInfo GetOrdersByID(int ID)
    {
        return MyOrders.GetOrdersByID(ID);
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
                resultstr = "<img src=\"/images/icon_success.gif\" align=\"absmiddle\" border=\"0\" />交易成功"; break;
            case 3:
                resultstr = "<img src=\"/images/icon_fail.gif\" align=\"absmiddle\" border=\"0\" />交易失败"; break;
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
                resultstr = "<img src=\"/images/icon_success.gif\" align=\"absmiddle\" border=\"0\" />已支付"; break;
            case 2:
                resultstr = "已退款"; break;
            case 3:
                resultstr = "退款处理中"; break;
            case 4:
                resultstr = "<img src=\"/images/icon_success.gif\" align=\"absmiddle\" border=\"0\" />已支付"; break;
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
                resultstr = "<img src=\"/images/icon_success.gif\" align=\"absmiddle\" border=\"0\" />已发货"; break;
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
            html+="</ul>";
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
    
    
}
