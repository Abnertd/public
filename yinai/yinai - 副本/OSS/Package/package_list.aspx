<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    string listtype;
    int isinsale;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("0dd17a70-862d-4e57-9b45-897b98e8a858");
        tools = ToolsFactory.CreateTools();

        listtype = tools.CheckStr(Request.QueryString["listtype"]);

        
       

        defaultkey = "";
        keyword = Request["keyword"];
        isinsale = tools.CheckInt(Request["isinsale"]);
        if (keyword != "输入捆绑名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入捆绑名称进行搜索";
        }
        if (keyword == "输入捆绑名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&isinsale=" + isinsale;
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
      <td class="content_title">捆绑产品管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="package_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right">
					<span class="left_nav">状态</span> 
					 <select name="isinsale">
					 <option value="0" <%=Public.CheckedSelected("0",isinsale.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",isinsale.ToString()) %>>上架</option>
					 <option value="2" <%=Public.CheckedSelected("2",isinsale.ToString()) %>>下架</option>
					 </select>
					 <span class="left_nav">搜索</span> 
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入捆绑名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
            url: 'package_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '名称', '捆绑价格', '重量', '库存','销售状态', '添加时间', "操作"],
            colModel: [
				{width:50, name: 'PackageInfo.Package_ID', index: 'PackageInfo.Package_ID', align: 'center'},
				{ name: 'PackageInfo.Package_Name', index: 'PackageInfo.Package_Name'},
				{width:80, name: 'PackageInfo.Package_Price', index: 'PackageInfo.Package_Price', align: 'center'},
				{width:80, name: 'PackageInfo.Package_Weight', index: 'PackageInfo.Package_Weight', align: 'center'},
				{width:50, name: 'PackageInfo.Package_StockAmount', index: 'PackageInfo.Package_StockAmount', align: 'center'},
				{width:50, name: 'PackageInfo.Package_isinsale', index: 'PackageInfo.Package_isinsale', align: 'center'},
				{width:100, name: 'PackageInfo.Package_Addtime', index: 'PackageInfo.Package_Addtime', align: 'center'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'PackageInfo.Package_ID',
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