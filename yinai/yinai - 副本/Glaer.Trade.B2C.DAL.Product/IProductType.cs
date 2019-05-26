using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IProductType
    {
        bool AddProductType(ProductTypeInfo entity);

        bool AddProductType_Brand(int ProductType_ID, int Brand_ID);

        bool EditProductType(ProductTypeInfo entity);

        int DelProductType(int ID);

        int DelProductType_Brand(int ID);

        int DelProductType_Brand(int ID, int Brand_ID);

        int DelProductType_Extend(int ProductType_ID);

        ProductTypeInfo GetProductTypeByID(int ID);

        ProductTypeInfo GetProductTypeMax();

        IList<ProductTypeInfo> GetProductTypes(QueryInfo Query);

        IList<BrandInfo> GetProductBrands(int ProductTypeID);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
