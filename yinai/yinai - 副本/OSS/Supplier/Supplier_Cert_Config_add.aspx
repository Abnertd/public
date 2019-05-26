<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("a1cb6100-8590-4dec-953b-59b13002df83");
   Supplier supplier = new Supplier();
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>添加供应商资质配置</title>
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
      <td class="content_title">添加供应商资质配置</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Cert_Config_Do.aspx">
      <input name="Supplier_Cert_Type" type="hidden" value="0" />
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">供应商类型</td>
          <td class="cell_content">
          <%=supplier.DisplaySupplierCertType(0) %>
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">资质名称</td>
          <td class="cell_content"><input name="Supplier_Cert_Name" type="text" id="Supplier_Cert_Name" size="50" maxlength="100" />
          <span class="t12_red">*</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">资质排序</td>
          <td class="cell_content"><input name="Supplier_Cert_Sort" type="text" id="Supplier_Cert_Sort" value="0" size="10" />
          
          </td>
        </tr>
        <tr>
          <td class="cell_title">资质备注</td>
          <td class="cell_content"><textarea name="Supplier_Cert_Note" cols="50" rows="5" ></textarea> 最多不超过200字符</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Supplier_Cert_Config_list.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
