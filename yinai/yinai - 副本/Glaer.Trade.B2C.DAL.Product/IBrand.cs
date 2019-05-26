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
    public interface IBrand
    {
        bool AddBrand(BrandInfo entity);

        bool EditBrand(BrandInfo entity);

        int DelBrand(int Brand_ID);

        BrandInfo GetBrandByID(int Brand_ID);

        IList<BrandInfo> GetBrands(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        string Get_Cate_Brand(string Cate_ID);
    }
}
