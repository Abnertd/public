<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    int  reply, overdue;

    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("249d2ad4-45f4-4945-8e78-d18c79053106");
        keyword = Request["keyword"];
        reply = tools.CheckInt(Request["reply"]);
        overdue = tools.CheckInt(Request["overdue"]);
        if (keyword != "输入询价标题、产品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入询价标题、产品名称进行搜索";
        }
        if (keyword == "输入询价标题、产品名称进行搜索")
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
      <td class="content_title">询价信息管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="supplier_priceask_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入询价标题、产品名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
            url: 'supplier_priceask_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>&reply=<%=reply %>&overdue=<%=overdue %>',
			datatype: "json",
			colNames: ['ID', '产品名称', '询价标题', '询价人', '回复状态', '询价时间', "操作"],
            colModel: [

				{ width: 50, name: 'SupplierPriceAskInfo.PriceAsk_ID', index: 'SupplierPriceAskInfo.PriceAsk_ID', align: 'center' },
                { width: 100, name: 'SupplierPriceAskInfo.PriceAsk_ProductID', index: 'SupplierPriceAskInfo.PriceAsk_ProductID' },
                { name: 'SupplierPriceAskInfo.PriceAsk_Title', index: 'SupplierPriceAskInfo.PriceAsk_Title' },
				{ width: 90, name: 'SupplierPriceAskInfo.PriceAsk_MemberID', index: 'SupplierPriceAskInfo.PriceAsk_MemberID', align: 'center' },
				{ width: 50, name: 'SupplierPriceAskInfo.PriceAsk_IsReply', index: 'SupplierPriceAskInfo.PriceAsk_IsReply', align: 'center' },
				{ width: 80, name: 'SupplierPriceAskInfo.PriceAsk_AddTime', index: 'SupplierPriceAskInfo.PriceAsk_AddTime', align: 'center' },
				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierPriceAskInfo.PriceAsk_ID',
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