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
        if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入昵称、邮箱、姓名、电话、手机进行搜索";
        }
        if (keyword == "输入昵称、邮箱、姓名、电话、手机进行搜索")
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
      <td class="content_title">会员选择</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="member_select.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" size="50" name="keyword" id="keyword" onfocus="this.value=''" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'ordercreate_do.aspx?action=memberlist&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
            colNames: ['选择','ID', '昵称', '注册邮箱','姓名','性别','电话','手机', '会员等级','虚拟账户余额','可用积分', '注册时间'],
            colModel: [
                { width:50,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
				{ width:50,name: 'MemberInfo.Member_ID', index: 'MemberInfo.Member_ID', align: 'center'},
                { width:100,name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName',align: 'center'},
				{ name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email',align: 'center'},
				{ width:50,name: 'Name', index: 'Name',align: 'center'},
				{ width:30,name: 'Sex', index: 'Sex',align: 'center'},
				{ width:60,name: 'Tel', index: 'Tel',align: 'center'},
				{ width:60,name: 'Mobile', index: 'Mobile',align: 'center'},
				{ width:40,name: 'Grade', index: 'Grade',align: 'center'},
				{ width:50,name: 'account', index: 'account',align: 'center'},
				{width:50, name: 'coin', index: 'coin',align: 'center'},
				{ width:100,name: 'MemberInfo.Member_Addtime', index: 'MemberInfo.Member_Addtime', align: 'center'},
				
			],
            sortname: 'MemberInfo.Member_ID',
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