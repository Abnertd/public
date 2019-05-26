<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;

    string QueryURL = "statistical_do.aspx?action=productbuyvisit";
    
    string startDate = "";
    string endDate = "";
    string orders_status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("240b888b-a0cf-4683-8798-bddafc443431");

        tools = ToolsFactory.CreateTools();
        
        startDate = tools.CheckStr(Request.Form["startDate"]);
        endDate = tools.CheckStr(Request.Form["endDate"]);
        orders_status = tools.CheckStr(Request.Form["orders_status"]);

        if (orders_status.Length == 0) { orders_status = "confirm"; }

        QueryURL += "&startDate=" + startDate + "&endDate=" + endDate + "&orders_status=" + orders_status;
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

<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>


</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">商品访问购买次数</td>
    </tr>
    <tr>
      <td class="content_content">
      
        <form action="" id="frmsearch" method="post">
        <table cellpadding="0" cellspacing="0">
          <tr>
            <td>分析时间段： <input type="text" class="input_calendar" name="startDate" id="startDate" maxlength="10" readonly="readonly" value="<%=startDate%>" />
            至 <input type="text" class="input_calendar" name="endDate" id="endDate" maxlength="10" readonly="readonly" value="<%=endDate%>" />
                <script type="text/javascript">
                    $(document).ready(function() {
                        $("#startDate").datepicker({ numberOfMonths: 2 });
                        $("#endDate").datepicker({ numberOfMonths: 2 });
                    });
                </script>
            </td>
            <td width="5"></td>
            <td>订单状态
                <select id="orders_status" name="orders_status">
                    <option value="confirm" <% =Public.CheckedSelected("confirm", orders_status)%> >已确认及已成功</option>
                    <option value="success" <% =Public.CheckedSelected("success", orders_status)%>>已成功</option>
                </select>
            </td>
            <td width="5"></td>
            <td><input type="submit" id="btnsubmit" value="分析" class="bt_orange" />
            <input type="button" id="output" value="导出" class="bt_orange" onclick="location.href='statistical_do.aspx?action=productbuy_export&startdate='+$('#startDate').val()+'&enddate='+$('#endDate').val()+'&orders_status=' + $('#orders_status').val();" /></td>
          </tr>
        </table>
        </form>
        
        <div style="height:5px;"></div>
      
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: '<% =QueryURL %>',
			datatype: "json",
            colNames: ['ID', "商品编码", '商品名称', "商品通用名", "商品类型", "规格", '访问次数', '购买次数', '访问购买率'],
            colModel: [
				{width:40, align: 'center',align: 'center', sortable:false},
				{width:90, align: 'center',sortable:false},
				{sortable:false},
				{sortable:false},
				{width:70, align: 'center',sortable:false},
				{width:70, align: 'center',sortable:false},
				{width:60,  name: 'xl', index: 'SUM(Orders_Goods_Amount)', align: 'center', sortable:false},
				{width:60,  name: 'xse', index: 'COUNT(DISTINCT Orders_ID)', align: 'center'},
				{width:80, align: 'center', name: 'xse', index: 'SUM(Orders_Goods_Product_Price * Orders_Goods_Amount)', align: 'center', sortable:false},
			],
            sortname: 'COUNT(DISTINCT Orders_ID)',
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