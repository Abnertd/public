<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private OrdersProcess UOD;
    string city, state, county;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        UOD = new OrdersProcess();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];

        state = tools.CheckStr(Request["state"]);
        city = tools.CheckStr(Request["city"]);
        county = tools.CheckStr(Request["county"]);
        
        switch (action)
        {

            case "memberlist":
                Response.Write(UOD.Member_Select());
                break;
            case "productlist":
                Response.Write(UOD.Product_Select());
                break;
            case "packagelist":
                Response.Write(UOD.Package_Select());
                break;
            case "addproduct":
                UOD.Cart_Product_Add();
                break;
            case "addpackage":
                UOD.Cart_Package_Add();
                break;
            case "changeaddr":
                UOD.Change_Member_Address();
                break;
            case "changedelivery":
                Response.Write(UOD.Delivery_Way_Select(state,city,county));
                break;
            case "changefee":
                Response.Write(Public.DisplayCurrency(tools.CheckFloat(Request["delivery_fee"])));
                break;
            case "changeallprice":
                Response.Write(Public.DisplayCurrency(tools.CheckFloat(Request["delivery_fee"]) + tools.CheckFloat(Session["total_price"].ToString())));
                break;
            case "order_create":
                UOD.Orders_Create();
                break;
            case "checkselectproduct":
                if (UOD.Cart_Product_Count() == 0)
                {
                    Response.Write("请选择想要购买的商品");
                }
                else
                {
                    Response.Write("true");
                }
                
                break;
            case "purchaseorder_create":
                UOD.Purchase_Orders_Create();
                break;  
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        UOD = null;
    }
</script>
