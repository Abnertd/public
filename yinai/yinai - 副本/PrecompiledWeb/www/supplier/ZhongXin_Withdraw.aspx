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

    //supplier.Supplier_AuditLogin_Check("/supplier/ZhongXin.aspx");

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
            //if (supplierinfo.Supplier_SQSISActive == 0)//如果买家没有通过授权书审核
            //{
            //    Response.Redirect("/supplier/shouquanshu.aspx");
            //}
        }
    }

    ZhongXin ZhongXinApp = new ZhongXin();
    ZhongXinInfo zhongxininfo = ZhongXinApp.GetZhongXinBySuppleir(tools.NullInt(Session["supplier_id"]));
    if (zhongxininfo == null)
    {
        Response.Redirect("/supplier/zhongxin.aspx");
    }

    decimal ZhongXinAmount = ZhongXinApp.GetAmount(zhongxininfo.SubAccount);
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="中信银行出金 - 会员中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->
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
            您现在的位置 > <a href="/">首页</a> > <a href="/supplier/index.aspx">我是卖家</a> > <span>中信账户出金</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% supplier.Get_Supplier_Left_HTML(7, 2); %>
            </div>
            <div class="pd_right">
                <div class="blk14_1" style="margin-top: 0px;">
                    <h2>中信账户申请出金</h2>

                    <div class="b14_1_main">
                        <form name="frm_account_profile" method="post" action="/supplier/account_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td width="100" align="right" class="t12_53">
                                        <b>中信附属账号</b>
                                    </td>
                                    <td align="left" style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">中信附属账号
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.SubAccount%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="t12_53">附属账号余额
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=ZhongXinAmount%>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">
                                        <b>出金账户信息</b>
                                    </td>
                                    <td align="left" style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">出金收款银行
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.ReceiptBank%>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">出金银行名称
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.BankName %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">出金银行行号
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.BankCode %>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100" align="right" class="t12_53">出金账户名称
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.OpenAccountName%>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100" align="right" class="t12_53">出金收款账号
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <%=zhongxininfo.ReceiptAccount%>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">
                                        <b>出金信息</b>
                                    </td>
                                    <td align="left" style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">申请出金金额
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <input name="Withdraw" id="Withdraw" class="txt_border" size="20" maxlength="20" value="<%=ZhongXinAmount %>" />元
                                <span class="t12_red">（下午16点以后提交的出金申请将于次日处理，请不要重复提交）</span></td>
                                </tr>
                                <tr>
                                    <td width="100" align="right" class="t12_53">出金手续费
                                    </td>
                                    <td align="left" id="WithdrawFeeTd" style="text-align: left"><%=ZhongXinApp.WithdrawFee(ZhongXinAmount, zhongxininfo.ReceiptBank) %></td>
                                </tr>
                                <tr>
                                    <td align="right" class="t12_53"></td>
                                    <td>
                                        <input name="action" type="hidden" value="zhongxinwithdraw" />
                                        <%if (zhongxininfo.Register != 2)
                                          {%>
                                        <input name="btn_submit" type="button" value="申请出金"  style="padding: 0px 10px; height: 26px;width:80px; background-color: #666666; color: #000; border: 1px solid #666666; -moz-border-radius: 3px; border-radius: 3px; font-size: 12px; font-weight: bold; cursor: pointer; " />
                                        <% }
                                          else
                                          {%>
                                        <input name="btn_submit" type="submit" value="申请出金" style="padding: 0px 10px; height: 26px; background-color: #c00; color: #fff; border: 1px solid #c00; -moz-border-radius: 3px; border-radius: 3px; font-size: 12px; font-weight: bold; cursor: pointer;" /><%} %>
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                </div>
                <div style="padding: 20px; line-height: 28px;">
                    <b>出金手续费收费标准</b>
                    <ul>
                        <li>1、中信内异地转账出金收费标准：</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;1万元（含）以下：5.0</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;1-10万元（含）：	10.0</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;10-50万元（含）：15.0</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;50-100万元（含）：20.0</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;100万元以上：转账金额×0.002%（最多200元）</li>
                        <li>2、跨行同城异地转账出金收费标准：</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;1万元（含）以下	5.50</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;1-10万元（含）：10.50</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;10-50万元（含）：15.50</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;50-100万元（含）：20.50</li>
                        <li>&nbsp;&nbsp;&nbsp;&nbsp;100万元以上：转账金额×0.002%＋0.5（最多200.5元）</li>
                    </ul>
                    <b></b>

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


</body>
</html>
