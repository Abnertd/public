<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    AD ad = new AD();
    ITools tools = ToolsFactory.CreateTools();
    if (Session["supplier_logined"].ToString() == "True")
    {
        Response.Redirect("/supplier/index.aspx");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="退出 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <div class="content">
    <div style=" margin:10px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="tip_bg_positive">
            <tr>
                <td height="150">
                    <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                        <tr>
                            <td width="100" align="center" valign="middle">
                                <img src="/images/info_success_48.gif">
                            </td>
                            <td align="left" valign="top">
                                <h2>
                                    您已成功退出</h2>
                                您已成功退出。点击这里
                                <input name="btnhome" type="button" class="buttonSkinB" id="btnhome" value="返回首页"
                                    onclick="location.href='/';" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div></div>
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
