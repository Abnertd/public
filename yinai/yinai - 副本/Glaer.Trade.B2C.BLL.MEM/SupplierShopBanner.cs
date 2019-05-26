using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierShopBanner : ISupplierShopBanner
    {
        protected DAL.MEM.ISupplierShopBanner MyDAL;
        protected IRBAC RBAC;

        public SupplierShopBanner()
        {
            MyDAL = DAL.MEM.SupplierShopBannerFactory.CreateSupplierShopBanner();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopBanner(SupplierShopBannerInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "bdf7d616-c68b-439b-97a7-ac19aebfacba"))
            {
                return MyDAL.AddSupplierShopBanner(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：bdf7d616-c68b-439b-97a7-ac19aebfacba错误");
            } 
        }

        public virtual bool EditSupplierShopBanner(SupplierShopBannerInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9f2a1a11-c019-4443-b6eb-18ab1483e0b9"))
            {
                return MyDAL.EditSupplierShopBanner(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9f2a1a11-c019-4443-b6eb-18ab1483e0b9错误");
            } 
        }

        public virtual int DelSupplierShopBanner(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a574ef1a-b5ce-43ba-ab38-3470a9896237"))
            {
                return MyDAL.DelSupplierShopBanner(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a574ef1a-b5ce-43ba-ab38-3470a9896237错误");
            } 
        }

        public virtual SupplierShopBannerInfo GetSupplierShopBannerByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "daff677a-1be4-4438-b1e8-32b453275341"))
            {
                return MyDAL.GetSupplierShopBannerByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：daff677a-1be4-4438-b1e8-32b453275341错误");
            } 
        }

        public virtual IList<SupplierShopBannerInfo> GetSupplierShopBanners(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "daff677a-1be4-4438-b1e8-32b453275341"))
            {
                return MyDAL.GetSupplierShopBanners(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：daff677a-1be4-4438-b1e8-32b453275341错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "daff677a-1be4-4438-b1e8-32b453275341"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：daff677a-1be4-4438-b1e8-32b453275341错误");
            } 
        }

    }


}

