<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    int Audit = 0;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("81e0af57-348d-4565-9e73-7146b3116b8c");
        Audit = tools.CheckInt(Request["Audit"]);
        keyword = Request["keyword"];
        if (keyword != "输入供应商名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入供应商名称进行搜索";
        }
        if (keyword == "输入供应商名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>供应商店铺关闭申请管理</title>
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
      <td class="content_title">供应商店铺关闭申请管理</td>
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
      <tr><td>
      <form action="Supplier_Tag_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr>
					<td><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入供应商名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				 
				</table>
				 </form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'supplier_closeshop_apply_Do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>&Audit=<%=Audit %>',
			datatype: "json",
            colNames: ['ID', '申请供应商','申请退款备注','状态','申请时间', "操作"],
            colModel: [
				
				{ width:50,name: 'SupplierCloseShopApplyInfo.CloseShop_Apply_ID', index: 'SupplierCloseShopApplyInfo.CloseShop_Apply_ID', align: 'center', sortable:false},
				{name: 'SupplierCloseShopApplyInfo.CloseShop_Apply_SupplierID', index: 'SupplierCloseShopApplyInfo.CloseShop_Apply_SupplierID',align: 'center'},
				{name: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Note', index: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Note',align: 'center'},
				{name: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Status', index: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Status',align: 'center'},
				{name: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Addtime', index: 'SupplierCloseShopApplyInfo.CloseShop_Apply_Addtime',align: 'center'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierCloseShopApplyInfo.CloseShop_Apply_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager',
			multiselect: false,
			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			height: "100%"
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