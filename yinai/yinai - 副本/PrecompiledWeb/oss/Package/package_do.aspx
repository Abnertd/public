<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private ITools tools;
    private Package myApp;
    int product_id;
    string strPID;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        myApp = new Package();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("573393a4-573e-4872-ad7b-b77d75e0f611");
                
                Session.Remove("productid_ed");
                myApp.AddPackage();
                break;
            case "renew":
                Public.CheckLogin("5666872b-2113-490b-a41f-a7a65083324a");
                
                Session.Remove("productid_ed");
                myApp.EditPackage();
                break;
            case "move":
                Public.CheckLogin("7b8b58e2-e509-4e6c-a68e-0361225cefa6");
                
                myApp.DelPackage();
                break;
            case "list":
                Public.CheckLogin("0dd17a70-862d-4e57-9b45-897b98e8a858");
                
                Response.Write(myApp.GetPackages());
                Response.End();
                break;
            case "check_product":

                Public.CheckLogin("573393a4-573e-4872-ad7b-b77d75e0f611/5666872b-2113-490b-a41f-a7a65083324a");
                
                strPID = tools.CheckStr(Request.QueryString["product_id"]);
                if (strPID.Length > 0 ) {
                    IList<PackageProductInfo> entityList = (IList<PackageProductInfo>)Session["PackageProductInfo"];
                    PackageProductInfo entity = null;
                    string[] PIDARR = strPID.Split(',');
                    foreach (string addPID in PIDARR)
                    {
                        if (tools.CheckInt(addPID) < 1) { continue; }
                        
                        entity = new PackageProductInfo();
                        entity.Package_Product_ProductID = int.Parse(addPID);
                        entity.Package_Product_Amount = 1;
                        entity.Package_Product_PackageID = 0;
                        entity.Package_Product_ID = 0;
                        entityList.Add(entity);
                    }
                    Session["PackageProductInfo"] = null;
                    Session["PackageProductInfo"] = entityList;
                    entityList = null; 
                }
                Response.Write(myApp.ShowProduct());
                break;
            case "product_del":
                Public.CheckLogin("573393a4-573e-4872-ad7b-b77d75e0f611/5666872b-2113-490b-a41f-a7a65083324a");
                
                product_id = tools.CheckInt(Request.QueryString["product_id"]);
                if (product_id > 0) {
                    IList<PackageProductInfo> entityList = (IList<PackageProductInfo>)Session["PackageProductInfo"];
                    foreach (PackageProductInfo entity in entityList) {
                        if (entity.Package_Product_ProductID == product_id) { entityList.Remove(entity); break; }
                    }
                    Session["PackageProductInfo"] = null;
                    Session["PackageProductInfo"] = entityList;
                    entityList = null; 
                }
                
                Response.Write(myApp.ShowProduct());
                break;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
