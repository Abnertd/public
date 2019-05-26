<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    private Category myApp;
    private int Cate_ParentID, page;
    private string keyword, ReqURL;
    private string defaultkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("2883de94-8873-4c66-8f9a-75d80c004acb");
        
        tools = ToolsFactory.CreateTools();
        myApp = new Category();
        Cate_ParentID = tools.CheckInt(Request.QueryString["cate_parentid"]);
        page = tools.CheckInt(Request.QueryString["page"]);
        keyword = Request["keyword"];
        if (keyword != "输入类别名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入类别名称进行搜索";
        }
        if (keyword == "输入类别名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        ReqURL = "keyword=" + Server.UrlEncode(defaultkey);
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
      <td class="content_title">商品分类</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="category.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入类别名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td class="content_content">
      
      <div style="font-size:14px; margin-bottom:5px;"><a href="category.aspx">根类别</a>：<% =myApp.CategoryRecursion(Cate_ParentID)%> <input type="button" name="btn_sch" class="btn_01" id="Button1" value="在本类别添加" onclick="location='category_add.aspx?parent_id=<%=Cate_ParentID %>';" /></div>
      
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'category_do.aspx?action=list&cate_parentid=<% =Cate_ParentID%>&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '商品分类名称', '子类数量', "排序", "操作"],
            colModel: [
				{ width:50,name: 'CategoryInfo.Cate_ID', index: 'CategoryInfo.Cate_ID', align: 'center'},
				{ align:'center',name: 'CategoryInfo.Cate_Name', index: 'CategoryInfo.Cate_Name'},
				{ width:50,align:'center',name: 'Amount', index: 'Amount', sortable:false},
				{ width:50,align:'center',name: 'CategoryInfo.Cate_Sort', index: 'CategoryInfo.Cate_Sort'},
				{ width:100,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'CategoryInfo.Cate_ID',
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
