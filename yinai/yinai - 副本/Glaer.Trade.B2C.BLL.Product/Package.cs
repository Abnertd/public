using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class Package : IPackage
    {
        protected DAL.Product.IPackage MyDAL;
        protected IRBAC RBAC;

        public Package()
        {
            MyDAL = DAL.Product.PackageFactory.CreatePackage();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddPackage(PackageInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "573393a4-573e-4872-ad7b-b77d75e0f611"))
            {
                return MyDAL.AddPackage(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：573393a4-573e-4872-ad7b-b77d75e0f611错误");
            }
        }

        public virtual bool EditPackage(PackageInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "5666872b-2113-490b-a41f-a7a65083324a"))
            {
                return MyDAL.EditPackage(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：5666872b-2113-490b-a41f-a7a65083324a错误");
            }
        }

        public virtual int DelPackage(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fc830fc1-1192-4097-9d92-e625a707cbc1"))
            {
                return MyDAL.DelPackage(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fc830fc1-1192-4097-9d92-e625a707cbc1错误");
            }
        }

        public virtual PackageInfo GetPackageByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0dd17a70-862d-4e57-9b45-897b98e8a858"))
            {
                return MyDAL.GetPackageByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0dd17a70-862d-4e57-9b45-897b98e8a858错误");
            }
        }

        public virtual IList<PackageInfo> GetPackages(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0dd17a70-862d-4e57-9b45-897b98e8a858"))
            {
                return MyDAL.GetPackages(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0dd17a70-862d-4e57-9b45-897b98e8a858错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0dd17a70-862d-4e57-9b45-897b98e8a858"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0dd17a70-862d-4e57-9b45-897b98e8a858错误");
            }
        }

        public virtual string GetPackageIDByProductID(int ProductID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0dd17a70-862d-4e57-9b45-897b98e8a858"))
            {
                return MyDAL.GetPackageIDByProductID(ProductID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0dd17a70-862d-4e57-9b45-897b98e8a858错误");
            }
        }

    }
}
