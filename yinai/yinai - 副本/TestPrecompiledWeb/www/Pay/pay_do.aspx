<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="../Public/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="../Public/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Orders orders = new Orders();
    Member MEM = new Member();
    Pay pay = new Pay();



    int orders_id;
    string orders_sn = "";
    int orders_Payway = 0;
    int pay_type = 0;
    string pay_intro = "";
    string pay_type_sign = "";
    orders_id = tools.CheckInt(Request["orders_id"]);
    //MEM.Member_Login_Check("/member/index.aspx");

    if (orders_id == 0)
    {
        pub.Msg("error", "错误信息", "订单不存在", false, 3, "/member/order_all.aspx");
    }

    OrdersInfo ordersinfo = orders.GetOrdersByID(orders_id);
    if (ordersinfo != null)
    {
        if (ordersinfo.Orders_Status < 2 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
        {
            orders_sn = ordersinfo.Orders_SN;
            orders_Payway = ordersinfo.Orders_Payway;
        }
        else
        {
            orders_id = 0;
        }

    }
    else
    {
        orders_id = 0;
    }
    if (orders_id == 0)
    {
        pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
    }
    pay_type_sign = tools.CheckStr(Request["pay_type"]);
    PayWayInfo paywayinfo = orders.GetPayWayByID(orders_Payway);
    if (paywayinfo != null)
    {
        if (paywayinfo.Pay_Way_Status == 1)
        {
            pay_type = paywayinfo.Pay_Way_Type;
            pay_intro = paywayinfo.Pay_Way_Intro;
        }
    }

    if (pay_type > 0)
    {
        PayInfo payinfo = orders.GetPayInfoByID(pay_type);
        if (payinfo != null)
        {
            pay_type_sign = payinfo.Sys_Pay_Sign;
        }
        //if (pay_type_sign == "YeePay")
        //{
        //    Response.Redirect("/pay/Pay_index.aspx?sn=" + orders_sn);
        //}
        pay.Pay_Request(pay_type_sign, "order_pay", orders_id, orders_sn);
        Response.End();
    } 
     
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="订单支付 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/jquery-extend-AdAdvance2.js"></script>
    <style type="text/css">
        .img_border {
            border: 1px solid #d5d5d5;
        }

        .table_filter {
            margin-top: 10px;
        }

        .pay_intro {
            line-height: 25px;
            padding: 10px;
        }
    </style>
</head>
<body>



    <uc1:Top ID="Page_Top" runat="server" />
    <div class="yz_content">
        <div class="yz_content_main">
            <!--位置说明 开始-->
            <div class="yz_position">
                您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > 订单支付
            </div>




            <table width="980" border="0" align="center" cellspacing="0" cellpadding="5" class="table_filter">
                <tr>
                    <td class="t14_red">订单支付</td>
                </tr>
            </table>
            <table width="980" align="center" border="0" cellspacing="0" cellpadding="5">
                <tr>
                    <td class="pay_intro"><%=pay_intro %></td>
                </tr>
            </table>


            <table border="0" cellpadding="0" cellspacing="0" width="960" align="center">
                <tr>
                    <td height="10"></td>
                </tr>
            </table>

        </div>
    </div>
    <uc2:Bottom ID="Page_Bottom" runat="server" />

</body>
</html>

