using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
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

    private IBid MyBLL;
    private IBidProduct MyBidProduct;
    private IBidAttachments MyBidAttachments;
    private IBidEnter MyBidEnter;
    private ITools tools;
    private ITender MyTender;
    private Supplier MySupplier;
	public Bid()
	{
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        MyBLL = BidFactory.CreateBid();
        tools = ToolsFactory.CreateTools();
        MyBidProduct = BidProductFactory.CreateBidProduct();
        MyBidAttachments = BidAttachmentsFactory.CreateBidAttachments();
        MyBidEnter = BidEnterFactory.CreateBidEnter();
        MyTender = TenderFactory.CreateTender();
        MySupplier = new Supplier();
	}

    public BidInfo GetBidByID(int ID)
    {
        return MyBLL.GetBidByID(ID, Public.GetUserPrivilege());
    }
    public TenderInfo GetTenderByID(int ID)
    {
        return MyTender.GetTenderByID(ID);
    }

    public BidProductInfo GetBidProductByID(int ID)
    {
        return MyBidProduct.GetBidProductByID(ID);
    }

    public BidAttachmentsInfo GetBidAttachmentsByID(int ID)
    {
        return MyBidAttachments.GetBidAttachmentsByID(ID);
    }
    public void Bid_Audit(int Audit,int Type)
    {
        int BID = tools.CheckInt(Request["BID"]);
        string Bid_AuditRemarks = tools.CheckStr(Request["Bid_AuditRemarks"]);
        if(BID<=0)
        {
            Public.Msg("error", "错误信息", "请选择要操作的项目", false, "{back}");
            return;
        }
        BidInfo entity = GetBidByID(BID);

        if(entity!=null)
        {
            entity.Bid_IsAudit = Audit;
            entity.Bid_AuditRemarks = Bid_AuditRemarks;
            entity.Bid_AuditTime = DateTime.Now;
            entity.Bid_IsShow = 1;
            if(MyBLL.EditBid(entity,Public.GetUserPrivilege()))
            {
                if(Type==0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "/bid/Bid_list.aspx");
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "Bid_list.aspx");
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，稍后重试", false, "{back}");
        }
    }


    public virtual void AddBid()
    {
        int Bid_ID = tools.CheckInt(Request.Form["Bid_ID"]);
        int Bid_MemberID = 0;
        string Bid_MemberCompany = tools.CheckStr(Request.Form["Bid_MemberCompany"]);
        int Bid_SupplierID = 0;
        string Bid_SupplierCompany = "";
        string Bid_Title = tools.CheckStr(Request.Form["Bid_Title"]);
        //DateTime Bid_EnterStartTime = tools.NullDate(Request.Form["Bid_EnterStartTime"]);
        //DateTime Bid_EnterEndTime = tools.NullDate(Request.Form["Bid_EnterEndTime"]);
        DateTime Bid_EnterStartTime = DateTime.Now;
        DateTime Bid_EnterEndTime = DateTime.Now;
        DateTime Bid_BidStartTime = tools.NullDate(Request.Form["Bid_BidStartTime"]);
        DateTime Bid_BidEndTime = tools.NullDate(Request.Form["Bid_BidEndTime"]);
        DateTime Bid_AddTime = DateTime.Now;
        double Bid_Bond = tools.CheckFloat(Request.Form["Bid_Bond"]);
        int Bid_Number = tools.CheckInt(Request.Form["Bid_Number"]);
        int Bid_Status = 0;
        string Bid_Content = Request.Form["Bid_Content"]; 
        int Bid_ProductType = tools.CheckInt(Request.Form["Bid_ProductType"]);
        double Bid_AllPrice = 0;
        int Bid_Type = 0;
        string Bid_Contract = "";
        int Bid_IsAudit = 0;
        DateTime Bid_AuditTime = DateTime.Now;
        string Bid_AuditRemarks = "";
        int Bid_ExcludeSupplierID = 0;
        string Bid_SN = BID_SN();
        DateTime Bid_DeliveryTime = DateTime.Now;
        int Bid_IsOrders = 0;
        DateTime Bid_OrdersTime = DateTime.Now;
        string Bid_OrdersSN = "";
        DateTime Bid_FinishTime = DateTime.Now;
        if (Bid_Title.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写公告标题", false, "{back}");
        }

        if (Bid_MemberCompany.Length == 0)
        {
          Public.Msg("error", "错误信息", "请填写采购商名称", false, "{back}");
        }

        if (DateTime.Compare(Bid_EnterStartTime, Bid_EnterEndTime) > 0)
        {

            Public.Msg("error", "错误信息", "报名时间有误", false, "{back}");
        }

        if (DateTime.Compare(Bid_BidStartTime, Bid_BidEndTime) > 0)
        {

            Public.Msg("error", "错误信息", "报价时间有误", false, "{back}");

        }

        if (Bid_Number <= 0)
        {

            Public.Msg("error", "错误信息", "请填写报价轮次", false, "{back}");


        }
        if (Bid_Bond < 0)
        {
            Public.Msg("error", "错误信息", "请确保保证金金额不小于零", false, "{back}");
        }

        BidInfo entity = new BidInfo();
        entity.Bid_ID = Bid_ID;
        entity.Bid_MemberID = Bid_MemberID;
        entity.Bid_MemberCompany = Bid_MemberCompany;
        entity.Bid_SupplierID = Bid_SupplierID;
        entity.Bid_SupplierCompany = Bid_SupplierCompany;
        entity.Bid_Title = Bid_Title;
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
        entity.Bid_SN = Bid_SN;
        entity.Bid_DeliveryTime = Bid_DeliveryTime;
        entity.Bid_IsOrders = Bid_IsOrders;
        entity.Bid_OrdersTime = Bid_OrdersTime;
        entity.Bid_OrdersSN = Bid_OrdersSN;
        entity.Bid_FinishTime = Bid_FinishTime;

        if (MyBLL.AddBid(entity, Public.GetUserPrivilege()))
        {
            BidInfo bidinfo = MyBLL.GetBidBySN(Bid_SN, Public.GetUserPrivilege());

            if(bidinfo!=null)
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Bid_Product_add.aspx?BID=" + bidinfo.Bid_ID);
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditBid(int Type)
    {
        int Bid_ID = tools.CheckInt(Request.Form["Bid_ID"]);
        int Bid_MemberID = 0;
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

        BidInfo entity = GetBidByID(Bid_ID);
        if (entity != null)
        {
            if (entity.Bid_MemberID != Bid_MemberID)
            {
                Public.Msg("error", "错误信息", "操作失败，您没有权限", false, "{back}");
            }

            if (Bid_Title.Length == 0)
            {
                Public.Msg("error", "错误信息", "请填写公告标题", false, "{back}");
            }
            if (Bid_MemberCompany.Length == 0)
            {
                if (Type == 0)
                {
                    Public.Msg("error", "错误信息", "请填写采购商名称", false, "{back}");
                }
                else
                {
                    Public.Msg("error", "错误信息", "请填写拍卖用户名称", false, "{back}");
                }
            }

            if (DateTime.Compare(Bid_EnterStartTime, Bid_EnterEndTime) > 0)
            {
                Public.Msg("error", "错误信息", "报名时间有误", false, "{back}");
            }

            if (DateTime.Compare(Bid_BidStartTime, Bid_BidEndTime) > 0)
            {
                if (Type == 0)
                {
                    Public.Msg("error", "错误信息", "报价时间有误", false, "{back}");
                }
                else
                {
                    Public.Msg("error", "错误信息", "竞价时间有误", false, "{back}");
                }
            }

            if (Bid_Number <= 0)
            {
                if (Type == 0)
                {
                    Public.Msg("error", "错误信息", "请填写报价轮次", false, "{back}");
                }
                else
                {
                    Public.Msg("error", "错误信息", "请填写竞价轮次", false, "{back}");
                }
            }
            if (Bid_Bond < 0)
            {
                Public.Msg("error", "错误信息", "请确保保证金金额不小于零", false, "{back}");
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

            if (MyBLL.EditBid(entity, Public.GetUserPrivilege()))
            {
                if (Type == 0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "bid_edit.aspx?BID=" + entity.Bid_ID);
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "auction_view.aspx?BID=" + entity.Bid_ID);
                }


            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void ReleaseBid(int Type)
    {
        int Bid_ID = tools.CheckInt(Request["BID"]);
        int Bid_MemberID = 0;

        BidInfo entity = GetBidByID(Bid_ID);
        if (entity != null)
        {
            if (entity.Bid_MemberID != Bid_MemberID)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }
            if (entity.Bid_Status > 0)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
                return;
            }

            if(entity.BidProducts==null)
            {
                Public.Msg("error", "错误信息", "至少添加一个商品", false, "{back}");
                return;
            }
            entity.Bid_Status = 1;
            if (MyBLL.EditBid(entity, Public.GetUserPrivilege()))
            {
                if (Type == 0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "bid_view.aspx?BID=" + entity.Bid_ID);
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "auction_view.aspx?BID=" + entity.Bid_ID);
                }

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
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
        if (Bid_BidID <= 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "招标不存在", false, "{back}");
            }
            else
            {
                Public.Msg("error", "错误信息", "拍卖不存在", false, "{back}");
            }

        }
        if (Check_MemberAndBid(Bid_BidID))
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "不可添加", false, "bid_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "不可添加", false, "auction_list.aspx");
            }

        }

        //if (Bid_Product_Code.Length == 0)
        //{
        //    if (Type == 0)
        //    {
        //        Public.Msg("error", "错误信息", "请填写物料编号", false, "{back}");
        //    }
        //    else
        //    {
        //        Public.Msg("error", "错误信息", "请填写产品编号", false, "{back}");
        //    }

        //}
        if (Bid_Product_Name.Length == 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "请填写物料名称", false, "{back}");

            }
            else
            {
                Public.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            }

        }

        if (Bid_Product_Spec.Length == 0)
        {

            Public.Msg("error", "错误信息", "请填写型号规格", false, "{back}");
        }

        //if (Bid_Product_Brand.Length == 0)
        //{
        //    Public.Msg("error", "错误信息", "请填写品牌名称", false, "{back}");
        //}

        if (Bid_Product_Unit.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写计量单位", false, "{back}");
        }

        if (Bid_Product_Amount <= 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "请填写采购数量", false, "{back}");
            }
            else
            {
                Public.Msg("error", "错误信息", "请填写产品数量", false, "{back}");
            }


        }

        if (Bid_Product_Delivery.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写物流信息", false, "{back}");
        }

        if (Type == 1)
        {
            if (Bid_Product_StartPrice <= 0)
            {
                Public.Msg("error", "错误信息", "请填写起标价格", false, "{back}");
            }

            if (Bid_Product_Img.Length == 0)
            {
                Public.Msg("error", "错误信息", "请上传产品图片", false, "{back}");
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
                Public.Msg("positive", "操作成功", "操作成功", true, "Bid_edit.aspx?list=1&BID=" + Bid_BidID);
            }
            else
            {
                //Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_view.aspx?list=1&BID=" + Bid_BidID);
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

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
        if (Bid_BidID <= 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "招标不存在", false, "{back}");
            }
            else
            {
                Public.Msg("error", "错误信息", "拍卖不存在", false, "{back}");
            }
        }

        if (Check_MemberAndBid(Bid_BidID))
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "不可修改", false, "bid_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "不可修改", false, "auction_list.aspx");
            }
        }
        //if (Bid_Product_Code.Length == 0)
        //{
        //    if (Type == 0)
        //    {
        //        Public.Msg("error", "错误信息", "请填写物料编号", false, "{back}");
        //    }
        //    else
        //    {
        //        Public.Msg("error", "错误信息", "请填写产品编号", false, "{back}");
        //    }

        //}
        if (Bid_Product_Name.Length == 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "请填写物料名称", false, "{back}");

            }
            else
            {
                Public.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            }

        }

        if (Bid_Product_Spec.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写型号规格", false, "{back}");
        }

        //if (Bid_Product_Brand.Length == 0)
        //{
        //    Public.Msg("error", "错误信息", "请填写品牌名称", false, "{back}");
        //}

        if (Bid_Product_Unit.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写计量单位", false, "{back}");
        }

        if (Bid_Product_Amount <= 0)
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "请填写采购数量", false, "{back}");
            }
            else
            {
                Public.Msg("error", "错误信息", "请填写产品数量", false, "{back}");
            }


        }

        if (Bid_Product_Delivery.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写物流信息", false, "{back}");
        }

        if (Type == 1)
        {
            if (Bid_Product_StartPrice <= 0)
            {
                Public.Msg("error", "错误信息", "请填写起标价格", false, "{back}");
            }

            if (Bid_Product_Img.Length == 0)
            {
                Public.Msg("error", "错误信息", "请上传产品图片", false, "{back}");
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
                Public.Msg("positive", "操作成功", "操作成功", true, "Bid_edit.aspx?list=1&BID=" + Bid_BidID);
            }
            else
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "auction_view.aspx?list=1&BID=" + Bid_BidID);
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    //逻辑删除招标/拍卖信息
    public void EditBidByID(int bid_id,int bidtype)
    {

        BidInfo entity = GetBidByID(bid_id);
       
        entity.Bid_IsShow=0;




        if (MyBLL.EditBid(entity, Public.GetUserPrivilege()))
        {
            if (bidtype == 0)
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "/Bid/Bid_list.aspx");
            }
            else
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "/Bid/auction_list.aspx");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    //逻辑删除招标报名/拍卖报名信息
    public void EditTenderByID(int tender_id, int tendertype)
    {

        //BidInfo entity = GetBidByID(bid_id);
        TenderInfo entity = GetTenderByID(tender_id);
        entity.Tender_IsShow = 0;




        if (MyTender.EditTender(entity))
        {
            if (tendertype == 0)
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "/Bid/Tender_list.aspx");
            }
            else
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "/Bid/auction_tender_list.aspx");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelBidProduct(int Type)
    {
        int ProductID = tools.CheckInt(Request["ProductID"]);
        int BidID = tools.CheckInt(Request["BidID"]);

        if (!Check_MemberAndBid(BidID))
        {
            MyBidProduct.DelBidProduct(ProductID);
            if (Type == 0)
            {
                Response.Redirect("Bid_edit.aspx?list=1&BID=" + BidID);
            }
            else
            {
                Response.Redirect("auction_view.aspx?list=1&BID=" + BidID);
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

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

        int Type = 0;
        BidInfo bidInfo = GetBidByID(Bid_Attachments_BidID);

        if (bidInfo == null)
        {
            Public.Msg("error", "错误信息", "信息不存在", false, "{back}");
        }
        else
        {
            if (bidInfo.Bid_Type == 1)
            {
                Type = 1;
            }
        }
        if (Check_MemberAndBid(Bid_Attachments_BidID))
        {
            if (Type == 0)
            {
                Public.Msg("error", "错误信息", "不可添加", false, "bid_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "不可添加", false, "auction_list.aspx");
            }

        }

        //if (Bid_Attachments_Sort <= 0)
        //{
        //    Public.Msg("error", "错误信息", "请填写序号", false, "{back}");
        //}

        if (Bid_Attachments_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写附件名称", false, "{back}");
        }

        //if (Bid_Attachments_format.Length == 0)
        //{
        //    Public.Msg("error", "错误信息", "请填写文件格式", false, "{back}");
        //}

        //if (Bid_Attachments_Size.Length == 0)
        //{
        //    Public.Msg("error", "错误信息", "请填写附件大小", false, "{back}");
        //}
        if (Bid_Attachments_Path.Length == 0)
        {
            Public.Msg("error", "错误信息", "请上传附件", false, "{back}");
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
                Public.Msg("positive", "操作成功", "操作成功", true, "bid_edit.aspx?list=2&BID=" + Bid_Attachments_BidID);
            }
            else
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "auction_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

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

            int Type = 0;
            BidInfo bidInfo = GetBidByID(Bid_Attachments_BidID);
            if (bidInfo == null)
            {
                Public.Msg("error", "错误信息", "信息不存在", false, "{back}");
            }
            else
            {
                if (bidInfo.Bid_Type == 1)
                {
                    Type = 1;
                }
            }
            if (Check_MemberAndBid(Bid_Attachments_BidID))
            {
                if (Type == 0)
                {
                    Public.Msg("error", "错误信息", "不可添加", false, "bid_list.aspx");
                }
                else
                {
                    Public.Msg("error", "错误信息", "不可添加", false, "auction_list.aspx");
                }

            }

            //if (Bid_Attachments_Sort <= 0)
            //{
            //    Public.Msg("error", "错误信息", "请填写序号", false, "{back}");
            //}

            if (Bid_Attachments_Name.Length == 0)
            {
                Public.Msg("error", "错误信息", "请填写附件名称", false, "{back}");
            }

            //if (Bid_Attachments_format.Length == 0)
            //{
            //    Public.Msg("error", "错误信息", "请填写文件格式", false, "{back}");
            //}

            //if (Bid_Attachments_Size.Length == 0)
            //{
            //    Public.Msg("error", "错误信息", "请填写附件大小", false, "{back}");
            //}
            if (Bid_Attachments_Path.Length == 0)
            {
                Public.Msg("error", "错误信息", "请上传附件", false, "{back}");
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
                    Public.Msg("positive", "操作成功", "操作成功", true, "bid_edit.aspx?list=2&BID=" + Bid_Attachments_BidID);
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "auction_view.aspx?list=2&BID=" + Bid_Attachments_BidID);
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelBidAttachments(int Type)
    {
        int AttID = tools.CheckInt(Request["AttID"]);
        int BidID = tools.CheckInt(Request["BidID"]);

        if (!Check_MemberAndBid(BidID))
        {
            MyBidAttachments.DelBidAttachments(AttID);
            if (Type == 0)
            {
                Response.Redirect("Bid_edit.aspx?list=2&BID=" + BidID);
            }
            else
            {
                Response.Redirect("auction_view.aspx?list=2&BID=" + BidID);
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }
    public string BID_SN()
    {
        string sn = "";
        bool ismatch = true;
        BidInfo bidinfo = null;

        sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + Public.Createvkey(5);
        while (ismatch == true)
        {
            bidinfo = MyBLL.GetBidBySN(sn, Public.GetUserPrivilege());
            if (bidinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyyyMMdd") + Public.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    public bool CheckBid(int BID)
    {
        BidInfo entity = GetBidByID(BID);
        if(entity!=null)
        {
            if(entity.Bid_MemberID>0)
            {
                return false;
            }
            else
            {
                if(entity.Bid_SupplierID==0&&entity.Bid_IsAudit==1&&DateTime.Compare(DateTime.Today,entity.Bid_BidEndTime)>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
    }
    public bool Check_MemberAndBid(int BidID)
    {
        int member_id = 0;

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
        return false;
    }
    public string GetBids(int Type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int IsAuditStatus = tools.CheckInt(Request["IsAuditStatus"]);

        IsAuditStatus = IsAuditStatus - 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Type", "=", Type.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_IsShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_Status", "=", "1"));
        if (IsAuditStatus>=0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_IsAudit", "=", IsAuditStatus.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "BidInfo.Bid_Title", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR", "str", "BidInfo.Bid_MemberCompany", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "BidInfo.Bid_SupplierCompany", "like", keyword));
        }
       
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<BidInfo> entitys = MyBLL.GetBids(Query, Public.GetUserPrivilege());

        if(entitys!=null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            foreach (BidInfo entity in entitys)
            {
                jsonBuilder.Append("{\"BidInfo.Bid_ID\":" + entity.Bid_ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Bid_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Bid_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Bid_MemberCompany));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Bid_EnterStartTime.ToString("yyyy-MM-dd")+"至"+entity.Bid_EnterEndTime.ToString("yyyy-MM-dd"));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Bid_BidStartTime.ToString("yyyy-MM-dd") + "至" + entity.Bid_BidEndTime.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");

                if(Type==0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Bid_DeliveryTime.ToString("yyyy-MM-dd"));
                    jsonBuilder.Append("\",");
                }
                

                if(entity.Bid_SupplierID==0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Bid_SupplierCompany));
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(IsAudit(entity.Bid_IsAudit));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(SignUpNumber(entity.Bid_ID));
                jsonBuilder.Append("\",");

                if(Type==0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"tender_list.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"查看报价数\\\">" + TenderNumber(entity.Bid_ID) + "</a>");
                    jsonBuilder.Append("\",");

                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"auction_tender_list.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"查看竞价数\\\">" + TenderNumber(entity.Bid_ID) + "</a>");
                    jsonBuilder.Append("\",");
                }
                


                jsonBuilder.Append("\"");

                if(Public.CheckPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"))
                {
                    if(Type==0)
                    {
                        if (entity.Bid_IsAudit > 0)
                        {


                            jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"bid_view.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"查看\\\" target=\\\"_blank\\\">查看</a>");
                        }
                        else
                        {
                            if(entity.Bid_MemberID==0&&entity.Bid_Status==0)
                            {
                                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"bid_edit.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"修改\\\">修改</a>&nbsp;&nbsp;");
                            }
                            jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"bid_view.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"查看\\\">查看</a>");
                        }
                        //jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"> <a href=\"javascript:void(0);\" onclick=\"confirmdelete('bid_do.aspx?action=movebid&BidID=" + entity.Bid_ID + "&bidtype=0')\" title=\"删除\">删除</a>");
                      //  jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"> <a href=\"javascript:void(0);\" title=\"删除\">删除</a>");
                        jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmlogdelete('bid_do.aspx?action=movebid&BidID=" + entity.Bid_ID + "&bidtype=" + "0" + "')\\\" title=\\\"删除\\\">删除</a>");


                        //Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\"><img src=\"/images/icon_edit.gif\" alt=\"修改\"> <a href=\"bid_Attachments_edit.aspx?AttID=" + entity.Bid_Attachments_ID + "\" title=\\\"修改\\\">修改</a>&nbsp;&nbsp;<img src=\"/images/icon_del.gif\"  alt=\"删除\"> <a href=\"javascript:void(0);\" onclick=\"confirmdelete('bid_do.aspx?action=moveAtt&BidID=" + entity.Bid_Attachments_BidID + "&AttID=" + entity.Bid_Attachments_ID + "')\" title=\"删除\">删除</a></td>";
                    }
                    else
                    {
                        jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"auction_view.aspx?BID=" + entity.Bid_ID + "\\\" title=\\\"查看\\\">查看</a>");
                        //jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"> <a href=\"javascript:void(0);\" onclick=\"confirmdelete('bid_do.aspx?action=movebid&BidID=" + entity.Bid_ID + "&bidtype=1')\" title=\"删除\">删除</a>");
                        jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmlogdelete('bid_do.aspx?action=movebid&BidID=" + entity.Bid_ID + "&bidtype=" + "1" + "')\\\" title=\\\"删除\\\">删除</a>");
                    }
                    


                    
                    
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }


    public string GetTenders(int Type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int BID = tools.CheckInt(Request["BID"]);

        if (BID>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_BidID = " + BID + "  group by Tender_SupplierID,Tender_BidID "));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_BidID in ( select distinct Bid_Enter_BidID from Bid_Enter where Bid_Enter_Type = " + Type + " )  group by Tender_SupplierID,Tender_BidID "));
        }
        if (keyword.Length>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_SupplierID", "in", "select Supplier_ID from Supplier where Supplier_CompanyName like '%" + keyword + "%'"));
        }
        if (BID>0)
        {
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_IsWin", "DESC"));
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        }
        else
        {
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_BidID", "DESC"));
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_IsWin", "DESC"));
            Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        }

       Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsShow", "=", "1"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);
        PageInfo pageinfo = MyTender.GetPageInfo(Query);

        if(entitys!=null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");

            foreach (TenderInfo entity in entitys)
            {
                jsonBuilder.Append("{\"TenderInfo.Tender_BidID\":" + entity.Tender_BidID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Tender_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Bid_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(SupplierName(entity.Tender_SupplierID)));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Tender_AllPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Tender_Addtime.ToString("yyyy-MM-dd"));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(IsWin(entity.Tender_Status,entity.Tender_IsWin));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if(Type==0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"tender_view.aspx?tenderid=" + entity.Tender_ID + "\\\" title=\\\"查看\\\" >查看</a>");

                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmlogdelete('bid_do.aspx?action=tender_edit&Tender_ID=" + entity.Tender_ID + "&Type=0')\\\" title=\\\"删除\\\">删除</a>");
                }
                else
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"auction_tender_view.aspx?tenderid=" + entity.Tender_ID + "\\\" title=\\\"查看\\\" >查看</a>");
            
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmlogdelete('bid_do.aspx?action=tender_edit&Tender_ID=" + entity.Tender_ID + "&Type=1')\\\" title=\\\"删除\\\">删除</a>");
                }
                
                jsonBuilder.Append("\",");


                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }


            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }

    }
    public string IsAudit(int IsAudit)
    {
        string name = "";

        switch(IsAudit)
        {
            case 0:
                name = "未审核";
                break;

            case 1:
                name = "已审核";
                break;

            case 2: name = "审核不通过";
                break;
            case 3: name = "冻结";
                break;

            default:
                break;
        }

        return name;
    }

    public string IsAudit(int Type, string selectname)
    {
        string select_str = "";
        select_str += "<select name=\"" + selectname + "\">";

        if (Type==0)
        {
            select_str += "<option value=\"0\" selected=\"selected\">全部</option>";
        }
        else
        {
            select_str += "<option value=\"0\" >全部</option>";
        }

        if (Type == 1)
        {
            select_str += "<option value=\"1\" selected=\"selected\">未审核</option>";
        }
        else
        {
            select_str += "<option value=\"1\">未审核</option>";
        }

        if (Type == 2)
        {
            select_str += "<option value=\"2\" selected=\"selected\">已审核</option>";
        }
        else
        {
            select_str += "<option value=\"2\">已审核</option>";
        }

        if (Type == 3)
        {
            select_str += "<option value=\"3\" selected=\"selected\" >审核未通过</option>";
        }
        else
        {
            select_str += "<option value=\"3\">审核未通过</option>";
        }
        
        
        
        
        select_str += "</select>";

        return select_str;
    }
    //报名数
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

    //报价数
    public int TenderNumber(int BID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", "  select MAX(Tender_ID) from Tender where Tender_BidID = " + BID + " group by Tender_SupplierID "));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Bid_Enter_ID", "Desc"));

        PageInfo page = MyTender.GetPageInfo(Query);

        if (page != null)
        {
            return page.RecordCount;
        }
        else
        {
            return 0;
        }
    }


    public string BidPoductList(IList<BidProductInfo> entitys,int Type)
    {
        string productlist = "";
        if(entitys!=null)
        {
            if (Type == 0)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    productlist = productlist + "<tr>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Sort+"</td>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Code+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Name+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Spec+"</td>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Brand+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Unit+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Amount+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Delivery+"</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">"+entity.Bid_Product_Remark+"</td>";
                    productlist = productlist + "</tr>";
                }
            }
            else if (Type==1)
            {
                foreach (BidProductInfo entity in entitys)
                {
                    productlist = productlist + "<tr>";

                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Code + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Name + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Spec + "</td>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Brand + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Unit + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Delivery + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Amount + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" +Public.DisplayCurrency(entity.Bid_Product_StartPrice) + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\"><img style=\"width:100px;height:100px;\" src=\"" + Application["Upload_Server_URL"] + entity.Bid_Product_Img + "\"/></td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Remark + "</td>";
                    productlist = productlist + "</tr>";
                }
            }
            else
            {
                foreach (BidProductInfo entity in entitys)
                {
                    productlist = productlist + "<tr>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Sort + "</td>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Code + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Name + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Spec + "</td>";
                    //productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Brand + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Unit + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Amount + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Delivery + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Product_Remark + "</td>";
                    productlist = productlist + "<td class=\"cell_content\" style=\"text-align:center;\"><img src=\"/images/icon_edit.gif\" alt=\"修改\"> <a href=\"bid_product_edit.aspx?productID=" + entity.Bid_Product_ID + "\" title=\\\"修改\\\">修改</a>&nbsp;&nbsp;<img src=\"/images/icon_del.gif\"  alt=\"删除\"> <a href=\"javascript:void(0);\" onclick=\"confirmdelete('bid_do.aspx?action=moveProduct&BidID="+entity.Bid_BidID+"&ProductID=" + entity.Bid_Product_ID + "')\" title=\"删除\">删除</a></td>";
                    productlist = productlist + "</tr>";
                }
            }
            
        }

        return productlist;
    }


    public void TenderProductList(int BID,IList<TenderProductInfo> entitys,int IsWin,int Type)
    {
        BidInfo bidinfo = GetBidByID(BID);
        if(bidinfo!=null)
        {
            if(Type==0)
            {
                IList<BidProductInfo> BidProducts = bidinfo.BidProducts;

                if (BidProducts != null && entitys != null)
                {
                    int i = 0;

                    for (; i < BidProducts.Count; i++)
                    {
                        Response.Write("<tr>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Sort + "</td>");
                        //Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Code + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Name + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Spec + "</td>");
                        //Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Brand + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Unit + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Amount + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Delivery + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Remark + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + Public.DisplayCurrency(entitys[i].Tender_Price) + "</td>");
                        if (IsWin == 1)
                        {
                            Response.Write("<td class=\"cell_content\" style=\"text-align:center;\" >" + entitys[i].Tender_Product_Name + "</td>");
                        }

                        
                        Response.Write("</tr>");
                    }
                }
            }
            else
            {
                IList<BidProductInfo> BidProducts = bidinfo.BidProducts;

                if (BidProducts != null && entitys != null)
                {
                    int i = 0;

                    for (; i < BidProducts.Count; i++)
                    {
                        Response.Write("<tr>");
                        //Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Code + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Name + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Spec + "</td>");
                        //Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Brand + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Unit + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Amount + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Delivery + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + BidProducts[i].Bid_Product_Remark + "</td>");
                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + Public.DisplayCurrency(BidProducts[i].Bid_Product_StartPrice) + "</td>");

                        Response.Write("<td class=\"cell_content\" style=\"text-align:center;\">" + Public.DisplayCurrency(entitys[i].Tender_Price) + "</td>");

                        Response.Write("</tr>");
                    }
                }
            }
            
        }
    }
    public string BidAttachments(IList<BidAttachmentsInfo> entitys, int Type)
    {
        string Attlist = "";
        if(entitys!=null)
        {
            foreach (BidAttachmentsInfo entity in entitys)
            {
                Attlist = Attlist + "<tr>";
                //Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Attachments_Sort + "</td>";
                Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Attachments_Name + "</td>";
                //Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Attachments_format + "</td>";
                //Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Attachments_Size + "</td>";
                Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\">" + entity.Bid_Attachments_Remarks + "</td>";
                Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\"><a href=\""+Application["Upload_Server_URL"]+entity.Bid_Attachments_Path +"\" target=\"_blank\">点击查看</a></td>";
                if(Type==1)
                {
                    Attlist = Attlist + "<td class=\"cell_content\" style=\"text-align:center;\"><img src=\"/images/icon_edit.gif\" alt=\"修改\"> <a href=\"bid_Attachments_edit.aspx?AttID=" + entity.Bid_Attachments_ID + "\" title=\\\"修改\\\">修改</a>&nbsp;&nbsp;<img src=\"/images/icon_del.gif\"  alt=\"删除\"> <a href=\"javascript:void(0);\" onclick=\"confirmdelete('bid_do.aspx?action=moveAtt&BidID=" + entity.Bid_Attachments_BidID + "&AttID=" + entity.Bid_Attachments_ID + "')\" title=\"删除\">删除</a></td>";
                }
                Attlist = Attlist + "</tr>";
            }
        }

        return Attlist;
    }


    public string SupplierName(int SupplierID)
    {
        SupplierInfo entity = MySupplier.GetSupplierByID(SupplierID);

        if(entity!=null)
        {
            return entity.Supplier_CompanyName;
        }
        else
        {
            return "--";
        }

    }

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


    //选择中标方
    public void WinBid(int Type)
    {
        int member_id = 0;
        int BidID = tools.CheckInt(Request["BidID"]);
        int TenderID = tools.CheckInt(Request["TenderID"]);

        BidInfo bidinfo = GetBidByID(BidID);
        TenderInfo tenderinfo = GetTenderByID(TenderID);
        if (bidinfo != null && tenderinfo != null)
        {
            if (bidinfo.Bid_MemberID != member_id)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_ID != tenderinfo.Tender_BidID)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_SupplierID > 0)
            {
                Public.Msg("error", "错误信息", "操作失败，已选中标方", false, "{back}");
            }
            if (bidinfo.Bid_Status != 1)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
            if (bidinfo.Bid_IsAudit != 1)
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");

            }

            bidinfo.Bid_SupplierID = tenderinfo.Tender_SupplierID;
            bidinfo.Bid_SupplierCompany = Supplier_CompanyName(tenderinfo.Tender_SupplierID);
            bidinfo.Bid_FinishTime = DateTime.Now;

            if (MyBLL.EditBid(bidinfo, Public.GetUserPrivilege()))
            {
                tenderinfo.Tender_Status = 1;
                tenderinfo.Tender_IsWin = 1;
                MyTender.EditTender(tenderinfo);

                if (Type == 0)
                {
                    NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]招标项目,未中标,返还保证金", bidinfo.Bid_Bond);
                    Public.Msg("positive", "操作成功", "操作成功", true, "/bid/tender_list.aspx?BID=" + bidinfo.Bid_ID);
                }
                else
                {
                    NotWinTender(bidinfo.Bid_ID, tenderinfo.Tender_SupplierID, "报名[" + bidinfo.Bid_Title + "]拍卖项目,未中标,返还保证金", bidinfo.Bid_Bond);
                    Public.Msg("positive", "操作成功", "操作成功", true, "/supplier/auction_tender_list.aspx?BID=" + bidinfo.Bid_ID);
                }

            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }

        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void NotWinTender(int BidID, int Supplider_ID, string Bid_Title, double Bid_Bond)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_ID", "in", " select MAX(Tender_ID) from Tender where Tender_SupplierID != " + Supplider_ID + " and Tender_IsWin = 0 and  Tender_BidID = " + BidID + " group by Tender_SupplierID,Tender_BidID "));
        Query.OrderInfos.Add(new OrderInfo("TenderInfo.Tender_ID", "DESC"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);

        if (entitys != null)
        {


            foreach (TenderInfo entity in entitys)
            {
                MySupplier.Supplier_Account_Log(entity.Tender_SupplierID, 1, Bid_Bond, Bid_Title);
                entity.Tender_Status = 1;
                entity.Tender_IsRefund = 1;
                MyTender.EditTender(entity);
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

    public string BidTitle(int BID)
    {
        BidInfo entity = GetBidByID(BID);
        if(entity!=null)
        {
            return entity.Bid_Title;
        }
        else
        {
            return "--";
        }
    }
}