<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private RBACResource myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("7e0cb63c-935a-4414-a0bb-5c1b7e259d92");
        myApp = new RBACResource();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddRBACResource();
                break;
            case "renew":
                myApp.EditRBACResource();
                break;
            case "move":
                myApp.DelRBACResource();
                break;
            case "list":
                Response.Write(myApp.GetRBACResources());
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