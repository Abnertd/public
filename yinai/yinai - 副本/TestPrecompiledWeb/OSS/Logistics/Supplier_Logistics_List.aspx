<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    int list;
    private SupplierLogistics MyApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("64bb04aa-9b78-4c41-ae9c-e94f57581e22");
        tools = ToolsFactory.CreateTools();
        MyApp = new SupplierLogistics();
        defaultkey = "";
        list = tools.CheckInt(Request["list"]);
        keyword = Request["keyword"];

        if (keyword != "输入货物名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入货物名称搜索";
        }
        if (keyword == "输入货物名称搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "list=" + list + "&keyword=" + Server.UrlEncode(defaultkey);
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
      <td class="content_title">物流商管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="Supplier_Logistics_List.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">

					<span class="left_nav">搜索</span> 
					审核状态：<%=MyApp.SupplierLogisticsStatus_Option(list,"list") %>
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入货物名称搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                url: 'Supplier_Logistics_do.aspx?action=list&<%=ReqURL %>',
                datatype: "json",
                colNames: ['ID', '货物名称', "发货地", "收货地", "数量", "发货时间", "状态", "操作"],
                colModel: [
                    { width: 40, name: 'SupplierLogisticsInfo.Supplier_Logistics_ID', index: 'SupplierLogisticsInfo.Supplier_Logistics_ID', align: 'center' },
                    { name: 'SupplierLogisticsInfo.Supplier_Logistics_Name', index: 'SupplierLogisticsInfo.Supplier_Logistics_Name', align: 'center' },
                    { width: 120, name: '.Supplier_Address_County', index: 'Supplier_Address_County', align: 'center', sortable: false },
                    { width: 120, name: 'Supplier_Orders_Address_County', index: 'Supplier_Orders_Address_County', align: 'center', sortable: false },
                    { width: 40, name: 'SupplierLogisticsInfo.Supplier_Logistics_Number', index: 'SupplierLogisticsInfo.Supplier_Logistics_Number', align: 'center' },
                    { width: 60, name: 'SupplierLogisticsInfo.Supplier_Logistics_DeliveryTime', index: 'SupplierLogisticsInfo.Supplier_Logistics_DeliveryTime', align: 'center' },
                    { width: 40, name: 'SupplierLogisticsInfo.Supplier_Logistics_IsAudit', index: 'SupplierLogisticsInfo.Supplier_Logistics_IsAudit', align: 'center' },
                    { width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                ],
                sortname: 'SupplierLogisticsInfo.Supplier_Logistics_ID',
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