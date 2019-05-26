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
    int TenderID = tools.CheckInt(Request["TenderID"]);
    supplier.Supplier_Login_Check("/supplier/tender_view.aspx?TenderID=" + TenderID);
    Bid MyBid = new Bid();
    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();

    int list = tools.CheckInt(Request["list"]);


    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
        {
            pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
        }
    }
    TenderInfo Tender = MyBid.GetTenderByID(TenderID);
    BidInfo entity = null;
    if (Tender != null)
    {
        if (Tender.Tender_SupplierID != supplierinfo.Supplier_ID)
        {
            Response.Redirect("/supplier/tender_list.aspx");
        }
        entity = MyBid.GetBidByID(Tender.Tender_BidID);

        if (entity != null)
        {
            if (entity.Bid_IsAudit != 1 || entity.Bid_Type == 1)
            {
                Response.Redirect("/supplier/tender_list.aspx");
            }
        }
        else
        {
            Response.Redirect("/supplier/tender_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/supplier/tender_list.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="报价详情 - 我是卖家 - " + pub.SEO_TITLE()%></title>
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


        function ShowList(obj) {
            if (obj == 1) {
                $("#aa01").hide();

                $("#aa02").show();
                $("#a02").removeClass().addClass("on");
                $("#a01").removeClass().addClass("");


            }
            else {

                $("#aa02").hide();
                $("#aa01").show();
                $("#a02").removeClass().addClass("");

                $("#a01").removeClass().addClass("on");
            }
        }

        window.onload = function () {
            ShowList(<%=list%>);
        };
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
    <style type="text/css">
        #mask_div {
            display: none;
            position: absolute;
            z-index: 1001;
            height: 100%;
            width: 100%;
            left: 0px;
            overflow-y: hidden;
            background: #000000;
            filter: Alpha(opacity=30);
            opacity: 0.3;
        }

        .blk02 {
            border: none;
        }

        .b14_1_main {
            border: none;
            margin-top: 0px;
        }

        .blk14_1 h2 {
            background-color: #f7f7f7;
            border: 1px solid #eeeeee;
            color: #333;
        }

        #html-div {
            z-index: 9999;
            width: 602px;
            background-color: #ffffff;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: 0px auto;
            padding: 0px;
        }

        .blk14_1 h2 {
            background-color: #f7f7f7;
            color: #333;
        }

        .blk02 h2 {
            border: 1px solid #eeeeee;
        }

        .b02_main {
            padding: 0;
        }


        .blk14_1 h2 span {
            float: right;
            display: inline;
            margin-top: 7px;
        }

            .blk14_1 h2 span a.a13 {
                background-image: url(../images/a_bg01.jpg);
                background-repeat: no-repeat;
                width: 74px;
                height: 26px;
                font-size: 12px;
                font-weight: normal;
                text-align: center;
                line-height: 26px;
                color: #333;
                display: inline-block;
                vertical-align: middle;
                margin-right: 7px;
            }

        /*.b14_1_main table {
            margin-top: 10px;
        }*/

            .b14_1_main table td.name {
                border-left: none;
            }

        .blk02 h2 ul li {
            border-left: 1px solid #eeeeee;
            border-bottom: 1px solid #eeeeee;
        }

        /*.b14_1_main table td {
            border-left: 1px solid #eeeeee;
        }*/
        .b14_1_main table tr td .td_bidround {
            border-right: none;
        }

        .b14_1_main table tr td .td_right {
            text-align: right;
        }

        .b14_1_main table tr td .td_left {
            text-align: left;
        }
    </style>
    <script>
        function AddProduct(obj) {

            $("#html-div").show();
            $("#mask_div").css('height', $("body").height() + 20);
            $("#html-div").html("<div style=\"text-align:center;padding-top:150px; \">加载中..</div>");
            $.ajax({
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                url: encodeURI("/supplier/tender_product.aspx?productid=" + obj + "&timer=" + Math.random()),
                success: function (data) {
                    $("#html-div").html(data);
                },
                error: function () {
                    alert("Error Script");
                }
            });
            if ($("#html-div").html() != "") {
                var _scrollHeight = $(document).scrollTop(), //获取当前窗口距离页面顶部高度 
    _windowHeight = $(window).height(), //获取当前窗口高度 
    _windowWidth = $(window).width(), //获取当前窗口宽度 
    _popupHeight = $("#html-div").height(), //获取弹出层高度 
    _popupWeight = $("#html-div").width(); //获取弹出层宽度 
                _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight;
                _posiLeft = (_windowWidth - _popupWeight) / 2;
                $("#html-div").css({ "left": _posiLeft + "px", "top": "200px", "display": "block" }); //设置position 
                $("#mask_div").show();
            }
        }
        function dialog_loginhide() {
            $("#html-div").css('left', '50%');
            $("#html-div").css('top', '50%');
            $("#html-div").hide();
            $("#mask_div").hide();
        }

        function Product(obj, id, name) {
            $("#tender_product_id" + obj).val(id);
            $("#tender_product_" + obj).html(name);
        }
    </script>
</head>
<body>
    <div id="dialog-message" title="" style="display: none;">
    </div>
    <div id="html-div" title="" style="display: none;">
    </div>
    <div id="mask_div">
    </div>
    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 投标管理 > <strong>报价详情</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% supplier.Get_Supplier_Left_HTML(6, 1); %>
                </div>
                <div class="pd_right">
                    <div class="blk02">
                        <%-- 拍卖信息 开始 --%>
                        <h2>
                            <ul>
                                <li id="a01" onclick="ShowList(0);">招标信息<img src="/images/icon15.jpg"></li>
                                <li id="a02" onclick="ShowList(1);">报价信息<img src="/images/icon15.jpg"></li>
                                <%--    <li id="a03" onclick="ShowList(2);">附件列表<img src="/images/icon15.jpg"></li>--%>
                            </ul>
                        </h2>
                        <div class="blk14_1" style="margin-top: 15px;" id="aa01">
                            <h2>招标信息
                        <%if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                          {%>
                                <span style="top: 0px;"><a class="a13" href="/supplier/tender_add.aspx?list=1&BID=<%=entity.Bid_ID %>">再次报价</a></span>
                                <%} %>
                            </h2>
                            <div class="main">
                                <div class="b14_1_main">
                                    <table width="975" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="100" class="name" style="border-left: 1px solid #eeeeee">公告标题</td>
                                            <td width="400"><span><a><%=entity.Bid_Title %></a></span></td>
                                            <td width="100" class="name">采购商</td>
                                            <td width="300"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                        </tr>
                                        <tr>
                                            <td class="name" style="border-left: 1px solid #eeeeee">报价金额</td>
                                            <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                            <td class="name">报价时间</td>
                                            <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                        </tr>
                                        <tr>
                                            <td width="100" class="name">竞价轮次</td>
                                            <td width="400"><span><a><%=MyBid.GetTenderCounts(entity.Bid_ID,  supplierinfo.Supplier_ID)%>次</a></span></td>
                                            <td width="100" class="name">保证金</td>
                                            <td width="300"><span><a><%=pub.FormatCurrency( entity.Bid_Bond )%></a></span></td>

                                        </tr>
                                        <tr>
                                            <td class="name" style="border-left: 1px solid #eeeeee">报价金额</td>
                                            <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                            <td class="name">报价时间</td>
                                            <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                        </tr>
                                    </table>

                                    <div style="margin-top: 20px;">
                                        <h2>商品清单</h2>
                                        <%if (Tender.Tender_IsWin == 0 || (Tender.Tender_IsWin == 1 && Tender.Tender_IsProduct == 1))
                                          {%>
                                        <table width="975" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="name">产品名称</td>
                                                <td class="name">型号规格</td>
                                                <td class="name">计量单位</td>
                                                <td class="name">采购数量</td>
                                                <td class="name">单价报价</td>
                                                <%if (Tender.Tender_IsWin == 1)
                                                  {%>
                                                <td class="name">报价商品</td>
                                                <%--  <td width="100" class="name">排名</td>--%>
                                                <%} %>
                                            </tr>
                                            <%MyBid.TenderProductList(entity.BidProducts, Tender.TenderProducts, Tender.Tender_IsWin); %>
                                        </table>
                                        <%}
                                          else
                                          { %>
                                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/tender_do.aspx">
                                            <table width="975" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="name" style="border-left: 1px solid #eeeeee">产品名称</td>
                                                    <td class="name">型号规格</td>
                                                    <td class="name">计量单位</td>
                                                    <td class="name">采购数量</td>
                                                    <td class="name">单价报价</td>
                                                    <td class="name">报价商品</td>
                                                    <td class="name">操作</td>
                                                </tr>
                                                <%MyBid.TenderProductAdd(entity.BidProducts, Tender.TenderProducts); %>
                                            </table>

                                            <input id="action" name="action" type="hidden" value="AddProduct" />
                                            <input id="TenderID" name="TenderID" type="hidden" value="<%=Tender.Tender_ID %>" />
                                            <div class="b02_main">
                                                <ul style="width: 850px;">
                                                    <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();" class="a05">提交报价商品</a></li>
                                                </ul>
                                            </div>
                                        </form>
                                        <%} %>
                                    </div>





                                    <div style="border: 1px solid #ddd; margin-top: 80px;">
                                        <h2 style="border-top: 1px solid #dddddd; border-top: none;">
                                            <ul>
                                                <%--<li id="Li2" onclick="ShowList(2);" class="on">附件列表</li>--%>
                                                <h2 style="border: none;">附件列表</h2>
                                                <%if (entity.Bid_Status == 0)
                                                  {%>
                                                <li style="float: right;"><a href="/member/bid_Attachments_add.aspx?BID=<%=entity.Bid_ID %>" class="more4" style="color: #ffffff">添加附件</a></li>
                                                <%} %>
                                            </ul>
                                        </h2>
                                        <div class="b02_main" id="aa03">
                                            <div class="blk14_1" style="margin-top: 0px;">
                                                <div class="b14_1_main" style="border-left: none; border-right: none;">
                                                    <table width="974" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>

                                                            <td width="356" class="name">附件名称</td>
                                                            <td class="name">说明</td>
                                                            <td width="127" class="name">操作</td>
                                                        </tr>
                                                        <%MyBid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 0); %>
                                                    </table>

                                                </div>

                                            </div>
                                            <div class="clear"></div>
                                        </div>

                                    </div>






                                    <div class="clear"></div>
                                </div>



                            </div>
                        </div>

                        <%--  拍卖信息 结束--%>




                        <%-- 拍卖信息 开始 --%>

                        <div class="blk14_1" style="margin-top: 15px;" id="aa02">
                            <h2>报价信息
                        <%if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                          {%>
                                <span style="top: 0px;"><a class="a13" href="/supplier/tender_add.aspx?list=1&BID=<%=entity.Bid_ID %>">再次报价</a></span>
                                <%} %>
                            </h2>
                            <div class="main">
                                <div class="b14_1_main">
                                    <table width="975" border="0" cellspacing="0" cellpadding="0">
                                        <tr>

                                            <td width="100" class="name">报价单位</td>
                                            <td width="300"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                            <td width="100" class="name" style="border-left: 1px solid #eeeeee">已竞价轮次</td>
                                            <td width="400"><span><a><%=MyBid.GetTenderCounts(entity.Bid_ID,  supplierinfo.Supplier_ID)%>次</a></span></td>
                                        </tr>
                                        <%-- <tr>
                                    <td class="name" style="border-left:1px solid #eeeeee">报价金额</td>
                                    <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                    <td class="name">报价时间</td>
                                    <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                </tr>--%>
                                    </table>

                                    <div style="margin-top: 20px;">                                       
                                        <h2>报价清单</h2>
                                        <%if (Tender.Tender_IsWin == 0 || (Tender.Tender_IsWin == 1 && Tender.Tender_IsProduct == 1))
                                          {%>
                                        <table width="975" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="name">产品名称</td>
                                                <td class="name">型号规格</td>
                                                <td class="name">计量单位</td>
                                                <td class="name">采购数量</td>
                                                <td class="name">单价报价</td>
                                                <%if (Tender.Tender_IsWin == 1)
                                                  {%>
                                                <td width="200" class="name">报价商品</td>
                                                <%} %>
                                            </tr>
                                            <%MyBid.TenderProductList(entity.BidProducts, Tender.TenderProducts, Tender.Tender_IsWin); %>
                                        </table>
                                        <%}
                                          else
                                          { %>
                                        <form name="frm_bid" id="Form1" method="post" action="/supplier/tender_do.aspx">
                                            <table width="975" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="120" class="name" style="border-left: 1px solid #eeeeee">产品名称</td>
                                                    <td width="200" class="name">型号规格</td>
                                                    <td width="80" class="name">计量单位</td>
                                                    <td width="80" class="name">采购数量</td>
                                                    <td width="80" class="name">单价报价</td>
                                                    <td width="150" class="name">报价商品</td>
                                                    <td width="50" class="name">操作</td>
                                                </tr>
                                                <%MyBid.TenderProductAdd(entity.BidProducts, Tender.TenderProducts); %>
                                            </table>

                                            <input id="Hidden1" name="action" type="hidden" value="AddProduct" />
                                            <input id="Hidden2" name="TenderID" type="hidden" value="<%=Tender.Tender_ID %>" />
                                            <div class="b02_main">
                                                <ul style="width: 850px;">
                                                    <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();" class="a05">提交报价商品</a></li>
                                                </ul>
                                            </div>
                                        </form>
                                        <%} %>
                                    </div>
                                    <div style="border: 1px solid #ddd; margin-top: 80px;">
                                        <h2 style="border-top: 1px solid #dddddd; border-top: none;">
                                            <ul>
                                                <%--<li id="Li2" onclick="ShowList(2);" class="on">附件列表</li>--%>
                                                <h2 style="border: none;">附件列表</h2>
                                                <%if (entity.Bid_Status == 0)
                                                  {%>
                                                <li style="float: right;"><a href="/member/bid_Attachments_add.aspx?BID=<%=entity.Bid_ID %>" class="more4" style="color: #ffffff">添加附件</a></li>
                                                <%} %>
                                            </ul>
                                        </h2>
                                        <div class="b02_main" id="Div3">
                                            <div class="blk14_1" style="margin-top: 0px;">
                                                <div class="b14_1_main" style="border-left: none; border-right: none;">
                                                    <table width="974" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>

                                                            <td width="356" class="name">附件名称</td>
                                                            <td class="name">说明</td>
                                                            <td width="127" class="name">操作</td>
                                                        </tr>
                                                        <%MyBid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 0); %>
                                                    </table>

                                                </div>

                                            </div>
                                            <div class="clear"></div>
                                        </div>

                                    </div>

                                    <%MyBid.MyBidRecord(entity.Bid_ID, entity.BidProducts, entity.Bid_Number, entity.Bid_SupplierID, entity.Bid_BidEndTime, 0); %>

                                    <div class="clear"></div>
                                </div>
                            </div>
                        </div>

                        <%--  拍卖信息 结束--%>
                    </div>


                </div>

            </div>
        </div>
    </div>
    <div class="clear"></div>
    <%--   </div>--%>
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
