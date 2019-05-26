<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Product myApp;
    private ITools tools;
    int Product_ID = 0;
    private string Product_SEO_Title, Product_SEO_Keyword, Product_SEO_Description;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
        myApp = new Product();
        tools = ToolsFactory.CreateTools();
        Product_ID = tools.CheckInt(Request["Product_ID"]);
        ProductInfo entity = myApp.GetProductByID(Product_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Product_ID = entity.Product_ID;

            Product_SEO_Title = entity.Product_SEO_Title;
            Product_SEO_Keyword = entity.Product_SEO_Keyword;
            Product_SEO_Description = entity.Product_SEO_Description;

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
<script type="text/javascript" src="/public/fckeditor/fckeditor.js"></script>
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
<script src="/Scripts/product.js" type="text/javascript"></script>
<!--颜色选择器-->
<link rel="stylesheet" href="/public/colorpicker/css/colorpicker.css" type="text/css" />
<script type="text/javascript" src="/public/colorpicker/js/colorpicker.js"></script>
</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改商品</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/product/product_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">TITLE<br />
            (页面标题)</td>
          <td class="cell_content"><input name="Product_SEO_Title" type="text" id="Product_SEO_Title" size="50" maxlength="200" value="<%  =Product_SEO_Title%>"/></td>
        </tr>
        <tr>
          <td class="cell_title">META_KEYWORDS<br />
            (页面关键词)</td>
          <td class="cell_content"><input name="Product_SEO_Keyword" type="text" id="Product_SEO_Keyword" size="50" maxlength="200" value="<%  =Product_SEO_Keyword%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_DESCRIPTION<br />
            (页面描述)</td>
          <td class="cell_content"><textarea name="Product_SEO_Description" cols="50" rows="5" id="Product_SEO_Description"><%  =Product_SEO_Description%></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="Product_ID" name="Product_ID" value="<% =Product_ID%>" />
            <input type="hidden" id="action" name="action" value="renewseo" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
            <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
