<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private PayType myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new PayType();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("4f0daa32-ae16-4398-ae1e-d2400ca0fff0");
                
                myApp.AddPayType();
                break;
            case "renew":
                Public.CheckLogin("fcd3782c-b791-40c6-a29d-9b43092de04f");
                
                myApp.EditPayType();
                break;
            case "move":
                Public.CheckLogin("52b0dc84-893a-4f1d-a15d-2023250ac8a6");
                
                myApp.DelPayType();
                break;
            case "list":
                Public.CheckLogin("80924a02-c37c-409b-ac63-43d7d4340fc5");
                
                Response.Write(myApp.GetPayTypes());
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
