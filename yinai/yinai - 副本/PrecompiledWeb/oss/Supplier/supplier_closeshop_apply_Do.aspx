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
                Public.CheckLogin("0791905a-1212-4fa5-8708-b835cc03c4a3");
                myApp.EditSupplierCloseShopApply(1);
                break;
            case "applynotpassaudit":
                Public.CheckLogin("0791905a-1212-4fa5-8708-b835cc03c4a3");
                myApp.EditSupplierCloseShopApply(2);
                break;
            case "move":
                Public.CheckLogin("bd8d861d-dca1-4e52-84a9-013c68e3134d");
                myApp.DelSupplierCloseShopApply();
                break;
            case "list":
                Public.CheckLogin("81e0af57-348d-4565-9e73-7146b3116b8c");

                Response.Write(myApp.GetSupplierCloseShopApplys());
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
