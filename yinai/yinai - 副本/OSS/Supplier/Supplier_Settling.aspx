<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    string keyword = "";
    string defaultkey = "";
    int status = 0;
    int Supplier_Trash = 0;
    int Supplier_Status = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools=ToolsFactory.CreateTools();
        Public.CheckLogin("00b42a78-2cef-4a22-865d-dd2ad9003ec5");
        keyword = tools.NullStr(Request["keyword"]);
        status = tools.CheckInt(Request["status"]);
        Supplier_Trash = tools.CheckInt(Request["Trash"]);
        Supplier_Status = tools.CheckInt(Request["Status"]);
        if (keyword == "输入Email、公司名称进行搜索"||keyword.Length==0)
        {
            keyword = "输入Email、公司名称进行搜索";
        }
        if (keyword == "输入Email、公司名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>供应商佣金结算</title>
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
      <td class="content_title">供应商佣金结算</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(status==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey), "待结算订单")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(status==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&status=1", "申请结算订单")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(status==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&status=2", "已结算订单")%></td>
      
      </tr>
      </table>
      </td></tr>
      <tr><td>
    <form action="Supplier_list.aspx?status=<%=status %>" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<tr >
				<td align="left"><span class="left_nav">搜索</span> 
					
					<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入Email、公司名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				</tr>
			</table>
            </form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Supplier_do.aspx?action=settinglist&keyword=<%=Server.UrlEncode(defaultkey) %>&status=<%=status %>',
			datatype: "json",
            colNames: ['ID', '供应商名称','订单号',  '订单状态', '结算状态',  '订单总额','产品金额','价格优惠','运费','运费优惠','结算价','佣金', '下单时间'],
            colModel: [
				{ width:40, name: 'id', index: 'id', align: 'center', sortable:false},
				{  name: 'Supplier_Name', index: 'Supplier_Name', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_SN', index: 'OrdersInfo.Orders_SN', align: 'center'},
				
				{ width:55, name: 'OrdersInfo.Orders_Status', index: 'OrdersInfo.Orders_Status', align: 'center'},
				{ width:55, name: 'OrdersInfo.Orders_IsSettling', index: 'OrdersInfo.Orders_IsSettling', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_Total_AllPrice', index: 'OrdersInfo.Orders_Total_AllPrice', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_Total_Price', index: 'OrdersInfo.Orders_Total_Price', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_Total_PriceDiscount', index: 'OrdersInfo.Orders_Total_PriceDiscount', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_Total_Freight', index: 'OrdersInfo.Orders_Total_Freight', align: 'center'},
				{ width:70, name: 'OrdersInfo.Orders_Total_FreightDiscount', index: 'OrdersInfo.Orders_Total_FreightDiscount', align: 'center'},
				{ width:55, name: 'EndPrice', index: 'EndPrice', align: 'center', sortable:false},
				{ width:55, name: 'brokerage', index: 'brokerage', align: 'center'},
				{ width:100, name: 'OrdersInfo.Orders_Addtime', index: 'OrdersInfo.Orders_Addtime', align: 'center'},
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
        
        <div style="margin-top:5px;">
        <% if (status <=1)
           { %>
        <input type="button" id="Button3" class="bt_orange" value="结算" onclick="location.href='/orders/orders_do.aspx?action=orderendprice&orders_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <% }
            if (status == 1)
           { %>
        <input type="button" id="Button1" class="bt_orange" value="取消申请" onclick="location.href='/orders/orders_do.aspx?action=cancelendprice&orders_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%} %>
        
        
        
        </div>
      </td>
    </tr>
  </table>
       </td>
    </tr>
  </table>
</div>
</body>
</html>