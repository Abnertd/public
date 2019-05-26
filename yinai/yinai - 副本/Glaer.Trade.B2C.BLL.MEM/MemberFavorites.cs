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
    public class MemberFavorites : IMemberFavorites
    {
        protected DAL.MEM.IMemberFavorites MyDAL;

        public MemberFavorites()
        {
            MyDAL = DAL.MEM.MemberFavoritesFactory.CreateMemberFavorites();
        }

        public virtual bool AddMemberFavorites(MemberFavoritesInfo entity)
        {
            return MyDAL.AddMemberFavorites(entity);
        }


        public virtual int DelMemberFavorites(int ID)
        {
            return MyDAL.DelMemberFavorites(ID);
        }

        public virtual MemberFavoritesInfo GetMemberFavoritesByID(int ID)
        {
            return MyDAL.GetMemberFavoritesByID(ID);
        }

        public virtual MemberFavoritesInfo GetMemberFavoritesByProductID(int Member_ID, int type_id, int Product_ID)
        {
            return MyDAL.GetMemberFavoritesByProductID(Member_ID, type_id, Product_ID);
        }

        public virtual IList<MemberFavoritesInfo> GetMemberFavoritess(QueryInfo Query)
        {
            return MyDAL.GetMemberFavoritess(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }



        #region 易耐网新加
        //根据会员ID获取该会员收藏商品数量
        public virtual IList<MemberFavoritesInfo> GetMemberFavoritesByMemberID(int Member_ID, int type_id)
        {
            return MyDAL.GetMemberFavoritesByMemberID(Member_ID, type_id);
        }
        #endregion

    }
}

