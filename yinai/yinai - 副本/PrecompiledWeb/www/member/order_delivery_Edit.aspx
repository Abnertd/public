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
    Session["Cur_Position"] = Session["Home_Position"] = "";
    string type = tools.CheckStr(Request["type"]);
    member.Member_Login_Check("/member/order_delivery_list.aspx?type=" + type);
    OrdersDeliveryInfo ODEntity;
    int Orders_Delivery_ID = tools.CheckInt(Request["Orders_Delivery_ID"]);
    ODEntity = member.GetOrdersDeliveryByID(Orders_Delivery_ID);

    if (ODEntity == null)
    {
        pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
    }
    Orders orders = new Orders();
    OrdersInfo entity = orders.GetOrdersByID(ODEntity.Orders_Delivery_OrdersID);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/orders_delivery_list.aspx?payment=1&menu=1");
    }
    if (entity.Orders_BuyerID != tools.NullInt(Session["member_id"]))
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/orders_delivery_list.aspx?payment=1&menu=1");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="发货单详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <style type="text/css">
        .main table td {
            padding: 3px;
        }
        a.a11 {
            background-image: url(../images/save_buttom.jpg);
            background-repeat: no-repeat;
            width: 141px;
            height: 38px;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            line-height: 38px;
            display: block;
            color: #FFF;
            margin:5px auto;

        }
    </style>
    <script type="text/javascript">
        function turnnewpage(url) {
            location.href = url
        }


        function sum(amount_id, sum_id, price_id, product_count) {
            //alert(11);
            var sum_1 = 0;
            var sumSP = $("#" + sum_id);// 获得ID为first标签的jQuery对象 
            var Price = $("#" + price_id);// 获得ID为first标签的jQuery对象  
            var Amount = $("#" + amount_id);// 获得ID为first标签的jQuery对象  
            var orders_goods_target = $("#" + orders_goods_target);

            var num1 = sumSP.val();
          
            var num2 = Amount.val();// 取得second对象的值  数量 
            var sum = (num1) * (num2);


            Price.html(sum);


        

            var priceSum = 0;
            for (var i = 0; i < product_count; i++) {
                priceSum += parseFloat($("#Orders_Goods_EveryProduct_Sum" + (i + 1)).html());
            }

            $("#ss").html("￥" + priceSum.toFixed(2))
            $("#ss2").html("￥" + priceSum.toFixed(2));
            $("#ss3").html("￥" + priceSum.toFixed(2))
            $("#ss4").html("￥" + priceSum.toFixed(2));



        }

    </script>
</head>
<body>
    <uctop:top ID="top2" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>发货单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">                 
                    <%=member.Member_Left_HTML(1,6) %>                     
                </div>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                       <% member.order_Delivery_Detail(ODEntity, entity,true); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <div class="clear"></div>
    <ucbottom:bottom ID="bottom2" runat="server" />


</body>
</html>
