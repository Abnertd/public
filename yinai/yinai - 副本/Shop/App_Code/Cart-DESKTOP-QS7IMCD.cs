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
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.SAL;
/// <summary>
///Orders 的摘要说明
/// </summary>
public class Cart
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    Public_Class pub = new Public_Class();
    IEncrypt encrypt;
    IOrders MyOrders;
    IOrdersGoodsTmp MyCart;
    IProduct MyProduct;
    IMember MyMem;
    IPackage Mypackage;
    IMemberAddress MyAddr;
    IPromotionFavor MyFavor;
    IMemberFavorites MyMemberFavor;
    Addr addr = new Addr();
    IDeliveryWay MyDelivery;
    IPayWay MyPayway;
    IDeliveryTime Mydelierytime;
    IOrdersInvoice MyInvioce;
    IPromotionFavorFee MyFavorFee;
    IPromotionFavorCoupon MyCoupon;
    IPromotionFavorPolicy MyPolicy;
    IPromotionFavorGift MyGift;
    IPromotionLimit MyLimit;
    ISupplierCommissionCategory MyCommission;
    //Glaer.Trade.B2C.DAL.U_ORD.OrdersGoodsTmp DALGoodsTmp = new Glaer.Trade.B2C.DAL.U_ORD.OrdersGoodsTmp();
    ISupplier MySupplier;
    private PageURL pageurl;

    public Cart()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyOrders = OrdersFactory.CreateOrders();
        MyCart = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        MyProduct = ProductFactory.CreateProduct();
        MyMem = MemberFactory.CreateMember();
        Mypackage = PackageFactory.CreatePackage();
        MyAddr=MemberAddressFactory.CreateMemberAddress();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        MyPayway = PayWayFactory.CreatePayWay();
        Mydelierytime = DeliveryTimeFactory.CreateDeliveryTime();
        MyInvioce = OrdersInvoiceFactory.CreateOrdersInvoice();
        MyFavorFee = PromotionFavorFeeFactory.CreatePromotionFavorFee();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();
        MyPolicy = PromotionFavorPolicyFactory.CreatePromotionFavorPolicy();
        MyGift = PromotionFavorGiftFactory.CreatePromotionFavorGift();
        MyCommission = SupplierCommissionCategoryFactory.CreateSupplierCommissionCategory();
        MySupplier = SupplierFactory.CreateSupplier();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyLimit = PromotionLimitFactory.CreatePromotionLimit();
        MyMemberFavor = MemberFavoritesFactory.CreateMemberFavorites();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    }


    //购物车商品统计
    public int My_Cart_Count()
    {
        int Goods_Count = 0;  //购物车商品统计
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                Goods_Count += entity.Orders_Goods_Amount;
            }
        }
        return Goods_Count;
    }


   
}
