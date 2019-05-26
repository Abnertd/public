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
    public class Supplier : ISupplier
    {
        protected DAL.MEM.ISupplier MyDAL;
        protected IRBAC RBAC;

        public Supplier()
        {
            MyDAL = DAL.MEM.SupplierFactory.CreateSupplier();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplier(SupplierInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6834de44-d231-42bc-a89f-3b0e4461fcc1"))
            {
                return MyDAL.AddSupplier(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6834de44-d231-42bc-a89f-3b0e4461fcc1错误");
            }
        }

        public virtual bool EditSupplier(SupplierInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "40f51178-030c-402a-bee4-57ed6d1ca03f"))
            {
                return MyDAL.EditSupplier(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：40f51178-030c-402a-bee4-57ed6d1ca03f错误");
            }
        }

        public virtual bool UpdateSupplierLogin(int Supplier_ID, int Count, string Remote_IP, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "40f51178-030c-402a-bee4-57ed6d1ca03f"))
            {
                return MyDAL.UpdateSupplierLogin(Supplier_ID, Count, Remote_IP);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：40f51178-030c-402a-bee4-57ed6d1ca03f错误");
            }
        }

        public virtual int DelSupplier(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "97464e1d-061d-4a40-82fb-b7c8c0409894"))
            {
                return MyDAL.DelSupplier(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：97464e1d-061d-4a40-82fb-b7c8c0409894错误");
            }
        }

        public virtual SupplierInfo GetSupplierByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1392d14a-6746-4167-804a-d04a2f81d226"))
            {
                return MyDAL.GetSupplierByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1392d14a-6746-4167-804a-d04a2f81d226错误");
            }
        }

        public virtual SupplierInfo GetSupplierByEmail(string Email, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1392d14a-6746-4167-804a-d04a2f81d226"))
            {
                return MyDAL.GetSupplierByEmail(Email);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1392d14a-6746-4167-804a-d04a2f81d226错误");
            }
        }

        public virtual SupplierInfo SupplierLogin(string name, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1392d14a-6746-4167-804a-d04a2f81d226"))
            {
                return MyDAL.SupplierLogin(name);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1392d14a-6746-4167-804a-d04a2f81d226错误");
            }
        }

        public virtual IList<SupplierInfo> GetSuppliers(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1392d14a-6746-4167-804a-d04a2f81d226"))
            {
                return MyDAL.GetSuppliers(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1392d14a-6746-4167-804a-d04a2f81d226错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1392d14a-6746-4167-804a-d04a2f81d226"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1392d14a-6746-4167-804a-d04a2f81d226错误");
            }
        }

        public virtual bool EditSupplierDeliveryFee(SupplierDeliveryFeeInfo entity)
        {
            return MyDAL.EditSupplierDeliveryFee(entity);
        }

        public virtual int DelSupplierDeliveryFee(int Supplier_ID, int Delivery_ID)
        {
            return MyDAL.DelSupplierDeliveryFee(Supplier_ID,Delivery_ID);
        }

        public virtual SupplierDeliveryFeeInfo GetSupplierDeliveryFeeByID(int Supplier_ID, int Delivery_ID)
        {
            return MyDAL.GetSupplierDeliveryFeeByID(Supplier_ID, Delivery_ID);
        }

        public virtual bool AddSupplierRelateCert(SupplierRelateCertInfo entity)
        {
            return MyDAL.AddSupplierRelateCert(entity);
        }

        public virtual bool EditSupplierRelateCert(SupplierRelateCertInfo entity)
        {
            return MyDAL.EditSupplierRelateCert(entity);
        }

        public virtual int DelSupplierRelateCertBySupplierID(int ID)
        {
            return MyDAL.DelSupplierRelateCertBySupplierID(ID);
        }

    }

    public class SupplierCommissionCategory : ISupplierCommissionCategory
    {
        protected DAL.MEM.ISupplierCommissionCategory MyDAL;
        protected IRBAC RBAC;

        public SupplierCommissionCategory()
        {
            MyDAL = DAL.MEM.SupplierCommissionCategoryFactory.CreateSupplierCommissionCategory();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierCommissionCategory(SupplierCommissionCategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fca14348-91f1-4522-8063-98ff215d5dab"))
            {
                return MyDAL.AddSupplierCommissionCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }

        public virtual bool EditSupplierCommissionCategory(SupplierCommissionCategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "deaa9168-3ffc-42c3-bb94-829fbf7f2e22"))
            {
                return MyDAL.EditSupplierCommissionCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }

        public virtual int DelSupplierCommissionCategory(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "07d26693-d9d7-459b-a097-b6c5e763f8f7"))
            {
                return MyDAL.DelSupplierCommissionCategory(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }

        public virtual SupplierCommissionCategoryInfo GetSupplierCommissionCategoryByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed55dd89-e07e-438d-9529-a46de2cdda7b"))
            {
                return MyDAL.GetSupplierCommissionCategoryByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }

        public virtual IList<SupplierCommissionCategoryInfo> GetSupplierCommissionCategorys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed55dd89-e07e-438d-9529-a46de2cdda7b"))
            {
                return MyDAL.GetSupplierCommissionCategorys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ed55dd89-e07e-438d-9529-a46de2cdda7b"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            } 
        }



    }

    public class SupplierMerchants : ISupplierMerchants
    {
        protected DAL.MEM.ISupplierMerchants MyDAL;
        protected IRBAC RBAC;

        public SupplierMerchants()
        {
            MyDAL = DAL.MEM.SupplierMerchantsFactory.CreateSupplierMerchants();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierMerchants(SupplierMerchantsInfo entity)
        {
            return MyDAL.AddSupplierMerchants(entity);
        }

        public virtual bool EditSupplierMerchants(SupplierMerchantsInfo entity)
        {
            return MyDAL.EditSupplierMerchants(entity);
        }

        public virtual int DelSupplierMerchants(int ID)
        {
            return MyDAL.DelSupplierMerchants(ID);
        }

        public virtual SupplierMerchantsInfo GetSupplierMerchantsByID(int ID)
        {
            return MyDAL.GetSupplierMerchantsByID(ID);
        }

        public virtual IList<SupplierMerchantsInfo> GetSupplierMerchantss(QueryInfo Query)
        {
            return MyDAL.GetSupplierMerchantss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class SupplierMerchantsMessage : ISupplierMerchantsMessage
    {
        protected DAL.MEM.ISupplierMerchantsMessage MyDAL;
        protected IRBAC RBAC;

        public SupplierMerchantsMessage()
        {
            MyDAL = DAL.MEM.SupplierMerchantsMessageFactory.CreateSupplierMerchantsMessage();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity)
        {
            return MyDAL.AddSupplierMerchantsMessage(entity);
        }

        public virtual bool EditSupplierMerchantsMessage(SupplierMerchantsMessageInfo entity)
        {
            return MyDAL.EditSupplierMerchantsMessage(entity);
        }

        public virtual int DelSupplierMerchantsMessage(int ID)
        {
            return MyDAL.DelSupplierMerchantsMessage(ID);
        }

        public virtual SupplierMerchantsMessageInfo GetSupplierMerchantsMessageByID(int ID)
        {
            return MyDAL.GetSupplierMerchantsMessageByID(ID);
        }

        public virtual IList<SupplierMerchantsMessageInfo> GetSupplierMerchantsMessages(QueryInfo Query)
        {
            return MyDAL.GetSupplierMerchantsMessages(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class SupplierMargin : ISupplierMargin
    {
        protected DAL.MEM.ISupplierMargin MyDAL;
        protected IRBAC RBAC;

        public SupplierMargin()
        {
            MyDAL = DAL.MEM.SupplierMarginFactory.CreateSupplierMargin();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierMargin(SupplierMarginInfo entity)
        {
            return MyDAL.AddSupplierMargin(entity);
        }

        public virtual bool EditSupplierMargin(SupplierMarginInfo entity)
        {
            return MyDAL.EditSupplierMargin(entity);
        }

        public virtual int DelSupplierMargin(int ID)
        {
            return MyDAL.DelSupplierMargin(ID);
        }

        public virtual SupplierMarginInfo GetSupplierMarginByID(int ID)
        {
            return MyDAL.GetSupplierMarginByID(ID);
        }

        public virtual IList<SupplierMarginInfo> GetSupplierMargins(QueryInfo Query)
        {
            return MyDAL.GetSupplierMargins(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }


        public SupplierMarginInfo GetSupplierMarginByTypeID(int Type_ID)
        {
            return MyDAL.GetSupplierMarginByTypeID(Type_ID);
        }
    }
}

