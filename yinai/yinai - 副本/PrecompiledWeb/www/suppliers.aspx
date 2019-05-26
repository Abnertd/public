<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<%    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Product product = new Product();
    Product myProduct = new Product();

    Session["SubPosition"] = "Suppliers";
    
    Suppliers suppliers = new Suppliers();
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="供货商 - "+ pub.SEO_TITLE()%></title>
      <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/suppliers.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>

    <script  type="text/javascript">
        function switchTag(content) {

            if (document.getElementById(content).className == "hidecontent") {
                document.getElementById(content).className = "";
            }
            else { document.getElementById(content).className = "hidecontent"; }
        }
</script>
</head>
<body>
    <!--头部 开始-->
    <uctop:top ID="Top" runat="server" />
    <!--头部 结束-->

<!--主体 开始-->
<div class="content" style=" margin-top:0; background-color:#FFF;">
      <div class="content_main" style="background-color:#FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <strong>供货商</strong></div>
            <!--位置说明 结束-->
            <div class="partn">
                  <div class="pn_left">
                        <div class="blk28">
                        <h2>所有分类</h2>
                        <div class="b28_main">
                            <%=product.GetRelateCategory(1, 0)%>
                        </div>
                    </div>
                  </div>
                  <div class="pn_right">
                        <%=suppliers.SupplierShops() %>
                  </div>
                  
                  <div class="clear"></div>
            </div>
      </div>
</div>
<!--主体 结束-->


    <!--尾部 开始-->
    <ucbottom:bottom ID="Bottom" runat="server" />
    <!--尾部 结束-->

    

</body>
</html>
