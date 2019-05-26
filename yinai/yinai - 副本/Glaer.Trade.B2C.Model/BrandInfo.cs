using System;

namespace Glaer.Trade.B2C.Model
{
    public class BrandInfo
    {
        private int _Brand_ID;
        private string _Brand_Name;
        private string _Brand_Img;
        private string _Brand_URL;
        private string _Brand_Description;
        private int _Brand_Sort;
        private int _Brand_IsRecommend;
        private int _Brand_IsActive;
        private string _Brand_Site;

        public int Brand_ID
        {
            get { return _Brand_ID; }
            set { _Brand_ID = value; }
        }

        public string Brand_Name
        {
            get { return _Brand_Name; }
            set { _Brand_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Brand_Img
        {
            get { return _Brand_Img; }
            set { _Brand_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Brand_URL
        {
            get { return _Brand_URL; }
            set { _Brand_URL = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Brand_Description
        {
            get { return _Brand_Description; }
            set { _Brand_Description = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public int Brand_Sort
        {
            get { return _Brand_Sort; }
            set { _Brand_Sort = value; }
        }

        public int Brand_IsRecommend
        {
            get { return _Brand_IsRecommend; }
            set { _Brand_IsRecommend = value; }
        }

        public int Brand_IsActive
        {
            get { return _Brand_IsActive; }
            set { _Brand_IsActive = value; }
        }

        public string Brand_Site
        {
            get { return _Brand_Site; }
            set { _Brand_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    
}
