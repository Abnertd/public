<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<% Public.CheckLogin("8c936480-7e6e-4482-9e22-5eb9b1fbec8a");
   Supplier supplier = new Supplier();
   Session["MessageSupplierInfo"] = new List<SupplierInfo>();
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺样式添加</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
<script type="text/javascript">
<!--

function SelectSupplier(){
		window.open ("/supplier/selectsupplier.aspx?timer=" + Math.random(), "运营支撑系统", "height=560, width=600, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
	}
	function supplier_del(supplier_id){
	    $.ajax({
	        url: encodeURI("/supplier/supplier_do.aspx?action=supplier_del&supplier_id="+ supplier_id +"&timer="+Math.random()),
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
      <td class="content_title">店铺样式添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/shop/shop_Css_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">样式名称</td>
          <td class="cell_content"><input name="Shop_Css_Title" type="text" id="Shop_Css_Title" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">预览上传</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=shopcss&formname=formadd&frmelement=Shop_Css_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Shop_Css_Img" type="hidden" id="Shop_Css_Img" /></td>
        </tr>
         <tr>
          <td class="cell_title">样式预览</td>
          <td class="cell_content"><img src="/images/detail_no_pic.gif"  id="img_Shop_Css_Img"></td>
        </tr>
        <tr>
          <td class="cell_title">店铺类型</td>
          <td class="cell_content"><input name="Shop_Css_Target" type="checkbox" value="1" checked/> 体验店铺 <input type="checkbox" name="Shop_Css_Target" value="2" checked/> 展示店铺  <input type="checkbox" name="Shop_Css_Target" value="3" checked/> 销售店铺</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">指定供应商</td>
          <td class="cell_content"><div id="yhnr"><% =supplier.ShowSupplier()%></div>
          </td>
        </tr> 
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Shop_Css_IsActive" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Shop_Css_IsActive" value="0"/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Shop_Css_List.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
