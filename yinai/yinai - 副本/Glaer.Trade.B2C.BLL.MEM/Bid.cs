using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class Bid : IBid
    {
        protected DAL.MEM.IBid MyDAL;
        protected IRBAC RBAC;

        public Bid()
        {
            MyDAL = DAL.MEM.BidFactory.CreateBid();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBid(BidInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e202397a-bb1e-4e67-b008-67701d37c5cb"))
            {
                return MyDAL.AddBid(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e202397a-bb1e-4e67-b008-67701d37c5cb错误");
            }

            
        }

        public virtual bool EditBid(BidInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039"))
            {
                return MyDAL.EditBid(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039错误");
            }

            
        }

        public virtual int DelBid(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "01b227fd-4910-4b5f-a9c0-d0d54a693439"))
            {
                return MyDAL.DelBid(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：01b227fd-4910-4b5f-a9c0-d0d54a693439错误");
            }

            
        }

        public virtual BidInfo GetBidByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                return MyDAL.GetBidByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }

        }

        public virtual BidInfo GetBidBySN(string SN, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                return MyDAL.GetBidBySN(SN);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }

            
        }
        public virtual IList<BidInfo> GetBids(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                return MyDAL.GetBids(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }

           
        }

        public DataTable GetOrderProducts(int BidID)
        {
            return MyDAL.GetOrderProducts(BidID);
        }
        
        public virtual IList<BidInfo> GetListBids(int MemberID,string IsAudit,string Status, int Type, int PageSize, int CurrentPage, string keyword, string date, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = PageSize;
                Query.CurrentPage = CurrentPage;
                if (MemberID > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_MemberID", "=", MemberID.ToString()));
                }
               
                if (IsAudit.Length>0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_IsAudit", "in", IsAudit));
                }
                if (Status.Length>0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Status", "in", Status));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Type", "=", Type.ToString()));
                if (keyword.Length > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "BidInfo.Bid_Title", "%like%", keyword));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "BidInfo.Bid_MemberCompany", "%like%", keyword));
                }
                if (date != "")
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "funint", "DATEDIFF(d,{BidInfo.Bid_AddTime},'" + Convert.ToDateTime(date) + "')", ">=", "0"));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "funint", "DATEDIFF(d,{BidInfo.Bid_BidEndTime},'" + Convert.ToDateTime(date) + "')", "<=", "0"));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_IsShow", "=", "1"));
                Query.OrderInfos.Add(new OrderInfo("BidInfo.Bid_ID", "Desc"));

                return MyDAL.GetBids(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }

            

        }

        public virtual PageInfo GetPageInfoList(int MemberID,string IsAudit,string Status, int Type, int PageSize, int CurrentPage, string keyword, string date, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = PageSize;
                Query.CurrentPage = CurrentPage;
                if (MemberID > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_MemberID", "=", MemberID.ToString()));
                }

                if (IsAudit.Length > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_IsAudit", "in", IsAudit));
                }
                if (Status.Length > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Status", "in", Status));
                }

                Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Type", "=", Type.ToString()));
                if (keyword.Length > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "BidInfo.Bid_Title", "%like%", keyword));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "BidInfo.Bid_MemberCompany", "%like%", keyword));
                }
                if (date != "")
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "funint", "DATEDIFF(d,{BidInfo.Bid_AddTime},'" + Convert.ToDateTime(date) + "')", ">=", "0"));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "funint", "DATEDIFF(d,{BidInfo.Bid_BidEndTime},'" + Convert.ToDateTime(date) + "')", "<=", "0"));
                }

                Query.OrderInfos.Add(new OrderInfo("BidInfo.Bid_ID", "Desc"));

                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }
        }
        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2错误");
            }

            
        }

    }

    public class BidProduct : IBidProduct
    {
        protected DAL.MEM.IBidProduct MyDAL;
        protected IRBAC RBAC;

        public BidProduct()
        {
            MyDAL = DAL.MEM.BidProductFactory.CreateBidProduct();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBidProduct(BidProductInfo entity)
        {
            return MyDAL.AddBidProduct(entity);
        }

        public virtual bool EditBidProduct(BidProductInfo entity)
        {
            return MyDAL.EditBidProduct(entity);
        }

        public virtual int DelBidProduct(int ID)
        {
            return MyDAL.DelBidProduct(ID);
        }

        public virtual BidProductInfo GetBidProductByID(int ID)
        {
            return MyDAL.GetBidProductByID(ID);
        }

        public virtual IList<BidProductInfo> GetBidProducts(QueryInfo Query)
        {
            return MyDAL.GetBidProducts(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

    public class BidAttachments : IBidAttachments
    {
        protected DAL.MEM.IBidAttachments MyDAL;
        protected IRBAC RBAC;

        public BidAttachments()
        {
            MyDAL = DAL.MEM.BidAttachmentsFactory.CreateBidAttachments();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBidAttachments(BidAttachmentsInfo entity)
        {
            return MyDAL.AddBidAttachments(entity);
        }

        public virtual bool EditBidAttachments(BidAttachmentsInfo entity)
        {
            return MyDAL.EditBidAttachments(entity);
        }

        public virtual int DelBidAttachments(int ID)
        {
            return MyDAL.DelBidAttachments(ID);
        }

        public virtual BidAttachmentsInfo GetBidAttachmentsByID(int ID)
        {
            return MyDAL.GetBidAttachmentsByID(ID);
        }

        public virtual IList<BidAttachmentsInfo> GetBidAttachmentss(QueryInfo Query)
        {
            return MyDAL.GetBidAttachmentss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

    public class BidEnter : IBidEnter
    {
        protected DAL.MEM.IBidEnter MyDAL;
        protected IRBAC RBAC;

        public BidEnter()
        {
            MyDAL = DAL.MEM.BidEnterFactory.CreateBidEnter();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddBidEnter(BidEnterInfo entity)
        {
            return MyDAL.AddBidEnter(entity);
        }

        public virtual bool EditBidEnter(BidEnterInfo entity)
        {
            return MyDAL.EditBidEnter(entity);
        }

        public virtual int DelBidEnter(int ID)
        {
            return MyDAL.DelBidEnter(ID);
        }

        public virtual BidEnterInfo GetBidEnterByID(int ID)
        {
            return MyDAL.GetBidEnterByID(ID);
        }

        public virtual BidEnterInfo GetBidEnterBySupplierID(int BidID,int SupplierID)
        {
            return MyDAL.GetBidEnterBySupplierID(BidID,SupplierID);
        }
        public virtual DataTable GetBidEnterSupplierList(QueryInfo Query)
        {
            return MyDAL.GetBidEnterSupplierList(Query);
        }
        public virtual IList<BidEnterInfo> GetBidEnters(QueryInfo Query)
        {
            return MyDAL.GetBidEnters(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

    public class Tender : ITender
    {
        protected DAL.MEM.ITender MyDAL;
        protected IRBAC RBAC;

        public Tender()
        {
            MyDAL = DAL.MEM.TenderFactory.CreateTender();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddTender(TenderInfo entity)
        {
            return MyDAL.AddTender(entity);
        }

        public virtual bool EditTender(TenderInfo entity)
        {
            return MyDAL.EditTender(entity);
        }

        public virtual int DelTender(int ID)
        {
            return MyDAL.DelTender(ID);
        }

        public virtual TenderInfo GetTenderByID(int ID)
        {
            return MyDAL.GetTenderByID(ID);
        }

        public virtual TenderInfo GetTenderBySN(string SN)
        {
            return MyDAL.GetTenderBySN(SN);
        }

        public virtual IList<TenderInfo> GetTenders(QueryInfo Query)
        {
            return MyDAL.GetTenders(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual bool AddTenderProduct(TenderProductInfo entity)
        {
            return MyDAL.AddTenderProduct(entity);
        }

        public virtual bool EditTenderProduct(TenderProductInfo entity)
        {
            return MyDAL.EditTenderProduct(entity);
        }

        public virtual int DelTenderProduct(int ID)
        {
            return MyDAL.DelTenderProduct(ID);
        }

        public virtual TenderProductInfo GetTenderProductByID(int ID)
        {
            return MyDAL.GetTenderProductByID(ID);
        }

        public virtual IList<TenderProductInfo> GetTenderProducts(QueryInfo Query)
        {
            return MyDAL.GetTenderProducts(Query);
        }

        public virtual IList<TenderProductInfo> GetTenderProducts(int TenderID)
        {
            return MyDAL.GetTenderProducts(TenderID);
        }
    }
}
