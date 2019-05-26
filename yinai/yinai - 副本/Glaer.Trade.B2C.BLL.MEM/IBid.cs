using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IBid
    {
        bool AddBid(BidInfo entity, RBACUserInfo UserPrivilege);

        bool EditBid(BidInfo entity, RBACUserInfo UserPrivilege);

        int DelBid(int ID, RBACUserInfo UserPrivilege);

        BidInfo GetBidByID(int ID, RBACUserInfo UserPrivilege);

        BidInfo GetBidBySN(string SN, RBACUserInfo UserPrivilege);
        IList<BidInfo> GetBids(QueryInfo Query, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 获取用户招标拍卖列表
        /// </summary>
        /// <param name="MemberID">用户ID 0为全部</param>
        /// <param name="IsAudit">"1"为审核通过 ""为全部</param>
        /// <param name="Type">0招标1拍卖</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="keyword">关键词</param>
        /// <param name="date">日期</param>
        /// <param name="UserPrivilege"></param>
        /// <returns></returns>
        IList<BidInfo> GetListBids(int MemberID, string IsAudit, string Status, int Type, int PageSize, int CurrentPage, string keyword, string date, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfoList(int MemberID, string IsAudit, string Status, int Type, int PageSize, int CurrentPage, string keyword, string date, RBACUserInfo UserPrivilege);
        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        DataTable GetOrderProducts(int BidID);
    }

    public interface IBidProduct
    {
        bool AddBidProduct(BidProductInfo entity);

        bool EditBidProduct(BidProductInfo entity);

        int DelBidProduct(int ID);

        BidProductInfo GetBidProductByID(int ID);

        IList<BidProductInfo> GetBidProducts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IBidAttachments
    {
        bool AddBidAttachments(BidAttachmentsInfo entity);

        bool EditBidAttachments(BidAttachmentsInfo entity);

        int DelBidAttachments(int ID);

        BidAttachmentsInfo GetBidAttachmentsByID(int ID);

        IList<BidAttachmentsInfo> GetBidAttachmentss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface IBidEnter
    {
        bool AddBidEnter(BidEnterInfo entity);

        bool EditBidEnter(BidEnterInfo entity);

        int DelBidEnter(int ID);

        BidEnterInfo GetBidEnterByID(int ID);

        BidEnterInfo GetBidEnterBySupplierID(int BidID,int SupplierID);

        DataTable GetBidEnterSupplierList(QueryInfo Query);

        IList<BidEnterInfo> GetBidEnters(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

    public interface ITender
    {
        bool AddTender(TenderInfo entity);

        bool EditTender(TenderInfo entity);

        int DelTender(int ID);

        TenderInfo GetTenderByID(int ID);

        TenderInfo GetTenderBySN(string SN);

        IList<TenderInfo> GetTenders(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        bool AddTenderProduct(TenderProductInfo entity);

        bool EditTenderProduct(TenderProductInfo entity);

        int DelTenderProduct(int ID);

        TenderProductInfo GetTenderProductByID(int ID);
        IList<TenderProductInfo> GetTenderProducts(int TenderID);

        IList<TenderProductInfo> GetTenderProducts(QueryInfo Query);

    }
}
