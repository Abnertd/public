using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACResourceInfo
    {
        private int _RBAC_Resource_ID;
        private int _RBAC_Resource_GroupID;
        private string _RBAC_Resource_Name;
        private string _RBAC_Resource_Site;
        private IList<RBACPrivilegeInfo> _RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();

        public int RBAC_Resource_ID
        {
            get { return _RBAC_Resource_ID; }
            set { _RBAC_Resource_ID = value; }
        }

        public int RBAC_Resource_GroupID
        {
            get { return _RBAC_Resource_GroupID; }
            set { _RBAC_Resource_GroupID = value; }
        }

        public string RBAC_Resource_Name
        {
            get { return _RBAC_Resource_Name; }
            set { _RBAC_Resource_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string RBAC_Resource_Site
        {
            get { return _RBAC_Resource_Site; }
            set { _RBAC_Resource_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<RBACPrivilegeInfo> RBACPrivilegeInfos
        {
            get { return _RBACPrivilegeInfos; }
            set { _RBACPrivilegeInfos = value; }
        }

    }
}
