<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Orders myApp;
    private Member member;
    private Addr Addr;
    private OrdersLog ordersLog;
    
    private string Orders_SN, Orders_Fail_Note, Orders_Total_PriceDiscount_Note, Orders_Total_FreightDiscount_Note, Orders_Address_Country, Orders_Address_State, Orders_Address_City, Orders_Address_County, Orders_Address_StreetAddress, Orders_Address_Zip, Orders_Address_Name, Orders_Address_Phone_Countrycode, Orders_Address_Phone_Areacode, Orders_Address_Phone_Number, Orders_Address_Mobile, Orders_Delivery_Name, Orders_Payway_Name, Orders_Note, Orders_Admin_Note, Orders_Site, Orders_Source, Orders_VerifyCode,Orders_DeliveryTime;

    private int Orders_ID, Orders_BuyerID, Orders_SysUserID, Orders_Status, Orders_ERPSyncStatus, Orders_PaymentStatus, Orders_DeliveryStatus, Orders_InvoiceStatus, Orders_Fail_SysUserID, Orders_IsReturnCoin, Orders_Total_Coin, Orders_Total_UseCoin, Orders_Address_ID, Orders_Delivery, Orders_Payway, Orders_Admin_Sign, Orders_SourceType, Orders_DeliveryTime_ID, Orders_Paytype;
    private DateTime Orders_PaymentStatus_Time, Orders_DeliveryStatus_Time, Orders_Fail_Addtime, Orders_Addtime;
    private double Orders_Total_MKTPrice, Orders_Total_Price, Orders_Total_Freight, Orders_Total_PriceDiscount, Orders_Total_FreightDiscount, Orders_Total_AllPrice;

    
    //会员信息定义
    int Member_ID, Member_Sex;
    string Member_Email, Member_NickName, Member_Name, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile;
    string Member_StreetAddress, Member_State, Member_City, Member_County, Member_Zip;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d0106a6a-c9ef-4a18-8eb9-f65f11be76c4");

        tools = ToolsFactory.CreateTools();
        myApp = new Orders();
        Addr = new Addr();
        ordersLog = new OrdersLog();
        
        Orders_ID = tools.CheckInt(Request.QueryString["Orders_ID"]);
        OrdersInfo entity = myApp.GetOrdersByID(Orders_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Orders_ID = entity.Orders_ID;
            Orders_SN = entity.Orders_SN;
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
            Orders_Paytype = entity.Orders_PayType;
            Orders_Note = entity.Orders_Note;
            Orders_Admin_Note = entity.Orders_Admin_Note;
            Orders_Admin_Sign = entity.Orders_Admin_Sign;
            Orders_Site = entity.Orders_Site;
            Orders_SourceType = entity.Orders_SourceType;
            Orders_Source = entity.Orders_Source;
            Orders_VerifyCode = entity.Orders_VerifyCode;
            Orders_Addtime = entity.Orders_Addtime;
        }
        entity = null;

        
        
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
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
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
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">订单支付方式</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="Form1" name="frm_delivery" method="post" action="orders_do.aspx?orders_id=<%=Orders_ID%>">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">支付方式</td>
				<td align="left" class="cell_content"><%=myApp.Pay_Way_Select(Orders_Payway,1)%></td>
			  </tr>
              <tr>
				<td width="100" height="23" align="right" class="cell_title">支付条件</td>
				<td align="left" class="cell_content"><%=myApp.Pay_Type_Select(Orders_Paytype)%></td>
			  </tr>
			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input name="button" type="submit" class="bt_orange" id="Submit2" value="保存" />
				<input name="action" type="hidden" id="action" value="order_payway" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='/orders/orders_view.aspx?orders_id=<%=Orders_ID %>';"/></td>
          </tr>
        </table>
			</form>
      </td>
    </tr>
  </table>
</div>



</body>
</html>