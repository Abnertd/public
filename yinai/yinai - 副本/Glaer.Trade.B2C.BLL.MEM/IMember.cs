using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IMember
    {
        bool AddMember(MemberInfo entity, RBACUserInfo UserPrivilege);

        bool EditMember(MemberInfo entity, RBACUserInfo UserPrivilege);

        bool UpdateMemberLogin(int Member_ID, int Count, string Remote_IP, RBACUserInfo UserPrivilege);

        int DelMember(int ID, RBACUserInfo UserPrivilege);

        MemberInfo GetMemberByID(int ID, RBACUserInfo UserPrivilege);

        MemberInfo GetMemberByEmail(string email, RBACUserInfo UserPrivilege);

        MemberInfo GetMemberByNickName(string NickName, RBACUserInfo UserPrivilege);

        MemberInfo GetMemberByLogin(string nickname, string password, RBACUserInfo UserPrivilege);


        bool GetMemberAccountByLogin(string nickname, RBACUserInfo UserPrivilege);

        IList<MemberInfo> GetMembers(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);


        MemberProfileInfo GetMemberProfileByID(int ID, RBACUserInfo UserPrivilege);

        bool AddMemberProfile(MemberProfileInfo entity, RBACUserInfo UserPrivilege);

        bool EditMemberProfile(MemberProfileInfo entity, RBACUserInfo UserPrivilege);

        bool AddMemberRelateCert(MemberRelateCertInfo entity);

        bool EditMemberRelateCert(MemberRelateCertInfo entity);

        int DelMemberRelateCertByMemberID(int ID);

        IList<MemberRelateCertInfo> GetMemberRelateCerts(int Cert_MemberID);



    }

    public interface IMemberLog
    {
        bool AddMemberLog(MemberLogInfo entity);

        int DelMemberLog(int ID);

        IList<MemberLogInfo> GetMemberLogs(QueryInfo Query);
    }

    public interface IMemberPurchase
    {
        bool AddMemberPurchase(MemberPurchaseInfo entity);

        bool EditMemberPurchase(MemberPurchaseInfo entity);

        int DelMemberPurchase(int ID);

        MemberPurchaseInfo GetMemberPurchaseByID(int ID);

        IList<MemberPurchaseInfo> GetMemberPurchases(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IMemberPurchaseReply
    {
        bool AddMemberPurchaseReply(MemberPurchaseReplyInfo entity);

        bool EditMemberPurchaseReply(MemberPurchaseReplyInfo entity);

        int DelMemberPurchaseReply(int ID);

        MemberPurchaseReplyInfo GetMemberPurchaseReplyByID(int ID);

        IList<MemberPurchaseReplyInfo> GetMemberPurchaseReplys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IMemberToken
    {
        bool AddMemberToken(MemberTokenInfo entity);

        bool EditMemberToken(MemberTokenInfo entity);

        bool CheckMemberToken(string user, string pwd, int type, int tokentime);

        string GetMemberToken(string user, string pwd, int type, int tokentime);

        int DelMemberToken(int ID);

        MemberTokenInfo GetMemberTokenByID(int ID);

        IList<MemberTokenInfo> GetMemberTokens(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }


}
