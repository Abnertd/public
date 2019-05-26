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
    member.Member_Login_Check("/member/feedback.aspx");

    MemberInfo memberinfo = member.GetMemberByID();

    if (member == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    MemberProfileInfo profile = memberinfo.MemberProfileInfo;
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="售后服务 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
    <%-- <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>

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


    <script type="text/javascript">
        //检查售后服务用户名
        function check_feed_name(object) //1
        {
            $.ajaxSetup({ async: false });
            $("#" + object + "_tip").load("/member/account_do.aspx?action=feedback&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                $("#" + object + "_div").attr('class', 'login_txt_focus2');
                return false;
            }
            else {
                $("#" + object + "_div").attr('class', 'login_txt2');
                return true;
            }
        }

        //检查邮箱
        function check_member_email(object) {
            $.ajaxSetup({ async: false });
            $("#" + object + "_tip").load("/member/register_do.aspx?action=checkemail&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                $("#" + object + "_div").attr('class', 'login_txt_focus2');
                return false;
            }
            else {
                $("#" + object + "_div").attr('class', 'login_txt2');
                return true;
            }
        }

        //检查电话号码
        function check_feedback_phone(object) {
            //alert($("#Feedback_Tel").val());
            $.ajaxSetup({ async: false });
            $("#" + object + "_tip").load("/member/register_do.aspx?action=checkfeedbackphone&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                $("#" + object + "_div").attr('class', 'login_txt_focus2');
                return false;
            }
            else {
                $("#" + object + "_div").attr('class', 'login_txt2');
            }
        }
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
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>售后服务</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(5, 3) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>售后服务</h2>
                        <div class="blk17_sz">
                            <form name="formadd" id="formadd" method="post" action="/member/feedback_do.aspx">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="92" class="name">类型
                                        </td>
                                        <td width="801">

                                            <input name="Feedback_Type" type="hidden" id="Text1" style="width: 300px; background: none !important" class="input01" maxlength="50" value="1" />
                                            网站留言
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">姓名
                                        </td>
                                        <td width="801">
                                            <input name="Feedback_Name" type="text" id="Feedback_Name" style="width: 300px; background: none !important" class="input01" maxlength="50" onblur="check_feed_name('Feedback_Name')" value="<%=profile.Member_Name %>" />
                                            <strong class="regtip" id="Feedback_Name_tip" style="font-weight: 500"></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">E-mail
                                        </td>
                                        <td width="801">
                                            <input name="Feedback_Email" type="text" id="Feedback_Email" style="width: 300px; background: none !important" class="input01" maxlength="100" onblur="check_member_email('Feedback_Email')" value="<%=memberinfo.Member_Email %>" />
                                            <strong class="regtip" id="Feedback_Email_tip" style="font-weight: 500"></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">您的手机
                                        </td>
                                        <td width="801">
                                            <input name="Feedback_Tel" type="text" id="Feedback_Tel" maxlength="50" style="width: 300px; background: none !important" class="input01" onblur="check_feedback_phone('Feedback_Tel')" value="<%=profile.Member_Phone_Number%>" />
                                            <strong class="regtip" id="Feedback_Tel_tip" style="font-weight: 500"></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">内容</td>
                                        <td>
                                            <textarea name="feedback_content" cols="50" class="required" rows="5" id="feedback_content"></textarea><i>*</i></td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name">验证码
                                        </td>
                                        <td width="801">
                                            <input name="verifycode" type="text" id="Text4" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());" class="required" size="10" maxlength="10" /><i>*</i><img title="看不清？换一张" alt="看不清？换一张" src="/public/verifycode.aspx" style="cursor: pointer; display: inline;" id="var_img" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());" align="absmiddle" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="92" class="name"></td>
                                        <td width="801">
                                            <input name="action" type="hidden" id="action" value="add">
                                            <a href="javascript:void(0);" onclick="$('#formadd').submit();" class="a11"></a></td>
                                    </tr>
                                </table>


                            </form>
                        </div>

                        <div class="b14_1_main" style="margin-top: 15px;">
                            <%member.Feedback_List(); %>
                        </div>
                    </div>

                </div>
                <div class="clear"></div>
            </div>


        </div>
        <!--主体 结束-->
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
        <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
