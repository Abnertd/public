﻿<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string title = "";
    string listtype = "";
    private ITools tools;
    int isreply;
    string date_start, date_end;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9877a09e-5dda-4b1e-bf6f-042504449eeb");
        listtype = Request.QueryString["listtype"];
        tools = ToolsFactory.CreateTools();
        switch (listtype)
        {



            //case "message":
            //    title = "留言";
            //    break;
            //case "idea":
            //    //title = "意见";
            //    title = "金融服务";
            //    break;
            //case "suggest":
            //    title = "建议意向";
            //    break;
            //case "complain":
            //    title = "投诉";
            //    break;
            //default:
            //    listtype = "message";
            //    title = "留言";
            //    break;
            default:
               listtype = "signup";
               title = "立即发布留言";
               break;
        }

        defaultkey = "";
        keyword = Request["keyword"];
        isreply = tools.CheckInt(Request["isreply"]);
        if (keyword != "输入姓名、电话进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入姓名、电话进行搜索";
        }
        if (keyword == "输入姓名、电话进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&isreply=" + isreply;
        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
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
                <td class="content_title"><%=title %>管理</td>
            </tr>
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td>
                    <form action="signup_list.aspx?listtype=<% =listtype%>" method="post" name="frm_sch"
                        id="frm_sch">
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr bgcolor="#F5F9FC">
                                <td align="right">时间：
                    <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10"
                        readonly="readonly" value="<%=date_start %>" />
                                    -
                    <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                        readonly="readonly" value="<%=date_end%>" />

                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $("#date_start").datepicker({ numberOfMonths: 1 });
                                            $("#date_end").datepicker({ numberOfMonths: 1 });
                                        });
                                    </script>

                                <%--    <span class="left_nav">状态</span>--%>
                                   <%-- <select name="isreply">
                                        <option value="0" <%=Public.CheckedSelected("0",isreply.ToString()) %>>全部</option>
                                        <option value="1" <%=Public.CheckedSelected("1",isreply.ToString()) %>>已回复</option>
                                        <option value="2" <%=Public.CheckedSelected("2",isreply.ToString()) %>>未回复</option>
                                    </select>--%>
                                    <span class="left_nav">搜索</span>
                                    <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入姓名、电话进行搜索'){this.value='';}"
                                        value="<% =keyword %>">
                                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                                </td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
            <tr>

                <%if (listtype == "signup")
                  {
              
                %>
                <td class="content_content">
                    <table id="list"></table>
                    <div id="pager"></div>
                    <script type="text/javascript">
                        jQuery("#list").jqGrid({
                            url: 'supplier_do.aspx?action=signup&listtype=<% =listtype%>&date_start=<% =Request["date_start"]%>&date_end=<% =Request["date_end"]%>&<%=ReqURL %>',
            datatype: "json",

            colNames: ['ID', '留言姓名', '手机号','立即报价时间', "操作"],
            colModel: [
				{ width: 150, name: 'id', index: 'id', align: 'center' },
			
				{ width: 300, align: 'center', name: 'BidUpRequireQuickInfo.Bid_Up_ContractMan', index: 'BidUpRequireQuickInfo.Bid_Up_ContractMan' },
				{ align: 'center', name: 'BidUpRequireQuickInfo.Bid_Up_ContractMobile', index: 'BidUpRequireQuickInfo.Bid_Up_ContractMobile' },
                { align: 'center', name: 'BidUpRequireQuickInfo.Bid_Up_AddTime', index: 'BidUpRequireQuickInfo.Bid_Up_AddTime' },
				//{ name: 'BidUpRequireQuickInfo.Bid_Up_ContractContent', index: 'BidUpRequireQuickInfo.Bid_Up_ContractContent' },
			
				{ width: 100, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'Bid_Up_ID',
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
                   <%-- <form action="/supplier/supplier_do.aspx" method="post">
                        <div style="margin-top: 5px;">
                            <div style="margin-top: 5px;">

                                <% if (Public.CheckPrivilege("4190769c-4fd5-4013-a701-fe7594c1017f"))
                                   { %>
                                <input type="button" id="export" class="bt_orange" value="导出勾选<% =title%>" onclick="location.href = 'supplier_do.aspx?action=feedbackexport&feedback_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                                <%} %>
                            </div>
                    </form>--%>
                </td>

                <%}%>
                
              
            
            </tr>
        </table>
    </div>
</body>
</html>
