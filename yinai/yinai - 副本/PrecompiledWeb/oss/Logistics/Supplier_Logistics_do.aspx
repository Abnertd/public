<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private SupplierLogistics myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SupplierLogistics();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "list":
                Public.CheckLogin("64bb04aa-9b78-4c41-ae9c-e94f57581e22");

                Response.Write(myApp.GetSupplierLogisticss());
                Response.End();
                break;

            case "Audit":
                Public.CheckLogin("65632742-f14a-4e44-8f7d-64e56c866da4");
                myApp.EditSupplierLogistics(1);
                break;

            case "NotAudit":
                Public.CheckLogin("65632742-f14a-4e44-8f7d-64e56c866da4");
                myApp.EditSupplierLogistics(2);
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>