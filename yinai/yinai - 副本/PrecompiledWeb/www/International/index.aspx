<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/englishtop_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/englishbottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%    Public_Class pub = new Public_Class();
      ITools tools = ToolsFactory.CreateTools();
      Supplier supplier = new Supplier();
      CMS cms = new CMS();

      AD ad = new AD();

      Statistics statistics = new Statistics();
      Session["Position"] = "Home";

      Product product = new Product();
      Bid MyBid = new Bid();
      Logistics MyLogistics = new Logistics();


    
                                                                  
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>English Home</title>
    <link rel="stylesheet" type="text/css" href="/css/index.css" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>

    <script type="text/javascript" src="/scripts/hdtab2.js"></script>


    <script type="text/javascript" src="/scripts/member.js"></script>
    <%--  <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script src="scripts/layer/layer.js"></script>


    <script src="/scripts/MSClass.js"></script>
    <script src="/scripts/common.js"></script>

    <script type="text/javascript" src="/scripts/hdtab.js"></script>

    <script type="text/javascript">

        window.onload = function () {


            var SDmode1 = new scrollDoor();
            SDmode1.sd(["a01", "a02", "a03", "a04", "a05", "a06"], ["aa01", "aa02", "aa03", "aa04", "aa05", "aa06"], "on", " ");


            var SDmode2 = new scrollDoor();
            SDmode2.sd(["d01", "d02"], ["dd01", "dd02"], "on", " ");


        }

    </script>
    <style type="text/css">
          html,body,ul,ol,li,p,h1,h2,h3,h4,h5,h6,form,fieldset,img,div,dl,dt,dd{margin:0;padding:0;border:0;font-family:Arial;}
    </style>

</head>
<body>
    <uctop:top runat="server" ID="HomeTop" IsIndex="true" />
    <!--轮播 开始-->
    <div class="banner" style="height: 442px;">
        <!--flash begin-->
        <div id="internationalflash">
            <div style="display: block;" id="internationalflash1" target="_blank"><a href="#" target="_blank"></a></div>
            <div style="display: none;" id="internationalflash2" target="_blank"><a href="#" target="_blank"></a></div>
            <div style="display: none;" id="internationalflash3" target="_blank"><a href="#" target="_blank"></a></div>
            <div style="display: none;" id="internationalflash4" target="_blank"><a href="#" target="_blank"></a></div>
            <%--	<div style="display: none;"  id="flash5" target="_blank" ><a href="#" target="_blank"></a></div>--%>
            <div class="internationalflash_bar">
                <div class="dq" id="f1" onclick="changeflash(1)">1</div>
                <div class="no" id="f2" onclick="changeflash(2)">2</div>
                <div class="no" id="f3" onclick="changeflash(3)">3</div>
                <div class="no" id="f4" onclick="changeflash(4)">4</div>
                <%--<div class="no" id="f5" onclick="changeflash(5)">5</div>--%>
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
                //$(".item5").hover(function () { $("#tit_fc5").slideDown("normal"); }, function () { $("#tit_fc5").slideUp("fast"); });
            });
            var currentindex = 1;
            $("#flashBg").css("background-color", $("#englishflash1").attr("name"));
            function changeflash(i) {
                currentindex = i;
                for (j = 1; j <= 4; j++) {//此处的5代表你想要添加的幻灯片的数量与下面的5相呼应
                    if (j == i) {
                        $("#internationalflash" + j).fadeIn("normal");
                        $("#internationalflash" + j).css("display", "block");
                        $("#f" + j).removeClass();
                        $("#f" + j).addClass("dq");
                        $("#flashBg").css("background-color", $("#flash" + j).attr("name"));
                    }
                    else {
                        $("#internationalflash" + j).css("display", "none");
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
                currentindex = currentindex >= 4 ? 1 : currentindex + 1;//此处的5代表幻灯片循环遍历的次数
                changeflash(currentindex);
            }
            $(document).ready(function () {
                $(".internationalflash_bar div").mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });
                startAm();
            });
        </script>
    </div>
    <!--轮播 结束-->
    <div class="clear"></div>

    <%--轮播大图下方正文部分 开始--%>
    <div class="En_info1" style="padding-top: 0px;">
        <div class="En_info1_main" style="margin: 0 auto; width: 1076px;">
            <%=cms.GetEnglishHomeBigPicBelowArticle() %>
        </div>
    </div>
    <%--轮播大图下方正文部分 结束--%>

    <%--正文下方  两个文章广告位 开始--%>
    <div class="En_info1">
        <div class="En_info1_main" style="width: 1076px;">
            <%=cms.GetEnglishHomeBigPicBelow() %>
        </div>
    </div>
    <div class="clear"></div>
    <%--正文下方  两个文章广告位  结束--%>

 <%-- Product 开始--%>
    <div class="En_info2">
        <div class="En_info2_main">
            <%=cms.GetEnglishHomeProductShow() %>
        </div>
    </div>
    <div class="clear"></div>
    <%-- Product 结束--%>

      <%--Service 开始--%>
    <div class="En_info3">
        <a name="En_info3"></a>
        <div class="En_info3_main">
            <div class="En_info3_nav">
                <span class="En_info3_nav_left" style="font-size: 30px;">Service</span>
            </div>
            <%=cms.GetEnglishHoneServerDistance() %>
        </div>
    </div>
    <div class="clear"></div>
    <%--Service 结束--%>

    <%--Contact Us  开始--%>
    <div class="En_info4">
        <div class="En_info4_main">
            <a name="En_info4"></a>
            <div class="En_info4_nav">
                <span class="En_info4_nav_left" style="font-size: 30px;">Contact Us</span>
            </div>
            <div class="En_info4_list">
                <div class="En_info4_left">
                    <p class="En_info4_list_1">BEI JING EASYNAI Information Technology Co.Ltd.</p>
                   <%-- <p class="En_info4_list_1">Room 306,Haidian District Venture Plaza,Beijing</p>--%>
                    <p class="En_info4_list_2">Room 306,Haidian District Venture Plaza,Beijing</p>
                    <p class="En_info4_list_3">400-8108-802</p>
                    <p class="En_info4_list_4">www.easynai.com</p>
                    <p class="En_info4_list_5">bjeasynai@163.com</p>

                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <%--Contact Us  结束--%>



    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>
