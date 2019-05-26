<%@ Page Language="C#" %>
<%@ Import namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">

    private ITools tools;


    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        int BidID=-1,BidType=-1;
        Bid MyBid = new Bid();
        string action = Request["action"];
        if (action=="bid_isshow")
        {
          
        string Bid_ID = tools.CheckStr(Request["Bid_ID"].ToString());
           BidID = Convert.ToInt32(Bid_ID);
        string Bid_Type=tools.CheckStr(Request["Bid_Type"].ToString());
       BidType=Convert.ToInt32(Bid_Type); 
        }

        switch (action)
        {
 
            case "add":
                MyBid.AddBid(0);
                break;
                
            case "addauction":
                MyBid.AddBid(1);
                break;
            case "edit":
                MyBid.EditBid(0);
                break;
                
            case"editauction":
                MyBid.EditBid(1);
                break;
            case "Release":
                MyBid.ReleaseBid(0);
                break;
                
            case "Revoke":
                MyBid.RevokeBid(0);
                break;
                
            case "addProduct":
                MyBid.AddBidProduct(0);
                break;
                
            case "addAuctionProduct":
                MyBid.AddBidProduct(1);
                break;
                
            case "editProduct":
                MyBid.EditBidProduct(0);
                break;

            case "editAuctionProduct":
                MyBid.EditBidProduct(1);
                break;
                 
            case "move":
                MyBid.DelBidProduct(0);
                break;
            case "moveatt":
                MyBid.DelBidAttachments(0);
                break;
                
                
            case"addAttachments":
                MyBid.AddBidAttachments();
                break;

            case "editAttachments":
                MyBid.EditBidAttachments();
                break;

            case "winadd":
                MyBid.WinBid(0);
                break;
                
                
               //商家以及会员隐藏招标
            case"bid_isshow":
                MyBid.Bid_IsShow(BidID, BidType);
                break;
                
        }
    }
</script>