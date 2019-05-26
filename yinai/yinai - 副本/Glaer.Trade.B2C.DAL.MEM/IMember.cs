using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface IMember
    {
        bool AddMember(MemberInfo entity);

        bool EditMember(MemberInfo entity);

        bool UpdateMemberLogin(int Member_ID, int Count, string Remote_IP);

        int DelMember(int ID);

        MemberInfo GetMemberByID(int ID);

        MemberInfo GetMemberByEmail(string email);

        MemberInfo GetMemberByNickName(string NickName);

        MemberInfo GetMemberByLogin(string nickname, string password);

      bool  GetMemberAccountByLogin(string nickname);


        IList<MemberInfo> GetMembers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);


        MemberProfileInfo GetMemberProfileByID(int ID);

        bool AddMemberProfile(MemberProfileInfo entity);

        bool EditMemberProfile(MemberProfileInfo entity);

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

        int DelMemberToken(int ID);

        string GetMemberToken(string user, string pwd, int type, int tokentime);

        MemberTokenInfo GetMemberTokenByID(int ID);

        IList<MemberTokenInfo> GetMemberTokens(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }



}
