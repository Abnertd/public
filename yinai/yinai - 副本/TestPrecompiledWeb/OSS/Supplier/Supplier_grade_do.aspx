<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private SupplierGrade myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new SupplierGrade();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("a9ed67d8-d6b1-4518-87ac-f01dadaba761");
                
                myApp.AddSupplierGrade();
                break;
            case "renew":
                Public.CheckLogin("065594cf-5094-4ce6-b753-3c360d3213bd");
                
                myApp.EditSupplierGrade();
                break;
            case "move":
                Public.CheckLogin("3c4246f5-0b23-4c6e-8c73-65c14a2a76bc");
                
                myApp.DelSupplierGrade();
                break;
            case "list":
                Public.CheckLogin("1d3f7ace-2191-4c5e-9403-840ddaf191c0");
                
                Response.Write(myApp.GetSupplierGrades());
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
