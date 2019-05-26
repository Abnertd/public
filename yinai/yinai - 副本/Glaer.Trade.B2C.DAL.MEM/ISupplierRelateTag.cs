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
    public interface ISupplierTag
    {
        bool AddSupplierTag(SupplierTagInfo entity);

        bool EditSupplierTag(SupplierTagInfo entity);

        int DelSupplierTag(int ID);

        SupplierTagInfo GetSupplierTagByID(int ID);

        IList<SupplierTagInfo> GetSupplierTags(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddSupplierRelateTag(SupplierRelateTagInfo entity);

        int DelSupplierRelateTagBySupplierID(int Supplier_ID);

        IList<SupplierRelateTagInfo> GetSupplierRelateTagsBySupplierID(int Supplier_ID);
    }


}
