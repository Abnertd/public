<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    int isnotify;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e996b26f-2c14-482f-b5f6-955f38b50b3f");
        
        ITools tools;
        tools = ToolsFactory.CreateTools();
        defaultkey = "";
        keyword = Request["keyword"];
        isnotify = tools.CheckInt(Request["isnotify"]);
        if (keyword != "输入邮件地址、产品编号、产品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入邮件地址、产品编号、产品名称进行搜索";
        }
        if (keyword == "输入邮件地址、产品编号、产品名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&isnotify=" + isnotify;
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
      <td class="content_title">到货通知管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="product_notify_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  
					<td align="right">
					<span class="left_nav">状态</span> 
					 <select name="isnotify">
					 <option value="0" <%=Public.CheckedSelected("0",isnotify.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",isnotify.ToString()) %>>已通知</option>
					 <option value="2" <%=Public.CheckedSelected("2",isnotify.ToString()) %>>未通知</option>
					 </select>
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入邮件地址、产品编号、产品名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'product_notify_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '邮件地址','产品编码', '产品名称', '规格', '生产企业','状态','时间', "操作"],
            colModel: [
				{ width:50,name: 'notify_ID', index: 'notify_ID', align: 'center'},
				{ width:100,name: 'notify_email', index: 'notify_email', align: 'center'},
				{ width:50,name: 'product_code', index: 'product_code', align: 'center'},
				{ name: 'product_name', index: 'product_name', align: 'center'},
				{ width:80,name: 'product_spec', index: 'product_spec', align: 'center'},
				{ name: 'product_maker', index: 'product_maker', align: 'center'},
				{ width:50,name: 'notify_status', index: 'notify_status', align: 'center'},
				{ width:80,name: 'notify_time', index: 'notify_time', align: 'center'},
				{ width:100,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ProductNotifyInfo.Product_Notify_ID',
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