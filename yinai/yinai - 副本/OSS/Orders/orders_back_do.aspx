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
            case "list":
                Public.CheckLogin("aaa944b1-6068-42cd-82b5-d7f4841ecf45");
                
                Response.Write(myApp.GetOrdersBackApplyList());
                Response.End();
                break;
            case "list1":
                Public.CheckLogin("add11f44-d1eb-48bc-bc58-48673b91591a");

                Response.Write(myApp.GetOrdersBackApplyList1());
                Response.End();
                break;
                
            case "apply_edit":
                Public.CheckLogin("1f9e3d6c-2229-4894-891b-13e73dd2e593");

                myApp.OrdersBackApplyEdit();
                Response.End();
                break;
            case "move":
                Public.CheckLogin("2a5f3eef-36a5-4d2a-83cc-3a4ff9f084ed");

                myApp.DelOrdersBackApply();
                Response.End();
                break;
            case "productinsert":
                Public.CheckLogin("add11f44-d1eb-48bc-bc58-48673b91591a");
                myApp.Creat_Orders_Delivery();
                Response.End();
                break;
                
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
