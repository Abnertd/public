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
    Public_Class pub;

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
        pub = new Public_Class();
    }

    public void AddProductTag()
    {
        int Product_Tag_ID = tools.CheckInt(Request.Form["Product_Tag_ID"]);
        string Product_Tag_Name = tools.CheckStr(Request.Form["Product_Tag_Name"]);
        int Product_Tag_IsSupplier = tools.CheckInt(Request.Form["Product_Tag_IsSupplier"]);
        int Product_Tag_IsActive = tools.CheckInt(Request.Form["Product_Tag_IsActive"]);
        int Product_Tag_Sort = tools.CheckInt(Request.Form["Product_Tag_Sort"]);
        string Product_Tag_Site = pub.GetCurrentSite();

        ProductTagInfo entity = new ProductTagInfo(); 
        entity.Product_Tag_ID = Product_Tag_ID;
        entity.Product_Tag_Name = Product_Tag_Name;
        entity.Product_Tag_IsSupplier = 1;
        entity.Product_Tag_IsActive = Product_Tag_IsActive;
        entity.Product_Tag_Sort = Product_Tag_Sort;
        entity.Product_Tag_Site = Product_Tag_Site;
        entity.Product_Tag_SupplierID = tools.NullInt(Session["supplier_id"]);

        if (MyBLL.AddProductTag(entity, pub.CreateUserPrivilege("2f1d706e-3356-494d-821c-c4173a923328")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditProductTag()
    {
        int Product_Tag_ID = tools.CheckInt(Request.Form["Product_Tag_ID"]);
        string Product_Tag_Name = tools.CheckStr(Request.Form["Product_Tag_Name"]);
        int Product_Tag_IsSupplier = tools.CheckInt(Request.Form["Product_Tag_IsSupplier"]);
        int Product_Tag_IsActive = tools.CheckInt(Request.Form["Product_Tag_IsActive"]);
        int Product_Tag_Sort = tools.CheckInt(Request.Form["Product_Tag_Sort"]);
        //string Tag_ProductID = tools.NullStr(Request["favor_productid"]);
        string Product_Tag_Site = pub.GetCurrentSite();

        ProductTagInfo entity = GetProductTagByID(Product_Tag_ID);
        if (entity == null || entity.Product_Tag_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            entity = null;
            pub.Msg("error", "错误信息", "记录不存在", true, "tag.aspx");
        }
 
        entity.Product_Tag_ID = Product_Tag_ID;
        entity.Product_Tag_Name = Product_Tag_Name;
        entity.Product_Tag_IsActive = Product_Tag_IsActive;
        entity.Product_Tag_IsSupplier = 1;
        entity.Product_Tag_Sort = Product_Tag_Sort;
        entity.Product_Tag_Site = Product_Tag_Site;
        entity.Product_Tag_SupplierID = tools.NullInt(Session["supplier_id"]);

        if (MyBLL.EditProductTag(entity, pub.CreateUserPrivilege("2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f")))
        {
           // MyBLL.AddProductRelateTag(Tag_ProductID, entity.Product_Tag_ID);
            pub.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProductTag() {
        int product_tag_id = tools.CheckInt(Request.QueryString["product_tag_id"]);

        ProductTagInfo tagEntity = GetProductTagByID(product_tag_id);
        if (tagEntity == null || tagEntity.Product_Tag_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            tagEntity = null;
            pub.Msg("error", "错误信息", "记录不存在", true, "tag.aspx");
        }
        tagEntity = null;

        if (MyBLL.DelProductTag(product_tag_id, pub.CreateUserPrivilege("7b8b58e2-e509-4e6c-a68e-0361225cefa6")) > 0)
        {
            MyBLL.AddProductRelateTag("", product_tag_id);

            pub.Msg("positive", "操作成功", "操作成功", true, "tag.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ProductTagInfo GetProductTagByID(int producttag_id)
    {
        return MyBLL.GetProductTagByID(producttag_id, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
    }

    public void GetProductTags()
    {
        string keyword=tools.CheckStr(Request["keyword"]);
        int tag_isactive=tools.CheckInt(Request["tag_isactive"]);
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
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
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductTagInfo.Product_Tag_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_ID", "DESC"));

        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"973\" cellpadding=\"0\" align=\"center\" cellspacing=\"2\" class=\"table02\" >");
        Response.Write("<tr>");
        Response.Write("  <td width=\"300\" class=\"name\">标签名称</td>");
        Response.Write("  <td width=\"223\"  class=\"name\">启用</td>");
        Response.Write("  <td width=\"225\"  class=\"name\">排序</td>");
        Response.Write("  <td width=\"225\"  class=\"name\">操作</td>");
        Response.Write("</tr>");

        IList<ProductTagInfo> entitys = MyBLL.GetProductTags(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (entitys != null)
        {
            foreach (ProductTagInfo entity in entitys)
            {
                i++;

                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\" >");
                }
                else
                {
                    Response.Write("<tr>");
                }
                
                Response.Write("<td>" + entity.Product_Tag_Name + "</td>");

                Response.Write("<td>");
                if (entity.Product_Tag_IsActive == 1)
                {
                    Response.Write("启用");
                }
                else
                {
                    Response.Write("关闭");
                }
                Response.Write("</td>");

                Response.Write("<td>");
                Response.Write(entity.Product_Tag_Sort);
                Response.Write("</td>");

                Response.Write("<td>");
                Response.Write("<span><a href=\"/supplier/tag_edit.aspx?product_tag_id=" + entity.Product_Tag_ID + "\" class=\"a12\">修改</a></span>");
                Response.Write(" <span><a href=\"/supplier/tag_do.aspx?action=del&product_tag_id=" + entity.Product_Tag_ID + "\">删除</a></span>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<table width=\"973\" cellpadding=\"0\" align=\"center\" cellspacing=\"2\" class=\"table02\" >");
            Response.Write("<tr><td  colspan=\"4\" align=\"center\" >没有记录</td></tr>");
            Response.Write("</table>");
        }
    }

    public string GetTagProductID(int Tag_ID)
    {
        return MyProduct.GetTagProductID(Tag_ID.ToString());
    }

}
