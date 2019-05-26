using System;

namespace Glaer.Trade.B2C.Model
{
    public class OrdersLogInfo
    {
        private int _Orders_Log_ID;
        private int _Orders_Log_OrdersID;
        private DateTime _Orders_Log_Addtime;
        private string _Orders_Log_Operator;
        private string _Orders_Log_Remark;
        private string _Orders_Log_Action;
        private string _Orders_Log_Result;

        public int Orders_Log_ID {
            get { return _Orders_Log_ID; }
            set { _Orders_Log_ID = value; }
        }

        public int Orders_Log_OrdersID {
            get { return _Orders_Log_OrdersID; }
            set { _Orders_Log_OrdersID = value; }
        }

        public DateTime Orders_Log_Addtime {
            get { return _Orders_Log_Addtime; }
            set { _Orders_Log_Addtime = value; }
        }

        public string Orders_Log_Operator {
            get { return _Orders_Log_Operator; }
            set { _Orders_Log_Operator = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Log_Remark {
            get { return _Orders_Log_Remark; }
            set { _Orders_Log_Remark = value.Length > 1000 ? value.Substring(0, 1000) : value.ToString(); }
        }

        public string Orders_Log_Action {
            get { return _Orders_Log_Action; }
            set { _Orders_Log_Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Orders_Log_Result {
            get { return _Orders_Log_Result; }
            set { _Orders_Log_Result = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
