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
/// 支付条件管理类
/// </summary>
public class PayType
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IPayType MyBLL;

    public PayType()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = PayTypeFactory.CreatePayType();
    }

    public void AddPayType()
    {
        int Pay_Type_ID = tools.CheckInt(Request.Form["Pay_Type_ID"]);
        string Pay_Type_Name = tools.CheckStr(Request.Form["Pay_Type_Name"]);
        int Pay_Type_Sort = tools.CheckInt(Request.Form["Pay_Type_Sort"]);
        int Pay_Type_IsActive = tools.CheckInt(Request.Form["Pay_Type_IsActive"]);
        string Pay_Type_Site = tools.CheckStr(Request.Form["Pay_Type_Site"]);

        if (Pay_Type_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写支付条件名称", false, "{back}"); return; }

        PayTypeInfo entity = new PayTypeInfo();
         entity.Pay_Type_ID = Pay_Type_ID;
         entity.Pay_Type_Name = Pay_Type_Name;
         entity.Pay_Type_Sort = Pay_Type_Sort;
         entity.Pay_Type_IsActive = Pay_Type_IsActive;
         entity.Pay_Type_Site = Public.GetCurrentSite();

        if (MyBLL.AddPayType(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "pay_type_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditPayType()
    {
        int Pay_Type_ID = tools.CheckInt(Request.Form["Pay_Type_ID"]);
        string Pay_Type_Name = tools.CheckStr(Request.Form["Pay_Type_Name"]);
        int Pay_Type_Sort = tools.CheckInt(Request.Form["Pay_Type_Sort"]);
        int Pay_Type_IsActive = tools.CheckInt(Request.Form["Pay_Type_IsActive"]);
        string Pay_Type_Site = tools.CheckStr(Request.Form["Pay_Type_Site"]);

        if (Pay_Type_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写支付条件名称", false, "{back}"); return; }

        PayTypeInfo entity = GetPayTypeByID(Pay_Type_ID);
        if (entity != null)
        {
            entity.Pay_Type_Name = Pay_Type_Name;
            entity.Pay_Type_Sort = Pay_Type_Sort;
            entity.Pay_Type_IsActive = Pay_Type_IsActive;

            if (MyBLL.EditPayType(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "pay_type_list.aspx");
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

    public void DelPayType()
    {
        int pay_type_ID = tools.CheckInt(Request.QueryString["pay_type_ID"]);
        if (MyBLL.DelPayType(pay_type_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "pay_type_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public PayTypeInfo GetPayTypeByID(int cate_id)
    {
        return MyBLL.GetPayTypeByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetPayTypes()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayTypeInfo.Pay_Type_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        PayInfo PayInfo;
        IList<PayTypeInfo> entitys = MyBLL.GetPayTypes(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PayTypeInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PayTypeInfo.Pay_Type_ID\":" + entity.Pay_Type_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Pay_Type_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Pay_Type_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Pay_Type_IsActive == 1) { jsonBuilder.Append("启用"); }
                else { jsonBuilder.Append("禁用"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Pay_Type_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("fcd3782c-b791-40c6-a29d-9b43092de04f")) 
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"pay_type_edit.aspx?pay_type_id=" + entity.Pay_Type_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("52b0dc84-893a-4f1d-a15d-2023250ac8a6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('pay_type_do.aspx?action=move&pay_type_id=" + entity.Pay_Type_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

}
