<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<% 
    Member member = new Member();
    Statistics statistics = new Statistics();
    Public_Class pub = new Public_Class();
    member.Member_Login_Check("/member/member_statistics.aspx");

    string startdate = DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd");
    string enddate = DateTime.Today.ToString("yyyy-MM-dd");
    
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="统计报表 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/Echarts/esl.js" type="text/javascript"></script>
    <script src="/scripts/Echarts/echarts.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
     <%-- <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>
 
    <script src="/scripts/statistics.js"></script>
    <style type="text/css">
        .blk07_sz h2 .list02 {
            background-color: #fff;
            border-top: 1px solid #dddddd;
            border-right: 1px solid #dddddd;
        }

.blk07 h2 ul.list02 li.on {
    border-top: 2px solid #fc504e;
}
.blk07 h2 ul li.on {
    background-color: #fff;
    border-bottom: 1px solid #ffffff;
    border-top: 2px solid #d1121a;
    cursor: pointer;
}
.blk07 h2 ul li {
    border-right: 1px solid #cccccc;
    color: #333;
    cursor: pointer;
    display: inline;
    float: left;
    font-size: 14px;
    font-weight: bold;
    height: 40px;
    line-height: 40px;
    text-align: center;
    width: 150px;
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
            $("#startdate_purchase").datepicker({ numberOfMonths: 1 });
            $("#enddate_purchase").datepicker({ numberOfMonths: 1 });

            $("#startdate_orderscounts").datepicker({ numberOfMonths: 1 });
            $("#enddate_orderscounts").datepicker({ numberOfMonths: 1 });

            $("#startdate_purchaseranking").datepicker({ numberOfMonths: 1 });
            $("#enddate_purchaseranking").datepicker({ numberOfMonths: 1 });

            //默认加载采购额统计
            MyEcharts("purchase", "purchase");

            //选项卡切换
            $(".list02 li").click(function (e) {
                $(".list02 li.on").removeClass("on");
                $(this).addClass("on");

                var _this = this, strdata;
                $(".list02 li").each(function (i, e) {
                    strdata = $(this).attr("data");

                    if (_this == this) {
                        $("#" + strdata).show();

                        if (strdata == "purchase" || strdata == "orderscounts") {
                            if ($("#detail_" + strdata).html().length == 0) {
                                MyEcharts(strdata, strdata);
                            }
                        }
                        else if (strdata = "purchaseranking") {
                            if ($("#detail_" + strdata).html().length == 0) {
                                MyTables(strdata, strdata);
                            }
                        }
                    }
                    else {
                        $("#" + strdata).hide();
                    }
                });
            });

            //销售额查询分析按钮
            $("#btn_purchase").click(function () {
                var strElement = "purchase";
                MyEcharts(strElement, strElement);
            });

            //销量查询分析按钮
            $("#btn_orderscounts").click(function () {
                var strElement = "orderscounts";
                MyEcharts(strElement, strElement);
            });

            $("#btn_purchaseranking").click(function () {
                var strElement = "purchaseranking";
                MyTables(strElement, strElement);
            });

            function MyTables(element, action, page) {
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
                    });

                $.getJSON(
                    "datastatistics_do.ashx?rows=10&page=" + page + "&sidx=SUM(Orders_Goods_Amount)&sord=desc",
                    $("#frm_" + element).serialize(),
                    function (data) {
                        $("#detail_" + element).html(ShowTables(data));

                        if (data != null) {
                            $("#input_pager").keyup(function (evt) {
                                var key = window.event ? evt.keyCode : evt.which;
                                if (key == 13) {
                                    MyTables(element, action, parseInt(this.value));
                                }
                            });

                            if (data["page"] > 1) {
                                $("#first_pager").click(function () {
                                    MyTables(element, action, 1)
                                });

                                $("#prev_pager").click(function () {
                                    var page = parseInt(data["page"]) - 1;
                                    MyTables(element, action, page)
                                });
                            }

                            if (data["page"] < data["total"]) {
                                $("#next_pager").click(function () {
                                    var page = parseInt(data["page"]) + 1;
                                    MyTables(element, action, page)
                                });

                                $("#last_pager").click(function () {
                                    MyTables(element, action, data["total"])
                                });
                            }
                        }
                    }
                );
            }

            //销售量额排名
            function ShowTables(data) {
                var strHTM = "";
                strHTM += "<table width=\"973\" border=\"0\" cellpadding=\"0\" cellspacing=\"2\">";
                strHTM += "	<tr>";
                strHTM += "		<td width=\"36\" class=\"name\">ID</td>";
                strHTM += "		<td width=\"130\" class=\"name\">商品编号</td>";
                strHTM += "		<td width=\"209\" class=\"name\">商品名称</td>";
                strHTM += "		<td width=\"180\" class=\"name\">供应商</td>";
                strHTM += "		<td width=\"120\" class=\"name\">所属分类</td>";
                strHTM += "		<td width=\"70\" class=\"name\">规格</td>";
                strHTM += "		<td width=\"90\" class=\"name\">采购量</td>";
                strHTM += "		<td width=\"120\" class=\"name\">采购总额</td>";
                strHTM += "	</tr>";

                var page = 0, total = 0, records = 0;

                if (data != null) {
                    var irow = data["rows"].length;
                    for (var i = 0; i < irow; i++) {
                        strHTM += "	<tr>";
                        for (var j = 0; j < data["rows"][i]["cell"].length; j++) {
                            strHTM += "		<td>" + data["rows"][i]["cell"][j] + "</td>";
                        }
                        strHTM += "	</tr>";
                    }
                    page = data["page"];
                    total = data["total"];
                    records = data["records"];
                }
                strHTM += "</table>";

                strHTM += "<div class=\"page\">";
                
               
                //strHTM += "	<a href=\"javascript:void(0);\" id=\"prev_pager\"> < </a>";
            
                //strHTM += "	<a class=\"on\">" + page + "/" + total + "</a>";
                //strHTM += "	<a href=\"javascript:void(0);\" id=\"next_pager\"> > </a>";
                if (records > 0) {
                    strHTM += "	<a href=\"javascript:void(0);\" id=\"prev_pager\"> < </a>";
                    strHTM += "	<a class=\"on\">" + page + "/" + total + "</a>";
                    strHTM += "	<a href=\"javascript:void(0);\" id=\"next_pager\"> > </a>";
                }
                else {
                    strHTM += "暂无数据";
                }

              
                strHTM += "</div>";

                return strHTM;
            }

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
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position">当前位置 >  <a href="/member/index.aspx">我是买家</a> > <strong>统计报表</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="partd_1">
                    <div class="pd_left">                     
                            <%=member.Member_Left_HTML(2,1) %>                     
                    </div>
                    <div class="pd_right">
                        <div class="blk14_1" style="margin-top: 0px;">
                            <h2 style="background-color: #ff6600">统计报表</h2>

                            <div class="blk07_sz">
                                <h2 style="background-color: none !important">
                                    <ul class="list02">
                                        <li data="purchase" class="on">采购额统计</li>
                                        <li data="orderscounts">订单量统计</li>
                                        <li data="purchaseranking">采购量额排名</li>
                                    </ul>
                                    <div class="clear"></div>
                                </h2>

                                <div class="b14_1_main" id="purchase" style="border:0px;">

                                    <form id="frm_purchase" action="?">
                                        <input type="hidden" name="action" value="purchase" />
                                        <div class="b07_info" style="padding-left:10px;padding-bottom:10px;">
                                            分析时段：
                                <%
                                    Response.Write("<input value=\"" + startdate + "\" id=\"startdate_purchase\" name=\"startDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                    Response.Write(" 至 <input value=\"" + enddate + "\" id=\"enddate_purchase\" name=\"endDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                %>
                                            <a href="javascript:;" id="btn_purchase">分 析</a>

                                        </div>
                                    </form>

                                    <div class="b07_info02">
                                        <div id="detail_purchase" style="height: 400px;"></div>
                                    </div>
                                </div>

                                <div class="b14_1_main" id="orderscounts" style="display: none; border:0px;">

                                    <form id="frm_orderscounts" action="?">
                                        <input type="hidden" name="action" value="orderscounts" />
                                        <div class="b07_info" style="padding-left:10px;padding-bottom:10px;">
                                            分析时段：
                                    
                                     <%
                                         Response.Write("<input value=\"" + startdate + "\" id=\"startdate_orderscounts\" name=\"startDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                         Response.Write(" 至 <input value=\"" + enddate + "\" id=\"enddate_orderscounts\" name=\"endDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                     %>

                                            <a href="javascript:;" id="btn_orderscounts">分 析</a>
                                        </div>
                                    </form>

                                    <div class="b07_info02">
                                        <div id="detail_orderscounts" style="height: 400px;"></div>
                                    </div>
                                </div>

                                <div class="b07_main" id="purchaseranking" style="display: none;">
                                    <form name="frm_purchaseranking" id="frm_purchaseranking">
                                        <input type="hidden" name="action" value="purchaseranking" />
                                        <div class="b07_info" style="padding-left:10px;padding-bottom:10px;">
                                            分析时段：
                                <%
                                    Response.Write("<input value=\"" + startdate + "\" id=\"startdate_purchaseranking\" name=\"startDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                    Response.Write(" 至 <input value=\"" + enddate + "\" id=\"enddate_purchaseranking\" name=\"endDate\" type=\"text\" readonly=\"readonly\" class=\"input01\" />");
                                %>

                                            <a href="javascript:;" id="btn_purchaseranking">分 析</a>

                                        </div>
                                    </form>

                                    <div class="b14_1_main" id="detail_purchaseranking" style="border:0px;"></div>

                                </div>

                            </div>
                        </div>
                    </div>
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
