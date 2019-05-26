<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private PayType myApp;
    private ITools tools;

    private string Pay_Type_Name,Pay_Type_Site;
    private int Pay_Type_ID, Pay_Type_Sort, Pay_Type_IsActive;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("fcd3782c-b791-40c6-a29d-9b43092de04f");
        myApp = new PayType();
        tools = ToolsFactory.CreateTools();

        Pay_Type_ID = tools.CheckInt(Request.QueryString["Pay_Type_ID"]);
        PayTypeInfo entity = myApp.GetPayTypeByID(Pay_Type_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Pay_Type_ID = entity.Pay_Type_ID;
            Pay_Type_Name = entity.Pay_Type_Name;
            Pay_Type_IsActive = entity.Pay_Type_IsActive;
            Pay_Type_Sort = entity.Pay_Type_Sort;
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
      <td class="content_title">支付条件修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="pay_type_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        
        <tr>
          <td class="cell_title">支付条件名称</td>
          <td class="cell_content"><input name="Pay_Type_Name" type="text" id="Pay_Type_Name" size="50" maxlength="50" value="<% =Pay_Type_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Pay_Type_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Pay_Type_Sort" size="10" maxlength="10" value="<% =Pay_Type_Sort%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Pay_Type_IsActive" type="radio" value="1" <% =Public.CheckedRadio(Pay_Type_IsActive.ToString(), "1")%> />是 <input type="radio" name="Pay_Type_IsActive" value="0" <% =Public.CheckedRadio(Pay_Type_IsActive.ToString(), "0")%>/>否</td>
        </tr>

      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Pay_Type_ID" name="Pay_Type_ID" value="<% =Pay_Type_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='pay_type_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>