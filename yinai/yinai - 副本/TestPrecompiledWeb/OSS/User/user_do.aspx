<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Sys myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        myApp = new Sys();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("7d494fee-ce23-4c47-9579-7191665865f4");
                
                myApp.AddRBACUser();
                break;
            case "renew":
                Public.CheckLogin("b47f8b43-cd62-4afc-8538-9acc6ba2a762");
                
                myApp.EditRBACUser();
                break;
            case "move":
                Public.CheckLogin("3498a173-9641-4bf1-996b-624e944ad209");
                
                myApp.DelRBACUser();
                break;
            case "list":
                Public.CheckLogin("f7fb595e-75cf-4dd2-8557-fadfa5756058");
                
                Response.Write(myApp.GetRBACUsers());
                Response.End();
                break;
            case "renewpassword":
                Public.CheckPrivilege("all");

                myApp.EditPassword();
                break; 
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>