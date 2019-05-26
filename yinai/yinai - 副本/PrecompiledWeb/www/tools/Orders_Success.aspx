<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%

    
    ITools tools = ToolsFactory.CreateTools();
    ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
    Glaer.Trade.B2C.BLL.ORD.IOrders MyBLL = Glaer.Trade.B2C.BLL.ORD.OrdersFactory.CreateOrders();
    Glaer.Trade.B2C.BLL.Product.IProduct MyProduct = Glaer.Trade.B2C.BLL.Product.ProductFactory.CreateProduct();
    
    Orders orders = new Orders();
    
    int orders_id = 0;
    int Product_ID, Goods_Amount, Goods_Type;
    IList<OrdersGoodsInfo> entitys = null;
    
    string sql_order = "SELECT Orders_ID FROM Orders WHERE Orders_PaymentStatus = 4 AND Orders_DeliveryStatus = 2 AND DATEDIFF(hh, Orders_DeliveryStatus_Time, GETDATE()) >= 240";
    SqlDataReader rs = DBHelper.ExecuteReader(sql_order);
    if (rs.HasRows)
    {
        while (rs.Read())
        {
            orders_id = tools.NullInt(rs["orders_id"]);
            DBHelper.ExecuteNonQuery("UPDATE Orders SET Orders_Status = 2 WHERE Orders_ID = " + orders_id + " AND Orders_Status = 1");
            
            entitys = MyBLL.GetGoodsListByOrderID(orders_id);
            if (entitys != null)
            {
                foreach (OrdersGoodsInfo entity in entitys)
                {
                    Product_ID = entity.Orders_Goods_Product_ID;
                    Goods_Amount = entity.Orders_Goods_Amount;
                    Goods_Type = entity.Orders_Goods_Type;

                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                    {
                        MyProduct.UpdateProductSaleAmount(Product_ID, Goods_Amount);
                    }
                }
            }
            entitys = null;
            
            orders.Orders_Log(orders_id, "系统", "完成", "成功", "超过240小时未点完成，系统自动完成");
        }
    }
    rs.Close();
    rs =null; 
    
%>


