<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Product myApp;
    private ProductAuditReason myreason;
    private ITools tools;
    string product_idstr = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Public.CheckLogin("all");

        myApp = new Product();
        myreason = new ProductAuditReason();
        tools = ToolsFactory.CreateTools();
        string objValue;
        int product_id;
        string action = Request["action"];

        product_idstr = tools.CheckStr(Request.QueryString["product_id"]);

        switch (action)
        {
            case "new":
                Public.CheckLogin("a8dcfdfb-2227-40b3-a598-9643fd4c7e18");

                myApp.AddProduct();
                Response.End();
                break;
            case "renew":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");

                myApp.EditProduct();
                Response.End();
                break;
            case "renewseo":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");

                myApp.EditProductSEO();
                break;
            case "move":
                Public.CheckLogin("fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f");

                myApp.DelProduct();
                Response.End();
                break;
            case "batchmove":
                Public.CheckLogin("fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f");

                myApp.DelProduct_Batch();
                Response.End();
                break;
            case "list":
                Public.CheckLogin("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8");

                Response.Write(myApp.GetProducts());
                Response.End();
                break;
            case "productexport":
                Public.CheckLogin("56b7d7ed-969a-45d5-b198-416fd0c2a3a4");

                myApp.ProductExport();
                Response.End();
                break;
            case "productallexport":
                Public.CheckLogin("56b7d7ed-969a-45d5-b198-416fd0c2a3a4");

                myApp.ProductExport_All();
                Response.End();
                break;
            case "productpriceexport":
                Public.CheckLogin("e033e512-bc19-4f41-9263-b61bf13572dc");

                myApp.ProductPriceExport();
                Response.End();
                break;

            case "check_name":
                objValue = tools.CheckStr(Request.QueryString["objValue"]);
                if (objValue == null || objValue == "")
                {
                    Response.Write(Public.AjaxTip("error", "请填写商品名称"));
                }
                Response.End();
                break;
            case "check_code":
                objValue = tools.CheckStr(Request.QueryString["objValue"]);
                product_id = tools.CheckInt(Request.QueryString["product_id"]);
                if (objValue == null || objValue == "")
                {
                    Response.Write(Public.AjaxTip("error", "请填写商品编码"));
                }
                else
                {
                    if (!myApp.CheckProductCode(objValue, product_id)) { Response.Write(Public.AjaxTip("error", "已存在该商品编码")); }
                }
                Response.End();
                break;
            case "check_cate":
                objValue = tools.CheckStr(Request.QueryString["objValue"]);
                if (objValue == null || objValue == "")
                {
                    Response.Write(Public.AjaxTip("error", "请选择商品所属类别"));
                }
                Response.End();
                break;
            case "check_type":
                objValue = tools.CheckStr(Request.QueryString["objValue"]);
                if (objValue == null || objValue == "" || objValue == "0")
                {
                    Response.Write(Public.AjaxTip("error", "请选择商品所属类型"));
                }
                Response.End();
                break;
            case "grade_price":
                double price = tools.CheckFloat(Request.QueryString["product_price"]);
                product_id = tools.CheckInt(Request.QueryString["product_id"]);
                Response.Write(myApp.Product_Grade_Price_Form(product_id, price, 0));
                Response.End();
                break;
            case "change_maincate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(myApp.Product_Category_Select(cate_id, target_div));
                Response.End();
                break;
            case "check_maincate":
                int parent = tools.CheckInt(Request.QueryString["parent"]);
                int cate = tools.CheckInt(Request.QueryString["cate_id"]);
                if (parent == 0 && cate == 0)
                {
                    Response.Write(Public.AjaxTip("error", "请选择商品主分类"));
                }
                Response.End();
                break;
            case "audit":
                myApp.Product_Audit_Edit(1);
                Response.End();
                break;
            case "denyaudit":
                myApp.Product_Audit_Edit(2);
                Response.End();
                break;
            case "cancelaudit":
                myApp.Product_Audit_Edit(0);
                Response.End();
                break;
            case "insale":
                myApp.Product_Insale_Edit(1);
                Response.End();
                break;
            case "cancelinsale":
                myApp.Product_Insale_Edit(0);
                Response.End();
                break;
            case "copyproduct":
                myApp.CopyProduct(tools.CheckInt(Request.QueryString["product_id"]));
                Response.End();
                break;
        }

        if (product_idstr == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的产品", false, "{back}");
            return;
        }

        if (tools.Left(product_idstr, 1) == ",") { product_idstr = product_idstr.Remove(0, 1); }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">商品审核</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/Product/product_Do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">原因</td>
                                <td class="cell_content">
                                    <%=myreason.GetProductAuditReasonsCheckbox("audit_reason") %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">原因备注</td>
                                <td class="cell_content">
                                    <textarea name="Audit_Note" id="Audit_Note" cols="50" rows="5"></textarea></td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="working" name="action" value="denyaudit" />
                                    <input type="hidden" id="product_id" name="product_id" value="<%=product_idstr %>" />
                                    <input name="Brand_Img" type="hidden" id="Brand_Img" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="确定" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'product.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
