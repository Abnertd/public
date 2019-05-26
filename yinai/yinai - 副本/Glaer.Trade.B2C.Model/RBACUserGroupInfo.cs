using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACUserGroupInfo
    {
        private int _RBAC_UserGroup_ID;
        private string _RBAC_UserGroup_Name;
        private int _RBAC_UserGroup_ParentID;
        private string _RBAC_UserGroup_Site;

        public int RBAC_UserGroup_ID
        {
            get { return _RBAC_UserGroup_ID; }
            set { _RBAC_UserGroup_ID = value; }
        }

        public string RBAC_UserGroup_Name
        {
            get { return _RBAC_UserGroup_Name; }
            set { _RBAC_UserGroup_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int RBAC_UserGroup_ParentID
        {
            get { return _RBAC_UserGroup_ParentID; }
            set { _RBAC_UserGroup_ParentID = value; }
        }

        public string RBAC_UserGroup_Site
        {
            get { return _RBAC_UserGroup_Site; }
            set { _RBAC_UserGroup_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
