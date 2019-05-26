<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% Public.CheckLogin("40f51178-030c-402a-bee4-57ed6d1ca03f");
   Supplier Supplier = new Supplier();


   int Supplier_id;
   string Supplier_Bank_Name = "";
   string Supplier_Bank_NetWork = "";
   string Supplier_Bank_SName = "";
   string Supplier_Bank_Account = "";
   string Supplier_Bank_Attachment = "";
   string Supplier_Online_Code = "";

   string Supplier_Cert, Supplier_TempCreditLimit_ContractSN;
   int Supplier_Cert_Status, Supplier_CertType, Supplier_FavorMonth, Supplier_TempCreditLimit_Expires, Supplier_CreditLimit_Expires, Supplier_Type = 0;
   Supplier_CertType = 0;
   Supplier_FavorMonth = 0;
   Supplier_Cert_Status = 0;
   Supplier_CreditLimit_Expires = 0;
   Supplier_TempCreditLimit_Expires = 0;
   double Supplier_AgentRate = 0;
   double Supplier_CreditLimitRemain = 0, Supplier_CreditLimit = 0, Supplier_TempCreditLimit = 0, Supplier_TempCreditLimitRemain = 0;
   IList<SupplierRelateCertInfo> relateinfos = null;
   ITools tools;
   Addr addr = new Addr();
   tools = ToolsFactory.CreateTools();

   Supplier_id = tools.CheckInt(Request["Supplier_id"]);




   string Member_NickName = "";
   string Member_State = "";
   string Member_City = "";
   string Member_Company = "";
   string Member_Country = "";
   string Member_County = "";
   string Member_StreetAddress = "";
   string Member_Name = "";
   string Member_Phone_Number = "";

   string Member_Mobile = "";
   string Member_Email = "";
   string Member_QQ = "";
   string Member_Corporate = "";
   string Member_CorporateMobile = "";
   double Member_RegisterFunds = 0;
   string Member_TaxationCode = "";
   string Member_OrganizationCode = "";

   string Member_BankAccountCode = "";
   string Member_SealImg = "";
   string Member_Fax = "";
   string Member_Zip = "";
   int Member_SupplierID = 0;
   string Member_Company_Introduce = "";
   string Member_Company_Contact = "";
   string Member_RealName = "";
   string Member_UniformSocial_Number = "";
   Member member = new Member();


   Public pub = new Public();



   MemberInfo memberInfo = member.GetMemberByID(member.GetMemberID_BySupplierID(Supplier_id));
   MemberProfileInfo profileInfo = null;


   if (memberInfo != null)
   {
       Member_Email = memberInfo.Member_Email;
       profileInfo = memberInfo.MemberProfileInfo;
       Member_SupplierID = memberInfo.Member_SupplierID;
       Member_Company_Introduce = memberInfo.Member_Company_Introduce;
       Member_Company_Contact = memberInfo.Member_Company_Contact;
       if (profileInfo != null)
       {
           Member_NickName = memberInfo.Member_NickName;
           Member_Company = profileInfo.Member_Company;
           Member_Email = memberInfo.Member_Email;
           Member_State = profileInfo.Member_State;
           Member_City = profileInfo.Member_City;



           Member_Country = profileInfo.Member_Country;
           Member_County = profileInfo.Member_County;
           Member_StreetAddress = profileInfo.Member_StreetAddress;

           Member_Name = profileInfo.Member_Name;
           Member_Phone_Number = profileInfo.Member_Phone_Number;
           //Member_Mobile = profileInfo.Member_Mobile;
           Member_Mobile = memberInfo.Member_LoginMobile;



           Member_QQ = profileInfo.Member_QQ;

           Member_Corporate = profileInfo.Member_Corporate;
           Member_CorporateMobile = profileInfo.Member_CorporateMobile;
           Member_RegisterFunds = profileInfo.Member_RegisterFunds;
           Member_TaxationCode = profileInfo.Member_TaxationCode;
           Member_OrganizationCode = profileInfo.Member_OrganizationCode;
           Member_TaxationCode = profileInfo.Member_TaxationCode;
           Member_BankAccountCode = profileInfo.Member_BankAccountCode;

           Member_SealImg = profileInfo.Member_SealImg;
           Member_Fax = profileInfo.Member_Fax;
           Member_Zip = profileInfo.Member_Zip;
           Member_RealName = profileInfo.Member_RealName;
           Member_UniformSocial_Number = profileInfo.Member_UniformSocial_Number;



       }


   }


   SupplierInfo supplierinfo = Supplier.GetSupplierByID(Member_SupplierID);

   if (supplierinfo != null)
   {
       SupplierOnlineInfo SupplierOnline = Supplier.GetSupplierOnlineByID(supplierinfo.Supplier_ID);
       if (SupplierOnline != null)
       {
           Supplier_Online_Code = SupplierOnline.Supplier_Online_Code;
       }
       //IList<SupplierOnlineInfo> SupplierOnlines = Supplier.GetSupplierOnlinesByID(supplierinfo.Supplier_ID);
       //int i = 0;
       //if (SupplierOnlines != null)
       //{

       //    foreach (var SupplierOnline in SupplierOnlines)
       //    {
       //        i++;
       //        SupplierOnline.Supplier_Online_Code;

       //    } 
       //}
   }


   Supplier_TempCreditLimit_ContractSN = "";







   if (supplierinfo == null)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   else
   {
       Supplier_CertType = supplierinfo.Supplier_CertType;
       Supplier_Type = supplierinfo.Supplier_Type;
       relateinfos = supplierinfo.SupplierRelateCertInfos;
       Supplier_Cert_Status = supplierinfo.Supplier_Cert_Status;
       Supplier_FavorMonth = supplierinfo.Supplier_FavorMonth;
       Supplier_AgentRate = supplierinfo.Supplier_AgentRate;
       Supplier_CreditLimitRemain = supplierinfo.Supplier_CreditLimitRemain;
       Supplier_CreditLimit = supplierinfo.Supplier_CreditLimit;
       Supplier_TempCreditLimit = supplierinfo.Supplier_TempCreditLimit;
       Supplier_TempCreditLimitRemain = supplierinfo.Supplier_TempCreditLimitRemain;
       Supplier_CreditLimit_Expires = supplierinfo.Supplier_CreditLimit_Expires;
       Supplier_TempCreditLimit_Expires = supplierinfo.Supplier_TempCreditLimit_Expires;
       Supplier_TempCreditLimit_ContractSN = supplierinfo.Supplier_TempCreditLimit_ContractSN;
   }

   SupplierBankInfo entity1 = Supplier.GetSupplierBankInfoBySupplierID(Supplier_id);
   if (entity1 != null)
   {
       Supplier_Bank_Name = entity1.Supplier_Bank_Name;
       Supplier_Bank_NetWork = entity1.Supplier_Bank_NetWork;
       Supplier_Bank_SName = entity1.Supplier_Bank_SName;
       Supplier_Bank_Account = entity1.Supplier_Bank_Account;
       Supplier_Bank_Attachment = entity1.Supplier_Bank_Attachment;
   }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商信息</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChangeSupplierAuditStatus(obj) {
            $("#formadd").attr("action", "Supplier_Do.aspx?action=" + obj);
            document.formadd.submit();
        }

        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_Admin"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }


    </script>
    <script type="text/javascript">
        change_inputcss();
        btn_scroll_move();
    </script>
</head>
<body>
    <div class="content_div">
        <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Do.aspx">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">供应商信息</td>
                </tr>
                <tr>
                    <td class="content_content">

                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                            <tr>
                                <td class="cell_title">用户名</td>
                                <%--  <td class="cell_content"><%=supplierinfo.Supplier_CompanyName%> 【<%=supplierinfo.Supplier_Nickname%>】</td>--%>
                                <td class="cell_content"><%=Member_NickName%> 【<%=Member_Company%>】</td>
                               <%-- <td class="cell_title">注册邮箱</td>
                                <td class="cell_content"><%=Member_Email%></td>--%>
                               <%--  <td class="cell_title">公司名称</td>   
                                                            
                                <td class="cell_content" ><%=Member_Company%> </td>--%>

                                <td class="cell_title">公司名称</td>
                                <td class="cell_content">
                                    <input type="text" name="Member_Company" id="Text2" value="<%=Member_Company %>" /></td>
                               
                            </tr>
                           <%-- <tr>--%>
                                <%--<td class="cell_title">公司名称</td>                               
                                <td class="cell_content"><%=Member_Company%> </td>--%>
                               
                                <%--<td class="cell_title">所在地区</td>
                                <td class="cell_content" id="div_area"><%=addr.DisplayAddress(Member_State ,Member_City,  Member_County)%>
                                    <input type="hidden" id="Supplier_State" name="Supplier_State" value="<%=Member_State%>" />
                                    <input type="hidden" id="Supplier_City" name="Supplier_City" value="<%=Member_City%>" />
                                    <input type="hidden" id="Supplier_County" name="Supplier_County" value="<%=Member_County%>" />
                                </td>--%>
                          <%--  </tr>--%>
                            <tr>
                                <td class="cell_title">公司地址</td>
                                <td class="cell_content">
                                    <input type="hidden" name="Supplier_Country" id="Supplier_Country" value=" <%=Member_StreetAddress%>" />
                                    <input name="Supplier_Address" type="text" id="Supplier_Address" maxlength="100" value="<%=supplierinfo.Supplier_Address%>" /></td>
                                        <td class="cell_title">真实姓名</td>
                                <td class="cell_content">
                                    <input type="text" name="Member_RealName" id="Member_RealName" value="<%=Member_RealName %>" /></td>
                            </tr>
                            <tr>
                               
                                <td class="cell_title">手机号码</td>
                                <td class="cell_content">
                                    <input name="Supplier_Mobile" type="text" id="Supplier_Mobile" maxlength="11" value="<%=Member_Mobile%>" /></td>
                                 <td class="cell_title">常用联系人</td>
                                <td class="cell_content">
                                    <input name="Supplier_Contactman" type="text" id="Text1" maxlength="50" value="<%=Member_Name%>" /></td>
                            </tr>

                             <%-- <tr>
                                 <td class="cell_title">常用联系人</td>
                                <td class="cell_content">
                                    <input name="Supplier_Contactman" type="text" id="Supplier_Contactman" maxlength="50" value="<%=Member_Name%>" /></td>
                                <td class="cell_title">联系人邮箱</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_ContactEmail" id="Supplier_ContactEmail" value="<%=supplierinfo.Supplier_ContactEmail %>" /></td>
                            </tr>--%>

                            <tr>
                              
                       

                                 <td class="cell_title">常用联系人电话</td>
                                <td class="cell_content">
                                    <input name="Supplier_Phone" type="text" id="Supplier_Phone" maxlength="20" value="<%=Member_Phone_Number%>" /></td>
                                
                                <td class="cell_title">QQ号码</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_ContactQQ" id="Supplier_ContactQQ" value="<%=Member_QQ %>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">法人</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_Corporate" id="Supplier_Corporate" value="<%=Member_Corporate %>" /></td>
                              
                                <td class="cell_title">统一社会代码证号</td>
                                <td class="cell_content">
                                    <input type="text" name="Member_UniformSocial_Number" id="Member_UniformSocial_Number" value="<%=Member_UniformSocial_Number %>" /></td>
                                 <%-- <td class="cell_title">法定代表人电话</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_CorporateMobile" id="Supplier_CorporateMobile" value="<%=Member_CorporateMobile %>" /></td>--%>
                            </tr>
                           <%-- <tr>--%>
                                <%--<td class="cell_title">注册资金</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_RegisterFunds" id="Supplier_RegisterFunds" value="<%=Member_RegisterFunds %>" />
                                    万元</td>--%>
                                <%--<td class="cell_title">统一社会代码证号</td>
                                <td class="cell_content">
                                    <input type="text" name="Member_UniformSocial_Number" id="Member_UniformSocial_Number" value="<%=Member_UniformSocial_Number %>" /></td>--%>
                           <%-- </tr>--%>
                            <%--<tr>
                                <td class="cell_title">组织机构代码证副本号</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_OrganizationCode" id="Supplier_OrganizationCode" value="<%=Member_OrganizationCode%>" /></td>
                                <td class="cell_title">税务登记证副本号</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_TaxationCode" id="Supplier_TaxationCode" value="<%=Member_TaxationCode %>" /></td>
                            </tr>--%>
                         <tr>
                                

                            <%--       <td class="cell_title">是否注册品牌或商标</td>
                                <td class="cell_content">
                                    <input type="radio" id="Supplier_IsTrademark1" name="Supplier_IsTrademark" value="0" <%=Public.CheckedRadio(supplierinfo.Supplier_IsTrademark.ToString(), "0")%> />否
                                     <input type="radio" id="Supplier_IsTrademark2" name="Supplier_IsTrademark" value="1" <%=Public.CheckedRadio(supplierinfo.Supplier_IsTrademark.ToString(), "1")%> />是</td>
                                <%--  </tr>--%>
                             <%--   <td class="cell_title">是否授权品牌</td>
                                <td class="cell_content">
                                    <input type="radio" id="Supplier_IsAuthorize1" name="Supplier_IsAuthorize" value="0" <%=Public.CheckedRadio(supplierinfo.Supplier_IsAuthorize.ToString(), "0")%> />否
                                    <input type="radio" id="Supplier_IsAuthorize2" name="Supplier_IsAuthorize" value="1" <%=Public.CheckedRadio(supplierinfo.Supplier_IsAuthorize.ToString(), "1")%> />是</td>--%>
                            </tr>
                            <tr>
                                <td class="cell_title">商家QQ客服</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_Online_Code" value="<%=Supplier_Online_Code %>" /><span style="color:red;font-size:12px;">不需要修改,值为空</span></td>
                                <td class="cell_title">公司电话</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_ServicesPhone" id="Supplier_ServicesPhone" value="<%=supplierinfo.Supplier_ServicesPhone%>" /></td>
                            </tr>
                            <%--<tr>
                                <td class="cell_title">经营年限</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_OperateYear" id="Supplier_OperateYear" value="<%=supplierinfo.Supplier_OperateYear%>" /></td>
                                <td class="cell_title">销售模式</td>
                                <td class="cell_content">
                                    <input type="radio" id="Supplier_SaleType1" name="Supplier_SaleType" value="0" <%=Public.CheckedRadio(supplierinfo.Supplier_SaleType.ToString(), "0")%> />自销
                                    <input type="radio" id="Supplier_SaleType2" name="Supplier_SaleType" value="1" <%=Public.CheckedRadio(supplierinfo.Supplier_SaleType.ToString(), "1")%> />代销</td>
                            </tr>
                            <tr>
                                <td class="cell_title">订阅接收手机</td>
                                <td class="cell_content">
                                    <input name="Supplier_SysMobile" type="text" id="Supplier_SysMobile"
                                        maxlength="11" value="<%=supplierinfo.Supplier_SysMobile%>" /></td>
                                <td class="cell_title">订阅接收邮箱</td>
                                <td class="cell_content">
                                    <input name="Supplier_SysEmail" type="text" id="Supplier_SysEmail"
                                        maxlength="100" value="<%=supplierinfo.Supplier_SysEmail%>" /></td>
                            </tr>--%>
                            <%--<tr>
                                <td class="cell_title">传真</td>
                                <td class="cell_content">
                                    <input name="Supplier_Fax" type="text" id="Supplier_Fax"
                                        maxlength="20" value="<%=Member_Fax%>" /></td>
                                <td class="cell_title">账户信息</td>
                                <td class="cell_content">保证金：<%=Public.DisplayCurrency(supplierinfo.Supplier_Security_Account)%>  </td>
                            </tr>--%>
                          <%--  <tr>
                                <td class="cell_title">公章图片</td>
                                <td class="cell_content">
                                    <table border="0" width="30%" cellpadding="3" cellspacing="0" style="width: 300px; margin-left: 5px;">
                                        <tr>
                                            <td align="left" height="120">
                                                <input type="hidden" value="<%=supplierinfo.Supplier_SealImg%>" id="Supplier_SealImg" name="Supplier_SealImg" />
                                                <img id="img_Supplier_SealImg" src="<%=Public .FormatImgURL(Member_SealImg, "fullpath")%>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=formadd&frmelement=Supplier_SealImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_Admin"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>

                                            </td>
                                        </tr>
                                    </table>
                                    <span>*</span>
                                </td>
                                <td class="cell_title">注册时间</td>
                                <td class="cell_content"><%=supplierinfo.Supplier_Addtime %></td>
                            </tr>--%>
                          <%--  <%if (supplierinfo.Supplier_AuditStatus == 1)
                              { %>
                            <tr>
                                <td class="cell_title">代理费用比率</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_AgentRate" value="<%=Supplier_AgentRate %>" />% </td>

                                <td class="cell_title">运费模式</td>
                                <td class="cell_content"><input name="Supplier_DeliveryMode" type="radio" id="Supplier_DeliveryMode1" value="1" checked />
                                    单独计算</td>

                            </tr>
                            <%} %>--%>
                            <tr>
                                <td class="cell_title">供应商营销标签</td>
                                <td class="cell_content"><%=Supplier.Display_Supplier_Tag(Supplier_id)%></td>
                                <td class="cell_title">供应商状态</td>
                                <td class="cell_content">
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status" value="0" <% =Public.CheckedRadio(supplierinfo.Supplier_Status.ToString(), "0")%> />
                                    不启用
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status1" value="1" <% =Public.CheckedRadio(supplierinfo.Supplier_Status.ToString(), "1")%> />
                                    启用此账号
                                    <input name="Supplier_Status" type="radio" id="Supplier_Status2" value="2" <% =Public.CheckedRadio(supplierinfo.Supplier_Status.ToString(), "2")%> />
                                    冻结此账号</td>
                            </tr>
                            <tr>
                                <td class="cell_title">最后登录时间</td>
                                <td class="cell_content"><% =supplierinfo.Supplier_Lastlogintime%></td>

                                <td class="cell_title">登录次数</td>
                                <td class="cell_content"><%=supplierinfo.Supplier_LoginCount %></td>
                            </tr>
                            <tr>
                                <td class="cell_title">订单邮件提醒</td>
                                <td class="cell_content">
                                    <input name="Supplier_AllowOrderEmail" type="radio" id="Supplier_AllowOrderEmail" value="0" <% =Public.CheckedRadio(supplierinfo.Supplier_AllowOrderEmail.ToString(), "0")%> />
                                    不启用
                                    <input name="Supplier_AllowOrderEmail" type="radio" id="Supplier_AllowOrderEmail1" value="1" <% =Public.CheckedRadio(supplierinfo.Supplier_AllowOrderEmail.ToString(), "1")%> />
                                    启用</td>

                               <%-- <td class="cell_title">邮政编码</td>
                              
                                <td class="cell_content">
                                    <input type="text" name="Member_Zip" value="<%=Member_Zip %>" /></td>--%>

                            </tr>
                            <%--   </tr>--%>

                            <%--   <tr>
                                  <td class="cell_title">商家QQ客服</td>
                                <td class="cell_content">
                                    <input type="text" name="Supplier_Online_Code" value="<%=Supplier_Online_Code %>" /></td>
                               
                            </tr>--%>

                            <tr>
                                <td class="cell_title">公司介绍</td>
                                <td class="cell_content" colspan="3">
                                    <textarea rows="" cols="2" style="width: 688px; height: 98px;" name="Member_Company_Introduce"><% =Server.HtmlDecode(Member_Company_Introduce) %></textarea></td>
                            </tr>
                            <tr>
                                <td class="cell_title">公司联系方式</td>
                                <td class="cell_content" colspan="3">
                                    <textarea rows="" cols="2" style="width: 688px; height: 98px;" name="Member_Company_Contact"><% =Server.HtmlDecode(Member_Company_Contact) %></textarea></td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>


            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">资质信息</td>
                </tr>
                <tr>
                    <td class="content_content">

                        <div id="cert_show" style="position: absolute; border: 1px solid #000; display: none;"></div>
                        <div id="cert_compare"></div>
                        <script>
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
                                IList<SupplierCertInfo> certs = null;
                                certs = Supplier.GetSupplierCertByType(Supplier_CertType);
                                if (certs != null)
                                {
                                    Response.Write("<tr>");
                                    foreach (SupplierCertInfo certinfo in certs)
                                    {
                                        Supplier_Cert = Supplier.Get_Supplier_Cert(certinfo.Supplier_Cert_ID, relateinfos);
                            %>
                            <td width="<%=100/certs.Count %>%" align="center">
                                <table border="0" cellpadding="3" cellspacing="0" width="<%=100/certs.Count %>%">
                                    <tr>
                                        <%--    <td align="center">
                                         <img id="img_supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp" name="img_supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp" src="<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>" width="200" height="200"
                                                onmouseover="show_cert('<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>');"
                                                onmouseout="$('#cert_show').hide();" />
                                        </td>--%>

                                        <td align="center">
                                            <a href="Supplier_Cert_Pic.aspx?Supplier_ID=<%=supplierinfo.Supplier_ID%>&&Cert_ID=<%=certinfo.Supplier_Cert_ID %>" target="_blank">
                                                <img id="img_supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp" name="img_supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp" src="<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>" width="200" height="200" />
                                            </a>

                                        </td>


                                    </tr>
                                    <tr>
                                        <td align="center" height="30">
                                            <input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript:openUpload('supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp');" />
                                            <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript:delImage('supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp');">
                                            <input type="hidden" name="supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp" id="supplier_cert<%=certinfo.Supplier_Cert_ID %>_tmp"
                                                value="<%=Supplier_Cert %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center"><%=certinfo.Supplier_Cert_Name %></td>
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
                    </td>
                </tr>


            </table>

            <div class="foot_gapdiv">
            </div>
            <div class="float_option_div" id="float_option_div">
                <%if (supplierinfo.Supplier_AuditStatus == 0 && Public.CheckPrivilege("d8de7f81-7e9a-44ea-9463-dd1afda2b74e"))
                  { %>
                <input type="button" name="Audit" class="bt_orange" value="审核通过" onclick="ChangeSupplierAuditStatus('passaudit');" />
                <input type="button" name="NotAudit" class="bt_orange" value="审核不通过" onclick="ChangeSupplierAuditStatus('notpassaudit');" />

                <%} if (supplierinfo.Supplier_AuditStatus == 1)
                  { %>
                <input type="hidden" id="working" name="action" value="renew" />

                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                <%} if (supplierinfo.Supplier_AuditStatus == 2 && Public.CheckPrivilege("d8de7f81-7e9a-44ea-9463-dd1afda2b74e"))
                  { %>
                <input type="button" name="Audit" class="bt_orange" value="审核通过" onclick="ChangeSupplierAuditStatus('passaudit');" />
                <%} %>
                <input type="hidden" id="Supplier_id" name="Supplier_id" value="<%=Supplier_id %>" />
                <input type="hidden" id="Supplier_type" name="Supplier_type" value="<%=Supplier_Type %>" />
                <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_list.aspx';" />
            </div>
        </form>

    </div>
</body>
</html>
