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
    Orders orders = new Orders();


    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    member.Member_Login_Check("/member/order_close.aspx?orders_sn=" + orders_sn);

    if (orders_sn == "")
    {
        pub.Msg("error", "错误信息", "订单不存在", false, "/supplier/order_list.aspx");
    }
    string Orders_Note = "";
    double totalpayamount = 0;
    OrdersInfo ordersinfo = member.GetOrdersInfoBySN(orders_sn);
    if (ordersinfo != null)
    {
        totalpayamount = member.GetOrdersPayedAmount(ordersinfo.Orders_ID);
        if (ordersinfo.Orders_Status != 1 || ordersinfo.Orders_PaymentStatus != 0 || totalpayamount >= ordersinfo.Orders_Total_AllPrice)
        {
            orders_sn = "";
        }
        else
        {
            Orders_Note = ordersinfo.Orders_Note;
        }
    }
    else
    {
        orders_sn = "";
    }

    if (orders_sn == "")
    {
        pub.Msg("error", "错误信息", "无法执行此操作", false, "/member/order_list.aspx");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单付款 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="../scripts/common.js"></script>
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
    <script type="text/javascript">
        function sys_payway_select(textstr) {

            MM_findObj("Orders_Payment_Name").value = textstr;
        }
    </script>
    <style type="text/css">
        .buttonSkinA {
            border: none;
            _border: 0;
            background-image: url(../images/a_bg01.jpg);
            background-repeat: no-repeat;
            width: 74px;
            height: 26px;
            font-size: 12px;
            font-weight: normal;
            text-align: center;
            line-height: 26px;
            text-align: center;
            color: #333;
            display: inline-block;
            vertical-align: middle;
            margin-top: 18px;
        }

        .b14_1_main table {
            border-bottom: 1px solid #eeeeee;
            border-left: 1px solid #eeeeee;
            border-right: 1px solid #eeeeee;
        }


        .input01 {
            background: none;
            width: 182px;
            height: 38px;
            padding-left: 2px;
            border: 1px solid #fff;
        }

        .input_name {
            border-bottom: 1px solid #eeeeee;
        }

        .b14_1_main table td .input_name {
            text-align: left;
            /*padding-left:15px;*/
            padding-left: 15px;
        }
    </style>

</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position"><a href="/tradeindex.aspx">首页</a> > <a href="/member/">会员中心</a> > 交易管理 > <strong>订单付款</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(1, 1) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>订单付款</h2>
                        <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04">

                                    <form id="frm_edit" name="frm_edit" method="post" action="/pay/pay_do_new.aspx">
                                        <table border="0" cellpadding="5" cellspacing="0">
                                            <tr>
                                                <td width="92" class="name" style="padding-right: 5px; border-top: 1px solid #eeeeee">订单编号
                                                </td>
                                                <td width="801" class="input_name" style="border-top: 1px solid #eeeeee"><%=orders_sn %></td>
                                            </tr>

                                            <tr>
                                                <td width="92" class="name" style="padding-right: 5px;">订单金额</td>
                                                <td class="input_name"><%=pub.FormatCurrency(ordersinfo.Orders_Total_AllPrice) %></td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td width="92" class="name" style="padding-right: 5px;">已付款金额</td>
                                                <td class="input_name"><%=pub.FormatCurrency(totalpayamount) %></td>
                                            </tr>


                                            <tr>

                                                <td width="92" class="name" style="padding-right: 5px;">支付方式
                                                </td>
                                                <td align="left" class="input_name"><%orders.Sys_Payway_Select();%><input type="text" name="Orders_Payment_Name" id="Orders_Payment_Name" style="display: none;" /></td>
                                            </tr>
                                            <%--     <tr>
				<td width="100" align="right" class="t12_53">支付方式</td>
				<td align="left"><%=orders.Sys_Payway_Select("Contract_PaymentID", contractinfo.Contract_Payway_ID)%>&nbsp;&nbsp;<span style="font-size:12px; color:Red; ">请不要变更支付方式</span></td>
			  </tr>--%>


                                            <tr style="display: none">
                                                <td width="92" class="name">本次付款金额
                                                </td>
                                                <td>
                                                    <%--  <input type="text" id="Orders_PaymentAmount" name="Orders_PaymentAmount" value="<%=Math.Round(ordersinfo.Orders_Total_AllPrice-totalpayamount,2) %>" style="width: 100px;" class="input01" onkeyup="if(isNaN(value))execCommand('undo');" onafterpaste="if(isNaN(value))execCommand('undo')" /><i>*</i>--%>
                                                    <input type="text" id="Orders_PaymentAmount" name="Orders_PaymentAmount" value="<%=pub.FormatCurrency(ordersinfo.Orders_Total_AllPrice) %>" style="width: 100px;" class="input01" readonly="readonly" background="none;" onkeyup="if(isNaN(value))execCommand('undo');" onafterpaste="if(isNaN(value))execCommand('undo')" /><i>*</i>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td width="92" class="name" style="padding-right: 5px;">支付备注
                                                </td>
                                                <td align="left" class="input_name">
                                                    <textarea name="Orders_Payment_Note" id="Orders_Payment_Note" cols="50" rows="5"></textarea></td>
                                            </tr>

                                            <tr style="border-left: none;">
                                                <%--  <td></td>--%>
                                                <td style="text-align: left; border-right: none; border-bottom: none;" colspan="2" id="paybutton">
                                                    <input type="hidden" name="orders_id" value="<% =ordersinfo.Orders_ID%>" />
                                                    <input type="hidden" name="action" value="contract_pay" />
                                                    <input type="submit" name="btn_add" value=" 立即付款 " class="buttonSkinA" style="height: 26px; border: 0px; box-shadow: none;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>
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
