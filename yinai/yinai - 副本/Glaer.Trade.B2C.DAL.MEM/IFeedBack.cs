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
    public interface IFeedBack
    {
        bool AddFeedBack(FeedBackInfo entity);

        bool EditFeedBack(FeedBackInfo entity);

        bool EditFeedBackReadStatus(int FeedBack_ID, int Read_Status, int Reply_Read_Status);

        int DelFeedBack(int ID);

        FeedBackInfo GetFeedBackByID(int ID);

        IList<FeedBackInfo> GetFeedBacks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
