<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string keyword="";
    int product_cate;
    string defaultkey = "";
    private ITools tools;
    Product myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        myApp = new Product();
        tools = ToolsFactory.CreateTools();
        product_cate = tools.CheckInt(Request["product_cate"]);

        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }
        
        keyword = Request["keyword"];
        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索";
        }
        if (keyword == "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
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
<script src="/scripts/product.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">商品选择</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="product_select.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					<span id="main_cate"><%=myApp.Product_Category_Select(product_cate, "main_cate")%></span>
					 <input type="text" name="keyword" size="70" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'ordercreate_do.aspx?action=productlist&product_cate=<%=product_cate %>&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID', '商品编码',  '商品名称', '商品规格','生产企业','市场价格','本站价格'],
            colModel: [
				{ width:50,name: 'ProductInfo.Product_ID', index: 'ProductInfo.Product_ID', align: 'center'},
				{ width:110,align:'center',name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code'},
				{ name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name'},
				{ width:110,align:'center',name: 'ProductInfo.Product_Spec', index: 'ProductInfo.Product_Spec'},
				{ width: 150, name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker' },
				{ width: 80, align: 'center', name: 'ProductInfo.Product_MKTPrice', index: 'ProductInfo.Product_MKTPrice', hidden: true },
				{ width: 80, align: 'center', name: 'ProductInfo.Product_Price', index: 'ProductInfo.Product_Price' },
			],
            sortname: 'ProductInfo.Product_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager',
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%",
			shrinkToFit:true
        });
        </script>
        <div>
        <input type="button" value="选择" class="btn_01" onclick="$.ajaxSetup({async: false});$('#goods_tmpinfo',window.opener.document).load('/ordercreate/ordercreate_do.aspx?action=addproduct&product_id=' + jQuery('#list').jqGrid('getGridParam','selarrrow') + '&fresh=' + Math.random() + '');window.close();">
        </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>