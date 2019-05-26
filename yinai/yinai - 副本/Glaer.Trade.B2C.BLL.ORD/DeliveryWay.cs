using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class DeliveryWay : IDeliveryWay
    {
        protected DAL.ORD.IDeliveryWay MyDAL;
        protected IRBAC RBAC;

        public DeliveryWay() {
            MyDAL = DAL.ORD.DeliveryWayFactory.CreateDeliveryWay();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddDeliveryWay(DeliveryWayInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.AddDeliveryWay(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

        public virtual bool EditDeliveryWay(DeliveryWayInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "58d92d67-4e0b-4a4c-bd5c-6062c432554d"))
            {
                return MyDAL.EditDeliveryWay(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：58d92d67-4e0b-4a4c-bd5c-6062c432554d错误");
            }
        }

        public virtual int DelDeliveryWay(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9909b492-b55c-49bf-b726-0f2d36e7ff4b"))
            {
                return MyDAL.DelDeliveryWay(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9909b492-b55c-49bf-b726-0f2d36e7ff4b错误");
            }
        }

        public virtual DeliveryWayInfo GetDeliveryWayByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "837c9372-3b25-494f-b141-767e195e3c88"))
            {
                return MyDAL.GetDeliveryWayByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：837c9372-3b25-494f-b141-767e195e3c88错误");
            }
        }

        public virtual IList<DeliveryWayInfo> GetDeliveryWays(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "837c9372-3b25-494f-b141-767e195e3c88"))
            {
                return MyDAL.GetDeliveryWays(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：837c9372-3b25-494f-b141-767e195e3c88错误");
            }
        }

        public virtual IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state, string city, string county, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "837c9372-3b25-494f-b141-767e195e3c88"))
            {
                return MyDAL.GetDeliveryWaysByDistrict(state, city, county);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "837c9372-3b25-494f-b141-767e195e3c88"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：837c9372-3b25-494f-b141-767e195e3c88错误");
            }
        }

        public virtual bool AddDeliveryWayDistrict(DeliveryWayDistrictInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.AddDeliveryWayDistrict(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

        public virtual bool EditDeliveryWayDistrict(DeliveryWayDistrictInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.EditDeliveryWayDistrict(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

        public virtual int DelDeliveryWayDistrict(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.DelDeliveryWayDistrict(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

        public virtual DeliveryWayDistrictInfo GetDeliveryWayDistrictByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.GetDeliveryWayDistrictByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

        public virtual IList<DeliveryWayDistrictInfo> GetDeliveryWayDistrictsByDWID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8632585c-0447-4003-a97d-48cade998a05"))
            {
                return MyDAL.GetDeliveryWayDistrictsByDWID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8632585c-0447-4003-a97d-48cade998a05错误");
            }
        }

    }
}
