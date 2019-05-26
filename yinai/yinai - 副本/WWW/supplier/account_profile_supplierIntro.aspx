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
    int Supplier_AuditStatus = 0;
    SupplierInfo Suppliernfo = supplier.GetSupplierByID(tools.CheckInt(Session["member_id"].ToString()));
    string Member_QQ = "";
    if (Suppliernfo != null)
    {
        Supplier_AuditStatus = Suppliernfo.Supplier_AuditStatus;
    }



    MemberInfo memberInfo = member.GetMemberByID();

    MemberProfileInfo profileInfo = null;
    if (memberInfo == null)
    {
        Response.Redirect("/member/index.aspx");
    }
    else
    {
        profileInfo = memberInfo.MemberProfileInfo;

        if (profileInfo == null)
        {
            Response.Redirect("/member/index.aspx");
        }
        else
        {
            Member_QQ = profileInfo.Member_QQ;
        }
    }
    MemberSubAccountInfo subaccountinfo = member.GetSubAccountByID();

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title><%="账户信息 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
    <%--<script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>--%>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <%--<script type="text/javascript" src="js/jquery-1.5.1.min.js" charset="gb2312"></script>--%>
    <script type="text/javascript">
        function openSealUpload(openObj) {
            $("#td_sealupload").show();
            $("#iframe_sealupload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=sealimg&formname=frm_account_profile&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
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
    </style>
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
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
        <div class="content02" style="margin-bottom: 20px;">
            <div class="position">当前位置 > <a href="/member/index.aspx">我是卖家</a> > <strong>公司介绍</strong></div>
            <!--位置说明 结束-->

            <div class="partd_1">
                <div class="pd_left">
                    <% supplier.Get_Supplier_Left_HTML(2, 14); %>
                </div>

                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>公司介绍</h2>
                        <div class="blk07_sz">

                            <div class="b07_main_sz">
                                <div class="b07_info04" style="padding:0">
                                    <form name="frm_account_profile" id="frm_account_profile" method="post" action="/member/account_do.aspx">
                                        <table width="972px;" border="0" cellspacing="0" cellpadding="5" class="table_padding_5">




                                            <tr>
                                               <%-- <td width="138" class="name">公司介绍</td>--%>
                                                <td align="left">
                                                    <textarea cols="80" id="Member_Company_Introduce" name="Member_Company_Introduce" rows="16"> <%=memberInfo.Member_Company_Introduce%></textarea>
                                                    <script type="text/javascript">
                                                        var About_ContentEditor;
                                                        KindEditor.ready(function (K) {
                                                            About_ContentEditor = K.create('#Member_Company_Introduce', {
                                                                width: '972px;',
                                                                height: '358px',
                                                                filterMode: true,
                                                                afterBlur: function () { this.sync(); }
                                                            });
                                                        });
                                                    </script>
                                                </td>
                                            </tr>
                                             <tr>
                                            <td style="color:#ff6600;padding-left:10px;">说明:在商家店铺首页公司介绍位置展示</td>
                                        </tr>
                                            <tr>
                                              
                                                <td>
                                                    <input name="action" type="hidden" id="action" value="account_profile_SupplierIntro" />
                                                    <input name="hidden_type" id="hidden_type" type="hidden" value="save" />
                                                    <%--<a href="javascript:void(0);" onclick="$('#hidden_type').val('save');$('#frm_account_profile').submit();" class="a11" style="display: inline-block;">保 存</a>--%>
                                                    <a href="javascript:void(0);" onclick="$('#hidden_type').val('saveandaudit');$('#frm_account_profile').submit();" class="a11" style="display: inline-block; margin-left: 15px; background-image: url(../images/save_buttom.jpg);"></a>
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
