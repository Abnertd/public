<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Logistics myApp = new Logistics();
        string action = Request["action"];
        switch (action)
        {

            case "login":
                myApp.Logistics_Login();
                break;
                
            case "Logout":
                myApp.Logistics_LogOut();
                break;

            case "newprice":
                myApp.AddLogisticsTender();
                break;
                
            case "password":
                myApp.UpdateLogisticsPassword();
                break;

            case "profile":
                myApp.UpdateLogisticsProfile();
                break;
                
            case"dislog_add":
                myApp.AddLogisticsLine();
                break;
                
            case"LogisticMove":
                myApp.DelLogistic();
                break;

    
        }
    }
</script>