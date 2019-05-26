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

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.CMS;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.SAL;

/// <summary>
///Product 的摘要说明
/// </summary>
public class Shop
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    public int Shop_SupplierID;
    public string Shop_Name;
    public string Shop_Title;
    public string Shop_banner_Title;
    public string Shop_Keyword;
    public string Shop_Intro;
    public string shop_img = "";
    public int Shop_Css;
    public string shop_gapcolor;
    public string Shop_banner;
    public int banner_title_size;
    public string banner_title_family;
    public string banner_title_color;
    public int banner_title_leftpadding;
    public string shop_supplier_name;
    public string Supplier_Address;
    public string Supplier_Mobile;

    public int Shop_Banner_IsActive;
    public int Shop_Top_IsActive;
    public int Shop_TopNav_IsActive;
    public int Shop_Info_IsActive;
    public int Shop_LeftSearch_IsActive;
    public int Shop_LeftCate_IsActive;
    public int Shop_LeftSale_IsActive;
    public int Shop_Left_IsActive;
    public int Shop_Right_IsActive;
    public int Shop_RightProduct_IsActive;

    private ISupplier MyBLL;
    private ISupplierShop MyShop;
    private ISupplierShopApply MyShopApply;
    private ISupplierShopBanner MyShopBanner;
    private ISupplierShopCss MyShopCss;
    private ISupplierShopPages MyShopPages;
    private ISupplierShopEvaluate MyShopEvaluate;
    private ISupplierCategory MyProductCate;
    private ISupplierShopArticle MyShopArticle;
    private ISupplierTag MySupplierTag;
    private IProduct Myproduct;
    private IProductTypeExtend MyExtend;
    private IShoppingAsk Myshopask;
    private IBrand MyBrand;
    private ICategory MyCate;
    private IMember MyMEM;
    private IMemberGrade Mygrade;
    private IOrders MyOrder;
    private ITools tools;
    private IMember MyMem;
    private Public_Class pub;
    private IPromotionFavor MyFavor;
    private IPromotionCouponRule MyCouponRule;
    private IProductReview MyReview;
    private IProductTag MyTag;
    private Addr addr;
    private ISupplierOnline MyOnline;
    private PageURL pageurl1;
    private ISupplierMessage MyMessage;
    private IProductWholeSalePrice MySalePrice;

    public Shop()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBLL = SupplierFactory.CreateSupplier();
        MyShop = SupplierShopFactory.CreateSupplierShop();
        MyShopBanner = SupplierShopBannerFactory.CreateSupplierShopBanner();
        MyShopCss = SupplierShopCssFactory.CreateSupplierShopCss();
        MyShopPages = SupplierShopPagesFactory.CreateSupplierShopPages();
        MyShopEvaluate = SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
        MyProductCate = SupplierCategoryFactory.CreateSupplierCategory();
        MyShopArticle = SupplierShopArticleFactory.CreateSupplierShopArticle();
        Myproduct = ProductFactory.CreateProduct();
        MyCate = CategoryFactory.CreateCategory();
        MyMem = MemberFactory.CreateMember();
        MyShopApply = SupplierShopApplyFactory.CreateSupplierShopApply();
        Myshopask = ShoppingAskFactory.CreateShoppingAsk();
        MyReview = ProductReviewFactory.CreateProductReview();
        MyExtend = ProductTypeExtendFactory.CreateProductTypeExtend();
        MyBrand = BrandFactory.CreateBrand();
        MyMEM = MemberFactory.CreateMember();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
        MyOrder = OrdersFactory.CreateOrders();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyTag = ProductTagFactory.CreateProductTag();
        MyCouponRule = PromotionCouponRuleFactory.CreatePromotionFavorCoupon();
        MySupplierTag = SupplierTagFactory.CreateSupplierTag();
         pageurl1 = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        tools = ToolsFactory.CreateTools();
        pub = new Public_Class();
        addr = new Addr();
        MyMessage = SupplierMessageFactory.CreateSupplierMessage();
        MyOnline = SupplierOnlineFactory.CreateSupplierOnline();

        MySalePrice = ProductWholeSalePriceFactory.CreateProductWholeSalePrice();
    }

    public void Shop_Initial()
    {
        string banner_img = "";
        
        string banner_url = "";
        shop_supplier_name = "";
        Supplier_Address = "";
        Supplier_Mobile = "";
        bool shop_flag = false;
        int banner = 0;
        shop_flag = true;
        string shop_domain = tools.CheckStr(Request.Url.Host);
        shop_domain = shop_domain.ToLower();
        shop_domain = shop_domain.Split('.')[0];
        SupplierShopInfo entity=null;
        entity = (SupplierShopInfo)Session["shop_info"];
        if (entity == null)
        {
            //entity = MyShop.GetSupplierShopByDomain(shop_domain);
            entity = GetShopinfoByDomain(shop_domain);
        }
        else
        {
            if (entity.Shop_Domain != shop_domain)
            {
                //entity = MyShop.GetSupplierShopByDomain(shop_domain);
                entity = GetShopinfoByDomain(shop_domain);
            }
        }
        if (entity != null)
        {
            if (entity.Shop_Status == 0)
            {
                Response.Redirect(tools.NullStr(Application["Site_URL"]));
            }
            //Session["shop_info"] = entity;
            Session["shop_id"] = entity.Shop_ID;
            Session["Shop_Type"] = entity.Shop_Type;
            Shop_SupplierID = entity.Shop_SupplierID;
            Shop_Name = entity.Shop_Name;
            Shop_Title = entity.Shop_SEO_Title;
            shop_img = entity.Shop_Img;
            Shop_Keyword = entity.Shop_SEO_Keyword;
            Shop_Intro = entity.Shop_SEO_Description;
            banner = entity.Shop_Banner;
            Shop_banner_Title = entity.Shop_Banner_Title;
            banner_title_size = entity.Shop_Banner_Title_Size;
            banner_title_family = entity.Shop_Banner_Title_Family;
            banner_title_color = entity.Shop_banner_Title_color;
            banner_title_leftpadding = entity.Shop_Banner_Title_LeftPadding;
            banner_img = entity.Shop_Banner_Img;
            Shop_Css = entity.Shop_Css;
            Shop_Banner_IsActive = entity.Shop_Banner_IsActive;
            Shop_Top_IsActive = entity.Shop_Top_IsActive;
            Shop_TopNav_IsActive = entity.Shop_TopNav_IsActive;
            Shop_Info_IsActive = entity.Shop_Info_IsActive;
            Shop_LeftSearch_IsActive = entity.Shop_LeftSearch_IsActive;
            Shop_LeftCate_IsActive = entity.Shop_LeftCate_IsActive;
            Shop_LeftSale_IsActive = entity.Shop_LeftSale_IsActive;
            Shop_Left_IsActive = entity.Shop_Left_IsActive;
            Shop_Right_IsActive = entity.Shop_Right_IsActive;
            Shop_RightProduct_IsActive = entity.Shop_RightProduct_IsActive;
            
            Session["shop_supplier_id"] = entity.Shop_SupplierID;

        }
        else
        {
            Response.Redirect(tools.NullStr(Application["Site_URL"]));
        }

        if (Shop_Title.Length>0)
        {
            Shop_Title = " - " + Shop_Title;
        }
        string banner_width = "";
        if (entity.Shop_Type == 2)
        {
            banner_width = "";
        }
        if (banner == 0)
        {
            if (banner_img.IndexOf("swf") == -1)
            {
                banner_url = banner_img;
                if (banner_url.Length>0)
                {
                    
                }
                else
                {
                    banner_url = "/shop/banner/default/banner1.png";
                }
                
               
                Shop_banner += "<div class=\"ad03\"  style=\"background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:round; background-position:top center; height:117px; width:1200px; margin:0 auto; \">";
               
                Shop_banner += "	<div class=\"ad03_main\" style=\" background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:round; background-position:top center; height:117px; width:1200px; margin:0 auto; \"></div>";
                Shop_banner += "</div>";

                //Shop_banner = "<img src=\"" + Shop_banner + "\" " + banner_width + ">";
            }
            else
            {
                Shop_banner = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"960\">";
                Shop_banner += "<param name=\"movie\" value=\"" + pub.FormatImgURL(banner_img,"fullpath") + "\">";
                Shop_banner += "<param name=\"quality\" value=\"high\">";
                Shop_banner += "<param name=\"quality\" value=\"transparent\">";
                Shop_banner += "<param name=\"wmode\" value=\"transparent\">";
                Shop_banner += "<param name=\"SCALE\" value=\"exactfit\">";
                Shop_banner += "<embed src=\"" + pub.FormatImgURL(banner_img, "fullpath") + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"960\" scale=\"exactfit\" wmode=\"transparent\" > </embed>";
                Shop_banner += "</object>";
            }
        }
        else if (banner >= 1)
        {
            SupplierShopBannerInfo bannerinfo = MyShopBanner.GetSupplierShopBannerByID(banner, pub.CreateUserPrivilege("daff677a-1be4-4438-b1e8-32b453275341"));
            if (bannerinfo != null)
            {
                banner_url = bannerinfo.Shop_Banner_Url;
                if (bannerinfo.Shop_Banner_Type == 0)
                {
                    Shop_banner += "<div class=\"ad03\" style=\"background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:no-repeat; background-position:top center; height:117px; width:100%; margin:0;\">";
                    Shop_banner += "	<div class=\"ad03_main\" style=\" background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:no-repeat; background-position:top center; height:117px; width:1200px; margin:0 auto;\"></div>";
                    Shop_banner += "</div>";

                    //Shop_banner = "<img src=\"" + Shop_banner + "\" " + banner_width + ">";
                }
                else
                {
                    banner_url = pub.FormatImgURL(banner_url, "fullpath");
                    Shop_banner = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"960\">";
                    Shop_banner += "<param name=\"movie\" value=\"" + banner_url + "\">";
                    Shop_banner += "<param name=\"quality\" value=\"high\">";
                    Shop_banner += "<param name=\"quality\" value=\"transparent\">";
                    Shop_banner += "<param name=\"wmode\" value=\"transparent\">";
                    Shop_banner += "<param name=\"SCALE\" value=\"exactfit\">";
                    Shop_banner += "<embed src=\"" + banner_url + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"960\" scale=\"exactfit\" wmode=\"transparent\" > </embed>";
                    Shop_banner += "</object>";
                }
            }
            else
            {
                Shop_banner += "<div class=\"ad03\" style=\"background-image:url(/images/ad_pic05.jpg); background-repeat:no-repeat; background-position:top center; height:117px; width:100%; margin:0;\">";
                Shop_banner += "	<div class=\"ad03_main\" style=\" background-image:url(/images/ad_pic05.jpg); background-repeat:no-repeat; background-position:top center; height:117px; width:1200px; margin:0 auto;\"></div>";
                Shop_banner += "</div>";

                //Shop_banner = "<img src=\"/images/banner/1.gif\" " + banner_width + ">";
            }
        }
        else
        {
            Shop_banner += "<div class=\"ad03\" style=\"background-image:url(/images/ad_pic05.jpg); background-repeat:no-repeat; background-position:top center; height:117px; width:100%; margin:0;\">";
            Shop_banner += "	<div class=\"ad03_main\" style=\" background-image:url(/images/images/ad_pic05.jpg); background-repeat:no-repeat; background-position:top center; height:117px; width:1200px; margin:0 auto;\"></div>";
            Shop_banner += "</div>";

            //Shop_banner = "<img src=\"/images/banner/1.gif\" " + banner_width + ">";
        }

        SupplierShopCssInfo cssinfo = MyShopCss.GetSupplierShopCssByID(Shop_Css, pub.CreateUserPrivilege("3396b3c6-8116-4c3b-9682-6d29c937947e"));
        if (cssinfo != null)
        {
            shop_gapcolor = cssinfo.Shop_Css_GapColor;
        }
        else
        {
            Shop_Css = 1;
            shop_gapcolor = "#DCDCDC";
        }

        SupplierInfo supplierinfo = MyBLL.GetSupplierByID(tools.NullInt(Session["shop_supplier_id"]), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierinfo != null)
        {
            shop_supplier_name = supplierinfo.Supplier_CompanyName;
            Supplier_Address = supplierinfo.Supplier_Address;
            Supplier_Mobile = supplierinfo.Supplier_Mobile;
        }

    }

    public SupplierShopInfo GetShopinfoByDomain(string shop_domain)
    {
        SupplierShopInfo entity = null;
        entity = MyShop.GetSupplierShopByDomain(shop_domain);
        if (entity == null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_ID", "in", "select Shop_Domain_ShopID from Supplier_Shop_Domain where Shop_Domain_Status=1 and Shop_Domain_Name='"+shop_domain+"'"));
            Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", ""));
            IList<SupplierShopInfo> entitys = MyShop.GetSupplierShops(Query);
            if (entitys != null)
            {
                entity = entitys[0];
            }
        }
        return entity;
    }

    #region 辅助函数

    //获取商家类型
    public int GetSupplierGrade(int supplier_ID)
    {
        SupplierInfo entity = MyBLL.GetSupplierByID(supplier_ID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entity != null)
        {
            return entity.Supplier_Type;
        }
        else
        {
            return 3;
        }
    }

    //获取会员昵称
    public string Get_Member_Name(int member_id)
    {
        string member_nickname="";
        MemberInfo entity = MyMEM.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (entity != null)
        {
            member_nickname = entity.Member_NickName;
        }
        return member_nickname;
    }

    //获取指定订单产品金额
    public double Evaluate_Product_Price(int Orders_ID, int Product_ID)
    {
        double Product_Price=0;
        IList<OrdersGoodsInfo> entitys = MyOrder.GetGoodsListByOrderID(Orders_ID);
        if (entitys != null)
        {
            foreach (OrdersGoodsInfo entity in entitys)
            {
                if ((entity.Orders_Goods_Type != 2 || (entity.Orders_Goods_Type == 0 && entity.Orders_Goods_ParentID > 0))&&entity.Orders_Goods_Product_ID==Product_ID)
                {
                    Product_Price = entity.Orders_Goods_Product_Price;
                }
            }
        }
        return Product_Price;
    }
    #endregion

    /// <summary>
    /// 入住年数
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public int SettledYear(int Supplier_ID)
    {
        SupplierShopInfo ShopEntity = MyShop.GetSupplierShopBySupplierID(Supplier_ID);
         int Year = 1;
        int i = 0;
        try
        {
            i =ShopEntity.Shop_Addtime.Year - DateTime.Now.Year;
            if (i > 0)
            {
                Year = i;
            }
            if (Year == 0)
            {
                Year = 1;
            }
        }
        catch
        {

        }
        return Year;
        //return SettledYear(ShopEntity);
    }

    //public int SettledYear(SupplierShopInfo ShopEntity)
    //{
    //    //int Years = 0;
    //    //if (ShopEntity != null)
    //    //{
    //    //    Years = DateTime.Today.Year - ShopEntity.Shop_Addtime.Year;
    //    //    if (DateTime.Today.CompareTo(ShopEntity.Shop_Addtime.AddYears(Years)) < 0)
    //    //    {
    //    //        Years--;
    //    //    }
    //    //}
    //    //ShopEntity = null;

    //    //return Years;

    //    int Year = 1;
    //    int i = 0;
    //    try
    //    {
    //        i =ShopEntity.Shop_Addtime.Year - DateTime.Now.Year;
    //        if (i > 0)
    //        {
    //            Year = i;
    //        }
    //        if (Year == 0)
    //        {
    //            Year = 1;
    //        }
    //    }
    //    catch
    //    {

    //    }
    //    return Year;
    //}

    public void SupplierMessageAdd(int Shop_Apply_SupplierID, string Supplier_Message_Title, string Supplier_Message_Content)
    {
        SupplierMessageInfo message = new SupplierMessageInfo();
        message.Supplier_Message_ID = 0;
        message.Supplier_Message_SupplierID = Shop_Apply_SupplierID;
        message.Supplier_Message_Title = Supplier_Message_Title;
        message.Supplier_Message_Content = Supplier_Message_Content;
        message.Supplier_Message_Addtime = DateTime.Now;
        message.Supplier_Message_IsRead = 0;
        message.Supplier_Message_Site = pub.GetCurrentSite();
        MyMessage.AddSupplierMessage(message, pub.CreateUserPrivilege("11fe78b3-c971-4ed1-bb5e-3a31b60b19cd"));
    }

    //更新店铺点击数
    public void UpdateShopHits()
    {
        SupplierShopInfo entity = MyShop.GetSupplierShopByID(tools.NullInt(Session["shop_id"]));
        if (entity != null)
        {
            entity.Shop_Hits = entity.Shop_Hits + 1;
            MyShop.EditSupplierShop(entity);
        }
    }

    //获取商家所在省市区
    public string Get_Supplier_Sate()
    {
        string addrstr = "";
        SupplierInfo entity = MyBLL.GetSupplierByID(tools.NullInt(Session["shop_supplier_id"]), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entity != null)
        {
            addrstr = addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County);
        }
        return addrstr;
    }

    //获取店铺版面导航
    public string Get_Supplier_Pages_Nav()
    {
        string htmlstr = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopPagesInfo.Shop_Pages_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEX"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXLEFT"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXTOP"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopPagesInfo.Shop_Pages_Sort", "Asc"));
        IList<SupplierShopPagesInfo> entitys = MyShopPages.GetSupplierShopPagess(Query);
        if (entitys != null)
        {
            foreach (SupplierShopPagesInfo entity in entitys)
            {
                htmlstr += "<li" + (tools.NullStr(Session["shop_page"]).Equals(entity.Shop_Pages_Sign) ? " class=\"on\"" : "") + " ><a href=\"/shop_pages.aspx?sign=" + entity.Shop_Pages_Sign + "\">" + tools.Left(entity.Shop_Pages_Title, 4) + "</a></li>";
            }
        }
        return htmlstr;
    }

    //根据版面标识与供应商获取版面信息
    public SupplierShopPagesInfo GetSupplierShopPagesByIDSign(string Sign, int Supplier_ID)
    {
        SupplierShopPagesInfo entity=MyShopPages.GetSupplierShopPagesByIDSign(Sign, Supplier_ID);
        if (entity != null)
        {
            if (entity.Shop_Pages_Ischeck != 1)
            {
                entity = null;
            }
        }
        return entity;
    }

    //标签产品
    public void Product_Tag_List()
    {
        string tmp_list = "";
        string Tag_ProductID = "0";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductTagInfo.Product_Tag_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_Sort", "Asc"));
        IList<ProductTagInfo> entitys = MyTag.GetProductTags(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (entitys != null)
        {
            foreach (ProductTagInfo Taginfo in entitys)
            {
                Tag_ProductID = Myproduct.GetTagProductID(Taginfo.Product_Tag_ID.ToString());

                QueryInfo Query1 = new QueryInfo();
                Query1.PageSize = 3;
                Query1.CurrentPage = 1;
                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", Tag_ProductID));
                Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
                IList<ProductInfo> productinfos = Myproduct.GetProducts(Query1, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (productinfos != null)
                {
                    tmp_list += "<div class=\"dp_blk05\">";
                    tmp_list += "<h2>" + Taginfo.Product_Tag_Name + "</h2>";
                    tmp_list += "<div class=\"dp_main\">";
                    tmp_list += "<ul class=\"dp_lst2\">";
                    foreach (ProductInfo productinfo in productinfos)
                    {
                        tmp_list += "<li>";
                        tmp_list += "<a href=\"/detail.aspx?product_id="+productinfo.Product_ID+"\" target=\"_blank\">";
                        tmp_list += "<img src=\"" + pub.FormatImgURL(productinfo.Product_Img, "thumbnail") + "\"/>";
                        tmp_list += "</a>";
                        tmp_list += "<p class=\"p1\">";
                        tmp_list += "<a href=\"/detail.aspx?product_id=" + productinfo.Product_ID + "\" target=\"_blank\">";
                        tmp_list += tools.CutStr(productinfo.Product_Name, 50);
                        tmp_list += "</a></p>";
                        tmp_list += "<p class=\"p2\"><strong>￥" + productinfo.Product_Price + "</strong></p>";
                        tmp_list += "<p class=\"p3\"><a href=\"/detail.aspx?product_id=" + productinfo.Product_ID + "\" target=\"_blank\" class=\"a9\">加入购物车</a>";
                        tmp_list += "<a href=\"" + Application["Site_URL"] + "/member/fav_do.aspx?action=goods&id=" + productinfo.Product_ID + "\" target=\"_blank\" class=\"a10\">收藏</a></p></li>";
                    }
                    tmp_list += "</ul>";
                    tmp_list += "<div class=\"clear\"></div>";
                    tmp_list += "</div>";
                    tmp_list += "</div>";
                }
            }
        }
        Response.Write(tmp_list);
    }

    //首页楼层产品列表
    public void Home_Floor_List(int show_num)
    {
        int irowmax = 4;
        string orderby, keyword, relate_note, promotionstr, page_type;
        int icount;
        string ProductURL, Product_Name;
        int Product_ID;
        int irow = 0;
        string cate_str;
        String tmp_list = "";
        string product_arry = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> cates = MyProductCate.GetSupplierCategorys(Query);
        if (cates != null)
        {
            foreach (SupplierCategoryInfo cate in cates)
            {
                cate_str = Get_All_SubSupplierCate(cate.Supplier_Cate_ID);
                product_arry = "";
                product_arry = MyProductCate.GetSupplierProductCategorysByCateArry(cate_str);
                if (product_arry.Length == 0)
                {
                    product_arry = "0";
                }

                tmp_list += " <div class=\"shop_cateTit\">";
                tmp_list += " " + cate.Supplier_Cate_Name;
                tmp_list += " <span><a href=\"/category.aspx?cate_id="+cate.Supplier_Cate_ID+"\">更多..</a></span></div>";
                tmp_list += " <div class=\"blank10\"></div>";

                Query = new QueryInfo();
                Query.PageSize = show_num;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", product_arry));

                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                icount = 1;
                IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (entitys != null)
                {
                    //橱窗

                    tmp_list += "<div class=\"shop_goodsItem\">";
                    tmp_list += "<ul>";





                    irow = 0;
                    foreach (ProductInfo entity in entitys)
                    {
                        irow = irow + 1;
                        Product_ID = entity.Product_ID;
                        Product_Name = entity.Product_Name;
                        ProductURL = "/detail.aspx?product_id=" + entity.Product_ID + "";

                        tmp_list += "   <li>";
                        tmp_list += "       <div class=\"goodsbox\">";
                        tmp_list += "       <a href=\"" + ProductURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"  width=\"165\" height=\"165\" border=\"0\" alt=\"" + Product_Name + "\"  onload=\"javascript:AutosizeImage(this,165,165);\" /></a>";
                        tmp_list += "       <p class=\"product_name\"><a href=\"" + ProductURL + "\" target=\"_blank\">" + tools.CutStr(Product_Name, 25) + "</a></p>";
                        tmp_list += "       <p class=\"product_price\">" + pub.FormatCurrency(entity.Product_Price) + "</p>";
                        tmp_list += "       <p class=\"detail_mktprice\">" + pub.FormatCurrency(entity.Product_MKTPrice) + "</p>";
                        tmp_list += "       </div>";
                        tmp_list += "   </li>";




                        icount = icount + 1;
                        if (irow % irowmax == 0)
                        {
                            tmp_list += "</ul> <ul> ";
                        }

                    }

                    for (irow = irow; irow <= irowmax; irow++)
                    {
                        Response.Write("<li></li>");
                    }

                    tmp_list += "</ul>";
                    tmp_list += "</div>";
                    tmp_list += "<div class=\"blank10\"></div>";
                    
                }
            }
        }
        Response.Write(tmp_list);


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

    #region 店铺评价

    //获取评论总体信息
    public void shop_nav_evaluate_info()
    {
        int Shop_product_Count, Shop_product_Sum, Shop_service_Count, Shop_service_Sum, Shop_delivery_Count, Shop_delivery_Sum;
        double Shop_product_Avg, Shop_service_Avg, Shop_delivery_Avg;
        Shop_product_Count = 0;
        Shop_product_Sum = 0;
        Shop_service_Count = 0;
        Shop_service_Sum = 0;
        Shop_delivery_Count = 0;
        Shop_delivery_Sum = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
                Shop_product_Sum = Shop_product_Sum + entity.Shop_Evaluate_Product;
                Shop_service_Count = Shop_service_Count + 1;
                Shop_service_Sum = Shop_service_Sum + entity.Shop_Evaluate_Service;
                Shop_delivery_Count = Shop_delivery_Count + 1;
                Shop_delivery_Sum = Shop_delivery_Sum + entity.Shop_Evaluate_Delivery;
            }
        }
        if (Shop_product_Count > 0 && Shop_product_Sum > 0)
        {
            Shop_product_Avg = tools.CheckFloat((Shop_product_Sum / Shop_product_Count).ToString());
        }
        else
        {
            Shop_product_Avg = 0;
        }
        if (Shop_service_Count > 0 && Shop_service_Sum > 0)
        {
            Shop_service_Avg = tools.CheckFloat((Shop_service_Sum / Shop_service_Count).ToString());
        }
        else
        {
            Shop_service_Avg = 0;
        }
        if (Shop_delivery_Count > 0 && Shop_delivery_Sum > 0)
        {
            Shop_delivery_Avg = tools.CheckFloat((Shop_delivery_Sum / Shop_delivery_Count).ToString());
        }
        else
        {
            Shop_delivery_Avg = 0;
        }


        Response.Write("<li>卖家商品满意度:");
        evaluate_showstar_nograde("small", Shop_product_Count, Shop_product_Avg);
        Response.Write("</li>");
        Response.Write("<li>卖家服务满意度:");
        evaluate_showstar_nograde("small", Shop_service_Count, Shop_service_Avg);
        Response.Write(" </li>");
        Response.Write("<li>发货速度满意度:");
        evaluate_showstar_nograde("small", Shop_delivery_Count, Shop_delivery_Avg);
        Response.Write(" </li>");
    }

    //获取评论总体信息
    public void shop_evaluate_info()
    {
        int Shop_product_Count = 0;
        int Shop_product_Sum = 0;
        int Shop_service_Count = 0;
        int Shop_service_Sum = 0;
        int Shop_delivery_Count = 0;
        int Shop_delivery_Sum = 0;
        double Shop_product_Avg = 0;
        double Shop_service_Avg = 0;
        double Shop_delivery_Avg = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
                Shop_product_Sum = Shop_product_Sum + entity.Shop_Evaluate_Product;
                Shop_service_Count = Shop_service_Count + 1;
                Shop_service_Sum = Shop_service_Sum + entity.Shop_Evaluate_Service;
                Shop_delivery_Count = Shop_delivery_Count + 1;
                Shop_delivery_Sum = Shop_delivery_Sum + entity.Shop_Evaluate_Delivery;
            }
        }
        if (Shop_product_Count > 0 && Shop_product_Sum > 0)
        {
            Shop_product_Avg = Math.Round(tools.CheckFloat((Shop_product_Sum / Shop_product_Count).ToString()), 1);
        }
        else
        {
            Shop_product_Avg = 0;
        }
        if (Shop_service_Count > 0 && Shop_service_Sum > 0)
        {
            Shop_service_Avg = Math.Round(tools.CheckFloat((Shop_service_Sum / Shop_service_Count).ToString()), 1);
        }
        else
        {
            Shop_service_Avg = 0;
        }
        if (Shop_delivery_Count > 0 && Shop_delivery_Sum > 0)
        {
            Shop_delivery_Avg = Math.Round(tools.CheckFloat((Shop_delivery_Sum / Shop_delivery_Count).ToString()),1);
        }
        else
        {
            Shop_delivery_Avg = 0;
        }



        Response.Write(" <div class=\"shop_evaluateT_L\">");
        Response.Write("<ul>");
        Response.Write("<li>卖家商品满意度</li>");
        Response.Write("<li>");
        evaluate_showstar("small", Shop_product_Count, Shop_product_Avg);
        Response.Write("</li>");
        Response.Write("<li>卖家服务满意度</li>");
        Response.Write("<li>");
        evaluate_showstar("small", Shop_service_Count, Shop_service_Avg);
        Response.Write("</li>");
        Response.Write("<li>发货速度满意度</li>");
        Response.Write("<li>");
        evaluate_showstar("small", Shop_delivery_Count, Shop_delivery_Avg);
        Response.Write("</li>");
        Response.Write("</ul>");
        Response.Write("</div>");

        Response.Write("<div class=\"shop_evaluateT_R\">");
        Response.Write("     <p><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/></p>");
        Response.Write("     <p><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/></p>");
        Response.Write("     <p><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\"style=\"display:inline;\"/><img src=\"images/dp_pic02_03.jpg\" style=\"display:inline;\"/></p>");
        Response.Write("</div>");
        Response.Write("<div class=\"shop_evaluateT_RB\">");
        Response.Write("     <span>1分<br />非常不满</span>");
        Response.Write("     <span>2分<br />不满意</span>");
        Response.Write("     <span>3分<br />一般</span>");
        Response.Write("     <span>4分<br />满意</span>");
        Response.Write("     <span>5分<br />非常满意</span>");
        Response.Write("</div>");

    }

    //带评分说明评论星标显示
    public void evaluate_showstar(string style, int Review_count, double review_average)
{
    string img_on = "";
    string img_off = "";
    string img_half = "";
    int Maxnum = 0;
    int i = 0;
    int j = 0;
    string site_url = "";
	//site_url = Application(Session("current_language") + "_" + "site_url");
	switch (style) {
		case "small":
		case "detail":
		case "review":
            img_on = site_url + "/images/dp_pic02_03.jpg";
            img_off = site_url + "/images/dp_pic02_04.jpg";
            img_half = site_url + "/images/dp_pic02_01.jpg";
			break;
		case "big":
		case "summary":
            img_on = site_url + "/images/dp_pic02_03.jpg";
            img_off = site_url + "/images/dp_pic02_04.jpg";
            img_half = site_url + "/images/dp_pic02_01.jpg";
			break;
		default:
            img_on = site_url + "/images/dp_pic02_03.jpg";
            img_off = site_url + "/images/dp_pic02_04.jpg";
            img_half = site_url + "/images/dp_pic02_01.jpg";
			break;
	}
	Maxnum = 5;

	if (review_average > Maxnum)
		review_average = Maxnum;


	if (review_average > 0) {
		review_average = review_average * 10;
        for (i = 1; i <= review_average / 10; i++)
        {
            Response.Write("<img src=\"" + img_on + "\" border=\"0\" align=\"absmiddle\" style=\"display:inline;\">");
        }
        if ((review_average % 10) > 0)
        {
            Response.Write("<img src=\"" + img_half + "\" border=\"0\" align=\"absmiddle\" style=\"display:inline;\">");
            if (style != "review")
            {
                for (j = i + 1; j <= Maxnum; j++)
                {
                    Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\" style=\"display:inline;\">");
                }
            }
        }
        else
        {
            if (style != "review")
            {
                for (j = i; j <= Maxnum; j++)
                {
                    Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\" style=\"display:inline;\">");
                }
            }
        }
    }
    else
    {
        if (style != "review")
        {
            for (i = 1; i <= Maxnum; i++)
            {
                Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\" style=\"display:inline;\">");
            }
        }
    }

	switch (style) {
		case "small":
		case "big":
			Response.Write("</a>");
			break;
		case "summary":
		case "detail":
		case "review":
			break;
	}

	switch (style) {
		case "small":
		case "big":
			Response.Write(" <span class=\"t14b\">" + review_average / 10 + "</span>分&nbsp;  ( " + Review_count + "人 )");
			break;
		case "summary":
		case "detail":
		case "review":
			break;
	}
}

    //评论星标显示
    public void evaluate_showstar_nograde(string style, int Review_count, double review_average)
    {
        string img_on = "";
        string img_off = "";
        string img_half = "";
        int Maxnum = 0;
        int i = 0;
        int  j = 0;
        string  site_url = "";
        //site_url = Application(Session("current_language") + "_" + "site_url");
        switch (style)
        {
            case "small":
            case "detail":
            case "review":
                img_on = site_url + "/images/dp_pic02_03.jpg";
                img_off = site_url + "/images/dp_pic02_04.jpg";
                img_half = site_url + "/images/dp_pic02_01.jpg";
                break;
            case "big":
            case "summary":
                img_on = site_url + "/images/dp_pic02_03.jpg";
                img_off = site_url + "/images/dp_pic02_04.jpg";
                img_half = site_url + "/images/dp_pic02_01.jpg";
                break;
            default:
                img_on = site_url + "/images/dp_pic02_03.jpg";
                img_off = site_url + "/images/dp_pic02_04.jpg";
                img_half = site_url + "/images/dp_pic02_01.jpg";
                break;
        }
        Maxnum = 5;

        if (review_average > Maxnum)
            review_average = Maxnum;


        if (review_average > 0)
        {
            review_average = review_average * 10;
            for (i = 1; i <= review_average / 10; i++)
            {
                Response.Write("<img src=\"" + img_on + "\" border=\"0\" align=\"absmiddle\">");
            }
            if ((review_average % 10) > 0)
            {
                Response.Write("<img src=\"" + img_half + "\" border=\"0\" align=\"absmiddle\">");
                if (style != "review")
                {
                    for (j = i + 1; j <= Maxnum; j++)
                    {
                        Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\">");
                    }
                }
            }
            else
            {
                if (style != "review")
                {
                    for (j = i; j <= Maxnum; j++)
                    {
                        Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\">");
                    }
                }
            }
        }
        else
        {
            if (style != "review")
            {
                for (i = 1; i <= Maxnum; i++)
                {
                    Response.Write("<img src=\"" + img_off + "\" border=\"0\" align=\"absmiddle\">");
                }
            }
        }

        switch (style)
        {
            case "small":
            case "big":
                Response.Write("</a>");
                break;
            case "summary":
            case "detail":
            case "review":
                break;
        }

    }

    //店铺评论列表
    public void Shop_evaluate_list()
    {
        int i = 0;
        i = 0;
        string pageurl = "shop_evaluate.aspx?key=key";
        
        string td_bgcolor = "";
        string member_nickname = "";
        int member_id = 0;
        string member_email = null;
        System.Data.SqlClient.SqlDataReader rs_ask_list = null;
        td_bgcolor = "#F6F8F9";

        Response.Write("<div class=\"shop_evaluat_contentT\">");
        Response.Write("      <div class=\"shop_evaluat_contentTL\">评价内容</div>");
        Response.Write("      <div class=\"shop_evaluat_contentTM\">评价人</div>");
        Response.Write("      <div class=\"shop_evaluat_contentTR\">商品信息</div>");
        Response.Write(" </div>");

        Response.Write("      <div class=\"shop_evaluat_contentM\">");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Query.CurrentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        PageInfo Page = MyShopEvaluate.GetPageInfo(Query);
        if (entitys != null)
        {
            Response.Write("<ul>");
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                i = i + 1;
                if (i % 2 == 0)
                {
                    Response.Write("<li id=\"list_bgcolor\">");
                }
                else
                {
                    Response.Write("<li>");
                }
                Response.Write("<div class=\"shop_evaluat_listL\">" + entity.Shop_Evaluate_Note + "<br>[" + entity.Shop_Evaluate_Addtime + "]</div>");
                Response.Write("<div class=\"shop_evaluat_listM\">买家：" + Get_Member_Name(entity.Shop_Evaluate_MemberID) + "</div>");
                //Response.Write("<div class=\"shop_evaluat_listR\"><a href=\"/detail.aspx?product_id="+entity.Shop_Evaluate_Productid+"\" target=\"_blank\" style=\"color:#3366ff;\">" + GetProductName(entity.Shop_Evaluate_Productid) + "</a> <span class=\"t12_orange\">" + pub.FormatCurrency(Evaluate_Product_Price(entity.Shop_Evaluate_OrderId, entity.Shop_Evaluate_Productid)) + "</span></div>");
                Response.Write("<div class=\"shop_evaluat_listR\"><a href=\"/detail.aspx?product_id=" + entity.Shop_Evaluate_Productid + "\" target=\"_blank\" style=\"color:#3366ff;\">" + GetProductName(entity.Shop_Evaluate_Productid) + "</a> <span class=\"t12_orange\">" + pub.FormatCurrency(Evaluate_Product_Price(entity.Shop_Evaluate_ContractID, entity.Shop_Evaluate_Productid)) + "</span></div>");
                Response.Write("</li>");
            }
            Response.Write("</ul>");
        }
        else
        {
            Response.Write("<center>暂无评价信息</center>");
        }
        Response.Write("</div>");
        Response.Write("<div class=\"blank10\"></div>");
        Response.Write("<div style=\"float:right\">");
        pub.Page(Page.PageCount, Page.CurrentPage, pageurl, Page.PageSize, Page.RecordCount);
        Response.Write("</div>");

    }


    #endregion

    //获取商家标签
    public string GetSupplierTag()
    {
        string html = "";
        int Supplier_ID = tools.NullInt(Session["shop_supplier_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierTagInfo.Supplier_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierTagInfo.Supplier_Tag_ID", "in", "select Supplier_RelateTag_TagID from Supplier_RelateTag where Supplier_RelateTag_SupplierID="+Supplier_ID));
        Query.OrderInfos.Add(new OrderInfo("SupplierTagInfo.Supplier_Tag_ID", "Asc"));
        IList<SupplierTagInfo> entitys = MySupplierTag.GetSupplierTags(Query, pub.CreateUserPrivilege("169befcc-aa3b-42d1-b5b8-d1a08096bc0e"));
        if (entitys != null)
        {
            foreach (SupplierTagInfo entity in entitys)
            {
                html += "<img src=\"" + pub.FormatImgURL(entity.Supplier_Tag_Img, "fullpath") + "\" align=\"absmiddle\" alt=\""+entity.Supplier_Tag_Name+"\" title=\""+entity.Supplier_Tag_Name+"\"> ";
            }
        }
        return html;
    }

    //根据编号获取类别信息
    public CategoryInfo GetCategoryByID(int Cate_ID)
    {
        return MyCate.GetCategoryByID(Cate_ID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
    }

    //商品详情 类别链接
    public string GetCateMapByCateID(int ID)
    {
        string HTML = "";
        int CateID = ID;
        CategoryInfo cateinfo = GetCategoryByID(CateID);

        while (cateinfo != null)
        {
            HTML = "&nbsp;>&nbsp;<a href=\"/category.aspx\">" + cateinfo.Cate_Name + "</a>" + HTML;
            cateinfo = GetCategoryByID(cateinfo.Cate_ParentID);
        }
        if (ID == 0)
        {
            HTML = "&nbsp;>&nbsp;<a href=\"category.aspx\">所有商品分类</a>";
        }
        return HTML;
    }

    //产品左侧店内类目
    public void Shop_Left_Menu(int cate_id)
    {
        string arry_cate = "";
        int i = 0;
        string arry_sub = "";
        arry_cate = "";
        i = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            int icount = 1;
            foreach (SupplierCategoryInfo entity in entitys)
            {
                Response.Write("<div class=\"title07\"><span onclick=\"openShutManager(this,'box_" + icount + "')\"><a id=\"" + icount + "\" class=\"hidecontent\" onclick=\"switchTag(" + icount + ");\">" + entity.Supplier_Cate_Name + "</a></span></div>");
                Response.Write("<ul id=\"box_" + icount + "\">");
                Shop_Left_Menu_sub(cate_id, entity.Supplier_Cate_ID, 0);
                Response.Write("</ul>");                 
            }
        }
    }

    public string GetSupplerProductCate(int cate_id)
    {
        string str_position = "";
        string cate_str = "";
       
         cate_str += getProductCateByCateID(cate_id);
         str_position += getCateNames(cate_str);
        
        return str_position;
    }

    public string getProductCateByCateID(int CateID)
    {
        string strCate = CateID.ToString();
        SupplierCategoryInfo category = MyProductCate.GetSupplierCategoryByID(CateID);
        if (category != null)
        {
            strCate += getProductCateByCateID(category.Supplier_Cate_Parentid) + "," + strCate;
        }
        return strCate;
    }   

    public string getCateNames(string str)
    {
        string strNames = "";
        int a = 0;
        string[] vals = str.Split(',');

        for (int i = 0; i < vals.Length; i++)
        {
            a++;
            int cate = tools.CheckInt(vals[i]);
            SupplierCategoryInfo category = MyProductCate.GetSupplierCategoryByID(cate);

            if (category != null)
            {

                strNames += "&nbsp;&nbsp;>&nbsp;&nbsp;<a href=\"/category.aspx?cate_id=" + category.Supplier_Cate_ID + "\" style=\"color:#000000;\">" + category.Supplier_Cate_Name + "</a>";

            }

        }
        return strNames;
    }

    //产品左侧店内子类目
    public void Shop_Left_Menu_sub(int cate_id, int parentid, int count)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", parentid.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        { 
            foreach (SupplierCategoryInfo entity in entitys)
            {
                Response.Write("<li><a href=\"/category.aspx?cate_id="+ entity.Supplier_Cate_ID +"\">"+ entity.Supplier_Cate_Name +"</a></li>");
            }
            Response.Write("</ul></dd>");
        }   
                                                       
    }

    //左侧在线客服
    public string shop_online_service()
    { 
        string html="";
        string online_type="";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_Sort", "Asc"));
        IList<SupplierOnlineInfo> entitys = MyOnline.GetSupplierOnlines(Query);
        html = "<div style=\"width:190px; position:absolute; overflow:hidden;display:none; border:1px solid #dddddd;background-color:#ffffff;margin-left:90px; z-index:100;padding-bottom:5px;padding-left:5px;\" id=\"div_online1\" onmouseover=\"$('#div_online1').show();\">";
        if (entitys != null)
        {
            html += "<table width=\"180\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"footline\"><tr><td height=\"27\"><b>在线客服</b></td><td align=\"right\"><a href=\"javascript:void(0);\" onclick=\"$('#div_online1').hide();\"><font color=\"#CC0000\">关闭</font></a></td></tr></table><table width=\"170\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

            foreach (SupplierOnlineInfo entity in entitys)
            {
                if(online_type!=entity.Supplier_Online_Type)
                {
                html += "<tr><td height=\"35\" colspan=\"2\" class=\"online_type\">" +entity.Supplier_Online_Type+ "</tD></tr>";
                    online_type = entity.Supplier_Online_Type;
                }
                if(entity.Supplier_Online_Type=="QQ")
                {
                html += "<tr><td class=\"online_item\" align=\"center\" width=\"50%\">"+entity.Supplier_Online_Code+"</td><td class=\"online_item\" align=\"center\"> "+entity.Supplier_Online_Name+"</td></tr>";
                }
                else
                {
                html += "<tr><td class=\"online_item\" align=\"center\" width=\"50%\"><a href=\"msnim:chat?contact=" + entity.Supplier_Online_Code + "\"><img src=\"/images/online_msn.gif\" border=\"0\"  align=\"absmiddle\"/></a></td><td class=\"online_item\" align=\"center\"> "+entity.Supplier_Online_Name+"</td></tr>";
                }

                
            }
            html += "</table>";
        }
        html += "</div>";
        if (entitys != null)
        {
            html += "<a href=\"javascript:void(0);\" onclick=\"$('#div_online1').show();\"><i class=\"contact_01\" style=\"display:inline-block;\"></i></a>";
        }
        else
        {
            html += "<a href=\"javascript:void(0);\" onclick=\"$('#div_online1').show();\"><i class=\"contact_01\" style=\"display:inline-block;\"></i></a>";
        }
        return html;
    }

    //构建产品搜索地址
    public string Get_FilterURl(string Flag, string Value)
    {
        string Filter_URL, orderby, keyword;
        int isgallerylist, cate_id, allshop;
        double minprice, maxprice;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);
        cate_id = tools.CheckInt(Request["cate_id"]);
        orderby = tools.CheckStr(Request["orderby"]);
        keyword = tools.CheckStr(Request["keyword"]);
        minprice = tools.CheckFloat(Request["minprice"]);
        maxprice = tools.CheckFloat(Request["maxprice"]);
        Filter_URL = "/category.aspx?cate_id={cate_id}&keyword={keyword}&isgallerylist={isgallerylist}&orderby={orderby}&minprice={minprice}&maxprice={maxprice}";

        //显示方式
        if (Flag == "cate_id")
        {
            Filter_URL = Filter_URL.Replace("{cate_id}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{cate_id}", cate_id.ToString());
        }
        //显示方式
        if (Flag == "isgallerylist")
        {
            Filter_URL = Filter_URL.Replace("{isgallerylist}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{isgallerylist}", isgallerylist.ToString());
        }

        //排序
        if (Flag == "orderby")
        {
            Filter_URL = Filter_URL.Replace("{orderby}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{orderby}", orderby);
        }

        //关键词
        if (Flag == "keyword")
        {
            Value = Value.Replace("\"", "&quot;");
            Filter_URL = Filter_URL.Replace("{keyword}", Value);
        }
        else
        {
            keyword = keyword.Replace("\"", "&quot;");
            Filter_URL = Filter_URL.Replace("{keyword}", keyword);
        }

        //最低价
        if (Flag == "minprice")
        {
            Filter_URL = Filter_URL.Replace("{minprice}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{minprice}", minprice.ToString());
        }

        //最高价
        if (Flag == "maxprice")
        {
            Filter_URL = Filter_URL.Replace("{maxprice}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{maxprice}", maxprice.ToString());
        }
        return Filter_URL;
    }



    //构建产品搜索地址
    public string Get_FilterURl_Home(string Flag, string Value)
    {
        string Filter_URL, orderby, keyword;
        int isgallerylist, cate_id, allshop;
        double minprice, maxprice;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);
        cate_id = tools.CheckInt(Request["cate_id"]);
        orderby = tools.CheckStr(Request["orderby"]);
        keyword = tools.CheckStr(Request["keyword"]);
        minprice = tools.CheckFloat(Request["minprice"]);
        maxprice = tools.CheckFloat(Request["maxprice"]);
        Filter_URL = "/index.aspx?cate_id={cate_id}&keyword={keyword}&isgallerylist={isgallerylist}&orderby={orderby}&minprice={minprice}&maxprice={maxprice}";

        //显示方式
        if (Flag == "cate_id")
        {
            Filter_URL = Filter_URL.Replace("{cate_id}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{cate_id}", cate_id.ToString());
        }
        //显示方式
        if (Flag == "isgallerylist")
        {
            Filter_URL = Filter_URL.Replace("{isgallerylist}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{isgallerylist}", isgallerylist.ToString());
        }

        //排序
        if (Flag == "orderby")
        {
            Filter_URL = Filter_URL.Replace("{orderby}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{orderby}", orderby);
        }

        //关键词
        if (Flag == "keyword")
        {
            Value = Value.Replace("\"", "&quot;");
            Filter_URL = Filter_URL.Replace("{keyword}", Value);
        }
        else
        {
            keyword = keyword.Replace("\"", "&quot;");
            Filter_URL = Filter_URL.Replace("{keyword}", keyword);
        }

        //最低价
        if (Flag == "minprice")
        {
            Filter_URL = Filter_URL.Replace("{minprice}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{minprice}", minprice.ToString());
        }

        //最高价
        if (Flag == "maxprice")
        {
            Filter_URL = Filter_URL.Replace("{maxprice}", Value);
        }
        else
        {
            Filter_URL = Filter_URL.Replace("{maxprice}", maxprice.ToString());
        }
        return Filter_URL;
    }
    /// <summary>
    /// 商品排序方式及展示方式栏
    /// </summary>
    public string Product_View_Mode()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string stateCode = tools.CheckStr(Request["stateCode"]);
        string orderby = tools.CheckStr(Request["orderby"]);
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);
        int instock = tools.CheckInt(Request["instock"]);
        int indeal = tools.CheckInt(Request["indeal"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();

        orderby = orderby.Replace("\"", "&quot;").ToString();


        int isgallerylist;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        StringBuilder sb = new StringBuilder();

        sb.Append("<b id=\"titpage\"><a href=\"javascript:void(0);\">上一页</a><a href=\"javascript:void(0);\">下一页</a></b>");
        sb.Append("<ul>");
        sb.Append("<li><span style=\"font-weight: bold;\">排序：</span></li>");

        switch (orderby)
        {
            case "normal":
                sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\">默认<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\"  onclick=\"javascript:filter_setvalue('orderby','normal');\">默认<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }


        switch (orderby)
        {
            case "price_down":
                sb.Append("<li class=\"on\"><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a24\"></a><a href=\"javascript:;\" class=\"a27\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
            case "price_up":
                sb.Append("<li class=\"on\"><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a26\"></a><a href=\"javascript:;\" class=\"a25\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
            default:
                sb.Append("<li><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a24\"></a><a href=\"javascript:;\" class=\"a25\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "hotsale_down":
            //    sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\">销量<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "hotsale_up":
                sb.Append("<li class=\"on\"><span><a href=\"javascript:;\">销量<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\">销量<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "hit_down":
            //    sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hit_up');\">人气<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "hit_up":
                sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\">人气<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hit_up');\">人气<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "time_down":
            //    sb.Append("<li class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','time_up');\">上架时间<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "time_up":
                sb.Append("<li class=\"on\"><span><a href=\"javascript:;\">上架时间<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','time_up');\">上架时间<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        if (instock == 1)
        {
            sb.Append("<li><span><input name=\"instock\" id=\"instock\" type=\"checkbox\" value=\"1\" onclick=\"javascript:filter_setvalue('instock','0');\"  checked  />现货</span></li>");
        }
        else
        {
            sb.Append("<li><span><input name=\"instock\" id=\"instock\" type=\"checkbox\" value=\"0\" onclick=\"javascript:filter_setvalue('instock','1');\" />现货</span></li>");
        }

        if (indeal == 1)
        {
            sb.Append("<li><span><input name=\"indeal\" id=\"indeal\" type=\"checkbox\" value=\"1\" onclick=\"javascript:filter_setvalue('indeal','0');\" checked />有成交记录</span></li>");
        }
        else
        {
            sb.Append("<li><span><input name=\"indeal\" id=\"indeal\" type=\"checkbox\" value=\"0\" onclick=\"javascript:filter_setvalue('indeal','1');\" />有成交记录</span></li>");
        }
        sb.Append("</ul>");

        return sb.ToString();
    }



    /// <summary>
    /// 商品排序方式及展示方式栏
    /// </summary>
    public string Product_View_Mode_Home()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string stateCode = tools.CheckStr(Request["stateCode"]);
        string orderby = tools.CheckStr(Request["orderby"]);
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);
        int instock = tools.CheckInt(Request["instock"]);
        int indeal = tools.CheckInt(Request["indeal"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();

        orderby = orderby.Replace("\"", "&quot;").ToString();


        int isgallerylist;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);

        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }
        StringBuilder sb = new StringBuilder();

        sb.Append("<b id=\"titpage\"><a href=\"javascript:void(0);\">上一页</a><a href=\"javascript:void(0);\">下一页</a></b>");
        sb.Append("<ul>");
        sb.Append("<li><span style=\"font-weight: bold;\">排序：</span></li>");

        switch (orderby)
        {
            case "normal":
                //sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\">默认<img src=\"/images/icon20.jpg\"></a></span></li>");
                sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\">默认<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                //sb.Append("<li><span><a href=\"javascript:;\"  onclick=\"javascript:filter_setvalue('orderby','normal');\">默认<img src=\"/images/icon19.jpg\"></a></span></li>");
                sb.Append("<li><span><a href=\"javascript:;\"  onclick=\"javascript:filter_setvalue('orderby','normal');\">默认<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }


        switch (orderby)
        {
            case "price_down":
                sb.Append("<li class=\"on\"><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a24\"></a><a href=\"javascript:;\" class=\"a27\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
            case "price_up":
                sb.Append("<li class=\"on\"><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a26\"></a><a href=\"javascript:;\"  class=\"a25\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
            default:
                sb.Append("<li><span>价格<i><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','price_up');\" class=\"a24\"></a><a href=\"javascript:;\"  class=\"a25\" onclick=\"javascript:filter_setvalue('orderby','price_down');\"></a></i></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "hotsale_down":
            //    sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\">销量<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "hotsale_up":
                sb.Append("<li class=\"on\"><span><a href=\"javascript:;\">销量<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hotsale_up');\">销量<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "hit_down":
            //    sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hit_up');\">人气<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "hit_up":
                sb.Append("<li  class=\"on\"><span><a href=\"javascript:;\">人气<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','hit_up');\">人气<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        switch (orderby)
        {
            //case "time_down":
            //    sb.Append("<li class=\"on\"><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','time_up');\">上架时间<img src=\"/images/icon19.jpg\"></a></span></li>");
            //    break;
            case "time_up":
                sb.Append("<li class=\"on\"><span><a href=\"javascript:;\">上架时间<img src=\"/images/icon20.jpg\"></a></span></li>");
                break;
            default:
                sb.Append("<li><span><a href=\"javascript:;\" onclick=\"javascript:filter_setvalue('orderby','time_up');\">上架时间<img src=\"/images/icon19.jpg\"></a></span></li>");
                break;
        }

        if (instock == 1)
        {
            sb.Append("<li><span><input name=\"instock\" id=\"instock\" type=\"checkbox\" value=\"1\" onclick=\"javascript:filter_setvalue('instock','0');\"  checked  />现货</span></li>");
        }
        else
        {
            sb.Append("<li><span><input name=\"instock\" id=\"instock\" type=\"checkbox\" value=\"0\" onclick=\"javascript:filter_setvalue('instock','1');\" />现货</span></li>");
        }

        if (indeal == 1)
        {
            sb.Append("<li><span><input name=\"indeal\" id=\"indeal\" type=\"checkbox\" value=\"1\" onclick=\"javascript:filter_setvalue('indeal','0');\" checked />有成交记录</span></li>");
        }
        else
        {
            sb.Append("<li><span><input name=\"indeal\" id=\"indeal\" type=\"checkbox\" value=\"0\" onclick=\"javascript:filter_setvalue('indeal','1');\" />有成交记录</span></li>");
        }
        sb.Append("</ul>");

        return sb.ToString();
    }

    //产品筛选
    public void  BAK_product_filter()
    {
        string filter_str ="";
        string orderby, keyword, cate_nav, sql_type, sql_rct ;
        int isgallerylist, cate_id, brand_id, recordcount ;
        double minprice, maxprice;
        cate_nav = "";
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);
        orderby = tools.CheckStr(Request["orderby"]);
        keyword = tools.CheckStr(Request["keyword"]);
        minprice = tools.CheckFloat(Request["minprice"]);
        maxprice = tools.CheckFloat(Request["maxprice"]);
        cate_id = tools.CheckInt(Request["cate_id"]);
        keyword = keyword.Replace("\"", "&quot;");
        if(orderby == "")
        {
            orderby = "time_up";
        }

        if (cate_id > 0)
        {
            cate_nav = Get_Supplier_Cate_nav(cate_id, "&nbsp;<img src=\"/images/filter_titgap.gif\" align=\"absmiddle\">  &nbsp;");
        }
        if(cate_nav == "" )
        {
            cate_nav = "全部商品";
    }
        filter_str += "<form action=\"category.aspx\" method=\"post\" name=\"frm_search\">";
        filter_str += "<div class=\"shop_rightSearchT\">";
        filter_str += "       <div class=\"dp_blk06\" style=\"width:761px; overflow:hidden;\">";
        filter_str += "<h2>" + cate_nav + "</h2>";
        filter_str += "      </div>";

        filter_str += " </div>";

        filter_str += "<div class=\"filter_mid\" id=\"cate_sub\">";



        Response.Write(filter_str);
        Supplier_Cate_list("filter", cate_id);
        filter_str = "";
        filter_str += "</div>";

        filter_str += "<div class=\"filter_foot\">";

        
        filter_str += " &nbsp; 关键字：<input type=\"text\" name=\"keyword\" size=\"15\" value=\"" + keyword + "\">";
        filter_str += " &nbsp; &nbsp; 价格：<input type=\"text\" name=\"minprice\" size=\"5\" value=\"" + minprice + "\"> 到 <input type=\"text\" name=\"maxprice\" size=\"5\" value=\"" + maxprice + "\">&nbsp; <input type=\"image\" src=\"/images/btn_search.gif\" style=\"width:80px;height:26px;\" align=\"absmiddle\">";
        filter_str += "</div>";
        filter_str += "<div class=\"blank10\"></div>";
        filter_str += "  <div class=\"filter_sortbg\">";

        filter_str += " &nbsp; &nbsp; 排序：";
        switch( orderby)
        {
            case "time_down":
                filter_str += "<img src=\"/images/sortby_time_down_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按上架时间由后到先排序\" onclick=\"location='" + Get_FilterURl("orderby", "time_up") + "'\">";
                break;
            case "time_up":
                filter_str += "<img src=\"/images/sortby_time_up_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按上架时间由先到后排序\" onclick=\"location='" + Get_FilterURl("orderby", "time_down") + "'\">";
                break;
            default:
                filter_str += "<img src=\"/images/sortby_time_down.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按上架时间由后到先排序\" onclick=\"location='" + Get_FilterURl("orderby", "time_down") + "'\">";
                break;
        }
        switch( orderby)
        {
            case "price_up":
                filter_str += " <img src=\"/images/sortby_price_up_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" alt=\"按上架时间由低到高排序\" height=\"15\" onclick=\"location='" + Get_FilterURl("orderby", "price_down") + "'\">";
                break;
            case "price_down":
                filter_str += " <img src=\"/images/sortby_price_down_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按上架时间由高到低排序\" onclick=\"location='" + Get_FilterURl("orderby", "price_up") + "'\">";
                break;
            default:
                filter_str += " <img src=\"/images/sortby_price_up.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按上架时间由低到高排序\" onclick=\"location='" + Get_FilterURl("orderby", "price_up") + "'\">";
        break;
        }
        switch( orderby)
        {
            case "sale_up":
                filter_str += " <img src=\"/images/sortby_hotsale_down_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按销量由高到低排序\" onclick=\"location='" + Get_FilterURl("orderby", "sale_down") + "'\">";
                break;
            case "sale_down":
                filter_str += " <img src=\"/images/sortby_hotsale_up_act.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按销量由低到高排序\" onclick=\"location='" + Get_FilterURl("orderby", "sale_up") + "'\">";
                break;
            default:
                filter_str += " <img src=\"/images/sortby_hotsale_down.gif\" border=\"0\" style=\"display:inline;\" align=\"absmiddle\" width=\"24\" height=\"15\" alt=\"按销量由高到低排序\" onclick=\"location='" + Get_FilterURl("orderby", "sale_up") + "'\">";
        break;
        }

        filter_str += "</div>";
        filter_str += "</form>";
        Response.Write(filter_str);
}

    public void product_filter()
    {
        int cate_id = tools.CheckInt(Request["cate_id"]);
        int parentid = cate_id;

        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            strHTML.Append("<div class=\"b30_main\">");
            strHTML.Append("  <dl>");
            strHTML.Append("	  <dt>分类：</dt>");
            strHTML.Append("	  <dd>");

            List<SupplierCategoryInfo> subList = entitys.Where(p => p.Supplier_Cate_Parentid == cate_id).ToList();
            if (subList.Count == 0)
            {
                parentid = entitys.First(pp => pp.Supplier_Cate_ID == cate_id).Supplier_Cate_Parentid;

                subList = entitys.Where(p => p.Supplier_Cate_Parentid == parentid).ToList();

                strHTML.Append("<span><a href=\"category.aspx?cate_id=" + parentid + "\">不限</a></span>");
            }
            else
            {
                strHTML.Append("<span><a href=\"category.aspx?cate_id=" + cate_id + "\" class=\"on\">不限</a></span>");
            }

            foreach (SupplierCategoryInfo entity in subList) 
            {
                strHTML.Append("<span><a href=\"category.aspx?cate_id=" + entity.Supplier_Cate_ID + "\" " + (entity.Supplier_Cate_ID == cate_id ? "class=\"on\"" : "") + ">" + entity.Supplier_Cate_Name + "</a></span>");
            }

            List<SupplierCategoryInfo> BackSpace = entitys.Where(pp => pp.Supplier_Cate_ID == parentid).ToList();
            if (BackSpace != null && BackSpace.Count() > 0)
            {
                strHTML.Append("<span><a href=\"category.aspx?cate_id=" + BackSpace[0].Supplier_Cate_Parentid + "\">上一级</a></span>");
            }

            strHTML.Append("</dd><div class=\"clear\"></div>");
            strHTML.Append("  </dl>");
            strHTML.Append("</div>");
        }

        Response.Write(strHTML.ToString());

        string keyword = tools.CheckStr(Request["keyword"]);
        string isgallerylist = tools.CheckStr(Request["isgallerylist"]);
        string orderby = tools.CheckStr(Request["orderby"]);
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);
        int isgroup = tools.CheckInt(Request["isgroup"]);
        int ispromotion = tools.CheckInt(Request["ispromotion"]);
        int isRecommend = tools.CheckInt(Request["isRecommend"]);
        int isgift = tools.CheckInt(Request["isgift"]);
        int instock = tools.CheckInt(Request["instock"]);
        int indeal = tools.CheckInt(Request["indeal"]);
        //string stateCode = tools.CheckStr(Request["stateCode"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();
        isgallerylist = isgallerylist.Replace("\"", "&quot;").ToString();
        orderby = orderby.Replace("\"", "&quot;").ToString();

        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"category.aspx\">");
        //Response.Write("<input type=\"hidden\" name=\"tag_id\" id=\"tag_id\" value=\"" + tag_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id\" value=\"" + cate_id + "\">");
        //Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"keyword\" id=\"keyword\" value=\"" + keyword + "\">");
        Response.Write("<input type=\"hidden\" name=\"isgallerylist\" id=\"isgallerylist\" value=\"" + isgallerylist + "\">");
        Response.Write("<input type=\"hidden\" name=\"orderby\" id=\"orderby\" value=\"" + orderby + "\">");
        Response.Write("<input type=\"hidden\" name=\"instock\" id=\"instock\" value=\"" + instock + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_min\" id=\"price_min\" value=\"" + price_min + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_max\" id=\"price_max\" value=\"" + price_max + "\">");
        //Response.Write("<input type=\"hidden\" name=\"stateCode\" id=\"stateCode\" value=\"" + stateCode + "\">");

        Response.Write("<input type=\"hidden\" name=\"indeal\" id=\"indeal\" value=\"" + indeal + "\">");
        Response.Write("</form>");
        Response.Write("</div>");

    }



    public void product_filter_Home()
    {
        int cate_id = tools.CheckInt(Request["cate_id"]);
        int parentid = cate_id;

        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            strHTML.Append("<div class=\"b30_main\">");
            strHTML.Append("  <dl>");
            strHTML.Append("	  <dt>分类：</dt>");
            strHTML.Append("	  <dd>");

            List<SupplierCategoryInfo> subList = entitys.Where(p => p.Supplier_Cate_Parentid == cate_id).ToList();
            if (subList.Count == 0)
            {
                parentid = entitys.First(pp => pp.Supplier_Cate_ID == cate_id).Supplier_Cate_Parentid;

                subList = entitys.Where(p => p.Supplier_Cate_Parentid == parentid).ToList();

                strHTML.Append("<span><a href=\"index.aspx?cate_id=" + parentid + "\">不限</a></span>");
            }
            else
            {
                strHTML.Append("<span><a href=\"index.aspx?cate_id=" + cate_id + "\" class=\"on\">不限</a></span>");
            }

            foreach (SupplierCategoryInfo entity in subList)
            {
                strHTML.Append("<span><a href=\"index.aspx?cate_id=" + entity.Supplier_Cate_ID + "#CurPosition\" " + (entity.Supplier_Cate_ID == cate_id ? "class=\"on\"" : "") + ">" + entity.Supplier_Cate_Name + "</a></span>");
            }

            List<SupplierCategoryInfo> BackSpace = entitys.Where(pp => pp.Supplier_Cate_ID == parentid).ToList();
            if (BackSpace != null && BackSpace.Count() > 0)
            {
                strHTML.Append("<span><a href=\"index.aspx?cate_id=" + BackSpace[0].Supplier_Cate_Parentid + "#CurPosition\">上一级</a></span>");
            }

            strHTML.Append("</dd><div class=\"clear\"></div>");
            strHTML.Append("  </dl>");
            strHTML.Append("</div>");
        }

        Response.Write(strHTML.ToString());

        string keyword = tools.CheckStr(Request["keyword"]);
        string isgallerylist = tools.CheckStr(Request["isgallerylist"]);
        string orderby = tools.CheckStr(Request["orderby"]);
        double price_min = tools.CheckFloat(Request["price_min"]);
        double price_max = tools.CheckFloat(Request["price_max"]);
        int isgroup = tools.CheckInt(Request["isgroup"]);
        int ispromotion = tools.CheckInt(Request["ispromotion"]);
        int isRecommend = tools.CheckInt(Request["isRecommend"]);
        int isgift = tools.CheckInt(Request["isgift"]);
        int instock = tools.CheckInt(Request["instock"]);
        int indeal = tools.CheckInt(Request["indeal"]);
        //string stateCode = tools.CheckStr(Request["stateCode"]);

        keyword = keyword.Replace("\"", "&quot;").ToString();
        isgallerylist = isgallerylist.Replace("\"", "&quot;").ToString();
        orderby = orderby.Replace("\"", "&quot;").ToString();

        Response.Write("<div style=\"display:none\">");
        Response.Write("<form name=\"form_filter\" id=\"form_filter\" method=\"string\" action=\"index.aspx#CurPosition\">");
        //Response.Write("<input type=\"hidden\" name=\"tag_id\" id=\"tag_id\" value=\"" + tag_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"cate_id\" id=\"cate_id\" value=\"" + cate_id + "\">");
        //Response.Write("<input type=\"hidden\" name=\"brand_id\" id=\"brand_id\" value=\"" + Brand_ID + "\">");
        Response.Write("<input type=\"hidden\" name=\"keyword\" id=\"keyword\" value=\"" + keyword + "\">");
        Response.Write("<input type=\"hidden\" name=\"isgallerylist\" id=\"isgallerylist\" value=\"" + isgallerylist + "\">");
        Response.Write("<input type=\"hidden\" name=\"orderby\" id=\"orderby\" value=\"" + orderby + "\">");
        Response.Write("<input type=\"hidden\" name=\"instock\" id=\"instock\" value=\"" + instock + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_min\" id=\"price_min\" value=\"" + price_min + "\">");
        Response.Write("<input type=\"hidden\" name=\"price_max\" id=\"price_max\" value=\"" + price_max + "\">");
        //Response.Write("<input type=\"hidden\" name=\"stateCode\" id=\"stateCode\" value=\"" + stateCode + "\">");

        Response.Write("<input type=\"hidden\" name=\"indeal\" id=\"indeal\" value=\"" + indeal + "\">");
        Response.Write("</form>");
        Response.Write("</div>");

    }

    //类别导航
    public string Get_Supplier_Cate_nav(int cate_id, string cate_gap)
    {
        string cate_nav;
        int requestcate, cate_parentid;
        requestcate = tools.CheckInt(Request["cate_id"]);
        cate_parentid = 0;
        cate_nav = "";
        SupplierCategoryInfo categoryinfo = MyProductCate.GetSupplierCategoryByIDSupplier(cate_id, tools.NullInt(Session["shop_supplier_id"]));
        if(categoryinfo!=null)
        {
            cate_parentid=categoryinfo.Supplier_Cate_Parentid;
            if(requestcate!=cate_id)
            {
             cate_nav = "<a href=\"" + Get_FilterURl("cate_id", categoryinfo.Supplier_Cate_ID.ToString())+ "\" style=\"color:#ffffff;\">" + categoryinfo.Supplier_Cate_Name + "</a>";
            }
            else
            {
            cate_nav = categoryinfo.Supplier_Cate_Name;
            }
        }
        
        if (cate_parentid > 0)
        {
            cate_nav = Get_Supplier_Cate_nav(cate_parentid, cate_gap) + cate_gap + cate_nav;
        }
        return  cate_nav;
    }

    public int Get_SubAmount(int cate_id)
    {
        int sub_amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", cate_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            sub_amount = entitys.Count;
        }
        return sub_amount;
    }

    //获取所有子分类
    public string Get_All_SubSupplierCate(int Cate_id)
    {
        string Cate_Arry = Cate_id.ToString();


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", Cate_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Desc"));
        IList<SupplierCategoryInfo> Categorys = MyProductCate.GetSupplierCategorys(Query);
        if (Categorys != null)
        {
            foreach (SupplierCategoryInfo entity in Categorys)
            {
                //Cate_Arry = Cate_Arry + "," + entity.Cate_ID.ToString();
                Cate_Arry = Cate_Arry + "," + Get_All_SubSupplierCate(entity.Supplier_Cate_ID);
            }

        }

        return Cate_Arry;
    }

    public void Supplier_Cate_list(string  use, int cate_id)
    {
        int sub_num=0;
        int cate_parentid=0;
        SupplierCategoryInfo categoryinfo;

        switch (use)
        {
            
            case "filter":
                sub_num = Get_SubAmount(cate_id);
                QueryInfo Query = new QueryInfo();
                if (sub_num > 0 )
                {
                    Query.PageSize = 0;
                    Query.CurrentPage = 1;
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", cate_id.ToString()));
                    Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
                }
                else
                {
                    categoryinfo = MyProductCate.GetSupplierCategoryByIDSupplier(cate_id, tools.NullInt(Session["shop_supplier_id"]));
                    if(categoryinfo!=null)
                    {
                        cate_parentid=categoryinfo.Supplier_Cate_Parentid;
                    }
                    Query.PageSize = 0;
                    Query.CurrentPage = 1;
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", cate_parentid.ToString()));
                    Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
                }

                IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
                if (entitys != null)
                {
                    Response.Write("<b>分类</b> &nbsp; &nbsp; ");
                    if( sub_num > 0 || cate_id == 0)
                    {
                        Response.Write("<a href=\"" + Get_FilterURl("cate_id", cate_id.ToString()) + "\" class=\"filter_item_select\">全部</a>");
                    }
                    else
                    {
                        Response.Write("<a href=\"" + Get_FilterURl("cate_id", cate_parentid.ToString()) + "\" class=\"filter_item_unselect\">全部</a>");
                    }
                    foreach(SupplierCategoryInfo entity in entitys)
                    {
                         cate_parentid = entity.Supplier_Cate_Parentid;
                        Response.Write(" &nbsp; &nbsp; ");
                        if( cate_id ==entity.Supplier_Cate_ID)
                        {
                            Response.Write("<a href=\"" + Get_FilterURl("cate_id", entity.Supplier_Cate_ID.ToString()) + "\" class=\"filter_item_select\">" + entity.Supplier_Cate_Name + "</a>");
                        }
                        else
                        {
                            Response.Write("<a href=\"" + Get_FilterURl("cate_id", entity.Supplier_Cate_ID.ToString()) + "\" class=\"filter_item_unselect\">" + entity.Supplier_Cate_Name + "</a>");
                        }
                    }
                    if( cate_parentid > 0 )
                    {
                        Response.Write(" &nbsp; &nbsp; ");
                        categoryinfo = MyProductCate.GetSupplierCategoryByIDSupplier(cate_parentid, tools.NullInt(Session["shop_supplier_id"]));
                        if (categoryinfo != null)
                        {
                            cate_parentid = categoryinfo.Supplier_Cate_Parentid;
                        }
                        Response.Write("<a href=\"" + Get_FilterURl("cate_id", cate_parentid.ToString()) + "\" class=\"filter_item_unselect\">返回上一级</a>");
                    }
                }
               
                    break;
    }
    }

    //产品列表
    public void product_list(string uses, int cate_id, int irowmax)
    {
        string orderby, keyword, relate_note, promotionstr, page_type;
        int isgallerylist, allshop;
        double minprice, maxprice;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);
        orderby = tools.CheckStr(Request["orderby"]);
        keyword = tools.CheckStr(Request["keyword"]);
        minprice = tools.CheckFloat(Request["minprice"]);
        maxprice = tools.CheckFloat(Request["maxprice"]);


        relate_note = "";
        promotionstr = "";
        page_type = "";

        int brand_id;
        string arrkeyword, ikeyword;
        //品牌ID
        brand_id = tools.CheckInt(Request["brand_id"]);
        //排序方式
        orderby = tools.NullStr(Request["orderby"]);
        if (orderby == "")
        {
            orderby = "time_down";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = tools.NullInt(Request["page"]);
        if (Query.CurrentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        //是否列表聚合暂时屏蔽
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));

        if (cate_id > 0)
        {
            string cate_str = Get_All_SubSupplierCate(cate_id);
            string product_arry = "";
            product_arry = MyProductCate.GetSupplierProductCategorysByCateArry(cate_str);
            if (product_arry.Length == 0)
            {
                product_arry = "0";
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", product_arry));
        }

        //if (brand_id > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        //}
        if (minprice > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", ">=", minprice.ToString()));
        }
        if (maxprice > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", "<=", maxprice.ToString()));
        }


        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Name", "like", keyword));
        }

        switch (uses)
        {
            case "new":
                orderby = "";
                break;
            case "groupbuy":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsGroupBuy", "=", "1"));
                break;

        }

        //构建分页URL
        string pageurl = Get_FilterURl("none", "");
        int suppliertype = GetSupplierGrade(tools.NullInt(Session["shop_supplier_id"]));
        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }

        switch (orderby)
        {
            case "hotsale_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Asc"));
                break;
            case "hotsale_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
                break;
            case "price_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "Asc"));
                break;
            case "price_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "Desc"));
                break;
            case "time_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "Asc"));
                break;
            case "time_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "Desc"));
                break;
            case "sale_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
                break;
            case "sale_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Asc"));
                break;
            default:
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
                break;
        }

        //有货
        int instock = tools.CheckInt(Request["instock"]);
        if (instock == 1)
        {
            pageurl = pageurl + "&instock=" + instock;
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_UsableAmount", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_IsNoStock", "=", "1"));
        }

        int indeal = tools.CheckInt(Request["indeal"]);
        if (indeal == 1)
        {
            pageurl = pageurl + "&indeal=" + indeal;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SaleAmount", ">", "0"));
        }

        IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys == null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\">记录不存在或已被删除！</td></tr>");
            Response.Write("</table>");
        }
        else
        {
            //橱窗
            int irow = 0;
            StringBuilder strHTML = new StringBuilder("<ul>");
            string targetURL;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + pageurl1.FormatURL(pageurl1.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li>");
                strHTML.Append("	<div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                strHTML.Append("	<div class=\"p_box\">");
                strHTML.Append("		<div class=\"p_box_info01\">");
                strHTML.Append("		<div class=\"p_box_info02\"><a href=\"" + targetURL + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 50) + "</a></div>");
                strHTML.Append("			<b><i>成交 " + entity.Product_SaleAmount + " 件</i>售价：");
                //if (entity.Product_PriceType == 1)
                //{
                //    strHTML.Append("<strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong>");
                //}
                //else
                //{
                //    strHTML.Append("<strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong>");
                //}
              
  strHTML.Append("<strong>" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong>");
                strHTML.Append("</b>");

                //strHTML.Append("			<div class=\"p_box_info01_fox\">");
                //strHTML.Append("				<a></a>");
                //strHTML.Append("				<dl>");
                //strHTML.Append("					<dt>运费<br><strong>8.00</strong>元<br>起</dt>");
                //strHTML.Append("					<dd>");

                //IList<ProductWholeSalePriceInfo> freightList = MySalePrice.GetProductWholeSalePriceByProductID(entity.Product_ID);
                //if (freightList != null)
                //{
                //    foreach (ProductWholeSalePriceInfo SalePriceInfo in freightList)
                //    {
                //        strHTML.Append("<p><span>" + SalePriceInfo.Product_WholeSalePrice_MinAmount + "-" + SalePriceInfo.Product_WholeSalePrice_MaxAmount + "件</span>" + pub.FormatCurrency(SalePriceInfo.Product_WholeSalePrice_Price) + "</p>");
                //    }
                //}
                //strHTML.Append("					</dd>"); 
                //strHTML.Append("					<div class=\"clear\"></div>");
                //strHTML.Append("				</dl>");
                //strHTML.Append("			</div>");

                strHTML.Append("		</div>");
                //strHTML.Append("		<div class=\"p_box_info02\"><a href=\"" + targetURL + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 50) + "</a><span style=\"float:right;\" onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Product_SupplierID + 1000) + "_9999','" + entity.Product_ID + "');\"><img src=\"/images/icon04.png\" style=\"display:inline-block; vertical-align:middle; margin-left:15px;width:74px;\"></span></div>");
              
                strHTML.Append("		<div class=\"p_box_info04\"><a href=\"" + targetURL + "\" target=\"_blank\" class=\"a16\">加入购物车</a><a href=\"javascript:;\" onclick=\"favorites_add_ajax("+entity.Product_ID+",'product');\" class=\"a17\">加入收藏</a></div>");
                strHTML.Append("	</div>");
                strHTML.Append("</li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div><script type=\"text/javascript\">$('#titpage').html('<a href=\"" + (page.CurrentPage <= 1 ? "javascript:void(0);" : pageurl + "&page=" + (page.CurrentPage - 1)) + "#CurPosition\">上一页</a><a href=" + (page.CurrentPage >= page.PageCount ? "javascript:void(0);" : pageurl + "&page=" + (page.CurrentPage + 1)) + "#CurPosition>下一页</a>')</script>");
            Response.Write(strHTML.ToString());

            pub.Page(page.PageCount, page.CurrentPage, pageurl, page.PageSize, page.RecordCount);
        }
    }


    //产品列表
    public void product_list_Home(string uses, int cate_id, int irowmax)
    {
        string orderby, keyword, relate_note, promotionstr, page_type;
        int isgallerylist, allshop;
        double minprice, maxprice;
        isgallerylist = tools.CheckInt(Request["isgallerylist"]);
        orderby = tools.CheckStr(Request["orderby"]);
        keyword = tools.CheckStr(Request["keyword"]);
        minprice = tools.CheckFloat(Request["minprice"]);
        maxprice = tools.CheckFloat(Request["maxprice"]);


        relate_note = "";
        promotionstr = "";
        page_type = "";

        int brand_id;
        string arrkeyword, ikeyword;
        //品牌ID
        brand_id = tools.CheckInt(Request["brand_id"]);
        //排序方式
        orderby = tools.NullStr(Request["orderby"]);
        if (orderby == "")
        {
            orderby = "time_down";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 16;
        Query.CurrentPage = tools.NullInt(Request["page"]);
        if (Query.CurrentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        //是否列表聚合暂时屏蔽
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));

        if (cate_id > 0)
        {
            string cate_str = Get_All_SubSupplierCate(cate_id);
            string product_arry = "";
            product_arry = MyProductCate.GetSupplierProductCategorysByCateArry(cate_str);
            if (product_arry.Length == 0)
            {
                product_arry = "0";
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", product_arry));
        }

        if (brand_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_BrandID", "=", brand_id.ToString()));
        }
        if (minprice > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", ">=", minprice.ToString()));
        }
        if (maxprice > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_Price", "<=", maxprice.ToString()));
        }


        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Name", "like", keyword));
        }

        switch (uses)
        {
            case "new":
                orderby = "";
                break;
            case "groupbuy":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsGroupBuy", "=", "1"));
                break;

        }

        //构建分页URL
        string pageurl = Get_FilterURl_Home("none", "");
        int suppliertype = GetSupplierGrade(tools.NullInt(Session["shop_supplier_id"]));
        if (isgallerylist != 0)
        {
            isgallerylist = 1;
        }

        switch (orderby)
        {
            case "hotsale_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Asc"));
                break;
            case "hotsale_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
                break;
            case "price_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "Asc"));
                break;
            case "price_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Price", "Desc"));
                break;
            case "time_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "Asc"));
                break;
            case "time_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Addtime", "Desc"));
                break;
            case "sale_up":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
                break;
            case "sale_down":
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Asc"));
                break;
            default:
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
                break;
        }

        //有货
        int instock = tools.CheckInt(Request["instock"]);
        if (instock == 1)
        {
            pageurl = pageurl + "&instock=" + instock;
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_UsableAmount", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.Product_IsNoStock", "=", "1"));
        }

        int indeal = tools.CheckInt(Request["indeal"]);
        if (indeal == 1)
        {
            pageurl = pageurl + "&indeal=" + indeal;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SaleAmount", ">", "0"));
        }

        IList<ProductInfo> entitys = Myproduct.GetProducts(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = Myproduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys == null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\">记录不存在或已被删除！</td></tr>");
            Response.Write("</table>");
        }
        else
        {
            //橱窗
            int irow = 0;
            StringBuilder strHTML = new StringBuilder("<ul>");
            string targetURL;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + pageurl1.FormatURL(pageurl1.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li>");
                strHTML.Append("	<div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                strHTML.Append("	<div class=\"p_box\">");
                strHTML.Append("		<div class=\"p_box_info01\">");
                strHTML.Append("		<div class=\"p_box_info02\"><a href=\"" + targetURL + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 20) + "</a></div>");
                strHTML.Append("			<b><i>成交 " + entity.Product_SaleAmount + " 件</i>售价：");
                //if (entity.Product_PriceType == 1)
                //{
                //    strHTML.Append("<strong>" + pub.FormatCurrency(entity.Product_Price) + "</strong>");
                //}
                //else
                //{
                //    strHTML.Append("<strong>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</strong>");
                //}

                strHTML.Append("<strong>" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong>");
                strHTML.Append("</b>");

                //strHTML.Append("			<div class=\"p_box_info01_fox\">");
                //strHTML.Append("				<a></a>");
                //strHTML.Append("				<dl>");
                //strHTML.Append("					<dt>运费<br><strong>8.00</strong>元<br>起</dt>");
                //strHTML.Append("					<dd>");

                //IList<ProductWholeSalePriceInfo> freightList = MySalePrice.GetProductWholeSalePriceByProductID(entity.Product_ID);
                //if (freightList != null)
                //{
                //    foreach (ProductWholeSalePriceInfo SalePriceInfo in freightList)
                //    {
                //        strHTML.Append("<p><span>" + SalePriceInfo.Product_WholeSalePrice_MinAmount + "-" + SalePriceInfo.Product_WholeSalePrice_MaxAmount + "件</span>" + pub.FormatCurrency(SalePriceInfo.Product_WholeSalePrice_Price) + "</p>");
                //    }
                //}
                //strHTML.Append("					</dd>"); 
                //strHTML.Append("					<div class=\"clear\"></div>");
                //strHTML.Append("				</dl>");
                //strHTML.Append("			</div>");

                strHTML.Append("		</div>");
             

                strHTML.Append("		<div class=\"p_box_info04\"><a href=\"" + targetURL + "\" target=\"_blank\" class=\"a16\">加入购物车</a><a href=\"javascript:;\" onclick=\"favorites_add_ajax(" + entity.Product_ID + ",'product');\" class=\"a17\">加入收藏</a></div>");
                strHTML.Append("	</div>");
                strHTML.Append("</li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div><script type=\"text/javascript\">$('#titpage').html('<a href=\"" + (page.CurrentPage <= 1 ? "javascript:void(0);" : pageurl + "&page=" + (page.CurrentPage - 1)) + "#CurPosition\">上一页</a><a href=" + (page.CurrentPage >= page.PageCount ? "javascript:void(0);" : pageurl + "&page=" + (page.CurrentPage + 1)) + "#CurPosition>下一页</a>')</script>");
            Response.Write(strHTML.ToString());

            pub.Page(page.PageCount, page.CurrentPage, pageurl, page.PageSize, page.RecordCount);
        }
    }

    //根据商品编号获取商品信息
    public ProductInfo GetProductByID(int ID)
    {
        ProductInfo entity = Myproduct.GetProductByID(ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entity != null)
        {
            if (entity.Product_SupplierID != tools.NullInt(Session["shop_supplier_id"]))
            {
                entity = null;
            }
        }   
        return entity;
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

    //商品详情
    public void Product_Detail(ProductInfo productinfo)
    {
        string Product_Img, S_Product_Img, Product_Img_Ext_1, S_Product_Img_Ext_1, Product_Img_Ext_2, S_Product_Img_Ext_2;
        string Product_Img_Ext_3, S_Product_Img_Ext_3, Product_Img_Ext_4, S_Product_Img_Ext_4;
        string Product_Name, Brand_Name;
        double Product_Price = 0;
        int Product_ID, Brand_ID;
        bool cxflag = false;
        Product_Name = productinfo.Product_Name;
        Product_ID = productinfo.Product_ID;
        Brand_ID = productinfo.Product_BrandID;
        Brand_Name = "";
        BrandInfo brandinfo = MyBrand.GetBrandByID(Brand_ID, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        if (brandinfo != null)
        {
            Brand_Name = brandinfo.Brand_Name;
        }

        string[] Img_Arry = GetProductImg(Product_ID);

        Product_Img = pub.FormatImgURL(Img_Arry[0], "fullpath");
        S_Product_Img = pub.FormatImgURL(Img_Arry[0], "thumbnail");
        Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "fullpath");
        S_Product_Img_Ext_1 = pub.FormatImgURL(Img_Arry[1], "thumbnail");
        Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "fullpath");
        S_Product_Img_Ext_2 = pub.FormatImgURL(Img_Arry[2], "thumbnail");
        Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "fullpath");
        S_Product_Img_Ext_3 = pub.FormatImgURL(Img_Arry[3], "thumbnail");
        Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "fullpath");
        S_Product_Img_Ext_4 = pub.FormatImgURL(Img_Arry[4], "thumbnail");

        if (Product_Img == "/images/detail_no_pic.gif")
        {
            Product_Img = "/images/detail_no_pic_big.gif";
            S_Product_Img = "/images/detail_no_pic_big.gif";
        }

        //商品详情头部信息
        Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"font-size: 20px; font-weight: bolder;\">" + Product_Name + "<span style=\"color: " + productinfo.Product_NoteColor + ";\">" + productinfo.Product_Note + "</span></td></tr></table>");
        Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"padding-top:10px;\">");
        Response.Write("<tr><td width=\"310\" align=\"center\" valign=\"top\">");
        //商品图片
        Response.Write("<table border=\"0\" width=\"300\" height=\"300\" cellspacing=\"0\" cellpadding=\"0\" class=\"img_border\"><tr><td align=\"center\" valign=\"middle\">");
        Response.Write("<a href=\"" + Product_Img + "\" onclick=\"javascript:window.open('/images.aspx?product_id=" + Product_ID + "&img='+MM_findObj('product_img').src);\" class=\"jqzoom\" id=\"demo1\"  name=\"demo1\" title=\"" + Product_Name + "\">");
        Response.Write("<img id=\"product_img\" src=\"" + Product_Img + "\" width=\"300\" height=\"300\" border=\"0\" alt=\"" + Product_Name + "\"/></a>");
        Response.Write("</td></tr></table>");

        //缩略图
        Response.Write("            <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        Response.Write("              <tr>");
        if (Product_Img != "/images/detail_no_pic.gif")
        {
            Response.Write("            <td id=\"product_img_s_1\" width=\"58\" height=\"66\" align=\"center\" class=\"img_border_on\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img + "',1);switchimgborder('product_img_s_1');\" onmouseover=\"javascript:if(this.className=='img_border_off'){this.className='img_border_over';}\" onmouseout=\"javascript:if(this.className!='img_border_on'){this.className='img_border_off';}\">");
            Response.Write("<img src=\"" + S_Product_Img + "\" border=\"0\" width=\"50\" height=\"50\" onload=\"javascript:AutosizeImage(this,50,50);MM_preloadImages('" + Product_Img + "');\" ></a></td>");
            Response.Write("            <td width=\"3\"></td>");
        }

        if (Product_Img_Ext_1 != "/images/detail_no_pic.gif")
        {
            Response.Write("            <td id=\"product_img_s_2\" width=\"58\" height=\"66\" align=\"center\" class=\"img_border_off\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_1 + "',1);switchimgborder('product_img_s_2');\" onmouseover=\"javascript:if(this.className=='img_border_off'){this.className='img_border_over';}\" onmouseout=\"javascript:if(this.className!='img_border_on'){this.className='img_border_off';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_1 + "\" border=\"0\" width=\"50\" height=\"50\" onload=\"javascript:AutosizeImage(this,50,50);MM_preloadImages('" + Product_Img_Ext_1 + "');\" ></a></td>");
            Response.Write("            <td width=\"3\"></td>");
        }

        if (Product_Img_Ext_2 != "/images/detail_no_pic.gif")
        {
            Response.Write("            <td id=\"product_img_s_3\" width=\"58\" height=\"66\" align=\"center\" class=\"img_border_off\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_2 + "',1);switchimgborder('product_img_s_3');\" onmouseover=\"javascript:if(this.className=='img_border_off'){this.className='img_border_over';}\" onmouseout=\"javascript:if(this.className!='img_border_on'){this.className='img_border_off';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_2 + "\" border=\"0\" width=\"50\" height=\"50\" onload=\"javascript:AutosizeImage(this,50,50);MM_preloadImages('" + Product_Img_Ext_2 + "');\" ></a></td>");
            Response.Write("            <td width=\"3\"></td>");
        }

        if (Product_Img_Ext_3 != "/images/detail_no_pic.gif")
        {
            Response.Write("            <td id=\"product_img_s_4\" width=\"58\" height=\"66\" align=\"center\" class=\"img_border_off\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_3 + "',1);switchimgborder('product_img_s_4');\" onmouseover=\"javascript:if(this.className=='img_border_off'){this.className='img_border_over';}\" onmouseout=\"javascript:if(this.className!='img_border_on'){this.className='img_border_off';}\">");
            Response.Write("<img src=\"" + Product_Img_Ext_3 + "\" border=\"0\" width=\"50\" height=\"50\" onload=\"javascript:AutosizeImage(this,50,50);MM_preloadImages('" + Product_Img_Ext_3 + "');\" ></a></td>");
            Response.Write("            <td width=\"3\"></td>");
        }

        if (Product_Img_Ext_4 != "/images/detail_no_pic.gif")
        {
            Response.Write("            <td id=\"product_img_s_5\" width=\"58\" height=\"66\" align=\"center\" class=\"img_border_off\" onclick=\"javascript:MM_swapImage('product_img','','" + Product_Img_Ext_4 + "',1);switchimgborder('product_img_s_5');\" onmouseover=\"javascript:if(this.className=='img_border_off'){this.className='img_border_over';}\" onmouseout=\"javascript:if(this.className!='img_border_on'){this.className='img_border_off';}\">");
            Response.Write("<img src=\"" + Product_Img_Ext_4 + "\" border=\"0\" width=\"50\" height=\"50\" onload=\"javascript:AutosizeImage(this,50,50);MM_preloadImages('" + Product_Img_Ext_4 + "');\" ></a></td>");
            //Response.Write("            <td width=\"2\"></td>");
        }

        Response.Write("              </tr>");
        Response.Write("            </table>");

        Response.Write("            <table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"padding-bottom:10px;padding-top:10px;\">");
        Response.Write("           <tr><td align=\"center\" style=\"line-height:20px;\"></td>");
        Response.Write("              </tr>");
        Response.Write("            <tr><td align=\"center\">");
        Response.Write("<a href=\"/images.aspx?product_id=" + Product_ID + "\" target=\"_blank\" class=\"a_t12_blue\"><img src=\"/images/ico_view.jpg\" align=\"absmiddle\"> 查看大图</a> </td></tr> ");
        Response.Write("<tr><td height=\"30px;\" style=\"padding-left:3px;\"><div id=\"ckepop\">");

        Response.Write("<a href=\"http://www.jiathis.com/share/\" class=\"jiathis jiathis_txt jtico jtico_jiathis\" target=\"_blank\"></a>");
        Response.Write("<a class=\"jiathis_button_tsina\">新浪微博</a>");
        Response.Write("<a class=\"jiathis_button_renren\">人人网</a>");
        Response.Write("</div>");

        Response.Write("<script type=\"text/javascript\" src=\"http://v2.jiathis.com/code/jia.js\" charset=\"utf-8\"></script>");
        Response.Write("</td></tr>");
        Response.Write("            </table>");

        Response.Write("</td><td width=\"10\"></td><td valign=\"top\">");

        Product_Price = pub.Get_Member_Grade_Price(productinfo.Product_ID, productinfo.Product_Price);
        //商品信息
        Response.Write("<table width=\"280px\" style=\"margin-left:0px;\" border=\"0\" cellpadding=\"2\" cellspacing=\"0\">");

        //Response.Write("<tr><td height=\"22\"><span style=\"margin-right:6px;\">副</span><span style=\"margin-right:6px;\">名</span>称：" + productinfo.Product_SubName + "</td></tr>");
        //Response.Write("<tr><td height=\"22\">商品编号：" + productinfo.Product_Code + "</td></tr>");
        //if (Brand_Name != "")
        //{
        //    Response.Write("<tr><td height=\"22\">商品品牌：" + Brand_Name + "</td></tr>");
        //}

        if (productinfo.Product_Maker != "")
        {
            Response.Write("<tr><td height=\"22\">生产企业：" + productinfo.Product_Maker + "</td></tr>");
        }
        Response.Write("<tr><td id=\"buyextend\"></td></tr>");
        Response.Write("<tr><td height=\"22\">商品评分：");

        for (int i = 1; i <= (int)productinfo.Product_Review_Average; i++)
        {
            Response.Write("<img src=\"/images/icon_star_on_small.gif\" />");
        }
        for (int i = 1; i <= 5 - (int)productinfo.Product_Review_Average; i++)
        {
            Response.Write("<img src=\"/images/icon_star_off_small.gif\" />");
        }
        Response.Write("&nbsp;&nbsp;<a class=\"jisuan\">" + Product_Review(productinfo.Product_ID, "4,5") + "%</a></td></tr>");

        
            Response.Write("<tr><td height=\"22\"><span style=\"margin-right:24px;\">库</span>存：");
            if ((productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0) || (productinfo.Product_IsNoStock == 1))
            {
                Response.Write(" 有货 ");
            }
            else
            {
                Response.Write(" (暂时无货，<a id=\"openBox1\" onclick=\"CheckLogin('/product/product_notify.aspx?pid=" + Product_ID + "');\" class=\"a_t12_blue\">到货通知</a>)<script>$('#openBox1').zxxbox();</script>");
            }
            Response.Write("</td></tr>");
            PromotionLimitInfo limitproduct = pub.GetPromotionLimitByProductID(Product_ID);
            int product_islimit = 0;
            if (productinfo.Product_IsCoinBuy == 0)
            {

                if (limitproduct != null)
                {
                    product_islimit = 1;
                    cxflag = true;

                    Response.Write("<tr><td colspan=\"2\" height=\"22\" style=\"color:#333333;background:#ffffe3;border:1px solid #e1dc9a; padding-left:5px; line-height:30px;\"><span style=\"margin-right:6px;\">限</span><span style=\"margin-right:6px;\">时</span>价：<span class=\"t12_red\" style=\"color:#000000;\"><strong class=\"size\">" + pub.FormatCurrency(limitproduct.Promotion_Limit_Price) + "</strong> &nbsp; 优惠节省：<span class=\"t12_red\">" + pub.FormatCurrency(productinfo.Product_MKTPrice - limitproduct.Promotion_Limit_Price) + "</span>");

                    TimeSpan span1 = limitproduct.Promotion_Limit_Endtime - DateTime.Now;
                    int timespan;
                    timespan = (span1.Days * (24 * 3600)) + (span1.Hours * 3600) + (span1.Minutes * 60) + (span1.Seconds);
                    Response.Write("              <br/>");
                    Response.Write("                <span class=\"t12\" height=\"22\" colspan=\"2\" id=\"limit_tip\" name=\"limit_tip\">");

                    Response.Write("<script>updatetime(" + timespan + ",'limit_tip')</script></span>");
                    Response.Write("</td>");
                    Response.Write("              </tr>");
                }

            }
            else
            {
                Response.Write("<tr><td>所需积分：<strong class=\"size\">" + productinfo.Product_CoinBuy_Coin + "</strong>&nbsp;" + Application["Coin_Name"].ToString() + "</td></tr>");
            }
            Response.Write("<tr><td height=\"5px\"></td></tr>");
            Response.Write("<tr><td width=\"100%\" class=\"jifen\">");

            Response.Write("<p>");

            Response.Write("购买数量：<img src=\"/images/ico_unfold.jpg\" style=\"cursor:pointer;\" onclick=\"$.ajaxSetup({async: false});if(parseInt($('#buy_amount').val())>1){$('#buy_amount').attr('value',parseInt($('#buy_amount').val())-1);};\" width=\"9\" height=\"9\"/>&nbsp;");
            Response.Write("<input name=\"buy_amount\" id=\"buy_amount\" type=\"text\" onkeyup=\"if(isNaN($(this).val())){$(this).val('1');}\" value=\"1\" style=\"width:32px;text-align:center; height:16px;border:1px solid #999999;\"/>&nbsp;");
            Response.Write("<img src=\"/images/ico_fold.jpg\" style=\"cursor:pointer;\" onclick=\"$.ajaxSetup({async: false});if(parseInt($('#buy_amount').val())<=99999){$('#buy_amount').attr('value',parseInt($('#buy_amount').val())+1);};\" width=\"9\" height=\"9\"/>");

            Response.Write("</p>");
            Response.Write("<p class=\"shi\"></p>");
            Response.Write("<p style=\"height:30px; border: 1px solid #cccccc; background: #ffffff; padding-left: 16px;line-height: 30px; width: 405px;\">");
            Response.Write("<img src=\"/images/ico_gift.jpg\" width=\"16\" height=\"14\" align=\"absmiddle\" style=\"margin-right: 10px;\" />");
            if (productinfo.Product_IsCoinBuy == 0)
            {
                //检查是否赠送指定积分
                Response.Write("购买该商品，可获得");

                if (productinfo.Product_IsGiftCoin == 1)
                {
                    Response.Write("<span style=\"color: #cc0000; font-weight: bolder;\">" + (int)(Product_Price * productinfo.Product_Gift_Coin) + "</span>积分</p>");

                }
                else
                {
                    Response.Write("<span style=\"color: #cc0000; font-weight: bolder;\">" + pub.Get_Member_Coin(Product_Price) + "</span>积分</p>");
                }
            }
            else
            {
                Response.Write("积分换购商品不能获得积分</p>");
            }
            Response.Write("<p class=\"shi\"></p>");
            Response.Write("<p class=\"xiaoche\" style=\"margin-bottom:20px;\">");
            if (productinfo.Product_IsInsale == 1)
            {
                if ((productinfo.Product_IsNoStock == 0 && productinfo.Product_UsableAmount > 0) || (productinfo.Product_IsNoStock == 1))
                {
                    Response.Write("<a href=\"" + Application["Site_URL"] + "/cart/cart_do.aspx?action=add&product_id=" + Product_ID + "\" onclick=\"javascript:return AddCartExt(this);\"><img src=\"/images/btn_addcart.jpg\" width=\"143\" height=\"34\" border=\"0\" alt=\"添加到购物车\" /></a>");
                }
                else
                {
                    Response.Write("<a href=\"javascript:void(0);\"><img src=\"/Images/btn_unbuy.jpg\" width=\"135\" height=\"37\" border=\"0\" alt=\"暂无该商品\" /></a>");
                }
                Response.Write("<a onclick=\"AjaxCheckLogin_ShouCang('/detail.aspx?product_id=" + productinfo.Product_ID + "','" + productinfo.Product_ID + "','openBox_" + productinfo.Product_ID + "');\" id=\"openBox_" + productinfo.Product_ID + "\"><img src=\"/images/btn_addfavor.jpg\" style=\" cursor:pointer;\" width=\"113\" height=\"30\" border=\"0\" alt=\"收藏该商品\" /></a>");
                Response.Write("<script>$('#openBox_" + productinfo.Product_ID + "').zxxbox();</script>");
            }

            Response.Write("</p>");
            Response.Write("<p class=\"fei\"></p>");
            //Response.Write("<p class=\"fei\">运费计算：查询相应地区的运输费用、方式和时间<a href=\"#\" class=\"jisuan\">[开始计算]</a></p>");

            Response.Write("</td></tr>");
            Response.Write("<tr><td height=\"2\"></td></tr>");
            string promotion_policy = Get_Promotion_Policy(productinfo.Product_ID);
            string gift = Get_Promotion_Gift(Product_ID);
            if (promotion_policy + gift != "")
            {
                Response.Write("<tr><td width=\"100%\">");
                Response.Write("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"450\" style=\"border:1px solid #cccccc;margin-left:-2px;margin-right:-2px;\" bgcolor=\"#ffffff\">");
                Response.Write("<tr><td style=\"color:#D94D00; font-weight:bold; font-size:12px; height:25px;ling-height:25px;padding-left:5px;\">温馨提示</td></tr>");
                Response.Write("<tr><td style=\"padding-left:10px;\">");

                if (promotion_policy != "")
                {
                    Response.Write("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">");
                    Response.Write("              <tr>");
                    Response.Write("                <td class=\"t12_red\" style=\"line-height:22px;\">" + promotion_policy + "</td>");
                    Response.Write("              </tr>");
                    Response.Write("</table>");
                }
                //赠品优惠
                if (gift != "")
                {
                    Response.Write("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">");
                    Response.Write("<tr><td class=\"t12_red\" style=\"line-height:22px;\">" + gift + "</td></tr>");
                    Response.Write("</table>");
                }
                Response.Write("</td></tr>");
                Response.Write("</table>");

                Response.Write("</td></tr>");
            }
        
        Response.Write("</table>");

        Response.Write("</td></tr>");
        Response.Write("</table>");
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

    //商品评论信息概况
    public void Product_Reviews_Info(int product_id, int Product_Review_Count, double Product_Review_Average)
    {
        Product_Review_Average = tools.CheckFloat(Product_Review_Average.ToString());

        Response.Write("<div id=\"dg11\" style=\"line-height:23px;\">");
        Response.Write("  <div id=\"dg11-1\" style=\" color:#E06700; text-align:center; font-size:30px; font-weight:bold; font-family:宋体;\">");
        Response.Write(Product_Review(product_id, "4,5") + "%");
        Response.Write("  <p style=\" margin-top:7px; font-size:12px; font-weight:normal;\">好评度</p></div>");
        Response.Write("  <div id=\"dg11-2\">");
        Response.Write("    <table width=\"238\" border=\"0\">");
        Response.Write("      <tr><td width=\"40\">好评</td>");
        Response.Write("      <td width=\"152\">" + Product_Review_Percentage(product_id, "5,4") + "</td>");
        Response.Write("      <td width=\"60\"></td></tr>");
        Response.Write("      <tr><td width=\"40\">中评</td>");
        Response.Write("      <td width=\"152\">" + Product_Review_Percentage(product_id, "3") + "</td>");
        Response.Write("      <td width=\"60\"></td></tr>");
        Response.Write("      <tr><td width=\"40\">差评</td>");
        Response.Write("      <td width=\"152\">" + Product_Review_Percentage(product_id, "1,2") + "</td>");
        Response.Write("      <td width=\"60\"></td></tr>");
        Response.Write("    </table>");
        Response.Write("  </div>");
        Response.Write("  <div id=\"dg11-3\">");
        Response.Write("  <table><tr><td style=\"vertical-align:top;\"><img src=\"/images/jifen11.jpg\" style=\"float:left;margin-top:3px;\" width=\"16\" height=\"14\" /></td><td style=\"vertical-align:top;\">发表评价即可或得积分，前五位评价用户可获的多倍积分！</td></tr></table>");
        Response.Write("  </div>");

        Response.Write("  <div id=\"dg11-4\" style=\" line-height:30px;text-align:center: color:#666666;\">共有 <span class=\"t14_red\"><b>" + Product_Review_Count + "</b></span> 篇用户评论<br/>");
        Response.Write("  <a href=\"" + Application["Site_URL"] + "/product/reviews.aspx?product_id=" + product_id + "\" style=\"color: #006699; text-decoration: none; background: none;\">");
        Response.Write("  <span style=\" text-align:center:\">查看全部评价</span></a></div>");
        Response.Write("  </div>");

    }

    //商品详情页 评论列表
    public void ProductDetail_Review_List(int Product_ID, string star)
    {
        string review_list = "";
        int icount = 0;
        string member_nickname = "游客";
        string td_bgcolor = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.NullInt(Application["Product_Review_Config_ProductCount"]);
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews != null)
        {
            review_list = review_list + "<table width=\"720\" border=\"0\" cellspacing=\"1\" cellpadding=\"5\" align=\"center\">";


            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                member_nickname = "游客";
                icount = icount + 1;
                //if (td_bgcolor == "#ffffff")
                //{
                //    td_bgcolor = "#F6F8F9";
                //}
                //else
                //{
                //    td_bgcolor = "#ffffff";
                //}
               if (entity.Shop_Evaluate_Productid > 0)
              
               
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        member_nickname = member.Member_NickName;
                    }
                }

                review_list = review_list + "<tr bgcolor=\"" + td_bgcolor + "\">";
                review_list = review_list + "<td align=\"center\">";
                review_list = review_list + "<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                review_list = review_list + "  <tr>";
                review_list = review_list + "    <td width=\"60\" height=\"30\" align=\"center\" valign=\"top\" class=\"step_num_off\">" + icount + "</td>";
                review_list = review_list + "    <td align=\"left\"><table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                review_list = review_list + "      <tr>";
                review_list = review_list + "        <td height=\"30\"><table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                review_list = review_list + "          <tr>";
                review_list = review_list + "            <td width=\"200\" height=\"30\" class=\"t12_grey\">评分：" + Review_ShowStar("detail", 0, 0, entity.Shop_Evaluate_Product) + "</td>";
                review_list = review_list + "            <td class=\"t12_grey\">评论人：" + member_nickname + "";
                review_list = review_list + "</td>";
                review_list = review_list + "          </tr>";
                review_list = review_list + "        </table></td>";
                review_list = review_list + "        </tr>";
                review_list = review_list + "      <tr>";
                review_list = review_list + "        <td height=\"30\" class=\"t12\">" + entity.Shop_Evaluate_Note + "</td>";
                review_list = review_list + "      </tr>";
                review_list = review_list + "      <tr>";
                review_list = review_list + "        <td height=\"30\"><table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                review_list = review_list + "          <tr>";
                review_list = review_list + "            <td width=\"200\" height=\"30\" class=\"info_date\">" + entity.Shop_Evaluate_Addtime + "</td>";
                review_list = review_list + "          </tr>";
                review_list = review_list + "        </table></td>";
                review_list = review_list + "        </tr>";
                review_list = review_list + "    </table></td>";
                review_list = review_list + "  </tr>";
                review_list = review_list + "</table>";
                review_list = review_list + "</td>";
                review_list = review_list + "</tr>";

            }

            review_list = review_list + "</table>";
        }
        else
        {
            review_list = review_list + "<table width=\"720\" border=\"0\" cellspacing=\"1\" align=\"center\" cellpadding=\"5\">";
            review_list = review_list + "<tr>";
            review_list = review_list + "<td height=\"10\"></td>";
            review_list = review_list + "</tr>";

            review_list = review_list + "<tr>";
            review_list = review_list + "<td class=\"t12_grey\" align=\"center\">" + Application["Product_Review_Config_NoRecordTip"] + "</td>";
            review_list = review_list + "</tr>";
            review_list = review_list + "</table>";
        }
        Response.Write(review_list);
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
        PageInfo validpage = MyShopEvaluate.GetPageInfo(Query);
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
        string percent_str = "";
        percent_str = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr><td width=\"100\">";
        percent_str = percent_str + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr><td bgcolor=\"#FFFBD1\"><table width=\"" + width_td + "\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"0\" ><tr><td height=\"14\" bgcolor=\"#FEDD00\"></td></tr></table></td></tr></table>";
        percent_str = percent_str + "</td><td>&nbsp;" + width_td + "</td></tr></table>";
        return percent_str;
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
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
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
        PageInfo validpage = MyShopEvaluate.GetPageInfo(Query);
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

    //商品评论星显示
    public string Review_ShowStar(string style, int product_id, int Review_count, double review_average)
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
                show_str = show_str + "<img src=\"" + img_on + "\" border=\"0\">";
            }
            if ((review_average % 10) > 0)
            {
                show_str = show_str + "<img src=\"" + img_half + "\" border=\"0\">";
                for (j = i + 1; j <= Maxnum; j++)
                {
                    show_str = show_str + "<img src=\"" + img_off + "\" border=\"0\">";
                }
            }
            else
            {
                if (style != "review")
                {
                    for (j = i; j <= Maxnum; j++)
                    {
                        show_str = show_str + "<img src=\"" + img_off + "\" border=\"0\">";
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
                    show_str = show_str + "<img src=\"" + img_off + "\" border=\"0\">";
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
        IList<SupplierShopEvaluateInfo> reviews = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews == null)
            return 0;
        return reviews.Count;
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

    #region "购物咨询"

    public void Product_Shoppingask( int productid, int pagesize,int allflag)
    {

        Response.Write("<table border=\"0\" align=\"right\" style=\"\" cellspacing=\"0\" cellpadding=\"5\" width=\"100%\">"); ;
        Product_Ask_List(productid, pagesize, allflag);
        Response.Write("</table>");

    }

    public void Product_Ask_List(int Product_Id, int pagesize, int allflag)
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

        if (allflag != 1)
        {
            Response.Write("<tr><td>");
            Response.Write("            <table border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">");
            Response.Write("  <tr>");
            Response.Write("    <td align=\"left\" class=\"t12\">购买之前，如有问题，请向" + Application["site_name"] + "咨询。  <a href=\"ask_add.aspx?product_id=" + Product_Id + "\" class=\"a_t12_orange\">[发表咨询]</a></td>");
            Response.Write("<td align=\"right\">  <a href=\"/ask_add.aspx?product_id=" + Product_Id + "\" class=\"a_t12_blue\">浏览所有咨询信息>></a></td>");
            Response.Write("  </tr>");
            Response.Write("  </table>");
            Response.Write("</td></tr>");
        }


        QueryInfo Query = new QueryInfo();
        Query.PageSize = pagesize;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", "=", Product_Id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
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
                i = i + 1;
                Response.Write("<tr><td colspan=\"2\" ");
                if (i % 2 == 0)
                {
                    Response.Write("style=\"background-color:#F4F9FF;\">");
                }
                else
                {
                    Response.Write("style=\"background-color:#ffffff;\">");
                    //Response.Write(" align=\"left\">");
                }
                Response.Write("<table width=\"100%\" border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"3\">");
                if (entity.Ask_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Ask_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        Response.Write("<tr><td colspan=\"2\" style=\"word-break:break-all;\"><img src=\"/images/qq_v01.gif\" align=\"absmiddle\"> 会员：" + member.Member_NickName + "</td></tr>");
                    }
                    else
                    {
                        Response.Write("<tr><td colspan=\"2\" style=\"word-break:break-all;\"><img src=\"/images/qq_n01.gif\" align=\"absmiddle\"> 游客</td></tr>");

                    }
                }


                Response.Write("<tr><td colspan=\"2\" style=\"word-break:break-all;\"><img src=\"/images/icon_ask.gif\" align=\"absmiddle\"> 咨询内容：" + entity.Ask_Content + " [" + entity.Ask_Addtime + "]</td></tr>");
                Response.Write("<tr><td colspan=\"2\" style=\"word-break:break-all;\" class=\"t12_orange\"><img src=\"/images/icon_reply.gif\" align=\"absmiddle\"> 回复内容：" + entity.Ask_Reply + " 感谢您对我们的关注，祝您购物愉快！</td></tr>");
                Response.Write("</table>");
                Response.Write("</td></tr>");
            }
            if (allflag == 1)
            {
                Response.Write("<tr><td align=\"right\" style=\"padding-top:10px;\">");
                pub.Page(page.PageCount, page.CurrentPage, pageurl, page.PageSize, page.RecordCount);
                Response.Write("</td></tr>");
            }
        }
        else
        {
            Response.Write("<tr><td>");
            Response.Write("<table border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" width=\"98%\">");
            Response.Write("<tr><td align=\"center\" height=\"30\"> 暂无相关咨询!</td>");
            Response.Write("</tr>");
            Response.Write("  </table>");
            Response.Write("</td></tr>");
        }     
    }

    public void Product_Ask_Form(int product_id)
    {

        Response.Write("<div class=\"pro_ask_message\" id=\"product_message\">");
        Response.Write("   <div class=\"shop_ask_messageTit\">留言薄</div>");
        Response.Write("<form action=\"shop_askdo.aspx\" method=\"post\">");
        Response.Write("   <div class=\"shop_ask_messageBox\">");
        Response.Write("        <div class=\"shop_ask_messageBoxL\"><br>向商家提问：</div>");
        Response.Write("        <div class=\"shop_ask_messageBoxR\"><textarea rows=\"5\" cols=\"50\" name=\"ask_content\" style=\"display:inline;\"></textarea></div>");
        Response.Write("   </div>");
        Response.Write("   <div class=\"blank5\"></div>");

        Response.Write("   <div class=\"shop_ask_messageBox\">");
        Response.Write("        <div class=\"shop_ask_messageBoxL\">联系方式：</div>");
        Response.Write("        <div class=\"shop_ask_messageBoxR\"><input type=\"text\" name=\"ask_contact\" id=\"ask_contact\" size=\"30\" style=\"display:inline;\" maxlength=\"100\"></div>");
        Response.Write("   </div>");
        Response.Write("   <div class=\"blank5\"></div>");
        Response.Write("   <div class=\"shop_ask_messageBox\" >");
        Response.Write("        <div class=\"shop_ask_messageBoxL\">请输入验证码：</div>");
        Response.Write("        <div class=\"shop_ask_messageBoxR\"><input type=\"text\" name=\"ask_verify\" id=\"ask_verify\" size=\"10\" style=\"display:inline;\" maxlength=\"10\">&nbsp;&nbsp;<img src=\"/public/verifycode.aspx?timer='+Math.random();\" id=\"img_verifycode\" align=\"absmiddle\" style=\"display:inline;\">   <a href=\"javascript:void(0);\" onclick=\"javascript:MM_findObj('img_verifycode').src='/public/verifycode.aspx?timer='+Math.random();return false;\">刷新验证码</a></div>");
        Response.Write("   </div>");
        Response.Write("    <div class=\"blank5\"></div>");
        Response.Write("   <div class=\"shop_ask_messageBox\">");
        Response.Write("       <div class=\"shop_ask_messageBoxR2\"><input type=\"hidden\" name=\"action\" value=\"ask_add\"><input type=\"hidden\" name=\"ask_productid\" value=\"" + product_id + "\"><input style=\"display:inline;\" class=\"shop_ask_btn\" type=\"submit\"></div>");
        Response.Write("   </div>");
        Response.Write("</form>");

    }

    //购物咨询添加
    public void Shopping_Ask_Add()
    {
        string ask_content, ask_verify,ask_contact;
        int ask_type, product_id,ask_isincognito;
        ask_type = tools.CheckInt(Request.Form["ask_type"]);
        product_id = tools.CheckInt(Request.Form["ask_productid"]);
        ask_content = tools.CheckStr(Request.Form["ask_content"]);
        ask_verify = tools.CheckStr(Request.Form["ask_verify"]);
         ask_contact =  tools.CheckStr(Request.Form["ask_contact"]);
        ask_isincognito = tools.CheckInt(Request.Form["ask_isincognito"]);
        //if (Session["member_logined"].ToString() != "True")
        //{
        //    pub.Msg("error", "错误信息", "请您注册登录后再进行购买咨询", false, "{back}");
        //}


        if (ask_verify != Session["Trade_Verify"].ToString())
        {
            pub.Msg("error", "错误信息", "验证码错误", false, "{back}");
        }
        if (ask_content == "")
        {
            pub.Msg("error", "错误信息", "请输入咨询内容", false, "{back}");
        }

        ShoppingAskInfo entity = new ShoppingAskInfo();
        entity.Ask_ID = 0;
        entity.Ask_Type = ask_type;
        entity.Ask_Contact = ask_contact;
        entity.Ask_Content = ask_content;
        entity.Ask_Reply = "";
        entity.Ask_Addtime = DateTime.Now;
        entity.Ask_SupplierID = tools.NullInt(Session["shop_supplier_id"]);
        entity.Ask_MemberID = tools.NullInt(Session["member_id"]);
        entity.Ask_ProductID = product_id;
        entity.Ask_Isreply = 0;
        entity.Ask_IsCheck = 0;
        entity.Ask_Pleasurenum = 0;
        entity.Ask_Displeasure = 0;
        entity.Ask_Site = pub.GetCurrentSite();

        if (Myshopask.AddShoppingAsk(entity, pub.CreateUserPrivilege("7da52156-9a1e-46af-bad4-7611cef159e3")))
        {
            SupplierMessageAdd(entity.Ask_SupplierID, "您有一条店铺咨询信息", "咨询内容：" + ask_content);
            pub.Msg("positive", "成功", "您的咨询已经成功提交，我们会尽快处理回复！", true, "shop_ask.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "咨询提交失败，请稍后再试！", false, "{back}");
        }
    }

    #endregion


    //获取指定活动文章
    public SupplierShopArticleInfo GetSupplierShopArticleByID(int Article_ID)
    {
        SupplierShopArticleInfo entity = MyShopArticle.GetSupplierShopArticleByID(Article_ID);
        if (entity != null)
        {
            if (entity.Shop_Article_IsActive == 0)
            {
                entity = null;
            }
        }
        return entity;
    }

    //活动区列表
    public void Shop_Article_List()
    {
        int i = 0;
        i = 0;
        string pageurl = "Shop_Article_List.aspx?key=key";

        System.Data.SqlClient.SqlDataReader rs_ask_list = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Query.CurrentPage < 1)
        {
            Query.CurrentPage = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopArticleInfo.Shop_Article_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopArticleInfo.Shop_Article_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopArticleInfo.Shop_Article_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopArticleInfo.Shop_Article_ID", "DESC"));
        IList<SupplierShopArticleInfo> entitys = MyShopArticle.GetSupplierShopArticles(Query);
        PageInfo Page = MyShopArticle.GetPageInfo(Query);
        if (entitys != null)
        {
            Response.Write("<ul>");
            foreach (SupplierShopArticleInfo entity in entitys)
            {
                Response.Write("<li>");
                Response.Write("<div class=\"shop_active_listL\"><a href=\"Shop_Article_detail.aspx?article=" + entity.Shop_Article_ID + "\" style=\"color:#3366cc;\">" + entity.Shop_Article_Title + "</a></div>");
                Response.Write("<div class=\"shop_active_listR\">" + entity.Shop_Article_Addtime + "</div>");
                Response.Write("</li>");
            }
            Response.Write("</ul><div class=\"clear\"></div>");
        }
        else
        {
            Response.Write("<center>暂无活动信息</center>");
        }

        pub.Page(Page.PageCount, Page.CurrentPage, pageurl, Page.PageSize, Page.RecordCount);

    }

    /// <summary>
    /// 获取供应商店铺开通申请
    /// </summary>
    /// <param name="ID">供应商编号</param>
    public SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID)
    {
        return MyShopApply.GetSupplierShopApplyBySupplierID(ID);
    }

    //更新活动文章浏览数
    public void Shop_Article_Hits(SupplierShopArticleInfo entity)
    {
        entity.Shop_Article_Hits += 1;
        MyShopArticle.EditSupplierShopArticle(entity);
    }


    
    /// <summary>
    /// 计算店铺平均分
    /// </summary>
    public void shop_evaluate_info_AVG()
    {
        int Shop_product_Count = 0;
        int Shop_product_Sum = 0;
        int Shop_service_Count = 0;
        int Shop_service_Sum = 0;
        int Shop_delivery_Count = 0;
        int Shop_delivery_Sum = 0;
        double Shop_product_Avg = 0;
        double Shop_service_Avg = 0;
        double Shop_delivery_Avg = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
                Shop_product_Sum = Shop_product_Sum + entity.Shop_Evaluate_Product;
                Shop_service_Count = Shop_service_Count + 1;
                Shop_service_Sum = Shop_service_Sum + entity.Shop_Evaluate_Service;
                Shop_delivery_Count = Shop_delivery_Count + 1;
                Shop_delivery_Sum = Shop_delivery_Sum + entity.Shop_Evaluate_Delivery;
            }
        }
        if (Shop_product_Count > 0 && Shop_product_Sum > 0)
        {
            Shop_product_Avg = Math.Round(tools.CheckFloat((Shop_product_Sum / Shop_product_Count).ToString()), 1);
        }
        else
        {
            Shop_product_Avg = 0;
        }
        if (Shop_service_Count > 0 && Shop_service_Sum > 0)
        {
            Shop_service_Avg = Math.Round(tools.CheckFloat((Shop_service_Sum / Shop_service_Count).ToString()), 1);
        }
        else
        {
            Shop_service_Avg = 0;
        }
        if (Shop_delivery_Count > 0 && Shop_delivery_Sum > 0)
        {
            Shop_delivery_Avg = Math.Round(tools.CheckFloat((Shop_delivery_Sum / Shop_delivery_Count).ToString()), 1);
        }
        else
        {
            Shop_delivery_Avg = 0;
        }


        double ShopAVG = Math.Round((Shop_product_Avg + Shop_service_Avg + Shop_delivery_Avg) / 3, 1);

        for (int i = 1; i < ShopAVG; i++)
        {
            Response.Write("<img src=\"/images/x2.jpg\" />");
        }
    }
   
}
