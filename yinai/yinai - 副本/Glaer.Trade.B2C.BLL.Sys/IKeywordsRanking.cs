using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IKeywordsRanking
    {
        bool AddKeywordsRanking(KeywordsRankingInfo entity);

        bool EditKeywordsRanking(KeywordsRankingInfo entity);

        int DelKeywordsRanking(int ID);

        KeywordsRankingInfo GetKeywordsRankingByID(int ID);

        IList<KeywordsRankingInfo> GetKeywordsRankings(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
