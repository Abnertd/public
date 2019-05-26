<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    Product myApp;

    string listtype, searchtype, keyword, ReqURL;
    int product_cate, product_supplier, TheirType;
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

        if (listtype == "")
        {
            listtype = "normal";
        }

        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

        //ReqURL = "listtype=" + listtype + "&searchtype=" + searchtype + "&keyword=" + Server.UrlEncode(keyword) + "&product_cate=" + product_cate;

        if (keyword != "输入商品编码、商品名称、供应商名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称、供应商名称进行搜索";
        }
        if (keyword == "输入商品编码、商品名称、供应商名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        TheirType = tools.CheckInt(Request["TheirType"]);

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
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td>
                    <form action="product.aspx?listtype=<%=listtype %>" method="post" name="frm_sch" id="frm_sch">
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr bgcolor="#F5F9FC">
                                <td align="right">
                                    <span class="left_nav">搜索</span>
                                    <%--所属类型：
                    <select name="TheirType">
                        <option value="0">全部</option>
                        <option value="1" <%=(TheirType == 1 ? "selected=\"selected\"" : "")%>>系统</option>
                        <option value="2" <%=(TheirType == 2 ? "selected=\"selected\"" : "")%>>供应商</option>
                    </select>--%>
                    供应商：<%=myApp.Product_Supplier_Select(product_supplier, "product_supplier")%>
                    分类：<span id="main_cate"><%=myApp.Product_Category_Select(product_cate, "main_cate")%></span>
                                    <input type="text" name="keyword" size="70" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、供应商名称进行搜索'){this.value='';}"
                                        value="<% =keyword %>" />
                                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                                </td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td class="content_content">


                    <table id="list"></table>
                    <div id="pager"></div>
                    <script type="text/javascript">
                        jQuery("#list").jqGrid({
                            url: 'product_do.aspx?action=list&type=product&<%=ReqURL%>',
            datatype: "json",
			<%if (listtype != "normal")
     { %>
                            //colNames: ["ID", "商品编码", "商品名称", "供应商", "商品类型", "商品价格", "库存", "销售状态", "审核状态", "排序", "操作"],
                            colNames: ["ID",  "商品名称", "供应商", "商品类型", "商品价格", "库存", "销售状态", "审核状态", "排序", "操作"],
            <%}
     else
     {
            %>
                            //colNames: ["ID", "商品编码", "商品名称", "供应商", "商品类型", "商品价格", "库存", "销售状态", "审核状态", "排序", "操作"],
                            colNames: ["ID", "商品名称", "供应商", "商品类型", "商品价格", "库存", "销售状态", "审核状态", "排序", "操作"],
            <%
            }%>

            colModel: [
				{ width: 40, name: 'ProductInfo.Product_ID', index: 'ProductInfo.Product_ID', align: 'center' },
				//{ width: 70, name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code', align: 'center' },
				{ width: 90, name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name' },
				{ width: 80, align: 'center', name: 'ProductInfo.Product_SupplierID', index: 'ProductInfo.Product_SupplierID' },
				{ width: 50, align: 'center', name: 'ProductInfo.Product_TypeID', index: 'ProductInfo.Product_TypeID' },
                //{ width: 80, name: 'ProductInfo.Product_PriceType', index: 'ProductInfo.Product_PriceType', align: 'center' },
				{ width: 40, name: 'ProductInfo.Product_Price', index: 'ProductInfo.Product_Price', align: 'center' },
				{ width: 30, name: 'ProductInfo.Product_StockAmount', index: 'ProductInfo.Product_StockAmount', align: 'center' },
				{ width: 40, name: 'ProductInfo.Product_IsInsale', index: 'ProductInfo.Product_IsInsale', align: 'center' },
				{ width: 25, name: 'ProductInfo.Product_IsAudit', index: 'ProductInfo.Product_IsAudit', align: 'center' },
				{ width: 20, name: 'ProductInfo.Product_Sort', index: 'ProductInfo.Product_Sort', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false }
            ],
			<%if (listtype != "normal")
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
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%"
        });
        $("#list").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'scroll' });
                    </script>

                    <form action="/product/product_do.aspx" method="post">
                        <div style="margin-top: 5px;">
                            <% if (Public.CheckPrivilege("dbc8af48-4896-487e-9e25-2efc64aa9f5b"))
                               { %>
                            <input type="button" id="Button3" class="bt_orange" value="审核" onclick="location.href = 'product_do.aspx?action=audit&product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <input type="button" id="Button6" class="bt_orange" value="审核不通过" onclick="location.href = 'product_do.aspx?product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <input type="button" id="Button4" class="bt_orange" value="取消审核" onclick="location.href = 'product_do.aspx?action=cancelaudit&product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <%} %>

                            <% if (Public.CheckPrivilege("15bbb62f-0913-49a9-8b00-cd8cc91e8a9e"))
                               { %>
                            <input type="button" id="Button2" class="bt_orange" value="上架" onclick="location.href = 'product_do.aspx?action=insale&product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <input type="button" id="Button5" class="bt_orange" value="下架" onclick="location.href = 'product_do.aspx?action=cancelinsale&product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <%} %>

                            <% if (Public.CheckPrivilege("56b7d7ed-969a-45d5-b198-416fd0c2a3a4"))
                               { %>
                            <input type="button" id="export" class="bt_orange" value="导出勾选商品" onclick="location.href = 'product_do.aspx?action=productexport&product_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            <input type="button" id="Button1" class="bt_orange" value="导出全部商品" onclick="location.href = 'product_do.aspx?action=productallexport'" />
                            <%} %>
                        </div>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>

