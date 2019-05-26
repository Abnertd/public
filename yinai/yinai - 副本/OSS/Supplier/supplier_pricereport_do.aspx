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
                Public.CheckLogin("b482df13-2941-4314-9200-b64db8b358bc");
                myApp.AddSupplierPriceReport();
                break;
            case "renew":
                Public.CheckLogin("d2656c57-1fbb-4928-8bff-41488d5763cc");
                myApp.EditSupplierPriceReport();
                break;
            case "list":
                Public.CheckLogin("6a12664e-4eeb-4259-b7b5-904044194067");

                Response.Write(myApp.GetSupplierPriceReport());
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("0c39529f-732e-463e-9344-dc6d9f64cef9");
                myApp.SupplierPriceReportAudit(1);
                Response.End();
                break;
            case "denyaudit":
                Public.CheckLogin("0c39529f-732e-463e-9344-dc6d9f64cef9");
                myApp.SupplierPriceReportAudit(2);
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
