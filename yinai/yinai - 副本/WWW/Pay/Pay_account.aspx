<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/Supplier_Top.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="../Public/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<%@ Register Src="../Public/PayHTML.ascx" TagName="PayHTML" TagPrefix="uc3" %>
<% 
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay = new Pay();
    Cart cart = new Cart();
    Member MEM = new Member();
    string btn_value = "现在支付";
    string sn = tools.CheckStr(Request["sn"]);
    double orders_price = 0;
    string order_sn;
    order_sn = "";
    MemberAccountOrdersInfo ordersinfo = MEM.GetMemberAccountOrdersByOrdersSN(sn);
    if (ordersinfo != null)
    {
        if (((ordersinfo.Account_Orders_MemberID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Account_Orders_MemberID > 0) || (ordersinfo.Account_Orders_SupplierID == tools.CheckInt(Session["supplier_id"].ToString()) && ordersinfo.Account_Orders_SupplierID > 0)) && ordersinfo.Account_Orders_Status == 0)
        {
            order_sn = ordersinfo.Account_Orders_SN;
            orders_price = ordersinfo.Account_Orders_Amount;
        }
        else
        {
            Response.Redirect("/member/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/member/index.aspx");
    }

    if (Request["action"] == "account_add")
    {
        string pay_type_sign = tools.CheckStr(Request["pay_type"]);
        if (pay_type_sign == "ONLINE_PAY")
        {
            Response.Redirect("/pay/Pay_account.aspx?sn=" + sn);
        }
        pay.Pay_Request(pay_type_sign, "account_pay", 0, sn);
        Response.End();
    }

    //MEM.Member_Login_Check("/pay/Pay_account.aspx?sn=" + sn);
     
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%="账户充值 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="/scripts/common.js"></script>
    <script language="javascript" type="text/javascript" src="/scripts/jquery.js"></script>
    <style type="text/css">
        #div_hk p
        {
            height: 30px;
            line-height: 30px;
            color: #333333;
        }
        .a
        {
            border-bottom: 1px solid #8fd58f;
        }
        .b
        {
            background-image: url(/images/opt.jpg);
            line-height: 30px;
            text-align: center;
            width: 102px;
            height: 30px;
            background-repeat: no-repeat;
            font-size: 14px;
            font-weight: bold;
            color: #278429;
            cursor: pointer;
        }
        .c
        {
            cursor: pointer;
            width: 102px;
            text-align: center;
            border-bottom: 1px solid #8fd58f;
            font-size: 14px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <%--<uctop:top runat="server" ID="Top" />--%>
    <div id="webwrap">
        <div class="list_top">
            <a href="../index.aspx">首页</a>&nbsp;>&nbsp;账户充值</div>
        <div style="width: 1200px; margin: 0 auto;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                <tr>
                    <td>
                        <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
                            <tr>
                                <td height="0">
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px; font-size: 13px; font-weight: bold; color: #FF6600;"
                                    align="left">
                                    充值金额：<%=pub.FormatCurrency(orders_price) %>
                                </td>
                            </tr>
                            <tr>
                                <td height="0">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-bottom: 10px;">
            <style type="text/css">
                #div_hk p
                {
                    height: 30px;
                    line-height: 30px;
                    color: #333333;
                }
                .a
                {
                    border-bottom: 1px solid #cccccc;
                }
                .b
                {
                    background-image: url(/images/opt.jpg);
                    line-height: 30px;
                    text-align: center;
                    width: 102px;
                    height: 30px;
                    background-repeat: no-repeat;
                    font-size: 14px;
                    font-weight: bold;
                    color: #666666;
                    cursor: pointer;
                }
                .c
                {
                    cursor: pointer;
                    width: 102px;
                    text-align: center;
                    border-bottom: 1px solid #cccccc;
                    font-size: 14px;
                    cursor: pointer;
                }
            </style>
            <div style="width: 1100px; margin: 0 auto; margin-top: 0px; padding-top: 0px;">
                <table style="width: 1100px; margin-top: 10px;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="a" width="125" style="font-weight: bold; color: #333333; font-size: 14px;
                            padding-left: 25px;">
                            请选择支付方式
                        </td>
                        <td class="b" id="td_zf">
                            <a onmouseover="$('#td_hk').attr('class','c');$('#td_zf').attr('class','b');$('#img_zf').show();$('#div_zf').show();$('#div_hk').hide();">
                                在线支付</a>
                        </td>
                        <td class="c" id="td_hk">
                            <a onmouseover="$('#td_hk').attr('class','b');$('#td_zf').attr('class','c');$('#img_zf').hide();$('#div_zf').hide();$('#div_hk').show();">
                                银行汇款</a>
                        </td>
                        <td class="a">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <form name="form1" id="form1" method="post" action="/pay/Pay_account.aspx">
                <div id="div_zf" style="border: 1px solid #cccccc; border-top: 0px;">
                    <p style="height: 35px; line-height: 35px; font-size: 13px; padding-left: 26px; color: #333333;
                        padding-top: 10px;">
                        国内银行卡或信用卡</p>
                    <input type="hidden" value="0001" name="pay_bank" id="pay_bank" />
                    <table style="width: 800px; margin-left: 50px; margin: 0 auto;" border="0" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="width: 20%; height: 50px;">
                                <input type="radio" name="pay_type" value="ALIPAY" checked="checked" style="margin-top: 10px;
                                    float: left; margin-right: 11px;" /><img alt="支付宝" title="支付宝" src="/images/logo_alipay.gif" />
                            </td>
                            <td style="width: 20%; height: 50px;">
                                <input type="radio" value="CHINAPAY" onclick="$('#pay_bank').attr('value','0001');"
                                    name="pay_type" style="margin-top: 10px; margin-right: 11px; float: left;" /><img
                                        style="float: left;" alt="银联" title="银联" src="/images/logo_chinapay.jpg" />
                            </td>
                            <td style="width: 20%; height: 50px;">
                                <input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','ABC');"
                                    style="margin-top: 10px; float: left; margin-right: 11px;" /><img alt="中国农业银行" title="中国农业银行"
                                        src="/images/logo_99bill_abc.gif" />
                            </td>
                            <td style="width: 20%; height: 50px;">
                                <input type="radio" name="pay_type" value="BOC" style="margin-top: 10px; float: left;
                                    margin-right: 11px;" /><img alt="中国银行" title="中国银行" src="/images/logo_99bill_boc.gif" />
                            </td>
                            <td style="width: 20%; height: 50px;">
                                <input type="radio" name="pay_type" value="99BILL" onclick="$('#pay_bank').attr('value','CCB');"
                                    style="margin-top: 10px; float: left; margin-right: 11px;" /><img alt="中国建设银行" title="中国建设银行"
                                        src="/images/logo_ccb_b2c.gif" />
                            </td>
                    </table>
                </div>
                <div id="div_hk" style="border: 1px solid #cccccc; border-top: 0px; padding-bottom: 10px;">
                    <p style="height: 35px; line-height: 35px; font-size: 13px; padding-left: 26px; color: #333333;
                        padding-top: 10px;">
                        温馨提示：</p>
                    <%=cart.GetPayWayByActive()%>
                </div>
                <div style="font-size: 14px; background-color: #ffffff; color: #333333; padding-top: 10px;">
                    <img src="/images/zhifu.jpg" onclick="$('#form1').submit();" id="img_zf" style="float: right;
                        cursor: pointer;" />
                </div>
                <input type="hidden" name="sn" value="<%=order_sn %>" />
                <input type="hidden" name="action" value="account_add" />
                </form>
            </div>
        </div>
    </div>
    <uc2:Bottom ID="Page_Bottom" runat="server" />
</body>
</html>
