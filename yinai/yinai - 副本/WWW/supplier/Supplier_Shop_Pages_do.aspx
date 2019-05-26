<%@ Page Language="C#" %>

<script runat="server">
    Supplier supplier;
    protected void Page_Load(object sender, EventArgs e)
    {
        supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Pages_List.aspx");
        Supplier MyApp = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                MyApp.SupplierShopPagesSave();
                break;
            case "renew":
                MyApp.EditSupplierShopPages();
                break;
            case "pagesdel":
                MyApp.SupplierShopPages_Del();
                break;
            case "articlesave":
                MyApp.SupplierShopArticleSave();
                break;
            case "articledel":
                MyApp.SupplierShopArticle_Del();
                break;
        }

    }
</script>
