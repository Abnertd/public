<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

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

      string Keywords = tools.NullStr(Application["Site_Keyword"]);
      string Description = tools.NullStr(Application["Site_Description"]);


      DateTime tmCur = DateTime.Now;
      //string CurtTime = "您";
      string Supplier_Name = "";
      //定义登录会员对应商家的审核状态,若审核成功  招标、物流板块信息显示，否则不显示
      int Supplier_AuditStatus = -1;
      //if (tmCur.Hour < 8 || tmCur.Hour > 18)
      //{//晚上
      //    CurtTime = "晚上好";
      //}
      //else if (tmCur.Hour > 8 && tmCur.Hour < 12)
      //{//上午
      //    CurtTime = "上午好";
      //}
      //else
      //{//下午
      //    CurtTime = "下午好";
      //}




      int member_id = int.Parse(tools.NullStr(Session["member_id"]).ToString());
      int Member_SupplierID = -1;
      MemberInfo memberinfo = new Member().GetMemberByID();

      if (memberinfo != null)
      {
          Member_SupplierID = memberinfo.Member_SupplierID;
      }
      SupplierInfo supplierinfo = supplier.GetSupplierByID(Member_SupplierID);

      if (supplierinfo != null)
      {
          Supplier_AuditStatus = supplierinfo.Supplier_AuditStatus;
          Supplier_Name = supplierinfo.Supplier_CompanyName;
      }

      bool Meber_Islogined = tools.NullStr(Session["member_logined"]) == "True";
                 
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/flash01.css" rel="stylesheet" type="text/css" />
    <link href="/css/hi_icon.css" rel="stylesheet" type="text/css" />
    <link href="../layer.m/layer.m.css" rel="stylesheet" />

    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>



    <script type="text/javascript" src="/scripts/hdtab2.js"></script>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <script src="/scripts/layer/layer.js"></script>
    <script src="/scripts/MSClass.js"></script>
    <script src="/scripts/common.js"></script>

    <script type="text/javascript" src="/scripts/hdtab.js"></script>



    <style type="text/css">
        #member_btn {
            background-color: #ff6600;
            border-bottom: 2px solid #e74c00;
            clear: both;
            color: #fff;
            display: block;
            float: left;
            font-size: 15px;
            font-weight: normal;
            height: 36px;
            line-height: 36px;
            text-align: center;
            width: 237px;
        }

        .f2_right ul {
            width: 270px;
            margin: 0;
            padding: 0;
            list-style: none;
        }

            .f2_right ul li {
                float: left;
                /*width: 67.5px;*/
                font-size: 12px;
                background-color: #ff6600;
                height: 54px;
                line-height: 54px;
                color: #fff;
                text-align: center;
            }

        .top_scroll .scroll_content ul li span {
            float: left;
            width: 269px;
            font-size: 12px;
            background-color: #fff;
            height: 56px;
            line-height: 56px;
            color: #808080;
            text-align: center;
        }
        table.table_001, tr, td, th {
   margin-bottom: 15px;
        }

        /*.table_001  tr td th{
          margin:  4px auto;
           
        }*/

            .table_001 tr td {
                line-height: 40px;
            }

                .table_001 tr td img {
                    margin: 0 20px 10px 20px;
                }
    </style>
    <script type="text/javascript">




        $(document).ready(function () {


            var j = 1;
            $(".handle").each(function () {
                if ($.browser.msie && ($.browser.version <= "8.0")) {
                    $(this).children("p").html(j);
                    j++;
                }
                else {
                    var i = $(this).attr("id");
                    $(this).children("p").html(i);
                }
            })

            $(".handle").click(function () {
                if (!$(this).siblings(".slide").is(":visible")) {
                    $(this).addClass("select");
                    $(this).siblings(".slide").animate({ width: "show" });
                    $(this).parent().siblings().children(".slide").animate({ width: "hide" });
                    $(this).parent().siblings().children(".handle").removeClass("select");
                }
                else {
                    $(this).siblings(".slide").animate({ width: "hide" });
                    $(this).removeClass("select");
                }
            })
        })


        //示范一个公告层
        function SignUpNow() {
            layer.open({
                type: 2
   , title: false //不显示标题栏
                // , closeBtn: false
   , area: ['480px;', '340px']

   , shade: 0.8
   , id: 'LAY_layuipro' //设定一个id，防止重复弹出
   , resize: false
   , btnAlign: 'c'
   , moveType: 1 //拖拽模式，0或者1
                //, content: $("/Bid/SignUpPopup.aspx")
       , content: ("/Bid/SignUpPopup.aspx")
            });
        }


    </script>

    <script type="text/javascript">;
        $(function () {
            var len2 = $(".lst2 li").length //最大数量
            var index2 = 0;
            var picTimer2;
            showtab2(index2);

            //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放

            $(".blk22").hover(function () {
                clearInterval(picTimer2);
            }, function () {
                picTimer2 = setInterval(function () {
                    showtab2(index2);
                    index2++;
                    if (index2 > len2) { index2 = 0; showtab2(index2); }
                }, 3000); //此4000代表自动播放的间隔，单位：毫秒
            }).trigger("mouseleave");


            //显示图片函数，根据接收的index值显示相应的内容

            function showtab2(index2) { //普通切换值
                $(".lst2 li").removeClass("on").eq(index2).addClass("on"); //为当前的按钮切换到选中的效果
                $(".lst2main").hide().eq(index2).show();
            }

            //标准滑动门

            $(".lst2 li").each(function (c) {
                $(this).hover(function () {
                    $(".lst2 li").removeClass('on')
                    $(this).addClass('on');
                    $(".lst2main").hide(); $(".lst2main").eq(c).show();
                })
            })
        })




    </script>
    <script type="text/javascript" src="/scripts/modernizr.custom.js"></script>


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
</head>

<body>

    <uctop:top runat="server" ID="HomeTop" IsIndex="true" />


    <!--主体 开始-->

    <div class="big_content">
        <div class="home_content">
            <div class="parta">
                <div class="pa_left">
                    <!--轮播 开始-->
                    <div class="banner" style="height: 320px; overflow: hidden;">
                        <!--flash begin-->
                        <div id="flash" class="slideShow">
                            <%=ad.AD_Show("Home_Scroll", "", "tradeindex_scroll", 0)%>
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
                            $("#flashBg").css("background-color", $("#flash1").attr("name"));
                            function changeflash(i) {
                                currentindex = i;
                                for (j = 1; j <= 4; j++) {//此处的5代表你想要添加的幻灯片的数量与下面的4相呼应
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
                            var timerID;
                            function startAm() {
                                timerID = setInterval("timer_tick()", 3000);//8000代表间隔时间设置
                            }
                            function stopAm() {
                                clearInterval(timerID);
                            }
                            function timer_tick() {
                                currentindex = currentindex >= 4 ? 1 : currentindex + 1;//此处的5代表幻灯片循环遍历的次数
                                changeflash(currentindex);
                            }
                            function lwt_out() {
                                startAm();
                            }
                            function lwt(id) {
                                clearInterval(timerID);
                            }
                            $(document).ready(function () {
                                $(".flash_bar div").mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });
                                startAm();
                            });
                        </script>
                    </div>
                    <!--轮播 结束-->
                    <div class="blk03">
                        <ul>
                            <li><%=ad.AD_Show("Home_Scroll_Foot", "1", "cycle", 0)%></li>
                            <li><%=ad.AD_Show("Home_Scroll_Foot", "2", "cycle", 0)%></li>
                            <li><%=ad.AD_Show("Home_Scroll_Foot", "3", "cycle", 0)%></li>

                        </ul>
                        <div class="clear"></div>
                    </div>
                </div>
                <div class="pa_right">
                    <div class="blk04">
                        <%  if (!Meber_Islogined)
                            {
                              
                        %>

                        <div class="b04_info01">
                            <ul>
                                <li style="border-right: 1px solid #eeeeee;"><a href="/register.aspx" target="_blank">
                                    <img src="/images/icon16.jpg">免费注册</a></li>
                                <li><a href="/login.aspx" target="_blank">
                                    <img src="/images/icon17.jpg">登 录</a></li>
                            </ul>
                            <div class="clear"></div>
                        </div>
                        <form id="loginform" action="/member/login_do.aspx" method="post" onkeydown="if(event.keyCode==13){document.logform.submit();}">
                            <div class="b04_info02">
                                <p>
                                    <input name="Member_UserName" id="Member_UserName" type="text" style="line-height: 38px;" placeholder="昵称/手机号/邮箱" class="input01" />
                                </p>
                                <p>
                                    <input name="Member_Password" id="Member_Password" type="password" placeholder="请输入6～20位密码" class="input02" />
                                </p>

                                <p>
                                    <input type="text" class="input03" name="verifycode" id="verifycode" placeholder="请输入验证码" />
                                    <a href="javascript:void(0);" onclick="$('#supplier_verify_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());">
                                        <img src="/Public/verifycode.aspx" width="65" height="32" id="supplier_verify_img" /></a>
                                </p>

                                <p><a href="javascript:;" id="member_btn" onclick="$('#loginform').submit();" class="login">进入我的易耐网</a></p>
                                <input type="hidden" name="action" value="indexlogin" />
                            </div>
                        </form>




                        <%}
                            else
                            { %>


                        <table cellpadding="0" cellspacing="0" border="0" width="100%;" class="table_001">
                         
                            <tr>
                                <% if (memberinfo != null)
                                   {                                       
                                %>

                                <td colspan="3" style="padding-top: 10px;font-size:15px; padding-left: 16px; text-align: center; color: #ff6600;">
                                    <%--      <img src="<%=pub.FormatImgURL(memberinfo.MemberProfileInfo.Member_HeadImg, "fullpath") %> " style="height: 40px; width: 40px; float: left;" />--%>
                                    Hi，<%=tools.CutStr( Supplier_Name,20) %></td>
                                <%} %>
                            </tr>

                            <tr>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "unconfirm") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=unconfirm">待确认</a></td>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "payment") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=payment">待支付</a></td>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "delivery") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=delivery">待发货</a></td>


                            </tr>
                            <tr>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "accept") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=accept">待签收</a></td>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "success") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=success">交易成功</a></td>
                                <td style="width: 26px; text-align: center; font-size: 14px;"><span style="color: #ff6600; display: block; height: 19px;"><%=new Member().Member_Order_Count(member_id, "faiture") %></span><a href="/member/order_list.aspx?orderDate=1&orderStatus=faiture">交易失败</a></td>
                            </tr>
                        </table>
                        <%--<div class="b04_info03">
                            <h2>
                                <ul>
                                    <li id="b01" class="on3"><span>
                                        <img src="images/icon24.png" width="36" height="36" /></span></li>
                                    <li id="b02"><span>
                                        <img src="images/icon24.png" width="36" height="36" /></span></li>
                                    <li id="b03"><span>
                                        <img src="images/icon24.png" width="36" height="36" /></span></li>
                                    <li id="b04"><span>
                                        <img src="images/icon24.png" width="36" height="36" /></span></li>
                                </ul>
                                <div class="clear"></div>
                            </h2>
                            <div class="b04_info03_box" id="bb01">
                                <ul>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon18.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon19.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon20.jpg">
                                        </div>
                                    </a></li>
                                </ul>
                                <div class="clear"></div>
                            </div>
                            <div class="b04_info03_box" id="bb02" style="display: none;">
                                <ul>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon18.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon19.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon20.jpg">
                                        </div>
                                    </a></li>
                                </ul>
                                <div class="clear"></div>
                            </div>
                            <div class="b04_info03_box" id="bb03" style="display: none;">
                                <ul>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon18.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon19.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon20.jpg">
                                        </div>
                                    </a></li>
                                </ul>
                                <div class="clear"></div>
                            </div>
                            <div class="b04_info03_box" id="bb04" style="display: none;">
                                <ul>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon18.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon19.jpg">
                                        </div>
                                    </a></li>
                                    <li><a href="#" target="_blank">
                                        <div class="img_box">
                                            <img src="images/icon20.jpg">
                                        </div>
                                    </a></li>
                                </ul>
                                <div class="clear"></div>
                            </div>
                        </div>--%>


                        <%} %>

                        <div class="b04_info04" style="padding:12px 5px 19px 5px">
                            <h2><a href="/notice/index.aspx?cate_id=3" target="_blank">更多 ></a>平台公告</h2>
                            <ul>
                                <%=cms.Home_Top_Notice(3, 3) %>
                            </ul>
                        </div>
                        <div class="b04_info05">
                            <dl>
                                <dt><span><%=DateTime.Now.Year %><i><%=DateTime.Now.Month %></i></span><strong><%=DateTime.Now.Day %></strong></dt>
                                <dd>
                                    <p>今日上架商品<strong><%=product.Home_TodayProduct_Count() %></strong>件</p>
                                    <p>总商品数<strong><%=product.Home_TotalProduct_Count() %></strong>件</p>
                                </dd>
                                <div class="clear"></div>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>

            <div class="partb">
                <%=ad.AD_Show("Home_NewProduct_AboveImg", "", "cycle", 0)%>
            </div>


            <!--楼层 开始-->

            <!--1F 商城中心 开始-->

            <div class="f">
                <div class="f2_left">
                    <%=product.Home_1Floor(14,"首页商品推荐(1F商城中心)") %>
                </div>

                <div class="f2_right" style="height: 564px;">
                    <ul>
                        <li style="float: left; width: 110px;"><span>产品</span></li>
                        <li style="float: left; width: 40px;"><span>状态</span></li>
                        <li style="float: left; width: 60px;"><span>数量(吨)</span></li>
                        <li style="float: left; width: 60px;"><span>时间</span></li>
                    </ul>

                    <div class="word" id="top_scroll">
                        <div id="scroll_content" style="width: 268px; height: 248.5px">
                            <%=cms.Home_2F_Scroll(39) %>
                        </div>
                        <%--    /************control_enabel：是否启用按钮控制ID************/
    /************direction：滚动方向：0上 1下 2左 3右************/
    /************left_div：左控制按钮ID************/
    /************right_div：右控制按钮ID************/
    /************scroll_body：循环主体容器ID************/
    /************scroll_content：循环主体内容容器ID************/
    /************total_width：循环体总宽度************/
    /************total_height：循环体总高度************/
    /************scroll_width：每次循环宽度（0为翻屏）************/
    /************scroll_speed：循环速度步长（越大越慢）************/--%>
                        <%--  <script type="text/javascript">srcoll_left_right_Control(false, 0, "", "", "top_scroll", "scroll_content", 270, 446, 56.1, 10, 1);</script>--%>
                        <script type="text/javascript">srcoll_left_right_Control(false, 0, "", "", "top_scroll", "scroll_content", 270, 917, 55, 10, 1);</script>
                    </div>
                </div>
            </div>
            <!--1F 商城中心 结束-->

            <!--2F供应链金融 开始  -->
            <div class="f2">
                <div class="f2_left">


                    <h2><a href="/Financial/index.aspx" target="_blank" class="more">更多 ></a><strong><i>2F</i>供应链金融</strong><b><a href="/Financial/index.aspx">金融中心</a></b></h2>
                    <div class="f2_main">
                        <ul>

                            <li><a href="/Financial/index.aspx#part03_left_title" target="_blank">
                                <img src="images/img17_2.png" onmouseover="this.src='images/img17.png'" onmouseout="this.src='images/img17_2.png'" width="263" height="408" style="color: #ff6600" /></a></li>
                            <li><a href="/Financial/index.aspx#part03_left_title2" target="_blank">
                                <img src="images/img18_2.png" onmouseover="this.src='images/img18.png'" onmouseout="this.src='images/img18_2.png'" width="263" height="408" /></a></li>
                            <li><a href="/Financial/index.aspx#part03_left_title3" target="_blank">
                                <img src="images/img19_2.png" onmouseover="this.src='images/img19.png'" onmouseout="this.src='images/img19_2.png'" width="263" height="408" /></a></li>
                        </ul>
                    </div>

                </div>




                <div class="f2_right">
                    <h2>服务承诺</h2>
                    <div class="zbpm">
                        <p>
                            <%=ad.AD_Show("Home_2F_Right_AD", "", "cycle", 0)%>
                        </p>

                    </div>
                </div>
            </div>
            <!--2F供应链金融 结束  -->




            <!-- 3F招标拍卖 开始-->
            <div class="f2">
                <div class="f2_left">
                    <h2><a href="/bid/" class="more">更多 ></a><strong><i>3F</i>招标拍卖</strong><b><a href="/bid/" id="d01" class="on">招标大厅</a><a href="/bid/auction.aspx" id="d02">拍卖大厅</a></b></h2>

                    <div class="f2_main2">
                        <table cellpadding="0" cellspacing="0" class="table_style" border="0" width="100%" id="dd01">
                            <thead>
                                <tr>
                                    <td width="153">招标公告</td>
                                    <td width="152">招标单位</td>
                                    <%--  <td width="152">交货时间</td>--%>
                                    <td width="152">交货数量</td>
                                    <td width="152">报价开始时间</td>
                                    <td width="152">报价结束时间</td>
                                    <td width="134">&nbsp;</td>
                                </tr>
                            </thead>
                            <tbody>
                                <%MyBid.Bid_Indexs(0); %>
                            </tbody>
                        </table>
                        <table cellpadding="0" cellspacing="0" class="table_style" border="0" width="100%" id="dd02" style="display: none;">
                            <thead>
                                <tr>
                                    <td width="303">拍卖公告</td>
                                    <td width="152">拍卖用户</td>

                                    <td width="152">报价开始时间</td>
                                    <td width="152">报价结束时间</td>
                                    <td width="134">&nbsp;</td>
                                </tr>
                            </thead>
                            <tbody>
                                <%MyBid.Bid_Indexs(1); %>
                            </tbody>
                        </table>
                    </div>


                </div>
                <div class="f2_right">
                    <h2>招标拍卖</h2>
                    <div class="zbpm">
                        <p>
                            <%--  <img src="/images/img7.jpg" width="254" height="124" />--%>
                            <%=ad.AD_Show("Home_3F_Right_AboveAD","","cycle",0) %>
                        </p>
                        <p><a href="/member/bid_add.aspx" class="btn02">我要招标</a></p>
                    </div>
                    <div class="zbpm" style="border: none;">
                        <p>
                            <%-- <img src="/images/img8.jpg" width="254" height="124" />--%>
                            <%=ad.AD_Show("Home_3F_Right_BelowAD","","cycle",0) %>
                        </p>
                        <p><a href="/supplier/auction_add.aspx" class="btn02">我要拍卖</a></p>
                    </div>
                </div>
            </div>
            <!-- 3F招标拍卖 结束-->







            <!--4F行情资讯 开始-->
            <div class="f2">

                <div class="f2_left">
                    <h2><a href="/article/" target="_blank" class="more">更多 ></a><strong><i>4F</i>行情资讯</strong></h2>



                    <div class="f2_main2">
                        <div class="new_left">
                            <div class="new_1">
                                <%=cms.Home_4F_InterMarket() %>
                            </div>
                            <div class="new_2">
                                <ul style="float: left;">
                                    <%=cms.Home_4F_InterMarketBottomArticles("首页4F大图文章下面左侧3篇文章") %>
                                </ul>
                                <ul style="float: right;">


                                    <%=cms.Home_4F_InterMarketBottomArticles("首页4F大图文章下面右侧3篇文章") %>
                                </ul>
                            </div>
                        </div>
                        <div class="new_right">





                            <%=cms.Home_4F_InterMarketRightArticles("首页4F大图右侧上面3篇文章") %>

                            <%--    <%=cms.Home_4F_InterMarketRightArticles("首页4F大图右侧下面1篇文章") %>--%>
                        </div>

                    </div>



                </div>
                <div class="f2_right">
                    <h2>独家数据</h2>

                    <div class="zbpm" style="height: 345px; border-bottom: none;">
                        <p>
                            <%=ad.AD_Show("Home_Statistic", "", "cycle", 1) %>
                        </p>
                    </div>
                </div>
            </div>
            <!--4F国际市场 结束-->


            <!--5F仓储物流 开始-->
            <div class="f2">
                <div class="f2_left">
                    <h2><a href="/Logistics/" class="more">更多 ></a><strong><i>5F</i>仓储物流</strong><b>
                        <a href="/supplier/order_list.aspx?orderDate=1&orderStatus=LogisticsDelivery">我要找车</a><a href="/Logistics/">我要发车</a></b></h2>


                    <%if ((Supplier_AuditStatus == 1) && (Meber_Islogined))
                      {
                          
                    %>
                    <div class="f2_main2">
                        <table cellpadding="0" cellspacing="0" class="table_style" border="0" width="100%">
                            <col width="26%" />
                            <col width="18%" />
                            <col width="18%" />
                            <col width="12%" />
                            <col width="10%" />
                            <col width="16%" />
                            <thead>
                                <tr>
                                    <td>货物名称</td>
                                    <td>发货地址</td>
                                    <td>收货地址</td>
                                    <td>发货时间</td>
                                    <td>数量(单位)</td>
                                    <td>操作</td>
                                </tr>
                            </thead>

                            <%MyLogistics.IndexList(); %>
                        </table>

                    </div>
                    <%} %>
                </div>
                <div class="f2_right">
                    <%--  <h2>招标拍卖</h2>--%>
                    <div class="zbpm" style="height: 319px; border-bottom: 0px; margin-bottom: 0px; padding: 0px;">
                        <p>
                            <%=ad.AD_Show("Home_5F_Right_AD", "", "cycle", 1) %>
                        </p>

                    </div>

                </div>
            </div>
            <!--5F仓储物流 结束-->
            <!--楼层 结束-->

        </div>
    </div>

    <div id="leftsead">
        <ul>
            <li>
                <%--<a href="http://wpa.qq.com/msgrd?v=3&uin=800022936&site=qq&menu=yes" target="_blank">--%>


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


    <!--主体 结束-->







    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />

    <!--尾部 结束-->

    <script type="text/javascript">

        window.onload = function () {
            if ("<% =tools.NullStr(Session["member_logined"].ToString())%>" == "True") {
                var sdmode = new scrollDoor();
                sdmode.sd(["b01", "b02", "b03", "b04"], ["bb01", "bb02", "bb03", "bb04"], "on3", " ");


                var SDmode1 = new scrollDoor();
                SDmode1.sd(["a01", "a02", "a03", "a04", "a05", "a06"], ["aa01", "aa02", "aa03", "aa04", "aa05", "aa06"], "on", " ");


                var SDmode2 = new scrollDoor();
                SDmode2.sd(["d01", "d02"], ["dd01", "dd02"], "on", " ");
            } else {
                var SDmode1 = new scrollDoor();
                SDmode1.sd(["a01", "a02", "a03", "a04", "a05", "a06"], ["aa01", "aa02", "aa03", "aa04", "aa05", "aa06"], "on", " ");


                var SDmode2 = new scrollDoor();
                SDmode2.sd(["d01", "d02"], ["dd01", "dd02"], "on", " ");

            }

        }

    </script>
</body>
</html>

