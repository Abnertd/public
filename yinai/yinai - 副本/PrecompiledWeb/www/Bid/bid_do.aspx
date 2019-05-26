<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Bid MyBid = new Bid();
        string action = Request["action"];
       
        switch (action)
        {
                
            case "sign_up":
                MyBid.SignUp();
                break;


            case "checkbid_bond":
                MyBid.Check_Bid_Bond();
                break;

            case "BidUpRequireQuickadd":
                MyBid.BidUpRequireQuickAdd();
                break;
                
        }
    }
</script>