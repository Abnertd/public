<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Control Language="C#" ClassName="smalltop_simple.ascx"  %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>英文主页</title>
    <link rel="stylesheet" type="text/css" href="css/index.css" />
    <script src="/js/1.js" type="text/javascript"></script>
</head>










<body>
    <div class="En_top">
        <div class="En_top_main">
            <div class="En_top_main_left">
                <img src="/images/en_logo.png" width="104" height="65" /></div>
            <div class="En_top_main_right">
                <ul>
                   <%-- <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/International/Index.aspx" class="a_nav">首 页</a></li>--%>
                     <%-- <li><a href="/International/index.aspx">首页</a></li>
                    <li><a href="/International/Detail.aspx">商城选购</a></li>
                    <li><a href="/International/index.aspx/#En_info3">招标</a></li>
                    <li><a href="/International/marketDynamics.aspx">Market Dynamics</a></li>
                    <li><a href="/International/index.aspx/#En_info4">Contact Us</a></li>--%>

<%--                      <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/Index.aspx" class="a_nav">首 页</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "Category" ? " class=\"on\" " :" ") %>><a href="/product/category.aspx" class="a_nav">商城选购</a></li>

                <li <%=(Convert.ToString(Session["Position"]) == "Bid" ? " class=\"on\" " :" ") %>><a href="/bid/">招标拍卖</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "Logistics" ? " class=\"on\" " :" ") %>><a href="/Logistics/">仓储物流</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "financialservice" ? " class=\"on\" " :"on ") %>><a href="/Financial/index.aspx" class="a_nav">金融中心</a></li>


                <li <%=(Convert.ToString(Session["Position"]) == "IndustryInformation" ? " class=\"on\" " :" ") %>><a href="/article/Index.aspx" class="a_nav">行情资讯</a></li>--%>
                    
                      <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/Index.aspx" class="a_nav">首 页</a></li>
                <li <%=(Convert.ToString(Session["Position"]) == "Category" ? " class=\"on\" " :" ") %>><a href="/product/category.aspx" class="a_nav">商城选购</a></li>

                <li><a href="/bid/">招标拍卖</a></li>
                <li><a href="/Logistics/">仓储物流</a></li>
                <li ><a href="/Financial/index.aspx" class="a_nav"  style="color:#ff6600">金融中心</a></li>


                <li ><a href="/article/Index.aspx" class="a_nav">行情资讯</a></li>


                </ul>
            </div>
        </div>
    </div>
</body>
</html>
