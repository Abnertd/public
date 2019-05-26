<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<% Public.CheckLogin("227ca224-42de-48c6-9e4b-d09d019f7b36");
   Shop MyApp = new Shop();
   Supplier supplier = new Supplier();
   ITools tools;
   tools = ToolsFactory.CreateTools();
   int Shop_Css_ID = tools.CheckInt(Request["Shop_Css_ID"]);
   string Shop_Css_Title = "";
   string Shop_Css_Img = "";
   int Shop_Css_IsActive = 0;
   string Shop_Css_Target = "";
   string Shop_Css_Img_img = "";
   SupplierShopCssInfo entity = MyApp.GetSupplierShopCssByID(Shop_Css_ID);
   if (entity != null)
   {
       Shop_Css_Title = entity.Shop_Css_Title;
       Shop_Css_Target=entity.Shop_Css_Target;
       Shop_Css_Img = entity.Shop_Css_Img;
       Shop_Css_IsActive = entity.Shop_Css_IsActive;
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   if (Shop_Css_Img.Length == 0)
   {
       Shop_Css_Img_img = "/images/detail_nopic.gif";
   }
   else
   {
       Shop_Css_Img_img = Public.FormatImgURL(Shop_Css_Img, "fullpath");
   }
   Shop_Css_Target = ",," + Shop_Css_Target + ",";
   Session["MessageSupplierInfo"] = MyApp.GetSupplierShopCssRelateSuppler(Shop_Css_ID);
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺样式修改</title>
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
      <td class="content_title">店铺样式修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/shop/shop_css_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">样式名称</td>
          <td class="cell_content"><input name="Shop_Css_Title" type="text" id="Shop_Css_Title" value="<%=Shop_Css_Title %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">预览上传</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=shopcss&formname=formadd&frmelement=Shop_Css_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Shop_Css_Img" type="hidden" id="Shop_Css_Img" value="<%=Shop_Css_Img %>" /></td>
        </tr>
         <tr>
          <td class="cell_title">样式预览</td>
          <td class="cell_content"><img src="<%=Shop_Css_Img_img %>"  id="img_Shop_Css_Img"></td>
        </tr>
        <tr>
          <td class="cell_title">店铺类型</td>
          <td class="cell_content"><input name="Shop_Css_Target" type="checkbox" value="1" <%if(Shop_Css_Target.IndexOf(",1,")>0){Response.Write("checked");} %>/> 体验店铺 <input type="checkbox" name="Shop_Css_Target" value="2" <%if(Shop_Css_Target.IndexOf(",2,")>0){Response.Write("checked");} %>/> 展示店铺  <input type="checkbox" name="Shop_Css_Target" value="3" <%if(Shop_Css_Target.IndexOf(",3,")>0){Response.Write("checked");} %>/> 销售店铺</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">指定供应商</td>
          <td class="cell_content"><div id="yhnr"><% =supplier.ShowSupplier()%></div>
          </td>
        </tr> 
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Shop_Css_IsActive" type="radio" value="1" <% =Public.CheckedRadio(Shop_Css_IsActive.ToString(), "1")%> />是 <input type="radio" name="Shop_Css_IsActive" value="0" <% =Public.CheckedRadio(Shop_Css_IsActive.ToString(), "0")%>/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="renew" />
            <input type="hidden" id="Shop_Css_ID" name="Shop_Css_ID" value="<%=Shop_Css_ID %>" />
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
