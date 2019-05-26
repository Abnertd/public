<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%
    Public.CheckLogin("567b39bd-d8ee-4c79-b067-7ad68e6ca348");
    ProductType producttype=new ProductType(); %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js"  type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加商品参数</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Producttype_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品参数名称</td>
          <td class="cell_content"><input name="ProductType_Name" type="text" id="ProductType_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用该商品参数</td>
          <td class="cell_content"><input name="ProductType_IsActive" type="radio" id="ProductType_IsActive1" value="1" checked="checked"/>是<input type="radio" name="ProductType_IsActive" id="ProductType_IsActive2" value="0"/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">商品参数排序</td>
          <td class="cell_content"><input name="ProductType_Sort" type="text" id="ProductType_Sort" value="1" size="10" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">关联品牌</td>
          <td class="cell_content"><%=producttype.ProductType_BrandSelect(null)%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存商品参数" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='productType.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
