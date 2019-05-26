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
    public class SupplierShopCss : ISupplierShopCss
    {
        protected DAL.MEM.ISupplierShopCss MyDAL;
        protected IRBAC RBAC;

        public SupplierShopCss()
        {
            MyDAL = DAL.MEM.SupplierShopCssFactory.CreateSupplierShopCss();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopCss(SupplierShopCssInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8c936480-7e6e-4482-9e22-5eb9b1fbec8a"))
            {
                return MyDAL.AddSupplierShopCss(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8c936480-7e6e-4482-9e22-5eb9b1fbec8a错误");
            } 
        }

        public virtual bool EditSupplierShopCss(SupplierShopCssInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "227ca224-42de-48c6-9e4b-d09d019f7b36"))
            {
                return MyDAL.EditSupplierShopCss(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：227ca224-42de-48c6-9e4b-d09d019f7b36错误");
            } 
        }

        public virtual int DelSupplierShopCss(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8407715f-18d7-445b-92a1-0c7ce9cc027a"))
            {
                return MyDAL.DelSupplierShopCss(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8407715f-18d7-445b-92a1-0c7ce9cc027a错误");
            } 
        }

        public virtual SupplierShopCssInfo GetSupplierShopCssByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3396b3c6-8116-4c3b-9682-6d29c937947e"))
            {
                return MyDAL.GetSupplierShopCssByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3396b3c6-8116-4c3b-9682-6d29c937947e错误");
            } 
        }

        public virtual IList<SupplierShopCssInfo> GetSupplierShopCsss(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3396b3c6-8116-4c3b-9682-6d29c937947e"))
            {
                return MyDAL.GetSupplierShopCsss(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3396b3c6-8116-4c3b-9682-6d29c937947e错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3396b3c6-8116-4c3b-9682-6d29c937947e"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3396b3c6-8116-4c3b-9682-6d29c937947e错误");
            } 
        }

        public virtual bool AddSupplierShopCssRelateSupplier(SupplierShopCssRelateSupplierInfo entity)
        {
            return MyDAL.AddSupplierShopCssRelateSupplier(entity);
        }

        public virtual int DelSupplierShopCssRelateSupplierBySupplierID(int ID)
        {
            return MyDAL.DelSupplierShopCssRelateSupplierBySupplierID(ID);
        }

        public virtual int DelSupplierShopCssRelateSupplierByCssID(int Css_ID)
        {
            return MyDAL.DelSupplierShopCssRelateSupplierByCssID(Css_ID);
        }

        public virtual IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliers(int Relate_SupplierID)
        {
            return MyDAL.GetSupplierShopCssRelateSuppliers(Relate_SupplierID);
        }

        public virtual IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliersByCss(int Css_ID)
        {
            return MyDAL.GetSupplierShopCssRelateSuppliersByCss(Css_ID);
        }

    }



}

