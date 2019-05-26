using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface ICategory
    {
        bool AddCategory(CategoryInfo entity);

        bool EditCategory(CategoryInfo entity);

        int DelCategory(int Cate_ID);

        CategoryInfo GetCategoryByID(int Cate_ID);

        IList<CategoryInfo> GetCategorys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        int GetSubCateCount(int Cate_ID, string SiteSign);

        IList<CategoryInfo> GetSubCategorys(int Cate_ID, string SiteSign);

        string Get_All_SubCateID(int Cate_ID);
    }
}
