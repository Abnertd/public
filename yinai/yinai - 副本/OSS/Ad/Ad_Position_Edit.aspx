<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private AD Ad;
    private ITools tools;

    private int position_id, position_channelid, position_isactive, position_width, position_height, U_Ad_Position_Marketing;
    private string position_name, position_value,position_site;
    double U_Ad_Position_Price;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("afbc3245-62b5-4eb3-aefb-c6c8f3e2b02d");
        
        Ad = new AD();
        tools = ToolsFactory.CreateTools();

        position_id = tools.CheckInt(Request.QueryString["position_id"]);
        if (position_id == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ADPositionInfo entity = Ad.GetAD_PositionByID(position_id);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            position_id = entity.Ad_Position_ID;
            position_name = entity.Ad_Position_Name  ;
            position_value  = entity.Ad_Position_Value  ;
            position_channelid  = entity.Ad_Position_ChannelID ;
            position_width = entity.Ad_Position_Width;
            position_height = entity.Ad_Position_Height;
            position_isactive = entity.Ad_Position_IsActive;
            position_site = entity.Ad_Position_Site;
            U_Ad_Position_Marketing = entity.U_Ad_Position_Marketing;
            U_Ad_Position_Price = entity.U_Ad_Position_Price;
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
      <td class="content_title">修改广告位置</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/ad/ad_position_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">位置名称</td>
          <td class="cell_content"><input name="Ad_Position_Name" value="<%=position_name %>" type="text" id="Ad_Position_Name" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">位置代号</td>
          <td class="cell_content"><input name="Ad_Position_Value"  value="<%=position_value %>" type="text" id="Ad_Position_Value" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">所属频道</td>
          <td class="cell_content"><%Ad.Select_Position_Channel("Ad_Position_ChannelID", position_channelid);%></td>
        </tr>
        <tr>
          <td class="cell_title">启用该位置</td>
          <td class="cell_content"><input name="Ad_Position_IsActive" type="radio" id="Ad_Position_IsActive1" value="1" <% =Public.CheckedRadio(position_isactive.ToString(), "1")%>/>是<input type="radio" name="Ad_Position_IsActive" id="Ad_Position_IsActive2" value="0" <% =Public.CheckedRadio(position_isactive.ToString(), "0")%>/>否 </td>
        </tr>
        <%--<tr>
          <td class="cell_title">是否推广</td>
          <td class="cell_content"><input name="U_Ad_Position_Marketing" type="radio" id="U_Ad_Position_Marketing1" value="1" <% =Public.CheckedRadio(U_Ad_Position_Marketing.ToString(), "1")%>/>是<input type="radio" name="U_Ad_Position_Marketing" id="U_Ad_Position_Marketing0" value="0" <% =Public.CheckedRadio(U_Ad_Position_Marketing.ToString(), "0")%> />否</td>
        </tr>
        <tr>
          <td class="cell_title">推广价格</td>
          <td class="cell_content"><input name="U_Ad_Position_Price" size="10" type="text" id="U_Ad_Position_Price" maxlength="10" value="<%=U_Ad_Position_Price %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />元/天</td>
        </tr>--%>
        <tr>
          <td class="cell_title">位置宽度</td>
          <td class="cell_content"><input name="Ad_Position_Width" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=position_width %>" type="text" id="Ad_Position_Width" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">位置高度</td>
          <td class="cell_content"><input name="Ad_Position_Height" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=position_height %>" type="text" id="Ad_Position_Height" size="50" maxlength="100" /></td>
        </tr>
        
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="Ad_Position_ID" name="Ad_Position_ID" value="<%=position_id %>" />
            <input type="hidden" id="working" name="action" value="renew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存位置" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='ad_position.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
