<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private DeliveryWay myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("58d92d67-4e0b-4a4c-bd5c-6062c432554d");

        myApp = new DeliveryWay();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                myApp.AddDeliveryWayDistrict();
                break;
            case "renew":
                myApp.EditDeliveryWayDistrict();
                break;
            case "move":
                myApp.DelDeliveryWayDistrict();
                break;
            case "list":
                Response.Write(myApp.GetDeliveryWayDistricts());
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
