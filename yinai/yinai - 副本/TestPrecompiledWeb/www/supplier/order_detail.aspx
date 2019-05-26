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
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int menu = tools.NullInt(Request["menu"]);
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    supplier.Supplier_Login_Check("/supplier/order_detail.aspx?menu=" + menu + "&orders_sn=" + orders_sn);

    OrdersInfo entity = supplier.GetOrdersInfoBySN(orders_sn, tools.NullInt(Session["supplier_id"]));
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/orders_list.aspx?payment=1&menu=1");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单详情 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <%--   <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <%--    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <style type="text/css">
        .main table td {
            padding: 3px;
        }
    </style>
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
    <!--弹出菜单 end-->
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

        function turnnewpage(url) {
            location.href = url
        }
        function updateorderprice() {

            MM_findObj('Orders_Total_AllPrice').value = parseFloat(MM_findObj('Orders_Total_Price').value) + parseFloat(MM_findObj('Orders_Total_Freight').value) - parseFloat(MM_findObj('Orders_Total_PriceDiscount').value) - parseFloat(MM_findObj('Orders_Total_FreightDiscount').value);

        }


    </script>

    <!--示范一个公告层 开始-->
    <script type="text/javascript">
        function SignUpNow() {
            layer.open({
                type: 2
   , title: false //不显示标题栏
                //, closeBtn: false
   , area: ['480px;', '340px']

   , shade: 0.8
   , id: 'LAY_layuipro' //设定一个id，防止重复弹出
   , resize: false
   , btnAlign: 'c'
   , moveType: 1 //拖拽模式，0或者1              
                , content: ("/Bid/SignUpPopup.aspx")
            });
        }
    </script>
    <!--示范一个公告层 结束-->




</head>
<body>
    <uctop:top ID="top2" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>订单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(1, 1); %>
                </div>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <% supplier.order_Detail(orders_sn); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>
            <li><a href="javascript:void(0);" onclick="SignUpNow();">
                <div class="hides" style="width: 130px; height: 50px; display: none;" id="qq">
                    <div class="hides" id="p1">
                        <img src="/images/nav_1_1.png" />
                    </div>
                </div>
                <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
            </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px; height: 50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px; height: 50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->
    <div class="clear"></div>
    <ucbottom:bottom ID="bottom2" runat="server" />


</body>
</html>
