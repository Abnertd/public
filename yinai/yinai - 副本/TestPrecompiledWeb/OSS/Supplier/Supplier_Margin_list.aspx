<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html>

<script runat="server">
    ITools tools;

    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("d7c53f80-5ad9-4bc4-8f4e-cc31144f7de6");
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>保证金标准管理</title>
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
      <td class="content_title">保证金标准管理&nbsp;&nbsp;<a href="Supplier_Margin_add.aspx">[添加保证金]</a></td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr><td>
    
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: '/supplier/Supplier_Ｍargin_do.aspx?action=list',
            datatype: "json",
            colNames: ['ID', '供货商类型', '保证金金额', "操作"],
            colModel: [
				{ width: 50, name: 'SupplierMarginInfo.Supplier_Margin_ID', index: 'SupplierMarginInfo.Supplier_Margin_ID', align: 'center' },
                { width: 100, name: 'SupplierMarginInfo.Supplier_Margin_Type', index: 'SupplierMarginInfo.Supplier_Margin_Type', align: 'center' },
				{ name: 'SupplierMarginInfo.Supplier_Margin_Amount', index: 'SupplierMarginInfo.Supplier_Margin_Amount', align: 'center' },
				{ width: 100, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'SupplierMarginInfo.Supplier_Margin_ID',
            sortorder: "desc",
            rowNum: 10,
            rowList: [10, 20, 40],
            pager: 'pager',
            multiselect: false,
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%"
        });
        </script>
      </td>
    </tr>
  </table>
  </td>
  </tr>
  </table>
</div>
</body>
</html>
