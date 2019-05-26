<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private PayWay myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("7950ffdc-827d-4432-a177-eb1d96ad4c3e");
        myApp = new PayWay();
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
      <td class="content_title">支付方式添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="pay_way_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">支付类型</td>
          <td class="cell_content">
          <select name="Pay_Way_Type" id="Pay_Way_Type" onchange="this.form.Pay_Way_Name.value=this.options[this.selectedIndex].text;"">
            <option value="0">线下支付</option>
            <% =myApp.SysPayOption(0)%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">支付方式名称</td>
          <td class="cell_content"><input name="Pay_Way_Name" type="text" id="Pay_Way_Name" size="50" maxlength="50" value="线下支付" /></td>
        </tr>
        <tr>
          <td class="cell_title">图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=preview&formname=formadd&frmelement=Pay_Way_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Pay_Way_Img" style="display:none;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="" id="img_Pay_Way_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Pay_Way_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Pay_Way_Sort" size="10" maxlength="10" value="1" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Pay_Way_Status" type="radio" value="1" checked="checked"/>是 <input type="radio" name="Pay_Way_Status" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">货到付款</td>
          <td class="cell_content"><input name="Pay_Way_Cod" type="radio" value="1"/>是 <input type="radio" name="Pay_Way_Cod" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">支付方式说明</td>
          <td class="cell_content">
            <textarea cols="80" id="Pay_Way_Intro" name="Pay_Way_Intro" rows="16"></textarea>
            <script type="text/javascript">
                var Pay_Way_IntroEditor;
                KindEditor.ready(function (K) {
                    Pay_Way_IntroEditor = K.create('#Pay_Way_Intro', {
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
            <input name="Pay_Way_Img" type="hidden" id="Pay_Way_Img" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='pay_way_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>