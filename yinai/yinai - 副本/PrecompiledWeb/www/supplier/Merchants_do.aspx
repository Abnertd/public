<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Supplier_MerchantsList.aspx");
        string action = Request["action"];
        ITools tools = ToolsFactory.CreateTools();
        switch (action)
        {
            case "save":
                supplier.Supplier_Merchants_Add();
                break;
            case "edit":
                supplier.Supplier_Merchants_Edit();
                break;
            case "move":
                supplier.DelSupplierMerchants();
                break;
            case "purchasereply":
                supplier.AddPurchaseReply();
                break;
            case "show_reply_dialog":
                supplier.Show_PurchaseReply_Dialog();
                break;
        }
    }
</script>
