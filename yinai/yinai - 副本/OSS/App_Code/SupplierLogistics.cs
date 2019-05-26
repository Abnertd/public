using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// SupplierLogistics 的摘要说明
/// </summary>
public class SupplierLogistics
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ISupplierLogistics MyBLL;
    private ITools tools;
    private Addr Addr;
    private ILogisticsTender MyLogisticsTender;
    private Logistics MyLogistics;
	public SupplierLogistics()
	{
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBLL = SupplierLogisticsFactory.CreateSupplierLogistics();
        tools = ToolsFactory.CreateTools();
        Addr = new Addr();
        MyLogisticsTender = LogisticsTenderFactory.CreateLogisticsTender();
        MyLogistics = new Logistics();
	}


    public SupplierLogisticsInfo GetSupplierLogisticsByID(int ID)
    {
        return MyBLL.GetSupplierLogisticsByID(ID, Public.GetUserPrivilege());
    }

    public virtual void EditSupplierLogistics(int IsAudit)
    {

        int Supplier_Logistics_ID = tools.CheckInt(Request.Form["Supplier_Logistics_ID"]);
        int Supplier_Logistics_IsAudit = IsAudit;
        DateTime Supplier_Logistics_AuditTime = DateTime.Now;
        string Supplier_Logistics_AuditRemarks = tools.CheckStr(Request.Form["Supplier_Logistics_AuditRemarks"]);


        SupplierLogisticsInfo entity = GetSupplierLogisticsByID(Supplier_Logistics_ID);
        if(entity!=null)
        {
            entity.Supplier_Logistics_ID = Supplier_Logistics_ID;
            entity.Supplier_Logistics_IsAudit = Supplier_Logistics_IsAudit;
            entity.Supplier_Logistics_AuditTime = Supplier_Logistics_AuditTime;
            entity.Supplier_Logistics_AuditRemarks = Supplier_Logistics_AuditRemarks;


            if (MyBLL.EditSupplierLogistics(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Logistics_view.aspx?Supplier_Logistics_ID=" + Supplier_Logistics_ID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        
    }

    public virtual void DelSupplierLogistics()
    {
        int Supplier_Logistics_ID = tools.CheckInt(Request.QueryString["Supplier_Logistics_ID"]);
        if (MyBLL.DelSupplierLogistics(Supplier_Logistics_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_Logistics_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string  GetSupplierLogisticss()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int list = tools.CheckInt(Request["list"]);

        list = list - 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierLogisticsInfo.Supplier_Logistics_ID", ">", "0"));

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierLogisticsInfo.Supplier_Logistics_Name", "like", keyword));
        }
        if (list>=0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierLogisticsInfo.Supplier_Logistics_IsAudit", "=", list.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<SupplierLogisticsInfo> entitys = MyBLL.GetSupplierLogisticss(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            foreach (SupplierLogisticsInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SupplierLogisticsInfo.Supplier_Logistics_ID\":" + entity.Supplier_Logistics_ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Logistics_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Logistics_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + " " + entity.Supplier_Address_StreetAddress);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Addr.DisplayAddress(entity.Supplier_Orders_Address_State, entity.Supplier_Orders_Address_City, entity.Supplier_Orders_Address_County) + " " + entity.Supplier_Orders_Address_StreetAddress);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Logistics_Number);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(SupplierLogisticsStatus(entity));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("64bb04aa-9b78-4c41-ae9c-e94f57581e22"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"Supplier_Logistics_view.aspx?Supplier_Logistics_ID=" + entity.Supplier_Logistics_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string SupplierLogisticsStatus(SupplierLogisticsInfo entity)
    {
        string Name = "--";
        if (entity != null)
        {
            if (entity.Supplier_Status == 0)
            {
                Name = "待发布";
            }
            else if (entity.Supplier_Status == 1)
            {
                if (entity.Supplier_Logistics_IsAudit == 0)
                {
                    Name = "待审核";
                }
                else if (entity.Supplier_Logistics_IsAudit == 1)
                {
                    if (entity.Supplier_LogisticsID == 0)
                    {
                        Name = "配货中";
                    }
                    else
                    {
                        Name = "已完成";
                    }
                }
                else
                {
                    Name = "审核不通过";
                }
            }
            else
            {
                Name = "已撤销";
            }
        }
        return Name;
    }


    public string SupplierLogisticsStatus_Option(int Type, string selectname)
    {
        string select_str = "";
        select_str += "<select name=\"" + selectname + "\">";

        if (Type == 0)
        {
            select_str += "<option value=\"0\" selected=\"selected\">全部</option>";
        }
        else
        {
            select_str += "<option value=\"0\" >全部</option>";
        }

        if (Type == 1)
        {
            select_str += "<option value=\"1\" selected=\"selected\">未审核</option>";
        }
        else
        {
            select_str += "<option value=\"1\">未审核</option>";
        }

        if (Type == 2)
        {
            select_str += "<option value=\"2\" selected=\"selected\">已审核</option>";
        }
        else
        {
            select_str += "<option value=\"2\">已审核</option>";
        }

        if (Type == 3)
        {
            select_str += "<option value=\"3\" selected=\"selected\" >审核未通过</option>";
        }
        else
        {
            select_str += "<option value=\"3\">审核未通过</option>";
        }




        select_str += "</select>";

        return select_str;
    }

    public string LogisticsTendersList(int SupplierLogisticsID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "LogisticsTenderInfo.Logistics_Tender_SupplierLogisticsID", "=", SupplierLogisticsID.ToString()));

        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_IsWin", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("LogisticsTenderInfo.Logistics_Tender_ID", "DESC"));

        IList<LogisticsTenderInfo> entitys = MyLogisticsTender.GetLogisticsTenders(Query);
        if(entitys!=null)
        {
            StringBuilder Builder = new StringBuilder();
            foreach(LogisticsTenderInfo entity in entitys)
            {
                Builder.Append("<tr>");
                Builder.Append("<td class=\"cell_content\" style=\"text-align:center;\">" + MyLogistics.GetLogisticsNameByID(entity.Logistics_Tender_LogisticsID) + "</td>");
                Builder.Append("<td class=\"cell_content\" style=\"text-align:center;\">" + Public.DisplayCurrency(entity.Logistics_Tender_Price) + "</td>");
                Builder.Append("<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Logistics_Tender_AddTime.ToString("yyyy-MM-dd") + "</td>");
                Builder.Append("<td class=\"cell_content\" style=\"text-align:center;\">" + GetLogisgicsTenderIsWin(entity.Logistics_Tender_IsWin) + "</td>");
                Builder.Append("</tr>");
            }

            return Builder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string GetLogisgicsTenderIsWin(int IsWin)
    {
        string Name = "";

        switch (IsWin)
        {
            case 0:
                Name = "--";
                break;
            case 1:
                Name = "中标";
                break;
            case 2:
                Name = "未中标";
                break;
        }
        return Name;
    }
}