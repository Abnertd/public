<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private MemberGrade myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new MemberGrade();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("603eef98-3a55-46d6-9e8e-81772645adeb");
                
                myApp.AddMemberGrade();
                break;
            case "renew":
                Public.CheckLogin("73df03fa-ef43-486a-9b4c-b9c3e834cbbb");
                
                myApp.EditMemberGrade();
                break;
            case "move":
                Public.CheckLogin("8f0cba35-c84e-4cfb-8ed5-26802004a848");
                
                myApp.DelMemberGrade();
                break;
            case "list":
                Public.CheckLogin("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea");
                
                Response.Write(myApp.GetMemberGrades());
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
