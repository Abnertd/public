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
    public class SupplierTag : ISupplierTag
    {
        protected DAL.MEM.ISupplierTag MyDAL;
        protected IRBAC RBAC;

        public SupplierTag()
        {
            MyDAL = DAL.MEM.SupplierTagFactory.CreateSupplierTag();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierTag(SupplierTagInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "da202dcc-2dba-4d0b-829f-d170541a1e80"))
            {
                return MyDAL.AddSupplierTag(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：da202dcc-2dba-4d0b-829f-d170541a1e80错误");
            } 
            
        }

        public virtual bool EditSupplierTag(SupplierTagInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a6691534-a5e6-4636-901b-88a62ea1acc1"))
            {
                return MyDAL.EditSupplierTag(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a6691534-a5e6-4636-901b-88a62ea1acc1错误");
            } 
        }

        public virtual int DelSupplierTag(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9a2249c4-2c18-4902-b8ea-7e597084cca5"))
            {
                return MyDAL.DelSupplierTag(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9a2249c4-2c18-4902-b8ea-7e597084cca5错误");
            } 
        }

        public virtual SupplierTagInfo GetSupplierTagByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "169befcc-aa3b-42d1-b5b8-d1a08096bc0e"))
            {
                return MyDAL.GetSupplierTagByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：169befcc-aa3b-42d1-b5b8-d1a08096bc0e错误");
            } 
        }

        public virtual IList<SupplierTagInfo> GetSupplierTags(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "169befcc-aa3b-42d1-b5b8-d1a08096bc0e"))
            {
                return MyDAL.GetSupplierTags(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：169befcc-aa3b-42d1-b5b8-d1a08096bc0e错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "169befcc-aa3b-42d1-b5b8-d1a08096bc0e"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：169befcc-aa3b-42d1-b5b8-d1a08096bc0e错误");
            } 
        }

        public virtual bool AddSupplierRelateTag(SupplierRelateTagInfo entity)
        {
            return MyDAL.AddSupplierRelateTag(entity);
        }

        public virtual int DelSupplierRelateTagBySupplierID(int Supplier_ID)
        {
            return MyDAL.DelSupplierRelateTagBySupplierID(Supplier_ID);
        }

        public virtual IList<SupplierRelateTagInfo> GetSupplierRelateTagsBySupplierID(int Supplier_ID)
        {
            return MyDAL.GetSupplierRelateTagsBySupplierID(Supplier_ID);
        }
    }



}

