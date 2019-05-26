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
    public class SysCity : ISysCity {
        protected DAL.Sys.ISysCity MyDAL;
        protected IRBAC RBAC;

        public SysCity() {
            MyDAL = DAL.Sys.SysCityFactory.CreateSysCity();
            RBAC = RBACFactory.CreateRBAC();
        } 

        public virtual bool AddSysCity (SysCityInfo entity)  {
            return MyDAL.AddSysCity(entity);
        }

        public virtual bool EditSysCity (SysCityInfo entity) {
            return MyDAL.EditSysCity(entity);
        }

        public virtual int DelSysCity (int ID) {
            return MyDAL.DelSysCity(ID);
        }
        
        public virtual SysCityInfo GetSysCityByID(int ID) {
            return MyDAL.GetSysCityByID(ID);
        }
        
        public virtual IList<SysCityInfo> GetSysCitys(QueryInfo Query) {
            return MyDAL.GetSysCitys(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query) {
            return MyDAL.GetPageInfo(Query);
        }
        
    }

}
