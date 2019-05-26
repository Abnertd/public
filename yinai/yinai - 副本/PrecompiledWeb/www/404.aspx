<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<%    
    Public_Class pub = new Public_Class();
   
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="有货有信贷 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
      <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>

    <!--弹出菜单 end-->
    <style type="text/css">
        .zkw_title21 {
            margin-bottom: 10px;
        }
        .b07_info04 select
        {
         height:30px;
         font-size:14px;
        }
    </style>

    <script type="text/javascript">

        function checkordersprice(orders_price) {
            if ($("#select_price").val() == "+") {

            }
            else {
                if (orders_price < $("#Orders_Total_PriceDiscount").val()) {
                    $("#Orders_Total_PriceDiscount").val(orders_price)
                }
                else if (orders_price == $("#Orders_Total_PriceDiscount").val()) {
                    $("#Orders_Total_PriceDiscount").val(1)
                }
            }
        }

        function checkordersfee(orders_freight) {
            if ($("#select_freight").val() == "+") {

            }
            else {
                if (orders_freight < $("#Orders_Total_FreightDiscount").val()) {
                    $("#Orders_Total_FreightDiscount").val(orders_freight)
                }
            }
        }

        function setSelectPrice() {
            $("#Orders_Total_PriceDiscount").val(0);
        }

        function setSelectFee() {
            $("#Orders_Total_FreightDiscount").val(0);
        }

    </script>


</head>
<body>
    <uctop:top ID="top1" runat="server" />

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="50"></td>
        </tr>
        <tr>
            <td align="center">
                <img src="/images/error_icon.png"></td>
        </tr>
        <tr>
            <td align="center">
                <h1>哎呀…您访问的页面不存在</h1>
            </td>
        </tr>
        <tr>
            <td align="center">您可能输入了错误的网址，或者该网页已删除或移动</td>
        </tr>
        <tr>
            <td align="center"><a href="/index.aspx" style="font-size: 18px;">返回网站首页</a></td>
        </tr>
        <tr>
            <td height="50"></td>
        </tr>
    </table>

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
