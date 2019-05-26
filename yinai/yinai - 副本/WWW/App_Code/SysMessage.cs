using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
/// SysMessage 的摘要说明
/// </summary>
public class SysMessage
{

    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;


    private ITools tools;
    private ISysMessage MyBLL;
    private Public_Class pub;

	public SysMessage()
	{
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysMessageFactory.CreateSysMessage();
        pub = new Public_Class();
	}

    /// <summary>
    /// 推送消息通知
    /// </summary>
    /// <param name="Message_Type">消息类型</param>
    /// <param name="Message_UserType">用户类型</param>
    /// <param name="Message_ReceiveID">消息接收人ID</param>
    /// <param name="Message_SendID">消息发送人ID</param>
    /// <param name="Message_Content">消息内容</param>
    public void SendMessage(int Message_Type, int Message_UserType, int Message_ReceiveID, int Message_SendID, string Message_Content)
    {
        SysMessageInfo entity = new SysMessageInfo();
        entity.Message_ID = 0;
        entity.Message_Type = Message_Type;
        entity.Message_UserType = Message_UserType;
        entity.Message_ReceiveID = Message_ReceiveID;
        entity.Message_SendID = Message_SendID;
        entity.Message_Content = Message_Content;
        entity.Message_Addtime = DateTime.Now;
        entity.Message_Status = 0;
        entity.Message_Site = pub.GetCurrentSite();
        MyBLL.AddSysMessage(entity);
    }

    public int GetMessageNum(int MessageType)
    {
        int MessageNum = 0;
 QueryInfo Query=new QueryInfo ();
 Query.PageSize = 0;
      Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
      if (MessageType==2)
      {
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "2"));
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_Status", "=", "0"));
      }
      else
      {
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", tools.NullInt(Session["member_id"]).ToString()));
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "1"));
          Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_Status", "=", "0"));

      }

      Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_IsHidden", "=", "0"));
      //string sql = "select Message_ID from Sys_Message where DateDiff(dd,Message_Addtime,getdate())=0 ";
      //Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ID", "in", "" + sql + ""));
      IList<SysMessageInfo> SysMessageEntitys = MyBLL.GetSysMessages(Query);
      if (SysMessageEntitys != null)
      {
          //foreach (SysMessageInfo SysMessageEntity in SysMessageEntitys)
          //{
          //    MessageNum++;
          //}
          MessageNum = SysMessageEntitys.Count;
      }

      return MessageNum;

        //GetSysMessages
    }

}