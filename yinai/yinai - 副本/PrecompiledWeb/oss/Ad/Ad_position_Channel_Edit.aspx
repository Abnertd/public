<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private AD Ad;
    private ITools tools;

    private int channel_id;
    private string channel_name, channel_note, channel_site;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c6dba721-72aa-4ca4-86fe-2306566e17eb");
        
        Ad = new AD();
        tools = ToolsFactory.CreateTools();

        channel_id = tools.CheckInt(Request.QueryString["channel_id"]);
        if (channel_id == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ADPositionChannelInfo entity = Ad.GetAD_Position_ChannelByID(channel_id);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            channel_id = entity.AD_Position_Channel_ID;
            channel_name = entity.AD_Position_Channel_Name  ;
            channel_note  = entity.AD_Position_Channel_Note  ;
            channel_site  = entity.AD_Position_Channel_Site ;
            
        }
    }
</script>


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
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改广告频道</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/ad/ad_position_channel_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">频道名称</td>
          <td class="cell_content"><input name="AD_Position_Channel_Name" type="text" id="AD_Position_Channel_Name" value="<%=channel_name %>" size="50" maxlength="100" /></td>
        </tr>
        
        <tr>
          <td class="cell_title">频道描述</td>
          <td class="cell_content"><textarea name="AD_Position_Channel_Note" id="AD_Position_Channel_Note" cols="50" rows="5"><%=channel_note %></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="AD_Position_Channel_ID" name="AD_Position_Channel_ID" value="<%=channel_id %>" />
            <input type="hidden" id="action" name="action" value="renew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存频道" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='ad_position_channel.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
