<%@ Page Language="C#" %>

<script runat="server">
    private OrdersDelivery myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new OrdersDelivery();
        string action = Request["action"];
        switch (action)
        {
            //case "new":
            //    myApp.AddOrdersDelivery();
            //    break;
            //case "renew":
            //    myApp.EditOrdersDelivery();
            //    break;
            //case "move":
            //    myApp.DelOrdersPayment();
            //    break;
            case "list":
                Public.CheckLogin("f606309a-2aa9-42e3-9d45-e0f306682a29");

                Response.Write(myApp.GetOrdersDeliverys());
                Response.End();
                break;
            case "deliveryexport":
                Public.CheckLogin("453d0181-57f9-4041-9e10-6f42d12cd43f");

                myApp.OrdersDelivery_Export();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
