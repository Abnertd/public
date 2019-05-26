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
    public class SupplierShopCss : ISupplierShopCss
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopCss()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopCss(SupplierShopCssInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Css";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Css_ID"] = entity.Shop_Css_ID;
            DrAdd["Shop_Css_Title"] = entity.Shop_Css_Title;
            DrAdd["Shop_Css_Target"] = entity.Shop_Css_Target;
            DrAdd["Shop_Css_GapColor"] = entity.Shop_Css_GapColor;
            DrAdd["Shop_Css_Img"] = entity.Shop_Css_Img;
            DrAdd["Shop_Css_IsActive"] = entity.Shop_Css_IsActive;
            DrAdd["Shop_Css_Site"] = entity.Shop_Css_Site;

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

        public virtual bool EditSupplierShopCss(SupplierShopCssInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Css WHERE Shop_Css_ID = " + entity.Shop_Css_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Css_ID"] = entity.Shop_Css_ID;
                    DrAdd["Shop_Css_Title"] = entity.Shop_Css_Title;
                    DrAdd["Shop_Css_Target"] = entity.Shop_Css_Target;
                    DrAdd["Shop_Css_GapColor"] = entity.Shop_Css_GapColor;
                    DrAdd["Shop_Css_Img"] = entity.Shop_Css_Img;
                    DrAdd["Shop_Css_IsActive"] = entity.Shop_Css_IsActive;
                    DrAdd["Shop_Css_Site"] = entity.Shop_Css_Site;

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

        public virtual int DelSupplierShopCss(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Css WHERE Shop_Css_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopCssInfo GetSupplierShopCssByID(int ID)
        {
            SupplierShopCssInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Css WHERE Shop_Css_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopCssInfo();

                    entity.Shop_Css_ID = Tools.NullInt(RdrList["Shop_Css_ID"]);
                    entity.Shop_Css_Target = Tools.NullStr(RdrList["Shop_Css_Target"]);
                    entity.Shop_Css_Title = Tools.NullStr(RdrList["Shop_Css_Title"]);
                    entity.Shop_Css_GapColor = Tools.NullStr(RdrList["Shop_Css_GapColor"]);
                    entity.Shop_Css_Img = Tools.NullStr(RdrList["Shop_Css_Img"]);
                    entity.Shop_Css_IsActive = Tools.NullInt(RdrList["Shop_Css_IsActive"]);
                    entity.Shop_Css_Site = Tools.NullStr(RdrList["Shop_Css_Site"]);

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

        public virtual IList<SupplierShopCssInfo> GetSupplierShopCsss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopCssInfo> entitys = null;
            SupplierShopCssInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Css";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopCssInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopCssInfo();
                        entity.Shop_Css_ID = Tools.NullInt(RdrList["Shop_Css_ID"]);
                        entity.Shop_Css_Target = Tools.NullStr(RdrList["Shop_Css_Target"]);
                        entity.Shop_Css_Title = Tools.NullStr(RdrList["Shop_Css_Title"]);
                        entity.Shop_Css_GapColor = Tools.NullStr(RdrList["Shop_Css_GapColor"]);
                        entity.Shop_Css_Img = Tools.NullStr(RdrList["Shop_Css_Img"]);
                        entity.Shop_Css_IsActive = Tools.NullInt(RdrList["Shop_Css_IsActive"]);
                        entity.Shop_Css_Site = Tools.NullStr(RdrList["Shop_Css_Site"]);

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
                SqlTable = "Supplier_Shop_Css";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Css_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddSupplierShopCssRelateSupplier(SupplierShopCssRelateSupplierInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Css_RelateSupplier";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Relate_ID"] = entity.Relate_ID;
            DrAdd["Relate_ShopCssID"] = entity.Relate_ShopCssID;
            DrAdd["Relate_SupplierID"] = entity.Relate_SupplierID;

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

        public virtual int DelSupplierShopCssRelateSupplierBySupplierID(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Css_RelateSupplier WHERE Relate_SupplierID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelSupplierShopCssRelateSupplierByCssID(int Css_ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Css_RelateSupplier WHERE Relate_ShopCssID = " + Css_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliers(int Relate_SupplierID)
        {
            IList<SupplierShopCssRelateSupplierInfo> entitys = null;
            SupplierShopCssRelateSupplierInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Supplier_Shop_Css_RelateSupplier where Relate_SupplierID=" + Relate_SupplierID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopCssRelateSupplierInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopCssRelateSupplierInfo();
                        entity.Relate_ID = Tools.NullInt(RdrList["Relate_ID"]);
                        entity.Relate_ShopCssID = Tools.NullInt(RdrList["Relate_ShopCssID"]);
                        entity.Relate_SupplierID = Tools.NullInt(RdrList["Relate_SupplierID"]);


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

        public virtual IList<SupplierShopCssRelateSupplierInfo> GetSupplierShopCssRelateSuppliersByCss(int Css_ID)
        {
            IList<SupplierShopCssRelateSupplierInfo> entitys = null;
            SupplierShopCssRelateSupplierInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Supplier_Shop_Css_RelateSupplier where Relate_ShopCssID=" + Css_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopCssRelateSupplierInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopCssRelateSupplierInfo();
                        entity.Relate_ID = Tools.NullInt(RdrList["Relate_ID"]);
                        entity.Relate_ShopCssID = Tools.NullInt(RdrList["Relate_ShopCssID"]);
                        entity.Relate_SupplierID = Tools.NullInt(RdrList["Relate_SupplierID"]);


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

    }

}
