<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private FriendlyLinkCate myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("0a9f21bd-88cb-4121-94b8-f865a9de2c3b");
        
        myApp = new FriendlyLinkCate();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddFriendlyLinkCate();
                break;
            case "renew":
                myApp.EditFriendlyLinkCate();
                break;
            case "move":
                myApp.DelFriendlyLinkCate();
                break;
            case "list":
                Response.Write(myApp.GetFriendlyLinkCates());
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