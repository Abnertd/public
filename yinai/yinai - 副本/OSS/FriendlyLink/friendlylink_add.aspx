<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private FriendlyLink myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("54dd622d-fc2d-434d-a36a-c4968caf18b3");
        myApp = new FriendlyLink();
        
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">友情链接管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="friendlylink_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">所属类别</td>
          <td class="cell_content">
          <select name="FriendlyLink_CateID" id="FriendlyLink_CateID">
            <% =myApp.CategoryOption(0)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">链接名称</td>
          <td class="cell_content"><input name="FriendlyLink_Name" type="text" id="FriendlyLink_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="FriendlyLink_URL" type="text" id="FriendlyLink_URL" size="50" maxlength="100" value="http://" /></td>
        </tr>
        <tr>
          <td class="cell_title">图片</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=friendlylink&formname=formadd&frmelement=FriendlyLink_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe> <span class="tip">图片尺寸：168×48</span></td>
        </tr>
        <tr id="tr_FriendlyLink_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_FriendlyLink_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">显示</td>
          <td class="cell_content"><input name="FriendlyLink_IsActive" type="radio" id="FriendlyLink_IsActive1" value="1" checked="checked"/>是 <input type="radio" name="FriendlyLink_IsActive" id="Notice_IsHot2" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">链接次序</td>
          <td class="cell_content"><input name="FriendlyLink_Sort" type="text" id="FriendlyLink_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="1" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input type="hidden" id="FriendlyLink_Img" name="FriendlyLink_Img" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='friendlylink_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>