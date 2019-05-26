<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.MEM" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    CMS cms = new CMS();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    IMemberFavorites MyFavor;
    
    Cart cart = new Cart();
    string SET_Title = "商品分类 - " + pub.SEO_TITLE();
    string Keywords = tools.NullStr(Application["Site_Keyword"]);
    string Description = tools.NullStr(Application["Site_Description"]);
    MyFavor = MemberFavoritesFactory.CreateMemberFavorites();


    int MemberFavNum = cms.MyFavProductsNum(0); 
   
    Session["Position"] = "IndustryInformation";
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>行情资讯 - <%=pub.SEO_TITLE() %></title>
     <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="Keywords" content="<% =Keywords%>" />
    <meta name="Description" content="<%=Description%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    

   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <!--滑动门 开始-->
 <%--   <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script src="/scripts/hdtab.js" type="text/javascript"></script>
    <script type="/text/javascript">
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
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/index.aspx" >首页</a> > 行情资讯</div>
        <!--位置 结束-->
        <div class="banner02">
            <img src="/images/img12.jpg" width="1200" height="156" />
        </div>
        <div class="parte" style="border: none;">
            <div class="pe_left">
                <div class="blk17_1" style="border: none;">
                    <%cms.GetHangYeInfoByCateID();%>
                </div>
            </div>
            <div class="pe_right">
                <div class="blk18_1">
                    <h2>热门咨询推荐</h2>
                    <div class="b18_main_1">
                        <%cms.GetHangYeSort();%>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>

    </div>
    <!--主体 结束-->
    <!--尾部 开始-->
  <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
    <!--右侧滚动 开始-->
    <div class="right_scroll">
        <div class="scr_info01">
            <ul>
                <li>
                    <%int cart_amount = cart.My_Cart_Count(); %>
                    <div class="li_fox"><a href="/cart/my_cart.aspx"><strong><%=cart_amount %></strong>采购清单</a></div>
                </li>
                <li>
                    <div class="li_fox"><a href="/member/member_favorites.aspx" target="_blank"><strong><%=MemberFavNum %></strong>收藏</a></div>
                </li>
                <li>
                    <div class="li_fox"><a href="/member/message_list.aspx?action=list" ><strong> <%=new SysMessage().GetMessageNum(1)%></strong>消息</a></div>
                </li>
                <li>
                    <div class="li_fox">
                        <a href="#head_box">
                            <img src="/images/icon23.jpg" style="width: 22px; margin: 0 auto; margin-top: 18px;"></a>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <!--右侧滚动 结束-->
</body>
</html>

