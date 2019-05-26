<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/public/top.ascx" TagName="top" TagPrefix="uctop" %>
<%@ Register Src="~/public/bottom.ascx" TagName="bottom" TagPrefix="ucbottom" %>
<%@ Register Src="~/public/left.ascx" TagName="left" TagPrefix="ucleft" %>
<%
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Shop shop = new Shop();
    shop.Shop_Initial();
    Session["shop_page"] = "article";
 %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%="活动区 - "+shop.Shop_Name + shop.Shop_Title %></title>
  
<meta name="Keywords" content="<%=shop.Shop_Keyword%>" />
<meta name="description" content="<%=shop.Shop_Intro%>" />
<link href="css/index0<%=shop.Shop_Css %>.css" rel="stylesheet" type="text/css" />
<link href="css/Scroll_Shop.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/1.js"></script>
<script type="text/javascript" src="/scripts/common.js"></script>

    <style type="text/css">
        /* ====================
             店铺活动 模块
        ==================== */
        .shop_active_list{padding-left:10px; padding-right:10px}
        .shop_active_list ul{list-style-type:none; margin:0px; padding:0px;}
        .shop_active_list ul li{list-style-type:none; padding-top:10px; padding-bottom:10px; display:inline; float:left;border-bottom:1px dashed #cccccc;}
        .shop_active_list ul li#list_bgcolor{background-color:#F8F8F8}
        .shop_active_listL{width:805px; float:left}
        .shop_active_listR{width:150px; float:left; text-align:right}
    </style>

</head>
<body>
    <uctop:top ID="top" runat="server" />

    <!--主体 开始-->
    <div class="content02" style="background-color:#FFF;">
        <div class="content02_main" style="background-color:#FFF;">
            <div class="partl" style=" margin-top:15px;">

                <div class="pl_left">
                    <ucleft:left ID="left" runat="server" />
                </div>

                <div class="pl_right">
                    <div class="blk30" style="margin: 0;">
                        <h2 style="font-size: 14px;">店铺公告</h2>
                    </div>

                    <!--列表 开始-->
                    <div class="shop_active_list">
                        <%shop.Shop_Article_List();%>
                    </div>
                    <!--列表 结束-->
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->

    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
