using System;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierCloseShopApplyInfo
    {
        private int _CloseShop_Apply_ID;
        private int _CloseShop_Apply_SupplierID;
        private string _CloseShop_Apply_Note;
        private int _CloseShop_Apply_Status;
        private string _CloseShop_Apply_AdminNote;
        private DateTime _CloseShop_Apply_Addtime;
        private DateTime _CloseShop_Apply_AdminTime;
        private string _CloseShop_Apply_Site;

        public int CloseShop_Apply_ID
        {
            get { return _CloseShop_Apply_ID; }
            set { _CloseShop_Apply_ID = value; }
        }

        public int CloseShop_Apply_SupplierID
        {
            get { return _CloseShop_Apply_SupplierID; }
            set { _CloseShop_Apply_SupplierID = value; }
        }

        public string CloseShop_Apply_Note
        {
            get { return _CloseShop_Apply_Note; }
            set { _CloseShop_Apply_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int CloseShop_Apply_Status
        {
            get { return _CloseShop_Apply_Status; }
            set { _CloseShop_Apply_Status = value; }
        }

        public string CloseShop_Apply_AdminNote
        {
            get { return _CloseShop_Apply_AdminNote; }
            set { _CloseShop_Apply_AdminNote = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime CloseShop_Apply_Addtime
        {
            get { return _CloseShop_Apply_Addtime; }
            set { _CloseShop_Apply_Addtime = value; }
        }

        public DateTime CloseShop_Apply_AdminTime
        {
            get { return _CloseShop_Apply_AdminTime; }
            set { _CloseShop_Apply_AdminTime = value; }
        }

        public string CloseShop_Apply_Site
        {
            get { return _CloseShop_Apply_Site; }
            set { _CloseShop_Apply_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
