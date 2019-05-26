<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    
        Public_Class pub = new Public_Class();
        ITools tools = ToolsFactory.CreateTools();
        Supplier supplier = new Supplier();
        Member member = new Member();
        Orders orders = new Orders();
        double Orders_AllPrice = 0;
        double Orders_PayedAmount = 0;
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        int pay_type = tools.CheckInt(Request["pay_type"]);
    
        if (pay_type != 1)
        {
            pay_type = 0;
        }


        member.Member_Login_Check("/member/orders_pay.aspx?orders_sn=" + orders_sn);
        MemberInfo memberinfo = member.GetMemberByID();
        if (memberinfo == null)
        {
            Response.Redirect("/member/index.aspx");
        }

        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单信息不存在", false, "{back}");
        }
        OrdersInfo ordersinfo = member.GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status != 1 || (ordersinfo.Orders_PaymentStatus > 1))
            {
                orders_sn = "";
            }
            Orders_AllPrice = ordersinfo.Orders_Total_AllPrice;
            //Orders_PayedAmount = supplier.Get_Contract_PayedAmount(ordersinfo.Contract_ID);
            if (ordersinfo.Orders_BuyerID != tools.NullInt(Session["member_id"]) && ordersinfo.Orders_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "无法执行此操作", false, "/member/order_list.aspx");
            }
            if (ordersinfo.Orders_BuyerID != tools.NullInt(Session["member_id"]))
            {
                pay_type = 0;
            }
        }
        else
        {
            orders_sn = "";
        }

        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "无法执行此操作", false, "{back}");
        }


%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单支付 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <style type="text/css">
        .main table td {
            padding: 3px;
            line-height: 22px;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap"> <div class="content02" style="margin-bottom:20px;">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/index.aspx">首页</a> > <a href="/member/index.aspx">我是买家</a> > <span>合同支付</span>
        </div>
      <%--  <div class="clear"></div>--%>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <%=member.Member_Left_HTML(0, 0)%>
            </div>
            <div class="pd_right">
                <div class="blk14_1">
                    <h2>订单支付</h2>
                    <div class="b14_1_main">

                        <form id="frm_delivery" name="frm_delivery" method="post" action="contract_do.aspx">
                            <table width="100%" border="0" cellpadding="0" cellspacing="5">
                                <tr>
                                    <td width="100" align="right" class="t12_53">合同总价</td>
                                    <td align="left"><%=pub.FormatCurrency(Orders_AllPrice) %></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">已支付费用</td>
                                    <td align="left"><%=pub.FormatCurrency(Orders_PayedAmount)%></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">待支付金额</td>
                                    <td align="left"><%=pub.FormatCurrency(Orders_AllPrice - Orders_PayedAmount)%></td>
                                </tr>

                                <tr>
                                    <td width="100" align="right" class="t12_53">支付金额</td>
                                    <td align="left">
                                        <input type="text" name="Contract_Payment_Amount" class="txt_border" id="Contract_Payment_Amount" value="<%=Math.Round(Orders_AllPrice - Orders_PayedAmount, 2, MidpointRounding.AwayFromZero)%>" style="width: 100px;" /></td>
                                </tr>
                                <tr>
                                    <td width="100" class="t12_53" align="right">支付备注</td>
                                    <td align="left">
                                        <textarea name="Contract_Payment_Note" id="Contract_Payment_Note" cols="50" rows="5"></textarea></td>
                                </tr>
                                <tr>
                                    <td class="t12_53" align="right">上传支付凭据</td>
                                    <td align="left">
                                        <iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=attachment&formname=frm_delivery&frmelement=Contract_Payment_Attachment&rtvalue=1&rturl=<% =Application["upload_server_return_WWW"]%>" width="100%" height="20" frameborder="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="left">
                                        <input type="submit" name="btn_add" value=" 保存 " class="buttonSkinA" />
                                        <input name="action" type="hidden" id="action" value="contract_pay" />
                                        <input name="Contract_Payment_Attachment" type="hidden" id="Contract_Payment_Attachment" />
                                        <input name="orders_sn" type="hidden" id="orders_sn" value="<%=orders_sn %>" />
                                    </td>
                                </tr>
                            </table>

                        </form>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div> </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
