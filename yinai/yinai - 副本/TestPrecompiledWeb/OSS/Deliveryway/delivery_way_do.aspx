<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private DeliveryWay myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new DeliveryWay();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("8632585c-0447-4003-a97d-48cade998a05");
                
                myApp.AddDeliveryWay();
                break;
            case "renew":
                Public.CheckLogin("58d92d67-4e0b-4a4c-bd5c-6062c432554d");
                
                myApp.EditDeliveryWay();
                break;
            case "move":
                Public.CheckLogin("9909b492-b55c-49bf-b726-0f2d36e7ff4b");
                
                myApp.DelDeliveryWay();
                break;
            case "list":
                Public.CheckLogin("837c9372-3b25-494f-b141-767e195e3c88");
                
                Response.Write(myApp.GetDeliveryWays());
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
