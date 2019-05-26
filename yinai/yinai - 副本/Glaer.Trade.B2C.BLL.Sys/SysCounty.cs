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
    public class SysCounty : ISysCounty
    {
        protected DAL.Sys.ISysCounty MyDAL;
        protected IRBAC RBAC;

        public SysCounty()
        {
            MyDAL = DAL.Sys.SysCountyFactory.CreateSysCounty();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSysCounty(SysCountyInfo entity)
        {
            return MyDAL.AddSysCounty(entity);
        }

        public virtual bool EditSysCounty(SysCountyInfo entity)
        {
            return MyDAL.EditSysCounty(entity);
        }

        public virtual int DelSysCounty(int ID)
        {
            return MyDAL.DelSysCounty(ID);
        }

        public virtual SysCountyInfo GetSysCountyByID(int ID)
        {
            return MyDAL.GetSysCountyByID(ID);
        }

        public virtual IList<SysCountyInfo> GetSysCountys(QueryInfo Query)
        {
            return MyDAL.GetSysCountys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
