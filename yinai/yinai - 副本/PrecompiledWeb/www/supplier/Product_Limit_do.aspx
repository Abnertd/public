<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        supplier.Supplier_Login_Check("/supplier/Product_Limit_List.aspx");
        ITools tools = ToolsFactory.CreateTools();
        switch (action)
        {
            case "searchproduct":
                Response.Write(supplier.Select_Supplier_Product(0));
                break;
            case "save":
                supplier.AddPromotionLimit();
                break;
            case "renew":
                supplier.EditPromotionLimit();
                break;
            case "remove":
                supplier.DelPromotionLimit();
                break;
            case "showproduct":
                supplier.Show_ProductInfo(0);
                break;
            
        }

    }
</script>