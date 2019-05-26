using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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

using System.Text.RegularExpressions;


using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.Util.SQLHelper;

/// <summary>
/// 订单处理类
/// </summary>
public class OrdersProcess
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IOrders MyBLL;
    private IOrdersGoodsTmp Mygoodstmp;
    private IDeliveryTime MyDeliverytime;
    private OrdersLog orderlog = new OrdersLog();
    private DeliveryWay deliveryway = new DeliveryWay();
    private IPayWay payway;
    private IProduct MyProduct;
    private IPackage MyPackage;
    private IOrdersPayment Mypayment;
    private IOrdersDelivery Mydelivery;
    private IOrdersInvoice MyInvoice;
    private ISQLHelper DBHelper;
    private Addr Addr;
    private IMember MyMEM;
    private IMemberAddress MyAddr;
    private ISupplier MySupplier;
    private ISupplierCommissionCategory MyCommission;
    private IPayType MyPayType;
    private ISupplierPriceReportDetail MyReportDetail;
    private ISupplierPurchaseDetail MyPurchaseDetail;
    Member member = new Member();
    ProductPrice productprice = new ProductPrice();
    Product product=new Product();
    Supplier supplier = new Supplier();
    public OrdersProcess()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = OrdersFactory.CreateOrders();
        MyDeliverytime = DeliveryTimeFactory.CreateDeliveryTime();
        payway = PayWayFactory.CreatePayWay();
        MyProduct = ProductFactory.CreateProduct();
        MyPackage = PackageFactory.CreatePackage();
        Mypayment = OrdersPaymentFactory.CreateOrdersPayment();
        Mydelivery = OrdersDeliveryFactory.CreateOrdersDelivery();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        MyInvoice = OrdersInvoiceFactory.CreateOrdersInvoice();
        Mygoodstmp = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        MyAddr = MemberAddressFactory.CreateMemberAddress();
        MyMEM = MemberFactory.CreateMember();
        MySupplier = SupplierFactory.CreateSupplier();
        MyCommission = SupplierCommissionCategoryFactory.CreateSupplierCommissionCategory();
        MyReportDetail=SupplierPriceReportDetailFactory.CreateSupplierPriceReportDetail();
        MyPurchaseDetail=SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
        MyPayType = PayTypeFactory.CreatePayType();
        Addr = new Addr();
    }

    #region "订单修改"

    //获取捆绑产品库存
    public ProductStockInfo Get_Package_Stock(IList<PackageProductInfo> packageproducts)
    {
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        int Package_Stock = 0;
        bool IsNoStock = true;
        int cur_stock = 0;
        bool nostock = false;
        string Package_Product_Arry = "0";
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> products = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
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
                            cur_stock = (int)(product.Product_UsableAmount / packproduct.Package_Product_Amount);
                        }
                    }
                }
                if (cur_stock <= 0 && Package_Stock > 0)
                {
                    nostock = true;
                }
                if (Package_Stock > cur_stock || Package_Stock == 0)
                {
                    Package_Stock = cur_stock;
                }
            }
            if (nostock)
            {
                Package_Stock = 0;
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

    //检查捆绑产品合法性
    public bool Check_Package_Valid(IList<PackageProductInfo> packageproducts)
    {
        bool IsValid = true;
        ProductInfo entity = null;
        foreach (PackageProductInfo obj in packageproducts)
        {
            entity = MyProduct.GetProductByID(obj.Package_Product_ProductID,Public.GetUserPrivilege());
            if (entity != null)
            {
                if (entity.Product_IsInsale == 0)
                {
                    IsValid = false;
                }
                if (entity.Product_IsAudit == 0)
                {
                    IsValid = false;
                }
            }
            else
            {
                IsValid = false;
            }
        }

        return IsValid;
    }

    //获取订单修改产品重量
    public double Get_Goodstmp_Weight(int Supplier_ID,int Orders_ID)
    {
        double total_weight = 0;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_OrdersID", "=", Orders_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID", "=", Supplier_ID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                //统计产品重量
                if (entity.Orders_Goods_Type == 0 || entity.Orders_Goods_Type == 3)
                {
                    goods_product = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (goods_product != null)
                    {
                        total_weight = total_weight + (goods_product.Product_Weight * entity.Orders_Goods_Amount);
                    }
                }
                //统计套装重量
                if (entity.Orders_Goods_Type == 2&&entity.Orders_Goods_ParentID ==0)
                {
                    goods_package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
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
    public double My_Goodstmp_weightprice(double weight, double initialweigh, double initialfee, double upweight, double upfee)
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

    //获取捆绑产品市场价
    public double Get_Package_MKTPrice(IList<PackageProductInfo> packageproducts)
    {
        double Package_MKTPrice = 0;
        string Package_Product_Arry = "0";
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> products = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (products != null)
        {
            foreach (ProductInfo product in products)
            {
                Package_MKTPrice = Package_MKTPrice + product.Product_MKTPrice;
            }

        }

        return Package_MKTPrice;
    }

    //获取运费
    public double Get_Goodstmp_Deliveryfee(int delivery_id, double total_weight)
    {
        double delivery_fee = 0;
        DeliveryWayInfo delivery = deliveryway.GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == Public.GetCurrentSite())
            {
                if (delivery.Delivery_Way_FeeType == 1)
                {
                    delivery_fee = My_Goodstmp_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
                }
                else
                {
                    delivery_fee = delivery.Delivery_Way_Fee;
                }
            }
        }
        return delivery_fee;
    }

    //获取购物车运费
    public double Get_Goodstmp_Deliveryfee(int supplier_id, int delivery_id, double total_weight)
    {

        double delivery_fee = 0;
        DeliveryWayInfo delivery = deliveryway.GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == Public.GetCurrentSite())
            {
                if (delivery.Delivery_Way_FeeType == 1)
                {
                    delivery_fee = My_Goodstmp_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
                }
                else
                {
                    delivery_fee = delivery.Delivery_Way_Fee;
                }
            }
        }
        return delivery_fee;
    }

    //获取购物车产品运费
    public double Get_Cart_FreightFee(int delivery_id, int Orders_ID)
    {
        double total_weight, total_fee;
        total_fee = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_OrdersID", "=", Orders_ID.ToString()));

        string supplier_id = "";

        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (supplier_id == "")
                {
                    supplier_id = entity.Orders_Goods_Product_SupplierID.ToString();
                    total_weight = Get_Goodstmp_Weight(entity.Orders_Goods_Product_SupplierID,Orders_ID);
                    total_fee += Get_Goodstmp_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                }
                else
                {
                    foreach (string substr in supplier_id.Split(','))
                    {
                        if (tools.CheckInt(substr) != entity.Orders_Goods_Product_SupplierID)
                        {
                            supplier_id += "," + entity.Orders_Goods_Product_SupplierID.ToString();
                            total_weight = Get_Goodstmp_Weight(entity.Orders_Goods_Product_SupplierID, Orders_ID);
                            total_fee += Get_Goodstmp_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                        }
                    }
                }
            }
        }
        return total_fee;
    }

    public OrdersInfo GetOrdersByID(int ID)
    {
        return MyBLL.GetOrdersByID(ID);
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

    public IList<OrdersGoodsTmpInfo> OrdersGoodstmpSearch(IList<OrdersGoodsTmpInfo> GoodsList, int ParentID)
    {
        IList<OrdersGoodsTmpInfo> OrdersGoodsList = new List<OrdersGoodsTmpInfo>();
        foreach (OrdersGoodsTmpInfo entity in GoodsList)
        {
            if (entity.Orders_Goods_ParentID == ParentID) { OrdersGoodsList.Add(entity); }
        }
        return OrdersGoodsList;
    }

    public int ClearOrdersGoodsTmpByOrdersID(int Orders_ID)
    {
        return Mygoodstmp.ClearOrdersGoodsTmpByOrdersID(Orders_ID);
    }

    //将订单产品导入待编辑产品
    public void SetOrdersGoods_To_GoodsTmp(int Orders_ID)
    {
        int parent_id = 0;
        Mygoodstmp.ClearOrdersGoodsTmpByOrdersID(Orders_ID);
        OrdersGoodsTmpInfo goodstmp = null;
        IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
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

        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if(ordersinfo==null)
        {return "";}
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
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

                    strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
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
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
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
                    strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
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
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
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

    //添加订单产品
    public void Orders_Goodstmp_Add()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Orders_buyerID = tools.CheckInt(Request["Orders_buyerID"]);
        string product_code = tools.CheckStr(Request["product_code"]);
        double product_price = 0;
        double Product_PurchasingPrice, Product_brokerage;

        OrdersGoodsTmpInfo goodstmp = null;

        if (product_code != "" && Orders_ID > 0 && Orders_buyerID > 0)
        {
            ProductInfo productinfo = MyProduct.GetProductByCode(product_code, Public.GetCurrentSite(), Public.GetUserPrivilege());
            if (productinfo != null)
            {
                //检查产品
                if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == Public.GetCurrentSite())
                {
                    //判断是否已包含此产品
                    if (Check_Contain_Goods(Orders_ID, productinfo.Product_ID) == false)
                    {
                        //判断库存
                        if (productinfo.Product_IsNoStock == 1 || (productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0))
                        {
                            Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                            Product_brokerage = 0;        //初始化佣金

                            //计算佣金
                            if (productinfo.Product_SupplierID > 0)
                            {
                                //采用佣金分类
                                if (productinfo.Product_Supplier_CommissionCateID > 0)
                                {
                                    SupplierCommissionCategoryInfo commissioncate = MyCommission.GetSupplierCommissionCategoryByID(productinfo.Product_Supplier_CommissionCateID, Public.GetUserPrivilege());
                                    if (commissioncate != null)
                                    {
                                        Product_brokerage = (productinfo.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                                    }
                                }
                                else
                                {
                                    //使用差价方式
                                    Product_brokerage = productinfo.Product_Price - productinfo.Product_PurchasingPrice;
                                }
                            }
                            product_price = productprice.Get_Member_Price(Orders_buyerID, productinfo.Product_ID, productinfo.Product_Price);
                            if (productinfo.Product_IsGroupBuy == 1)
                            {
                                if (1 >= productinfo.Product_GroupNum)
                                {
                                    product_price = productinfo.Product_GroupPrice;
                                }
                            }
                            goodstmp = new OrdersGoodsTmpInfo();
                            goodstmp.Orders_Goods_Type = 0;
                            goodstmp.Orders_Goods_BuyerID = Orders_buyerID;
                            goodstmp.Orders_Goods_SessionID = "";
                            goodstmp.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;
                            goodstmp.Orders_Goods_ParentID = 0;
                            goodstmp.Orders_Goods_Product_ID = productinfo.Product_ID;
                            goodstmp.Orders_Goods_Product_Code = productinfo.Product_Code;
                            goodstmp.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                            goodstmp.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                            goodstmp.Orders_Goods_Product_Name = productinfo.Product_Name;
                            goodstmp.Orders_Goods_Product_Img = productinfo.Product_Img;
                            goodstmp.Orders_Goods_Product_Price = product_price;
                            goodstmp.Orders_Goods_Product_MKTPrice = productinfo.Product_MKTPrice;
                            goodstmp.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                            goodstmp.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                            goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                            goodstmp.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);
                            goodstmp.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                            goodstmp.Orders_Goods_Product_UseCoin = 0;
                            goodstmp.Orders_Goods_Amount = 1;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_Product_SalePrice = productinfo.Product_Price;
                            goodstmp.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                            goodstmp.Orders_Goods_Product_brokerage = Product_brokerage;
                            goodstmp.Orders_Goods_OrdersID = Orders_ID;

                            Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }
            }
        }
        Response.Write(Orders_Edit_Goods(Orders_ID, Orders_buyerID, false));
    }

    //修改订单产品数量
    public void Orders_Goodstmp_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int goods_id = tools.CheckInt(Request["goods_id"]);
        int Orders_buyerID = tools.CheckInt(Request["Orders_buyerID"]);
        string Code = tools.CheckStr(Request["Code"]);
        string DeliveryDate = tools.CheckStr(Request["DeliveryDate"]);
        int buyamount = tools.CheckInt(Request["buyamount"]);
        int old_buyamount = 1;
        double product_price = tools.CheckFloat(Request["price"]);
        //if (buyamount < 1)
        //{
        //    buyamount = 1;
        //}
        int package_stock = 0;
        ProductStockInfo packagestockinfo = new ProductStockInfo();

        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        if (goodstmps != null)
        {
            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;
            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                old_buyamount = 0;
                //子商品信息
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);

                if (entity.Orders_Goods_ID == goods_id&&entity.Orders_Goods_Product_ID==0)
                {
                    if (buyamount > 0)
                    {
                        entity.Orders_Goods_Amount = buyamount;
                    }
                    if (product_price > 0)
                    {
                        entity.Orders_Goods_Product_Price = product_price;
                        entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);
                    }
                    if (Code.Length > 0)
                    {
                        entity.Orders_Goods_Product_Code = Code;
                    }
                    if (DeliveryDate.Length > 0)
                    {
                        entity.Orders_Goods_Product_DeliveryDate = DeliveryDate;
                    }

                    Mygoodstmp.EditOrdersGoodsTmp(entity);
                    continue;
                }

                //正常产品
                if (entity.Orders_Goods_Type == 0 && entity.Orders_Goods_ID == goods_id)
                {
                   
                    //获取产品信息
                    ProductInfo productinfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (productinfo != null)
                    {
                        //检查产品
                        if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == Public.GetCurrentSite())
                        {
                            //判断库存
                            if (productinfo.Product_IsNoStock == 1 || (productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0))
                            {
                                //判断限购
                                if (productinfo.Product_QuotaAmount == 0 || (productinfo.Product_QuotaAmount > 0 && productinfo.Product_QuotaAmount >= buyamount))
                                {
                                    //product_price = productprice.Get_Member_Price(Orders_buyerID, productinfo.Product_ID, productinfo.Product_Price);
                                    //if (productinfo.Product_IsGroupBuy == 1)
                                    //{
                                    //    if (buyamount >= productinfo.Product_GroupNum)
                                    //    {
                                    //        product_price = productinfo.Product_GroupPrice;
                                    //    }
                                    //}
                                    if (buyamount > 0)
                                    {
                                        entity.Orders_Goods_Amount = buyamount;
                                    }
                                    if (product_price > 0)
                                    {
                                        entity.Orders_Goods_Product_Price = product_price;
                                        entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);
                                    }
                                    if (DeliveryDate.Length > 0)
                                    {
                                        entity.Orders_Goods_Product_DeliveryDate = DeliveryDate;
                                    }

                                    Mygoodstmp.EditOrdersGoodsTmp(entity);

                                }
                            }
                        }
                    }
                }
                //检查套装
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ID == goods_id)
                {
                    PackageInfo packageInfo = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (packageInfo != null)
                    {
                        if (packageInfo.Package_IsInsale == 1)
                        {
                            //库存判断
                            packagestockinfo = Get_Package_Stock(packageInfo.PackageProductInfos);
                            package_stock = packagestockinfo.Product_Stock_Amount;

                            if (packagestockinfo.Product_Stock_IsNoStock == 1 || (buyamount <= package_stock))
                            {
                                old_buyamount = (int)Math.Round(entity.Orders_Goods_Amount);
                                //product_price = productprice.Get_Member_Price(Orders_buyerID, 0, packageInfo.Package_Price);
                                if (buyamount > 0)
                                {
                                    entity.Orders_Goods_Amount = buyamount;
                                }
                                if (product_price > 0)
                                {
                                    entity.Orders_Goods_Product_Price = product_price;
                                    entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);
                                }
                                if (DeliveryDate.Length > 0)
                                {
                                    entity.Orders_Goods_Product_DeliveryDate = DeliveryDate;
                                }

                                Mygoodstmp.EditOrdersGoodsTmp(entity);
                            }
                        }
                    }
                }

                if (GoodsListSub.Count > 0)
                {

                    if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ID == goods_id)
                    {
                        foreach (OrdersGoodsTmpInfo goods in GoodsListSub)
                        {
                            if (old_buyamount > 0)
                            {
                                if (buyamount > 0)
                                {
                                    goods.Orders_Goods_Amount = (goods.Orders_Goods_Amount / old_buyamount) * buyamount;
                                    Mygoodstmp.EditOrdersGoodsTmp(goods);
                                }
                                
                            }

                        }
                    }

                }
            }
        }
        Response.Write(Orders_Edit_Goods(Orders_ID, Orders_buyerID, false));
    }

    //删除订单产品
    public void Orders_Goodstmp_Del()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Goods_ID = tools.CheckInt(Request["Goods_ID"]);
        int Orders_buyerID = tools.CheckInt(Request["Orders_buyerID"]);
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_ID == Goods_ID)
                {
                    if (entity.Orders_Goods_Type == 2)
                    {
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 2, 0, entity.Orders_Goods_SessionID);

                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 2, Goods_ID, entity.Orders_Goods_SessionID);
                    }
                    else
                    {
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, entity.Orders_Goods_Type, 0, entity.Orders_Goods_SessionID);
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 1, Goods_ID, entity.Orders_Goods_SessionID);
                    }



                }
            }
        }
        Response.Write(Orders_Edit_Goods(Orders_ID, Orders_buyerID, false));

    }

    //检查订单产品临时表中是否包含指定产品
    public bool Check_Contain_Goods(int Orders_ID, int Product_ID)
    {
        bool check_value = false;
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_Type == 0 && entity.Orders_Goods_Product_ID == Product_ID)
                {
                    check_value = true;
                }
            }
        }
        return check_value;
    }

    //检查订单产品临时表中是否包含指定捆绑产品
    public bool Check_Contain_PackageGoods(int Orders_ID, int Package_ID)
    {
        bool check_value = false;
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0 && entity.Orders_Goods_Product_ID == Package_ID)
                {
                    check_value = true;
                }
            }
        }
        return check_value;
    }

    //订单支付方式选择
    public string Pay_Way_Select1(int Payway_ID, int delivery_cod)
    {
        string way_list = "";
        way_list = "<select name=\"order_payway\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> payways = payway.GetPayWays(Query, Public.GetUserPrivilege());
        if (payways != null)
        {
            foreach (PayWayInfo entity in payways)
            {
                way_list = way_list + "  <option value=\"" + entity.Pay_Way_ID + "\" " + Public.CheckedSelected(entity.Pay_Way_ID.ToString(), Payway_ID.ToString()) + ">";
                way_list = way_list + "" + entity.Pay_Way_Name + "";
                way_list = way_list + "</option>";
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    //订单配送方式选择
    public string Delivery_Way_Select1(int Orders_ID, int delivery_way, string state, string city, string county)
    {
        string way_list = "";
        way_list = "<select name=\"order_delivery\" onchange=\"$.ajaxSetup({async: false});$('#fee_td').load('/orders/orders_do.aspx?action=goodstmp_fee&delivery_id='+this.options[this.selectedIndex].value+'&Orders_ID=" + Orders_ID + "&fresh=' + Math.random() + '');updateorderprice();\">";
        IList<DeliveryWayInfo> deliveryways = deliveryway.GetDeliveryWaysByDistrict(state, city, county);
        if (deliveryways != null)
        {
            foreach (DeliveryWayInfo entity in deliveryways)
            {

                way_list = way_list + "  <option value=\"" + entity.Delivery_Way_ID + "\" " + Public.CheckedSelected(entity.Delivery_Way_ID.ToString(), delivery_way.ToString()) + ">";
                way_list = way_list + "" + entity.Delivery_Way_Name + "";
                way_list = way_list + "</option>";
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    //AJAX更新运费
    public void Orders_Goodstmp_Deliveryfee()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int delivery_id = tools.CheckInt(Request["delivery_id"]);
        //double total_weight = Get_Goodstmp_Weight(Orders_ID);
        double delivery_fee = Get_Cart_FreightFee(delivery_id, Orders_ID);

        Response.Write("<input type=\"text\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" id=\"Orders_Total_Freight\" name=\"Orders_Total_Freight\" value=\"" + delivery_fee.ToString("0.00") + "\" onchange=\"updateorderprice();\" />");
    }

    //订单修改
    public void Orders_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Total_Coin = 0;
        double Total_Price = 0;
        double Total_MKTPrice = 0;
        double Total_Allprice = 0;
        int delivery_id, payment_id, parent_id;
        string delivery_name, payment_name,log_remark;
        double delivery_fee, price_discount, fee_discount;

        payment_name = "";
        delivery_name = "";
        parent_id = 0;
        log_remark = "商品金额由：{old_product_price}修改为：{new_product_price};";
        log_remark = log_remark + "价格优惠由：{old_favor_price}修改为：{new_favor_price};订单总金额由：{old_total_price}修改为：{new_total_price};";

        delivery_fee = tools.CheckFloat(Request.Form["Orders_Total_Freight"]);
        price_discount = tools.CheckFloat(Request.Form["Orders_Total_PriceDiscount"]);
        fee_discount = tools.CheckFloat(Request.Form["Orders_Total_FreightDiscount"]);



        delivery_id = tools.CheckInt(Request.Form["order_delivery"]);
        payment_id = tools.CheckInt(Request.Form["order_payway"]);

        //DeliveryWayInfo deliveryinfo = deliveryway.GetDeliveryWayByID(delivery_id);
        //if (deliveryinfo != null)
        //{
        //    if (deliveryinfo.Delivery_Way_Status == 1 && deliveryinfo.Delivery_Way_Site == Public.GetCurrentSite())
        //    {
        //        delivery_name = deliveryinfo.Delivery_Way_Name;
        //    }
        //    else
        //    {
        //        delivery_id = 0;
        //    }
        //}
        //else
        //{
        //    delivery_id = 0;
        //}

        //PayWayInfo paywayinfo = payway.GetPayWayByID(payment_id, Public.GetUserPrivilege());
        //if (paywayinfo != null)
        //{
        //    if (paywayinfo.Pay_Way_Status == 1 && paywayinfo.Pay_Way_Site == Public.GetCurrentSite())
        //    {
        //        payment_name = paywayinfo.Pay_Way_Name;
        //    }
        //    else
        //    {
        //        payment_id = 0;
        //    }
        //}
        //else
        //{
        //    payment_id = 0;
        //}

        //if (delivery_id == 0 || payment_id == 0)
        //{
        //    Public.Msg("error", "错误提示", "配送方式或支付方式错误！", false, "{back}");
        //    Response.End();
        //}

        OrdersGoodsInfo ordergoods = null;
        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status <1 && ordersinfo.Orders_DeliveryStatus == 0 && ordersinfo.Orders_PaymentStatus == 0)
            {
                log_remark = log_remark.Replace("{old_product_price}", ordersinfo.Orders_Total_Price.ToString());
                //log_remark = log_remark.Replace("{old_freight}", ordersinfo.Orders_Delivery_Name);
                log_remark = log_remark.Replace("{old_payment}", ordersinfo.Orders_Payway_Name);
                //log_remark = log_remark.Replace("{old_freight_fee}", ordersinfo.Orders_Total_Freight.ToString());
                log_remark = log_remark.Replace("{old_favor_price}", ordersinfo.Orders_Total_PriceDiscount.ToString());
                //log_remark = log_remark.Replace("{old_favor_fee}", ordersinfo.Orders_Total_FreightDiscount.ToString());
                log_remark = log_remark.Replace("{old_total_price}", ordersinfo.Orders_Total_AllPrice.ToString());
                IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(Orders_ID);
                if (goodstmps != null)
                {
                    //删除原订单产品并添加新产品
                    MyBLL.DelOrdersGoodsByOrdersID(Orders_ID);
                    IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
                    IList<OrdersGoodsTmpInfo> GoodsListSub = null;


                    foreach (OrdersGoodsTmpInfo entity in GoodsList)
                    {
                        GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);
                        Total_Coin = Total_Coin + (int)Math.Round(entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);
                        Total_Price = Total_Price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
                        Total_MKTPrice = Total_MKTPrice + (entity.Orders_Goods_Product_MKTPrice * entity.Orders_Goods_Amount);
                        ordergoods = new OrdersGoodsInfo();
                        ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                        ordergoods.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                        ordergoods.Orders_Goods_ParentID = entity.Orders_Goods_ParentID;
                        ordergoods.Orders_Goods_OrdersID = Orders_ID;
                        ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                        ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                        ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                        ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                        ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                        ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                        ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                        ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                        ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                        ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                        ordergoods.Orders_Goods_Product_DeliveryDate = entity.Orders_Goods_Product_DeliveryDate;
                        ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                        ordergoods.Orders_Goods_Product_brokerage = entity.Orders_Goods_Product_brokerage;
                        ordergoods.Orders_Goods_Product_SalePrice = entity.Orders_Goods_Product_SalePrice;
                        ordergoods.Orders_Goods_Product_PurchasingPrice = entity.Orders_Goods_Product_PurchasingPrice;
                        ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                        ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                        ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                        ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                        

                        MyBLL.AddOrdersGoods(ordergoods);
                        ordergoods = null;

                        if (GoodsListSub.Count > 0)
                        {
                            parent_id = MyBLL.Get_Max_Goods_ID();
                            if (parent_id > 0)
                            {
                                foreach (OrdersGoodsTmpInfo goods in GoodsListSub)
                                {
                                    ordergoods = new OrdersGoodsInfo();
                                    ordergoods.Orders_Goods_Type = goods.Orders_Goods_Type;
                                    ordergoods.Orders_Goods_ParentID = parent_id;
                                    ordergoods.Orders_Goods_Product_SupplierID = entity.Orders_Goods_Product_SupplierID;
                                    ordergoods.Orders_Goods_OrdersID = Orders_ID;
                                    ordergoods.Orders_Goods_Product_ID = goods.Orders_Goods_Product_ID;
                                    ordergoods.Orders_Goods_Product_Code = goods.Orders_Goods_Product_Code;
                                    ordergoods.Orders_Goods_Product_CateID = goods.Orders_Goods_Product_CateID;
                                    ordergoods.Orders_Goods_Product_BrandID = goods.Orders_Goods_Product_BrandID;
                                    ordergoods.Orders_Goods_Product_Name = goods.Orders_Goods_Product_Name;
                                    ordergoods.Orders_Goods_Product_Img = goods.Orders_Goods_Product_Img;
                                    ordergoods.Orders_Goods_Product_Price = goods.Orders_Goods_Product_Price;
                                    ordergoods.Orders_Goods_Product_MKTPrice = goods.Orders_Goods_Product_MKTPrice;
                                    ordergoods.Orders_Goods_Product_Maker = goods.Orders_Goods_Product_Maker;
                                    ordergoods.Orders_Goods_Product_Spec = goods.Orders_Goods_Product_Spec;
                                    ordergoods.Orders_Goods_Product_AuthorizeCode = goods.Orders_Goods_Product_AuthorizeCode;
                                    ordergoods.Orders_Goods_Product_brokerage = goods.Orders_Goods_Product_brokerage;
                                    ordergoods.Orders_Goods_Product_SalePrice = entity.Orders_Goods_Product_SalePrice;
                                    ordergoods.Orders_Goods_Product_PurchasingPrice = entity.Orders_Goods_Product_PurchasingPrice;
                                    ordergoods.Orders_Goods_Product_Coin = goods.Orders_Goods_Product_Coin;
                                    ordergoods.Orders_Goods_Product_IsFavor = goods.Orders_Goods_Product_IsFavor;
                                    ordergoods.Orders_Goods_Product_UseCoin = goods.Orders_Goods_Product_UseCoin;
                                    ordergoods.Orders_Goods_Amount = goods.Orders_Goods_Amount;

                                    MyBLL.AddOrdersGoods(ordergoods);
                                    ordergoods = null;
                                }
                            }
                        }

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


                    //ordersinfo.Orders_Payway = payment_id;
                    //ordersinfo.Orders_Payway_Name = payment_name;
                    //ordersinfo.Orders_Delivery = delivery_id;
                    //ordersinfo.Orders_Delivery_Name = delivery_name;
                    ordersinfo.Orders_Total_Price = Total_Price;
                    ordersinfo.Orders_Total_MKTPrice = Total_MKTPrice;
                    ordersinfo.Orders_Total_Coin = Total_Coin;
                    ordersinfo.Orders_Total_Freight = delivery_fee;
                    ordersinfo.Orders_Total_FreightDiscount = fee_discount;
                    ordersinfo.Orders_Total_PriceDiscount = price_discount;
                    ordersinfo.Orders_Total_AllPrice = Total_Allprice;
                    log_remark = log_remark.Replace("{new_product_price}", ordersinfo.Orders_Total_Price.ToString());
                    //log_remark = log_remark.Replace("{new_freight}", ordersinfo.Orders_Delivery_Name);
                    log_remark = log_remark.Replace("{new_payment}", ordersinfo.Orders_Payway_Name);
                    //log_remark = log_remark.Replace("{new_freight_fee}", ordersinfo.Orders_Total_Freight.ToString());
                    log_remark = log_remark.Replace("{new_favor_price}", ordersinfo.Orders_Total_PriceDiscount.ToString());
                    //log_remark = log_remark.Replace("{new_favor_fee}", ordersinfo.Orders_Total_FreightDiscount.ToString());
                    log_remark = log_remark.Replace("{new_total_price}", ordersinfo.Orders_Total_AllPrice.ToString());

                    MyBLL.EditOrders(ordersinfo);

                    Mygoodstmp.ClearOrdersGoodsTmpByOrdersID(Orders_ID);
                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "订单修改", "成功", log_remark);
                    Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);

                }
                else
                {
                    Public.Msg("error", "错误提示", "请添加订单产品！", false, "{back}");
                }
            }
        }

    }


    #endregion

    #region "客服下单"

    //用户选择
    public string Member_Select()
    {
        string keyword = "";
        keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Phone_Number", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberProfileInfo.Member_Mobile", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        if (Query.CurrentPage == 0)
        {
            Query.CurrentPage = 1;
        }

        PageInfo pageinfo = MyMEM.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberInfo> entitys = MyMEM.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberInfo entity in entitys)
            {

                jsonBuilder.Append("{\"MemberInfo.Member_ID\":" + entity.Member_ID + ",\"cell\":[");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<input type=\\\"button\\\" value=\\\"选择\\\" class=\\\"btn_01\\\" onclick=\\\"$('#member_id',window.opener.document).attr('value','" + entity.Member_ID + "');$('#member_name',window.opener.document).html('" + entity.Member_NickName + "');window.close();\\\">");
                jsonBuilder.Append("\",");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_NickName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplaySex(entity.MemberProfileInfo.Member_Sex));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.MemberProfileInfo.Member_Phone_Number);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Mobile));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Member_Account));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_CoinRemain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Addtime.ToString("yy-MM-dd"));
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

    //产品选择
    public string Product_Select()
    {
        string keyword = "";
        int product_cate = 0;
        keyword = tools.CheckStr(Request["keyword"]);
        product_cate = tools.CheckInt(Request["product_cate"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "like", keyword));
        }
        if (product_cate > 0)
        {
            string subCates = product.Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
            //string product_idstr = product.Get_All_CateProductID(subCates);
            //if (subCates == product_cate.ToString())
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_CateID", "=", product_cate.ToString()));
            //}
            //else
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_CateID", "IN", subCates));
            //}
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "IN", product_idstr));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsInsale", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        if (Query.CurrentPage == 0)
        {
            Query.CurrentPage = 1;
        }
        PageInfo pageinfo = MyProduct.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Product_ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Code));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Spec));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Maker));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Product_MKTPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Product_Price));
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

    //捆绑产品选择
    public string Package_Select()
    {
        string keyword = "";
        keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_IsInsale", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        if (Query.CurrentPage == 0)
        {
            Query.CurrentPage = 1;
        }
        PageInfo pageinfo = MyPackage.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<PackageInfo> entitys = MyPackage.GetPackages(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PackageInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Package_ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Package_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Package_Name));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Package_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\" alt=\\\"查看\\\"> <a href=\\\"/package/package_view.aspx?package_id=" + entity.Package_ID + "\\\" title=\\\"查看\\\">查看</a>");
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

    //检查订单产品临时表中是否包含指定产品
    public bool Check_Cart_Contain_Goods(int Product_ID)
    {
        bool check_value = false;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_ID", "=", Product_ID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            check_value = true;
        }
        return check_value;
    }

    //检查订单产品临时表中是否包含指定捆绑产品
    public bool Check_Cart_Contain_PackageGoods(int Package_ID)
    {
        bool check_value = false;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Type", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_Product_ID", "=", Package_ID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0 && entity.Orders_Goods_Product_ID == Package_ID)
                {
                    check_value = true;
                }
            }
        }
        return check_value;
    }

    //添加产品
    public void Cart_Product_Add()
    {
        string Product_id = tools.CheckStr(Request["product_id"]);
        double product_price = 0;
        double Product_PurchasingPrice, Product_brokerage;

        OrdersGoodsTmpInfo goodstmp = null;

        if (Product_id != "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", Product_id));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (ProductInfo productinfo in entitys)
                {

                    //判断是否已包含此产品
                    if (Check_Cart_Contain_Goods(productinfo.Product_ID) == false)
                    {
                        //判断库存
                        if (productinfo.Product_IsNoStock == 1 || (productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0))
                        {
                            Product_PurchasingPrice = productinfo.Product_PurchasingPrice;
                            Product_brokerage = 0;        //初始化佣金

                            //计算佣金
                            if (productinfo.Product_SupplierID > 0)
                            {
                                //采用佣金分类
                                if (productinfo.Product_Supplier_CommissionCateID > 0)
                                {
                                    SupplierCommissionCategoryInfo commissioncate = MyCommission.GetSupplierCommissionCategoryByID(productinfo.Product_Supplier_CommissionCateID, Public.GetUserPrivilege());
                                    if (commissioncate != null)
                                    {
                                        Product_brokerage = (productinfo.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                                    }
                                }
                                else
                                {
                                    //使用差价方式
                                    Product_brokerage = productinfo.Product_Price - productinfo.Product_PurchasingPrice;
                                }
                            }
                            product_price = productinfo.Product_Price;
                            if (productinfo.Product_IsGroupBuy == 1)
                            {
                                if (1 >= productinfo.Product_GroupNum)
                                {
                                    product_price = productinfo.Product_GroupPrice;
                                }
                            }
                            goodstmp = new OrdersGoodsTmpInfo();
                            goodstmp.Orders_Goods_Type = 0;
                            goodstmp.Orders_Goods_BuyerID = 0;
                            goodstmp.Orders_Goods_SessionID = Session.SessionID;
                            goodstmp.Orders_Goods_Product_SupplierID = productinfo.Product_SupplierID;
                            goodstmp.Orders_Goods_ParentID = 0;
                            goodstmp.Orders_Goods_Product_ID = productinfo.Product_ID;
                            goodstmp.Orders_Goods_Product_Code = productinfo.Product_Code;
                            goodstmp.Orders_Goods_Product_CateID = productinfo.Product_CateID;
                            goodstmp.Orders_Goods_Product_BrandID = productinfo.Product_BrandID;
                            goodstmp.Orders_Goods_Product_Name = productinfo.Product_Name;
                            goodstmp.Orders_Goods_Product_Img = productinfo.Product_Img;
                            goodstmp.Orders_Goods_Product_Price = product_price;
                            goodstmp.Orders_Goods_Product_MKTPrice = productinfo.Product_MKTPrice;
                            goodstmp.Orders_Goods_Product_Maker = productinfo.Product_Maker;
                            goodstmp.Orders_Goods_Product_Spec = productinfo.Product_Spec;
                            goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                            goodstmp.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(0, product_price);
                            goodstmp.Orders_Goods_Product_IsFavor = productinfo.Product_IsFavor;
                            goodstmp.Orders_Goods_Product_UseCoin = 0;
                            goodstmp.Orders_Goods_Amount = 1;
                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                            goodstmp.Orders_Goods_Product_SalePrice = productinfo.Product_Price;
                            goodstmp.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                            goodstmp.Orders_Goods_Product_brokerage = Product_brokerage;
                            goodstmp.Orders_Goods_OrdersID = -1;

                            Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                            goodstmp = null;
                        }
                    }
                }
            }
        }
        Response.Write(Orders_Goods_List(0, false));
    }

    //添加捆绑产品
    public void Cart_Package_Add()
    {
        string Package_id = tools.CheckStr(Request["package_id"]);
        double Package_price = 0;
        double Package_MKTPrice = 0;
        ProductStockInfo packagestockinfo = null;
        int package_stock = 0;
        int goods_parentid = 0;
        int Package_Coin = 0;
        string Package_Product_Arry = "0";
        double Product_PurchasingPrice, Product_brokerage;
        SupplierCommissionCategoryInfo commissioncate;
        OrdersGoodsTmpInfo goodstmp = null;

        if (Package_id != "")
        {
            QueryInfo Query = new QueryInfo();

            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_ID", "in", Package_id));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_IsInsale", "=", "1"));
            //获取套装信息
            IList<PackageInfo> entitys = MyPackage.GetPackages(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (PackageInfo packageInfo in entitys)
                {
                    //判断是否已添加
                    if (Check_Cart_Contain_PackageGoods(packageInfo.Package_ID) == false)
                    {
                        //库存判断
                        packagestockinfo = Get_Package_Stock(packageInfo.PackageProductInfos);
                        package_stock = packagestockinfo.Product_Stock_Amount;

                        if ((1 >= package_stock) && packagestockinfo.Product_Stock_IsNoStock == 0)
                        {
                            Response.Write(Orders_Goods_List(0, false));
                            Response.End();
                        }
                        //套装产品合法性判断
                        if (Check_Package_Valid(packageInfo.PackageProductInfos) == false)
                        {
                            Response.Write(Orders_Goods_List(0, false));
                            Response.End();
                        }


                        Package_price = productprice.Get_Member_Price(0, 0, packageInfo.Package_Price);
                        Package_MKTPrice = Get_Package_MKTPrice(packageInfo.PackageProductInfos);

                        Package_Coin = productprice.Get_Member_Coin(0, Package_price);

                        if (Package_MKTPrice == 0)
                        {
                            Package_MKTPrice = Package_price;
                        }

                        goodstmp = new OrdersGoodsTmpInfo();
                        goodstmp.Orders_Goods_ID = 0;
                        goodstmp.Orders_Goods_Type = 2;
                        goodstmp.Orders_Goods_BuyerID = 0;
                        goodstmp.Orders_Goods_SessionID = Session.SessionID;
                        goodstmp.Orders_Goods_Product_SupplierID = 0;
                        goodstmp.Orders_Goods_ParentID = 0;
                        goodstmp.Orders_Goods_Product_ID = packageInfo.Package_ID;
                        goodstmp.Orders_Goods_Product_Code = "";
                        goodstmp.Orders_Goods_Product_CateID = 0;
                        goodstmp.Orders_Goods_Product_BrandID = 0;
                        goodstmp.Orders_Goods_Product_Name = packageInfo.Package_Name;
                        goodstmp.Orders_Goods_Product_Img = "/images/icon_package.gif";
                        goodstmp.Orders_Goods_Product_Price = Package_price;
                        goodstmp.Orders_Goods_Product_MKTPrice = Package_MKTPrice;
                        goodstmp.Orders_Goods_Product_Maker = "";
                        goodstmp.Orders_Goods_Product_Spec = "";
                        goodstmp.Orders_Goods_Product_AuthorizeCode = "";
                        goodstmp.Orders_Goods_Product_Coin = Package_Coin;
                        goodstmp.Orders_Goods_Product_IsFavor = 1;
                        goodstmp.Orders_Goods_Product_UseCoin = 0;
                        goodstmp.Orders_Goods_Amount = 1;
                        goodstmp.Orders_Goods_Addtime = DateTime.Now;
                        goodstmp.Orders_Goods_OrdersID = -1;
                        goodstmp.Orders_Goods_Product_SalePrice = Package_price;
                        goodstmp.Orders_Goods_Product_PurchasingPrice = 0;
                        goodstmp.Orders_Goods_Product_brokerage = 0;

                        Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                        goodstmp = null;

                        //获取主绑定产品在购物车内的编号
                        goods_parentid = Mygoodstmp.Get_Orders_Goods_ParentID(Session.SessionID, packageInfo.Package_ID, 2);

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
                            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
                            Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                            IList<ProductInfo> products = MyProduct.GetProductList(Query1, Public.GetUserPrivilege());
                            if (products != null)
                            {
                                foreach (PackageProductInfo packproduct in packageInfo.PackageProductInfos)
                                {
                                    foreach (ProductInfo entity in products)
                                    {
                                        if (packproduct.Package_Product_ProductID == entity.Product_ID)
                                        {
                                            Product_PurchasingPrice = entity.Product_PurchasingPrice;
                                            Product_brokerage = 0;        //初始化佣金

                                            //计算佣金
                                            if (entity.Product_SupplierID > 0)
                                            {
                                                //采用佣金分类
                                                if (entity.Product_Supplier_CommissionCateID > 0)
                                                {
                                                    commissioncate = MyCommission.GetSupplierCommissionCategoryByID(entity.Product_Supplier_CommissionCateID, Public.GetUserPrivilege());
                                                    if (commissioncate != null)
                                                    {
                                                        Product_brokerage = (entity.Product_Price * commissioncate.Supplier_Commission_Cate_Amount) / 100;
                                                    }
                                                }
                                                else
                                                {
                                                    //使用差价方式
                                                    Product_brokerage = entity.Product_Price - entity.Product_PurchasingPrice;
                                                }
                                            }

                                            goodstmp = new OrdersGoodsTmpInfo();
                                            goodstmp.Orders_Goods_ID = 0;
                                            goodstmp.Orders_Goods_Type = 2;
                                            goodstmp.Orders_Goods_BuyerID = 0;
                                            goodstmp.Orders_Goods_SessionID = Session.SessionID;
                                            goodstmp.Orders_Goods_Product_SupplierID = entity.Product_SupplierID;
                                            goodstmp.Orders_Goods_ParentID = goods_parentid;
                                            goodstmp.Orders_Goods_Product_ID = entity.Product_ID;
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
                                            goodstmp.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(0, entity.Product_Price);
                                            goodstmp.Orders_Goods_Product_IsFavor = entity.Product_IsFavor;
                                            goodstmp.Orders_Goods_Product_UseCoin = 0;
                                            goodstmp.Orders_Goods_Amount = packproduct.Package_Product_Amount;
                                            goodstmp.Orders_Goods_Addtime = DateTime.Now;
                                            goodstmp.Orders_Goods_OrdersID = -1;
                                            goodstmp.Orders_Goods_Product_SalePrice = entity.Product_Price;
                                            goodstmp.Orders_Goods_Product_PurchasingPrice = Product_PurchasingPrice;
                                            goodstmp.Orders_Goods_Product_brokerage = Product_brokerage;

                                            Mygoodstmp.AddOrdersGoodsTmp(goodstmp);
                                            goodstmp = null;
                                        }
                                    }
                                }

                            }
                        }
                    }

                }
            }
        }
        Response.Write(Orders_Goods_List(0, false));
    }

    //修改订单产品数量
    public void Cart_Goodstmp_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Product_ID = tools.CheckInt(Request["Product_ID"]);
        int Orders_buyerID = tools.CheckInt(Request["Orders_buyerID"]);
        int buyamount = tools.CheckInt(Request["buyamount"]);
        int old_buyamount = 1;
        double product_price = 0;
        if (buyamount < 1)
        {
            buyamount = 1;
        }
        int package_stock = 0;
        ProductStockInfo packagestockinfo = new ProductStockInfo();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;
            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                old_buyamount = 0;
                //子商品信息
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);

                //正常产品
                if (entity.Orders_Goods_Type == 0 && entity.Orders_Goods_Product_ID == Product_ID)
                {
                    //获取产品信息
                    ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, Public.GetUserPrivilege());
                    if (productinfo != null)
                    {
                        //检查产品
                        if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1 && productinfo.Product_Site == Public.GetCurrentSite())
                        {
                            //判断库存
                            if (productinfo.Product_IsNoStock == 1 || (productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0))
                            {
                                //判断限购
                                if (productinfo.Product_QuotaAmount == 0 || (productinfo.Product_QuotaAmount > 0 && productinfo.Product_QuotaAmount >= buyamount))
                                {
                                    product_price = productprice.Get_Member_Price(Orders_buyerID, productinfo.Product_ID, productinfo.Product_Price);
                                    if (productinfo.Product_IsGroupBuy == 1)
                                    {
                                        if (buyamount >= productinfo.Product_GroupNum)
                                        {
                                            product_price = productinfo.Product_GroupPrice;
                                        }
                                    }
                                    entity.Orders_Goods_Amount = buyamount;
                                    entity.Orders_Goods_Product_Price = product_price;
                                    entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);

                                    Mygoodstmp.EditOrdersGoodsTmp(entity);

                                }
                            }
                        }
                    }
                }
                //检查套装
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_Product_ID == Product_ID)
                {
                    PackageInfo packageInfo = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (packageInfo != null)
                    {
                        if (packageInfo.Package_IsInsale == 1)
                        {
                            //库存判断
                            packagestockinfo = Get_Package_Stock(packageInfo.PackageProductInfos);
                            package_stock = packagestockinfo.Product_Stock_Amount;

                            if (packagestockinfo.Product_Stock_IsNoStock == 1 || (buyamount <= package_stock))
                            {
                                old_buyamount = (int)Math.Round(entity.Orders_Goods_Amount);
                                product_price = productprice.Get_Member_Price(Orders_buyerID, 0, packageInfo.Package_Price);
                                entity.Orders_Goods_Amount = buyamount;
                                entity.Orders_Goods_Product_Price = product_price;
                                entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(Orders_buyerID, product_price);

                                Mygoodstmp.EditOrdersGoodsTmp(entity);
                            }
                        }
                    }
                }

                if (GoodsListSub.Count > 0)
                {

                    if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_Product_ID == Product_ID)
                    {
                        foreach (OrdersGoodsTmpInfo goods in GoodsListSub)
                        {
                            if (old_buyamount > 0)
                            {
                                goods.Orders_Goods_Amount = (goods.Orders_Goods_Amount / old_buyamount) * buyamount;
                                Mygoodstmp.EditOrdersGoodsTmp(goods);
                            }

                        }
                    }

                }
            }
        }
        Response.Write(Orders_Goods_List(Orders_buyerID, false));
    }

    //删除订单产品
    public void Cart_Goodstmp_Del()
    {
        int Goods_ID = tools.CheckInt(Request["Goods_ID"]);
        int Orders_buyerID = tools.CheckInt(Request["Orders_buyerID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                if (entity.Orders_Goods_ID == Goods_ID)
                {
                    if (entity.Orders_Goods_Type == 2)
                    {
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 2, 0, entity.Orders_Goods_SessionID);

                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 2, Goods_ID, entity.Orders_Goods_SessionID);
                    }
                    else
                    {
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, entity.Orders_Goods_Type, 0, entity.Orders_Goods_SessionID);
                        Mygoodstmp.DelOrdersGoodsTmp(Goods_ID, 1, Goods_ID, entity.Orders_Goods_SessionID);
                    }



                }
            }
        }
        Response.Write(Orders_Goods_List(Orders_buyerID, false));

    }

    //订单商品清单
    public string Orders_Goods_List(int Orders_buyerID, bool ispreview)
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
        //strHTML += "        <td>市场价</td>";
        strHTML += "        <td>单价</td>";
        strHTML += "        <td>数量</td>";
        strHTML += "        <td>合计</td>";
        if (ispreview == false)
        {
            strHTML += "        <td>删除</td>";
        }
        strHTML += "    </tr>";


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;

            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                //子商品信息
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);

                strHTML += "    <tr class=\"goods_list\">";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
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

                    strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
                    strHTML += "    <td>" + entity.Orders_Goods_Product_Name;
                    if (GoodsListSub.Count > 0) { strHTML += "<img width=\"20\" src=\"/images/icon_gift.gif\" style=\"margin-left:5px;vertical-align:middle;cursor:pointer;\" onclick=\"displaySubGoods(" + entity.Orders_Goods_ID + ");\" />"; }
                    strHTML += "</td>";
                }

                strHTML += "</tr>";
                strHTML += "</table></td>";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Spec + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Maker + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                if (ispreview == false)
                {
                    strHTML += "        <td><input type=\"text\" size=\"5\" value=\"" + entity.Orders_Goods_Amount + "\" name=\"Orders_Goods_Amount\" onchange=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmpcart_edit&buyamount='+this.value+'&product_id=" + entity.Orders_Goods_Product_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');";
                    strHTML += "MM_findObj('Orders_Total_Price').value=MM_findObj('goodsprice').value;updateorderprice();";
                    strHTML += "\"></td>";
                }
                else
                {
                    strHTML += "        <td>" + entity.Orders_Goods_Amount + "</tD>";
                }
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                if (ispreview == false)
                {
                    strHTML += "        <td align=\"center\"><input class=\"btn_01\" type=\"button\" value=\"删除\" onclick=\"$.ajaxSetup({async: false});$('#goods_tmpinfo').load('/orders/orders_do.aspx?action=goodstmpcart_del&goods_id=" + entity.Orders_Goods_ID + "&Orders_buyerID=" + Orders_buyerID + "&fresh=' + Math.random() + '');";
                    strHTML += "MM_findObj('Orders_Total_Price').value=MM_findObj('goodsprice').value;updateorderprice();";

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

    //购物车产品统计
    public int Cart_Product_Count()
    {
        int Goods_Count = 0;  //购物车产品统计
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_OrdersID", "=", "-1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        PageInfo page = Mygoodstmp.GetPageInfo(Query);
        if (page != null)
        {
            Goods_Count = page.RecordCount;
        }
        return Goods_Count;
    }

    //收货地址选择
    public void Member_Address(int Member_ID)
    {
        string address_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", Member_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        address_list = address_list + "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        if (entitys != null)
        {
            foreach (MemberAddressInfo entity in entitys)
            {

                address_list = address_list + "<tr><td height=\"30\">";
                address_list = address_list + "<input type=\"radio\" name=\"Orders_Address_ID\" value=\"" + entity.Member_Address_ID + "\" onclick=\"$.ajaxSetup({async: false});$('#other_addr').load('ordercreate_do.aspx?action=changeaddr&address_id=" + entity.Member_Address_ID + "&fresh=' + Math.random() + '');";
                address_list = address_list + "$('#delivery_area').load('ordercreate_do.aspx?action=changedelivery&state=" + entity.Member_Address_State + "&county=" + entity.Member_Address_County + "&city=" + entity.Member_Address_City + "&fresh=' + Math.random() + '');";

                address_list = address_list + "$('#other_addr').hide();\">";

                address_list = address_list + " " + entity.Member_Address_Name + " ";

                address_list = address_list + "" + Addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + "";

                address_list = address_list + " " + entity.Member_Address_StreetAddress + "";

                address_list = address_list + " " + entity.Member_Address_Zip + "";

                address_list = address_list + " Tel：" + entity.Member_Address_Phone_Number + "";

                address_list = address_list + " " + entity.Member_Address_Mobile + "";

                address_list = address_list + "</td></tr>";
            }

        }
        address_list = address_list + "<tr><td height=\"30\"><input type=\"radio\" name=\"Orders_Address_ID\" value=\"0\" onclick=\"$('#other_addr').show();\"> 其他收货地址</td></tr>";
        address_list = address_list + "</table>";
        Response.Write(address_list);
    }

    //修改其他城市信息
    public void Change_Member_Address()
    {
        int address_id;
        address_id = tools.CheckInt(Request["address_id"]);
        string address_list = "";

        string address_city, address_state, address_county, address_street, address_name, address_zipcode, address_tel, address_mobile;
        address_city = "";
        address_state = "";
        address_county = "";
        address_street = "";
        address_name = "";
        address_zipcode = "";
        address_tel = "";
        address_mobile = "";

        if (address_id > 0)
        {
            MemberAddressInfo entity = MyAddr.GetMemberAddressByID(address_id);
            if (entity != null)
            {
                address_city = entity.Member_Address_City;
                address_state = entity.Member_Address_State;
                address_county = entity.Member_Address_County;
                address_street = entity.Member_Address_StreetAddress;
                address_name = entity.Member_Address_Name;
                address_zipcode = entity.Member_Address_Zip;
                address_tel = entity.Member_Address_Phone_Number;
                address_mobile = entity.Member_Address_Mobile;
            }

        }

        address_list = address_list + "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
        address_list = address_list + "	<tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">省份</td>";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\">";
        address_list = address_list + "	<input type=\"hidden\" name=\"Orders_Address_Country\" id=\"Orders_Address_Country\" value=\"CN\" />";
        address_list = address_list + "	<input type=\"hidden\" id=\"Orders_Address_State\" name=\"Orders_Address_State\" value=\"" + address_state + "\">";
        address_list = address_list + "    <input type=\"hidden\" id=\"Orders_Address_City\" name=\"Orders_Address_City\" value=\"" + address_city + "\">";
        address_list = address_list + "    <input type=\"hidden\" id=\"Orders_Address_County\" name=\"Orders_Address_County\" value=\"" + address_county + "\">";
        address_list = address_list + "	<span id=\"textDiv\">" + Addr.UOD_SelectAddress("textDiv", "Orders_Address_State", "Orders_Address_City", "Orders_Address_County", address_state, address_city, address_county) + "</span> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "  <tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">收货地址</td>";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\"><input name=\"Orders_Address_StreetAddress\" type=\"text\" id=\"Orders_Address_StreetAddress\" size=\"60\" maxlength=\"100\" value=\"" + address_street + "\"/> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "  <tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">邮编</td>";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\"><input name=\"Orders_Address_Zip\" type=\"text\" id=\"Orders_Address_Zip\" size=\"20\" maxlength=\"10\" value=\"" + address_zipcode + "\"/> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "  <tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">收货人姓名</td>";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\"><input name=\"Orders_Address_Name\" type=\"text\" id=\"Orders_Address_Name\" size=\"20\" maxlength=\"50\" value=\"" + address_name + "\"/> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "  <tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">联系电话</td>";
        address_list = address_list + "	";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\">";
        address_list = address_list + "	<input name=\"Orders_Address_Phone_Countrycode\" type=\"hidden\" id=\"Orders_Address_Phone_Countrycode\" value=\"+86\" />";
        address_list = address_list + "	<input name=\"Orders_Address_Phone_Number\" type=\"text\" id=\"Orders_Address_Phone_Number\" size=\"20\" maxlength=\"20\" value=\"" + address_tel + "\"/> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "    <tr>";
        address_list = address_list + "	<td width=\"100\" height=\"23\" align=\"right\">手机</td>";
        address_list = address_list + "	<td align=\"left\" class=\"cell_content\"><input name=\"Orders_Address_Mobile\" type=\"text\" id=\"Orders_Address_Mobile\" size=\"20\" maxlength=\"20\" value=\"" + address_mobile + "\"/> <span class=\"Required\">*</span></td>";
        address_list = address_list + "  </tr>";
        address_list = address_list + "	</table>";
        Response.Write(address_list);

    }

    //订单配送方式选择
    public string Delivery_Way_Select(string state, string city, string county)
    {
        //double total_weight = Get_Goodstmp_Weight(0,-1);
        double delivery_fee = 0;
        string way_list = "";
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        IList<DeliveryWayInfo> deliveryways = deliveryway.GetDeliveryWaysByDistrict(state, city, county);
        if (deliveryways != null)
        {
            foreach (DeliveryWayInfo entity in deliveryways)
            {
                delivery_fee = Get_Cart_FreightFee(entity.Delivery_Way_ID, -1);
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\"><input type=\"radio\" name=\"Orders_Delivery_ID\" id=\"Orders_Delivery_ID\" value=\"" + entity.Delivery_Way_ID + "\" onclick=\"$('#freight_fee').load('ordercreate_do.aspx?action=changefee&delivery_fee=" + delivery_fee + "');$('#order_price').load('ordercreate_do.aspx?action=changeallprice&delivery_fee=" + delivery_fee + "');\"></td>";
                way_list = way_list + "  <td >" + entity.Delivery_Way_Name + " </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //订单支付方式选择
    public string Pay_Way_Select(int delivery_cod)
    {
        string way_list = "";
        int i = 0;
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (delivery_cod == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Cod", "=", "0"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> payways = payway.GetPayWays(Query, Public.GetUserPrivilege());
        if (payways != null)
        {
            foreach (PayWayInfo entity in payways)
            {
                i++;
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\">";
                if (i == 1)
                {
                    way_list = way_list + "<input type=\"radio\" name=\"Orders_Payway_ID\" id=\"Orders_Payway_ID\" value=\"" + entity.Pay_Way_ID + "\" checked ></td>";
                }
                else
                {
                    way_list = way_list + "<input type=\"radio\" name=\"Orders_Payway_ID\" id=\"Orders_Payway_ID\" value=\"" + entity.Pay_Way_ID + "\" ></td>";
                }
                way_list = way_list + "  <td >" + entity.Pay_Way_Name + " </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //订单送货时间方式选择
    public string DeliveryTime_Select()
    {
        string way_list = "";
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryTimeInfo.Delivery_Time_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryTimeInfo.Delivery_Time_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("DeliveryTimeInfo.Delivery_Time_Sort", "asc"));
        IList<DeliveryTimeInfo> entitys = MyDeliverytime.GetDeliveryTimes(Query);
        if (entitys != null)
        {
            foreach (DeliveryTimeInfo entity in entitys)
            {
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\"><input type=\"radio\" name=\"Orders_DeliveryTime_ID\" id=\"Orders_DeliveryTime_ID\" value=\"" + entity.Delivery_Time_ID + "\" ></td>";
                way_list = way_list + "  <td >" + entity.Delivery_Time_Name + " </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //订单付款条件选择
    public string PayType_Select()
    {
        string way_list = "";
        int i = 0;
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayTypeInfo.Pay_Type_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayTypeInfo.Pay_Type_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("PayTypeInfo.Pay_Type_Sort", "asc"));
        IList<PayTypeInfo> entitys = MyPayType.GetPayTypes(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PayTypeInfo entity in entitys)
            {
                i++;

                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\">";
                if (i == 1)
                {
                    way_list = way_list + "<input type=\"radio\" name=\"Orders_Paytype_ID\" id=\"Orders_Paytype_ID\" value=\"" + entity.Pay_Type_ID + "\" checked></td>";
                }
                else
                {
                    way_list = way_list + "<input type=\"radio\" name=\"Orders_Paytype_ID\" id=\"Orders_Paytype_ID\" value=\"" + entity.Pay_Type_ID + "\" ></td>";
                }
                way_list = way_list + "  <td >" + entity.Pay_Type_Name + " </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //购物车价格更新
    public void Cart_Price_Update(int member_id)
    {
        double product_price = 0;
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmpsByOrdersID(-1);
        if (goodstmps != null)
        {
            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;
            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                //子商品信息
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);

                //正常产品
                if (entity.Orders_Goods_Type == 0)
                {
                    //获取产品信息
                    ProductInfo productinfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (productinfo != null)
                    {

                        product_price = productprice.Get_Member_Price(member_id, entity.Orders_Goods_Product_ID, productinfo.Product_Price);
                        if (productinfo.Product_IsGroupBuy == 1)
                        {
                            if (entity.Orders_Goods_Amount >= productinfo.Product_GroupNum)
                            {
                                product_price = productinfo.Product_GroupPrice;
                            }
                        }
                        entity.Orders_Goods_Product_Price = product_price;
                        entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(member_id, product_price);

                        Mygoodstmp.EditOrdersGoodsTmp(entity);

                    }
                }
                //检查套装
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                {
                    PackageInfo packageInfo = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (packageInfo != null)
                    {
                        if (packageInfo.Package_IsInsale == 1)
                        {
                            product_price = productprice.Get_Member_Price(member_id, 0, packageInfo.Package_Price);
                            entity.Orders_Goods_Product_Price = product_price;
                            entity.Orders_Goods_Product_Coin = productprice.Get_Member_Coin(member_id, product_price);

                            Mygoodstmp.EditOrdersGoodsTmp(entity);

                        }
                    }
                }
            }
        }
    }

    //生成订单号
    public string orders_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersInfo ordersinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.CreatevkeyNum(5);
        while (ismatch == true)
        {
            ordersinfo = MyBLL.GetOrdersBySN(sn);
            if (ordersinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.CreatevkeyNum(5);
            }
            else
            {
                ismatch = false;
            }
        }


        return sn;
    }

    //订单生成
    public void Orders_Create()
    {
        //判断购物车数量
        if (Cart_Product_Count() == 0)
        {
            Public.Msg("error", "错误提示", "请选择要购买的产品！", false, "ordercreate_1.aspx");
            Response.End();
        }

        //检查用户合法性
        int member_id;
        member_id = tools.CheckInt(Request["member_id"]);
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择购买用户！", false, "ordercreate_1.aspx");
            Response.End();
        }
        MemberInfo memberinfo = MyMEM.GetMemberByID(member_id, Public.GetUserPrivilege());
        if (member_id == null)
        {
            member_id = 0;
        }
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择购买用户！", false, "ordercreate_1.aspx");
            Response.End();
        }

        int Address_ID = 0;
        string Address_Province, Address_City, Address_District, Address_StreetAddress, Address_Zip, Address_Name, Address_Phone_Areacode, Address_Phone_Number, Address_Mobile;

        Address_ID = tools.CheckInt(Request["Orders_Address_ID"]);
        Address_Province = tools.CheckStr(Request["Orders_Address_State"]);
        Address_City = tools.CheckStr(Request["Orders_Address_City"]);
        Address_District = tools.CheckStr(Request["Orders_Address_County"]);
        Address_StreetAddress = tools.CheckStr(Request["Orders_Address_StreetAddress"]);
        Address_Zip = tools.CheckStr(Request["Orders_Address_Zip"]);
        Address_Name = tools.CheckStr(Request["Orders_Address_Name"]);
        Address_Phone_Number = tools.CheckStr(Request["Orders_Address_Phone_Number"]);
        Address_Mobile = tools.CheckStr(Request["Orders_Address_Mobile"]);

        if (Address_City == "" || Address_District == "" || Address_Province == "" || Address_Zip == "" || Address_StreetAddress == "" || Address_Name == "" || (Address_Phone_Number == "" && Address_Mobile == ""))
        {
            Public.Msg("error", "错误提示", "请将收货地址填写完整！", false, "{back}");
            Response.End();
        }

        if (Address_ID == 0)
        {
            MemberAddressInfo entity = new MemberAddressInfo();
            entity.Member_Address_ID = 0;
            entity.Member_Address_MemberID = member_id;
            entity.Member_Address_Country = Public.GetCurrentSite();
            entity.Member_Address_State = Address_Province;
            entity.Member_Address_City = Address_City;
            entity.Member_Address_County = Address_District;
            entity.Member_Address_StreetAddress = Address_StreetAddress;
            entity.Member_Address_Zip = Address_Zip;
            entity.Member_Address_Name = Address_Name;
            entity.Member_Address_Phone_Countrycode = "+86";
            entity.Member_Address_Phone_Areacode = "";
            entity.Member_Address_Phone_Number = Address_Phone_Number;
            entity.Member_Address_Mobile = Address_Mobile;
            entity.Member_Address_Site = Public.GetCurrentSite();

            MyAddr.AddMemberAddress(entity);
        }

        string Orders_Note = tools.CheckStr(Request["order_note"]);
        string Admin_Note = tools.CheckStr(Request["admin_note"]);

        int delivery_id, payway_id, deliverytime_id, delivery_cod;
        string delivery_name, payway_name;
        double delivery_fee, total_weight;

        delivery_id = tools.CheckInt(Request["Orders_Delivery_ID"]);
        payway_id = tools.CheckInt(Request["Orders_Payway_ID"]);
        deliverytime_id = tools.CheckInt(Request["Orders_DeliveryTime_ID"]);
        //total_weight = Get_Goodstmp_Weight(-1);

        //配送方式
        DeliveryWayInfo deliveryinfo = deliveryway.GetDeliveryWayByID(delivery_id);
        if (deliveryinfo != null)
        {
            delivery_id = deliveryinfo.Delivery_Way_ID;
            delivery_name = deliveryinfo.Delivery_Way_Name;
            delivery_cod = deliveryinfo.Delivery_Way_Cod;
            delivery_fee = Get_Cart_FreightFee(delivery_id, -1);
        }
        else
        {
            delivery_id = 0;
            delivery_name = "";
            delivery_cod = 0;
            delivery_fee = 0;
        }

        if (delivery_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择配送方式！", false, "{back}");
            Response.End();
        }

        //支付方式
        PayWayInfo paywayinfo = payway.GetPayWayByID(payway_id, Public.GetUserPrivilege());
        if (paywayinfo != null)
        {
            payway_id = paywayinfo.Pay_Way_ID;
            payway_name = paywayinfo.Pay_Way_Name;
        }
        else
        {
            payway_id = 0;
            payway_name = "";
        }

        if (payway_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择支付方式！", false, "{back}");
            Response.End();
        }

        //送货时间
        DeliveryTimeInfo deliverytimeinfo = MyDeliverytime.GetDeliveryTimeByID(deliverytime_id);
        if (deliverytimeinfo == null)
        {
            deliverytime_id = 0;
        }

        if (deliverytime_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择送货时间！", false, "{back}");
            Response.End();
        }

        int ticket_type;
        string ticket_title;
        ticket_type = tools.CheckInt(Request.Form["ticket_type"]);
        ticket_title = tools.CheckStr(Request.Form["ticket_title"]);


        if (ticket_type == 1 && ticket_title == "")
        {
            Public.Msg("error", "错误提示", "请填写发票抬头", false, "{back}");
            Response.End();
        }


        //保存订单
        int orders_id = 0;
        OrdersInfo order = new OrdersInfo();
        string sn = orders_sn();
        string Orders_VerifyCode = Public.Createvkey(64);
        order.Orders_SN = sn;
        order.Orders_BuyerID = member_id;
        order.Orders_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
        order.Orders_SourceType = 2;
        order.Orders_Source = Session["User_Name"].ToString();
        order.U_Orders_IsMonitor = 1;

        //订单状态
        order.Orders_Status = 0;
        order.Orders_ERPSyncStatus = 0;
        order.Orders_PaymentStatus = 0;
        order.Orders_DeliveryStatus = 0;
        order.Orders_DeliveryStatus_Time = DateTime.Now;
        order.Orders_Fail_Addtime = DateTime.Now;
        order.Orders_PaymentStatus_Time = DateTime.Now;
        if (ticket_type > 0)
        {
            order.Orders_InvoiceStatus = 0;
        }
        else
        {
            order.Orders_InvoiceStatus = 3;
        }
        order.Orders_Fail_SysUserID = 0;
        order.Orders_Fail_Note = "";
        order.Orders_IsReturnCoin = 0;

        //订单价格初始
        order.Orders_Total_MKTPrice = 0;
        order.Orders_Total_Price = 0;
        order.Orders_Total_Freight = 0;
        order.Orders_Total_Coin = 0;
        order.Orders_Total_UseCoin = 0;
        order.Orders_Total_AllPrice = 0;

        order.Orders_Admin_Sign = 0;
        order.Orders_Admin_Note = Admin_Note;
        order.Orders_Address_ID = Address_ID;
        order.Orders_Address_Country = Public.GetCurrentSite();
        order.Orders_Address_State = Address_Province;
        order.Orders_Address_City = Address_City;
        order.Orders_Address_County = Address_District;
        order.Orders_Address_StreetAddress = Address_StreetAddress;
        order.Orders_Address_Zip = Address_Zip;
        order.Orders_Address_Name = Address_Name;
        order.Orders_Address_Phone_Countrycode = "+86";
        order.Orders_Address_Phone_Areacode = "";
        order.Orders_Address_Phone_Number = Address_Phone_Number;
        order.Orders_Address_Mobile = Address_Mobile;

        order.Orders_Note = Orders_Note;
        order.Orders_Site = Public.GetCurrentSite();
        order.Orders_Addtime = DateTime.Now;

        order.Orders_Delivery_Time_ID = deliverytime_id;
        order.Orders_Delivery = delivery_id;
        order.Orders_Delivery_Name = delivery_name;

        order.Orders_Payway = payway_id;
        order.Orders_Payway_Name = payway_name;
        order.Orders_VerifyCode = Orders_VerifyCode;

        order.Orders_Total_FreightDiscount = 0;
        order.Orders_Total_FreightDiscount_Note = "";
        order.Orders_Total_PriceDiscount = 0;
        order.Orders_Total_PriceDiscount_Note = "";

        order.Orders_From = "";

        if (MyBLL.AddOrders(order))
        {
            OrdersInfo ordersinfo = MyBLL.GetOrdersBySN(sn);
            if (ordersinfo != null)
            {
                orders_id = ordersinfo.Orders_ID;

                //添加购物车产品到订单
                CartInfo cart = Orders_Goods_Add(orders_id);

                double total_allprice = 0;
                total_allprice = cart.Total_Product_Price + delivery_fee;
                if (total_allprice < 0)
                {
                    total_allprice = 0;
                }


                ordersinfo.Orders_Total_MKTPrice = cart.Total_Product_MktPrice;
                ordersinfo.Orders_Total_Price = cart.Total_Product_Price;
                ordersinfo.Orders_Total_Freight = delivery_fee;
                ordersinfo.Orders_Total_Coin = cart.Total_Product_Coin;
                ordersinfo.Orders_Total_UseCoin = cart.Total_Product_UseCoin;
                ordersinfo.Orders_Total_AllPrice = total_allprice;
                MyBLL.EditOrders(ordersinfo);

                order_save_invoicesave(orders_id);

                Mygoodstmp.ClearOrdersGoodsTmp(Session.SessionID);

                orderlog.Orders_Log(orders_id, Session["User_Name"].ToString(), "添加", "成功", "订单创建");

                if (ordersinfo.Orders_Admin_Note != "")
                {
                    orderlog.Orders_Log(orders_id, Session["User_Name"].ToString(), "修改管理员备注", "成功", "修改管理员备注为：" + ordersinfo.Orders_Admin_Note);
                }

                Response.Redirect("/ordercreate/ordercreate_1.aspx");
            }
        }
        Public.Msg("error", "错误信息", "订单生成失败！", false, "{back}");



    }

    //添加订单产品
    public CartInfo Orders_Goods_Add(int orders_id)
    {
        double total_mktprice, total_price;
        int total_coin, total_usecoin, parent_id;

        total_mktprice = 0;
        total_price = 0;
        total_coin = 0;
        total_usecoin = 0;
        OrdersGoodsInfo ordergoods = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        IList<OrdersGoodsTmpInfo> goodstmps = Mygoodstmp.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {

            IList<OrdersGoodsTmpInfo> GoodsList = OrdersGoodstmpSearch(goodstmps, 0);
            IList<OrdersGoodsTmpInfo> GoodsListSub = null;


            foreach (OrdersGoodsTmpInfo entity in GoodsList)
            {
                GoodsListSub = OrdersGoodstmpSearch(goodstmps, entity.Orders_Goods_ID);
                total_mktprice = total_mktprice + (entity.Orders_Goods_Product_MKTPrice * entity.Orders_Goods_Amount);
                total_price = total_price + (entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Price);
                total_coin = total_coin + (int)Math.Round(entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount);
                total_usecoin = total_usecoin + (int)Math.Round(entity.Orders_Goods_Amount * entity.Orders_Goods_Product_UseCoin);

                ordergoods = new OrdersGoodsInfo();
                ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                ordergoods.Orders_Goods_ParentID = entity.Orders_Goods_ParentID;
                ordergoods.Orders_Goods_OrdersID = orders_id;
                ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                ordergoods.Orders_Goods_Product_brokerage = entity.Orders_Goods_Product_brokerage;
                ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                MyBLL.AddOrdersGoods(ordergoods);

                parent_id = MyBLL.Get_Max_Goods_ID();
                if (parent_id > 0)
                {
                    Orders_SubGoods_Add(orders_id, entity.Orders_Goods_ID, parent_id, GoodsListSub);
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

    //添加订单子产品
    public void Orders_SubGoods_Add(int orders_id, int parent_ID, int New_parentid, IList<OrdersGoodsTmpInfo> goodstmps)
    {
        OrdersGoodsInfo ordergoods = null;
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {

                ordergoods = new OrdersGoodsInfo();
                ordergoods.Orders_Goods_Type = entity.Orders_Goods_Type;
                ordergoods.Orders_Goods_ParentID = New_parentid;
                ordergoods.Orders_Goods_OrdersID = orders_id;
                ordergoods.Orders_Goods_Product_ID = entity.Orders_Goods_Product_ID;
                ordergoods.Orders_Goods_Product_Code = entity.Orders_Goods_Product_Code;
                ordergoods.Orders_Goods_Product_CateID = entity.Orders_Goods_Product_CateID;
                ordergoods.Orders_Goods_Product_BrandID = entity.Orders_Goods_Product_BrandID;
                ordergoods.Orders_Goods_Product_Name = entity.Orders_Goods_Product_Name;
                ordergoods.Orders_Goods_Product_Img = entity.Orders_Goods_Product_Img;
                ordergoods.Orders_Goods_Product_Price = entity.Orders_Goods_Product_Price;
                ordergoods.Orders_Goods_Product_MKTPrice = entity.Orders_Goods_Product_MKTPrice;
                ordergoods.Orders_Goods_Product_Maker = entity.Orders_Goods_Product_Maker;
                ordergoods.Orders_Goods_Product_Spec = entity.Orders_Goods_Product_Spec;
                ordergoods.Orders_Goods_Product_AuthorizeCode = entity.Orders_Goods_Product_AuthorizeCode;
                ordergoods.Orders_Goods_Product_brokerage = entity.Orders_Goods_Product_brokerage;
                ordergoods.Orders_Goods_Product_Coin = entity.Orders_Goods_Product_Coin;
                ordergoods.Orders_Goods_Product_IsFavor = entity.Orders_Goods_Product_IsFavor;
                ordergoods.Orders_Goods_Product_UseCoin = entity.Orders_Goods_Product_UseCoin;
                ordergoods.Orders_Goods_Amount = entity.Orders_Goods_Amount;
                MyBLL.AddOrdersGoods(ordergoods);

                ordergoods = null;
            }
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
        MyInvoice.AddOrdersInvoice(invoice);
        invoice = null;
    }

    #endregion

    #region "采购报价下单"
    //订单商品清单
    public string Purchase_Goods_List(int Purchase_ID,int PriceReport_ID, bool ispreview)
    {
        StringBuilder strHTML = new StringBuilder();
        double goods_price = 0;
        strHTML.Append("");
        strHTML.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">");
        strHTML.Append("    <tr class=\"goods_head\">");
        if (ispreview)
        {
            strHTML.Append("		<td> 选择 </td>");
        }
        strHTML.Append("		<td> 商品名称 </td>");
        strHTML.Append("		<td> 规格/单位 </td>");
        strHTML.Append("		<td> 采购单价 </td>");
        strHTML.Append("		<td> 采购报价 </td>");
        strHTML.Append("		<td> 供应商 </td>");
        strHTML.Append("		<td> 商品数量 </td>");
        strHTML.Append("		<td> 交货时间 </td>");
        strHTML.Append("    </tr>");
        SupplierPurchaseInfo purchaseinfo = supplier.GetSupplierPurchaseByID(Purchase_ID);
        if (purchaseinfo == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //获取卖家名称信息
        int SupplyID=0;
        SupplierPriceReportInfo pricereport = supplier.GetSupplierPriceReportByID(PriceReport_ID);
        if (pricereport != null)
        {
            SupplyID = pricereport.PriceReport_MemberID;
        }
        SupplierInfo sinfo = null;
        if (SupplyID > 0)
        {
            sinfo = supplier.GetSupplierByID(SupplyID);
        }
        string Supplier_name = "";
        if (sinfo != null)
        {
            Supplier_name = sinfo.Supplier_CompanyName;
        }
        string Goods_ID = tools.CheckStr(Request["Goods_ID"]);
        if (Goods_ID.Length == 0)
        {
            Goods_ID = "0";
        }

        //获取采购清单信息
        IList<SupplierPriceReportDetailInfo> reports = MyReportDetail.GetSupplierPriceReportDetailsByPriceReportID(PriceReport_ID);
        IList<SupplierPurchaseDetailInfo> goodstmps;
        if (ispreview)
        {
            goodstmps = MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(Purchase_ID);
        }
        else
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_PurchaseID", "=", Purchase_ID.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_ID", "in", Goods_ID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseDetailInfo.Detail_ID", "Desc"));
            goodstmps = MyPurchaseDetail.GetSupplierPurchaseDetails(Query);
        }
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

                            strHTML.Append("    <tr class=\"goods_list\">");
                            if (ispreview)
                            {
                                strHTML.Append("<td align=\"center\"><input type=\"checkbox\" name=\"Goods_ID\" value=\"" + entity.Detail_ID + "\" checked></td>");
                            }
                            else
                            {
                                strHTML.Append("<td align=\"center\" style=\"display:none;\"><input type=\"checkbox\" name=\"Goods_ID\" value=\"" + entity.Detail_ID + "\" checked></td>");
                            }
                            strHTML.Append("<td class=\"td6\" style=\"text-align:left;padding:0px 5px;line-height:20px;\">" + entity.Detail_Name + "</td>");
                            strHTML.Append("		<td>" + entity.Detail_Spec + "</td>");
                            strHTML.Append("		<td class=\"price_list\">" + Public.DisplayCurrency(entity.Detail_Price) + " </td>");
                            strHTML.Append("		<td class=\"price_list\">" + Public.DisplayCurrency(report.Detail_Price) + " </td>");
                            strHTML.Append("		<td>" + Supplier_name + "</td>");
                            strHTML.Append("		<td>" + report.Detail_Amount + "</td>");
                            strHTML.Append("		<td>" + purchaseinfo.Purchase_DeliveryTime.ToString("yyyy-MM-dd") + "</td>");


                            strHTML.Append("    </tr>");
                            Session["total_price"] = tools.CheckFloat(Session["total_price"].ToString()) + ((report.Detail_Amount * report.Detail_Price));
                            break;
                        }
                    }
                }
                else
                {
                    Response.Redirect("/supplier/supplier_pricereport_list.aspx");
                }  
            }
        }
        else
        {
            Response.Redirect("/supplier/supplier_pricereport_list.aspx");
        }
        strHTML.Append("</table>");
        goodstmps = null;
        return strHTML.ToString();
    }

    //订单生成
    public void Purchase_Orders_Create()
    {
        //申请报价验证
        string Goods_ID = tools.CheckStr(Request["Goods_ID"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

        if (PriceReport_ID == 0 || Purchase_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        if (Goods_ID.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择要采购的商品信息", false, "purchase_orders_add.aspx?PriceReport_ID=" + PriceReport_ID);
        }
        SupplierPriceReportInfo entity = supplier.GetSupplierPriceReportByID(PriceReport_ID);
        SupplierPurchaseInfo spinfo = null;
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "报价记录不存在", false, "{back}");
            Response.End();
        }
        if (entity.PriceReport_AuditStatus != 1)
        {
            Public.Msg("error", "错误信息", "当前报价信息未审核通过！", false, "{back}");
            Response.End();
        }
        if (entity.PriceReport_MemberID == 0)
        {
            Public.Msg("error", "错误信息", "不能针对平台报价进行采购！", false, "{back}");
            Response.End();
        }
        spinfo = supplier.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
        if (spinfo == null)
        {
            Public.Msg("error", "错误信息", "采购申请记录不存在", false, "{back}");
            Response.End();
        }
        //采购申请未审核通过或已删除或已过期
        if (spinfo.Purchase_Trash == 1 || spinfo.Purchase_Status != 2 || spinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
        {
            Public.Msg("error", "错误信息", "无效的采购申请信息！", false, "{back}");
            Response.End();
        }
        //检查报价明细
        IList<SupplierPriceReportDetailInfo> reports = MyReportDetail.GetSupplierPriceReportDetailsByPriceReportID(PriceReport_ID);
        if (reports == null)
        {
            Public.Msg("error", "错误信息", "报价信息中不包括商品信息！", false, "{back}");
            Response.End();
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
            Public.Msg("error", "错误信息", "请选择要采购的商品信息", false, "purchase_orders_add.aspx?PriceReport_ID=" + PriceReport_ID);
        }

        //检查用户合法性
        int member_id;
        member_id = entity.PriceReport_MemberID;
        SupplierInfo supplierinfo = supplier.GetSupplierByID(member_id);
        if (member_id == null)
        {
            member_id = 0;
        }
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "报价用不存在！", false, "{back}");
            Response.End();
        }

        int Address_ID = 0;
        string Address_Province, Address_City, Address_District, Address_StreetAddress, Address_Zip, Address_Name, Address_Phone_Areacode, Address_Phone_Number, Address_Mobile;

        Address_ID = tools.CheckInt(Request["Orders_Address_ID"]);
        Address_Province = tools.CheckStr(Request["Orders_Address_State"]);
        Address_City = tools.CheckStr(Request["Orders_Address_City"]);
        Address_District = tools.CheckStr(Request["Orders_Address_County"]);
        Address_StreetAddress = tools.CheckStr(Request["Orders_Address_StreetAddress"]);
        Address_Zip = tools.CheckStr(Request["Orders_Address_Zip"]);
        Address_Name = tools.CheckStr(Request["Orders_Address_Name"]);
        Address_Phone_Number = tools.CheckStr(Request["Orders_Address_Phone_Number"]);
        Address_Mobile = tools.CheckStr(Request["Orders_Address_Mobile"]);

        if (Address_City == "" || Address_District == "" || Address_Province == "" || Address_Zip == "" || Address_StreetAddress == "" || Address_Name == "" || (Address_Phone_Number == "" && Address_Mobile == ""))
        {
            Public.Msg("error", "错误提示", "请将收货地址填写完整！", false, "{back}");
            Response.End();
        }

        

        string Orders_Note = tools.CheckStr(Request["order_note"]);

        int delivery_id, payway_id, Orders_Paytype_ID, delivery_cod;
        string delivery_name, payway_name;
        double delivery_fee, total_weight;
        delivery_fee = 0;
        delivery_id = tools.CheckInt(Request["Orders_Delivery_ID"]);
        payway_id = tools.CheckInt(Request["Orders_Payway_ID"]);
        Orders_Paytype_ID = tools.CheckInt(Request["Orders_Paytype_ID"]);

        

        //支付方式
        PayWayInfo paywayinfo = payway.GetPayWayByID(payway_id, Public.GetUserPrivilege());
        if (paywayinfo != null)
        {
            payway_id = paywayinfo.Pay_Way_ID;
            payway_name = paywayinfo.Pay_Way_Name;
        }
        else
        {
            payway_id = 0;
            payway_name = "";
        }

        if (payway_id == 0)
        {
            Public.Msg("error", "错误提示", "请选择支付方式！", false, "{back}");
            Response.End();
        }

        //付款条件
        PayTypeInfo paytype = MyPayType.GetPayTypeByID(Orders_Paytype_ID, Public.GetUserPrivilege());
        if (paytype == null)
        {
            Orders_Paytype_ID = 0;
        }

        if (Orders_Paytype_ID == 0)
        {
            Public.Msg("error", "错误提示", "请选择付款条件！", false, "{back}");
            Response.End();
        }

       

        //保存订单
        int orders_id = 0;
        OrdersInfo order = new OrdersInfo();
        string sn = orders_sn();
        string Orders_VerifyCode = Public.Createvkey(64);
        order.Orders_SN = sn;
        if (spinfo.Purchase_TypeID == 0)
        {
            order.Orders_Type = 2;  //定制采购订单
        }
        else
        {
            order.Orders_Type = 3;  //代理采购订单
        }
        order.Orders_BuyerID = 0;
        order.Orders_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
        order.Orders_SourceType = 2;
        order.Orders_Source = Session["User_Name"].ToString();
        order.U_Orders_IsMonitor = 1;

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

        //订单价格初始
        order.Orders_Total_MKTPrice = 0;
        order.Orders_Total_Price = 0;
        order.Orders_Total_Freight = 0;
        order.Orders_Total_Coin = 0;
        order.Orders_Total_UseCoin = 0;
        order.Orders_Total_AllPrice = 0;

        order.Orders_Admin_Sign = 0;
        order.Orders_Admin_Note = "";
        order.Orders_Address_ID = Address_ID;
        order.Orders_Address_Country = Public.GetCurrentSite();
        order.Orders_Address_State = Address_Province;
        order.Orders_Address_City = Address_City;
        order.Orders_Address_County = Address_District;
        order.Orders_Address_StreetAddress = Address_StreetAddress;
        order.Orders_Address_Zip = Address_Zip;
        order.Orders_Address_Name = Address_Name;
        order.Orders_Address_Phone_Countrycode = "+86";
        order.Orders_Address_Phone_Areacode = "";
        order.Orders_Address_Phone_Number = Address_Phone_Number;
        order.Orders_Address_Mobile = Address_Mobile;

        order.Orders_Note = Orders_Note;
        order.Orders_Site = Public.GetCurrentSite();
        order.Orders_Addtime = DateTime.Now;

        order.Orders_Delivery_Time_ID = 0;
        order.Orders_Delivery = delivery_id;
        order.Orders_Delivery_Name = "";

        order.Orders_Payway = payway_id;
        order.Orders_Payway_Name = payway_name;
        order.Orders_VerifyCode = Orders_VerifyCode;
        order.Orders_PayType = paytype.Pay_Type_ID;
        order.Orders_PayType_Name = paytype.Pay_Type_Name;

        order.Orders_Total_FreightDiscount = 0;
        order.Orders_Total_FreightDiscount_Note = "";
        order.Orders_Total_PriceDiscount = 0;
        order.Orders_Total_PriceDiscount_Note = "";

        order.Orders_From = "";
        order.Orders_SupplierID = entity.PriceReport_MemberID;
        order.Orders_PurchaseID = spinfo.Purchase_ID;
        order.Orders_PriceReportID = entity.PriceReport_ID;
        if (MyBLL.AddOrders(order))
        {
            OrdersInfo ordersinfo = MyBLL.GetOrdersBySN(sn);
            if (ordersinfo != null)
            {
                orders_id = ordersinfo.Orders_ID;

                //添加购物车产品到订单
                CartInfo cart = BuyOrders_Goods_Add(orders_id, entity.PriceReport_MemberID, entity.PriceReport_DeliveryDate.ToString("yyyy-MM-dd"), goodstmps, reports);

                double total_allprice = 0;
                total_allprice = cart.Total_Product_Price + delivery_fee;
                if (total_allprice < 0)
                {
                    total_allprice = 0;
                }


                ordersinfo.Orders_Total_MKTPrice = cart.Total_Product_MktPrice;
                ordersinfo.Orders_Total_Price = cart.Total_Product_Price;
                ordersinfo.Orders_Total_Freight = delivery_fee;
                ordersinfo.Orders_Total_Coin = cart.Total_Product_Coin;
                ordersinfo.Orders_Total_UseCoin = cart.Total_Product_UseCoin;
                ordersinfo.Orders_Total_AllPrice = total_allprice;
                MyBLL.EditOrders(ordersinfo);



                orderlog.Orders_Log(orders_id, Session["User_Name"].ToString(), "添加", "成功", "订单创建");


                Response.Redirect("/orders/orders_view.aspx?Orders_ID="+orders_id);
            }
        }
        Public.Msg("error", "错误信息", "订单生成失败！", false, "{back}");
    }

    //添加订单商品
    public CartInfo BuyOrders_Goods_Add(int orders_id, int Supplier_ID, string Delivery_Time, IList<SupplierPurchaseDetailInfo> goodstmps, IList<SupplierPriceReportDetailInfo> reports)
    {
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
                            product_coin = 0;

                            total_mktprice = total_mktprice + (report.Detail_Amount * report.Detail_Price);
                            total_price = total_price + (report.Detail_Amount * report.Detail_Price);
                            total_coin += product_coin;
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
                            MyBLL.AddOrdersGoods(ordergoods);
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
    #endregion


}
