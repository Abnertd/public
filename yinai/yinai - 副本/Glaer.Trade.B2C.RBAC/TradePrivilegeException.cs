using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.RBAC
{
    public class TradePrivilegeException : System.Exception
    {
        public TradePrivilegeException(string message):base(message)
        {
        
        }
    }
}
