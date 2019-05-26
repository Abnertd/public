<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("ec8e6ed4-907f-4777-be1c-e07690e2eab0");
   Supplier supplier = new Supplier();
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺等级添加</title>
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
      <td class="content_title">店铺等级添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/shop/shop_grade_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">等级名称</td>
          <td class="cell_content"><input name="Shop_Grade_Name" type="text" id="Shop_Grade_Name" size="50" maxlength="50" /> <span class="t12_red">*</span></td>
        </tr>
        <tr>
          <td class="cell_title">限制添加商品数</td>
          <td class="cell_content"><input name="Shop_Grade_ProductLimit" type="text" id="Shop_Grade_ProductLimit" size="50" maxlength="50" /> <span class="tip">0为不限</span> </td>
        </tr>
         <tr>
          <td class="cell_title">默认佣金</td>
          <td class="cell_content"><input name="Shop_Grade_DefaultCommission" type="text" id="Shop_Grade_DefaultCommission" size="50" maxlength="50" />% </td>
        </tr>
        
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Shop_Grade_IsActive" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Shop_Grade_IsActive" value="0"/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Shop_Css_List.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
