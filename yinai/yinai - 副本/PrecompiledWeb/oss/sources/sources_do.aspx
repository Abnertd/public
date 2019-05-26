<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Sources myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Sources();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("97e064d7-080e-4c2e-a700-1fc5e7d318fc");

                myApp.AddSources();
                break;
            case "renew":
                Public.CheckLogin("485f0ee2-f5a3-41a0-a778-68a87c5b5d89");
                
                myApp.EditSources();
                break;
            case "move":
                Public.CheckLogin("820c7d2f-a000-4122-858b-ff98a77c7eb1");
                
                myApp.DelSources();
                break;
            case "list":
                Public.CheckLogin("51ce468d-a37a-42dd-9bef-3a0d7ab4eff7");
                
                Response.Write(myApp.GetSourcess());
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