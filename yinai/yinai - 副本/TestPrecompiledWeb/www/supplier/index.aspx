<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    //Orders orders = new Orders();
    CMS cms = new CMS();
    supplier.Supplier_AuditLogin_Check("/supplier/index.aspx");
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="卖家中心 - " + pub.SEO_TITLE()%></title>
     
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
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/scripts/Echarts/esl.js" type="text/javascript"></script>
    <script src="/scripts/Echarts/echarts.js" type="text/javascript"></script>
     <script type="text/javascript" src="/scripts/common.js"></script>
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
        $(function () {
            $("#startdate_salesprice").datepicker({ numberOfMonths: 1 });
            $("#enddate_salesprice").datepicker({ numberOfMonths: 1 });

            //默认加载销售额统计
            MyEcharts("salesprice", "salesprice");

            //销售额查询分析按钮
            $("#btn_salesprice").click(function () {
                var strElement = "salesprice";
                MyEcharts(strElement, strElement);
            });

            //图表显示
            function MyEcharts(element, action) {

                require(['echarts', 'echarts/chart/line'],
                    function (ec) {
                        var myChart = ec.init(document.getElementById("detail_" + element));

                        //图表显示提示信息
                        myChart.showLoading({
                            text: '暂无数据',
                            effect: 'bubble',
                            textStyle: {
                                fontSize: 30
                            }
                        });

                        //定义图表options
                        var options = {
                            tooltip: {
                                trigger: 'axis'
                            },
                            grid: { x: 70, y: 20, x2: 20, y2: 36 },
                            legend: {
                                data: []
                            },
                            toolbox: {
                                show: true,
                                feature: {
                                    mark: false
                                }
                            },
                            calculable: true,
                            xAxis: [
                                {
                                    type: 'category',
                                    data: []
                                }
                            ],
                            yAxis: [
                                {
                                    type: 'value',
                                    splitArea: { show: true }
                                }
                            ],
                            series: []
                        };

                        $.ajax({
                            type: "post", global: false, async: false, dataType: "json",
                            url: "datastatistics_do.ashx",
                            data: $("#frm_" + element).serialize(),
                            success: function (result) {
                                if (result) {
                                    //将返回的category和series对象赋值给options对象内的category和series
                                    //因为xAxis是一个数组 这里需要是xAxis[i]的形式
                                    options.xAxis[0].data = result.category;
                                    options.series = result.series;
                                    options.legend.data = result.legend;

                                    myChart.hideLoading();
                                    myChart.setOption(options);
                                }
                            }
                        });

                    });
            }

        });
    </script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position">当前位置 >  <strong>我是卖家</strong></div>
            <!--位置说明 结束-->
            <div class="partd" style="margin-top:auto !important">
                 <div class="pd_left">               
                        <% supplier.Get_Supplier_Left_HTML(0, 0); %>                  
                </div>            
                <div class="pd_right">
                    <div class="pd_r_left" style="border-top:1px solid #dddddd">
                        <%if (tools.NullInt(Session["account_id"]) == 0)
                          { %>
                            <div class="blk16">
                                <ul>
                                    <li class="li03" style="position: relative; margin-left: -1px; width: 232px;margin-right:auto !important">
                                       <%-- <h2>交易前
                                        </h2>--%>
                                        <div class="li_main">
                                            <ul>
                                                <li style="margin-left:32px;">
                                                    <img src="/images/icon09_sz.jpg"><a href="/supplier/order_list.aspx?orderStatus=unconfirm"><span><%=supplier.Supplier_Order_Count(0,tools.NullInt(Session["supplier_id"]),"unconfirm") %></span>待确认</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li class="li03" style="margin-right:auto !important">
                                        <%--<h2>交易中
                                        </h2>--%>
                                        <div class="li_main">
                                            <ul>
                                                 <li style="margin-left:30px;">
                                                    <img src="/images/icon12_sz.jpg"><a href="/supplier/order_list.aspx?orderStatus=payment"><span><%=supplier.Supplier_Order_Count(0,tools.NullInt(Session["supplier_id"]),"payment") %></span>待支付</a></li>
                                               <li style="margin-left:32px;">
                                                    <img src="/images/icon13_sz.jpg"><a href="/supplier/order_list.aspx?orderStatus=delivery"><span><%=supplier.Supplier_Order_Count(0,tools.NullInt(Session["supplier_id"]),"delivery") %></span>待发货</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li class="li03" style="margin-right:auto !important">
                                      <%--  <h2 style="border-right: none;">交易后
                                        </h2>--%>
                                        <div class="li_main" style="border-right: none;">
                                            <ul>
                                                <li style="margin-left:32px;">
                                                    <img src="/images/icon14_sz.jpg"><a href="/supplier/order_list.aspx?orderStatus=accept"><span><%=supplier.Supplier_Order_Count(0,tools.NullInt(Session["supplier_id"]),"accept") %></span>待签收</a></li>
                                            </ul>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        <%}
                        %>
                        <div class="blk13">
                            <h2 style="border-bottom: 1px solid #fb4f4f;">销售额统计</h2>

                            <form id="frm_salesprice" action="?">
                                <input type="hidden" name="action" value="salesprice" />

                                <div class="b13_info">
                                    分析时段：
                                            <%
                                                string startdate = DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd");
                                                string enddate = DateTime.Today.ToString("yyyy-MM-dd");

                                                Response.Write("<input value=\"" + startdate + "\" id=\"startdate_salesprice\" name=\"startdate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                                Response.Write(" 至 <input value=\"" + enddate + "\" id=\"enddate_salesprice\" name=\"enddate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                            %>
                                    <%--&nbsp;&nbsp;&nbsp;&nbsp;订单状态
                                            <select id="orders_status" name="orders_status">
                                                <option value="confirm">已确认及已成功</option>
                                                <option value="success" selected="selected">已成功</option>
                                            </select>--%>
                                    <a href="javascript:void(0);" id="btn_salesprice">分 析</a>
                                </div>
                            </form>

                            <div class="b13_info02" id="detail_salesprice" style="height: 203px"></div>
                        </div>
                    </div>
                    <div class="pd_r_right">
                        <div class="blk14" style="height: 278px;">
                            <h2 style="border-bottom: 1px solid #ff6600;">网站公告</h2>
                            <div class="b14_main_1">
                                <%cms.Member_Index_Notice(); %>
                            </div>
                        </div>
                        <div class="blk14" style="margin-top: 15px;">
                            <h2 style="border-bottom: 1px solid #ff6600;">帮助中心</h2>
                            <div class="b14_main_1">
                                <%cms.Member_Index_Help(); %>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
            </div>
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
