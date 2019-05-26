<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.Product" %>

<script runat="server">
    
    private SCMPurchasing myApp;
    private ITools tools;
    private IProduct PROBLL;
    private string Product_Code;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SCMPurchasing();
        tools = ToolsFactory.CreateTools();
        PROBLL = ProductFactory.CreateProduct();
        
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("7cf163d1-e650-4f92-b5ad-69c9fde076d4/81979be3-1310-4ad3-b044-efe68dc4902e/6f566c26-20d6-4768-a180-492310e5b8cd/dc81bffa-f5f8-411c-b3f5-435702bb2df8");
                
                myApp.AddPurchasing();
                break;
            case "renew":
                Public.CheckLogin("a56c96f7-fb31-4944-a248-45a8ad3c4398/a133f0cd-9a5e-4d02-ad94-9e0c0424d66d/87a8726b-5113-46ec-845c-6bd377935196/bfc31928-a7f1-45e0-bd3e-88a4268593ce");
                
                myApp.EditPurchasing();
                break;
            case "move":
                Public.CheckLogin("6b56546f-b64b-4365-85dd-e053f0a51630/598449ed-90f9-4211-bdd5-d471d8fda8a0/692dd604-e428-40cf-9202-43d333245b1d/7c806dd5-9a9a-4e83-bdec-10b328491c1a");
                
                myApp.DelPurchasing();
                break;
            case "list":
                Public.CheckLogin("8b46f99a-d4ef-42aa-b593-795525ab4bab/ad01bd92-647f-40d7-8985-845fc46d832f/0ca87d96-8bfd-4f62-88bb-ef256b41cfdb/70eb9566-dddf-425f-8074-8638a9089f08/849805bd-ba21-4508-a803-9e0e5cc33b66");
                
                Response.Write(myApp.GetPurchasings());
                Response.End();
                break;
            case "export":
                Public.CheckLogin("8b46f99a-d4ef-42aa-b593-795525ab4bab/ad01bd92-647f-40d7-8985-845fc46d832f/0ca87d96-8bfd-4f62-88bb-ef256b41cfdb/70eb9566-dddf-425f-8074-8638a9089f08/849805bd-ba21-4508-a803-9e0e5cc33b66");

                myApp.GetPurchasings_Export();
                Response.End();
                break;
            case "nolist":
                Public.CheckLogin("8b46f99a-d4ef-42aa-b593-795525ab4bab/ad01bd92-647f-40d7-8985-845fc46d832f/0ca87d96-8bfd-4f62-88bb-ef256b41cfdb/70eb9566-dddf-425f-8074-8638a9089f08/849805bd-ba21-4508-a803-9e0e5cc33b66");

                Response.Write(myApp.GetNoStockPurchasings());
                Response.End();
                break;
            case "productdetail":

                Public.CheckLogin("all");
                
                Product_Code = tools.CheckStr(Request.QueryString["Product_Code"]);

                if (Product_Code.Length > 0)
                {
                    ProductInfo productEntity = PROBLL.GetProductByCode(Product_Code, Public.GetCurrentSite(), Public.GetUserPrivilege());

                    if (productEntity != null)
                    {
                        if (productEntity.Product_SupplierID == 0)
                        {
                            Response.Write("<table>");
                            Response.Write("    <tr>");
                            Response.Write("        <th align=\"right\">商品名称</th>");
                            Response.Write("        <td>" + productEntity.Product_Name + "</td>");
                            Response.Write("    </tr>");
                            Response.Write("    <tr>");
                            Response.Write("        <th align=\"right\">产地</th>");
                            Response.Write("        <td>" + productEntity.Product_Maker + "</td>");
                            Response.Write("    </tr>");
                            Response.Write("    <tr>");
                            Response.Write("        <th align=\"right\">规格</th>");
                            Response.Write("        <td>" + productEntity.Product_Spec + "</td>");
                            Response.Write("    </tr>");
                            Response.Write("</table>");
                        }
                    }
                }
                break; 
            case "selectproduct":
                Response.Write(myApp.SelectProduct());
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
        PROBLL = null;
    }
</script>
