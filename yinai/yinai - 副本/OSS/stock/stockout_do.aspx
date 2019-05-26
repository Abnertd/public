<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Product myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Product();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "list":
                Public.CheckLogin("6f896c98-c62f-43f6-a276-39e43697c771");

                Response.Write(myApp.GetStockouts());
                Response.End();
                break;
            case "processed":
                Public.CheckLogin("1da083a0-f751-4a93-995e-ca3c1edf44cc");
                
                myApp.Stockout_Processed();
                break;
            case "move":
                Public.CheckLogin("cd25a138-603d-445c-83f9-736de139c4c1");
                
                myApp.Stockout_Del();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>