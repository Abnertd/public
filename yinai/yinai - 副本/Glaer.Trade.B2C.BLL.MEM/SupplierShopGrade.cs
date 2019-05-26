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
    public class SupplierShopGrade : ISupplierShopGrade
    {
        protected DAL.MEM.ISupplierShopGrade MyDAL;
        protected IRBAC RBAC;

        public SupplierShopGrade()
        {
            MyDAL = DAL.MEM.SupplierShopGradeFactory.CreateSupplierShopGrade();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierShopGrade(SupplierShopGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ec8e6ed4-907f-4777-be1c-e07690e2eab0"))
            {
                return MyDAL.AddSupplierShopGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ec8e6ed4-907f-4777-be1c-e07690e2eab0错误");
            } 
        }

        public virtual bool EditSupplierShopGrade(SupplierShopGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "adde3836-fe74-4976-9297-61fe4b3db991"))
            {
                return MyDAL.EditSupplierShopGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：adde3836-fe74-4976-9297-61fe4b3db991错误");
            } 
        }

        public virtual int DelSupplierShopGrade(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3ba627a6-0a91-48d3-a4bc-9e2a84fc8dba"))
            {
                return MyDAL.DelSupplierShopGrade(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3ba627a6-0a91-48d3-a4bc-9e2a84fc8dba错误");
            } 
        }

        public virtual SupplierShopGradeInfo GetSupplierShopGradeByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c558f983-68ec-4a91-a330-1c1f04ebdf01"))
            {
                return MyDAL.GetSupplierShopGradeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c558f983-68ec-4a91-a330-1c1f04ebdf01错误");
            } 
        }

        public virtual IList<SupplierShopGradeInfo> GetSupplierShopGrades(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c558f983-68ec-4a91-a330-1c1f04ebdf01"))
            {
                return MyDAL.GetSupplierShopGrades(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c558f983-68ec-4a91-a330-1c1f04ebdf01错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c558f983-68ec-4a91-a330-1c1f04ebdf01"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c558f983-68ec-4a91-a330-1c1f04ebdf01错误");
            } 
        }

    }



}

