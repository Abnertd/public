using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    //class BidUpRequireQuickInfo
    //{
    //}
    public class BidUpRequireQuickInfo
    {
        private int _Bid_Up_ID;
        private string _Bid_Up_ContractMan;
        private string _Bid_Up_ContractMobile;
        private string _Bid_Up_ContractContent;
        private int _Bid_Up_Type;
        private string _Bid_Up_Note;
        private string _Bid_Up_Note1;
        private DateTime _Bid_Up_AddTime;

        public int Bid_Up_ID
        {
            get { return _Bid_Up_ID; }
            set { _Bid_Up_ID = value; }
        }

        public string Bid_Up_ContractMan
        {
            get { return _Bid_Up_ContractMan; }
            set { _Bid_Up_ContractMan = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }

        public string Bid_Up_ContractMobile
        {
            get { return _Bid_Up_ContractMobile; }
            set { _Bid_Up_ContractMobile = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }

        public string Bid_Up_ContractContent
        {
            get { return _Bid_Up_ContractContent; }
            set { _Bid_Up_ContractContent = value; }
        }

        public int Bid_Up_Type
        {
            get { return _Bid_Up_Type; }
            set { _Bid_Up_Type = value; }
        }

        public string Bid_Up_Note
        {
            get { return _Bid_Up_Note; }
            set { _Bid_Up_Note = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Bid_Up_Note1
        {
            get { return _Bid_Up_Note1; }
            set { _Bid_Up_Note1 = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }       
        public DateTime Bid_Up_AddTime
        {
            get { return _Bid_Up_AddTime; }
            set { _Bid_Up_AddTime = value; }
        }

    }
}
