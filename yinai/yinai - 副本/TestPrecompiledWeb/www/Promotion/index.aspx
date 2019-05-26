<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%
    int promotion_id;
    string promotion_title,promotion_tophtml;
    promotion_title = "";
    promotion_tophtml = "";
    AD ad = new AD();
    ITools tools;
    tools =ToolsFactory.CreateTools();
    Promotion MyApp=new Promotion();
    Product Myproduct = new Product();
    Public_Class pub = new Public_Class();
    PromotionInfo entity;
    promotion_id = tools.CheckInt(Request["id"]);
    entity = MyApp.GetPromotionByID(promotion_id);
    if (entity == null)
    {
        Response.Redirect("/");
    }
    else
    {
        promotion_title = entity.Promotion_Title;
        promotion_tophtml = entity.Promotion_TopHtml;
    }
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=promotion_title + " - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index3.css" rel="stylesheet" type="text/css" />
    <link href="/css/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    
    <style type="text/css">
        .promotion_product_list ul{ margin:10px 5px 5px;}
        .promotion_product_list li{ width:180px; margin:10px; float:left; display:inline;}
        .promotion_product_list li img{ width:178px; border:solid 1px #d0d0d0; }
        .promotion_product_list .p_name{ line-height:20px; height:36px; padding:5px 0px; text-align:left; overflow:hidden; }
        .promotion_product_list .p_price{ line-height:22px; height:22px; padding-bottom:5px; }
        .promotion_product_list .p_price span{ color:#f00; font-weight:bold; font-family: "Verdana", "Arial", "Helvetica", "sans-serif"; }
        .promotion_product_list .p_btn img{ width:auto; height:auto;  display:inline; border:none;}
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    <!--主体 开始-->
    <div id="zkwebwrap">
        
        <table width="1210" border="0" cellpadding="0" cellspacing="0">
    	<tr><td><%=promotion_tophtml%></td></tr>
    	<%--<tr><td><%=ad.AD_Show("Product_Promotion_Img", promotion_id.ToString(), "productimglist", 6)%></td></tr>--%>
    	<tr><td class="promotion_product_list"><%Myproduct.Promotion_Product_List(entity.Promotion_Type,entity.PromotionProducts); %></td></tr>
    	</table>
        
    </div>
    <!--主体 结束-->
    
  <ucbottom:bottom runat="server" ID="Bottom" />
    

</body>
</html>

