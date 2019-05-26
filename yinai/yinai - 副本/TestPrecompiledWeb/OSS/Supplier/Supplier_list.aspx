<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    string keyword = "";
    string defaultkey = "";
    int Audit = 0;
    int Supplier_Trash = 0;
    int Supplier_Status = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
        keyword = tools.CheckStr(Request["keyword"]);
        Audit = tools.CheckInt(Request["Audit"]);
        Supplier_Trash = tools.CheckInt(Request["Trash"]);
        Supplier_Status = tools.CheckInt(Request["Status"]);
        if (Supplier_Status == 2)
        {
            Audit = 2;
        }
        if (keyword.Length == 0)
        {
            keyword = "输入用户名、Email、公司名称进行搜索";
        }
        if (keyword == "输入用户名、Email、公司名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        //Audit = 0;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商信息</title>
   
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
                <td class="content_title">供应商信息</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" height="26" class="opt_foot">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="<%if (Audit == 0 && Supplier_Trash == 0) { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey), "待审核")%>
                                        </td>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="<%if (Audit == 1) { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=1", "审核通过")%></td>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="<%if (Audit == 2 && Supplier_Status == 0 && Supplier_Trash == 0) { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=2", "审核不通过")%></td>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="<%if (Supplier_Status == 2) { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Status=2&Audit=2", "冻结供应商")%></td>
                                        <td class="opt_gap">&nbsp;</td>
                                        <td class="<%if (Supplier_Trash == 1) { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Trash=1&Audit=2", "回收站")%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <form action="Supplier_list.aspx?Audit=<%=Audit %>" method="post" name="frm_sch" id="frm_sch">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td align="left"><span class="left_nav">搜索</span>

                                                <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入用户名、Email、公司名称进行搜索'){this.value='';}" id="keyword" value="<% =keyword %>">
                                                <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
                                        </tr>
                                    </table>
                                </form>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="list"></table>
                                <div id="pager"></div>
                                <script type="text/javascript">
                                    jQuery("#list").jqGrid({
                                        url: 'Supplier_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>&Audit=<%=Audit %>&Status=<%=Supplier_Status %>&Trash=<%=Supplier_Trash %>',     
                datatype: "json",
                colNames: ['ID', "用户名", '公司名称', '联系人', '联系电话', '手机号码', '审核状态', '启用状态', '注册时间', '登录次数', "操作"],
                colModel: [

				{ width: 50, name: 'SupplierInfo.Supplier_ID', index: 'SupplierInfo.Supplier_ID', align: 'center' },
                { name: 'SupplierInfo.Supplier_Nickname', index: 'SupplierInfo.Supplier_Nickname', align: 'center' },
				{ name: 'SupplierInfo.Supplier_CompanyName', index: 'SupplierInfo.Supplier_CompanyName', align: 'center' },
				{ name: 'SupplierInfo.Supplier_Contactman', index: 'SupplierInfo.Supplier_Contactman', align: 'center' },
                { name: 'SupplierInfo.Supplier_Phone', index: 'SupplierInfo.Supplier_Phone', align: 'center' },
                { name: 'SupplierInfo.Supplier_Mobile', index: 'SupplierInfo.Supplier_Mobile', align: 'center' },
                { name: 'SupplierInfo.Supplier_AuditStatus', index: 'SupplierInfo.Supplier_AuditStatus', align: 'center' },
				{ name: 'SupplierInfo.Supplier_Status', index: 'SupplierInfo.Supplier_Status', align: 'center' },
				{ name: 'SupplierInfo.Supplier_Addtime', index: 'SupplierInfo.Supplier_Addtime', align: 'center' },
                { name: 'SupplierInfo.Supplier_LoginCount', index: 'SupplierInfo.Supplier_LoginCount', align: 'center' },
				{ name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                ],
                sortname: 'SupplierInfo.Supplier_ID',
                sortorder: "desc",
                rowNum: GetrowNum(),
                rowList: GetrowList(),
                pager: 'pager',
                multiselect: true,
                viewsortcols: [false, 'horizontal', true],
                width: getTotalWidth() - 35,
                height: "100%"
            });
            $("#list").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'scroll' });
                                </script>
                                <div style="margin-top: 5px;">
                                    <% if (Audit == 0 && Supplier_Trash == 0 && Public.CheckPrivilege("d8de7f81-7e9a-44ea-9463-dd1afda2b74e"))
                                       { %>
                                    <input type="button" id="Button3" class="bt_orange" value="审核通过" onclick="location.href = 'supplier_do.aspx?action=audit&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />

                                    <input type="button" id="Button4" class="bt_orange" value="审核不通过" onclick="location.href = 'supplier_do.aspx?action=unaudit&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                                    <%} %>

                                    <% if (Audit == 1 && Public.CheckPrivilege("508c538f-25c2-4e38-b7c3-14779900c6d7"))
                                       { %>
                                    <input type="button" id="Button1" class="bt_orange" value="启用订单邮件提醒" onclick="location.href = 'supplier_do.aspx?action=AllowOrderEmail&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />

                                    <input type="button" id="Button2" class="bt_orange" value="禁用订单邮件提醒" onclick="location.href = 'supplier_do.aspx?action=unAllowOrderEmail&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />
                                    <%} %>

                                    <% if (Audit == 2 && Supplier_Status == 0 && Supplier_Trash == 0 && Public.CheckPrivilege("d8de7f81-7e9a-44ea-9463-dd1afda2b74e"))
                                       { %>
                                    <input type="button" id="Button5" class="bt_orange" value="审核通过" onclick="location.href = 'supplier_do.aspx?action=audit&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />

                                    <%} %>
                                    <% if (Audit == 2 && Supplier_Status == 0 && Supplier_Trash == 0 && Public.CheckPrivilege("dec68736-cc0f-4d60-ad6a-1e6fd96f8d9b"))
                                       { %>
                                    <input type="button" id="Button6" class="bt_orange" value="删除" onclick="location.href = 'supplier_do.aspx?action=trash&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />

                                    <%} %>
                                    <% if (Supplier_Trash == 1 && Public.CheckPrivilege("d736b77e-334a-4ce1-9748-7f1a23330a6c"))
                                       { %>
                                    <input type="button" id="Button7" class="bt_orange" value="删除" onclick="location.href = 'supplier_do.aspx?action=remove&supplier_id=' + jQuery('#list').jqGrid('getGridParam', 'selarrrow');" />

                                    <%} %>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
