<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Addr addr = new Addr();

    myApp.Supplier_AuditLogin_Check("/supplier/shouquanshu.aspx");

    SupplierInfo supplierinfo = myApp.GetSupplierByID();
    string tip = tools.CheckStr(Request["tip"]);

    IList<SupplierRelateCertInfo> relateinfos = null;

    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        if (supplierinfo.Supplier_Type == 0)
        {
            Response.Redirect("/supplier/index.aspx");
            //pub.Msg("error", "错误信息", "买家才能上传授权书！", false, "/supplier/index.aspx"); 
        }
    }
    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="我的授权书 - 会员中心 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();

            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/FileUpload.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="supplier_cert.aspx">我的授权书</a>
        </div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="partd_1">
            <div class="pd_left">
                <% myApp.Get_Supplier_Left_HTML(9, 110); %>
            </div>
            <div class="pd_right">
                <div class="blk14_1" style="margin-top: 0px">
                    <h2>我的授权书</h2>
                    <div style="padding-left: 35px;">
                        <%
                            //if (tip == "success")
                            //{
                            //    //pub.Tip("positive", "您的授权书已上传，请等待审核结果！");
                            //}
                            //else
                            //{
                            if (supplierinfo.Supplier_SQSISActive == 2)
                            {
                                pub.Tip("error", "您的授权书审核未通过，请重新上传！");
                            }
                            else if (supplierinfo.Supplier_SQSISActive == 1)
                            {
                                pub.Tip("info", "您的授权书已通过审核！");
                            }
                            else
                            {

                            }
                                                        //}
                        %>
                    </div>
                    <div class="b14_1_main">
                        <div class="b07_main">
                            <%--  <div class="b07_info04">--%>
                            <form name="formadd" id="formadd" method="post" action="/supplier/account_do.aspx">
                                <div class="main">
                                    <div style="font-size: 16px; font-weight: 800; width: 100%; color: Red; text-indent: 87px; line-height: 40px;">※ 填写以后，请拍摄照片进行上传，我们会在第一时间审核</div>
                                    <input type="hidden" name="Supplier_CertType" value="0" />
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_2_content"
                                        class="table_padding_5">
                                        <tr>
                                            <td align="right" style="line-height: 24px;" class="t12_53">上传授权书
                                            </td>
                                            <td align="left">

                                                <div>
                                                    <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/fileupload.aspx?App=attachment&formname=formadd&frmelement=Supplier_SQS&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                                        width="242" height="22" frameborder="0" scrolling="no" style="margin-left: 10px;"></iframe>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td align="right" style="line-height: 24px;" class="t12_53">授权书路径
                                            </td>
                                            <td align="left">
                                                <input name="Supplier_SQS" id="Supplier_SQS" class="txt_border" size="40" type="text"
                                                    value="" />
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div style="text-align: center; padding-top: 15px;">
                                    <input name="action" type="hidden" id="action" value="shop_sqssave" />
                                    <input name="btn_submit" type="image" src="/images/save_buttom.jpg" />
                                </div>
                            </form>
                            <%--      </div>--%>
                        </div>
                    </div>
                </div>
                <div style="padding: 20px; line-height: 28px;">
                    <b>授权书下载</b>
                    <ul>
                        <li>1、<a href="/supplier/fj/授权书.doc" style="color: Blue"><u>点击下载《采购商入驻授权书》</u></a></li>
                        <li>2、授权书填写以后，请拍摄照片进行上传，我们会在第一时间进行审核</u></a></li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--主体 结束-->
    <ucbottom:bottom ID="bottom1" runat="server" />
</body>
</html>
