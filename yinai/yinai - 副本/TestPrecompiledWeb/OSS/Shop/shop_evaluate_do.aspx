<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private ProductReview myReview;
    private Shop myshop;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myReview = new ProductReview();
        tools = ToolsFactory.CreateTools();
        myshop = new Shop();
        string action = Request["action"];
        switch (action)
        {
            case "update_config":
                Public.CheckLogin("b948d76d-944c-4a97-82dc-a3917ce6dcd9");
                
                myReview.EditProductReviewConfig();
                break;
                //商品评价
            case "list":
                Public.CheckLogin("74500aee-9f7e-4939-a039-61fafe790776");
                Response.Write(myshop.GetSupplierShopEvaluates());
                Response.End();
                break;
                //店铺评价
            case "supplier_evaluate_list":
                Public.CheckLogin("74500aee-9f7e-4939-a039-61fafe790776");
                Response.Write(myshop.GetShopSupplierEvaluates());
                Response.End();
                break;
                
            case "audit":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.EditSupplierShopEvaluate("audit");
                Response.End();
                break;
            case "deny":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.EditSupplierShopEvaluate("deny");
                Response.End();
                break;


            




            case "shop_evaluate_audit":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.EditSupplierShopEvaluate("shop_evaluate_audit");
                Response.End();
                break;
            case "shop_evaluate_deny":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.EditSupplierShopEvaluate("shop_evaluate_deny");
                Response.End();
                break;



            case "rewevaluate":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.Edit_SupplierShopEvaluate();
                Response.End();
                break;
                
                
            case "rewsupplierevaluate":
                Public.CheckLogin("532bf3c0-e243-4a10-a664-b764fa7c23f4");
                myshop.Edit_SupplierShopEvaluate();
                Response.End();
                break;
                
                
                
                
            
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myReview = null;
        tools = null;
    }
</script>
