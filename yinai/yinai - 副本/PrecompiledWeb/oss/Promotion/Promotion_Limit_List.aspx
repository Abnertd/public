<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%  Public.CheckLogin("22d21441-155a-4dc5-aec6-dcf5bdedd5cf");
    string keyword, defaultkey, ReqURL;
    int Status;
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Status = tools.CheckInt(Request["status"]);
    int group_id;
    group_id = tools.CheckInt(Request["group_id"]);
    defaultkey = "";
    keyword = Request["keyword"];
    if (keyword != "输入商品编码、商品名称、分组名称进行搜索" && keyword != null && keyword != "")
    {
        keyword = keyword;
    }
    else
    {
        keyword = "输入商品编码、商品名称、分组名称进行搜索";
    }
    if (keyword == "输入商品编码、商品名称、分组名称进行搜索")
    {
        defaultkey = "";
    }
    else
    {
        defaultkey = keyword;
    }
    ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Status=" + Status + "&group_id=" + group_id;
    
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
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">限时促销</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Status==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey),"全部优惠")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Status==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Status=1", "未开始优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Status==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Status=2", "应用中优惠")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Status==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Status=3", "已过期优惠")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td>
      <form action="promotion_limit_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
	  <tr >
		<td><span class="left_nav">搜索</span> 
		 <input type="hidden" name="status" value="<%=Status %>" />
		 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、分组名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
	  </tr>
	</table>
	</form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
         var lastsel = 0;
        jQuery("#list").jqGrid({
        url: 'Promotion_limit_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '所属供应商','商品编码', "商品名称", "规格","生产企业","本站价格","排序","限时价格","开始时间","结束时间"],
            colModel: [
				{width:50, name: 'PromotionLimitInfo.Promotion_Limit_ID', index: 'PromotionLimitInfo.Promotion_Limit_ID', align: 'center'},
				{width:80,align:'center', name: 'group_name', index: 'group_name', sortable:false},
				{width:80,align:'center', name: 'product_code', index: 'product_code', sortable:false},
				{align:'center', name: 'product_name', index: 'product_name', sortable:false},
				{width:100,align:'center', name: 'product_spec', index: 'product_spec', sortable:false},
				{width:150,align:'center', name: 'product_maker', index: 'product_maker', sortable:false},
				{width:80,align:'center', name: 'product_price', index: 'product_price', sortable:false},
	            {width:50,align:'center', name: 'PromotionLimitInfo.Promotion_Limit_Sort', index: 'PromotionLimitInfo.Promotion_Limit_Sort', editable: true},
				{width:80,align:'center', name: 'PromotionLimitInfo.Promotion_Limit_Price', index: 'PromotionLimitInfo.Promotion_Limit_Price'},
				{width:150,align:'center', name: 'PromotionLimitInfo.Promotion_Limit_Starttime', index: 'PromotionLimitInfo.Promotion_Limit_Starttime'},
				{width:150,align:'center', name: 'PromotionLimitInfo.Promotion_Limit_Endtime', index: 'PromotionLimitInfo.Promotion_Limit_Endtime'},
				
			],
            sortname: 'PromotionLimitInfo.Promotion_Limit_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager', 
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%",
			editurl: "Promotion_Limit_do.aspx?action=listsortedit",
						onSelectRow: function(id) {
			    if (id && id !== lastsel) {
			        jQuery('#list').jqGrid('saveRow', lastsel);
			        jQuery('#list').jqGrid('editRow', id, true);
			        lastsel = id;
			    }
		}
        });
        </script>
      </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
</div>
</body>
</html>
