<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/cartTop.ascx" TagPrefix="uctop" TagName="top" %>
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
    string IsFirstTag = "";
    if (Session["IsFirstTag"] == null)
    {

    }
    else
    {
        IsFirstTag = tools.CheckStr(Session["IsFirstTag"].ToString());
    }
    Session["url_after_login"] = "/cart/my_cart.aspx";

    if (tools.NullInt(Application["RepidBuy_IsEnable"]) == 0)
    {
        if (IsFirstTag == "False")
        {

        }
        else
        {
            mem.Member_Login_Check1("/cart/my_cart.aspx");

        }


    }
    Session["IsFirstTag"] = "";
    if (Application["Trade_Contract_IsActive"].ToString() == "1")
    {
        contract_url = "/cart/cart_pact.aspx";
    }
    else
    {
        contract_url = "/cart/order_confirm.aspx";
    }

    Session["url_after_login"] = "/cart/my_cart.aspx";
    Session["Web_Cursor"] = "Category";
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的购物车<%=" - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/common.js"></script>
    <%--    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script src="/scripts/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/cart.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/hdtab.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setSelectSubAll(supplier_id, goods_id) {
            //当没有选中某个子复选框时，SelectAll取消选中
            if (!$("#chk_cart_goods_" + supplier_id + goods_id).checked) {
                $("#chk_cart_supplier_" + supplier_id).attr("checked", false);
            }
            var chsub = $("input[type='checkbox'][class='select-sub" + supplier_id + "']").length; //获取subcheck的个数
            var checkedsub = $("input[type='checkbox'][class='select-sub" + supplier_id + "']:checked").length; //获取选中的subcheck的个数

            if (checkedsub == chsub) {
                $("#chk_cart_supplier_" + supplier_id).attr("checked", true);
            }
        }
        function getCountPrice(a, b) {
            $.ajaxSetup({ async: false });

            var countprice = 0;
            $("input[name=chk_cart_goods]:checked").each(function () {

                countprice = countprice + parseFloat($("#price_" + $(this).val()).val().toString());

            });

            $("#strong_totalprice").html("<strong>￥" + (parseFloat(countprice, 2)).toFixed(2) + "</strong>");
        }
        function getCountSumPrice(a, b) {
            $.ajaxSetup({ async: false });

            var countprice = 0;
            $("input[name=chk_cart_goods]:checked").each(function () {

                countprice = countprice + parseFloat($("#price_" + $(this).val()).val().toString());

            });

            $("#strong_totalprice").html("<strong>￥" + (parseFloat(countprice, 2)).toFixed(2) + "</strong>");
        }
        function check_Cart_All_New() {

            if ($('#chk_all_goods').attr("checked")) {
                $("input[name='chk_cart_goods']:enabled").attr("checked", true);
                //$("input[name='chk_cart_supplier']:enabled").attr("checked", true);
            }
            else {
                $("input[name='chk_cart_goods']").attr("checked", false);
                //$("input[name='chk_cart_supplier']").attr("checked", false);
            }
            //getCountSumPrice();
        }
        //function Checkbox_ALL(obj) {
        //    if ($(obj).prop('checked')) {
        //        $("input[name=OrdersGoodsArray]").prop('checked', 'checked');
        //        $("input[name=OrdersGoodsArray2]").prop('checked', 'checked');
        //    }
        //    else {
        //        $("input[name=OrdersGoodsArray]").removeAttr('checked');
        //        $("input[name=OrdersGoodsArray2]").removeAttr('checked');
        //    }
        //    getCountPrice();
        //}
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
    <script src="/scripts/1.js" type="text/javascript"></script>
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
    <!--滑动门 开始-->
    <script type="text/javascript" src="/scripts/hdtab.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["e01", "e02"], ["ee01", "ee02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
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
    <style type="text/css">
        .empty {
            width: 798px;
            overflow: hidden;
            border: 1px solid #DDD;
            background: #F7F7F7;
            padding: 50px 0 50px 200px;
            margin-top: 10px;
        }

            .empty p {
                height: 120px;
                padding-left: 150px;
                background: url(/images/shopping_icon02.png) no-repeat left center;
                line-height: 80px;
                font-size: 14px;
            }
    </style>
</head>
<body>
    <div id="head_box" style="border-bottom: 2px solid #ff6600;">
        <!--头部 开始-->
        <uctop:top runat="server" ID="Top" />
        <!--头部 开始-->
        <div class="head" style="width: 1000px;">
            <div class="logo">
                <a href="/">
                    <img src="/images/logo.jpg"></a>
            </div>
            <div class="tit">我的购物车</div>
            <div class="head_right">
                <ul>
                    <li>
                        <img src="/images/icon04.jpg">正品保证</li>
                    <li>
                        <img src="/images/icon05.jpg">明码实价</li>
                    <li>
                        <img src="/images/icon06.jpg">售后保障</li>
                </ul>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
        <!--头部 结束-->
    </div>
    <!--主体 开始-->
    <div class="content03">
        <div class="blk34">
            <ul>
                <li class="on" style="margin-left: 13px;"><span>1</span>我的购物车</li>
                <li style="margin-left: 105px;"><span>2</span>完善订单信息</li>
                <li style="margin-left: 98px;"><span>3</span>订单提交成功</li>
            </ul>
            <div class="clear"></div>
        </div>

        <% cart.My_Cart_ProductList(false);%>
    </div>
    <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>
            <li>
                <a href="javascript:void(0);" onclick="SignUpNow();">
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

    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>
<script type="text/javascript">
    function Renew(Orders_Goods_Product_ID, Orders_Goods_ID, Orders_Goods_ID) {
        alert(Orders_Goods_Product_ID, Orders_Goods_ID, Orders_Goods_ID)
        $.post("/cart/cart_do.aspx?action=renew&product_id=" + Orders_Goods_Product_ID + "&goods_id=" + Orders_Goods_ID + "&buy_amount='+MM_findObj('buy_amount_" + Orders_Goods_ID + "').value;")
        alert("success")
    }

</script>
