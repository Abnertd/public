<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    string keyword = "";
    string defaultkey = "";
    int Audit = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools=ToolsFactory.CreateTools();
        Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
        keyword = Request["keyword"];
        Audit = tools.CheckInt(Request["Audit"]);
        if (keyword != "输入Email、公司名称进行搜索"&&keyword!=null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入Email、公司名称进行搜索";
        }
        if (keyword == "输入Email、公司名称进行搜索")
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
<title>采购商资质信息</title>
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
      <td class="content_title">采购商资质信息</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey), "待审核")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=1", "审核通过")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Audit=2", "审核不通过")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td>
    <tr><td>
    <form action="member_cert.aspx?Audit=<%=Audit %>" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<tr >
				<td align="left"><span class="left_nav">搜索</span> 
					<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入Email、公司名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				</tr>
			</table>
            </form>
    </td></tr>
    <tr>
      <td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'member_do.aspx?action=certlist&keyword=<%=Server.UrlEncode(defaultkey) %>&Audit=<%=Audit %>',
            datatype: "json",
            colNames: ['ID', '账号', '公司名称', '联系人', '所在地区',  '资质状态', "操作"],
            colModel: [
				{ width: 50, name: 'MemberInfo.Member_ID', index: 'MemberInfo.Member_ID', align: 'center' },
                { width: 100, name: 'MemberInfo.Member_Account', index: 'MemberInfo.Member_Account', align: 'center' },
				{ name: 'MemberInfo.Member_Company', index: 'MemberInfo.Member_Company', align: 'center' },
				{ width: 50, name: 'MemberInfo.Member_Name', index: 'MemberInfo.Member_Name', align: 'center' },
				{ width: 150, name: 'MemberInfo.Member_Area', index: 'MemberInfo.Member_Area', align: 'center' },
                 { width: 50, name: 'MemberInfo.Member_Cert_Status', index: 'MemberInfo.Member_Cert_Status', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'MemberInfo.Member_ID',
            sortorder: "desc",
            rowNum: GetrowNum(),
            rowList: GetrowList(),
            pager: 'pager',
            multiselect: false,
            viewsortcols: [false, 'horizontal', true],
            width: getTotalWidth() - 35,
            height: "100%"
        });
        </script>
      </td>
    </tr>
  </table>
       </td>
    </tr>
  </table>
</div>
</body>
</html>