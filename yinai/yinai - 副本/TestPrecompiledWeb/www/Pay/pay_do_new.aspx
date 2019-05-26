<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
        Product product = new Product();
        //supplier.Supplier_Login_Check("/supplier/index.aspx");
        string action = Request["action"];

        switch (action)
        {

            case "contract_pay":
                supplier.Contract_Payment_Action("create");
                break;

            case "chongqian":
                supplier.Contract_Payment_Action1("create");
                break;
           
        }

    }
</script>
