<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("fca14348-91f1-4522-8063-98ff215d5dab");
   Supplier supplier = new Supplier();
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>添加供应商标签</title>
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
      <td class="content_title">添加供应商标签</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Tag_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">标签名称</td>
          <td class="cell_content"><input name="Supplier_Tag_Name" type="text" id="Supplier_Tag_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">标签图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=supplier&formname=formadd&frmelement=Supplier_Tag_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Supplier_Tag_Img" type="hidden" id="Supplier_Tag_Img" /></td>
        </tr>
        <tr id="tr_Supplier_Tag_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Supplier_Tag_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Supplier_Tag_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">页面内容</td>
          <td class="cell_content">
            <textarea cols="80" id="About_Content" name="Supplier_Tag_Content" rows="16"></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Supplier_Tag_Content');
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Supplier_list.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
