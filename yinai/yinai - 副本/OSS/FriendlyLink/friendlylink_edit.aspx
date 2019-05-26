<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private FriendlyLink myApp;
    private ITools tools;

    private string FriendlyLink_Name, FriendlyLink_Img, FriendlyLink_URL, FriendlyLink_Site;
    private int FriendlyLink_ID, FriendlyLink_CateID, FriendlyLink_IsActive, FriendlyLink_IsImg, FriendlyLink_Sort;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("363bfb90-0d0b-42ae-af25-54004fd061e3");
        myApp = new FriendlyLink();
        tools = ToolsFactory.CreateTools();

        FriendlyLink_ID = tools.CheckInt(Request.QueryString["FriendlyLink_ID"]);
        FriendlyLinkInfo entity = myApp.GetFriendlyLinkByID(FriendlyLink_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            FriendlyLink_ID = entity.FriendlyLink_ID;
            FriendlyLink_CateID = entity.FriendlyLink_CateID;
            FriendlyLink_Name = entity.FriendlyLink_Name;
            FriendlyLink_Img = entity.FriendlyLink_Img;
            FriendlyLink_URL = entity.FriendlyLink_URL;
            FriendlyLink_IsActive = entity.FriendlyLink_IsActive;
            FriendlyLink_IsImg = entity.FriendlyLink_IsImg;
            FriendlyLink_Site = entity.FriendlyLink_Site;
            FriendlyLink_Sort = entity.FriendlyLink_Sort;
        }
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
            <% =myApp.CategoryOption(FriendlyLink_CateID)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">链接名称</td>
          <td class="cell_content"><input name="FriendlyLink_Name" type="text" id="FriendlyLink_Name" size="50" maxlength="100" value="<% =FriendlyLink_Name%>"/></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="FriendlyLink_URL" type="text" id="FriendlyLink_URL" size="50" maxlength="100" value="<% =FriendlyLink_URL%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">图片</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=friendlylink&formname=formadd&frmelement=FriendlyLink_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe>  <span class="tip">图片尺寸：168×48</span></td>
        </tr>
        <tr id="tr_FriendlyLink_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_FriendlyLink_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">显示</td>
          <td class="cell_content"><input name="FriendlyLink_IsActive" type="radio" id="FriendlyLink_IsActive1" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="1" <% =Public.CheckedRadio(FriendlyLink_IsActive.ToString(), "1")%>/>是 <input type="radio" name="FriendlyLink_IsActive" id="FriendlyLink_IsActive2" value="0" <% =Public.CheckedRadio(FriendlyLink_IsActive.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">链接次序</td>
          <td class="cell_content"><input name="FriendlyLink_Sort" type="text" id="FriendlyLink_Sort" value="<% =FriendlyLink_Sort%>" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="FriendlyLink_ID" name="FriendlyLink_ID" value="<% =FriendlyLink_ID%>" />
            <input type="hidden" id="FriendlyLink_Img" name="FriendlyLink_Img" value="<% =FriendlyLink_Img%>" />
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