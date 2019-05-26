<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private NoticeCate myAppC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d2658816-1905-471f-935e-c60d4620f4d7");

        myAppC = new NoticeCate();
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
      <td class="content_title">添加公告</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="notice_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">公告主题</td>
          <td class="cell_content"><input name="Notice_Title" type="text" id="Notice_Title" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">公告类别</td>
          <td class="cell_content">
          <select name="Notice_Cate" id="Notice_Cate">
            <% =myAppC.NoticeCateOption(0)%>
          </select></td>
        </tr>
         <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Notice_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">公告内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Notice_Content" name="Notice_Content" rows="16"></textarea>
            <script type="text/javascript">
                var Notice_ContentEditor;
                KindEditor.ready(function (K) {
                    Notice_ContentEditor = K.create('#Notice_Content', {
                        width: '100%',
                        height: '500px',
                        filterMode: false,
                        afterBlur: function () { this.sync(); }
                    });
                });
            </script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">热点</td>
          <td class="cell_content"><input name="Notice_IsHot" type="radio" id="Notice_IsHot1" value="1"/>是 <input type="radio" name="Notice_IsHot" id="Notice_IsHot2" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否显示</td>
          <td class="cell_content"><input name="Notice_IsAudit" type="radio" id="Notice_IsAudit1" value="1" checked="checked"/>是 <input type="radio" name="Notice_IsAudit" id="Notice_IsAudit2" value="0" />否</td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='notice_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>