using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface ICategory
    {
        bool AddCategory(CategoryInfo entity, RBACUserInfo UserPrivilege);

        bool EditCategory(CategoryInfo entity, RBACUserInfo UserPrivilege);

        int DelCategory(int Cate_ID, RBACUserInfo UserPrivilege);

        CategoryInfo GetCategoryByID(int Cate_ID, RBACUserInfo UserPrivilege);

        IList<CategoryInfo> GetCategorys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        int GetSubCateCount(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege);

        IList<CategoryInfo> GetSubCategorys(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege);

        string SelectCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID, string SiteSign, RBACUserInfo UserPrivilege);

        string DisplayCategoryRecursion(int cate_id, string href, RBACUserInfo UserPrivilege);

        string Get_All_SubCateID(int Cate_ID);
    }
}
