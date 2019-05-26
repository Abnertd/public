<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private RBACPrivilege myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new RBACPrivilege();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("df7e7e2e-bbe2-48b0-976c-17a74c4a45e6");
                
                myApp.AddRBACPrivilege();
                break;
            case "renew":
                Public.CheckLogin("51be7b46-e0f7-46dd-b0b2-a462fcb907ae");
                
                myApp.EditRBACPrivilege();
                break;
            case "move":
                Public.CheckLogin("1030465e-7113-4db6-9b3c-da21aca07748");
                
                myApp.DelRBACPrivilege();
                break;
            case "list":
                Public.CheckLogin("147d21e2-7989-44e7-8b08-0c64797c2513");
                
                Response.Write(myApp.GetRBACPrivileges());
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