<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script runat="server">
    SysCity MyApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
        MyApp = new SysCity();
        
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>地区添加</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">地区添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sys_state_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">地区名称</td>
          <td class="cell_content"><input name="Sys_County_CN" type="text" id="Sys_County_CN" size="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">所属城市</td>
          <td class="cell_content"><%MyApp.getSelectCitys(""); %></td>
        </tr>
        <tr>
          <td class="cell_title">是否启用</td>
          <td class="cell_content"><input name="Sys_County_IsActive" type="radio" checked="checked" id="Sys_County_IsActive" value="1" />是<input type="radio" name="Sys_County_IsActive" id="Sys_County_IsActive1" value="0"/>否 </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="countynew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Sys_County_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>