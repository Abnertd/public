<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/englishtop_simple.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%    Public_Class pub = new Public_Class();
      ITools tools = ToolsFactory.CreateTools();
      Supplier supplier = new Supplier();
      CMS cms = new CMS();


      AD ad = new AD();

      Statistics statistics = new Statistics();
      Session["Position"] = "financialservice";
      string SET_Title = "金融页面 - " + pub.SEO_TITLE();
      string Keywords = tools.NullStr(Application["Site_Keyword"]);
      string Description = tools.NullStr(Application["Site_Description"]);

      Product product = new Product();
      Bid MyBid = new Bid();
      Logistics MyLogistics = new Logistics();


    
                                                                  
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<title>金融页面</title>--%>
      <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>
        <%=SET_Title%></title>
  
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link rel="stylesheet" type="text/css" href="/css/index.css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <script src="/scripts/1.js" type="text/javascript"></script>
    <script src="../scripts/jquery.js" type="text/javascript"></script>
<%--        <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>
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
    <%--   <uctop:top runat="server" ID="HomeTop" IsIndex="true" />--%>
    <div class="En_top">
        <div class="En_top_main">
            <div class="En_top_main_left">
                <img src="/images/en_logo1.png" width="204" height="60" />
            </div>
            <div class="En_top_main_right">
                <ul>
                    <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/Index.aspx" class="a_nav">首 页</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Category" ? " class=\"on\" " :" ") %>><a href="/product/category.aspx" class="a_nav">商城选购</a></li>

                    <li <%=(Convert.ToString(Session["Position"]) == "Bid" ? " class=\"on\" " :" ") %>><a href="/bid/">招标拍卖</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Logistics" ? " class=\"on\" " :" ") %>><a href="/Logistics/">仓储物流</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "financialservice" ? " class=\"on\" " :" ") %>><a href="/Financial/index.aspx" class="a_nav" style="color:#ea5514">金融中心</a></li>


                    <li <%=(Convert.ToString(Session["Position"]) == "IndustryInformation" ? " class=\"on\" " :" ") %>><a href="/article/Index.aspx" class="a_nav">行情资讯</a></li>
                </ul>
            </div>
        </div>
    </div>


    <!--轮播 开始-->
    <div class="banner" style="height: 442px;">
        <!--flash begin-->
        <div id="flash">
            <div style="display: block;" id="flash1" target="_blank"><a href="#" target="_blank"></a></div>
              <div  style="display: none;" id="flash2" target="_blank" ><a href="#" target="_blank"></a></div>
            <div style="display: none;"  id="flash3" target="_blank" ><a href="#" target="_blank"></a></div>
            <div style="display: none;"  id="flash4" target="_blank" ><a href="#" target="_blank"></a></div>
            <%-- <div style="display: none;"  id="flash5" target="_blank" ><a href="#" target="_blank"></a></div>--%>
        <div class="flash_bar">
                <div class="dq" id="f1" onclick="changeflash(1)">1</div>
                <div class="no" id="f2" onclick="changeflash(2)">2</div>
                <div class="no" id="f3" onclick="changeflash(3)">3</div>
                <div class="no" id="f4" onclick="changeflash(4)">4</div>
        <%--            <div class="no" id="f5" onclick="changeflash(5)">5</div>--%>
            </div>
        </div>
        <!--banner通栏自适应浏览器宽度的Banner幻灯片-->
         <script type="text/javascript">
        //<![CDATA[
        $(document).ready(function () {
            $(".item1").hover(function () { $("#tit_fc1").slideDown("normal"); }, function () { $("#tit_fc1").slideUp("fast"); });
            $(".item2").hover(function () { $("#tit_fc2").slideDown("normal"); }, function () { $("#tit_fc2").slideUp("fast"); });
            $(".item3").hover(function () { $("#tit_fc3").slideDown("normal"); }, function () { $("#tit_fc3").slideUp("fast"); });
            $(".item4").hover(function () { $("#tit_fc4").slideDown("normal"); }, function () { $("#tit_fc4").slideUp("fast"); });
            $(".item5").hover(function () { $("#tit_fc5").slideDown("normal"); }, function () { $("#tit_fc5").slideUp("fast"); });
        });
        var currentindex = 1;
        $("#flashBg").css("background-color", $("#flash1").attr("name"));
        function changeflash(i) {
            currentindex = i;
            for (j = 1; j <=4; j++) {//此处的5代表你想要添加的幻灯片的数量与下面的5相呼应
                if (j == i) {
                    $("#flash" + j).fadeIn("normal");
                    $("#flash" + j).css("display", "block");
                    $("#f" + j).removeClass();
                    $("#f" + j).addClass("dq");
                    $("#flashBg").css("background-color", $("#flash" + j).attr("name"));
                }
                else {
                    $("#flash" + j).css("display", "none");
                    $("#f" + j).removeClass();
                    $("#f" + j).addClass("no");
                }
            }
        }
        function startAm() {
            timerID = setInterval("timer_tick()", 4000);//8000代表间隔时间设置
        }
        function stopAm() {
            clearInterval(timerID);
        }
        function timer_tick() {
            currentindex = currentindex >= 4? 1 : currentindex + 1;//此处的5代表幻灯片循环遍历的次数
            changeflash(currentindex);
        }
        $(document).ready(function () {
            $(".flash_bar div").mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });
            startAm();
        });
    </script>
    </div>
    <!--轮播 结束-->
    <div class="jr_main">
        <div class="jr_main_part01">
            <div>
                <img src="/images/title4.png" width="1200" height="24" /></div>
            <div class="jr2_sssj">
                <ul>
                    <li>
                        <p>
                            <img src="/images/icon18.png" width="174" height="123" /></p>
                        <p><font>108,289,212</font><br />
                            累计融资金额</p>
                    </li>
                    <li>
                        <p>
                            <img src="/images/icon19.png" width="174" height="123" /></p>
                        <p><font>3,234,302</font><br />
                            累计投资收益</p>
                    </li>
                    <li>
                        <p>
                            <img src="/images/icon20.png" width="174" height="123" /></p>
                        <p><font>113304</font><br />
                            累计收益人数</p>
                    </li>
                </ul>
            </div>
            <div class="jr2_part02">
                <div class="jr2_part02_left">


                    <%=ad.AD_Show("market_Dynamics_NoticeLeft", "", "cycle", 0)%>
                </div>
                <div class="jr2_part02_right">
                    <h2>公告栏</h2>
                    <%=cms.EnglishMarketDynamicsNotice(6, 3) %>
                </div>
            </div>
            <div class="jr2_part03">
                <div class="jr3_part03_left">
                    <a name="part03_left_title"/>
                    <div class="part03_left_title" name="part03_left_title">商业承兑融资</div>
                    <table cellpadding="0" cellspacing="0" border="0" class="part03_left_mian">
                        <col width="33%" />
                        <col width="33%" />
                        <col width="33%" />
                        <tr>
                            <td>我有承兑<br />
                                我要兑现</td>
                            <td style="color: #333;"><font>5%</font><br />
                                年化率起步</td>
                            <td><a href="/Financial/financialservice.aspx?fianicial_type=2">
                                <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                        <tr>
                            <td>我有现金<br />
                                我要投资</td>
                            <td><font>8%</font><br />
                                年化率起步</td>
                            <td><a href="/Financial/financialservice.aspx?fianicial_type=2">
                                <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                    </table>
                </div>
                <div class="jr3_part03_right">
                    <div class="part03_right_1">
                        <p><font>商业承兑贴现</font></p>
                        <p>你的商业承兑背书给我们，我们给你现金，按照票据到期时间，收取一定比例利息，利息最低少至年化率5%。</p>
                    </div>
                    <div class="part03_right_1" style="border-bottom: none;">
                        <p><font>商业承兑投资</font></p>
                        <p>我们将已收到的商业承兑挂出，制定预期的年化收益，你有现金可以投资，到期按比例取得分红。</p>
                    </div>
                </div>
            </div>
            <%-- <%=cms.EnglishMarketDynamicsNoticeBelowArticle(4, 3) %>--%>
            <div class="jr2_part03">
                <div class="jr3_part03_left">
                    <a name="part03_left_title2"/>
                    <div class="part03_left_title2" name="part03_left_title2">货押融资</div>
                    <table cellpadding="0" cellspacing="0" border="0" class="part03_left_mian">
                        <col width="33%" />
                        <col width="33%" />
                        <col width="33%" />
                        <tr>
                            <td>我有存货<br />
                                抵押融资</td>
                            <td style="color: #333;"><font>12%</font><br />
                                年化率低至</td>
                             <td><a href="/Financial/financialservice.aspx?fianicial_type=3">
                                 <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                        <tr>
                            <td>我有期货<br />
                                提前预定</td>
                            <td><font>12%</font><br />
                                价格下浮</td>
                            <td><a href="/Financial/financialservice.aspx?fianicial_type=3">
                                 <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                    </table>
                </div>
                <div class="jr3_part03_right">
                    <div class="part03_right_1">
                        <p><font>现货融资</font></p>
                        <p>你有现货，抵给我们，我们给你现金，到期收取一定比例的利息，利息最低少至年化率12%。</p>
                    </div>
                    <div class="part03_right_1" style="border-bottom: none;">
                        <p><font>期货融资</font></p>
                        <p>你有计划性生产，可以预售下一期货物，价格比市场价更优，我们可以提前预定，价格较市场下浮10%起步。</p>
                    </div>
                </div>
            </div>
            <div class="jr2_part03">
                <div class="jr3_part03_left">
                    <a name="part03_left_title3" />
                    <div class="part03_left_title3" >应收账款融资</div>
                    <table cellpadding="0" cellspacing="0" border="0" class="part03_left_mian">
                        <col width="33%" />
                        <col width="33%" />
                        <col width="33%" />
                        <tr>
                            <td>我有债权<br />
                                提前收账</td>
                            <td style="color: #333;"><font>12%</font><br />
                                年化率低至</td>
                           <td><a href="/Financial/financialservice.aspx?fianicial_type=4">
                                <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                        <tr>
                            <td>我有债权<br />
                                三方抹账</td>
                            <td><font>0%</font><br />
                                佣金</td>
                             <td><a href="/Financial/financialservice.aspx?fianicial_type=4">
                                 <img src="/images/btn1_1.png" onmousemove="this.src='/images/btn1.png'" onmouseout="this.src='/images/btn1_1.png'" width="104" height="33" /></a></td>
                        </tr>
                    </table>
                </div>
                <div class="jr3_part03_right">
                    <div class="part03_right_1">
                        <p><font>应收账款买断</font></p>
                        <p>你将你的应收账款转给我们，我们收取一定费用，费用最低少至12%。</p>
                    </div>
                    <div class="part03_right_1" style="border-bottom: none;">
                        <p><font>三方抹账</font></p>
                        <p>你将你的应收账款信息交由我们，我们寻找上游欠款厂家，达到三方抹账的目的。</p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
              <div class="jr2_part04">
            	<div style="margin-top:50px;"><img src="/images/title5.png" width="1200" height="24" /></div>
                <div style="margin-top:50px;">
                  

                    <%=ad.AD_Show("market_Dynamics_ApplyProcedure", "", "cycle", 0)%>
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
    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>
