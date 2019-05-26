<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("6834de44-d231-42bc-a89f-3b0e4461fcc1");
   Supplier Supplier = new Supplier();
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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加供应商</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">供应商账号</td>
          <td class="cell_content"><input name="Supplier_Account" type="text" id="Supplier_Account" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">供应商密码</td>
          <td class="cell_content"><input name="Supplier_Password" type="password" id="Supplier_Password" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">供应商名称</td>
          <td class="cell_content"><input name="Supplier_Name" type="text" id="Supplier_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">电话</td>
          <td class="cell_content"><input name="Supplier_Phone" type="text" id="Supplier_Phone" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">传真</td>
          <td class="cell_content"><input name="Supplier_Fax" type="text" id="Supplier_Fax" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content"><input name="Supplier_Contactman" type="text" id="Supplier_Contactman" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">地址</td>
          <td class="cell_content"><input name="Supplier_Address" type="text" id="Supplier_Address" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">合作模式</td>
          <td class="cell_content"><input name="Supplier_Mode" type="radio" id="Supplier_Mode1" value="0" checked /> 佣金模式 <input name="Supplier_Mode" type="radio" id="Supplier_Mode" value="1" /> 差价模式</td>
        </tr>
        <tr>
          <td class="cell_title">运费模式</td>
          <td class="cell_content"><input name="Supplier_DeliveryMode" type="radio" id="Supplier_DeliveryMode" value="0" checked /> 合并计算 <input name="Supplier_DeliveryMode" type="radio" id="Supplier_DeliveryMode1" value="1" /> 单独计算</td>
        </tr>
        <tr>
          <td class="cell_title">供应商状态</td>
          <td class="cell_content"><input name="Supplier_Status" type="radio" id="Supplier_Status" value="0" checked /> 不启用 <input name="Supplier_Status" type="radio" id="Supplier_Status1" value="1" /> 启用此账号 <input name="Supplier_Status" type="radio" id="Supplier_Status2" value="2" /> 冻结此账号</td>
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
