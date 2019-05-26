<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private AD Ad;
    private ITools tools;

    private int ad_id, ad_mediakind, ad_freq, ad_iscontain, ad_sort, ad_isactive, ad_channel, U_Ad_Audit;
    private string ad_title, ad_kind,ad_media,ad_mediacode,ad_link,ad_propertys,ad_site,ad_start, ad_end;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c47f67fa-1142-459d-b466-e3216848ff9c");
        
        Ad = new AD();
        tools = ToolsFactory.CreateTools();

        ad_id = tools.CheckInt(Request.QueryString["ad_id"]);
        if (ad_id == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        ADInfo entity = Ad.GetADByID(ad_id);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            ad_id = entity.Ad_ID;
            ad_title = entity.Ad_Title  ;
            ad_kind   = entity.Ad_Kind  ;
            ad_mediakind  = entity.Ad_MediaKind ;
            ad_media = entity.Ad_Media;
            ad_link = entity.Ad_Link;
            ad_freq = entity.Ad_Show_Freq;
            ad_isactive = entity.Ad_IsActive;
            ad_iscontain = entity.Ad_IsContain;
            ad_propertys = entity.Ad_Propertys;
            ad_start = entity.Ad_StartDate.ToShortDateString();
            ad_end = entity.Ad_EndDate.ToShortDateString();
            ad_sort = entity.Ad_Sort;
            ad_site = entity.Ad_Site;
            if (ad_mediakind == 4)
            {
                ad_mediacode = ad_media;
                ad_media = "";
            }
            if (ad_propertys != "")
            {
                ad_propertys = ad_propertys.Substring(1, ad_propertys.Length - 2);
            }
            ad_channel = Ad.GetAdPositionByKind(ad_kind);
            U_Ad_Audit = entity.U_Ad_Audit;
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

<script type="text/javascript">
    function getChannelID(obj) {
        $("#adposition_position").load("/AD/Ad_do.aspx?action=adpositionchannel&channel_id="+obj+"&timer=" + Math.random());
    }
</script>
    
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改广告</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/ad/ad_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">广告名称</td>
          <td class="cell_content"><input name="Ad_Title" type="text" value="<%=ad_title %>" id="Ad_Title" size="50" maxlength="50" /></td>
        </tr>
       <tr>
          <td class="cell_title">广告位置</td>
          <td class="cell_content"><%=Ad.AD_Position_Select(ad_channel)%>&nbsp;<span id="adposition_position"><%Ad.Select_AD_Position1("Ad_Kind", ad_kind, ad_channel);%></span></td>
        </tr>
        <tr>
          <td class="cell_title">广告类型</td>
          <td class="cell_content"><input name="Ad_MediaKind" type="radio" id="Ad_MediaKind1" value="1" <% =Public.CheckedRadio(ad_mediakind.ToString(), "1")%> /> 文字 <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind2" value="2" <% =Public.CheckedRadio(ad_mediakind.ToString(), "2")%>/> 图片 <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind3" value="3" <% =Public.CheckedRadio(ad_mediakind.ToString(), "3")%>/> Flash <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind4" value="4" <% =Public.CheckedRadio(ad_mediakind.ToString(), "4")%>/> 富媒体</td>
        </tr>
        <tr>
          <td class="cell_title">媒体文件</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=AD&formname=formadd&frmelement=Ad_Media&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
         <tr id="tr_Ad_Media" <%if (ad_media.Length==0){Response.Write("style=\"display:none;\"");}%>>
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<%=Application["upload_server_url"]+ad_media %>" id="img_Ad_Media" width="600" height="78" /></td>
        </tr>
        <tr>
          <td class="cell_title">媒体代码</td>
          <td class="cell_content"><textarea name="Ad_Mediacode" cols="50" rows="5" /><%=ad_mediacode %></textarea></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="Ad_Link" type="text" id="Ad_Link" value="<%=ad_link %>" size="50" maxlength="255" /></td>
        </tr>
        <tr>
          <td class="cell_title">广告频率</td>
          <td class="cell_content"><input name="Ad_Show_Freq" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Ad_Show_Freq" value="<%=ad_freq %>" size="50" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">广告权重</td>
          <td class="cell_content"><input name="Ad_Sort" type="text" id="Ad_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" size="50" value="<%=ad_sort %>" maxlength="100" />
          <span class="tip">数字越小越靠前</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">起止时间</td>
          <td class="cell_content"><input type="text" class="input_calendar" name="Ad_StartDate" value="<%=ad_start %>" id="Ad_StartDate" maxlength="10" readonly="readonly" /> - <input type="text" value="<%=ad_end %>" class="input_calendar" name="Ad_EndDate" id="Ad_EndDate" maxlength="10" readonly="readonly" />
              <script type="text/javascript">
                  $(document).ready(function() {
                      $("#Ad_StartDate").datepicker({ numberOfMonths: 1 });
                      $("#Ad_EndDate").datepicker({ numberOfMonths: 1 });
                  });
              </script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">启用该广告</td>
          <td class="cell_content"><input name="Ad_IsActive" type="radio" id="Ad_IsActive1" value="1" <% =Public.CheckedRadio(ad_isactive.ToString(), "1")%>/>是<input type="radio" name="Ad_IsActive" id="Ad_IsActive2" value="0" <% =Public.CheckedRadio(ad_isactive.ToString(), "0")%>/>否</td>
        </tr>
        
        
        <tr>
          <td class="cell_title">特定参数值选项</td>
          <td class="cell_content"><input name="Ad_IsContain" type="radio" id="Ad_IsContain1" value="1" <% =Public.CheckedRadio(ad_iscontain.ToString(), "1")%> /> 包含 <input name="Ad_IsContain" type="radio" id="Ad_IsContain2" value="0" <% =Public.CheckedRadio(ad_iscontain.ToString(), "0")%>/> 排除</td>
        </tr>
        <tr>
          <td class="cell_title">特定参数值</td>
          <td class="cell_content"><textarea name="Ad_Propertys" cols="50" rows="5" /><%=ad_propertys %></textarea> <span class="tip">参数间以"|"分隔</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="renew" />
            <input type="hidden" id="ad_id" name="ad_id" value="<%=ad_id %>" />
            <input type="hidden" id="Ad_Media" name="Ad_Media" value="<%=ad_media %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存广告" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='ad.aspx';" /></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
