<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "new":
                Public.CheckLogin("e0920e95-65fa-4e3c-9dd6-2794ccc45782");

                supplier.AddSupplierAgentProtocal();
                break;
            case "renew":
                Public.CheckLogin("7abc095a-d322-4312-861c-aecb6088c3bb");

                supplier.EditSupplierAgentProtocal(0);
                break;
            case "audit":
                Public.CheckLogin("03ea4ec5-4a1d-4e02-9b91-4cca4e3b4200");
                supplier.EditSupplierAgentProtocal(2);
                break;
            case "list":
                Public.CheckLogin("0aab7822-e327-4dcd-bc30-4cbf289067e4");
                Response.Write(supplier.GetSupplierAgentProtocal_list());
                Response.End();
               break ;
        }

    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
