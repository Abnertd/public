<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private PayWay myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("4f0daa32-ae16-4398-ae1e-d2400ca0fff0");
        myApp = new PayWay();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">支付条件添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="pay_type_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">支付条件名称</td>
          <td class="cell_content"><input name="Pay_Type_Name" type="text" id="Pay_Type_Name" size="50" maxlength="50" value="" /></td>
        </tr>
        
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Pay_Type_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Pay_Type_Sort" size="10" maxlength="10" value="1" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Pay_Type_IsActive" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Pay_Type_IsActive" value="0"/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='pay_type_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>