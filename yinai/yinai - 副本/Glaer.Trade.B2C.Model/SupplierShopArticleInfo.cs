using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopArticleInfo
    {
        private int _Shop_Article_ID;
        private int _Shop_Article_SupplierID;
        private string _Shop_Article_Title;
        private string _Shop_Article_Content;
        private int _Shop_Article_Hits;
        private DateTime _Shop_Article_Addtime;
        private int _Shop_Article_IsActive;
        private string _Shop_Article_Site;

        public int Shop_Article_ID
        {
            get { return _Shop_Article_ID; }
            set { _Shop_Article_ID = value; }
        }

        public int Shop_Article_SupplierID
        {
            get { return _Shop_Article_SupplierID; }
            set { _Shop_Article_SupplierID = value; }
        }

        public string Shop_Article_Title
        {
            get { return _Shop_Article_Title; }
            set { _Shop_Article_Title = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Article_Content
        {
            get { return _Shop_Article_Content; }
            set { _Shop_Article_Content = value; }
        }

        public int Shop_Article_Hits
        {
            get { return _Shop_Article_Hits; }
            set { _Shop_Article_Hits = value; }
        }

        public DateTime Shop_Article_Addtime
        {
            get { return _Shop_Article_Addtime; }
            set { _Shop_Article_Addtime = value; }
        }

        public int Shop_Article_IsActive
        {
            get { return _Shop_Article_IsActive; }
            set { _Shop_Article_IsActive = value; }
        }

        public string Shop_Article_Site
        {
            get { return _Shop_Article_Site; }
            set { _Shop_Article_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
