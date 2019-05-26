<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Sys myApp;
    private ITools tools;
    private RBACUserGroup myGroupApp;

    private string RBAC_User_Name;
    private int RBAC_User_ID, RBAC_User_GroupID;
    private IList<RBACRoleInfo> roleList;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b47f8b43-cd62-4afc-8538-9acc6ba2a762");
        
        myApp = new Sys();
        myGroupApp = new RBACUserGroup();
        tools = ToolsFactory.CreateTools();

        RBAC_User_ID = tools.CheckInt(Request.QueryString["RBAC_User_ID"]);
        RBACUserInfo entity = myApp.GetRBACUserByID(RBAC_User_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            RBAC_User_ID = entity.RBAC_User_ID;
            RBAC_User_GroupID = entity.RBAC_User_GroupID;
            RBAC_User_Name = entity.RBAC_User_Name;
            roleList = entity.RBACRoleInfos;
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
      <td class="content_title">用户管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="user_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">所属分组</td>
          <td class="cell_content">
          <select name="RBAC_User_GroupID" id="RBAC_User_GroupID">
            <% =myGroupApp.UserGroupOption(RBAC_User_GroupID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">用户名</td>
          <td class="cell_content"><% =RBAC_User_Name%></td>
        </tr>
        <tr>
          <td class="cell_title">密码</td>
          <td class="cell_content"><input name="RBAC_User_Password" type="password" id="RBAC_User_Password" style="width:200px;" maxlength="20" /></td>
        </tr>
        <tr>
          <td class="cell_title">重复密码</td>
          <td class="cell_content"><input name="RBAC_User_Password_Confirm" type="password" id="RBAC_User_Password_Confirm" style="width:200px;" maxlength="20" /></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">角色选择</td>
          <td class="cell_content"><% =myApp.DisplayRoleCheckbox(roleList)%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="RBAC_User_ID" name="RBAC_User_ID" value="<% =RBAC_User_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='user_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>