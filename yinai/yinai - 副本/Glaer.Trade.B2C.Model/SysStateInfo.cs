using System;

namespace Glaer.Trade.B2C.Model
{
    public class SysStateInfo
    {
        private int _Sys_State_ID;
        private string _Sys_State_CountryCode;
        private string _Sys_State_Code;
        private string _Sys_State_CN;
        private int _Sys_State_IsActive;

        public int Sys_State_ID
        {
            get { return _Sys_State_ID; }
            set { _Sys_State_ID = value; }
        }

        public string Sys_State_CountryCode
        {
            get { return _Sys_State_CountryCode; }
            set { _Sys_State_CountryCode = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_State_Code
        {
            get { return _Sys_State_Code; }
            set { _Sys_State_Code = value.Length > 8 ? value.Substring(0, 8) : value.ToString(); }
        }

        public string Sys_State_CN
        {
            get { return _Sys_State_CN; }
            set { _Sys_State_CN = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Sys_State_IsActive
        {
            get { return _Sys_State_IsActive; }
            set { _Sys_State_IsActive = value; }
        }

    }
}
