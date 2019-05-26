<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier myApp = new Supplier();
        Orders uorder = new Orders();
        tools = ToolsFactory.CreateTools();
        string OrdersID = Request["Orders_ID"];
        int Orders_ID = Convert.ToInt32(OrdersID);
        string action = Request["action"];
        int ISDetails = -1;
        if (action == "save_order")
        {

            ISDetails = tools.CheckInt(Request["ISDetails"].ToString());
        }
        switch (action)
        {
            case "ordersnoteedit":
                myApp.Orders_NoteEdit();
                break;
            case "ordersclose":
                myApp.Orders_Close();
                break;
               //商家确认订单
            case "orderfirm":
             myApp.Orders_Confirm();
              break;
            case "invoice_open":
                myApp.Orders_Invoice_Action("open");
                break;     
            case "invoice_cancel":
                myApp.Orders_Invoice_Action("cancel");
                break;
            case "order_prepare":
                myApp.Orders_Delivery_Prepare();
                break;
            case "order_accept":
                myApp.Orders_Delivery_Accept();
                break;
            case "orderacceptsettle":
                myApp.Orders_Delivery_AcceptSettle();
                break;
            case "create_freight":
                myApp.Orders_Delivery("create");
                break;
            case "cancel_freight":
                myApp.Orders_Delivery("cancel");
                break;
            case "preserve_price":
                myApp.Update_Orders_Price();
                break;
            case "ordergoodsexport":
                myApp.Orders_Goods_Export();
                break;
            case "ordersexport":
                myApp.Orders_Export();
                break;
            case "apply_settling":
                myApp.Orders_Settling_Apply();
                break;
            case "backapply":
                myApp.OrdersBackApplyEdit();
                break;
            case "productinsert":
                myApp.Creat_Orders_Delivery();
                break;
            case "accompanyingadd":
                myApp.Orders_AccompanyingAdd();
                break;
            case "accompanyinglist":
                myApp.Orders_AccompanyingList();
                break;
            case "save_order":
                uorder.Orders_Edit(ISDetails, "save_order");
                //myApp.Orders_Confirm();
                break;

            //隐藏支付失败订单
            case "order_delete":
                uorder.OrdersIsShow_Edit(Orders_ID,true);
                break;


            //隐藏支付失败订单
            case "order_complete":
                uorder.Orders_Complete_Edit(Orders_ID);
                break;
                
                
                
                
                //重新修改成卖家签收
            //卖家签收
            case "orderaccept":
                myApp.Orders_Delivery_Accepts();
                break;


            //卖家修改收货单
            case "orderacceptedit":
                myApp.Orders_Delivery_AcceptEdit();
                break;  

               
        }

    }
</script>