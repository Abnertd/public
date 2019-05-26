using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IMemberGrade
    {
        bool AddMemberGrade(MemberGradeInfo entity, RBACUserInfo UserPrivilege);

        bool EditMemberGrade(MemberGradeInfo entity, RBACUserInfo UserPrivilege);

        int DelMemberGrade(int ID, RBACUserInfo UserPrivilege);

        MemberGradeInfo GetMemberGradeByID(int ID, RBACUserInfo UserPrivilege);

        IList<MemberGradeInfo> GetMemberGrades(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        MemberGradeInfo GetMemberDefaultGrade();
    }

}