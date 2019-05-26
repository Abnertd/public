using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class MemberJsonInfo
    {
        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _member_id;
        public string Member_id
        {
            get { return _member_id; }
            set { _member_id = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }
    }
}
