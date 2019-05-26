using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using Glaer.Trade.Util.SQLHelper;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.AD;

/// <summary>
///AD 的摘要说明
/// </summary>
public class AD
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ISQLHelper DBHelper;
    private ITools tools;
    private IAD MyAD;
    private IAD_Position_Channel Mychannel;
    private IADPosition Myposition;
    private Supplier supplier;

    public AD()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        tools = ToolsFactory.CreateTools();
        MyAD = ADFactory.CreateAD();
        Mychannel = AD_Position_ChannelFactory.CreateAD_Position_Channel();
        Myposition = ADPositionFactory.CreateADPosition();
        supplier = new Supplier();
    }

    //添加广告频道
    public void AddAD_Position_Channel()
    {
        int AD_Position_Channel_ID = tools.CheckInt(Request.Form["AD_Position_Channel_ID"]);
        string AD_Position_Channel_Name = tools.CheckStr(Request.Form["AD_Position_Channel_Name"]);
        string AD_Position_Channel_Note = tools.CheckStr(Request.Form["AD_Position_Channel_Note"]);
        string AD_Position_Channel_Site = Public.GetCurrentSite();

        if (AD_Position_Channel_Name == "") {
            Public.Msg("error", "错误信息", "请填写频道名称", false, "{back}");
        }

        ADPositionChannelInfo entity = new ADPositionChannelInfo();
        entity.AD_Position_Channel_ID = AD_Position_Channel_ID;
        entity.AD_Position_Channel_Name = AD_Position_Channel_Name;
        entity.AD_Position_Channel_Note = AD_Position_Channel_Note;
        entity.AD_Position_Channel_Site = AD_Position_Channel_Site;

        if (Mychannel.AddAD_Position_Channel(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改广告频道
    public void EditAD_Position_Channel()
    {

        int AD_Position_Channel_ID = tools.CheckInt(Request.Form["AD_Position_Channel_ID"]);
        string AD_Position_Channel_Name = tools.CheckStr(Request.Form["AD_Position_Channel_Name"]);
        string AD_Position_Channel_Note = tools.CheckStr(Request.Form["AD_Position_Channel_Note"]);
        string AD_Position_Channel_Site = Public.GetCurrentSite();

        if (AD_Position_Channel_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写频道名称", false, "{back}");
        }

        ADPositionChannelInfo entity = new ADPositionChannelInfo();
        entity.AD_Position_Channel_ID = AD_Position_Channel_ID;
        entity.AD_Position_Channel_Name = AD_Position_Channel_Name;
        entity.AD_Position_Channel_Note = AD_Position_Channel_Note;
        entity.AD_Position_Channel_Site = AD_Position_Channel_Site;


        if (Mychannel.EditAD_Position_Channel(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除广告频道
    public void DelAD_Position_Channel()
    {
        int cate_id = tools.CheckInt(Request.QueryString["channel_id"]);
        if (Mychannel.DelAD_Position_Channel(cate_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取频道
    public ADPositionChannelInfo GetAD_Position_ChannelByID(int channel_id)
    {
        return Mychannel.GetAD_Position_ChannelByID(channel_id, Public.GetUserPrivilege());
    }

    //选择位置频道
    public void Select_Position_Channel(string obj_name,int channel_id)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"0\">选择频道</option>");
        
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionChannelInfo.AD_Position_Channel_ID", "Desc"));
        IList<ADPositionChannelInfo> Channels = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (Channels != null)
        {            
            foreach (ADPositionChannelInfo entity in Channels)
            {
                if(entity.AD_Position_Channel_ID==channel_id)
                {
                    Response.Write("<option value=\""+entity.AD_Position_Channel_ID+"\" selected>"+entity.AD_Position_Channel_Name+"</option>");
                }
                else
                {
                    Response.Write("<option value=\""+entity.AD_Position_Channel_ID+"\">"+entity.AD_Position_Channel_Name+"</option>");
                }
            }
            
        }
        Response.Write("</select>");
    }

    //获取广告频道
    public string GetAdPositionChannels()
    {


        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Site", "=", Public.GetCurrentSite()));
        string keyword = tools.CheckStr(Request["keyword"]);

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Mychannel.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADPositionChannelInfo> Channels = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (Channels != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ADPositionChannelInfo entity in Channels)
            {
                jsonBuilder.Append("{\"ADPositionChannelInfo.AD_Position_Channel_ID\":" + entity.AD_Position_Channel_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.AD_Position_Channel_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.AD_Position_Channel_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("c6dba721-72aa-4ca4-86fe-2306566e17eb"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_position_channel_edit.aspx?channel_id=" + entity.AD_Position_Channel_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("9adc558d-446c-41cc-a092-bd1313d855e8"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_position_channel_do.aspx?action=move&channel_id=" + entity.AD_Position_Channel_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //添加广告位置
    public void AddAD_Position()
    {
        int Ad_Position_ID = tools.CheckInt(Request.Form["Ad_Position_ID"]);
        string Ad_Position_Name = tools.CheckStr(Request.Form["Ad_Position_Name"]);
        int Ad_Position_ChannelID = tools.CheckInt(Request.Form["Ad_Position_ChannelID"]);
        string Ad_Position_Value = tools.CheckStr(Request.Form["Ad_Position_Value"]);
        int Ad_Position_Width = tools.CheckInt(Request.Form["Ad_Position_Width"]);
        int Ad_Position_Height = tools.CheckInt(Request.Form["Ad_Position_Height"]);
        int Ad_Position_IsActive = tools.CheckInt(Request.Form["Ad_Position_IsActive"]);
        string Ad_Position_Site = Public.GetCurrentSite();

        int U_Ad_Position_Marketing = tools.CheckInt(Request.Form["U_Ad_Position_Marketing"]);
        double U_Ad_Position_Price = tools.CheckFloat(Request.Form["U_Ad_Position_Price"]);

        if (Ad_Position_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写位置名称", false, "{back}");
        }
        if (Ad_Position_Value == "")
        {
            Public.Msg("error", "错误信息", "请填写位置代号", false, "{back}");
        }

        ADPositionInfo entity = new ADPositionInfo();
        entity.Ad_Position_ID = Ad_Position_ID;
        entity.Ad_Position_Name = Ad_Position_Name;
        entity.Ad_Position_ChannelID = Ad_Position_ChannelID;
        entity.Ad_Position_Value = Ad_Position_Value;
        entity.Ad_Position_Width = Ad_Position_Width;
        entity.Ad_Position_Height = Ad_Position_Height;
        entity.Ad_Position_IsActive = Ad_Position_IsActive;
        entity.Ad_Position_Site = Ad_Position_Site;
        entity.U_Ad_Position_Marketing = U_Ad_Position_Marketing;
        entity.U_Ad_Position_Price = U_Ad_Position_Price;

        if (Myposition.AddAD_Position(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ad_position_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改广告位置
    public void EditAD_Position()
    {

        int Ad_Position_ID = tools.CheckInt(Request.Form["Ad_Position_ID"]);
        string Ad_Position_Name = tools.CheckStr(Request.Form["Ad_Position_Name"]);
        int Ad_Position_ChannelID = tools.CheckInt(Request.Form["Ad_Position_ChannelID"]);
        string Ad_Position_Value = tools.CheckStr(Request.Form["Ad_Position_Value"]);
        int Ad_Position_Width = tools.CheckInt(Request.Form["Ad_Position_Width"]);
        int Ad_Position_Height = tools.CheckInt(Request.Form["Ad_Position_Height"]);
        int Ad_Position_IsActive = tools.CheckInt(Request.Form["Ad_Position_IsActive"]);
        string Ad_Position_Site = Public.GetCurrentSite();
        int U_Ad_Position_Marketing = tools.CheckInt(Request.Form["U_Ad_Position_Marketing"]);
        double U_Ad_Position_Price = tools.CheckFloat(Request.Form["U_Ad_Position_Price"]);

        if (Ad_Position_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写位置名称", false, "{back}");
        }
        if (Ad_Position_Value == "")
        {
            Public.Msg("error", "错误信息", "请填写位置代号", false, "{back}");
        }

        ADPositionInfo entity = new ADPositionInfo();
        entity.Ad_Position_ID = Ad_Position_ID;
        entity.Ad_Position_Name = Ad_Position_Name;
        entity.Ad_Position_ChannelID = Ad_Position_ChannelID;
        entity.Ad_Position_Value = Ad_Position_Value;
        entity.Ad_Position_Width = Ad_Position_Width;
        entity.Ad_Position_Height = Ad_Position_Height;
        entity.Ad_Position_IsActive = Ad_Position_IsActive;
        entity.Ad_Position_Site = Ad_Position_Site;
        entity.U_Ad_Position_Marketing = U_Ad_Position_Marketing;
        entity.U_Ad_Position_Price = U_Ad_Position_Price;

        if (Myposition.EditAD_Position(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ad_position.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除广告位置
    public void DelAD_Position()
    {
        int position_id = tools.CheckInt(Request.QueryString["position_id"]);
        if (Myposition.DelAD_Position(position_id, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ad_position.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取广告位置
    public ADPositionInfo GetAD_PositionByID(int cate_id)
    {
        return Myposition.GetAD_PositionByID(cate_id, Public.GetUserPrivilege());
    }

    //获取广告位置
    public ADPositionInfo GetAD_PositionByValue(string AD_Kind)
    {
        return Myposition.GetAD_PositionByValue(AD_Kind, Public.GetUserPrivilege());
    }

    //获取广告位置
    public string GetAdPositions()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int Ad_Channel = tools.CheckInt(Request["Ad_Channel"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        if (Ad_Channel > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADPositionInfo.Ad_Position_ChannelID", "=", Ad_Channel.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ADPositionInfo.Ad_Position_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ADPositionInfo.Ad_Position_Value", "like", keyword));
        }

        string Marketing = tools.CheckStr(Request["Marketing"]);
        if (Marketing == "1" || Marketing == "0")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.U_Ad_Position_Marketing", "=", Marketing));

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Myposition.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ADPositionInfo entity in Positions)
            {
                jsonBuilder.Append("{\"ADPositionInfo.AD_Position_ID\":" + entity.Ad_Position_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Position_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Position_Value));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ADPositionChannelInfo channel = Mychannel.GetAD_Position_ChannelByID(entity.Ad_Position_ChannelID, Public.GetUserPrivilege());
                if (channel != null)
                {
                    jsonBuilder.Append(channel.AD_Position_Channel_Name);
                }
                else
                {
                    jsonBuilder.Append("&nbsp;");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_Width);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_Height);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.DisplayCurrency(entity.U_Ad_Position_Price));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("afbc3245-62b5-4eb3-aefb-c6c8f3e2b02d"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_position_edit.aspx?position_id=" + entity.Ad_Position_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("67c30881-650c-4f84-aa81-08e2e379798c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_position_do.aspx?action=move&position_id=" + entity.Ad_Position_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //选择广告位置
    public void Select_AD_Position(string obj_name, string position_value)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"\">选择位置</option>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {

            foreach (ADPositionInfo entity in Positions)
            {
                if (entity.Ad_Position_Value == position_value)
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\" selected>" + entity.Ad_Position_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\">" + entity.Ad_Position_Name + "</option>");
                }
            }

        }
        Response.Write("</select>");
    }


    public void Select_AD_Position1(string obj_name, string position_value, int channel_id)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"\">选择位置</option>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_ChannelID", "=", channel_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            foreach (ADPositionInfo entity in Positions)
            {
                if (entity.Ad_Position_Value == position_value)
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\" selected>" + entity.Ad_Position_Name + "（" + entity.Ad_Position_Width + "*" + entity.Ad_Position_Height + "）</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\">" + entity.Ad_Position_Name + "（" + entity.Ad_Position_Width + "*" + entity.Ad_Position_Height + "）</option>");
                }
            }
        }
        Response.Write("</select>");
    }

    //广告添加
    public void AddAD()
    {
        int Ad_ID = tools.CheckInt(Request.Form["Ad_ID"]);
        string Ad_Title = tools.CheckStr(Request.Form["Ad_Title"]);
        string Ad_Kind = tools.CheckStr(Request.Form["Ad_Kind"]);
        int Ad_MediaKind = tools.CheckInt(Request.Form["Ad_MediaKind"]);
        string Ad_Media = tools.CheckStr(Request.Form["Ad_Media"]);
        string Ad_Mediacode = Request.Form["Ad_Mediacode"];
        string Ad_Link = tools.CheckStr(Request.Form["Ad_Link"]);
        int Ad_Show_Freq = tools.CheckInt(Request.Form["Ad_Show_Freq"]);
        int Ad_Show_times = 0;
        int Ad_Hits = 0;
        DateTime Ad_StartDate = tools.NullDate(Request.Form["Ad_StartDate"]);
        DateTime Ad_EndDate = tools.NullDate(Request.Form["Ad_EndDate"]);
        int Ad_IsContain = tools.CheckInt(Request.Form["Ad_IsContain"]);
        string Ad_Propertys = tools.CheckStr(Request.Form["Ad_Propertys"]);
        int Ad_Sort = tools.CheckInt(Request.Form["Ad_Sort"]);
        int Ad_IsActive = tools.CheckInt(Request.Form["Ad_IsActive"]);
        string Ad_Site = Public.GetCurrentSite();
        int U_Ad_Audit = 1;

        if(Ad_MediaKind == 4)
        {
            Ad_Media=Ad_Mediacode;
        }
        if (Ad_MediaKind == 1)
        {
            Ad_Media = "";
        }

        if (Ad_Title == "")
        {
            Public.Msg("error", "错误信息", "请填写广告名称", false, "{back}");
        }
        if (Ad_Kind == "")
        {
            Public.Msg("error", "错误信息", "请选择广告位置", false, "{back}");
        }
        if (Request.Form["Ad_EndDate"] == "" || Request.Form["Ad_EndDate"] == "")
        {
            Public.Msg("error", "错误信息", "请选择广告起止时间", false, "{back}");
        }
        if ((Ad_MediaKind >1 ) && Ad_Media=="")
        {
            Public.Msg("error", "错误信息", "请设置广告媒体", false, "{back}");
        }

        Ad_Propertys = "|" + Ad_Propertys + "|";
        

        ADInfo entity = new ADInfo();
        entity.Ad_ID = Ad_ID;
        entity.Ad_Title = Ad_Title;
        entity.Ad_Kind = Ad_Kind;
        entity.Ad_MediaKind = Ad_MediaKind;
        entity.Ad_Media = Ad_Media;
        entity.Ad_Link = Ad_Link;
        entity.Ad_Show_Freq = Ad_Show_Freq;
        entity.Ad_Show_times = Ad_Show_times;
        entity.Ad_Hits = Ad_Hits;
        entity.Ad_StartDate = Ad_StartDate;
        entity.Ad_EndDate = Ad_EndDate;
        entity.Ad_IsContain = Ad_IsContain;
        entity.Ad_Propertys = Ad_Propertys;
        entity.Ad_Sort = Ad_Sort;
        entity.Ad_IsActive = Ad_IsActive;
        entity.Ad_Site = Ad_Site;
        entity.U_Ad_Audit = U_Ad_Audit;
        entity.Ad_Addtime = DateTime.Now;

        if (MyAD.AddAD(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //广告修改
    public void EditAD()
    {

        int Ad_ID = tools.CheckInt(Request.Form["Ad_ID"]);
        string Ad_Title = tools.CheckStr(Request.Form["Ad_Title"]);
        string Ad_Kind = tools.CheckStr(Request.Form["Ad_Kind"]);
        int Ad_MediaKind = tools.CheckInt(Request.Form["Ad_MediaKind"]);
        string Ad_Media = tools.CheckStr(Request.Form["Ad_Media"]);
        string Ad_Mediacode = Request.Form["Ad_Mediacode"];
        string Ad_Link = tools.CheckStr(Request.Form["Ad_Link"]);
        int Ad_Show_Freq = tools.CheckInt(Request.Form["Ad_Show_Freq"]);
        int Ad_Show_times = tools.CheckInt(Request.Form["Ad_Show_times"]);
        int Ad_Hits = tools.CheckInt(Request.Form["Ad_Hits"]);
        DateTime Ad_StartDate = tools.NullDate(Request.Form["Ad_StartDate"]);
        DateTime Ad_EndDate = tools.NullDate(Request.Form["Ad_EndDate"]);
        int Ad_IsContain = tools.CheckInt(Request.Form["Ad_IsContain"]);
        string Ad_Propertys = tools.CheckStr(Request.Form["Ad_Propertys"]);
        int Ad_Sort = tools.CheckInt(Request.Form["Ad_Sort"]);
        int Ad_IsActive = tools.CheckInt(Request.Form["Ad_IsActive"]);
        string Ad_Site = Public.GetCurrentSite();
        int U_Ad_Audit = tools.CheckInt(Request.Form["U_Ad_Audit"]);

        if (Ad_MediaKind == 4)
        {
            Ad_Media = Ad_Mediacode;
        }
        if (Ad_MediaKind == 1)
        {
            Ad_Media = "";
        }

        if (Ad_Title == "")
        {
            Public.Msg("error", "错误信息", "请填写广告名称", false, "{back}");
        }
        if (Ad_Kind == "")
        {
            Public.Msg("error", "错误信息", "请选择广告位置", false, "{back}");
        }
        if (Request.Form["Ad_EndDate"] == "" || Request.Form["Ad_EndDate"] == "")
        {
            Public.Msg("error", "错误信息", "请选择广告起止时间", false, "{back}");
        }
        if ((Ad_MediaKind > 1) && Ad_Media == "")
        {
            Public.Msg("error", "错误信息", "请设置广告媒体", false, "{back}");
        }
        Ad_Propertys = "|" + Ad_Propertys + "|";

        ADInfo entity = GetADByID(Ad_ID);
        if (entity != null)
        {
            entity.Ad_ID = Ad_ID;
            entity.Ad_Title = Ad_Title;
            entity.Ad_Kind = Ad_Kind;
            entity.Ad_MediaKind = Ad_MediaKind;
            entity.Ad_Media = Ad_Media;
            entity.Ad_Link = Ad_Link;
            entity.Ad_Show_Freq = Ad_Show_Freq;
            entity.Ad_Show_times = Ad_Show_times;
            entity.Ad_Hits = Ad_Hits;
            entity.Ad_StartDate = Ad_StartDate;
            entity.Ad_EndDate = Ad_EndDate;
            entity.Ad_IsContain = Ad_IsContain;
            entity.Ad_Propertys = Ad_Propertys;
            entity.Ad_Sort = Ad_Sort;
            entity.Ad_IsActive = Ad_IsActive;
            entity.Ad_Site = Ad_Site;



            if (MyAD.EditAD(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "AD.aspx");
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

    //广告删除
    public void DelAD()
    {
        int AD_ID = tools.CheckInt(Request.QueryString["AD_ID"]);
        if (MyAD.DelAD(AD_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "AD.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取广告
    public ADInfo GetADByID(int cate_id)
    {
        return MyAD.GetADByID(cate_id, Public.GetUserPrivilege());
    }

    //获取广告
    public string GetAds()
    {
        ADPositionInfo positioninfo=null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        string Ad_Kind = tools.CheckStr(Request["Ad_Kind"]);
        int ad_channel_id = tools.CheckInt(Request["ad_channel_id"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.U_Ad_Advertiser", "=", "0"));
        if (Ad_Kind.Length >0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "=", Ad_Kind));
        }
        if (Ad_Kind.Length == 0 & ad_channel_id != 0)
        {
            string ad_kind_str = GetADKinds(ad_channel_id);

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "in", ad_kind_str));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Title", "like", keyword));
        }

        string Audit = tools.CheckStr(Request["Audit"]);
        if (Audit == "1" || Audit == "0")
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.U_Ad_Audit", "=", Audit));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyAD.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADInfo> Ads = MyAD.GetADs(Query, Public.GetUserPrivilege());
        if (Ads != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            Supplier Supplier = new Supplier();

            foreach (ADInfo entity in Ads)
            {
                jsonBuilder.Append("{\"ADInfo.AD_ID\":" + entity.Ad_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                positioninfo= Myposition.GetAD_PositionByValue(entity.Ad_Kind, Public.GetUserPrivilege());
                if (positioninfo != null)
                {
                    jsonBuilder.Append(positioninfo.Ad_Position_Name);
                }
                else
                {
                    jsonBuilder.Append(entity.Ad_Kind);
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ad_MediaKind == 1)
                {
                    jsonBuilder.Append("文字");
                }
                else if(entity.Ad_MediaKind==2)
                {
                jsonBuilder.Append("图片");
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    jsonBuilder.Append("Flash");
                }
                else
                {
                    jsonBuilder.Append("富媒体");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_Freq);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_times);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Hits);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_StartDate.ToShortDateString() + " - " + entity.Ad_EndDate.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(" <img src=\\\""+Public.FormatImgURL(entity.Ad_Media,"fullpath")+"\\\" width=\\\"50\\\" height=\\\"10\\\" >");
                jsonBuilder.Append("\",");
                
                jsonBuilder.Append("\"");
                

                if (Public.CheckPrivilege("c47f67fa-1142-459d-b466-e3216848ff9c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_edit.aspx?ad_id=" + entity.Ad_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("6087aa59-bd66-4eb5-8fb0-f72da294b1ae"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_do.aspx?action=move&ad_id=" + entity.Ad_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //获取广告推广申请
    public string GetAd_Applys()
    {
        ADPositionInfo positioninfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        string Ad_Kind = tools.CheckStr(Request["Ad_Kind"]);
        int ad_channel_id = tools.CheckInt(Request["ad_channel_id"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.U_Ad_Advertiser", ">", "0"));
        if (Ad_Kind.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "=", Ad_Kind));
        }
        if (Ad_Kind.Length == 0 & ad_channel_id != 0)
        {
            string ad_kind_str = GetADKinds(ad_channel_id);
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "in", ad_kind_str));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ADInfo.Ad_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ADInfo.U_Ad_Advertiser", "in", "select supplier_id from supplier where Supplier_CompanyName like '%"+keyword+"%'"));
        }

        string Audit = tools.CheckStr(Request["Audit"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.U_Ad_Audit", "=", Audit));

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyAD.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADInfo> Ads = MyAD.GetADs(Query, Public.GetUserPrivilege());
        if (Ads != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            Supplier Supplier = new Supplier();
            SupplierInfo SupplierEntity;

            foreach (ADInfo entity in Ads)
            {
                jsonBuilder.Append("{\"id\":" + entity.Ad_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                positioninfo = Myposition.GetAD_PositionByValue(entity.Ad_Kind, Public.GetUserPrivilege());
                if (positioninfo != null)
                {
                    jsonBuilder.Append(positioninfo.Ad_Position_Name);
                }
                else
                {
                    jsonBuilder.Append(entity.Ad_Kind);
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ad_MediaKind == 1)
                {
                    jsonBuilder.Append("文字");
                }
                else if (entity.Ad_MediaKind == 2)
                {
                    jsonBuilder.Append("图片");
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    jsonBuilder.Append("Flash");
                }
                else
                {
                    jsonBuilder.Append("富媒体");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_Freq);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_times);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Hits);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_StartDate.ToShortDateString() + " - " + entity.Ad_EndDate.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.U_Ad_Audit == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else if (entity.U_Ad_Audit == 2)
                {
                    jsonBuilder.Append("审核不通过");
                }
                else
                {
                    jsonBuilder.Append("未审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierEntity = Supplier.GetSupplierByID(entity.U_Ad_Advertiser);
                if (SupplierEntity != null)
                    jsonBuilder.Append(SupplierEntity.Supplier_CompanyName);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"ad_view.aspx?ad_id=" + entity.Ad_ID + "\\\" title=\\\"查看\\\">查看</a>");

                if (Public.CheckPrivilege("c47f67fa-1142-459d-b466-e3216848ff9c") && entity.U_Ad_Audit == 0)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_edit.aspx?ad_id=" + entity.Ad_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("6087aa59-bd66-4eb5-8fb0-f72da294b1ae") && entity.U_Ad_Audit != 1)
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_do.aspx?action=move&ad_id=" + entity.Ad_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    //根据频道获取广告位置代码
    public string GetADKinds(int id)
    {
        string ad_kinds = "'0'";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_ChannelID", "=", id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));

        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            foreach (ADPositionInfo entity in Positions)
            {
                ad_kinds += "," + "'" + entity.Ad_Position_Value + "'";
            }
        }
        return ad_kinds;

    }

    //添广告频道再添位置
    public string AD_Position_Select(int value)
    {
        string select_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        IList<ADPositionChannelInfo> adpositionchannelinfos = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (adpositionchannelinfos != null)
        {
            select_list += "<select name=\"adpositionchannel\" id=\"adpositionchannel\" onchange=\"getChannelID(this.options[this.selectedIndex].value);\">";
            select_list += "<option value=\"0\">选择频道</option>";
            foreach (ADPositionChannelInfo entity in adpositionchannelinfos)
            {
                if (value == entity.AD_Position_Channel_ID)
                {
                    select_list += "<option value=\"" + entity.AD_Position_Channel_ID + "\" selected>" + entity.AD_Position_Channel_Name + "</option>";
                }
                else
                {
                    select_list += "<option value=\"" + entity.AD_Position_Channel_ID + "\">" + entity.AD_Position_Channel_Name + "</option>";
                }
            }
            select_list += "</select>";
        }
        return select_list;
    }

    //根据位置代码获取广告频道
    public int GetAdPositionByKind(string kind)
    {
        int Ad_Position_ChannelID = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Value", "=", kind));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));

        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            foreach (ADPositionInfo entity in Positions)
            {
                Ad_Position_ChannelID = entity.Ad_Position_ChannelID;
            }
        }
        return Ad_Position_ChannelID;
    }

    //广告推广审核
    public void ADApply_Audit_Edit(int status)
    {
        string ad_id = tools.CheckStr(Request["ad_id"]);
        if (ad_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的申请信息", false, "{back}");
            return;
        }

        ADPositionInfo AdPositionEntity = null;
        if (tools.Left(ad_id, 1) == ",") { ad_id = ad_id.Remove(0, 1); }

        ProductDenyReasonInfo reasoninfo;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.Ad_ID", "in", ad_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.U_Ad_Audit", "=","0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.U_Ad_Advertiser", ">", "0"));
        Query.OrderInfos.Add(new OrderInfo("ADInfo.Ad_ID", "DESC"));
        IList<ADInfo> entitys = MyAD.GetADs(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (ADInfo entity in entitys)
            {
                if (status == 1)
                {
                    AdPositionEntity = Myposition.GetAD_PositionByValue(entity.Ad_Kind, Public.GetUserPrivilege());
                    if (AdPositionEntity == null)
                        AdPositionEntity = new ADPositionInfo();


                    double DeductMoney = ((entity.Ad_EndDate - entity.Ad_StartDate).Days + 1) * AdPositionEntity.U_Ad_Position_Price;
                    if (supplier.GetSupplierAdvAccount(entity.U_Ad_Advertiser) >= DeductMoney)
                    {
                        supplier.Supplier_Account_Log(entity.U_Ad_Advertiser, 2, Math.Round(0 - DeductMoney, 2), "广告服务费");
                    }
                    else
                    {
                        Public.Msg("error", "错误信息", "账户余额不足", false, "{back}");
                        break;
                    }
                }
                entity.U_Ad_Audit = status;
                MyAD.EditAD(entity, Public.GetUserPrivilege());
            }
        }

        Response.Redirect("/ad/ad_apply.aspx");

    }


}

