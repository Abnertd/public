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
    public interface IMemberFavorites
    {
        bool AddMemberFavorites(MemberFavoritesInfo entity);

        int DelMemberFavorites(int ID);

        MemberFavoritesInfo GetMemberFavoritesByID(int ID);

        MemberFavoritesInfo GetMemberFavoritesByProductID(int Member_ID, int type_id, int Product_ID);

        IList<MemberFavoritesInfo> GetMemberFavoritess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        #region 易耐网新加
        //根据会员ID获取该会员收藏商品数量
        IList<MemberFavoritesInfo> GetMemberFavoritesByMemberID(int Member_ID, int type_id);
        #endregion
    }
}
