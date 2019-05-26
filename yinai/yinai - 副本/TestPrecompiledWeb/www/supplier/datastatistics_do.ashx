<%@ WebHandler Language="C#" Class="datastatistics_do" %>

using System;
using System.Web;
using Glaer.Trade.Util.Tools;

public class datastatistics_do : IHttpHandler, System.Web.SessionState.IRequiresSessionState{

    ITools tools;
    SupplierStatistics statistics;

    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        tools = ToolsFactory.CreateTools();
        statistics = new SupplierStatistics();
        
        switch (context.Request["action"])
        {
            case "salesprice":
                statistics.SalesPrice();
                break;
            case "salesamount":
                statistics.SalesAmount();
                break;
            case "salesranking":
                context.Response.Write(statistics.SalesAmountRanked());
                break;
        }
        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}