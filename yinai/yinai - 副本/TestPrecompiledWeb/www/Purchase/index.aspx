<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%    Public_Class pub = new Public_Class();
      ITools tools = ToolsFactory.CreateTools();
      Product product = new Product();
      CMS cms = new CMS(); 
      AD ad = new AD();
      Session["Position"] = "purchase";
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%= pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
   
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            <i>共找到<strong><%=product.GetSupplierPurchasesCount()%></strong>条 </i>当前位置 > <a href="/" target="_blank">
                首页</a> > <span>采购信息</span></div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partc">
            <div class="pc_left">
                <!--产品分类 开始-->
                <div class="blk02"> 
                     <%=product.GetRelateCategory(0, 0)%>
                </div>
                <!--产品分类 结束-->
            </div>
            <div class="pc_right">
                <%product.SupplierPurchase_list(); %>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
