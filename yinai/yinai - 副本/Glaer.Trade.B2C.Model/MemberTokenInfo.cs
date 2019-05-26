using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberTokenInfo
    {
        private string _Token;
        private int _Type;
        private int _MemberID;
        private string _IP;
        private DateTime _CreateTime;
        private DateTime _UpdateTime;
        private DateTime _ExpiredTime;

        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        public string IP
        {
            get { return _IP; }
            set { _IP = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }

        public DateTime ExpiredTime
        {
            get { return _ExpiredTime; }
            set { _ExpiredTime = value; }
        }
    }
}
