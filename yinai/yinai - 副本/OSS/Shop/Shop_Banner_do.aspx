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
                Public.CheckLogin("bdf7d616-c68b-439b-97a7-ac19aebfacba");
                myApp.AddSupplierShopBanner();
                break;
            case "renew":
                Public.CheckLogin("9f2a1a11-c019-4443-b6eb-18ab1483e0b9");
                myApp.EditSupplierShopBanner();
                break;
            case "move":
                Public.CheckLogin("a574ef1a-b5ce-43ba-ab38-3470a9896237");
                myApp.DelSupplierShopBanner();
                break;
            case "list":
                Public.CheckLogin("daff677a-1be4-4438-b1e8-32b453275341");

                Response.Write(myApp.GetSupplierShopBanners());
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
