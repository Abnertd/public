<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% Public.CheckLogin("876d73fe-0893-41e7-b44b-062713a6b190");
   AD Ad = new AD();
    %>

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
      <td class="content_title">添加广告</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/ad/ad_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">广告名称</td>
          <td class="cell_content"><input name="Ad_Title" type="text" id="Ad_Title" size="50" maxlength="50" /></td>
        </tr>
       <tr>
          <td class="cell_title">广告位置</td>
          <td class="cell_content"><%=Ad.AD_Position_Select(0) %>&nbsp;<span id="adposition_position"></span></td>
        </tr>
        <tr>
          <td class="cell_title">广告类型</td>
          <td class="cell_content"><input name="Ad_MediaKind" type="radio" id="Ad_MediaKind1" value="1" checked /> 文字 <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind2" value="2" /> 图片 <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind3" value="3" /> Flash <input name="Ad_MediaKind" type="radio" id="Ad_MediaKind4" value="4" /> 富媒体</td>
        </tr>
        <tr>
          <td class="cell_title">媒体文件</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=AD&formname=formadd&frmelement=Ad_Media&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Ad_Media" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Ad_Media" width="600" height="78" /></td>
        </tr>
        <tr>
          <td class="cell_title">媒体代码</td>
          <td class="cell_content"><textarea name="Ad_Mediacode" cols="50" rows="5" /></textarea></td>
        </tr>
        <tr>
          <td class="cell_title">链接地址</td>
          <td class="cell_content"><input name="Ad_Link" type="text" id="Ad_Link" value="http://" size="50" maxlength="255" /></td>
        </tr>
        <tr>
          <td class="cell_title">广告频率</td>
          <td class="cell_content"><input name="Ad_Show_Freq" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Ad_Show_Freq" size="50" value="1" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">广告权重</td>
          <td class="cell_content"><input name="Ad_Sort" value="1" type="text" id="Ad_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" size="50" maxlength="100" />
          <span class="tip">数字越小越靠前</span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">起止时间</td>
          <td class="cell_content"><input type="text" class="input_calendar" name="Ad_StartDate" id="Ad_StartDate" maxlength="10" readonly="readonly" /> - <input type="text" class="input_calendar" name="Ad_EndDate" id="Ad_EndDate" maxlength="10" readonly="readonly" />
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
          <td class="cell_content"><input name="Ad_IsActive" type="radio" id="Ad_IsActive1" value="1" checked="checked"/>是<input type="radio" name="Ad_IsActive" id="Ad_IsActive2" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否审核广告</td>
          <td class="cell_content"><input name="U_Ad_Audit" type="radio" id="U_Ad_Audit1" value="1" checked="checked"/>是<input type="radio" name="U_Ad_Audit" id="U_Ad_Audit0" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">特定参数值选项</td>
          <td class="cell_content"><input name="Ad_IsContain" type="radio" id="Ad_IsContain1" value="1" checked /> 包含 <input name="Ad_IsContain" type="radio" id="Ad_IsContain2" value="0" /> 排除</td>
        </tr>
        <tr>
          <td class="cell_title">特定参数值</td>
          <td class="cell_content"><textarea name="Ad_Propertys" cols="50" rows="5" /></textarea> <span class="tip">参数间以"|"分隔</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="working" name="action" value="new" />
            <input type="hidden" id="Ad_Media" name="Ad_Media" />
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
