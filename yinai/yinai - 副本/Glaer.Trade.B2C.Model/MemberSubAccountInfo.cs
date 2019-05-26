using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberSubAccountInfo
    {
        private int _ID;
        private int _MemberID;
        private string _AccountName;
        private string _Password;
        private string _Name;
        private string _Mobile;
        private string _Email;
        private DateTime _Addtime;
        private DateTime _LastLoginTime;
        private int _IsActive;
        private string _Privilege;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Addtime
        {
            get { return _Addtime; }
            set { _Addtime = value; }
        }

        public DateTime LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; }
        }

        public int IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public string Privilege
        {
            get { return _Privilege; }
            set { _Privilege = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }
    }

    public class MemberSubAccountLogInfo
    {
        private int _ID;
        private int _MemberID;
        private int _AccountID;
        private string _Action;
        private string _Note;
        private DateTime _Addtime;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        public int AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        public string Action
        {
            get { return _Action; }
            set { _Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Note
        {
            get { return _Note; }
            set { _Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Addtime
        {
            get { return _Addtime; }
            set { _Addtime = value; }
        }
    }
}
