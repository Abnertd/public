using System;

namespace Glaer.Trade.B2C.Model
{
    public class PayTypeInfo
    {
        private int _Pay_Type_ID;
        private string _Pay_Type_Name;
        private int _Pay_Type_Sort;
        private int _Pay_Type_IsActive;
        private string _Pay_Type_Site;

        public int Pay_Type_ID
        {
            get { return _Pay_Type_ID; }
            set { _Pay_Type_ID = value; }
        }

        public string Pay_Type_Name
        {
            get { return _Pay_Type_Name; }
            set { _Pay_Type_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Pay_Type_Sort
        {
            get { return _Pay_Type_Sort; }
            set { _Pay_Type_Sort = value; }
        }

        public int Pay_Type_IsActive
        {
            get { return _Pay_Type_IsActive; }
            set { _Pay_Type_IsActive = value; }
        }

        public string Pay_Type_Site
        {
            get { return _Pay_Type_Site; }
            set { _Pay_Type_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
