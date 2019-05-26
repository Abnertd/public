<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    Member member = new Member();
    ITools tools = ToolsFactory.CreateTools();
    MemberInfo memberInfo = member.GetMemberByID();
    member.Member_Login_Check("/member/emailverify.aspx");
    if (memberInfo != null && memberInfo.Member_Emailverify == 1)
    {
        Response.Redirect("/member/");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="邮箱验证 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
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
                    background-color: #e33d3d;
                    font-size: 18px;
                    font-weight: normal;
                    line-height: 40px;
                    border-radius: 2px;
                    color: #FFF;
                }

                    .table_help input.buttonSkinB:hover {
                        background-color: #ce1329;
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
            <div class="position">当前位置 ><a href="/tradeindex.aspx">首页</a> > <a href="/member/emailverify.aspx">邮箱验证</a></div>
            <!--位置说明 结束-->
            <div class="partd_sz">

                <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0" class="table_padding_6 table_help">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td class="t14">
                                        <strong>邮箱验证</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="t14" style="line-height: 22px;">
                                        <p class="t14">
                                            <strong>还差最后一步，就可以完成注册！</strong>
                                        </p>
                                        <p>
                                            <span class="t14_orange">您的邮箱还没通过验证，现在请按以下步骤操作</span><br />
                                            <br />
                                            <strong>第一步：查收验证邮件</strong><br />
                                            我们已发送验证邮件到：<a style="color: blue;" href="http://<%=member.GetMail_Site(tools.NullStr(Session["member_email"]).Substring(tools.NullStr(Session["member_email"]).IndexOf('@')+1))%>"
                                                target="_blank"><%=Session["member_email"]%></a>，如果您的邮箱有误，<a href="javascript:;"
                                                    onclick="javascript:MM_findObj('modifyemail').style.display='';" class="a_t12_blue" style="color: #ff6600;">请点此修改</a><br />
                                            <br />
                                            <div id="modifyemail" style="display: none">
                                                <form name="form_modifyemail" action="/member/register_do.aspx" method="post">
                                                    <span class="t14">更换新的邮箱</span><br />
                                                    <input name="member_email" type="text" id="member_email" size="40" maxlength="100" />
                                                    <input name="btnsubmit" type="submit" value="验证" align="absmiddle" />
                                                    <input name="action" type="hidden" id="action" value="modifyemail">
                                                    <div id="tip_supplier_email">
                                                    </div>
                                                </form>
                                                <br />
                                            </div>
                                            <strong>第二步：点击邮件中验证链接</strong><br />
                                            点击验证邮件中的验证链接，即可验证成功！<p />
                                            <br />
                                            <a href="/member/">
                                                <img src="/images/but_after.gif" border="0" /></a><br />
                                            <hr size="1" noshade="noshade" color="#CDCDCD" />
                                            <span class="t12">如果没有收到验证邮件，</span><a href="/member/register_do.aspx?action=resendemailverify"
                                                class="a_t12_blue" style="color: #ff6600;">请点此重新发送</a>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
