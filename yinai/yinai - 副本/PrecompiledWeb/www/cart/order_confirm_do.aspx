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
        
        string action = Request["action"];
        string SupplyID = tools.NullStr(Request["SupplyID"]);
        member.Member_Login_Check("/cart/order_confirm.aspx");
        //检查购物车数量
        if (cart.My_Cart_Count(SupplyID) == 0)
        {
            Response.Redirect("/cart/my_cart.aspx");
        }
        
        switch (action)
        {
            case "saveorder":
                cart.Orders_Save();
                break;
           
        }
    }
</script>
