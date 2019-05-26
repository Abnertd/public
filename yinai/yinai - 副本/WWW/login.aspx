<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />
<% 
    Public_Class pub = new Public_Class();
    AD ad = new AD();
    Member member = new Member();
    ITools tools = ToolsFactory.CreateTools();
    Session["Home_Position"] = Session["Cur_Position"] = "";
    Session["logintype"] = "False";
    int type = tools.CheckInt(Request["u_type"]);
    string username = "";
    if (Request.Cookies["Supplier_Email"] != null)
    {
        username = Request.Cookies["Supplier_Email"].Value;
    }
    else
    {

    }

    if (tools.NullStr(Session["member_logined"]) == "True")
    {
        Response.Redirect("/member/index.aspx");
    }
    else if (tools.NullStr(Session["supplier_logined"]) == "True")
    {
        Response.Redirect("/supplier/index.aspx");
    }
    string umsg = "";
    string pmsg = "";
    string vmsg = "";
    string login = tools.CheckStr(Request["login"]);
    if (login == "vmsg")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">验证码错误&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "umsg_k")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">请输入您的用户名&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "pmsg")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">密码错误&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "umsg_w")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">账号未注册&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "err_check")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">注册信息审核中&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "err_active")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">账号信息登录失败&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "err_active1")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">账号密码错误&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "umsg_sms")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">短信效验码错误&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "err_active_sub")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">子账户已禁用&nbsp;&nbsp;</td></tr></table>";
    }
    else if (login == "err_time_sub")
    {
        pmsg = "<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img src='/images/tip-error.gif' hspace='5' align='absmiddle' style=\"width:11px; height:11px;display:inline;border:0;\">子账户时间过期&nbsp;&nbsp;</td></tr></table>";
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="登录 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
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
    <script src="js/1.js"></script>
    <script type="text/javascript" src="/js/jquery_002.js"></script>
    <script type="text/javascript" src="/js/javascript.js"></script>
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
                            <h2>欢迎进入<span>易耐网</span></h2>
                            <form id="loginform" action="/member/login_do.aspx" method="post" onkeydown="if(event.keyCode==13){document.loginform.submit();}">
                                <ul>
                                  
                                    <li class="li05">
                                        <input name="Member_UserName" id="Member_UserName" type="text"  placeholder="昵称/手机号/邮箱" /></li>
                                    <li class="li06">
                                        <input name="Member_Password" id="Member_Password" type="password"  placeholder="请输入6～20位密码" /></li>
                                   <li class="li07">
                                        <span style="float: left;">
                                            <input type="text" name="verifycode" id="verifycode"
                                                style="width: 195px;" placeholder="请输入验证码" /></span><span style="float: right;padding-top: 5px;"><b style="margin-left: 10px; margin-right: 10px;"><img src="/Public/verifycode.aspx" width="65" height="32" id="supplier_verify_img" style="display: inline; vertical-align: middle" /></b><a
                                                    href="javascript:void(0);" onclick="$('#supplier_verify_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a></span>    <div id="verifycode_tip"></div>
                                    </li>
                                       <%=pmsg %>
                                  <%--  <li style="height: 20px; margin-top: 15px;"><a href="/member/getpassword.aspx" target="_blank" class="a01">忘记密码？</a><input name="autologin" id="autologin" type="checkbox" value="1" />30天免登录</li>--%>
                                      <li style="height: 20px; margin-top: 15px;"><a href="/member/getpassword.aspx" target="_blank" class="a01">忘记密码？</a><input name="autologin" id="autologin" type="checkbox" value="1" />30天免登录</li>
                                    <%-- <li style="height: 20px; margin-top: 15px;"><a href="/member/getpassword.aspx" target="_blank" class="a01">忘记密码？</a></li>--%>
                                   
                                    <li><a href="javascript:;" id="member_btn" onclick="$('#loginform').submit();" class="login">登 录</a></li>
                                    <input type="hidden" name="action" value="login" />
                                    <li><a href="/register.aspx" target="_blank" class="login2">注 册</a></li>
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
