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
            
            case "list":
                Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
                Response.Write(myApp.GetMemberConsumptions());
                Response.End();
                break;
            case "accountlist":
                Public.CheckLogin("all");
                Response.Write(myApp.GetMemberAccountLogs());
                Response.End();
                break;
            case "coin_process":
                Public.CheckLogin("833b9bdd-a344-407b-b23a-671348d57f76");
                myApp.Member_Coin_Process();
                break;
            case "account_process":
                Public.CheckLogin("all");
                myApp.Member_Account_Process();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
