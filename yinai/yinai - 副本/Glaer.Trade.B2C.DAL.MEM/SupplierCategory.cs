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
    public class SupplierCategory : ISupplierCategory
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCategory()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        #region 产品分组定义
        public virtual bool AddSupplierCategory(SupplierCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Category";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Cate_ID"] = entity.Supplier_Cate_ID;
            DrAdd["Supplier_Cate_Name"] = entity.Supplier_Cate_Name;
            DrAdd["Supplier_Cate_Parentid"] = entity.Supplier_Cate_Parentid;
            DrAdd["Supplier_Cate_SupplierID"] = entity.Supplier_Cate_SupplierID;
            DrAdd["Supplier_Cate_Sort"] = entity.Supplier_Cate_Sort;
            DrAdd["Supplier_Cate_Site"] = entity.Supplier_Cate_Site;

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

        public virtual bool EditSupplierCategory(SupplierCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Category WHERE Supplier_Cate_ID = " + entity.Supplier_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Cate_ID"] = entity.Supplier_Cate_ID;
                    DrAdd["Supplier_Cate_Name"] = entity.Supplier_Cate_Name;
                    DrAdd["Supplier_Cate_Parentid"] = entity.Supplier_Cate_Parentid;
                    DrAdd["Supplier_Cate_SupplierID"] = entity.Supplier_Cate_SupplierID;
                    DrAdd["Supplier_Cate_Sort"] = entity.Supplier_Cate_Sort;
                    DrAdd["Supplier_Cate_Site"] = entity.Supplier_Cate_Site;

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

        public virtual int DelSupplierCategory(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Category WHERE Supplier_Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCategoryInfo GetSupplierCategoryByID(int ID)
        {
            SupplierCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Category WHERE Supplier_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCategoryInfo();

                    entity.Supplier_Cate_ID = Tools.NullInt(RdrList["Supplier_Cate_ID"]);
                    entity.Supplier_Cate_Name = Tools.NullStr(RdrList["Supplier_Cate_Name"]);
                    entity.Supplier_Cate_Parentid = Tools.NullInt(RdrList["Supplier_Cate_Parentid"]);
                    entity.Supplier_Cate_SupplierID = Tools.NullInt(RdrList["Supplier_Cate_SupplierID"]);
                    entity.Supplier_Cate_Sort = Tools.NullInt(RdrList["Supplier_Cate_Sort"]);
                    entity.Supplier_Cate_Site = Tools.NullStr(RdrList["Supplier_Cate_Site"]);

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

        public virtual SupplierCategoryInfo GetSupplierCategoryByIDSupplier(int ID,int Supplier_ID)
        {
            SupplierCategoryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Category WHERE Supplier_Cate_SupplierID=" + Supplier_ID + " And Supplier_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCategoryInfo();

                    entity.Supplier_Cate_ID = Tools.NullInt(RdrList["Supplier_Cate_ID"]);
                    entity.Supplier_Cate_Name = Tools.NullStr(RdrList["Supplier_Cate_Name"]);
                    entity.Supplier_Cate_Parentid = Tools.NullInt(RdrList["Supplier_Cate_Parentid"]);
                    entity.Supplier_Cate_SupplierID = Tools.NullInt(RdrList["Supplier_Cate_SupplierID"]);
                    entity.Supplier_Cate_Sort = Tools.NullInt(RdrList["Supplier_Cate_Sort"]);
                    entity.Supplier_Cate_Site = Tools.NullStr(RdrList["Supplier_Cate_Site"]);

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

        public virtual IList<SupplierCategoryInfo> GetSupplierCategorys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCategoryInfo> entitys = null;
            SupplierCategoryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Category";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCategoryInfo();
                        entity.Supplier_Cate_ID = Tools.NullInt(RdrList["Supplier_Cate_ID"]);
                        entity.Supplier_Cate_Name = Tools.NullStr(RdrList["Supplier_Cate_Name"]);
                        entity.Supplier_Cate_Parentid = Tools.NullInt(RdrList["Supplier_Cate_Parentid"]);
                        entity.Supplier_Cate_SupplierID = Tools.NullInt(RdrList["Supplier_Cate_SupplierID"]);
                        entity.Supplier_Cate_Sort = Tools.NullInt(RdrList["Supplier_Cate_Sort"]);
                        entity.Supplier_Cate_Site = Tools.NullStr(RdrList["Supplier_Cate_Site"]);

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
                SqlTable = "Supplier_Category";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Cate_ID) FROM " + SqlTable + SqlParam;

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
        #endregion

        #region 产品/产品分组关系
        
        public virtual bool AddSupplierProductCategory(SupplierProductCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Product_Category";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Product_Cate_ID"] = entity.Supplier_Product_Cate_ID;
            DrAdd["Supplier_Product_Cate_CateID"] = entity.Supplier_Product_Cate_CateID;
            DrAdd["Supplier_Product_Cate_ProductID"] = entity.Supplier_Product_Cate_ProductID;

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

        public virtual bool EditSupplierProductCategory(SupplierProductCategoryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Product_Category WHERE Supplier_Product_Cate_ID = " + entity.Supplier_Product_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Product_Cate_ID"] = entity.Supplier_Product_Cate_ID;
                    DrAdd["Supplier_Product_Cate_CateID"] = entity.Supplier_Product_Cate_CateID;
                    DrAdd["Supplier_Product_Cate_ProductID"] = entity.Supplier_Product_Cate_ProductID;

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

        /// <summary>
        /// 删除指定产品的产品/分组关系
        /// </summary>
        /// <param name="ID">产品编号</param>
        /// <returns></returns>
        public virtual int DelSupplierProductCategoryByProductID(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Product_Category WHERE Supplier_Product_Cate_ProductID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除指定店铺分组的产品/分组关系
        /// </summary>
        /// <param name="ID">产品编号</param>
        /// <returns></returns>
        public virtual int DelSupplierProductCategoryByCateID(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Product_Category WHERE Supplier_Product_Cate_CateID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据产品编号获取所属店铺产品分组
        /// </summary>
        /// <param name="Product_ID">产品编号</param>
        /// <returns>产品/产品分组关系</returns>
        public virtual IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByProductID(int Product_ID)
        {
            IList<SupplierProductCategoryInfo> entitys = null;
            SupplierProductCategoryInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Supplier_Product_Category WHERE Supplier_Product_Cate_ProductID = " + Product_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierProductCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierProductCategoryInfo();
                        entity.Supplier_Product_Cate_ID = Tools.NullInt(RdrList["Supplier_Product_Cate_ID"]);
                        entity.Supplier_Product_Cate_CateID = Tools.NullInt(RdrList["Supplier_Product_Cate_CateID"]);
                        entity.Supplier_Product_Cate_ProductID = Tools.NullInt(RdrList["Supplier_Product_Cate_ProductID"]);

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

        /// <summary>
        /// 根据店铺产品分组编号获取相关产品
        /// </summary>
        /// <param name="Product_ID">店铺产品分组编号</param>
        /// <returns>产品/产品分组关系</returns>
        public virtual IList<SupplierProductCategoryInfo> GetSupplierProductCategorysByCateID(int Cate_ID)
        {
            IList<SupplierProductCategoryInfo> entitys = null;
            SupplierProductCategoryInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT * FROM Supplier_Product_Category WHERE Supplier_Product_Cate_CateID = " + Cate_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierProductCategoryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierProductCategoryInfo();
                        entity.Supplier_Product_Cate_ID = Tools.NullInt(RdrList["Supplier_Product_Cate_ID"]);
                        entity.Supplier_Product_Cate_CateID = Tools.NullInt(RdrList["Supplier_Product_Cate_CateID"]);
                        entity.Supplier_Product_Cate_ProductID = Tools.NullInt(RdrList["Supplier_Product_Cate_ProductID"]);

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

        /// <summary>
        /// 根据店铺产品分组编号获取相关产品
        /// </summary>
        /// <param name="Product_ID">店铺产品分组编号</param>
        /// <returns>产品编号</returns>
        public virtual string GetSupplierProductCategorysByCateArry(string Cate_ID)
        {
            IList<SupplierProductCategoryInfo> entitys = null;
            string SqlList;
            string product_arry = "";
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "SELECT distinct Supplier_Product_Cate_ProductID FROM Supplier_Product_Category WHERE Supplier_Product_Cate_CateID in (" + Cate_ID + ")";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierProductCategoryInfo>();
                    while (RdrList.Read())
                    {
                        if (product_arry.Length > 0)
                        {
                            product_arry = product_arry + "," + Tools.NullInt(RdrList["Supplier_Product_Cate_ProductID"]).ToString();
                        }
                        else
                        {
                            product_arry =  Tools.NullInt(RdrList["Supplier_Product_Cate_ProductID"]).ToString();
                        }
                    }
                }
                return product_arry;
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

        #endregion
    }



}
