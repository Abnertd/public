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
    Bid MyBid = new Bid();
    member.Member_Login_Check("/member/bid_add.aspx");
    int list = tools.CheckInt(Request["list"]);
    DateTime Today = DateTime.Now;
    string Bid_MemberCompany = "";
    MemberInfo memberInfo = member.GetMemberByID();

    if (memberInfo != null)
    {
        if (memberInfo.MemberProfileInfo != null)
        {
            Bid_MemberCompany = memberInfo.MemberProfileInfo.Member_Company;
        }
    }
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="创建招标 - 我是买家 - " + pub.SEO_TITLE()%></title>
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
    <script>
        //保证金金额判断
        function check_Bid_Bond(object) {
            $.ajaxSetup({ async: false });
            $("#Bid_Bond_tip").load("/Bid/bid_do.aspx?action=checkbid_bond&val=" + $("#" + object).val() + "&timer=" + Math.random());
            if ($("#Bid_Bond_tip").html().indexOf("cc0000") > 0) {
                $("#m_Bid_Bond").hide();
                return false;
            }
            else {
                $("#m_Bid_Bond").show();
                pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
                return true;
            }
        }
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
        var a = 0;

        function Bidup_Click() {
            
            if (a = 0) {
                a++;

                var index = layer.load(2, {
                    shade: [0.6, '#FFF'] //0.1透明度的白色背景
                });

                $.post("/member/bid_do.aspx?action=add", { "Bid_Title": $('#Bid_Title').val(), "Bid_MemberCompany": $('#Bid_MemberCompany').val(), "Bid_BidStartTime": $('#Bid_BidStartTime').val(), "Bid_BidEndTime": $('#Bid_BidEndTime').val(), "Bid_DeliveryTime": $('#Bid_DeliveryTime').val(), "Bid_Number": $('#Bid_Number').val(), "Bid_Bond": $('#Bid_Bond').val(), "Bid_Content": $('#Bid_Content').val() }, function (data, status) {
                    var a = data.split(',');
                    //bid_id 新加投标ID
                    var a1 = a[0];
                    //type   招标/拍卖
                    var a2 = a[1];

                    if (a2 == "successbid") {
                        layer.close(index);
                        alert("请完善商品清单");
                        //me.resetload();
                        window.location.href = "/member/Bid_Product_add.aspx?BID=" + a1;
                    } else if (a2 == "successauction") {
                        layer.close(index);
                        alert("发布成功");
                        window.location.href = "/supplier/auction_Product_add.aspx?BID=" + a1;
                    } else {
                        layer.close(index);
                        alert("发布失败");
                    }
                });
            }
        }
    </script>




    <script>
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
    <style>
        .input_calendar {
            background-image: url(/Images/icon_calendar.png);
            background-repeat: no-repeat;
            background-position: left center;
            padding-top: 0px;
            padding-right: 0px;
            padding-bottom: 0px;
            /*padding-left: 20px;*/
            width: 100px;
        }

        .layui-anim {
            top: 250px !important;
        }
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 >  <a href="/member/index.aspx">我是买家</a> > <span>创建招标</span></div>
            <div class="partd_1">
                <div class="pd_left">
                    <% =member.Member_Left_HTML(3, 2) %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">创建招标<img src="/images/icon15.jpg"></li>
                                <%-- <li id="a01" onclick="ShowList(0);">创建招标<img src="/images/icon15.jpg"></li>--%>
                                <%-- <li id="a02" onclick="ShowList(1);">添加附件<img src="/images/icon15.jpg"></li>--%>
                            </ul>
                        </h2>
                        <div id="aa01">
                            <form name="frm_bid" id="frm_bid" method="post" action="/member/bid_do.aspx">
                                <div class="b02_main">
                                    <ul style="width: 850px;">
                                        <li><span><i>*</i>公告标题：</span><label><input name="Bid_Title" id="Bid_Title" type="text" style="width: 298px;" value="" /></label></li>
                                        <div class="clear"></div>
                                        <li><span><i>*</i>采购商：</span><label><input name="Bid_MemberCompany" id="Bid_MemberCompany" type="text" style="width: 298px;" value="<%=Bid_MemberCompany %>" readonly="true" /></label></li>
                                        <div class="clear"></div>

                                        <%--  <li><span><i>*</i>报名时间：</span><label><input name="Bid_EnterStartTime" id="Bid_EnterStartTime" type="text" value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'});" />-<input name="Bid_EnterEndTime" id="Bid_EnterEndTime" type="text"value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" readonly="readonly" style="width: 158px;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'});"/></label></li>
<div class="clear"></div>--%>
                                        <li><span><i>*</i>报价时间：</span><label><input name="Bid_BidStartTime" id="Bid_BidStartTime" type="text" class="input_calendar" value="<%=Today.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 128px; padding-left: 25px;" />
                                            <script>$(function () { $("#Bid_BidStartTime").datepicker({ inline: true }); })</script>
                                            -<input name="Bid_BidEndTime" id="Bid_BidEndTime" type="text" value="<%=Today.ToString("yyyy-MM-dd") %>" class="input_calendar" readonly="readonly" style="width: 128px; padding-left: 25px;" /></label></li>
                                        <script>$(function () { $("#Bid_BidEndTime").datepicker({ inline: true }); })</script>
                                        <div class="clear"></div>
                                        <li><span><i>*</i>交货时间：</span><label><input name="Bid_DeliveryTime" class="input_calendar" id="Bid_DeliveryTime" type="text" value="<%=Today.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 128px; padding-left: 25px;" /></label></li>
                                        <script>$(function () { $("#Bid_DeliveryTime").datepicker({ inline: true }); })</script>
                                        <div class="clear"></div>
                                        <%--  <% =new Product() .Product_Unit_Select(Product_ID, "Product_Unit")%>--%>
                                        <%--  <li><span><i>*</i>报价轮次：</span><label><input name="Bid_Number" id="Bid_Number" type="text" value="1" style="width: 138px;" />&nbsp;次</label></li>
                                <div class="clear"></div>--%>
                                        <li><span><i>*</i>报价轮次：</span><label><% =new Bid() .Bid_Times_Select(0, "Bid_Number")%> &nbsp;次</label></li>
                                        <div class="clear"></div>

                                        <li><span><i>*</i>保证金：</span><label><input name="Bid_Bond" id="Bid_Bond" type="text" value="" style="width: 138px;" onfocus="$('#b_Bid_Bond').hide();" onblur="check_Bid_Bond('Bid_Bond');" />&nbsp;元<strong class="regtip" id="Bid_Bond_tip" style="font-size: 12px;"></strong></label></li>
                                        <div class="clear"></div>

                                        <li style="display: none;"><span><i>*</i>产品清单：</span><label><input name="Bid_ProductType" id="Bid_ProductType1" type="radio" value="0" />不展示&nbsp;&nbsp;<input name="Bid_ProductType" id="Bid_ProductType2" type="radio" value="1" checked />展示</label></li>
                                        <div class="clear"></div>

                                        <li><span><i>*</i>公告内容：</span>
                                        </li>
                                        <textarea id="Bid_Content" name="Bid_Content" rows="80" cols="60"></textarea>
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
                                        <%--<li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();" class="a05" style="background-color: none; width: 79px;">保 存</a></li>--%>
                                        <li><a href="javascript:void(0);" onclick="Bidup_Click();" class="a05" style="background-color: none; width: 79px;">保 存</a></li>
                                    </ul>

                                    <div class="clear"></div>
                                </div>
                                <input name="action" type="hidden" id="action" value="add" />

                            </form>
                        </div>

                        <%--添加附件  卡签开始--%>
                        <%--<div class="b02_main" id="aa02" >
                            <div class="blk14_1" style="margin-top: 0px;">
                              <div class="b14_1_main" style="border-left:none;border-right:none;">
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
                        </div>--%>

                        <%--添加附件  卡签结束--%>
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
