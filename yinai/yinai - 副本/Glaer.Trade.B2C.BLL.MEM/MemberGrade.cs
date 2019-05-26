using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberGrade : IMemberGrade
    {
        protected DAL.MEM.IMemberGrade MyDAL;
        protected IRBAC RBAC;

        public MemberGrade()
        {
            MyDAL = DAL.MEM.MemberGradeFactory.CreateMemberGrade();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberGrade(MemberGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "603eef98-3a55-46d6-9e8e-81772645adeb"))
            {
                return MyDAL.AddMemberGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：603eef98-3a55-46d6-9e8e-81772645adeb错误");
            }
        }

        public virtual bool EditMemberGrade(MemberGradeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "73df03fa-ef43-486a-9b4c-b9c3e834cbbb"))
            {
                return MyDAL.EditMemberGrade(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：73df03fa-ef43-486a-9b4c-b9c3e834cbbb错误");
            }
        }

        public virtual int DelMemberGrade(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8f0cba35-c84e-4cfb-8ed5-26802004a848"))
            {
                return MyDAL.DelMemberGrade(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8f0cba35-c84e-4cfb-8ed5-26802004a848错误");
            }
        }

        public virtual MemberGradeInfo GetMemberGradeByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"))
            {
                return MyDAL.GetMemberGradeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea错误");
            }
        }

        public virtual IList<MemberGradeInfo> GetMemberGrades(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"))
            {
                return MyDAL.GetMemberGrades(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea错误");
            }
        }

        public virtual MemberGradeInfo GetMemberDefaultGrade()
        {
            return MyDAL.GetMemberDefaultGrade();
        }
    }

}
