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


    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    member.Member_Login_Check("/member/order_close.aspx?orders_sn=" + orders_sn);

    if (orders_sn == "")
    {
        pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_list.aspx");
    }
    string Orders_Note = "";
    OrdersInfo ordersinfo = member.GetOrdersInfoBySN(orders_sn);
    if (ordersinfo != null)
    {
        if (ordersinfo.Orders_Status != 0 || ordersinfo.Orders_PaymentStatus != 0 || ordersinfo.Orders_DeliveryStatus != 0)
        {
            orders_sn = "";
        }
        else
        {
            Orders_Note = ordersinfo.Orders_Note;
        }
        if (ordersinfo.Orders_BuyerID != tools.NullInt(Session["member_id"]))
        {
            orders_sn = "";
        }
    }
    else
    {
        orders_sn = "";
    }

    if (orders_sn == "")
    {
        pub.Msg("error", "错误信息", "无法执行此操作", false, "/member/order_list.aspx");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单取消 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>

    <style type="text/css">
        .buttonSkinA {
            border: none;
            _border: 0;
            background-image: url(../images/a_bg01.jpg);
            background-repeat: no-repeat;
            width: 74px;
            height: 26px;
            font-size: 12px;
            font-weight: normal;
            text-align: center;
            line-height: 26px;
            text-align: center;
            color: #333;
            display: inline-block;
            vertical-align: middle;
            margin-top: 18px;
        }
    </style>

</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">

            <!--位置说明 开始-->
            <div class="position"><a href="/index.aspx">首页</a> > <a href="/member/">会员中心</a> > 交易管理 > <strong>订单取消</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%=member.Member_Left_HTML(1, 1) %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>订单取消</h2>
                        <div class="blk08" style="margin-top: 15px; padding: 10px;">

                            <form id="frm_edit" name="frm_edit" method="post" action="orders_do.aspx?orders_sn=<%=orders_sn%>">
                                <table border="0" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td height="20" style="text-align: right; padding-right: 10px;">
                                            <strong>取消原因</strong>
                                        </td>
                                        <td>
                                            <textarea name="orders_close_note" id="orders_close_note" cols="45" rows="5"></textarea>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td style="text-align: left;">
                                            <input type="hidden" name="orders_sn" value="<% =orders_sn %>" />
                                            <input type="hidden" name="action" value="ordersclose" />
                                            <input type="submit" name="btn_add" value=" 确定 " class="buttonSkinA" />
                                        </td>
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
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
