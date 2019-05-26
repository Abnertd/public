<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%  Public.CheckLogin("b83adfda-1c87-4cc1-94e8-b5d905cc3da8");
    string keyword, defaultkey, ReqURL;
    defaultkey = "";
    keyword = Request["keyword"];
    if (keyword != "输入商品参数名称进行搜索" && keyword != null)
    {
        keyword = keyword;
    }
    else
    {
        keyword = "输入商品参数名称进行搜索";
    }
    if (keyword == "输入商品参数名称进行搜索")
    {
        defaultkey = "";
    }
    else
    {
        defaultkey = keyword;
    }
    ReqURL = "keyword=" + Server.UrlEncode(defaultkey);
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
      <td class="content_title">商品参数</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="producttype.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入商品参数名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Producttype_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '商品参数名称', "排序", "操作"],
            colModel: [
				{width:50, name: 'ProductType.ProductType_ID', index: 'ProductType.ProductType_ID', align: 'center'},
				{align:'center', name: 'ProductType.ProductType_Name', index: 'ProductType.ProductType_Name'},
				{width:50, align:'center',name: 'ProductType.ProductType_Sort', index: 'ProductType.ProductType_Sort'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ProductType.ProductType_ID',
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
</div>
</body>
</html>
