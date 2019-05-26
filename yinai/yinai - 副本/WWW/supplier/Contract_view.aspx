<%@ Page Language="C#" %>
<% Contract MyApp = new Contract();
   Orders orders=new Orders ();
   Public_Class pub = new Public_Class();
   Glaer.Trade.Util.Tools.Tools tools=new Glaer.Trade.Util.Tools.Tools ();
  // Glaer.Trade.B2C.BLL.ORD.IOrders iorders=Glaer.Trade.B2C.BLL.ORD.OrdersFactory.CreateOrders();
   int orders_id = 0;
  string orders_sn ="";
 int contract_id=  tools.CheckInt( Request["contract_id"].ToString());
 Glaer.Trade.B2C.Model.OrdersInfo ordersinfo = null;
 if (contract_id>0)
 {
     orders_id = orders.getOrdersIDByContractID(contract_id);
     if (orders_id>0)
     {
    
          ordersinfo = orders.GetOrdersByID(orders_id);
         if (ordersinfo!=null)
         {
             orders_sn = ordersinfo.Orders_SN;
         }
     }
 }
 else
 {
     pub.Msg("error", "错误信息", "记录不存在", false, "/member/Order_Contract_View.aspx?orders_sn=" + orders_sn + "");
 }
 
 

 //Glaer.Trade.B2C.Model.OrdersInfo ordersinfo = orders.GetOrdersBySn(orders_sn);
 
 if (ordersinfo == null)
 {
     pub.Msg("error", "错误信息", "记录不存在", false, "/member/Order_Contract_View.aspx?orders_sn=" + orders_sn + "");
 }
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合同查看</title>

</head>
<body>

    <% MyApp.Contract_View_SupplierNew(ordersinfo, contract_id);
       if (Request["action"] != "print")
       {
           Response.Write("<style type=\"text/css\">td{font-size:14px;}div{font-size:14px;}.bill td{line-height:20px;font-size:14px;}</style>");
       }
        %>
        <script type="text/javascript">            pagesetup_null();</script>
</body>
</html>
