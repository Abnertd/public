<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Product product = new Product();
        ITools tools = ToolsFactory.CreateTools();
        string action = Request["action"];

        switch (action)
        {
            case "insale":
                product.Product_Insale_Edit(1);
                Response.End();
                break;
            case "cancelinsale":
                product.Product_Insale_Edit(0);
                Response.End();
                break;
                
                
            case "inquiry":
                product.AddSupplierPriceAsk();
                break;
            case "loadProduct":
                product.Home_NewProduct_List();
                break;
            case "getmore":
                product.TradeIndex_Product_List();
                break;

            case "getmore1":
                string cate_ids = tools.CheckStr(Request["defaultcate"]);
                product.TradeIndex_OtherProduct_List(cate_ids);
                break;
            case "gethotmore":
                product.TradeIndex_Hot_ProductList("今日爆款");
                break;

            case "load_favcount":
                int product_id = tools.NullInt(Request["product_id"]);
                Response.Write(product.GetFavoriteCountByProductID(product_id));
                break;
        }
    }
</script>
