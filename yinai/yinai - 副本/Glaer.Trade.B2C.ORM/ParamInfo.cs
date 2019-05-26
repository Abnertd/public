using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.ORM
{
    public class ParamInfo
    {
        private string _strJoint = "AND";
        private string _strField = "";
        private string _strType = "";
        private string _strTally = "";
        private string _strValue = "";

        public ParamInfo()
        {

        }

        public ParamInfo(string Joint, string Type, string Field, string Tally, string Value)
        {
            _strJoint = Joint;
            _strType = Type;
            _strField = Field;
            _strTally = Tally;
            _strValue = Value;
        }

        public string strJoint
        {
            get { return _strJoint; }
            set { _strJoint = value; }
        }

        public string strField
        {
            get { return _strField; }
            set { _strField = value; }
        }

        public string strType
        {
            get { return _strType; }
            set { _strType = value; }
        }

        public string strTally
        {
            get { return _strTally; }
            set { _strTally = value; }
        }

        public string strValue
        {
            get { return _strValue; }
            set { _strValue = value; }
        }
    }
}
