<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();

    int cate_id = tools.CheckInt(Request["cate_id"]);
    FriendlyLinkCateInfo entity = cms.GetFriendlyLinkCateInfoByID(cate_id);

    Session["Position"] = "Home";
    string about_title = "关于我们";
    string sign = "";
    sign = tools.CheckStr(Request["sign"]);
    if (sign == "")
    {
        sign = "aboutus";
        about_title = "关于我们";
    }
    AboutInfo aboutinfo = cms.GetAboutBySign(sign);
    if (aboutinfo != null)
    {
        if (aboutinfo.About_IsActive == 1)
        {
            about_title = aboutinfo.About_Title;
        }
        else
        {
            Response.Redirect("/about/index.aspx");
            about_title = "关于我们";
        }
    }
    else
    {
        Response.Redirect("/about/index.aspx");
        about_title = "关于我们";
    }
    if (sign == "contactus")
    {
        Session["Position"] = sign;
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=about_title + " - " + pub.SEO_TITLE()%></title>

    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
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
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/hdtab.js" type="text/javascript"></script>
      
    <style type="text/css">
        .b27_main ul li {
            float: left;
            margin: 0 0 10px 10px;
            padding: 0;
            width: 110px;
            text-align: center;
        }

            .b27_main ul li a img {
                height: 60px;
                width: 1120px;
            }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
    <script src="/scripts/1.js" type="text/javascript"></script>
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
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="content" style="margin-top: 0;">
        <div class="content_main">
            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/index.aspx">首页</a> > <strong><%=about_title%></strong></div>
            <!--位置说明 结束-->
            <div class="partk">
                <div class="pk_left">
                    <div class="blk26">
                        <h2>关于我们</h2>
                        <div class="b26_main">
                            <ul>
                                <%=cms.Help_About_LinkNav(sign)%>
                                <li class="on"><a href="/abount/index_link.aspx" style="cursor: pointer;">友情链接</a></li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="pk_right">
                    <div class="blk27">
                        <h2>友情链接</h2>
                        <div class="b27_main">
                            <%cms.FriendlyLink_Detail1(entity);%>
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
