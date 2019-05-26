<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();

    member.Member_Login_Check("/member/account_password.aspx");

    string actionValue = "account_password";
    int account_id = tools.NullInt(Session["member_accountid"]);
    if (account_id > 0)
    {
        actionValue = "account_password_sub";
    }
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="修改密码 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <%--  <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>

    <style>
        .img_style {
            width: 100%;
            clear: both;
            height: auto;
            display: inline;
        }

            .img_style img {
                width: 11px;
                float: left;
                margin-top: 13px;
                margin-right: 11px;
            }

        .input01 {
            background-image: none;
            padding-left: 3px;
        }

        .b14_1_main table td.name {
            background-color: #ffffff !important;
            border-bottom: none !important;
            border-right: none !important;
        }
    </style>
    <!--弹出菜单 start-->
    <script type="text/javascript">
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

    <!--示范一个公告层 开始-->
    <script type="text/javascript">
        function SignUpNow() {
            layer.open({
                type: 2
   , title: false //不显示标题栏
                //, closeBtn: false
   , area: ['480px;', '340px']

   , shade: 0.8
   , id: 'LAY_layuipro' //设定一个id，防止重复弹出
   , resize: false
   , btnAlign: 'c'
   , moveType: 1 //拖拽模式，0或者1              
                , content: ("/Bid/SignUpPopup.aspx")
            });
        }
    </script>
    <!--示范一个公告层 结束-->
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>修改密码</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(5,7) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>修改密码</h2>
                        <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04">
                                    <%
                                        if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                                        {
                                            pub.Tip("positive", "您的密码已成功修改！");
                                    %>
                                    <table width="893" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="10"></td>
                                        </tr>
                                    </table>
                                    <%}%>
                                    <form name="frm_account_profile" id="frm_account_profile" method="post" action="/member/account_do.aspx">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td width="138" class="name">原密码
                                                </td>
                                                <td width="755">
                                                    <input name="Member_oldpassword" type="password" style="width: 300px;" class="input01" id="Member_oldpassword" /><span><i>*</i> 输入原密码</span>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td align="right">&nbsp;
                                                </td>
                                                <td class="tip">
                                                    <div id="tip_Member_oldpassword">
                                                    </div>
                                                    输入原密码
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="name">新密码
                                                </td>
                                                <td>
                                                    <input name="Member_password" type="password" style="width: 300px;" class="input01" id="Member_password" /><span><i>*</i> 请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）</span>
                                                </td>
                                            </tr>
                                            <%--  <tr>
                                                <td align="right">&nbsp;
                                                </td>
                                                <td class="tip">
                                                    <div id="tip_Member_password">
                                                    </div>
                                                    请使用由数字和字母共同组成的密码来增强您的帐号的安全型。请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="name">确认新密码
                                                </td>
                                                <td>
                                                    <input name="Member_password_confirm" style="width: 300px;" class="input01" type="password" id="Member_password_confirm" /><span><i>*</i>   请再次输入密码</span>
                                                </td>
                                            </tr>
                                            <%--  <tr>
                                                <td align="right">&nbsp;
                                                </td>
                                                <td class="tip">
                                                    <div id="tip_Member_password_confirm">
                                                    </div>
                                                    请再次输入密码
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="name">验证码
                                                </td>
                                                <td>
                                                    <%--<input name="verifycode" type="text" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());"--%>
                                                    <input name="verifycode" type="text"
                                                        class="txt_border" id="verifycode" /><i>*</i>
                                                    <img id="var_img" alt="看不清？换一张" title="看不清？换一张" src="/public/verifycode.aspx" onclick="this.src='../Public/verifycode.aspx?timer='+Math.random();"
                                                        style="cursor: pointer; display: inline;" align="absmiddle" />
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td align="right" class="t12_53">&nbsp;
                                                </td>
                                                <td>
                                                    <span class="tip">
                                                        <div id="tip_verifycode">
                                                        </div>
                                                    </span>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="right" class="t12_53">&nbsp;
                                                </td>
                                                <td>
                                                    <input name="action" type="hidden" id="action" value="<%=actionValue %>" />
                                                    <a href="javascript:void(0);" onclick="$('#frm_account_profile').submit();" class="a11"></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>


            </div>
        </div>
        <%--右侧浮动弹框 开始--%>
        <div id="leftsead">
            <ul>
                <li>
                    <a href="javascript:void(0);" onclick="SignUpNow();">
                        <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                            <div class="hides" id="p1">
                                <img src="/images/nav_1_1.png" />
                            </div>
                        </div>
                        <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
                <li id="tel">
                    <a href="javascript:void(0)">
                        <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                            <div class="hides" id="p2">
                                <img src="/images/nav_2_1.png">
                            </div>

                        </div>
                        <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
                <li id="btn">
                    <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                        <div class="hides" style="width: 130px; height: 50px; display: none">
                            <div class="hides" id="p3">
                                <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                            </div>
                        </div>
                        <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
                <li id="Li1">
                    <a href="#top">
                        <div class="hides" style="width: 130px; display: none" id="Div1">
                            <div class="hides" id="p4">
                                <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                            </div>
                        </div>
                        <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                    </a>
                </li>
            </ul>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#leftsead a").hover(function () {

                    $(this).children("div.hides").show();
                    $(this).children("img.shows").hide();
                    $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

                }, function () {
                    $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
                });
                $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
            });
        </script>
        <%--右侧浮动弹框 结束--%>
        <!--主体 结束-->
        <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
