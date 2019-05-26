<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private About myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new About();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("c747b411-cf59-447b-a2d7-7e5510589f25");
                
                myApp.AddAbout();
                break;
            case "renew":
                Public.CheckLogin("b15dd1c4-d9c5-4b09-b7c2-3ef4d24af7ef");
                
                myApp.EditAbout();
                break;
            case "move":
                Public.CheckLogin("622c8cf4-0cae-47f7-bd02-19bd8b5c169d");
                
                myApp.DelAbout();
                break;
            case "list":
                Public.CheckLogin("db8de73b-9ac0-476e-866e-892dd35589c5");
                
                Response.Write(myApp.GetAbouts());
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