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
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
/// 支付方式管理类
/// </summary>
public class ProductAuditReason
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductAuditReason MyBLL;

    public ProductAuditReason()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ProductAuditReasonFactory.CreateProductAuditReason();
    }

    public void AddProductAuditReason()
    {
        int AuditReasonID = 0;
        int Product_Audit_Reason_ID = 0;
        string Product_Audit_Reason_Note = tools.CheckStr(Request["Reason_Note"]);


        if (Product_Audit_Reason_Note.Length == 0) { Public.Msg("error", "错误信息", "请填写原因内容", false, "{back}"); return; }

        ProductAuditReasonInfo entity = new ProductAuditReasonInfo();
        entity.Product_Audit_Reason_ID = Product_Audit_Reason_ID;
        entity.Product_Audit_Reason_Note = Product_Audit_Reason_Note;

        if (MyBLL.AddProductAuditReason(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "audit_reason.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditProductAuditReason()
    {
        int Product_Audit_Reason_ID = tools.CheckInt(Request.Form["reason_ID"]);
        string Product_Audit_Reason_Note = tools.CheckStr(Request["Reason_Note"]);

        if (Product_Audit_Reason_Note.Length == 0) { Public.Msg("error", "错误信息", "请填写原因内容", false, "{back}"); return; }

        ProductAuditReasonInfo entity = new ProductAuditReasonInfo();
        entity.Product_Audit_Reason_ID = Product_Audit_Reason_ID;
        entity.Product_Audit_Reason_Note = Product_Audit_Reason_Note;

        if (MyBLL.EditProductAuditReason(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "audit_reason.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProductAuditReason()
    {
        int Product_Audit_Reason_ID = tools.CheckInt(Request.QueryString["Reason_ID"]);
        if (MyBLL.DelProductAuditReason(Product_Audit_Reason_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "audit_reason.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ProductAuditReasonInfo GetProductAuditReasonByID(int reason_id)
    {
        return MyBLL.GetProductAuditReasonByID(reason_id, Public.GetUserPrivilege());
    }

    public string GetProductAuditReasons()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductAuditReasonInfo.Product_Audit_Reason_ID", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ProductAuditReasonInfo> entitys = MyBLL.GetProductAuditReasons(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductAuditReasonInfo entity in entitys)
            {

                jsonBuilder.Append("{\"ProductAuditReasonInfo.Product_Audit_Reason_ID\":" + entity.Product_Audit_Reason_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Audit_Reason_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Product_Audit_Reason_Note));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("78d18ad2-7c45-4a9c-9a53-cbe50562c242"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"audit_reason.aspx?action=renew&reason_id=" + entity.Product_Audit_Reason_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("158a1875-7682-4781-97ef-7f31e39280c1"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('audit_reason_do.aspx?action=move&reason_id=" + entity.Product_Audit_Reason_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string GetProductAuditReasonsCheckbox(string Check_Name)
    {
        string check_str="";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductAuditReasonInfo.Product_Audit_Reason_ID", ">", "0"));


        IList<ProductAuditReasonInfo> entitys = MyBLL.GetProductAuditReasons(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
             foreach (ProductAuditReasonInfo entity in entitys)
            {
                check_str = check_str + "<input type=\"checkbox\" name=\""+Check_Name+"\" value=\""+ entity.Product_Audit_Reason_ID +"\"> " + entity.Product_Audit_Reason_Note + "<br>";
            }
             return check_str;
        }
        else
        {
            return null;
        }
    }

    public string GetProductAuditReasonsByProductID(int Product_ID)
    {
        string check_str = "";
        ProductAuditReasonInfo reasoninfo;

        IList<ProductDenyReasonInfo> entitys = MyBLL.GetProductDenyReasons(Product_ID);
        if (entitys != null)
        {
            foreach (ProductDenyReasonInfo entity in entitys)
            {
                reasoninfo = MyBLL.GetProductAuditReasonByID(entity.Product_Deny_Reason_AuditReasonID,Public.GetUserPrivilege());
                if (reasoninfo != null)
                {
                    check_str = check_str + "" + reasoninfo.Product_Audit_Reason_Note + "<br>";
                }
            }
            return check_str;
        }
        else
        {
            return null;
        }
    }


}
