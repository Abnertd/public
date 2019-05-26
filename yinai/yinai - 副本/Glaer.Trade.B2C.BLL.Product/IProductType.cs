using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IProductType
    {
        bool AddProductType(ProductTypeInfo entity, RBACUserInfo UserPrivilege);

        bool AddProductType_Brand(int ProductType_ID, int Brand_ID, RBACUserInfo UserPrivilege);

        bool EditProductType(ProductTypeInfo entity, RBACUserInfo UserPrivilege);

        int DelProductType(int ID, RBACUserInfo UserPrivilege);

        int DelProductType_Brand(int ID, RBACUserInfo UserPrivilege);

        int DelProductType_Brand(int ID, int Brand_ID, RBACUserInfo UserPrivilege);

        int DelProductType_Extend(int ID, RBACUserInfo UserPrivilege);

        ProductTypeInfo GetProductTypeByID(int ID, RBACUserInfo UserPrivilege);

        ProductTypeInfo GetProductTypeMax(RBACUserInfo UserPrivilege);

        IList<ProductTypeInfo> GetProductTypes(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<BrandInfo> GetProductBrands(int ProductTypeID, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
