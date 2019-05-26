<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">

    ITools tools = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bid MyBid = new Bid();

        string Is_Supplier = "";
        int Tender_ID = 0;
      tools=ToolsFactory.CreateTools();
        
        string action = Request["action"];
        if (action=="auction_tender_edit")
        {
            Is_Supplier = tools.CheckStr(Request["IsSupplier"].ToString());
        

            string TenderID = tools.CheckStr(Request["Tender_ID"].ToString());
            Tender_ID = Convert.ToInt32(TenderID);
        }

        switch (action)
        {
 
            case "add":
                MyBid.AddTender(1);
                break;

            case "auction_tender_edit":
                //MyBid.AddTender(1);
                MyBid.TenderLosDete(Tender_ID, Is_Supplier);
                break;
                
                
        }
    }
</script>