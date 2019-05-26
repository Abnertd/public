<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>


<script runat="server">

    private ProductAuditReason myApp;
    private ITools tools;

    private string  reason_note, Act;
    private int reason_ID;


    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ProductAuditReason();
        tools = ToolsFactory.CreateTools();

        Act = tools.CheckStr(Request.QueryString["action"]);

        if (Act == "renew") {
            Public.CheckLogin("158a1875-7682-4781-97ef-7f31e39280c1");

            reason_ID = tools.CheckInt(Request.QueryString["reason_ID"]);
            ProductAuditReasonInfo entity = myApp.GetProductAuditReasonByID(reason_ID);
            if (entity == null) {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
            else {
                reason_ID = entity.Product_Audit_Reason_ID;
                reason_note = entity.Product_Audit_Reason_Note;
            }
            Act = "renew";
        }
        else {
            Public.CheckLogin("a71b2324-aa1c-46c8-8525-742f96b44755");
            Act = "new";
        }
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
      <td class="content_title">商品审核原因 <% if (Public.CheckPrivilege("a71b2324-aa1c-46c8-8525-742f96b44755"))
                                        { %> [<a href="javascript:void(0);" onclick="javascript:$('#block_add').show();">添加原因</a>]<%}%></td>
    </tr>
    <%
        if (Public.CheckPrivilege("a71b2324-aa1c-46c8-8525-742f96b44755"))
        { 
            %>
    <tr id="block_add" <% if (Act != "renew") { Response.Write("style = \"display:none;\""); } %>>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="audit_reason_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

        <tr>
          <td class="cell_title">原因内容</td>
          <td class="cell_content"><input name="reason_note" type="text" id="reason_note" size="50" maxlength="50" value="<% =reason_note%>" /></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="<% =Act%>" />
            <input type="hidden" id="reason_ID" name="reason_ID" value="<% =reason_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="javascript:location='Audit_Reason.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
<%
        }
        %>
    <tr>
      <td class="content_content">
        
        
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'audit_reason_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '原因',  "操作"],
            colModel: [
				{width:50, name: 'Reason.Reason_ID', index: 'Reason.Reason_ID', align: 'center'},
				{align:'center', name: 'Reason.Reason_Note', index: 'Reason.Reason_Note'},
				{width:100, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ProductAuditReasonInfo.Product_Audit_Reason_ID',
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
