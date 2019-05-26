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
            
            case "list":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                Response.Write(myApp.GetSupplierConsumptions());
                Response.End();
                break;
            case "accountlist":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                Response.Write(myApp.GetSupplierAccountLogs());
                Response.End();
                break;
            case "coin_process":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                //myApp.Supplier_Coin_Process();
                break;
            case "account_process":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
             //   myApp.Supplier_Account_Process();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
