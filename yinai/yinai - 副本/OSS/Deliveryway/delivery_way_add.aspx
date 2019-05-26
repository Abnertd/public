<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8632585c-0447-4003-a97d-48cade998a05");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
<script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">配送方式添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="delivery_way_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">配送方式名称</td>
          <td class="cell_content"><input name="Delivery_Way_Name" type="text" id="Delivery_Way_Name" size="50" maxlength="50" /></td>
        </tr>
        
        <tr>
          <td class="cell_title">费用方式</td>
          <td class="cell_content"><input type="radio" name="delivery_way_feetype" value="0" onclick="if(this.checked){samefee.style.display='';weightfee.style.display='none';weight.style.display='none';}" checked />统一计费 &nbsp; &nbsp; <input type="radio" name="delivery_way_feetype" value="1"  onclick="if(this.checked){samefee.style.display='none';weightfee.style.display='';weight.style.display='';}"/>按重量计费</td>
        </tr>
        <tr id="samefee" name="samefee">
          <td class="cell_title">费用</td>
          <td class="cell_content"><input name="delivery_way_fee" type="text" id="Text1" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0" /></td>
        </tr>
        <tr id="weight" style="display:none;">
          <td class="cell_title">重量</td>
          <td class="cell_content">
          首重重量 
            <select name="Delivery_Way_InitialWeight">
              <option value="500">500克</option>
              <option value="1000">1公斤</option>
              <option value="1200">1.2公斤</option>
              <option value="2000">2公斤</option>
              <option value="5000">5公斤</option>
              <option value="10000">10公斤</option>
              <option value="20000">20公斤</option>
              <option value="50000">50公斤</option>
            </select>
            续费重量 
            <select name="Delivery_Way_UpWeight">
                <option value="500">500克</option>
                <option value="1000">1公斤</option>
                <option value="1200">1.2公斤</option>
                <option value="2000">2公斤</option>
                <option value="5000">5公斤</option>
                <option value="10000">10公斤</option>
                <option value="20000">20公斤</option>
                <option value="50000">50公斤</option>
            </select>
          </td>
        </tr>
        <tr id="weightfee" style="display:none;">
          <td class="cell_title">费用</td>
          <td class="cell_content">首重费用 <input name="Delivery_Way_InitialFee" type="text" id="Delivery_Way_InitialFee" size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0" /> &nbsp; &nbsp; 续重费用 <input name="Delivery_Way_UpFee" type="text" id="Delivery_Way_UpFee"  size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0"/></td>
        </tr>
        <tr>
          <td class="cell_title">图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=preview&formname=formadd&frmelement=Delivery_Way_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Delivery_Way_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Delivery_Way_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Delivery_Way_Sort" type="text" id="Delivery_Way_Sort" size="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="10" value="1" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Delivery_Way_Status" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Delivery_Way_Status" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">支持货到付款</td>
          <td class="cell_content"><input name="Delivery_Way_Cod" type="radio" value="1"/>是 <input type="radio" name="Delivery_Way_Cod" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">查询网址</td>
          <td class="cell_content"><input name="Delivery_Way_Url" type="text" id="Delivery_Way_Url" size="50" maxlength="300" value="" /> 物流信息查询网址中物流单号请以 {delivery_sn} 代替</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">配送方式说明</td>
          <td class="cell_content">
            <textarea cols="80" id="Delivery_Way_Intro" name="Delivery_Way_Intro" rows="16"></textarea>
            <script type="text/javascript">
                var Delivery_Way_IntroEditor;
                KindEditor.ready(function (K) {
                    Delivery_Way_IntroEditor = K.create('#Delivery_Way_Intro', {
                        width: '100%',
                        height: '500px',
                        filterMode: false,
                        afterBlur: function () { this.sync(); }
                    });
                });
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="Delivery_Way_Img" type="hidden" id="Delivery_Way_Img" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='delivery_way_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>