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
///Category 的摘要说明
/// </summary>
public class Category {
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ICategory MyBLL;
    private IProductType MyTBLL;

    public Category() {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = CategoryFactory.CreateCategory();
        MyTBLL = ProductTypeFactory.CreateProductType();
    }

    public void AddCategory() {
        int Cate_ParentID = tools.CheckInt(Request.Form["Cate_ParentID"]);
        string Cate_Name = tools.CheckStr(Request.Form["Cate_Name"]);
        int Cate_TypeID = tools.CheckInt(Request.Form["Cate_TypeID"]);
        string Cate_Img = tools.CheckStr(Request.Form["Cate_Img"]);
        int Cate_ProductTypeID = 0;
        int Cate_Sort = tools.CheckInt(Request.Form["Cate_Sort"]);
        int Cate_IsFrequently = tools.CheckInt(Request.Form["Cate_IsFrequently"]);
        int Cate_IsActive = tools.CheckInt(Request.Form["Cate_IsActive"]);
        int Cate_Count_Brand = 0;
        int Cate_Count_Product = 0;
        string Cate_SEO_Path = tools.CheckStr(Request.Form["Cate_SEO_Path"]);
        string Cate_SEO_Title = tools.CheckStr(Request.Form["Cate_SEO_Title"]);
        string Cate_SEO_Keyword = tools.CheckStr(Request.Form["Cate_SEO_Keyword"]);
        string Cate_SEO_Description = tools.CheckStr(Request.Form["Cate_SEO_Description"]);
        string Cate_Site = Public.GetCurrentSite();

        if (Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称！", false, "{back}");
        }

        CategoryInfo entity = new CategoryInfo();
        entity.Cate_ParentID = Cate_ParentID;
        entity.Cate_Name = Cate_Name;
        entity.Cate_TypeID = Cate_TypeID;
        entity.Cate_Img = Cate_Img;
        entity.Cate_ProductTypeID = Cate_ProductTypeID;
        entity.Cate_Sort = Cate_Sort;
        entity.Cate_IsFrequently = Cate_IsFrequently;
        entity.Cate_IsActive = Cate_IsActive;
        entity.Cate_Count_Product = Cate_Count_Product;
        entity.Cate_Count_Brand = Cate_Count_Brand;
        entity.Cate_SEO_Path = Cate_SEO_Path;
        entity.Cate_SEO_Title = Cate_SEO_Title;
        entity.Cate_SEO_Keyword = Cate_SEO_Keyword;
        entity.Cate_SEO_Description = Cate_SEO_Description;
        entity.Cate_Site = Cate_Site;

        if (MyBLL.AddCategory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "category_add.aspx?parent_id=" + entity.Cate_ParentID);
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditCategory() {
        int Cate_ID = tools.CheckInt(Request.Form["Cate_ID"]);
        int Cate_ParentID = tools.CheckInt(Request.Form["Cate_ParentID"]);
        string Cate_Name = tools.CheckStr(Request.Form["Cate_Name"]);
        int Cate_TypeID = tools.CheckInt(Request.Form["Cate_TypeID"]);
        string Cate_Img = tools.CheckStr(Request.Form["Cate_Img"]);
        int Cate_ProductTypeID = 0;
        int Cate_Sort = tools.CheckInt(Request.Form["Cate_Sort"]);
        int Cate_IsFrequently = tools.CheckInt(Request.Form["Cate_IsFrequently"]);
        int Cate_IsActive = tools.CheckInt(Request.Form["Cate_IsActive"]);
        int Cate_Count_Brand = 0;
        int Cate_Count_Product = 0;
        string Cate_SEO_Path = tools.CheckStr(Request.Form["Cate_SEO_Path"]);
        string Cate_SEO_Title = tools.CheckStr(Request.Form["Cate_SEO_Title"]);
        string Cate_SEO_Keyword = tools.CheckStr(Request.Form["Cate_SEO_Keyword"]);
        string Cate_SEO_Description = tools.CheckStr(Request.Form["Cate_SEO_Description"]);
        string Cate_Site = Public.GetCurrentSite();

        if (Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称！", false, "{back}");
        }

        CategoryInfo entity = new CategoryInfo();
        entity.Cate_ID = Cate_ID;
        entity.Cate_ParentID = Cate_ParentID;
        entity.Cate_Name = Cate_Name;
        entity.Cate_TypeID = Cate_TypeID;
        entity.Cate_Img = Cate_Img;
        entity.Cate_ProductTypeID = Cate_ProductTypeID;
        entity.Cate_Sort = Cate_Sort;
        entity.Cate_IsFrequently = Cate_IsFrequently;
        entity.Cate_IsActive = Cate_IsActive;
        entity.Cate_Count_Product = Cate_Count_Product;
        entity.Cate_Count_Brand = Cate_Count_Brand;
        entity.Cate_SEO_Path = Cate_SEO_Path;
        entity.Cate_SEO_Title = Cate_SEO_Title;
        entity.Cate_SEO_Keyword = Cate_SEO_Keyword;
        entity.Cate_SEO_Description = Cate_SEO_Description;
        entity.Cate_Site = Cate_Site;

        if (MyBLL.EditCategory(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "category.aspx?cate_parentid=" + Cate_ParentID);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelCategory() {
        int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);

        CategoryInfo entity = GetCategoryByID(cate_id);
        if (entity == null)
            Public.Msg("error", "错误信息", "该类别不存在", false, "{back}");

        if (MyBLL.DelCategory(cate_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "category.aspx?cate_parentid=" + entity.Cate_ParentID);
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public CategoryInfo GetCategoryByID(int cate_id) {
        return MyBLL.GetCategoryByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetCategorys()
    {

        int Cate_ParentID = tools.CheckInt(Request.QueryString["cate_parentid"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Name", "like", keyword));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Cate_ParentID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<CategoryInfo> Categorys = MyBLL.GetCategorys(Query, Public.GetUserPrivilege());
        if (Categorys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (CategoryInfo entity in Categorys)
            {
                jsonBuilder.Append("{\"CategoryInfo.Cate_ID\":" + entity.Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href='category.aspx?cate_parentid=" + entity.Cate_ID + "'>" + Public.JsonStr(entity.Cate_Name) + "</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetSubCateAmount(entity.Cate_ID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("2dcee4f1-71e1-4cbd-afa3-470f0b554fd0"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"category_edit.aspx?cate_id=" + entity.Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("14d9fa43-7e21-4eed-8955-39fafce6f185"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('category_do.aspx?action=move&cate_id=" + entity.Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public int GetSubCateAmount(int Cate_ParentID)
    {
        int Amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Cate_ParentID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));

        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "asc"));

        IList<CategoryInfo> Categorys = MyBLL.GetCategorys(Query, Public.GetUserPrivilege());
        if (Categorys != null)
        {
            Amount = Categorys.Count;
        }
        return Amount;
    }

    public string SelectCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID) {
        return MyBLL.SelectCategoryOption(Cate_ID, selectVal, appendTag, shieldID, Public.GetCurrentSite(), Public.GetUserPrivilege());
    }

    public string CategoryRecursion(int cate_id) {
        string strCateRecursion = MyBLL.DisplayCategoryRecursion(cate_id, "/product/category.aspx?cate_parentid={cate_id}", Public.GetUserPrivilege());
        //if (strCateRecursion.Length > 0)
        //    strCateRecursion = "&nbsp;&gt;&nbsp;" + strCateRecursion;

        return strCateRecursion;
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

}
