using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierContractTemplateInfo
    {
        private int _Contract_Template_ID;
        private string _Contract_Template_Name;
        private int _Contract_Template_SupplierID;
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

        public int Contract_Template_SupplierID
        {
            get { return _Contract_Template_SupplierID; }
            set { _Contract_Template_SupplierID = value; }
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
