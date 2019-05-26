
<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    int member_grade = 0;
    string defaultkey = "";
    string member_source = "";
    int Audit = 0;
    int Member_Trash = 0;
    int Member_Status = 0; 
    Member myApp;
    string date_start, date_end,listtype;
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("3a9a9cdf-ef00-407d-98ef-44e23be397e8");
        tools = ToolsFactory.CreateTools();
        myApp = new Member();
        keyword = Request["keyword"];
        member_grade = tools.CheckInt(Request["member_grade"]);
        member_source = tools.CheckStr(Request["member_source"]);
        listtype = tools.CheckStr(Request["listtype"]);

        Audit = tools.CheckInt(Request["Audit"]);
        Member_Trash = tools.CheckInt(Request["Trash"]);
        Member_Status = tools.CheckInt(Request["Status"]);

        if (listtype == "all" || listtype == "")
        {
            listtype = "audit";
        }
        
        
        if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索"&&keyword!=null)
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
        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        Audit = -1;        
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
      <td class="content_title">采购商管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">


 <%--     <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
           <td class="opt_gap">&nbsp;</td>
      <td class="<%if (listtype=="audit") { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
      <%=Public.Page_Option("?listtype=audit&keyword=" + Server.UrlEncode(defaultkey) + "", "审核通过")%></td>


      <td class="opt_gap">&nbsp;</td>
      <td class="<%if (listtype == "uncommitted") { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
      <%=Public.Page_Option("?listtype=uncommitted&keyword=" + Server.UrlEncode(defaultkey) + "", "未提交审核")%>
      </td>

      <td class="opt_gap">&nbsp;</td>
      <td class="<%if (listtype == "unaudit") { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
      <%=Public.Page_Option("?listtype=unaudit&keyword=" + Server.UrlEncode(defaultkey) + "", "未审核")%></td>

     

      <td class="opt_gap">&nbsp;</td>
      <td class="<%if (listtype == "denyaudit") { Response.Write("opt_cur"); } else { Response.Write("opt_uncur"); } %>">
      <%=Public.Page_Option("?listtype=denyaudit&keyword=" + Server.UrlEncode(defaultkey) + "", "审核未通过")%></td>
      </tr>
      </table>
      </td></tr>--%>



      <tr><td>
        <form action="member_list.aspx" method="post" name="frm_sch" id="frm_sch">
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tr bgcolor="#F5F9FC">
                <td align="right">
                    <span class="left_nav">搜索</span> 注册日期：
                    <input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10"
                        readonly="readonly" value="<%=date_start %>" />
                    -
                    <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10"
                        readonly="readonly" value="<%=date_end%>" />

                    <script type="text/javascript">
                        $(document).ready(function() {
                            $("#date_start").datepicker({ numberOfMonths: 1 });
                            $("#date_end").datepicker({ numberOfMonths: 1 });
                        });
                    </script>

                    等级：<%=myApp.GetMemberGradeHTML(member_grade, "member_grade")%>
                    来源：<%=myApp.GetMemberSourceHTML(member_source, "member_source")%>
                    <input type="hidden" name="listtype" value="<% =Request["listtype"]%>" /><input type="text"
                        name="keyword" size="50" onfocus="if(this.value=='输入昵称、邮箱、姓名、电话、手机进行搜索'){this.value='';}"
                        id="keyword" value="<% =keyword %>">
                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" />
                </td>
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
                url: 'member_do.aspx?action=list&listtype=<%=listtype%>&member_source=<%=member_source %>&date_start=<% =Request["date_start"]%>&date_end=<% =Request["date_end"]%>&member_grade=<%=Request["member_grade"] %>&keyword=<%=Server.UrlEncode(defaultkey) %>',
            datatype: "json",
                //colNames: ['ID', '用户名', 'Email','公司名称','联系人','联系人手机','联系电话', '审核状态','注册时间', "操作"],
            colNames: ['ID', '用户名', 'Email','公司名称','联系人','联系人手机','联系电话', '注册时间', "操作"],
            colModel: [
				{ width:30,name: 'MemberInfo.Member_ID', index: 'MemberInfo.Member_ID', align: 'center'},
                { width:70,name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName',align: 'center'},
				{ width:100,name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email',align: 'center'},
                { width:80,name: 'company', index: 'company',align: 'center', sortable:false},
				{ width:50,name: 'Name', index: 'Name',align: 'center', sortable:false},
				{ width:60,name: 'Tel', index: 'Tel',align: 'center', sortable:false},
				{ width:60,name: 'Mobile', index: 'Mobile',align: 'center', sortable:false},
				//{ width:70,name: 'MemberInfo.Member_AuditStatus', index: 'MemberInfo.Member_AuditStatus', align: 'center'},
				{ width:70,name: 'MemberInfo.Member_Addtime', index: 'MemberInfo.Member_Addtime', align: 'center'},
				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
            ],
            sortname: 'MemberInfo.Member_ID',
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
        <form action="/member/member_do.aspx" method="post">

        <div style="margin-top:5px;">
        
       
            <%if (Public.CheckPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe") && listtype == "unaudit")
              {%>

            <input type="button" id="Button1" class="bt_orange" value="审核通过" onclick="location.href='member_do.aspx?action=audit&listtype=<% =Request["listtype"]%>    &member_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />

            <input type="button" id="Button2" class="bt_orange" value="审核不通过" onclick="location.href='member_do.aspx?action=denyaudit&listtype=<% =Request["listtype"]%>    &member_id='+jQuery('#list').jqGrid('getGridParam','selarrrow');" />

            <%} %>
        </div>
        </form>
      </td>
    </tr>
  </table>
       </td>
    </tr>
  </table>
</div>
</body>
</html>