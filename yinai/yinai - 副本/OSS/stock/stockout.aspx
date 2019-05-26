<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    int isprocess;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6f896c98-c62f-43f6-a276-39e43697c771");
        
        tools = ToolsFactory.CreateTools();


        defaultkey = "";
        keyword = Request["keyword"];
        isprocess = tools.CheckInt(Request["isprocess"]);
        if (keyword != "输入产品名称、用户名、联系电话、Email进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入产品名称、用户名、联系电话、Email进行搜索";
        }
        if (keyword == "输入产品名称、用户名、联系电话、Email进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&isprocess=" + isprocess;
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
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">缺货登记管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="stockout.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">状态</span> 
					 <select name="isprocess">
					 <option value="0" <%=Public.CheckedSelected("0",isprocess.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",isprocess.ToString()) %>>已处理</option>
					 <option value="2" <%=Public.CheckedSelected("2",isprocess.ToString()) %>>未处理</option>
					 </select>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入产品名称、用户名、联系电话、Email进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'stockout_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '产品名称', "用户名","联系电话","E-mail","状态", "操作"],
            colModel: [
				{width:50, name: 'StockoutBookingInfo.Stockout_ID', index: 'StockoutBookingInfo.Stockout_ID', align: 'center'},
				{ name: 'StockoutBookingInfo.Stockout_Product_Name', index: 'StockoutBookingInfo.Stockout_Product_Name'},
				{ width:100,name: 'StockoutBookingInfo.Stockout_Member_Name', index: 'StockoutBookingInfo.Stockout_Member_Name', align: 'center'},
				{ width:100,name: 'StockoutBookingInfo.Stockout_Member_Tel', index: 'StockoutBookingInfo.Stockout_Member_Tel', align: 'center'},
				{ width:100,name: 'StockoutBookingInfo.Stockout_Member_Email', index: 'StockoutBookingInfo.Stockout_Member_Email', align: 'center'},
				{ width:30,name: 'StockoutBookingInfo.Stockout_IsRead', index: 'StockoutBookingInfo.Stockout_IsRead', align: 'center'},
				{ width:80,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'StockoutBookingInfo.Stockout_ID',
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