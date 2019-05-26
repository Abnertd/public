using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACPrivilegeInfo
    {
        private string _RBAC_Privilege_ID;
        private int _RBAC_Privilege_ResourceID;
        private string _RBAC_Privilege_Name;
        private int _RBAC_Privilege_IsActive;
        private DateTime _RBAC_Privilege_Addtime;

        public string RBAC_Privilege_ID
        {
            get { return _RBAC_Privilege_ID; }
            set { _RBAC_Privilege_ID = value; }
        }

        public int RBAC_Privilege_ResourceID
        {
            get { return _RBAC_Privilege_ResourceID; }
            set { _RBAC_Privilege_ResourceID = value; }
        }

        public string RBAC_Privilege_Name
        {
            get { return _RBAC_Privilege_Name; }
            set { _RBAC_Privilege_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int RBAC_Privilege_IsActive
        {
            get { return _RBAC_Privilege_IsActive; }
            set { _RBAC_Privilege_IsActive = value; }
        }

        public DateTime RBAC_Privilege_Addtime
        {
            get { return _RBAC_Privilege_Addtime; }
            set { _RBAC_Privilege_Addtime = value; }
        }

    }
}
