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
    public class SupplierPayBackApply : ISupplierPayBackApply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierPayBackApply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierPayBackApply(SupplierPayBackApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_PayBack_Apply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_PayBack_Apply_ID"] = entity.Supplier_PayBack_Apply_ID;
            DrAdd["Supplier_PayBack_Apply_SupplierID"] = entity.Supplier_PayBack_Apply_SupplierID;
            DrAdd["Supplier_PayBack_Apply_Type"] = entity.Supplier_PayBack_Apply_Type;
            DrAdd["Supplier_PayBack_Apply_Amount"] = entity.Supplier_PayBack_Apply_Amount;
            DrAdd["Supplier_PayBack_Apply_Note"] = entity.Supplier_PayBack_Apply_Note;
            DrAdd["Supplier_PayBack_Apply_Addtime"] = entity.Supplier_PayBack_Apply_Addtime;
            DrAdd["Supplier_PayBack_Apply_Status"] = entity.Supplier_PayBack_Apply_Status;
            DrAdd["Supplier_PayBack_Apply_AdminAmount"] = entity.Supplier_PayBack_Apply_AdminAmount;
            DrAdd["Supplier_PayBack_Apply_AdminNote"] = entity.Supplier_PayBack_Apply_AdminNote;
            DrAdd["Supplier_PayBack_Apply_AdminTime"] = entity.Supplier_PayBack_Apply_AdminTime;
            DrAdd["Supplier_PayBack_Apply_Site"] = entity.Supplier_PayBack_Apply_Site;
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

        public virtual bool EditSupplierPayBackApply(SupplierPayBackApplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_PayBack_Apply WHERE Supplier_PayBack_Apply_ID = " + entity.Supplier_PayBack_Apply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_PayBack_Apply_ID"] = entity.Supplier_PayBack_Apply_ID;
                    DrAdd["Supplier_PayBack_Apply_SupplierID"] = entity.Supplier_PayBack_Apply_SupplierID;
                    DrAdd["Supplier_PayBack_Apply_Type"] = entity.Supplier_PayBack_Apply_Type;
                    DrAdd["Supplier_PayBack_Apply_Amount"] = entity.Supplier_PayBack_Apply_Amount;
                    DrAdd["Supplier_PayBack_Apply_Note"] = entity.Supplier_PayBack_Apply_Note;
                    DrAdd["Supplier_PayBack_Apply_Addtime"] = entity.Supplier_PayBack_Apply_Addtime;
                    DrAdd["Supplier_PayBack_Apply_Status"] = entity.Supplier_PayBack_Apply_Status;
                    DrAdd["Supplier_PayBack_Apply_AdminAmount"] = entity.Supplier_PayBack_Apply_AdminAmount;
                    DrAdd["Supplier_PayBack_Apply_AdminNote"] = entity.Supplier_PayBack_Apply_AdminNote;
                    DrAdd["Supplier_PayBack_Apply_AdminTime"] = entity.Supplier_PayBack_Apply_AdminTime;
                    DrAdd["Supplier_PayBack_Apply_Site"] = entity.Supplier_PayBack_Apply_Site;
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

        public virtual int DelSupplierPayBackApply(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_PayBack_Apply WHERE Supplier_PayBack_Apply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int ID)
        {
            SupplierPayBackApplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_PayBack_Apply WHERE Supplier_PayBack_Apply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierPayBackApplyInfo();

                    entity.Supplier_PayBack_Apply_ID = Tools.NullInt(RdrList["Supplier_PayBack_Apply_ID"]);
                    entity.Supplier_PayBack_Apply_SupplierID = Tools.NullInt(RdrList["Supplier_PayBack_Apply_SupplierID"]);
                    entity.Supplier_PayBack_Apply_Type = Tools.NullInt(RdrList["Supplier_PayBack_Apply_Type"]);
                    entity.Supplier_PayBack_Apply_Amount = Tools.NullDbl(RdrList["Supplier_PayBack_Apply_Amount"]);
                    entity.Supplier_PayBack_Apply_Note = Tools.NullStr(RdrList["Supplier_PayBack_Apply_Note"]);
                    entity.Supplier_PayBack_Apply_Addtime = Tools.NullDate(RdrList["Supplier_PayBack_Apply_Addtime"]);
                    entity.Supplier_PayBack_Apply_Status = Tools.NullInt(RdrList["Supplier_PayBack_Apply_Status"]);
                    entity.Supplier_PayBack_Apply_AdminAmount = Tools.NullDbl(RdrList["Supplier_PayBack_Apply_AdminAmount"]);
                    entity.Supplier_PayBack_Apply_AdminNote = Tools.NullStr(RdrList["Supplier_PayBack_Apply_AdminNote"]);
                    entity.Supplier_PayBack_Apply_AdminTime = Tools.NullDate(RdrList["Supplier_PayBack_Apply_AdminTime"]);
                    entity.Supplier_PayBack_Apply_Site = Tools.NullStr(RdrList["Supplier_PayBack_Apply_Site"]);
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

        public virtual IList<SupplierPayBackApplyInfo> GetSupplierPayBackApplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierPayBackApplyInfo> entitys = null;
            SupplierPayBackApplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_PayBack_Apply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierPayBackApplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierPayBackApplyInfo();
                        entity.Supplier_PayBack_Apply_ID = Tools.NullInt(RdrList["Supplier_PayBack_Apply_ID"]);
                        entity.Supplier_PayBack_Apply_SupplierID = Tools.NullInt(RdrList["Supplier_PayBack_Apply_SupplierID"]);
                        entity.Supplier_PayBack_Apply_Type = Tools.NullInt(RdrList["Supplier_PayBack_Apply_Type"]);
                        entity.Supplier_PayBack_Apply_Amount = Tools.NullDbl(RdrList["Supplier_PayBack_Apply_Amount"]);
                        entity.Supplier_PayBack_Apply_Note = Tools.NullStr(RdrList["Supplier_PayBack_Apply_Note"]);
                        entity.Supplier_PayBack_Apply_Addtime = Tools.NullDate(RdrList["Supplier_PayBack_Apply_Addtime"]);
                        entity.Supplier_PayBack_Apply_Status = Tools.NullInt(RdrList["Supplier_PayBack_Apply_Status"]);
                        entity.Supplier_PayBack_Apply_AdminAmount = Tools.NullDbl(RdrList["Supplier_PayBack_Apply_AdminAmount"]);
                        entity.Supplier_PayBack_Apply_AdminNote = Tools.NullStr(RdrList["Supplier_PayBack_Apply_AdminNote"]);
                        entity.Supplier_PayBack_Apply_AdminTime = Tools.NullDate(RdrList["Supplier_PayBack_Apply_AdminTime"]);
                        entity.Supplier_PayBack_Apply_Site = Tools.NullStr(RdrList["Supplier_PayBack_Apply_Site"]);
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
                SqlTable = "Supplier_PayBack_Apply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_PayBack_Apply_ID) FROM " + SqlTable + SqlParam;

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
