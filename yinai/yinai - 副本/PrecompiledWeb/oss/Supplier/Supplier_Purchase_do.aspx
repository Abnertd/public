<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Supplier myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("59ac1c26-ba95-42c9-b418-fae8465f6e94");
                myApp.AddSupplierPurchase(1);
                Response.End();
                break;
            case "renew":
                Public.CheckLogin("aa55fc69-156e-45fe-84fa-f0df964cd3e0");
                myApp.EditSupplierPurchase(1);
                Response.End();
                break;
            case "list":
                Public.CheckLogin("c197743d-e397-4d11-b6fc-07d1d24aa774");
                Response.Write(myApp.GetSupplierPurchases());
                Response.End();
                break;
            case "audit":
                Public.CheckLogin("398b0fb4-3a5d-4de8-96e1-ef1b9814cbea");
                myApp.SupplierPurchaseAudit(2);
                Response.End();
                break;
            case "denyaudit":
                Public.CheckLogin("398b0fb4-3a5d-4de8-96e1-ef1b9814cbea");
                myApp.SupplierPurchaseAudit(3);
                Response.End();
                break;
            case "recommend":
                Public.CheckLogin("9ff2555b-8d3b-44c6-87fb-4f8af64d73f5");
                myApp.SupplierPurchaseRecommend(1);
                Response.End();
                break;
            case "unrecommend":
                Public.CheckLogin("9ff2555b-8d3b-44c6-87fb-4f8af64d73f5");
                myApp.SupplierPurchaseRecommend(0);
                Response.End();
                break;
            case "hangup":
                Public.CheckLogin("d3c51814-7b5e-4275-bcb4-bdbc3a9f94da");
                myApp.SupplierPurchaseActive(0);
                Response.End();
                break;
            case "unhangup":
                Public.CheckLogin("d3c51814-7b5e-4275-bcb4-bdbc3a9f94da");
                myApp.SupplierPurchaseActive(1);
                Response.End();
                break;
            case "trash":
                Public.CheckLogin("db50b828-d105-43e2-9a40-f9447a999d91");
                myApp.SupplierPurchaseTrash(1);
                Response.End();
                break;
            case "untrash":
                Public.CheckLogin("db50b828-d105-43e2-9a40-f9447a999d91");
                myApp.SupplierPurchaseTrash(0);
                Response.End();
                break;
            case "update":
                Public.CheckLogin("aa55fc69-156e-45fe-84fa-f0df964cd3e0");
                myApp.EditSupplierPurchase();
                Response.End();
                break;

            case "change_mainpurchasecate":
                string target_div = tools.CheckStr(Request.QueryString["target"]);
                int cate_id = tools.CheckInt(Request.QueryString["cate_id"]);
                Response.Write(myApp.Purchase_Category_Select(cate_id, target_div));
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
