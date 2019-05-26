using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierSubAccountInfo
    {
        private int _Supplier_SubAccount_ID;
        private int _Supplier_SubAccount_SupplierID;
        private string _Supplier_SubAccount_Name;
        private string _Supplier_SubAccount_Password;
        private string _Supplier_SubAccount_TrueName;
        private string _Supplier_SubAccount_Department;
        private string _Supplier_SubAccount_Tel;
        private string _Supplier_SubAccount_Mobile;
        private string _Supplier_SubAccount_Email;
        private DateTime _Supplier_SubAccount_ExpireTime;
        private DateTime _Supplier_SubAccount_AddTime;
        private DateTime _Supplier_SubAccount_lastLoginTime;
        private int _Supplier_SubAccount_IsActive;
        private string _Supplier_SubAccount_Privilege;

        public int Supplier_SubAccount_ID
        {
            get { return _Supplier_SubAccount_ID; }
            set { _Supplier_SubAccount_ID = value; }
        }

        public int Supplier_SubAccount_SupplierID
        {
            get { return _Supplier_SubAccount_SupplierID; }
            set { _Supplier_SubAccount_SupplierID = value; }
        }

        public string Supplier_SubAccount_Name
        {
            get { return _Supplier_SubAccount_Name; }
            set { _Supplier_SubAccount_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_Password
        {
            get { return _Supplier_SubAccount_Password; }
            set { _Supplier_SubAccount_Password = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_TrueName
        {
            get { return _Supplier_SubAccount_TrueName; }
            set { _Supplier_SubAccount_TrueName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_Department
        {
            get { return _Supplier_SubAccount_Department; }
            set { _Supplier_SubAccount_Department = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_Tel
        {
            get { return _Supplier_SubAccount_Tel; }
            set { _Supplier_SubAccount_Tel = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_Mobile
        {
            get { return _Supplier_SubAccount_Mobile; }
            set { _Supplier_SubAccount_Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_SubAccount_Email
        {
            get { return _Supplier_SubAccount_Email; }
            set { _Supplier_SubAccount_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Supplier_SubAccount_ExpireTime
        {
            get { return _Supplier_SubAccount_ExpireTime; }
            set { _Supplier_SubAccount_ExpireTime = value; }
        }

        public DateTime Supplier_SubAccount_AddTime
        {
            get { return _Supplier_SubAccount_AddTime; }
            set { _Supplier_SubAccount_AddTime = value; }
        }

        public DateTime Supplier_SubAccount_lastLoginTime
        {
            get { return _Supplier_SubAccount_lastLoginTime; }
            set { _Supplier_SubAccount_lastLoginTime = value; }
        }

        public int Supplier_SubAccount_IsActive
        {
            get { return _Supplier_SubAccount_IsActive; }
            set { _Supplier_SubAccount_IsActive = value; }
        }

        public string Supplier_SubAccount_Privilege
        {
            get { return _Supplier_SubAccount_Privilege; }
            set { _Supplier_SubAccount_Privilege = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

    }
}
