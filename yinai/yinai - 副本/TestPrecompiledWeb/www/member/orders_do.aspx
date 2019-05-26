<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    { 
        
        Member myApp = new Member();
        Product product = new Product();
        Orders uorder = new Orders();
        int Orders_Delivery_ID = 0  ;
           string  Orders_DeliveryID="";
          
       
        string action = Request["action"];
        tools=ToolsFactory.CreateTools();
        if (action=="tuihuo")
        {
            Orders_Delivery_ID = tools.CheckInt(Request["Orders_Delivery_ID"]);
            
            Orders_DeliveryID=Orders_Delivery_ID.ToString();
        }
       
        string OrdersID = tools.CheckStr(Request["Orders_ID"]);
        int Orders_ID = 0;
        if (OrdersID.ToString().Length>0 )
        {
             //Orders_ID = Convert.ToInt32(OrdersID.ToString());
            Orders_ID = Convert.ToInt32(OrdersID.ToString());
        }
      
        switch (action)
        {
            case "ordersnoteedit":
                myApp.Orders_NoteEdit();
                break;
            case "ordersclose":
                myApp.Orders_Close();
                break;
            case "orderfirm":
                myApp.Orders_Confirm();
                break;
                //买家签收
            case "orderaccept":
                myApp.Orders_Delivery_Accept();
                break;  
                //买家修改收货单
                     case "orderacceptedit":
                myApp.Orders_Delivery_AcceptEdit();
                break; 
            case "contractcreate":
                myApp.Orders_Contract_Create();
                break;
            case "review_add":
                product.AddProductReview();
                break;
            case "save_order":

                uorder.Orders_Edit(1);
                break;
            //case "renew":
            //    cart.Cart_ProductEdit(goods_id, product_id, buy_amount);
            //    break;
                //隐藏支付失败订单
            case "order_delete":
                uorder.OrdersIsShow_Edit(Orders_ID,false);
                break;
                //确认结算
            case "orderacceptsettle":
                myApp.Orders_Delivery_AcceptSettle();
                break;
                
                

            //退货
            case "returngoods":
                uorder.Orders_Edit(Orders_ID, false);
                break;
                
                
                
                
        }

    }
</script>