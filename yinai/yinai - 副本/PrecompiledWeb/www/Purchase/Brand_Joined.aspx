﻿<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<%    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Product product = new Product();
    CMS cms = new CMS();
    Product myProduct = new Product();
    AD ad = new AD();
    Session["SubPosition"] = "Brand";
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="品牌加盟 - "+ pub.SEO_TITLE()%></title>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <!--商品排行 start-->
    <script src="/scripts/jquery-extend-AdAdvance.js" type="text/javascript"></script>
    <!--商品排行 end-->
    <script type="text/javascript">
        function show_hiddendiv() {
            document.getElementById("hidden_div").style.display = 'block';
            document.getElementById("_strHref").href = 'javascript:hidden_showdiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02_2.png>";
        }
        function hidden_showdiv() {
            document.getElementById("hidden_div").style.display = 'none';
            document.getElementById("_strHref").href = 'javascript:show_hiddendiv();';
            document.getElementById("_strSpan").innerHTML = "<img src=/images/icon02.png>";
        }
    </script>
</head>
<body>

     <!--头部 开始-->
    <uctop:top ID="Top" runat="server" />
    <!--头部 结束-->

    <!--主体 开始-->
    <div class="content" style="margin-top: 0;">
        <div class="content_main">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <strong>品牌加盟</strong></div>
            <!--位置说明 结束-->
            <!--广告 开始-->
            <div class="ad" style="height: 161px; overflow: hidden;"><%=ad.AD_Show("BrandJoined_Index_AD","","cycle",0) %></div>
            <!--广告 结束-->
            <div class="partb">
                <h3 class="title02">
                    <ul>
                        <li class="on"><a href="javascript:;">默认</a></li>
                        <li><span>排序</span></li>
                    </ul>
                    招商品牌<strong><%=product.BrandJoined_Count() %></strong>款
                       <div class="clear"></div>
                </h3>
                <!--列表 开始-->
                <div class="list_03">
                 <%product.BrandJoined_List(); %>
                </div>
                <!--列表 结束-->
            </div>
        </div>
    </div>
    <!--主体 结束-->


     <!--尾部 开始-->
    <ucbottom:bottom ID="Bottom" runat="server" />
    <!--尾部 结束-->
</body>
</html>
