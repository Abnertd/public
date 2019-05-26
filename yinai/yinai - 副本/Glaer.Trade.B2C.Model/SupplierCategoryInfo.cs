using System;


namespace Glaer.Trade.B2C.Model
{
    public class SupplierCategoryInfo
    {
        private int _Supplier_Cate_ID;
        private string _Supplier_Cate_Name;
        private int _Supplier_Cate_Parentid;
        private int _Supplier_Cate_SupplierID;
        private int _Supplier_Cate_Sort;
        private string _Supplier_Cate_Site;

        public int Supplier_Cate_ID
        {
            get { return _Supplier_Cate_ID; }
            set { _Supplier_Cate_ID = value; }
        }

        public string Supplier_Cate_Name
        {
            get { return _Supplier_Cate_Name; }
            set { _Supplier_Cate_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Supplier_Cate_Parentid
        {
            get { return _Supplier_Cate_Parentid; }
            set { _Supplier_Cate_Parentid = value; }
        }

        public int Supplier_Cate_SupplierID
        {
            get { return _Supplier_Cate_SupplierID; }
            set { _Supplier_Cate_SupplierID = value; }
        }

        public int Supplier_Cate_Sort
        {
            get { return _Supplier_Cate_Sort; }
            set { _Supplier_Cate_Sort = value; }
        }

        public string Supplier_Cate_Site
        {
            get { return _Supplier_Cate_Site; }
            set { _Supplier_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
