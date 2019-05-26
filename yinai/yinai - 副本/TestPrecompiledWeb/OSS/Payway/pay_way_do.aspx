<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private PayWay myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new PayWay();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("7950ffdc-827d-4432-a177-eb1d96ad4c3e");
                
                myApp.AddPayWay();
                break;
            case "renew":
                Public.CheckLogin("a47a2618-e716-44e3-95b4-bee4c21c34f3");
                
                myApp.EditPayWay();
                break;
            case "move":
                Public.CheckLogin("efcc1ead-ea67-4186-9fc9-4dca88d56c64");
                
                myApp.DelPayWay();
                break;
            case "list":
                Public.CheckLogin("4484c144-8777-4852-a352-4a89ac5df06f");
                
                Response.Write(myApp.GetPayWays());
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
