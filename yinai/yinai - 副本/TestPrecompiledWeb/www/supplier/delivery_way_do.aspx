<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/index.aspx");
        string action = Request["action"];
        
        switch (action)
        {
            case "new":
                supplier.AddDeliveryWay();
                break;
            case "renew":
                supplier.EditDeliveryWay();
                break;
            case "del":
                supplier.DelDeliveryWay();
                break;
            case "add":
                supplier.AddDeliveryWayDistrict();
                break;
            case "districtdel":
                supplier.DelDeliveryWayDistrict();
                break;
        }

    }
</script>
