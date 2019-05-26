using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
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
///Package 的摘要说明
/// </summary>
public class Package
{

    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IPackage MyBLL;
    private IProduct MyBLLPRO;
    private Product MyProduct;
    Public_Class pub;

    public Package()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = PackageFactory.CreatePackage();
        MyBLLPRO = ProductFactory.CreateProduct();
        MyProduct = new Product();
        pub = new Public_Class();
    }

    public void AddPackage()
    {
        int Package_ID = tools.CheckInt(Request.Form["Package_ID"]);
        string Package_Name = tools.CheckStr(Request.Form["Package_Name"]);
        int Package_IsInsale = tools.CheckInt(Request.Form["Package_IsInsale"]);
        int Package_StockAmount = tools.CheckInt(Request.Form["Package_StockAmount"]);
        int Package_Weight = tools.CheckInt(Request.Form["Package_Weight"]);
        double Package_Price = tools.CheckFloat(Request.Form["Package_Price"]);
        int Package_Sort = tools.CheckInt(Request.Form["Package_Sort"]);

        PackageInfo entity = new PackageInfo();
        entity.Package_ID = Package_ID;
        entity.Package_Name = Package_Name;
        entity.Package_IsInsale = Package_IsInsale;
        entity.Package_StockAmount = Package_StockAmount;
        entity.Package_Weight = Package_Weight;
        entity.Package_Price = Package_Price;
        entity.Package_Sort = Package_Sort;
        entity.Package_Addtime = DateTime.Now;
        entity.Package_Site = pub.GetCurrentSite();
        entity.PackageProductInfos = ReadPackageProduct();
        entity.Package_SupplierID = tools.NullInt(Session["supplier_id"]);

        if (MyBLL.AddPackage(entity, pub.CreateUserPrivilege("573393a4-573e-4872-ad7b-b77d75e0f611")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Package_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public IList<PackageProductInfo> ReadPackageProduct()
    {
        string product_id = tools.CheckStr(Request.Form["product_id"]);
        string product_amount = tools.CheckStr(Request.Form["product_amount"]);
        string[] ProductIDArr = product_id.Split(',');
        string[] ProductAmountArr = product_amount.Split(',');

        int icount = 0;

        IList<PackageProductInfo> entityList = new List<PackageProductInfo>();
        PackageProductInfo product = null;
        foreach (string pid in ProductIDArr)
        {
            product = new PackageProductInfo();
            product.Package_Product_ProductID = int.Parse(pid);
            product.Package_Product_Amount = int.Parse(ProductAmountArr[icount]);
            entityList.Add(product);
            icount++;
        }

        if (entityList.Count == 0) { entityList = null; }

        return entityList;
    }

    public void EditPackage()
    {
        int Package_ID = tools.CheckInt(Request.Form["Package_ID"]);
        string Package_Name = tools.CheckStr(Request.Form["Package_Name"]);
        int Package_IsInsale = tools.CheckInt(Request.Form["Package_IsInsale"]);
        int Package_StockAmount = tools.CheckInt(Request.Form["Package_StockAmount"]);
        int Package_Weight = tools.CheckInt(Request.Form["Package_Weight"]);
        double Package_Price = tools.CheckFloat(Request.Form["Package_Price"]);
        int Package_Sort = tools.CheckInt(Request.Form["Package_Sort"]);

        PackageInfo entity = GetPackageByID(Package_ID);
        if (entity == null || entity.Package_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            entity = null;
            pub.Msg("error", "错误信息", "记录不存在", true, "package_list.aspx");
        }

        entity.Package_ID = Package_ID;
        entity.Package_Name = Package_Name;
        entity.Package_IsInsale = Package_IsInsale;
        entity.Package_StockAmount = Package_StockAmount;
        entity.Package_Weight = Package_Weight;
        entity.Package_Price = Package_Price;
        entity.Package_Sort = Package_Sort;
        entity.Package_Addtime = DateTime.Now;
        entity.Package_Site = pub.GetCurrentSite();
        entity.PackageProductInfos = ReadPackageProduct();
        entity.Package_SupplierID = tools.NullInt(Session["supplier_id"]);

        if (MyBLL.EditPackage(entity, pub.CreateUserPrivilege("5666872b-2113-490b-a41f-a7a65083324a")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Package_list.aspx");
        }
        else {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelPackage()
    {
        int Package_ID = tools.CheckInt(Request.QueryString["Package_ID"]);

        PackageInfo tagEntity = GetPackageByID(Package_ID);
        if (tagEntity == null || tagEntity.Package_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            tagEntity = null;
            pub.Msg("error", "错误信息", "记录不存在", true, "package_list.aspx");
        }
        tagEntity = null;

        if (MyBLL.DelPackage(Package_ID, pub.CreateUserPrivilege("fc830fc1-1192-4097-9d92-e625a707cbc1")) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "package_list.aspx");
        }
        else {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public PackageInfo GetPackageByID(int cate_id)
    {
        return MyBLL.GetPackageByID(cate_id, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
    }

    public void GetPackages()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int isinsale = tools.CheckInt(Request["isinsale"]);
        string PageUrl = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 15;
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Query.CurrentPage < 1) Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PackageInfo.Package_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));

        if (isinsale == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PackageInfo.Package_IsInsale", "=", "1"));
        }
        else if (isinsale == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PackageInfo.Package_IsInsale", "=", "0"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo("PackageInfo.Package_ID", "DESC"));

        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">名称</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">捆绑价格</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">重量</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">销售状态</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">添加时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));

        IList<PackageInfo> entitys = MyBLL.GetPackages(Query, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
        if (entitys != null)
        {
            foreach (PackageInfo entity in entitys)
            {
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + entity.Package_Name + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + pub.FormatCurrency(entity.Package_Price) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + entity.Package_Weight + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                if (entity.Package_IsInsale == 1)
                {
                    Response.Write("上架");
                }
                else
                {
                    Response.Write("下架");
                }
                Response.Write("</td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">" + entity.Package_Addtime + "</td>");

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                Response.Write("<a href=\"/supplier/package_edit.aspx?package_id=" + entity.Package_ID + "\">修改</a>");
                Response.Write(" <a href=\"/supplier/package_do.aspx?action=move&package_id=" + entity.Package_ID + "\">删除</a>");
                Response.Write("</td>");
                Response.Write("</tr>");

            }

            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, PageUrl, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"8\" align=\"center\">没有记录</td></tr>");
            Response.Write("</table>");
        }

    }

    public string SelectProduct()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int product_cate = tools.CheckInt(Request["product_cate"]);
        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

        IList<PackageProductInfo> entityList = (IList<PackageProductInfo>)Session["PackageProductInfo"];
        string productSelected = "0";

        foreach (PackageProductInfo ppinfo in entityList) {
            productSelected += "," + ppinfo.Package_Product_ProductID.ToString();
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.U_Product_Shipper", "=", "0"));

        if (product_cate > 0)
        {
            string subCates = MyProduct.Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
            //string product_idstr = MyProduct.Get_All_CateProductID(subCates);
            
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "IN", product_idstr));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "like", keyword));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));

        if (productSelected.Length>0)
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productSelected));

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));

        IList<ProductInfo> entitys = null;
        if (Query.ParamInfos.Count > 5)
        {
            entitys = MyBLLPRO.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        }

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<form method=\"post\"  id=\"frmadd\" name=\"frmadd\">");
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"30\"></td>");
        jsonBuilder.Append("        <td width=\"80\">库存</td>");
        jsonBuilder.Append("        <td>商品编码</td>");
        jsonBuilder.Append("        <td>商品名称</td>");
        jsonBuilder.Append("        <td>规格</td>");
        jsonBuilder.Append("        <td>生产企业</td>");
        jsonBuilder.Append("        <td>本站价格</td>");
        jsonBuilder.Append("    </tr>");

        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"product_id\" name=\"product_id\" value=\"" + entity.Product_ID + "\" /></td>");
                jsonBuilder.Append("        <td>" + entity.Product_SaleAmount + "</td>");
                jsonBuilder.Append("        <td>" + entity.Product_Code + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                jsonBuilder.Append("    </tr>");
            }
            entitys = null;
        }
        else
        {
            jsonBuilder.Append("<tr class=\"list_td_bg\"><td colspan=\"7\">选择分类或输入关键词搜索</td></tr>");
        }
        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
        jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
        jsonBuilder.Append("        <td colspan=\"6\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:product_add('product_id');\" /></td>");
        jsonBuilder.Append("    </tr>");
        jsonBuilder.Append("</table>");
        jsonBuilder.Append("</form>");

        return jsonBuilder.ToString();
    }

    public string ShowProduct() 
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"><input type=\"button\" value=\"添加\" onclick=\"SelectProduct()\" class=\"bt_orange\"></td>");
        jsonBuilder.Append("        <td width=\"100\">捆绑数量</td>");
        jsonBuilder.Append("        <td>商品编码</td>");
        jsonBuilder.Append("        <td>商品名称</td>");
        jsonBuilder.Append("        <td>规格</td>");
        jsonBuilder.Append("        <td>生产企业</td>");
        jsonBuilder.Append("        <td>本站价格</td>");
        jsonBuilder.Append("    </tr>");

        IList<PackageProductInfo> entityList = (IList<PackageProductInfo>)Session["PackageProductInfo"];

        ProductInfo productEntity = null;

        foreach (PackageProductInfo entity in entityList)
        {
            productEntity = MyBLLPRO.GetProductByID(entity.Package_Product_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (productEntity != null)
            {
                if (productEntity.Product_SupplierID == tools.NullInt(Session["supplier_id"]) && productEntity.U_Product_Shipper == 0)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"hidden\" name=\"product_id\" value=\"" + entity.Package_Product_ProductID + "\"><a href=\"javascript:product_del(" + entity.Package_Product_ProductID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                    jsonBuilder.Append("        <td><input type=\"text\" name=\"product_amount\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" value=\"" + entity.Package_Product_Amount + "\" size=\"10\"  /></td>");
                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(productEntity.Product_Code) + "</td>");
                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(productEntity.Product_Name) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(productEntity.Product_Spec) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(productEntity.Product_Maker) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(productEntity.Product_Price) + "</td>");
                    jsonBuilder.Append("    </tr>");
                }
            }
        }
        jsonBuilder.Append("</table>");
        entityList = null;

        return jsonBuilder.ToString();
    }

    public string GetPackageProduct()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"100\">捆绑数量</td>");
        jsonBuilder.Append("        <td>商品名称</td>");
        jsonBuilder.Append("    </tr>");

        IList<PackageProductInfo> entityList = (IList<PackageProductInfo>)Session["PackageProductInfo"];

        ProductInfo productEntity = null;

        foreach (PackageProductInfo entity in entityList)
        {
            productEntity = MyBLLPRO.GetProductByID(entity.Package_Product_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td>" + entity.Package_Product_Amount + "</td>");
            jsonBuilder.Append("        <td align=\"left\">" + productEntity.Product_Name + "</td>");
            jsonBuilder.Append("    </tr>");
        }
        jsonBuilder.Append("</table>");
        entityList = null;

        return jsonBuilder.ToString();
    }

}
