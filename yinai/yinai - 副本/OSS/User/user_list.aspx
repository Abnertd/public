<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("f7fb595e-75cf-4dd2-8557-fadfa5756058");
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
      <td class="content_title">用户管理</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'user_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '用户名', '最后登录时间', '最后登录IP', '添加时间', "操作"],
            colModel: [
				{width:40,  name: 'RBACUserInfo.RBAC_User_ID', index: 'RBACUserInfo.RBAC_User_ID', align: 'center'},
				{ align: 'center',  name: 'RBACUserInfo.RBAC_User_Name', index: 'RBACUserInfo.RBAC_User_Name'},
				{width:110,  name: 'RBACUserInfo.RBAC_User_LastLogin', index: 'RBACUserInfo.RBAC_User_LastLogin', align: 'center'},
				{ width:100, name: 'RBACUserInfo.RBAC_User_LastLoginIP', index: 'RBACUserInfo.RBAC_User_LastLoginIP', align: 'center'},
				{width:110,  name: 'RBACUserInfo.RBAC_User_Addtime', index: 'RBACUserInfo.RBAC_User_Addtime', align: 'center'},
				{width:80,  name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'RBACUserInfo.RBAC_User_ID',
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