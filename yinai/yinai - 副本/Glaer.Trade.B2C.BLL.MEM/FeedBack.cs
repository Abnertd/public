using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class FeedBack : IFeedBack
    {
        protected DAL.MEM.IFeedBack MyDAL;
        protected IRBAC RBAC;

        public FeedBack()
        {
            MyDAL = DAL.MEM.FeedBackFactory.CreateFeedBack();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddFeedBack(FeedBackInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8ccafb10-8a4a-425f-8111-a1e4eb46a0b4"))
            {
                return MyDAL.AddFeedBack(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8ccafb10-8a4a-425f-8111-a1e4eb46a0b4错误");
            }
        }

        public virtual bool EditFeedBack(FeedBackInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "02cc2c2c-9ecc-462a-86dc-406f792ac83a"))
            {
                return MyDAL.EditFeedBack(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：02cc2c2c-9ecc-462a-86dc-406f792ac83a错误");
            }
        }

        public virtual bool EditFeedBackReadStatus(int FeedBack_ID, int Read_Status, int Reply_Read_Status, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "02cc2c2c-9ecc-462a-86dc-406f792ac83a"))
            {
                return MyDAL.EditFeedBackReadStatus(FeedBack_ID, Read_Status, Reply_Read_Status);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：02cc2c2c-9ecc-462a-86dc-406f792ac83a错误");
            }
        }

        public virtual int DelFeedBack(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cc567804-3e2e-4c6c-aa22-c9a353508074"))
            {
                return MyDAL.DelFeedBack(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cc567804-3e2e-4c6c-aa22-c9a353508074错误");
            }
        }

        public virtual FeedBackInfo GetFeedBackByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9877a09e-5dda-4b1e-bf6f-042504449eeb"))
            {
                return MyDAL.GetFeedBackByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9877a09e-5dda-4b1e-bf6f-042504449eeb错误");
            }
        }

        public virtual IList<FeedBackInfo> GetFeedBacks(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9877a09e-5dda-4b1e-bf6f-042504449eeb"))
            {
                return MyDAL.GetFeedBacks(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9877a09e-5dda-4b1e-bf6f-042504449eeb错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9877a09e-5dda-4b1e-bf6f-042504449eeb"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9877a09e-5dda-4b1e-bf6f-042504449eeb错误");
            }
        }

    }
}

