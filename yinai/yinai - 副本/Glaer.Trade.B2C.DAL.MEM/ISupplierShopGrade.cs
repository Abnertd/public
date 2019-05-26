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
    public interface ISupplierShopGrade
    {
        bool AddSupplierShopGrade(SupplierShopGradeInfo entity);

        bool EditSupplierShopGrade(SupplierShopGradeInfo entity);

        int DelSupplierShopGrade(int ID);

        SupplierShopGradeInfo GetSupplierShopGradeByID(int ID);

        IList<SupplierShopGradeInfo> GetSupplierShopGrades(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }


}
