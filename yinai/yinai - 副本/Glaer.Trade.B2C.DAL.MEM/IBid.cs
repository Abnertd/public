using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface IBid
    {
        bool AddBid(BidInfo entity);

        bool EditBid(BidInfo entity);

        int DelBid(int ID);

        BidInfo GetBidByID(int ID);

        BidInfo GetBidBySN(string SN);

        DataTable GetOrderProducts(int BidID);
        IList<BidInfo> GetBids(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
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
