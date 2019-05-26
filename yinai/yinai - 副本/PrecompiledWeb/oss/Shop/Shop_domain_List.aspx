<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword, defaultkey;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("d43c156b-194d-4a29-a7b2-b55a199ded70");
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入域名、供应商名称进行搜索" && keyword != "")
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入域名、供应商名称进行搜索";
        }
        if (keyword == "输入域名、供应商名称进行搜索")
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
<title>店铺域名申请管理</title>
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
      <td class="content_title">店铺域名申请管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Shop_domain_List.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
			<tr bgcolor="#F5F9FC" >
			<td align="right"><span class="left_nav">搜索</span> 
            
				<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入域名、供应商名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>" /> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
            url: 'Shop_do.aspx?action=domainlist&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID', '店铺域名','域名类型','供应商名称','审核状态' ,'添加时间' ,"操作"],
            colModel: [
				{ width: 50, name: 'SupplierShopDomainInfo.Shop_Domain_ID', index: 'SupplierShopDomainInfo.Shop_Domain_ID', align: 'center', sortable: false },
                { name: 'SupplierShopDomainInfo.Shop_Domain_Name', index: 'SupplierShopDomainInfo.Shop_Domain_Name', align: 'left' },
                {width:150,  name: 'SupplierShopDomainInfo.Shop_Domain_Type', index: 'SupplierShopDomainInfo.Shop_Domain_Type', align:'center' },
                {width:150,  name: 'SupplierShopDomainInfo.Shop_Domain_SupplierID', index: 'SupplierShopDomainInfo.Shop_Domain_SupplierID', align:'center' },
                
                {width:80,  name: 'SupplierShopDomainInfo.Shop_Domain_Status', index: 'SupplierShopDomainInfo.Shop_Domain_Status', align:'center' },
                 {width:120,  name: 'SupplierShopDomainInfo.Shop_Domain_Addtime', index: 'SupplierShopDomainInfo.Shop_Domain_Addtime', align:'center' },
   				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierShopDomainInfo.Shop_Domain_ID',
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
        
        <form action="/orders/shop_do.aspx" method="post">
        <div style="margin-top:5px;">
        <div style="margin-top:5px;">
        <%if (Public.CheckPrivilege("d43c156b-194d-4a29-a7b2-b55a199ded70"))
          { %>
        <input type="button" id="export" class="bt_orange" value="审核通过" onclick="location.href='shop_do.aspx?action=domaincheck&domain_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button1" class="bt_orange" value="审核不通过" onclick="location.href='shop_do.aspx?action=domaindeny&domain_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <%} %>
        </div>
        </form>
        
      </td>
    </tr>
  </table>
</div>
</body>
</html>