using System;
using System.Collections.Generic;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class ProductAuditReasonInfo
    {
        private int _Product_Audit_Reason_ID;
        private string _Product_Audit_Reason_Note;

        public int Product_Audit_Reason_ID
        {
            get { return _Product_Audit_Reason_ID; }
            set { _Product_Audit_Reason_ID = value; }
        }

        public string Product_Audit_Reason_Note
        {
            get { return _Product_Audit_Reason_Note; }
            set { _Product_Audit_Reason_Note = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

    }
}
