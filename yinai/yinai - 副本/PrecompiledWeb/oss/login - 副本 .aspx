<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%
    String msg;
    switch (Request.QueryString["tip"])
    {
        case "ErrorVerifyCode":
            msg = "您输入的验证码不正确，请重新输入";
            break;
        case "ErrorInfo":
            msg = "用户名和密码不正确";
            break;
        case "nologin":
            msg = "登陆超时或未登录!";
            break;
        default:
            msg = "请保护好您的密码并定期更改密码";
            break;
    }

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Trade OSS 电子商务平台运营支撑系统</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<style type="text/css">
	body{background: url(/images/top_bg.jpg) no-repeat top;}
	.login_vline{
		width:20px;
		text-align:center;
	}
	.login_bg{
		background:url(images/login_bg.gif) no-repeat top;
		width:341px;
	}
	.login_text{
		font:14px "黑体";
		color:#2c65ac;
		width:46px;
		height:30px;
		text-align:right;
		padding-right:6px;
	}
	.input{
		width:160px;
		border:1px #6897cf solid;
		padding:2px;
	}
</style>
</head>
<body>
<div><div>
<table border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:160px;">
  <tr>
    <td width="341" valign="middle"><img src="/images/Logo_Login.jpg" alt="Trade OSS 电子商务平台运营支撑系统" width="300" height="160" /></td>
    <td valign="middle" class="login_vline"><img src="/images/login_vline.gif" width="1" height="260" /></td>
    <td valign="top" class="login_bg">
    <form id="frmlogin" name="frmlogin" method="post" action="/logindo.aspx">
      <table border="0" cellspacing="0" cellpadding="0" style="margin:30px auto auto 30px; width:220px;">
        <tr>
          <td class="login_text">用户名</td>
          <td><input name="username" type="text" class="input" id="username" maxlength="50" value="<% =Server.UrlDecode(Request.Cookies["username"].Value)%>" /></td>
        </tr>
        <tr>
          <td class="login_text">密码</td>
          <td><input name="password" type="password" class="input" id="password" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="login_text">验证码</td>
          <td><input name="verifycode" type="text" class="input" style="width:60px;" id="verifycode" maxlength="6" />&nbsp;<img src="/public/verifycode.aspx" align="absmiddle"></td>
        </tr>
        <tr> 
          <td></td>
          <td height="6"></td>
        </tr>
        <tr>
          <td></td>
          <td><input name="userremember" type="checkbox" id="userremember" value="1" checked="checked" /> <span class="t12">记住用户名</span></td>
        </tr>
        <tr>
          <td></td>
          <td height="10"></td>
        </tr>
        <tr>
          <td><input type="hidden" name="action" value="login" /></td>
          <td><input type="image" name="imageField" id="imageField" src="/images/loginbtn.gif" /></td>
        </tr>
        <tr>
          <td></td>
          <td height="10"></td>
        </tr>
      </table>
      <div style="margin-left:30px; width:220px;"><img src="images/tip-info.png" width="13" height="13" align="absmiddle"/>&nbsp;<span class="tip"><% =msg %></span></div>
      </form>
    </td>
  </tr>
</table>
</body>
</html>
