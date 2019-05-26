<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Shop myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Shop();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("8c936480-7e6e-4482-9e22-5eb9b1fbec8a");
                myApp.AddSupplierShopCss();
                break;
            case "renew":
                Public.CheckLogin("227ca224-42de-48c6-9e4b-d09d019f7b36");
                myApp.EditSupplierShopCss();
                break;
            case "move":
                Public.CheckLogin("8407715f-18d7-445b-92a1-0c7ce9cc027a");
                myApp.DelSupplierShopCss();
                break;
            case "list":
                Public.CheckLogin("3396b3c6-8116-4c3b-9682-6d29c937947e");

                Response.Write(myApp.GetSupplierShopCsses());
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
