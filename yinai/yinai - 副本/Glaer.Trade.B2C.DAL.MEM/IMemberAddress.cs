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
    public interface IMemberAddress
    {
        bool AddMemberAddress(MemberAddressInfo entity);

        bool EditMemberAddress(MemberAddressInfo entity);

        int DelMemberAddress(int ID);

        MemberAddressInfo GetMemberAddressByID(int ID);

        IList<MemberAddressInfo> GetMemberAddresss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
