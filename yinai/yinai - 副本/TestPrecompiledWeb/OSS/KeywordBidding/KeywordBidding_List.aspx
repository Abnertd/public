<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%   
    Public.CheckLogin("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71");
    ITools tools = ToolsFactory.CreateTools();
    AD Ad = new AD();
    string keyword, defaultkey, ReqURL;
    defaultkey = "";
    keyword = Request["keyword"];
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

    string listtype = tools.CheckStr(Request.QueryString["listtype"]);
    int Audit = tools.CheckInt(Request["Audit"]);
    ReqURL = "keywordbidding_do.aspx?action=list&listtype=" + listtype + "&keyword=" + Server.UrlEncode(defaultkey) + "&Audit=" + Audit;
    
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
            <td class="content_title">管理关键词竞价</td>
        </tr>
        <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey), "待审核")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=1", "审核通过")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=2", "审核不通过")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td height="5"></td></tr>
    <tr><td>
                <form action="KeywordBidding_List.aspx" method="post" name="frm_sch" id="frm_sch">
                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                    <tr >
                        <td >
                            <span class="left_nav">搜索</span>
                            <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入关键词搜索'){this.value='';}" value="<% =keyword %>" />
                                <input type="hidden" name="audit" value="<%=Audit %>" />
                            <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                        </td>
                    </tr>
                </table>
                </form>
            </td>
        </tr>
        <tr>
            <td>
                <table id="list"></table>
                <div id="pager"></div>
                
                <div style="margin-top:5px;">
                <%if (Audit == 0 && Public.CheckPrivilege("1cbc86b5-d248-4553-88c9-4629438c2464"))
                  { %>
                     <input type="button" id="Button3" class="bt_orange" value="审核通过" onclick="location.href='KeywordBidding_do.aspx?action=audit&KeywordBidding_ID='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                     <input type="button" id="Button1" class="bt_orange" value="审核不通过" onclick="location.href='KeywordBidding_do.aspx?action=unaudit&KeywordBidding_ID='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                    <%} %>
                    <%if (Audit == 2 && Public.CheckPrivilege("cc52c0d7-188d-4915-955a-7e0857e958bc"))
                  { %>
                     <input type="button" id="Button2" class="bt_orange" value="删除" onclick="location.href='KeywordBidding_do.aspx?action=remove&KeywordBidding_ID='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                    <%} %>
                </div>
                

            </td>
        </tr>
    </table>
    </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    jQuery("#list").jqGrid({
    url: '<%=ReqURL %>',
        datatype: "json",
        colNames: ['ID',"商品名称","供应商", "关键词","出价","展示次数","点击次数","起止时间","审核状态"],
        colModel: [
            {width:70, name: 'KeywordBiddingInfo.KeywordBidding_ID', index: 'KeywordBiddingInfo.KeywordBidding_ID', align: 'center'},
            { name: 'KeywordBiddingInfo.KeywordBidding_ProductID', index: 'KeywordBiddingInfo.KeywordBidding_ProductID', align: 'center', sortable:false},
            { name: 'KeywordBiddingInfo.KeywordBidding_KeywordID', index: 'KeywordBiddingInfo.KeywordBidding_KeywordID', align: 'center'},
            { name: 'KeywordBiddingInfo.KeywordBidding_SupplierID', index: 'KeywordBiddingInfo.KeywordBidding_SupplierID', align: 'center'},
            {width:80, align:'KeywordBiddingInfo.KeywordBidding_Price', name: 'KeywordBiddingInfo.KeywordBidding_Price', index: 'KeywordBidding_Price'},
            {width:60, align:'center', name: 'KeywordBiddingInfo.KeywordBidding_ShowTimes', index: 'KeywordBiddingInfo.KeywordBidding_ShowTimes'},
            {width:60, name: 'KeywordBiddingInfo.KeywordBidding_Hits', index: 'KeywordBiddingInfo.KeywordBidding_Hits', align: 'center'},
            {width:160, name: 'KeywordBiddingInfo.KeywordBidding_EndDate', index: 'KeywordBiddingInfo.KeywordBidding_EndDate', align: 'center'},
            {width:60, name: 'KeywordBiddingInfo.KeywordBidding_Audit', index: 'KeywordBiddingInfo.KeywordBidding_Audit', align: 'center'}
        ],
        sortname: 'KeywordBiddingInfo.KeywordBidding_ID',
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

</body>
</html>
