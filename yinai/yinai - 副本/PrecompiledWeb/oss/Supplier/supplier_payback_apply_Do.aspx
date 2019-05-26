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
            case "applypassaudit":
                Public.CheckLogin("479e01e0-d543-47c2-a229-52e9eb847886");
                myApp.EditSupplierPayBackApply(1);
                break;
            case "applynotpassaudit":
                Public.CheckLogin("479e01e0-d543-47c2-a229-52e9eb847886");
                myApp.EditSupplierPayBackApply(2);
                break;
            case "move":
                Public.CheckLogin("70939c0f-4e76-4f4a-9d6c-cff9e11e27ec");
                myApp.DelSupplierPayBackApply();
                break;
            case "list":
                Public.CheckLogin("b90823db-e737-4ae9-b428-1494717b85c7");

                Response.Write(myApp.GetSupplierPayBackApplys());
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
