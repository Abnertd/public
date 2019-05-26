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
    public class MemberConsumption : IMemberConsumption
    {
        protected DAL.MEM.IMemberConsumption MyDAL;

        public MemberConsumption()
        {
            MyDAL = DAL.MEM.MemberConsumptionFactory.CreateMemberConsumption();
        }

        public virtual bool AddMemberConsumption(MemberConsumptionInfo entity)
        {
            return MyDAL.AddMemberConsumption(entity);
        }

        public virtual int DelMemberConsumption(int ID)
        {
            return MyDAL.DelMemberConsumption(ID);
        }

        public virtual IList<MemberConsumptionInfo> GetMemberConsumptions(QueryInfo Query)
        {
            return MyDAL.GetMemberConsumptions(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

