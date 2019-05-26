<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay = new Pay();

    string pay_payment = Request["notify_type"];

    if (pay_payment == "trade_status_sync")
    {
        pay.VFINANCE_Trade_Status_Sync();
    }

    if (pay_payment == "refund_status_sync")
    {
        pay.VFINANCE_Refund_Status_Sync();
    }

    if (pay_payment == "transfer_status_sync")
    {
        pay.VFINANCE_Transfer_Status_Sync();
    }
    
 %>

