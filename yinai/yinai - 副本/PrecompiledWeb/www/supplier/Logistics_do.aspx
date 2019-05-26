<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Logistics MyApp = new Logistics();
        string action = Request["action"];
        switch (action)
        {

            case"new":
                MyApp.AddSupplierLogistics();
                break;
                
            case "winadd":
                MyApp.WinLogistics();
                break;
        }
    }
</script>