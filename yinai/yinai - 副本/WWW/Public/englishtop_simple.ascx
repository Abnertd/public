<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Control Language="C#" ClassName="englishtop_simple.ascx"  %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>English Home</title>
    <link rel="stylesheet" type="text/css" href="css/index.css" />
    <script src="/js/1.js" type="text/javascript"></script>
</head>
<body>
    <div class="En_top">
        <div class="En_top_main">
            <div class="En_top_main_left">
                 <a href="/index.aspx"> <img src="/images/en_logo.png" width="104" height="65"  /></a></div>
            <div class="En_top_main_right">
                <ul>
                    <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/International/Index.aspx" class="a_nav">Home</a></li>
                     <li <%=(Convert.ToString(Session["Position"]) == "Product" ? " class=\"on\" " :" ") %>><a href="/International/Product.aspx" class="a_nav">Product</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Service" ? " class=\"on\" " :" ") %>><a href="/International/index.aspx/#En_info3" class="a_nav">Service</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Dynamics" ? " class=\"on\" " :" ") %>><a href="/International/marketDynamics.aspx" class="a_nav">Market Dynamics</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Contact" ? " class=\"on\" " :" ") %>><a href="/International/index.aspx/#En_info4" class="a_nav">Contact Us</a></li>  
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
