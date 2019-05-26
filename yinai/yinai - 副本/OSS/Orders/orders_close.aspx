<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
 
       
    private int Orders_ID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("705ee66c-4a2f-4064-9f34-3c4fb5d45e21");

        tools = ToolsFactory.CreateTools();
        
        Orders_ID = tools.CheckInt(Request.QueryString["Orders_ID"]);
    }

    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;
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

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">订单关闭</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="Form1" name="frm_delivery" method="post" action="orders_do.aspx?orders_id=<%=Orders_ID%>">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
              <tr>
                <td class="cell_title">关闭原因</td>
                <td valign="top" align="left" class="cell_content">
                  <textarea name="orders_close_note" id="orders_close_note" cols="45" rows="5"></textarea></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input name="button" type="submit" class="bt_orange" id="Submit1" value="保存" />
				<input name="action" type="hidden" id="action" value="order_close" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='/orders/orders_view.aspx?orders_id=<%=Orders_ID %>';"/></td>
          </tr>
        </table>
			</form>
      </td>
    </tr>
  </table>
</div>



</body>
</html>