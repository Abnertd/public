<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/Supplier_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();


    int Purchase_ID = tools.NullInt(Request["Purchase_ID"]);
    supplier.Supplier_Login_Check("/supplier/Purchase_Reply.aspx?Purchase_ID=" + Purchase_ID);
    if (Purchase_ID < 0)
    {
        Response.Redirect("/supplier/index.aspx");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="报价留言 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
    <script type="text/javascript">

        function check_supplier_nickname(object) //1
        {
            $.ajaxSetup({ async: false });
            $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checknickname&val=" + encodeURIComponent($("#" + object).val()) + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</head>


<!--主体 开始-->
<div class="content02" style="background-color: #FFF; width: 975px;">
    <div class="content02_main" style="background-color: #FFF; width: 975px;">
        <div class="partc">
            <div class="pc_right" style="width: 975px;">
                <div class="blk17">
                    <form name="frm_reply" id="frm_reply" method="post" action="/supplier/merchants_do.aspx">

                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                            <tr>
                                <td width="92" class="name">标题
                                </td>
                                <td width="801">
                                    <input name="Reply_Title" type="text" id="Reply_Title" style="width: 300px;" class="input01" /><i>*</i>
                                </td>
                            </tr>
                            <tr>
                                <td width="92" class="name">内容
                                </td>
                                <td width="801">
                                    <textarea name="Reply_Content" id="Reply_Content" cols="80" rows="5"></textarea><i>*</i>
                                </td>
                            </tr>
                            <tr>
                                <td width="92" class="name">姓名
                                </td>
                                <td width="801">
                                    <input name="Reply_Contactman" type="text" id="Reply_Contactman" style="width: 300px;" class="input01" /><i>*</i>
                                </td>
                            </tr>
                            <tr>
                                <td width="92" class="name">手机
                                </td>
                                <td width="801">
                                    <input name="Reply_Mobile" type="text" id="Reply_Mobile" style="width: 300px;" class="input01" /><i>*</i>
                                </td>
                            </tr>
                            <tr>
                                <td width="92" class="name">Email
                                </td>
                                <td width="801">
                                    <input name="Reply_Email" type="text" id="Reply_Email" style="width: 300px;" class="input01" />
                                </td>
                            </tr>
                            <tr>
                                <td width="92" class="name">&nbsp;
                                </td>
                                <td width="801">
                                    <span class="table_v_title">
                                        <input name="action" type="hidden" id="action" value="purchasereply" />
                                        <input name="Reply_PurchaseID" id="Reply_PurchaseID" type="hidden" value="<%=Purchase_ID %>" />
                                        <a href="javascript:void();" onclick="$('#frm_reply').submit();" class="a11">保 存</a>
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


</html>
