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
                Public.CheckLogin("a1cb6100-8590-4dec-953b-59b13002df83");
                myApp.AddSupplierCert();
                break;
            case "renew":
                Public.CheckLogin("b399de70-e5f8-4d76-b0d7-16dc38245efc");
                myApp.EditSupplierCert();
                break;
            case "move":
                Public.CheckLogin("2760865e-7bac-4e14-8e54-a7de7e99fee6");
                myApp.DelSupplierCert();
                break;
            case "list":
                Public.CheckLogin("29f32a17-8d3f-4ca5-9628-524316760713");

                Response.Write(myApp.GetSupplierCert());
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
