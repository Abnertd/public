<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<% Public.CheckLogin("deaa9168-3ffc-42c3-bb94-829fbf7f2e22");
   Supplier supplier = new Supplier();
   ITools tools;
   tools = ToolsFactory.CreateTools();
    string Supplier_Tag_Name,Supplier_Tag_Img,Supplier_Tag_Content,Supplier_Tag_Site;
   int Supplier_Tag_ID = tools.CheckInt(Request.QueryString["Supplier_Tag_ID"]);
   Supplier_Tag_Name = "";
   Supplier_Tag_Img = "";
   Supplier_Tag_Content = "";
   SupplierTagInfo entity = supplier.GetSupplierTagByID(Supplier_Tag_ID);
   if (entity == null)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   else
   {
       Supplier_Tag_ID = entity.Supplier_Tag_ID;
       Supplier_Tag_Name = entity.Supplier_Tag_Name;
       Supplier_Tag_Img = entity.Supplier_Tag_Img;
       Supplier_Tag_Content = entity.Supplier_Tag_Content;
       Supplier_Tag_Site = entity.Supplier_Tag_Site;

   }
   
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>供应商标签修改</title>
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
      <td class="content_title">供应商标签修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Tag_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">标签名称</td>
          <td class="cell_content"><input name="Supplier_Tag_Name" type="text" id="Supplier_Tag_Name" value="<%=Supplier_Tag_Name %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">标签图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=supplier&formname=formadd&frmelement=Supplier_Tag_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe><input name="Supplier_Tag_Img" type="hidden" id="Supplier_Tag_Img" value="<%=Supplier_Tag_Img %>" /></td>
        </tr>
        <tr id="tr_Supplier_Tag_Img" <%if(Supplier_Tag_Img.Length==0){Response.Write("style='display:none;'");} %>>
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<% =Application["upload_server_url"]+Supplier_Tag_Img%>" id="img_Supplier_Tag_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Supplier_Tag_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">页面内容</td>
          <td class="cell_content">
            <textarea cols="80" id="About_Content" name="Supplier_Tag_Content" rows="16"><%=Supplier_Tag_Content%></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Supplier_Tag_Content');
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="renew" />
            <input type="hidden" id="Supplier_Tag_ID" name="Supplier_Tag_ID" value="<%=Supplier_Tag_ID %>" />
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
