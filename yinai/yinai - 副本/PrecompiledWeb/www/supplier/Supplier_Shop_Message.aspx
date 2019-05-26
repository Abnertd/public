<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    Supplier supplier = new Supplier();

    supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Message.aspx");
    
    string action = tools.NullStr(Request.QueryString["action"]);
    string date_start, date_end;
    if (action == "product")
    {
        Session["message_guide"] = "product";
    }
    else
    {
        action = "shop";
        Session["message_guide"] = "shop";
    }

    double Supplier_Account;
    Supplier_Account = 0;
    SupplierInfo supplierinfo = supplier.GetSupplierByID();
    if (supplierinfo != null)
    {
        Supplier_Account = supplierinfo.Supplier_Security_Account;
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="客服直通车 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <style type="text/css">
        .zkw_blk19_main img { display:inline; }
    </style>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    

    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		  您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_Shop_Message.aspx">客服直通车</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                   <% supplier.Get_Supplier_Left_HTML(2, 10); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>客服直通车</h2>
                        <div class="main">
                                        
                        <div class="zkw_order">
                            <h3 class="zkw_title21">
                                <%
                                    StringBuilder strHTML = new StringBuilder();
                                    strHTML.Append("<ul class=\"zkw_lst31\">");

                                    if (tools.NullStr(Session["message_guide"]) == "shop")
                                    {
                                        strHTML.Append("	<li class=\"on\"><a href=\"/supplier/Supplier_Shop_Message.aspx\">店铺咨询</a></li>");
                                    }
                                    else
                                    {
                                        strHTML.Append("	<li><a href=\"/supplier/Supplier_Shop_Message.aspx\">店铺咨询</a></li>");
                                    }
                                    if (tools.NullStr(Session["message_guide"]) == "product")
                                    {
                                        strHTML.Append("	<li class=\"on\" ><a href=\"/supplier/Supplier_Shop_Message.aspx?action=product\">商品咨询</a></li>");
                                    }
                                    else
                                    {
                                        strHTML.Append("	<li><a href=\"/supplier/Supplier_Shop_Message.aspx?action=product\">商品咨询</a></li>");
                                    }
                                    strHTML.Append("</ul><div class=\"clear\"></div>");
                                    Response.Write(strHTML.ToString());
                                %>
                            </h3>
                            
                            <%supplier.Shop_Ask_List(action);%>
                            
                        </div>
                        
                        </div>
                  </div>
            </div>
            <div class="clear"></div>
      </div> 
</div>
<!--主体 结束-->
 
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
