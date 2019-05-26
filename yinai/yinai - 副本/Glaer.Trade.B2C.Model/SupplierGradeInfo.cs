using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierGradeInfo
    {
        private int _Supplier_Grade_ID;
        private string _Supplier_Grade_Name;
        private int _Supplier_Grade_Percent;
        private int _Supplier_Grade_Default;
        private int _Supplier_Grade_RequiredCoin;
        private string _Supplier_Grade_Site;

        public int Supplier_Grade_ID
        {
            get { return _Supplier_Grade_ID; }
            set { _Supplier_Grade_ID = value; }
        }

        public string Supplier_Grade_Name
        {
            get { return _Supplier_Grade_Name; }
            set { _Supplier_Grade_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_Grade_Percent
        {
            get { return _Supplier_Grade_Percent; }
            set { _Supplier_Grade_Percent = value; }
        }

        public int Supplier_Grade_Default
        {
            get { return _Supplier_Grade_Default; }
            set { _Supplier_Grade_Default = value; }
        }

        public int Supplier_Grade_RequiredCoin
        {
            get { return _Supplier_Grade_RequiredCoin; }
            set { _Supplier_Grade_RequiredCoin = value; }
        }

        public string Supplier_Grade_Site
        {
            get { return _Supplier_Grade_Site; }
            set { _Supplier_Grade_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
