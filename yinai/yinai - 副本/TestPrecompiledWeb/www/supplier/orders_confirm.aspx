<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    OrdersInfo entity = null;
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();

    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    double orders_price = 0, orders_freight = 0;

    myApp.Supplier_Login_Check("/supplier/orders_confirm.aspx?orders_sn=" + orders_sn);

    int Orders_Type = 0;

    if (orders_sn.Length > 0)
    {
        entity = myApp.GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
        if (entity == null)
        {
            pub.Msg("error", "错误信息", "订单记录不存在", false, "/supplier/order_list.aspx");
        }
        else
        {
            Orders_Type = entity.Orders_Type;
            orders_price = entity.Orders_Total_Price;
            orders_freight = entity.Orders_Total_Freight;
        }
        if (entity.Orders_Status != 0)
        {
            pub.Msg("error", "错误信息", "无法执行此操作", false, "/supplier/order_list.aspx");
        }
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="订单确认 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <style type="text/css">
        .zkw_title21 {
            margin-bottom: 10px;
        }

        .b07_info04 select {
            height: 30px;
            font-size: 14px;
        }
    </style>

    <script type="text/javascript">

        function checkordersprice(orders_price) {
            if ($("#select_price").val() == "+") {

            }
            else {
                if (orders_price < $("#Orders_Total_PriceDiscount").val()) {
                    $("#Orders_Total_PriceDiscount").val(orders_price)
                }
                else if (orders_price == $("#Orders_Total_PriceDiscount").val()) {
                    $("#Orders_Total_PriceDiscount").val(1)
                }
            }
        }

        function checkordersfee(orders_freight) {
            if ($("#select_freight").val() == "+") {

            }
            else {
                if (orders_freight < $("#Orders_Total_FreightDiscount").val()) {
                    $("#Orders_Total_FreightDiscount").val(orders_freight)
                }
            }
        }

        function setSelectPrice() {
            $("#Orders_Total_PriceDiscount").val(0);
        }

        function setSelectFee() {
            $("#Orders_Total_FreightDiscount").val(0);
        }


        function sum(amount_id, sum_id, price_id, product_count) {
            //alert(11);
            var sum_1 = 0;
            var sumSP = $("#" + sum_id);// 获得ID为first标签的jQuery对象 
            var Price = $("#" + price_id);// 获得ID为first标签的jQuery对象  
            var Amount = $("#" + amount_id);// 获得ID为first标签的jQuery对象  
            var orders_goods_target = $("#" + orders_goods_target);

            var num1 = sumSP.val();
            //var num1 = Price.val();// 取得first对象的值 单价  
            var num2 = Amount.val();// 取得second对象的值  数量 
            var sum = (num1) * (num2);


            Price.html(sum);


            //var ss1 = $("#ss1").html().replace("￥", "");

            //var ss2 = sum + parseFloat(ss1);

            //sumSP.text(sum);

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
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>订单确认</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% myApp.Get_Supplier_Left_HTML(1, 1); %>
                </div>
              <%--  <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>订单确认</h2>
                        <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04">
                                    <form name="formadd" id="formadd" method="post" action="/supplier/orders_do.aspx">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                            <tr>
                                                <td width="92" class="name">订单编号
                                                </td>
                                                <td width="801"><%=orders_sn %></td>
                                            </tr>
                                            <tr>
                                                <td width="92" class="name">商品金额
                                                </td>
                                                <td width="801"><%=pub.FormatCurrency(entity.Orders_Total_Price) %></td>
                                            </tr>
                                         
                                            <tr>
                                                <td width="92" class="name">订单金额</td>
                                                <td><%=pub.FormatCurrency(entity.Orders_Total_AllPrice) %></td>
                                            </tr>
                                   
                                            <tr>
                                                <td width="92" class="name">价格调整说明
                                                </td>
                                                <td width="801">
                                                    <textarea id="Orders_Total_PriceDiscount_Note" name="Orders_Total_PriceDiscount_Note" cols="50" rows="5"><%=entity.Orders_Total_PriceDiscount_Note %></textarea>
                                                </td>
                                            </tr>
                                         
                                            <tr>
                                                <td width="92" class="name"></td>
                                                <td width="801">
                                                    <input name="action" type="hidden" id="action" value="orderfirm" />
                                                    <input type="hidden" id="Orders_ID" name="Orders_ID" value="<%=entity.Orders_ID %>" />
                                                    <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>--%>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <% myApp.order_Details(orders_sn); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <!--主体 结束-->
        <div class="clear"></div>
        <ucbottom:bottom ID="bottom2" runat="server" />
</body>
</html>
