﻿<%@ Page Language="C#" %>

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
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
    }
    if (entity.Orders_BuyerID != tools.NullInt(Session["member_id"]))
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="发货单详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
            margin: 0px auto;
        }
    </style>
    <script type="text/javascript">
        function turnnewpage(url) {
            location.href = url
        }
    </script>
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


        function confirmreceive(Orders_Delivery_ID) {
            var index = layer.load(2, {
                shade: [0.4, '#FFF'] //0.1透明度的白色背景
            });

            //$.ajax({
            //    type: "post",
            //    url: "/member/orders_do.aspx?action=orderacceptsettle&delivery_id=" + Orders_Delivery_ID + "\"",
            //    success: function (msg) {
            //        alert(msg)
            //        if (msg != "sucess") {
            //            layer.close(index);
            //            alert("确认签收失败");
            //        }
            //        else {
            //            layer.close(index);
            //            alert("确认签收成功");
            //            window.location.href = "/member/order_delivery_list.aspx";
            //        }
            //    }
            //    })
            //alert(Orders_Delivery_ID);
            $.post("/member/orders_do.aspx?action=orderacceptsettle&delivery_id=" + Orders_Delivery_ID + "\"", function (data, status) {
                var a = data.split('错误信息');
                if (a.length == 1) {
                    layer.close(index);
                    //alert("确认签收成功");
                    window.location.href = "/member/order_delivery_list.aspx";
                }
                else {
                    layer.close(index);
                    //alert(data);
                    
                }
                //alert(a[0]);
                //if (data == "success") {
                //    layer.close(index);
                //    alert("确认签收成功");

                //    window.location.href = "/member/order_delivery_list.aspx";
                //} else {
                //    layer.close(index);
                //    alert("确认签收失败");
                //    //window.location.href = "/member/order_delivery_list.aspx";
                //}

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
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>发货单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(1,6) %>
                </div>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <% member.order_Delivery_Detail(ODEntity, entity, false); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!-- 右侧弹框 开始-->
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
    <!-- 右侧弹框 结束-->
    <!--主体 结束-->

    <div class="clear"></div>
    <ucbottom:bottom ID="bottom2" runat="server" />


</body>
</html>
