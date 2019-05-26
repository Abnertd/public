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
    string type = tools.CheckStr(Request["type"]);
    supplier.Supplier_Login_Check("/supplier/order_list.aspx?type=" + type);

    //string action=tools.NullStr(Request.QueryString["action"]);
    string date_start, date_end;
    string orders_sn;
    orders_sn = tools.CheckStr(Request["orders_sn"]);
    int order_id = tools.CheckInt(Request["order_id"]);

    if (type != "unprocessed" && type != "processing" && type != "success" && type != "faiture")
    {
        type = "all";
    }
    date_start = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    date_end = tools.FormatDate(DateTime.Now, "yyyy-MM-dd");
    string title = "";
    title = "我的发货单";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=title + " - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
   
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>

    <script type="text/javascript" src="/scripts/common.js"></script>
  <%--  <script src="../scripts/common1.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript" src="/scripts/cart.js"></script>--%>
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>


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
    </script>
    <!--示范一个公告层 结束-->
    <script type="text/javascript">
        function check_all() {
            if ($("#check_all").attr('checked')) {
                $("input[name='check']").attr('checked', 'checked');
            }
            else {
                $("input[name='check']").attr('checked', '');
            }
        }
        $(function () {
            $("#export").click(function () {
                var checkedValues = new Array();
                $("input[name='check']").each(function () {
                    if ($(this).is(':checked')) {
                        checkedValues.push($(this).val());
                    }
                });
                strDelGoodID = checkedValues.join(',')
                location.href = 'orders_do.aspx?action=ordergoodsexport&orders_id=' + strDelGoodID;
            })
            $("#ordersexport").click(function () {
                var checkedValues = new Array();
                $("input[name='check']").each(function () {
                    if ($(this).is(':checked')) {
                        checkedValues.push($(this).val());
                    }
                });
                strDelGoodID = checkedValues.join(',')
                location.href = 'orders_do.aspx?action=ordersexport&orders_id=' + strDelGoodID;
            })
        })


        function OrderSearch(object) {
            var keyword = $("#" + object).val();
            var orderDate = $("#orderDate").val();
            var orderStatus = $("#orderStatus").val();

            if (keyword == "订单号/商品名称/商品编号") {
                alert('请输入查询关键词！');
            } else {
                window.location.href = "/supplier/order_list.aspx?keyword=" + keyword + "&orderDate=" + orderDate + "&orderStatus=" + orderStatus;
            }
        }

        function OrderDate() {
            var sDate = $("#orderDate").val();
            if (sDate == 0) {
                sDate = 1;
            }
            var oState = $("#orderStatus").val();
            window.location = "/supplier/order_list.aspx?orderDate=" + sDate + "&orderStatus=" + oState;
        }

        function OrderStatus() {
            var sDate = $("#orderDate").val();
            var oState = $("#orderStatus").val();
            window.location = "/supplier/order_list.aspx?orderDate=" + sDate + "&orderStatus=" + oState;
        }

    </script>

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 交易管理 > <strong>我的发货单</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">                  
                    <% supplier.Get_Supplier_Left_HTML(1, 2); %>                  
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>我的发货单</h2>
                        <div class="b14_1_main" style="margin-top: 15px;">
                            <% supplier.Orders_Delivery_List(order_id); %>
                        </div>
                    </div>
                </div>

                <div class="clear">
                </div>
            </div>
        </div>
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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
