<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Supplier myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("6be3f53c-4751-403c-8b6c-91f1527e32a1");

        myApp = new Supplier();

        Session["MessageSupplierInfo"] = new List<SupplierInfo>();

        if (Request["action"] == "send_mail")
        {
            myApp.Send_Sysemail();
        }

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
    window.open("selectsupplier.aspx?timer=" + Math.random(), "运营支撑系统", "height=560, width=600, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
	}
	function supplier_del(supplier_id) {
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
      <td class="content_title">订阅邮件发送</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="sysemail_send.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
      <tr>
          <td class="cell_title" valign="top">会员选择</td>
          <td class="cell_content"><div id="yhnr"><% =myApp.ShowSupplier()%></div>
          <span class="t12_red">注：不选择用户表示发送至所有会员！</span></td>
        </tr> 
        <tr>
          <td class="cell_title">邮件标题</td>
          <td class="cell_content"><input name="sysmail_title" type="text" id="sysmail_title" size="50" maxlength="50" /></td>
        </tr>

        <tr>
          <td class="cell_title" valign="top">邮件内容</td>
          <td class="cell_content">
            <textarea cols="80" id="sysmail_content" name="sysmail_content" rows="16"></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('sysmail_content');
            </script>
          </td>
        </tr>
        
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="send_mail" />
            <input name="save" type="submit" class="bt_orange" id="save" value="发送" />
             </td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>