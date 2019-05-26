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
using System.Linq;

using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
/// 商品中心使用
/// </summary>
public class Product
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private IProduct Myproduct;
    private IProductWholeSalePrice MySalePrice;
    private IOrders MyOrders;
    private IBrand MyBrand;
    private ICategory MyCate;
    private ITools tools;
    private IProductTag MyTag;
    private IProductType MyType;
    private IPackage MyPackage;
    private IProductTypeExtend MyExtend;
    private IProductReview MyReview;
    private Public_Class pub;
    private IMember MyMem;
    private IMemberGrade Mygrade;
    private IProductPrice Myprice;
    private IShoppingAsk Myshopask;
    private ISupplier Mysupplier;
    private IStockoutBooking Mysotckout;
    private IPromotionFavorFee MyFavorFee;
    private IPromotionLimit MyLimit;
    private IPromotionFavorGift MyGift;
    private IProductNotify Mynotify;
    private IPromotion MyPromotion;
    private Member memberclass;
    private AD adclass;
    private Orders orders;
    private IPromotionFavor MyFavor;
    private IPromotionCouponRule MyCouponRule;
    private ISupplierShop MyShop;
    private PageURL pageurl;
    private ISupplierShopEvaluate MyEvaluate;
    private IHomeLeftCate MyLeftCate;
    private IKeywordBidding MyKeywordBidding;
    private IAddr MyAddr;
    private IDeliveryWay MyDeliveryWay;
    private ISupplierPurchase MyPurchase;
    //private ISupplierPurchaseDetail MyPurchaseDetail;
    private Addr myaddr;
    Supplier supplier_class;
    private ISupplierPriceAsk MyPriceAsk;
    private IContract myContract;
    private IMemberFavorites MyFavorites;
    private IMemberPurchase MyMPurchase;
    private IKeywordsRanking MyRanking;
    private ISupplierMerchants MyMerchants;
    private ISupplierOnline MySupplierOnline;


    public string Domain = "";
    public Product()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyFavorites = MemberFavoritesFactory.CreateMemberFavorites();
        MyEvaluate = SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
        Myproduct = ProductFactory.CreateProduct();
        tools = ToolsFactory.CreateTools();
        MyCate = CategoryFactory.CreateCategory();
        MyBrand = BrandFactory.CreateBrand();
        MyTag = ProductTagFactory.CreateProductTag();
        MyType = ProductTypeFactory.CreateProductType();
        MyPackage = PackageFactory.CreatePackage();
        MyReview = ProductReviewFactory.CreateProductReview();
        MyExtend = ProductTypeExtendFactory.CreateProductTypeExtend();
        MyMem = MemberFactory.CreateMember();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
        Myprice = ProductPriceFactory.CreateProductPrice();
        Myshopask = ShoppingAskFactory.CreateShoppingAsk();
        Mysupplier = SupplierFactory.CreateSupplier();
        Mysotckout = StockoutBookingFactory.CreateStockoutBooking();
        MyFavorFee = PromotionFavorFeeFactory.CreatePromotionFavorFee();
        MyLimit = PromotionLimitFactory.CreatePromotionLimit();
        Mynotify = ProductNotifyFactory.CreateProductNotify();
        MyGift = PromotionFavorGiftFactory.CreatePromotionFavorGift();
        MyPromotion = PromotionFactory.CreatePromotion();
        MyShop = SupplierShopFactory.CreateSupplierShop();
        MyKeywordBidding = KeywordBiddingFactory.CreateKeywordBidding();
        pub = new Public_Class();
        memberclass = new Member();
        adclass = new AD();
        orders = new Orders();
        supplier_class = new Supplier();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyCouponRule = PromotionCouponRuleFactory.CreatePromotionFavorCoupon();
        MyOrders = OrdersFactory.CreateOrders();
        MyLeftCate = HomeLeftCateFactory.CreateHomeLeftCate();
        MyAddr = AddrFactory.CreateAddr();
        MyDeliveryWay = DeliveryWayFactory.CreateDeliveryWay();
        myaddr = new Addr();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        MyPurchase = SupplierPurchaseFactory.CreateSupplierPurchase();
        //MyPurchaseDetail = SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
        MyPriceAsk = SupplierPriceAskFactory.CreateSupplierPriceAsk();
        myContract = ContractFactory.CreateContract();
        MySalePrice = ProductWholeSalePriceFactory.CreateProductWholeSalePrice();
        MyMPurchase = MemberPurchaseFactory.CreateMemberPurchase();
        MyRanking = KeywordsRankingFactory.CreateKeywordsRanking();
        MyMerchants = SupplierMerchantsFactory.CreateSupplierMerchants();
        MySupplierOnline = SupplierOnlineFactory.CreateSupplierOnline();
    }

    public void AddKeywordRanking(int type, string keyword)
    {
        int ID = tools.CheckInt(Request.Form["ID"]);
        int Type = type;
        string Keyword = keyword;
        DateTime addtime = DateTime.Now;
        string Site = pub.GetCurrentSite();

        KeywordsRankingInfo entity = new KeywordsRankingInfo();
        entity.ID = ID;
        entity.Type = Type;
        entity.Keyword = Keyword;
        entity.addtime = addtime;
        entity.Site = Site;

        MyRanking.AddKeywordsRanking(entity);
    }


    //获取所有子分类
    public string Get_All_SubCate(int Cate_id)
    {
        string Cate_Arry = MyCate.Get_All_SubCateID(Cate_id);
        return Cate_Arry;
    }



    //获取第一级子分类
    public string Get_First_SubCate(int Cate_id)
    {
        string Cate_Arry = Cate_id.ToString();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Cate_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "Desc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                Cate_Arry = Cate_Arry + "," + entity.Cate_ID.ToString();
            }
        }
        return Cate_Arry;
    }

    /// <summary>
    /// 根据标签名称获取商品集合
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <param name="Tag_Name"></param>
    /// <returns></returns>   
    public string GetTagProductList(int Show_Num, string Tag_Name)
    {
        StringBuilder strHTML = new StringBuilder();
        //获取推荐标签信息
        ProductTagInfo Taginfo = MyTag.GetProductTagByValue(Tag_Name, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (Taginfo != null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = Show_Num;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID = " + Taginfo.Product_Tag_ID + ""));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
            IList<ProductInfo> listProducts = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            strHTML.Append("    <ul>");
            if (listProducts != null)
            {
                int i = 0;
                foreach (ProductInfo listProduct in listProducts)
                {
                    i++;
                    if (i != 5)
                    {
                        strHTML.Append("      <li><div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, listProduct.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(listProduct.Product_Img, "thumbnail") + "\" width=\"229\" height=\"155\" /></a></div><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, listProduct.Product_ID.ToString()) + "\">" + listProduct.Product_Name + "</a></p></li>");
                    }
                    else
                    {
                        strHTML.Append("      <li style=\"margin:0px;\"><div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, listProduct.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(listProduct.Product_Img, "thumbnail") + "\" width=\"229\" height=\"155\" /></a></div><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, listProduct.Product_ID.ToString()) + "\">" + listProduct.Product_Name + "</a></p></li>");
                    }

                }

            }
            strHTML.Append("    </ul>");
            return strHTML.ToString();
        }
        else
        {
            return null;
        }
    }

    public int Get_TopCate(int Cate_id)
    {

        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 0;
        //Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "=", Cate_id.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        //Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "Desc"));
        CategoryInfo Cateinfo = MyCate.GetCategoryByID(Cate_id, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Cateinfo != null)
        {
            if (Cateinfo.Cate_ParentID != 0)
            {
                return Get_TopCate(Cateinfo.Cate_ParentID);
            }
            else
            {
                return Cateinfo.Cate_ID;
            }
        }
        else
        {
            return 0;
        }

    }

    //获取指定类别下全部商品编号
    public string Get_All_CateProductID(string Cate_Arry)
    {
        string ProudctID_Arry = "";
        ProudctID_Arry = Myproduct.GetCateProductID(Cate_Arry);
        return ProudctID_Arry;
    }

    //获取品牌
    public BrandInfo GetBrandInfoByID(int ID)
    {
        return MyBrand.GetBrandByID(ID, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
    }

    public string getProductBrands(int cate_id)
    {
        string product_ids = "";
        string brand_ids = ",0,";
        string brand_list = "";
        string cate_ids = "";
        int Product_Review_Average = 0;
        cate_ids = Get_All_SubCate(cate_id);
        QueryInfo Query1 = new QueryInfo();

        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        if (cate_ids.Length > 0 && cate_id > 0)
        {
            Query1.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cate_ids));
            Query1.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cate_ids + ")"));
        }
        Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
        IList<ProductInfo> products = Myproduct.GetProducts(Query1, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (products != null)
        {
            foreach (ProductInfo productinfo in products)
            {
                if (brand_ids.IndexOf(productinfo.Product_BrandID.ToString()) > 0)
                { }
                else
                {
                    brand_ids += productinfo.Product_BrandID + ",";
                }
            }

        }
        brand_ids = brand_ids.Substring(1, brand_ids.Length - 2);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_ID", "in", brand_ids));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "ASC"));
        IList<BrandInfo> entitys = MyBrand.GetBrands(Query, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        brand_list += "<div class=\"brand\">";

        int i = 0;
        if (entitys != null)
        {
            brand_list += "<span><a href=\"/product/brand_list.aspx\">更多品牌>></a></span>";
            foreach (BrandInfo entity in entitys)
            {
                i++;
                brand_list += "<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(cate_id.ToString(), entity.Brand_ID.ToString(), "", "", "")) + "\"><img src=\"" + pub.FormatImgURL(entity.Brand_Img, "fullpath") + "\"  onload=\"javascript:AutosizeImage(this,85,50);\"/></a>";
            }
        }
        brand_list += " </div>";
        return brand_list;

    }

    //根据编号获取类别信息
    public CategoryInfo GetCategoryByID(int Cate_ID)
    {
        return MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
    }

    public string GetCategoryNameByID(int Cate_ID)
    {
        CategoryInfo entity = GetCategoryByID(Cate_ID);
        if (entity != null)
        {
            return entity.Cate_Name;
        }
        else
        {
            return "";
        }
    }

    public CategoryInfo GetCategoryByName(string Name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Name", "=", Name));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        IList<CategoryInfo> entitys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (entitys != null)
        {
            return entitys[0];
        }
        return null;
    }

    //商品有效性检查
    public bool Check_Product_Valid(ProductInfo productinfo)
    {
        if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string Select_Search_Cate(int cate_id)
    {
        string select_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 12;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                if (cate_id == entity.Cate_ID)
                {
                    select_list = select_list + "<option value=\"" + entity.Cate_ID + "\" selected>" + entity.Cate_Name + "</option>";
                }
                else
                {
                    select_list = select_list + "<option value=\"" + entity.Cate_ID + "\">" + entity.Cate_Name + "</option>";
                }

            }
        }
        return select_list;
    }

    //获取会员价格
    public double Get_Member_Price(int product_id, double product_price)
    {
        return pub.Get_Member_Price(product_id, product_price);
    }

    //获取指定会员等级价格
    public double Get_Membergrade_Price(int member_grade, double product_price)
    {
        return pub.Get_MemberGrade_Price(member_grade, product_price);
    }

    //获取指定分类指定标签商品信息  /**/
    public IList<ProductInfo> GetCateTagProduct(int Show_Num, int cate_id, string tag)
    {
        //获取所有子类编号
        string sub_cate = Get_All_SubCate(cate_id);
        //获取推荐标签信息
        ProductTagInfo Taginfo = MyTag.GetProductTagByValue(tag, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (Taginfo != null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = Show_Num;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID in (" + Taginfo.Product_Tag_ID.ToString() + ")"));
            if (sub_cate.Length > 0 && cate_id > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", sub_cate));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + sub_cate + ")"));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

            return Products;

        }
        else
        {
            return null;
        }
    }

    public string Search_Result_Display(string keyword)
    {
        keyword = tools.CheckStr(keyword);
        string return_value = ",";

        if (keyword != "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "%like%", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubName", "%like%", keyword));
            //Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "%like%", keyword));
            //Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "%like%", keyword));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Asc"));
            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (Products != null)
            {
                foreach (ProductInfo entity in Products)
                {
                    if (entity.Product_Name.IndexOf(keyword) >= 0)
                    {
                        return_value = return_value + entity.Product_Name + ",";
                    }
                    else if (entity.Product_SubName.IndexOf(keyword) >= 0)
                    {
                        return_value = return_value + entity.Product_SubName + ",";
                    }
                }
                return_value = return_value.Substring(0, return_value.Length - 1);
            }
        }
        return return_value;
    }

    //检查是否销售商家
    public int GetSupplierGrade(int supplier_ID)
    {
        SupplierInfo entity = Mysupplier.GetSupplierByID(supplier_ID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entity != null)
        {
            SupplierShopInfo shopinfo = MyShop.GetSupplierShopBySupplierID(supplier_ID);
            if (shopinfo != null)
            {
                return shopinfo.Shop_Type;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 过滤XSS攻击字符
    /// </summary>
    /// <param name="inVal">输入字符</param>
    /// <returns></returns>
    public string CheckXSS(string inVal)
    {

        if (inVal == null || inVal.Length == 0)
        {
            return "";
        }
        else
        {

            inVal = System.Text.RegularExpressions.Regex.Replace(inVal, "alert\\([^\\)]*\\)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return inVal.Replace("\"", "&quot;");
        }
    }


    #region 首页

    /// <summary>
    /// 首页二级类别
    /// </summary>
    /// <param name="cate_id"></param>
    /// <returns></returns>
    public string HomeSubCate(int cate_id, int num)
    {
        int i = 0;
        StringBuilder strHTML = new StringBuilder();


        strHTML.Append("<h2>");
        strHTML.Append("<span>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", cate_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        IList<CategoryInfo> entitys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (entitys != null)
        {
            foreach (CategoryInfo entity in entitys)
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append("<a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\">" + entity.Cate_Name + "</a>");
                }
                else
                {
                    strHTML.Append(" | <a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\">" + entity.Cate_Name + "</a>");
                }
            }
        }
        strHTML.Append(" <a href=\"/product/category.aspx?cate_id=" + cate_id + "\" class=\"a2\">更多>></a></span>");
        strHTML.Append("</span><i>" + num + "</i>");
        CategoryInfo categoryinfo = GetCategoryByID(cate_id);
        if (categoryinfo != null)
        {
            strHTML.Append(categoryinfo.Cate_Name);
        }

        strHTML.Append("</h2>");

        return strHTML.ToString();
    }

    /// <summary>
    /// 滑动类别显示
    /// </summary>
    /// <returns></returns>
    public string Home_Left_Cate_bak()
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"testbox\">");
        strHTML.Append("	<ul>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
        IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);

        List<CategoryInfo> FirstList = entityList.Where(P => P.Cate_ParentID == 0).ToList();
        List<CategoryInfo> SecondList = null;
        List<CategoryInfo> ThridList = null;
        // int i = 0;
        // string icount = "";
        int ifirst = 1, isecond = 1;

        if (FirstList != null)
        {
            foreach (CategoryInfo entity in FirstList)
            {
                //   i++;
                // icount = i.ToString("00");



                strHTML.Append("<li>");
                strHTML.Append("<dl class=\"a3\" style=\"height:43px; padding:6px 0 0 0;\">");
                strHTML.Append("<dt><a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\" >" + entity.Cate_Name + "</a></dt>");
                strHTML.Append("<dd>");

                strHTML.Append(adclass.AD_Show("menuCateAD", entity.Cate_ID.ToString(), "keyword_article", 4));
                // strHTML.Append("<a href=\"#\" target=\"_blank\">轴承阀门</a> <a href=\"#\" target=\"_blank\">电器元件</a> <a href=\"#\" target=\"_blank\">密封件</a>  <a href=\"#\" target=\"_blank\">通用</a>");
                strHTML.Append("</dd>");
                strHTML.Append("</dl>");




                //strHTML.Append("		<li><a class=\"a3\" href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\" style=\"background-image:url(" + pub.FormatImgURL(entity.Cate_Img, "fullpath") + "); background-repeat:no-repeat; background-position:12px 10px;\"><span>" + entity.Cate_Name + "</span></a>");
                strHTML.Append("			<div class=\"boxshow\" id=\"xs01\">");
                strHTML.Append("				<div class=\"boxshow_left\">");

                SecondList = entityList.Where(P => P.Cate_ParentID == entity.Cate_ID).ToList();
                if (SecondList != null)
                {
                    foreach (CategoryInfo second in SecondList)
                    {
                        strHTML.Append("					<dl class=\"dst5\">");
                        strHTML.Append("						<dt><a href=\"/product/category.aspx?cate_id=" + second.Cate_ID + "\">" + second.Cate_Name + "</a></dt>");
                        strHTML.Append("						<dd>");
                        ThridList = entityList.Where(P => P.Cate_ParentID == second.Cate_ID).ToList();

                        if (ThridList != null)
                        {
                            foreach (CategoryInfo thrid in ThridList)
                                strHTML.Append("<a href=\"/product/category.aspx?cate_id=" + thrid.Cate_ID + "\">" + thrid.Cate_Name + "</a>");
                        }
                        SecondList = null;

                        strHTML.Append("</dd>						<div class=\"clear\"></div>");
                        strHTML.Append("					</dl>");
                    }
                }
                SecondList = null;

                strHTML.Append("				</div>");
                strHTML.Append("				<div class=\"boxshow_right\">");
                strHTML.Append("					<div class=\"b_r_box\">");
                strHTML.Append("						<h3 class=\"title04\">促销信息</h3>");
                strHTML.Append("						<div class=\"fox\">");
                strHTML.Append("							<ul class=\"lst6\">");
                strHTML.Append(adclass.AD_Show("POPUP_MENU_FIRST", entity.Cate_ID.ToString(), "keyword7", 1));
                strHTML.Append("							</ul>");
                strHTML.Append("						</div>");
                strHTML.Append("					</div>");
                //strHTML.Append("					<div class=\"b_r_box\">");
                //strHTML.Append("						<h3 class=\"title04\">促销信息</h3>");
                //strHTML.Append("						<div class=\"fox\">");
                //strHTML.Append("							<ul class=\"lst6\">");
                //strHTML.Append(adclass.AD_Show("POPUP_MENU_SECOND", entity.Cate_ID.ToString(), "keyword7", 1));
                //strHTML.Append("							</ul>");
                //strHTML.Append("						</div>");
                //strHTML.Append("					</div>");
                strHTML.Append("				</div>");
                strHTML.Append("				<div class=\"clear\"></div>");
                strHTML.Append("			</div>");
                strHTML.Append("		</li>");


                ifirst++;
                if (ifirst == 13)
                {
                    break;
                }
            }

        }
        entityList = null;
        FirstList = null;

        strHTML.Append("	</ul>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }

    public string Home_Left_Cate_bak(int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
        IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);
        if (entityList == null)
            return string.Empty;

        List<CategoryInfo> FirstList = entityList.Where(P => P.Cate_ParentID == 0).ToList();
        List<CategoryInfo> subCate = null;
        List<CategoryInfo> ThirdList = null;

        if (FirstList != null)
        {
            string ImageURL;
            foreach (CategoryInfo entity in FirstList)
            {
                i++;
                ImageURL = entity.Cate_Img;
                if (ImageURL.Length > 0)
                    ImageURL = pub.FormatImgURL(ImageURL, "fullpath");
                else
                    ImageURL = "/images/icon1.jpg";


                strHTML.Append("<dl>");

                strHTML.Append("<dt><p><img src=\"" + ImageURL + "\" />" + entity.Cate_Name + "</p></dt>");

                if (i == 1)
                {
                    strHTML.Append("<dd>");
                }
                else
                {
                    strHTML.Append("<dd style=\"display:none;\">");
                }
                strHTML.Append("<ul>");

                subCate = entityList.Where(P => P.Cate_ParentID == entity.Cate_ID).ToList();
                if (subCate != null)
                {
                    foreach (CategoryInfo item in subCate)
                    {
                        strHTML.Append("<li>");
                        strHTML.Append("<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(item.Cate_ID.ToString(), "", "", "", "")) + "\"><span>></span>" + item.Cate_Name + "</a>");
                        strHTML.Append("<div class=\"li_box\">");
                        ThirdList = entityList.Where(P => P.Cate_ParentID == item.Cate_ID).ToList();
                        if (ThirdList != null)
                        {
                            foreach (CategoryInfo items in ThirdList)
                            {
                                strHTML.Append("<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(items.Cate_ID.ToString(), "", "", "", "")) + "\">" + items.Cate_Name + "</a>");
                            }
                        }
                        strHTML.Append("</div>");
                        strHTML.Append("</li>");
                    }
                }

                strHTML.Append("</ul>");
                strHTML.Append("</dd>");


                strHTML.Append("</dl>");
            }
        }
        return strHTML.ToString();
    }



    public string Home_Left_Cate()
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
        IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);
        if (entityList == null)
            return string.Empty;

        List<CategoryInfo> FirstList = entityList.Where(P => P.Cate_ParentID == 0).ToList();
        List<CategoryInfo> subCate = null;
        List<CategoryInfo> ThirdList = null;

        if (FirstList != null)
        {
            string ImageURL;
            foreach (CategoryInfo entity in FirstList)
            {
                i++;
                ImageURL = entity.Cate_Img;
                if (ImageURL.Length > 0)
                    ImageURL = pub.FormatImgURL(ImageURL, "fullpath");
                else
                    ImageURL = "/images/icon1.jpg";

                strHTML.Append("<ul>");

                subCate = entityList.Where(P => P.Cate_ParentID == entity.Cate_ID).ToList();
                if (subCate != null)
                {
                    strHTML.Append("    <li>");
                    int j = 0;
                    j++;
                    if (j < 2)
                    {
                        strHTML.Append("   <dl class=\"a3\" style=\"border-top: none;\">");

                    }
                    else
                    {
                        strHTML.Append("  <dl class=\"a3\">");

                    }

                    strHTML.Append("       <dt><a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\"><span style=\"float: left;padding-top:3.5px;\">");
                    strHTML.Append("           <img src=\"" + ImageURL + "\" width=\"23\" height=\"23\" /></span><span style=\"float: left; font-size: 14px; margin-left: 5px; font-weight: bold;\">" + entity.Cate_Name + "</span><span>></span></a></dt>");
                    strHTML.Append("       <dd>");
                    int i1 = 0;
                    foreach (CategoryInfo item in subCate)
                    {
                        i1++;

                        if (i1 < 4)
                        {
                            string Cate_Name = item.Cate_Name;

                            if (item.Cate_Name.Contains("耐火"))
                            {
                                Cate_Name = Cate_Name.Replace("耐火", "");
                            }
                            if (item.Cate_Name.Contains("不定性标准制品"))
                            {
                                Cate_Name = Cate_Name.Replace("不定性标准制品", "不定性制品");
                            }
                            if (i1 == 3)
                            {

                                if (Cate_Name != "纤维制品")
                                {
                                    strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\" >" + Cate_Name + "</a>");

                                }
                            }
                            if (i1 == 2)
                            {


                                if ((Cate_Name == "不定型制品"))
                                {
                                    strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\" style=\"padding-left: 5px\">" + Cate_Name + "</a>");
                                }
                                else
                                {
                                    strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\" >" + Cate_Name + "</a>");
                                }
                            }
                            if (i1 == 1)
                            {
                                if ((Cate_Name == "不定型制品"))
                                {
                                    strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\"  style=\"padding-left: 5px\" >" + Cate_Name + "</a>");
                                }
                                else
                                {
                                    strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\"  >" + Cate_Name + "</a>");
                                }

                            }
                        }

                    }
                    strHTML.Append("       </dd>");
                    strHTML.Append("   </dl>");




                    strHTML.Append("   <div class=\"boxshow\" id=\"xs01 style=\"display:b\"\">");
                    strHTML.Append("       <div class=\"boxshow_left\">");

                    foreach (CategoryInfo item in subCate)
                    {
                        strHTML.Append("           <dl class=\"dst5\">");
                        strHTML.Append("               <dt><a href=\"/product/category.aspx?cate_id=" + item.Cate_ID + "\"\">" + item.Cate_Name + "</a></dt>");
                        ThirdList = entityList.Where(P => P.Cate_ParentID == item.Cate_ID).ToList();
                        if (ThirdList != null)
                        {
                            strHTML.Append("               <dd>");
                            foreach (CategoryInfo items in ThirdList)
                            {
                                strHTML.Append("               <a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(items.Cate_ID.ToString(), "", "", "", "")) + "\">" + items.Cate_Name + "</a>");
                            }
                            strHTML.Append("               </dd>");
                        }
                        strHTML.Append("               <div class=\"clear\"></div>");
                        strHTML.Append("           </dl>");
                    }


                    strHTML.Append("       </div>");
                    strHTML.Append("       <div class=\"boxshow_right\">");
                    strHTML.Append("           <div class=\"b_r_box\">");
                    strHTML.Append("               <h3 class=\"title04\">优秀商家</h3>");
                    strHTML.Append("               <div class=\"fox\">");





                    QueryInfo QuerySupp = new QueryInfo();

                    string sql = "(select Supplier_RelateTag_SupplierID from Supplier_RelateTag where Supplier_RelateTag_TagID=4) ";
                    QuerySupp.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_ID", "in", sql));

                    QuerySupp.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
                    QuerySupp.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_IsHaveShop", "=", "1"));
                    QuerySupp.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_AuditStatus", "=", "1"));
                    QuerySupp.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Status", "=", "1"));
                    QuerySupp.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
                    IList<SupplierInfo> Supplierinfos = Mysupplier.GetSuppliers(QuerySupp, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (Supplierinfos != null)
                    {
                        foreach (var Supplierinfo in Supplierinfos)
                        {
                            SupplierShopInfo suppliershop = MyShop.GetSupplierShopBySupplierID(Supplierinfo.Supplier_ID);
                            //pub.GetShopDomain(shopinfos1.Shop_Domain)
                            if (suppliershop != null)
                            {
                                strHTML.Append("                  <a target=\"_blank\" href=\"" + pub.GetShopDomain(suppliershop.Shop_Domain) + "\">");

                                strHTML.Append("         " + Supplierinfo.Supplier_CompanyName + "");


                                strHTML.Append("       </a>");
                            }
                        }

                    }



                    strHTML.Append("               </div>");
                    strHTML.Append("           </div>");
                    strHTML.Append("       </div>");
                    strHTML.Append("       <div class=\"clear\"></div>");
                    strHTML.Append("   </div>");
                    strHTML.Append("  </li>");

                }
                strHTML.Append("</ul>");
            }
        }
        return strHTML.ToString();
    }


    public string SlideDivCate(string cate_id)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_ParentID", "=", cate_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_Active", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HomeLeftCateInfo.Home_Left_Cate_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("HomeLeftCateInfo.Home_Left_Cate_Sort", "ASC"));
        IList<HomeLeftCateInfo> entityList = MyLeftCate.GetHomeLeftCates(Query, pub.CreateUserPrivilege("d843afda-7680-45fa-bc00-32278bf77ae8"));

        QueryInfo inquire = null;
        IList<HomeLeftCateInfo> subList = null;

        int ICount = 1;

        if (entityList != null)
        {
            foreach (HomeLeftCateInfo entity in entityList)
            {

                ICount = 1;
                strHTML.Append("<dl class=\"dst2\"><dt><a href=\"" + entity.Home_Left_Cate_URL + "\" target=\"_blank\">" + entity.Home_Left_Cate_Name + "</a></dt>");

                inquire = new QueryInfo();
                inquire.PageSize = 0;
                inquire.CurrentPage = 1;
                inquire.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_ParentID", "=", entity.Home_Left_Cate_ID.ToString()));
                inquire.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_Active", "=", "1"));
                inquire.ParamInfos.Add(new ParamInfo("AND", "str", "HomeLeftCateInfo.Home_Left_Cate_Site", "=", pub.GetCurrentSite()));
                inquire.OrderInfos.Add(new OrderInfo("HomeLeftCateInfo.Home_Left_Cate_Sort", "ASC"));
                subList = MyLeftCate.GetHomeLeftCates(inquire, pub.CreateUserPrivilege("d843afda-7680-45fa-bc00-32278bf77ae8"));

                if (subList != null)
                {
                    strHTML.Append("<dd>");

                    foreach (HomeLeftCateInfo sub in subList)
                    {
                        if (ICount == 1)
                        {
                            strHTML.Append("<a href=\"" + entity.Home_Left_Cate_URL + "\" target=\"_blank\" style=\"padding-left:0px;\">" + sub.Home_Left_Cate_Name + "</a>");
                        }
                        else
                        {
                            strHTML.Append("| <a href=\"" + entity.Home_Left_Cate_URL + "\" target=\"_blank\">" + sub.Home_Left_Cate_Name + "</a>");
                        }
                        ICount++;

                    }
                    strHTML.Append("</dd>");
                }
                inquire = null;
                subList = null;

                strHTML.Append("		 <div class=\"clear\"></div>");
                strHTML.Append("		</dl>");
                ICount++;
            }


        }

        return strHTML.ToString();
    }

    public string SlideDivBrand(int cate_id)
    {
        string html = "";
        CategoryInfo Entity = GetCategoryByID(cate_id);
        if (Entity != null)
        {
            ProductTypeInfo TEntity = MyType.GetProductTypeByID(Entity.Cate_TypeID, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (TEntity != null)
            {
                IList<BrandInfo> BExtitys = TEntity.BrandInfos;
                if (BExtitys != null)
                {
                    int i = 0;
                    html += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                    html += "<tr>";
                    html += "<td colspan=\"2\" class=\"td_title\">推荐品牌</td>";
                    html += "</tr>";
                    foreach (BrandInfo BExtity in BExtitys)
                    {
                        i++;
                        html += "<tr>";
                        if (BExtity.Brand_IsRecommend == 1 && BExtity.Brand_IsActive == 1)
                        {
                            html += "<td class=\"td_content\"><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(cate_id.ToString(), BExtity.Brand_ID.ToString(), "", "", "")) + "\">" + BExtity.Brand_Name + "</a></td>";
                            if (i % 2 == 0)
                            {
                                html += "</tr><tr>";
                            }
                        }
                        html += "</tr>";
                    }
                    html += "<tr><td height=\"10\"></td></tr>";
                    html += "</table>";
                }
            }
            string ad = adclass.AD_Show("Home_Promotion_Text", Entity.Cate_ID.ToString(), "new_textlist", 0);
            if (ad != null && ad != "")
            {
                html += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                html += "<tr>";
                html += "<td class=\"td_title\">促销专题</td>";
                html += "</tr>";
                html += ad;
                html += "<tr><td height=\"10\"></td></tr>";
                html += "</table>";
            }
        }
        return html;
    }

    /// <summary>
    /// 列表页左侧类别
    /// </summary>
    /// <param name="Cate_ID">类别</param>
    /// <returns></returns>
    public string CategoryLeft(int Cate_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        string Cate_Name = "商品分类";
        int Cate_ParentID = 0;
        if (Cate_ID > 0)
        {
            CategoryInfo category = new CategoryInfo();
            category = MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
            if (category != null)
            {
                Cate_Name = category.Cate_Name;
                Cate_ParentID = category.Cate_ParentID;
            }
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<li><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity.Cate_ID.ToString(), "", "", "", "")) + "\">" + entity.Cate_Name + "</a></li>");
            }
        }
        else
        {
            strHTML.Append(CategoryLeft(Cate_ParentID));
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 首页标签商品
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <param name="Tag"></param>
    /// <returns></returns>
    public string Home_Tag_Product(int Show_Num, string Tag)
    {
        int ii = 0;
        StringBuilder strHTML = new StringBuilder();
        IList<ProductInfo> products = GetCateTagProduct(Show_Num, 0, Tag);
        if (products != null)
        {
            string targetURL;
            foreach (ProductInfo e in products)
            {
                ii++;
                targetURL = pageurl.FormatURL(pageurl.product_detail, e.Product_ID.ToString());
                if (ii == Show_Num)
                {
                    strHTML.Append("<li style=\"border-right:0px;\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<a href=\"" + targetURL + "\" title=\"" + e.Product_Name + "\" target=\"_blank\">");
                strHTML.Append("<img src=\"" + pub.FormatImgURL(e.Product_Img, "thumbnail") + "\" /></a>");
                strHTML.Append("<p><a href=\"" + targetURL + "\" target=\"_blank\">" + tools.CutStr(e.Product_Name, 20) + "</a></p>");
                //strHTML.Append("<p>单价:<span>" + pub.FormatCurrency(Get_Member_Price(e.Product_ID, e.Product_Price)) + "</span></p>");

                SupplierInfo sinfo = supplier_class.GetSupplierByID(e.Product_SupplierID);

                if (sinfo != null)
                {
                    strHTML.Append("<p> " + sinfo.Supplier_CompanyName + " </p>");
                }
                else
                {
                    strHTML.Append("<p>[易耐产业金服]</p>");
                }

                strHTML.Append("</li>");

            }
        }
        products = null;

        return strHTML.ToString();
    }

    /// <summary>
    /// 首页精品推荐
    /// </summary>
    /// <param name="Tag"></param>
    /// <returns></returns>
    public string Home_Boutique_Product(string Tag)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ProductInfo> products = GetCateTagProduct(6, 0, Tag);
        if (products != null)
        {
            string targetURL;
            //int ICount = 1;
            //string count = "";
            foreach (ProductInfo e in products)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, e.Product_ID.ToString());

                strHTML.Append("<dl>");
                strHTML.Append("<dt><a href=\"" + targetURL + "\" target=\"_blank\">");
                strHTML.Append("<img src=\"" + pub.FormatImgURL(e.Product_Img, "thumbnail") + "\" /></a></dt>");
                strHTML.Append("<dd>");
                strHTML.Append("<b><a href=\"" + targetURL + "\" target=\"_blank\">" + tools.CutStr(e.Product_Name, 20) + "</a></b>");
                strHTML.Append("<p>");
                strHTML.Append("规格型号：" + e.Product_Spec + "</p>");
                strHTML.Append("<p>");
                strHTML.Append("交货期：" + e.U_Product_DeliveryCycle + "</p>");
                strHTML.Append("<p>");
                strHTML.Append("价格：<strong>" + pub.FormatCurrency(Get_Member_Price(e.Product_ID, e.Product_Price)) + "</strong></p>");
                strHTML.Append("<p>");
                strHTML.Append("<a href=\"" + targetURL + "\" target=\"_blank\">详 情</a></p>");
                strHTML.Append("</dd>");
                strHTML.Append("<div class=\"clear\">");
                strHTML.Append("</div>");
                strHTML.Append("</dl>");
                //strHTML.Append("</div>");



                //ICount++;
            }
        }
        products = null;

        return strHTML.ToString();
    }

    /// <summary>
    /// 热销推荐
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <param name="Tag"></param>
    /// <returns></returns>
    public string ListPages_Hot_Recommend(int Cate_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ProductInfo> products = GetCateTagProduct(2, Cate_ID, "热销推荐");
        if (products != null)
        {
            string targetURL;
            foreach (ProductInfo e in products)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, e.Product_ID.ToString());

                strHTML.Append("<dl class=\"dst12\">");
                strHTML.Append("	<dt><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(e.Product_Img, "thumbnail") + "\"></a></dt>");
                strHTML.Append("	<dd><b><a href=\"" + targetURL + "\" target=\"_blank\">" + e.Product_Name + "</a></b>");
                strHTML.Append("<p>特价:<strong>" + pub.FormatCurrency(Get_Member_Price(e.Product_ID, e.Product_Price)) + "</strong></p>");
                strHTML.Append("<p><a href=\"" + targetURL + "\" target=\"_blank\">立即抢购</a></p>");
                strHTML.Append("	</dd>");
                strHTML.Append("	<div class=\"clear\"></div>");
                strHTML.Append("</dl>");
            }
        }
        products = null;

        return strHTML.ToString();
    }

    /// <summary>
    /// 首页限时抢购
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <returns></returns>
    public string Home_Recommend_LimitProduct(int Show_Num)
    {
        string product_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        int product_count = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Starttime}, GETDATE())", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(s,{PromotionLimitInfo.Promotion_Limit_Endtime}, GETDATE())", "<=", "0"));
        Query.OrderInfos.Add(new OrderInfo("PromotionLimitInfo.Promotion_Limit_Sort", "Asc"));
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits(Query, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (entitys != null)
        {
            TimeSpan span1;
            int timespan;

            string targetURL;

            foreach (PromotionLimitInfo entity in entitys)
            {
                ProductInfo productinfo = GetProductByID(entity.Promotion_Limit_ProductID);
                if (productinfo != null)
                {
                    if (productinfo.Product_IsInsale == 1 && productinfo.Product_IsAudit == 1)
                    {
                        targetURL = pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString());
                        product_count++;

                        if (product_count == 1)
                        {
                            span1 = entity.Promotion_Limit_Endtime - DateTime.Now;
                            timespan = (span1.Days * (24 * 3600)) + (span1.Hours * 3600) + (span1.Minutes * 60) + (span1.Seconds);

                            product_list += "<script>homecountdown(" + timespan + ", 'limit')</script>";
                        }

                        product_list += "<li><a href=\"" + targetURL + "\" title=\"" + productinfo.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(productinfo.Product_Img, "thumbnail") + "\" /></a><p><a href=\"" + targetURL + "\" target=\"_blank\">" + productinfo.Product_Name + "</a></p><strong>" + pub.FormatCurrency(Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "元 </strong></li>";

                    }
                }

            }
        }
        return product_list;
    }

    //首页促销专区
    public string Home_Promotion_List()
    {
        string Product_Arry = "0";
        PromotionInfo entity = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.OrderInfos.Add(new OrderInfo("PromotionInfo.Promotion_ID", "DESC"));
        IList<PromotionInfo> entitys = MyPromotion.GetPromotions(Query, pub.CreateUserPrivilege("0b16441f-dc42-4fd0-b8f1-0f8a80146771"));
        if (entitys != null)
        {
            entity = entitys[0];
            if (entity == null)
            {
                entity = new PromotionInfo();
            }
        }
        IList<PromotionProductInfo> PEntitys = entity.PromotionProducts;
        if (PEntitys != null)
        {
            foreach (PromotionProductInfo PEntity in PEntitys)
            {
                Product_Arry += "," + PEntity.Promotion_Product_Product_ID;
            }
        }
        Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Product_Arry));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "ASC"));

        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        string brand_list = "";
        int i = 0;
        brand_list = brand_list + "<div class=\"grey_border\" style=\" margin-top:7px; height:227px;\">";
        brand_list = brand_list + "<div class=\"left_sortlist\"><ul>";

        brand_list = brand_list + "<li class=\"no\" style=\" width:177px; text-align:left; padding-left:18px;\">促销专区</li>";
        brand_list += "</ul>";
        brand_list += "</div>";
        brand_list += "<div id=\"Div1\">";
        if (Products != null)
        {
            brand_list = brand_list + "<table cellpadding=\"5\" cellspacing=\"0\" align=\"center\" border=\"0\" width=\"140\">";

            brand_list = brand_list + "<tr>";

            i = i + 1;
            brand_list = brand_list + "	   <td>";

            brand_list = brand_list + "<DIV style=\"height:185px; OVERFLOW: hidden;border:0px;\" class=\"div_hotsale\">";
            brand_list = brand_list + "            <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            brand_list = brand_list + "<tr><td>";


            brand_list = brand_list + "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            brand_list = brand_list + "<tr><td class=\"detail_package\">";
            brand_list = brand_list + "<div style=\" overflow:hidden;\" id=\"promotion_list\">";
            brand_list = brand_list + "<ul style=\"padding-left:36px;\">";
            foreach (ProductInfo PEntity in Products)
            {
                brand_list = brand_list + "<li style=\"padding-left:0px;padding-top:5px;padding-bottom:3px;float:none;\">";
                brand_list = brand_list + "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, PEntity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(PEntity.Product_Img, "thumbnail") + "\" border=\"0\" onload=\"javascript:AutosizeImage(this,111,111);\" width=\"111\" height=\"111\" alt=\"" + PEntity.Product_Name + "\" /></a>";
                brand_list = brand_list + "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, PEntity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + PEntity.Product_Name + "\">" + tools.CutStr(PEntity.Product_Name, 16) + "</a><br />";
                brand_list = brand_list + "<span class=\"list_mktprice\">零售价：" + pub.FormatCurrency(PEntity.Product_MKTPrice) + "</span><br />";
                brand_list = brand_list + "促销价：<span class=\"list_price\">" + pub.FormatCurrency(Get_Member_Price(PEntity.Product_ID, PEntity.Product_Price)) + "</span>";
                brand_list = brand_list + "</li>";
            }
            brand_list = brand_list + "</ul>";
            brand_list += "</div>";
            brand_list = brand_list + "</td></tr>";
            brand_list = brand_list + "</table>";


            brand_list = brand_list + "</td></tr>";
            brand_list = brand_list + "</table>";
            brand_list = brand_list + "</div>";
            brand_list = brand_list + "<script type=\"text/javascript\">";
            brand_list = brand_list + "var marquee=new Marquee(\"promotion_list\");";
            brand_list = brand_list + "marquee.Direction=\"up\";";
            brand_list = brand_list + "marquee.Step=1;";
            brand_list = brand_list + "marquee.Width=190;";
            brand_list = brand_list + "marquee.Height=222;";
            brand_list = brand_list + "marquee.Timer=20;";
            brand_list = brand_list + "marquee.WaitTime=1;";
            brand_list = brand_list + "marquee.ScrollStep=190;";
            brand_list = brand_list + "marquee.Start();";
            brand_list = brand_list + "</script>";
            //brand_list = brand_list + "</div>";
            brand_list = brand_list + "<div class=\"clear\"></div>";



            brand_list = brand_list + "	   </td>";

            brand_list = brand_list + "</tr></table>";
        }
        brand_list += "</div>";
        brand_list += "</div>";
        return brand_list;
    }

    /// <summary>
    /// 首页团购专区
    /// </summary>
    /// <returns></returns>
    public string Home_GroupShopping()
    {
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();

        DataTable DTSource = DBHelper.Query("SELECT Product_ID, Product_Name, Product_MKTPrice, Product_Price, Product_Note, Product_Img FROM Product_Basic WHERE Product_ID IN(SELECT TOP 1 Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID = (SELECT TOP 1 Product_Tag_ID FROM Product_Tag WHERE Product_Tag_Name = '团购专区') ORDER BY Product_RelateTag_ID ASC)");

        StringBuilder strHTML = new StringBuilder();
        if (DTSource.Rows.Count > 0)
        {
            string targetURL;
            targetURL = pageurl.FormatURL(pageurl.product_detail, tools.NullStr(DTSource.Rows[0]["Product_ID"]));

            strHTML.Append("<dl class=\"zkw_dst5\">");
            strHTML.Append("	<dt><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(tools.NullStr(DTSource.Rows[0]["Product_Img"]), "thumbnail") + "\" /></a></dt>");
            strHTML.Append("	<dd><b><a href=\"" + targetURL + "\" title=\"" + tools.NullStr(DTSource.Rows[0]["Product_Name"]) + "\" target=\"_blank\">" + tools.NullStr(DTSource.Rows[0]["Product_Name"]) + "</a></b>");
            strHTML.Append("			<p> 原价:<strong>" + pub.FormatCurrency(tools.NullDbl(DTSource.Rows[0]["Product_MKTPrice"])) + "</strong></p>");
            strHTML.Append("	</dd>");
            strHTML.Append("	<div class=\"clear\"></div>");
            strHTML.Append("</dl>");
            strHTML.Append("<span>");
            strHTML.Append("<p>" + tools.NullStr(DTSource.Rows[0]["Product_Note"]) + "</p>");
            strHTML.Append("</span><span><a href=\"" + targetURL + "\" target=\"_blank\">团购价:<strong>" + pub.FormatCurrency(Get_Member_Price(tools.NullInt(DTSource.Rows[0]["Product_ID"]), tools.NullDbl(DTSource.Rows[0]["Product_Price"]))) + "</strong></a></span>");
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 全部类别
    /// </summary>
    /// <returns></returns>
    public string AllCategory()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
        IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);

        List<CategoryInfo> FirstList = entityList.Where(P => P.Cate_ParentID == 0).ToList();
        List<CategoryInfo> SecondList = null;
        List<CategoryInfo> ThridList = null;

        //StringBuilder strHTML = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        if (FirstList != null)
        {
            int f1 = 0;
            foreach (CategoryInfo First in FirstList)
            {
                f1++;
                sb.Append("<div class=\"blk02_info\">");
                sb.Append("<h2 " + (f1 == 1 ? " class=\"on\"" : "") + " >" + First.Cate_Name + "</h2>");//<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(First.Cate_ID.ToString(), "", "", "", "")) + "\">
                sb.Append("<div class=\"blk02_info_main\" " + (f1 == 1 ? "" : "style=\"display:none;\"") + " >");


                //strHTML.Append("<div class=\"allcategory\">");
                //strHTML.Append("	<h3><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(First.Cate_ID.ToString(), "", "", "", "")) + "\">" + First.Cate_Name + "</a></h3>");
                SecondList = entityList.Where(P => P.Cate_ParentID == First.Cate_ID).ToList();

                int f2 = 0;
                if (SecondList != null && SecondList.Count > 0)
                {

                    foreach (CategoryInfo Second in SecondList)
                    {
                        f2++;
                        sb.Append("<h3><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(Second.Cate_ID.ToString(), "", "", "", "")) + "\">" + Second.Cate_Name + "</a></h3>");



                        //strHTML.Append("	<dl>");
                        //strHTML.Append("		<dt><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(Second.Cate_ID.ToString(), "", "", "", "")) + "\">" + Second.Cate_Name + "</a></dt>");
                        //strHTML.Append("		<dd>");

                        ThridList = entityList.Where(P => P.Cate_ParentID == Second.Cate_ID).ToList();
                        if (ThridList != null && ThridList.Count > 0)
                        {
                            sb.Append("<ul " + (f2 == 1 ? "" : "style=\"display:none;\"") + " >");
                            foreach (CategoryInfo thrid in ThridList)
                            {
                                sb.Append("<li><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(thrid.Cate_ID.ToString(), "", "", "", "")) + "\">" + thrid.Cate_Name + "</a></li>");
                                // strHTML.Append("<em><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(thrid.Cate_ID.ToString(), "", "", "", "")) + "\">" + thrid.Cate_Name + "</a></em>");
                            }
                            sb.Append("</ul>");
                        }
                        else
                        {
                            //strHTML.Append("<em>&nbsp;</em>");
                        }
                        ThridList = null;

                        //strHTML.Append("</dd>");
                        //strHTML.Append("<div class=\"clear\"></div></dl>");



                    }
                }
                SecondList = null;

                //strHTML.Append("</div>");






                sb.Append("</div>");
                sb.Append("</div>");
            }

        }
        entityList = null;
        FirstList = null;

        return sb.ToString();
    }


    public QueryInfo GetHomeListQuery()
    {
        int type = tools.CheckInt(Request["type"]);
        int curpage = tools.CheckInt(Request["page"]);
        if (curpage <= 0)
        {
            curpage = 1;
        }

        string Extend_ProductArry = "";
        int Extend_ID;
        string Cate_Arry = "";
        int cate_id = tools.CheckInt(Request["cate_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        int brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));


        //按类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));


        return Query;
    }

    public void Home_NewProduct_List()
    {
        StringBuilder strHTML = new StringBuilder();
        int type = tools.CheckInt(Request["type"]);
        int currpage = tools.CheckInt(Request["page"]);
        if (currpage <= 0)
        {
            currpage = 1;
        }

        IList<ProductInfo> listProduct = null;
        PageInfo pageInfo = null;

        SupplierShopInfo shopInfo = null;
        int i = 0;
        string shopName = string.Empty, shopUrl = string.Empty;

        QueryInfo Query = GetHomeListQuery();

        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
        }

        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));


        if (listProduct != null)
        {
            foreach (ProductInfo entity in listProduct)
            {
                i++;

                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                if (shopInfo != null)
                {
                    shopName = shopInfo.Shop_Name;
                    shopUrl = shopInfo.Shop_Domain;
                }

                if (i % 4 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                strHTML.Append("<div class=\"p_box\">");

                strHTML.Append(Product_List_WholeSalePrice(entity));

                strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                strHTML.Append("<div class=\"p_box_info03\">");
                strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><a href=\"javascript:;\"><img src=\"/images/icon04.png\"></a></span>");
                strHTML.Append("<div class=\"p_box_info03_fox\">");
                strHTML.Append("<i></i>");

                strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</li>");
            }
        }
        else
        {
            Query = GetHomeListQuery();

            if (type == 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", "=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
            }

            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (listProduct != null)
            {
                foreach (ProductInfo entity in listProduct)
                {
                    i++;

                    shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                    if (shopInfo != null)
                    {
                        shopName = shopInfo.Shop_Name;
                        shopUrl = shopInfo.Shop_Domain;
                    }

                    if (i % 4 == 0)
                    {
                        strHTML.Append("<li class=\"mr0\">");
                    }
                    else
                    {
                        strHTML.Append("<li>");
                    }
                    strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                    strHTML.Append("<div class=\"p_box\">");

                    strHTML.Append(Product_List_WholeSalePrice(entity));

                    strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                    strHTML.Append("<div class=\"p_box_info03\">");
                    strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><a href=\"javascript:;\"><img src=\"/images/icon04.png\"></a></span>");
                    strHTML.Append("<div class=\"p_box_info03_fox\">");
                    strHTML.Append("<i></i>");

                    strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                    strHTML.Append("</div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</li>");
                }
            }
            else
            {
                Query = GetHomeListQuery();

                listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (listProduct != null)
                {
                    foreach (ProductInfo entity in listProduct)
                    {
                        i++;

                        shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                        if (shopInfo != null)
                        {
                            shopName = shopInfo.Shop_Name;
                            shopUrl = shopInfo.Shop_Domain;
                        }

                        if (i % 4 == 0)
                        {
                            strHTML.Append("<li class=\"mr0\">");
                        }
                        else
                        {
                            strHTML.Append("<li>");
                        }
                        strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                        strHTML.Append("<div class=\"p_box\">");

                        strHTML.Append(Product_List_WholeSalePrice(entity));

                        strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                        strHTML.Append("<div class=\"p_box_info03\">");
                        strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><a href=\"javascript:;\"><img src=\"/images/icon04.png\"></a></span>");
                        strHTML.Append("<div class=\"p_box_info03_fox\">");
                        strHTML.Append("<i></i>");

                        strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                        strHTML.Append("</div>");
                        strHTML.Append("</div>");
                        strHTML.Append("</div>");
                        strHTML.Append("</li>");
                    }
                }
            }
        }
        Response.Write(strHTML.ToString());
    }

    public int Home_NewProduct_ListPageCount()
    {
        int pageCount = 0;
        PageInfo pageInfo = null;
        int type = tools.CheckInt(Request["type"]);

        QueryInfo Query = GetHomeListQuery();
        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
        }

        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (pageInfo != null)
        {
            pageCount = pageInfo.PageCount;
        }
        else
        {
            Query = GetHomeListQuery();

            if (type == 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
            }

            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (pageInfo != null)
            {
                pageCount = pageInfo.PageCount;
            }
            else
            {
                Query = GetHomeListQuery();

                if (type == 1)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
                }

                pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (pageInfo != null)
                {
                    pageCount = pageInfo.PageCount;
                }
                else
                {
                    pageCount = 0;
                }
            }
        }
        return pageCount;
    }

    public int Home_NewProduct_Count(int cate_id, int type)
    {
        string Cate_Arry = "";

        IList<ProductInfo> listProduct = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }


        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (listProduct != null)
        {
            return listProduct.Count;
        }
        else
        {
            Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;

            if (cate_id > 0)
            {
                //获取子类
                Cate_Arry = Get_All_SubCate(cate_id);
            }

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

            if (cate_id > 0 && Cate_Arry.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }

            if (type == 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

            if (listProduct != null)
            {
                return listProduct.Count;
            }
            else
            {
                Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;

                if (cate_id > 0)
                {
                    //获取子类
                    Cate_Arry = Get_All_SubCate(cate_id);
                }

                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

                if (cate_id > 0 && Cate_Arry.Length > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
                }

                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
                listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (listProduct != null)
                {
                    return listProduct.Count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    //今日上架商品
    public int Home_TodayProduct_Count()
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;



        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ProductInfo.Product_Addtime}, GETDATE())", "=", "0"));

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
        PageInfo page = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }
    }

    //总商品
    public int Home_TotalProduct_Count()
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;



        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
        PageInfo page = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }
    }


    /// <summary>
    /// 首页商城列表
    /// </summary>
    /// <param name="CateID"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public string Home_Product(int CateID, string name)
    {
        StringBuilder strHTML = new StringBuilder();
        string qian = ""; string hou = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 7;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", CateID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsFrequently", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            strHTML.Append("<h3>");
            int i = 0;
            foreach (CategoryInfo entity in Categorys)
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append("<a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\" target=\"_blank\" id=\"a0" + i + "\" class=\"on\">" + entity.Cate_Name + "</a>");
                    qian = qian + "\"a0" + i + "\"";
                    hou = hou + "\"aa0" + i + "\"";
                }
                else
                {
                    strHTML.Append("<a href=\"/product/category.aspx?cate_id=" + entity.Cate_ID + "\" target=\"_blank\" id=\"a0" + i + "\" >" + entity.Cate_Name + "</a>");
                    qian = qian + ", \"a0" + i + "\"";
                    hou = hou + ", \"aa0" + i + "\"";
                }


            }

            strHTML.Append("<a href=\"/product/category.aspx\" target=\"_blank\">更多 ></a></h3>");
            strHTML.Append("<script type=\"text/javascript\">");
            strHTML.Append(" window.onload = function () { ");
            strHTML.Append(" var SDmodel = new scrollDoor(); ");
            strHTML.Append("SDmodel.sd([" + qian + "], [" + hou + "], \"on\", \" \");");
            strHTML.Append(" } ");
            strHTML.Append("</script>");

            strHTML.Append("<div class=\"ymg_b02_box\">");
            i = 0;
            foreach (CategoryInfo entity in Categorys)
            {
                i++;
                strHTML.Append("<div class=\"ymg_b02_main\" id=\"aa0" + i + "\">");
                strHTML.Append("<div id=\"LeftButton" + i + "\" class=\"button_left\"><a></a></div>");
                strHTML.Append("<div class=\"box\" >");
                strHTML.Append("<div id=\"MarqueeDiv" + i + "\" style=\" overflow:hidden;\">");
                strHTML.Append("<div id=\"MarqueeDiv3Boxent" + i + "\" style=\" overflow:hidden;\">");
                // strHTML.Append(Home_Product_List(6, entity.Cate_ID, name));
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("<div id=\"RightButton" + i + "\" class=\"button_right\"><a></a></div>");
                strHTML.Append("<script>srcoll_left_right_Control(true,2,\"LeftButton" + i + "\",\"RightButton" + i + "\",\"MarqueeDiv" + i + "\",\"MarqueeDiv3Boxent" + i + "\",992,404,248,20);</script>");
                strHTML.Append("</div>");
            }

            strHTML.Append("</div>");
        }


        return strHTML.ToString();
    }


    public string Home_1Floor(int ShowNum, string TagName)
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("    <h2><a href=\"/product/category.aspx\" class=\"more\">更多 ></a></a><strong><i>1F</i>商城中心</strong><b>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        int i = 0;
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        int CategoryNum = Categorys.Count;
        if (Categorys != null)
        {
            foreach (CategoryInfo Category in Categorys)
            {
                i++;
                strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + Category.Cate_ID + "\" id=\"a0" + i + "\"  >" + Category.Cate_Name + "</a>");

            }

            strHTML.Append("     </b></h2>");


            strHTML.Append("         <div class=\"f_main\">");
            strHTML.Append("             <div class=\"f_info01\" >");
            strHTML.Append("                 <img src=\"/images/ad_pic03.jpg\" style=\"height:513px;width:213px;\"></a><div class=\"f_info01_box\"><b>热门推荐</b><p>");
            strHTML.Append(adclass.AD_Show("Home_Floor1_Tag", "", "keyword", 0));
            strHTML.Append("              </p>");
            strHTML.Append("                 </div>");
            strHTML.Append("             </div>");



            int ij = 0;
            QueryInfo Query1;
            int jj = 0;
            foreach (CategoryInfo category in Categorys)
            {
                ij++;
                jj++;
                var css = "";
                if (ij >= 2)
                {
                    css = "style='display:none;width:704px!important;'";
                }
                else
                {
                    css = " style='width:704px!important;'";
                }

                strHTML.Append("             <div  class=\"f_info02_2\" " + css + "  id=\"aa0" + ij + "\">");
                strHTML.Append("                 <ul>");
                string GetAllCateProduct = Get_All_SubCate(category.Cate_ID);
                string product_cate = "";
                string productsArray1 = "0";
                IList<ProductInfo> products1 = GetCateTagProduct_TagID(100, 0, 5);
                if (products1 != null)
                {
                    foreach (ProductInfo products11 in products1)
                    {
                        product_cate = Myproduct.GetProductCategory(products11.Product_ID);

                        if (product_cate.Length > 0)
                        {
                            string[] sArrays = product_cate.Split(new char[] { ',' });
                            foreach (string sArray in sArrays)
                            {
                                if (GetAllCateProduct.Contains(sArray))
                                {
                                    productsArray1 += "," + products11.Product_ID;
                                }
                            }
                        }
                    }

                }


                Query1 = null;
                Query1 = new QueryInfo();
                Query1.PageSize = 0;
                Query1.CurrentPage = 1;
                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "" + productsArray1 + " "));

                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Desc"));
                Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                IList<ProductInfo> products = Myproduct.GetProducts(Query1, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));



                if (products != null)
                {
                    int ii = 0;
                    foreach (ProductInfo product in products)
                    {
                        ii++;
                        if (ii < 7)
                        {
                            strHTML.Append("<li>");
                            strHTML.Append("                         <div class=\"logo_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + " \">");
                            strHTML.Append("                          <img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" width=\"180px;\" height=\"180px; padding-left: 0.8px;\" /></a></div>");
                            strHTML.Append("                         <p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\"   title=" + product.Product_Name + "  style=\"color: #3083e1\">" + tools.CutStr(product.Product_Name, 24) + " </a></p>");
                            strHTML.Append("                         <strong ><span style=\"color:#ff6600\">单价:" + pub.FormatCurrency(Get_Member_Price(product.Product_ID, product.Product_Price)) + "</span></strong>");
                            strHTML.Append("                     </li>");
                        }
                    }
                }
                strHTML.Append("                 </ul>");
                strHTML.Append("                 <div class=\"clear\"></div>");
                strHTML.Append("             </div>");
            }
        }
        strHTML.Append("         </div>");
        return strHTML.ToString();
    }



    //获取指定分类指定标签商品信息(通过标签号)  /**/
    public IList<ProductInfo> GetCateTagProduct_TagID(int Show_Num, int cate_id, int tag_id)
    {
        //获取所有子类编号
        string sub_cate = Get_All_SubCate(cate_id);
        if (sub_cate == "")
        {
            return null;
        }
        else
        {
            //获取推荐标签信息
            ProductTagInfo Taginfo = MyTag.GetProductTagByID(tag_id, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
            if (Taginfo != null)
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = Show_Num;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID = " + Taginfo.Product_Tag_ID + ""));


                if (sub_cate.Length > 0 && cate_id > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", sub_cate));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + sub_cate + ")"));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
                IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

                return Products;

            }
            else
            {
                return null;
            }

        }
    }

    ////获取所有子分类
    //public string Get_All_SubCate(int Cate_id)
    //{
    //    string Cate_Arry = Cate_id.ToString();
    //    if (Cate_id != 0)
    //    {
    //        QueryInfo Query = new QueryInfo();
    //        Query.PageSize = 0;
    //        Query.CurrentPage = 1;
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
    //        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
    //        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "Desc"));
    //        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
    //        Cate_Arry = Get_SubCate(Cate_id, Categorys);
    //    }
    //    return Cate_Arry;
    //}

    ////获取子分类
    //public string Get_SubCate(int Cate_id, IList<CategoryInfo> Categorys)
    //{
    //    string Cate_Arry = Cate_id.ToString();
    //    if (Categorys != null)
    //    {
    //        foreach (CategoryInfo entity in Categorys)
    //        {
    //            if (entity.Cate_ParentID == Cate_id)
    //            {
    //                Cate_Arry = Cate_Arry + "," + Get_SubCate(entity.Cate_ID, Categorys);
    //            }
    //        }

    //    }

    //    return Cate_Arry;
    //}

    #endregion

    #region  交易平台首页

    public QueryInfo TradeIndex_Product_ListQuery()
    {
        int type = tools.CheckInt(Request["type"]);
        int curpage = tools.CheckInt(Request["page"]);
        if (curpage <= 0)
        {
            curpage = 1;
        }

        string Extend_ProductArry = "";
        int Extend_ID;
        string Cate_Arry = "";
        int cate_id = tools.CheckInt(Request["cate_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        string collection_array = "";
        int collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id > 0)
        {
            collection_array = Get_All_SubCate(collection_id);
        }

        string cateprice_array = "";
        int cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id > 0)
        {
            cateprice_array = Get_All_SubCate(cateprice_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = curpage;
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        int brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));


        //按分类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        //按款式查询
        if (collection_id > 0 && collection_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
        }

        //按价格查询
        if (cateprice_id > 0 && cateprice_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

        return Query;
    }

    public void TradeIndex_Product_Filter(int cate_id, int cate_parentid, int cate_typeid)
    {
        int catefatherid = cate_id;
        string Cate_Arry = Get_First_SubCate(cate_id);
        string collection_array = "54", cateprice_array = "55";
        string keyword, isgallerylist, orderby, Extend_Value;
        double price_min, price_max;
        int isgroup, ispromotion, isRecommend, isgift, Brand_ID, tag_id, target, type, collection_id, cateprice_id;
        int icount = 1;

        IList<ProductTypeExtendInfo> extends = null;

        Brand_ID = tools.CheckInt(Request["brand_id"]);
        tag_id = tools.CheckInt(Request["tag_id"]);
        target = tools.CheckInt(Request["target"]);
        type = tools.CheckInt(Request["type"]);

        collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id == 0)
        {
            collection_id = 54;
        }

        cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id == 0)
        {
            cateprice_id = 55;
        }

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"screen\">");
        //strHTML.Append("<h2 style=\"height: 40px;background-color: #f5f5f5;font-size: 16px;font-weight: bold;color: #333; line-height: 40px;padding: 0 10px;border-bottom: 2px solid #f60001;\"><a style=\"font-size: 12px;font-weight: normal;color: #666;float: right;display: inline;\" href=\"/tradeindex/iframe_product.aspx?cate_id=" + cate_id + "&type=" + type + "&target=" + target + "\">重置筛选条件</a></h2>");
        strHTML.Append("<dl>");
        strHTML.Append("<dt>分类：</dt>");
        strHTML.Append(TradeIndex_Product_Cate_list(cate_id, Cate_Arry, cate_parentid, target));
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        bool extend_show = true;
        HttpCookie cookie = Request.Cookies["extend_div"];
        if (cookie == null || cookie.Value.Equals("hide"))
        {
            extend_show = false;
        }

        if (cate_id > 0 && cate_typeid > 0)
        {
            ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (producttype != null)
            {
                IList<BrandInfo> brands = producttype.BrandInfos;
                if (brands != null)
                {
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt>品牌：</dt>");
                    strHTML.Append(TradeIndex_Product_Brand_list(brands, cate_id, Brand_ID, target));
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                }
                extends = producttype.ProductTypeExtendInfos;
                if (extends != null)
                {
                    foreach (ProductTypeExtendInfo entity in extends)
                    {
                        if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                        {
                            strHTML.Append(TradeIndex_Product_Extend_list(entity, target));

                            if (icount == 3)
                            {
                                strHTML.Append("<span " + (extend_show ? "" : "style=\"display:none\"") + " id=\"hidden_div\">");
                            }

                            icount++;
                        }
                    }
                }
            }
            if (icount > 3)
            {
                strHTML.Append("</span>");
            }
        }
        else
        {
            strHTML.Append("<dl>");
            strHTML.Append("<dt>品牌：</dt>");
            strHTML.Append(TradeIndex_Product_Brand_list(null, cate_id, Brand_ID, target));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>款式：</dt>");
            strHTML.Append(TradeIndex_Product_Cates_list("collection_id", collection_id, collection_array, 54, target));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>价格：</dt>");
            strHTML.Append(TradeIndex_Product_Cates_list("cateprice_id", cateprice_id, cateprice_array, 55, target));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");


        }
        strHTML.Append("</div>");

        if (cate_id > 0 && cate_typeid > 0)
        {
            if (icount > 3)
            {
                strHTML.Append("<div class=\"blk005\">");
                if (extend_show)
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:hidden_showdiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02_2.png\" /></span></a>");
                }
                else
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:show_hiddendiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02.png\" /></span></a>");
                }
                strHTML.Append("</div>");
            }
        }

        Response.Write(strHTML.ToString());

        keyword = tools.CheckStr(Request["keyword"]);
        isgallerylist = tools.CheckStr(Request["isgallerylist"]);
        orderby = tools.CheckStr(Request["orderby"]);
        price_min = tools.CheckFloat(Request["price_min"]);
        price_max = tools.CheckFloat(Request["price_max"]);
        isgroup = tools.CheckInt(Request["isgroup"]);
        ispromotion = tools.CheckInt(Request["ispromotion"]);
        isRecommend = tools.CheckInt(Request["isRecommend"]);
        isgift = tools.CheckInt(Request["isgift"]);
        int instock = tools.CheckInt(Request["instock"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();
        isgallerylist = isgallerylist.Replace("\"", "&quot;").ToString();
        orderby = orderby.Replace("\"", "&quot;").ToString();

        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter" + target + "\" method=\"string\" action=\"\">");
        Response.Write("<input type=\"hidden\" name=\"tag_id\" id=\"tag_id" + target + "\" value=\"" + tag_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id" + target + "\" value=\"" + cate_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id" + target + "\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"keyword\" id=\"keyword" + target + "\" value=\"" + keyword + "\">");
        Response.Write("<input type=\"hidden\" name=\"isgallerylist\" id=\"isgallerylist" + target + "\" value=\"" + isgallerylist + "\">");
        Response.Write("<input type=\"hidden\" name=\"orderby\" id=\"orderby" + target + "\" value=\"" + orderby + "\">");
        Response.Write("<input type=\"hidden\" name=\"instock\" id=\"instock" + target + "\" value=\"" + instock + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_min\" id=\"price_min" + target + "\" value=\"" + price_min + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_max\" id=\"price_max" + target + "\" value=\"" + price_max + "\">");
        Response.Write("<input type=\"hidden\" name=\"target\" id=\"target" + target + "\" value=\"" + target + "\">");
        Response.Write("<input type=\"hidden\" name=\"type\" id=\"type" + target + "\" value=\"" + type + "\">");
        Response.Write("<input type=\"hidden\" name=\"collection_id\" id=\"collection_id" + target + "\" value=\"" + collection_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cateprice_id\" id=\"cateprice_id" + target + "\" value=\"" + cateprice_id + "\">");

        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                {
                    Extend_Value = tools.NullStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                    Response.Write("<input type=\"hidden\" name=\"filter_" + entity.ProductType_Extend_ID + "\" id=\"filter_" + entity.ProductType_Extend_ID + target + "\" value=\"" + Extend_Value + "\">");
                }
            }
        }
        Response.Write("</form>");
        Response.Write("</div>");
    }

    public string TradeIndex_Product_Cate_list(int cate_id, string Cate_Arry, int cate_parentid, int target)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["cate_id"]);
        if (Cate_Arry == cate_id.ToString())
        {
            strHTML.Append("<a href=\"javascript:set_form_filter('cate_id'," + cate_parentid + "," + target + ");\" ");
        }
        else
        {
            strHTML.Append("<a href=\"javascript:set_form_filter('cate_id'," + cate_id + "," + target + ");\"");
        }

        if (Cate_Arry == cate_id.ToString())
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }

        strHTML.Append(">全部</a>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Cate_Arry == cate_id.ToString())
        {
            Cate_Arry = Get_First_SubCate(cate_parentid);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_parentid.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Cate_Arry + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:set_form_filter('cate_id'," + entity.Cate_ID + "," + target + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }

            //if (cate_parentid > 0 && cate_parentid != 58 && cate_parentid != 59 && cate_parentid != 61 && cate_parentid != 62)
            //{
            //    strHTML.Append("<span><a href=\"javascript:set_form_filter('cate_id'," + cate_parentid + "," + target + ");\">上一级</a></span>");
            //}
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }

    public string TradeIndex_Product_Brand_list(IList<BrandInfo> brands, int Cate_ID, int Brand_ID, int target)
    {
        StringBuilder strHTML = new StringBuilder("<dd>");

        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('brand_id',''," + target + ");\"");
        if (Brand_ID > 0)
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (brands != null)
        {
            foreach (BrandInfo entity in brands)
            {
                if (entity.Brand_IsActive == 1)
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('brand_id'," + entity.Brand_ID + "," + target + ");\" ");
                    if (Brand_ID == entity.Brand_ID)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + entity.Brand_Name + "</a>");
                }
            }
        }
        else
        {
            QueryInfo Query = new QueryInfo();
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "desc"));

            IList<BrandInfo> listBrand = MyBrand.GetBrands(Query, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));

            if (listBrand != null)
            {
                foreach (BrandInfo entity in listBrand)
                {
                    if (entity.Brand_IsActive == 1)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('brand_id'," + entity.Brand_ID + "," + target + ");\" ");
                        if (Brand_ID == entity.Brand_ID)
                        {
                            strHTML.Append(" class=\"on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + entity.Brand_Name + "</a>");
                    }
                }
            }
        }
        strHTML.Append("</dd>");
        return strHTML.ToString();
    }

    public string TradeIndex_Product_Extend_list(ProductTypeExtendInfo extend, int target)
    {
        string Extend_Value = tools.NullStr(Request["filter_" + extend.ProductType_Extend_ID]).Trim();
        string default_value = extend.ProductType_Extend_Default;

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dl>");
        strHTML.Append("<dt>" + extend.ProductType_Extend_Name + "：</dt><dd>");
        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('filter_" + extend.ProductType_Extend_ID + "',''," + target + ");\"");

        if (Extend_Value != "")
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (default_value.Length > 0)
        {
            foreach (string extend_value in default_value.Split('|'))
            {
                if (extend_value != "")
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('filter_" + extend.ProductType_Extend_ID + "','" + extend_value + "'," + target + ");\"");
                    if (Extend_Value == extend_value)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + extend_value + "</a></span>");
                }
            }
        }
        else
        {
            string Exit_Extend = "||";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ExtendID", "=", extend.ProductType_Extend_ID.ToString()));
            IList<ProductExtendInfo> entitys = Myproduct.GetProductExtends(Query);
            if (entitys != null)
            {
                foreach (ProductExtendInfo Pre_Extend in entitys)
                {
                    //检查属性重复性
                    if (Exit_Extend.IndexOf("|" + Pre_Extend.Extend_Value + "|") < 0)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_filter('filter_" + extend.ProductType_Extend_ID + "','" + Pre_Extend.Extend_Value + "'," + target + ");\"");
                        if (Extend_Value == Pre_Extend.Extend_Value)
                        {
                            strHTML.Append(" class=\"on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + Pre_Extend.Extend_Value + "</a> ");

                        Exit_Extend += Pre_Extend.Extend_Value + "|";
                    }
                }
            }
        }
        strHTML.Append("</dd>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        return strHTML.ToString();
    }

    public string TradeIndex_Product_Cates_list(string cate_name, int cate_id, string Cate_Arry, int cate_parentid, int target)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["" + cate_name + ""]);
        //if (Cate_Arry == cate_id.ToString())
        //{
        //    strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + cate_parentid + "," + target + ");\" ");
        //}
        //else
        //{
        //    strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + cate_id + "," + target + ");\"");
        //}

        //if (Cate_Arry == cate_id.ToString())
        //{

        //}
        //else
        //{
        //    strHTML.Append(" class=\"on\"");
        //}

        //strHTML.Append(">全部</a>");

        strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + cate_parentid + "," + target + ");\"");
        if (cate_id == 54 || cate_id == 55)
        {
            strHTML.Append(" class=\"on\"");
        }
        else
        {

        }
        strHTML.Append(">全部</a>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + cate_parentid + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + entity.Cate_ID + "," + target + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }

    public void TradeIndex_Product_List()
    {
        StringBuilder strHTML = new StringBuilder();
        int type = tools.CheckInt(Request["type"]);
        int currpage = tools.CheckInt(Request["page"]);
        if (currpage <= 0)
        {
            currpage = 1;
        }

        IList<ProductInfo> listProduct = null;
        PageInfo pageInfo = null;

        SupplierShopInfo shopInfo = null;
        int i = 0;
        string shopName = string.Empty, shopUrl = string.Empty;

        QueryInfo Query = TradeIndex_Product_ListQuery();

        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
        }

        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));


        if (listProduct != null)
        {
            foreach (ProductInfo entity in listProduct)
            {
                i++;

                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                if (shopInfo != null)
                {
                    shopName = shopInfo.Shop_Name;
                    shopUrl = shopInfo.Shop_Domain;
                }

                if (i % 4 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                strHTML.Append("<div class=\"p_box\">");

                strHTML.Append(Product_List_WholeSalePrice(entity));

                strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                strHTML.Append("<div class=\"p_box_info03\">");
                strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><span style=\"float:right;cursor:pointer;\"  onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img style=\"display:inline-block; vertical-align:middle; margin-left:10px;width:74px;\" src=\"/images/icon04.png\" style=\"width:74px;\"></span></span>");
                strHTML.Append("<div class=\"p_box_info03_fox\">");
                strHTML.Append("<i></i>");

                strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</li>");
            }
        }
        else
        {
            Query = TradeIndex_Product_ListQuery();

            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (listProduct != null)
            {
                foreach (ProductInfo entity in listProduct)
                {
                    i++;

                    shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                    if (shopInfo != null)
                    {
                        shopName = shopInfo.Shop_Name;
                        shopUrl = shopInfo.Shop_Domain;
                    }

                    if (i % 4 == 0)
                    {
                        strHTML.Append("<li class=\"mr0\">");
                    }
                    else
                    {
                        strHTML.Append("<li>");
                    }
                    strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                    strHTML.Append("<div class=\"p_box\">");

                    strHTML.Append(Product_List_WholeSalePrice(entity));

                    strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                    strHTML.Append("<div class=\"p_box_info03\">");
                    strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><span  onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img style=\"display:inline-block; vertical-align:middle; margin-left:10px;width:74px;\" src=\"/images/icon04.png\" style=\"width:74px;\"></span></span>");
                    strHTML.Append("<div class=\"p_box_info03_fox\">");
                    strHTML.Append("<i></i>");

                    strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                    strHTML.Append("</div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</li>");
                }
            }
        }
        Response.Write(strHTML.ToString());
    }

    public int TradeIndex_Product_ListPageCount()
    {
        int pageCount = 0;
        PageInfo pageInfo = null;
        int type = tools.CheckInt(Request["type"]);

        QueryInfo Query = TradeIndex_Product_ListQuery();

        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
        }

        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (pageInfo.PageCount > 0)
        {
            pageCount = pageInfo.PageCount;
        }
        else
        {
            Query = TradeIndex_Product_ListQuery();
            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (pageInfo.PageCount > 0)
            {
                pageCount = pageInfo.PageCount;
            }
            else
            {
                pageCount = 0;
            }
        }
        return pageCount;
    }

    public int TradeIndex_Product_Count(int cate_id, int type)
    {
        string Cate_Arry = "", collection_array = "", cateprice_array = "";
        string Extend_ProductArry = "";
        int Extend_ID;
        int brand_id = tools.CheckInt(Request["brand_id"]);
        int cateprice_id = 0, collection_id = 0;

        IList<ProductInfo> listProduct = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }
        else
        {
            cate_id = 53;
        }


        collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id > 0)
        {
            collection_array = Get_All_SubCate(collection_id);
        }


        cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id > 0)
        {
            cateprice_array = Get_All_SubCate(cateprice_id);
        }
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        //按款式查询
        if (collection_id > 0 && collection_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
        }

        //按价格查询
        if (cateprice_id > 0 && cateprice_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
        }

        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }

        if (type == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (listProduct != null)
        {
            return listProduct.Count;
        }
        else
        {
            Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;

            if (cate_id > 0)
            {
                //获取子类
                Cate_Arry = Get_All_SubCate(cate_id);
            }
            else
            {
                cate_id = 53;
            }

            collection_id = tools.CheckInt(Request["collection_id"]);
            if (collection_id > 0)
            {
                collection_array = Get_All_SubCate(collection_id);
            }


            cateprice_id = tools.CheckInt(Request["cateprice_id"]);
            if (cateprice_id > 0)
            {
                cateprice_array = Get_All_SubCate(cateprice_id);
            }

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

            if (cate_id > 0 && Cate_Arry.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }

            //按类查询
            if (collection_id > 0 && collection_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
            }

            //按类查询
            if (cateprice_id > 0 && cateprice_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
            }

            if (brand_id > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
            }

            foreach (string querystring in Request.QueryString)
            {
                if (querystring != null)
                {
                    if (querystring.IndexOf("filter_") >= 0)
                    {
                        Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                        if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                        {
                            Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                        }
                    }
                }
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (listProduct != null)
            {
                return listProduct.Count;
            }
            else
            {
                return 0;
            }
        }
    }

    #region 其他珠宝

    public void TradeIndex_OtherProduct_Filter(string cate_array, int cate_id)
    {
        int Brand_ID, target, type;


        Brand_ID = tools.CheckInt(Request["brand_id"]);
        target = tools.CheckInt(Request["target"]);
        type = tools.CheckInt(Request["type"]);

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"screen\">");
        strHTML.Append("<dl>");
        strHTML.Append("<dt>分类：</dt>");
        strHTML.Append(TradeIndex_OtherProduct_Cate_list(cate_array, target));
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");


        strHTML.Append("<dl>");
        strHTML.Append("<dt>品牌：</dt>");
        strHTML.Append(TradeIndex_OtherProduct_Brand_list(target, Brand_ID));
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");


        strHTML.Append("</div>");



        Response.Write(strHTML.ToString());


        int instock = tools.CheckInt(Request["instock"]);



        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter" + target + "\" method=\"string\" action=\"\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id" + target + "\" value=\"" + cate_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id" + target + "\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"instock\" id=\"instock" + target + "\" value=\"" + instock + "\">");
        Response.Write("<input type=\"hidden\" name=\"target\" id=\"target" + target + "\" value=\"" + target + "\">");
        Response.Write("<input type=\"hidden\" name=\"type\" id=\"type" + target + "\" value=\"" + type + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_array\" id=\"cate_array" + target + "\" value=\"" + cate_array + "\">");
        Response.Write("</form>");
        Response.Write("</div>");
    }


    public int TradeIndex_OtherProduct_Count(string cate_ids)
    {
        int counts = 0;
        string Cate_Arry = "0";
        string Extend_ProductArry = "";
        int Extend_ID;
        int cate_id = tools.CheckInt(Request["cate_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }
        int brand_id = tools.CheckInt(Request["brand_id"]);

        IList<ProductInfo> listProduct = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

        //按类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }
        else
        {
            string[] cate_arr = cate_ids.Split(',');
            if (cate_arr != null)
            {
                foreach (string item in cate_arr)
                {
                    Cate_Arry += Get_All_SubCate(tools.CheckInt(item));
                }
            }

            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }


        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));
        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (listProduct != null)
        {
            return listProduct.Count;
        }
        else
        {
            return 0;
        }
    }

    public string TradeIndex_OtherProduct_Cate_list(string Cate_Arry, int target)
    {
        StringBuilder strHTML = new StringBuilder("<dd>");

        int id = tools.CheckInt(Request["cate_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Cate_Arry + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:set_form_other_filter('cate_id'," + entity.Cate_ID + "," + target + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }

    public string TradeIndex_OtherProduct_Brand_list(int target, int Brand_ID)
    {
        StringBuilder strHTML = new StringBuilder("<dd>");

        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "desc"));

        IList<BrandInfo> listBrand = MyBrand.GetBrands(Query, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));

        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_other_filter('brand_id',''," + target + ");\"");
        if (Brand_ID > 0)
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (listBrand != null)
        {
            foreach (BrandInfo entity in listBrand)
            {
                if (entity.Brand_IsActive == 1)
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_form_other_filter('brand_id'," + entity.Brand_ID + "," + target + ");\" ");
                    if (Brand_ID == entity.Brand_ID)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + entity.Brand_Name + "</a>");
                }
            }
            strHTML.Append("</dd>");
        }

        return strHTML.ToString();
    }

    public void TradeIndex_OtherProduct_List(string cate_ids)
    {
        StringBuilder strHTML = new StringBuilder();
        int type = tools.CheckInt(Request["type"]);
        int currpage = tools.CheckInt(Request["page"]);
        if (currpage <= 0)
        {
            currpage = 1;
        }

        IList<ProductInfo> listProduct = null;
        PageInfo pageInfo = null;

        SupplierShopInfo shopInfo = null;
        int i = 0;
        string shopName = string.Empty, shopUrl = string.Empty;

        int curpage = tools.CheckInt(Request["page"]);
        if (curpage <= 0)
        {
            curpage = 1;
        }

        string Extend_ProductArry = "";
        int Extend_ID;
        string Cate_Arry = "";
        int cate_id = tools.CheckInt(Request["cate_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = curpage;
        // //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        int brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));


        //按类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }
        else
        {
            string[] cate_arr = cate_ids.Split(',');
            if (cate_arr != null)
            {
                foreach (string item in cate_arr)
                {
                    Cate_Arry += Get_All_SubCate(tools.CheckInt(item));
                }
            }

            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));


        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));


        if (listProduct != null)
        {
            foreach (ProductInfo entity in listProduct)
            {
                i++;

                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                if (shopInfo != null)
                {
                    shopName = shopInfo.Shop_Name;
                    shopUrl = shopInfo.Shop_Domain;
                }

                if (i % 4 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                strHTML.Append("<div class=\"p_box\">");

                strHTML.Append(Product_List_WholeSalePrice(entity));

                strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                strHTML.Append("<div class=\"p_box_info03\">");
                strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><span style=\"float:right;cursor:pointer;\"  onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img style=\"display:inline-block; vertical-align:middle; margin-left:10px;width:74px;\" src=\"/images/icon04.png\" style=\"width:74px;\"></span></span>");
                strHTML.Append("<div class=\"p_box_info03_fox\">");
                strHTML.Append("<i></i>");

                strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</li>");
            }
        }
        Response.Write(strHTML.ToString());
    }

    public int TradeIndex_OtherProduct_ListPageCount(string cate_ids)
    {
        int pageCount = 0;
        PageInfo pageInfo = null;
        int type = tools.CheckInt(Request["type"]);
        int curpage = tools.CheckInt(Request["page"]);
        string Cate_Arry = "";

        if (curpage <= 0)
        {
            curpage = 1;
        }

        int cate_id = tools.CheckInt(Request["cate_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = curpage;
        //聚合是否列表显示 暂时屏蔽掉
        //  Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));


        int brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        //按类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }
        else
        {
            string[] cate_arr = cate_ids.Split(',');
            if (cate_arr != null)
            {
                foreach (string item in cate_arr)
                {
                    Cate_Arry += Get_All_SubCate(tools.CheckInt(item));
                }
            }

            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (pageInfo.PageCount > 0)
        {
            pageCount = pageInfo.PageCount;
        }
        else
        {
            Query = TradeIndex_Product_ListQuery();

            if (type == 1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
            }

            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (pageInfo.PageCount > 0)
            {
                pageCount = pageInfo.PageCount;
            }
            else
            {
                Query = TradeIndex_Product_ListQuery();

                //if (type == 1)
                //{
                //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
                //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
                //}

                pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (pageInfo.PageCount > 0)
                {
                    pageCount = pageInfo.PageCount;
                }
                else
                {
                    pageCount = 0;
                }
            }
        }
        return pageCount;
    }


    #endregion

    #region  今日爆款

    public QueryInfo TradeIndex_Hot_ProductQuery(string tag, int Show_Num)
    {
        QueryInfo Query = new QueryInfo();

        int curpage = 0;

        if (Show_Num == 0)
        {
            curpage = 1;
        }
        else
        {
            curpage = tools.CheckInt(Request["page"]);
            if (curpage <= 0)
            {
                curpage = 1;
            }
        }

        string Extend_ProductArry = "";
        int Extend_ID;
        string Cate_Arry = "";
        int cate_id = tools.CheckInt(Request["cate_hot_id"]);
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        string collection_array = "";
        int collection_id = tools.CheckInt(Request["collection_hot_id"]);
        if (collection_id > 0)
        {
            collection_array = Get_All_SubCate(collection_id);
        }

        string cateprice_array = "";
        int cateprice_id = tools.CheckInt(Request["cateprice_hot_id"]);
        if (cateprice_id > 0)
        {
            cateprice_array = Get_All_SubCate(cateprice_id);
        }


        //获取推荐标签信息
        ProductTagInfo Taginfo = MyTag.GetProductTagByValue(tag, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (Taginfo != null)
        {
            Query.PageSize = Show_Num;
            Query.CurrentPage = curpage;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID in (" + Taginfo.Product_Tag_ID.ToString() + ")"));
            int brand_id = tools.CheckInt(Request["brand_hot_id"]);
            if (brand_id > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
            }

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            //聚合是否列表显示 暂时屏蔽掉
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));

            //按分类查询
            if (cate_id > 0 && Cate_Arry.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }

            //按款式查询
            if (collection_id > 0 && collection_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
            }

            //按价格查询
            if (cateprice_id > 0 && cateprice_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
            }

            foreach (string querystring in Request.QueryString)
            {
                if (querystring != null)
                {
                    if (querystring.IndexOf("filter_hot_") >= 0)
                    {
                        Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                        if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                        {
                            Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                        }
                    }
                }
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));

            return Query;
        }
        else
        {
            return new QueryInfo();
        }
    }

    public int TradeIndex_Hot_ProductCount(string tag)
    {
        QueryInfo Query = TradeIndex_Hot_ProductQuery(tag, 0);
        IList<ProductInfo> listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (listProduct != null)
        {
            return listProduct.Count;
        }
        else
        {
            return 0;
        }
    }

    public int TradeIndex_Hot_ProductListCount(string tag)
    {
        int pageCount = 0;
        PageInfo pageInfo = null;
        QueryInfo Query = TradeIndex_Hot_ProductQuery(tag, 16);

        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (pageInfo.PageCount > 0)
        {
            pageCount = pageInfo.PageCount;
        }
        else
        {
            pageCount = 0;
        }
        return pageCount;
    }

    public void TradeIndex_Hot_ProductList(string Tag)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ProductInfo> listProduct = null;
        PageInfo pageInfo = null;

        SupplierShopInfo shopInfo = null;
        int i = 0;
        string shopName = string.Empty, shopUrl = string.Empty;

        QueryInfo Query = TradeIndex_Hot_ProductQuery(Tag, 16);
        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (listProduct != null)
        {
            foreach (ProductInfo entity in listProduct)
            {
                i++;

                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                if (shopInfo != null)
                {
                    shopName = shopInfo.Shop_Name;
                    shopUrl = shopInfo.Shop_Domain;
                }

                if (i % 4 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>新品</i></span></div>");
                strHTML.Append("<div class=\"p_box\">");

                strHTML.Append(Product_List_WholeSalePrice(entity));

                strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></div>");
                strHTML.Append("<div class=\"p_box_info03\">");
                strHTML.Append("<span><a href=\"" + supplier_class.GetShopURL(shopUrl) + "\" target=\"_blank\">" + shopName + "</a><span style=\"float:right;cursor:pointer;\"  onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img style=\"display:inline-block; vertical-align:middle; margin-left:10px;width:74px;\" src=\"/images/icon04.png\" style=\"width:74px;\"></span></span>");
                strHTML.Append("<div class=\"p_box_info03_fox\">");
                strHTML.Append("<i></i>");

                strHTML.Append(Product_List_SupplierInfo(supplier_class.GetSupplierByID(entity.Product_SupplierID)));

                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("</li>");
            }
        }
        Response.Write(strHTML.ToString());
    }


    public void TradeIndex_Hot_Product_Filter(int cate_id, int cate_parentid, int cate_typeid)
    {
        int catefatherid = cate_id;
        string Cate_Arry = Get_First_SubCate(cate_id);
        string collection_array = "54", cateprice_array = "55";
        string keyword, isgallerylist, orderby, Extend_Value;
        double price_min, price_max;
        int isgroup, ispromotion, isRecommend, isgift, Brand_ID, tag_id, target, type, collection_id, cateprice_id;
        int icount = 1;

        IList<ProductTypeExtendInfo> extends = null;

        Brand_ID = tools.CheckInt(Request["brand_hot_id"]);
        tag_id = tools.CheckInt(Request["tag_hot_id"]);
        target = tools.CheckInt(Request["target_hot"]);
        type = tools.CheckInt(Request["type_hot"]);

        collection_id = tools.CheckInt(Request["collection_hot_id"]);
        if (collection_id == 0)
        {
            collection_id = 54;
        }

        cateprice_id = tools.CheckInt(Request["cateprice_hot_id"]);
        if (cateprice_id == 0)
        {
            cateprice_id = 55;
        }

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"screen\">");
        //strHTML.Append("<h2 style=\"height: 40px;background-color: #f5f5f5;font-size: 16px;font-weight: bold;color: #333; line-height: 40px;padding: 0 10px;border-bottom: 2px solid #f60001;\"><a style=\"font-size: 12px;font-weight: normal;color: #666;float: right;display: inline;\" href=\"/tradeindex/iframe_product.aspx?cate_id=" + cate_id + "&type=" + type + "&target=" + target + "\">重置筛选条件</a></h2>");
        strHTML.Append("<dl>");
        strHTML.Append("<dt>分类：</dt>");
        strHTML.Append(TradeIndex_Hot_Product_Cate_list(cate_id, Cate_Arry, cate_parentid));
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        bool extend_show = true;
        HttpCookie cookie = Request.Cookies["extend_div"];
        if (cookie == null || cookie.Value.Equals("hide"))
        {
            extend_show = false;
        }

        if (cate_id > 0 && cate_typeid > 0)
        {
            ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (producttype != null)
            {
                IList<BrandInfo> brands = producttype.BrandInfos;
                if (brands != null)
                {
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt>品牌：</dt>");
                    strHTML.Append(TradeIndex_Hot_Product_Brand_list(brands, cate_id, Brand_ID));
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                }
                extends = producttype.ProductTypeExtendInfos;
                if (extends != null)
                {
                    foreach (ProductTypeExtendInfo entity in extends)
                    {
                        if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                        {
                            strHTML.Append(TradeIndex_Hot_Product_Extend_list(entity));

                            if (icount == 3)
                            {
                                strHTML.Append("<span " + (extend_show ? "" : "style=\"display:none\"") + " id=\"hidden_div\">");
                            }

                            icount++;
                        }
                    }
                }
            }
            if (icount > 3)
            {
                strHTML.Append("</span>");
            }
        }
        else
        {
            strHTML.Append("<dl>");
            strHTML.Append("<dt>品牌：</dt>");
            strHTML.Append(TradeIndex_Hot_Product_Brand_list(null, cate_id, Brand_ID));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>款式：</dt>");
            strHTML.Append(TradeIndex_Hot_Product_Cates_list("collection_hot_id", collection_id, collection_array, 54));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>价格：</dt>");
            strHTML.Append(TradeIndex_Hot_Product_Cates_list("cateprice_hot_id", cateprice_id, cateprice_array, 55));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");


        }
        strHTML.Append("</div>");

        if (cate_id > 0 && cate_typeid > 0)
        {
            if (icount > 3)
            {
                strHTML.Append("<div class=\"blk005\">");
                if (extend_show)
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:hidden_showdiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02_2.png\" /></span></a>");
                }
                else
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:show_hiddendiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02.png\" /></span></a>");
                }
                strHTML.Append("</div>");
            }
        }

        Response.Write(strHTML.ToString());

        keyword = tools.CheckStr(Request["keyword_hot"]);
        isgallerylist = tools.CheckStr(Request["isgallerylist_hot"]);
        orderby = tools.CheckStr(Request["orderby_hot"]);
        price_min = tools.CheckFloat(Request["price_hot_min"]);
        price_max = tools.CheckFloat(Request["price_hot_max"]);
        isgroup = tools.CheckInt(Request["isgroup_hot"]);
        ispromotion = tools.CheckInt(Request["ispromotion_hot"]);
        isRecommend = tools.CheckInt(Request["isRecommend_hot"]);
        isgift = tools.CheckInt(Request["isgift_hot"]);
        int instock = tools.CheckInt(Request["instock_hot"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();
        isgallerylist = isgallerylist.Replace("\"", "&quot;").ToString();
        orderby = orderby.Replace("\"", "&quot;").ToString();

        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_hot_filter\" id=\"form_hot_filter\" method=\"string\" action=\"\">");
        Response.Write("<input type=\"hidden\" name=\"tag_hot_id\" id=\"tag_hot_id\" value=\"" + tag_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_hot_id\" id=\"cate_hot_id\" value=\"" + cate_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"brand_hot_id\" id=\"brand_hot_id\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"keyword_hot\" id=\"keyword_hot\" value=\"" + keyword + "\">");
        Response.Write("<input type=\"hidden\" name=\"isgallerylist_hot\" id=\"isgallerylist_hot\" value=\"" + isgallerylist + "\">");
        Response.Write("<input type=\"hidden\" name=\"orderby_hot\" id=\"orderby_hot\" value=\"" + orderby + "\">");
        Response.Write("<input type=\"hidden\" name=\"instock_hot\" id=\"instock_hot\" value=\"" + instock + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_hot_min\" id=\"price_hot_min\" value=\"" + price_min + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_hot_max\" id=\"price_hot_max\" value=\"" + price_max + "\">");
        Response.Write("<input type=\"hidden\" name=\"target_hot\" id=\"target_hot\" value=\"" + target + "\">");
        Response.Write("<input type=\"hidden\" name=\"type_hot\" id=\"type_hot\" value=\"" + type + "\">");
        Response.Write("<input type=\"hidden\" name=\"collection_hot_id\" id=\"collection_hot_id\" value=\"" + collection_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cateprice_hot_id\" id=\"cateprice_hot_id\" value=\"" + cateprice_id + "\">");

        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                {
                    Extend_Value = tools.NullStr(Request["filter_hot_" + entity.ProductType_Extend_ID]).Trim();
                    Response.Write("<input type=\"hidden\" name=\"filter_hot_" + entity.ProductType_Extend_ID + "\" id=\"filter_hot_" + entity.ProductType_Extend_ID + target + "\" value=\"" + Extend_Value + "\">");
                }
            }
        }
        Response.Write("</form>");
        Response.Write("</div>");
    }

    public string TradeIndex_Hot_Product_Cate_list(int cate_id, string Cate_Arry, int cate_parentid)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["cate_hot_id"]);
        if (Cate_Arry == cate_id.ToString())
        {
            strHTML.Append("<a href=\"javascript:set_hot_form_filter('cate_hot_id'," + cate_parentid + ");\" ");
        }
        else
        {
            strHTML.Append("<a href=\"javascript:set_hot_form_filter('cate_hot_id'," + cate_id + ");\"");
        }

        if (Cate_Arry == cate_id.ToString())
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }

        strHTML.Append(">全部</a>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Cate_Arry == cate_id.ToString())
        {
            Cate_Arry = Get_First_SubCate(cate_parentid);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_parentid.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Cate_Arry + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:set_hot_form_filter('cate_hot_id'," + entity.Cate_ID + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }

            if (cate_parentid > 0 && cate_parentid != 58 && cate_parentid != 59 && cate_parentid != 61 && cate_parentid != 62)
            {
                strHTML.Append("<span><a href=\"javascript:set_hot_form_filter('cate_hot_id'," + cate_parentid + ");\">上一级</a></span>");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }

    public string TradeIndex_Hot_Product_Brand_list(IList<BrandInfo> brands, int Cate_ID, int Brand_ID)
    {
        StringBuilder strHTML = new StringBuilder("<dd>");

        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('brand_hot_id','');\"");
        if (Brand_ID > 0)
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (brands != null)
        {
            foreach (BrandInfo entity in brands)
            {
                if (entity.Brand_IsActive == 1)
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('brand_hot_id'," + entity.Brand_ID + ");\" ");
                    if (Brand_ID == entity.Brand_ID)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + entity.Brand_Name + "</a>");
                }
            }
        }
        else
        {
            QueryInfo Query = new QueryInfo();
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "desc"));

            IList<BrandInfo> listBrand = MyBrand.GetBrands(Query, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));

            if (listBrand != null)
            {
                foreach (BrandInfo entity in listBrand)
                {
                    if (entity.Brand_IsActive == 1)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('brand_hot_id'," + entity.Brand_ID + ");\" ");
                        if (Brand_ID == entity.Brand_ID)
                        {
                            strHTML.Append(" class=\"on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + entity.Brand_Name + "</a>");
                    }
                }
            }
        }
        strHTML.Append("</dd>");
        return strHTML.ToString();
    }

    public string TradeIndex_Hot_Product_Extend_list(ProductTypeExtendInfo extend)
    {
        string Extend_Value = tools.NullStr(Request["filter_" + extend.ProductType_Extend_ID]).Trim();
        string default_value = extend.ProductType_Extend_Default;

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dl>");
        strHTML.Append("<dt>" + extend.ProductType_Extend_Name + "：</dt><dd>");
        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('filter_hot_" + extend.ProductType_Extend_ID + "','');\"");

        if (Extend_Value != "")
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (default_value.Length > 0)
        {
            foreach (string extend_value in default_value.Split('|'))
            {
                if (extend_value != "")
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('filter_hot_" + extend.ProductType_Extend_ID + "','" + extend_value + "');\"");
                    if (Extend_Value == extend_value)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + extend_value + "</a></span>");
                }
            }
        }
        else
        {
            string Exit_Extend = "||";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ExtendID", "=", extend.ProductType_Extend_ID.ToString()));
            IList<ProductExtendInfo> entitys = Myproduct.GetProductExtends(Query);
            if (entitys != null)
            {
                foreach (ProductExtendInfo Pre_Extend in entitys)
                {
                    //检查属性重复性
                    if (Exit_Extend.IndexOf("|" + Pre_Extend.Extend_Value + "|") < 0)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:set_hot_form_filter('filter_hot_" + extend.ProductType_Extend_ID + "','" + Pre_Extend.Extend_Value + "');\"");
                        if (Extend_Value == Pre_Extend.Extend_Value)
                        {
                            strHTML.Append(" class=\"on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + Pre_Extend.Extend_Value + "</a> ");

                        Exit_Extend += Pre_Extend.Extend_Value + "|";
                    }
                }
            }
        }
        strHTML.Append("</dd>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        return strHTML.ToString();
    }

    public string TradeIndex_Hot_Product_Cates_list(string cate_name, int cate_id, string Cate_Arry, int cate_parentid)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["" + cate_name + ""]);

        strHTML.Append("<a href=\"javascript:set_hot_form_filter('" + cate_name + "'," + cate_parentid + ");\"");
        if (cate_id == 54 || cate_id == 55)
        {
            strHTML.Append(" class=\"on\"");
        }
        else
        {

        }
        strHTML.Append(">全部</a>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + cate_parentid + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:set_hot_form_filter('" + cate_name + "'," + entity.Cate_ID + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }


    #endregion


    #endregion

    #region 商品信息

    //添加评论页 商品信息
    public void Get_ProducInfo_Review(ProductInfo entity)
    {
        string HTML = "";
        HTML += "<div style=\" width:214px; float:left;margin-right:7px; border:1px solid #cccccc;padding-top:10px;padding-bottom:10px;\">";
        HTML += "<table cellpadding=\"2\" width=\"206\" cellspacing=\"0\" border=\"0\" style=\"padding-left:12px;\">";
        HTML += "<tr><td style=\" text-align:center;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" style=\"cursor:pointer;display:inline;\" width=\"190\" height=\"190\" alt=\"" + entity.Product_Name + "\" onload=\"javascript:AutosizeImage(this,190,190);\"></a></td></tr>";
        HTML += "<tr><td style=\"line-height:20px;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" alt=\"" + entity.Product_Name + "\" title=\"" + entity.Product_Name + "\" class=\"a_t12_green\">" + entity.Product_Name + "</a></td></tr>";


        HTML += "<tr><td style=\"height:20px;\">评价得分：";
        for (int i = 1; i <= (int)entity.Product_Review_Average; i++)
        {
            HTML += "<img src=\"/images/x2.jpg\"  style=\"width:18px;display:inline\"/>";
        }
        for (int i = 1; i <= 5 - (int)entity.Product_Review_Average; i++)
        {
            HTML += "<img src=\"/images/x.jpg\"  style=\"display:inline\"/>";
        }

        HTML += "</td></tr>";
        HTML += "<tr><td style=\"height:20px;\">评论数：" + entity.Product_Review_ValidCount + "条</td></tr>";
        HTML += "<tr><td style=\"text-align:center;\"><img src=\"/images/btn_addcart.jpg\" onclick=\"javascript:location.href='" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "'\" style=\"cursor:pointer; width:143px; height:34px; display:inline;\" /><br/>";
        //HTML += "<a href=\"/member/fav_do.aspx?action=goods&id=" + entity.Product_ID + "\" target=\"_blank\"><img src=\"/images/btn_addfavor.jpg\" style=\"cursor:pointer;margin-top:5px;display:inline;\" width=\"113\" height=\"30\" border=\"0\" alt=\"收藏该商品\" /></a>";
        HTML += "<a href=\"javascript:void(0)\" onclick=\"product_favorites( " + entity.Product_ID + ")\" ><img src=\"/images/btn_addfavor.jpg\" style=\"cursor:pointer;margin-top:5px;display:inline;\" width=\"113\" height=\"30\" border=\"0\" alt=\"收藏该商品\" /></a>";
        HTML += "</td></tr>";
        HTML += "</table></div>";
        Response.Write(HTML);
    }

    /// <summary>
    /// 列表页相关分类
    /// </summary>
    /// <param name="cate_id">类别id</param>
    /// <param name="parent_id">父级ID</param>
    /// <returns></returns>
    public string GetRelateCategory_bak(int cate_id, int parent_id)
    {
        StringBuilder sb = new StringBuilder();
        IList<CategoryInfo> entitys1 = null;
        IList<CategoryInfo> entitys2 = null;
        IList<CategoryInfo> entitys3 = null;
        int parent_parent_id = parent_id;
        //获取子类数量
        int subCount = MyCate.GetSubCateCount(cate_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));

        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = 0;
        Query1.CurrentPage = 1;



        //顶级类
        if (parent_id == 0)
        {
            parent_parent_id = cate_id;
            entitys1 = MyCate.GetSubCategorys(parent_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        }
        else
        {
            CategoryInfo cateinfo = GetCategoryByID(parent_id);
            if (cateinfo != null)
            {
                if (subCount != 0)
                {
                    parent_parent_id = parent_id;
                    entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                }
                else
                {
                    //二级类
                    if (cateinfo.Cate_ParentID == 0)
                    {
                        entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                    }
                    else//三级以下
                    {
                        CategoryInfo cateinfo2 = GetCategoryByID(cateinfo.Cate_ParentID);
                        if (cateinfo2 != null)
                        {
                            parent_parent_id = cateinfo2.Cate_ID;
                            entitys1 = MyCate.GetSubCategorys(cateinfo2.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                        }
                    }
                }

            }
        }



        if (entitys1 != null)
        {
            foreach (CategoryInfo entity1 in entitys1)
            {
                if (parent_parent_id == entity1.Cate_ID)
                {
                    sb.Append("<div class=\"blk02_info\">");
                    sb.Append("<h2 class=\"on swapCate\"><a class=\"leftCateSel_1\" href=\"/product/category.aspx?cate_id=" + entity1.Cate_ID + "\">" + entity1.Cate_Name + "</a></h2>");
                    sb.Append("<div class=\"blk02_info_main\">");
                    entitys2 = MyCate.GetSubCategorys(entity1.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                    if (entitys2 != null)
                    {
                        foreach (CategoryInfo entity2 in entitys2)
                        {



                            if (parent_id == entity2.Cate_ID || cate_id == entity2.Cate_ID)
                            {
                                sb.Append("<h3><a class=\"leftCateSel\" href=\"/product/category.aspx?cate_id=" + entity2.Cate_ID + "\">" + entity2.Cate_Name + "</a></h3>");

                                entitys3 = MyCate.GetSubCategorys(entity2.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                                if (entitys3 != null)
                                {
                                    sb.Append("<ul>");

                                    foreach (CategoryInfo entity3 in entitys3)
                                    {
                                        if (cate_id == entity3.Cate_ID)
                                        {
                                            sb.Append("<li><a class=\"leftCateSel\" href=\"/product/category.aspx?cate_id=" + entity3.Cate_ID + "\">" + entity3.Cate_Name + "</a></li>");
                                        }
                                        else
                                        {
                                            sb.Append("<li><a href=\"/product/category.aspx?cate_id=" + entity3.Cate_ID + "\">" + entity3.Cate_Name + "</a></li>");

                                        }
                                    }

                                    sb.Append("</ul>");


                                }
                            }
                            else
                            {
                                sb.Append("<h3><a href=\"/product/category.aspx?cate_id=" + entity2.Cate_ID + "\">" + entity2.Cate_Name + "</a></h3>");
                            }
                        }
                    }
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div class=\"blk02_info\">");
                    sb.Append("<h2 class=\"swapCate\"><a class=\"leftCateSel_1\" href=\"/product/category.aspx?cate_id=" + entity1.Cate_ID + "\">" + entity1.Cate_Name + "</a></h2>");
                    sb.Append("<div class=\"blk02_info_main\" style=\"display:none;\">");



                    entitys2 = MyCate.GetSubCategorys(entity1.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                    if (entitys2 != null)
                    {
                        foreach (CategoryInfo entity2 in entitys2)
                        {



                            if (parent_id == entity2.Cate_ID || cate_id == entity2.Cate_ID)
                            {
                                sb.Append("<h3><a class=\"leftCateSel\" href=\"/product/category.aspx?cate_id=" + entity2.Cate_ID + "\">" + entity2.Cate_Name + "</a></h3>");

                                entitys3 = MyCate.GetSubCategorys(entity2.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                                if (entitys3 != null)
                                {
                                    sb.Append("<ul>");

                                    foreach (CategoryInfo entity3 in entitys3)
                                    {
                                        if (cate_id == entity3.Cate_ID)
                                        {
                                            sb.Append("<li><a class=\"leftCateSel\" href=\"/product/category.aspx?cate_id=" + entity3.Cate_ID + "\">" + entity3.Cate_Name + "</a></li>");
                                        }
                                        else
                                        {
                                            sb.Append("<li><a href=\"/product/category.aspx?cate_id=" + entity3.Cate_ID + "\">" + entity3.Cate_Name + "</a></li>");

                                        }
                                    }

                                    sb.Append("</ul>");


                                }
                            }
                            else
                            {
                                sb.Append("<h3><a href=\"/product/category.aspx?cate_id=" + entity2.Cate_ID + "\">" + entity2.Cate_Name + "</a></h3>");
                            }
                        }
                    }



                    sb.Append("</div>");
                    sb.Append("</div>");
                }

            }

        }



        sb.Append("<script type=\"text/javascript\">");
        sb.Append("$(document).ready(function () {");
        sb.Append("$(\".swapCate\").click(function () {");
        sb.Append("$(this).toggleClass(\"on\",\"\");");
        sb.Append("$(this).next(\".blk02_info_main\").slideToggle();");
        sb.Append("});");
        sb.Append("});");
        sb.Append("");
        sb.Append("</script>");


        return sb.ToString();
    }

    public string GetRelateCategory(int cate_id, int parent_id)
    {
        StringBuilder sb = new StringBuilder();

        IList<CategoryInfo> entitys1 = null;
        IList<CategoryInfo> entitys2 = null;
        IList<CategoryInfo> entitys3 = null;
        int parent_parent_id = parent_id;
        int i = 0;

        //获取子类数量
        int subCount = MyCate.GetSubCateCount(cate_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));

        if (parent_id == 0)
        {
            parent_parent_id = cate_id;
            entitys1 = MyCate.GetSubCategorys(parent_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        }
        else
        {
            CategoryInfo cateinfo = GetCategoryByID(parent_id);
            if (cateinfo != null)
            {
                if (subCount != 0)
                {
                    parent_parent_id = parent_id;
                    entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                }
                else
                {
                    //二级类
                    if (cateinfo.Cate_ParentID == 0)
                    {
                        entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                    }
                    else//三级以下
                    {
                        CategoryInfo cateinfo2 = GetCategoryByID(cateinfo.Cate_ParentID);
                        if (cateinfo2 != null)
                        {
                            parent_parent_id = cateinfo2.Cate_ID;
                            entitys1 = MyCate.GetSubCategorys(cateinfo2.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                        }
                    }
                }
            }
        }

        if (entitys1 != null)
        {
            foreach (CategoryInfo entity1 in entitys1)
            {
                i++;
                if (i == 1)
                {
                    sb.Append("<div class=\"b28_info\">");
                    sb.Append("<h3><span onclick=\"openShutManager(this,'box')\" ><a id=\"" + i + "\" href=\"javascript:;\"  class=\"hidecontent\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                    sb.Append("<div class=\"b28_info_main\" id=\"box\">");
                }
                else
                {
                    sb.Append("<div class=\"b28_info\">");
                    sb.Append("<h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\" href=\"javascript:;\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                    sb.Append("<div class=\"b28_info_main\" id=\"box" + i + "\"  style=\"display:none;\">");
                }
                entitys2 = MyCate.GetSubCategorys(entity1.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                sb.Append("<ul>");
                if (entitys2 != null)
                {
                    foreach (CategoryInfo entity2 in entitys2)
                    {
                        sb.Append("<li><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity2.Cate_ID.ToString(), "", "", "", "")) + "\">" + entity2.Cate_Name + "</a></li>");
                    }
                }
                sb.Append("</ul>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }

        return sb.ToString();
    }

    public string GetRelateCategory_Search(int cate_id, int parent_id)
    {
        StringBuilder sb = new StringBuilder();

        string keyword = tools.CheckStr(Request["keyword"]);

        IList<CategoryInfo> entitys1 = null;
        IList<CategoryInfo> entitys2 = null;
        IList<CategoryInfo> entitys3 = null;
        int parent_parent_id = parent_id;
        int i = 0;

        //获取子类数量
        int subCount = MyCate.GetSubCateCount(cate_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));

        if (parent_id == 0)
        {
            parent_parent_id = cate_id;
            entitys1 = MyCate.GetSubCategorys(parent_id, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        }
        else
        {
            CategoryInfo cateinfo = GetCategoryByID(parent_id);
            if (cateinfo != null)
            {
                if (subCount != 0)
                {
                    parent_parent_id = parent_id;
                    entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                }
                else
                {
                    //二级类
                    if (cateinfo.Cate_ParentID == 0)
                    {
                        entitys1 = MyCate.GetSubCategorys(cateinfo.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                    }
                    else//三级以下
                    {
                        CategoryInfo cateinfo2 = GetCategoryByID(cateinfo.Cate_ParentID);
                        if (cateinfo2 != null)
                        {
                            parent_parent_id = cateinfo2.Cate_ID;
                            entitys1 = MyCate.GetSubCategorys(cateinfo2.Cate_ParentID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                        }
                    }
                }
            }
        }

        if (entitys1 != null)
        {
            foreach (CategoryInfo entity1 in entitys1)
            {
                i++;
                if (i == 1)
                {
                    sb.Append("<div class=\"b28_info\">");
                    if (entity1.Cate_ID == parent_id)
                    {
                        sb.Append("<h3><span onclick=\"openShutManager(this,'box')\" ><a id=\"" + i + "\" name=\"switchTag\" href=\"javascript:;\"  class=\"hidecontent\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                        sb.Append("<div class=\"b28_info_main\" id=\"box\">");
                    }
                    else
                    {
                        sb.Append("<h3><span onclick=\"openShutManager(this,'box')\" ><a id=\"" + i + "\" name=\"switchTag\" href=\"javascript:;\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                        sb.Append("<div class=\"b28_info_main\" id=\"box\"  style=\"display:none;\">");
                    }
                }
                else
                {
                    sb.Append("<div class=\"b28_info\">");


                    if (entity1.Cate_ID == parent_id)
                    {
                        sb.Append("<h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\" name=\"switchTag\" href=\"javascript:;\"  class=\"hidecontent\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                        sb.Append("<div class=\"b28_info_main\" id=\"box" + i + "\" >");
                    }
                    else
                    {
                        sb.Append("<h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\" name=\"switchTag\" href=\"javascript:;\" onClick=\"switchTag(" + i + ");\">" + entity1.Cate_Name + "</a></span></h3>");
                        sb.Append("<div class=\"b28_info_main\" id=\"box" + i + "\"  style=\"display:none;\">");
                    }
                }
                entitys2 = MyCate.GetSubCategorys(entity1.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                sb.Append("<ul>");
                if (entitys2 != null)
                {
                    foreach (CategoryInfo entity2 in entitys2)
                    {
                        if (entity2.Cate_ID == cate_id)
                        {
                            sb.Append("<li class=\"on\"><a href=\"/product/search.htm?cate_id=" + entity2.Cate_ID + "&keyword=" + keyword + "\">" + entity2.Cate_Name + "</a></li>");
                        }
                        else
                        {
                            sb.Append("<li><a href=\"/product/search.htm?cate_id=" + entity2.Cate_ID + "&keyword=" + keyword + "\">" + entity2.Cate_Name + "</a></li>");
                        }
                    }
                }
                sb.Append("</ul>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }

        return sb.ToString();
    }

    //商品详情 类别链接
    public string GetCateMapByCateID(int ID)
    {
        string HTML = "";
        int CateID = ID;
        CategoryInfo cateinfo = GetCategoryByID(CateID);

        while (cateinfo != null)
        {
            HTML = "&nbsp;>&nbsp;<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(cateinfo.Cate_ID.ToString(), "", "", "", "")) + "\">" + cateinfo.Cate_Name + "</a>" + HTML;
            cateinfo = GetCategoryByID(cateinfo.Cate_ParentID);
        }
        if (ID == 0)
        {
            HTML = "&nbsp;>&nbsp;<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam("", "", "", "", "")) + "\">所有商品分类</a>";
        }
        return HTML;
    }


    //设置频道session
    public void Set_Cate_Session(int cate_id)
    {
        //Session["Cur_Position"] = cate_id;
        switch (cate_id)
        {
            case 47:
                Session["Cur_Position"] = "47";
                break;
            case 46:
                Session["Cur_Position"] = "46";
                break;
            case 222:
                Session["Cur_Position"] = "222";
                break;
            case 44:
                Session["Cur_Position"] = "44";
                break;
            case 223:
                Session["Cur_Position"] = "223";
                break;
        }
    }

    public int getCateParentID(int cate_id)
    {
        int parent_id = cate_id;
        if (cate_id != 0)
        {
            CategoryInfo category = MyCate.GetCategoryByID(cate_id, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
            if (category != null)
            {
                if (category.Cate_ParentID != 0)
                {
                    parent_id = getCateParentID(category.Cate_ParentID);
                }
            }
        }
        return parent_id;
    }

    //商品频道特价促销商品
    public string Product_Rromotion_Product(int Show_Num, int Cate_ID)
    {
        string product_list = "";
        int i = 0;
        IList<ProductInfo> products = GetCateTagProduct(Show_Num, Cate_ID, "频道特价促销");
        if (products != null)
        {
            product_list = product_list + "<ul>";
            foreach (ProductInfo entity in products)
            {
                i = i + 1;
                product_list = product_list + "<li><div><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\" width=\"120\" height=\"120\" alt=\"" + entity.Product_Name + "\" /></a></div><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 20) + "</a><br /><span class=\"t12_orange\" style=\"color:" + entity.Product_NoteColor + ";\">&nbsp;" + tools.CutStr(entity.Product_Note, 20) + "</span></p><p class=\"price_p\"><span class=\"m_price\">" + pub.FormatCurrency(entity.Product_MKTPrice) + "</span> <span class=\"p_price\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></p></li>";
                i = i + 1;
                if (i < Show_Num)
                {
                    product_list = product_list + "<li class=\"li_gap\"></li>";
                }

            }
            product_list = product_list + "</ul>";
        }
        return product_list;

    }

    /// <summary>
    /// 商品栏目左侧销售排行
    /// </summary>
    /// <param name="Show_Num">显示格式</param>
    /// <param name="Cate_ID">类别ID</param>
    /// <returns></returns>
    public string Product_LeftSale_Product(int Show_Num, int Cate_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;
        string sub_cate = Get_All_SubCate(Cate_ID);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        if (sub_cate.Length > 0 && Cate_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", sub_cate));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + sub_cate + ")"));
        }
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (Products != null)
        {
            foreach (ProductInfo entity in Products)
            {
                i++;
                if (i < 4)
                {
                    strHTML.Append(" <li><i class=\"bg\">" + i + "</i><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 23) + "</a></li>");
                }
                else
                {
                    strHTML.Append(" <li><i>" + i + "</i><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 23) + "</a></li>");
                }
            }
        }
        return strHTML.ToString();
    }


    /// <summary>
    /// 商品栏目左侧销售排行
    /// </summary>
    /// <param name="Show_Num">显示格式</param>
    /// <param name="Cate_ID">类别ID</param>
    /// <returns></returns>
    public string Product_LeftSale_Product_new(string Tag_Name, int Show_Num, int Tag_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;
        //string sub_cate = Get_All_SubCate(Cate_ID);
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = Show_Num;
        //Query.CurrentPage = 1;

        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        //if (sub_cate.Length > 0 && Cate_ID > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", sub_cate));
        //    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + sub_cate + ")"));
        //}
        //Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        //IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        IList<ProductInfo> Products = GetCateTagProduct_TagID(Show_Num, 0, Tag_ID);
        if (Products != null)
        {
            foreach (ProductInfo entity in Products)
            {
                i++;
                if (i < 4)
                {
                    strHTML.Append(" <li><i class=\"bg\">" + i + "</i><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 23) + "</a></li>");
                }
                else
                {
                    strHTML.Append(" <li><i>" + i + "</i><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 23) + "</a></li>");
                }
            }
        }
        return strHTML.ToString();
    }

    /// <summary>
    /// 商品左侧最近浏览记录
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <returns></returns>
    public string Product_Left_LastView_Product(int Show_Num)
    {
        //读Cookies
        int i = 0;
        string product_list = "";
        string viewhistory, formatviewhistory;
        if (Request.Cookies["product_viewhistory_zh-cn"] == null)
        {
            Response.Cookies["product_viewhistory_zh-cn"].Value = "";
        }

        viewhistory = tools.NullStr(Request.Cookies["product_viewhistory_zh-cn"].Value);
        viewhistory = viewhistory.Trim();

        formatviewhistory = "0";

        if (viewhistory != "")
        {
            foreach (string id in viewhistory.Split(','))
            {
                if (tools.CheckInt(id) > 0)
                {
                    formatviewhistory = formatviewhistory + "," + id;
                }
            }
        }

        StringBuilder strHTML = new StringBuilder();

        if (formatviewhistory != "0")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = Show_Num;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", formatviewhistory));
            //聚合是否列表显示 暂时屏蔽掉
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (Products != null)
            {

                foreach (string id in formatviewhistory.Split(','))
                {
                    foreach (ProductInfo entity in Products)
                    {
                        if (entity.Product_ID.ToString() == id)
                        {
                            strHTML.Append("<dl>");
                            strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">");
                            strHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" /></a></dt>");
                            strHTML.Append("<dd>");
                            strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 50) + "</a></p>");
                            if (entity.Product_PriceType == 1)
                            {
                                strHTML.Append("<p><strong>" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong></p>");
                            }
                            else
                            {
                                strHTML.Append("<p><strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong></p>");
                            }
                            strHTML.Append("</dd>");
                            strHTML.Append("<div class=\"clear\"></div>");
                            strHTML.Append("</dl>");
                        }
                    }
                }
            }
        }
        return strHTML.ToString();
    }

    //商品详情页最近浏览记录 /**/
    public string Product_Detail_Left(int Show_Num)
    {
        //读Cookies
        int i = 0;
        string product_list = "";
        string viewhistory, formatviewhistory;
        if (Request.Cookies["product_viewhistory_zh-cn"] == null)
        {
            Response.Cookies["product_viewhistory_zh-cn"].Value = "";
        }

        viewhistory = tools.NullStr(Request.Cookies["product_viewhistory_zh-cn"].Value);
        viewhistory = viewhistory.Trim();

        formatviewhistory = "0";

        if (viewhistory != "")
        {
            foreach (string id in viewhistory.Split(','))
            {
                if (tools.CheckInt(id) > 0)
                {
                    formatviewhistory = formatviewhistory + "," + id;
                }
            }
        }

        if (formatviewhistory != "0")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = Show_Num;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", formatviewhistory));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (Products != null)
            {
                product_list = product_list + "<div id=\"che2left1\" style=\"margin-top:10px;\">";
                product_list = product_list + "  <h3>浏览过的商品</h3>";
                product_list = product_list + "  <div id=\"gou-list\">";
                product_list = product_list + "    <ul style=\" padding-bottom:5px;\">";
                foreach (string id in formatviewhistory.Split(','))
                {
                    foreach (ProductInfo entity in Products)
                    {
                        if (entity.Product_ID.ToString() == id)
                        {
                            product_list = product_list + "<li><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" width=\"45\" height=\"45\" onload=\"javascript:AutosizeImage(this,45,45);\" title=\"" + entity.Product_Name + "\" target=\"_blank\" /></a><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\">" + tools.Left(entity.Product_Name, 10) + "</a></p><p>" + tools.Left(entity.Product_Spec, 18) + "</p><p class=\"rr\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</p></li>";

                        }
                    }
                }

                product_list = product_list + "    </ul>";
                product_list = product_list + "  </div>";
                product_list = product_list + "</div>";
            }
        }
        return product_list;
    }

    //记录商品最近浏览记录
    public void Recent_View_Add(int product_id)
    {
        //置浏览记录

        string viewhistory, new_history;
        int i = 0;

        Response.Cookies["product_viewhistory_zh-cn"].Expires = DateTime.Now.AddYears(-1);
        if (Request.Cookies["product_viewhistory_zh-cn"] == null)
        {
            Response.Cookies["product_viewhistory_zh-cn"].Value = "";
        }
        viewhistory = tools.NullStr(Request.Cookies["product_viewhistory_zh-cn"].Value);
        if (viewhistory == "")
        {
            viewhistory = ",";
        }
        else
        {
            //消除重复记录
            viewhistory = viewhistory.Replace("," + product_id + ",", ",");
        }
        viewhistory = "," + product_id + viewhistory;
        new_history = ",";
        //只保留前10个浏览记录
        foreach (string id in viewhistory.Split(','))
        {
            if (tools.CheckInt(id) > 0)
            {
                i = i + 1;
                new_history = new_history + id + ",";
                if (i > 10)
                {
                    break;
                }
            }

        }

        //写Cookies
        Response.Cookies["product_viewhistory_zh-cn"].Expires = DateTime.Now.AddYears(1);
        Response.Cookies["product_viewhistory_zh-cn"].Value = new_history;
    }


    /// <summary>
    /// 采购商中心首页最近浏览商品
    /// </summary>
    /// <returns></returns>
    public string Member_Index_Right_LastView_Product()
    {
        StringBuilder strHTML = new StringBuilder();

        string viewhistory, formatviewhistory;
        if (Request.Cookies["product_viewhistory_zh-cn"] == null)
        {
            Response.Cookies["product_viewhistory_zh-cn"].Value = "";
        }

        viewhistory = tools.NullStr(Request.Cookies["product_viewhistory_zh-cn"].Value);
        viewhistory = viewhistory.Trim();

        formatviewhistory = "0";

        if (viewhistory != "")
        {
            foreach (string id in viewhistory.Split(','))
            {
                if (tools.CheckInt(id) > 0)
                {
                    formatviewhistory = formatviewhistory + "," + id;
                }
            }
        }


        strHTML.Append("<ul>");

        if (formatviewhistory != "0")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 3;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", formatviewhistory));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
            //IList<ProductInfo> Products =Myproduct.get GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (Products != null)
            {

                foreach (string id in formatviewhistory.Split(','))
                {
                    foreach (ProductInfo entity in Products)
                    {
                        if (entity.Product_ID.ToString() == id)
                        {
                            //strHTML.Append("<li><div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 20) + "</a></p>");
                            strHTML.Append(" <li><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">");
                            strHTML.Append("     " + tools.CutStr(entity.Product_Name, 20) + "</a></p><p>规格：" + entity.Product_Spec + "");
                            //strHTML.Append("         </p><p>" + entity.Product_Spec + "</p>");
                            strHTML.Append("             </li>");



                            strHTML.Append("</li>");
                        }
                    }
                }
            }
        }
        strHTML.Append("</ul>");

        return strHTML.ToString();
    }


    //获取子类别
    public IList<CategoryInfo> Product_GetFrequently_Cate(int Cate_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + Cate_ID + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsFrequently", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        return Categorys;
    }

    //获取子类别
    public IList<CategoryInfo> Product_GetFirstSub_Cate(int Cate_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + Cate_ID + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        return Categorys;
    }

    //商品频道各类商品
    public string Product_Cate_Product(int Show_Num, int Cate_ID)
    {
        string product_list = "";
        int i = 0;
        IList<ProductInfo> Products = GetCateTagProduct(Show_Num, Cate_ID, "首页栏目显示");
        if (Products != null)
        {
            foreach (ProductInfo entity in Products)
            {
                i++;
                if (i >= Show_Num)
                {
                    product_list = product_list + "<li style=\"margin:0;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\"  onload=\"javascript:AutosizeImage(this,100,100);\" width=\"100\" height=\"100\" alt=\"" + entity.Product_Name + "\" /></a>";
                    product_list = product_list + "<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 24) + "</a></p>";
                    product_list = product_list + "<p class=\"deep\"><span>" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></p></li>";
                }
                else
                {
                    product_list = product_list + "<li><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\"  onload=\"javascript:AutosizeImage(this,100,100);\" width=\"100\" height=\"100\" alt=\"" + entity.Product_Name + "\" /></a>";
                    product_list = product_list + "<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 24) + "</a></p>";
                    product_list = product_list + "<p class=\"deep\"><span>" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></p></li>";
                }
            }
            product_list = product_list + "</ul>";
        }
        return product_list;
    }

    //获取顶级类别编号
    public int Get_First_CateID(int Cate_ID)
    {
        int First_ID = 0;
        CategoryInfo category = MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (category != null)
        {
            if (category.Cate_ParentID == 0)
            {
                First_ID = category.Cate_ID;
            }
            else
            {
                First_ID = Get_First_CateID(category.Cate_ParentID);
            }
        }
        return First_ID;
    }

    //获取类别导航
    public string Get_Cate_Nav(int Cate_ID, string gap_char)
    {
        string cate_nav = "";
        IList<CategoryInfo> entitys = null;
        CategoryInfo category = MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (category != null)
        {
            entitys = Product_GetFirstSub_Cate(Cate_ID);
            if (entitys != null && entitys.Count > 0)
            {
                cate_nav = cate_nav + "<a href=\"/product/category.aspx?cate_id=" + category.Cate_ID + "\">" + category.Cate_Name + "</a>";
            }
            else
            {
                cate_nav = cate_nav + "" + category.Cate_Name + "";
            }
            cate_nav = Get_Cate_Nav(category.Cate_ParentID, gap_char) + gap_char + cate_nav;
        }

        return cate_nav;
    }

    public string Get_Cate_DetailNav(int Cate_ID, string gap_char)
    {
        string cate_nav = "";
        CategoryInfo category = MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (category != null)
        {
            cate_nav = cate_nav + "<a href=\"/product/category.aspx?cate_id=" + category.Cate_ID + "\">" + category.Cate_Name + "</a>";
            cate_nav = Get_Cate_DetailNav(category.Cate_ParentID, gap_char) + gap_char + cate_nav;
        }
        return cate_nav;
    }

    #region 列表页搜索

    /// <summary>
    /// 商品筛选条件
    /// </summary>
    /// <param name="cate_id"></param>
    /// <param name="cate_parentid"></param>
    /// <param name="cate_typeid"></param>

    // 商品筛选条件  
    public void Product_Filter(int cate_id, int cate_parentid, int cate_typeid)
    {
        string Prouduct_State_Array = "0";

        int catefatherid = cate_id;
        string Cate_Arry = Get_First_SubCate(cate_id);


        string keyword, isgallerylist, orderby, Extend_Value;

        double price_min, price_max;
        int price_num;
        string prdouct_label;
        int isgroup, ispromotion, isRecommend, isgift;










        string Brand_ID, Supplier_ID, region_id;

        int i = 0;

        IList<ProductTypeExtendInfo> extends = null;
        IList<BrandInfo> brands = null;
        IList<SupplierInfo> suppliers = null;
        Brand_ID = tools.CheckStr(Request["brand_id"]);
        Supplier_ID = tools.CheckStr(Request["supplier_id"]);
        region_id = tools.CheckStr(Request["region_id"]);


        Response.Write(" <div class=\"parte\">");

        //  显示分类
        Response.Write("<div class=\"screen_info\" style=\"height: inherit;\" >");
        Response.Write("<strong  id=\"am1\">分类：</strong>");
        Response.Write("<dl style=\"height: inherit;\">");
        Response.Write(Product_Filter_Cate_list(cate_id, Cate_Arry, cate_parentid));
        Response.Write("<div class=\"clear\"></div>");
        Response.Write("</dl>");
        Response.Write("<div class=\"clear\"></div>");
        Response.Write("</div>");

        ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
        if (producttype != null)
        {
            brands = producttype.BrandInfos;
            suppliers = producttype.SupplierInfos;
            extends = producttype.ProductTypeExtendInfos;
        }
        else
        {
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_IsActive", "=", "1"));
            Query1.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "asc"));
            brands = MyBrand.GetBrands(Query1, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));



            QueryInfo Query2 = new QueryInfo();
            Query2.PageSize = 0;
            Query2.CurrentPage = 1;
            Query2.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
            Query2.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_IsHaveShop", "=", "1"));
            Query2.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_AuditStatus", "=", "1"));
            Query2.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Status", "=", "1"));
            Query2.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
            suppliers = Mysupplier.GetSuppliers(Query2, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));

        }

        //if (brands != null)
        //{
        //    //品牌
        //    Response.Write("<div class=\"screen_info\">");
        //    Response.Write("<strong  id=\"am1\">品牌：</strong>");
        //    Response.Write("<dl>");
        //    Response.Write(Product_Filter_Brand_list(brands, cate_id, Brand_ID));

        //    Response.Write("<div class=\"clear\"></div>");
        //    Response.Write("</dl>");
        //    Response.Write("<div class=\"clear\"></div>");
        //    Response.Write("</div>");
        //}

        //if (suppliers != null)
        //{
        //    //商家
        //    Response.Write("<div class=\"screen_info\">");
        //    Response.Write("<strong  id=\"am1\">商家：</strong>");
        //    Response.Write("<dl>");
        //Response.Write(Product_Filter_Supplier_list(suppliers, cate_id, Supplier_ID.ToString()));

        //    Response.Write("<div class=\"clear\"></div>");
        //    Response.Write("</dl>");
        //    Response.Write("<div class=\"clear\"></div>");
        //    Response.Write("</div>");
        //}

        //  显示地区
        //Response.Write("<div class=\"screen_info\">");
        //Response.Write("<strong  id=\"am1\">产地：</strong>");
        //Response.Write("<dl>");       
        //Response.Write(Product_Filter_Region_list(Prouduct_State_Array, cate_id, region_id.ToString()));      
        //Response.Write("<div class=\"clear\"></div>");
        //Response.Write("</dl>");
        //Response.Write("<div class=\"clear\"></div>");
        //Response.Write("</div>");




        // 扩展属性
        bool extendflag = false;

        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1 && entity.ProductType_Extend_Default != "")
                {
                    Extend_Value = tools.CheckStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                    if (Extend_Value.Length > 0)
                    {
                        extendflag = true;
                        break;
                    }
                }
            }
            if (extendflag)
            {
                Response.Write("<div id=\"cate_extend\">");
            }
            else
            {
                Response.Write("<div id=\"cate_extend\" style=\"display:none;\">");
            }
            extendflag = false;
            //  扩展属性
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                {
                    extendflag = true;
                    Response.Write(Product_Filter_Extend_list(entity));
                }
            }
            Response.Write("</div>");
        }


        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1 && entity.ProductType_Extend_Default != "")
                {
                    Extend_Value = tools.CheckStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                    if (Extend_Value.Length > 0)
                    {
                        extendflag = false;
                    }
                }
            }
        }

        Response.Write("</div>");
        if (extendflag)
        {
            Response.Write("<div class=\"parte_a\"><a href=\"javascript:void(0)\" onclick=\"$('#cate_extend').show();$('#b_unfold_extend').hide();$('#b_fold_extend').show();\" id=\"b_unfold_extend\">更 多</a><a href=\"javascript:void(0)\"  onclick=\"$('#cate_extend').hide();$('#b_unfold_extend').show();$('#b_fold_extend').hide();\" id=\"b_fold_extend\" style=\"display:none;\">收 起</a></div>");
        }
    }

    public void Product_Filter_Search_Cod(int cate_id)
    {
        string keyword, isgallerylist, orderby;
        double price_min, price_max;
        int isgroup, ispromotion, isRecommend, isgift, Brand_ID;
        Brand_ID = tools.CheckInt(Request["brand_id"]);

        keyword = tools.CheckStr(Request["keyword"]);
        isgallerylist = tools.CheckStr(Request["isgallerylist"]);
        orderby = tools.CheckStr(Request["orderby"]);
        price_min = tools.CheckFloat(Request["price_min"]);
        price_max = tools.CheckFloat(Request["price_max"]);
        isgroup = tools.CheckInt(Request["isgroup"]);
        ispromotion = tools.CheckInt(Request["ispromotion"]);
        isRecommend = tools.CheckInt(Request["isRecommend"]);
        isgift = tools.CheckInt(Request["isgift"]);
        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"/product/search.htm\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id\" value=\"" + cate_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"keyword\" id=\"keyword\" value=\"" + keyword + "\">");
        Response.Write("<input type=\"hidden\" name=\"isgallerylist\" id=\"isgallerylist\" value=\"" + isgallerylist + "\">");
        Response.Write("<input type=\"hidden\" name=\"orderby\" id=\"orderby\" value=\"" + orderby + "\">");
        Response.Write("</form>");
        Response.Write("</div>");
    }


    /// <summary>
    /// 筛选类别选择
    /// </summary>
    /// <param name="cate_id"></param>
    /// <param name="Cate_Arry"></param>
    /// <param name="cate_parentid"></param>
    /// <returns></returns>
    public string Product_Filter_Cate_list(int cate_id, string Cate_Arry, int cate_parentid)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dt>");
        int cate_parentids = 0;

        int id = tools.CheckInt(Request["cate_id"]);
        if (Cate_Arry == cate_id.ToString())
        {
            strHTML.Append("<span><a href=\"javascript:filter_setvalue('cate_id'," + cate_parentid + ");\" ");
        }
        else
        {
            strHTML.Append("<span><a href=\"javascript:filter_setvalue('cate_id'," + cate_id + ");\"");
        }

        if (Cate_Arry == cate_id.ToString())
        {
        }
        else
        {
            strHTML.Append(" class=\"a_on\"");
        }
        strHTML.Append(">全部</a></span></dt>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Cate_Arry == cate_id.ToString())
        {
            Cate_Arry = Get_First_SubCate(cate_parentid);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_parentid.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Cate_Arry + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            strHTML.Append("<dd class=\"dd01\" id=\"more_cate\">");
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:filter_setvalue('cate_id'," + entity.Cate_ID + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"a_on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
                cate_parentids = entity.Cate_ParentID;
            }
            strHTML.Append("</dd>");
            //暂时去掉
            //if (Categorys.Count > 8)
            //{
            //    strHTML.Append("<dd class=\"dd02\"><a href=\"javascript:void(0)\" onclick=\"$('#more_cate').css('height','auto');$('#b_fold_cate').show();$('#b_unfold_cate').hide();\" id=\"b_unfold_cate\">更多 +</a><a href=\"javascript:void(0)\"  onclick=\"$('#more_cate').css('height','30px');$('#b_unfold_cate').show();$('#b_fold_cate').hide();\" id=\"b_fold_cate\" style=\"display:none;\">收起 -</a></dd>");
            //}
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 计算字符串中子串出现的次数
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="substring">子串</param>
    /// <returns>出现的次数</returns>
    public static int SubstringCount(string str, string substring)
    {
        if (str.Contains(substring))
        {
            string strReplaced = str.Replace(substring, "");
            return (str.Length - strReplaced.Length) / substring.Length;
        }

        return 0;
    }


    /// <summary>
    /// 筛选类别选择
    /// </summary>
    /// <param name="cate_id"></param>
    /// <param name="Cate_Arry"></param>
    /// <param name="cate_parentid"></param>
    /// <returns></returns>
    //public string Product_Filter_Region_list(int region_id, string Region_Arry, int cate_parentid)
    //{
    //    StringBuilder strHTML = new StringBuilder();

    //    strHTML.Append("<dt>");
    //    int cate_parentids = 0;

    //    int id = tools.CheckInt(Request["region_id"]);
    //    if (Region_Arry == region_id.ToString())
    //    {
    //        strHTML.Append("<span><a href=\"javascript:filter_setvalue('region_id'," + cate_parentid + ");\" ");
    //    }
    //    else
    //    {
    //        strHTML.Append("<span><a href=\"javascript:filter_setvalue('region_id'," + region_id + ");\"");
    //    }

    //    if (Region_Arry == region_id.ToString())
    //    {
    //    }
    //    else
    //    {
    //        strHTML.Append(" class=\"a_on\"");
    //    }
    //    strHTML.Append(">全部</a></span></dt>");

    //    QueryInfo Query = new QueryInfo();
    //    Query.PageSize = 0;
    //    Query.CurrentPage = 1;
    //    if (Region_Arry == region_id.ToString())
    //    {
    //        Region_Arry = Get_First_SubCate(cate_parentid);
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_parentid.ToString()));
    //    }
    //    else
    //    {
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", region_id.ToString()));
    //    }
    //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Region_Arry + ""));
    //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
    //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
    //    Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
    //    IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
    //    if (Categorys != null)
    //    {
    //        strHTML.Append("<dd class=\"dd01\" id=\"more_cate\">");
    //        foreach (CategoryInfo entity in Categorys)
    //        {
    //            strHTML.Append("<a href=\"javascript:filter_setvalue('cate_id'," + entity.Cate_ID + ");\"");

    //            if (id == entity.Cate_ID)
    //            {
    //                strHTML.Append(" class=\"a_on\" ");
    //            }
    //            //strHTML.Append(">" + entity.Cate_Name + "</a> ");
    //            strHTML.Append(">" + entity.Cate_Name + "</a> ");
    //            cate_parentids = entity.Cate_ParentID;
    //        }
    //        strHTML.Append("</dd>");
    //        if (Categorys.Count > 8)
    //        {
    //            strHTML.Append("<dd class=\"dd02\"><a href=\"javascript:void(0)\" onclick=\"$('#more_cate').css('height','auto');$('#b_fold_cate').show();$('#b_unfold_cate').hide();\" id=\"b_unfold_cate\">更多 +</a><a href=\"javascript:void(0)\"  onclick=\"$('#more_cate').css('height','30px');$('#b_unfold_cate').show();$('#b_fold_cate').hide();\" id=\"b_fold_cate\" style=\"display:none;\">收起 -</a></dd>");
    //        }
    //    }

    //    return strHTML.ToString();
    //}

    public string Product_Filter_Region_list(string Prouduct_State_Array, int Cate_ID, string Region_ID)
    {
        string Product_State_Name = "";
        string supplier_list = "";
        supplier_list = supplier_list + "<dt>";
        if (Region_ID == "0")
        {
            Region_ID = "";
        }
        if (Region_ID.Length > 0)
        {
            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('region_id','');\" class=\"\"";
        }
        else
        {
            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('region_id','');\" class=\"a_on\"";
        }

        supplier_list = supplier_list + ">不限</a>";
        supplier_list = supplier_list + "</dt>";
        if (Region_ID.Length > 0)
        {
            supplier_list = supplier_list + "<dd id=\"more_region\" class=\"dd01\" style=\"height:auto\">";
        }
        else
        {
            supplier_list = supplier_list + "<dd id=\"more_region\" class=\"dd01\">";
        }
        bool iscontion = false;
        string idarry = "0";
        int i = 0;
        foreach (string sid in Region_ID.Split(','))
        {
            if (tools.CheckInt(sid) > 0)
            {
                i++;
                idarry += "," + tools.CheckInt(sid);
            }
        }
        Region_ID = idarry;
        idarry = "0";


        QueryInfo RegionProductQuery = new QueryInfo();
        RegionProductQuery.PageSize = 0;
        RegionProductQuery.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));

        //聚合是否列表显示 暂时屏蔽掉

        //  RegionProductQuery.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_GroupCode", "=", ""));

        //RegionProductQuery.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_GroupCode", "=", ""));
        //RegionProductQuery.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        RegionProductQuery.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        RegionProductQuery.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        RegionProductQuery.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        IList<ProductInfo> products = Myproduct.GetProductList(RegionProductQuery, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        string productArray = "0";
        if (products != null)
        {
            iscontion = false;
            idarry = "0";

            string Product_StateName = "";
            foreach (ProductInfo entity in products)
            {
                int Count = 5;
                StateInfo StateInfo = MyAddr.GetStateInfoByCode(entity.Product_State_Name);
                if (StateInfo != null)
                {
                    Product_StateName = StateInfo.State_CN;
                    productArray += "," + Product_StateName;
                }

                iscontion = false;
                if (Product_StateName.Length > 0)
                {
                    Count = SubstringCount(productArray, Product_StateName);
                }

                if (Count < 2)
                {
                    if ((entity.Product_State_Name.Length > 0))
                    {
                        foreach (string sid in Region_ID.Split(','))
                        {
                            if (tools.CheckStr(sid) == entity.Product_State_Name)
                            {
                                iscontion = true;
                                break;
                            }
                        }



                        if (iscontion)
                        {
                            foreach (string sid in Region_ID.Split(','))
                            {
                                if (sid.Length > 0 && tools.CheckStr(sid) != entity.Product_State_Name && sid != "0")
                                {
                                    if (!idarry.Contains(sid))
                                    {
                                        idarry += "," + tools.CheckInt(sid);
                                    }

                                }
                            }
                            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('region_id','" + idarry + "');\" class=\"";
                            supplier_list = supplier_list + "a_on";
                        }
                        else
                        {
                            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('region_id','" + Region_ID + "," + entity.Product_State_Name + "');\" class=\"";
                            supplier_list = supplier_list + "";
                        }
                        supplier_list = supplier_list + "\">" + Product_StateName + "</a>";
                    }
                }




            }

        }
        supplier_list = supplier_list + "</dd>";


        if (Product_State_Name == "0" || Product_State_Name.Length < 1)
        {
            Product_State_Name = "";
        }
        //暂时去掉
        //if (products != null)
        //{
        //    if (i > 3)
        //    {
        //        if (Product_State_Name == "")
        //        {
        //            supplier_list += "<dd class=\"dd02\"><a href=\"javascript:void(0);\"  onclick=\"$('#more_region').css('height','auto');$('#b_fold_region').show();$('#b_unfold_region').hide();\" id=\"b_unfold_region\">更多 +</a><a href=\"javascript:void(0);\" onclick=\"$('#more_region').css('height','30px');$('#b_unfold_region').show();$('#b_fold_region').hide();\" id=\"b_fold_region\" style=\"display:none;\">收起 -</a></dd>";
        //        }

        //    }
        //}






        return supplier_list;
    }

    public string Product_Filter_Cate_Display(int cate_id, int cate_typeid)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ProductTypeExtendInfo> extends = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
        IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);
        if (entityList == null)
            return string.Empty;


        int Brand_ID = tools.CheckInt(Request["brand_id"]);
        string ExtendType_Value = "";

        strHTML.Append("<h2 class=\"classify\">");

        strHTML.Append(Product_Filter_Cate_Show(cate_id));

        if (Brand_ID > 0)
        {
            BrandInfo entityBrand = MyBrand.GetBrandByID(Brand_ID, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
            if (entityBrand != null)
            {
                strHTML.Append(" <div class=\"span_h3\"><i><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('brand_id',0);\">X</a></i>" + entityBrand.Brand_Name + "</div>");
            }
        }


        if (cate_typeid > 0)
        {
            ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (producttype != null)
            {
                extends = producttype.ProductTypeExtendInfos;
                if (extends != null)
                {
                    foreach (ProductTypeExtendInfo entity in extends)
                    {
                        if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                        {
                            ExtendType_Value = tools.NullStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                            if (ExtendType_Value != "")
                            {
                                strHTML.Append(" <div class=\"span_h3\"><i><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('filter_" + entity.ProductType_Extend_ID + "','');\">X</a></i>" + ExtendType_Value + "</div>");
                            }
                        }
                    }
                }
            }
        }

        strHTML.Append("</h2>");
        return strHTML.ToString();
    }

    public string Product_Filter_Cate_Show(int cate_id)
    {
        string strHTML = "";

        CategoryInfo cateInfo = null;

        cateInfo = GetCategoryByID(cate_id);
        if (cateInfo != null)
        {
            if (cateInfo.Cate_ParentID == 0)
            {
                strHTML = strHTML + "<strong><a>" + cateInfo.Cate_Name + "</a></strong> > ";
            }
            else
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", cateInfo.Cate_ParentID.ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", pub.GetCurrentSite()));
                Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
                RBACUserInfo userInfo = pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb");
                IList<CategoryInfo> entityList = MyCate.GetCategorys(Query, userInfo);

                strHTML = strHTML + " <div class=\"span_h2\">";
                strHTML = strHTML + "<span>" + cateInfo.Cate_Name + "<i></i></span>";
                if (entityList != null)
                {
                    strHTML = strHTML + "<div class=\"span_box\">";
                    foreach (CategoryInfo entity in entityList)
                    {
                        string cateids = Get_First_SubCate(entity.Cate_ID);
                        if (entity.Cate_ParentID > 0 && Get_First_SubCate(entity.Cate_ID) != entity.Cate_ID.ToString())
                        {
                            strHTML = strHTML + "<a>" + entity.Cate_Name + "</a>";
                        }
                        else
                        {
                            strHTML = strHTML + "<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity.Cate_ID.ToString(), "", "", "", "")) + "\">" + entity.Cate_Name + "</a>";
                        }
                    }
                    strHTML = strHTML + "</div> > ";
                }
                strHTML = strHTML + "</div>";
                strHTML = Product_Filter_Cate_Show(cateInfo.Cate_ParentID) + strHTML;
            }
        }
        return strHTML;
    }

    /// <summary>
    /// 筛选品牌选择
    /// </summary>
    /// <param name="brands"></param>
    /// <param name="Cate_ID"></param>
    /// <param name="Brand_ID"></param>
    /// <returns></returns>
    //筛选品牌选择
    public string Product_Filter_Brand_list(IList<BrandInfo> brands, int Cate_ID, string Brand_ID)
    {
        string brand_list = "";
        brand_list = brand_list + "<dt>";
        if (Brand_ID == "0")
        {
            Brand_ID = "";
        }
        if (Brand_ID.Length > 0)
        {
            brand_list = brand_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id','');\" class=\"\"";
        }
        else
        {
            brand_list = brand_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id','');\" class=\"a_on\"";
        }

        brand_list = brand_list + ">不限</a>";
        brand_list = brand_list + "</dt>";
        if (Brand_ID.Length > 0)
        {
            brand_list = brand_list + "<dd id=\"more_brand\" class=\"dd01\" style=\"height:auto\">";
        }
        else
        {
            brand_list = brand_list + "<dd id=\"more_brand\" class=\"dd01\">";
        }
        bool iscontion = false;
        string idarry = "0";
        foreach (string sid in Brand_ID.Split(','))
        {
            if (tools.CheckInt(sid) > 0)
            {
                idarry += "," + tools.CheckInt(sid);
            }
        }
        int i = 0;
        Brand_ID = idarry;
        idarry = "0";
        foreach (BrandInfo entity in brands)
        {
            iscontion = false;
            idarry = "0";
            if (entity.Brand_IsActive == 1)
            {
                foreach (string sid in Brand_ID.Split(','))
                {
                    if (tools.CheckInt(sid) == entity.Brand_ID)
                    {
                        iscontion = true;
                        break;
                    }
                }
                //brand_list = brand_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id'," + entity.Brand_ID + ");\" class=\"";

                i++;
                if (iscontion)
                {
                    foreach (string sid in Brand_ID.Split(','))
                    {
                        if (tools.CheckInt(sid) > 0 && tools.CheckInt(sid) != entity.Brand_ID)
                        {
                            idarry += "," + tools.CheckInt(sid);
                        }
                    }
                    brand_list = brand_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id','" + idarry + "');\" class=\"";
                    brand_list = brand_list + "a_on";
                }
                else
                {
                    brand_list = brand_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id','" + Brand_ID + "," + entity.Brand_ID + "');\" class=\"";
                    brand_list = brand_list + "";
                }
                brand_list = brand_list + "\">" + entity.Brand_Name + "</a>";
            }
        }
        brand_list = brand_list + "</dd>";
        if (Brand_ID == "0")
        {
            Brand_ID = "";
        }
        //暂时去掉
        //if (i > 8 && Brand_ID == "")
        //{

        //    brand_list += "<dd class=\"dd02\"><a href=\"javascript:void(0)\" onclick=\"$('#more_brand').css('height','auto');$('#b_fold_brand').show();$('#b_unfold_brand').hide();\" id=\"b_unfold_brand\">更多 +</a><a href=\"javascript:void(0);\"onclick= \"$('#more_brand').css('height','30px');$('#b_unfold_brand').show();$('#b_fold_brand').hide();\" id=\"b_fold_brand\" style=\"display:none;\">收起 -</a></dd>";

        //}


        return brand_list;
    }


    //筛选商家选择
    public string Product_Filter_Supplier_list(IList<SupplierInfo> suppliers, int Cate_ID, string Supplier_ID)
    {
        string supplier_list = "";
        supplier_list = supplier_list + "<dt>";
        if (Supplier_ID == "0")
        {
            Supplier_ID = "";
        }
        if (Supplier_ID.Length > 0)
        {
            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('supplier_id','');\" class=\"\"";
        }
        else
        {
            supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('supplier_id','');\" class=\"a_on\"";
        }

        supplier_list = supplier_list + ">不限</a>";
        supplier_list = supplier_list + "</dt>";
        if (Supplier_ID.Length > 0)
        {
            supplier_list = supplier_list + "<dd id=\"more_supplier\" class=\"dd01\" style=\"height:auto\">";
        }
        else
        {
            supplier_list = supplier_list + "<dd id=\"more_supplier\" class=\"dd01\">";
        }
        bool iscontion = false;
        string idarry = "0";
        foreach (string sid in Supplier_ID.Split(','))
        {
            if (tools.CheckInt(sid) > 0)
            {
                idarry += "," + tools.CheckInt(sid);
            }
        }
        Supplier_ID = idarry;
        idarry = "0";
        foreach (SupplierInfo entity in suppliers)
        {
            iscontion = false;
            idarry = "0";
            if ((entity.Supplier_IsHaveShop == 1) && (entity.Supplier_Status == 1) && (entity.Supplier_AuditStatus == 1))
            {
                foreach (string sid in Supplier_ID.Split(','))
                {
                    if (tools.CheckInt(sid) == entity.Supplier_ID)
                    {
                        iscontion = true;
                        break;
                    }
                }



                if (iscontion)
                {
                    foreach (string sid in Supplier_ID.Split(','))
                    {
                        if (tools.CheckInt(sid) > 0 && tools.CheckInt(sid) != entity.Supplier_ID)
                        {
                            idarry += "," + tools.CheckInt(sid);
                        }
                    }
                    supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('supplier_id','" + idarry + "');\" class=\"";
                    supplier_list = supplier_list + "a_on";
                }
                else
                {
                    supplier_list = supplier_list + "<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('supplier_id','" + Supplier_ID + "," + entity.Supplier_ID + "');\" class=\"";
                    supplier_list = supplier_list + "";
                }
                supplier_list = supplier_list + "\">" + entity.Supplier_CompanyName + "</a>";
            }
        }
        supplier_list = supplier_list + "</dd>";
        if (Supplier_ID == "0")
        {
            Supplier_ID = "";
        }
        //暂时去掉
        //if (suppliers != null)
        //{
        //    if (suppliers.Count > 3)
        //    {
        //        if (Supplier_ID == "")
        //        {
        //            supplier_list += "<dd class=\"dd02\"><a href=\"javascript:$('#more_supplier').css('height','auto');$('#b_fold_supplier').show();$('#b_unfold_supplier').hide();\" id=\"b_unfold_supplier\">更多 +</a><a href=\"javascript:$('#more_supplier').css('height','30px');$('#b_unfold_supplier').show();$('#b_fold_supplier').hide();\" id=\"b_fold_supplier\" style=\"display:none;\">收起 -</a></dd>";
        //        }

        //    }
        //}



        return supplier_list;
    }

    /// <summary>
    /// 筛选扩展属性选择
    /// </summary>
    /// <param name="extend"></param>
    /// <returns></returns>
    public string Product_Filter_Extend_list(ProductTypeExtendInfo extend)
    {
        string Extend_Value = tools.NullStr(Request["filter_" + extend.ProductType_Extend_ID]).Trim();
        string default_value = extend.ProductType_Extend_Default;

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"screen_info\">");
        //strHTML.Append("<dl>");
        strHTML.Append("<strong id=\"am1\">" + extend.ProductType_Extend_Name + "：</strong><dl>");
        strHTML.Append("<dt><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','');\"");

        if (Extend_Value != "")
        {

        }
        else
        {
            strHTML.Append(" class=\"a_on\"");
        }
        strHTML.Append(">不限</a></dt>");
        strHTML.Append("<dd class=\"dd01\">");
        if (default_value.Length > 0)
        {
            foreach (string extend_value in default_value.Split('|'))
            {
                if (extend_value != "")
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','" + extend_value + "');\"");
                    if (Extend_Value == extend_value)
                    {
                        strHTML.Append(" class=\"a_on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + extend_value + "</a>");
                }
            }
        }
        else
        {
            string Exit_Extend = "||";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ExtendID", "=", extend.ProductType_Extend_ID.ToString()));
            IList<ProductExtendInfo> entitys = Myproduct.GetProductExtends(Query);
            if (entitys != null)
            {
                foreach (ProductExtendInfo Pre_Extend in entitys)
                {
                    //检查属性重复性
                    if (Exit_Extend.IndexOf("|" + Pre_Extend.Extend_Value + "|") < 0)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','" + Pre_Extend.Extend_Value + "');\"");
                        if (Extend_Value == Pre_Extend.Extend_Value)
                        {
                            strHTML.Append(" class=\"a_on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + Pre_Extend.Extend_Value + "</a> ");

                        Exit_Extend += Pre_Extend.Extend_Value + "|";
                    }
                }
            }
        }

        strHTML.Append("</dd>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</div>");
        return strHTML.ToString();
    }
    /// <summary>
    /// 商品排序方式及展示方式栏
    /// </summary>
    public string Product_View_Mode_bak()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string stateCode = tools.CheckStr(Request["stateCode"]);
        string orderby = tools.CheckStr(Request["orderby"]);
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();

        orderby = orderby.Replace("\"", "&quot;").ToString();


        int isgallerylist;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        StringBuilder sb = new StringBuilder();

        sb.Append("<ul>");
        //sb.Append("<li class=\"one\">所有地区</li>");
        //sb.Append("<li >");

        //sb.Append("<div  id=\"sel_address\">");


        //IList<StateInfo> stateList = null;

        //stateList = MyAddr.GetStatesByCountry("1");


        ////选择省
        //if (stateList != null)
        //{
        //    sb.Append("<select class=\"one\" name=\"s_111\" id=\"s_111\" onchange=\"javascript:filter_setvalue('stateCode',this.value);\" >");
        //    sb.Append("<option value=\"\">----选择省----</option>");
        //    foreach (StateInfo entity in stateList)
        //    {
        //        sb.Append("<option value=\"" + entity.State_Code + "\"");
        //        if (entity.State_Code == stateCode) { sb.Append(" selected=\"selected\""); }
        //        sb.Append(">" + entity.State_CN + "</option>");
        //    }
        //    sb.Append("</select>");
        //}

        //sb.Append(myaddr.SelectAddressDelivery("sel_address", "sel_state", "sel_city", "sel_area", "", "", ""));

        //sb.Append("</div>");
        //sb.Append("</li>");




        sb.Append("<li><label>销量</label>");

        sb.Append("<a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','hotsale_down');\"  class=\"a7 " + (orderby == "hotsale_down" ? "a7on" : "") + "\"  ></a>");
        //sb.Append("<a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\" class=\"a8\"></a>");
        sb.Append("</li>");
        sb.Append("<li><label>价格</label>");
        sb.Append("<a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"  class=\"a7 " + (orderby == "price_down" ? "a7on" : "") + "\" ></a>");
        sb.Append("<a  href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','price_up');\"  class=\"a8 " + (orderby == "price_up" ? "a8on" : "") + "\" ></a>");

        sb.Append("</li>");
        sb.Append("<li><label>上架时间</label>");
        sb.Append("<a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','time_down');\"  class=\"a7 " + (orderby == "time_down" ? "a7on" : "") + "\" ></a>");
        sb.Append("<a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','time_up');\"  class=\"a8 " + (orderby == "time_up" ? "a8on" : "") + "\" ></a>");
        sb.Append("</li>");
        sb.Append("<li>");

        sb.Append("<input name=\"price_min_txt\" id=\"price_min_txt\" type=\"text\" value=\"" + price_min + "\" />");

        sb.Append("-");
        sb.Append("<input name=\"price_max_txt\" id=\"price_max_txt\" type=\"text\" value=\"" + price_max + "\" /> ");


        sb.Append("<input style=\"width:36px;height:20px;\" type=\"image\" src=\"../images/buttom_bg.jpg\" onclick=\"javascript:filter_setvalue('price_min',price_min_txt.value);javascript:filter_setvalue('price_max',price_max_txt.value);\" /></li>");

        sb.Append("</ul>");

        return sb.ToString();

    }




    /// <summary>
    /// 商品排序方式及展示方式栏
    /// </summary> 

    public string Product_View_Mode()
    {
        string mode_str = "";
        //排序方式
        string orderby;
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);



        int isgallerylist;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0 && isgallerylist != 2)
        {
            isgallerylist = 1;
        }
        mode_str += "<ul class=\"list01\">";
        mode_str += "<li class=\"on\" id=\"am\" ><a href=\"javascript:void(0);\"  onclick=\"javascript:filter_setvalue('orderby','');\">默认</a></li>";
        if (orderby == "")
        {
            //mode_str += "<li class=\"on\" id=\"am\" ><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','');\">综合排序</a></li>";

        }
        else
        {
            //mode_str += "<li id=\"am\" ><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','');\">综合排序</a></li>";
        }

        if (orderby == "price_down")
        {
            mode_str += "<li class=\"on\" id=\"am\" ><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','price_up');\">价格<img src=\"/images/icon_20.jpg\" /></a></li>";
        }
        else if (orderby == "price_up")
        {
            mode_str += "<li class=\"on\" id=\"am\" ><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','price_down');\">价格<img src=\"/images/icon_21.jpg\" /></a></li>";
        }
        else
        {
            mode_str += "<li id=\"am\" ><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','price_down');\">价格<img src=\"/images/icon25.jpg\" /></a></li>";
        }

        if (orderby == "hotsale_down")
        {
            mode_str += "<li class=\"on\" id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\">销量<img src=\"/images/icon_20.jpg\" /></a></li>";
        }
        else if (orderby == "hotsale_up")
        {
            mode_str += "<li class=\"on\" id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','hotsale_down');\">销量<img src=\"/images/icon_21.jpg\" /></a></li>";
        }
        else
        {
            mode_str += "<li id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','hotsale_down');\">销量<img src=\"/images/icon25.jpg\" /></a></li>";
        }

        if (orderby == "time_down")
        {
            mode_str += "<li class=\"on\" id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','time_up');\">上架时间<img src=\"/images/icon_20.jpg\" /></a></li>";
        }
        else if (orderby == "time_up")
        {
            mode_str += "<li class=\"on\" id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','time_down');\">上架时间<img src=\"/images/icon_21.jpg\" /></a></li>";
        }
        else
        {
            mode_str += "<li  id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','time_down');\">上架时间<img src=\"/images/icon25.jpg\" /></a></li>";
        }

        if (orderby == "star_down")
        {
            mode_str += "<li class=\"on\"   id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','star_up');\">好评度<img src=\"/images/icon_20.jpg\" /></a></li>";
        }
        else if (orderby == "star_up")
        {
            mode_str += "<li class=\"on\"  id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','star_down');\">好评度<img src=\"/images/icon_21.jpg\" /></a></li>";
        }
        else
        {
            mode_str += "<li  id=\"am\"><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','star_down');\">好评度<img src=\"/images/icon25.jpg\" /></a></li>";
        }
        mode_str += "</ul>";

        mode_str += "<ul class=\"list02\">";
        mode_str += "    <li>展示方式 :</li>";
        if (isgallerylist == 0)
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" class=\"a09_1\"></a></li>";
        }
        else
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('isgallerylist','0');\" class=\"a09\"></a></li>";
        }
        if (isgallerylist == 1)
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" class=\"a10_1\"></a></li>";
        }
        else
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('isgallerylist','1');\" class=\"a10\"></a></li>";
        }
        if (isgallerylist == 2)
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" class=\"a11_1\"></a></li>";
        }
        else
        {
            mode_str += "    <li pa><a href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('isgallerylist','2');\" class=\"a11\"></a></li>";
        }
        mode_str += "</ul>";

        int cate_id = tools.CheckInt(Request["cate_id"]);
        string keyword = pub.CheckXSS(tools.CheckStr(Request["keyword"]));
        orderby = pub.CheckXSS(tools.CheckStr(Request["orderby"]));
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);
        int isgroup = tools.CheckInt(Request["isgroup"]);
        int ispromotion = tools.CheckInt(Request["ispromotion"]);
        int isRecommend = tools.CheckInt(Request["isRecommend"]);
        int isgift = tools.CheckInt(Request["isgift"]);
        string Brand_ID = pub.CheckXSS(tools.CheckStr(Request["Brand_ID"]));
        string Supplier_ID = pub.CheckXSS(tools.CheckStr(Request["Supplier_ID"]));
        string Region_ID = pub.CheckXSS(tools.CheckStr(Request["Region_ID"]));
        int cate_typeid = 0;
        IList<ProductTypeExtendInfo> extends = null;
        if (cate_id > 0)
        {
            CategoryInfo cateinfo = GetCategoryByID(cate_id);
            if (cateinfo != null)
            {
                if (cateinfo.Cate_IsActive == 1)
                {

                    cate_typeid = cateinfo.Cate_TypeID;
                }
            }
            ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (producttype != null)
            {
                extends = producttype.ProductTypeExtendInfos;
            }
        }
        string Extend_Value = "";
        mode_str += "<div style=\"display:none\">";
        mode_str += "<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"?\">";
        mode_str += "<input type=\"hidden\" name=\"cate_id\" id=\"cate_id\" value=\"" + cate_id + "\">";
        mode_str += "<input type=\"hidden\" name=\"supplier_id\" id=\"supplier_id\" value=\"" + Supplier_ID + "\">";
        mode_str += "<input type=\"hidden\" name=\"region_id\" id=\"region_id\" value=\"" + Region_ID + "\">";
        mode_str += "<input type=\"hidden\" name=\"brand_id\" id=\"brand_id\" value=\"" + Brand_ID + "\">";
        mode_str += "<input type=\"hidden\" name=\"keyword\" id=\"keyword\" value=\"" + keyword + "\">";
        mode_str += "<input type=\"hidden\" name=\"isgallerylist\" id=\"isgallerylist\" value=\"" + isgallerylist + "\">";
        mode_str += "<input type=\"hidden\" name=\"orderby\" id=\"orderby\" value=\"" + orderby + "\">";

        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1 && entity.ProductType_Extend_Default != "")
                {
                    Extend_Value = tools.CheckStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                    mode_str += "<input type=\"hidden\" name=\"filter_" + entity.ProductType_Extend_ID + "\" id=\"filter_" + entity.ProductType_Extend_ID + "\" value=\"" + Extend_Value + "\">";
                }
            }
        }

        mode_str += "</form>";
        mode_str += "</div>";

        return mode_str;
    }

    #endregion

    /// <summary>
    /// 同类品牌
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="typeid"></param>
    public void SimilarBrand(int cateid, int typeid)
    {
        StringBuilder strHTML = new StringBuilder();

        ProductTypeInfo producttype = MyType.GetProductTypeByID(typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
        if (producttype != null)
        {
            IList<BrandInfo> brands = producttype.BrandInfos;
            if (brands != null)
            {
                foreach (BrandInfo brand in brands)
                    strHTML.Append("<li><a href=\"/product/category.aspx?brand_id=" + brand.Brand_ID + "\">" + brand.Brand_Name + "</a></li>");
            }
        }
        Response.Write(strHTML.ToString());
    }

    /// <summary>
    /// 同类产品推荐
    /// </summary>
    /// <param name="cateid"></param>
    public void Similar_Product(int cateid, int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Get_All_CateProductID(cateid.ToString())));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "<>", Product_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
        IList<ProductInfo> entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            string targetURL;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li>");
                strHTML.Append("<div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                strHTML.Append("<p><a href=\"" + targetURL + "\" target=\"_blank\">" + entity.Product_Name + "</a></p>");
                if (entity.Product_PriceType == 1)
                {
                    strHTML.Append("<p><strong>" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong></p>");
                }
                else
                {
                    strHTML.Append("<p><strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong></p>");
                }
                strHTML.Append("</li>");

                targetURL = null;
            }
        }

        Response.Write(strHTML.ToString());
    }


    public void Similar_Recommend_Products(int cateid)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Get_All_CateProductID(cateid.ToString())));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
        IList<ProductInfo> entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            string targetURL;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li><a href=\"" + targetURL + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 25) + "</a></li>");

                targetURL = null;
            }
        }

        Response.Write(strHTML.ToString());
    }


    /// <summary>
    /// 同类产品推荐
    /// </summary>
    /// <param name="cateid"></param>
    public void Similar_Product(int cateid)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Get_All_CateProductID(cateid.ToString())));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
        IList<ProductInfo> entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            string targetURL;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li>");
                strHTML.Append("<div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                strHTML.Append("<p><a href=\"" + targetURL + "\" target=\"_blank\">" + entity.Product_Name + "</a></p>");
                if (entity.Product_PriceType == 1)
                {
                    strHTML.Append("<p><strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong></p>");
                }
                else
                {
                    strHTML.Append("<p><strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong></p>");
                }
                strHTML.Append("</li>");

                targetURL = null;
            }
        }

        Response.Write(strHTML.ToString());
    }

    //当前商品筛选条件
    public string product_filter_value()
    {
        string functionReturnValue = "";
        int brand_id = tools.CheckInt(Request["brand_id"]);

        BrandInfo brand = MyBrand.GetBrandByID(brand_id, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        if (brand != null)
        {
            functionReturnValue = functionReturnValue + brand.Brand_Name;
        }

        string Product_extend_Request_Get = "";
        string Product_extend_Request_Get_Value = "";
        foreach (string extend in Request.QueryString)
        {
            Product_extend_Request_Get = extend;
            if (Product_extend_Request_Get != null)
            {
                if (tools.Left(tools.NullStr(Product_extend_Request_Get), 6) == "filter")
                {
                    Product_extend_Request_Get_Value = tools.CheckStr(Request[Product_extend_Request_Get]);
                    if (Product_extend_Request_Get_Value.Length > 0)
                    {
                        if (functionReturnValue.Length > 0)
                        {
                            functionReturnValue = functionReturnValue + " + " + Product_extend_Request_Get_Value;
                        }
                        else
                        {
                            functionReturnValue = functionReturnValue + Product_extend_Request_Get_Value;
                        }
                    }
                }
            }
        }
        return functionReturnValue;
    }

    //商品列表
    public void Product_List(string uses, int cate_id, int irowmax, string keyword)
    {
        string orderby;
        string supplier_id = "";
        int isgallerylist;
        string keywords = "";
        string Product_Arry = "";
        string Cate_Arry = "";
        int pagesize = 12;
        string Extend_ProductArry = "0";
        int Extend_ID;
        int product_counts = 0;
        int product_count = 0;
        double price_min = 0;
        double price_max = 0;
        string brand_id = "";
        string region_id = "";
        int isgroup = 0;
        int icount = 1;
        int irow;
        string promotionstr = "";
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "?";


        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0 && isgallerylist != 2)
        {
            isgallerylist = 1;
        }
        page_url = page_url + "isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);

        //关键词
        //keyword = tools.CheckStr(Request["top_keyword"]);
        keywords = tools.CheckStr(Request["key_word"]);
        supplier_id = tools.CheckStr(Request["supplier_id"]);
        if (keyword == "")
        {
            keyword = keywords;
        }
        if (keyword != "")
        {
            page_url = page_url + "&key_word=" + keyword;
        }
        if (cate_id != 0)
        {
            page_url = page_url + "&cate_id=" + cate_id;
        }
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);

        }

        if ((cate_id > 0 && Cate_Arry != "") || cate_id == 0)
        {
            //构建查询条件
            QueryInfo Query = new QueryInfo();
            Query.PageSize = pagesize;
            Query.CurrentPage = curr_page;

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));


            //聚合是否列表显示 暂时屏蔽掉
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_GroupCode", "=", ""));
            // Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_GroupCode", "=", ""));
            //Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_IsListShow", "=", "1"));
            if (Cate_Arry.Length > 0 && cate_id > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }


            IList<ProductInfo> BiddingProductList = null;//new List<ProductInfo>(50);
            string bidding_idstr = "0";
            int bidding_count = 0;
            //按关键词查询
            if (keyword != "")
            {
                foreach (string keywordsub in keyword.Split(' '))
                {
                    if (keywordsub != "")
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "%like%", keywordsub));
                        Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "%like%", keywordsub));
                        Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "%like%", keywordsub));
                        Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "%like%", keywordsub));
                        Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "%like%", keywordsub));

                        //添加竞价商品
                        //GetBiddingProduct(keywordsub, ref BiddingProductList);

                    }
                }
                if (BiddingProductList != null)
                {
                    bidding_count = BiddingProductList.Count;
                    foreach (ProductInfo productinfo in BiddingProductList)
                    {
                        bidding_idstr = bidding_idstr + "," + productinfo.Product_ID;
                    }
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "not in", bidding_idstr));
                }
            }

            foreach (string querystring in Request.QueryString)
            {
                if (querystring != null)
                {
                    if (querystring.IndexOf("filter_") >= 0)
                    {
                        Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                        if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                        {
                            page_url = page_url + "&" + querystring + "=" + Request.QueryString[querystring];
                            foreach (string sid in tools.CheckStr(Request.QueryString[querystring]).Split(','))
                            {
                                if (sid.Length > 0)
                                {
                                    Extend_ProductArry += "," + Myproduct.GetExtendProductID(Extend_ID, sid);

                                }
                                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                            }

                        }
                    }
                }
            }

            if (bidding_count > 0)
            {
                Query.PageSize = 0;
                Query.CurrentPage = 1;
            }

            price_min = tools.CheckFloat(Request["price_min"]);
            price_max = tools.CheckFloat(Request["price_max"]);
            isgroup = tools.CheckInt(Request["isgroup"]);
            brand_id = tools.CheckStr(Request["brand_id"]);
            region_id = tools.CheckStr(Request["region_id"]);
            //if (brand_id.Length > 0)
            //{
            //    page_url = page_url + "&brand_id=" + brand_id;
            //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "in", brand_id.ToString()));
            //}
            if (region_id.Length > 0)
            {
                page_url = page_url + "&region_id=" + region_id;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_State_Name", "in", region_id.ToString()));
            }
            if (price_min > 0)
            {
                page_url = page_url + "&price_min=" + price_min;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", "<=", price_min.ToString()));
            }

            if (price_max > 0)
            {
                page_url = page_url + "&price_max=" + price_max;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", "<=", price_max.ToString()));
            }

            page_url = page_url + "&orderby=" + orderby;
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            if (supplier_id.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "in", supplier_id));
            }

            switch (orderby)
            {
                case "hotsale_up":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "asc"));
                    break;
                case "hotsale_down":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
                    break;
                case "price_up":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "asc"));
                    break;
                case "price_down":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "desc"));
                    break;
                case "time_up":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "asc"));
                    break;
                case "time_down":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
                    break;
                case "star_up":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Review_Average", "Asc"));
                    break;
                case "star_down":
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Review_Average", "Desc"));
                    break;
                default:
                    //改排序按照评论,销量两种规则
                    //  Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "ASC"));
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Review_Average", "Desc"));
                    Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
                    break;
            }
            //PromotionLimitInfo limitinfo;

            IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            PageInfo pageinfo = new PageInfo();
            int result_count = 0;
            if (Products != null)
            {
                result_count = Products.Count;
            }
            //重新定义分页
            if (bidding_count > 0)
            {

                pageinfo.RecordCount = result_count + bidding_count;
                pageinfo.PageSize = pagesize;
                if (pageinfo.RecordCount % pagesize == 0)
                {
                    pageinfo.PageCount = pageinfo.RecordCount / pagesize;
                }
                else
                {
                    pageinfo.PageCount = (int)(pageinfo.RecordCount / pagesize) + 1;
                }
                pageinfo.CurrentPage = curr_page;
            }
            else
            {
                pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            }
            StringBuilder StrHTML = new StringBuilder();
            if (Products != null)
            {

                product_counts = 1;

                if (isgallerylist == 1)
                {
                    //列表
                    StrHTML.Append("<div class=\"blk16\" style=\"border:0px; height:auto;\"><table width=\"968\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    StrHTML.Append("  <tr>");
                    StrHTML.Append("    <td width=\"404\" class=\"name\">商 品</td>");
                    StrHTML.Append("    <td width=\"110\" class=\"name\">价 格</td>");
                    StrHTML.Append("    <td width=\"109\" class=\"name\">好评数</td>");
                    StrHTML.Append("    <td width=\"220\" class=\"name\">所属商家</td>");
                    StrHTML.Append("    <td width=\"125\" class=\"name\">购 买</td>");
                    StrHTML.Append("  </tr>");

                    irow = 0;

                    #region 正常商品
                    int current_num = 0;
                    if (bidding_count < (pageinfo.CurrentPage) * pagesize)
                    {
                        string brand_name = "";
                        current_num = bidding_count;
                        foreach (ProductInfo entity in Products)
                        {
                            brand_name = "";
                            current_num = current_num + 1;

                            if (current_num <= (pageinfo.CurrentPage) * pagesize)
                            {
                                irow++;
                                BrandInfo brandinfo = GetBrandInfoByID(entity.Product_BrandID);
                                if (brandinfo != null)
                                {
                                    brand_name = brandinfo.Brand_Name;
                                }
                                if (irow % 2 > 0)
                                {
                                    StrHTML.Append("  <tr class=\"bg\">");
                                }
                                else
                                {
                                    StrHTML.Append("  <tr>");
                                }
                                StrHTML.Append("    <td>");
                                StrHTML.Append("        <dl>");
                                StrHTML.Append("            <dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Product_Name + "</a></dt>");
                                StrHTML.Append("            <dd>规格：" + entity.Product_Spec + " | 交货周期：" + entity.U_Product_DeliveryCycle + " </dd>");
                                StrHTML.Append("        </dl>");
                                StrHTML.Append("    </td>");
                                StrHTML.Append("    <td><strong>" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong></td>");
                                StrHTML.Append("    <td><span>" + Product_Review_CountByStar(entity.Product_ID, "4,5") + "</span></td>");
                                StrHTML.Append("    <td>");
                                SupplierShop shopinfo = new SupplierShop();
                                SupplierShopInfo shopinfos = shopinfo.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                                if (shopinfos != null)
                                {
                                    //int addyear = (int)((DateTime.Today - shopinfos.Shop_Addtime.Date).TotalDays / 365 + 1);
                                    //if (addyear < 0)
                                    //{
                                    //    addyear = 0;
                                    //}
                                    StrHTML.Append("        <p><a href=\"" + pub.GetShopDomain(shopinfos.Shop_Domain) + "\">" + shopinfos.Shop_Name + "</a></p>");
                                    StrHTML.Append("        <p>入驻第<strong>" + pub.DateDiffYear(shopinfos.Shop_Addtime.Date, DateTime.Now) + "</strong>年" + SupplierTag(entity.Product_SupplierID) + "</p>");
                                    // StrHTML.Append("        <p>入驻第<strong>" + addyear + "</strong>年" + SupplierTag(entity.Product_SupplierID) + "</p>");

                                }

                                StrHTML.Append("    </td>");
                                StrHTML.Append("    <td>");
                                StrHTML.Append("        <label><a href=\"javascript:void(0);\" onclick=\"editbuyamount(-1," + entity.Product_UsableAmount + "," + entity.Product_ID + ");\">-</a><input name=\"\" type=\"text\" value=\"1\" id=\"buyamount_" + entity.Product_ID + "\" name=\"buyamount_" + entity.Product_ID + "\" /><a href=\"javascript:void(0);\" onclick=\"editbuyamount(1," + entity.Product_UsableAmount + "," + entity.Product_ID + ");\">+</a></label>");
                                StrHTML.Append("        <i><a href=\"javascript:void(0);\" onclick=\"Ajax_ListAddToCart(" + entity.Product_ID + ");\" class=\"a12\"></a><a href=\"javascript:void(0);\" onclick=\"product_favorites(" + entity.Product_ID + " );\" class=\"a13\"></a></i>");
                                StrHTML.Append("    </td>");
                                StrHTML.Append("  </tr>");
                            }
                        }
                    }
                    StrHTML.Append("</table></div>");
                    #endregion
                }
                else if (isgallerylist == 2)
                {
                    //图+列表
                    StrHTML.Append("<div class=\"blk151\">");
                    StrHTML.Append("    <table width=\"968\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    StrHTML.Append("  <tr>");
                    StrHTML.Append("    <td width=\"339\" class=\"name\" ><div class=\"td\" style=\"text-align:left; padding:0 0 0 45px;\">商 品</div></td>");
                    StrHTML.Append("    <td width=\"122\" class=\"name\">品 牌</td>");
                    StrHTML.Append("    <td width=\"103\" class=\"name\">价 格</td>");
                    StrHTML.Append("    <td width=\"93\" class=\"name\">好评数</td>");
                    StrHTML.Append("    <td width=\"186\" class=\"name\">所属商家</td>");
                    StrHTML.Append("    <td width=\"125\" class=\"name\">购买</td>");
                    StrHTML.Append("  </tr>");

                    irow = 0;
                    #region 正常商品
                    int current_num = 0;
                    if (bidding_count < (pageinfo.CurrentPage) * pagesize)
                    {
                        string brand_name = "";
                        current_num = bidding_count;
                        foreach (ProductInfo entity in Products)
                        {
                            brand_name = "";
                            current_num = current_num + 1;

                            if (current_num <= (pageinfo.CurrentPage) * pagesize)
                            {
                                irow++;
                                BrandInfo brandinfo = GetBrandInfoByID(entity.Product_BrandID);
                                if (brandinfo != null)
                                {
                                    brand_name = brandinfo.Brand_Name;
                                }
                                if (irow % 2 > 0)
                                {
                                    StrHTML.Append("  <tr class=\"bg\">");
                                }
                                else
                                {
                                    StrHTML.Append("  <tr>");
                                }
                                StrHTML.Append("    <td>");
                                StrHTML.Append("        <dl>");
                                StrHTML.Append("            <dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></dt>");
                                StrHTML.Append("            <dd>");
                                StrHTML.Append("                <p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Product_Name + "</a></p>");
                                StrHTML.Append("                <p>" + entity.Product_Spec + " | 交货周期：" + entity.U_Product_DeliveryCycle + "</p>");
                                StrHTML.Append("            </dd>");
                                StrHTML.Append("            <div class=\"clear\"></div>");
                                StrHTML.Append("        </dl>");
                                StrHTML.Append("    </td>");
                                StrHTML.Append("    <td><em>" + brand_name + "</em></td>");
                                StrHTML.Append("    <td><strong>" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong></td>");
                                StrHTML.Append("    <td><span>" + Product_Review_CountByStar(entity.Product_ID, "4,5") + "</span></td>");
                                StrHTML.Append("    <td>");
                                SupplierShop shopinfo = new SupplierShop();
                                SupplierShopInfo shopinfos = shopinfo.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                                if (shopinfos != null)
                                {
                                    //int addyear = (int)((DateTime.Today - shopinfos.Shop_Addtime.Date).TotalDays / 365 + 1);
                                    //if (addyear < 0)
                                    //{
                                    //    addyear = 0;
                                    //}
                                    StrHTML.Append("       <p><a href=\"" + pub.GetShopDomain(shopinfos.Shop_Domain) + "\">" + shopinfos.Shop_Name + "</a></p>");
                                    StrHTML.Append("        <p>入驻第<strong>" + pub.DateDiffYear(shopinfos.Shop_Addtime.Date, DateTime.Now) + "</strong>年" + SupplierTag(entity.Product_SupplierID) + "</p>");
                                    // StrHTML.Append("       <p>入驻第<strong>" + addyear + "</strong>年" + SupplierTag(entity.Product_SupplierID) + "</p>");
                                }
                                StrHTML.Append("    </td>");
                                StrHTML.Append("    <td>");
                                StrHTML.Append("        <label><a href=\"javascript:void(0);\" onclick=\"editbuyamount(-1," + entity.Product_UsableAmount + "," + entity.Product_ID + ");\">-</a><input name=\"\" type=\"text\" value=\"1\" id=\"buyamount_" + entity.Product_ID + "\" name=\"buyamount_" + entity.Product_ID + "\" /><a href=\"javascript:void(0);\" onclick=\"editbuyamount(1," + entity.Product_UsableAmount + "," + entity.Product_ID + ");\">+</a></label>");
                                StrHTML.Append("        <i><a href=\"javascript:void(0);\" onclick=\"Ajax_ListAddToCart(" + entity.Product_ID + ");\" class=\"a12\"></a><a href=\"javascript:void(0);\" onclick=\"product_favorites(" + entity.Product_ID + " );\" class=\"a13\"></a></i>");
                                StrHTML.Append("    </td>");
                                StrHTML.Append("  </tr>");
                            }
                        }
                    }
                    StrHTML.Append("</table></div>");
                    #endregion
                }
                else
                {
                    StrHTML.Append("<ul> ");
                    //橱窗
                    irow = 0;

                    #region 正常商品
                    int current_num = 0;
                    if (bidding_count < (pageinfo.CurrentPage) * pagesize)
                    {
                        current_num = bidding_count;
                        foreach (ProductInfo entity in Products)
                        {
                            current_num = current_num + 1;
                            if (current_num <= (pageinfo.CurrentPage) * pagesize)
                            {
                                irow++;

                                StrHTML.Append("<li><div class=\"img_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" ><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                                StrHTML.Append("<p style=\"padding-top:3px;\"><strong style=\"font-size:22px;\">" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong></p>");
                                StrHTML.Append("<p style=\"padding-bottom:10px;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\"  alt=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 20) + "</a></p>");

                                SupplierShop shopinfo = new SupplierShop();
                                SupplierShopInfo shopinfos = shopinfo.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                                ISupplierTag MySupplierTag = SupplierTagFactory.CreateSupplierTag();
                                IList<SupplierRelateTagInfo> EList = MySupplierTag.GetSupplierRelateTagsBySupplierID(entity.Product_SupplierID);

                                if (shopinfos != null)
                                {
                                    StrHTML.Append(" <span style=\"font-size:20px;\"><a href=\"" + pub.GetShopDomain(shopinfos.Shop_Domain) + "\" title=\"" + shopinfos.Shop_Name + "\" style=\"font-size:14px;\">" + tools.CutStr(shopinfos.Shop_Name, 14) + "</a>&nbsp&nbsp");
                                    if (EList != null)
                                    {
                                        int tagid = 0;
                                        foreach (var tag in EList)
                                        {
                                            tagid = tag.Supplier_RelateTag_TagID;
                                            if (tagid == 2)
                                            {

                                                SupplierTagInfo SupplierTaginfo = MySupplierTag.GetSupplierTagByID(tagid, pub.CreateUserPrivilege("169befcc-aa3b-42d1-b5b8-d1a08096bc0e"));
                                                if (SupplierTaginfo != null)
                                                {
                                                    StrHTML.Append("  <img src=\"" + pub.FormatImgURL(SupplierTaginfo.Supplier_Tag_Img, "fullpath") + "\" style=\"display:inline;margin-left:8px;height:20px;width:20px;\"  title=\"" + SupplierTaginfo.Supplier_Tag_Name + "\">");
                                                }

                                            }
                                            if (tagid == 3)
                                            {
                                                SupplierTagInfo SupplierTaginfo1 = MySupplierTag.GetSupplierTagByID(3, pub.CreateUserPrivilege("169befcc-aa3b-42d1-b5b8-d1a08096bc0e"));
                                                if (SupplierTaginfo1 != null)
                                                {
                                                    StrHTML.Append(" <img src=\"" + pub.FormatImgURL(SupplierTaginfo1.Supplier_Tag_Img, "fullpath") + "\" style=\"height:20px;width:20px;display:inline;\"  title=\"" + SupplierTaginfo1.Supplier_Tag_Name + "\" >");
                                                }

                                            }

                                        }

                                    }

                                    //SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                                    //if (supplierinfo != null)
                                    //{
                                    //    if (supplierinfo.Supplier_Security_Account > 0)
                                    //    {
                                    //        SupplierTagInfo SupplierTaginfo1 = MySupplierTag.GetSupplierTagByID(3, pub.CreateUserPrivilege("169befcc-aa3b-42d1-b5b8-d1a08096bc0e"));
                                    //        if (SupplierTaginfo1 != null)
                                    //        {
                                    //            StrHTML.Append(" <img src=\"" + pub.FormatImgURL(SupplierTaginfo1.Supplier_Tag_Img, "fullpath") + "\" style=\"height:20px;width:20px;display:inline;\" >");
                                    //        }

                                    //    }
                                    //}


                                    StrHTML.Append("   </span>");

                                }

                                StrHTML.Append("</li>");

                            }
                        }
                    }

                    #endregion
                    StrHTML.Append("  </ul>");
                    StrHTML.Append(" <div class=\"clear\"></div>");
                }
                Response.Write(StrHTML.ToString());
                //输出分页
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);

            }

        }

        //如果没有有商品记录
        if (product_counts == 0)
        {
            string key = tools.NullStr(Request["key_word"]);
            Session["product_sort"] = key;
            //key = Server.UrlEncode(key);
            Response.Write("<div style=\"border:1px solid #F1F1F1; width:771px;\">");
            Response.Write("<div style=\"background:url(/images/nosearchicon.gif) no-repeat 0px 0px; margin-top:40px; margin-left:95px; height:80px; font-size:16px; color:#333333; font-weight:bold;\">");
            Response.Write("<p style=\"padding-top:10px; padding-left:44px;\">很抱歉，没有找到相关的商品</p></div>");
            Response.Write("<div style=\" border-top:1px solid #F1F1F1; height:30px; width:700px; margin:0 auto;\"></div>");
            //Response.Write("<form id=\"bottomSearchForm\" method=\"get\" action=\"/product/search.aspx\">");
            Response.Write("<form id=\"bottomSearchForm\" method=\"get\" action=\"/product/product_search.aspx\">");
            Response.Write("<div style=\"height:200px;\">");
            Response.Write("<div>");
            Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr>");
            Response.Write("<td width=\"200\" style=\" font-size:14px; font-weight:bold; color:#333333;\" align=\"right\">重新搜索</td>");
            Response.Write("<td width=\"170\" align=\"right\">");
            Response.Write("<input type=\"text\" name=\"key_word\" style=\"text-indent: 5px;\" maxlength=\"12\" onfocus=\"this.select();\" value=\"" + key + "\" class=\"search_txt\" /></td>");
            Response.Write("<td align=\"left\" width=\"70\"><input type=\"image\" src=\"../images/search_btn.jpg\" /></td>");
            Response.Write("</tr>");
            Response.Write("</table>");
            Response.Write("</div>");
            Response.Write("<div style=\"padding-left:204px; color:#878787; padding-top:10px;\">");
            Response.Write("<p style=\"height:20px; line-height:20px; font-size:12px;\">1、看看输入的文字是否有误<br />2、可点击左侧的类目进行筛选<br/>3、<a href=\"/help/stockout.aspx\" style=\"color:#336699;\">缺货登记</a>");

            Response.Write("</div>");
            Response.Write("</div>");
            Response.Write("</form>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("</div>");

        }
    }

    /// <summary>
    /// 商家营销标签
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public string SupplierTag(int Supplier_ID)
    {

        ISupplierTag Myproduct = SupplierTagFactory.CreateSupplierTag();

        IList<SupplierRelateTagInfo> EList = Myproduct.GetSupplierRelateTagsBySupplierID(Supplier_ID);
        if (EList == null)
            return "";

        string strtabid = "0";
        foreach (SupplierRelateTagInfo E in EList)
            strtabid += "," + E.Supplier_RelateTag_TagID;

        if (strtabid == "0")
            return "";

        IList<SupplierTagInfo> entitys = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierTagInfo.Supplier_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierTagInfo.Supplier_Tag_ID", "in", strtabid));
        entitys = Myproduct.GetSupplierTags(Query, pub.CreateUserPrivilege("169befcc-aa3b-42d1-b5b8-d1a08096bc0e"));

        StringBuilder tagHTML = new StringBuilder();
        foreach (SupplierRelateTagInfo E in EList)
        {
            IList<SupplierTagInfo> resultlist = entitys.Where(p => p.Supplier_Tag_ID == E.Supplier_RelateTag_TagID).ToList();
            if (resultlist.Count > 0)
            {
                if (resultlist[0].Supplier_Tag_Img.Length > 0)
                {
                    tagHTML.Append("<img src=\"" + pub.FormatImgURL(resultlist[0].Supplier_Tag_Img, "fullpath") + "\" align=\"absmiddle\" title=\"" + resultlist[0].Supplier_Tag_Name + "\" alt=\"" + resultlist[0].Supplier_Tag_Name + "\" width=\"16\" height=\"16\" style=\"display:inline\" /> ");
                }
            }
        }

        Myproduct = null;

        return tagHTML.ToString();
    }

    public string Product_List_WholeSalePrice(ProductInfo entity)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<div class=\"p_box_info01\">");
        sb.Append(" <b><i>成交 " + entity.Product_SaleAmount + " 件</i>批发价：");
        if (entity.Product_PriceType == 1)
        {
            sb.Append("<strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong>");
        }
        else
        {
            sb.Append("<strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong>");
        }

        IList<ProductWholeSalePriceInfo> entitys = MySalePrice.GetProductWholeSalePriceByProductID(entity.Product_ID);

        sb.Append("</b>");
        sb.Append("<div class=\"p_box_info01_fox\">");
        sb.Append("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"></a>");
        sb.Append("<dl>");
        //sb.Append("<dt>运费<br><strong>8.00</strong>元<br>起</dt>");
        sb.Append("<dd>");
        if (entitys != null)
        {
            foreach (ProductWholeSalePriceInfo SalePriceInfo in entitys)
            {
                sb.Append("<p><span>" + SalePriceInfo.Product_WholeSalePrice_MinAmount + "-" + SalePriceInfo.Product_WholeSalePrice_MaxAmount + "件</span>" + pub.FormatCurrency(SalePriceInfo.Product_WholeSalePrice_Price) + "</p>");
            }
        }
        sb.Append("</dd>");
        sb.Append("<div class=\"clear\"></div>");
        sb.Append("</dl>");
        sb.Append("</div>");
        sb.Append("</div>");

        return sb.ToString();
    }

    public string Product_List_SupplierInfo(SupplierInfo entity)
    {
        StringBuilder sb = new StringBuilder();

        if (entity != null)
        {
            SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Supplier_ID);
            if (shopInfo != null)
            {
                sb.Append("<div class=\"p_box_info03_fox_main\">");
                sb.Append("<p>" + shopInfo.Shop_Name + "</p>");
                sb.Append("<p><span>供应等级：</span><img src=\"/images/icon05.png\"><img src=\"/images/icon05.png\"></p>");
                sb.Append("<p><span>入驻：</span>第" + pub.DateDiffYear(shopInfo.Shop_Addtime, DateTime.Now) + "年</p>");
                sb.Append("<p><span>经营模式：</span>经销批发</p>");
                sb.Append("<p><span>供应商品：</span>" + supplier_class.GetSupplierProductAmount(entity.Supplier_ID) + "条</p>");
                sb.Append("<p><span>注册时间：</span>" + entity.Supplier_Addtime.Year + "年</p>");
                sb.Append("<p style=\"border-bottom: 1px solid #eeeeee; padding: 0 0 5px 15px; margin-bottom: 5px;\"><span>注册资金：</span>" + (entity.Supplier_RegisterFunds == 0 ? "无需验资" : "" + entity.Supplier_RegisterFunds + "") + "</p>");
                sb.Append("<p><span>认证荣誉：</span>1项</p>");
                sb.Append("<p><span>认证信息：</span>已通过认证</p>");
                sb.Append("<p style=\"background-color: #f2f6f5; border-top: 1px solid #eeeeee; line-height: 30px; margin-top: 5px;\"><span>交易勋章：</span><img src=\"/images/icon06.png\"><img src=\"/images/icon06.png\"></p>");
                sb.Append("</div>");
            }

        }
        return sb.ToString();
    }

    public int ProductCount(string uses, int cate_id)
    {
        string orderby;
        int isgallerylist;
        string keyword = Request["keyword"];
        int tag_id = tools.CheckInt(Request["tag_id"]);
        string Product_Arry = "";
        string Cate_Arry = "";
        int pagesize = 20;
        string Extend_ProductArry = "";
        int Extend_ID;
        int product_counts = 0;
        int product_count = 0;
        double price_min = 0;
        double price_max = 0;
        int brand_id = 0;
        int isgroup = 0;
        int icount = 1;
        int irow;
        string promotionstr = "";
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "?";

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
            pagesize = 10;
        }
        page_url = page_url + "isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);
        page_url = page_url + "&orderby=" + orderby;

        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            page_url = page_url + "&keyword=" + keyword;
        }
        if (cate_id != 0)
        {
            page_url = page_url + "&cate_id=" + cate_id;
        }
        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);

        }

        //构建查询条件
        QueryInfo Query = new QueryInfo();
        Query.PageSize = pagesize;
        Query.CurrentPage = curr_page;
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //按类查询
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }
        IList<ProductInfo> BiddingProductList = new List<ProductInfo>(50);
        string bidding_idstr = "0";
        int bidding_count = 0;
        //按关键词查询
        if (keyword != "")
        {
            foreach (string keywordsub in keyword.Split(' '))
            {
                if (keywordsub != "")
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Code", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Name", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubNameInitials", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Intro", "%like%", keywordsub));
                }
            }
        }
        //类型标签
        if (tag_id == 1 || tag_id == 2 || tag_id == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", "select Product_RelateTag_ProductID from Product_RelateTag where Product_RelateTag_TagID=" + tag_id));
        }
        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (Request.QueryString[querystring] != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                        page_url = page_url + "&" + querystring + "=" + Request.QueryString[querystring];
                    }
                }
            }
        }

        if (bidding_count > 0)
        {
            Query.PageSize = 0;
            Query.CurrentPage = 1;
        }


        //按促销条件
        switch (uses)
        {
            case "groupbuy":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsGroupBuy", "=", "1"));
                break;
                //case "coinbuy":
                //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "1"));
                //    break;
        }
        price_min = tools.CheckFloat(Request["price_min"]);
        price_max = tools.CheckFloat(Request["price_max"]);
        isgroup = tools.CheckInt(Request["isgroup"]);
        brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            page_url = page_url + "&brand_id=" + brand_id;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }
        if (price_min > 0)
        {
            page_url = page_url + "&price_min=" + price_min;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", ">=", price_min.ToString()));
        }

        if (price_max > 0)
        {
            page_url = page_url + "&price_max=" + price_max;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", "<=", price_max.ToString()));
        }
        if (isgroup == 1)
        {
            page_url = page_url + "&isgroup=" + isgroup;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsGroupBuy", "=", "1"));
        }

        //有货
        int instock = tools.CheckInt(Request["instock"]);
        if (instock == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_UsableAmount", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_IsNoStock", "=", "1"));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        switch (orderby)
        {
            case "hotsale_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "asc"));
                break;
            case "hotsale_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
                break;
            case "price_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "asc"));
                break;
            case "price_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "desc"));
                break;
            case "time_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "asc"));
                break;
            case "time_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
                break;
            case "star_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Review_Average", "Asc"));
                break;
            case "star_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Review_Average", "Desc"));
                break;
            default:
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
                break;
        }
        // PromotionLimitInfo limitinfo;

        //IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo pageinfo = new PageInfo();
        pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        //SupplierShopInfo shopinfo;

        if (pageinfo == null)
        {
            return 0;
        }
        else
        {
            return pageinfo.RecordCount;
        }

    }

    public int GetFavoriteCountByProductID(int Product_ID)
    {
        int count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_TargetID", "=", Product_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> entitys = MyFavorites.GetMemberFavoritess(Query);

        if (entitys != null)
        {
            count = entitys.Count;
        }
        return count;
    }

    //根据商品编号获取商品信息
    public ProductInfo GetProductByID(int ID)
    {
        return Myproduct.GetProductByID(ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    }

    //获取商品图片信息
    public string[] GetProductImg(int product_id)
    {
        string ipaths = Myproduct.GetProductImg(product_id);

        string[] ipathArr = { "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif" };

        if (ipaths.Length > 0)
        {
            ipathArr = ipaths.Split(',');
        }
        return ipathArr;
    }






    //商品详情页
    public void Product_Detail(ProductInfo productinfo, bool isview)
    {
        int cur_supplierid = 0;
        string supplier_id = "";
        string Supplier_Online_Code = "";
        if ((Session["supplier_id"] != null) && (Session["supplier_id"] != ""))
        {
            supplier_id = tools.NullStr(Session["supplier_id"].ToString());
        }
        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = 1;
        Query1.CurrentPage = 1;
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_ID", "=", supplier_id));
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
        Query1.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        IList<SupplierInfo> supplierinfos = Mysupplier.GetSuppliers(Query1, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierinfos != null)
        {
            int i = 0;
            foreach (SupplierInfo supplierinfo in supplierinfos)
            {
                i++;
                cur_supplierid = supplierinfo.Supplier_ID;
            }
        }

        Session["cur_supplierid"] = cur_supplierid;
        string Supplier_CompanyName = "";
        string Supplier_Address = "";
        string Brand_Name = "";
        string supplier_img = "";
        string Supplier_Phone = "";
        int Supplier_ID = 0;
        StringBuilder sb = new StringBuilder();

        sb.Append("      <div class=\"pg_left\">");

        SupplierInfo supplierInfo = Mysupplier.GetSupplierByID(productinfo.Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierInfo != null)
        {
            Supplier_ID = supplierInfo.Supplier_ID;
            Supplier_CompanyName = supplierInfo.Supplier_CompanyName;
            Supplier_Address = supplierInfo.Supplier_Address;


            //SupplierShop suppliershop = new SupplierShop();
            SupplierShop shopinfo = new SupplierShop();
            //SupplierShop SupplierShopEntity = shopinfo.GetSupplierShopByID(supplierInfo.Supplier_ID);
            SupplierShopInfo shopinfos = shopinfo.GetSupplierShopBySupplierID(supplierInfo.Supplier_ID);
            if (shopinfos != null)
            {
                supplier_img = pub.FormatImgURL(shopinfos.Shop_Img, "fullpath");
            }


            Supplier_Phone = supplierInfo.Supplier_Phone;


        }
        QueryInfo QueryS_OnLine = new QueryInfo();
        QueryS_OnLine.PageSize = 1;
        QueryS_OnLine.CurrentPage = 1;
        QueryS_OnLine.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_SupplierID", "=", Supplier_ID.ToString()));
        QueryS_OnLine.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Site", "=", "CN"));
        QueryS_OnLine.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_Sort", "Asc"));
        QueryS_OnLine.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_ID", "Desc"));
        IList<SupplierOnlineInfo> SupplierOnlineInfos = MySupplierOnline.GetSupplierOnlines(QueryS_OnLine);
        if (SupplierOnlineInfos != null)
        {

            int i = 0;
            foreach (SupplierOnlineInfo SupplierOnlineInfo in SupplierOnlineInfos)
            {
                i++;
                if (i == 1)
                {
                    Supplier_Online_Code = SupplierOnlineInfo.Supplier_Online_Code;
                }
                else
                {
                    return;
                }

            }
        }




        BrandInfo brandinfo = MyBrand.GetBrandByID(productinfo.Product_BrandID, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        if (brandinfo != null)
        {
            Brand_Name = brandinfo.Brand_Name;
        }
        string[] Img_Arry = GetProductImg(productinfo.Product_ID);

        string Product_Img = pub.FormatImgURL(Img_Arry[0], "fullpath");
        string S_Product_Img = pub.FormatImgURL(Img_Arry[0], "thumbnail");
        //string Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "fullpath");
        //string S_Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "thumbnail");
        //string Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "fullpath");
        //string S_Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "thumbnail");
        //string Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "fullpath");
        //string S_Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "thumbnail");
        //string Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "fullpath");
        //string S_Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "thumbnail");
        if (Product_Img == "/images/detail_no_pic.gif")
        {
            Product_Img = "/images/detail_no_pic_big.gif";
            S_Product_Img = "/images/detail_no_pic_big.gif";
        }

        sb.Append("<dl>");
        sb.Append("<dt>");
        sb.Append("    <a href=\"javascript:;\" class=\"jqzoom\" id=\"displayphoto\"  name=\"demo1\" title=\"\"><img id=\"product_img\" src=\"" + Product_Img + "\"></a>");


        sb.Append("     </dt>");
        sb.Append("<dd>");
        sb.Append("<ul id=\"scrollable\">");

        if (Product_Img != "/images/detail_no_pic.gif")
        {
            sb.Append("<li class=\"on\" id=\"product_img_s_1\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img + "',1);switchimgborder('product_img_s_1');\"><img src=\"" + Product_Img + "\"></li>");
        }

        //if (Product_Img_Ext_1 != "/images/detail_no_pic.gif")
        //{
        //    sb.Append("<li id=\"product_img_s_2\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_1 + "',1);switchimgborder('product_img_s_2');\"><img src=\"" + Product_Img_Ext_1 + "\"></li>");
        //}

        //if (Product_Img_Ext_2 != "/images/detail_no_pic.gif")
        //{
        //    sb.Append("<li id=\"product_img_s_3\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_2 + "',1);switchimgborder('product_img_s_3');\"><img src=\"" + Product_Img_Ext_2 + "\"></li>");
        //}

        //if (Product_Img_Ext_3 != "/images/detail_no_pic.gif")
        //{
        //    sb.Append("<li id=\"product_img_s_4\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_3 + "',1);switchimgborder('product_img_s_4');\"><img src=\"" + Product_Img_Ext_3 + "\"></li>");
        //}
        sb.Append("</ul>");
        sb.Append("<div class=\"clear\"></div>");
        sb.Append("</dd>");
        sb.Append("</dl>");



        sb.Append("  </div>");

        string IsHaveStock = "";
        int Product_StockAmount = productinfo.Product_StockAmount;
        if (Product_StockAmount > 0)
        {
            IsHaveStock = "有货";
        }
        else
        {
            IsHaveStock = "无货";
        }
        //sb.Append("            <td style=\"float:Right;margin-top:10px;\">");
        //
        //sb.Append("            </td>");

        sb.Append("         <div class=\"pg_right\">");
        sb.Append("                  <h2>" + productinfo.Product_Name + "<span>" + productinfo.Product_Note + "</span></h2>");




        sb.Append("                  <div class=\"pg_r_info01\">");
        sb.Append("                        <ul>");
        sb.Append("                            <li>价格：<strong>" + pub.FormatCurrency(pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "</strong><i>" + IsHaveStock + "</i></li>");

        sb.Append("                            <li>评价：");
        for (int i = 1; i <= (int)productinfo.Product_Review_Average; i++)
        {
            sb.Append("<img src=\"/images/x2.jpg\" />");
        }
        for (int i = 1; i <= 5 - (int)productinfo.Product_Review_Average; i++)
        {
            sb.Append("<img src=\"/images/x.jpg\" />");
        }

        sb.Append("          （<a href=\"/product/reviews.aspx?product_id=" + productinfo.Product_ID + "\" class=\"a15\">共有" + productinfo.Product_Review_ValidCount + "条评价</a>）</li>");


        //sb.Append("                            <li>促销：该商品消费满200元减免50</li>");
        sb.Append("                        </ul>");
        //sb.Append("                        <span><img src=\"/images/手机端.png\">商品二维码</span>");
        sb.Append("                  </div>");
        sb.Append("                  <div class=\"pg_r_info02\">");
        sb.Append("                       <ul>");
        sb.Append("                           <li>商品产地：" + myaddr.Get_State_Name(productinfo.Product_State_Name) + " " + myaddr.Get_City_Name(productinfo.Product_City_Name) + " " + myaddr.Get_County_Name(productinfo.Product_County_Name) + "</li>");
        //sb.Append("                           <li>卖家：" + Supplier_CompanyName + "" + Supplier_Online_Code + "</li>");

        SupplierShop shopin = new SupplierShop();



        QueryInfo queryinfo00 = new QueryInfo();


        queryinfo00.PageSize = 0;
        queryinfo00.CurrentPage = 1;
        queryinfo00.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_IsActive", "=", "1"));
        queryinfo00.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Site", "=", "CN"));
        queryinfo00.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_SupplierID", "=", Supplier_ID.ToString()));

        queryinfo00.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_ID", "Desc"));
        IList<SupplierOnlineInfo> supplierinfos1 = MySupplierOnline.GetSupplierOnlines(queryinfo00);


        if (Supplier_Online_Code.Length > 50)
        {
            sb.Append("                           <li>" + Supplier_Online_Code + "</li>");
        }

        else
        {
            if (supplierinfos1 != null)
            {
                int i = 0;
                foreach (SupplierOnlineInfo supplierinfo in supplierinfos1)
                {
                    i++;

                    if ((supplierinfo.Supplier_Online_Code.Length < 11) && (supplierinfo.Supplier_Online_Code.Length > 0))
                    {
                        if (i == 1)
                        {
                            sb.Append("                           <li> <a target=\"_blank\" href=\"http://wpa.qq.com/msgrd?v=3&uin=" + Supplier_Online_Code + "&site=qq&menu=yes\"><img border=\"0\" src=\"http://wpa.qq.com/pa?p=2:" + Supplier_Online_Code + ":51\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\"/></a></li>");
                        }

                    }
                }
            }

        }




        //sb.Append("                           <li>产品库存：" + productinfo.Product_UsableAmount + " ");
        sb.Append("                           <li>产品库存：" + productinfo.Product_StockAmount + " ");



        if ((productinfo.Product_Unit.Length > 0) && (productinfo.Product_Unit != "0"))
        {
            sb.Append(" (单位:" + productinfo.Product_Unit + ")  ");
        }


        sb.Append("    </li>                          ");

        sb.Append("                           <li>规格型号：" + productinfo.Product_Spec + "</li>                          ");
        sb.Append("                           <li>起订量：" + productinfo.U_Product_MinBook + "   ");

        if ((productinfo.Product_Unit.Length > 0) && (productinfo.Product_Unit != "0"))
        {
            sb.Append(" (单位:" + productinfo.Product_Unit + ")  ");
        }

        sb.Append("         </li>                          ");
        sb.Append("                       </ul>                                                   ");
        sb.Append("                  </div>                                                       ");





        sb.Append("                  <div class=\"pg_r_info03\">                                    ");
        sb.Append("                        <ul>                                                   ");



        sb.Append(Product_BuyExtend_Show(productinfo.Product_ID, productinfo.Product_TypeID, productinfo.Product_GroupCode));




        sb.Append("                            <li>数量：<label> <input name=\"buy_amount\" id=\"buy_amount\" onkeyup=\"setAmount.modify('#buy_amount');\" type=\"text\" value=\"1\" />");

        sb.Append("               <a href=\"javascript:void(0);\" onclick=\"setAmount.add('#buy_amount');\" >+</a>");
        sb.Append("             <a href=\"javascript:;\" onclick=\"setAmount.reduce('#buy_amount');\"  >-</a></label>");
        if (productinfo.Product_SupplierID == cur_supplierid)
        {
            sb.Append("                  <a href=\"javascript:void(0)\" onclick=\"Ajax_AddToCart_Confirm(" + productinfo.Product_ID + ")\"  class=\"a14_hui\">我要采购</a></li>");
        }
        else
        {
            sb.Append("                  <a href=\"javascript:void(0)\" onclick=\"Ajax_AddToCart_Confirm(" + productinfo.Product_ID + ")\"  class=\"a14\">我要采购</a><a href=\"javascript:void(0);\" onclick=\"product_favorites(" + productinfo.Product_ID + " );\"  class=\"a16\">收 藏</a></li>");
        }



        sb.Append("                        </ul> ");
        sb.Append("                  </div>                     ");
        sb.Append("            </div>                           ");
        sb.Append("            <div class=\"pg_info\">            ");
        sb.Append("                  <dl>                       ");

        SupplierShopInfo shopinfos1 = shopin.GetSupplierShopBySupplierID(productinfo.Product_SupplierID);
        if (shopinfos1 != null)
        {
            sb.Append("                      <dt><a href=\"" + pub.GetShopDomain(shopinfos1.Shop_Domain) + "\">");

            if (supplier_img != null)
            {
                sb.Append("      <img src=" + supplier_img + ">");
            }
            else
            {

                sb.Append("     <img src=\"../暂无图片.png\"/>");
            }

            sb.Append("   公司名称:" + Supplier_CompanyName + "</a></dt>");

        }
        else
        {
            sb.Append("                      <dt><a>");

            if (supplier_img != null)
            {
                sb.Append("      <img src=" + supplier_img + ">");
            }
            else
            {

                sb.Append("     <img src=\"../暂无图片.png\"/>");
            }
            sb.Append("       公司名称:" + Supplier_CompanyName + "</a></dt>");
        }
        sb.Append("                      <dd>");
        if (supplierInfo != null)
        {
            sb.Append("<p><span>入住时间：</span>" + supplierInfo.Supplier_Addtime.ToString("yyyy年MM月dd日") + "</p>");
        }

        sb.Append("                          <p>公司地址：" + Supplier_Address + "</p>");
        if (shopinfos1 != null)
        {
            sb.Append("  		<p>入驻第 <strong style=\"color:#ff6600\">" + pub.DateDiffYear(shopinfos1.Shop_Addtime, DateTime.Now) + "</strong> 年</p>");
        }


        sb.Append("                          <p>服务电话：" + Supplier_Phone + "</p>");
        sb.Append("                          <p>在线交易：" + MyOrders.GetSupplierOrdersNum(Supplier_ID) + "笔</p>");

        if (shopinfos1 != null)
        {
            //sb.Append("                          <a href=\"" + pub.GetShopDomain(shopinfos.Shop_Domain) + "\" target=\"_blank\" >进入店铺</a>");
        }

        sb.Append("                      </dd>");
        sb.Append("                  </dl>");

        sb.Append("     </div>");


        Response.Write(sb.ToString());

    }

    public void Product_Detail_SupplierInfo(int supplier_id, int product_id)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierInfo supplierInfo = Mysupplier.GetSupplierByID(supplier_id, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        SupplierShopInfo shopInfo = null;
        string shop_img = "";
        string Shop_Domain = "http://";

        if (supplierInfo != null)
        {
            shopInfo = MyShop.GetSupplierShopBySupplierID(supplier_id);
            if (shopInfo != null)
            {
                shop_img = shopInfo.Shop_Img;
                Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
            }

            strHTML.Append("<div class=\"b28_info02\">");
            strHTML.Append("<dl>");
            strHTML.Append("<dt><a href=\"" + Shop_Domain + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(shop_img, "fullpath") + "\"></a></dt>");
            strHTML.Append("<dd>");
            strHTML.Append("<p><a href=\"" + Shop_Domain + "\" target=\"_blank\">" + supplierInfo.Supplier_CompanyName + "</a></p>");
            strHTML.Append("<p><a>" + supplierInfo.Supplier_Contactman + "</a><span onclick=\"NTKF.im_openInPageChat('sz_" + (supplierInfo.Supplier_ID + 1000) + "_9999','" + product_id + "');\"><img src=\"/images/icon12.png\" style=\"width:74px;display:inline;vertical-align: middle;margin-left:10px;\"></span></p>");
            strHTML.Append("<p><span>入住时间：</span>" + supplierInfo.Supplier_Addtime.ToString("yyyy年MM月dd日") + "</p>");
            strHTML.Append("<p><span>公司地址：</span>" + myaddr.DisplayAddress(supplierInfo.Supplier_State, "", "") + "</p>");
            strHTML.Append("<p><span>入驻第<strong>" + pub.DateDiffYear(shopInfo.Shop_Addtime, DateTime.Now) + "</strong>年</span></p>");
            strHTML.Append("</dd>");
            strHTML.Append("</dl>");
            strHTML.Append("</div>");
            strHTML.Append("<div class=\"b28_info03\">");
            strHTML.Append("<ul>");
            strHTML.Append("<li><a href=\"javascript:;\"><img src=\"/images/icon26.jpg\">买家保障</a><a href=\"javascript:;\"><img src=\"/images/icon33.jpg\">企业身份认证</a></li>");
            strHTML.Append("<li><span>交易等级：</span><img src=\"/images/icon34.jpg\"></li>");
            strHTML.Append("<li><span>满意度：</span>4.2</li>");
            strHTML.Append("<li style=\"padding: 5px 0 5px 12px;\"><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">进入店铺</a><a href=\"javascript:;\" onclick=\"favorites_shop_ajax(" + shopInfo.Shop_ID + ")\" class=\"a33\">关注店铺</a></li>");
            strHTML.Append("</ul>");
            strHTML.Append("</div>");
        }
        Response.Write(strHTML.ToString());
    }


    public string Delivery_Province_Select()
    {
        string html = "";
        IList<StateInfo> stateList = null;
        int i = 0;
        stateList = MyAddr.GetStatesByCountry("1");
        if (stateList != null)
        {
            html += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"5\" width=\"100%\">";
            html += "<tr>";
            foreach (StateInfo entity in stateList)
            {
                if (i > 0 && i % 4 == 0)
                {
                    html += "</tr><tr>";
                }
                i = i + 1;
                html += "<td width=\"25%\" onclick=\"$('#city_container').load('/product/ask_do.aspx?action=getcity&state=" + entity.State_Code + "&timer='+Math.random());\">" + entity.State_CN + "</td>";
            }
            html += "</tr>";
            html += "</table>";
        }
        return html;
    }

    public string Delivery_City_Select()
    {
        string html = "";
        IList<CityInfo> cityList = null;
        int i = 0;
        cityList = MyAddr.GetCitysByState(tools.CheckStr(Request["state"]));
        if (cityList != null)
        {
            html += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"5\" width=\"100%\">";
            html += "<tr>";
            foreach (CityInfo entity in cityList)
            {
                if (i > 0 && i % 4 == 0)
                {
                    html += "</tr><tr>";
                }
                i = i + 1;
                html += "<td width=\"25%\" onclick=\"$.ajaxSetup({async: false});$('#fee_area').load('/product/ask_do.aspx?action=getdeliveryfee&supplier_id='+$('#product_supplierid').val()+'&city=" + entity.City_Code + "&timer='+Math.random());$('#city_name').html('" + entity.City_CN + "  <img src=/images/arrow_down.jpg align=absmiddle>');$('#city_select').hide();\">" + entity.City_CN + "</td>";
            }
            html += "</tr>";
            html += "</table>";
        }
        return html;
    }

    public string Get_City_DeliveryFee(string city, int supplier_id)
    {
        string html = "";
        double delivery_fee = 0;
        CityInfo cityinfo = MyAddr.GetCityInfoByCode(city);
        if (cityinfo != null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_SupplierID", "=", supplier_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_ID", "in", "select District_DeliveryWayID from Delivery_Way_District where (District_State='' or District_State='" + cityinfo.City_StateCode + "') and (District_City='' or District_City='" + cityinfo.City_Code + "')"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("DeliveryWayInfo.Delivery_Way_Sort", "asc"));
            IList<DeliveryWayInfo> entitys = MyDeliveryWay.GetDeliveryWays(Query, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
            if (entitys != null)
            {
                foreach (DeliveryWayInfo entity in entitys)
                {
                    if (entity.Delivery_Way_FeeType == 1)
                    {
                        delivery_fee = entity.Delivery_Way_InitialFee;
                    }
                    else
                    {
                        delivery_fee = entity.Delivery_Way_Fee;
                    }
                    if (delivery_fee > 0)
                    {
                        html += entity.Delivery_Way_Name + ":" + pub.FormatCurrency(delivery_fee) + " ";
                    }
                    else
                    {
                        html += entity.Delivery_Way_Name + " <img src=\"/images/ico_nofee.jpg\" align=\"absmiddle\"> ";
                    }
                }
            }
            else
            {
                html = "暂不支持该配送区域";
            }
        }
        else
        {
            html = "暂不支持该配送区域";
        }
        return html;
    }

    /// <summary>
    /// 获取产品购买选项
    /// </summary>
    /// <param name="Product_ID">产品编号</param>
    /// <param name="GroupCode">聚合编号</param>
    /// <returns>购买选项信息</returns>
    public string Product_BuyExtend_Show(int Product_ID, int Product_TypeID, string GroupCode)
    {
        string Extend_Str = "";
        string Product_Arry = "0";
        string Extend_Val = "";
        string Extend_list = "";
        IList<ProductExtendInfo> productextend_arry = null;
        if (GroupCode.Length > 0)
        {
            IList<ProductInfo> products;
            IList<ProductExtendInfo> productextends;

            ProductTypeInfo typeinfo = MyType.GetProductTypeByID(Product_TypeID, pub.CreateUserPrivilege("123"));
            if (typeinfo == null)
            {
                return Extend_Str;
            }
            if (typeinfo.ProductTypeExtendInfos == null)
            {
                return Extend_Str;
            }
            products = GetProductByGroupCode(GroupCode);
            if (products != null)
            {
                foreach (ProductInfo productinfo in products)
                {
                    Product_Arry += "," + productinfo.Product_ID;
                }
            }
            else
            {
                return Extend_Str;
            }
            //获取当前产品属性
            productextends = Myproduct.ProductExtendValue(Product_ID);

            //遍历当前产品所属商品类型的所有扩展属性
            foreach (ProductTypeExtendInfo extend in typeinfo.ProductTypeExtendInfos)
            {
                //检查是否为购买选项
                //if (extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Options == 2 && extend.ProductType_Extend_Gather > 0)
                if (extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Options == 2 && extend.ProductType_Extend_Gather > 0)
                {
                    //图片形式选择
                    if (extend.ProductType_Extend_DisplayForm == 2)
                    {
                        Extend_list = Product_BuyExtend_Value(Product_ID, extend.ProductType_Extend_ID, Product_Arry, productextend_arry, 2);
                        if (Extend_list.Length > 0)
                        {
                            //Extend_Str += "<li class=\"li05\">";
                            //Extend_Str += "<span>" + extend.ProductType_Extend_Name + "</span>";
                            //Extend_Str += "<div class=\"li_box\">";
                            //Extend_Str += "<dl><dt>";
                            //Extend_Str += Extend_list;
                            //Extend_Str += "</dt><div class=\"clear\"></div>";
                            //Extend_Str += "</dl>";
                            //Extend_Str += "</div>";
                            //Extend_Str += "</li>";
                            Extend_Str += "<li class=\"li05\">";
                            Extend_Str += "" + extend.ProductType_Extend_Name + "";
                            //Extend_Str += "<div class=\"li_box\">";
                            //Extend_Str += "<dl><dt>";
                            Extend_Str += Extend_list;
                            //Extend_Str += "</dt><div class=\"clear\"></div>";
                            //Extend_Str += "</dl>";
                            //Extend_Str += "</div>";
                            Extend_Str += "</li>";
                        }
                    }
                    else
                    {
                        //文字形式选择
                        Extend_list = Product_BuyExtend_Value(Product_ID, extend.ProductType_Extend_ID, Product_Arry, productextend_arry, 1);
                        if (Extend_list.Length > 0)
                        {
                            //Extend_Str += "<li class=\"li05\">";
                            //Extend_Str += "<span>" + extend.ProductType_Extend_Name + "</span>";
                            //Extend_Str += "<div class=\"li_box\">";
                            //Extend_Str += "<dl><dt>";
                            //Extend_Str += Extend_list;
                            //Extend_Str += "</dt><div class=\"clear\"></div>";
                            //Extend_Str += "</dl>";
                            //Extend_Str += "</div>";
                            //Extend_Str += "</li>";
                            Extend_Str += "<li class=\"li05\">";
                            Extend_Str += "" + extend.ProductType_Extend_Name + "";
                            //Extend_Str += "<div class=\"li_box\">";
                            //Extend_Str += "<dl><dt>";
                            Extend_Str += Extend_list;
                            //Extend_Str += "</dt><div class=\"clear\"></div>";
                            //Extend_Str += "</dl>";
                            //Extend_Str += "</div>";
                            Extend_Str += "</li>";
                        }
                    }


                    //保存当前产品对应购买选项的值
                    if (productextends != null)
                    {
                        foreach (ProductExtendInfo productextend in productextends)
                        {
                            if (productextend.Extent_ID == extend.ProductType_Extend_ID)
                            {

                                if (productextend.Extend_Value.Length > 0)
                                {
                                    if (productextend_arry == null)
                                    {
                                        productextend_arry = new List<ProductExtendInfo>();
                                    }
                                    Extend_Val += " " + productextend.Extend_Value;
                                    productextend_arry.Add(productextend);
                                }

                            }
                        }
                    }
                    Extend_Str += "<div class=\"clear\"></div>";
                }

            }
        }
        Extend_Str += "<input type=\"hidden\" id=\"addname\" name=\"addname\" value=\"" + Extend_Val + "\">";
        return Extend_Str;
    }

    //获取指定购买选项选择值
    public string Product_BuyExtend_Value(int Product_ID, int Extend_ID, string Product_Arry, IList<ProductExtendInfo> Pre_Extends, int Info_Type)
    {
        string Extend_Str = "";
        string Product_NewArry = Product_Arry;
        string Product_Result;
        int i = 0;
        //获取本属性前的各属性均匹配的产品
        QueryInfo Query = new QueryInfo();
        IList<ProductExtendInfo> entitys = null;
        if (Pre_Extends != null)
        {
            Product_NewArry = "0";
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ProductID", "in", Product_Arry));
            entitys = Myproduct.GetProductExtends(Query);
            if (entitys != null)
            {
                foreach (ProductExtendInfo Pre_Extend in Pre_Extends)
                {
                    Product_Result = "0";
                    i = i + 1;
                    foreach (ProductExtendInfo entity in entitys)
                    {

                        if (entity.Extent_ID == Pre_Extend.Extent_ID && ((entity.Extend_Value == Pre_Extend.Extend_Value)))
                        {
                            if (i == 1)
                            {
                                Product_Result += "," + entity.Product_ID;
                            }
                            else
                            {
                                if (("," + Product_NewArry + ",").IndexOf("," + entity.Product_ID + ",") > 0)
                                {
                                    Product_Result += "," + entity.Product_ID;
                                }
                            }
                        }
                    }
                    Product_NewArry = Product_Result;
                }
            }
            if (Product_NewArry == "0")
            {
                return Extend_Str;
            }
        }
        string Exit_Extend = "||";
        //获取各种产品的唯一购买属性
        Query = null;
        Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ProductID", "in", Product_NewArry));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ExtendID", "=", Extend_ID.ToString()));
        entitys = Myproduct.GetProductExtends(Query);
        if (entitys != null)
        {
            foreach (ProductExtendInfo Pre_Extend in entitys)
            {
                //检查属性重复性
                if (Exit_Extend.IndexOf("|" + Pre_Extend.Extend_Value + "|") < 0)
                {
                    if (Info_Type == 1)
                    {
                        //Extend_Str += "<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 1) + "  class=\"opt_off\">" + Pre_Extend.Extend_Value + "</a></dt>";
                        Extend_Str += "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 1) + "  class=\"opt_off\">" + Pre_Extend.Extend_Value + "</a>";
                    }
                    else
                    {
                        //Extend_Str += "<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 2) + "  class=\"opt_off\"><img src=\"" + pub.FormatImgURL(Pre_Extend.Extend_Img, "fullpath") + "\"></a></dt>";
                        Extend_Str += "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 2) + "  class=\"opt_off\"><img src=\"" + pub.FormatImgURL(Pre_Extend.Extend_Img, "fullpath") + "\"></a>";
                    }
                    Exit_Extend += Pre_Extend.Extend_Value + "|";
                }
            }
        }
        return Extend_Str;
    }

    //检查是否为当前产品属性
    public string Get_Extend_Status(int Product_ID, int Extend_ID, string Extend_Val, int Info_Type)
    {
        string status_str = "";
        IList<ProductExtendInfo> Pre_Extends = Myproduct.ProductExtendValue(Product_ID);
        if (Pre_Extends != null)
        {
            foreach (ProductExtendInfo entity in Pre_Extends)
            {
                if (Extend_ID == entity.Extent_ID && ((Extend_Val == entity.Extend_Value)))
                {
                    //status_str = " class=\"opt_on\"";
                    status_str = " class=\"on\"";
                }
            }
        }
        return status_str;
    }

    //获取指定聚合编号的所有产品信息
    public IList<ProductInfo> GetProductByGroupCode(string Group_Code)
    {
        if (Group_Code.Length == 0)
        {
            return null;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        //按类查询
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_GroupCode", "=", Group_Code));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        return Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    }



    /// <summary>
    /// 详细页聚合商品显示
    /// </summary>
    /// <param name="Product_ID"></param>
    /// <param name="Group_Code"></param>
    public void ProductGroup(int Product_ID, string Group_Code)
    {
        StringBuilder strHTML = new StringBuilder();

        IList<ProductInfo> entityList = GetProductByGroupCode(Group_Code);
        if (entityList != null)
        {
            foreach (ProductInfo entity in entityList)
            {
                if (Product_ID == entity.Product_ID)
                    strHTML.Append("<li class=\"on\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" /><span>双色紫</span></a></li>");
                else
                    strHTML.Append("<li ><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" /><span>双色紫</span></a></li>");
            }
        }

        Response.Write(strHTML.ToString());
    }

    #region 优惠信息显示

    //获取优惠卷信息 NEW
    public string Get_Promotion_Policy(int Product_ID)
    {
        string return_value = "";
        IList<PromotionFavorPolicyInfo> FavorPolicys;
        FavorPolicys = MyFavor.GetProductPolicys(Product_ID, "CN");
        if (FavorPolicys != null)
        {
            //return_value += "<span style=\"color:#cc0000;\">";
            foreach (PromotionFavorPolicyInfo entity in FavorPolicys)
            {
                if (entity.Promotion_Policy_Manner == 1)
                {
                    return_value += "购买金额满" + entity.Promotion_Policy_Payline + "元时";
                    return_value += "优惠" + entity.Promotion_Policy_Price + "元";
                }
                if (entity.Promotion_Policy_Manner == 2)
                {
                    return_value += "购买金额满" + entity.Promotion_Policy_Payline + "元时";
                    return_value += "打" + (100 - entity.Promotion_Policy_Percent) / 10 + "折";
                }
                if (entity.Promotion_Policy_Manner == 3)
                {
                    return_value += "购买金额满" + entity.Promotion_Policy_Payline + "元时";
                    PromotionCouponRuleInfo CouponRule = MyCouponRule.GetPromotionCouponRuleByID(entity.Promotion_Policy_CouponRuleID, pub.CreateUserPrivilege("e5a32e42-426a-4202-818a-ad20a980b4cb"));
                    if (CouponRule != null)
                    {
                        if (CouponRule.Coupon_Rule_Price > 0)
                        {
                            return_value += "赠送一张价值" + CouponRule.Coupon_Rule_Price + "元的" + CouponRule.Coupon_Rule_Title;
                        }
                        else
                        {
                            return_value += "赠送一张" + (100 - CouponRule.Coupon_Rule_Percent) / 10 + "折" + CouponRule.Coupon_Rule_Title;
                        }
                    }

                }
                return_value += "<br/>";
            }
            //return_value += "</span>";
        }
        return return_value;
    }

    //获取运费优惠 NEW
    public string Get_Promotion_Gift(int Product_ID)
    {
        string return_value = "";
        int iniamount = 0;
        double inibuyamount = 0;
        IList<PromotionFavorGiftInfo> FavorGifts;
        FavorGifts = MyFavor.GetProductGifts(Product_ID, "CN");
        if (FavorGifts != null)
        {
            foreach (PromotionFavorGiftInfo giftinfo in FavorGifts)
            {
                if (giftinfo.Promotion_Gift_Amounts != null)
                {
                    iniamount = 0;
                    inibuyamount = 0;
                    foreach (PromotionFavorGiftAmountInfo subamount in giftinfo.Promotion_Gift_Amounts)
                    {
                        if (subamount.Gift_Amount_Amount > 0)
                        {
                            if (subamount.Gift_Amount_Amount == iniamount && iniamount > 0)
                            {
                                return_value += "或 ";
                            }
                            else
                            {
                                return_value += "购买数量满" + subamount.Gift_Amount_Amount + "件 ";
                            }
                            return_value += "赠送：";
                            if (subamount.Promotion_Gift_Gifts != null)
                            {
                                foreach (PromotionFavorGiftGiftInfo subgift in subamount.Promotion_Gift_Gifts)
                                {
                                    return_value += GetProductName(subgift.Gift_ProductID);
                                    return_value += " × " + subgift.Gift_Amount + "; ";
                                }
                            }
                            return_value += "<br>";
                        }
                        else
                        {

                            if (subamount.Gift_Amount_BuyAmount == inibuyamount && inibuyamount > 0)
                            {
                                return_value += "或 ";
                            }
                            else
                            {
                                return_value += "购买金额满" + pub.FormatCurrency(subamount.Gift_Amount_BuyAmount) + "元 ";
                            }
                            return_value += "赠送：";
                            if (subamount.Promotion_Gift_Gifts != null)
                            {
                                foreach (PromotionFavorGiftGiftInfo subgift in subamount.Promotion_Gift_Gifts)
                                {
                                    return_value += GetProductName(subgift.Gift_ProductID);
                                    return_value += " × " + subgift.Gift_Amount + "; ";
                                }
                            }
                            return_value += "<br>";
                        }
                        iniamount = subamount.Gift_Amount_Amount;
                        inibuyamount = subamount.Gift_Amount_BuyAmount;
                    }
                }
            }
        }
        return return_value;
    }

    //获取赠品优惠
    public string DEL_Get_Promotion_Gift(int product_id, int product_brand, int isgroupbuy, int islimit)
    {
        string gift_str = "";
        string product_cate = "";
        bool cate_ispatch, brand_isbatch, product_ispatch;
        QueryInfo Query = new QueryInfo();

        Query.PageSize = 0;

        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorGiftInfo.Promotion_Gift_Starttime}, GETDATE())", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorGiftInfo.Promotion_Gift_Endtime}, GETDATE())", "<=", "0"));
        IList<PromotionFavorGiftInfo> entitys = MyGift.GetPromotionFavorGifts(Query, pub.CreateUserPrivilege("15bb07a5-83ee-4157-94c2-693bc4312d74"));
        if (entitys != null)
        {
            foreach (PromotionFavorGiftInfo entity in entitys)
            {
                cate_ispatch = false;
                brand_isbatch = false;
                product_ispatch = false;

                //检查团购与限时排除
                if ((entity.Promotion_Gift_Group == 0 || (entity.Promotion_Gift_Group == 1 && isgroupbuy == 0)) && (entity.Promotion_Gift_Limit == 0 || (entity.Promotion_Gift_Limit == 1 && islimit == 0)))
                {
                    //以分类与品牌为条件
                    if (entity.Promotion_Gift_Products == null)
                    {
                        product_ispatch = true;

                        //判断分类
                        if (entity.Promotion_Gift_Cates != null)
                        {
                            foreach (PromotionFavorGiftCateInfo giftcate in entity.Promotion_Gift_Cates)
                            {
                                product_cate = Myproduct.GetProductCategory(product_id);
                                if (product_cate != "")
                                {
                                    foreach (string subcate in product_cate.Split(','))
                                    {
                                        if (tools.CheckInt(subcate) == giftcate.Favor_CateId)
                                        {
                                            cate_ispatch = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            cate_ispatch = true;
                        }

                        //判断品牌
                        if (entity.Promotion_Gift_Brands != null)
                        {
                            foreach (PromotionFavorGiftBrandInfo giftbrand in entity.Promotion_Gift_Brands)
                            {
                                if (product_brand == giftbrand.Favor_BrandID)
                                {
                                    brand_isbatch = true;
                                }
                            }
                        }
                        else
                        {
                            brand_isbatch = true;
                        }
                    }
                    else
                    {
                        brand_isbatch = true;
                        cate_ispatch = true;

                        //已商品为适用对象条件
                        if (entity.Promotion_Gift_Products != null)
                        {
                            foreach (PromotionFavorGiftProductInfo giftproduct in entity.Promotion_Gift_Products)
                            {
                                if (product_id == giftproduct.Favor_ProductID)
                                {
                                    product_ispatch = true;
                                }
                            }
                        }
                    }


                    //判断以上判断条件全部符合输出各项目赠品
                    if (cate_ispatch == true && brand_isbatch == true && product_ispatch == true)
                    {
                        foreach (PromotionFavorGiftAmountInfo giftamount in entity.Promotion_Gift_Amounts)
                        {
                            gift_str = gift_str + "购买该商品满";
                            if (giftamount.Gift_Amount_Amount > 0)
                            {
                                gift_str = gift_str + giftamount.Gift_Amount_Amount + "件 ";
                            }
                            else
                            {
                                gift_str = gift_str + pub.FormatCurrency(giftamount.Gift_Amount_BuyAmount) + "元 ";
                            }

                            foreach (PromotionFavorGiftGiftInfo giftgift in giftamount.Promotion_Gift_Gifts)
                            {
                                gift_str = gift_str + "赠" + giftgift.Gift_Amount + "件" + GetProductName(giftgift.Gift_ProductID) + ";";
                            }
                            gift_str = gift_str + "<br/>";
                        }
                    }
                }

            }
        }
        return gift_str;
    }

    public string GetProductName(int product_id)
    {
        string product_name = "";
        ProductInfo product = GetProductByID(product_id);

        if (product != null)
        {
            if (product.Product_IsAudit == 1)
            {
                product_name = product.Product_Name;
            }

        }
        return product_name;
    }

    //获取运费优惠信息
    public string DEL_Get_Product_Favor_Fee(int Cate_ID)
    {

        string return_value = "";
        IList<PromotionFavorFeeInfo> entitys;
        IList<PromotionFavorFeeInfo> results;
        string id_arry = "0";
        entitys = MyFavorFee.GetPromotionFavorFeesByCateDistrict(Cate_ID, "0", -1, pub.CreateUserPrivilege("db71e6f9-f858-4469-b45e-b6ab55412853"));
        if (entitys != null)
        {
            foreach (PromotionFavorFeeInfo entity in entitys)
            {
                id_arry = id_arry + "," + entity.Promotion_Fee_ID;
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorFeeInfo.Promotion_Fee_ID", "in", id_arry));

            Query.OrderInfos.Add(new OrderInfo("PromotionFavorFeeInfo.Promotion_Fee_ID", "asc"));
            results = MyFavorFee.GetPromotionFavorFees(Query, pub.CreateUserPrivilege("db71e6f9-f858-4469-b45e-b6ab55412853"));
            if (results != null)
            {
                foreach (PromotionFavorFeeInfo result in results)
                {
                    return_value = return_value + result.Promotion_Fee_Title + "<br/>";
                }
            }
        }
        return return_value;
    }

    public string Get_Product_Favor_Fee(int Product_ID)
    {

        StringBuilder strHTML = new StringBuilder();
        IList<PromotionFavorFeeInfo> feeList = MyFavor.GetProductFees(Product_ID, pub.GetCurrentSite());
        if (feeList != null)
        {
            foreach (PromotionFavorFeeInfo entity in feeList)
            {
                strHTML.Append(entity.Promotion_Fee_Title + ";<br />");
            }
        }

        return strHTML.ToString();
    }

    #endregion

    //捆绑销售
    public string Detail_Package_Product(int Product_ID)
    {

        string product_list = "";
        string Package_Arry = "";
        string Package_Product_Arry = "0";
        double Mkt_Price = 0;
        int package_stock = 0;
        int i = 0;
        ProductStockInfo packagestockinfo = new ProductStockInfo();
        Package_Arry = MyPackage.GetPackageIDByProductID(Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
        if (Package_Arry != "0")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PackageInfo.Package_ID", "in", Package_Arry));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PackageInfo.Package_IsInsale", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("PackageInfo.Package_Sort", "asc"));
            IList<PackageInfo> packages = MyPackage.GetPackages(Query, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
            if (packages != null)
            {
                product_list = product_list + "<div class=\"yz_blk16\">";
                product_list = product_list + "<h2 class=\"yz_title02\">";
                product_list = product_list + "<ul class=\"yz_lst05\">";
                product_list = product_list + "<li class=\"on\" id=\"Li1\">组合套装</li>";
                product_list = product_list + "</ul>";
                product_list = product_list + "<div class=\"clear\"></div>";
                product_list = product_list + "</h2>";
                //product_list = product_list + "<DIV style=\"WIDTH: 730px; HEIGHT: 170px; OVERFLOW: hidden;\" id=\"packagediv\">";
                product_list = product_list + "<DIV style=\"OVERFLOW: hidden;\" id=\"packagediv\">";
                product_list = product_list + "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">";
                product_list = product_list + "<tr><td height=\"20\"></td></tr><tr><td>";

                int SupplierID;
                bool ErrorPackage = false;

                foreach (PackageInfo entity in packages)
                {
                    i = 0;
                    Package_Product_Arry = "0";
                    Mkt_Price = 0;
                    if (entity.PackageProductInfos != null)
                    {
                        foreach (PackageProductInfo obj in entity.PackageProductInfos)
                        {
                            Package_Product_Arry = Package_Product_Arry + "," + obj.Package_Product_ProductID;
                        }
                        Query.PageSize = 0;
                        Query.CurrentPage = 1;
                        Query.ParamInfos.Clear();
                        Query.OrderInfos.Clear();
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Package_Product_Arry));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
                        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                        IList<ProductInfo> products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (products != null)
                        {

                            #region 商品发货方处理

                            ErrorPackage = false;
                            SupplierID = -1;
                            foreach (ProductInfo E in products)
                            {
                                if (E.U_Product_Shipper == 1)
                                    E.Product_SupplierID = 0;

                                if (SupplierID != -1 && SupplierID != E.Product_SupplierID)
                                {
                                    ErrorPackage = true;
                                    break;
                                }

                                SupplierID = E.Product_SupplierID;
                            }

                            if (ErrorPackage) continue;

                            #endregion

                            product_list = product_list + "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">";
                            product_list = product_list + "<tr><td class=\"detail_package\"><ul>";

                            foreach (ProductInfo obj in products)
                            {
                                i = i + 1;
                                foreach (PackageProductInfo productobj in entity.PackageProductInfos)
                                {
                                    if (obj.Product_ID == productobj.Package_Product_ProductID)
                                    {
                                        Mkt_Price += (obj.Product_MKTPrice * productobj.Package_Product_Amount);
                                    }

                                }

                                if (i <= 4)
                                {
                                    product_list = product_list + "<li><div><a href=\"" + pageurl.FormatURL(pageurl.product_detail, obj.Product_ID.ToString()) + "\" target=\"_blank\"><img  src=\"" + pub.FormatImgURL(obj.Product_Img, "thumbnail") + "\" border=\"0\" onload=\"javascript:AutosizeImage(this,90,90);\" width=\"90\" height=\"90\" alt=\"" + obj.Product_Name + "\" /></a></div>";
                                    product_list = product_list + "<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, obj.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + obj.Product_Name + "\" style=\"font-size:12px; font-weight:normal;\">" + tools.Left(obj.Product_Name, 15) + "</a></p>";
                                    product_list = product_list + "</li>";
                                    if (i < 4 && i < products.Count)
                                    {
                                        product_list = product_list + "<li class=\"package_gap\">+</li>";
                                    }
                                }
                            }
                            product_list = product_list + "</ul>";
                            product_list = product_list + "</td><td width=\"140\" height=\"140\" align=\"left\" class=\"detail_package_intro\">";
                            product_list = product_list + "<span style=\"color:#cc0000;\">" + entity.Package_Name + "</span><br>";
                            product_list = product_list + "易种价：<span class=\"price_original\">" + pub.FormatCurrency(Mkt_Price) + "元</span><br>";
                            product_list = product_list + "套装价：<span class=\"price_small\">" + pub.FormatCurrency(Get_Member_Price(0, entity.Package_Price)) + "元</span><br>";
                            product_list = product_list + "节省：<span class=\"price_small\">" + pub.FormatCurrency(Mkt_Price - Get_Member_Price(0, entity.Package_Price)) + "元</span><br>";

                            packagestockinfo = Get_Package_Stock(entity.PackageProductInfos);
                            package_stock = packagestockinfo.Product_Stock_Amount;

                            //库存大于0或为零库存套装
                            if ((package_stock > 0 || packagestockinfo.Product_Stock_IsNoStock == 1) && Check_Package_Valid(entity.PackageProductInfos))
                            {
                                product_list = product_list + "<a href=\"/cart/cart_do.aspx?action=add&package_id=" + entity.Package_ID + "\" target=\"_blank\"><img border=\"none\" class=\"buy\" src=\"/images/butbuy.jpg\" alt=\"购买\" width=\"64\" height=\"24\" /></a>";
                            }
                            else
                            {
                                product_list = product_list + "<img border=\"none\" class=\"buy\" src=\"/images/unbutbuy.gif\" alt=\"购买\" />";
                            }


                            product_list = product_list + "</td></tr>";
                            product_list = product_list + "</table>";
                        }
                    }
                }
                product_list = product_list + "</td></tr>";
                product_list = product_list + "</table>";
                product_list = product_list + "</div>";
                product_list = product_list + "</div>";
                product_list = product_list + "<script type=\"text/javascript\">";
                product_list = product_list + "var marquee=new Marquee(\"packagediv\");";
                product_list = product_list + "marquee.Direction=\"up\";";
                product_list = product_list + "marquee.Step=1;";
                product_list = product_list + "marquee.Width=730;";
                product_list = product_list + "marquee.Height=160;";
                product_list = product_list + "marquee.Timer=20;";
                product_list = product_list + "marquee.ScrollStep=1;";
                product_list = product_list + "marquee.Start();";
                product_list = product_list + "</script>";

            }
        }

        return product_list;
    }

    //商品扩展属性
    public string Product_Extend_Content(int Product_ID)
    {
        string extend_list = "";
        IList<ProductExtendInfo> productextends = Myproduct.ProductExtendValue(Product_ID);
        if (productextends != null)
        {
            extend_list += "<table cellspacing=\"1\" cellpadding=\"5\" border=\"0\" style=\"table-layout:fixed; margin-top:10px; margin-bottom:16px; background-color:#C1DBB6; color:#000000; line-height:32px;\" width=\"723\">";
            foreach (ProductExtendInfo entity in productextends)
            {

                if (tools.NullStr(entity.Extend_Value) != "")
                {
                    ProductTypeExtendInfo extend = MyExtend.GetProductTypeExtendByID(entity.Extent_ID);
                    if (extend != null && extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Gather == 0)
                    {
                        extend_list = extend_list + "<tr><td width=\"120\" bgcolor=\"#F9F9F9\">" + extend.ProductType_Extend_Name + "</td><td bgcolor=\"#F9F9F9\">" + entity.Extend_Value + "</td></tr>";
                    }
                }
            }
            extend_list += "</table>";
        }

        return extend_list;
    }



    //商品扩展属性
    public string Product_Extend_Content_New(int Product_ID)
    {
        string extend_list = "";
        IList<ProductExtendInfo> productextends = Myproduct.ProductExtendValue(Product_ID);
        if (productextends != null)
        {
            //extend_list += "<table cellspacing=\"1\" cellpadding=\"5\" border=\"0\" style=\"table-layout:fixed; margin-top:10px; margin-bottom:16px; background-color:#C1DBB6; color:#000000; line-height:32px;\" width=\"723\">";
            foreach (ProductExtendInfo entity in productextends)
            {

                if (tools.NullStr(entity.Extend_Value) != "")
                {
                    ProductTypeExtendInfo extend = MyExtend.GetProductTypeExtendByID(entity.Extent_ID);
                    if (extend != null && extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Gather == 0)
                    {
                        //extend_list = extend_list + "<tr><td width=\"120\" bgcolor=\"#F9F9F9\">" + extend.ProductType_Extend_Name + "</td><td bgcolor=\"#F9F9F9\">" + entity.Extend_Value + "</td></tr>";
                        extend_list = extend_list + "<p>" + extend.ProductType_Extend_Name + "：" + entity.Extend_Value + "</p>";
                    }
                }
            }
            //extend_list += "</table>";
        }

        return extend_list;
    }

    //获取捆绑商品库存
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (products != null)
        {
            foreach (PackageProductInfo packproduct in packageproducts)
            {
                foreach (ProductInfo product in products)
                {
                    if (packproduct.Package_Product_ProductID == product.Product_ID)
                    {
                        //只统计非零库存商品库存
                        if (product.Product_IsNoStock == 0)
                        {
                            IsNoStock = false;
                            cur_stock = (int)(product.Product_UsableAmount / packproduct.Package_Product_Amount);
                            if (cur_stock == 0)
                            {
                                cur_stock = -1;
                            }
                        }
                    }
                }
                if (cur_stock < 0)
                {
                    nostock = true;
                }
                if ((Package_Stock > cur_stock || Package_Stock == 0) && cur_stock > 0)
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

    //检查捆绑商品合法性
    public bool Check_Package_Valid(IList<PackageProductInfo> packageproducts)
    {
        bool IsValid = true;
        ProductInfo entity = null;
        foreach (PackageProductInfo obj in packageproducts)
        {
            entity = Myproduct.GetProductByID(obj.Package_Product_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
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

    //获取捆绑商品市场价
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (products != null)
        {
            foreach (ProductInfo product in products)
            {
                Package_MKTPrice = Package_MKTPrice + product.Product_MKTPrice;
            }

        }

        return Package_MKTPrice;
    }

    //商品详情相关商品排行
    public string Left_Relate_Product_back(int Show_Num, int Cate_ID)
    {
        string product_list = "";

        int i = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_CateID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products != null)
        {
            product_list = product_list + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"220\" class=\"product_left_cate_border\">";
            product_list = product_list + "<tr><td colspan=\"2\" class=\"product_left_cate_tit\"><img align=\"absmiddle\" src=\"/images/ico_arrow.gif\" /> &nbsp; 相关商品排行</td></tr>";
            product_list = product_list + "<tr><td height=\"5\" colspan=\"2\"></td></tr>";
            product_list = product_list + "<tr><td>";
            product_list = product_list + "<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" border=\"0\" width=\"210\">";
            foreach (ProductInfo entity in Products)
            {
                i = i + 1;
                if (i == 1)
                {
                    product_list = product_list + "<tr><td width=\"25\" align=\"center\" class=\"foot_dash\"><img src=\"/images/ico_" + i.ToString() + ".jpg\" /></td><td height=\"50\" class=\"foot_dash\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" width=\"50\" border=\"0\" heght=\"50\" /></a></td><td align=\"left\" width=\"140\" class=\"foot_dash\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + entity.Product_Name + "</a></td></tr>";
                }
                else
                {
                    product_list = product_list + "<tr><td width=\"25\" align=\"center\" class=\"foot_dash\"><img src=\"/images/ico_" + i.ToString() + ".jpg\" /></td><td height=\"25\" colspan=\"2\" class=\"foot_dash\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + entity.Product_Name + "</a> <span class=\"t12_orange\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></td></tr>";
                }
            }
            product_list = product_list + "</table>";
            product_list = product_list + "</td></tr>";
            product_list = product_list + "<tr><td height=\"5\" colspan=\"2\"></td></tr>";
            product_list = product_list + "</table>";

        }
        return product_list;


    }

    //商品详情相关商品排行
    public string Left_Relate_Product(int Show_Num, int Cate_ID)
    {
        string product_list = "";

        int i = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_CateID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products != null)
        {
            product_list = product_list + "<div id=\"che2left1\">";
            product_list = product_list + "<h3>猜您还喜欢</h3>";
            product_list = product_list + "<div id=\"gou-list\">";
            product_list = product_list + "<ul style=\" padding-bottom:5px;\">";
            foreach (ProductInfo entity in Products)
            {
                product_list = product_list + "<li><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" width=\"45\" height=\"45\" onload=\"javascript:AutosizeImage(this,45,45);\" title=\"" + entity.Product_Name + "\" target=\"_blank\" /></a><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 30) + "</a></p><p class=\"rr\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</p>";

            }
            product_list = product_list + "</ul>";
            product_list = product_list + "</div></div>";

        }
        return product_list;
    }

    //商品详情左侧特价排行
    public string Left_SpecPrice_Product(int Show_Num)
    {
        string product_list = "";
        int i = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products != null)
        {
            product_list = product_list + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"product_left_cate_border\">";
            product_list = product_list + "<tr><td colspan=\"2\" class=\"product_left_cate_tit\"><img align=\"absmiddle\" src=\"/images/ico_arrow.gif\" /> &nbsp;特价商品推荐</td></tr>";
            product_list = product_list + "<tr><td height=\"5\" colspan=\"2\"></td></tr>";
            product_list = product_list + "<tr><td>";
            product_list = product_list + "<table cellpadding=\"5\" cellspacing=\"0\" align=\"center\" border=\"0\" width=\"200\">";
            product_list = product_list + "<tr>";
            foreach (ProductInfo entity in Products)
            {
                i = i + 1;
                product_list = product_list + "<td align=\"left\" width=\"100\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" width=\"90\" border=\"0\" heght=\"90\" class=\"img_border\" alt=\"" + entity.Product_Name + "\" onload=\"AutosizeImage(this,90,90)\" /></a></td>";
                if (i % 2 == 0)
                {
                    product_list = product_list + "</tr><tr>";
                }

            }
            product_list = product_list + "</tr></table>";
            product_list = product_list + "</td></tr>";
            product_list = product_list + "<tr><td height=\"5\" colspan=\"2\"></td></tr>";
            product_list = product_list + "</table>";

        }
        return product_list;
    }

    //发送推荐邮件
    public void Product_SendRecommend()
    {
        string mailto, mailname, mailtitle, mailcontent;
        int flag;
        mailto = tools.CheckStr(Request.Form["email"].Trim());
        if (tools.CheckEmail(mailto) == false)
        {
            pub.Msg("info", "信息提示", "请输入有效的邮件地址!", false, "{back}");
        }
        mailname = tools.CheckStr(Request.Form["username"].Trim());
        mailtitle = tools.CheckStr(Request.Form["title"].Trim());
        mailcontent = Request.Form["content"];

        if (mailname == "" || mailtitle == "" || mailcontent == "")
        {
            pub.Msg("info", "信息提示", "请将所有信息填写完整!", false, "{back}");
        }

        flag = pub.Sendmail(mailto, mailtitle, "来自" + mailname + "通过" + Application["site_name"] + "发给你的邮件：", mailcontent);
        if (flag == 1)
        {
            pub.Msg("positive", "操作成功", "邮件发送成功!", true, "/index.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "邮件发送失败!", false, "{back}");
        }
    }

    //到货通知添加
    public virtual void AddProductNotify()
    {
        int Product_Notify_ID = 0;
        int Product_Notify_MemberID = tools.NullInt(Session["member_id"]);
        string Product_Notify_Email = tools.CheckStr(Request.Form["email_addr"]);
        int Product_Notify_ProductID = tools.CheckInt(Request.Form["pid"]);
        int Product_Notify_IsNotify = 0;
        DateTime Product_Notify_Addtime = DateTime.Now;

        if (tools.CheckEmail(Product_Notify_Email) == false)
        {
            pub.Msg("error", "错误信息", "请输入正确的邮件地址！", false, "{back}");
        }

        ProductNotifyInfo entity = new ProductNotifyInfo();
        entity.Product_Notify_ID = Product_Notify_ID;
        entity.Product_Notify_MemberID = Product_Notify_MemberID;
        entity.Product_Notify_Email = Product_Notify_Email;
        entity.Product_Notify_ProductID = Product_Notify_ProductID;
        entity.Product_Notify_IsNotify = Product_Notify_IsNotify;
        entity.Product_Notify_Addtime = Product_Notify_Addtime;
        entity.Product_Notify_Site = "CN";

        if (Mynotify.AddProductNotify(entity))
        {
            pub.Msg("positive", "操作成功", "信息提交成功", true, "/index.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //缺货登记添加
    public void Stock_Out_Add()
    {
        string Trade_Verify = tools.CheckStr(Request["verifycode"]);
        if (Trade_Verify != tools.NullStr(Session["Trade_Verify"]))
        {
            pub.Msg("info", "信息提示", "验证码输入错误!", false, "{back}");
        }
        string stockout_product_name = tools.CheckStr(Request.Form["stockout_product_name"].ToString().Trim());
        string stockout_product_describe = tools.CheckStr(Request.Form["stockout_product_describe"]);
        string stockout_member_name = tools.CheckStr(Request.Form["stockout_member_name"]);
        string stockout_member_tel = tools.CheckStr(Request.Form["stockout_member_tel"]);
        string stockout_member_email = tools.CheckStr(Request.Form["stockout_member_email"]);

        if (stockout_product_name == "")
        {
            pub.Msg("info", "信息提示", "请输入要登记的商品名称!", false, "{back}");
        }

        StockoutBookingInfo stockoutbook = new StockoutBookingInfo();
        stockoutbook.Stockout_Product_Name = stockout_product_name;
        stockoutbook.Stockout_Product_Describe = stockout_product_describe;
        stockoutbook.Stockout_Member_Name = stockout_member_name;
        stockoutbook.Stockout_Member_Tel = stockout_member_tel;
        stockoutbook.Stockout_Member_Email = stockout_member_email;
        stockoutbook.Stockout_IsRead = 0;
        stockoutbook.Stockout_Addtime = DateTime.Now;
        stockoutbook.Stockout_Site = "CN";

        if (Mysotckout.AddStockoutBooking(stockoutbook, pub.CreateUserPrivilege("342a2ee7-c8eb-4ed6-8ecc-eac99e7623ff")))
        {
            pub.Msg("positive", "操作成功", "缺货登记提交成功!", true, "/index.aspx");
        }
        else
        {
            pub.Msg("positive", "操作成功", "缺货登记提交失败!", false, "{back}");
        }
    }

    //品牌列表
    public void getProductBrands()
    {
        int cate_id = tools.CheckInt(Request["cate_id"]);
        string nav_tit = "推荐品牌";
        CategoryInfo cateinfo = GetCategoryByID(cate_id);
        if (cateinfo != null)
        {
            nav_tit = cateinfo.Cate_Name + "品牌";
        }
        else
        {
            cate_id = 0;
        }
        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = 0;
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
        if (cate_id > 0)
        {
            string subCates = Get_All_SubCate(cate_id);
            Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_ID", "in", "select distinct product_brandid from product_basic where Product_IsInsale=1 and  Product_IsAudit=1 and (product_cateid in (" + subCates + ") or product_id in (SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")))"));
        }
        else
        {
            Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsRecommend", "=", "1"));
        }
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_IsActive", "=", "1"));
        Query1.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "asc"));
        IList<BrandInfo> entitys = MyBrand.GetBrands(Query1, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        if (entitys != null)
        {

            //Response.Write("<div class=\"prolist_right3\" style=\"margin-bottom:10px;\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px\">&nbsp;" + nav_tit + "</td></tr></table></h3>");
            //Response.Write("<div class=\"brand_list\">");
            //Response.Write("<ul>");
            if (entitys != null)
            {
                foreach (BrandInfo entity in entitys)
                {
                    Response.Write("<li><a href=\"/product/category.aspx?brand_id=" + entity.Brand_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Brand_Img, "fullpath") + "\" width=\"120\" height=\"70\" onload=\"javascript:AutosizeImage(this,120,70);\"/></a></li>");
                }
            }
            //Response.Write("</ul>");
            //Response.Write("</div>");
            //Response.Write("<div class=\"clear\"></div></div>");
        }

    }

    /// <summary>
    /// 品牌频道展示
    /// </summary>
    /// <param name="Cate_ID">所属分类</param>
    /// <param name="IsRecommend">是否推荐</param>
    /// <param name="Shownum">显示数量</param>
    public void Brands_Recommend(int Cate_ID, int IsRecommend, int Shownum)
    {
        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = Shownum;
        Query1.CurrentPage = 1;
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", pub.GetCurrentSite()));
        if (IsRecommend == 1)
        {
            Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsRecommend", "=", "1"));
        }
        if (Cate_ID > 0)
        {
            string subCates = Get_All_SubCate(Cate_ID);
            Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_ID", "in", "select distinct product_brandid from product_basic where Product_IsInsale=1 and  Product_IsAudit=1 and (product_cateid in (" + subCates + ") or product_id in (SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")))"));
        }
        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
        Query1.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "asc"));
        IList<BrandInfo> entitys = MyBrand.GetBrands(Query1, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        if (entitys != null)
        {
            Response.Write("<ul>");
            if (entitys != null)
            {
                foreach (BrandInfo entity in entitys)
                {
                    Response.Write("<li><a href=\"/product/category.aspx?brand_id=" + entity.Brand_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Brand_Img, "fullpath") + "\" width=\"120\" height=\"70\" onload=\"javascript:AutosizeImage(this,120,70);\" alt=\"" + entity.Brand_Name + "\" title=\"" + entity.Brand_Name + "\" /></a></li>");
                }
            }
            Response.Write("</ul>");

        }

    }

    public void Brands_Cate_List()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                Response.Write("<div class=\"brand_layertit\"><h3>" + entity.Cate_Name + "品牌</h3><a href=\"brand_more.aspx?cate_id=" + entity.Cate_ID + "\">更多>></a></div>");
                Response.Write("<div class=\"brand_layerlist\">");
                Response.Write("    <div class=\"brand_recmdlist_left\">");
                Brands_Recommend(entity.Cate_ID, 0, 10);
                Response.Write("    </div>");
                Response.Write("    <div class=\"brand_recmdlist_right\"><div>" + entity.Cate_Name + "品牌推荐产品</div>");
                Response.Write(adclass.AD_Show("Brand_Right_AD", entity.Cate_ID.ToString(), "cycle", 1));
                Response.Write("    </div>");
                Response.Write("</div><div class=\"clear\"></div>");

            }
        }
    }

    //网站地图列表
    public void Site_Map()
    {
        IList<CategoryInfo> entitys;
        QueryInfo Query = new QueryInfo();
        QueryInfo Query1;
        QueryInfo Query2;
        IList<CategoryInfo> subentitys;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo Category in Categorys)
            {

                Response.Write("<div class=\"prolist_right3\" style=\"margin-bottom:10px;\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px;cursor:pointer;\" onclick=\"location='" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(Category.Cate_ID.ToString(), "", "", "", "")) + "';\">&nbsp;" + Category.Cate_Name + "</td></tr></table></h3>");
                Query1 = null;
                Query1 = new QueryInfo();
                Query1.PageSize = 0;
                Query1.CurrentPage = 1;
                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + Category.Cate_ID + ""));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
                Query1.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
                entitys = MyCate.GetCategorys(Query1, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                if (entitys != null)
                {
                    foreach (CategoryInfo entity in entitys)
                    {
                        Response.Write("<div class=\"map_tit\">");
                        Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity.Cate_ID.ToString(), "", "", "", "")) + "\" target=\"_blank\" class=\"a_t12_blue\">" + entity.Cate_Name + "</a>");
                        Response.Write("</div>");

                        Query2 = null;
                        Query2 = new QueryInfo();
                        Query2.PageSize = 0;
                        Query2.CurrentPage = 1;
                        Query2.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + entity.Cate_ID + ""));
                        Query2.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
                        Query2.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
                        Query2.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
                        subentitys = MyCate.GetCategorys(Query2, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                        if (subentitys != null)
                        {
                            Response.Write("<div class=\"map_item\">");
                            foreach (CategoryInfo subentity in subentitys)
                            {

                                Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(subentity.Cate_ID.ToString(), "", "", "", "")) + "\" target=\"_blank\" class=\"a_t12_blue\">" + subentity.Cate_Name + "</a> | ");

                            }
                            Response.Write("</div>");
                        }
                    }

                }
                Response.Write("<div class=\"clear\"></div></div>");
            }
        }


    }

    /// <summary>
    /// 获得竞价商品
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <returns></returns>
    public void GetBiddingProduct(string keyword, ref IList<ProductInfo> entityList)
    {
        Supplier supplier = new Supplier();

        ProductInfo entity;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_KeywordID", "in", "select distinct Keyword_ID from KeywordBidding_Keyword where Keyword_Name like '%" + keyword + "%'"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_Audit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{KeywordBiddingInfo.KeywordBidding_StartDate}, GETDATE())", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{KeywordBiddingInfo.KeywordBidding_EndDate}, GETDATE())", "<=", "0"));
        Query.OrderInfos.Add(new OrderInfo("KeywordBiddingInfo.KeywordBidding_Price", "Desc"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.NewID()", ""));
        IList<KeywordBiddingInfo> entitys = MyKeywordBidding.GetKeywordBiddings(Query, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
        int KeywordBidding_ID = 0;
        if (entitys != null)
        {
            foreach (KeywordBiddingInfo entity1 in entitys)
            {
                if (entity1.KeywordBidding_Price >
                    supplier.GetSupplierAdvAccount(entity1.KeywordBidding_SupplierID)) continue;

                KeywordBidding_ID = entity1.KeywordBidding_ID;
                entity = GetProductByID(entity1.KeywordBidding_ProductID);
                if (entity != null && entity.Product_IsInsale == 1 && entity.Product_IsAudit == 1 && entity.Product_Site == pub.GetCurrentSite())
                {

                    entity.Product_Sort = KeywordBidding_ID;
                    entityList.Add(entity);
                }
            }
        }

        supplier = null;
    }

    /// <summary>
    /// 更新关键词竞价展示次数
    /// </summary>
    /// <param name="KeywordBidding_ID">关键词竞价编号</param>
    public void UpdateBiddingShow(int KeywordBidding_ID)
    {
        KeywordBiddingInfo entity = MyKeywordBidding.GetKeywordBiddingByID(KeywordBidding_ID, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
        if (entity != null)
        {
            entity.KeywordBidding_ShowTimes = entity.KeywordBidding_ShowTimes + 1;
            MyKeywordBidding.EditKeywordBidding(entity);
        }
    }

    /// <summary>
    /// 关键词虚拟余额处理
    /// </summary>
    /// <param name="KeywordBidding_ID">关键词竞价申请信息</param>
    /// <param name="Supplier_ID">发布供应商编号</param>
    public void BiddingAccounting(int KeywordBidding_ID, int Supplier_ID)
    {
        Supplier supplier = new Supplier();
        KeywordBiddingInfo BiddingEntity = supplier.GetKeywordBidding(KeywordBidding_ID);
        if (BiddingEntity != null)
        {
            supplier.Supplier_Account_Log(Supplier_ID, 2, Math.Round(0 - BiddingEntity.KeywordBidding_Price, 2), "关键词竞价扣费");
        }
        supplier = null;
    }


    public string GuessLikeProduct_new(string Tag_Name, int Show_Num, int Tag_ID)
    {
        StringBuilder strHTML = new StringBuilder();


        IList<ProductInfo> productinfos = GetCateTagProduct_TagID(Show_Num, 0, Tag_ID);
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



                //strHTML.Append("   <p>规格：" + productinfo.Product_Spec + "</p>                        ");
                strHTML.Append("   <p>售价：<strong>" + pub.FormatCurrency(pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "</strong></p></li>                ");

            }
        }
        strHTML.Append("</ul>");

        return strHTML.ToString();
    }
    #endregion

    #region 新品上线

    public int GetNewProductCount(int cate_id)
    {
        IList<ProductInfo> listProduct = null;
        int count = 0;
        int brand_id;
        int Extend_ID;
        string Cate_Arry = "";
        string Extend_ProductArry = "";

        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        string collection_array = "";
        int collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id > 0)
        {
            collection_array = Get_All_SubCate(collection_id);
        }

        string cateprice_array = "";
        int cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id > 0)
        {
            cateprice_array = Get_All_SubCate(cateprice_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        //按款式查询
        if (collection_id > 0 && collection_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
        }

        //按价格查询
        if (cateprice_id > 0 && cateprice_array.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
        }

        brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                    }
                }
            }
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (listProduct != null)
        {
            count = listProduct.Count;
        }
        else
        {
            Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

            if (cate_id > 0 && Cate_Arry.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }

            //按款式查询
            if (collection_id > 0 && collection_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
            }

            //按价格查询
            if (cateprice_id > 0 && cateprice_array.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
            }

            brand_id = tools.CheckInt(Request["brand_id"]);
            if (brand_id > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
            }

            foreach (string querystring in Request.QueryString)
            {
                if (querystring != null)
                {
                    if (querystring.IndexOf("filter_") >= 0)
                    {
                        Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                        if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                        {
                            Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                        }
                    }
                }
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (listProduct != null)
            {
                count = listProduct.Count;
            }
        }
        return count;
    }

    public void NewProduct_List(string uses, int cate_id, int irowmax)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ProductInfo> listProduct = null;
        PageInfo pageInfo = null;

        int Extend_ID;
        int i = 0;
        int brand_id;
        string Cate_Arry = "";
        string Extend_ProductArry = "";
        string targetUrl = "";
        string page_url = "?action=list";
        int pagesize = 20;
        int curr_page = tools.CheckInt(Request["page"]);
        if (curr_page <= 0)
        {
            curr_page = 1;
        }

        if (cate_id > 0)
        {
            //获取子类
            Cate_Arry = Get_All_SubCate(cate_id);
        }

        string collection_array = "";
        int collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id > 0)
        {
            collection_array = Get_All_SubCate(collection_id);
        }

        string cateprice_array = "";
        int cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id > 0)
        {
            cateprice_array = Get_All_SubCate(cateprice_id);
        }

        string supplier_name = "";
        SupplierInfo supplierInfo = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = pagesize;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.AddDays(-7) + "',{ProductInfo.Product_Addtime})", ">=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now + "',{ProductInfo.Product_Addtime})", "<=", "0"));
        if (cate_id > 0 && Cate_Arry.Length > 0)
        {
            page_url = page_url + "&cate_id=" + cate_id;
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
        }

        //按款式查询
        if (collection_id > 0 && collection_array.Length > 0)
        {
            page_url = page_url + "&collection_id=" + collection_id;
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
        }

        //按价格查询
        if (cateprice_id > 0 && cateprice_array.Length > 0)
        {
            page_url = page_url + "&cateprice_id=" + cateprice_id;
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
        }

        brand_id = tools.CheckInt(Request["brand_id"]);
        if (brand_id > 0)
        {
            page_url = page_url + "&brand_id=" + brand_id;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }

        foreach (string querystring in Request.QueryString)
        {
            if (querystring != null)
            {
                if (querystring.IndexOf("filter_") >= 0)
                {
                    Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                    if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                    {
                        Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                        page_url = page_url + "&" + querystring + "=" + Request.QueryString[querystring];
                    }
                }
            }
        }

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

        listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        strHTML.Append(" <ul>");
        if (listProduct != null)
        {
            foreach (ProductInfo entity in listProduct)
            {
                i++;
                targetUrl = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                supplierInfo = Mysupplier.GetSupplierByID(entity.Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierInfo != null)
                {
                    supplier_name = supplierInfo.Supplier_CompanyName;
                }

                if (i % 4 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<div class=\"img_box\"><a href=\"" + targetUrl + "\" target=\"_blank\"><img title=\"" + entity.Product_Name + "\" src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>24小时备货</i><i>新品</i></span></div>");
                strHTML.Append("<div class=\"p_box\">");
                strHTML.Append(Product_List_WholeSalePrice(entity));
                strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + targetUrl + "\" target=\"_blank\" style=\"overflow:hidden;height:15px;display:block;\" title=\"" + entity.Product_Name + "\">" + entity.Product_Name + "</a></div>");
                strHTML.Append("<div class=\"p_box_info03\">");
                strHTML.Append("<p><a href=\"" + pub.ReturnShopUrl(entity.Product_SupplierID) + "\" target=\"_blank\">" + supplier_name + "</a><span style=\"float:right;cursor:pointer;\" onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img src=\"/images/icon04.png\" style=\"width:74px;margin-left:30px;display:inline;vertical-align: middle;\"></span></p>");
                strHTML.Append("<div class=\"p_box_info03_fox\">");
                strHTML.Append("<i></i>");
                strHTML.Append(Product_List_SupplierInfo(supplierInfo));
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append("<div class=\"p_box_info04\"><a href=\"/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "\" onclick=\"javascript:return AddCartExt(this);\" class=\"a16\">计入进货单</a><a  href=\"javascript:;\" onclick=\"favorites_add_ajax(" + entity.Product_ID + ",'product');\" class=\"a17\">加入收藏</a></div>");
                strHTML.Append("</div>");
                strHTML.Append("</li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div>");
            Response.Write(strHTML.ToString());
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {
            page_url = "?action=list";
            Query = new QueryInfo();
            Query.PageSize = pagesize;
            Query.CurrentPage = curr_page;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            //聚合是否列表显示 暂时屏蔽掉
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));

            if (cate_id > 0 && Cate_Arry.Length > 0)
            {
                page_url = page_url + "&cate_id=" + cate_id;
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", Cate_Arry));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + Cate_Arry + ")"));
            }

            //按款式查询
            if (collection_id > 0 && collection_array.Length > 0)
            {
                page_url = page_url + "&collection_id=" + collection_id;
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", collection_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + collection_array + ")"));
            }

            //按价格查询
            if (cateprice_id > 0 && cateprice_array.Length > 0)
            {
                page_url = page_url + "&cateprice_id=" + cateprice_id;
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", cateprice_array));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + cateprice_array + ")"));
            }

            brand_id = tools.CheckInt(Request["brand_id"]);
            if (brand_id > 0)
            {
                page_url = page_url + "&brand_id=" + brand_id;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
            }

            foreach (string querystring in Request.QueryString)
            {
                if (querystring != null)
                {
                    if (querystring.IndexOf("filter_") >= 0)
                    {
                        Extend_ID = tools.CheckInt(tools.NullStr(querystring.Substring(7)));
                        if (tools.NullStr(Request.QueryString[querystring]) != "" && Extend_ID > 0)
                        {
                            Extend_ProductArry = Myproduct.GetExtendProductID(Extend_ID, tools.CheckStr(Request.QueryString[querystring]));
                            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Extend_ProductArry));
                            page_url = page_url + "&" + querystring + "=" + Request.QueryString[querystring];
                        }
                    }
                }
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "desc"));

            listProduct = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            pageInfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

            strHTML.Append(" <ul>");
            if (listProduct != null)
            {
                foreach (ProductInfo entity in listProduct)
                {
                    i++;
                    targetUrl = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                    supplierInfo = Mysupplier.GetSupplierByID(entity.Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierInfo != null)
                    {
                        supplier_name = supplierInfo.Supplier_CompanyName;
                    }

                    if (i % 4 == 0)
                    {
                        strHTML.Append("<li class=\"mr0\">");
                    }
                    else
                    {
                        strHTML.Append("<li>");
                    }
                    strHTML.Append("<div class=\"img_box\"><a href=\"" + targetUrl + "\" target=\"_blank\"><img title=\"" + entity.Product_Name + "\" src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>24小时备货</i><i>新品</i></span></div>");
                    strHTML.Append("<div class=\"p_box\">");
                    strHTML.Append(Product_List_WholeSalePrice(entity));
                    strHTML.Append("<div class=\"p_box_info02\"><a href=\"" + targetUrl + "\" target=\"_blank\" style=\"overflow:hidden;height:15px;display:block;\" title=\"" + entity.Product_Name + "\">" + entity.Product_Name + "</a></div>");
                    strHTML.Append("<div class=\"p_box_info03\">");
                    strHTML.Append("<p><a href=\"" + pub.ReturnShopUrl(entity.Product_SupplierID) + "\" target=\"_blank\">" + supplier_name + "</a><span style=\"float:right;cursor:pointer;\" onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img src=\"/images/icon04.png\" style=\"width:74px;margin-left:30px;display:inline;vertical-align: middle;\"></span></p>");
                    strHTML.Append("<div class=\"p_box_info03_fox\">");
                    strHTML.Append("<i></i>");
                    strHTML.Append(Product_List_SupplierInfo(supplierInfo));
                    strHTML.Append("</div>");
                    strHTML.Append("</div>");
                    strHTML.Append("<div class=\"p_box_info04\"><a href=\"/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "\" onclick=\"javascript:return AddCartExt(this);\" class=\"a16\">计入进货单</a><a  href=\"javascript:;\" onclick=\"favorites_add_ajax(" + entity.Product_ID + ",'product');\" class=\"a17\">加入收藏</a></div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</li>");
                }
                strHTML.Append("</ul>");
                strHTML.Append("<div class=\"clear\"></div>");
                Response.Write(strHTML.ToString());
                pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
            }
            else
            {

            }
        }
    }

    public void NewProduct_Filter(int cate_id, int cate_parentid, int cate_typeid, string action_url)
    {
        int catefatherid = cate_id;
        string Cate_Arry = Get_First_SubCate(cate_id);
        string collection_array = "54", cateprice_array = "55";
        string keyword, isgallerylist, orderby, Extend_Value;
        double price_min, price_max;
        int isgroup, ispromotion, isRecommend, isgift, Brand_ID, tag_id, target, type, collection_id, cateprice_id;
        int icount = 1;

        IList<ProductTypeExtendInfo> extends = null;

        Brand_ID = tools.CheckInt(Request["brand_id"]);
        tag_id = tools.CheckInt(Request["tag_id"]);
        target = tools.CheckInt(Request["target"]);
        type = tools.CheckInt(Request["type"]);

        collection_id = tools.CheckInt(Request["collection_id"]);
        if (collection_id == 0)
        {
            collection_id = 54;
        }

        cateprice_id = tools.CheckInt(Request["cateprice_id"]);
        if (cateprice_id == 0)
        {
            cateprice_id = 55;
        }

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"screen\">");
        strHTML.Append("<dl>");
        strHTML.Append("<dt>分类：</dt>");
        strHTML.Append(Product_Filter_Cate_list(cate_id, Cate_Arry, cate_parentid));
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        bool extend_show = true;
        HttpCookie cookie = Request.Cookies["extend_div"];
        if (cookie == null || cookie.Value.Equals("hide"))
        {
            extend_show = false;
        }

        if (cate_id > 0 && cate_typeid > 0)
        {
            ProductTypeInfo producttype = MyType.GetProductTypeByID(cate_typeid, pub.CreateUserPrivilege("b83adfda-1c87-4cc1-94e8-b5d905cc3da8"));
            if (producttype != null)
            {
                IList<BrandInfo> brands = producttype.BrandInfos;
                if (brands != null)
                {
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt>品牌：</dt>");
                    strHTML.Append(Product_Filter_Brand_list(brands, cate_id, Brand_ID.ToString()));
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                }
                extends = producttype.ProductTypeExtendInfos;
                if (extends != null)
                {
                    foreach (ProductTypeExtendInfo entity in extends)
                    {
                        if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                        {
                            strHTML.Append(Product_Filter_Extend_list(entity));

                            if (icount == 3)
                            {
                                strHTML.Append("<span " + (extend_show ? "" : "style=\"display:none\"") + " id=\"hidden_div\">");
                            }

                            icount++;
                        }
                    }
                }
            }
            if (icount > 3)
            {
                strHTML.Append("</span>");
            }
        }
        else
        {
            strHTML.Append("<dl>");
            strHTML.Append("<dt>品牌：</dt>");
            //strHTML.Append(Product_Filter_Brand_list(brands, cate_id, Brand_ID));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>款式：</dt>");
            strHTML.Append(NewProduct_Filter_Cates_list("collection_id", collection_id, collection_array, 54));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");

            strHTML.Append("<dl>");
            strHTML.Append("<dt>价格：</dt>");
            strHTML.Append(NewProduct_Filter_Cates_list("cateprice_id", cateprice_id, cateprice_array, 55));
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</dl>");
        }
        strHTML.Append("</div>");


        if (cate_id > 0 && cate_typeid > 0)
        {
            if (icount > 3)
            {
                strHTML.Append("<div class=\"blk005\">");
                if (extend_show)
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:hidden_showdiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02_2.png\" /></span></a>");
                }
                else
                {
                    strHTML.Append("<a id=\"_strHref\" href=\"javascript:show_hiddendiv();\" class=\"a01\"><span id=\"_strSpan\"><img src=\"/images/icon02.png\" /></span></a>");
                }
                strHTML.Append("</div>");
            }
        }

        Response.Write(strHTML.ToString());



        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"" + action_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id\" value=\"" + cate_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"collection_id\" id=\"collection_id\" value=\"" + collection_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cateprice_id\" id=\"cateprice_id\" value=\"" + cateprice_id + "\">");

        if (extends != null)
        {
            foreach (ProductTypeExtendInfo entity in extends)
            {
                if (entity.ProductType_Extend_IsActive == 1 && entity.ProductType_Extend_IsSearch == 1)
                {
                    Extend_Value = tools.NullStr(Request["filter_" + entity.ProductType_Extend_ID]).Trim();
                    Response.Write("<input type=\"hidden\" name=\"filter_" + entity.ProductType_Extend_ID + "\" id=\"filter_" + entity.ProductType_Extend_ID + "\" value=\"" + Extend_Value + "\">");
                }
            }
        }

        Response.Write("</form>");
        Response.Write("</div>");
    }


    public string NewProduct_Filter_Cate_list(int cate_id, string Cate_Arry, int cate_parentid)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["cate_id"]);
        if (Cate_Arry == cate_id.ToString())
        {
            strHTML.Append("<a href=\"javascript:filter_setvalue('cate_id'," + cate_parentid + ");\" ");
        }
        else
        {
            strHTML.Append("<a href=\"javascript:filter_setvalue('cate_id'," + cate_id + ");\"");
        }
        if (Cate_Arry == cate_id.ToString())
        {
        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Cate_Arry == cate_id.ToString())
        {
            Cate_Arry = Get_First_SubCate(cate_parentid);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_parentid.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "<>", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", "" + Cate_Arry + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:filter_setvalue('cate_id'," + entity.Cate_ID + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }

    public string NewProduct_Filter_Cates_list(string cate_name, int cate_id, string Cate_Arry, int cate_parentid)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dd>");

        int id = tools.CheckInt(Request["" + cate_name + ""]);
        //if (Cate_Arry == cate_id.ToString())
        //{
        //    strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + cate_parentid + "," + target + ");\" ");
        //}
        //else
        //{
        //    strHTML.Append("<a href=\"javascript:set_form_filter('" + cate_name + "'," + cate_id + "," + target + ");\"");
        //}

        //if (Cate_Arry == cate_id.ToString())
        //{

        //}
        //else
        //{
        //    strHTML.Append(" class=\"on\"");
        //}

        //strHTML.Append(">全部</a>");

        strHTML.Append("<a href=\"javascript:filter_setvalue('" + cate_name + "'," + cate_parentid + ");\"");
        if (cate_id == 54 || cate_id == 55)
        {
            strHTML.Append(" class=\"on\"");
        }
        else
        {

        }
        strHTML.Append(">全部</a>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", "" + cate_parentid + ""));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            foreach (CategoryInfo entity in Categorys)
            {
                strHTML.Append("<a href=\"javascript:filter_setvalue('" + cate_name + "'," + entity.Cate_ID + ");\"");

                if (id == entity.Cate_ID)
                {
                    strHTML.Append(" class=\"on\" ");
                }
                strHTML.Append(">" + entity.Cate_Name + "</a> ");
            }
        }
        strHTML.Append("</dd>");

        return strHTML.ToString();
    }


    public string NewProduct_Filter_Brand_list(IList<BrandInfo> brands, int Cate_ID, int Brand_ID)
    {
        StringBuilder strHTML = new StringBuilder("<dd>");

        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id','');\"");
        if (Brand_ID > 0)
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        foreach (BrandInfo entity in brands)
        {
            if (entity.Brand_IsActive == 1)
            {
                strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('brand_id'," + entity.Brand_ID + ");\" ");
                if (Brand_ID == entity.Brand_ID)
                {
                    strHTML.Append(" class=\"on\"");
                }
                else
                {

                }
                strHTML.Append(">" + entity.Brand_Name + "</a>");
            }
        }
        strHTML.Append("</dd>");
        return strHTML.ToString();
    }

    /// <summary>
    /// 筛选扩展属性选择
    /// </summary>
    /// <param name="extend"></param>
    /// <returns></returns>
    public string NewProduct_Filter_Extend_list(ProductTypeExtendInfo extend)
    {
        string Extend_Value = tools.NullStr(Request["filter_" + extend.ProductType_Extend_ID]).Trim();
        string default_value = extend.ProductType_Extend_Default;

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<dl>");
        strHTML.Append("<dt>" + extend.ProductType_Extend_Name + "：</dt><dd>");
        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','');\"");

        if (Extend_Value != "")
        {

        }
        else
        {
            strHTML.Append(" class=\"on\"");
        }
        strHTML.Append(">全部</a>");

        if (default_value.Length > 0)
        {
            foreach (string extend_value in default_value.Split('|'))
            {
                if (extend_value != "")
                {
                    strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','" + extend_value + "');\"");
                    if (Extend_Value == extend_value)
                    {
                        strHTML.Append(" class=\"on\"");
                    }
                    else
                    {

                    }
                    strHTML.Append(">" + extend_value + "</a></span>");
                }
            }
        }
        else
        {
            string Exit_Extend = "||";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductExtendInfo.Product_Extend_ExtendID", "=", extend.ProductType_Extend_ID.ToString()));
            IList<ProductExtendInfo> entitys = Myproduct.GetProductExtends(Query);
            if (entitys != null)
            {
                foreach (ProductExtendInfo Pre_Extend in entitys)
                {
                    //检查属性重复性
                    if (Exit_Extend.IndexOf("|" + Pre_Extend.Extend_Value + "|") < 0)
                    {
                        strHTML.Append("<a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('filter_" + extend.ProductType_Extend_ID + "','" + Pre_Extend.Extend_Value + "');\"");
                        if (Extend_Value == Pre_Extend.Extend_Value)
                        {
                            strHTML.Append(" class=\"on\"");
                        }
                        else
                        {

                        }
                        strHTML.Append(">" + Pre_Extend.Extend_Value + "</a> ");

                        Exit_Extend += Pre_Extend.Extend_Value + "|";
                    }
                }
            }
        }

        strHTML.Append("</dd>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</dl>");

        return strHTML.ToString();
    }

    #endregion

    #region"商品评论"

    /// <summary>
    /// 商品详情页面评论列表
    /// </summary>
    /// <param name="Product_ID"></param>
    /// <param name="star"></param>
    /// <returns></returns>
    public string Product_Review_List_Top(int Product_ID, string star)
    {
        string review_list = "";
        int icount = 0;
        string member_nickname = "游客";
        DateTime supplier_addtime = DateTime.Now;

        int page = tools.CheckInt(Request.QueryString["page"]);
        if (page < 1)
        {
            page = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.NullInt(Application["Product_Review_Config_ProductCount"]);
        Query.CurrentPage = 1;


        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (star != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);


        StringBuilder sb = new StringBuilder();

        if (reviews != null)
        {
            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                member_nickname = "游客";
                icount = icount + 1;

                if (entity.Shop_Evaluate_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        member_nickname = member.Member_NickName;
                    }
                }

                sb.Append(" <dl>");
                sb.Append("                  <dt><img src=\"/images/icon14.jpg\"></dt>");
                sb.Append("                  <dd>");
                sb.Append("                      <p><span>评论时间：" + entity.Shop_Evaluate_Addtime + "</span>会员：" + member_nickname + "<i>好评</i>");


                if (entity.Shop_Evaluate_Product == 5)
                {
                    sb.Append("     <img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"></p>");
                }
                else if (entity.Shop_Evaluate_Product == 4)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"></p>");
                }
                else if (entity.Shop_Evaluate_Product == 3)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"></p>");
                }
                else if (entity.Shop_Evaluate_Product == 2)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"></p>");
                }
                else if (entity.Shop_Evaluate_Product == 1)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"></p>");
                }
                sb.Append("                      <b>" + entity.Shop_Evaluate_Note + "</b>");
                sb.Append("                  </dd>");
                sb.Append("                  <div class=\"clear\"></div>");
                sb.Append("              </dl>");


            }

        }
        else
        {
            sb.Append("<dl><center>" + Application["Product_Review_Config_NoRecordTip"] + "</center></dl>");

        }

        return sb.ToString();
    }
    //商品评论信息概况
    public int Product_Review_CountByStar(int product_id, string star)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));

        if (star.Length == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "=", star));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }

        PageInfo validpage = MyEvaluate.GetPageInfo(Query);

        return validpage.RecordCount;
    }
    //商品评论信息
    public int Products_Review_CountByStar(int product_id, string star)
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_ProductID", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_IsCheck", "=", "1"));

        if (star.Length == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "=", star));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }

        PageInfo validpage = MyEvaluate.GetPageInfo(Query);

        return validpage.RecordCount;
    }
    //商品评论信息概况
    public void Product_Reviews_Info(int product_id, int Product_Review_Count, double Product_Review_Average)
    {
        Product_Review_Average = tools.CheckFloat(Product_Review_Average.ToString());
        StringBuilder strHTML = new StringBuilder();
        //int ValidCount = MyReview.GetProductReviewValidCount(product_id);
        int ValidCount = MyEvaluate.GetSupplierShopEvaluateReviewValidCount(product_id);

        int BestCount = Product_Review_CountByStar(product_id, "5,4");
        int CommonCount = Product_Review_CountByStar(product_id, "3");
        int BadCount = Product_Review_CountByStar(product_id, "1,2");
        string width_td1, width_td2, width_td3;


        if (ValidCount == 0)
        {
            width_td1 = "0%";
        }
        else
        {
            width_td1 = System.Math.Round(tools.NullDbl(BestCount) / ValidCount * 100, 0) + "%";
        }

        strHTML.Append("<dl>");
        strHTML.Append("  <dt><span><strong>" + width_td1.Replace("%", "") + "</strong>%</span>好评度</dt>");
        strHTML.Append("  <dd>");
        strHTML.Append("    <ul>");
        strHTML.Append("      <li>好评<i><em style=\"width: " + width_td1 + ";\"></em></i><span>" + width_td1 + "</span></li>");


        #region 中评计算

        if (ValidCount == 0)
        {
            width_td2 = "0%";
        }
        else
        {
            width_td2 = System.Math.Round(tools.NullDbl(CommonCount) / ValidCount * 100, 0) + "%";
        }

        #endregion

        strHTML.Append("      <li>中评<i><em style=\"width: " + width_td2 + ";\"></em></i><span>" + width_td2 + "</span></li>");

        #region 差评计算
        if (ValidCount == 0)
        {
            width_td3 = "0%";
        }
        else
        {
            width_td3 = System.Math.Round(tools.NullDbl(BadCount) / ValidCount * 100, 0) + "%";
        }

        #endregion
        strHTML.Append("      <li>差评<i><em style=\"width: " + width_td3 + ";\"></em></i><span>" + width_td3 + "</span></li>");
        strHTML.Append("    </ul>");
        strHTML.Append("  </dd>");
        strHTML.Append("  <div class=\"clear\"></div>");
        strHTML.Append("</dl>");
        strHTML.Append("<p>购买此商品，订单签收后即可发表评论</p>");
        strHTML.Append("<script type=\"text/javascript\">$(\"#pr_1\").text(" + width_td1 + ");$(\"#pr_2\").text(" + width_td2 + ");$(\"#pr_3\").text(" + width_td3 + ")</script>");

        Response.Write(strHTML.ToString());
    }

    public int GetProductEvaluateValidCount(int Product_ID)
    {
        int Shop_product_Count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
            }
        }
        return Shop_product_Count;
    }

    //商品评论评分比
    public double Product_Review(int product_id, string star)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        PageInfo validpage = MyEvaluate.GetPageInfo(Query);
        int Review_count, review_rank_count;
        string width_td;
        //获取有效评论总数
        Review_count = GetProductEvaluateValidCount(product_id);
        if (Review_count == 0)
        {
            Review_count = 1;
        }
        //获取当前评分的有效评论总数
        review_rank_count = validpage.RecordCount;
        //return tools.NullInt(tools.NullDbl(review_rank_count) / Review_count * 100);
        return System.Math.Round(tools.NullDbl(review_rank_count) / Review_count * 100, 1);
    }

    //商品评论评分比
    public string Product_Review_Percentage(int product_id, string star)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        PageInfo validpage = MyEvaluate.GetPageInfo(Query);
        int Review_count, review_rank_count;
        string width_td;

        //获取有效评论总数
        Review_count = GetProductEvaluateValidCount(product_id);
        //获取当前评分的有效评论总数
        review_rank_count = validpage.RecordCount;

        if (Review_count == 0)
        {
            width_td = "0%";
        }
        else
        {
            width_td = System.Math.Round(tools.NullDbl(review_rank_count) / Review_count * 100, 1) + "%";
        }
        //return "<i><em style=\"width:" + width_td + ";\"></em></i><span>" + width_td + "</span>";
        return "<em><i style=\"width: " + width_td + ";\"></i></em>";

    }

    //商品评论表单
    public void Product_Review_Add_Form(int Orders_ID)
    {
        //IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Orders_ID);
        OrdersInfo ordersinfos = MyOrders.GetOrdersByID(Orders_ID);

        if (ordersinfos != null)
        {
            IList<OrdersGoodsInfo> ordersgoodsinfos = orders.GetOrdersGoodsInfoBySN(ordersinfos.Orders_SN);
            int i = 0;
            if (ordersgoodsinfos != null)
            {
                foreach (OrdersGoodsInfo ordersgoodsinfo in ordersgoodsinfos)
                {
                    Response.Write("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\" style=\"margin-top:10px;border-bottom:1px solid #eeeeee\">");
                    i++;
                    Response.Write("<tr><td width=\"120\" valign=\"top\">");

                    Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100px;\"><tr><td width=\"100\" align=\"center\">");
                    Response.Write("<img src=\"" + pub.FormatImgURL(ordersgoodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + ordersgoodsinfo.Orders_Goods_Product_Name + "\" title=\"" + ordersgoodsinfo.Orders_Goods_Product_Name + "\" width=\"100\" height=\"100\" onload=\"javascript:AutosizeImage(this,100,100);\"/>");
                    Response.Write("</td></tr>");
                    Response.Write("</table>");
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"100%\"><tr><td>商品名称</td><td>" + ordersgoodsinfo.Orders_Goods_Product_Name + "</td></tr><tr><td>评分</td><td>");
                    Response.Write("<input type=\"radio\" name=\"review_star" + i + "\" value=\"5\" checked   class=\"input02\" style=\"box-shadow:none\">很好&nbsp;" + Review_ShowStar("review", 0, 0, 5) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"4\" class=\"input02\" style=\"box-shadow:none\">好&nbsp;" + Review_ShowStar("review", 0, 0, 4) + "&nbsp;<input name=\"review_star" + i + "\" type=\"radio\" value=\"3\" class=\"input02\" style=\"box-shadow:none\">一般&nbsp;" + Review_ShowStar("review", 0, 0, 3) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"2\" class=\"input02\" style=\"box-shadow:none\">较差&nbsp;" + Review_ShowStar("review", 0, 0, 2) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"1\" class=\"input02\" style=\"box-shadow:none\">很差&nbsp;" + Review_ShowStar("review", 0, 0, 1));
                    Response.Write("</td></tr>");
                    Response.Write("<tr><td>评论内容</td><td>");
                    Response.Write("<textarea name=\"review_content" + i + "\" cols=\"45\" rows=\"5\"></textarea>");
                    Response.Write("</td></tr></table>");
                    Response.Write("<input type=\"hidden\" name=\"review_productid" + i + "\" id=\"review_productid\" value=\"" + ordersgoodsinfo.Orders_Goods_Product_ID + "\"></td><tr>");

                    Response.Write("</table>");
                }
                Response.Write("<input type=\"hidden\" name=\"amount\" value=\"" + i + "\">");
            }
        }
    }

    //商品评论星显示
    public string Review_ShowStar_bak(string style, int product_id, int Review_count, double review_average)
    {
        string site_url, img_on, img_off, img_half, show_str;
        site_url = Application["site_url"].ToString();
        show_str = "";

        if (style == "small" || style == "detail" || style == "review")
        {
            img_on = site_url + "/images/icon_star_on_small.gif";
            img_off = site_url + "/images/icon_star_off_small.gif";
            img_half = site_url + "/images/icon_star_half_small.gif";
        }
        else if (style == "big" || style == "summary")
        {
            img_on = site_url + "/images/icon_star_on_big.gif";
            img_off = site_url + "/images/icon_star_off_big.gif";
            img_half = site_url + "/images/icon_star_half_big.gif";
        }
        else
        {
            img_on = site_url + "/images/icon_star_on_small.gif";
            img_off = site_url + "/images/icon_star_off_small.gif";
            img_half = site_url + "/images/icon_star_half_small.gif";
        }

        int Maxnum = 5;

        if (review_average > Maxnum)
        {
            review_average = Maxnum;
        }

        if (style == "small" || style == "big")
        {
            show_str = show_str + "<a href=\"" + site_url + "/Products/reviews.aspx?product_id=" + product_id + "\" title=\"" + review_average + "分\">";
        }

        int i, j;
        if (review_average > 0)
        {
            review_average = review_average * 10;
            for (i = 1; i <= (int)(review_average / 10); i++)
            {
                show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_on + "\" border=\"0\">";
            }
            if ((review_average % 10) > 0)
            {
                show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_half + "\" border=\"0\">";
                for (j = i + 1; j <= Maxnum; j++)
                {
                    show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                }
            }
            else
            {
                if (style != "review")
                {
                    for (j = i; j <= Maxnum; j++)
                    {
                        show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                    }
                }
            }
        }
        else
        {
            if (style != "review")
            {
                for (j = 1; j <= Maxnum; j++)
                {
                    show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                }
            }
        }

        if (style == "small" || style == "big")
        {
            show_str = show_str + "</a>";
            if (Review_count > 0)
            {
                show_str = show_str + " (<a href=\"" + site_url + "/Products/reviews.aspx?product_id=" + product_id + "\" title=\"查看评论\">已有" + Review_count + "人评论</a>)";
            }
            else
            {
                show_str = show_str + " (<a href=\"" + site_url + "/Products/reviews_add.aspx?product_id=" + product_id + "\" title=\"我要评论\">评</a>)";
            }
        }
        return show_str;
    }


    public string Review_ShowStar(string style, int product_id, int Review_count, double review_average)
    {
        string site_url, img_on, img_off, img_half, show_str;
        site_url = Application["site_url"].ToString();
        show_str = "";

        if (style == "small" || style == "detail" || style == "review")
        {
            img_on = "/images/icon_star_on_small.gif";
            img_off = "/images/icon_star_off_small.gif";
            img_half = "/images/icon_star_half_small.gif";
        }
        else if (style == "big" || style == "summary")
        {
            img_on = "/images/icon_star_on_big.gif";
            img_off = "/images/icon_star_off_big.gif";
            img_half = "/images/icon_star_half_big.gif";
        }
        else
        {
            img_on = "/images/icon_star_on_small.gif";
            img_off = "/images/icon_star_off_small.gif";
            img_half = "/images/icon_star_half_small.gif";
        }

        int Maxnum = 5;

        if (review_average > Maxnum)
        {
            review_average = Maxnum;
        }

        if (style == "small" || style == "big")
        {
            show_str = show_str + "<a href=\"" + site_url + "/Products/reviews.aspx?product_id=" + product_id + "\" title=\"" + review_average + "分\">";
        }

        int i, j;
        if (review_average > 0)
        {
            review_average = review_average * 10;
            for (i = 1; i <= (int)(review_average / 10); i++)
            {
                show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_on + "\" border=\"0\">";
            }
            if ((review_average % 10) > 0)
            {
                show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_half + "\" border=\"0\">";
                for (j = i + 1; j <= Maxnum; j++)
                {
                    show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                }
            }
            else
            {
                if (style != "review")
                {
                    for (j = i; j <= Maxnum; j++)
                    {
                        show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                    }
                }
            }
        }
        else
        {
            if (style != "review")
            {
                for (j = 1; j <= Maxnum; j++)
                {
                    show_str = show_str + "<img style=\"display:inline;\" src=\"" + img_off + "\" border=\"0\">";
                }
            }
        }

        if (style == "small" || style == "big")
        {
            show_str = show_str + "</a>";
            if (Review_count > 0)
            {
                show_str = show_str + " (<a href=\"" + site_url + "/Products/reviews.aspx?product_id=" + product_id + "\" title=\"查看评论\">已有" + Review_count + "人评论</a>)";
            }
            else
            {
                show_str = show_str + " (<a href=\"" + site_url + "/Products/reviews_add.aspx?product_id=" + product_id + "\" title=\"我要评论\">评</a>)";
            }
        }
        return show_str;
    }

    //商品评论添加
    public void AddProductReview()
    {
        Supplier supplier = new Supplier();
        string review_verify = "";
        int Product_Review_Average = 0;
        int Product_Review_MemberID = 0;
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        int Orders_ID = 0;

        // 检查验证码
        if (tools.CheckInt(Application["Product_Review_Config_VerifyCode_IsOpen"].ToString()) == 1)
        {
            review_verify = tools.CheckStr(Request.Form["review_verify"]);
            if (Session["Trade_Verify"] == null)
            {
                Session["Trade_Verify"] = "";
            }

            if (review_verify == "")
            {
                Response.Write("请输入验证码");
                Response.End();
            }

            if (review_verify != Session["Trade_Verify"].ToString())
            {
                Response.Write("验证码错误");
                Response.End();
            }
        }

        OrdersInfo ordersinfo = MyOrders.GetOrdersBySN(orders_sn);
        if (ordersinfo != null)
        {
            Product_Review_MemberID = ordersinfo.Orders_BuyerID;
            if (ordersinfo.Orders_IsEvaluate == 1)
            {
                Response.Write("此订单已评价");
                Response.End();
                pub.Msg("error", "错误信息", "此订单已评价", false, "/index.aspx");
            }
            if (ordersinfo.Orders_Status != 2)
            {
                Response.Write("此订单尚未结束或已取消");
                Response.End();
            }
            if (ordersinfo.Orders_BuyerID != tools.NullInt(Session["member_id"]))
            {
                Response.Write("此订单不存在");
                Response.End();
            }
            Orders_ID = ordersinfo.Orders_ID;
        }
        else
        {
            Response.Write("此订单不存在");
            Response.End();
        }

        int Supplier_ID = ordersinfo.Orders_SupplierID;


        int gift_coin = tools.NullInt(Application["Product_Review_giftcoin"]);

        //检查用户编号
        if (Session["member_id"] == null)
        {
            Session["member_id"] = 0;
        }
        int Review_Valid = 0;
        double Product_Average = 0;
        int Review_Count = 0;
        int Product_Review_ID = tools.CheckInt(Request["Product_Review_ID"]);
        int amount = tools.CheckInt(Request.Form["amount"]);
        string Product_Review_Subject = "";
        int attitude_review_star = tools.CheckInt(Request["attitude_review_star"]);
        if (attitude_review_star == 0)
        {
            attitude_review_star = 5;
        }
        int delivery_review_star = tools.CheckInt(Request["delivery_review_star"]);
        if (delivery_review_star == 0)
        {
            delivery_review_star = 5;
        }
        int Product_Review_Useful = 0;
        int Product_Review_Useless = 0;
        DateTime Product_Review_Addtime = DateTime.Now;
        int Product_Review_IsShow = 1;
        //if (tools.CheckInt(Application["Product_Review_Config_ManagerReply_Show"].ToString()) == 1)
        //{
        //    Product_Review_IsShow = 0;
        //}
        //else
        //{
        //    Product_Review_IsShow = 1;
        //}

        int b = 0;
        int Product_Review_ProductID = 0;
        int Product_Review_Star = 0;
        string Product_Review_Content = "";
        if (ordersinfo.Orders_Type == 1)
        {
            ProductInfo productinfo;
            for (int i = 1; i <= amount; i++)
            {
                Product_Review_ProductID = tools.CheckInt(Request["review_productid" + i + ""]);
                Product_Review_Star = tools.CheckInt(Request["review_star" + i + ""]);
                Product_Review_Content = tools.CheckStr(Request["review_content" + i + ""]);
                if (Product_Review_Star == 0)
                {
                    Product_Review_Star = 5;
                }
                if (Product_Review_Content == "")
                {
                    Product_Review_Content = "很好";
                }
                else
                {
                    foreach (string keyword in tools.NullStr(Application["Sys_Sensitive_Keyword"]).Split('|'))
                    {
                        if (keyword.Length > 0)
                        {
                            Product_Review_Content = Product_Review_Content.Replace(keyword, "**");
                        }
                    }
                }

                SupplierShopEvaluateInfo entity = new SupplierShopEvaluateInfo();
                entity.Shop_Evaluate_ContractID = Orders_ID;
                entity.Shop_Evaluate_SupplierID = Supplier_ID;
                entity.Shop_Evaluate_Addtime = Product_Review_Addtime;
                entity.Shop_Evaluate_MemberID = Product_Review_MemberID;
                entity.Shop_Evaluate_Recommend = 0;
                entity.Shop_Evaluate_Site = pub.GetCurrentSite();
                entity.Shop_Evaluate_Productid = Product_Review_ProductID;
                entity.Shop_Evaluate_Product = Product_Review_Star;
                entity.Shop_Evaluate_Service = attitude_review_star;
                entity.Shop_Evaluate_Delivery = delivery_review_star;
                entity.Shop_Evaluate_Note = Product_Review_Content;
                entity.Shop_Evaluate_Ischeck = Product_Review_IsShow;

                if (tools.CheckInt(Application["Product_Review_Config_ManagerReply_Show"].ToString()) == 0)
                {
                    entity.Shop_Evaluate_IsGift = 1;
                }
                else
                {
                    entity.Shop_Evaluate_IsGift = 0;
                }

                if (MyEvaluate.AddSupplierShopEvaluate(entity))
                {
                    b = 1;
                    productinfo = GetProductByID(Product_Review_ProductID);
                    if (productinfo != null)
                    {
                        Review_Count = productinfo.Product_Review_Count;
                        //获取有效评论总数
                        Review_Valid = GetProductEvaluateCount(Product_Review_ProductID);

                        if (productinfo.Product_Review_Average == 0)
                        {
                            Product_Review_Average = 5;
                        }
                        else
                        {
                            Product_Review_Average = Convert.ToInt32(productinfo.Product_Review_Average);
                        }
                        if (Review_Valid > 0 && Product_Review_IsShow == 1)
                        {
                            //商品已有评论总星数
                            double Sum_Product_Review_Star = Product_Review_Average * (Review_Valid - 1);
                            //商品评论后的平均星数
                            double Average_Product_Review_Star = (Sum_Product_Review_Star + Product_Review_Star) / Review_Valid;

                            Product_Average = Math.Round(Average_Product_Review_Star, 2);
                            //商品评论后的评论数
                            Review_Count = Review_Count + 1;
                            //修改数据
                            MyReview.UpdateProductReviewINfo(Product_Review_ProductID, Product_Average, Review_Count, Review_Valid);
                        }
                        else
                        {
                            Review_Count = Review_Count + 1;
                            MyReview.UpdateProductReviewINfo(Product_Review_ProductID, Product_Average, Review_Count, Review_Valid);
                        }
                    }
                }
            }
            //总体评价
            Product_Review_Content = tools.CheckStr(Request["Shop_Evaluate_Note"]);
            SupplierShopEvaluateInfo entity1 = new SupplierShopEvaluateInfo();
            entity1.Shop_Evaluate_ContractID = Orders_ID;
            entity1.Shop_Evaluate_SupplierID = Supplier_ID;
            entity1.Shop_Evaluate_Addtime = Product_Review_Addtime;
            entity1.Shop_Evaluate_MemberID = Product_Review_MemberID;
            entity1.Shop_Evaluate_Recommend = 0;
            entity1.Shop_Evaluate_Site = pub.GetCurrentSite();
            entity1.Shop_Evaluate_Productid = 0;
            entity1.Shop_Evaluate_Product = 0;
            entity1.Shop_Evaluate_Service = attitude_review_star;
            entity1.Shop_Evaluate_Delivery = delivery_review_star;
            entity1.Shop_Evaluate_Note = Product_Review_Content;
            entity1.Shop_Evaluate_Ischeck = Product_Review_IsShow;
            if (tools.CheckInt(Application["Product_Review_Config_ManagerReply_Show"].ToString()) == 0)
            {
                entity1.Shop_Evaluate_IsGift = 1;
            }
            else
            {
                entity1.Shop_Evaluate_IsGift = 0;
            }
            MyEvaluate.AddSupplierShopEvaluate(entity1);

        }
        else
        {
            //总体评价
            Product_Review_Content = tools.CheckStr(Request["Shop_Evaluate_Note"]);
            SupplierShopEvaluateInfo entity1 = new SupplierShopEvaluateInfo();
            entity1.Shop_Evaluate_ContractID = Orders_ID;
            entity1.Shop_Evaluate_SupplierID = Supplier_ID;
            entity1.Shop_Evaluate_Addtime = Product_Review_Addtime;
            entity1.Shop_Evaluate_MemberID = Product_Review_MemberID;
            entity1.Shop_Evaluate_Recommend = 0;
            entity1.Shop_Evaluate_Site = pub.GetCurrentSite();
            entity1.Shop_Evaluate_Productid = 0;
            entity1.Shop_Evaluate_Product = 0;
            entity1.Shop_Evaluate_Service = attitude_review_star;
            entity1.Shop_Evaluate_Delivery = delivery_review_star;
            entity1.Shop_Evaluate_Note = Product_Review_Content;
            entity1.Shop_Evaluate_Ischeck = Product_Review_IsShow;
            if (tools.CheckInt(Application["Product_Review_Config_ManagerReply_Show"].ToString()) == 0)
            {
                entity1.Shop_Evaluate_IsGift = 1;
            }
            else
            {
                entity1.Shop_Evaluate_IsGift = 0;
            }
            MyEvaluate.AddSupplierShopEvaluate(entity1);
            b = 1;
        }
        if (b == 1)
        {
            if (gift_coin > 0 && Product_Review_IsShow == 1)
            {
                //更新用户积分
                supplier.Supplier_Coin_AddConsume(gift_coin, "发表评论获得积分", Product_Review_MemberID, false);
            }
            ordersinfo.Orders_IsEvaluate = 1;

            MyOrders.EditOrders(ordersinfo);

            Response.Write("success");
            Response.End();

            //pub.Msg("positive", "操作成功", Application["Product_Review_Config_Show_SuccessTip"].ToString(), true, "/member/order_list.aspx");
        }
        else
        {
            Response.Write("操作失败，请稍后重试");
            Response.End();
        }
    }

    //获取产品评论数量
    public int GetProductEvaluateCount(int Product_ID)
    {
        int Evaluate_Count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (Product_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        }
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            Evaluate_Count = entitys.Count;
        }
        return Evaluate_Count;

    }

    //商品评论列表
    public void Product_Review_List(int Product_ID, string star)
    {
        string review_list = "";
        string page_url = "?product_id=" + Product_ID;
        int icount = 0;
        string member_nickname = "游客";
        string supplier_nickname = "";
        string shop_img = "";
        DateTime supplier_addtime = DateTime.Now;

        QueryInfo Query = new QueryInfo();


        Query.PageSize = tools.NullInt(Application["Product_Review_Config_ProductCount"]);
        if (Query.PageSize == 0)
        {
            Query.PageSize = 10;
        }
        int currentPage = tools.CheckInt(Request.QueryString["page"]);
        if (currentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = currentPage;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (star != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);


        PageInfo pageinfo = MyEvaluate.GetPageInfo(Query);

        StringBuilder sb = new StringBuilder();

        if (reviews != null)
        {
            sb.Append("<table width=\"973\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
            sb.Append("<tr>");
            sb.Append("<td width=\"368\" class=\"name\">评价心得</td>");
            sb.Append("<td width=\"159\" class=\"name\">顾客满意度</td>");
            sb.Append("<td width=\"302\" class=\"name\">评论商家</td>");
            sb.Append("<td width=\"139\" class=\"name\">评论时间</td>");
            sb.Append("</tr>");

            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                member_nickname = "游客";
                icount = icount + 1;

                if (entity.Shop_Evaluate_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        member_nickname = member.Member_NickName;
                    }
                }

                if (entity.Shop_Evaluate_SupplierID > 0)
                {
                    SupplierInfo supplier = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplier != null)
                    {
                        supplier_nickname = supplier.Supplier_CompanyName;
                        supplier_addtime = supplier.Supplier_Addtime;

                        SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(supplier.Supplier_ID);
                        if (shopInfo != null)
                        {
                            shop_img = shopInfo.Shop_Img;
                        }
                    }
                }

                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<dl class=\"dst05\">");
                sb.Append("<dt>" + entity.Shop_Evaluate_Note + "</dt>");
                sb.Append("<div class=\"clear\"></div>");
                sb.Append("</dl>");
                sb.Append("</td>");
                sb.Append("<td><span>");

                if (entity.Shop_Evaluate_Product == 5)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\">");
                }
                else if (entity.Shop_Evaluate_Product == 4)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\">");
                }
                else if (entity.Shop_Evaluate_Product == 3)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\">");
                }
                else if (entity.Shop_Evaluate_Product == 2)
                {
                    sb.Append("<img src=\"/images/x2.jpg\"><img src=\"/images/x2.jpg\">");
                }
                else if (entity.Shop_Evaluate_Product == 1)
                {
                    sb.Append("<img src=\"/images/x2.jpg\">");
                }

                sb.Append("</span></td>");
                sb.Append("<td>");
                sb.Append("<dl class=\"dst06\">");
                sb.Append("<dt>");
                sb.Append("<img src=\"" + pub.FormatImgURL(shop_img, "fullpath") + "\"></dt>");
                sb.Append("<dd>");
                sb.Append("<p>" + supplier_nickname + "</p>");
                sb.Append("<p>入驻第<strong>" + pub.DateDiffYear(supplier_addtime, DateTime.Now) + "</strong>年</p>");
                sb.Append("</dd>");
                sb.Append("<div class=\"clear\"></div>");
                sb.Append("</dl>");
                sb.Append("</td>");
                sb.Append("<td>" + entity.Shop_Evaluate_Addtime.ToString("yyyy-MM-dd HH:mm") + "</td>");
                sb.Append("</tr>");

            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            //Response.Write("<div class=\"page04\"><a href=\"#\" target=\"_blank\"><< 上一页</a><a href=\"#\" target=\"_blank\">1</a><a href=\"#\" target=\"_blank\">2</a><a href=\"#\" target=\"_blank\">3</a> ... <a href=\"#\" target=\"_blank\">下一页 >></a></div>");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);

        }
        else
        {

            review_list = review_list + "<table width=\"100%\" border=\"0\" cellspacing=\"1\" align=\"center\" cellpadding=\"5\">";
            review_list = review_list + "<tr>";
            review_list = review_list + "<td height=\"10\"></td>";
            review_list = review_list + "</tr>";

            review_list = review_list + "<tr>";
            review_list = review_list + "<td class=\"t12_grey\" align=\"center\">" + Application["Product_Review_Config_NoRecordTip"] + "</td>";
            review_list = review_list + "</tr>";
            review_list = review_list + "</table>";

            Response.Write(review_list);
        }


    }

    public void Product_Review_List_bak(int Product_ID, string star)
    {
        string review_list = "";
        string page_url = "?product_id=" + Product_ID;


        QueryInfo Query = new QueryInfo();


        Query.PageSize = tools.NullInt(Application["Product_Review_Config_ProductCount"]);
        if (Query.PageSize == 0)
        {
            Query.PageSize = 10;
        }
        int currentPage = tools.CheckInt(Request.QueryString["page"]);
        if (currentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = currentPage;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (star != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);


        PageInfo pageinfo = MyEvaluate.GetPageInfo(Query);

        if (reviews != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                string supplier_nickname = "游客";
                if (entity.Shop_Evaluate_MemberID > 0)
                {
                    SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierinfo != null)
                    {
                        supplier_nickname = supplierinfo.Supplier_Nickname;
                    }
                }

                sb.Append("<dl>");
                sb.Append("<dt><span>会员：" + supplier_nickname + "</span>评论时间：" + entity.Shop_Evaluate_Addtime.ToString("yyyy/MM/dd") + "</dt>");

                sb.Append("<dd>" + entity.Shop_Evaluate_Note + "</dd>");
                sb.Append("<div class=\"clear\">");
                sb.Append("</div>");
                sb.Append("</dl>");

            }
            Response.Write(sb.ToString());
            Response.Write("<div class=\"page\" style=\"float:right;\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");
        }
        else
        {

            review_list = review_list + "<table width=\"100%\" border=\"0\" cellspacing=\"1\" align=\"center\" cellpadding=\"5\">";
            review_list = review_list + "<tr>";
            review_list = review_list + "<td height=\"10\"></td>";
            review_list = review_list + "</tr>";

            review_list = review_list + "<tr>";
            review_list = review_list + "<td class=\"t12_grey\" align=\"center\">" + Application["Product_Review_Config_NoRecordTip"] + "</td>";
            review_list = review_list + "</tr>";
            review_list = review_list + "</table>";

            Response.Write(review_list);
        }


    }

    //获取评论数量
    public int Get_Product_Review_Amount(int Product_ID, string star)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (star != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        }
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews == null)
            return 0;
        return reviews.Count;
    }

    //商品详情页 评论列表
    public void ProductDetail_Review_List(int Product_ID, string star)
    {
        string review_list = "";
        StringBuilder sb = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        if (Application["Product_Review_Config_ListCount"] == null)
        {
            Query.PageSize = 10;
        }
        else
        {
            Query.PageSize = tools.CheckInt(Application["Product_Review_Config_ListCount"].ToString());
        }
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_ProductID", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsShow", "=", "1"));
        if (star != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_Star", "in", star));
        }
        Query.OrderInfos.Add(new OrderInfo("ProductReviewInfo.Product_Review_ID", "desc"));

        IList<ProductReviewInfo> reviews = MyReview.GetProductReviews(Query, pub.CreateUserPrivilege("cb1e9c33-7ac5-4939-8520-a0e192cb0129"));
        //PageInfo review_page = MyReview.GetPageInfo(Query, pub.CreateUserPrivilege("cb1e9c33-7ac5-4939-8520-a0e192cb0129"));

        if (reviews != null)
        {
            foreach (ProductReviewInfo entity in reviews)
            {
                string supplier_nickname = "游客";
                if (entity.Product_Review_MemberID > 0)
                {
                    SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Product_Review_MemberID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierinfo != null)
                    {
                        supplier_nickname = supplierinfo.Supplier_Nickname;
                    }
                }

                sb.Append("<dl>");
                sb.Append("<dt><span>会员：" + supplier_nickname + "</span>评论时间：" + entity.Product_Review_Addtime.ToString("yyyy/MM/dd") + "</dt>");

                sb.Append("<dd>" + entity.Product_Review_Content + "</dd>");
                sb.Append("<div class=\"clear\">");
                sb.Append("</div>");
                sb.Append("</dl>");

            }
        }
        else
        {
            review_list = review_list + "<table width=\"100%\" border=\"0\" cellspacing=\"1\" align=\"center\" cellpadding=\"5\">";
            review_list = review_list + "<tr>";
            review_list = review_list + "<td height=\"10\"></td>";
            review_list = review_list + "</tr>";

            review_list = review_list + "<tr>";
            review_list = review_list + "<td class=\"t12_grey\" align=\"center\">" + Application["Product_Review_Config_NoRecordTip"] + "</td>";
            review_list = review_list + "</tr>";
            review_list = review_list + "</table>";
            sb.Append(review_list);
        }
        Response.Write(sb.ToString());
    }

    #endregion

    #region 产品对比

    /// <summary>
    /// 添加产品到对比
    /// </summary>
    public void AddProductContrast(int Product_ID)
    {

        IList<ProductInfo> entitys = new List<ProductInfo>();
        ProductInfo entity = GetProductByID(Product_ID);
        if (Session["ProductInfo"] != null)
        {
            entitys = (IList<ProductInfo>)Session["ProductInfo"];
            if (entitys.Count == 3)
            {
                pub.Msg("positive", "信息提示", "同时最多可对比3件产品", true, "/Product/Product_Contrast.aspx");
            }
            if (entity != null)
            {
                foreach (ProductInfo productinfo in entitys)
                {
                    if (productinfo.Product_ID == entity.Product_ID)
                    {
                        pub.Msg("info", "信息提示", "该产品已存在于对比列表", false, "/Product/Product_Contrast.aspx");
                    }
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "该产品不存在!", false, "/index.aspx");
            }
        }
        entitys.Add(entity);

        Session["ProductInfo"] = entitys;
        pub.Msg("positive", "信息提示", "该产品已添加至对比列表", true, "/Product/Product_Contrast.aspx");

    }

    /// <summary>
    /// 产品对比
    /// </summary>
    /// <returns></returns>
    public string Product_Contrast()
    {
        string show_list = "";
        IList<ProductInfo> entitys = (IList<ProductInfo>)Session["ProductInfo"];
        int a = 0;
        if (entitys != null)
        {
            if (entitys.Count == 0)
            {
                show_list += "<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"100%\" style=\"margin-top:10px;\">";
                show_list += "<tr>";
                show_list += "<td align=\"center\" style=\"height:40px; line-height:40px;\">暂无对比信息</td></tr></table>";
            }
            else
            {
                a = entitys.Count + 1;
                show_list += "<table border=\"0\" cellpadding=\"5\" cellspacing=\"1\" style=\"background:#cccccc;margin-top:10px; width:100%;\"><tr>";

                show_list = show_list + "<tr>";
                show_list = show_list + "<td align=\"center\" style=\"background:#ffffff;\" width=\"20%\" height=\"40\">操作</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list = show_list + "<td width=\"20%\" style=\"background:#ffffff;\" align=\"center\"><a style=\"text-decoration:none; color:#6699ff;\" href=\"/Product/Reviews_do.aspx?action=delcontrast&Product_ID=" + entitys[i].Product_ID + "\">移除该商品</a></td>";
                }
                show_list = show_list + "</tr>";


                show_list += "<td style=\"background:#ffffff;\" align=\"center\" valign=\"middle\" width=\"20%\">产品图片</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list += "<td align=\"center\" style=\"background:#ffffff;\" width=\"20%\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entitys[i].Product_ID.ToString()) + "\"><img src=\"" + pub.FormatImgURL(entitys[i].Product_Img, "fullpath") + "\" width=\"90\" height=\"90\" onload=\"javascript:AutosizeImage(this,90,90);\"/></a></br><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entitys[i].Product_ID.ToString()) + "\">" + entitys[i].Product_Name + "</a></td>";
                }
                show_list += "</tr>";
                show_list += "<tr><td style=\"background:#ffffff;\" align=\"center\"  valign=\"middle\" width=\"20%\">价格</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list += "<td align=\"center\" height=\"40\" width=\"20%\" valign=\"middle\" style=\"background:#ffffff; color:#cc0000;\">￥" + Get_Member_Price(entitys[i].Product_ID, entitys[i].Product_Price) + "</td>";
                }
                show_list += "</tr>";

                show_list += "<tr><td style=\"background:#ffffff;\" align=\"center\" valign=\"middle\" width=\"20%\">所属品牌</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    BrandInfo brandinfo = GetBrandInfoByID(entitys[i].Product_BrandID);
                    if (brandinfo != null)
                    {
                        show_list += "<td align=\"center\" height=\"40\" valign=\"middle\" style=\"background:#ffffff;\" width=\"20%\">" + brandinfo.Brand_Name + "</td>";
                    }
                    else
                    {
                        show_list += "<td align=\"center\" height=\"40\" valign=\"middle\" style=\"background:#ffffff;\" width=\"20%\">--</td>";
                    }
                }
                show_list += "</tr>";
                show_list += "<tr><td style=\"background:#ffffff;\" align=\"center\" valign=\"middle\" width=\"20%\">生产商</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list += "<td align=\"center\" height=\"40\" valign=\"middle\" style=\"background:#ffffff;\" width=\"20%\">" + entitys[i].Product_Maker + "</td>";
                }
                show_list += "<tr><td style=\"background:#ffffff;\" align=\"center\"  valign=\"middle\" width=\"20%\">包装规格</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list += "<td align=\"center\" height=\"40\" valign=\"middle\" style=\"background:#ffffff;\" width=\"20%\">" + entitys[i].Product_Spec + "</td>";
                }
                show_list += "</tr>";
                show_list += "<tr><td style=\"background:#ffffff;\" align=\"center\"  valign=\"middle\" width=\"20%\">产品毛重</td>";
                for (int i = 0; i < entitys.Count; i++)
                {
                    show_list += "<td align=\"center\" height=\"40\" valign=\"middle\" style=\"background:#ffffff;\" width=\"20%\">" + entitys[i].Product_Weight + "g</td>";
                }
                show_list += "</tr></table>";

            }
        }
        else
        {
            show_list += "<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"100%\" style=\"margin-top:10px;\">";
            show_list += "<tr>";
            show_list += "<td align=\"center\" style=\"height:40px; line-height:40px;\">暂无对比信息</td></tr></table>";
        }


        return show_list;
    }

    /// <summary>
    /// 移除对比专业
    /// </summary>
    public void DelProduct_Contrast(int Product_ID)
    {

        IList<ProductInfo> entitys = new List<ProductInfo>();
        IList<ProductInfo> entitys1 = new List<ProductInfo>();
        if (Session["ProductInfo"] != null)
        {
            entitys = (IList<ProductInfo>)Session["ProductInfo"];

            foreach (ProductInfo entity in entitys)
            {
                if (entity.Product_ID != Product_ID)
                {
                    entitys1.Add(entity);
                }
            }
        }
        Session["ProductInfo"] = entitys1;
        pub.Msg("positive", "信息提示", "移除对比产品成功", true, "/Product/Product_Contrast.aspx");

    }

    #endregion

    #region "购物咨询"

    public void Product_Shoppingask(int productid)
    {
        Product_Ask_List(productid, 5, 0, 0);
    }

    public void Product_Ask_List(int Product_Id, int pagesize, int allflag, int cate_id)
    {
        string keyword;
        int i = 0;
        int recordcount = 0;
        keyword = tools.CheckStr(Request["keyword"]);
        string pageurl = "?product_id=" + Product_Id;
        int curpage;
        curpage = tools.CheckInt(Request["page"]);
        if (curpage < 1)
        {
            curpage = 1;
        }

        StringBuilder strHTML = new StringBuilder();
        string AskMember;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = pagesize;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", "=", Product_Id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_IsCheck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_Isreply", "=", "1"));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ShoppingAskInfo.Ask_Content", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo("ShoppingAskInfo.Ask_ID", "Desc"));
        IList<ShoppingAskInfo> shopask = Myshopask.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        PageInfo page = Myshopask.GetPageInfo(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        if (shopask != null)
        {
            recordcount = page.RecordCount;
            foreach (ShoppingAskInfo entity in shopask)
            {
                AskMember = "游客";

                if (entity.Ask_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Ask_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        AskMember = member.Member_NickName;
                    }
                }

                //strHTML.Append("<dl class=\"dst19\">");
                //strHTML.Append("	<dt>网友：<span>" + AskMember + "</span> 提问时间:<span>" + entity.Ask_Addtime + "</span></dt>");
                //strHTML.Append("	<dd><b><strong class=\"str5\">咨询内容：</strong><span>" + entity.Ask_Content + "</span></b><div class=\"clear\"></div>");
                //strHTML.Append("		<p><strong class=\"str6\">易种回复：</strong><span>" + entity.Ask_Reply + "，感谢您对我们的关注，祝您购物愉快！</span></p>");
                //strHTML.Append("		<div class=\"clear\"></div>");
                //strHTML.Append("	</dd>");
                //strHTML.Append("</dl>");


                strHTML.Append(" <dl>");
                strHTML.Append("                   <dt><span>提问时间：" + entity.Ask_Addtime + "</span><i>Q</i><p>会员提问：" + entity.Ask_Contact + "</p></dt>");
                strHTML.Append("                   <dd><em>A</em><p>客服回答：" + entity.Ask_Reply + "</p></dd>");
                strHTML.Append("              </dl>");

            }

            Response.Write(strHTML.ToString());

            if (allflag == 1)
            {
                Response.Write("<table width=\"100%\"><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
                pub.Page(page.PageCount, page.CurrentPage, pageurl, page.PageSize, page.RecordCount);
                Response.Write("</div></td></tr></table>");
            }

        }
        else
        {
            Response.Write("<dl><center>暂无相关咨询</center></dl>");

        }
    }

    //购物咨询添加
    public void Shopping_Ask_Add()
    {
        string ask_content, ask_verify, ask_contact;
        int ask_type, product_id;
        ask_type = tools.CheckInt(Request.Form["ask_type"]);
        product_id = tools.CheckInt(Request.Form["ask_productid"]);
        ask_content = tools.CheckStr(Request.Form["ask_content"]);
        ask_contact = tools.CheckStr(Request.Form["ask_contact"]);
        ask_verify = Request.Form["ask_verify"];



        if (ask_type < 1 || ask_type > 4)
        {
            ask_type = 1;
        }
        if (product_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择要咨询的商品", false, "{back}");
        }
        if (ask_verify != Session["Trade_Verify"].ToString())
        {
            pub.Msg("error", "错误信息", "验证码错误", false, "{back}");
        }
        if (ask_content == "")
        {
            pub.Msg("error", "错误信息", "请输入咨询内容", false, "{back}");
        }
        ProductInfo productinfo = GetProductByID(product_id);
        if (productinfo != null)
        {
            ShoppingAskInfo entity = new ShoppingAskInfo();
            entity.Ask_ID = 0;
            entity.Ask_Type = ask_type;
            entity.Ask_Contact = ask_contact;
            entity.Ask_Content = ask_content;
            entity.Ask_Reply = "";
            entity.Ask_Addtime = DateTime.Now;
            entity.Ask_SupplierID = productinfo.Product_SupplierID;
            entity.Ask_MemberID = tools.NullInt(Session["member_id"]);
            entity.Ask_ProductID = product_id;
            entity.Ask_Isreply = 0;
            entity.Ask_IsCheck = 0;
            entity.Ask_Pleasurenum = 0;
            entity.Ask_Displeasure = 0;
            entity.Ask_Site = pub.GetCurrentSite();

            if (Myshopask.AddShoppingAsk(entity, pub.CreateUserPrivilege("7da52156-9a1e-46af-bad4-7611cef159e3")))
            {
                pub.Msg("positive", "成功", "您的咨询已经成功提交，我们会尽快处理回复！", true, pageurl.FormatURL(pageurl.product_detail, product_id.ToString()));
            }
            else
            {
                pub.Msg("error", "错误信息", "咨询提交失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "咨询提交失败，请稍后再试！", false, "{back}");
        }

    }

    #endregion

    #region 采购申请
    /// <summary>
    /// 前台采购展示列表
    /// </summary>
    public void SupplierPurchase_list()
    {

        string keyword = tools.CheckStr(Request["keyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string stateCode = tools.CheckStr(Request["stateCode"]);
        int curpage = tools.CheckInt(Request["page"]);
        //排序方式
        string orderby = tools.CheckStr(Request.QueryString["orderby"]);
        string Pageurl = "?action=list";
        Pageurl += "&stateCode=" + stateCode;
        Pageurl += "&orderby=" + orderby;

        if (curpage < 1)
        {
            curpage = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToShortDateString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));
        //if (supplier_id > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SupplierID", "<>", supplier_id.ToString()));
        //}
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsPublic", "=", "1"));

        if (keyword != "")
        {
            Pageurl += "&keyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "like", keyword));
        }

        if (stateCode != "")
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_State", "=", stateCode));
        }



        switch (orderby)
        {
            case "addtime_up":
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_Addtime", "asc"));
                break;
            case "addtime_down":
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_Addtime", "desc"));
                break;
            case "validdate_up":
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ValidDate", "asc"));
                break;
            case "validdate_down":
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ValidDate", "desc"));
                break;

            default:
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
                break;
        }

        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchasesList(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        PageInfo page = MyPurchase.GetPageInfo(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));


        StringBuilder sb = new StringBuilder();


        sb.Append("<!--排序 开始-->");
        sb.Append("<div class=\"blk05\" style=\"margin-top: 0;\">");
        sb.Append("<div class=\"blk05_info01\">");
        sb.Append("<strong>全部采购信息</strong></div>");
        sb.Append("<div class=\"blk05_info02\">");



        sb.Append("<ul><li><div>");


        IList<StateInfo> stateList = null;

        stateList = MyAddr.GetStatesByCountry("1");


        //选择省
        if (stateList != null)
        {
            sb.Append("<select class=\"one\" name=\"stateCode\" id=\"stateCode\" onchange=\"javascript:filter_setvalue('stateCode',this.value);\" >");
            sb.Append("<option value=\"\">----选择省----</option>");
            foreach (StateInfo entity in stateList)
            {
                sb.Append("<option value=\"" + entity.State_Code + "\"");
                if (entity.State_Code == stateCode) { sb.Append(" selected=\"selected\""); }
                sb.Append(">" + entity.State_CN + "</option>");
            }
            sb.Append("</select>");
        }

        sb.Append("</div></li>");

        //sb.Append("<li class=\"one\">所有地区</li>");
        //sb.Append("<li class=\"one\">出货地</li>");

        sb.Append("<li><label>报价截止时间</label>");


        sb.Append("<a  href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','validdate_down');\"  class=\"a7 " + (orderby == "validdate_down" ? "a7on" : "") + "\" ></a>");
        sb.Append("<a  href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','validdate_up');\"  class=\"a8 " + (orderby == "validdate_up" ? "a8on" : "") + " \"></a>");

        sb.Append("</li>");

        sb.Append("</li>");
        sb.Append("<li><label>信息发布时间</label>");
        sb.Append("<a  href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','addtime_down');\"  class=\"a7 " + (orderby == "addtime_down" ? "a7on" : "") + "\" ></a>");
        sb.Append("<a  href=\"javascript:void(0);\" onclick=\"javascript:filter_setvalue('orderby','addtime_up');\"  class=\"a8 " + (orderby == "addtime_up" ? "a8on" : "") + "\" ></a>");

        sb.Append("</li>");

        sb.Append("<li class=\"bo\"><a href=\"/supplier/Shopping_add.aspx\" target=\"_blank\">我要发布求购信息</a></li>");
        sb.Append("</ul>");

        sb.Append("<div class=\"clear\">");
        sb.Append("</div>");

        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("<!--排序 结束-->");


        sb.Append("<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"/Purchase/index.aspx\">");

        sb.Append("<input type=\"hidden\" name=\"orderby\" id=\"orderby\" value=\"" + orderby + "\">");
        sb.Append("<input type=\"hidden\" name=\"stateCode\" id=\"stateCode\" value=\"" + stateCode + "\">");

        sb.Append("</form>");






        sb.Append("<!--列表 开始-->");
        sb.Append("<div class=\"list_box\">");




        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {

                sb.Append("<div class=\"list_info02\">");
                sb.Append("<div class=\"list_info02_01\">");
                sb.Append("<a href=\"/Purchase/detail.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\"><span>" + entity.Purchase_Title + "</span></a>");
                sb.Append("</div>");
                sb.Append("<div class=\"list_info02_02\">");

                sb.Append("<p>采购类型：" + pub.GetPurchaseType(entity.Purchase_TypeID) + "</p>");

                //sb.Append("<p>要求：现货</p>");
                sb.Append("</div>");
                sb.Append("<div class=\"list_info02_04\">");
                sb.Append("<p>");

                SupplierInfo sinfo = supplier_class.GetSupplierByID(entity.Purchase_SupplierID);

                if (sinfo != null)
                {
                    sb.Append("求购商：" + sinfo.Supplier_CompanyName + "</p>");
                }
                else
                {
                    sb.Append("求购商：[易耐产业金服]</p>");
                }
                sb.Append("<p>");
                StateInfo stateinfo = MyAddr.GetStateInfoByCode(entity.Purchase_State);
                if (stateinfo != null)
                {
                    sb.Append("所在地：" + stateinfo.State_CN + "</p>");
                }
                else
                {
                    sb.Append("所在地：-- </p>");
                }
                sb.Append("</div>");
                sb.Append("<div class=\"list_info02_05\">");
                sb.Append("<p>");
                sb.Append("发布时间：" + entity.Purchase_Addtime.ToString("yyyy/MM/dd") + "</p>");
                sb.Append("<p>");
                sb.Append("报价截止：" + entity.Purchase_ValidDate.ToString("yyyy/MM/dd") + "</p>");
                sb.Append("</div>");
                sb.Append("<div class=\"list_info02_06\">");
                sb.Append("<a href=\"/Purchase/detail.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\" class=\"a03\">查看详情</a> ");
                sb.Append(" <a href=\"/supplier/Shopping_PriceReport.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\" class=\"a04\">我要报价</a>");
                sb.Append("</div>");
                sb.Append("<div class=\"clear\">");
                sb.Append("</div>");
                sb.Append("</div>");


            }

            sb.Append("</div>");
            sb.Append("<!--列表 结束-->");
            Response.Write(sb.ToString());

            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(sb.ToString());
            Response.Write("<div style=\"line-height:35px; text-align:center\">暂无采购信息</div>");
        }





    }
    /// <summary>
    /// 采购展示，统计数量
    /// </summary>
    /// <returns></returns>
    public int GetSupplierPurchasesCount()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string stateCode = tools.CheckStr(Request["stateCode"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToShortDateString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsPublic", "=", "1"));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "like", keyword));
        }
        if (stateCode != "")
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_State", "=", stateCode));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
        PageInfo page = MyPurchase.GetPageInfo(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));

        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;

        }

    }
    /// <summary>
    /// 详情页采购商信息
    /// </summary>
    /// <param name="supplier_id"></param>
    public void SupplierPurchase_Supplier(int supplier_id)
    {
        StringBuilder sb = new StringBuilder();
        SupplierInfo sinfo = null;


        sb.Append("<h2>采购商信息</h2>");
        sb.Append("<div class=\"main\">");
        sb.Append("<div class=\"blk11_info01\">");


        sinfo = supplier_class.GetSupplierByID(supplier_id);

        if (sinfo != null)
        {
            sb.Append("<a href=\"#\" target=\"_blank\"><img src=\"x\">" + sinfo.Supplier_CompanyName + "</a></p>");
        }
        else
        {
            sb.Append("<a href=\"#\" target=\"_blank\"><img src=\"x\"> -- </a></p>");
        }


        sb.Append("</div>");
        sb.Append("<div class=\"blk11_info01\">");
        sb.Append("<h3>");
        sb.Append("公司详情</h3>");
        sb.Append("<p>");
        sb.Append("来自 上海</p>");
        sb.Append("<p>");
        sb.Append("主营行业：矿业输送设备</p>");
        sb.Append("<p>");
        sb.Append("企业类型：有限责任公司</p>");
        sb.Append("<p>");
        sb.Append("注册资本：人民币 50.0000 万</p>");
        sb.Append("<p>");
        sb.Append("经营模式：其他</p>");
        sb.Append("<p>");
        sb.Append("年营业额：人民币 3001 万元/年</p>");
        sb.Append("</div>");
        sb.Append("<div class=\"blk11_info01\">");
        sb.Append("<h3>");
        sb.Append("联系方式</h3>");
        sb.Append("<p>");
        sb.Append("买家要求：<span>报价后可见</span></p>");
        sb.Append("</div>");
        sb.Append("</div>");
        Response.Write(sb.ToString());
    }
    /// <summary>
    /// 详情页推荐采购
    /// </summary>
    public void SupplierPurchase_IsRecommend(string show)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int pagesize = 5;
        if (show == "detail")
        { pagesize = 5; }
        else if (show == "index")
        { pagesize = 15; }
        QueryInfo Query = new QueryInfo();

        Query.PageSize = pagesize;

        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToShortDateString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsPublic", "=", "1"));
        //if (supplier_id > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SupplierID", "<>", supplier_id.ToString()));
        //}

        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_IsRecommend", "Desc"));
        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));

        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchasesList(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));

        StringBuilder sb = new StringBuilder();

        if (show == "detail")
        {
            #region 采购详情页
            if (entitys != null)
            {
                int i = 0;
                sb.Append("<ul>");
                foreach (SupplierPurchaseInfo entity in entitys)
                {
                    sb.Append("<li " + (++i <= 3 ? "style=\"width: 191px;\"" : "") + ">");
                    sb.Append("<dl>");
                    sb.Append("<dt><a href=\"/Purchase/detail.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\">" + entity.Purchase_Title + "</a><p>");
                    sb.Append("采购类型：<strong>" + pub.GetPurchaseType(entity.Purchase_TypeID) + "</strong></p>");
                    sb.Append("</dt>");
                    sb.Append("<dd>");


                    SupplierInfo sinfo = supplier_class.GetSupplierByID(entity.Purchase_SupplierID);

                    if (sinfo != null)
                    {
                        StateInfo stateinfo = MyAddr.GetStateInfoByCode(sinfo.Supplier_State);
                        if (stateinfo != null)
                        {
                            sb.Append("<p>来自 " + stateinfo.State_CN + " 的" + sinfo.Supplier_CompanyName + "</p>");
                        }
                        else
                        {
                            sb.Append("<p>来自 [易耐产业金服]</p>");
                        }
                    }
                    else
                    {
                        sb.Append("<p>来自 [易耐产业金服]</p>");
                    }

                    sb.Append("<p>发布时间：" + entity.Purchase_Addtime.ToString("yyyy/MM/dd") + "</p>");

                    sb.Append("<p>截止日期：" + entity.Purchase_ValidDate.ToString("yyyy/MM/dd") + "</p>");

                    sb.Append("<p><a href=\"/supplier/Shopping_PriceReport.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\">立即报价</a></p>");

                    sb.Append("</dd>");
                    sb.Append("</dl>");
                    sb.Append("</li>");

                }
                sb.Append("</ul>");
                sb.Append("<div class=\"clear\">");
                sb.Append("</div>");
                Response.Write(sb.ToString());
            }
            #endregion
        }
        else if (show == "index")
        {
            #region 首页 采购
            if (entitys != null)
            {
                int i = 0;
                sb.Append("<ul>");
                foreach (SupplierPurchaseInfo entity in entitys)
                {

                    string str = "";
                    switch (i++)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 10:
                        case 11:
                        case 12:
                            str = "class=\"bg\"";
                            break;
                        default:
                            break;
                    }
                    sb.Append("<li " + str + " ><span>发布时间：" + entity.Purchase_Addtime.ToShortDateString() + "</span>");
                    sb.Append("<a href=\"/Purchase/detail.aspx?apply_id=" + entity.Purchase_ID + "\" target=\"_blank\">" + entity.Purchase_Title + "</a>");


                    //SupplierInfo sinfo = supplier_class.GetSupplierByID(entity.Purchase_SupplierID);

                    //if (sinfo != null)
                    //{
                    //    StateInfo stateinfo = MyAddr.GetStateInfoByCode(sinfo.Supplier_State);
                    //    if (stateinfo != null)
                    //    {
                    //        sb.Append("<p>来自 " + stateinfo.State_CN + " 的" + sinfo.Supplier_CompanyName + "</p>");
                    //    }
                    //    else
                    //    {
                    //        sb.Append("<p>来自 [易耐产业金服]</p>");
                    //    }
                    //}
                    //else
                    //{
                    //    sb.Append("<p>来自 [易耐产业金服]</p>");
                    //}

                    sb.Append("</li>");

                }
                sb.Append("</ul>");
                sb.Append("<div class=\"clear\">");
                sb.Append("</div>");
                Response.Write(sb.ToString());
            }
            #endregion
        }
    }



    /// <summary>
    /// 大宗采购列表
    /// </summary>
    public void Purchase_IndexList()
    {
        StringBuilder strHTML = new StringBuilder();

        MemberInfo memberInfo = null;
        string member_company = "", member_img = "";
        int i = 0;
        string Pageurl = "?action=list";
        string orderby = tools.CheckStr(Request.QueryString["orderby"]);
        int curpage = tools.CheckInt(Request["page"]);
        if (curpage < 1)
        {
            curpage = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseInfo.Purchase_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseInfo.Purchase_Addtime", "desc"));
        IList<MemberPurchaseInfo> entitys = MyMPurchase.GetMemberPurchases(Query);
        PageInfo pageInfo = MyMPurchase.GetPageInfo(Query);


        if (entitys != null)
        {
            strHTML.Append("<ul>");
            foreach (MemberPurchaseInfo entity in entitys)
            {
                i++;

                memberInfo = MyMem.GetMemberByID(entity.Purchase_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberInfo != null && memberInfo.MemberProfileInfo != null)
                {
                    member_company = memberInfo.MemberProfileInfo.Member_Company;
                    member_img = memberInfo.MemberProfileInfo.Member_HeadImg;
                }

                if (i % 2 == 0)
                {
                    strHTML.Append("<li class=\"mr0\">");
                }
                else
                {
                    strHTML.Append("<li>");
                }
                strHTML.Append("<h2><span>采购数量：<strong>" + entity.Purchase_Amount + "</strong>" + entity.Purchase_Unit + "</span><a href=\"/purchase/purchase_detail.aspx?Purchase_ID=" + entity.Purchase_ID + "\" target=\"_blank\">" + entity.Purchase_Title + "</a></h2>");
                strHTML.Append("<div class=\"li_box_left\">");
                strHTML.Append("<table width=\"288\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"2\" class=\"name\"><a href=\"/purchase/purchase_detail.aspx?Purchase_ID=" + entity.Purchase_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(member_img, "fullpath") + "\" width=\"120\" height=\"120\"></a></td>");
                strHTML.Append("</tr>");
                strHTML.Append("<tr>");
                strHTML.Append("<td width=\"83\" class=\"name\">采购商家：</td>");
                strHTML.Append("<td width=\"205\">" + member_company + "</td>");
                strHTML.Append("</tr>");
                strHTML.Append("<tr>");
                strHTML.Append("<td class=\"name\">采购商品：</td>");
                strHTML.Append("<td>" + entity.Purchase_Title + "</td>");
                strHTML.Append("</tr>");
                strHTML.Append("<tr>");
                strHTML.Append("<td class=\"name\">采购数量：</td>");
                strHTML.Append("<td>" + entity.Purchase_Amount + entity.Purchase_Unit + "</td>");
                strHTML.Append("</tr>");
                strHTML.Append("<tr>");
                strHTML.Append("<td class=\"name\">有效期至：</td>");
                strHTML.Append("<td>" + entity.Purchase_Validity.ToString("yyyy年MM月dd日") + "</td>");
                strHTML.Append("</tr>");
                strHTML.Append("<tr>");
                strHTML.Append("<td class=\"name\">备注说明：</td>");
                strHTML.Append("<td>" + entity.Purchase_Intro + "</td>");
                strHTML.Append("</tr>");
                strHTML.Append("</table>");

                strHTML.Append("</div>");
                strHTML.Append("<div class=\"li_box_right\">");
                strHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Purchase_Img, "thumbnail") + "\">");
                strHTML.Append("</div>");
                strHTML.Append("<div class=\"clear\"></div>");
                strHTML.Append("<div class=\"a_box03\"><a href=\"javascript:;\" onclick=\"Show_PurcharseReply_Dialog(" + entity.Purchase_ID + ")\" class=\"a22\">报价留言</a><a href=\"/purchase/purchase_detail.aspx?Purchase_ID=" + entity.Purchase_ID + "\" target=\"_blank\" class=\"a23\">查看详情</a></div>");
                strHTML.Append("</li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div>");

            Response.Write(strHTML.ToString());
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, Pageurl, pageInfo.PageSize, pageInfo.RecordCount);
        }
    }

    public int GetPurchaseCount()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseInfo.Purchase_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseInfo.Purchase_Addtime", "desc"));
        IList<MemberPurchaseInfo> entitys = MyMPurchase.GetMemberPurchases(Query);
        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }

    public MemberPurchaseInfo GetMemberPurchaseInfoByID(int ID)
    {
        return MyMPurchase.GetMemberPurchaseByID(ID);
    }

    public string PurchaseIndex_LeftMemberInfo(int Member_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        MemberInfo memberInfo = MyMem.GetMemberByID(Member_ID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

        if (memberInfo != null && memberInfo.MemberProfileInfo != null)
        {
            strHTML.Append("<dl>");

            strHTML.Append("<dt><img src=\"" + pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_HeadImg, "fullpath") + "\"></dt>");

            strHTML.Append("<dd>");
            strHTML.Append("<p>" + memberInfo.MemberProfileInfo.Member_Company + "</p>");
            strHTML.Append("<p><span>入住时间：</span>" + memberInfo.Member_Addtime.ToString("yyyy年MM月dd日") + "</p>");
            strHTML.Append("<p><span>公司地址：</span>" + myaddr.DisplayAddress(memberInfo.MemberProfileInfo.Member_State, memberInfo.MemberProfileInfo.Member_City, "") + "</p>");
            strHTML.Append("</dd>");

            strHTML.Append("</dl>");
        }

        return strHTML.ToString();
    }

    public string PurchaseIndex_RightMemberInfo(int Member_ID)
    {
        StringBuilder strHTML = new StringBuilder();


        strHTML.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"943\" style=\"padding:10px;\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\">公司名称</td>");
        strHTML.Append("<td></td>");
        strHTML.Append("</tr>");
        strHTML.Append("</table>");

        return strHTML.ToString();
    }


    #endregion

    #region  招商加盟

    public void BrandJoined_List()
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;
        int currPage = tools.CheckInt(Request["page"]);
        if (currPage == 0)
        {
            currPage = 1;
        }
        string page_url = "?action=list";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = currPage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMerchantsInfo.Merchants_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMerchantsInfo.Merchants_SupplierID", "in", "select Supplier_ID from Supplier where Supplier_Trash=0 and Supplier_AuditStatus=1 and Supplier_Site='CN'"));
        Query.OrderInfos.Add(new OrderInfo("SupplierMerchantsInfo.Merchants_AddTime", "desc"));

        IList<SupplierMerchantsInfo> entitys = MyMerchants.GetSupplierMerchantss(Query);
        PageInfo pageInfo = MyMerchants.GetPageInfo(Query);

        if (entitys != null)
        {
            strHTML.Append("<ul>");

            foreach (SupplierMerchantsInfo entity in entitys)
            {
                SupplierInfo supplierInfo = supplier_class.GetSupplierByID(entity.Merchants_SupplierID);

                if (supplierInfo != null)
                {
                    SupplierShopInfo shopInfo = supplier_class.GetSupplierShopBySupplierID(entity.Merchants_SupplierID);

                    if (shopInfo != null)
                    {
                        i++;

                        if (i % 2 == 0)
                        {
                            strHTML.Append("<li class=\"mr0\">");
                        }
                        else
                        {
                            strHTML.Append("<li>");
                        }
                        strHTML.Append("<dl>");
                        strHTML.Append("<dt><a href=\"" + supplier_class.GetShopURL(shopInfo.Shop_Domain) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(shopInfo.Shop_Img, "fullpath") + "\"></a></dt>");
                        strHTML.Append("<dd>");
                        strHTML.Append("<b><a href=\"" + supplier_class.GetShopURL(shopInfo.Shop_Domain) + "\" target=\"_blank\">" + shopInfo.Shop_Name + "</a><i>" + supplier_class.GetSuppleirGrade(shopInfo.Shop_SupplierID) + "</i></b>");
                        strHTML.Append("<p>主营：" + shopInfo.Shop_MainProduct + "</p>");
                        strHTML.Append("</dd>");
                        strHTML.Append("<div class=\"clear\"></div>");
                        strHTML.Append("</dl>");
                        strHTML.Append("<div class=\"table_box\">");
                        strHTML.Append("<table width=\"531\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
                        strHTML.Append("<tr>");
                        strHTML.Append("<td width=\"76\" class=\"name\">品牌：</td>");
                        strHTML.Append("<td width=\"225\">" + supplierInfo.Supplier_CompanyName + "</td>");
                        strHTML.Append("<td width=\"76\" class=\"name\">注册资金：</td>");
                        strHTML.Append("<td width=\"144\"><strong>" + (supplierInfo.Supplier_RegisterFunds == 0 ? "无需验资" : "" + supplierInfo.Supplier_RegisterFunds + "万") + "</strong></td>");
                        strHTML.Append("</tr>");
                        strHTML.Append("<tr>");
                        strHTML.Append("<td class=\"name\">优势：</td>");
                        strHTML.Append("<td>" + entity.Merchants_Advantage + "</td>");
                        strHTML.Append("<td class=\"name\">加盟渠道：</td>");
                        strHTML.Append("<td>" + entity.Merchants_Channel + "</td>");
                        strHTML.Append("</tr>");
                        strHTML.Append("<tr>");
                        strHTML.Append("<td class=\"name\">加盟条件：</td>");
                        strHTML.Append("<td>" + entity.Merchants_Trem + "</td>");
                        strHTML.Append("<td class=\"name\">联系方式：</td>");
                        strHTML.Append("<td>" + supplierInfo.Supplier_Mobile + "</td>");
                        strHTML.Append("</tr>");
                        strHTML.Append("</table>");

                        strHTML.Append("</div>");
                        strHTML.Append("<div class=\"a_box02\"><a href=\"javascript:;\" onclick=\"Show_MerchantsReply_Dialog(" + entity.Merchants_ID + ");\" class=\"a19\">立即代理</a><a href=\"" + supplier_class.GetShopURL(shopInfo.Shop_Domain) + "\" target=\"_blank\" class=\"a20\">查看加盟</a><a href=\"Brand_Detail.aspx?Merchants_ID=" + entity.Merchants_ID + "\" target=\"_blank\" class=\"a21\">查看招商详情</a></div>");
                        strHTML.Append("</li>");
                    }
                }
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div>");
            Response.Write(strHTML.ToString());
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {

        }
    }

    public int BrandJoined_Count()
    {
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMerchantsInfo.Merchants_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierMerchantsInfo.Merchants_AddTime", "desc"));
        PageInfo pageInfo = MyMerchants.GetPageInfo(Query);
        if (pageInfo != null)
        {
            return pageInfo.RecordCount;
        }
        else
        {
            return 0;
        }
    }

    public SupplierMerchantsInfo GetBrandJoinedByID(int id)
    {
        return MyMerchants.GetSupplierMerchantsByID(id);
    }

    #endregion

    #region

    public double GetProductWholeSalePrice(int product_id, int buy_amount)
    {
        double product_price = 0;
        IList<ProductWholeSalePriceInfo> entitys = MySalePrice.GetProductWholeSalePriceByProductID(product_id);
        if (entitys != null)
        {
            foreach (ProductWholeSalePriceInfo entity in entitys)
            {
                if (entity.Product_WholeSalePrice_MinAmount <= buy_amount && buy_amount <= entity.Product_WholeSalePrice_MaxAmount)
                {
                    product_price = entity.Product_WholeSalePrice_Price;
                }
            }
        }
        return product_price;
    }

    #endregion

    /// <summary>
    /// 其他商家再看
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="show_type"></param>
    public void Product_Detail_OtherMEMView(int cateid, int show_type)
    {
        StringBuilder strHTML = new StringBuilder();
        string targetUrl = "";
        SupplierShopInfo shopInfo = null;
        string supplier_name = "";
        string shop_url = "";
        int i = 0;

        QueryInfo Query = new QueryInfo();
        if (show_type == 1)
        {
            Query.PageSize = 5;
        }
        else
        {
            Query.PageSize = 4;
        }
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Get_All_CateProductID(cateid.ToString())));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "desc"));
        IList<ProductInfo> entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                i++;
                targetUrl = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                if (show_type == 1)
                {
                    if (i == entitys.Count)
                    {
                        strHTML.Append("<li class=\"mr0\">");
                    }
                    else
                    {
                        strHTML.Append("<li>");
                    }
                    strHTML.Append("<div class=\"img_box\">");
                    strHTML.Append("<a href=\"" + targetUrl + "\" target=\"_blank\">");
                    strHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a>");
                    strHTML.Append("</div>");
                    strHTML.Append("<p><a style=\"height:18px;display:block;overflow:hidden;\" href=\"" + targetUrl + "\" target=\"_blank\">" + entity.Product_Name + "</a></p>");
                    if (entity.Product_PriceType == 1)
                    {
                        strHTML.Append("<p><strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong></p>");
                    }
                    else
                    {
                        strHTML.Append("<p><strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong></p>");
                    }
                    strHTML.Append("</il>");
                }
                else
                {
                    shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Product_SupplierID);
                    if (shopInfo != null)
                    {
                        supplier_name = shopInfo.Shop_Name;
                        shop_url = "http://" + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                    }

                    if (i == entitys.Count)
                    {
                        strHTML.Append("<li class=\"mr0\">");
                    }
                    else
                    {
                        strHTML.Append("<li>");
                    }
                    strHTML.Append("<div class=\"img_box\">");
                    strHTML.Append("<a href=\"" + targetUrl + "\" target=\"_blank\">");
                    strHTML.Append("<img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a><span><i>24小时备货</i><i>新品</i></span>");
                    strHTML.Append("</div>");

                    strHTML.Append("<div class=\"p_box\">");
                    strHTML.Append("<div class=\"p_box_info01\">");
                    strHTML.Append("<b><i>成交 " + entity.Product_SaleAmount + " 件</i>批发价：");
                    if (entity.Product_PriceType == 1)
                    {
                        strHTML.Append("<strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong></b>");
                    }
                    else
                    {
                        strHTML.Append("<strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong></b>");
                    }
                    strHTML.Append("</div>");
                    strHTML.Append("<div class=\"p_box_info02\"><a  href=\"" + targetUrl + "\" target=\"_blank\">" + entity.Product_Name + "<span>" + entity.Product_Note + "</span></a></div>");
                    strHTML.Append("<div class=\"p_box_info03\">");
                    strHTML.Append("<span><a href=\"" + shop_url + "\" target=\"_blank\">" + supplier_name + "</a><span onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"></span></span>");
                    strHTML.Append("</div>");

                    strHTML.Append("<div class=\"p_box_info04\"><a href=\"/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "\" onclick=\"javascript:return AddCartExt(this);\" class=\"a16\">计入进货单</a><a href=\"javascript:;\" onclick=\"favorites_ajax(" + entity.Product_ID + ")\" class=\"a17\">加入收藏</a></div>");
                    strHTML.Append("</div>");
                    strHTML.Append("</li>");
                }
            }
        }
        Response.Write(strHTML.ToString());
    }

    public void Product_Detail_PriceList(int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0; string strBg = "";
        IList<ProductWholeSalePriceInfo> entitys = MySalePrice.GetProductWholeSalePriceByProductID(Product_ID);

        if (entitys != null)
        {
            foreach (ProductWholeSalePriceInfo entity in entitys)
            {
                i++;

                if (i == 1)
                {
                    strBg = "#ff7872;";
                }
                else if (i == 2)
                {
                    strBg = "#ff7068;";
                }
                else if (i == 3)
                {
                    strBg = "#ee6a66;";
                }

                strHTML.Append("<dd style=\"background-color: " + strBg + "\">" + entity.Product_WholeSalePrice_MinAmount + "-" + entity.Product_WholeSalePrice_MaxAmount + "件<strong>" + pub.FormatCurrency(entity.Product_WholeSalePrice_Price) + "</strong></dd>");

                if (i == 3)
                {
                    break;
                }
            }

            if (i < 3)
            {
                for (int j = 1; j <= i; j++)
                {
                    strHTML.Append("<dd style=\"background-color: #ee6a66;\"></dd>");
                }
            }


        }
        Response.Write(strHTML.ToString());
    }

    public void Product_Detail_DealList(int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;

        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsInfo.Orders_Goods_OrdersID", "in", " select Orders_ID from Orders where Orders_Status=2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_Product_ID", "=", Product_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersGoodsInfo.Orders_Goods_ID", "desc"));
        IList<OrdersGoodsInfo> entitys = MyOrders.GetOrdersGoodsList(Query);

        SupplierInfo supplierInfo = null;
        OrdersInfo ordersInfo = null;
        DateTime orders_addtime = DateTime.Now;
        string Supplier_Name = "";

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\">采购商</td>");
        strHTML.Append("<td class=\"name\">商品规格</td>");
        strHTML.Append("<td class=\"name\">成交单价（元）</td>");
        strHTML.Append("<td class=\"name\">数量</td>");
        strHTML.Append("<td class=\"name\">成交时间</td>");
        strHTML.Append("</tr>");
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                i++;

                ordersInfo = MyOrders.GetOrdersByID(entity.Orders_Goods_OrdersID);
                if (ordersInfo != null)
                {
                    orders_addtime = ordersInfo.Orders_Addtime;
                }

                supplierInfo = Mysupplier.GetSupplierByID(entity.Orders_Goods_Product_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierInfo != null)
                {
                    Supplier_Name = supplierInfo.Supplier_CompanyName;
                }

                if (i % 2 == 0)
                {
                    strHTML.Append("<tr class=\"bg\">");
                }
                else
                {
                    strHTML.Append("<tr>");
                }

                strHTML.Append("<tr>");
                strHTML.Append("<td>" + Supplier_Name + "</td>");
                strHTML.Append("<td>规格：" + entity.Orders_Goods_Product_Spec + "</td>");
                strHTML.Append("<td><strong>" + entity.Orders_Goods_Product_Price + "</strong></td>");
                strHTML.Append("<td><span>" + entity.Orders_Goods_Amount + "</span>累计采购" + entity.Orders_Goods_Amount + "</td>");
                strHTML.Append("<td>" + orders_addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                strHTML.Append("</tr>");
            }
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\">暂无成交记录！</td>");
            strHTML.Append("</tr>");
        }
        strHTML.Append("</table>");

        Response.Write(strHTML.ToString());
    }

    public int Product_Detail_DealCount(int Product_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsInfo.Orders_Goods_OrdersID", "in", " select Orders_ID from Orders where Orders_Status=2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsInfo.Orders_Goods_Product_ID", "=", Product_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("OrdersGoodsInfo.Orders_Goods_ID", "desc"));
        IList<OrdersGoodsInfo> entitys = MyOrders.GetOrdersGoodsList(Query);
        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }



    public void Product_Detail_Extend(ProductInfo productInfo)
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;
        string brand_name = "";

        IList<ProductExtendInfo> productextends = Myproduct.ProductExtendValue(productInfo.Product_ID);


        BrandInfo brandInfo = GetBrandInfoByID(productInfo.Product_BrandID);
        if (brandInfo != null)
        {
            brand_name = brandInfo.Brand_Name;
        }

        strHTML.Append("<div class=\"b33_info\">");
        strHTML.Append("<table width=\"943\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");

        strHTML.Append("<tr>");
        strHTML.Append("<td width=\"104\" class=\"name\">商品名称：</td>");
        strHTML.Append("<td width=\"210\" style=\"overflow:hide;\" title=\"" + productInfo.Product_Name + "\">" + tools.CutStr(productInfo.Product_Name, 25) + "</td>");
        //strHTML.Append("<td width=\"102\" class=\"name\">商品编号：</td>");
        //strHTML.Append("<td width=\"212\">" + productInfo.Product_Code + "</td>");
        //strHTML.Append("<td width=\"102\" class=\"name\">品牌：</td>");
        //strHTML.Append("<td width=\"213\">" + brand_name + "</td>");
        strHTML.Append("</tr>");

        strHTML.Append("<tr>");
        strHTML.Append("<td width=\"104\" class=\"name\">上架时间：</td>");
        strHTML.Append("<td width=\"210\">" + productInfo.Product_Addtime.ToString("yyyy-MM-dd") + "</td>");
        if (productInfo.Product_Weight == 0)
        {
            strHTML.Append("<td width=\"102\"></td>");
            strHTML.Append("<td width=\"212\">");
        }
        else
        {
            strHTML.Append("<td width=\"102\" class=\"name\">重量：</td>");
            strHTML.Append("<td width=\"212\">" + productInfo.Product_Weight + " ");
        }
        if (productInfo.Product_Unit.Length > 0)
        {
            strHTML.Append("(" + productInfo.Product_Unit + ")");

        }


        strHTML.Append("  </td>");
        strHTML.Append("<td width=\"102\" class=\"name\">规格型号：</td>");
        strHTML.Append("<td width=\"213\">" + productInfo.Product_Spec + "</td>");
        strHTML.Append("</tr>");

        if (productextends != null)
        {
            strHTML.Append("<tr>");

            foreach (ProductExtendInfo entity in productextends)
            {
                if (pub.FormatNullToStr(entity.Extend_Value) != "")
                {
                    ProductTypeExtendInfo extend = MyExtend.GetProductTypeExtendByID(entity.Extent_ID);

                    if (extend != null && extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Gather == 0)
                    {
                        i++;
                        if (i == 1 || i == 13)
                        {
                            strHTML.Append("<td width=\"104\" class=\"name\">" + extend.ProductType_Extend_Name + "：</td>");
                            strHTML.Append("<td width=\"210\">" + entity.Extend_Value + "</td>");
                        }
                        else if (i == 2 || i == 14)
                        {
                            strHTML.Append("<td width=\"102\" class=\"name\">" + extend.ProductType_Extend_Name + "：</td>");
                            strHTML.Append("<td width=\"212\">" + entity.Extend_Value + "</td>");
                        }
                        else if (i == 3 || i == 15)
                        {
                            strHTML.Append("<td width=\"102\" class=\"name\">" + extend.ProductType_Extend_Name + "：</td>");
                            strHTML.Append("<td width=\"213\">" + entity.Extend_Value + "</td>");
                        }
                        else
                        {
                            strHTML.Append("<td class=\"name\">" + extend.ProductType_Extend_Name + "：</td>");
                            strHTML.Append("<td >" + entity.Extend_Value + "</td>");
                        }
                    }

                    if (i % 3 == 0)
                    {
                        strHTML.Append("</tr>");
                        strHTML.Append("<tr>");
                    }

                    if (i == 12)
                    {
                        strHTML.Append("</tr>");
                        strHTML.Append("</table>");

                        strHTML.Append("<span style=\"display: none\" id=\"hidden_div\">");
                        strHTML.Append("<table width=\"943\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        strHTML.Append("<tr>");
                    }
                }
            }

            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            strHTML.Append("</span>");
            strHTML.Append("</div>");

            if (i > 12)
            {
                strHTML.Append("<div class=\"blk006\">");
                strHTML.Append("<a id=\"_strHref\" href=\"javascript:show_hiddendiv();\" class=\"a01\">");
                strHTML.Append("<span id=\"_strSpan\">");
                strHTML.Append("<img src=\"/images/icon32.jpg\"></span></a>");
                strHTML.Append("</div>");
            }
        }
        else
        {
            strHTML.Append("</table>");
            strHTML.Append("</div>");
        }
        Response.Write(strHTML.ToString());
    }

    /// <summary>
    /// 更新商品点击数
    /// </summary>
    /// <param name="Product_ID">编号</param>
    public void UpdateProductHits(int Product_ID)
    {
        ProductInfo entity = GetProductByID(Product_ID);
        if (entity != null)
        {
            entity.Product_Hits = entity.Product_Hits + 1;
            Myproduct.EditProductInfo(entity, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d"));
        }
    }

    //促销商品列表
    public void Promotion_Product_List(int Product_Type, IList<PromotionProductInfo> entitys)
    {
        string Product_Arry = "0";
        string Cate_Arry = "";
        int product_counts = 0;
        string Extend_ProductArry = "";
        int icount = 1;
        int irowmax = 5;
        string promotionstr = "";
        int irow;
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";
        if (curr_page < 1)
        {
            curr_page = 1;
        }

        if (entitys != null)
        {
            foreach (PromotionProductInfo ent in entitys)
            {
                Product_Arry = Product_Arry + "," + ent.Promotion_Product_Product_ID;
            }
        }
        page_url = "?id=" + tools.CheckInt(Request["id"]);

        //构建查询条件
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 18;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        if (Product_Type == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Product_Arry));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        PromotionLimitInfo limitinfo;
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products != null)
        {
            string targetURL;
            Response.Write("<ul>");
            foreach (ProductInfo entity in Products)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                Response.Write("<li><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" alt=\"" + entity.Product_Name + "\" /></a>");
                Response.Write("<p class=\"p_name\"><a href=\"" + targetURL + "\" target=\"_blank\">" + entity.Product_Name + "</a></p><p class=\"p_price\">促销价：<span>" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span></p><p class=\"p_btn\">");
                if (entity.Product_UsableAmount > 0 || entity.Product_IsNoStock == 1)
                {
                    //Response.Write("<a href=\"/cart/cart_do.aspx?action=add&product_id=" + entity.Product_ID + "\"><img src=\"/images/btn_buy.jpg\" alt=\"加入到购物车\" width=\"50\" height=\"22\" /></a>");

                    Response.Write("<a href=\"" + targetURL + "\"  target=\"_blank\"><img src=\"/images/btn_buy.jpg\" alt=\"加入到购物车\" width=\"50\" height=\"22\" /></a>");
                }
                else
                {
                    Response.Write("<a><img src=\"/images/btn_nostock.jpg\" alt=\"暂无货\" width=\"50\" height=\"22\" /></a>");
                }
                Response.Write("&nbsp;<a href=\"/supplier/fav_do.aspx?action=product&id=" + entity.Product_ID + "\" target=\"myfav\"><img src=\"/images/shoucang.jpg\" width=\"50\" height=\"22\" alt=\"收藏该商品\" /></a></p> </li>");
            }
            Response.Write("</ul>");
        }

        //输出分页
        Response.Write("<div class=\"clear\"></div><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
        Response.Write("<tr><td height=\"10\"></td></tr>");
        Response.Write("<tr><td align=\"right\">");
        pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
        Response.Write("</td><td width=\"5\"></td></tr>");
        Response.Write("</table>");
    }

    //积分换购列表
    public void Promotion_Product_List()
    {
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        page_url = "?list=list";
        //构建查询条件
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "ASC"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products != null)
        {
            //橱窗
            Response.Write("<div style=\"width:958px; border:1px solid #f1f1f1; \">");
            Response.Write("<div id=\"fenlei-rightlist\" style=\"margin-left:0px;margin-top:20px;\">");
            Response.Write("  <ul>");
            foreach (ProductInfo entity in Products)
            {
                Response.Write("<li style=\"float:left; margin-left:13px;margin-bottom:20px;\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img style=\"border:1px solid #D0D0D0;\" src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" alt=\"" + entity.Product_Name + "\" width=\"120\" height=\"120\" /></a>");
                Response.Write("<p class=\"wenzi\">" + tools.CutStr(entity.Product_Name, 20, "") + "</p><p><span>" + entity.Product_CoinBuy_Coin.ToString() + Application["Coin_Name"].ToString() + "</span></p><p>");

                Response.Write("<a href=\"/cart/cart_do.aspx?action=add_coinbuy&product_id=" + entity.Product_ID + "\"><img src=\"/images/btn_exchange.jpg\" alt=\"加入到购物车\" width=\"50\" height=\"22\" /></a></p> </li>");
            }
            Response.Write("  </ul>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("</div>");

            //Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            //Response.Write("<tr><td height=\"10\"></td></tr>");
            //Response.Write("<tr><td align=\"right\">");
            //pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            //Response.Write("</td><td width=\"5\"></td></tr>");
            //Response.Write("</table>");
            Response.Write("</div>");
        }

        //输出分页
    }

    //新品上架
    public void New_Product_List(string uses, int cate_id, int irowmax)
    {
        string orderby;
        int isgallerylist;
        string keyword = Request["keyword"];
        string Product_Arry = "";
        int product_counts = 0;
        int product_count = 0;
        int icount = 1;
        int irow;
        string promotionstr = "";
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        page_url = page_url + "?isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);
        //if (orderby == "")
        //{
        //    orderby = "time_down";
        //}
        page_url = page_url + "&orderby=" + orderby;
        //关键词
        if ((cate_id > 0 && Product_Arry != "") || cate_id == 0)
        {
            //构建查询条件
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 32;
            Query.CurrentPage = curr_page;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            PromotionLimitInfo limitinfo;
            IList<ProductInfo> Products = GetCateTagProduct(0, cate_id, "新品上架");
            string product_ids = "0";
            if (Products != null)
            {
                foreach (ProductInfo entity1 in Products)
                {
                    product_ids += "," + entity1.Product_ID;
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_ids));
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
                IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                product_counts = 1;
                Response.Write("<div class=\"prolist_right3\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px\">&nbsp;新品上架</td></tr></table></h3>");
                irow = 1;
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"color:#666666;\">");
                Response.Write("  <tr>");
                foreach (ProductInfo entity in entitys)
                {
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\">");
                    //limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                    //if (limitinfo != null)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_limit.gif\" border=\"0\" width=\"57\" height=\"57\"></a></div>";
                    //}
                    //Response.Write(promotionstr);
                    promotionstr = "";
                    Response.Write("<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"170\" align=\"center\" valign=\"middle\">");
                    //if (entity.Product_IsGroupBuy == 1)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_groupbuy.gif\" border=\"0\"></a></div>";
                    //}
                    Response.Write("<div style=\"height:170;wdith:170;\" ><table width=\"128px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td height=\"170\" align=\"center\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\" width=\"170\" height=\"170\" alt=\"" + entity.Product_Name + "\" onload=\"javascript:AutosizeImage(this,170,170);\"></a></td></tr></table></div></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"36\" align=\"center\" style=\"line-height:20px\">");
                    Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" class=\"a_t12_grey\" target=\"_blank\">" + tools.CutStr(pub.Format_Product_Name(entity.Product_Name, entity.Product_Note), 14).Replace("<span class=\"t12_red\">", "<span class=\"t12_red\" style=\"color:" + entity.Product_NoteColor + ";\">"));
                    Response.Write("</a> ");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"25\" align=\"center\">");
                    switch (uses)
                    {
                        case "coinbuy":
                            Response.Write(entity.Product_CoinBuy_Coin + "<span>" + Application["Coin_Name"]);
                            break;
                        default:
                            limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                            if (limitinfo != null)
                            {
                                Response.Write("限时价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(limitinfo.Promotion_Limit_Price) + "</span>");
                            }
                            else
                            {
                                Response.Write("易种价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span>");
                            }
                            break;
                    }
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    //if (uses == "groupbuy")
                    //{
                    //    Response.Write("      <tr>");
                    //    Response.Write("        <td height=\"25\" align=\"center\" class=\"t12\">");
                    //    Response.Write("        最低批发数量" + entity.Product_GroupNum + entity.Product_Unit);
                    //    Response.Write("        </td>");
                    //    Response.Write("      </tr>");
                    //}

                    product_count = entity.Product_UsableAmount;

                    Response.Write("    </table></td>");
                    irow = irow + 1;
                    icount = icount + 1;
                    if (irow > irowmax)
                    {
                        irow = 1;
                        Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"5\"></td></tr><tr>");
                    }
                }
                for (irow = irow; irow <= irowmax; irow++)
                {
                    Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
                }
                Response.Write("  </tr>");
                Response.Write("</table>");
                //输出分页
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("<div class=\"prolist_page\">");
                Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                Response.Write("<tr><td height=\"10\"></td></tr>");
                Response.Write("<tr><td align=\"right\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</td></tr>");
                Response.Write("</table>");
                Response.Write("</div>");
                Response.Write("</div>");
            }
        }

        //如果有产品记录
        if (product_counts == 0)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\"><img src=\"/images/icon_alert.gif\"> 抱歉，没有找到符合条件的商品！ </td></tr>");
            Response.Write("</table>");
        }
    }

    //疯狂促销
    public void Promotion_Product_List(string uses, int show_num, int cate_id, int irowmax, string Cate_Name, string Favor_Css)
    {
        string orderby;
        int isgallerylist;
        string keyword = Request["keyword"];
        string Product_Arry = "";
        int product_counts = 0;
        int product_count = 0;

        int icount = 1;
        int irow;
        string promotionstr = "";
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        page_url = page_url + "?isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);
        //if (orderby == "")
        //{
        //    orderby = "time_down";
        //}
        page_url = page_url + "&orderby=" + orderby;
        //关键词


        //构建查询条件
        QueryInfo Query = new QueryInfo();
        Query.PageSize = show_num;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        PromotionLimitInfo limitinfo;
        IList<ProductInfo> Products = GetCateTagProduct(0, cate_id, "疯狂促销");
        string product_ids = "0";
        if (Products != null)
        {
            foreach (ProductInfo entity1 in Products)
            {
                product_ids += "," + entity1.Product_ID;
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_ids));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
            IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            product_counts = 1;

            Response.Write("<div class=\"prolist_right3\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px\">&nbsp;" + Cate_Name + "疯狂促销</td></tr></table></h3>");
            irow = 1;
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"color:#666666;\">");
            Response.Write("  <tr>");
            foreach (ProductInfo entity in entitys)
            {
                Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\">");

                promotionstr = "";
                Response.Write("<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                Response.Write("      <tr>");
                Response.Write("        <td height=\"180\" align=\"center\" valign=\"middle\">");

                Response.Write("<div style=\"height:170;wdith:170;\" ><table width=\"128px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td height=\"170\" align=\"center\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\" width=\"170\" height=\"170\" alt=\"" + entity.Product_Name + "\" onload=\"javascript:AutosizeImage(this,170,170);\" onmouseover=\"setover($(this));\" onmouseout=\"setout($(this));\"></a></td></tr></table></div></td>");
                Response.Write("      </tr>");
                Response.Write("      <tr>");
                Response.Write("        <td height=\"36\" align=\"center\" style=\"line-height:20px\">");
                Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" class=\"a_t12_grey\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(pub.Format_Product_Name(entity.Product_Name, entity.Product_Note), 26).Replace("<span class=\"t12_red\">", "<span class=\"t12_red\" style=\"color:" + entity.Product_NoteColor + ";\">"));
                Response.Write("</a> ");
                Response.Write("        </td>");
                Response.Write("      </tr>");
                Response.Write("      <tr>");
                Response.Write("        <td height=\"25\" align=\"center\">");
                double Product_Price = Get_Member_Price(entity.Product_ID, entity.Product_Price);
                if (Product_Price > 0)
                {
                    Response.Write("      <div style=\"float:left\" class=\"" + Favor_Css + "\">" + Math.Round((double)(entity.Product_MKTPrice / Product_Price), 1) + "折</div><div style=\"width:95px;float:right;text-align:left\">");
                }
                else
                {
                    Response.Write("      <div style=\"float:left\" class=\"" + Favor_Css + "\">0折</div><div style=\"width:95px;float:right;text-align:left\">");
                }
                limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                //Response.Write("市场价 <span style=\"color:#cc0000;\">" + entity.Product_MKTPrice + "</span><br>");
                if (limitinfo != null)
                {
                    Response.Write("限时价 <span style=\"color:#cc0000;\">" + limitinfo.Promotion_Limit_Price + "</span>");
                }
                else
                {
                    Response.Write("本站价 <span style=\"color:#cc0000;\">" + Product_Price + "</span>");
                }
                Response.Write("        </div></td>");
                Response.Write("      </tr>");

                Response.Write("    </table></td>");
                irow = irow + 1;
                icount = icount + 1;
                if (irow > irowmax)
                {
                    irow = 1;
                    Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"5\"></td></tr><tr>");
                }

            }
            for (irow = irow; irow <= irowmax; irow++)
            {
                Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
            }
            Response.Write("  </tr>");
            Response.Write("</table>");
            Response.Write("<div class=\"clear\"></div>");

            Response.Write("</div>");
        }



    }

    //热卖商品
    public void Hot_Product_List(string uses, int cate_id, int irowmax)
    {
        string orderby;
        int isgallerylist;
        string keyword = Request["keyword"];
        string Product_Arry = "";
        int product_counts = 0;
        int product_count = 0;

        int icount = 1;
        int irow;
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        page_url = page_url + "?isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);
        //if (orderby == "")
        //{
        //    orderby = "time_down";
        //}
        page_url = page_url + "&orderby=" + orderby;
        //关键词

        if ((cate_id > 0 && Product_Arry != "") || cate_id == 0)
        {
            //构建查询条件
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 32;
            Query.CurrentPage = curr_page;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            PromotionLimitInfo limitinfo;
            IList<ProductInfo> Products = GetCateTagProduct(0, cate_id, "热卖推荐");
            string product_ids = "0";
            if (Products != null)
            {
                foreach (ProductInfo entity1 in Products)
                {
                    product_ids += "," + entity1.Product_ID;
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_ids));
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
                IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                product_counts = 1;

                Response.Write("<div class=\"prolist_right3\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px\">&nbsp;热卖商品</td></tr></table></h3>");
                irow = 1;
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"color:#666666;\">");
                Response.Write("  <tr>");
                foreach (ProductInfo entity in entitys)
                {
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\">");
                    //limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                    //if (limitinfo != null)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_limit.gif\" border=\"0\" width=\"57\" height=\"57\"></a></div>";
                    //}

                    //Response.Write(promotionstr);
                    Response.Write("<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"170\" align=\"center\" valign=\"middle\">");


                    //if (entity.Product_IsGroupBuy == 1)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_groupbuy.gif\" border=\"0\"></a></div>";
                    //}

                    Response.Write("<div style=\"height:170;wdith:170;\" ><table width=\"128px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td height=\"170\" align=\"center\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\" width=\"170\" height=\"170\" alt=\"" + entity.Product_Name + "\" onload=\"javascript:AutosizeImage(this,170,170);\"></a></td></tr></table></div></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"36\" align=\"center\" style=\"line-height:20px\">");
                    Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" class=\"a_t12_grey\" target=\"_blank\">" + tools.CutStr(pub.Format_Product_Name(entity.Product_Name, entity.Product_Note), 14).Replace("<span class=\"t12_red\">", "<span class=\"t12_red\" style=\"color:" + entity.Product_NoteColor + ";\">"));
                    Response.Write("</a> ");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"25\" align=\"center\">");
                    switch (uses)
                    {
                        case "coinbuy":
                            Response.Write(entity.Product_CoinBuy_Coin + "<span>" + Application["Coin_Name"]);
                            break;
                        default:
                            limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                            if (limitinfo != null)
                            {
                                Response.Write("限时价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(limitinfo.Promotion_Limit_Price) + "</span>");
                            }
                            else
                            {
                                Response.Write("易种价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span>");
                            }
                            break;
                    }

                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    //if (uses == "groupbuy")
                    //{
                    //    Response.Write("      <tr>");
                    //    Response.Write("        <td height=\"25\" align=\"center\" class=\"t12\">");
                    //    Response.Write("        最低批发数量" + entity.Product_GroupNum + entity.Product_Unit);
                    //    Response.Write("        </td>");
                    //    Response.Write("      </tr>");
                    //}

                    product_count = entity.Product_UsableAmount;

                    Response.Write("    </table></td>");
                    irow = irow + 1;
                    icount = icount + 1;
                    if (irow > irowmax)
                    {
                        irow = 1;
                        Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"5\"></td></tr><tr>");
                    }

                }
                for (irow = irow; irow <= irowmax; irow++)
                {
                    Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
                }
                Response.Write("  </tr>");
                Response.Write("</table>");
                //输出分页
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("<div class=\"prolist_page\">");
                Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                Response.Write("<tr><td height=\"10\"></td></tr>");
                Response.Write("<tr><td align=\"right\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</td></tr>");
                Response.Write("</table>");
                Response.Write("</div>");
                Response.Write("</div>");
            }
        }

        //如果有产品记录
        if (product_counts == 0)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\"><img src=\"/images/icon_alert.gif\"> 抱歉，没有找到符合条件的商品！ </td></tr>");
            Response.Write("</table>");
        }
    }

    //会员独享
    public void Member_Product_List(string uses, int cate_id, int irowmax)
    {
        string orderby;
        int isgallerylist;
        string keyword = Request["keyword"];
        string Product_Arry = "";
        int product_counts = 0;
        int product_count = 0;

        int icount = 1;
        int irow;
        string promotionstr = "";
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "";

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        //显示方式
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        page_url = page_url + "?isgallerylist=" + isgallerylist;
        //排序方式
        orderby = Request["orderby"];
        orderby = tools.CheckStr(orderby);
        //if (orderby == "")
        //{
        //    orderby = "time_down";
        //}
        page_url = page_url + "&orderby=" + orderby;
        //关键词

        if ((cate_id > 0 && Product_Arry != "") || cate_id == 0)
        {
            //构建查询条件
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 32;
            Query.CurrentPage = curr_page;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            PromotionLimitInfo limitinfo;
            IList<ProductInfo> Products = GetCateTagProduct(0, cate_id, "会员独享");
            string product_ids = "0";
            if (Products != null)
            {
                foreach (ProductInfo entity1 in Products)
                {
                    product_ids += "," + entity1.Product_ID;
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_ids));
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
                IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                product_counts = 1;

                Response.Write("<div class=\"prolist_right3\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:5px; font-size:14px\">&nbsp;会员独享</td></tr></table></h3>");
                irow = 1;
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"color:#666666;\">");
                Response.Write("  <tr>");
                foreach (ProductInfo entity in entitys)
                {
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\">");
                    //limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                    //if (limitinfo != null)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_limit.gif\" border=\"0\" width=\"57\" height=\"57\"></a></div>";
                    //}

                    //Response.Write(promotionstr);
                    promotionstr = "";
                    Response.Write("<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"170\" align=\"center\" valign=\"middle\">");

                    //if (entity.Product_IsGroupBuy == 1)
                    //{
                    //    promotionstr = "<div class=\"product_ico\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"/images/icon_groupbuy.gif\" border=\"0\"></a></div>";
                    //}
                    Response.Write("<div style=\"height:170;wdith:170;\" ><table width=\"128px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td height=\"170\" align=\"center\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" border=\"0\" width=\"170\" height=\"170\" alt=\"" + entity.Product_Name + "\" onload=\"javascript:AutosizeImage(this,170,170);\"></a></td></tr></table></div></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"36\" align=\"center\" style=\"line-height:20px\">");
                    Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" class=\"a_t12_grey\" target=\"_blank\">" + tools.CutStr(pub.Format_Product_Name(entity.Product_Name, entity.Product_Note), 14).Replace("<span class=\"t12_red\">", "<span class=\"t12_red\" style=\"color:" + entity.Product_NoteColor + ";\">"));
                    Response.Write("</a> ");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"25\" align=\"center\">");
                    switch (uses)
                    {
                        case "coinbuy":
                            Response.Write(entity.Product_CoinBuy_Coin + "<span>" + Application["Coin_Name"]);
                            break;
                        default:
                            limitinfo = pub.GetPromotionLimitByProductID(entity.Product_ID);
                            if (limitinfo != null)
                            {
                                Response.Write("限时价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(limitinfo.Promotion_Limit_Price) + "</span>");
                            }
                            else
                            {
                                Response.Write("易种价：<span style=\"color:#cc0000;\">" + pub.FormatCurrency(Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</span>");
                            }
                            break;
                    }

                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    //if (uses == "groupbuy")
                    //{
                    //    Response.Write("      <tr>");
                    //    Response.Write("        <td height=\"25\" align=\"center\" class=\"t12\">");
                    //    Response.Write("        最低批发数量" + entity.Product_GroupNum + entity.Product_Unit);
                    //    Response.Write("        </td>");
                    //    Response.Write("      </tr>");
                    //}
                    product_count = entity.Product_UsableAmount;
                    Response.Write("    </table></td>");
                    irow = irow + 1;
                    icount = icount + 1;
                    if (irow > irowmax)
                    {
                        irow = 1;
                        Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"5\"></td></tr><tr>");
                    }

                }
                for (irow = irow; irow <= irowmax; irow++)
                {
                    Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
                }
                Response.Write("  </tr>");
                Response.Write("</table>");

                //输出分页
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("<div class=\"prolist_page\">");
                Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                Response.Write("<tr><td height=\"10\"></td></tr>");
                Response.Write("<tr><td align=\"right\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</td></tr>");
                Response.Write("</table>");
                Response.Write("</div>");
                Response.Write("</div>");
            }
        }

        //如果有产品记录
        if (product_counts == 0)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\"><img src=\"/images/icon_alert.gif\"> 抱歉，没有找到符合条件的商品！ </td></tr>");
            Response.Write("</table>");
        }
    }

    //产品名称列表
    public void GetProductNameList()
    {
        int i = 0;
        int irow, icount;
        string Product_Names = "";
        icount = 0;
        string hkeyword = "";
        hkeyword = tools.CheckStr(Request["hkeyword"]);
        string page_url = "";
        if (hkeyword == "输入需要查询的关键词")
        {
            hkeyword = "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        IList<ProductInfo> entitys = null;

        if (hkeyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", hkeyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_NameInitials", "like", hkeyword));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
            entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        }


        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                if (Product_Names.Length == 0)
                {
                    Product_Names = "<a href=\"javascript:void(0);\" onclick=\"$('#top_keyword').val('" + entity.Product_Name + "');$('#div_kselect_drag').hide();\">" + tools.CutStr(entity.Product_Name, 30) + "</a>";
                }
                else
                {
                    Product_Names += "<br><a href=\"javascript:void(0);\" onclick=\"$('#top_keyword').val('" + entity.Product_Name + "');$('#div_kselect_drag').hide();\">" + tools.CutStr(entity.Product_Name, 30) + "</a>";
                }
            }
        }
        Response.Write(Product_Names);

    }

    //实体店铺列表
    public void GetSupplierShopsNameList()
    {
        int i = 0;
        int irow, icount;
        string Shop_Names = "";
        icount = 0;
        string hkeyword = "";
        hkeyword = tools.CheckStr(Request["hkeyword"]);
        string page_url = "";
        if (hkeyword == "输入需要查询的关键词")
        {
            hkeyword = "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = 1;
        IList<SupplierShopInfo> entitys = null;

        if (hkeyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Name", "like", hkeyword));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "DESC"));
            entitys = MyShop.GetSupplierShops(Query);
        }


        if (entitys != null)
        {
            foreach (SupplierShopInfo entity in entitys)
            {
                if (Shop_Names.Length == 0)
                {
                    Shop_Names = "<a href=\"javascript:void(0);\" onclick=\"$('#hkeyword').val('" + entity.Shop_Name + "');$('#div_select_drag').hide();\">" + tools.CutStr(entity.Shop_Name, 30) + "</a>";
                }
                else
                {
                    Shop_Names += "<br><a href=\"javascript:void(0);\" onclick=\"$('#hkeyword').val('" + entity.Shop_Name + "');$('#div_select_drag').hide();\">" + tools.CutStr(entity.Shop_Name, 30) + "</a>";
                }
            }
        }
        Response.Write(Shop_Names);

    }

    //首页商家列表
    public void GetSupplierShops()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_Recommend", "DESC"));
        IList<SupplierShopInfo> entitys = MyShop.GetSupplierShops(Query);
        if (entitys != null)
        {
            foreach (SupplierShopInfo entity in entitys)
            {
                Response.Write("<li><a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\"  /></a><p><a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\">" + entity.Shop_Name + "</a></li>");
            }
        }
    }

    //实体店铺列表
    public void GetSupplierShopsList(int Cate_ID, int Type_id, int irowmax, string Cate_Name)
    {
        int i = 0;
        int irow, icount;

        icount = 0;
        int curr_page = tools.CheckInt(Request["page"]);
        string hkeyword = "";
        hkeyword = tools.CheckStr(Request["hkeyword"]);
        string page_url = "";
        if (hkeyword == "输入需要查询的关键词")
        {
            hkeyword = "";
        }
        if (curr_page < 1)
        {
            curr_page = 1;
        }
        Response.Write("<div class=\"prolist_right3\"><h3><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td width=\"25px\" align=\"right\"><img src=\"/images/icon_filter1.jpg\" align=\"absmiddle\" style=\"padding-top:5px;\"/></td><td style=\"color:#cc0000; padding-top:0px; font-size:14px\">&nbsp;" + Cate_Name + "</td><td align=\"right\" height=\"28\"><a href=\"shops_list.aspx?type=" + Type_id + "&Cate_ID=" + Cate_ID + "\" class=\"a_t12_blue\">更多</a>&nbsp; </td></tr></table></h3>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", "CN"));
        if (Cate_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_ID", "in", "select shop_cate_shopid from supplier_shop_category where shop_cate_cateid in (" + Get_All_SubCate(Cate_ID) + ")"));
        }
        if (hkeyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Name", "like", hkeyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Type", "=", Type_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "DESC"));
        PageInfo pageinfo = MyShop.GetPageInfo(Query);
        IList<SupplierShopInfo> entitys = MyShop.GetSupplierShops(Query);
        if (entitys != null)
        {

            irow = 1;
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"color:#666666;\">");
            Response.Write("  <tr>");
            foreach (SupplierShopInfo entity in entitys)
            {
                Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\">");
                Response.Write("<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                Response.Write("      <tr>");
                Response.Write("        <td height=\"170\" align=\"center\" valign=\"middle\">");

                Response.Write("<div style=\"height:170;wdith:170;\" ><table width=\"128px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td height=\"170\" align=\"center\" valign=\"middle\"><a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\" border=\"0\" width=\"170\" height=\"170\" alt=\"" + entity.Shop_Name + "\" onload=\"javascript:AutosizeImage(this,170,170);\"></a></td></tr></table></div></td>");
                Response.Write("      </tr>");
                Response.Write("      <tr>");
                Response.Write("        <td height=\"36\" align=\"center\" style=\"line-height:20px\">");
                Response.Write("<a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\" class=\"a_t12_grey\" target=\"_blank\">" + tools.CutStr(entity.Shop_Name, 14));
                Response.Write("</a> ");
                Response.Write("        </td>");
                Response.Write("      </tr>");
                Response.Write("    </table></td>");
                irow = irow + 1;
                icount = icount + 1;
                if (irow > irowmax)
                {
                    irow = 1;
                    Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"5\"></td></tr><tr>");
                }

            }
            for (irow = irow; irow <= irowmax; irow++)
            {
                Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
            }
            Response.Write("  </tr>");
            Response.Write("</table>");




        }
        else
        {
            Response.Write("<div style=\"line-height:35px; text-align:center\">暂无相关店铺</div>");
        }
        Response.Write("<div class=\"clear\"></div>");

        Response.Write("</div>");


    }



    public int GetSupplierShopsCount()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        //int search_type_id = tools.CheckInt(Request["search_type_id"]);
        //int curr_page = tools.CheckInt(Request["page"]);
        //string page_url = "/product/shop.aspx?search_type_id=" + search_type_id;

        //if (curr_page < 1)
        //{
        //    curr_page = 1;
        //}

        QueryInfo Query = new QueryInfo();
        //Query.PageSize = 10;
        //Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", pub.GetCurrentSite()));
        if (keyword != "")
        {
            //page_url += "&keyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "DESC"));
        PageInfo pageinfo = MyShop.GetPageInfo(Query);

        if (pageinfo == null)
        {
            return 0;
        }
        else
        {
            return pageinfo.RecordCount;
        }

    }


    //实体店铺列表
    public void GetSupplierShops_List()
    {

        string keyword = tools.CheckStr(Request["keyword"]);
        int search_type_id = tools.CheckInt(Request["search_type_id"]);
        int curr_page = tools.CheckInt(Request["page"]);
        string page_url = "/product/shop.aspx?search_type_id=" + search_type_id;

        if (curr_page < 1)
        {
            curr_page = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curr_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", "CN"));
        if (keyword != "")
        {
            page_url += "&keyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "DESC"));
        PageInfo pageinfo = MyShop.GetPageInfo(Query);
        IList<SupplierShopInfo> entitys = MyShop.GetSupplierShops(Query);



        StringBuilder sb = new StringBuilder();

        sb.Append("<!--排序 开始-->");
        sb.Append("<div class=\"blk05\">");
        sb.Append("<div class=\"blk05_info01\"><strong>全部店铺</strong></div>");
        sb.Append("<div class=\"blk05_info02\">");
        sb.Append("<form action=\"\" method=\"get\">");
        sb.Append("<ul>");
        sb.Append("<li class=\"one\">所有地区</li>");
        sb.Append("</ul>");
        sb.Append("<div class=\"clear\"></div>");
        sb.Append("</form>");
        sb.Append("</div>");
        sb.Append("</div>");



        if (entitys != null)
        {
            sb.Append("<div class=\"list_box\">");



            //Response.Write("<div class=\"yz_blk14_info02\">");
            foreach (SupplierShopInfo entity in entitys)
            {

                sb.Append("<div class=\"list_info\">");
                sb.Append("<div class=\"list_info_01\"><a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\" width=\"133\" height=\"133\" onload=\"javascript:AutosizeImage(this,133,133);\"/></a></div>");
                sb.Append("<div class=\"list_info_02\">");
                sb.Append("<b><a href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\" target=\"_blank\">" + entity.Shop_Name + "</a></b>");
                sb.Append("<p>主营产品：" + entity.Shop_MainProduct + "</p>");
                sb.Append("</div>");
                sb.Append("<div class=\"list_info04\">");
                sb.Append("<a  href=\"" + supplier_class.GetShopURL(entity.Shop_Domain) + "\" target=\"_blank\" class=\"a03\">进入店铺</a>");
                sb.Append("<a  href=\"/supplier/fav_do.aspx?action=shop&id=" + entity.Shop_ID + "\" target=\"_blank\" class=\"a04\">加入收藏</a>");
                sb.Append("</div>");
                sb.Append("<div class=\"clear\"></div>");
                sb.Append("</div>");

            }

            Response.Write(sb.ToString());
            Response.Write("</div>");

            Response.Write("<div class=\"page\" style=\"float:right;\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");

        }
        else
        {
            Response.Write("<div style=\"line-height:35px; text-align:center\">暂无相关店铺</div>");
        }

        //Response.Write("<div class=\"clear\"></div>");
        //Response.Write("</div>");
    }


    public int GetSupplierShopProductAmount(int supplier_id)
    {
        int flag = 0;
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));

        PageInfo pageinfo = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (pageinfo != null)
        {
            flag = pageinfo.RecordCount;
        }
        return flag;
    }

    //实体店铺左侧类别 /**/
    public string Supplier_Left_Cate(int Cate_ID, int Parent_ID)
    {
        string Cate_Arry = "";
        Cate_Arry += "<a href=\"javascript:void(0);\" style=\"text-decoration:none;cursor:pointer;\"><h4>产品分类</h4></a>";
        Cate_Arry += "  <div id=\"help-zong\">";
        int cate_id_parent = 0;
        CategoryInfo categoryinfo = MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (categoryinfo != null)
        {
            cate_id_parent = categoryinfo.Cate_ParentID;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Parent_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            Cate_Arry = Cate_Arry + "<div class=\"category-list1\">";

            foreach (CategoryInfo entity in Categorys)
            {
                if (cate_id_parent == entity.Cate_ID || Cate_ID == entity.Cate_ID || cate_id_parent == 0)
                {
                    Cate_Arry = Cate_Arry + "<div style=\"cursor:pointer; margin-bottom:2px;\" onclick=\"GetCateDivStyle('" + entity.Cate_ID + "');\"><h3><span><img id=\"div_img_" + entity.Cate_ID + "\" src=\"../images/sub.gif\" width=\"9\" height=\"9\" /></span><a href=\"javascript:;\" style=\"cursor:pointer;text-decoration:none;color:#cc0000;\" onclick=\"javascript:location.href='shops.aspx?cate_id=" + entity.Cate_ID + "'\">" + (entity.Cate_Name.Length > 7 ? entity.Cate_Name.Substring(0, 7) : entity.Cate_Name) + "</a></h3></div>";
                }
                else
                {
                    Cate_Arry = Cate_Arry + "<div style=\"cursor:pointer;margin-bottom:2px;\" onclick=\"GetCateDivStyle('" + entity.Cate_ID + "');\"><h3><span><img id=\"div_img_" + entity.Cate_ID + "\" src=\"../images/subj.gif\" width=\"9\" height=\"9\" /></span><a href=\"javascript:;\" style=\"cursor:pointer;text-decoration:none;color:#cc0000;\" onclick=\"javascript:location.href='shops.aspx?cate_id=" + entity.Cate_ID + "'\">" + (entity.Cate_Name.Length > 7 ? entity.Cate_Name.Substring(0, 7) : entity.Cate_Name) + "</a></h3></div>";
                }

                Cate_Arry = Cate_Arry + Supplier_List_SubCate(entity.Cate_ID, Cate_ID, cate_id_parent);
            }
            Cate_Arry = Cate_Arry + "</div>";
        }
        Cate_Arry = Cate_Arry + "</div>";

        return Cate_Arry;
    }

    public string Supplier_List_SubCate(int Cate_ID, int id, int cate_id_parent)
    {
        string HTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys != null)
        {
            HTML += "<div width=\"168px\" class=\"left_cate_style\" id=\"div_Cate_ID_" + Cate_ID + "\"";
            if (id != Cate_ID && Cate_ID != cate_id_parent && cate_id_parent != 0)
            {
                HTML += " style=\"float:left; margin-left:10px;display:none; padding-top:5px; padding-bottom:5px;\"";
            }
            else
            {
                HTML += "style=\"float:left; margin-left:10px; padding-top:5px; padding-bottom:5px;\"";
            }
            HTML += "><ul>";
            foreach (CategoryInfo entity in Categorys)
            {
                if (entity.Cate_ID == id)
                {
                    HTML += "<li style=\"line-height:28px;width:84px;\"><a href=\"shops.aspx?cate_id=" + entity.Cate_ID + "\" style=\"color:#cc0000;\">" + entity.Cate_Name + "</a></li>";
                }
                else
                {
                    HTML += "<li style=\"line-height:28px; width:84px;\"><a href=\"shops.aspx?cate_id=" + entity.Cate_ID + "\">" + entity.Cate_Name + "</a></li>";
                }
            }
            HTML += "</ul></div>";
            HTML += "<div class=\"clear\"></div>";
        }
        return HTML;
    }

    /// <summary>
    /// 首页评论信息
    /// </summary>
    public void Home_Product_Review()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "DESC"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews != null)
        {
            string ProName, ProImg, ProURL;
            RBACUserInfo UserInfo = pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8");
            ProductInfo ProEntity = null;
            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                ProEntity = Myproduct.GetProductByID(entity.Shop_Evaluate_Productid, UserInfo);
                if (ProEntity != null)
                {
                    ProName = ProEntity.Product_Name;
                    ProImg = pub.FormatImgURL(ProEntity.Product_Img, "thumbnail");
                    ProURL = pageurl.FormatURL(pageurl.product_detail, ProEntity.Product_ID.ToString());
                }
                else
                {
                    ProName = ProImg = ProURL = string.Empty;
                }


                strHTML.Append("<dl >");

                strHTML.Append("	<dt><a href=\"" + ProURL + "\" target=\"_blank\" title=\"" + ProName + "\" ><img src=\"" + ProImg + "\" /></a></dt>");
                strHTML.Append("	<dd>");
                strHTML.Append("		<b><a href=\"" + ProURL + "\" target=\"_blank\">" + ProName + "</a></b>");
                strHTML.Append("		<p>" + entity.Shop_Evaluate_Note + "</p>");
                strHTML.Append("	</dd>");
                strHTML.Append("	<div class=\"clear\"></div>");
                strHTML.Append("</dl>");

            }
        }

        Response.Write(strHTML.ToString());
    }

    /// <summary>
    /// 产品询价
    /// </summary>
    public void AddSupplierPriceAsk()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int product_id = tools.CheckInt(Request["product_id"]);

        string PriceAsk_Title = tools.CheckStr(Request["PriceAsk_Title"]);
        string PriceAsk_Name = tools.CheckStr(Request["PriceAsk_Name"]);
        string PriceAsk_Phone = tools.CheckStr(Request["PriceAsk_Phone"]);

        int PriceAsk_Quantity = tools.CheckInt(Request["PriceAsk_Quantity"]);
        double PriceAsk_Price = tools.CheckFloat(Request["PriceAsk_Price"]);
        string PriceAsk_DeliveryDate = tools.CheckStr(Request["PriceAsk_DeliveryDate"]);
        string PriceAsk_Content = tools.CheckStr(Request["PriceAsk_Content"]);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (verifycode != Session["Trade_Verify"].ToString() && verifycode.Length == 0)
        {
            pub.Msg("info", "信息提示", "验证码输入错误", false, "{back}");
        }

        if (PriceAsk_Title == "")
        {
            pub.Msg("info", "提示信息", "请填写询价标题", false, "{back}");
        }
        if (PriceAsk_Name == "")
        {
            pub.Msg("info", "提示信息", "请填写联系人名称", false, "{back}");
        }
        if (PriceAsk_Phone.Length > 0)
        {
            if (!pub.Checkmobile(PriceAsk_Phone))
            {
                pub.Msg("info", "提示信息", "联系手机号码格式错误", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "提示信息", "请填写联系手机号码", false, "{back}");
        }

        if (PriceAsk_Quantity == 0)
        {
            pub.Msg("info", "提示信息", "请填写意向采购数量", false, "{back}");
        }
        if (PriceAsk_Price == 0)
        {
            pub.Msg("info", "提示信息", "请填写意向采购价格", false, "{back}");
        }
        if (PriceAsk_DeliveryDate == "")
        {
            pub.Msg("info", "提示信息", "请选择期望交货时间", false, "{back}");
        }
        if (PriceAsk_Content == "")
        {
            pub.Msg("info", "提示信息", "请填写询价内容", false, "{back}");
        }
        ProductInfo productinfo = GetProductByID(product_id);
        if (productinfo != null)
        {
            if (productinfo.Product_SupplierID == supplier_id)
            {
                Response.Redirect("/product/category.aspx");
            }

            if (productinfo.Product_IsAudit != 1 || productinfo.Product_IsInsale != 1)
            {
                Response.Redirect("/product/category.aspx");
            }

            DateTime now = DateTime.Now;
            SupplierPriceAskInfo spriceaskinfo = new SupplierPriceAskInfo();
            spriceaskinfo.PriceAsk_Title = PriceAsk_Title;
            spriceaskinfo.PriceAsk_Name = PriceAsk_Name;
            spriceaskinfo.PriceAsk_Phone = PriceAsk_Phone;
            spriceaskinfo.PriceAsk_Quantity = PriceAsk_Quantity;
            spriceaskinfo.PriceAsk_Price = PriceAsk_Price;
            spriceaskinfo.PriceAsk_DeliveryDate = tools.NullDate(PriceAsk_DeliveryDate);
            spriceaskinfo.PriceAsk_Content = PriceAsk_Content;

            spriceaskinfo.PriceAsk_IsReply = 0;
            spriceaskinfo.PriceAsk_MemberID = supplier_id;
            spriceaskinfo.PriceAsk_ProductID = product_id;
            spriceaskinfo.PriceAsk_ReplyContent = "";
            spriceaskinfo.PriceAsk_AddTime = now;
            spriceaskinfo.PriceAsk_ReplyTime = now;


            if (MyPriceAsk.AddSupplierPriceAsk(spriceaskinfo, pub.CreateUserPrivilege("db32b459-6e76-4ce9-816d-0ca7b1dea952")))
            {
                pub.Msg("info", "提示信息", "询价信息提交成功！", false, "/product/detail.aspx?product_id=" + product_id);
            }
            else
            {
                pub.Msg("error", "提示信息", "操作失败！", false, "{back}");
            }
        }
        else
        {
            Response.Redirect("/product/category.aspx");
        }

    }



    //产品审核
    public void Product_Insale_Edit(int status)
    {

        string product_id = tools.CheckStr(Request.QueryString["product_id"]);
        Response.Write(product_id);
        if (product_id == "")
        {
            pub.Msg("error", "错误信息", "请选择要操作的产品", false, "{back}");
            return;
        }

        if (tools.Left(product_id, 1) == ",") { product_id = product_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        //IList<ProductInfo> entitys = MyBLL.GetProducts(Query, pub.GetUserPrivilege());
        IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                entity.Product_IsInsale = status;

                Myproduct.EditProductInfo(entity, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d"));
                //MyBLL.EditProductInfo(entity, pub.GetUserPrivilege());
            }
        }

        Response.Redirect("/product/product.aspx");

    }

    public bool CheckProductCode(string code, int id)
    {
        if (code == null || code.Length == 0) { return false; }


        ProductInfo Entity = Myproduct.GetProductByCode(code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Entity != null)
        {
            if (id == 0) { return false; }

            if (Entity.Product_ID == id) { return true; }
            else { return false; }
        }
        else
        {
            return true;
        }
    }



    //产品单位选择
    public string Product_Unit_Select(int Product_ID, string Select_Unit)
    {
        string select_str = "";
        select_str += "<select onchange=\"getSelectUnit(this.id)\" name=\"" + Select_Unit + "\">";
        select_str += "<option value=\"0\">不限</option>";

        string[] Product_Units = { "kg", "吨", "块" };

        if (Product_ID == 0)
        {
            foreach (string Product_Unit in Product_Units)
            {
                if (Product_Unit == "kg")
                {
                    select_str += "<option value=\"" + Product_Unit + "\" >" + Product_Unit + "</option>";
                }
                else if ((Product_Unit == "吨"))
                {
                    select_str += "<option value=\"" + Product_Unit + "\" >" + Product_Unit + "</option>";
                }
                else if ((Product_Unit == "块"))
                {
                    select_str += "<option value=\"" + Product_Unit + "\" >" + Product_Unit + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + Product_Unit + "\" >" + "不限" + "</option>";
                }
            }
        }
        else
        {
            ProductInfo entity = GetProductByID(Product_ID);
            if (entity != null)
            {
                foreach (string Product_Unit in Product_Units)
                {
                    if (entity.Product_Unit == Product_Unit)
                    {
                        select_str += "<option id=\"selected_Unit\" value=\"" + Product_Unit + "\" selected  >" + Product_Unit + "</option>";
                    }
                    else
                    {
                        select_str += "<option value=\"" + Product_Unit + "\"   >" + Product_Unit + "</option>";
                    }
                }
            }
        }

        select_str += "</select>";
        return select_str;
    }






    #region 会员中心评论信息
    //会员中心我的店铺评论  isshopevalute:0表示店铺评价1表示商品评价 
    public void shopvaluate(int isproductevalute)
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (isproductevalute == 0)
        {
            //表示商品评价
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"262px;\" class=\"name\">商品名称</td>";

            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">供应商</td>";
            tmp_head = tmp_head + "<td width=\"112px;\" class=\"name\">订单编号</td>";
            tmp_head = tmp_head + "<td width=\"112px;\" class=\"name\">评论类型</td>";

            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">商品评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无评论 </td></tr>";
        }
        else
        {
            //表示店铺评价
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">供应商名称</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">订单编号</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">评论类型</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">服务评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">发货评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无评论</td></tr>";
        }


        page_url = "?keyword=" + keyword;
        //int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_MemberId", "=", tools.NullInt(Session["member_id"].ToString()).ToString()));
        if (isproductevalute == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));

        }
        else
        {
            //店铺评论
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", "0"));


        }


        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "DESC"));

        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        PageInfo page = MyEvaluate.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";

            SQLHelper DBhelper = new SQLHelper();
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {

                string supplier_name = "";
                string Shop_Domain = "http://";





                SupplierShopInfo shopInfo = null;
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_SupplierID);
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;


                }
                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Orders_SN = "";
                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                Response.Write("<tr>");
                string product_id = entity.Shop_Evaluate_Productid.ToString();
                string Product_Name = "";

                ProductInfo proentity = GetProductByID(entity.Shop_Evaluate_Productid);
                if (proentity != null)
                {
                    Product_Name = proentity.Product_Name;
                }
                if (entity.Shop_Evaluate_Productid > 0)
                {
                    //Myproduct.getproduct
                    Response.Write("<td><span style=\"float:left;margin-left:18px;\"><a href=\"/product/detail.aspx?product_id=" + entity.Shop_Evaluate_Productid + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(proentity.Product_Img, "thumbnail") + "\" style=\"height:60px;width:60px;\"></a></span><span style=\"    float: left;    margin-left: 10px;    margin-top: 10px;  \"><a href=\"/product/detail.aspx?product_id=" + entity.Shop_Evaluate_Productid + "\" target=\"_blank\" style=\"display:block;width:140px;\">" + Product_Name + "</a></span></td>");
                    //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product_id) + "\" target=\"_blank\">" + Product_Name + "</a></span></td>");
                    Response.Write("<td><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">" + supplier_name + "</a></span></td>");

                }
                else
                {
                    Response.Write("<td><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">" + supplier_name + "</a></span></td>");
                }
                Response.Write("<td><span><a href=\"/member/order_view.aspx?orders_sn=" + Orders_SN + "\" target=\"_blank\">" + Orders_SN + "</a></span></td>");


                //string product_name = tools.CheckStr(DBhelper.ExecuteScalar("select Product_Name from Product_Basic  where Product_ID=" + product_id + "").ToString());


                if (entity.Shop_Evaluate_Productid > 0)
                {
                    Response.Write("<td>" + "商品评价" + "</td>");


                    Response.Write("<td>" + entity.Shop_Evaluate_Product + "</td>");


                }
                else
                {
                    Response.Write("<td>" + "商家评价" + "</td>");
                    Response.Write("<td>" + entity.Shop_Evaluate_Service + "</td>");
                    Response.Write("<td>" + entity.Shop_Evaluate_Delivery + "</td>");
                }

                if (isproductevalute == 0)
                {
                    //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product_id) + "\" target=\"_blank\">" + "查看" + "</a></span></td>");
                    Response.Write("<td><span><a href=\"/valuate/member_productvaluate_view.aspx?evaluate_id=" + entity.Shop_Evaluate_ID + "&isshopevalute=0&EvaluateType=1\" target=\"_blank\">" + "查看" + "</a></span></td>");
                }
                else
                {


                    //Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];

                    Response.Write("<td><span><a href=\"/valuate/member_shopvaluate_view.aspx?evaluate_id=" + entity.Shop_Evaluate_ID + "&isshopevalute=1&EvaluateType=0\" target=\"_blank\">" + "查看" + "</a></span><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">进入店铺</a></span></td>");
                    //Response.Write("<td></td>");
                }


                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }

    }


    //商家中心我的店铺评论  isshopevalute:0表示店铺评价1表示商品评价 
    public void shopsuppliervaluate(int isproductevalute)
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (isproductevalute == 0)
        {
            //表示商品评价
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"262px;\" class=\"name\">商品名称</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">采购商</td>";
            tmp_head = tmp_head + "<td width=\"112px;\" class=\"name\">订单编号</td>";

            tmp_head = tmp_head + "<td width=\"112px;\" class=\"name\">评论类型</td>";

            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">商品评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无评论 </td></tr>";
        }
        else
        {
            //表示店铺评价
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">采购商名称</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">订单编号</td>";

            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">评论类型</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">服务评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">发货评分</td>";
            tmp_head = tmp_head + "<td width=\"162px;\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无评论</td></tr>";
        }


        page_url = "?keyword=" + keyword;
        //int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["supplier_id"].ToString()).ToString()));
        if (isproductevalute == 0)
        {
            //商品评论Shop_Evaluate_ProductID 
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));

        }
        else
        {
            //店铺评论
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", "0"));


        }


        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "DESC"));

        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        PageInfo page = MyEvaluate.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            //DateTime Bid_BidEndTime = DateTime.Now;
            //string Bid_Status = "";
            SQLHelper DBhelper = new SQLHelper();
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {

                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Orders_SN = "";


                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                Response.Write("<tr>");
                string product_id = entity.Shop_Evaluate_Productid.ToString();

                string Product_Name = "";
                string supplier_name = "";
                string Shop_Domain = "http://";

                SupplierShopInfo shopInfo = null;
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_MemberID);
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;

                    //Response.Write("<td><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">" + supplier_name + "</a></span></td>");
                }


                ProductInfo proentity = GetProductByID(entity.Shop_Evaluate_Productid);
                if (proentity != null)
                {
                    Product_Name = proentity.Product_Name;
                }
                if (entity.Shop_Evaluate_Productid > 0)
                {
                    //Myproduct.getproduct
                    Response.Write("<td><span style=\"float:left;margin-left:18px;\"><a href=\"/product/detail.aspx?product_id=" + entity.Shop_Evaluate_Productid + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(proentity.Product_Img, "thumbnail") + "\" style=\"height:60px;width:60px;\"></a></span><span style=\"    float: left;    margin-left: 10px;    margin-top: 10px;  \"><a href=\"/product/detail.aspx?product_id=" + entity.Shop_Evaluate_Productid + "\" target=\"_blank\" style=\"display:block;width:140px;\">" + Product_Name + "</a></span></td>");
                    //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product_id) + "\" target=\"_blank\">" + Product_Name + "</a></span></td>");
                    Response.Write("<td><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">" + supplier_name + "</a></span></td>");

                }
                else
                {
                    Response.Write("<td><span><a href=\"" + Shop_Domain + "\" target=\"_blank\" class=\"a32\">" + supplier_name + "</a></span></td>");
                }
                Response.Write("<td><span><a href=\"/supplier/order_detail.aspx?orders_sn=" + Orders_SN + "\" target=\"_blank\">" + Orders_SN + "</a></span></td>");
                if (entity.Shop_Evaluate_Productid > 0)
                {
                    Response.Write("<td>" + "商品评价" + "</td>");
                    //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product_id) + "\" target=\"_blank\">" + Product_Name + "</a></span></td>");

                    Response.Write("<td>" + entity.Shop_Evaluate_Product + "</td>");


                }
                else
                {
                    Response.Write("<td>" + "商家评价" + "</td>");
                    Response.Write("<td>" + entity.Shop_Evaluate_Service + "</td>");
                    Response.Write("<td>" + entity.Shop_Evaluate_Delivery + "</td>");
                }










                if (isproductevalute == 0)
                {
                    //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product_id) + "\" target=\"_blank\">" + "查看" + "</a></span></td>");
                    Response.Write("<td><span><a href=\"/valuate/supplier_productvaluate_view.aspx?evaluate_id=" + entity.Shop_Evaluate_ID + "&isshopevalute=0&EvaluateType=1\" target=\"_blank\">" + "查看" + "</a></span></td>");
                }
                else
                {


                    //Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];

                    Response.Write("<td><span><a href=\"/valuate/supplier_shopvaluate_view.aspx?evaluate_id=" + entity.Shop_Evaluate_ID + "&isshopevalute=1&EvaluateType=0\" target=\"_blank\">" + "查看" + "</a></span></td>");
                    //Response.Write("<td></td>");
                }


                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }

    }

    //EvaluateType  0:供应商评价  1:商品评价
    public string GetEvaluateDetail(int EvaluateId, int EvaluateType)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierShopEvaluateInfo entity = MyEvaluate.GetSupplierShopEvaluateByID(EvaluateId);
        if (entity != null)
        {

            if (entity.Shop_Evaluate_Productid > 0)
            {
                string Orders_SN = "--";
                //商品评价详情页面
                strHTML.Append("<div class=\"b06_main_sz\" style=\"border:1px solid #dddddd;padding-top:15px;margin-top:12px;\">");
                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Shop_Domain = "http://";
                SupplierShopInfo shopInfo = null;
                string supplier_name = "";
                string Product_Name = "";
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_SupplierID);

                ProductInfo productinfo = GetProductByID(entity.Shop_Evaluate_Productid);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;
                }
                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                string targetURL = "";
                if (productinfo != null)
                {
                    targetURL = pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString());
                    strHTML.Append("<p style=\"padding-left:80px;padding-top:20px;\"> <a href=\"" + targetURL + "\" title=\"" + productinfo.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(productinfo.Product_Img, "thumbnail") + "\" style=\"height:140px;width:140px;\" target=\"_blank\"></a></p>");

                }
                strHTML.Append("<p><span>商品名称：</span>" + Product_Name + "</p>");
                strHTML.Append("<p><span>订单编号：</span>" + Orders_SN + "</p>");
                strHTML.Append("<p><span>供应商：</span>" + supplier_name + "</p>");

                strHTML.Append("<p><span>评价类型：</span>" + "商品评价" + "</p>");

                strHTML.Append("<p><span>商品评分：</span>" + entity.Shop_Evaluate_Product + "</p>");
                strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");
                //strHTML.Append(" <span><a href=\"javascript:void(0);\" onclick=\"history.go(-1);\">返回</a></span>");
                strHTML.Append("</div>");
            }
            else
            {//商家评价详情页面
                string Orders_SN = "--";
                //商品评价详情页面
                strHTML.Append("<div class=\"b06_main_sz\" style=\"border:1px solid #dddddd;padding-top:15px;margin-top:12px;\">");
                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Shop_Domain = "http://";
                SupplierShopInfo shopInfo = null;
                string supplier_name = "";
                string Product_Name = "";
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_SupplierID);

                ProductInfo productinfo = GetProductByID(entity.Shop_Evaluate_Productid);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;


                }
                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                strHTML.Append("<p><span>订单编号：</span>" + Orders_SN + "</p>");
                strHTML.Append("<p><span>供应商：</span>" + supplier_name + "</p>");

                strHTML.Append("<p><span>评价类型：</span>" + "供应商评价" + "</p>");

                strHTML.Append("<p><span>服务评分：</span>" + entity.Shop_Evaluate_Service + "</p>");
                strHTML.Append("<p><span>发货评分：</span>" + entity.Shop_Evaluate_Delivery + "</p>");
                strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");

                strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");

                strHTML.Append("</div>");
            }


        }


        return strHTML.ToString();
    }



    //EvaluateType  0:供应商评价  1:商品评价
    public string GetSupplierEvaluateDetail(int EvaluateId, int EvaluateType)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierShopEvaluateInfo entity = MyEvaluate.GetSupplierShopEvaluateByID(EvaluateId);
        if (entity != null)
        {

            if (entity.Shop_Evaluate_Productid > 0)
            {
                string Orders_SN = "--";
                //商品评价详情页面
                strHTML.Append("<div class=\"b06_main_sz\" style=\"border:1px solid #dddddd;padding-top:15px;margin-top:12px;\">");
                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Shop_Domain = "http://";
                SupplierShopInfo shopInfo = null;
                string supplier_name = "";
                string Product_Name = "";
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_SupplierID);

                ProductInfo productinfo = GetProductByID(entity.Shop_Evaluate_Productid);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;
                }
                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                strHTML.Append("<p><span>订单编号：</span>" + Orders_SN + "</p>");
                strHTML.Append("<p><span>供应商：</span>" + supplier_name + "</p>");
                //if (EvaluateType == 1)
                //{  //EvaluateType  0:供应商评价  1:商品评价
                strHTML.Append("<p><span>评价类型：</span>" + "商品评价" + "</p>");
                strHTML.Append("<p><span>商品名称：</span>" + Product_Name + "</p>");
                strHTML.Append("<p><span>商品评分：</span>" + entity.Shop_Evaluate_Product + "</p>");
                strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");
                //}
                //else
                //{  //EvaluateType  0:供应商评价  1:商品评价
                //    strHTML.Append("<p><span>评价类型：</span>" + "供应商评价" + "</p>");

                //    strHTML.Append("<p><span>服务评分：</span>" + entity.Shop_Evaluate_Service + "</p>");
                //    strHTML.Append("<p><span>发货评分：</span>" + entity.Shop_Evaluate_Delivery + "</p>");
                //    strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");
                //}

                //strHTML.Append("<p><span>订单编号：</span>" + Orders_SN + "</p>");






                strHTML.Append("</div>");
            }
            else
            {//商家评价详情页面
                string Orders_SN = "--";
                //商品评价详情页面
                strHTML.Append("<div class=\"b06_main_sz\" style=\"border:1px solid #dddddd;padding-top:15px;margin-top:12px;\">");
                OrdersInfo orderinfo = MyOrders.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                string Shop_Domain = "http://";
                SupplierShopInfo shopInfo = null;
                string supplier_name = "";
                string Product_Name = "";
                SupplierInfo supplierinfo = Mysupplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Shop_Evaluate_SupplierID);

                ProductInfo productinfo = GetProductByID(entity.Shop_Evaluate_Productid);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
                if (shopInfo != null)
                {
                    //shop_img = shopInfo.Shop_Img;
                    Shop_Domain = Shop_Domain + shopInfo.Shop_Domain + Application["Shop_Second_Domain"];
                }
                if (supplierinfo != null)
                {
                    supplier_name = supplierinfo.Supplier_CompanyName;


                }
                if (orderinfo != null)
                {
                    Orders_SN = orderinfo.Orders_SN;
                }

                strHTML.Append("<p><span>订单编号：</span>" + Orders_SN + "</p>");
                strHTML.Append("<p><span>供应商：</span>" + supplier_name + "</p>");
                //if (EvaluateType == 1)
                //{  //EvaluateType  0:供应商评价  1:商品评价
                //    strHTML.Append("<p><span>评价类型：</span>" + "商品评价" + "</p>");
                //    strHTML.Append("<p><span>商品名称：</span>" + Product_Name + "</p>");
                //    strHTML.Append("<p><span>商品评分：</span>" + entity.Shop_Evaluate_Product + "</p>");
                //    strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");
                //}
                //else
                //{  //EvaluateType  0:供应商评价  1:商品评价
                strHTML.Append("<p><span>评价类型：</span>" + "供应商评价" + "</p>");

                strHTML.Append("<p><span>服务评分：</span>" + entity.Shop_Evaluate_Service + "</p>");
                strHTML.Append("<p><span>发货评分：</span>" + entity.Shop_Evaluate_Delivery + "</p>");
                strHTML.Append("<p><span>商品评价：</span>" + entity.Shop_Evaluate_Note + "</p>");
                //}








                strHTML.Append("</div>");
            }


        }


        return strHTML.ToString();
    }

    #endregion

}
