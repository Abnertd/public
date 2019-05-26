<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%  Public.CheckLogin("db71e6f9-f858-4469-b45e-b6ab55412853");
    string keyword, defaultkey, ReqURL;
    int Fee_Status, Fee_IsActive, Fee_IsChecked;
    ITools tools;
    tools = ToolsFactory.CreateTools();
    defaultkey = "";
    keyword = Request["keyword"];
    if (keyword != "输入优惠标题搜索" && keyword != null && keyword!="")
    {
        keyword = keyword;
    }
    else
    {
        keyword = "输入优惠标题搜索";
    }
    if (keyword == "输入优惠标题搜索")
    {
        defaultkey = "";
    }
    else
    {
        defaultkey = keyword;
    }
    Fee_Status = tools.CheckInt(Request["Fee_Status"]);
    Fee_IsChecked = tools.CheckInt(Request["Fee_IsChecked"]);
    Fee_IsActive = tools.CheckInt(Request["Fee_IsActive"]);
    ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=" + Fee_Status + "&Fee_IsChecked=" + Fee_IsChecked + "&Fee_IsActive=" + Fee_IsActive;
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">运费优惠</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_IsChecked==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey),"全部优惠")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_IsChecked==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=0&Fee_IsChecked=1&Fee_IsActive=0", "待审核优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_Status==2&&Fee_IsActive==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=2&Fee_IsChecked=2&Fee_IsActive=2", "未开始优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_Status==1&&Fee_IsActive==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=1&Fee_IsChecked=2&Fee_IsActive=2", "应用中优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_IsActive==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=1&Fee_IsChecked=2&Fee_IsActive=1", "暂停中优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_Status==3&&Fee_IsChecked==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=3&Fee_IsChecked=2&Fee_IsActive=0", "已结束优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_IsChecked==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Fee_Status=0&Fee_IsChecked=3&Fee_IsActive=0", "无效优惠")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td>
      <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="?<%=ReqURL.Replace("keyword=" + Server.UrlEncode(defaultkey) + "&","") %>" method="post" name="frm_sch" id="frm_sch" >
				  <tr>
				  
					<td align="left">
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入优惠标题搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
		</td></tr>
		<tr><td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Promotion_Favor_Fee_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '优惠标题', "优惠条件","有效期","状态","优先级", "操作"],
            colModel: [
				{width:50, name: 'PromotionFavorFeeInfo.Promotion_Fee_ID', index: 'PromotionFavorFeeInfo.Promotion_Fee_ID', align: 'center'},
				{align:'center', name: 'PromotionFavorFeeInfo.Promotion_Fee_Title', index: 'PromotionFavorFeeInfo.Promotion_Fee_Title'},
				{width:80, align:'center',name: 'PromotionFavorFeeInfo.Promotion_Fee_Payline', index: 'PromotionFavorFeeInfo.Promotion_Fee_Payline'},
				{width:150, align:'center',name: 'PromotionFavorFeeInfo.Favor_Fee_valid', index: 'PromotionFavorFeeInfo.Favor_Fee_valid', sortable:false},
				{width:50, align:'center',name: 'PromotionFavorFeeInfo.Favor_Fee_status', index: 'PromotionFavorFeeInfo.Favor_Fee_status', sortable:false},
				{width:50, align:'center',name: 'PromotionFavorFeeInfo.Promotion_Fee_Sort', index: 'PromotionFavorFeeInfo.Promotion_Fee_Sort'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'PromotionFavorFeeInfo.Promotion_Fee_ID',
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
        <form action="/promotion/Promotion_Favor_Fee_do.aspx" method="post">
        <div style="margin-top:5px;">
        
        
        <% if (Public.CheckPrivilege("e067b0bd-80be-461d-96d0-d3887e5c2c3d"))
           {
               if (Fee_IsActive == 2)
               { %>
        <input type="button" id="Button5" class="bt_orange" value="暂停" onclick="location.href='Promotion_Favor_Fee_do.aspx?action=cancelactive&favor_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%}
               if (Fee_IsActive == 1)
               { %>
        <input type="button" id="Button2" class="bt_orange" value="恢复" onclick="location.href='Promotion_Favor_Fee_do.aspx?action=active&favor_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%}
           } %>
        
        <% if (Public.CheckPrivilege("8358602a-fa75-4854-9635-d9f5f725123b") && Fee_IsChecked == 1)
           { %>
        <input type="button" id="Button3" class="bt_orange" value="审核" onclick="location.href='Promotion_Favor_Fee_do.aspx?action=audit&favor_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button4" class="bt_orange" value="审核不通过" onclick="location.href='Promotion_Favor_Fee_do.aspx?action=cancelaudit&favor_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%} %>
        
        </div>
        </form>
        </td></tr></table>
      </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
</div>
</body>
</html>
