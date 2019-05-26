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
    public class SupplierAddress : ISupplierAddress
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierAddress()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierAddress(SupplierAddressInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Address";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Address_ID"] = entity.Supplier_Address_ID;
            DrAdd["Supplier_Address_SupplierID"] = entity.Supplier_Address_SupplierID;
            DrAdd["Supplier_Address_Country"] = entity.Supplier_Address_Country;
            DrAdd["Supplier_Address_State"] = entity.Supplier_Address_State;
            DrAdd["Supplier_Address_City"] = entity.Supplier_Address_City;
            DrAdd["Supplier_Address_County"] = entity.Supplier_Address_County;
            DrAdd["Supplier_Address_StreetAddress"] = entity.Supplier_Address_StreetAddress;
            DrAdd["Supplier_Address_Zip"] = entity.Supplier_Address_Zip;
            DrAdd["Supplier_Address_Name"] = entity.Supplier_Address_Name;
            DrAdd["Supplier_Address_Phone_Countrycode"] = entity.Supplier_Address_Phone_Countrycode;
            DrAdd["Supplier_Address_Phone_Areacode"] = entity.Supplier_Address_Phone_Areacode;
            DrAdd["Supplier_Address_Phone_Number"] = entity.Supplier_Address_Phone_Number;
            DrAdd["Supplier_Address_Mobile"] = entity.Supplier_Address_Mobile;
            DrAdd["Supplier_Address_Site"] = entity.Supplier_Address_Site;

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

        public virtual bool EditSupplierAddress(SupplierAddressInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Address WHERE Supplier_Address_ID = " + entity.Supplier_Address_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Address_ID"] = entity.Supplier_Address_ID;
                    DrAdd["Supplier_Address_SupplierID"] = entity.Supplier_Address_SupplierID;
                    DrAdd["Supplier_Address_Country"] = entity.Supplier_Address_Country;
                    DrAdd["Supplier_Address_State"] = entity.Supplier_Address_State;
                    DrAdd["Supplier_Address_City"] = entity.Supplier_Address_City;
                    DrAdd["Supplier_Address_County"] = entity.Supplier_Address_County;
                    DrAdd["Supplier_Address_StreetAddress"] = entity.Supplier_Address_StreetAddress;
                    DrAdd["Supplier_Address_Zip"] = entity.Supplier_Address_Zip;
                    DrAdd["Supplier_Address_Name"] = entity.Supplier_Address_Name;
                    DrAdd["Supplier_Address_Phone_Countrycode"] = entity.Supplier_Address_Phone_Countrycode;
                    DrAdd["Supplier_Address_Phone_Areacode"] = entity.Supplier_Address_Phone_Areacode;
                    DrAdd["Supplier_Address_Phone_Number"] = entity.Supplier_Address_Phone_Number;
                    DrAdd["Supplier_Address_Mobile"] = entity.Supplier_Address_Mobile;
                    DrAdd["Supplier_Address_Site"] = entity.Supplier_Address_Site;

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

        public virtual int DelSupplierAddress(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Address WHERE Supplier_Address_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierAddressInfo GetSupplierAddressByID(int ID)
        {
            SupplierAddressInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Address WHERE Supplier_Address_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierAddressInfo();

                    entity.Supplier_Address_ID = Tools.NullInt(RdrList["Supplier_Address_ID"]);
                    entity.Supplier_Address_SupplierID = Tools.NullInt(RdrList["Supplier_Address_SupplierID"]);
                    entity.Supplier_Address_Country = Tools.NullStr(RdrList["Supplier_Address_Country"]);
                    entity.Supplier_Address_State = Tools.NullStr(RdrList["Supplier_Address_State"]);
                    entity.Supplier_Address_City = Tools.NullStr(RdrList["Supplier_Address_City"]);
                    entity.Supplier_Address_County = Tools.NullStr(RdrList["Supplier_Address_County"]);
                    entity.Supplier_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Address_StreetAddress"]);
                    entity.Supplier_Address_Zip = Tools.NullStr(RdrList["Supplier_Address_Zip"]);
                    entity.Supplier_Address_Name = Tools.NullStr(RdrList["Supplier_Address_Name"]);
                    entity.Supplier_Address_Phone_Countrycode = Tools.NullStr(RdrList["Supplier_Address_Phone_Countrycode"]);
                    entity.Supplier_Address_Phone_Areacode = Tools.NullStr(RdrList["Supplier_Address_Phone_Areacode"]);
                    entity.Supplier_Address_Phone_Number = Tools.NullStr(RdrList["Supplier_Address_Phone_Number"]);
                    entity.Supplier_Address_Mobile = Tools.NullStr(RdrList["Supplier_Address_Mobile"]);
                    entity.Supplier_Address_Site = Tools.NullStr(RdrList["Supplier_Address_Site"]);

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

        public virtual IList<SupplierAddressInfo> GetSupplierAddresss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierAddressInfo> entitys = null;
            SupplierAddressInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Address";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierAddressInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierAddressInfo();
                        entity.Supplier_Address_ID = Tools.NullInt(RdrList["Supplier_Address_ID"]);
                        entity.Supplier_Address_SupplierID = Tools.NullInt(RdrList["Supplier_Address_SupplierID"]);
                        entity.Supplier_Address_Country = Tools.NullStr(RdrList["Supplier_Address_Country"]);
                        entity.Supplier_Address_State = Tools.NullStr(RdrList["Supplier_Address_State"]);
                        entity.Supplier_Address_City = Tools.NullStr(RdrList["Supplier_Address_City"]);
                        entity.Supplier_Address_County = Tools.NullStr(RdrList["Supplier_Address_County"]);
                        entity.Supplier_Address_StreetAddress = Tools.NullStr(RdrList["Supplier_Address_StreetAddress"]);
                        entity.Supplier_Address_Zip = Tools.NullStr(RdrList["Supplier_Address_Zip"]);
                        entity.Supplier_Address_Name = Tools.NullStr(RdrList["Supplier_Address_Name"]);
                        entity.Supplier_Address_Phone_Countrycode = Tools.NullStr(RdrList["Supplier_Address_Phone_Countrycode"]);
                        entity.Supplier_Address_Phone_Areacode = Tools.NullStr(RdrList["Supplier_Address_Phone_Areacode"]);
                        entity.Supplier_Address_Phone_Number = Tools.NullStr(RdrList["Supplier_Address_Phone_Number"]);
                        entity.Supplier_Address_Mobile = Tools.NullStr(RdrList["Supplier_Address_Mobile"]);
                        entity.Supplier_Address_Site = Tools.NullStr(RdrList["Supplier_Address_Site"]);

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
                SqlTable = "Supplier_Address";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Address_ID) FROM " + SqlTable + SqlParam;

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
