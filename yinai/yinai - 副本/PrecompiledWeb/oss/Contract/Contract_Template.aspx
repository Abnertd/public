<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

        Public.CheckLogin("d4d58107-0e58-485f-af9e-3b38c7ff9672");
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
      <td class="content_title">合同模版管理</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'contract_template_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '模版名称', '模版标识', '编辑时间', "操作"],
            colModel: [
				{ width:50,name: 'ContractTemplateInfo.Contract_Template_ID', index: 'ContractTemplateInfo.Contract_Template_ID', align: 'center', width: '60px'},
				{ width:100,name: 'ContractTemplateInfo.Contract_Template_Name', index: 'ContractTemplateInfo.Contract_Template_Name', align: 'center'},
				{ width:40,name: 'ContractTemplateInfo.Contract_Template_Code', index: 'ContractTemplateInfo.Contract_Template_Code', align: 'center', width: '60px'},
				{ width:30,name: 'ContractTemplateInfo.Contract_Template_Addtime', index: 'ContractTemplateInfo.Contract_Template_Addtime', align: 'center', width: '60px'},
				{ width:100,name: 'Operate', index: 'Operate', align: 'center', width: '80px', sortable:false},
			],
            sortname: 'ContractTemplateInfo.Contract_Template_ID',
			sortorder: "desc",
			rowNum: 10,
			rowList:[10,20,40], 
			pager: 'pager',
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%",
			shrinkToFit:true 
        });
        </script>
      </td>
    </tr>
  </table>
</div>
</body>
</html>