<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    myApp.Supplier_Login_Check("/supplier/Product_list.aspx");
    string Product_SEO_Title, Product_SEO_Keyword, Product_SEO_Description;
    int Product_ID = 0;

    Product_ID = tools.CheckInt(Request["Product_ID"]);
    Product_SEO_Title = "";
    Product_SEO_Keyword = "";
    Product_SEO_Description = "";

    ProductInfo entity = myApp.GetProductByID(Product_ID);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        Product_ID = entity.Product_ID;
        Product_SEO_Title = entity.Product_SEO_Title;
        Product_SEO_Keyword = entity.Product_SEO_Keyword;
        Product_SEO_Description = entity.Product_SEO_Description;

    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="商品SEO管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

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
    <style type="text/css">
        .input01 {
            background: none;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />



    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>商品SEO管理</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% myApp.Get_Supplier_Left_HTML(3, 3); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>商品SEO管理</h2>
                        <div class="blk17_sz">

                            <form name="formadd" id="formadd" method="post" action="/supplier/product_do.aspx">

                                <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                    <tr>
                                        <td width="92" class="name">SEO标题</td>
                                        <td width="801">
                                            <input name="Product_SEO_Title" type="text" id="Product_SEO_Title" style="width: 300px;" class="input01" maxlength="50" value="<%=Product_SEO_Title %>" />
                                            <br />
                                             <span><font style="color: red;">*</font> 标题是被搜索引擎当作确定当前网页主题的最主要的参数。  </span>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td width="120px" class="name">SEO关键词</td>
                                        <td width="600px">
                                            <textarea name="Product_SEO_Keyword" cols="50" rows="5" style="width: 600px;"><%=Product_SEO_Keyword%></textarea>
                                             <br />
                                            <span><font style="color: red;">*</font>关键词优化是让网站目标关键词在某个搜索引擎上得到更好的排名。让更多的用户都能快速的查找到自己的网站关键词。
                                            </span></td>
                                    </tr>
                                   
                                    <tr style="margin-top:15px;">
                                        <td width="120px" class="name">SEO介绍</td>
                                        <td width="600px;">
                                            <textarea name="Product_SEO_Description" cols="50" rows="5" style="width: 600px;"><%=Product_SEO_Description %></textarea>
                                            <br />
                                            <span><font style="color: red;">*</font>通过总结搜索引擎的排名规律,对网站进行合理优化,使你的网站在 百度 和Google的排名提高,让搜索引擎给你带来客户。
                                            </span></td>


                                    </tr>
                                    
                                     <tr style="margin-top:15px;">
                                        <td width="120" class="name"></td>
                                        <td width="801">
                                            <input name="Product_ID" type="hidden" id="Product_ID" value="<% =Product_ID%>" />
                                            <input name="action" type="hidden" id="action" value="renewseo" />
                                            <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a>
                                        </td>
                                    </tr>
                                </table>
                            </form>
                        </div>
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


    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
