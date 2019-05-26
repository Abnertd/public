<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        
        string action = Request["action"];
        
        supplier.Supplier_Login_Check("/supplier/feedback.aspx");
        
        
        switch (action)
        {
            case "add":
                supplier.AddFeedBack(1);
                break;

            case "shoppingask_reply":
                supplier.EditShoppingAsk();
                break;
        }

    }
</script>
