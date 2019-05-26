<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1d3f7ace-2191-4c5e-9403-840ddaf191c0");
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
      <td class="content_title">供应商等级管理 [<a href="Supplier_grade_add.aspx">添加等级</a>]</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Supplier_grade_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '等级名称', '优惠百分比', '是否为默认', '所需积分', "操作"],
            colModel: [
				{width:50, name: 'SupplierGradeInfo.Supplier_Grade_ID', index: 'SupplierGradeInfo.Supplier_Grade_ID', align: 'center'},
				{width:80, name: 'SupplierGradeInfo.Supplier_Grade_Name', index: 'SupplierGradeInfo.Supplier_Grade_Name', align: 'center'},
				{width:80, name: 'SupplierGradeInfo.Supplier_Grade_Percent', index: 'SupplierGradeInfo.Supplier_Grade_Percent', align: 'center'},
				{width:80, name: 'SupplierGradeInfo.Supplier_Grade_Default', index: 'SupplierGradeInfo.Supplier_Grade_Default', align: 'center'},
				{width:80, name: 'SupplierGradeInfo.Supplier_Grade_RequiredCoin', index: 'SupplierGradeInfo.Supplier_Grade_RequiredCoin', align: 'center'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierGradeInfo.Supplier_Grade_ID',
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