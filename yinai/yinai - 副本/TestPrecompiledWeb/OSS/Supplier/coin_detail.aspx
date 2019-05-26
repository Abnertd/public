<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    ITools tools;
    string keyword, date_start, date_end;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
        tools = ToolsFactory.CreateTools();

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
<script type="text/javascript">
  $(document).ready(function() {
      $("#date_start").datepicker({ numberOfMonths: 1 });
      $("#date_end").datepicker({ numberOfMonths: 1 });
  });
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">供应商积分明细</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="coin_detail.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 起始日期：
					<input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10" readonly="readonly" value="<%=date_start %>" /> - <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10" readonly="readonly" value="<%=date_end%>" />

					 公司名称：<input type="text" name="keyword" id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'coin_do.aspx?action=list&keyword=<% =Server.UrlEncode(Request["keyword"])%>&date_start=<% =Request["date_start"]%>&date_end=<% =Request["date_end"]%>',
			datatype: "json",
            colNames: ['ID', '公司名称', '收入', '支出', '余额', '备注', "时间"],
            colModel: [
				{width:50, name: 'SupplierConsumptionInfo.Consump_ID', index: 'SupplierConsumptionInfo.Consump_ID', align: 'center'},
				{width:80,align:'center', name: 'username', index: 'username', sortable:false},
				{width:50, align:'center',name: 'coinadd', index: 'coinadd', sortable:false},
				{width:50, align:'center',name: 'coinreduct', index: 'coinreduct', sortable:false},
				{width:50, name: 'SupplierConsumptionInfo.Consump_CoinRemain', index: 'SupplierConsumptionInfo.Consump_CoinRemain', align: 'center'},
				{ name: 'SupplierConsumptionInfo.Consump_Reason', index: 'SupplierConsumptionInfo.Consump_Reason'},
				{width:100, name: 'SupplierConsumptionInfo.Consump_Addtime', index: 'SupplierConsumptionInfo.Consump_Addtime', align: 'center'},
			],
            sortname: 'SupplierConsumptionInfo.Consump_ID',
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