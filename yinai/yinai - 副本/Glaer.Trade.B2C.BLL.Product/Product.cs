using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class Product : IProduct {
        protected DAL.Product.IProduct MyDAL;
        protected IRBAC RBAC;

        public Product() {
            MyDAL = DAL.Product.ProductFactory.CreateProduct();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a8dcfdfb-2227-40b3-a598-9643fd4c7e18"))
            {
                return MyDAL.AddProduct(entity, cateArray, tagArray, imgArray, extends);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a8dcfdfb-2227-40b3-a598-9643fd4c7e18错误");
            }
        }

        public virtual void SaveProductTag(int Product_ID, string[] extends)
        {
            MyDAL.SaveProductTag(Product_ID, extends);
        }

        public virtual bool EditProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d"))
            {
                return MyDAL.EditProduct(entity, cateArray, tagArray, imgArray, extends);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d错误");
            }
        }

        public virtual bool EditProductInfo(ProductInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d"))
            {
                return MyDAL.EditProductInfo(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d错误");
            }
        }

        public virtual int DelProduct(int cate_id, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f"))
            {
                return MyDAL.DelProduct(cate_id);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f错误");
            }
        }

        public virtual ProductInfo GetProductByID(int cate_id, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetProductByID(cate_id);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual ProductInfo GetProductByCode(string Code, string Site, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetProductByCode(Code, Site);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual ProductInfo GetProductByName(string Name, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetProductByName(Name);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual IList<ProductInfo> GetProducts(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetProducts(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual IList<ProductInfo> GetProductList(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetProductList(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege) 
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae7f5215-a21a-4af2-8d47-3cda2e1e2de8错误");
            }
        }

        public virtual string GetProductImg(int product_id)
        {
            return MyDAL.GetProductImg(product_id);
        }

        public virtual string GetProductCategory(int product_id)
        {
            return MyDAL.GetProductCategory(product_id);
        }

        public virtual IList<ProductTypeExtendInfo> ProductExtendEditor(int ProductType_ID)
        {
            return MyDAL.ProductExtendEditor(ProductType_ID);
        }

        public virtual IList<ProductExtendInfo> ProductExtendValue(int Product_ID) {
            return MyDAL.ProductExtendValue(Product_ID);
        }

        public virtual IList<ProductExtendInfo> GetProductExtends(QueryInfo Query)
        {
            return MyDAL.GetProductExtends(Query);
        }

        public virtual string GetProductTag(int product_id) {
            return MyDAL.GetProductTag(product_id);
        }

        public virtual string GetCateProductID(string Cate_Arry)
        {
            return MyDAL.GetCateProductID(Cate_Arry);
        }

        public virtual string GetTagProductID(string Tag_Id)
        {
            return MyDAL.GetTagProductID(Tag_Id);
        }

        public virtual string GetExtendProductID(int Extend_ID, string Extend_Value)
        {
            return MyDAL.GetExtendProductID(Extend_ID, Extend_Value);
        }
        public virtual bool UpdateProductStock(string code, int amount, int usableamount)
        {
            return MyDAL.UpdateProductStock(code, amount, usableamount);
        }

        public virtual bool UpdateProductStockExcepNostock(int product_id, int amount, int usableamount)
        {
            return MyDAL.UpdateProductStockExcepNostock(product_id, amount, usableamount);
        }

        public virtual bool UpdateProductSaleAmount(int product_id, int amount)
        {
            return MyDAL.UpdateProductSaleAmount(product_id, amount);
        }

        public virtual bool UpdateProductGroupInfo(string Group_Code, int Product_IsListShow, int Product_ID)
        {
            return MyDAL.UpdateProductGroupInfo(Group_Code, Product_IsListShow, Product_ID);
        }

        public virtual bool UpdateProductGroupCode(string NewGroup_Code, string OldGroup_Code)
        {
            return MyDAL.UpdateProductGroupCode(NewGroup_Code, OldGroup_Code);
        }

        public virtual string GetGroupProductID(string Group_Code)
        {
            return MyDAL.GetGroupProductID(Group_Code);
        }


        public IList<ProductExtendInfo> ProductExtendValues(string Product_Ids)
        {
           return MyDAL.ProductExtendValues(Product_Ids);
        }


        public string GetProductExtendValue(int Extend_ID,string Product_Ids)
        {
            return MyDAL.GetProductExtendValue(Extend_ID,Product_Ids);
        }


        public int DelProductExtendByID(int Product_ID)
        {
            return MyDAL.DelProductExtendByID(Product_ID);
        }
    }

    public class ProductWholeSalePrice : IProductWholeSalePrice
    {
        protected DAL.Product.IProductWholeSalePrice MyDAL;
        protected IRBAC RBAC;

        public ProductWholeSalePrice()
        {
            MyDAL = DAL.Product.ProductWholeSalePriceFactory.CreateProductWholeSalePrice();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddProductWholeSalePrice(ProductWholeSalePriceInfo entity)
        {
            return MyDAL.AddProductWholeSalePrice(entity);
        }

        public virtual bool EditProductWholeSalePrice(ProductWholeSalePriceInfo entity)
        {
            return MyDAL.EditProductWholeSalePrice(entity);
        }

        public virtual int DelProductWholeSalePrice(int ID)
        {
            return MyDAL.DelProductWholeSalePrice(ID);
        }

        public virtual ProductWholeSalePriceInfo GetProductWholeSalePriceByID(int ID)
        {
            return MyDAL.GetProductWholeSalePriceByID(ID);
        }

        public virtual IList<ProductWholeSalePriceInfo> GetProductWholeSalePrices(QueryInfo Query)
        {
            return MyDAL.GetProductWholeSalePrices(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public int DelProductWholeSalePriceByProductID(int Product_ID)
        {
            return MyDAL.DelProductWholeSalePriceByProductID(Product_ID);
        }


        public IList<ProductWholeSalePriceInfo> GetProductWholeSalePriceByProductID(int ID)
        {
            return MyDAL.GetProductWholeSalePriceByProductID(ID);
        }
    }
}


