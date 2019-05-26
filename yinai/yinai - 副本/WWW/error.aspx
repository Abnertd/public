<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<%    
    Public_Class pub = new Public_Class();
   
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%="有货有信贷 - " + pub.SEO_TITLE()%></title>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
  <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="50"></td>
        </tr>
        <tr>
            <td align="center">
                <img src="/images/error_icon.png"></td>
        </tr>
        <tr>
            <td align="center">
                <h1>哎呀…您访问的页面出错了</h1>
            </td>
        </tr>
        <tr>
            <td align="center">系统在处理您的页面请求时发生了错误，请稍后重试</td>
        </tr>
        <tr>
            <td align="center"><a href="/index.aspx" style="font-size:18px;">返回网站首页</a></td>
        </tr>
        <tr>
            <td height="50"></td>
        </tr>
    </table>

  <ucbottom:bottom runat="server" ID="Bottom" />   
</body>
</html>
