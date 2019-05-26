using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public class SysState : ISysState
    {
        protected DAL.Sys.ISysState MyDAL;
        protected IRBAC RBAC;

        public SysState()
        {
            MyDAL = DAL.Sys.SysStateFactory.CreateSysState();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSysState(SysStateInfo entity)
        {
            return MyDAL.AddSysState(entity);
        }

        public virtual bool EditSysState(SysStateInfo entity)
        {
            return MyDAL.EditSysState(entity);
        }

        public virtual int DelSysState(int ID)
        {
            return MyDAL.DelSysState(ID);
        }

        public virtual SysStateInfo GetSysStateByID(int ID)
        {
            return MyDAL.GetSysStateByID(ID);
        }

        public virtual IList<SysStateInfo> GetSysStates(QueryInfo Query)
        {
            return MyDAL.GetSysStates(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
