<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    string listtype, titleName;
    string keyword, date_start, date_end;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("f606309a-2aa9-42e3-9d45-e0f306682a29");
        tools = ToolsFactory.CreateTools();

        listtype = tools.CheckStr(Request.QueryString["listtype"]);

        if (listtype == "returned") { titleName = "退货单"; }
        else { titleName = "发货单"; }

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
        <form action="orders_delivery_list.aspx?listtype=<%=listtype %>" method="post" name="frm_sch"
        id="frm_sch">
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
                url: 'orders_delivery_do.aspx?action=list&listtype=<% =listtype%>&keyword=<%=keyword %>&date_start=<%=date_start %>&date_end=<%=date_end %>',
            datatype: "json",
            colNames: ['ID', '订单编号', '单据编码', '物流费用', '操作时间', "操作"],
            colModel: [
				{ width: 50, name: 'id', index: 'id', align: 'center', sortable: false },
				{ name: 'OrdersDeliveryInfo.Orders_Delivery_OrdersID', index: 'OrdersDeliveryInfo.Orders_Delivery_OrdersID', align: 'center' },
				{ name: 'OrdersDeliveryInfo.Orders_Delivery_DocNo', index: 'OrdersDeliveryInfo.Orders_Delivery_DocNo', align: 'center' },
				{ name: 'OrdersDeliveryInfo.Orders_Delivery_Amount', index: 'OrdersDeliveryInfo.Orders_Delivery_Amount', align: 'center' },
				{ name: 'OrdersDeliveryInfo.Orders_Delivery_Addtime', index: 'OrdersDeliveryInfo.Orders_Delivery_Addtime', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'OrdersDeliveryInfo.Orders_Delivery_ID',
            sortorder: "desc",
            rowNum: 15,
            rowList: [15, 30, 45],
            pager: 'pager',
            multiselect: true,
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%"
        });
        </script>
        <form action="/ordersdelivery/orders_delivery_do.aspx" method="post">
        <div style="margin-top:5px;">
        <div style="margin-top:5px;">
        
        <% if (Public.CheckPrivilege("453d0181-57f9-4041-9e10-6f42d12cd43f"))
           { %>
        <input type="button" id="export" class="bt_orange" value="导出勾选<% =titleName%>" onclick="location.href='orders_delivery_do.aspx?action=deliveryexport&delivery_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%  }%>
        </div>
        </form>
      </td>
    </tr>
  </table>
</div>
</body>
</html>