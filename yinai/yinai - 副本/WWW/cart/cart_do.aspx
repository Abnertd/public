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
        string coupon_code, coupon_verifycode, orders_sn;
        
        string action = Request["action"];
        int product_id = tools.CheckInt(tools.NullStr(Request["product_id"]));
        int package_id = tools.CheckInt(tools.NullStr(Request["package_id"]));
        int buy_amount = tools.CheckInt(tools.NullStr(Request["buy_amount"]));
        int goods_id = tools.CheckInt(tools.NullStr(Request["goods_id"]));
        coupon_code = tools.CheckStr(tools.NullStr(Request["coupon_code"]));
        coupon_verifycode = tools.CheckStr(tools.NullStr(Request["verify_code"]));
        orders_sn = tools.CheckStr(tools.NullStr(Request["orders_sn"]));
        if (buy_amount < 1)
        {
            buy_amount = 1;
        }
        
        switch (action)
        {
            case "add":
                cart.My_Cart_Update("add", product_id, package_id, buy_amount);
                break;
            case "add_coinbuy":
                cart.My_Cart_Update("add_coinbuy", product_id, package_id, buy_amount);
                break;
            case "renew":
                
                cart.Cart_ProductEdit(goods_id, product_id, buy_amount);
                break;

            //case "renew":
            //    cart.Cart_ProductEdit(goods_id, product_id, buy_amount);
            //    break;

            case "contract_renew":
                cart.Contract_ProductEdit(goods_id, product_id, buy_amount, orders_sn);
                break;
                
                
                
            case "packrenew":
                cart.Cart_PackageEdit(goods_id, package_id, buy_amount);
                break;
            case "move":
                cart.Cart_ProductDel(goods_id);
                break;
            case "packmove":
                cart.Cart_PackageDel(goods_id);
                break;
            case "moveall":
                cart.Cart_ProductDel_All();
                break;
            case "batchmove":
                cart.Cart_ProductDel_Batch();
                break;
            case "coupon_verify":
                cart.Cart_Check_Coupon(coupon_code, coupon_verifycode);
                cart.Cart_Coupon_Display();
                Response.End();
                break;
            case "coupon_del":
                cart.Cart_Coupon_Cancel();
                cart.Cart_Coupon_Display();
                Response.End();
                break;
            case "update_cartprice":
                cart.My_Cartprice(0, tools.NullDbl(Session["delivery_fee"]));
                Response.End();
                break;
            case "update_totalprice":
                cart.My_Carttotalprice();
                Response.End();
                break;
            case "setCartPrice":
                cart.My_CartPrice();
                Response.End();
                break;

            case "setCartPrice2":
                cart.My_BidCartPrice();
                Response.End();
                break;
                
            case "selectaddress":
                cart.Cart_Address_Info();
                Response.End();
                break;
            case "selectdelivery":
                cart.Cart_Delivery_List();
                Response.End();
                break;

            case "ChangeAddress":
                cart.ChangeAddress();
                Response.End();
                break;
            case "resetdelivery":
                Session["Orders_Delivery_ID"] = 0;
                Session["Orders_Payway_ID"] = 0;
                Response.End();
                break;
            case "change_totalprice":
                Response.Write(cart.Get_Cart_TotalPrice());
                Response.End();
                break;
            case "setdefaultaddress":
                cart.SetDefaultAddress();
                Response.End();
                break;
            case "setDeliverySession":
                cart.SetCartDeliverySession();
                break;
            case "loadcart":
                cart.My_Cart_ProductList(false);
                break;
            case "getconfirm":
                cart.GetConfirmUrl();
                break;
        }
        //Response.Redirect("/cart/my_cart.aspx");

    }
</script>
