<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    Public_Class pub = new Public_Class();
    Supplier supplier = new Supplier();
    Member member = new Member();
    string emailverify_result = Request["result"]; 
%> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="邮箱验证 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

      <!--主体 开始-->
    <div class="content02_sz" style="background-color: #FFF;">
        <div class="content02_main_sz" style="background-color: #FFF;">
        <!--位置说明 开始-->
        <div class="position">当前位置 > <a href="/tradeindex.aspx">首页</a> > <strong>邮箱验证</strong>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_sz">

        <table width="100%" border="0" align="center" cellpadding="6" cellspacing="0" class="table_help_sz">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                        <tr>
                            <td class="t14">
                                <strong>邮箱验证</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%if (emailverify_result != "true")
                                  {%>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td class="t14">
                                            <img src="/images/info_error_48.gif" style="display:inline; vertical-align:middle; margin:0 10px;" />验证失败，请检查您点击的链接是否正确。你也可以尝试
                                            <input name="btneamilvarify" type="button" class="buttonSkinB" id="btneamilvarify"
                                                value="重新验证" onclick="location.href='/member/emailverify.aspx';" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <hr size="1" noshade="noshade" color="#CDCDCD" />
                                            如果有任何疑问，欢迎给我们留言，我们将尽快给您回复！
                                        </td>
                                    </tr>
                                </table>
                                <%}
                                  else
                                  {%>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td class="t14">
                                            <img src="/images/info_success_48.gif" style="display:inline; vertical-align:middle; margin:0 10px;" /><%=member.replace_sys_config("验证成功，欢迎使用{sys_config_site_name}！")%>
                                            <%
                                                if (Session["supplier_logined"].ToString() != "True")
                                                {
                                                    Response.Write("<input name=\"btnindex\" type=\"button\" class=\"buttonSkinB\" id=\"btnindex\" value=\"登录网站\" onclick=\"location.href='/login.aspx?u_type=1';\"/>");
                                                }
                                                else
                                                {
                                                    Response.Write("<input name=\"btnindex\" type=\"button\" class=\"buttonSkinB\" id=\"btnindex\" value=\"进入会员中心\" onclick=\"location.href='/member/index.aspx';\"/>");
                                                } %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <hr size="1" noshade="noshade" color="#CDCDCD" />
                                            如果有任何疑问，欢迎给我们留言，我们将尽快给您回复！
                                        </td>
                                    </tr>
                                </table>
                                <%}%>
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
