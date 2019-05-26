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
    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Online_Add.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="在线客服添加 - 会员中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />

    <!--layer弹出框.js .css引用 开始-->
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <!--layer弹出框.js .css引用 结束-->

    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>


    <script type="text/javascript" src="/scripts/common.js"></script>
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
        .yz_blk19_main img {
            display: inline;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #fff">
            <!--位置说明 开始-->
            <div class="position">
                <%--  您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Online_Add.aspx">在线客服添加</a>--%>
                <div class="position">当前位置 > <a href="/supplier/">我是卖家 > </a><strong>在线客服添加</strong></div>
            </div>
            <%--  <div class="clear"></div>--%>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 12); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>在线客服管理</h2>
                        <div class="b14_1_main">

                            <form name="formadd" method="post" action="/supplier/Supplier_Shop_Online_do.aspx">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">客服名称
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <input name="Online_Name" type="text" id="Online_Name" class="txt_border"
                                                size="40" maxlength="100" value="" />
                                            <span class="t14_red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">客服代码类型
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <input type="radio" name="Online_Type" value="QQ" checked />
                                            QQ客服代码
                            <input type="radio" name="Online_Type" value="MSN" />
                                            MSN客服账号
                            <span class="t14_red">*</span> 两者任选其一
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">QQ客服代码
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <textarea name="Online_QQ" cols="40" rows="5"></textarea>
                                            <span class="t14_red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">MSN客服账号
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <input name="Online_MSN" type="text" id="Online_MSN" class="txt_border"
                                                size="40" maxlength="100" value="" />
                                            <span class="t14_red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">状态
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <input type="radio" name="Online_IsActive" value="0" />
                                            不启用
                            <input type="radio" name="Online_IsActive" value="1" checked />
                                            启用
                            <span class="t14_red">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="line-height: 24px;" class="t12_53">分组排序
                                        </td>
                                        <td align="left" style="text-align: left">
                                            <input name="Online_Sort" type="text" id="Online_Sort" class="txt_border"
                                                size="10" maxlength="50" value="0" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="t12_53"></td>
                                        <td align="left" style="text-align: left">
                                            <input name="action" type="hidden" id="action" value="add"><input name="btn_submit" type="image" src="/images/save_buttom.jpg" /></td>
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
