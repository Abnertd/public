using System;

namespace Glaer.Trade.B2C.Model
{
    public class AddrInfo
    {
        private StateInfo _StateInfo = null;
        private CityInfo _CityInfo = null;
        private CountyInfo _CountyInfo = null;

        public StateInfo StateInfo {
            get { return _StateInfo; }
            set { _StateInfo = value; }
        }

        public CityInfo CityInfo {
            get { return _CityInfo; }
            set { _CityInfo = value; }
        }

        public CountyInfo CountyInfo {
            get { return _CountyInfo; }
            set { _CountyInfo = value; }
        }

    }

    public class StateInfo 
    {
        private string _State_CountryCode = string.Empty;
        private string _State_Code = string.Empty;
        private string _State_CN = string.Empty;

        public string State_CountryCode {
            get { return _State_CountryCode; }
            set { _State_CountryCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string State_Code {
            get { return _State_Code; }
            set { _State_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string State_CN {
            get { return _State_CN; }
            set { _State_CN = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }
    }

    public class CityInfo
    {
        private string _City_StateCode = string.Empty;
        private string _City_Code = string.Empty;
        private string _City_CN = string.Empty;

        public string City_StateCode
        {
            get { return _City_StateCode; }
            set { _City_StateCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string City_Code
        {
            get { return _City_Code; }
            set { _City_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string City_CN
        {
            get { return _City_CN; }
            set { _City_CN = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }
    }

    public class CountyInfo
    {
        private string _County_CityCode = string.Empty;
        private string _County_Code = string.Empty;
        private string _County_CN = string.Empty;

        public string County_CityCode
        {
            get { return _County_CityCode; }
            set { _County_CityCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string County_Code
        {
            get { return _County_Code; }
            set { _County_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string County_CN
        {
            get { return _County_CN; }
            set { _County_CN = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }
    }

}
