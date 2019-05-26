<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Pay pay = new Pay();
        ITools tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        string request_no = tools.NullStr(Session["request_no"]);
        string outer_trade_no = tools.CheckStr(Request["orders_sn"]);

        switch (action)
        {
            case "cancel_trade":
                pay.Cancel_Trade(request_no, outer_trade_no, "支付失败");
                break;
            case "orders_loanapply":
                pay.Loan_Apply();
                break;
        }
    }
</script>
