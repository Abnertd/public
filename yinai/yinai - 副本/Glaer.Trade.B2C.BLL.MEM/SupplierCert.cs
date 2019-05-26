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
    public class SupplierCert : ISupplierCert
    {
        protected DAL.MEM.ISupplierCert MyDAL;
        protected IRBAC RBAC;

        public SupplierCert()
        {
            MyDAL = DAL.MEM.SupplierCertFactory.CreateSupplierCert();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierCert(SupplierCertInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a1cb6100-8590-4dec-953b-59b13002df83"))
            {
                return MyDAL.AddSupplierCert(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a1cb6100-8590-4dec-953b-59b13002df83错误");
            } 
        }

        public virtual bool EditSupplierCert(SupplierCertInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b399de70-e5f8-4d76-b0d7-16dc38245efc"))
            {
                return MyDAL.EditSupplierCert(entity); 
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b399de70-e5f8-4d76-b0d7-16dc38245efc错误");
            } 
        }

        public virtual int DelSupplierCert(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2760865e-7bac-4e14-8e54-a7de7e99fee6"))
            {
                return MyDAL.DelSupplierCert(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2760865e-7bac-4e14-8e54-a7de7e99fee6错误");
            } 
        }

        public virtual SupplierCertInfo GetSupplierCertByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "29f32a17-8d3f-4ca5-9628-524316760713"))
            {
                return MyDAL.GetSupplierCertByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：29f32a17-8d3f-4ca5-9628-524316760713错误");
            } 
        }

        public virtual IList<SupplierCertInfo> GetSupplierCerts(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "29f32a17-8d3f-4ca5-9628-524316760713"))
            {
                return MyDAL.GetSupplierCerts(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：29f32a17-8d3f-4ca5-9628-524316760713错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "29f32a17-8d3f-4ca5-9628-524316760713"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：29f32a17-8d3f-4ca5-9628-524316760713错误");
            } 
        }

    }

    public class SupplierCertType : ISupplierCertType
    {
        protected DAL.MEM.ISupplierCertType MyDAL;
        protected IRBAC RBAC;

        public SupplierCertType()
        {
            MyDAL = DAL.MEM.SupplierCertFactory.CreateSupplierCertType();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierCertType(SupplierCertTypeInfo entity)
        {
            return MyDAL.AddSupplierCertType(entity);
        }

        public virtual bool EditSupplierCertType(SupplierCertTypeInfo entity)
        {
            return MyDAL.EditSupplierCertType(entity);
        }

        public virtual int DelSupplierCertType(int ID)
        {
            return MyDAL.DelSupplierCertType(ID);
        }

        public virtual SupplierCertTypeInfo GetSupplierCertTypeByID(int ID)
        {
            return MyDAL.GetSupplierCertTypeByID(ID);
        }

        public virtual IList<SupplierCertTypeInfo> GetSupplierCertTypes(QueryInfo Query)
        {
            return MyDAL.GetSupplierCertTypes(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

}

