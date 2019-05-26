<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% Public.CheckLogin("fe2e0dd7-2773-4748-915a-103065ed0334");
   int isreply, ischeck;
   string keyword, defaultkey, ReqURL,page_title,target;
   ITools tools;
   tools = ToolsFactory.CreateTools();
   defaultkey = "";
   keyword = Request["keyword"];
   target = Request["target"];
   isreply = tools.CheckInt(Request["isreply"]);
   ischeck = tools.CheckInt(Request["ischeck"]);
   if (keyword != "输入咨询内容、咨询人、产品名称进行搜索" && keyword != null)
   {
       keyword = keyword;
   }
   else
   {
       keyword = "输入咨询内容、咨询人、产品名称进行搜索";
   }
   if (keyword == "输入咨询内容、咨询人、产品名称进行搜索")
   {
       defaultkey = "";
   }
   else
   {
       defaultkey = keyword;
   }

   ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&isreply=" + isreply + "&ischeck=" + ischeck + "&target=" + target;
   if (target == "supplier")
   {
       page_title = "店铺咨询管理";
   }
   else
   {
       page_title = "产品咨询管理";
   }
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=page_title %></title>
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
      <td class="content_title"><%=page_title %></td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="shopping_ask_list.aspx?target=<%=target %>" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right">
					<span class="left_nav">审核状态</span> 
					 <select name="ischeck">
					 <option value="0" <%=Public.CheckedSelected("0",ischeck.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",ischeck.ToString()) %>>已审核</option>
					 <option value="2" <%=Public.CheckedSelected("2",ischeck.ToString()) %>>未审核</option>
					 </select>
					<span class="left_nav">回复状态</span> 
					 <select name="isreply">
					 <option value="0" <%=Public.CheckedSelected("0",isreply.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",isreply.ToString()) %>>已回复</option>
					 <option value="2" <%=Public.CheckedSelected("2",isreply.ToString()) %>>未回复</option>
					 </select>
					 <span class="left_nav">搜索</span> 
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入咨询内容、咨询人、产品名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'shopping_ask_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '咨询内容', "咨询人", "咨询产品","审核状态","回复状态","咨询时间","操作"],
            colModel: [
				{width:40, name: 'ShoppingAskInfo.Ask_ID', index: 'ShoppingAskInfo.Ask_ID', align: 'center'},
				{ name: 'ShoppingAskInfo.Ask_Content', index: 'ShoppingAskInfo.Ask_Content'},
				{width:100,align:'center', name: 'ShoppingAskInfo.Ask_MemberID', index: 'ShoppingAskInfo.Ask_MemberID'},
				{width:150, name: 'ShoppingAskInfo.Ask_ProductID', index: 'ShoppingAskInfo.Ask_ProductID'},
				{width:40, name: 'ShoppingAskInfo.Ask_IsCheck', index: 'ShoppingAskInfo.Ask_IsCheck',align: 'center'},
				{width:40, name: 'ShoppingAskInfo.Ask_Isreply', index: 'ShoppingAskInfo.Ask_Isreply',align: 'center'},
				{width:150, name: 'ShoppingAskInfo.Ask_Addtime', index: 'ShoppingAskInfo.Ask_Addtime',align: 'center'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ShoppingAskInfo.Ask_ID',
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
         <input type="button" id="Button3" class="bt_orange" value="审核通过" onclick="location.href='Shopping_ask_do.aspx?action=audit&target=<%=target %>&ask_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
         </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>
