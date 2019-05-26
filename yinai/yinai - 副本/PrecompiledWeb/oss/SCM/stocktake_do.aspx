<%@ Page Language="C#" %>

<script runat="server">
    
    private SCMPurchasing myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("849805bd-ba21-4508-a803-9e0e5cc33b66");

        myApp = new SCMPurchasing();
        string action = Request["action"];
        switch (action)
        {
            case "list":
                Response.Write(myApp.StockTakeList());
                Response.End();
                break;
            case "stocktake":
                myApp.ProductStockTake();
                break;
            case "showstock":
                Response.Write(myApp.ShowBranchStock(Request.QueryString["product_code"]));
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
