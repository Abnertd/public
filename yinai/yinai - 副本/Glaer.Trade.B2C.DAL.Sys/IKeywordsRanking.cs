using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
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
