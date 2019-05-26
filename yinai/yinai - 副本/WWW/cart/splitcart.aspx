<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Cart cart = new Cart();
    Orders orders = new Orders();
    Member mem = new Member();
    string contract_url;
    Session["Orders_Address_ID"] = 0;
    Session["Orders_Delivery_ID"] = 0;
    Session["Orders_Payway_ID"] = 0;
    Session["Orders_DeliveryTime_ID"] = 0;
    Session["delivery_fee"] = 0;        //运费
    Session["order_favor_coupon"] = "0";//优惠券编号
    Session["all_favor_coupon"] = "0";//优惠券使用信息
    Session["order_favorfee"] = 0;   //运费优惠编号
    Session["favor_fee"] = 0;        //运费优惠金额
    Session["favor_coupon_price"] = 0;        //优惠券优惠金额
    Session["favor_policy_price"] = 0;  //优惠政策优惠金额
    Session["favor_policy_id"] = 0;     //优惠政策优惠编号
    Session["total_price"] = 0;
    Session["total_coin"] = 0;

    contract_url = "/cart/order_confirm.aspx";

    Session["url_after_login"] = "/cart/my_cart.aspx";
    string orders_sn = tools.CheckStr(Request["sn"]);
    int orders_id = 0;
    double orders_price = 0;
    string payway_name, delivery_name;
    payway_name = "";
    delivery_name = "";
    int payway_id, payway_cod;
    payway_cod = 0;
    payway_id = 0;
    string btn_value = "现在支付";
    string action = tools.CheckStr(Request["action"]);
    if (action == "success")
    {
        OrdersInfo ordersinfo = orders.GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["supplier_id"].ToString()))
            {
                orders_id = ordersinfo.Orders_ID;
                orders_price = ordersinfo.Orders_Total_AllPrice;
                payway_id = ordersinfo.Orders_Payway;
                payway_name = ordersinfo.Orders_Payway_Name;
                delivery_name = ordersinfo.Orders_Delivery_Name;
            }
            else
            {
                Response.Redirect("/supplier/index.aspx");
            }
        }
        else
        {
            Response.Redirect("/supplier/index.aspx");
        }
        PayWayInfo payway = orders.GetPayWayByID(payway_id);
        if (payway != null)
        {
            payway_cod = payway.Pay_Way_Cod;
        }
        if (payway_cod == 1)
        {
            btn_value = "支付说明";
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>请选择要结算的商品<%=" - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/jquery-extend-AdAdvance2.js"></script>
    <style type="text/css">
        #ding2 img{ display:inline; }
        .jiesuan { float:left; cursor:pointer;}
        #ding2 table td{padding:5px;}
    </style>
</head>
<body>
    <!--头部 开始-->
<div class="head" style=" height:130px;">
      <uctop:top ID="top1" runat="server" />
      <div class="head_info" style=" background-image:none; width:990px;">
            <div class="logo">
                <a href="/index.aspx">
                <img src="/images/logo.gif"></a>
                </div>
            <div class="num_box">
                  <ul>
                      <li class="on">1.我的进货单</li>
                      <li>2.填写核对订单信息</li>
                      <li>3.成功提交订单</li>
                  </ul>
            </div>
            <div class="clear"></div>
      </div>
</div>
<!--头部 结束-->
 <div class="partg">
      <h2>我的进货单</h2>
      <div class="pg_main">

      <div style="width: 960px; margin: 0 auto;
                text-align: center; font-size: 14px; color: #FF6600; line-height: 50px; margin-bottom: 15px;">
                <b>由于您购买的商品是由不同的商家进行配送，因此需要分别结算以下订单！ </b>
            </div>
            <div id="ding2">
                <%=cart.Get_CartListBySupplier()%>
            </div>

      </div>
</div>
   
    
<!--尾部 开始-->
<div class="foot">
<ucbottom:bottom ID="bottom1" runat="server" />
       
</div>
<!--尾部 结束-->
    
</body>
</html>
