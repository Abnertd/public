<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private ProductAuditReason myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        
        myApp = new ProductAuditReason();
        tools = ToolsFactory.CreateTools();
        string objValue;
        int product_id;
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("a71b2324-aa1c-46c8-8525-742f96b44755");
                
                myApp.AddProductAuditReason();
                break;
            case "renew":
                Public.CheckLogin("158a1875-7682-4781-97ef-7f31e39280c1");
                
                myApp.EditProductAuditReason();
                break;
            case "move":
                Public.CheckLogin("78d18ad2-7c45-4a9c-9a53-cbe50562c242");
                
                myApp.DelProductAuditReason();
                break;
            case "list":
                Public.CheckLogin("a1db5d4d-d497-42b6-992e-0420d6cdc446");
                
                Response.Write(myApp.GetProductAuditReasons());
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