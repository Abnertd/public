<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private ITools tools;
    private Statistic myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        myApp = new Statistic();
        
        string action = Request["action"];
        switch (action)
        {
            case "salecount":
                Public.CheckLogin("a1a413d5-f6c6-44c3-ae8b-a27f3a4b66c7");
                
                Response.Write(myApp.saleQuantity());
                Response.End();
                break;
            case "mebmerbuy":
                Public.CheckLogin("e102f8de-c29a-4233-b1cd-ba9cf037409b");
                
                Response.Write(myApp.memberBuyQuantity());
                Response.End();
                break;
            case"productbuyvisit":
                Public.CheckLogin("240b888b-a0cf-4683-8798-bddafc443431");
                
                Response.Write(myApp.productBuyVisit());
                Response.End();
                break;
            case "salecount_export":
                Public.CheckLogin("a1a413d5-f6c6-44c3-ae8b-a27f3a4b66c7");
                myApp.saleQuantity_Export();
                Response.End();
                break;
            case "memberbuy_export":
                Public.CheckLogin("e102f8de-c29a-4233-b1cd-ba9cf037409b");

                myApp.memberBuyQuantity_Export();
                Response.End();
                break;
            case "productbuy_export":
                Public.CheckLogin("240b888b-a0cf-4683-8798-bddafc443431");

                myApp.productBuyVisit_Export();
                Response.End();
                break;
            case "commission":
                Public.CheckLogin("all");
                Response.Write(myApp.Supplier_Commission());
                Response.End();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        tools = null;
        myApp = null;
    }
</script>