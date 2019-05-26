using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierAgentProtocalInfo
    {
        private int _Protocal_ID;
        private string _Protocal_Code;
        private int _Protocal_PurchaseID;
        private int _Protocal_SupplierID;
        private int _Protocal_Status;
        private string _Protocal_Template;
        private DateTime _Protocal_Addtime;
        private string _Protocal_Site;

        public int Protocal_ID
        {
            get { return _Protocal_ID; }
            set { _Protocal_ID = value; }
        }

        public string Protocal_Code
        {
            get { return _Protocal_Code; }
            set { _Protocal_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Protocal_PurchaseID
        {
            get { return _Protocal_PurchaseID; }
            set { _Protocal_PurchaseID = value; }
        }

        public int Protocal_SupplierID
        {
            get { return _Protocal_SupplierID; }
            set { _Protocal_SupplierID = value; }
        }

        public int Protocal_Status
        {
            get { return _Protocal_Status; }
            set { _Protocal_Status = value; }
        }

        public string Protocal_Template
        {
            get { return _Protocal_Template; }
            set { _Protocal_Template = value; }
        }

        public DateTime Protocal_Addtime
        {
            get { return _Protocal_Addtime; }
            set { _Protocal_Addtime = value; }
        }

        public string Protocal_Site
        {
            get { return _Protocal_Site; }
            set { _Protocal_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
