<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Logistics logistics = new Logistics();
    Session["Position"] = "";
  
    logistics.Logistics_Login_Check("/Logistics/Logistics_list.aspx");
  
    Logistics MyLogistics = new Logistics();
   
    LogisticsInfo logisticsinfo = logistics.GetLogisticsByID();
  
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="物流报价 - 物流商用户中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>

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
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
    <style type="text/css">
        .img_style {
            width: 100%;
            clear: both;
            height: auto;
            display: inline;
        }

            .img_style img {
                width: 11px;
                float: left;
                margin-top: 13px;
                margin-right: 11px;
            }

        .blk14_1 h2 span {
            float: right;
            display: inline;
            margin-top: 7px;
        }

            .blk14_1 h2 span a.a13 {
                background-image: url(../images/a_bg01.jpg);
                background-repeat: no-repeat;
                width: 74px;
                height: 26px;
                font-size: 12px;
                font-weight: normal;
                text-align: center;
                line-height: 26px;
                color: #333;
                display: inline-block;
                vertical-align: middle;
                margin-right: 7px;
            }
          
        .input_calendar {
    background-image: url(/Images/icon_calendar.png);
    background-repeat: no-repeat;
    background-position: left center;
    padding-top: 0px;
    padding-right: 0px;
    padding-bottom: 0px;
    /*padding-left: 20px;*/
    width: 100px;
   
}

    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <a href="/Logistics/Logistics.aspx">物流商</a> > <strong>发布物流 </strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% logistics.Get_Logistics_Left_HTML(1, 1); %>
                </div>
                <div class="pc_right">



                            <div class="blk14_1" style="margin-top: 1px;">
                                <h2>发布物流</h2>

                                <div class="main">
                                    <div class="b14_1_main">


                                        <form name="frm_bid" id="frm_dislog" method="post" action="/Logistics/Logistics_do.aspx">
                                            <div class="b02_main">
                                                <ul style="width: 850px;margin-left:0px;">

                                                    <li><span>联系人：</span><label><input name="Logistics_Line_Contact" id="Logistics_Line_Contact" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>


                                                    <li><span>联系电话：</span><label><input name="Logistics_Line_Note" id="Logistics_Line_Note" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>


                                                    <li><span>车型：</span><label><input name="Logistics_Line_CarType" id="Logistics_Line_CarType" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>


                                                    
                                                    <li><span>发货地址：</span><label><input name="Logistics_Line_Delivery_Address" id="Logistics_Line_Delivery_Address" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>



                                                    
                                                    <li><span>收货地址：</span><label><input name="Logistics_Line_Receiving_Address" id="Logistics_Line_Receiving_Address" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>

                                                  

                                                   
                                                    <li><span>交货时间：</span><label><input name="Logistics_Line_DeliverTime" class="input_calendar" id="Logistics_Line_DeliverTime" type="text" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 128px; padding-left: 25px;" /></label></li>
                                                    <script type="text/javascript">$(function () { $("#Logistics_Line_DeliverTime").datepicker({ inline: true }); })</script>
                                                   
                                                     <li><span>发货价格：</span><label><input name="Logistics_Line_Deliver_Price" id="Logistics_Line_Deliver_Price" type="text" style="width: 298px;" value="" /></label></li>
                                                    <div class="clear"></div>

                                                    
                                                     <div class="clear"></div>

                                               

                                                   
                                                  
                                               
                                                    <li><a href="javascript:void(0);" onclick="$('#frm_dislog').submit();" class="a05" style="background-color: none; width: 79px;">保 存</a></li>
                                                </ul>

                                                <div class="clear"></div>
                                            </div>
                                            <input name="action" type="hidden" id="action" value="dislog_add" />

                                        </form>
                                    </div>



                                </div>
                            </div>
                     
                </div>

            </div>
        </div>
    </div>
    <div class="clear"></div>

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




