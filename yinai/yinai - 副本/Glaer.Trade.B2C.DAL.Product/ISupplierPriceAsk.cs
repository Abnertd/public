using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface ISupplierPriceAsk
    {
        bool AddSupplierPriceAsk(SupplierPriceAskInfo entity);

        bool EditSupplierPriceAsk(SupplierPriceAskInfo entity);

        int DelSupplierPriceAsk(int ID);

        int DelSupplierPriceAskByProductID(int ProductID);

        SupplierPriceAskInfo GetSupplierPriceAskByID(int ID);

        IList<SupplierPriceAskInfo> GetSupplierPriceAsks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
