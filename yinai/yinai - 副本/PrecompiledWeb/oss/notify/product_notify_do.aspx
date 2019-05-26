<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private ProductNotify myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ProductNotify();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "send_notify":
                Public.CheckLogin("98dd8cbd-8ea7-4a59-89ec-988f149c16bb");
                
                myApp.ProductNotifySend();
                break;
            case "move":
                Public.CheckLogin("d5183803-ddfa-4a0b-8319-bed75950a08c");

                myApp.DelProductNotify();
                break;
            case "list":
                Public.CheckLogin("e996b26f-2c14-482f-b5f6-955f38b50b3f");
                
                Response.Write(myApp.GetProductNotifys());
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
