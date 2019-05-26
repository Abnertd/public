<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Home_Position"] = Session["Cur_Position"] = "";
    Public_Class pub = new Public_Class();
    Addr addr = new Addr();
    Member member = new Member();
    Supplier supplier = new Supplier();
    if (Session["member_logined"] == "True")
    {
        Response.Redirect("/member/index.aspx");
    }  
%>



<html xmlns="http://www.w3.org/1999/xhtml">
<head>


    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title><%="注册 - " + pub.SEO_TITLE()%></title>

    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

  <%--  <link href="/css/index.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <!--滑动门 开始-->
    <%-- <script type="text/javascript" src="/scripts/hdtab.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02"], ["aa01", "aa02"], "on", " ");
        }
    </script>--%>
    <!--滑动门 结束-->

    <style type="text/css">
        .regtip {
            margin-top: 12px;
            font-weight: normal;
            padding-left: 5px;
            font-family: "微软雅黑";
            font-size: 12px;
            color: #999;
        }

        .regist-2013 .btnt .close-reveal-modal {
            display: block;
            cursor: pointer;
            width: 322px;
            height: 34px;
            line-height: 34px;
            background: #488c48;
            color: #FFF;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            font-family: "微软雅黑";
            font-size: 16px;
            font-weight: 800;
            text-align: center;
            text-decoration: none;
        }

        .b02_main ul li label a.a03 {
            background-color: #ff6600;
        }
    </style>
    <script src="/scripts/1.js" type="text/javascript"></script>
    <script type="/text/javascript" src="/scripts/jquery_002.js"></script>
    <script type="/text/javascript" src="/scripts/javascript.js"></script>


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


        //获取短信验证码
        function get_sms_checkcode() {
            $.ajaxSetup({ async: false });
            var chkmobile = check_member_loginmobile('member_mobile');
            var verifycode = $("#verifycode").val();

            if (verifycode != "") {
                if (chkmobile) {
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
        //function get_sms_checkcode() {
        //    $.ajaxSetup({ async: false });
        //    var chkmobile = check_member_loginmobile('member_mobile');
        //    var verifycode = $("#verifycode").val();

        //    if (verifycode != "") {
        //        if (chkmobile) {
        //            $.get(encodeURI("/member/register_do.aspx?action=smscheckcode&phone=" + $("#member_mobile").val() + "&verifycode=" + $("#verifycode").val() + "&t=" + Math.random()), function (data) {

        //                if (data["result"] == "true") {

        //                    //$("#member_mobile_tip").html('校验码已发出，请注意查收短信。');
        //                    $("#btn_sms_checkcode").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#f5f5f5", "color": "#666", "border": "1px solid #dddddd" });

        //                    var maxSecond = 119;
        //                    var timer = setInterval(function () {
        //                        if (maxSecond <= 0) {
        //                            clearInterval(timer);

        //                            //$("#member_mobile_tip").empty();

        //                            $("#btn_sms_checkcode").bind("click", function () {
        //                                get_sms_checkcode();
        //                            }).html("获取短信").removeAttr("style");
        //                        }
        //                        else {
        //                            $("#btn_sms_checkcode").html(maxSecond + "秒后重新获取");
        //                        }
        //                        maxSecond--;
        //                    }, 1000);
        //                }
        //                else {
        //                    alert(data["msg"]);
        //                }
        //            }, "json");
        //        }
        //    }
        //    else {
        //        alert("请输入图形验证码！");
        //    }
        //}
    </script>
    <!--弹出菜单 end-->
    <!--弹出菜单 end-->
</head>
<body style="background-color: #fff;">
    <uctop:top ID="top" runat="server" />

    <!--主体 开始-->
    <div class="content02" style="margin-top: 40px!important;">
        <div class="blk02">
            <h2>

                <ul>
                    <li id="a01" class="on">注 册<img src="images/icon15.jpg"></li>
                    <%--  <li id="a02">卖家注册<img src="images/icon15.jpg"></li>--%>
                </ul>

                <div class="clear"></div>
                <span>已有易耐网账号，<a href="/login.aspx" target="_blank">马上登录</a></span>
            </h2>
            <div class="b02_main" id="aa01">
                <form name="regform" id="regform" action="/member/register_do.aspx" method="post" onsubmit="return check_regform();">
                    <ul style="width: 810px;">
                        <li><span><i>*</i>用户名：</span><label><input name="member_realname" id="member_realname" type="text" placeholder="请输入用户名" style="width: 298px;" onfocus="$('#m_real_name').hide();" onblur="check_real_name('member_realname');" maxlength="20" /><i>*</i><strong class="regtip" id="member_realname_tip"></strong></label>
                        </li>
                        <div class="clear"></div>


                        <li><span><i>*</i>输入密码：</span><label><input name="member_password" id="member_password" type="password" style="width: 298px;" value="" placeholder="请输入6～20位密码(A-Z,a-z,0-9,不要输入空格)" onfocus="$('#m_pwd_cipher').hide();" onblur="check_member_pwd('member_password');" maxlength="20" /><i>*</i><strong class="regtip" id="member_password_tip"></strong></label>
                        </li>
                        <div class="clear"></div>


                        <li><span><i>*</i>确认密码：</span><label><input name="member_password_confirm" id="member_password_confirm" type="password" style="width: 298px;" value="" placeholder="请再次输入密码" onblur="check_member_repwd('member_password_confirm');" maxlength="20" /><i>*</i><strong class="regtip" id="'objeck'"></strong></label>
                        </li>
                        <div class="clear"></div>



                        <li><span><i>*</i>公司名称：</span><label><input name="member_company" id="member_company" type="text" placeholder="请准确输入公司名称" style="width: 298px;" onfocus="$('#m_member_company').hide();" onblur="check_real_suppliername('member_company');" maxlength="20" /><i>*</i><strong class="regtip" id="check_real_suppliername_tip"></strong></label>
                        </li>
                        <div class="clear"></div>


                        <li><span><i>*</i>真实姓名：</span><label><input name="Member_Profile_Contact" id="Member_Profile_Contact" type="text" placeholder="请准确输入真实姓名" style="width: 298px;" onfocus="$('#m_Member_Profile_Contact').hide();" onblur="check_Member_Profile_Contact('Member_Profile_Contact');" maxlength="20" /><i>*</i><strong class="regtip" id="check_Member_Profile_Contact_tip"></strong></label>
                        </li>
                        <div class="clear"></div>




                        <%--    <li><span><i>*</i>邮箱地址：</span><label><input name="member_email" id="member_email" type="text"  placeholder ="请准确输入邮箱地址" onblur="check_member_email('member_email')" style="width: 298px;" /><i>*</i><strong class="regtip" id="member_email_tip"></strong></label></li>
                        <div class="clear"></div>--%>

                        <li><span><i>*</i>手机号码：</span><label><input name="member_mobile" id="member_mobile" type="text" style="width: 298px;" value="" placeholder="请输入有效的手机号码" onblur="check_member_mobile('member_mobile');" autocomplete="off" /><i>*</i><strong class="regtip" id="member_mobile_tip"></strong></label></li>
                        <div class="clear"></div>

                        <li><span>验证码：</span><label><input type="text" name="verifycode" id="verifycode" onblur="check_member_verifycode('verifycode');"
                            style="width: 195px;" /><b style="margin-left: 10px; margin-right: 10px; display: inline; width: auto !important; margin-top: 6px;"><img src="/Public/verifycode.aspx" width="65" height="32" id="var_img" style="display: inline; vertical-align: middle" /></b><a
                                href="javascript:void(0);" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img
                                    src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a><i>*</i><strong
                                        class="regtip" id="verifycode_tip"></strong></label></li>


                        <li><span><i>*</i>短信验证码：</span><label><input name="mobile_verifycode" id="mobile_verifycode" type="text" placeholder="短信验证码" style="width: 138px;" onblur="check_sms_checkcode('mobile_verifycode', 'member_mobile');" autocomplete="off" /><a id="mobile_verifycode_tip" onclick="get_sms_checkcode()" class="a03">获取短信验证码</a><span id="btn_sms_checkcode"></span></label></li>
                        <div class="clear"></div>







                        <li style="font-size: 14px; color: #666; padding: 0 0 0 117px;">
                            <input name="checkbox_agreement" id="checkbox_agreement" type="checkbox" value="1" checked="checked" />已阅读并同意<a href="/help/index.aspx?help_id=40" target="_blank" class="a04">《易耐网会员协议》</a></li>



                        <div class="reveal-modal">
                            <div class="thickbox" id="myModal" style="width: 924px; height: 500px; left: 470px; top: 91.5px; visibility: hidden; position: fixed;">
                                <div class="thicktitle" id="" style="width: 922px;"><span>易耐网用户协议</span></div>
                                <div class="thickcon" id="Div1" style="width: 922px; height: 298px; padding-left: 0px; padding-right: 0px; border-left-width: 1px; border-right-width: 1px;">
                                    <div class=" regist-2013">
                                        <div class="regist-bor">
                                            <div class="mc">
                                                <div id="protocol-con">
                                                    <h4>易耐网服务条款的确认和接纳</h4>
                                                    <p>易耐网的各项服务的所有权和运作权归易耐网。易耐网提供的服务将完全按照其发布的服务条款和操作规则严格执行。用户必须完全同意所有服务条款并完成注册程序，才能成为易耐网的正式用户。用户确认：本协议条款是处理双方权利义务的当然约定依据，除非违反国家强制性法律，否则始终有效。在下订单的同时，您也同时承认了您拥有购买这些产品的权利能力和行为能力，并且将您对您在订单中提供的所有信息的真实性负责。</p>
                                                    <h5>服务简介</h5>
                                                    <p>易耐网运用自己的操作系统通过国际互联网络为用户提供网络服务。同时，用户必须： </p>
                                                    <p>如双方就本协议内容或其执行发生任何争议，双方应尽力友好协商解决；协商不成时，任何一方均可向本站所在地的人民法院提起诉讼。</p>
                                                </div>
                                                <div class="btnt">
                                                    <a class="close-reveal-modal">同意并继续</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <li><a href="javascript:;" onclick="$('#regform').submit();" class="a05">立即注册</a></li>
                        <input type="hidden" name="action" value="basicinfo" />
                    </ul>
                </form>
                <b>注册遇到问题？请拨打客服电话：<strong>400-8108-802</strong></b>
                <div class="clear"></div>
            </div>

        </div>
    </div>
    <!--主体 结束-->

    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
