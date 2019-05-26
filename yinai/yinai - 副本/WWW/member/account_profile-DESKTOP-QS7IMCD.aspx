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
    Supplier supplier = new Supplier();
    SupplierInfo Supplierinfo = null;
    int Supplier_AuditStatus = 0;
    int Supplier_Cert_Status;
    string Member_QQ = "";
    //SupplierInfo Suppliernfo = supplier.GetSupplierByID(tools.CheckInt(Session["member_id"].ToString()));
    IList<SupplierRelateCertInfo> relateinfos = null;

    string Supplier_Cert = "";
    //资质
    Supplier_Cert_Status = 1;
    //Supplier_CertType = 0;

    MemberInfo memberInfo = member.GetMemberByID();

    IList<SupplierCertInfo> certs = supplier.GetSupplierCertByType(0);
    if (memberInfo != null)
    {
        Supplierinfo = supplier.GetSupplierByID(memberInfo.Member_SupplierID);
        if (Supplierinfo != null)
        {
            Supplier_AuditStatus = Supplierinfo.Supplier_AuditStatus;
            relateinfos = Supplierinfo.SupplierRelateCertInfos;
            Supplier_AuditStatus = Supplierinfo.Supplier_Cert_Status;
        }
    }


    MemberProfileInfo profileInfo = null;
    SupplierInfo supplierinfo = null;
    if (memberInfo == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    else
    {
        supplierinfo = supplier.GetSupplierByID();
        profileInfo = memberInfo.MemberProfileInfo;

        if (profileInfo == null)
        {
            Response.Redirect("/member/index.aspx");
        }
        else
        {
            Member_QQ = profileInfo.Member_QQ;
        }
    }
    MemberSubAccountInfo subaccountinfo = member.GetSubAccountByID();
    member.Member_Login_Check("/member/account_invoice.aspx");

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title><%="账户信息 - 我是买家 - " + pub.SEO_TITLE()%></title>

    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <%--  <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <%--    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>

    <script type="text/javascript">
        function openSealUpload(openObj) {
            $("#td_sealupload").show();
            $("#iframe_sealupload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=frm_account_profile&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }
        function delSealImage(openObj) {
            $("#img_" + openObj).attr("src", "/images/detail_no_pic.gif");
            $("#" + openObj).val("/images/detail_no_pic.gif");
        }

    </script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
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
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=frm_account_profile&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>

    <!--示范一个公告层 开始-->
    <script type="text/javascript">
        function SignUpNow() {
            layer.open({
                type: 2
   , title: false //不显示标题栏
                //, closeBtn: false
   , area: ['480px;', '340px']

   , shade: 0.8
   , id: 'LAY_layuipro' //设定一个id，防止重复弹出
   , resize: false
   , btnAlign: 'c'
   , moveType: 1 //拖拽模式，0或者1              
                , content: ("/Bid/SignUpPopup.aspx")
            });
        }
    </script>
    <!--示范一个公告层 结束-->
    <style type="text/css">
        .input01 {
            background-image: none;
            padding-left: 10px;
            width: 252px;
        }


        .ke-container ke-container-default .ke-edit {
            display: block;
            height: 255px;
        }

        .b07_info04 table td.name {
            padding: 10px;
            text-align:left;
            padding-left:35%;
        }

        td {
            text-align: center;
        }
    </style>
    <!--弹出菜单 end-->

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>资料管理</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1">
                <div class="pd_left">
                    <%--   <div class="menu_1">--%>
                    <% =member.Member_Left_HTML(5, 4) %>
                    <%--   </div>--%>
                </div>

                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <%if (Supplierinfo.Supplier_AuditStatus != 0)
                          {
                          
                        %>
                        <h2><span><a href="/member/account_profile_edit.aspx" class="a13" style="float: right; margin-right: 15px; color: #ffffff; font-weight: normal; font-size: 12px;">修改</a></span>账户信息</h2>

                        <%}
                          else
                          { %>

                        <h2>账户信息</h2>
                        <%} %>

                        <div class="blk07_sz">

                            <div class="b07_main_sz">
                                <div class="b07_info04">
                                    <form name="frm_account_profile" id="frm_account_profile" method="post" action="/member/account_do.aspx">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">

                                            <%--    未审核或者审核失败 --%>

                                            <%if (Supplierinfo.Supplier_AuditStatus != 1)
                                              {                                                
                                            %>
                                            <tr>
                                                <td width="148" class="name">用户名
                                                </td>
                                                <td><span id="Member_NickName" name="Member_NickName">
                                                    <%=memberInfo.Member_NickName%></span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="148" class="name">真实姓名
                                                </td>
                                                <td>
                                                    <%=profileInfo.Member_RealName%>
                                                </td>
                                            </tr>




                                            <tr>
                                                <td width="148" class="name">手机号码
                                                </td>
                                                <td>
                                                    <%=memberInfo.Member_LoginMobile%>                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="148" class="name">QQ号码
                                                </td>
                                                <td>

                                                    <input name="Member_Profile_QQ" type="text" id="Member_Profile_QQ" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_QQ%>" /><i>*</i>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td width="148" class="name">常用联系人
                                                </td>
                                                <td>
                                                    <input name="Member_Profile_Contactman" type="text" id="Member_Profile_Contactman" style="width: 258px;" class="input01" maxlength="50" value="<%=profileInfo.Member_Name%>" />
                                                </td>
                                            </tr>

                                            <%--<tr>
                                                <td width="148" class="name">Email
                                                </td>
                                                <td>

                                                    <input name="Member_Email" type="text" id="Text1" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=memberInfo.Member_Email%>" />
                                                </td>
                                            </tr>--%>

                                            <tr>
                                                <td width="148" class="name">常用联系人电话
                                                </td>
                                                <td>
                                                    <input name="Member_Profile_Phone" type="text" id="Member_Profile_Phone" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Phone_Number%>" />
                                                </td>
                                            </tr>


                                            <%-- <tr>
                                                <td width="168" class="name">联系人邮箱</td>
                                                <td>
                                                    <input type="text" name="Supplier_ContactEmail" id="Supplier_ContactEmail" style="width: 258px;" class="input01" value="<%=supplierinfo.Supplier_ContactEmail%>" />
                                                </td>
                                            </tr>--%>


                                            <%-- <tr>
                                                <td width="148" class="name">联系人QQ
                                                </td>
                                                <td>

                                                    <input name="Member_Profile_QQ" type="text" id="Member_Profile_QQ" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_QQ%>" />
                                                </td>
                                            </tr>--%>

                                            <%-- <tr>
                                                <td width="148" class="name">传真号码
                                                </td>
                                                <td>

                                                    <input name="Member_Profile_Fax" type="text" id="Member_Profile_Fax" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Fax%>" />
                                                </td>
                                            </tr>




                                            <tr>
                                                <td width="148" class="name">所在地
                                                <input type="hidden" id="Member_Profile_State" name="Member_Profile_State" value="<%=profileInfo.Member_State%>" />
                                                    <input type="hidden" id="Member_Profile_City" name="Member_Profile_City" value="<%=profileInfo.Member_City%>" />
                                                    <input type="hidden" id="Member_Profile_County" name="Member_Profile_County" value="<%=profileInfo.Member_County%>" />
                                                </td>
                                                <td id="div_area" style="width: 228px;">
                                                    <%=addr.SelectAddressNew("div_area", "Member_Profile_State", "Member_Profile_City", "Member_Profile_County", profileInfo.Member_State, profileInfo.Member_City, profileInfo.Member_County)%>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td width="148" class="name">公司名称
                                                </td>
                                                <td align="left">
                                                    <%=profileInfo.Member_Company%>                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="148" class="name">公司地址</td>
                                                <td>
                                                    <input type="hidden" name="Member_Profile_Country" id="Member_Profile_Country" value="<%=profileInfo.Member_Country%>" /><input
                                                        name="Member_Profile_Address" type="text" id="Member_Profile_Address"
                                                        style="width: 258px;" class="input01" maxlength="100" value="<%=profileInfo.Member_StreetAddress%>" /><i>*</i>
                                                </td>
                                            </tr>

                                            <%--                                            <tr>

                                                <td width="148" class="name">邮政编码
                                                </td>
                                                <td>
                                                    
                                                    <input name="Member_Profile_Zip" type="text" id="Member_Profile_Zip" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Zip%>" />
                                                </td>
                                            </tr>--%>


                                            <tr>
                                                <td width="148" class="name">法人
                                                </td>
                                                <td>
                                                    <input name="Member_Corporate" type="text" id="Member_Corporate" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Corporate%>" /><i>*</i>
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                                <td width="148" class="name">法定代表人电话
                                                </td>
                                                <td>
                                                    <input name="Member_CorporateMobile" type="text" id="Member_CorporateMobile" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_CorporateMobile%>" /><i>*</i>
                                                </td>
                                            </tr>--%>
                                            <%--  客服电话改为公司电话--%>
                                            <tr>
                                                <td width="168" class="name">公司电话</td>
                                                <td>
                                                    <input type="text" name="Supplier_ServicesPhone" id="Supplier_ServicesPhone" style="width: 258px;" class="input01" value="<%=supplierinfo.Supplier_ServicesPhone%>" />
                                                    <i>*</i> </td>
                                            </tr>

                                            <tr>
                                                <td width="148" class="name">统一社会代码证号
                                                </td>
                                                <td>
                                                    <input name="Member_UniformSocial_Number" type="text" id="Member_UniformSocial_Number" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_UniformSocial_Number%>" />
                                                    <i>*</i>
                                                </td>
                                                <td><span style="padding-left: 10px; color: red; font-size: 12px; display: block;">未办理三证合一的填写营业执照的本照</span></td>
                                            </tr>

                                            <%-- <tr>
                                                <td width="128" class="name">注册资金
                                                </td>
                                                <td>
                                                    <input name="Member_RegisterFunds" type="text" id="Member_RegisterFunds" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_RegisterFunds%>" />
                                                    万元<i>*</i>
                                                </td>
                                            </tr>--%>

                                            <%-- <tr>
                                                <td width="168" class="name">是否授权品牌</td>
                                                <td>
                                                    <input type="radio" id="Supplier_IsAuthorize1" name="Supplier_IsAuthorize" value="0" <%=pub.CheckedRadio(supplierinfo.Supplier_IsAuthorize.ToString(), "0")%> />否
                                    <input type="radio" id="Supplier_IsAuthorize2" name="Supplier_IsAuthorize" value="1" <%=pub.CheckedRadio(supplierinfo.Supplier_IsAuthorize.ToString(), "1")%> />是
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">是否注册品牌或商标</td>
                                                <td>
                                                    <input type="radio" id="Supplier_IsTrademark1" name="Supplier_IsTrademark" value="0" <%=pub.CheckedRadio(supplierinfo.Supplier_IsTrademark.ToString(), "0")%> />否
                                     <input type="radio" id="Supplier_IsTrademark2" name="Supplier_IsTrademark" value="1" <%=pub.CheckedRadio(supplierinfo.Supplier_IsTrademark.ToString(), "1")%> />是
                                                </td>
                                            </tr>--%>





                                            <%-- <tr>
                                                <td width="168" class="name">经营年限</td>
                                                <td>
                                                    <input type="text" name="Supplier_OperateYear" id="Supplier_OperateYear" style="width: 258px;" class="input01" value="<%=supplierinfo.Supplier_OperateYear%>" /></td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">销售模式</td>
                                                <td>
                                                    <input type="radio" id="Supplier_SaleType1" name="Supplier_SaleType" value="0" <%=pub.CheckedRadio(supplierinfo.Supplier_SaleType.ToString(), "0")%> />自销
                                    <input type="radio" id="Supplier_SaleType2" name="Supplier_SaleType" value="1" <%=pub.CheckedRadio(supplierinfo.Supplier_SaleType.ToString(), "1")%> />代销
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">订阅接收手机
                                                </td>
                                                <td width="771">
                                                    <input name="Supplier_SysMobile" type="text" id="Supplier_SysMobile"
                                                        style="width: 258px;" class="input01" maxlength="11" value="<%=supplierinfo.Supplier_SysMobile%>" /><i>*</i>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">订阅接收邮箱
                                                </td>
                                                <td width="771">
                                                    <input name="Supplier_SysEmail" type="text" id="Supplier_SysEmail"
                                                        style="width: 258px;" class="input01" maxlength="100" value="<%=supplierinfo.Supplier_SysEmail%>" /><i>*</i>
                                                </td>
                                            </tr>





                                            <tr>
                                                <td width="148" class="name">公章图片
                                                </td>
                                                <td>
                                                    <div class="b07_main">
                                                        <table border="0" width="30%" cellpadding="3" cellspacing="0" style="width: 321px; margin-left: 5px;">
                                                            <tr>
                                                                <td align="center" height="90">
                                                                    <input type="hidden" value="<%=profileInfo.Member_SealImg%>" id="Member_Profile_SealImg" name="Member_Profile_SealImg" />
                                                                    <img id="img_Member_Profile_SealImg" src="<%=pub.FormatImgURL(profileInfo.Member_SealImg, "fullpath")%>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 20px;">
                                                                <td>
                                                                    <iframe id="iframe_saleimg" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=frm_account_profile&frmelement=Member_Profile_SealImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                                                                </td>

                                                                <td style="color: #ff0000">*</td>
                                                            </tr>
                                                        </table>

                                                    </div>

                                                </td>

                                            </tr>--%>

                                            <input type="hidden" name="Supplier_CertType" value="0" />
                                            <tr>
                                                <%foreach (SupplierCertInfo entity in certs)
                                                  {
                                                      Supplier_Cert = supplier.Get_Supplier_Certtmp(entity.Supplier_Cert_ID, relateinfos);
                                                %>
                                                <td width="<%=(100 / certs.Count)%>%" style="text-align: center; margin-left: 38px;">
                                                    <table border="0" cellpadding="3" cellspacing="0" style="margin: 0 auto; width: <%=(100 / certs.Count)%>%; text-align: center;">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_supplier_cert<%=entity.Supplier_Cert_ID%>_tmp" name="img_supplier_cert<%=entity.Supplier_Cert_ID%>_tmp"
                                                                    src="<%=pub.FormatImgURL(Supplier_Cert, "fullpath")%>" width="120" height="120"
                                                                    onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <%if (Supplier_Cert_Status != 2)
                                                          { %>
                                                        <tr>
                                                            <td align="center" height="30" style="margin-top: 150px;">

                                                                <input type="hidden" name="supplier_cert<%=entity.Supplier_Cert_ID%>_tmp" id="supplier_cert<%=entity.Supplier_Cert_ID%>_tmp"
                                                                    value="<%=Supplier_Cert%>" />

                                                                <iframe style="margin: 40px 0px 0 15px;" id="iframe_upload<%=entity.Supplier_Cert_ID%>" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=frm_account_profile&frmelement=supplier_cert<%=entity.Supplier_Cert_ID%>_tmp&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe>
                                                            </td>
                                                        </tr>
                                                        <%} %>
                                                        <tr>
                                                            <td align="center">
                                                                <%=entity.Supplier_Cert_Name%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%} %>
                                            </tr>


                                            <tr id="td_upload" style="display: none; text-align: left;">
                                                <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="100%" height="90" frameborder="0" scrolling="no"
                                                    align="absmiddle"></iframe>
                                                </td>
                                            </tr>





                                            <tr>
                                                <td width="148"></td>
                                                <td>
                                                    <input name="action" type="hidden" id="action" value="account_profile">
                                                    <input name="hidden_type" id="hidden_type" type="hidden" value="save" />

                                                    <a href="javascript:void(0);" onclick="$('#hidden_type').val('saveandaudit');$('#frm_account_profile').submit();" class="a11" style="display: inline-block; margin-left: 15px; background-image: url(../images/save_buttom.jpg);"></a>
                                                </td>
                                            </tr>
                                            <%} %>

                                            <%--   资质未审核 0--%>
                                            <%else
                                              { %>



                                            <tr>
                                                <td width="148" colspan="2"  class="name">用户名
                                              <span id="Span1" name="Member_NickName">
                                                    <%=memberInfo.Member_NickName%></span>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td width="148" colspan="2" class="name">真实姓名
                                               <span id="Member_RealName" name="Member_RealName">
                                                    <%=profileInfo.Member_RealName%></span>
                                                </td>
                                            </tr>

                                            <%--<tr>
                                                <td width="148" class="name">Email
                                                </td>
                                                <td>
                                                    <%=memberInfo.Member_Email%>                                                
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td width="148" colspan="2" class="name">手机号码
                                             
                                                    <%=memberInfo.Member_LoginMobile%>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="148" colspan="2" class="name">QQ号码
                                           
                                                    <%=profileInfo.Member_QQ%>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td width="148" colspan="2" class="name">常用联系人
                                              
                                                    <%=profileInfo.Member_Name%>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td width="168" class="name">联系人邮箱</td>
                                                <td>
                                                    <%=supplierinfo.Supplier_ContactEmail%>
                                                </td>
                                            </tr>--%>



                                            <tr>
                                                <td width="148" colspan="2" class="name">常用联系人电话
                                        
                                                    <%=profileInfo.Member_Phone_Number%>
                                                </td>
                                            </tr>




                                            <%--   <tr>
                                                <td width="148" class="name">传真号码
                                                </td>
                                                <td>
                                                    <%=profileInfo.Member_Fax%>
                                                </td>
                                            </tr>--%>




                                            <%-- <tr>
                                                <td width="148" class="name">所在地
                                                <input type="hidden" id="Hidden2" name="Member_Profile_State" value="<%=profileInfo.Member_State%>" />
                                                    <input type="hidden" id="Hidden3" name="Member_Profile_City" value="<%=profileInfo.Member_City%>" />
                                                    <input type="hidden" id="Hidden4" name="Member_Profile_County" value="<%=profileInfo.Member_County%>" />
                                                </td>
                                                <td>

                                                    <%=addr.DisplayAddresses(profileInfo.Member_State, profileInfo.Member_State, profileInfo.Member_State)%>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td width="148" colspan="2" class="name">公司名称
                                           
                                                    <%=profileInfo.Member_Company%>                                             
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="148" colspan="2" class="name">公司地址
                                                    <%=profileInfo.Member_StreetAddress%>
                                                </td>
                                            </tr>

                                            <%--<tr>

                                                <td width="148" class="name">邮政编码
                                                </td>
                                                <td>
                                                    <%=profileInfo.Member_Zip%>
                                                </td>
                                            </tr>--%>

                                            <tr>
                                                <td width="148" colspan="2" class="name">法人
                                          
                                                    <%=profileInfo.Member_Corporate%>
                                                </td>
                                            </tr>

                                            <%--  <tr>
                                                <td width="148" class="name">法定代表人电话
                                                </td>
                                                <td>
                                                    <%=profileInfo.Member_CorporateMobile%>
                                                </td>
                                            </tr>--%>
                                            <%--  客服电话改为公司电话--%>
                                            <tr>

                                                <td width="168" colspan="2" class="name">公司电话
                                                    <%=supplierinfo.Supplier_ServicesPhone%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="148" colspan="2" class="name">统一社会代码证号
                                             
                                                    <%=profileInfo.Member_UniformSocial_Number%>


                                                    <span style="color: red; font-size: 12px; display: block;">未办理三证合一的填写营业执照的本照</span>
                                                </td>
                                            </tr>

                                            <%--  <tr>
                                                <td width="148" class="name">注册资金
                                                </td>
                                                <td>
                                                    <%=profileInfo.Member_RegisterFunds%>  万元
                                                </td>
                                            </tr>




                                            <tr>
                                                <td width="168" class="name">是否授权品牌</td>

                                                <%if (supplierinfo.Supplier_IsAuthorize == 1)
                                                  { %>
                                                <td>是</td>
                                                <%}
                                                  else
                                                  { %><td>否</td>
                                                <%} %>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">是否注册品牌或商标</td>
                                                <%if (supplierinfo.Supplier_IsTrademark == 1)
                                                  { %>
                                                <td>是</td>
                                                <%}
                                                  else
                                                  { %><td>否</td>
                                                <%} %>
                                            </tr>



                                            <tr>
                                                <td width="168" class="name">客服电话</td>
                                                <td>
                                                    <%=supplierinfo.Supplier_ServicesPhone%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">经营年限</td>
                                                <td>
                                                    <%=supplierinfo.Supplier_OperateYear%></td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">销售模式</td>
                                                <%if (supplierinfo.Supplier_IsTrademark == 1)
                                                  { %>
                                                <td>代销</td>
                                                <%}
                                                  else
                                                  { %><td>自销</td>
                                                <%} %>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">订阅接收手机
                                                </td>
                                                <td width="771">
                                                    <%=supplierinfo.Supplier_SysMobile%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="168" class="name">订阅接收邮箱
                                                </td>
                                                <td width="771">
                                                    <%=supplierinfo.Supplier_SysEmail%>
                                                </td>
                                            </tr>



                                            <tr>
                                                <td width="148" class="name">公章图片
                                                </td>
                                                <td>
                                                    <div class="b07_main">
                                                        <table border="0" width="30%" cellpadding="3" cellspacing="0" style="width: 321px; margin-left: 5px;">
                                                            <tr>
                                                                <td align="center" height="90">
                                                                    <input type="hidden" value="<%=profileInfo.Member_SealImg%>" id="Hidden1" name="Member_Profile_SealImg" />
                                                                    <img id="img1" src="<%=pub.FormatImgURL(profileInfo.Member_SealImg, "fullpath")%>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>

                                                </td>

                                            </tr>--%>




                                            <tr>
                                                <%foreach (SupplierCertInfo entity in certs)
                                                  {
                                                      Supplier_Cert = supplier.Get_Supplier_Certtmp(entity.Supplier_Cert_ID, relateinfos);
                                                %>
                                                <td width="<%=(100 / certs.Count)%>%" style="text-align: center; margin-left: 38px;">
                                                    <table border="0" cellpadding="3" cellspacing="0" style="margin: 0 auto; width: <%=(100 / certs.Count)%>%; text-align: center;">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img2" name="img_supplier_cert<%=entity.Supplier_Cert_ID%>_tmp"
                                                                    src="<%=pub.FormatImgURL(Supplier_Cert, "fullpath")%>" width="120" height="120"
                                                                    onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="center">
                                                                <%=entity.Supplier_Cert_Name%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%} %>
                                            </tr>





                                            <%} %>
                                        </table>


                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <input type="hidden" name="Supplier_CertType" value="0" />
            <table width="893" border="0" cellspacing="0" cellpadding="2" id="Table1" class="table_padding_5">
            </table>


            <div class="clear">
            </div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>
            <li>
                <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                            <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px; height: 50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
