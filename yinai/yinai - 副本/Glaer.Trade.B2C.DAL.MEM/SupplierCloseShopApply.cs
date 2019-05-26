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
    public class SupplierCloseShopApply : ISupplierCloseShopApply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCloseShopApply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierCloseShopApply(SupplierCloseShopApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_CloseShop_Apply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["CloseShop_Apply_ID"] = entity.CloseShop_Apply_ID;
            DrAdd["CloseShop_Apply_SupplierID"] = entity.CloseShop_Apply_SupplierID;
            DrAdd["CloseShop_Apply_Note"] = entity.CloseShop_Apply_Note;
            DrAdd["CloseShop_Apply_Status"] = entity.CloseShop_Apply_Status;
            DrAdd["CloseShop_Apply_AdminNote"] = entity.CloseShop_Apply_AdminNote;
            DrAdd["CloseShop_Apply_Addtime"] = entity.CloseShop_Apply_Addtime;
            DrAdd["CloseShop_Apply_AdminTime"] = entity.CloseShop_Apply_AdminTime;
            DrAdd["CloseShop_Apply_Site"] = entity.CloseShop_Apply_Site;

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

        public virtual bool EditSupplierCloseShopApply(SupplierCloseShopApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_CloseShop_Apply WHERE CloseShop_Apply_ID = " + entity.CloseShop_Apply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["CloseShop_Apply_ID"] = entity.CloseShop_Apply_ID;
                    DrAdd["CloseShop_Apply_SupplierID"] = entity.CloseShop_Apply_SupplierID;
                    DrAdd["CloseShop_Apply_Note"] = entity.CloseShop_Apply_Note;
                    DrAdd["CloseShop_Apply_Status"] = entity.CloseShop_Apply_Status;
                    DrAdd["CloseShop_Apply_AdminNote"] = entity.CloseShop_Apply_AdminNote;
                    DrAdd["CloseShop_Apply_Addtime"] = entity.CloseShop_Apply_Addtime;
                    DrAdd["CloseShop_Apply_AdminTime"] = entity.CloseShop_Apply_AdminTime;
                    DrAdd["CloseShop_Apply_Site"] = entity.CloseShop_Apply_Site;

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

        public virtual int DelSupplierCloseShopApply(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_CloseShop_Apply WHERE CloseShop_Apply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID(int ID)
        {
            SupplierCloseShopApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_CloseShop_Apply WHERE CloseShop_Apply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCloseShopApplyInfo();

                    entity.CloseShop_Apply_ID = Tools.NullInt(RdrList["CloseShop_Apply_ID"]);
                    entity.CloseShop_Apply_SupplierID = Tools.NullInt(RdrList["CloseShop_Apply_SupplierID"]);
                    entity.CloseShop_Apply_Note = Tools.NullStr(RdrList["CloseShop_Apply_Note"]);
                    entity.CloseShop_Apply_Status = Tools.NullInt(RdrList["CloseShop_Apply_Status"]);
                    entity.CloseShop_Apply_AdminNote = Tools.NullStr(RdrList["CloseShop_Apply_AdminNote"]);
                    entity.CloseShop_Apply_Addtime = Tools.NullDate(RdrList["CloseShop_Apply_Addtime"]);
                    entity.CloseShop_Apply_AdminTime = Tools.NullDate(RdrList["CloseShop_Apply_AdminTime"]);
                    entity.CloseShop_Apply_Site = Tools.NullStr(RdrList["CloseShop_Apply_Site"]);

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

        public virtual IList<SupplierCloseShopApplyInfo> GetSupplierCloseShopApplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCloseShopApplyInfo> entitys = null;
            SupplierCloseShopApplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_CloseShop_Apply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCloseShopApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCloseShopApplyInfo();
                        entity.CloseShop_Apply_ID = Tools.NullInt(RdrList["CloseShop_Apply_ID"]);
                        entity.CloseShop_Apply_SupplierID = Tools.NullInt(RdrList["CloseShop_Apply_SupplierID"]);
                        entity.CloseShop_Apply_Note = Tools.NullStr(RdrList["CloseShop_Apply_Note"]);
                        entity.CloseShop_Apply_Status = Tools.NullInt(RdrList["CloseShop_Apply_Status"]);
                        entity.CloseShop_Apply_AdminNote = Tools.NullStr(RdrList["CloseShop_Apply_AdminNote"]);
                        entity.CloseShop_Apply_Addtime = Tools.NullDate(RdrList["CloseShop_Apply_Addtime"]);
                        entity.CloseShop_Apply_AdminTime = Tools.NullDate(RdrList["CloseShop_Apply_AdminTime"]);
                        entity.CloseShop_Apply_Site = Tools.NullStr(RdrList["CloseShop_Apply_Site"]);

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
                SqlTable = "Supplier_CloseShop_Apply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(CloseShop_Apply_ID) FROM " + SqlTable + SqlParam;

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
