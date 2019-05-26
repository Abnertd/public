using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;


namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierCategory
    {
        #region 产品分组定义
        bool AddSupplierCategory(SupplierCategoryInfo entity);

        bool EditSupplierCategory(SupplierCategoryInfo entity);

        int DelSupplierCategory(int ID);

        SupplierCategoryInfo GetSupplierCategoryByID(int ID);

        SupplierCategoryInfo GetSupplierCategoryByIDSupplier(int ID, int Supplier_ID);

        IList<SupplierCategoryInfo> GetSupplierCategorys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
        #endregion

        #region 产品/产品分组关系
        bool AddSupplierProductCategory(SupplierProductCategoryInfo entity);

        bool EditSupplierProductCategory(SupplierProductCategoryInfo entity);

        int DelSupplierProductCategoryByProductID(int ID);

        int DelSupplierProductCategoryByCateID(int ID);

        IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByProductID(int Product_ID);

        IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByCateID(int Cate_ID);

        string GetSupplierProductCategorysByCateArry(string Cate_ID);
        #endregion
    }


}
