<%@ Page Language="C#" %>

<script runat="server">
    
    private SCMPurchasing myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("56ce0ee5-9140-483d-a2b6-1ec6a100196e");

        myApp = new SCMPurchasing();
        string action = Request["action"];
        switch (action)
        {
            case "list":
                Response.Write(myApp.StockAlert());
                Response.End();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
