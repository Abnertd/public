using System;

namespace Glaer.Trade.B2C.Model
{
    public class SysCountyInfo
    {
        private int _Sys_County_ID;
        private string _Sys_County_CityCode;
        private string _Sys_County_Code;
        private string _Sys_County_CN;
        private int _Sys_County_IsActive;

        public int Sys_County_ID
        {
            get { return _Sys_County_ID; }
            set { _Sys_County_ID = value; }
        }

        public string Sys_County_CityCode
        {
            get { return _Sys_County_CityCode; }
            set { _Sys_County_CityCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_County_Code
        {
            get { return _Sys_County_Code; }
            set { _Sys_County_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_County_CN
        {
            get { return _Sys_County_CN; }
            set { _Sys_County_CN = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Sys_County_IsActive
        {
            get { return _Sys_County_IsActive; }
            set { _Sys_County_IsActive = value; }
        }

    }
}
