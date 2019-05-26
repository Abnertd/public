using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IFeedBack
    {
        bool AddFeedBack(FeedBackInfo entity, RBACUserInfo UserPrivilege);

        bool EditFeedBack(FeedBackInfo entity, RBACUserInfo UserPrivilege);

        bool EditFeedBackReadStatus(int FeedBack_ID, int Read_Status, int Reply_Read_Status, RBACUserInfo UserPrivilege);

        int DelFeedBack(int ID, RBACUserInfo UserPrivilege);

        FeedBackInfo GetFeedBackByID(int ID, RBACUserInfo UserPrivilege);

        IList<FeedBackInfo> GetFeedBacks(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }
}
