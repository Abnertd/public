<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    int CateID;
    private ArticleCate myAppC;
    string keyword, defaultkey, ReqURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7");
        myAppC = new ArticleCate();
        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = Request["keyword"];
        CateID = tools.CheckInt(Request["article_cate"]);
        if (keyword != "输入文章标题题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入文章标题题搜索";
        }
        if (keyword == "输入文章标题题搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&CateID=" + CateID;
        
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
      <td class="content_title">管理文章</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="Article_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">类别</span> 
					 <span id="main_cate"> <% =myAppC.Article_Category_Select(CateID, "main_cate")%>
                      </span>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入文章标题题搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Article_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '文章标题', "所属类别" ,"排序","操作"],
            colModel: [
				{width:40,  name: 'ArticleInfo.Article_ID', index: 'ArticleInfo.Article_ID', align: 'center'},
				{ name: 'ArticleInfo.Article_Title', index: 'ArticleInfo.Article_Title'},
				{ width: 100, align: 'center', name: 'ArticleInfo.Article_CateID', index: 'ArticleInfo.Article_CateID' },
                //{ width: 100, align: 'center', name: 'ArticleInfo.Article_CateID', index: 'ArticleInfo.Article_CateID' },
                { width: 30, align: 'center', name: 'ArticleInfo.Article_Sort', index: 'ArticleInfo.Article_Sort' },
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ArticleInfo.Article_ID',
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