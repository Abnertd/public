<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    int Purchase_ID, audit, reply, overdue;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools=ToolsFactory.CreateTools();
        Public.CheckLogin("6a12664e-4eeb-4259-b7b5-904044194067");
        keyword = Request["keyword"];
        Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);
        audit = tools.CheckInt(Request["audit"]);
        reply = tools.CheckInt(Request["reply"]);
        overdue = tools.CheckInt(Request["overdue"]);
        if (keyword != "输入采购标题进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入采购标题进行搜索";
        }
        if (keyword == "输入采购标题进行搜索")
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
      <td class="content_title">报价信息管理</td>
    </tr>
    <%--<tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(audit==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_ID=" + Purchase_ID + "&audit=0", "全部")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(audit==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_ID=" + Purchase_ID + "&audit=1", "待审核")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(audit==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_ID=" + Purchase_ID + "&audit=2", "已审核")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(audit==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_ID=" + Purchase_ID + "&audit=3", "审核不通过")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      </tr>
      </table>
      </td></tr>--%>
      <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="supplier_pricereport_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入采购标题进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>">
                     <input type="hidden" value="<%=Purchase_ID %>" name="Purchase_ID" />
                      <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
            url: 'supplier_pricereport_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>&Purchase_ID=<%=Purchase_ID %>&audit=<%=audit %>&reply=<%=reply %>&overdue=<%=overdue %>',
			datatype: "json",
			colNames: ['ID', '采购标题', '采购人', '报价人', '审核状态', '回复状态', '报价时间', "操作"],
            colModel: [

				{ width: 50, name: 'SupplierPriceReportInfo.PriceReport_ID', index: 'SupplierPriceReportInfo.PriceReport_ID', align: 'center' },
                {  name: 'SupplierPriceReportInfo.PriceReport_PurchaseID', index: 'SupplierPriceReportInfo.PriceReport_PurchaseID', sortable: false },
                { width: 90, name: 'SupplierPriceReportInfo.PriceReport_Title', index: 'SupplierPriceReportInfo.PriceReport_Title', sortable: false, align: 'center' },
				{ width: 90, name: 'SupplierPriceReportInfo.PriceReport_MemberID', index: 'SupplierPriceReportInfo.PriceReport_MemberID', align: 'center' },
				{ width: 50, name: 'SupplierPriceReportInfo.PriceReport_AuditStatus', index: 'SupplierPriceReportInfo.PriceReport_AuditStatus', align: 'center' },
                { width: 50, name: 'SupplierPriceReportInfo.PriceReport_IsReply', index: 'SupplierPriceReportInfo.PriceReport_IsReply', align: 'center' },
				{ width: 70, name: 'SupplierPriceReportInfo.PriceReport_AddTime', index: 'SupplierPriceReportInfo.PriceReport_AddTime', align: 'center' },
				{width:70, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierPriceReportInfo.PriceReport_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(),
			pager: 'pager',
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
        });
        </script>
        <div style="margin-top:5px;">
        <% if (audit == 1 && Public.CheckPrivilege("0c39529f-732e-463e-9344-dc6d9f64cef9"))
           { %>
        <input type="button" id="Button1" class="bt_orange" value="审核通过" onclick="location.href='Supplier_pricereport_do.aspx?action=audit&Purchase_ID=<%=Purchase_ID %>&pricereport_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        
        <input type="button" id="Button2" class="bt_orange" value="审核不通过" onclick="location.href='Supplier_pricereport_do.aspx?action=denyaudit&Purchase_ID=<%=Purchase_ID %>&pricereport_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%}%>
        </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>