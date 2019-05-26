<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private RBACResource myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a63248fd-532f-40a8-850d-d217c5ddd38a");
        myApp = new RBACResource();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddRBACResourceGroup();
                break;
            case "renew":
                myApp.EditRBACResourceGroup();
                break;
            case "move":
                myApp.DelRBACResourceGroup();
                break;
            case "list":
                Response.Write(myApp.GetRBACResourceGroups());
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