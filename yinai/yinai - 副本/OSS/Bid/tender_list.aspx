<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    private Bid MyBid;
    int BID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("db8de73b-9ac0-476e-866e-892dd35589c5");
        tools = ToolsFactory.CreateTools();
        MyBid = new Bid();
        defaultkey = "";
        keyword = Request["keyword"];
        BID = tools.CheckInt(Request["BID"]);
        if (keyword != "输入报价单位搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入报价单位搜索";
        }
        if (keyword == "输入报价单位搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "BID=" + BID + "&keyword=" + Server.UrlEncode(defaultkey);
    }
</script>
 <script type="text/javascript">

     //逻辑删除
     function confirmlogdelete(gotourl) {
         if ($("#dialog-confirmdelete").length == 0) {
             $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要删除吗?\"><p><span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>您确定要删除该条信息吗？</p>");
         }
         $("#dialog-confirmdelete").dialog({
             modal: true,
             width: 400,
             buttons: {
                 "确认": function () {
                     $(this).dialog("close");
                     location = gotourl;
                 },
                 "取消": function () {
                     $(this).dialog("close");
                 }
             }
         });
         $("#dialog-confirmdelete").dialog({
             close: function () {
                 $(this).dialog("destroy");
             }
         });
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
      <td class="content_title">报价列表</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="tender_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">

					<span class="left_nav">搜索</span> 
                        <input type="hidden" name="BID" id="BID" value="<%=BID %>" />
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入报价单位搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                url: 'bid_do.aspx?action=Tenderlist&<%=ReqURL %>',
                datatype: "json",
                colNames: ['ID', '招标公告','报价单位', "报价总金额", "报价时间", "是否中标", "操作"],
                colModel: [
                    { width: 40, name: 'TenderInfo.Tender_ID', index: 'TenderInfo.Tender_ID', align: 'center' },
                    { name: 'TenderInfo.Tender_BidID', index: 'TenderInfo.Tender_BidID', align: 'center' },
                    { name: 'TenderInfo.Tender_SupplierID', index: 'TenderInfo.Tender_SupplierID', align: 'center' },
                    { width: 80, name: 'TenderInfo.Tender_AllPrice', index: 'TenderInfo.Tender_AllPrice', align: 'center' },
                    { width: 110, name: 'TenderInfo.Tender_Addtime', index: 'TenderInfo.Tender_Addtime', align: 'center' },
                    { width: 110, name: 'TenderInfo.Tender_IsWin', index: 'TenderInfo.Tender_IsWin', align: 'center' },

                    { width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                ],
                sortname: 'TenderInfo.Tender_ID',
                sortorder: "desc",
                rowNum: GetrowNum(),
                rowList: GetrowList(),
                pager: 'pager',
                multiselect: false,
                viewsortcols: [false, 'horizontal', true],
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