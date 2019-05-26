<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Member myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Member();

        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "cert_add":
                myApp.AddMemberCert();
                Response.End();
                break;
            case "cert_edit":
                myApp.EditMemberCert();
                Response.End();
                break;
            case "cert_del":
                myApp.DelMemberCert();
                Response.End();
                break;
            case "cert_list":
                Response.Write(myApp.GetMemberCerts());
                Response.End();
                break;
        }
    }
</script>
