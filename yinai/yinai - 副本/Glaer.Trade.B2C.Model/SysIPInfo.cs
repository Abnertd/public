using System;

namespace Glaer.Trade.B2C.Model
{
    public class SysIPInfo
    {
        private long _onip;
        private string _oniptxt;
        private long _offip;
        private string _offiptxt;
        private int _ProvinceID;
        private int _CityID;
        private int _CountyID;
        private string _country;
        private string _city;

        public long onip
        {
            get { return _onip; }
            set { _onip = value; }
        }

        public string oniptxt
        {
            get { return _oniptxt; }
            set { _oniptxt = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public long offip
        {
            get { return _offip; }
            set { _offip = value; }
        }

        public string offiptxt
        {
            get { return _offiptxt; }
            set { _offiptxt = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int ProvinceID
        {
            get { return _ProvinceID; }
            set { _ProvinceID = value; }
        }

        public int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        public int CountyID
        {
            get { return _CountyID; }
            set { _CountyID = value; }
        }

        public string country
        {
            get { return _country; }
            set { _country = value.Length > 255 ? value.Substring(0, 255) : value.ToString(); }
        }

        public string city
        {
            get { return _city; }
            set { _city = value.Length > 255 ? value.Substring(0, 255) : value.ToString(); }
        }

    }
}
