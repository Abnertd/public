<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    //string keyword, defaultkey, ReqURL;
    DateTime startDate;
    DateTime endDate;
    string SubAccount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();

        SubAccount = tools.CheckStr(Request["SubAccount"]).Trim();

        //DateTime startDate;
        //DateTime endDate;
        if (DateTime.TryParse(tools.CheckStr(Request["date_start"]), out startDate) && DateTime.TryParse(tools.CheckStr(Request["date_end"]), out endDate))
        {
        }
        else
        {
            startDate = DateTime.Today.AddDays(-7);
            endDate = DateTime.Today;
        }
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
    <style>
        /*.Page{ height:30px; overflow:hidden; line-height:30px; text-align:center; margin-bottom:30px;}
.Page span{ font-size:12px; color:#666; margin:0 3px;}
.Page strong{ font-size:12px; color:#cd151a;}
.Page a{ display:inline-block; height:28px; line-height:28px; padding:0 10px; border:1px solid #e4e4e4; margin:0 3px; font-size:12px; color:#666;}
.Page a.on{ background:#d2150d; color:#fff; font-weight:600;}*/


        .page {
            font-size: 12px;
            font-weight: normal;
            color: #666;
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
        }

            .page a {
                border: 1px solid #e6e6e6;
                line-height: 25px;
                height: 25px;
                overflow: hidden;
                padding: 0 10px;
                display: inline-block;
                color: #666;
                font-family: Arial, Helvetica, sans-serif;
                vertical-align: middle;
                margin-left: -1px;
            }

                .page a:hover {
                    background-color: #ca0007;
                    text-decoration: none;
                    height: 25px;
                    overflow: hidden;
                    color: #fff;
                    border: 1px solid #ca0007;
                }

                .page a.on {
                    border: 1px solid #ca0007;
                    background-color: #ca0007;
                    line-height: 25px;
                    height: 25px;
                    overflow: hidden;
                    padding: 0 10px;
                    display: inline-block;
                    font-family: Arial, Helvetica, sans-serif;
                    color: #fff;
                }

            .page span {
                border: 1px solid #e6e6e6;
                background-color: #f7f7f7;
                line-height: 25px;
                height: 25px;
                overflow: hidden;
                padding: 0 10px;
                display: inline-block;
                color: #666;
                font-family: Arial, Helvetica, sans-serif;
                vertical-align: middle;
            }
    </style>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">交易流水查询</td>
            </tr>
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td>
                    <form action="?" method="post" name="frm_sch" id="frm_sch">
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr bgcolor="#F5F9FC">
                                <td align="left" style="color: Red"></td>
                                <td align="left">
                                    <%--<span class="left_nav">搜索</span> --%>
					
					 中信帐号：<input type="text" name="SubAccount" size="50" id="SubAccount" value="<% =SubAccount %>">
                                    起始日期：
				     <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10" readonly="readonly" value="<%=startDate.ToString("yyyy-MM-dd") %>" />
                                    -
                                    <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10" readonly="readonly" value="<%=endDate.ToString("yyyy-MM-dd")%>" />
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $("#date_start").datepicker({ numberOfMonths: 1 });
                                            $("#date_end").datepicker({ numberOfMonths: 1 });
                                        });
                                    </script>
                                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <% new ZhongXin().Account_List(SubAccount, startDate, endDate); %>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
