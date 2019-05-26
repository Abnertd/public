using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class DeliveryWay : IDeliveryWay
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public DeliveryWay()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddDeliveryWay(DeliveryWayInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Delivery_Way";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Delivery_Way_ID"] = entity.Delivery_Way_ID;
            DrAdd["Delivery_Way_SupplierID"] = entity.Delivery_Way_SupplierID;
            DrAdd["Delivery_Way_Name"] = entity.Delivery_Way_Name;
            DrAdd["Delivery_Way_Sort"] = entity.Delivery_Way_Sort;
            DrAdd["Delivery_Way_InitialWeight"] = entity.Delivery_Way_InitialWeight;
            DrAdd["Delivery_Way_UpWeight"] = entity.Delivery_Way_UpWeight;
            DrAdd["Delivery_Way_FeeType"] = entity.Delivery_Way_FeeType;
            DrAdd["Delivery_Way_Fee"] = entity.Delivery_Way_Fee;
            DrAdd["Delivery_Way_InitialFee"] = entity.Delivery_Way_InitialFee;
            DrAdd["Delivery_Way_UpFee"] = entity.Delivery_Way_UpFee;
            DrAdd["Delivery_Way_Status"] = entity.Delivery_Way_Status;
            DrAdd["Delivery_Way_Cod"] = entity.Delivery_Way_Cod;
            DrAdd["Delivery_Way_Img"] = entity.Delivery_Way_Img;
            DrAdd["Delivery_Way_Url"] = entity.Delivery_Way_Url;
            DrAdd["Delivery_Way_Intro"] = entity.Delivery_Way_Intro;
            DrAdd["Delivery_Way_Site"] = entity.Delivery_Way_Site;
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

        public virtual bool EditDeliveryWay(DeliveryWayInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Delivery_Way WHERE Delivery_Way_ID = " + entity.Delivery_Way_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Delivery_Way_ID"] = entity.Delivery_Way_ID;
                    DrAdd["Delivery_Way_SupplierID"] = entity.Delivery_Way_SupplierID;
                    DrAdd["Delivery_Way_Name"] = entity.Delivery_Way_Name;
                    DrAdd["Delivery_Way_Sort"] = entity.Delivery_Way_Sort;
                    DrAdd["Delivery_Way_InitialWeight"] = entity.Delivery_Way_InitialWeight;
                    DrAdd["Delivery_Way_UpWeight"] = entity.Delivery_Way_UpWeight;
                    DrAdd["Delivery_Way_FeeType"] = entity.Delivery_Way_FeeType;
                    DrAdd["Delivery_Way_Fee"] = entity.Delivery_Way_Fee;
                    DrAdd["Delivery_Way_InitialFee"] = entity.Delivery_Way_InitialFee;
                    DrAdd["Delivery_Way_UpFee"] = entity.Delivery_Way_UpFee;
                    DrAdd["Delivery_Way_Status"] = entity.Delivery_Way_Status;
                    DrAdd["Delivery_Way_Cod"] = entity.Delivery_Way_Cod;
                    DrAdd["Delivery_Way_Img"] = entity.Delivery_Way_Img;
                    DrAdd["Delivery_Way_Url"] = entity.Delivery_Way_Url;
                    DrAdd["Delivery_Way_Intro"] = entity.Delivery_Way_Intro;
                    DrAdd["Delivery_Way_Site"] = entity.Delivery_Way_Site;
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

        public virtual int DelDeliveryWay(int ID)
        {
            string SqlAdd = "DELETE FROM Delivery_Way WHERE Delivery_Way_ID = " + ID;

            try {
                DBHelper.ExecuteNonQuery("DELETE FROM Delivery_Way_District WHERE District_DeliveryWayID = " + ID);
                DBHelper.ExecuteNonQuery("DELETE FROM Supplier_Delivery_Fee WHERE Supplier_Delivery_Fee_DeliveryID = " + ID);
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual DeliveryWayInfo GetDeliveryWayByID(int ID)
        {
            DeliveryWayInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Delivery_Way WHERE Delivery_Way_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new DeliveryWayInfo();
                    entity.Delivery_Way_ID = Tools.NullInt(RdrList["Delivery_Way_ID"]);
                    entity.Delivery_Way_SupplierID = Tools.NullInt(RdrList["Delivery_Way_SupplierID"]);
                    entity.Delivery_Way_Name = Tools.NullStr(RdrList["Delivery_Way_Name"]);
                    entity.Delivery_Way_Sort = Tools.NullInt(RdrList["Delivery_Way_Sort"]);
                    entity.Delivery_Way_InitialWeight = Tools.NullInt(RdrList["Delivery_Way_InitialWeight"]);
                    entity.Delivery_Way_UpWeight = Tools.NullInt(RdrList["Delivery_Way_UpWeight"]);
                    entity.Delivery_Way_FeeType = Tools.NullInt(RdrList["Delivery_Way_FeeType"]);
                    entity.Delivery_Way_Fee = Tools.NullDbl(RdrList["Delivery_Way_Fee"]);
                    entity.Delivery_Way_InitialFee = Tools.NullDbl(RdrList["Delivery_Way_InitialFee"]);
                    entity.Delivery_Way_UpFee = Tools.NullDbl(RdrList["Delivery_Way_UpFee"]);
                    entity.Delivery_Way_Status = Tools.NullInt(RdrList["Delivery_Way_Status"]);
                    entity.Delivery_Way_Cod = Tools.NullInt(RdrList["Delivery_Way_Cod"]);
                    entity.Delivery_Way_Img = Tools.NullStr(RdrList["Delivery_Way_Img"]);
                    entity.Delivery_Way_Url = Tools.NullStr(RdrList["Delivery_Way_Url"]);
                    entity.Delivery_Way_Intro = Tools.NullStr(RdrList["Delivery_Way_Intro"]);
                    entity.Delivery_Way_Site = Tools.NullStr(RdrList["Delivery_Way_Site"]);
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

        public virtual IList<DeliveryWayInfo> GetDeliveryWays(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<DeliveryWayInfo> entitys = null;
            DeliveryWayInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Delivery_Way";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<DeliveryWayInfo>();
                    while (RdrList.Read())
                    {
                        entity = new DeliveryWayInfo();
                        entity.Delivery_Way_ID = Tools.NullInt(RdrList["Delivery_Way_ID"]);
                        entity.Delivery_Way_SupplierID = Tools.NullInt(RdrList["Delivery_Way_SupplierID"]);
                        entity.Delivery_Way_Name = Tools.NullStr(RdrList["Delivery_Way_Name"]);
                        entity.Delivery_Way_Sort = Tools.NullInt(RdrList["Delivery_Way_Sort"]);
                        entity.Delivery_Way_InitialWeight = Tools.NullInt(RdrList["Delivery_Way_InitialWeight"]);
                        entity.Delivery_Way_UpWeight = Tools.NullInt(RdrList["Delivery_Way_UpWeight"]);
                        entity.Delivery_Way_FeeType = Tools.NullInt(RdrList["Delivery_Way_FeeType"]);
                        entity.Delivery_Way_Fee = Tools.NullDbl(RdrList["Delivery_Way_Fee"]);
                        entity.Delivery_Way_InitialFee = Tools.NullDbl(RdrList["Delivery_Way_InitialFee"]);
                        entity.Delivery_Way_UpFee = Tools.NullDbl(RdrList["Delivery_Way_UpFee"]);
                        entity.Delivery_Way_Status = Tools.NullInt(RdrList["Delivery_Way_Status"]);
                        entity.Delivery_Way_Cod = Tools.NullInt(RdrList["Delivery_Way_Cod"]);
                        entity.Delivery_Way_Img = Tools.NullStr(RdrList["Delivery_Way_Img"]);
                        entity.Delivery_Way_Url = Tools.NullStr(RdrList["Delivery_Way_Url"]);
                        entity.Delivery_Way_Intro = Tools.NullStr(RdrList["Delivery_Way_Intro"]);
                        entity.Delivery_Way_Site = Tools.NullStr(RdrList["Delivery_Way_Site"]);
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

        public virtual IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state,string city,string county)
        {
            IList<DeliveryWayInfo> entitys = null;
            DeliveryWayInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Delivery_Way where Delivery_Way_ID in (select District_DeliveryWayID from Delivery_Way_District where (District_State='' or District_State='" +state+ "') and (District_City='' or District_City='" +city+ "') and (District_County='' or District_County='" +county+ "')) and Delivery_Way_Status=1 order by Delivery_Way_Sort asc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<DeliveryWayInfo>();
                    while (RdrList.Read())
                    {
                        entity = new DeliveryWayInfo();
                        entity.Delivery_Way_ID = Tools.NullInt(RdrList["Delivery_Way_ID"]);
                        entity.Delivery_Way_SupplierID = Tools.NullInt(RdrList["Delivery_Way_SupplierID"]);
                        entity.Delivery_Way_Name = Tools.NullStr(RdrList["Delivery_Way_Name"]);
                        entity.Delivery_Way_Sort = Tools.NullInt(RdrList["Delivery_Way_Sort"]);
                        entity.Delivery_Way_InitialWeight = Tools.NullInt(RdrList["Delivery_Way_InitialWeight"]);
                        entity.Delivery_Way_UpWeight = Tools.NullInt(RdrList["Delivery_Way_UpWeight"]);
                        entity.Delivery_Way_FeeType = Tools.NullInt(RdrList["Delivery_Way_FeeType"]);
                        entity.Delivery_Way_Fee = Tools.NullDbl(RdrList["Delivery_Way_Fee"]);
                        entity.Delivery_Way_InitialFee = Tools.NullDbl(RdrList["Delivery_Way_InitialFee"]);
                        entity.Delivery_Way_UpFee = Tools.NullDbl(RdrList["Delivery_Way_UpFee"]);
                        entity.Delivery_Way_Status = Tools.NullInt(RdrList["Delivery_Way_Status"]);
                        entity.Delivery_Way_Cod = Tools.NullInt(RdrList["Delivery_Way_Cod"]);
                        entity.Delivery_Way_Img = Tools.NullStr(RdrList["Delivery_Way_Img"]);
                        entity.Delivery_Way_Url = Tools.NullStr(RdrList["Delivery_Way_Url"]);
                        entity.Delivery_Way_Intro = Tools.NullStr(RdrList["Delivery_Way_Intro"]);
                        entity.Delivery_Way_Site = Tools.NullStr(RdrList["Delivery_Way_Site"]);
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
                SqlTable = "Delivery_Way";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Delivery_Way_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddDeliveryWayDistrict(DeliveryWayDistrictInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Delivery_Way_District";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["District_ID"] = entity.District_ID;
            DrAdd["District_DeliveryWayID"] = entity.District_DeliveryWayID;
            DrAdd["District_Country"] = entity.District_Country;
            DrAdd["District_State"] = entity.District_State;
            DrAdd["District_City"] = entity.District_City;
            DrAdd["District_County"] = entity.District_County;

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

        public virtual bool EditDeliveryWayDistrict(DeliveryWayDistrictInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Delivery_Way_District WHERE District_ID = " + entity.District_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["District_ID"] = entity.District_ID;
                    DrAdd["District_DeliveryWayID"] = entity.District_DeliveryWayID;
                    DrAdd["District_Country"] = entity.District_Country;
                    DrAdd["District_State"] = entity.District_State;
                    DrAdd["District_City"] = entity.District_City;
                    DrAdd["District_County"] = entity.District_County;
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

        public virtual int DelDeliveryWayDistrict(int ID)
        {
            string SqlAdd = "DELETE FROM Delivery_Way_District WHERE District_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual DeliveryWayDistrictInfo GetDeliveryWayDistrictByID(int ID)
        {
            DeliveryWayDistrictInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Delivery_Way_District WHERE District_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new DeliveryWayDistrictInfo();
                    entity.District_ID = Tools.NullInt(RdrList["District_ID"]);
                    entity.District_DeliveryWayID = Tools.NullInt(RdrList["District_DeliveryWayID"]);
                    entity.District_Country = Tools.NullStr(RdrList["District_Country"]);
                    entity.District_State = Tools.NullStr(RdrList["District_State"]);
                    entity.District_City = Tools.NullStr(RdrList["District_City"]);
                    entity.District_County = Tools.NullStr(RdrList["District_County"]);
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

        public virtual IList<DeliveryWayDistrictInfo> GetDeliveryWayDistrictsByDWID(int ID)
        {
            IList<DeliveryWayDistrictInfo> entitys = null;
            DeliveryWayDistrictInfo entity = null;
            SqlDataReader RdrList = null;
            string SqlList;
            try {
                SqlList = "SELECT * FROM Delivery_Way_District WHERE District_DeliveryWayID = " + ID + " ORDER BY District_ID DESC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<DeliveryWayDistrictInfo>();
                    while (RdrList.Read()) {
                        entity = new DeliveryWayDistrictInfo();
                        entity.District_ID = Tools.NullInt(RdrList["District_ID"]);
                        entity.District_DeliveryWayID = Tools.NullInt(RdrList["District_DeliveryWayID"]);
                        entity.District_Country = Tools.NullStr(RdrList["District_Country"]);
                        entity.District_State = Tools.NullStr(RdrList["District_State"]);
                        entity.District_City = Tools.NullStr(RdrList["District_City"]);
                        entity.District_County = Tools.NullStr(RdrList["District_County"]);
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
                RdrList.Close();
                RdrList = null;
            }
        }

    }

}
