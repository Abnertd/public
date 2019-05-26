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
using Glaer.Trade.Util.SQLHelper;
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
    Member memberclass = new Member();
    Product productclass = new Product();
    SysMessage messageclass = new SysMessage();
    Orders ordersclass = new Orders();
    Credit credit = new Credit();
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
    IPayType MyPayType;
    IDeliveryTime Mydelierytime;
    IOrdersInvoice MyInvioce;
    IPromotionFavorFee MyFavorFee;
    IPromotionFavorCoupon MyCoupon;
    IPromotionFavorPolicy MyPolicy;
    IPromotionFavorGift MyGift;
    IPromotionLimit MyLimit;
    ISupplierCommissionCategory MyCommission;
    ISupplierShopGrade MyShopGrade;
    ISupplier MySupplier;
    ISupplierShop MyShop;
    ISupplierPurchaseDetail MyPurchaseDetail;
    IContract MyContract;
    private IContractTemplate MyTemplate;
    Supplier supplier;
    private Pay pay;
    private PageURL pageurl;
    private Bid MyBid;
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
        MyAddr = MemberAddressFactory.CreateMemberAddress();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        MyPayway = PayWayFactory.CreatePayWay();
        MyPayType = PayTypeFactory.CreatePayType();
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
        MyShop = SupplierShopFactory.CreateSupplierShop();
        MyShopGrade = SupplierShopGradeFactory.CreateSupplierShopGrade();
        MyPurchaseDetail = SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
        pay = new Pay();
        supplier = new Supplier();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        MyBid = new Bid();
        MyContract = ContractFactory.CreateContract();
        MyTemplate = ContractTemplateFactory.CreateContractTemplate();
    }

    //获取会员获取积分
    public int Get_Member_Coin(double Product_Price)
    {
        return pub.Get_Member_Coin(Product_Price);
    }

    public string Get_Cart_TotalPrice()
    {
        double total_price = 0;
        string all_totalprice = "0.00";

        string goods_ids = tools.CheckStr(Request["goods_id"]);
        string[] goods_array = goods_ids.Split(',');

        ProductInfo productInfo = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_ID", "in", goods_ids));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);

        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_ParentID == 0)
                {
                    productInfo = productclass.GetProductByID(entity.Orders_Goods_Product_ID);
                    if (productInfo != null)
                    {
                        //统计信息

                        total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                        all_totalprice = pub.FormatCurrency(total_price);
                    }
                }
            }
        }
        return all_totalprice;
    }

    //积分兑换检查
    public bool My_Cart_CheckCoinBuy(int product_id, int coinnum)
    {
        int CoinCount = 0;  //已兑换商品所花费积分
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "3"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_ID", "<>", product_id.ToString()));
        IList<OrdersGoodsTmpInfo> cartgoods = MyCart.GetOrdersGoodsTmps(Query);
        if (cartgoods != null)
        {
            foreach (OrdersGoodsTmpInfo entity in cartgoods)
            {
                CoinCount = CoinCount + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_UseCoin);
            }
        }
        CoinCount = CoinCount + coinnum;
        if (tools.CheckInt(Session["member_coinremain"].ToString()) >= CoinCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //订单生成积分检查
    public bool CheckCoinEnough(int member_id, int coinnum)
    {
        if (tools.CheckInt(Session["member_coinremain"].ToString()) >= coinnum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    ////购物车商品统计
    //public int My_Cart_Count()
    //{
    //    int Goods_Count = 0;  //购物车商品统计
    //    QueryInfo Query = new QueryInfo();
    //    Query.PageSize = 0;
    //    Query.CurrentPage = 1;
    //    if (tools.NullInt(Session["member_id"]) > 0)
    //    {
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
    //    }
    //    else
    //    {
    //        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
    //    }
    //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
    //    IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
    //    if (goodstmps != null)
    //    {
    //        foreach (OrdersGoodsTmpInfo entity in goodstmps)
    //        {
    //            Goods_Count += entity.Orders_Goods_Amount;
    //        }
    //    }
    //    return Goods_Count;
    //}
    public int My_Cart_Count()//得到购物车总数量
    {
        //int goods_num = 0;
        SQLHelper DBHelper = new SQLHelper();
        int goods_num = tools.CheckInt(DBHelper.ExecuteScalar("select count(*) from Orders_Goods_Tmp where Orders_Goods_SessionID='" + Session.SessionID.ToString() + "'").ToString());
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 0;
        //Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        //IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        //var goods_num = goodstmps == null ? 0 : goodstmps.Count;
        return goods_num;
    }

    //购物车商品统计
    public int My_Cart_ProductKind()
    {
        int Goods_Count = 0;  //购物车商品统计
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
            return Goods_Count;
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {

            string ProductArray = "0";
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if ((entity.Orders_Goods_Product_ID > 0) && (!ProductArray.Contains(entity.Orders_Goods_Product_ID.ToString())))
                {
                    Goods_Count++;
                    ProductArray += "," + entity.Orders_Goods_Product_ID;
                }
                //Goods_Count += entity.Orders_Goods_Amount;
            }
        }
        return Goods_Count;
    }

    //购物车商品统计
    public int My_Cart_Count(string SupplierID)
    {
        int Goods_Count = 0;  //购物车商品统计
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "in", SupplierID.ToString()));
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

    //购物车商品信息
    public void My_Cart_ProductInfo()
    {
        string Product_Name = "";
        StringBuilder strHTML = new StringBuilder();
        int Goods_Count = 0;  //购物车商品统计
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            strHTML.Append("   <div class=\"li_box_main\">");
            double totalprice = 0;
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {

                totalprice += entity.Orders_Goods_Product_Price * (double)entity.Orders_Goods_Amount;
                Product_Name = entity.Orders_Goods_Product_Name;
                strHTML.Append("          <dl>");
                strHTML.Append("                   <dt><a href=\"/product/detail.aspx?product_id=" + entity.Orders_Goods_Product_ID + "\">");
                strHTML.Append("                       <img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\"   title=" + Product_Name + "></a></dt>");
                strHTML.Append("                   <dd>");
                //strHTML.Append("                       <p><a href=\"/product/detail.aspx?product_id=" + entity.Orders_Goods_Product_ID + "\"title=" + Product_Name + ">" + tools.CutStr(Product_Name, 30) + "</a></p>");
                strHTML.Append("                       <p><a href=\"/cart/my_cart.aspx\"title=" + Product_Name + ">" + tools.CutStr(Product_Name, 30) + "</a></p>");
                strHTML.Append("                       <p>" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "×" + entity.Orders_Goods_Amount + "</p>");
                strHTML.Append("                   </dd>");
                strHTML.Append("                   <div class=\"clear\"></div>");
                strHTML.Append("               </dl>");

            }
            strHTML.Append("  </div>");

            strHTML.Append(" <a href=\"/cart/my_cart.aspx\"><b style=\"margin-right:1px;\">总计：<strong>" + pub.FormatCurrency(totalprice) + "</strong><span style=\"margin-right:1px;float:right;color:#329cf2\">去结算</span></b></a>");
        }
        Response.Write(strHTML);
    }

    //购物车商品类别
    public string My_Cart_Cate()
    {
        string cate = "0";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> entitys = MyCart.GetOrdersGoodsTmps(Query);
        if (entitys != null)
        {
            foreach (OrdersGoodsTmpInfo entity in entitys)
            {
                cate += "," + entity.Orders_Goods_Product_CateID;
            }
        }
        if (tools.Left(cate, 1) == ",")
        {
            cate = cate.Remove(0, 1);
        }
        return cate;
    }

    //购物车商品ID
    public string My_Cart_ProductID()
    {
        string ProductID = "0";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "0"));
        IList<OrdersGoodsTmpInfo> entitys = MyCart.GetOrdersGoodsTmps(Query);
        if (entitys != null)
        {
            foreach (OrdersGoodsTmpInfo entity in entitys)
            {
                ProductID += "," + entity.Orders_Goods_Product_ID;
            }
        }
        if (tools.Left(ProductID, 1) == ",")
        {
            ProductID = ProductID.Remove(0, 1);
        }
        return ProductID;
    }

    //购物车页翻页效果
    public void GetProductListHTML()
    {
        string Cate = My_Cart_Cate();
        string product_list = "";
        int i = 0;
        int j;
        int num = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "not in", My_Cart_ProductID()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_CateID", "in", Cate));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_IsNoStock", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_UsableAmount", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            product_list = product_list + "<div id=\"yang\" style=\" position:relative;\">";
            product_list = product_list + "  <div id=\"yang1\"><img src=\"/images/cart_top.jpg\" width=\"960\" height=\"5\" /></div>";
            product_list = product_list + "<div id=\"yang2\" style=\" position:relative;\">";
            product_list = product_list + "  <h3 style=\"padding-left:15px;padding-top:10px;\"><img style=\" margin-left:15px; margin-right:10px; float:left;\" src=\"/images/ico_arrow.jpg\" width=\"10\" height=\"11\" />你可能还喜欢的商品</h3>";
            product_list = product_list + "<div style=\"position:relative;overflow:hidden; height:158px;\">";
            product_list = product_list + "<ul>";
            product_list = product_list + "<li>";
            product_list = product_list + "<table cellpadding=\"10\" cellspacing=\"0\" align=\"center\" border=\"0\" style=\" margin:10px 0px 5px 10px;\" width=\"930\" >";
            product_list = product_list + "<tr>";
            foreach (ProductInfo entity in entitys)
            {
                i = i + 1;
                num = num + 1;
                product_list = product_list + "<td width=\"33%\">";
                product_list = product_list + "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" title=\"" + entity.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" style=\"border:1px solid #d0d0d0; float:left;\" width=\"115\" height=\"115\" /></a>";
                product_list = product_list + "<p style=\"color:#005096; padding-top:5px; padding-left:125px; line-height:20px;\"><a style=\"color:#005096;\" href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" title=\"" + entity.Product_Name + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 50) + "</a><br/>零售价：" + pub.FormatCurrency(entity.Product_MKTPrice) + "<br/>";
                PromotionLimitInfo limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                if (limitinfo != null)
                {
                    product_list = product_list + "限时价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(limitinfo.Promotion_Limit_Price) + "<br/>";
                }
                else
                {
                    string TagArry = MyProduct.GetProductTag(entity.Product_ID);
                    if (TagArry.IndexOf(",10") > 0)
                    {
                        product_list = product_list + "促销价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(productclass.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</b><br/>";
                    }
                    else
                    {
                        product_list = product_list + "售价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(productclass.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "<br/>";
                    }
                }
                product_list = product_list + "</p>";
                product_list = product_list + "<p style=\" padding-left:125px; padding-top:10px;\"><a href=\"javascript:;\" onclick=\"Add_cart('/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "');\"><img src=\"/images/btn_addcart1.jpg\" width=\"94\" height=\"22\" /></a></p>";
                product_list = product_list + "</td>";
                if (i % 3 == 0 && num < entitys.Count)
                {
                    product_list = product_list + "</tr>";
                    product_list = product_list + "</table>";
                    product_list = product_list + "</li>";

                    product_list = product_list + "<li>";
                    product_list = product_list + "<table cellpadding=\"10\" cellspacing=\"0\" align=\"center\" border=\"0\" style=\" margin:10px 0px 5px 10px;\" width=\"930\" >";
                    product_list = product_list + "<tr>";
                    i = 0;
                }
            }
            for (j = 1; j <= 3 - i; j++)
            {
                product_list = product_list + "<td width=\"33%\"></td>";
            }
            product_list = product_list + "</tr>";
            product_list = product_list + "</table>";
            product_list = product_list + "</li>";
            product_list = product_list + "</ul>";
            product_list = product_list + "</div>";
            product_list = product_list + "</div>";
            product_list = product_list + "<div id=\"yang3\"><img src=\"/images/cart_bottom.jpg\" width=\"960\" height=\"4\" /></div>";
            product_list = product_list + "</div>";
            if (entitys.Count / 3.0 > 1)
            {
                product_list = product_list + "<script type=\"text/javascript\">";
                product_list = product_list + "function AutoScrollMdico(obj){ ";
                product_list = product_list + "$(obj).find(\"ul:first\").animate({";
                product_list = product_list + " marginTop:\"-140px\"";
                product_list = product_list + "},500,function(){";
                product_list = product_list + " $(this).css({marginTop:\"0px\"}).find(\"li:first\").appendTo(this);";
                product_list = product_list + " });";
                product_list = product_list + "}";
                product_list = product_list + "$(document).ready(function(){";
                product_list = product_list + "setInterval('AutoScrollMdico(\"#yang2\")',5000)";
                product_list = product_list + "});";
                product_list = product_list + "</script>";
            }
        }
        else
        {
            int member_id = tools.CheckInt(Session["member_id"].ToString());
            if (member_id > 0)
            {
                QueryInfo MQuery = new QueryInfo();
                MQuery.PageSize = 0;
                MQuery.CurrentPage = 1;
                MQuery.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
                MQuery.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "0"));
                MQuery.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
                IList<MemberFavoritesInfo> favoriates = MyMemberFavor.GetMemberFavoritess(MQuery);
                if (favoriates != null)
                {
                    product_list = product_list + "<div id=\"yang\" style=\" position:relative;\">";
                    product_list = product_list + "  <div id=\"yang1\"><img src=\"/images/cart_top.jpg\" width=\"960\" height=\"5\" /></div>";
                    product_list = product_list + "<div id=\"yang2\" style=\" position:relative;\">";
                    product_list = product_list + "  <h3 style=\"padding-left:15px;padding-top:10px;\"><img style=\" margin-left:15px; margin-right:10px; float:left;\" src=\"/images/ico_arrow.jpg\" width=\"10\" height=\"11\" />我的收藏</h3>";
                    product_list = product_list + "<div style=\"position:relative;overflow:hidden; height:158px;\">";
                    product_list = product_list + "<ul>";
                    product_list = product_list + "<li>";
                    product_list = product_list + "<table cellpadding=\"10\" cellspacing=\"0\" align=\"center\" border=\"0\" style=\" margin:10px 0px 5px 10px;\" width=\"930\" >";
                    product_list = product_list + "<tr>";
                    foreach (MemberFavoritesInfo favoriate in favoriates)
                    {
                        ProductInfo entity = MyProduct.GetProductByID(favoriate.Member_Favorites_TargetID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (entity != null)
                        {
                            i = i + 1;
                            num = num + 1;
                            product_list = product_list + "<td width=\"33%\">";
                            product_list = product_list + "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" title=\"" + entity.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" style=\"border:1px solid #d0d0d0; float:left;\" width=\"115\" height=\"115\" /></a>";
                            product_list = product_list + "<p style=\"color:#005096; padding-top:5px; padding-left:125px;line-height:20px; \"><a style=\"color:#005096;\" href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" title=\"" + entity.Product_Name + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 50) + "</a><br/>零售价：" + pub.FormatCurrency(entity.Product_MKTPrice) + "<br/>";
                            PromotionLimitInfo limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                            if (limitinfo != null)
                            {
                                product_list = product_list + "限时价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(limitinfo.Promotion_Limit_Price) + "<br/>";
                            }
                            else
                            {
                                string TagArry = MyProduct.GetProductTag(entity.Product_ID);
                                if (TagArry.IndexOf(",10") > 0)
                                {
                                    product_list = product_list + "促销价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(productclass.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</b><br/>";
                                }
                                else
                                {
                                    product_list = product_list + "售价：<b style=\"color:#fa0130;font-size:14px;font-family:Arial, Helvetica, sans-serif;font-weight:bold;\">" + pub.FormatCurrency(productclass.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "<br/>";
                                }
                            }
                            product_list = product_list + "</p>";
                            product_list = product_list + "<p style=\" padding-left:125px; padding-top:10px;\">";
                            if (entity.Product_UsableAmount > 0 || entity.Product_IsNoStock == 1)
                            {
                                product_list = product_list + "<a href=\"javascript:;\" onclick=\"Add_cart('/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "');\"><img src=\"/images/btn_buy.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"添加到购物车\" /></a> ";
                            }
                            else
                            {
                                product_list = product_list + "<img src=\"/Images/btn_nostock.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"暂无货\" /> ";
                            }
                            product_list = product_list + "<a href=\"javascript:;\" onclick=\"if(confirm('确定将选择商品从收藏夹中删除吗？')==true){AjaxDelShouCang(" + favoriate.Member_Favorites_ID + ");}\"><img src=\"/Images/btn_del.jpg\" align=\"absmiddle\" border=\"0\" alt=\"从收藏中移除该商品\" /></a></p>";
                            product_list = product_list + "</td>";
                            if (i % 3 == 0 && num < favoriates.Count)
                            {
                                product_list = product_list + "</tr>";
                                product_list = product_list + "</table>";
                                product_list = product_list + "</li>";

                                product_list = product_list + "<li>";
                                product_list = product_list + "<table cellpadding=\"10\" cellspacing=\"0\" align=\"center\" border=\"0\" style=\" margin:10px 0px 5px 10px;\" width=\"930\" >";
                                product_list = product_list + "<tr>";
                                i = 0;
                            }
                        }
                    }
                    for (j = 1; j <= 3 - i; j++)
                    {
                        product_list = product_list + "<td width=\"33%\"></td>";
                    }
                    product_list = product_list + "</tr>";
                    product_list = product_list + "</table>";
                    product_list = product_list + "</li>";
                    product_list = product_list + "</ul>";
                    product_list = product_list + "</div>";
                    product_list = product_list + "</div>";
                    product_list = product_list + "<div id=\"yang3\"><img src=\"/images/cart_bottom.jpg\" width=\"960\" height=\"4\" /></div>";
                    product_list = product_list + "</div>";
                    if (favoriates.Count / 3.0 > 1)
                    {
                        product_list = product_list + "<script type=\"text/javascript\">";
                        product_list = product_list + "function AutoScrollMdico(obj){ ";
                        product_list = product_list + "$(obj).find(\"ul:first\").animate({";
                        product_list = product_list + " marginTop:\"-140px\"";
                        product_list = product_list + "},500,function(){";
                        product_list = product_list + " $(this).css({marginTop:\"0px\"}).find(\"li:first\").appendTo(this);";
                        product_list = product_list + " });";
                        product_list = product_list + "}";
                        product_list = product_list + "$(document).ready(function(){";
                        product_list = product_list + "setInterval('AutoScrollMdico(\"#yang2\")',5000)";
                        product_list = product_list + "});";
                        product_list = product_list + "</script>";
                    }
                }
            }
        }
        Response.Write(product_list);
    }

    public void GetProductListByCates()
    {
        string html = "";
        string Cate = My_Cart_Cate();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "not in", My_Cart_ProductID()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_CateID", "in", Cate));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_IsNoStock", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_UsableAmount", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            Response.Write("<ul>");
            foreach (ProductInfo entity in entitys)
            {
                Response.Write("<li><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" alt=\"" + entity.Product_Name + "\" /></a>");
                Response.Write("<p>" + tools.CutStr(entity.Product_Name, 23) + "</p><p>售价：<span>" + pub.FormatCurrency(productclass.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></p><p>");
                if (entity.Product_UsableAmount > 0 || entity.Product_IsNoStock == 1)
                {
                    Response.Write("<a class=\"addcart\" href=\"/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "\"><img src=\"/images/btn_buy.jpg\" alt=\"加入到购物车\" /></a>");
                }
                //else
                //{
                //    Response.Write("<img src=\"/images/btn_nostock.jpg\" alt=\"暂无货\" width=\"50\" height=\"22\" />");
                //}
                //Response.Write("    &nbsp;<a style=\"cursor:pointer;\" id=\"openBox_" + entity.Product_ID + "\" onclick=\"AjaxCheckLogin_ShouCang('/cart/my_cart.aspx','" + entity.Product_ID + "','openBox_" + entity.Product_ID + "');\"><img src=\"/images/shoucang.jpg\" width=\"50\" height=\"22\" alt=\"收藏该商品\" /></a><script>$('#openBox_" + entity.Product_ID + "').zxxbox();</script></p> </li>");
                Response.Write("</p></li>");
            }
            Response.Write("</ul>");
        }
    }

    public void Home_CartProductList()
    {
        string html = "";
        string productURL = "";
        double total_price = 0;
        int Goods_Count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                Goods_Count = Goods_Count + entity.Orders_Goods_Amount;
                html += "<div style=\" margin-bottom:5px; height:52px; margin-left:5px; margin-right:5px;width:330px; \">";
                if (entity.Orders_Goods_Type != 2)
                {
                    productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());
                    string url = "/cart/cart_do.aspx?action=move&goods_id=" + entity.Orders_Goods_ID;

                    html += "<span style=\"width:50px; height:50px; border:1px solid #cccccc; float:left;\">";
                    html += "<a href=\"" + productURL + "\" style=\"color:#375079;\" target=\"_blank\"><img style=\"display:inline;\" src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"50\" height=\"50\" /></a>";
                    html += "</span>";
                    html += "<span style=\"width:110px; text-align:left; padding-left:5px; padding-right:5px; height:52px; float:left;\">";
                    html += "<a href=\"" + productURL + "\" style=\"color:#375079;\" style=\"display:inline;\" title=\"" + entity.Orders_Goods_Product_Name + "\" alt=\"" + entity.Orders_Goods_Product_Name + "\" target=\"_blank\">" + tools.Left(entity.Orders_Goods_Product_Name, 10) + "</a>";
                    html += "</span>";
                    html += "<span style=\"width:150px; height:52px; float:left;\">";
                    html += "<p style=\" font-weight:bold; color:#cc0000;\" >" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "×" + entity.Orders_Goods_Amount + "</p>";
                    html += "<p><a href=\"javascript:;\" style=\"color:#375079;\" onclick=\"Del_cart('" + url + "');\">删除</a></p>";
                    html += "</span>";
                    html += "<div class=\"clear\"></div>";
                }
                else
                {

                    string url = "/cart/cart_do.aspx?action=packmove&goods_id=" + entity.Orders_Goods_ID;
                    html += "<span style=\"width:50px; height:50px; border:1px solid #cccccc; float:left;\">";
                    html += "<img src=\"/images/icon_package.gif\" style=\"color:#375079;\" width=\"50\" height=\"50\" style=\"display:inline;\"/>";
                    html += "</span>";
                    html += "<span style=\"width:110px; text-align:left; padding-left:5px; padding-right:5px; height:52px; float:left;\">";
                    html += tools.Left(entity.Orders_Goods_Product_Name, 30);
                    html += "</span>";
                    html += "<span style=\"width:150px; height:52px; float:left;\">";
                    html += "<p style=\" font-weight:bold; color:#cc0000;\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "×" + entity.Orders_Goods_Amount + "</p>";
                    html += "<p><a href=\"javascript:;\" style=\"color:#375079;\" onclick=\"Del_cart('" + url + "');\">删除</a></p>";
                    html += "</span>";
                }
                html += "</div>";
            }

            html += "<div style=\"padding-top:10px;\">";
            html += "<p style=\" text-align:right; padding-right:10px;\">共<span style=\"font-weight:bold; color:#cc0000;\">" + Goods_Count + "</span>件商品 共计<span style=\"font-weight:bold; color:#cc0000; font-size:16px;\">" + pub.FormatCurrency(total_price) + "</span></p>";
            html += "<p style=\" text-align:right; padding-right:10px;\"><a href=\"/cart/my_cart.aspx\"><img src=\"/images/cart.jpg\" style=\"display:inline;\"/></a></p>";
            html += "</div>";
        }
        else
        {
            html += "<p>购物车中还没有商品，赶紧选购吧!</p>";
        }
        Response.Write(html);
    }

    public double GetShopGradeCommission(int Grade_ID)
    {
        double Grade_Commission = 0;
        SupplierShopGradeInfo gradeinfo = MyShopGrade.GetSupplierShopGradeByID(Grade_ID, pub.CreateUserPrivilege("c558f983-68ec-4a91-a330-1c1f04ebdf01"));
        if (gradeinfo != null)
        {
            Grade_Commission = gradeinfo.Shop_Grade_DefaultCommission;
        }
        return Grade_Commission;
    }

    public string GuessLikeProduct(string Tag_Name, int Show_Num)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Hits", "desc"));
        IList<ProductInfo> productinfos = MyProduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        strHTML.Append("<ul>");
        if (productinfos != null)
        {
            string targetURL;
            int i = 0;
            foreach (ProductInfo productinfo in productinfos)
            {
                i++;
                targetURL = pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString());
                strHTML.Append("    <li><p> <a href=\"" + targetURL + "\" target=\"_blank\">                      ");
                strHTML.Append("      " + tools.CutStr(productinfo.Product_Name, 25) + "</a>                          ");
                strHTML.Append("            </p>                                             ");
                strHTML.Append("   <p>规格：" + productinfo.Product_Spec + "</p>                        ");
                strHTML.Append("   <p>售价：<strong>" + pub.FormatCurrency(pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "</strong></p></li>                ");

            }
        }
        strHTML.Append("</ul>");

        return strHTML.ToString();
    }




    #region "更新购物车"

    //更新购物车
    public void My_Cart_Update(string action, int product_id, int package_id, int buy_amount)
    {
        int goods_id = tools.CheckInt(tools.NullStr(Request["goods_id"]));
        switch (action)
        {
            case "add":
                if (product_id > 0)
                {
                    Cart_ProductAdd("add", product_id, buy_amount);
                }
                if (product_id == 0 && package_id > 0)
                {
                    Cart_PackageAdd(package_id, buy_amount);
                }
                break;
            case "add_coinbuy":
                if (product_id > 0)
                {
                    Cart_ProductAdd("add_coinbuy", product_id, buy_amount);
                }
                break;
        }
    }

    //添加商品到购物车
    public void Cart_ProductAdd(string action, int product_id, int buy_amount)
    {
        //if (tools.NullInt(Application["RepidBuy_IsEnable"]) == 0)
        //{
        //    memberclass.Member_Login_Check("/cart/my_cart.aspx");

        //}



        int cur_supplierid = 0;
        int cart_amount = 0;  //购物车内已有数量
        bool Coinover_Flag = true;      //可换购标记
        string Product_Name;
        string addname = tools.CheckStr(tools.NullStr(Request["addname"]));   //扩展属性
        bool goods_isrepeat = false;            //检查购物车重复性
        int goods_id = 0;
        double Product_Price, Product_MKTPrice, Group_Price, Product_PurchasingPrice, Product_brokerage, Product_SalePrice;
        int Group_BuyAmount, IsGroup, Product_Coin, Orders_Goods_Type, CoinBuy_Coin, normal_amount;
        string chufang = "";
        Orders_Goods_Type = 0;
        CoinBuy_Coin = 0;
        if ((Session["cur_supplierid"] == null) || (Session["cur_supplierid"] == ""))
        {
            cur_supplierid = 0;
        }
        else
        {
            cur_supplierid = tools.CheckInt(Session["cur_supplierid"].ToString());
        }



        if (addname != "")
        {
            addname = " [" + addname + "]";
        }

        ProductInfo productinfo = MyProduct.GetProductByID(product_id, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (productinfo != null)
        {
            //检查商品
            if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == "CN")
            {
                //获取购物车已有库存
                cart_amount = MyCart.Get_Orders_Goods_Amount(Session.SessionID, product_id);
                if (productinfo.Product_SupplierID == cur_supplierid)
                {
                    Response.Write("您不能购买自己的产品");
                    Response.End();
                }
                string member_buyenable = "";
                if ((Session["member_buyenable"] == null) || (Convert.ToString(Session["member_buyenable"]) == ""))
                {

                }
                else
                {
                    member_buyenable = tools.CheckStr(Session["member_buyenable"].ToString());
                }
                if (member_buyenable.Length == 0)
                {
                    //Response.Write(Session["member_buyenable"]);
                    //Response.Write("请登录账号!");
                    //Response.End();
                }

                else if (member_buyenable == "False")
                {
                    Response.Write(Session["member_buyenable"]);
                    Response.Write("该子账号没有购买商品的权限!");
                    Response.End();
                }
                else if (member_buyenable == "True")
                {

                }
                else
                {
                    Response.Write(Session["member_buyenable"]);
                    Response.Write("请登录会员账号进行购买!");
                    Response.End();
                }


                //如果不是零库存商品,判断是否超过可用库存，否则不予判断
                if (productinfo.Product_IsNoStock == 0)
                {
                    //检查购买数量是否超过可用购买数量
                    if ((buy_amount + cart_amount) > productinfo.Product_UsableAmount)
                    {
                        //pub.Msg("error", "错误信息", "该商品暂时缺货", false, "{back}");
                        Response.Write("该商品暂时缺货");
                        Response.End();
                    }
                }

                //检查购买数量是否超过购买限额
                if (productinfo.Product_QuotaAmount > 0 && (buy_amount + cart_amount) > productinfo.Product_QuotaAmount)
                {
                    //  pub.Msg("error", "错误信息", "购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, false, "{back}");
                    Response.Write("购买数量超过商品限购" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                    Response.End();
                }

                if (buy_amount + cart_amount < productinfo.U_Product_MinBook)
                {
                    //   pub.Msg("error", "错误信息", "购买数量低于最小起订量" + productinfo.U_Product_MinBook + productinfo.Product_Unit, false, "{back}");
                    Response.Write("购买数量低于最小起订量" + productinfo.U_Product_MinBook + productinfo.Product_Unit);
                    Response.End();
                }

                //检查是否为积分兑换
                if (productinfo.Product_IsCoinBuy == 0 && action == "add_coinbuy")
                {
                    Coinover_Flag = false;
                }

                Product_Name = productinfo.Product_Name + addname;
                PromotionLimitInfo limitinfo;
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                if (tools.NullInt(Session["member_id"]) > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
                }
                else
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
                }
                if (action == "add")
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "0"));
                }
                else
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "3"));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_Product_Name", "=", Product_Name));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_ID", "=", product_id.ToString()));
                IList<OrdersGoodsTmpInfo> cartgoods = MyCart.GetOrdersGoodsTmps(Query);
                if (cartgoods != null)
                {
                    goods_isrepeat = true;

                    foreach (OrdersGoodsTmpInfo entity in cartgoods)
                    {
                        buy_amount = buy_amount + entity.Orders_Goods_Amount;
                        goods_id = entity.Orders_Goods_ID;
                    }
                }
                else
                {
                    int gradeid = 1;
                    MemberGradeInfo GradeInfo = memberclass.GetMemberDefaultGrade();
                    if (GradeInfo != null)
                    {
                        gradeid = GradeInfo.Member_Grade_ID;
                    }

                    //判断是否超过限时产品每笔购买上限
                    limitinfo = MyLimit.GetPromotionLimitByProductID(product_id, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
                    if (limitinfo != null)
                    {
                        foreach (PromotionLimitMemberGradeInfo entity in limitinfo.PromotionLimitMemberGrades)
                        {
                            if ((tools.NullInt(Session["member_id"]) == 0 && entity.Promotion_Limit_MemberGrade_Grade == gradeid) || (entity.Promotion_Limit_MemberGrade_Grade == tools.NullInt(Session["member_grade"]) && tools.NullInt(Session["member_id"]) > 0))
                            {
                                if (limitinfo.Promotion_Limit_Limit > 0 && limitinfo.Promotion_Limit_Limit < buy_amount)
                                {
                                    //pub.Msg("error", "错误信息", "该产品一次最多可购买" + limitinfo.Promotion_Limit_Limit + "件", false, "{back}");
                                    // pub.Msg("error", "错误信息", "购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, false, "{back}");
                                    Response.Write("购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                                    Response.End();
                                }
                            }
                        }
                    }

                    //如果为第一次计入进货单
                    //if (productinfo.Product_PriceType == 1)
                    //{
                    Product_Price = productinfo.Product_Price;        //商品价格
                    //}
                    //else
                    //{
                    //    Product_Price = pub.GetProductPrice(productinfo.Product_ManualFee, productinfo.Product_Weight);
                    //}

                    Product_MKTPrice = productinfo.Product_MKTPrice;  //商品市场价格
                    IsGroup = productinfo.Product_IsGroupBuy;
                    Group_BuyAmount = productinfo.Product_GroupNum;
                    Group_Price = productinfo.Product_GroupPrice;
                    Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                    Product_brokerage = 0;        //初始化佣金

                    if (Product_MKTPrice == 0)
                    {
                        Product_MKTPrice = Product_Price;
                    }

                    Product_Price = productclass.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                    //if (productinfo.Product_PriceType == 1)
                    //{
                    //    Product_Price = productclass.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                    //}
                    //else
                    //{
                    //    Product_Price = productclass.Get_Member_Price(productinfo.Product_ID, pub.GetProductPrice(productinfo.Product_ManualFee, productinfo.Product_Weight));
                    //}
                    normal_amount = MyCart.Get_Orders_Goods_TypeAmount(Session.SessionID, productinfo.Product_ID, 0);
                    //检查是否团购
                    if (IsGroup == 1)
                    {
                        if (buy_amount >= Group_BuyAmount)
                        {
                            Product_Price = Group_Price;
                        }
                    }

                    Product_Coin = Get_Member_Coin(Product_Price);

                    //检查是否赠送指定积分
                    if (productinfo.Product_IsGiftCoin == 1)
                    {
                        Product_Coin = (int)(Product_Price * productinfo.Product_Gift_Coin);
                    }

                    //如果为积分兑换
                    if (action == "add_coinbuy" && Coinover_Flag == true)
                    {
                        Orders_Goods_Type = 3;
                        Product_Price = 0;
                        Product_Coin = 0;
                        CoinBuy_Coin = productinfo.Product_CoinBuy_Coin;

                        //判断用户积分是否大于购买该商品所需总积分，coinoverflag为1表示不大于，0表示大于可以换取
                        if (My_Cart_CheckCoinBuy(product_id, (CoinBuy_Coin * buy_amount)) == false)
                        {
                            Coinover_Flag = false;
                        }
                    }

                    int Supplier_ID = 0;
                    //检查是否为开通店铺的商家
                    if (productinfo.Product_SupplierID > 0)
                    {
                        SupplierInfo supplierinfo = MySupplier.GetSupplierByID(productinfo.Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        if (supplierinfo != null)
                        {
                            if (supplierinfo.Supplier_IsHaveShop == 1)
                            {
                                Supplier_ID = productinfo.Product_SupplierID;
                            }
                        }
                    }

                    if (Coinover_Flag)
                    {
                        //Product_SalePrice = productclass.GetProductWholeSalePrice(product_id, buy_amount);
                        Product_SalePrice = productinfo.Product_Price;

                        if (Product_SalePrice > 0)
                        {
                            Product_Price = Product_SalePrice;
                        }

                        OrdersGoodsTmpInfo goodstmp = new OrdersGoodsTmpInfo();
                        goodstmp.Orders_Goods_ID = 0;
                        goodstmp.Orders_Goods_Type = Orders_Goods_Type;
                        goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                        goodstmp.Orders_Goods_SessionID = Session.SessionID;
                        goodstmp.Orders_Goods_Product_SupplierID = Supplier_ID;
                        goodstmp.Orders_Goods_ParentID = 0;
                        goodstmp.Orders_Goods_Product_ID = product_id;
                        goodstmp.Orders_Goods_Product_Code = productinfo.Product_Code;
                        goodstmp.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                        goodstmp.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                        goodstmp.Orders_Goods_Product_Name = Product_Name;
                        goodstmp.Orders_Goods_Product_Img = productinfo.Product_Img;
                        goodstmp.Orders_Goods_Product_Price = Product_Price;
                        goodstmp.Orders_Goods_Product_MKTPrice = Product_MKTPrice;
                        goodstmp.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                        goodstmp.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                        goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                        goodstmp.Orders_Goods_Product_Coin = Product_Coin;
                        goodstmp.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                        goodstmp.Orders_Goods_Product_UseCoin = CoinBuy_Coin;
                        goodstmp.Orders_Goods_Amount = buy_amount;
                        goodstmp.Orders_Goods_Addtime = DateTime.Now;
                        goodstmp.Orders_Goods_Product_SalePrice = productinfo.Product_Price;
                        goodstmp.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                        goodstmp.Orders_Goods_Product_brokerage = Product_brokerage;
                        goodstmp.Orders_Goods_OrdersID = 0;
                        MyCart.AddOrdersGoodsTmp(goodstmp);
                        goodstmp = null;
                    }
                }
            }
        }

        if (goods_isrepeat)
        {
            Cart_ProductEdit(goods_id, product_id, buy_amount);
        }
        if (Coinover_Flag == false)
        {
            if (tools.CheckInt(Session["supplier_id"].ToString()) > 0)
            {
                //pub.Msg("info", "提示信息", "您的" + Application["Coin_Name"] + "不足，该操作没有生效", false, "{back}");
                Response.Write("购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                //   pub.Msg("info", "提示信息", "购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, false, "{back}");
                Response.End();
            }
            else
            {
                Session["url_after_login"] = "/cart/my_cart.aspx?action=add_coinbuy&product_id=" + product_id;

                //pub.Msg("info", "提示信息", "您尚未登录，该操作没有生效", true, 3, "/login.aspx?u_type=1");
                //   pub.Msg("info", "提示信息", "购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, true, 3, "/login.aspx?u_type=1");
                Response.Write("购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                Response.End();
            }
        }

        if (Convert.ToString(Request.QueryString["passto"]) == "confirm")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            if (tools.NullInt(Session["member_id"]) > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
            }
            IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);

            if (goodstmps != null && My_Cart_ProductSupplier(goodstmps) && goodstmps.Count == 1)
            {
                goodstmps = null;
                //Response.Redirect("/cart/order_confirm.aspx?SupplyID=" + tools.NullInt(Session["Supplier_List"]));
                Response.Write("ToCart");
                Response.End();
            }
            else
            {
                goodstmps = null;
                //Response.Redirect("/cart/my_cart.aspx");

                Response.Write("ToCart");
                Response.End();
            }
        }
        else
        {
            //Response.Redirect("/cart/my_cart.aspx");
            Response.Write("商品已加入购物车！");
            Response.End();
        }
    }

    public void GetConfirmUrl()
    {
        Response.Write("/cart/order_confirm.aspx?SupplyID=" + tools.NullInt(Session["Supplier_List"]));
        Response.End();
    }

    //添加套装到购物车
    public void Cart_PackageAdd(int package_id, int buy_amount)
    {
        int cart_amount = 0;
        int package_stock = 0;
        bool goods_isrepeat = false;
        int goods_id = 0;
        int Package_Coin = 0;
        int goods_parentid = 0;
        double Package_Price, Package_MKTPrice, Product_PurchasingPrice, Product_brokerage;
        string Package_Product_Arry = "0";
        OrdersGoodsTmpInfo goodstmp = null;
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        SupplierCommissionCategoryInfo commissioncate = null;
        SupplierShopInfo shopinfo;


        //if (tools.NullInt(Application["RepidBuy_IsEnable"]) == 0)
        //{
        //    memberclass.Member_Login_Check("/cart/order_confirm.aspx");

        //}



        //获取套装信息
        PackageInfo packageInfo = Mypackage.GetPackageByID(package_id, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
        if (packageInfo != null)
        {
            //检查上架状态
            if (packageInfo.Package_IsInsale == 1)
            {
                //库存判断
                packagestockinfo = productclass.Get_Package_Stock(packageInfo.PackageProductInfos);
                package_stock = packagestockinfo.Product_Stock_Amount;
                cart_amount = MyCart.Get_Orders_Goods_PackageAmount(Session.SessionID, package_id);
                if ((buy_amount + cart_amount >= package_stock) && packagestockinfo.Product_Stock_IsNoStock == 0)
                {
                    pub.Msg("error", "错误信息", "该商品暂时缺货", false, "/cart/my_cart.aspx");
                    Response.End();
                }

                if (productclass.Check_Package_Valid(packageInfo.PackageProductInfos) == false)
                {
                    pub.Msg("error", "错误信息", "该商品暂时缺货", false, "/cart/my_cart.aspx");
                    Response.End();
                }

                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                if (tools.NullInt(Session["member_id"]) > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
                }
                else
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "2"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_ID", "=", package_id.ToString()));
                IList<OrdersGoodsTmpInfo> cartgoods = MyCart.GetOrdersGoodsTmps(Query);
                if (cartgoods != null)
                {
                    goods_isrepeat = true;
                    foreach (OrdersGoodsTmpInfo entity in cartgoods)
                    {
                        goods_id = entity.Orders_Goods_ID;
                    }
                }
                else
                {
                    Package_Price = productclass.Get_Member_Price(0, packageInfo.Package_Price);
                    Package_MKTPrice = productclass.Get_Package_MKTPrice(packageInfo.PackageProductInfos);
                    Package_Coin = Get_Member_Coin(Package_Price);

                    if (Package_MKTPrice == 0)
                    {
                        Package_MKTPrice = Package_Price;
                    }

                    goodstmp = new OrdersGoodsTmpInfo();
                    goodstmp.Orders_Goods_ID = 0;
                    goodstmp.Orders_Goods_Type = 2;
                    goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                    goodstmp.Orders_Goods_SessionID = Session.SessionID;
                    goodstmp.Orders_Goods_Product_SupplierID = 0;
                    goodstmp.Orders_Goods_ParentID = 0;
                    goodstmp.Orders_Goods_Product_ID = package_id;
                    goodstmp.Orders_Goods_Product_SupplierID = 0;
                    goodstmp.Orders_Goods_Product_Code = "";
                    goodstmp.Orders_Goods_Product_CateID = 0;
                    goodstmp.Orders_Goods_Product_BrandID = 0;
                    goodstmp.Orders_Goods_Product_Name = packageInfo.Package_Name;
                    goodstmp.Orders_Goods_Product_Img = "/images/icon_package.gif";
                    goodstmp.Orders_Goods_Product_Price = Package_Price;
                    goodstmp.Orders_Goods_Product_MKTPrice = Package_MKTPrice;
                    goodstmp.Orders_Goods_Product_Maker = "";
                    goodstmp.Orders_Goods_Product_Spec = "";
                    goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                    goodstmp.Orders_Goods_Product_Coin = Package_Coin;
                    goodstmp.Orders_Goods_Product_IsFavor = 1;
                    goodstmp.Orders_Goods_Product_UseCoin = 0;
                    goodstmp.Orders_Goods_Amount = buy_amount;
                    goodstmp.Orders_Goods_Addtime = DateTime.Now;
                    goodstmp.Orders_Goods_Product_brokerage = 0;
                    goodstmp.Orders_Goods_Product_SalePrice = Package_Price;
                    goodstmp.Orders_Goods_Product_PurchasingPrice = 0;

                    goodstmp.Orders_Goods_OrdersID = 0;

                    MyCart.AddOrdersGoodsTmp(goodstmp);
                    goodstmp = null;

                    goods_parentid = MyCart.Get_Orders_Goods_ParentID(Session.SessionID, package_id, 2);

                    if (goods_parentid > 0)
                    {
                        foreach (PackageProductInfo obj in packageInfo.PackageProductInfos)
                        {
                            Package_Product_Arry = Package_Product_Arry + "," + obj.Package_Product_ProductID;
                        }

                        QueryInfo Query1 = new QueryInfo();
                        Query1.PageSize = 0;
                        Query1.CurrentPage = 1;
                        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Package_Product_Arry));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
                        Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                        IList<ProductInfo> products = MyProduct.GetProductList(Query1, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (products != null)
                        {
                            int supplier = 0;
                            int issupplier = 0;
                            bool ErrorPackage = false;

                            foreach (PackageProductInfo packproduct in packageInfo.PackageProductInfos)
                            {
                                foreach (ProductInfo entity in products)
                                {
                                    if (packproduct.Package_Product_ProductID == entity.Product_ID)
                                    {
                                        if (entity.U_Product_Shipper == 1)
                                            entity.Product_SupplierID = 0;

                                        if (issupplier == 1 && supplier != entity.Product_SupplierID)
                                        {
                                            ErrorPackage = true;
                                            break;
                                        }

                                        issupplier = 1;
                                        supplier = entity.Product_SupplierID;
                                        //issupplier = entity.U_Product_ISSupply;

                                        Product_PurchasingPrice = entity.Product_PurchasingPrice;
                                        Product_brokerage = 0;        //初始化佣金

                                        //计算佣金
                                        if (entity.Product_SupplierID > 0)
                                        {
                                            //采用佣金分类
                                            if (entity.Product_Supplier_CommissionCateID > 0)
                                            {
                                                commissioncate = MyCommission.GetSupplierCommissionCategoryByID(entity.Product_Supplier_CommissionCateID, pub.CreateUserPrivilege("ed55dd89-e07e-438d-9529-a46de2cdda7b"));
                                                if (commissioncate != null)
                                                {
                                                    Product_brokerage = (entity.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                                                }
                                            }
                                            else
                                            {
                                                shopinfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                                                if (shopinfo != null)
                                                {
                                                    Product_brokerage = (entity.Product_Price * GetShopGradeCommission(shopinfo.Shop_Type) / 100);

                                                }
                                            }
                                            //else
                                            //{
                                            //    //使用差价方式
                                            //    Product_brokerage = entity.Product_Price - entity.Product_PurchasingPrice;
                                            //}
                                        }
                                        goodstmp = new OrdersGoodsTmpInfo();
                                        goodstmp.Orders_Goods_ID = 0;
                                        goodstmp.Orders_Goods_Type = 2;
                                        goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                                        goodstmp.Orders_Goods_SessionID = Session.SessionID;
                                        goodstmp.Orders_Goods_ParentID = goods_parentid;
                                        goodstmp.Orders_Goods_Product_ID = entity.Product_ID;
                                        goodstmp.Orders_Goods_Product_SupplierID = entity.Product_SupplierID;
                                        goodstmp.Orders_Goods_Product_Code = entity.Product_Code;
                                        goodstmp.Orders_Goods_Product_CateID = entity.Product_CateID;
                                        goodstmp.Orders_Goods_Product_BrandID = entity.Product_BrandID;
                                        goodstmp.Orders_Goods_Product_Name = entity.Product_Name;
                                        goodstmp.Orders_Goods_Product_Img = entity.Product_Img;
                                        goodstmp.Orders_Goods_Product_Price = entity.Product_Price;
                                        goodstmp.Orders_Goods_Product_MKTPrice = entity.Product_MKTPrice;
                                        goodstmp.Orders_Goods_Product_Maker = entity.Product_Maker;
                                        goodstmp.Orders_Goods_Product_Spec = entity.Product_Spec;
                                        goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                                        goodstmp.Orders_Goods_Product_Coin = Get_Member_Coin(entity.Product_Price);
                                        goodstmp.Orders_Goods_Product_IsFavor = entity.Product_IsFavor;
                                        goodstmp.Orders_Goods_Product_UseCoin = 0;
                                        goodstmp.Orders_Goods_Amount = packproduct.Package_Product_Amount * buy_amount;
                                        goodstmp.Orders_Goods_Product_brokerage = Product_brokerage;
                                        goodstmp.Orders_Goods_Product_SalePrice = entity.Product_Price;
                                        goodstmp.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                                        goodstmp.Orders_Goods_Addtime = DateTime.Now;
                                        goodstmp.Orders_Goods_OrdersID = 0;
                                        MyCart.AddOrdersGoodsTmp(goodstmp);
                                        goodstmp = null;
                                    }
                                }
                            }

                            if (ErrorPackage)
                            {
                                MyCart.DelOrdersGoodsTmp(0, 2, goods_parentid, Session.SessionID);
                                MyCart.DelOrdersGoodsTmp(goods_parentid, 2, 0, Session.SessionID);
                            }
                            else
                            {
                                OrdersGoodsTmpInfo TEntity = MyCart.GetOrdersGoodsTmpByID(goods_parentid);
                                if (TEntity != null)
                                {
                                    TEntity.Orders_Goods_Product_SupplierID = supplier;
                                    MyCart.EditOrdersGoodsTmp(TEntity);
                                }
                            }

                        }
                    }
                }

            }
        }
        Response.Redirect("/cart/my_cart.aspx");
    }

    //修改购物车商品
    public void Cart_ProductEdit(int goods_id, int product_id, int buy_amount)
    {
        int gradeid = 1;
        MemberGradeInfo GradeInfo = memberclass.GetMemberDefaultGrade();
        if (GradeInfo != null)
        {
            gradeid = GradeInfo.Member_Grade_ID;
        }

        int cart_amount = 0;  //购物车内已有数量
        bool Coinover_Flag = true;      //可换购标记
        double Product_Price, Product_MKTPrice, Group_Price;
        int Group_BuyAmount, IsGroup, Product_Coin, CoinBuy_Coin, normal_amount;
        CoinBuy_Coin = 0;
        PromotionLimitInfo limitinfo;
        //判断是否超过限时产品每笔购买上限
        limitinfo = MyLimit.GetPromotionLimitByProductID(product_id, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (limitinfo != null)
        {
            foreach (PromotionLimitMemberGradeInfo entity in limitinfo.PromotionLimitMemberGrades)
            {
                if ((tools.NullInt(Session["member_id"]) == 0 && entity.Promotion_Limit_MemberGrade_Grade == gradeid) || (entity.Promotion_Limit_MemberGrade_Grade == tools.NullInt(Session["member_grade"]) && tools.NullInt(Session["member_id"]) > 0))
                {
                    if (limitinfo.Promotion_Limit_Limit > 0 && limitinfo.Promotion_Limit_Limit < buy_amount)
                    {
                        pub.Msg("error", "错误信息", "该产品一次最多可购买" + limitinfo.Promotion_Limit_Limit + "件", false, "{back}");
                        //  Response.Write("该产品一次最多可购买" + limitinfo.Promotion_Limit_Limit + "件");
                        Response.End();
                    }
                }
            }
        }

        ProductInfo productinfo = MyProduct.GetProductByID(product_id, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (productinfo != null)
        {
            //检查商品
            if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == "CN")
            {
                //获取购物车已有库存
                cart_amount = MyCart.Get_Orders_Goods_Amount(Session.SessionID, product_id);

                OrdersGoodsTmpInfo goodstmp = MyCart.GetOrdersGoodsTmpByID(goods_id);
                if (goodstmp != null)
                {
                    if (goodstmp.Orders_Goods_Product_ID == product_id && goodstmp.Orders_Goods_SessionID == Session.SessionID)
                    {
                        //扣除原有数量
                        cart_amount = cart_amount - goodstmp.Orders_Goods_Amount;

                        //如果不是零库存商品,判断是否超过可用库存，否则不予判断
                        if (productinfo.Product_IsNoStock == 0)
                        {
                            //检查购买数量是否超过可用购买数量
                            if ((buy_amount + cart_amount) > productinfo.Product_UsableAmount)
                            {
                                pub.Msg("error", "错误信息", "该商品暂时缺货", false, "/cart/my_cart.aspx");
                                //  Response.Write("该商品暂时缺货！");
                                Response.End();
                            }
                        }

                        //检查购买数量是否超过购买限额
                        if (productinfo.Product_QuotaAmount > 0 && (buy_amount + cart_amount) > productinfo.Product_QuotaAmount)
                        {
                            pub.Msg("error", "错误信息", "购买数量超过商品限购" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, false, "/cart/my_cart.aspx");
                            //  Response.Write("购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                            Response.End();
                        }

                        if (buy_amount + cart_amount < productinfo.U_Product_MinBook)
                        {
                            Response.Write("购买数量低于最小起订量" + productinfo.U_Product_MinBook + productinfo.Product_Unit);
                            Response.End();
                        }

                        //检查是否为积分兑换
                        if (productinfo.Product_IsCoinBuy == 0 && goodstmp.Orders_Goods_Type == 3)
                        {
                            Coinover_Flag = false;
                        }

                        Product_Price = productinfo.Product_Price;        //商品价格
                        IsGroup = productinfo.Product_IsGroupBuy;
                        Group_BuyAmount = productinfo.Product_GroupNum;
                        Group_Price = productinfo.Product_GroupPrice;


                        Product_Price = productclass.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                        normal_amount = MyCart.Get_Orders_Goods_TypeAmount(Session.SessionID, productinfo.Product_ID, 0);
                        //检查是否团购
                        if (IsGroup == 1)
                        {
                            if ((buy_amount) >= Group_BuyAmount)
                            {
                                Product_Price = Group_Price;
                            }
                        }

                        Product_Coin = Get_Member_Coin(Product_Price);

                        //检查是否赠送指定积分
                        if (productinfo.Product_IsGiftCoin == 1)
                        {
                            Product_Coin = (int)(Product_Price * productinfo.Product_Gift_Coin);
                        }

                        //如果为积分兑换
                        if (goodstmp.Orders_Goods_Type == 3 && Coinover_Flag == true)
                        {
                            Product_Price = 0;
                            Product_Coin = 0;
                            CoinBuy_Coin = productinfo.Product_CoinBuy_Coin;

                            //判断用户积分是否大于购买该商品所需总积分，coinoverflag为1表示不大于，0表示大于可以换取
                            if (My_Cart_CheckCoinBuy(product_id, (CoinBuy_Coin * buy_amount)) == false)
                            {
                                Coinover_Flag = false;
                            }
                        }

                        if (Coinover_Flag)
                        {
                            double Product_SalePrice = productclass.GetProductWholeSalePrice(product_id, buy_amount);

                            if (Product_SalePrice == 0)
                            {
                                goodstmp.Orders_Goods_Product_Price = Product_Price;
                            }
                            else
                            {
                                goodstmp.Orders_Goods_Product_Price = Product_SalePrice;
                            }
                            goodstmp.Orders_Goods_Product_UseCoin = CoinBuy_Coin;
                            goodstmp.Orders_Goods_Product_Coin = Product_Coin;
                            goodstmp.Orders_Goods_Amount = buy_amount;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_OrdersID = 0;

                            MyCart.EditOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }
            }
        }

        if (Coinover_Flag == false)
        {
            if (tools.CheckInt(Session["supplier_id"].ToString()) > 0)
            {
                pub.Msg("info", "提示信息", "您的" + Application["Coin_Name"] + "不足，该操作没有生效", false, "{back}");
                // Response.Write("您的" + Application["Coin_Name"] + "不足，该操作没有生效");
                Response.End();
            }
            else
            {
                Session["url_after_login"] = "/cart/my_cart.aspx?action=add_coinbuy&product_id=" + product_id;
                pub.Msg("info", "提示信息", "您尚未登录，该操作没有生效", true, 3, "/login.aspx?u_type=1");
                //   Response.Write("您尚未登录，该操作没有生效");
                Response.End();
            }
        }


        if (Convert.ToString(Request.QueryString["passto"]) == "confirm")
        {
            //Response.Redirect("/cart/my_cart.aspx");
            Response.Write("ToCart");
            Response.End();
        }
        else if (Convert.ToString(Request["passto"]) == "add")
        {
            Response.Write("商品已加入购物车！");
            Response.End();
        }
        else
        {
            Response.Redirect("/cart/my_cart.aspx");
        }
    }



    //修改商品合同信息
    public void Contract_ProductEdit(int goods_id, int product_id, int buy_amount, string orders_sn)
    {
        int gradeid = 1;
        MemberGradeInfo GradeInfo = memberclass.GetMemberDefaultGrade();
        if (GradeInfo != null)
        {
            gradeid = GradeInfo.Member_Grade_ID;
        }

        int cart_amount = 0;  //购物车内已有数量
        bool Coinover_Flag = true;      //可换购标记
        double Product_Price, Product_MKTPrice, Group_Price;
        int Group_BuyAmount, IsGroup, Product_Coin, CoinBuy_Coin, normal_amount;
        CoinBuy_Coin = 0;
        PromotionLimitInfo limitinfo;
        //判断是否超过限时产品每笔购买上限
        limitinfo = MyLimit.GetPromotionLimitByProductID(product_id, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (limitinfo != null)
        {
            foreach (PromotionLimitMemberGradeInfo entity in limitinfo.PromotionLimitMemberGrades)
            {
                if ((tools.NullInt(Session["member_id"]) == 0 && entity.Promotion_Limit_MemberGrade_Grade == gradeid) || (entity.Promotion_Limit_MemberGrade_Grade == tools.NullInt(Session["member_grade"]) && tools.NullInt(Session["member_id"]) > 0))
                {
                    if (limitinfo.Promotion_Limit_Limit > 0 && limitinfo.Promotion_Limit_Limit < buy_amount)
                    {
                        //pub.Msg("error", "错误信息", "该产品一次最多可购买" + limitinfo.Promotion_Limit_Limit + "件", false, "{back}");
                        Response.Write("该产品一次最多可购买" + limitinfo.Promotion_Limit_Limit + "件");
                        Response.End();
                    }
                }
            }
        }

        ProductInfo productinfo = MyProduct.GetProductByID(product_id, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (productinfo != null)
        {
            //检查商品
            if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == "CN")
            {
                //获取购物车已有库存
                cart_amount = MyCart.Get_Orders_Goods_Amount(Session.SessionID, product_id);

                OrdersGoodsTmpInfo goodstmp = MyCart.GetOrdersGoodsTmpByID(goods_id);
                if (goodstmp != null)
                {
                    if (goodstmp.Orders_Goods_Product_ID == product_id && goodstmp.Orders_Goods_SessionID == Session.SessionID)
                    {
                        //扣除原有数量
                        cart_amount = cart_amount - goodstmp.Orders_Goods_Amount;

                        //如果不是零库存商品,判断是否超过可用库存，否则不予判断
                        if (productinfo.Product_IsNoStock == 0)
                        {
                            //检查购买数量是否超过可用购买数量
                            if ((buy_amount + cart_amount) > productinfo.Product_UsableAmount)
                            {

                                Response.Write("该商品暂时缺货！");
                                Response.End();
                            }
                        }

                        //检查购买数量是否超过购买限额
                        if (productinfo.Product_QuotaAmount > 0 && (buy_amount + cart_amount) > productinfo.Product_QuotaAmount)
                        {
                            //pub.Msg("error", "错误信息", "购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit, false, "/cart/my_cart.aspx");
                            Response.Write("购买数量超过商品限购数量" + productinfo.Product_QuotaAmount + productinfo.Product_Unit);
                            Response.End();
                        }

                        if (buy_amount + cart_amount < productinfo.U_Product_MinBook)
                        {
                            Response.Write("购买数量低于最小起订量" + productinfo.U_Product_MinBook + productinfo.Product_Unit);
                            Response.End();
                        }

                        //检查是否为积分兑换
                        if (productinfo.Product_IsCoinBuy == 0 && goodstmp.Orders_Goods_Type == 3)
                        {
                            Coinover_Flag = false;
                        }

                        Product_Price = productinfo.Product_Price;        //商品价格
                        IsGroup = productinfo.Product_IsGroupBuy;
                        Group_BuyAmount = productinfo.Product_GroupNum;
                        Group_Price = productinfo.Product_GroupPrice;


                        Product_Price = productclass.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                        normal_amount = MyCart.Get_Orders_Goods_TypeAmount(Session.SessionID, productinfo.Product_ID, 0);
                        //检查是否团购
                        if (IsGroup == 1)
                        {
                            if ((buy_amount) >= Group_BuyAmount)
                            {
                                Product_Price = Group_Price;
                            }
                        }

                        Product_Coin = Get_Member_Coin(Product_Price);

                        //检查是否赠送指定积分
                        if (productinfo.Product_IsGiftCoin == 1)
                        {
                            Product_Coin = (int)(Product_Price * productinfo.Product_Gift_Coin);
                        }

                        //如果为积分兑换
                        if (goodstmp.Orders_Goods_Type == 3 && Coinover_Flag == true)
                        {
                            Product_Price = 0;
                            Product_Coin = 0;
                            CoinBuy_Coin = productinfo.Product_CoinBuy_Coin;

                            //判断用户积分是否大于购买该商品所需总积分，coinoverflag为1表示不大于，0表示大于可以换取
                            if (My_Cart_CheckCoinBuy(product_id, (CoinBuy_Coin * buy_amount)) == false)
                            {
                                Coinover_Flag = false;
                            }
                        }

                        if (Coinover_Flag)
                        {
                            double Product_SalePrice = productclass.GetProductWholeSalePrice(product_id, buy_amount);

                            if (Product_SalePrice == 0)
                            {
                                goodstmp.Orders_Goods_Product_Price = Product_Price;
                            }
                            else
                            {
                                goodstmp.Orders_Goods_Product_Price = Product_SalePrice;
                            }
                            goodstmp.Orders_Goods_Product_UseCoin = CoinBuy_Coin;
                            goodstmp.Orders_Goods_Product_Coin = Product_Coin;
                            goodstmp.Orders_Goods_Amount = buy_amount;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_OrdersID = 0;

                            MyCart.EditOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }
            }
        }

        //if (Coinover_Flag == false)
        //{
        //    if (tools.CheckInt(Session["supplier_id"].ToString()) > 0)
        //    {
        //        //pub.Msg("info", "提示信息", "您的" + Application["Coin_Name"] + "不足，该操作没有生效", false, "{back}");
        //        Response.Write("您的" + Application["Coin_Name"] + "不足，该操作没有生效");
        //        Response.End();
        //    }
        //    else
        //    {
        //        Session["url_after_login"] = "/cart/my_cart.aspx?action=add_coinbuy&product_id=" + product_id;
        //        //pub.Msg("info", "提示信息", "您尚未登录，该操作没有生效", true, 3, "/login.aspx?u_type=1");
        //        Response.Write("您尚未登录，该操作没有生效");
        //        Response.End();
        //    }
        //}


        if (Convert.ToString(Request.QueryString["passto"]) == "confirm")
        {

            Response.Write("ToCart");
            Response.End();
        }
        else if (Convert.ToString(Request["passto"]) == "add")
        {
            Response.Write("商品已加入购物车！");
            Response.End();
        }
        else
        {
            if (orders_sn.ToString().Length > 0)
            {
                Response.Redirect("/member/Order_Contract.aspx?orders_sn=" + orders_sn + "");
            }

        }
    }

    //修改套装到购物车
    public void Cart_PackageEdit(int goods_id, int package_id, int buy_amount)
    {
        int cart_amount = 0;
        int package_stock = 0;
        bool goods_isrepeat = false;
        int Package_Coin = 0;
        int goods_parentid = 0;
        double Package_Price, Package_MKTPrice;
        string Package_Product_Arry = "0";
        OrdersGoodsTmpInfo goodstmp = null;
        ProductStockInfo packagestockinfo = new ProductStockInfo();

        //获取套装信息
        PackageInfo packageInfo = Mypackage.GetPackageByID(package_id, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
        if (packageInfo != null)
        {
            //检查上架状态
            if (packageInfo.Package_IsInsale == 1)
            {

                //库存判断
                packagestockinfo = productclass.Get_Package_Stock(packageInfo.PackageProductInfos);
                package_stock = packagestockinfo.Product_Stock_Amount;
                if ((buy_amount > package_stock) && packagestockinfo.Product_Stock_IsNoStock == 0)
                {
                    pub.Msg("error", "错误信息", "该商品暂时缺货", false, "/cart/my_cart.aspx");
                    Response.End();
                }

                goodstmp = MyCart.GetOrdersGoodsTmpByID(goods_id);
                if (goodstmp != null)
                {
                    //记录原数量
                    cart_amount = goodstmp.Orders_Goods_Amount;
                    Package_Price = productclass.Get_Member_Price(0, packageInfo.Package_Price);
                    Package_MKTPrice = productclass.Get_Package_MKTPrice(packageInfo.PackageProductInfos);

                    Package_Coin = Get_Member_Coin(Package_Price);

                    if (Package_MKTPrice == 0)
                    {
                        Package_MKTPrice = Package_Price;
                    }

                    goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                    goodstmp.Orders_Goods_SessionID = Session.SessionID;
                    goodstmp.Orders_Goods_Product_Name = packageInfo.Package_Name;
                    goodstmp.Orders_Goods_Product_Price = Package_Price;
                    goodstmp.Orders_Goods_Product_MKTPrice = Package_MKTPrice;
                    goodstmp.Orders_Goods_Product_Coin = Package_Coin;
                    goodstmp.Orders_Goods_Amount = buy_amount;
                    goodstmp.Orders_Goods_Addtime = DateTime.Now;
                    goodstmp.Orders_Goods_OrdersID = 0;

                    MyCart.EditOrdersGoodsTmp(goodstmp);

                    QueryInfo Query = new QueryInfo();
                    Query.PageSize = 0;
                    Query.CurrentPage = 1;
                    if (tools.NullInt(Session["member_id"]) > 0)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
                    }
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", goods_id.ToString()));
                    IList<OrdersGoodsTmpInfo> cartgoods = MyCart.GetOrdersGoodsTmps(Query);
                    if (cartgoods != null)
                    {
                        foreach (OrdersGoodsTmpInfo entity in cartgoods)
                        {
                            goodstmp = new OrdersGoodsTmpInfo();
                            goodstmp = entity;
                            goodstmp.Orders_Goods_Amount = (goodstmp.Orders_Goods_Amount / cart_amount) * buy_amount;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_OrdersID = 0;

                            MyCart.EditOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }
            }
        }
        Response.Redirect("/cart/my_cart.aspx");
    }

    //删除购物车商品
    public void Cart_ProductDel(int goods_id)
    {
        //删除普通商品
        if (MyCart.DelOrdersGoodsTmp(goods_id, 0, 0, Session.SessionID) > 0)
        {
            //删除积分兑换商品
            //MyCart.DelOrdersGoodsTmp(goods_id, 3, 0, Session.SessionID);

            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("error");
            Response.End();
        }
        ////删除普通商品
        //MyCart.DelOrdersGoodsTmp(goods_id, 0, 0, Session.SessionID);
        ////删除积分兑换商品
        //MyCart.DelOrdersGoodsTmp(goods_id, 3, 0, Session.SessionID);
        ////Response.Write("success");
        //Response.Redirect("/cart/my_cart.aspx");
        //Response.End();
    }

    //删除购物车套装
    public void Cart_PackageDel(int goods_id)
    {
        //删除普通商品
        if (MyCart.DelOrdersGoodsTmp(goods_id, 2, 0, Session.SessionID) > 0)
        {
            //删除积分兑换商品
            MyCart.DelOrdersGoodsTmp(goods_id, 2, goods_id, Session.SessionID);

            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("error");
            Response.End();
        }
    }

    //清除购物车
    public void Cart_ProductDel_All()
    {
        MyCart.ClearOrdersGoodsTmp(Session.SessionID);

        Response.Redirect("/cart/my_cart.aspx");
    }

    public void Cart_ProductDel_Batch()
    {
        string goods_ids = tools.CheckStr(Request["chk_cart_goods"]);
        int result = MyCart.ClearOrdersGoodsTmpByGoodsID(goods_ids);
        Response.Write("success");
        Response.End();
    }

    #endregion

    #region "购物车"

    public IList<OrdersGoodsTmpInfo> Get_Carts(int SupplyID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_Product_IsFavor", "=", "1"));
        IList<OrdersGoodsTmpInfo> Cartinfos = MyCart.GetOrdersGoodsTmps(Query);
        return Cartinfos;
    }

    public IList<OrdersGoodsTmpInfo> Get_Orders_Carts(int Orders_ID)
    {
        return ordersclass.Get_Orders_Carts(Orders_ID);
    }

    //购物车有效性检查
    public void Check_Cart_Valid()
    {
        int gradeid = 1;
        MemberGradeInfo GradeInfo = memberclass.GetMemberDefaultGrade();
        if (GradeInfo != null)
        {
            gradeid = GradeInfo.Member_Grade_ID;
        }

        PromotionLimitInfo limitinfo;
        ProductInfo productinfo = null;
        PackageInfo packageinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                //检查普通商品
                if ((entity.Orders_Goods_Type == 0 || entity.Orders_Goods_Type == 3) && entity.Orders_Goods_ParentID == 0)
                {
                    productinfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                    if (productinfo != null)
                    {
                        //删除未上架未审核商品
                        if (productinfo.Product_IsInsale == 0 || productinfo.Product_IsAudit == 0)
                        {
                            MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                            MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                            break;
                        }
                        //删除无库存商品
                        if (productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount == 0)
                        {
                            MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                            MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                            break;
                        }
                        //删除非积分兑换商品
                        if (entity.Orders_Goods_Type == 3 && productinfo.Product_IsCoinBuy == 0)
                        {
                            MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                            MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                            break;
                        }
                        //删除当前登录商家本身的商品
                        if (tools.NullInt(Session["member_id"]) == productinfo.Product_SupplierID && productinfo.Product_SupplierID > 0)
                        {
                            MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                            MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                            break;
                        }
                    }
                    else
                    {
                        MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                        MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                        break;
                    }

                    //判断是否超过限时产品每笔购买上限
                    limitinfo = MyLimit.GetPromotionLimitByProductID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
                    if (limitinfo != null)
                    {
                        foreach (PromotionLimitMemberGradeInfo gradeinfo in limitinfo.PromotionLimitMemberGrades)
                        {
                            if ((tools.NullInt(Session["member_id"]) == 0 && gradeinfo.Promotion_Limit_MemberGrade_Grade == gradeid) || (gradeinfo.Promotion_Limit_MemberGrade_Grade == tools.NullInt(Session["member_grade"]) && tools.NullInt(Session["member_id"]) > 0))
                            {
                                if (limitinfo.Promotion_Limit_Limit > 0 && limitinfo.Promotion_Limit_Limit < entity.Orders_Goods_Amount)
                                {
                                    Cart_ProductEdit(entity.Orders_Goods_ID, entity.Orders_Goods_Product_ID, limitinfo.Promotion_Limit_Limit);
                                }
                            }
                        }
                    }
                }
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                {
                    packageinfo = Mypackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                    if (packageinfo != null)
                    {
                        //删除未上架套装
                        if (packageinfo.Package_IsInsale == 0)
                        {
                            MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                            MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                            break;
                        }

                    }
                    else
                    {
                        //删除不存在套装
                        MyCart.DelOrdersGoodsTmp(entity.Orders_Goods_ID, entity.Orders_Goods_Type, 0, Session.SessionID);
                        MyCart.DelOrdersGoodsTmp(0, entity.Orders_Goods_Type, entity.Orders_Goods_ID, Session.SessionID);
                        break;
                    }
                }
            }
        }
    }

    //优惠政策判断
    public double Cart_Check_Policy()
    {
        double favor_price;
        favor_price = 0;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Carts(SupplyID);
        IList<FavorDiscountInfo> Favorinfos;
        Favorinfos = MyFavor.Get_Policy_Discount(Cartinfos, tools.NullInt(Session["member_grade"]), "CN");
        if (Favorinfos != null)
        {
            foreach (FavorDiscountInfo Favorinfo in Favorinfos)
            {
                favor_price = favor_price + Favorinfo.Discount_Amount;
            }
        }

        return favor_price;
    }

    //赠品优惠判断
    public string Cart_Check_Gift()
    {
        int favor_rank, ininum;
        string Product_Name, return_value;
        return_value = "";
        favor_rank = 0;
        ProductInfo productinfo;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Carts(SupplyID);
        IList<PromotionFavorGiftInfo> FavorGifts;
        FavorGifts = MyFavor.Get_Gift_Discount(Cartinfos, tools.NullInt(Session["member_grade"]), "CN");
        int innernum = 0;
        if (FavorGifts != null)
        {
            foreach (PromotionFavorGiftInfo entity in FavorGifts)
            {
                favor_rank = favor_rank + 1;
                ininum = 0;

                if (entity.Promotion_Gift_Amounts != null)
                {
                    foreach (PromotionFavorGiftAmountInfo amount in entity.Promotion_Gift_Amounts)
                    {
                        innernum = 0;
                        if (amount.Promotion_Gift_Gifts != null)
                        {
                            ininum = ininum + 1;
                            foreach (PromotionFavorGiftGiftInfo giftinfo in amount.Promotion_Gift_Gifts)
                            {
                                innernum = innernum + 1;
                                productinfo = MyProduct.GetProductByID(giftinfo.Gift_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                                if (productinfo != null && productinfo.Product_SupplierID == SupplyID)
                                {
                                    if (productinfo.Product_IsNoStock == 0)
                                    {
                                        if (giftinfo.Gift_Amount <= productinfo.Product_UsableAmount)
                                        {
                                            if (innernum == 1)
                                            {
                                                if (ininum == 1)
                                                {
                                                    return_value = return_value + entity.Promotion_Gift_Title + "<br/>";
                                                    return_value = return_value + "<input type=\"radio\" name=\"order_gift" + favor_rank + "\" value=\"" + amount.Gift_Amount_ID + "\" checked> ";
                                                }
                                                else
                                                {
                                                    return_value = return_value + "<input type=\"radio\" name=\"order_gift" + favor_rank + "\" value=\"" + amount.Gift_Amount_ID + "\"> ";
                                                }
                                            }
                                            return_value = return_value + productinfo.Product_Name + " × " + giftinfo.Gift_Amount + "； ";
                                            innernum = innernum + 1;
                                        }
                                        else
                                        {
                                            innernum = innernum - 1;
                                        }
                                    }
                                    else
                                    {
                                        if (innernum == 1)
                                        {
                                            if (ininum == 1)
                                            {
                                                return_value = return_value + entity.Promotion_Gift_Title + "<br/>";
                                                return_value = return_value + "<input type=\"radio\" name=\"order_gift" + favor_rank + "\" value=\"" + amount.Gift_Amount_ID + "\" checked> ";
                                            }
                                            else
                                            {
                                                return_value = return_value + "<input type=\"radio\" name=\"order_gift" + favor_rank + "\" value=\"" + amount.Gift_Amount_ID + "\"> ";
                                            }
                                        }
                                        return_value = return_value + productinfo.Product_Name + " × " + giftinfo.Gift_Amount + "； ";
                                        innernum = innernum + 1;

                                    }

                                }
                                else
                                {
                                    innernum = innernum - 1;
                                }
                            }
                            if (innernum > 1)
                            {
                                return_value = return_value + "<br>";
                            }
                        }
                    }
                }
            }

        }

        return return_value;
    }

    //判断购物车商品是否为同一供应商提供
    public bool My_Cart_ProductSupplier(IList<OrdersGoodsTmpInfo> goodstmps)
    {
        Session["Supplier_List"] = "";
        string Supplier_List = "";
        foreach (OrdersGoodsTmpInfo entity in goodstmps)
        {
            if (entity.Orders_Goods_ParentID == 0)
            {
                if (Supplier_List == "")
                {
                    Supplier_List += entity.Orders_Goods_Product_SupplierID;
                }
                else
                {
                    string[] list = Supplier_List.Split(',');
                    bool bz = true;
                    foreach (string str in list)
                    {
                        if (str == entity.Orders_Goods_Product_SupplierID.ToString())
                        {
                            bz = false;
                        }
                    }
                    if (bz)
                    {
                        Supplier_List += "," + entity.Orders_Goods_Product_SupplierID;
                    }
                }
            }
        }
        Session["Supplier_List"] = Supplier_List;
        if (Supplier_List.IndexOf(',') > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //购物车商品列表
    public CartInfo My_Cart_ProductList_bak(bool isproview)
    {
        Check_Cart_Valid();

        int favor_flag = 0;
        double total_nofavor_price = 0;
        double total_mktprice = 0;
        double total_price = 0;
        int total_coin = 0;
        double total_weight = 0;
        int totalcount = 0;
        string productURL = string.Empty;

        string contract_url = "";
        CartInfo cart = null;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            Response.Write("<div class=\"main_info01\">");
            Response.Write("<table width=\"988\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
            Response.Write("  <tr>");
            Response.Write("    <td width=\"100\" class=\"tit\">商品编号</td>");
            Response.Write("    <td width=\"335\" class=\"tit\">商品</td>");
            Response.Write("    <td width=\"132\" class=\"tit\">单价</td>");
            Response.Write("    <td width=\"197\" class=\"tit\">供应商</td>");
            Response.Write("    <td width=\"126\" class=\"tit\">数量</td>");
            Response.Write("    <td width=\"116\" class=\"tit\">操作</td>");
            Response.Write("  </tr>");
            totalcount = 0;
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_ParentID == 0)
                {
                    SupplierInfo sinfo = null;
                    if (entity.Orders_Goods_Product_SupplierID > 0)
                    {
                        sinfo = MySupplier.GetSupplierByID(entity.Orders_Goods_Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        if (sinfo != null)
                        {
                            if (sinfo.Supplier_IsHaveShop == 0)
                            {
                                sinfo = null;
                            }
                        }
                    }
                    string Supplier_name = "易耐平台";
                    if (sinfo != null)
                    {
                        Supplier_name = sinfo.Supplier_CompanyName;
                    }

                    totalcount += entity.Orders_Goods_Amount;
                    //统计信息
                    favor_flag = entity.Orders_Goods_Product_IsFavor;
                    if (favor_flag == 0)
                    {
                        total_nofavor_price = total_nofavor_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
                    }

                    total_mktprice = total_mktprice + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_MKTPrice);
                    total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                    total_coin = total_coin + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);

                    Response.Write("<tr>");
                    Response.Write("<td>" + entity.Orders_Goods_Product_Code + "</td>");
                    Response.Write("<td>");
                    if (entity.Orders_Goods_Product_ID > 0)
                    {
                        if (entity.Orders_Goods_Type != 2)
                        {
                            productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());
                            Response.Write("    <dl>");
                            Response.Write("        <dt><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + entity.Orders_Goods_Product_Name + "\" /></dt>");
                            Response.Write("        <dd>");
                            Response.Write("            <p>" + entity.Orders_Goods_Product_Name + "</p>");
                            Response.Write("        </dd>");
                            Response.Write("    </dl>");
                        }
                        else
                        {
                            Response.Write("    <dl>");
                            Response.Write("        <dd>");
                            Response.Write("            <p>[套装] " + entity.Orders_Goods_Product_Name + "</p>");
                            Response.Write("        </dd>");
                            Response.Write("    </dl>");
                        }
                    }

                    Response.Write("</td>");
                    Response.Write("<td><strong>" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</strong></td>");
                    Response.Write("<td>" + Supplier_name + "</td>");
                    Response.Write("<td><a class=\"a_01\" onclick=\"setAmount.reduce('#buy_amount_" + entity.Orders_Goods_ID + "'); $('#buy_amount_" + entity.Orders_Goods_ID + "').blur();\"></a>");
                    Response.Write("<label>");
                    if (entity.Orders_Goods_Type != 2)
                    {
                        //Response.Write("<input class=\"input05\" name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" class=\"input_buy_amount\" value=\"" + entity.Orders_Goods_Amount + "\" size=\"1\" onblur=\"location='/cart/cart_do.aspx?action=renew&product_id=" + entity.Orders_Goods_Product_ID + "&goods_id=" + entity.Orders_Goods_ID + "&buy_amount='+MM_findObj('buy_amount_" + entity.Orders_Goods_ID + "').value;\" maxlength=\"8\">");
                        Response.Write("<input class=\"input05\" name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" class=\"input_buy_amount\" value=\"" + entity.Orders_Goods_Amount + "\" size=\"1\" onblur=\"Renew(" + entity.Orders_Goods_Product_ID + "," + entity.Orders_Goods_ID + "," + entity.Orders_Goods_ID + ")");
                    }
                    else
                    {
                        Response.Write("<input class=\"input05\" name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" class=\"input_buy_amount\" value=\"" + entity.Orders_Goods_Amount + "\" size=\"1\" onblur=\"location='/cart/cart_do.aspx?action=packrenew&package_id=" + entity.Orders_Goods_Product_ID + "&goods_id=" + entity.Orders_Goods_ID + "&buy_amount='+MM_findObj('buy_amount_" + entity.Orders_Goods_ID + "').value;\" maxlength=\"8\">");
                    }
                    Response.Write("</label><a class=\"a_02\" onclick=\"setAmount.add('#buy_amount_" + entity.Orders_Goods_ID + "'); $('#buy_amount_" + entity.Orders_Goods_ID + "').blur();\"></a></td>");
                    //if (entity.Orders_Goods_Type != 2)
                    //{
                    //    Response.Write("<td><a href=\"/cart/cart_do.aspx?action=move&goods_id=" + entity.Orders_Goods_ID + "\" onclick=\"if(confirm('确定将该商品从购物车删除吗？')==false){return false;}\" >删除</a></td>");
                    //}
                    //else
                    //{
                    //    Response.Write("<td><a href=\"/cart/cart_do.aspx?action=packmove&goods_id=" + entity.Orders_Goods_ID + "\" onclick=\"if(confirm('确定将该商品从购物车删除吗？')==false){return false;}\" >删除</a></td>");

                    //}
                    if (entity.Orders_Goods_Type != 2)
                    {

                        Response.Write("<td width=\"195\"><a href=\"/cart/cart_do.aspx?action=move&goods_id=" + entity.Orders_Goods_ID + "\" onclick=\"if(confirm('确定将该商品从购物车删除吗？')==false){return false;}\" >删除</a></td>");
                        // strHTML.Append("<td width=\"195\"><a  href=\"javascript:void();\" onclick=\"movecartgood(" + entity.Orders_Goods_ID + ")\">删除商品</a> / <a href=\"javascript:void();\" onclick=\"AjaxCheckLogin_ShouCang('/cart/My_cart.aspx'," + entity.Orders_Goods_Product_ID + ")\">加入收藏</a></td>");
                    }
                    else
                    {
                        Response.Write("<td><a href=\"/cart/cart_do.aspx?action=packmove&goods_id=" + entity.Orders_Goods_ID + "\" onclick=\"if(confirm('确定将该商品从购物车删除吗？')==false){return false;}\" >删除</a></td>");

                    }
                    Response.Write("</tr>");

                }

            }
            Response.Write("</table>");
            if (My_Cart_ProductSupplier(goodstmps))
                contract_url = "/cart/order_confirm.aspx?SupplyID=" + tools.NullInt(Session["Supplier_List"]);
            else
                contract_url = "/cart/splitcart.aspx";

            Response.Write("</div>");

            cart = new CartInfo();
            cart.Total_Product_MktPrice = total_mktprice;
            cart.Total_Product_Price = total_price;
            cart.Total_Product_Coin = total_coin;
            cart.Total_Nofavor_Product_Price = total_nofavor_price;
            cart.Total_Favor_Price = 0;
            cart.Total_Weight = total_weight;

            Response.Write("<div class=\"main_info02\">");
            Response.Write("       <span>" + totalcount + "件商品 总计：<strong>" + pub.FormatCurrency(total_price) + "</strong></span><a href=\"javascript:location.href='cart_do.aspx?action=moveall'\" onclick=\"if(confirm('您确定要清空购物车吗？')==false){return false;}\">清空购物车</a>");
            Response.Write(" </div>");
            Response.Write("<div class=\"main_info03\">");
            Response.Write("      <a href=\"" + contract_url + "\" class=\"a_03\">去结算 ></a><a href=\"/\" class=\"a_04\">继续购买</a>");
            Response.Write("</div>");


        }
        else
        {
            Response.Write("<div class=\"main_info01\">");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
            Response.Write("  <tr bgcolor=\"ffffff\">");
            Response.Write("    <td width=\"100\"> </td>");
            Response.Write("    <td style=\"width:0px;height:150px;\" align=\"center\"><img src=\"/Images/Shoppingcart_128x128.gif\" width=\"128\" height=\"128\" /></td>");
            Response.Write("    <td style=\"text-align:left; padding-left:10px;\"><h1 class=\"t14\">您的购物车还是空的，赶快行动吧！您可以：</h1>");
            Response.Write("      <p style=\"line-height:30px;\">马上去 <a href=\"/index.aspx\" class=\"a_t12_blue\"><u>挑选商品</u></a></p>");
            Response.Write("    <p>看看 <a href=\"/supplier/orders_list.aspx\" class=\"a_t12_blue\"><u>我的订单</u></a></p>");
            Response.Write("    </td>");
            Response.Write("    <td width=\"200\"></td>");
            Response.Write("  </tr>");
            Response.Write("</table>");
            Response.Write("</div>");
        }
        return cart;
    }

    public CartInfo My_Cart_ProductList(bool isproview)
    {
        Check_Cart_Valid();

        StringBuilder strHTML = new StringBuilder();


        string supplier_ids = MyCart.GetOrdersGoodsTmpSupplierID(Session.SessionID, tools.NullInt(Session["member_id"]));
        //string supplier_ids = MyCart.GetOrdersGoodsTmpSupplierID("it1lb355k5qu5b55tfvgp02t", tools.NullInt(Session["member_id"]));

        string[] supplier_arry;
        if (supplier_ids != "")
        {
            supplier_arry = supplier_ids.Split(',');
        }
        else
        {
            supplier_arry = new string[0];
        }

        int favor_flag = 0;
        double total_nofavor_price = 0;
        double total_mktprice = 0;
        double total_price = 0;
        int total_coin = 0;
        double total_weight = 0;
        int totalcount = 0;
        string productURL = string.Empty;
        string supplier_name = "";
        string contract_url = "";
        CartInfo cart = null;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        SupplierInfo supplierInfo = null;
        int i = 0;
        if (supplier_arry.Length > 0 && supplier_arry != null)
        {
            strHTML.Append("<h2 class=\"title08\" style=\"margin-bottom:15px;\"><strong>购物车</strong> </h2>");
            strHTML.Append("<form id=\"cart_form\" name=\"cart_form\" action=\"/cart/order_confirm.aspx\" method=\"post\" onsubmit=\"return check_cartform();\">");
            foreach (string supplier_id in supplier_arry)
            {
                i++;
                if (tools.CheckInt(supplier_id) > 0)
                {
                    supplierInfo = MySupplier.GetSupplierByID(tools.CheckInt(supplier_id), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierInfo != null)
                    {
                        supplier_name = supplierInfo.Supplier_CompanyName;
                    }
                    else
                    {
                        supplier_name = "第三方供货商";
                    }
                }
                else
                {
                    supplier_name = "易耐平台";
                }



                strHTML.Append("<div class=\"blk35_sz\">");

                strHTML.Append("<h2>");
                strHTML.Append("<ul>");
                if (i == 1)
                {
                    strHTML.Append("<li style=\"width:405px;\"><input type=\"radio\" name=\"SupplyID\" value=\"" + supplier_id + "\" checked/><strong>" + supplier_name + "</strong></li>");
                }
                else
                {
                    strHTML.Append("<li style=\"width:405px;\"><input type=\"radio\" name=\"SupplyID\" value=\"" + supplier_id + "\" /><strong>" + supplier_name + "</strong></li>");
                }
                strHTML.Append("<li style=\"width:107px;text-align:center;\">单价</li>");
                strHTML.Append("<li style=\"width:208px;text-align:center;\">数量</li>");
                strHTML.Append("<li style=\"width:83px;text-align:center;\">小计（元）</li>");
                strHTML.Append("<li style=\"width:194px;text-align:center;\">操作</li>");
                strHTML.Append("</ul>");
                strHTML.Append("<div class=\"clear\"></div>");
                strHTML.Append("</h2>");

                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                if (tools.NullInt(Session["member_id"]) > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
                }
                else
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", supplier_id));
                IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);

                if (goodstmps != null)
                {
                    foreach (OrdersGoodsTmpInfo entity in goodstmps)
                    {
                        if (entity.Orders_Goods_ParentID == 0)
                        {
                            goods_product = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                            if (goods_product != null)
                            {
                                productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());

                                strHTML.Append("<div class=\"b35_main_sz\">");
                                strHTML.Append("<table width=\"998\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                                strHTML.Append("<tr>");
                                strHTML.Append("<td width=\"405\">");
                                strHTML.Append("<dl>");
                                //
                                //strHTML.Append("<input type=\"checkbox\" id=\"chk_cart_goods_" + supplier_id + "_" + entity.Orders_Goods_ID + "\" name=\"chk_cart_goods\" value=\"" + entity.Orders_Goods_ID + "\" onclick=\"setSelectSubAll(" + supplier_id + "," + entity.Orders_Goods_ID + ");\" class=\"select-sub" + supplier_id + "\"   checked=\"checked\"  />");




                                strHTML.Append("<input type=\"checkbox\" id=\"chk_cart_goods_" + supplier_id + "_" + entity.Orders_Goods_ID + "\" name=\"chk_cart_goods\" value=\"" + entity.Orders_Goods_ID + "\" onclick=\"getCountPrice(" + supplier_id + "," + entity.Orders_Goods_ID + ");\" class=\"select-sub" + supplier_id + "\"   checked=\"checked\"  /><input type=\"hidden\" id=\"price_" + entity.Orders_Goods_ID + "\" value=\"" + entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price + "\"/>");

                                strHTML.Append("<dt><a href=\"" + productURL + "\" target=\"_blank\">");
                                strHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                                strHTML.Append("<dd>");
                                strHTML.Append("<p><a href=\"" + productURL + "\" >" + entity.Orders_Goods_Product_Name + "</a></p>");
                                //strHTML.Append("<p>编号：" + entity.Orders_Goods_Product_Code + "</p>");
                                //strHTML.Append("<p>材质型号：" + entity.Orders_Goods_Product_Spec + "</p>");
                                //new Product().Product_Extend_Content_New(goods_product.Product_ID)
                                strHTML.Append("" + new Product().Product_Extend_Content_New(goods_product.Product_ID) + "");
                                strHTML.Append("</dd>");
                                strHTML.Append("<div class=\"clear\"></div>");
                                strHTML.Append("</dl>");
                                strHTML.Append("</td>");

                                strHTML.Append("<td width=\"107\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>");
                                total_mktprice = total_mktprice + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_MKTPrice);
                                total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);


                                strHTML.Append("<td width=\"208\"><em><a onclick=\"setAmount.reduce('#buy_amount_" + entity.Orders_Goods_ID + "'); $('#buy_amount_" + entity.Orders_Goods_ID + "').blur();\" class=\"a26\"></a>");
                                if (entity.Orders_Goods_Type != 2)
                                {

                                    strHTML.Append("<input name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" value=\"" + entity.Orders_Goods_Amount + "\"  onblur=\"location='/cart/cart_do.aspx?action=renew&product_id=" + entity.Orders_Goods_Product_ID + "&goods_id=" + entity.Orders_Goods_ID + "&buy_amount='+MM_findObj('buy_amount_" + entity.Orders_Goods_ID + "').value;\" maxlength=\"8\">");
                                    //strHTML.Append("<input name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" value=\"" + entity.Orders_Goods_Amount + "\"  onclick=\"AAA(" + entity.Orders_Goods_Product_ID + "," + entity.Orders_Goods_ID + ","+MM_findObj('buy_amount_" + entity.Orders_Goods_ID + "').value+");\"/>");
                                }
                                else
                                {
                                    strHTML.Append("<input name=\"buy_amount_" + entity.Orders_Goods_ID + "\" id=\"buy_amount_" + entity.Orders_Goods_ID + "\" type=\"text\" value=\"" + entity.Orders_Goods_Amount + "\" onblur=\"$.post('/cart/cart_do.aspx?action=packrenew&package_id=" + entity.Orders_Goods_Product_ID + "&goods_id=" + entity.Orders_Goods_ID + "&buy_amount='+MM_findObj('buy_amount_" + entity.Orders_Goods_ID + "').value);\"/>");
                                }
                                strHTML.Append("<a onclick=\"setAmount.add('#buy_amount_" + entity.Orders_Goods_ID + "'); $('#buy_amount_" + entity.Orders_Goods_ID + "').blur();\" class=\"a27\"></a></em></td>");


                                strHTML.Append("<td width=\"83\"><strong>" + pub.FormatCurrency(entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + "</strong></td>");

                                strHTML.Append("<td width=\"195\"><a  href=\"javascript:void(0);\" onclick=\"movecartgood(" + entity.Orders_Goods_ID + ")\">删除商品</a> / <a href=\"javascript:;\" onclick=\"favorites_add_ajax(" + entity.Orders_Goods_Product_ID + ",'product');\">加入收藏</a></td>");
                                strHTML.Append("</tr>");
                                strHTML.Append("</table>");
                                strHTML.Append("</div>");
                            }
                        }
                    }
                }
                strHTML.Append("</div>");
            }
            strHTML.Append("<div class=\"blk36_sz\">");
            strHTML.Append("<div class=\"b36_left_sz\">");
            //strHTML.Append("<input name=\"chk_all_goods\" id=\"chk_all_goods\"  onclick=\"check_Cart_All();\" type=\"checkbox\"   checked=\"checked\" />全选 <a href=\"javascript:;\" onclick=\"MoveCartGoodsByID();\">删除选中的商品</a>  </div>");
            strHTML.Append("<input name=\"chk_all_goods\" id=\"chk_all_goods\"  onclick=\"check_Cart_All_New();\" type=\"checkbox\"   checked=\"checked\" />全选 <a href=\"javascript:;\" onclick=\"MoveCartGoodsByID();\">删除选中的商品</a>  </div>");
            strHTML.Append("<div class=\"b36_right_sz\">");
            strHTML.Append("<p class=\"p1\"><strong id=\"strong_totalprice\">" + pub.FormatCurrency(total_price) + "</strong><span>总计金额：</span></p>");
            strHTML.Append("</div>");
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("<div class=\"b36_info_sz\">");
            strHTML.Append("<span><a href=\"javascript:;\" onclick=\"$('#cart_form').submit();\" >去结算</a></span>");
            //strHTML.Append("<input type=\"hidden\" id=\"supplier_id\" name=\"supplier_id\" value=\"" + supplier_id + "\"/>");
            strHTML.Append("<a href=\"javascript:;\" onclick=\"location.href='/product/category.aspx'\"><< 继续购物 </a>");
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");
            strHTML.Append("</form>");
        }
        else
        {
            strHTML.Append("<h2 class=\"title08\"><strong>购物车</strong></h2>");
            strHTML.Append("<div class=\"blk35\">");
            strHTML.Append("<div class=\"empty\">");
            strHTML.Append("<p>购物车内暂时没有商品，您可以<a style=\"color:#ff6600;\" href=\"/index.aspx\">去首页</a>挑选喜欢的商品</p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");
        }
        Response.Write(strHTML.ToString());
        return cart;
    }

    //发票表单/备注/优惠券
    public void My_Cart_Invoice()
    {
        Response.Write("  <tr>");
        Response.Write("    <td colspan=\"7\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        Response.Write("      <tr>");
        Response.Write("        <td class=\"t14\"><strong>订单备注</strong></td>");
        Response.Write("        <td><input type=\"text\" name=\"order_note\" id=\"order_note\" size=\"60\" maxlength=\"100\"> </td>");
        Response.Write("      </tr>");
        Response.Write("    </table></td>");
        Response.Write("  </tr>");
        Response.Write("  <tr>");
        Response.Write("    <td colspan=\"7\" id=\"invoice\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        Response.Write("      <tr>");
        Response.Write("        <td height=\"30\">发票类型</td>");
        Response.Write("        <td><input type=\"radio\" name=\"ticket_type\" value=\"0\" onclick=\"MM_findObj('common_ticket').style.display='none';\"> 不需要发票 <input type=\"radio\" name=\"ticket_type\" value=\"1\" onclick=\"MM_findObj('common_ticket').style.display='';\" checked> 普通发票 </td>");
        Response.Write("      </tr>");
        Response.Write("        </table>");
        Response.Write("        <table border=\"0\" cellspacing=\"0\" cellpadding=\"5\" id=\"common_ticket\">");
        Response.Write("      <tr>");
        Response.Write("        <td>发票抬头</td>");
        //Response.Write("        <td><input type=\"text\" name=\"ticket_title\" value=\"个人\" size=\"40\"> <span class=\"t12_red\">*</span> 小于100个字符</td>");
        Response.Write("        <td><input type=\"hidden\" name=\"ticket_title\" value=\"个人\">个人  <span class=\"t12_red\">如需顾客姓名，请咨询在线客服</span></td>");
        Response.Write("      </tr>");
        Response.Write("    </table>");
        Response.Write("</td>");
        Response.Write("  </tr>");
        Response.Write("  <tr>");
        Response.Write("    <td colspan=\"7\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        Response.Write("      <tr>");
        Response.Write("        <td class=\"t14\"><img src=\"/images/fold.gif\" id=\"ico_fold\" onclick=\"$('#coupon_verify').show();$(this).hide();$('#ico_unfold').show();\"><img src=\"/images/unfold.gif\" id=\"ico_unfold\" style=\"display:none;\" onclick=\"$('#coupon_verify').hide();$(this).hide();$('#ico_fold').show();\"> <strong>使用优惠券/代金券</strong></td></tr>");
        Response.Write("        <tr><td id=\"coupon_verify\" style=\"display:none;\">");

        //Response.Write("卡号：<input type=\"text\" name=\"coupon_code\" id=\"coupon_code\" size=\"20\" maxlength=\"100\"> &nbsp; 验证码：<input type=\"text\" name=\"coupon_verifycode\" id=\"coupon_verifycode\" size=\"20\" maxlength=\"100\"> <img src=\"/images/btn_verify.gif\" align=\"absmiddle\" style=\"cousor:pointer\" onclick=\"$.ajaxSetup({async: false});$('#coupon_verify').load('/cart/cart_do.aspx?action=coupon_verify&coupon_code='+$('#coupon_code').val()+'&verify_code='+$('#coupon_verifycode').val()+'&time=' + Math.random());\">");
        //Response.Write("        <div style=\"padding:10px;line-height:22px;\">");

        //Response.Write("</div>");
        Cart_Coupon_Display();
        Response.Write("</td>");
        Response.Write("      </tr>");
        Response.Write("    </table></td>");
        Response.Write("  </tr>");
    }

    //显示已使用的优惠券
    public void Cart_Coupon_Display()
    {
        //if (tools.NullInt(Application["Coupon_UsedAmount"]) == 0 || (tools.NullStr(Session["order_favor_coupon"]) == "0" && tools.NullInt(Application["Coupon_UsedAmount"]) == 1))
        //{
        //    Response.Write("卡号：<input type=\"text\" name=\"coupon_code\" id=\"coupon_code\" size=\"20\" maxlength=\"100\"> &nbsp; 验证码：<input type=\"text\" name=\"coupon_verifycode\" id=\"coupon_verifycode\" size=\"20\" maxlength=\"100\"> <img src=\"/images/btn_verify.gif\" align=\"absmiddle\" style=\"cursor:pointer\" onclick=\"$.ajaxSetup({async: false});");
        //    Response.Write("$('#coupon_verify').load('/cart/cart_do.aspx?action=coupon_verify&coupon_code='+$('#coupon_code').val()+'&verify_code='+$('#coupon_verifycode').val()+'&time=' + Math.random());");
        //    Response.Write("$('#Cartpriceinfo').load('/cart/cart_do.aspx?action=update_cartprice&time=' + Math.random());");
        //    Response.Write("$('#total_price').load('/cart/cart_do.aspx?action=update_totalprice&time=' + Math.random());\">");
        //}
        Response.Write("        <div style=\"padding:10px;line-height:22px;\">");

        int i = 0;
        string all_coupon, valid_coupon, refresh_coupon;
        all_coupon = tools.NullStr(Session["all_favor_coupon"]);
        valid_coupon = tools.NullStr(Session["order_favor_coupon"]);
        bool Is_Valid = false;
        PromotionFavorCouponInfo couponinfo = null;
        refresh_coupon = "0";
        if (all_coupon != "0")
        {
            foreach (string subcoupon in all_coupon.Split(','))
            {
                if (tools.CheckInt(subcoupon) > 0)
                {
                    Is_Valid = false;
                    couponinfo = MyCoupon.GetPromotionFavorCouponByID(tools.CheckInt(subcoupon), pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                    if (couponinfo != null)
                    {
                        i = i + 1;
                        if (i > 1)
                        {
                            Response.Write("<br>");
                        }
                        if (valid_coupon != "0")
                        {
                            foreach (string subvalid in valid_coupon.Split(','))
                            {
                                if (subcoupon == subvalid)
                                {
                                    Is_Valid = true;
                                    break;
                                }
                            }
                            if (Is_Valid)
                            {
                                refresh_coupon = refresh_coupon + "," + subcoupon;
                                Response.Write("<a href=\"javascript:void(0);\" class=\"a_t12_blue\" onclick=\"$.ajaxSetup({async: false});");
                                Response.Write("$('#youhui_list').load('/cart/cart_do.aspx?action=coupon_del&coupon_id=" + subcoupon + "&time=' + Math.random());");
                                //Response.Write("$('#Cartpriceinfo').load('/cart/cart_do.aspx?action=update_cartprice&time=' + Math.random());");
                                Response.Write("$('#cart_price').load('/cart/cart_do.aspx?action=update_totalprice&time=' + Math.random());\">取消</a> &nbsp; " + couponinfo.Promotion_Coupon_Title);
                            }
                            else
                            {
                                Response.Write("<a href=\"javascript:void(0);\" class=\"a_t12_blue\" onclick=\"$.ajaxSetup({async: false});");
                                Response.Write("$('#youhui_list').load('/cart/cart_do.aspx?action=coupon_del&coupon_id=" + subcoupon + "&time=' + Math.random());");
                                //Response.Write("$('#Cartpriceinfo').load('/cart/cart_do.aspx?action=update_cartprice&time=' + Math.random());");
                                Response.Write("$('#cart_price').load('/cart/cart_do.aspx?action=update_totalprice&time=' + Math.random());\">取消</a> &nbsp; <span=\"t12_red\">请选用符合条件的优惠券</font>");
                            }
                        }
                        else
                        {
                            Response.Write("<a href=\"javascript:void(0);\" class=\"a_t12_blue\" onclick=\"$.ajaxSetup({async: false});");
                            Response.Write("$('#youhui_list').load('/cart/cart_do.aspx?action=coupon_del&coupon_id=" + subcoupon + "&time=' + Math.random());");
                            //Response.Write("$('#Cartpriceinfo').load('/cart/cart_do.aspx?action=update_cartprice&time=' + Math.random());");
                            Response.Write("$('#cart_price').load('/cart/cart_do.aspx?action=update_totalprice&time=' + Math.random());\">取消</a> &nbsp; <span=\"t12_red\">请选用符合条件的优惠券</font>");
                        }
                    }
                }
            }
        }
        Response.Write("</div>");
    }

    //验证优惠券
    public void Cart_Check_Coupon(string coupon_code, string coupon_verifycode)
    {
        string cate_arry, brand_arry, all_coupon;
        double cart_total_favor_price;
        bool Is_Contain = false;
        cate_arry = "";
        brand_arry = "";
        FavorDiscountInfo favorinfo;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Carts(SupplyID);
        PromotionFavorCouponInfo couponinfo = MyCoupon.GetPromotionFavorCouponByCode(coupon_code, coupon_verifycode, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));

        if (couponinfo != null)
        {
            all_coupon = tools.NullStr(Session["all_favor_coupon"]);
            if (all_coupon == "0")
            {
                Session["all_favor_coupon"] = Session["all_favor_coupon"] + "," + couponinfo.Promotion_Coupon_ID;
            }
            else
            {
                foreach (string subcoupon in all_coupon.Split(','))
                {
                    if (subcoupon == couponinfo.Promotion_Coupon_ID.ToString())
                    {
                        Is_Contain = true;
                        break;
                    }
                }
                if (Is_Contain == false)
                {
                    Session["all_favor_coupon"] = Session["all_favor_coupon"] + "," + couponinfo.Promotion_Coupon_ID;
                }
                else
                {
                    return;
                }
            }
            favorinfo = MyFavor.Get_Coupon_Discount(Cartinfos, coupon_code, coupon_verifycode, tools.NullInt(Session["member_id"]), "CN", 0);
            if (favorinfo != null)
            {
                if (favorinfo.Discount_Policy == couponinfo.Promotion_Coupon_ID)
                {
                    Session["order_favor_coupon"] = Session["order_favor_coupon"] + "," + couponinfo.Promotion_Coupon_ID.ToString();
                    Session["favor_coupon_price"] = tools.NullDbl(Session["favor_coupon_price"]) + favorinfo.Discount_Amount;
                }
            }
        }
        else
        {
            Response.End();
        }
    }

    //取消使用的优惠券
    public void Cart_Coupon_Cancel()
    {
        int coupon_id;

        coupon_id = tools.CheckInt(Request["coupon_id"]);
        string all_coupon, valid_coupon, coupon_arry;
        coupon_arry = "";
        all_coupon = tools.NullStr(Session["all_favor_coupon"]);
        valid_coupon = tools.NullStr(Session["order_favor_coupon"]);
        Session["all_favor_coupon"] = "0";
        Session["order_favor_coupon"] = "0";
        Session["favor_coupon_price"] = 0;
        PromotionFavorCouponInfo Favor_Coupon = null;

        if (all_coupon != "0")
        {
            foreach (string subcoupon in all_coupon.Split(','))
            {
                if (subcoupon != coupon_id.ToString())
                {
                    Favor_Coupon = MyCoupon.GetPromotionFavorCouponByID(tools.CheckInt(subcoupon), pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
                    if (Favor_Coupon != null)
                    {
                        Cart_Check_Coupon(Favor_Coupon.Promotion_Coupon_Code, Favor_Coupon.Promotion_Coupon_Verifycode);
                    }

                }
            }
        }

    }

    //获取购物车商品重量
    public double Get_Cart_Weight(int supplier_id)
    {
        double total_weight = 0;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                //统计商品重量
                if (entity.Orders_Goods_Type == 0 || entity.Orders_Goods_Type == 3)
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

    //获取购物车运费
    public double Get_Cart_Deliveryfee(int supplier_id, int delivery_id, double total_weight)
    {

        double delivery_fee = 0;
        DeliveryWayInfo delivery = GetDeliveryWayByID(delivery_id);
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
                    else
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
                }
            }
        }
        return delivery_fee;
    }

    //获取购物车商品运费
    public double Get_Cart_FreightFee(int delivery_id)
    {
        double total_weight, total_fee;
        total_fee = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }

        string supplier_id = "";

        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (supplier_id == "")
                {
                    supplier_id = entity.Orders_Goods_Product_SupplierID.ToString();
                    total_weight = Get_Cart_Weight(SupplyID);
                    total_fee += Get_Cart_Deliveryfee(SupplyID, delivery_id, total_weight);
                }
                else
                {
                    foreach (string substr in supplier_id.Split(','))
                    {
                        if (tools.CheckInt(substr) != entity.Orders_Goods_Product_SupplierID)
                        {
                            supplier_id += "," + entity.Orders_Goods_Product_SupplierID.ToString();
                            total_weight = Get_Cart_Weight(SupplyID);
                            total_fee += Get_Cart_Deliveryfee(SupplyID, delivery_id, total_weight);
                        }
                    }
                }
            }
        }
        return total_fee;
    }

    //获取购物车商品运费
    public double Get_BIDCart_FreightFee(int delivery_id)
    {
        double total_weight, total_fee;
        total_fee = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int SupplyID = tools.NullInt(Session["BIDSupplierID"]);
        int BID = tools.NullInt(Session["BID"]);
        total_weight = MyBid.Get_Cart_Weight(BID);
        total_fee = total_fee + Get_Cart_Deliveryfee(SupplyID, delivery_id, total_weight);
        return total_fee;
    }

    //获取运费优惠
    public double Favor_Fee_Check()
    {
        double favor_fee_price;
        favor_fee_price = 0;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Carts(SupplyID);
        FavorDiscountInfo Favorinfo;
        int member_grade, payway_id, delivery_id, state_id;
        member_grade = tools.NullInt(Session["member_grade"]);
        payway_id = tools.NullInt(Session["Orders_Payway_ID"]);
        delivery_id = tools.NullInt(Session["Orders_Delivery_ID"]);
        state_id = tools.NullInt(Session["Orders_Address_State"]);
        Favorinfo = MyFavor.Get_DeliveryFee_Discount(Cartinfos, member_grade, payway_id, delivery_id, state_id, tools.NullDbl(Session["delivery_fee"]), "CN");
        if (Favorinfo != null)
        {
            Session["order_favorfee"] = Favorinfo.Discount_Policy;
            if (Favorinfo.Discount_Amount == -1)
            {
                favor_fee_price = tools.NullDbl(Session["delivery_fee"]);
            }
            else
            {
                favor_fee_price = Favorinfo.Discount_Amount;
            }

        }

        return favor_fee_price;
    }

    //运费优惠价格
    public double My_Cart_favfee()
    {
        double fav_fee = tools.NullDbl(Session["favor_fee"]);
        double favor_price = 0;
        double delivery_fee = tools.NullDbl(Session["delivery_fee"]);
        if (fav_fee == -1)
        {
            favor_price = delivery_fee;
        }
        else
        {
            if (fav_fee > delivery_fee)
            {
                favor_price = delivery_fee;
            }
            else
            {
                favor_price = fav_fee;
            }
        }
        return favor_price;
    }

    //运费优惠信息显示
    public void Cart_Policyfee_Display()
    {
        int policyfee_id = tools.NullInt(Session["order_favorfee"]);
        if (policyfee_id > 0)
        {
            PromotionFavorFeeInfo entity = MyFavorFee.GetPromotionFavorFeeByID(policyfee_id, pub.CreateUserPrivilege("db71e6f9-f858-4469-b45e-b6ab55412853"));
            if (entity != null)
            {
                Response.Write("运费优惠政策：" + entity.Promotion_Fee_Title);
                Response.Write(" &nbsp; 运费优惠金额：" + pub.FormatCurrency(My_Cart_favfee()));
                Response.Write(" &nbsp; 政策有效期：" + entity.Promotion_Fee_Starttime.ToShortDateString() + " 至 " + entity.Promotion_Fee_Endtime.ToShortDateString());
                Response.Write("<input type=\"hidden\" name=\"order_favfeepolicy\" value=\"" + policyfee_id + "\">");
            }
        }
    }

    //购物车价格信息
    public void My_Cartprice(double productprice, double delivery_fee)
    {
        Session["total_price"] = tools.CheckFloat(Session["total_price"].ToString()) + productprice;
        if (delivery_fee >= 0)
        {
            Session["delivery_fee"] = delivery_fee;
        }
        Response.Write("商品金额：" + pub.FormatCurrency(tools.CheckFloat(Session["total_price"].ToString())) + "元 + 运费：" + pub.FormatCurrency(tools.CheckFloat(Session["delivery_fee"].ToString())) + "元 - 运费优惠：" + My_Cart_favfee() + "元 - 价格优惠：" + pub.FormatCurrency(tools.NullDbl(Session["favor_coupon_price"]) + tools.NullDbl(Session["favor_policy_price"])) + "元");
    }

    public void My_CartPrice()
    {
        StringBuilder strHTML = new StringBuilder();

        Session["delivery_fee"] = Get_Cart_FreightFee(tools.NullInt(Session["Orders_Delivery_ID"])); //运费

        strHTML.Append("<p><span>" + pub.FormatCurrency(tools.CheckFloat(Session["total_price"].ToString())) + "</span>商品金额：</p>");
        //strHTML.Append("<p><span>" + pub.FormatCurrency(tools.CheckFloat(Session["delivery_fee"].ToString())) + "</span>运费：</p>");

        Response.Write(strHTML.ToString());
    }

    public void My_BidCartPrice()
    {
        StringBuilder strHTML = new StringBuilder();

        Session["delivery_fee"] = Get_BIDCart_FreightFee(tools.NullInt(Session["Orders_Delivery_ID"])); //运费

        strHTML.Append("<p><span>" + pub.FormatCurrency(tools.CheckFloat(Session["total_price"].ToString())) + "</span>商品金额：</p>");
        //strHTML.Append("<p><span>" + pub.FormatCurrency(tools.CheckFloat(Session["delivery_fee"].ToString())) + "</span>运费：</p>");

        Response.Write(strHTML.ToString());
    }

    //订单总价
    public void My_Carttotalprice()
    {
        double total_price, delivery_fee, favor_feeprice, total_favprice;
        int total_coin;
        total_price = tools.CheckFloat(Session["total_price"].ToString());
        total_coin = tools.CheckInt(Session["total_coin"].ToString());

        delivery_fee = 0.00;
        //delivery_fee = tools.CheckFloat(Session["delivery_fee"].ToString());
        total_favprice = tools.CheckFloat(Session["favor_coupon_price"].ToString()) + tools.CheckFloat(Session["favor_policy_price"].ToString());
        favor_feeprice = 0;

        StringBuilder strHTML = new StringBuilder();

        total_price = total_price + delivery_fee - favor_feeprice - total_favprice;
        if (total_price < 0)
            total_price = 0;

        strHTML.Append("<p><span>¥<strong>" + Math.Round(total_price, 2).ToString("0.00") + "</strong></span>待支付：</p>");

        Response.Write(strHTML.ToString() + "<input type=\"hidden\" id=\"tmp_totalprice\" name=\"tmp_totalprice\" value=\"" + total_price + "\"><input type=\"hidden\" name=\"order_favor_couponid\" value=\"" + tools.NullStr(Session["order_favor_coupon"]) + "\">");
    }

    public void SetCartDeliverySession()
    {
        int Delivery_Way_ID = tools.CheckInt(Request["Delivery_Way_ID"]);

        if (Delivery_Way_ID > 0)
        {
            Session["Orders_Delivery_ID"] = Delivery_Way_ID;
        }
    }

    #endregion

    #region "配送支付"

    //收货地址薄
    public void Cart_Address_List_bak()
    {
        string address_list = "";
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        int addressid = tools.NullInt(Session["Orders_Address_ID"]);


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {
                if (addressid <= 0)
                {
                    i = i + 1;
                    if (i == 1)
                    {
                        Session["Orders_Address_ID"] = entity.Member_Address_ID;
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none; float:right; margin-right:20px;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a> <input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" checked=\"checked\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                    else
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none; float:right;margin-right:20px;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a> <input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                }
                else
                {
                    if (addressid == entity.Member_Address_ID)
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none; float:right;margin-right:20px;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a> <input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" checked=\"checked\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                    else
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none; float:right;margin-right:20px;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a> <input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                }
                address_list = address_list + "<strong>" + entity.Member_Address_ID + "</strong> &nbsp;" + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + " " + entity.Member_Address_StreetAddress + " </p>";
            }
        }
        else
        {
            address_list = address_list + "<p>暂无地址信息</p>";
        }
        Response.Write(address_list);
    }
    //收货地址薄
    public void Cart_Address_List()
    {
        string address_list = "";
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        int addressid = tools.NullInt(Session["Orders_Address_ID"]);
        address_list = "<strong>常用地址</strong><br />";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {
                if (addressid <= 0)
                {
                    i = i + 1;
                    if (i == 1)
                    {
                        Session["Orders_Address_ID"] = entity.Member_Address_ID;
                        //Session["Orders_Address_ID_cart"] = entity.Member_Address_ID;
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a><input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" checked=\"checked\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                    else
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a><input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                }
                else
                {
                    if (addressid == entity.Member_Address_ID)
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a><input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" checked=\"checked\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                    else
                    {
                        address_list = address_list + "<p><a href=\"javascript:;\" style=\"text-decoration:none;\" onclick=\"Del_Address('" + entity.Member_Address_ID + "');\"><span> [删除]</span></a><input name=\"address\" onclick=\"$('#div_address').load('/cart/pub_do.aspx?action=address&address_id=" + entity.Member_Address_ID + "');\" type=\"radio\" value=\"" + entity.Member_Address_ID + "\" />";
                    }
                }
                address_list = address_list + " <strong>" + entity.Member_Address_Name + "</strong>&nbsp;&nbsp;<a class=\"wn\">" + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + "</a>&nbsp;&nbsp;" + entity.Member_Address_StreetAddress + "</p>";
            }
        }
        else
        {
            address_list = address_list + "<p><a class=\"wn\">暂无地址信息</a></p>";
        }
        Response.Write(address_list);
    }


    //选择收货地址
    public void Select_Cart_Address(int address_id)
    {
        MemberAddressInfo entity = MyAddr.GetMemberAddressByID(address_id);
        if (entity == null)
        {
            address_id = tools.NullInt(Session["Orders_Address_ID"]);
            if (address_id > 0)
            {
                entity = MyAddr.GetMemberAddressByID(address_id);
            }
            if (entity == null)
            {
                int member_id = tools.CheckInt(Session["member_id"].ToString());
                if (member_id > 0)
                {
                    QueryInfo Query = new QueryInfo();
                    Query.PageSize = 1;
                    Query.CurrentPage = 1;
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
                    Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
                    IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
                    if (entitys != null)
                    {
                        entity = entitys[0];
                    }
                    else
                    {
                        entity = new MemberAddressInfo();
                    }
                }
                else
                {
                    entity = new MemberAddressInfo();
                }
            }
        }
        string HTML = "";
        HTML += "<table width=\"800\" border=\"0\" class=\"shou\">";
        HTML += "<tr>";
        HTML += "  <td width=\"98\" height=\"25\" align=\"right\"><span style=\"color:#cc0000; margin-right:5px;\">*</span>收货人姓名：</td>";
        HTML += "  <td width=\"682\" height=\"25\"><label>";
        HTML += "    <input type=\"text\" name=\"Orders_Address_Name\" value=\"" + entity.Member_Address_Name + "\" id=\"Orders_Address_Name\" />";
        HTML += "  </label><span id=\"name_tip\" style=\"color:#CC0000;\"></span></td>";
        HTML += "</tr>";
        HTML += "<tr>";
        HTML += "  <td height=\"25\" align=\"right\"><span style=\"color:#cc0000; margin-right:5px;\">*</span>省份：";
        HTML += "    <input type=\"hidden\" id=\"Member_Address_State\" name=\"Member_Address_State\" value=\"" + entity.Member_Address_State + "\"/>";
        HTML += "    <input type=\"hidden\" id=\"Member_Address_City\" name=\"Member_Address_City\" value=\"" + entity.Member_Address_City + "\"/>";
        HTML += "    <input type=\"hidden\" id=\"Member_Address_County\" name=\"Member_Address_County\" value=\"" + entity.Member_Address_County + "\"/>";
        HTML += "  </td>";
        HTML += "  <td height=\"25\">";
        HTML += "  <a id=\"div_area\">" + addr.SelectAddress("div_area", "Member_Address_State", "Member_Address_City", "Member_Address_County", entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + "</a>";
        //HTML += "  注：标*的为支持货到付款的地区，<a href=\"#\" style=\"color:#006699; text-decoration:none;\">查看货到付款的地区</a>";
        HTML += "    <span id=\"state_tip\" style=\"color:#CC0000;\"></span>";
        HTML += "  </td>";
        HTML += "</tr>";
        HTML += "<tr>";
        HTML += "  <td height=\"25\" align=\"right\"><span style=\"color:#cc0000; margin-right:5px;\">*</span>地址：</td>";
        HTML += "  <td height=\"25\"><input type=\"text\" name=\"Orders_Address_StreetAddress\" style=\"width:365px;\" id=\"Orders_Address_StreetAddress\" value=\"" + entity.Member_Address_StreetAddress + "\" /><span id=\"address_tip\" style=\"color:#CC0000;\"></span></td>";
        HTML += "</tr>";
        HTML += "<tr>";
        HTML += "<td height=\"25\" align=\"right\"><span style=\"color:#cc0000; margin-right:5px;\">*</span>手机号码：</td>";
        HTML += "<td height=\"25\"><input type=\"text\" name=\"Orders_Address_Mobile\" id=\"Orders_Address_Mobile\" onkeyup=\"if(isNaN($(this).val())){$(this).val('');}\" value=\"" + entity.Member_Address_Mobile + "\" />";
        HTML += "  或者 固定电话：";
        HTML += "  <input type=\"text\" name=\"Orders_Address_Phone_Number\" value=\"" + entity.Member_Address_Phone_Number + "\" id=\"Orders_Address_Phone_Number\" />";
        HTML += "用于接受发货通知短信及送货前确认<span id=\"mobile_tip\" style=\"color:#CC0000;\"></span></td>";
        HTML += "</tr>";
        HTML += "<tr>";
        HTML += "<td height=\"25\" align=\"right\"><span style=\"color:#cc0000; margin-right:5px;\">*</span>邮政编码：</td>";
        HTML += "<td height=\"25\"><input type=\"text\" name=\"Orders_Address_Zip\" value=\"" + entity.Member_Address_Zip + "\" onkeyup=\"if(isNaN($(this).val())){$(this).val('');}\" id=\"Orders_Address_Zip\" />";
        HTML += " 有助于快速确定送货地址<span id=\"Zip_tip\" style=\"color:#CC0000;\"></span></td>";
        HTML += "</tr>";
        HTML += "</table>";
        Response.Write(HTML);
    }

    //选择收货地址
    public void Cart_Address_Info()
    {
        //int address_id = tools.NullInt(Session["Orders_Address_ID"]);
        int address_id = tools.NullInt(Session["Orders_Address_ID_cart"]);
        MemberAddressInfo entity = MyAddr.GetMemberAddressByID(address_id);
        if (entity == null)
        {
            int member_id = tools.CheckInt(Session["member_id"].ToString());
            if (member_id > 0)
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 1;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
                Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
                IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
                if (entitys != null)
                {
                    entity = entitys[0];
                    if (entity != null)
                    {
                        Session["Orders_Address_ID"] = entity.Member_Address_ID;
                    }
                }
                else
                {
                    entity = new MemberAddressInfo();
                }
            }
            else
            {
                entity = new MemberAddressInfo();
            }
        }
        else
        {
            Session["Orders_Address_ID"] = entity.Member_Address_ID;
        }


        string address_list = "";
        Session["Orders_Address_State"] = entity.Member_Address_State;
        address_list = address_list + "<table width=\"97%\" border=\"0\" align=\"left\" cellpadding=\"5\" cellspacing=\"0\">";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td width=\"60\" height=\"5\"><span style=\"margin-right:6px;\">收</span><span style=\"margin-right:6px;\">货</span>人：</td><td>" + entity.Member_Address_Name + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td height=\"5\"><span style=\"margin-right:24px;\">地</span>区：</td><td>" + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td height=\"5\">收货地址：</td><td>" + entity.Member_Address_StreetAddress + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td height=\"5\">邮政编码：</td><td>" + entity.Member_Address_Zip + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td height=\"5\">固定电话：</td><td>" + entity.Member_Address_Phone_Number + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td height=\"5\">移动电话：</td><td>" + entity.Member_Address_Mobile + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "</table>";
        if (entity.Member_Address_Name == "" || entity.Member_Address_Name == null)
        {
            address_list = address_list + "<script>$('#guanbi_address').show();$('#xiugai_address').hide();</script>";
        }
        Response.Write(address_list);
    }

    public void Cart_Address_Infos()
    {
        StringBuilder strHTML = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        //int addressid = tools.NullInt(Session["Orders_Address_ID"]);
        int addressid = tools.NullInt(Session["Orders_Address_ID_cart"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {
                if (addressid <= 0)
                {
                    if (entity.Member_Address_IsDefault == 1)
                    {
                        strHTML.Append(entity.Member_Address_Name + " " + entity.Member_Address_Mobile + " " + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + entity.Member_Address_StreetAddress);
                    }
                }
                else
                {
                    if (addressid == entity.Member_Address_ID)
                    {
                        strHTML.Append(entity.Member_Address_Name + " " + entity.Member_Address_Mobile + " " + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + entity.Member_Address_StreetAddress);
                    }
                }
            }
        }
        else
        {
            strHTML.Append("暂无");
        }
        Response.Write(strHTML.ToString());
    }



    public void ChangeAddress()
    {
        StringBuilder strHTML = new StringBuilder();

        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int addressid = tools.NullInt(Session["Orders_Address_ID_cart"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {
                if (addressid <= 0)
                {
                    if (entity.Member_Address_IsDefault == 1)
                    {
                        strHTML.Append(entity.Member_Address_Name + " " + entity.Member_Address_Mobile + " " + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + entity.Member_Address_StreetAddress);
                    }
                }
                else
                {
                    if (addressid == entity.Member_Address_ID)
                    {
                        strHTML.Append(entity.Member_Address_Name + " " + entity.Member_Address_Mobile + " " + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + entity.Member_Address_StreetAddress);
                    }
                }
            }
        }


        //strHTML.Append("AAAAAAAAAAAAAAAAA");
        Response.Write(strHTML.ToString());
    }

    public void SetDefaultAddress()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int address_id = tools.CheckInt(Request["address_id"]);

        if (address_id <= 0)
        {
            Response.Write("False");
            Response.End();
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {
                if (address_id == entity.Member_Address_ID)
                {
                    entity.Member_Address_IsDefault = 1;
                }
                else
                {
                    entity.Member_Address_IsDefault = 0;
                }
                MyAddr.EditMemberAddress(entity);
            }
        }
        Response.Write("True");
        Response.End();
    }

    public void Delivery_Payway_Info()
    {
        string deliveryname = "";
        string deliveryfee = "";
        //DeliveryWayInfo DeliveryInfo = GetDeliveryWayByID(tools.NullInt(Session["Orders_Delivery_ID"]));
        //if (DeliveryInfo != null)
        //{
        //    deliveryname = DeliveryInfo.Delivery_Way_Name;
        //    if (DeliveryInfo.Delivery_Way_FeeType == 0)
        //    {
        //        deliveryfee = DeliveryInfo.Delivery_Way_Fee + "元";
        //    }
        //    else
        //    {
        //        deliveryfee = "首重费用：" + pub.FormatCurrency(DeliveryInfo.Delivery_Way_InitialFee) + " 续重费用：" + pub.FormatCurrency(DeliveryInfo.Delivery_Way_UpFee);
        //    }
        //}
        string payname = "";
        PayWayInfo PayInfo = GetPayWayByID(tools.NullInt(Session["Orders_Payway_ID"]));
        if (PayInfo != null)
        {
            payname = PayInfo.Pay_Way_Name;
        }
        string paytypename = "";
        PayTypeInfo PaytypeInfo = GetPayTypeByID(tools.NullInt(Session["Orders_Paytype_ID"]));
        if (PaytypeInfo != null)
        {
            paytypename = PaytypeInfo.Pay_Type_Name;
        }
        string time = "";
        //DeliveryTimeInfo TimeInfo = GetDeliveryTimeByID(tools.NullInt(Session["Orders_DeliveryTime_ID"]));
        //if (TimeInfo != null)
        //{
        //    time = TimeInfo.Delivery_Time_Name;
        //}
        string address_list = "";
        address_list = address_list + "<table width=\"97%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\">";
        //address_list = address_list + "<tr>";
        //address_list = address_list + "<td width=\"60\" height=\"5\">配送方式：</td><td>" + deliveryname + "</td>";
        //address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td width=\"60\" height=\"0\">支付方式：</td><td>" + payname + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td width=\"60\" height=\"0\">支付条件：</td><td>" + paytypename + "</td>";
        address_list = address_list + "</tr>";
        //address_list = address_list + "<tr>";
        //address_list = address_list + "<td height=\"0\"><span style=\"margin-right:24px;\">运</span>费：</td><td>" + deliveryfee + "</td>";
        //address_list = address_list + "</tr>";
        //address_list = address_list + "<tr>";
        //address_list = address_list + "<td height=\"5\">送货时间：</td><td>" + time + "</td>";
        //address_list = address_list + "</tr>";
        address_list = address_list + "</table>";
        Response.Write(address_list);
    }

    //获取并检查收货地址
    public SupplierAddressInfo Check_Address_Info()
    {
        string Supplier_Address_Country = "";
        string Supplier_Address_State = "";
        string Supplier_Address_City = "";
        string Supplier_Address_County = "";
        string Supplier_Address_StreetAddress = "";
        string Supplier_Address_Zip = "";
        string Supplier_Address_Name = "";
        string Supplier_Address_Phone_Countrycode = "";
        string Supplier_Address_Phone_Areacode = "";
        string Supplier_Address_Phone_Number = "";
        string Supplier_Address_Mobile = "";
        string Supplier_Address_Site = "CN";

        Supplier_Address_Country = tools.CheckStr(Request.Form["Orders_Address_Country"]);
        Supplier_Address_State = tools.CheckStr(Request.Form["Orders_Address_Province"]);
        Supplier_Address_City = tools.CheckStr(Request.Form["Orders_Address_City"]);
        Supplier_Address_County = tools.CheckStr(Request.Form["Orders_Address_District"]);
        Supplier_Address_StreetAddress = tools.CheckStr(Request.Form["Orders_Address_StreetAddress"]);
        Supplier_Address_Zip = tools.CheckStr(Request.Form["Orders_Address_Zip"]);
        Supplier_Address_Name = tools.CheckStr(Request.Form["Orders_Address_Name"]);
        Supplier_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Orders_Address_Phone_Countrycode"]);
        Supplier_Address_Phone_Areacode = "";
        Supplier_Address_Phone_Number = tools.CheckStr(Request.Form["Orders_Address_Phone_Number"]);
        Supplier_Address_Mobile = tools.CheckStr(Request.Form["Orders_Address_Mobile"]);


        SupplierAddressInfo address = new SupplierAddressInfo();
        address.Supplier_Address_ID = 0;
        address.Supplier_Address_SupplierID = 0;
        address.Supplier_Address_Country = Supplier_Address_Country;
        address.Supplier_Address_State = Supplier_Address_State;
        address.Supplier_Address_City = Supplier_Address_City;
        address.Supplier_Address_County = Supplier_Address_County;
        address.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
        address.Supplier_Address_Zip = Supplier_Address_Zip;
        address.Supplier_Address_Name = Supplier_Address_Name;
        address.Supplier_Address_Phone_Countrycode = Supplier_Address_Phone_Countrycode;
        address.Supplier_Address_Phone_Areacode = Supplier_Address_Phone_Areacode;
        address.Supplier_Address_Phone_Number = Supplier_Address_Phone_Number;
        address.Supplier_Address_Mobile = Supplier_Address_Mobile;
        address.Supplier_Address_Site = Supplier_Address_Site;

        return address;
    }

    //选择收货地址
    public void Cart_Address_firmInfo(SupplierAddressInfo entity)
    {
        string address_list = "";

        if (entity == null)
        {
            Response.Redirect("/cart/order_confirm.aspx");
        }
        Session["Orders_Address_State"] = entity.Supplier_Address_State;
        address_list = "<table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        address_list = address_list + "<tr>";

        address_list = address_list + "<td>";
        address_list = address_list + "<table width=\"97%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"0\">";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td colspan=\"2\" height=\"5\"></td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td colspan=\"2\">收 货 人：" + entity.Supplier_Address_Name + "</td>";

        address_list = address_list + "<td colspan=\"2\">地&nbsp; &nbsp;区：" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "</td>";

        address_list = address_list + "<td colspan=\"2\">收货地址：" + entity.Supplier_Address_StreetAddress + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td colspan=\"2\">邮政编码：" + entity.Supplier_Address_Zip + "</td>";

        address_list = address_list + "<td colspan=\"2\">固定电话：" + entity.Supplier_Address_Phone_Number + "</td>";

        address_list = address_list + "<td colspan=\"2\">移动电话：" + entity.Supplier_Address_Mobile + "</td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "<tr>";
        address_list = address_list + "<td colspan=\"2\" height=\"5\"></td>";
        address_list = address_list + "</tr>";
        address_list = address_list + "</table>";
        address_list = address_list + "</td>";



        address_list = address_list + "</tr>";
        address_list = address_list + "</table>";
        Response.Write(address_list);
    }

    //获取配送方式
    public DeliveryWayInfo GetDeliveryWayByID(int ID)
    {
        return MyDelivery.GetDeliveryWayByID(ID, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
    }

    //检查配送方式的合法性
    public int Check_DeliveryID(int delivery_id, string state, string city, string county)
    {
        int result_delivery = 0;
        if (delivery_id == 0)
        {
            return 0;
        }
        IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWaysByDistrict(state, city, county, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (entitys != null)
        {
            foreach (DeliveryWayInfo entity in entitys)
            {
                if (entity.Delivery_Way_Site == "CN" && entity.Delivery_Way_ID == delivery_id)
                {
                    result_delivery = entity.Delivery_Way_ID;
                }
            }
        }
        return result_delivery;
    }

    //配送方式
    public void Cart_Delivery_List()
    {
        //int address_id = tools.NullInt(Session["Orders_Address_ID"]);
        int address_id = tools.NullInt(Session["Orders_Address_ID_cart"]);
        MemberAddressInfo address = MyAddr.GetMemberAddressByID(address_id);
        if (address == null)
        {
            int member_id = tools.CheckInt(Session["member_id"].ToString());
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
            IList<MemberAddressInfo> addresslist = MyAddr.GetMemberAddresss(Query);
            if (addresslist != null)
            {
                address = addresslist[0];
            }
            else
            {
                address = new MemberAddressInfo();
            }
        }
        int delivery_id = tools.NullInt(Session["Orders_Delivery_ID"]);
        int hidden = delivery_id;
        StringBuilder delivery_list = new StringBuilder();

        delivery_list.Append("<ul>");
        IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWaysByDistrict(address.Member_Address_State, address.Member_Address_City, address.Member_Address_County, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (entitys != null)
        {
            int i = 0;
            foreach (DeliveryWayInfo entity in entitys)
            {
                if (entity.Delivery_Way_Site == "CN")
                {
                    i++;
                    if (delivery_id > 0)
                    {
                        delivery_list.Append("<li><input " + pub.CheckRadio(entity.Delivery_Way_ID.ToString(), delivery_id.ToString()) + " name=\"Orders_Delivery\" onclick=\"$('#Orders_Delivery_ID').attr('value','" + entity.Delivery_Way_ID + "');Set_DeliverySession(" + entity.Delivery_Way_ID + ");\" type=\"radio\" value=\"" + entity.Delivery_Way_ID + "\" /><span>" + entity.Delivery_Way_Name + "</span>" + tools.CleanHTML(entity.Delivery_Way_Intro) + "</li>");
                    }
                    else
                    {
                        if (i == 1)
                        {
                            hidden = entity.Delivery_Way_ID;
                            Session["Orders_Delivery_ID"] = entity.Delivery_Way_ID;
                        }
                        delivery_list.Append("<li><input " + pub.CheckRadio(entity.Delivery_Way_ID.ToString(), hidden.ToString()) + " name=\"Orders_Delivery\" onclick=\"$('#Orders_Delivery_ID').attr('value','" + entity.Delivery_Way_ID + "');Set_DeliverySession(" + entity.Delivery_Way_ID + ");\" type=\"radio\" value=\"" + entity.Delivery_Way_ID + "\" /><span>" + entity.Delivery_Way_Name + "</span>" + tools.CleanHTML(entity.Delivery_Way_Intro) + "</li>");
                    }
                }
            }
        }
        delivery_list.Append("<input type=\"hidden\" id=\"Orders_Delivery_ID\" name=\"Orders_Delivery_ID\" value=\"" + hidden.ToString() + "\"/>");
        delivery_list.Append("</ul>");

        Session["delivery_fee"] = Get_Cart_FreightFee(tools.NullInt(Session["Orders_Delivery_ID_cart"])); //运费

        Response.Write(delivery_list.ToString());
    }

    //招标拍卖获取配送方式
    public void Bid_Cart_Delivery_List()
    {
        int address_id = tools.NullInt(Session["Orders_Address_ID"]);
        MemberAddressInfo address = MyAddr.GetMemberAddressByID(address_id);
        if (address == null)
        {
            int member_id = tools.CheckInt(Session["member_id"].ToString());
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
            IList<MemberAddressInfo> addresslist = MyAddr.GetMemberAddresss(Query);
            if (addresslist != null)
            {
                address = addresslist[0];
            }
            else
            {
                address = new MemberAddressInfo();
            }
        }
        int delivery_id = tools.NullInt(Session["Orders_Delivery_ID"]);
        int hidden = delivery_id;
        StringBuilder delivery_list = new StringBuilder();

        delivery_list.Append("<ul>");
        IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWaysByDistrict(address.Member_Address_State, address.Member_Address_City, address.Member_Address_County, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (entitys != null)
        {
            int i = 0;
            foreach (DeliveryWayInfo entity in entitys)
            {
                if (entity.Delivery_Way_Site == "CN")
                {
                    i++;
                    if (delivery_id > 0)
                    {
                        delivery_list.Append("<li><input " + pub.CheckRadio(entity.Delivery_Way_ID.ToString(), delivery_id.ToString()) + " name=\"Orders_Delivery\" onclick=\"$('#Orders_Delivery_ID').attr('value','" + entity.Delivery_Way_ID + "');Set_DeliverySession2(" + entity.Delivery_Way_ID + ");\" type=\"radio\" value=\"" + entity.Delivery_Way_ID + "\" /><span>" + entity.Delivery_Way_Name + "</span>" + tools.CleanHTML(entity.Delivery_Way_Intro) + "</li>");
                    }
                    else
                    {
                        if (i == 1)
                        {
                            hidden = entity.Delivery_Way_ID;
                            Session["Orders_Delivery_ID"] = entity.Delivery_Way_ID;
                        }
                        delivery_list.Append("<li><input " + pub.CheckRadio(entity.Delivery_Way_ID.ToString(), hidden.ToString()) + " name=\"Orders_Delivery\" onclick=\"$('#Orders_Delivery_ID').attr('value','" + entity.Delivery_Way_ID + "');Set_DeliverySession2(" + entity.Delivery_Way_ID + ");\" type=\"radio\" value=\"" + entity.Delivery_Way_ID + "\" /><span>" + entity.Delivery_Way_Name + "</span>" + tools.CleanHTML(entity.Delivery_Way_Intro) + "</li>");
                    }
                }
            }
        }
        delivery_list.Append("<input type=\"hidden\" id=\"Orders_Delivery_ID\" name=\"Orders_Delivery_ID\" value=\"" + hidden.ToString() + "\"/>");
        delivery_list.Append("</ul>");

        Session["delivery_fee"] = Get_BIDCart_FreightFee(tools.NullInt(Session["Orders_Delivery_ID"])); //运费

        Response.Write(delivery_list.ToString());
    }
    //获取支付方式
    public PayWayInfo GetPayWayByID(int ID)
    {
        return MyPayway.GetPayWayByID(ID, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
    }

    //获取支付条件
    public PayTypeInfo GetPayTypeByID(int ID)
    {
        return MyPayType.GetPayTypeByID(ID, pub.CreateUserPrivilege("80924a02-c37c-409b-ac63-43d7d4340fc5"));
    }

    //获取线下付款方式
    public string GetPayWayByActive()
    {
        string html = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Type", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "ASC"));
        IList<PayWayInfo> Entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (Entitys != null)
        {
            html += "<table border=\"0\" style=\" margin-left:90px; margin-top:10px;\" cellpadding=\"0\" cellspacing=\"0\">";
            foreach (PayWayInfo entity in Entitys)
            {
                html += "<tr><td width=\"150\">";
                if (entity.Pay_Way_Img != "")
                {
                    html += "<img src=\"" + Application["Upload_Server_URL"] + entity.Pay_Way_Img + "\" />";
                }
                html += "</td><td height=\"90\" valign=\"top\">" + entity.Pay_Way_Intro + "</td></tr>";
                html += "<tr><td height=\"5px;\"></td><td></td></tr>";
            }
            html += "</table>";
        }
        return html;
    }

    //支付方式
    public void Cart_Payway_List(int delivery_cod)
    {
        double total_price = tools.CheckFloat(Session["total_price"].ToString()) + tools.CheckFloat(Session["delivery_fee"].ToString());

        int payid = 0;
        if (delivery_cod == -1)
        {
            DeliveryWayInfo deliveryinfo = GetDeliveryWayByID(tools.NullInt(Session["Orders_Delivery_ID"]));
            if (deliveryinfo != null)
            {
                delivery_cod = deliveryinfo.Delivery_Way_Cod;
            }
            payid = tools.NullInt(Session["Orders_Payway_ID"]);
        }
        int hidden = payid;
        double available_credit = 0;

        //QueryMemberLoanJsonInfo LoanJson = credit.Query_Member_Loan("M" + tools.NullStr(Session["member_id"]));
        //if (LoanJson != null)
        //{
        //    available_credit = LoanJson.Available_credit;
        //}

        StringBuilder payway_list = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (entitys != null)
        {
            payway_list.Append("<ul>");

            int i = 0;
            foreach (PayWayInfo entity in entitys)
            {
                i++;
                if (payid > 0)
                {
                    payway_list.Append("<li><input " + pub.CheckRadio(entity.Pay_Way_ID.ToString(), payid.ToString()) + " type=\"radio\" onclick=\"load_loan_product(" + entity.Pay_Way_ID + ");\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\" /><span>" + entity.Pay_Way_Name + "</span>" + tools.CleanHTML(entity.Pay_Way_Intro) + "</li>");
                }
                else
                {
                    if (i == 1)
                    {
                        hidden = entity.Pay_Way_ID;
                        Session["Orders_Payway_ID"] = entity.Pay_Way_ID;
                    }

                    payway_list.Append("<li>");
                    if (entity.Pay_Way_ID == 2)
                    {
                        payway_list.Append("<input" + pub.CheckRadio(entity.Pay_Way_ID.ToString(), hidden.ToString()) + " type=\"radio\" onclick=\"load_loan_product(" + entity.Pay_Way_ID + ");\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\"/><span>" + entity.Pay_Way_Name + "</span>  " + tools.CleanHTML(entity.Pay_Way_Intro) + "");


                        payway_list.Append("<div id=\"xindai\" style=\"display:none;\"></div>");
                    }
                    else if (entity.Pay_Way_ID == 3)
                    {
                        payway_list.Append("<input" + pub.CheckRadio(entity.Pay_Way_ID.ToString(), hidden.ToString()) + " type=\"radio\" onclick=\"load_loan_product(" + entity.Pay_Way_ID + ");\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\"/><span>" + entity.Pay_Way_Name + "</span>  " + tools.CleanHTML(entity.Pay_Way_Intro) + "");


                        payway_list.Append("<div id=\"zuhe\" style=\"display:none;\">");
                        payway_list.Append("<div class=\"li_box\">");
                        payway_list.Append("<ul>");
                        payway_list.Append("<li><input type=\"text\" name=\"apply_credit_amount\" id=\"apply_credit_amount\" value=\"0\" class=\"input01\" onkeyup=\"if(isNaN(value)){execCommand('undo')};apply_credit_check('#apply_credit_amount'," + total_price + ");\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" style=\"height:25px;\" /><span>当前可用额度（" + available_credit + "元）</span></li>");
                        payway_list.Append("</ul>");
                        payway_list.Append("</div>");

                        payway_list.Append("<div id=\"loan_lable\"></div>");

                        payway_list.Append("</div>");
                    }
                    else
                    {
                        payway_list.Append("<input" + pub.CheckRadio(entity.Pay_Way_ID.ToString(), hidden.ToString()) + " type=\"radio\" onclick=\"load_loan_product(" + entity.Pay_Way_ID + ");\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\"/><span>" + entity.Pay_Way_Name + "</span>  " + tools.CleanHTML(entity.Pay_Way_Intro) + "");
                    }

                    payway_list.Append("</li>");

                }
            }
            payway_list.Append("</ul>");
            payway_list.Append("<input type=\"hidden\" id=\"Orders_Payway_ID\" name=\"Orders_Payway_ID\" value=\"" + hidden.ToString() + "\"/>");

        }
        Response.Write(payway_list.ToString());
    }


    public QueryLoanProductJsonInfo GetQueryLoanProductByMemberID(string Uid)
    {
        QueryLoanProductJsonInfo JsonInfo = credit.Query_Loan_Product(Uid);

        return JsonInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Uid"></param>
    /// <returns></returns>
    public string GetQueryLoanProductTermList(IList<LoanProductTermInfo> termlist)
    {
        StringBuilder strHTML = new StringBuilder();

        int payid = 0;
        if (tools.NullInt(Session["Loan_Product_Term_ID"]) > 0)
        {
            payid = tools.NullInt(Session["Loan_Product_Term_ID"]);
        }
        int hidden = payid;

        if (termlist != null)
        {
            int i = 0;
            strHTML.Append("<div class=\"li_box\">");
            strHTML.Append("<ul>");
            foreach (LoanProductTermInfo termInfo in termlist)
            {
                i++;
                if (payid > 0)
                {
                    strHTML.Append("<li><input name=\"Loan_Product_Term\" id=\"Loan_Product_Term\" type=\"radio\" value=\"" + termInfo.Id + "\"  " + pub.CheckRadio(termInfo.Id, payid.ToString()) + "  onclick=\"$('#Loan_Product_Term_ID').attr('value','" + termInfo.Id + "');\" /><span>贷款期限：" + termInfo.Loan_term + pub.QueryLoanProductUnitChange(termInfo.Term_unit) + ",利率：" + termInfo.Interest_rate + "/" + pub.QueryLoanProductUnitChange(termInfo.Interest_rate_unit) + "</span></li>");
                }
                else
                {
                    if (i == 1)
                    {
                        hidden = tools.CheckInt(termInfo.Id);
                        Session["Loan_Product_Term_ID"] = termInfo.Id;
                    }

                    strHTML.Append("<li><input name=\"Loan_Product_Term\" id=\"Loan_Product_Term\" type=\"radio\" value=\"" + termInfo.Id + "\"  " + pub.CheckRadio(termInfo.Id, hidden.ToString()) + "  onclick=\"$('#Loan_Product_Term_ID').attr('value','" + termInfo.Id + "');\" /><span>贷款期限：" + termInfo.Loan_term + pub.QueryLoanProductUnitChange(termInfo.Term_unit) + ",利率：" + termInfo.Interest_rate + "/" + pub.QueryLoanProductUnitChange(termInfo.Interest_rate_unit) + "</span></li>");
                }
            }
            strHTML.Append("</ul>");
            strHTML.Append("<input type=\"hidden\" id=\"Loan_Product_Term_ID\" name=\"Loan_Product_Term_ID\" value=\"" + hidden.ToString() + "\"/>");
            strHTML.Append("</div>");

        }
        return strHTML.ToString();
    }

    public string GetQueryLoanProductMethodList(IList<LoanProductMethodInfo> methodlist)
    {
        StringBuilder strHTML = new StringBuilder();

        int payid = 0;
        if (tools.NullInt(Session["Loan_Product_Method_ID"]) > 0)
        {
            payid = tools.NullInt(Session["Loan_Product_Method_ID"]);
        }
        int hidden = payid;

        int i = 0;
        if (methodlist != null)
        {
            strHTML.Append("<div class=\"li_box\">");
            strHTML.Append("<ul>");
            foreach (LoanProductMethodInfo methodInfo in methodlist)
            {
                i++;
                if (payid > 0)
                {
                    strHTML.Append("<li><input name=\"Loan_Product_Method\" type=\"radio\" " + pub.CheckRadio(methodInfo.Id.ToString(), payid.ToString()) + " value=\"" + methodInfo.Id + "\" onclick=\"$('#Loan_Product_Method_ID').attr('value','" + methodInfo.Id + "');\" /><span>计息方式：" + methodInfo.Name + "</span></li>");
                }
                else
                {
                    if (i == 1)
                    {
                        hidden = tools.CheckInt(methodInfo.Id);
                        Session["Loan_Product_Method_ID"] = methodInfo.Id;
                    }

                    strHTML.Append("<li><input name=\"Loan_Product_Method\" type=\"radio\" " + pub.CheckRadio(methodInfo.Id, hidden.ToString()) + " value=\"" + methodInfo.Id + "\" onclick=\"$('#Loan_Product_Method_ID').attr('value','" + methodInfo.Id + "');\" /><span>计息方式：" + methodInfo.Name + "</span></li>");

                }
            }
            strHTML.Append("</ul>");
            strHTML.Append("<input type=\"hidden\" id=\"Loan_Product_Method_ID\" name=\"Loan_Product_Method_ID\" value=\"" + hidden.ToString() + "\"/>");
            strHTML.Append("</div>");
        }
        return strHTML.ToString();
    }

    public string GetQueryLoanProductInfo()
    {
        QueryLoanProductJsonInfo JsonInfo = GetQueryLoanProductByMemberID("M" + tools.NullStr(Session["member_id"]));
        IList<LoanProductFeeInfo> feelist = null;

        StringBuilder strHTML = new StringBuilder();

        int i = 0;
        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            strHTML.Append(GetQueryLoanProductTermList(JsonInfo.Loan_term_list));
            strHTML.Append(GetQueryLoanProductMethodList(JsonInfo.Repay_method_list));

            Session["agreement_no"] = JsonInfo.Agreement_no;
            Session["margin_rate"] = JsonInfo.Margin_rate;

            feelist = JsonInfo.Fee_list;
            if (feelist != null)
            {
                foreach (LoanProductFeeInfo feeinfo in feelist)
                {
                    i++;
                    if (i == 1)
                    {
                        Session["fee_rate"] = feeinfo.Fee_rate;
                        break;
                    }
                }
            }
        }
        return strHTML.ToString();
    }

    public void Cart_Payway_List_bak(int delivery_cod)
    {
        int payid = 0;
        if (delivery_cod == -1)
        {
            DeliveryWayInfo deliveryinfo = GetDeliveryWayByID(tools.NullInt(Session["Orders_Delivery_ID"]));
            if (deliveryinfo != null)
            {
                delivery_cod = deliveryinfo.Delivery_Way_Cod;
            }
            payid = tools.NullInt(Session["Orders_Payway_ID"]);
        }
        int hidden = payid;
        string payway_list = "";
        payway_list = "<table width=\"800\" border=\"0\" style=\"margin-top:5px;\" cellspacing=\"0\">";
        payway_list += "<tr><td height=\"12\" style=\"width:180px;color:#333333;font-weight:bolder;font-size:14px;\">支付方式：</td>";
        payway_list += "<td height=\"12\" class=\"ab\" style=\"color:#333333;font-size:12px;\">备注</td></tr>";
        //payway_list = payway_list + "<form name=\"frm_delivery\" method=\"post\" action=\"/cart/order_deliverytime.aspx\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        //if (delivery_cod == 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Cod", "=", "0"));
        //}
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (entitys != null)
        {
            int i = 0;
            foreach (PayWayInfo entity in entitys)
            {
                i++;
                if (payid > 0)
                {
                    payway_list = payway_list + "<tr><td width=\"180\" height=\"30\"><input " + pub.CheckRadio(entity.Pay_Way_ID.ToString(), payid.ToString()) + " type=\"radio\" onclick=\"$('#Orders_Payway_ID').attr('value','" + entity.Pay_Way_ID + "');\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\"> " + entity.Pay_Way_Name + "</td>";
                    payway_list += "<td height=\"12\" class=\"ab\">" + entity.Pay_Way_Intro + "</td>";
                }
                else
                {
                    if (i == 1)
                    {
                        hidden = entity.Pay_Way_ID;
                        Session["Orders_Payway_ID"] = entity.Pay_Way_ID;
                    }
                    payway_list = payway_list + "<tr><td width=\"180\" height=\"30\"><input " + pub.CheckRadio("1", i.ToString()) + " type=\"radio\" onclick=\"$('#Orders_Payway_ID').attr('value','" + entity.Pay_Way_ID + "');\" name=\"Orders_Payway\" value=\"" + entity.Pay_Way_ID + "\">  " + entity.Pay_Way_Name + "</td>";
                    payway_list += "<td height=\"12\" class=\"ab\">" + entity.Pay_Way_Intro + "</td>";
                }
            }
        }
        else
        {
            payway_list = payway_list + "<tr><td align=\"center\">暂无可用支付方式</tD></tr>";
        }
        payway_list = payway_list + "<tr><td><input type=\"hidden\" id=\"Orders_Payway_ID\" name=\"Orders_Payway_ID\" value=\"" + hidden.ToString() + "\"/></tD></tr>";
        payway_list = payway_list + "</table>";
        Response.Write(payway_list);
    }

    public void Load_Payway_Info()
    {
        int payid = tools.CheckInt(Request["payway_id"]);
        int hidden = payid;
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (entitys != null)
        {
            int i = 0;
            foreach (PayWayInfo entity in entitys)
            {
                i++;

                if (payid <= 0)
                {
                    if (i == 1)
                    {
                        hidden = entity.Pay_Way_ID;
                        Session["Orders_Payway_ID"] = entity.Pay_Way_ID;
                        strHTML.Append(entity.Pay_Way_Name);
                    }
                }
                else
                {
                    if (payid == entity.Pay_Way_ID)
                    {
                        strHTML.Append(entity.Pay_Way_Name);
                    }
                }
            }
        }
        Response.Write(strHTML.ToString());
    }

    //支付条件
    public void Cart_Paytype_List()
    {
        int Pay_Type_ID = 0;
        string payway_list = "";
        payway_list = "<div id=\"ti312\" >付款条件<br />";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayTypeInfo.Pay_Type_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayTypeInfo.Pay_Type_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayTypeInfo.Pay_Type_Sort", "asc"));
        IList<PayTypeInfo> entitys = MyPayType.GetPayTypes(Query, pub.CreateUserPrivilege("80924a02-c37c-409b-ac63-43d7d4340fc5"));
        if (entitys != null)
        {
            int i = 0;
            foreach (PayTypeInfo entity in entitys)
            {
                i++;
                if (i == 1)
                {
                    Session["Orders_Paytype_ID"] = entity.Pay_Type_ID;
                    payway_list = payway_list + "<p><input type=\"radio\" checked onclick=\"$('#Orders_Paytype_ID').attr('value','" + entity.Pay_Type_ID + "')\" name=\"Pay_Type_ID\" value=\"" + entity.Pay_Type_ID + "\"><label>" + entity.Pay_Type_Name + "</label><br /></p>";
                }
                else
                {
                    payway_list = payway_list + "<p><input type=\"radio\" onclick=\"$('#Orders_Paytype_ID').attr('value','" + entity.Pay_Type_ID + "')\" name=\"Pay_Type_ID\" value=\"" + entity.Pay_Type_ID + "\"><label>" + entity.Pay_Type_Name + "</label><br /></p>";
                }
            }
        }
        else
        {
            payway_list = payway_list + "<p>暂无可用支付条件</p>";
        }
        payway_list = payway_list + "<input type=\"hidden\" id=\"Orders_Paytype_ID\" name=\"Orders_Paytype_ID\" value=\"" + Pay_Type_ID.ToString() + "\"/>";
        payway_list += "</div>";
        Response.Write(payway_list);
    }

    //送货时间
    public void Cart_DeliveryTime_List()
    {
        string payway_list = "";
        int time_id = tools.NullInt(Session["Orders_DeliveryTime_ID"]);
        int hidden = time_id;
        payway_list = "<div id=\"ti312\" >送货日期<br />";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryTimeInfo.Delivery_Time_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryTimeInfo.Delivery_Time_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("DeliveryTimeInfo.Delivery_Time_Sort", "ASC"));
        IList<DeliveryTimeInfo> entitys = Mydelierytime.GetDeliveryTimes(Query);
        if (entitys != null)
        {
            int i = 0;
            foreach (DeliveryTimeInfo entity in entitys)
            {
                i++;
                if (time_id > 0)
                {
                    payway_list = payway_list + "<p><input type=\"radio\" " + pub.CheckRadio(entity.Delivery_Time_ID.ToString(), time_id.ToString()) + " onclick=\"$('#Orders_DeliveryTime_ID').attr('value','" + entity.Delivery_Time_ID + "')\" name=\"Orders_DeliveryTime\" value=\"" + entity.Delivery_Time_ID + "\"><label for=\"CODTime1\">" + entity.Delivery_Time_Name + "</label><br /></p>";
                }
                else
                {
                    if (i == 1)
                    {
                        hidden = entity.Delivery_Time_ID;
                        Session["Orders_DeliveryTime_ID"] = entity.Delivery_Time_ID;
                    }
                    payway_list = payway_list + "<p><input type=\"radio\" " + pub.CheckRadio("1", i.ToString()) + " onclick=\"$('#Orders_DeliveryTime_ID').attr('value','" + entity.Delivery_Time_ID + "')\" name=\"Orders_DeliveryTime\" value=\"" + entity.Delivery_Time_ID + "\"><label for=\"CODTime1\">" + entity.Delivery_Time_Name + "</label><br /></p>";
                }
            }
            payway_list = payway_list + "<p class=\"ab\">声明：我们会努力按照您指定的时间配送，但因天气、交通等各类因素影响，您的订单有可能会有延误现象！敬请谅解</p>";
            payway_list += "<input type=\"hidden\" id=\"Orders_DeliveryTime_ID\" name=\"Orders_DeliveryTime_ID\" value=\"" + hidden.ToString() + "\"/>";
            payway_list += "<input type=\"hidden\" id=\"U_Orders_IsNotify\" value=\"0\"/>";
            payway_list += "<input type=\"hidden\" id=\"U_Orders_PaymentMode\" value=\"1\"/>";
            payway_list += "";
            payway_list += "</div>";
        }
        Response.Write(payway_list);

    }

    //根据编号获取送货时间
    public DeliveryTimeInfo GetDeliveryTimeByID(int ID)
    {
        return Mydelierytime.GetDeliveryTimeByID(ID);
    }

    #endregion

    #region"生成订单"

    //排序
    public string[] Compositor(string[] list)
    {
        string bz = "";
        for (int i = 0; i < list.Length - 1; i++)
        {
            for (int j = i + 1; j < list.Length; j++)
            {
                if (tools.CheckInt(list[i]) > tools.CheckInt(list[j]))
                {
                    bz = list[i];
                    list[i] = list[j];
                    list[j] = bz;
                    bz = "";
                }
            }
        }
        return list;
    }

    //按供应商分订单 列表
    public string Get_CartListBySupplier()
    {
        int favor_flag = 0;
        double total_nofavor_price = 0;
        double total_mktprice = 0;
        double total_price = 0;
        int total_coin = 0;
        string html = "";
        string Supplier_List = tools.NullStr(Session["Supplier_List"]);
        if (Supplier_List == "")
        {
            Response.Redirect("/cart/my_cart.aspx");
        }
        string[] list = Supplier_List.Split(',');
        if (Supplier_List.IndexOf(",0") > 0)
        {
            list = Compositor(list);
        }
        int i = 0;
        int SupplyID = 0;
        int value = 0;
        string SupplyName = "";
        SupplierInfo supplierinfo = null;
        foreach (string str in list)
        {
            SupplyID = tools.CheckInt(str);
            if (SupplyID == 0)
            {
                SupplyName = "易耐平台";
            }
            else
            {
                supplierinfo = MySupplier.GetSupplierByID(SupplyID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierinfo != null)
                {
                    SupplyName = supplierinfo.Supplier_CompanyName;
                }
                else
                {
                    SupplyName = "第三方商家";
                }
            }

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
            if (tools.NullInt(Session["member_id"]) > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
            IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
            if (goodstmps != null)
            {
                i = i + 1;
                if (i == 1)
                {
                    value = SupplyID;
                }
                html += "<div style=\" font-weight:bold; padding-left:20px; line-height:30px; height:30px;\"><input onclick=\"$('#SupplyID').attr('value','" + SupplyID + "');\" name=\"supply\" type=\"radio\" " + pub.CheckRadio("1", i.ToString()) + " value=\"" + SupplyID + "\" />订单" + i + "&nbsp;&nbsp;由" + SupplyName + "配送</div>";
                html += "<table style=\"width:920px; margin-left:20px;\"  border=\"0\" align=\"left\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#cccccc\">";
                html += "<tr bgcolor=\"#ffffff\">";
                html += "<td height=\"27\" width=\"120\" align=\"center\" >商品编号</td>";
                html += "<td width=\"460\" align=\"center\">商品名称</td>";
                html += "<td width=\"120\" align=\"center\">商品价格</td>";
                html += "<td width=\"100\" align=\"center\">数量</td>";
                html += "<td width=\"120\" align=\"center\">合计</td>";
                html += "</tr>";
                foreach (OrdersGoodsTmpInfo entity in goodstmps)
                {

                    html += "<tr bgcolor=\"#FFFFFF\">";
                    html += "  <td height=\"27\" align=\"center\">" + entity.Orders_Goods_Product_Code + "</td>";
                    if (entity.Orders_Goods_Product_ID > 0)
                    {
                        if (entity.Orders_Goods_Type != 2)
                        {
                            //productURL = pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString());

                            html += "<td><a>" + tools.CutStr(entity.Orders_Goods_Product_Name, 60) + "</a></td>";
                        }
                        else
                        {
                            html += "<td>[套装] <a style=\"color:#333333;\">" + tools.CutStr(entity.Orders_Goods_Product_Name, 60) + "<a></td>";
                        }
                    }
                    //html += "  <td width=\"427\" >" + entity.Orders_Goods_Product_Name + "</td>";

                    html += "  <td  align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                    html += "  <td align=\"center\">" + entity.Orders_Goods_Amount + "</td>";
                    html += "  <td  align=\"center\" style=\"color:#cc0000;font-size:12px;font-weight:bolder; font-family:Verdana, Geneva, sans-serif;\">" + pub.FormatCurrency(entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + "</td>";
                    html += "</tr>";
                }
                html += "</table>";
                html += "<div class=\"clear\"></div>";
            }
        }
        html += "<form name=\"order\" id=\"order\" method=\"post\" action=\"/cart/order_confirm.aspx\">";
        html += "<div class=\"main_info03\"><a onclick=\"location = '/cart/order_confirm.aspx?SupplyID='+$('#SupplyID').attr('value')\" class=\"a_03\">去结算</a>";
        html += "<input type=\"hidden\" name=\"SupplyID\" id=\"SupplyID\" value=\"" + value + "\" /></div><div class=\"clear\"></div>";
        html += "</form>";
        return html;
    }

    public IList<OrdersGoodsTmpInfo> GetOrdersGoodsTmpList(string goods_ids, string supplier_id)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", supplier_id));

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);

        return goodstmps;
    }

    public void SetOrdersConfirmPrice()
    {
        string[] SupplyID = tools.NullStr(Session["SupplierID"]).Split(',');
        string goods_ids = tools.NullStr(Request["chk_cart_goods"]);

        double total_mktprice = 0;
        double total_allprice = 0;
        double total_price = 0;
        int total_coin = 0;

        if (SupplyID.Length > 0)
        {
            total_mktprice = 0;
            total_price = 0;
            foreach (string supplier_id in SupplyID)
            {
                IList<OrdersGoodsTmpInfo> goodstmps = GetOrdersGoodsTmpList(goods_ids, supplier_id);
                if (goodstmps != null)
                {
                    foreach (OrdersGoodsTmpInfo entity in goodstmps)
                    {
                        if (entity.Orders_Goods_ParentID == 0)
                        {
                            total_mktprice = total_mktprice + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_MKTPrice);
                            total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                            total_coin = total_coin + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);
                            total_allprice = total_allprice + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);

                            Session["total_price"] = total_allprice;        //商品总价
                            Session["total_coin"] = total_coin;         //商品积分
                        }
                    }
                }
            }
        }
    }

    //订单页购物车列表
    public string Get_Cart_List()
    {
        //Check_Cart_Valid();

        string[] SupplyID = tools.NullStr(Session["SupplierID"]).Split(',');
        string goods_ids = tools.NullStr(Request["chk_cart_goods"]);
        StringBuilder strHTML = new StringBuilder();

        SupplierInfo supplierInfo = null;
        string supplier_name = "";

        if (SupplyID.Length > 0)
        {
            strHTML.Append("<h2><a href=\"/cart/my_cart.aspx\"><<返回购物车修改商品</a>第一项、合同详情</h2>");

            foreach (string supplier_id in SupplyID)
            {
                double sum_price = 0;//计算不同商家商品总金额
                if (tools.CheckInt(supplier_id) > 0)
                {
                    supplierInfo = MySupplier.GetSupplierByID(tools.CheckInt(supplier_id), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierInfo != null)
                    {
                        supplier_name = supplierInfo.Supplier_CompanyName;
                    }
                    else
                    {
                        supplier_name = "第三方供货商";
                    }
                }
                else
                {
                    supplier_name = "易耐平台";
                }

                strHTML.Append("<div class=\"b38_main03\">");

                strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strHTML.Append("<tr>");

                strHTML.Append("<td width=\"283\">" + supplier_name + "</td>");


                strHTML.Append("<td width=\"345\">单 价</td>");
                strHTML.Append("<td width=\"174\">数 量</td>");
                strHTML.Append("<td width=\"170\">小计 （元）</td>");
                strHTML.Append("</tr>");
                strHTML.Append("</table>");

                IList<OrdersGoodsTmpInfo> goodstmps = GetOrdersGoodsTmpList(goods_ids, supplier_id);

                if (goodstmps != null)
                {
                    foreach (OrdersGoodsTmpInfo entity in goodstmps)
                    {
                        if (entity.Orders_Goods_ParentID == 0)
                        {
                            ProductInfo productInfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                            if (productInfo != null)
                            {
                                strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                                strHTML.Append("<tr>");
                                strHTML.Append("<td width=\"283\">");
                                strHTML.Append("<dl>");
                                strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                                strHTML.Append("<dd>");
                                strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "</a></p>");
                                //strHTML.Append("<p>编号：" + entity.Orders_Goods_Product_Code + "</p>");
                                strHTML.Append("" + new Product().Product_Extend_Content_New(productInfo.Product_ID) + "");
                                strHTML.Append("</dd>");
                                strHTML.Append("<div class=\"clear\"></div>");
                                strHTML.Append("</dl>");
                                strHTML.Append("</td>");

                                strHTML.Append("<td width=\"345\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>");
                                strHTML.Append("<td width=\"174\">" + entity.Orders_Goods_Amount + "</td>");
                                strHTML.Append("<td width=\"170\">" + pub.FormatCurrency(entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price) + "</td>");

                                sum_price = sum_price + entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price;

                                strHTML.Append("</tr>");
                                strHTML.Append("</table>");
                            }
                        }
                    }
                }

                strHTML.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;商品总金额：<strong>" + pub.FormatCurrency(sum_price) + "</strong></b>");
                strHTML.Append("</div>");

            }
            strHTML.Append("<input type=\"hidden\" id=\"chk_cart_goods\" name=\"chk_cart_goods\" value=\"" + goods_ids + "\">");
        }

        return strHTML.ToString();
    }

    //获取订单商品按优惠券指定类别指定品牌参与优惠价格
    public double Goods_Favor_Price(int orders_id, string cate_id, string brand_id, int policy_group, int policy_limit)
    {
        double favor_price = 0;
        string product_cate = "";
        bool Is_Match = false;
        PromotionLimitInfo limitinfo = null;
        ProductInfo productinfo = null;
        IList<OrdersGoodsInfo> goodstmps = MyOrders.GetGoodsListByOrderID(orders_id);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsInfo entity in goodstmps)
            {
                Is_Match = false;
                if ((entity.Orders_Goods_Product_IsFavor == 1) && ((entity.Orders_Goods_Type == 0) || (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0)))
                {
                    //获取商品信息以坚持是否为团购或限时商品
                    productinfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                    if (productinfo != null)
                    {
                        limitinfo = pub.GetPromotionLimitByProductID(entity.Orders_Goods_Product_ID);

                        //如果商品不是团购，或未限制团购商品
                        //如果商品不是限时，或未限制限时商品
                        if ((productinfo.Product_IsGroupBuy == 0 || (productinfo.Product_IsGroupBuy == 1 && policy_group == 0)) && (limitinfo == null || (limitinfo != null && policy_limit == 0)))
                        {
                            //获取商品总分类
                            product_cate = MyProduct.GetProductCategory(entity.Orders_Goods_Product_ID);


                            //验证分类的匹配性
                            if (product_cate != "")
                            {
                                if (cate_id != "")
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

                                //分类验证有效性
                                if (Is_Match)
                                {
                                    //验证品牌的匹配性
                                    if (brand_id != "")
                                    {
                                        Is_Match = false;

                                        foreach (string subbrand in brand_id.Split(','))
                                        {
                                            if (subbrand == entity.Orders_Goods_Product_BrandID.ToString())
                                            {
                                                Is_Match = true;
                                                break;
                                            }
                                        }
                                    }

                                    //同时匹配类别与品牌
                                    if (Is_Match)
                                    {
                                        favor_price = favor_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        return favor_price;
    }

    //检查运费优惠
    public CartInfo Orders_Favor_Fee_Check(int orders_id, string State_ID, double Delivery_Fee, int delivery_id, int payway_id)
    {
        CartInfo cart = new CartInfo();
        cart.Total_Favor_Fee = 0;
        cart.Favor_Fee_Note = "";
        double favor_fee_price;
        favor_fee_price = 0;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        Cartinfos = Get_Orders_Carts(orders_id);
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

    //检查优惠政策
    public CartInfo Orders_Favor_Policy_Check(int orders_id)
    {
        CartInfo cart = new CartInfo();
        double favor_price;
        favor_price = 0;
        string policy_note = "";
        string policy_id = "0";
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Orders_Carts(orders_id);
        IList<FavorDiscountInfo> Favorinfos;
        Favorinfos = MyFavor.Get_Policy_Discount(Cartinfos, tools.NullInt(Session["member_grade"]), "CN");
        if (Favorinfos != null)
        {
            foreach (FavorDiscountInfo Favorinfo in Favorinfos)
            {
                if (Favorinfo.Discount_Amount > 0)
                {
                    favor_price = favor_price + Favorinfo.Discount_Amount;
                    policy_note = policy_note + Favorinfo.Discount_Note;
                    policy_id = policy_id + "," + Favorinfo.Discount_Policy;
                }
            }
        }
        cart.Total_Product_Price = 0;
        cart.Total_Favor_Price = favor_price;
        cart.Favor_Price_Note = policy_note;
        cart.Favor_Policy_ID = policy_id;

        return cart;
    }

    //检查优惠券使用
    public CartInfo Orders_Coupon_Check(int orders_id, string coupon_id)
    {
        CartInfo cart = new CartInfo();
        FavorDiscountInfo favorinfo;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Orders_Carts(orders_id);

        PromotionFavorCouponInfo couponinfo = null;
        string cate_arry, brand_arry, all_coupon, coupon_note;
        double cart_total_favor_price, coupon_price;
        coupon_price = 0;
        coupon_note = "";
        all_coupon = "";


        if (coupon_id != "0")
        {
            foreach (string subcoupon in coupon_id.Split(','))
            {

                cate_arry = "";
                brand_arry = "";
                cart_total_favor_price = 0;
                if (tools.CheckInt(subcoupon) > 0)
                {
                    couponinfo = MyCoupon.GetPromotionFavorCouponByID(tools.CheckInt(subcoupon), pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));

                    if (couponinfo != null)
                    {


                        favorinfo = MyFavor.Get_Coupon_Discount(Cartinfos, couponinfo.Promotion_Coupon_Code, couponinfo.Promotion_Coupon_Verifycode, tools.NullInt(Session["member_id"]), "CN", 0);
                        if (favorinfo != null)
                        {
                            if (favorinfo.Discount_Policy == couponinfo.Promotion_Coupon_ID)
                            {
                                //记录已使用的优惠券编号
                                if (all_coupon == "")
                                {
                                    all_coupon = couponinfo.Promotion_Coupon_ID.ToString();
                                }
                                else
                                {
                                    all_coupon = all_coupon + couponinfo.Promotion_Coupon_ID.ToString();
                                }
                                coupon_price = coupon_price + favorinfo.Discount_Amount;
                                coupon_note = coupon_note + favorinfo.Discount_Note;
                            }
                        }
                    }
                }
            }
        }

        cart.Total_Favor_Price = coupon_price;
        cart.Favor_Price_Note = coupon_note;
        cart.Favor_Fee_Note = all_coupon;
        return cart;
    }

    //检查赠品优惠
    public void Orders_Gift_Check()
    {
        int favor_rank, ininum, gift_amount;
        string Product_Name, return_value;
        return_value = "";
        favor_rank = 0;
        ProductInfo productinfo;
        OrdersGoodsTmpInfo goodstmp;
        IList<OrdersGoodsTmpInfo> Cartinfos;
        int SupplyID = tools.NullInt(Session["SupplierID"]);
        Cartinfos = Get_Carts(SupplyID);
        IList<PromotionFavorGiftInfo> FavorGifts;
        FavorGifts = MyFavor.Get_Gift_Discount(Cartinfos, tools.NullInt(Session["member_grade"]), "CN");
        if (FavorGifts != null)
        {
            foreach (PromotionFavorGiftInfo entity in FavorGifts)
            {
                favor_rank = favor_rank + 1;
                ininum = 0;
                gift_amount = tools.NullInt(Request["order_gift" + favor_rank + ""]);
                if (entity.Promotion_Gift_Amounts != null)
                {
                    foreach (PromotionFavorGiftAmountInfo amount in entity.Promotion_Gift_Amounts)
                    {
                        if (amount.Promotion_Gift_Gifts != null && gift_amount == amount.Gift_Amount_ID)
                        {
                            foreach (PromotionFavorGiftGiftInfo giftinfo in amount.Promotion_Gift_Gifts)
                            {
                                productinfo = MyProduct.GetProductByID(giftinfo.Gift_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                                if (productinfo != null)
                                {
                                    if (productinfo.Product_IsNoStock == 0)
                                    {
                                        if (giftinfo.Gift_Amount <= productinfo.Product_UsableAmount)
                                        {
                                            goodstmp = new OrdersGoodsTmpInfo();
                                            goodstmp.Orders_Goods_ID = 0;
                                            goodstmp.Orders_Goods_Type = 1;
                                            goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                                            goodstmp.Orders_Goods_SessionID = Session.SessionID;
                                            goodstmp.Orders_Goods_ParentID = 0;
                                            goodstmp.Orders_Goods_Product_ID = productinfo.Product_ID;
                                            goodstmp.Orders_Goods_Product_Code = productinfo.Product_Code;
                                            goodstmp.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                                            goodstmp.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                                            goodstmp.Orders_Goods_Product_Name = productinfo.Product_Name;
                                            goodstmp.Orders_Goods_Product_Img = productinfo.Product_Img;
                                            goodstmp.Orders_Goods_Product_Price = 0;
                                            goodstmp.Orders_Goods_Product_MKTPrice = productinfo.Product_MKTPrice;
                                            goodstmp.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                                            goodstmp.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                                            goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                                            goodstmp.Orders_Goods_Product_Coin = 0;
                                            goodstmp.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                                            goodstmp.Orders_Goods_Product_UseCoin = 0;
                                            goodstmp.Orders_Goods_Amount = giftinfo.Gift_Amount;
                                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                                            goodstmp.Orders_Goods_OrdersID = 0;

                                            MyCart.AddOrdersGoodsTmp(goodstmp);
                                            goodstmp = null;
                                        }
                                    }
                                    else
                                    {
                                        goodstmp = new OrdersGoodsTmpInfo();
                                        goodstmp.Orders_Goods_ID = 0;
                                        goodstmp.Orders_Goods_Type = 1;
                                        goodstmp.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                                        goodstmp.Orders_Goods_SessionID = Session.SessionID;
                                        goodstmp.Orders_Goods_ParentID = 0;
                                        goodstmp.Orders_Goods_Product_ID = productinfo.Product_ID;
                                        goodstmp.Orders_Goods_Product_Code = productinfo.Product_Code;
                                        goodstmp.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                                        goodstmp.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                                        goodstmp.Orders_Goods_Product_Name = productinfo.Product_Name;
                                        goodstmp.Orders_Goods_Product_Img = productinfo.Product_Img;
                                        goodstmp.Orders_Goods_Product_Price = 0;
                                        goodstmp.Orders_Goods_Product_MKTPrice = productinfo.Product_MKTPrice;
                                        goodstmp.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                                        goodstmp.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                                        goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                                        goodstmp.Orders_Goods_Product_Coin = 0;
                                        goodstmp.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                                        goodstmp.Orders_Goods_Product_UseCoin = 0;
                                        goodstmp.Orders_Goods_Amount = giftinfo.Gift_Amount;
                                        goodstmp.Orders_Goods_Addtime = DateTime.Now;
                                        goodstmp.Orders_Goods_OrdersID = 0;

                                        MyCart.AddOrdersGoodsTmp(goodstmp);
                                        goodstmp = null;
                                    }
                                }
                            }
                            return_value = return_value + "<br>";
                        }
                    }
                }
            }
        }
    }

    //验证配送方式及支付方式 Ajax
    public void ValidatePay()
    {
        int order_address = tools.NullInt(Session["Orders_Address_ID"]);
        if (order_address < 1)
        {
            Response.Write("error_address");
            Response.End();
        }

        MemberAddressInfo address = MyAddr.GetMemberAddressByID(order_address);
        if (address != null)
        {
            if (address.Member_Address_MemberID != tools.CheckInt(Session["member_id"].ToString()))
            {
                Response.Write("error_address");
                Response.End();
            }
        }
        else
        {
            Response.Write("error_address");
            Response.End();
        }

        ////配送方式判断
        //int delivery_id;
        //delivery_id = tools.NullInt(Session["Orders_Delivery_ID"]);

        ////判断地区有效性
        //delivery_id = Check_DeliveryID(delivery_id, address.Supplier_Address_State, address.Supplier_Address_City, address.Supplier_Address_County);
        //if (delivery_id == 0)
        //{
        //    Response.Write("error");
        //    Response.End();
        //}

        //int delivery_cod = 0;

        //DeliveryWayInfo delivery = GetDeliveryWayByID(delivery_id);
        //if (delivery != null)
        //{
        //    delivery_cod = delivery.Delivery_Way_Cod;
        //}
        //else
        //{
        //    Response.Write("error");
        //    Response.End();
        //}

        int payway_id;
        payway_id = tools.NullInt(Session["Orders_Payway_ID"]);

        if (payway_id == 0)
        {
            Response.Write("error");
            Response.End();
        }
        PayWayInfo payway = MyPayway.GetPayWayByID(payway_id, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payway != null)
        {
            if (payway.Pay_Way_Cod == 1)
            {
                Response.Write("error");
                Response.End();
            }
        }
        else
        {
            Response.Write("error");
            Response.End();
        }
        Response.Write("success");
        Response.End();
    }

    #region 保存订单

    /// <summary>
    /// 保存订单
    /// </summary>
    public void Orders_Save()
    {
        string[] SupplyID = tools.CheckStr(Request["SupplyID"]).Split(',');
        string goods_id = tools.CheckStr(Request["chk_cart_goods"]);
        string order_sn = "";
        int Isagreement = tools.CheckInt(tools.NullStr(Request.Form["checkbox_agreement"]));//条款一

        #region 判断订单数据
        //判断是否同意条款
        if (Isagreement != 1)
        {
            pub.Msg("error", "您需要接受用户注册协议", "要完成注册，您需要接受易耐网争议处理规则", false, "{back}");
        }


        //收货地址判断
        int order_address;
        MemberAddressInfo address = null;

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            order_address = tools.NullInt(Session["Orders_Address_ID"]);
            if (order_address < 1)
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }

            address = MyAddr.GetMemberAddressByID(order_address);
            if (address != null)
            {
                if (address.Member_Address_MemberID != tools.CheckInt(Session["member_id"].ToString()))
                {
                    pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }
        }

        ////配送方式判断
        int delivery_id = 0;
        delivery_id = tools.NullInt(Request["Orders_Delivery_ID"]);

        //判断地区有效性
        delivery_id = Check_DeliveryID(delivery_id, address.Member_Address_State, address.Member_Address_City, address.Member_Address_County);
        if (delivery_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择配送方式", false, "{back}");
            Response.End();
        }

        double delivery_fee = 0;

        int delivery_cod = 0;

        DeliveryWayInfo delivery = GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            delivery_cod = delivery.Delivery_Way_Cod;
            delivery_fee = Get_Cart_FreightFee(delivery_id);
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择配送方式", false, "{back}");
            Response.End();
        }

        //支付方式
        int payway_id;
        payway_id = tools.NullInt(Request["Orders_Payway_ID"]);
        if (payway_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }

        PayWayInfo payway = MyPayway.GetPayWayByID(payway_id, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payway != null)
        {
            if (payway.Pay_Way_Status == 0)
            {
                pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                Response.End();
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }

        List<string> PayNames = new List<string>(2) { "信贷支付", "组合支付" };
        if (PayNames.Contains(payway.Pay_Way_Name))
        {
            int Loan_Product_Term_ID = tools.NullInt(Session["Loan_Product_Term_ID"]);
            if (Loan_Product_Term_ID == 0)
            {
                pub.Msg("error", "错误信息", "请选择贷款期限", false, "{back}");
                Response.End();
            }

            int Loan_Product_Method_ID = tools.NullInt(Session["Loan_Product_Method_ID"]);
            if (Loan_Product_Method_ID == 0)
            {
                pub.Msg("error", "错误信息", "请选择计息方式", false, "{back}");
                Response.End();
            }
        }

        #endregion

        #region 填充订单模型

        int account_flag;
        double account_pay = 0;
        int responsible = tools.CheckInt(Request["responsible"]);
        double Orders_Totalprice = tools.CheckFloat(Request["tmp_totalprice"]);
        string agreement_no = tools.NullStr(Session["agreement_no"]);
        double Orders_FeeRate = tools.NullDbl(Session["fee_rate"]);
        double Orders_MarginRate = tools.NullDbl(Session["margin_rate"]);
        double apply_credit_amount = tools.NullDbl(Request["apply_credit_amount"]);
        double credit_amount = 0;

        if (SupplyID.Length > 0)
        {
            foreach (string supplier_id in SupplyID)
            {
                double total_price = 0;

                //保存订单
                int orders_id = 0;
                OrdersInfo order = new OrdersInfo();
                string sn = orders_sn();
                int supplier_id1 = 0;
                order_sn += sn + ",";

                string Orders_VerifyCode = pub.Createvkey();
                order.Orders_SN = sn;
                order.Orders_Type = 1;  //现货订单
                order.Orders_BuyerType = 1;
                order.Orders_ContractID = 0;
                order.Orders_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                order.Orders_SysUserID = 0;
                order.Orders_SourceType = 1;
                order.Orders_Source = "";
                order.U_Orders_IsMonitor = 0;

                //订单状态
                order.Orders_Status = 0;
                order.Orders_ERPSyncStatus = 0;
                order.Orders_PaymentStatus = 0;
                order.Orders_DeliveryStatus = 0;
                order.Orders_DeliveryStatus_Time = DateTime.Now;
                order.Orders_Fail_Addtime = DateTime.Now;
                order.Orders_PaymentStatus_Time = DateTime.Now;
                order.Orders_InvoiceStatus = 0;
                order.Orders_Fail_SysUserID = 0;
                order.Orders_Fail_Note = "";
                order.Orders_IsReturnCoin = 0;
                order.Orders_IsSettling = 0;
                order.Orders_IsEvaluate = 0;

                //订单价格初始
                order.Orders_Total_MKTPrice = 0;
                order.Orders_Total_Price = 0;
                order.Orders_Total_Freight = 0;
                order.Orders_Total_Coin = 0;
                order.Orders_Total_UseCoin = 0;
                order.Orders_Total_AllPrice = 0;
                order.Orders_Account_Pay = account_pay;

                order.Orders_Admin_Sign = 0;
                order.Orders_Admin_Note = "";
                order.Orders_Address_ID = address.Member_Address_ID;
                order.Orders_Address_Country = address.Member_Address_Country;
                order.Orders_Address_State = address.Member_Address_State;
                order.Orders_Address_City = address.Member_Address_City;
                order.Orders_Address_County = address.Member_Address_County;
                order.Orders_Address_StreetAddress = address.Member_Address_StreetAddress;
                order.Orders_Address_Zip = address.Member_Address_Zip;
                order.Orders_Address_Name = address.Member_Address_Name;
                order.Orders_Address_Phone_Countrycode = address.Member_Address_Phone_Countrycode;
                order.Orders_Address_Phone_Areacode = address.Member_Address_Phone_Areacode;
                order.Orders_Address_Phone_Number = address.Member_Address_Phone_Number;
                order.Orders_Address_Mobile = address.Member_Address_Mobile;

                order.Orders_Note = tools.Left(tools.CheckStr(Request["order_note"]), 100);
                order.Orders_Site = "CN";
                order.Orders_Addtime = DateTime.Now;

                order.Orders_Delivery_Time_ID = 0;
                order.Orders_Delivery = 0;
                order.Orders_Delivery_Name = "";
                order.Orders_Delivery = delivery.Delivery_Way_ID;
                order.Orders_Delivery_Name = delivery.Delivery_Way_Name;

                order.Orders_Payway = payway.Pay_Way_ID;
                order.Orders_Payway_Name = payway.Pay_Way_Name;
                order.Orders_VerifyCode = Orders_VerifyCode;

                order.Orders_Total_FreightDiscount = 0;
                order.Orders_Total_FreightDiscount_Note = "";
                order.Orders_Total_PriceDiscount = 0;
                order.Orders_Total_PriceDiscount_Note = "";
                order.Orders_From = Session["customer_source"].ToString();
                order.Orders_SupplierID = tools.CheckInt(supplier_id);
                order.Orders_PurchaseID = 0;
                order.Orders_PriceReportID = 0;
                order.Orders_MemberStatus = 1;
                order.Orders_MemberStatus_Time = DateTime.Now;
                order.Orders_SupplierStatus = 0;
                order.Orders_SupplierStatus_Time = DateTime.Now;
                order.Orders_AgreementNo = agreement_no;
                order.Orders_LoanMethodID = tools.NullInt(Request["Loan_Product_Method_ID"]);
                order.Orders_LoanTermID = tools.NullInt(Request["Loan_Product_Term_ID"]);
                order.Orders_FeeRate = Orders_FeeRate;
                order.Orders_MarginRate = Orders_MarginRate;
                order.Orders_MarginFee = 0;
                order.Orders_Fee = 0;
                order.Orders_ApplyCreditAmount = 0;
                order.Orders_Responsible = responsible;
                order.Orders_IsShow = 1;

        #endregion

                if (MyOrders.AddOrders(order))
                {
                    OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(sn);
                    if (ordersinfo != null)
                    {



                        orders_id = ordersinfo.Orders_ID;
                        //添加购物车商品到订单
                        CartInfo cart = Orders_Goods_Add(orders_id, tools.CheckInt(supplier_id), goods_id);

                        if (tools.NullInt(Session["member_id"]) > 0)
                        {
                            //会员积分扣除
                            if (CheckCoinEnough(tools.CheckInt(Session["member_id"].ToString()), cart.Total_Product_UseCoin) && cart.Total_Product_UseCoin > 0)
                            {
                                Session["supplier_coinremain"] = tools.NullInt(Session["supplier_coinremain"]) - cart.Total_Product_UseCoin;
                            }
                        }

                        //检查运费优惠
                        CartInfo favorfeeinfo;
                        double favor_fee = 0;
                        string Orders_Total_FreightDiscount_Note = "";

                        string coupon_id = tools.CheckStr(Request["order_favor_couponid"]);
                        double coupon_price = 0;
                        double policy_price = 0;
                        double cart_total_favor_price = 0;
                        string Orders_Total_PriceDiscount_Note = "";
                        string favor_policy_id = "";
                        CartInfo orderfavorinfo;


                        //汇总订单价格信息
                        double total_allprice = 0;
                        total_allprice = cart.Total_Product_Price + delivery_fee - favor_fee - coupon_price - policy_price;
                        if (total_allprice < 0)
                        {
                            total_allprice = 0;
                        }
                        ordersinfo.Orders_IsShow = 1;
                        ordersinfo.Orders_Responsible = responsible;
                        ordersinfo.Orders_Total_FreightDiscount = favor_fee;
                        ordersinfo.Orders_Total_FreightDiscount_Note = Orders_Total_FreightDiscount_Note;
                        ordersinfo.Orders_Total_PriceDiscount = coupon_price + policy_price;
                        ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;
                        ordersinfo.Orders_Total_MKTPrice = cart.Total_Product_MktPrice;
                        ordersinfo.Orders_Total_Price = cart.Total_Product_Price;
                        ordersinfo.Orders_Total_Freight = delivery_fee;
                        ordersinfo.Orders_Total_Coin = cart.Total_Product_Coin;
                        ordersinfo.Orders_Total_UseCoin = cart.Total_Product_UseCoin;
                        ordersinfo.Orders_Total_AllPrice = total_allprice;
                        total_price = total_allprice - ordersinfo.Orders_Account_Pay;

                        if (ordersinfo.Orders_Payway == 2 || ordersinfo.Orders_Payway == 3)
                        {
                            if (apply_credit_amount > 0)
                            {
                                if (apply_credit_amount > total_allprice)
                                {
                                    ordersinfo.Orders_ApplyCreditAmount = total_allprice;
                                    apply_credit_amount = apply_credit_amount - total_allprice;
                                }
                                else
                                {
                                    ordersinfo.Orders_ApplyCreditAmount = apply_credit_amount;
                                    apply_credit_amount = 0;
                                }
                            }
                            else
                            {
                                ordersinfo.Orders_ApplyCreditAmount = total_allprice;
                            }
                        }


                        if (total_price == 0)
                        {
                            ordersinfo.Orders_PaymentStatus = 1;
                            ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;
                            ordersinfo.Orders_Payway_Name = "虚拟账户";
                        }


                        #region  生成订单,同事生成意向合同

                        string Contract_SN = "";
                        string Contract_Template = "";
                        string Contract_Note = "";
                        int contract_type = tools.CheckInt(Request["contract_type"]);

                        int Template_ID = 1;
                        //string Contract_Name = tools.CheckStr(Request["Contract_Name"]);
                        string Contract_Name = "易耐网电子交易平台挂牌交易电子合同";
                        //string orders_sn = tools.CheckStr(Request["orders_sn"]);

                        if (sn.Length > 0)
                        {

                            //if (ordersinfo == null)
                            //{
                            //    pub.Msg("error", "错误信息", "订单记录不存在", false, "/supplier/order_list.aspx");
                            //}
                            //else
                            //{
                            contract_type = ordersinfo.Orders_Type;
                            //}
                            if (ordersinfo.Orders_ContractID > 0)
                            {
                                pub.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "/supplier/order_list.aspx");
                            }
                        }

                        if (Contract_Name.Length == 0)
                        {
                            pub.Msg("info", "提示信息", "请填写合同名称", false, "{back}");
                        }

                        Contract_Note = tools.CheckStr(Request["Contract_Template_ContentEdit"]);



                        supplier_id1 = Convert.ToInt32(supplier_id);
                        Contract_SN = Create_TmpContract_SN();
                        ContractInfo entity = new ContractInfo();
                        entity.Contract_Type = contract_type;
                        entity.Contract_SN = Contract_SN;
                        entity.Contract_BuyerID = supplier_id1;
                        entity.Contract_AllPrice = 0;
                        entity.Contract_Template = "";


                        entity.Contract_Freight = 0;
                        entity.Contract_ServiceFee = 0;
                        entity.Contract_Delivery_ID = 0;
                        entity.Contract_Delivery_Name = "";
                        entity.Contract_Payway_ID = 0;
                        entity.Contract_Payway_Name = "";
                        entity.Contract_Site = pub.GetCurrentSite();
                        //0：意向合同 1：履行中的合同 2：交易成功的合同  3：交易失败的合同
                        entity.Contract_Status = 0;
                        //0：双方未确认 1:用户已确认2：平台未确认
                        entity.Contract_Confirm_Status = 0;
                        entity.Contract_Payment_Status = 0;
                        entity.Contract_Delivery_Status = 0;
                        entity.Contract_Note = Contract_Note;
                        entity.Contract_Addtime = DateTime.Now;
                        entity.Contract_Discount = 0;
                        entity.Contract_Source = 0;
                        entity.Contract_IsEvaluate = 0;
                        MyContract.AddContract(entity, pub.CreateUserPrivilege("010afb3b-1cbf-47f9-8455-c35fe5eceea7"));

                        #endregion


                        //更新订单价格信息
                        ContractInfo ContractEntity = MyContract.GetContractBySn(Contract_SN, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
                        if (ContractEntity != null)
                        {
                            ordersinfo.Orders_ContractID = ContractEntity.Contract_ID;
                        }

                        MyOrders.EditOrders(ordersinfo);








                        //保存优惠券信息
                        if (coupon_id != "")
                        {
                            foreach (string subcoupon in coupon_id.Split(','))
                            {
                                if (tools.CheckInt(subcoupon) > 0)
                                {
                                    Orders_Coupon_Use(orders_id, tools.CheckInt(subcoupon));
                                }
                            }
                        }

                        //派送优惠券
                        //ordersclass.Orders_SendCoupon_Action("add", tools.NullInt(Session["member_id"]), orders_id, favor_policy_id, cart_total_favor_price);

                        //保存发票信息
                        //InvoiceInfo.Invoice_OrdersID = orders_id;
                        //MyInvioce.AddOrdersInvoice(InvoiceInfo);     

                        //清空购物车信息
                        if (goods_id != "")
                        {
                            MyCart.ClearOrdersGoodsTmpByGoodsID(goods_id, ordersinfo.Orders_SupplierID);
                        }
                        else
                        {
                            MyCart.ClearOrdersGoodsTmpByOrdersID(ordersinfo.Orders_ID);
                        }

                        string mailsubject, mailbodytitle, mailbody;
                        mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                        mailbodytitle = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                        mailbody = memberclass.mail_template("order_create", "", "", ordersinfo.Orders_SN);
                        if (tools.NullInt(Session["member_id"]) > 0)
                        {
                            pub.Sendmail(tools.NullStr(Session["member_email"]), mailsubject, mailbodytitle, mailbody);
                        }

                        ordersclass.Orders_Log(orders_id, "", "添加", "成功", "订单创建");

                        //向采购商推送消息
                        messageclass.SendMessage(1, 1, tools.NullInt(Session["member_id"]), 0, "您的订单:" + sn + "已提交成功，请等待供货商确认！");

                        //向供货商推送消息
                        messageclass.SendMessage(1, 2, tools.NullInt(supplier_id), 0, "" + tools.NullStr(Session["Member_Company"]) + "提交了订单，请尽快确认，订单号:" + sn + "");

                        //短信推送
                        SMS mySMS = new SMS();
                        SupplierInfo supplierEntity = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        mySMS.Send(supplierEntity.Supplier_Mobile, supplierEntity.Supplier_CompanyName + "," + ordersinfo.Orders_SN, "supplier_news_orders_remind");

                        //短信发送
                        //SupplierInfo supplierEntity = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        //if (supplierEntity != null && supplierEntity.Supplier_Mobile.Length > 0)
                        //{
                        //    string[] content = { supplierEntity.Supplier_CompanyName, ordersinfo.Orders_SN };
                        //    new SMS().Send(supplierEntity.Supplier_Mobile, "supplier_news_orders_remind", content);
                        //}
                    }
                }
            }
        }


        Response.Redirect("/cart/shop_complete.aspx?sn=" + order_sn.TrimEnd(','));


        pub.Msg("error", "错误信息", "订单生成失败！", false, "/cart/my_cart.aspx");
        Response.End();

    }

    #endregion

    public void Bid_Orders_Save()
    {
        int SupplyID = tools.CheckInt(Request["SupplyID"]);
        string goods_id = tools.CheckStr(Request["chk_cart_goods"]);
        string order_sn = "";
        int BID = tools.CheckInt(Request["BID"]);
        //收货地址判断
        int order_address;
        MemberAddressInfo address = null;

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            order_address = tools.NullInt(Session["Orders_Address_ID"]);
            if (order_address < 1)
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }

            address = MyAddr.GetMemberAddressByID(order_address);
            if (address != null)
            {
                if (address.Member_Address_MemberID != tools.CheckInt(Session["member_id"].ToString()))
                {
                    pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }
        }

        ////配送方式判断
        int delivery_id = 0;
        delivery_id = tools.NullInt(Request["Orders_Delivery_ID"]);

        //判断地区有效性
        delivery_id = Check_DeliveryID(delivery_id, address.Member_Address_State, address.Member_Address_City, address.Member_Address_County);
        if (delivery_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择配送方式", false, "{back}");
            Response.End();
        }

        double delivery_fee = 0;

        int delivery_cod = 0;

        DeliveryWayInfo delivery = GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            delivery_cod = delivery.Delivery_Way_Cod;
            delivery_fee = Get_BIDCart_FreightFee(delivery_id);
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择配送方式", false, "{back}");
            Response.End();
        }

        //支付方式
        int payway_id;
        payway_id = tools.NullInt(Request["Orders_Payway_ID"]);
        if (payway_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }

        PayWayInfo payway = MyPayway.GetPayWayByID(payway_id, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payway != null)
        {
            if (payway.Pay_Way_Status == 0)
            {
                pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                Response.End();
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }

        List<string> PayNames = new List<string>(2) { "信贷支付", "组合支付" };
        if (PayNames.Contains(payway.Pay_Way_Name))
        {
            int Loan_Product_Term_ID = tools.NullInt(Session["Loan_Product_Term_ID"]);
            if (Loan_Product_Term_ID == 0)
            {
                pub.Msg("error", "错误信息", "请选择贷款期限", false, "{back}");
                Response.End();
            }

            int Loan_Product_Method_ID = tools.NullInt(Session["Loan_Product_Method_ID"]);
            if (Loan_Product_Method_ID == 0)
            {
                pub.Msg("error", "错误信息", "请选择计息方式", false, "{back}");
                Response.End();
            }
        }

        int account_flag;
        double account_pay = 0;
        double Orders_Totalprice = tools.CheckFloat(Request["tmp_totalprice"]);
        string agreement_no = tools.NullStr(Session["agreement_no"]);
        double Orders_FeeRate = tools.NullDbl(Session["fee_rate"]);
        double Orders_MarginRate = tools.NullDbl(Session["margin_rate"]);
        double apply_credit_amount = tools.NullDbl(Request["apply_credit_amount"]);
        double credit_amount = 0;

        if (SupplyID > 0)
        {

            double total_price = 0;

            //保存订单
            int orders_id = 0;
            OrdersInfo order = new OrdersInfo();
            string sn = orders_sn();

            order_sn += sn + ",";

            string Orders_VerifyCode = pub.Createvkey();
            order.Orders_SN = sn;
            order.Orders_Type = 1;  //现货订单
            order.Orders_BuyerType = 1;
            order.Orders_ContractID = 0;
            order.Orders_BuyerID = tools.CheckInt(Session["member_id"].ToString());
            order.Orders_SysUserID = 0;
            order.Orders_SourceType = 1;
            order.Orders_Source = "";
            order.U_Orders_IsMonitor = 0;

            //订单状态
            order.Orders_Status = 0;
            order.Orders_ERPSyncStatus = 0;
            order.Orders_PaymentStatus = 0;
            order.Orders_DeliveryStatus = 0;
            order.Orders_DeliveryStatus_Time = DateTime.Now;
            order.Orders_Fail_Addtime = DateTime.Now;
            order.Orders_PaymentStatus_Time = DateTime.Now;
            order.Orders_InvoiceStatus = 0;
            order.Orders_Fail_SysUserID = 0;
            order.Orders_Fail_Note = "";
            order.Orders_IsReturnCoin = 0;
            order.Orders_IsSettling = 0;
            order.Orders_IsEvaluate = 0;

            //订单价格初始
            order.Orders_Total_MKTPrice = 0;
            order.Orders_Total_Price = 0;
            order.Orders_Total_Freight = 0;
            order.Orders_Total_Coin = 0;
            order.Orders_Total_UseCoin = 0;
            order.Orders_Total_AllPrice = 0;
            order.Orders_Account_Pay = account_pay;

            order.Orders_Admin_Sign = 0;
            order.Orders_Admin_Note = "";
            order.Orders_Address_ID = address.Member_Address_ID;
            order.Orders_Address_Country = address.Member_Address_Country;
            order.Orders_Address_State = address.Member_Address_State;
            order.Orders_Address_City = address.Member_Address_City;
            order.Orders_Address_County = address.Member_Address_County;
            order.Orders_Address_StreetAddress = address.Member_Address_StreetAddress;
            order.Orders_Address_Zip = address.Member_Address_Zip;
            order.Orders_Address_Name = address.Member_Address_Name;
            order.Orders_Address_Phone_Countrycode = address.Member_Address_Phone_Countrycode;
            order.Orders_Address_Phone_Areacode = address.Member_Address_Phone_Areacode;
            order.Orders_Address_Phone_Number = address.Member_Address_Phone_Number;
            order.Orders_Address_Mobile = address.Member_Address_Mobile;

            order.Orders_Note = tools.Left(tools.CheckStr(Request["order_note"]), 100);
            order.Orders_Site = "CN";
            order.Orders_Addtime = DateTime.Now;

            order.Orders_Delivery_Time_ID = 0;
            order.Orders_Delivery = 0;
            order.Orders_Delivery_Name = "";
            order.Orders_Delivery = delivery.Delivery_Way_ID;
            order.Orders_Delivery_Name = delivery.Delivery_Way_Name;

            order.Orders_Payway = payway.Pay_Way_ID;
            order.Orders_Payway_Name = payway.Pay_Way_Name;
            order.Orders_VerifyCode = Orders_VerifyCode;

            order.Orders_Total_FreightDiscount = 0;
            order.Orders_Total_FreightDiscount_Note = "";
            order.Orders_Total_PriceDiscount = 0;
            order.Orders_Total_PriceDiscount_Note = "";
            order.Orders_From = "BID";
            order.Orders_SupplierID = SupplyID;
            order.Orders_PurchaseID = 0;
            order.Orders_PriceReportID = 0;
            order.Orders_MemberStatus = 1;
            order.Orders_MemberStatus_Time = DateTime.Now;
            order.Orders_SupplierStatus = 0;
            order.Orders_SupplierStatus_Time = DateTime.Now;
            order.Orders_AgreementNo = agreement_no;
            order.Orders_LoanMethodID = tools.NullInt(Request["Loan_Product_Method_ID"]);
            order.Orders_LoanTermID = tools.NullInt(Request["Loan_Product_Term_ID"]);
            order.Orders_FeeRate = Orders_FeeRate;
            order.Orders_MarginRate = Orders_MarginRate;
            order.Orders_MarginFee = 0;
            order.Orders_Fee = 0;
            order.Orders_IsShow = 1;
            order.Orders_ApplyCreditAmount = 0;

            if (MyOrders.AddOrders(order))
            {
                OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(sn);
                if (ordersinfo != null)
                {
                    orders_id = ordersinfo.Orders_ID;
                    //添加购物车商品到订单
                    CartInfo cart = BidOrders_Goods_Add(orders_id, BID);

                    if (tools.NullInt(Session["member_id"]) > 0)
                    {
                        //会员积分扣除
                        if (CheckCoinEnough(tools.CheckInt(Session["member_id"].ToString()), cart.Total_Product_UseCoin) && cart.Total_Product_UseCoin > 0)
                        {
                            Session["supplier_coinremain"] = tools.NullInt(Session["supplier_coinremain"]) - cart.Total_Product_UseCoin;
                        }
                    }

                    //检查运费优惠
                    CartInfo favorfeeinfo;
                    double favor_fee = 0;
                    string Orders_Total_FreightDiscount_Note = "";

                    string coupon_id = tools.CheckStr(Request["order_favor_couponid"]);
                    double coupon_price = 0;
                    double policy_price = 0;
                    double cart_total_favor_price = 0;
                    string Orders_Total_PriceDiscount_Note = "";
                    string favor_policy_id = "";
                    CartInfo orderfavorinfo;


                    //汇总订单价格信息
                    double total_allprice = 0;
                    total_allprice = cart.Total_Product_Price + delivery_fee - favor_fee - coupon_price - policy_price;
                    if (total_allprice < 0)
                    {
                        total_allprice = 0;
                    }

                    ordersinfo.Orders_Total_FreightDiscount = favor_fee;
                    ordersinfo.Orders_Total_FreightDiscount_Note = Orders_Total_FreightDiscount_Note;
                    ordersinfo.Orders_Total_PriceDiscount = coupon_price + policy_price;
                    ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;
                    ordersinfo.Orders_Total_MKTPrice = cart.Total_Product_MktPrice;
                    ordersinfo.Orders_Total_Price = cart.Total_Product_Price;
                    ordersinfo.Orders_Total_Freight = delivery_fee;
                    ordersinfo.Orders_Total_Coin = cart.Total_Product_Coin;
                    ordersinfo.Orders_Total_UseCoin = cart.Total_Product_UseCoin;
                    ordersinfo.Orders_Total_AllPrice = total_allprice;
                    total_price = total_allprice - ordersinfo.Orders_Account_Pay;

                    if (ordersinfo.Orders_Payway == 2 || ordersinfo.Orders_Payway == 3)
                    {
                        if (apply_credit_amount > 0)
                        {
                            if (apply_credit_amount > total_allprice)
                            {
                                ordersinfo.Orders_ApplyCreditAmount = total_allprice;
                                apply_credit_amount = apply_credit_amount - total_allprice;
                            }
                            else
                            {
                                ordersinfo.Orders_ApplyCreditAmount = apply_credit_amount;
                                apply_credit_amount = 0;
                            }
                        }
                        else
                        {
                            ordersinfo.Orders_ApplyCreditAmount = total_allprice;
                        }
                    }


                    if (total_price == 0)
                    {
                        ordersinfo.Orders_PaymentStatus = 1;
                        ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;
                        ordersinfo.Orders_Payway_Name = "虚拟账户";
                    }
                    //更新订单价格信息
                    MyOrders.EditOrders(ordersinfo);

                    //保存优惠券信息
                    if (coupon_id != "")
                    {
                        foreach (string subcoupon in coupon_id.Split(','))
                        {
                            if (tools.CheckInt(subcoupon) > 0)
                            {
                                Orders_Coupon_Use(orders_id, tools.CheckInt(subcoupon));
                            }
                        }
                    }




                    string mailsubject, mailbodytitle, mailbody;
                    mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                    mailbodytitle = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                    mailbody = memberclass.mail_template("order_create", "", "", ordersinfo.Orders_SN);
                    if (tools.NullInt(Session["member_id"]) > 0)
                    {
                        pub.Sendmail(tools.NullStr(Session["member_email"]), mailsubject, mailbodytitle, mailbody);
                    }

                    ordersclass.Orders_Log(orders_id, "", "添加", "成功", "订单创建");

                    //向采购商推送消息
                    messageclass.SendMessage(1, 1, tools.NullInt(Session["member_id"]), 0, "您的订单:" + sn + "已提交成功，请等待供货商确认！");

                    //向供货商推送消息
                    messageclass.SendMessage(1, 2, SupplyID, 0, "" + tools.NullStr(Session["Member_Company"]) + "提交了订单，请尽快确认，订单号:" + sn + "");

                }
            }
            MyBid.EditBidOrders(BID, sn);
        }


        Response.Redirect("/cart/shop_complete.aspx?sn=" + order_sn.TrimEnd(','));


        pub.Msg("error", "错误信息", "订单生成失败！", false, "{back}");
        Response.End();
    }
    //优惠券使用
    public void Orders_Coupon_Use(int Orders_ID, int Coupon_ID)
    {
        if (Orders_ID > 0 && Coupon_ID > 0)
        {
            PromotionFavorCouponInfo couponinfo = MyCoupon.GetPromotionFavorCouponByID(Coupon_ID, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
            if (couponinfo != null)
            {
                //添加订单使用优惠券记录
                MyOrders.AddOrdersCoupon(Orders_ID, Coupon_ID);

                //更新优惠券使用信息
                if (couponinfo.Promotion_Coupon_Amount == 1)
                {
                    couponinfo.Promotion_Coupon_Isused = 1;
                }
                couponinfo.Promotion_Coupon_UseAmount = couponinfo.Promotion_Coupon_UseAmount + 1;
                MyCoupon.EditPromotionFavorCoupon(couponinfo);
            }

        }
    }

    //添加订单商品
    public CartInfo Orders_Goods_Add(int orders_id)
    {
        int SupplyID = tools.CheckInt(Request["SupplyID"]);
        double total_mktprice, total_price;
        int total_coin, total_usecoin, parent_id;
        ProductInfo productinfo = null;
        SupplierCommissionCategoryInfo commissioncate = null;
        double Product_PurchasingPrice, Product_brokerage, Product_Price;

        total_mktprice = 0;
        total_price = 0;
        total_coin = 0;
        total_usecoin = 0;

        OrdersGoodsInfo ordergoods = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                total_mktprice = total_mktprice + (entity.Orders_Goods_Product_MKTPrice * entity.Orders_Goods_Amount);
                total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                total_coin = total_coin + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);
                total_usecoin = total_usecoin + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_UseCoin);

                //初始化佣金信息
                Product_PurchasingPrice = 0;
                Product_brokerage = 0;
                Product_Price = 0;

                //获取商品信息
                if (((entity.Orders_Goods_Type != 2) || (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0)))
                {
                    productinfo = productclass.GetProductByID(entity.Orders_Goods_Product_ID);
                    if (productinfo != null)
                    {
                        Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                        Product_Price = productinfo.Product_Price;

                        //采用佣金分类
                        //if (productinfo.Product_Supplier_CommissionCateID > 0)
                        //{
                        //    commissioncate = MyCommission.GetSupplierCommissionCategoryByID(productinfo.Product_Supplier_CommissionCateID, pub.CreateUserPrivilege("ed55dd89-e07e-438d-9529-a46de2cdda7b"));
                        //    if (commissioncate != null)
                        //    {
                        //        Product_brokerage = (productinfo.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                        //    }
                        //    commissioncate = null;
                        //}
                        //else
                        //{
                        //    //使用差价方式
                        //    Product_brokerage = productinfo.Product_Price - productinfo.Product_PurchasingPrice;
                        //}

                        //恢复代销商品商家ID
                        entity.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;
                    }
                }

                ordergoods = new OrdersGoodsInfo();
                ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                ordergoods.Orders_Goods_ParentID = entity.Orders_Goods_ParentID;
                ordergoods.Orders_Goods_OrdersID = orders_id;
                ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                ordergoods.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                ordergoods.Orders_Goods_Product_DeliveryDate = productinfo.U_Product_DeliveryCycle;
                ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                ordergoods.Orders_Goods_Product_brokerage = Product_brokerage;
                ordergoods.Orders_Goods_Product_SalePrice = Product_Price;
                ordergoods.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                MyOrders.AddOrdersGoods(ordergoods);

                parent_id = MyOrders.Get_Max_Goods_ID();
                if (parent_id > 0)
                {
                    Orders_SubGoods_Add(orders_id, entity.Orders_Goods_ID, parent_id);
                }
            }
        }
        CartInfo cart = new CartInfo();
        cart.Total_Product_MktPrice = tools.CheckFloat(total_mktprice.ToString("0.00"));
        cart.Total_Product_Price = tools.CheckFloat(total_price.ToString("0.00"));
        cart.Total_Product_Coin = total_coin;
        cart.Total_Product_UseCoin = total_usecoin;
        return cart;
    }

    public CartInfo Orders_Goods_Add(int orders_id, int supplier_id, string goods_id)
    {
        double total_mktprice, total_price;
        int total_coin, total_usecoin, parent_id;
        ProductInfo productinfo = null;
        SupplierCommissionCategoryInfo commissioncate = null;
        double Product_PurchasingPrice, Product_brokerage, Product_Price;

        total_mktprice = 0;
        total_price = 0;
        total_coin = 0;
        total_usecoin = 0;

        OrdersGoodsInfo ordergoods = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", supplier_id.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        if (goods_id != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_ID", "in", goods_id));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                productinfo = productclass.GetProductByID(entity.Orders_Goods_Product_ID);
                if (productinfo != null)
                {

                    total_mktprice = total_mktprice + (entity.Orders_Goods_Product_MKTPrice * entity.Orders_Goods_Amount);
                    total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                    total_coin = total_coin + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);
                    total_usecoin = total_usecoin + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_UseCoin);


                    //初始化佣金信息
                    Product_PurchasingPrice = 0;
                    Product_brokerage = 0;
                    Product_Price = 0;

                    //获取商品信息
                    if (((entity.Orders_Goods_Type != 2) || (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0)))
                    {
                        productinfo = productclass.GetProductByID(entity.Orders_Goods_Product_ID);
                        if (productinfo != null)
                        {
                            Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                            Product_Price = productinfo.Product_Price;

                            //恢复代销商品商家ID
                            entity.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;
                        }
                    }

                    ordergoods = new OrdersGoodsInfo();
                    ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                    ordergoods.Orders_Goods_ParentID = entity.Orders_Goods_ParentID;
                    ordergoods.Orders_Goods_OrdersID = orders_id;
                    ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                    ordergoods.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                    ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                    ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                    ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                    ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                    ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                    ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                    ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                    ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                    ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                    ordergoods.Orders_Goods_Product_DeliveryDate = productinfo.U_Product_DeliveryCycle;
                    ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                    ordergoods.Orders_Goods_Product_brokerage = Product_brokerage;
                    ordergoods.Orders_Goods_Product_SalePrice = Product_Price;
                    ordergoods.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                    ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                    ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                    ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                    ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                    MyOrders.AddOrdersGoods(ordergoods);

                    parent_id = MyOrders.Get_Max_Goods_ID();
                    if (parent_id > 0)
                    {
                        Orders_SubGoods_Add(orders_id, entity.Orders_Goods_ID, parent_id);
                    }
                }
            }
        }
        CartInfo cart = new CartInfo();
        cart.Total_Product_MktPrice = tools.CheckFloat(total_mktprice.ToString("0.00"));
        cart.Total_Product_Price = tools.CheckFloat(total_price.ToString("0.00"));
        cart.Total_Product_Coin = total_coin;
        cart.Total_Product_UseCoin = total_usecoin;
        return cart;
    }

    //添加招标信息订单商品
    public CartInfo BidOrders_Goods_Add(int orders_id, int BID)
    {
        double total_mktprice, total_price;
        int total_coin, total_usecoin, parent_id;
        ProductInfo productinfo = null;
        SupplierCommissionCategoryInfo commissioncate = null;
        double Product_PurchasingPrice, Product_brokerage, Product_Price;

        total_mktprice = 0;
        total_price = 0;
        total_coin = 0;
        total_usecoin = 0;

        OrdersGoodsInfo ordergoods = null;

        int Type = 0;
        DataTable datatable = MyBid.BidOrders_Goods_Add(BID, ref Type);

        if (datatable != null)
        {
            foreach (DataRow entity in datatable.Rows)
            {
                if (Type == 0)
                {
                    productinfo = productclass.GetProductByID(tools.CheckInt(entity["Tender_Product_ProductID"].ToString()));
                }
                else
                {
                    productinfo = productclass.GetProductByID(tools.CheckInt(entity["Bid_Product_ProductID"].ToString()));
                }
                if (productinfo != null)
                {
                    total_mktprice = total_mktprice + (productinfo.Product_MKTPrice * tools.CheckInt(entity["Bid_Product_Amount"].ToString()));

                    total_price = total_price + (tools.CheckInt(entity["Bid_Product_Amount"].ToString()) * tools.CheckFloat(entity["Tender_Price"].ToString()));
                    total_coin = total_coin + (Get_Member_Coin(tools.CheckFloat(entity["Tender_Price"].ToString())) * tools.CheckInt(entity["Bid_Product_Amount"].ToString()));
                    total_usecoin = 0;


                    //初始化佣金信息
                    Product_PurchasingPrice = 0;
                    Product_brokerage = 0;
                    Product_Price = 0;

                    ordergoods = new OrdersGoodsInfo();
                    ordergoods.Orders_Goods_Type = 0;
                    ordergoods.Orders_Goods_ParentID = 0;
                    ordergoods.Orders_Goods_OrdersID = orders_id;
                    ordergoods.Orders_Goods_Product_ID = productinfo.Product_ID;
                    ordergoods.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;


                    ordergoods.Orders_Goods_Product_Code = productinfo.Product_Code;
                    ordergoods.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                    ordergoods.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                    ordergoods.Orders_Goods_Product_Name = productinfo.Product_Name;
                    ordergoods.Orders_Goods_Product_Img = productinfo.Product_Img;
                    ordergoods.Orders_Goods_Product_Price = tools.CheckFloat(entity["Tender_Price"].ToString());
                    ordergoods.Orders_Goods_Product_MKTPrice = productinfo.Product_MKTPrice;
                    ordergoods.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                    ordergoods.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                    ordergoods.Orders_Goods_Product_DeliveryDate = productinfo.U_Product_DeliveryCycle;
                    ordergoods.Orders_Goods_Product_AuthorizeCode = "";
                    ordergoods.Orders_Goods_Product_brokerage = Product_brokerage;
                    ordergoods.Orders_Goods_Product_SalePrice = Product_Price;
                    ordergoods.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;

                    int Product_Coin = Get_Member_Coin(tools.CheckFloat(entity["Tender_Price"].ToString()));

                    //检查是否赠送指定积分
                    if (productinfo.Product_IsGiftCoin == 1)
                    {
                        Product_Coin = (int)(tools.CheckFloat(entity["Tender_Price"].ToString()) * productinfo.Product_Gift_Coin);
                    }

                    ordergoods.Orders_Goods_Product_Coin = Product_Coin;
                    ordergoods.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                    ordergoods.Orders_Goods_Product_UseCoin = 0;
                    ordergoods.Orders_Goods_Amount = tools.CheckInt(entity["Bid_Product_Amount"].ToString());
                    MyOrders.AddOrdersGoods(ordergoods);
                }
            }
        }

        CartInfo cart = new CartInfo();
        cart.Total_Product_MktPrice = tools.CheckFloat(total_mktprice.ToString("0.00"));
        cart.Total_Product_Price = tools.CheckFloat(total_price.ToString("0.00"));
        cart.Total_Product_Coin = total_coin;
        cart.Total_Product_UseCoin = total_usecoin;
        return cart;

    }
    //添加订单子商品
    public void Orders_SubGoods_Add(int orders_id, int parent_ID, int New_parentid)
    {

        OrdersGoodsInfo ordergoods = null;
        ProductInfo productinfo = null;
        SupplierCommissionCategoryInfo commissioncate = null;
        double Product_PurchasingPrice, Product_brokerage, Product_Price;
        int Supplier_ID = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", parent_ID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                //初始化佣金信息
                Product_PurchasingPrice = 0;
                Product_brokerage = 0;
                Product_Price = 0;

                //获取商品信息
                if (((entity.Orders_Goods_Type != 2) || (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0)))
                {
                    productinfo = productclass.GetProductByID(entity.Orders_Goods_Product_ID);
                    if (productinfo != null)
                    {
                        Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                        Product_Price = productinfo.Product_Price;

                        //采用佣金分类
                        if (productinfo.Product_Supplier_CommissionCateID > 0)
                        {
                            commissioncate = MyCommission.GetSupplierCommissionCategoryByID(productinfo.Product_Supplier_CommissionCateID, pub.CreateUserPrivilege("ed55dd89-e07e-438d-9529-a46de2cdda7b"));
                            if (commissioncate != null)
                            {
                                Product_brokerage = (productinfo.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                            }
                            commissioncate = null;
                        }
                        else
                        {
                            //使用差价方式
                            Product_brokerage = productinfo.Product_Price - productinfo.Product_PurchasingPrice;
                        }

                        //恢复代销商品商家ID
                        entity.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;
                    }
                }
                ordergoods = new OrdersGoodsInfo();
                ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                ordergoods.Orders_Goods_ParentID = New_parentid;
                ordergoods.Orders_Goods_OrdersID = orders_id;
                ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                ordergoods.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                ordergoods.Orders_Goods_Product_DeliveryDate = productinfo.U_Product_DeliveryCycle;
                ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                ordergoods.Orders_Goods_Product_brokerage = Product_brokerage;
                ordergoods.Orders_Goods_Product_SalePrice = Product_Price;
                ordergoods.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                MyOrders.AddOrdersGoods(ordergoods);

                ordergoods = null;

                Supplier_ID = entity.Orders_Goods_Product_SupplierID;
            }

            #region 更新主体商品商户ID

            OrdersGoodsInfo GoodsEntity = MyOrders.GetOrdersGoodsByID(New_parentid);
            if (GoodsEntity != null)
            {
                GoodsEntity.Orders_Goods_Product_SupplierID = Supplier_ID;
                MyOrders.EditOrdersGoods(GoodsEntity);
            }

            #endregion

        }
    }

    //保存订单保存发票信息
    public void order_save_invoicesave(int orders_id)
    {
        int ticket_type;
        string ticket_title;
        ticket_type = tools.CheckInt(Request.Form["ticket_type"]);
        ticket_title = tools.CheckStr(Request.Form["ticket_title"]);

        OrdersInvoiceInfo invoice = new OrdersInvoiceInfo();
        invoice.Invoice_ID = 0;
        invoice.Invoice_Type = ticket_type;
        invoice.Invoice_Title = ticket_title;
        invoice.Invoice_OrdersID = orders_id;

        MyInvioce.AddOrdersInvoice(invoice);
    }

    //生成订单号
    public string orders_sn()
    {
        string sn = "";
        bool ismatch = true;
        OrdersInfo ordersinfo = null;

        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            ordersinfo = MyOrders.GetOrdersBySN(sn);
            if (ordersinfo != null)
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

    //获取购物车总价
    public double Get_Cart_Price()
    {
        double total_price = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        int SupplyID = tools.CheckInt(Request["SupplyID"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", SupplyID.ToString()));
        if (tools.NullInt(Session["member_id"]) > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                total_price = total_price + (Math.Round(entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price));
            }
        }
        return total_price;
    }

    //生成采购订单
    public void BuyOrders_Save()
    {
        //申请报价验证
        int supplier_id1 = 0;
        string Goods_ID = tools.CheckStr(Request["Goods_ID"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

        ////配送方式判断
        int delivery_id = 0;
        delivery_id = tools.NullInt(Request["Orders_Delivery_ID"]);

        if (PriceReport_ID == 0 || Purchase_ID == 0)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        if (Goods_ID.Length == 0)
        {
            pub.Msg("error", "错误信息", "请选择要采购的商品信息", false, "/cart/my_buycart.aspx?PriceReport_ID=" + PriceReport_ID + "&Purchase_ID=" + Purchase_ID);
        }
        SupplierPriceReportInfo entity = supplier.SupplierPriceReportByID(PriceReport_ID);
        SupplierPurchaseInfo spinfo = null;
        int supplier_id = tools.NullInt(Session["member_id"]);
        if (entity == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        int PriceReport_MemberID = 0;
        DateTime PriceReport_DeliveryDate = DateTime.Now;
        if (entity != null)
        {
            PriceReport_MemberID = entity.PriceReport_MemberID;
            PriceReport_DeliveryDate = entity.PriceReport_DeliveryDate;
        }

        if (entity.PriceReport_AuditStatus != 1)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        spinfo = supplier.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
        if (spinfo == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //报价人类型验证
        if (entity.PriceReport_MemberID > 0 && spinfo.Purchase_TypeID == 0)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //验证采购发布人
        if (spinfo.Purchase_SupplierID != supplier_id)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //采购申请未审核通过或已删除或已过期
        if (spinfo.Purchase_Trash == 1 || spinfo.Purchase_Status != 2 || spinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //检查报价明细
        IList<SupplierPriceReportDetailInfo> reports = supplier.GetSupplierPriceReportDetailsByPriceReportID(PriceReport_ID);
        if (reports == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //选择的采购商品
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_PurchaseID", "=", Purchase_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_ID", "in", Goods_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseDetailInfo.Detail_ID", "Desc"));
        IList<SupplierPurchaseDetailInfo> goodstmps = MyPurchaseDetail.GetSupplierPurchaseDetails(Query);
        if (goodstmps == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }

        //收货地址判断
        int order_address;
        MemberAddressInfo address = null;

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            order_address = tools.NullInt(Session["Orders_Address_ID"]);
            if (order_address < 1)
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }

            address = MyAddr.GetMemberAddressByID(order_address);
            if (address != null)
            {
                if (address.Member_Address_MemberID != tools.CheckInt(Session["member_id"].ToString()))
                {
                    pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                    Response.End();
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "请选择收货地址", false, "{back}");
                Response.End();
            }
        }

        double delivery_fee = 0;

        //支付方式
        int payway_id;
        payway_id = tools.NullInt(Session["Orders_Payway_ID"]);

        if (payway_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }
        PayWayInfo payway = MyPayway.GetPayWayByID(payway_id, pub.CreateUserPrivilege("4484c144-8777-4852-a352-4a89ac5df06f"));
        if (payway != null)
        {
            if (payway.Pay_Way_Status == 0)
            {
                pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
                Response.End();
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择支付方式", false, "{back}");
            Response.End();
        }

        //支付条件
        int paytype_id;
        paytype_id = tools.NullInt(Session["Orders_Paytype_ID"]);

        if (paytype_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择支付条件", false, "{back}");
            Response.End();
        }
        PayTypeInfo paytype = GetPayTypeByID(paytype_id);
        if (paytype != null)
        {
            if (paytype.Pay_Type_IsActive == 0)
            {
                pub.Msg("error", "错误信息", "请选择支付条件", false, "{back}");
                Response.End();
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请选择支付条件", false, "{back}");
            Response.End();
        }


        int account_flag;
        double account_pay = 0;
        double Orders_Totalprice = tools.CheckFloat(Request["tmp_totalprice"]);


        double total_price = 0;

        //保存订单
        int orders_id = 0;
        OrdersInfo order = new OrdersInfo();
        string sn = orders_sn();
        string Orders_VerifyCode = pub.Createvkey();
        order.Orders_SN = sn;
        if (spinfo.Purchase_TypeID == 0)
        {
            order.Orders_Type = 2;  //定制采购订单
        }
        else
        {
            order.Orders_Type = 3;  //代理采购订单
        }
        order.Orders_BuyerType = 1;
        order.Orders_ContractID = 0;
        order.Orders_BuyerID = tools.CheckInt(Session["supplier_id"].ToString());
        order.Orders_SysUserID = 0;
        order.Orders_SourceType = 1;
        order.Orders_Source = "";
        order.U_Orders_IsMonitor = 0;

        //订单状态
        order.Orders_Status = 0;
        order.Orders_ERPSyncStatus = 0;
        order.Orders_PaymentStatus = 0;
        order.Orders_DeliveryStatus = 0;
        order.Orders_DeliveryStatus_Time = DateTime.Now;
        order.Orders_Fail_Addtime = DateTime.Now;
        order.Orders_PaymentStatus_Time = DateTime.Now;
        order.Orders_InvoiceStatus = 0;
        order.Orders_Fail_SysUserID = 0;
        order.Orders_Fail_Note = "";
        order.Orders_IsReturnCoin = 0;
        order.Orders_IsSettling = 0;
        order.Orders_IsEvaluate = 0;

        //订单价格初始
        order.Orders_Total_MKTPrice = 0;
        order.Orders_Total_Price = 0;
        order.Orders_Total_Freight = 0;
        order.Orders_Total_Coin = 0;
        order.Orders_Total_UseCoin = 0;
        order.Orders_Total_AllPrice = 0;
        order.Orders_Account_Pay = account_pay;

        order.Orders_Admin_Sign = 0;
        order.Orders_Admin_Note = "";
        order.Orders_Address_ID = address.Member_Address_ID;
        order.Orders_Address_Country = address.Member_Address_Country;
        order.Orders_Address_State = address.Member_Address_State;
        order.Orders_Address_City = address.Member_Address_City;
        order.Orders_Address_County = address.Member_Address_County;
        order.Orders_Address_StreetAddress = address.Member_Address_StreetAddress;
        order.Orders_Address_Zip = address.Member_Address_Zip;
        order.Orders_Address_Name = address.Member_Address_Name;
        order.Orders_Address_Phone_Countrycode = address.Member_Address_Phone_Countrycode;
        order.Orders_Address_Phone_Areacode = address.Member_Address_Phone_Areacode;
        order.Orders_Address_Phone_Number = address.Member_Address_Phone_Number;
        order.Orders_Address_Mobile = address.Member_Address_Mobile;

        order.Orders_Note = tools.Left(tools.CheckStr(Request["order_note"]), 100);
        order.Orders_Site = "CN";
        order.Orders_Addtime = DateTime.Now;

        order.Orders_Delivery_Time_ID = 0;
        order.Orders_Delivery = 0;
        order.Orders_Delivery_Name = "";

        order.Orders_Payway = payway.Pay_Way_ID;
        order.Orders_Payway_Name = payway.Pay_Way_Name;
        order.Orders_PayType = paytype.Pay_Type_ID;
        order.Orders_PayType_Name = paytype.Pay_Type_Name;
        order.Orders_VerifyCode = Orders_VerifyCode;

        order.Orders_Total_FreightDiscount = 0;
        order.Orders_Total_FreightDiscount_Note = "";
        order.Orders_Total_PriceDiscount = 0;
        order.Orders_Total_PriceDiscount_Note = "";
        order.Orders_From = Session["customer_source"].ToString();
        order.Orders_SupplierID = entity.PriceReport_MemberID;
        order.Orders_PurchaseID = spinfo.Purchase_ID;
        order.Orders_PriceReportID = entity.PriceReport_ID;
        if (MyOrders.AddOrders(order))
        {
            OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(sn);
            if (ordersinfo != null)
            {
                orders_id = ordersinfo.Orders_ID;
                //添加采购申请商品到订单
                CartInfo cart = BuyOrders_Goods_Add(orders_id, PriceReport_MemberID, PriceReport_DeliveryDate.ToString("yyyy-MM-dd"), goodstmps, reports);

                //检查运费优惠
                CartInfo favorfeeinfo;
                double favor_fee = 0;
                string Orders_Total_FreightDiscount_Note = "";

                string coupon_id = tools.CheckStr(Request["order_favor_couponid"]);
                double coupon_price = 0;
                double policy_price = 0;
                double cart_total_favor_price = 0;
                string Orders_Total_PriceDiscount_Note = "";
                string favor_policy_id = "";
                CartInfo orderfavorinfo;

                //检查优惠券
                orderfavorinfo = null;

                //汇总订单价格信息
                double total_allprice = 0;
                total_allprice = cart.Total_Product_Price + delivery_fee - favor_fee - coupon_price - policy_price;
                if (total_allprice < 0)
                {
                    total_allprice = 0;
                }

                ordersinfo.Orders_Total_FreightDiscount = favor_fee;
                ordersinfo.Orders_Total_FreightDiscount_Note = Orders_Total_FreightDiscount_Note;
                ordersinfo.Orders_Total_PriceDiscount = coupon_price + policy_price;
                ordersinfo.Orders_Total_PriceDiscount_Note = Orders_Total_PriceDiscount_Note;
                ordersinfo.Orders_Total_MKTPrice = cart.Total_Product_MktPrice;
                ordersinfo.Orders_Total_Price = cart.Total_Product_Price;
                ordersinfo.Orders_Total_Freight = delivery_fee;
                ordersinfo.Orders_Total_Coin = cart.Total_Product_Coin;
                ordersinfo.Orders_Total_UseCoin = cart.Total_Product_UseCoin;
                ordersinfo.Orders_Total_AllPrice = total_allprice;
                total_price = total_allprice - ordersinfo.Orders_Account_Pay;
                if (total_price == 0)
                {
                    ordersinfo.Orders_PaymentStatus = 1;
                    ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;
                    ordersinfo.Orders_Payway_Name = "虚拟账户";
                }



                #region  生成订单,同事生成意向合同

                string Contract_SN = "";
                string Contract_Template = "";
                string Contract_Note = "";
                int contract_type = tools.CheckInt(Request["contract_type"]);

                int Template_ID = 1;
                //string Contract_Name = tools.CheckStr(Request["Contract_Name"]);
                string Contract_Name = "易耐网电子交易平台挂牌交易电子合同";
                //string orders_sn = tools.CheckStr(Request["orders_sn"]);

                if (sn.Length > 0)
                {

                    //if (ordersinfo == null)
                    //{
                    //    pub.Msg("error", "错误信息", "订单记录不存在", false, "/supplier/order_list.aspx");
                    //}
                    //else
                    //{
                    contract_type = ordersinfo.Orders_Type;
                    //}
                    if (ordersinfo.Orders_ContractID > 0)
                    {
                        pub.Msg("error", "错误信息", "该订单已经附加在其他合同中！", false, "/supplier/order_list.aspx");
                    }
                }

                if (Contract_Name.Length == 0)
                {
                    pub.Msg("info", "提示信息", "请填写合同名称", false, "{back}");
                }

                Contract_Note = tools.CheckStr(Request["Contract_Template_ContentEdit"]);



                supplier_id1 = Convert.ToInt32(supplier_id);
                Contract_SN = Create_TmpContract_SN();
                ContractInfo entity1 = new ContractInfo();
                entity1.Contract_Type = contract_type;
                entity1.Contract_SN = Contract_SN;
                entity1.Contract_BuyerID = supplier_id1;
                entity1.Contract_AllPrice = 0;
                entity1.Contract_Template = "";

                entity1.Contract_Freight = 0;
                entity1.Contract_ServiceFee = 0;
                entity1.Contract_Delivery_ID = 0;
                entity1.Contract_Delivery_Name = "";
                entity1.Contract_Payway_ID = 0;
                entity1.Contract_Payway_Name = "";
                entity1.Contract_Site = pub.GetCurrentSite();
                //0：意向合同 1：履行中的合同 2：交易成功的合同  3：交易失败的合同
                entity1.Contract_Status = 0;
                //0：双方未确认 1:用户已确认2：平台未确认
                entity1.Contract_Confirm_Status = 0;
                entity1.Contract_Payment_Status = 0;
                entity1.Contract_Delivery_Status = 0;
                entity1.Contract_Note = Contract_Note;
                entity1.Contract_Addtime = DateTime.Now;
                entity1.Contract_Discount = 0;
                entity1.Contract_Source = 0;
                entity1.Contract_IsEvaluate = 0;
                MyContract.AddContract(entity1, pub.CreateUserPrivilege("010afb3b-1cbf-47f9-8455-c35fe5eceea7"));

                #endregion



                //更新订单价格信息
                MyOrders.EditOrders(ordersinfo);

                //保存优惠券信息
                if (coupon_id != "")
                {
                    foreach (string subcoupon in coupon_id.Split(','))
                    {
                        if (tools.CheckInt(subcoupon) > 0)
                        {
                            Orders_Coupon_Use(orders_id, tools.CheckInt(subcoupon));
                        }
                    }
                }

                //清空购物车信息 
                int SupplyID = tools.NullInt(Session["SupplierID"]);
                MyCart.ClearOrdersGoodsTmp(Session.SessionID, SupplyID);

                string mailsubject, mailbodytitle, mailbody;
                mailsubject = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                mailbodytitle = "您在" + tools.NullStr(Application["site_name"]) + "上的订单信息";
                mailbody = memberclass.mail_template("order_create", "", "", ordersinfo.Orders_SN);
                if (tools.NullInt(Session["member_id"]) > 0)
                {
                    pub.Sendmail(tools.NullStr(Session["member_email"]), mailsubject, mailbodytitle, mailbody);
                }

                ordersclass.Orders_Log(orders_id, "", "添加", "成功", "订单创建");

                Response.Redirect("/cart/shop_complete.aspx?sn=" + sn);
            }
        }
        pub.Msg("error", "错误信息", "订单生成失败！", false, "/cart/my_cart.aspx");
        Response.End();

    }

    //添加订单商品
    public CartInfo BuyOrders_Goods_Add(int orders_id, int Supplier_ID, string Delivery_Time, IList<SupplierPurchaseDetailInfo> goodstmps, IList<SupplierPriceReportDetailInfo> reports)
    {
        int SupplyID = tools.CheckInt(Request["SupplyID"]);
        double total_mktprice, total_price;
        int total_coin, total_usecoin;
        int product_coin;
        total_mktprice = 0;
        total_price = 0;
        total_coin = 0;
        total_usecoin = 0;

        OrdersGoodsInfo ordergoods = null;

        if (goodstmps != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in goodstmps)
            {
                if (reports != null)
                {
                    foreach (SupplierPriceReportDetailInfo report in reports)
                    {
                        if (report.Detail_PurchaseDetailID == entity.Detail_ID)
                        {
                            product_coin = pub.Get_Member_Coin((report.Detail_Price));

                            total_mktprice = total_mktprice + (report.Detail_Amount * report.Detail_Price);
                            total_price = total_price + (report.Detail_Amount * report.Detail_Price);
                            total_coin += (product_coin * report.Detail_Amount);
                            total_usecoin = 0;



                            ordergoods = new OrdersGoodsInfo();
                            ordergoods.Orders_Goods_Type = 0;
                            ordergoods.Orders_Goods_ParentID = 0;
                            ordergoods.Orders_Goods_OrdersID = orders_id;
                            ordergoods.Orders_Goods_Product_ID = 0;
                            ordergoods.Orders_Goods_Product_SupplierID = Supplier_ID;
                            ordergoods.Orders_Goods_Product_Code = "";
                            ordergoods.Orders_Goods_Product_CateID = 0;
                            ordergoods.Orders_Goods_Product_BrandID = 0;
                            ordergoods.Orders_Goods_Product_Name = entity.Detail_Name;
                            ordergoods.Orders_Goods_Product_Img = "";
                            ordergoods.Orders_Goods_Product_Price = report.Detail_Price;
                            ordergoods.Orders_Goods_Product_MKTPrice = report.Detail_Price;
                            ordergoods.Orders_Goods_Product_Maker = "";
                            ordergoods.Orders_Goods_Product_Spec = entity.Detail_Spec;
                            ordergoods.Orders_Goods_Product_DeliveryDate = Delivery_Time;
                            ordergoods.Orders_Goods_Product_AuthorizeCode = "";
                            ordergoods.Orders_Goods_Product_brokerage = 0;
                            ordergoods.Orders_Goods_Product_SalePrice = 0;
                            ordergoods.Orders_Goods_Product_PurchasingPrice = 0;
                            ordergoods.Orders_Goods_Product_Coin = product_coin;
                            ordergoods.Orders_Goods_Product_IsFavor = 0;
                            ordergoods.Orders_Goods_Product_UseCoin = 0;
                            ordergoods.Orders_Goods_Amount = report.Detail_Amount;
                            MyOrders.AddOrdersGoods(ordergoods);
                            break;
                        }
                    }
                }


            }
        }
        CartInfo cart = new CartInfo();
        cart.Total_Product_MktPrice = tools.CheckFloat(total_mktprice.ToString("0.00"));
        cart.Total_Product_Price = tools.CheckFloat(total_price.ToString("0.00"));
        cart.Total_Product_Coin = total_coin;
        cart.Total_Product_UseCoin = total_usecoin;
        return cart;
    }


    //生成意向合同编号
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
}
