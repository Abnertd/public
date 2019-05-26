<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>


<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Supplier_MerchantsList.aspx");
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="招商加盟管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />


     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script src="/scripts/layer/layer.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
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
</head>
<body>

    <uctop:top ID="top1" runat="server" />



    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02" style="background-color: #FFF;">
       
            <!--位置说明 开始-->
         <div class="position">当前位置 >  <a href="/supplier/">我是卖家 > </a> <strong>招商加盟管理</strong></div>
            <!--位置说明 结束-->
            <div class="partc">
                <div class="pd_left">
                   <%-- <div class="blk12">--%>
                        <% supplier.Get_Supplier_Left_HTML(2, 8); %>
                   <%-- </div>--%>
                </div>
                <div class="pd_right">
<div class="blk14_1" style="margin-top:0px;">
                    
  <h2><span><a href="/supplier/supplier_merchants_add.aspx" class="a13" style="float:right;margin-right:15px;color:#ffffff;font-weight:normal;font-size:12px;">添加</a></span>招商加盟管理</h2>
                    <div class="b14_1_main" style=" margin-top:15px;">                        
                        <%supplier.Supplier_Merchants_List();%>
                    </div>



                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
   
        </div>
    <!--主体 结束-->

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
