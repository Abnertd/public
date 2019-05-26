<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.DAL.Product" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.MEM" %>

<script runat="server">
    private Shop myApp;
    private ITools tools;
    private IProduct Myproduct;
    private IMember Mymem;
    private Orders MyOrder;
    private Supplier MySupplier;
    private Contract myContract;
    private string Shop_Evaluate_Note, Shop_Evaluate_Site, Product_Name, Member_Nickname, Orders_SN, Supplier_CompanyName;
    private int Shop_Evaluate_ID, Shop_Evaluate_SupplierID, Shop_Evaluate_ContractID, Shop_Evaluate_ProductID, Shop_Evaluate_MemberID, Shop_Evaluate_Product, Shop_Evaluate_Service, Shop_Evaluate_Delivery, Shop_Evaluate_IsCheck, Shop_Evaluate_Recommend, Shop_Evaluate_IsGift;
    private DateTime Shop_Evaluate_Addtime;



    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("74500aee-9f7e-4939-a039-61fafe790776");

        myApp = new Shop();
        tools = ToolsFactory.CreateTools();
        Myproduct = ProductFactory.CreateProduct();
        Mymem = MemberFactory.CreateMember();
        MyOrder = new Orders();
        MySupplier = new Supplier();
        myContract = new Contract();
        Product_Name = "";
        Member_Nickname = "";
        Shop_Evaluate_ID = tools.CheckInt(Request.QueryString["Shop_Evaluate_ID"]);
        if (Shop_Evaluate_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }


        SupplierShopEvaluateInfo entity = myApp.GetSupplierShopEvaluateByID(Shop_Evaluate_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Shop_Evaluate_ID = entity.Shop_Evaluate_ID;
            Shop_Evaluate_SupplierID = entity.Shop_Evaluate_SupplierID;
            Shop_Evaluate_ContractID = entity.Shop_Evaluate_ContractID;
            Shop_Evaluate_ProductID = entity.Shop_Evaluate_Productid;
            Shop_Evaluate_MemberID = entity.Shop_Evaluate_MemberID;
            Shop_Evaluate_Product = entity.Shop_Evaluate_Product;
            Shop_Evaluate_Service = entity.Shop_Evaluate_Service;
            Shop_Evaluate_Delivery = entity.Shop_Evaluate_Delivery;
            Shop_Evaluate_Note = entity.Shop_Evaluate_Note;
            Shop_Evaluate_IsCheck = entity.Shop_Evaluate_Ischeck;
            Shop_Evaluate_Recommend = entity.Shop_Evaluate_Recommend;
            Shop_Evaluate_IsGift = entity.Shop_Evaluate_IsGift;
            Shop_Evaluate_Addtime = entity.Shop_Evaluate_Addtime;

            if (Shop_Evaluate_ProductID > 0)
            {
                ProductInfo productinfo = Myproduct.GetProductByID(Shop_Evaluate_ProductID);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
            }
            if (Shop_Evaluate_MemberID > 0)
            {

                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Shop_Evaluate_MemberID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    Member_Nickname = supplierinfo.Supplier_CompanyName;
                }
            }
            if (Shop_Evaluate_ContractID > 0)
            {
                ContractInfo orders = myContract.GetContractByID(Shop_Evaluate_ContractID);
                if (orders != null)
                {
                    Orders_SN = orders.Contract_SN;
                }
            }
            if (Shop_Evaluate_SupplierID > 0)
            {
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(Shop_Evaluate_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
                }
            }
            else
            {
                Supplier_CompanyName = "易耐产业金服";
            }
        }

        if (Product_Name == "")
        {
            Product_Name = "未知";
        }
        if (Member_Nickname == "")
        {
            Member_Nickname = "游客或未知";
        }
        if (Orders_SN == "")
        {
            Orders_SN = "未知";
        }
        if (Supplier_CompanyName == "")
        {
            Supplier_CompanyName = "未知";
        }

    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>店铺商家评价查看</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <form id="formadd" name="formadd" method="post" action="shop_evaluate_do.aspx">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <%if (Shop_Evaluate_ProductID > 0)
                  {   %>
                <tr>
                    <td class="content_title">店铺商品评价查看</td>
                </tr>
                <%}
                  else
                  { %>
                <tr>
                    <td class="content_title">商家评价查看</td>
                </tr>

                <%} %>



                <tr>
                    <td class="content_content">

                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                             <tr>
                                <td class="cell_title">供应商</td>
                                <td class="cell_content"><%=Supplier_CompanyName%></td>
                            </tr>

                            <tr>
                                <td class="cell_title">评论人</td>
                                <td class="cell_content"><%=Member_Nickname %> [<%=Shop_Evaluate_Addtime%>]</td>
                            </tr>
                            <%-- <tr>
          <td class="cell_title">合同编号</td>
          <td class="cell_content"><%=Orders_SN%></td>
        </tr>--%>
                          

                            <tr>
                                <td class="cell_title">评论信息</td>
                                <td class="cell_content">
                                   <%-- <b>产品评分：</b><input name="Shop_Evaluate_Product" type="text" id="Shop_Evaluate_Product" size="10" maxlength="10" value="<% =Shop_Evaluate_Product%>" />--%>
                                    <b>服务评分：</b>
                                    <input name="Shop_Evaluate_Service" type="text" id="Shop_Evaluate_Service" size="10" maxlength="10" value="<% =Shop_Evaluate_Service%>" />
                                    <b>发货评分：</b><input name="Shop_Evaluate_Delivery" type="text" id="Shop_Evaluate_Delivery" size="10" maxlength="10" value="<% =Shop_Evaluate_Delivery%>" />
                                    <%-- <b>审核状态：</b>
          <%
              
              if (Shop_Evaluate_IsCheck == 1)
              {
                  Response.Write("已审核");
              }
              else
              {
                Response.Write("未审核");
              }
          
              %>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">评价说明 </td>
                                <td class="cell_content">
                                    <input name="Shop_Evaluate_Note" type="text" id="Shop_Evaluate_Note" size="70" maxlength="100" value="<% =Shop_Evaluate_Note%>" />
                                </td>
                            </tr>

                        </table>
                        <%--  <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            
             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='shop_evaluate_list.aspx'"/></td>
          </tr>
        </table>--%>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="rewsupplierevaluate" />
                                    <input type="hidden" id="Shop_Evaluate_ID" name="Shop_Evaluate_ID" value="<% =Shop_Evaluate_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <%--<input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'about_list.aspx';" /></td>--%>
                                   <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'shop_evaluate_list.aspx'"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
