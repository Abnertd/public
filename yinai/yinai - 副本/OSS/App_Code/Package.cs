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

        if (Package_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写套装名称", false, "{back}"); }


        PackageInfo entity = new PackageInfo();
        entity.Package_ID = Package_ID;
        entity.Package_Name = Package_Name;
        entity.Package_IsInsale = Package_IsInsale;
        entity.Package_StockAmount = Package_StockAmount;
        entity.Package_Weight = Package_Weight;
        entity.Package_Price = Package_Price;
        entity.Package_Sort = Package_Sort;
        entity.Package_Addtime = DateTime.Now;
        entity.Package_Site = Public.GetCurrentSite();
        entity.PackageProductInfos = ReadPackageProduct();

        if (MyBLL.AddPackage(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Package_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public IList<PackageProductInfo> ReadPackageProduct()
    {
        string product_id = tools.CheckStr(Request.Form["product_id"]);
        string product_amount = tools.CheckStr(Request.Form["product_amount"]);
        string[] ProductIDArr = product_id.Split(',');
        string[] ProductAmountArr = product_amount.Split(',');

        if (product_id.Length == 0) { Public.Msg("error", "错误信息", "请选择套装商品", false, "{back}"); }

        int icount = 0;

        IList<PackageProductInfo> entityList = new List<PackageProductInfo>();
        PackageProductInfo product = null;
        foreach (string pid in ProductIDArr)
        {
            if (tools.CheckInt(pid) == 0 || tools.CheckInt(ProductAmountArr[icount]) == 0) continue;

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

        if (Package_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写套装名称", false, "{back}"); }

        PackageInfo entity = new PackageInfo();
        entity.Package_ID = Package_ID;
        entity.Package_Name = Package_Name;
        entity.Package_IsInsale = Package_IsInsale;
        entity.Package_StockAmount = Package_StockAmount;
        entity.Package_Weight = Package_Weight;
        entity.Package_Price = Package_Price;
        entity.Package_Sort = Package_Sort;
        entity.Package_Addtime = DateTime.Now;
        entity.Package_Site = Public.GetCurrentSite();
        entity.PackageProductInfos = ReadPackageProduct();

        if (MyBLL.EditPackage(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Package_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelPackage()
    {
        int Package_ID = tools.CheckInt(Request.QueryString["Package_ID"]);
        if (MyBLL.DelPackage(Package_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Package_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public PackageInfo GetPackageByID(int cate_id)
    {
        return MyBLL.GetPackageByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetPackages()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int isinsale = tools.CheckInt(Request["isinsale"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PackageInfo.Package_SupplierID", "=", "0"));
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
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<PackageInfo> entitys = MyBLL.GetPackages(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PackageInfo entity in entitys)
            {
                jsonBuilder.Append("{\"PackageInfo.Package_ID\":" + entity.Package_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Package_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Package_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Package_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Package_Weight);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Package_StockAmount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Package_IsInsale == 1)
                {
                    jsonBuilder.Append("上架");
                }
                else
                {
                    jsonBuilder.Append("下架");
                }
                
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Package_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("7b8b58e2-e509-4e6c-a68e-0361225cefa6"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"package_edit.aspx?package_id=" + entity.Package_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("fc830fc1-1192-4097-9d92-e625a707cbc1"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('package_do.aspx?action=move&package_id=" + entity.Package_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductInfo.Product_SupplierID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductInfo.U_Product_Shipper", "=", "1"));
        if (product_cate > 0)
        {
            string subCates = MyProduct.Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
            //string product_idstr = MyProduct.Get_All_CateProductID(subCates);
            //if (subCates == product_cate.ToString())
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_CateID", "=", product_cate.ToString()));
            //}
            //else
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_CateID", "IN", subCates));
            //}
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

        IList<ProductInfo> entitys = MyBLLPRO.GetProductList(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
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

            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
            jsonBuilder.Append("        <td colspan=\"6\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:product_add('product_id');\" /></td>");
            jsonBuilder.Append("    </tr>");
            jsonBuilder.Append("</table>");
            jsonBuilder.Append("</form>");

            entitys = null;
            return jsonBuilder.ToString();
        }
        else { return null; }
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
            productEntity = MyBLLPRO.GetProductByID(entity.Package_Product_ProductID, Public.GetUserPrivilege());
            if (productEntity != null)
            {
                if (productEntity.Product_SupplierID == 0 || productEntity.U_Product_Shipper == 1)
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
            productEntity = MyBLLPRO.GetProductByID(entity.Package_Product_ProductID, Public.GetUserPrivilege());

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
