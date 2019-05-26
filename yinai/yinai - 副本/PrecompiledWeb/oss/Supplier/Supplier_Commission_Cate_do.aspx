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
                Public.CheckLogin("fca14348-91f1-4522-8063-98ff215d5dab");
                myApp.AddSupplierCommissionCategory();
                break;
            case "renew":
                Public.CheckLogin("deaa9168-3ffc-42c3-bb94-829fbf7f2e22");
                myApp.EditSupplierCommissionCategory();
                break;
            case "move":
                Public.CheckLogin("07d26693-d9d7-459b-a097-b6c5e763f8f7");
                myApp.DelSupplierCommissionCategory();
                break;
            case "list":
                Public.CheckLogin("ed55dd89-e07e-438d-9529-a46de2cdda7b");
                
                Response.Write(myApp.GetSupplierCommissionCategory());
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
