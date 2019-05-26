using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class Member : IMember
    {
        protected DAL.MEM.IMember MyDAL;
        protected IRBAC RBAC;

        public Member()
        {
            MyDAL = DAL.MEM.MemberFactory.CreateMember();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMember(MemberInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "5d071ec6-31d8-4960-a77d-f8209bbab496"))
            {
                return MyDAL.AddMember(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：5d071ec6-31d8-4960-a77d-f8209bbab496错误");
            }
        }

        public virtual bool EditMember(MemberInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
            {
                return MyDAL.EditMember(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：079ec5fc-33fe-4d58-a17f-14b5877b4ffe错误");
            }
        }

        public virtual bool UpdateMemberLogin(int Member_ID, int Count, string Remote_IP, RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.UpdateMemberLogin(Member_ID, Count, Remote_IP);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }

        public virtual int DelMember(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d736b77e-334a-4ce1-9748-7f1a23330a6c"))
            {
                return MyDAL.DelMember(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d736b77e-334a-4ce1-9748-7f1a23330a6c错误");
            }
        }

        public virtual MemberInfo GetMemberByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }

        public virtual MemberInfo GetMemberByEmail(string email, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberByEmail(email);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }

        public virtual MemberInfo GetMemberByNickName(string NickName, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberByNickName(NickName);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            } 
        }

      



        public virtual MemberInfo GetMemberByLogin(string nickname, string password, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberByLogin(nickname, password);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }



        public bool GetMemberAccountByLogin(string nickname, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberAccountByLogin(nickname);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }


        public virtual IList<MemberInfo> GetMembers(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3a9a9cdf-ef00-407d-98ef-44e23be397e8"))
            {
                return MyDAL.GetMembers(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3a9a9cdf-ef00-407d-98ef-44e23be397e8错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3a9a9cdf-ef00-407d-98ef-44e23be397e8") || RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3a9a9cdf-ef00-407d-98ef-44e23be397e8、833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }

        public virtual bool AddMemberProfile(MemberProfileInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "5d071ec6-31d8-4960-a77d-f8209bbab496"))
            {
                return MyDAL.AddMemberProfile(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：5d071ec6-31d8-4960-a77d-f8209bbab496错误");
            }
        }

        public virtual bool EditMemberProfile(MemberProfileInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "079ec5fc-33fe-4d58-a17f-14b5877b4ffe"))
            {
                return MyDAL.EditMemberProfile(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：079ec5fc-33fe-4d58-a17f-14b5877b4ffe错误");
            }
        }




        public virtual MemberProfileInfo GetMemberProfileByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "833b9bdd-a344-407b-b23a-671348d57f76"))
            {
                return MyDAL.GetMemberProfileByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：833b9bdd-a344-407b-b23a-671348d57f76错误");
            }
        }



        public bool AddMemberRelateCert(MemberRelateCertInfo entity)
        {
            return MyDAL.AddMemberRelateCert(entity);
        }

        public bool EditMemberRelateCert(MemberRelateCertInfo entity)
        {
            return MyDAL.EditMemberRelateCert(entity);
        }

        public int DelMemberRelateCertByMemberID(int ID)
        {
            return MyDAL.DelMemberRelateCertByMemberID(ID);
        }

        public IList<MemberRelateCertInfo> GetMemberRelateCerts(int Cert_MemberID)
        {
            return MyDAL.GetMemberRelateCerts(Cert_MemberID);
        }

        

    }

    public class MemberLog : IMemberLog
    {
        protected DAL.MEM.IMemberLog MyDAL;

        public MemberLog()
        {
            MyDAL = DAL.MEM.MemberLogFactory.CreateMemberLog();
        }

        public virtual bool AddMemberLog(MemberLogInfo entity)
        {
            return MyDAL.AddMemberLog(entity);
        }

        public virtual int DelMemberLog(int ID)
        {
            return MyDAL.DelMemberLog(ID);
        }

        public virtual IList<MemberLogInfo> GetMemberLogs(QueryInfo Query)
        {
            return MyDAL.GetMemberLogs(Query);
        }
    }

    public class MemberPurchase : IMemberPurchase
    {
        protected DAL.MEM.IMemberPurchase MyDAL;
        protected IRBAC RBAC;

        public MemberPurchase()
        {
            MyDAL = DAL.MEM.MemberPurchaseFactory.CreateMemberPurchase();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberPurchase(MemberPurchaseInfo entity)
        {
            return MyDAL.AddMemberPurchase(entity);
        }

        public virtual bool EditMemberPurchase(MemberPurchaseInfo entity)
        {
            return MyDAL.EditMemberPurchase(entity);
        }

        public virtual int DelMemberPurchase(int ID)
        {
            return MyDAL.DelMemberPurchase(ID);
        }

        public virtual MemberPurchaseInfo GetMemberPurchaseByID(int ID)
        {
            return MyDAL.GetMemberPurchaseByID(ID);
        }

        public virtual IList<MemberPurchaseInfo> GetMemberPurchases(QueryInfo Query)
        {
            return MyDAL.GetMemberPurchases(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class MemberPurchaseReply : IMemberPurchaseReply
    {
        protected DAL.MEM.IMemberPurchaseReply MyDAL;
        protected IRBAC RBAC;

        public MemberPurchaseReply()
        {
            MyDAL = DAL.MEM.MemberPurchaseReplyFactory.CreateMemberPurchaseReply();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberPurchaseReply(MemberPurchaseReplyInfo entity)
        {
            return MyDAL.AddMemberPurchaseReply(entity);
        }

        public virtual bool EditMemberPurchaseReply(MemberPurchaseReplyInfo entity)
        {
            return MyDAL.EditMemberPurchaseReply(entity);
        }

        public virtual int DelMemberPurchaseReply(int ID)
        {
            return MyDAL.DelMemberPurchaseReply(ID);
        }

        public virtual MemberPurchaseReplyInfo GetMemberPurchaseReplyByID(int ID)
        {
            return MyDAL.GetMemberPurchaseReplyByID(ID);
        }

        public virtual IList<MemberPurchaseReplyInfo> GetMemberPurchaseReplys(QueryInfo Query)
        {
            return MyDAL.GetMemberPurchaseReplys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }
    }

    public class MemberToken : IMemberToken
    {
        protected DAL.MEM.IMemberToken MyDAL;
        protected IRBAC RBAC;

        public MemberToken()
        {
            MyDAL = DAL.MEM.MemberTokenFactory.CreateMemberToken();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddMemberToken(MemberTokenInfo entity)
        {
            return MyDAL.AddMemberToken(entity);
        }

        public virtual bool EditMemberToken(MemberTokenInfo entity)
        {
            return MyDAL.EditMemberToken(entity);
        }

        public virtual int DelMemberToken(int ID)
        {
            return MyDAL.DelMemberToken(ID);
        }

        public virtual MemberTokenInfo GetMemberTokenByID(int ID)
        {
            return MyDAL.GetMemberTokenByID(ID);
        }

        public virtual IList<MemberTokenInfo> GetMemberTokens(QueryInfo Query)
        {
            return MyDAL.GetMemberTokens(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }


        public bool CheckMemberToken(string user, string pwd, int type, int tokentime)
        {
            return MyDAL.CheckMemberToken(user, pwd, type, tokentime);
        }


        public string GetMemberToken(string user, string pwd, int type, int tokentime)
        {
            return MyDAL.GetMemberToken(user, pwd, type, tokentime);
        }
    }

}
