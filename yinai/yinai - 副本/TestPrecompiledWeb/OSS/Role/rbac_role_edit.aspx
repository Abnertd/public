<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private RBACRole myApp;
    private ITools tools;

    private string RBAC_Role_Name, RBAC_Role_Description, RBAC_Role_Site;
    private int RBAC_Role_ID, RBAC_Role_IsSystem;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("4df5eb30-ee06-49a4-b119-4c72e5dfaebc");
        
        myApp = new RBACRole();
        tools = ToolsFactory.CreateTools();

        RBAC_Role_ID = tools.CheckInt(Request.QueryString["RBAC_Role_ID"]);
        RBACRoleInfo entity = myApp.GetRBACRoleByID(RBAC_Role_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            RBAC_Role_ID = entity.RBAC_Role_ID;
            RBAC_Role_Name = entity.RBAC_Role_Name;
            RBAC_Role_Description = entity.RBAC_Role_Description;
            RBAC_Role_IsSystem = entity.RBAC_Role_IsSystem;
            RBAC_Role_Site = entity.RBAC_Role_Site;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">角色添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="rbac_role_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">角色名称</td>
          <td class="cell_content"><input name="RBAC_Role_Name" type="text" id="RBAC_Role_Name" style="width:200px;" maxlength="50" value="<% =RBAC_Role_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">角色说明</td>
          <td class="cell_content"><textarea cols="60" rows="6" id="RBAC_Role_Description" name="RBAC_Role_Description"><% =RBAC_Role_Description%></textarea></td>
        </tr>
        <tr>
          <td class="cell_title">系统角色</td>
          <td class="cell_content"><input name="RBAC_Role_IsSystem" type="radio" id="RBAC_Role_IsSystem1" value="1" <% =Public.CheckedRadio(RBAC_Role_IsSystem.ToString(), "1")%>/>是 <input type="radio" name="RBAC_Role_IsSystem" id="RBAC_Role_IsSystem2" value="0" <% =Public.CheckedRadio(RBAC_Role_IsSystem.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">角色权限</td>
          <td class="cell_content"><% =myApp.DisplayResourceGroup(RBAC_Role_ID)%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="RBAC_Role_ID" name="RBAC_Role_ID" value="<% =RBAC_Role_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='rbac_role_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>