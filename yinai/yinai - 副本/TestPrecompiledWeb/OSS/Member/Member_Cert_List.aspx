<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        keyword = Request["keyword"];
        if (keyword != "输入资质文件名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入资质文件名称进行搜索";
        }
        if (keyword == "输入资质文件名称进行搜索")
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
<title>采购商资质配置管理</title>
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
      <td class="content_title">采购商资质配置管理&nbsp;&nbsp;<a href="Member_Cert_add.aspx">[添加采购商资质配置]</a></td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="member_Cert_list.aspx" method="post" name="frm_sch" id="frm_sch" >
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				
				  <tr>
					<td><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入资质文件名称进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                url: '/member/Member_Cert_Do.aspx?action=cert_list&keyword=<%=Server.UrlEncode(defaultkey) %>',
            datatype: "json",
            colNames: ['ID', '供应商类型', '资质文件名称', '排序', "操作"],
            colModel: [

				{ width: 50, name: 'MemberCertInfo.Supplier_Cert_ID', index: 'MemberCertInfo.Member_Cert_ID', align: 'center', sortable: false },
				{ name: 'MemberCertInfo.Member_Cert_Type', index: 'MemberCertInfo.Member_Cert_Type', align: 'center', hidden: true },
				{ name: 'MemberCertInfo.Member_Cert_Name', index: 'MemberCertInfo.Member_Cert_Name', align: 'center' },
				{ name: 'MemberCertInfo.Member_Cert_Sort', index: 'MemberCertInfo.Member_Cert_Sort', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
            ],
            sortname: 'MemberCertInfo.Member_Cert_ID',
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