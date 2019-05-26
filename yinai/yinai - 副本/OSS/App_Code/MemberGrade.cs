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
///MemberGrade 的摘要说明
/// </summary>
public class MemberGrade
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMemberGrade MyBLL;

    public MemberGrade()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = MemberGradeFactory.CreateMemberGrade();
    }

    public void AddMemberGrade()
    {
        int Member_Grade_ID = tools.CheckInt(Request.Form["Member_Grade_ID"]);
        string Member_Grade_Name = tools.CheckStr(Request.Form["Member_Grade_Name"]);
        int Member_Grade_Percent = tools.CheckInt(Request.Form["Member_Grade_Percent"]);
        int Member_Grade_Default = tools.CheckInt(Request.Form["Member_Grade_Default"]);
        int Member_Grade_RequiredCoin = tools.CheckInt(Request.Form["Member_Grade_RequiredCoin"]);
        double Member_Grade_CoinRate = tools.CheckFloat(Request.Form["Member_Grade_CoinRate"]);

        MemberGradeInfo entity = new MemberGradeInfo();
        entity.Member_Grade_ID = Member_Grade_ID;
        entity.Member_Grade_Name = Member_Grade_Name;
        entity.Member_Grade_Percent = Member_Grade_Percent;
        entity.Member_Grade_Default = Member_Grade_Default;
        entity.Member_Grade_RequiredCoin = Member_Grade_RequiredCoin;
        entity.Member_Grade_CoinRate = Member_Grade_CoinRate;
        entity.Member_Grade_Addtime = DateTime.Now;
        entity.Member_Grade_Site = Public.GetCurrentSite();

        if (MyBLL.AddMemberGrade(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "member_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditMemberGrade()
    {
        int Member_Grade_ID = tools.CheckInt(Request.Form["Member_Grade_ID"]);
        string Member_Grade_Name = tools.CheckStr(Request.Form["Member_Grade_Name"]);
        int Member_Grade_Percent = tools.CheckInt(Request.Form["Member_Grade_Percent"]);
        int Member_Grade_Default = tools.CheckInt(Request.Form["Member_Grade_Default"]);
        int Member_Grade_RequiredCoin = tools.CheckInt(Request.Form["Member_Grade_RequiredCoin"]);
        double Member_Grade_CoinRate = tools.CheckFloat(Request.Form["Member_Grade_CoinRate"]);

        MemberGradeInfo entity = new MemberGradeInfo();
        entity.Member_Grade_ID = Member_Grade_ID;
        entity.Member_Grade_Name = Member_Grade_Name;
        entity.Member_Grade_Percent = Member_Grade_Percent;
        entity.Member_Grade_Default = Member_Grade_Default;
        entity.Member_Grade_RequiredCoin = Member_Grade_RequiredCoin;
        entity.Member_Grade_CoinRate = Member_Grade_CoinRate;
        entity.Member_Grade_Addtime = DateTime.Now;
        entity.Member_Grade_Site = Public.GetCurrentSite();

        if (MyBLL.EditMemberGrade(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "member_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelMemberGrade()
    {
        int Member_Grade_ID = tools.CheckInt(Request.QueryString["Member_Grade_ID"]);
        if (MyBLL.DelMemberGrade(Member_Grade_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "member_grade_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public MemberGradeInfo GetMemberGradeByID(int cate_id)
    {
        return MyBLL.GetMemberGradeByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetMemberGrades()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberGradeInfo> entitys = MyBLL.GetMemberGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberGradeInfo entity in entitys)
            {
                jsonBuilder.Append("{\"MemberGradeInfo.Member_Grade_ID\":" + entity.Member_Grade_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Grade_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade_Percent);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Member_Grade_Default == 1) { jsonBuilder.Append("是"); }
                else { jsonBuilder.Append("否"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade_RequiredCoin);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade_CoinRate);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Grade_Addtime.ToString("yy-MM-dd"));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("73df03fa-ef43-486a-9b4c-b9c3e834cbbb"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"member_grade_edit.aspx?member_grade_id=" + entity.Member_Grade_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("8f0cba35-c84e-4cfb-8ed5-26802004a848"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('member_grade_do.aspx?action=move&member_grade_id=" + entity.Member_Grade_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
