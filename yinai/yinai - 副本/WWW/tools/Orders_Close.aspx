<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.Util.SQLHelper" %>
<%
    string sql_order = "";
    int orders_id = 0;
    ITools tools;
    tools = ToolsFactory.CreateTools();
    sql_order = "select orders_id from orders where Orders_Payway in (select Pay_Way_ID from pay_way where Pay_Way_Cod=0) and Orders_PaymentStatus=0 and Datediff(hh,orders_addtime,GETDATE())>=72";
    ISQLHelper DBHelper;
    Orders orders = new Orders();
    DBHelper = SQLHelperFactory.CreateSQLHelper();
    SqlDataReader rs = DBHelper.ExecuteReader(sql_order);
    if (rs.HasRows)
    {
        while (rs.Read())
        {
            orders_id = tools.NullInt(rs["orders_id"]);
            DBHelper.ExecuteNonQuery("update orders set orders_status=3,Orders_Fail_Note='超过72小时未付款，系统自动取消' where orders_id=" + orders_id);
            orders.Orders_Log(orders_id, "系统", "取消", "成功", "超过72小时未付款，系统自动取消");
        }
    }
    rs.Close();
    rs =null; 
    
%>