<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">

   private ITools tools ;

    protected void Page_Load(object sender, EventArgs e)
    {
        int BidID = -1;
        int BidType = -1;
        int TenderID = -1;
        Bid MyBid = new Bid();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        
        if ((action == "tender_isshow") || (action == "winadd1"))
        {
            string Bid_ID = tools.CheckStr(Request["Bid_ID"].ToString());
           BidID = Convert.ToInt32(Bid_ID);
            string Bid_Type = tools.CheckStr(Request["Bid_Type"].ToString());
            BidType = Convert.ToInt32(Bid_Type);
            if ((action == "winadd1"))
            {
                string Tender_ID = tools.CheckStr(Request["TenderID"].ToString());
                TenderID = Convert.ToInt32(Tender_ID);
            }
         
            
        
        }
      
       

        switch (action)
        {
 

                
            case "addauction":
                MyBid.AddBid(1);
                break;
                
            case"editauction":
                MyBid.EditBid(1);
                break;
            case "Release":
                MyBid.ReleaseBid(1);
                break;
                
            case "Revoke":
                MyBid.RevokeBid(1);
                break;
                

                
            case "addAuctionProduct":
                MyBid.AddBidProduct(1);
                break;
                


            case "editAuctionProduct":
                MyBid.EditBidProduct(1);
                break;
                 
            case "move":
                MyBid.DelBidProduct(1);
                break;
            case "moveatt":
                MyBid.DelBidAttachments(1);
                break;
                
                
            case"addAttachments":
                MyBid.AddBidAttachments();
                break;

            case "editAttachments":
                MyBid.EditBidAttachments();
                break;
                //拍卖标识1
            case "winadd":
                MyBid.WinBid(1);
                break;

//中标 拍卖招标详情页面
            case "winadd1":
                MyBid.WinBid1(BidType, BidID, TenderID);
                break;



            //商家以及会员隐藏招标
            case "tender_isshow":
                MyBid.Bid_IsShow(BidID, BidType);
                break;
                
        }
    }
</script>