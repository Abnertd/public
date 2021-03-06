﻿<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    string pagerul="";
    string VisitURL = "orders_do.aspx?action=list";
    string orders_status, orders_paymentstatus, orders_deliverystatus,orders_source, orders_invoicestatus, orders_admin_sign, keyword,date_start,date_end;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("5e807815-409c-4d01-8e1a-2f835fbf2ac5");
        tools = ToolsFactory.CreateTools();
        
        //订单状态
        orders_status = tools.CheckStr(Request.QueryString["orders_status"]);
        if (orders_status.Length == 0) { orders_status = "-1"; }
        VisitURL += "&orders_status=" + orders_status;
        
        //付款状态
        orders_paymentstatus = tools.CheckStr(Request.QueryString["orders_paymentstatus"]);
        if (orders_paymentstatus.Length == 0) { orders_paymentstatus = "-1"; }
        VisitURL += "&orders_paymentstatus=" + orders_paymentstatus;

        //配送状态
        orders_deliverystatus = tools.CheckStr(Request.QueryString["orders_deliverystatus"]);
        if (orders_deliverystatus.Length == 0) { orders_deliverystatus = "-1"; }
        VisitURL += "&orders_deliverystatus=" + orders_deliverystatus;

        //发票状态
        orders_invoicestatus = tools.CheckStr(Request.QueryString["orders_invoicestatus"]);
        if (orders_invoicestatus.Length == 0) { orders_invoicestatus = "-1"; }
        VisitURL += "&orders_invoicestatus=" + orders_invoicestatus;

        //订单标记
        orders_admin_sign = tools.CheckStr(Request.QueryString["orders_admin_sign"]);
        VisitURL += "&orders_admin_sign=" + orders_admin_sign;

        pagerul = VisitURL.Replace("orders_do", "orders_list");
        //关键词
        keyword = Request["keyword"];
        VisitURL += "&keyword=" + Server.UrlEncode(Request["keyword"]);

        keyword = Request["keyword"];
        if (keyword != "输入订单号、收货人姓名、电话、手机进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入订单号、收货人姓名、电话、手机进行搜索";
        }
        
        //订单来源
        orders_source = tools.CheckStr(Request["orders_source"]);
        if (orders_invoicestatus.Length == 0) { orders_invoicestatus = "0"; }
        VisitURL += "&orders_source=" + orders_source;

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);
        VisitURL += "&date_start=" + date_start;

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);
        VisitURL += "&date_end=" + date_end;

        
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
      <td class="content_title">订单管理</td>
    </tr>
    <tr>
      <td height="5"></td>
    </tr>
    <tr>
              <td>
              <form action="<%=pagerul%>" method="post" name="frm_sch" id="frm_sch" >
			  <table width="100%" border="0" cellspacing="0" cellpadding="5">
				  <tr bgcolor="#F5F9FC" >
					<td height="23" class="left_nav">
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=0")%>"><img src="/images/icon_star_off.gif" alt="标记默认" width="16" height="16" border="0"/></a>
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=1")%>"><img src="/images/icon_star.gif" alt="标记为星" width="16" height="16" border="0"/></a>
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=2")%>"><img src="/images/icon_clock.gif" alt="处理时间过长" width="16" height="16" border="0" /></a>
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=3")%>"><img src="/images/icon_alert.gif" alt="提示" width="16" height="16" border="0"/></a>
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=4")%>"><img src="/images/icon_fail.gif" alt="标记失败" width="16" height="16" border="0"/></a>
                      <a href="<%=pagerul.Replace("orders_admin_sign="+ orders_admin_sign, "orders_admin_sign=5")%>"><img src="/images/icon_success.gif" alt="标记成功" width="16" height="16" border="0"/></a></td>
					<td align="right"><span class="left_nav">搜索</span> 起始日期：
					<input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10" readonly="readonly" value="<%=date_start %>" /> - <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10" readonly="readonly" value="<%=date_end%>" />
                    <script type="text/javascript">
                        $(document).ready(function() {
                            $("#date_start").datepicker({ numberOfMonths: 1 });
                            $("#date_end").datepicker({ numberOfMonths: 1 });
                        });
                    </script>
					 <input type="text" name="keyword" id="keyword" size="50" onfocus="this.value=''" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				</table>
				</form>
				<table width="100%" border="0" cellspacing="0" cellpadding="5">
				  <tr bgcolor="#F5F9FC">
					<td height="23">
                    <table border="0" cellspacing="0" cellpadding="3">
                        <tr>
                          <td><strong>订单状态：</strong></td>
                          <td><a href="<%=pagerul.Replace("orders_status=" + orders_status, "orders_status=-1")%>"  class="<%if(orders_status=="-1"){Response.Write("filter_on");}else{Response.Write("filter_off");} %>">全部</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_status=" + orders_status, "orders_status=0")%>" class="<%if(orders_status=="0" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">未确认</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_status=" + orders_status, "orders_status=1")%>" class="<%if(orders_status=="1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">处理中</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_status=" + orders_status, "orders_status=2")%>" class="<%if(orders_status=="2" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">交易成功</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_status=" + orders_status, "orders_status=3")%>" class="<%if(orders_status=="3" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">交易失败</a></td>
                         
                          <td><strong>支付状态：</strong></td>
                          <td><a href="<%=pagerul.Replace("orders_paymentstatus=" + orders_paymentstatus, "orders_paymentstatus=-1")%>" class="<%if(orders_paymentstatus=="-1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">全部</a></td>
                        
                          <td><a href="<%=pagerul.Replace("orders_paymentstatus=" + orders_paymentstatus, "orders_paymentstatus=0")%>"  class="<%if(orders_paymentstatus=="0" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">未支付</a></td>
                         
                          <td><a href="<%=pagerul.Replace("orders_paymentstatus=" + orders_paymentstatus, "orders_paymentstatus=1")%>" class="<%if(orders_paymentstatus=="1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">已支付</a></td>
                          <td><a href="<%=pagerul.Replace("orders_paymentstatus=" + orders_paymentstatus, "orders_paymentstatus=4")%>" class="<%if(orders_paymentstatus=="4" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">已到账</a></td>
                          
                          <td><strong>发货状态：</strong></td>
                          <td><a href="<%=pagerul.Replace("orders_deliverystatus=" + orders_deliverystatus, "orders_deliverystatus=-1")%>" class="<%if(orders_deliverystatus=="-1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">全部</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_deliverystatus=" + orders_deliverystatus, "orders_deliverystatus=0")%>" class="<%if(orders_deliverystatus=="0" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">待采购</a></td>
                          <td><a href="<%=pagerul.Replace("orders_deliverystatus=" + orders_deliverystatus, "orders_deliverystatus=6")%>" class="<%if(orders_deliverystatus=="6" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">配货中</a></td>
                          <td><a href="<%=pagerul.Replace("orders_deliverystatus=" + orders_deliverystatus, "orders_deliverystatus=1")%>" class="<%if(orders_deliverystatus=="1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">已发货</a></td>
                          <td><strong>发票状态：</strong></td>
                          <td><a href="<%=pagerul.Replace("orders_invoicestatus=" + orders_invoicestatus, "orders_invoicestatus=-1")%>" class="<%if(orders_invoicestatus=="-1" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">全部</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_invoicestatus=" + orders_invoicestatus, "orders_invoicestatus=0")%>" class="<%if(orders_invoicestatus=="0" ){ Response.Write("filter_on");}else{ Response.Write("filter_off");}%>">未开票</a></td>
                          
                          <td><a href="<%=pagerul.Replace("orders_invoicestatus=" + orders_invoicestatus, "orders_invoicestatus=1")%>" class="<%if(orders_invoicestatus=="1"){ Response.Write("filter_on");}else{Response.Write("filter_off");}%>">已开票</a></td>
                      </tr>
                      </table></td>
				  </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: '<%=VisitURL%>',
			datatype: "json",
            colNames: ['ID', '订单号', '下单时间', '订单总额', '订单状态', '付款状态', '发货状态', '发票状态', '收货人', '付款方式', "操作"],
            colModel: [
				{ width:40, name: 'id', index: 'id', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_SN', index: 'OrdersInfo.Orders_SN', align: 'center'},
				{ width:60, name: 'OrdersInfo.Orders_Addtime', index: 'OrdersInfo.Orders_Addtime', align: 'center', sortable:false},
				{ width:70, name: 'OrdersInfo.Orders_Total_AllPrice', index: 'OrdersInfo.Orders_Total_AllPrice', align: 'center'},
				{ width:55, name: 'OrdersInfo.Orders_Status', index: 'OrdersInfo.Orders_Status', align: 'center'},
				{ width:55, name: 'OrdersInfo.Orders_PaymentStatus', index: 'OrdersInfo.Orders_PaymentStatus', align: 'center'},
				{ width:55, name: 'OrdersInfo.Orders_DeliveryStatus', index: 'OrdersInfo.Orders_DeliveryStatus', align: 'center'},
				{ width:65, name: 'OrdersInfo.Orders_InvoiceStatus', index: 'OrdersInfo.Orders_InvoiceStatus', align: 'center'},
				{ width:50, name: 'OrdersInfo.Orders_Address_Name', index: 'OrdersInfo.Orders_Address_Name', align: 'center'},
				{ width:55, name: 'OrdersInfo.Orders_Payway_Name', index: 'OrdersInfo.Orders_Payway_Name', align: 'center'},
				{ width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'OrdersInfo.Orders_ID',
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
        <form action="/orders/orders_do.aspx" method="post">
        <div style="margin-top:5px;">
        <div style="margin-top:5px;"><input type="button" id="export" class="bt_orange" value="导出商品清单" onclick="location.href='orders_do.aspx?action=ordergoodsexport&orders_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button1" class="bt_orange" value="导出订单信息" onclick="location.href='orders_do.aspx?action=ordersexport&orders_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        
        </div>
        </form>
      </td>
    </tr>
  </table>
</div>
</body>
</html>