<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Help myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        myApp = new Help();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("e64214dc-33e4-4576-bab9-deb7802bad6c");
                
                myApp.AddHelp();
                break;
            case "renew":
                Public.CheckLogin("14422eb0-8367-45e1-b955-c40aee162094");
                
                myApp.EditHelp();
                break;
            case "move":
                Public.CheckLogin("c8585704-c4d5-40e8-8f5c-89940b5d7dfc");
                
                myApp.DelHelp();
                break;
            case "list":
                Public.CheckLogin("a015e960-173c-429d-98d2-69e5a023b5dc");
                
                Response.Write(myApp.GetHelps());
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