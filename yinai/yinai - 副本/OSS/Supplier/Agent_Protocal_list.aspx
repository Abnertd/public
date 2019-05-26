<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    string keyword = "";
    string defaultkey = "";
    string ReqURL = "";
    int audit;
    ITools tools;
    Supplier supplier = new Supplier();
    Product product = new Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("0aab7822-e327-4dcd-bc30-4cbf289067e4");
        keyword = tools.CheckStr(Request["keyword"]);
        audit = tools.CheckInt(Request["audit"]);

        if (keyword != "输入采购标题进行搜索" && keyword != null && keyword != "")
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
       // ReqURL = "?action=list&keyword="+Server.UrlEncode(defaultkey)+"&Purchase_Cate="+Purchase_CateID+"&Purchase_Type="+Purchase_Type+"&Trash="+Trash+"&audit="+audit+"&isactive="+isactive;
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
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" height="26" class="opt_foot">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&audit=0", "全部")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&audit=1", "用户未确认")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&audit=2", "用户已确认")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        <td class="<%if(audit==3){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
                                            <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&audit=3", "平台已确认")%>
                                        </td>
                                        <td class="opt_gap">
                                            &nbsp;
                                        </td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <form action="Agent_Protocal_list.aspx?audit=<%=audit %>" method="post" name="frm_sch" id="frm_sch">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr bgcolor="#F5F9FC">
                                        <td align="right">
                                            <span class="left_nav">搜索</span>
                                            <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入采购标题进行搜索'){this.value='';}"
                                                id="keyword" value="<%=keyword %>" />
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
                                        url: 'Agent_Protocal_do.aspx?action=list&audit=<%=audit %>&keyword=<%=Server.UrlEncode(defaultkey) %>',
                                        datatype: "json",
                                        colNames: ['ID', '采购标题', '采购会员', '审核状态', '创建时间', "操作"],
                                        colModel: [
				{ width: 50, name: 'SupplierAgentProtocalInfo.Protocal_ID', index: 'SupplierAgentProtocalInfo.Protocal_ID', align: 'center' },
                { name: 'SupplierAgentProtocalInfo.Protocal_PurchaseID', index: 'SupplierAgentProtocalInfo.Protocal_PurchaseID' },
                { width: 50, name: 'SupplierAgentProtocalInfo.Protocal_SupplierID', index: 'SupplierAgentProtocalInfo.Protocal_SupplierID', align: 'center' },
                { width: 50, name: 'SupplierAgentProtocalInfo.Protocal_Status', index: 'SupplierAgentProtocalInfo.Protocal_Status', align: 'center' },
                  { width: 50, name: 'SupplierAgentProtocalInfo.Protocal_Addtime', index: 'SupplierAgentProtocalInfo.Protocal_Addtime', align: 'center' },
				{ width: 100, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                                        sortname: 'SupplierAgentProtocalInfo.Protocal_ID',
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
                                
                            </td>
                        </tr>
                    </table>
    </div>
</body>
</html>
