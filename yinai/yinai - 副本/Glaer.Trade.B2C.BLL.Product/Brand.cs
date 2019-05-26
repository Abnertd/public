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
    public class Brand : IBrand
    {
        protected DAL.Product.IBrand MyDAL;
        protected IRBAC RBAC;

        public Brand()
        {
            MyDAL = DAL.Product.BrandFactory.CreateBrand();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBrand(BrandInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "31d0ad2e-dd9b-4a04-a800-407b8ba3c9e9"))
            {
                return MyDAL.AddBrand(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：31d0ad2e-dd9b-4a04-a800-407b8ba3c9e9错误");
            }
        }

        public virtual bool EditBrand(BrandInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9592b436-454a-42cf-83f4-0d9ce83c339a"))
            {
                return MyDAL.EditBrand(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9592b436-454a-42cf-83f4-0d9ce83c339a错误");
            }
        }

        public virtual int DelBrand(int cate_id, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3297a5d3-44e6-4318-aa23-4d31288a291b"))
            {
                return MyDAL.DelBrand(cate_id);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3297a5d3-44e6-4318-aa23-4d31288a291b错误");
            }
        }

        public virtual BrandInfo GetBrandByID(int Cate_ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9b17d437-fb2a-4caa-821e-daf13d9efae4"))
            {
                return MyDAL.GetBrandByID(Cate_ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9b17d437-fb2a-4caa-821e-daf13d9efae4错误");
            }
        }

        public virtual IList<BrandInfo> GetBrands(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9b17d437-fb2a-4caa-821e-daf13d9efae4"))
            {
                return MyDAL.GetBrands(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9b17d437-fb2a-4caa-821e-daf13d9efae4错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9b17d437-fb2a-4caa-821e-daf13d9efae4"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9b17d437-fb2a-4caa-821e-daf13d9efae4错误");
            }
        }

        public virtual string Get_Cate_Brand(string Cate_ID)
        {
            return MyDAL.Get_Cate_Brand(Cate_ID);
        }

    }
}

