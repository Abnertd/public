using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IBidUpRequireQuick
    {
        bool AddBidUpRequireQuick(BidUpRequireQuickInfo entity);

        bool EditBidUpRequireQuick(BidUpRequireQuickInfo entity);

        int DelBidUpRequireQuick(int ID);

        BidUpRequireQuickInfo GetBidUpRequireQuickByID(int ID);

        IList<BidUpRequireQuickInfo> GetBidUpRequireQuicks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
