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
    public interface IProduct
    {
        bool AddProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends);

        void SaveProductTag(int Product_ID, string[] extends);

        bool EditProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends);

        bool EditProductInfo(ProductInfo entity);

        int DelProduct(int ID);

        ProductInfo GetProductByID(int ID);

        ProductInfo GetProductByCode(string Code, string Site);

        ProductInfo GetProductByName(string Name);

        IList<ProductInfo> GetProducts(QueryInfo Query);

        IList<ProductInfo> GetProductList(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        string GetProductImg(int product_id);

        string GetProductCategory(int product_id);

        IList<ProductTypeExtendInfo> ProductExtendEditor(int ProductType_ID);

        IList<ProductExtendInfo> ProductExtendValue(int Product_ID);

        string GetProductExtendValue(int Extend_ID, string Product_Ids);

        IList<ProductExtendInfo> ProductExtendValues(string Product_Ids);

        IList<ProductExtendInfo> GetProductExtends(QueryInfo Query);

        string GetProductTag(int product_id);

        string GetCateProductID(string Cate_Arry);

        string GetTagProductID(string Tag_Id);

        string GetExtendProductID(int Extend_ID, string Extend_Value);

        int DelProductExtendByID(int Product_ID);

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="code">商品编号</param>
        /// <param name="amount">更新数量</param>
        /// <param name="usableamount">更新数量(可用数量)</param>
        /// <returns></returns>
        bool UpdateProductStock(string code, int amount, int usableamount);

        bool UpdateProductStockExcepNostock(int product_id, int amount, int usableamount);

        bool UpdateProductSaleAmount(int product_id, int amount);

        bool UpdateProductGroupInfo(string Group_Code, int Product_IsListShow, int Product_ID);

        bool UpdateProductGroupCode(string NewGroup_Code, string OldGroup_Code);

        string GetGroupProductID(string Group_Code);
    }

    public interface IProductWholeSalePrice
    {
        bool AddProductWholeSalePrice(ProductWholeSalePriceInfo entity);

        bool EditProductWholeSalePrice(ProductWholeSalePriceInfo entity);

        int DelProductWholeSalePrice(int ID);

        int DelProductWholeSalePriceByProductID(int Product_ID);

        ProductWholeSalePriceInfo GetProductWholeSalePriceByID(int ID);

        IList<ProductWholeSalePriceInfo> GetProductWholeSalePriceByProductID(int ID);

        IList<ProductWholeSalePriceInfo> GetProductWholeSalePrices(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
