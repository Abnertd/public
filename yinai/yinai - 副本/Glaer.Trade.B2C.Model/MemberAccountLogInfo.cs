using System;

namespace Glaer.Trade.B2C.Model
{
    public class MemberAccountLogInfo
    {
        private int _Account_Log_ID;
        private int _Account_Log_MemberID;
        
        private double _Account_Log_Amount;
        private double _Account_Log_Remain;
        private string _Account_Log_Note;
        private DateTime _Account_Log_Addtime;
        private string _Account_Log_Site;

        public int Account_Log_ID
        {
            get { return _Account_Log_ID; }
            set { _Account_Log_ID = value; }
        }

        public int Account_Log_MemberID
        {
            get { return _Account_Log_MemberID; }
            set { _Account_Log_MemberID = value; }
        }
       

        public double Account_Log_Amount
        {
            get { return _Account_Log_Amount; }
            set { _Account_Log_Amount = value; }
        }

        public double Account_Log_Remain
        {
            get { return _Account_Log_Remain; }
            set { _Account_Log_Remain = value; }
        }

        public string Account_Log_Note
        {
            get { return _Account_Log_Note; }
            set { _Account_Log_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Account_Log_Addtime
        {
            get { return _Account_Log_Addtime; }
            set { _Account_Log_Addtime = value; }
        }

        public string Account_Log_Site
        {
            get { return _Account_Log_Site; }
            set { _Account_Log_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
