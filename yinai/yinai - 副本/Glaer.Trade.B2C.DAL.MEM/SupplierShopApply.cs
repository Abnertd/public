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
    public class SupplierShopApply : ISupplierShopApply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopApply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopApply(SupplierShopApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Apply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Apply_ID"] = entity.Shop_Apply_ID;
            DrAdd["Shop_Apply_SupplierID"] = entity.Shop_Apply_SupplierID;
            DrAdd["Shop_Apply_ShopType"] = entity.Shop_Apply_ShopType;
            DrAdd["Shop_Apply_Name"] = entity.Shop_Apply_Name;
            DrAdd["Shop_Apply_PINCode"] = entity.Shop_Apply_PINCode;
            DrAdd["Shop_Apply_Mobile"] = entity.Shop_Apply_Mobile;
            DrAdd["Shop_Apply_ShopName"] = entity.Shop_Apply_ShopName;
            DrAdd["Shop_Apply_CompanyType"] = entity.Shop_Apply_CompanyType;
            DrAdd["Shop_Apply_Lawman"] = entity.Shop_Apply_Lawman;
            DrAdd["Shop_Apply_CertCode"] = entity.Shop_Apply_CertCode;
            DrAdd["Shop_Apply_CertAddress"] = entity.Shop_Apply_CertAddress;
            DrAdd["Shop_Apply_CompanyAddress"] = entity.Shop_Apply_CompanyAddress;
            DrAdd["Shop_Apply_CompanyPhone"] = entity.Shop_Apply_CompanyPhone;
            DrAdd["Shop_Apply_Certification1"] = entity.Shop_Apply_Certification1;
            DrAdd["Shop_Apply_Certification2"] = entity.Shop_Apply_Certification2;
            DrAdd["Shop_Apply_Certification3"] = entity.Shop_Apply_Certification3;
            DrAdd["Shop_Apply_Certification4"] = entity.Shop_Apply_Certification4;
            DrAdd["Shop_Apply_Certification5"] = entity.Shop_Apply_Certification5;
            DrAdd["Shop_Apply_MainBrand"] = entity.Shop_Apply_MainBrand;
            DrAdd["Shop_Apply_Status"] = entity.Shop_Apply_Status;
            DrAdd["Shop_Apply_Note"] = entity.Shop_Apply_Note;
            DrAdd["Shop_Apply_Addtime"] = entity.Shop_Apply_Addtime;

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

        public virtual bool EditSupplierShopApply(SupplierShopApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Apply WHERE Shop_Apply_ID = " + entity.Shop_Apply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Apply_ID"] = entity.Shop_Apply_ID;
                    DrAdd["Shop_Apply_SupplierID"] = entity.Shop_Apply_SupplierID;
                    DrAdd["Shop_Apply_ShopType"] = entity.Shop_Apply_ShopType;
                    DrAdd["Shop_Apply_Name"] = entity.Shop_Apply_Name;
                    DrAdd["Shop_Apply_PINCode"] = entity.Shop_Apply_PINCode;
                    DrAdd["Shop_Apply_Mobile"] = entity.Shop_Apply_Mobile;
                    DrAdd["Shop_Apply_ShopName"] = entity.Shop_Apply_ShopName;
                    DrAdd["Shop_Apply_CompanyType"] = entity.Shop_Apply_CompanyType;
                    DrAdd["Shop_Apply_Lawman"] = entity.Shop_Apply_Lawman;
                    DrAdd["Shop_Apply_CertCode"] = entity.Shop_Apply_CertCode;
                    DrAdd["Shop_Apply_CertAddress"] = entity.Shop_Apply_CertAddress;
                    DrAdd["Shop_Apply_CompanyAddress"] = entity.Shop_Apply_CompanyAddress;
                    DrAdd["Shop_Apply_CompanyPhone"] = entity.Shop_Apply_CompanyPhone;
                    DrAdd["Shop_Apply_Certification1"] = entity.Shop_Apply_Certification1;
                    DrAdd["Shop_Apply_Certification2"] = entity.Shop_Apply_Certification2;
                    DrAdd["Shop_Apply_Certification3"] = entity.Shop_Apply_Certification3;
                    DrAdd["Shop_Apply_Certification4"] = entity.Shop_Apply_Certification4;
                    DrAdd["Shop_Apply_Certification5"] = entity.Shop_Apply_Certification5;
                    DrAdd["Shop_Apply_MainBrand"] = entity.Shop_Apply_MainBrand;
                    DrAdd["Shop_Apply_Status"] = entity.Shop_Apply_Status;
                    DrAdd["Shop_Apply_Note"] = entity.Shop_Apply_Note;
                    DrAdd["Shop_Apply_Addtime"] = entity.Shop_Apply_Addtime;

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

        public virtual int DelSupplierShopApply(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Apply WHERE Shop_Apply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopApplyInfo GetSupplierShopApplyByID(int ID)
        {
            SupplierShopApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Apply WHERE Shop_Apply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopApplyInfo();

                    entity.Shop_Apply_ID = Tools.NullInt(RdrList["Shop_Apply_ID"]);
                    entity.Shop_Apply_SupplierID = Tools.NullInt(RdrList["Shop_Apply_SupplierID"]);
                    entity.Shop_Apply_ShopType = Tools.NullInt(RdrList["Shop_Apply_ShopType"]);
                    entity.Shop_Apply_Name = Tools.NullStr(RdrList["Shop_Apply_Name"]);
                    entity.Shop_Apply_PINCode = Tools.NullStr(RdrList["Shop_Apply_PINCode"]);
                    entity.Shop_Apply_Mobile = Tools.NullStr(RdrList["Shop_Apply_Mobile"]);
                    entity.Shop_Apply_ShopName = Tools.NullStr(RdrList["Shop_Apply_ShopName"]);
                    entity.Shop_Apply_CompanyType = Tools.NullStr(RdrList["Shop_Apply_CompanyType"]);
                    entity.Shop_Apply_Lawman = Tools.NullStr(RdrList["Shop_Apply_Lawman"]);
                    entity.Shop_Apply_CertCode = Tools.NullStr(RdrList["Shop_Apply_CertCode"]);
                    entity.Shop_Apply_CertAddress = Tools.NullStr(RdrList["Shop_Apply_CertAddress"]);
                    entity.Shop_Apply_CompanyAddress = Tools.NullStr(RdrList["Shop_Apply_CompanyAddress"]);
                    entity.Shop_Apply_CompanyPhone = Tools.NullStr(RdrList["Shop_Apply_CompanyPhone"]);
                    entity.Shop_Apply_Certification1 = Tools.NullStr(RdrList["Shop_Apply_Certification1"]);
                    entity.Shop_Apply_Certification2 = Tools.NullStr(RdrList["Shop_Apply_Certification2"]);
                    entity.Shop_Apply_Certification3 = Tools.NullStr(RdrList["Shop_Apply_Certification3"]);
                    entity.Shop_Apply_Certification4 = Tools.NullStr(RdrList["Shop_Apply_Certification4"]);
                    entity.Shop_Apply_Certification5 = Tools.NullStr(RdrList["Shop_Apply_Certification5"]);
                    entity.Shop_Apply_MainBrand = Tools.NullStr(RdrList["Shop_Apply_MainBrand"]);
                    entity.Shop_Apply_Status = Tools.NullInt(RdrList["Shop_Apply_Status"]);
                    entity.Shop_Apply_Note = Tools.NullStr(RdrList["Shop_Apply_Note"]);
                    entity.Shop_Apply_Addtime = Tools.NullDate(RdrList["Shop_Apply_Addtime"]);

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

        public virtual SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID)
        {
            SupplierShopApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Apply WHERE Shop_Apply_SupplierID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopApplyInfo();

                    entity.Shop_Apply_ID = Tools.NullInt(RdrList["Shop_Apply_ID"]);
                    entity.Shop_Apply_SupplierID = Tools.NullInt(RdrList["Shop_Apply_SupplierID"]);
                    entity.Shop_Apply_ShopType = Tools.NullInt(RdrList["Shop_Apply_ShopType"]);
                    entity.Shop_Apply_Name = Tools.NullStr(RdrList["Shop_Apply_Name"]);
                    entity.Shop_Apply_PINCode = Tools.NullStr(RdrList["Shop_Apply_PINCode"]);
                    entity.Shop_Apply_Mobile = Tools.NullStr(RdrList["Shop_Apply_Mobile"]);
                    entity.Shop_Apply_ShopName = Tools.NullStr(RdrList["Shop_Apply_ShopName"]);
                    entity.Shop_Apply_CompanyType = Tools.NullStr(RdrList["Shop_Apply_CompanyType"]);
                    entity.Shop_Apply_Lawman = Tools.NullStr(RdrList["Shop_Apply_Lawman"]);
                    entity.Shop_Apply_CertCode = Tools.NullStr(RdrList["Shop_Apply_CertCode"]);
                    entity.Shop_Apply_CertAddress = Tools.NullStr(RdrList["Shop_Apply_CertAddress"]);
                    entity.Shop_Apply_CompanyAddress = Tools.NullStr(RdrList["Shop_Apply_CompanyAddress"]);
                    entity.Shop_Apply_CompanyPhone = Tools.NullStr(RdrList["Shop_Apply_CompanyPhone"]);
                    entity.Shop_Apply_Certification1 = Tools.NullStr(RdrList["Shop_Apply_Certification1"]);
                    entity.Shop_Apply_Certification2 = Tools.NullStr(RdrList["Shop_Apply_Certification2"]);
                    entity.Shop_Apply_Certification3 = Tools.NullStr(RdrList["Shop_Apply_Certification3"]);
                    entity.Shop_Apply_Certification4 = Tools.NullStr(RdrList["Shop_Apply_Certification4"]);
                    entity.Shop_Apply_Certification5 = Tools.NullStr(RdrList["Shop_Apply_Certification5"]);
                    entity.Shop_Apply_MainBrand = Tools.NullStr(RdrList["Shop_Apply_MainBrand"]);
                    entity.Shop_Apply_Status = Tools.NullInt(RdrList["Shop_Apply_Status"]);
                    entity.Shop_Apply_Note = Tools.NullStr(RdrList["Shop_Apply_Note"]);
                    entity.Shop_Apply_Addtime = Tools.NullDate(RdrList["Shop_Apply_Addtime"]);

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

        public virtual IList<SupplierShopApplyInfo> GetSupplierShopApplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopApplyInfo> entitys = null;
            SupplierShopApplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Apply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopApplyInfo();
                        entity.Shop_Apply_ID = Tools.NullInt(RdrList["Shop_Apply_ID"]);
                        entity.Shop_Apply_SupplierID = Tools.NullInt(RdrList["Shop_Apply_SupplierID"]);
                        entity.Shop_Apply_ShopType = Tools.NullInt(RdrList["Shop_Apply_ShopType"]);
                        entity.Shop_Apply_Name = Tools.NullStr(RdrList["Shop_Apply_Name"]);
                        entity.Shop_Apply_PINCode = Tools.NullStr(RdrList["Shop_Apply_PINCode"]);
                        entity.Shop_Apply_Mobile = Tools.NullStr(RdrList["Shop_Apply_Mobile"]);
                        entity.Shop_Apply_ShopName = Tools.NullStr(RdrList["Shop_Apply_ShopName"]);
                        entity.Shop_Apply_CompanyType = Tools.NullStr(RdrList["Shop_Apply_CompanyType"]);
                        entity.Shop_Apply_Lawman = Tools.NullStr(RdrList["Shop_Apply_Lawman"]);
                        entity.Shop_Apply_CertCode = Tools.NullStr(RdrList["Shop_Apply_CertCode"]);
                        entity.Shop_Apply_CertAddress = Tools.NullStr(RdrList["Shop_Apply_CertAddress"]);
                        entity.Shop_Apply_CompanyAddress = Tools.NullStr(RdrList["Shop_Apply_CompanyAddress"]);
                        entity.Shop_Apply_CompanyPhone = Tools.NullStr(RdrList["Shop_Apply_CompanyPhone"]);
                        entity.Shop_Apply_Certification1 = Tools.NullStr(RdrList["Shop_Apply_Certification1"]);
                        entity.Shop_Apply_Certification2 = Tools.NullStr(RdrList["Shop_Apply_Certification2"]);
                        entity.Shop_Apply_Certification3 = Tools.NullStr(RdrList["Shop_Apply_Certification3"]);
                        entity.Shop_Apply_Certification4 = Tools.NullStr(RdrList["Shop_Apply_Certification4"]);
                        entity.Shop_Apply_Certification5 = Tools.NullStr(RdrList["Shop_Apply_Certification5"]);
                        entity.Shop_Apply_MainBrand = Tools.NullStr(RdrList["Shop_Apply_MainBrand"]);
                        entity.Shop_Apply_Status = Tools.NullInt(RdrList["Shop_Apply_Status"]);
                        entity.Shop_Apply_Note = Tools.NullStr(RdrList["Shop_Apply_Note"]);
                        entity.Shop_Apply_Addtime = Tools.NullDate(RdrList["Shop_Apply_Addtime"]);

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
                SqlTable = "Supplier_Shop_Apply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Apply_ID) FROM " + SqlTable + SqlParam;

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
