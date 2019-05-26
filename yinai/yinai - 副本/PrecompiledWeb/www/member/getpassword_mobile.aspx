<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Glaer.Trade.Util.Tools.ITools tools = Glaer.Trade.Util.Tools.ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    string member_mobile = tools.CheckStr(Request["member_mobile"]);
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="找回密码 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>


    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>

    <script type="text/javascript" src="/scripts/member.js"></script>
    <%-- <script src="/scripts/hdtab.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        //window.onload = function () {
        //    var SDmodel = new scrollDoor();
        //    SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
        //    SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        //}
    </script>
    <!--滑动门 结束-->
    <%-- <script src="/scripts/1.js" type="text/javascript"></script>--%>
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



        //获取短信验证码
        function get_sms_checkcode() {
            $.ajaxSetup({ async: false });
            //var chkmobile = alert($("#member_mobile").val());
            var verifycode = $("#verifycode").val();

            if (verifycode != "") {
                if (member_mobile) {
                    $.get(encodeURI("/member/register_do.aspx?action=smscheckcode&phone=" + $("#member_mobile").val() + "&verifycode=" + $("#verifycode").val() + "&t=" + Math.random()), function (data) {

                        if (data["result"] == "true") {

                            //$("#member_mobile_tip").html('校验码已发出，请注意查收短信。');
                            $(".a03").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#ff6600", "color": "#FFF", "border": "1px solid #dddddd" });

                            var maxSecond = 119;
                            var timer = setInterval(function () {
                                if (maxSecond <= 0) {
                                    clearInterval(timer);

                                    //$("#member_mobile_tip").empty();

                                    $(".a03").bind("click", function () {
                                        get_sms_checkcode();
                                    }).html("获取短信").removeAttr("style");
                                }
                                else {
                                    $(".a03").html(maxSecond + "秒后重新获取");
                                }
                                maxSecond--;
                            }, 1000);
                        }
                        else {
                            alert(data["msg"]);
                        }
                    }, "json");
                }
            }
            else {
                alert("请输入图形验证码！");
            }
        }



        //检查短信效验码
        function check_sms_checkcode(object, sign) {
            //alert(object);
            $("#" + object + "_tip").load("/member/register_do.aspx?action=check_sms_checkcode&val=" + $("#" + object).val() + "&sign=" + $("#" + sign).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }



        //示范一个公告层
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
                //, content: $("/Bid/SignUpPopup.aspx")
   , content: ("/Bid/SignUpPopup.aspx")
            });
        }

    </script>
    <!--弹出菜单 end-->


    <style type="text/css">
        .t14 {
            font-size: 14px;
        }

        .table_help {
            border: 1px solid #e7e7e7;
            padding-top: 0px;
            padding-right: 10px;
            padding-left: 10px;
        }

            .table_help td {
                padding: 5px;
            }

            .table_help input {
                padding: 3px;
                line-height: 20px;
            }

                .table_help input.buttonSkinB {
                    border: none;
                    _border: 0px;
                    padding: 0px 20px;
                    cursor: pointer;
                    height: 40px;
                    background-color: #ff6600;
                    font-size: 18px;
                    font-weight: normal;
                    line-height: 40px;
                    border-radius: 2px;
                    color: #FFF;
                }

                    .table_help input.buttonSkinB:hover {
                        background-color: #ff6600;
                        color: #FFF;
                        text-decoration: none;
                    }

        a.a03 {
            background-color: #ff6600;
            padding: 10px 13px;
            color: #fff;
            border-radius: 2px;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position">当前页面 > <a href="/index.aspx">首页</a> ><strong><a href="getpassword.aspx">找回密码</a></strong> </div>
            <!--位置说明 结束-->
            <div class="partd" style="margin-top: 0px;">

                <form name="form_getpass" action="login_do.aspx" method="post">
                    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="table_help">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td class="t14" height="50" width="100" style="text-align: right;">手机号码：
                                
                                  
                                        </td>
                                        <td>
                                            <%=member_mobile %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t14" height="50" width="100" style="text-align: right;">验证码：

                                        </td>
                                        <td>
                                            <label>
                                                <input type="text" name="verifycode" id="verifycode" onblur="check_member_verifycode('verifycode');"
                                                    style="width: 195px; height: 24px;" /><a
                                                        href="javascript:void(0);" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img
                                                            src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a><i>*</i><b style="margin-left: 10px; margin-right: 10px; display: inline; width: auto !important; margin-top: 6px;"><img src="/Public/verifycode.aspx" width="65" height="32" id="var_img" style="display: inline; vertical-align: middle" /></b><strong
                                                                class="regtip" id="verifycode_tip"></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t14" height="50" width="100" style="text-align: right;"><span>短信验证码：</span></td>
                                        <td>
                                            <label>
                                                <input name="mobile_verifycode" id="mobile_verifycode" type="text" placeholder="短信验证码" style="width: 168px; height: 24px;" onblur="check_sms_checkcode('mobile_verifycode', 'member_mobile');" autocomplete="off" /><a id="mobile_verifycode_tip" onclick="get_sms_checkcode()" class="a03">获取短信验证码</a><span id="btn_sms_checkcode"></span></label>
                                        </td>
                                    </tr>


                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="60" align="center">
                                <input name="btnsubmit" type="submit" class="buttonSkinB" id="btnsubmit" value="下一步" />
                                <input name="action" type="hidden" id="action" value="MobilevalidateIsTrue" />
                                <input name="member_mobile" type="hidden" id="member_mobile" value="<%=member_mobile %>" />

                            </td>
                        </tr>
                    </table>
                </form>

            </div>
        </div>
    </div>
    <!-- 右侧弹框 开始-->
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
    <!-- 右侧弹框 结束-->
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
