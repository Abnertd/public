<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    AD ad = new AD();
    
    ArticleInfo article = null;
    Session["Position"] = "IndustryInformation";
    string article_title = "帮助中心";
    int article_id = 0, cate_id = 0;
    string hkeyword = "";
    hkeyword = tools.CheckStr(Request["hkeyword"]);
    string default_key = "";
    string Article_Source = "";
    string Article_Author = "";
    string Article_Addtime = "";
    string Article_Content = "";
    if (hkeyword.Length == 0)
    {
        default_key = "输入需要查询的关键词";
    }
    else
    {
        default_key = hkeyword;
    }
    if (hkeyword == "输入需要查询的关键词")
    {
        hkeyword = "";
    }
    article_id = tools.CheckInt(Request["article_id"]);
    //cate_id = tools.CheckInt(Request["cate_id"]);

    article = cms.GetArticleInfoByID(article_id);
    if (article != null)
    {
        if (article.Article_IsAudit != 0)
        {
            article_title = article.Article_Title;
            cate_id = article.Article_CateID;
            Article_Source = article.Article_Source;
            Article_Author = article.Article_Author;
            Article_Addtime = article.Article_Addtime.ToShortDateString();
            Article_Content = article.Article_Content;
        }
        else
        {
            Response.Redirect("/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/index.aspx");
    }    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=article_title + " - " + pub.SEO_TITLE()%></title>
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


    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
<%--    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/hdtab.js" type="text/javascript"></script>
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
    <uctop:top ID="top" runat="server" />
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/index.aspx" target="_blank">首页</a> > <%=article_title %></div>
        <!--位置 结束-->
        <div class="banner02">
          <%--  <img src="/images/img12.jpg" width="1200" height="156" />--%>
               <%=ad.AD_Show("Article_Detail_TopPic","","cycle",0) %>
        </div>
        <div class="parte" style="border: none;">
            <div class="pe_left">
                <div class="blk19">
                    <h2><%=article_title %></h2>
                    <h3><span>来源：<%=Article_Source %></span><span>编辑：<%=Article_Author %></span><span>发布时间：<%=Article_Addtime %></span></h3>
                    <div class="b19_main">
                        <p>
                        <%=Article_Content %></p>
                    </div>
                    <%--<div class="b19_main02">分享到：<img src="/images/icon44.jpg"></div>--%>
                   <%-- <ul style="margin-top: 10px; margin-left: 95px;">
                        <li>--%>
                            <div class="b19_main" style="padding:15px 0 15px 30px;">
                                <div id="ckepop">

                                    <a href="http://www.jiathis.com/share/" class="jiathis jiathis_txt jtico jtico_jiathis" target="_blank"></a>
                                    <a class="jiathis_button_tsina">新浪微博</a>
                                    <a class="jiathis_button_renren">人人网</a>
                                </div>
                                <script type="text/javascript" src="http://v2.jiathis.com/code/jia.js" charset="utf-8"></script>
                            </div>
                      <%--  </li>
                    </ul>
                    --%>


                    <div class="b19_main03"> 
                       <%=cms.GetPreAndNext(1,article.Article_CateID)%>
                        <%=cms.GetPreAndNext(0,article.Article_CateID)%>
                         
                    </div>
                    <div class="b19_main04">
                        <b>相关文章</b>
                        <ul>
                            <%cms.GetRelateHangYeSort(7);%>
                        </ul>
                        <div class="clear"></div>
                    </div>
                </div>
            </div>
            <div class="pe_right">
                <div class="blk18_1">
                    <h2>热门新闻排行</h2>
                    <div class="b18_main_1">
                        <ul>
                            <%cms.GetHangYeSort();%>
                        </ul>
                    </div>
                </div>
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
    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
