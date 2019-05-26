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
    public class RBACUser : IRBACUser
    {
        ITools Tools;
        ISQLHelper DBHelper;
        IRBACRole MyRole;
        public RBACUser()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
            MyRole = RBACRoleFactory.CreateRBACRole();
        }

        public virtual bool AddRBACUser(RBACUserInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_User";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["RBAC_User_GroupID"] = entity.RBAC_User_GroupID;
            DrAdd["RBAC_User_Name"] = entity.RBAC_User_Name;
            DrAdd["RBAC_User_Password"] = entity.RBAC_User_Password;
            DrAdd["RBAC_User_LastLogin"] = entity.RBAC_User_LastLogin;
            DrAdd["RBAC_User_LastLoginIP"] = entity.RBAC_User_LastLoginIP;
            DrAdd["RBAC_User_Addtime"] = entity.RBAC_User_Addtime;
            DrAdd["RBAC_User_Site"] = entity.RBAC_User_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);

                SaveUserRole(GetLastUserID(entity.RBAC_User_Name), entity.RBACRoleInfos);
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

        private void SaveUserRole(int user_id, IList<RBACRoleInfo> roleList)
        {
            if (roleList == null) { return; }
            ArrayList sqlList = new ArrayList(roleList.Count);
            sqlList.Add("DELETE FROM RBAC_UserRole WHERE RBAC_UserRole_UserID =" + user_id);
            foreach (RBACRoleInfo role in roleList)
            {
                sqlList.Add("INSERT INTO RBAC_UserRole (RBAC_UserRole_UserID, RBAC_UserRole_RoleID) VALUES (" + user_id + ",'" + role.RBAC_Role_ID + "')");
            }
            DBHelper.ExecuteNonQuery(sqlList);
        }

        private int GetLastUserID(string strName)
        {
            int Role_ID = 0;
            SqlDataReader rdr = DBHelper.ExecuteReader("SELECT RBAC_User_ID FROM RBAC_User WHERE RBAC_User_Name = '" + strName + "'");
            if (rdr.Read()) { Role_ID = Tools.NullInt(rdr[0]); }
            rdr.Close();
            rdr = null;

            return Role_ID;
        }

        public virtual bool EditRBACUser(RBACUserInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_User WHERE RBAC_User_ID = " + entity.RBAC_User_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_User_GroupID"] = entity.RBAC_User_GroupID;
                    DrAdd["RBAC_User_Name"] = entity.RBAC_User_Name;
                    DrAdd["RBAC_User_Password"] = entity.RBAC_User_Password;
                    DrAdd["RBAC_User_LastLogin"] = entity.RBAC_User_LastLogin;
                    DrAdd["RBAC_User_LastLoginIP"] = entity.RBAC_User_LastLoginIP;
                    DrAdd["RBAC_User_Addtime"] = entity.RBAC_User_Addtime;
                    DrAdd["RBAC_User_Site"] = entity.RBAC_User_Site;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                    SaveUserRole(entity.RBAC_User_ID, entity.RBACRoleInfos);
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

        public virtual int DelRBACUser(int ID)
        {
            string SqlAdd = "DELETE FROM RBAC_User WHERE RBAC_User_ID = " + ID;
            try
            {
                DBHelper.ExecuteNonQuery("DELETE FROM RBAC_UserRole WHERE RBAC_UserRole_UserID =" + ID);
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual RBACUserInfo GetRBACUserByID(int ID)
        {
            RBACUserInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_User WHERE RBAC_User_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACUserInfo();
                    entity.RBAC_User_ID = Tools.NullInt(RdrList["RBAC_User_ID"]);
                    entity.RBAC_User_GroupID = Tools.NullInt(RdrList["RBAC_User_GroupID"]);
                    entity.RBAC_User_Name = Tools.NullStr(RdrList["RBAC_User_Name"]);
                    entity.RBAC_User_Password = Tools.NullStr(RdrList["RBAC_User_Password"]);
                    entity.RBAC_User_LastLogin = Tools.NullDate(RdrList["RBAC_User_LastLogin"]);
                    entity.RBAC_User_LastLoginIP = Tools.NullStr(RdrList["RBAC_User_LastLoginIP"]);
                    entity.RBAC_User_Addtime = Tools.NullDate(RdrList["RBAC_User_Addtime"]);
                    entity.RBAC_User_Site = Tools.NullStr(RdrList["RBAC_User_Site"]);
                }
                RdrList.Close();
                RdrList = null;

                if (entity != null) { entity.RBACRoleInfos = GetRoleListByUser(entity.RBAC_User_ID); }

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

        public virtual RBACUserInfo GetRBACUserByName(string UserName)
        {
            RBACUserInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_User WHERE RBAC_User_Name = '" + UserName + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new RBACUserInfo();
                    entity.RBAC_User_ID = Tools.NullInt(RdrList["RBAC_User_ID"]);
                    entity.RBAC_User_GroupID = Tools.NullInt(RdrList["RBAC_User_GroupID"]);
                    entity.RBAC_User_Name = Tools.NullStr(RdrList["RBAC_User_Name"]);
                    entity.RBAC_User_Password = Tools.NullStr(RdrList["RBAC_User_Password"]);
                    entity.RBAC_User_LastLogin = Tools.NullDate(RdrList["RBAC_User_LastLogin"]);
                    entity.RBAC_User_LastLoginIP = Tools.NullStr(RdrList["RBAC_User_LastLoginIP"]);
                    entity.RBAC_User_Addtime = Tools.NullDate(RdrList["RBAC_User_Addtime"]);
                    entity.RBAC_User_Site = Tools.NullStr(RdrList["RBAC_User_Site"]);
                }

                RdrList.Close();
                RdrList = null;

                if (entity != null) { entity.RBACRoleInfos = GetRoleListByUser(entity.RBAC_User_ID); }

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

        public virtual IList<RBACUserInfo> GetRBACUsers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACUserInfo> entitys = null;
            RBACUserInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_User";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<RBACUserInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACUserInfo();
                        entity.RBAC_User_ID = Tools.NullInt(RdrList["RBAC_User_ID"]);
                        entity.RBAC_User_GroupID = Tools.NullInt(RdrList["RBAC_User_GroupID"]);
                        entity.RBAC_User_Name = Tools.NullStr(RdrList["RBAC_User_Name"]);
                        entity.RBAC_User_Password = Tools.NullStr(RdrList["RBAC_User_Password"]);
                        entity.RBAC_User_LastLogin = Tools.NullDate(RdrList["RBAC_User_LastLogin"]);
                        entity.RBAC_User_LastLoginIP = Tools.NullStr(RdrList["RBAC_User_LastLoginIP"]);
                        entity.RBAC_User_Addtime = Tools.NullDate(RdrList["RBAC_User_Addtime"]);
                        entity.RBAC_User_Site = Tools.NullStr(RdrList["RBAC_User_Site"]);
                        entity.RBACRoleInfos = null;

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                RdrList.Close();
                RdrList = null;

                if (entitys != null) { foreach (RBACUserInfo UserInfo in entitys) { UserInfo.RBACRoleInfos = GetRoleListByUser(UserInfo.RBAC_User_ID); } }

            }
            catch (Exception ex) { throw ex; }
            finally { if (RdrList != null) { RdrList.Close(); RdrList = null; } }

            return entitys;
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "RBAC_User";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_User_ID) FROM " + SqlTable + SqlParam;

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

        public virtual IList<RBACRoleInfo> GetRoleListByUser(int User_ID)
        {
            IList<RBACRoleInfo> entitys = null;
            RBACRoleInfo entity = null;
            string SqlList = "SELECT A.RBAC_Role_ID, A.RBAC_Role_Name, A.RBAC_Role_Description, A.RBAC_Role_IsSystem";
            SqlList += " FROM RBAC_Role AS A INNER JOIN RBAC_UserRole AS B ON A.RBAC_Role_ID = B.RBAC_UserRole_RoleID";
            SqlList += " WHERE B.RBAC_UserRole_UserID =" + User_ID;
            DataTable Dt = DBHelper.Query(SqlList);
            if (Dt.Rows.Count > 0)
            {
                entitys = new List<RBACRoleInfo>();
                foreach (DataRow dr in Dt.Rows)
                {
                    entity = new RBACRoleInfo();
                    entity.RBAC_Role_ID = Tools.NullInt(dr["RBAC_Role_ID"]);
                    entity.RBAC_Role_Name = Tools.NullStr(dr["RBAC_Role_Name"]);
                    entity.RBAC_Role_Description = Tools.NullStr(dr["RBAC_Role_Description"]);
                    entity.RBAC_Role_IsSystem = Tools.NullInt(dr["RBAC_Role_IsSystem"]);
                    entity.RBACPrivilegeInfos = MyRole.GetPrivilegeListByRole(entity.RBAC_Role_ID);
                    entitys.Add(entity);
                    entity = null;
                }
            }
            return entitys;
        }

        public virtual bool EditUserPassword(string UserPassword, int UserID)
        {
            try
            {
                DBHelper.ExecuteNonQuery("UPDATE RBAC_User SET RBAC_User_Password = '" + UserPassword + "' WHERE RBAC_User_ID = " + UserID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class RBACUserGroup : IRBACUserGroup
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public RBACUserGroup()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddRBACUserGroup(RBACUserGroupInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM RBAC_UserGroup";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["RBAC_UserGroup_ID"] = entity.RBAC_UserGroup_ID;
            DrAdd["RBAC_UserGroup_Name"] = entity.RBAC_UserGroup_Name;
            DrAdd["RBAC_UserGroup_ParentID"] = entity.RBAC_UserGroup_ParentID;
            DrAdd["RBAC_UserGroup_Site"] = entity.RBAC_UserGroup_Site;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditRBACUserGroup(RBACUserGroupInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM RBAC_UserGroup WHERE RBAC_UserGroup_ID = " + entity.RBAC_UserGroup_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["RBAC_UserGroup_ID"] = entity.RBAC_UserGroup_ID;
                    DrAdd["RBAC_UserGroup_Name"] = entity.RBAC_UserGroup_Name;
                    DrAdd["RBAC_UserGroup_ParentID"] = entity.RBAC_UserGroup_ParentID;
                    DrAdd["RBAC_UserGroup_Site"] = entity.RBAC_UserGroup_Site;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
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

        public virtual int DelRBACUserGroup(int ID)
        {
            string SqlAdd = "DELETE FROM RBAC_UserGroup WHERE RBAC_UserGroup_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual RBACUserGroupInfo GetRBACUserGroupByID(int ID)
        {
            RBACUserGroupInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM RBAC_UserGroup WHERE RBAC_UserGroup_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new RBACUserGroupInfo();
                    entity.RBAC_UserGroup_ID = Tools.NullInt(RdrList["RBAC_UserGroup_ID"]);
                    entity.RBAC_UserGroup_Name = Tools.NullStr(RdrList["RBAC_UserGroup_Name"]);
                    entity.RBAC_UserGroup_ParentID = Tools.NullInt(RdrList["RBAC_UserGroup_ParentID"]);
                    entity.RBAC_UserGroup_Site = Tools.NullStr(RdrList["RBAC_UserGroup_Site"]);
                }
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

        public virtual IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<RBACUserGroupInfo> entitys = null;
            RBACUserGroupInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "RBAC_UserGroup";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<RBACUserGroupInfo>();
                    while (RdrList.Read())
                    {
                        entity = new RBACUserGroupInfo();
                        entity.RBAC_UserGroup_ID = Tools.NullInt(RdrList["RBAC_UserGroup_ID"]);
                        entity.RBAC_UserGroup_Name = Tools.NullStr(RdrList["RBAC_UserGroup_Name"]);
                        entity.RBAC_UserGroup_ParentID = Tools.NullInt(RdrList["RBAC_UserGroup_ParentID"]);
                        entity.RBAC_UserGroup_Site = Tools.NullStr(RdrList["RBAC_UserGroup_Site"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
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

            try {
                Page = new PageInfo();
                SqlTable = "RBAC_UserGroup";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(RBAC_UserGroup_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }

}
