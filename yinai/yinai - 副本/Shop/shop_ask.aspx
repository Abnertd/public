<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/public/top.ascx" TagName="top" TagPrefix="uctop" %>
<%@ Register Src="~/public/bottom.ascx" TagName="bottom" TagPrefix="ucbottom" %>
<%@ Register Src="~/public/left.ascx" TagName="left" TagPrefix="ucleft" %>

<%
    
    Public_Class pub = new Public_Class();
    Product product = new Product();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Shop shop = new Shop();
    shop.Shop_Initial();
    string index_content = "";
    string index_top = "";
    string index_left = "";
    Session["shop_page"] = "Consultation";

    string page_title, page_content;
    page_title = "";
    page_content = "";
    SupplierShopPagesInfo pageinfo = null;

    pageinfo = shop.GetSupplierShopPagesByIDSign("INDEXLEFT", tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo != null)
    {
        index_left = pageinfo.Shop_Pages_Content;

    }
   
  
 %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%="咨询留言 - " + shop.Shop_Name + shop.Shop_Title%></title>
 
<meta name="Keywords" content="<%=shop.Shop_Keyword%>" />
<meta name="description" content="<%=shop.Shop_Intro%>" />
<link href="css/index0<%=shop.Shop_Css %>.css" rel="stylesheet" type="text/css" />
    <link href="css/Scroll_Shop.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/1.js"></script>
<script type="text/javascript" src="/scripts/common.js"></script>

    <style type="text/css">
        .shop_active_list { padding-top:10px; }
        .shop_active_list img { display:inline; }
        .shop_active_list table table td { line-height:26px; }
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
                        <h2 style="font-size: 14px;">咨询留言</h2>
                    </div>
                    <!--列表 开始-->
                    <div class="shop_active_list">
                        <%
                            shop.Product_Shoppingask(0, 10, 1);
                            shop.Product_Ask_Form(0);
                            Response.Write("</div></div>");
                        %>
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
