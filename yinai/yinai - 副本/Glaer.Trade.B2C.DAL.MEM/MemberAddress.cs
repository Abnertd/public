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
    public class MemberAddress : IMemberAddress
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberAddress()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberAddress(MemberAddressInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Address";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Address_ID"] = entity.Member_Address_ID;
            DrAdd["Member_Address_MemberID"] = entity.Member_Address_MemberID;
            DrAdd["Member_Address_Country"] = entity.Member_Address_Country;
            DrAdd["Member_Address_State"] = entity.Member_Address_State;
            DrAdd["Member_Address_City"] = entity.Member_Address_City;
            DrAdd["Member_Address_County"] = entity.Member_Address_County;
            DrAdd["Member_Address_StreetAddress"] = entity.Member_Address_StreetAddress;
            DrAdd["Member_Address_Zip"] = entity.Member_Address_Zip;
            DrAdd["Member_Address_Name"] = entity.Member_Address_Name;
            DrAdd["Member_Address_Phone_Countrycode"] = entity.Member_Address_Phone_Countrycode;
            DrAdd["Member_Address_Phone_Areacode"] = entity.Member_Address_Phone_Areacode;
            DrAdd["Member_Address_Phone_Number"] = entity.Member_Address_Phone_Number;
            DrAdd["Member_Address_Mobile"] = entity.Member_Address_Mobile;
            DrAdd["Member_Address_IsDefault"] = entity.Member_Address_IsDefault;
            DrAdd["Member_Address_Site"] = entity.Member_Address_Site;

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

        public virtual bool EditMemberAddress(MemberAddressInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Address WHERE Member_Address_ID = " + entity.Member_Address_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_Address_ID"] = entity.Member_Address_ID;
                    DrAdd["Member_Address_MemberID"] = entity.Member_Address_MemberID;
                    DrAdd["Member_Address_Country"] = entity.Member_Address_Country;
                    DrAdd["Member_Address_State"] = entity.Member_Address_State;
                    DrAdd["Member_Address_City"] = entity.Member_Address_City;
                    DrAdd["Member_Address_County"] = entity.Member_Address_County;
                    DrAdd["Member_Address_StreetAddress"] = entity.Member_Address_StreetAddress;
                    DrAdd["Member_Address_Zip"] = entity.Member_Address_Zip;
                    DrAdd["Member_Address_Name"] = entity.Member_Address_Name;
                    DrAdd["Member_Address_Phone_Countrycode"] = entity.Member_Address_Phone_Countrycode;
                    DrAdd["Member_Address_Phone_Areacode"] = entity.Member_Address_Phone_Areacode;
                    DrAdd["Member_Address_Phone_Number"] = entity.Member_Address_Phone_Number;
                    DrAdd["Member_Address_Mobile"] = entity.Member_Address_Mobile;
                    DrAdd["Member_Address_IsDefault"] = entity.Member_Address_IsDefault;
                    DrAdd["Member_Address_Site"] = entity.Member_Address_Site;

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

        public virtual int DelMemberAddress(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Address WHERE Member_Address_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberAddressInfo GetMemberAddressByID(int ID)
        {
            MemberAddressInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Address WHERE Member_Address_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberAddressInfo();

                    entity.Member_Address_ID = Tools.NullInt(RdrList["Member_Address_ID"]);
                    entity.Member_Address_MemberID = Tools.NullInt(RdrList["Member_Address_MemberID"]);
                    entity.Member_Address_Country = Tools.NullStr(RdrList["Member_Address_Country"]);
                    entity.Member_Address_State = Tools.NullStr(RdrList["Member_Address_State"]);
                    entity.Member_Address_City = Tools.NullStr(RdrList["Member_Address_City"]);
                    entity.Member_Address_County = Tools.NullStr(RdrList["Member_Address_County"]);
                    entity.Member_Address_StreetAddress = Tools.NullStr(RdrList["Member_Address_StreetAddress"]);
                    entity.Member_Address_Zip = Tools.NullStr(RdrList["Member_Address_Zip"]);
                    entity.Member_Address_Name = Tools.NullStr(RdrList["Member_Address_Name"]);
                    entity.Member_Address_Phone_Countrycode = Tools.NullStr(RdrList["Member_Address_Phone_Countrycode"]);
                    entity.Member_Address_Phone_Areacode = Tools.NullStr(RdrList["Member_Address_Phone_Areacode"]);
                    entity.Member_Address_Phone_Number = Tools.NullStr(RdrList["Member_Address_Phone_Number"]);
                    entity.Member_Address_Mobile = Tools.NullStr(RdrList["Member_Address_Mobile"]);
                    entity.Member_Address_IsDefault = Tools.NullInt(RdrList["Member_Address_IsDefault"]);
                    entity.Member_Address_Site = Tools.NullStr(RdrList["Member_Address_Site"]);

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

        public virtual IList<MemberAddressInfo> GetMemberAddresss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberAddressInfo> entitys = null;
            MemberAddressInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Address";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberAddressInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberAddressInfo();
                        entity.Member_Address_ID = Tools.NullInt(RdrList["Member_Address_ID"]);
                        entity.Member_Address_MemberID = Tools.NullInt(RdrList["Member_Address_MemberID"]);
                        entity.Member_Address_Country = Tools.NullStr(RdrList["Member_Address_Country"]);
                        entity.Member_Address_State = Tools.NullStr(RdrList["Member_Address_State"]);
                        entity.Member_Address_City = Tools.NullStr(RdrList["Member_Address_City"]);
                        entity.Member_Address_County = Tools.NullStr(RdrList["Member_Address_County"]);
                        entity.Member_Address_StreetAddress = Tools.NullStr(RdrList["Member_Address_StreetAddress"]);
                        entity.Member_Address_Zip = Tools.NullStr(RdrList["Member_Address_Zip"]);
                        entity.Member_Address_Name = Tools.NullStr(RdrList["Member_Address_Name"]);
                        entity.Member_Address_Phone_Countrycode = Tools.NullStr(RdrList["Member_Address_Phone_Countrycode"]);
                        entity.Member_Address_Phone_Areacode = Tools.NullStr(RdrList["Member_Address_Phone_Areacode"]);
                        entity.Member_Address_Phone_Number = Tools.NullStr(RdrList["Member_Address_Phone_Number"]);
                        entity.Member_Address_Mobile = Tools.NullStr(RdrList["Member_Address_Mobile"]);
                        entity.Member_Address_IsDefault = Tools.NullInt(RdrList["Member_Address_IsDefault"]);
                        entity.Member_Address_Site = Tools.NullStr(RdrList["Member_Address_Site"]);

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
                SqlTable = "Member_Address";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_Address_ID) FROM " + SqlTable + SqlParam;

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
