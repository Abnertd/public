
<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />


    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
</head>
<body>
    <form id="formAdd" method="post" action="/Bid/bid_do.aspx">
        <table>
            <div style="width: 480px; height: 310px;">
                <%--<div style="padding: 20px; line-height: 22px; background-color: #f8f8f8; color: #fff; font-weight: 300; width: 440px; height: 200px;">--%>
                <div style="padding: 20px; line-height: 22px;  font-weight: 300; width: 440px; height: 200px;">
                    <div style="color: #ff6600; font-size: 24px; text-align: center">快速发布需求</div>
                    <div style="color: #999999; font-size: 14px; text-align: center">(无需注册，即可发布)</div>
                    <textarea style="width: 430px; height: 110px; margin-top: 8px" cols="3" id="signupcontent" rows="4" name="signupcontent"></textarea>
                    <div style="color: #000000; margin-top: 20px; font-size: 16px">
                        <span style="margin-left: 2px">联系人:<input id="signupcontractman" style="width: 130px" name="signupcontractman" value="" /></span>
                        <span style="margin-left: 20px">联系电话:<input id="signupcontractmobile" name="signupcontractmobile" style="width: 130px" value="" /></span>
                    </div>
                    <div style="color: #000000; margin-top: 16px; font-size: 16px">
                        <span style="float: left; margin-left: 13px;">验证码:
                                            <input type="text" name="verifycode" id="verifycode"
                                                style="width: 130px; margin-left: -5px" placeholder="请输入验证码" /></span>  <span style="margin-left: 25px; float: left;">
                                                    <img src="/Public/verifycode.aspx" width="65" height="32" id="supplier_verify_img" style="display: inline; vertical-align: middle" /><a
                                                        href="javascript:void(0);" onclick="$('#supplier_verify_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"><img src="/images/sx_icon.jpg" width="14" height="14" title="看不清，换一张" alt="看不清，换一张" style="display: inline; vertical-align: middle" /></a></span>
                        <div id="verifycode_tip"></div>
                    </div>

                    <%-- <div style="color: #000000; margin-top: 20px; font-size: 16px">
                        <span style="margin-left: 2px">联系人:<input id="Text1" style="width: 130px" name="signupcontractman" value="" /></span>
                        <span style="margin-left: 40px">联系电话:<input id="Text2" name="signupcontractmobile" style="width: 130px" value="" /></span>
                   </div>--%>
                </div>
                <div style="margin-top: 55px">

                    <input name="action" type="hidden" id="action" value="BidUpRequireQuickadd" />
                    <input name="Feedback_Attachment" type="hidden" id="Feedback_Attachment" />
                    <span style="float: left; margin-left: 187px; margin-bottom: 10px;"><a href="javascript:void(0);" onclick="$('#formAdd').submit();" class="btn03" style="width: 98px;">发布</a>
                    </span>


                </div>
            </div>
        </table>
    </form>
</body>
</html>
