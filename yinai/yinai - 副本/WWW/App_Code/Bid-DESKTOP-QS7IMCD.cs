using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Bid 的摘要说明
/// </summary>
public class Bid
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private IBid MyBid;
    private IBidProduct MyBidProduct;
    private IBidAttachments MyBidAttachments;
    private IBidEnter MyBidEnter;
    private ITools tools;
    private Public_Class pub;
    private Supplier MySupplier;
    private ISupplier MyBLLSupplier;
    private ITender MyTender;
    private Product MyProduct;
    private PageURL pageurl;
    private IBidUpRequireQuick MyBidUpRequire;
    private Glaer.Trade.B2C.BLL.ORD.IOrders MyOrders;
    private IMember MyMember;
    ZhongXinUtil.SendMessages sendmessages;


    //投标保证金账户
    string bidguaranteeaccno;
    string bidguaranteeaccnm;
    //商家保证金账户
    string merchantguaranteeaccno;
    string merchantguaranteeaccnm;

    public Bid()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBid = BidFactory.CreateBid();
        tools = ToolsFactory.CreateTools();
        pub = new Public_Class();
        MyBidProduct = BidProductFactory.CreateBidProduct();
        MyBidAttachments = BidAttachmentsFactory.CreateBidAttachments();
        MyBidEnter = BidEnterFactory.CreateBidEnter();
        MySupplier = new Supplier();
        MyMember = MemberFactory.CreateMember();
        MyBLLSupplier = SupplierFactory.CreateSupplier();
        MyTender = TenderFactory.CreateTender();
        MyBidUpRequire = BidUpRequireQuickFactory.CreateBidUpRequireQuick();
        MyProduct = new Product();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
        sendmessages = new ZhongXinUtil.SendMessages();
        MyOrders = Glaer.Trade.B2C.BLL.ORD.OrdersFactory.CreateOrders();



        //投标保证金账户
        bidguaranteeaccno = System.Configuration.ConfigurationManager.AppSettings["zhongxin_bidguaranteeaccno"];
        bidguaranteeaccnm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_bidguaranteeaccnm"];
        //商家保证金账户
        merchantguaranteeaccno = System.Configuration.ConfigurationManager.AppSettings["zhongxin_merchantguaranteeaccno"];
        merchantguaranteeaccnm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_merchantguaranteeaccnm"];
    }


    //生成订单号
    public string Bid_SN()
    {
        string sn = "";
        bool ismatch = true;
        BidInfo bidinfo = null;

        sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            bidinfo = MyBid.GetBidBySN(sn, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
            if (bidinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    public IList<BidInfo> GetBidByID()
    {
        //获取当前登录用户ID
        int member_id = tools.NullInt(Session["member_id"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo query = new QueryInfo();
        return MyBid.GetListBids(member_id, "", "", 0, 0, 0, "", "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));

    }


    public BidInfo GetBidByID(int ID)
    {
        return MyBid.GetBidByID(ID, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
    }




    public BidProductInfo GetBidProductByID(int ID)
    {
        return MyBidProduct.GetBidProductByID(ID);
    }


    public BidAttachmentsInfo GetBidAttachmentsByID(int ID)
    {
        return MyBidAttachments.GetBidAttachmentsByID(ID);
    }

    public BidEnterInfo GetBidEnterBySupplierID(int BID, int Supplier_ID)
    {
        return MyBidEnter.GetBidEnterBySupplierID(BID, Supplier_ID);
    }

    public TenderInfo GetTenderByID(int ID)
    {
        return MyTender.GetTenderByID(ID);
    }


    //public bool EditTenderBy_ID(TenderInfo entity)
    //{
    //    return MyTender.EditTender(entity);
    //}


    public void TenderLosDete(int Tender_ID, string Is_Supplier)
    {
        TenderInfo tenderinfo = MyTender.GetTenderByID(Tender_ID);
        tenderinfo.Tender_IsShow = 0;
        if (MyTender.EditTender(tenderinfo))
        {
            if (Is_Supplier == "1")
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/tender_list.aspx");
            }
            else
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/member/auction_list.aspx");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "拍卖公告删除失败", false, "{back}");
        }
    }

    public void AddBid(int Type)
    {
        //int Bid_ID = tools.CheckInt(Request.Form["Bid_ID"]);
        int Bid_MemberID = tools.NullInt(Session["member_id"]);
        string Bid_MemberCompany = tools.CheckStr(Request.Form["Bid_MemberCompany"]);
        int Bid_SupplierID = 0;
        string Bid_SupplierCompany = "";
        int Bid_Number = 0;
        string Bid_Title = tools.CheckStr(Request.Form["Bid_Title"]);
        //DateTime Bid_EnterStartTime = tools.NullDate(Request.Form["Bid_EnterStartTime"]);
        //DateTime Bid_EnterEndTime = tools.NullDate(Request.Form["Bid_EnterEndTime"]);
        DateTime Bid_EnterStartTime = DateTime.Now;
        DateTime Bid_EnterEndTime = DateTime.Now;

        DateTime Bid_BidStartTime = tools.NullDate(Request.Form["Bid_BidStartTime"]);
        DateTime Bid_BidEndTime = tools.NullDate(Request.Form["Bid_BidEndTime"]);
        Bid_BidEndTime = Bid_BidEndTime.AddHours(23).AddMinutes(59).AddSeconds(59);

        DateTime Bid_AddTime = DateTime.Now;
        double Bid_Bond = tools.CheckFloat(Request.Form["Bid_Bond"]);
        string BidNumber = tools.CheckStr(Request.Form["Bid_Number"]);
        //string BidNumber ="1";


        if (BidNumber == "0")
        {
            Bid_Number = 100;
        }
        else
        {
            Bid_Number = tools.CheckInt(BidNumber);
        }
        int Bid_Status = 0;
        string Bid_Content = Request.Form["Bid_Content"];
        //int Bid_ProductType = tools.CheckInt(Request.Form["Bid_ProductType"]);
        int Bid_ProductType = 1;
        double Bid_AllPrice = 0;
        int Bid_Type = Type;
        string Bid_Contract = "";
        int Bid_IsAudit = 0;
        DateTime Bid_AuditTime = DateTime.Now;
        string Bid_AuditRemarks = "";
        int Bid_ExcludeSupplierID = tools.NullInt(Session["supplier_id"]);
        DateTime Bid_DeliveryTime = tools.NullDate(Request.Form["Bid_DeliveryTime"]);
        int Bid_IsOrders = 0;
        DateTime Bid_OrdersTime = DateTime.Now;
        string Bid_OrdersSN = "";
        DateTime Bid_FinishTime = DateTime.Now;
        if (Bid_MemberID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        if (Bid_Title.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写公告标题", false, "{back}");
        }
        if (Bid_MemberCompany.Length == 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写采购商名称", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "请填写拍卖用户名称", false, "{back}");
            }

        }



        if (DateTime.Compare(Bid_BidStartTime, Bid_BidEndTime) > 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "报价时间有误", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "竞价时间有误", false, "{back}");
            }

        }

        if (Bid_Number <= 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写报价轮次", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "请填写竞价轮次", false, "{back}");
            }

        }
        if (Bid_Bond < 0)
        {
            pub.Msg("error", "错误信息", "请确保保证金金额不小于零", false, "{back}");
        }
        string Bid_sn = Bid_SN();
        BidInfo entity = new BidInfo();
        //entity.Bid_ID = Bid_ID;
        entity.Bid_MemberID = Bid_MemberID;
        entity.Bid_MemberCompany = Bid_MemberCompany;
        entity.Bid_SupplierID = Bid_SupplierID;
        entity.Bid_SupplierCompany = Bid_SupplierCompany;
        entity.Bid_Title = Bid_Title;
        //entity.Bid_EnterStartTime = Bid_EnterStartTime;
        //entity.Bid_EnterEndTime = Bid_EnterEndTime;

        entity.Bid_EnterStartTime = Bid_EnterStartTime;
        entity.Bid_EnterEndTime = Bid_EnterEndTime;


        entity.Bid_BidStartTime = Bid_BidStartTime;
        entity.Bid_BidEndTime = Bid_BidEndTime;
        entity.Bid_AddTime = Bid_AddTime;
        entity.Bid_Bond = Bid_Bond;
        entity.Bid_Number = Bid_Number;
        entity.Bid_Status = Bid_Status;
        entity.Bid_Content = Bid_Content;
        entity.Bid_ProductType = Bid_ProductType;
        entity.Bid_AllPrice = Bid_AllPrice;
        entity.Bid_Type = Bid_Type;
        entity.Bid_Contract = Bid_Contract;
        entity.Bid_IsAudit = Bid_IsAudit;
        entity.Bid_AuditTime = Bid_AuditTime;
        entity.Bid_AuditRemarks = Bid_AuditRemarks;
        entity.Bid_ExcludeSupplierID = Bid_ExcludeSupplierID;
        entity.Bid_SN = Bid_sn;
        entity.Bid_DeliveryTime = Bid_DeliveryTime;
        entity.Bid_IsOrders = Bid_IsOrders;
        entity.Bid_OrdersTime = Bid_OrdersTime;
        entity.Bid_OrdersSN = Bid_OrdersSN;
        entity.Bid_FinishTime = Bid_FinishTime;
        entity.Bid_IsShow = 1;

        ZhongXin mycredit = new ZhongXin();
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);
        ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(Supplier_ID);

        if (PayAccountInfo != null)
        {
            decimal accountremain = 0;
            accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
            if (accountremain < (decimal)Bid_Bond)
            {
                //pub.Msg("error", "错误信息", "您的账户余额不足，请入金充值！", false, "{back}");
                Response.Write("false");

                Response.End();
            }
        }
        else
        {
            //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            Response.Write("false");
            Response.End();
        }

        if (MyBid.AddBid(entity, pub.CreateUserPrivilege("e202397a-bb1e-4e67-b008-67701d37c5cb")))
        {
            //ZhongXin mycredit = new ZhongXin();
            BidInfo bidinfo = MyBid.GetBidBySN(Bid_sn, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
            //int Supplier_ID = tools.NullInt(Session["supplier_id"]);
            //ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(Supplier_ID);

            //if (PayAccountInfo != null)
            //{
            //    decimal accountremain = 0;
            //    accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
            //    if (accountremain <= (decimal)Bid_Bond)
            //    {
            //        pub.Msg("error", "错误信息", "您的账户余额不足，请入金充值！", false, "{back}");
            //        Response.End();
            //    }
            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //}


            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplier_ID);
            string supplier_name = "";

            if (supplierinfo != null)
            {
                supplier_name = supplierinfo.Supplier_CompanyName;
            }
            string strResult = string.Empty;


            if (bidinfo != null)
            {
                if (Type == 0)
                {
                    //pub.Msg("positive", "操作成功", "操作成功", true, "/member/Bid_Product_add.aspx?BID=" + bidinfo.Bid_ID);


                    if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "商家:" + supplier_name + "创建招标,充值了" + Bid_Bond + "元保证金", Bid_Bond, ref strResult, supplier_name))
                    {
                        //pub.Msg("positive", "操作成功", "操作成功", true, "/member/Bid_Product_add.aspx?BID=" + bidinfo.Bid_ID);
                        //Response.Write("successbid|" + bidinfo.Bid_ID);
                        Response.Write("" + bidinfo.Bid_ID + ",successbid");
                        Response.End();
                    }
                    //else if (bidinfo.Bid_Bond == 0) //保证金为0无需缴纳
                    //{
                    //    Response.Write("" + bidinfo.Bid_ID + ",successbid");
                    //    Response.End();
                    //}
                    else
                    {
                        //pub.Msg("error", "错误信息", strResult, false, "{back}");
                        Response.Write("false");
                        Response.End();
                    }
                }
                else
                {

                    //商家发布拍卖向投标保证金账户,充值所需金额
                    if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "商家:" + supplier_name + "创建拍卖,充值了" + Bid_Bond + "元保证金", Bid_Bond, ref strResult, supplier_name))
                    {
                        //pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_Product_add.aspx?BID=" + bidinfo.Bid_ID);
                        //Response.Write("successauction|" + bidinfo.Bid_ID);
                        Response.Write("" + bidinfo.Bid_ID + ",successauction");
                        Response.End();
                    }
                    else
                    {
                        //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                        Response.Write("false");
                        Response.End();
                    }

                    //}
                    //}
                    //pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_Product_add.aspx?BID=" + bidinfo.Bid_ID);
                }

            }
            else
            {
                //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                Response.Write("false");
            }

        }
        else
        {
            //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            Response.Write("false");
        }
    }


    public void EditBid(int Type)
    {
        int Bid_ID = tools.CheckInt(Request.Form["Bid_ID"]);
        int Bid_MemberID = tools.NullInt(Session["member_id"]);
        string Bid_MemberCompany = tools.CheckStr(Request.Form["Bid_MemberCompany"]);
        string Bid_Title = tools.CheckStr(Request.Form["Bid_Title"]);
        //DateTime Bid_EnterStartTime = tools.NullDate(Request.Form["Bid_EnterStartTime"]);
        //DateTime Bid_EnterEndTime = tools.NullDate(Request.Form["Bid_EnterEndTime"]);
        DateTime Bid_EnterStartTime = DateTime.Now;
        DateTime Bid_EnterEndTime = DateTime.Now;

        DateTime Bid_BidStartTime = tools.NullDate(Request.Form["Bid_BidStartTime"]);
        DateTime Bid_BidEndTime = tools.NullDate(Request.Form["Bid_BidEndTime"]);
        double Bid_Bond = tools.CheckFloat(Request.Form["Bid_Bond"]);
        int Bid_Number = tools.CheckInt(Request.Form["Bid_Number"]);
        int Bid_ProductType = tools.CheckInt(Request.Form["Bid_ProductType"]);
        string Bid_Content = Request.Form["Bid_Content"];
        int Bid_ExcludeSupplierID = tools.NullInt(Session["supplier_id"]);
        DateTime Bid_DeliveryTime = tools.NullDate(Request.Form["Bid_DeliveryTime"]);
        if (Bid_MemberID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }

        BidInfo entity = GetBidByID(Bid_ID);
        if (entity != null)
        {
            if (entity.Bid_MemberID != Bid_MemberID)
            {
                pub.Msg("error", "错误信息", "操作失败，您没有权限", false, "{back}");
            }

            if (Bid_Title.Length == 0)
            {
                pub.Msg("error", "错误信息", "请填写公告标题", false, "{back}");
            }
            if (Bid_MemberCompany.Length == 0)
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "请填写采购商名称", false, "{back}");
                }
                else
                {
                    pub.Msg("error", "错误信息", "请填写拍卖用户名称", false, "{back}");
                }
            }

            //if (DateTime.Compare(Bid_EnterStartTime, Bid_EnterEndTime) > 0)
            //{
            //    pub.Msg("error", "错误信息", "报名时间有误", false, "{back}");
            //}

            //if (DateTime.Compare(Bid_EnterEndTime, Bid_BidStartTime)>=0)
            //{
            //    if (Type == 0)
            //    {
            //        pub.Msg("error", "错误信息", "报价时间应晚于报名结束时间", false, "{back}");
            //    }
            //    else
            //    {
            //        pub.Msg("error", "错误信息", "竞价时间应晚于报名结束时间", false, "{back}");
            //    }
            //}
            if (DateTime.Compare(Bid_BidStartTime, Bid_BidEndTime) > 0)
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "报价时间有误", false, "{back}");
                }
                else
                {
                    pub.Msg("error", "错误信息", "竞价时间有误", false, "{back}");
                }
            }

            if (Bid_Number <= 0)
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "请填写报价轮次", false, "{back}");
                }
                else
                {
                    pub.Msg("error", "错误信息", "请填写竞价轮次", false, "{back}");
                }
            }
            if (Bid_Bond < 0)
            {
                pub.Msg("error", "错误信息", "请确保保证金金额不小于零", false, "{back}");
            }

            entity.Bid_ID = Bid_ID;
            entity.Bid_MemberID = Bid_MemberID;
            entity.Bid_MemberCompany = Bid_MemberCompany;
            entity.Bid_Title = Bid_Title;
            entity.Bid_EnterStartTime = Bid_EnterStartTime;
            entity.Bid_EnterEndTime = Bid_EnterEndTime;
            entity.Bid_BidStartTime = Bid_BidStartTime;
            entity.Bid_BidEndTime = Bid_BidEndTime;
            entity.Bid_Bond = Bid_Bond;
            entity.Bid_Number = Bid_Number;
            entity.Bid_ProductType = Bid_ProductType;
            entity.Bid_Content = Bid_Content;
            entity.Bid_ExcludeSupplierID = Bid_ExcludeSupplierID;
            entity.Bid_DeliveryTime = Bid_DeliveryTime;

            if (MyBid.EditBid(entity, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            {
                if (Type == 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_view.aspx?BID=" + entity.Bid_ID);
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?BID=" + entity.Bid_ID);
                }


            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 发布招标
    /// </summary>
    public void ReleaseBid(int Type)
    {
        int Bid_ID = tools.CheckInt(Request["Bid_ID"]);
        int Bid_MemberID = tools.NullInt(Session["member_id"]);

        if (Bid_MemberID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }

        BidInfo entity = GetBidByID(Bid_ID);
        if (entity != null)
        {
            if (entity.Bid_MemberID != Bid_MemberID)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            if (entity.Bid_Status > 0)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            if (entity.Bid_IsAudit == 3)
            {
                pub.Msg("error", "错误信息", "" + entity.Bid_Title + "招标项目已被冻结", false, "{back}");
                return;
            }
            if (entity.Bid_IsAudit == 2)
            {
                pub.Msg("error", "错误信息", "" + entity.Bid_Title + "招标项目审核失败", false, "{back}");
                return;
            }
            entity.Bid_Status = 1;
            if (MyBid.EditBid(entity, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            {
                if (Type == 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_view.aspx?BID=" + entity.Bid_ID);
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?BID=" + entity.Bid_ID);
                }

            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    /// <summary>
    /// 撤销发布
    /// </summary>
    public void RevokeBid(int Type)
    {
        int Bid_ID = tools.CheckInt(Request["Bid_ID"]);
        int Bid_MemberID = tools.NullInt(Session["member_id"]);

        if (Bid_MemberID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }

        BidInfo entity = GetBidByID(Bid_ID);
        if (entity != null)
        {
            if (entity.Bid_MemberID != Bid_MemberID)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            if (entity.Bid_Status != 1)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) >= 0)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            entity.Bid_Status = 2;
            if (MyBid.EditBid(entity, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            {

                if (Type == 0)
                {
                    NotWinTender(entity.Bid_ID, 0, "[" + entity.Bid_Title + "]招标项目已撤销,返还保证金", entity.Bid_Bond);
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_view.aspx?BID=" + entity.Bid_ID);
                }
                else
                {
                    NotWinTender(entity.Bid_ID, 0, "[" + entity.Bid_Title + "]拍卖项目已撤销,返还保证金", entity.Bid_Bond);
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?BID=" + entity.Bid_ID);
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //我是买家招标拍卖列表
    public void Bid_MemberList(int Type)
    {
        int member_id = tools.NullInt(Session["member_id"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        string date_start = tools.CheckStr(Request["date_start"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        string Status = tools.CheckStr(Request["Status"]);

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"974\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        tmp_head = tmp_head + "<td width=\"228\" class=\"name\">公告标题</td>";
        //tmp_head = tmp_head + "<td width=\"229\" class=\"name\">报名时间</td>";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<td width=\"179\" class=\"name\">报价时间</td>";
        }
        else
        {
            tmp_head = tmp_head + "<td width=\"179\" class=\"name\">竞价时间</td>";
        }
        tmp_head = tmp_head + "<td width=\"106\" class=\"name\">当前状态</td>";
        tmp_head = tmp_head + "<td width=\"237\" class=\"name\">操作</td>";
        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        if (Type == 0)
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无招标</td></tr>";
        }
        else
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无拍卖</td></tr>";
        }
        if (curpage < 1)
        {
            curpage = 1;
        }

        page_url = "?keyword=" + keyword + "&date_start=" + date_start;

        IList<BidInfo> entitys = MyBid.GetListBids(member_id, "", Status, Type, PageSize, curpage, keyword, date_start, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));

        PageInfo page = MyBid.GetPageInfoList(member_id, "", Status, Type, PageSize, curpage, keyword, date_start, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            foreach (BidInfo entity in entitys)
            {
                //if (entity.Bid_IsShow==1)
                //{


                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td title=\"" + entity.Bid_Title + "\">" + tools.CutStr(entity.Bid_Title, 18) + "</td>";
                //tmp_list = tmp_list + "<td>" + entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                //tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd") + "<span style=\"color:#ff6600\">至</span>" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + Bid_Status(entity) + "</td>";

                if (Type == 0)
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/member/bid_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>&nbsp;&nbsp;";

                    //if (entity.Bid_OrdersSN.Length>0)
                    //{
                    //    tmp_list = tmp_list + "<td><span><a href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Bid_OrdersSN + "\" target=\"_blank\">查看订单</a>";  
                    //}

                    //Bid_Status:0待发布  Bid_IsAudit:0未审核
                    if (entity.Bid_Status == 0 || entity.Bid_IsAudit == 0)
                    {
                        tmp_list = tmp_list + "<a href=\"/member/bid_do.aspx?action=bid_isshow&Bid_ID=" + entity.Bid_ID + "&Bid_Type=0\">删除</a>&nbsp;&nbsp;";
                    }

                    //if (entity.Bid_IsAudit==0)
                    //{
                    //     tmp_list = tmp_list + "<a href=\"/member/bid_do.aspx?action=bid_isshow&Bid_ID=" + entity.Bid_ID + "&Bid_Type=0\">删除</a>";
                    //}


                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "&nbsp;&nbsp;<a href=\"/member/tender_list.aspx?BID=" + entity.Bid_ID + "\">查看报价</a>";
                    }
                }
                else
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/supplier/auction_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>&nbsp;&nbsp;";
                    if ((entity.Bid_OrdersSN.Trim().ToString().Length > 0))
                    {
                        tmp_list = tmp_list + "<a href=\"/supplier/order_detail.aspx?orders_sn=" + entity.Bid_OrdersSN + "\" target=\"_blank\">查看订单</a>&nbsp;&nbsp;";
                    }

                    //Bid_Status:0待发布  Bid_IsAudit:0未审核
                    if (entity.Bid_Status == 0 || entity.Bid_IsAudit == 0)
                    {
                        tmp_list = tmp_list + "<a href=\"/member/bid_do.aspx?action=bid_isshow&Bid_ID=" + entity.Bid_ID + "&Bid_Type=1\">删除</a>&nbsp;&nbsp;";
                    }

                    //tmp_list = tmp_list + "<td><span><a href=\"/member/bid_do.aspx?actionBID=" + entity.Bid_ID + "&Type=0\">查看</a>";

                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "<a href=\"/supplier/auction_tender_list.aspx?BID=" + entity.Bid_ID + "\">查看竞价</a>";
                    }
                }

                tmp_list = tmp_list + "</span></td>";
                tmp_list = tmp_list + "</tr>";
                //}
            }

            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }
    public void Bid_MemberIndexList(int Type, int pagesize)
    {

        int curpage = 1;
        int member_id = tools.NullInt(Session["member_id"]);

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        if (Type == 0)
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无招标</td></tr>";
        }
        else
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无拍卖</td></tr>";
        }
        if (curpage < 1)
        {
            curpage = 1;
        }

        IList<BidInfo> entitys = MyBid.GetListBids(member_id, "", "", Type, pagesize, curpage, "", "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
        if (entitys != null)
        {
            tmp_list = "";
            foreach (BidInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Bid_Title + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                tmp_list = tmp_list + "<td>" + Bid_Status(entity) + "</td>";

                if (Type == 0)
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/member/bid_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>";

                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"/member/tender_list.aspx?BID=" + entity.Bid_ID + "\">查看报价</a>";
                    }
                }
                else
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/supplier/auction_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>";

                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"/supplier/auction_tender_list.aspx?BID=" + entity.Bid_ID + "\">查看竞价</a>";
                    }
                }

                tmp_list = tmp_list + "</span></td>";
                tmp_list = tmp_list + "</tr>";
            }

            Response.Write(tmp_list);
        }
        else
        {
            Response.Write(tmp_list);
        }
    }

    public void Bid_MemberIndexList(int Type)
    {
        int PageSize = 5;
        int curpage = 1;
        int member_id = tools.NullInt(Session["member_id"]);

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";
        if (Type == 0)
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无招标</td></tr>";
        }
        else
        {
            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无拍卖</td></tr>";
        }
        if (curpage < 1)
        {
            curpage = 1;
        }

        IList<BidInfo> entitys = MyBid.GetListBids(member_id, "", "", Type, PageSize, curpage, "", "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
        if (entitys != null)
        {
            tmp_list = "";
            foreach (BidInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Bid_Title + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm") + "至" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
                tmp_list = tmp_list + "<td>" + Bid_Status(entity) + "</td>";

                if (Type == 0)
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/member/bid_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>";

                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"/member/tender_list.aspx?BID=" + entity.Bid_ID + "\">查看报价</a>";
                    }
                }
                else
                {
                    tmp_list = tmp_list + "<td><span><a href=\"/supplier/auction_view.aspx?BID=" + entity.Bid_ID + "\">查看</a>";

                    if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) > 0 && entity.Bid_Status == 1 && entity.Bid_IsAudit == 1)
                    {
                        tmp_list = tmp_list + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"/supplier/auction_tender_list.aspx?BID=" + entity.Bid_ID + "\">查看竞价</a>";
                    }
                }

                tmp_list = tmp_list + "</span></td>";
                tmp_list = tmp_list + "</tr>";
            }

            Response.Write(tmp_list);
        }
        else
        {
            Response.Write(tmp_list);
        }
    }

    /// <summary>
    /// 招标读取列表
    /// </summary>
    /// <param name="MemberID">会员</param>
    /// <param name="IsAudit">审核</param>
    /// <param name="Status">状态</param>
    /// <param name="Type">0招标1拍卖</param>
    /// <param name="PageSize">分页</param>
    /// <param name="CurrentPage">展示数</param>
    /// <param name="keyword">关键词</param>
    /// <param name="date">时间</param>
    /// <returns></returns>
    public QueryInfo BidsList(int MemberID, string IsAudit, string Status, int Type, int PageSize, int CurrentPage, string keyword, string date)
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

        return Query;
    }
    //0 代表招标;1 代表拍卖
    public void Bid_IndexList(int Type)
    {
        int member_id = 0;
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        string Status = "1";

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<thead>";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"400\">招标公告</td>";
            tmp_head = tmp_head + "<td width=\"200\">招标单位</td>";
            tmp_head = tmp_head + "<td width=\"100\">交货时间</td>";
            //tmp_head = tmp_head + "<td width=\"150\">报名结束时间</td>";
            tmp_head = tmp_head + "<td width=\"150\">报价开始时间</td>";
            tmp_head = tmp_head + "<td width=\"150\">报价结束时间</td>";
            tmp_head = tmp_head + "<td width=\"100\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_head = tmp_head + "</thead>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无招标信息</td></tr>";
        }
        else
        {
            tmp_head = tmp_head + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<thead>";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"500\">拍卖公告</td>";
            tmp_head = tmp_head + "<td width=\"200\">拍卖用户</td>";
            tmp_head = tmp_head + "<td width=\"150\">报价开始时间</td>";
            tmp_head = tmp_head + "<td width=\"150\">报价结束时间</td>";
            tmp_head = tmp_head + "<td width=\"100\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_head = tmp_head + "</thead>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无拍卖信息</td></tr>";
        }


        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";



        if (curpage < 1)
        {
            curpage = 1;
        }

        page_url = "?keyword=" + keyword;

        IList<BidInfo> entitys = MyBid.GetListBids(member_id, "1", Status, Type, PageSize, curpage, keyword, "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));

        PageInfo page = MyBid.GetPageInfoList(member_id, "1", Status, Type, PageSize, curpage, keyword, "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "<tbody>";
            foreach (BidInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td>" + entity.Bid_Title + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_MemberCompany + "</td>";
                if (Type == 0)
                {
                    tmp_list = tmp_list + "<td>" + entity.Bid_DeliveryTime.ToString("yyyy-MM-dd") + "</td>";
                }
                if (Type == 0)
                {
                    tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd") + "</td>";
                }
                else
                {

                    tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd") + "</td>";
                }

                tmp_list = tmp_list + "<td>" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd") + "</td>";



                if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) <= 0)
                {

                    if (Type == 0)
                    {
                        tmp_list = tmp_list + "<td><a href=\"/bid/view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }
                    if (Type == 1)
                    {
                        tmp_list = tmp_list + "<td><a href=\"/bid/auction_view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }

                }
                else if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                {
                    if (Type == 0)
                    {
                        tmp_list = tmp_list + "<td><a href=\"/bid/view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }
                    else
                    {
                        tmp_list = tmp_list + "<td><a href=\"/bid/auction_view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }

                }
                else
                {
                    if (Type == 0)
                    {
                        tmp_list = tmp_list + "<td><a ><img src=\"/images/brn4.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }
                    else
                    {
                        tmp_list = tmp_list + "<td><a ><img src=\"/images/brn4.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }

                }
                tmp_list = tmp_list + "</tr>";
            }
            tmp_list = tmp_list + "</tbody>";
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }

    }

    //首页招标拍卖列表
    public void Bid_Indexs(int Type)
    {
        int member_id = 0;
        string keyword = "";
        string page_url = "";
        int curpage = 1;
        int PageSize = 8;
        string Status = "1";

        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {


            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无招标信息</td></tr>";
        }
        else
        {


            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无拍卖信息</td></tr>";
        }






        if (curpage < 1)
        {
            curpage = 1;
        }



        IList<BidInfo> entitys = MyBid.GetListBids(member_id, "1", Status, Type, PageSize, curpage, keyword, "", pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));




        if (entitys != null)
        {
            tmp_list = "";
            bool Meber_Islogined = tools.NullStr(Session["member_logined"]) == "True";
            int memberber_id = tools.NullInt(Session["member_id"]);
            ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
            int supplier_id = tools.NullInt(DBHelper.ExecuteScalar(" select Member_SupplierID from Member where Member_ID=" + memberber_id + ""));
            //int supplier_id = tools.NullInt(Session["supplier_id"]);
            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(supplier_id);
            int Supplier_AuditStatus = -1;
            if (supplierinfo != null)
            {
                Supplier_AuditStatus = supplierinfo.Supplier_AuditStatus;
            }

            foreach (BidInfo entity in entitys)
            {
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td title=" + entity.Bid_Title + ">" + tools.CutStr(entity.Bid_Title, 18) + "</td>";
                //招标是买家
                int Bid_Product_AmountSum = 0;
                int Bid_Product_Amount = 0;
                string Bid_Product_Unit = "";

                if ((Supplier_AuditStatus == 1) && (Meber_Islogined))
                {
                    tmp_list = tmp_list + "<td title=" + entity.Bid_MemberCompany + ">" + tools.CutStr(entity.Bid_MemberCompany, 18) + "</td>";
                }
                else
                {
                    tmp_list = tmp_list + "<td title=" + "登录可见" + "><span style=\"color:#ff6600\">" + "登录可见" + "</span></td>";
                }
                if (Type == 0)
                {

                    QueryInfo Query = new QueryInfo();
                    Query.PageSize = 0;
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidProductInfo.Bid_BidID", "=", entity.Bid_ID.ToString()));
                    Query.OrderInfos.Add(new OrderInfo("BidProductInfo.Bid_Product_ID", "ASC"));


                    IList<BidProductInfo> bidproductInfos = MyBidProduct.GetBidProducts(Query);
                    //if (bidproductInfos!=null)
                    //{
                    //    foreach (var bidproductInfo in bidproductInfos)
                    //    {
                    //      //  if ((Bid_Product_Unit=="吨")|| (Bid_Product_Unit=="T"))
                    //      //  {

                    //      //  }
                    //      //Bid_Product_Amount =  bidproductInfo.Bid_Product_Amount;
                    //      //Bid_Product_AmountSum += Bid_Product_Amount;


                    //      Bid_Product_Unit  =  bidproductInfo.Bid_Product_Unit;
                    //    }  
                    //}
                    if (bidproductInfos != null)
                    {
                        foreach (var bidproductInfo in bidproductInfos)
                        {
                            Bid_Product_Amount = bidproductInfo.Bid_Product_Amount;
                            Bid_Product_AmountSum += Bid_Product_Amount;
                            Bid_Product_Unit = bidproductInfo.Bid_Product_Unit;
                        }
                    }

                    tmp_list = tmp_list + "<td>" + Bid_Product_AmountSum + "(" + Bid_Product_Unit + ")</td>";


                }
                tmp_list = tmp_list + "<td>" + entity.Bid_BidStartTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td>" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd") + "</td>";

                //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) <= 0)
                //{
                //    if (Type == 0)
                //    {
                //        tmp_list = tmp_list + "<td><a href=\"/bid/view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn2.jpg\" width=\"79\" height=\"28\" /></a></td>";
                //    }
                //    else
                //    {
                //        tmp_list = tmp_list + "<td><a href=\"/bid/auction_view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn2.jpg\" width=\"79\" height=\"28\" /></a></td>";
                //    }

                //}
                //else 
                if (DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) <= 0)
                {
                    if ((Supplier_AuditStatus == 1) && (Meber_Islogined))
                    {
                        if (Type == 0)
                        {
                            tmp_list = tmp_list + "<td><a href=\"/bid/view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                        }
                        else
                        {
                            tmp_list = tmp_list + "<td><a href=\"/bid/auction_view.aspx?BID=" + entity.Bid_ID + "\"><img src=\"/images/brn3.jpg\" width=\"79\" height=\"28\" /></a></td>";
                        }
                    }
                    else
                    {
                        if (Type == 0)
                        {
                            tmp_list = tmp_list + "<td><a href=\"/#top\"><img src=\"/images/brn2_1.jpg\" width=\"79\" height=\"28\" /></a></td>";
                        }
                        else
                        {
                            tmp_list = tmp_list + "<td><a href=\"/#top\"><img src=\"/images/brn2_1.jpg\" width=\"79\" height=\"28\" /></a></td>";
                        }
                    }


                }
                else
                {
                    if (Type == 0)
                    {
                        tmp_list = tmp_list + "<td><a><img src=\"/images/brn4.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }
                    else
                    {
                        tmp_list = tmp_list + "<td><a><img src=\"/images/brn4.jpg\" width=\"79\" height=\"28\" /></a></td>";
                    }

                }
                tmp_list = tmp_list + "</tr>";
            }

            Response.Write(tmp_list);

        }
        else
        {
            Response.Write(tmp_list);
        }

    }

    public string Bid_Status(BidInfo entity)
    {
        if (entity != null)
        {
            if (entity.Bid_Status == 0)
            {
                return "待发布";
            }
            else if (entity.Bid_Status == 1)
            {
                if (entity.Bid_IsAudit == 0)
                {
                    return "待审核";
                }
                else if (entity.Bid_IsAudit == 1)
                {
                    if (DateTime.Compare(DateTime.Now, entity.Bid_EnterStartTime) < 0)
                    {
                        return "未开始";
                    }
                    else if (DateTime.Compare(DateTime.Now, entity.Bid_EnterStartTime) >= 0 && DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) < 0)
                    {
                        return "报名中";
                    }
                    else if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) > 0 && DateTime.Compare(DateTime.Now, entity.Bid_BidStartTime) < 0)
                    {
                        return "";
                    }
                    else if (DateTime.Compare(DateTime.Now, entity.Bid_BidStartTime) >= 0 && DateTime.Compare(DateTime.Now, entity.Bid_BidEndTime) < 0)
                    {
                        return "报价中";
                    }
                    else
                    {
                        if (entity.Bid_SupplierID > 0)
                        {
                            return "已完成";
                        }
                        else
                        {
                            if (entity.Bid_Type == 0)
                            {
                                return "待选中标方";
                            }
                            else
                            {
                                return "待选买家";
                            }
                        }
                    }

                }
                else
                {
                    return "审核未通过";
                }

            }
            else if (entity.Bid_Status == 2)
            {
                return "已撤销";
            }
            else
            {
                return "流拍";
            }
        }
        else
        {
            return "--";
        }
    }

    /// <summary>
    /// 添加商品
    /// </summary>
    /// <param name="Type"></param>
    public void AddBidProduct(int Type)
    {
        int Bid_Product_ID = tools.CheckInt(Request.Form["Bid_Product_ID"]);
        int Bid_BidID = tools.CheckInt(Request.Form["Bid_BidID"]);
        int Bid_Product_Sort = tools.CheckInt(Request.Form["Bid_Product_Sort"]);
        string Bid_Product_Code = tools.CheckStr(Request.Form["Bid_Product_Code"]);
        string Bid_Product_Name = tools.CheckStr(Request.Form["Bid_Product_Name"]);
        string Bid_Product_Spec = tools.CheckStr(Request.Form["Bid_Product_Spec"]);
        string Bid_Product_Brand = tools.CheckStr(Request.Form["Bid_Product_Brand"]);
        string Bid_Product_Unit = tools.CheckStr(Request.Form["Bid_Product_Unit"]);
        int Bid_Product_Amount = tools.CheckInt(Request.Form["Bid_Product_Amount"]);
        string Bid_Product_Delivery = tools.CheckStr(Request.Form["Bid_Product_Delivery"]);
        string Bid_Product_Remark = tools.CheckStr(Request.Form["Bid_Product_Remark"]);
        double Bid_Product_StartPrice = tools.CheckFloat(Request.Form["Bid_Product_StartPrice"]);
        string Bid_Product_Img = tools.CheckStr(Request.Form["Bid_Product_Img"]);
        int Bid_Product_ProductID = tools.CheckInt(Request.Form["Bid_Product_ProductID"]);
        int member_id = tools.NullInt(Session["member_id"]);
        if (member_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        if (Bid_BidID <= 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "招标不存在", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "拍卖不存在", false, "{back}");
            }

        }
        if (Check_MemberAndBid(Bid_BidID, Type))
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "不可添加", false, "/member/bid_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "不可添加", false, "/supplier/auction_list.aspx");
            }

        }

        //if(Bid_Product_Code.Length==0)
        //{
        //    if (Type == 0)
        //    {
        //        pub.Msg("error", "错误信息", "请填写物料编号", false, "{back}");
        //    }
        //    else
        //    {
        //        pub.Msg("error", "错误信息", "请填写产品编号", false, "{back}");
        //    }

        //}
        if (Bid_Product_Name.Length == 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");

            }
            else
            {
                pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            }

        }

        if (Bid_Product_Spec.Length == 0)
        {

            pub.Msg("error", "错误信息", "请填写型号规格", false, "{back}");
        }

        //if (Bid_Product_Brand.Length == 0)
        //{
        //    pub.Msg("error", "错误信息", "请填写品牌名称", false, "{back}");
        //}

        if (Bid_Product_Unit.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写计量单位", false, "{back}");
        }

        if (Bid_Product_Amount <= 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写采购数量", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "请填写产品数量", false, "{back}");
            }


        }

        if (Bid_Product_Delivery.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写物流信息", false, "{back}");
        }

        if (Type == 1)
        {
            if (Bid_Product_StartPrice <= 0)
            {
                pub.Msg("error", "错误信息", "请填写起标价格", false, "{back}");
            }

            if (Bid_Product_Img.Length == 0)
            {
                pub.Msg("error", "错误信息", "请上传产品图片", false, "{back}");
            }

            if (AuctionProductCount(Bid_BidID) > 0)
            {
                pub.Msg("error", "错误信息", "拍卖商品只能添加一个", false, "{back}");
            }
            if (Bid_Product_ProductID <= 0)
            {
                pub.Msg("error", "错误信息", "请选择拍卖商品", false, "{back}");
            }
        }

        BidProductInfo entity = new BidProductInfo();
        entity.Bid_Product_ID = Bid_Product_ID;
        entity.Bid_BidID = Bid_BidID;
        entity.Bid_Product_Sort = Bid_Product_Sort;
        entity.Bid_Product_Code = Bid_Product_Code;
        entity.Bid_Product_Name = Bid_Product_Name;
        entity.Bid_Product_Spec = Bid_Product_Spec;
        entity.Bid_Product_Brand = Bid_Product_Brand;
        entity.Bid_Product_Unit = Bid_Product_Unit;
        entity.Bid_Product_Amount = Bid_Product_Amount;
        entity.Bid_Product_Delivery = Bid_Product_Delivery;
        entity.Bid_Product_Remark = Bid_Product_Remark;
        entity.Bid_Product_StartPrice = Bid_Product_StartPrice;
        entity.Bid_Product_Img = Bid_Product_Img;
        entity.Bid_Product_ProductID = Bid_Product_ProductID;

        if (MyBidProduct.AddBidProduct(entity))
        {
            if (Type == 0)
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/member/Bid_view.aspx?list=1&BID=" + Bid_BidID);
            }
            else
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?list=1&BID=" + Bid_BidID);
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 修改商品
    /// </summary>
    /// <param name="Type"></param>
    public void EditBidProduct(int Type)
    {

        int Bid_Product_ID = tools.CheckInt(Request.Form["ProductID"]);
        int Bid_BidID = tools.CheckInt(Request.Form["Bid_BidID"]);
        int Bid_Product_Sort = tools.CheckInt(Request.Form["Bid_Product_Sort"]);
        string Bid_Product_Code = tools.CheckStr(Request.Form["Bid_Product_Code"]);
        string Bid_Product_Name = tools.CheckStr(Request.Form["Bid_Product_Name"]);
        string Bid_Product_Spec = tools.CheckStr(Request.Form["Bid_Product_Spec"]);
        string Bid_Product_Brand = tools.CheckStr(Request.Form["Bid_Product_Brand"]);
        string Bid_Product_Unit = tools.CheckStr(Request.Form["Bid_Product_Unit"]);
        int Bid_Product_Amount = tools.CheckInt(Request.Form["Bid_Product_Amount"]);
        string Bid_Product_Delivery = tools.CheckStr(Request.Form["Bid_Product_Delivery"]);
        string Bid_Product_Remark = tools.CheckStr(Request.Form["Bid_Product_Remark"]);
        double Bid_Product_StartPrice = tools.CheckFloat(Request.Form["Bid_Product_StartPrice"]);
        string Bid_Product_Img = tools.CheckStr(Request.Form["Bid_Product_Img"]);

        int member_id = tools.NullInt(Session["member_id"]);
        if (member_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        if (Bid_BidID <= 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "招标不存在", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "拍卖不存在", false, "{back}");
            }
        }

        if (Check_MemberAndBid(Bid_BidID, Type))
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "不可修改", false, "/member/bid_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "不可修改", false, "/supplier/auction_list.aspx");
            }
        }

        if (Bid_Product_Name.Length == 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");

            }
            else
            {
                pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            }

        }

        if (Bid_Product_Spec.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写型号规格", false, "{back}");
        }


        if (Bid_Product_Unit.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写计量单位", false, "{back}");
        }

        if (Bid_Product_Amount <= 0)
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "请填写采购数量", false, "{back}");
            }
            else
            {
                pub.Msg("error", "错误信息", "请填写产品数量", false, "{back}");
            }


        }

        if (Bid_Product_Delivery.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写物流信息", false, "{back}");
        }

        if (Type == 1)
        {
            if (Bid_Product_StartPrice <= 0)
            {
                pub.Msg("error", "错误信息", "请填写起标价格", false, "{back}");
            }

            if (Bid_Product_Img.Length == 0)
            {
                pub.Msg("error", "错误信息", "请上传产品图片", false, "{back}");
            }
        }

        BidProductInfo entity = GetBidProductByID(Bid_Product_ID);
        entity.Bid_Product_ID = Bid_Product_ID;
        entity.Bid_BidID = Bid_BidID;
        entity.Bid_Product_Sort = Bid_Product_Sort;
        entity.Bid_Product_Code = Bid_Product_Code;
        entity.Bid_Product_Name = Bid_Product_Name;
        entity.Bid_Product_Spec = Bid_Product_Spec;
        entity.Bid_Product_Brand = Bid_Product_Brand;
        entity.Bid_Product_Unit = Bid_Product_Unit;
        entity.Bid_Product_Amount = Bid_Product_Amount;
        entity.Bid_Product_Delivery = Bid_Product_Delivery;
        entity.Bid_Product_Remark = Bid_Product_Remark;
        entity.Bid_Product_StartPrice = Bid_Product_StartPrice;
        entity.Bid_Product_Img = Bid_Product_Img;


        if (MyBidProduct.EditBidProduct(entity))
        {
            if (Type == 0)
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/member/Bid_view.aspx?list=1&BID=" + Bid_BidID);
            }
            else
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?list=1&BID=" + Bid_BidID);
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public int AuctionProductCount(int BID)
    {
        BidInfo entity = GetBidByID(BID);

        if (entity != null)
        {
            if (entity.BidProducts != null)
            {
                return entity.BidProducts.Count;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    public void DelBidProduct(int Type)
    {
        int ProductID = tools.CheckInt(Request["ProductID"]);
        int BidID = tools.CheckInt(Request["BidID"]);

        if (!Check_MemberAndBid(BidID, Type))
        {
            MyBidProduct.DelBidProduct(ProductID);
            if (Type == 0)
            {
                Response.Redirect("/member/Bid_view.aspx?list=1&BID=" + BidID);
            }
            else
            {
                Response.Redirect("/supplier/auction_view.aspx?list=1&BID=" + BidID);
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 商品列表
    /// </summary>
    /// <param name="entitys"></param>
    /// <param name="Status"></param>
    /// <param name="Type"></param>
    public void BidProductList(IList<BidProductInfo> entitys, int Status, int Type)
    {
        if (Type == 0)
        {
            if (entitys != null)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    Response.Write("<tr>");
                    //Response.Write("<td>" + entity.Bid_Product_Sort + "</td>");
                    //Response.Write("<td alt=\"" + entity.Bid_Product_Code + "\"  title=\"" + entity.Bid_Product_Code + "\">" + entity.Bid_Product_Code + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Name + "\"  title=\"" + entity.Bid_Product_Name + "\">" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Spec + "\"  title=\"" + entity.Bid_Product_Spec + "\">" + entity.Bid_Product_Spec + "</td>");
                    //Response.Write("<td alt=\"" + entity.Bid_Product_Brand + "\"  title=\"" + entity.Bid_Product_Brand + "\">" + entity.Bid_Product_Brand + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Unit + "\"  title=\"" + entity.Bid_Product_Unit + "\">" + tools.CutStr( entity.Bid_Product_Unit,20) + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Amount + "\"  title=\"" + entity.Bid_Product_Amount + "\">" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Delivery + "\"  title=\"" + entity.Bid_Product_Delivery + "\">" + entity.Bid_Product_Delivery + "</td>");
                    Response.Write("<td alt=\"" + entity.Bid_Product_Remark + "\"  title=\"" + entity.Bid_Product_Remark + "\">" + entity.Bid_Product_Remark + "</td>");
                    Response.Write("<td>");

                    if (Status == 0)
                    {
                        Response.Write("<span><a href=\"/member/bid_product_edit.aspx?ProductID=" + entity.Bid_Product_ID + "\">修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"confirmdelete('/member/bid_do.aspx?action=move&ProductID=" + entity.Bid_Product_ID + "&BidID=" + entity.Bid_BidID + "');\">删除</a></span>");
                    }
                    //else
                    //{
                    Response.Write("<span><a href=\"/member/bid_product_view.aspx?ProductID=" + entity.Bid_Product_ID + "\">查看详情</a><span>");
                    //}
                    Response.Write("</td>");
                    Response.Write("</tr>");
                }
            }
            else
            {
                Response.Write("<tr><td colspan=\"7\">暂无商品清单</td></tr>");
            }
        }
        else
        {
            if (entitys != null)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    Response.Write("<tr>");
                    //Response.Write("<td>" + entity.Bid_Product_Code + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Spec + "</td>");
                    //Response.Write("<td>" + entity.Bid_Product_Brand + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("</tr>");
                }
            }
            else
            {
                Response.Write("<tr><td colspan=\"4\">暂无商品清单</td></tr>");
            }
        }
    }

    /// <summary>
    /// 拍卖商品列表
    /// </summary>
    /// <param name="entitys"></param>
    /// <param name="Status"></param>
    /// <param name="Type"></param>
    public void AuctionProductList(IList<BidProductInfo> entitys, int Status, int Type)
    {
        if (Type == 0)
        {
            if (entitys != null)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    Response.Write("<tr>");

                    //Response.Write("<td>" + entity.Bid_Product_Code + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Spec + "</td>");
                    //Response.Write("<td>" + entity.Bid_Product_Brand + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Delivery + "</td>");
                    Response.Write("<td><span><a>" + pub.FormatCurrency(entity.Bid_Product_StartPrice) + "</a></span></td>");
                    Response.Write("<td>" + entity.Bid_Product_Remark + "</td>");
                    Response.Write("<td>");

                    if (Status == 0)
                    {
                        Response.Write("<span><a href=\"/supplier/auction_product_edit.aspx?ProductID=" + entity.Bid_Product_ID + "\">修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"confirmdelete('/supplier/auction_do.aspx?action=move&ProductID=" + entity.Bid_Product_ID + "&BidID=" + entity.Bid_BidID + "');\">删除</a></span>");
                    }
                    //else
                    //{
                    //    Response.Write("<span><a href=\"/supplier/auction_product_view.aspx?ProductID=" + entity.Bid_Product_ID + "\">查看详情</a><span>");
                    //}
                    Response.Write("<span><a href=\"/supplier/auction_product_view.aspx?ProductID=" + entity.Bid_Product_ID + "\">查看详情</a><span>");
                    Response.Write("</td>");
                    Response.Write("</tr>");
                }
            }
            else
            {
                Response.Write("<tr><td colspan=\"10\">暂无商品清单</td></tr>");
            }
        }
        else
        {
            if (entitys != null)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    Response.Write("<tr>");
                    //Response.Write("<td>" + entity.Bid_Product_Code + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Spec + "</td>");
                    //Response.Write("<td>" + entity.Bid_Product_Brand + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("<td>" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("<td><span><a>" + pub.FormatCurrency(entity.Bid_Product_Amount) + "</a></span></td>");
                    Response.Write("</tr>");
                }
            }
            else
            {
                Response.Write("<tr><td colspan=\"7\">暂无竞价商品清单</td></tr>");
            }
        }
    }
    public void BidProdcutView(IList<BidProductInfo> entitys, int Type)
    {
        if (entitys != null)
        {
            int i = 0;
            foreach (BidProductInfo entity in entitys)
            {
                i++;
                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }

                if (Type == 0)
                {
                    Response.Write("<td width=\"185px\">" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("<td width=\"155px\">" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("<td width=\"175px\" style=\"text-align:center;\">" + entity.Bid_Product_Spec + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Delivery + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Remark + "</td>");
                    //Response.Write("<td width=\"100\">" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("</tr>");
                }
                else
                {
                    Response.Write("<td width=\"185px\">" + entity.Bid_Product_Name + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Amount + "</td>");
                    Response.Write("<td width=\"155px\">" + entity.Bid_Product_Unit + "</td>");
                    Response.Write("<td width=\"175px\">" + pub.FormatCurrency(entity.Bid_Product_StartPrice) + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Delivery + "</td>");
                    Response.Write("<td width=\"175px\">" + entity.Bid_Product_Remark + "</td>");
                    //Response.Write("<td width=\"317\" style=\"text-align:left;\">" + entity.Bid_Product_Spec + "</td>");
                    Response.Write("</tr>");
                }

            }
        }
        else
        {
            if (Type == 0)
            {
                Response.Write("<tr><td colspan=\"4\">暂无招标商品</td></tr>");
            }
            else
            {
                Response.Write("<tr><td colspan=\"5\">暂无拍卖商品</td></tr>");
            }

        }
    }

    public void BidAttachmentsView(IList<BidAttachmentsInfo> entitys, int Type)
    {
        if (entitys != null)
        {
            int i = 0;
            foreach (BidAttachmentsInfo entity in entitys)
            {
                i++;
                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }

                if (Type == 0)
                {
                    Response.Write("<td style=\"width:410px;padding:none;\">" + entity.Bid_Attachments_Name + "</td>");
                    Response.Write("<td style=\"width:400px;padding:none;\" title=" + entity.Bid_Attachments_Remarks + " >" + tools.CutStr(entity.Bid_Attachments_Remarks, 30) + "</td>");
                    Response.Write("<td style=\"width:237px;padding:none;\"><a href=\"" + Application["Upload_Server_URL"] + entity.Bid_Attachments_Path + "\" target=\"_blank\" style=\"color: #ff6600;\">点击查看</a></td>");
                    Response.Write("</tr>");
                }
                else
                {
                    Response.Write("<td  style=\"width:410px;padding:0;\">" + entity.Bid_Attachments_Name + "</td>");
                    Response.Write("<td  style=\"width:400px;padding:0;\">" + entity.Bid_Attachments_Remarks + "</td>");
                    Response.Write("<td  style=\"width:237px;padding:0;\"><a href=\"" + Application["Upload_Server_URL"] + entity.Bid_Attachments_Path + "\" target=\"_blank\" style=\"color: #ff6600;\">点击查看</a></td>");
                    Response.Write("</tr>");
                }

            }
        }
    }
    /// <summary>
    /// 添加附件
    /// </summary>
    public virtual void AddBidAttachments()
    {
        int Bid_Attachments_ID = tools.CheckInt(Request.Form["Bid_Attachments_ID"]);
        int Bid_Attachments_Sort = tools.CheckInt(Request.Form["Bid_Attachments_Sort"]);
        string Bid_Attachments_Name = tools.CheckStr(Request.Form["Bid_Attachments_Name"]);
        string Bid_Attachments_format = tools.CheckStr(Request.Form["Bid_Attachments_format"]);
        string Bid_Attachments_Size = tools.CheckStr(Request.Form["Bid_Attachments_Size"]);
        string Bid_Attachments_Remarks = tools.CheckStr(Request.Form["Bid_Attachments_Remarks"]);
        string Bid_Attachments_Path = tools.CheckStr(Request.Form["Bid_Attachments_Path"]);
        int Bid_Attachments_BidID = tools.CheckInt(Request.Form["Bid_Attachments_BidID"]);

        int member_id = tools.NullInt(Session["member_id"]);
        if (member_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        int Type = 0;
        BidInfo bidInfo = GetBidByID(Bid_Attachments_BidID);

        if (bidInfo == null)
        {
            pub.Msg("error", "错误信息", "信息不存在", false, "{back}");
        }
        else
        {
            if (bidInfo.Bid_Type == 1)
            {
                Type = 1;
            }

        }
        if (Check_MemberAndBid(Bid_Attachments_BidID, Type))
        {
            if (Type == 0)
            {
                pub.Msg("error", "错误信息", "不可添加", false, "/member/bid_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "不可添加", false, "/supplier/auction_list.aspx");
            }

        }

        //if(Bid_Attachments_Sort<=0)
        //{
        //    pub.Msg("error", "错误信息", "请填写序号", false, "{back}");
        //}

        if (Bid_Attachments_Name.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写附件名称", false, "{back}");
        }

        //if (Bid_Attachments_format.Length == 0)
        //{
        //    pub.Msg("error", "错误信息", "请填写文件格式", false, "{back}");
        //}

        //if (Bid_Attachments_Size.Length == 0)
        //{
        //    pub.Msg("error", "错误信息", "请填写附件大小", false, "{back}");
        //}
        if (Bid_Attachments_Path.Length == 0)
        {
            pub.Msg("error", "错误信息", "请上传附件", false, "{back}");
        }
        BidAttachmentsInfo entity = new BidAttachmentsInfo();
        entity.Bid_Attachments_ID = Bid_Attachments_ID;
        entity.Bid_Attachments_Sort = Bid_Attachments_Sort;
        entity.Bid_Attachments_Name = Bid_Attachments_Name;
        entity.Bid_Attachments_format = Bid_Attachments_format;
        entity.Bid_Attachments_Size = Bid_Attachments_Size;
        entity.Bid_Attachments_Remarks = Bid_Attachments_Remarks;
        entity.Bid_Attachments_Path = Bid_Attachments_Path;
        entity.Bid_Attachments_BidID = Bid_Attachments_BidID;

        if (MyBidAttachments.AddBidAttachments(entity))
        {
            if (Type == 0)
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
            }
            else
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 修改附件
    /// </summary>
    public virtual void EditBidAttachments()
    {
        int Bid_Attachments_ID = tools.CheckInt(Request.Form["Bid_Attachments_ID"]);
        int Bid_Attachments_Sort = tools.CheckInt(Request.Form["Bid_Attachments_Sort"]);
        string Bid_Attachments_Name = tools.CheckStr(Request.Form["Bid_Attachments_Name"]);
        string Bid_Attachments_format = tools.CheckStr(Request.Form["Bid_Attachments_format"]);
        string Bid_Attachments_Size = tools.CheckStr(Request.Form["Bid_Attachments_Size"]);
        string Bid_Attachments_Remarks = tools.CheckStr(Request.Form["Bid_Attachments_Remarks"]);
        string Bid_Attachments_Path = tools.CheckStr(Request.Form["Bid_Attachments_Path"]);
        int Bid_Attachments_BidID = tools.CheckInt(Request.Form["Bid_Attachments_BidID"]);

        BidAttachmentsInfo entity = GetBidAttachmentsByID(Bid_Attachments_ID);
        if (entity != null)
        {
            int member_id = tools.NullInt(Session["member_id"]);
            if (member_id <= 0)
            {
                pub.Msg("error", "错误信息", "请先登录", false, "{back}");
            }
            int Type = 0;
            BidInfo bidInfo = GetBidByID(Bid_Attachments_BidID);
            if (bidInfo == null)
            {
                pub.Msg("error", "错误信息", "信息不存在", false, "{back}");
            }
            else
            {
                if (bidInfo.Bid_Type == 1)
                {
                    Type = 1;
                }
            }
            if (Check_MemberAndBid(Bid_Attachments_BidID, Type))
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "不可添加", false, "/member/bid_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "不可添加", false, "/supplier/auction_list.aspx");
                }

            }

            //if (Bid_Attachments_Sort <= 0)
            //{
            //    pub.Msg("error", "错误信息", "请填写序号", false, "{back}");
            //}

            if (Bid_Attachments_Name.Length == 0)
            {
                pub.Msg("error", "错误信息", "请填写附件名称", false, "{back}");
            }

            //if (Bid_Attachments_format.Length == 0)
            //{
            //    pub.Msg("error", "错误信息", "请填写文件格式", false, "{back}");
            //}

            //if (Bid_Attachments_Size.Length == 0)
            //{
            //    pub.Msg("error", "错误信息", "请填写附件大小", false, "{back}");
            //}
            if (Bid_Attachments_Path.Length == 0)
            {
                pub.Msg("error", "错误信息", "请上传附件", false, "{back}");
            }

            entity.Bid_Attachments_ID = Bid_Attachments_ID;
            entity.Bid_Attachments_Sort = Bid_Attachments_Sort;
            entity.Bid_Attachments_Name = Bid_Attachments_Name;
            entity.Bid_Attachments_format = Bid_Attachments_format;
            entity.Bid_Attachments_Size = Bid_Attachments_Size;
            entity.Bid_Attachments_Remarks = Bid_Attachments_Remarks;
            entity.Bid_Attachments_Path = Bid_Attachments_Path;
            entity.Bid_Attachments_BidID = Bid_Attachments_BidID;

            if (MyBidAttachments.EditBidAttachments(entity))
            {
                if (Type == 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 附件列表
    /// </summary>
    /// <param name="entitys"></param>
    /// <param name="Status"></param>
    /// <param name="Type"></param>
    public void BidAttachmentsList(IList<BidAttachmentsInfo> entitys, int Status, int Type)
    {
        if (entitys != null)
        {
            foreach (BidAttachmentsInfo entity in entitys)
            {
                Response.Write("<tr>");
                //Response.Write("<td>" + entity.Bid_Attachments_Sort + "</td>");
                Response.Write("<td>" + entity.Bid_Attachments_Name + "</td>");
                //Response.Write("<td>" + entity.Bid_Attachments_format + "</td>");
                //Response.Write("<td>" + entity.Bid_Attachments_Size + "</td>");
                Response.Write("<td>" + entity.Bid_Attachments_Remarks + "</td>");
                //Response.Write("<td>" + entity.Bid_Attachments_Remarks + "</td>");
                Response.Write("<td>");

                if (Type == 0)
                {
                    if (Status == 0)
                    {
                        Response.Write("<span><a href=\"/member/bid_Attachments_edit.aspx?AttID=" + entity.Bid_Attachments_ID + "\">修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"confirmdelete('/member/bid_do.aspx?action=moveatt&AttID=" + entity.Bid_Attachments_ID + "&BidID=" + entity.Bid_Attachments_BidID + "');\">删除</a></span>");
                    }
                    else
                    {
                        Response.Write("<span><a href=\"" + pub.FormatImgURL(entity.Bid_Attachments_Path, "fullpath") + "\">查看详情</a><span>");
                    }
                }
                else
                {
                    if (Status == 0)
                    {
                        Response.Write("<span><a href=\"/supplier/bid_Attachments_edit.aspx?AttID=" + entity.Bid_Attachments_ID + "\">修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"confirmdelete('/supplier/auction_do.aspx?action=moveatt&AttID=" + entity.Bid_Attachments_ID + "&BidID=" + entity.Bid_Attachments_BidID + "');\">删除</a></span>");
                    }
                    else
                    {
                        Response.Write("<span><a href=\"/supplier/bid_Attachments_view.aspx?AttID=" + entity.Bid_Attachments_ID + "\">查看详情</a><span>");
                    }
                }

                Response.Write("</td>");
                Response.Write("</tr>");
            }
        }
        else
        {
            Response.Write("<tr><td colspan=\"3\">暂无附件</td></tr>");
        }
    }

    public void DelBidAttachments(int Type)
    {
        int AttID = tools.CheckInt(Request["AttID"]);
        int BidID = tools.CheckInt(Request["BidID"]);

        if (!Check_MemberAndBid(BidID, Type))
        {
            MyBidAttachments.DelBidAttachments(AttID);
            if (Type == 0)
            {
                Response.Redirect("/member/Bid_view.aspx?list=2&BID=" + BidID);
            }
            else
            {
                Response.Redirect("/supplier/auction_view.aspx?list=2&BID=" + BidID);
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    public bool Check_MemberAndBid(int BidID, int Type)
    {
        int member_id = tools.NullInt(Session["member_id"]);
        if (member_id <= 0)
        {
            return true;
        }
        BidInfo entity = GetBidByID(BidID);

        if (entity == null)
        {
            return true;
        }

        if (member_id != entity.Bid_MemberID)
        {
            return true;
        }


        if (entity.Bid_Status > 0)
        {
            return true;
        }
        if (entity.Bid_Type != Type)
        {
            return true;
        }
        return false;
    }

    #region 报名

    /// <summary>
    /// 报名数
    /// </summary>
    public int SignUpNumber(int BID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_BidID", "=", BID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("BidEnterInfo.Bid_Enter_ID", "Desc"));

        PageInfo page = MyBidEnter.GetPageInfo(Query);

        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 根据招标ID获取报价数量
    /// </summary>
    public int SignBidTenderNumber(int BID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", BID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "Desc"));
        PageInfo page = MyTender.GetPageInfo(Query);
        //PageInfo page = MyBidEnter.GetPageInfo(Query);

        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }
    }


    public bool IsSignUp(int BID, int Supplier_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_BidID", "=", BID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("BidEnterInfo.Bid_Enter_ID", "Desc"));

        IList<BidEnterInfo> entitys = MyBidEnter.GetBidEnters(Query);
        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //拍卖竞价报名

    //生成订单号
    public string Generating_Order_Number()
    {
        string sn = "";
        bool ismatch = true;
        OrdersInfo ordersinfo = null;

        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            ordersinfo = MyOrders.GetOrdersBySN(sn);
            if (ordersinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    /// <summary>
    /// 交付保证金
    /// </summary>
    public void CashDeposit()
    {
        #region 定义字段
        int supplierid_cashdeposit_INT = 0;
        int member_cashdeposit_INT = 0;
        int BID = 0;
        double accountpay_cashdeposit_Double = 0;
        BidInfo mybid_cashdeposit_BID = new BidInfo();
        SupplierInfo mysupplier_cashdeposit_Supplier = new SupplierInfo();
        BidEnterInfo mybidenter_cashdeposit_BidEnter = new BidEnterInfo();
        IList<BidProductInfo> mybidproduct_cashdeposit = new List<BidProductInfo>();
        Glaer.Trade.B2C.Model.OrdersInfo myorder_cashdeposit_Order = new OrdersInfo();
        Glaer.Trade.B2C.Model.OrdersGoodsInfo myordergoods_cashdeposit_Order = new OrdersGoodsInfo();
        #endregion

        try
        {
            #region 获取数据
            //获取卖家ID
            supplierid_cashdeposit_INT = tools.NullInt(Session["supplier_id"]);
            //获取卖家数据
            mysupplier_cashdeposit_Supplier = MySupplier.GetSupplierByID(supplierid_cashdeposit_INT);
            //获取买家ID
            member_cashdeposit_INT = tools.NullInt(Session["member_id"]);
            //获取招标编号
            BID = tools.NullInt(Request["BID"]);
            //获取招标模型数据
            mybid_cashdeposit_BID = GetBidByID(BID);

            #endregion

            #region 判断
            if (supplierid_cashdeposit_INT <= 0 || member_cashdeposit_INT <= 0)
            {
                pub.Msg("error", "错误信息", "请先登录", false, "{ back}");
                Response.End();
            }
            if (mybid_cashdeposit_BID == null)
            {
                pub.Msg("error", "错误信息", "项目不存在", false, "{ back}");
                Response.End();
            }

            if (mybid_cashdeposit_BID.Bid_ExcludeSupplierID == supplierid_cashdeposit_INT || mybid_cashdeposit_BID.Bid_MemberID == member_cashdeposit_INT)
            {
                if (mybid_cashdeposit_BID.Bid_Type == 0)
                {
                    pub.Msg("error", "错误信息", "不能报名自己的发布的招标项目", false, "{ back}");
                    Response.End();
                }
                else
                {
                    pub.Msg("error", "错误信息", "不能报名自己的发布的拍卖项目", false, "{ back}");
                    Response.End();
                }

            }

            if (mysupplier_cashdeposit_Supplier != null)
            {
                if (mysupplier_cashdeposit_Supplier.Supplier_AuditStatus != 1)
                {
                    pub.Msg("error", "错误信息", "请等待资质审核", false, "{ back}");

                    Response.End();
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "请先登录", false, "{ back}");
                Response.End();
            }

            #endregion

            #region 填充订单模型
            myorder_cashdeposit_Order.Orders_SN = Generating_Order_Number();
            myorder_cashdeposit_Order.Orders_Type = 1;  //现货订单
            myorder_cashdeposit_Order.Orders_BuyerType = 1;
            myorder_cashdeposit_Order.Orders_ContractID = 0;
            myorder_cashdeposit_Order.Orders_BuyerID = tools.CheckInt(Session["member_id"].ToString());
            myorder_cashdeposit_Order.Orders_SysUserID = 0;
            myorder_cashdeposit_Order.Orders_SourceType = 1;
            myorder_cashdeposit_Order.Orders_Source = "";
            myorder_cashdeposit_Order.U_Orders_IsMonitor = 0;

            //订单状态
            myorder_cashdeposit_Order.Orders_Status = 1;
            myorder_cashdeposit_Order.Orders_ERPSyncStatus = 0;
            myorder_cashdeposit_Order.Orders_PaymentStatus = 0;
            myorder_cashdeposit_Order.Orders_DeliveryStatus = 0;
            myorder_cashdeposit_Order.Orders_DeliveryStatus_Time = DateTime.Now;
            myorder_cashdeposit_Order.Orders_Fail_Addtime = DateTime.Now;
            myorder_cashdeposit_Order.Orders_PaymentStatus_Time = DateTime.Now;
            myorder_cashdeposit_Order.Orders_InvoiceStatus = 0;
            myorder_cashdeposit_Order.Orders_Fail_SysUserID = 0;
            myorder_cashdeposit_Order.Orders_Fail_Note = "";
            myorder_cashdeposit_Order.Orders_IsReturnCoin = 0;
            myorder_cashdeposit_Order.Orders_IsSettling = 0;
            myorder_cashdeposit_Order.Orders_IsEvaluate = 0;

            //订单价格初始
            myorder_cashdeposit_Order.Orders_Total_MKTPrice = 0;
            myorder_cashdeposit_Order.Orders_Total_Price = 0;
            myorder_cashdeposit_Order.Orders_Total_Freight = 0;
            myorder_cashdeposit_Order.Orders_Total_Coin = 0;
            myorder_cashdeposit_Order.Orders_Total_UseCoin = 0;
            myorder_cashdeposit_Order.Orders_Total_AllPrice = 0;
            myorder_cashdeposit_Order.Orders_Account_Pay = accountpay_cashdeposit_Double;

            myorder_cashdeposit_Order.Orders_Admin_Sign = 0;
            myorder_cashdeposit_Order.Orders_Admin_Note = "";
            //myorder_cashdeposit_Order.Orders_Address_ID = address.Member_Address_ID;
            //myorder_cashdeposit_Order.Orders_Address_Country = address.Member_Address_Country;
            //myorder_cashdeposit_Order.Orders_Address_State = address.Member_Address_State;
            //myorder_cashdeposit_Order.Orders_Address_City = address.Member_Address_City;
            //myorder_cashdeposit_Order.Orders_Address_County = address.Member_Address_County;
            //myorder_cashdeposit_Order.Orders_Address_StreetAddress = address.Member_Address_StreetAddress;
            //myorder_cashdeposit_Order.Orders_Address_Zip = address.Member_Address_Zip;
            //myorder_cashdeposit_Order.Orders_Address_Name = address.Member_Address_Name;
            //myorder_cashdeposit_Order.Orders_Address_Phone_Countrycode = address.Member_Address_Phone_Countrycode;
            //myorder_cashdeposit_Order.Orders_Address_Phone_Areacode = address.Member_Address_Phone_Areacode;
            //myorder_cashdeposit_Order.Orders_Address_Phone_Number = address.Member_Address_Phone_Number;
            //myorder_cashdeposit_Order.Orders_Address_Mobile = address.Member_Address_Mobile;

            myorder_cashdeposit_Order.Orders_Note = "|招标订单AS56482(系统添加)|:" + BID;
            myorder_cashdeposit_Order.Orders_Site = "CN";
            myorder_cashdeposit_Order.Orders_Addtime = DateTime.Now;
            myorder_cashdeposit_Order.Orders_Delivery_Time_ID = 0;
            myorder_cashdeposit_Order.Orders_Delivery = 0;
            myorder_cashdeposit_Order.Orders_Delivery_Name = "";
            //myorder_cashdeposit_Order.Orders_Delivery = delivery.Delivery_Way_ID;
            //myorder_cashdeposit_Order.Orders_Delivery_Name = delivery.Delivery_Way_Name;
            //myorder_cashdeposit_Order.Orders_Payway = payway.Pay_Way_ID;
            //myorder_cashdeposit_Order.Orders_Payway_Name = payway.Pay_Way_Name;
            //myorder_cashdeposit_Order.Orders_VerifyCode = Orders_VerifyCode;
            myorder_cashdeposit_Order.Orders_Total_FreightDiscount = 0;
            myorder_cashdeposit_Order.Orders_Total_FreightDiscount_Note = "";
            myorder_cashdeposit_Order.Orders_Total_PriceDiscount = 0;
            myorder_cashdeposit_Order.Orders_Total_PriceDiscount_Note = "";
            myorder_cashdeposit_Order.Orders_Total_AllPrice = mybid_cashdeposit_BID.Bid_Bond;
            //myorder_cashdeposit_Order.Orders_From = Session["customer_source"].ToString();
            myorder_cashdeposit_Order.Orders_SupplierID = tools.NullInt(supplierid_cashdeposit_INT);
            myorder_cashdeposit_Order.Orders_PurchaseID = 0;
            myorder_cashdeposit_Order.Orders_PriceReportID = 0;
            myorder_cashdeposit_Order.Orders_MemberStatus = 1;
            myorder_cashdeposit_Order.Orders_MemberStatus_Time = DateTime.Now;
            myorder_cashdeposit_Order.Orders_SupplierStatus = 0;
            myorder_cashdeposit_Order.Orders_SupplierStatus_Time = DateTime.Now;
            //myorder_cashdeposit_Order.Orders_AgreementNo = agreement_no;
            //myorder_cashdeposit_Order.Orders_LoanMethodID = tools.NullInt(Request["Loan_Product_Method_ID"]);
            //myorder_cashdeposit_Order.Orders_LoanTermID = tools.NullInt(Request["Loan_Product_Term_ID"]);
            //myorder_cashdeposit_Order.Orders_FeeRate = Orders_FeeRate;
            //myorder_cashdeposit_Order.Orders_MarginRate = Orders_MarginRate;
            myorder_cashdeposit_Order.Orders_MarginFee = 0;
            myorder_cashdeposit_Order.Orders_Fee = 0;
            myorder_cashdeposit_Order.Orders_ApplyCreditAmount = 0;
            //myorder_cashdeposit_Order.Orders_Responsible = responsible;
            myorder_cashdeposit_Order.Orders_IsShow = 1;
            if (myorder_cashdeposit_Order != null)
            {
                MyOrders.AddOrders(myorder_cashdeposit_Order);
            }
            #endregion

            #region 填充订单商品模型
            QueryInfo query = new QueryInfo();
            query.PageSize = 0;
            query.CurrentPage = 1;

            query.ParamInfos.Add(new ParamInfo("AND", "int", "BidProductInfo.Bid_BidID", "=", tools.NullInt(BID).ToString()));
            mybidproduct_cashdeposit = MyBidProduct.GetBidProducts(query);
            if (mybidproduct_cashdeposit != null)
            {
                foreach (BidProductInfo item in mybidproduct_cashdeposit)
                {
                    OrdersGoodsInfo entity = new OrdersGoodsInfo();

                    entity.Orders_Goods_OrdersID = tools.NullInt(MyOrders.GetOrdersBySN(myorder_cashdeposit_Order.Orders_SN).Orders_ID);
                    entity.Orders_Goods_Product_Name = tools.NullStr(item.Bid_Product_Name);
                    entity.Orders_Goods_Product_Price = tools.NullDbl(item.Bid_Product_StartPrice);
                    entity.Orders_Goods_Product_Spec = tools.NullStr(item.Bid_Product_Spec);
                    entity.Orders_Goods_Product_Img = tools.NullStr(item.Bid_Product_Img);
                    entity.Orders_Goods_Product_MKTPrice = tools.NullDbl(item.Bid_Product_StartPrice);
                    entity.Orders_Goods_Product_SalePrice = tools.NullDbl(item.Bid_Product_StartPrice);
                    entity.Orders_Goods_Amount = item.Bid_Product_Amount;



                    if (entity != null)
                    {


                        if (MyOrders.AddOrdersGoods(entity))
                        {
                            pub.Msg("right", "信息", "请缴费", false, "{ back}");

                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "系统繁忙稍后重试", false, "{ back}");
                        }
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "未获取商品", false, "{ back}");

                    }
                }
            }
            #endregion

            #region 添加订单信息
            if (myorder_cashdeposit_Order != null)
            {
                MyOrders.AddOrders(myorder_cashdeposit_Order);
            }

            if (myordergoods_cashdeposit_Order != null)
            {
                MyOrders.AddOrdersGoods(myordergoods_cashdeposit_Order);
            }
            #endregion

        }
        catch (Exception ex)
        {
            //错误提示
            pub.Msg("error", "错误信息", ex.Message, false, "{ back}");
        }


    }


    #region  客户测试
    public void SignUp()
    {

        int BID = tools.NullInt(Request["BID"]);

        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        int member_id = tools.NullInt(Session["member_id"]);
        if (Supplier_ID <= 0 || member_id <= 0)
        {
            Response.Write("请先登录");
            Response.End();
        }
        BidInfo entity = GetBidByID(BID);
        if (entity == null)
        {
            Response.Write("项目不存在");
            Response.End();
        }

        if (entity.Bid_ExcludeSupplierID == Supplier_ID || entity.Bid_MemberID == member_id)
        {
            if (entity.Bid_Type == 0)
            {
                Response.Write("不能报名自己发布的招标项目");
                Response.End();
            }
            else
            {
                Response.Write("不能报名自己发布的拍卖项目");
                Response.End();
            }

        }


        SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplier_ID);
        SupplierInfo supplier = MySupplier.GetSupplierByID();
        ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(supplier.Supplier_ID);
        ZhongXin mycredit = new ZhongXin();
        if (supplierinfo != null)
        {
            if (supplierinfo.Supplier_AuditStatus != 1)
            {
                Response.Write("请等待资质审核");
                Response.End();
            }
        }
        else
        {
            Response.Write("请先登录");
            Response.End();
        }
        //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterStartTime) < 0)
        //{
        //    if (entity.Bid_Type == 0)
        //    {
        //        //Response.Write("招标项目报名未开始");
        //        //Response.End();
        //    }
        //    else
        //    {
        //        //Response.Write("拍卖项目报名未开始");
        //        //Response.End();
        //    }


        //}

        //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) > 0)
        //{
        //    if (entity.Bid_Type == 0)
        //    {
        //    Response.Write("招标项目报名已结束");
        //    Response.End();
        //}
        //else
        //{
        //    Response.Write("拍卖项目报名已结束");
        //    Response.End();
        //    }


        //}

        if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1)
        {
            if (entity.Bid_Type == 0)
            {
                Response.Write("招标项目不可报名");
                Response.End();
            }
            else
            {
                Response.Write("拍卖项目不可报名");
                Response.End();
            }


        }

        BidEnterInfo Bidenter = GetBidEnterBySupplierID(entity.Bid_ID, Supplier_ID);
        if (Bidenter != null)
        {
            Response.Write("已报名,不可重复报名");
            Response.End();
        }



        if (supplier == null)
        {
            Response.Write("请先登录");
            Response.End();
        }

        //if (supplier.Supplier_Security_Account - entity.Bid_Bond < 0)
        //{

        //    Response.Write("保证金不足,请先充值");
        //    Response.End();
        //}

        string Supplier_CompanyName = "商家";
        if (supplierinfo != null)
        {
            Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
        }
        if (PayAccountInfo != null)
        {
            decimal accountremain = 0;
            accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
            if (accountremain < (decimal)entity.Bid_Bond)
            {
                Response.Write("您的账户余额不足，请入金充值！");
                Response.End();
            }
            else
            {
                string Log_note = "";
                //Bid_Type 0:代表招标  1:代表拍卖
                if (entity.Bid_Type == 1)
                {

                    string supplier_name = supplierinfo.Supplier_CompanyName;
                    string strResult = string.Empty;
                    if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "买家参与拍卖交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
                        ))
                    {
                        Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                    }
                    else
                    {
                        Response.Write("拍卖投标保证金转账扣款失败");
                        Response.End();
                    }


                }
                else
                {
                    string supplier_name = supplierinfo.Supplier_CompanyName;
                    string strResult = string.Empty;
                    if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "卖家参与招标交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
                        ))
                    {
                        //Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                        Log_note = "报名[" + entity.Bid_Title + "]招标项目,扣除保证金";
                    }
                    else
                    {
                        Response.Write("招标投标保证金转账扣款失败");
                        Response.End();
                    }
                    //Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
                }


                MySupplier.Supplier_Account_Log(Supplier_ID, 1, -(entity.Bid_Bond), Log_note);

                Bidenter = new BidEnterInfo();

                Bidenter.Bid_Enter_ID = 0;
                Bidenter.Bid_Enter_BidID = entity.Bid_ID;
                Bidenter.Bid_Enter_SupplierID = Supplier_ID;
                Bidenter.Bid_Enter_Bond = entity.Bid_Bond;
                Bidenter.Bid_Enter_Type = entity.Bid_Type;
                Bidenter.Bid_Enter_IsShow = 1;
                if (MyBidEnter.AddBidEnter(Bidenter))
                {
                    //Response.Write("True");
                    Response.Write("success");
                    Response.End();
                }
                else
                {
                    Response.Write("报名失败,请稍后再报名");
                    Response.End();
                }
            }
        }
        else
        {
            Response.Write("未找到该平台下" + Supplier_CompanyName + "商家的中信账号");
            Response.End();
        }

    }






    //public void SignUp()
    //{

    //    int BID = tools.NullInt(Request["BID"]);

    //    int Supplier_ID = tools.NullInt(Session["supplier_id"]);

    //    int member_id = tools.NullInt(Session["member_id"]);
    //    if (Supplier_ID <= 0 || member_id <= 0)
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }
    //    BidInfo entity = GetBidByID(BID);
    //    if (entity == null)
    //    {
    //        Response.Write("项目不存在");
    //        Response.End();
    //    }

    //    if (entity.Bid_ExcludeSupplierID == Supplier_ID || entity.Bid_MemberID == member_id)
    //    {
    //        if (entity.Bid_Type == 0)
    //        {
    //            Response.Write("不能报名自己发布的招标项目");
    //            Response.End();
    //        }
    //        else
    //        {
    //            Response.Write("不能报名自己发布的拍卖项目");
    //            Response.End();
    //        }

    //    }
    //    SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplier_ID);
    //    SupplierInfo supplier = MySupplier.GetSupplierByID();
    //    ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(supplier.Supplier_ID);
    //    ZhongXin mycredit = new ZhongXin();
    //    if (supplierinfo != null)
    //    {
    //        if (supplierinfo.Supplier_AuditStatus != 1)
    //        {
    //            Response.Write("请等待资质审核");
    //            Response.End();
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }
    //    //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterStartTime) < 0)
    //    //{
    //    //    if (entity.Bid_Type == 0)
    //    //    {
    //    //        //Response.Write("招标项目报名未开始");
    //    //        //Response.End();
    //    //    }
    //    //    else
    //    //    {
    //    //        //Response.Write("拍卖项目报名未开始");
    //    //        //Response.End();
    //    //    }


    //    //}

    //    //if (DateTime.Compare(DateTime.Now, entity.Bid_EnterEndTime) > 0)
    //    //{
    //    //    if (entity.Bid_Type == 0)
    //    //    {
    //    //    Response.Write("招标项目报名已结束");
    //    //    Response.End();
    //    //}
    //    //else
    //    //{
    //    //    Response.Write("拍卖项目报名已结束");
    //    //    Response.End();
    //    //    }


    //    //}

    //    if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1)
    //    {
    //        if (entity.Bid_Type == 0)
    //        {
    //            Response.Write("招标项目不可报名");
    //            Response.End();
    //        }
    //        else
    //        {
    //            Response.Write("拍卖项目不可报名");
    //            Response.End();
    //        }


    //    }

    //    BidEnterInfo Bidenter = GetBidEnterBySupplierID(entity.Bid_ID, Supplier_ID);
    //    if (Bidenter != null)
    //    {
    //        Response.Write("已报名,不可重复报名");
    //        Response.End();
    //    }



    //    if (supplier == null)
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }

    //    //if (supplier.Supplier_Security_Account-entity.Bid_Bond<0)
    //    //{

    //    //    Response.Write("保证金不足,请先充值");
    //    //    Response.End();
    //    //}
    //    string Supplier_CompanyName = "商家";
    //    if (supplierinfo != null)
    //    {
    //        Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
    //    }
    //    if (PayAccountInfo != null)
    //    {
    //        decimal accountremain = 0;
    //        accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
    //        if (accountremain < (decimal)entity.Bid_Bond)
    //        {
    //            Response.Write("您的账户余额不足，请入金充值！");
    //            Response.End();
    //        }
    //        else
    //        {
    //            string Log_note = "";
    //            //Bid_Type 0:代表招标  1:代表拍卖
    //            if (entity.Bid_Type == 1)
    //            {

    //                string supplier_name = supplierinfo.Supplier_CompanyName;
    //                string strResult = string.Empty;
    //                if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "买家参与拍卖交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
    //                    ))
    //                {
    //                    Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
    //                }
    //                else
    //                {
    //                    Response.Write("投标保证金扣款失败");
    //                    Response.End();
    //                }


    //            }
    //            else
    //            {
    //                Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
    //            }


    //            MySupplier.Supplier_Account_Log(Supplier_ID, 1, -(entity.Bid_Bond), Log_note);

    //            Bidenter = new BidEnterInfo();

    //            Bidenter.Bid_Enter_ID = 0;
    //            Bidenter.Bid_Enter_BidID = entity.Bid_ID;
    //            Bidenter.Bid_Enter_SupplierID = Supplier_ID;
    //            Bidenter.Bid_Enter_Bond = entity.Bid_Bond;
    //            Bidenter.Bid_Enter_Type = entity.Bid_Type;
    //            Bidenter.Bid_Enter_IsShow = 1;
    //            if (MyBidEnter.AddBidEnter(Bidenter))
    //            {
    //                Response.Write("True");
    //                Response.End();
    //            }
    //            else
    //            {
    //                Response.Write("报名失败,请稍后再报名");
    //                Response.End();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("未找到该平台下" + Supplier_CompanyName + "的中信账号");
    //        Response.End();
    //    }

    //}
    #endregion

    //public void SignUp()
    //{

    //    int BID = tools.NullInt(Request["BID"]);

    //    int Supplier_ID = tools.NullInt(Session["supplier_id"]);

    //    int member_id = tools.NullInt(Session["member_id"]);
    //    if (Supplier_ID <= 0 || member_id <= 0)
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }
    //    BidInfo entity = GetBidByID(BID);
    //    if (entity == null)
    //    {
    //        Response.Write("项目不存在");
    //        Response.End();
    //    }

    //    if (entity.Bid_ExcludeSupplierID == Supplier_ID || entity.Bid_MemberID == member_id)
    //    {
    //        if (entity.Bid_Type == 0)
    //        {
    //            Response.Write("不能报名自己发布的招标项目");
    //            Response.End();
    //        }
    //        else
    //        {
    //            Response.Write("不能报名自己发布的拍卖项目");
    //            Response.End();
    //        }

    //    }
    //    SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplier_ID);
    //    SupplierInfo supplier = MySupplier.GetSupplierByID();
    //    //ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(supplier.Supplier_ID);
    //    //ZhongXin mycredit = new ZhongXin();
    //    if (supplierinfo != null)
    //    {
    //        if (supplierinfo.Supplier_AuditStatus != 1)
    //        {
    //            Response.Write("请等待资质审核");
    //            Response.End();
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }


    //    if (entity.Bid_IsAudit != 1 || entity.Bid_Status != 1)
    //    {
    //        if (entity.Bid_Type == 0)
    //        {
    //            Response.Write("招标项目不可报名");
    //            Response.End();
    //        }
    //        else
    //        {
    //            Response.Write("拍卖项目不可报名");
    //            Response.End();
    //        }


    //    }

    //    BidEnterInfo Bidenter = GetBidEnterBySupplierID(entity.Bid_ID, Supplier_ID);
    //    if (Bidenter != null)
    //    {
    //        Response.Write("已报名,不可重复报名");
    //        Response.End();
    //    }



    //    if (supplier == null)
    //    {
    //        Response.Write("请先登录");
    //        Response.End();
    //    }

    //    //if (supplier.Supplier_Security_Account-entity.Bid_Bond<0)
    //    //{

    //    //    Response.Write("保证金不足,请先充值");
    //    //    Response.End();
    //    //}
    //    string Supplier_CompanyName = "商家";
    //    if (supplierinfo != null)
    //    {
    //        Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
    //    }
    //    //if (PayAccountInfo != null)
    //    //{
    //        decimal accountremain = 0;
    //        //accountremain = mycredit.GetAmount(PayAccountInfo.SubAccount);
    //        //if (accountremain < (decimal)entity.Bid_Bond)
    //        //{
    //        //    Response.Write("您的账户余额不足，请入金充值！");
    //        //    Response.End();
    //        //}
    //        //else
    //        //{
    //            string Log_note = "";
    //            //Bid_Type 0:代表招标  1:代表拍卖
    //            //if (entity.Bid_Type == 1)
    //            //{

    //            //    string supplier_name = supplierinfo.Supplier_CompanyName;
    //            //    string strResult = string.Empty;
    //                //if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "买家参与拍卖交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
    //                //    ))
    //                //{
    //                //    Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
    //                //}
    //                //else
    //                //{
    //                //    Response.Write("拍卖保证金扣款失败");
    //                //    Response.End();
    //                //}


    //            //}
    //            //else
    //            //{
    //                //Log_note = "报名[" + entity.Bid_Title + "]拍卖项目,扣除保证金";
    //                //string supplier_name = supplierinfo.Supplier_CompanyName;
    //                //string strResult = string.Empty;
    //                //if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "买家参与拍卖交纳保证金", entity.Bid_Bond, ref strResult, supplier_name
    //                //    ))
    //                //{
    //                //    Log_note = "报名[" + entity.Bid_Title + "]招标项目,扣除保证金";
    //                //}
    //                //else
    //                //{
    //                //    Response.Write("投标保证金扣款失败");
    //                //    Response.End();
    //                //}
    //            //}


    //            MySupplier.Supplier_Account_Log(Supplier_ID, 1, -(entity.Bid_Bond), Log_note);

    //            Bidenter = new BidEnterInfo();

    //            Bidenter.Bid_Enter_ID = 0;
    //            Bidenter.Bid_Enter_BidID = entity.Bid_ID;
    //            Bidenter.Bid_Enter_SupplierID = Supplier_ID;
    //            Bidenter.Bid_Enter_Bond = entity.Bid_Bond;
    //            Bidenter.Bid_Enter_Type = entity.Bid_Type;
    //            Bidenter.Bid_Enter_IsShow = 1;
    //            if (MyBidEnter.AddBidEnter(Bidenter))
    //            {
    //                Response.Write("True");
    //                Response.End();
    //            }
    //            else
    //            {
    //                Response.Write("报名失败,请稍后再报名");
    //                Response.End();
    //            }
    //        //}
    //    //}
    //    //else
    //    //{
    //    //    Response.Write("未找到该平台下" + Supplier_CompanyName + "的中信账号");
    //    //    Response.End();
    //    //}

    //}



    public void Check_Bid_Bond()
    {
        double bid_bond = 0.00;
        string var = tools.CheckStr(Request["val"]);
        if (var == "0")
        {
            Response.Write("<font color=\"#ff6600\">不缴纳保证金！</font>");
            return;
        }
        else
        {
            if (tools.CheckStr(var.ToString()).Length > 0)
            {
                bid_bond = tools.CheckFloat(var.ToString());
                if (bid_bond > 0)
                {
                    Response.Write("<font color=\"#ff6600\">保证金金额正确！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#cc0000\">请输入正确的保证金！</font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#ff6600\">保证金金额正确！</font>");
                return;
            }
        }
    }

    public void MemberBidList(int Type)
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">招标公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">招标单位</td>";
            tmp_head = tmp_head + "<td width=\"254\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
            tmp_head = tmp_head + "<td width=\"126\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
        }
        else
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">拍卖公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">拍卖用户</td>";
            tmp_head = tmp_head + "<td width=\"254\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
            tmp_head = tmp_head + "<td width=\"126\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";

        }

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无报名</td></tr>";

        page_url = "?keyword=" + keyword;
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "Bid_MemberID", "=", Supplier_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "Bid_Type", "=", Type.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "Bid_IsShow", "=", "1"));

        DataTable datatable = (DataTable)MyBid.GetBids(Query, pub.CreateUserPrivilege("32aa37b4 - 916e-4ffd - 81cb - aaa27fc3aaa2"));
        PageInfo page = MyBid.GetPageInfo(Query, pub.CreateUserPrivilege("32aa37b4 - 916e-4ffd - 81cb - aaa27fc3aaa2"));

        Response.Write(tmp_head);
        if (datatable.Rows.Count > 0)
        {
            tmp_list = "";

            foreach (DataRow entity in datatable.Rows)
            {
                DateTime Bid_BidStartTime = tools.NullDate(entity["Bid_BidStartTime"]);
                DateTime Bid_BidEndTime = tools.NullDate(entity["Bid_BidEndTime"]);
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td title=" + entity["Bid_Title"] + ">" + tools.CutStr(entity["Bid_Title"].ToString(), 18) + "</td>";
                tmp_list = tmp_list + "<td  title=" + entity["Bid_MemberCompany"] + ">" + tools.CutStr(entity["Bid_MemberCompany"].ToString(), 18) + "</td>";
                //tmp_list = tmp_list + "<td>" + Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                tmp_list = tmp_list + "<td>" + Bid_BidStartTime.ToString("yyyy-MM-dd") + "<span style=\"color:#ff6600\">至</span>" + Bid_BidEndTime.ToString("yyyy-MM-dd") + "</td>";
                tmp_list = tmp_list + "<td><a href>查看</a></td>";

                if (Type == 0)
                {
                    if (DateTime.Compare(DateTime.Now, Bid_BidStartTime) >= 0 && DateTime.Compare(DateTime.Now, Bid_BidEndTime) <= 0)
                    {
                        tmp_list = tmp_list + "<td><span><a href=\"/supplier/tender_add.aspx?BID=" + entity["Bid_ID"] + "\">报价</a></span></td>";
                    }
                    else
                    {
                        tmp_list = tmp_list + "<td>--</td>";
                    }
                }
                else
                {
                    if (DateTime.Compare(DateTime.Now, Bid_BidStartTime) >= 0 && DateTime.Compare(DateTime.Now, Bid_BidEndTime) <= 0)
                    {

                        tmp_list = tmp_list + "<td><span><a href=\"/member/auction_tender_add.aspx?BID=" + entity["Bid_ID"] + "\">竞价</a></span></td>";
                        //tmp_list = tmp_list + "<td></td>";

                    }
                    else
                    {
                        tmp_list = tmp_list + "<td>--</td>";

                    }
                }


                tmp_list = tmp_list + "</tr>";

            }
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }


    public void SupplierBidEnterList(int Type)
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">招标公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">招标单位</td>";
            tmp_head = tmp_head + "<td width=\"254\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
            tmp_head = tmp_head + "<td width=\"126\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
        }
        else
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">拍卖公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">拍卖用户</td>";
            tmp_head = tmp_head + "<td width=\"254\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"156\" class=\"name\">当前状态</td>";
            tmp_head = tmp_head + "<td width=\"126\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";

        }

        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

        tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无报名</td></tr>";

        page_url = "?keyword=" + keyword;
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_SupplierID", "=", Supplier_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_Type", "=", Type.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("BidEnterInfo.Bid_Enter_ID", "DESC"));

        DataTable datatable = MyBidEnter.GetBidEnterSupplierList(Query);
        PageInfo page = MyBidEnter.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (datatable.Rows.Count > 0)
        {
            tmp_list = "";

            foreach (DataRow entity in datatable.Rows)
            {
                DateTime Bid_BidStartTime = tools.NullDate(entity["Bid_BidStartTime"]);
                DateTime Bid_BidEndTime = tools.NullDate(entity["Bid_BidEndTime"]);
                tmp_list = tmp_list + "<tr>";
                tmp_list = tmp_list + "<td title=" + entity["Bid_Title"] + ">" + tools.CutStr(entity["Bid_Title"].ToString(), 18) + "</td>";
                tmp_list = tmp_list + "<td  title=" + entity["Bid_MemberCompany"] + ">" + tools.CutStr(entity["Bid_MemberCompany"].ToString(), 18) + "</td>";
                //tmp_list = tmp_list + "<td>" + Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                tmp_list = tmp_list + "<td>" + Bid_BidStartTime.ToString("yyyy-MM-dd") + "<span style=\"color:#ff6600\">至</span>" + Bid_BidEndTime.ToString("yyyy-MM-dd") + "</td>";
                if (DateTime.Compare(Bid_BidEndTime, DateTime.Now)>0)
                {
                    tmp_list = tmp_list + "<td>报价中</td>";

                }
                tmp_list = tmp_list + "<td>"+entity["Tender_IsWin"]=="0"?"未中标":"已中标" +"</td>";

                if (Type == 0)
                {
                    if (DateTime.Compare(DateTime.Now, Bid_BidStartTime) >= 0 && DateTime.Compare(DateTime.Now, Bid_BidEndTime) <= 0)
                    {
                        tmp_list = tmp_list + "<td><span><a href=\"/supplier/tender_add.aspx?BID=" + entity["Bid_ID"] + "\">报价</a></span></td>";
                    }
                    else
                    {
                        tmp_list = tmp_list + "<td><span><a href=\"/supplier/tender_add.aspx?BID=" + entity["Bid_ID"] + "\">查看</a></span></td>";
                    }
                }
                else
                {
                    if (DateTime.Compare(DateTime.Now, Bid_BidStartTime) >= 0 && DateTime.Compare(DateTime.Now, Bid_BidEndTime) <= 0)
                    {

                        tmp_list = tmp_list + "<td><span><a href=\"/member/auction_tender_add.aspx?BID=" + entity["Bid_ID"] + "\">竞价</a></span></td>";
                        //tmp_list = tmp_list + "<td></td>";

                    }
                    else
                    {
                        tmp_list = tmp_list + "<td>--</td>";

                    }
                }


                tmp_list = tmp_list + "</tr>";

            }
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }
    #endregion

    #region 报价


    /// <summary>
    /// 报价显示(带历史记录)
    /// </summary>
    /// <param name="BID">招标id</param>
    /// <param name="BidProducts">招标商品列表</param>
    /// <param name="Bid_Number">报价次数</param>
    public void Tender_List(int BID, IList<BidProductInfo> BidProducts, int Bid_Number, int supplier, DateTime BidEndTime, int Type)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        SQLHelper DBHelper = new SQLHelper();
        if (supplier_id > 0)
        {
            IList<TenderInfo> entitys = GetTenders(BID, supplier_id);
            if (entitys != null)
            {
                //if (entitys.Count < Bid_Number && supplier == 0 && DateTime.Compare(DateTime.Now, BidEndTime) <= 0)
                //{
                IList<TenderProductInfo> tenderProducts = null;
                if (entitys.Count < Bid_Number && supplier == 0 && DateTime.Compare(DateTime.Now, BidEndTime) <= 0)
                {
                    Response.Write("<div class=\"b02_main\">");
                    Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");
                    Response.Write("<tr>");
                    if (Type == 0)
                    {
                        //Response.Write("<td width=\"300\" class=\"name\">物料编号</td>");
                        Response.Write("<td width=\"300\" class=\"name\">产品名称</td>   ");
                        Response.Write("<td width=\"200\" class=\"name\">采购数</td>");
                        //Response.Write("<td width=\"200\" class=\"name\">排名</td>");
                        Response.Write("<td width=\"200\" class=\"name\">单价</td>");

                    }
                    else
                    {
                        Response.Write("<td width=\"300\" class=\"name\">产品名称</td>");
                        Response.Write("<td width=\"300\" class=\"name\">产品数量</td>   ");
                        Response.Write("<td width=\"150\" class=\"name\">起标价格</td>");
                        Response.Write("<td width=\"150\" class=\"name\">单价</td>");
                    }

                    Response.Write("</tr>");

                    tenderProducts = entitys[0].TenderProducts;
                    //int product_price_sort=0;
                    if (tenderProducts != null)
                    {
                        int sum = tenderProducts.Count;

                        for (int i = 0; i < sum; i++)
                        {
                            if (BidProducts == null)
                            {
                                break;
                            }

                            if (tenderProducts[i].Tender_Product_BidProductID == BidProducts[i].Bid_Product_ID)
                            {
                                Response.Write("<tr>");
                                if (Type == 0)
                                {
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                }
                                else
                                {
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                    Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                                }

                                Response.Write("<td><input name=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\" id=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\"  type=\"text\" value=\"" + tenderProducts[i].Tender_Price + "\" /></td>");
                                Response.Write("</tr>");
                            }

                        }

                    }
                    tenderProducts = null;

                    Response.Write("</table>");
                    Response.Write("<ul style=\"width:850px;\">");
                    if (Type == 0)
                    {
                        Response.Write("<li><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\">提交报价</a></li>");
                    }
                    else
                    {
                        Response.Write("<li><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\">提交竞价</a></li>");
                    }

                    Response.Write("</ul>");
                    Response.Write("<div class=\"clear\"></div>");
                    Response.Write("</div>");
                }
                Response.Write("<div class=\"blk11_1\" style=\"margin-top: 0px;padding:0 0 10px 0;\">");
                //Response.Write("<h3 style=\"margin-bottom:10px;\">历史记录(以最后一条记录为准)</h3>");
                //Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");



                //foreach (TenderInfo entity in entitys)
                //{

                //    Response.Write("<tr>");
                //    int AllPriceSort = 0;
                //    //string sql1 = "select  RowNum=IDENTITY(INT,1,1),Tender_Price into #T  from Tender_Product tp join Tender t on tp.Tender_TenderID=t.Tender_ID join Bid_Product bp on tp.Tender_Product_BidProductID=bp.Bid_Product_ID where Tender_BidID=" + BID + " and bp.Bid_Product_ID=" + tenderProducts[i].Tender_Product_BidProductID + " order by Tender_Price asc  select min(RowNum) from #T  where Tender_Price=" + tenderProducts[i].Tender_Price + " DROP TABLE #T; ";


                //    string sql2 = "select RowNum=IDENTITY(INT,1,1),Tender_AllPrice,Tender_BidID into #T1 from Tender where     Tender_BidID=" + entity.Tender_BidID + "  order by Tender_AllPrice asc select ROWNUM from #T1 WHERE  Tender_AllPrice=" + entity.Tender_AllPrice + "DROP TABLE #T1; ";
                //    AllPriceSort = tools.CheckInt(DBHelper.ExecuteScalar(sql2).ToString());
                //    if (Type == 0)
                //    {
                //        Response.Write("<td width=\"150\" class=\"name\">报价总价</td>");

                //        Response.Write("<td width=\"150\" ><span><a>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>   ");
                //        Response.Write("<td width=\"150\" class=\"name\">总价排名</td>");
                //        Response.Write("<td width=\"150\"><span><a>" + AllPriceSort + "</a></span></td>");
                //    }
                //    else
                //    {
                //        Response.Write("<td width=\"300\" class=\"name\">竞价总价</td>");
                //        Response.Write("<td width=\"300\" ><span><a>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>   ");
                //    }


                //    if (Type == 0)
                //    {
                //        Response.Write("<td width=\"150\" class=\"name\">报价时间</td>");
                //        Response.Write("<td width=\"150\" ><span><a>" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");
                //    }
                //    else
                //    {
                //        Response.Write("<td width=\"150\" class=\"name\">竞价时间</td>");
                //        Response.Write("<td width=\"300\" colspan=\"2\" ><span><a>" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");
                //    }



                //    Response.Write("</tr>");


                //    Response.Write("<tr>");



                //    if (Type == 0)
                //    {

                //        Response.Write("<td width=\"150\" class=\"name\" colspan=\"2\">产品名称</td>   ");
                //        //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");
                //        Response.Write("<td width=\"100\" class=\"name\">采购数量</td>");
                //        Response.Write("<td width=\"150\" class=\"name\">排名</td>");
                //        Response.Write("<td width=\"200\" class=\"name\" colspan=\"2\">单价</td>");
                //        //Response.Write("<td width=\"400\" colspan=\"2\" class=\"name\">总计</td>");

                //    }
                //    else
                //    {
                //        Response.Write("<td width=\"150\" class=\"name\">产品名称</td>");
                //        //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");
                //        Response.Write("<td width=\"100\" class=\"name\">产品数量</td>   ");
                //        Response.Write("<td width=\"150\" class=\"name\">起标价格</td>");
                //        Response.Write("<td width=\"200\" class=\"name\">排名</td>");
                //        Response.Write("<td width=\"150\" class=\"name\">单价</td>");
                //    }
                //    Response.Write("</tr>");

                //    tenderProducts = entity.TenderProducts;
                //    if (tenderProducts != null)
                //    {
                //        int sum = tenderProducts.Count;

                //        for (int i = 0; i < sum; i++)
                //        {
                //            int product_price_sort = 0;
                //            string sql1 = "select  RowNum=IDENTITY(INT,1,1),Tender_Price into #T  from Tender_Product tp join Tender t on tp.Tender_TenderID=t.Tender_ID join Bid_Product bp on tp.Tender_Product_BidProductID=bp.Bid_Product_ID where Tender_BidID=" + BID + " and bp.Bid_Product_ID=" + tenderProducts[i].Tender_Product_BidProductID + " order by Tender_Price asc  select min(RowNum) from #T  where Tender_Price=" + tenderProducts[i].Tender_Price + " DROP TABLE #T; ";
                //            product_price_sort = tools.CheckInt(DBHelper.ExecuteScalar(sql1).ToString());
                //            if (BidProducts == null)
                //            {
                //                break;
                //            }

                //            if (tenderProducts[i].Tender_Product_BidProductID == BidProducts[i].Bid_Product_ID)
                //            {
                //                Response.Write("<tr>");
                //                if (Type == 0)
                //                {
                //                    //    Response.Write("<td>" + BidProducts[i].Bid_Product_Code + "</td>");
                //                    Response.Write("<td colspan=\"2\">" + BidProducts[i].Bid_Product_Name + "</td>");




                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                //                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");
                //                    Response.Write("<td colspan=\"2\">" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                //                    //Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                //                }
                //                else
                //                {
                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");





                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                //                    Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                //                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");
                //                    Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                //                }

                //                Response.Write("</tr>");
                //            }

                //        }

                //    }
                //}
                Response.Write("<h3 style=\"margin-bottom:10px;\">我的报价记录(以最后一条记录为准)</h3>");


                int BidRound = entitys.Count + 1;

                foreach (TenderInfo entity in entitys)
                {
                    Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");
                    BidRound = BidRound - 1;

                    Response.Write("<tr>");
                    int AllPriceSort = 0;


                    string sql2 = "select RowNum=IDENTITY(INT,1,1),Tender_AllPrice,Tender_BidID into #T1 from Tender where     Tender_BidID=" + entity.Tender_BidID + "  order by Tender_AllPrice asc select ROWNUM from #T1 WHERE  Tender_AllPrice=" + entity.Tender_AllPrice + "DROP TABLE #T1; ";
                    AllPriceSort = tools.CheckInt(DBHelper.ExecuteScalar(sql2).ToString());
                    if (Type == 0)
                    {


                        Response.Write("<td width=\"225px\" style=\"border-right:none;\" class=\"td_bidround\" ><strong>报价轮次:</strong><span><a style=\"font-size:16px;\">" + BidRound + "</a></span></td>   ");

                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");


                        //Response.Write("<td width=\"225\" style=\"border-right:none;\" class=\"td_bidround\"><strong>报价总价:</strong><span><a style=\"font-size:16px;\">" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>");



                        //Response.Write("<td width=\"150\" class=\"name\">总价排名</td>");
                        //Response.Write("<td width=\"150\"><span><a>" + AllPriceSort + "</a></span></td>");
                    }
                    else
                    {
                        Response.Write("<td width=\"195px\" style=\"border-right:none;\" class=\"td_bidround\" ><strong>报价轮次:</strong><span><a style=\"font-size:16px;\">" + BidRound + "</a></span></td>   ");

                        Response.Write("<td width=\"195px\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");



                        //Response.Write("<td width=\"300\" class=\"name\">竞价总价</td>");
                        //Response.Write("<td width=\"300\" ><span><a>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>   ");
                    }


                    if (Type == 0)
                    {

                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");

                        Response.Write("<td width=\"225px\"  style=\"border-right:none;text-align:left;\" class=\"td_bidround\"><strong>报价时间:</strong><span><a style=\"font-size:16px;\">" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");



                    }
                    else
                    {
                        //Response.Write("<td width=\"150\" class=\"name\">竞价时间</td>");
                        //Response.Write("<td width=\"300\" colspan=\"2\" ><span><a>" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");
                        Response.Write("<td width=\"195px\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");
                        //Response.Write("<td width=\"195px\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");

                        Response.Write("<td width=\"195px\"  style=\"border-right:none;text-align:center;\" colspan=\"2\" class=\"td_bidround\"><strong>竞价时间:</strong><span><a style=\"font-size:16px;\">" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");
                    }



                    Response.Write("</tr>");


                    Response.Write("<tr>");



                    if (Type == 0)
                    {

                        Response.Write("<td width=\"225\" class=\"name\" >产品名称</td>   ");
                        //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");


                        Response.Write("<td width=\"225\" class=\"name\" >单价</td>");
                        Response.Write("<td width=\"225\" class=\"name\">采购数量</td>");

                        Response.Write("<td width=\"225\" class=\"name\">排名</td>");
                        //Response.Write("<td width=\"400\" colspan=\"2\" class=\"name\">总计</td>");

                    }
                    else
                    {
                        Response.Write("<td width=\"195\" class=\"name\">产品名称</td>");
                        //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");
                        Response.Write("<td width=\"195\" class=\"name\">产品数量</td>   ");
                        Response.Write("<td width=\"195\" class=\"name\">起标价格</td>");
                        Response.Write("<td width=\"195\" class=\"name\">排名</td>");
                        Response.Write("<td width=\"195\" class=\"name\">单价</td>");
                    }
                    Response.Write("</tr>");

                    tenderProducts = entity.TenderProducts;
                    if (tenderProducts != null)
                    {
                        int sum = tenderProducts.Count;

                        for (int i = 0; i < sum; i++)
                        {
                            int product_price_sort = 0;
                            string sql1 = "select  RowNum=IDENTITY(INT,1,1),Tender_Price into #T  from Tender_Product tp join Tender t on tp.Tender_TenderID=t.Tender_ID join Bid_Product bp on tp.Tender_Product_BidProductID=bp.Bid_Product_ID where Tender_BidID=" + BID + " and bp.Bid_Product_ID=" + tenderProducts[i].Tender_Product_BidProductID + " order by Tender_Price asc  select min(RowNum) from #T  where Tender_Price=" + tenderProducts[i].Tender_Price + " DROP TABLE #T; ";
                            product_price_sort = tools.CheckInt(DBHelper.ExecuteScalar(sql1).ToString());
                            if (BidProducts == null)
                            {
                                break;
                            }

                            if (tenderProducts[i].Tender_Product_BidProductID == BidProducts[i].Bid_Product_ID)
                            {
                                Response.Write("<tr>");
                                if (Type == 0)
                                {
                                    //    Response.Write("<td>" + BidProducts[i].Bid_Product_Code + "</td>");
                                    Response.Write("<td >" + BidProducts[i].Bid_Product_Name + "</td>");


                                    Response.Write("<td >" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");

                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");

                                    //Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                                }
                                else
                                {
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");





                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                    Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");
                                    Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                                }

                                Response.Write("</tr>");
                            }

                        }

                    }
                    if (Type == 0)
                    {
                        //Response.Write("<td width=\"150\" class=\"name\">报价时间</td>");
                        Response.Write("<td width=\"225\" style=\"border-right:none;\" class=\"td_bidround\"><strong>报价总价:</strong><span><a style=\"font-size:16px;\">" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>");


                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");
                    }
                    if (Type == 0)
                    {
                        int All_Price_Sort = 0;
                        string sql1 = "select RowNum=IDENTITY(INT,1,1),Tender_AllPrice,Tender_BidID into #T3 from Tender  WHERE Tender_BidID=" + entity.Tender_BidID + " order by Tender_AllPrice asc select ROWNUM from #T3 where  Tender_AllPrice=" + entity.Tender_AllPrice + " DROP TABLE #T3; ";
                        All_Price_Sort = tools.CheckInt(DBHelper.ExecuteScalar(sql1).ToString());



                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");
                        //Response.Write("<td width=\"150\" class=\"name\">报价时间</td>");
                        Response.Write("<td width=\"225\" style=\"border-right:none; class=\"td_bidround\"><strong>总价排名:</strong><span><a style=\"font-size:16px;\">" + All_Price_Sort + "</a></span></td>");
                    }
                    Response.Write("</table>");
                }



                Response.Write("</table>");
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("</div>");
            }
            else if (DateTime.Compare(DateTime.Now, BidEndTime) <= 0)
            {
                Response.Write("<div class=\"b02_main\">");
                Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");
                Response.Write("<tr>");
                if (Type == 0)
                {
                    //Response.Write("<td width=\"300\" class=\"name\">物料编号</td>");
                    Response.Write("<td width=\"300\" class=\"name\" colspan=\"2\">产品名称</td>   ");
                    Response.Write("<td width=\"300\" class=\"name\">采购数量</td>");
                    Response.Write("<td width=\"300\" class=\"name\">单价</td>");
                }
                else
                {
                    Response.Write("<td width=\"300\" class=\"name\">产品名称</td>");
                    Response.Write("<td width=\"300\" class=\"name\">产品数量</td>   ");
                    Response.Write("<td width=\"150\" class=\"name\">起标价格</td>");
                    Response.Write("<td width=\"150\" class=\"name\">单价</td>");
                }
                Response.Write("</tr>");

                if (BidProducts != null)
                {
                    int sum = BidProducts.Count;

                    for (int i = 0; i < sum; i++)
                    {
                        Response.Write("<tr>");
                        if (Type == 0)
                        {
                            //Response.Write("<td>" + BidProducts[i].Bid_Product_Code + "</td>");
                            Response.Write("<td colspan=\"2\">" + BidProducts[i].Bid_Product_Name + "</td>");
                            Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                        }
                        else
                        {
                            Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");
                            Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                            Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                        }
                        Response.Write("<td><input name=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\" id=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\"  type=\"text\" value=\"\" /></td>");
                        Response.Write("</tr>");
                    }

                }

                Response.Write("</table>");
                Response.Write("<ul style=\"width:850px;\">");

                if (Type == 0)
                {
                    Response.Write("<li  style=\"margin-left:150px;\"><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\">提交报价</a></li>");
                }
                else
                {
                    Response.Write("<li style=\"margin-left:150px;\"><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\" >提交竞价</a></li>");
                }
                Response.Write("</ul>");
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("</div>");

            }
            //}
        }
    }




    //报价信息 我的报价记录
    public void MyBidRecord(int BID, IList<BidProductInfo> BidProducts, int Bid_Number, int supplier, DateTime BidEndTime, int Type)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        SQLHelper DBHelper = new SQLHelper();
        if (supplier_id > 0)
        {
            IList<TenderInfo> entitys = GetTenders(BID, supplier_id);
            if (entitys != null)
            {
                //if (entitys.Count < Bid_Number && supplier == 0 && DateTime.Compare(DateTime.Now, BidEndTime) <= 0)
                //{
                IList<TenderProductInfo> tenderProducts = null;
                //if (entitys.Count < Bid_Number && supplier == 0 && DateTime.Compare(DateTime.Now, BidEndTime) <= 0)
                //{
                //    Response.Write("<div class=\"b02_main\">");
                //    Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");
                //    Response.Write("<tr>");
                //    if (Type == 0)
                //    {
                //        //Response.Write("<td width=\"300\" class=\"name\">物料编号</td>");
                //        Response.Write("<td width=\"300\" class=\"name\">产品名称</td>   ");
                //        Response.Write("<td width=\"200\" class=\"name\">采购数</td>");
                //        //Response.Write("<td width=\"200\" class=\"name\">排名</td>");
                //        Response.Write("<td width=\"200\" class=\"name\">单价</td>");

                //    }
                //    //else
                //    //{
                //    //    Response.Write("<td width=\"300\" class=\"name\">产品名称</td>");
                //    //    Response.Write("<td width=\"300\" class=\"name\">产品数量</td>   ");
                //    //    Response.Write("<td width=\"150\" class=\"name\">起标价格</td>");
                //    //    Response.Write("<td width=\"150\" class=\"name\">单价</td>");
                //    //}

                //    Response.Write("</tr>");

                //    tenderProducts = entitys[0].TenderProducts;
                //    //int product_price_sort=0;
                //    if (tenderProducts != null)
                //    {
                //        int sum = tenderProducts.Count;

                //        for (int i = 0; i < sum; i++)
                //        {
                //            if (BidProducts == null)
                //            {
                //                break;
                //            }

                //            if (tenderProducts[i].Tender_Product_BidProductID == BidProducts[i].Bid_Product_ID)
                //            {
                //                Response.Write("<tr>");
                //                if (Type == 0)
                //                {
                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");
                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                //                }
                //                else
                //                {
                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");
                //                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                //                    Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                //                }

                //                Response.Write("<td><input name=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\" id=\"tender_product_" + BidProducts[i].Bid_Product_ID + "\"  type=\"text\" value=\"" + tenderProducts[i].Tender_Price + "\" /></td>");
                //                Response.Write("</tr>");
                //            }

                //        }

                //    }
                //    tenderProducts = null;

                //    Response.Write("</table>");
                //    Response.Write("<ul style=\"width:850px;\">");
                //    if (Type == 0)
                //    {
                //        Response.Write("<li><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\">提交报价</a></li>");
                //    }
                //    else
                //    {
                //        Response.Write("<li><a href=\"javascript:void(0);\" onclick=\"$('#frm_bid').submit();\"  class=\"a05\">提交竞价</a></li>");
                //    }

                //    Response.Write("</ul>");
                //    Response.Write("<div class=\"clear\"></div>");
                //    Response.Write("</div>");
                //}
                Response.Write("<div class=\"blk11_1\" style=\"margin-top: 0px;padding:0 0 10px 0;\">");
                Response.Write("<h3 style=\"margin-bottom:10px;\">我的报价记录(以最后一条记录为准)</h3>");


                int BidRound = entitys.Count + 1;

                foreach (TenderInfo entity in entitys)
                {
                    Response.Write("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #eeeeee;\">");
                    BidRound = BidRound - 1;

                    Response.Write("<tr>");
                    int AllPriceSort = 0;


                    string sql2 = "select RowNum=IDENTITY(INT,1,1),Tender_AllPrice,Tender_BidID into #T1 from Tender where     Tender_BidID=" + entity.Tender_BidID + "  order by Tender_AllPrice asc select ROWNUM from #T1 WHERE  Tender_AllPrice=" + entity.Tender_AllPrice + "DROP TABLE #T1; ";
                    AllPriceSort = tools.CheckInt(DBHelper.ExecuteScalar(sql2).ToString());
                    if (Type == 0)
                    {


                        Response.Write("<td width=\"225px\" style=\"border-right:none;\" class=\"td_bidround\" ><strong>报价轮次:</strong><span><a style=\"font-size:16px;\">" + BidRound + "</a></span></td>   ");

                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");


                        //Response.Write("<td width=\"225\" style=\"border-right:none;\" class=\"td_bidround\"><strong>报价总价:</strong><span><a style=\"font-size:16px;\">" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>");



                        //Response.Write("<td width=\"150\" class=\"name\">总价排名</td>");
                        //Response.Write("<td width=\"150\"><span><a>" + AllPriceSort + "</a></span></td>");
                    }
                    //else
                    //{
                    //    Response.Write("<td width=\"300\" class=\"name\">竞价总价</td>");
                    //    Response.Write("<td width=\"300\" ><span><a>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>   ");
                    //}


                    if (Type == 0)
                    {
                        //Response.Write("<td width=\"225px\" class=\"name\"></td>");
                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");

                        Response.Write("<td width=\"225px\"  style=\"border-right:none;text-align:left;\" class=\"td_bidround\"><strong>报价时间:</strong><span><a style=\"font-size:16px;\">" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");



                    }
                    //else
                    //{
                    //    Response.Write("<td width=\"150\" class=\"name\">竞价时间</td>");
                    //    Response.Write("<td width=\"300\" colspan=\"2\" ><span><a>" + entity.Tender_Addtime.ToString("yyyy-MM-dd HH:mm") + "</a></span></td>");
                    //}



                    Response.Write("</tr>");


                    Response.Write("<tr>");



                    if (Type == 0)
                    {

                        Response.Write("<td width=\"225\" class=\"name\" >产品名称</td>   ");
                        //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");


                        Response.Write("<td width=\"225\" class=\"name\" >单价</td>");
                        Response.Write("<td width=\"225\" class=\"name\">采购数量</td>");

                        Response.Write("<td width=\"225\" class=\"name\">排名</td>");
                        //Response.Write("<td width=\"400\" colspan=\"2\" class=\"name\">总计</td>");

                    }
                    //else
                    //{
                    //    Response.Write("<td width=\"150\" class=\"name\">产品名称</td>");
                    //    //Response.Write("<td width=\"150\" class=\"name\">产品规格</td>   ");
                    //    Response.Write("<td width=\"100\" class=\"name\">产品数量</td>   ");
                    //    Response.Write("<td width=\"150\" class=\"name\">起标价格</td>");
                    //    Response.Write("<td width=\"200\" class=\"name\">排名</td>");
                    //    Response.Write("<td width=\"150\" class=\"name\">单价</td>");
                    //}
                    Response.Write("</tr>");

                    tenderProducts = entity.TenderProducts;
                    if (tenderProducts != null)
                    {
                        int sum = tenderProducts.Count;

                        for (int i = 0; i < sum; i++)
                        {
                            int product_price_sort = 0;
                            string sql1 = "select  RowNum=IDENTITY(INT,1,1),Tender_Price into #T  from Tender_Product tp join Tender t on tp.Tender_TenderID=t.Tender_ID join Bid_Product bp on tp.Tender_Product_BidProductID=bp.Bid_Product_ID where Tender_BidID=" + BID + " and bp.Bid_Product_ID=" + tenderProducts[i].Tender_Product_BidProductID + " order by Tender_Price asc  select min(RowNum) from #T  where Tender_Price=" + tenderProducts[i].Tender_Price + " DROP TABLE #T; ";
                            product_price_sort = tools.CheckInt(DBHelper.ExecuteScalar(sql1).ToString());
                            if (BidProducts == null)
                            {
                                break;
                            }

                            if (tenderProducts[i].Tender_Product_BidProductID == BidProducts[i].Bid_Product_ID)
                            {
                                Response.Write("<tr>");
                                if (Type == 0)
                                {
                                    //    Response.Write("<td>" + BidProducts[i].Bid_Product_Code + "</td>");
                                    Response.Write("<td >" + BidProducts[i].Bid_Product_Name + "</td>");


                                    Response.Write("<td >" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");

                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");

                                    //Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                                }
                                else
                                {
                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Name + "</td>");





                                    Response.Write("<td>" + BidProducts[i].Bid_Product_Amount + "</td>");
                                    Response.Write("<td><span><a>" + pub.FormatCurrency(BidProducts[i].Bid_Product_StartPrice) + "</a></span></td>");
                                    Response.Write("<td><span style=\"color:#ff6600\">" + product_price_sort + "</span></td>");
                                    Response.Write("<td>" + pub.FormatCurrency(tenderProducts[i].Tender_Price) + "</td>");
                                }

                                Response.Write("</tr>");
                            }

                        }

                    }
                    if (Type == 0)
                    {
                        //Response.Write("<td width=\"150\" class=\"name\">报价时间</td>");
                        Response.Write("<td width=\"225\" style=\"border-right:none;\" class=\"td_bidround\"><strong>报价总价:</strong><span><a style=\"font-size:16px;\">" + pub.FormatCurrency(entity.Tender_AllPrice) + "</a></span></td>");


                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");
                    }
                    if (Type == 0)
                    {
                        int All_Price_Sort = 0;
                        string sql1 = "select RowNum=IDENTITY(INT,1,1),Tender_AllPrice,Tender_BidID into #T3 from Tender  WHERE Tender_BidID=" + entity.Tender_BidID + " order by Tender_AllPrice asc select ROWNUM from #T3 where  Tender_AllPrice=" + entity.Tender_AllPrice + " DROP TABLE #T3; ";
                        All_Price_Sort = tools.CheckInt(DBHelper.ExecuteScalar(sql1).ToString());



                        Response.Write("<td width=\"225\"  style=\"border-right:none;\" class=\"td_bidround\"></td>");
                        //Response.Write("<td width=\"150\" class=\"name\">报价时间</td>");
                        Response.Write("<td width=\"225\" style=\"border-right:none; class=\"td_bidround\"><strong>总价排名:</strong><span><a style=\"font-size:16px;\">" + All_Price_Sort + "</a></span></td>");
                    }
                    Response.Write("</table>");
                }



                Response.Write("<div class=\"clear\"></div>");
                Response.Write("</div>");
            }
        }
    }




    /// <summary>
    /// 商家报价列表
    /// </summary>
    public void Tender_List(int Type)
    {


        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"168\" class=\"name\">招标公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">招标单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"100\" class=\"name\">招标状态</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"186\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无报价</td></tr>";
        }
        else
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"168\" class=\"name\">拍卖公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">拍卖单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">竞价时间</td>";
            tmp_head = tmp_head + "<td width=\"100\" class=\"name\">拍卖状态</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"186\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无竞价</td></tr>";
        }


        page_url = "?keyword=" + keyword;
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_SupplierID = " + Supplier_ID + " group by Tender_SupplierID,Tender_BidID "));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "in", " select Bid_Enter_BidID from Bid_Enter where Bid_Enter_SupplierID = " + Supplier_ID + " and Bid_Enter_Type = " + Type));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo page = MyTender.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            DateTime Bid_BidEndTime = DateTime.Now;
            string Bid_Status = "";
            foreach (TenderInfo entity in entitys)
            {
                BidInfo bidinfo = GetBidByID(entity.Tender_BidID);
                if (bidinfo != null)
                {

                    if (DateTime.Compare(bidinfo.Bid_BidStartTime, DateTime.Now) > 0)
                    {
                        Bid_Status = "未开始";
                    }
                    else if ((DateTime.Compare(bidinfo.Bid_BidStartTime, DateTime.Now) < 0) && (DateTime.Compare(bidinfo.Bid_BidEndTime, DateTime.Now) > 0))
                    {
                        Bid_Status = "竞价中";
                    }
                    else
                    {
                        Bid_Status = "已结束";
                    }

                }

                Response.Write("<tr>");
                Response.Write("<td>" + entity.Bid_Title + "</td>");
                Response.Write("<td>" + entity.BidMemberCompany + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</td>");
                Response.Write("<td>" + entity.Tender_Addtime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + Bid_Status + "</td>");
                Response.Write("<td>" + IsWin(entity.Tender_Status, entity.Tender_IsWin) + "</td>");
                if (Type == 0)
                {
                    if ((bidinfo.Bid_OrdersSN.ToString().Trim().Length > 0) && (entity.Tender_IsWin == 1))
                    {
                        Response.Write("<td><span><a href=\"/supplier/order_detail.aspx?orders_sn=" + bidinfo.Bid_OrdersSN + "\" target=\"_blank\">查看订单</a>&nbsp;&nbsp;<a href=\"/supplier/tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                    }
                    else
                    {
                        Response.Write("<td><span><a href=\"/supplier/tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                    }



                }
                else
                {
                    if ((bidinfo.Bid_OrdersSN.ToString().Trim().Length > 0) && (entity.Tender_IsWin == 1))
                    {
                        Response.Write("<td><span><a href=\"/member/order_view.aspx?orders_sn=" + bidinfo.Bid_OrdersSN + "\" target=\"_blank\">查看订单</a>&nbsp;&nbsp;<a href=\"/member/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                    }
                    else
                    {
                        Response.Write("<td><span><a href=\"/member/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                    }

                }

                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }

    public IList<TenderInfo> GetTenders(int Bid, int Supplier_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", Bid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_SupplierID", "=", Supplier_ID.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "Desc"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);

        return entitys;
    }

    public int GetTenderCounts(int Bid, int Supplier_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", Bid.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_SupplierID", "=", Supplier_ID.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "Desc"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);

        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }
    public void AddTender(int Type)
    {
        int Tender_ID = tools.CheckInt(Request.Form["Tender_ID"]);
        int Tender_SupplierID = tools.NullInt(Session["supplier_id"]);
        int Tender_BidID = tools.CheckInt(Request.Form["BID"]);
        DateTime Tender_Addtime = DateTime.Now;
        int Tender_IsWin = 0;
        int Tender_Status = 0;
        double Tender_AllPrice = 0;
        int Tender_IsRefund = 0;
        string Tender_SN = Tender_Code();


        if (Tender_SupplierID <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }

        BidInfo bidinfo = GetBidByID(Tender_BidID);

        if (bidinfo != null)
        {
            if (!IsSignUp(Tender_BidID, Tender_SupplierID))
            {
                pub.Msg("error", "错误信息", "请先报名", false, "{back}");
            }
            if (GetTenderCounts(Tender_BidID, Tender_SupplierID) >= bidinfo.Bid_Number)
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "报价达到上限不可再报", false, "{back}");
                }
                else
                {
                    pub.Msg("error", "错误信息", "竞价达到上限不可再报", false, "{back}");
                }

            }

            if (DateTime.Compare(DateTime.Now, bidinfo.Bid_BidStartTime) < 0 || DateTime.Compare(DateTime.Now, bidinfo.Bid_BidEndTime) > 0)
            {
                if (Type == 0)
                {
                    pub.Msg("error", "错误信息", "不是报价时间不能报价", false, "{back}");
                }
                else
                {
                    pub.Msg("error", "错误信息", "不是竞价时间不能竞价", false, "{back}");
                }

            }
            TenderInfo entity = new TenderInfo();
            entity.Tender_ID = Tender_ID;
            entity.Tender_SupplierID = Tender_SupplierID;
            entity.Tender_BidID = Tender_BidID;
            entity.Tender_Addtime = Tender_Addtime;
            entity.Tender_IsWin = Tender_IsWin;
            entity.Tender_Status = Tender_Status;
            entity.Tender_AllPrice = Tender_AllPrice;
            entity.Tender_IsRefund = Tender_IsRefund;
            entity.Tender_SN = Tender_SN;
            entity.TenderProducts = new List<TenderProductInfo>();
            if (bidinfo.BidProducts != null)
            {
                foreach (BidProductInfo bidProduct in bidinfo.BidProducts)
                {
                    double price = tools.CheckInt(Request.Form["tender_product_" + bidProduct.Bid_Product_ID]);

                    if (price <= 0)
                    {
                        if (Type == 0)
                        {
                            pub.Msg("error", "错误信息", "请填写报价金额", false, "{back}");
                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "请填写竞价金额", false, "{back}");


                        }



                    }
                    if (price < bidProduct.Bid_Product_StartPrice && Type == 1)
                    {
                        pub.Msg("error", "错误信息", "竞价金额不能小于起拍价格", false, "{back}");
                    }

                    TenderProductInfo TenderProduct = new TenderProductInfo();

                    TenderProduct.Tender_Price = price;
                    TenderProduct.Tender_Product_BidProductID = bidProduct.Bid_Product_ID;
                    TenderProduct.Tender_Product_ID = 0;
                    TenderProduct.Tender_Product_Name = "--";
                    TenderProduct.Tender_Product_ProductID = 0;
                    TenderProduct.Tender_TenderID = 0;
                    Tender_AllPrice = Tender_AllPrice + price * bidProduct.Bid_Product_Amount;
                    entity.TenderProducts.Add(TenderProduct);
                    TenderProduct = null;
                }
            }
            entity.Tender_AllPrice = Tender_AllPrice;
            if (MyTender.AddTender(entity))
            {
                if (Type == 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/tender_add.aspx?list=1&BID=" + Tender_BidID);
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/auction_tender_add.aspx?list=1&BID=" + Tender_BidID);
                }


            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string Tender_Code()
    {
        string sn = "";
        bool ismatch = true;
        TenderInfo tender = null;

        sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            tender = MyTender.GetTenderBySN(sn);
            if (tender != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    /// <summary>
    /// 商家报价列表
    /// </summary>
    public void Tender_View_List(int Type)
    {


        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        if (Type == 0)
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">招标公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">招标单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无报价</td></tr>";
        }
        else
        {
            tmp_head = tmp_head + "<div class=\"b14_1_main\">";
            tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            tmp_head = tmp_head + "<tr>";
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">拍卖公告</td>";
            tmp_head = tmp_head + "<td width=\"169\" class=\"name\">拍卖单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">竞价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";
            tmp_head = tmp_head + "</tr>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
            tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无竞价</td></tr>";
        }


        page_url = "?keyword=" + keyword;
        int Supplier_ID = tools.NullInt(Session["supplier_id"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_SupplierID = " + Supplier_ID + " group by Tender_SupplierID,Tender_BidID "));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "in", " select Bid_Enter_BidID from Bid_Enter where Bid_Enter_SupplierID = " + Supplier_ID + " and Bid_Enter_Type = " + Type + " and Bid_Enter_IsShow = " + "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo page = MyTender.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            foreach (TenderInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entity.Bid_Title + "</td>");
                Response.Write("<td>" + entity.BidMemberCompany + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</td>");
                Response.Write("<td>" + entity.Tender_Addtime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + IsWin(entity.Tender_Status, entity.Tender_IsWin) + "</td>");
                if (Type == 0)
                {

                    //Response.Write("<td><span><a href=\"/supplier/tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span><span><a href=\"/supplier/tender_do.aspx?action=tender_edit&Tender_ID=" + entity.Tender_ID + "&IsSupplier=1\">删除</a></span></td>");
                    Response.Write("<td><span><a href=\"/supplier/tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                }
                else
                {

                    //Response.Write("<td><span><a href=\"/member/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span><span><a href=\"/member/auction_tender_do.aspx?action=auction_tender_edit&Tender_ID=" + entity.Tender_ID + "&IsSupplier=0\">删除</a></span></td>");
                    Response.Write("<td><span><a href=\"/member/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">查看</a></span></td>");
                }

                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }

    /// <summary>
    /// 商家中心---拍卖类表 报价信息
    /// </summary>
    /// <param name="Tender_BidID">招标ID</param>
    public void Tender_MemberView(int BidID, int Type)
    {


        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        if (Type == 0)
        {
            //tmp_head = tmp_head + "<td width=\"368\" class=\"name\">报价单位</td>";
            //tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价金额</td>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">排名</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价轮次</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">公司名称</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">报价(总价)</td>";
            tmp_head = tmp_head + "<td width=\"100\" class=\"name\">是否中标</td>";
            //tmp_head = tmp_head + "<td width=\"96\" class=\"name\">备注</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无报价</td></tr>";
        }
        else
        {
            //tmp_head = tmp_head + "<td width=\"368\" class=\"name\">竞价单位</td>";
            //tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价总额</td>";
            tmp_head = tmp_head + "<td width=\"268\" class=\"name\">排名</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价轮次</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">公司名称</td>";

            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">报价(总价)</td>";
            tmp_head = tmp_head + "<td width=\"100\" class=\"name\">是否中标</td>";
            //tmp_head = tmp_head + "<td width=\"96\" class=\"name\">备注</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"6\">暂无竞价</td></tr>";
        }

        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";



        page_url = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_BidID = " + BidID + " group by Tender_SupplierID,Tender_BidID "));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_IsWin", "DESC"));
        //Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));

        if (Type == 0)
        {
            //Type：0  招标 价格由低到高
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_AllPrice", "DESC"));
        }
        else
        {
            //Type：1  拍卖 价格由高到低
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_AllPrice", "ASC"));

        }

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo page = MyTender.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            int i = 0;
            foreach (TenderInfo entity in entitys)
            {
                i++;
                Response.Write("<tr>");
                Response.Write("<td>" + i + "</td>");
                Response.Write("<td>" + GetBidTimes(BidID, entity.Tender_SupplierID) + "</td>");
                Response.Write("<td>" + Supplier_CompanyName(entity.Tender_SupplierID) + "</td>");
                Response.Write("<td><span style=\"color:#ff6600\">" + pub.FormatCurrency(entity.Tender_AllPrice) + "</span></td>");

                Response.Write("<td>" + IsWin(entity.Tender_Status, entity.Tender_IsWin) + "</td>");
                //Response.Write("<td>" +entity.not+ "</td>");
                if (Type == 0)
                {
                    Response.Write("<td><span><a href=\"/member/tender_view.aspx?BID=" + entity.Tender_BidID + "&TenderID=" + entity.Tender_ID + "\" target=\"_blank\">详情</a><a href=\"/supplier/auction_do.aspx?action=winadd1&TenderID=" + entity.Tender_ID + "&Bid_ID=" + BidID + "&Bid_Type=" + Type + "\" target=\"_blank\">中标</a>");

                }
                else
                {
                    Response.Write("<td><span><a href=\"/supplier/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">详情</a><a href=\"/supplier/auction_do.aspx?action=winadd1&TenderID=" + entity.Tender_ID + "&Bid_ID=" + BidID + "&Bid_Type=" + Type + "\" target=\"_blank\">中标</a>");


                }

                if (entity.Tender_IsWin == 1)
                {
                    Response.Write("<a></a>");
                }
                Response.Write("</span></td>");
                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }
    }



    public int GetBidTimes(int Bid, int Supplier_id)
    {
        int BidTimes = 0;


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_SupplierID", "=", Supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", Bid.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        if (entitys != null)
        {
            BidTimes = entitys.Count;
        }

        return BidTimes;
    }

    /// <summary>
    /// 我是买家查看报价
    /// </summary>
    /// <param name="Tender_BidID">招标ID</param>
    public void Tender_Member_List(IList<Glaer.Trade.B2C.Model.BidInfo> list, int Type)
    {


        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        if (Type == 0)
        {
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">报价单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无报价</td></tr>";
        }
        else
        {
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">竞价单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价总额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">竞价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无竞价</td></tr>";
        }

        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";



        page_url = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;
        StringBuilder sb = new StringBuilder();
        sb.Append("select MAX(Tender_ID) from Tender where");
        foreach (BidInfo item in list)
        {
            sb.Append(" OR Tender_BidID = " + item.Bid_ID);

        }
        sb.Remove(sb.ToString().IndexOf("O"), 2);
        sb.Append(" group by Tender_SupplierID,Tender_BidID ");
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in",
               sb.ToString()));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_IsWin", "DESC"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo page = MyTender.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            foreach (TenderInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + Supplier_CompanyName(entity.Tender_SupplierID) + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</td>");
                Response.Write("<td>" + entity.Tender_Addtime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + IsWin(entity.Tender_Status, entity.Tender_IsWin) + "</td>");
                if (Type == 0)
                {
                    Response.Write("<td><span><a href=\"/member/tender_view.aspx?BID=" + entity.Tender_BidID + "&TenderID=" + entity.Tender_ID + "\">详情</a>");

                }
                else
                {
                    Response.Write("<td><span><a href=\"/supplier/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">详情</a>");

                }

                if (entity.Tender_IsWin == 1)
                {
                    Response.Write("<a></a>");
                }
                Response.Write("</span></td>");
                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }

    }

    /// <summary>
    /// 我是买家查看报价
    /// </summary>
    /// <param name="Tender_BidID">招标ID</param>
    public void Tender_Member_List(int Tender_BidID, int Type)
    {


        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "";
        int curpage = tools.CheckInt(Request["page"]);
        int PageSize = 20;
        if (curpage < 1)
        {
            curpage = 1;
        }
        string tmp_head = "", tmp_list = "", tmp_toolbar_bottom = "";

        tmp_head = tmp_head + "<div class=\"b14_1_main\">";
        tmp_head = tmp_head + "<table width=\"973\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        tmp_head = tmp_head + "<tr>";
        if (Type == 0)
        {
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">报价单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">报价金额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">报价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无报价</td></tr>";
        }
        else
        {
            tmp_head = tmp_head + "<td width=\"368\" class=\"name\">竞价单位</td>";
            tmp_head = tmp_head + "<td width=\"140\" class=\"name\">竞价总额</td>";
            tmp_head = tmp_head + "<td width=\"114\" class=\"name\">竞价时间</td>";
            tmp_head = tmp_head + "<td width=\"96\" class=\"name\">是否中标</td>";
            tmp_head = tmp_head + "<td width=\"86\" class=\"name\">操作</td>";

            tmp_list = tmp_list + "<tr><td colspan=\"5\">暂无竞价</td></tr>";
        }

        tmp_head = tmp_head + "</tr>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</table>";
        tmp_toolbar_bottom = tmp_toolbar_bottom + "</div>";



        page_url = "?keyword=" + keyword;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = PageSize;
        Query.CurrentPage = curpage;
        StringBuilder sb = new StringBuilder();
        sb.Append("select MAX(Tender_ID) from Tender where");

        sb.Append("  Tender_BidID = " + Tender_BidID);


        sb.Append(" group by Tender_SupplierID,Tender_BidID ");
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in",
               sb.ToString()));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_IsWin", "DESC"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo page = MyTender.GetPageInfo(Query);

        Response.Write(tmp_head);
        if (entitys != null)
        {
            tmp_list = "";
            foreach (TenderInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + Supplier_CompanyName(entity.Tender_SupplierID) + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</td>");
                Response.Write("<td>" + entity.Tender_Addtime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + IsWin(entity.Tender_Status, entity.Tender_IsWin) + "</td>");
                if (Type == 0)
                {
                    Response.Write("<td><span><a href=\"/member/tender_view.aspx?BID=" + entity.Tender_BidID + "&TenderID=" + entity.Tender_ID + "\">详情</a>");

                }
                else
                {
                    Response.Write("<td><span><a href=\"/supplier/auction_tender_view.aspx?TenderID=" + entity.Tender_ID + "\">详情</a>");

                }

                if (entity.Tender_IsWin == 1)
                {
                    Response.Write("<a></a>");
                }
                Response.Write("</span></td>");
                Response.Write("</tr>");
            }
            Response.Write(tmp_toolbar_bottom);

            Response.Write("<div class=\"page\">");
            pub.Page2(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write(tmp_list); Response.Write(tmp_toolbar_bottom);
        }

    }
    /// <summary>
    /// 是否中标
    /// </summary>
    /// <returns></returns>
    public string IsWin(int Status, int IsWin)
    {
        if (Status == 0)
        {
            return "--";
        }
        else
        {
            if (IsWin == 1)
            {
                return "中标";
            }
            else
            {
                return "未中标";
            }
        }
    }


    public void TenderProductList(IList<BidProductInfo> BidProduct, IList<TenderProductInfo> TenderProducts, int Type)
    {
        if (BidProduct != null && TenderProducts != null)
        {
            int sum = BidProduct.Count;
            for (int i = 0; i < sum; i++)
            {
                Response.Write("<tr>");
                //Response.Write("<td>" + BidProduct[i].Bid_Product_Code + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Name + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Spec + "</td>");
                //Response.Write("<td>" + BidProduct[i].Bid_Product_Brand + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Unit + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Amount + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(TenderProducts[i].Tender_Price) + "</td>");
                if (Type == 1)
                {
                    Response.Write("<td>" + TenderProducts[i].Tender_Product_Name + "</td>");
                    //Response.Write("<td>" + TenderProducts[i].Tender_Product_Name + "</td>");
                }
                Response.Write("</tr>");
            }
        }
    }

    public void AuctionTenderProductList(IList<BidProductInfo> BidProduct, IList<TenderProductInfo> TenderProducts)
    {
        if (BidProduct != null && TenderProducts != null)
        {
            int sum = BidProduct.Count;
            for (int i = 0; i < sum; i++)
            {
                Response.Write("<tr>");

                Response.Write("<td>" + BidProduct[i].Bid_Product_Name + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Spec + "</td>");

                Response.Write("<td>" + BidProduct[i].Bid_Product_Unit + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Amount + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(BidProduct[i].Bid_Product_StartPrice) + "</td>");
                Response.Write("<td><span><a>" + pub.FormatCurrency(TenderProducts[i].Tender_Price) + "</a></span></td>");
                //获取排名  招标价格由低到高  拍卖价格由高到低,招标标示:0  拍卖标示:1
                //Response.Write("<td><span><a>" + TenderSort(BidProduct[i].Bid_BidID, 0, 1) + "</a></span></td>");
                //Response.Write("<td><span><a href=\"" + pageurl.FormatURL(pageurl.product_detail,TenderProducts[i].Tender_Product_ID).ToString()) + "\" target=\"_blank\" title=" + BidProduct[i].Bid_Product_Name  + " >" + BidProduct[i].Bid_Product_Name + "</a></span></td>");
                Response.Write("<td><span>  <a href=\"" + pageurl.FormatURL(pageurl.product_detail, BidProduct[i].Bid_Product_ID.ToString()) + " \" title=" + BidProduct[i].Bid_Product_Name + "  target=\"_blank\">" + "查看商品" + "</a></span></td>");
                Response.Write("<td></td>");

                Response.Write("</tr>");
            }
        }
    }

    //当前招标/拍卖排名
    public int TenderSort(int bid_id, int supplier_id, int type)
    {

        int Sort = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;


        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "=", bid_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_Status", "=", type.ToString()));



        if (type == 0)
        { //招标排名顺序价格由低到高

            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_AllPrice", "ASC"));
        }
        else
        {
            //拍卖排名顺序价格由高到低
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_AllPrice", "DESC"));
        }
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        if (entitys != null)
        {

            foreach (var entity in entitys)
            {
                Sort = Sort + 1;
            }
        }


        return Sort;
    }

    public void TenderProductAdd(IList<BidProductInfo> BidProduct, IList<TenderProductInfo> TenderProducts)
    {
        if (BidProduct != null && TenderProducts != null)
        {
            int sum = BidProduct.Count;
            for (int i = 0; i < sum; i++)
            {
                Response.Write("<tr>");
                Response.Write("<td style=\"border-left:1px solid #eeeeee\">" + BidProduct[i].Bid_Product_Name + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Spec + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Unit + "</td>");
                Response.Write("<td>" + BidProduct[i].Bid_Product_Amount + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(TenderProducts[i].Tender_Price) + "</td>");
                Response.Write("<td><span><a id=\"tender_product_" + TenderProducts[i].Tender_Product_ID + "\">" + TenderProducts[i].Tender_Product_Name + "</a></span><input name=\"tender_product_id" + TenderProducts[i].Tender_Product_ID + "\" id=\"tender_product_id" + TenderProducts[i].Tender_Product_ID + "\" value=\"\" type=\"hidden\" /></td>");
                Response.Write("<td><a style=\"background-image:url(../images/a_bg01.jpg); background-repeat:no-repeat; width:74px; height:26px; font-size:12px; font-weight:normal; text-align:center; line-height:26px; color:#333; display:inline-block; vertical-align:middle; margin-right:7px;\" href=\"javascript:void(0);\" onclick=\"AddProduct(" + TenderProducts[i].Tender_Product_ID + ");\">选择商品</a></td>");
                Response.Write("</tr>");
            }
        }
    }
    public string Supplier_CompanyName(int supplier_id)
    {
        SupplierInfo entity = MySupplier.GetSupplierByID(supplier_id);

        if (entity != null)
        {
            return entity.Supplier_CompanyName;
        }
        else
        {
            return "--";
        }
    }
    #endregion

    #region 中标

  

    private IList<TenderInfo> GetTenderInforbyBid(int Bid)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", Bid.ToString()));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "Desc"));
        return MyTender.GetTenders(Query);

    }

    public void WinBid(int Type)
    {
        int member_id = tools.NullInt(Session["member_id"]);
        int BidID = tools.CheckInt(Request.Form["BidID"]);
        int TenderID = tools.CheckInt(Request.Form["TenderID"]);

        if (member_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        BidInfo bidinfo = GetBidByID(BidID);
        TenderInfo tenderinfo = GetTenderByID(TenderID);
        if (bidinfo != null && tenderinfo != null)
        {
            if (bidinfo.Bid_MemberID != member_id)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_ID != tenderinfo.Tender_BidID)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_SupplierID > 0)
            {
                pub.Msg("error", "错误信息", "操作失败，已选中标方", false, "{back}");
            }
            if (bidinfo.Bid_Status != 1)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_IsAudit != 1)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            }

            bidinfo.Bid_SupplierID = tenderinfo.Tender_SupplierID;
            bidinfo.Bid_SupplierCompany = Supplier_CompanyName(tenderinfo.Tender_SupplierID);
            bidinfo.Bid_FinishTime = DateTime.Now;

            IList<TenderInfo> Tenderinfor_SupplierID = GetTenderInforbyBid(bidinfo.Bid_ID);
            int supplierID = member_id;
            SupplierInfo supplierinfo = MySupplier.GetSupplierByID(supplierID);
            SMS mySMS = new SMS();
            mySMS.Send(supplierinfo.Supplier_Mobile,"尊敬的用户您好，您的投标项目"+bidinfo.Bid_Title+"已中标，请与发布公司联系");
            foreach (TenderInfo item in Tenderinfor_SupplierID)
            {
                if (item.Tender_SupplierID != supplierID)
                {
                    if (Type == 0)
                    {
                        if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,返还保证金", bidinfo.Bid_Bond))
                        {
                            
                            
                                pub.Msg("positive", "操作成功", "操作成功", true, "/member/tender_list.aspx?BID=" + bidinfo.Bid_ID);
                            
                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                        }
                    }
                    else
                    {
                        if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,返还保证金", bidinfo.Bid_Bond)
)
                        {
                            pub.Msg("positive", "操作成功", "操作成功", true, "/member/tender_list.aspx?BID=" + bidinfo.Bid_ID);

                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                        }
                    }
                    item.Tender_Status = 1;
                    item.Tender_IsWin = 1;
                    MyTender.EditTender(item);


                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/tender_list.aspx?BID=" + bidinfo.Bid_ID);
                }

            }
            //if (Type == 0)
            //{
            //    if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,返还保证金", bidinfo.Bid_Bond))
            //    {
            //        MyBid.EditBid(bidinfo, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039"));


            //    }
            //    else
            //    {
            //        pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            //    }

            //}
            //else
            //{
            //    if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,返还保证金", bidinfo.Bid_Bond))
            //    {
            //        MyBid.EditBid(bidinfo, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039"));


            //        tenderinfo.Tender_Status = 1;
            //        tenderinfo.Tender_IsWin = 1;
            //        MyTender.EditTender(tenderinfo);
            //        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_tender_list.aspx?BID=" + bidinfo.Bid_ID);
            //    }
            //    else
            //    {
            //        pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //    }
            //}


            //if (MyBid.EditBid(bidinfo, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            //{
            //tenderinfo.Tender_Status = 1;
            //tenderinfo.Tender_IsWin = 1;
            //MyTender.EditTender(tenderinfo);
            ////0 代表招标
            //if (Type == 0)
            //{
            //if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,未中标,返还保证金", bidinfo.Bid_Bond))
            //{
            //    pub.Msg("positive", "操作成功", "操作成功", true, "/member/tender_list.aspx?BID=" + bidinfo.Bid_ID);
            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            //}

            //}
            //    //1 代表拍卖
            //else
            //{
            //if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,未中标,返还保证金", bidinfo.Bid_Bond))
            //{
            //    tenderinfo.Tender_Status = 1;
            //    tenderinfo.Tender_IsWin = 1;
            //    MyTender.EditTender(tenderinfo);
            //    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_tender_list.aspx?BID=" + bidinfo.Bid_ID);
            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //}

            //}

            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //}

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void WinBid1(int Type, int BidID, int TenderID)
    {
        int member_id = tools.NullInt(Session["member_id"]);
        //int BidID = tools.CheckInt(Request.Form["BidID"]);
        //int TenderID = tools.CheckInt(Request.Form["TenderID"]);

        if (member_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }
        BidInfo bidinfo = GetBidByID(BidID);
        TenderInfo tenderinfo = GetTenderByID(TenderID);
        if (bidinfo != null && tenderinfo != null)
        {
            if (bidinfo.Bid_MemberID != member_id)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_ID != tenderinfo.Tender_BidID)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_SupplierID > 0)
            {
                pub.Msg("error", "错误信息", "操作失败，已选中标方", false, "{back}");
            }
            if (bidinfo.Bid_Status != 1)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_IsAudit != 1)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            }

            bidinfo.Bid_SupplierID = tenderinfo.Tender_SupplierID;
            bidinfo.Bid_SupplierCompany = Supplier_CompanyName(tenderinfo.Tender_SupplierID);
            bidinfo.Bid_FinishTime = DateTime.Now;


            if (Type == 0)
            {
                if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,返还保证金", bidinfo.Bid_Bond))
                {
                    MyBid.EditBid(bidinfo, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039"));

                    tenderinfo.Tender_Status = 1;
                    tenderinfo.Tender_IsWin = 1;
                    MyTender.EditTender(tenderinfo);


                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/Bid_view.aspx?BID=" + bidinfo.Bid_ID);
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                }

            }
            else
            {
                if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,返还保证金", bidinfo.Bid_Bond))
                {
                    tenderinfo.Tender_Status = 1;
                    tenderinfo.Tender_IsWin = 1;
                    MyTender.EditTender(tenderinfo);
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?BID=" + bidinfo.Bid_ID);
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            }


            //if (MyBid.EditBid(bidinfo, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            //{
            //tenderinfo.Tender_Status = 1;
            //tenderinfo.Tender_IsWin = 1;
            //MyTender.EditTender(tenderinfo);
            ////0 代表招标
            //if (Type == 0)
            //{
            //if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,未中标,返还保证金", bidinfo.Bid_Bond))
            //{
            //    pub.Msg("positive", "操作成功", "操作成功", true, "/member/tender_list.aspx?BID=" + bidinfo.Bid_ID);
            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            //}

            //}
            //    //1 代表拍卖
            //else
            //{
            //if (NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,未中标,返还保证金", bidinfo.Bid_Bond))
            //{
            //    tenderinfo.Tender_Status = 1;
            //    tenderinfo.Tender_IsWin = 1;
            //    MyTender.EditTender(tenderinfo);
            //    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_tender_list.aspx?BID=" + bidinfo.Bid_ID);
            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //}

            //}

            //}
            //else
            //{
            //    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            //}

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public bool NotWinTender(int BidID, int Supplider_ID, string Bid_Title, double Bid_Bond)
    {
        bool issucces = false;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_SupplierID != " + Supplider_ID + " and Tender_IsWin = 0 and  Tender_BidID = " + BidID + " group by Tender_SupplierID,Tender_BidID "));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where   Tender_BidID = " + BidID + " group by Tender_SupplierID,Tender_BidID "));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        /// <param name="payAccNo">付款账号</param>
        /// <param name="recvAccNo">收款账号</param>
        /// <param name="recvAccNm">收款账号名</param>



        if (entitys != null)
        {


            foreach (TenderInfo entity in entitys)
            {
                double Bid_Enter_Bond = 0.00;
                ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(Supplider_ID);
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Supplider_ID);
                QueryInfo Query1 = new QueryInfo();
                Query1.PageSize = 1;
                Query1.CurrentPage = 1;
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_BidID", "=", entity.Tender_BidID.ToString()));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BidEnterInfo.Bid_Enter_SupplierID", "=", Supplider_ID.ToString()));
                Query1.OrderInfos.Add(new OrderInfo("BidEnterInfo.Bid_Enter_ID", "Desc"));

                IList<BidEnterInfo> BidEnterInfos = MyBidEnter.GetBidEnters(Query1);
                if (BidEnterInfos != null)
                {
                    foreach (var BidEnterInfo in BidEnterInfos)
                    {
                        Bid_Enter_Bond = BidEnterInfo.Bid_Enter_Bond;
                    }

                }
                string Supplier_CompanyName = "";
                if (supplierinfo != null)
                {
                    Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
                }
                string strResult = string.Empty;
                if (PayAccountInfo != null)
                {


                    if (sendmessages.Transfer(bidguaranteeaccno, PayAccountInfo.SubAccount, PayAccountInfo.CompanyName, Bid_Title, Bid_Enter_Bond, ref strResult, Supplier_CompanyName))
                    {
                        MySupplier.Supplier_Account_Log(entity.Tender_SupplierID, 1, Bid_Bond, Bid_Title);
                        entity.Tender_Status = 1;
                        entity.Tender_IsRefund = 1;
                        MyTender.EditTender(entity);
                        issucces = true;
                    }
                    else
                    {
                        issucces = false;
                    }

                }





            }

        }
        return issucces;
    }
    #endregion


    #region 招标基本
    public void Bid_IsShow(int bid_id, int bidtype)
    {

        BidInfo entity = MyBid.GetBidByID(bid_id, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
        if (entity != null)
        {
            entity.Bid_IsShow = 0;
            if (MyBid.EditBid(entity, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039")))
            {
                if (bidtype == 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/bid_list.aspx");
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_list.aspx");

                }
            }
            else
            {
                if (bidtype == 0)
                {
                    pub.Msg("error", "错误信息", "删除失败", false, "/member/bid_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "删除失败", false, "/supplier/order_list.aspx");
                }

            }
            //if (MyOrders.EditOrders(entity))
            //{
            //    if (issupplier == true)
            //    {
            //        pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/order_list.aspx");
            //    }
            //    else
            //    {
            //        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_list.aspx");

            //    }

            //}
            //else
            //{
            //    if (issupplier == true)
            //    {
            //        pub.Msg("error", "错误信息", "订单删除失败", false, "/supplier/order_list.aspx");
            //    }
            //    else
            //    {
            //        pub.Msg("error", "错误信息", "订单删除失败", false, "/member/order_list.aspx");
            //    }


            //}
        }
        else
        {
            if (bidtype == 0)
            {
                pub.Msg("error", "错误信息", "删除失败", false, "/member/bid_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "删除失败", false, "/supplier/order_list.aspx");
            }

        }



    }

    //报价轮次选择
    public string Bid_Times_Select(int Bid_ID, string Bid_Number)
    {
        string select_str = "";
        select_str += "<select onchange=\"getSelectUnit(this.id)\" id=\"" + Bid_Number + "\" name=\"" + Bid_Number + "\" style=\"height:25px;padding:0px;\">";
        select_str += "<option value=\"1\">1</option>";

        string[] Bid_Times = { "2", "3", "4" };

        if (Bid_ID == 0)
        {
            foreach (string Bid_Time in Bid_Times)
            {
                //if (Bid_Time == "1")
                //{
                //    select_str += "<option value=\"" + Bid_Time + "\" >" + Bid_Time + "</option>";
                //}
                if ((Bid_Time == "2"))
                {
                    select_str += "<option value=\"" + Bid_Time + "\" >" + Bid_Time + "</option>";
                }
                else if ((Bid_Time == "3"))
                {
                    select_str += "<option value=\"" + Bid_Time + "\" >" + Bid_Time + "</option>";
                }
                else if ((Bid_Time == "4"))
                {
                    select_str += "<option value=\"" + Bid_Time + "\" >" + Bid_Time + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + Bid_Time + "\" >" + "1" + "</option>";
                }
            }
        }
        else
        {
            //ProductInfo entity = GetProductByID(Product_ID);
            BidInfo entity = GetBidByID(Bid_ID);
            if (entity != null)
            {
                string BidNumber = entity.Bid_Number.ToString();
                foreach (string Bid_Time in Bid_Times)
                {
                    if (BidNumber == Bid_Time)
                    {
                        select_str += "<option id=\"selected_Unit\" value=\"" + Bid_Time + "\" selected  >" + Bid_Time + "</option>";
                    }
                    else
                    {
                        select_str += "<option value=\"" + Bid_Time + "\"   >" + Bid_Time + "</option>";
                    }
                }
            }
        }

        select_str += "</select>";
        return select_str;
    }

    #endregion


    public void AddProduct()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int TenderID = tools.CheckInt(Request.Form["TenderID"]);
        if (supplier_id <= 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "{back}");
        }

        TenderInfo entity = GetTenderByID(TenderID);
        if (entity != null)
        {
            if (entity.Tender_SupplierID != supplier_id)
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (entity.Tender_IsWin == 0)
            {
                pub.Msg("error", "错误信息", "未中标", false, "{back}");
            }
            if (entity.Tender_IsProduct == 1)
            {
                pub.Msg("error", "错误信息", "已提交报价清单商品", false, "{back}");
            }

            if (entity.TenderProducts != null)
            {
                foreach (TenderProductInfo tenderproduct in entity.TenderProducts)
                {
                    int productid = tools.CheckInt(Request["tender_product_id" + tenderproduct.Tender_Product_ID]);

                    if (productid > 0)
                    {
                        ProductInfo product = MyProduct.GetProductByID(productid);
                        if (product != null)
                        {

                            if (product.Product_IsAudit == 1 && product.Product_IsInsale == 1)
                            {
                                tenderproduct.Tender_Product_ProductID = product.Product_ID;
                                tenderproduct.Tender_Product_Name = product.Product_Name;

                                if (MyTender.EditTenderProduct(tenderproduct))
                                {

                                }
                                else
                                {
                                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

                                }
                            }
                            else
                            {
                                pub.Msg("error", "错误信息", "您选择的商品未上架或者审核未通过", false, "{back}");
                            }
                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "请选择报价商品", false, "{back}");
                        }


                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "请选择报价商品", false, "{back}");
                    }
                }

                entity.Tender_IsProduct = 1;
                if (MyTender.EditTender(entity))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/tender_view.aspx?TenderID=" + entity.Tender_ID);
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    #region 订单相关


    /// <summary>
    /// 是否可生成订单
    /// </summary>
    /// <param name="Bid"></param>
    /// <returns></returns>
    public bool BidOrderStatus(int Bid, ref int supplierID)
    {
        bool status = false;

        BidInfo entity = GetBidByID(Bid);
        supplierID = 0;
        if (entity != null)
        {

            if (entity.Bid_SupplierID > 0 && entity.Bid_IsAudit == 1 && entity.Bid_Status == 1 && entity.Bid_IsOrders == 0)
            {
                if (entity.Bid_Type == 0)
                {
                    supplierID = entity.Bid_SupplierID;
                    if (TenderOrderStatus(entity.Bid_ID, entity.Bid_SupplierID))
                    {
                        status = true;
                    }
                }
                else
                {
                    supplierID = entity.Bid_ExcludeSupplierID;
                    status = true;
                }
            }
        }

        return status;
    }

    public bool TenderOrderStatus(int BID, int SupplierID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_BidID", "=", BID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_SupplierID", "=", SupplierID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsWin", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsProduct", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "Desc"));

        IList<TenderInfo> entitys = MyTender.GetTenders(Query);

        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public string BidOrderProductList(int BID)
    {
        StringBuilder strHTML = new StringBuilder();

        BidInfo bidinfo = GetBidByID(BID);

        if (bidinfo != null)
        {
            strHTML.Append("<h2>订单商品信息</h2>");
            strHTML.Append("<div class=\"b38_main03\">");
            //strHTML.Append("<h3>");
            //strHTML.Append("<ul>");
            //if(bidinfo.Bid_Type==0)
            //{
            //    strHTML.Append("<li style=\"font-size:13px; text-align:left;\"><strong>" + bidinfo.Bid_SupplierCompany + "</strong>发货订单<span></span></li>");
            //}
            //else
            //{
            //    strHTML.Append("<li style=\"font-size:13px; text-align:left;\"><strong>" + Supplier_CompanyName(bidinfo.Bid_ExcludeSupplierID) + "</strong>发货订单<span></span></li>");
            //}

            //strHTML.Append("<li style=\" margin-left:90px;\">单 价</li>");
            //strHTML.Append("<li style=\" margin-left:232px;\">数 量</li>");
            //strHTML.Append("<li style=\" margin-left:130px;\">小计 （元）</li>");
            //strHTML.Append("</ul>");
            //strHTML.Append("<div class=\"clear\"></div>");
            //strHTML.Append("</h3>");
            strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            strHTML.Append("<tr>");
            if (bidinfo.Bid_Type == 0)
            {
                strHTML.Append("<td width=\"283\">" + bidinfo.Bid_SupplierCompany + "</td>");
            }
            else
            {
                strHTML.Append("<td width=\"283\">" + Supplier_CompanyName(bidinfo.Bid_ExcludeSupplierID) + "</td>");
            }

            strHTML.Append("<td width=\"345\">单 价</td>");
            strHTML.Append("<td width=\"174\">数 量</td>");
            strHTML.Append("<td width=\"170\">小计 （元）</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            DataTable datatable = MyBid.GetOrderProducts(BID);
            double sum_price = 0;
            if (datatable != null)
            {

                foreach (DataRow entity in datatable.Rows)
                {

                    strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    strHTML.Append("<tr>");
                    strHTML.Append("<td width=\"283\">");
                    strHTML.Append("<dl>");
                    if (bidinfo.Bid_Type == 0)
                    {
                        ProductInfo product = MyProduct.GetProductByID(tools.CheckInt(entity["Tender_Product_ProductID"].ToString()));
                        if (product != null)
                        {
                            strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\"></a></dt>");
                            strHTML.Append("<dd>");
                            strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\">" + entity["Tender_Product_Name"].ToString() + "</a></p>");
                            //strHTML.Append("<p>编号：" + product.Product_Code + "</p>");
                            strHTML.Append("<p>" + new Product().Product_Extend_Content_New(product.Product_ID) + "</p>");
                            strHTML.Append("</dd>");
                            strHTML.Append("<div class=\"clear\"></div>");
                            strHTML.Append("</dl>");
                            strHTML.Append("</td>");

                            strHTML.Append("<td width=\"345\">" + pub.FormatCurrency(tools.CheckFloat(entity["Tender_Price"].ToString())) + "</td>");
                            strHTML.Append("<td width=\"174\">" + entity["Bid_Product_Amount"].ToString() + "</td>");
                            strHTML.Append("<td width=\"170\">" + pub.FormatCurrency(tools.CheckInt(entity["Bid_Product_Amount"].ToString()) * tools.CheckFloat(entity["Tender_Price"].ToString())) + "</td>");
                            sum_price = sum_price + tools.CheckInt(entity["Bid_Product_Amount"].ToString()) * tools.CheckFloat(entity["Tender_Price"].ToString());
                        }
                    }
                    else
                    {
                        ProductInfo product = MyProduct.GetProductByID(tools.CheckInt(entity["Bid_Product_ProductID"].ToString()));
                        if (product != null)
                        {
                            strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\"></a></dt>");
                            strHTML.Append("<dd>");
                            strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\">" + entity["Bid_Product_Name"].ToString() + "</a></p>");
                            //strHTML.Append("<p>编号：" + product.Product_Code + "</p>");
                            strHTML.Append("<p>" + new Product().Product_Extend_Content_New(product.Product_ID) + "</p>");
                            strHTML.Append("</dd>");
                            strHTML.Append("<div class=\"clear\"></div>");
                            strHTML.Append("</dl>");
                            strHTML.Append("</td>");

                            strHTML.Append("<td width=\"345\">" + pub.FormatCurrency(tools.CheckFloat(entity["Tender_Price"].ToString())) + "</td>");
                            strHTML.Append("<td width=\"174\">" + entity["Bid_Product_Amount"].ToString() + "</td>");
                            strHTML.Append("<td width=\"170\">" + pub.FormatCurrency(tools.CheckInt(entity["Bid_Product_Amount"].ToString()) * tools.CheckFloat(entity["Tender_Price"].ToString())) + "</td>");
                            sum_price = sum_price + tools.CheckInt(entity["Bid_Product_Amount"].ToString()) * tools.CheckFloat(entity["Tender_Price"].ToString());
                        }
                    }
                    strHTML.Append("</tr>");
                    strHTML.Append("</table>");
                }
                strHTML.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;商品总金额：<strong>" + pub.FormatCurrency(sum_price) + "</strong></b>");
                strHTML.Append("</div>");
                BidSetOrdersConfirmPrice(sum_price);
            }
        }

        return strHTML.ToString();
    }


    public void BidSetOrdersConfirmPrice(double total_allprice)
    {

        Session["total_price"] = total_allprice;        //商品总价
        Session["total_coin"] = tools.CheckInt(total_allprice.ToString());
    }

    public double Get_Cart_Weight(int BID)
    {
        double total_weight = 0;
        BidInfo bidinfo = GetBidByID(BID);
        if (bidinfo != null)
        {


            DataTable datatable = MyBid.GetOrderProducts(BID);

            if (datatable != null)
            {
                foreach (DataRow entity in datatable.Rows)
                {
                    if (bidinfo.Bid_Type == 0)
                    {

                        ProductInfo product = MyProduct.GetProductByID(tools.CheckInt(entity["Tender_Product_ProductID"].ToString()));
                        if (product != null)
                        {
                            total_weight = total_weight + product.Product_Weight * tools.CheckInt(entity["Bid_Product_Amount"].ToString());
                        }
                    }
                    else
                    {
                        ProductInfo product = MyProduct.GetProductByID(tools.CheckInt(entity["Bid_Product_ProductID"].ToString()));
                        if (product != null)
                        {
                            total_weight = total_weight + product.Product_Weight * tools.CheckInt(entity["Bid_Product_Amount"].ToString());
                        }
                    }
                }
            }
        }

        return total_weight;
    }


    public DataTable BidOrders_Goods_Add(int BID, ref int Type)
    {
        BidInfo bidinfo = GetBidByID(BID);
        Type = 0;
        if (bidinfo != null)
        {
            Type = bidinfo.Bid_Type;

            DataTable datatable = MyBid.GetOrderProducts(BID);

            return datatable;
        }
        else
        {
            return null;
        }
    }


    public void EditBidOrders(int BID, string Orders_SN)
    {
        BidInfo entity = GetBidByID(BID);

        if (entity != null)
        {
            entity.Bid_IsOrders = 1;
            entity.Bid_OrdersTime = DateTime.Now;
            entity.Bid_OrdersSN = Orders_SN;

            MyBid.EditBid(entity, pub.CreateUserPrivilege("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039"));
        }
    }
    #endregion


    //快速发布方法
    public void BidUpRequireQuickAdd()
    {
        string contractman = tools.CheckStr(Request["signupcontractman"]);
        string signupcontent = tools.CheckStr(Request["signupcontent"]);
        string signupcontractmobile = tools.CheckStr(Request["signupcontractmobile"]);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]).Trim()).ToLower();
        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "错误信息", "验证码错误", false, "{back}");
        }
        if (contractman.Length < 1)
        {
            pub.Msg("error", "错误信息", "请填写联系人姓名", false, "{back}");
        }

        if (signupcontractmobile.Length < 1)
        {
            pub.Msg("error", "错误信息", "请填写联系人电话", false, "{back}");
        }
        if (!pub.Checkmobile(signupcontractmobile))
        {
            pub.Msg("error", "错误信息", "请填写正确的联系方式", false, "{back}");
        }
        string url = Request.RawUrl;
        BidUpRequireQuickInfo entity = new BidUpRequireQuickInfo();

        entity.Bid_Up_ContractMan = contractman;
        entity.Bid_Up_ContractContent = signupcontent;

        entity.Bid_Up_ContractMobile = signupcontractmobile;
        entity.Bid_Up_AddTime = DateTime.Now;
        if (MyBidUpRequire.AddBidUpRequireQuick(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "{back}");
        }
        else
        {
            pub.Msg("error", "错误信息", "添加失败", false, "{back}");
        }


    }
}