<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/index.aspx");
        string action = Request["action"];
        ITools tools = ToolsFactory.CreateTools();
        switch (action)
        {

            case "shop_apply":

                supplier.AddSupplierPurchase(0);
                break;
            case "shop_apply_1":

                supplier.AddSupplierPurchase(1);
                break;
            case "applydel":
                supplier.DelSupplierPurchase();
                break;
                
            case "shop_apply_edit":
                supplier.EditSupplierPurchase(0);
                break;
            case "shop_apply_edit_1":
                supplier.EditSupplierPurchase(1);
                break;
            case "shop_apply_pricereport":
                supplier.SupplierPurchase_PriceReport();
                break;
            case "change_mainpurchasecate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(supplier.Purchase_Category_Select(cate_id, target_div));
                Response.End();
                break; 
        }
    }
</script>
