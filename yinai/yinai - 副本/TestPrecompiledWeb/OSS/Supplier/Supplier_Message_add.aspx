<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Supplier supplier ;
    protected void Page_Load(object sender, EventArgs e)
    {
        supplier = new Supplier();
        Public.CheckLogin("11fe78b3-c971-4ed1-bb5e-3a31b60b19cd");
        Session["MessageSupplierInfo"] = new List<SupplierInfo>();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<script type="text/javascript">
<!--

function SelectSupplier(){
		window.open ("selectsupplier.aspx?timer=" + Math.random(), "运营支撑系统", "height=560, width=600, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
	}
	function supplier_del(supplier_id){
	    $.ajax({
	        url: encodeURI("supplier_do.aspx?action=supplier_del&supplier_id="+ supplier_id +"&timer="+Math.random()),
		    type: "get", 
		    global: false, 
		    async: false,
		    dataType: "html",
		    success:function(data){
			    $("#yhnr").html(data);
		    },
		    error: function (){
			    alert("Error Script");
		    }
        });
	}
//-->
</script>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加政策通知</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="supplier_message_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
      <tr>
          <td class="cell_title" valign="top">供应商选择</td>
          <td class="cell_content"><div id="yhnr"><% =supplier.ShowSupplier()%></div>
          <span class="t12_red">注：不选择用户表示发送至所有供应商！</span></td>
        </tr> 
        <tr>
          <td class="cell_title">通知标题</td>
          <td class="cell_content"><input name="Supplier_Message_Title" type="text" id="Supplier_Message_Title" size="50" maxlength="100" /></td>
        </tr>

        <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Supplier_Message_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">通知内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Supplier_Message_Content" name="Supplier_Message_Content" rows="20"></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Supplier_Message_Content');
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='supplier_message.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>