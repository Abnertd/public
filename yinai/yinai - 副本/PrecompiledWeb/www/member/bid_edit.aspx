<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Session["Position"] = "";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Bid Mybid = new Bid();
    int BID = tools.CheckInt(Request["BID"]);
    member.Member_Login_Check("/member/bid_edit.aspx?BID=" + BID);
    BidInfo entity = Mybid.GetBidByID(BID);
    if (entity != null)
    {
        if (entity.Bid_MemberID != tools.NullInt(Session["member_id"]) || entity.Bid_MemberID == 0)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
        if (entity.Bid_Status > 0)
        {
            Response.Redirect("/member/bid_list.aspx");
        }

        if (entity.Bid_Type == 1)
        {
            Response.Redirect("/member/bid_list.aspx");
        }
    }
    else
    {
        Response.Redirect("/member/bid_list.aspx");
    }
    DateTime Today = DateTime.Now;
    
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="修改招标 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/hdtab.js"></script>
    <script src="/scripts/1.js"></script>

    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
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

    <style type="text/css">
           .layui-anim {
            top: 250px !important;
        }
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 >  <a href="/member/index.aspx">我是买家</a> > <span>修改招标</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(2, 2) %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">修改招标<img src="/images/icon15.jpg"></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/member/bid_do.aspx">
                            <div class="b02_main">
                                <ul style="width: 850px;">
                                    <li><span><i>*</i>公告标题：</span><label><input name="Bid_Title" id="Bid_Title" type="text" style="width: 298px;" value="<%=entity.Bid_Title %>" /></label></li>
                                    <div class="clear"></div>
                                    <li><span><i>*</i>采购商：</span><label><input name="Bid_MemberCompany" id="Bid_MemberCompany" type="text" style="width: 298px;" value="<%=entity.Bid_MemberCompany %>" /></label></li>
                                    <div class="clear"></div>

                                    <%--  <li><span><i>*</i>报名时间：</span><label><input name="Bid_EnterStartTime" id="Bid_EnterStartTime" type="text" value="<%=entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" />-<input name="Bid_EnterEndTime" id="Bid_EnterEndTime" type="text"value="<%=entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onClick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" /></label></li>
                                <div class="clear"></div>--%>
                                    <li><span><i>*</i>报价时间：</span><label><input name="Bid_BidStartTime" id="Bid_BidStartTime" type="text" value="<%=entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" />-<input name="Bid_BidEndTime" id="Bid_BidEndTime" type="text" value="<%=entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onclick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });" /></label></li>
                                    <div class="clear"></div>
                                    <li><span><i>*</i>交货时间：</span><label><input name="Bid_DeliveryTime" id="Bid_DeliveryTime" type="text" value="<%=entity.Bid_DeliveryTime.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 138px;" /></label></li>
                                    <script>$(function () { $("#Bid_DeliveryTime").datepicker({ inline: true }); })</script>
                                    <div class="clear"></div>
                                    <li><span><i>*</i>报价轮次：</span><label><input name="Bid_Number" id="Bid_Number" type="text" value="<%=entity.Bid_Number %>" style="width: 138px;" />&nbsp;次</label></li>
                                    <div class="clear"></div>
                                    <li><span><i>*</i>保证金：</span><label><input name="Bid_Bond" id="Bid_Bond" type="text" value="<%=entity.Bid_Bond %>" style="width: 138px;" />&nbsp;元</label></li>
                                    <div class="clear"></div>

                                    <li style="display:none;"><span><i>*</i>产品清单：</span><label><input name="Bid_ProductType" id="Bid_ProductType1" type="radio" value="1" <%=pub.CheckedRadio(entity.Bid_ProductType.ToString(),"1") %> />展示&nbsp;&nbsp;<input name="Bid_ProductType" id="Bid_ProductType2" type="radio" value="1" <%=pub.CheckedRadio(entity.Bid_ProductType.ToString(),"1") %> />展示</label></li>
                                    <div class="clear"></div>

                                    <li><span><i>*</i>公告内容：</span>
                                    </li>
                                    <textarea id="Bid_Content" name="Bid_Content" rows="80" cols="16"><%=entity.Bid_Content %></textarea>
                                    <script type="text/javascript">
                                        var Bid_ContentEditor;
                                        KindEditor.ready(function (K) {
                                            Bid_ContentEditor = K.create('#Bid_Content', {
                                                width: '100%',
                                                height: '500px',
                                                filterMode: false,
                                                afterBlur: function () { this.sync(); }

                                            });
                                        });
                                    </script>
                                    <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();" class="a05" style="background-color: none; background-image: url(../images/save_buttom.jpg); width: 79px; height: 28px;"></a></li>
                                </ul>

                                <div class="clear"></div>
                            </div>
                            <input name="action" type="hidden" id="action" value="edit" />
                            <input name="Bid_ID" type="hidden" id="Bid_ID" value="<%=entity.Bid_ID %>" />
                        </form>
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
                <%--   <a href="http://wpa.qq.com/msgrd?v=3&uin=800022936&site=qq&menu=yes" target="_blank">--%>
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
                <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes">
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
