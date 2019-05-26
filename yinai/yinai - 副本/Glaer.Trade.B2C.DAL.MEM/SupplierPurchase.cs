using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using System.Data;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System.Data.SqlClient;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierPurchase : ISupplierPurchase
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPurchase()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPurchase(SupplierPurchaseInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Purchase";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Purchase_ID"] = entity.Purchase_ID;
            DrAdd["Purchase_TypeID"] = entity.Purchase_TypeID;
            DrAdd["Purchase_SupplierID"] = entity.Purchase_SupplierID;
            DrAdd["Purchase_Title"] = entity.Purchase_Title;
            DrAdd["Purchase_DeliveryTime"] = entity.Purchase_DeliveryTime;
            DrAdd["Purchase_State"] = entity.Purchase_State;
            DrAdd["Purchase_City"] = entity.Purchase_City;
            DrAdd["Purchase_County"] = entity.Purchase_County;
            DrAdd["Purchase_Address"] = entity.Purchase_Address;
            DrAdd["Purchase_Intro"] = entity.Purchase_Intro;
            DrAdd["Purchase_Addtime"] = entity.Purchase_Addtime;
            DrAdd["Purchase_Status"] = entity.Purchase_Status;
            DrAdd["Purchase_IsActive"] = entity.Purchase_IsActive;
            DrAdd["Purchase_ActiveReason"] = entity.Purchase_ActiveReason;
            DrAdd["Purchase_Trash"] = entity.Purchase_Trash;
            DrAdd["Purchase_ValidDate"] = entity.Purchase_ValidDate;
            DrAdd["Purchase_Attachment"] = entity.Purchase_Attachment;
            DrAdd["Purchase_Site"] = entity.Purchase_Site;
            DrAdd["Purchase_IsRecommend"] = entity.Purchase_IsRecommend;
            DrAdd["Purchase_IsPublic"] = entity.Purchase_IsPublic;
            DrAdd["Purchase_CateID"] = entity.Purchase_CateID;
            DrAdd["Purchase_SysUserID"] = entity.Purchase_SysUserID;

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

        public virtual bool EditSupplierPurchase(SupplierPurchaseInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Purchase WHERE Purchase_ID = " + entity.Purchase_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Purchase_ID"] = entity.Purchase_ID;
                    DrAdd["Purchase_TypeID"] = entity.Purchase_TypeID;
                    DrAdd["Purchase_SupplierID"] = entity.Purchase_SupplierID;
                    DrAdd["Purchase_Title"] = entity.Purchase_Title;
                    DrAdd["Purchase_DeliveryTime"] = entity.Purchase_DeliveryTime;
                    DrAdd["Purchase_State"] = entity.Purchase_State;
                    DrAdd["Purchase_City"] = entity.Purchase_City;
                    DrAdd["Purchase_County"] = entity.Purchase_County;
                    DrAdd["Purchase_Address"] = entity.Purchase_Address;
                    DrAdd["Purchase_Intro"] = entity.Purchase_Intro;
                    DrAdd["Purchase_Addtime"] = entity.Purchase_Addtime;
                    DrAdd["Purchase_Status"] = entity.Purchase_Status;
                    DrAdd["Purchase_IsActive"] = entity.Purchase_IsActive;
                    DrAdd["Purchase_ActiveReason"] = entity.Purchase_ActiveReason;
                    DrAdd["Purchase_Trash"] = entity.Purchase_Trash;
                    DrAdd["Purchase_ValidDate"] = entity.Purchase_ValidDate;
                    DrAdd["Purchase_Attachment"] = entity.Purchase_Attachment;
                    DrAdd["Purchase_Site"] = entity.Purchase_Site;
                    DrAdd["Purchase_IsRecommend"] = entity.Purchase_IsRecommend;
                    DrAdd["Purchase_IsPublic"] = entity.Purchase_IsPublic;
                    DrAdd["Purchase_CateID"] = entity.Purchase_CateID;
                    DrAdd["Purchase_SysUserID"] = entity.Purchase_SysUserID;
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

        public virtual int DelSupplierPurchase(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Purchase WHERE Purchase_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPurchaseInfo GetSupplierPurchaseByID(int ID)
        {
            SupplierPurchaseInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Purchase WHERE Purchase_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPurchaseInfo();

                    entity.Purchase_ID = Tools.NullInt(RdrList["Purchase_ID"]);
                    entity.Purchase_TypeID = Tools.NullInt(RdrList["Purchase_TypeID"]);
                    entity.Purchase_SupplierID = Tools.NullInt(RdrList["Purchase_SupplierID"]);
                    entity.Purchase_Title = Tools.NullStr(RdrList["Purchase_Title"]);
                    entity.Purchase_DeliveryTime = Tools.NullDate(RdrList["Purchase_DeliveryTime"]);
                    entity.Purchase_State = Tools.NullStr(RdrList["Purchase_State"]);
                    entity.Purchase_City = Tools.NullStr(RdrList["Purchase_City"]);
                    entity.Purchase_County = Tools.NullStr(RdrList["Purchase_County"]);
                    entity.Purchase_Address = Tools.NullStr(RdrList["Purchase_Address"]);
                    entity.Purchase_Intro = Tools.NullStr(RdrList["Purchase_Intro"]);
                    entity.Purchase_Addtime = Tools.NullDate(RdrList["Purchase_Addtime"]);
                    entity.Purchase_Status = Tools.NullInt(RdrList["Purchase_Status"]);
                    entity.Purchase_IsActive = Tools.NullInt(RdrList["Purchase_IsActive"]);
                    entity.Purchase_ActiveReason = Tools.NullStr(RdrList["Purchase_ActiveReason"]);
                    entity.Purchase_Trash = Tools.NullInt(RdrList["Purchase_Trash"]);
                    entity.Purchase_ValidDate = Tools.NullDate(RdrList["Purchase_ValidDate"]);
                    entity.Purchase_Attachment = Tools.NullStr(RdrList["Purchase_Attachment"]);
                    entity.Purchase_Site = Tools.NullStr(RdrList["Purchase_Site"]);
                    entity.Purchase_IsRecommend = Tools.NullInt(RdrList["Purchase_IsRecommend"]);
                    entity.Purchase_IsPublic = Tools.NullInt(RdrList["Purchase_IsPublic"]);
                    entity.Purchase_CateID = Tools.NullInt(RdrList["Purchase_CateID"]);
                    entity.Purchase_SysUserID = Tools.NullInt(RdrList["Purchase_SysUserID"]);
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

         
        public virtual IList<SupplierPurchaseInfo> GetSupplierPurchasesList(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPurchaseInfo> entitys = null;
            SupplierPurchaseInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Purchase";
                SqlField = "Purchase_ID,Purchase_TypeID,Purchase_SupplierID,Purchase_Title,Purchase_DeliveryTime,Purchase_State,Purchase_City,Purchase_County,Purchase_Address";
                SqlField += ",Purchase_Addtime,Purchase_Status,Purchase_IsActive,Purchase_ActiveReason,Purchase_Trash,Purchase_ValidDate,Purchase_Attachment,Purchase_Site,Purchase_IsRecommend,Purchase_IsPublic,Purchase_CateID,Purchase_SysUserID";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseInfo();
                        entity.Purchase_ID = Tools.NullInt(RdrList["Purchase_ID"]);
                        entity.Purchase_TypeID = Tools.NullInt(RdrList["Purchase_TypeID"]);
                        entity.Purchase_SupplierID = Tools.NullInt(RdrList["Purchase_SupplierID"]);
                        entity.Purchase_Title = Tools.NullStr(RdrList["Purchase_Title"]);
                        entity.Purchase_DeliveryTime = Tools.NullDate(RdrList["Purchase_DeliveryTime"]);
                        entity.Purchase_State = Tools.NullStr(RdrList["Purchase_State"]);
                        entity.Purchase_City = Tools.NullStr(RdrList["Purchase_City"]);
                        entity.Purchase_County = Tools.NullStr(RdrList["Purchase_County"]);
                        entity.Purchase_Address = Tools.NullStr(RdrList["Purchase_Address"]);
                        entity.Purchase_Addtime = Tools.NullDate(RdrList["Purchase_Addtime"]);
                        entity.Purchase_Status = Tools.NullInt(RdrList["Purchase_Status"]);
                        entity.Purchase_IsActive = Tools.NullInt(RdrList["Purchase_IsActive"]);
                        entity.Purchase_ActiveReason = Tools.NullStr(RdrList["Purchase_ActiveReason"]);
                        entity.Purchase_Trash = Tools.NullInt(RdrList["Purchase_Trash"]);
                        entity.Purchase_ValidDate = Tools.NullDate(RdrList["Purchase_ValidDate"]);
                        entity.Purchase_Attachment = Tools.NullStr(RdrList["Purchase_Attachment"]);
                        entity.Purchase_Site = Tools.NullStr(RdrList["Purchase_Site"]);
                        entity.Purchase_IsRecommend = Tools.NullInt(RdrList["Purchase_IsRecommend"]);
                        entity.Purchase_IsPublic = Tools.NullInt(RdrList["Purchase_IsPublic"]);
                        entity.Purchase_CateID = Tools.NullInt(RdrList["Purchase_CateID"]);
                        entity.Purchase_SysUserID = Tools.NullInt(RdrList["Purchase_SysUserID"]);
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

        public virtual IList<SupplierPurchaseInfo> GetSupplierPurchases(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPurchaseInfo> entitys = null;
            SupplierPurchaseInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Purchase";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseInfo();
                        entity.Purchase_ID = Tools.NullInt(RdrList["Purchase_ID"]);
                        entity.Purchase_TypeID = Tools.NullInt(RdrList["Purchase_TypeID"]);
                        entity.Purchase_SupplierID = Tools.NullInt(RdrList["Purchase_SupplierID"]);
                        entity.Purchase_Title = Tools.NullStr(RdrList["Purchase_Title"]);
                        entity.Purchase_DeliveryTime = Tools.NullDate(RdrList["Purchase_DeliveryTime"]);
                        entity.Purchase_State = Tools.NullStr(RdrList["Purchase_State"]);
                        entity.Purchase_City = Tools.NullStr(RdrList["Purchase_City"]);
                        entity.Purchase_County = Tools.NullStr(RdrList["Purchase_County"]);
                        entity.Purchase_Address = Tools.NullStr(RdrList["Purchase_Address"]);
                        entity.Purchase_Intro = Tools.NullStr(RdrList["Purchase_Intro"]);
                        entity.Purchase_Addtime = Tools.NullDate(RdrList["Purchase_Addtime"]);
                        entity.Purchase_Status = Tools.NullInt(RdrList["Purchase_Status"]);
                        entity.Purchase_IsActive = Tools.NullInt(RdrList["Purchase_IsActive"]);
                        entity.Purchase_ActiveReason = Tools.NullStr(RdrList["Purchase_ActiveReason"]);
                        entity.Purchase_Trash = Tools.NullInt(RdrList["Purchase_Trash"]);
                        entity.Purchase_ValidDate = Tools.NullDate(RdrList["Purchase_ValidDate"]);
                        entity.Purchase_Attachment = Tools.NullStr(RdrList["Purchase_Attachment"]);
                        entity.Purchase_Site = Tools.NullStr(RdrList["Purchase_Site"]);
                        entity.Purchase_IsRecommend = Tools.NullInt(RdrList["Purchase_IsRecommend"]);
                        entity.Purchase_IsPublic = Tools.NullInt(RdrList["Purchase_IsPublic"]);
                        entity.Purchase_CateID = Tools.NullInt(RdrList["Purchase_CateID"]);
                        entity.Purchase_SysUserID = Tools.NullInt(RdrList["Purchase_SysUserID"]);
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
                SqlTable = "Supplier_Purchase";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Purchase_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddSupplierPurchasePrivate(SupplierPurchasePrivateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Purchase_Private";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Purchase_Private_ID"] = entity.Purchase_Private_ID;
            DrAdd["Purchase_Private_SupplierID"] = entity.Purchase_Private_SupplierID;
            DrAdd["Purchase_Private_PurchaseID"] = entity.Purchase_Private_PurchaseID;

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

        public virtual int DelSupplierPurchasePrivateByPurchase(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Purchase_Private WHERE Purchase_Private_PurchaseID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierPurchasePrivateInfo> GetSupplierPurchasePrivatesByPurchase(int ID)
        {
            IList<SupplierPurchasePrivateInfo> entitys = null;
            SupplierPurchasePrivateInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Supplier_Purchase_Private where Purchase_Private_PurchaseID=" + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchasePrivateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchasePrivateInfo();
                        entity.Purchase_Private_ID = Tools.NullInt(RdrList["Purchase_Private_ID"]);
                        entity.Purchase_Private_SupplierID = Tools.NullInt(RdrList["Purchase_Private_SupplierID"]);
                        entity.Purchase_Private_PurchaseID = Tools.NullInt(RdrList["Purchase_Private_PurchaseID"]);

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

        public virtual bool GetSupplierPurchasePrivatesByPurchaseSupplier(int PurchaseID, int SupplierID)
        {
            try
            {
               string SqlCount = "select count(*) from Supplier_Purchase_Private where Purchase_Private_PurchaseID=" + PurchaseID + " and Purchase_Private_SupplierID=" + SupplierID;
               int RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
               if (RecordCount > 0)
               {
                   return true;
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

        }
    }

    public class SupplierPurchaseDetail : ISupplierPurchaseDetail
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPurchaseDetail()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Purchase_Detail";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Detail_ID"] = entity.Detail_ID;
            DrAdd["Detail_PurchaseID"] = entity.Detail_PurchaseID;
            DrAdd["Detail_Name"] = entity.Detail_Name;
            DrAdd["Detail_Spec"] = entity.Detail_Spec;
            DrAdd["Detail_Amount"] = entity.Detail_Amount;
            DrAdd["Detail_Price"] = entity.Detail_Price;

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

        public virtual bool EditSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Purchase_Detail WHERE Detail_ID = " + entity.Detail_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Detail_ID"] = entity.Detail_ID;
                    DrAdd["Detail_PurchaseID"] = entity.Detail_PurchaseID;
                    DrAdd["Detail_Name"] = entity.Detail_Name;
                    DrAdd["Detail_Spec"] = entity.Detail_Spec;
                    DrAdd["Detail_Amount"] = entity.Detail_Amount;
                    DrAdd["Detail_Price"] = entity.Detail_Price;

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

        public virtual int DelSupplierPurchaseDetail(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Purchase_Detail WHERE Detail_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelSupplierPurchaseDetailByPurchaseID(int Apply_ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Purchase_Detail WHERE Detail_PurchaseID = " + Apply_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPurchaseDetailInfo GetSupplierPurchaseDetailByID(int ID)
        {
            SupplierPurchaseDetailInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Purchase_Detail WHERE Detail_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPurchaseDetailInfo();

                    entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                    entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                    entity.Detail_Name = Tools.NullStr(RdrList["Detail_Name"]);
                    entity.Detail_Spec = Tools.NullStr(RdrList["Detail_Spec"]);
                    entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                    entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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

        public virtual IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetailsByPurchaseID(int Apply_ID)
        {
            IList<SupplierPurchaseDetailInfo> entitys = null;
            SupplierPurchaseDetailInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Supplier_Purchase_Detail where Detail_PurchaseID=" + Apply_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseDetailInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseDetailInfo();
                        entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                        entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                        entity.Detail_Name = Tools.NullStr(RdrList["Detail_Name"]);
                        entity.Detail_Spec = Tools.NullStr(RdrList["Detail_Spec"]);
                        entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                        entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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

        public virtual IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetails(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPurchaseDetailInfo> entitys = null;
            SupplierPurchaseDetailInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Purchase_Detail";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseDetailInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseDetailInfo();
                        entity.Detail_ID = Tools.NullInt(RdrList["Detail_ID"]);
                        entity.Detail_PurchaseID = Tools.NullInt(RdrList["Detail_PurchaseID"]);
                        entity.Detail_Name = Tools.NullStr(RdrList["Detail_Name"]);
                        entity.Detail_Spec = Tools.NullStr(RdrList["Detail_Spec"]);
                        entity.Detail_Amount = Tools.NullInt(RdrList["Detail_Amount"]);
                        entity.Detail_Price = Tools.NullDbl(RdrList["Detail_Price"]);

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
                SqlTable = "Supplier_Purchase_Detail";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Detail_ID) FROM " + SqlTable + SqlParam;

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

    public class SupplierPurchaseCategory : ISupplierPurchaseCategory
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPurchaseCategory()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Purchase_Category";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Cate_ID"] = entity.Cate_ID;
            DrAdd["Cate_Name"] = entity.Cate_Name;
            DrAdd["Cate_ParentID"] = entity.Cate_ParentID;
            DrAdd["Cate_Sort"] = entity.Cate_Sort;
            DrAdd["Cate_IsActive"] = entity.Cate_IsActive;
            DrAdd["Cate_Site"] = entity.Cate_Site;

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

        public virtual bool EditSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Purchase_Category WHERE Cate_ID = " + entity.Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Cate_ID"] = entity.Cate_ID;
                    DrAdd["Cate_Name"] = entity.Cate_Name;
                    DrAdd["Cate_ParentID"] = entity.Cate_ParentID;
                    DrAdd["Cate_Sort"] = entity.Cate_Sort;
                    DrAdd["Cate_IsActive"] = entity.Cate_IsActive;
                    DrAdd["Cate_Site"] = entity.Cate_Site;

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

        public virtual int DelSupplierPurchaseCategory(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Purchase_Category WHERE Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int ID)
        {
            SupplierPurchaseCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Purchase_Category WHERE Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPurchaseCategoryInfo();

                    entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                    entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                    entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                    entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                    entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                    entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);

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

        public virtual IList<SupplierPurchaseCategoryInfo> GetSupplierPurchaseCategorys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPurchaseCategoryInfo> entitys = null;
            SupplierPurchaseCategoryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Purchase_Category";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseCategoryInfo();
                        entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                        entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                        entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                        entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                        entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                        entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);

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
                SqlTable = "Supplier_Purchase_Category";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Cate_ID) FROM " + SqlTable + SqlParam;

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



        public virtual int GetSubSupplierPurchaseCateCount(int Cate_ID, string SiteSign)
        {
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar("SELECT COUNT(Cate_ID) FROM Supplier_Purchase_Category WHERE Cate_IsActive=1 AND Cate_ParentID = " + Cate_ID + " AND Cate_Site = '" + SiteSign + "'"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierPurchaseCategoryInfo> GetSubSupplierPurchaseCategorys(int Cate_ID, string SiteSign)
        {
            IList<SupplierPurchaseCategoryInfo> entitys = null;
            SupplierPurchaseCategoryInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Supplier_Purchase_Category WHERE Cate_IsActive=1 AND Cate_ParentID = " + Cate_ID + " AND Cate_Site = '" + SiteSign + "' Order By Cate_Sort Asc,Cate_ID Desc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPurchaseCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPurchaseCategoryInfo();
                        entity.Cate_ID = Tools.NullInt(RdrList["Cate_ID"]);
                        entity.Cate_ParentID = Tools.NullInt(RdrList["Cate_ParentID"]);
                        entity.Cate_Name = Tools.NullStr(RdrList["Cate_Name"]);
                        entity.Cate_Sort = Tools.NullInt(RdrList["Cate_Sort"]);
                        entity.Cate_IsActive = Tools.NullInt(RdrList["Cate_IsActive"]);
                        entity.Cate_Site = Tools.NullStr(RdrList["Cate_Site"]);
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

        public virtual string Get_All_SubSupplierPurchaseCateID(int Cate_ID)
        {
            string SqlList, Cate_Arry;
            Cate_Arry = Cate_ID.ToString();
            if (Cate_ID == 0)
            {
                return Cate_Arry;
            }
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "with a as (select cate_id from Supplier_Purchase_Category where cate_id=" + Cate_ID + " union all select Supplier_Purchase_Category.cate_id from Supplier_Purchase_Category,a where Supplier_Purchase_Category.cate_parentid=a.cate_id) select * from a";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    while (RdrList.Read())
                    {
                        if (Cate_ID != Tools.NullInt(RdrList["Cate_ID"]))
                        {
                            Cate_Arry += "," + Tools.NullInt(RdrList["Cate_ID"]);
                        }
                    }
                }
                return Cate_Arry;
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
