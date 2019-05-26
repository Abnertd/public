using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierOnlineInfo
    {
        private int _Supplier_Online_ID;
        private int _Supplier_Online_SupplierID;
        private string _Supplier_Online_Type;
        private string _Supplier_Online_Name;
        private string _Supplier_Online_Code;
        private int _Supplier_Online_Sort;
        private int _Supplier_Online_IsActive;
        private DateTime _Supplier_Online_Addtime;
        private string _Supplier_Online_Site;

        public int Supplier_Online_ID
        {
            get { return _Supplier_Online_ID; }
            set { _Supplier_Online_ID = value; }
        }

        public int Supplier_Online_SupplierID
        {
            get { return _Supplier_Online_SupplierID; }
            set { _Supplier_Online_SupplierID = value; }
        }

        public string Supplier_Online_Type
        {
            get { return _Supplier_Online_Type; }
            set { _Supplier_Online_Type = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Online_Name
        {
            get { return _Supplier_Online_Name; }
            set { _Supplier_Online_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Online_Code
        {
            get { return _Supplier_Online_Code; }
            set { _Supplier_Online_Code = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public int Supplier_Online_Sort
        {
            get { return _Supplier_Online_Sort; }
            set { _Supplier_Online_Sort = value; }
        }

        public int Supplier_Online_IsActive
        {
            get { return _Supplier_Online_IsActive; }
            set { _Supplier_Online_IsActive = value; }
        }

        public DateTime Supplier_Online_Addtime
        {
            get { return _Supplier_Online_Addtime; }
            set { _Supplier_Online_Addtime = value; }
        }

        public string Supplier_Online_Site
        {
            get { return _Supplier_Online_Site; }
            set { _Supplier_Online_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
