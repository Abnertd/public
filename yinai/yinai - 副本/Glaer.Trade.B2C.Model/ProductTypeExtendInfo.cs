using System;

namespace Glaer.Trade.B2C.Model
{
    public class ProductTypeExtendInfo
    {
        private int _ProductType_Extend_ID;
        private int _ProductType_Extend_ProductTypeID;
        private string _ProductType_Extend_Name;
        private string _ProductType_Extend_Display;
        private int _ProductType_Extend_IsSearch;
        private int _ProductType_Extend_Options;
        private string _ProductType_Extend_Default;
        private int _ProductType_Extend_IsActive;
        private int _ProductType_Extend_Gather;
        private int _ProductType_Extend_DisplayForm;
        private int _ProductType_Extend_Sort;
        private string _ProductType_Extend_Site;

        public int ProductType_Extend_ID
        {
            get { return _ProductType_Extend_ID; }
            set { _ProductType_Extend_ID = value; }
        }

        public int ProductType_Extend_ProductTypeID
        {
            get { return _ProductType_Extend_ProductTypeID; }
            set { _ProductType_Extend_ProductTypeID = value; }
        }

        public string ProductType_Extend_Name
        {
            get { return _ProductType_Extend_Name; }
            set { _ProductType_Extend_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string ProductType_Extend_Display
        {
            get { return _ProductType_Extend_Display; }
            set { _ProductType_Extend_Display = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int ProductType_Extend_IsSearch
        {
            get { return _ProductType_Extend_IsSearch; }
            set { _ProductType_Extend_IsSearch = value; }
        }

        public int ProductType_Extend_Options
        {
            get { return _ProductType_Extend_Options; }
            set { _ProductType_Extend_Options = value; }
        }

        public string ProductType_Extend_Default
        {
            get { return _ProductType_Extend_Default; }
            set { _ProductType_Extend_Default = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int ProductType_Extend_IsActive
        {
            get { return _ProductType_Extend_IsActive; }
            set { _ProductType_Extend_IsActive = value; }
        }

        public int ProductType_Extend_Gather
        {
            get { return _ProductType_Extend_Gather; }
            set { _ProductType_Extend_Gather = value; }
        }

        public int ProductType_Extend_DisplayForm
        {
            get { return _ProductType_Extend_DisplayForm; }
            set { _ProductType_Extend_DisplayForm = value; }
        }

        public int ProductType_Extend_Sort
        {
            get { return _ProductType_Extend_Sort; }
            set { _ProductType_Extend_Sort = value; }
        }

        public string ProductType_Extend_Site
        {
            get { return _ProductType_Extend_Site; }
            set { _ProductType_Extend_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
