<%@ Page Language="C#" %>

<script runat="server">
    
    private SCMDepot myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SCMDepot();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("db79d1a3-cb45-46b2-bf9c-a7f35de307d7");
                
                myApp.AddDepot();
                break;
            case "renew":
                Public.CheckLogin("8fad33fd-ec7f-47e4-bb94-0aca729a5e46");
                
                myApp.EditDepot();
                break;
            case "move":
                Public.CheckLogin("0a198c2c-1dda-4ead-b195-c4083a7cc9fd");
                
                myApp.DelDepot();
                break;
            case "list":
                Public.CheckLogin("39e690a8-dd52-4644-a5f2-3ad4f7edb504");
                
                Response.Write(myApp.GetDepots());
                Response.End();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
