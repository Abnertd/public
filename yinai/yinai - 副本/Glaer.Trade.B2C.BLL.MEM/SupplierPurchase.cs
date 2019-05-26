using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierPurchase : ISupplierPurchase
    {
        protected DAL.MEM.ISupplierPurchase MyDAL;
        protected IRBAC RBAC;

        public SupplierPurchase()
        {
            MyDAL = DAL.MEM.SupplierPurchaseFactory.CreateSupplierPurchase();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPurchase(SupplierPurchaseInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "59ac1c26-ba95-42c9-b418-fae8465f6e94"))
            {
                return MyDAL.AddSupplierPurchase(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：59ac1c26-ba95-42c9-b418-fae8465f6e94错误");
            }
        }

        public virtual bool EditSupplierPurchase(SupplierPurchaseInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "aa55fc69-156e-45fe-84fa-f0df964cd3e0"))
            {
                return MyDAL.EditSupplierPurchase(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：aa55fc69-156e-45fe-84fa-f0df964cd3e0错误");
            }
        }

        public virtual int DelSupplierPurchase(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "af3d7bc9-1182-4aea-9be5-a826be6a5615"))
            {
                return MyDAL.DelSupplierPurchase(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：af3d7bc9-1182-4aea-9be5-a826be6a5615错误");
            }
        }

        public virtual SupplierPurchaseInfo GetSupplierPurchaseByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c197743d-e397-4d11-b6fc-07d1d24aa774"))
            {
                return MyDAL.GetSupplierPurchaseByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c197743d-e397-4d11-b6fc-07d1d24aa774错误");
            }
        }

        public virtual IList<SupplierPurchaseInfo> GetSupplierPurchasesList(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c197743d-e397-4d11-b6fc-07d1d24aa774"))
            {
                return MyDAL.GetSupplierPurchasesList(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c197743d-e397-4d11-b6fc-07d1d24aa774错误");
            }
        }

        public virtual IList<SupplierPurchaseInfo> GetSupplierPurchases(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c197743d-e397-4d11-b6fc-07d1d24aa774"))
            {
                return MyDAL.GetSupplierPurchases(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c197743d-e397-4d11-b6fc-07d1d24aa774错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c197743d-e397-4d11-b6fc-07d1d24aa774"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c197743d-e397-4d11-b6fc-07d1d24aa774错误");
            }
        }

        public virtual bool AddSupplierPurchasePrivate(SupplierPurchasePrivateInfo entity)
        {
            return MyDAL.AddSupplierPurchasePrivate(entity);
        }

        public virtual int DelSupplierPurchasePrivateByPurchase(int ID)
        {
            return MyDAL.DelSupplierPurchasePrivateByPurchase(ID);
        }

        public virtual IList<SupplierPurchasePrivateInfo> GetSupplierPurchasePrivatesByPurchase(int ID)
        {
            return MyDAL.GetSupplierPurchasePrivatesByPurchase(ID);
        }

        public virtual bool GetSupplierPurchasePrivatesByPurchaseSupplier(int PurchaseID, int SupplierID)
        {
            return MyDAL.GetSupplierPurchasePrivatesByPurchaseSupplier(PurchaseID, SupplierID);
        }
    }

    public class SupplierPurchaseDetail : ISupplierPurchaseDetail
    {
        protected DAL.MEM.ISupplierPurchaseDetail MyDAL;
        protected IRBAC RBAC;

        public SupplierPurchaseDetail()
        {
            MyDAL = DAL.MEM.SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity)
        {
            return MyDAL.AddSupplierPurchaseDetail(entity);
        }

        public virtual bool EditSupplierPurchaseDetail(SupplierPurchaseDetailInfo entity)
        {
            return MyDAL.EditSupplierPurchaseDetail(entity);
        }

        public virtual int DelSupplierPurchaseDetail(int ID)
        {
            return MyDAL.DelSupplierPurchaseDetail(ID);
        }

        public virtual int DelSupplierPurchaseDetailByPurchaseID(int Apply_ID)
        {
            return MyDAL.DelSupplierPurchaseDetailByPurchaseID(Apply_ID);
        }

        public virtual SupplierPurchaseDetailInfo GetSupplierPurchaseDetailByID(int ID)
        {
            return MyDAL.GetSupplierPurchaseDetailByID(ID);
        }

        public virtual IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetails(QueryInfo Query)
        {
            return MyDAL.GetSupplierPurchaseDetails(Query);
        }

        public virtual IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetailsByPurchaseID(int Apply_ID)
        {
            return MyDAL.GetSupplierPurchaseDetailsByPurchaseID(Apply_ID);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

    public class SupplierPurchaseCategory : ISupplierPurchaseCategory
    {
        protected DAL.MEM.ISupplierPurchaseCategory MyDAL;
        protected IRBAC RBAC;

        public SupplierPurchaseCategory()
        {
            MyDAL = DAL.MEM.SupplierPurchaseCategoryFactory.CreateSupplierPurchaseCategory();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "259477d3-f3bb-4fa7-9ef2-d0138677035b"))
            {
                return MyDAL.AddSupplierPurchaseCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：259477d3-f3bb-4fa7-9ef2-d0138677035b错误");
            }
           
        }

        public virtual bool EditSupplierPurchaseCategory(SupplierPurchaseCategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0bdbc767-9db5-4b38-ace4-6f886ddc285e"))
            {
                return MyDAL.EditSupplierPurchaseCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：50bdbc767-9db5-4b38-ace4-6f886ddc285e错误");
            }
          
        }

        public virtual int DelSupplierPurchaseCategory(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "616c185b-5b10-4bb1-b9f8-2112cf41ae6f"))
            {
                return MyDAL.DelSupplierPurchaseCategory(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：616c185b-5b10-4bb1-b9f8-2112cf41ae6f错误");
            }
         
        }

        public virtual SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7eccd02b-0624-4add-a11f-ce21d405a1d5"))
            {
                return MyDAL.GetSupplierPurchaseCategoryByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7eccd02b-0624-4add-a11f-ce21d405a1d5错误");
            }
 
        }

        public virtual IList<SupplierPurchaseCategoryInfo> GetSupplierPurchaseCategorys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7eccd02b-0624-4add-a11f-ce21d405a1d5"))
            {
                return MyDAL.GetSupplierPurchaseCategorys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7eccd02b-0624-4add-a11f-ce21d405a1d5错误");
            }
          
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7eccd02b-0624-4add-a11f-ce21d405a1d5"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7eccd02b-0624-4add-a11f-ce21d405a1d5错误");
            }
   
        }



        public virtual IList<SupplierPurchaseCategoryInfo> GetSubSupplierPurchaseCategorys(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetSubSupplierPurchaseCategorys(Cate_ID, SiteSign);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual string SelectSupplierPurchaseCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID, string SiteSign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7eccd02b-0624-4add-a11f-ce21d405a1d5"))
            {

            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7eccd02b-0624-4add-a11f-ce21d405a1d5错误");
            }
            string strHTML = "";
            IList<SupplierPurchaseCategoryInfo> entitys = GetSubSupplierPurchaseCategorys(Cate_ID, SiteSign, UserPrivilege);
            if (entitys != null)
            {
                appendTag += "&nbsp;&nbsp;";
                foreach (SupplierPurchaseCategoryInfo entity in entitys)
                {
                    if (shieldID != entity.Cate_ID)
                    {
                        if (entity.Cate_ID == selectVal)
                        {
                            strHTML += "<option value=\"" + entity.Cate_ID + "\" selected=\"selected\">" + appendTag + entity.Cate_Name + "</option>";
                        }
                        else
                        {
                            strHTML += "<option value=\"" + entity.Cate_ID + "\">" + appendTag + entity.Cate_Name + "</option>";
                        }
                        strHTML += SelectSupplierPurchaseCategoryOption(entity.Cate_ID, selectVal, appendTag, shieldID, SiteSign, UserPrivilege);
                    }
                }
            }
            return strHTML;
        }

        public virtual string DisplaySupplierPurchaseCategoryRecursion(int cate_id, string href, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "7eccd02b-0624-4add-a11f-ce21d405a1d5"))
            { }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7eccd02b-0624-4add-a11f-ce21d405a1d5错误");
            }

            int Cate_ParentID;
            string Cate_Name, CateNameStr;
            CateNameStr = "";

            SupplierPurchaseCategoryInfo entity = MyDAL.GetSupplierPurchaseCategoryByID(cate_id);
            if (entity != null)
            {
                cate_id = entity.Cate_ID;
                Cate_ParentID = entity.Cate_ParentID;
                Cate_Name = entity.Cate_Name;

                if (Cate_ParentID > 0)
                    CateNameStr = DisplaySupplierPurchaseCategoryRecursion(Cate_ParentID, href, UserPrivilege);

                if (CateNameStr.Length > 0)
                    CateNameStr += "&nbsp;&gt;&nbsp;";

                if (href.Length > 0)
                {
                    CateNameStr += "<a href=\"" + href.Replace("{cate_id}", cate_id.ToString()) + "\">" + Cate_Name + "</a>";
                }
                else
                {
                    CateNameStr += Cate_Name;
                }
            }
            return CateNameStr;
        }

        public virtual string Get_All_SubSupplierPurchaseCateID(int Cate_ID)
        {
            return MyDAL.Get_All_SubSupplierPurchaseCateID(Cate_ID);
        }

    }
}
