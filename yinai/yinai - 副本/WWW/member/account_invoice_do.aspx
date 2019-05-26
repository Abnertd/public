<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        member.Member_Login_Check("/supplier/index.aspx");
        string action = Request["action"];

        switch (action)
        {
            case "Invoice_add":
                member.Member_Invoice_Add();
                break;
            case "editinvoice":
                member.Member_Invoice_Edit();
                break;
            case "moveinvoice":
                member.Member_Invoice_Del();
                break;
        }
    }
</script>
