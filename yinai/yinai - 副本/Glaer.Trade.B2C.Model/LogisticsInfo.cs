using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class LogisticsInfo
    {
        private int _Logistics_ID;
        private string _Logistics_NickName;
        private string _Logistics_Password;
        private string _Logistics_CompanyName;
        private string _Logistics_Name;
        private string _Logistics_Tel;
        private int _Logistics_Status;
        private DateTime _Logistics_Addtime;
        private DateTime _Logistics_Lastlogin_Time;

        public int Logistics_ID
        {
            get { return _Logistics_ID; }
            set { _Logistics_ID = value; }
        }

        public string Logistics_NickName
        {
            get { return _Logistics_NickName; }
            set { _Logistics_NickName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Logistics_Password
        {
            get { return _Logistics_Password; }
            set { _Logistics_Password = value.Length > 64 ? value.Substring(0, 64) : value.ToString(); }
        }

        public string Logistics_CompanyName
        {
            get { return _Logistics_CompanyName; }
            set { _Logistics_CompanyName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Logistics_Name
        {
            get { return _Logistics_Name; }
            set { _Logistics_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Logistics_Tel
        {
            get { return _Logistics_Tel; }
            set { _Logistics_Tel = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Logistics_Status
        {
            get { return _Logistics_Status; }
            set { _Logistics_Status = value; }
        }

        public DateTime Logistics_Addtime
        {
            get { return _Logistics_Addtime; }
            set { _Logistics_Addtime = value; }
        }
        public DateTime Logistics_Lastlogin_Time
        {
            get { return _Logistics_Lastlogin_Time; }
            set { _Logistics_Lastlogin_Time = value; }
        } 

    }
}
