using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class Product : IProduct
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Product()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM product_basic";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_ID"] = entity.Product_ID;
            DrAdd["Product_Code"] = entity.Product_Code;
            DrAdd["Product_CateID"] = entity.Product_CateID;
            DrAdd["Product_BrandID"] = entity.Product_BrandID;
            DrAdd["Product_TypeID"] = entity.Product_TypeID;
            DrAdd["Product_Name"] = entity.Product_Name;
            DrAdd["Product_NameInitials"] = entity.Product_NameInitials;
            DrAdd["Product_SubName"] = entity.Product_SubName;
            DrAdd["Product_SubNameInitials"] = entity.Product_SubNameInitials;
            DrAdd["Product_SupplierID"] = entity.Product_SupplierID;
            DrAdd["Product_Supplier_CommissionCateID"] = entity.Product_Supplier_CommissionCateID;
            DrAdd["Product_MKTPrice"] = entity.Product_MKTPrice;
            DrAdd["Product_GroupPrice"] = entity.Product_GroupPrice;
            DrAdd["Product_PurchasingPrice"] = entity.Product_PurchasingPrice;
            DrAdd["Product_Price"] = entity.Product_Price;
            DrAdd["Product_PriceUnit"] = entity.Product_PriceUnit;
            DrAdd["Product_Unit"] = entity.Product_Unit;
            DrAdd["Product_GroupNum"] = entity.Product_GroupNum;
            DrAdd["Product_Note"] = entity.Product_Note;
            DrAdd["Product_NoteColor"] = entity.Product_NoteColor;
            DrAdd["Product_Audit_Note"] = entity.Product_Audit_Note;
            DrAdd["Product_Weight"] = entity.Product_Weight;
            DrAdd["Product_Img"] = entity.Product_Img;
            DrAdd["Product_Publisher"] = entity.Product_Publisher;
            DrAdd["Product_StockAmount"] = entity.Product_StockAmount;
            DrAdd["Product_SaleAmount"] = entity.Product_SaleAmount;
            DrAdd["Product_Review_Count"] = entity.Product_Review_Count;
            DrAdd["Product_Review_ValidCount"] = entity.Product_Review_ValidCount;
            DrAdd["Product_Review_Average"] = entity.Product_Review_Average;
            DrAdd["Product_IsInsale"] = entity.Product_IsInsale;
            DrAdd["Product_IsGroupBuy"] = entity.Product_IsGroupBuy;
            DrAdd["Product_IsCoinBuy"] = entity.Product_IsCoinBuy;
            DrAdd["Product_IsFavor"] = entity.Product_IsFavor;
            DrAdd["Product_IsGift"] = entity.Product_IsGift;
            DrAdd["Product_IsAudit"] = entity.Product_IsAudit;
            DrAdd["Product_IsGiftCoin"] = entity.Product_IsGiftCoin;
            DrAdd["Product_Gift_Coin"] = entity.Product_Gift_Coin;
            DrAdd["Product_CoinBuy_Coin"] = entity.Product_CoinBuy_Coin;
            DrAdd["Product_Addtime"] = entity.Product_Addtime;
            DrAdd["Product_Intro"] = entity.Product_Intro;
            DrAdd["Product_AlertAmount"] = entity.Product_AlertAmount;
            DrAdd["Product_UsableAmount"] = entity.Product_UsableAmount;
            DrAdd["Product_IsNoStock"] = entity.Product_IsNoStock;
            DrAdd["Product_Spec"] = entity.Product_Spec;
            DrAdd["Product_Maker"] = entity.Product_Maker;
            DrAdd["Product_Description"] = entity.Product_Description;
            DrAdd["Product_Sort"] = entity.Product_Sort;
            DrAdd["Product_QuotaAmount"] = entity.Product_QuotaAmount;
            DrAdd["Product_IsListShow"] = entity.Product_IsListShow;
            DrAdd["Product_GroupCode"] = entity.Product_GroupCode;
            DrAdd["Product_Hits"] = entity.Product_Hits;
            DrAdd["Product_Site"] = entity.Product_Site;
            DrAdd["Product_SEO_Title"] = entity.Product_SEO_Title;
            DrAdd["Product_SEO_Keyword"] = entity.Product_SEO_Keyword;
            DrAdd["Product_SEO_Description"] = entity.Product_SEO_Description;
            DrAdd["U_Product_Parameters"] = entity.U_Product_Parameters;
            DrAdd["U_Product_SalesByProxy"] = entity.U_Product_SalesByProxy;
            DrAdd["U_Product_Shipper"] = entity.U_Product_Shipper;
            DrAdd["U_Product_DeliveryCycle"] = entity.U_Product_DeliveryCycle;
            DrAdd["U_Product_MinBook"] = entity.U_Product_MinBook;
            DrAdd["Product_PriceType"] = entity.Product_PriceType;
            DrAdd["Product_ManualFee"] = entity.Product_ManualFee;
            DrAdd["Product_LibraryImg"] = entity.Product_LibraryImg;
            DrAdd["Product_State_Name"] = entity.Product_State_Name;
            DrAdd["Product_City_Name"] = entity.Product_City_Name;
            DrAdd["Product_County_Name"] = entity.Product_County_Name;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);

                entity.Product_ID = GetLastProduct(entity.Product_Name, entity.Product_Code);

                //处理类别
                SaveProductCategory(entity.Product_ID, cateArray);

                //处理标签
                //SaveProductTag(entity.Product_ID, tagArray);

                //处理图片
                SaveProductImg(entity.Product_ID, imgArray);

                //处理扩展属性
                SaveProductExtend(entity.Product_ID, extends);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        //保存商品对应类别
        private void SaveProductCategory(int Product_ID, string[] extends)
        {
            ArrayList sqlList = new ArrayList(extends.GetLength(0));
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Category WHERE Product_Category_ProductID =" + Product_ID);
            foreach (string cateid in extends)
            {
                if (cateid != "" && Tools.IsInt(cateid))
                    sqlList.Add("INSERT INTO Product_Category (Product_Category_CateID, Product_Category_ProductID) VALUES (" + cateid + ", " + Product_ID + ")");
            }
            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        //保存商品对应标签
        public virtual void SaveProductTag(int Product_ID, string[] extends)
        {
            ArrayList sqlList = new ArrayList(extends.GetLength(0));
            DBHelper.ExecuteNonQuery("DELETE FROM Product_RelateTag WHERE Product_RelateTag_ProductID =" + Product_ID);
            foreach (string tagid in extends)
            {
                if (tagid != "" && Tools.IsInt(tagid))
                    sqlList.Add("INSERT INTO Product_RelateTag (Product_RelateTag_TagID, Product_RelateTag_ProductID) VALUES (" + tagid + ", " + Product_ID + ")");
            }
            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        //保存商品对应图片
        private void SaveProductImg(int Product_ID, string[] extends) {
            ArrayList sqlList = new ArrayList(extends.GetLength(0));
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Img WHERE Product_Img_ProductID =" + Product_ID);
            foreach (string imgPath in extends)
            {
                if (imgPath != "")
                    sqlList.Add("INSERT INTO Product_Img (Product_Img_ProductID, Product_Img_Path) VALUES (" + Product_ID + ", '" + imgPath + "')");
            }
            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        //保存商品扩展属性
        private void SaveProductExtend(int Product_ID, IList<ProductExtendInfo> extends) {
            ArrayList sqlList = new ArrayList(extends.Count);
            DBHelper.ExecuteNonQuery("DELETE FROM Product_Extend WHERE Product_Extend_ProductID =" + Product_ID);
            foreach (ProductExtendInfo extend in extends) {
                sqlList.Add("INSERT INTO Product_Extend (Product_Extend_ProductID, Product_Extend_ExtendID, Product_Extend_Value, Product_Extend_Img) VALUES (" + Product_ID + ", " + extend.Extent_ID + ",'" + extend.Extend_Value + "','" + extend.Extend_Img + "')");
            }

            DBHelper.ExecuteNonQuery(sqlList);
            sqlList = null;
        }

        public virtual bool EditProduct(ProductInfo entity, string[] cateArray, string[] tagArray, string[] imgArray, IList<ProductExtendInfo> extends)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM product_basic WHERE Product_ID = " + entity.Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Code"] = entity.Product_Code;
                    DrAdd["Product_CateID"] = entity.Product_CateID;
                    DrAdd["Product_BrandID"] = entity.Product_BrandID;
                    DrAdd["Product_Name"] = entity.Product_Name;
                    DrAdd["Product_NameInitials"] = entity.Product_NameInitials;
                    DrAdd["Product_SubName"] = entity.Product_SubName;
                    DrAdd["Product_SubNameInitials"] = entity.Product_SubNameInitials;
                    DrAdd["Product_TypeID"] = entity.Product_TypeID;
                    DrAdd["Product_SupplierID"] = entity.Product_SupplierID;
                    DrAdd["Product_Supplier_CommissionCateID"] = entity.Product_Supplier_CommissionCateID;
                    DrAdd["Product_MKTPrice"] = entity.Product_MKTPrice;
                    DrAdd["Product_GroupPrice"] = entity.Product_GroupPrice;
                    DrAdd["Product_PurchasingPrice"] = entity.Product_PurchasingPrice;
                    DrAdd["Product_Price"] = entity.Product_Price;
                    DrAdd["Product_PriceUnit"] = entity.Product_PriceUnit;
                    DrAdd["Product_Unit"] = entity.Product_Unit;
                    DrAdd["Product_GroupNum"] = entity.Product_GroupNum;
                    DrAdd["Product_Note"] = entity.Product_Note;
                    DrAdd["Product_NoteColor"] = entity.Product_NoteColor;
                    DrAdd["Product_Audit_Note"] = entity.Product_Audit_Note;
                    DrAdd["Product_Weight"] = entity.Product_Weight;
                    DrAdd["Product_Img"] = entity.Product_Img;
                    DrAdd["Product_Publisher"] = entity.Product_Publisher;

                    DrAdd["Product_IsInsale"] = entity.Product_IsInsale;
                    DrAdd["Product_IsGroupBuy"] = entity.Product_IsGroupBuy;
                    DrAdd["Product_IsCoinBuy"] = entity.Product_IsCoinBuy;
                    DrAdd["Product_IsFavor"] = entity.Product_IsFavor;
                    DrAdd["Product_IsGift"] = entity.Product_IsGift;
                    DrAdd["Product_IsAudit"] = entity.Product_IsAudit;
                    DrAdd["Product_IsGiftCoin"] = entity.Product_IsGiftCoin;
                    DrAdd["Product_Gift_Coin"] = entity.Product_Gift_Coin;
                    DrAdd["Product_CoinBuy_Coin"] = entity.Product_CoinBuy_Coin;

                    DrAdd["Product_Intro"] = entity.Product_Intro;
                    DrAdd["Product_AlertAmount"] = entity.Product_AlertAmount;
                    DrAdd["Product_IsNoStock"] = entity.Product_IsNoStock;
                    DrAdd["Product_UsableAmount"] = entity.Product_UsableAmount;
                    DrAdd["Product_StockAmount"] = entity.Product_StockAmount;
                    DrAdd["Product_Spec"] = entity.Product_Spec;
                    DrAdd["Product_Maker"] = entity.Product_Maker;
                    DrAdd["Product_Description"] = entity.Product_Description;
                    DrAdd["Product_Sort"] = entity.Product_Sort;
                    DrAdd["Product_QuotaAmount"] = entity.Product_QuotaAmount;
                    DrAdd["Product_IsListShow"] = entity.Product_IsListShow;
                    DrAdd["Product_GroupCode"] = entity.Product_GroupCode;
                    DrAdd["Product_Hits"] = entity.Product_Hits;
                    DrAdd["Product_Site"] = entity.Product_Site;
                    DrAdd["Product_SEO_Title"] = entity.Product_SEO_Title;
                    DrAdd["Product_SEO_Keyword"] = entity.Product_SEO_Keyword;
                    DrAdd["Product_SEO_Description"] = entity.Product_SEO_Description;
                    DrAdd["U_Product_Parameters"] = entity.U_Product_Parameters;
                    DrAdd["U_Product_SalesByProxy"] = entity.U_Product_SalesByProxy;
                    DrAdd["U_Product_Shipper"] = entity.U_Product_Shipper;
                    DrAdd["U_Product_DeliveryCycle"] = entity.U_Product_DeliveryCycle;
                    DrAdd["U_Product_MinBook"] = entity.U_Product_MinBook;
                    DrAdd["Product_PriceType"] = entity.Product_PriceType;
                    DrAdd["Product_ManualFee"] = entity.Product_ManualFee;
                    DrAdd["Product_LibraryImg"] = entity.Product_LibraryImg;
                    DrAdd["Product_State_Name"] = entity.Product_State_Name;
                    DrAdd["Product_City_Name"] = entity.Product_City_Name;
                    DrAdd["Product_County_Name"] = entity.Product_County_Name;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                    //处理类别
                    SaveProductCategory(entity.Product_ID, cateArray);

                    //处理标签
                    //SaveProductTag(entity.Product_ID, tagArray);

                    //处理图片
                    SaveProductImg(entity.Product_ID, imgArray);

                    //处理扩展属性
                    SaveProductExtend(entity.Product_ID, extends);

                }
                else{
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual bool EditProductInfo(ProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM product_basic WHERE Product_ID = " + entity.Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Code"] = entity.Product_Code;
                    DrAdd["Product_CateID"] = entity.Product_CateID;
                    DrAdd["Product_BrandID"] = entity.Product_BrandID;
                    DrAdd["Product_Name"] = entity.Product_Name;
                    DrAdd["Product_NameInitials"] = entity.Product_NameInitials;
                    DrAdd["Product_SubName"] = entity.Product_SubName;
                    DrAdd["Product_SubNameInitials"] = entity.Product_SubNameInitials;
                    DrAdd["Product_TypeID"] = entity.Product_TypeID;
                    DrAdd["Product_SupplierID"] = entity.Product_SupplierID;
                    DrAdd["Product_Supplier_CommissionCateID"] = entity.Product_Supplier_CommissionCateID;
                    DrAdd["Product_MKTPrice"] = entity.Product_MKTPrice;
                    DrAdd["Product_GroupPrice"] = entity.Product_GroupPrice;
                    DrAdd["Product_PurchasingPrice"] = entity.Product_PurchasingPrice;
                    DrAdd["Product_Price"] = entity.Product_Price;
                    DrAdd["Product_PriceUnit"] = entity.Product_PriceUnit;
                    DrAdd["Product_Unit"] = entity.Product_Unit;
                    DrAdd["Product_GroupNum"] = entity.Product_GroupNum;
                    DrAdd["Product_Note"] = entity.Product_Note;
                    DrAdd["Product_NoteColor"] = entity.Product_NoteColor;
                    DrAdd["Product_Audit_Note"] = entity.Product_Audit_Note;
                    DrAdd["Product_Weight"] = entity.Product_Weight;
                    DrAdd["Product_Img"] = entity.Product_Img;
                    DrAdd["Product_Publisher"] = entity.Product_Publisher;

                    DrAdd["Product_IsInsale"] = entity.Product_IsInsale;
                    DrAdd["Product_IsGroupBuy"] = entity.Product_IsGroupBuy;
                    DrAdd["Product_IsCoinBuy"] = entity.Product_IsCoinBuy;
                    DrAdd["Product_IsFavor"] = entity.Product_IsFavor;
                    DrAdd["Product_IsGift"] = entity.Product_IsGift;
                    DrAdd["Product_IsAudit"] = entity.Product_IsAudit;
                    DrAdd["Product_IsGiftCoin"] = entity.Product_IsGiftCoin;
                    DrAdd["Product_Gift_Coin"] = entity.Product_Gift_Coin;
                    DrAdd["Product_CoinBuy_Coin"] = entity.Product_CoinBuy_Coin;

                    DrAdd["Product_Intro"] = entity.Product_Intro;
                    DrAdd["Product_AlertAmount"] = entity.Product_AlertAmount;
                    DrAdd["Product_IsNoStock"] = entity.Product_IsNoStock;
                    DrAdd["Product_UsableAmount"] = entity.Product_UsableAmount;
                    DrAdd["Product_StockAmount"] = entity.Product_StockAmount;
                    DrAdd["Product_Spec"] = entity.Product_Spec;
                    DrAdd["Product_Maker"] = entity.Product_Maker;
                    DrAdd["Product_Description"] = entity.Product_Description;
                    DrAdd["Product_Sort"] = entity.Product_Sort;
                    DrAdd["Product_QuotaAmount"] = entity.Product_QuotaAmount;
                    DrAdd["Product_IsListShow"] = entity.Product_IsListShow;
                    DrAdd["Product_GroupCode"] = entity.Product_GroupCode;
                    DrAdd["Product_Hits"] = entity.Product_Hits;
                    DrAdd["Product_Site"] = entity.Product_Site;
                    DrAdd["Product_SEO_Title"] = entity.Product_SEO_Title;
                    DrAdd["Product_SEO_Keyword"] = entity.Product_SEO_Keyword;
                    DrAdd["Product_SEO_Description"] = entity.Product_SEO_Description;
                    DrAdd["U_Product_Parameters"] = entity.U_Product_Parameters;
                    DrAdd["U_Product_SalesByProxy"] = entity.U_Product_SalesByProxy;
                    DrAdd["U_Product_Shipper"] = entity.U_Product_Shipper;

                    DrAdd["U_Product_DeliveryCycle"] = entity.U_Product_DeliveryCycle;
                    DrAdd["U_Product_MinBook"] = entity.U_Product_MinBook;
                    DrAdd["Product_PriceType"] = entity.Product_PriceType;
                    DrAdd["Product_ManualFee"] = entity.Product_ManualFee;
                    DrAdd["Product_LibraryImg"] = entity.Product_LibraryImg;
                    DrAdd["Product_State_Name"] = entity.Product_State_Name;
                    DrAdd["Product_City_Name"] = entity.Product_City_Name;
                    DrAdd["Product_County_Name"] = entity.Product_County_Name;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual int DelProduct(int Product_ID) {

            ArrayList SqlDel = new ArrayList(5);
            SqlDel.Add("DELETE FROM Product_Basic WHERE Product_ID = " + Product_ID);
            SqlDel.Add("DELETE FROM Product_Img WHERE Product_Img_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Category WHERE Product_Category_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_RelateTag WHERE Product_RelateTag_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Extend WHERE Product_Extend_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Price WHERE Product_Price_ProcutID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Deny_Reason WHERE Product_Deny_Reason_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_HistoryPrice WHERE History_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Notify WHERE Product_Notify_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_RelateTag WHERE Product_RelateTag_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Product_Review WHERE Product_Review_ProductID =" + Product_ID);
            SqlDel.Add("DELETE FROM Shopping_Ask WHERE Ask_ProductID =" + Product_ID);
            try {
                DBHelper.ExecuteNonQuery(SqlDel);
                return -1;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual ProductInfo GetProductByID(int Product_ID)
        {
            ProductInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM product_basic WHERE Product_ID = " + Product_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductInfo();
                    entity.Product_ID = Tools.NullInt(RdrList["Product_ID"]);
                    entity.Product_Code = Tools.NullStr(RdrList["Product_Code"]);
                    entity.Product_CateID = Tools.NullInt(RdrList["Product_CateID"]);
                    entity.Product_BrandID = Tools.NullInt(RdrList["Product_BrandID"]);
                    entity.Product_TypeID = Tools.NullInt(RdrList["Product_TypeID"]);
                    entity.Product_SupplierID = Tools.NullInt(RdrList["Product_SupplierID"]);
                    entity.Product_Supplier_CommissionCateID = Tools.NullInt(RdrList["Product_Supplier_CommissionCateID"]);
                    entity.Product_Name = Tools.NullStr(RdrList["Product_Name"]);
                    entity.Product_NameInitials = Tools.NullStr(RdrList["Product_NameInitials"]);
                    entity.Product_SubName = Tools.NullStr(RdrList["Product_SubName"]);
                    entity.Product_SubNameInitials = Tools.NullStr(RdrList["Product_SubNameInitials"]);
                    entity.Product_MKTPrice = Tools.NullDbl(RdrList["Product_MKTPrice"]);
                    entity.Product_GroupPrice = Tools.NullDbl(RdrList["Product_GroupPrice"]);
                    entity.Product_PurchasingPrice = Tools.NullDbl(RdrList["Product_PurchasingPrice"]);
                    entity.Product_Price = Tools.NullDbl(RdrList["Product_Price"]);
                    entity.Product_PriceUnit = Tools.NullStr(RdrList["Product_PriceUnit"]);
                    entity.Product_Unit = Tools.NullStr(RdrList["Product_Unit"]);
                    entity.Product_GroupNum = Tools.NullInt(RdrList["Product_GroupNum"]);
                    entity.Product_Note = Tools.NullStr(RdrList["Product_Note"]);
                    entity.Product_NoteColor = Tools.NullStr(RdrList["Product_NoteColor"]);
                    entity.Product_Audit_Note = Tools.NullStr(RdrList["Product_Audit_Note"]);
                    entity.Product_Weight = Tools.NullDbl(RdrList["Product_Weight"]);
                    entity.Product_Img = Tools.NullStr(RdrList["Product_Img"]);
                    entity.Product_Publisher = Tools.NullStr(RdrList["Product_Publisher"]);
                    entity.Product_StockAmount = Tools.NullInt(RdrList["Product_StockAmount"]);
                    entity.Product_SaleAmount = Tools.NullInt(RdrList["Product_SaleAmount"]);
                    entity.Product_Review_Count = Tools.NullInt(RdrList["Product_Review_Count"]);
                    entity.Product_Review_ValidCount = Tools.NullInt(RdrList["Product_Review_ValidCount"]);
                    entity.Product_Review_Average = Tools.NullDbl(RdrList["Product_Review_Average"]);
                    entity.Product_IsInsale = Tools.NullInt(RdrList["Product_IsInsale"]);
                    entity.Product_IsGroupBuy = Tools.NullInt(RdrList["Product_IsGroupBuy"]);
                    entity.Product_IsCoinBuy = Tools.NullInt(RdrList["Product_IsCoinBuy"]);
                    entity.Product_IsFavor = Tools.NullInt(RdrList["Product_IsFavor"]);
                    entity.Product_IsGift = Tools.NullInt(RdrList["Product_IsGift"]);
                    entity.Product_IsAudit = Tools.NullInt(RdrList["Product_IsAudit"]);
                    entity.Product_IsGiftCoin = Tools.NullInt(RdrList["Product_IsGiftCoin"]);
                    entity.Product_Gift_Coin = Tools.NullDbl(RdrList["Product_Gift_Coin"]);
                    entity.Product_CoinBuy_Coin = Tools.NullInt(RdrList["Product_CoinBuy_Coin"]);
                    entity.Product_Addtime = Tools.NullDate(RdrList["Product_Addtime"]);
                    entity.Product_Intro = Tools.NullStr(RdrList["Product_Intro"]);
                    entity.Product_AlertAmount = Tools.NullInt(RdrList["Product_AlertAmount"]);
                    entity.Product_UsableAmount = Tools.NullInt(RdrList["Product_UsableAmount"]);
                    entity.Product_IsNoStock = Tools.NullInt(RdrList["Product_IsNoStock"]);
                    entity.Product_Spec = Tools.NullStr(RdrList["Product_Spec"]);
                    entity.Product_Maker = Tools.NullStr(RdrList["Product_Maker"]);
                    entity.Product_Description = Tools.NullStr(RdrList["Product_Description"]);
                    entity.Product_Sort = Tools.NullInt(RdrList["Product_Sort"]);
                    entity.Product_QuotaAmount = Tools.NullInt(RdrList["Product_QuotaAmount"]);
                    entity.Product_IsListShow = Tools.NullInt(RdrList["Product_IsListShow"]);
                    entity.Product_GroupCode = Tools.NullStr(RdrList["Product_GroupCode"]);
                    entity.Product_Hits = Tools.NullInt(RdrList["Product_Hits"]);
                    entity.Product_Site = Tools.NullStr(RdrList["Product_Site"]);
                    entity.Product_SEO_Title = Tools.NullStr(RdrList["Product_SEO_Title"]);
                    entity.Product_SEO_Keyword = Tools.NullStr(RdrList["Product_SEO_Keyword"]);
                    entity.Product_SEO_Description = Tools.NullStr(RdrList["Product_SEO_Description"]);
                    entity.U_Product_Parameters = Tools.NullStr(RdrList["U_Product_Parameters"]);
                    entity.U_Product_SalesByProxy = Tools.NullInt(RdrList["U_Product_SalesByProxy"]);
                    entity.U_Product_Shipper = Tools.NullInt(RdrList["U_Product_Shipper"]);

                    entity.U_Product_DeliveryCycle = Tools.NullStr(RdrList["U_Product_DeliveryCycle"]);
                    entity.U_Product_MinBook = Tools.NullInt(RdrList["U_Product_MinBook"]);
                    entity.Product_PriceType = Tools.NullInt(RdrList["Product_PriceType"]);
                    entity.Product_ManualFee = Tools.NullDbl(RdrList["Product_ManualFee"]);
                    entity.Product_LibraryImg = Tools.NullStr(RdrList["Product_LibraryImg"]);
                    entity.Product_State_Name = Tools.NullStr(RdrList["Product_State_Name"]);
                    entity.Product_City_Name = Tools.NullStr(RdrList["Product_City_Name"]);
                    entity.Product_County_Name = Tools.NullStr(RdrList["Product_County_Name"]);
                     
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual ProductInfo GetProductByCode(string Code, string Site) {
            ProductInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM product_basic WHERE Product_Code = '" + Code + "' AND Product_Site = '" + Site + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new ProductInfo();
                    entity.Product_ID = Tools.NullInt(RdrList["Product_ID"]);
                    entity.Product_Code = Tools.NullStr(RdrList["Product_Code"]);
                    entity.Product_CateID = Tools.NullInt(RdrList["Product_CateID"]);
                    entity.Product_BrandID = Tools.NullInt(RdrList["Product_BrandID"]);
                    entity.Product_TypeID = Tools.NullInt(RdrList["Product_TypeID"]);
                    entity.Product_Name = Tools.NullStr(RdrList["Product_Name"]);
                    entity.Product_NameInitials = Tools.NullStr(RdrList["Product_NameInitials"]);
                    entity.Product_SubName = Tools.NullStr(RdrList["Product_SubName"]);
                    entity.Product_SubNameInitials = Tools.NullStr(RdrList["Product_SubNameInitials"]);
                    entity.Product_SupplierID = Tools.NullInt(RdrList["Product_SupplierID"]);
                    entity.Product_Supplier_CommissionCateID = Tools.NullInt(RdrList["Product_Supplier_CommissionCateID"]);
                    entity.Product_MKTPrice = Tools.NullDbl(RdrList["Product_MKTPrice"]);
                    entity.Product_GroupPrice = Tools.NullDbl(RdrList["Product_GroupPrice"]);
                    entity.Product_PurchasingPrice = Tools.NullDbl(RdrList["Product_PurchasingPrice"]);
                    entity.Product_Price = Tools.NullDbl(RdrList["Product_Price"]);
                    entity.Product_PriceUnit = Tools.NullStr(RdrList["Product_PriceUnit"]);
                    entity.Product_Unit = Tools.NullStr(RdrList["Product_Unit"]);
                    entity.Product_GroupNum = Tools.NullInt(RdrList["Product_GroupNum"]);
                    entity.Product_Note = Tools.NullStr(RdrList["Product_Note"]);
                    entity.Product_NoteColor = Tools.NullStr(RdrList["Product_NoteColor"]);
                    entity.Product_Audit_Note = Tools.NullStr(RdrList["Product_Audit_Note"]);
                    entity.Product_Weight = Tools.NullDbl(RdrList["Product_Weight"]);
                    entity.Product_Img = Tools.NullStr(RdrList["Product_Img"]);
                    entity.Product_Publisher = Tools.NullStr(RdrList["Product_Publisher"]);
                    entity.Product_StockAmount = Tools.NullInt(RdrList["Product_StockAmount"]);
                    entity.Product_SaleAmount = Tools.NullInt(RdrList["Product_SaleAmount"]);
                    entity.Product_Review_Count = Tools.NullInt(RdrList["Product_Review_Count"]);
                    entity.Product_Review_ValidCount = Tools.NullInt(RdrList["Product_Review_ValidCount"]);
                    entity.Product_Review_Average = Tools.NullDbl(RdrList["Product_Review_Average"]);
                    entity.Product_IsInsale = Tools.NullInt(RdrList["Product_IsInsale"]);
                    entity.Product_IsGroupBuy = Tools.NullInt(RdrList["Product_IsGroupBuy"]);
                    entity.Product_IsCoinBuy = Tools.NullInt(RdrList["Product_IsCoinBuy"]);
                    entity.Product_IsFavor = Tools.NullInt(RdrList["Product_IsFavor"]);
                    entity.Product_IsGift = Tools.NullInt(RdrList["Product_IsGift"]);
                    entity.Product_IsAudit = Tools.NullInt(RdrList["Product_IsAudit"]);
                    entity.Product_IsGiftCoin = Tools.NullInt(RdrList["Product_IsGiftCoin"]);
                    entity.Product_Gift_Coin = Tools.NullDbl(RdrList["Product_Gift_Coin"]);
                    entity.Product_CoinBuy_Coin = Tools.NullInt(RdrList["Product_CoinBuy_Coin"]);
                    entity.Product_Addtime = Tools.NullDate(RdrList["Product_Addtime"]);
                    entity.Product_Intro = Tools.NullStr(RdrList["Product_Intro"]);
                    entity.Product_AlertAmount = Tools.NullInt(RdrList["Product_AlertAmount"]);
                    entity.Product_UsableAmount = Tools.NullInt(RdrList["Product_UsableAmount"]);
                    entity.Product_IsNoStock = Tools.NullInt(RdrList["Product_IsNoStock"]);
                    entity.Product_Spec = Tools.NullStr(RdrList["Product_Spec"]);
                    entity.Product_Maker = Tools.NullStr(RdrList["Product_Maker"]);
                    entity.Product_Description = Tools.NullStr(RdrList["Product_Description"]);
                    entity.Product_Sort = Tools.NullInt(RdrList["Product_Sort"]);
                    entity.Product_QuotaAmount = Tools.NullInt(RdrList["Product_QuotaAmount"]);
                    entity.Product_IsListShow = Tools.NullInt(RdrList["Product_IsListShow"]);
                    entity.Product_GroupCode = Tools.NullStr(RdrList["Product_GroupCode"]);
                    entity.Product_Hits = Tools.NullInt(RdrList["Product_Hits"]);
                    entity.Product_Site = Tools.NullStr(RdrList["Product_Site"]);
                    entity.Product_SEO_Title = Tools.NullStr(RdrList["Product_SEO_Title"]);
                    entity.Product_SEO_Keyword = Tools.NullStr(RdrList["Product_SEO_Keyword"]);
                    entity.Product_SEO_Description = Tools.NullStr(RdrList["Product_SEO_Description"]);
                    entity.U_Product_Parameters = Tools.NullStr(RdrList["U_Product_Parameters"]);
                    entity.U_Product_SalesByProxy = Tools.NullInt(RdrList["U_Product_SalesByProxy"]);
                    entity.U_Product_Shipper = Tools.NullInt(RdrList["U_Product_Shipper"]);

                    entity.U_Product_DeliveryCycle = Tools.NullStr(RdrList["U_Product_DeliveryCycle"]);
                    entity.U_Product_MinBook = Tools.NullInt(RdrList["U_Product_MinBook"]);
                    entity.Product_PriceType = Tools.NullInt(RdrList["Product_PriceType"]);
                    entity.Product_ManualFee = Tools.NullDbl(RdrList["Product_ManualFee"]);
                    entity.Product_LibraryImg = Tools.NullStr(RdrList["Product_LibraryImg"]);
                    entity.Product_State_Name = Tools.NullStr(RdrList["Product_State_Name"]);
                    entity.Product_City_Name = Tools.NullStr(RdrList["Product_City_Name"]);
                    entity.Product_County_Name = Tools.NullStr(RdrList["Product_County_Name"]);

                }

                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual ProductInfo GetProductByName(string Name) {
            ProductInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM product_basic WHERE Product_Name = '" + Name + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new ProductInfo();
                    entity.Product_ID = Tools.NullInt(RdrList["Product_ID"]);
                    entity.Product_Code = Tools.NullStr(RdrList["Product_Code"]);
                    entity.Product_CateID = Tools.NullInt(RdrList["Product_CateID"]);
                    entity.Product_BrandID = Tools.NullInt(RdrList["Product_BrandID"]);
                    entity.Product_TypeID = Tools.NullInt(RdrList["Product_TypeID"]);
                    entity.Product_SupplierID = Tools.NullInt(RdrList["Product_SupplierID"]);
                    entity.Product_Supplier_CommissionCateID = Tools.NullInt(RdrList["Product_Supplier_CommissionCateID"]);
                    entity.Product_Name = Tools.NullStr(RdrList["Product_Name"]);
                    entity.Product_NameInitials = Tools.NullStr(RdrList["Product_NameInitials"]);
                    entity.Product_SubName = Tools.NullStr(RdrList["Product_SubName"]);
                    entity.Product_SubNameInitials = Tools.NullStr(RdrList["Product_SubNameInitials"]);
                    entity.Product_MKTPrice = Tools.NullDbl(RdrList["Product_MKTPrice"]);
                    entity.Product_GroupPrice = Tools.NullDbl(RdrList["Product_GroupPrice"]);
                    entity.Product_PurchasingPrice = Tools.NullDbl(RdrList["Product_PurchasingPrice"]);
                    entity.Product_Price = Tools.NullDbl(RdrList["Product_Price"]);
                    entity.Product_PriceUnit = Tools.NullStr(RdrList["Product_PriceUnit"]);
                    entity.Product_Unit = Tools.NullStr(RdrList["Product_Unit"]);
                    entity.Product_GroupNum = Tools.NullInt(RdrList["Product_GroupNum"]);
                    entity.Product_Note = Tools.NullStr(RdrList["Product_Note"]);
                    entity.Product_NoteColor = Tools.NullStr(RdrList["Product_NoteColor"]);
                    entity.Product_Audit_Note = Tools.NullStr(RdrList["Product_Audit_Note"]);
                    entity.Product_Weight = Tools.NullDbl(RdrList["Product_Weight"]);
                    entity.Product_Img = Tools.NullStr(RdrList["Product_Img"]);
                    entity.Product_Publisher = Tools.NullStr(RdrList["Product_Publisher"]);
                    entity.Product_StockAmount = Tools.NullInt(RdrList["Product_StockAmount"]);
                    entity.Product_SaleAmount = Tools.NullInt(RdrList["Product_SaleAmount"]);
                    entity.Product_Review_Count = Tools.NullInt(RdrList["Product_Review_Count"]);
                    entity.Product_Review_ValidCount = Tools.NullInt(RdrList["Product_Review_ValidCount"]);
                    entity.Product_Review_Average = Tools.NullDbl(RdrList["Product_Review_Average"]);
                    entity.Product_IsInsale = Tools.NullInt(RdrList["Product_IsInsale"]);
                    entity.Product_IsGroupBuy = Tools.NullInt(RdrList["Product_IsGroupBuy"]);
                    entity.Product_IsCoinBuy = Tools.NullInt(RdrList["Product_IsCoinBuy"]);
                    entity.Product_IsFavor = Tools.NullInt(RdrList["Product_IsFavor"]);
                    entity.Product_IsGift = Tools.NullInt(RdrList["Product_IsGift"]);
                    entity.Product_IsAudit = Tools.NullInt(RdrList["Product_IsAudit"]);
                    entity.Product_IsGiftCoin = Tools.NullInt(RdrList["Product_IsGiftCoin"]);
                    entity.Product_Gift_Coin = Tools.NullInt(RdrList["Product_Gift_Coin"]);
                    entity.Product_CoinBuy_Coin = Tools.NullInt(RdrList["Product_CoinBuy_Coin"]);
                    entity.Product_Addtime = Tools.NullDate(RdrList["Product_Addtime"]);
                    entity.Product_Intro = Tools.NullStr(RdrList["Product_Intro"]);
                    entity.Product_AlertAmount = Tools.NullInt(RdrList["Product_AlertAmount"]);
                    entity.Product_UsableAmount = Tools.NullInt(RdrList["Product_UsableAmount"]);
                    entity.Product_IsNoStock = Tools.NullInt(RdrList["Product_IsNoStock"]);
                    entity.Product_Spec = Tools.NullStr(RdrList["Product_Spec"]);
                    entity.Product_Maker = Tools.NullStr(RdrList["Product_Maker"]);
                    entity.Product_Description = Tools.NullStr(RdrList["Product_Description"]);
                    entity.Product_Sort = Tools.NullInt(RdrList["Product_Sort"]);
                    entity.Product_QuotaAmount = Tools.NullInt(RdrList["Product_QuotaAmount"]);
                    entity.Product_IsListShow = Tools.NullInt(RdrList["Product_IsListShow"]);
                    entity.Product_GroupCode = Tools.NullStr(RdrList["Product_GroupCode"]);
                    entity.Product_Hits = Tools.NullInt(RdrList["Product_Hits"]);
                    entity.Product_Site = Tools.NullStr(RdrList["Product_Site"]);
                    entity.Product_SEO_Title = Tools.NullStr(RdrList["Product_SEO_Title"]);
                    entity.Product_SEO_Keyword = Tools.NullStr(RdrList["Product_SEO_Keyword"]);
                    entity.Product_SEO_Description = Tools.NullStr(RdrList["Product_SEO_Description"]);
                    entity.U_Product_Parameters = Tools.NullStr(RdrList["U_Product_Parameters"]);
                    entity.U_Product_SalesByProxy = Tools.NullInt(RdrList["U_Product_SalesByProxy"]);
                    entity.U_Product_Shipper = Tools.NullInt(RdrList["U_Product_Shipper"]);

                    entity.U_Product_DeliveryCycle = Tools.NullStr(RdrList["U_Product_DeliveryCycle"]);
                    entity.U_Product_MinBook = Tools.NullInt(RdrList["U_Product_MinBook"]);
                    entity.Product_PriceType = Tools.NullInt(RdrList["Product_PriceType"]);
                    entity.Product_ManualFee = Tools.NullDbl(RdrList["Product_ManualFee"]);
                    entity.Product_LibraryImg = Tools.NullStr(RdrList["Product_LibraryImg"]);
                    entity.Product_State_Name = Tools.NullStr(RdrList["Product_State_Name"]);
                    entity.Product_City_Name = Tools.NullStr(RdrList["Product_City_Name"]);
                    entity.Product_County_Name = Tools.NullStr(RdrList["Product_County_Name"]);

                }

                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<ProductInfo> GetProducts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductInfo> entitys = null;
            ProductInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "product_basic";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductInfo();
                        entity.Product_ID = Tools.NullInt(RdrList["Product_ID"]);
                        entity.Product_Code = Tools.NullStr(RdrList["Product_Code"]);
                        entity.Product_CateID = Tools.NullInt(RdrList["Product_CateID"]);
                        entity.Product_BrandID = Tools.NullInt(RdrList["Product_BrandID"]);
                        entity.Product_TypeID = Tools.NullInt(RdrList["Product_TypeID"]);
                        entity.Product_SupplierID = Tools.NullInt(RdrList["Product_SupplierID"]);
                        entity.Product_Supplier_CommissionCateID = Tools.NullInt(RdrList["Product_Supplier_CommissionCateID"]);
                        entity.Product_Name = Tools.NullStr(RdrList["Product_Name"]);
                        entity.Product_NameInitials = Tools.NullStr(RdrList["Product_NameInitials"]);
                        entity.Product_SubName = Tools.NullStr(RdrList["Product_SubName"]);
                        entity.Product_SubNameInitials = Tools.NullStr(RdrList["Product_SubNameInitials"]);
                        entity.Product_MKTPrice = Tools.NullDbl(RdrList["Product_MKTPrice"]);
                        entity.Product_GroupPrice = Tools.NullDbl(RdrList["Product_GroupPrice"]);
                        entity.Product_PurchasingPrice = Tools.NullDbl(RdrList["Product_PurchasingPrice"]);
                        entity.Product_Price = Tools.NullDbl(RdrList["Product_Price"]);
                        entity.Product_PriceUnit = Tools.NullStr(RdrList["Product_PriceUnit"]);
                        entity.Product_Unit = Tools.NullStr(RdrList["Product_Unit"]);
                        entity.Product_GroupNum = Tools.NullInt(RdrList["Product_GroupNum"]);
                        entity.Product_Note = Tools.NullStr(RdrList["Product_Note"]);
                        entity.Product_NoteColor = Tools.NullStr(RdrList["Product_NoteColor"]);
                        entity.Product_Audit_Note = Tools.NullStr(RdrList["Product_Audit_Note"]);
                        entity.Product_Weight = Tools.NullDbl(RdrList["Product_Weight"]);
                        entity.Product_Img = Tools.NullStr(RdrList["Product_Img"]);
                        entity.Product_Publisher = Tools.NullStr(RdrList["Product_Publisher"]);
                        entity.Product_StockAmount = Tools.NullInt(RdrList["Product_StockAmount"]);
                        entity.Product_SaleAmount = Tools.NullInt(RdrList["Product_SaleAmount"]);
                        entity.Product_Review_Count = Tools.NullInt(RdrList["Product_Review_Count"]);
                        entity.Product_Review_ValidCount = Tools.NullInt(RdrList["Product_Review_ValidCount"]);
                        entity.Product_Review_Average = Tools.NullDbl(RdrList["Product_Review_Average"]);
                        entity.Product_IsInsale = Tools.NullInt(RdrList["Product_IsInsale"]);
                        entity.Product_IsGroupBuy = Tools.NullInt(RdrList["Product_IsGroupBuy"]);
                        entity.Product_IsCoinBuy = Tools.NullInt(RdrList["Product_IsCoinBuy"]);
                        entity.Product_IsFavor = Tools.NullInt(RdrList["Product_IsFavor"]);
                        entity.Product_IsGift = Tools.NullInt(RdrList["Product_IsGift"]);
                        entity.Product_IsAudit = Tools.NullInt(RdrList["Product_IsAudit"]);
                        entity.Product_IsGiftCoin = Tools.NullInt(RdrList["Product_IsGiftCoin"]);
                        entity.Product_Gift_Coin = Tools.NullDbl(RdrList["Product_Gift_Coin"]);
                        entity.Product_CoinBuy_Coin = Tools.NullInt(RdrList["Product_CoinBuy_Coin"]);
                        entity.Product_Addtime = Tools.NullDate(RdrList["Product_Addtime"]);
                        entity.Product_Intro = Tools.NullStr(RdrList["Product_Intro"]);
                        entity.Product_AlertAmount = Tools.NullInt(RdrList["Product_AlertAmount"]);
                        entity.Product_UsableAmount = Tools.NullInt(RdrList["Product_UsableAmount"]);
                        entity.Product_IsNoStock = Tools.NullInt(RdrList["Product_IsNoStock"]);
                        entity.Product_Spec = Tools.NullStr(RdrList["Product_Spec"]);
                        entity.Product_Maker = Tools.NullStr(RdrList["Product_Maker"]);
                        entity.Product_Description = Tools.NullStr(RdrList["Product_Description"]);
                        entity.Product_Sort = Tools.NullInt(RdrList["Product_Sort"]);
                        entity.Product_QuotaAmount = Tools.NullInt(RdrList["Product_QuotaAmount"]);
                        entity.Product_IsListShow = Tools.NullInt(RdrList["Product_IsListShow"]);
                        entity.Product_GroupCode = Tools.NullStr(RdrList["Product_GroupCode"]);
                        entity.Product_Hits = Tools.NullInt(RdrList["Product_Hits"]);
                        entity.Product_Site = Tools.NullStr(RdrList["Product_Site"]);
                        entity.Product_SEO_Title = Tools.NullStr(RdrList["Product_SEO_Title"]);
                        entity.Product_SEO_Keyword = Tools.NullStr(RdrList["Product_SEO_Keyword"]);
                        entity.Product_SEO_Description = Tools.NullStr(RdrList["Product_SEO_Description"]);
                        entity.U_Product_Parameters = Tools.NullStr(RdrList["U_Product_Parameters"]);
                        entity.U_Product_SalesByProxy = Tools.NullInt(RdrList["U_Product_SalesByProxy"]);
                        entity.U_Product_Shipper = Tools.NullInt(RdrList["U_Product_Shipper"]);

                        entity.U_Product_DeliveryCycle = Tools.NullStr(RdrList["U_Product_DeliveryCycle"]);
                        entity.U_Product_MinBook = Tools.NullInt(RdrList["U_Product_MinBook"]);
                        entity.Product_PriceType = Tools.NullInt(RdrList["Product_PriceType"]);
                        entity.Product_ManualFee = Tools.NullDbl(RdrList["Product_ManualFee"]);
                        entity.Product_LibraryImg = Tools.NullStr(RdrList["Product_LibraryImg"]);
                        entity.Product_State_Name = Tools.NullStr(RdrList["Product_State_Name"]);
                        entity.Product_City_Name = Tools.NullStr(RdrList["Product_City_Name"]);
                        entity.Product_County_Name = Tools.NullStr(RdrList["Product_County_Name"]);


                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<ProductInfo> GetProductList(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductInfo> entitys = null;
            ProductInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "product_basic";
                SqlField = "Product_ID, Product_Code, Product_CateID, Product_BrandID, Product_TypeID,Product_SupplierID,Product_Supplier_CommissionCateID, Product_Name, Product_NameInitials, Product_SubName, Product_SubNameInitials, Product_MKTPrice, Product_GroupPrice, Product_PurchasingPrice,Product_Price, Product_PriceUnit,Product_Unit, Product_GroupNum, Product_Note, Product_NoteColor,Product_Audit_Note, Product_Weight, Product_Img, Product_Publisher, Product_StockAmount, Product_SaleAmount, Product_Review_Count, Product_Review_ValidCount, Product_Review_Average, Product_IsInsale, Product_IsGroupBuy, Product_Description,Product_IsCoinBuy, Product_IsFavor, Product_IsGift,Product_IsGiftCoin,Product_Gift_Coin, Product_CoinBuy_Coin, Product_IsAudit, Product_Addtime,  Product_AlertAmount, Product_UsableAmount, Product_IsNoStock, Product_Spec, Product_Maker, Product_Sort, Product_QuotaAmount,Product_IsListShow,Product_GroupCode, Product_Hits, Product_Site, U_Product_SalesByProxy, U_Product_Shipper,U_Product_DeliveryCycle,U_Product_MinBook,Product_PriceType,Product_ManualFee,Product_LibraryImg,Product_State_Name,Product_City_Name,Product_County_Name";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductInfo();
                        entity.Product_ID = Tools.NullInt(RdrList["Product_ID"]);
                        entity.Product_Code = Tools.NullStr(RdrList["Product_Code"]);
                        entity.Product_CateID = Tools.NullInt(RdrList["Product_CateID"]);
                        entity.Product_BrandID = Tools.NullInt(RdrList["Product_BrandID"]);
                        entity.Product_TypeID = Tools.NullInt(RdrList["Product_TypeID"]);
                        entity.Product_SupplierID = Tools.NullInt(RdrList["Product_SupplierID"]);
                        entity.Product_Supplier_CommissionCateID = Tools.NullInt(RdrList["Product_Supplier_CommissionCateID"]);
                        entity.Product_Name = Tools.NullStr(RdrList["Product_Name"]);
                        entity.Product_NameInitials = Tools.NullStr(RdrList["Product_NameInitials"]);
                        entity.Product_SubName = Tools.NullStr(RdrList["Product_SubName"]);
                        entity.Product_SubNameInitials = Tools.NullStr(RdrList["Product_SubNameInitials"]);
                        entity.Product_MKTPrice = Tools.NullDbl(RdrList["Product_MKTPrice"]);
                        entity.Product_GroupPrice = Tools.NullDbl(RdrList["Product_GroupPrice"]);
                        entity.Product_PurchasingPrice = Tools.NullDbl(RdrList["Product_PurchasingPrice"]);
                        entity.Product_Price = Tools.NullDbl(RdrList["Product_Price"]);
                        entity.Product_PriceUnit = Tools.NullStr(RdrList["Product_PriceUnit"]);
                        entity.Product_Unit = Tools.NullStr(RdrList["Product_Unit"]);
                        entity.Product_GroupNum = Tools.NullInt(RdrList["Product_GroupNum"]);
                        entity.Product_Note = Tools.NullStr(RdrList["Product_Note"]);
                        entity.Product_NoteColor = Tools.NullStr(RdrList["Product_NoteColor"]);
                        entity.Product_Audit_Note = Tools.NullStr(RdrList["Product_Audit_Note"]);
                        entity.Product_Weight = Tools.NullDbl(RdrList["Product_Weight"]);
                        entity.Product_Img = Tools.NullStr(RdrList["Product_Img"]);
                        entity.Product_Publisher = Tools.NullStr(RdrList["Product_Publisher"]);
                        entity.Product_StockAmount = Tools.NullInt(RdrList["Product_StockAmount"]);
                        entity.Product_SaleAmount = Tools.NullInt(RdrList["Product_SaleAmount"]);
                        entity.Product_Review_Count = Tools.NullInt(RdrList["Product_Review_Count"]);
                        entity.Product_Review_ValidCount = Tools.NullInt(RdrList["Product_Review_ValidCount"]);
                        entity.Product_Review_Average = Tools.NullDbl(RdrList["Product_Review_Average"]);
                        entity.Product_IsInsale = Tools.NullInt(RdrList["Product_IsInsale"]);
                        entity.Product_IsGroupBuy = Tools.NullInt(RdrList["Product_IsGroupBuy"]);
                        entity.Product_IsCoinBuy = Tools.NullInt(RdrList["Product_IsCoinBuy"]);
                        entity.Product_IsFavor = Tools.NullInt(RdrList["Product_IsFavor"]);
                        entity.Product_IsGift = Tools.NullInt(RdrList["Product_IsGift"]);
                        entity.Product_IsAudit = Tools.NullInt(RdrList["Product_IsAudit"]);
                        entity.Product_IsGiftCoin = Tools.NullInt(RdrList["Product_IsGiftCoin"]);
                        entity.Product_Gift_Coin = Tools.NullDbl(RdrList["Product_Gift_Coin"]);
                        entity.Product_CoinBuy_Coin = Tools.NullInt(RdrList["Product_CoinBuy_Coin"]);
                        entity.Product_Addtime = Tools.NullDate(RdrList["Product_Addtime"]);
                        entity.Product_AlertAmount = Tools.NullInt(RdrList["Product_AlertAmount"]);
                        entity.Product_UsableAmount = Tools.NullInt(RdrList["Product_UsableAmount"]);
                        entity.Product_IsNoStock = Tools.NullInt(RdrList["Product_IsNoStock"]);
                        entity.Product_Spec = Tools.NullStr(RdrList["Product_Spec"]);
                        entity.Product_Maker = Tools.NullStr(RdrList["Product_Maker"]);
                        entity.Product_Description = Tools.NullStr(RdrList["Product_Description"]);
                        entity.Product_Sort = Tools.NullInt(RdrList["Product_Sort"]);
                        entity.Product_QuotaAmount = Tools.NullInt(RdrList["Product_QuotaAmount"]);
                        entity.Product_IsListShow = Tools.NullInt(RdrList["Product_IsListShow"]);
                        entity.Product_GroupCode = Tools.NullStr(RdrList["Product_GroupCode"]);
                        entity.Product_Hits = Tools.NullInt(RdrList["Product_Hits"]);
                        entity.U_Product_SalesByProxy = Tools.NullInt(RdrList["U_Product_SalesByProxy"]);
                        entity.U_Product_Shipper = Tools.NullInt(RdrList["U_Product_Shipper"]);

                        entity.U_Product_DeliveryCycle = Tools.NullStr(RdrList["U_Product_DeliveryCycle"]);
                        entity.U_Product_MinBook = Tools.NullInt(RdrList["U_Product_MinBook"]);
                        entity.Product_PriceType = Tools.NullInt(RdrList["Product_PriceType"]);
                        entity.Product_ManualFee = Tools.NullDbl(RdrList["Product_ManualFee"]);
                        entity.Product_LibraryImg = Tools.NullStr(RdrList["Product_LibraryImg"]);
                        entity.Product_State_Name = Tools.NullStr(RdrList["Product_State_Name"]);
                        entity.Product_City_Name = Tools.NullStr(RdrList["Product_City_Name"]);
                        entity.Product_County_Name = Tools.NullStr(RdrList["Product_County_Name"]);


                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "product_basic";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual string GetProductImg(int product_id) {
            string SqlList = "", ImgPath = "";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_Img_Path FROM Product_Img WHERE Product_Img_ProductID =" + product_id;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read()) {
                    if (ImgPath.Length > 0) { ImgPath += "," + Tools.NullStr(RdrList[0]); }
                    else { ImgPath = Tools.NullStr(RdrList[0]); }
                }
                
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null; 
                }
            }
            return ImgPath;
        }

        public virtual string GetProductCategory(int product_id) {
            string SqlList = "", strCate = "";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_Category_CateID FROM Product_Category WHERE Product_Category_ProductID =" + product_id;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read()) {
                    if (strCate.Length > 0) { strCate += "," + Tools.NullStr(RdrList[0]); }
                    else { strCate = Tools.NullStr(RdrList[0]); }
                }
                    
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strCate;
        
        }

        private int GetLastProduct(string product_name, string product_code) {
            int Product_ID = 0;
            string SqlList = "SELECT Product_ID FROM Product_Basic WHERE Product_Name = '" + product_name + "' AND Product_Code = '" + product_code + "' ORDER BY Product_ID DESC";
            SqlDataReader RdrList = null;
            try  {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    Product_ID = Tools.NullInt(RdrList[0]);
                }
            }
            catch (Exception ex)  {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
            return Product_ID;
        }

        public virtual IList<ProductTypeExtendInfo> ProductExtendEditor(int ProductType_ID) {
            IList<ProductTypeExtendInfo> entitys = null;
            ProductTypeExtendInfo entity = null;
            string SqlList = null;
            SqlDataReader RdrList = null;
            SqlList = "SELECT * FROM ProductType_Extend WHERE ProductType_Extend_ProductTypeID =" + ProductType_ID + " AND ProductType_Extend_IsActive = 1 ORDER BY ProductType_Extend_Sort";

            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<ProductTypeExtendInfo>();
                    while (RdrList.Read()) {
                        entity = new ProductTypeExtendInfo();
                        entity.ProductType_Extend_ID = Tools.NullInt(RdrList["ProductType_Extend_ID"]);
                        entity.ProductType_Extend_ProductTypeID = Tools.NullInt(RdrList["ProductType_Extend_ProductTypeID"]);
                        entity.ProductType_Extend_Name = Tools.NullStr(RdrList["ProductType_Extend_Name"]);
                        entity.ProductType_Extend_Display = Tools.NullStr(RdrList["ProductType_Extend_Display"]);
                        entity.ProductType_Extend_IsSearch = Tools.NullInt(RdrList["ProductType_Extend_IsSearch"]);
                        entity.ProductType_Extend_Options = Tools.NullInt(RdrList["ProductType_Extend_Options"]);
                        entity.ProductType_Extend_Default = Tools.NullStr(RdrList["ProductType_Extend_Default"]);
                        entity.ProductType_Extend_IsActive = Tools.NullInt(RdrList["ProductType_Extend_IsActive"]);
                        entity.ProductType_Extend_Gather = Tools.NullInt(RdrList["ProductType_Extend_Gather"]);
                        entity.ProductType_Extend_DisplayForm = Tools.NullInt(RdrList["ProductType_Extend_DisplayForm"]);
                        entity.ProductType_Extend_Sort = Tools.NullInt(RdrList["ProductType_Extend_Sort"]);
                        entity.ProductType_Extend_Site = Tools.NullStr(RdrList["ProductType_Extend_Site"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
            }
            catch(Exception ex) {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
            return entitys;
        }

        public virtual IList<ProductExtendInfo> ProductExtendValue(int Product_ID)
        {
            IList<ProductExtendInfo> entitys = null;
            ProductExtendInfo entity = null;
            string SqlList = null;
            SqlDataReader RdrList = null;
            SqlList = "SELECT * FROM Product_Extend WHERE Product_Extend_ProductID =" + Product_ID;

            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<ProductExtendInfo>();
                    while (RdrList.Read()) {
                        entity = new ProductExtendInfo();
                        entity.Product_ID = Tools.NullInt(RdrList["Product_Extend_ProductID"]);
                        entity.Extent_ID = Tools.NullInt(RdrList["Product_Extend_ExtendID"]);
                        entity.Extend_Value = Tools.NullStr(RdrList["Product_Extend_Value"]);
                        entity.Extend_Img = Tools.NullStr(RdrList["Product_Extend_Img"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
            return entitys;
        }

        public virtual IList<ProductExtendInfo> ProductExtendValues(string Product_Ids)
        {
            IList<ProductExtendInfo> entitys = null;
            ProductExtendInfo entity = null;
            string SqlList = null;
            SqlDataReader RdrList = null;
            SqlList = "SELECT * FROM Product_Extend WHERE Product_Extend_ProductID in (" + Product_Ids + ")";

            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductExtendInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductExtendInfo();
                        entity.Product_ID = Tools.NullInt(RdrList["Product_Extend_ProductID"]);
                        entity.Extent_ID = Tools.NullInt(RdrList["Product_Extend_ExtendID"]);
                        entity.Extend_Value = Tools.NullStr(RdrList["Product_Extend_Value"]);
                        entity.Extend_Img = Tools.NullStr(RdrList["Product_Extend_Img"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
            return entitys;
        }

        public virtual string GetProductExtendValue(int Extend_ID,string Product_Ids)
        {
            string Extend_Value = "0";
            string SqlList = null;
            SqlDataReader RdrList = null;
            SqlList = "SELECT Product_Extend_Value FROM Product_Extend WHERE  Product_Extend_ExtendID= " + Extend_ID + " and Product_Extend_ProductID in (" + Product_Ids + ")";

            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        Extend_Value += "," + Tools.NullStr(RdrList["Product_Extend_Value"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
            return Extend_Value;
        }

        public virtual int DelProductExtendByID(int Product_ID)
        {
            string SqlAdd = "DELETE FROM Product_Extend WHERE Product_Extend_ProductID = " + Product_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<ProductExtendInfo> GetProductExtends(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductExtendInfo> entitys = null;
            ProductExtendInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_Extend";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductExtendInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductExtendInfo();
                        entity.Product_ID = Tools.NullInt(RdrList["Product_Extend_ProductID"]);
                        entity.Extent_ID = Tools.NullInt(RdrList["Product_Extend_ExtendID"]);
                        entity.Extend_Value = Tools.NullStr(RdrList["Product_Extend_Value"]);
                        entity.Extend_Img = Tools.NullStr(RdrList["Product_Extend_Img"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual string GetProductTag(int product_id)
        {
            string SqlList = "", strTag = "";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_RelateTag_TagID FROM Product_RelateTag WHERE Product_RelateTag_ProductID =" + product_id;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strTag += "," + Tools.NullStr(RdrList[0]);
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strTag;
        }

        public virtual string GetCateProductID(string Cate_Arry)
        {
            string SqlList = "", strProductID = "0";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_Category_ID,Product_Category_CateID,Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in ("+Cate_Arry+")";
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strProductID += "," + Tools.NullStr(RdrList["Product_Category_ProductID"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strProductID;
        }

        public virtual string GetTagProductID(string Tag_Id)
        {
            string SqlList = "", strProductID = "0";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID in (" + Tag_Id + ")";
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strProductID += "," + Tools.NullStr(RdrList["Product_RelateTag_ProductID"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strProductID;
        }

        public virtual string GetExtendProductID(int Extend_ID,string Extend_Value)
        {
            string SqlList = "", strProductID = "0";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_Extend_ProductID FROM Product_Extend WHERE Product_Extend_ExtendID=" + Extend_ID + " and Product_Extend_Value='" + Extend_Value + "'";
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strProductID += "," + Tools.NullStr(RdrList["Product_Extend_ProductID"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strProductID;
        }

        public virtual bool UpdateProductStock(string code, int amount, int usableamount)
        {
            string SqlUpdate = "UPDATE Product_Basic SET Product_StockAmount = Product_StockAmount + " + amount + ", Product_UsableAmount = Product_UsableAmount + " + usableamount;
            SqlUpdate += " WHERE Product_Code = '" + code + "'";
            try { DBHelper.ExecuteNonQuery(SqlUpdate); return true; }
            catch (Exception ex) { throw ex; }
        }

        public virtual bool UpdateProductStockExcepNostock(int product_id, int amount, int usableamount)
        {
            string SqlUpdate = "UPDATE Product_Basic SET Product_StockAmount = Product_StockAmount + " + amount + ", Product_UsableAmount = Product_UsableAmount + " + usableamount;
            SqlUpdate += " WHERE Product_IsNoStock=0 and Product_id = " + product_id + "";
            try { DBHelper.ExecuteNonQuery(SqlUpdate); return true; }
            catch (Exception ex) { throw ex; }
        }

        public virtual bool UpdateProductSaleAmount(int product_id, int amount)
        {
            string SqlUpdate = "UPDATE Product_Basic SET Product_SaleAmount = Product_SaleAmount + " + amount + "";
            SqlUpdate += " WHERE Product_ID = " + product_id + "";
            try { DBHelper.ExecuteNonQuery(SqlUpdate); return true; }
            catch (Exception ex) { throw ex; }
        }

        public virtual bool UpdateProductGroupInfo(string Group_Code, int Product_IsListShow, int Product_ID)
        {
            string SqlUpdate = "update Product_Basic set Product_IsListShow = " + Product_IsListShow + ",Product_GroupCode ='" + Group_Code + "' where Product_id = " + Product_ID + " ";
            DBHelper.ExecuteNonQuery(SqlUpdate);

            return true;
        }

        public virtual bool UpdateProductGroupCode(string NewGroup_Code, string OldGroup_Code)
        {
            string SqlUpdate = "update Product_Basic set Product_IsListShow=1,Product_GroupCode ='" + NewGroup_Code + "' where Product_GroupCode = '" + OldGroup_Code + "' ";
            DBHelper.ExecuteNonQuery(SqlUpdate);

            return true;
        }

        public virtual string GetGroupProductID(string Group_Code)
        {
            string SqlList = "", strProductID = "0";
            SqlDataReader RdrList = null;

            SqlList = "SELECT Product_id FROM Product_Basic WHERE Product_GroupCode ='" + Group_Code + "'";
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strProductID += "," + Tools.NullStr(RdrList["Product_id"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
            return strProductID;
        }
    }

    public class ProductWholeSalePrice : IProductWholeSalePrice
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductWholeSalePrice()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductWholeSalePrice(ProductWholeSalePriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_WholeSalePrice";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_WholeSalePrice_ID"] = entity.Product_WholeSalePrice_ID;
            DrAdd["Product_WholeSalePrice_ProductID"] = entity.Product_WholeSalePrice_ProductID;
            DrAdd["Product_WholeSalePrice_MinAmount"] = entity.Product_WholeSalePrice_MinAmount;
            DrAdd["Product_WholeSalePrice_MaxAmount"] = entity.Product_WholeSalePrice_MaxAmount;
            DrAdd["Product_WholeSalePrice_Price"] = entity.Product_WholeSalePrice_Price;
            DrAdd["Product_WholeSalePrice_IsActive"] = entity.Product_WholeSalePrice_IsActive;
            DrAdd["Product_WholeSalePrice_Site"] = entity.Product_WholeSalePrice_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditProductWholeSalePrice(ProductWholeSalePriceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_WholeSalePrice WHERE Product_WholeSalePrice_ID = " + entity.Product_WholeSalePrice_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_WholeSalePrice_ID"] = entity.Product_WholeSalePrice_ID;
                    DrAdd["Product_WholeSalePrice_ProductID"] = entity.Product_WholeSalePrice_ProductID;
                    DrAdd["Product_WholeSalePrice_MinAmount"] = entity.Product_WholeSalePrice_MinAmount;
                    DrAdd["Product_WholeSalePrice_MaxAmount"] = entity.Product_WholeSalePrice_MaxAmount;
                    DrAdd["Product_WholeSalePrice_Price"] = entity.Product_WholeSalePrice_Price;
                    DrAdd["Product_WholeSalePrice_IsActive"] = entity.Product_WholeSalePrice_IsActive;
                    DrAdd["Product_WholeSalePrice_Site"] = entity.Product_WholeSalePrice_Site;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelProductWholeSalePrice(int ID)
        {
            string SqlAdd = "DELETE FROM Product_WholeSalePrice WHERE Product_WholeSalePrice_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelProductWholeSalePriceByProductID(int Product_ID)
        {
            string SqlAdd = "DELETE FROM Product_WholeSalePrice WHERE Product_WholeSalePrice_ProductID = " + Product_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductWholeSalePriceInfo GetProductWholeSalePriceByID(int ID)
        {
            ProductWholeSalePriceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_WholeSalePrice WHERE Product_WholeSalePrice_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductWholeSalePriceInfo();

                    entity.Product_WholeSalePrice_ID = Tools.NullInt(RdrList["Product_WholeSalePrice_ID"]);
                    entity.Product_WholeSalePrice_ProductID = Tools.NullInt(RdrList["Product_WholeSalePrice_ProductID"]);
                    entity.Product_WholeSalePrice_MinAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MinAmount"]);
                    entity.Product_WholeSalePrice_MaxAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MaxAmount"]);
                    entity.Product_WholeSalePrice_Price = Tools.NullDbl(RdrList["Product_WholeSalePrice_Price"]);
                    entity.Product_WholeSalePrice_IsActive = Tools.NullInt(RdrList["Product_WholeSalePrice_IsActive"]);
                    entity.Product_WholeSalePrice_Site = Tools.NullStr(RdrList["Product_WholeSalePrice_Site"]);

                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<ProductWholeSalePriceInfo> GetProductWholeSalePriceByProductID(int ID)
        {
            IList<ProductWholeSalePriceInfo> entitys = null;
            ProductWholeSalePriceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_WholeSalePrice WHERE Product_WholeSalePrice_ProductID = " + ID + " order by Product_WholeSalePrice_ID asc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductWholeSalePriceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductWholeSalePriceInfo();

                        entity.Product_WholeSalePrice_ID = Tools.NullInt(RdrList["Product_WholeSalePrice_ID"]);
                        entity.Product_WholeSalePrice_ProductID = Tools.NullInt(RdrList["Product_WholeSalePrice_ProductID"]);
                        entity.Product_WholeSalePrice_MinAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MinAmount"]);
                        entity.Product_WholeSalePrice_MaxAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MaxAmount"]);
                        entity.Product_WholeSalePrice_Price = Tools.NullDbl(RdrList["Product_WholeSalePrice_Price"]);
                        entity.Product_WholeSalePrice_IsActive = Tools.NullInt(RdrList["Product_WholeSalePrice_IsActive"]);
                        entity.Product_WholeSalePrice_Site = Tools.NullStr(RdrList["Product_WholeSalePrice_Site"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<ProductWholeSalePriceInfo> GetProductWholeSalePrices(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductWholeSalePriceInfo> entitys = null;
            ProductWholeSalePriceInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_WholeSalePrice";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductWholeSalePriceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductWholeSalePriceInfo();
                        entity.Product_WholeSalePrice_ID = Tools.NullInt(RdrList["Product_WholeSalePrice_ID"]);
                        entity.Product_WholeSalePrice_ProductID = Tools.NullInt(RdrList["Product_WholeSalePrice_ProductID"]);
                        entity.Product_WholeSalePrice_MinAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MinAmount"]);
                        entity.Product_WholeSalePrice_MaxAmount = Tools.NullInt(RdrList["Product_WholeSalePrice_MaxAmount"]);
                        entity.Product_WholeSalePrice_Price = Tools.NullDbl(RdrList["Product_WholeSalePrice_Price"]);
                        entity.Product_WholeSalePrice_IsActive = Tools.NullInt(RdrList["Product_WholeSalePrice_IsActive"]);
                        entity.Product_WholeSalePrice_Site = Tools.NullStr(RdrList["Product_WholeSalePrice_Site"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Product_WholeSalePrice";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_WholeSalePrice_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
