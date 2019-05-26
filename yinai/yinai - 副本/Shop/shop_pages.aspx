<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/public/top.ascx" TagName="top" TagPrefix="uctop" %>
<%@ Register Src="~/public/bottom.ascx" TagName="bottom" TagPrefix="ucbottom" %>
<%@ Register Src="~/public/left.ascx" TagName="left" TagPrefix="ucleft" %>

<%
    
    Public_Class pub = new Public_Class();
    ITools tools;
    Product product = new Product();
    tools = ToolsFactory.CreateTools();
    Shop shop = new Shop();
    shop.Shop_Initial();
    string index_content = "";
    string index_top = "";
    string index_left = "";
    string page_sign=tools.CheckStr(tools.NullStr(Request["sign"]));
    if(page_sign.Length==0)
    {
        Response.Redirect("/");
    }
    string page_title,page_content;
    page_title="";
    page_content="";
    SupplierShopPagesInfo pageinfo = shop.GetSupplierShopPagesByIDSign(page_sign, tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo != null)
    {
        page_title = pageinfo.Shop_Pages_Title;
        page_content = pageinfo.Shop_Pages_Content;
        Session["shop_page"] = pageinfo.Shop_Pages_Sign;
   
    }
    else
    {
        Response.Redirect("/");
    }

 %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=page_title + " - "+shop.Shop_Name + shop.Shop_Title %></title>
<meta name="Keywords" content="<%=shop.Shop_Keyword%>" />
<meta name="description" content="<%=shop.Shop_Intro%>" />
<link href="css/index0<%=shop.Shop_Css %>.css" rel="stylesheet" type="text/css" />
    <link href="css/Scroll_Shop.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/scripts/1.js"></script>
<script type="text/javascript" src="/scripts/common.js"></script>

</head>
<body>
    <uctop:top ID="top" runat="server" />

    <%=page_content%>
    
    <ucbottom:bottom ID="bottom" runat="server" />
</body>
</html>
