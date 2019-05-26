<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<!DOCTYPE html>

<%
    Session["SubPosition"] = "Purchase";
    Product product = new Product();
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Addr addr = new Addr();
    Member member=new Member();
    MemberInfo memberInfo = null;
    int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

    if (Purchase_ID == 0)
    {
        Response.Redirect("/index.aspx");
    }
    
    MemberPurchaseInfo purchaseInfo = product.GetMemberPurchaseInfoByID(Purchase_ID);
    if (purchaseInfo == null)
    {
        Response.Redirect("/index.aspx");
    }
    else
    {
        memberInfo = member.GetMemberByID(purchaseInfo.Purchase_MemberID);
        if (memberInfo == null && memberInfo.MemberProfileInfo == null)
        {
            Response.Redirect("/index.aspx");
        }
    }
    
     %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=purchaseInfo.Purchase_Title+" - "+pub.SEO_TITLE() %></title>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>
    <style type="text/css">
        .companyTable th { width:100px; text-align:right; line-height:30px; padding:0px 5px; background-color: #f5f5f5; }
        .companyTable td { padding:0px 30px 0px 5px; }
        .tablecert { margin-top:10px; }
        .tablecert td table td { line-height:30px; }
        .tablecert img { display:inline; vertical-align:middle; }
        .b28_info02 dl dt img { width:120px; height:120px; margin:auto; }
    </style>
</head>
<body>
    <uctop:top ID="Top" runat="server" />
    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > <a href="/product/purchase_Index.aspx">大宗采购</a> > <strong><%=purchaseInfo.Purchase_Title %></strong></div>
            <!--位置说明 结束-->
            <div class="partm">
                <div class="pm_left">
                    <dl>
                        <dt style="border-bottom: none;">
                            <img src="<%=pub.FormatImgURL(purchaseInfo.Purchase_Img,"fullpath") %>" style="width: 320px; height: 320px; margin: 0 auto;"></dt>
                        <div class="clear"></div>
                    </dl>
                </div>
                <div class="pm_right">
                    <div class="pm_info07">
                        <table width="747" border="0" cellspacing="5" cellpadding="0">
                            <tr>
                                <td width="113" class="name">采购商品：</td>
                                <td width="619"><%=purchaseInfo.Purchase_Title %></td>
                            </tr>
                            <tr>
                                <td class="name">采购商家：</td>
                                <td>北京光蓝珠宝有限公司</td>
                            </tr>
                            <tr>
                                <td class="name">采购数量：</td>
                                <td><%=purchaseInfo.Purchase_Amount+purchaseInfo.Purchase_Unit %></td>
                            </tr>
                            <tr>
                                <td class="name">有效日期：</td>
                                <td><%=purchaseInfo.Purchase_Validity.ToString("yyyy年MM月dd日") %></td>
                            </tr>
                            <tr>
                                <td class="name">备注说明：</td>
                                <td>
                                    <p><%=purchaseInfo.Purchase_Intro %></p>
                                </td>
                            </tr>
                            <tr>
                                <td class="name">报价留言：</td>
                                <td><a href="javascript:;" onclick="Show_PurcharseReply_Dialog(<%=Purchase_ID %>);">报价留言</a></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div class="partl" style="margin-top: 15px;">
                <div class="pl_left">
                    <div class="blk28">
                        <h2>采购商信息</h2>
                        <div class="b28_info02">
                             <dl>
                                        <dt><img src="<%=pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_HeadImg,"fullpath") %>"></dt>
                                        <dd>
                                            <p><%=memberInfo.MemberProfileInfo.Member_Company %></p>
                                            <p><span>入住时间：</span><%=memberInfo.Member_Addtime.ToString("yyyy年MM月dd日") %></p>
                                            <p><span>公司地址：</span><%=addr.DisplayAddress(memberInfo.MemberProfileInfo.Member_State,memberInfo.MemberProfileInfo.Member_City,"") %></p>
                                            <%--<p><span>信用评价：</span><img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg"><img src="images/x.jpg"></p>--%>
                                        </dd>
                                    </dl>
                        </div>
                    </div>
                    <%--<div class="blk28" style="margin-top: 15px;">
                        <h2>采购咨询</h2>
                        <div class="b28_info02">
                            <dl>
                                <dd>
                                    <p><a href="#" target="_blank">在线咨询<img src="/images/icon12.png"></a></p>
                                    <p><span>采购热线：</span>400-882-6621</p>
                                </dd>
                            </dl>
                        </div>
                    </div>--%>
                </div>
                <div class="pl_right">
                    <div class="blk28">
                        <h2 style="background-color: #FFF;">公司概况</h2>
                        <div class="b28_main08">
                            <table class="companyTable">
                                <tr>
                                    <th>公司名称</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_Company %></td>
                                    <th>所在地区</th>
                                    <td><%=addr.DisplayAddress(memberInfo.MemberProfileInfo.Member_State,memberInfo.MemberProfileInfo.Member_City,memberInfo.MemberProfileInfo.Member_County)+memberInfo.MemberProfileInfo.Member_StreetAddress%></td>
                                </tr>
                                <tr>
                                    <th>注册资金</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_RegisterFunds %></td>
                                    <th>营业执照副本号</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_BusinessCode %></td>
                                </tr>
                                <tr>
                                    <th>组织机构代码</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_OrganizationCode %></td>
                                    <th>法人</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_Corporate %></td>
                                </tr>
                                <tr>
                                    <th>注册资金</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_RegisterFunds %></td>
                                    <th>税务登记证</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_TaxationCode  %></td>
                                </tr>
                                <tr>
                                    <th>银行开户号</th>
                                    <td><%=memberInfo.MemberProfileInfo.Member_BankAccountCode %></td>
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
                                IList<MemberCertInfo> certs = null;
                                certs = member.GetMemberCertByType(0);
                                if (certs != null)
                                {
                                    Response.Write("<table class=\"table_padding_5 tablecert\" width=\"100%\" >");
                                    Response.Write("	<tr>");
                                    
                                    string Member_Cert;
                                    int rows = certs.Count >= 6 ? 6 : certs.Count;
                                    if (rows == 0) rows = 1;
                                    double width = 100.0 / (double)(rows);
                                    int icount = 0;
                                    
                                    foreach (MemberCertInfo entity in certs)
                                    {
                                        Member_Cert = member.Get_Member_Cert(entity.Member_Cert_ID, memberInfo.MemberRelateCertInfos);

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
                                        Response.Write(entity.Member_Cert_Name);
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
