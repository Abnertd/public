<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Shop myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Shop();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "list":
              Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
              Response.Write(myApp.GetSupplierShops());
                Response.End();
                break;
            case "renew":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.EditSupplierShop();
                break;
            case "shopupgrade1":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopUpGradeBatch(1);
                Response.End();
                break;
            case "shopupgrade2":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopUpGradeBatch(2);
                Response.End();
                break;
            case "shopupgrade3":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopUpGradeBatch(3);
                Response.End();
                break;
            case "pageslist":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                Response.Write(myApp.GetShopPages());
                Response.End();
                break;
            case "pagecheck":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopPagesCheck(1);
                Response.End();
                break;
            case "pagedeny":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopPagesCheck(2);
                Response.End();
                break;
            case "pagemove":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.DelSupplierShopPages();
                Response.End();
                break;
            case "articlelist":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                Response.Write(myApp.GetShopArticles());
                Response.End();
                break;
            case "articlecheck":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopArticleCheck(1);
                Response.End();
                break;
            case "articledeny":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopArticleCheck(2);
                Response.End();
                break;
            case "articlemove":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.DelSupplierShopArticle();
                Response.End();
                break;
            case "domainlist":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                Response.Write(myApp.GetShopDomains());
                Response.End();
                break;
            case "domaincheck":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopDomainsCheck(1);
                Response.End();
                break;
            case "domaindeny":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.SupplierShopDomainsCheck(2);
                Response.End();
                break;
            case "doaminmove":
                Public.CheckLogin("1392d14a-6746-4167-804a-d04a2f81d226");
                myApp.DelSupplierShopDomains();
                Response.End();
                break;
            case "upgrade":
                Public.CheckLogin("97f86571-d7ce-4fce-add5-f444c3a89ce3");
                myApp.Supplier_Shop_UpGrade();
                Response.End();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
