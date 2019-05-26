using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface ISources
    {
        bool AddSources(SourcesInfo entity);

        bool EditSources(SourcesInfo entity);

        int DelSources(int ID);

        SourcesInfo GetSourcesByID(int ID);

        SourcesInfo GetSourcesByCode(string Code);

        IList<SourcesInfo> GetSourcess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
