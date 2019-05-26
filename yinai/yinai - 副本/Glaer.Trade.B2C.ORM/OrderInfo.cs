using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.ORM
{
    public class OrderInfo
    {
        string _strField = "";
        string _strSort = "";

        public OrderInfo()
        {
        }

        public OrderInfo(string strField, string strSort)
        {
            _strField = strField;
            _strSort = strSort;
        }

        public string strField
        {
            get { return _strField; }
            set { _strField = value; }
        }

        public string strSort
        {
            get { return _strSort; }
            set { _strSort = value; }
        }
    }
}
