<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    
    private Bid myApp;
    private ITools tools;

    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Bid();
        tools = ToolsFactory.CreateTools();
        int Bid_ID = -1;
        int bid_type = -1;
        string Is_Supplier = "";
        int Tender_ID = 0;
        string action = Request["action"];
        int Type = -1;
        if (action == "movebid")
        {
            string BidID = Request["BidID"];

            Bid_ID = Convert.ToInt32(BidID);

            string bidtype = Request["bidtype"];
            bid_type = Convert.ToInt32(bidtype);
        }





        if (action == "tender_edit")
        {
            string type = tools.CheckStr(Request["Type"].ToString());
            Type = Convert.ToInt32(type);

            string TenderID = tools.CheckStr(Request["Tender_ID"].ToString());
            Tender_ID = Convert.ToInt32(TenderID);
        }



        switch (action)
        {

            case "new":
                Public.CheckLogin("e202397a-bb1e-4e67-b008-67701d37c5cb");
                myApp.AddBid();
                break;

            case "renew":
                Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
                myApp.EditBid(0);
                break;

            case "Release":
                Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
                myApp.ReleaseBid(0);
                break;


            case "newProduct":
                Public.CheckLogin("e202397a-bb1e-4e67-b008-67701d37c5cb");
                myApp.AddBidProduct(0);
                break;

            case "moveProduct":
                Public.CheckLogin("01b227fd-4910-4b5f-a9c0-d0d54a693439");
                myApp.DelBidProduct(0);
                break;

            case "moveAtt":
                Public.CheckLogin("01b227fd-4910-4b5f-a9c0-d0d54a693439");
                myApp.DelBidAttachments(0);
                break;

            case "renewProduct":
                Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
                myApp.EditBidProduct(0);
                break;

            case "newArtt":
                Public.CheckLogin("e202397a-bb1e-4e67-b008-67701d37c5cb");
                myApp.AddBidAttachments();
                break;

            case "renewArtt":
                Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
                myApp.EditBidAttachments();
                break;
            //招标类表
            case "list":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");

                Response.Write(myApp.GetBids(0));
                Response.End();
                break;

            //逻辑删除指定招标
            case "movebid":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");
                myApp.EditBidByID(Bid_ID, bid_type);
                Response.End();
                break;


            //逻辑删除指定招标
            //case "tender_edit":
            case "tender_edit":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");
                myApp.EditTenderByID(Tender_ID, Type);
                Response.End();
                break;


            //拍卖列表
            case "auctionlist":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");

                Response.Write(myApp.GetBids(1));
                Response.End();
                break;
                //  1:通过
            case "Audit":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(1, 0);
                break;
            //  1:不通过
            case "NotAudit":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(2, 0);
                break;

            //新加    3表示冻结
               
            case "Frozen":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(3, 0);
                break;


            case "CancelFrozen":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(1, 0);
                break;

            case "AuctionAudit":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(1, 1);
                break;

            case "AuctionNotAudit":
                Public.CheckLogin("7a95f3d0-c2a4-4daa-98ed-330c8ed15a94");
                myApp.Bid_Audit(2, 1);
                break;

            case "Tenderlist":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");

                Response.Write(myApp.GetTenders(0));
                Response.End();
                break;
            case "AuctionTenderlist":
                Public.CheckLogin("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2");

                Response.Write(myApp.GetTenders(1));
                Response.End();
                break;

            case "winadd":
                Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
                myApp.WinBid(0);
                break;
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
