using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using System.Data.SqlClient;
using System.Data;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierSubAccount : ISupplierSubAccount
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierSubAccount()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierSubAccount(SupplierSubAccountInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_SubAccount";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_SubAccount_ID"] = entity.Supplier_SubAccount_ID;
            DrAdd["Supplier_SubAccount_SupplierID"] = entity.Supplier_SubAccount_SupplierID;
            DrAdd["Supplier_SubAccount_Name"] = entity.Supplier_SubAccount_Name;
            DrAdd["Supplier_SubAccount_Password"] = entity.Supplier_SubAccount_Password;
            DrAdd["Supplier_SubAccount_TrueName"] = entity.Supplier_SubAccount_TrueName;
            DrAdd["Supplier_SubAccount_Department"] = entity.Supplier_SubAccount_Department;
            DrAdd["Supplier_SubAccount_Tel"] = entity.Supplier_SubAccount_Tel;
            DrAdd["Supplier_SubAccount_Mobile"] = entity.Supplier_SubAccount_Mobile;
            DrAdd["Supplier_SubAccount_Email"] = entity.Supplier_SubAccount_Email;
            DrAdd["Supplier_SubAccount_ExpireTime"] = entity.Supplier_SubAccount_ExpireTime;
            DrAdd["Supplier_SubAccount_AddTime"] = entity.Supplier_SubAccount_AddTime;
            DrAdd["Supplier_SubAccount_lastLoginTime"] = entity.Supplier_SubAccount_lastLoginTime;
            DrAdd["Supplier_SubAccount_IsActive"] = entity.Supplier_SubAccount_IsActive;
            DrAdd["Supplier_SubAccount_Privilege"] = entity.Supplier_SubAccount_Privilege;

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

        public virtual bool EditSupplierSubAccount(SupplierSubAccountInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_SubAccount WHERE Supplier_SubAccount_ID = " + entity.Supplier_SubAccount_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_SubAccount_ID"] = entity.Supplier_SubAccount_ID;
                    DrAdd["Supplier_SubAccount_SupplierID"] = entity.Supplier_SubAccount_SupplierID;
                    DrAdd["Supplier_SubAccount_Name"] = entity.Supplier_SubAccount_Name;
                    DrAdd["Supplier_SubAccount_Password"] = entity.Supplier_SubAccount_Password;
                    DrAdd["Supplier_SubAccount_TrueName"] = entity.Supplier_SubAccount_TrueName;
                    DrAdd["Supplier_SubAccount_Department"] = entity.Supplier_SubAccount_Department;
                    DrAdd["Supplier_SubAccount_Tel"] = entity.Supplier_SubAccount_Tel;
                    DrAdd["Supplier_SubAccount_Mobile"] = entity.Supplier_SubAccount_Mobile;
                    DrAdd["Supplier_SubAccount_Email"] = entity.Supplier_SubAccount_Email;
                    DrAdd["Supplier_SubAccount_ExpireTime"] = entity.Supplier_SubAccount_ExpireTime;
                    DrAdd["Supplier_SubAccount_AddTime"] = entity.Supplier_SubAccount_AddTime;
                    DrAdd["Supplier_SubAccount_lastLoginTime"] = entity.Supplier_SubAccount_lastLoginTime;
                    DrAdd["Supplier_SubAccount_IsActive"] = entity.Supplier_SubAccount_IsActive;
                    DrAdd["Supplier_SubAccount_Privilege"] = entity.Supplier_SubAccount_Privilege;

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

        public virtual int DelSupplierSubAccount(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_SubAccount WHERE Supplier_SubAccount_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual SupplierSubAccountInfo GetSupplierSubAccountByName(string name)
        {
            SupplierSubAccountInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_SubAccount WHERE Supplier_SubAccount_Name = '" + name + "' OR Supplier_SubAccount_Mobile = '" + name + "' OR Supplier_SubAccount_Email = '" + name + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierSubAccountInfo();

                    entity.Supplier_SubAccount_ID = Tools.NullInt(RdrList["Supplier_SubAccount_ID"]);
                    entity.Supplier_SubAccount_SupplierID = Tools.NullInt(RdrList["Supplier_SubAccount_SupplierID"]);
                    entity.Supplier_SubAccount_Name = Tools.NullStr(RdrList["Supplier_SubAccount_Name"]);
                    entity.Supplier_SubAccount_Password = Tools.NullStr(RdrList["Supplier_SubAccount_Password"]);
                    entity.Supplier_SubAccount_TrueName = Tools.NullStr(RdrList["Supplier_SubAccount_TrueName"]);
                    entity.Supplier_SubAccount_Department = Tools.NullStr(RdrList["Supplier_SubAccount_Department"]);
                    entity.Supplier_SubAccount_Tel = Tools.NullStr(RdrList["Supplier_SubAccount_Tel"]);
                    entity.Supplier_SubAccount_Mobile = Tools.NullStr(RdrList["Supplier_SubAccount_Mobile"]);
                    entity.Supplier_SubAccount_Email = Tools.NullStr(RdrList["Supplier_SubAccount_Email"]);
                    entity.Supplier_SubAccount_ExpireTime = Tools.NullDate(RdrList["Supplier_SubAccount_ExpireTime"]);
                    entity.Supplier_SubAccount_AddTime = Tools.NullDate(RdrList["Supplier_SubAccount_AddTime"]);
                    entity.Supplier_SubAccount_lastLoginTime = Tools.NullDate(RdrList["Supplier_SubAccount_lastLoginTime"]);
                    entity.Supplier_SubAccount_IsActive = Tools.NullInt(RdrList["Supplier_SubAccount_IsActive"]);
                    entity.Supplier_SubAccount_Privilege = Tools.NullStr(RdrList["Supplier_SubAccount_Privilege"]);

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


        public virtual SupplierSubAccountInfo GetSupplierSubAccountByID(int ID)
        {
            SupplierSubAccountInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_SubAccount WHERE Supplier_SubAccount_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierSubAccountInfo();

                    entity.Supplier_SubAccount_ID = Tools.NullInt(RdrList["Supplier_SubAccount_ID"]);
                    entity.Supplier_SubAccount_SupplierID = Tools.NullInt(RdrList["Supplier_SubAccount_SupplierID"]);
                    entity.Supplier_SubAccount_Name = Tools.NullStr(RdrList["Supplier_SubAccount_Name"]);
                    entity.Supplier_SubAccount_Password = Tools.NullStr(RdrList["Supplier_SubAccount_Password"]);
                    entity.Supplier_SubAccount_TrueName = Tools.NullStr(RdrList["Supplier_SubAccount_TrueName"]);
                    entity.Supplier_SubAccount_Department = Tools.NullStr(RdrList["Supplier_SubAccount_Department"]);
                    entity.Supplier_SubAccount_Tel = Tools.NullStr(RdrList["Supplier_SubAccount_Tel"]);
                    entity.Supplier_SubAccount_Mobile = Tools.NullStr(RdrList["Supplier_SubAccount_Mobile"]);
                    entity.Supplier_SubAccount_Email = Tools.NullStr(RdrList["Supplier_SubAccount_Email"]);
                    entity.Supplier_SubAccount_ExpireTime = Tools.NullDate(RdrList["Supplier_SubAccount_ExpireTime"]);
                    entity.Supplier_SubAccount_AddTime = Tools.NullDate(RdrList["Supplier_SubAccount_AddTime"]);
                    entity.Supplier_SubAccount_lastLoginTime = Tools.NullDate(RdrList["Supplier_SubAccount_lastLoginTime"]);
                    entity.Supplier_SubAccount_IsActive = Tools.NullInt(RdrList["Supplier_SubAccount_IsActive"]);
                    entity.Supplier_SubAccount_Privilege = Tools.NullStr(RdrList["Supplier_SubAccount_Privilege"]);
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

        public virtual IList<SupplierSubAccountInfo> GetSupplierSubAccounts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierSubAccountInfo> entitys = null;
            SupplierSubAccountInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_SubAccount";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierSubAccountInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierSubAccountInfo();
                        entity.Supplier_SubAccount_ID = Tools.NullInt(RdrList["Supplier_SubAccount_ID"]);
                        entity.Supplier_SubAccount_SupplierID = Tools.NullInt(RdrList["Supplier_SubAccount_SupplierID"]);
                        entity.Supplier_SubAccount_Name = Tools.NullStr(RdrList["Supplier_SubAccount_Name"]);
                        entity.Supplier_SubAccount_Password = Tools.NullStr(RdrList["Supplier_SubAccount_Password"]);
                        entity.Supplier_SubAccount_TrueName = Tools.NullStr(RdrList["Supplier_SubAccount_TrueName"]);
                        entity.Supplier_SubAccount_Department = Tools.NullStr(RdrList["Supplier_SubAccount_Department"]);
                        entity.Supplier_SubAccount_Tel = Tools.NullStr(RdrList["Supplier_SubAccount_Tel"]);
                        entity.Supplier_SubAccount_Mobile = Tools.NullStr(RdrList["Supplier_SubAccount_Mobile"]);
                        entity.Supplier_SubAccount_Email = Tools.NullStr(RdrList["Supplier_SubAccount_Email"]);
                        entity.Supplier_SubAccount_ExpireTime = Tools.NullDate(RdrList["Supplier_SubAccount_ExpireTime"]);
                        entity.Supplier_SubAccount_AddTime = Tools.NullDate(RdrList["Supplier_SubAccount_AddTime"]);
                        entity.Supplier_SubAccount_lastLoginTime = Tools.NullDate(RdrList["Supplier_SubAccount_lastLoginTime"]);
                        entity.Supplier_SubAccount_IsActive = Tools.NullInt(RdrList["Supplier_SubAccount_IsActive"]);
                        entity.Supplier_SubAccount_Privilege = Tools.NullStr(RdrList["Supplier_SubAccount_Privilege"]);

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
                SqlTable = "Supplier_SubAccount";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_SubAccount_ID) FROM " + SqlTable + SqlParam;

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
