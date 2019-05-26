﻿<%@ Page Language="C#" %>

<script runat="server">

    private Addr addr;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        addr = new Addr();
        string targetDiv, stateName, cityName, countyName, stateCode, cityCode, countyCode;
        string action = Request.QueryString["action"];
        switch (action) {
            case "fill":
                targetDiv = Request.QueryString["targetdiv"];
                stateName = Request.QueryString["statename"];
                cityName = Request.QueryString["cityname"];
                countyName = Request.QueryString["countyname"];
                stateCode = Request.QueryString["statecode"];
                cityCode = Request.QueryString["citycode"];
                countyCode = Request.QueryString["countycode"];
                Response.Write(addr.SelectAddress(targetDiv, stateName, cityName, countyName, stateCode, cityCode, countyCode));
                break;
            case "filluod":
                targetDiv = Request.QueryString["targetdiv"];
                stateName = Request.QueryString["statename"];
                cityName = Request.QueryString["cityname"];
                countyName = Request.QueryString["countyname"];
                stateCode = Request.QueryString["statecode"];
                cityCode = Request.QueryString["citycode"];
                countyCode = Request.QueryString["countycode"];
                Response.Write(addr.UOD_SelectAddress(targetDiv, stateName, cityName, countyName, stateCode, cityCode, countyCode));
                break;

            case "newfill":
                targetDiv = Request.QueryString["targetdiv"];
                stateName = Request.QueryString["statename"];
                cityName = Request.QueryString["cityname"];
                countyName = Request.QueryString["countyname"];
                stateCode = Request.QueryString["statecode"];
                cityCode = Request.QueryString["citycode"];
                countyCode = Request.QueryString["countycode"];
                Response.Write(addr.SelectAddressNew(targetDiv, stateName, cityName, countyName, stateCode, cityCode, countyCode));
                break;
        }
        addr = null;
    }
</script>

