using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACRoleInfo
    {
        private int _RBAC_Role_ID;
        private string _RBAC_Role_Name;
        private string _RBAC_Role_Description;
        private int _RBAC_Role_IsSystem;
        private string _RBAC_Role_Site;
        private IList<RBACPrivilegeInfo> _RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();

        public int RBAC_Role_ID
        {
            get { return _RBAC_Role_ID; }
            set { _RBAC_Role_ID = value; }
        }

        public string RBAC_Role_Name
        {
            get { return _RBAC_Role_Name; }
            set { _RBAC_Role_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string RBAC_Role_Description
        {
            get { return _RBAC_Role_Description; }
            set { _RBAC_Role_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int RBAC_Role_IsSystem
        {
            get { return _RBAC_Role_IsSystem; }
            set { _RBAC_Role_IsSystem = value; }
        }

        public string RBAC_Role_Site
        {
            get { return _RBAC_Role_Site; }
            set { _RBAC_Role_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<RBACPrivilegeInfo> RBACPrivilegeInfos
        {
            get { return _RBACPrivilegeInfos; }
            set { _RBACPrivilegeInfos = value; }
        }

    }
}
