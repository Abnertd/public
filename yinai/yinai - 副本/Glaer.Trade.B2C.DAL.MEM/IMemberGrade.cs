using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface IMemberGrade
    {
        bool AddMemberGrade(MemberGradeInfo entity);

        bool EditMemberGrade(MemberGradeInfo entity);

        int DelMemberGrade(int ID);

        MemberGradeInfo GetMemberGradeByID(int ID);

        IList<MemberGradeInfo> GetMemberGrades(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        MemberGradeInfo GetMemberDefaultGrade();
    }

}
