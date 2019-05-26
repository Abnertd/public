using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Logistics 的摘要说明
/// </summary>
public class Logistics
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ILogistics MyBLL;
    private ITools tools;
    IEncrypt encrypt;

	public Logistics()
	{
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBLL = LogisticsFactory.CreateLogistics();
        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        
	}


    public LogisticsInfo GetLogisticsByID(int ID)
    {
        return MyBLL.GetLogisticsByID(ID, Public.GetUserPrivilege());
    }

    public string GetLogisticsNameByID(int ID)
    {
        LogisticsInfo entity = GetLogisticsByID(ID);
        if(entity!=null)
        {
            return entity.Logistics_CompanyName;
        }
        else
        {
            return "--";
        }
    }
    public LogisticsInfo GetLogisticsByNickName(string Name)
    {
        return MyBLL.GetLogisticsByNickName(Name, Public.GetUserPrivilege());
    }

    public virtual void AddLogistics()
    {
        int Logistics_ID = tools.CheckInt(Request.Form["Logistics_ID"]);
        string Logistics_NickName = tools.CheckStr(Request.Form["Logistics_NickName"]);
        string password_confirm = tools.CheckStr(Request.Form["password_confirm"]);
        string Logistics_Password = tools.CheckStr(Request.Form["Logistics_Password"]);
        string Logistics_CompanyName = tools.CheckStr(Request.Form["Logistics_CompanyName"]);
        string Logistics_Name = tools.CheckStr(Request.Form["Logistics_Name"]);
        string Logistics_Tel = tools.CheckStr(Request.Form["Logistics_Tel"]);
        int Logistics_Status = tools.CheckInt(Request.Form["Logistics_Status"]);
        DateTime Logistics_Addtime =DateTime.Now;
        DateTime Logistics_Lastlogin_Time = DateTime.Now;

        if(Logistics_NickName.Length==0)
        {
            Public.Msg("error", "错误信息", "请填写登录名", false, "{back}");
            return; 
        }
        LogisticsInfo logisticsinfo = GetLogisticsByNickName(Logistics_NickName);

        if(logisticsinfo!=null)
        {
            Public.Msg("error", "错误信息", "登录名已存在", false, "{back}"); 
            return; 
        }
        if(Logistics_Password.Length==0)
        {
            Public.Msg("error", "错误信息", "请填写登录密码", false, "{back}");
            return; 
        }

        if(Logistics_Password!=password_confirm)
        {
            Public.Msg("error", "错误信息", "登录密码与确认密码不一致", false, "{back}");
            return; 
        }

        if(Logistics_CompanyName.Length==0)
        {
            Public.Msg("error", "错误信息", "请填写物流商公司名", false, "{back}");
            return; 
        }

        if (Logistics_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写联系人", false, "{back}");
            return;
        }

        if (Logistics_Tel.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写联系电话", false, "{back}");
            return;
        }
        LogisticsInfo entity = new LogisticsInfo();
        entity.Logistics_ID = Logistics_ID;
        entity.Logistics_NickName = Logistics_NickName;
        entity.Logistics_Password = encrypt.MD5(Logistics_Password);
        entity.Logistics_CompanyName = Logistics_CompanyName;
        entity.Logistics_Name = Logistics_Name;
        entity.Logistics_Tel = Logistics_Tel;
        entity.Logistics_Status = Logistics_Status;
        entity.Logistics_Addtime = Logistics_Addtime;
        entity.Logistics_Lastlogin_Time = Logistics_Lastlogin_Time;

        if (MyBLL.AddLogistics(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Logistics_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditLogistics()
    {

        int Logistics_ID = tools.CheckInt(Request.Form["Logistics_ID"]);
        string Logistics_Password = tools.CheckStr(Request.Form["Logistics_Password"]);
        string password_confirm = tools.CheckStr(Request.Form["password_confirm"]);
        string Logistics_CompanyName = tools.CheckStr(Request.Form["Logistics_CompanyName"]);
        string Logistics_Name = tools.CheckStr(Request.Form["Logistics_Name"]);
        string Logistics_Tel = tools.CheckStr(Request.Form["Logistics_Tel"]);
        int Logistics_Status = tools.CheckInt(Request.Form["Logistics_Status"]);

        if (Logistics_Password.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写登录密码", false, "{back}");
            return;
        }

        if (Logistics_Password != password_confirm)
        {
            Public.Msg("error", "错误信息", "登录密码与确认密码不一致", false, "{back}");
            return;
        }

        if (Logistics_CompanyName.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写物流商公司名", false, "{back}");
            return;
        }

        if (Logistics_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写联系人", false, "{back}");
            return;
        }

        if (Logistics_Tel.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写联系电话", false, "{back}");
            return;
        }

        LogisticsInfo entity = GetLogisticsByID(Logistics_ID);
        if (entity!=null)
        {
            

            entity.Logistics_ID = Logistics_ID;
            entity.Logistics_Password = encrypt.MD5(Logistics_Password);
            entity.Logistics_CompanyName = Logistics_CompanyName;
            entity.Logistics_Name = Logistics_Name;
            entity.Logistics_Tel = Logistics_Tel;
            entity.Logistics_Status = Logistics_Status;


            if (MyBLL.EditLogistics(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Logistics_list.aspx");
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

    public virtual void DelLogistics()
    {
        int Logistics_ID = tools.CheckInt(Request.QueryString["Logistics_ID"]);
        if (MyBLL.DelLogistics(Logistics_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Logistics_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetLogisticss()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);


        Query.ParamInfos.Add(new ParamInfo("AND", "str", "LogisticsInfo.Logistics_ID", ">", "0"));

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "LogisticsInfo.Logistics_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "LogisticsInfo.Logistics_CompanyName", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<LogisticsInfo> entitys = MyBLL.GetLogisticss(Query, Public.GetUserPrivilege());
        if(entitys!=null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            foreach (LogisticsInfo entity in entitys)
            {
                jsonBuilder.Append("{\"LogisticsInfo.Logistics_ID\":" + entity.Logistics_ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Logistics_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Logistics_NickName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Logistics_CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Logistics_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Logistics_Tel));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(LogisticsStatus(entity.Logistics_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("bd38ff8b-f627-44ec-9275-39c9df7425e1"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"Logistics_edit.aspx?Logistics_ID=" + entity.Logistics_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("dcfc8ade-7987-40c0-8591-a33c2a603e61"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Logistics_do.aspx?action=move&Logistics_ID=" + entity.Logistics_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public string LogisticsStatus(int Status)
    {
        string Name = "";

        switch(Status)
        {
            case 0:
                Name = "未启用";
                break;

            case 1:
                Name = "启用";
                break;

            default:
                break;
        }
        return Name;
    }


}