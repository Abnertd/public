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
    supplier.Supplier_Login_Check("/supplier/auction_add.aspx");

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
    DateTime Today = DateTime.Now;
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="创建拍卖 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />

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
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <%--    <script src="/scripts/jquery-ui/jquery-ui.js"></script>--%>

    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
    <%--    <script src="/scripts/My97DatePicker/WdatePicker.js"></script>--%>
    <script type="text/javascript">
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
    <style type="text/css">
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
    </style>
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



        function Auction_up_Click() {

            var index = layer.load(2, {
                shade: [0.4, '#FFF'] //0.1透明度的白色背景
            });

            $.post("/supplier/auction_do.aspx?action=addauction", { "Bid_Title": $('#Bid_Title').val(), "Bid_MemberCompany": $('#Bid_MemberCompany').val(), "Bid_BidStartTime": $('#Bid_BidStartTime').val(), "Bid_BidEndTime": $('#Bid_BidEndTime').val(), "Bid_DeliveryTime": $('#Bid_DeliveryTime').val(), "Bid_Number": $('#Bid_Number').val(), "Bid_Bond": $('#Bid_Bond').val(), "Bid_Content": $('#Bid_Content').val() }, function (data, status) {
                var a = data.split(',');
                //bid_id 新加投标ID
                var a1 = a[0];
                //type   招标/拍卖
                var a2 = a[1];

                if (a2 == "successbid") {
                    layer.close(index);
                    alert("发布成功");
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
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position">当前位置 >  <a href="/supplier/">我是卖家</a> > 投标拍卖管理 > <strong>创建拍卖</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(5, 4); %>
                </div>
                <div class="pd_right">

                    <div class="blk02">
                        <h2>
                            <ul>
                                <li class="on">创建拍卖<img src="/images/icon15.jpg" /></li>
                            </ul>
                        </h2>
                        <form name="frm_bid" id="frm_bid" method="post" action="/supplier/auction_do.aspx">
                            <div class="b02_main">
                                <ul style="width: 850px;">
                                    <li><span><i>*</i>公告标题：</span><label><input name="Bid_Title" id="Bid_Title" type="text" style="width: 298px;" value="" /></label></li>
                                    <div class="clear"></div>
                                    <li><span><i>*</i>拍卖用户：</span><label><input name="Bid_MemberCompany" id="Bid_MemberCompany" type="text" style="width: 298px;" value="" /></label></li>
                                    <div class="clear"></div>



                                    <div class="clear"></div>

                                    <li><span><i>*</i>竞价时间：</span><label><input name="Bid_BidStartTime" id="Bid_BidStartTime" type="text" value="<%=Today.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 158px; padding-left: 25px;" class="input_calendar" />
                                        <script type="text/javascript">$(function () { $("#Bid_BidStartTime").datepicker({ inline: true }); })</script>
                                        -
                                     <input name="Bid_BidEndTime" id="Bid_BidEndTime" type="text" value="<%=Today.ToString("yyyy-MM-dd") %>" readonly="readonly" style="width: 158px; padding-left: 25px;" class="input_calendar" /></label></li>
                                    <script type="text/javascript">$(function () { $("#Bid_BidEndTime").datepicker({ inline: true }); })</script>
                                    <div class="clear"></div>


                                    <li><span><i>*</i>竞价轮次：</span><label> <% =new Bid() .Bid_Times_Select(0, "Bid_Number")%>&nbsp;次</label></li>
                                    <div class="clear"></div>
                                    <%-- <li><span><i>*</i>保证金：</span><label><input name="Bid_Bond" id="Bid_Bond" type="text" value="" style="width: 138px;" />&nbsp;元</label></li>--%>
                                    <li><span><i>*</i>保证金：</span><label><input name="Bid_Bond" id="Bid_Bond" type="text" value="" style="width: 138px;" onfocus="$('#b_Bid_Bond').hide();" onblur="check_Bid_Bond('Bid_Bond');" />&nbsp;元<strong class="regtip" id="Bid_Bond_tip" style="font-size: 12px;"></strong></label></li>
                                    <div class="clear"></div>

                                    <%--  <li><span><i>*</i>上传附件：</span><label><iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=Bid&formname=frm_bid&frmelement=Bid_Attachments_Path&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe></label></li>
                                <div class="clear"></div>--%>


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
                                    <%--  <li><a href="javascript:void(0);" onclick="$('#frm_bid').submit();" class="a05" style="background-color: none; background-image: url(../images/save_buttom.jpg); width: 79px; height: 28px;"></a></li>--%>
                                    <li><a href="javascript:void(0);" onclick="Auction_up_Click();" class="a05" style="background-color: none; width: 79px;">保 存</a></li>
                                </ul>

                                <div class="clear"></div>
                            </div>
                            <input name="action" type="hidden" id="action" value="addauction" />
                            <input name="Bid_DeliveryTime" id="Bid_DeliveryTime" type="hidden" value="<%=Today.ToString("yyyy-MM-dd") %>" />
                            <input name="Bid_ProductType" id="Bid_ProductType" type="hidden" value="1" />
                        </form>
                    </div>
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
