using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Text.RegularExpressions;



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
using Glaer.Trade.Util.SQLHelper;

/// <summary>
/// 订单处理类
/// </summary>
public class Orders
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IOrders MyBLL;
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
    private IPromotionFavorFee MyFavorFee;
    private Addr Addr;
    private IMail mail;
    private IPromotionFavorCoupon MyCoupon;
    private IPromotionFavorPolicy MyPolicy;
    private IOrdersBackApply MyBack;
    private ISupplier MySupplier;
    private IPromotionFavor MyFavor;
    private IMemberGrade MyGrade;
    private IOrdersBackApplyProduct MyBackProduct;
    private IPayType paytype;

    Member member = new Member();


    public Orders()
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
        MyFavorFee = PromotionFavorFeeFactory.CreatePromotionFavorFee();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();
        MyPolicy = PromotionFavorPolicyFactory.CreatePromotionFavorPolicy();
        mail = MailFactory.CreateMail();
        MyBack = OrdersBackApplyFactory.CreateOrdersBackApply();
        MySupplier = SupplierFactory.CreateSupplier();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyGrade = MemberGradeFactory.CreateMemberGrade();
        MyBackProduct = OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct();
        paytype = PayTypeFactory.CreatePayType();
        Addr = new Addr();
    }

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

    public string OrdersStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "<span class=\"status_red\">未确认</span>"; break;
            case 1:
                resultstr = "<span class=\"status_red\">处理中</span>"; break;
            case 2:
                resultstr = "<span class=\"status_green\">交易成功</span>"; break;
            case 3:
                resultstr = "<span class=\"status_red\">交易失败</span>"; break;
            case 4:
                resultstr = "<span class=\"status_red\">申请退换货</span>"; break;
            case 5:
                resultstr = "<span class=\"status_red\">申请退款</span>"; break;
        }
        return resultstr;
    }

    public string PaymentStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "<span class=\"status_red\">未支付</span>"; break;
            case 1:
                resultstr = "<span class=\"status_green\">已支付</span>"; break;
            case 2:
                resultstr = "<span class=\"status_red\">已退款</span>"; break;
            case 3:
                resultstr = "<span class=\"status_red\">退款处理中</span>"; break;
            case 4:
                resultstr = "<span class=\"status_green\">已到帐</span>"; break;
        }
        return resultstr;
    }

    public string DeliveryStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "<span class=\"status_red\">待采购</span>"; break;
            case 1:
                resultstr = "<span class=\"status_green\">已发货</span>"; break;
            case 2:
                resultstr = "<span class=\"status_green\">已签收</span>"; break;
            case 3:
                resultstr = "<span class=\"status_red\">已拒收</span>"; break;
            case 4:
                resultstr = "<span class=\"status_red\">退货处理中</span>"; break;
            case 5:
                resultstr = "<span class=\"status_red\">已退货</span>"; break;
            case 6:
                resultstr = "<span class=\"status_red\">配货中</span>"; break;

        }
        return resultstr;
    }

    public string InvoiceStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "<span class=\"status_red\">未开票</span>"; break;
            case 1:
                resultstr = "<span class=\"status_green\">已开票</span>"; break;
            case 2:
                resultstr = "<span class=\"status_red\">已退票</span>"; break;
            case 3:
                resultstr = "<span class=\"status_red\">不需要发票</span>"; break;
        }

        return resultstr;
    }

    public string OrdersSettlingStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "<span class=\"status_red\">未结算</span>"; break;
            case 1:
                resultstr = "<span class=\"status_red\">申请结算</span>"; break;
            case 2:
                resultstr = "<span class=\"status_green\">已结算</span>"; break;
        }

        return resultstr;
    }

    #endregion

    public OrdersInfo GetOrdersByID(int ID)
    {
        return MyBLL.GetOrdersByID(ID);
    }

    public OrdersInfo GetOrdersBySn(string code)
    {
        return MyBLL.GetOrdersBySN(code);
    }

    public IList<OrdersGoodsTmpInfo> Get_Orders_Carts(int Orders_ID)
    {
        IList<OrdersGoodsInfo> OrderGoods = MyBLL.GetGoodsListByOrderID(Orders_ID);
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

    //订单列表
    public string GetOrdersList()
    {
        int member_id = tools.CheckInt(Request.QueryString["member_id"]);
        int orders_status = tools.CheckInt(Request.QueryString["orders_status"]);
        int orders_source = tools.CheckInt(Request.QueryString["orders_source"]);
        int orders_paymentstatus = tools.CheckInt(Request.QueryString["orders_paymentstatus"]);
        int orders_deliverystatus = tools.CheckInt(Request.QueryString["orders_deliverystatus"]);
        int orders_invoicestatus = tools.CheckInt(Request.QueryString["orders_invoicestatus"]);
        string keyword = tools.CheckStr(Request.QueryString["keyword"]);
        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        int Orders_Type = tools.CheckInt(Request.QueryString["Orders_Type"]);
        string Supplier_ID = Request.QueryString["Supplier_ID"];
        if (Supplier_ID == null || Supplier_ID.Length == 0)
            Supplier_ID = "-1";
        else
            Supplier_ID = tools.CheckInt(Supplier_ID).ToString();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Site", "=", Public.GetCurrentSite()));

        string product_id = "0";

        if (Supplier_ID == "0")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", "=", "0"));
        else if (Supplier_ID == "1")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SupplierID", ">", "0"));

        if (orders_status != -1) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Status", "=", orders_status.ToString())); }
        if (orders_source != 0) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_SourceType", "=", orders_source.ToString())); }
        if (orders_paymentstatus != -1) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_PaymentStatus", "=", orders_paymentstatus.ToString())); }
        if (orders_deliverystatus != -1) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_DeliveryStatus", "=", orders_deliverystatus.ToString())); }
        if (orders_invoicestatus != -1) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_InvoiceStatus", "=", orders_invoicestatus.ToString())); }
        if (Orders_Type > 0) { Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Type", "=", Orders_Type.ToString())); }
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "OrdersInfo.Orders_SN", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_Address_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_Address_Mobile", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_ID", "in", "Select Orders_Goods_OrdersID From Orders_Goods where Orders_Goods_Product_Code like '%" + keyword + "%' or Orders_Goods_Product_Name like '%"+keyword+"%'"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersInfo.Orders_Address_Phone_Number", "like", keyword));
        }
        if (member_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", member_id.ToString()));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{OrdersInfo.Orders_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{OrdersInfo.Orders_Addtime})", "<=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);
        IList<OrdersInfo> entitys = MyBLL.GetOrderss(Query);
        Query = null;
        if (entitys != null)
        {
            Sources sources = new Sources();
            SupplierInfo supplierinfo = null;
            StringBuilder jsonBuilder = new StringBuilder();
            string Supplier_CompanyName = "";
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Orders_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_SN);
                jsonBuilder.Append("\",");
              
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_Total_AllPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Addtime.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");

                if (orders_source != 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Orders_Source);
                    jsonBuilder.Append("\",");
                }

                Supplier_CompanyName = "易耐产业金服";
                if (entity.Orders_SupplierID > 0)
                {
                    supplierinfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, Public.GetUserPrivilege());
                    if (supplierinfo != null)
                    {
                        Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
                    }
                    else {
                        Supplier_CompanyName = "--";
                    }
                }
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Orders_Address_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Supplier_CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_Payway_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(OrdersStatus(entity.Orders_Status).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Orders_Status < 1 && entity.Orders_PaymentStatus == 0 && entity.Orders_DeliveryStatus == 0 && Public.CheckPrivilege("c7f19325-8f6e-419a-a276-a6a4dee0517d"))
                {
                    jsonBuilder.Append("<a href=\\\"orders_edit.aspx?orders_id=" + entity.Orders_ID + "\\\"><img src=\\\"/images/icon_edit.gif\\\" alt=\\\"编辑\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"> 编辑</a>");
                }
                if (Public.CheckPrivilege("5e807815-409c-4d01-8e1a-2f835fbf2ac5"))
                {
                    jsonBuilder.Append(" <a href=\\\"orders_view.aspx?orders_id=" + entity.Orders_ID + "\\\"><img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" border=\\\"0\\\" align=\\\"absmiddle\\\"> 查看</a>");
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

    public string GetOrdersGoodsByOrdersID(int Orders_ID)
    {
        int i = 0;
        string strHTML = "";
        bool Is_Savebatch = false;
        bool Is_Savebuy = false;
        int stock = 0;
        OrdersInfo ordersinfo = new OrdersInfo();
        ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo == null)
        {
            return strHTML;
        }
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        ProductStockInfo productstockinfo = new ProductStockInfo();
        //列出商品信息
        IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
        strHTML += "<form name=\"frm_batch\" action=\"orders_do.aspx\" method=\"post\">";
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">";
        strHTML += "    <tr class=\"goods_head\">";
        strHTML += "        <td>商品编号</td>";
        strHTML += "        <td>名称</td>";
        strHTML += "        <td>规格</td>";
        //strHTML += "        <td>生产企业</td>";
        strHTML += "        <td>交货期</td>";
        //strHTML += "        <td>市场价</td>";
        strHTML += "        <td>单价</td>";
        strHTML += "        <td>数量</td>";
        //strHTML += "        <td>获赠" + Application["Coin_Name"].ToString() + "</td>";
        strHTML += "        <td>总价</td>";
        //if (ordersinfo.Orders_Type == 1)
        //{
        //    strHTML += "        <td>可用库存</td>";
        //}
        strHTML += "    </tr>";
        if (GoodsListAll != null)
        {
            IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(GoodsListAll, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;

            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                stock = 0;
                packagestockinfo.Product_Stock_Amount = 0;
                packagestockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_Amount = 0;
                //子商品信息
                GoodsListSub = OrdersGoodsSearch(GoodsListAll, entity.Orders_Goods_ID);

                strHTML += "    <tr class=\"goods_list\">";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
                strHTML += "        <td><table align=\"left\">";
                strHTML += "<tr>";

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
                //strHTML += "        <td>" + entity.Orders_Goods_Product_Maker + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Product_DeliveryDate + "</td>";
                
                //strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Amount + "</td>";
                //strHTML += "        <td>" + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                //if (ordersinfo.Orders_Type == 1)
                //{
                //    if (entity.Orders_Goods_Type != 2)
                //    {
                //        productstockinfo = Get_Productcount(entity.Orders_Goods_Product_ID);
                //        stock = productstockinfo.Product_Stock_Amount;

                //        if (stock <= 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                //        {
                //            strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                //        }
                //        else if (stock > 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                //        {
                //            strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                //        }
                //        else if (productstockinfo.Product_Stock_IsNoStock == 1)
                //        {
                //            strHTML += "        <td class=\"t12_green\">零库存</td>";
                //        }
                //    }
                //    else
                //    {

                //        PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                //        if (package != null)
                //        {
                //            IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                //            if (packageproducts != null)
                //            {

                //                packagestockinfo = Get_Package_Count(packageproducts);
                //                stock = packagestockinfo.Product_Stock_Amount;
                //            }
                //        }
                //        if (stock <= 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                //        {
                //            strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                //        }
                //        else if (stock > 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                //        {
                //            strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                //        }
                //        else if (packagestockinfo.Product_Stock_IsNoStock == 1)
                //        {
                //            strHTML += "        <td class=\"t12_green\">零库存</td>";
                //        }

                //    }
                //}

                strHTML += "    </tr>";

                if (GoodsListSub.Count > 0)
                {
                    i = 0;
                    //strHTML += "    <tr id=\"subgoods_" + entity.Orders_Goods_ID + "\" class=\"goods_list\" style=\"display:none;\">";
                    //strHTML += "        <td colspan=\"14\"><table width=\"100%\">";
                    foreach (OrdersGoodsInfo goodsSub in GoodsListSub)
                    {
                        i = i + 1;
                        strHTML += "    <tr class=\"goods_list\" id=\"subgoods_" + entity.Orders_Goods_ID + "_" + i + "\">";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Code + "</td>";
                        strHTML += "        <td><table align=\"left\">";
                        strHTML += "<tr>";

                        if (goodsSub.Orders_Goods_Type == 1)
                        {
                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + goodsSub.Orders_Goods_Product_Img + "\" /></td>";
                            strHTML += "    <td><span class=\"t12_red\">[赠品]</span> " + goodsSub.Orders_Goods_Product_Name + "</td>";
                        }
                        else
                        {

                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + Public.FormatImgURL(goodsSub.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
                            strHTML += "    <td>" + goodsSub.Orders_Goods_Product_Name;
                            strHTML += "</td>";
                        }

                        strHTML += "</tr>";
                        strHTML += "</table></td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Spec + "</td>";
                        //strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Maker + "</td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_DeliveryDate + "</td>";
                        
                        //strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_MKTPrice) + "</td>";
                        strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_Price) + "</td>";
                        strHTML += "        <td>1×" + (goodsSub.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "</td>";
                        strHTML += "        <td>--</td>";
                        //strHTML += "        <td>--</td>";
                        //if (ordersinfo.Orders_Type == 1)
                        //{
                        //    strHTML += "        <td>--</td>";
                        //}

                        strHTML += "    </tr>";
                    }
                }
                GoodsListSub = null;

            }
        }
        strHTML += "</table>";
        GoodsListAll = null;


        //底部信息显示

        strHTML += "<table border=\"0\" width=\"100%\" cellpadding=\"3\" cellspacing=\"1\">";
        strHTML += "<tr bgcolor=\"#ffffff\">";
        

        strHTML += "<td align=\"right\"><input type=\"hidden\" name=\"orders_id\" value=\"" + Orders_ID + "\"><input type=\"hidden\" id=\"action\" name=\"action\" value=\"save_batch\">";
        strHTML += "<div style=\"text-align:right;margin:5px;\">产品价格 <span class=\"price_list\">" + Public.DisplayCurrency(ordersinfo.Orders_Total_Price) + "</span></div>";
        //strHTML += "<div style=\"text-align:right;margin:5px;\">运费 <span class=\"price_list\">" + Public.DisplayCurrency(ordersinfo.Orders_Total_Freight) + "</span></div>";
        strHTML += "<div style=\"text-align:right;margin:5px;\">价格优惠 <span class=\"price_list\">-" + Public.DisplayCurrency(ordersinfo.Orders_Total_PriceDiscount) + "</span></div>";
        //strHTML += "<div style=\"text-align:right;margin:5px;\">运费优惠 <span class=\"price_list\">-" + Public.DisplayCurrency(ordersinfo.Orders_Total_FreightDiscount) + "</span></div>";
        strHTML += "<div style=\"text-align:right;margin:5px;\">总价(含运费) <span class=\"price_list_big\">" + Public.DisplayCurrency(ordersinfo.Orders_Total_AllPrice) + "</span></div>";
        ordersinfo = null;

        strHTML += "</td></tr></table>";
        strHTML += "</form>";
        return strHTML;
    }

    public IList<OrdersGoodsInfo> GetGoodsListByOrderID(int Orders_ID)
    {
        return MyBLL.GetGoodsListByOrderID(Orders_ID); 
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

    public string Order_Detail_Goods_Mail(string Orders_SN)
    {
        int orders_id = 0;
        string strHTML = "";
        OrdersInfo ordersinfo = MyBLL.GetOrdersBySN(Orders_SN);
        if (ordersinfo != null)
        {
            orders_id = ordersinfo.Orders_ID;
            double total_price = ordersinfo.Orders_Total_Price;
            double Total_Freight = ordersinfo.Orders_Total_Freight;
            double Orders_Total_PriceDiscount = ordersinfo.Orders_Total_PriceDiscount;
            double Orders_Total_FreightDiscount = ordersinfo.Orders_Total_FreightDiscount;
            double Total_AllPrice = ordersinfo.Orders_Total_AllPrice;
            IList<OrdersGoodsInfo> entitys = MyBLL.GetGoodsListByOrderID(orders_id);
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
                            strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                            strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"" + tools.NullStr(Application["site_url"]) + "/product/detail.aspx?product_id=" + entity.Orders_Goods_Product_ID + "\"><strong>" + entity.Orders_Goods_Product_Name + "</strong></a></td>";
                        }
                    }
                    else
                    {
                        strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                        strHTML = strHTML + "              <td><strong>" + entity.Orders_Goods_Product_Name + "</strong></td>";
                    }
                    strHTML = strHTML + "            </tr>";
                    strHTML = strHTML + "          </table></td>";
                    strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Spec + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Product_Maker + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Amount + "</td>";
                    strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + entity.Orders_Goods_Amount * entity.Orders_Goods_Product_Coin + "</td>";
                    strHTML = strHTML + "          <td align=\"right\" bgcolor=\"#FFFFFF\" class=\"price_small\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
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

                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><span class=\"t12_red\">[赠品]</span> <a class=\"a_t12_black\" href=\"/product/detail.aspx?product_id=" + ent.Orders_Goods_Product_ID + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
                                }
                                else
                                {
                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
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

                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td align=\"left\" class=\"t12_black\"><a class=\"a_t12_black\" href=\"/product/detail.aspx?product_id=" + ent.Orders_Goods_Product_ID + "\"><strong>" + ent.Orders_Goods_Product_Name + "</strong></a></td>";
                                }
                                else
                                {
                                    strHTML = strHTML + "              <td width=\"42\" height=\"42\" align=\"center\" class=\"img_border\" bgcolor=\"#FFFFFF\"><img src=\"" + Public.FormatImgURL(ent.Orders_Goods_Product_Img, "thumbnail") + "\" width=\"36\" height=\"36\" border=\"0\" onload=\"javascript:AutosizeImage(this,36,36);\" /></td>";
                                    strHTML = strHTML + "              <td><strong>" + ent.Orders_Goods_Product_Name + "</strong></td>";
                                }
                                strHTML = strHTML + "            </tr>";
                                strHTML = strHTML + "          </table></td>";
                            }
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + ent.Orders_Goods_Product_Spec + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + ent.Orders_Goods_Product_Maker + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" class=\"t12_black\" bgcolor=\"#FFFFFF\">" + Public.DisplayCurrency(ent.Orders_Goods_Product_MKTPrice) + "</td>";
                            strHTML = strHTML + "          <td align=\"center\" bgcolor=\"#FFFFFF\">" + Public.DisplayCurrency(ent.Orders_Goods_Product_Price) + "</td>";
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
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">产品总价 <span class=\"price_small\">" + Public.DisplayCurrency(total_price) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">运费 <span class=\"price_small\">" + Public.DisplayCurrency(Total_Freight) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                if (Orders_Total_PriceDiscount > 0)
                {
                    strHTML = strHTML + "          <tr>";
                    strHTML = strHTML + "            <td align=\"right\" class=\"t12\">价格优惠 <span class=\"price_small\">-" + Public.DisplayCurrency(Orders_Total_PriceDiscount) + "</span></td>";
                    strHTML = strHTML + "          </tr>";
                }
                if (Orders_Total_FreightDiscount > 0)
                {
                    strHTML = strHTML + "          <tr>";
                    strHTML = strHTML + "            <td align=\"right\" class=\"t12\">运费优惠 <span class=\"price_small\">-" + Public.DisplayCurrency(Orders_Total_FreightDiscount) + "</span></td>";
                    strHTML = strHTML + "          </tr>";
                }
                strHTML = strHTML + "          <tr>";
                strHTML = strHTML + "            <td align=\"right\" class=\"t12\">总价（含运费） <span class=\"price\">" + Public.DisplayCurrency(Total_AllPrice) + "</span></td>";
                strHTML = strHTML + "          </tr>";
                strHTML = strHTML + "        </table>";
            }
        }
        return strHTML;
    }

    //根据编号获取送货时间
    public DeliveryTimeInfo GetDeliveryTimeByID(int ID)
    {
        return MyDeliverytime.GetDeliveryTimeByID(ID);
    }

    //保存订单产品批号
    public void Orders_Goods_Batchcode_Save()
    {
        int Orders_ID = tools.CheckInt(Request["orders_id"]);
        string batch_code = "";
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus == 6 && ordersinfo.Orders_Status < 2)
            {
                IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo entity in GoodsListAll)
                    {
                        if ((entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0 && entity.U_Orders_Goods_Product_BatchCode == "") || (entity.Orders_Goods_Type != 2 && entity.Orders_Goods_ParentID == 0 && entity.U_Orders_Goods_Product_BatchCode == ""))
                        {
                            batch_code = tools.CheckStr(Request["product_batchcode_" + entity.Orders_Goods_ID]);
                            if (batch_code != "")
                            {
                                entity.U_Orders_Goods_Product_BatchCode = batch_code;
                                MyBLL.EditOrdersGoods(entity);
                                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "设置产品批号", "成功", "设置产品：" + entity.Orders_Goods_Product_Name + " 批号为：" + batch_code);
                            }
                        }
                    }
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //保存订单产品采购信息
    public void Orders_Goods_Buy_Save()
    {
        int Orders_ID = tools.CheckInt(Request["orders_id"]);
        string buychannel = "";
        int buyamount = 0;
        double buyprice = 0;
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus < 6 && ordersinfo.Orders_Status == 1)
            {
                IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo entity in GoodsListAll)
                    {
                        if ((entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0 && entity.U_Orders_Goods_Product_BuyChannel == "") || (entity.Orders_Goods_Type != 2 && entity.Orders_Goods_ParentID == 0 && entity.U_Orders_Goods_Product_BuyChannel == ""))
                        {
                            buychannel = tools.CheckStr(Request["product_buychannel_" + entity.Orders_Goods_ID]);
                            buyamount = tools.CheckInt(Request["product_buyamount_" + entity.Orders_Goods_ID]);
                            buyprice = tools.CheckFloat(Request["product_buyprice_" + entity.Orders_Goods_ID]);
                            if (buychannel != "")
                            {
                                entity.U_Orders_Goods_Product_BuyChannel = buychannel;
                                entity.U_Orders_Goods_Product_BuyAmount = buyamount;
                                entity.U_Orders_Goods_Product_BuyPrice = buyprice;
                                MyBLL.EditOrdersGoods(entity);
                                //orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "设置产品批号", "成功", "设置产品：" + entity.Orders_Goods_Product_Name + " 批号为：" + batch_code);
                            }
                        }
                    }
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //检查订单产品批号是否全部填写
    public bool Check_Orders_Goods_Batchcode(int Orders_ID)
    {
        bool Is_All = true;
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus == 6 && ordersinfo.Orders_Status < 2)
            {
                IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo entity in GoodsListAll)
                    {
                        if ((entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0 && entity.U_Orders_Goods_Product_BatchCode == "") || (entity.Orders_Goods_Type != 2 && entity.Orders_Goods_ParentID == 0 && entity.U_Orders_Goods_Product_BatchCode == ""))
                        {
                            Is_All = false;
                        }
                    }
                }
            }
        }
        return Is_All;
    }

    //检查订单产品采购信息是否全部填写
    public bool Check_Orders_Goods_Buy(int Orders_ID)
    {
        bool Is_All = true;
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus <6 && ordersinfo.Orders_Status ==1)
            {
                IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
                if (GoodsListAll != null)
                {
                    foreach (OrdersGoodsInfo entity in GoodsListAll)
                    {
                        if ((entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID > 0 && entity.U_Orders_Goods_Product_BuyChannel == "") || (entity.Orders_Goods_Type != 2 && entity.Orders_Goods_ParentID == 0 && entity.U_Orders_Goods_Product_BuyChannel == ""))
                        {
                            Is_All = false;
                        }
                    }
                }
            }
        }
        return Is_All;
    }

    //管理员备注修改
    public void Orders_Admin_Note()
    {
        string Orders_Admin_Note = tools.CheckStr(Request.Form["Orders_Admin_Note"]);
        int Orders_Admin_Sign = tools.CheckInt(Request.Form["Orders_Admin_Sign"]);
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        OrdersInfo orderinfo = GetOrdersByID(Orders_ID);
        if (orderinfo != null)
        {
            orderinfo.Orders_Admin_Sign = Orders_Admin_Sign;
            orderinfo.Orders_Admin_Note = Orders_Admin_Note;

            if (MyBLL.EditOrders(orderinfo))
            {
                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "修改管理员备注", "成功", "修改管理员备注为：" + Orders_Admin_Note);
            }

            Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
        }
        else
        {
            Response.Redirect("orders/orders_list.aspx?listtype=all");
        }
    }

    //获取默认等级
    public int GetMembeDefaultrGradeID()
    {
        int default_Grade = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        IList<MemberGradeInfo> entitys = MyGrade.GetMemberGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberGradeInfo entity in entitys)
            {
                if (entity.Member_Grade_Default == 1)
                {
                    default_Grade = entity.Member_Grade_ID;
                }
            }
        }
        return default_Grade;
        
    }

    //检查运费优惠
    public CartInfo Orders_Favor_Fee_Check(int Member_ID,string State_ID, double Delivery_Fee, int Orders_ID, int delivery_id, int payway_id)
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
        member_grade = 0;
        MemberInfo memberinfo = member.GetMemberByID(Member_ID);
        if (memberinfo != null)
        {
            member_grade = memberinfo.Member_Grade;
        }
        if (member_grade == 0)
        {
            member_grade = GetMembeDefaultrGradeID();
        }
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

    //获取购物车参加优惠产品价格
    public double Cart_Cate_Price(string cate_id, int Orders_ID)
    {
        double favor_price = 0;
        string product_cate = "";
        bool Is_Match = false;
        IList<OrdersGoodsInfo> goodstmps = MyBLL.GetGoodsListByOrderID(Orders_ID);
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

    //订单收货地址修改
    public void Orders_Address_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Orders_Address_Country = tools.CheckStr(Request.Form["Orders_Address_Country"]);
        string Orders_Address_State = tools.CheckStr(Request.Form["Orders_Address_State"]);
        string Orders_Address_City = tools.CheckStr(Request.Form["Orders_Address_City"]);
        string Orders_Address_County = tools.CheckStr(Request.Form["Orders_Address_County"]);
        string Orders_Address_StreetAddress = tools.CheckStr(Request.Form["Orders_Address_StreetAddress"]);
        string Orders_Address_Zip = tools.CheckStr(Request.Form["Orders_Address_Zip"]);
        string Orders_Address_Name = tools.CheckStr(Request.Form["Orders_Address_Name"]);
        string Orders_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Orders_Address_Phone_Countrycode"]);
        string Orders_Address_Phone_Areacode = "";
        string Orders_Address_Phone_Number = tools.CheckStr(Request.Form["Orders_Address_Phone_Number"]);
        string Orders_Address_Mobile = tools.CheckStr(Request.Form["Orders_Address_Mobile"]);
        CartInfo favorfee;

        if (Orders_Address_County == "0" || Orders_Address_County == "")
        {
            Public.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Orders_Address_StreetAddress == "" || Orders_Address_Zip == "" || Orders_Address_Name == "" || (Orders_Address_Phone_Number == "" && Orders_Address_Mobile == ""))
        {
            Public.Msg("info", "提示信息", "请将收货人信息写完整", false, "{back}");
        }
        if (Orders_ID > 0)
        {
            OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
            if (ordersinfo != null)
            {
                if (ordersinfo.Orders_DeliveryStatus != 0 || ordersinfo.Orders_Status != 0 || ordersinfo.Orders_PaymentStatus != 0)
                {
                    Public.Msg("error", "错误信息", "该订单地址不可修改", false, "{back}");
                }
                favorfee = Orders_Favor_Fee_Check(ordersinfo.Orders_BuyerID,Orders_Address_State, ordersinfo.Orders_Total_Freight, ordersinfo.Orders_ID,ordersinfo.Orders_Delivery,ordersinfo.Orders_Payway);
                ordersinfo.Orders_Address_State = Orders_Address_State;
                ordersinfo.Orders_Address_City = Orders_Address_City;
                ordersinfo.Orders_Address_County = Orders_Address_County;
                ordersinfo.Orders_Address_StreetAddress = Orders_Address_StreetAddress;
                ordersinfo.Orders_Address_Zip = Orders_Address_Zip;
                ordersinfo.Orders_Address_Name = Orders_Address_Name;
                ordersinfo.Orders_Address_Phone_Number = Orders_Address_Phone_Number;
                ordersinfo.Orders_Address_Mobile = Orders_Address_Mobile;
                ordersinfo.Orders_Total_FreightDiscount = favorfee.Total_Favor_Fee;
                ordersinfo.Orders_Total_FreightDiscount_Note = favorfee.Favor_Fee_Note;
                ordersinfo.Orders_Total_AllPrice = ordersinfo.Orders_Total_Freight + ordersinfo.Orders_Total_Price - ordersinfo.Orders_Total_PriceDiscount - ordersinfo.Orders_Total_FreightDiscount;
                MyBLL.EditOrders(ordersinfo);
                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "修改收货地址", "成功", "修改订单收货地址");
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //订单配送方式选择
    public string Delivery_Way_Select(int Orders_ID, int delivery_way, string state, string city, string county)
    {
        //double total_weight = Get_Goods_Weight(Orders_ID);
        double delivery_fee = 0;
        string way_list = "";
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        IList<DeliveryWayInfo> deliveryways = deliveryway.GetDeliveryWaysByDistrict(state, city, county);
        if (deliveryways != null)
        {
            foreach (DeliveryWayInfo entity in deliveryways)
            {
                delivery_fee = Get_Orders_FreightFee(entity.Delivery_Way_ID,Orders_ID);
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\"><input type=\"radio\" name=\"order_delivery\" id=\"order_delivery\" value=\"" + entity.Delivery_Way_ID + "\" onclick=\"MM_findObj('orders_total_freight').value=" + delivery_fee.ToString("0.00") + ";\" " + Public.CheckedRadio(entity.Delivery_Way_ID.ToString(), delivery_way.ToString()) + "></td>";
                way_list = way_list + "  <td ><span class=\"t14\">" + entity.Delivery_Way_Name + "</span> </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //获取订单产品重量
    public double Get_Goods_Weight(int Supplier_ID,int Orders_ID)
    {
        double total_weight = 0;
        ProductInfo goods_product = null;
        PackageInfo goods_package = null;
        IList<OrdersGoodsInfo> goodsall = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (goodsall != null)
        {
            foreach (OrdersGoodsInfo entity in goodsall)
            {
                if (entity.Orders_Goods_Product_SupplierID == Supplier_ID)
                {
                    //统计产品重量
                    if (entity.Orders_Goods_Type == 0 || entity.Orders_Goods_Type == 3 || entity.Orders_Goods_Type == 1)
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
        }
        return total_weight;
    }

    //按重量计费运费价格
    public double My_Goods_weightprice(double weight, double initialweigh, double initialfee, double upweight, double upfee)
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

    //获取运费
    public double Get_Goods_Deliveryfee(int delivery_id, double total_weight)
    {
        double delivery_fee = 0;
        DeliveryWayInfo delivery = deliveryway.GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == Public.GetCurrentSite())
            {
                if (delivery.Delivery_Way_FeeType == 1)
                {
                    delivery_fee = My_Goods_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
                }
                else
                {
                    delivery_fee = delivery.Delivery_Way_Fee;
                }
            }
        }
        return delivery_fee;
    }

    //获取订单中指定供应商运费
    public double Get_Orders_Deliveryfee(int supplier_id, int delivery_id, double total_weight)
    {

        double delivery_fee = 0;
        DeliveryWayInfo delivery = deliveryway.GetDeliveryWayByID(delivery_id);
        if (delivery != null)
        {
            if (delivery.Delivery_Way_Status == 1 && delivery.Delivery_Way_Site == Public.GetCurrentSite())
            {
                if (supplier_id == 0)
                {
                    if (delivery.Delivery_Way_FeeType == 1)
                    {
                        delivery_fee = My_Goods_weightprice(total_weight, delivery.Delivery_Way_InitialWeight, delivery.Delivery_Way_InitialFee, delivery.Delivery_Way_UpWeight, delivery.Delivery_Way_UpFee);
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
                            delivery_fee = My_Goods_weightprice(total_weight, supplierdelivery.Supplier_Delivery_Fee_InitialWeight, supplierdelivery.Supplier_Delivery_Fee_InitialFee, supplierdelivery.Supplier_Delivery_Fee_UpWeight, supplierdelivery.Supplier_Delivery_Fee_UpFee);
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

    //获取订单产品全部运费
    public double Get_Orders_FreightFee(int delivery_id, int Orders_ID)
    {
        double total_weight, total_fee;
        total_fee = 0;
        string supplier_id = "";

        IList<OrdersGoodsInfo> goodstmps = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsInfo entity in goodstmps)
            {
                if (supplier_id == "")
                {
                    supplier_id = entity.Orders_Goods_Product_SupplierID.ToString();
                    total_weight = Get_Goods_Weight(entity.Orders_Goods_Product_SupplierID, Orders_ID);
                    total_fee += Get_Orders_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                }
                else
                {
                    foreach (string substr in supplier_id.Split(','))
                    {
                        if (tools.CheckInt(substr) != entity.Orders_Goods_Product_SupplierID)
                        {
                            supplier_id += "," + entity.Orders_Goods_Product_SupplierID;
                            total_weight = Get_Goods_Weight(entity.Orders_Goods_Product_SupplierID, Orders_ID);
                            total_fee += Get_Orders_Deliveryfee(entity.Orders_Goods_Product_SupplierID, delivery_id, total_weight);
                        }
                    }
                }
            }
        }
        return total_fee;
    }

    //物流公司选择
    public string Delivery_Company_Select(string select_name,string Code)
    {

        string way_list = "";
        way_list = "<select name=\"" + select_name + "\" " + Code + ">";
        IList<DeliveryWayInfo> deliveryways = deliveryway.GetDeliveryWaysInfo();
        if (deliveryways != null)
        {
            foreach (DeliveryWayInfo entity in deliveryways)
            {

                way_list = way_list + "  <option value=\"" + entity.Delivery_Way_Name + "\">" + entity.Delivery_Way_Name + "</option>";
            }
        }
        way_list = way_list + "</select>";
        return way_list;
    }

    //配送方式编辑
    public void Orders_Delivery_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int order_delivery = tools.CheckInt(Request.Form["order_delivery"]);
        double orders_total_freight = tools.CheckFloat(Request.Form["orders_total_freight"]);
        double orders_total_freight_old = 0;
        string delivery_name_old = "";
        string delivery_name = "";
        CartInfo favorfee;

        DeliveryWayInfo delivery_way = deliveryway.GetDeliveryWayByID(order_delivery);
        if (delivery_way != null)
        {
            if (delivery_way.Delivery_Way_Status == 1 && delivery_way.Delivery_Way_Site == Public.GetCurrentSite())
            {
                delivery_name = delivery_way.Delivery_Way_Name;
            }
            else
            {
                order_delivery = 0;
            }
        }
        else
        {
            order_delivery = 0;
        }

        if (order_delivery == 0)
        {
            Public.Msg("error", "错误信息", "请选择有效的配送方式", false, "{back}");
        }

        OrdersInfo orderinfo = GetOrdersByID(Orders_ID);
        if (orderinfo != null)
        {
            if (orderinfo.Orders_DeliveryStatus != 0 || orderinfo.Orders_Status != 0 || orderinfo.Orders_PaymentStatus != 0)
            {
                Public.Msg("error", "错误信息", "该订单配送方式不可修改", false, "{back}");
            }
            orders_total_freight_old = orderinfo.Orders_Total_Freight;
            delivery_name_old = orderinfo.Orders_Delivery_Name;
            favorfee = Orders_Favor_Fee_Check(orderinfo.Orders_BuyerID,orderinfo.Orders_Address_State, orders_total_freight, orderinfo.Orders_ID, order_delivery, orderinfo.Orders_Payway);
            orderinfo.Orders_Delivery = order_delivery;
            orderinfo.Orders_Delivery_Name = delivery_name;
            orderinfo.Orders_Total_Freight = orders_total_freight;
            orderinfo.Orders_Total_FreightDiscount = favorfee.Total_Favor_Fee;
            orderinfo.Orders_Total_FreightDiscount_Note = favorfee.Favor_Fee_Note;
            orderinfo.Orders_Total_AllPrice = orderinfo.Orders_Total_Price + orders_total_freight - orderinfo.Orders_Total_PriceDiscount - orderinfo.Orders_Total_FreightDiscount;



            if (MyBLL.EditOrders(orderinfo))
            {
                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "修改配送方式", "成功", "配送方式由：(" + delivery_name_old + ")修改为(" + delivery_name + ")<br>费用由：(" + Public.DisplayCurrency(orders_total_freight_old) + ")修改为(" + Public.DisplayCurrency(orders_total_freight) + ")");
            }

            Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
        }
        else
        {
            Response.Redirect("orders/orders_list.aspx?listtype=all");
        }
    }

    //订单支付方式选择
    public string Pay_Way_Select(int Payway_ID, int delivery_cod)
    {
        string way_list = "";
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
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\"><input type=\"radio\" name=\"order_payway\" id=\"order_payway\" value=\"" + entity.Pay_Way_ID + "\" " + Public.CheckedRadio(entity.Pay_Way_ID.ToString(), Payway_ID.ToString()) + "></td>";
                way_list = way_list + "  <td ><span class=\"t14\">" + entity.Pay_Way_Name + "</span> </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
    }

    //订单支付方式选择
    public string Pay_Type_Select(int Paytype_ID)
    {
        string way_list = "";
        way_list = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"3\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayTypeInfo.Pay_Type_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayTypeInfo.Pay_Type_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PayTypeInfo.Pay_Type_Sort", "asc"));
        IList<PayTypeInfo> paytypes = paytype.GetPayTypes(Query, Public.GetUserPrivilege());
        if (paytypes != null)
        {
            foreach (PayTypeInfo entity in paytypes)
            {
                way_list = way_list + "<tr>";
                way_list = way_list + "  <td width=\"20\" align=\"center\"><input type=\"radio\" name=\"order_paytype\" id=\"order_paytype\" value=\"" + entity.Pay_Type_ID + "\" " + Public.CheckedRadio(entity.Pay_Type_ID.ToString(), Paytype_ID.ToString()) + "></td>";
                way_list = way_list + "  <td ><span class=\"t14\">" + entity.Pay_Type_Name + "</span> </td>";
                way_list = way_list + "</tr>";
            }
        }
        way_list = way_list + "</table>";
        return way_list;
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_Sort", "asc"));
        IList<PayWayInfo> entitys = payway.GetPayWays(Query, Public.GetUserPrivilege());
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

    //支付方式修改
    public void Orders_Payway_Edit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int order_payway = tools.CheckInt(Request.Form["order_payway"]);
        int order_paytype = tools.CheckInt(Request.Form["order_paytype"]);
        string payway_name, payway_name_old;
        payway_name = "";
        string paytype_name, paytype_name_old;
        paytype_name = "";

        PayWayInfo paywayinfo = payway.GetPayWayByID(order_payway, Public.GetUserPrivilege());
        if (paywayinfo != null)
        {
            if (paywayinfo.Pay_Way_Status == 1 && paywayinfo.Pay_Way_Site == Public.GetCurrentSite())
            {
                payway_name = paywayinfo.Pay_Way_Name;
            }
            else
            {
                order_payway = 0;
            }
        }
        else
        {
            order_payway = 0;
        }

        PayTypeInfo paytypeinfo = paytype.GetPayTypeByID(order_paytype, Public.GetUserPrivilege());
        if (paytypeinfo != null)
        {
            if (paytypeinfo.Pay_Type_IsActive == 1 && paytypeinfo.Pay_Type_Site == Public.GetCurrentSite())
            {
                paytype_name = paytypeinfo.Pay_Type_Name;
            }
            else
            {
                order_paytype = 0;
            }
        }
        else
        {
            order_paytype = 0;
        }

        if (order_payway == 0)
        {
            Public.Msg("error", "错误信息", "请选择有效的支付方式", false, "{back}");
        }
        if (order_paytype == 0)
        {
            Public.Msg("error", "错误信息", "请选择有效的支付条件", false, "{back}");
        }

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            payway_name_old = ordersinfo.Orders_Payway_Name;
            ordersinfo.Orders_Payway = order_payway;
            ordersinfo.Orders_Payway_Name = payway_name;
            paytype_name_old = ordersinfo.Orders_PayType_Name;
            ordersinfo.Orders_PayType = order_paytype;
            ordersinfo.Orders_PayType_Name = paytype_name;
            if (MyBLL.EditOrders(ordersinfo))
            {
                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "修改支付方式", "成功", "支付方式由：(" + payway_name_old + ")修改为(" + payway_name + ")");
                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "修改支付条件", "成功", "支付条件由：(" + paytype_name_old + ")修改为(" + paytype_name + ")");
            }

            Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
        }
        else
        {
            Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
        }
    }

    //订单详细页按钮状态
    public void Order_Detail_Button(int Orders_Status, int Orders_ispay, int Orders_Payway, int Orders_isfreight, int Orders_Delivery, int Order_id, DateTime Orders_Addtime, int Supplier_ID, int Orders_HasSubset)
    {
        string cancel_confirm, disable_confirm, disable_payment, disable_refund, disable_reach, disable_prepare, disable_freight,disable_accept, disable_regoods, disable_success, disable_close;
        string disable_split;

        //取消按钮
        if (Orders_Status == 0)
        {
            cancel_confirm = "";
        }
        else
        {
            cancel_confirm = "disabled=\"disabled\"";
        }

        //确认按钮
        if (Orders_Status == 0)
        {
            disable_confirm = "";
        }
        else
        {
            disable_confirm = "disabled=\"disabled\"";
        }

        //支付按钮
        if (Orders_Status <2 && Orders_ispay == 0)
        {
            disable_payment = "";
        }
        else
        {
            disable_payment = "disabled=\"disabled\"";
        }

        //退款按钮
        if (Orders_Status == 1 && (Orders_ispay == 1 || Orders_ispay == 4))
        {
            disable_refund = "";
        }
        else
        {
            disable_refund = "disabled=\"disabled\"";
        }

        //到账按钮
        if (Orders_Status == 1 && Orders_ispay == 1 )
        {
            disable_reach = "";
        }
        else
        {
            disable_reach = "disabled=\"disabled\"";
        }

        //配货中按钮
        if (Orders_Status == 1 && Orders_isfreight == 0)
        {
            disable_prepare = "";
        }
        else
        {
            disable_prepare = "disabled=\"disabled\"";
        }

        //拆单处理
        if (Orders_Status == 1 && Orders_isfreight == 6 && Orders_ispay == 4 && Orders_HasSubset == 0)
            disable_split = "";
        else
            disable_split = "disabled=\"disabled\"";

        //配送按钮
        if (Orders_Status == 1 && (Orders_isfreight == 6))
        {
            disable_freight = "";
        }
        else
        {
            disable_freight = "disabled=\"disabled\"";
        }

        //签收按钮
        if (Orders_Status == 1 && (Orders_isfreight == 1 && (Orders_ispay==1||Orders_ispay==4)))
        {
            disable_accept = "";
        }
        else
        {
            disable_accept = "disabled=\"disabled\"";
        }

        //退货按钮
        if (Orders_Status == 1 && (Orders_isfreight != 0 && Orders_isfreight != 6 && Orders_isfreight != 5))
        {
            disable_regoods = "";
        }
        else
        {
            disable_regoods = "disabled=\"disabled\"";
        }

        //成功按钮
        if (Orders_Status == 1 && (Orders_ispay == 4) && (Orders_isfreight == 2))
        {
            disable_success = "";
        }
        else
        {
            disable_success = "disabled=\"disabled\"";
        }

        //失败按钮
        if (Orders_Status == 1 && (Orders_ispay == 2 || Orders_ispay == 0) && (Orders_isfreight == 5 || Orders_isfreight == 0))
        {
            disable_close = "";
        }
        else
        {
            disable_close = "disabled=\"disabled\"";
        }

        if (Public.CheckPrivilege("c4f1a30c-b384-4311-bb68-0cd9e892196c"))
        {
            Response.Write("<input name=\"btn_cancel\" type=\"button\" class=\"btn_01\" " + cancel_confirm + " id=\"btn_cancel\" value=\"取消\" onclick=\"turnnewpage('orders_cancel.aspx?orders_id=" + Order_id + "')\"/> ");
        }

        //Supplier_ID为0则为系统处理订单否则为供应商处理订单
        if (Public.CheckPrivilege("c1de0682-e5a2-4545-9c21-748b73c014ef") && Supplier_ID==0)
        {
            Response.Write("<input name=\"btn_confirm\" type=\"button\" class=\"btn_01\" " + disable_confirm + " id=\"btn_confirm\" value=\"确认\" onclick=\"turnnewpage('orders_do.aspx?action=order_confirm&orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("272974c4-7a98-4a00-a1e0-6d730e565cdb"))
        {
            Response.Write("<input name=\"btn_payment\" type=\"button\" class=\"btn_01\" " + disable_payment + " id=\"btn_confirm\" value=\"支付\" onclick=\"turnnewpage('orders_pay.aspx?orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("02fb8713-d70d-4da2-9f7f-2ce5cd033e0a"))
        {
            Response.Write("<input name=\"btn_refund\" type=\"button\" class=\"btn_01\" " + disable_refund + " id=\"btn_confirm\" value=\"退款\" onclick=\"turnnewpage('orders_paycancel.aspx?orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("25b5f3a9-5aba-45ac-8f28-c4f30e6054aa"))
        {
            Response.Write("<input name=\"btn_reach\" type=\"button\" class=\"btn_01\" " + disable_reach + " id=\"btn_reach\" value=\"已到帐\" onclick=\"turnnewpage('orders_do.aspx?action=order_reach&orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("25996ef4-35b5-4980-a4cd-60c368db97f8"))
        {
            Response.Write("<input name=\"btn_prepare\" type=\"button\" class=\"btn_01\" " + disable_prepare + " id=\"btn_prepare\" value=\"配货中\"  onclick=\"turnnewpage('orders_do.aspx?action=order_prepare&orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("6623eae9-9663-4726-844c-7e0eefa5c335"))
        {
            //Response.Write("<input name=\"btn_split\" type=\"button\" class=\"btn_01\" " + disable_split + " id=\"btn_split\" value=\"拆分子订单\" onclick=\"turnnewpage('orders_split.aspx?orders_id=" + Order_id + "')\"/> ");

            Response.Write("<input name=\"btn_freight\" type=\"button\" class=\"btn_01\" " + disable_freight + " id=\"btn_freight\" value=\"发货\"  onclick=\"turnnewpage('orders_freight.aspx?orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("e3e651b8-b47a-4391-80b5-970e624c6275"))
        {
            Response.Write("<input name=\"btn_accept\" type=\"button\" class=\"btn_01\" " + disable_accept + " id=\"btn_accept\" value=\"签收\"  onclick=\"turnnewpage('orders_do.aspx?action=order_accept&orders_id=" + Order_id + "')\"/> ");
        }

        if (Public.CheckPrivilege("4d0ebf65-94db-4992-bfcf-49b28f09ef38"))
        {
            Response.Write("<input name=\"btn_regoods\" type=\"button\" class=\"btn_01\" " + disable_regoods + " id=\"btn_regoods\" value=\"退货\" onclick=\"turnnewpage('orders_freightcancel.aspx?orders_id=" + Order_id + "')\" /> ");
        }

        Response.Write("&nbsp;&nbsp;");
        if (Public.CheckPrivilege("f210813c-8d1d-4e1d-8dde-7d566925f6d6"))
        {
            Response.Write("<input name=\"btn_success\" type=\"button\" class=\"btn_01\" " + disable_success + " id=\"btn_success\" value=\"完成\" onclick=\"turnnewpage('orders_do.aspx?action=order_success&orders_id=" + Order_id + "')\" /> ");
        }

        if (Public.CheckPrivilege("705ee66c-4a2f-4064-9f34-3c4fb5d45e21"))
        {
            Response.Write("<input name=\"btn_close\" type=\"button\" class=\"btn_01\" " + disable_close + " id=\"btn_close\" value=\"关闭\" onclick=\"turnnewpage('orders_close.aspx?orders_id=" + Order_id + "')\" /> ");
        }

        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    }

    //优惠券恢复使用
    public void Orders_Coupon_ReUse(int Orders_ID)
    {
        string coupon_id;
        PromotionFavorCouponInfo entity;
        coupon_id = MyBLL.GetOrdersCoupons(Orders_ID);
        if (coupon_id != "")
        {
            foreach (string subcoupon in coupon_id.Split(','))
            {
                entity = MyCoupon.GetPromotionFavorCouponByID(tools.CheckInt(subcoupon), Public.GetUserPrivilege());
                if (entity != null)
                {
                    entity.Promotion_Coupon_UseAmount = entity.Promotion_Coupon_UseAmount - 1;
                    entity.Promotion_Coupon_Isused = 0;
                    MyCoupon.EditPromotionFavorCoupon(entity);
                }
            }
        }
    }
    
    //订单取消
    public void Orders_Cancel()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int total_usecoin, orders_buyerid;
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                orders_buyerid = ordersinfo.Orders_BuyerID;
                total_usecoin = ordersinfo.Orders_Total_UseCoin;
                orders_sn = ordersinfo.Orders_SN;

                ordersinfo.Orders_Status = 3;
                ordersinfo.Orders_Fail_Note = tools.CheckStr(Request.Form["orders_cancel_note"]);
                ordersinfo.Orders_Fail_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
                ordersinfo.Orders_Fail_Addtime = DateTime.Now;

                if (MyBLL.EditOrders(ordersinfo))
                {
                    //优惠券恢复
                    Orders_Coupon_ReUse(Orders_ID);

                    //派发优惠券删除
                    Orders_SendCoupon_Action("delete", ordersinfo.Orders_BuyerID, Orders_ID);

                    //积分返还
                    if (total_usecoin > 0)
                    {
                        member.Member_Coin_AddConsume(total_usecoin, "订单" + orders_sn + "取消返还积分", orders_buyerid, true);
                    }

                    //虚拟账号余额回返
                    double account_pay = 0;
                    account_pay = ordersinfo.Orders_Account_Pay;
                    if (account_pay > 0)
                    {
                        member.Member_Account_Log(ordersinfo.Orders_BuyerID, account_pay, "订单" + orders_sn + "取消,抵扣金额退回");
                    }
                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "取消", "成功", "订单取消,取消原因：" + tools.CheckStr(Request.Form["orders_cancel_note"]));
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //订单确认
    public void Orders_Confirm()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 0)
            {
                //现货订单验证可用库存
                if (ordersinfo.Orders_Type == 1)
                {
                    if (CheckProductEnough(Orders_ID) == false)
                    {
                        Public.Msg("info", "信息提示", "可用库存不足", false, "{back}");
                    }
                }

                orders_sn = ordersinfo.Orders_SN;

                ordersinfo.Orders_Status = 1;

                if (MyBLL.EditOrders(ordersinfo))
                {
                    //现货订单可用库存减少
                    if (ordersinfo.Orders_Type == 1)
                    {
                        //减少可用库存
                        ProductCountAction(Orders_ID, "del");
                    }

                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "确认", "成功", "订单确认");
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //订单支付已到帐
    public void Orders_PayReach()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_PaymentStatus == 1 && ordersinfo.Orders_Status == 1)
            {

                orders_sn = ordersinfo.Orders_SN;

                ordersinfo.Orders_PaymentStatus = 4;
                ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;

                if (MyBLL.EditOrders(ordersinfo))
                {


                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "确认", "成功", "订单支付已到帐");
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //订单配货中
    public void Orders_Delivery_Prepare()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus == 0 && ordersinfo.Orders_Status == 1)
            {

                orders_sn = ordersinfo.Orders_SN;

                ordersinfo.Orders_DeliveryStatus = 6;

                if (MyBLL.EditOrders(ordersinfo))
                {


                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "确认", "成功", "订单配货中");
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //订单已签收
    public void Orders_Delivery_Accept()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_DeliveryStatus == 1 && ordersinfo.Orders_Status == 1)
            {

                orders_sn = ordersinfo.Orders_SN;

                ordersinfo.Orders_DeliveryStatus = 2;
                ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;

                if (MyBLL.EditOrders(ordersinfo))
                {


                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "订单签收", "成功", "订单签收");
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //生成支付单号
    public string orders_payment_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersPaymentInfo paymentinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.CreatevkeyNum(5);
        while (ismatch == true)
        {
            paymentinfo = Mypayment.GetOrdersPaymentBySn(sn);
            if (paymentinfo != null)
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

    //订单支付
    public void Orders_Pay(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Orders_Payment_Name = tools.CheckStr(Request["Orders_Payment_Name"]);
        string Orders_Payment_Note = tools.CheckStr(Request["Orders_Payment_Note"]);
        int Orders_Payment_PaymentStatus = 0;
        int Orders_Buyerid = 0;
        int Order_Payment_ID = 0;
        string Orders_SN = "";
        string Orders_Payment_DocNo = orders_payment_sn();
        string paymentmemo = "";

        if (Orders_Payment_Name == "")
        {
            Public.Msg("info", "信息提示", "请填写支付方式", false, "{back}");
        }


        if (operate == "create")
        {
            Orders_Payment_PaymentStatus = 1;
        }
        else
        {
            Orders_Payment_PaymentStatus = 2;
        }

        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status <2 && (ordersinfo.Orders_PaymentStatus != 2))
            {
                Orders_SN = ordersinfo.Orders_SN;
                Orders_Buyerid = ordersinfo.Orders_BuyerID;
                //if (Orders_Buyerid == 0)
                //{
                //    Public.Msg("info", "信息提示", "无法为该订单用户进行退付款", false, "{back}");
                //}

                OrdersPaymentInfo orderspayment = new OrdersPaymentInfo();
                orderspayment.Orders_Payment_ID = 0;
                orderspayment.Orders_Payment_OrdersID = Orders_ID;
                orderspayment.Orders_Payment_PaymentStatus = Orders_Payment_PaymentStatus;
                orderspayment.Orders_Payment_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
                orderspayment.Orders_Payment_DocNo = Orders_Payment_DocNo;
                orderspayment.Orders_Payment_Name = Orders_Payment_Name;
                orderspayment.Orders_Payment_Amount = ordersinfo.Orders_Total_AllPrice;
                orderspayment.Orders_Payment_Note = Orders_Payment_Note;
                orderspayment.Orders_Payment_Addtime = DateTime.Now;
                orderspayment.Orders_Payment_Site = Public.GetCurrentSite();

                if (Mypayment.AddOrdersPayment(orderspayment))
                {
                    orderspayment = Mypayment.GetOrdersPaymentBySn(Orders_Payment_DocNo);

                    if (orderspayment != null)
                    {
                        Order_Payment_ID = orderspayment.Orders_Payment_ID;
                        if (operate == "create")
                        {
                            paymentmemo = "订单付款，付款单号 {order_payment_sn}  金额{order_payment_totalprice}";
                            paymentmemo = paymentmemo.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Order_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                            paymentmemo = paymentmemo.Replace("{order_payment_totalprice}", Public.DisplayCurrency(ordersinfo.Orders_Total_AllPrice));
                            orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "付款", "成功", paymentmemo);
                        }
                        else
                        {
                            if (Orders_Payment_Name == "虚拟账户")
                            {
                                //虚拟账号退款
                            }
                            paymentmemo = "订单退款，退款单号 {order_payment_sn}  金额{order_payment_totalprice}";
                            paymentmemo = paymentmemo.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Order_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                            paymentmemo = paymentmemo.Replace("{order_payment_totalprice}", Public.DisplayCurrency(ordersinfo.Orders_Total_AllPrice));
                            orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "退款", "成功", paymentmemo);
                        }
                        ordersinfo.Orders_PaymentStatus = Orders_Payment_PaymentStatus;
                        ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;
                        MyBLL.EditOrders(ordersinfo);
                        Public.Msg("positive", "操作成功", "操作成功", true, "/orders/orders_view.aspx?orders_id=" + Orders_ID);
                    }
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //申请退款
    public void Orders_PayApply()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int Orders_Delivery_ID = tools.CheckInt(Request["Orders_Delivery_ID"]);
        int Orders_Payment_MemberID = tools.CheckInt(Request["Orders_Payment_MemberID"]);
        string Orders_Payment_Name = tools.CheckStr(Request["Orders_Payment_Name"]);
        string Orders_Payment_Note = tools.CheckStr(Request["Orders_Payment_Note"]);
        double Orders_Payment_Amount = tools.NullDbl(Request["Orders_Payment_Amount"]);
        int Orders_Payment_PaymentStatus = 0;
        int Orders_Buyerid = 0;
        int Order_Payment_ID = 0;
        string Orders_SN = "";
        string Orders_Payment_DocNo = orders_payment_sn();
        string paymentmemo = "";

        if (Orders_Payment_Name == "")
        {
            Public.Msg("info", "信息提示", "请填写支付方式", false, "{back}");
        }

        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            //if (ordersinfo.Orders_Status == 4 && ordersinfo.Orders_PaymentStatus == 4)
            //{
            Orders_SN = ordersinfo.Orders_SN;
            Orders_Buyerid = ordersinfo.Orders_BuyerID;
            if (Orders_Buyerid == 0)
            {
                Public.Msg("info", "信息提示", "无法为该订单用户进行退款", false, "{back}");
            }

            OrdersPaymentInfo orderspayment = new OrdersPaymentInfo();
            orderspayment.Orders_Payment_ID = 0;
            orderspayment.Orders_Payment_OrdersID = Orders_ID;
            orderspayment.Orders_Payment_MemberID = Orders_Buyerid;
            orderspayment.Orders_Payment_PaymentStatus = 2;
            orderspayment.Orders_Payment_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
            orderspayment.Orders_Payment_DocNo = Orders_Payment_DocNo;
            orderspayment.Orders_Payment_Name = Orders_Payment_Name;
            orderspayment.Orders_Payment_Status = 0;
            orderspayment.Orders_Payment_ApplyAmount = Orders_Payment_Amount;
            orderspayment.Orders_Payment_Note = Orders_Payment_Note;
            orderspayment.Orders_Payment_Addtime = DateTime.Now;
            orderspayment.Orders_Payment_Site = Public.GetCurrentSite();

            if (Mypayment.AddOrdersPayment(orderspayment))
            {
                orderspayment = Mypayment.GetOrdersPaymentBySn(Orders_Payment_DocNo);
                OrdersDeliveryInfo ordersdeliveryinfo = Mydelivery.GetOrdersDeliveryByID(Orders_Delivery_ID, Public.GetUserPrivilege());
                if (ordersdeliveryinfo != null)
                {
                    ordersdeliveryinfo.Orders_Delivery_Status = 1;
                    Mydelivery.EditOrdersDelivery(ordersdeliveryinfo, Public.GetUserPrivilege());
                }
                if (orderspayment != null)
                {
                    Order_Payment_ID = orderspayment.Orders_Payment_ID;

                    paymentmemo = "订单退款，退款单号 {order_payment_sn}  金额{Orders_Payment_Amount}";
                    paymentmemo = paymentmemo.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Order_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                    paymentmemo = paymentmemo.Replace("{Orders_Payment_Amount}", Public.DisplayCurrency(Orders_Payment_Amount));
                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "申请退款", "成功", paymentmemo + "申请退款");

                    ordersinfo.Orders_PaymentStatus = 3;//操作订单为退款处理中
                    ordersinfo.Orders_PaymentStatus_Time = DateTime.Now;
                    MyBLL.EditOrders(ordersinfo);
                    Public.Msg("positive", "操作成功", "操作成功", true, "/OrdersDelivery/orders_delivery_list.aspx?listtype=returned");
                }
            }
            //}
        }
        Response.Redirect("/OrdersDelivery/orders_delivery_list.aspx?listtype=returned");
    }

    //生成配送单号
    public string orders_delivery_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersDeliveryInfo deliveryinfo = null; 
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + Public.CreatevkeyNum(5);
        while (ismatch == true)
        {
            deliveryinfo = Mydelivery.GetOrdersDeliveryBySn(sn, Public.GetUserPrivilege());
            if (deliveryinfo != null)
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
            Public.Msg("info", "信息提示", "请填写配送方式", false, "{back}");
        }

        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 4)
            {
                Orders_SN = ordersinfo.Orders_SN;

                OrdersDeliveryInfo ordersdelivery = new OrdersDeliveryInfo();
                ordersdelivery.Orders_Delivery_ID = 0;
                ordersdelivery.Orders_Delivery_DeliveryStatus = 5;
                ordersdelivery.Orders_Delivery_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
                ordersdelivery.Orders_Delivery_OrdersID = Orders_ID;
                ordersdelivery.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
                ordersdelivery.Orders_Delivery_Name = Orders_Delivery_Name;
                ordersdelivery.Orders_Delivery_companyName = Orders_Delivery_companyName;
                ordersdelivery.Orders_Delivery_Code = Orders_Delivery_Code;
                ordersdelivery.Orders_Delivery_Amount = Orders_Delivery_Amount;
                ordersdelivery.Orders_Delivery_Note = Orders_Delivery_Note;
                ordersdelivery.Orders_Delivery_Addtime = DateTime.Now;
                ordersdelivery.Orders_Delivery_Status = 0;
                ordersdelivery.Orders_Delivery_Site = Public.GetCurrentSite();
                if (Mydelivery.AddOrdersDelivery(ordersdelivery, Public.GetUserPrivilege()))
                {
                    ordersdelivery = Mydelivery.GetOrdersDeliveryBySn(Orders_Delivery_DocNo, Public.GetUserPrivilege());

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

                                ////实退货数量
                                //OrdersDeliveryGoodsInfo ordersdeliverygoodsinfo = new OrdersDeliveryGoodsInfo();
                                //ordersdeliverygoodsinfo.Orders_Delivery_Goods_DeliveryID = ordersdelivery.Orders_Delivery_ID;
                                //ordersdeliverygoodsinfo.Orders_Delivery_Goods_GoodsID = Orders_BackApply_Product_ProductID;
                                //ordersdeliverygoodsinfo.Orders_Delivery_Goods_Amount = Orders_BackApply_Product_ApplyAmount;
                                //MyBackDeliveryGoods.AddOrdersDeliveryGoods(ordersdeliverygoodsinfo);
                            }

                        }
                        freightreason = ordersdelivery.Orders_Delivery_DocNo;
                        freightreason = "订单退货，退货单号{order_freight_sn}";
                        freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + ordersdelivery.Orders_Delivery_ID + "\">" + ordersdelivery.Orders_Delivery_DocNo + "</a>");
                        //实际库存退回
                        //ProductStockBackAction(ordersdelivery.Orders_Delivery_ID);
                        //ProductCountBackAction(ordersdelivery.Orders_Delivery_ID);
                        orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "退货", "成功", freightreason);
                        ordersinfo.Orders_DeliveryStatus = 5;
                        ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                        MyBLL.EditOrders(ordersinfo);
                        OrdersBackApplyInfo orderbackapplyinfo = MyBack.GetOrdersBackApplyByID(back_id, Public.GetUserPrivilege());
                        if (orderbackapplyinfo != null)
                        {
                            orderbackapplyinfo.Orders_BackApply_Status = 3;
                            MyBack.EditOrdersBackApply(orderbackapplyinfo, Public.GetUserPrivilege());
                        }
                        Public.Msg("positive", "操作成功", "操作成功", true, "/orders/orders_backproductlist.aspx");
                    }
                }
            }
        }
        Response.Redirect("/orders/orders_backproductlist.aspx");
    }

    

    


    //订单发货
    public void Orders_Delivery(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Orders_Delivery_Name = tools.CheckStr(Request["Orders_Delivery_Name"]);
        string Orders_Delivery_companyName = tools.CheckStr(Request["Orders_Delivery_companyName"]);
        string Orders_Delivery_Code = tools.CheckStr(Request["Orders_Delivery_Code"]);
        string Orders_Delivery_Note = tools.CheckStr(Request["Orders_Delivery_Note"]);
        double Orders_Delivery_Amount = tools.CheckFloat(Request["Orders_Delivery_Amount"]);
        int Orders_Delivery_DeliveryStatus = 0;
        int Orders_Buyerid = 0;
        string Orders_SN = "";
        int Orders_Delivery_ID;
        string Orders_Delivery_DocNo = orders_delivery_sn();
        string freightreason = "";
        string member_email="";

        if (Orders_Delivery_Name == "")
        {
            Public.Msg("info", "信息提示", "请填写配送方式", false, "{back}");
        }


        if (operate == "create")
        {
            Orders_Delivery_DeliveryStatus = 1;
        }
        else
        {
            Orders_Delivery_DeliveryStatus = 5;
        }

        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 1 && (ordersinfo.Orders_DeliveryStatus != 5))
            {
                Orders_SN = ordersinfo.Orders_SN;
                Orders_Buyerid = ordersinfo.Orders_BuyerID;
                if (CheckProductStockEnough(Orders_ID) == false && Orders_Delivery_DeliveryStatus == 1)
                {
                    Public.Msg("info", "信息提示", "订单产品库存不足", false, "{back}");

                }

                OrdersDeliveryInfo ordersdelivery = new OrdersDeliveryInfo();
                ordersdelivery.Orders_Delivery_ID = 0;
                ordersdelivery.Orders_Delivery_DeliveryStatus = Orders_Delivery_DeliveryStatus;
                ordersdelivery.Orders_Delivery_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
                ordersdelivery.Orders_Delivery_OrdersID = Orders_ID;
                ordersdelivery.Orders_Delivery_DocNo = Orders_Delivery_DocNo;
                ordersdelivery.Orders_Delivery_Name = Orders_Delivery_Name;
                ordersdelivery.Orders_Delivery_companyName = Orders_Delivery_companyName;
                ordersdelivery.Orders_Delivery_Code = Orders_Delivery_Code;
                ordersdelivery.Orders_Delivery_Amount = Orders_Delivery_Amount;
                ordersdelivery.Orders_Delivery_Note = Orders_Delivery_Note;
                ordersdelivery.Orders_Delivery_Addtime = DateTime.Now;
                ordersdelivery.Orders_Delivery_Site = Public.GetCurrentSite();



                if (Mydelivery.AddOrdersDelivery(ordersdelivery, Public.GetUserPrivilege()))
                {
                    ordersdelivery = Mydelivery.GetOrdersDeliveryBySn(Orders_Delivery_DocNo, Public.GetUserPrivilege());

                    if (ordersdelivery != null)
                    {
                        Orders_Delivery_ID = ordersdelivery.Orders_Delivery_ID;
                        if (operate == "create")
                        {
                            //实际库存扣除
                            ProductStockAction(Orders_ID, "del");

                            freightreason = "订单发货，发货单号{order_freight_sn}";
                            freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + Orders_Delivery_ID + "\">" + Orders_Delivery_DocNo + "</a>");
                            orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "发货", "成功", freightreason);
                        }
                        else
                        {
                            //实际库存退回
                            ProductStockAction(Orders_ID, "add");
                            ProductCountAction(Orders_ID, "add");
                            freightreason = "订单退货，退货单号{order_freight_sn}";
                            freightreason = freightreason.Replace("{order_freight_sn}", "<a href=\"/ordersdelivery/orders_delivery_view.aspx?orders_delivery_id=" + Orders_Delivery_ID + "\">" + Orders_Delivery_DocNo + "</a>");
                            orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "退货", "成功", freightreason);
                        }
                        ordersinfo.Orders_DeliveryStatus = Orders_Delivery_DeliveryStatus;
                        ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;
                        MyBLL.EditOrders(ordersinfo);
                        MemberInfo memberinfo = member.GetMemberByID(ordersinfo.Orders_BuyerID);
                        if (memberinfo != null)
                        {
                            member_email = memberinfo.Member_Email;
                        }
                        if (operate == "create")
                        { 
                            //发送订单邮件
                            string mailsubject, mailbodytitle, mailbody;
                            mailsubject = "您在"+tools.NullStr(Application["site_name"])+"上的订单已发货";
                            mailbodytitle = mailsubject;
                            mailbody = mail_template("order_freight", "",ordersinfo.Orders_SN);
                            Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                            Confirm_Send_Goods(ordersinfo.Orders_SN, Orders_Delivery_companyName, Orders_Delivery_Code); 
                        }
                        Public.Msg("positive", "操作成功", "操作成功", true, "/orders/orders_view.aspx?orders_id=" + Orders_ID);
                    }
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
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
	    //安全校验码，与partner是一组，获取方式是：用签约时支付宝帐号登陆支付宝网站www.alipay.com，在供应商服务我的供应商里即可查到。 
	    //Dim partner As String = ""
	    //商户ID，合作身份者ID，合作伙伴ID 
	    string _input_charset = "utf-8";
	    //编码类型，完全根据客户自身的项目的编码格式而定，千万不要填错。否则极其容易造成MD5加密错误。
	    string trade_no = "";
	    string trade_status = "";
        IList<MemberPayLogInfo> paylog=Mypayment.GetMemberPayLogByOrdersSn(orders_sn);
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

	    if (trade_no!="") {
		    trade_status = trade_no;
		    if (trade_no.IndexOf("ALIPAYTRADENO:") >= 0) {
                trade_no = trade_no.Substring(trade_no.IndexOf("ALIPAYTRADENO:")+13);
			    if (trade_no.IndexOf("<BR>") > 0) {
                    trade_no = trade_no.Substring(1, trade_no.IndexOf("<BR>") - 1);
			    }
		    }
	    }
	    if (trade_no=="") {
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

	    for (i = 0; i <= Sortedstr.Length - 1; i++) {

		    if (i == Sortedstr.Length - 1) {
			    prestr.Append(Sortedstr[i]);
		    } else {
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

    //获取快递100物流信息
    public string Get_Delivery_Info(string typeCom, string Orders_Delivery_Code)
    {
        string ApiKey = "d2fcad63758650e7";

        typeCom = Public.GetKuaiDiCom(typeCom);

        string apiurl = "http://api.kuaidi100.com/api?id=" + ApiKey + "&com=" + typeCom + "&nu=" + Orders_Delivery_Code + "&show=2&muti=1&order=asc";
        WebRequest request = WebRequest.Create(@apiurl);
        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        Encoding encode = Encoding.UTF8;
        StreamReader reader = new StreamReader(stream, encode);
        string detail = reader.ReadToEnd();
        return detail;
    }

    //订单完成
    public void Orders_Success()
    {
        int Orders_ID = tools.CheckInt(Request["orders_id"]);
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        {
            if (ordersinfo != null)
            {
                if (ordersinfo.Orders_Status == 1 && (ordersinfo.Orders_PaymentStatus == 1 || ordersinfo.Orders_PaymentStatus == 4) && (ordersinfo.Orders_DeliveryStatus == 1 || ordersinfo.Orders_DeliveryStatus == 2))
                {
                    //赠送积分
                    if (ordersinfo.Orders_Total_Coin > 0)
                    {
                        member.Member_Coin_AddConsume(ordersinfo.Orders_Total_Coin, "订单" + ordersinfo.Orders_SN + "完成赠送积分", ordersinfo.Orders_BuyerID, false);
                    }

                    //更新派发优惠券
                    //Orders_SendCoupon_Action("valid", ordersinfo.Orders_BuyerID, Orders_ID, "",0);

                    //记录订单日志
                    orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "完成", "成功", "订单交易完成");

                    //更新产品销售
                    Orders_Product_Update_Salecount(Orders_ID);

                    //更新订单状态
                    ordersinfo.Orders_Status = 2;
                    ordersinfo.Orders_DeliveryStatus = 2;
                    ordersinfo.Orders_IsReturnCoin = 1;
                    MyBLL.EditOrders(ordersinfo);
                }
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //赠送积分和派发优惠券
    public void Order_Coin_Gift(int Orders_ID)
    {
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        {
            if (ordersinfo.Orders_Status == 1 && (ordersinfo.Orders_PaymentStatus == 1 || ordersinfo.Orders_PaymentStatus == 4) && ordersinfo.Orders_DeliveryStatus == 2)
            {
                //赠送积分
                if (ordersinfo.Orders_Total_Coin > 0)
                {
                    member.Member_Coin_AddConsume(ordersinfo.Orders_Total_Coin, "订单" + ordersinfo.Orders_SN + "完成赠送积分", ordersinfo.Orders_BuyerID, false);
                }

                //更新派发优惠券
                Orders_SendCoupon_Action("valid", ordersinfo.Orders_BuyerID, Orders_ID);

                //记录订单日志
                orderlog.Orders_Log(Orders_ID, "系统自动赠送积分", "完成", "成功", "订单交易完成");
                //更新订单状态
                ordersinfo.Orders_IsReturnCoin = 1;
                ordersinfo.Orders_Status = 2;
                MyBLL.EditOrders(ordersinfo);
            }
        }
    }


    //创建权限
    public void CreateUserPrivilege()
    {
        RBACUserInfo UserInfo = null;
        if (Session["UserPrivilege"] != null)
        {
            UserInfo = (RBACUserInfo)Session["UserPrivilege"];
        }
        else
        {
            UserInfo = new RBACUserInfo();
        }
        UserInfo.RBACRoleInfos.Add(new RBACRoleInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[0].RBAC_Privilege_ID = "18cde8c2-8be5-4b15-b057-795726189795";
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[1].RBAC_Privilege_ID = "833b9bdd-a344-407b-b23a-671348d57f76";
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[2].RBAC_Privilege_ID = "079ec5fc-33fe-4d58-a17f-14b5877b4ffe";
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[3].RBAC_Privilege_ID = "d394b6b8-560a-49b9-9d20-1a356d3bf984";
        Session["UserPrivilege"] = UserInfo;
    }

    //订单关闭
    public void Orders_Close()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        int total_usecoin, orders_buyerid;
        string orders_sn;

        OrdersInfo ordersinfo = MyBLL.GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
        {

            orders_buyerid = ordersinfo.Orders_BuyerID;
            orders_sn = ordersinfo.Orders_SN;
            if (ordersinfo.Orders_DeliveryStatus != 5 && ordersinfo.Orders_Status > 0)
            {
                ProductCountAction(Orders_ID, "add");
            }
            ordersinfo.Orders_Status = 3;
            ordersinfo.Orders_Fail_Note = tools.CheckStr(Request.Form["orders_close_note"]);
            ordersinfo.Orders_Fail_SysUserID = tools.CheckInt(Session["User_ID"].ToString());
            ordersinfo.Orders_Fail_Addtime = DateTime.Now;

            if (MyBLL.EditOrders(ordersinfo))
            {
                //使用优惠券恢复
                Orders_Coupon_ReUse(Orders_ID);

                //删除派发优惠券
                Orders_SendCoupon_Action("delete", ordersinfo.Orders_BuyerID, Orders_ID);

                orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "关闭", "成功", "订单关闭,关闭原因：" + tools.CheckStr(Request.Form["orders_close_note"]));
            }
        }
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);
    }

    //派发优惠券操作
    public void Orders_SendCoupon_Action(string Action, int Member_ID, int Orders_ID)
    {
        QueryInfo Query;
        switch (Action)
        {
            case "valid":
                int validnum;
                TimeSpan timespan;
                Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", Public.GetCurrentSite()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", Member_ID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_OrdersID", "=", Orders_ID.ToString()));
                IList<PromotionFavorCouponInfo> coupons = MyCoupon.GetPromotionFavorCoupons(Query, Public.GetUserPrivilege());
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
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", Public.GetCurrentSite()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", Member_ID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_OrdersID", "=", Orders_ID.ToString()));
                IList<PromotionFavorCouponInfo> coupones = MyCoupon.GetPromotionFavorCoupons(Query, Public.GetUserPrivilege());
                if (coupones != null)
                {
                    foreach (PromotionFavorCouponInfo entity in coupones)
                    {
                        MyCoupon.DelPromotionFavorCoupon(entity.Promotion_Coupon_ID, Public.GetUserPrivilege());
                    }
                }
                break;
        }
    }

    //订单发票操作
    public void Orders_Invoice_Action(string operate)
    {
        int Orders_ID = tools.CheckInt(Request["Orders_id"]);
        OrdersInfo ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo != null)
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
            if (MyBLL.EditOrders(ordersinfo))
            {
                switch (operate)
                {
                    case "open":
                        //记录订单日志
                        orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "开票", "成功", "订单开票");
                        break;
                    case "cancel":
                        //记录订单日志
                        orderlog.Orders_Log(Orders_ID, Session["User_Name"].ToString(), "退票", "成功", "订单退票");
                        break;
                }
            }

        }

        Response.Redirect("/orders/orders_view.aspx?orders_id=" + Orders_ID);

    }

    //获取订单发票信息
    public OrdersInvoiceInfo GetOrdersInvoiceByOrdersID(int Orders_ID)
    {
        return MyInvoice.GetOrdersInvoiceByOrdersID(Orders_ID);
    }

    //产品可用库存操作
    public void ProductCountAction(int Orders_ID, string action)
    {
        int Product_ID, Goods_Amount, Goods_Type;

        IList<OrdersGoodsInfo> entitys = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
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

    //产品实际库存操作
    public void ProductStockAction(int Orders_ID, string action)
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

        IList<OrdersGoodsInfo> entitys = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                Product_ID = entity.Orders_Goods_Product_ID;
                Goods_Amount = (int)Math.Round(entity.Orders_Goods_Amount)  ;
                Goods_Type = entity.Orders_Goods_Type;
                switch (action)
                {
                    //退货
                    case "add":
                        if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                        {
                            ProductInfo productinfo = MyProduct.GetProductByID(Product_ID, Public.GetUserPrivilege());
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
                                    MyProduct.UpdateProductStock(entity.Orders_Goods_Product_Code, (int)Math.Round(entity.Orders_Goods_Amount), 0);
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

    //检查订单产品可用库存
    public bool CheckProductEnough(int orders_id)
    {
        bool resultvalue = true;
        int stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        IList<OrdersGoodsInfo> goodses = MyBLL.GetGoodsListByOrderID(orders_id);
        if (goodses != null)
        {
            foreach (OrdersGoodsInfo entity in goodses)
            {
                productstockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_Amount = 0;
                if (entity.Orders_Goods_Type != 2)
                {
                    productstockinfo = Get_Productcount(entity.Orders_Goods_Product_ID);
                    stock = productstockinfo.Product_Stock_Amount;

                    if (productstockinfo.Product_Stock_IsNoStock == 0 && entity.Orders_Goods_Amount > stock)
                    {
                        resultvalue = false;
                    }
                }
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                {
                    PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (package != null)
                    {
                        IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                        if (packageproducts != null)
                        {
                            productstockinfo = Get_Package_Count(packageproducts);
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
                    else
                    {
                        resultvalue = false;
                    }
                }
            }
        }
        return resultvalue;
    }

    //检查订单产品实际库存
    public bool CheckProductStockEnough(int orders_id)
    {
        bool resultvalue = true;
        int stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        IList<OrdersGoodsInfo> goodses = MyBLL.GetGoodsListByOrderID(orders_id);
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
                    PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
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

    //获取产品可用库存
    public ProductStockInfo Get_Productcount(int product_id)
    {
        int product_stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        productstockinfo.Product_Stock_Amount = 0;
        productstockinfo.Product_Stock_IsNoStock = 0;
        ProductInfo product = MyProduct.GetProductByID(product_id, Public.GetUserPrivilege());
        if (product != null)
        {
            productstockinfo.Product_Stock_IsNoStock = product.Product_IsNoStock;

            productstockinfo.Product_Stock_Amount = product.Product_UsableAmount;
        }
        return productstockinfo;
    }

    //获取产品实际库存
    public ProductStockInfo Get_Productstock(int product_id)
    {
        int product_stock = 0;
        ProductStockInfo productstockinfo = new ProductStockInfo();
        productstockinfo.Product_Stock_Amount = 0;
        productstockinfo.Product_Stock_IsNoStock = 0;
        ProductInfo product = MyProduct.GetProductByID(product_id, Public.GetUserPrivilege());
        if (product != null)
        {

            productstockinfo.Product_Stock_IsNoStock = product.Product_IsNoStock;

            productstockinfo.Product_Stock_Amount = product.Product_StockAmount;

        }
        return productstockinfo;
    }

    //获取捆绑产品可用库存
    public ProductStockInfo Get_Package_Count(IList<PackageProductInfo> packageproducts)
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

    //订单发票信息选择
    public string Orders_Invoice_Info(OrdersInvoiceInfo entity)
    {
        StringBuilder HTML = new StringBuilder();
        HTML.Append("");
        HTML.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        if (entity != null)
        {
            if (entity.Invoice_Type == 1)
            {
                if (entity.Invoice_Title == "单位")
                {
                    HTML.Append("  <tr>");
                    HTML.Append("    <td width=\"100\" align=\"right\"><strong>发票类型</strong></td><td width=\"300\">普通发票</td><td width=\"100\" align=\"right\"><strong>发票抬头</strong></td><td>" + entity.Invoice_Title + "</td>");
                    HTML.Append("  </tr>");
                    HTML.Append("  <tr>");
                    HTML.Append("    <td width=\"100\" align=\"right\"><strong>单位名称</strong></td><td>" + entity.Invoice_FirmName + "</td><td width=\"100\" align=\"right\"><strong>发票内容</strong></td><td>明细</td>");
                    HTML.Append("  </tr>");
                }
                else
                {
                    HTML.Append("  <tr>");
                    HTML.Append("    <td width=\"100\" align=\"right\"><strong>发票类型</strong></td><td width=\"300\">普通发票</td><td></td><td></td>");
                    HTML.Append("  </tr>");
                    HTML.Append("  <tr>");
                    HTML.Append("    <td width=\"100\" align=\"right\"><strong>发票抬头</strong></td><td>" + entity.Invoice_Title + "</td><td width=\"100\" align=\"right\"><strong>发票内容</strong><td>明细</td>");
                    HTML.Append("  </tr>");
                }
            }
            else if (entity.Invoice_Type == 2)
            {
                HTML.Append("  <tr>");

                HTML.Append("    <td width=\"100\" align=\"right\"><strong>发票类型</strong></td><td width=\"300\">增值税发票</td><td width=\"100\" align=\"right\"><strong>发票内容</strong></td><td>" + entity.Invoice_VAT_Content + "</td>");

                HTML.Append("  </tr>");
                HTML.Append("  <tr>");
                HTML.Append("    <td width=\"100\" align=\"right\"><strong>单位名称</strong><td>" + entity.Invoice_VAT_FirmName + "</td><td width=\"100\" align=\"right\"><strong>纳税人识别号</strong></td><td>" + entity.Invoice_VAT_Code + "</td>");
                HTML.Append("  </tr>");
                HTML.Append("  <tr>");
                HTML.Append("    <td width=\"100\" align=\"right\"><strong>注册地址</strong></td><td>" + entity.Invoice_VAT_RegAddr + "</td><td width=\"100\" align=\"right\"><strong>注册电话</strong></td><td>" + entity.Invoice_VAT_RegTel + "</td>");
                HTML.Append("  </tr>");
                HTML.Append("  <tr>");
                HTML.Append("    <td width=\"100\" align=\"right\"><strong>开户银行</strong></td><td>" + entity.Invoice_VAT_Bank + "</td><td width=\"100\" align=\"right\"><strong>银行账户</strong></td><td>" + entity.Invoice_VAT_BankAcount + "</td>");
                HTML.Append("  </tr>");
            }
        }
        HTML.Append("</table>");
        return HTML.ToString();
    }

    //更新产品销售量
    public void Orders_Product_Update_Salecount(int Orders_ID)
    {
        int Product_ID, Goods_Amount, Goods_Type;

        IList<OrdersGoodsInfo> entitys = MyBLL.GetGoodsListByOrderID(Orders_ID);
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

    //更新订单监管
    public void Orders_Monitor_Status(int status)
    {
        int orders_id = tools.CheckInt(Request["orders_id"]);
        OrdersInfo ordersinfo = GetOrdersByID(orders_id);
        if (ordersinfo != null)
        {
            ordersinfo.U_Orders_IsMonitor = status;
            MyBLL.EditOrders(ordersinfo);
        }
        ordersinfo = null;
        Response.Redirect("/orders/orders_view.aspx?orders_id=" + orders_id);
    }

    //退换货申请列表
    public string GetOrdersBackApplyList()
    {
        string keyword=tools.CheckStr(Request["keyword"]);
        int back_status = tools.CheckInt(Request["back_status"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersBackApplyInfo.Orders_BackApply_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "OrdersBackApplyInfo.Orders_BackApply_OrdersCode", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersBackApplyInfo.Orders_BackApply_Name", "like", keyword));
        }
        if (back_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "0"));
        }
        if (back_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "1"));
        }
        if (back_status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "2"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBack.GetPageInfo(Query, Public.GetUserPrivilege());

        MemberInfo memberinfo;
        IList<OrdersBackApplyInfo> entitys = MyBack.GetOrdersBackApplys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersBackApplyInfo entity in entitys)
            {
                memberinfo = member.GetMemberByID(entity.Orders_BackApply_MemberID);
                if (memberinfo == null) { memberinfo = new MemberInfo(); }

                jsonBuilder.Append("{\"OrdersBackApplyInfo.Orders_BackApply_ID\":" + entity.Orders_BackApply_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_OrdersCode);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_BackApply_Amount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Orders_BackApply_Type == 1)
                {
                    jsonBuilder.Append("换货");
                }
                else if (entity.Orders_BackApply_Type == 2)
                {
                    jsonBuilder.Append("退款");
                }
                else
                {
                    jsonBuilder.Append("退货");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(memberinfo.Member_Email);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Orders_BackApply_Status == 0) { jsonBuilder.Append("未处理"); }
                else if (entity.Orders_BackApply_Status == 1) { jsonBuilder.Append("审核通过"); }
                else if (entity.Orders_BackApply_Status == 2) { jsonBuilder.Append("审核不通过"); }
                else if (entity.Orders_BackApply_Status == 4) { jsonBuilder.Append("申请失败"); }
                else { jsonBuilder.Append("已处理"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");


                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"orders_back_view.aspx?back_id=" + entity.Orders_BackApply_ID + "\\\" title=\\\"查看\\\">查看</a>");


                if (Public.CheckPrivilege("2a5f3eef-36a5-4d2a-83cc-3a4ff9f084ed"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('orders_back_do.aspx?action=move&back_id=" + entity.Orders_BackApply_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //退换货申请列表
    public string GetOrdersBackApplyList1()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersBackApplyInfo.Orders_BackApply_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "OrdersBackApplyInfo.Orders_BackApply_OrdersCode", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersBackApplyInfo.Orders_BackApply_Name", "like", keyword));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersBackApplyInfo.Orders_BackApply_Status", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBack.GetPageInfo(Query, Public.GetUserPrivilege());

        MemberInfo memberinfo;
        IList<OrdersBackApplyInfo> entitys = MyBack.GetOrdersBackApplys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersBackApplyInfo entity in entitys)
            {
                memberinfo = member.GetMemberByID(entity.Orders_BackApply_MemberID);
                if (memberinfo == null) { memberinfo = new MemberInfo(); }

                jsonBuilder.Append("{\"id\":" + entity.Orders_BackApply_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_OrdersCode);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Orders_BackApply_Amount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Orders_BackApply_Type == 1)
                {
                    jsonBuilder.Append("换货");
                }
                else
                {
                    jsonBuilder.Append("退货");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(memberinfo.Member_Email);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_BackApply_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Orders_BackApply_Status == 0) { jsonBuilder.Append("未处理"); }
                else if (entity.Orders_BackApply_Status == 1) { jsonBuilder.Append("审核通过"); }
                else if (entity.Orders_BackApply_Status == 2) { jsonBuilder.Append("审核不通过"); }
                else { jsonBuilder.Append("已处理"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");


                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"orders_backproductview.aspx?back_id=" + entity.Orders_BackApply_ID + "\\\" title=\\\"查看\\\">查看</a>");

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

    public OrdersBackApplyInfo GetOrdersBackApplyByID(int ID)
    {
        return MyBack.GetOrdersBackApplyByID(ID, Public.GetUserPrivilege());
    }

    public void OrdersBackApplyEdit()
    {
        int back_id = tools.CheckInt(Request["back_id"]);
        double back_Amount = tools.CheckFloat(Request["back_amount"]);
        int back_action = tools.CheckInt(Request["back_action"]);
        string back_note = tools.CheckStr(Request["back_note"]);
        if (back_note.Length == 0)
        {
            Public.Msg("info", "信息提示", "请将审核备注填写完整！", false, "{back}");
        }
        OrdersBackApplyInfo backinfo = GetOrdersBackApplyByID(back_id);
        if (backinfo != null)
        {
            if (backinfo.Orders_BackApply_Status == 1)
            {
                if (back_action == 1)
                {
                    backinfo.Orders_BackApply_Status = 3;
                }
                else
                {
                    backinfo.Orders_BackApply_Status = 4;
                }
                backinfo.Orders_BackApply_AdminNote = back_note;
                backinfo.Orders_BackApply_AdminTime = DateTime.Now;
                backinfo.Orders_BackApply_Amount = back_Amount;
                MyBack.EditOrdersBackApply(backinfo, Public.GetUserPrivilege());
            }
            else if (backinfo.Orders_BackApply_Status == 0)
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
                MyBack.EditOrdersBackApply(backinfo, Public.GetUserPrivilege());
            }
           
        }
        Response.Redirect("orders_back_view.aspx?back_id=" + back_id);
    }

    public void DelOrdersBackApply()
    {
        int back_id = tools.CheckInt(Request["back_id"]);
        if (MyBack.DelOrdersBackApply(back_id, Public.GetUserPrivilege()) > 0)
        {

            Public.Msg("positive", "操作成功", "操作成功", true, "orders_backlist.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetOrderBackGoodsInfo(int back_id)
    {
        int i = 0;
        string return_value = "";
        IList<OrdersBackApplyProductInfo> ordersbackapplyproductinfos = MyBackProduct.GetOrdersBackApplyProductByApplyID(back_id);
        if (ordersbackapplyproductinfos != null)
        {
            return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
            return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">实退数量</td></tr>";
            foreach (OrdersBackApplyProductInfo ordersbackapplyproductinfo in ordersbackapplyproductinfos)
            {
                OrdersGoodsInfo entity = MyBLL.GetOrdersGoodsByID(ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID);
                if (entity != null)
                {
                    i++;
                    return_value += "<tr><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\" align=\"center\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\"><input type=\"text\" name=\"Orders_BackApply_Product_ApplyAmount_" + i + "\" size=\"8\" value=\"" + ordersbackapplyproductinfo.Orders_BackApply_Product_ApplyAmount + "\"/><input type=\"hidden\" name=\"Orders_BackApply_Product_ProductID_" + i + "\" value=\"" + ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID + "\"/></td></tr>";
                }
            }
            return_value += "<input type=\"hidden\" name=\"goods_amount\" value=\"" + i + "\"|></table>";
        }
        return return_value;
    }

    public string GetOrderBackGoodsInfo1(int back_id)
    {
        string return_value = "";
        IList<OrdersBackApplyProductInfo> ordersbackapplyproductinfos = MyBackProduct.GetOrdersBackApplyProductByApplyID(back_id);
        if (ordersbackapplyproductinfos != null)
        {
            return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
            return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">实退数量</td></tr>";
            foreach (OrdersBackApplyProductInfo ordersbackapplyproductinfo in ordersbackapplyproductinfos)
            {
                OrdersGoodsInfo entity = MyBLL.GetOrdersGoodsByID(ordersbackapplyproductinfo.Orders_BackApply_Product_ProductID);
                if (entity != null)
                {
                    return_value += "<tr><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\" align=\"center\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\">" + ordersbackapplyproductinfo.Orders_BackApply_Product_ApplyAmount + "</td></tr>";
                }
            }
            return_value += "</table>";
        }
        return return_value;
    }

    public string GetOrderBackDeliveryGoodsInfo(int delivery_id)
    {
        string return_value = "";
        //IList<OrdersDeliveryGoodsInfo> ordersdeliverygoodsinfos = MyBackDeliveryGoods.GetOrdersDeliveryGoodsByDeliveryID(delivery_id);
        //if (ordersdeliverygoodsinfos != null)
        //{
        //    return_value += "<table border=\"0\" width=\"600\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#999999;\"/>";
        //    return_value += "<tr><td style=\"background:#ffffff;\" align=\"center\">编码</td><td style=\"background:#ffffff;\" align=\"center\">商品</td><td style=\"background:#ffffff;\" align=\"center\">名称</td><td style=\"background:#ffffff;\" align=\"center\">实退数量</td></tr>";
        //    foreach (OrdersDeliveryGoodsInfo ordersdeliverygoodsinfo in ordersdeliverygoodsinfos)
        //    {
        //        OrdersGoodsInfo entity = MyBLL.GetOrdersGoodsByID(ordersdeliverygoodsinfo.Orders_Delivery_Goods_GoodsID);
        //        if (entity != null)
        //        {
        //            return_value += "<tr><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Code + "</td><td style=\"background:#ffffff;\" align=\"center\"><img src=\"" + Public.FormatImgURL(entity.Orders_Goods_Product_Img, "fullpath") + "\" width=\"40\" height=\"40\"/></td><td style=\"background:#ffffff;\">" + entity.Orders_Goods_Product_Name + "</td><td style=\"background:#ffffff;\" align=\"center\">" + ordersdeliverygoodsinfo.Orders_Delivery_Goods_Amount + "</td></tr>";
        //        }
        //    }
        //    return_value += "</table>";
        //}
        return return_value;
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
            switch (count_type)
            {
                case "order_unprocessed":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "order_processing":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    break;
                case "order_success":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                    break;
                case "order_faiture":
                    Query.ParamInfos.Add(new ParamInfo("AND(", "int", "OrdersInfo.Orders_Status", "=", "3"));
                    Query.ParamInfos.Add(new ParamInfo("or)", "int", "OrdersInfo.Orders_Status", "=", "4"));
                    break;
            }
            PageInfo page = MyBLL.GetPageInfo(Query);
            if (page != null)
            {
                Order_Count = page.RecordCount;
            }
        }
        return Order_Count;
    }

    public string GetSupplierName(int Supplier_ID)
    {
        string Supplier_Name = "";
        SupplierInfo entity = MySupplier.GetSupplierByID(Supplier_ID, Public.GetUserPrivilege());
        if (entity != null)
        {
            Supplier_Name = entity.Supplier_CompanyName;
        }
        return Supplier_Name;
    }

    /// <summary>
    /// 供应商佣金列表
    /// </summary>
    /// <returns></returns>
    public string Supplier_Commission()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);

        int isendprice = tools.CheckInt(Request["status"]);


        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_IsSettling", "=", isendprice.ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        IList<OrdersInfo> entitys = MyBLL.GetOrderss(Query);
        PageInfo page = MyBLL.GetPageInfo(Query);
        if(entitys!=null)
        {
            int Member_ID, buyCount, i;
            string Member_Email, Member_Nickname;
            double buySum;
            i = 0;
            double total_price, total_endprice, total_brokerage, product_price, product_brokerage;
            int supplier_id=0;
            total_price = 0;
            total_endprice = 0;
            total_brokerage = 0;


            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + page.CurrentPage + ",\"total\":" + page.PageCount + ",\"records\":" + page.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (OrdersInfo entity in entitys)
            {
                product_price = entity.Orders_Total_AllPrice;
                total_price = total_price + product_price;
                product_brokerage = 0;
                supplier_id = 0;
                IList<OrdersGoodsInfo> goodsinfos = GetGoodsListByOrderID(entity.Orders_ID);
                if (goodsinfos != null)
                {
                    foreach (OrdersGoodsInfo goodsinfo in goodsinfos)
                    {
                        product_brokerage = product_brokerage + (goodsinfo.Orders_Goods_Product_brokerage * goodsinfo.Orders_Goods_Amount);
                        total_brokerage = total_brokerage + product_brokerage;
                        supplier_id = goodsinfo.Orders_Goods_Product_SupplierID;
                    }
                }
                jsonBuilder.Append("{\"id\":" + entity.Orders_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Orders_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"/Supplier/Supplier_edit.aspx?Supplier_id=" + supplier_id + "\\\">" + Public.JsonStr(GetSupplierName(supplier_id)) + "</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"/orders/orders_view.aspx?orders_id=" + entity.Orders_ID + "\\\">" + Public.JsonStr(entity.Orders_SN) + "</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(OrdersStatus(entity.Orders_Status).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(OrdersSettlingStatus(entity.Orders_IsSettling).Replace("\"", "\\\""));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(entity.Orders_Total_AllPrice) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(entity.Orders_Total_Price) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(entity.Orders_Total_PriceDiscount) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(entity.Orders_Total_Freight) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(entity.Orders_Total_FreightDiscount) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(product_price - product_brokerage) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(product_brokerage) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + entity.Orders_Addtime+ "");
                jsonBuilder.Append("\",");


                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");


            

            //jsonBuilder.Append("{\"id\":0,\"cell\":[");
            ////各字段
            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("0");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("合计");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("--");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("--");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("" + Public.DisplayCurrency(total_price) + "");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("" + Public.DisplayCurrency((total_price - total_brokerage)) + "");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("" + Public.DisplayCurrency(total_brokerage) + "");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("--");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Append("\"");
            //jsonBuilder.Append("--");
            //jsonBuilder.Append("\",");

            //jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            //jsonBuilder.Append("]},");

            
            return jsonBuilder.ToString();
        }
        else
        {
            return "";
        }
        
    }

    /// <summary>
    /// 供应商佣金结算信息
    /// </summary>
    /// <param name="Status">结算状态</param>
    public virtual void Supplier_CommissionSettling(int Status)
    {
        string orders_id = tools.CheckStr(Request["orders_id"]);
        if (orders_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(orders_id, 1) == ",") { orders_id = orders_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_IsSettling", "<", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_ID", "in", orders_id));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Asc"));
        IList<OrdersInfo> entitys = MyBLL.GetOrderss(Query);

        if (entitys != null)
        {
            foreach (OrdersInfo entity in entitys)
            {
                entity.Orders_IsSettling = Status;
                MyBLL.EditOrders(entity);
                if (Status == 2)
                {
                    orderlog.Orders_Log(entity.Orders_ID, Session["User_Name"].ToString(), "订单结算", "成功", "平台订单结算");
                }
                else
                {
                    orderlog.Orders_Log(entity.Orders_ID, Session["User_Name"].ToString(), "取消订单结算申请", "成功", "取消订单结算申请");
                }
            }
        }
        Response.Redirect("/supplier/supplier_settling.aspx");
        

    }

    #region 订单打印

    public string GetPrintTemplate(string sign)
    {
        string strContent = "";
        string SqlList = "SELECT Orders_PrintTemplate_Content FROM Orders_PrintTemplate WHERE Orders_PrintTemplate_Sign = '" + sign + "'";
        SqlDataReader RdrList = null;
        try
        {
            RdrList = DBHelper.ExecuteReader(SqlList);
            if (RdrList.Read())
            {
                strContent = tools.NullStr(RdrList[0]);
            }
        }
        catch (Exception ex)
        { strContent = ex.StackTrace.ToString(); }
        finally
        {
            if (RdrList != null)
            {
                RdrList.Close();
                RdrList = null;
            }
        }
        return strContent;
    }

    public string CreateBarCode(string text, int height, int width, bool showText)
    {
        string strTemp = text.ToLower();

        //替换各个字符
        strTemp = strTemp.Replace("0", "_|_|__||_||_|");
        strTemp = strTemp.Replace("1", "_||_|__|_|_||");
        strTemp = strTemp.Replace("2", "_|_||__|_|_||");
        strTemp = strTemp.Replace("3", "_||_||__|_|_|");
        strTemp = strTemp.Replace("4", "_|_|__||_|_||");
        strTemp = strTemp.Replace("5", "_||_|__||_|_|");
        strTemp = strTemp.Replace("7", "_|_|__|_||_||");
        strTemp = strTemp.Replace("6", "_|_||__||_|_|");
        strTemp = strTemp.Replace("8", "_||_|__|_||_|");
        strTemp = strTemp.Replace("9", "_|_||__|_||_|");
        strTemp = strTemp.Replace("a", "_||_|_|__|_||");
        strTemp = strTemp.Replace("b", "_|_||_|__|_||");
        strTemp = strTemp.Replace("c", "_||_||_|__|_|");
        strTemp = strTemp.Replace("d", "_|_|_||__|_||");
        strTemp = strTemp.Replace("e", "_||_|_||__|_|");
        strTemp = strTemp.Replace("f", "_|_||_||__|_|");
        strTemp = strTemp.Replace("g", "_|_|_|__||_||");
        strTemp = strTemp.Replace("h", "_||_|_|__||_|");
        strTemp = strTemp.Replace("i", "_|_||_|__||_|");
        strTemp = strTemp.Replace("j", "_|_|_||__||_|");
        strTemp = strTemp.Replace("k", "_||_|_|_|__||");
        strTemp = strTemp.Replace("l", "_|_||_|_|__||");
        strTemp = strTemp.Replace("m", "_||_||_|_|__|");
        strTemp = strTemp.Replace("n", "_|_|_||_|__||");
        strTemp = strTemp.Replace("o", "_||_|_||_|__|");
        strTemp = strTemp.Replace("p", "_|_||_||_|__|");
        strTemp = strTemp.Replace("r", "_||_|_|_||__|");
        strTemp = strTemp.Replace("q", "_|_|_|_||__||");
        strTemp = strTemp.Replace("s", "_|_||_|_||__|");
        strTemp = strTemp.Replace("t", "_|_|_||_||__|");
        strTemp = strTemp.Replace("u", "_||__|_|_|_||");
        strTemp = strTemp.Replace("v", "_|__||_|_|_||");
        strTemp = strTemp.Replace("w", "_||__||_|_|_|");
        strTemp = strTemp.Replace("x", "_|__|_||_|_||");
        strTemp = strTemp.Replace("y", "_||__|_||_|_|");
        strTemp = strTemp.Replace("z", "_|__||_||_|_|");
        strTemp = strTemp.Replace("-", "_|__|_|_||_||");
        strTemp = strTemp.Replace("*", "_|__|_||_||_|");
        strTemp = strTemp.Replace("/", "_|__|__|_|__|");
        strTemp = strTemp.Replace("%", "_|_|__|__|__|");
        strTemp = strTemp.Replace("+", "_|__|_|__|__|");
        strTemp = strTemp.Replace(".", "_||__|_|_||_|");

        strTemp = strTemp.Replace("_", "<td style=\"background:#fff; width:" + width + "px;\"></td>");
        strTemp = strTemp.Replace("|", "<td style=\"background:#000; width:" + width + "px;\"></td>");

        string tablestr = string.Empty;

        tablestr = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" height=\"" + height + "\">";
        tablestr += "<tr>" + strTemp + "</tr>";
        tablestr += "</table>";

        if (showText) { return tablestr + text; }
        else { return tablestr; }
    }

    

    public void ShoppingList(int Orders_ID)
    {
        string Orders_SN, Orders_Fail_Note, Orders_Total_PriceDiscount_Note, Orders_Total_FreightDiscount_Note, Orders_Address_Country, Orders_Address_State, Orders_Address_City, Orders_Address_County, Orders_Address_StreetAddress, Orders_Address_Zip, Orders_Address_Name, Orders_Address_Phone_Countrycode, Orders_Address_Phone_Areacode, Orders_Address_Phone_Number, Orders_Address_Mobile, Orders_Delivery_Name, Orders_Payway_Name, Orders_Note, Orders_Admin_Note, Orders_Site, Orders_Source, Orders_VerifyCode, Orders_DeliveryTime;

        int Orders_BuyerID, Orders_SysUserID, Orders_Status, Orders_ERPSyncStatus, Orders_PaymentStatus, Orders_DeliveryStatus, Orders_InvoiceStatus, Orders_Fail_SysUserID, Orders_IsReturnCoin, Orders_Total_Coin, Orders_Total_UseCoin, Orders_Address_ID, Orders_Delivery, Orders_Payway, Orders_Admin_Sign, Orders_SourceType, Orders_DeliveryTime_ID;
        DateTime Orders_PaymentStatus_Time, Orders_DeliveryStatus_Time, Orders_Fail_Addtime, Orders_Addtime;
        double Orders_Total_MKTPrice, Orders_Total_Price, Orders_Total_Freight, Orders_Total_PriceDiscount, Orders_Total_FreightDiscount, Orders_Total_AllPrice;

        int Member_ID, Member_Sex;
        string Member_Email, Member_NickName, Member_Name, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile;
        string Member_StreetAddress, Member_State, Member_City, Member_County, Member_Zip;

        OrdersInfo entity = GetOrdersByID(Orders_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            return;
        }
        else
        {
            Orders_ID = entity.Orders_ID;
            Orders_SN = entity.Orders_SN;
            Orders_BuyerID = entity.Orders_BuyerID;
            Orders_SysUserID = entity.Orders_SysUserID;
            Orders_Status = entity.Orders_Status;
            Orders_ERPSyncStatus = entity.Orders_ERPSyncStatus;
            Orders_PaymentStatus = entity.Orders_PaymentStatus;
            Orders_PaymentStatus_Time = entity.Orders_PaymentStatus_Time;
            Orders_DeliveryStatus = entity.Orders_DeliveryStatus;
            Orders_DeliveryStatus_Time = entity.Orders_DeliveryStatus_Time;
            Orders_InvoiceStatus = entity.Orders_InvoiceStatus;
            Orders_Fail_SysUserID = entity.Orders_Fail_SysUserID;
            Orders_Fail_Note = entity.Orders_Fail_Note;
            Orders_Fail_Addtime = entity.Orders_Fail_Addtime;
            Orders_IsReturnCoin = entity.Orders_IsReturnCoin;
            Orders_Total_MKTPrice = entity.Orders_Total_MKTPrice;
            Orders_Total_Price = entity.Orders_Total_Price;
            Orders_Total_Freight = entity.Orders_Total_Freight;
            Orders_Total_Coin = entity.Orders_Total_Coin;
            Orders_Total_UseCoin = entity.Orders_Total_UseCoin;
            Orders_Total_PriceDiscount = entity.Orders_Total_PriceDiscount;
            Orders_Total_FreightDiscount = entity.Orders_Total_FreightDiscount;
            Orders_Total_PriceDiscount_Note = entity.Orders_Total_PriceDiscount_Note;
            Orders_Total_FreightDiscount_Note = entity.Orders_Total_FreightDiscount_Note;
            Orders_Total_AllPrice = entity.Orders_Total_AllPrice;
            Orders_Address_ID = entity.Orders_Address_ID;
            Orders_Address_Country = entity.Orders_Address_Country;
            Orders_Address_State = entity.Orders_Address_State;
            Orders_Address_City = entity.Orders_Address_City;
            Orders_Address_County = entity.Orders_Address_County;
            Orders_Address_StreetAddress = entity.Orders_Address_StreetAddress;
            Orders_Address_Zip = entity.Orders_Address_Zip;
            Orders_Address_Name = entity.Orders_Address_Name;
            Orders_Address_Phone_Countrycode = entity.Orders_Address_Phone_Countrycode;
            Orders_Address_Phone_Areacode = entity.Orders_Address_Phone_Areacode;
            Orders_Address_Phone_Number = entity.Orders_Address_Phone_Number;
            Orders_Address_Mobile = entity.Orders_Address_Mobile;
            Orders_DeliveryTime_ID = entity.Orders_Delivery_Time_ID;
            Orders_Delivery = entity.Orders_Delivery;
            Orders_Delivery_Name = entity.Orders_Delivery_Name;
            Orders_Payway = entity.Orders_Payway;
            Orders_Payway_Name = entity.Orders_Payway_Name;
            Orders_Note = entity.Orders_Note;
            Orders_Admin_Note = entity.Orders_Admin_Note;
            Orders_Admin_Sign = entity.Orders_Admin_Sign;
            Orders_Site = entity.Orders_Site;
            Orders_SourceType = entity.Orders_SourceType;
            Orders_Source = entity.Orders_Source;
            Orders_VerifyCode = entity.Orders_VerifyCode;
            Orders_Addtime = entity.Orders_Addtime;
        }
        entity = null;

        Member_ID = 0;
        Member_Email = "";
        Member_NickName = "";
        Member_Sex = 0;
        Member_Name = "";
        Member_Phone_Areacode = "";
        Member_Phone_Number = "";
        Member_Mobile = "";
        Member_StreetAddress = "";
        Member_State = "";
        Member_City = "";
        Member_County = "";
        Member_Zip = "";

        MemberInfo memberInfo = member.GetMemberByID(Orders_BuyerID);
        if (memberInfo != null)
        {
            Member_ID = memberInfo.Member_ID;
            Member_Email = memberInfo.Member_Email;
            Member_NickName = memberInfo.Member_NickName;

            MemberProfileInfo ProfileInfo = memberInfo.MemberProfileInfo;
            if (ProfileInfo != null)
            {
                Member_Sex = ProfileInfo.Member_Sex;
                Member_Name = ProfileInfo.Member_Name;
                Member_Phone_Areacode = ProfileInfo.Member_Phone_Areacode;
                Member_Phone_Number = ProfileInfo.Member_Phone_Number;
                Member_Mobile = ProfileInfo.Member_Mobile;
                Member_StreetAddress = ProfileInfo.Member_StreetAddress;
                Member_State = ProfileInfo.Member_State;
                Member_City = ProfileInfo.Member_City;
                Member_County = ProfileInfo.Member_County;
                Member_Zip = ProfileInfo.Member_Zip;
            }
            ProfileInfo = null;
        }
        memberInfo = null;

        string TemplateContent = GetPrintTemplate("ORDERS_PRINT");

        string tmp_head, tmp_list, tmp_bottom;
        string[] PrintTemplate;
        TemplateContent = TemplateContent.Replace("{site_name}", Application["site_name"].ToString());
        TemplateContent = TemplateContent.Replace("{site_url}", Application["site_url"].ToString());
        TemplateContent = TemplateContent.Replace("{site_tel}", Application["site_tel"].ToString());
        TemplateContent = TemplateContent.Replace("{site_email}", Application["site_email"].ToString());


        Regex Regex = new Regex("{for}");
        PrintTemplate = Regex.Split(TemplateContent);
        Regex = null;
        tmp_head = PrintTemplate[0];

        Regex = new Regex("{next}");
        PrintTemplate = Regex.Split(PrintTemplate[1]);
        Regex = null;
        tmp_list = PrintTemplate[0];
        tmp_bottom = PrintTemplate[1];

        //基本信息
        tmp_head = tmp_head.Replace("{orders_addtime}", Orders_Addtime.ToString());
        tmp_head = tmp_head.Replace("{orders_sn}", Orders_SN);
        tmp_head = tmp_head.Replace("{orders_note}", Orders_Note);

        //会员信息
        tmp_head = tmp_head.Replace("{member_nickname}", Member_NickName);
        tmp_head = tmp_head.Replace("{member_name}", Member_Name);
        tmp_head = tmp_head.Replace("{member_phone}", "+86-" + Member_Phone_Areacode + "-" + Member_Phone_Number);

        Response.Write(tmp_head);
        string Info = "";
        int i = 0;
        IList<OrdersGoodsInfo> goodses = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (goodses != null)
        {
            foreach (OrdersGoodsInfo goods in goodses)
            {
                Info = tmp_list;
                i++;
                Info = Info.Replace("{list_num}", i.ToString());
                Info = Info.Replace("{orders_goods_productname}", goods.Orders_Goods_Product_Name);
                Info = Info.Replace("{orders_goods_spec}", goods.Orders_Goods_Product_Spec );
                Info = Info.Replace("{orders_goods_maker}", goods.Orders_Goods_Product_Maker);
                Info = Info.Replace("{orders_goods_mktprice}", Public.DisplayCurrency(goods.Orders_Goods_Product_MKTPrice));
                Info = Info.Replace("{orders_goods_price}", Public.DisplayCurrency(goods.Orders_Goods_Product_Price));
                Info = Info.Replace("{orders_goods_amount}", goods.Orders_Goods_Amount.ToString());
                Info = Info.Replace("{orders_goods_coin}", goods.Orders_Goods_Product_Coin.ToString());
                Info = Info.Replace("{orders_goods_allprice}", Public.DisplayCurrency(goods.Orders_Goods_Product_Price * goods.Orders_Goods_Amount));
                Response.Write(Info);
            }
        }


        tmp_bottom = tmp_bottom.Replace("{orders_note}", Orders_Note);
        //tmp_bottom = tmp_bottom.Replace("{orders_total_freight}", Public.DisplayCurrency(Orders_Total_Freight));
        tmp_bottom = tmp_bottom.Replace("运费：{orders_total_freight}", "");
        tmp_bottom = tmp_bottom.Replace("{orders_total_price}", Public.DisplayCurrency(Orders_Total_Price));
        tmp_bottom = tmp_bottom.Replace("{orders_total_allprice}", Public.DisplayCurrency(Orders_Total_AllPrice));
        tmp_bottom = tmp_bottom.Replace("{orders_total_favorprice}", Public.DisplayCurrency(Orders_Total_PriceDiscount));

        Response.Write(tmp_bottom);
    }

    public void DeliveryList(int Orders_ID)
    {
        string Orders_SN, Orders_Fail_Note, Orders_Total_PriceDiscount_Note, Orders_Total_FreightDiscount_Note, Orders_Address_Country, Orders_Address_State, Orders_Address_City, Orders_Address_County, Orders_Address_StreetAddress, Orders_Address_Zip, Orders_Address_Name, Orders_Address_Phone_Countrycode, Orders_Address_Phone_Areacode, Orders_Address_Phone_Number, Orders_Address_Mobile, Orders_Delivery_Name, Orders_Payway_Name, Orders_Note, Orders_Admin_Note, Orders_Site, Orders_Source, Orders_VerifyCode, Orders_DeliveryTime;

        int Orders_BuyerID, Orders_SysUserID, Orders_Status, Orders_ERPSyncStatus, Orders_PaymentStatus, Orders_DeliveryStatus, Orders_InvoiceStatus, Orders_Fail_SysUserID, Orders_IsReturnCoin, Orders_Total_Coin, Orders_Total_UseCoin, Orders_Address_ID, Orders_Delivery, Orders_Payway, Orders_Admin_Sign, Orders_SourceType, Orders_DeliveryTime_ID;
        DateTime Orders_PaymentStatus_Time, Orders_DeliveryStatus_Time, Orders_Fail_Addtime, Orders_Addtime;
        double Orders_Total_MKTPrice, Orders_Total_Price, Orders_Total_Freight, Orders_Total_PriceDiscount, Orders_Total_FreightDiscount, Orders_Total_AllPrice;

        int Member_ID, Member_Sex;
        string Member_Email, Member_NickName, Member_Name, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile;
        string Member_StreetAddress, Member_State, Member_City, Member_County, Member_Zip, Orders_PayType_Name;

        OrdersInfo entity = GetOrdersByID(Orders_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            return;
        }
        else
        {
            Orders_ID = entity.Orders_ID;
            Orders_SN = entity.Orders_SN;
            Orders_BuyerID = entity.Orders_BuyerID;
            Orders_SysUserID = entity.Orders_SysUserID;
            Orders_Status = entity.Orders_Status;
            Orders_ERPSyncStatus = entity.Orders_ERPSyncStatus;
            Orders_PaymentStatus = entity.Orders_PaymentStatus;
            Orders_PaymentStatus_Time = entity.Orders_PaymentStatus_Time;
            Orders_DeliveryStatus = entity.Orders_DeliveryStatus;
            Orders_DeliveryStatus_Time = entity.Orders_DeliveryStatus_Time;
            Orders_InvoiceStatus = entity.Orders_InvoiceStatus;
            Orders_Fail_SysUserID = entity.Orders_Fail_SysUserID;
            Orders_Fail_Note = entity.Orders_Fail_Note;
            Orders_Fail_Addtime = entity.Orders_Fail_Addtime;
            Orders_IsReturnCoin = entity.Orders_IsReturnCoin;
            Orders_Total_MKTPrice = entity.Orders_Total_MKTPrice;
            Orders_Total_Price = entity.Orders_Total_Price;
            Orders_Total_Freight = entity.Orders_Total_Freight;
            Orders_Total_Coin = entity.Orders_Total_Coin;
            Orders_Total_UseCoin = entity.Orders_Total_UseCoin;
            Orders_Total_PriceDiscount = entity.Orders_Total_PriceDiscount;
            Orders_Total_FreightDiscount = entity.Orders_Total_FreightDiscount;
            Orders_Total_PriceDiscount_Note = entity.Orders_Total_PriceDiscount_Note;
            Orders_Total_FreightDiscount_Note = entity.Orders_Total_FreightDiscount_Note;
            Orders_Total_AllPrice = entity.Orders_Total_AllPrice;
            Orders_Address_ID = entity.Orders_Address_ID;
            Orders_Address_Country = entity.Orders_Address_Country;
            Orders_Address_State = entity.Orders_Address_State;
            Orders_Address_City = entity.Orders_Address_City;
            Orders_Address_County = entity.Orders_Address_County;
            Orders_Address_StreetAddress = entity.Orders_Address_StreetAddress;
            Orders_Address_Zip = entity.Orders_Address_Zip;
            Orders_Address_Name = entity.Orders_Address_Name;
            Orders_Address_Phone_Countrycode = entity.Orders_Address_Phone_Countrycode;
            Orders_Address_Phone_Areacode = entity.Orders_Address_Phone_Areacode;
            Orders_Address_Phone_Number = entity.Orders_Address_Phone_Number;
            Orders_Address_Mobile = entity.Orders_Address_Mobile;
            Orders_DeliveryTime_ID = entity.Orders_Delivery_Time_ID;
            Orders_Delivery = entity.Orders_Delivery;
            Orders_Delivery_Name = entity.Orders_Delivery_Name;
            Orders_Payway = entity.Orders_Payway;
            Orders_Payway_Name = entity.Orders_Payway_Name;
            Orders_PayType_Name = entity.Orders_PayType_Name;
            Orders_Note = entity.Orders_Note;
            Orders_Admin_Note = entity.Orders_Admin_Note;
            Orders_Admin_Sign = entity.Orders_Admin_Sign;
            Orders_Site = entity.Orders_Site;
            Orders_SourceType = entity.Orders_SourceType;
            Orders_Source = entity.Orders_Source;
            Orders_VerifyCode = entity.Orders_VerifyCode;
            Orders_Addtime = entity.Orders_Addtime;
        }
        entity = null;

        Member_ID = 0;
        Member_Email = "";
        Member_NickName = "";
        Member_Sex = 0;
        Member_Name = "";
        Member_Phone_Areacode = "";
        Member_Phone_Number = "";
        Member_Mobile = "";
        Member_StreetAddress = "";
        Member_State = "";
        Member_City = "";
        Member_County = "";
        Member_Zip = "";

        MemberInfo memberInfo = member.GetMemberByID(Orders_BuyerID);
        if (memberInfo != null)
        {
            Member_ID = memberInfo.Member_ID;
            Member_Email = memberInfo.Member_Email;
            Member_NickName = memberInfo.Member_NickName;

            MemberProfileInfo ProfileInfo = memberInfo.MemberProfileInfo;
            if (ProfileInfo != null)
            {
                Member_Sex = ProfileInfo.Member_Sex;
                Member_Name = ProfileInfo.Member_Name;
                Member_Phone_Areacode = ProfileInfo.Member_Phone_Areacode;
                Member_Phone_Number = ProfileInfo.Member_Phone_Number;
                Member_Mobile = ProfileInfo.Member_Mobile;
                Member_StreetAddress = ProfileInfo.Member_StreetAddress;
                Member_State = ProfileInfo.Member_State;
                Member_City = ProfileInfo.Member_City;
                Member_County = ProfileInfo.Member_County;
                Member_Zip = ProfileInfo.Member_Zip;
            }
            ProfileInfo = null;
        }
        memberInfo = null;

        string TemplateContent = GetPrintTemplate("ORDERS_FREIGHT_PRINT");

        string tmp_head, tmp_list, tmp_bottom;
        string[] PrintTemplate;
        TemplateContent = TemplateContent.Replace("{site_name}", Application["site_name"].ToString());
        TemplateContent = TemplateContent.Replace("{site_url}", Application["site_url"].ToString());
        TemplateContent = TemplateContent.Replace("{site_tel}", Application["site_tel"].ToString());
        TemplateContent = TemplateContent.Replace("{site_email}", Application["site_email"].ToString());


        Regex Regex = new Regex("{for}");
        PrintTemplate = Regex.Split(TemplateContent);
        Regex = null;
        tmp_head = PrintTemplate[0];

        Regex = new Regex("{next}");
        PrintTemplate = Regex.Split(PrintTemplate[1]);
        Regex = null;
        tmp_list = PrintTemplate[0];
        tmp_bottom = PrintTemplate[1];

        //基本信息
        tmp_head = tmp_head.Replace("{orders_addtime}", Orders_Addtime.ToString());
        tmp_head = tmp_head.Replace("{orders_sn}", Orders_SN);
        tmp_head = tmp_head.Replace("{orders_note}", Orders_Note);
        tmp_head = tmp_head.Replace("{orders_barcode}", CreateBarCode(Orders_SN, 30, 1, true));

        //会员信息
        tmp_head = tmp_head.Replace("{member_nickname}", Member_NickName);
        tmp_head = tmp_head.Replace("{member_name}", Member_Name);
        tmp_head = tmp_head.Replace("{member_phone}", "+86-" + Member_Phone_Areacode + "-" + Member_Phone_Number);

        Response.Write(tmp_head);
        string Info = "";
        int i = 0;
        IList<OrdersGoodsInfo> goodses = MyBLL.GetGoodsListByOrderID(Orders_ID);
        if (goodses != null)
        {
            foreach (OrdersGoodsInfo goods in goodses)
            {
                i++;
                Info = tmp_list;
                Info = Info.Replace("{list_num}", i.ToString());
                Info = Info.Replace("{orders_goods_productname}", goods.Orders_Goods_Product_Name);
                Info = Info.Replace("{orders_goods_spec}", goods.Orders_Goods_Product_Spec);
                Info = Info.Replace("{orders_goods_maker}", goods.Orders_Goods_Product_Maker);
                Info = Info.Replace("{orders_goods_mktprice}", Public.DisplayCurrency(goods.Orders_Goods_Product_MKTPrice));
                Info = Info.Replace("{orders_goods_price}", Public.DisplayCurrency(goods.Orders_Goods_Product_Price));
                Info = Info.Replace("{orders_goods_amount}", goods.Orders_Goods_Amount.ToString());
                Info = Info.Replace("{orders_goods_coin}", goods.Orders_Goods_Product_Coin.ToString());
                Info = Info.Replace("{orders_goods_allprice}", Public.DisplayCurrency(goods.Orders_Goods_Product_Price * goods.Orders_Goods_Amount));
                Response.Write(Info);
            }
        }

        tmp_bottom = tmp_bottom.Replace("{orders_note}", Orders_Note);

        tmp_bottom = tmp_bottom.Replace("{orders_total_favorprice}", Public.DisplayCurrency(Orders_Total_PriceDiscount));
        tmp_bottom = tmp_bottom.Replace("{orders_total_allprice}", Public.DisplayCurrency(Orders_Total_AllPrice));
        tmp_bottom = tmp_bottom.Replace("{orders_delivery_name}", Orders_Delivery_Name);
        tmp_bottom = tmp_bottom.Replace("{orders_address_name}", Orders_Address_Name);
        tmp_bottom = tmp_bottom.Replace("{orders_address_phone}", Orders_Address_Phone_Areacode + "-" + Orders_Address_Phone_Number);
        tmp_bottom = tmp_bottom.Replace("{orders_address_mobile}", Orders_Address_Mobile);
        tmp_bottom = tmp_bottom.Replace("{orders_address}", Addr.DisplayAddress(Orders_Address_State, Orders_Address_City, Orders_Address_Country) + " " + Orders_Address_StreetAddress);
        tmp_bottom = tmp_bottom.Replace("{orders_address_zip}", Orders_Address_Zip);

        tmp_bottom = tmp_bottom.Replace("{orders_delivery_name}", Orders_Delivery_Name);
        tmp_bottom = tmp_bottom.Replace("{orders_payway_name}", Orders_Payway_Name);
        tmp_bottom = tmp_bottom.Replace("{orders_payway_status}", PaymentStatus(Orders_PaymentStatus));
        tmp_bottom = tmp_bottom.Replace("送货时间：{orders_delivery_time}", "付款条件：" + Orders_PayType_Name);

        Response.Write(tmp_bottom);
    }

    #endregion

    #region "订单导出"

    //订单产品导出
    public void Orders_Goods_Export()
    {
        string OrdersArry = tools.CheckStr(Request["Orders_id"]);
        if (OrdersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }
        if (tools.Left(OrdersArry, 1) == ",") { OrdersArry = OrdersArry.Remove(0, 1); }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "订单号", "订单状态", "下单时间",  "支付方式",  "总价","商品总价","商品编号", "商品名称", "单价", "数量","商品规格","生产企业" };
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_ID", "in", OrdersArry));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));

        IList<OrdersInfo> ordersinfo = MyBLL.GetOrderss(Query);
        if (ordersinfo != null)
        {
            foreach (OrdersInfo entity in ordersinfo)
            {
                IList<OrdersGoodsInfo> ordersgoodsinfo = MyBLL.GetGoodsListByOrderID(entity.Orders_ID);
                if (ordersgoodsinfo != null)
                {
                    foreach (OrdersGoodsInfo goods in ordersgoodsinfo)
                    {
                        if (goods.Orders_Goods_Type != 2 || (goods.Orders_Goods_Type == 2 && goods.Orders_Goods_ParentID > 0))
                        {

                            Orders_ID = entity.Orders_ID;
                            dr = dt.NewRow();

                            dr["订单号"] = entity.Orders_SN;
                            dr["订单状态"] = OrdersStatus(entity.Orders_Status);
                            dr["下单时间"] = entity.Orders_Addtime;

                            //orderspayment = Mypayment.GetOrdersPaymentByOrdersID(entity.Orders_ID, 0);
                            //if (orderspayment != null)
                            //{
                            //    dr["付款单号"] = orderspayment.Orders_Payment_DocNo;
                            //}
                            //else
                            //{
                            //    dr["付款单号"] = "";
                            //}
                            //dr["付款单状态"] = PaymentStatus(entity.Orders_PaymentStatus);
                            //dr["付款时间"] = entity.Orders_PaymentStatus_Time;

                            //ordersdelivery = Mydelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 0, Public.GetUserPrivilege());
                            //if (ordersdelivery != null)
                            //{
                            //    dr["发货单号"] = ordersdelivery.Orders_Delivery_DocNo;
                            //}
                            //else
                            //{
                            //    dr["发货单号"] = "";
                            //}

                            //dr["发货单状态"] = DeliveryStatus(entity.Orders_DeliveryStatus);
                            //dr["发货时间"] = entity.Orders_DeliveryStatus_Time;

                            //dr["配送方式"] = entity.Orders_Delivery_Name;
                            dr["支付方式"] = entity.Orders_Payway_Name;

                            //dr["产品批号"] = goods.U_Orders_Goods_Product_BatchCode;
                            //dr["采购渠道"] = goods.U_Orders_Goods_Product_BuyChannel;
                            //dr["采购数量"] = goods.U_Orders_Goods_Product_BuyAmount;
                            //dr["采购价格"] = goods.U_Orders_Goods_Product_BuyPrice;

                            dr["总价"] = entity.Orders_Total_AllPrice;
                            dr["商品总价"] = entity.Orders_Total_Price;
                            //dr["运费"] = entity.Orders_Total_Freight;

                            dr["商品编号"] = goods.Orders_Goods_Product_Code;
                            dr["商品名称"] = goods.Orders_Goods_Product_Name;
                            dr["单价"] = goods.Orders_Goods_Product_Price;
                            dr["数量"] = goods.Orders_Goods_Amount;
                            dr["商品规格"] = goods.Orders_Goods_Product_Spec;
                            dr["生产企业"] = goods.Orders_Goods_Product_Maker;

                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
        }

        Public.toExcel(dt);
    }

    //订单导出
    public void Orders_Export()
    {
        string OrdersArry = tools.CheckStr(Request["Orders_id"]);
        if (OrdersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(OrdersArry, 1) == ",") { OrdersArry = OrdersArry.Remove(0, 1); }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "订单号", "订单状态", "下单时间",  "支付方式", "公司名称", "电话",  "手机", "Email", "地址", "邮编", "收货人", "收货人电话", "收货人手机", "收货人地址", "收货人邮编", "总价", "商品总价", "商品清单", "客服名称"};
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        int Orders_ID = 0;
        QueryInfo Query = new QueryInfo();
        SupplierInfo supplierinfo = null;
        MemberProfileInfo memberprofile = null;
        OrdersPaymentInfo orderspayment = null;
        OrdersDeliveryInfo ordersdelivery = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_ID", "in", OrdersArry));
        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "DESC"));

        IList<OrdersInfo> ordersinfo = MyBLL.GetOrderss(Query);
        if (ordersinfo != null)
        {
            foreach (OrdersInfo entity in ordersinfo)
            {
                Orders_ID = entity.Orders_ID;
                dr = dt.NewRow();

                dr["订单号"] = entity.Orders_SN;
                dr["订单状态"] = OrdersStatus(entity.Orders_Status);
                dr["下单时间"] = entity.Orders_Addtime;
                //orderspayment = Mypayment.GetOrdersPaymentByOrdersID(entity.Orders_ID, 0);
                //if (orderspayment != null)
                //{
                //    dr["付款单号"] = orderspayment.Orders_Payment_DocNo;
                //}
                //else
                //{
                //    dr["付款单号"] = "";
                //}
                //dr["付款单状态"] = PaymentStatus(entity.Orders_PaymentStatus);
                //dr["付款时间"] = entity.Orders_PaymentStatus_Time;

                //ordersdelivery = Mydelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 0, Public.GetUserPrivilege());
                //if (ordersdelivery != null)
                //{
                //    dr["发货单号"] = ordersdelivery.Orders_Delivery_DocNo;
                //}
                //else
                //{
                //    dr["发货单号"] = "";
                //}
                //dr["发货单状态"] = DeliveryStatus(entity.Orders_DeliveryStatus);
                //dr["发货时间"] = entity.Orders_DeliveryStatus_Time;

                //dr["配送方式"] = entity.Orders_Delivery_Name;
                dr["支付方式"] = entity.Orders_Payway_Name;

                supplierinfo = new SupplierInfo();
                supplierinfo = MySupplier.GetSupplierByID(entity.Orders_BuyerID,Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    dr["公司名称"] = supplierinfo.Supplier_CompanyName;
                    dr["电话"] = supplierinfo.Supplier_Phone;

                    dr["手机"] = supplierinfo.Supplier_Mobile;
                    dr["Email"] = supplierinfo.Supplier_Email;
                    dr["地址"] = Addr.DisplayAddress(supplierinfo.Supplier_State, supplierinfo.Supplier_City, supplierinfo.Supplier_County) + " " + supplierinfo.Supplier_Address;
                    dr["邮编"] = supplierinfo.Supplier_Zip;
                }
                else
                {
                    dr["公司名称"] = "";
                    dr["电话"] = "";
                    dr["手机"] = "";
                    dr["Email"] = "";
                    dr["地址"] = "";
                    dr["邮编"] = "";
                }

                dr["收货人"] = entity.Orders_Address_Name;
                dr["收货人电话"] = entity.Orders_Address_Phone_Areacode + "-" + entity.Orders_Address_Phone_Number;
                dr["收货人手机"] = entity.Orders_Address_Mobile;
                dr["收货人地址"] = Addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + " " + entity.Orders_Address_StreetAddress;
                dr["收货人邮编"] = entity.Orders_Address_Zip;

                dr["总价"] = entity.Orders_Total_AllPrice;
                dr["商品总价"] = entity.Orders_Total_Price;

                dr["商品清单"] = orders_excel_goods(entity.Orders_ID);
                dr["客服名称"] = entity.Orders_Source;
                //dr["用户名"] = entity.Orders_Source;

                dt.Rows.Add(dr);

            }
        }

        Public.toExcel(dt);
    }

    //订单产品
    public string orders_excel_goods(int orders_ID)
    {
        string result_value = "";
        IList<OrdersGoodsInfo> ordersgoodsinfo = MyBLL.GetGoodsListByOrderID(orders_ID);
        if (ordersgoodsinfo != null)
        {
            foreach (OrdersGoodsInfo goods in ordersgoodsinfo)
            {
                if (goods.Orders_Goods_Type != 2 || (goods.Orders_Goods_Type == 2 && goods.Orders_Goods_ParentID > 0))
                {
                    result_value = result_value + goods.Orders_Goods_Product_Name + "(数量：" + goods.Orders_Goods_Amount + "；单价：" + Public.DisplayCurrency(goods.Orders_Goods_Product_Price) + "；采购渠道：" + goods.U_Orders_Goods_Product_BuyChannel + "；采购数量：" + goods.U_Orders_Goods_Product_BuyAmount + "；采购价格：" + goods.U_Orders_Goods_Product_BuyPrice + ")；";
                }
            }
        }
        return result_value;
    }

    #endregion

    #region "邮件处理"

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

    #endregion

    #region "支付处理函数"

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

    #endregion

    #region 拆单处理

    public string OrdersSplit_SelectGoods(int Orders_ID)
    {
        int i = 0;
        string strHTML = "";
        int stock = 0;
        OrdersInfo ordersinfo = new OrdersInfo();
        ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo == null)
        {
            return strHTML;
        }
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        ProductStockInfo productstockinfo = new ProductStockInfo();
        //列出商品信息
        IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">";
        strHTML += "    <tr class=\"goods_head\">";
        strHTML += "        <td>商品编号</td>";
        strHTML += "        <td>商品</td>";
        strHTML += "        <td>规格</td>";
        strHTML += "        <td>生产企业</td>";
        //strHTML += "        <td>市场价</td>";
        strHTML += "        <td>单价</td>";
        strHTML += "        <td>数量</td>";
        strHTML += "        <td>拆分数量</td>";
        strHTML += "        <td>可用库存</td>";
        strHTML += "    </tr>";
        if (GoodsListAll != null)
        {
            IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(GoodsListAll, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;

            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                stock = 0;
                packagestockinfo.Product_Stock_Amount = 0;
                packagestockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_Amount = 0;
                //子商品信息
                GoodsListSub = OrdersGoodsSearch(GoodsListAll, entity.Orders_Goods_ID);

                strHTML += "    <tr class=\"goods_list\">";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
                strHTML += "        <td><table align=\"left\">";
                strHTML += "<tr>";

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
                
                strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Amount + "</td>";

                strHTML += "        <td><input type=\"text\" size=\"10\" onkeyup=\"CheckInputAmount(this, " + entity.Orders_Goods_Amount + ");\" name=\"splitamount_" + entity.Orders_Goods_ID + "\" /></td>";
                if (entity.Orders_Goods_Type != 2)
                {
                    productstockinfo = Get_Productcount(entity.Orders_Goods_Product_ID);
                    stock = productstockinfo.Product_Stock_Amount;

                    if (stock <= 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                    }
                    else if (stock > 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                    }
                    else if (productstockinfo.Product_Stock_IsNoStock == 1)
                    {
                        strHTML += "        <td class=\"t12_green\">零库存</td>";
                    }
                }
                else
                {
                    PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (package != null)
                    {
                        IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                        if (packageproducts != null)
                        {

                            packagestockinfo = Get_Package_Count(packageproducts);
                            stock = packagestockinfo.Product_Stock_Amount;
                        }
                    }
                    if (stock <= 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                    }
                    else if (stock > 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                    }
                    else if (packagestockinfo.Product_Stock_IsNoStock == 1)
                    {
                        strHTML += "        <td class=\"t12_green\">零库存</td>";
                    }

                }

                strHTML += "    </tr>";

                if (GoodsListSub.Count > 0)
                {
                    i = 0;
                    foreach (OrdersGoodsInfo goodsSub in GoodsListSub)
                    {
                        i = i + 1;
                        strHTML += "    <tr class=\"goods_list\" id=\"subgoods_" + entity.Orders_Goods_ID + "_" + i + "\">";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Code + "</td>";
                        strHTML += "        <td><table align=\"left\">";
                        strHTML += "<tr>";

                        if (goodsSub.Orders_Goods_Type == 1)
                        {
                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + goodsSub.Orders_Goods_Product_Img + "\" /></td>";
                            strHTML += "    <td><span class=\"t12_red\">[赠品]</span> " + goodsSub.Orders_Goods_Product_Name + "</td>";
                        }
                        else
                        {

                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + Public.FormatImgURL(goodsSub.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
                            strHTML += "    <td>" + goodsSub.Orders_Goods_Product_Name;
                            strHTML += "</td>";
                        }

                        strHTML += "</tr>";
                        strHTML += "</table></td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Spec + "</td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Maker + "</td>";
                        
                        strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_MKTPrice) + "</td>";
                        strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_Price) + "</td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Amount + "</td>";
                        strHTML += "        <td>拆分数量</td>";
                        strHTML += "        <td>--</td>";

                        strHTML += "    </tr>";
                    }
                }
                GoodsListSub = null;

            }
        }
        strHTML += "</table>";

        GoodsListAll = null;
        ordersinfo = null;

        return strHTML;
    }

    public string GetSubsetOrdersGoodsByOrdersID(int Orders_ID)
    {
        int i = 0;
        string strHTML = "";
        int stock = 0;
        OrdersInfo ordersinfo = new OrdersInfo();
        ordersinfo = GetOrdersByID(Orders_ID);
        if (ordersinfo == null)
        {
            return strHTML;
        }
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        ProductStockInfo productstockinfo = new ProductStockInfo();
        //列出商品信息
        IList<OrdersGoodsInfo> GoodsListAll = MyBLL.GetGoodsListByOrderID(Orders_ID);
        strHTML += "<form name=\"frm_batch\" action=\"orders_do.aspx\" method=\"post\">";
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"goods_table\">";
        strHTML += "    <tr class=\"goods_head\">";
        strHTML += "        <td>商品编号</td>";
        strHTML += "        <td>商品</td>";
        strHTML += "        <td>规格</td>";
        strHTML += "        <td>生产企业</td>";
        //strHTML += "        <td>市场价</td>";
        strHTML += "        <td>单价</td>";
        strHTML += "        <td>数量</td>";
        strHTML += "        <td>获赠" + Application["Coin_Name"].ToString() + "</td>";
        strHTML += "        <td>合计</td>";
        strHTML += "        <td>可用库存</td>";
        strHTML += "    </tr>";
        if (GoodsListAll != null)
        {
            IList<OrdersGoodsInfo> GoodsList = OrdersGoodsSearch(GoodsListAll, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;

            foreach (OrdersGoodsInfo entity in GoodsList)
            {
                stock = 0;
                packagestockinfo.Product_Stock_Amount = 0;
                packagestockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_IsNoStock = 0;
                productstockinfo.Product_Stock_Amount = 0;
                //子商品信息
                GoodsListSub = OrdersGoodsSearch(GoodsListAll, entity.Orders_Goods_ID);

                strHTML += "    <tr class=\"goods_list\">";
                strHTML += "        <td>" + entity.Orders_Goods_Product_Code + "</td>";
                strHTML += "        <td><table align=\"left\">";
                strHTML += "<tr>";

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
                strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_MKTPrice) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                strHTML += "        <td>" + entity.Orders_Goods_Amount + "</td>";
                strHTML += "        <td>" + (entity.Orders_Goods_Product_Coin * entity.Orders_Goods_Amount) + "</td>";
                strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                if (entity.Orders_Goods_Type != 2)
                {
                    productstockinfo = Get_Productcount(entity.Orders_Goods_Product_ID);
                    stock = productstockinfo.Product_Stock_Amount;

                    if (stock <= 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                    }
                    else if (stock > 0 && productstockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                    }
                    else if (productstockinfo.Product_Stock_IsNoStock == 1)
                    {
                        strHTML += "        <td class=\"t12_green\">零库存</td>";
                    }
                }
                else
                {
                    PackageInfo package = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, Public.GetUserPrivilege());
                    if (package != null)
                    {
                        IList<PackageProductInfo> packageproducts = package.PackageProductInfos;
                        if (packageproducts != null)
                        {

                            packagestockinfo = Get_Package_Count(packageproducts);
                            stock = packagestockinfo.Product_Stock_Amount;
                        }
                    }
                    if (stock <= 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_red\">" + stock + "</td>";
                    }
                    else if (stock > 0 && packagestockinfo.Product_Stock_IsNoStock == 0)
                    {
                        strHTML += "        <td class=\"t12_green\">" + stock + "</td>";
                    }
                    else if (packagestockinfo.Product_Stock_IsNoStock == 1)
                    {
                        strHTML += "        <td class=\"t12_green\">零库存</td>";
                    }

                }

                strHTML += "    </tr>";

                if (GoodsListSub.Count > 0)
                {
                    i = 0;
                    foreach (OrdersGoodsInfo goodsSub in GoodsListSub)
                    {
                        i = i + 1;
                        strHTML += "    <tr class=\"goods_list\" id=\"subgoods_" + entity.Orders_Goods_ID + "_" + i + "\">";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Code + "</td>";
                        strHTML += "        <td><table align=\"left\">";
                        strHTML += "<tr>";

                        if (goodsSub.Orders_Goods_Type == 1)
                        {
                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + goodsSub.Orders_Goods_Product_Img + "\" /></td>";
                            strHTML += "    <td><span class=\"t12_red\">[赠品]</span> " + goodsSub.Orders_Goods_Product_Name + "</td>";
                        }
                        else
                        {

                            strHTML += "    <td width=\"42\" height=\"42\" class=\"img_border\"><img width=\"36\" height=\"36\" src=\"" + Public.FormatImgURL(goodsSub.Orders_Goods_Product_Img, "thumbnail") + "\" /></td>";
                            strHTML += "    <td>" + goodsSub.Orders_Goods_Product_Name;
                            strHTML += "</td>";
                        }

                        strHTML += "</tr>";
                        strHTML += "</table></td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Spec + "</td>";
                        strHTML += "        <td>" + goodsSub.Orders_Goods_Product_Maker + "</td>";
                        strHTML += "        <td class=\"price_original_big\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_MKTPrice) + "</td>";
                        strHTML += "        <td class=\"price_list\">" + Public.DisplayCurrency(goodsSub.Orders_Goods_Product_Price) + "</td>";
                        strHTML += "        <td>1×" + (goodsSub.Orders_Goods_Amount / entity.Orders_Goods_Amount) + "</td>";
                        strHTML += "        <td>--</td>";
                        strHTML += "        <td>--</td>";
                        strHTML += "        <td>--</td>";

                        strHTML += "    </tr>";
                    }
                }
                GoodsListSub = null;

            }
        }
        strHTML += "</table>";
        GoodsListAll = null;
        ordersinfo = null;

        return strHTML;
    }



    #endregion 

}


