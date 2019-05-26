using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IBrand
    {
        bool AddBrand(BrandInfo entity, RBACUserInfo UserPrivilege);

        bool EditBrand(BrandInfo entity, RBACUserInfo UserPrivilege);

        int DelBrand(int Cate_ID, RBACUserInfo UserPrivilege);

        BrandInfo GetBrandByID(int Cate_ID, RBACUserInfo UserPrivilege);

        IList<BrandInfo> GetBrands(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        string Get_Cate_Brand(string Cate_ID);

    }
}
