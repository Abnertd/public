<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        Statistics statistics = new Statistics();
       
        string action = Request["action"];

        switch (action)
        {
            case "account_detail":
                statistics.Member_Account_Detail();
                break;
            case "purchase":
                statistics.Member_PurchaseAmount_Statistics();
                break;
            case "orderscount":
                statistics.Member_OrdersCount_Statistics();
                break;
            case "purchases":
                statistics.Member_Purchases_Statistics();
                break;
        }
    }
</script>
