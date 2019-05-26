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
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
/// 配送方式管理类
/// </summary>
public class DeliveryWay
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IDeliveryWay MyBLL;
    private IAddr addrBLL;

    public DeliveryWay()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = DeliveryWayFactory.CreateDeliveryWay();
        addrBLL = AddrFactory.CreateAddr();
    }

    public void AddDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.Form["Delivery_Way_ID"]);
        string Delivery_Way_Name = tools.CheckStr(Request.Form["Delivery_Way_Name"]);
        int Delivery_Way_Sort = tools.CheckInt(Request.Form["Delivery_Way_Sort"]);
        int Delivery_Way_InitialWeight = tools.CheckInt(Request.Form["Delivery_Way_InitialWeight"]);
        int Delivery_Way_UpWeight = tools.CheckInt(Request.Form["Delivery_Way_UpWeight"]);
        int Delivery_Way_FeeType = tools.CheckInt(Request.Form["Delivery_Way_FeeType"]);
        double Delivery_Way_Fee = tools.CheckFloat(Request.Form["Delivery_Way_Fee"]);
        double Delivery_Way_InitialFee = tools.CheckFloat(Request.Form["Delivery_Way_InitialFee"]);
        double Delivery_Way_UpFee = tools.CheckFloat(Request.Form["Delivery_Way_UpFee"]);
        int Delivery_Way_Status = tools.CheckInt(Request.Form["Delivery_Way_Status"]);
        int Delivery_Way_Cod = tools.CheckInt(Request.Form["Delivery_Way_Cod"]);
        string Delivery_Way_Img = tools.CheckStr(Request.Form["Delivery_Way_Img"]);
        string Delivery_Way_Url = tools.CheckStr(Request.Form["Delivery_Way_Url"]);
        string Delivery_Way_Intro = Request.Form["Delivery_Way_Intro"];

        if (Delivery_Way_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写配送方式名称", false, "{back}"); return; }

        DeliveryWayInfo entity = new DeliveryWayInfo();
        entity.Delivery_Way_ID = Delivery_Way_ID;
        entity.Delivery_Way_Name = Delivery_Way_Name;
        entity.Delivery_Way_Sort = Delivery_Way_Sort;
        entity.Delivery_Way_InitialWeight = Delivery_Way_InitialWeight;
        entity.Delivery_Way_UpWeight = Delivery_Way_UpWeight;
        entity.Delivery_Way_FeeType = Delivery_Way_FeeType;
        entity.Delivery_Way_Fee = Delivery_Way_Fee;
        entity.Delivery_Way_InitialFee = Delivery_Way_InitialFee;
        entity.Delivery_Way_UpFee = Delivery_Way_UpFee;
        entity.Delivery_Way_Status = Delivery_Way_Status;
        entity.Delivery_Way_Cod = Delivery_Way_Cod;
        entity.Delivery_Way_Img = Delivery_Way_Img;
        entity.Delivery_Way_Url = Delivery_Way_Url;
        entity.Delivery_Way_Intro = Delivery_Way_Intro;
        entity.Delivery_Way_Site = Public.GetCurrentSite();

        if (MyBLL.AddDeliveryWay(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.Form["Delivery_Way_ID"]);
        string Delivery_Way_Name = tools.CheckStr(Request.Form["Delivery_Way_Name"]);
        int Delivery_Way_Sort = tools.CheckInt(Request.Form["Delivery_Way_Sort"]);
        int Delivery_Way_InitialWeight = tools.CheckInt(Request.Form["Delivery_Way_InitialWeight"]);
        int Delivery_Way_UpWeight = tools.CheckInt(Request.Form["Delivery_Way_UpWeight"]);
        int Delivery_Way_FeeType = tools.CheckInt(Request.Form["Delivery_Way_FeeType"]);
        double Delivery_Way_Fee = tools.CheckFloat(Request.Form["Delivery_Way_Fee"]);
        double Delivery_Way_InitialFee = tools.CheckFloat(Request.Form["Delivery_Way_InitialFee"]);
        double Delivery_Way_UpFee = tools.CheckFloat(Request.Form["Delivery_Way_UpFee"]);
        int Delivery_Way_Status = tools.CheckInt(Request.Form["Delivery_Way_Status"]);
        int Delivery_Way_Cod = tools.CheckInt(Request.Form["Delivery_Way_Cod"]);
        string Delivery_Way_Img = tools.CheckStr(Request.Form["Delivery_Way_Img"]);
        string Delivery_Way_Url = tools.CheckStr(Request.Form["Delivery_Way_Url"]);
        string Delivery_Way_Intro = Request.Form["Delivery_Way_Intro"];

        if (Delivery_Way_Name.Length == 0) { Public.Msg("error", "错误信息", "请填写配送方式名称", false, "{back}"); return; }

        DeliveryWayInfo entity = new DeliveryWayInfo();
        entity.Delivery_Way_ID = Delivery_Way_ID;
        entity.Delivery_Way_Name = Delivery_Way_Name;
        entity.Delivery_Way_Sort = Delivery_Way_Sort;
        entity.Delivery_Way_InitialWeight = Delivery_Way_InitialWeight;
        entity.Delivery_Way_UpWeight = Delivery_Way_UpWeight;
        entity.Delivery_Way_FeeType = Delivery_Way_FeeType;
        entity.Delivery_Way_Fee = Delivery_Way_Fee;
        entity.Delivery_Way_InitialFee = Delivery_Way_InitialFee;
        entity.Delivery_Way_UpFee = Delivery_Way_UpFee;
        entity.Delivery_Way_Status = Delivery_Way_Status;
        entity.Delivery_Way_Cod = Delivery_Way_Cod;
        entity.Delivery_Way_Img = Delivery_Way_Img;
        entity.Delivery_Way_Url = Delivery_Way_Url;
        entity.Delivery_Way_Intro = Delivery_Way_Intro;
        entity.Delivery_Way_Site = Public.GetCurrentSite();


        if (MyBLL.EditDeliveryWay(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        if (MyBLL.DelDeliveryWay(Delivery_Way_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public DeliveryWayInfo GetDeliveryWayByID(int cate_id) {
        return MyBLL.GetDeliveryWayByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetDeliveryWays()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<DeliveryWayInfo> entitys = MyBLL.GetDeliveryWays(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (DeliveryWayInfo entity in entitys)
            {
                jsonBuilder.Append("{\"DeliveryWayInfo.Delivery_Way_ID\":" + entity.Delivery_Way_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Delivery_Way_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Delivery_Way_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Delivery_Way_FeeType == 0) { jsonBuilder.Append(entity.Delivery_Way_Fee); }
                else { jsonBuilder.Append("首重费用：" + Public.DisplayCurrency(entity.Delivery_Way_InitialFee) + " 续重费用：" + Public.DisplayCurrency(entity.Delivery_Way_UpFee)); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Delivery_Way_Status == 1) { jsonBuilder.Append("启用"); }
                else { jsonBuilder.Append("关闭"); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Delivery_Way_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<a href=\\\"district_list.aspx?delivery_way_id=" + entity.Delivery_Way_ID + "\\\">设置区域</a>\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("58d92d67-4e0b-4a4c-bd5c-6062c432554d"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"delivery_way_edit.aspx?delivery_way_id=" + entity.Delivery_Way_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("9909b492-b55c-49bf-b726-0f2d36e7ff4b"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('delivery_way_do.aspx?action=move&delivery_way_id=" + entity.Delivery_Way_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public IList<DeliveryWayInfo> GetDeliveryWaysInfo()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));


        return MyBLL.GetDeliveryWays(Query, Public.GetUserPrivilege());
        
    }

    public IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state, string city, string county)
    {
        return MyBLL.GetDeliveryWaysByDistrict(state, city, county, Public.GetUserPrivilege());
    }

    public void AddDeliveryWayDistrict()
    {
        int District_ID = tools.CheckInt(Request.Form["District_ID"]);
        int District_DeliveryWayID = tools.CheckInt(Request.Form["District_DeliveryWayID"]);
        string District_Country = tools.CheckStr(Request.Form["District_Country"]);
        string District_State = tools.CheckStr(Request.Form["District_State"]);
        string District_City = tools.CheckStr(Request.Form["District_City"]);
        string District_County = tools.CheckStr(Request.Form["District_County"]);

        DeliveryWayDistrictInfo entity = new DeliveryWayDistrictInfo();
        entity.District_ID = District_ID;
        entity.District_DeliveryWayID = District_DeliveryWayID;
        entity.District_Country = District_Country;
        entity.District_State = District_State;
        entity.District_City = District_City;
        entity.District_County = District_County;

        if (MyBLL.AddDeliveryWayDistrict(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "district_list.aspx?delivery_way_id=" + District_DeliveryWayID);
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditDeliveryWayDistrict()
    {
        int District_ID = tools.CheckInt(Request.Form["District_ID"]);
        int District_DeliveryWayID = tools.CheckInt(Request.Form["District_DeliveryWayID"]);
        string District_Country = tools.CheckStr(Request.Form["District_Country"]);
        string District_State = tools.CheckStr(Request.Form["District_State"]);
        string District_City = tools.CheckStr(Request.Form["District_City"]);
        string District_County = tools.CheckStr(Request.Form["District_County"]);

        DeliveryWayDistrictInfo entity = new DeliveryWayDistrictInfo();
        entity.District_ID = District_ID;
        entity.District_DeliveryWayID = District_DeliveryWayID;
        entity.District_Country = District_Country;
        entity.District_State = District_State;
        entity.District_City = District_City;
        entity.District_County = District_County;

        if (MyBLL.EditDeliveryWayDistrict(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "district_list.aspx?delivery_way_id=" + District_DeliveryWayID);
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelDeliveryWayDistrict()
    {
        int District_ID = tools.CheckInt(Request.QueryString["District_ID"]);
        if (MyBLL.DelDeliveryWayDistrict(District_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "district_list.aspx?delivery_way_id=" + Request.QueryString["delivery_way_id"]);
        }
        else {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public DeliveryWayDistrictInfo GetDeliveryWayDistrictByID(int cate_id)
    {
        return MyBLL.GetDeliveryWayDistrictByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetDeliveryWayDistricts() 
    {
        int Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        IList<DeliveryWayDistrictInfo> entitys = MyBLL.GetDeliveryWayDistrictsByDWID(Delivery_Way_ID, Public.GetUserPrivilege());

        if (entitys != null)
        {
            DeliveryWayInfo wayinfo = MyBLL.GetDeliveryWayByID(Delivery_Way_ID, Public.GetUserPrivilege());

            StateInfo stateEntity = null;
            CityInfo cityEntity = null;
            CountyInfo countyEntity = null;

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":1,\"total\":1,\"records\":" + entitys.Count + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (DeliveryWayDistrictInfo entity in entitys)
            {

                stateEntity = addrBLL.GetStateInfoByCode(entity.District_State);
                cityEntity = addrBLL.GetCityInfoByCode(entity.District_City);
                countyEntity = addrBLL.GetCountyInfoByCode(entity.District_County);

                jsonBuilder.Append("{\"DeliveryWayDistrictInfo.District_ID\":" + entity.District_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.District_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (wayinfo == null) { jsonBuilder.Append(Delivery_Way_ID); }
                else { jsonBuilder.Append(wayinfo.Delivery_Way_Name); }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (entity.District_State == "0" || entity.District_State.Length == 0) { jsonBuilder.Append("全部"); }
                else {
                    if (stateEntity == null) { jsonBuilder.Append(entity.District_State); }
                    else { jsonBuilder.Append(stateEntity.State_CN); }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.District_City == "0" || entity.District_City.Length == 0) { jsonBuilder.Append("全部"); }
                else {
                    if (cityEntity == null) { jsonBuilder.Append(entity.District_City); }
                    else { jsonBuilder.Append(cityEntity.City_CN); }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.District_County == "0" || entity.District_County.Length == 0) { jsonBuilder.Append("全部"); }
                else {
                    if (countyEntity == null) { jsonBuilder.Append(entity.District_County); }
                    else { jsonBuilder.Append(countyEntity.County_CN); }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"district_list.aspx?action=renew&delivery_way_id=" + Delivery_Way_ID + "&district_id=" + entity.District_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('district_do.aspx?action=move&delivery_way_id=" + Delivery_Way_ID + "&district_id=" + entity.District_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
