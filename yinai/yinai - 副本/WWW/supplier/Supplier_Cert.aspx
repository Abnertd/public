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
    Member myMem = new Member();


    int Member_AuditStatus = -1;
    int Supplier_AuditStatus =-1;
    if (tools.NullInt(Session["supplier_auditstatus"]) == 1)
    {
        myApp.Supplier_AuditLogin_Check("/supplier/supplier_cert.aspx");
    }

    SupplierInfo supplierinfo = myApp.GetSupplierByID();
    string Supplier_Cert = "";
    int Supplier_Cert_Status, Supplier_CertType, Member_Cert_Status;
    IList<SupplierRelateCertInfo> relateinfos = null;

    //资质
    Supplier_Cert_Status = 1;
    Supplier_CertType = 0;
    if (supplierinfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        Supplier_CertType = supplierinfo.Supplier_CertType;

        relateinfos = supplierinfo.SupplierRelateCertInfos;
        Supplier_Cert_Status = supplierinfo.Supplier_Cert_Status;
       Supplier_AuditStatus= supplierinfo.Supplier_AuditStatus;
    }
    
    

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%="资质管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="../css/index_newadd.css" rel="stylesheet" />
     <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
 <%--   <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    
  
<%--    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>
    <!--弹出菜单 end-->

     
   <!--示范一个公告层 开始-->
  <script type="text/javascript">
      function SignUpNow() {
          layer.open({
              type: 2
 , title: false //不显示标题栏
              //, closeBtn: false
 , area: ['480px;', '340px']

 , shade: 0.8
 , id: 'LAY_layuipro' //设定一个id，防止重复弹出
 , resize: false
 , btnAlign: 'c'
 , moveType: 1 //拖拽模式，0或者1              
              , content: ("/Bid/SignUpPopup.aspx")
          });
      }
    </script>
   <!--示范一个公告层 结束-->
    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>

    <style type="text/css">
        .blk17 table td {
            text-align: center;
        }


            .blk17 table td a.a11 {
                background-image: url(../images/a_bg05.jpg);
                background-repeat: no-repeat;
                width: 141px;
                height: 38px;
                font-size: 16px;
                font-weight: bold;
                text-align: center;
                line-height: 38px;
                display: block;
                color: #FFF;
                margin-bottom: 18px;
                margin-top: 15px;
            }


        .blk table tr td table {
            margin-left: 38px;
        }


        .uploadify-button {
            margin-top: 200px;
        }
    </style>

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/">我是卖家</a> > 账户管理 > <strong>资质管理</strong></div>
            <!--位置说明 结束-->
            <%-- <div class="partc">--%>
            <div class="partd_1" style="overflow: visible">
                <div class="pd_left">
                      <% myApp.Get_Supplier_Left_HTML(4, 5); %>
                 
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>资质管理</h2>                     
                        
                    <%
                        IList<SupplierCertInfo> certs = myApp.GetSupplierCertByType(0);
                        //未审核 certs!=null true                         
                        if (certs != null && tools.NullInt(Supplier_AuditStatus) ==1)
                        { 
                    %>
                        <div class="blk17_sz">
                            <input type="hidden" name="Supplier_CertType1" value="0" />
                            <table width="893" border="0" cellspacing="0" cellpadding="2" id="Table1" class="table_padding_5">

                                <tr>
                                    <%foreach (SupplierCertInfo entity in certs)
                                      {
                                          Supplier_Cert = myApp.Get_Supplier_Cert(entity.Supplier_Cert_ID, relateinfos);
                                    %>
                                    <td width="<%=(100/certs.Count) %>%" style="text-align: center;">                                       
                                        <table border="0" width="100%" cellpadding="3" id="id001" cellspacing="0" style="margin-left: 38px; width: <%=(100/certs.Count) %>%; text-align: center;">
                                            <tr>
                                                <td align="center" height="120">
                                                    <a href="<%=pub.FormatImgURL(Supplier_Cert,"fullpath") %>" target="_blank">
                                                        <img id="img1" src="<%=pub.FormatImgURL(Supplier_Cert,"fullpath") %>" width="120" alt="点击查看原图" title="点击查看原图" height="120" onload="javascript:AutosizeImage(this,120,120);"></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <%=entity.Supplier_Cert_Name%><input type="hidden" name="supplier_cert<%=entity.Supplier_Cert_ID %>"
                                                        id="supplier_cert<%=entity.Supplier_Cert_ID %>" value="<%=Supplier_Cert %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <%}
                                    %>
                                </tr>
                            </table>
                        </div>
                                                
                        <%}%>
                        <form name="formadd" id="formadd" method="post">
                    <%--   资质未审核 0--%>
                            <%if (Supplier_Cert_Status == 0)
                              { %>
                            <div class="blk17" style="margin-top: 15px;">
                                <input type="hidden" name="Supplier_CertType1" value="0" />

                                <%if (certs != null)
                                  { %>
                                <table width="893" border="0" cellspacing="0" cellpadding="5" style="width: 100%;">
                                    <tr>
                                        <%foreach (SupplierCertInfo entity in certs)
                                          {
                                              Supplier_Cert = myApp.Get_Supplier_Certtmp(entity.Supplier_Cert_ID, relateinfos);
                                        %>
                                        <td width="<%=(100/certs.Count) %>%" style="text-align: center; margin-left: 38px;">
                                            <table border="0" cellpadding="3" cellspacing="0" style="margin: 0 auto; width: <%=(100/certs.Count) %>%; text-align: center;">
                                                <tr>
                                                    <td align="center" height="120">
                                                        <img id="img_supplier_cert<%=entity.Supplier_Cert_ID %>_tmp" name="img_supplier_cert<%=entity.Supplier_Cert_ID %>_tmp"
                                                            src="<%=pub.FormatImgURL(Supplier_Cert,"fullpath") %>" width="120" height="120"
                                                            onload="javascript:AutosizeImage(this,120,120);">
                                                    </td>
                                                </tr>
                                                <%if (Supplier_Cert_Status != 2)
                                                  { %>
                                                <tr>
                                                    <td align="center" height="30" style="margin-top: 150px;">
                                                        <%--<input type="button" name="btn_upload" value="上传" class="buttonupload" onclick="javascript:openUpload('supplier_cert<%=entity.Supplier_Cert_ID %> _tmp');" />
                                                    <input type="button" name="btn_upload" value="删除" class="buttonupload" onclick="javascript:delImage('supplier_cert<%=entity.Supplier_Cert_ID %>    _tmp');">--%>
                                                        <input type="hidden" name="supplier_cert<%=entity.Supplier_Cert_ID %>_tmp" id="supplier_cert<%=entity.Supplier_Cert_ID %>_tmp"
                                                            value="<%=Supplier_Cert %>" />

                                                        <iframe style="margin: 40px 0 0 15px;" id="iframe_upload<%=entity.Supplier_Cert_ID %>" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=formadd&frmelement=supplier_cert<%=entity.Supplier_Cert_ID %>_tmp&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe>
                                                    </td>
                                                </tr>
                                                <%} %>
                                                <tr>
                                                    <td align="center">
                                                        <%=entity.Supplier_Cert_Name%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%} %>
                                    </tr>

                                    <tr id="td_upload" style="display: none; text-align: left;">
                                        <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="100%" height="90" frameborder="0" scrolling="no"
                                            align="absmiddle"></iframe>
                                        </td>
                                    </tr>

                                    <% if (Supplier_Cert_Status != 2)
                                       { %>
                                    <tr>
                                        <td colspan="4" align="center" style="text-align: center;">
                                            <input type="hidden" id="action" name="action" value="shop_certsave" />
                                            <a href="javascript:void();" onclick="shop_certsave();" class="a11" style="background-color:none;background-image:url(../images/save_buttom.jpg); width:79px;height:28px;margin-left:100px "></a>
                                        </td>
                                    </tr>
                                    <%} %>
                                </table>
                                <%} %>
                            </div>
                            <%} %>
                        </form>
                    </div>





                </div>
                <div class="clear">
                </div>
            </div>
            
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
   <div id="leftsead">
        <ul>           
            <li>
                <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                           <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
              <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none">
                        <div class="hides" id="p3">
                            <img src="images/nav_3_1.png" width="130px;" height="50px" id="Img2" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
   <%--右侧浮动弹框 结束--%>
    <!--主体 结束-->
    <ucbottom:bottom ID="bottom1" runat="server" />
</body>
</html>
