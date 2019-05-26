using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class SupplierPriceReport : ISupplierPriceReport
    {
        protected DAL.MEM.ISupplierPriceReport MyDAL;
        protected IRBAC RBAC;

        public SupplierPriceReport()
        {
            MyDAL = DAL.MEM.SupplierPriceReportFactory.CreateSupplierPriceReport();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPriceReport(SupplierPriceReportInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b482df13-2941-4314-9200-b64db8b358bc"))
            {
                return MyDAL.AddSupplierPriceReport(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b482df13-2941-4314-9200-b64db8b358bc错误");
            }
          
        }

        public virtual bool EditSupplierPriceReport(SupplierPriceReportInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d2656c57-1fbb-4928-8bff-41488d5763cc"))
            {
                return MyDAL.EditSupplierPriceReport(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d2656c57-1fbb-4928-8bff-41488d5763cc错误");
            }
          
           
        }

        public virtual int DelSupplierPriceReport(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2c2d7ac8-916d-4638-b4e8-9230f59b990c"))
            {
                return MyDAL.DelSupplierPriceReport(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2c2d7ac8-916d-4638-b4e8-9230f59b990c错误");
            }
          
           
        }

        public virtual SupplierPriceReportInfo GetSupplierPriceReportByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {
                return MyDAL.GetSupplierPriceReportByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
           
        }

        public virtual IList<SupplierPriceReportInfo> GetSupplierPriceReports(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {
                return MyDAL.GetSupplierPriceReports(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
          
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
          
        }

    }

    public class SupplierPriceReportDetail : ISupplierPriceReportDetail
    {
        protected DAL.MEM.ISupplierPriceReportDetail MyDAL;
        protected IRBAC RBAC;

        public SupplierPriceReportDetail()
        {
            MyDAL = DAL.MEM.SupplierPriceReportDetailFactory.CreateSupplierPriceReportDetail();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b482df13-2941-4314-9200-b64db8b358bc"))
            {
                return MyDAL.AddSupplierPriceReportDetail(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b482df13-2941-4314-9200-b64db8b358bc错误");
            }
          
           
        }

        public virtual bool EditSupplierPriceReportDetail(SupplierPriceReportDetailInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d2656c57-1fbb-4928-8bff-41488d5763cc"))
            {
                return MyDAL.EditSupplierPriceReportDetail(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d2656c57-1fbb-4928-8bff-41488d5763cc错误");
            }
          
           
        }

        public virtual int DelSupplierPriceReportDetail(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2c2d7ac8-916d-4638-b4e8-9230f59b990c"))
            {
                return MyDAL.DelSupplierPriceReportDetail(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2c2d7ac8-916d-4638-b4e8-9230f59b990c错误");
            }
          
          
        }

        public virtual int DelSupplierPriceReportDetailByPriceReportID(int ID)
        {
            return MyDAL.DelSupplierPriceReportDetailByPriceReportID(ID);
        }

        public virtual SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {

                return MyDAL.GetSupplierPriceReportDetailByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
        }

        public virtual SupplierPriceReportDetailInfo GetSupplierPriceReportDetailByPurchaseDetailID(int Detail_PurchaseID, int Detail_PurchaseDetailID)
        {
            return MyDAL.GetSupplierPriceReportDetailByPurchaseDetailID(Detail_PurchaseID,Detail_PurchaseDetailID);
        }

        public virtual IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetailsByPriceReportID(int ID)
        {
            return MyDAL.GetSupplierPriceReportDetailsByPriceReportID(ID);
        }

        public virtual IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetails(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {
                return MyDAL.GetSupplierPriceReportDetails(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
            
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "6a12664e-4eeb-4259-b7b5-904044194067"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：6a12664e-4eeb-4259-b7b5-904044194067错误");
            }
          
           
        }

    }
}
