<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/MEM_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();

    int brand_joined_id = tools.CheckInt(Request["id"]);
    member.Member_Login_Check("/member/brand_joined_replay.aspx?id=" + brand_joined_id);

    if (brand_joined_id < 0)
    {
        Response.Redirect("/member/index.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="品牌加盟 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>

</head>
<body>

    <!--主体 开始-->
    <div class="content02">
        <div class="content02_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/index.aspx">首页</a> > <a href="/member/index.aspx">我是买家</a> > 辅助管理 > <strong>品牌加盟</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pc_left">
                    <div class="blk12">
                        <%=member.Member_Left_HTML(1,1) %>
                    </div>
                </div>
                <div class="pc_right">
                    <div class="title03">品牌加盟</div>
                    <div class="blk17">
                        <form name="frm_reply" id="frm_reply" method="post" action="/member/account_do.aspx">

                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td width="92" class="name">内容
                                    </td>
                                    <td width="801">
                                        <textarea name="Message_Content" id="Message_Content" cols="80" rows="5"></textarea><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">联系人
                                    </td>
                                    <td width="801">
                                        <input name="Message_Contactman" type="text" id="Message_Contactman" style="width: 300px;" class="input01" /><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">联系电话
                                    </td>
                                    <td width="801">
                                        <input name="Message_ContactMobile" type="text" id="Message_ContactMobile" style="width: 300px;" class="input01" /><i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">Email
                                    </td>
                                    <td width="801">
                                        <input name="Message_ContactEmail" type="text" id="Message_ContactEmail" style="width: 300px;" class="input01" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">&nbsp;
                                    </td>
                                    <td width="801">
                                        <span class="table_v_title">
                                            <input name="action" type="hidden" id="action" value="merchantsreply" />
                                            <input name="Message_MerchantsID" id="Message_MerchantsID" type="hidden" value="<%=brand_joined_id %>" />
                                            <a href="javascript:void(0);" onclick="$('#frm_reply').submit();" class="a11">保 存</a>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
  
</body>
</html>
