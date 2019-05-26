<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Logistics myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Logistics();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("5793c951-d2ee-45f8-9c31-35fcb853819d");
                
                myApp.AddLogistics();
                break;
            case "renew":
                Public.CheckLogin("bd38ff8b-f627-44ec-9275-39c9df7425e1");
                
                myApp.EditLogistics();
                break;
            case "move":
                Public.CheckLogin("dcfc8ade-7987-40c0-8591-a33c2a603e61");
                
                myApp.DelLogistics();
                break;
            case "list":
                Public.CheckLogin("8426b82b-1be6-4d27-84a7-9d45597be557");

                Response.Write(myApp.GetLogisticss());
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