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

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///ProductType 的摘要说明
/// </summary>
public class ProductType
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductType MyProductType;
    private IBrand MyBrand;

    public ProductType() {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyProductType = ProductTypeFactory.CreateProductType();
        MyBrand = BrandFactory.CreateBrand();
    }

    //添加产品类型
    public void AddProductType()
    {
        int ProductType_ID = tools.CheckInt(Request.Form["ProductType_ID"]);
        string ProductType_Name = tools.CheckStr(Request.Form["ProductType_Name"]);
        int ProductType_Sort = tools.CheckInt(Request.Form["ProductType_Sort"]);
        int ProductType_IsActive = tools.CheckInt(Request.Form["ProductType_IsActive"]);
        string ProductType_Site = Public.GetCurrentSite();
        string brand_select = tools.CheckStr(Request.Form["ProductType_Brand"]) + ",";
        string[] Product_Brand = brand_select.Split(',');

        if (ProductType_Name == "") 
        {
            Public.Msg("error", "错误信息", "请填写类型名称！", false, "{back}");
        }


        ProductTypeInfo entity = new ProductTypeInfo();
        ProductTypeInfo MaxType = new ProductTypeInfo();
        entity.ProductType_ID = ProductType_ID;
        entity.ProductType_Name = ProductType_Name;
        entity.ProductType_Sort = ProductType_Sort;
        entity.ProductType_IsActive = ProductType_IsActive;
        entity.ProductType_Site = ProductType_Site;

        if (MyProductType.AddProductType(entity, Public.GetUserPrivilege()))
        {
            MaxType = MyProductType.GetProductTypeMax(Public.GetUserPrivilege());
            if (MaxType != null)
            {
                foreach (string i in Product_Brand)
                {
                    if(tools.CheckInt(i)>0)
                    {
                        MyProductType.AddProductType_Brand(MaxType.ProductType_ID, tools.CheckInt(i), Public.GetUserPrivilege());
                    }
                }
            }
            
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductType_Add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改产品类型
    public void EditProductType()
    {

        int ProductType_ID = tools.CheckInt(Request.Form["ProductType_ID"]);
        string ProductType_Name = tools.CheckStr(Request.Form["ProductType_Name"]);
        int ProductType_Sort = tools.CheckInt(Request.Form["ProductType_Sort"]);
        int ProductType_IsActive = tools.CheckInt(Request.Form["ProductType_IsActive"]);
        string ProductType_Site = Public.GetCurrentSite();
        string brand_select = tools.CheckStr(Request.Form["ProductType_Brand"]) + ",";
        string[] Product_Brand = brand_select.Split(',');

        if (ProductType_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写类型名称！", false, "{back}");
        }

        ProductTypeInfo entity = new ProductTypeInfo();
        entity.ProductType_ID = ProductType_ID;
        entity.ProductType_Name = ProductType_Name;
        entity.ProductType_Sort = ProductType_Sort;
        entity.ProductType_IsActive = ProductType_IsActive;
        entity.ProductType_Site = ProductType_Site;


        if (MyProductType.EditProductType(entity, Public.GetUserPrivilege()))
        {
            MyProductType.DelProductType_Brand(entity.ProductType_ID, Public.GetUserPrivilege());
            foreach (string i in Product_Brand)
            {
                if (tools.CheckInt(i) > 0)
                {
                    MyProductType.AddProductType_Brand(entity.ProductType_ID, tools.CheckInt(i), Public.GetUserPrivilege());
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductType.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除产品类型
    public void DelProductType()
    {
        int producttype_id = tools.CheckInt(Request.QueryString["producttype_id"]);
        if (MyProductType.DelProductType(producttype_id, Public.GetUserPrivilege()) > 0)
        {
            MyProductType.DelProductType_Brand(producttype_id, Public.GetUserPrivilege());

            MyProductType.DelProductType_Extend(producttype_id, Public.GetUserPrivilege());
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductType.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取产品类型
    public ProductTypeInfo GetProductTypeByID(int cate_id)
    {
        return MyProductType.GetProductTypeByID(cate_id, Public.GetUserPrivilege());
    }

    //获取产品类型列表
    public string GetProductTypes() {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyProductType.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductTypeInfo> ProductTypes = MyProductType.GetProductTypes(Query, Public.GetUserPrivilege());
        if (ProductTypes != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductTypeInfo entity in ProductTypes)
            {
                jsonBuilder.Append("{\"ProductType.ProductType_ID\":" + entity.ProductType_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ProductType_ID );
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<a href=\\\"producttypeExtend.aspx?producttype_id=" + entity.ProductType_ID + "\\\">");
                jsonBuilder.Append(entity.ProductType_Name );
                jsonBuilder.Append("</a>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ProductType_Sort );
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("847e8136-fd2f-4834-86b7-f2c984705eff"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_set.gif\\\"> <a href=\\\"producttypeExtend.aspx?producttype_id=" + entity.ProductType_ID + "\\\" title=\\\"设置属性\\\">设置属性</a> <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"producttype_edit.aspx?producttype_id=" + entity.ProductType_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("fcc7d1f7-e2f5-440f-a827-2e53e6e62184"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('producttype_do.aspx?action=move&producttype_id=" + entity.ProductType_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //获取产品类型列表
    public string GetProductTypesByBrandID(int Brand_ID)
    {
        string html = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductType.ProductType_ID", "in", "select ProductType_Brand_ProductTypeID from ProductType_Brand where ProductType_Brand_BrandID="+Brand_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductType.ProductType_ID", "Desc"));
        IList<ProductTypeInfo> ProductTypes = MyProductType.GetProductTypes(Query, Public.GetUserPrivilege());
        if (ProductTypes != null)
        {
            foreach (ProductTypeInfo entity in ProductTypes)
            {
                html += "<a href=\"ProductType_Edit.aspx?ProductType_ID="+entity.ProductType_ID+"\">" + entity.ProductType_Name + "</a> &nbsp; ";
            }
        }
        return html;
    }

    //产品类型品牌选择
    public string ProductType_BrandSelect_BAK(IList<BrandInfo> ProductType_Brands) {
        string brand_select;
        brand_select="";
        bool Is_target=false;
        IList<BrandInfo> Brands=new List<BrandInfo>();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "asc"));
        Brands = MyBrand.GetBrands(Query, Public.GetUserPrivilege());

        if (Brands != null)
        {
            foreach (BrandInfo entity in Brands)
            {
                Is_target = false;
                if (ProductType_Brands != null)
                {
                    foreach (BrandInfo producttype_brand in ProductType_Brands)
                    {
                        if (producttype_brand.Brand_ID == entity.Brand_ID)
                        {
                            Is_target = true;
                        }
                        
                    }
                    if (Is_target==true)
                    {
                        brand_select = brand_select + " <input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\" checked> " + entity.Brand_Name;
                    }
                    else
                    {
                        brand_select = brand_select + " <input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\"> " + entity.Brand_Name;
                    }
                }
                else
                {
                    brand_select = brand_select + " <input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\"> " + entity.Brand_Name;
                }
            }
        }
        return brand_select;
        
    }

    public string ProductType_BrandSelect(IList<BrandInfo> ProductType_Brands)
    {
        bool Is_target = false;
        int ICount = 1;
        IList<BrandInfo> Brands = new List<BrandInfo>();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Name", "asc"));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_Sort", "asc"));
        Brands = MyBrand.GetBrands(Query, Public.GetUserPrivilege());

        StringBuilder strHTML = new StringBuilder();

        IList<BrandInfo> removelist = null;

        strHTML.Append("<div class=\"selecttit\"><input id=\"chk_allbrand\" name=\"chk_allbrand\" type=\"checkbox\" onclick=\"select_AllBrand();\"  /><a name=\"top\"></a><a href=\"#A\">A</a><a href=\"#B\">B</a><a href=\"#C\">C</a><a href=\"#D\">D</a><a href=\"#E\">E</a><a href=\"#F\">F</a><a href=\"#G\">G</a><a href=\"#H\">H</a><a href=\"#I\">I</a><a href=\"#J\">J</a><a href=\"#K\">K</a><a href=\"#L\">L</a><a href=\"#M\">M</a><a href=\"#N\">N</a><a href=\"#O\">O</a><a href=\"#P\">P</a><a href=\"#Q\">Q</a><a href=\"#R\">R</a><a href=\"#S\">S</a><a href=\"#T\">T</a><a href=\"#U\">U</a><a href=\"#V\">V</a><a href=\"#W\">W</a><a href=\"#X\">X</a><a href=\"#Y\">Y</a><a href=\"#Z\">Z</a><a href=\"#other\">其他</a></div>");

        if (Brands != null)
        {
            string[] Items = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            foreach (string item in Items)
            {
                strHTML.Append("<dl class=\"selectcnt\">");
                strHTML.Append("    <dt><a name=\"" + item + "\">" + item + "</a><a href=\"#top\" class=\"backtop\">返回顶部</a></dt>");
                strHTML.Append("    <dd><ul>");

                removelist = new List<BrandInfo>();

                #region 内容输出

                foreach (BrandInfo entity in Brands)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(Public.GetFirstWordLetter(entity.Brand_Name), "^" + item)) continue;

                    removelist.Add(entity);

                    #region 判断是否选中

                    Is_target = false;
                    if (ProductType_Brands != null)
                    {
                        foreach (BrandInfo producttype_brand in ProductType_Brands)
                        {
                            if (producttype_brand.Brand_ID == entity.Brand_ID)
                            {
                                Is_target = true;
                                break;
                            }
                        }
                    }

                    #endregion

                    if (Is_target == true)
                    {
                        strHTML.Append("<li><input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\" checked> " + entity.Brand_Name + "</li>");
                    }
                    else
                    {
                        strHTML.Append("<li><input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\"> " + entity.Brand_Name + "</li>");
                    }

                }

                #endregion

                foreach (BrandInfo moveentity in removelist) Brands.Remove(moveentity);
                removelist.Clear();
                removelist = null;

                strHTML.Append("</ul><div class=\"claerfloat\"></div></dd>");
                strHTML.Append("</dl>");
            }

            if (Brands.Count > 0)
            {
                strHTML.Append("<dl class=\"selectcnt\">");
                strHTML.Append("    <dt><a name=\"other\">其他</a><a href=\"#top\" class=\"backtop\">返回顶部</a></dt>");
                strHTML.Append("    <dd><ul>");

                #region 内容输出

                foreach (BrandInfo entity in Brands)
                {
                    #region 判断是否选中

                    Is_target = false;
                    if (ProductType_Brands != null)
                    {
                        foreach (BrandInfo producttype_brand in ProductType_Brands)
                        {
                            if (producttype_brand.Brand_ID == entity.Brand_ID)
                            {
                                Is_target = true;
                                break;
                            }
                        }
                    }

                    #endregion

                    if (Is_target == true)
                    {
                        strHTML.Append("<li><input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\" checked> " + entity.Brand_Name + "</li>");
                    }
                    else
                    {
                        strHTML.Append("<li><input type=\"checkbox\" name=\"ProductType_Brand\" value=\"" + entity.Brand_ID + "\"> " + entity.Brand_Name + "</li>");
                    }
                }

                #endregion

                strHTML.Append("</ul><div class=\"claerfloat\"></div></dd>");
                strHTML.Append("</dl>");
            }

        }

        return strHTML.ToString();
    }

}
