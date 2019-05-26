<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html>

<script runat="server">
    private ITools tools;
    private Member myApp;
    private MemberGrade Mgrade;
    Addr Addr;
    Orders orderApp;

    private string Member_Grade_Name, Member_Cert;

    private string Member_Name, Member_StreetAddress, Member_Country, Member_State, Member_City, Member_County, Member_Zip, Member_Phone_Countrycode, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile, Member_RegIP;
    private int Member_Profile_ID, Member_Profile_MemberID, Member_Sex, Member_Occupational, Member_Education, Member_Income, Member_AuditStatus, Member_Cert_Status;

    private string Member_Email, Member_LoginMobile, Member_NickName, Member_Password, Member_VerifyCode, Member_LastLogin_IP, Member_Site, Member_Company, Member_Fax, Member_QQ, Member_OrganizationCode, Member_BusinessCode, Member_SealImg, Member_Corporate, Member_CorporateMobile, Member_TaxationCode, Member_BankAccountCode, Member_HeadImg;
    private int Member_ID, Member_Emailverify, Member_LoginMobileverify, Member_LoginCount, Member_CoinCount, Member_CoinRemain, Member_Trash, Member_Grade, Member_AllowSysEmail, Member_Status, Member_Type;
    private DateTime Member_LastLogin_Time, Member_Addtime, Member_Birthday;
    private double Member_Account, Member_Frozen, Member_RegisterFunds;

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

            relateinfos = entity.MemberRelateCertInfos;
            MemberProfileInfo ProfileInfo = entity.MemberProfileInfo;

            if (ProfileInfo != null)
            {
                Member_Profile_ID = ProfileInfo.Member_Profile_ID;
                Member_Profile_MemberID = ProfileInfo.Member_Profile_MemberID;
                Member_Name = ProfileInfo.Member_Name;
                Member_Sex = ProfileInfo.Member_Sex;
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
                Member_SealImg = ProfileInfo.Member_SealImg;
                Member_Corporate = ProfileInfo.Member_Corporate;
                Member_CorporateMobile = ProfileInfo.Member_CorporateMobile;
                Member_RegisterFunds = ProfileInfo.Member_RegisterFunds;
                Member_TaxationCode = ProfileInfo.Member_TaxationCode;
                Member_BankAccountCode = ProfileInfo.Member_BankAccountCode;
                Member_HeadImg = ProfileInfo.Member_HeadImg;
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
    <title>采购商资料修改</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/member.js" type="text/javascript"></script>
    <script src="/Scripts/layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">

        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">采购商资料修改</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">注册Email</td>
                                <td class="cell_content"><% =Member_Email%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">用户名</td>
                                <td class="cell_content"><% =Member_NickName%>
                                    <input type="hidden" id="member_id" name="member_id" value="<%=Member_ID %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">注册手机号</td>
                                <td class="cell_content"><% =Member_LoginMobile%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">用户头像</td>
                                <td class="cell_content">
                                    <input type="hidden" value="<%=Member_HeadImg %>" id="Member_HeadImg" name="Member_HeadImg" />
                                    <img id="img_Member_HeadImg" src="<%=Public.FormatImgURL(Member_HeadImg,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                    <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=headimg&formname=formadd&frmelement=Member_HeadImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_Admin"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>

                            <tr>
                                <td class="cell_title">公司名称</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_CompanyName" type="text" id="Member_Profile_CompanyName" style="width: 300px;" class="input01" maxlength="100" value="<%=Member_Company %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">联系人</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_Contactman" type="text" id="Member_Profile_Contactman" style="width: 300px;" class="input01" maxlength="50" value="<%=Member_Name%>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">联系电话</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_Phone" type="text" id="Member_Profile_Phone" style="width: 300px;" class="input01" maxlength="20" value="<%=Member_Phone_Number %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">联系人手机</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_Mobile" type="text" id="Member_Profile_Mobile" style="width: 300px;" class="input01" maxlength="11" value="<%=Member_LoginMobile %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">传真号码</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_Fax" type="text" id="Member_Profile_Fax" style="width: 300px;" class="input01" maxlength="20" value="<%=Member_Fax %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">QQ</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_QQ" type="text" id="Member_Profile_QQ" style="width: 300px;" class="input01" maxlength="20" value="<%=Member_QQ %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">所在地址</td>
                                <td class="cell_content" id="div_area">
                                    <%=Addr.SelectAddress("div_area", "Member_Profile_State", "Member_Profile_City", "Member_Profile_County", Member_State, Member_City, Member_County)%><span class="t12_red">*</span>
                                </td>
                                <td>
                                    <input type="hidden" id="Member_Profile_State" name="Member_Profile_State" value="<%=Member_State%>" />
                                    <input type="hidden" id="Member_Profile_City" name="Member_Profile_City" value="<%=Member_City%>" />
                                    <input type="hidden" id="Member_Profile_County" name="Member_Profile_County" value="<%=Member_County%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">街道地址</td>
                                <td class="cell_content">
                                    <input type="hidden" name="Member_Profile_Country" id="Member_Profile_Country" value="<%=Member_Country %>" />
                                    <input name="Member_Profile_Address" type="text" id="Member_Profile_Address" style="width: 300px;" class="input01" maxlength="100" value="<%=Member_StreetAddress %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">组织机构代码</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_OrganizationCode" type="text" id="Member_Profile_OrganizationCode" style="width: 300px;" class="input01" maxlength="20" value="<%=Member_OrganizationCode %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">营业执照代码</td>
                                <td class="cell_content">
                                    <input name="Member_Profile_BusinessCode" type="text" id="Member_Profile_BusinessCode" style="width: 300px;" class="input01" maxlength="20" value="<%=Member_BusinessCode %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">法定代表人姓名</td>
                                <td class="cell_content">
                                    <input name="Member_Corporate" type="text" id="Member_Corporate" style="width: 300px;" class="input01"
                                        maxlength="20" value="<%=Member_Corporate %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">法定代表人电话</td>
                                <td class="cell_content">
                                    <input name="Member_CorporateMobile" type="text" id="Member_CorporateMobile" style="width: 300px;" class="input01"
                                        maxlength="20" value="<%=Member_CorporateMobile %>" /><span class="t12_red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">注册资金</td>
                                <td class="cell_content">
                                    <input name="Member_RegisterFunds" type="text" id="Member_RegisterFunds" style="width: 300px;" class="input01"
                                        maxlength="20" value="<%=Member_RegisterFunds %>" />
                                    万元
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">税务登记证副本号</td>
                                <td class="cell_content">
                                    <input name="Member_TaxationCode" type="text" id="Member_TaxationCode" style="width: 300px;" class="input01"
                                        maxlength="20" value="<%=Member_TaxationCode %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">银行开户许可证号</td>
                                <td class="cell_content">
                                    <input name="Member_BankAccountCode" type="text" id="Member_BankAccountCode" style="width: 300px;" class="input01"
                                        maxlength="20" value="<%=Member_BankAccountCode %>" />
                                </td>
                            </tr>

                            <tr>
                                <td class="cell_title">公章图片</td>
                                <td class="cell_content">
                                    <input type="hidden" value="<%=Member_SealImg %>" id="Member_Profile_SealImg" name="Member_Profile_SealImg" />
                                    <img id="img_Member_Profile_SealImg" src="<%=Public.FormatImgURL(Member_SealImg,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                    <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=formadd&frmelement=Member_Profile_SealImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_Admin"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>

        <div class="foot_gapdiv">
        </div>
        <div class="float_option_div" id="float_option_div">
            <input type="button" id="save" class="bt_orange" name="save" value="保存" onclick="update_member_profile();" />
            <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'member_list.aspx';" />
        </div>
        ·
    </div>
</body>
</html>
