using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class KeywordBidding : IKeywordBidding
    {
        protected DAL.MEM.IKeywordBidding MyDAL;
        protected IRBAC RBAC;

        public KeywordBidding()
        {
            MyDAL = DAL.MEM.KeywordBiddingFactory.CreateKeywordBidding();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddKeywordBidding(KeywordBiddingInfo entity)
        {
            return MyDAL.AddKeywordBidding(entity);
        }

        public virtual bool EditKeywordBidding(KeywordBiddingInfo entity)
        {
            return MyDAL.EditKeywordBidding(entity);
        }

        public virtual int DelKeywordBidding(int Supplier_ID,int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cc52c0d7-188d-4915-955a-7e0857e958bc"))
            {
                return MyDAL.DelKeywordBidding(Supplier_ID,ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cc52c0d7-188d-4915-955a-7e0857e958bc错误");
            } 
        }

        public virtual KeywordBiddingInfo GetKeywordBiddingByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"))
            {
                return MyDAL.GetKeywordBiddingByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71错误");
            } 
        }

        public virtual IList<KeywordBiddingInfo> GetKeywordBiddings(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"))
            {
                return MyDAL.GetKeywordBiddings(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71错误");
            } 
        }

        public virtual bool EditKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0f39c533-9740-427f-ae56-649518a414c3"))
            {
                return MyDAL.EditKeywordBiddingKeyword(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0f39c533-9740-427f-ae56-649518a414c3错误");
            } 
        }

        public virtual bool AddKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity)
        {
            return MyDAL.AddKeywordBiddingKeyword(entity);
        }

        public virtual int DelKeywordBiddingKeyword(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "daebed75-ab60-46af-bd24-8d7da34f360a"))
            {
                return MyDAL.DelKeywordBiddingKeyword(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：daebed75-ab60-46af-bd24-8d7da34f360a错误");
            } 
        }

        public virtual KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1b86dfa7-32e5-4136-b3d1-a8a670f415ff"))
            {
                return MyDAL.GetKeywordBiddingKeywordByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1b86dfa7-32e5-4136-b3d1-a8a670f415ff错误");
            } 
        }

        public virtual KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByName(string Keyword, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1b86dfa7-32e5-4136-b3d1-a8a670f415ff"))
            {
                return MyDAL.GetKeywordBiddingKeywordByName(Keyword);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1b86dfa7-32e5-4136-b3d1-a8a670f415ff错误");
            }
        }

        public virtual IList<KeywordBiddingKeywordInfo> GetKeywordBiddingKeywords(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1b86dfa7-32e5-4136-b3d1-a8a670f415ff"))
            {
                return MyDAL.GetKeywordBiddingKeywords(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1b86dfa7-32e5-4136-b3d1-a8a670f415ff错误");
            } 
        }

        public virtual PageInfo GetKeywordPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1b86dfa7-32e5-4136-b3d1-a8a670f415ff"))
            {
                return MyDAL.GetKeywordPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1b86dfa7-32e5-4136-b3d1-a8a670f415ff错误");
            } 
        }
        

    }




}

