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
/// 支付方式管理类
/// </summary>
public class PayWay
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IPayWay MyBLL;

    public PayWay()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = PayWayFactory.CreatePayWay();
    }

    public void AddPayWay()
    {
        int Pay_Way_ID = tools.CheckInt(Request.Form["Pay_Way_ID"]);
        int Pay_Way_Type = tools.CheckInt(Request.Form["Pay_Way_Type"]);
        string Pay_Way_Name = tools.CheckStr(Request.Form["Pay_Way_Name"]);
        int Pay_Way_Sort = tools.CheckInt(Request.Form["Pay_Way_Sort"]);
        int Pay_Way_Status = tools.CheckInt(Request.Form["Pay_Way_Status"]);
        string Pay_Way_Img = tools.CheckStr(Request.Form["Pay_Way_Img"]);
        int Pay_Way_Cod = tools.CheckInt(Request.Form["Pay_Way_Cod"]);
        string Pay_Way_Intro = Request.Form["Pay_Way_Intro"];

        if (Pay_Way_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写支付方式名称", false, "{back}"); return; }

        PayWayInfo entity = new PayWayInfo();
        entity.Pay_Way_ID = Pay_Way_ID;
        entity.Pay_Way_Type = Pay_Way_Type;
        entity.Pay_Way_Name = Pay_Way_Name;
        entity.Pay_Way_Sort = Pay_Way_Sort;
        entity.Pay_Way_Status = Pay_Way_Status;
        entity.Pay_Way_Img = Pay_Way_Img;
        entity.Pay_Way_Cod = Pay_Way_Cod;
        entity.Pay_Way_Intro = Pay_Way_Intro;
        entity.Pay_Way_Site = Public.GetCurrentSite();

        if (MyBLL.AddPayWay(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "pay_way_add.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditPayWay()
    {
        int Pay_Way_ID = tools.CheckInt(Request.Form["Pay_Way_ID"]);
        int Pay_Way_Type = tools.CheckInt(Request.Form["Pay_Way_Type"]);
        string Pay_Way_Name = tools.CheckStr(Request.Form["Pay_Way_Name"]);
        int Pay_Way_Sort = tools.CheckInt(Request.Form["Pay_Way_Sort"]);
        int Pay_Way_Status = tools.CheckInt(Request.Form["Pay_Way_Status"]);
        string Pay_Way_Img = tools.CheckStr(Request.Form["Pay_Way_Img"]);
        int Pay_Way_Cod = tools.CheckInt(Request.Form["Pay_Way_Cod"]);
        string Pay_Way_Intro = Request.Form["Pay_Way_Intro"];

        if (Pay_Way_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写支付方式名称", false, "{back}"); return; }

        PayWayInfo entity = new PayWayInfo();
        entity.Pay_Way_ID = Pay_Way_ID;
        entity.Pay_Way_Type = Pay_Way_Type;
        entity.Pay_Way_Name = Pay_Way_Name;
        entity.Pay_Way_Sort = Pay_Way_Sort;
        entity.Pay_Way_Status = Pay_Way_Status;
        entity.Pay_Way_Cod = Pay_Way_Cod;
        entity.Pay_Way_Img = Pay_Way_Img;
        entity.Pay_Way_Intro = Pay_Way_Intro;
        entity.Pay_Way_Site = Public.GetCurrentSite();

        if (MyBLL.EditPayWay(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "pay_way_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelPayWay()
    {
        int Pay_Way_ID = tools.CheckInt(Request.QueryString["Pay_Way_ID"]);
        if (MyBLL.DelPayWay(Pay_Way_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "pay_way_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public PayWayInfo GetPayWayByID(int cate_id)
    {
        return MyBLL.GetPayWayByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetPayWays()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        PayInfo PayInfo;
        IList<PayWayInfo> entitys = MyBLL.GetPayWays(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PayWayInfo entity in entitys)
            {
                PayInfo = MyBLL.GetPayByID(entity.Pay_Way_Type, Public.GetUserPrivilege());
                if (PayInfo == null) { PayInfo = new PayInfo(); PayInfo.Sys_Pay_Name = "线下支付"; }

                jsonBuilder.Append("{\"PayWayInfo.Pay_Way_ID\":" + entity.Pay_Way_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Pay_Way_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Pay_Way_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(PayInfo.Sys_Pay_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Pay_Way_Status == 1) { jsonBuilder.Append("启用"); }
                else { jsonBuilder.Append("关闭"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Pay_Way_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("a47a2618-e716-44e3-95b4-bee4c21c34f3")) 
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"pay_way_edit.aspx?pay_way_id=" + entity.Pay_Way_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("efcc1ead-ea67-4186-9fc9-4dca88d56c64"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('pay_way_do.aspx?action=move&pay_way_id=" + entity.Pay_Way_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string SysPayOption(int selectValue)
    {
        string strHTML = "";
        IList<PayInfo> entitys = MyBLL.GetPaysBySite(Public.GetCurrentSite(), Public.GetUserPrivilege());
        if (entitys == null) { return strHTML; }
        foreach (PayInfo entity in entitys)
        {
            if (entity.Sys_Pay_ID == selectValue) {
                strHTML += "<option value=\"" + entity.Sys_Pay_ID + "\" selected=\"selected\">" + entity.Sys_Pay_Name + "</option>";
            }
            else {
                strHTML += "<option value=\"" + entity.Sys_Pay_ID + "\">" + entity.Sys_Pay_Name + "</option>";
            }
        }

        return strHTML;
    }

}
