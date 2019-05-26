<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    int CateID;
    private HelpCate myAppC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1b86dfa7-32e5-4136-b3d1-a8a670f415ff");
        
        tools = ToolsFactory.CreateTools();
        myAppC = new HelpCate();

        defaultkey = "";
        keyword = Request["keyword"];
        CateID = tools.CheckInt(Request["CateID"]);
        if (keyword != "输入关键词搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入关键词搜索";
        }
        if (keyword == "输入关键词搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&CateID=" + CateID;
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>竞价关键词管理</title>
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
      <td class="content_title">竞价关键词管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Keyword_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入关键词搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				 
				</table>
				 </form>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Keyword_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '关键词', "起步价",'竞价数量', "操作"],
            colModel: [
				{width:40,  name: 'KeywordBiddingKeywordInfo.Keyword_ID', index: 'KeywordBiddingKeywordInfo.Keyword_ID', align: 'center'},
				{ name: 'KeywordBiddingKeywordInfo.Keyword_Name', index: 'KeywordBiddingKeywordInfo.Keyword_Name'},
				{width:80, name: 'KeywordBiddingKeywordInfo.Keyword_MinPrice', index: 'KeywordBiddingKeywordInfo.Keyword_MinPrice', align: 'center'},
				{width:80, name: 'Amount', index: 'Amount', align: 'center', sortable:false},
				{width:80,  name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'KeywordBiddingKeywordInfo.Keyword_ID',
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

                    <%if (Public.CheckPrivilege("daebed75-ab60-46af-bd24-8d7da34f360a"))
                  { %>
                     <input type="button" id="Button2" class="bt_orange" value="删除" onclick="location.href='Keyword_do.aspx?action=remove&Keyword_ID='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                    <%} %>
                </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>