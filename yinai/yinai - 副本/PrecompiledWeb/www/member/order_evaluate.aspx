<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Product product = new Product();
    int orders_id = 0, orders_type = 0;
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    //member.Member_Login_Check("/member/order_evaluate.aspx");
    if (orders_sn == "")
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_list.aspx");
    }

    OrdersInfo ordersInfo = member.GetOrdersInfoBySN(orders_sn);
    if (ordersInfo != null)
    {
        if (ordersInfo.Orders_BuyerID != tools.NullInt(Session["member_id"]))
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_list.aspx");
        }
        else
        {
            //if (ordersInfo.Orders_Status != 2 || (ordersInfo.Orders_IsEvaluate != 0))
            if (ordersInfo.Orders_Status != 2 || (ordersInfo.Orders_IsEvaluate != 0))
            {
                pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_list.aspx");
            }
            orders_id = ordersInfo.Orders_ID;
            orders_type = ordersInfo.Orders_Type;
        }
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单评价 - 我是买家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
    <style type="text/css">
        .input02 {
            height: auto;
            padding-left: 0px;
            width: auto;
            background: none;
            box-shadow: none;
            border: 0px;
        }

        .b14_1_main table td {
            border-bottom: 0px;
        }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position"><a href="/index.aspx">首页</a> > <a href="/member/">会员中心</a> > 交易管理 > <strong>订单评价</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(1, 1) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>订单评价</h2>
                        <div class="b14_1_main">
                            <div class="b07_main">
                                <div class="b07_info04" style="padding-top: 0px">
                                    <form action="/member/orders_do.aspx" method="post" name="frm_add" id="frm_add" onkeydown="if(event.keyCode==13){return false;}">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <%if (orders_type == 1)
                                              { %>
                                            <tr>
                                                <td style="font-size: 14px; font-weight: bold;" align="left">对商品进行评价
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%product.Product_Review_Add_Form(orders_id); %>
                                                </td>
                                            </tr>

                                            <%} %>
                                            <tr>
                                                <td height="30" style="font-size: 14px; font-weight: bold;" align="left">对卖家进行评价
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="5" cellspacing="0" class="table_padding_5">
                                                        <tr>
                                                            <td width="100" align="right">卖家的服务态度
                                                            </td>
                                                            <td>
                                                                <input type="radio" name="attitude_review_star" value="5" class="input02" style="box-shadow: none" checked="checked" />很好&nbsp;<%=product.Review_ShowStar("review", 0, 0, 5) %>
                                                                <input type="radio" name="attitude_review_star" value="4" class="input02" style="box-shadow: none" />好&nbsp;<%=product.Review_ShowStar("review", 0, 0, 4) %>
                                                                <input type="radio" name="attitude_review_star" value="3" class="input02" style="box-shadow: none" />一般&nbsp;<%=product.Review_ShowStar("review", 0, 0, 3) %>
                                                                <input type="radio" name="attitude_review_star" value="2" class="input02" style="box-shadow: none" />较差&nbsp;<%=product.Review_ShowStar("review", 0, 0, 2) %>
                                                                <input type="radio" name="attitude_review_star" value="1" class="input02" style="box-shadow: none" />很差&nbsp;<%=product.Review_ShowStar("review", 0, 0, 1) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100" align="right">卖家的发货速度
                                                            </td>
                                                            <td>
                                                                <input type="radio" name="delivery_review_star" value="5" checked="checked" class="input02" style="box-shadow: none" />很好&nbsp;<%=product.Review_ShowStar("review", 0, 0, 5) %>
                                                                <input type="radio" name="delivery_review_star" value="4" class="input02" style="box-shadow: none" />好&nbsp;<%=product.Review_ShowStar("review", 0, 0, 4) %>
                                                                <input type="radio" name="delivery_review_star" value="3" class="input02" style="box-shadow: none" />一般&nbsp;<%=product.Review_ShowStar("review", 0, 0, 3) %>
                                                                <input type="radio" name="delivery_review_star" value="2" class="input02" style="box-shadow: none" />较差&nbsp;<%=product.Review_ShowStar("review", 0, 0, 2) %>
                                                                <input type="radio" name="delivery_review_star" value="1" class="input02" style="box-shadow: none" />很差&nbsp;<%=product.Review_ShowStar("review", 0, 0, 1) %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100" align="right">评价说明
                                                            </td>
                                                            <td>
                                                                <textarea cols="50" rows="5" name="Shop_Evaluate_Note"></textarea>
                                                            </td>
                                                        </tr>
                                                        <%if (tools.CheckInt(Application["Product_Review_Config_VerifyCode_IsOpen"].ToString()) == 1)
                                                          { %>
                                                        <tr>
                                                            <td width="100" align="right">验证码
                                                            </td>
                                                            <td>
                                                                <input name="review_verify" type="text" width="50" maxlength="10" />
                                                                &nbsp;
                                                <img src="/Public/verifycode.aspx" id="var_img" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());"
                                                    alt="看不清？换一张" width="60" style="cursor: pointer; padding-left: 5px; display: inline;" height="26"
                                                    align="absmiddle" />
                                                                <span style="color: #999999; font-size: 12px;">看不清？<a href="javascript:;" style="color: #006699;"
                                                                    onclick="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());">换一张</a></span>
                                                            </td>
                                                        </tr>
                                                        <%} %>
                                                        <tr>
                                                            <td width="100" align="right"></td>
                                                            <td>
                                                                <a href="javascript:void(0);" onclick="orders_evaluate();" class="a11"></a>
                                                                <input type="hidden" name="orders_sn" id="orders_sn" value="<%=orders_sn %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100" align="right"></td>
                                                            <td height="10"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
