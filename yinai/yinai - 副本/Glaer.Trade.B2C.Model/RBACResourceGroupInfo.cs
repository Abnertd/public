using System;

namespace Glaer.Trade.B2C.Model
{
    public class RBACResourceGroupInfo
    {
        private int _RBAC_ResourceGroup_ID;
        private string _RBAC_ResourceGroup_Name;
        private int _RBAC_ResourceGroup_ParentID;
        private string _RBAC_ResourceGroup_Site;

        public int RBAC_ResourceGroup_ID
        {
            get { return _RBAC_ResourceGroup_ID; }
            set { _RBAC_ResourceGroup_ID = value; }
        }

        public string RBAC_ResourceGroup_Name
        {
            get { return _RBAC_ResourceGroup_Name; }
            set { _RBAC_ResourceGroup_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int RBAC_ResourceGroup_ParentID
        {
            get { return _RBAC_ResourceGroup_ParentID; }
            set { _RBAC_ResourceGroup_ParentID = value; }
        }

        public string RBAC_ResourceGroup_Site
        {
            get { return _RBAC_ResourceGroup_Site; }
            set { _RBAC_ResourceGroup_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
