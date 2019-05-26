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
            strHTML.Append("<option value=\"\">选择省</option>");
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
            strHTML.Append("<option value=\"\">选择市/地区</option>");
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
            strHTML.Append("<option value=\"\">选择市/地区</option>");
            strHTML.Append("</select>");
        }

        //选择区/县
        if (countyList != null)
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\" onchange=\"RefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "', '" + cityCode + "', this.options[this.selectedIndex].value);\">");
            strHTML.Append("<option value=\"\">选择区/县</option>");
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
            strHTML.Append("<option value=\"\">选择区/县</option>");
            strHTML.Append("</select>");
        }

        return strHTML.ToString();
    }

    public string SelectAddressNew(string targetDiv, string stateName, string cityName, string countyName, string stateCode, string cityCode, string countyCode)
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
            //strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', this.options[this.selectedIndex].value, '', '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', this.options[this.selectedIndex].value, '', '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择省</option>");
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
          
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "',this.options[this.selectedIndex].value, '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
            strHTML.Append("</select>");
        }

        //选择区/县
        if (countyList != null)
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "', '" + cityCode + "', this.options[this.selectedIndex].value);\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择区/县</option>");
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
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\"  style=\" margin-right:5px;\">");
            strHTML.Append("<option value=\"\">选择区/县</option>");
            strHTML.Append("</select>");
        }

        return strHTML.ToString();
    }

    public string SelectProductState(string targetDiv, string stateName, string cityName, string countyName, string stateCode, string cityCode, string countyCode)
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
            strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', this.options[this.selectedIndex].value, '', '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择省</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "',this.options[this.selectedIndex].value, '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
            strHTML.Append("</select>");
        }

        //选择区/县
        if (countyList != null)
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\" onchange=\"RefillAddressNew('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "', '" + cityCode + "', this.options[this.selectedIndex].value);\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择区/县</option>");
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
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择区/县</option>");
            strHTML.Append("</select>");
        }

        return strHTML.ToString();
    }

    public string SelectAddressSecond(string targetDiv, string stateName, string cityName,  string stateCode, string cityCode)
    {
        StringBuilder strHTML = new StringBuilder();

        IList<StateInfo> stateList = null;
        IList<CityInfo> cityList = null;
        try
        {
            stateList = MyBLL.GetStatesByCountry("1");
            cityList = MyBLL.GetCitysByState(stateCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //选择省
        if (stateList != null)
        {
            strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"RefillAddressSecond('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '', this.options[this.selectedIndex].value, '', '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择省</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\" onchange=\"RefillAddressSecond('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '', '" + stateCode + "',this.options[this.selectedIndex].value, '');\" style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\"  style=\" margin-right:10px;\">");
            strHTML.Append("<option value=\"\">选择市/地区</option>");
            strHTML.Append("</select>");
        }


        return strHTML.ToString();
    }
    public string SelectAddressDelivery(string targetDiv, string stateName, string cityName, string countyName, string stateCode, string cityCode, string countyCode)
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
            strHTML.Append("<select name=\"s_" + stateName + "\" id=\"s_" + stateName + "\" onchange=\"DeliveryRefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', this.options[this.selectedIndex].value, '', '');\">");
            strHTML.Append("<option value=\"\">----全部省----</option>");
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
            strHTML.Append("<select name=\"s_" + cityName + "\" id=\"s_" + cityName + "\" onchange=\"DeliveryRefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "',this.options[this.selectedIndex].value, '');\">");
            strHTML.Append("<option value=\"\">----全部市/地区----</option>");
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
            strHTML.Append("<option value=\"\">----全部市/地区----</option>");
            strHTML.Append("</select>");
        }

        //选择区/县
        if (countyList != null)
        {
            strHTML.Append("<select name=\"s_" + countyName + "\" id=\"s_" + countyName + "\" onchange=\"DeliveryRefillAddress('" + targetDiv + "', '" + stateName + "', '" + cityName + "', '" + countyName + "', '" + stateCode + "', '" + cityCode + "', this.options[this.selectedIndex].value);\">");
            strHTML.Append("<option value=\"\">----全部区/县----</option>");
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
            strHTML.Append("<option value=\"\">----全部区/县----</option>");
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


    public string DisplayAddresses(string stateCode, string cityCode, string countyCode)
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

    /// <summary>
    /// 根据ip地址转换为IP库值
    /// </summary>
    /// <returns>ip地址对应IP库值</returns>
    public long Get_IPNum()
    {
        string strIPAddr = "";
        strIPAddr = tools.NullStr(Request.ServerVariables["Remote_Addr"]);
        //strIPAddr = "124.232.143.19 ";
        long lnResults = 0;
        long lnIndex = 0;
        string[] lnIpAry = null;
        lnIpAry = strIPAddr.Split('.');

        for (lnIndex = 0; lnIndex <= 3; lnIndex++)
        {
            if (!(lnIndex == 3))
            {
                lnIpAry[lnIndex] = (tools.CheckInt(lnIpAry[lnIndex]) * (Math.Pow(256, (3 - lnIndex)))).ToString();
            }
            lnResults = lnResults + Convert.ToInt64(lnIpAry[lnIndex]);
        }
        return lnResults;

    }

    /// <summary>
    /// 根据IP获取ip库记录实体
    /// </summary>
    /// <returns>ip库记录实体</returns>
    public SysIPInfo Get_CityByIP()
    {
        SysIPInfo ipinfo = new SysIPInfo();
        long ipnum = Get_IPNum();
        int cityid = 0;
        IList<SysIPInfo> entitys = MyBLL.GetSysIPs(ipnum);
        if (entitys != null)
        {
            ipinfo = entitys[0];
        }
        if (ipinfo.CityID == 0)
        {
            ipinfo.CityID = 1;
        }
        if (ipinfo.ProvinceID == 1)
        {
            ipinfo.ProvinceID = 1;
        }
        return ipinfo;
    }

    /// <summary>
    /// 根据省市编码获取省市名称
    /// </summary>
    /// <param name="City_Code">城市编码</param>
    /// <returns>城市名称</returns>
    public string Get_State_Name(string State_Code)
    {
        string State_Name = "";
        StateInfo entity = MyBLL.GetStateInfoByCode(State_Code);
        if (entity != null)
        {
            State_Name = entity.State_CN;
        }
        return State_Name;
    }

    /// <summary>
    /// 根据城市编码获取城市名称
    /// </summary>
    /// <param name="City_Code">城市编码</param>
    /// <returns>城市名称</returns>
    public string Get_City_Name(string City_Code)
    {
        string City_Name = "";
        CityInfo entity = MyBLL.GetCityInfoByCode(City_Code);
        if (entity != null)
        {
            City_Name = entity.City_CN;
        }
        return City_Name;
    }



    /// <summary>
    /// 根据县区编码获取县区名称
    /// </summary>
    /// <param name="City_Code">城市编码</param>
    /// <returns>城市名称</returns>
    public string Get_County_Name(string County_Code)
    {
        string County_Name = "";
        CountyInfo entity = MyBLL.GetCountyInfoByCode(County_Code);
        if (entity != null)
        {
            County_Name = entity.County_CN;
        }
        return County_Name;
    }
}
