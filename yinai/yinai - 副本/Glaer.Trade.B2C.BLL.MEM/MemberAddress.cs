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
    public class MemberAddress : IMemberAddress
    {
        protected DAL.MEM.IMemberAddress MyDAL;

        public MemberAddress()
        {
            MyDAL = DAL.MEM.MemberAddressFactory.CreateMemberAddress();
        }

        public virtual bool AddMemberAddress(MemberAddressInfo entity)
        {
            return MyDAL.AddMemberAddress(entity);
        }

        public virtual bool EditMemberAddress(MemberAddressInfo entity)
        {
            return MyDAL.EditMemberAddress(entity);
        }

        public virtual int DelMemberAddress(int ID)
        {
            return MyDAL.DelMemberAddress(ID);
        }

        public virtual MemberAddressInfo GetMemberAddressByID(int ID)
        {
            return MyDAL.GetMemberAddressByID(ID);
        }

        public virtual IList<MemberAddressInfo> GetMemberAddresss(QueryInfo Query)
        {
            return MyDAL.GetMemberAddresss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }
}

