<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Supplier myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("11fe78b3-c971-4ed1-bb5e-3a31b60b19cd");
                myApp.AddSupplierMessage();
                break;
            case "renew":
                Public.CheckLogin("b7d38ac5-000c-4d07-9ca3-46df47367554");
                myApp.EditSupplierMessage();
                break;
            case "move":
                Public.CheckLogin("ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7");
                myApp.DelSupplierMessage();
                break;
            case "list":
                Public.CheckLogin("d8b3c47b-26c4-435f-884e-c9951464b633");
                
                Response.Write(myApp.GetSupplierMessages());
                Response.End();
                break;
            
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
