<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();


    string pmsg = "";

    string login = tools.CheckStr(Request["login"]);
    if (login == "vmsg")
    {
        pmsg = "验证码错误";
    }
    else if (login == "umsg_k")
    {
        pmsg = "请输入您的登录名";
    }
    else if (login == "pmsg")
    {
        pmsg = "密码错误";
    }
    else if (login == "smsg")
    {
        pmsg = "账号已冻结";
    }
    else if (login == "err_active")
    {
        pmsg = "物流商登录失败";
    }
    else
    {
        pmsg = "为了您的账号安全，请勿在公共场所自动登录";
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="物流商登录 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>

    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>

    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["f01", "f02"], ["ff01", "ff02"], "on", " ");
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $(document).keydown(function (event) {
                if (event.keyCode == 13) {
                    if ($("#ff01").css("display") == "block") {
                        $("#member_btn").click();
                    } else {
                        $("#supplier_btn").click();
                    }
                }
            });
        });
    </script>

    <!--滑动门 结束-->
    <script src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/jquery_002.js"></script>
    <script type="text/javascript" src="/scripts/javascript.js"></script>
    <!--弹出菜单 start-->
    <script>
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>
    <!--弹出菜单 end-->
    <style type="text/css">
        .banner04_info_main a.a38 {
            width: 102px;
            background-color: #f5f5f5;
            display: inline-block;
            vertical-align: middle;
            margin-left: 10px;
            border: 1px solid #dddddd;
            font-size: 12px;
            text-align: center;
            line-height: 38px;
            color: #666;
        }

            .banner04_info_main a.a38:hover {
                background-color: #e1000c;
                color: #FFF;
                border: 1px solid #e1000c;
                text-decoration: none;
            }

              .e_b21_info01{height: 26px;
  background-color: #fcf9d5;
  border: 1px solid #f5ed87;
  font-size: 12px;
  font-weight: bold;
  color: #ffb400;
  line-height: 26px;
  padding: 0 0 0 34px;
  background-repeat: no-repeat;
  background-position: 8px 4px;
  margin-bottom: 10px;}
    </style>

</head>

<body>
    <!--主体 开始-->
    <uctop:top ID="top" runat="server" />
    <!--主体部分开始*********************************************************************************************************************-->
    <div class="logo_wrap_fff">
        <div>
            <div>
                <div class="banner04">
                    <div class="banner04_main"></div>
                    <div class="b04_info">
                        <div class="b04_info_login">
                            <h2><span>物流商登录</span></h2>
                            <div class="e_b21_info01" id="td_msg"><%=pmsg %></div>
                            <form id="loginform" action="/Logistics/Logistics_do.aspx" method="post">
                                <ul>
                                    <li class="li05">
                                        <input name="Logistics_NickName" id="Logistics_NickName" type="text" style="width: 356px;" placeholder="登录名" /></li>
                                    <li class="li06">
                                        <input name="Logistics_Password" id="Logistics_Password" type="password" style="width: 356px;" placeholder="请输入6～20位密码" /></li>
                                    <li class="li07">
                                        <span style="float: left;">
                                            <input type="text" name="verifycode" id="verifycode"
                                                style="width: 250px;" placeholder="请输入验证码" /></span><span style="float: right;"><b style="margin-left: 10px; margin-right: 10px;"><img src="/Public/verifycode.aspx" width="65" height="32" id="supplier_verify_img" style="display: inline; vertical-align: middle" /></b><a
                                                    href="javascript:void(0);" onclick="$('#supplier_verify_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img
                                                        src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a></span>
                                    </li>
                                    <li><a href="javascript:;" id="member_btn" onclick="$('#loginform').submit();" class="login">登 录</a></li>
                                    <input type="hidden" name="action" value="login" />
                                </ul>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--主体部分结束*********************************************************************************************************************-->
    <!--主体 结束-->



    <!--尾部 开始-->
    <ucbottom:bottom ID="bottom" runat="server" />
    <!--尾部 结束-->

</body>
</html>
