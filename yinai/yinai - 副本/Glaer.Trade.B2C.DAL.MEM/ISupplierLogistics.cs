using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface ISupplierLogistics
    {
        bool AddSupplierLogistics(SupplierLogisticsInfo entity);

        bool EditSupplierLogistics(SupplierLogisticsInfo entity);

        int DelSupplierLogistics(int ID);

        SupplierLogisticsInfo GetSupplierLogisticsByID(int ID);

        IList<SupplierLogisticsInfo> GetSupplierLogisticss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface ILogisticsTender
    {
        bool AddLogisticsTender(LogisticsTenderInfo entity);

        bool EditLogisticsTender(LogisticsTenderInfo entity);

        int DelLogisticsTender(int ID);

        LogisticsTenderInfo GetLogisticsTenderByID(int ID);

        IList<LogisticsTenderInfo> GetLogisticsTenders(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
