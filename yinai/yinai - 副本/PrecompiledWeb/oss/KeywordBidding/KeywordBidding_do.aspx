<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        KeywordBidding myApp = new KeywordBidding();
        string working = Request["action"];
        switch (working) { 
            case "list":
                Public.CheckLogin("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71");
                Response.Write(myApp.KeywordBidding_List());
                break;
            case "audit":
                Public.CheckLogin("1cbc86b5-d248-4553-88c9-4629438c2464");
                myApp.AuditKeywordBidding(1);
                break;
            case "unaudit":
                Public.CheckLogin("1cbc86b5-d248-4553-88c9-4629438c2464");
                myApp.AuditKeywordBidding(2);
                break;
            case "remove":
                Public.CheckLogin("cc52c0d7-188d-4915-955a-7e0857e958bc");
                myApp.DelKeywordBidding();
                break;
        }
    }
</script>