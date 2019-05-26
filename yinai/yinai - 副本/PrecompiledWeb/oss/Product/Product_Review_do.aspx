<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private ProductReview myReview;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myReview = new ProductReview();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "update_config":
                Public.CheckLogin("b948d76d-944c-4a97-82dc-a3917ce6dcd9");
                
                myReview.EditProductReviewConfig();
                break;
            case "move":
                Public.CheckLogin("3f48e253-5e00-44ce-8a9e-7475134bfd18");

                myReview.DelProductReview();
                break;
            case "list":
                Public.CheckLogin("cb1e9c33-7ac5-4939-8520-a0e192cb0129");
                
                Response.Write(myReview.GetProductReviews());
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
