<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<% Public.CheckLogin("9f2a1a11-c019-4443-b6eb-18ab1483e0b9");
   Shop MyApp = new Shop();
   ITools tools;
   tools = ToolsFactory.CreateTools();
   int Shop_Banner_ID = tools.CheckInt(Request["Shop_Banner_ID"]);
   string Shop_Banner_Name = "";
   string Shop_Banner_Url = "";
   string Shop_Banner_Url_img = "";
   int Shop_Banner_IsActive = 0;
   SupplierShopBannerInfo entity = MyApp.GetSupplierShopBannerByID(Shop_Banner_ID);
   if (entity != null)
   {
       Shop_Banner_Name = entity.Shop_Banner_Name;
       Shop_Banner_Url = entity.Shop_Banner_Url;
       Shop_Banner_IsActive = entity.Shop_Banner_IsActive;
   }
   else
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   if (Shop_Banner_Url.Length == 0)
   {
       Shop_Banner_Url_img = "/images/banner_nopic.gif";
   }
   else
   {
       Shop_Banner_Url_img = Public.FormatImgURL(Shop_Banner_Url, "fullpath");
   }
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>店铺Banner修改</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">店铺Banner修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/shop/shop_banner_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">Banner名称</td>
          <td class="cell_content"><input name="Shop_Banner_Name" type="text" id="Shop_Banner_Name" value="<%=Shop_Banner_Name %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">图片上传</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=shopbanner&formname=formadd&frmelement=Shop_Banner_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Shop_Banner_Img" type="hidden" id="Shop_Banner_Img" value="<%=Shop_Banner_Url %>" /></td>
        </tr>
         <tr>
          <td class="cell_title">Banner预览</td>
          <td class="cell_content"><img src="<%=Shop_Banner_Url_img %>"  id="img_Shop_Banner_Img"></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Shop_Banner_IsActive" type="radio" value="1" <% =Public.CheckedRadio(Shop_Banner_IsActive.ToString(), "1")%> />是 <input type="radio" name="Shop_Banner_IsActive" value="0" <% =Public.CheckedRadio(Shop_Banner_IsActive.ToString(), "0")%>/>否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="renew" />
            <input type="hidden" id="Shop_Banner_ID" name="Shop_Banner_ID" value="<%=Shop_Banner_ID %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Shop_Banner_List.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
