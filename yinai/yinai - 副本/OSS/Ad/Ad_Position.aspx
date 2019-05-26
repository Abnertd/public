<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% Public.CheckLogin("d3aa1596-cc86-46c7-80f0-8bf6248ee31e");
   AD Ad = new AD();
    ITools tools;
    tools=ToolsFactory.CreateTools();
   string Ad_Channel;
   string keyword, defaultkey, ReqURL;
   defaultkey = "";
   keyword = Request["keyword"];
   Ad_Channel = Request["Ad_Channel"];

   if (keyword != "输入位置名称、位置代号搜索" && keyword != null)
   {
       keyword = keyword;
   }
   else
   {
       keyword = "输入位置名称、位置代号搜索";
   }
   if (keyword == "输入位置名称、位置代号搜索")
   {
       defaultkey = "";
   }
   else
   {
       defaultkey = keyword;
   }

   string Marketing = tools.CheckStr(Request["Marketing"]);

   ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Ad_Channel=" + Ad_Channel + "&Marketing=" + Marketing;
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
      <td class="content_title">管理广告位置</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="ad_position.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					
					<span class="left_nav">推广状态</span>
					<select name="Marketing">
					    <option value="">全部</option>
					    <option value="1" <%=Public.CheckedSelected(Marketing, "1")%>>推广中</option>
					    <option value="0" <%=Public.CheckedSelected(Marketing, "0")%>>未推广</option>
					</select>
					
					<span class="left_nav">频道</span> 
					 <%Ad.Select_Position_Channel("Ad_Channel", tools.CheckInt(Ad_Channel));%>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入位置名称、位置代号搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Ad_Position_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ["ID", "位置名称","位置代号","所属频道","宽度","高度","操作"],
            colModel: [
				{width:40, name: 'ADPositionInfo.Ad_Position_ID', index: 'ADPositionInfo.Ad_Position_ID', align: 'center'},
				{ name: 'ADPositionInfo.Ad_Position_Name', index: 'ADPositionInfo.Ad_Position_Name'},
				{ name: 'ADPositionInfo.Ad_Position_Value', index: 'ADPositionInfo.Ad_Position_Value'},
				{width:80,align:'center', name: 'ADPositionInfo.Ad_Position_ChannelID', index: 'ADPositionInfo.Ad_Position_ChannelID'},
				{width:50,align:'center', name: 'ADPositionInfo.Ad_Position_Width', index: 'ADPositionInfo.Ad_Position_Width', sortable:false},
				{width:50,align:'center', name: 'ADPositionInfo.Ad_Position_Height', index: 'ADPositionInfo.Ad_Position_Height', sortable:false},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ADPositionInfo.Ad_Position_ID',
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
