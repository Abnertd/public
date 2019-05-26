<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string keyword;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("add11f44-d1eb-48bc-bc58-48673b91591a");
        tools = ToolsFactory.CreateTools();
        keyword = Request["keyword"];       
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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">待入库退换货申请管理</td>
    </tr>
        <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="orders_backlist.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					 订单号或申请人：<input type="text" name="keyword" id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'orders_back_do.aspx?action=list1&keyword=<%=Server.UrlEncode(Request["keyword"]) %>',
			datatype: "json",
            colNames: ['ID', '订单号', '退款金额', '操作类型','Email','申请人', '申请时间', '状态', "操作"],
            colModel: [
				{ width:50,name: 'back_id', index: 'back_id', align: 'center'},
				{ name: 'back_ordercode', index: 'back_ordercode', align: 'center'},
				{ width:50,name: 'back_amount', index: 'back_amount', align: 'center'},
				{ width:50,name: 'back_type', index: 'back_type', align: 'center'},
				{ width:100,name: 'back_email', index: 'back_email', align: 'center'},
				{ width:80,name: 'back_name', index: 'back_name', align: 'center'},
				{ width:150,name: 'back_addtime', index: 'back_addtime', align: 'center'},
				{ width:50,name: 'back_status', index: 'back_status', align: 'center'},
				{ width:100,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'OrdersBackApplyInfo.Orders_BackApply_ID',
			sortorder: "desc",
			rowNum: 10,
			rowList:[10,20,40], 
			pager: 'pager', 
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%",
			shrinkToFit:false
        });
        $("#list").closest(".ui-jqgrid-bdiv").css({'overflow-x':'scroll'});
        </script>
         <form action="/orders/orders_back_do.aspx" method="post">
        <div style="margin-top:5px;">
       
        <input type="button" id="Button1" class="bt_orange" value="导出勾选退货信息" onclick="location.href='orders_back_do.aspx?action=ordersbackexport&back_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        
        <input type="button" id="Button2" class="bt_orange" value="导出全部退货信息" onclick="location.href='orders_back_do.aspx?action=ordersbackexportall'" /> 
       
        
        </div>
        </form>
      </td>
    </tr>
  </table>
</div>
</body>
</html>