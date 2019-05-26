using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractTemplateInfo
    {
        private int _Contract_Template_ID;
        private string _Contract_Template_Name;
        private string _Contract_Template_Code;
        private string _Contract_Template_Content;
        private DateTime _Contract_Template_Addtime;
        private string _Contract_Template_Site;

        public int Contract_Template_ID
        {
            get { return _Contract_Template_ID; }
            set { _Contract_Template_ID = value; }
        }

        public string Contract_Template_Name
        {
            get { return _Contract_Template_Name; }
            set { _Contract_Template_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Template_Code
        {
            get { return _Contract_Template_Code; }
            set { _Contract_Template_Code = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Contract_Template_Content
        {
            get { return _Contract_Template_Content; }
            set { _Contract_Template_Content = value; }
        }

        public DateTime Contract_Template_Addtime
        {
            get { return _Contract_Template_Addtime; }
            set { _Contract_Template_Addtime = value; }
        }

        public string Contract_Template_Site
        {
            get { return _Contract_Template_Site; }
            set { _Contract_Template_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
