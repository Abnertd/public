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
                Public.CheckLogin("1b86dfa7-32e5-4136-b3d1-a8a670f415ff");
                Response.Write(myApp.KeywordBidding_Keyword_List());
                break;
            case "renew":
                Public.CheckLogin("0f39c533-9740-427f-ae56-649518a414c3");
                myApp.EditKeywordBidding_Keyword();
                break;
            case "remove":
                Public.CheckLogin("daebed75-ab60-46af-bd24-8d7da34f360a");
                myApp.DelKeywordBidding_Keyword();
                break;
        }
    }
</script>