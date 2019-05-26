<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c558f983-68ec-4a91-a330-1c1f04ebdf01");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺样式管理</title>
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
      <td class="content_title">店铺等级管理</td>
    </tr>
    <tr><td height="5"></td></tr>

    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
            url: 'Shop_Grade_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '等级名称', '审核状态' ,"操作"],
            colModel: [

				{ width: 50, name: 'SupplierShopGradeInfo.Shop_Grade_ID', index: 'SupplierShopGradeInfo.Shop_Grade_ID', align: 'center', sortable: false },
                { name: 'SupplierShopGradeInfo.Shop_Grade_Name', index: 'SupplierShopGradeInfo.Shop_Grade_Name', align: 'left' },
                 {width:80, name: 'SupplierShopGradeInfo.Shop_Grade_IsActive', index: 'SupplierShopGradeInfo.Shop_Grade_IsActive', align:'center' },
   				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierShopGradeInfo.Shop_Grade_ID',
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