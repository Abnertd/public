using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class ContractTemplate : IContractTemplate
    {
        protected DAL.ORD.IContractTemplate MyDAL;
        protected IRBAC RBAC;

        public ContractTemplate()
        {
            MyDAL = DAL.ORD.ContractTemplateFactory.CreateContractTemplate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContractTemplate(ContractTemplateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "09c3558e-e719-4f31-b3ba-c6cabae4fbc9"))
            {
                return MyDAL.AddContractTemplate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：09c3558e-e719-4f31-b3ba-c6cabae4fbc9错误");
            } 
        }

        public virtual bool EditContractTemplate(ContractTemplateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "54d1aadd-ca23-4c1a-b56e-793543423a39"))
            {
                return MyDAL.EditContractTemplate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：54d1aadd-ca23-4c1a-b56e-793543423a39错误");
            } 
        }

        public virtual int DelContractTemplate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "123dda29-e3d2-487a-b91d-22743c18f99e"))
            {
                return MyDAL.DelContractTemplate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：123dda29-e3d2-487a-b91d-22743c18f99e错误");
            } 
        }

        public virtual ContractTemplateInfo GetContractTemplateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d4d58107-0e58-485f-af9e-3b38c7ff9672"))
            {
                return MyDAL.GetContractTemplateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d4d58107-0e58-485f-af9e-3b38c7ff9672错误");
            } 
        }

        public virtual ContractTemplateInfo GetContractTemplateBySign(string Sign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d4d58107-0e58-485f-af9e-3b38c7ff9672"))
            {
                return MyDAL.GetContractTemplateBySign(Sign);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d4d58107-0e58-485f-af9e-3b38c7ff9672错误");
            } 
        }

        public virtual IList<ContractTemplateInfo> GetContractTemplates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d4d58107-0e58-485f-af9e-3b38c7ff9672"))
            {
                return MyDAL.GetContractTemplates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d4d58107-0e58-485f-af9e-3b38c7ff9672错误");
            } 
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d4d58107-0e58-485f-af9e-3b38c7ff9672"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d4d58107-0e58-485f-af9e-3b38c7ff9672错误");
            } 
        }

    }

}
