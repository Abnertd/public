using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierShopBannerInfo
    {
        private int _Shop_Banner_ID;
        private int _Shop_Banner_Type;
        private string _Shop_Banner_Name;
        private string _Shop_Banner_Url;
        private int _Shop_Banner_IsActive;
        private string _Shop_Banner_Site;

        public int Shop_Banner_ID
        {
            get { return _Shop_Banner_ID; }
            set { _Shop_Banner_ID = value; }
        }

        public int Shop_Banner_Type
        {
            get { return _Shop_Banner_Type; }
            set { _Shop_Banner_Type = value; }
        }

        public string Shop_Banner_Name
        {
            get { return _Shop_Banner_Name; }
            set { _Shop_Banner_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Shop_Banner_Url
        {
            get { return _Shop_Banner_Url; }
            set { _Shop_Banner_Url = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Shop_Banner_IsActive
        {
            get { return _Shop_Banner_IsActive; }
            set { _Shop_Banner_IsActive = value; }
        }

        public string Shop_Banner_Site
        {
            get { return _Shop_Banner_Site; }
            set { _Shop_Banner_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
