<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    object getpass_verify = Session["getpass_verify"];
  object member_mobile = Request["member_mobile"];
  if ((getpass_verify == null) || (getpass_verify == ""))
  {
      Response.Redirect("/member/getpassword.aspx");
  }
  else if (getpass_verify.ToString() != "true")
  {
      Response.Redirect("/member/getpassword.aspx");
  }



  //object IsTrue = Session["getpasswordreset_bymoile"];

  //  if (IsTrue == "True")
  //{

  //}
  //else
  //{
  //    Response.Redirect("/member/getpassword.aspx");
  //}
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="找回密码 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


    <%--     <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
     <script src="/scripts/layer/layer.js"></script>--%>

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/flash01.css" rel="stylesheet" type="text/css" />
    <link href="/css/hi_icon.css" rel="stylesheet" type="text/css" />
    <link href="../layer.m/layer.m.css" rel="stylesheet" />

    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>



    <script type="text/javascript" src="/scripts/hdtab2.js"></script>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <script src="/scripts/layer/layer.js"></script>
    <script src="/scripts/MSClass.js"></script>
    <script src="/scripts/common.js"></script>

    <script type="text/javascript" src="/scripts/hdtab.js"></script>
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
                // , closeBtn: false
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

        .t12_red {
            color: #f00;
            font-size: 12px;
            font-family: "微软雅黑";
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/index.aspx">首页</a> ><strong><a href="getpassword.aspx">找回密码</a></strong> </div>
            <!--位置说明 结束-->
            <div class="partd" style="margin-top: 0px;">

                <form name="form_getpass" action="login_do.aspx" method="post">
                    <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0" class="table_help table_padding_5">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td class="t14">
                                            <strong>重新设置密码</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                <tr>
                                                    <td width="200" align="right" class="t14">手机号码
                                                    </td>
                                                    <td class="t14_red">
                                                        <%=member_mobile%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="200" align="right" class="t14">
                                                        <span class="t12_red">*</span>新密码
                                                    </td>
                                                    <td>
                                                        <input name="member_password" type="password" id="supplier_password" size="30"
                                                            maxlength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                    <td class="tip_grey">
                                                        <div id="tip_member_password">
                                                        </div>
                                                        请使用由数字和字母共同组成的密码来增强您的帐号的安全型。请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="200" align="right" class="t14">
                                                        <span class="t12_red">*</span>确认新密码
                                                    </td>
                                                    <td>
                                                        <input name="member_password_confirm" type="password" id="supplier_password_confirm"
                                                            size="30" maxlength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                    <td class="tip">
                                                        <div id="tip_member_password_confirm">
                                                        </div>
                                                        请再次输入密码
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="200" align="right" class="t14">
                                                        <span class="t12_red">*</span>验证码
                                                    </td>
                                                    <td>
                                                        <input name="verifycode" type="text" id="verifycode" size="10" maxlength="10" />
                                                        <span><b style="margin-left: 10px; margin-right: 10px;">
                                                            <img src="/Public/verifycode.aspx" width="65" height="32" id="supplier_verify_img" style="display: inline; vertical-align: middle" /></b><a
                                                                href="javascript:void(0);" onclick="$('#supplier_verify_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="t14">&nbsp;</td>
                                                    <td><span class="tip">
                                                        <div id="tip_verifycode"></div>
                                                    </span></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="60" align="center">
                                <input name="btnsubmit" type="submit" class="buttonSkinB" id="btnsubmit" value="提交新密码" /><input
                                    name="action" type="hidden" id="action" value="resetpassbymoile" />
                            </td>
                        </tr>
                    </table>
                </form>


            </div>
        </div>
    </div>
    <div id="leftsead">
        <ul>
            <li>
                <%--<a href="http://wpa.qq.com/msgrd?v=3&uin=800022936&site=qq&menu=yes" target="_blank">--%>


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
    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
