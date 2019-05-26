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
    public interface IZhongXin
    {
        bool AddZhongXin(ZhongXinInfo entity);

        bool EditZhongXin(ZhongXinInfo entity);

        int DelZhongXin(int ID);

        ZhongXinInfo GetZhongXinByID(int ID);

        ZhongXinInfo GetZhongXinBySuppleir(int ID);

        IList<ZhongXinInfo> GetZhongXins(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool SaveZhongXinAccountLog(ZhongXinAccountLogInfo entity);

        double GetZhongXinAccountRemainByMemberID(int MemberID);
    }
}
