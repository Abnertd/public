<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>


<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    Public_Class pub = new Public_Class();
    Supplier SupplierApp = new Supplier();
    ITools tools = ToolsFactory.CreateTools();

    int shoppingask_id = tools.CheckInt(Request["shoppingask_id"]);
    SupplierApp.Supplier_Login_Check("/supplier/supplier_shoppingask_reply.aspx?shopingask_id=" + shoppingask_id);

    ShoppingAskInfo entity = SupplierApp.GetShoppingAskInfoByID(shoppingask_id);
    if (entity == null)
    {
        Response.Redirect("/supplier/supplier_shop_shoppingask.aspx");
    }
     
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="咨询管理 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
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
    <script type="text/javascript" src="/scripts/common.js"></script>

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
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置 开始-->          
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>咨询管理</strong></div>

            <!--位置 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%-- <div class="blk12">--%>
                    <%SupplierApp.Get_Supplier_Left_HTML(2, 11); %>
                    <%--   </div>--%>
                </div>

                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">

                        <h2>咨询管理</h2>

                        <div class="blk17_sz">
                            <form name="formadd" id="formadd" method="post" action="/supplier/feedback_do.aspx">
                                <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td align="right" class="name">咨询人</td>
                                        <td>
                                            <%
                                                if (entity.Ask_MemberID > 0)
                                                {
                                                    Response.Write(SupplierApp.GetMemberNickName(entity.Ask_MemberID));
                                                }
                                                else
                                                {
                                                    Response.Write("游客");
                                                }                    
                                            %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="name">咨询产品</td>
                                        <td>
                                            <%
                                                Response.Write(SupplierApp.GetProductNameByID(entity.Ask_ProductID));
                                            %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="name">咨询时间</td>
                                        <td>
                                            <%=entity.Ask_Addtime %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="name">咨询内容</td>
                                        <td>
                                            <%=entity.Ask_Content %>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right" class="name">咨询回复</td>
                                        <td>
                                            <textarea name="Ask_Reply" id="Ask_Reply" rows="5" cols="50"><%=entity.Ask_Reply %></textarea></td>
                                    </tr>

                                    <tr>
                                        <td align="right" class="name">&nbsp;
                                        </td>
                                        <td>
                                            <span class="table_v_title">
                                                <input name="action" type="hidden" id="action" value="shoppingask_reply" />
                                                <input name="shoppingask_id" type="hidden" id="shoppingask_id" value="<%=shoppingask_id %>" />
                                                <a href="javascript:void();" onclick="$('#formadd').submit();" class="a11"></a>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </form>
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
