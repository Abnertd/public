<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("51ce468d-a37a-42dd-9bef-3a0d7ab4eff7");
        
        tools = ToolsFactory.CreateTools();
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
      <td class="content_title">来源管理</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'sources_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '来源名称', "来源代码", "操作"],
            colModel: [
				{width:40,  name: 'SourcesInfo.Sources_ID', index: 'SourcesInfo.Sources_ID', align: 'center'},
				{ name: 'SourcesInfo.Sources_Name', index: 'SourcesInfo.Sources_Name'},
				{align: 'center', name: 'SourcesInfo.Sources_Code', index: 'SourcesInfo.Sources_Code'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SourcesInfo.Sources_ID',
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