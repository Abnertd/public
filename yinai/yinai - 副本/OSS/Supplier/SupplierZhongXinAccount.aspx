<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    ITools tools;
    string keyword = "";
    string defaultkey = "";
    int Audit = 0;
    int Supplier_Trash = 0;
    int Supplier_Status = 0;
    int Supplier_Type;
    string sjtype = "";
    string Supplier_IsJiaoFei = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        tools=ToolsFactory.CreateTools();
        Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
        keyword = tools.CheckStr(Request["keyword"]);
        Audit = tools.CheckInt(Request["Audit"]);
        Supplier_Trash = tools.CheckInt(Request["Trash"]);
        Supplier_Status = tools.CheckInt(Request["Status"]);
        Supplier_Type = 1;// tools.CheckInt(Request["Supplier_Type"]);
        Supplier_IsJiaoFei = tools.CheckStr(Request["Supplier_IsJiaoFei"]);
        
        if (Supplier_Status == 2)
        {
            Audit = 2;
        }
        if (keyword.Length==0)
        {
            keyword = "输入用户名、Email、公司名称进行搜索";
        }
        if (keyword == "输入用户名、Email、公司名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        Audit = 1;

        if (Supplier_Type==0)
        {
            sjtype = "卖家信息";
        }
        else
        {
            sjtype = "买家信息";
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>中信担保账户汇总</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
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
      <td class="content_title">中信担保账户汇总</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="26" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Audit == 1 && Supplier_Status == 0){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Supplier_Type=" + Supplier_Type + "&Trash=-1&Audit=-1", "全部商家")%>
      </td>

      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Supplier_IsJiaoFei=="0"){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Supplier_Type=" + Supplier_Type + "&Supplier_IsJiaoFei=0&Status=-1", "未缴费")%></td>

      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Supplier_IsJiaoFei=="1"){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?keyword=" + Server.UrlEncode(defaultkey) + "&Supplier_Type=" + Supplier_Type + "&Supplier_IsJiaoFei=1&Status=-1", "已缴费")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td>
    <form action="Supplier_list.aspx?Audit=<%=Audit %>&Supplier_Type=<%=Supplier_Type %>" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<tr >
				<td align="left"><span class="left_nav">搜索</span> 
					
					<input type="text" name="keyword" size="50" onfocus="if(this.value=='输入用户名、Email、公司名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                url: 'Supplier_do.aspx?action=zhongxin_list&account=1&keyword=<%=Server.UrlEncode(defaultkey) %>&Audit=<%=Audit %>&Supplier_Type=<%=Supplier_Type %>&Status=<%=Supplier_Status %>&Trash=<%=Supplier_Trash %>&Supplier_IsJiaoFei=<%=Supplier_IsJiaoFei %>',
                datatype: "json",
                colNames: ['ID', "用户名", '公司名称', '联系人', '联系电话', '审核状态', '启用状态', '注册时间', '最后登录时间', '担保余额', "操作"],
                colModel: [

				{ width: 50, name: 'SupplierInfo.Supplier_ID', index: 'SupplierInfo.Supplier_ID', align: 'center' },
                { width: 100, name: 'SupplierInfo.Supplier_Nickname', index: 'SupplierInfo.Supplier_Nickname', align: 'center' },
				{ name: 'SupplierInfo.Supplier_CompanyName', index: 'SupplierInfo.Supplier_CompanyName', align: 'center' },
				{ width: 50, name: 'SupplierInfo.Supplier_Contactman', index: 'SupplierInfo.Supplier_Contactman', align: 'center' },
                 { width: 50, name: 'SupplierInfo.Supplier_Phone', index: 'SupplierInfo.Supplier_Phone', align: 'center' },
                { width: 50, name: 'SupplierInfo.Supplier_AuditStatus', index: 'SupplierInfo.Supplier_AuditStatus', align: 'center' },
				{ width: 50, name: 'SupplierInfo.Supplier_Status', index: 'SupplierInfo.Supplier_Status', align: 'center' },
				{ width: 70, name: 'SupplierInfo.Supplier_Addtime', index: 'SupplierInfo.Supplier_Addtime', align: 'center' },
                { width: 70, name: 'SupplierInfo.Supplier_Lastlogintime', index: 'SupplierInfo.Supplier_Lastlogintime', align: 'center' },
//                { width: 50, name: 'SupplierInfo.Supplier_LoginCount', index: 'SupplierInfo.Supplier_LoginCount', align: 'center' },
                {width: 50, name: 'AccountRemain', index: 'AccountRemain', align: 'center', sortable: false },
				{ width: 90, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                sortname: 'SupplierInfo.Supplier_ID',
                sortorder: "desc",
                rowNum: GetrowNum(),
                rowList: GetrowList(),
                pager: 'pager',
                multiselect: false,
                viewsortcols: [false, 'horizontal', false],
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