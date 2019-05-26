<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">

    private ProductType producttype;
    private ITools tools;

    private int ProductType_ID, ProductType_Sort, ProductType_IsActive;
    private string ProductType_Name, ProductType_Site;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("847e8136-fd2f-4834-86b7-f2c984705eff");
        
        producttype = new ProductType();
        tools = ToolsFactory.CreateTools();

        ProductType_ID = tools.CheckInt(Request.QueryString["ProductType_ID"]);
        if (ProductType_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ProductTypeInfo entity = producttype.GetProductTypeByID(ProductType_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            ProductType_ID = entity.ProductType_ID;
            ProductType_Name = entity.ProductType_Name;
            ProductType_IsActive = entity.ProductType_IsActive;
            ProductType_Site = entity.ProductType_Site;
            ProductType_Sort = entity.ProductType_Sort;
        }
    }
</script>
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
      <td class="content_title"><%=ProductType_Name %>属性 [<a href="ProductTypeExtend_Add.aspx?producttype_id=<%=ProductType_ID %>">添加新属性</a>]</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'ProducttypeExtend_do.aspx?action=list&producttype_id=<%=ProductType_ID %>',
			datatype: "json",
            colNames: ['ID', '扩展属性名称',"表现方式","筛选项", "排序", "操作"],
            colModel: [
				{width:50, name: 'ProductType_Extend.ProductType_Extend_ID', index: 'ProductType_Extend.ProductType_Extend_ID', align: 'center'},
				{ name: 'ProductType_Extend.ProductType_Extend_Name', index: 'ProductType_Extend.ProductType_Extend_Name'},
				{width:50, name: 'ProductType_Extend.ProductType_Extend_Display', index: 'ProductType_Extend.ProductType_Extend_Display', align: 'center'},
				{width:30, name: 'ProductType_Extend.ProductType_Extend_IsSearch', index: 'ProductType_Extend.ProductType_Extend_IsSearch', align: 'center'},
				{width:30,align:'center', name: 'ProductType_Extend.ProductType_Extend_Sort', index: 'ProductType_Extend.ProductType_Extend_Sort'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:true},
			],
            sortname: 'ProductType_Extend.ProductType_Extend_ID',
			sortorder: "desc",
			rowNum: 0,
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
