<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="~/public/top.ascx" TagName="top" TagPrefix="uctop" %>
<%--<%@ Register Src="~/public/Bottom_old.ascx" TagName="bottom" TagPrefix="ucbottom" %>--%>
<%@ Register Src="~/public/Bottom.ascx" TagName="bottom" TagPrefix="ucbottom" %>
<%@ Register Src="~/public/left.ascx" TagName="left" TagPrefix="ucleft" %>

<% 
    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();
    Shop shop = new Shop();
    shop.Shop_Initial();

    int cate_id = tools.CheckInt(Request["cate_id"]);

    Session["shop_page"] = "product";
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=shop.Shop_Name + shop.Shop_Title %></title>
    
    <meta name="Keywords" content="<%=shop.Shop_Keyword%>" />
    <meta name="description" content="<%=shop.Shop_Intro%>" />


    <link href="css/index0<%=shop.Shop_Css %>.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <link href="css/Scroll_Shop.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <uctop:top ID="top" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <div class="partl" style="margin-top: 15px;">

                <div class="pl_left">
                    <ucleft:left ID="left" runat="server" />
                </div>

             <%--   <%if (shop.Shop_RightProduct_IsActive == 1)
                  { %>--%>
                <div class="pl_right">
                    <div class="blk30" style="margin: 0;">
                        <h2 style="font-size: 14px;">所有商品</h2>
                        <% shop.product_filter(); %>
                    </div>

                    <!--排序 开始-->
                    <div class="blk31">
                        <%=shop.Product_View_Mode() %>
                        <div class="clear"></div>
                    </div>
                    <!--排序 结束-->

                    <!--列表 开始-->
                    <div class="list_06" style="width: 989px;">
                        <% shop.product_list("", cate_id, 4); %>
                    </div>
                    <!--列表 结束-->
                </div>
           <%--     <%} %>--%>

                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->



    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
