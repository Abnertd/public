using System;

namespace Glaer.Trade.B2C.Model
{
    public class AboutInfo
    {
        private int _About_ID;
        private int _About_IsActive;
        private string _About_Title;
        private string _About_Sign;
        private string _About_Content;
        private int _About_Sort;
        private string _About_Site;

        public int About_ID
        {
            get { return _About_ID; }
            set { _About_ID = value; }
        }

        public int About_IsActive
        {
            get { return _About_IsActive; }
            set { _About_IsActive = value; }
        }

        public string About_Title
        {
            get { return _About_Title; }
            set { _About_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string About_Sign
        {
            get { return _About_Sign; }
            set { _About_Sign = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string About_Content
        {
            get { return _About_Content; }
            set { _About_Content = value; }
        }

        public int About_Sort
        {
            get { return _About_Sort; }
            set { _About_Sort = value; }
        }

        public string About_Site
        {
            get { return _About_Site; }
            set { _About_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
