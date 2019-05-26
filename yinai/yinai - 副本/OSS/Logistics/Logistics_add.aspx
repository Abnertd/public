<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c747b411-cf59-447b-a2d7-7e5510589f25");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
<script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加物流商</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="Logistics_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">物流商登录名</td>
          <td class="cell_content"><input name="Logistics_NickName" type="text" id="Logistics_NickName" size="50" maxlength="100" /></td>
        </tr>

          <tr>
          <td class="cell_title">登录密码</td>
          <td class="cell_content"><input name="Logistics_Password" type="password" id="Logistics_Password" size="50" maxlength="100" /></td>
        </tr>

          <tr>
          <td class="cell_title">确认密码</td>
          <td class="cell_content"><input name="password_confirm" type="password" id="password_confirm" size="50" maxlength="100" /></td>
        </tr>

          <tr>
          <td class="cell_title">物流商公司名</td>
          <td class="cell_content"><input name="Logistics_CompanyName" type="text" id="Logistics_CompanyName" size="50" maxlength="100" /></td>
        </tr>

          <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content"><input name="Logistics_Name" type="text" id="Logistics_Name" size="50" maxlength="100" /></td>
        </tr>
          <tr>
          <td class="cell_title">联系电话</td>
          <td class="cell_content"><input name="Logistics_Tel" type="text" id="Logistics_Tel" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">是否启用</td>
          <td class="cell_content"><input name="Logistics_Status" type="radio" id="Logistics_Status1" value="1" checked="checked"/>是 <input type="radio" name="Logistics_Status" id="Logistics_Status2" value="0"/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Logistics_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>