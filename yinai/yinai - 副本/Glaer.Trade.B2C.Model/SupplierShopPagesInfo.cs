using System;


namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopPagesInfo
    {
        private int _Shop_Pages_ID;
        private string _Shop_Pages_Title;
        private int _Shop_Pages_SupplierID;
        private string _Shop_Pages_Sign;
        private string _Shop_Pages_Content;
        private int _Shop_Pages_Ischeck;
        private int _Shop_Pages_IsActive;
        private int _Shop_Pages_Sort;
        private DateTime _Shop_Pages_Addtime;
        private string _Shop_Pages_Site;

        public int Shop_Pages_ID
        {
            get { return _Shop_Pages_ID; }
            set { _Shop_Pages_ID = value; }
        }

        public string Shop_Pages_Title
        {
            get { return _Shop_Pages_Title; }
            set { _Shop_Pages_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Pages_SupplierID
        {
            get { return _Shop_Pages_SupplierID; }
            set { _Shop_Pages_SupplierID = value; }
        }

        public string Shop_Pages_Sign
        {
            get { return _Shop_Pages_Sign; }
            set { _Shop_Pages_Sign = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Shop_Pages_Content
        {
            get { return _Shop_Pages_Content; }
            set { _Shop_Pages_Content = value; }
        }

        public int Shop_Pages_Ischeck
        {
            get { return _Shop_Pages_Ischeck; }
            set { _Shop_Pages_Ischeck = value; }
        }

        public int Shop_Pages_IsActive
        {
            get { return _Shop_Pages_IsActive; }
            set { _Shop_Pages_IsActive = value; }
        }

        public int Shop_Pages_Sort
        {
            get { return _Shop_Pages_Sort; }
            set { _Shop_Pages_Sort = value; }
        }

        public DateTime Shop_Pages_Addtime
        {
            get { return _Shop_Pages_Addtime; }
            set { _Shop_Pages_Addtime = value; }
        }

        public string Shop_Pages_Site
        {
            get { return _Shop_Pages_Site; }
            set { _Shop_Pages_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
