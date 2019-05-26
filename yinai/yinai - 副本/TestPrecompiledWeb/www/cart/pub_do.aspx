<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Cart cart = new Cart();
        Public_Class pub = new Public_Class();
        ITools tools;
        tools = ToolsFactory.CreateTools();

        int address_id = tools.CheckInt(Request["address_id"]);
        if (address_id > 0)
        {
            Session["Orders_Address_ID"] = address_id;
        }
        else
        { 
            
        }
        
        string action = Request["action"];
        switch (action)
        {
            case "addresslist":
                cart.Cart_Address_List();
                Response.End();
                break;
            //case "address":
            //    cart.Select_Cart_Address(tools.CheckInt(Request["address_id"]));
            //    Response.End();
            //    break;
            //case "address":
            //    cart.Cart_Address_List();
            //    Response.End();
            //    break;
            case "address":
                cart.Select_Cart_Address(tools.CheckInt(Request["address_id"]));
                Response.End();
                break;
                
            case "addressinfo":
                cart.Cart_Address_Infos();
                Response.End();
                break;
                
            case "loadpay":
                cart.Cart_Payway_List(tools.CheckInt(Request["cod"]));
                Response.End();
                break;
            case "KeepDelivery":
                Session["Orders_Delivery_ID"] = tools.CheckInt(Request["delivery_id"]);
                Session["Orders_Payway_ID"] = tools.CheckInt(Request["pay_id"]);
                Session["Orders_Paytype_ID"] = tools.CheckInt(Request["paytype_id"]);
                Session["Orders_DeliveryTime_ID"] = tools.CheckInt(Request["time_id"]);
                Session["U_Orders_IsNotify"] = tools.CheckInt(Request["isnotify"]);
                Session["U_Orders_PaymentMode"] = tools.CheckInt(Request["paymentmode"]);
                Response.Write("1");
                Response.End();
                break;  
            case "deliverypay":
                cart.Delivery_Payway_Info();
                Response.End();
                break;
            case "validatepay":
                cart.ValidatePay();
                Response.End();
                break;
            case "loadpayinfo":
                cart.Load_Payway_Info();
                Response.End();
                break;
            case "load_loanproduct":
                Response.Write(cart.GetQueryLoanProductInfo());
                Response.End();
                break;
            case "cartcount":
                Response.Write(cart.My_Cart_Count());
                Response.End();
                break;
        }
    }
</script>
