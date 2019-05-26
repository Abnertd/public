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
    public class RBACResourceGroup : IRBACResourceGroup
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public RBACResourceGroup()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddRBACResourceGroup(RBACResourceGroupInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_ResourceGroup";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["RBAC_ResourceGroup_ID"] = entity.RBAC_ResourceGroup_ID;
            DrAdd["RBAC_ResourceGroup_Name"] = entity.RBAC_ResourceGroup_Name;
            DrAdd["RBAC_ResourceGroup_ParentID"] = entity.RBAC_ResourceGroup_ParentID;
            DrAdd["RBAC_ResourceGroup_Site"] = entity.RBAC_ResourceGroup_Site;

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

        public virtual bool EditRBACResourceGroup(RBACResourceGroupInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_ResourceGroup WHERE RBAC_ResourceGroup_ID = " + entity.RBAC_ResourceGroup_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_ResourceGroup_ID"] = entity.RBAC_ResourceGroup_ID;
                    DrAdd["RBAC_ResourceGroup_Name"] = entity.RBAC_ResourceGroup_Name;
                    DrAdd["RBAC_ResourceGroup_ParentID"] = entity.RBAC_ResourceGroup_ParentID;
                    DrAdd["RBAC_ResourceGroup_Site"] = entity.RBAC_ResourceGroup_Site;

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

        public virtual int DelRBACResourceGroup(int ID)
        {
            string SqlAdd = "DELETE FROM RBAC_ResourceGroup WHERE RBAC_ResourceGroup_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual RBACResourceGroupInfo GetRBACResourceGroupByID(int ID)
        {
            RBACResourceGroupInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_ResourceGroup WHERE RBAC_ResourceGroup_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACResourceGroupInfo();

                    entity.RBAC_ResourceGroup_ID = Tools.NullInt(RdrList["RBAC_ResourceGroup_ID"]);
                    entity.RBAC_ResourceGroup_Name = Tools.NullStr(RdrList["RBAC_ResourceGroup_Name"]);
                    entity.RBAC_ResourceGroup_ParentID = Tools.NullInt(RdrList["RBAC_ResourceGroup_ParentID"]);
                    entity.RBAC_ResourceGroup_Site = Tools.NullStr(RdrList["RBAC_ResourceGroup_Site"]);

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

        public virtual IList<RBACResourceGroupInfo> GetRBACResourceGroups(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACResourceGroupInfo> entitys = null;
            RBACResourceGroupInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_ResourceGroup";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<RBACResourceGroupInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACResourceGroupInfo();
                        entity.RBAC_ResourceGroup_ID = Tools.NullInt(RdrList["RBAC_ResourceGroup_ID"]);
                        entity.RBAC_ResourceGroup_Name = Tools.NullStr(RdrList["RBAC_ResourceGroup_Name"]);
                        entity.RBAC_ResourceGroup_ParentID = Tools.NullInt(RdrList["RBAC_ResourceGroup_ParentID"]);
                        entity.RBAC_ResourceGroup_Site = Tools.NullStr(RdrList["RBAC_ResourceGroup_Site"]);

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
                SqlTable = "RBAC_ResourceGroup";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_ResourceGroup_ID) FROM " + SqlTable + SqlParam;

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

    public class RBACResource : IRBACResource
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public RBACResource()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddRBACResource(RBACResourceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_Resource";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["RBAC_Resource_ID"] = entity.RBAC_Resource_ID;
            DrAdd["RBAC_Resource_GroupID"] = entity.RBAC_Resource_GroupID;
            DrAdd["RBAC_Resource_Name"] = entity.RBAC_Resource_Name;
            DrAdd["RBAC_Resource_Site"] = entity.RBAC_Resource_Site;

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

        public virtual bool EditRBACResource(RBACResourceInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_Resource WHERE RBAC_Resource_ID = " + entity.RBAC_Resource_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_Resource_ID"] = entity.RBAC_Resource_ID;
                    DrAdd["RBAC_Resource_GroupID"] = entity.RBAC_Resource_GroupID;
                    DrAdd["RBAC_Resource_Name"] = entity.RBAC_Resource_Name;
                    DrAdd["RBAC_Resource_Site"] = entity.RBAC_Resource_Site;

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

        public virtual int DelRBACResource(int ID)
        {
            string SqlAdd = "DELETE FROM RBAC_Resource WHERE RBAC_Resource_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual RBACResourceInfo GetRBACResourceByID(int ID)
        {
            RBACResourceInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_Resource WHERE RBAC_Resource_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACResourceInfo();

                    entity.RBAC_Resource_ID = Tools.NullInt(RdrList["RBAC_Resource_ID"]);
                    entity.RBAC_Resource_GroupID = Tools.NullInt(RdrList["RBAC_Resource_GroupID"]);
                    entity.RBAC_Resource_Name = Tools.NullStr(RdrList["RBAC_Resource_Name"]);
                    entity.RBAC_Resource_Site = Tools.NullStr(RdrList["RBAC_Resource_Site"]);

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

        public virtual IList<RBACResourceInfo> GetRBACResources(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACResourceInfo> entitys = null;
            RBACResourceInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_Resource";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<RBACResourceInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACResourceInfo();
                        entity.RBAC_Resource_ID = Tools.NullInt(RdrList["RBAC_Resource_ID"]);
                        entity.RBAC_Resource_GroupID = Tools.NullInt(RdrList["RBAC_Resource_GroupID"]);
                        entity.RBAC_Resource_Name = Tools.NullStr(RdrList["RBAC_Resource_Name"]);
                        entity.RBAC_Resource_Site = Tools.NullStr(RdrList["RBAC_Resource_Site"]);

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
                SqlTable = "RBAC_Resource";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_Resource_ID) FROM " + SqlTable + SqlParam;

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
