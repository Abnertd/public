<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private RBACRole myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new RBACRole();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("71268a79-72ee-4892-a8f8-cf02b13c312a");
                
                myApp.AddRBACRole();
                break;
            case "renew":
                Public.CheckLogin("4df5eb30-ee06-49a4-b119-4c72e5dfaebc");
                
                myApp.EditRBACRole();
                break;
            case "move":
                Public.CheckLogin("3cfb485b-375e-4b4a-af15-1cf74946e333");
                
                myApp.DelRBACRole();
                break;
            case "list":
                Public.CheckLogin("8b470aa6-c158-4b70-8e7f-c640af462cf1");
                
                Response.Write(myApp.GetRBACRoles());
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