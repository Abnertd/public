<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    //静态化配置
    PageURL pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    Product product = new Product();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Supplier supplier = new Supplier();
    Addr addr = new Addr();
    //AD ad = new AD(); 
    string Purchase_Title = ""; 
    int apply_id = tools.CheckInt(Request["apply_id"]);
    if (apply_id == 0)
    {
        Response.Redirect("/Purchase/index.aspx");
    }
    string address = "";
    string cateName = " -- ";
    string purState = "接受报价中";
    string purDay = "距离截止时间：报价结束";
    bool priceReportStatus = false;
    int supplier_id = tools.NullInt(Session["supplier_id"]);

    SupplierPurchaseInfo entity = supplier.GetSupplierPurchaseByID(apply_id);
    if (entity != null)
    {
        if (entity.Purchase_Status != 2 || entity.Purchase_IsActive != 1 || entity.Purchase_Trash != 0)
        {
            Response.Redirect("/Purchase/index.aspx");
        }
        //if (entity.Purchase_SupplierID == supplier_id && supplier_id > 0)
        //{
        //    Response.Redirect("/purchase/index.aspx");
        //}
        if (entity.Purchase_IsPublic == 0)
        {
            //if (!supplier.CheckSupplierPurchasePrivates(entity.Purchase_ID, supplier_id))
            //{
                Response.Redirect("/purchase/index.aspx");
            //}
        }
        //else
        //{
        //    Response.Redirect("/purchase/index.aspx");
        //}
        if ((entity.Purchase_ValidDate - DateTime.Now).Days < 0)
        {
            Response.Redirect("/purchase/index.aspx");
            purState = "报价结束";
            priceReportStatus = false;
        }
        else
        {

            purState = "接受报价中";
            purDay = "距离截止时间：还有" + ((entity.Purchase_ValidDate - DateTime.Now).Days + 1) + "天";
            priceReportStatus = true;
        }

        Purchase_Title = entity.Purchase_Title;
        //address = entity.Purchase_State + " " + entity.Purchase_City + " " + entity.Purchase_County + " " + entity.Purchase_Address;

        address = addr.DisplayAddress(entity.Purchase_State, entity.Purchase_City, entity.Purchase_County) + " " + entity.Purchase_Address;
        SupplierPurchaseCategoryInfo category = supplier.GetSupplierPurchaseCategoryByID(entity.Purchase_CateID);
        if (category != null)
        {
            cateName = category.Cate_Name;
        }

    }
    else
    {
        Response.Redirect("/Purchase/index.aspx");
    }  
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=Purchase_Title%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            当前位置 > <a href="/index.aspx" target="_blank">首页</a> > <a href="/Purchase/index.aspx">
                采购信息</a> > <span>
                    <%=Purchase_Title%></span></div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partf">
            <div class="pf_left">
                <div class="blk09">
                    <h2>
                        <strong>
                            <%=Purchase_Title%></strong><span><%=purState %></span></h2>
                    <div class="pf_left_info01">
                        <p>
                            采购类型：<%=pub.GetPurchaseType(entity.Purchase_TypeID)%></p>
                        <p>
                            采购分类：<%=cateName %></p>
                        <p>
                            收货地：<%=address %></p>
                        <p>
                            报价截止日期：<%=entity.Purchase_ValidDate.ToString("yyyy-MM-dd") %></p>
                    </div>
                    <div class="pf_left_info02">
                        <h3>
                            供应商符合以下条件优先</h3>
                        <p>
                            提供样品图片</p>
                        <p>
                            提供增值税发票</p>
                        <p>
                            提供参数规格说明</p>
                        <h3>
                            采购清单</h3>
                        <table width="922" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="247" class="tit">
                                    产品
                                </td>
                                <td width="203" class="tit">
                                    预计数量
                                </td>
                                <td width="209" class="tit">
                                    单位
                                </td>
                                <td width="261" class="tit">
                                    目标单价（元）
                                </td>
                            </tr>
                            <%
                                IList<SupplierPurchaseDetailInfo> entitys_detail = supplier.GetSupplierPurchaseDetailsByApplyID(apply_id);
                                if (entitys_detail != null)
                                {
                                    foreach (SupplierPurchaseDetailInfo entity_detail in entitys_detail)
                                    {
                            %>
                            <tr>
                                <td>
                                    <%=entity_detail.Detail_Name%>
                                </td>
                                <td>
                                    <%=entity_detail.Detail_Amount %>
                                </td>
                                <td>
                                    <%=entity_detail.Detail_Spec%>
                                </td>
                                <td>
                                    <%=pub.FormatCurrency(entity_detail.Detail_Price) %>
                                </td>
                            </tr>
                            <%
                                }
                                }
                            %>
                        </table>
                        <%if(entity.Purchase_Attachment.Length>0)
                          {
                                 Response.Write("<h3>附件下载</h3>");
                                 Response.Write("<p><a href=\""+pub.FormatImgURL(entity.Purchase_Attachment,"fullpath")+"\" target=\"_blank\" class=\"a_t12_blue\">点此下载附件</a></p>");
                            }%>
                        <h3>
                            详细要求</h3>
                        <p>
                            <%=entity.Purchase_Intro %></p>
                    </div>
                    <div class="pf_left_info03">
                        <%=purDay %>
                        <%if (priceReportStatus)
                          { %>
                        <a href="/supplier/Shopping_PriceReport.aspx?apply_id=<%=apply_id %>" target="_blank">
                            立即报价</a>
                        <%} %></div>
                </div>
                <!--询价推荐 开始-->
                <div class="blk10" style="background:url(/images/bg02_3.gif) repeat-x;">
                    <h2 style="background:url(/images/bg02_2.gif) no-repeat;">相关采购单推荐</h2>
                    <div class="main">
                        <%product.SupplierPurchase_IsRecommend("detail"); %>
                    </div>
                </div>
                <!--询价推荐 结束-->
            </div>

            <%--<div class="pf_right">
                <!--采购商信息 开始-->
                <div class="blk11">
                    <%product.SupplierPurchase_Supplier(entity.Purchase_SupplierID); %>
                </div>
                <!--采购商信息 结束-->
            </div>--%>

            <div class="clear"></div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
