<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Orders myApp;
    private Member member;
    private OrdersProcess uorder;
    private Addr Addr;
    private OrdersLog ordersLog;
    private OrdersPayment orderspayment;
    private OrdersDelivery orderdelivery;

    private string Orders_SN, Orders_Fail_Note, Orders_Total_PriceDiscount_Note, Orders_Total_FreightDiscount_Note, Orders_Address_Country, Orders_Address_State, Orders_Address_City, Orders_Address_County, Orders_Address_StreetAddress, Orders_Address_Zip, Orders_Address_Name, Orders_Address_Phone_Countrycode, Orders_Address_Phone_Areacode, Orders_Address_Phone_Number, Orders_Address_Mobile, Orders_Delivery_Name, Orders_Payway_Name, Orders_Note, Orders_Admin_Note, Orders_Site, Orders_Source, Orders_VerifyCode, Orders_DeliveryTime;

    private int Orders_ID, Orders_BuyerID, Orders_SysUserID, Orders_Status, Orders_ERPSyncStatus, Orders_PaymentStatus, Orders_DeliveryStatus, Orders_InvoiceStatus, Orders_Fail_SysUserID, Orders_IsReturnCoin, Orders_Total_Coin, Orders_Total_UseCoin, Orders_Address_ID, Orders_Delivery, Orders_Payway, Orders_Admin_Sign, Orders_SourceType, Orders_DeliveryTime_ID, U_Orders_IsMonitor;
    private DateTime Orders_PaymentStatus_Time, Orders_DeliveryStatus_Time, Orders_Fail_Addtime, Orders_Addtime;
    private double Orders_Total_MKTPrice, Orders_Total_Price, Orders_Total_Freight, Orders_Total_PriceDiscount, Orders_Total_FreightDiscount, Orders_Total_AllPrice;


    //会员信息定义
    int Member_ID, Member_Sex;
    string Member_Email, Member_NickName, Member_Name, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile;
    string Member_StreetAddress, Member_State, Member_City, Member_County, Member_Zip;


    string Invoice_Type, Invoice_Title;




    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c7f19325-8f6e-419a-a276-a6a4dee0517d");
        //Public.CheckLogin("all");

        tools = ToolsFactory.CreateTools();
        myApp = new Orders();
        member = new Member();
        uorder = new OrdersProcess();
        Addr = new Addr();
        ordersLog = new OrdersLog();
        orderspayment = new OrdersPayment();
        orderdelivery = new OrdersDelivery();

        Orders_ID = tools.CheckInt(Request.QueryString["Orders_ID"]);
        OrdersInfo entity = myApp.GetOrdersByID(Orders_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
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
            Orders_Note = entity.Orders_Note;
            Orders_Admin_Note = entity.Orders_Admin_Note;
            Orders_Admin_Sign = entity.Orders_Admin_Sign;
            Orders_Site = entity.Orders_Site;
            Orders_SourceType = entity.Orders_SourceType;
            Orders_Source = entity.Orders_Source;
            Orders_VerifyCode = entity.Orders_VerifyCode;
            U_Orders_IsMonitor = entity.U_Orders_IsMonitor;
            Orders_Addtime = entity.Orders_Addtime;

        }
        entity = null;

        if (Orders_Status >= 1 || Orders_PaymentStatus != 0 || Orders_DeliveryStatus != 0)
        {
            Public.Msg("error", "错误提示", "该订单无法编辑", false, "{back}");
            Response.End();
        }

        MemberInfo memberInfo = member.GetMemberByID(Orders_BuyerID);
        if (memberInfo != null)
        {
            Member_ID = memberInfo.Member_ID;
            Member_Email = memberInfo.Member_Email;
            Member_NickName = memberInfo.Member_NickName;

            MemberProfileInfo ProfileInfo = memberInfo.MemberProfileInfo;
            if (ProfileInfo != null)
            {
                Member_Sex = ProfileInfo.Member_Sex;
                Member_Name = ProfileInfo.Member_Name;
                Member_Phone_Areacode = ProfileInfo.Member_Phone_Areacode;
                Member_Phone_Number = ProfileInfo.Member_Phone_Number;
                Member_Mobile = ProfileInfo.Member_Mobile;
                Member_StreetAddress = ProfileInfo.Member_StreetAddress;
                Member_State = ProfileInfo.Member_State;
                Member_City = ProfileInfo.Member_City;
                Member_County = ProfileInfo.Member_County;
                Member_Zip = ProfileInfo.Member_Zip;
            }
            ProfileInfo = null;
        }
        memberInfo = null;


        Orders_DeliveryTime = "";




        uorder.SetOrdersGoods_To_GoodsTmp(Orders_ID);

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
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
    function updateorderprice()
    {
	    
        MM_findObj('Orders_Total_AllPrice').value=parseFloat(MM_findObj('Orders_Total_Price').value)+parseFloat(MM_findObj('Orders_Total_Freight').value)-parseFloat(MM_findObj('Orders_Total_PriceDiscount').value)-parseFloat(MM_findObj('Orders_Total_FreightDiscount').value);

    }
</script>
</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">订单编辑</td>
    </tr>
    <tr>
      <td class="content_content">
      
      <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr class="orders_tdbg">
           <td><span class="title">商品信息</span></td>
        </tr>
        <tr><td>
        <form name="frm_batch" action="orders_do.aspx" method=\"post\">
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr><td height="5"></td></tr>
        <tr>
           <td id="goods_tmpinfo">
           <%=uorder.Orders_Edit_Goods(Orders_ID, Orders_BuyerID,false)%>
           </td>
             </tr>
             
           </table>
           </form>
           </td>
        </tr>
        <tr><td height="5"></td></tr>
        <tr class="orders_tdbg">
           <td><span class="title">订单基本信息</span></td>
        </tr>
        <tr><td height="5"></td></tr>
        <tr>
           <td>
            <form id="frm_order" name="frm_order" method="post" action="orders_do.aspx?orders_id=<%=Orders_ID%>">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			
			<tr>
				<td width="100" height="23" align="right" class="cell_title">订单号</td>
				<td align="left" class="cell_content"><%=Orders_SN%></td>
				<td width="100" height="23" align="right" class="cell_title">下单时间</td>
				<td align="left" class="cell_content"><%=Orders_Addtime %></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">商品金额</td>
				<td align="left" class="cell_content"><input type="text" readonly id="Orders_Total_Price" name="Orders_Total_Price" value="<%=Orders_Total_Price.ToString("0.00")%>" /></td>
				<td width="100" height="23" align="right" class="cell_title" style="display:none">配送方式</td>
				<td align="left" class="cell_content" style="display:none"><%=uorder.Delivery_Way_Select1(Orders_ID,Orders_Delivery, Orders_Address_State, Orders_Address_City, Orders_Address_County)%></td>

                <td width="100" height="23" align="right" class="cell_title">订单价格优惠</td>
				<td align="left" class="cell_content"><input type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="Orders_Total_PriceDiscount" name="Orders_Total_PriceDiscount" onchange="updateorderprice();" value="<%=Orders_Total_PriceDiscount %>" /></td>
			  </tr>
			  <tr style="display:none;">
				<td width="100" height="23" align="right" class="cell_title">运费</td>
				<td align="left" class="cell_content" id="fee_td"><input type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="Orders_Total_Freight" name="Orders_Total_Freight" value="<%=Orders_Total_Freight %>" onchange="updateorderprice();" /></td>
				<td width="100" height="23" align="right" class="cell_title">支付方式</td>
				<td align="left" class="cell_content"><%=uorder.Pay_Way_Select1(Orders_Payway, 1)%></td>
			  </tr>
			  <tr style="display:none;">
				
				<td width="100" height="23" align="right" class="cell_title">订单运费优惠</td>
				<td align="left" class="cell_content"><input type="text" id="Orders_Total_FreightDiscount" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" name="Orders_Total_FreightDiscount" onchange="updateorderprice();" value="<%=Orders_Total_FreightDiscount %>" /></td>
			  </tr>
			  
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">订单总金额</td>
				<td align="left" colspan="3" class="cell_content"><input type="text" readonly id="Orders_Total_AllPrice" name="Orders_Total_AllPrice" value="<%=Orders_Total_AllPrice.ToString("0.00") %>" /></td>
				
			  </tr>
			</table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right"><span class="t12_red">运费及运费优惠按照商品数量核对</span> 
            <input name="button" type="submit" class="bt_orange" id="Submit1" value="保存" />
				<input name="action" type="hidden" id="action" value="save_order" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='/orders/orders_view.aspx?orders_id=<%=Orders_ID %>    ';"/></td>
          </tr>
        </table>
			</form>
           
      </td>
    </tr>
  </table>
          </td>
        </tr>
          </table>
</div>

<script type="text/javascript">
    $(document).ready(function(){
        $("#admin_sign_<%=Orders_Admin_Sign%>").attr({ checked:"checked"});
    });
    
    function displaySubGoods(id){
        $("#subgoods_"+ id).toggle();
        if ($("#subgoods_"+ id).css("display") == "none"){ 
            $("#subicon_"+ id).attr("src", "/images/display_close.gif");
        }
        else{
            $("#subicon_"+ id).attr("src", "/images/display_open.gif");
        }
    }
</script>

</body>
</html>