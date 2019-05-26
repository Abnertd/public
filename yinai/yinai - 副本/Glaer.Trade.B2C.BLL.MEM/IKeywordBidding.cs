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
    public interface IKeywordBidding
    {
        bool AddKeywordBidding(KeywordBiddingInfo entity);

        bool EditKeywordBidding(KeywordBiddingInfo entity);

        int DelKeywordBidding(int Supplier_ID, int ID, RBACUserInfo UserPrivilege);

        KeywordBiddingInfo GetKeywordBiddingByID(int ID, RBACUserInfo UserPrivilege);

        IList<KeywordBiddingInfo> GetKeywordBiddings(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool AddKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity);

        bool EditKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity, RBACUserInfo UserPrivilege);

        int DelKeywordBiddingKeyword(int ID, RBACUserInfo UserPrivilege);

        KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByID(int ID, RBACUserInfo UserPrivilege);

        KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByName(string Keyword, RBACUserInfo UserPrivilege);

        IList<KeywordBiddingKeywordInfo> GetKeywordBiddingKeywords(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetKeywordPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);


    }



    


}

