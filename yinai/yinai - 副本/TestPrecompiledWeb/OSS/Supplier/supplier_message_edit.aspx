<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Supplier supplier = new Supplier();
    int Supplier_Message_ID;
    int supplier_id = 0;
    string message_title = "";
    string message_content = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b7d38ac5-000c-4d07-9ca3-46df47367554");
        Supplier supplier = new Supplier();
        ITools tools;
        tools = ToolsFactory.CreateTools();
        Supplier_Message_ID = tools.CheckInt(Request["Supplier_Message_ID"]);
        SupplierMessageInfo entity = supplier.GetSupplierMessageByID(Supplier_Message_ID);
        if (entity != null)
        {
            supplier_id = entity.Supplier_Message_SupplierID ;
            message_title = entity.Supplier_Message_Title;
            message_content = entity.Supplier_Message_Content;
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
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
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">政策通知</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="supplier_message_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
      <tr>
          <td class="cell_title">目标供应商</td>
          <td class="cell_content"><%=supplier.Select_Supplier("Supplier_Message_SupplierID", supplier_id)%></td>
        </tr>
        <tr>
          <td class="cell_title">通知标题</td>
          <td class="cell_content"><input name="Supplier_Message_Title" type="text" id="Supplier_Message_Title" size="50" value="<%=message_title %>" maxlength="100" /></td>
        </tr>

        <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Supplier_Message_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">通知内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Supplier_Message_Content" name="Supplier_Message_Content" rows="20"><% =message_content%></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Supplier_Message_Content');
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Supplier_Message_ID" name="Supplier_Message_ID" value="<%=Supplier_Message_ID %>" />
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