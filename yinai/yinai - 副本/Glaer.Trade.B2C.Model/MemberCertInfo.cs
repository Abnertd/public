using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberCertTypeInfo
    {
        private int _Member_Cert_Type_ID;
        private string _Member_Cert_Type_Name;
        private int _Member_Cert_Type_Sort;
        private int _Member_Cert_Type_IsActive;
        private string _Member_Cert_Type_Site;

        public int Member_Cert_Type_ID
        {
            get { return _Member_Cert_Type_ID; }
            set { _Member_Cert_Type_ID = value; }
        }

        public string Member_Cert_Type_Name
        {
            get { return _Member_Cert_Type_Name; }
            set { _Member_Cert_Type_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Member_Cert_Type_Sort
        {
            get { return _Member_Cert_Type_Sort; }
            set { _Member_Cert_Type_Sort = value; }
        }

        public int Member_Cert_Type_IsActive
        {
            get { return _Member_Cert_Type_IsActive; }
            set { _Member_Cert_Type_IsActive = value; }
        }

        public string Member_Cert_Type_Site
        {
            get { return _Member_Cert_Type_Site; }
            set { _Member_Cert_Type_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }

    public class MemberCertInfo
    {
        private int _Member_Cert_ID;
        private int _Member_Cert_Type;
        private string _Member_Cert_Name;
        private string _Member_Cert_Note;
        private int _Member_Cert_Sort;
        private DateTime _Member_Cert_Addtime;
        private string _Member_Cert_Site;

        public int Member_Cert_ID
        {
            get { return _Member_Cert_ID; }
            set { _Member_Cert_ID = value; }
        }

        public int Member_Cert_Type
        {
            get { return _Member_Cert_Type; }
            set { _Member_Cert_Type = value; }
        }

        public string Member_Cert_Name
        {
            get { return _Member_Cert_Name; }
            set { _Member_Cert_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Cert_Note
        {
            get { return _Member_Cert_Note; }
            set { _Member_Cert_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Member_Cert_Sort
        {
            get { return _Member_Cert_Sort; }
            set { _Member_Cert_Sort = value; }
        }

        public DateTime Member_Cert_Addtime
        {
            get { return _Member_Cert_Addtime; }
            set { _Member_Cert_Addtime = value; }
        }

        public string Member_Cert_Site
        {
            get { return _Member_Cert_Site; }
            set { _Member_Cert_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }
}
