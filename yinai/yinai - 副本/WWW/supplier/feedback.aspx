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

    supplier.Supplier_Login_Check("/supplier/feedback.aspx");

    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    //MemberProfileInfo profile = member.MemberProfileInfo;
    
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="客服留言 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
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
        .yz_blk19_main img {
            display: inline;
        }

        #var_img {
        display:inline;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
     <div class="webwrap">
    <div class="content02">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="feedback.aspx">客服留言</a>
        </div>
        <div class="clear"></div>
        <!--位置说明 结束-->
        <div class="partc">
            <div class="partd_1" >
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(10, 3); %>
            </div>
            <div class="pc_right">
               <%-- <div class="blk13">--%>
                    <%--<h2>客服留言</h2>--%>
                  <div class="title03">客服留言</div>
                    <div class="blk17_sz">
                        <%
                            if (tools.CheckStr(Request.QueryString["tip"]) == "success")
                            {
                                pub.Tip("positive", "您的咨询已成功提交，我们的客服人员会尽快回复，感谢您对" + Application["Site_Name"].ToString() + "的支持！祝您购物愉快！！");
                        %>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10"></td>
                            </tr>
                        </table>
                        <%}%><form name="form_feedback" id="form_feedback" method="post" action="/supplier/feedback_do.aspx">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="8" class="table_padding_5">
                                            <tr>
                                                <td width="100" align="center" class="t12_53">类型
                                                </td>
                                                <td class="t12_grey" align="left">
                                                    <select name="Feedback_Type">
                                                        <option value='1'>简单的留言</option>
                                                        <option value='2'>对网站的意见</option>
                                                        <option value='3'>对公司的建议</option>
                                                        <option value='4'>具有合作意向</option>
                                                        <option value='5'>商品投诉</option>
                                                        <option value='6'>服务投诉</option>
                                                    </select>
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="center" class="t12_53">姓名
                                                </td>
                                                <td class="t12_grey" align="left">
                                                    <input name="Feedback_Name" class="txt_border" type="text" id="Text1" size="30" maxlength="50"
                                                        value="" />
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="center" class="t12_53">公司名称
                                                </td>
                                                <td class="t12_grey" align="left">
                                                    <input name="Feedback_CompanyName" class="txt_border" type="text" id="Feedback_CompanyName" size="30" maxlength="100"
                                                        value="" />
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="center" class="t12_53">E-mail
                                                </td>
                                                <td class="t12_grey" align="left">
                                                    <input name="Feedback_Email" class="txt_border" type="text" id="Text2" size="30"
                                                        maxlength="100" value="" />
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="center" class="t12_53">您的电话
                                                </td>
                                                <td class="t12_grey" align="left">
                                                    <input name="Feedback_Tel" class="txt_border" type="text" id="Text3" size="30" maxlength="50"
                                                        value="" />
                                                    <span class="t12_red">*</span> 请留下您的电话，以便我们及时与您取得联系
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="t12_53">内容
                                                </td>
                                                <td align="left">
                                                    <textarea name="feedback_content" cols="50" class="txt_border" rows="5" style="height: 60px;"
                                                        id="Textarea1"></textarea>
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="t12_53" align="right">附件</td>
                                                <td align="left">
                                                    <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=attachment&formname=form_feedback&frmelement=Feedback_Attachment&rtvalue=1&rturl=<% =Application["upload_server_return_WWW"]%>" width="100%" height="20" frameborder="0" scrolling="no"></iframe>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="t12_53">验证码
                                                </td>
                                                <td align="left">
                                                    <input name="verifycode" style="width: 100px;" type="text" id="Text4" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());"
                                                        class="txt_border" size="10" maxlength="10" />
                                                    <img title="看不清？换一张" alt="看不清？换一张"  src="/public/verifycode.aspx" style="cursor: pointer;"
                                                        id="var_img" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"
                                                        align="absmiddle" />
                                                    <span class="t12_red">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="t12_53"></td>
                                                <td align="left">
                                                    <input name="btn_submit" type="image" src="/images/a_bg05.jpg" align="absmiddle" /><input
                                                        name="action" type="hidden" id="Hidden2" value="add" />
                                                    <input name="Feedback_Attachment" type="hidden" id="Feedback_Attachment" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </form>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10"></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="table_member table_padding_5">
                            <tr>
                                <td class="table_member_bar">您提交的留言信息
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%
                                        supplier.Feedback_List();
                                    %>
                                </td>
                            </tr>
                        </table>


                    </div>
              <%--  </div>--%>
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
