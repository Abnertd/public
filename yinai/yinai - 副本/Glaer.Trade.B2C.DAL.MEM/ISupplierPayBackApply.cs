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
    public interface ISupplierPayBackApply
    {
        bool AddSupplierPayBackApply(SupplierPayBackApplyInfo entity);

        bool EditSupplierPayBackApply(SupplierPayBackApplyInfo entity);

        int DelSupplierPayBackApply(int ID);

        SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int ID);

        IList<SupplierPayBackApplyInfo> GetSupplierPayBackApplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }


}
