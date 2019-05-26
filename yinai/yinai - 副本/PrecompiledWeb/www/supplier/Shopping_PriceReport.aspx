<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Addr addr = new Addr();
    int apply_id = tools.CheckInt(Request["apply_id"]);
    string purchaseName = " -- ";
    string address = "";
    string cateName = " -- ";
    int supplier_id = tools.NullInt(Session["supplier_id"]);
    myApp.Supplier_Login_Check("/supplier/Shopping_PriceReport.aspx?apply_id=" + apply_id);

    if (apply_id == 0)
    {
        Response.Redirect("/Purchase/index.aspx");
    }

    SupplierPurchaseInfo spinfo = myApp.GetSupplierPurchaseByID(apply_id);

    if (spinfo != null)
    {
        if (spinfo.Purchase_SupplierID == supplier_id)
        {
            Response.Redirect("/Purchase/index.aspx");
        }
        if (spinfo.Purchase_Status != 2 || spinfo.Purchase_IsActive != 1 || spinfo.Purchase_Trash != 0)
        {
            Response.Redirect("/Purchase/index.aspx");
        }
        if ((spinfo.Purchase_ValidDate - DateTime.Now).Days<0)
        {
            Response.Redirect("/Purchase/index.aspx");
        }

        if (spinfo.Purchase_IsPublic == 0)
        {
            if (!myApp.CheckSupplierPurchasePrivates(spinfo.Purchase_ID, supplier_id))
            {
                //pub.Msg("info", "提示信息", "非公开报价", true, "/Purchase/index.aspx");
                Response.Redirect("/Purchase/index.aspx");
            }
        }
        purchaseName = spinfo.Purchase_Title;
        address = addr.DisplayAddress(spinfo.Purchase_State, spinfo.Purchase_City, spinfo.Purchase_County) + " " + spinfo.Purchase_Address;
        SupplierPurchaseCategoryInfo category = myApp.GetSupplierPurchaseCategoryByID(spinfo.Purchase_CateID);
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
        <%="采购报价 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <span>采购报价</span>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                <% myApp.Get_Supplier_Left_HTML(11, 99); %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>
                        采购报价</h2>
                    <div class="main">
                        <div class="zkw_order">
                            
                            <form name="formadd" id="formadd" method="post" action="/supplier/shopping_do.aspx">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content"
                                class="table_padding_5">
                                <tr>
                                    <td align="right" width="100" style="line-height: 24px;" class="t12_53">
                                        采购标题
                                    </td>
                                    <td align="left">
                                        <%= purchaseName %>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购类型
                                    </td>
                                    <td>
                                        <%=pub.GetPurchaseType(spinfo.Purchase_TypeID)%>
                                    </td>
                                </tr>
                                                  <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购分类
                                    </td>
                                    <td>
                                        <%=cateName%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        收货地
                                    </td>
                                    <td>
                                        <%= address %>
                                    </td>
                                </tr>
                                <tr>
                                   <td align="right" style="line-height: 24px;" class="t12_53">
                                        报价截至时间
                                    </td>
                                    <td>
                                        <%=spinfo.Purchase_ValidDate.ToString("yyyy-MM-dd") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        采购标题
                                    </td>
                                    <td align="left">
                                        <%=spinfo.Purchase_Title %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        详细要求
                                    </td>
                                    <td align="left">
                                        <div style="padding:10px;">
                                                    <%=spinfo.Purchase_Intro %></div>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        报价标题
                                    </td>
                                    <td align="left">
                                        <input name="PriceReport_Title" type="text" class="txt_border" id="PriceReport_Title"
                                            size="40" maxlength="100" />
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>--%>
                                
                                
                            </table>


                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="apply_3_content"
                                >
                                <tr><td align="right" width="100" height="30"><b>采购清单</b></td><td>&nbsp;</td></tr>
                                <tr>
                                    <td colspan="2">
                                    <table width="100%" cellpadding="0" align="center" cellspacing="1" style="background:#dadada; margin:10px 0px;" ><tr style="background:url(/images/ping.jpg); height:30px;">  <th align="center" valign="middle">产品名称</th>  <th align="center" valign="middle" >规格/单位</th>  <th width="120" align="center" valign="middle">采购数量</th>  <th width="110" align="center" valign="middle">采购单价</th><th width="120" align="center" valign="middle">供货数量</th>  <th width="110" align="center" valign="middle">供货报价</th></tr>
                                        <%
                                            IList<SupplierPurchaseDetailInfo> entitys = myApp.GetSupplierPurchaseDetailsByApplyID(apply_id);
                                            int i = 1;

                                            if (entitys != null)
                                            {

                                                foreach (SupplierPurchaseDetailInfo entity in entitys)
                                                {
                                                    
                                        %>
                                        <tr bgcolor="#FFFFFF" height="35"><td align="center" valign="middle"><%=entity.Detail_Name%></td><td align="center" valign="middle"><%=entity.Detail_Spec%></td><td align="center" valign="middle"><%=entity.Detail_Amount%></td><td align="center" valign="middle"> <%=pub.FormatCurrency(entity.Detail_Price)%></td><td align="center" valign="middle"><input type="text" class="txt_border" name="Detail_Amount<%=i %>"  style="width:80px;" id="Text1"
                                                        value="" /></td><td align="center" valign="middle"> <input type="text" class="txt_border" name="Detail_Price<%=i %>" style="width:80px;" id="Text2"
                                                        value="" /><input type="hidden" name="detail_<%=i %>" value="<%=entity.Detail_ID %>" /></td></tr>
                                        
                                        <%
i++;
                                                }

                                            } %>
                                           </table>

                                        <input type="hidden" value="<%=i %>" name="Purchase_Amount" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                             <tr>
                                    <td align="right" width="100" style="line-height: 24px;" class="t12_53">
                                        交货时间
                                    </td>
                                    <td align="left">
                                        <input type="text" class="input_calendar" name="PriceReport_DeliveryDate" id="PriceReport_DeliveryDate"
                                            maxlength="10" readonly="readonly" />
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                $("#PriceReport_DeliveryDate").datepicker({ numberOfMonths: 1 });
                                            });
                                        </script>
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        联系人
                                    </td>
                                    <td align="left">
                                        <input name="PriceReport_Name" type="text" class="txt_border" id="PriceReport_Name"
                                            size="40" maxlength="20" />
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        联系手机
                                    </td>
                                    <td align="left">
                                        <input name="PriceReport_Phone" class="txt_border" type="text" id="PriceReport_Phone"
                                            size="40" maxlength="20" />
                                        <span class="t12_red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="line-height: 24px;" class="t12_53">
                                        备注
                                    </td>
                                    <td align="left">
                                        <textarea name="PriceReport_Note"  id="PriceReport_Note"
                                            cols="50" rows="5" ></textarea>
                                        
                                    </td>
                                </tr>
                                </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                <tr>
                                    <td align="center">
                                        <input name="apply_id" type="hidden" id="apply_id" value="<%=apply_id %>" />
                                        <input name="action" type="hidden" id="action" value="shop_apply_pricereport" />
                                        <input name="btn_submit" type="image" src="/images/save.jpg" />
                                    </td>
                                </tr>
                            </table>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
