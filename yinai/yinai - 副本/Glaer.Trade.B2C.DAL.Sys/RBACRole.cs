using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;


namespace Glaer.Trade.B2C.DAL.Sys
{
    public class RBACRole : IRBACRole
    {
        ITools Tools;
        ISQLHelper DBHelper;

        public RBACRole()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddRBACRole(RBACRoleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_Role";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["RBAC_Role_ID"] = entity.RBAC_Role_ID;
            DrAdd["RBAC_Role_Name"] = entity.RBAC_Role_Name;
            DrAdd["RBAC_Role_Description"] = entity.RBAC_Role_Description;
            DrAdd["RBAC_Role_IsSystem"] = entity.RBAC_Role_IsSystem;
            DrAdd["RBAC_Role_Site"] = entity.RBAC_Role_Site;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);

                SaveRolePrivilege(GetLastRoleID(entity.RBAC_Role_Name), entity.RBACPrivilegeInfos);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        private void SaveRolePrivilege(int role_id, IList<RBACPrivilegeInfo> privilegeList)
        {
            if (privilegeList == null) { return; }
            ArrayList sqlList = new ArrayList(privilegeList.Count);
            sqlList.Add("DELETE FROM RBAC_RolePrivilege WHERE RBAC_RolePrivilege_RoleID =" + role_id);
            foreach (RBACPrivilegeInfo privilege in privilegeList)
            {
                sqlList.Add("INSERT INTO RBAC_RolePrivilege (RBAC_RolePrivilege_RoleID, RBAC_RolePrivilege_PrivilegeID) VALUES (" + role_id + ",'" + privilege.RBAC_Privilege_ID + "')");
            }
            DBHelper.ExecuteNonQuery(sqlList);
        }

        private int GetLastRoleID(string role_name)
        {
            int Role_ID = 0;
            SqlDataReader rdr = DBHelper.ExecuteReader("SELECT RBAC_Role_ID FROM RBAC_Role WHERE RBAC_Role_Name = '"+ role_name +"'");
            if (rdr.Read()) { Role_ID = Tools.NullInt(rdr[0]); }
            rdr.Close();
            rdr = null;

            return Role_ID;
        }

        public virtual bool EditRBACRole(RBACRoleInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_Role WHERE RBAC_Role_ID = " + entity.RBAC_Role_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_Role_ID"] = entity.RBAC_Role_ID;
                    DrAdd["RBAC_Role_Name"] = entity.RBAC_Role_Name;
                    DrAdd["RBAC_Role_Description"] = entity.RBAC_Role_Description;
                    DrAdd["RBAC_Role_IsSystem"] = entity.RBAC_Role_IsSystem;
                    DrAdd["RBAC_Role_Site"] = entity.RBAC_Role_Site;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                    SaveRolePrivilege(entity.RBAC_Role_ID, entity.RBACPrivilegeInfos);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelRBACRole(int ID)
        {
            string SqlAdd = "DELETE FROM RBAC_Role WHERE RBAC_Role_ID = " + ID;
            try {
                DBHelper.ExecuteNonQuery("DELETE FROM RBAC_RolePrivilege WHERE RBAC_RolePrivilege_RoleID =" + ID);
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual RBACRoleInfo GetRBACRoleByID(int ID)
        {
            RBACRoleInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_Role WHERE RBAC_Role_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACRoleInfo();
                    entity.RBAC_Role_ID = Tools.NullInt(RdrList["RBAC_Role_ID"]);
                    entity.RBAC_Role_Name = Tools.NullStr(RdrList["RBAC_Role_Name"]);
                    entity.RBAC_Role_Description = Tools.NullStr(RdrList["RBAC_Role_Description"]);
                    entity.RBAC_Role_IsSystem = Tools.NullInt(RdrList["RBAC_Role_IsSystem"]);
                    entity.RBAC_Role_Site = Tools.NullStr(RdrList["RBAC_Role_Site"]);
                    entity.RBACPrivilegeInfos = null;
                }

                RdrList.Close();
                RdrList = null;

                if (entity != null) { entity.RBACPrivilegeInfos = GetPrivilegeListByRole(entity.RBAC_Role_ID); }

                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<RBACRoleInfo> GetRBACRoles(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACRoleInfo> entitys = null;
            RBACRoleInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_Role";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<RBACRoleInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACRoleInfo();
                        entity.RBAC_Role_ID = Tools.NullInt(RdrList["RBAC_Role_ID"]);
                        entity.RBAC_Role_Name = Tools.NullStr(RdrList["RBAC_Role_Name"]);
                        entity.RBAC_Role_Description = Tools.NullStr(RdrList["RBAC_Role_Description"]);
                        entity.RBAC_Role_IsSystem = Tools.NullInt(RdrList["RBAC_Role_IsSystem"]);
                        entity.RBAC_Role_Site = Tools.NullStr(RdrList["RBAC_Role_Site"]);
                        entity.RBACPrivilegeInfos = null;
                        entitys.Add(entity);
                        entity = null;
                    }
                }

                if (entitys != null) { foreach (RBACRoleInfo RoleInfo in entitys) { RoleInfo.RBACPrivilegeInfos = GetPrivilegeListByRole(RoleInfo.RBAC_Role_ID); } }

                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
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
                SqlTable = "RBAC_Role";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_Role_ID) FROM " + SqlTable + SqlParam;

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

        public virtual IList<RBACPrivilegeInfo> GetPrivilegeListByRole(int Role_ID)
        {
            IList<RBACPrivilegeInfo> entitys = null;
            RBACPrivilegeInfo entity = null;
            string SqlList = "SELECT A.RBAC_Privilege_ID, A.RBAC_Privilege_ResourceID, A.RBAC_Privilege_Name";
            SqlList += " FROM RBAC_Privilege AS A INNER JOIN RBAC_RolePrivilege AS B ON A.RBAC_Privilege_ID = B.RBAC_RolePrivilege_PrivilegeID";
            SqlList += " WHERE B.RBAC_RolePrivilege_RoleID =" + Role_ID;
            DataTable Dt = DBHelper.Query(SqlList);
            if (Dt.Rows.Count > 0) {
                entitys = new List<RBACPrivilegeInfo>();
                foreach (DataRow dr in Dt.Rows) {
                    entity = new RBACPrivilegeInfo();
                    entity.RBAC_Privilege_ID = Tools.NullStr(dr["RBAC_Privilege_ID"]);
                    entity.RBAC_Privilege_ResourceID = Tools.NullInt(dr["RBAC_Privilege_ResourceID"]);
                    entity.RBAC_Privilege_Name = Tools.NullStr(dr["RBAC_Privilege_Name"]);
                    entitys.Add(entity);
                    entity = null;
                }
            }
            return entitys;
        }

    }

}
