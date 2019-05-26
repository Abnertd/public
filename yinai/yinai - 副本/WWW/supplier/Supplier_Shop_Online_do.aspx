<%@ Page Language="C#" %>

<script runat="server">
    Supplier supplier;
    protected void Page_Load(object sender, EventArgs e)
    {
        supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Message.aspx");
        Supplier MyApp = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "add":
                MyApp.AddSupplierOnline();
                break;
            case "remove":
                MyApp.DelSupplierOnline();
                break;
            case "edit":
                MyApp.EditSupplierOnline();
                break;
            
        }

    }
</script>
