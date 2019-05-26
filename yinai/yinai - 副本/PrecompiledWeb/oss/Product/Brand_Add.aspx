<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("31d0ad2e-dd9b-4a04-a800-407b8ba3c9e9");
     Brand myApp;
     myApp = new Brand();
    %>

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
      <td class="content_title">添加商品品牌</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/Brand_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">品牌名称</td>
          <td class="cell_content"><input name="Brand_Name" type="text" id="Brand_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">商品类型</td>
          <td class="cell_content">
          <%=myApp.BrandProductType_Select(0) %>
          </td>
        </tr>
        <tr>
          <td class="cell_title">品牌图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=brand&formname=formadd&frmelement=Brand_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Brand_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Brand_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">品牌排序</td>
          <td class="cell_content"><input name="Brand_Sort" type="text" id="Brand_Sort" value="1" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
        <tr>
          <td class="cell_title">品牌推荐</td>
          <td class="cell_content"><input name="Brand_IsRecommend" type="radio" id="Brand_IsRecommend1" value="1" checked="checked"/>是<input type="radio" name="Brand_IsRecommend" id="Brand_IsRecommend2" value="0"/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">启用该品牌</td>
          <td class="cell_content"><input name="Brand_IsActive" type="radio" id="radio" value="1" checked="checked"/>是<input type="radio" name="Brand_IsActive" id="radio2" value="0"/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">品牌地址</td>
          <td class="cell_content"><input name="Brand_Url" type="text" id="Brand_Url" size="50" maxlength="200" /></td>
        </tr>
        <tr>
          <td class="cell_title">品牌描述</td>
          <td class="cell_content"><textarea name="Brand_Description" id="Brand_Description" cols="50" rows="5"></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="Brand_Img" type="hidden" id="Brand_Img" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存品牌" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='brand.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
