using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierGrade : ISupplierGrade
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierGrade()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierGrade(SupplierGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Grade";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Grade_ID"] = entity.Supplier_Grade_ID;
            DrAdd["Supplier_Grade_Name"] = entity.Supplier_Grade_Name;
            DrAdd["Supplier_Grade_Percent"] = entity.Supplier_Grade_Percent;
            DrAdd["Supplier_Grade_Default"] = entity.Supplier_Grade_Default;
            DrAdd["Supplier_Grade_RequiredCoin"] = entity.Supplier_Grade_RequiredCoin;
            DrAdd["Supplier_Grade_Site"] = entity.Supplier_Grade_Site;

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

        public virtual bool EditSupplierGrade(SupplierGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Grade WHERE Supplier_Grade_ID = " + entity.Supplier_Grade_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Grade_ID"] = entity.Supplier_Grade_ID;
                    DrAdd["Supplier_Grade_Name"] = entity.Supplier_Grade_Name;
                    DrAdd["Supplier_Grade_Percent"] = entity.Supplier_Grade_Percent;
                    DrAdd["Supplier_Grade_Default"] = entity.Supplier_Grade_Default;
                    DrAdd["Supplier_Grade_RequiredCoin"] = entity.Supplier_Grade_RequiredCoin;
                    DrAdd["Supplier_Grade_Site"] = entity.Supplier_Grade_Site;

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

        public virtual int DelSupplierGrade(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Grade WHERE Supplier_Grade_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierGradeInfo GetSupplierGradeByID(int ID)
        {
            SupplierGradeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Grade WHERE Supplier_Grade_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierGradeInfo();

                    entity.Supplier_Grade_ID = Tools.NullInt(RdrList["Supplier_Grade_ID"]);
                    entity.Supplier_Grade_Name = Tools.NullStr(RdrList["Supplier_Grade_Name"]);
                    entity.Supplier_Grade_Percent = Tools.NullInt(RdrList["Supplier_Grade_Percent"]);
                    entity.Supplier_Grade_Default = Tools.NullInt(RdrList["Supplier_Grade_Default"]);
                    entity.Supplier_Grade_RequiredCoin = Tools.NullInt(RdrList["Supplier_Grade_RequiredCoin"]);
                    entity.Supplier_Grade_Site = Tools.NullStr(RdrList["Supplier_Grade_Site"]);

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

        public virtual IList<SupplierGradeInfo> GetSupplierGrades(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierGradeInfo> entitys = null;
            SupplierGradeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Grade";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierGradeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierGradeInfo();
                        entity.Supplier_Grade_ID = Tools.NullInt(RdrList["Supplier_Grade_ID"]);
                        entity.Supplier_Grade_Name = Tools.NullStr(RdrList["Supplier_Grade_Name"]);
                        entity.Supplier_Grade_Percent = Tools.NullInt(RdrList["Supplier_Grade_Percent"]);
                        entity.Supplier_Grade_Default = Tools.NullInt(RdrList["Supplier_Grade_Default"]);
                        entity.Supplier_Grade_RequiredCoin = Tools.NullInt(RdrList["Supplier_Grade_RequiredCoin"]);
                        entity.Supplier_Grade_Site = Tools.NullStr(RdrList["Supplier_Grade_Site"]);

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
                SqlTable = "Supplier_Grade";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Grade_ID) FROM " + SqlTable + SqlParam;

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

        public virtual SupplierGradeInfo GetSupplierDefaultGrade()
        {
            SupplierGradeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Grade WHERE Supplier_Grade_Default = 1 ";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierGradeInfo();
                    entity.Supplier_Grade_ID = Tools.NullInt(RdrList["Supplier_Grade_ID"]);
                    entity.Supplier_Grade_Name = Tools.NullStr(RdrList["Supplier_Grade_Name"]);
                    entity.Supplier_Grade_Percent = Tools.NullInt(RdrList["Supplier_Grade_Percent"]);
                    entity.Supplier_Grade_Default = Tools.NullInt(RdrList["Supplier_Grade_Default"]);
                    entity.Supplier_Grade_RequiredCoin = Tools.NullInt(RdrList["Supplier_Grade_RequiredCoin"]);
                    entity.Supplier_Grade_Site = Tools.NullStr(RdrList["Supplier_Grade_Site"]);
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
