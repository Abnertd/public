using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACUserInfo
    {
        private int _RBAC_User_ID;
        private int _RBAC_User_GroupID;
        private string _RBAC_User_Name;
        private string _RBAC_User_Password;
        private DateTime _RBAC_User_LastLogin;
        private string _RBAC_User_LastLoginIP;
        private DateTime _RBAC_User_Addtime;
        private string _RBAC_User_Site;
        private IList<RBACRoleInfo> _RBACRoleInfos = new List<RBACRoleInfo>();

        public int RBAC_User_ID
        {
            get { return _RBAC_User_ID; }
            set { _RBAC_User_ID = value; }
        }

        public int RBAC_User_GroupID
        {
            get { return _RBAC_User_GroupID; }
            set { _RBAC_User_GroupID = value; }
        }

        public string RBAC_User_Name
        {
            get { return _RBAC_User_Name; }
            set { _RBAC_User_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string RBAC_User_Password
        {
            get { return _RBAC_User_Password; }
            set { _RBAC_User_Password = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime RBAC_User_LastLogin
        {
            get { return _RBAC_User_LastLogin; }
            set { _RBAC_User_LastLogin = value; }
        }

        public string RBAC_User_LastLoginIP
        {
            get { return _RBAC_User_LastLoginIP; }
            set { _RBAC_User_LastLoginIP = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime RBAC_User_Addtime
        {
            get { return _RBAC_User_Addtime; }
            set { _RBAC_User_Addtime = value; }
        }

        public string RBAC_User_Site
        {
            get { return _RBAC_User_Site; }
            set { _RBAC_User_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public IList<RBACRoleInfo> RBACRoleInfos
        {
            get { return _RBACRoleInfos; }
            set { _RBACRoleInfos = value; }
        }

    }
}
