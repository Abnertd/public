<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private SysState myApp;
    private SysCity MyCity;
    private SysCounty MyCounty;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        myApp = new SysState();
        MyCity = new SysCity();
        MyCounty = new SysCounty();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "statelist":
                Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
                Response.Write(myApp.GetSysStates());
                Response.End();
                break;
             
            case "statenew":
                Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
                myApp.AddSysState();
                break;
                
            case "staterenew":
                Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
                myApp.EditSysState();
                break;
                
            case "statemove":
                Public.CheckLogin("6364a1f1-8b6d-43cb-a2eb-268ff86b4840");
                myApp.DelSysState();
                break;

            case "citylist":
                Public.CheckLogin("09598cde-07da-480c-9ac1-e0b701187954");
                Response.Write(MyCity.GetSysCitys());
                break;
            case "cityrenew":
                Public.CheckLogin("09598cde-07da-480c-9ac1-e0b701187954");
                MyCity.EditSysCity();
                break;

            case "citynew":
                Public.CheckLogin("09598cde-07da-480c-9ac1-e0b701187954");
                MyCity.AddSysCity();
                break;
                
            case "citymove":
                Public.CheckLogin("09598cde-07da-480c-9ac1-e0b701187954");
                MyCity.DelSysCity();
                break;

            case "countylist":
                Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
                Response.Write(MyCounty.GetSysCountys());
                break;
            case "countynew":
                Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
                MyCounty.AddSysCounty();
                break;
            case "countyrenew":
                Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
                MyCounty.EditSysCounty();
                break;
            case "countymove":
                Public.CheckLogin("23d8b0bc-4121-47f8-9010-da4bbeff07e0");
                MyCounty.DelSysCounty();
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
