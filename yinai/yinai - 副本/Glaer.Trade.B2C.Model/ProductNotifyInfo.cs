using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class ProductNotifyInfo
    {
        private int _Product_Notify_ID;
        private int _Product_Notify_MemberID;
        private string _Product_Notify_Email;
        private int _Product_Notify_ProductID;
        private int _Product_Notify_IsNotify;
        private DateTime _Product_Notify_Addtime;
        private string _Product_Notify_Site;

        public int Product_Notify_ID
        {
            get { return _Product_Notify_ID; }
            set { _Product_Notify_ID = value; }
        }

        public int Product_Notify_MemberID
        {
            get { return _Product_Notify_MemberID; }
            set { _Product_Notify_MemberID = value; }
        }

        public string Product_Notify_Email
        {
            get { return _Product_Notify_Email; }
            set { _Product_Notify_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Product_Notify_ProductID
        {
            get { return _Product_Notify_ProductID; }
            set { _Product_Notify_ProductID = value; }
        }

        public int Product_Notify_IsNotify
        {
            get { return _Product_Notify_IsNotify; }
            set { _Product_Notify_IsNotify = value; }
        }

        public DateTime Product_Notify_Addtime
        {
            get { return _Product_Notify_Addtime; }
            set { _Product_Notify_Addtime = value; }
        }

        public string Product_Notify_Site
        {
            get { return _Product_Notify_Site; }
            set { _Product_Notify_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
