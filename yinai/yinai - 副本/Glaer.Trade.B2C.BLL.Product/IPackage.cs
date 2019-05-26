using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IPackage
    {
        bool AddPackage(PackageInfo entity, RBACUserInfo UserPrivilege);

        bool EditPackage(PackageInfo entity, RBACUserInfo UserPrivilege);

        int DelPackage(int ID, RBACUserInfo UserPrivilege);

        PackageInfo GetPackageByID(int ID, RBACUserInfo UserPrivilege);

        IList<PackageInfo> GetPackages(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        string GetPackageIDByProductID(int ProductID, RBACUserInfo UserPrivilege);

    }

}
