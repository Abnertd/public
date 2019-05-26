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
            case "renew":
                Public.CheckLogin("19578feb-813c-49cd-83c3-65c51bb05b09");
                myApp.EditSupplierPriceAsk();
                break;
            case "list":
                Public.CheckLogin("249d2ad4-45f4-4945-8e78-d18c79053106");

                Response.Write(myApp.GetSupplierPriceAsk());
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
