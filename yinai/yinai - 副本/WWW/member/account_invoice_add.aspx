<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member myApp = new Member();
    Addr addr = new Addr();
    myApp.Member_Login_Check("/member/account_Invoice_add.aspx");
    int Invoice_Type = 0;
    string Invoice_Title = "个人";
    if (Request["Type"] == "0" || Request["Type"] == "1")
    {
        Invoice_Type = tools.CheckInt(Request["Type"]);
    }
    if (tools.CheckStr(Request["Title"]) == "个人" || tools.CheckStr(Request["Title"]) == "单位")
    {
        Invoice_Title = tools.CheckStr(Request["Title"]);
    }
    MemberInfo memberinfo = myApp.GetMemberByID();
    MemberProfileInfo profileInfo = null;
    if (memberinfo == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    else
    {
        profileInfo = memberinfo.MemberProfileInfo;
        if (profileInfo == null)
        {
            Response.Redirect("/member/index.aspx");
        }
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="添加发票 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetInvoiceType(ID) {
            var type = $("#" + ID).val();
            window.location = "account_Invoice_add.aspx?Type=" + type;
        }
        function GetInvoiceTitle(ID) {
            var Title = $("#" + ID).val();
            window.location = "account_Invoice_add.aspx?Type=0&Title=" + Title;
        }
        function check_member_IsBlank(object) {
            $("#" + object + "_tip").load("/member/register_do.aspx?action=checkisblank&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
                return false;
            }
            else {
                return true;
            }
        }
        function check_ZipCode(object) {
            $("#" + object + "_tip").load("/member/register_do.aspx?action=checkzip&val=" + $("#" + object).val() + "&timer=" + Math.random());
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
    <style type="text/css">
        .zkw_Title21 {
            margin-bottom: 10px;
        }
        .blk17_sz table td input {
            height: auto;
            border: 1px solid #dddddd;
            box-shadow: inset 1px 1px 4px #d9d9d9;
        }
        .blk17_sz table td input {
            /*height: 30px;*/
            border: 1px solid #dddddd;
            box-shadow: inset 1px 1px 4px #fff;
        }

        /*.blk17_sz table td input.buttonSkinA { background-image:url(../images/save_buttom.jpg); background-repeat:no-repeat; width:141px; height:38px; font-size:16px; font-weight:bold; text-align:center; line-height:38px; display:block; color:#FFF;}*/
        .buttonSkinA {
            border-radius:1px;
            color:white !important;
            background-color:#ff6600;
            width:80px; height:38px;
            text-align:center;
        }
    </style>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom:20px;">
            <!--位置说明 开始-->
            <div class="position">
                您现在的位置 > <a href="/index.aspx">首页</a> > <a href="/member/index.aspx">我是买家</a> > <a href="account_Invoice_add.aspx">添加发票</a>
            </div>
          <%--  <div class="clear">--%>
            </div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=myApp.Member_Left_HTML(4,5) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1">
                        <h2>添加发票</h2>
                        <div class="main">
                            <%--<div class="zkw_order">--%>
                            <div class="blk17_sz">
                                <form name="frm_account_profile" id="frm_account_profile" action="account_Invoice_do.aspx" method="post" onsubmit="return check_Invoice_all();">
                                    <table width="100%" border="0" cellpadding="5" cellspacing="0" class="table_padding_5">

                                        <tr>
                                            <td align="right" class="frm_left" width="116">发票类型
                                            </td>
                                            <td class="frm_right">
                                                <input id="Invoice_type" name="Invoice_type" onclick="GetInvoiceType('Invoice_type');" type="radio" value="0" <%=pub.CheckRadio("0", Invoice_Type.ToString())%> />
                                                普通发票&nbsp; 
                            <input id="Invoice_type1" name="Invoice_type" onclick="GetInvoiceType('Invoice_type1');" type="radio" value="1" <%=pub.CheckRadio("1", Invoice_Type.ToString())%> />
                                                增值税发票
                            <span class="t12_red">*</span>
                                            </td>
                                        </tr>
                                        <%if (Invoice_Type == 0)
                                          { %>
                                        <tr>
                                            <td align="right" class="frm_left">发票抬头
                                            </td>
                                            <td class="frm_right">
                                                <input id="Invoice_Title" name="Invoice_Title" type="radio" value="个人" onclick="GetInvoiceTitle('Invoice_Title');" <%=pub.CheckRadio("个人", Invoice_Title)%> />
                                                个人&nbsp; 
                            <input id="Invoice_Title1" name="Invoice_Title" type="radio" value="单位" onclick="GetInvoiceTitle('Invoice_Title1');" <%=pub.CheckRadio("单位", Invoice_Title)%> />
                                                单位
                            <span class="t12_red">*</span>
                                            </td>
                                        </tr>
                                        <%if (Invoice_Title == "单位")
                                          { %>
                                        <tr>
                                            <td align="right" class="frm_left">单位名称
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_FirmName" class="txt_border" type="text" id="Invoice_FirmName" size="25" maxlength="100" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_FirmName_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right" class="frm_left">单位地址
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_RegAddr" class="txt_border" type="text" id="Text6" size="25" maxlength="100" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">单位电话
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_RegTel" class="txt_border" type="text" id="Text7" size="25" maxlength="50" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">开户银行
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_Bank" class="txt_border" type="text" id="Text8" size="25" maxlength="50" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">银行账户
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_BankAccount" class="txt_border" type="text" id="Text9" size="25" maxlength="50" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">税号
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_Code" class="txt_border" type="text" id="Text5" size="25" maxlength="50" />

                                            </td>
                                        </tr>
                                        <%}
                                          else if (Invoice_Title == "个人")
                                          { %>
                                        <tr>
                                            <td align="right" class="frm_left">姓名
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_PersonelName" class="txt_border" type="text" id="Invoice_PersonelName" size="25" maxlength="50" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_PersonelName_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">身份证号
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_PersonelCard" class="txt_border" type="text" id="Invoice_PersonelCard" size="25" maxlength="50" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_PersonelCard_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <%} %>
                                        <tr>
                                            <td align="right" valign="top" class="frm_left">发票内容
                                            </td>
                                            <td class="frm_right">
                                                <input id="Invoice_Details" name="Invoice_Details" type="radio" value="1" checked="checked" />
                                                明细
                            <span class="t12_red">*</span>
                                            </td>
                                        </tr>
                                        <%}
                                          else
                                          { %>
                                        <tr>
                                            <td align="right" class="frm_left">单位名称
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_FirmName" class="txt_border" type="text" id="Invoice_VAT_FirmName" size="25" maxlength="100" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_VAT_FirmName_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">纳税人识别号
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_Code" class="txt_border" type="text" id="Invoice_VAT_Code" size="25" maxlength="50" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_VAT_Code_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">注册地址
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_RegAddr" class="txt_border" type="text" id="Text4" size="25" maxlength="100" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">注册电话
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_RegTel" class="txt_border" type="text" id="Invoice_VAT_RegTel" size="25" maxlength="50" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">开户银行
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_Bank" class="txt_border" type="text" id="Invoice_VAT_Bank" size="25" maxlength="50" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_VAT_Bank_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">银行账户
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_VAT_BankAccount" class="txt_border" type="text" id="Invoice_VAT_BankAccount" size="25" maxlength="50" />
                                                <span class="t12_red">*</span>
                                                <span id="Invoice_VAT_BankAccount_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">纳税人资格证
                                            </td>
                                            <td class="frm_right">
                                                <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=attachment&formName=frm_account_profile&frmelement=Invoice_VAT_Cert&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">资格证路径</td>
                                            <td class="frm_right">
                                                <input type="text" readonly class="txt_border" name="Invoice_VAT_Cert" id="Invoice_VAT_Cert" size="25" maxlength="50" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">发票内容
                                            </td>
                                            <td class="frm_right">
                                                <input id="Radio5" name="Invoice_VAT_Content" type="radio" value="明细" checked="checked" />
                                                明细
                            <span class="t12_red">*</span>
                                            </td>
                                        </tr>
                                        <%} %>
                                        <tr>
                                            <td align="right" class="frm_left">邮寄地址
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_Address" type="text" class="txt_border" id="Invoice_Address" size="25" onblur="check_member_IsBlank('Invoice_Address');" maxlength="50" value="<%=addr.DisplayAddress(profileInfo.Member_State,profileInfo.Member_City,profileInfo.Member_County)+profileInfo.Member_StreetAddress %>" />
                                                <span class="t12_red">*</span> <span id="Invoice_Address_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">收票人姓名
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_Name" type="text" class="txt_border" id="Invoice_Name" size="25" onblur="check_member_IsBlank('Invoice_Name');" maxlength="50" value="<%=profileInfo.Member_Name %>" />
                                                <span class="t12_red">*</span> <span id="Invoice_Name_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">联系电话
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_Tel" type="text" class="txt_border" id="Invoice_Tel" size="25" onblur="check_member_IsBlank('Invoice_Tel');" maxlength="50" value="<%=memberinfo.Member_LoginMobile %>" />
                                                <span class="t12_red">*</span> <span id="Invoice_Tel_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="frm_left">邮编
                                            </td>
                                            <td class="frm_right">
                                                <input name="Invoice_ZipCode" type="text" class="txt_border" id="Invoice_ZipCode" size="25" onblur="check_ZipCode('Invoice_ZipCode');" maxlength="50" value="<%=profileInfo.Member_Zip %>" />
                                                <span class="t12_red">*</span> <span id="Invoice_ZipCode_tip" class="t12_grey"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" class="t14"></td>
                                            <td>
                                                <input name="action" type="hidden" id="action" value="Invoice_add" />
                                                <input name="btn_submit" type="submit" class="buttonSkinA" id="Submit1"  />
                                                
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
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
