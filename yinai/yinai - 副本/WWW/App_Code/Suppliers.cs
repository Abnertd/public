using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.Util.Http;
using System.Text;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// 前台供货商显示
/// </summary>
public class Suppliers
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;
    private Product product;
    private Public_Class pub;
    private Supplier supplier;
    private PageURL pageurl;
    private IJsonHelper JsonHelper;
    private ICategory category;
    private IProduct MyPro;

    private Glaer.Trade.B2C.BLL.MEM.ISupplierShop MyShop;

    public Suppliers()
	{
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        JsonHelper = JsonHelperFactory.CreateJsonHelper();
        product = new Product();
        pub = new Public_Class();
        supplier = new Supplier();
        pageurl = new PageURL();
        category = CategoryFactory.CreateCategory();
        MyPro =  ProductFactory.CreateProduct();
        MyShop = Glaer.Trade.B2C.BLL.MEM.SupplierShopFactory.CreateSupplierShop();
	}

    public string SupplierShops()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        string top_keyword = tools.CheckStr(Request["key_word"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_Name", "asc"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "asc"));

        if (top_keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Name", "like", top_keyword));
        }
        IList<SupplierShopInfo> entitys = MyShop.GetSupplierShops(Query);

        StringBuilder strHTML = new StringBuilder();

        IList<SupplierShopInfo> removelist = null;

        strHTML.Append("<div class=\"blk44\"><a name=\"top\"></a><a href=\"#A\">A</a><a href=\"#B\">B</a><a href=\"#C\">C</a><a href=\"#D\">D</a><a href=\"#E\">E</a><a href=\"#F\">F</a><a href=\"#G\">G</a><a href=\"#H\">H</a><a href=\"#I\">I</a><a href=\"#J\">J</a><a href=\"#K\">K</a><a href=\"#L\">L</a><a href=\"#M\">M</a><a href=\"#N\">N</a><a href=\"#O\">O</a><a href=\"#P\">P</a><a href=\"#Q\">Q</a><a href=\"#R\">R</a><a href=\"#S\">S</a><a href=\"#T\">T</a><a href=\"#U\">U</a><a href=\"#V\">V</a><a href=\"#W\">W</a><a href=\"#X\">X</a><a href=\"#Y\">Y</a><a href=\"#Z\">Z</a><a href=\"#other\">(0-9)</a></div>");

        if (entitys != null)
        {
            string[] Items = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            StringBuilder strContent = new StringBuilder();

            foreach (string item in Items)
            {
                strContent = new StringBuilder();
                removelist = new List<SupplierShopInfo>();

                #region 内容输出

                foreach (SupplierShopInfo entity in entitys)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(pub.GetFirstWordLetter(entity.Shop_Name), "^" + item)) continue;

                    removelist.Add(entity);

                    strContent.Append("<li><a href=\"http://" + entity.Shop_Domain + Application["Shop_Second_Domain"] + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\" title=\"" + entity.Shop_Name + "\"  style=\" height: 200px;  width: 200px;margin: 0;\"  /><span title=\"" + entity.Shop_Name + "\" >" +tools.CutStr( entity.Shop_Name,16) + "</span></a></li>");
                   

                }

                #endregion

                foreach (SupplierShopInfo moveentity in removelist) entitys.Remove(moveentity);
                removelist.Clear();
                removelist = null;

                if (strContent.Length > 0)
                {
                    strHTML.Append("<div class=\"blk45\">");
                    strHTML.Append("	  <h2>");
                    strHTML.Append("		  <strong><em><a name=\"" + item + "\">" + item + "</a></em><span>相关供应商</span></strong>");
                    strHTML.Append("	  </h2>");
                    strHTML.Append("	  <div class=\"b45_main02\">");
                    strHTML.Append("			<ul>");
                    strHTML.Append(strContent.ToString());
                    strHTML.Append("			</ul>");
                    strHTML.Append("	  </div>");
                    strHTML.Append("</div>");
                }
            }

            if (entitys.Count > 0)
            {
                strHTML.Append("<div class=\"blk45\">");
                strHTML.Append("	  <h2>");
                strHTML.Append("		  <strong><em><a name=\"other\">(0-9)</a></em><span>相关供应商</span></strong>");
                strHTML.Append("	  </h2>");
                strHTML.Append("	  <div class=\"b45_main02\">");
                strHTML.Append("			<ul>");

                #region 内容输出

                foreach (SupplierShopInfo entity in entitys)
                {
                    //if (entity.Shop_Img)
                    //{
                    strHTML.Append("<li style=\"   margin: 5px 13px;\"><a href=\"http://" + entity.Shop_Domain + Application["Shop_Second_Domain"] + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\" title=\"" + entity.Shop_Name + "\" style=\" height: 200px;  width: 200px;margin: 0;\"  /><span title=\"" + entity.Shop_Name + "\" >" + tools.CutStr(entity.Shop_Name, 16) + "</span></a></li>");
                    //}
                    //else
                    //{
                    //    strHTML.Append("<li style=\"   margin: 5px 13px;\"><a href=\"http://" + entity.Shop_Domain + Application["Shop_Second_Domain"] + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Shop_Img, "fullpath") + "\" title=\"" + entity.Shop_Name + "\" style=\" height: 200px;  width: 200px;margin: 0;\"  /></a></li>");
                    //}
                   

                  
                }

                #endregion

                strHTML.Append("			</ul>");
                strHTML.Append("	  </div>");
                strHTML.Append("</div>");
            }

            entitys = null;
        }

        return strHTML.ToString();
    }

    public string GetSubCateLeft(int ParentID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", ParentID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "ASC"));
        IList<CategoryInfo> Categorys = category.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (Categorys == null)
        {
            return string.Empty;
        }

        StringBuilder strHTML = new StringBuilder();

        int icount = 0;
        foreach (CategoryInfo entity in Categorys)
        {
            icount++;

            strHTML.Append("<li" + (icount % 2 == 1 ? " class=\"bg\"" : "") + "><a href=\"" + pageurl.FormatURL(pageurl.categorylist, pageurl.categoryParam(entity.Cate_ID.ToString(), "", "", "", "")) + "\">" + entity.Cate_Name + "</a></li>");
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 珠宝库显示
    /// </summary>
    /// <param name="TopNum"></param>
    /// <returns></returns>
    public string GetJewelrysLibrary(int TopNum)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = TopNum;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsCoinBuy", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "LEN(ProductInfo.Product_LibraryImg)", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> Products = MyPro.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Products == null)
        {
            return string.Empty;
        }

        StringBuilder strJSON = new StringBuilder(); 

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<ul>\r\n");
        int icount = 0;
        foreach (ProductInfo entity in Products)
        {
            icount++;

            if (icount > 1 && icount % 8 == 0)
                strHTML.Append("</ul><ul>");

            strJSON.Append(",{\"name\":\"<a style='color:#ce1329;' href='" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "' target='_blank'>" + entity.Product_Name + "</a>\",\"note\":\"" + entity.Product_Note + "\",\"price\":\"" + pub.Get_Member_Price(entity.Product_ID, entity.Product_Price).ToString("0.00") + "\",\"id\":\"" + entity.Product_ID + "\",\"libraryimg\":\"" + pub.FormatImgURL(entity.Product_LibraryImg, "fullpath") + "\"}");

            strHTML.Append("	<li><img data=\"" + (icount - 1) + "\" src=\"" + pub.FormatImgURL(entity.Product_LibraryImg, "thumbnail") + "\" /></li>\r\n");
        }
        strHTML.Append("\r\n</ul>\r\n<script type=\"text/javascript\"> Librarys = {\"count\":\"" + Products.Count + "\", \"data\":[" + strJSON.Remove(0, 1).ToString() + "]};</script>");

        return strHTML.ToString();
    }

}