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
    public interface IProductTag
    {
        bool AddProductTag(ProductTagInfo entity, RBACUserInfo UserPrivilege);

        bool EditProductTag(ProductTagInfo entity, RBACUserInfo UserPrivilege);

        int DelProductTag(int Cate_ID, RBACUserInfo UserPrivilege);

        ProductTagInfo GetProductTagByID(int Cate_ID, RBACUserInfo UserPrivilege);

        ProductTagInfo GetProductTagByValue(string tag_Value, RBACUserInfo UserPrivilege);

        IList<ProductTagInfo> GetProductTags(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        int AddProductRelateTag(string Product_RelateTag_ProductID, int Product_RelateTag_TagID);

    }
}



