<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>


<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%  
     
    ITools tools = ToolsFactory.CreateTools();
    AD ad = new AD();
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    Product product = new Product();
    Member member = new Member();
    Cart cart = new Cart();

    //member.Member_Login_Check("/product/category.aspx");
    string productviewcookie = tools.CheckStr(Request["productviewcookie"]);
    if (productviewcookie == "clear")
    {
        Request.Cookies["product_viewhistory_zh-cn"].Value = null;
    }
    string cate_name = "";
    int cate_id;
    int id = 0;
    int cate_parentid = 0;
    int cate_typeid = 0;
    string filter_value;
    string SET_Title = pub.SEO_TITLE();
    string Keywords = tools.NullStr(Application["Site_Keyword"]);
    string Description = tools.NullStr(Application["Site_Description"]);
    cate_id = tools.CheckInt(Request["cate_id"]);
    filter_value = product.product_filter_value();
    int parentid = 0;
    CategoryInfo category = product.GetCategoryByID(cate_id);

    int cate_top_id = product.Get_TopCate(cate_id);
    switch (cate_top_id)
    {
        case 230:
            Session["Position"] = "cate";
            break;
        case 231:
            Session["Position"] = "cate2";
            break;
        case 232:
            Session["Position"] = "cate3";
            break;
        case 233:
            Session["Position"] = "cate4";
            break;
        default:
            Session["Position"] = "cate";
            break;
    }


    if (category != null)
    {
        if (category.Cate_IsActive == 1)
        {
            cate_name = category.Cate_Name;
            if (category.Cate_SEO_Title == "")
            {
                SET_Title = category.Cate_Name + " - " + pub.SEO_TITLE();
            }
            else
            {
                SET_Title = category.Cate_SEO_Title;
                Keywords = category.Cate_SEO_Keyword;
                Description = category.Cate_SEO_Description;
            }
            cate_parentid = category.Cate_ParentID;
            cate_typeid = category.Cate_TypeID;
            id = cate_id;
        }
    }

    parentid = product.getCateParentID(cate_id);
    //Session["Web_Cursor"] = "Category";
    Session["Position"] = "Category";
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>商品搜索 - <%=SET_Title%></title>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <%--    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="../scripts/common1.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/cart.js"></script>
    <%-- <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>--%>
    <!--滑动门 结束-->
    <%--   <script src="/js/1.js" type="text/javascript"></script>--%>
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

        function switchTag(content) {

            if (document.getElementById(content).className == "hidecontent") {
                document.getElementById(content).className = "";
            }
            else { document.getElementById(content).className = "hidecontent"; }

        }
    </script>
    <script type="text/javascript">
        function show_hiddendiv() {
            document.getElementById("hidden_div").style.display = 'block';
            document.getElementById("_strHref").href = 'javascript:hidden_showdiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02_2.png>";

            SetCookie("extend_div", "show");
        }
        function hidden_showdiv() {
            document.getElementById("hidden_div").style.display = 'none';
            document.getElementById("_strHref").href = 'javascript:show_hiddendiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02.png>";

            SetCookie("extend_div", "hide");
        }
        function editbuyamount(target, maxamount, id) {
            $.ajax({
                cache: false,
                type: "POST",
                url: "/product/ask_do.aspx?action=editamount&target=" + target + "&maxamount=" + maxamount + "&amount=" + $('#buyamount_' + id).val() + "&t=" + Math.random(),
                async: false,
                success: function (msg) {
                    $('#buyamount_' + id).val(msg);
                }
            })
        }
        //Ajax计入进货单
        function Ajax_ListAddToCart(productid) {

            $.ajaxSetup({ async: false });
            $.ajax({
                cache: false,
                type: "POST",
                url: "/cart/cart_do.aspx?action=add&product_id=" + productid + "&buy_amount=" + $("#buyamount_" + productid).val() + "&passto=add",
                async: false,
                success: function (msg) {
                    alert(msg);
                },
                error: function (request) {
                    alert("Connection error");
                }
            })
        }
    </script>

    <style>
        h2.classify {
            font-size: 12px;
            font-weight: normal;
            color: #666;
            line-height: 27px;
            height: 27px;
            text-align: left;
            font-family: "宋体";
            padding: 15px 0;
        }

            h2.classify strong {
                font-size: 18px;
                display: inline-block;
                vertical-align: middle;
                font-family: "Microsoft YaHei";
                line-height: 25px;
                height: 27px;
            }

                h2.classify strong a {
                    color: #666;
                    display: block;
                }

                    h2.classify strong a:hover {
                        color: #ce1329;
                    }

            h2.classify .span_h2 {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                font-family: "宋体";
            }

                h2.classify .span_h2 span {
                    padding: 0 7px 0 10px;
                    border: 1px solid #dddddd;
                    height: 25px;
                    line-height: 25px;
                    font-family: "Microsoft YaHei";
                    display: inline-block;
                    position: relative;
                    z-index: 2;
                }

                    h2.classify .span_h2 span i {
                        width: 13px;
                        height: 13px;
                        display: inline-block;
                        vertical-align: middle;
                        margin-left: 7px;
                        background-image: url(/images/head_icon01.jpg);
                        background-repeat: no-repeat;
                    }

                h2.classify .span_h2:hover {
                    z-index: 99999;
                }

                    h2.classify .span_h2:hover span {
                        border: 1px solid #ce1329;
                        border-bottom: 1px solid #ffffff;
                        background-color: #FFF;
                        z-index: 2;
                    }

                        h2.classify .span_h2:hover span i {
                            width: 13px;
                            height: 13px;
                            display: inline-block;
                            vertical-align: middle;
                            margin-left: 7px;
                            background-image: url(/images/head_icon02.jpg);
                            background-repeat: no-repeat;
                        }

                h2.classify .span_h2 .span_box {
                    border: 1px solid #ce1329;
                    padding: 10px;
                    background-color: #FFF;
                    position: absolute;
                    top: 26px;
                    left: 0;
                    display: block;
                    width: 350px;
                    display: none;
                    z-index: 1;
                }

                    h2.classify .span_h2 .span_box a {
                        font-size: 12px;
                        font-weight: normal;
                        color: #666;
                        display: inline-block;
                        vertical-align: middle;
                        margin: 0 10px;
                        font-family: "Microsoft YaHei";
                    }

                        h2.classify .span_h2 .span_box a:hover {
                            color: #ce1329;
                        }

                h2.classify .span_h2:hover .span_box {
                    display: block;
                }

            h2.classify .span_h3 {
                display: inline-block;
                vertical-align: middle;
                border: 1px solid #ce1329;
                background-color: #FFF;
                height: 25px;
                line-height: 25px;
                overflow: hidden;
                padding: 0 10px 0 0;
            }

                h2.classify .span_h3 i a {
                    background-color: #ce1329;
                    font-size: 14px;
                    font-weight: normal;
                    color: #FFF;
                    line-height: 25px;
                    width: 25px;
                    float: left;
                    display: inline;
                    font-style: normal;
                    text-align: center;
                    margin-right: 10px;
                }

                    h2.classify .span_h3 i a:hover {
                        color: #FFF;
                        text-decoration: underline;
                    }
    </style>

</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="content">
        <div class="position">
            当前位置 >
           <a href="/index.aspx">首页</a> &nbsp;>&nbsp;商品搜索
        </div>
        <div class="partf" style="margin-top: 0px;">
            <div class="pf_left">
                <div class="blk07">
                    <h2>热销排行</h2>
                    <div class="b07_main">
                        <ul>
                            <%=product.Product_LeftSale_Product(5, cate_id)%>
                        </ul>
                    </div>
                </div>
                <div class="blk08">
                    <h2>推荐商品</h2>
                    <div class="b08_main">
                        <ul>
                            <%=cart.GuessLikeProduct("猜你喜欢",5) %>
                        </ul>
                    </div>
                </div>
                <div class="blk09" style="height: auto;">
                    <h2>浏览记录<a href="javascript:void(0);" onclick="clear_product_view_history()"></a></h2>
                    <div class="b09_main" id="div_product_view_history">
                        <ul>
                            <%=product.Member_Index_Right_LastView_Product() %>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="pf_right">

                <div class="blk10">
                    <%--排序  开始--%>
                    <%=product.Product_View_Mode() %>
                    <%--排序  结束--%>
                    <div class="clear"></div>
                </div>
                <div class="blk11">
                    <%product.Product_List("", cate_id, 4, "");%>
                    <div class="clear"></div>
                </div>
                <div class="blk12">
                    <dl>
                        <dt>热搜商品:</dt>
                        <dd><%=ad.AD_Show("Top_Search_Keyword", "", "keyword", 0)%></dd>
                        <div class="clear"></div>
                    </dl>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>


    <!--尾部 结束-->
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
