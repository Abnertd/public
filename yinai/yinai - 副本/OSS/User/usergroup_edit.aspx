<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private RBACUserGroup myApp;
    private ITools tools;

    private string RBAC_UserGroup_Name, RBAC_UserGroup_Site;
    private int RBAC_UserGroup_ID, RBAC_UserGroup_ParentID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a2f95df4-346a-47b2-a112-1f8e3f062298");
        
        myApp = new RBACUserGroup();
        tools = ToolsFactory.CreateTools();

        RBAC_UserGroup_ID = tools.CheckInt(Request.QueryString["RBAC_UserGroup_ID"]);
        RBACUserGroupInfo entity = myApp.GetRBACUserGroupByID(RBAC_UserGroup_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            RBAC_UserGroup_ID = entity.RBAC_UserGroup_ID;
            RBAC_UserGroup_Name = entity.RBAC_UserGroup_Name;
            RBAC_UserGroup_ParentID = entity.RBAC_UserGroup_ParentID;
            RBAC_UserGroup_Site = entity.RBAC_UserGroup_Site;
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
      <td class="content_title">用户群组管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="usergroup_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">群组名称</td>
          <td class="cell_content"><input name="RBAC_UserGroup_Name" type="text" id="RBAC_UserGroup_Name" size="50" maxlength="50" value="<% =RBAC_UserGroup_Name%>"/></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="RBAC_UserGroup_ID" name="RBAC_UserGroup_ID" value="<% =RBAC_UserGroup_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='usergroup_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>