<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% Public.CheckLogin("40f51178-030c-402a-bee4-57ed6d1ca03f");
   Supplier Supplier = new Supplier();


   int Supplier_id;
   int Supplier_ID;
   int Cert_ID;
   string Supplier_Bank_Name = "";
   string Supplier_Bank_NetWork = "";
   string Supplier_Bank_SName = "";
   string Supplier_Bank_Account = "";
   string Supplier_Bank_Attachment = "";
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

   Supplier_ID = tools.CheckInt(Request["Supplier_ID"]);

   Cert_ID = tools.CheckInt(Request["Cert_ID"]);


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
   string Supplier_CompanyName="";
   Member member = new Member();


   Public pub = new Public();


   
  MemberInfo memberInfo = member.GetMemberByID(member.GetMemberID_BySupplierID(Supplier_id));
   MemberProfileInfo profileInfo = null;


   if (memberInfo != null)
   {
       Member_Email = memberInfo.Member_Email;
       profileInfo = memberInfo.MemberProfileInfo;
       Member_SupplierID = memberInfo.Member_SupplierID;
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
           Member_Mobile = profileInfo.Member_Mobile;



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
          Member_Zip= profileInfo.Member_Zip;


       }


   }


   SupplierInfo supplierinfo = Supplier.GetSupplierByID(Member_SupplierID);
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
      Supplier_CompanyName= supplierinfo.Supplier_CompanyName;
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


   Supplier_Cert = Supplier.Get_Supplier_Cert(Cert_ID, relateinfos); 
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商信息</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
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
                <%
                    //IList<SupplierCertInfo> certs = null;
                   //             certs = Supplier.GetSupplierCertByType(Supplier_CertType);
                    SupplierCertInfo cert = null;
                    cert = Supplier.GetSupplierCertByID(Cert_ID);
                    %>
                <tr>
                    <td class="content_title"><span style="font-size:16px;"><%=Supplier_CompanyName%></span> --- <span style="font-size:13px;"><%=cert.Supplier_Cert_Name %></span></td>
                </tr>
               <tr><td align="center">
                                           
                                         <img id="img_supplier_cert<%=Cert_ID %>_tmp" name="img_supplier_cert<%=Cert_ID %>_tmp" src="<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>" width="700" height="700" />
                                          

                                        </td></tr>
            </table>


       

            <div class="foot_gapdiv">
            </div>
          
        </form>

    </div>
</body>
</html>
