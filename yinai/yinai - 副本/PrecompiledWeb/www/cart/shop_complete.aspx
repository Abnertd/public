<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>


<%@ Register Src="~/Public/cartTop.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    ITools tools = ToolsFactory.CreateTools();
    Orders orders = new Orders();
    Member member = new Member();
    Public_Class pub = new Public_Class();
   
    int payway_id, payway_cod;
    string btn_value = "现在支付";
    payway_cod = 0;
    payway_id = 0;
    member.Member_Login_Check("/member/index.aspx");

    string orders_sn = tools.CheckStr(Request["sn"]);
    string[] sn_array = orders_sn.Split(',');
    int orders_id = 0;
    int orders_count=0;
    double orders_price = 0, account_pay = 0;
    string payway_name, delivery_name;
    payway_name = "";
    delivery_name = "";
    if (sn_array != null && sn_array.Length > 0)
    {
        foreach (string order_sn in sn_array)
        {
            orders_count++;
            OrdersInfo ordersinfo = orders.GetOrdersInfoBySN(order_sn);
            if (ordersinfo != null)
            {
                if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
                {
                    orders_id = ordersinfo.Orders_ID;
                    orders_price = orders_price + ordersinfo.Orders_Total_AllPrice;
                    account_pay = 0;
                    payway_id = ordersinfo.Orders_Payway;
                    payway_name = ordersinfo.Orders_Payway_Name;
                    delivery_name = ordersinfo.Orders_Delivery_Name;
                }
                else
                {
                    Response.Redirect("/member/index.aspx");
                }
            }
            else
            {
                Response.Redirect("/member/index.aspx");
            }
        }
    }
    else
    {
        Response.Redirect("/member/index.aspx");
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
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单成功<%=" - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/jquery-extend-AdAdvance2.js"></script>
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
    <style type="text/css">
        
        .pg_main table td {
            padding: 8px;
        }
    </style>
</head>
<body>
        <div id="head_box" style="border-bottom:2px solid #ff6600;">
    <!--头部 开始-->
    <uctop:top runat="server" ID="Top" />
        <!--头部 开始-->
<div class="head" style="width:1000px;">
      <div class="logo"><a href="/"><img src="/images/logo.jpg"></a></div>
      <div class="tit">订单提交成功</div>
      <div class="head_right">
            <ul>
                <li><img src="/images/icon04.jpg">正品保证</li>
                <li><img src="/images/icon05.jpg">明码实价</li>
                <li><img src="/images/icon06.jpg">售后保障</li>
            </ul>
            <div class="clear"></div>
      </div>
      <div class="clear"></div>
</div>
<!--头部 结束-->
</div>
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF; width: 1000px;">
            <div class="blk34" style="background-image: url(/images/shop_car_bg03.jpg); border-bottom: none;">
                <ul>
                    <li class="on" style="margin-left: 13px;"><span>1</span>我的进货单</li>
                    <li class="on" style="margin-left: 105px;"><span>2</span>完善订单信息</li>
                    <li class="on" style="margin-left: 98px;"><span>3</span>订单提交成功</li>
                </ul>
                <div class="clear"></div>
            </div>
            <div class="blk40">
                <dl>
                    <dt>
                        <img src="/images/icon40.jpg"></dt>
                    <dd>
                        <b>订单提交成功，请等候供应商确认！</b>
                        <p>共 <%=orders_count %> 张订单：<%=orders_sn %> | 待支付：<strong><%=pub.FormatCurrency(orders_price) %></strong></p>
                        <p>供应商在收到订单后会在<strong>48小时</strong>内进行订单金额确认，请您耐心等待！订单确认后，我们会在第一时间通过Email和手机短信方式通知您！</p>
                    </dd>
                    <div class="clear"></div>
                </dl>
            </div>
           <%-- <div class="blk41"><a href="/Index.aspx" class="a36">再去转转</a><a href="/member/order_list.aspx" class="a37">查看订单</a></div>--%>
             <div class="blk41"><a href="/Index.aspx" class="a36">再去转转</a><a href="/member/order_view.aspx?orders_sn=<%=orders_sn %>" class="a37">查看订单</a></div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
 <div id="leftsead">
        <ul>           
            <li>
  <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                           <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
              <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none">
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
    <ucbottom:bottom ID="Bottom" runat="server" />
    <!--尾部 结束-->

</body>
</html>
