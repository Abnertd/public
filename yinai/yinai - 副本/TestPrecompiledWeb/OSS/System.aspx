<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private Config myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("ef84a07f-6281-4f54-84f9-c345adf9d765");
        
        myApp = new Config();
        tools = ToolsFactory.CreateTools();
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>

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
      <td class="content_title">系统管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <table id="list"></table>
      <div id="pager"></div>
      <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'system_do.aspx?action=list',
			datatype: "json",
			colNames: ['ID', '站点名称', '站点域名', '站点地址', '站点标识', "操作"],
            colModel: [
				{ width: 40, name: 'ConfigInfo.Sys_Config_ID', index: 'ConfigInfo.Sys_Config_ID', align: 'center' },
				{ name: 'ConfigInfo.Site_Name', index: 'ConfigInfo.Site_Name' },
				{ name: 'ConfigInfo.Site_DomainName', index: 'ConfigInfo.Site_DomainName' },
				{ name: 'ConfigInfo.Site_URL', index: 'ConfigInfo.Site_URL' },
				{ name: 'ConfigInfo.Sys_Config_Site', index: 'ConfigInfo.Sys_Config_Site' },
				{ width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
			sortname: 'ConfigInfo.Sys_Config_ID',
			sortorder: "desc",
			rowNum: 10,
			rowList:[10,20,40], 
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