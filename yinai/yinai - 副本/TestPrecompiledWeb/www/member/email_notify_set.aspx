<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();

    supplier.Supplier_AuditLogin_Check("/member/email_notify_set.aspx");
    string action_value, btn_value, btn_value2, action_value2;
    if (tools.NullInt(Session["Supplier_AllowSysEmail"]) == 0)
    {
        action_value = "allowsysemail";
        btn_value = "邮件订阅";
    }
    else
    {
        action_value = "cancelsysemail";
        btn_value = "取消订阅";
    }

    if (tools.NullInt(Session["Supplier_AllowSysMessage"]) == 0)
    {
        action_value2 = "allowsysmessage";
        btn_value2 = "手机订阅";
    }
    else
    {
        action_value2 = "cancelsysmessage";
        btn_value2 = "取消订阅";
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="信息订阅 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <style type="text/css">
        .yz_blk19_main img {
            display: inline;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="margin-bottom:20px;">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/index.aspx">首页</a> > <a href="/member/index.aspx">我是买家</a> > <a href="email_notify_set.aspx">信息订阅</a>
        </div>
        <div class="clear"></div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(10, 2); %>
            </div>
            <div class="pd_right">
                <div class="blk14_1">
                    <h2>信息订阅</h2>
                    <div class="b14_1_main">


                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="t14_red" colspan="2">内部销售商品的型号，规格，品种将随时更新，为确保您不错过心仪的商品，我们将不定期地向您的电子邮箱发送最新商品目录及相关资讯，敬请关注。请问您是否愿意订阅此类邮件？
                                </td>
                            </tr>
                            <tr>
                               <td>
                                    <form name="frm_account_profile" method="post" action="/supplier/account_do.aspx">
                                        <input name="action" type="hidden" value="<%=action_value %>" />
                                        <input name="btn_submit" type="submit" class="buttonupload" id="Submit1" value="<%=btn_value %>" />
                                    </form>
                                </td>
                                <td>
                                    <form name="frm_account_profile" method="post" action="/supplier/account_do.aspx">
                                        <input name="action" type="hidden" value="<%=action_value2 %>" />
                                        <input name="btn_submit" type="submit" class="buttonupload" id="Submit2" value="<%=btn_value2 %>" />
                                    </form>
                                </td>
                            </tr>
                        </table>


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
