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
                Public.CheckLogin("da202dcc-2dba-4d0b-829f-d170541a1e80");
                myApp.AddSupplierTag();
                break;
            case "renew":
                Public.CheckLogin("a6691534-a5e6-4636-901b-88a62ea1acc1");
                myApp.EditSupplierTag();
                break;
            case "move":
                Public.CheckLogin("9a2249c4-2c18-4902-b8ea-7e597084cca5");
                myApp.DelSupplierTag();
                break;
            case "list":
                Public.CheckLogin("169befcc-aa3b-42d1-b5b8-d1a08096bc0e");
                
                Response.Write(myApp.GetSupplierTag());
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
