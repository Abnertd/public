<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    
    private ZhongXin myApp;
    private ITools tools;
    private int idInt;

    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        myApp = new ZhongXin();
        string action = Request["action"];
        
        switch (action)
        {
            case "renew":
                Public.CheckLogin("all");
                myApp.EditZhonghang();
                break;
            case "tuisong":
                Public.CheckLogin("all");
                myApp.AccountSign();
                break;
            case "list":
                Public.CheckLogin("all");
                Response.Write(myApp.GetZhongXins());
                break;
            case "subaccount":
                Public.CheckLogin("all");
                myApp.subAccountEdit();
                break;
            case "editRegister":
                Public.CheckLogin("all");
                idInt = Convert.ToInt32(Request["ID"]);
               myApp.RegistZhonghang(idInt);
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
