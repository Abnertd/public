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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=SET_Title%></title>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="../scripts/common1.js"></script>
    <script type="text/javascript" src="/scripts/cart.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
    <script src="/js/1.js" type="text/javascript"></script>
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
        <!--位置 开始-->  
          <div class="position"><a href="/">首页</a><%=product.Get_Cate_Nav(cate_id, "&nbsp;>&nbsp;")%></div>
        <!--位置 结束-->
        <!--筛选 开始-->
       <%-- <div class="parte">

            <%product.Product_Filter(cate_id, cate_parentid, cate_typeid);%>
        </div>--%>
     
        <!--筛选 结束-->
        <div class="partf">

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
                <div class="blk09">
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
                    <ul class="list01">
                        <%--排序  开始--%>
                        <%=product.Product_View_Mode() %>
                        <%--排序  结束--%>
                    </ul>
                    <ul class="list02">
                        <li>展示方式 :</li>
                        <li><a href="#" target="_blank" class="a09"></a></li>
                        <li><a href="#" target="_blank" class="a10"></a></li>
                        <li><a href="#" target="_blank" class="a11"></a></li>
                    </ul>
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
    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />




</body>
</html>
