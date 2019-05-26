using System;

namespace Glaer.Trade.B2C.Model
{
    public class ProductTagInfo
    {
        private int _Product_Tag_ID;
        private string _Product_Tag_Name;
        private int _Product_Tag_IsSupplier;
        private int _Product_Tag_SupplierID;
        private int _Product_Tag_IsActive;
        private int _Product_Tag_Sort;
        private string _Product_Tag_Site;

        public int Product_Tag_ID
        {
            get { return _Product_Tag_ID; }
            set { _Product_Tag_ID = value; }
        }

        public string Product_Tag_Name
        {
            get { return _Product_Tag_Name; }
            set { _Product_Tag_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Product_Tag_IsSupplier
        {
            get { return _Product_Tag_IsSupplier; }
            set { _Product_Tag_IsSupplier = value; }
        }

        public int Product_Tag_SupplierID
        {
            get { return _Product_Tag_SupplierID; }
            set { _Product_Tag_SupplierID = value; }
        }

        public int Product_Tag_IsActive
        {
            get { return _Product_Tag_IsActive; }
            set { _Product_Tag_IsActive = value; }
        }

        public int Product_Tag_Sort
        {
            get { return _Product_Tag_Sort; }
            set { _Product_Tag_Sort = value; }
        }

        public string Product_Tag_Site
        {
            get { return _Product_Tag_Site; }
            set { _Product_Tag_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
