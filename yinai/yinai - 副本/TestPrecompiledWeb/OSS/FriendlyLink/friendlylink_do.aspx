<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private FriendlyLink myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new FriendlyLink();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5");
                
                myApp.AddFriendlyLink();
                break;
            case "renew":
                Public.CheckLogin("363bfb90-0d0b-42ae-af25-54004fd061e3");
                
                myApp.EditFriendlyLink();
                break;
            case "move":
                Public.CheckLogin("be7c3360-d8c7-4343-8171-4a54a85ca5a5");
                
                myApp.DelFriendlyLink();
                break;
            case "list":
                Public.CheckLogin("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5");
                
                Response.Write(myApp.GetFriendlyLinks());
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