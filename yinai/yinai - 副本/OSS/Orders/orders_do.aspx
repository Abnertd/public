<%@ Page Language="C#" %>
<script runat="server">
    private Orders myApp;
    private OrdersProcess uorder;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Orders();
        uorder = new OrdersProcess();
        string action = Request["action"];
        switch (action)
        {
            //case "new":
            //    myApp.AddDeliveryWay();
            //    break;
            //case "renew":
            //    myApp.EditDeliveryWay();
            //    break;
            //case "move":
            //    myApp.DelDeliveryWay();
            //    break;
            case "list":
                Public.CheckLogin("5e807815-409c-4d01-8e1a-2f835fbf2ac5");
                
                Response.Write(myApp.GetOrdersList());
                Response.End();
                break;
            case "order_admin_note":
                Public.CheckLogin("c4de02f2-c819-4ae3-9a6e-be8645b5271f");
                
                myApp.Orders_Admin_Note();
                break;
            case "order_delivery":
                Public.CheckLogin("44d5b89f-ed3e-4167-8eea-f5c66ec48faa");
                
                myApp.Orders_Delivery_Edit();
                break;
            case "order_payway":
                Public.CheckLogin("d0106a6a-c9ef-4a18-8eb9-f65f11be76c4");
                
                myApp.Orders_Payway_Edit();
                break;
            case "cancel":
                Public.CheckLogin("c4f1a30c-b384-4311-bb68-0cd9e892196c");
                
                myApp.Orders_Cancel();
                break;
            case "order_confirm":
                Public.CheckLogin("c1de0682-e5a2-4545-9c21-748b73c014ef");
                
                myApp.Orders_Confirm();
                break;
            case "order_reach":
                Public.CheckLogin("25b5f3a9-5aba-45ac-8f28-c4f30e6054aa");
                
                myApp.Orders_PayReach();
                break;
            case "order_prepare":
                Public.CheckLogin("25996ef4-35b5-4980-a4cd-60c368db97f8");
                
                myApp.Orders_Delivery_Prepare();
                break;
            case "order_accept":
                Public.CheckLogin("e3e651b8-b47a-4391-80b5-970e624c6275");

                myApp.Orders_Delivery_Accept();
                break;
            case "create_payment":
                Public.CheckLogin("272974c4-7a98-4a00-a1e0-6d730e565cdb");
                
                myApp.Orders_Pay("create");
                break;
            case "cancel_payment":
                Public.CheckLogin("02fb8713-d70d-4da2-9f7f-2ce5cd033e0a");
                
                myApp.Orders_Pay("cancel");
                break;
            case "cancel_paymentapply":
                Public.CheckLogin("95515ef3-e035-4400-b4fc-da4d8f5a530f");

                myApp.Orders_PayApply();
                break;
            case "create_freight":
                Public.CheckLogin("6623eae9-9663-4726-844c-7e0eefa5c335");
                
                myApp.Orders_Delivery("create");
                break;
            case "cancel_freight":
                Public.CheckLogin("4d0ebf65-94db-4992-bfcf-49b28f09ef38");
                
                myApp.Orders_Delivery("cancel");
                break;
            case "order_success":
                Public.CheckLogin("f210813c-8d1d-4e1d-8dde-7d566925f6d6");
                
                myApp.Orders_Success();
                break;
            case "order_close":
                Public.CheckLogin("705ee66c-4a2f-4064-9f34-3c4fb5d45e21");
                
                myApp.Orders_Close();
                break;
            case "invoice_open":
                Public.CheckLogin("94a85b34-f666-4bea-b98d-d6565fa05988");
                
                myApp.Orders_Invoice_Action("open");
                break;
            case "invoice_cancel":
                Public.CheckLogin("94a85b34-f666-4bea-b98d-d6565fa05988");
                
                myApp.Orders_Invoice_Action("cancel");
                break;
            case "save_batch":
                Public.CheckLogin("all");
                
                myApp.Orders_Goods_Batchcode_Save();
                break;
            case "save_buy":
                Public.CheckLogin("all");
                
                myApp.Orders_Goods_Buy_Save();
                break;
            case "address_edit":
                Public.CheckLogin("32c0d059-33bb-435c-b09e-ad37378cdd1e");
                
                myApp.Orders_Address_Edit();
                break;
            case "ordergoodsexport":
                Public.CheckLogin("459b8b32-8af3-405c-8daa-4cadaa315f4b");
                
                myApp.Orders_Goods_Export();
                break;

            case "ordersexport":
                Public.CheckLogin("78d35c08-d684-4672-9fdb-a7f2187e251c");

                myApp.Orders_Export();
                break;

            case "goodstmp_add":

                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
                uorder.Orders_Goodstmp_Add();
                break;
            case "goodstmp_edit":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
                
                uorder.Orders_Goodstmp_Edit();
                break;
            case "goodstmp_del":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
                
                uorder.Orders_Goodstmp_Del();
                break;
            case "goodstmpcart_edit":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");

                uorder.Cart_Goodstmp_Edit();
                break;
            case "goodstmpcart_del":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");

                uorder.Cart_Goodstmp_Del();
                break;
            case "goodstmp_fee":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
                
                uorder.Orders_Goodstmp_Deliveryfee();
                break;
            case "save_order":
                Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
                
                uorder.Orders_Edit();
                break;
            case "orderendprice":
                Public.CheckLogin("00b42a78-2cef-4a22-865d-dd2ad9003ec5");
                myApp.Supplier_CommissionSettling(2);
                break;
            case "cancelendprice":
                Public.CheckLogin("00b42a78-2cef-4a22-865d-dd2ad9003ec5");
                myApp.Supplier_CommissionSettling(0);
                break; 

                
        }
    }
    
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
