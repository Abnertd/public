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

/// <summary>
///ProductTag 的摘要说明
/// </summary>
public class ProductTag
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductTag MyBLL;
    private IProduct MyProduct;

    public ProductTag()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ProductTagFactory.CreateProductTag();
        MyProduct = ProductFactory.CreateProduct();
    }

    public void AddProductTag()
    {
        int Product_Tag_ID = tools.CheckInt(Request.Form["Product_Tag_ID"]);
        string Product_Tag_Name = tools.CheckStr(Request.Form["Product_Tag_Name"]);
        int Product_Tag_IsSupplier = tools.CheckInt(Request.Form["Product_Tag_IsSupplier"]);
        int Product_Tag_IsActive = tools.CheckInt(Request.Form["Product_Tag_IsActive"]);
        string Product_Tag_Site = Public.GetCurrentSite();

        ProductTagInfo entity = new ProductTagInfo(); 
        entity.Product_Tag_ID = Product_Tag_ID;
        entity.Product_Tag_Name = Product_Tag_Name;
        entity.Product_Tag_IsSupplier = Product_Tag_IsSupplier;
        entity.Product_Tag_IsActive = Product_Tag_IsActive;
        entity.Product_Tag_Site = Product_Tag_Site;

        if (MyBLL.AddProductTag(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditProductTag()
    {
        int Product_Tag_ID = tools.CheckInt(Request.Form["Product_Tag_ID"]);
        string Product_Tag_Name = tools.CheckStr(Request.Form["Product_Tag_Name"]);
        int Product_Tag_IsSupplier = tools.CheckInt(Request.Form["Product_Tag_IsSupplier"]);
        int Product_Tag_IsActive = tools.CheckInt(Request.Form["Product_Tag_IsActive"]);
        string Tag_ProductID = tools.NullStr(Request["favor_productid"]);
        string Product_Tag_Site = Public.GetCurrentSite();

        ProductTagInfo entity = new ProductTagInfo(); 
        entity.Product_Tag_ID = Product_Tag_ID;
        entity.Product_Tag_Name = Product_Tag_Name;
        entity.Product_Tag_IsActive = Product_Tag_IsActive;
        entity.Product_Tag_IsSupplier = Product_Tag_IsSupplier;
        entity.Product_Tag_Site = Product_Tag_Site;

        if (MyBLL.EditProductTag(entity, Public.GetUserPrivilege()))
        {
            MyBLL.AddProductRelateTag(Tag_ProductID, entity.Product_Tag_ID);
            Public.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProductTag() {
        int product_tag_id = tools.CheckInt(Request.QueryString["product_tag_id"]);
        if (MyBLL.DelProductTag(product_tag_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ProductTagInfo GetProductTagByID(int producttag_id)
    {
        return MyBLL.GetProductTagByID(producttag_id, Public.GetUserPrivilege());
    }

    public string GetProductTags()
    {
        string keyword=tools.CheckStr(Request["keyword"]);
        int tag_isactive=tools.CheckInt(Request["tag_isactive"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tag_isactive == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        }
        else if (tag_isactive == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductTagInfo.Product_Tag_IsActive", "=", "0"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductTagInfo> entitys = MyBLL.GetProductTags(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductTagInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ProductTagInfo.Product_Tag_ID\":" + entity.Product_Tag_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Tag_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Tag_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Tag_IsActive == 1) { jsonBuilder.Append("启用");}
                else {jsonBuilder.Append("关闭");}
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"tag_edit.aspx?product_tag_id=" + entity.Product_Tag_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("7b8b58e2-e509-4e6c-a68e-0361225cefa6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('tag_do.aspx?action=move&product_tag_id=" + entity.Product_Tag_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string GetTagProductID(int Tag_ID)
    {
        return MyProduct.GetTagProductID(Tag_ID.ToString());
    }

}
