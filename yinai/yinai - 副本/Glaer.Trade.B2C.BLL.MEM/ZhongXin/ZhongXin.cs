using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class ZhongXin : IZhongXin
    {
        protected DAL.MEM.IZhongXin MyDAL;
        protected IRBAC RBAC;

        public ZhongXin()
        {
            MyDAL = DAL.MEM.ZhongXinFactory.Create();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddZhongXin(ZhongXinInfo entity)
        {
            return MyDAL.AddZhongXin(entity);
        }

        public virtual bool EditZhongXin(ZhongXinInfo entity)
        {
            return MyDAL.EditZhongXin(entity);
        }

        public virtual int DelZhongXin(int ID)
        {
            return MyDAL.DelZhongXin(ID);
        }

        public virtual ZhongXinInfo GetZhongXinByID(int ID)
        {
            return MyDAL.GetZhongXinByID(ID);
        }

        public virtual ZhongXinInfo GetZhongXinBySuppleir(int ID)
        {
            return MyDAL.GetZhongXinBySuppleir(ID);
        }

        public virtual IList<ZhongXinInfo> GetZhongXins(QueryInfo Query)
        {
            return MyDAL.GetZhongXins(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual bool SaveZhongXinAccountLog(ZhongXinAccountLogInfo entity)
        {
            return MyDAL.SaveZhongXinAccountLog(entity);
        }

        public virtual double GetZhongXinAccountRemainByMemberID(int MemberID)
        {
            return MyDAL.GetZhongXinAccountRemainByMemberID(MemberID);
        }
    }
}
