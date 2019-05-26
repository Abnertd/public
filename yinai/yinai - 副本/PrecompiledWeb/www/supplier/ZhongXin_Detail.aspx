<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Addr addr = new Addr();

    supplier.Supplier_AuditLogin_Check("/supplier/ZhongXin.aspx");

    //supplier.CheckBelongsType(new BelongsTypeEnum[] { BelongsTypeEnum.Finance });

    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        if (supplierinfo.Supplier_Type == 1)
        {
            if (supplierinfo.Supplier_SQSISActive == 0)//如果买家没有通过授权书审核
            {
                Response.Redirect("/supplier/shouquanshu.aspx");
            }
        }
    }

    ZhongXin ZhongXinApp = new ZhongXin();
    ZhongXinInfo zhongxininfo = ZhongXinApp.GetZhongXinBySuppleir(tools.NullInt(Session["supplier_id"]));
    if (zhongxininfo == null)
    {
        Response.Redirect("/supplier/zhongxin.aspx");
    }

    DateTime startDate;
    DateTime endDate;
    if (DateTime.TryParse(tools.CheckStr(Request["date_start"]), out startDate) && DateTime.TryParse(tools.CheckStr(Request["date_end"]), out endDate))
    {
    }
    else
    {
        startDate = DateTime.Today.AddDays(-7);
        endDate = DateTime.Today;
    }
    
    
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="中信交易流水查询 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>

    <style type="text/css">
        .txt_border {
            height: 25px;
        }

        .input10 {
            background-image: url("../images/buttom_bg.jpg");
            background-repeat: no-repeat;
            border: 0 none;
            cursor: pointer;
            height: 20px;
            padding: 0;
            width: 36px;
        }

        .b14_1_main table td {
            line-height: 12px;
        }

        .blk04_zx input {
            border: 1px solid #cccccc;
            color: #333;
            font-family: "微软雅黑";
            font-size: 12px;
            font-weight: normal;
            height: 24px;
            line-height: 24px;
            padding: 0 0 0 15px;
            width: 100px;
        }


        .input_calendar {
            background-image: url("/Images/icon_calendar.png");
            background-position: left center;
            background-repeat: no-repeat;
            line-height: 20px;
            padding: 0 0 0 20px;
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
    </script>
    <!--示范一个公告层 结束-->

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">我是卖家</a> > <span>中信交易流水查询</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(7, 3); %>
            </div>
            <div class="pd_right">
                <div class="blk14_1" style="margin-top: 0px;">
                    <h2>中信交易流水查询</h2>


                    <div class="blk04_zx" style="margin-top: 10px;">
                        <form name="datescope" method="get" action="?">
                            起始日期：<input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10" readonly="readonly" value="<%=startDate.ToString("yyyy-MM-dd") %>" />
                            终止日期：<input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10" readonly="readonly" value="<%=endDate.ToString("yyyy-MM-dd") %>" />
                            <input name="search" type="submit" class="input10" id="search" value="" style="height: 22px; width: 38px;" />
                        </form>
                    </div>


                    <div class="b14_1_main">



                        <%ZhongXinApp.Account_List(zhongxininfo.SubAccount, startDate, endDate); %>
                    </div>

                </div>
                <div style="padding: 20px; line-height: 28px;">
                    <b>中信电子回单打印流程及说明</b>
                    <ul>
                        <li>1、进入中信银行交易流水打印网址：<a href="https://enterprise.bank.ecitic.com/corporbank/userLogin.do" style="color: Blue" target="_blank"><u>（点击进入）</u></a></li>
                        <li>2、选择栏目条上的“<span style="font-size: 12px; color: Red;">电子回单服务</span>”</li>
                        <li>3、选择“<span style="font-size: 12px; color: Red;">附属账户电子回单</span>”</li>
                        <li>4、输入您的附属账号（ <%=zhongxininfo.SubAccount%>）和打印校验码，输入验证码提交打印</li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
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
    <ucbottom:bottom ID="bottom1" runat="server" />

    <script type="text/javascript">
        $(function () {
            $("#date_start").datepicker({ numberOfMonths: 1 });
            $("#date_end").datepicker({ numberOfMonths: 1 });
        });
    </script>

</body>
</html>
