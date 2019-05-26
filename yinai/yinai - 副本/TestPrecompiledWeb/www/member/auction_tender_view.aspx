<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Supplier MySupplier = new Supplier();
    Bid Mybid = new Bid();
    int TenderID = tools.CheckInt(Request["TenderID"]);
    member.Member_Login_Check("/member/auction_tender_view.aspx?TenderID=" + TenderID);
    TenderInfo Tender = Mybid.GetTenderByID(TenderID);
    BidInfo entity = null;
    int list = tools.CheckInt(Request["list"]);
    int supplier = 0;
    string Supplier_CompanyName = "";



    if (Tender != null)
    {
        if (Tender.Tender_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            Response.Redirect("/member/auction_list.aspx");
        }
        entity = Mybid.GetBidByID(Tender.Tender_BidID);
    }
    else
    {
        Response.Redirect("/member/auction_list.aspx");
    }


    SupplierInfo supplierinfo = MySupplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
    }
    else
    {
        Response.Redirect("/member/auction_list.aspx");
    }
    
 
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="竞价详情 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    

   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <script src="/scripts/hdtab.js"></script>
    <script src="/scripts/1.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script>
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
    <style>
        .blk02 {
            border: none;
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

        .blk02 h2 {
            border: 1px solid #eeeeee;
        }

        .blk14_1 h2 {
            background-color: #f7f7f7;
            color: #333;
            border: 1px solid #eeeeee;
        }

        .b14_1_main {
            border: none;
        }

        .b02_main {
            padding: 0;
        }

        .blk14_1 h2 {
            margin-top: 15px;
        }
          .layui-anim{ top:250px !important}
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />

    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <a href="/member/auction_list.aspx">拍卖竞价</a> ><span>竞价详情</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(4,4) %>
                </div>
                <div class="pd_right">
                    <div class="blk02">
                        <h2>
                            <ul>
                                <li id="a01" onclick="ShowList(0);">拍卖信息<img src="/images/icon15.jpg"></li>
                                <li id="a02" onclick="ShowList(1);">报价信息<img src="/images/icon15.jpg"></li>
                                <%--    <li id="a03" onclick="ShowList(2);">附件列表<img src="/images/icon15.jpg"></li>--%>
                            </ul>
                        </h2>

                        <div class="blk14_1" style="margin-top: 1px;" id="aa01">

                            <h2>竞价详情<%if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                                      {%>
                                <span style="top: 0;"><a class="a13" href="/member/auction_tender_add.aspx?list=1&BID=<%=entity.Bid_ID %>">再次竞价</a></span>
                                <%} %>
                            </h2>

                            <div class="main">
                                <div class="b14_1_main">
                                    <table width="973" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="100" class="name">公告标题</td>
                                            <td width="400"><span><a><%=entity.Bid_Title %></a></span></td>
                                            <td width="100" class="name">竞拍方</td>
                                            <td width="300"><span><a><%=Supplier_CompanyName%></a></span></td>
                                        </tr>
                                        <tr>
                                            <td class="name">竞价总金额</td>
                                            <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                            <td class="name">竞价时间</td>
                                            <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                        </tr>

                                        <tr>
                                            <td width="100" class="name">竞价轮次</td>
                                            <td width="400"><span><a><%=Mybid.GetTenderCounts(entity.Bid_ID,  supplierinfo.Supplier_ID)%>次</a></span></td>
                                            <td width="100" class="name">保证金</td>
                                            <td width="300"><span><a><%=pub.FormatCurrency( entity.Bid_Bond )%></a></span></td>

                                        </tr>


                                        <tr>
                                            <td class="name">是否中标</td>
                                            <td><span><a><%=Mybid.IsWin(Tender.Tender_Status,Tender.Tender_IsWin) %></a></span></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>

                                    <div style="margin-top: 0px;">
                                        <h2 style="border: 1px solid #eeeeee">竞价清单</h2>
                                        <table width="973px" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #eeeeee;">
                                            <tr>
                                                <%-- <td width="120" class="name">产品编号</td>--%>
                                                <td width="173" class="name">产品名称</td>
                                                <td width="160" class="name">型号规格</td>
                                                <%-- <td width="80" class="name">品牌</td>--%>
                                                <td width="160" class="name">计量单位</td>
                                                <td width="120" class="name">拍卖数量</td>
                                                <td width="120" class="name">起标价格</td>
                                                <td width="120" class="name">单价竞价1</td>
                                                <%-- <td width="120" class="name">竞价排名</td>--%>
                                                <td width="120" class="name">操作</td>
                                            </tr>
                                            <%Mybid.AuctionTenderProductList(entity.BidProducts, Tender.TenderProducts); %>
                                        </table>
                                    </div>









                                    <div style="border: 1px solid #ddd; margin-top: 15px;">

                                        <h2 style="border-top: 1px solid #dddddd; border-top: none; margin-top: -15px;">
                                            <ul style="width: 973px;">
                                                <h2 style="border: 1px solid #eeeeee">附件列表项</h2>




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
                                                        <%Mybid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 0); %>
                                                    </table>

                                                </div>

                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%if (Mybid.BidOrderStatus(entity.Bid_ID, ref supplier))
                              {%>
                            <div class="b02_main">
                                <ul style="width: 850px;">


                                    <li style="margin-left: 275px;"><a href="/cart/Bid_confirm.aspx?BID=<%=entity.Bid_ID%>" class="a05" style="margin-left: 0px; float: left;">生成订单</a></li>

                                </ul>
                            </div>
                            <%} %>
                            <div class="clear"></div>


                        </div>










                        <div class="blk14_1" style="margin-top: 1px;" id="aa02">

                            <h2>竞价详情<%if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                                      {%>
                                <span style="top: 0px;"><a class="a13" href="/member/auction_tender_add.aspx?list=1&BID=<%=entity.Bid_ID %>">再次竞价</a></span>
                                <%} %>
                            </h2>

                            <div class="main">
                                <div class="b14_1_main">
                                    <table width="973" border="0" cellspacing="0" cellpadding="0">
                                        <tr>

                                            <td width="100" class="name">报价单位</td>
                                            <td width="400"><span><a><%=Supplier_CompanyName %></a></span></td>
                                            <td width="100" class="name">报价轮次</td>
                                            <td width="300"><span><a><%=Mybid.GetTenderCounts(entity.Bid_ID,  supplierinfo.Supplier_ID)%>次</a></span></td>

                                        </tr>
                                    </table>

                                    <div style="margin-top: 0px;">
                                        <h2 style="border: 1px solid #eeeeee">竞价清单</h2>
                                        <table width="973px" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #eeeeee;">
                                            <tr>
                                                <%-- <td width="120" class="name">产品编号</td>--%>
                                                <td width="173" class="name">产品名称</td>
                                                <td width="160" class="name">型号规格</td>
                                                <%-- <td width="80" class="name">品牌</td>--%>
                                                <td width="160" class="name">计量单位</td>
                                                <td width="120" class="name">拍卖数量</td>
                                                <td width="120" class="name">起标价格</td>
                                                <td width="120" class="name">单价竞价2</td>
                                                <%-- <td width="120" class="name">竞价排名</td>--%>
                                                <td width="120" class="name">操作</td>
                                            </tr>
                                            <%Mybid.AuctionTenderProductList(entity.BidProducts, Tender.TenderProducts); %>
                                        </table>
                                    </div>









                                    <%-- <div style="border: 1px solid #ddd; margin-top: 15px;">

                                        <h2 style="border-top: 1px solid #dddddd; border-top: none; margin-top: -15px;">
                                            <ul style="width: 973px;">
                                                <h2 style="border: 1px solid #eeeeee">附件列表</h2>
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
                                                        <%Mybid.BidAttachmentsList(entity.BidAttachments, entity.Bid_Status, 0); %>
                                                    </table>

                                                </div>

                                            </div>
                                            <div class="clear"></div>
                                        </div>

                                    </div>--%>
                                </div>
                            </div>
                            <%if (Mybid.BidOrderStatus(entity.Bid_ID, ref supplier))
                              {%>
                            <div class="b02_main">
                                <ul style="width: 850px;">


                                    <li style="margin-left: 275px;"><a href="/cart/Bid_confirm.aspx?BID=<%=entity.Bid_ID%>" class="a05" style="margin-left: 0px; float: left;">生成订单</a></li>

                                </ul>
                            </div>
                            <%} %>

                              <%Mybid.MyBidRecord(entity.Bid_ID, entity.BidProducts, entity.Bid_Number, entity.Bid_SupplierID, entity.Bid_BidEndTime, 1); %>
                            <div class="clear"></div>


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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
