using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class BidFactory
    {
        public static IBid CreateBid()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".Bid";
            return (IBid)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class BidProductFactory
    {
        public static IBidProduct CreateBidProduct()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".BidProduct";
            return (IBidProduct)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class BidAttachmentsFactory
    {
        public static IBidAttachments CreateBidAttachments()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".BidAttachments";
            return (IBidAttachments)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class BidEnterFactory
    {
        public static IBidEnter CreateBidEnter()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".BidEnter";
            return (IBidEnter)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class TenderFactory
    {
        public static ITender CreateTender()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".Tender";
            return (ITender)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
