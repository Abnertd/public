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
    //Supplier supplier = new Supplier();
    //Orders orders = new Orders();
    Member member = new Member();
    //CMS cms = new CMS();
    Bid MyBid = new Bid();
    string Keywords = tools.NullStr(Application["Site_Keyword"]);
    string Description = tools.NullStr(Application["Site_Description"]);
    Logistics MyLoginStatus = new Logistics();
    string company_name = "", member_img = "";
    string member_nickname = tools.CheckStr(Session["member_nickname"].ToString());
    MemberInfo entity = member.GetMemberByID();
    if (entity != null && entity.MemberProfileInfo != null)
    {
        company_name = entity.MemberProfileInfo.Member_Company;
        member_img = entity.MemberProfileInfo.Member_HeadImg;

    }

    member.Member_Login_Check("/member/index.aspx");
    if (tools.NullInt(Session["member_auditstatus"]) != 1)
    {
        Response.Redirect("/member/account_profile.aspx");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我是买家 - <%= pub.SEO_TITLE()%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/flash01.css" rel="stylesheet" type="text/css" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <link href="/css/hi_icon.css" rel="stylesheet" type="text/css" />
    <%--    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <%-- <script type="text/javascript" src="/scripts/layer/layer.js"></script>
      <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>


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
    </style>

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

    </script>

    <script src="/scripts/modernizr.custom.js" type="text/javascript"></script>
    <script type="text/javascript">
        var hash = window.location.hash,
            current = 0,
            demos = Array.prototype.slice.call(document.querySelectorAll('#codrops-demos > a'));

        if (hash === '') hash = '#set-1';
        setDemo(demos[parseInt(hash.match(/#set-(\d+)/)[1]) - 1]);

        demos.forEach(function (el, i) {
            el.addEventListener('click', function () { setDemo(this); });
        });

        function setDemo(el) {
            var idx = demos.indexOf(el);
            if (current !== idx) {
                var currentDemo = demos[current];
                currentDemo.className = currentDemo.className.replace(new RegExp("(^|\\s+)" + 'current-demo' + "(\\s+|$)"), ' ');
            }
            current = idx;
            el.className = 'current-demo';
        }
    </script>

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

    <script type="text/javascript">
        //===========================点击展开关闭效果====================================
        function openShutManager(oSourceObj, oTargetObj, shutAble, oOpenTip, oShutTip) {
            var sourceObj = typeof oSourceObj == "string" ? document.getElementById(oSourceObj) : oSourceObj;
            var targetObj = typeof oTargetObj == "string" ? document.getElementById(oTargetObj) : oTargetObj;
            var openTip = oOpenTip || "";
            var shutTip = oShutTip || "";
            if (targetObj.style.display != "none") {
                if (shutAble) return;
                targetObj.style.display = "none";
                if (openTip && shutTip) {
                    sourceObj.innerHTML = shutTip;
                }
            } else {
                targetObj.style.display = "block";
                if (openTip && shutTip) {
                    sourceObj.innerHTML = openTip;
                }
            }
        }
    </script>


</head>



<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/index.aspx">首页</a> ><strong>我是买家</strong> </div>
            <div class="partd_1">

                <!--会员中心左侧具体内容 开始-->
                <div class="pd_left">

                    <% =member.Member_Left_HTML(1, 1) %>
                </div>

                <!--会员中心左侧具体内容 结束-->
                <div class="pd_right">
                    <div class="blk13_1">
                        <dl>
                            <%=member.Member_Index_Left_Info() %>
                        </dl>
                    </div>
                    <div class="blk14_1">
                        <h2>我发布的招标信息</h2>
                        <div class="b14_1_main">
                            <table width="974" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="235" class="name">公告标题</td>
                                    <td width="229" class="name">报名时间</td>
                                    <td width="229" class="name">报价时间</td>
                                    <td width="154" class="name">当前状态</td>
                                    <td width="127" style="border-right: solid 1px #eeeeee;" class="name">查看进度</td>
                                </tr>
                                <%MyBid.Bid_MemberIndexList(0); %>
                            </table>
                            <a href="/member/bid_list.aspx" class="more4">查看更多招标信息</a>
                        </div>
                    </div>
                    <div class="blk14_1">
                        <h2>我发布的物流信息</h2>
                        <div class="b14_1_main">
                            <table width="974" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="200" class="name">货物名称</td>
                                    <td width="259" class="name">发货地</td>
                                    <td width="258" class="name">收货地</td>
                                    <td width="66" class="name">数量</td>
                                    <td width="86" class="name">状态</td>
                                    <td width="106" style="border-right: solid 1px #eeeeee;" class="name">操作</td>
                                </tr>
                                <%MyLoginStatus.SupplierLogistics_IndexList(); %>
                            </table>
                            <a href="/supplier/Logistics_list.aspx" class="more4">查看更多采购信息</a>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
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

    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
