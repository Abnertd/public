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
///Brand 的摘要说明
/// </summary>
public class Brand
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IBrand brand;
    private IProductType MyProductType;

    public Brand()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        brand = BrandFactory.CreateBrand();
        MyProductType = ProductTypeFactory.CreateProductType();
    }

    //品牌添加
    public void AddBrand(){
        int Brand_ID = 0;
        string Brand_Name = tools.CheckStr(Request.Form["Brand_Name"].Trim());
        string Brand_Img = Request.Form["Brand_Img"];
        string Brand_Url = Request.Form["Brand_Url"].Trim();
        string Brand_Description = Request.Form["Brand_Description"];
        int Brand_Sort =tools.CheckInt(Request.Form["Brand_Sort"]);
        int Brand_IsRecommend = tools.CheckInt(Request.Form["Brand_IsRecommend"]);
        int Brand_IsActive = tools.CheckInt(Request.Form["Brand_IsActive"]);
        string Brand_Site = Public.GetCurrentSite();

        if (Brand_Name.Length == 0) 
        {
            Public.Msg("error", "错误信息", "请填写品牌名称！", false, "{back}");
        }

        BrandInfo entity=new BrandInfo();
        entity.Brand_ID = Brand_ID;
        entity.Brand_Name = Brand_Name;
        entity.Brand_Img = Brand_Img;
        entity.Brand_URL = Brand_Url;
        entity.Brand_Description = Brand_Description;
        entity.Brand_Sort = Brand_Sort;
        entity.Brand_IsRecommend = Brand_IsRecommend;
        entity.Brand_IsActive = Brand_IsActive;
        entity.Brand_Site = Brand_Site;

        if (brand.AddBrand(entity, Public.GetUserPrivilege()))
        {
            entity = GetLastBrandInfo();
            if (entity != null)
            {
                if (entity.Brand_Name == Brand_Name)
                {
                    string ProductTypeID = tools.CheckStr(Request["ProductTypeID"]);
                    if (ProductTypeID.Length > 0)
                    {
                        foreach (string typeid in ProductTypeID.Split(','))
                        {
                            if (tools.CheckInt(typeid) > 0)
                            {
                                MyProductType.AddProductType_Brand(tools.CheckInt(typeid), entity.Brand_ID, Public.GetUserPrivilege());
                            }
                        }
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "brand_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //品牌修改
    public void EditBrand()
    {
        int Brand_ID = tools.CheckInt(Request.Form["Brand_ID"]);
        string Brand_Name = tools.CheckStr(Request.Form["Brand_Name"].Trim());
        string Brand_Img = Request.Form["Brand_Img"];
        string Brand_Url = Request.Form["Brand_Url"].Trim();
        string Brand_Description = Request.Form["Brand_Description"];
        int Brand_Sort = tools.CheckInt(Request.Form["Brand_Sort"]);
        int Brand_IsRecommend = tools.CheckInt(Request.Form["Brand_IsRecommend"]);
        int Brand_IsActive = tools.CheckInt(Request.Form["Brand_IsActive"]);
        string Brand_Site = Public.GetCurrentSite();

        if (Brand_ID == 0)
        {
            Public.Msg("error", "错误信息", "请选择您要修改的商品品牌！", false, "{back}");
        }

        if (Brand_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写品牌名称！", false, "{back}");
        }

        BrandInfo entity = new BrandInfo();
        entity.Brand_ID = Brand_ID;
        entity.Brand_Name = Brand_Name;
        entity.Brand_Img = Brand_Img;
        entity.Brand_URL = Brand_Url;
        entity.Brand_Description = Brand_Description;
        entity.Brand_Sort = Brand_Sort;
        entity.Brand_IsRecommend = Brand_IsRecommend;
        entity.Brand_IsActive = Brand_IsActive;
        entity.Brand_Site = Brand_Site;

        if (brand.EditBrand(entity, Public.GetUserPrivilege()))
        {
            string ProductTypeID = tools.CheckStr(Request["ProductTypeID"]);
            MyProductType.DelProductType_Brand(0, Brand_ID, Public.GetUserPrivilege());
            if (ProductTypeID.Length > 0)
            {
                foreach (string typeid in ProductTypeID.Split(','))
                {
                    if (tools.CheckInt(typeid) > 0)
                    {
                        MyProductType.AddProductType_Brand(tools.CheckInt(typeid), Brand_ID, Public.GetUserPrivilege());
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "brand.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //最新品牌
    public BrandInfo GetLastBrandInfo()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage =1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_ID", "Desc"));
        IList<BrandInfo> Brands = brand.GetBrands(Query, Public.GetUserPrivilege());
        if (Brands != null)
        {
            return Brands[0];
        }
        else
        {
            return null;
        }
    }

    //品牌删除
    public void DelBrand()
    {
        int Brand_ID = tools.CheckInt(Request["brand_id"]);

        if (brand.DelBrand(Brand_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "brand.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            
        }
    }

    //根据ID获取品牌信息
    public BrandInfo GetBrandByID(int Brand_ID) {

        return brand.GetBrandByID(Brand_ID, Public.GetUserPrivilege());
    }

    //获取品牌信息列表
    public string GetBrands()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Name", "like", keyword)); 
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = brand.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<BrandInfo> Brands = brand.GetBrands(Query, Public.GetUserPrivilege());
        if (Brands != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (BrandInfo entity in Brands)
            {
                jsonBuilder.Append("{\"BrandInfo.Brand_ID\":" + entity.Brand_ID  + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Brand_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Brand_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Brand_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("9592b436-454a-42cf-83f4-0d9ce83c339a"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"brand_edit.aspx?brand_id=" + entity.Brand_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("3297a5d3-44e6-4318-aa23-4d31288a291b"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('brand_do.aspx?action=move&brand_id=" + entity.Brand_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //获取品牌相关产品类型
    public IList<ProductTypeInfo> GetBrandRelateProductType(int brand_id)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductType.ProductType_ID", "in", "select ProductType_Brand_ProductTypeID from ProductType_Brand where ProductType_Brand_BrandID="+brand_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductType.ProductType_ID", "Desc"));
        return  MyProductType.GetProductTypes(Query, Public.GetUserPrivilege());
    }

    //产品类型选择
    public string BrandProductType_Select(int brand_id)
    {
        string html = "";
        bool iscontain=false;
        IList<ProductTypeInfo> types = null;
        if (brand_id > 0)
        {
            types = GetBrandRelateProductType(brand_id);
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ProductType.ProductType_ID", "Desc"));
        IList<ProductTypeInfo> ProductTypes = MyProductType.GetProductTypes(Query, Public.GetUserPrivilege());
        if (ProductTypes != null)
        {
            foreach (ProductTypeInfo entity in ProductTypes)
            {
                iscontain=false;
                if (types != null)
                {
                    foreach (ProductTypeInfo typeinfo in types)
                    {
                        if (typeinfo.ProductType_ID == entity.ProductType_ID)
                        {
                            iscontain = true;
                            html += "<input type=\"checkbox\" name=\"ProductTypeID\" value=\"" + entity.ProductType_ID + "\" checked> " + entity.ProductType_Name + " &nbsp; ";
                            break;
                        }
                    }
                    if (iscontain == false)
                    {
                        html += "<input type=\"checkbox\" name=\"ProductTypeID\" value=\"" + entity.ProductType_ID + "\"> " + entity.ProductType_Name + " &nbsp; ";
                    }
                }
                else
                {
                    html += "<input type=\"checkbox\" name=\"ProductTypeID\" value=\"" + entity.ProductType_ID + "\"> " + entity.ProductType_Name + " &nbsp; ";
                }
            }
        }
        return html;
    }

    
}
