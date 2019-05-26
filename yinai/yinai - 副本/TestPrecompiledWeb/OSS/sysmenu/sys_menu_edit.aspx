<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    SysMenu MyApp;
    private ITools tools;
    private SysMenuInfo sysmenu;
    int menu_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a");
        MyApp = new SysMenu();
        tools = ToolsFactory.CreateTools();
        
        menu_id=tools.CheckInt(Request["menu_id"]);
        sysmenu = MyApp.GetSysMenuByID(menu_id);
        if (sysmenu == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">系统菜单</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sys_menu_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">菜单名称</td>
          <td class="cell_content"><input name="Sys_Menu_Name" type="text" id="Sys_Menu_Name" value="<%=sysmenu.Sys_Menu_Name %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">所属栏目</td>
          <td class="cell_content">
          <%MyApp.Select_Menu_Channel(sysmenu.Sys_Menu_Channel); %></td>
        </tr>
        <tr>
          <td class="cell_title">所属菜单</td>
          <td class="cell_content"><div id="menu_div"><%MyApp.Select_Menu_Parent("Sys_Menu_ParentID", sysmenu.Sys_Menu_ParentID, sysmenu.Sys_Menu_Channel); %></div></td>
        </tr>
        <tr>
          <td class="cell_title">权限代码</td>
          <td class="cell_content"><input name="Sys_Menu_Privilege" type="text" id="Sys_Menu_Privilege" size="50" value="<%=sysmenu.Sys_Menu_Privilege %>" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">菜单图标</td>
          <td class="cell_content"><input name="Sys_Menu_Icon" type="text" id="Sys_Menu_Icon" value="<%=sysmenu.Sys_Menu_Icon %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">页面地址</td>
          <td class="cell_content"><input name="Sys_Menu_Url" type="text" id="Sys_Menu_Url" value="<%=sysmenu.Sys_Menu_Url %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">目标窗口</td>
          <td class="cell_content">
          <select name="Sys_Menu_Target">
          <option value="0" <%=Public.CheckedSelected(sysmenu.Sys_Menu_Target.ToString(),"0") %>>框架内</option>
          <option value="1" <%=Public.CheckedSelected(sysmenu.Sys_Menu_Target.ToString(),"1") %>>新窗口</option>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">是否默认</td>
          <td class="cell_content"><input name="Sys_Menu_IsDefault" type="radio" id="Sys_Menu_IsDefault" value="1" <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsDefault.ToString(), "1")%>/>是<input type="radio" name="Sys_Menu_IsDefault" id="Sys_Menu_IsDefault1" value="0"  <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsDefault.ToString(), "0")%>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">是否常用</td>
          <td class="cell_content"><input name="Sys_Menu_IsCommon" type="radio" id="Sys_Menu_IsCommon" value="1" <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsCommon.ToString(), "1")%>/>是<input type="radio" name="Sys_Menu_IsCommon" id="Sys_Menu_IsCommon1" value="0"  <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsCommon.ToString(), "0")%>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">是否启用</td>
          <td class="cell_content"><input name="Sys_Menu_IsActive" type="radio" id="Sys_Menu_IsActive" value="1"  <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsActive.ToString(), "1")%>/>是<input type="radio" name="Sys_Menu_IsActive" id="Sys_Menu_IsActive1" value="0" <% =Public.CheckedRadio(sysmenu.Sys_Menu_IsActive.ToString(), "0")%>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Sys_Menu_Sort" type="text" id="Sys_Menu_Sort" value="<%=sysmenu.Sys_Menu_Sort %>" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Sys_Menu_ID" name="Sys_Menu_ID" value="<%=menu_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='sys_menu_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>