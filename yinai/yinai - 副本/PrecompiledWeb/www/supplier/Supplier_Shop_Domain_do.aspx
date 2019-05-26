<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "domain_add":
                supplier.AddSupplierShopDomain();
                break;
            case "remove":
                supplier.Shop_Domain_Del();
                break;
        }

    }
</script>
