using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierGrade : ISupplierGrade
    {
        protected DAL.MEM.ISupplierGrade MyDAL;
        protected IRBAC RBAC;

        public SupplierGrade()
        {
            MyDAL = DAL.MEM.SupplierGradeFactory.CreateSupplierGrade();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierGrade(SupplierGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a9ed67d8-d6b1-4518-87ac-f01dadaba761"))
            {
                return MyDAL.AddSupplierGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a9ed67d8-d6b1-4518-87ac-f01dadaba761错误");
            } 
        }

        public virtual bool EditSupplierGrade(SupplierGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "065594cf-5094-4ce6-b753-3c360d3213bd"))
            {
                return MyDAL.EditSupplierGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：065594cf-5094-4ce6-b753-3c360d3213bd错误");
            } 
        }

        public virtual int DelSupplierGrade(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3c4246f5-0b23-4c6e-8c73-65c14a2a76bc"))
            {
                return MyDAL.DelSupplierGrade(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3c4246f5-0b23-4c6e-8c73-65c14a2a76bc错误");
            } 
        }

        public virtual SupplierGradeInfo GetSupplierGradeByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1d3f7ace-2191-4c5e-9403-840ddaf191c0"))
            {
                return MyDAL.GetSupplierGradeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1d3f7ace-2191-4c5e-9403-840ddaf191c0错误");
            } 
        }

        public virtual IList<SupplierGradeInfo> GetSupplierGrades(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1d3f7ace-2191-4c5e-9403-840ddaf191c0"))
            {
                return MyDAL.GetSupplierGrades(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1d3f7ace-2191-4c5e-9403-840ddaf191c0错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1d3f7ace-2191-4c5e-9403-840ddaf191c0"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1d3f7ace-2191-4c5e-9403-840ddaf191c0错误");
            } 
        }

        public virtual SupplierGradeInfo GetSupplierDefaultGrade()
        {
            return MyDAL.GetSupplierDefaultGrade();
        }

    }

}
