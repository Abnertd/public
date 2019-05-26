<%@ Page Language="C#" %>

<script runat="server">
    Supplier supplier;
    protected void Page_Load(object sender, EventArgs e)
    {
        supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Supplier_PayBack_Apply.aspx");
        Supplier MyApp = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                MyApp.AddSupplierCloseShopApply();
                break;
           
        }

    }
</script>
