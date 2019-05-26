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
    <title><%="优惠券 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
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
</head>
<body>

    <uctop:top ID="top1" runat="server" />




    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <%--  <div class="position">当前位置 > <a href="/index.aspx">首页</a> > <span>会员中心</span></div>--%>

            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>优惠券</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="partd_1">
                    <div class="pd_left">                   
                            <% =member.Member_Left_HTML(1, 7) %>                     
                    </div>
                    <div class="pd_right">
                        <div class="blk14_1" style="margin-top: 0px;">
                            <h2>优惠券</h2>

                            <div class="b14_1_main" style="margin-top: 15px;">
                                <%member.Member_Coupon_List(); %>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
