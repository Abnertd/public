<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<% Public.CheckLogin("fca14348-91f1-4522-8063-98ff215d5dab");
   Supplier supplier = new Supplier();
   Session["MessageSupplierInfo"] = new List<SupplierInfo>();
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加佣金分类</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Commission_Cate_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title" valign="top">供应商选择</td>
          <td class="cell_content"><div id="yhnr"><% =supplier.ShowSupplier()%></div>
          </td>
        </tr> 
        <tr>
          <td class="cell_title">佣金分类名称</td>
          <td class="cell_content"><input name="Supplier_Commission_Cate_Name" type="text" id="Supplier_Commission_Cate_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">佣金百分比</td>
          <td class="cell_content"><input name="Supplier_Commission_Cate_Amount" type="text" id="Supplier_Commission_Cate_Amount" size="50" maxlength="50" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
        
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Supplier_list.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
