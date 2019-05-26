<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private NoticeCate myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new NoticeCate();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("8c732f78-5431-41c6-a3db-68787f36c223");
                
                myApp.AddNoticeCate();
                break;
            case "renew":
                Public.CheckLogin("705ff0e0-daa6-4649-bf27-20142c21ba9e");
                
                myApp.EditNoticeCate();
                break;
            case "move":
                Public.CheckLogin("e2e67cd1-dd5c-4c63-962a-fdbd0d7dc6a8");
                
                myApp.DelNoticeCate();
                break;
            case "list":
                Public.CheckLogin("fb3e87ba-3d4d-480d-934e-80048bcc0100");
                
                Response.Write(myApp.GetNoticeCates());
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