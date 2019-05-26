<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Cart cart = new Cart();
        Supplier supplier = new Supplier();
        Member member = new Member();
        Bid MyBid = new Bid();
        string action = Request["action"];
        int BID = tools.NullInt(Request["BID"]);
        member.Member_Login_Check("/cart/Bid_confirm.aspx?BID=" + BID);

        int SupplyID = 0;
        if (!MyBid.BidOrderStatus(BID, ref SupplyID))
        {
            Response.Redirect("/cart/Bid_confirm.aspx?BID=" + BID);
        }
        switch (action)
        {
            case "savebidorder":
                cart.Bid_Orders_Save();
                break;
           
        }
    }
</script>
