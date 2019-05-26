<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "login":
                supplier.Supplier_Login();
                break;
            case "fast_login":
                supplier.Supplier_FastLogin();
                break;
            case "logout":
                supplier.Supplier_LogOut();
                break;
            //case "getpass":
            //    supplier.supplier_getpass_sendmail();
            //    break;
            case "verify":
                supplier.supplier_getpass_verify();
                break;
            case "resetpass":
                supplier.supplier_getpass_resetpass();
                break;



          
        }
    }
</script>