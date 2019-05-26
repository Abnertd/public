<%@ Page Language="C#" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<% 

    Session["Position"] = "Logistics";
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Logistics MyLogistics = new Logistics();
    int ID = tools.CheckInt(Request["ID"]);
    Addr addr = new Addr();
    Supplier MySupplier = new Supplier();
    SupplierLogisticsInfo entity = MyLogistics.GetSupplierLogisticsByID(ID);
    if (entity != null)
    {
        if (entity.Supplier_Logistics_IsAudit != 1 || entity.Supplier_Status != 1)
        {
            Response.Redirect("/Logistics/index.aspx");
        }
    }
    else
    {
        Response.Redirect("/Logistics/index.aspx");
    }

%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="物流详情 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/1.js"></script>
    <!--弹出菜单 end-->
    <style>
        .b11_main_1 p {
            line-height: 30px;
        }

        td {
            font-size: 20px;
        }
    </style>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" />
    <!--主体 开始-->
    <div class="content">
        <!--位置 开始-->
        <div class="position">当前位置 > <a href="/">首页</a> > 物流详情</div>
        <!--位置 结束-->
        <div class="banner02">
            <img src="/images/img12.jpg" width="1200" height="156" /></div>
        <div class="parte" style="border: none;">
            <div class="blk10_1">
                <h2><%=entity.Supplier_Logistics_Name %></h2>
                <div style="text-align: center; padding-left:10%">
                    <table border="1" cellspacing="0" style="font-size: 20px; border: solid 1px; width: 85%; height: 300px; text-align: center">
                        <tr>
                            <td colspan="3">公司名称：<%=MySupplier.GetSupplierName(entity.Supplier_SupplierID) %></td>

                        </tr>
                        <tr>
                            <td>商品名称：<%=entity.Supplier_Logistics_Name %></td>
                            <td>发货数量：<%=entity.Supplier_Logistics_Number %>吨</td>
                            <td>到货日期：<%=entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") %></td>
                        </tr>
                        <tr>
                            <td>发货地址：<%=addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "&nbsp;" + entity.Supplier_Address_StreetAddress %></td>
                            <td colspan="2">收货地址：<%=addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "&nbsp;" + entity.Supplier_Orders_Address_StreetAddress %></td>
                        </tr>
                    </table>
                    <%--                  <p>数量：<%=entity.Supplier_Logistics_Number %></p>
                <p>到货日期：<%=entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") %></p>
                <p>发货地：<%=addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + "&nbsp;" + entity.Supplier_Address_StreetAddress %></p>
                <p>收货地：<%=addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + "&nbsp;" + entity.Supplier_Orders_Address_StreetAddress %></p>
            </div>--%>
                    <div style="position: initial; width:30%;padding-left:25%;padding-top:5px;" class="b10_1_main02">
                        <%if (entity.Supplier_LogisticsID == 0)
                            {%>
                        <a href="/Logistics/Logistics_Tender_Add.aspx?LogisticsID=<%=entity.Supplier_Logistics_ID %>">立即报价</a>
                        <% }
                        else
                        {%>
                        <a>已结束</a>
                        <%} %>
                    </div>
                </div>

                <%--              <div class="blk11_1">
                    <h2>商品清单</h2>
                    <h3>
                        <ul>
                            <li style="width:220px; text-align:left;">商品名称</li>
                            <li style="width:140px;">数量</li>
                            <li style="width:100px;">缩略图</li>
                            <li style="width:617px; border-right:none; text-align:left;">产品规格</li>
                        </ul>
                        <div class="clear"></div>
                    </h3>
                    <%=MySupplier.Order_Logistics_Goods(entity.Supplier_OrdersID,0) %>
        
              </div>--%>


                <div class="clear"></div>
            </div>

        </div>
        <!--主体 结束-->
        <!--尾部 开始-->
        <ucbottom:bottom runat="server" ID="Bottom" />
        <!--右侧滚动 结束-->
</body>
</html>

