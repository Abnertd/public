<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private AD Ad;
    private ITools tools;

    private int ad_id, ad_mediakind, ad_freq, ad_iscontain, ad_sort, ad_isactive, ad_channel, U_Ad_Audit;
    private string ad_title, ad_kind,ad_media,ad_mediacode,ad_link,ad_propertys,ad_site,ad_start, ad_end,position_name;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("bbefe763-2057-4d30-8af3-e19cdd484e00");
        
        Ad = new AD();
        tools = ToolsFactory.CreateTools();
        position_name = "";
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

            ADPositionInfo position = Ad.GetAD_PositionByValue(ad_kind);
            if (position != null)
            {
                position_name = position.Ad_Position_Name;
            }
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
          <td class="cell_content"><%=ad_title %></td>
        </tr>
       <tr>
          <td class="cell_title">广告位置</td>
          <td class="cell_content"><%=position_name%></td>
        </tr>
        <tr>
          <td class="cell_title">广告类型</td>
          <td class="cell_content">
          <%
              if (ad_mediakind == 1)
              {
                  Response.Write("文字");
              }
              else if (ad_mediakind == 2)
              {
                  Response.Write("图片");
              }
              else if (ad_mediakind == 3)
              {
                  Response.Write("Flash");
              }
              else if (ad_mediakind == 4)
              {
                  Response.Write("富媒体");
              }
               %>
          </td>
        </tr>


        <tr id="tr_Ad_Media" <%if (ad_mediakind == 2){Response.Write("style=\"display:none;\"");}%>>
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<%=Application["upload_server_url"]+ad_media %>" id="img_Ad_Media" /></td>
        </tr>
        <tr>
          <td class="cell_title">媒体代码</td>
          <td class="cell_content"><%=ad_mediacode %></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><%=ad_link %></td>
        </tr>
        <tr>
          <td class="cell_title">广告频率</td>
          <td class="cell_content"><%=ad_freq %></td>
        </tr>
        <tr>
          <td class="cell_title">广告权重</td>
          <td class="cell_content"><%=ad_sort %>
          
          </td>
        </tr>
        <tr>
          <td class="cell_title">起止时间</td>
          <td class="cell_content"><%=ad_start+" - "+ad_end%>
          </td>
        </tr>
        <tr>
          <td class="cell_title">启用该广告</td>
          <td class="cell_content">
          <%
              if (ad_isactive == 0)
              {
                  Response.Write("否");
              }
              else
              {
                  Response.Write("是");
              }
               %>
          </td>
        </tr>
        
        
        <tr>
          <td class="cell_title">特定参数值选项</td>
          <td class="cell_content">
          <%
              if (ad_iscontain == 0)
              {
                  Response.Write("排除");
              }
              else
              {
                  Response.Write("包含");
              }
               %>
         </td>
        </tr>
        <tr>
          <td class="cell_title">特定参数值</td>
          <td class="cell_content"><%=ad_propertys %></td>
        </tr>
      </table>
        
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
