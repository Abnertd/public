<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private RBACResource myApp;
    private ITools tools;

    private string RBAC_Resource_Name, RBAC_Resource_Site, Act;
    private int RBAC_Resource_ID, RBAC_Resource_GroupID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("7e0cb63c-935a-4414-a0bb-5c1b7e259d92");

        myApp = new RBACResource();
        tools = ToolsFactory.CreateTools();

        Act = tools.CheckStr(Request.QueryString["action"]);

        if (Act == "renew") {
            RBAC_Resource_ID = tools.CheckInt(Request.QueryString["rbac_resource_id"]);
            RBACResourceInfo entity = myApp.GetRBACResourceByID(RBAC_Resource_ID);
            if (entity == null) {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
            else {
                RBAC_Resource_ID = entity.RBAC_Resource_ID;
                RBAC_Resource_GroupID = entity.RBAC_Resource_GroupID;
                RBAC_Resource_Name = entity.RBAC_Resource_Name;
                RBAC_Resource_Site = entity.RBAC_Resource_Site;
            }
            Act = "renew";
        }
        else {
            Act = "new";
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
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">资源管理 [<a href="javascript:void(0);" onclick="javascript:$('#block_add').show();">添加资源</a>]</td>
    </tr>
    <tr id="block_add" <% if (Act != "renew") { Response.Write("style = \"display:none;\""); } %>>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="resource_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">所属分组</td>
          <td class="cell_content">
          <select name="RBAC_Resource_GroupID" id="RBAC_Resource_GroupID">
            <% =myApp.ResourceGroupOption(RBAC_Resource_GroupID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">资源名称</td>
          <td class="cell_content"><input name="RBAC_Resource_Name" type="text" id="RBAC_Resource_Name" size="50" maxlength="50" value="<% =RBAC_Resource_Name%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =Act%>" />
            <input type="hidden" id="RBAC_Resource_ID" name="RBAC_Resource_ID" value="<% =RBAC_Resource_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="javascript:location='resource_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'resource_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '资源名称', '所属分组', "操作"],
            colModel: [
				{width:40,  name: 'RBACResourceInfo.RBAC_Resource_ID', index: 'RBACResourceInfo.RBAC_Resource_ID', align: 'center'},
				{ name: 'RBACResourceInfo.RBAC_Resource_Name', index: 'RBACResourceInfo.RBAC_Resource_Name'},
				{ width:80, align: 'center',name: 'RBACResourceInfo.RBAC_Resource_GroupID', index: 'RBACResourceInfo.RBAC_Resource_GroupID'},
				{ width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'RBACResourceInfo.RBAC_Resource_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager',
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        </script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>