<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Config myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a2f95df4-346a-47b2-a112-1f8e3f062298");
        myApp = new Config();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddConfig();
                break;
            case "renew":
                myApp.EditConfig();
                break;
            case "move":
                myApp.DelConfig();
                break;
            case "list":
                Response.Write(myApp.GetConfigs());
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