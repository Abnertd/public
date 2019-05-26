<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
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


    <%-- <script src="/scripts/hdtab.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
    <script src="/scripts/1.js" type="text/javascript"></script>
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
                                        <td colspan="2" class="t14">
                                            <strong>忘记了密码？没关系，首先请输入您的邮箱地址或者手机号码</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t14" height="50" width="350">邮箱地址/手机号码
                                    <input name="member_email" type="text" id="member_email" size="30" maxlength="100" />
                                        </td>
                                        <td id="tip_member_email" align="left"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="60" align="center">
                                <input name="btnsubmit" type="submit" class="buttonSkinB" id="btnsubmit" value="下一步" />
                                <input name="action" type="hidden" id="action" value="getpass" />
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
