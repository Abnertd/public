<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%--<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>--%>

<%@ Register Src="~/Public/englishtop_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/englishbottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    AD ad = new AD();
    ArticleInfo article = null;
    //Session["Position"] = "IndustryInformation";
    string article_title = "InternationalArticle";
    Session["Position"] = "Product";
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
        default_key = "Please enter keywords for search";
    }
    else
    {
        default_key = hkeyword;
    }
    if (hkeyword == "Please enter keywords for search")
    {
        hkeyword = "";
    }
    article_id = tools.CheckInt(Request["id"]);
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
            Response.Redirect("/International/Index.aspx");
        }
    }
    else
    {
        Response.Redirect("/International/Index.aspx");
    }    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=article_title + " - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/hdtab.js" type="text/javascript"></script>
    <style type="text/css"> 
        html,body,ul,ol,li,p,h1,h2,h3,h4,h5,h6,form,fieldset,img,div,dl,dt,dd{margin:0;padding:0;border:0;font-family:Arial;}
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
    <style type="text/css">
        .content {
        min-height: 700px;
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
        <div class="position">Current Location > <a href="/index.aspx" target="_blank">Home</a> > <%=article_title %></div>
        <!--位置 结束-->
        <div class="banner02">
           <%-- <img src="/images/img12.jpg" width="1200" height="156" />--%>
             <%=ad.AD_Show("Article_Detail_TopPic","","cycle",0) %>
        </div>
        <div class="parte" style="border: none;">
            <div class="pe_left" style="width: 1200px;padding-top: 10px">
                <div class="blk19">
                    <h2 style="width:1200px;"><%=article_title %></h2>
                    <h3><span>Source：<%=Article_Source %></span><span>Aditor：<%=Article_Author %></span><span>Time：<%=Article_Addtime %></span></h3>
                    <div class="b19_main">
                        <p>
                            <%=Article_Content %>
                        </p>
                    </div>

                    
                </div>
            </div>
           
            <div class="clear"></div>
        </div>

    </div>
    <%--右侧浮动弹框 开始--%>
    
   
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->
    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
