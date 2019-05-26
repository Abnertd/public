<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("54431a50-c170-4854-b371-1ea960c86272");
                supplier.AddSupplierMargin();
                break;
            case "renew":
                Public.CheckLogin("78f37fa4-ba54-486e-9159-f3050560999d");
                supplier.EditSupplierMargin();
                break;
            case "move":
                Public.CheckLogin("d5a64ee0-f232-474e-9074-1e2dc396c2a3");
                supplier.DelSupplierMargin();
                break;
            case "list":
                Public.CheckLogin("d7c53f80-5ad9-4bc4-8f4e-cc31144f7de6");
                Response.Write(supplier.GetSupplierMargins());
                Response.End();
               break ;
        }
    }
</script>
