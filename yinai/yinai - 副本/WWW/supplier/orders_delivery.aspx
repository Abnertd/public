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
    //OrdersDelivery

    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    double Orders_Price = 0;
    double accompany_price = 0;


    myApp.Supplier_Login_Check("/supplier/orders_delivery.aspx?orders_sn=" + orders_sn);

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
            accompany_price = myApp.GetOrdersAccompanyingPrice(entity.Orders_ID);
            Orders_Type = entity.Orders_Type;
            Orders_Price = entity.Orders_Total_AllPrice - accompany_price;
        }
        //会员支付 将付款状态由4改为1  已到账改为已支付
        //if (entity.Orders_Status != 1 || entity.Orders_PaymentStatus != 4)
        if (entity.Orders_Status != 1 || entity.Orders_PaymentStatus != 4)
        {
            pub.Msg("error", "错误信息", "无法执行此操作", false, "/supplier/order_list.aspx");
        }
    }
    Session["freightverify"] = pub.Createvkey(6);
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单发货 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <%--  <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <style type="text/css">
        .b07_info04 table td input {
            box-shadow: none;
        }

        .b07_info04 table td span {
            text-align: center;
            display: block;
        }

        .b07_info04 table th {
            padding: 10px 0 10px 10px;
            font-size: 14px;
            border-bottom: 1px solid #eeeeee;
        }
    </style>
    <script type="text/javascript">

        function frmcheckall(oname, othis) {

            if ($(othis).prop("checked") == true) {

                $("input:checkbox[name='" + oname + "']").attr('checked', true);
            }
            else {
                $("input:checkbox[name='" + oname + "']").attr('checked', false);
            }
        }
        function checkInputNum(thisobj, minNumber, maxNumber) {
            if ((/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/).test(thisobj.value)) {
                if (parseInt(thisobj.value) > 0 && parseInt(thisobj.value) > maxNumber) {
                    alert("超过了最大发货数量(" + maxNumber + ")");
                    thisobj.value = maxNumber;
                }
                else if (parseInt(thisobj.value) < 0 && parseInt(thisobj.value) < minNumber) {
                    alert("超过了最大退货数量(" + Math.abs(minNumber) + ")");
                    thisobj.value = minNumber;
                }
            }
            else {
                alert("请输入正确的数量");
                thisobj.value = maxNumber;
            }
        }


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
</head>
<body>
    <uctop:top ID="top2" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>订单发货</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% myApp.Get_Supplier_Left_HTML(1, 1); %>
                </div>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>订单发货</h2>
                        <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04">
                                    <form name="formadd" id="formadd" method="post" action="/supplier/orders_do.aspx">

                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">

                                            <tr>
                                                <td width="92" class="name" style="border-top: 1px solid #eeeeee">物流公司</td>
                                                <td width="801" style="border-top: 1px solid #eeeeee">
                                                    <input type="text" id="Orders_Delivery_companyName" name="Orders_Delivery_companyName" value="" style="width: 300px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="92" class="name">物流单号</td>
                                                <td width="801">
                                                    <input type="text" name="Orders_Delivery_code" id="Orders_Delivery_code" value="" style="width: 300px;" /></td>
                                            </tr>


                                            <tr>
                                                <td width="92" class="name">司机电话</td>
                                                <td width="801">
                                                    <input type="text" name="Orders_Delivery_DriverMobile" id="Orders_Delivery_DriverMobile" value="" onblur="check_driver_mobile('Orders_Delivery_DriverMobile');" style="width: 300px;" /><strong class="regtip" id="Orders_Delivery_DriverMobile_tip"></strong></td>
                                            </tr>
                                            <tr>
                                                <td width="92" class="name">车牌号码</td>
                                                <td width="801">
                                                    <input type="text" name="Orders_Delivery_PlateNum" id="Orders_Delivery_PlateNum" value="" style="width: 300px;" /></td>
                                            </tr>
                                            <tr>
                                                <td width="92" class="name">运输方式</td>
                                                <td width="801" class="Orders_Delivery_TransportType" id="Orders_Delivery_TransportType">
                                                    <% =new Orders().OrdersDelivery_Type_Select(0, "Orders_Delivery_TransportType")%>
                                                    <%--  <input type="text" name="Orders_Delivery_TransportType" id="Orders_Delivery_TransportType" value="" />--%></td>
                                            </tr>




                                            <tr>
                                                <td width="92" class="name">配送备注</td>
                                                <td width="801">
                                                    <textarea name="Orders_Delivery_Note" id="Orders_Delivery_Note" cols="50" rows="5"></textarea></td>
                                            </tr>

                                        </table>
                                        <%Response.Write(myApp.orders_delivery_goods_select(entity.Orders_ID)); %>

                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" id="Table1" class="table_padding_5">
                                            <tr>
                                                <td width="92" class="name"></td>
                                                <td width="801">
                                                    <input type="hidden" name="freightverify" value="<%=Session["freightverify"] %>" />
                                                    <input name="action" type="hidden" id="action" value="create_freight" />
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
                </div>
            </div>
        </div>
        <!--主体 结束-->
    </div>
    <div class="clear"></div>
    <%--右侧浮动弹框 开始--%>
    <div id="leftsead">
        <ul>
            <li>
                <%--   <a href="http://wpa.qq.com/msgrd?v=3&uin=800022936&site=qq&menu=yes" target="_blank">--%>
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
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes">
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
    <ucbottom:bottom ID="bottom2" runat="server" />
</body>
</html>
