<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    Product myApp;
    
    string listtype, searchtype, keyword, ReqURL;
    int product_cate,product_supplier;
    string defaultkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8");

        tools = ToolsFactory.CreateTools();
        myApp = new Product();

        listtype = tools.CheckStr(Request.QueryString["listtype"]);
        searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
        keyword = Request["keyword"];
        product_supplier = tools.CheckInt(Request["product_supplier"]);
        product_cate = tools.CheckInt(Request["product_cate"]);
        
        if (listtype == "") {
            listtype = "normal";
        }

        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

        //ReqURL = "listtype=" + listtype + "&searchtype=" + searchtype + "&keyword=" + Server.UrlEncode(keyword) + "&product_cate=" + product_cate;
        
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
        ReqURL = "listtype=" + listtype + "&keyword=" + Server.UrlEncode(defaultkey) + "&product_cate=" + product_cate + "&product_supplier=" + product_supplier;
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
      <td class="content_title">商品管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="product.aspx?listtype=<%=listtype %>" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					供应商：<%=myApp.Product_Supplier_Select(product_supplier, "product_supplier")%>
					分类：<span id="main_cate"><%=myApp.Product_Category_Select(product_cate, "main_cate")%></span>
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
            url: 'product_do.aspx?action=list&type=seo&<%=ReqURL%>',
			datatype: "json",
			<%if(listtype!="normal")
			{ %>
            colNames: ["ID", "商品编码", "商品名称", "供应商","商品类型",   "市场价","本站价格", "销售状态 ","审核状态 ","排序 ","操作"],
            <%} 
            else
            {
            %>
            colNames: ["ID", "商品编码", "商品名称",  "供应商","商品类型","市场价","本站价格", "销售状态 ","审核状态 ","排序 ","操作"],
            <%
            }%>
            
            colModel: [
				{ width: 40, name: 'ProductInfo.Product_ID', index: 'ProductInfo.Product_ID', align: 'center'},
				{ width: 70, name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code'},
				{ width: 80, name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name'},
				//{ width: 80, name: 'ProductInfo.Product_SubName', index: 'ProductInfo.Product_SubName'},
				{ width: 80, align:'center',name: 'ProductInfo.Product_Supplier', index: 'ProductInfo.Product_Supplier'},
				{ width: 50, align:'center', name: 'ProductInfo.Product_TypeID', index: 'ProductInfo.Product_TypeID'},
				//{ width: 80, align:'center', name: 'ProductInfo.Product_Spec', index: 'ProductInfo.Product_Spec'},
				//{ width: 80, name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker'},
				{ width: 80, name: 'ProductInfo.Product_MKTPrice', index: 'ProductInfo.Product_MKTPrice', hidden: true},
				{ width: 80, name: 'ProductInfo.Product_Price', index: 'ProductInfo.Product_Price'},
				//{ width: 30, name: 'ProductInfo.Product_StockAmount', index: 'ProductInfo.Product_StockAmount', align: 'center'},
				{ width: 40, name: 'ProductInfo.Product_IsInsale', index: 'ProductInfo.Product_IsInsale', align: 'center'},
				{ width: 40, name: 'ProductInfo.Product_IsAudit', index: 'ProductInfo.Product_IsAudit', align: 'center'},
				{ width: 30,name: 'ProductInfo.Product_Sort', index: 'ProductInfo.Product_Sort', align: 'center'},
				
				{width: 180, name: 'Operate', index: 'Operate', align: 'center', sortable:false}
			],
			<%if(listtype!="normal")
			{ %>
            sortname: 'ProductInfo.Product_ID',
            sortorder: "desc",
            <%} 
            else
            {
            %>
            sortname: 'ProductInfo.Product_Sort',
            sortorder: "asc",
            <%
            }%>
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager', 
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        $("#list").closest(".ui-jqgrid-bdiv").css({'overflow-x':'scroll'});
        </script>
        
        
      </td>
    </tr>
  </table>
</div>
</body>
</html>

