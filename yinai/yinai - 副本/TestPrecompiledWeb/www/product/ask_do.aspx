<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>



<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ITools tools = ToolsFactory.CreateTools();
        Product product = new Product();
        CMS cms = new CMS();
        string action = Request["action"];
        switch (action)
        {
            case "ask_add":
                product.Shopping_Ask_Add();
                break;
            case "editamount":
                int amount = tools.CheckInt(Request["amount"]);
                amount = amount + tools.CheckInt(Request["target"]);
                if (tools.CheckInt(Request["amount"]) > tools.CheckInt(Request["maxamount"]))
                {
                    amount = tools.CheckInt(Request["maxamount"]);
                }
                
                if (amount < 1)
                {
                    amount = 1;
                }
                Response.Write(amount);
                break;
           // case "bidding":
           //     product.BiddingAccounting(tools.CheckInt(Request.QueryString["KeywordBidding_ID"]), tools.CheckInt(Request.QueryString["Supplier_ID"]));
           //     break;
           // case "shopkey":
           //     product.GetSupplierShopsNameList();
           //     break;
           // case "helpkey":
           //     cms.HelpNameList();
           //     break;
           // case "productkey":
           //     //Response.Write(Request.QueryString.ToString());    
           //     product.GetProductNameList();
           //     break;
           // case "getstate":
           //     Response.Write(product.Delivery_Province_Select(tools.CheckInt(Request["isnofreight"])));
           //     break;
           // case "getcity":
           //     Response.Write(product.Delivery_City_Select(tools.CheckInt(Request["isnofreight"])));
           //     break;
           // case "leftad":
           //     Response.Cookies["leftad"].Value = DateTime.Now.ToString();
           //     Response.Cookies["leftad"].Expires = DateTime.Now.AddMonths(1);
           //     break;
           //case "rightad":
           //     Response.Cookies["rightad"].Value = DateTime.Now.ToString();
           //     Response.Cookies["rightad"].Expires = DateTime.Now.AddMonths(1);
           //     break;
           //case "layoutad":
           //     Response.Cookies["layoutad"].Value = DateTime.Now.ToString();
           //     Response.Cookies["layoutad"].Expires = DateTime.Now.AddMonths(1);
           //     break;
           // case "getdeliveryfee":
           //     Response.Write(product.Get_City_DeliveryFee(tools.CheckStr(Request["city"]), tools.CheckInt(Request["isnofreight"]), tools.CheckInt(Request["supplier_id"])));
           //     break;
        }

    }
</script>

