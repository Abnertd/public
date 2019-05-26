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
    public class ProductReviewConfig : IProductReviewConfig
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductReviewConfig()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }


        public virtual bool EditProductReviewConfig(ProductReviewConfigInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_Review_Config WHERE Product_Review_Config_ID = " + entity.Product_Review_Config_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Review_Config_ID"] = entity.Product_Review_Config_ID;
                    DrAdd["Product_Review_Config_ProductCount"] = entity.Product_Review_Config_ProductCount;
                    DrAdd["Product_Review_Config_ListCount"] = entity.Product_Review_Config_ListCount;
                    DrAdd["Product_Review_Config_Power"] = entity.Product_Review_Config_Power;
                    DrAdd["Product_Review_giftcoin"] = entity.Product_Review_giftcoin;
                    DrAdd["Product_Review_Recommendcoin"] = entity.Product_Review_Recommendcoin;
                    DrAdd["Product_Review_Config_NoRecordTip"] = entity.Product_Review_Config_NoRecordTip;
                    DrAdd["Product_Review_Config_VerifyCode_IsOpen"] = entity.Product_Review_Config_VerifyCode_IsOpen;
                    DrAdd["Product_Review_Config_ManagerReply_Show"] = entity.Product_Review_Config_ManagerReply_Show;
                    DrAdd["Product_Review_Config_Show_SuccessTip"] = entity.Product_Review_Config_Show_SuccessTip;
                    DrAdd["Product_Review_Config_IsActive"] = entity.Product_Review_Config_IsActive;
                    DrAdd["Product_Review_Config_Site"] = entity.Product_Review_Config_Site;

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


        public virtual ProductReviewConfigInfo GetProductReviewConfig()
        {
            ProductReviewConfigInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT Top 1 * FROM Product_Review_Config";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductReviewConfigInfo();

                    entity.Product_Review_Config_ID = Tools.NullInt(RdrList["Product_Review_Config_ID"]);
                    entity.Product_Review_Config_ProductCount = Tools.NullInt(RdrList["Product_Review_Config_ProductCount"]);
                    entity.Product_Review_Config_ListCount = Tools.NullInt(RdrList["Product_Review_Config_ListCount"]);
                    entity.Product_Review_Config_Power = Tools.NullInt(RdrList["Product_Review_Config_Power"]);
                    entity.Product_Review_giftcoin = Tools.NullInt(RdrList["Product_Review_giftcoin"]);
                    entity.Product_Review_Recommendcoin = Tools.NullInt(RdrList["Product_Review_Recommendcoin"]);
                    entity.Product_Review_Config_NoRecordTip = Tools.NullStr(RdrList["Product_Review_Config_NoRecordTip"]);
                    entity.Product_Review_Config_VerifyCode_IsOpen = Tools.NullInt(RdrList["Product_Review_Config_VerifyCode_IsOpen"]);
                    entity.Product_Review_Config_ManagerReply_Show = Tools.NullInt(RdrList["Product_Review_Config_ManagerReply_Show"]);
                    entity.Product_Review_Config_Show_SuccessTip = Tools.NullStr(RdrList["Product_Review_Config_Show_SuccessTip"]);
                    entity.Product_Review_Config_IsActive = Tools.NullInt(RdrList["Product_Review_Config_IsActive"]);
                    entity.Product_Review_Config_Site = Tools.NullStr(RdrList["Product_Review_Config_Site"]);

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

        

    }
}
