<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private Contract myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Contract();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action) 
        {
            case "new":
                Public.CheckLogin("09c3558e-e719-4f31-b3ba-c6cabae4fbc9");
    
                myApp.AddContractTemplate();
                break;
            case "renew":
                Public.CheckLogin("54d1aadd-ca23-4c1a-b56e-793543423a39");
                
                myApp.EditContractTemplate();
                break;
            case "move":
                Public.CheckLogin("123dda29-e3d2-487a-b91d-22743c18f99e");
                
                myApp.DelContractTemplate();
                break;
            case "list":
                Public.CheckLogin("d4d58107-0e58-485f-af9e-3b38c7ff9672");

                Response.Write(myApp.GetContractTemplates());
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
