<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    string listtype, titleName;
    string keyword, date_start, date_end;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("493abbf7-121b-4a1e-a5da-977afb3d6718");
        tools = ToolsFactory.CreateTools();
        listtype = tools.CheckStr(Request.QueryString["listtype"]);

        if (listtype == "refund") 
        {
            titleName = "退款单"; 
        }
        else { titleName = "付款单"; }

        //关键词
        keyword = tools.CheckStr(Request["keyword"]);

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);
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
      <td class="content_title"><% =titleName%>管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
        <form action="orders_payment_list.aspx?listtype=<%=listtype %>" method="post" name="frm_sch" id="frm_sch">
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tr bgcolor="#F5F9FC">
                <td align="right">
                    <span class="left_nav">搜索</span> 起始日期：
                    <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10"
                        readonly="readonly" value="<%=date_start %>" />
                    -
                    <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                        readonly="readonly" value="<%=date_end%>" />

                    <script type="text/javascript">
                        $(document).ready(function() {
                            $("#date_start").datepicker({ numberOfMonths: 1 });
                            $("#date_end").datepicker({ numberOfMonths: 1 });
                        });
                    </script>

                    合同编号：<input type="text" name="keyword" id="keyword" value="<% =keyword %>">
                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                </td>
            </tr>
        </table>
        </form>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
       <script type="text/javascript">
           jQuery("#list").jqGrid({
               url: 'orders_payment_do.aspx?action=list&listtype=<% =listtype%>&keyword=<%=keyword %>&date_start=<%=date_start %>&date_end=<%=date_end %>',
            datatype: "json",
            colNames: ['ID', '订单编号', '单据编码', '金额', '支付方式', '操作时间', "操作"],
            colModel: [
				{ width: 50, name: 'id', index: 'id', align: 'center', sortable: false },
				{ width: 100, name: 'OrdersPaymentInfo.Orders_Payment_OrdersID', index: 'OrdersPaymentInfo.Orders_Payment_OrdersID', align: 'center' },
				{ name: 'OrdersPaymentInfo.Orders_Payment_DocNo', index: 'OrdersPaymentInfo.Orders_Payment_DocNo', align: 'center' },
				{ width: 100, name: 'OrdersPaymentInfo.Orders_Payment_Amount', index: 'OrdersPaymentInfo.Orders_Payment_Amount', align: 'center' },
				{ width: 100, name: 'OrdersPaymentInfo.Orders_Payment_Name', index: 'OrdersPaymentInfo.Orders_Payment_Name', align: 'center' },
				{ width: 100, name: 'OrdersPaymentInfo.Orders_Payment_Addtime', index: 'OrdersPaymentInfo.Orders_Payment_Addtime', align: 'center' },
				{ width: 60, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'OrdersPaymentInfo.Orders_Payment_ID',
            sortorder: "desc",
            rowNum: 15,
            rowList: [15, 30, 45],
            pager: 'pager',
            multiselect: true,
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%",
            shrinkToFit: true
        });
        </script>
        <form action="/orderspayment/orders_payment_do.aspx" method="post">
        <div style="margin-top:5px;">
        <div style="margin-top:5px;">
        
        <% if (Public.CheckPrivilege("b52048ac-508e-4bf6-9327-9ff77a32e54c")) { %>
        <input type="button" id="export" class="bt_orange" value="导出勾选<% =titleName%>" onclick="location.href='orders_payment_do.aspx?action=paymentexport&payment_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%}%>
        
        </div>
        </form>
      </td>
    </tr>
  </table>
</div>
</body>
</html>