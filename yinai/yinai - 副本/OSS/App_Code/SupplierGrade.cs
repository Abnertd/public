using System;
using System.Text;
using System.Data;
using System.Configuration;
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
using Glaer.Trade.B2C.BLL.MEM;

/// <summary>
///SupplierGrade 的摘要说明
/// </summary>
public class SupplierGrade
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISupplierGrade MyBLL;

    public SupplierGrade()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SupplierGradeFactory.CreateSupplierGrade();
    }

    public void AddSupplierGrade()
    {
        int Supplier_Grade_ID = tools.CheckInt(Request.Form["Supplier_Grade_ID"]);
        string Supplier_Grade_Name = tools.CheckStr(Request.Form["Supplier_Grade_Name"]);
        int Supplier_Grade_Percent = tools.CheckInt(Request.Form["Supplier_Grade_Percent"]);
        int Supplier_Grade_Default = tools.CheckInt(Request.Form["Supplier_Grade_Default"]);
        int Supplier_Grade_RequiredCoin = tools.CheckInt(Request.Form["Supplier_Grade_RequiredCoin"]);
        double Supplier_Grade_CoinRate = tools.CheckFloat(Request.Form["Supplier_Grade_CoinRate"]);

        if (Supplier_Grade_Name.Length == 0)
        {
            Public.Msg("info", "提示信息", "请填写供应商等级名称", false, "{back}");
        }

        SupplierGradeInfo entity = new SupplierGradeInfo();
        entity.Supplier_Grade_ID = Supplier_Grade_ID;
        entity.Supplier_Grade_Name = Supplier_Grade_Name;
        entity.Supplier_Grade_Percent = Supplier_Grade_Percent;
        entity.Supplier_Grade_Default = Supplier_Grade_Default;
        entity.Supplier_Grade_RequiredCoin = Supplier_Grade_RequiredCoin;
        entity.Supplier_Grade_Site = Public.GetCurrentSite();

        if (MyBLL.AddSupplierGrade(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditSupplierGrade()
    {
        int Supplier_Grade_ID = tools.CheckInt(Request.Form["Supplier_Grade_ID"]);
        string Supplier_Grade_Name = tools.CheckStr(Request.Form["Supplier_Grade_Name"]);
        int Supplier_Grade_Percent = tools.CheckInt(Request.Form["Supplier_Grade_Percent"]);
        int Supplier_Grade_Default = tools.CheckInt(Request.Form["Supplier_Grade_Default"]);
        int Supplier_Grade_RequiredCoin = tools.CheckInt(Request.Form["Supplier_Grade_RequiredCoin"]);
        double Supplier_Grade_CoinRate = tools.CheckFloat(Request.Form["Supplier_Grade_CoinRate"]);

        if (Supplier_Grade_Name.Length == 0)
        {
            Public.Msg("info", "提示信息", "请填写供应商等级名称", false, "{back}");
        }

        SupplierGradeInfo entity = new SupplierGradeInfo();
        entity.Supplier_Grade_ID = Supplier_Grade_ID;
        entity.Supplier_Grade_Name = Supplier_Grade_Name;
        entity.Supplier_Grade_Percent = Supplier_Grade_Percent;
        entity.Supplier_Grade_Default = Supplier_Grade_Default;
        entity.Supplier_Grade_RequiredCoin = Supplier_Grade_RequiredCoin;
        entity.Supplier_Grade_Site = Public.GetCurrentSite();

        if (MyBLL.EditSupplierGrade(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelSupplierGrade()
    {
        int Supplier_Grade_ID = tools.CheckInt(Request.QueryString["Supplier_Grade_ID"]);
        if (MyBLL.DelSupplierGrade(Supplier_Grade_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Supplier_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public SupplierGradeInfo GetSupplierGradeByID(int cate_id)
    {
        return MyBLL.GetSupplierGradeByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetSupplierGrades()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierGradeInfo.Supplier_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierGradeInfo> entitys = MyBLL.GetSupplierGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierGradeInfo entity in entitys)
            {
                jsonBuilder.Append("{\"SupplierGradeInfo.Supplier_Grade_ID\":" + entity.Supplier_Grade_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Grade_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Supplier_Grade_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Grade_Percent);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Supplier_Grade_Default == 1) { jsonBuilder.Append("是"); }
                else { jsonBuilder.Append("否"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Supplier_Grade_RequiredCoin);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("065594cf-5094-4ce6-b753-3c360d3213bd"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"Supplier_grade_edit.aspx?Supplier_grade_id=" + entity.Supplier_Grade_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("3c4246f5-0b23-4c6e-8c73-65c14a2a76bc"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Supplier_grade_do.aspx?action=move&Supplier_grade_id=" + entity.Supplier_Grade_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else {
            return null;
        }
    }


}
