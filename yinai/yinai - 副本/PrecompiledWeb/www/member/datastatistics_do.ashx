<%@ WebHandler Language="C#" Class="datastatistics_do" %>

using System;
using System.Web;
using Glaer.Trade.Util.Tools;

public class datastatistics_do : IHttpHandler, System.Web.SessionState.IRequiresSessionState{

    ITools tools;
    Statistics statistics;

    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        tools = ToolsFactory.CreateTools();
        statistics = new Statistics();
        
        switch (context.Request["action"])
        {
            case "accounts":
                statistics.Member_Account_Detail();
                break;
            case "purchase":
                statistics.Member_PurchaseAmount_Statistics();
                break;
            case "orderscounts":
                statistics.Member_OrdersCount_Statistics();
                break;
            case "purchaseranking":
                statistics.Member_Purchases_Statistics();
                break;

            case "HotKey":
                statistics.TradeIndex_Charts();
                break;
        }
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}