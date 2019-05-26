using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;


namespace Glaer.Trade.B2C.BLL.Product
{
    public class HomeLeftCate : IHomeLeftCate
    {
        protected DAL.Product.IHomeLeftCate MyDAL;
        protected IRBAC RBAC;

        public HomeLeftCate()
        {
            MyDAL = DAL.Product.HomeLeftCateFactory.CreateHomeLeftCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddHomeLeftCate(HomeLeftCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8738cd22-6808-4fdd-94f4-d9bb51b64509"))
            {
                return MyDAL.AddHomeLeftCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8738cd22-6808-4fdd-94f4-d9bb51b64509错误");
            } 
        }

        public virtual bool EditHomeLeftCate(HomeLeftCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "de88931b-4a5b-4bb7-8f68-4975ad26e59c"))
            {
                return MyDAL.EditHomeLeftCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：de88931b-4a5b-4bb7-8f68-4975ad26e59c错误");
            } 
        }

        public virtual int DelHomeLeftCate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4196e669-c4c0-4209-bbed-bc99a951c2c8"))
            {
                return MyDAL.DelHomeLeftCate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4196e669-c4c0-4209-bbed-bc99a951c2c8错误");
            } 
        }

        public virtual int DelHomeLeftCateAll(RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "4196e669-c4c0-4209-bbed-bc99a951c2c8"))
            {
                return MyDAL.DelHomeLeftCateAll();
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4196e669-c4c0-4209-bbed-bc99a951c2c8错误");
            } 
        }

        public virtual HomeLeftCateInfo GetHomeLeftCateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d843afda-7680-45fa-bc00-32278bf77ae8"))
            {
                return MyDAL.GetHomeLeftCateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d843afda-7680-45fa-bc00-32278bf77ae8错误");
            } 
        }

        public virtual HomeLeftCateInfo GetHomeLeftCateByLastID(RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d843afda-7680-45fa-bc00-32278bf77ae8"))
            {
                return MyDAL.GetHomeLeftCateByLastID();
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d843afda-7680-45fa-bc00-32278bf77ae8错误");
            }
        }

        public virtual IList<HomeLeftCateInfo> GetHomeLeftCates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d843afda-7680-45fa-bc00-32278bf77ae8"))
            {
                return MyDAL.GetHomeLeftCates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d843afda-7680-45fa-bc00-32278bf77ae8错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d843afda-7680-45fa-bc00-32278bf77ae8"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d843afda-7680-45fa-bc00-32278bf77ae8错误");
            } 
        }

    }


}
