<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    string keyword = "";
    string defaultkey = "";
    string ReqURL = "";
    int Purchase_Type, Trash, audit,overdue, isactive, Purchase_CateID;
    ITools tools;
    Supplier supplier = new Supplier();
    Product product = new Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("c197743d-e397-4d11-b6fc-07d1d24aa774");
        keyword = Request["keyword"];
        Purchase_Type = tools.CheckInt(Request["Purchase_Type"]);
        Trash = tools.CheckInt(Request["Trash"]);
        audit = tools.CheckInt(Request["audit"]);
        isactive = tools.CheckInt(Request["isactive"]);
        Purchase_CateID = tools.CheckInt(Request["Purchase_Cate"]);
        overdue = tools.CheckInt(Request["overdue"]);
        if (Purchase_CateID == 0) { Purchase_CateID = tools.CheckInt(Request["Purchase_Cate_parent"]); }
        if (keyword != "输入采购标题进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入采购标题进行搜索";
        }
        if (keyword == "输入采购标题进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        ReqURL = "?action=list&keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Cate=" + Purchase_CateID + "&Purchase_Type=" + Purchase_Type + "&Trash=" + Trash + "&audit=" + audit + "&isactive=" + isactive + "&overdue=" + overdue;
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
                <td class="content_title">
                    <%
                        if (Trash == 0)
                        {
                            Response.Write("采购申请管理");
                        }
                        else
                        {
                            Response.Write("采购申请回收站");
                        }
                    %>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <%--<tr>
                            <td valign="top" height="26" class="opt_foot">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==0&&isactive==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Type="+Purchase_Type+"&Trash="+Trash+"&audit=0", "全部")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Type="+Purchase_Type+"&Trash="+Trash+"&audit=1", "待审核")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Type="+Purchase_Type+"&Trash="+Trash+"&audit=2", "已审核")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Type="+Purchase_Type+"&Trash="+Trash+"&audit=3", "审核不通过")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(isactive==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Purchase_Type=" + Purchase_Type + "&Trash=" + Trash + "&isactive=1", "已挂起")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <form action="Supplier_Purchase_list.aspx?Purchase_Type=<%=Purchase_Type %>&Trash=<%=Trash %>&audit=<%=audit %>&isactive=<%=isactive %>" method="post" name="frm_sch" id="frm_sch">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr bgcolor="#F5F9FC">
                                        <td align="right">
                                            分类：<span id="main_cate"><%=supplier.Purchase_Category_Select(Purchase_CateID, "main_cate")%></span>
                                            <span class="left_nav">搜索</span>
                                            <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入采购标题进行搜索'){this.value='';}"
                                                id="keyword" value="<% =keyword %>" />
                                            <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                                        </td>
                                    </tr>
                                </table>
                                </form>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="list">
                                </table>
                                <div id="pager">
                                </div>
                                <script type="text/javascript">
                                    jQuery("#list").jqGrid({
                                        url: 'Supplier_Purchase_do.aspx<%=ReqURL %>',
                                        datatype: "json",
                                        colNames: ['ID', '采购分类', '采购标题', '发布人', '采购类型', '交货时间', '有效时间',  '审核状态', '挂起状态', '推荐状态', "操作"],
                                        colModel: [
				{ width: 50, name: 'SupplierPurchaseInfo.Purchase_ID', index: 'SupplierPurchaseInfo.Purchase_ID', align: 'center' },
                 { width: 50, name: 'SupplierPurchaseInfo.Purchase_CateID', index: 'SupplierPurchaseInfo.Purchase_CateID', align: 'center' },
                { name: 'SupplierPurchaseInfo.Purchase_Title', index: 'SupplierPurchaseInfo.Purchase_Title' },
                 { width: 50, name: 'SupplierPurchaseInfo.Purchase_SupplierID', index: 'SupplierPurchaseInfo.Purchase_SupplierID', align: 'center' },
                { width: 50, name: 'SupplierPurchaseInfo.Purchase_TypeID', index: 'SupplierPurchaseInfo.Purchase_TypeID', align: 'center' },
                 
               
				{ width: 70, name: 'SupplierPurchaseInfo.Purchase_DeliveryTime', index: 'SupplierPurchaseInfo.Purchase_DeliveryTime', align: 'center' },
				{ width: 50, name: 'SupplierPurchaseInfo.Purchase_ValidDate', index: 'SupplierPurchaseInfo.Purchase_ValidDate', align: 'center' },
                { width: 50, name: 'SupplierPurchaseInfo.Purchase_Status', index: 'SupplierPurchaseInfo.Purchase_Status', align: 'center' },
                { width: 50, name: 'SupplierPurchaseInfo.Purchase_IsActive', index: 'SupplierPurchaseInfo.Purchase_IsActive', align: 'center' },
               { width: 50, name: 'SupplierPurchaseInfo.Purchase_IsRecommend', index: 'SupplierPurchaseInfo.Purchase_IsRecommend', align: 'center' },
				{ width: 100, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                                        sortname: 'SupplierPurchaseInfo.Purchase_ID',
                                        sortorder: "desc",
                                        rowNum: GetrowNum(),
                                        rowList: GetrowList(),
                                        pager: 'pager',
                                        multiselect: true,
                                        viewsortcols: [false, 'horizontal', true],
                                        width: getTotalWidth() - 35,
                                        height: "100%"
                                    });
                                </script>
                                <div style="margin-top: 5px;">
                                    <% if (audit == 1 && Trash == 0 && Public.CheckPrivilege("398b0fb4-3a5d-4de8-96e1-ef1b9814cbea"))
                                       { %>
                                    <input type="button" id="Button1" class="bt_orange" value="审核通过" onclick="location.href='Supplier_Purchase_do.aspx?action=audit&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <input type="button" id="Button2" class="bt_orange" value="审核不通过" onclick="location.href='Supplier_Purchase_do.aspx?action=denyaudit&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <%}
                                       if (audit == 2 && Trash == 0)
                                       {
                                           if (isactive == 0 && Public.CheckPrivilege("d3c51814-7b5e-4275-bcb4-bdbc3a9f94da"))
                                           { %>
                                    <input type="button" id="Button5" class="bt_orange" value="挂起" onclick="location.href='Supplier_Purchase_do.aspx?action=hangup&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <%}

               if (Public.CheckPrivilege("9ff2555b-8d3b-44c6-87fb-4f8af64d73f5"))
               {
                                    %>
                                    <input type="button" id="Button7" class="bt_orange" value="推荐" onclick="location.href='Supplier_Purchase_do.aspx?action=recommend&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <input type="button" id="Button8" class="bt_orange" value="取消推荐" onclick="location.href='Supplier_Purchase_do.aspx?action=unrecommend&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <%
                                        }
           }
           if (isactive == 1 && Trash == 0 && Public.CheckPrivilege("d3c51814-7b5e-4275-bcb4-bdbc3a9f94da"))
           { %>
                                    <input type="button" id="Button6" class="bt_orange" value="恢复" onclick="location.href='Supplier_Purchase_do.aspx?action=unhangup&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />
                                    <%}
                    if (Trash == 0 && Public.CheckPrivilege("db50b828-d105-43e2-9a40-f9447a999d91"))
                    { %>
                                    <%--<input type="button" id="Button3" class="bt_orange" value="加入回收站" onclick="location.href='Supplier_Purchase_do.aspx?action=trash&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />--%>
                                    <%}
           if (Trash == 1 && Public.CheckPrivilege("db50b828-d105-43e2-9a40-f9447a999d91"))
           { %>
                                    <%--<input type="button" id="Button4" class="bt_orange" value="恢复" onclick="location.href='Supplier_Purchase_do.aspx?action=untrash&Purchase_Type=<%=Purchase_Type %>&purchase_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />--%>
                                    <%}
                                    %>
                                </div>
                            </td>
                        </tr>
                    </table>
    </div>
</body>
</html>
