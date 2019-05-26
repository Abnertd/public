using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.RBAC
{
    public interface IRBAC
    {
        bool CheckPrivilege(RBACUserInfo User, string PrivilegeCode);
    }
}
