using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.Product
{
    public class Category : ICategory
    {
        private DAL.Product.ICategory MyDAL;
        protected IRBAC RBAC;

        public Category()
        {
            MyDAL = DAL.Product.CategoryFactory.CreateCategory();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddCategory(CategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "402897ef-7473-4abd-9425-6220a61be7bf"))
            {
                return MyDAL.AddCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：402897ef-7473-4abd-9425-6220a61be7bf错误");
            }
        }

        public virtual bool EditCategory(CategoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2dcee4f1-71e1-4cbd-afa3-470f0b554fd0"))
            {
                return MyDAL.EditCategory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2dcee4f1-71e1-4cbd-afa3-470f0b554fd0错误");
            }
        }

        public virtual int DelCategory(int Cate_ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "14d9fa43-7e21-4eed-8955-39fafce6f185"))
            {
                return MyDAL.DelCategory(Cate_ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：14d9fa43-7e21-4eed-8955-39fafce6f185错误");
            }
        }

        public virtual CategoryInfo GetCategoryByID(int Cate_ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetCategoryByID(Cate_ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual IList<CategoryInfo> GetCategorys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetCategorys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual int GetSubCateCount(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetSubCateCount(Cate_ID, SiteSign);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual IList<CategoryInfo> GetSubCategorys(int Cate_ID, string SiteSign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {
                return MyDAL.GetSubCategorys(Cate_ID, SiteSign);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
        }

        public virtual string SelectCategoryOption(int Cate_ID, int selectVal, string appendTag, int shieldID, string SiteSign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            {

            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }
            string strHTML = "";
            IList<CategoryInfo> entitys = GetSubCategorys(Cate_ID, SiteSign, UserPrivilege);
            if (entitys != null)
            {
                appendTag += "&nbsp;&nbsp;";
                foreach (CategoryInfo entity in entitys)
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
                        strHTML += SelectCategoryOption(entity.Cate_ID, selectVal, appendTag, shieldID, SiteSign, UserPrivilege);
                    }
                }
            }
            return strHTML;
        }

        public virtual string DisplayCategoryRecursion(int cate_id, string href, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2883de94-8873-4c66-8f9a-75d80c004acb"))
            { }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2883de94-8873-4c66-8f9a-75d80c004acb错误");
            }

            int Cate_ParentID;
            string Cate_Name, CateNameStr;
            CateNameStr = "";

            CategoryInfo entity = MyDAL.GetCategoryByID(cate_id);
            if (entity != null)
            {
                cate_id = entity.Cate_ID;
                Cate_ParentID = entity.Cate_ParentID;
                Cate_Name = entity.Cate_Name;

                if (Cate_ParentID > 0)
                    CateNameStr = DisplayCategoryRecursion(Cate_ParentID, href, UserPrivilege);

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

        public virtual string Get_All_SubCateID(int Cate_ID)
        {
            return MyDAL.Get_All_SubCateID(Cate_ID);
        }
    }
}