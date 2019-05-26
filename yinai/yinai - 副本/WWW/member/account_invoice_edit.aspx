<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member myApp = new Member();
    myApp.Member_Login_Check("/member/account_invoice_edit.aspx");

    int invoice_id = tools.CheckInt(Request["invoice_id"]);
    if (invoice_id == 0)
    {
        Response.Redirect("/supplier/account_invoice.aspx");
    }
    MemberInvoiceInfo IEntity = myApp.GetMemberInvoiceByID(invoice_id);
    if (IEntity == null)
    {
        Response.Redirect("account_invoice.aspx");
    }
    else
    {
        if(IEntity.Invoice_MemberID!=tools.NullInt(Session["member_id"]))
        {
            Response.Redirect("account_invoice.aspx");
        }
    }
    int Invoice_Type = IEntity.Invoice_Type;
    string Invoice_Title = IEntity.Invoice_Title;
    if (Request["Type"] == "0" || Request["Type"] == "1")
    {
        Invoice_Type = tools.CheckInt(Request["Type"]);
    }
    if (tools.CheckStr(Request["Title"]) == "个人" || tools.CheckStr(Request["Title"]) == "单位")
    {
        Invoice_Title = tools.CheckStr(Request["Title"]);
    }   

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="修改发票 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <style type="text/css">
    .zkw_title21{margin-bottom:10px;}
    </style>
    <script type="text/javascript">
        function GetInvoiceType(ID) {
            var type = $("#" + ID).val();
            window.location = "account_invoice_edit.aspx?Type=" + type + "&invoice_id=<%=invoice_id %>";
        }
        function GetInvoiceTitle(ID) {
            var Title = $("#" + ID).val();
            window.location = "account_invoice_edit.aspx?Type=0&Title=" + Title + "&invoice_id=<%=invoice_id %>";
        }
        function check_member_IsBlank(object) {
            $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkisblank&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function check_ZipCode(object) {
            $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkzip&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function check_Invoice_all() {
            $.ajaxSetup({ async: false });
            var check_1 = false;
            var check_2 = false;
            var check_3 = false;
            var check_4 = false;
            check_1 = check_member_IsBlank('Invoice_Address');
            check_2 = check_member_IsBlank('Invoice_Name');
            check_3 = check_ZipCode('Invoice_ZipCode');
            check_4 = check_member_IsBlank('Invoice_Tel');

            if (check_1 && check_2 && check_3 && check_4) {
                return true;
            }
            else {
                return false;
            }
        }
</script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/index.aspx">首页</a> > <a href="/member/index.aspx">我是买家</a> > <span>修改发票</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                <% myApp.Member_Left_HTML(4, 6); %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>
                        修改发票</h2>
                    <div class="main">
                        <div class="zkw_order">
                            <form name="frm_account_profile" id="frm_account_profile" action="account_Invoice_do.aspx" method="post" onsubmit="return check_Invoice_all();">
                            <table width="100%" border="0" cellpadding="5" cellspacing="0"  class="table_padding_5">
                    
                    <tr>
                        <td align="right" class="frm_left" width="116" >
                            发票类型
                        </td>
                        <td class="frm_right">
                            <input id="Invoice_type" name="Invoice_type" onclick="GetInvoiceType('Invoice_type');" type="radio" value="0" <%=pub.CheckRadio("0", Invoice_Type.ToString())%> /> 普通发票&nbsp; 
                            <input id="Invoice_type1" name="Invoice_type" onclick="GetInvoiceType('Invoice_type1');" type="radio" value="1" <%=pub.CheckRadio("1", Invoice_Type.ToString())%> /> 增值税发票
                            <span class="t12_red">*</span>
                        </td>
                    </tr>
                    <%if (Invoice_Type == 0)
                      { %>
                    <tr>
                        <td align="right" class="frm_left">
                            发票抬头
                        </td>
                        <td class="frm_right">
                            <input id="Invoice_Title" name="Invoice_Title" type="radio" value="个人" onclick="GetInvoiceTitle('Invoice_Title');" <%=pub.CheckRadio("个人", Invoice_Title)%> /> 个人&nbsp; 
                            <input id="Invoice_Title1" name="Invoice_Title" type="radio" value="单位" onclick="GetInvoiceTitle('Invoice_Title1');" <%=pub.CheckRadio("单位", Invoice_Title)%> /> 单位
                            <span class="t12_red">*</span>
                        </td>
                    </tr>
                    <%if (Invoice_Title == "单位")
                      { %>
                    <tr>
                        <td align="right" class="frm_left">
                            单位名称
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_FirmName" class="txt_border" type="text" id="Invoice_FirmName" size="25" maxlength="100" value="<%=IEntity.Invoice_FirmName %>"/>
                            <span class="t12_red">*</span>
                            <span id="Invoice_FirmName_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right" class="frm_left">
                            单位地址
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_RegAddr" class="txt_border" type="text" id="Text6" size="25" maxlength="100" value="<%=IEntity.Invoice_VAT_RegAddr %>"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            单位电话
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_RegTel" class="txt_border" type="text" id="Text7" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_RegTel %>" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            开户银行
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_Bank" class="txt_border" type="text" id="Text8" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_Bank %>"/>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            银行账户
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_BankAccount" class="txt_border" type="text" id="Text9" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_BankAccount %>" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            税号
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_Code" class="txt_border" type="text" id="Text5" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_Code %>" />
                            
                        </td>
                    </tr>
                    <%}
                      else if (Invoice_Title == "个人")
                      { %>
                    <tr>
                        <td align="right" class="frm_left">
                            姓名
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_PersonelName" class="txt_border" type="text" id="Invoice_PersonelName" size="25" maxlength="50" value="<%=IEntity.Invoice_PersonelName %>"/>
                            <span class="t12_red">*</span>
                            <span id="Invoice_PersonelName_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            身份证号
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_PersonelCard" class="txt_border" type="text" id="Invoice_PersonelCard" size="25" maxlength="50" value="<%=IEntity.Invoice_PersonelCard %>"/>
                            <span class="t12_red">*</span>
                            <span id="Invoice_PersonelCard_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td align="right" valign="top" class="frm_left">
                            发票内容
                        </td>
                        <td class="frm_right">
                            <input id="Radio4" name="Invoice_Details" type="radio" value="1" checked="checked" /> 明细
                            <span class="t12_red">*</span>
                        </td>
                    </tr>
                    <%}
                      else
                      { %>
                    <tr>
                        <td align="right" class="frm_left">
                            单位名称
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_FirmName" class="txt_border" type="text" id="Invoice_VAT_FirmName" size="25" maxlength="100" value="<%=IEntity.Invoice_VAT_FirmName %>" />
                            <span class="t12_red">*</span>
                            <span id="Invoice_VAT_FirmName_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            纳税人识别号
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_Code" class="txt_border" type="text" id="Invoice_VAT_Code" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_Code %>"  />
                            <span class="t12_red">*</span>
                            <span id="Invoice_VAT_Code_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            注册地址
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_RegAddr" class="txt_border" type="text" id="Text4" size="25" maxlength="100" value="<%=IEntity.Invoice_VAT_RegAddr %>"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            注册电话
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_RegTel" class="txt_border" type="text" id="Invoice_VAT_RegTel" size="25" maxlength="50"  value="<%=IEntity.Invoice_VAT_RegTel %>"/>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            开户银行
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_Bank" class="txt_border" type="text" id="Invoice_VAT_Bank" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_Bank %>"/>
                            <span class="t12_red">*</span>
                            <span id="Invoice_VAT_Bank_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            银行账户
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_VAT_BankAccount" class="txt_border" type="text" id="Invoice_VAT_BankAccount" size="25" maxlength="50" value="<%=IEntity.Invoice_VAT_BankAccount %>" />
                            <span class="t12_red">*</span>
                            <span id="Invoice_VAT_BankAccount_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            纳税人资格证
                        </td>
                        <td class="frm_right">
                            <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=attachment&formName=frm_account_profile&frmelement=Invoice_VAT_Cert&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">资格证路径</td>
                        <td class="frm_right"><input type="text" class="txt_border" name="Invoice_VAT_Cert" id="Invoice_VAT_Cert" size="25" maxlength="50" readonly value="<%=IEntity.Invoice_VAT_Cert %>" /></td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            发票内容
                        </td>
                        <td class="frm_right">
                            <input id="Radio5" name="Invoice_VAT_Content" type="radio" value="明细" checked="checked" /> 明细
                            <span class="t12_red">*</span>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td align="right" class="frm_left">
                            邮寄地址
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_Address" type="text" class="txt_border" id="Invoice_Address" size="25" onblur="check_member_IsBlank('Invoice_Address');" maxlength="50"  value="<%=IEntity.Invoice_Address %>"/>
                             <span class="t12_red">*</span> <span id="Invoice_Address_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            收票人姓名
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_Name" type="text" class="txt_border" id="Invoice_Name" size="25" onblur="check_member_IsBlank('Invoice_Name');" maxlength="50" value="<%=IEntity.Invoice_Name %>" />
                             <span class="t12_red">*</span> <span id="Invoice_Name_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            联系电话
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_Tel" type="text" class="txt_border" id="Invoice_Tel" size="25" onblur="check_member_IsBlank('Invoice_Tel');" maxlength="50"  value="<%=IEntity.Invoice_Tel %>"/>
                             <span class="t12_red">*</span> <span id="Invoice_Tel_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="frm_left">
                            邮编
                        </td>
                        <td class="frm_right">
                            <input name="Invoice_ZipCode" type="text" class="txt_border" id="Invoice_ZipCode" size="25" onblur="check_ZipCode('Invoice_ZipCode');" maxlength="50"  value="<%=IEntity.Invoice_ZipCode %>" />
                             <span class="t12_red">*</span> <span id="Invoice_ZipCode_tip" class="t12_grey"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="t14">
                        </td>
                        <td>
                            <input name="action" type="hidden" id="action" value="editinvoice" />
                            <input name="Invoice_ID" type="hidden" id="Invoice_ID" value="<%=invoice_id %>" />
                            <input name="btn_submit" type="submit" class="buttonSkinA" id="Submit1" value="保存" />
                        </td>
                    </tr>
                    
                </table>
                </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
