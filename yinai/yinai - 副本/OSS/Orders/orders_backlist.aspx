<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string keyword;
    int back_status;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("aaa944b1-6068-42cd-82b5-d7f4841ecf45");
        tools = ToolsFactory.CreateTools();
        keyword = Request["keyword"];
        back_status = tools.CheckInt(Request["back_status"]);
        
        
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
      <td class="content_title">退换货申请管理</td>
    </tr>
        <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="orders_backlist.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					状态：<select name="back_status">
					<option value="0" <% =Public.CheckedSelected(back_status.ToString(), "0")%>>全部</option>
					<option value="1" <% =Public.CheckedSelected(back_status.ToString(), "1")%>>未处理</option>
					<option value="2" <% =Public.CheckedSelected(back_status.ToString(), "2")%>>处理中</option>
					<option value="3" <% =Public.CheckedSelected(back_status.ToString(), "3")%>>已处理</option>
					</select>
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
        url: 'orders_back_do.aspx?action=list&keyword=<%=Server.UrlEncode(Request["keyword"]) %>&back_status=<%=back_status %>',
			datatype: "json",
            colNames: ['ID', '订单号', '退款金额', '操作类型','Email','申请人', '申请时间', '状态', "操作"],
            colModel: [
				{ width:50,name: 'OrdersBackApplyInfo.Orders_BackApply_ID', index: 'OrdersBackApplyInfo.Orders_BackApply_ID', align: 'center'},
				{ name: 'back_ordercode', index: 'back_ordercode', align: 'center', sortable:false},
				{ width:50,name: 'OrdersBackApplyInfo.Orders_BackApply_Amount', index: 'OrdersBackApplyInfo.Orders_BackApply_Amount', align: 'center'},
				{ width:50,name: 'back_type', index: 'back_type', align: 'center', sortable:false},
				{ width:100,name: 'back_email', index: 'back_email', align: 'center', sortable:false},
				{ width:80,name: 'back_name', index: 'back_name', align: 'center', sortable:false},
				{ width:150,name: 'back_addtime', index: 'back_addtime', align: 'center', sortable:false},
				{ width:50,name: 'OrdersBackApplyInfo.Orders_BackApply_Status', index: 'OrdersBackApplyInfo.Orders_BackApply_Status', align: 'center'},
				{ width:100,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'OrdersBackApplyInfo.Orders_BackApply_ID',
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