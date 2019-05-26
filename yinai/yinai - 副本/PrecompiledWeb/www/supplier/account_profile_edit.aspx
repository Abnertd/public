<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Addr addr = new Addr();
    Supplier supplier = new Supplier();
    IList<SupplierRelateCertInfo> relateinfos = null;
    int Supplier_AuditStatus = 0;
    int Supplier_Cert_Status;
    string Member_QQ = "";
    



    MemberInfo memberInfo = member.GetMemberByID();
    if (memberInfo!=null)
    {
        SupplierInfo Supplierinfo = supplier.GetSupplierByID(memberInfo.Member_SupplierID);
        if (Supplierinfo != null)
        {
            Supplier_AuditStatus = Supplierinfo.Supplier_AuditStatus;
            //Supplier_AuditStatus = Supplierinfo.Supplier_Cert_Status;
            relateinfos = Supplierinfo.SupplierRelateCertInfos;
        }
    }
  
  
    MemberProfileInfo profileInfo = null;
    string Supplier_Cert = "";
    
    if (memberInfo == null)
    {
        Response.Redirect("/supplier/index.aspx");
    }
    else
    {
        profileInfo = memberInfo.MemberProfileInfo;

        if (profileInfo == null)
        {
            Response.Redirect("/supplier/index.aspx");
        }
        else
        {
            Member_QQ = profileInfo.Member_QQ;
        }
    }
    MemberSubAccountInfo subaccountinfo = member.GetSubAccountByID();
    IList<SupplierCertInfo> certs = supplier.GetSupplierCertByType(0);

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title><%="账户信息 - 我是卖家 - " + pub.SEO_TITLE()%></title>

    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
      <link href="../css/index_newadd.css" rel="stylesheet" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
<%--  <!--layer弹出框.js .css引用 结束-->
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
  <%--  <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>--%>

    <script type="text/javascript">
        function openSealUpload(openObj) {
            $("#td_sealupload").show();
            $("#iframe_sealupload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=frm_account_profile_edit&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }
        function delSealImage(openObj) {
            $("#img_" + openObj).attr("src", "/images/detail_no_pic.gif");
            $("#" + openObj).val("/images/detail_no_pic.gif");
        }

    </script>
<%--    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
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
            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=frm_account_profile_edit&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "";
        }
    </script>


    <style type="text/css">
        .input01 {
            background-image: none;
            padding-left: 10px;
            width: 252px;
        }


        .ke-container ke-container-default .ke-edit {
            display: block;
            height: 255px;
        }
        .b07_info04 table td.name {
              padding: 10px;
        }
  
    </style>
    <!--弹出菜单 end-->

</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>资料修改</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1">
                <div class="pd_left">              
                       <% supplier.Get_Supplier_Left_HTML(4, 6); %>
                </div>

                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>资料管理</h2>
                        <div class="blk07_sz">

                            <div class="b07_main_sz">
                                <div class="b07_info04">
                                    <form name="frm_account_profile_edit" id="frm_account_profile_edit" method="post" action="/supplier/account_do.aspx">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">
                                       
                                            <tr>
                                                <td width="148" class="name">用户名
                                                </td>
                                                <td >
                                                  <%--  <input name="Member_NickName" type="text" id="Member_NickName" style="width: 258px;" class="input01" maxlength="100" value="<%=memberInfo.Member_NickName %>" /><i>*</i>--%>
                                                    <%=memberInfo.Member_NickName %>
                                                </td>
                                            </tr>

                                          <%--  <tr style="display:none;">
                                                <td width="148" class="name">Email
                                                </td>
                                                <td>                                                 
                                                    <input name="Member_Email" type="text" id="Member_Email" style="width: 258px;" class="input01" maxlength="100" value="<%=memberInfo.Member_Email %>" /><i>*</i>
                                                </td>
                                            </tr>  --%>

                                            <tr>
                                                <td width="148" class="name">真实姓名
                                                </td>
                                                <td>                                                 
                                                   <%-- <input name="Member_RealName" type="text" id="Member_RealName" style="width: 258px;" class="input01" maxlength="100" value="<%=profileInfo.Member_RealName %>" /><i>*</i>--%>
                                                    <%=profileInfo.Member_RealName %>
                                                </td>
                                            </tr>  
                                           
                                             <tr>
                                                <td width="148" class="name">手机号码
                                                </td>
                                                <td>                                                   
                                                  <%--  <input name="Member_LoginMobile" type="text" id="Member_LoginMobile" style="width: 258px;" class="input01" maxlength="11" value="<%=memberInfo.Member_LoginMobile %>" /><i>*</i>--%>
                                              <%=memberInfo.Member_LoginMobile %>
                                                      </td>
                                            </tr>


                                            
                                             <tr>
                                                <td width="148" class="name">QQ号码
                                                </td>
                                                <td>
                                                  
                                                    <input name="Member_Profile_QQ" type="text" id="Member_Profile_QQ" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_QQ %>" /><i>*</i>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td width="148" class="name">常用联系人
                                                </td>
                                                <td>
                                                    <input name="Member_Profile_Contactman" type="text" id="Member_Profile_Contactman" style="width: 258px;" class="input01" maxlength="50" value="<%=profileInfo.Member_Name%>" /><i>*</i>
                                                </td>
                                            </tr>


                                              <tr>
                                                <td width="148" class="name">常用联系人电话
                                                </td>
                                                <td>
                                                    <input name="Member_Profile_Phone" type="text" id="Member_Profile_Phone" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Phone_Number %>" /><i>*</i>
                                                </td>
                                            </tr>







                                          
                                                                                    
                                           
                                            <tr>
                                               <td width="148" class="name">公司名称
                                                </td>
                                                <%-- <td align="left">                                              
                                                    <input name="Member_Profile_CompanyName" type="text" id="Member_Profile_CompanyName" style="width: 258px;" class="input01" maxlength="100" value="<%=profileInfo.Member_Company %>" /><i>*</i>
                                                </td>--%>
                                            <td><%=profileInfo.Member_Company %></td>    
                                            </tr>




                                           


                                           

                                          
                               

                                            <tr>
                                                <td width="148" class="name">详细地址</td>
                                                <td>
                                                    <input type="hidden" name="Member_Profile_Country" id="Member_Profile_Country" value="<%=profileInfo.Member_Country %>" /><input
                                                        name="Member_Profile_Address" type="text" id="Member_Profile_Address"
                                                        style="width: 258px;" class="input01" maxlength="100" value="<%=profileInfo.Member_StreetAddress %>" /><i>*</i>
                                                </td>
                                            </tr>

                                      

                                            <tr>
                                                <td width="148" class="name">法定代表人姓名
                                                </td>
                                                <td>
                                                    <input name="Member_Corporate" type="text" id="Member_Corporate" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_Corporate %>" /><i>*</i>
                                                </td>
                                            </tr>

                                         
                                       


                                             <tr>
                                                <td width="148px" class="name">统一社会代码证号
                                                </td>
                                                <td>
                                                    <input name="Member_UniformSocial_Number" type="text" id="Member_UniformSocial_Number" style="width: 258px;" class="input01"
                                                        maxlength="20" value="<%=profileInfo.Member_UniformSocial_Number %>" /> <i>*</i> <span style="padding-left:10px;color:red;font-size:12px;">未办理三证合一的填写营业执照的本照</span>
                                                </td>
                                            </tr>


                                             <input type="hidden" name="Supplier_CertType" value="0" />
                                            <tr>
                                                <%foreach (SupplierCertInfo entity in certs)
                                                  {
                                                      Supplier_Cert = supplier.Get_Supplier_Certtmp(entity.Supplier_Cert_ID, relateinfos);
                                                %>
                                                <td width="<%=(100 / certs.Count)%>%" style="text-align: center; margin-left: 38px;">
                                                    <table border="0" cellpadding="3" cellspacing="0" style="margin: 0 auto; width: <%=(100 / certs.Count)%>%; text-align: center;">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_supplier_cert<%=entity.Supplier_Cert_ID%>_tmp" name="img_supplier_cert<%=entity.Supplier_Cert_ID%>_tmp"
                                                                    src="<%=pub.FormatImgURL(Supplier_Cert, "fullpath")%>" width="120" height="120"
                                                                    onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <%if (Supplier_AuditStatus != 2)
                                                          { %>
                                                        <tr>
                                                            <td align="center" height="30" style="margin-top: 150px;">

                                                                <input type="hidden" name="supplier_cert<%=entity.Supplier_Cert_ID%>_tmp" id="supplier_cert<%=entity.Supplier_Cert_ID%>_tmp"
                                                                    value="<%=Supplier_Cert%>" />

                                                                <iframe style="margin: 40px 0 0 15px;" id="iframe_upload<%=entity.Supplier_Cert_ID%>" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shopcert&formname=frm_account_profile_edit&frmelement=supplier_cert<%=entity.Supplier_Cert_ID%>_tmp&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe>
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


                                            
                                            <tr>
                                                <td width="148"></td>
                                                <td>
                                                    <input name="action" type="hidden" id="action" value="account_profile_edit">
                                                    <input name="hidden_type" id="hidden_type" type="hidden" value="save" />
                                                    <%--<a href="javascript:void(0);" onclick="$('#hidden_type').val('save');$('#frm_account_profile').submit();" class="a11" style="display: inline-block;">保 存</a>--%>
                                                    <a href="javascript:void(0);" onclick="$('#hidden_type').val('saveandaudit');$('#frm_account_profile_edit').submit();" class="a11" style="display: inline-block; margin-left: 15px; background-image: url(../images/save_buttom.jpg);"></a>
                                                </td>
                                            </tr>


                                        </table>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="clear">
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
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
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
    <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
