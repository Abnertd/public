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
///Member 的摘要说明
/// </summary>
public class Feedback
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMember MyBLL;
    private IFeedBack MyFeedback;
    private ISupplier MySupplier;
    private IBidUpRequireQuick MyBidUpRequire;

    public Feedback()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = MemberFactory.CreateMember();
        MyFeedback = FeedBackFactory.CreateFeedBack();
        MySupplier = SupplierFactory.CreateSupplier();
        MyBidUpRequire = BidUpRequireQuickFactory.CreateBidUpRequireQuick();
    }

    public string GetFeedBacks()
    {
        string nickname = "";
        string type_name = "";
        int Feedback_Type = 0;
        string keyword = tools.CheckStr(Request["keyword"]);
        int isreply = tools.CheckInt(Request["isreply"]);
        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FeedBackInfo.Feedback_Site", "=", Public.GetCurrentSite()));

        string listtype = tools.CheckStr(Request.QueryString["listtype"]);

        switch (listtype)
        {
            case "message":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_Type", "=", "1"));
                break;
            case "idea":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_Type", "!=", "1"));
                break;
            case "suggest":
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "FeedBackInfo.Feedback_Type", "=", "3"));
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "FeedBackInfo.Feedback_Type", "=", "4"));
                break;
            case "complain":
                Query.ParamInfos.Add(new ParamInfo("AND(", "int", "FeedBackInfo.Feedback_Type", "=", "5"));
                Query.ParamInfos.Add(new ParamInfo("OR)", "int", "FeedBackInfo.Feedback_Type", "=", "6"));
                break;
            default:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_Type", "=", "1"));
                break;
        }
        if (isreply == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FeedBackInfo.Feedback_Reply_Content", "<>", ""));
        }
        else if (isreply == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FeedBackInfo.Feedback_Reply_Content", "=", ""));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "FeedBackInfo.Feedback_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "FeedBackInfo.Feedback_CompanyName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "FeedBackInfo.Feedback_Tel", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "FeedBackInfo.Feedback_Email", "like", keyword));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{FeedBackInfo.Feedback_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{FeedBackInfo.Feedback_Addtime})", "<=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyFeedback.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (FeedBackInfo entity in entitys)
            {
                nickname = "游客";
                MemberInfo member = new MemberInfo();

                if (entity.Feedback_MemberID > 0)
                {
                    member = MyBLL.GetMemberByID(entity.Feedback_MemberID, Public.GetUserPrivilege());
                    if (member != null)
                    {
                        nickname = member.Member_NickName;
                    }
                }

                SupplierInfo supplierinfo = new SupplierInfo();

                if (entity.Feedback_SupplierID > 0)
                {
                    supplierinfo = MySupplier.GetSupplierByID(entity.Feedback_SupplierID, Public.GetUserPrivilege());
                    if (supplierinfo != null)
                    {
                        nickname = supplierinfo.Supplier_Nickname;
                    }
                }

                switch (entity.Feedback_Type)
                {
                    //case 1:
                    //    type_name = "简单的留言";
                    //    break;
                    //case 2:
                    //    type_name = "对网站的意见";
                    //    break;
                    //case 3:
                    //    type_name = "对公司的建议";
                    //    break;
                    //case 4:
                    //    type_name = "具有合作意向";
                    //    break;
                    //case 5:
                    //    type_name = "产品投诉";
                    //    break;
                    //case 6:
                    //    type_name = "服务投诉";
                    //    break;


                    case 1:
                        type_name = "网站留言";
                        break;
                    case 2:
                        type_name = "商业承兑融资";
                        break;
                    case 3:
                        type_name = "应收账款融资";
                        break;
                    case 4:
                        type_name = "货押融资";
                        break;

                    //case 3:
                    //    type_name = "对公司的建议";
                    //    break;
                    //case 4:
                    //    type_name = "具有合作意向";
                    //    break;
                    //case 5:
                    //    type_name = "产品投诉";
                    //    break;
                    //case 6:
                    //    type_name = "服务投诉";
                    //break;


                }

                jsonBuilder.Append("{\"id\":" + entity.Feedback_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Feedback_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Feedback_Name));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.Feedback_CompanyName));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(type_name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Feedback_Tel));
                jsonBuilder.Append("\",");

                if (Feedback_Type==1)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Feedback_Email));
                    jsonBuilder.Append("\",");
                }
                else 
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(tools.CheckFloat(entity.Feedback_Amount.ToString()));
                    jsonBuilder.Append("\",");
                }
             

                jsonBuilder.Append("\"");
                if (entity.Feedback_Reply_Content != "")
                {
                    jsonBuilder.Append("已回复");
                }
                else
                {
                    jsonBuilder.Append("未回复");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Feedback_Addtime.ToString("yy-MM-dd"));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"feedback_view.aspx?feedback_id=" + entity.Feedback_ID + "\\\" title=\\\"查看\\\">查看</a> ");

                if (Public.CheckPrivilege("cc567804-3e2e-4c6c-aa22-c9a353508074"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\" alt=\\\"删除\\\" align=\\\"absmiddle\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('supplier_do.aspx?action=feedbackmove&listtype=" + listtype + "&feedback_id=" + entity.Feedback_ID + "')\\\" title=\\\"删除\\\">删除</a>");


                    //jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('depot_do.aspx?action=move&depot_id=" + Depot_ID + "')\\\" title=\\\"删除\\\">删除</a>");

                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

                supplierinfo = null;


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

    public void FeedBack_Del()
    {
        int feedback_id = tools.CheckInt(Request["feedback_id"]);
        string listtype = tools.CheckStr(Request["listtype"]);
        if (MyFeedback.DelFeedBack(feedback_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=" + listtype);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

        }
    }

    public void FeedBack_Reply()
    {
        int feedback_id = tools.CheckInt(Request["feedback_id"]);
        string feedback_reply = tools.CheckStr(Request.Form["Feedback_Reply_Content"]);
        FeedBackInfo feedback = MyFeedback.GetFeedBackByID(feedback_id, Public.GetUserPrivilege());
        if (feedback != null)
        {

            feedback.Feedback_Reply_Content = feedback_reply;
            feedback.Feedback_Reply_Addtime = DateTime.Now;
            if (MyFeedback.EditFeedBack(feedback, Public.GetUserPrivilege()))
            {
                switch (feedback.Feedback_Type)
                {
                    //case 1:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=message");
                    //    break;
                    //case 2:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=idea");
                    //    break;
                    //case 3:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=suggest");
                    //    break;
                    //case 4:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=suggest");
                    //    break;
                    //case 5:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=complain");
                    //    break;
                    //case 6:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=complain");
                    //    break;
                    case 1:
                        Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=message");
                        break;

                    case 2:
                        Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=message");
                        break;
                }

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试3", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试1", false, "{back}");
        }
    }

    public void FeedBack_SupplierReply()
    {
        int feedback_id = tools.CheckInt(Request["feedback_id"]);
        string feedback_reply = tools.CheckStr(Request.Form["Feedback_Reply_Content"]);
        FeedBackInfo feedback = MyFeedback.GetFeedBackByID(feedback_id, Public.GetUserPrivilege());
        if (feedback != null)
        {

            feedback.Feedback_Reply_Content = feedback_reply;
            feedback.Feedback_Reply_Addtime = DateTime.Now;
            if (MyFeedback.EditFeedBack(feedback, Public.GetUserPrivilege()))
            {
                switch (feedback.Feedback_Type)
                {
                    //case 1:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=message");
                    //    break;
                    //case 2:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=idea");
                    //    break;
                    //case 3:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=suggest");
                    //    break;
                    //case 4:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=suggest");
                    //    break;
                    //case 5:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=complain");
                    //    break;
                    //case 6:
                    //    Public.Msg("positive", "操作成功", "操作成功", true, "feedback_list.aspx?listtype=complain");
                    //    break;
                    case 1:
                        Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/feedback_list.aspx?listtype=message&menu_id=267");
                        break;

                    case 2:
                        Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/feedback_list.aspx?listtype=idea&menu_id=268");
                        break;
                    case 3:
                        Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/feedback_list.aspx?listtype=idea&menu_id=268");
                        break;
                    case 4:
                        Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/feedback_list.aspx?listtype=idea&menu_id=268");
                        break;

                }

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试3", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试1", false, "{back}");
        }
    }

    public FeedBackInfo GetFeedBackByID(int feedback_id)
    {
        return MyFeedback.GetFeedBackByID(feedback_id, Public.GetUserPrivilege());
    }
    public BidUpRequireQuickInfo GetBidUpRequireByID(int signup_id)
    {

        return MyBidUpRequire.GetBidUpRequireQuickByID(signup_id);
    }

    public void UpdateFeedBackStatus(int feedback_id, int read_status, int reply_read_status)
    {
        MyFeedback.EditFeedBackReadStatus(feedback_id, read_status, reply_read_status, Public.GetUserPrivilege());
    }

    public void FeedBack_Export()
    {

        string listtype = tools.CheckStr(Request.QueryString["listtype"]);  
        string feedback_id = tools.CheckStr(Request.QueryString["feedback_id"]);
        if (feedback_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }
        string type_name = "";
        if (tools.Left(feedback_id, 1) == ",") { feedback_id = feedback_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        //MemberInfo memberinfo = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FeedBackInfo.Feedback_ID", "in", feedback_id));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "DESC"));
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, Public.GetUserPrivilege());

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;
        

         string[] dtcol = { "编号", "昵称", "留言类型", "电话", "Email", "状态", "时间", "留言内容", "回复时间", "回复内容" };

         //if (listtype!="1")
         //{
        //  string[]   dtcol1 = { "编号", "昵称", "留言类型", "电话", "Email", "状态", "时间", "留言内容", "回复时间", "回复内容" };
         //}



        //}
        //else
        //{
          //   dtcol = { "编号", "昵称", "留言类型", "电话", "金额", "状态", "时间", "留言内容", "回复时间", "回复内容" };
        //}
       
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (FeedBackInfo entity in entitys)
        {
            switch (entity.Feedback_Type)
            {
                //case 1:
                //    type_name = "简单的留言";
                //    break;
                //case 2:
                //    type_name = "对网站的意见";
                //    break;
                //case 3:
                //    type_name = "对公司的建议";
                //    break;
                //case 4:
                //    type_name = "具有合作意向";
                //    break;
                //case 5:
                //    type_name = "产品投诉";
                //    break;
                //case 6:
                //    type_name = "服务投诉";
                //    break;
                                case 1:
                    type_name = "网站留言";
                    break;
                case 2:
                    type_name = "商业承兑融资";
                    break;
                case 3:
                    type_name = "货押融资";
                    break;
                case 4:
                    type_name = "应收账款融资";
                    break;

            }
            dr = dt.NewRow();
            dr[0] = entity.Feedback_ID;


            //memberinfo = MyBLL.GetMemberByID(entity.Feedback_MemberID,Public.GetUserPrivilege());
            //if (memberinfo != null)
            //{
            //    dr[1] = memberinfo.Member_NickName;
            //}
            //else
            //{
            //    dr[1] = "游客";
            //}

            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Feedback_SupplierID, Public.GetUserPrivilege());

            if (supplierinfo != null)
            {
                dr[1] = supplierinfo.Supplier_Nickname;
            }
            else
            {
                dr[1] = "游客";
            }

            dr[2] = type_name;
            dr[3] = entity.Feedback_Tel;
            if (listtype=="1")
            {
                dr[4] = entity.Feedback_Email;
            }
            else
            {
                dr[4] = entity.Feedback_Amount;
            }
          
            if (entity.Feedback_Reply_Content != "")
            {
                dr[5] = "已回复";
            }
            else
            {
                dr[5] = "未回复";
            }
            dr[6] = entity.Feedback_Addtime;
            dr[7] = entity.Feedback_Content;
            dr[8] = entity.Feedback_Reply_Addtime;
            dr[9] = entity.Feedback_Reply_Content;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }



    public void FeedBackFin_Export()
    {

        //string listtype = tools.CheckStr(Request.QueryString["listtype"]);
        string feedback_id = tools.CheckStr(Request.QueryString["feedback_id"]);
        if (feedback_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }
        string type_name = "";
        if (tools.Left(feedback_id, 1) == ",") { feedback_id = feedback_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        //MemberInfo memberinfo = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FeedBackInfo.Feedback_ID", "in", feedback_id));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "DESC"));
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, Public.GetUserPrivilege());

        if (entitys == null) { return; }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;


        string[] dtcol = { "编号", "昵称", "留言类型", "电话", "金额", "状态", "时间", "留言内容", "回复时间", "回复内容" };

        //if (listtype!="1")
        //{
        //  string[]   dtcol1 = { "编号", "昵称", "留言类型", "电话", "Email", "状态", "时间", "留言内容", "回复时间", "回复内容" };
        //}



        //}
        //else
        //{
        //   dtcol = { "编号", "昵称", "留言类型", "电话", "金额", "状态", "时间", "留言内容", "回复时间", "回复内容" };
        //}

        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        foreach (FeedBackInfo entity in entitys)
        {
            switch (entity.Feedback_Type)
            {
                //case 1:
                //    type_name = "简单的留言";
                //    break;
                //case 2:
                //    type_name = "对网站的意见";
                //    break;
                //case 3:
                //    type_name = "对公司的建议";
                //    break;
                //case 4:
                //    type_name = "具有合作意向";
                //    break;
                //case 5:
                //    type_name = "产品投诉";
                //    break;
                //case 6:
                //    type_name = "服务投诉";
                //    break;
                case 1:
                    type_name = "网站留言";
                    break;
                case 2:
                    type_name = "商业承兑融资";
                    break;
                case 3:
                    type_name = "应收账款融资";
                    break;
                case 4:
                    type_name = "货押融资";
                    break;

            }
            dr = dt.NewRow();
            dr[0] = entity.Feedback_ID;


            //memberinfo = MyBLL.GetMemberByID(entity.Feedback_MemberID,Public.GetUserPrivilege());
            //if (memberinfo != null)
            //{
            //    dr[1] = memberinfo.Member_NickName;
            //}
            //else
            //{
            //    dr[1] = "游客";
            //}

            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Feedback_SupplierID, Public.GetUserPrivilege());

            if (supplierinfo != null)
            {
                dr[1] = supplierinfo.Supplier_Nickname;
            }
            else
            {
                dr[1] = "游客";
            }

            dr[2] = type_name;
            dr[3] = entity.Feedback_Tel;    
                dr[4] = entity.Feedback_Amount;         

            if (entity.Feedback_Reply_Content != "")
            {
                dr[5] = "已回复";
            }
            else
            {
                dr[5] = "未回复";
            }
            dr[6] = entity.Feedback_Addtime;
            dr[7] = entity.Feedback_Content;
            dr[8] = entity.Feedback_Reply_Addtime;
            dr[9] = entity.Feedback_Reply_Content;
            dt.Rows.Add(dr);
            dr = null;
        }

        Public.toExcel(dt);
    }

}
