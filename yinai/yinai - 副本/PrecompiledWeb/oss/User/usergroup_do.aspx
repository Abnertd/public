<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private RBACUserGroup myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a2f95df4-346a-47b2-a112-1f8e3f062298");
        myApp = new RBACUserGroup();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddRBACUserGroup();
                break;
            case "renew":
                myApp.EditRBACUserGroup();
                break;
            case "move":
                myApp.DelRBACUserGroup();
                break;
            case "list":
                Response.Write(myApp.GetRBACUserGroups());
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