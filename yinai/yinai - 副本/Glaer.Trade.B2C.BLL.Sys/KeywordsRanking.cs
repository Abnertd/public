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
    public class KeywordsRanking : IKeywordsRanking
    {
        protected DAL.Sys.IKeywordsRanking MyDAL;
        protected IRBAC RBAC;

        public KeywordsRanking()
        {
            MyDAL = DAL.Sys.KeywordsRankingFactory.CreateKeywordsRanking();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddKeywordsRanking(KeywordsRankingInfo entity)
        {
            return MyDAL.AddKeywordsRanking(entity);
        }

        public virtual bool EditKeywordsRanking(KeywordsRankingInfo entity)
        {
            return MyDAL.EditKeywordsRanking(entity);
        }

        public virtual int DelKeywordsRanking(int ID)
        {
            return MyDAL.DelKeywordsRanking(ID);
        }

        public virtual KeywordsRankingInfo GetKeywordsRankingByID(int ID)
        {
            return MyDAL.GetKeywordsRankingByID(ID);
        }

        public virtual IList<KeywordsRankingInfo> GetKeywordsRankings(QueryInfo Query)
        {
            return MyDAL.GetKeywordsRankings(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}
