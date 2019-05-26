<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private HelpCate myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new HelpCate();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("e98d5669-6825-47e0-9f0b-d5c6af91f72e");
                
                myApp.AddHelpCate();
                break;
            case "renew":
                Public.CheckLogin("a0059a41-e628-4625-a67a-9da2f8b20fe1");

                myApp.EditHelpCate();
                break;
            case "move":
                Public.CheckLogin("b14f283a-740b-48e1-b243-60105b87a9a6");
                
                myApp.DelHelpCate();
                break;
            case "list":
                Public.CheckLogin("e2e6aec7-ff11-407b-9c3a-6317b06b1a7e");
                
                Response.Write(myApp.GetHelpCates());
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