﻿using System;
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
    public class SupplierConsumption : ISupplierConsumption
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierConsumption()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierConsumption(SupplierConsumptionInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Consumption";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Consump_ID"] = entity.Consump_ID;
            DrAdd["Consump_SupplierID"] = entity.Consump_SupplierID;
            DrAdd["Consump_CoinRemain"] = entity.Consump_CoinRemain;
            DrAdd["Consump_Coin"] = entity.Consump_Coin;
            DrAdd["Consump_Reason"] = entity.Consump_Reason;
            DrAdd["Consump_Addtime"] = entity.Consump_Addtime;

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


        public virtual int DelSupplierConsumption(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Consumption WHERE Consump_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierConsumptionInfo> GetSupplierConsumptions(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierConsumptionInfo> entitys = null;
            SupplierConsumptionInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Consumption";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierConsumptionInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierConsumptionInfo();
                        entity.Consump_ID = Tools.NullInt(RdrList["Consump_ID"]);
                        entity.Consump_SupplierID = Tools.NullInt(RdrList["Consump_SupplierID"]);
                        entity.Consump_CoinRemain = Tools.NullInt(RdrList["Consump_CoinRemain"]);
                        entity.Consump_Coin = Tools.NullInt(RdrList["Consump_Coin"]);
                        entity.Consump_Reason = Tools.NullStr(RdrList["Consump_Reason"]);
                        entity.Consump_Addtime = Tools.NullDate(RdrList["Consump_Addtime"]);

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
                SqlTable = "Supplier_Consumption";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Consump_ID) FROM " + SqlTable + SqlParam;

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
