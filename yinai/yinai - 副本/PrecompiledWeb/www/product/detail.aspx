<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    //静态化配置
    PageURL pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    Product product = new Product();
    Supplier supplier = new Supplier();
    CMS cmsclass = new CMS();
    ITools tools = ToolsFactory.CreateTools();
    Glaer.Trade.B2C.BLL.MEM.ISupplierShopEvaluate MyShopEvaluate = Glaer.Trade.B2C.BLL.MEM.SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
    Public_Class pub = new Public_Class();
    Cart cart = new Cart();
    Member mem = new Member();


    Glaer.Trade.B2C.BLL.Product.IProductReview productReview = Glaer.Trade.B2C.BLL.Product.ProductReviewFactory.CreateProductReview();

    AD ad = new AD();
    int cate_id = 0, typeid = 0;
    int product_id = 0;
    string product_name = "";
    string ProductName = "";
    int parent_id = 0;
    string productUrl = "";
    string cateUrl = "";
    string shopUrl = "";
    string product_price = "";
    string ProductNote = "";
    int Product_StockAmount = 0;
    string IsHaveStock = "";
    product_id = tools.CheckInt(Request["product_id"]);
    if (product_id == 0)
    {
        Response.Redirect("/index.aspx");
    }

    string productviewcookie = tools.CheckStr(Request["productviewcookie"]);
    if (productviewcookie == "clear")
    {
        Request.Cookies["product_viewhistory_zh-cn"].Value = null;
    }




    ProductInfo productinfo = product.GetProductByID(product_id);
    int GProductReviewCount = productReview.GetProductReviewValidCount(product_id);
    if (productinfo != null)
    {
        if (productinfo.Product_IsAudit == 1 && productinfo.Product_IsInsale == 1)
        {
            if (productinfo.Product_SEO_Title == "")
            {
                product_name = productinfo.Product_Name + " - " + pub.SEO_TITLE();

            }
            else
            {
                product_name = productinfo.Product_SEO_Title;

            }

            if (productinfo.Product_PriceType == 1)
            {
                product_price = pub.FormatCurrency(productinfo.Product_Price);
            }
            else
            {
                product_price = pub.FormatCurrency(pub.GetProductPrice(productinfo.Product_ManualFee, productinfo.Product_Weight));
            }

            productUrl = tools.NullStr(Application["Site_URL"]) + pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString());
            ProductName = productinfo.Product_Name;
            ProductNote = productinfo.Product_Note;
            Product_StockAmount = productinfo.Product_StockAmount;
            if (Product_StockAmount > 0)
            {
                IsHaveStock = "有货";
            }
            else
            {
                IsHaveStock = "无货";
            }

            cate_id = productinfo.Product_CateID;
            CategoryInfo category = product.GetCategoryByID(cate_id);
            if (category != null)
            {
                parent_id = category.Cate_ParentID;
                cateUrl = "<a href=\"/product/category.aspx?cate_id=" + category.Cate_ID + "\">" + category.Cate_Name + "</a>";
            }

            SupplierShopInfo shopInfo = supplier.GetSupplierShopBySupplierID(productinfo.Product_SupplierID);
            if (shopInfo != null)
            {
                shopUrl = "http://" + shopInfo.Shop_Domain + Application["Shop_Second_Domain"] + "/categroy.aspx";
            }

            //更新商品点击数
            product.UpdateProductHits(product_id);
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
    string deliverydeclare, saleservice;
    deliverydeclare = "";
    saleservice = "";
    AboutInfo aboutinfo = cmsclass.GetAboutBySign("deliverydeclare");
    if (aboutinfo != null)
    {
        if (aboutinfo.About_IsActive == 1)
        {
            deliverydeclare = aboutinfo.About_Content;
        }
    }
    aboutinfo = cmsclass.GetAboutBySign("saleservice");
    if (aboutinfo != null)
    {
        if (aboutinfo.About_IsActive == 1)
        {
            saleservice = aboutinfo.About_Content;
        }
    }
    int parentid = product.getCateParentID(cate_id);
    product.Recent_View_Add(product_id);
    Session["Web_Cursor"] = "Category";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=product_name %></title>
     
    <meta name="Keywords" content="<%=productinfo.Product_SEO_Keyword%>" />
    <meta name="Description" content="<%=productinfo.Product_SEO_Description%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
       <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="../scripts/common1.js"></script>
    <script type="text/javascript" src="/scripts/cart.js"></script>
    <script type="text/javascript" src="/scripts/MSClass.js"></script>
    <script type="text/javascript" src="/scripts/jqzoom.pack.1.0.1.js"></script>
    <script type="text/javascript" src="/scripts/jquery.highlight.js"></script>
    <script type="text/javascript" src="/scripts/hdtab.js"></script>

    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["c01","c02"],["cc01","cc02"],"on"," ");
            SDmodel.sd(["d01", "d02", "d03", "d04"], ["dd01", "dd02", "dd03", "dd04"], "on", " ");
            SDmodel.sd(["e01", "e02", "e03", "e04"], ["ee01", "ee02", "ee03", "ee04"], "on", " ");
        }

        var setAmount = {
            min: 1,
            max: <%=productinfo.Product_StockAmount%>,
            reg: function (x) {
                return new RegExp("^[1-9]\\d*$").test(x);
            },
            amount: function (obj, mode) {
                var x = $(obj).val();
                if (this.reg(x)) {
                    if (mode) {
                        x++;
                    } else {
                        x--;
                    }
                } else {
                    alert("请输入正确的数量！");
                    $(obj).val(this.min);
                    $(obj).focus();
                }
                return x;
            },
            reduce: function (obj) {
                var x = this.amount(obj, false);
                if (x >= this.min) {
                    $(obj).val(x);
                } else {
                    alert("商品数量最少为" + this.min);
                    $(obj).val(1);
                    $(obj).focus();
                }
            },
            add: function (obj) {
                var x = this.amount(obj, true);
                if (x <= this.max) {
                    $(obj).val(x);
                } else {
                    alert("商品数量最多为" + this.max);
                    $(obj).val(this.max);
                    $(obj).focus();
                }
            },
            modify: function (obj) {
                var x = $(obj).val();
                if (x==""||x==null) {
                    $(obj).val();
                }
                else if (x < this.min || x > this.max || !this.reg(x)) {
                    alert("请输入正确的数量！");
                    $(obj).val(this.min);
                    $(obj).focus();
                }
            }
        }

        function CountCost() {
            var costfee = parseFloat($("#P_YourPrice").val()) * parseFloat($("#buy_amount").val());
            costfee = Math.round(costfee * 100) / 100;

            $("#P_TotalPrice").text(costfee);
        }
    </script>

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
            var bigPhoto = $("#product_img");
            //alert(bigPhoto.val.length);
            $(".jqzoom").jqzoom({
                zoomWidth: bigPhoto.width(), zoomHeight: bigPhoto.height(), offset: 30, title: false, position: 'right'
            });

            var scrollable = $("#scrollable li");           

            scrollable.each(function (index, entity) {
                $(entity).mousemove(function (e) {
                    scrollable.removeClass("on");
                    $(this).addClass("on");
                    bigPhoto.attr("src", $(this).find("img").attr("src"));
                    $("#displayphoto").attr("href", $(this).find("img").attr("src"));
                });
            });

            $(scrollable[0]).addClass("on");
            bigPhoto.attr("src", $(scrollable[0]).find("img").attr("src"));
            $("#displayphoto").attr("href", $(scrollable[0]).find("img").attr("src"));
        });
    </script>
    <style type="text/css">
        .opt_on {
            border: 2px solid #FF7300;
        }

        .opt_off {
            border: 2px solid #DDD;
        }

        .content .pargt .pg_left dl dt a .preload {
            overflow: hidden;
        }
    </style>
    </head>
<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="content">
        <!--位置 开始-->
        <div class="position">
            当前位置 >
           <a href="/index.aspx">首页</a> > <%=productinfo.Product_Name %>
        </div>
        <!--位置 结束-->
        <!--商品详情开始-->
        <div class="partg">
            <%product.Product_Detail(productinfo, false); %>
        </div>
        <!--商品详情结束-->

        <%-- 热销产品 推荐产品  浏览记录  开始--%>
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
                            <%=cart.GuessLikeProduct("猜你喜欢",10) %>
                        </ul>
                    </div>
                </div>
                <div class="blk09" style="height: auto">
                    <h2>浏览记录<a href="javascript:void(0);" onclick="clear_product_view_history()"></a></h2>
                    <div class="b09_main" id="div_product_view_history">
                        <ul>
                            <%=product.Member_Index_Right_LastView_Product() %>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="pf_right">
                <div class="blk17">
                    <h2>
                        <ul>
                            <li id="c01" class="on">商品详情</li>
                            <li id="c02">物流售后</li>
                            <li><a href="#blk02">评论</a></li>
                            <li><a href="#blk03">咨询</a></li>
                        </ul>
                        <div class="clear"></div>
                    </h2>
                    <div class="b17_main" id="cc01">
                        <%--商品详情--%>
                        <%product.Product_Detail_Extend(productinfo); %>
                        <%=productinfo.Product_Intro %>
                    </div>
                    <div class="b17_main" id="cc02" style="display: none;">
                        <%=productinfo.U_Product_Parameters %>
                    </div>
                </div>
                <div class="blk18" id="blk02">
                    <h2>
                        <ul>
                            <li id="d01" class="on"><span>全部评价（<%=productinfo.Product_Review_ValidCount%>）</span></li>
                            <li id="d02"><span>好评（<font id="pr_11"><%=product.Get_Product_Review_Amount(productinfo.Product_ID,"4,5")%></font>）</span></li>
                            <li id="d03"><span>中评（<font id="pr_21"><%=product.Get_Product_Review_Amount(productinfo.Product_ID,"3")%></font>）</span></li>
                            <li id="d04"><span>差评（<font id="pr_31"><%=product.Get_Product_Review_Amount(productinfo.Product_ID,"1,2")%></font>）</span></li>
                            <li class="bo"><a href="/product/reviews.aspx?product_id=<%=productinfo.Product_ID %>" target="_blank">查看全部评论 ></a></li>
                        </ul>
                        <div class="clear"></div>
                    </h2>
                    <div class="b18_main">
                        <%product.Product_Reviews_Info(productinfo.Product_ID, productinfo.Product_Review_ValidCount, productinfo.Product_Review_Average);%>
                        <div class="clear"></div>
                    </div>

                    <div class="b18_main02" id="dd01">

                        <%=product.Product_Review_List_Top(productinfo.Product_ID, "1,2,3,4,5")%>
                    </div>
                    <div class="b18_main02" id="dd02" style="display: none;">

                        <%=product.Product_Review_List_Top(productinfo.Product_ID, "4,5")%>
                    </div>
                    <div class="b18_main02" id="dd03" style="display: none;">

                        <%=product.Product_Review_List_Top(productinfo.Product_ID, "3")%>
                    </div>
                    <div class="b18_main02" id="dd04" style="display: none;">

                        <%=product.Product_Review_List_Top(productinfo.Product_ID, "1,2")%>
                    </div>
                </div>
                <div class="blk18" id="blk03" style="margin-top: 20px;">
                    <h2>
                        <ul>
                            <li class="on">购买咨询</li>
                            <li class="bo"><a href="/product/ask_add.aspx?product_id=<%=productinfo.Product_ID %>" target="_blank">全部咨询></a></li>
                        </ul>
                        <div class="clear"></div>
                    </h2>
                    <div class="b18_main03">
                        <p>购买之前如有问题<a href="/product/ask_add.aspx?product_id=<%=productinfo.Product_ID %>">我要咨询</a></p>
                        <form action="ask_add.aspx?product_id=<%=productinfo.Product_ID %>" method="post">
                            <b style="border: 0px;">咨询检索：<input name="keyword" type="text" style="width: 265px; font-size: 12px; font-weight: normal; color: #666;" /><input name="" type="submit" value="搜索" style="font-family: 微软雅黑" /></b>
                            <div class="clear"></div>
                        </form>
                    </div>
                    <div class="b18_main04">

                        <% product.Product_Ask_List(productinfo.Product_ID, 5, 1, 0); %>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <%-- 热销产品 推荐产品  浏览记录  结束--%>
    </div>
    <!--主体 结束-->


  <ucbottom:bottom runat="server" ID="Bottom" />

    <div class="right_scroll">
        <ul>
            <li style="z-index: 1;">
                <div class="img_box02" onclick="NTKF.im_openInPageChat('sz_<%=productinfo.Product_SupplierID+1000 %>_9999','<%=productinfo.Product_ID %>');">
                    <a href="javascript:"></a>
                </div>

            </li>


            <li style="z-index: 2;">
                <div class="img_box"><a href="javascript:void(0);" onclick="product_favorites(<%=productinfo.Product_ID %>);"></a></div>
            </li>

            <li>
                <div class="img_box03"><a href="#h_top"></a></div>
            </li>
        </ul>
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
</body>
</html>
