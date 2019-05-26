using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class RBACPrivilege : IRBACPrivilege
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public RBACPrivilege()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddRBACPrivilege(RBACPrivilegeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_Privilege";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            //DrAdd["RBAC_Privilege_ID"] = entity.RBAC_Privilege_ID;
            DrAdd["RBAC_Privilege_ResourceID"] = entity.RBAC_Privilege_ResourceID;
            DrAdd["RBAC_Privilege_Name"] = entity.RBAC_Privilege_Name;
            DrAdd["RBAC_Privilege_IsActive"] = entity.RBAC_Privilege_IsActive;
            DrAdd["RBAC_Privilege_Addtime"] = entity.RBAC_Privilege_Addtime;

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

        public virtual bool EditRBACPrivilege(RBACPrivilegeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_Privilege WHERE RBAC_Privilege_ID = '" + entity.RBAC_Privilege_ID + "'";
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_Privilege_ID"] = entity.RBAC_Privilege_ID;
                    DrAdd["RBAC_Privilege_ResourceID"] = entity.RBAC_Privilege_ResourceID;
                    DrAdd["RBAC_Privilege_Name"] = entity.RBAC_Privilege_Name;
                    DrAdd["RBAC_Privilege_IsActive"] = entity.RBAC_Privilege_IsActive;
                    DrAdd["RBAC_Privilege_Addtime"] = entity.RBAC_Privilege_Addtime;

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

        public virtual int DelRBACPrivilege(string ID)
        {
            string SqlAdd = "DELETE FROM RBAC_Privilege WHERE RBAC_Privilege_ID = '" + ID + "'";
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual RBACPrivilegeInfo GetRBACPrivilegeByID(string ID)
        {
            RBACPrivilegeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_Privilege WHERE RBAC_Privilege_ID = '" + ID + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACPrivilegeInfo();
                    entity.RBAC_Privilege_ID = Tools.NullStr(RdrList["RBAC_Privilege_ID"]);
                    entity.RBAC_Privilege_ResourceID = Tools.NullInt(RdrList["RBAC_Privilege_ResourceID"]);
                    entity.RBAC_Privilege_Name = Tools.NullStr(RdrList["RBAC_Privilege_Name"]);
                    entity.RBAC_Privilege_IsActive = Tools.NullInt(RdrList["RBAC_Privilege_IsActive"]);
                    entity.RBAC_Privilege_Addtime = Tools.NullDate(RdrList["RBAC_Privilege_Addtime"]);
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

        public virtual IList<RBACPrivilegeInfo> GetRBACPrivileges(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACPrivilegeInfo> entitys = null;
            RBACPrivilegeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_Privilege";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<RBACPrivilegeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACPrivilegeInfo();
                        entity.RBAC_Privilege_ID = Tools.NullStr(RdrList["RBAC_Privilege_ID"]);
                        entity.RBAC_Privilege_ResourceID = Tools.NullInt(RdrList["RBAC_Privilege_ResourceID"]);
                        entity.RBAC_Privilege_Name = Tools.NullStr(RdrList["RBAC_Privilege_Name"]);
                        entity.RBAC_Privilege_IsActive = Tools.NullInt(RdrList["RBAC_Privilege_IsActive"]);
                        entity.RBAC_Privilege_Addtime = Tools.NullDate(RdrList["RBAC_Privilege_Addtime"]);
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
                SqlTable = "RBAC_Privilege";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_Privilege_ID) FROM " + SqlTable + SqlParam;

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
