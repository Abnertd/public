<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    Product myApp;
    Promotion myPromotion;
    ITools tools;
    string  searchtype, keyword, ReqURL;
    int product_cate;
    int limit, group;
    string defaultkey = "";
    string target = "";
    string productid;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();
        myApp = new Product();
        myPromotion = new Promotion();
        searchtype = tools.CheckStr(Request.QueryString["searchtype"]);
        productid = tools.NullStr(Session["selected_productid"]);
        keyword = Request["keyword"];
        target = tools.NullStr(Request["target"]);
        limit = tools.CheckInt(Request["limit"]);
        group = tools.CheckInt(Request["group"]);
        product_cate = tools.CheckInt(Request["product_cate"]);

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
        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&product_cate=" + product_cate + "&target=" + target;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
<script src="/Scripts/product.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

</head>
<body>
  <table width="600" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td>
      <table width="100%" cellpadding="0" cellspacing="0" border="0" class="picker_tittab">
      <tr><td class="picker_tit">产品选择</td><td width="30" align="center"><a href="javascript:void(0);" onclick="close_picker();"><img src="/images/close.gif" border="0"/></a></td></tr>
      </table>
      </td>
    </tr>
    <tr><td>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
	  <tr bgcolor="#F5F9FC" >
		<td align="right"><span class="left_nav">搜索</span> 
		<span id="main_cate"><%=myApp.Product_Category_Select(product_cate, "main_cate")%></span>
		 <input type="text" name="keyword" size="30" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索'){this.value='';}" value="<% =keyword %>" />
		 <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" onclick="SelectProduct('', true)" /></td>
	  </tr>
	</table>
	
    </td></tr>
    <tr>
      <td height="360" valign="top" id="tdTable"></td>
    </tr>
  </table>
  
<script type="text/javascript">
    SelectProduct('<%=ReqURL %>', false);
    
    function SelectProduct(requestparam, isauto) {
        $("#tdTable").html("<table id=\"list\"></table><div id=\"pager\"></div>");

        if (isauto) {
            var product_cate = $('#Product_cate').val() == '0' ? $('#Product_cate_parent').val() : $('#Product_cate').val();
            requestparam = "keyword=" + encodeURI($('#keyword').val()) + "&product_cate=" + product_cate;
        }
        
        var tableList = jQuery("#list");
        tableList.jqGrid({
            url: '/scm/purchasing/purchasing_do.aspx?action=selectproduct&' + requestparam,
            datatype: "json",
            colNames: ['ID', '商品编码', '商品名称', '生产企业'],
            colModel: [
            { width: 30, align: 'center', name: 'id', index: 'id', sortable: false },
            { align: 'left', name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code', sortable: false },
	        { align: 'left', name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name', sortable: false },
	        { align: 'left', name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker', sortable: false }
            ],
            sortname: 'ProductInfo.Product_ID',
            sortorder: "desc",
            rowNum: 10,
            pager: 'pager',
            multiselect: false,
            viewrecords: true,
            viewsortcols: [false, 'horizontal', true],
            width: 597,
            height: "100%",
            ondblClickRow: function(rowid) {
                $("#Purchasing_ProductCode").val($("#list").getCell(rowid, 1));
                ViewProductDetail($("#list").getCell(rowid, 1));
                close_picker();
            }
        });

        tableList.trigger("reloadGrid");
    }
</script>
  
</body>
</html>
