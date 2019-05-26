using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface IKeywordBidding
    {
        bool AddKeywordBidding(KeywordBiddingInfo entity);

        bool EditKeywordBidding(KeywordBiddingInfo entity);

        int DelKeywordBidding(int Supplier_ID, int ID);

        KeywordBiddingInfo GetKeywordBiddingByID(int ID);

        IList<KeywordBiddingInfo> GetKeywordBiddings(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity);

        bool EditKeywordBiddingKeyword(KeywordBiddingKeywordInfo entity);

        int DelKeywordBiddingKeyword(int ID);

        KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByID(int ID);

        KeywordBiddingKeywordInfo GetKeywordBiddingKeywordByName(string Keyword);

        IList<KeywordBiddingKeywordInfo> GetKeywordBiddingKeywords(QueryInfo Query);

        PageInfo GetKeywordPageInfo(QueryInfo Query);
    }



}
