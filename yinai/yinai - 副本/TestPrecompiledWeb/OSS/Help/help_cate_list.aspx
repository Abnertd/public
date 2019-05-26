<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e2e6aec7-ff11-407b-9c3a-6317b06b1a7e");
        
        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = Request["keyword"];

        if (keyword != "输入类别名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入类别名称搜索";
        }
        if (keyword == "输入类别名称搜索")
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
      <td class="content_title">帮助类别管理  [<a href="help_cate_add.aspx">新建类别</a>]</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="help_cate_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">

					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入类别名称搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'help_cate_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '类别名称', "排序", "操作"],
            colModel: [
				{width:40,  name: 'HelpCateInfo.Help_Cate_ID', index: 'HelpCateInfo.Help_Cate_ID', align: 'center'},
				{ name: 'HelpCateInfo.Help_Cate_Name', index: 'HelpCateInfo.Help_Cate_Name'},
				{width:40,  name: 'HelpCateInfo.Help_Cate_Sort', index: 'HelpCateInfo.Help_Cate_Sort', align: 'center'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'HelpCateInfo.Help_Cate_ID',
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