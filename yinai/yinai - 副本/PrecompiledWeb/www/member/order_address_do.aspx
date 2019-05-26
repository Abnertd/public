<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        string action = Request["action"];
        switch (action)
        {
            case "address_add":
                member.Member_Address_Add_back(action);
                break;
            case "address_edit":
                member.Member_Address_Edit(action);
                break;
            case "address_move":
                member.Member_Address_Del(action);
                break;
            case "cart_address_add":
                member.Member_Address_Add(action);
                break;
            case "cart_address_edit":
                member.Member_Address_Edit(action);
                break;
            case "cart_address_move":
                member.Member_Address_Del(action);
                break;
            case "check_supplier_address_form":
                member.Check_Member_Address_Form();
                break;
        }

    }
</script>
