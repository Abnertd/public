using System;
using System.Text;
using System.Data;
using System.Configuration;
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
///Addr 的摘要说明
/// </summary>
public class Addr
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    Public_Class pub = new Public_Class();
    IAddr MyBLL;

    public Addr()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = AddrFactory.CreateAddr();
    }

    public string SelectAddress(string targetDiv, string stateName, string cityName, string countyName, string stateCode, string cityCode, string countyCode)
    {
        StringBuilder strHTML = new StringBuilder();

        IList<StateInfo> stateList = null;
        IList<CityInfo> cityList = null;
        IList<CountyInfo> countyList = null;
        try
        {
            stateList = MyBLL.GetStatesByCountry("1");
            cityList = MyBLL.GetCitysByState(stateCode);
            countyList = MyBLL.GetCountysByCity(cityCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //选择省
        if (stateList != null)
        {
            strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"RefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', this.options[this.selectedIndex].value, '', '');\">");
            strHTML.Append("<option value=\"\">----选择省----</option>");
            foreach (StateInfo entity in stateList)
            {
                strHTML.Append("<option value=\"" + entity.State_Code + "\"");
                if (entity.State_Code == stateCode) { strHTML.Append(" selected=\"selected\""); }
                strHTML.Append(">" + entity.State_CN + "</option>");
            }
            strHTML.Append("</select>");
        }

        //选择市/地区
        if (cityList != null)
        {
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\" onchange=\"RefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "',this.options[this.selectedIndex].value, '');\">");
            strHTML.Append("<option value=\"\">----选择市/地区----</option>");
            foreach (CityInfo entity in cityList)
            {
                strHTML.Append("<option value=\"" + entity.City_Code + "\"");
                if (entity.City_Code == cityCode) { strHTML.Append(" selected=\"selected\""); }
                strHTML.Append(">" + entity.City_CN + "</option>");
            }
            strHTML.Append("</select>");
        }
        else
        {
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\">");
            strHTML.Append("<option value=\"\">----选择市/地区----</option>");
            strHTML.Append("</select>");
        }

        //选择区/县
        if (countyList != null)
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\" onchange=\"RefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "', '" + cityCode + "', this.options[this.selectedIndex].value);\">");
            strHTML.Append("<option value=\"\">----选择区/县----</option>");
            foreach (CountyInfo entity in countyList)
            {
                strHTML.Append("<option value=\"" + entity.County_Code + "\"");
                if (entity.County_Code == countyCode) { strHTML.Append(" selected=\"selected\""); }
                strHTML.Append(">" + entity.County_CN + "</option>");
            }
            strHTML.Append("</select>");
        }
        else
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\">");
            strHTML.Append("<option value=\"\">----选择区/县----</option>");
            strHTML.Append("</select>");
        }

        return strHTML.ToString();
    }

    public string DisplayAddress(string stateCode, string cityCode, string countyCode)
    {
        StateInfo stateEntity = null;
        CityInfo cityEntity = null;
        CountyInfo countyEntity = null;
        try
        {
            stateEntity = MyBLL.GetStateInfoByCode(stateCode);
            cityEntity = MyBLL.GetCityInfoByCode(cityCode);
            countyEntity = MyBLL.GetCountyInfoByCode(countyCode);
        }
        catch (Exception ex) { throw ex; }

        string strHTML = string.Empty;

        if (stateEntity != null) { strHTML = stateEntity.State_CN + " "; }
        if (cityEntity != null) { strHTML += cityEntity.City_CN + " "; }
        if (countyEntity != null) { strHTML += countyEntity.County_CN; }

        return strHTML.ToString();
    }

}
