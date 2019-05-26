<%@ Page Language="C#" %>

<script runat="server">
    private OrdersPayment myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new OrdersPayment();
        string action = Request["action"];
        switch (action)
        {
            //case "new":
            //    myApp.AddOrdersPayment();
            //    break;
            //case "renew":
            //    myApp.EditOrdersPayment();
            //    break;
            //case "move":
            //    myApp.DelOrdersPayment();
            //    break;
            case "list":
                Public.CheckLogin("493abbf7-121b-4a1e-a5da-977afb3d6718");
                Response.Write(myApp.GetOrdersPayments());
                Response.End();
                break;
            
            case "paymentexport":

                Public.CheckLogin("b52048ac-508e-4bf6-9327-9ff77a32e54c");
                myApp.OrdersPayment_Export();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
