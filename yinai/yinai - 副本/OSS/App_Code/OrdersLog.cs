using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;

/// <summary>
///OrdersLog 的摘要说明
/// </summary>
public class OrdersLog
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IOrdersLog  MyBLL;

    public OrdersLog()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = OrdersLogFactory.CreateOrdersLog();
    }

    public string GetOrdersLogsByOrdersID(int Orders_ID) {
        string strHTML = "";
       IList<OrdersLogInfo> LogInfoList = MyBLL.GetOrdersLogsByOrdersID(Orders_ID);
        strHTML += "<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">";
        strHTML += "    <tr class=\"list_head_bg\">";
        strHTML += "        <td width=\"60\">序号</td>";
        strHTML += "        <td width=\"130\">时间</td>";
        strHTML += "        <td>操作人</td>";
        strHTML += "        <td>备注</td>";
        strHTML += "    </tr>";
        if (LogInfoList != null) {
            int ICount = 1;
            foreach (OrdersLogInfo entity in LogInfoList) {
                strHTML += "    <tr class=\"list_td_bg\">";
                strHTML += "        <td>" + ICount + "</td>";
                strHTML += "        <td class=\"info_date\">" + entity.Orders_Log_Addtime + "</td>";
                if (entity.Orders_Log_Operator == "")
                {
                    strHTML += "        <td>用户</td>";
                }
                else
                {
                    strHTML += "        <td>" + entity.Orders_Log_Operator + "</td>";
                }
                strHTML += "        <td align=\"left\">" + entity.Orders_Log_Remark + "</td>";
                strHTML += "    </tr>";
                ICount += 1;
            }
        }
        strHTML += "</table>";
        return strHTML;
    }

    public void Orders_Log(int orders_id, string log_operator, string log_action, string log_result, string log_remark)
    {
        OrdersLogInfo orderslog = new OrdersLogInfo();
        orderslog.Orders_Log_ID = 0;
        orderslog.Orders_Log_Operator = log_operator;
        orderslog.Orders_Log_OrdersID = orders_id;
        orderslog.Orders_Log_Action = log_action;
        orderslog.Orders_Log_Remark = log_remark;
        orderslog.Orders_Log_Result = log_result;
        orderslog.Orders_Log_Addtime = DateTime.Now;
        MyBLL.AddOrdersLog(orderslog);
    }

}
