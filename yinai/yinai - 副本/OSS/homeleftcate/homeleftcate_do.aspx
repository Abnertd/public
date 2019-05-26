<%@ Page Language="C#" %>

<script runat="server">
    
    private HomeLeftCate myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        
        myApp = new HomeLeftCate();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("8738cd22-6808-4fdd-94f4-d9bb51b64509");
                myApp.AddHomeLeftCate();
                break;
            case "renew":
                Public.CheckLogin("de88931b-4a5b-4bb7-8f68-4975ad26e59c");
                myApp.EditHomeLeftCate();
                break;
            case "move":
                Public.CheckLogin("4196e669-c4c0-4209-bbed-bc99a951c2c8");
                myApp.DelHomeLeftCate();
                break;
            case "list":
                Public.CheckLogin("d843afda-7680-45fa-bc00-32278bf77ae8");
                Response.Write(myApp.GetHomeLeftCates());
                Response.End();
                break;
            case "inputcate":
                Public.CheckLogin("8738cd22-6808-4fdd-94f4-d9bb51b64509");
                myApp.HomeLeftCate_Input();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
