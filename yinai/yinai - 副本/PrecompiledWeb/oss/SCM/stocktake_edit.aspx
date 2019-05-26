<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.Product" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private IProduct myApp;

    private string Product_Code, Product_Name, Product_Maker, Product_Spec;
    private int Product_ID, Product_StockAmount, Product_UsableAmount;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("849805bd-ba21-4508-a803-9e0e5cc33b66");
        
        tools = ToolsFactory.CreateTools();
        myApp = ProductFactory.CreateProduct();

        Product_Code = tools.CheckStr(Request.QueryString["Product_Code"]);
        ProductInfo entity = myApp.GetProductByCode(Product_Code, Public.GetCurrentSite(), Public.GetUserPrivilege());
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Product_ID = entity.Product_ID;
            Product_Name = entity.Product_Name;
            Product_Code = entity.Product_Code;
            Product_Maker = entity.Product_Maker;
            Product_Spec = entity.Product_Spec;
            Product_StockAmount = entity.Product_StockAmount;
            Product_UsableAmount = entity.Product_UsableAmount;
        }

    }
</script>



<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">库存盘点</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="stocktake_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品名称</td>
          <td class="cell_content"><%=Product_Name%></td>
        </tr>
        <tr>
          <td class="cell_title">商品编码</td>
          <td class="cell_content"><%=Product_Code%></td>
        </tr>
        <tr>
          <td class="cell_title">规格</td>
          <td class="cell_content"><%=Product_Spec%></td>
        </tr>
        <tr>
          <td class="cell_title">产地</td>
          <td class="cell_content"><%=Product_Maker%></td>
        </tr>
        <tr>
          <td class="cell_title">库存</td>
          <td class="cell_content"><input name="Product_StockAmount" type="text" size="10" maxlength="100" value="<% =Product_StockAmount%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="stocktake" />
            <input type="hidden" id="Product_Code" name="Product_Code" value="<% =Product_Code%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>