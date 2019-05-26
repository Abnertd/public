using System;

namespace Glaer.Trade.B2C.Model
{
    /// <summary>
    /// 系统支付方式
    /// </summary>
    public class PayInfo
    {
        private int _Sys_Pay_ID;
        private string _Sys_Pay_Name;
        private string _Sys_Pay_Sign;
        private string _Sys_Pay_Picture;
        private int _Sys_Pay_Sort;
        private int _Sys_Pay_Trash;
        private string _Sys_Pay_Site;

        public int Sys_Pay_ID
        {
            get { return _Sys_Pay_ID; }
            set { _Sys_Pay_ID = value; }
        }

        public string Sys_Pay_Name
        {
            get { return _Sys_Pay_Name; }
            set { _Sys_Pay_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Sys_Pay_Sign
        {
            get { return _Sys_Pay_Sign; }
            set { _Sys_Pay_Sign = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Sys_Pay_Picture
        {
            get { return _Sys_Pay_Picture; }
            set { _Sys_Pay_Picture = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Sys_Pay_Sort
        {
            get { return _Sys_Pay_Sort; }
            set { _Sys_Pay_Sort = value; }
        }

        public int Sys_Pay_Trash
        {
            get { return _Sys_Pay_Trash; }
            set { _Sys_Pay_Trash = value; }
        }

        public string Sys_Pay_Site
        {
            get { return _Sys_Pay_Site; }
            set { _Sys_Pay_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
