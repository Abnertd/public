<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/englishtop_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/englishbottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%    Public_Class pub = new Public_Class();
      ITools tools = ToolsFactory.CreateTools();
      Supplier supplier = new Supplier();
      CMS cms = new CMS();

     
      AD ad = new AD();
       
      Statistics statistics = new Statistics();
      Session["Position"] = "Dynamics";
   
      Product product = new Product();
      Bid MyBid = new Bid();
      Logistics MyLogistics = new Logistics();


    
                                                                  
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>English Market Dynamics</title>
    <link rel="stylesheet" type="text/css" href="/css/index.css" />
    <script src="/scripts/1.js" type="text/javascript"></script>
    <style type="text/css">  html,body,ul,ol,li,p,h1,h2,h3,h4,h5,h6,form,fieldset,img,div,dl,dt,dd{margin:0;padding:0;border:0;font-family:Arial;}</style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" IsIndex="true" />


<div class="En_top_banner">
   <%-- <img src="/images/banner04.png" width="1200" height="478" />--%>
      <%=ad.AD_Show("FinancePage_TopBigPic", "", "cycle", 0)%>


</div>
<div class="clear"></div>
<div class="En_info1">
	<div class="En_info1_main">
    	<div class="scdt_title">Market Dynamics</div>     
        <%=cms.GetFinancePageMarketDy() %>
    </div>
</div>

<div class="clear"></div>
    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>
