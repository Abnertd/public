<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    Statistic myApp;
    
    string startDate = "";
    string endDate = "";
    string orders_status = "";
    
    int OrdersCount, MemberCount,BuyMemberCount;
    double OrdersAllPrice, OrdersAveragePrice;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("46cf719e-2722-44de-a7c8-4d96c46102e3");

        tools = ToolsFactory.CreateTools();
        myApp = new Statistic();
        
        startDate = tools.CheckStr(Request.Form["startDate"]);
        endDate = tools.CheckStr(Request.Form["endDate"]);
        orders_status = tools.CheckStr(Request.Form["orders_status"]);

        if (orders_status.Length == 0) { orders_status = "confirm"; }

        OrdersCount = myApp.GetOrdersCountByDate(startDate, endDate, orders_status);
        OrdersAllPrice = myApp.GetOrdersAllPriceByDate(startDate, endDate, orders_status);
        MemberCount = myApp.GetMemberCount();
        BuyMemberCount = myApp.GetBuyMemberCount(startDate, endDate);

        if (OrdersCount == 0) {
            OrdersAveragePrice = 0;
        }
        else {
            OrdersAveragePrice = OrdersAllPrice / OrdersCount;
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

<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>


</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">销售指标分析</td>
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
            <td><input type="submit" id="btnsubmit" value="分析" class="bt_orange" /></td>
          </tr>
        </table>
        </form>
        
        <div style="height:5px;"></div>
        <table id="list" width="100%" cellspacing="10" style="border:dotted 1px #ccc;">
            <tr>
                <td>
                <div style="margin:5px; font-size:14px; font-weight:bold;">客户平均订单金额</div>
                <table width="100%" class="list_table_bg" cellspacing="1">
                    <tr class="list_head_bg">
                        <td>总订单金额</td>
                        <td>总订单数</td>
                        <td>平均订单金额</td>
                    </tr>
                    <tr class="list_td_bg">
                        <td><%=Public.DisplayCurrency(OrdersAllPrice)%></td>
                        <td><%=OrdersCount%></td>
                        <td><%=Public.DisplayCurrency(OrdersAveragePrice)%></td>
                    </tr>
                </table>
                </td>
                <td>
                <div style="margin:5px; font-size:14px; font-weight:bold;">平均会员订单量</div>
                <table width="100%" class="list_table_bg" cellspacing="1">
                    <tr class="list_head_bg">
                        <td>总订单数</td>
                        <td>总会员数</td>
                        <td>平均会员订单量</td>
                    </tr>
                    <tr class="list_td_bg">
                        <td><%=OrdersCount%></td>
                        <td><%=MemberCount%></td>
                        <td><%=Math.Round(((double)OrdersCount / MemberCount),2)%></td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td>
                <div style="margin:5px; font-size:14px; font-weight:bold;">注册会员购买率</div>
                <table width="100%" class="list_table_bg" cellspacing="1">
                    <tr class="list_head_bg">
                        <td>有过订单的会员数</td>
                        <td>总会员数</td>
                        <td>注册会员购买率</td>
                    </tr>
                    <tr class="list_td_bg">
                        <td><%=BuyMemberCount%></td>
                        <td><%=MemberCount%></td>
                        <td><%=((double)BuyMemberCount / MemberCount) * 100%>%</td>
                    </tr>
                </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
      </td>
    </tr>
  </table>
</div>
</body>
</html>