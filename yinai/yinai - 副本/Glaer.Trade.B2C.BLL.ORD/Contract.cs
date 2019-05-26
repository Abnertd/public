using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.ORD
{
    public class Contract : IContract
    {
        protected DAL.ORD.IContract MyDAL;
        protected IRBAC RBAC;

        public Contract()
        {
            MyDAL = DAL.ORD.ContractFactory.CreateContract();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddContract(ContractInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "010afb3b-1cbf-47f9-8455-c35fe5eceea7"))
            {
                return MyDAL.AddContract(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：010afb3b-1cbf-47f9-8455-c35fe5eceea7错误");
            }
        }

        public virtual bool EditContract(ContractInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cd2be0f8-b35a-48ad-908b-b5165c0a1581"))
            {
                return MyDAL.EditContract(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cd2be0f8-b35a-48ad-908b-b5165c0a1581错误");
            }
        }

        public virtual int DelContract(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b27825e2-74c7-498b-9c83-ce0f460e9bda"))
            {
                return MyDAL.DelContract(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b27825e2-74c7-498b-9c83-ce0f460e9bda错误");
            }
        }

        public virtual ContractInfo GetContractByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a3465003-08b3-4a31-9103-28d16c57f2c8"))
            {
                return MyDAL.GetContractByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a3465003-08b3-4a31-9103-28d16c57f2c8错误");
            }
        }

        public virtual ContractInfo GetContractBySn(string Sn, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a3465003-08b3-4a31-9103-28d16c57f2c8"))
            {
                return MyDAL.GetContractBySn(Sn);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a3465003-08b3-4a31-9103-28d16c57f2c8错误");
            }
        }

        public virtual IList<ContractInfo> GetContracts(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a3465003-08b3-4a31-9103-28d16c57f2c8"))
            {
                return MyDAL.GetContracts(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a3465003-08b3-4a31-9103-28d16c57f2c8错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a3465003-08b3-4a31-9103-28d16c57f2c8"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a3465003-08b3-4a31-9103-28d16c57f2c8错误");
            }
        }

        public virtual int GetContractAmount(string Status, string Sn_Front, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a3465003-08b3-4a31-9103-28d16c57f2c8"))
            {
                return MyDAL.GetContractAmount(Status, Sn_Front);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a3465003-08b3-4a31-9103-28d16c57f2c8错误");
            } 
        }

        public virtual bool AddContractInvoice(ContractInvoiceInfo entity)
        {
            return MyDAL.AddContractInvoice(entity);
        }

        public virtual bool EditContractInvoice(ContractInvoiceInfo entity)
        {
            return MyDAL.EditContractInvoice(entity);
        }

        public virtual int DelContractInvoice(int ID)
        {
            return MyDAL.DelContractInvoice(ID);
        }

        public virtual ContractInvoiceInfo GetContractInvoiceByID(int ID)
        {
            return MyDAL.GetContractInvoiceByID(ID);
        }

        public virtual ContractInvoiceInfo GetContractInvoiceByContractID(int ID)
        {
            return MyDAL.GetContractInvoiceByContractID(ID);
        }

        public virtual bool AddContractInvoiceApply(ContractInvoiceApplyInfo entity)
        {
            return MyDAL.AddContractInvoiceApply(entity);
        }

        public virtual bool EditContractInvoiceApply(ContractInvoiceApplyInfo entity)
        {
            return MyDAL.EditContractInvoiceApply(entity);
        }

        public virtual int DelContractInvoiceApply(int ID)
        {
            return MyDAL.DelContractInvoiceApply(ID);
        }

        public virtual ContractInvoiceApplyInfo GetContractInvoiceApplyByID(int ID)
        {
            return MyDAL.GetContractInvoiceApplyByID(ID);
        }

        public virtual IList<ContractInvoiceApplyInfo> GetContractInvoiceApplysByContractID(int Contract_ID)
        {
            return MyDAL.GetContractInvoiceApplysByContractID(Contract_ID);
        }

        public virtual double Get_Contract_PayedAmount(int Contract_ID)
        {
            return MyDAL.Get_Contract_PayedAmount(Contract_ID);
        }
    }

}
