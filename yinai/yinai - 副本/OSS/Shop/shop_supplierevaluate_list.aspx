<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private ProductReview myReview;
    private int recommend_status, audit_status, view_status;
    private ITools tools;
    private string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("74500aee-9f7e-4939-a039-61fafe790776");
        myReview = new ProductReview();
        string action = Request["action"];
        if (action == "recommend" || action == "recommendcancel" || action == "show" || action == "showcancel")
        {
            myReview.EditProductReview(action);
        }

        defaultkey = "";
        keyword = Request["keyword"];
        audit_status = tools.CheckInt(Request["audit_status"]);
        if (keyword == null || keyword.Length == 0)
        {
            keyword = "输入合同编号、供应商、评论人、产品名称进行搜索";
        }

        if (keyword == "输入合同编号、供应商、评论人、产品名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&audit_status=" + audit_status;
    }
    
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商家评价管理</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
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
                <td class="content_title">商家评价管理</td>
            </tr>
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <form action="shop_evaluate_list.aspx" method="post" name="frm_sch" id="frm_sch">
                            <tr bgcolor="#F5F9FC">
                                <td align="right">
                                    <span class="left_nav">审核状态</span>
                                    <select name="audit_status">
                                        <option value="0" <%=Public.CheckedSelected("0",audit_status.ToString()) %>>全部</option>
                                        <option value="1" <%=Public.CheckedSelected("1",audit_status.ToString()) %>>已审核</option>
                                        <option value="2" <%=Public.CheckedSelected("2",audit_status.ToString()) %>>未审核</option>
                                    </select>
                                    <span class="left_nav">搜索</span>
                                    <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入合同编号、供应商、评论人、产品名称进行搜索'){this.value='';}" value="<% =keyword %>">
                                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
                            </tr>
                        </form>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <table id="list"></table>
                    <div id="pager"></div>
                    <script type="text/javascript">
                        jQuery("#list").jqGrid({
                            url: 'shop_evaluate_do.aspx?action=supplier_evaluate_list&<%=ReqURL %>',
            datatype: "json",
            colNames: ['ID', '订单编号', '供应商', '会员', "服务评分", "发货评分", "审核状态", "操作"],
            colModel: [
				{ width: 50, name: 'SupplierShopEvaluateInfo.Shop_Evaluate_ID', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_ID', align: 'center' },
				{ width: 80, align: 'center', name: 'SupplierShopEvaluateInfo.Shop_Evaluate_ContractID', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_ContractID' },
				{ name: 'SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID', align: 'center' },
				{ width: 100, name: 'SupplierShopEvaluateInfo.Shop_Evaluate_MemberId', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_MemberId' },
				//{ name: 'SupplierShopEvaluateInfo.Shop_Evaluate_Productid', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_Productid', align: 'left'},
				{ width: 50, name: 'SupplierShopEvaluateInfo.Shop_Evaluate_Service', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_Service', align: 'center' },
				{ width: 50, name: 'SupplierShopEvaluateInfo.Shop_Evaluate_Delivery', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_Delivery', align: 'center' },
				//{ width:50,name: 'SupplierShopEvaluateInfo.Shop_Evaluate_Product', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_Product', align: 'center'},
				{ width: 50, name: 'SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck', index: 'SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck', align: 'center' },
				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'SupplierShopEvaluateInfo.Shop_Evaluate_ID',
            sortorder: "desc",
            rowNum: GetrowNum(),
            rowList: GetrowList(),
            pager: 'pager',
            multiselect: true,
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%"
        });
                    </script>
                   <%-- <%if (Public.CheckPrivilege("532bf3c0-e243-4a10-a664-b764fa7c23f4"))
                      { %>
                    <form action="/shop/shop_evaluate_do.aspx" method="post">
                        <div style="margin-top: 5px;">
                            <div style="margin-top: 5px;">
                                <input type="button" id="export" class="bt_orange" value="审核通过" onclick="location.href = 'shop_evaluate_do.aspx?action=shop_evaluate_audit&Shop_Evaluate_ID=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                                <input type="button" id="Button1" class="bt_orange" value="审核不通过" onclick="location.href = 'shop_evaluate_do.aspx?action=shop_evaluate_deny&Shop_Evaluate_ID=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                            </div>
                    </form>
                    <%} %>--%>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
