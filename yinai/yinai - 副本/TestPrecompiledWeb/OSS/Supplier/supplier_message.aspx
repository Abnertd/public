<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword = "";
    string defaultkey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d8b3c47b-26c4-435f-884e-c9951464b633");
        keyword = Request["keyword"];
        if (keyword != "输入供应商、政策通知标题进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入供应商、政策通知标题进行搜索";
        }
        if (keyword == "输入供应商、政策通知标题进行搜索")
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
      <td class="content_title">政策通知管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="supplier_message.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" onfocus="if(this.value=='输入供应商、政策通知标题进行搜索'){this.value='';}"  id="keyword" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Supplier_message_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey) %>',
			datatype: "json",
			colNames: ['ID', '供应商账号', '供应商名称', '政策通知标题', '状态', '添加时间', "操作"],
            colModel: [
				
				{ width:50,name: 'SupplierMessageInfo.Supplier_Message_ID', index: 'SupplierMessageInfo.Supplier_Message_ID', align: 'center'},
                {  width:150,name: 'Supplier_Account', index: 'Supplier_Account',align: 'center', sortable:false},
                {  name: 'Supplier_Name', index: 'Supplier_Name',align: 'center', sortable:false},
				{name: 'Name', index: 'Name',align: 'center', sortable:false},
				{ width:70,name: 'SupplierMessageInfo.Supplier_Message_IsRead', index: 'SupplierMessageInfo.Supplier_Message_IsRead',align: 'center'},
				{ width:100,name: 'SupplierMessageInfo.Supplier_Message_Addtime', index: 'SupplierMessageInfo.Supplier_Message_Addtime',align: 'center'},
				{width:50, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'SupplierMessageInfo.Supplier_Message_ID',
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