using System;

namespace Glaer.Trade.B2C.Model
{
    public class ContractLogInfo
    {
        private int _Log_ID;
        private string _Log_Operator;
        private int _Log_Contact_ID;
        private int _Log_Result;
        private DateTime _Log_Addtime;
        private string _Log_Action;
        private string _Log_Remark;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public string Log_Operator
        {
            get { return _Log_Operator; }
            set { _Log_Operator = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Log_Contact_ID
        {
            get { return _Log_Contact_ID; }
            set { _Log_Contact_ID = value; }
        }

        public int Log_Result
        {
            get { return _Log_Result; }
            set { _Log_Result = value; }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }

        public string Log_Action
        {
            get { return _Log_Action; }
            set { _Log_Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Log_Remark
        {
            get { return _Log_Remark; }
            set { _Log_Remark = value.Length > 2000 ? value.Substring(0, 2000) : value.ToString(); }
        }

    }
}
