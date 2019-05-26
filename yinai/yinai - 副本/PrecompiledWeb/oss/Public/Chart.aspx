<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%
    ITools tools = ToolsFactory.CreateTools();
    Charts charts = new Charts();
	string ChartType = Request["Type"].ToString().ToLower();
    int intWidth = tools.CheckInt(Request.QueryString["width"]);
    int intHeight = tools.CheckInt(Request.QueryString["height"]);
    string orders_status = "";
    DateTime startDate = DateTime.Today;
    DateTime endDate = DateTime.Today;

    switch (ChartType) { 
        case "index":
            charts.SaleChart(intWidth, intHeight);
            break;
        case "seleamountimg":

            startDate = Public.ChangeToDate(Request.QueryString["startdate"]);
            endDate = Public.ChangeToDate(Request.QueryString["enddate"]);
            orders_status = tools.CheckStr(Request.QueryString["orders_status"]);

            charts.SaleChart(intWidth, intHeight, startDate, endDate, orders_status, "amount");
            break;
            
        case "ordersamountimg":
            
            startDate = Public.ChangeToDate(Request.QueryString["startdate"]);
            endDate = Public.ChangeToDate(Request.QueryString["enddate"]);
            orders_status = tools.CheckStr(Request.QueryString["orders_status"]);
            charts.SaleChart(intWidth, intHeight, startDate, endDate, orders_status, "orders");
            break;
    }
    
    charts = null;
	
%>
