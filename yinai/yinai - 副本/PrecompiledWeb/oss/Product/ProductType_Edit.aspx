<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">

    private ProductType producttype;
    private ITools tools;

    private int ProductType_ID, ProductType_Sort, ProductType_IsActive;
    private string ProductType_Name, ProductType_Site;
    private IList<BrandInfo> ProductType_Brands = new List<BrandInfo>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
        producttype = new ProductType();
        tools = ToolsFactory.CreateTools();

        ProductType_ID = tools.CheckInt(Request.QueryString["ProductType_ID"]);
        if (ProductType_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ProductTypeInfo entity = producttype.GetProductTypeByID(ProductType_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            ProductType_ID = entity.ProductType_ID;
            ProductType_Name = entity.ProductType_Name;
            ProductType_IsActive = entity.ProductType_IsActive;
            ProductType_Site = entity.ProductType_Site;
            ProductType_Sort = entity.ProductType_Sort;
            ProductType_Brands=entity.BrandInfos;
        }
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
      <td class="content_title">修改商品参数</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Producttype_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品参数名称</td>
          <td class="cell_content"><input name="ProductType_Name" type="text" id="ProductType_Name" value="<%=ProductType_Name %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用该商品参数</td>
          <td class="cell_content"><input name="ProductType_IsActive" type="radio" id="ProductType_IsActive1" value="1" <%=Public.CheckedRadio(ProductType_IsActive.ToString(),"1") %>/>是<input type="radio" name="ProductType_IsActive" id="ProductType_IsActive2" value="0" <%=Public.CheckedRadio(ProductType_IsActive.ToString(),"0") %>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">商品参数排序</td>
          <td class="cell_content"><input name="ProductType_Sort" type="text" id="ProductType_Sort" value="<%= ProductType_Sort%>" size="10" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">关联品牌</td>
          <td class="cell_content"><%=producttype.ProductType_BrandSelect(ProductType_Brands)%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="ProductType_ID" name="ProductType_ID" value="<%=ProductType_ID %>" />
            <input type="hidden" id="action" name="action" value="renew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存商品参数" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';"  onclick="location='productType.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
