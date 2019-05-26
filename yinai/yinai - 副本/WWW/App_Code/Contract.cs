using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Sys;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.BLL.ORD;
/// <summary>
/// Contract 的摘要说明
/// </summary>
public class Contract
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;
    private Addr addr;
    Orders Myorder = new Orders();
    private ToUcase MyUcase = new ToUcase();
    IContract MyContract;
    IContractTemplate MyContractTemplate;
    IOrdersLog MyOrdersLog;


    ISupplier MySupplier;
    // Member mymember = new Member();
    //IContractSigned MyContractSigned;

    ITools tools;
    IAddr iaddr;
    Credit credit;
    IOrders MyOrders;


    Public_Class pub = new Public_Class();
    public Contract()
    {
        iaddr = AddrFactory.CreateAddr();
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        tools = ToolsFactory.CreateTools();
        addr = new Addr();
        MyContract = ContractFactory.CreateContract();
        MyContractTemplate = ContractTemplateFactory.CreateContractTemplate();
        MySupplier = SupplierFactory.CreateSupplier();
        credit = new Credit();
        MyOrdersLog = OrdersLogFactory.CreateOrdersLog();
        MyOrders = OrdersFactory.CreateOrders();


        //MyContractSigned = ContractSignedFactory.CreateContractSigned();
    }








    //合同查看打印
    public void Contract_View_New(OrdersInfo ordersInfo, int contact_no)
    {
        string Template_Html = "";
        int Contract_ID;
        string Time = string.Empty;

        string Contract_Product_Goods = "";
        string ContractTemplate_TopFuJian = "";
        string Sell_Contract_Template_OnlyRead = "";
        string Supplier_CompanyName = "";
        string Member_NickName = "";
        string ContractTemResponsible = string.Empty;

        SupplierInfo supplierInfo = null;
        SupplierInfo Supplierinfo = null;

        string Sell_Contract_Template_EndFuJianContent = "";

        Contract_ID = tools.CheckInt(Request["Contract_ID"]);
        Contract_Product_Goods = new Contract().Contract_Orders_Goods_Print(Contract_ID);
        SupplierInfo MemberSupplierinfo = null;
        string MemberSupplierCompanyName = "";
        MemberInfo memberInfo = null;
        if (ordersInfo != null)
        {
            memberInfo = new Member().GetMemberByID(ordersInfo.Orders_BuyerID);
            Supplierinfo = new Supplier().GetSupplierByID(memberInfo.Member_SupplierID);

            if (memberInfo != null)
            {
                //Member_NickName = memberInfo.Member_NickName;
                MemberSupplierinfo = new Supplier().GetSupplierByID(memberInfo.Member_SupplierID);
                if (MemberSupplierinfo != null)
                {
                    MemberSupplierCompanyName = MemberSupplierinfo.Supplier_CompanyName;
                }
            }
        }
        List<OrdersLogInfo> logs = MyOrdersLog.GetOrdersLogsByOrdersID(ordersInfo.Orders_ID).Where(p => p.Orders_Log_Remark == "供应商确认订单").ToList();

        if (ordersInfo != null)
        {
            supplierInfo = MySupplier.GetSupplierByID(ordersInfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                Supplier_CompanyName = supplierInfo.Supplier_CompanyName;
            }
        }
        //合同顶部附件  
        ContractTemplateInfo ContractTemplateEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_TopFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));

        if (logs.Count > 0)
        {
            Time = tools.NullStr(logs.FirstOrDefault().Orders_Log_Addtime.ToString("yyyy年MM月dd日"));
        }

        //合同模板运输责任
        ContractTemplateInfo ContractTemResponsibleEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_Responsible", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemResponsibleEntity != null)
        {
            ContractTemResponsible = ContractTemResponsibleEntity.Contract_Template_Content;
            if (ordersInfo != null)
            {
                ContractTemResponsible = ContractTemResponsible.Replace("{Responsible}", ordersInfo.Orders_Responsible == 1 ? "卖家责任" : "买家责任");
            }
        }


        if (ContractTemplateEntity != null)
        {
            ContractTemplate_TopFuJian = ContractTemplateEntity.Contract_Template_Content;
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{supplier_name}", Supplier_CompanyName);
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{member_name}", MemberSupplierCompanyName);
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{time}", string.IsNullOrEmpty(Time) ? "卖家尚未确认订单" : Time);

            //ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{orders_goodslist}", Contract_Product_Goods);
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{orders_goodslist}", new Member().GetOrdersGoods(ordersInfo));
        }


        //合同尾部附件  
        ContractTemplateInfo Sell_Contract_Template_EndFuJianEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_EndFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (Sell_Contract_Template_EndFuJianEntity != null)
        {
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianEntity.Contract_Template_Content;

            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_name}", supplierInfo.Supplier_CompanyName);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_adress}", supplierInfo.Supplier_Address);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_corproate}", supplierInfo.Supplier_Corporate);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_name}", Supplierinfo.Supplier_CompanyName);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_adress}", Supplierinfo.Supplier_Address);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_corproate}", Supplierinfo.Supplier_Corporate);
        }




        //易耐网交易合同标准条款 
        ContractTemplateInfo Sell_Contract_Template_OnlyReadEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_OnlyRead", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (Sell_Contract_Template_OnlyReadEntity != null)
        {
            Sell_Contract_Template_OnlyRead = Sell_Contract_Template_OnlyReadEntity.Contract_Template_Content;

        }







        if (Contract_ID == 0)
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
        ContractInfo entity = GetContractByID(Contract_ID);
        if (entity != null)
        {
            if (Request["action"] == "print")
            {
                Response.Write("<script>window.print();</script>");
            }

            string address = "";// /**/
            ContractInfo ContractInfo = null;



            if (ordersInfo != null)
            {

                if (MySupplier != null)
                {
                    ContractInfo = MyContract.GetContractByID(contact_no, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
                    if (ContractInfo != null)
                    {
                        Template_Html = ContractInfo.Contract_Note;

                        Template_Html = Template_Html.Replace("{supplier_name}", Supplier_CompanyName);
                        Template_Html = Template_Html.Replace("{member_name}", MemberSupplierCompanyName);
                        Template_Html = Template_Html.Replace("{orders_address}", addr.DisplayAddress(ordersInfo.Orders_Address_State, ordersInfo.Orders_Address_City, ordersInfo.Orders_Address_County) + ordersInfo.Orders_Address_StreetAddress);



                        Template_Html = Template_Html.Replace("{supplier_sealimg}", pub.FormatImgURL(supplierInfo.Supplier_SealImg, "fullpath"));


                        Template_Html = Template_Html.Replace("{orders_paywayname}", ordersInfo.Orders_Payway_Name);
                        Template_Html = Template_Html.Replace("{orders_deliveryname}", ordersInfo.Orders_Delivery_Name);



                        Template_Html = Template_Html.Replace("{orders_goodslist}", new Member().GetOrdersGoods(ordersInfo));


                        if (memberInfo != null)
                        {
                            Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                            Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                        }
                    }
                }
                Template_Html = ContractTemplate_TopFuJian +  Template_Html + ContractTemResponsible + ordersInfo.Orders_ContractAdd + Sell_Contract_Template_OnlyRead + Sell_Contract_Template_EndFuJianContent; ;
            }

            Response.Write(Server.HtmlDecode(Template_Html));

        }
        else
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
    }

    //商家查看打印
    public void Contract_View_SupplierNew(OrdersInfo ordersInfo, int contact_no)
    {
        string Template_Html = "";
        int Contract_ID;
        string Contract_Product_Goods = "";
        string ContractTemplate_TopFuJian = "";
        string Sell_Contract_Template_OnlyRead = "";
        string Supplier_CompanyName = "";
        string Member_NickName = "";
        string ContractTemResponsible = string.Empty;
        string Time = string.Empty;

        SupplierInfo supplierInfo = null;
        string Sell_Contract_Template_EndFuJianContent = "";
        MemberInfo memberInfo = new Member().GetMemberByID(ordersInfo.Orders_BuyerID);
        Contract_ID = tools.CheckInt(Request["Contract_ID"]);
        Contract_Product_Goods = new Contract().Contract_Orders_Goods_Print(Contract_ID);
        SupplierInfo MemberSupplierinfo = null;

        string MemberSupplierCompanyName = "";
        if (memberInfo != null)
        {
            //Member_NickName = memberInfo.Member_NickName;
            MemberSupplierinfo = new Supplier().GetSupplierByID(memberInfo.Member_SupplierID);
            if (MemberSupplierinfo != null)
            {
                MemberSupplierCompanyName = MemberSupplierinfo.Supplier_CompanyName;
            }

        }

        if (ordersInfo != null)
        {
            supplierInfo = MySupplier.GetSupplierByID(ordersInfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                Supplier_CompanyName = supplierInfo.Supplier_CompanyName;
            }
        }
        List<OrdersLogInfo> logs = MyOrdersLog.GetOrdersLogsByOrdersID(ordersInfo.Orders_ID).Where(p => p.Orders_Log_Remark == "供应商确认订单").ToList();
        if (logs.Count > 0)
        {
            Time = tools.NullStr(logs.FirstOrDefault().Orders_Log_Addtime.ToString("yyyy年MM月dd日"));
        }
        //合同顶部附件  
        ContractTemplateInfo ContractTemplateEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_TopFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemplateEntity != null)
        {
            ContractTemplate_TopFuJian = ContractTemplateEntity.Contract_Template_Content;
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{supplier_name}", Supplier_CompanyName);
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{member_name}", MemberSupplierCompanyName);
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{orders_goodslist}", new Member().GetOrdersGoods(ordersInfo));
            ContractTemplate_TopFuJian = ContractTemplate_TopFuJian.Replace("{time}", string.IsNullOrEmpty(Time) ? "卖家尚未确认订单" : Time);

        }

        //合同模板运输责任
        ContractTemplateInfo ContractTemResponsibleEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_Responsible", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemResponsibleEntity != null)
        {
            ContractTemResponsible = ContractTemResponsibleEntity.Contract_Template_Content;
            if (ordersInfo != null)
            {
                ContractTemResponsible = ContractTemResponsible.Replace("{Responsible}", ordersInfo.Orders_Responsible == 1 ? "卖家责任" : "买家责任");
            }
        }


        //合同尾部附件  
        ContractTemplateInfo Sell_Contract_Template_EndFuJianEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_EndFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (Sell_Contract_Template_EndFuJianEntity != null)
        {
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianEntity.Contract_Template_Content;



            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_name}", supplierInfo.Supplier_CompanyName);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_adress}", supplierInfo.Supplier_Address);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{member_corproate}", supplierInfo.Supplier_Corporate);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_name}", MemberSupplierinfo.Supplier_CompanyName);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_adress}", MemberSupplierinfo.Supplier_Address);
            Sell_Contract_Template_EndFuJianContent = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_corproate}", MemberSupplierinfo.Supplier_Corporate);
        }




        //易耐网交易合同标准条款 
        ContractTemplateInfo Sell_Contract_Template_OnlyReadEntity = MyContractTemplate.GetContractTemplateBySign("Sell_Contract_Template_OnlyRead", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (Sell_Contract_Template_OnlyReadEntity != null)
        {
            Sell_Contract_Template_OnlyRead = Sell_Contract_Template_OnlyReadEntity.Contract_Template_Content;
            //Sell_Contract_Template_OnlyRead = Sell_Contract_Template_EndFuJianContent.Replace("{supplier_name}", Supplier_CompanyName);
            //Sell_Contract_Template_OnlyRead = Sell_Contract_Template_EndFuJianContent.Replace("{member_name}", MemberSupplierCompanyName);
        }







        if (Contract_ID == 0)
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
        ContractInfo entity = GetContractByID(Contract_ID);
        if (entity != null)
        {
            if (Request["action"] == "print")
            {
                Response.Write("<script>window.print();</script>");
            }

            string address = "";// /**/
            ContractInfo ContractInfo = null;
            //MemberInfo memberInfo = mymember.GetMemberByID();


            if (ordersInfo != null)
            {

                if (MySupplier != null)
                {
                    ContractInfo = MyContract.GetContractByID(contact_no, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
                    if (ContractInfo != null)
                    {



                        Template_Html = ContractInfo.Contract_Note;

                        Template_Html = Template_Html.Replace("{supplier_name}", Supplier_CompanyName);
                        Template_Html = Template_Html.Replace("{member_name}", MemberSupplierCompanyName);
                        Template_Html = Template_Html.Replace("{orders_address}", addr.DisplayAddress(ordersInfo.Orders_Address_State, ordersInfo.Orders_Address_City, ordersInfo.Orders_Address_County) + ordersInfo.Orders_Address_StreetAddress);



                        Template_Html = Template_Html.Replace("{supplier_sealimg}", pub.FormatImgURL(supplierInfo.Supplier_SealImg, "fullpath"));


                        Template_Html = Template_Html.Replace("{orders_paywayname}", ordersInfo.Orders_Payway_Name);
                        Template_Html = Template_Html.Replace("{orders_deliveryname}", ordersInfo.Orders_Delivery_Name);



                        Template_Html = Template_Html.Replace("{orders_goodslist}", new Member().GetOrdersGoods(ordersInfo));


                        if (memberInfo != null)
                        {
                            Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                            Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                        }
                    }
                }
                Template_Html = ContractTemplate_TopFuJian +  Template_Html + ContractTemResponsible + ordersInfo.Orders_ContractAdd + Sell_Contract_Template_OnlyRead + Sell_Contract_Template_EndFuJianContent; ;
            }

            Response.Write(Server.HtmlDecode(Template_Html));

        }
        else
        {
            Response.Write("<script>alert('合同无效!');windwo.close();</script>");
            Response.End();
        }
    }

    //获取合同附加订单(打印)
    public string GetPrintContractOrdersByContractsID(int Contract_ID)
    {
        string strHTML = "";
        int i = 0;
        strHTML += "<table border=\"0\"  width=\"100%\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
        strHTML += "<table border=\"0\"  width=\"608\" align=\"right\" class=\"list_tab\" style=\"border:1px solid #000000;\" cellpadding=\"0\" cellspacing=\"0\">";
        strHTML += "    <tr bgcolor=\"#ffffff\">";
        strHTML += "        <td align=\"center\" style=\"height:25px;\" width=\"50\">序号</td>";
        strHTML += "        <td align=\"center\">订单编号</td>";
        strHTML += "        <td align=\"center\">商品种类</td>";
        strHTML += "        <td align=\"center\">数量</td>";
        strHTML += "        <td align=\"center\">订单金额</td>";
        strHTML += "        <td align=\"center\">下单时间</td>";
        strHTML += "    </tr>";
        int Goods_Amount = 0;
        double Goods_Sum = 0;
        IList<OrdersGoodsInfo> GoodsListAll = null;
        ContractInfo Contract = GetContractByID(Contract_ID);
        if (Contract != null)
        {
            IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
            if (ordersinfos != null)
            {
                foreach (OrdersInfo ordersinfo in ordersinfos)
                {
                    Goods_Sum = 0;
                    Goods_Amount = 0;
                    i = i + 1;
                    GoodsListAll = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
                    if (GoodsListAll != null)
                    {
                        foreach (OrdersGoodsInfo good in GoodsListAll)
                        {
                            if ((good.Orders_Goods_ParentID == 0 && good.Orders_Goods_Type != 2) || good.Orders_Goods_ParentID > 0)
                            {
                                Goods_Amount = Goods_Amount + 1;
                                Goods_Sum = Goods_Sum + good.Orders_Goods_Amount;
                            }
                        }

                    }

                    strHTML += "    <tr bgcolor=\"#ffffff\">";
                    strHTML += "        <td align=\"left\">" + i + "</td>";
                    strHTML += "        <td align=\"left\">" + ordersinfo.Orders_SN + "</td>";
                    strHTML += "        <td align=\"left\">" + Goods_Amount + "</td>";
                    strHTML += "        <td align=\"left\">" + Goods_Sum + "</td>";
                    strHTML += "        <td align=\"left\">" + Public_Class.DisplayCurrency(ordersinfo.Orders_Total_AllPrice) + "</td>";
                    strHTML += "        <td align=\"left\">" + ordersinfo.Orders_Addtime + "</td>";
                    strHTML += "    </tr>";
                }
            }
        }

        strHTML += "    </table></td></tr></table>";
        return strHTML;
    }
    //合同订单所有产品(打印)
    public string Contract_Orders_Goods_Print(int Contract_ID)
    {
        string strHTML = "";
        IList<OrdersGoodsInfo> GoodsListAll = null;
        int freighted_amount;
        int icount = 0;

        ContractInfo contractinfo = GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            strHTML += "<table width=\"635\" border=\"0\" align=\"center\" class=\"list_tab\" style=\"border:1px solid #000000;page-break-before:always;\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#000000\">";
            strHTML += "<tr bgcolor=\"#ffffff\">";

            strHTML += "<td align=\"center\" style=\"height:25px;\" width=\"50\">序号</td>";
            strHTML += "<td align=\"center\">产品名称</td>";
            //strHTML += "<td align=\"center\">订单号</td>";
            strHTML += "<td align=\"center\">规格型号</td>";
            strHTML += "<td align=\"center\">单位</td>";
            strHTML += "<td align=\"center\">单价(元)</td>";

            if (contractinfo.Contract_Status == 1)
            {
                strHTML += "<td align=\"center\">规格</td>";
                strHTML += "<td align=\"center\">单价</td>";
                strHTML += "<td align=\"center\">数量</td>";
                strHTML += "<td align=\"center\">金额</td>";
            }
            else
            {
                strHTML += "<td align=\"center\">数量</td>";
            }
            //strHTML += "<td align=\"center\">总价(元)</td>";
            strHTML += "</tr>";
            IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
            if (ordersinfos != null)
            {
                foreach (OrdersInfo ordersinfo in ordersinfos)
                {
                    GoodsListAll = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
                    if (GoodsListAll != null)
                    {
                        foreach (OrdersGoodsInfo entity in GoodsListAll)
                        {
                            string Product_Spec = "";
                            string Product_Unit = "";
                            ProductInfo productEntity = new Product().GetProductByID(entity.Orders_Goods_Product_ID);
                            if (productEntity != null)
                            {
                                Product_Spec = productEntity.Product_Spec;
                                Product_Unit = productEntity.Product_Unit;
                                if (Product_Unit == "0")
                                {
                                    Product_Unit = "";
                                }
                            }

                            if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                            {
                                freighted_amount = 0;
                            }
                            else
                            {
                                icount = icount + 1;
                                strHTML += "<tr bgcolor=\"#ffffff\">";
                                strHTML += "<td align=\"center\">" + icount + "</td>";
                                strHTML += "<td align=\"center\">" + entity.Orders_Goods_Product_Name + "</td>";

                                //strHTML += "<td align=\"center\">" + ordersinfo.Orders_SN + "</td>";
                                strHTML += "<td align=\"center\">" + Product_Spec + "</td>";
                                strHTML += "<td align=\"center\">" + Product_Unit + "</td>";
                                strHTML += "<td align=\"center\">" + entity.Orders_Goods_Product_Price + "</td>";

                                if (contractinfo.Contract_Status == 1)
                                {
                                    strHTML += "<td align=\"center\">" + entity.Orders_Goods_Product_Spec + "&nbsp;</td>";
                                    strHTML += "<td align=\"center\">" + Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
                                    strHTML += "<td align=\"center\">" + (entity.Orders_Goods_Amount) + "</td>";
                                    strHTML += "<td align=\"center\">" + (Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount)) + "</td>";
                                }
                                else
                                {
                                    strHTML += "<td align=\"center\">" + (entity.Orders_Goods_Amount) + "</td>";
                                }
                                //strHTML += "<td align=\"center\">" + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>";
                                strHTML += "</tr>";
                            }
                        }
                    }
                }
            }
        }
        strHTML += "</table>";
        return strHTML;
    }
    ////合同订单所有产品(打印)
    //public string Contract_Orders_Goods_Print(int Contract_ID)
    //{
    //    string strHTML = "";
    //    IList<OrdersGoodsInfo> GoodsListAll = null;
    //    int freighted_amount;
    //    int icount = 0;

    //    ContractInfo contractinfo = GetContractByID(Contract_ID);
    //    if (contractinfo != null)
    //    {
    //        strHTML += "<table width=\"635\" border=\"0\" align=\"center\" class=\"list_tab\" style=\"border:1px solid #000000;page-break-before:always;\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#000000\">";
    //        strHTML += "<tr bgcolor=\"#ffffff\">";
    //        strHTML += "<td align=\"center\" style=\"height:25px;\" width=\"50\">序号</td>";
    //        strHTML += "<td align=\"center\">订单号</td>";
    //        strHTML += "<td align=\"center\">订货号</td>";
    //        strHTML += "<td align=\"center\">产品名称</td>";
    //        if (contractinfo.Contract_Status == 1)
    //        {
    //            strHTML += "<td align=\"center\">规格</td>";
    //            strHTML += "<td align=\"center\">单价</td>";
    //            strHTML += "<td align=\"center\">数量</td>";
    //            strHTML += "<td align=\"center\">金额</td>";
    //        }
    //        else
    //        {
    //            strHTML += "<td align=\"center\">数量</td>";
    //        }
    //        strHTML += "</tr>";
    //        IList<OrdersInfo> ordersinfos = MyOrders.GetOrderssByContractID(Contract_ID);
    //        if (ordersinfos != null)
    //        {
    //            foreach (OrdersInfo ordersinfo in ordersinfos)
    //            {
    //                GoodsListAll = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
    //                if (GoodsListAll != null)
    //                {
    //                    foreach (OrdersGoodsInfo entity in GoodsListAll)
    //                    {
    //                        if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
    //                        {
    //                            freighted_amount = 0;
    //                        }
    //                        else
    //                        {
    //                            icount = icount + 1;
    //                            strHTML += "<tr bgcolor=\"#ffffff\">";
    //                            strHTML += "<td align=\"left\">" + icount + "</td>";
    //                            strHTML += "<td align=\"left\">" + ordersinfo.Orders_SN + "</td>";
    //                            strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Code + "</td>";
    //                            strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Name + "</td>";
    //                            if (contractinfo.Contract_Status == 1)
    //                            {
    //                                strHTML += "<td align=\"left\">" + entity.Orders_Goods_Product_Spec + "&nbsp;</td>";
    //                                strHTML += "<td align=\"left\">" + Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price) + "</td>";
    //                                strHTML += "<td align=\"left\">" + (entity.Orders_Goods_Amount) + "</td>";
    //                                strHTML += "<td align=\"left\">" + (Public_Class.DisplayCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount)) + "</td>";
    //                            }
    //                            else
    //                            {
    //                                strHTML += "<td align=\"left\">" + (entity.Orders_Goods_Amount) + "</td>";
    //                            }
    //                            strHTML += "</tr>";
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    strHTML += "</table>";
    //    return strHTML;
    //}

    //根据合同编号获取合同信息
    public ContractInfo GetContractByID(int ID)
    {
        return MyContract.GetContractByID(ID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
    }

    //根据编号获取合同当前状态
    public ContractInfo GetContractState(int Contract_ID)
    {
        //int enterprise_id = tools.CheckInt(Session["enterprise_id"].ToString());
        //if (enterprise_id > 0)
        //{
        QueryInfo Query = new QueryInfo();
        Query.CurrentPage = 1;
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_ID", "=", Contract_ID.ToString()));
        IList<ContractInfo> list = MyContract.GetContracts(Query, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        if (list != null)
        {
            return list[0];
        }
        //    return null;
        //}
        else
        {
            return null;
        }
    }


    //根据编号获取合同当前状态
    public ContractTemplateInfo GetContractTemplateState(int ContractTemplate_ID)
    {

        QueryInfo Query = new QueryInfo();
        //Query.CurrentPage = 1;
        //Query.PageSize = 0;
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ContractInfo.Contract_ID", "=", ContractTemplate_ID.ToString()));
        ContractTemplateInfo list = MyContractTemplate.GetContractTemplateByID(ContractTemplate_ID, pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (list != null)
        {
            return list;
        }

        else
        {
            return null;
        }
    }


    public string GetContractContent_Old(ContractInfo ci)//得到合同的内容模板
    {
        string srcString = "";
        if (ci == null)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Host.ToString() + "/contract_content/contract_content.html";
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(url);
            httprequest.CookieContainer = cookieContainer;
            httprequest.ContentType = "text/html";
            httprequest.Method = "GET";
            httprequest.KeepAlive = false;

            // 得到Token（backUrl）
            HttpWebResponse httpresponse;
            try
            {
                httpresponse = (HttpWebResponse)httprequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpresponse = (HttpWebResponse)ex.Response;
            }
            System.IO.Stream responseStream = httpresponse.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
            srcString = reader.ReadToEnd();
        }
        else
        {
            //string filePath = Server.MapPath("/contract_content/contract_content.html");//路径
            //srcString = System.IO.File.ReadAllText(filePath);
            ContractInfo ContractInfo = MyContract.GetContractByID(1, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
            if (ContractInfo != null)
            {
                srcString = ContractInfo.Contract_Template;
            }
        }

        //Enterprise e = new Enterprise();
        //EnterpriseInfo enterpriseinfo = e.GetEnterpriseByID();
        string s = "";
        //if (enterpriseinfo != null)
        //{

        //    QueryInfo qi = new QueryInfo();
        //    qi.CurrentPage = 1;
        //    qi.PageSize = 0;
        //    string Contract_CompanyName = "";
        //    string Contract_CompanyAbbreviation = "";
        //    string State_CN = "";
        //    int Contract_ShopNum = 0;
        //    string Contract_CEO = "";
        //    string Contract_TrainCEO = "";
        //    string Contract_ShopType = "";
        //    string Contract_Address = "";
        //    string Contract_Tel = "";
        //    if (ci != null)
        //    {
        //        State_CN = iaddr.GetStateInfoByCode(ci.Contract_State).State_CN;
        //        Contract_Address = ci.Contract_Address;
        //        Contract_Tel = ci.Contract_Tel;
        //        Contract_CompanyName = ci.Contract_CompanyName;
        //        Contract_CompanyAbbreviation = ci.Contract_CompanyAbbreviation;
        //        Contract_ShopNum = ci.Contract_ShopNum;
        //        Contract_CEO = ci.Contract_CEO;
        //        Contract_TrainCEO = ci.Contract_TrainCEO;
        //        Contract_ShopType = ci.Contract_ShopType;
        //        Contract_Tel = ci.Contract_Tel;
        //    }
        //    else
        //    {
        //        State_CN = iaddr.GetStateInfoByCode(enterpriseinfo.Enterprise_State).State_CN;
        //        Contract_Address = enterpriseinfo.Enterprise_Stressaddress;
        //        Contract_CompanyName = enterpriseinfo.Enterprise_Name;
        //        Contract_Tel = enterpriseinfo.Enterprise_PhoneNumber;
        //    }
        //    s = srcString
        //        .Replace("{Enterprise_Name}", Contract_CompanyName)
        //        .Replace("{Enterprise_NickName}", Contract_CompanyAbbreviation)
        //        .Replace("{Enterprise_State}", State_CN)
        //        .Replace("{sdate}", DateTime.Now.ToString("yyyy-MM-dd"))
        //        .Replace("{edate}", DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"))
        //        .Replace("{Contract_CEO}", Contract_CEO)
        //        .Replace("{Contract_TrainCEO}", Contract_TrainCEO)
        //        .Replace("{Contract_ShopType}", Contract_ShopType)
        //        .Replace("{Contract_ShopNum}", Contract_ShopNum.ToString())
        //        .Replace("{address}", Contract_Address)
        //        .Replace("{phone}", Contract_Tel);

        //    return s;
        //}
        //else
        //{
        //    return srcString;
        //}
        return srcString;
    }


    public string GetContractContent(ContractInfo ci)//得到合同的内容模板
    {
        string srcString = "";
        if (ci == null)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Host.ToString() + "/contract_content/contract_content.html";
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(url);
            httprequest.CookieContainer = cookieContainer;
            httprequest.ContentType = "text/html";
            httprequest.Method = "GET";
            httprequest.KeepAlive = false;

            // 得到Token（backUrl）
            HttpWebResponse httpresponse;
            try
            {
                httpresponse = (HttpWebResponse)httprequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpresponse = (HttpWebResponse)ex.Response;
            }
            System.IO.Stream responseStream = httpresponse.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.UTF8);
            srcString = reader.ReadToEnd();
        }
        else
        {
            //string filePath = Server.MapPath("/contract_content/contract_content.html");//路径
            //srcString = System.IO.File.ReadAllText(filePath);
            ContractInfo ContractInfo = MyContract.GetContractByID(1, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
            if (ContractInfo != null)
            {
                srcString = ContractInfo.Contract_Template;
            }
        }

        //Enterprise e = new Enterprise();
        //EnterpriseInfo enterpriseinfo = e.GetEnterpriseByID();
        string s = "";
        //if (enterpriseinfo != null)
        //{

        //    QueryInfo qi = new QueryInfo();
        //    qi.CurrentPage = 1;
        //    qi.PageSize = 0;
        //    string Contract_CompanyName = "";
        //    string Contract_CompanyAbbreviation = "";
        //    string State_CN = "";
        //    int Contract_ShopNum = 0;
        //    string Contract_CEO = "";
        //    string Contract_TrainCEO = "";
        //    string Contract_ShopType = "";
        //    string Contract_Address = "";
        //    string Contract_Tel = "";
        //    if (ci != null)
        //    {
        //        State_CN = iaddr.GetStateInfoByCode(ci.Contract_State).State_CN;
        //        Contract_Address = ci.Contract_Address;
        //        Contract_Tel = ci.Contract_Tel;
        //        Contract_CompanyName = ci.Contract_CompanyName;
        //        Contract_CompanyAbbreviation = ci.Contract_CompanyAbbreviation;
        //        Contract_ShopNum = ci.Contract_ShopNum;
        //        Contract_CEO = ci.Contract_CEO;
        //        Contract_TrainCEO = ci.Contract_TrainCEO;
        //        Contract_ShopType = ci.Contract_ShopType;
        //        Contract_Tel = ci.Contract_Tel;
        //    }
        //    else
        //    {
        //        State_CN = iaddr.GetStateInfoByCode(enterpriseinfo.Enterprise_State).State_CN;
        //        Contract_Address = enterpriseinfo.Enterprise_Stressaddress;
        //        Contract_CompanyName = enterpriseinfo.Enterprise_Name;
        //        Contract_Tel = enterpriseinfo.Enterprise_PhoneNumber;
        //    }
        //    s = srcString
        //        .Replace("{Enterprise_Name}", Contract_CompanyName)
        //        .Replace("{Enterprise_NickName}", Contract_CompanyAbbreviation)
        //        .Replace("{Enterprise_State}", State_CN)
        //        .Replace("{sdate}", DateTime.Now.ToString("yyyy-MM-dd"))
        //        .Replace("{edate}", DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"))
        //        .Replace("{Contract_CEO}", Contract_CEO)
        //        .Replace("{Contract_TrainCEO}", Contract_TrainCEO)
        //        .Replace("{Contract_ShopType}", Contract_ShopType)
        //        .Replace("{Contract_ShopNum}", Contract_ShopNum.ToString())
        //        .Replace("{address}", Contract_Address)
        //        .Replace("{phone}", Contract_Tel);

        //    return s;
        //}
        //else
        //{
        //    return srcString;
        //}
        return srcString;
    }






    public string GetContractTemplateContent(ContractInfo ci, string orders_sn)//得到合同的内容模板
    {

        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        SupplierInfo supplierInfo = null;
        string supplierName = "";
        string member_name = "";

        MemberInfo memberinfo = new Member().GetMemberByID();
        if (memberinfo != null)
        {
            member_name = memberinfo.Member_NickName;

        }


        if (entity != null)
        {
            QueryLoanProjectJsonInfo JsonInfo = null;

            if (entity.Orders_Loan_proj_no != "")
            {
                JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
            }
            else
            {
                JsonInfo = new QueryLoanProjectJsonInfo();
            }

            supplierInfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                supplierName = supplierInfo.Supplier_CompanyName;
            }
            else
            {
                supplierName = "--";
            }


        }












        string srcString = "";
        if (entity.Orders_ContractID < 1)
        {

        }
        else
        {
            //ContractTemplateInfo ContractInfo = MyContractTemplate.GetContractTemplateByID(1, pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
            if (entity != null)
            {
                ContractInfo ContractInfo = MyContract.GetContractByID(entity.Orders_ContractID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
                if (ContractInfo != null)
                {
                    srcString = ContractInfo.Contract_Template;
                }
            }

        }


        string s = "";

        if (srcString != null)
        {

            QueryInfo qi = new QueryInfo();
            qi.CurrentPage = 1;
            qi.PageSize = 0;
            string Contract_CompanyName = "";
            string Contract_CompanyAbbreviation = "";
            string State_CN = "";
            int Contract_ShopNum = 0;
            string Contract_CEO = "";
            string Contract_TrainCEO = "";
            string Contract_ShopType = "";
            string Contract_Address = "";
            string Contract_Tel = "";
            if (ci != null)
            {

            }
            else
            {

            }

            return srcString;
            //return s;
        }
        else
        {
            return srcString;
        }
        return srcString;
    }

    public void edit_contract(int contract_id, string orders_sn)
    {
        ContractInfo contractinfo = MyContract.GetContractByID(contract_id, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        string Contract_Note = tools.CheckStr(Request["Contract_Note"]);
        contractinfo.Contract_Note = Contract_Note;
        contractinfo.Contract_Confirm_Status = 1;



        if (MyContract.EditContract(contractinfo, pub.CreateUserPrivilege("cd2be0f8-b35a-48ad-908b-b5165c0a1581")))
        {

            pub.Msg("positive", "操作成功", "操作成功！", false, "/member/Order_Contract.aspx?orders_sn=" + orders_sn);
        }
        else
        {
            pub.Msg("error", "操作失败", "操作失败！", false, "{back}");
        }
    }
}


