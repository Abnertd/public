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
     public interface IProductTag 
     {
        bool AddProductTag (ProductTagInfo entity);

        bool EditProductTag (ProductTagInfo entity);
        
        int DelProductTag(int Cate_ID);

        ProductTagInfo GetProductTagByID(int product_tag_id);

        ProductTagInfo GetProductTagByValue(string tag_Value);

        IList<ProductTagInfo> GetProductTags(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        int AddProductRelateTag(string Product_RelateTag_ProductID, int Product_RelateTag_TagID);
    }
}


