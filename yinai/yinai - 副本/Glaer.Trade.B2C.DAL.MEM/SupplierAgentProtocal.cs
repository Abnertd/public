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
    public class SupplierAgentProtocal : ISupplierAgentProtocal
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierAgentProtocal()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierAgentProtocal(SupplierAgentProtocalInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Agent_Protocal";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Protocal_ID"] = entity.Protocal_ID;
            DrAdd["Protocal_Code"] = entity.Protocal_Code;
            DrAdd["Protocal_PurchaseID"] = entity.Protocal_PurchaseID;
            DrAdd["Protocal_SupplierID"] = entity.Protocal_SupplierID;
            DrAdd["Protocal_Status"] = entity.Protocal_Status;
            DrAdd["Protocal_Template"] = entity.Protocal_Template;
            DrAdd["Protocal_Addtime"] = entity.Protocal_Addtime;
            DrAdd["Protocal_Site"] = entity.Protocal_Site;

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

        public virtual bool EditSupplierAgentProtocal(SupplierAgentProtocalInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Agent_Protocal WHERE Protocal_ID = " + entity.Protocal_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Protocal_ID"] = entity.Protocal_ID;
                    DrAdd["Protocal_Code"] = entity.Protocal_Code;
                    DrAdd["Protocal_PurchaseID"] = entity.Protocal_PurchaseID;
                    DrAdd["Protocal_SupplierID"] = entity.Protocal_SupplierID;
                    DrAdd["Protocal_Status"] = entity.Protocal_Status;
                    DrAdd["Protocal_Template"] = entity.Protocal_Template;
                    DrAdd["Protocal_Addtime"] = entity.Protocal_Addtime;
                    DrAdd["Protocal_Site"] = entity.Protocal_Site;

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

        public virtual int DelSupplierAgentProtocal(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Agent_Protocal WHERE Protocal_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID)
        {
            SupplierAgentProtocalInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Agent_Protocal WHERE Protocal_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierAgentProtocalInfo();

                    entity.Protocal_ID = Tools.NullInt(RdrList["Protocal_ID"]);
                    entity.Protocal_Code = Tools.NullStr(RdrList["Protocal_Code"]);
                    entity.Protocal_PurchaseID = Tools.NullInt(RdrList["Protocal_PurchaseID"]);
                    entity.Protocal_SupplierID = Tools.NullInt(RdrList["Protocal_SupplierID"]);
                    entity.Protocal_Status = Tools.NullInt(RdrList["Protocal_Status"]);
                    entity.Protocal_Template = Tools.NullStr(RdrList["Protocal_Template"]);
                    entity.Protocal_Addtime = Tools.NullDate(RdrList["Protocal_Addtime"]);
                    entity.Protocal_Site = Tools.NullStr(RdrList["Protocal_Site"]);

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

        public virtual SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID)
        {
            SupplierAgentProtocalInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Agent_Protocal WHERE Protocal_PurchaseID = " + PurchaseID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierAgentProtocalInfo();

                    entity.Protocal_ID = Tools.NullInt(RdrList["Protocal_ID"]);
                    entity.Protocal_Code = Tools.NullStr(RdrList["Protocal_Code"]);
                    entity.Protocal_PurchaseID = Tools.NullInt(RdrList["Protocal_PurchaseID"]);
                    entity.Protocal_SupplierID = Tools.NullInt(RdrList["Protocal_SupplierID"]);
                    entity.Protocal_Status = Tools.NullInt(RdrList["Protocal_Status"]);
                    entity.Protocal_Template = Tools.NullStr(RdrList["Protocal_Template"]);
                    entity.Protocal_Addtime = Tools.NullDate(RdrList["Protocal_Addtime"]);
                    entity.Protocal_Site = Tools.NullStr(RdrList["Protocal_Site"]);

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


        public virtual IList<SupplierAgentProtocalInfo> GetSupplierAgentProtocals(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierAgentProtocalInfo> entitys = null;
            SupplierAgentProtocalInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Agent_Protocal";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierAgentProtocalInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierAgentProtocalInfo();
                        entity.Protocal_ID = Tools.NullInt(RdrList["Protocal_ID"]);
                        entity.Protocal_Code = Tools.NullStr(RdrList["Protocal_Code"]);
                        entity.Protocal_PurchaseID = Tools.NullInt(RdrList["Protocal_PurchaseID"]);
                        entity.Protocal_SupplierID = Tools.NullInt(RdrList["Protocal_SupplierID"]);
                        entity.Protocal_Status = Tools.NullInt(RdrList["Protocal_Status"]);
                        entity.Protocal_Template = Tools.NullStr(RdrList["Protocal_Template"]);
                        entity.Protocal_Addtime = Tools.NullDate(RdrList["Protocal_Addtime"]);
                        entity.Protocal_Site = Tools.NullStr(RdrList["Protocal_Site"]);

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
                SqlTable = "Supplier_Agent_Protocal";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Protocal_ID) FROM " + SqlTable + SqlParam;

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
