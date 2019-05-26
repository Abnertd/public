using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.CMS;


/// <summary>
///Product 的摘要说明
/// </summary>
public class Product
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProduct MyBLL;
    private ICategory MyCBLL;
    private IProductType MyTBLL;
    private IProductTag MyPTBLL;
    private IBrand MyBBLL;
    private IProductPrice MyPrice;
    private IMemberGrade MyGrade;
    private ISupplierGrade MySupplierGrade;
    private IProductHistoryPrice MyHistory;
    private IStockoutBooking Mystockout;
    private IProductAuditReason MyReason;
    private ISupplier MySupplier;
    private IProduct_Article_Label MyLabel;
    private IProduct_Label MyPro_Label;

    public Product()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ProductFactory.CreateProduct();
        MyCBLL = CategoryFactory.CreateCategory();
        MyTBLL = ProductTypeFactory.CreateProductType();
        MyPTBLL = ProductTagFactory.CreateProductTag();
        MyBBLL = BrandFactory.CreateBrand();
        MyPrice = ProductPriceFactory.CreateProductPrice();
        MyGrade = MemberGradeFactory.CreateMemberGrade();
        MySupplierGrade = SupplierGradeFactory.CreateSupplierGrade();
        MyHistory = ProductHistoryPriceFactory.CreateProductHistoryPrice();
        Mystockout = StockoutBookingFactory.CreateStockoutBooking();
        MyReason = ProductAuditReasonFactory.CreateProductAuditReason();
        MySupplier = SupplierFactory.CreateSupplier();
        MyLabel = Product_Article_LabelFactory.CreateProduct_Article_Label();
        MyPro_Label = Product_LabelFactory.CreateProduct_Label();
    }

    public void AddProduct()
    {
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = Public.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = Public.GetFirstLetter(Product_SubName);
        int Product_PriceType = tools.CheckInt(Request.Form["Product_PriceType"]);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = 1;
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        string Product_Description = tools.CheckStr(Request.Form["Product_Description"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        string Product_SEO_Title = tools.CheckStr(Request.Form["Product_SEO_Title"]);
        string Product_SEO_Keyword = tools.CheckStr(Request.Form["Product_SEO_Keyword"]);
        string Product_SEO_Description = tools.CheckStr(Request.Form["Product_SEO_Description"]);
        int Product_SupplierID = tools.CheckInt(Request.Form["Product_SupplierID"]);
        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);
        int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        //string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        string Product_State_Name = tools.CheckStr(Request.Form["Product_State_Name"]);
        string Product_City_Name = tools.CheckStr(Request.Form["Product_City_Name"]);
        string Product_County_Name = tools.CheckStr(Request.Form["Product_County_Name"]);


        double Product_Grade_Price = 0;

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);

        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');

        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);





        if (!CheckProductCode(Product_Code, 0))
        {
            Public.Msg("error", "错误信息", "已存在该商品编码", false, "{back}");
            return;
        }

        //string code = "";
        //int num = 0;
        //QueryInfo Query1 = new QueryInfo();
        //Query1.PageSize = 1;
        //Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        //IList<ProductInfo> productinfos = MyBLL.GetProducts(Query1, Public.GetUserPrivilege());
        //if (productinfos != null)
        //{
        //    foreach (ProductInfo productinfo in productinfos)
        //    {
        //        code = productinfo.Product_Code.Replace("SZHJ", "");
        //        num = tools.NullInt(code) + 1;
        //        Product_Code = "SZHJ" + num.ToString("000000000");
        //    }
        //}
        //else
        //{
        //    Product_Code = "SZHJ000000001";
        //}

        ProductInfo entity = new ProductInfo();
        entity.Product_ID = Product_ID;
        entity.Product_Code = Product_Code;
        entity.Product_CateID = Product_Cate;
        entity.Product_BrandID = Product_BrandID;
        entity.Product_TypeID = Product_TypeID;
        entity.Product_SupplierID = Product_SupplierID;
        entity.Product_Supplier_CommissionCateID = 0;
        entity.Product_Name = Product_Name;
        entity.Product_NameInitials = Product_NameInitials;
        entity.Product_SubName = Product_SubName;
        entity.Product_SubNameInitials = Product_SubNameInitials;
        entity.Product_PriceType = Product_PriceType;
        entity.Product_MKTPrice = Product_MKTPrice;
        entity.Product_GroupPrice = Product_GroupPrice;
        entity.Product_PurchasingPrice = 0;
        entity.Product_Price = Product_Price;
        entity.Product_PriceUnit = Product_PriceUnit;
        entity.Product_Unit = Product_Unit;
        entity.Product_GroupNum = Product_GroupNum;
        entity.Product_Note = Product_Note;
        entity.Product_NoteColor = Product_NoteColor;
        entity.Product_Audit_Note = "";
        entity.Product_Weight = Product_Weight;
        entity.Product_Img = Product_Img;
        entity.Product_Publisher = Session["User_Name"].ToString();
        entity.Product_StockAmount = Product_StockAmount;
        entity.Product_SaleAmount = 0;
        entity.Product_Review_Count = 0;
        entity.Product_Review_ValidCount = 0;
        entity.Product_Review_Average = 0;
        entity.Product_IsInsale = Product_IsInsale;
        entity.Product_IsGroupBuy = Product_IsGroupBuy;
        entity.Product_IsCoinBuy = Product_IsCoinBuy;
        entity.Product_IsFavor = Product_IsFavor;
        entity.Product_IsGift = Product_IsGift;
        entity.Product_IsAudit = Product_IsAudit;
        entity.Product_IsGiftCoin = Product_IsGiftCoin;
        entity.Product_Gift_Coin = Product_Gift_Coin;
        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
        entity.Product_Addtime = DateTime.Now;
        entity.Product_Intro = Product_Intro;
        entity.Product_AlertAmount = Product_AlertAmount;
        entity.Product_UsableAmount = 0;
        entity.Product_IsNoStock = Product_IsNoStock;
        entity.Product_Spec = Product_Spec;
        entity.Product_Maker = Product_Maker;
        entity.Product_Description = Product_Description;
        entity.Product_Sort = Product_Sort;
        entity.Product_Hits = 0;
        entity.Product_QuotaAmount = Product_QuotaAmount;
        entity.Product_Site = Public.GetCurrentSite();
        entity.Product_SEO_Title = Product_SEO_Title;
        entity.Product_SEO_Keyword = Product_SEO_Keyword;
        entity.Product_SEO_Description = Product_SEO_Description;
        entity.U_Product_Parameters = tools.CheckHTML(Request.Form["U_Product_Parameters"]);
        entity.U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        entity.U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        entity.Product_IsListShow = 1;
        entity.Product_GroupCode = "";

        entity.U_Product_MinBook = U_Product_MinBook;
        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
        entity.Product_State_Name = Product_State_Name;
        entity.Product_City_Name = Product_City_Name;
        entity.Product_County_Name = Product_County_Name;

        IList<ProductExtendInfo> extends = ReadProductExtend(0);

        if (MyBLL.AddProduct(entity, cateArray, tagArray, ProductImages, extends, Public.GetUserPrivilege()))
        {
            ProductInfo productinfo = new ProductInfo();
            productinfo = MyBLL.GetProductByCode(Product_Code, Public.GetCurrentSite(), Public.GetUserPrivilege());
            if (productinfo != null)
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
                Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
                IList<MemberGradeInfo> grades = MyGrade.GetMemberGrades(Query, Public.GetUserPrivilege());
                if (grades != null)
                {
                    foreach (MemberGradeInfo grade in grades)
                    {
                        Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                        if (Product_Grade_Price > 0)
                        {
                            ProductPriceInfo priceinfo = new ProductPriceInfo();
                            priceinfo.Product_Price_ID = 0;
                            priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                            priceinfo.Product_Price_Price = Product_Grade_Price;
                            priceinfo.Product_Price_ProcutID = productinfo.Product_ID;
                            MyPrice.AddProductPrice(priceinfo);
                        }
                    }
                }

                //更新聚合信息
                string group_productid = "";
                string Group_Code = "";

                group_productid = tools.NullStr(Request["favor_productid"]);
                if (group_productid.Length > 0)
                {
                    Group_Code = tools.NullStr(Guid.NewGuid());
                    foreach (string subproductid in group_productid.Split(','))
                    {
                        if (tools.CheckInt(subproductid) > 0)
                        {
                            MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + tools.CheckInt(subproductid)]), tools.CheckInt(subproductid));
                        }
                    }
                    MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + productinfo.Product_ID]), productinfo.Product_ID);
                }

            }

            Public.Msg("positive", "操作成功", "操作成功", true, "product_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    private IList<ProductExtendInfo> ReadProductExtend(int Product_ID)
    {
        IList<ProductExtendInfo> extends = new List<ProductExtendInfo>();
        ProductExtendInfo extend = null;
        foreach (string frmelement in Request.Form)
        {
            if (frmelement.Length > 14 && frmelement.Substring(0, 14) == "product_extend")
            {
                extend = new ProductExtendInfo();
                extend.Product_ID = Product_ID;
                extend.Extent_ID = tools.CheckInt(frmelement.Substring(14));
                extend.Extend_Value = tools.CheckStr(Request.Form[frmelement]);
                extend.Extend_Img = tools.CheckStr(Request.Form["PExtend" + extend.Extent_ID + "_img"]);
                extends.Add(extend);
                extend = null;
            }
            else
            {
                if (frmelement.Length > 8 && frmelement.Substring(0, 8) == "p_extend")
                {
                    extend = new ProductExtendInfo();
                    extend.Product_ID = Product_ID;
                    extend.Extent_ID = tools.CheckInt(frmelement.Substring(8));
                    extend.Extend_Value = Request.Form[frmelement];
                    extends.Add(extend);
                    extend = null;
                }
            }
        }

        return extends;
    }

    public void EditProduct()
    {
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = Public.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = Public.GetFirstLetter(Product_SubName);
        int Product_PriceType = tools.CheckInt(Request.Form["Product_PriceType"]);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = 1;
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        string Product_Description = tools.CheckStr(Request.Form["Product_Description"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        string Product_SEO_Title = tools.CheckStr(Request.Form["Product_SEO_Title"]);
        string Product_SEO_Keyword = tools.CheckStr(Request.Form["Product_SEO_Keyword"]);
        string Product_SEO_Description = tools.CheckStr(Request.Form["Product_SEO_Description"]);
        int Product_SupplierID = tools.CheckInt(Request.Form["Product_SupplierID"]);
        int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        string Product_State_Name = tools.CheckStr(Request.Form["Product_State_Name"]);
        string Product_City_Name = tools.CheckStr(Request.Form["Product_City_Name"]);
        string Product_County_Name = tools.CheckStr(Request.Form["Product_County_Name"]);

        double Product_Grade_Price = 0;

        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

        string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
        string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
        string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
        string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
        string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);


        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);

        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');

        if (!CheckProductCode(Product_Code, Product_ID))
        {
            Public.Msg("error", "错误信息", "已存在该商品编码", false, "{back}");
            return;
        }


        ProductInfo entity = GetProductByID(Product_ID);
        if (entity == null)
        {
            entity = new ProductInfo();
        }

        entity.Product_ID = Product_ID;
        entity.Product_Code = Product_Code;
        entity.Product_CateID = Product_Cate;
        entity.Product_BrandID = Product_BrandID;
        entity.Product_TypeID = Product_TypeID;
        entity.Product_Name = Product_Name;
        entity.Product_NameInitials = Product_NameInitials;
        entity.Product_SubName = Product_SubName;
        entity.Product_SubNameInitials = Product_SubNameInitials;
        entity.Product_PriceType = Product_PriceType;
        entity.Product_MKTPrice = Product_MKTPrice;
        entity.Product_GroupPrice = Product_GroupPrice;
        entity.Product_Price = Product_Price;
        entity.Product_PriceUnit = Product_PriceUnit;
        entity.Product_Unit = Product_Unit;
        entity.Product_GroupNum = Product_GroupNum;
        entity.Product_Note = Product_Note;
        entity.Product_NoteColor = Product_NoteColor;
        entity.Product_Audit_Note = "";
        entity.Product_Weight = Product_Weight;
        entity.Product_Img = Product_Img;
        entity.Product_Publisher = Session["User_Name"].ToString();
        entity.Product_IsInsale = Product_IsInsale;
        entity.Product_IsGroupBuy = Product_IsGroupBuy;
        entity.Product_IsCoinBuy = Product_IsCoinBuy;
        entity.Product_IsGiftCoin = Product_IsGiftCoin;
        entity.Product_Gift_Coin = Product_Gift_Coin;
        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
        entity.Product_StockAmount = Product_StockAmount;
        entity.Product_Intro = Product_Intro;
        entity.Product_Spec = Product_Spec;
        entity.Product_Maker = Product_Maker;
        entity.Product_Description = Product_Description;
        entity.Product_Sort = Product_Sort;
        entity.Product_Site = Public.GetCurrentSite();
        entity.Product_SEO_Title = Product_SEO_Title;
        entity.Product_SEO_Keyword = Product_SEO_Keyword;
        entity.Product_SEO_Description = Product_SEO_Description;
        entity.U_Product_Parameters = tools.CheckHTML(Request.Form["U_Product_Parameters"]);
        entity.U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        entity.U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        entity.Product_AlertAmount = Product_AlertAmount;
        entity.Product_State_Name = Product_State_Name;
        entity.Product_City_Name = Product_City_Name;
        entity.Product_County_Name = Product_County_Name;
        entity.Product_SupplierID = Product_SupplierID;
        if (entity.Product_SupplierID == 0)
        {
            entity.Product_IsNoStock = Product_IsNoStock;
        }
        entity.U_Product_MinBook = U_Product_MinBook;
        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;

        IList<ProductExtendInfo> extends = ReadProductExtend(Product_ID);

        //记录主产品价格调价
        IList<ProductPriceInfo> prices = null;
        double price_original = 0;
        ProductInfo product = MyBLL.GetProductByID(Product_ID, Public.GetUserPrivilege());
        if (product != null)
        {
            Product_History_Price_Add(Session["User_Name"].ToString(), Product_ID, "市场价", product.Product_MKTPrice, Product_MKTPrice);
            Product_History_Price_Add(Session["User_Name"].ToString(), Product_ID, "本站价", product.Product_Price, Product_Price);
            Product_History_Price_Add(Session["User_Name"].ToString(), Product_ID, "团购价", product.Product_GroupPrice, Product_GroupPrice);
            prices = MyPrice.GetProductPrices(Product_ID);
        }

        if (MyBLL.EditProduct(entity, cateArray, tagArray, ProductImages, extends, Public.GetUserPrivilege()))
        {
            //记录各会员等级价格调价
            IList<MemberGradeInfo> grades = null;
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
            grades = MyGrade.GetMemberGrades(Query, Public.GetUserPrivilege());
            if (grades != null)
            {
                foreach (MemberGradeInfo grade in grades)
                {
                    Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                    price_original = 0;
                    if (prices != null)
                    {
                        foreach (ProductPriceInfo price in prices)
                        {
                            if (price.Product_Price_MemberGradeID == grade.Member_Grade_ID)
                            {
                                price_original = price.Product_Price_Price;
                            }
                        }
                    }

                    Product_History_Price_Add(Session["User_Name"].ToString(), Product_ID, grade.Member_Grade_Name, price_original, Product_Grade_Price);

                }
            }
            Query = null;
            MyPrice.DelProductPrice(Product_ID);
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
            Query1.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
            grades = MyGrade.GetMemberGrades(Query1, Public.GetUserPrivilege());
            if (grades != null)
            {
                foreach (MemberGradeInfo grade in grades)
                {
                    Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                    if (Product_Grade_Price > 0)
                    {
                        ProductPriceInfo priceinfo = new ProductPriceInfo();
                        priceinfo.Product_Price_ID = 0;
                        priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                        priceinfo.Product_Price_Price = Product_Grade_Price;
                        priceinfo.Product_Price_ProcutID = Product_ID;
                        MyPrice.AddProductPrice(priceinfo);
                    }
                }
            }

            MyPro_Label.DelProduct_Label_ProductID(Product_ID);

            string[] strkeyword = { Product_Keyword1, Product_Keyword2, Product_Keyword3, Product_Keyword4, Product_Keyword5 };
            for (int i = 0; i < strkeyword.Length; i++)
            {
                string name = strkeyword[i].ToString();
                if (name != "")
                {
                    Product_Article_LabelInfo entity1 = MyLabel.GetProduct_Article_LabelByName(name);
                    Product_LabelInfo entity3 = new Product_LabelInfo();
                    if (entity1 == null)
                    {
                        entity1 = new Product_Article_LabelInfo();
                        entity1.Product_Article_LabelName = name;
                        MyLabel.AddProduct_Article_Label(entity1);
                        int LabelID = 0;
                        Product_Article_LabelInfo entity2 = MyLabel.GetProduct_Article_LabelByTopID();
                        if (entity2 != null)
                        {
                            LabelID = entity2.Product_Article_LabelID;
                            entity3.Product_Label_LabelID = LabelID;
                            entity3.Product_Label_ProductID = Product_ID;
                            MyPro_Label.AddProduct_Label(entity3);
                        }
                    }
                    else
                    {
                        entity3.Product_Label_LabelID = entity1.Product_Article_LabelID;
                        entity3.Product_Label_ProductID = Product_ID;
                        MyPro_Label.AddProduct_Label(entity3);

                    }
                }

            }

            ////更新聚合信息
            ////string group_productid = "";
            ////string group_productid = GetGroupProductID(entity.Product_GroupCode);
            ////if (entity.Product_GroupCode.Length > 0)
            ////{
            ////    MyBLL.UpdateProductGroupCode("", entity.Product_GroupCode);
            ////}
            ////string Group_Code = "";
            //string group_productid = GetGroupProductID(entity.Product_GroupCode);
            //string Group_Code = "";

            //if (group_productid.Length > 0)
            //{
            //    foreach (string subproduct in group_productid.Split(','))
            //    {
            //        if (tools.CheckInt(subproduct) > 0)
            //        {
            //            MyBLL.UpdateProductGroupInfo("", 1, tools.CheckInt(subproduct));
            //        }
            //    }
            //}



            //group_productid = tools.NullStr(Request["favor_productid"]);
            //if (group_productid.Length > 0)
            //{
            //    Group_Code = tools.NullStr(Guid.NewGuid());
            //    foreach (string subproductid in group_productid.Split(','))
            //    {
            //        if (tools.CheckInt(subproductid) > 0)
            //        {
            //            MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + tools.CheckInt(subproductid)]), tools.CheckInt(subproductid));
            //        }
            //    }
            //    MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + Product_ID]), Product_ID);
            //}
            //else
            //{
            //    MyBLL.UpdateProductGroupInfo("", 1, Product_ID);
            //}


            //Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
            //更新聚合信息
            string group_productid = GetGroupProductID(entity.Product_GroupCode);
            string Group_Code = "";

            if (group_productid.Length > 0)
            {
                foreach (string subproduct in group_productid.Split(','))
                {
                    if (tools.CheckInt(subproduct) > 0)
                    {
                        MyBLL.UpdateProductGroupInfo("", 1, tools.CheckInt(subproduct));
                    }
                }
            }

            group_productid = tools.NullStr(Request["favor_productid"]);
            if (group_productid.Length > 0)
            {
                Group_Code = tools.NullStr(Guid.NewGuid());
                foreach (string subproductid in group_productid.Split(','))
                {
                    if (tools.CheckInt(subproductid) > 0)
                    {
                        MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + tools.CheckInt(subproductid)]), tools.CheckInt(subproductid));
                    }
                }
                MyBLL.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + Product_ID]), Product_ID);
            }
            else
            {
                MyBLL.UpdateProductGroupInfo("", 1, Product_ID);
            }

            Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //获取商品图片信息
    public string[] GetProductLabel(int product_id)
    {
        string ipaths = MyPro_Label.GetProductLabel(product_id);
        string[] ipathArr = { "", "", "", "", "" };
        if (ipaths.Length > 0)
        {
            ipaths += ",,,,,";
            ipathArr = ipaths.Split(',');
        }
        return ipathArr;
    }

    public void EditProductSEO()
    {
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        string Product_SEO_Title = tools.CheckStr(Request.Form["Product_SEO_Title"]);
        string Product_SEO_Keyword = tools.CheckStr(Request.Form["Product_SEO_Keyword"]);
        string Product_SEO_Description = tools.CheckStr(Request.Form["Product_SEO_Description"]);

        ProductInfo entity = GetProductByID(Product_ID);
        if (entity == null)
        {
            entity = new ProductInfo();
        }
        entity.Product_ID = Product_ID;
        entity.Product_SEO_Title = Product_SEO_Title;
        entity.Product_SEO_Keyword = Product_SEO_Keyword;
        entity.Product_SEO_Description = Product_SEO_Description;

        if (MyBLL.EditProductInfo(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProduct()
    {
        int product_id = tools.CheckInt(Request.QueryString["product_id"]);

        ProductInfo objEntity = GetProductByID(product_id);
        if (objEntity == null || objEntity.Product_SupplierID != 0)
        {
            objEntity = null;
            Public.Msg("error", "错误信息", "记录不存在", true, "product.aspx");
        }
        objEntity = null;

        if (MyBLL.DelProduct(product_id, Public.GetUserPrivilege()) == -1)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "product.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProduct_Batch()
    {
        string product_id = tools.CheckStr(Request.QueryString["product_id"]);
        if (product_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要删除的产品", false, "{back}");
            return;
        }

        if (tools.Left(product_id, 1) == ",") { product_id = product_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = MyBLL.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                if (entity.Product_SupplierID != 0) continue;

                MyBLL.DelProduct(entity.Product_ID, Public.GetUserPrivilege());
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "product.aspx");
    }

    /// <summary>
    /// 复制产品信息
    /// </summary>
    /// <param name="Product_ID">产品ID</param>
    public void CopyProduct(int Product_ID)
    {
        string SqlAdd = "INSERT INTO Product_Basic (Product_Code ,Product_CateID ,Product_BrandID ,Product_TypeID ,Product_SupplierID,Product_Supplier_CommissionCateID,Product_Name ,Product_NameInitials ,Product_SubName ,Product_SubNameInitials ,Product_MKTPrice ,Product_GroupPrice ,Product_PurchasingPrice,Product_Price ,Product_PriceUnit ,Product_Unit ,Product_GroupNum ,Product_Note ,Product_NoteColor ,Product_Weight ,Product_Img ,Product_Publisher ,Product_StockAmount ,Product_SaleAmount ,Product_Review_Count ,Product_Review_ValidCount ,Product_Review_Average ,Product_IsInsale ,Product_IsGroupBuy ,Product_IsCoinBuy ,Product_IsFavor ,Product_IsGift ,Product_IsGiftCoin ,Product_Gift_Coin ,Product_CoinBuy_Coin ,Product_IsAudit ,Product_Addtime ,Product_Intro ,Product_AlertAmount ,Product_UsableAmount ,Product_IsNoStock ,Product_Spec ,Product_Maker ,Product_Sort ,Product_QuotaAmount ,Product_Hits ,Product_Site ,Product_SEO_Title ,Product_SEO_Keyword ,Product_SEO_Description,Product_IsListShow,Product_GroupCode)";

        SqlAdd += "SELECT Product_Code ,Product_CateID ,Product_BrandID ,Product_TypeID , Product_SupplierID,Product_Supplier_CommissionCateID,'复件 '+Product_Name ,Product_NameInitials ,Product_SubName ,Product_SubNameInitials ,Product_MKTPrice ,Product_GroupPrice ,Product_PurchasingPrice,Product_Price ,Product_PriceUnit ,Product_Unit ,Product_GroupNum ,Product_Note ,Product_NoteColor ,Product_Weight ,Product_Img ,Product_Publisher ,Product_StockAmount ,0 ,0 ,0 ,0 ,Product_IsInsale ,Product_IsGroupBuy ,Product_IsCoinBuy ,Product_IsFavor ,Product_IsGift ,Product_IsGiftCoin ,Product_Gift_Coin ,Product_CoinBuy_Coin ,Product_IsAudit ,GETDATE() ,Product_Intro ,Product_AlertAmount ,Product_UsableAmount ,Product_IsNoStock ,Product_Spec ,Product_Maker ,Product_Sort ,Product_QuotaAmount ,0 ,Product_Site ,Product_SEO_Title ,Product_SEO_Keyword ,Product_SEO_Description,Product_IsListShow,Product_GroupCode FROM Product_Basic where Product_ID = " + Product_ID + " SELECT @@IDENTITY AS returnName";

        int NewProduct_ID = 0;
        Glaer.Trade.Util.SQLHelper.ISQLHelper DBHelper = Glaer.Trade.Util.SQLHelper.SQLHelperFactory.CreateSQLHelper();
        try
        {
            NewProduct_ID = int.Parse(DBHelper.ExecuteScalar(SqlAdd).ToString());

            if (NewProduct_ID > 0)
            {
                //复制类别
                SqlAdd = string.Empty;
                SqlAdd = "INSERT INTO Product_Category (Product_Category_CateID, Product_Category_ProductID) SELECT Product_Category_CateID, '" + NewProduct_ID + "' FROM Product_Category WHERE Product_Category_ProductID=" + Product_ID;
                DBHelper.ExecuteNonQuery(SqlAdd);

                //复制图片
                SqlAdd = string.Empty;
                SqlAdd = "INSERT INTO Product_Img (Product_Img_ProductID, Product_Img_Path) SELECT '" + NewProduct_ID + "', Product_Img_Path FROM Product_Img WHERE Product_Img_ProductID=" + Product_ID;
                DBHelper.ExecuteNonQuery(SqlAdd);

                //复制扩展属性
                SqlAdd = string.Empty;
                SqlAdd = "INSERT INTO Product_Extend (Product_Extend_ProductID, Product_Extend_ExtendID, Product_Extend_Value, Product_Extend_Img) SELECT '" + NewProduct_ID + "', Product_Extend_ExtendID, Product_Extend_Value, Product_Extend_Img FROM Product_Extend WHERE Product_Extend_ProductID=" + Product_ID;
                DBHelper.ExecuteNonQuery(SqlAdd);

                //复制价格属性
                SqlAdd = string.Empty;
                SqlAdd = "INSERT INTO Product_Price (Product_Price_ProcutID, Product_Price_MemberGradeID, Product_Price_Price) SELECT '" + NewProduct_ID + "', Product_Price_MemberGradeID, Product_Price_Price FROM Product_Price WHERE Product_Price_ProcutID=" + Product_ID;
                DBHelper.ExecuteNonQuery(SqlAdd);

                //复制标签属性
                SqlAdd = string.Empty;
                SqlAdd = "INSERT INTO Product_RelateTag (Product_RelateTag_ProductID, Product_RelateTag_TagID) SELECT '" + NewProduct_ID + "', Product_RelateTag_TagID FROM Product_RelateTag WHERE Product_RelateTag_ProductID=" + Product_ID;
                DBHelper.ExecuteNonQuery(SqlAdd);

                //Public.Msg("positive", "操作成功", "操作成功", false, "{close}");
            }
            else
            {
                Public.Msg("error", "错误信息", "数据没有复制成功, 请重试", false, "{back}");
            }

        }
        catch (Exception ex)
        {
            //throw ex;
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Basic WHERE Product_ID = " + NewProduct_ID);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Img WHERE Product_Img_ProductID =" + NewProduct_ID);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Category WHERE Product_Category_ProductID =" + NewProduct_ID);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_RelateTag WHERE Product_RelateTag_ProductID =" + NewProduct_ID);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Extend WHERE Product_Extend_ProductID =" + NewProduct_ID);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Price WHERE Product_Price_ProcutID =" + NewProduct_ID);

            Public.Msg("error", "错误信息", "数据没有复制成功, 请重试" + ex, false, "{back}");
        }

        if (NewProduct_ID > 0)
        {
            Response.Redirect("product_edit.aspx?Product_ID=" + NewProduct_ID);
        }
    }

    public ProductInfo GetProductByID(int product_id)
    {
        return MyBLL.GetProductByID(product_id, Public.GetUserPrivilege());
    }

    public string GetProducts()
    {
        string listtype = tools.CheckStr(Request["listtype"]);
        string product_idstr = "";
        string type = tools.CheckStr(Request["type"]);
        string searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
        string keyword = tools.CheckStr(Request.QueryString["keyword"]);

        int product_cate = tools.CheckInt(Request.QueryString["product_cate"]);
        int product_supplier = tools.CheckInt(Request.QueryString["product_supplier"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
       // Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));

        if (listtype == "unAudit")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "0"));
        if (listtype == "uninsale")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsInsale", "=", "0"));
        if (listtype == "insale")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsInsale", "=", "1"));
        if (listtype == "Audited")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
        if (listtype == "denyaudit")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "2"));

        if (product_cate > 0)
        {
            string subCates = Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_CateID", "in", subCates));
        }

        if (product_supplier != 0)
        {
            if (product_supplier == -1)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", "0"));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", product_supplier.ToString()));
            }
        }

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SupplierID", "in", "select Supplier_ID from Supplier where Supplier_Nickname like '%" + keyword + "%'"));

            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));
        }

        SupplierInfo productsupplier;

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductInfo> entitys = MyBLL.GetProducts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductInfo entity in entitys)
            {

                productsupplier = MySupplier.GetSupplierByID(entity.Product_SupplierID, Public.GetUserPrivilege());
                if (productsupplier == null)
                {
                    productsupplier = new SupplierInfo();
                    productsupplier.Supplier_CompanyName = "易耐";
                }

                jsonBuilder.Append("{\"id\":" + entity.Product_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_ID);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.Product_Code));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(productsupplier.Supplier_CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ProductTypeInfo ProductType = MyTBLL.GetProductTypeByID(entity.Product_TypeID, Public.GetUserPrivilege());
                if (ProductType != null)
                {
                    jsonBuilder.Append(Public.JsonStr(ProductType.ProductType_Name));
                }
                else
                {
                    //jsonBuilder.Append("&nbsp;");
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //if (entity.Product_PriceType == 1)
                //{
                //    jsonBuilder.Append("定价制");
                //}
                //else
                //{
                //    jsonBuilder.Append("计价制");
                //}
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                //if (entity.Product_PriceType == 1)
                //{
                //    jsonBuilder.Append(Public.DisplayCurrency(entity.Product_Price));
                //}
                //else
                //{
                //    jsonBuilder.Append(Public.DisplayCurrency(Public.GetProductPrice(entity.Product_ManualFee,entity.Product_Weight)));
                //}
                jsonBuilder.Append(Public.DisplayCurrency(entity.Product_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_StockAmount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_IsInsale == 0)
                {
                    jsonBuilder.Append("下架");
                }
                else
                {
                    jsonBuilder.Append("上架");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_IsAudit == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Product_IsAudit == 2)
                {
                    jsonBuilder.Append("<font color=\\\"#cc0000\\\">未通过</font>");
                }
                else
                {
                    jsonBuilder.Append("已审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (type == "product")
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"product_view.aspx?product_id=" + entity.Product_ID + "\\\" title=\\\"查看\\\" target=\\\"_blank\\\">查看</a>");
                }
                if (type == "seo")
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"product_seo_edit.aspx?product_id=" + entity.Product_ID + "\\\" title=\\\"修改SEO\\\" target=\\\"_black\\\">修改SEO</a>");
                }
                if (Public.CheckPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"product_edit.aspx?product_id=" + entity.Product_ID + "\\\" title=\\\"修改\\\" target=\\\"_black\\\">修改</a>");
                }

                if (Public.CheckPrivilege("fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f") && entity.Product_SupplierID == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('product_do.aspx?action=move&product_id=" + entity.Product_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                //jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"window.open('product_do.aspx?action=copyproduct&product_id=" + entity.Product_ID + "', '_black')\\\" title=\\\"复制\\\">复制</a>");

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

    //获取指定聚合编号的所有产品
    public string GetGroupProductID(string Group_Code)
    {
        return MyBLL.GetGroupProductID(Group_Code);
    }

    public string Get_All_SubCate(int Cate_id)
    {
        string Cate_Arry = MyCBLL.Get_All_SubCateID(Cate_id);
        return Cate_Arry;
    }

    //获取指定类别下全部产品编号
    public string Get_All_CateProductID(string Cate_Arry)
    {
        string ProudctID_Arry = "";
        ProudctID_Arry = MyBLL.GetCateProductID(Cate_Arry);
        return ProudctID_Arry;
    }

    public string[] GetProductImg(int product_id)
    {
        string ipaths = MyBLL.GetProductImg(product_id);
        string[] ipathArr = { "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif" };
        ipathArr = ipaths.Split(',');
        return ipathArr;
    }

    public string GetProductCategory(int product_id)
    {
        return MyBLL.GetProductCategory(product_id);
    }

    public string CategoryTree(int parentid, string cateid)
    {
        string cateidbak = "," + cateid + ",";
        string strHTML = "";
        IList<CategoryInfo> entitys = MyCBLL.GetSubCategorys(parentid, Public.GetCurrentSite(), Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (CategoryInfo entity in entitys)
            {
                if (entity.Cate_IsActive == 1)
                {
                    if (MyCBLL.GetSubCateCount(entity.Cate_ID, Public.GetCurrentSite(), Public.GetUserPrivilege()) > 0)
                    {
                        if (cateidbak.IndexOf("," + entity.Cate_ID + ",") >= 0)
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" open=\"yes\" checked=\"yes\">\n";
                        }
                        else
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\">\n";
                        }
                        strHTML += CategoryTree(entity.Cate_ID, cateid);
                        strHTML += "</item>\n";
                    }
                    else
                    {
                        if (cateidbak.IndexOf("," + entity.Cate_ID + ",") >= 0)
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" checked=\"yes\" />\n";
                        }
                        else
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" />\n";
                        }
                    }
                }
            }
        }
        return strHTML;
    }

    public string ProductDisplay(string ProductID)
    {
        string strHTML = "";
        if (ProductID == "")
        {
            return "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", ProductID));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "ASC"));

        IList<ProductInfo> entitys = MyBLL.GetProducts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                strHTML += entity.Product_Name + "&nbsp;&nbsp;";
            }
        }
        return strHTML;
    }

    public string CategoryDisplay(string CateID)
    {
        string strHTML = "";
        if (CateID == "")
        {
            return "";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "in", CateID));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "ASC"));

        IList<CategoryInfo> entitys = MyCBLL.GetCategorys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (CategoryInfo entity in entitys)
            {
                strHTML += entity.Cate_Name + "&nbsp;&nbsp;";
            }
        }
        return strHTML;
    }

    public string ProductTypeOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductType.ProductType_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductType.ProductType_Sort", "DESC"));
        IList<ProductTypeInfo> entitys = MyTBLL.GetProductTypes(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductTypeInfo entity in entitys)
            {
                if (entity.ProductType_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.ProductType_ID + "\" selected=\"selected\">" + entity.ProductType_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.ProductType_ID + "\">" + entity.ProductType_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

    public string ProductExtendEditor(int Product_TypeID, int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        string[] extDefault, valExts;
        string valExt = string.Empty, valImg = string.Empty;

        IList<ProductTypeExtendInfo> entitys = MyBLL.ProductExtendEditor(Product_TypeID);
        IList<ProductExtendInfo> extendList = null;

        if (entitys != null)
        {
            extendList = MyBLL.ProductExtendValue(Product_ID);

            strHTML.Append("<table cellspacing=\"3\" cellpadding=\"0\" border=\"0\">\n");
            foreach (ProductTypeExtendInfo entity in entitys)
            {
                valExts = GetProductExtendValue(entity.ProductType_Extend_ID, extendList).Split('|');

                valExt = valExts[0];
                valImg = valExts[1];

                strHTML.Append("\t<tr>\n");
                strHTML.Append("\t\t<td style=\"text-align:right;\">" + entity.ProductType_Extend_Name + "<td>\n");

                strHTML.Append("\t\t<td>");
                switch (entity.ProductType_Extend_Display.ToLower())
                {
                    case "text":
                        strHTML.Append("<input type=\"text\" name=\"product_extend" + entity.ProductType_Extend_ID + "\" value=\"" + valExt + "\" />");
                        break;
                    case "select":
                        extDefault = entity.ProductType_Extend_Default.Split('|');
                        strHTML.Append("<select name=\"product_extend" + entity.ProductType_Extend_ID + "\">");
                        foreach (string extText in extDefault)
                        {
                            if (extText.Length > 0)
                            {
                                if (extText == valExt)
                                {
                                    strHTML.Append("<option value=\"" + extText + "\" selected=\"selected\">" + extText + "</option>\n");
                                }
                                else
                                {
                                    strHTML.Append("<option value=\"" + extText + "\">" + extText + "</option>\n");
                                }
                            }
                        }
                        strHTML.Append("</select>");
                        break;
                    case "html":
                        strHTML.Append("<textarea style=\"display:none;\" cols=\"0\" rows=\"0\" id=\"tmp_content_" + entity.ProductType_Extend_ID + "\">" + valExt + "</textarea>");
                        strHTML.Append("<script type=\"text/javascript\">");
                        strHTML.Append("var oFCKeditor = new FCKeditor('p_extend" + entity.ProductType_Extend_ID + "');");
                        strHTML.Append("oFCKeditor.BasePath = \"/public/fckeditor/\";");
                        strHTML.Append("oFCKeditor.Width = 600;");
                        strHTML.Append("oFCKeditor.Height = 100;");
                        strHTML.Append("oFCKeditor.ToolbarSet = 'Basic';");
                        strHTML.Append("oFCKeditor.Value = MM_findObj('tmp_content_" + entity.ProductType_Extend_ID + "').value;");
                        strHTML.Append("oFCKeditor.Config['AutoDetectLanguage'] = false;");
                        strHTML.Append("oFCKeditor.Config['DefaultLanguage'] = \"zh-cn\";");
                        strHTML.Append("oFCKeditor.Create();");
                        strHTML.Append("</script>\n");
                        break;
                }
                strHTML.Append("<input type=\"hidden\" name=\"PExtend" + entity.ProductType_Extend_ID + "_img\" id=\"PExtend" + entity.ProductType_Extend_ID + "_img\" value=\"" + valImg + "\" />");
                if (entity.ProductType_Extend_Display.ToLower() != "html")
                {
                    strHTML.Append("<img src=\"/images/add_img.jpg\" border=\"0\" alt=\"添加图片\" id=\"img_PExtend" + entity.ProductType_Extend_ID + "_img\" onclick=\"uploadExample('PExtend" + entity.ProductType_Extend_ID + "_img')\" />");
                }
                strHTML.Append("<td>\n");
                strHTML.Append("\t</tr>\n");
            }
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string ProductExtendDisplay(int Product_TypeID, int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        string[] extDefault;
        string valExt = "";

        IList<ProductTypeExtendInfo> entitys = MyBLL.ProductExtendEditor(Product_TypeID);
        IList<ProductExtendInfo> extendList = null;

        if (entitys != null)
        {
            extendList = MyBLL.ProductExtendValue(Product_ID);

            strHTML.Append("<table cellspacing=\"3\" cellpadding=\"0\" border=\"0\">\n");
            foreach (ProductTypeExtendInfo entity in entitys)
            {
                valExt = GetProductExtendValue(entity.ProductType_Extend_ID, extendList);

                strHTML.Append("\t<tr>\n");
                strHTML.Append("\t\t<td style=\"text-align:right;\">" + entity.ProductType_Extend_Name + "<td>\n");
                strHTML.Append("\t\t<td>" + valExt + "<td>\n");
                strHTML.Append("\t</tr>\n");
            }
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string GetProductExtendValue(int Extend_ID, IList<ProductExtendInfo> extendList)
    {
        string valExt = "|";
        try
        {
            if (extendList != null)
            {
                foreach (ProductExtendInfo entity in extendList)
                {
                    if (entity.Extent_ID == Extend_ID)
                    {
                        valExt = entity.Extend_Value + "|" + entity.Extend_Img;
                        break;
                    }
                }
            }
        }
        catch (Exception ex) { }

        return valExt;
    }

    public string ProductBrandOption(int Product_TypeID, int selectValue)
    {
        string strHTML = "";
        IList<BrandInfo> entitys = MyTBLL.GetProductBrands(Product_TypeID, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (BrandInfo entity in entitys)
            {
                if (entity.Brand_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.Brand_ID + "\" selected=\"selected\">" + entity.Brand_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.Brand_ID + "\">" + entity.Brand_Name + "</option>";
                }
            }
        }

        return strHTML;

    }

    public string ProductTagChoose(int Product_ID)
    {
        string strHTML = "";
        string strTag = ",0";

        if (Product_ID > 0)
            strTag = "," + MyBLL.GetProductTag(Product_ID) + ",";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_ID", "DESC"));
        //PageInfo pageinfo = MyPTBLL.GetPageInfo(Query);
        IList<ProductTagInfo> entitys = MyPTBLL.GetProductTags(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductTagInfo entity in entitys)
            {
                if (strTag.IndexOf("," + entity.Product_Tag_ID.ToString() + ",") > 0)
                {
                    strHTML += " <input type=\"checkbox\" name=\"ProductTag_ID\" value=\"" + entity.Product_Tag_ID + "\" checked=\"checked\">" + entity.Product_Tag_Name;
                }
                else
                {
                    strHTML += " <input type=\"checkbox\" name=\"ProductTag_ID\" value=\"" + entity.Product_Tag_ID + "\">" + entity.Product_Tag_Name;
                }
            }
        }

        return strHTML;
    }

    public string ProductTagDisplay(int Product_ID)
    {
        string strHTML = "";
        string strTag = "";

        if (Product_ID > 0)
            strTag = MyBLL.GetProductTag(Product_ID);

        if (strTag.Length > 1)
            strTag = strTag.Substring(1);

        if (strTag.Length == 0)
            return "";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_ID", "in", strTag));

        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_ID", "DESC"));

        IList<ProductTagInfo> entitys = MyPTBLL.GetProductTags(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductTagInfo entity in entitys)
            {
                strHTML += entity.Product_Tag_Name + "&nbsp;&nbsp;";
            }
        }

        return strHTML;
    }

    public bool CheckProductCode(string code, int id)
    {
        if (code == null || code.Length == 0) { return false; }

        ProductInfo Entity = MyBLL.GetProductByCode(code, Public.GetCurrentSite(), Public.GetUserPrivilege());
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

    public void ProductExport_All()
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = MyBLL.GetProductList(Query, Public.GetUserPrivilege());

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "品名", "通用名称", "材质型号", "生产企业", "单位", "团购价格", "本站价格", "团购数量", "团购", "上架", "审核", "商品备注", "商品预警数量" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (ProductInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr["编号"] = entity.Product_Code;
            dr["品名"] = entity.Product_Name;
            dr["通用名称"] = entity.Product_SubName;
            dr["材质型号"] = entity.Product_Spec;
            dr["生产企业"] = entity.Product_Maker;
            dr["单位"] = entity.Product_Unit;
            //dr[6] = Public.DisplayCurrency(entity.Product_MKTPrice);
            dr["团购价格"] = Public.DisplayCurrency(entity.Product_GroupPrice);
            dr["本站价格"] = Public.DisplayCurrency(entity.Product_Price);
            dr["团购数量"] = entity.Product_GroupNum;
            if (entity.Product_IsGroupBuy == 1)
            {
                dr["团购"] = "是";
            }
            else
            {
                dr["团购"] = "否";
            }
            if (entity.Product_IsInsale == 1)
            {
                dr["上架"] = "是";
            }
            else
            {
                dr["上架"] = "否";
            }
            if (entity.Product_IsAudit == 1)
            {
                dr["审核"] = "是";
            }
            else
            {
                dr["审核"] = "否";
            }
            dr["商品备注"] = entity.Product_Note;
            dr["商品预警数量"] = entity.Product_AlertAmount;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }

    public void ProductExport()
    {
        string product_id = tools.CheckStr(Request.QueryString["product_id"]);
        if (product_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的产品", false, "{back}");
            return;
        }

        if (tools.Left(product_id, 1) == ",") { product_id = product_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = MyBLL.GetProductList(Query, Public.GetUserPrivilege());

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "品名", "通用名称", "材质型号", "生产企业", "单位", "团购价格", "本站价格", "团购数量", "团购", "上架", "审核", "商品备注", "商品预警数量" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (ProductInfo entity in entitys)
        {
            dr = dt.NewRow();
            dr["编号"] = entity.Product_Code;
            dr["品名"] = entity.Product_Name;
            dr["通用名称"] = entity.Product_SubName;
            dr["材质型号"] = entity.Product_Spec;
            dr["生产企业"] = entity.Product_Maker;
            dr["单位"] = entity.Product_Unit;
            //dr[6] = Public.DisplayCurrency(entity.Product_MKTPrice);
            dr["团购价格"] = Public.DisplayCurrency(entity.Product_GroupPrice);
            dr["本站价格"] = Public.DisplayCurrency(entity.Product_Price);
            dr["团购数量"] = entity.Product_GroupNum;
            if (entity.Product_IsGroupBuy == 1)
            {
                dr["团购"] = "是";
            }
            else
            {
                dr["团购"] = "否";
            }
            if (entity.Product_IsInsale == 1)
            {
                dr["上架"] = "是";
            }
            else
            {
                dr["上架"] = "否";
            }
            if (entity.Product_IsAudit == 1)
            {
                dr["审核"] = "是";
            }
            else
            {
                dr["审核"] = "否";
            }
            dr["商品备注"] = entity.Product_Note;
            dr["商品预警数量"] = entity.Product_AlertAmount;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }

    public void ProductPriceExport()
    {
        string start_date = tools.CheckStr(Request["start_date"]);
        string end_date = tools.CheckStr(Request["end_date"]);
        if (start_date == "")
        {
            start_date = DateTime.Now.ToShortDateString();
        }
        if (end_date == "")
        {
            end_date = DateTime.Now.ToShortDateString();
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ProductHistoryPriceInfo.History_Addtime}, '" + start_date + "')", "<=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ProductHistoryPriceInfo.History_Addtime}, '" + end_date + "')", ">=", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductHistoryPriceInfo.History_ID", "ASC"));
        IList<ProductHistoryPriceInfo> entitys = MyHistory.GetProductHistoryPrices(Query);

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;
        bool distinct = false;
        int i = 0;
        string member_price = "";
        ProductInfo productinfo = new ProductInfo();
        productinfo = null;

        string[] dtcol = { "商品编号", "商品名称", "通用名称", "材质型号", "生产企业", "原市场价", "现市场价", "原本站价", "现本站价", "原团购价", "现团购价", "会员价" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;
        i = 0;
        foreach (ProductHistoryPriceInfo entity in entitys)
        {

            if (entity.History_PriceType == "市场价")
            {

                distinct = true;
            }
            else
            {
                distinct = false;
            }
            if (distinct)
            {
                productinfo = MyBLL.GetProductByID(entity.History_ProductID, Public.GetUserPrivilege());
                if (productinfo != null)
                {
                    i = i + 1;
                    if (i > 1)
                    {
                        dr[11] = member_price;
                        dt.Rows.Add(dr);
                        dr = null;
                        member_price = "";
                    }
                    dr = dt.NewRow();
                    dr[0] = productinfo.Product_Code;
                    dr[1] = productinfo.Product_Name;
                    dr[2] = productinfo.Product_SubName;
                    dr[3] = productinfo.Product_Spec;
                    dr[4] = productinfo.Product_Maker;
                    dr[5] = Public.DisplayCurrency(entity.History_Price_Original);
                    dr[6] = Public.DisplayCurrency(entity.History_Price_New);
                }

            }
            else
            {
                if (productinfo != null)
                {
                    switch (entity.History_PriceType)
                    {
                        case "本站价":
                            dr[7] = Public.DisplayCurrency(entity.History_Price_Original);
                            dr[8] = Public.DisplayCurrency(entity.History_Price_New);
                            break;
                        case "团购价":
                            dr[9] = Public.DisplayCurrency(entity.History_Price_Original);
                            dr[10] = Public.DisplayCurrency(entity.History_Price_New);
                            break;
                        default:
                            member_price = member_price + "原" + entity.History_PriceType + ":" + Public.DisplayCurrency(entity.History_Price_Original);
                            member_price = member_price + " 现" + entity.History_PriceType + ":" + Public.DisplayCurrency(entity.History_Price_New) + "<br>";
                            break;
                    }


                }

            }


        }

        Public.toExcel(dt);
    }

    public string Product_Grade_Price_Formbak(int product_id, double Price, int isDisplay)
    {
        string form_str = "<table width=\"100%\" border=\"0\" bgcolor=\"#EFF4F8\" cellpadding=\"5\" cellspacing=\"1\" align=\"center\">";
        IList<ProductPriceInfo> prices = null;
        bool ismatch = false;

        if (product_id > 0)
        {
            prices = MyPrice.GetProductPrices(product_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
        IList<MemberGradeInfo> entitys = MyGrade.GetMemberGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberGradeInfo entity in entitys)
            {
                ismatch = false;

                form_str = form_str + "<tr>";
                form_str = form_str + "<td align=\"center\" width=\"33%\">" + entity.Member_Grade_Name + "</td>";

                if (prices != null)
                {
                    foreach (ProductPriceInfo price in prices)
                    {
                        if (price.Product_Price_MemberGradeID == entity.Member_Grade_ID)
                        {
                            ismatch = true;
                            if (isDisplay == 1)
                            {
                                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\">" + price.Product_Price_Price + "</td>";
                            }
                            else
                            {
                                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" value=\"" + price.Product_Price_Price + "\"></td>";
                            }
                        }
                    }
                    if (ismatch == false)
                    {
                        if (isDisplay == 1)
                        {
                            form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"></td>";
                        }
                        else
                        {
                            form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\"></td>";
                        }
                    }
                }
                else
                {
                    if (isDisplay == 1)
                    {
                        form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"></td>";
                    }
                    else
                    {
                        form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Member_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\"></td>";
                    }
                }
                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\">" + (((Price * entity.Member_Grade_Percent) / 100).ToString("0.00")) + "</td>";
                form_str = form_str + "</tr>";
            }
        }
        form_str = form_str + "</table>";
        return form_str;
    }

    public string Product_Grade_Price_Form(int product_id, double Price, int isDisplay)
    {
        string form_str = "<table width=\"100%\" border=\"0\" bgcolor=\"#EFF4F8\" cellpadding=\"5\" cellspacing=\"1\" align=\"center\">";
        IList<ProductPriceInfo> prices = null;
        bool ismatch = false;

        if (product_id > 0)
        {
            prices = MyPrice.GetProductPrices(product_id);
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierGradeInfo.Supplier_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierGradeInfo.Supplier_Grade_ID", "asc"));
        IList<SupplierGradeInfo> entitys = MySupplierGrade.GetSupplierGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierGradeInfo entity in entitys)
            {
                ismatch = false;

                form_str = form_str + "<tr>";
                form_str = form_str + "<td align=\"center\" width=\"33%\">" + entity.Supplier_Grade_Name + "</td>";

                if (prices != null)
                {
                    foreach (ProductPriceInfo price in prices)
                    {
                        if (price.Product_Price_MemberGradeID == entity.Supplier_Grade_ID)
                        {
                            ismatch = true;
                            if (isDisplay == 1)
                            {
                                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\">" + price.Product_Price_Price + "</td>";
                            }
                            else
                            {
                                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" value=\"" + price.Product_Price_Price + "\"></td>";
                            }
                        }
                    }
                    if (ismatch == false)
                    {
                        if (isDisplay == 1)
                        {
                            form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"></td>";
                        }
                        else
                        {
                            form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\"></td>";
                        }
                    }
                }
                else
                {
                    if (isDisplay == 1)
                    {
                        form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"></td>";
                    }
                    else
                    {
                        form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\"><input type=\"text\" name=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" id=\"Product_Grade_Price_" + entity.Supplier_Grade_ID + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\"></td>";
                    }
                }
                form_str = form_str + "<td align=\"center\" width=\"33%\" bgcolor=\"#ffffff\">" + (((Price * entity.Supplier_Grade_Percent) / 100).ToString("0.00")) + "</td>";
                form_str = form_str + "</tr>";
            }
        }
        form_str = form_str + "</table>";
        return form_str;
    }


    public IList<ProductPriceInfo> GetProductPrices(int product_id)
    {
        return MyPrice.GetProductPrices(product_id);
    }

    public void Product_History_Price_Add(string sys_name, int product_id, string price_type, double price_original, double price_new)
    {
        ProductHistoryPriceInfo historyprice = new ProductHistoryPriceInfo();
        historyprice.History_ID = 0;
        historyprice.History_SysName = sys_name;
        historyprice.History_ProductID = product_id;
        historyprice.History_PriceType = price_type;
        historyprice.History_Price_Original = price_original;
        historyprice.History_Price_New = price_new;
        historyprice.History_Addtime = DateTime.Now;
        MyHistory.AddProductHistoryPrice(historyprice);
    }

    public string Get_Category_Relate(int cate_id, string cate_str)
    {

        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            CategoryInfo category = MyCBLL.GetCategoryByID(cate_id, Public.GetUserPrivilege());
            if (category != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_Category_Relate(category.Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }

    public string Product_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_Category_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {

                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", cate));
                IList<CategoryInfo> categorys = MyCBLL.GetCategorys(Query, Public.GetUserPrivilege());
                if (categorys != null)
                {

                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"Product_cate\" name=\"Product_cate\" onchange=\"change_maincate('" + div_name + "','Product_cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "Product_cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_maincate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (CategoryInfo entity in categorys)
                    {
                        if (entity.Cate_IsActive == 1)
                        {
                            if (parentid == entity.Cate_ID || cate_id == entity.Cate_ID)
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\" selected>" + entity.Cate_Name + "</option>";
                            }
                            else
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\">" + entity.Cate_Name + "</option>";
                            }
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                categorys = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }

    public string GetProductIDByKeyword(string keyword)
    {
        string ProductID = "0";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
        Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));

        IList<ProductInfo> entitys = MyBLL.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                ProductID = ProductID + "," + entity.Product_ID;
            }
        }
        return ProductID;

    }

    //产品审核
    public void Product_Audit_Edit(int status)
    {
        string product_id = tools.CheckStr(Request["product_id"]);
        string audit_reason, Audit_Note;
        if (product_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的产品", false, "{back}");
            return;
        }

        audit_reason = tools.CheckStr(Request["audit_reason"]);
        Audit_Note = tools.CheckStr(Request["Audit_Note"]);

        if (tools.Left(product_id, 1) == ",") { product_id = product_id.Remove(0, 1); }

        ProductDenyReasonInfo reasoninfo;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = MyBLL.GetProducts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                if (entity.Product_IsAudit == 2)
                {
                    MyReason.DelProductDenyReason(entity.Product_ID);
                    entity.Product_Audit_Note = "";
                }
                if (status == 2)
                {
                    foreach (string subreason in audit_reason.Split(','))
                    {
                        if (tools.CheckInt(subreason) > 0)
                        {
                            reasoninfo = new ProductDenyReasonInfo();
                            reasoninfo.Product_Deny_Reason_ProductID = entity.Product_ID;
                            reasoninfo.Product_Deny_Reason_AuditReasonID = tools.CheckInt(subreason);
                            MyReason.AddProductDenyReason(reasoninfo);
                            reasoninfo = null;
                        }
                    }
                    entity.Product_Audit_Note = Audit_Note;
                }
                entity.Product_IsAudit = status;
                MyBLL.EditProductInfo(entity, Public.GetUserPrivilege());
            }
        }

        Response.Redirect("/product/product.aspx");

    }

    //产品审核
    public void Product_Insale_Edit(int status)
    {
        string product_id = tools.CheckStr(Request.QueryString["product_id"]);
        if (product_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的产品", false, "{back}");
            return;
        }

        if (tools.Left(product_id, 1) == ",") { product_id = product_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = MyBLL.GetProducts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                entity.Product_IsInsale = status;

                MyBLL.EditProductInfo(entity, Public.GetUserPrivilege());
            }
        }

        Response.Redirect("/product/product.aspx");

    }

    //供应商选择
    public string Product_Supplier_Select(int Supplier_ID, string Select_Name)
    {
        string select_str = "";
        select_str += "<select name=\"" + Select_Name + "\">";
        select_str += "<option value=\"0\">不限</option>";
        //if (Supplier_ID == -1)
        //{
        //    select_str += "<option value=\"-1\" selected>系统</option>";
        //}
        //else
        //{
        //    select_str += "<option value=\"-1\">系统</option>";
        //}
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_IsHaveShop", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_AuditStatus", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Status", "=", "1"));
        IList<SupplierInfo> entitys = MySupplier.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                if (entity.Supplier_ID == Supplier_ID)
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\" selected>" + entity.Supplier_CompanyName + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\">" + entity.Supplier_CompanyName + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }



    //产品单位选择
    public string Product_Unit_Select(int Product_ID, string Select_Unit)
    {
        string select_str = "";
        select_str += "<select name=\"" + Select_Unit + "\">";
        select_str += "<option value=\"0\">不限</option>";

        string[] Product_Units = { "kg", "吨", "块" };

        if (Product_ID==0)
        {
            foreach (string Product_Unit in Product_Units)
            {
                if (Product_Unit=="kg" )
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
                        select_str += "<option value=\"" + Product_Unit + "\" selected>" + Product_Unit + "</option>";
                    }
                    else
                    {
                        select_str += "<option value=\"" + Product_Unit + "\">" + Product_Unit + "</option>";
                    }
                }
            }
        }
       
        select_str += "</select>";
        return select_str;
    }

    //代销供应商选择
    public string Product_ConsignmentSupplier_Select(int Supplier_ID, string Select_Name)
    {
        string select_str = "";
        select_str += "<select name=\"" + Select_Name + "\">";
        select_str += "<option value=\"0\">不限</option>";
        //if (Supplier_ID == -1)
        //{
        //    select_str += "<option value=\"-1\" selected>系统</option>";
        //}
        //else
        //{
        //    select_str += "<option value=\"-1\">系统</option>";
        //}
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_IsHaveShop", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_AuditStatus", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Status", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_SaleType", "=", "1"));
        IList<SupplierInfo> entitys = MySupplier.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo entity in entitys)
            {
                if (entity.Supplier_ID == Supplier_ID)
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\" selected>" + entity.Supplier_CompanyName + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Supplier_ID + "\">" + entity.Supplier_CompanyName + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }


    #region "缺货登记"


    public string GetStockouts()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int isprocess = tools.CheckInt(Request["isprocess"]);
        string productidstr, memberidstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "StockoutBookingInfo.Stockout_Site", "=", Public.GetCurrentSite()));
        if (isprocess == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "StockoutBookingInfo.Stockout_IsRead", "=", "1"));
        }
        else if (isprocess == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "StockoutBookingInfo.Stockout_IsRead", "=", "0"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "StockoutBookingInfo.Stockout_Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "StockoutBookingInfo.Stockout_Member_Tel", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "StockoutBookingInfo.Stockout_Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "StockoutBookingInfo.Stockout_Member_Name", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = Mystockout.GetPageInfo(Query, Public.GetUserPrivilege());


        IList<StockoutBookingInfo> entitys = Mystockout.GetStockoutBookings(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (StockoutBookingInfo entity in entitys)
            {
                jsonBuilder.Append("{\"StockoutBookingInfo.Stockout_ID\":" + entity.Stockout_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Stockout_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Stockout_Product_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Stockout_Member_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Stockout_Member_Tel));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Stockout_Member_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Stockout_IsRead == 1) { jsonBuilder.Append("已处理"); }
                else { jsonBuilder.Append("未处理"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"stockout_view.aspx?stockout_id=" + entity.Stockout_ID + "\\\" title=\\\"查看\\\">查看</a>");
                if (Public.CheckPrivilege("cd25a138-603d-445c-83f9-736de139c4c1"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('stockout_do.aspx?action=move&stockout_id=" + entity.Stockout_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public StockoutBookingInfo GetStockoutByID(int ID)
    {
        return Mystockout.GetStockoutBookingByID(ID, Public.GetUserPrivilege());
    }

    public void Stockout_Processed()
    {
        int stockout_id = tools.CheckInt(Request["stockout_id"]);
        if (stockout_id > 0)
        {
            StockoutBookingInfo stockout = GetStockoutByID(stockout_id);
            if (stockout != null)
            {
                stockout.Stockout_IsRead = 1;
                Mystockout.EditStockoutBooking(stockout, Public.GetUserPrivilege());
            }
        }
        Response.Redirect("/stock/stockout_view.aspx?stockout_id=" + stockout_id);
    }

    public void Stockout_Del()
    {
        int stockout_id = tools.CheckInt(Request["stockout_id"]);
        if (Mystockout.DelStockoutBooking(stockout_id, Public.GetUserPrivilege()) == 1)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "stockout.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    #endregion


    #region  商品编号随机数
    private char[] constant =   
      {   
        '0','1','2','3','4','5','6','7','8','9',  
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
      };
    public string GenerateRandomNumber(int Length)
    {
        System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
        Random rd = new Random();
        for (int i = 0; i < Length; i++)
        {
            newRandom.Append(constant[rd.Next(62)]);
        }
        return newRandom.ToString();
    }
    #endregion
}
