<%@ Page Language="C#" %>

<script runat="server">
    
    private SCMSupplier myApp;
    private Feedback feedback;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SCMSupplier();
        feedback = new Feedback();
        
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("83c81946-5b6d-48cf-9e5a-17b4807d18ff");

                myApp.AddSupplier();
                break;
            case "renew":
                Public.CheckLogin("3306c908-ff91-4e6e-8c46-8157cd5b6e4a");

                myApp.EditSupplier();
                break;
            case "move":
                Public.CheckLogin("21de0894-85a2-4719-8088-774f1a815f43");

                myApp.DelSupplier();
                break;
            case "list":
                Public.CheckLogin("eb0180d5-4df5-4988-87ec-d6ac4e98fc8f");
                
                Response.Write(myApp.GetSuppliers());
                Response.End();
                break;
            case "feedbackmove":
                Public.CheckLogin("cc567804-3e2e-4c6c-aa22-c9a353508074");

                feedback.FeedBack_Del();
                Response.End();
                break;
           
                
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
    }
</script>
