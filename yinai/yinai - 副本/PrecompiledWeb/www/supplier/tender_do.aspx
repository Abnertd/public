<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">

    ITools tools = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bid MyBid = new Bid();

        string Is_Supplier = "";
        int Tender_ID = 0;
        
        string action = Request["action"];
        tools = ToolsFactory.CreateTools();

      
        if (action == "tender_edit")
        {
            Is_Supplier = tools.CheckStr(Request["IsSupplier"].ToString());
            //Tender_SupplierID = Convert.ToInt32(Tender_Supplier_ID);

            string TenderID = tools.CheckStr(Request["Tender_ID"].ToString());
            Tender_ID = Convert.ToInt32(TenderID);
        }
            
      
        switch (action)
        {
 
            case "add":
                MyBid.AddTender(0);
                break;
                
            case"AddProduct":
                MyBid.AddProduct();
                break;

            case "tender_edit":
                //MyBid.AddTender(1);
                MyBid.TenderLosDete(Tender_ID, Is_Supplier);
                break;    
                
                
            //case"movetender":
            //    MyBid.EditBid
                
                
        }
    }
</script>