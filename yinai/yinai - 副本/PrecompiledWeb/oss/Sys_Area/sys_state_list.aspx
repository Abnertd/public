<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    string keyword = "";
    string defaultkey = "";
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
        tools=ToolsFactory.CreateTools();
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入省份名称进行搜索" && keyword != "")
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入省份名称进行搜索";
        }
        if (keyword == "输入省份名称进行搜索")
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
<title>省份管理</title>
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
      <td class="content_title">省份管理&nbsp;&nbsp;<a href="sys_state_add.aspx" style="font-size:14px; font-weight:bold;">[添加省份]</a></td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="sys_state_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
			<tr bgcolor="#F5F9FC" >
			<td align="right"><span class="left_nav">搜索</span> 
            
				<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入省份名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>" /> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                    url: 'sys_state_do.aspx?action=statelist&keyword=<%=Server.UrlEncode(defaultkey) %>',
                    datatype: "json",
                    colNames: ['ID', '省份名称', '省份编码', '操作'],
                    colModel: [

				{ width: 50, name: 'SysStateInfo.Sys_State_ID', index: 'SysStateInfo.Sys_State_ID', align: 'center' },
                { width: 100, name: 'SysStateInfo.Sys_State_CN', index: 'SysStateInfo.Sys_State_CN', align: 'center' },
				{ name: 'SysStateInfo.Sys_State_Code', index: 'SysStateInfo.Sys_State_Code', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                    sortname: 'SysStateInfo.Sys_State_ID',
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
</div>
</body>
</html>