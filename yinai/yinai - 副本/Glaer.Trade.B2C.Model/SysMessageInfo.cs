using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SysMessageInfo
    {
        private int _Message_ID;
        private int _Message_Type;
        private int _Message_UserType;
        private int _Message_ReceiveID;
        private int _Message_SendID;
        private string _Message_Content;
        private DateTime _Message_Addtime;
        private int _Message_Status;
        private string _Message_Site;
        private int _Message_IsHidden;
        public int Message_ID
        {
            get { return _Message_ID; }
            set { _Message_ID = value; }
        }

        public int Message_Type
        {
            get { return _Message_Type; }
            set { _Message_Type = value; }
        }

        public int Message_UserType
        {
            get { return _Message_UserType; }
            set { _Message_UserType = value; }
        }

        public int Message_ReceiveID
        {
            get { return _Message_ReceiveID; }
            set { _Message_ReceiveID = value; }
        }

        public int Message_SendID
        {
            get { return _Message_SendID; }
            set { _Message_SendID = value; }
        }

        public string Message_Content
        {
            get { return _Message_Content; }
            set { _Message_Content = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public DateTime Message_Addtime
        {
            get { return _Message_Addtime; }
            set { _Message_Addtime = value; }
        }

        public int Message_Status
        {
            get { return _Message_Status; }
            set { _Message_Status = value; }
        }

        public string Message_Site
        {
            get { return _Message_Site; }
            set { _Message_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }
        public int Message_IsHidden
        {
            get { return _Message_IsHidden; }
            set { _Message_IsHidden = value; }
        }
    }
}
