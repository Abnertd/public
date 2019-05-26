<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%  Public.CheckLogin("4d14d977-e839-4322-ae0d-fa257030dd2b");
    SysMenu MyApp = new SysMenu();
    ITools tools;
    tools =ToolsFactory.CreateTools();
    int channel_id=tools.CheckInt(Request["Sys_Menu_Channel"]);%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
      <td class="content_title">系统菜单管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="frmsearch" method="get" action="?">
        <table>
            <tr>
                <td>
                <% MyApp.Select_Channel(channel_id); %>
                </td>
                <td><input name="save" type="submit" class="bt_orange" value="搜索" /></td>
            </tr>
        </table>
        </form>
        
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'sys_menu_do.aspx?action=list&channel_id=<%=channel_id %>',
			datatype: "json",
            colNames: ['ID', '菜单名称', "所属栏目","所属菜单","权限代码","页面地址","目标窗口","是否默认", "是否常用","是否启用","排序", "操作"],
            colModel: [
				{width:50, name: 'SysMenuInfo.Sys_Menu_ID', index: 'SysMenuInfo.Sys_Menu_ID', align: 'center'},
				{width:80,align:'left', name: 'SysMenuInfo.Sys_Menu_Name', index: 'SysMenuInfo.Sys_Menu_Name'},
				{width:60,align:'center', name: 'SysMenuInfo.Sys_Menu_Channel', index: 'SysMenuInfo.Sys_Menu_Channel'},
				{width:60,align:'center', name: 'SysMenuInfo.Sys_Menu_ParentID', index: 'SysMenuInfo.Sys_Menu_ParentID'},
				{width:190,align:'center', name: 'SysMenuInfo.Sys_Menu_privilege', index: 'SysMenuInfo.Sys_Menu_privilege'},
				{ name: 'SysMenuInfo.Sys_Menu_Url', index: 'SysMenuInfo.Sys_Menu_Url'},
				{width:50,align:'center', name: 'SysMenuInfo.Sys_Menu_Target', index: 'SysMenuInfo.Sys_Menu_Target'},
				{width:50,align:'center', name: 'SysMenuInfo.Sys_Menu_IsDefault', index: 'SysMenuInfo.Sys_Menu_IsDefault'},
				{width:50,align:'center', name: 'SysMenuInfo.Sys_Menu_IsCommon', index: 'SysMenuInfo.Sys_Menu_IsCommon'},
				{width:50,align:'center', name: 'SysMenuInfo.Sys_Menu_IsActive', index: 'SysMenuInfo.Sys_Menu_IsActive'},
				{width:50, align:'center',name: 'SysMenuInfo.Sys_Menu_Sort', index: 'PromotionInfo.Promotion_Addtime'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:true},
			],
            sortname: 'SysMenuInfo.Sys_Menu_ID',
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
