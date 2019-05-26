<%@ Page Language="C#" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Position"] = "Logistics";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Logistics MyLogistics = new Logistics();
    Addr addr = new Addr();
    string keyword = tools.CheckStr(Request["keyword"]);
    string Supplier_Address_State = tools.CheckStr(Request["Supplier_Address_State"]);
    string Supplier_Address_City = tools.CheckStr(Request["Supplier_Address_City"]);
    string Supplier_Orders_Address_State = tools.CheckStr(Request["Supplier_Orders_Address_State"]);
    string Supplier_Orders_Address_City = tools.CheckStr(Request["Supplier_Orders_Address_City"]);
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="仓储物流 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script src="/scripts/common.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
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
    <script src="/scripts/1.js"></script>
    <!--弹出菜单 start-->
    <script>
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
    <style>
        .ccwl_part2_1 span select {
            border: solid 1px #e4e4e4;
            height: 39px;
        }
    </style>
</head>
<body>
    <div id="head_box">
        <!--顶部 开始-->
        <uctop:top runat="server" ID="HomeTop" />
        <!--顶部 结束-->
        <!--头部 开始-->

        <!--头部 结束-->
        <!--导航 开始-->

        <!--导航 结束-->
    </div>
    <!--主体 开始-->
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/">首页</a> > 仓储物流</div>
        <!--位置 结束-->
        <div class="banner02">
            <img src="/images/img12.jpg" width="1200" height="156" /></div>
        <div class="parte" style="border: none;">
            <div class="ccwl_part1">
                <ul>
                    <li><a href="javascript:void(0);" class="part1_on">我要发货</a></li>
                </ul>
            </div>
            <div class="ccwl_part2">
                <form name="frm_bid" id="frm_bid" method="post" action="/Logistics/index.aspx">
                    <div class="ccwl_part2_1">
                        <span style="font-size:14px;">搜索发货地</span>
                        <span id="div_area"><%=addr.SelectAddressSecond("div_area", "Supplier_Address_State", "Supplier_Address_City",Supplier_Address_State, Supplier_Address_City)%></span>
                    </div>
                    <div class="ccwl_part2_1">
                        <span style="font-size:14px;">收货地</span>
                        <span id="div_area2"><%=addr.SelectAddressSecond("div_area2", "Supplier_Orders_Address_State", "Supplier_Orders_Address_City",  Supplier_Orders_Address_State, Supplier_Orders_Address_City)%></span>
                    </div>
                    <input type="hidden" id="Supplier_Address_State" name="Supplier_Address_State" value="<%=Supplier_Address_State %>" />
                    <input type="hidden" id="Supplier_Address_City" name="Supplier_Address_City" value="<%=Supplier_Address_City %>" />
                    <input type="hidden" id="Supplier_Orders_Address_State" name="Supplier_Orders_Address_State" value="<%=Supplier_Orders_Address_State %>" />
                    <input type="hidden" id="Supplier_Orders_Address_City" name="Supplier_Orders_Address_City" value="<%=Supplier_Orders_Address_City %>" />

                    <div class="ccwl_part2_2"><a href="javascript:void(0);" onclick="$('#frm_bid').submit();">查询</a></div>
                </form>
            </div>
            <div class="ccwl_part3">
                <%MyLogistics.Logistics_IndexList(); %>
            </div>



            <div class="ccwl_part1">
                <ul>
                    <li><a href="javascript:void(0);" class="part1_on">我有物流线路</a></li>
                </ul>
            </div>
               <div class="ccwl_part3">
                <%MyLogistics.Logistics_LineList(); %>
            </div>
            <div class="clear"></div>
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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>

