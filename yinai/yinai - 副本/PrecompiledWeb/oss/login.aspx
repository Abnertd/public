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
      
<link href="/CSS/index.css" rel="stylesheet" type="text/css" />
<link href="/CSS/flash01.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $(document).keydown(function (event) {
                if (event.keyCode == 13) {
                    $("#btn_login").click();
                }
            });
        });
    </script>


</head>
<body>

<div class="logo"><img src="/images/logo.jpg"></div>
      <!--轮播 开始-->
      <div class="banner" >
   <div id="flash">         
     <div  style="display:block;" id="flash1"><a></a></div>
   </div>

</div>
      <!--轮播 结束-->
      <div class="content">
            <div class="blk01">
                  <div class="b01_main">
                      <form id="frmlogin" name="frmlogin" method="post" action="/logindo.aspx">
                        <ul>
                             <li class="li01"><input name="username" type="text" class="input" id="username" maxlength="50" autocomplete="off" value="" style="width:253px; padding:0 0 0 45px;"/></li>

                             <li class="li02"><input  name="password" type="password" class="input" id="password" maxlength="50" style="width:253px; padding:0 0 0 45px;"/></li>

                             <li><input  name="verifycode" type="text" class="input" id="verifycode" maxlength="6" autocomplete="off" style="width:150px; padding:0 0 0 45px;"/>
                                &nbsp;<img style="cursor:pointer;" src="/public/verifycode.aspx" align="absmiddle" id="img_verify" onclick="$('#img_verify').attr('src','/public/verifycode.aspx?timer='+Math.random());"></li>

                             <li>
                                 <input type="hidden" name="action" value="login" />

                                 <a href="javascript:;" id="btn_login" onclick="$('#frmlogin').submit();" class="a01">立即登录</a></li>

                                <li><img src="/images/tip-info.png" width="13" height="13" align="absmiddle"/>&nbsp;<span class="tip"><% =msg %></span></li>

                        </ul>
                          </form>
                  </div>
            </div>
            <div class="clear"></div>
      </div>
   
</body>
</html>
