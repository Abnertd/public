<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string keyword="";
    string defaultkey = "";
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();
        keyword = Request["keyword"];
        if (keyword != "输入捆绑商品名称关键词进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入捆绑商品名称关键词进行搜索";
        }
        if (keyword == "输入捆绑商品名称关键词进行搜索")
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
      <td class="content_title">捆绑产品选择</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="package_select.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入捆绑商品名称关键词进行搜索'){this.value='';}" id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'ordercreate_do.aspx?action=packagelist&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['ID',   '商品名称', '捆绑价格','查看'],
            colModel: [
				{width:50, name: 'PackageInfo.Package_ID', index: 'PackageInfo.Package_ID', align: 'center'},
				{ name: 'PackageInfo.Package_Name', index: 'PackageInfo.Package_Name'},
				{ width:100,name: 'PackageInfo.Package_Price', index: 'PackageInfo.Package_Price', align: 'center'},
				{ width:80,name: 'view', index: 'view', align: 'center'},
			],
            sortname: 'PackageInfo.Package_ID',
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
        <div>
        <input type="button" value="选择" class="btn_01" onclick="$.ajaxSetup({async: false});$('#goods_tmpinfo',window.opener.document).load('/ordercreate/ordercreate_do.aspx?action=addpackage&package_id=' + jQuery('#list').jqGrid('getGridParam','selarrrow') + '&fresh=' + Math.random() + '');window.close();">
        </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>