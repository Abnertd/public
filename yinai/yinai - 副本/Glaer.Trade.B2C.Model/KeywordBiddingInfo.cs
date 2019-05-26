using System;


namespace Glaer.Trade.B2C.Model
{
    public class KeywordBiddingInfo
    {
        private int _KeywordBidding_ID;
        private int _KeywordBidding_SupplierID;
        private int _KeywordBidding_ProductID;
        private int _KeywordBidding_KeywordID;
        private double _KeywordBidding_Price;
        private DateTime _KeywordBidding_StartDate;
        private DateTime _KeywordBidding_EndDate;
        private int _KeywordBidding_ShowTimes;
        private int _KeywordBidding_Hits;
        private int _KeywordBidding_Audit;
        private string _KeywordBidding_Site;

        public int KeywordBidding_ID
        {
            get { return _KeywordBidding_ID; }
            set { _KeywordBidding_ID = value; }
        }

        public int KeywordBidding_SupplierID
        {
            get { return _KeywordBidding_SupplierID; }
            set { _KeywordBidding_SupplierID = value; }
        }

        public int KeywordBidding_ProductID
        {
            get { return _KeywordBidding_ProductID; }
            set { _KeywordBidding_ProductID = value; }
        }

        public int KeywordBidding_KeywordID
        {
            get { return _KeywordBidding_KeywordID; }
            set { _KeywordBidding_KeywordID = value; }
        }

        public double KeywordBidding_Price
        {
            get { return _KeywordBidding_Price; }
            set { _KeywordBidding_Price = value; }
        }

        public DateTime KeywordBidding_StartDate
        {
            get { return _KeywordBidding_StartDate; }
            set { _KeywordBidding_StartDate = value; }
        }

        public DateTime KeywordBidding_EndDate
        {
            get { return _KeywordBidding_EndDate; }
            set { _KeywordBidding_EndDate = value; }
        }

        public int KeywordBidding_ShowTimes
        {
            get { return _KeywordBidding_ShowTimes; }
            set { _KeywordBidding_ShowTimes = value; }
        }

        public int KeywordBidding_Hits
        {
            get { return _KeywordBidding_Hits; }
            set { _KeywordBidding_Hits = value; }
        }

        public int KeywordBidding_Audit
        {
            get { return _KeywordBidding_Audit; }
            set { _KeywordBidding_Audit = value; }
        }

        public string KeywordBidding_Site
        {
            get { return _KeywordBidding_Site; }
            set { _KeywordBidding_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class KeywordBiddingKeywordInfo
    {
        private int _Keyword_ID;
        private string _Keyword_Name;
        private double _Keyword_MinPrice;

        public int Keyword_ID
        {
            get { return _Keyword_ID; }
            set { _Keyword_ID = value; }
        }

        public string Keyword_Name
        {
            get { return _Keyword_Name; }
            set { _Keyword_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public double Keyword_MinPrice
        {
            get { return _Keyword_MinPrice; }
            set { _Keyword_MinPrice = value; }
        }

    }

    public class KeywordsRankingInfo
    {
        private int _ID;
        private int _Type;
        private string _Keyword;
        private DateTime _addtime;
        private string _Site;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Keyword
        {
            get { return _Keyword; }
            set { _Keyword = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        public string Site
        {
            get { return _Site; }
            set { _Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
    }
}
