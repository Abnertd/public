<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private ITools tools;
    private Orders myApp;
    private Member member;
    private Addr Addr;
    private OrdersLog ordersLog;
    private OrdersPayment orderspayment;
    private OrdersDelivery orderdelivery;
    private Supplier supplier;
    private Shop shop;
    private Contract contractclass;
    private string Orders_SN, Orders_Fail_Note, Orders_Total_PriceDiscount_Note, Orders_Total_FreightDiscount_Note, Orders_Address_Country, Orders_Address_State, Orders_Address_City, Orders_Address_County, Orders_Address_StreetAddress, Orders_Address_Zip, Orders_Address_Name, Orders_Address_Phone_Countrycode, Orders_Address_Phone_Areacode, Orders_Address_Phone_Number, Orders_Address_Mobile, Orders_Delivery_Name, Orders_Payway_Name, Orders_Note, Orders_Admin_Note, Orders_Site, Orders_Source, Orders_VerifyCode, Orders_DeliveryTime, Orders_PayType_Name;

    private int Orders_ID, Orders_BuyerID, Orders_SysUserID, Orders_Status, Orders_ERPSyncStatus, Orders_PaymentStatus, Orders_DeliveryStatus, Orders_InvoiceStatus, Orders_Fail_SysUserID, Orders_IsReturnCoin, Orders_Total_Coin, Orders_Total_UseCoin, Orders_Address_ID, Orders_Delivery, Orders_Payway, Orders_Admin_Sign, Orders_SourceType, Orders_DeliveryTime_ID, U_Orders_IsMonitor,Orders_Type;
    private DateTime Orders_PaymentStatus_Time, Orders_DeliveryStatus_Time, Orders_Fail_Addtime, Orders_Addtime;
    private double Orders_Total_MKTPrice, Orders_Total_Price, Orders_Total_Freight, Orders_Total_PriceDiscount, Orders_Total_FreightDiscount, Orders_Total_AllPrice;


    //会员信息定义
    int Member_ID, Member_Sex;
    string Member_Email, Member_NickName, Member_Name, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile;
    string Member_StreetAddress, Member_State, Member_City, Member_County, Member_Zip;

    int Orders_Payment_ID, Orders_Payment_OrdersID, Orders_Payment_PaymentStatus, Orders_Payment_SysUserID;
    string Orders_Payment_DocNo, Orders_Payment_Name, Orders_Payment_Note,Purchase_Title,PriceReport_Title;
    DateTime Orders_Payment_Addtime;

    int Orders_Delivery_ID, Orders_Delivery_DeliveryStatus, Orders_Delivery_SysUserID, Orders_PurchaseID, Orders_PriceReportID;
    string Orders_Delivery_DocNo, Orders_Delivery_Name1, Orders_Delivery_Note;
    string Supplier_Name, Shop_Name, Supplier_City, Supplier_State, Supplier_County, Supplier_Address, Supplier_Phone, Supplier_Fax, Supplier_Zip, Supplier_Contactman, Supplier_Mobile, Orders_Delivery_companyName;
    int Shop_Type, Supplier_ID,  Orders_HasSubset;
    DateTime Orders_Delivery_Addtime;
    string Invoice_Type, Invoice_Title, Contract_Sn,Supplier_Contact;
    int Contract_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("5e807815-409c-4d01-8e1a-2f835fbf2ac5");

        tools = ToolsFactory.CreateTools();
        myApp = new Orders();
        member = new Member();
        Addr = new Addr();
        ordersLog = new OrdersLog();
        orderspayment = new OrdersPayment();
        orderdelivery = new OrdersDelivery();
        supplier=new Supplier();
        shop = new Shop();
        contractclass = new Contract();
        Orders_ID = tools.CheckInt(Request.QueryString["Orders_ID"]);
        OrdersInfo entity = myApp.GetOrdersByID(Orders_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Orders_ID = entity.Orders_ID;
            Orders_SN = entity.Orders_SN;
            Orders_Type = entity.Orders_Type;
            Contract_ID = entity.Orders_ContractID;
            Orders_BuyerID = entity.Orders_BuyerID;
            Orders_SysUserID = entity.Orders_SysUserID;
            Orders_Status = entity.Orders_Status;
            Orders_ERPSyncStatus = entity.Orders_ERPSyncStatus;
            Orders_PaymentStatus = entity.Orders_PaymentStatus;
            Orders_PaymentStatus_Time = entity.Orders_PaymentStatus_Time;
            Orders_DeliveryStatus = entity.Orders_DeliveryStatus;
            Orders_DeliveryStatus_Time = entity.Orders_DeliveryStatus_Time;
            Orders_InvoiceStatus = entity.Orders_InvoiceStatus;
            Orders_Fail_SysUserID = entity.Orders_Fail_SysUserID;
            Orders_Fail_Note = entity.Orders_Fail_Note;
            Orders_Fail_Addtime = entity.Orders_Fail_Addtime;
            Orders_IsReturnCoin = entity.Orders_IsReturnCoin;
            Orders_Total_MKTPrice = entity.Orders_Total_MKTPrice;
            Orders_Total_Price = entity.Orders_Total_Price;
            Orders_Total_Freight = entity.Orders_Total_Freight;
            Orders_Total_Coin = entity.Orders_Total_Coin;
            Orders_Total_UseCoin = entity.Orders_Total_UseCoin;
            Orders_Total_PriceDiscount = entity.Orders_Total_PriceDiscount;
            Orders_Total_FreightDiscount = entity.Orders_Total_FreightDiscount;
            Orders_Total_PriceDiscount_Note = entity.Orders_Total_PriceDiscount_Note;
            Orders_Total_FreightDiscount_Note = entity.Orders_Total_FreightDiscount_Note;
            Orders_Total_AllPrice = entity.Orders_Total_AllPrice;
            Orders_Address_ID = entity.Orders_Address_ID;
            Orders_Address_Country = entity.Orders_Address_Country;
            Orders_Address_State = entity.Orders_Address_State;
            Orders_Address_City = entity.Orders_Address_City;
            Orders_Address_County = entity.Orders_Address_County;
            Orders_Address_StreetAddress = entity.Orders_Address_StreetAddress;
            Orders_Address_Zip = entity.Orders_Address_Zip;
            Orders_Address_Name = entity.Orders_Address_Name;
            Orders_Address_Phone_Countrycode = entity.Orders_Address_Phone_Countrycode;
            Orders_Address_Phone_Areacode = entity.Orders_Address_Phone_Areacode;
            Orders_Address_Phone_Number = entity.Orders_Address_Phone_Number;
            Orders_Address_Mobile = entity.Orders_Address_Mobile;
            Orders_DeliveryTime_ID = entity.Orders_Delivery_Time_ID;
            Orders_Delivery = entity.Orders_Delivery;
            Orders_Delivery_Name = entity.Orders_Delivery_Name;
            Orders_Payway = entity.Orders_Payway;
            Orders_Payway_Name = entity.Orders_Payway_Name;
            Orders_PayType_Name = entity.Orders_PayType_Name;
            Orders_Note = entity.Orders_Note;
            Orders_Admin_Note = entity.Orders_Admin_Note;
            Orders_Admin_Sign = entity.Orders_Admin_Sign;
            Orders_Site = entity.Orders_Site;
            Orders_SourceType = entity.Orders_SourceType;
            Orders_Source = entity.Orders_Source;
            Orders_VerifyCode = entity.Orders_VerifyCode;
            U_Orders_IsMonitor = entity.U_Orders_IsMonitor;
            Orders_Addtime = entity.Orders_Addtime;
            Supplier_ID = entity.Orders_SupplierID;
            Orders_PurchaseID=entity.Orders_PurchaseID;
            Orders_PriceReportID=entity.Orders_PriceReportID;
        }
        entity = null;

        //Supplier_ID=0;
        //IList<OrdersGoodsInfo> goodsinfos=myApp.GetGoodsListByOrderID(Orders_ID);
        //if(goodsinfos!=null)
        //{
        //    foreach(OrdersGoodsInfo goodsinfo in goodsinfos)
        //    {
        //        if(goodsinfo.Orders_Goods_ParentID>0||(goodsinfo.Orders_Goods_ParentID==0&&goodsinfo.Orders_Goods_Type!=2))
        //        {
        //            Supplier_ID=goodsinfo.Orders_Goods_Product_SupplierID;
        //            break;
        //        }
        //    }
        //}

        if (Orders_Type > 0 && Orders_PurchaseID > 0 && Orders_PriceReportID > 0)
        {
            SupplierPurchaseInfo purchaseinfo = supplier.GetSupplierPurchaseByID(Orders_PurchaseID);
            if (purchaseinfo != null)
            {
                Purchase_Title = purchaseinfo.Purchase_Title;
            }
            SupplierPriceReportInfo pricereportinfo = supplier.GetSupplierPriceReportByID(Orders_PriceReportID);
            if (pricereportinfo != null)
            {
                PriceReport_Title = pricereportinfo.PriceReport_Title;
            }
        }
        else
        {
            Purchase_Title = "";
            PriceReport_Title = "";
        }
        SupplierInfo buyerinfo = new SupplierInfo();
        if (Orders_BuyerID>57)
        {
            buyerinfo = supplier.GetSupplierByID(Orders_BuyerID+1);

        }
        else
        {
                    buyerinfo = supplier.GetSupplierByID(Orders_BuyerID);

        }
        if (buyerinfo != null)
        {
            Member_ID = buyerinfo.Supplier_ID;
            Member_Email = buyerinfo.Supplier_Email;
            Member_NickName = buyerinfo.Supplier_Nickname;
            Member_Name = buyerinfo.Supplier_CompanyName;
            Member_Phone_Number = buyerinfo.Supplier_Phone;
            Member_Mobile = buyerinfo.Supplier_Mobile;
            Member_State = buyerinfo.Supplier_State;
            Member_City = buyerinfo.Supplier_City;
            Member_County = buyerinfo.Supplier_County;
            Member_Zip = buyerinfo.Supplier_Zip;
            Member_StreetAddress = buyerinfo.Supplier_Address;
            Supplier_Contact = buyerinfo.Supplier_Contactman;

        }
        buyerinfo = null;

        if(Supplier_ID>0)
        {
            SupplierInfo supplierinfo = supplier.GetSupplierByID(Supplier_ID);
            if (supplierinfo != null)
            {
                Supplier_Name=supplierinfo.Supplier_CompanyName;
                Supplier_City=supplierinfo.Supplier_City;
                Supplier_State=supplierinfo.Supplier_State;
                Supplier_County=supplierinfo.Supplier_County;
                Supplier_Address=supplierinfo.Supplier_Address;
                Supplier_Phone=supplierinfo.Supplier_Phone;
                Supplier_Fax=supplierinfo.Supplier_Fax;
                Supplier_Zip=supplierinfo.Supplier_Zip;
                Supplier_Contactman=supplierinfo.Supplier_Contactman;
                Supplier_Mobile=supplierinfo.Supplier_Mobile;
            }
        }
        Contract_Sn = "";
        if (Contract_ID > 0)
        {
            ContractInfo contractinfo = contractclass.GetContractByID(Contract_ID);
            if (contractinfo != null)
            {
                Contract_Sn = contractinfo.Contract_SN;
            }
        }

        Orders_Payment_DocNo = "--";
        Orders_Payment_PaymentStatus = 0;


        Orders_DeliveryTime = "";
        DeliveryTimeInfo deliverytime = myApp.GetDeliveryTimeByID(Orders_DeliveryTime_ID);
        if (deliverytime != null)
        {
            Orders_DeliveryTime = deliverytime.Delivery_Time_Name;
        }



    }

    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;
        myApp = null;
        member = null;
        Addr = null;
        ordersLog = null;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单查看/操作</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
    <link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

    <style type="text/css">
        .tablebg_green {
            background: #390;
        }

            .tablebg_green td {
                background: #FFF;
            }
    </style>
    <script type="text/javascript">
	function turnnewpage(url){
		location.href=url
	}
    </script>
</head>
<body>

    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">订单管理</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr class="orders_tdbg">
                            <td width="100"><span class="title">订单信息</span></td>
                            <td align="left"><%myApp.Order_Detail_Button(Orders_Status, Orders_PaymentStatus, Orders_Payway, Orders_DeliveryStatus, Orders_Delivery, Orders_ID, Orders_Addtime, Supplier_ID, Orders_HasSubset); %>
                &nbsp;&nbsp;打印 : 
                <% if (Public.CheckPrivilege("c7d71545-1f4e-4f61-8055-1cd24ef4a596"))
                   {%>
                                <input name="btn_print" type="button" class="btn_01" id="btn_print" value="购物清单" onclick="window.open('orders_print.aspx?orders_id=<% =Orders_ID %>')" />
                                <input name="btn_print" type="button" class="btn_01" id="Button1" value="配货单" onclick="window.open('orders_freight_print.aspx?orders_id=<% =Orders_ID %>')" />
                                <input name="btn_print" type="button" class="btn_01" id="Button2" value="联合打印" onclick="window.open('orders_print_all.aspx?orders_id=<% =Orders_ID %>')" />
                                <%} %>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="1" class="tablebg_green">
                                                <tr>
                                                    <td width="70" align="right" class="t12_green">订单号</td>
                                                    <td class="t12_red"><%=Orders_SN%></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="t12_green">订单状态</td>
                                                    <td><%=myApp.OrdersStatus(Orders_Status)%></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="t12_green">下单时间</td>
                                                    <td><%=Orders_Addtime%>
                     
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>



                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="orders_tdbg">
                            <td><span class="title">订单基本信息</span></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td width="100" align="right"><strong>订单号</strong></td>
                                        <td><%=Orders_SN %> 【<%=myApp.OrdersType(Orders_Type)%>】</td>
                                        <td align="right"><strong>所属合同</strong></td>
                                        <td>
                                            <% 
                          if (Contract_ID > 0)
                          {
                              Response.Write("<a href=\"/contract/contract_detail.aspx?contract_id=" + Contract_ID + "\" target=\"_blank\">" + Contract_Sn + "</a>");
                          }
                          else
                          {
                              if (Orders_Status == 1)
                              {
                                  if (Supplier_ID == 0)
                                  {
                                      Response.Write("<form action=\"/contract/contract_do.aspx\" method=\"post\">");
                                      Response.Write(contractclass.TmpContract_Select(Orders_Type) + " <input name=\"btn_delivery\" type=\"submit\" class=\"btn_01\" id=\"btn_delivery\" value=\"添加订单至选择合同\"> <input name=\"btn_delivery\" type=\"button\" class=\"btn_01\" id=\"btn_delivery\" value=\"添加订单至新意向合同\" onclick=\"location='/contract/contract_add.aspx?orders_sn=" + Orders_SN + "';\">");
                                      Response.Write("<input type=\"hidden\" value=\"" + Orders_SN + "\" name=\"orders_sn\" /><input type=\"hidden\" name=\"action\" value=\"order_contract\" /></form>");
                                  }
                              }
                              else
                              {
                                  Response.Write("--");
                              }
                          }
                                            %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>支付条件</strong></td>
                                        <td><%=Orders_PayType_Name %>
                    &nbsp;&nbsp;
                   
                                        </td>
                                        <td align="right"><strong>支付方式</strong></td>
                                        <td><%=Orders_Payway_Name%>
                 &nbsp;&nbsp;
                  
                  <% 
                      if (Public.CheckPrivilege("d0106a6a-c9ef-4a18-8eb9-f65f11be76c4"))
                      {
                          if (Orders_PaymentStatus == 0 && Orders_Status == 0) { Response.Write("<input name=\"btn_payway\" type=\"button\" class=\"btn_01\" value=\"编辑\" onclick=\"location.href='orders_payway.aspx?orders_id=" + Orders_ID + "'\" />"); }
                      }
                  %>
                                        </td>
                                    </tr>
                                    <%
                    if (Orders_Type > 1)
                    {
                        Response.Write("<tr>");
                        Response.Write("<td width=\"100\" align=\"right\"><strong>采购申请</strong></td>");
                        Response.Write("<td><a href=\"/supplier/Supplier_Purchase_view.aspx?Purchase_ID="+Orders_PurchaseID+"\" target=\"_blank\">" + Purchase_Title + "</a></td>");
                        Response.Write("<td align=\"right\"><strong>采购报价</strong></td>");
                        Response.Write("<td><a href=\"/supplier/Supplier_pricereport_view.aspx?PriceReport_ID="+Orders_PriceReportID+"\" target=\"_blank\">" + PriceReport_Title + "</td>");
                        Response.Write("</tr>");
                    }
                                    %>
                                    <tr>
                                        <td align="right" class="t12_red"><strong>订单备注</strong></td>
                                        <td colspan="3"><% =Orders_Note%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="orders_tdbg">
                            <td><span class="title">订购人信息</span>
                                <%
               if (Orders_BuyerID > 0)
               {
                   Response.Write("<input name=\"btn_address\" type=\"button\" class=\"btn_01\" id=\"btn_address\" value=\"查看\" onclick=\"location.href='/supplier/supplier_view.aspx?Supplier_id=" + Orders_BuyerID + "'\" />");
               } %></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td width="100" align="right"><strong>昵称</strong></td>
                                        <td><%=Member_NickName%></td>
                                        <td align="right"><strong>公司名称</strong></td>
                                        <td><%=Member_Name%></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>固定电话</strong></td>
                                        <td><%=Member_Phone_Areacode +"-"+ Member_Phone_Number%></td>
                                        <td align="right"><strong>联系人</strong></td>
                                        <td><%=Supplier_Contact%></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>手机</strong></td>
                                        <td><%=Member_Mobile%></td>
                                        <td align="right"><strong>Email</strong></td>
                                        <td><%=Member_Email%></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>地址</strong></td>
                                        <td colspan="3"><%=Addr.DisplayAddress(Member_State, Member_City, Member_County) +" "+ Member_StreetAddress %> 邮编 <%=Member_Zip%></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <%if (Supplier_ID > 0) { %>
                        <tr class="orders_tdbg">
                            <td><span class="title">供应商信息</span>
                                <%
            if (Supplier_ID > 0)
            {
                Response.Write("<input name=\"btn_address\" type=\"button\" class=\"btn_01\" id=\"btn_address\" value=\"查看\" onclick=\"location.href='/supplier/supplier_view.aspx?Supplier_id=" + Supplier_ID + "'\" />");
            } %></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td width="100" align="right"><strong>供应商名称</strong></td>
                                        <td><%=Supplier_Name%></td>
                                        <td align="right"><strong>店铺名称</strong></td>
                                        <td><%=Shop_Name%></td>
                                    </tr>
                                    <tr>

                                        <td align="right"><strong>联系人</strong></td>
                                        <td><%=Supplier_Contactman%></td>
                                        <td align="right"><strong>手机</strong></td>
                                        <td><%=Supplier_Mobile%></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>联系电话</strong></td>
                                        <td><%=Supplier_Phone%></td>
                                        <td align="right"><strong>传真</strong></td>
                                        <td><%=Supplier_Fax%></td>
                                    </tr>
                                    <tr>

                                        <td align="right"><strong>邮编</strong></td>
                                        <td><%=Supplier_Zip%></td>
                                        <td align="right"><strong>地址</strong></td>
                                        <td><%=Addr.DisplayAddress(Supplier_State, Supplier_City, Supplier_County) + " " + Supplier_Address%> </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <%} %>
                        <tr class="orders_tdbg">
                            <td><span class="title">收货人信息</span> <% 
                                                    if (Public.CheckPrivilege("32c0d059-33bb-435c-b09e-ad37378cdd1e"))
                                                    {
                                                        if (Orders_DeliveryStatus == 0 && Orders_Status == 0 && Orders_PaymentStatus==0) { Response.Write("<input name=\"btn_address\" type=\"button\" class=\"btn_01\" id=\"btn_address\" value=\"编辑\" onclick=\"location.href='orders_address.aspx?orders_id=" + Orders_ID + "'\" />"); }
                                                    }
                            %></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td width="100" align="right"><strong>真实姓名</strong></td>
                                        <td><%=Orders_Address_Name%></td>
                                        <td width="100" align="right"><strong>固定电话</strong></td>
                                        <td><%=Orders_Address_Phone_Number %></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><strong>手机</strong></td>
                                        <td><%=Orders_Address_Mobile %></td>
                                        <td align="right"><strong>地址</strong></td>
                                        <td><%=Addr.DisplayAddress(Orders_Address_State, Orders_Address_City, Orders_Address_County) + " " + Orders_Address_StreetAddress%> 邮编 <% =Orders_Address_Zip %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr class="orders_tdbg">
                            <td><span class="title">商品清单</span></td>
                        </tr>
                        <tr>
                            <td>
                                <div style="margin: 5px 0px;"><%=myApp.GetOrdersGoodsByOrdersID(Orders_ID)%></div>
                            </td>
                        </tr>
                        <%--<tr class="orders_tdbg">
           <td><span class="title">订单优惠</span></td>
        </tr>
        <tr>
           <td style="padding-left:10px;">
           <div style="padding:5px;">价格优惠：<span class="price_list"><%=Public.DisplayCurrency(Orders_Total_PriceDiscount)%></span></div>
           <div style="padding:5px;">优惠备注：<%=Orders_Total_PriceDiscount_Note%></div>
           <div style="padding:5px;">运费优惠：<span class="price_list"><%=Public.DisplayCurrency(Orders_Total_FreightDiscount)%></span></div>
           <div style="padding:5px;">优惠备注：<%=Orders_Total_FreightDiscount_Note%></div>
           </td>
        </tr>--%>
                        <%if(Orders_Status==3)
          { %>
                        <tr class="orders_tdbg">
                            <td><span class="title">订单关闭备注</span></td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                <div style="padding: 5px;">关闭原因：<span><%=Orders_Fail_Note%></span></div>
                                <div style="padding: 5px;">关闭时间：<%=Orders_Fail_Addtime%></div>
                            </td>
                        </tr>
                        <%} %>
                        <tr class="orders_tdbg">
                            <td><span class="title">管理员订单备注</span></td>
                        </tr>
                        <tr>
                            <td>
                                <form id="frm_ordersign" name="frm_ordersign" method="post" action="orders_do.aspx?orders_id=<%=Orders_ID%>">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td width="100" align="right">标记</td>
                                            <td>
                                                <input name="Orders_Admin_Sign" type="radio" id="admin_sign_0" value="0" />
                                                <img src="/images/icon_star_off.gif" alt="标记默认" style="vertical-align: middle;" />

                                                <input name="Orders_Admin_Sign" type="radio" id="admin_sign_1" value="1" />
                                                <img src="/images/icon_star.gif" alt="标记为星" style="vertical-align: middle;" />

                                                <input type="radio" name="Orders_Admin_Sign" id="admin_sign_2" value="2" />
                                                <img src="/images/icon_clock.gif" alt="处理时间过长" style="vertical-align: middle;" />

                                                <input type="radio" name="Orders_Admin_Sign" id="admin_sign_3" value="3" />
                                                <img src="/images/icon_alert.gif" alt="提示" style="vertical-align: middle;" />

                                                <input type="radio" name="Orders_Admin_Sign" id="admin_sign_4" value="4" />
                                                <img src="/images/icon_fail.gif" alt="标记失败" style="vertical-align: middle;" />

                                                <input type="radio" name="Orders_Admin_Sign" id="admin_sign_5" value="5" />
                                                <img src="/images/icon_success.gif" alt="标记成功" style="vertical-align: middle;" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right">备注</td>
                                            <td>
                                                <textarea name="Orders_Admin_Note" id="Orders_Admin_Note" cols="45" rows="5"><%=Orders_Admin_Note%></textarea></td>
                                        </tr>
                                        <% if (Public.CheckPrivilege("c4de02f2-c819-4ae3-9a6e-be8645b5271f")) { %>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <input name="button" type="submit" class="bt_orange" id="button" value="保存" />
                                                <input name="action" type="hidden" id="action" value="order_admin_note" /></td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </form>
                            </td>
                        </tr>
                        <tr class="orders_tdbg">
                            <td><span class="title">订单日志</span></td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding: 5px;"><%=ordersLog.GetOrdersLogsByOrdersID(Orders_ID)%></div>
                            </td>
                        </tr>



                    </table>

                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
    $(document).ready(function(){
       $("#admin_sign_<%=Orders_Admin_Sign%>").attr({ checked: "checked" });
           });

           function displaySubGoods(id) {
               $("tr[id^=subgoods_" + id + "_]").toggle();
               if ($("tr[id^=subgoods_" + id + "_]").css("display") == "none") {
                   $("tr[id^=subgoods_" + id + "_]").attr("src", "/images/display_close.gif");
               }
               else {
                   $("tr[id^=subgoods_" + id + "_]").attr("src", "/images/display_open.gif");
               }
           }
    </script>

</body>
</html>
