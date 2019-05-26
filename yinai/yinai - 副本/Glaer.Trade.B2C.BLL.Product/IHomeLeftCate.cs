using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Product
{
    public interface IHomeLeftCate
    {
        bool AddHomeLeftCate(HomeLeftCateInfo entity, RBACUserInfo UserPrivilege);

        bool EditHomeLeftCate(HomeLeftCateInfo entity, RBACUserInfo UserPrivilege);

        int DelHomeLeftCate(int ID, RBACUserInfo UserPrivilege);

        int DelHomeLeftCateAll(RBACUserInfo UserPrivilege);

        HomeLeftCateInfo GetHomeLeftCateByID(int ID, RBACUserInfo UserPrivilege);

        HomeLeftCateInfo GetHomeLeftCateByLastID(RBACUserInfo UserPrivilege);

        IList<HomeLeftCateInfo> GetHomeLeftCates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}



