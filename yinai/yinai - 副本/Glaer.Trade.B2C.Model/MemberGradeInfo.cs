using System;

namespace Glaer.Trade.B2C.Model
{
    public class MemberGradeInfo
    {
        private int _Member_Grade_ID;
        private string _Member_Grade_Name;
        private int _Member_Grade_Percent;
        private int _Member_Grade_Default;
        private int _Member_Grade_RequiredCoin;
        private double _Member_Grade_CoinRate;
        private DateTime _Member_Grade_Addtime;
        private string _Member_Grade_Site;

        public int Member_Grade_ID
        {
            get { return _Member_Grade_ID; }
            set { _Member_Grade_ID = value; }
        }

        public string Member_Grade_Name
        {
            get { return _Member_Grade_Name; }
            set { _Member_Grade_Name = value.Length > 30 ? value.Substring(0, 30) : value.ToString(); }
        }

        public int Member_Grade_Percent
        {
            get { return _Member_Grade_Percent; }
            set { _Member_Grade_Percent = value; }
        }

        public int Member_Grade_Default
        {
            get { return _Member_Grade_Default; }
            set { _Member_Grade_Default = value; }
        }

        public int Member_Grade_RequiredCoin
        {
            get { return _Member_Grade_RequiredCoin; }
            set { _Member_Grade_RequiredCoin = value; }
        }

        public double Member_Grade_CoinRate
        {
            get { return _Member_Grade_CoinRate; }
            set { _Member_Grade_CoinRate = value; }
        }

        public DateTime Member_Grade_Addtime
        {
            get { return _Member_Grade_Addtime; }
            set { _Member_Grade_Addtime = value; }
        }

        public string Member_Grade_Site
        {
            get { return _Member_Grade_Site; }
            set { _Member_Grade_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
