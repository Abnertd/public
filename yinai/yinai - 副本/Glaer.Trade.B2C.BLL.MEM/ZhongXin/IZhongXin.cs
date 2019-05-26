using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
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
