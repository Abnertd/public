using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierTagInfo
    {
        private int _Supplier_Tag_ID;
        private string _Supplier_Tag_Name;
        private string _Supplier_Tag_Img;
        private string _Supplier_Tag_Content;
        private string _Supplier_Tag_Site;

        public int Supplier_Tag_ID
        {
            get { return _Supplier_Tag_ID; }
            set { _Supplier_Tag_ID = value; }
        }

        public string Supplier_Tag_Name
        {
            get { return _Supplier_Tag_Name; }
            set { _Supplier_Tag_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Tag_Img
        {
            get { return _Supplier_Tag_Img; }
            set { _Supplier_Tag_Img = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Supplier_Tag_Content
        {
            get { return _Supplier_Tag_Content; }
            set { _Supplier_Tag_Content = value; }
        }

        public string Supplier_Tag_Site
        {
            get { return _Supplier_Tag_Site; }
            set { _Supplier_Tag_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class SupplierRelateTagInfo
    {
        private int _Supplier_RelateTag_ID;
        private int _Supplier_RelateTag_SupplierID;
        private int _Supplier_RelateTag_TagID;

        public int Supplier_RelateTag_ID
        {
            get { return _Supplier_RelateTag_ID; }
            set { _Supplier_RelateTag_ID = value; }
        }

        public int Supplier_RelateTag_SupplierID
        {
            get { return _Supplier_RelateTag_SupplierID; }
            set { _Supplier_RelateTag_SupplierID = value; }
        }

        public int Supplier_RelateTag_TagID
        {
            get { return _Supplier_RelateTag_TagID; }
            set { _Supplier_RelateTag_TagID = value; }
        }

    }
}
