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
using System.Linq;

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
    private ISQLHelper DBHelper;
    private Member memberclass;
    private AD adclass;
    private Orders orders;
    private IPromotionFavor MyFavor;
    private IPromotionCouponRule MyCouponRule;
    private Orders myOrders = new Orders();
    private ISupplierShop MyShop;
    private ISupplierShopEvaluate MyEvaluate;
    private ISupplierMessage MyMessage;
    private PageURL pageurl;
    private IProductWholeSalePrice MySalePrice;


    public Product()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

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
        pub = new Public_Class();
        memberclass = new Member();
        orders = new Orders();
        adclass = new AD();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyCouponRule = PromotionCouponRuleFactory.CreatePromotionFavorCoupon();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        MyMessage = SupplierMessageFactory.CreateSupplierMessage();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        MySalePrice = ProductWholeSalePriceFactory.CreateProductWholeSalePrice();
    }


    public void Home_Product_List(int show_num)
    {

        StringBuilder strHTML = new StringBuilder();
        string Tag_ProductID = "0";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = show_num;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductTagInfo.Product_Tag_SupplierID", "=", tools.NullInt(Session["shop_supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_Sort", "Asc"));
        IList<ProductTagInfo> entitys = MyTag.GetProductTags(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (entitys != null)
        {
            int i1 = 0;

            foreach (ProductTagInfo Taginfo in entitys)
            {
                i1++;
                if (i1 == 1)
                {
                    strHTML.Append("	   <div class=\"list_06\">               ");
                }
                else
                {
                    strHTML.Append("	   <div class=\"list_06\" style=\"margin-top:14px;\">               ");
                }

                strHTML.Append("	        <ul style=\"width:975px;\">       ");


                Tag_ProductID = Myproduct.GetTagProductID(Taginfo.Product_Tag_ID.ToString());

                strHTML.Append("<h2 style=\" height:40px; font-size:15px; font-weight:bold; border:1px solid #dddddd; padding:0 0 0 25px; line-height:40px;\">" + Taginfo.Product_Tag_Name + "</h2>");


                QueryInfo Query1 = new QueryInfo();
                Query1.PageSize = 0;
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
                    string targetURL;
                    int i2 = 0;
                    int j = 0;
                    foreach (ProductInfo entity in productinfos)
                    {
                        targetURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                        j++;
                        if (j%4==0)
                        {
                            strHTML.Append("<li style=\"margin-right:0px;\">");
                        }
                        else
                        {
                            strHTML.Append("<li >");
                        } 
                      


                        strHTML.Append("	<div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div>");
                        strHTML.Append("	<div class=\"p_box\">");
                        strHTML.Append("		<div class=\"p_box_info01\">");
                        strHTML.Append("		<div class=\"p_box_info02\"><a href=\"" + targetURL + "\" target=\"_blank\" title=\"" + entity.Product_Name + "\">" + tools.CutStr(entity.Product_Name, 50) + "</a></div>");
                        strHTML.Append("			<b><i>成交 " + entity.Product_SaleAmount + " 件</i>售价：");


                        strHTML.Append("<strong>" + pub.FormatCurrency(pub.Get_Member_Price(entity.Product_ID, entity.Product_Price)) + "</strong>");
                        strHTML.Append("</b>");


                        strHTML.Append("		</div>");


                        strHTML.Append("		<div class=\"p_box_info04\"><a href=\"" + targetURL + "\" target=\"_blank\" class=\"a16\">加入购物车</a><a href=\"javascript:;\" onclick=\"favorites_add_ajax(" + entity.Product_ID + ",'product');\" class=\"a17\">加入收藏</a></div>");
                        strHTML.Append("	</div>");
                        strHTML.Append("</li>");



                    }
                }
                strHTML.Append("   </ul>");
                strHTML.Append("     <div class=\"clear\"></div>");
                strHTML.Append(" </div>");
            }
        }





        Response.Write(strHTML.ToString());
    }

    //获取商家类型
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

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", "CN"));
        if (cate_id > 0)
        {
            string subCates = Get_All_SubCate(cate_id);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_ID", "in", "select distinct product_brandid from product_basic where Product_IsInsale=1 and  Product_IsAudit=1 and (product_cateid in (" + subCates + ") or product_id in (SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")))"));
        }
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "ASC"));
        IList<BrandInfo> entitys = MyBrand.GetBrands(Query, pub.CreateUserPrivilege("9b17d437-fb2a-4caa-821e-daf13d9efae4"));
        brand_list += "<div class=\"brand\">";

        int i = 0;
        if (entitys != null)
        {
            brand_list += "<span>更多品牌>></span>";
            foreach (BrandInfo entity in entitys)
            {
                i++;
                brand_list += "<a href=\"/product/category.aspx?cate_id=" + cate_id + "&brand_id=" + entity.Brand_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Brand_Img, "fullpath") + "\"  onload=\"javascript:AutosizeImage(this,85,50);\"/></a>";
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
            //获取指定标签编号对应所有商品编号
            string Tag_ProductID = Myproduct.GetTagProductID(Taginfo.Product_Tag_ID.ToString());
            if (Tag_ProductID == "")
            {
                return null;
            }
            else
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = Show_Num;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", Tag_ProductID));
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

    public string Home_Left_Cate()
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

        int ifirst = 1, isecond = 1;

        if (FirstList != null)
        {
            foreach (CategoryInfo entity in FirstList)
            {
                strHTML.Append("		<li>");
                strHTML.Append("			<dl class=\"a3\">");
                strHTML.Append("				<dt><a href=\"" + Application["Site_URL"] + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity.Cate_ID.ToString(), "", "", "", "")) + "\">" + entity.Cate_Name + "</a></dt>");
                strHTML.Append("				<dd>");
                strHTML.Append("					<ul class=\"zkw_lst4\">");

                SecondList = entityList.Where(P => P.Cate_ParentID == entity.Cate_ID).ToList();
                if (SecondList != null)
                {

                    isecond = 1;
                    foreach (CategoryInfo second in SecondList)
                    {
                        strHTML.Append("<li><a href=\"" + Application["Site_URL"] + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(second.Cate_ID.ToString(), "", "", "", "")) + "\">" + second.Cate_Name + "</a></li>");

                        if (isecond >= 4) break;
                        isecond++;
                    }
                }

                strHTML.Append("					</ul>");
                strHTML.Append("					<div class=\"clear\"></div>");
                strHTML.Append("				</dd>");
                strHTML.Append("			</dl>");
                strHTML.Append("			<div class=\"boxshow\" id=\"xs01\">");
                strHTML.Append("				<div class=\"boxshow_left\">");

                if (SecondList != null)
                {
                    foreach (CategoryInfo second in SecondList)
                    {
                        strHTML.Append("					<dl class=\"dst5\">");
                        strHTML.Append("						<dt><a href=\"" + Application["Site_URL"] + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(second.Cate_ID.ToString(), "", "", "", "")) + "\">" + second.Cate_Name + "</a></dt>");
                        strHTML.Append("						<dd>");
                        ThridList = entityList.Where(P => P.Cate_ParentID == second.Cate_ID).ToList();

                        if (ThridList != null)
                        {
                            foreach (CategoryInfo thrid in ThridList)
                                strHTML.Append("<a href=\"" + Application["Site_URL"] + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(thrid.Cate_ID.ToString(), "", "", "", "")) + "\">" + thrid.Cate_Name + "</a>");
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
                strHTML.Append("					<div class=\"b_r_box\">");
                strHTML.Append("						<h3 class=\"title04\">促销信息</h3>");
                strHTML.Append("						<div class=\"fox\">");
                strHTML.Append("							<ul class=\"lst6\">");
                strHTML.Append(adclass.AD_Show("POPUP_MENU_SECOND", entity.Cate_ID.ToString(), "keyword7", 1));
                strHTML.Append("							</ul>");
                strHTML.Append("						</div>");
                strHTML.Append("					</div>");
                strHTML.Append("				</div>");
                strHTML.Append("				<div class=\"clear\"></div>");
                strHTML.Append("			</div>");
                strHTML.Append("		</li>");

                if (ifirst >= 5) break;
                ifirst++;

            }

        }
        entityList = null;
        FirstList = null;

        strHTML.Append("	</ul>");
        strHTML.Append("</div>");

        return strHTML.ToString();
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
    /// 获取商品关联视频
    /// </summary>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    public string[,] GetProductVideo(int ProductID)
    {
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();
        DataTable table = DBHelper.Query("SELECT Product_Video_File, Product_Video_Thumbnail FROM Product_Video WHERE Product_Video_ProductID = " + ProductID);

        int ICount = 0;
        string[,] DataSource = new string[table.Rows.Count, 2];
        foreach (DataRow row in table.Rows)
        {
            DataSource[ICount, 0] = tools.NullStr(row["Product_Video_File"]);
            DataSource[ICount, 1] = tools.NullStr(row["Product_Video_Thumbnail"]);
            ICount++;
        }

        return DataSource;
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
        Response.Write(" <div class=\"b24_main_left\">");
        Response.Write("<div class=\"b24_m_l_info01\" id=\"gg01\">");
        Response.Write("<dl class=\"dst14\">");
        Response.Write("<dt><div id=\"image_wrap\" style=\"height:300px; width:300px;\">");
        Response.Write("<a href=\"" + Product_Img + "\" onclick=\"javascript:window.open('/images.aspx?product_id=" + Product_ID + "&img='+MM_findObj('product_img').src);\" class=\"jqzoom\" id=\"demo1\"  name=\"demo1\" title=\"" + Product_Name + "\">");
        Response.Write("<img id=\"product_img\" src=\"" + S_Product_Img + "\" width=\"300\" height=\"300\" border=\"0\" alt=\"" + Product_Name + "\"/></a>");
        Response.Write("</div></dt>");

        Response.Write("<dd>");
        Response.Write("<div style=\"margin:0 auto; width:312px; height:55px; margin-top:10px;\">");
        Response.Write("<div class=\"scrollable\" id=\"scrollable\">");
        Response.Write("<div class=\"items\"><ul>");

        if (Product_Img != "/images/detail_no_pic.gif")
        {
            Response.Write("<li class=\"on\" id=\"product_img_s_1\" onclick=\"javascript:MM_swapImage('product_img','','" + S_Product_Img + "',1);switchimgborder('product_img_s_1');\" onmouseover=\"javascript:if(this.className=='bg'){this.className='on bg';}\" onmouseout=\"javascript:if(this.className!='on1 bg'){this.className='bg';}\">");
            Response.Write("<img src=\"" + S_Product_Img + "\" onload=\"javascript:AutosizeImage(this,67,67);MM_preloadImages('" + S_Product_Img + "');\" ></li>");
        }
        if (Product_Img_Ext_1 != "/images/detail_no_pic.gif")
        {
            Response.Write("<li id=\"product_img_s_2\" onclick=\"javascript:MM_swapImage('product_img','','" + S_Product_Img_Ext_1 + "',1);switchimgborder('product_img_s_2');\" onmouseover=\"javascript:if(this.className=='bg'){this.className='on bg';}\" onmouseout=\"javascript:if(this.className!='on1 bg'){this.className='bg';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_1 + "\" onload=\"javascript:AutosizeImage(this,67,67);MM_preloadImages('" + S_Product_Img_Ext_1 + "');\" ></li>");
        }
        if (Product_Img_Ext_2 != "/images/detail_no_pic.gif")
        {
            Response.Write("<li id=\"product_img_s_3\" onclick=\"javascript:MM_swapImage('product_img','','" + S_Product_Img_Ext_2 + "',1);switchimgborder('product_img_s_3');\" onmouseover=\"javascript:if(this.className=='bg'){this.className='on bg';}\" onmouseout=\"javascript:if(this.className!='on1 bg'){this.className='bg';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_2 + "\" onload=\"javascript:AutosizeImage(this,67,67);MM_preloadImages('" + S_Product_Img_Ext_2 + "');\" ></li>");
        }
        if (Product_Img_Ext_3 != "/images/detail_no_pic.gif")
        {
            Response.Write("<li id=\"product_img_s_4\" onclick=\"javascript:MM_swapImage('product_img','','" + S_Product_Img_Ext_3 + "',1);switchimgborder('product_img_s_4');\" onmouseover=\"javascript:if(this.className=='bg'){this.className='on bg';}\" onmouseout=\"javascript:if(this.className!='on1 bg'){this.className='bg';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_3 + "\" onload=\"javascript:AutosizeImage(this,67,67);MM_preloadImages('" + S_Product_Img_Ext_3 + "');\" ></li>");
        }
        if (Product_Img_Ext_4 != "/images/detail_no_pic.gif")
        {
            Response.Write("<li id=\"product_img_s_5\" onclick=\"javascript:MM_swapImage('product_img','','" + S_Product_Img_Ext_4 + "',1);switchimgborder('product_img_s_5');\" onmouseover=\"javascript:if(this.className=='bg'){this.className='on bg';}\" onmouseout=\"javascript:if(this.className!='on1 bg'){this.className='bg';}\">");
            Response.Write("<img src=\"" + S_Product_Img_Ext_4 + "\" onload=\"javascript:AutosizeImage(this,67,67);MM_preloadImages('" + S_Product_Img_Ext_4 + "');\" ></li>");
        }

        Response.Write("</ul><div class=\"clear\"></div>");
        Response.Write("</div>");
        Response.Write("</div>");
        Response.Write("</div>");
        Response.Write("</dd>");
        Response.Write("</dl>");
        Response.Write("</div>");
        Response.Write("<div class=\"b24_m_l_info02\">");
        Response.Write("<ul class=\"lst31\">");
        Response.Write("<li><strong>好友分享:</strong><div class=\"li_box02\">");
        Response.Write("<div class=\"jiathis_style\">");
        Response.Write("<a href=\"http://www.jiathis.com/share\" class=\"jiathis jiathis_txt jtico jtico_jiathis\" target=\"_blank\"></a>");
        Response.Write("<a class=\"jiathis_button_qzone\"></a>");
        Response.Write("<a class=\"jiathis_button_tsina\"></a>");
        Response.Write("<a class=\"jiathis_button_tqq\"></a>");
        Response.Write("<a class=\"jiathis_button_renren\"></a></a>");

        Response.Write("<a class=\"jiathis_counter_style\"></a>");
        Response.Write("</div>");
        Response.Write("<script type=\"text/javascript\" src=\"http://v3.jiathis.com/code/jia.js?uid=1351041314755898\" charset=\"utf-8\"></script>");
        Response.Write("</div></li>");
        Response.Write("</ul>");
        Response.Write("<div class=\"clear\"></div>");
        Response.Write("</div>");
        Response.Write("</div>");


        Response.Write("<div class=\"yz_blk15_main_right\">");
        Response.Write("<div class=\"b15_m_r_info01\">");
        Response.Write("<ul class=\"yz_lst23\">");
        Response.Write("<li><h2 class=\"yz_title14\">" + productinfo.Product_Name + "</h2></li>");
        Response.Write("<li>商品编号：<span class=\"sp02\">" + productinfo.Product_Code + "</span></li>");
        Response.Write("<li>商品价格：<span class=\"sp03\"><em>" + pub.FormatCurrency(pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price)) + "</em></span><span class=\"sp04\">市场价:￥" + productinfo.Product_MKTPrice + "</span></li>");
        //Response.Write("<li>商品品牌：<span class=\"sp05\">" + Brand_Name + "</span></li>");
        Response.Write("<li>商品厂商：<span class=\"sp05\">" + productinfo.Product_Maker + "</span></li>");
        Response.Write("<li>库&nbsp;&nbsp;存：");
        if (productinfo.Product_IsNoStock == 1 || productinfo.Product_UsableAmount > 0)
        {
            Response.Write("现货");
        }
        else
        {
            Response.Write("无货");
        }

        Response.Write("<li class=\"li19\">商品评分：");

        for (int i = 1; i <= (int)productinfo.Product_Review_Average; i++)
        {
            Response.Write("<img src=\"/images/icon_star_on_small.gif\" />");
        }
        for (int i = 1; i <= 5 - (int)productinfo.Product_Review_Average; i++)
        {
            Response.Write("<img src=\"/images/icon_star_off_small.gif\" />");
        }

        Response.Write("（共4条评价）</li>");
        Response.Write("</ul>");
        Response.Write("</div>");
        PromotionLimitInfo limitproduct = pub.GetPromotionLimitByProductID(productinfo.Product_ID);
        int product_islimit = 0;
        if (limitproduct != null)
        {
            product_islimit = 1;
            Response.Write("<li><div class=\"divlimit\">");
            Response.Write("    <div>限时价格：<span class=\"t12_red\"><b>" + pub.FormatCurrency(limitproduct.Promotion_Limit_Price) + "</b></span></div>");
            Response.Write("    <div id=\"limit_tip\"></div>");
            Response.Write("</div>");
            TimeSpan span1 = limitproduct.Promotion_Limit_Endtime - DateTime.Now;
            int timespan = (span1.Days * (24 * 3600)) + (span1.Hours * 3600) + (span1.Minutes * 60) + (span1.Seconds);
            Response.Write("<script>updatetime(" + timespan + ",'limit_tip')</script>");
            Response.Write("</li>");
        }

        if (GetSupplierGrade(productinfo.Product_SupplierID) > 2)
        {
            Response.Write("<div class=\"b15_m_r_info02\">");
            Response.Write("<div class=\"b15_m_r_main02\">");

            Response.Write(Product_BuyExtend_Show(Product_ID, productinfo.Product_TypeID, productinfo.Product_GroupCode));

            Response.Write("<div class=\"b15_box03\">");
            Response.Write("<strong>我要买:</strong><a href=\"javascript:void(0);\" onclick=\"setAmount.reduce('#buy_amount');CountCost();\">-</a>");
            Response.Write("<input type=\"text\" id=\"buy_amount\" onkeyup=\"setAmount.modify('#buy_amount'); CountCost();\"  class=\"input09\" value=\"1\"/>");
            Response.Write("<a onclick=\"setAmount.add('#buy_amount');CountCost();\" href=\"javascript:void(0);\"  style=\"margin-right:5px;\">+</a>");
            Response.Write("</div>");
            Response.Write("<div class=\"b15_box03\">");
            Response.Write("<strong>小 计:</strong><span class=\"sp07\">￥<em id=\"P_TotalPrice\">" + pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price) + "</em></span>");
            Response.Write("<input type=\"hidden\" id=\"P_YourPrice\" value=\"" + pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price) + "\"/>");
            Response.Write("</div>");
            Response.Write("<div class=\"b15_box04\">");
            Response.Write("<ul class=\"yz_lst25\">");
            if (productinfo.Product_UsableAmount > 0 || productinfo.Product_IsNoStock == 1)
            {
                Response.Write("<li class=\"buttom02\"><a href=\"" + Application["Site_URL"] + "/cart/cart_do.aspx?action=add&product_id=" + productinfo.Product_ID + "\" onclick=\"javascript:return AddCartExt(this);\">加入购物车</a></li>");
            }
            else
            {
                Response.Write("<li class=\"buttom02\"><a href=\"javascript:void(0);\" style=\"color:#cccccc;\">暂时无货</a></li>");
            }
            Response.Write("<li class=\"buttom03\"><a href=\"" + Application["Site_URL"] + "/member/fav_do.aspx?action=goods&id=" + productinfo.Product_ID + "\">加入收藏</a></li>");
            Response.Write("</ul>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("</div>");

            Response.Write("</div>");

            Response.Write("</div>");
        }
        Response.Write("</div>");
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
                if (extend.ProductType_Extend_IsActive == 1 && extend.ProductType_Extend_Options == 2 && extend.ProductType_Extend_Gather > 0)
                {
                    //图片形式选择
                    if (extend.ProductType_Extend_DisplayForm == 2)
                    {
                        Extend_list = Product_BuyExtend_Value(Product_ID, extend.ProductType_Extend_ID, Product_Arry, productextend_arry, 2);
                        if (Extend_list.Length > 0)
                        {
                            Extend_Str += "<div class=\"buy_option_div\">";
                            Extend_Str += "<span class=\"imgtit\">" + extend.ProductType_Extend_Name + "：</span><div class=\"buy_img\">";
                            Extend_Str += Extend_list;
                            Extend_Str += "</div>";
                            Extend_Str += "</div>";
                        }
                    }
                    else
                    {
                        //文字形式选择
                        Extend_list = Product_BuyExtend_Value(Product_ID, extend.ProductType_Extend_ID, Product_Arry, productextend_arry, 1);
                        if (Extend_list.Length > 0)
                        {
                            Extend_Str += "<div class=\"buy_option_div\">";
                            Extend_Str += "<span class=\"txttit\">" + extend.ProductType_Extend_Name + "：</span><div class=\"buy_txt\">";
                            Extend_Str += Extend_list;
                            Extend_Str += "</div>";
                            Extend_Str += "</div>";
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
                        Extend_Str += "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 1) + ">" + Pre_Extend.Extend_Value + "</a>";
                    }
                    else
                    {
                        Extend_Str += "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, Pre_Extend.Product_ID.ToString()) + "\" " + Get_Extend_Status(Product_ID, Pre_Extend.Extent_ID, Pre_Extend.Extend_Value, 2) + "><img src=\"" + pub.FormatImgURL(Pre_Extend.Extend_Img, "fullpath") + "\"></a>";
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
                    status_str = " class=\"opt_on\"";
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
                                product_list = product_list + "<a href=\"" + Application["Site_URL"] + "/cart/cart_do.aspx?action=add&package_id=" + entity.Package_ID + "\" target=\"_blank\"><img border=\"none\" class=\"buy\" src=\"/images/butbuy.jpg\" alt=\"购买\" width=\"64\" height=\"24\" /></a>";
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


    //获取商品图片信息
    public string[] GetProductImg(int product_id)
    {
        string ipaths = Myproduct.GetProductImg(product_id);
        string[] ipathArr = { "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif" };
        ipathArr = ipaths.Split(',');
        return ipathArr;
    }

    //店铺销量排行
    public string Shop_LeftSale_Product(int Show_Num)
    {
        int i = 0;
        string product_list = "";
        string targetURL = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "in", tools.NullStr(Session["shop_supplier_id"])));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_SaleAmount", "Desc"));
        IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (Products != null)
        {
            foreach (ProductInfo entity in Products)
            {
                targetURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());
                i = i + 1;

                if (i < 4)
                {
                    product_list += "<li><i class=\"bg\">" + i + "</i><a href=\"" + targetURL + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 30) + "</a></li>";
                }
                else
                {
                    product_list += "<li><i class=\"bg1\" >" + i + "</i><a href=\"/detail.aspx?product_id=" + entity.Product_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 30) + "</a></li>";
                }

            }
        }
        return product_list;

    }

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

                strHTML.Append("<dl class=\"dst19\">");
                strHTML.Append("	<dt>网友：<span>" + AskMember + "</span> 提问时间:<span>" + entity.Ask_Addtime + "</span></dt>");
                strHTML.Append("	<dd><b><strong class=\"str5\">咨询内容：</strong><span>" + entity.Ask_Content + "</span></b><div class=\"clear\"></div>");
                strHTML.Append("		<p><strong class=\"str6\">易种回复：</strong><span>" + entity.Ask_Reply + "，感谢您对我们的关注，祝您购物愉快！</span></p>");
                strHTML.Append("		<div class=\"clear\"></div>");
                strHTML.Append("	</dd>");
                strHTML.Append("</dl>");

            }

            Response.Write(strHTML.ToString());

            if (allflag == 1)
            {
                Response.Write("<table width=\"100%\"><tr><td align=\"right\"><div class=\"yz_b14_info_right yz_b14_info_right_2\" style=\"float:right;\">");
                pub.Page(page.PageCount, page.CurrentPage, pageurl, page.PageSize, page.RecordCount);
                Response.Write("</div></td></tr></table>");
            }
            else
            {
                Response.Write("<div class=\"zkw_info\">共 <span>" + recordcount + "</span> 条   <a href=\"ask_add.aspx?product_id=" + Product_Id + "\">浏览所有咨询信息>></a></div>");
            }
        }
        else
        {
            Response.Write("<table border=\"0\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" width=\"98%\">");
            Response.Write("<tr><td align=\"center\" height=\"30\"> 暂无相关咨询! &nbsp;<a href=\"shop_ask.aspx?product_id=" + Product_Id + "\" style=\"color:#006699;\">发表咨询>></a></td>");
            Response.Write("</tr>");
            Response.Write("  </table>");
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
                SupplierMessageAdd(entity.Ask_SupplierID, "您有一条商品咨询信息", "咨询商品：" + productinfo.Product_Name + "，咨询内容：" + ask_content);
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
        string td_bgcolor = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.NullInt(Application["Product_Review_Config_ProductCount"]);
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews != null)
        {
            review_list = review_list + "<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"5\">";


            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                member_nickname = "游客";
                icount = icount + 1;
                if (td_bgcolor == "#ffffff")
                {
                    td_bgcolor = "#f6f8f9";
                }
                else
                {
                    td_bgcolor = "#ffffff";
                }
                if (entity.Shop_Evaluate_MemberID > 0)
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
                review_list = review_list + "    <td width=\"60\" height=\"30\" align=\"center\" valign=\"middle\" class=\"step_num_off\">" + icount + "</td>";
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
        return review_list;
    }

    //商品评论信息概况
    public void Product_Reviews_Info(int product_id, int Product_Review_Count, double Product_Review_Average)
    {
        Product_Review_Average = tools.CheckFloat(Product_Review_Average.ToString());
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"b27_b_01\">");
        strHTML.Append("	<dl class=\"dst17\">");
        strHTML.Append("		<dt>" + Product_Review(product_id, "4,5") + "%</dt>");
        strHTML.Append("		<dd>好评度</dd>");
        strHTML.Append("	</dl>");
        strHTML.Append("</div>");
        strHTML.Append("<div class=\"b27_b_02\">");
        strHTML.Append("	<ul class=\"lst34\">");
        strHTML.Append("		<li>好评<span>" + Product_Review_Percentage(product_id, "5,4") + "</li>");
        strHTML.Append("		<li>中评<span>" + Product_Review_Percentage(product_id, "3") + "</li>");
        strHTML.Append("		<li>差评<span>" + Product_Review_Percentage(product_id, "1,2") + "</li>");
        strHTML.Append("	</ul>");
        strHTML.Append("</div>");

        string topMember = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", product_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1")); ;
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (reviews != null)
        {
            MemberInfo MemEntity;
            foreach (SupplierShopEvaluateInfo e in reviews)
            {
                MemEntity = MyMem.GetMemberByID(e.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (MemEntity != null)
                    topMember += "<span>" + MemEntity.Member_NickName + "</span>";
            }
        }

        strHTML.Append("<div class=\"b27_b_03\">");
        strHTML.Append("	<p>前5位评价用户:" + topMember + "</p>");
        strHTML.Append("	<p><b><strong>发表评价即可获得积分！</strong>(<a href=\"/help/\" target=\"_blank\">详见积分规则</a>)；</b></p>");
        strHTML.Append("</div>");
        strHTML.Append("<div class=\"b27_b_04\">");
        strHTML.Append("	<ul class=\"lst35\">");
        strHTML.Append("		<li>我购买过此商品</li>");
        strHTML.Append("		<li class=\"li21\"><a href=\"/member/order_all.aspx\">我要评论</a></li>");
        strHTML.Append("	</ul>");
        strHTML.Append("</div>");

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
        return "<i><em style=\"width:" + width_td + ";\"></em></i><span>" + width_td + "</span>";

    }

    //商品评论表单
    public void Product_Review_Add_Form(string orders_sn)
    {
        IList<OrdersGoodsInfo> ordersgoodsinfos = orders.GetOrdersGoodsInfoBySN(orders_sn);
        int i = 0;
        if (ordersgoodsinfos != null)
        {
            foreach (OrdersGoodsInfo ordersgoodsinfo in ordersgoodsinfos)
            {
                Response.Write("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\" style=\"margin-top:10px;\">");
                i++;
                Response.Write("<tr><td width=\"25%\" valign=\"top\">");

                Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"80\" align=\"center\">");
                Response.Write("<img src=\"" + pub.FormatImgURL(ordersgoodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\" alt=\"" + ordersgoodsinfo.Orders_Goods_Product_Name + "\" title=\"" + ordersgoodsinfo.Orders_Goods_Product_Name + "\" width=\"60\" height=\"60\" onload=\"javascript:AutosizeImage(this,60,60);\"/>");
                Response.Write("</td><td style=\"line-height:30px;\">" + ordersgoodsinfo.Orders_Goods_Product_Name + "</td></tr>");
                Response.Write("</table>");
                Response.Write("</td>");
                Response.Write("<td width=\"75%\">");
                Response.Write("<table border=\"0\" cellpadding=\"5\" cellspacing=\"0\"><tr><td>评分</td><td>");
                Response.Write("<input type=\"radio\" name=\"review_star" + i + "\" value=\"5\" checked>很好&nbsp;" + Review_ShowStar("review", 0, 0, 5) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"4\">好&nbsp;" + Review_ShowStar("review", 0, 0, 4) + "&nbsp;<input name=\"review_star" + i + "\" type=\"radio\" value=\"3\">一般&nbsp;" + Review_ShowStar("review", 0, 0, 3) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"2\">较差&nbsp;" + Review_ShowStar("review", 0, 0, 2) + "&nbsp;<input type=\"radio\" name=\"review_star" + i + "\" value=\"1\">很差&nbsp;" + Review_ShowStar("review", 0, 0, 1));
                Response.Write("</td></tr>");
                Response.Write("<tr><td>评论内容</td><td>");
                Response.Write("<textarea name=\"review_content" + i + "\" cols=\"45\" rows=\"5\"></textarea>");
                Response.Write("</td></tr></table>");
                Response.Write("<input type=\"hidden\" name=\"review_productid" + i + "\" id=\"review_productid\" value=\"" + ordersgoodsinfo.Orders_Goods_Product_ID + "\"></td><tr>");

                Response.Write("</table>");
                Recent_View_Add(ordersgoodsinfo.Orders_Goods_Product_ID);
            }
            Response.Write("<input type=\"hidden\" name=\"amount\" value=\"" + i + "\"");
        }


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
        int icount = 0;
        string member_nickname = "游客";
        string td_bgcolor = "";
        string listtype = tools.NullStr(Request.QueryString["listtype"]);
        int page = tools.CheckInt(tools.NullStr(Request.QueryString["page"]));
        string page_url = "?listtype=" + listtype + "&product_id=" + Product_ID + "&star=" + star;
        if (page < 1)
        {
            page = 1;
        }

        QueryInfo Query = new QueryInfo();
        if (Application["Product_Review_Config_ListCount"] == null)
        {
            Query.PageSize = 10;
        }
        else
        {
            Query.PageSize = tools.CheckInt(Application["Product_Review_Config_ListCount"].ToString());
        }

        StringBuilder strHTML = new StringBuilder();
        Query.CurrentPage = page;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Product", "in", star));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> reviews = MyEvaluate.GetSupplierShopEvaluates(Query);
        PageInfo review_page = MyEvaluate.GetPageInfo(Query);
        if (reviews != null)
        {
            foreach (SupplierShopEvaluateInfo entity in reviews)
            {
                member_nickname = "游客";
                if (entity.Shop_Evaluate_MemberID > 0)
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Shop_Evaluate_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                    if (member != null)
                    {
                        member_nickname = member.Member_NickName;
                    }
                }

                strHTML.Append("<dl class=\"dst18\">");
                strHTML.Append("	<dt><img src=\"/images/pic.jpg\" />" + member_nickname + "</dt>");
                strHTML.Append("	<dd>");
                strHTML.Append("		<p><strong>" + entity.Shop_Evaluate_Addtime.ToString("yyyy-MM-dd") + "</strong>网友评分:<img src=\"/images/2_" + (6 - entity.Shop_Evaluate_Product) + ".png\" /></p>");
                strHTML.Append("		<p><span>评论内容:" + entity.Shop_Evaluate_Note + "</span></p>");
                //strHTML.Append("		<p><span class=\"sp4\">此评价对我:</span><a href=\"#\" class=\"a10\">有用(<i>0</i>)</a><a href=\"#\" class=\"a11\">没用(<i>0</i>)</a></p>");
                strHTML.Append("	</dd>");
                strHTML.Append("	<div class=\"clear\"></div>");
                strHTML.Append("</dl>");
            }

            Response.Write(strHTML.ToString() + "<div class=\"pagedisplay\">");
            pub.Page(review_page.PageCount, review_page.CurrentPage, page_url, review_page.PageSize, review_page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            review_list = review_list + "<table width=\"720\" border=\"0\" cellspacing=\"1\" align=\"center\" cellpadding=\"5\">";
            review_list = review_list + "<tr><td height=\"10\"></td></tr>";
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
            int ICount = 1;
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

                if (ICount > 2)
                    strHTML.Append("<dl class=\"zkw_dst6 bg0\">");
                else
                    strHTML.Append("<dl class=\"zkw_dst6\">");

                strHTML.Append("	<dt><a href=\"" + ProURL + "\" target=\"_blank\" title=\"" + ProName + "\" ><img src=\"" + ProImg + "\" /></a></dt>");
                strHTML.Append("	<dd>");
                strHTML.Append("		<b><a href=\"" + ProURL + "\" target=\"_blank\">" + ProName + "</a></b>");
                strHTML.Append("		<p>" + entity.Shop_Evaluate_Note + "</p>");
                strHTML.Append("	</dd>");
                strHTML.Append("	<div class=\"clear\"></div>");
                strHTML.Append("</dl>");

                ICount++;
            }
        }

        Response.Write(strHTML.ToString());
    }

    #endregion
}
