using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.RBAC
{
    public class RBAC : IRBAC
    {
        public virtual bool CheckPrivilege(RBACUserInfo User, string PrivilegeCode)
        {
            try
            {
                bool HavePrivilege = false;
                if (User != null)
                {

                    string[] codeArray = System.Text.RegularExpressions.Regex.Split(PrivilegeCode, "/");
                    foreach (string code in codeArray)
                    {

                        foreach (RBACRoleInfo Role in User.RBACRoleInfos)
                        {
                            foreach (RBACPrivilegeInfo Privilege in Role.RBACPrivilegeInfos)
                            {
                                if (Privilege.RBAC_Privilege_ID == code)
                                {
                                    HavePrivilege = true;
                                }
                            }
                        }


                    }

                }
                
                return HavePrivilege;
            }
            catch
            {
                return false;
            }
        }
    }
}
