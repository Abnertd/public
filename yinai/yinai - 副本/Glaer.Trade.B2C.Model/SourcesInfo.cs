using System;

namespace Glaer.Trade.B2C.Model
{
    public class SourcesInfo
    {
        private int _Sources_ID;
        private string _Sources_Name;
        private string _Sources_Code;
        private string _Sources_Site;

        public int Sources_ID
        {
            get { return _Sources_ID; }
            set { _Sources_ID = value; }
        }

        public string Sources_Name
        {
            get { return _Sources_Name; }
            set { _Sources_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Sources_Code
        {
            get { return _Sources_Code; }
            set { _Sources_Code = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Sources_Site
        {
            get { return _Sources_Site; }
            set { _Sources_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
