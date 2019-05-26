<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Notice myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Notice();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("d2658816-1905-471f-935e-c60d4620f4d7");
                
                myApp.AddNotice();
                break;
            case "renew":
                Public.CheckLogin("34e5a2e1-5126-4a1f-ad23-dbe7f9e7528a");
                
                myApp.EditNotice();
                break;
            case "move":
                Public.CheckLogin("2c551863-a2bd-44a8-aef9-512784f0f4a0");
                
                myApp.DelNotice();
                break;
            case "list":
                Public.CheckLogin("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7");
                
                Response.Write(myApp.GetNotices());
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