<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private ITools tools;
    private Member myApp;
    private MemberGrade Mgrade;
    Addr Addr;
    Orders orderApp;

    private string Member_Grade_Name, Member_Cert;

    private string Member_Name, Member_StreetAddress, Member_Country, Member_State, Member_City, Member_County, Member_Zip, Member_Phone_Countrycode, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile, Member_RegIP, Member_Company_Introduce, Member_Company_Contact;
    private int Member_Profile_ID, Member_Profile_MemberID, Member_Sex, Member_Occupational, Member_Education, Member_Income, Member_AuditStatus, Member_Cert_Status;

    private string Member_Email, Member_LoginMobile, Member_NickName, Member_Password, Member_VerifyCode, Member_LastLogin_IP, Member_Site, Member_Company, Member_Fax, Member_QQ, Member_OrganizationCode, Member_BusinessCode;
    private int Member_ID, Member_Emailverify, Member_LoginMobileverify, Member_LoginCount, Member_CoinCount, Member_CoinRemain, Member_Trash, Member_Grade, Member_AllowSysEmail, Member_Status,Member_Type;
    private DateTime Member_LastLogin_Time, Member_Addtime, Member_Birthday;
    private double Member_Account, Member_Frozen;

    IList<MemberRelateCertInfo> relateinfos = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
        myApp = new Member();
        tools = ToolsFactory.CreateTools();
        Mgrade = new MemberGrade();
        Addr = new Addr();
        orderApp = new Orders();

        Member_ID = tools.CheckInt(Request.QueryString["Member_ID"]);
        MemberInfo entity = myApp.GetMemberByID(Member_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Member_ID = entity.Member_ID;
            Member_Type = entity.Member_Type;
            Member_Email = entity.Member_Email;
            Member_Emailverify = entity.Member_Emailverify;
            Member_LoginMobile = entity.Member_LoginMobile;
            Member_LoginMobileverify = entity.Member_LoginMobileverify;
            Member_NickName = entity.Member_NickName;
            Member_Password = entity.Member_Password;
            Member_VerifyCode = entity.Member_VerifyCode;
            Member_LoginCount = entity.Member_LoginCount;
            Member_LastLogin_IP = entity.Member_LastLogin_IP;
            Member_LastLogin_Time = entity.Member_LastLogin_Time;
            Member_CoinCount = entity.Member_CoinCount;
            Member_CoinRemain = entity.Member_CoinRemain;
            Member_Addtime = entity.Member_Addtime;
            Member_Trash = entity.Member_Trash;
            Member_Grade = entity.Member_Grade;
            Member_Account = entity.Member_Account;
            Member_Frozen = entity.Member_Frozen;
            Member_AllowSysEmail = entity.Member_AllowSysEmail;
            Member_Site = entity.Member_Site;
            Member_RegIP = entity.Member_RegIP;
            Member_Status = entity.Member_Status;
            Member_AuditStatus = entity.Member_AuditStatus;
            Member_Cert_Status = entity.Member_Cert_Status;
            Member_Company_Introduce = entity.Member_Company_Introduce;
            Member_Company_Contact = entity.Member_Company_Contact;
            
            relateinfos = entity.MemberRelateCertInfos;
            MemberProfileInfo ProfileInfo = entity.MemberProfileInfo;

            if (ProfileInfo != null)
            {
                Member_Profile_ID = ProfileInfo.Member_Profile_ID;
                Member_Profile_MemberID = ProfileInfo.Member_Profile_MemberID;
                Member_Name = ProfileInfo.Member_Name;
                Member_Sex = ProfileInfo.Member_Sex;
                Member_Birthday = ProfileInfo.Member_Birthday;
                Member_Occupational = ProfileInfo.Member_Occupational;
                Member_Education = ProfileInfo.Member_Education;
                Member_Income = ProfileInfo.Member_Income;
                Member_StreetAddress = ProfileInfo.Member_StreetAddress;
                Member_County = ProfileInfo.Member_County;
                Member_City = ProfileInfo.Member_City;
                Member_State = ProfileInfo.Member_State;
                Member_Country = ProfileInfo.Member_Country;
                Member_Zip = ProfileInfo.Member_Zip;
                Member_Phone_Countrycode = ProfileInfo.Member_Phone_Countrycode;
                Member_Phone_Areacode = ProfileInfo.Member_Phone_Areacode;
                Member_Phone_Number = ProfileInfo.Member_Phone_Number;
                Member_Mobile = ProfileInfo.Member_Mobile;
                Member_Company = ProfileInfo.Member_Company;
                Member_Fax = ProfileInfo.Member_Fax;
                Member_QQ = ProfileInfo.Member_QQ;
                Member_OrganizationCode = ProfileInfo.Member_OrganizationCode;
                Member_BusinessCode = ProfileInfo.Member_BusinessCode;
                
            }

            MemberGradeInfo GradeInfo = Mgrade.GetMemberGradeByID(Member_Grade);
            if (GradeInfo != null) { Member_Grade_Name = GradeInfo.Member_Grade_Name; }
            else { Member_Grade_Name = "--"; }
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
        Mgrade = null;
        Addr = null;
    }
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/member.js" type="text/javascript"></script>
    <script src="/Scripts/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChangeMemberAuditStatus(obj) {
            $("#formadd").attr("action", "/member/member_do.aspx?action=" + obj);
            document.formadd.submit();
        }
    </script>

    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src","<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formcert&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_Admin"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>

</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">采购商信息</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">注册邮箱</td>
                            <td class="cell_content"><% =Member_Email%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">用户名</td>
                            <td class="cell_content"><% =Member_NickName%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">公司名称</td>
                            <td class="cell_content"><% =Member_Company%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">联系人</td>
                            <td class="cell_content"><% =Member_Name%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">联系电话</td>
                            <td class="cell_content"><% =Member_Phone_Countrycode + "-" + Member_Phone_Areacode + "-" + Member_Phone_Number%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">联系人手机</td>
                            <td class="cell_content"><% =Member_Mobile %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">地址</td>
                            <td class="cell_content"><% =Addr.DisplayAddress(Member_State, Member_City, Member_County) +" "+ Member_StreetAddress %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">邮编</td>
                            <td class="cell_content"><% =Member_Zip%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">注册时间</td>
                            <td class="cell_content"><% =Member_Addtime%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">注册IP</td>
                            <td class="cell_content"><% =Member_RegIP%></td>
                        </tr>
                        <%--<tr>
          <td class="cell_title">IP所在地</td>
          <td class="cell_content"><% =Addr.GetIPArea(Member_RegIP)%></td>
        </tr>--%>
                        <tr>
                            <td class="cell_title">订单数</td>
                            <td class="cell_content"><a href="/orders/orders_list.aspx?listtype=all&member_id=<%=Member_ID %>"><%=orderApp.Member_Order_Count(Member_ID,"")%>个</a></td>
                        </tr>
                        <tr>
                            <td class="cell_title">登陆次数</td>
                            <td class="cell_content"><% =Member_LoginCount%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">最后登录时间</td>
                            <td class="cell_content"><% =Member_LastLogin_Time%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">最后登录IP</td>
                            <td class="cell_content"><% =Member_LastLogin_IP%></td>
                        </tr>


                         <tr>
                            <td class="cell_title">公司介绍</td>
                            <td class="cell_content" ><textarea rows=""  cols="1" style="width:688px;height:98px; " readonly="readonly" ><% =Member_Company_Introduce%></textarea></td>
                        </tr>
                        <tr>
                            <td class="cell_title">公司联系方式</td>
                            <td class="cell_content" ><textarea rows=""  cols="1" style="width:688px;height:98px; " readonly="readonly" ><% =Member_Company_Contact%></textarea></td>
                        </tr>

                        <tr>
                            <td class="cell_title">审核状态</td>
                            <td class="cell_content">
                                <%=myApp.Member_AuditStatus(Member_AuditStatus) %>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>



        </table>

        <%--<table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">资质信息</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form name="formcert" id="formcert" method="post" action="/member/account_do.aspx">
                    <div id="cert_show" style="position: absolute; border: 1px solid #000; display: none;"></div>
                    <div id="cert_compare"></div>
                    <script type="text/javascript">
                        function show_cert(url) {
                            $("#cert_show").html('<img src=' + url + ' width="600">')
                            $("#cert_show").show();
                            var ojbfoot = $("#cert_compare").offset().top - $("#cert_show").height() - 10;
                            var ojbleft = $("#cert_compare").width() / 2 - ($("#cert_show").width() / 2);
                            $("#cert_show").css("top", ojbfoot);
                            $("#cert_show").css("left", ojbleft);
                        }

                    </script>
                    <table width="100%" border="0" cellpadding="5" cellspacing="0">
                        <%
                            IList<MemberCertInfo> certs = null;
                            certs = myApp.GetMemberCertByType(0);

                            if (certs != null)
                            {
                                Response.Write("<tr>");
                                foreach (MemberCertInfo certinfo in certs)
                                {
                                    Member_Cert = myApp.Get_Member_Cert(certinfo.Member_Cert_ID, relateinfos);
                        %>
                        <td width="<%=100/certs.Count %>%" align="center">

                            <table border="0" cellpadding="3" cellspacing="0" width="<%=100/certs.Count %>%">
                                <tr>
                                    <td  align="center">
                                        <img id="img_member_cert<%=certinfo.Member_Cert_ID %>_tmp" name="img_member_cert<%=certinfo.Member_Cert_ID %>_tmp" src="<% =Public.FormatImgURL(Member_Cert, "fullpath") %>" width="200" height="200"
                                            onmouseover="show_cert('<% =Public.FormatImgURL(Member_Cert, "fullpath") %>');"
                                            onmouseout="$('#cert_show').hide();" /></td>
                                </tr>
                                <tr>
                                                <td align="center" height="30">
                                                    <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript:openUpload('member_cert<%=certinfo.Member_Cert_ID %>_tmp');" />
                                                    <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript:delImage('member_cert<%=certinfo.Member_Cert_ID %>_tmp');">
                                                    <input type="hidden" name="member_cert<%=certinfo.Member_Cert_ID %>_tmp" id="member_cert<%=certinfo.Member_Cert_ID %>_tmp"
                                                        value="<%=Member_Cert %>" />
                                                </td>
                                            </tr>
                                <tr>
                                    <td align="center">
                                        <%=certinfo.Member_Cert_Name%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                        <%
                        }
                          Response.Write("</tr>");
                            }
                            
                            %>
                        <tr id="td_upload" style="display: none">
                            <td colspan="4" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="300" height="90" frameborder="0" scrolling="no"
                                align="absmiddle"></iframe>
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
        </table>--%>


       
        <div class="foot_gapdiv">
        </div>


        <div class="float_option_div" id="float_option_div">
            <form id="formadd" name="formadd" method="post" action="/member/member_do.aspx">
                <%if (Member_AuditStatus == 0 && Public.CheckPrivilege("d8de7f81-7e9a-44ea-9463-dd1afda2b74e"))
                  { %>
                <input type="button" name="Audit" class="bt_orange" value="审核通过" onclick="ChangeMemberAuditStatus('audit');" />
                <input type="button" name="NotAudit" class="bt_orange" value="审核不通过" onclick="ChangeMemberAuditStatus('denyaudit');" />
                <%} %>

                <input type="button" id="savecert" class="bt_orange" name="savecert" value="保存资质" onclick="save_member_cert();" />

                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'member_list.aspx';" />

                <input type="hidden" id="member_id" name="member_id" value="<%=Member_ID %>" />
                <input type="hidden" id="member_certtype" name="member_certtype" value="<%=Member_Type %>" />
            </form>

        </div>



    </div>
</body>
</html>
