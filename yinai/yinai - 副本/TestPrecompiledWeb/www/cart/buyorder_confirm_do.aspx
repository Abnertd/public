<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Cart cart = new Cart();
        Supplier supplier = new Supplier();
        
        string action = Request["action"];
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

        supplier.Supplier_Login_Check("/cart/my_buycart.aspx?PriceReport_ID=" + PriceReport_ID + "&Purchase_ID=" + Purchase_ID);
        switch (action)
        {
            case "saveorder":
                cart.BuyOrders_Save();
                break;
            
           
        }

    }
</script>
