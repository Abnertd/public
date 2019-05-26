<%@ Page Language="C#"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    Public_Class pub = new Public_Class();
    Member member = new Member();
     %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="ERP用户绑定 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
</head>
<body>
   <%-- <uctop:top ID="top1" runat="server" />--%>

     <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/member/">采购商用户中心</a> > 辅助功能 > <strong>收货地址管理</strong></div>
            <!--位置说明 结束-->

            <!--会员中心主体 开始-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%=member.Member_Left_HTML(3,8) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">ERP用户绑定</div>
                    <div class="blk17">
                        <form action="/member/account_do.aspx" method="post" name="frm_binding" id="frm_binding">
                            <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td width="92" class="name">ERP用户ID：</td>
                                    <td> <input name="ERPuserid" type="text" id="ERPuserid" style="width: 366px;" maxlength="100" value="" /><i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">ERP用户密码：</td>
                                    <td width="801">
                                        <input name="Pwd" type="password" id="Pwd" style="width: 366px;" maxlength="100" value="" />
                                        <i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">&nbsp;</td>
                                    <td width="801">
                                        <a href="javascript:void(0);" onclick="erp_binding();" class="a11">绑 定</a>
                                        <input type="hidden" name="action" id="action" value="erp_binding" />
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <!--会员中心主体 结束-->
        </div>
    </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
