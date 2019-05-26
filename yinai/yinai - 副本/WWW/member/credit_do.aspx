<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        ITools tools = ToolsFactory.CreateTools();
        string action = Request["action"];

        switch (action)
        {
            case "loan_apply":
                string orders_sn = tools.CheckStr(Request["orders_sn"]);
                member.Member_Loan_Apply_Ajax(orders_sn);
                Response.End();
                break;
            case "loan_cancel":
                member.Member_Loan_Cancel();
                break;
            case "loan_push":
                member.Member_Loan_Push();
                break;
            case "loan_repayment":
                member.Member_Loan_Repayment();
                break;
        }
    }
</script>
