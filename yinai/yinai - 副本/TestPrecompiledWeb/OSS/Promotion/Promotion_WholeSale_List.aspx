<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%  Public.CheckLogin("83aba19a-e789-43a9-8ffd-d9ba3efd2852");
    string keyword, defaultkey, ReqURL;
    int Status;
    ITools tools;
    tools = ToolsFactory.CreateTools();
    defaultkey = "";
    int group_id;
    group_id = tools.CheckInt(Request["group_id"]);
    keyword = Request["keyword"];
    if (keyword != "输入商品编码、商品名称、分组名称进行搜索" && keyword != null && keyword != "")
    {
        keyword = keyword;
    }
    else
    {
        keyword = "输入商品编码、商品名称、分组名称进行搜索";
    }
    if (keyword == "输入商品编码、商品名称、分组名称进行搜索")
    {
        defaultkey = "";
    }
    else
    {
        defaultkey = keyword;
    }
    ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&group_id=" + group_id;
    
    %>
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
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">批发促销</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td>
      <form action="promotion_WholeSale_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
	  <tr >
		<td><span class="left_nav">搜索</span> 
		 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、分组名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
	  </tr>
	</table>
	</form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Promotion_wholesale_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID','所属分组','商品编码', "商品名称", "规格","生产企业","本站价格","批发价格","最小购买数量", "操作"],
            colModel: [
				{width:50, name: 'PromotionWholeSaleInfo.Promotion_WholeSale_ID', index: 'PromotionWholeSaleInfo.Promotion_WholeSale_ID', align: 'center'},
				{width:80,align:'center', name: 'group_name', index: 'group_name', sortable:false},
				{width:80,align:'center', name: 'product_code', index: 'product_code', sortable:false},
				{align:'center', name: 'product_name', index: 'product_name', sortable:false},
				{width:100,align:'center', name: 'product_spec', index: 'product_spec', sortable:false},
				{width:150,align:'center', name: 'product_maker', index: 'product_maker', sortable:false},
				{width:80,align:'center', name: 'product_price', index: 'product_price', sortable:false},
	
				{width:80,align:'center', name: 'PromotionWholeSaleInfo.Promotion_WholeSale_Price', index: 'PromotionWholeSaleInfo.Promotion_WholeSale_Price'},
				{width:80,align:'center', name: 'PromotionWholeSaleInfo.Promotion_WholeSale_MinAmount', index: 'PromotionWholeSaleInfo.Promotion_WholeSale_MinAmount'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'PromotionWholeSaleInfo.Promotion_WholeSale_ID',
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
  </td>
    </tr>
  </table>
</div>
</body>
</html>
