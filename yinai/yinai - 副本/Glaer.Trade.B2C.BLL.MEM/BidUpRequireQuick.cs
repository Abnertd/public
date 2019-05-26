using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class BidUpRequireQuick : IBidUpRequireQuick
    {
        protected DAL.MEM.IBidUpRequireQuick MyDAL;
        protected IRBAC RBAC;

        public BidUpRequireQuick()
        {
            MyDAL = DAL.MEM.BidUpRequireQuickFactory.CreateBidUpRequireQuick();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBidUpRequireQuick(BidUpRequireQuickInfo entity)
        {
            return MyDAL.AddBidUpRequireQuick(entity);
        }

        public virtual bool EditBidUpRequireQuick(BidUpRequireQuickInfo entity)
        {
            return MyDAL.EditBidUpRequireQuick(entity);
        }

        public virtual int DelBidUpRequireQuick(int ID)
        {
            return MyDAL.DelBidUpRequireQuick(ID);
        }

        public virtual BidUpRequireQuickInfo GetBidUpRequireQuickByID(int ID)
        {
            return MyDAL.GetBidUpRequireQuickByID(ID);
        }

        public virtual IList<BidUpRequireQuickInfo> GetBidUpRequireQuicks(QueryInfo Query)
        {
            return MyDAL.GetBidUpRequireQuicks(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}
