<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    supplier.Supplier_Login_Check("/supplier/Supplier_product_count.aspx");
    if (tools.NullInt(Session["supplier_ishaveshop"]) == 0)
    {
        pub.Msg("error", "错误信息", "您还没有开通店铺", false, "/supplier/index.aspx");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="库存管理 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <style type="text/css">
        .yz_ph_right img { display:inline; }
    </style>
    
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
     <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
               您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_product_count.aspx">库存管理</a>
          </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                  <% supplier.Get_Supplier_Left_HTML(5, 11); %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>
                        库存管理</h2>
                    <div class="main">
                        <%supplier.Shop_Product_Stock();%>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
     
    
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
