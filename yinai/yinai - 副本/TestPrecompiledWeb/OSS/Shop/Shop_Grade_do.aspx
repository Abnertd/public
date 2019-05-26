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
                Public.CheckLogin("ec8e6ed4-907f-4777-be1c-e07690e2eab0");
                myApp.AddSupplierShopGrade();
                break;
            case "renew":
                Public.CheckLogin("adde3836-fe74-4976-9297-61fe4b3db991");
                myApp.EditSupplierShopGrade();
                break;
            case "move":
                Public.CheckLogin("3ba627a6-0a91-48d3-a4bc-9e2a84fc8dba");
                myApp.DelSupplierShopGrade();
                break;
            case "list":
                Public.CheckLogin("c558f983-68ec-4a91-a330-1c1f04ebdf01");

                Response.Write(myApp.GetSupplierShopGrades());
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
