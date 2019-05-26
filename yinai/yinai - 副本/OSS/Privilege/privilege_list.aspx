<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private RBACPrivilege myApp;
    private ITools tools;

    private string RBAC_Privilege_Name, RBAC_Privilege_ID, Act, keyword, defaultkey;
    private int RBAC_Privilege_ResourceID;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        myApp = new RBACPrivilege();
        tools = ToolsFactory.CreateTools();

        Act = tools.CheckStr(Request.QueryString["action"]);
        RBAC_Privilege_ResourceID = tools.CheckInt(Request.QueryString["RBAC_Privilege_ResourceID"]);

        if (Act == "renew") {
            Public.CheckLogin("51be7b46-e0f7-46dd-b0b2-a462fcb907ae");
            
            RBAC_Privilege_ID = tools.CheckStr(Request.QueryString["rbac_privilege_id"]);
            RBACPrivilegeInfo entity = myApp.GetRBACPrivilegeByID(RBAC_Privilege_ID);
            if (entity == null) {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
            else {
                RBAC_Privilege_ID = entity.RBAC_Privilege_ID;
                RBAC_Privilege_Name = entity.RBAC_Privilege_Name;
                RBAC_Privilege_ResourceID = entity.RBAC_Privilege_ResourceID;
            }
            Act = "renew";
        }
        else {
            Public.CheckLogin("147d21e2-7989-44e7-8b08-0c64797c2513");
            Act = "new";
        }
        keyword = Request["keyword"];
        if (keyword != "输入权限代码、权限名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入权限代码、权限名称进行搜索";
        }
        if (keyword == "输入权限代码、权限名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>权限管理</title>
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
      <td class="content_title">权限管理 <% if (Public.CheckPrivilege("df7e7e2e-bbe2-48b0-976c-17a74c4a45e6"))
                                        { %> [<a href="javascript:void(0);" onclick="javascript:$('#block_add').show();">添加权限</a>]<%}%></td>
    </tr>
    <%
        if (Public.CheckPrivilege("df7e7e2e-bbe2-48b0-976c-17a74c4a45e6/51be7b46-e0f7-46dd-b0b2-a462fcb907ae"))
        { 
            %>
    <tr id="block_add" <% if (Act != "renew") { Response.Write("style = \"display:none;\""); } %>>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="privilege_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">所属资源</td>
          <td class="cell_content">
          <select name="RBAC_Privilege_ResourceID" id="RBAC_Privilege_ResourceID">
            <% =myApp.ResourceOption(RBAC_Privilege_ResourceID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">权限名称</td>
          <td class="cell_content"><input name="RBAC_Privilege_Name" type="text" id="RBAC_Privilege_Name" size="50" maxlength="50" value="<% =RBAC_Privilege_Name%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =Act%>" />
            <input type="hidden" id="RBAC_Privilege_ID" name="RBAC_Privilege_ID" value="<% =RBAC_Privilege_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="javascript:location='privilege_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
<%
        }
        %>
    <tr>
      <td class="content_content">
        <form id="frmsearch" method="get" action="">
        <table>
            <tr>
                <td>
                <select name="RBAC_Privilege_ResourceID" id="Search_Resource_ID">
                <option value="0">全部</option>
                <% =myApp.ResourceOption(RBAC_Privilege_ResourceID)%>
                </select> <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入权限代码、权限名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> </td>
                <td><input name="save" type="submit" class="bt_orange" value="搜索" /></td>
            </tr>
        </table>
        </form>
        
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'privilege_do.aspx?action=list&rbac_privilege_resourceid=<% =RBAC_Privilege_ResourceID%>&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID', '权限名称', '所属资源', "操作"],
            colModel: [
				{ name: 'RBACPrivilegeInfo.RBAC_Privilege_ID', index: 'RBACPrivilegeInfo.RBAC_Privilege_ID', align: 'center'},
				{width:100, name: 'RBACPrivilegeInfo.RBAC_Privilege_Name', index: 'RBACPrivilegeInfo.RBAC_Privilege_Name', align: 'center'},
				{width:100, name: 'RBACPrivilegeInfo.RBAC_Privilege_ResourceID', index: 'RBACPrivilegeInfo.RBAC_Privilege_ResourceID', align: 'center'},
				{width:80,  name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'RBACPrivilegeInfo.RBAC_Privilege_ResourceID',
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