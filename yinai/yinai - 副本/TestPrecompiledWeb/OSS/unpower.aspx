<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">没有权限</td>
    </tr>
    <tr>
      <td class="content_content">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="100" height="60">&nbsp;</td>
            <td width="80"><img src="/images/icon_alert_b.gif" width="50" height="50" /></td>
            <td style="font:bold 14px '微软雅黑'; color:#f00;">对不起，您没有权限！<%=Request["tip"].Replace("没有权限，","") %></td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</div>
</body>
</html>