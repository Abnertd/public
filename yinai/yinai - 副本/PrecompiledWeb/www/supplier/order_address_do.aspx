<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "address_add":
                supplier.Supplier_Address_Add_back(action);
                break;
            case "address_edit":
                supplier.Supplier_Address_Edit(action);
                break;
            case "address_move":
                supplier.Supplier_Address_Del(action);
                break;
            case "cart_address_add":
                supplier.Supplier_Address_Add(action);
                break;
            case "cart_address_edit":
                supplier.Supplier_Address_Edit(action);
                break;
            case "cart_address_move":
                supplier.Supplier_Address_Del(action);
                break;
            case "check_supplier_address_form":
                supplier.Check_Supplier_Address_Form();
                break;
        }

    }
</script>
