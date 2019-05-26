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
    supplier.Supplier_Login_Check("/supplier/order_list.aspx");
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int menu = tools.NullInt(Request["menu"]);
    string orders_sn = tools.CheckStr(Request["orders_sn"]);
    supplier.Supplier_Login_Check("/supplier/order_view.aspx?orders_sn=" + orders_sn);

    OrdersInfo entity = supplier.GetOrdersInfoBySN(orders_sn,tools.NullInt(Session["supplier_id"]));
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/order_list.aspx");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="订单详情 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
 
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
 <style type="text/css">
 .main table td{padding:3px;}
 </style>
    <script type="text/javascript">
        function turnnewpage(url) {
            location.href = url
        }
    </script>
</head>
<body>
    <uctop:top ID="top2" runat="server" />

    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>订单详情</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(1, 1); %>
                </div>
                <div class="pd_right" style="width: 972px;">
                    <div class="blk14_1" style="margin-top: 0px;">
                       <% supplier.Buy_order_Detail(orders_sn); %>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <div class="clear"></div>
    <ucbottom:bottom ID="bottom2" runat="server" />

</body>
</html>
