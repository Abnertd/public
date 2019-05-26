<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    string Feedback_Name="";
    string Feedback_Email="";
    string Feedback_Tel="";
    string Feedback_Content="";
    string Feedback_Reply_Content="";
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();


    Glaer.Trade.B2C.BLL.MEM.IFeedBack MyFeedback;
    MyFeedback = Glaer.Trade.B2C.BLL.MEM.FeedBackFactory.CreateFeedBack();
    member.Member_Login_Check("/member/feedback.aspx");

    MemberInfo memberinfo = member.GetMemberByID();



   
    if (member == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    MemberProfileInfo profile = memberinfo.MemberProfileInfo;
    int feedback_id = tools.NullInt(Request["feedback_id"]);
 FeedBackInfo feedbackinfo=   MyFeedback.GetFeedBackByID(feedback_id, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
 if (feedbackinfo!=null)
 {
   Feedback_Name=  feedbackinfo.Feedback_Name;
   Feedback_Email=  feedbackinfo.Feedback_Email;
  Feedback_Tel=   feedbackinfo.Feedback_Tel;
   Feedback_Content=  feedbackinfo.Feedback_Content;
  Feedback_Reply_Content=   feedbackinfo.Feedback_Reply_Content;
 }
 else
 {
     Response.Redirect("/member/feedback.aspx");
 }
  
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="售后服务 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
     <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
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
                        <% =member.Member_Left_HTML(3, 0) %>
                    </div>
                    <div class="pd_right">
                        <div class="blk14_1" style="margin-top: 0px;">
                            <h2>留言详情</h2>
                            <div class="blk17_sz">
                                <form name="formadd" id="formadd" method="post" action="/member/feedback_do.aspx">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="92" class="name">类型:
                                            </td>
                                            <td width="801">
                                             
                                                 <input name="Feedback_Type" type="hidden" id="Text1" style="width: 300px; background: none !important" class="input01" maxlength="50" value="1"    />                                                                                             
                                                网站留言
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="92" class="name">姓名:
                                            </td>
                                            <td width="801">
                                              <%=Feedback_Name %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="92" class="name">E-mail:
                                            </td>
                                            <td width="801">
                                                <%=Feedback_Email %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="92" class="name">您的手机:
                                            </td>
                                            <td width="801">
                                                <%=Feedback_Tel %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="92" class="name">留言内容:</td>
                                            <td>
                                              <%=Feedback_Content %></td>
                                        </tr>
                                         <tr>
                                            <td width="92" class="name">回复信息:</td>
                                            <td>
                                               <%=Feedback_Reply_Content %></td>
                                        </tr>
                                      <%--  <tr>
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
                                        </tr>--%>
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

      <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
