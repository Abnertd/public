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
    Addr addr = new Addr();

    if (tools.NullInt(Session["member_auditstatus"]) == 1)
    {
        member.Member_Login_Check("/member/member_cert.aspx");
    }

    MemberInfo memberInfo = member.GetMemberByID();
    string Member_Cert = "";
    int Member_Cert_Status, Member_CertType;

    IList<MemberRelateCertInfo> relateinfos = null;


    Member_Cert_Status = 0;
    Member_CertType = 0;
    if (memberInfo == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    else
    {
        Member_CertType = memberInfo.Member_Type;

        relateinfos = memberInfo.MemberRelateCertInfos;
        Member_Cert_Status = memberInfo.Member_Cert_Status;
    }
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="资质管理 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src","<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>


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
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>资质管理</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">                 
                        <%=member.Member_Left_HTML(3,4)%>                   
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>资质管理</h2>

                        <%
                            IList<MemberCertInfo> certs = null;
                            certs = member.GetMemberCertByType(0);
                            if (certs != null && tools.NullInt(Session["member_auditstatus"]) == 1)
                            {
                        %>
                        <div class="blk17_sz">
                            <input type="hidden" name="Member_CertType" value="0" />
                            <table width="893" border="0" cellspacing="0" cellpadding="2" id="Table1" class="table_padding_5">


                                <tr>
                                    <%foreach (MemberCertInfo entity in certs)
                                      {
                                          Member_Cert = member.Get_Member_Cert(entity.Member_Cert_ID, relateinfos);
                                    %>
                                    <td width="<%=(100/certs.Count) %>%">
                                        <table border="0" width="100%" cellpadding="3" cellspacing="0" style="width: <%=(100/certs.Count) %>%;">
                                            <tr>
                                                <td align="center" height="120">
                                                    <a href="<%=pub.FormatImgURL(Member_Cert,"fullpath") %>" target="_blank">
                                                        <img id="img1" src="<%=pub.FormatImgURL(Member_Cert,"fullpath") %>" width="120" alt="点击查看原图" title="点击查看原图" height="120" onload="javascript:AutosizeImage(this,120,120);"></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <%=entity.Member_Cert_Name%><input type="hidden" name="member_cert<%=entity.Member_Cert_ID %>"
                                                        id="member_cert<%=entity.Member_Cert_ID %>" value="<%=Member_Cert %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <%} %>
                                </tr>

                            </table>
                        </div>
                        <%} %>

                        <form name="formadd" id="formadd" method="post" action="/member/account_do.aspx">
                            <%if (Member_Cert_Status != 2 && tools.NullInt(Session["member_auditstatus"]) != 1)
                              { %>
                            <div class="blk17_sz">
                                <input type="hidden" name="Member_CertType" value="0" />

                                <%if (certs != null)
                                  { %>
                                <table width="893" border="0" cellspacing="0" cellpadding="5" style="width: 100%;">
                                    <tr>
                                        <%foreach (MemberCertInfo entity in certs)
                                          {
                                              Member_Cert = member.Get_Member_Certtmp(entity.Member_Cert_ID, relateinfos);
                                        %>
                                        <td width="<%=(100/certs.Count) %>%">
                                            <table border="0" cellpadding="3" cellspacing="0" style="width: <%=(100/certs.Count) %>%">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_member_cert<%=entity.Member_Cert_ID %>_tmp" name="img_member_cert<%=entity.Member_Cert_ID %>_tmp"
                                                            src="<%=pub.FormatImgURL(Member_Cert,"fullpath") %>" width="120" height="120"
                                                            onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <%if (Member_Cert_Status != 2)
                                                  { %>
                                                <tr>
                                                    <td align="center" height="30">
                                                        <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript:openUpload('member_cert<%=entity.Member_Cert_ID %>    _tmp');" />
                                                        <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript:delImage('member_cert<%=entity.Member_Cert_ID %>    _tmp');">
                                                        <input type="hidden" name="member_cert<%=entity.Member_Cert_ID %>_tmp" id="member_cert<%=entity.Member_Cert_ID %>_tmp"
                                                            value="<%=Member_Cert %>" />
                                                    </td>
                                                </tr>
                                                <%} %>
                                                <tr>
                                                    <td align="center">
                                                        <%=entity.Member_Cert_Name%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%} %>
                                    </tr>

                                    <tr id="td_upload" style="display: none">
                                        <td colspan="4" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="300" height="90" frameborder="0" scrolling="no"
                                            align="absmiddle"></iframe>
                                        </td>
                                    </tr>

                                    <% if (Member_Cert_Status != 2)
                                       { %>
                                    <tr>
                                        <td colspan="4" align="center" style="text-align: center;">
                                            <input type="hidden" id="action" name="action" value="certsave" />
                                            <a href="javascript:void(0);" onclick="$('#formadd').submit();" class="a11" style="display: inline-block;"></a>
                                        </td>
                                    </tr>
                                    <%} %>
                                </table>
                                <%} %>
                            </div>
                            <%} %>
                        </form>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
