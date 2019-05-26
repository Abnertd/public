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
    Member member = new Member();
    Supplier supplier = new Supplier();
    int BID = tools.CheckInt(Request["BID"]);
    int list = tools.CheckInt(Request["list"]);
    supplier.Supplier_Login_Check("/supplier/tender_add.aspx?BID=" + BID);
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
    BidInfo entity = MyBid.GetBidByID(BID);

    if (entity != null)
    {
        if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1 || entity.Bid_Type == 1)
        {
            Response.Redirect("/supplier/tender_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/supplier/tender_list.aspx");
    }

    if (!MyBid.IsSignUp(BID, supplierinfo.Supplier_ID))
    {
        Response.Redirect("/supplier/tender_list.aspx");
    }

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="投标添加 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <link type="text/css" href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>

    <script type="text/javascript">

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
    <style type="text/css">
        .blk02 {
            border: none;
        }

        .b14_1_main {
            border: none;
        }

            .b14_1_main table {
                margin-top: 10px;
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



        .blk02 h2 ul li {
            border-left: 1px solid #eeeeee;
            border-bottom: 1px solid #eeeeee;
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
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是买家</a> > 投标管理 > <strong>添加投标</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">

                    <% =member.Member_Left_HTML(2, 2) %>
                </div>
                <div class="pc_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li id="a01" onclick="ShowList(0);">招标信息<img src="/images/icon15.jpg"></li>
                                <li id="a02" onclick="ShowList(1);">我要报价<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>

                        <div class="b14_1_main" id="aa01" style="border: 0px solid #dddddd; display: none;">
                            <table width="975" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 0px solid #eeeeee;">
                                <tr>
                                    <td width="100" class="name">公告标题</td>
                                    <td width="400"><span><a><%=entity.Bid_Title %></a></span></td>
                                    <td width="100" class="name">采购商</td>
                                    <td width="300"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                </tr>
                                <tr>
                                    <%--  <td class="name">报名时间</td>
                                    <td><%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>至<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>--%>
                                    <td class="name">报价时间</td>
                                    <td colspan="3"><%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>至<%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                                </tr>
                                <tr>
                                    <td class="name">交货时间</td>
                                    <td><span><a><%=entity.Bid_DeliveryTime.ToString("yyyy-MM-dd") %></a></span><td class="name">保证金</td>
                                        <td><%=entity.Bid_Bond %>元</td>
                                </tr>
                            </table>
                            <div class="blk11_1" style="margin-top: 20px;">
                                <h2 style="font-size: 15px; font-weight: bold;">公告内容</h2>
                                <div class="b11_main_1">
                                    <%=entity.Bid_Content %>
                                </div>
                            </div>
                            <div class="clear"></div>


                            <div class="blk11_1" style="margin-top: 20px; padding: 0 0 10px 0;">
                                <h2 style="font-size: 15px; font-weight: bold;">商品清单</h3>
                                <table width="975" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #eeeeee;">
                                    <tr>
                                        <%--  <td width="120" class="name">物料编号</td>--%>
                                        <td width="180" class="name">产品名称</td>
                                        <td width="180" class="name">型号规格</td>
                                        <%--  <td width="180" class="name">品牌</td>--%>
                                        <td width="180" class="name">计量单位</td>
                                        <td width="180" class="name">采购数量</td>
                                    </tr>
                                    <%MyBid.BidProductList(entity.BidProducts, entity.Bid_Status, 1); %>
                                </table>
                            </div>


                            <div class="blk11_1" style="margin-top: 20px; padding: 0 0 10px 0;">
                                 <h2>附件列表</h2>
                  <h3 style="padding-left: 10px;">
                    <ul>
                        <li style="width:400px; ">附件名称</li>
                        <li style="width:400px ">说明</li>
                        <li style="width:237px ; border-right: none;">操作</li>
                    </ul>
                    <div class="clear"></div>
                </h3>
                <table width="1050px;" border="0" cellspacing="0" cellpadding="0">
                    <%MyBid.BidAttachmentsView(entity.BidAttachments, 0); %>
                </table>

                            </div>


                        </div>

                        <div class="b14_1_main" id="aa02" style="border: 0px solid #dddddd; display: none;">



                            <table width="973" border="0" cellspacing="0" cellpadding="0">
                                <tr>

                                    <td width="100" class="name">报价单位</td>
                                    <td width="300"><span><a><%=entity.Bid_MemberCompany %></a></span></td>
                                    <td width="100" class="name" style="border-left: 1px solid #eeeeee">已报价轮次</td>
                                    <td width="400"><span><a><%=MyBid.GetTenderCounts(entity.Bid_ID,  supplierinfo.Supplier_ID)%>次</a></span></td>
                                </tr>
                                <%-- <tr>
                                    <td class="name" style="border-left:1px solid #eeeeee">报价金额</td>
                                    <td><span><a><%=pub.FormatCurrency(Tender.Tender_AllPrice) %></a></span></td>
                                    <td class="name">报价时间</td>
                                    <td><%=Tender.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") %></td>
                                </tr>--%>
                            </table>


                            <form name="frm_bid" id="frm_bid" method="post" action="/supplier/tender_do.aspx">
                                <%MyBid.Tender_List(BID, entity.BidProducts, entity.Bid_Number, entity.Bid_SupplierID, entity.Bid_BidEndTime, 0); %>
                                <input name="action" type="hidden" id="action" value="add" />
                                <input name="BID" type="hidden" id="BID" value="<%=BID %>" />
                                <input name="Tender_BidMemberCompany" type="hidden" id="Tender_BidMemberCompany" value="<%=entity.Bid_MemberCompany %>" />

                            </form>

                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="clear"></div>
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

    </script >
    <%--右侧浮动弹框 结束--%>
    <!--主体 结束-- >


            <ucbottom:bottom runat="server" ID="Bottom" />

</body >
</html >
