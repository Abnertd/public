<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword, defaultkey;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入版面标题、标识、供应商名称进行搜索" && keyword != "")
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入版面标题、标识、供应商名称进行搜索";
        }
        if (keyword == "输入版面标题、标识、供应商名称进行搜索")
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
<title>店铺版面管理</title>
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
      <td class="content_title">店铺版面管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Shop_Pages_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
			<tr bgcolor="#F5F9FC" >
			<td align="right"><span class="left_nav">搜索</span> 
            
				<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入版面标题、标识、供应商名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>" /> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
            url: 'Shop_do.aspx?action=pageslist&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID', '版面标题','供应商名称','版面标识','审核状态' ,'添加时间' ,"操作"],
            colModel: [
				{ width: 50, name: 'SupplierShopPagesInfo.Shop_Pages_ID', index: 'SupplierShopPagesInfo.Shop_Pages_ID', align: 'center', sortable: false },
                { name: 'SupplierShopPagesInfo.Shop_Pages_Title', index: 'SupplierShopPagesInfo.Shop_Pages_Title', align: 'left' },
                {width:150,  name: 'SupplierShopPagesInfo.Shop_Pages_SupplierID', index: 'SupplierShopPagesInfo.Shop_Pages_SupplierID', align:'center' },
                {width:80,  name: 'SupplierShopPagesInfo.Shop_Pages_Sign', index: 'SupplierShopPagesInfo.Shop_Pages_Sign', align:'center' },
                {width:80,  name: 'SupplierShopPagesInfo.Shop_Pages_Ischeck', index: 'SupplierShopPagesInfo.Shop_Pages_Ischeck', align:'center' },
                 {width:120,  name: 'SupplierShopPagesInfo.Shop_Pages_Addtime', index: 'SupplierShopPagesInfo.Shop_Pages_Addtime', align:'center' },
   				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierShopPagesInfo.Shop_Pages_ID',
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
        <input type="button" id="export" class="bt_orange" value="审核通过" onclick="location.href='shop_do.aspx?action=pagecheck&pages_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        <input type="button" id="Button1" class="bt_orange" value="审核不通过" onclick="location.href='shop_do.aspx?action=pagedeny&pages_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" /> 
        </div>
        </form>
        
      </td>
    </tr>
  </table>
</div>
</body>
</html>