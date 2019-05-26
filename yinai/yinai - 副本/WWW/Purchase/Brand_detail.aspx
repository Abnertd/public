<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<%
    Session["SubPosition"] = "Brand";
    Product product = new Product();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Addr addr = new Addr();

    Supplier supplier = new Supplier();
    int Merchants_ID = tools.CheckInt(Request["Merchants_ID"]);
    if (Merchants_ID == 0)
    {
        Response.Redirect("/index.aspx");
    }

    SupplierInfo supplierInfo;

    SupplierMerchantsInfo MerchantsInfo = product.GetBrandJoinedByID(Merchants_ID);
    if (MerchantsInfo == null)
    {
        Response.Redirect("/index.aspx");
    }

    supplierInfo = supplier.GetSupplierByID(MerchantsInfo.Merchants_SupplierID);
    if (supplierInfo == null)
    {
        Response.Redirect("/index.aspx");
    }

    SupplierShopInfo shopInfo = supplier.GetSupplierShopBySupplierID(MerchantsInfo.Merchants_SupplierID);
    if (shopInfo == null)
    {
        Response.Redirect("/index.aspx");
    }
    
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=MerchantsInfo.Merchants_Name+" - "+pub.SEO_TITLE() %></title>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/member.js"></script>
    <style type="text/css">
        .companyTable th {
            width: 100px;
            text-align: right;
            line-height: 30px;
            padding: 0px 5px;
            background-color: #f5f5f5;
        }

        .companyTable td {
            padding: 0px 30px 0px 5px;
        }

        .tablecert {
            margin-top: 10px;
        }

            .tablecert td table td {
                line-height: 30px;
            }

            .tablecert img {
                display: inline;
                vertical-align: middle;
            }

        .b28_info02 dl dt img {
            width: 120px;
            height: 120px;
            margin: auto;
        }
    </style>
</head>
<body>
    <uctop:top ID="Top" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <a href="/purchase/brand_joined.htm">品牌加盟</a> > <strong><%=MerchantsInfo.Merchants_Name %></strong></div>
            <!--位置说明 结束-->
            <div class="partm">
                <div class="pm_left">
                    <dl>
                        <dt style="border-bottom: none;">
                            <img src="<%=pub.FormatImgURL(MerchantsInfo.Merchants_Img,"fullpath") %>" style="width: 320px; height: 320px; margin: 0 auto;"></dt>
                        <div class="clear"></div>
                    </dl>
                </div>
                <div class="pm_right">
                    <div class="pm_info07">
                        <table width="747" border="0" cellspacing="5" cellpadding="0">
                            <tr>
                                <td width="113" class="name">招商加盟：</td>
                                <td width="619"><%=MerchantsInfo.Merchants_Name %></td>
                            </tr>
                            <tr>
                                <td class="name">有效期：</td>
                                <td><%=MerchantsInfo.Merchants_Validity %>个月</td>
                            </tr>
                            <tr>
                                <td class="name">加盟渠道：</td>
                                <td>
                                    <%=MerchantsInfo.Merchants_Channel %>
                                </td>
                            </tr>
                            <tr>
                                <td class="name">加盟条件：</td>
                                <td>
                                    <%=MerchantsInfo.Merchants_Trem %>
                                </td>
                            </tr>
                            <tr>
                                <td class="name">说明：</td>
                                <td>
                                    <p><%=MerchantsInfo.Merchants_Intro %></p>
                                </td>
                            </tr>
                            <tr>
                                <td class="name">快速加盟：</td>
                                <td><a href="javascript:;" onclick="Show_MerchantsReply_Dialog(<%=Merchants_ID %>);">快速加盟</a></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div class="partl" style="margin-top: 15px;">
                <div class="pl_left">
                    <div class="blk28">
                        <h2>商家信息</h2>
                        <div class="b28_info02">
                            <dl>
                                <dt>
                                    <img src="<%=pub.FormatImgURL(shopInfo.Shop_Img,"fullpath") %>"></dt>
                                <dd>
                                    <p><%=supplierInfo.Supplier_CompanyName %></p>
                                    <p><span>入住时间：</span><%=supplierInfo.Supplier_Addtime.ToString("yyyy年MM月dd日") %></p>
                                    <p><span>公司地址：</span><%=addr.DisplayAddress(supplierInfo.Supplier_State, supplierInfo.Supplier_City, supplierInfo.Supplier_County) %></p>
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="pl_right">
                    <div class="blk28">
                        <h2 style="background-color: #FFF;">公司概况</h2>
                        <div class="b28_main08">
                            <table class="companyTable">
                                <tr>
                                    <th>公司名称</th>
                                    <td><%=supplierInfo.Supplier_CompanyName %></td>
                                    <th>所在地区</th>
                                    <td><%=addr.DisplayAddress(supplierInfo.Supplier_State, supplierInfo.Supplier_City, supplierInfo.Supplier_County) + supplierInfo.Supplier_Address%></td>
                                </tr>
                                <tr>
                                    <th>法人</th>
                                    <td><%=supplierInfo.Supplier_Corporate %></td>
                                    <th>电话</th>
                                    <td><%=supplierInfo.Supplier_CorporateMobile %></td>
                                </tr>
                                <tr>
                                    <th>注册资金</th>
                                    <td><%=supplierInfo.Supplier_RegisterFunds %></td>
                                    <th>营业执照副本号</th>
                                    <td><%=supplierInfo.Supplier_BusinessCode %></td>
                                </tr>
                                <tr>
                                    <th>组织机构代码</th>
                                    <td><%=supplierInfo.Supplier_OrganizationCode %></td>
                                    <th>税务登记证</th>
                                    <td><%=supplierInfo.Supplier_TaxationCode %></td>
                                </tr>
                                <tr>
                                    <th>银行开户号</th>
                                    <td><%=supplierInfo.Supplier_BankAccountCode %></td>
                                    <th></th>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="blk28" style="margin-top: 15px;">
                        <h2 style="background-color: #FFF;">认证信息</h2>
                        <div class="b28_main08">

                            <%
                                IList<SupplierCertInfo> certs = supplier.GetSupplierCertByType(0);
                                if (certs != null)
                                {
                                    Response.Write("<table class=\"table_padding_5 tablecert\" width=\"100%\" >");
                                    Response.Write("	<tr>");

                                    string Member_Cert;
                                    int rows = certs.Count >= 6 ? 6 : certs.Count;
                                    if (rows == 0) rows = 1;
                                    double width = 100.0 / (double)(rows);
                                    int icount = 0;

                                    foreach (SupplierCertInfo entity in certs)
                                    {
                                        Member_Cert = supplier.Get_Supplier_Cert(entity.Supplier_Cert_ID, supplierInfo.SupplierRelateCertInfos);

                                        if ((icount % rows) == 0)
                                        {
                                            Response.Write("</tr><tr>");
                                        }

                                        Response.Write("		<td width=\"" + width + "%\">");
                                        Response.Write("			<table border=\"0\" width=\"100%\" cellpadding=\"3\" cellspacing=\"0\">");
                                        Response.Write("				<tr>");
                                        Response.Write("					<td align=\"center\" height=\"120\">");
                                        Response.Write("						<a href=\"" + pub.FormatImgURL(Member_Cert, "fullpath") + "\" target=\"_blank\">");
                                        Response.Write("							<img id=\"img1\" src=\"" + pub.FormatImgURL(Member_Cert, "fullpath") + "\" width=\"120\" alt=\"点击查看原图\" title=\"点击查看原图\" height=\"120\" onload=\"javascript:AutosizeImage(this,120,120);\"></a>");
                                        Response.Write("					</td>");
                                        Response.Write("				</tr>");
                                        Response.Write("				<tr>");
                                        Response.Write("					<td align=\"center\">");
                                        Response.Write(entity.Supplier_Cert_Name);
                                        Response.Write("					</td>");
                                        Response.Write("				</tr>");
                                        Response.Write("			</table>");
                                        Response.Write("		</td>");

                                        icount++;
                                    }
                                    Response.Write("	</tr>");
                                    Response.Write("</table>");
                                }
                            %>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <ucbottom:bottom ID="Bottom" runat="server" />
</body>
</html>
