<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/auction_list.aspx");
    Bid MyBid = new Bid();
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="拍卖列表 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
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
        .zkw_19_fox img {
            vertical-align: middle;
            display: inline;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>拍卖列表</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% supplier.Get_Supplier_Left_HTML(5, 3); %>
                </div>
                <div class="pc_right">


                    <div class="blk14_1" style="margin-top: 1px;">
                        <h2>拍卖列表</h2>
                        <%MyBid.Bid_MemberList(1); %>
                    </div>

                </div>
            </div>
        </div>
        <div class="clear"></div>
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
