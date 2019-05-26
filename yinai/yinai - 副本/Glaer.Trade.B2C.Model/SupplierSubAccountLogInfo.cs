using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SupplierSubAccountLogInfo
    {
        private int _Log_ID;
        private int _Log_Supplier_ID;
        private int _Log_SubAccount_ID;
        private string _Log_SubAccount_Action;
        private string _Log_SubAccount_Note;
        private DateTime _Log_Addtime;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public int Log_Supplier_ID
        {
            get { return _Log_Supplier_ID; }
            set { _Log_Supplier_ID = value; }
        }

        public int Log_SubAccount_ID
        {
            get { return _Log_SubAccount_ID; }
            set { _Log_SubAccount_ID = value; }
        }

        public string Log_SubAccount_Action
        {
            get { return _Log_SubAccount_Action; }
            set { _Log_SubAccount_Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Log_SubAccount_Note
        {
            get { return _Log_SubAccount_Note; }
            set { _Log_SubAccount_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }

    }
}
