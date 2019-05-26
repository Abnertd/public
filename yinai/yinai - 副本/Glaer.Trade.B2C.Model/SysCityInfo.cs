using System;

namespace Glaer.Trade.B2C.Model
{
    public class SysCityInfo
    {
        private int _Sys_City_ID;
        private string _Sys_City_StateCode;
        private string _Sys_City_Code;
        private string _Sys_City_CN;
        private int _Sys_City_IsActive;

        public int Sys_City_ID
        {
            get { return _Sys_City_ID; }
            set { _Sys_City_ID = value; }
        }

        public string Sys_City_StateCode
        {
            get { return _Sys_City_StateCode; }
            set { _Sys_City_StateCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_City_Code
        {
            get { return _Sys_City_Code; }
            set { _Sys_City_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_City_CN
        {
            get { return _Sys_City_CN; }
            set { _Sys_City_CN = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Sys_City_IsActive
        {
            get { return _Sys_City_IsActive; }
            set { _Sys_City_IsActive = value; }
        }

    }
}
