using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.DAL.Product
{
    public interface IPackage
    {
        bool AddPackage(PackageInfo entity);

        bool EditPackage(PackageInfo entity);

        int DelPackage(int ID);

        PackageInfo GetPackageByID(int ID);

        IList<PackageInfo> GetPackages(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddPackageProduct(IList<PackageProductInfo> entitys, int package_id);

        /// <summary>
        /// 删除该捆绑下的所有商品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        int DelPackageProduct(int ID);

        int GetLastPackage(string package_name);

        IList<PackageProductInfo> GetProductListByPackage(int ID);

        string GetPackageIDByProductID(int ProductID);

    }

}
