using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface ISources
    {
        bool AddSources(SourcesInfo entity, RBACUserInfo UserPrivilege);

        bool EditSources(SourcesInfo entity, RBACUserInfo UserPrivilege);

        int DelSources(int ID, RBACUserInfo UserPrivilege);

        SourcesInfo GetSourcesByID(int ID, RBACUserInfo UserPrivilege);

        SourcesInfo GetSourcesByCode(string Code);

        IList<SourcesInfo> GetSourcess(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}
