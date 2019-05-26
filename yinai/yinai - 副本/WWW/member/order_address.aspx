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
    int member_address_id = tools.CheckInt(Request["member_address_id"]);
    Session["Cur_Position"] = "";
    member.Member_Login_Check("/member/order_address.aspx");

    int address_id;
    string Member_Address_Country, Member_Address_State, Member_Address_City, Member_Address_County, Member_Address_StreetAddress;
    string Member_Address_Zip, Member_Address_Name, Member_Address_Phone_Countrycode, Member_Address_Phone_Number, Member_Address_Mobile;

    string action = "address_add";
    
    Member_Address_Country = "";
    Member_Address_State = "";
    Member_Address_City = "";
    Member_Address_County = "";
    Member_Address_StreetAddress = "";
    Member_Address_Zip = "";
    Member_Address_Name = "";
    Member_Address_Phone_Countrycode = "";
    Member_Address_Phone_Number = "";
    Member_Address_Mobile = "";

    address_id = tools.CheckInt(tools.NullStr(Request.QueryString["member_address_id"]));
    if (address_id > 0)
    {
        MemberAddressInfo address = member.GetMemberAddressByID(address_id);
        if (address != null)
        {
            if (address.Member_Address_MemberID == tools.CheckInt(Session["member_id"].ToString()))
            {
                action = "address_edit";
                Member_Address_Country = address.Member_Address_Country;
                Member_Address_State = address.Member_Address_State;
                Member_Address_City = address.Member_Address_City;
                Member_Address_County = address.Member_Address_County;
                Member_Address_StreetAddress = address.Member_Address_StreetAddress;
                Member_Address_Zip = address.Member_Address_Zip;
                Member_Address_Name = address.Member_Address_Name;
                Member_Address_Phone_Countrycode = address.Member_Address_Phone_Countrycode;
                Member_Address_Phone_Number = address.Member_Address_Phone_Number;
                Member_Address_Mobile = address.Member_Address_Mobile;
            }
            else
            {
                address_id = 0;
            }
        }
        else
        {
            action = "address_add";
            address_id = 0;
        }
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="收货地址管理 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

     <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
<%--    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/cart.js"></script>
<%--      <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
 --%>
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
    <style type="text/css">
        .main table td {
            padding: 5px;
        }

       
    </style>
    <script type="text/javascript">
        function Check_Member_Address_Form() {

            var frm_address = $('#frm_address');
            $.post("order_address_do.aspx?action=check_supplier_address_form&t=" + Math.random(), frm_address.serialize(), function (data) {
                if (data == "success") {
                    frm_address.removeAttr("onsubmit");
                    frm_address.submit();
                }
                else {
                    layer.msg(data, {icon:2, time: 2000});
                }
                frm_address = null;
            });

            return false;
        }
    </script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="webwrap">
    <div class="content02">
      
            <!--位置说明 开始-->
           <div class="position">当前位置 > <a href="/member/index.aspx">我是买家</a> > <strong>收货地址管理</strong></div>
            <!--位置说明 结束-->

            <!--会员中心主体 开始-->
            <div class="partd_1">
                <div class="pd_left">                             
                        <%=member.Member_Left_HTML(5,5) %>                                         
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top:0px;">
                    <h2>收货地址管理</h2>

                     <div class="b14_1_main" >
                        <%member.Member_Address();%>
                    </div>

                    <div class="blk17_sz">
                        <form action="/member/order_address_do.aspx" method="post" name="frm_address" id="frm_address" onsubmit="return Check_Member_Address_Form()">
                            <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td width="92" class="name">区域：
                                <input type="hidden" id="Member_Address_State" name="Member_Address_State" value="<%=Member_Address_State %>" />
                                        <input type="hidden" id="Member_Address_City" name="Member_Address_City" value="<%=Member_Address_City %>" />
                                        <input type="hidden" id="Member_Address_County" name="Member_Address_County" value="<%=Member_Address_County %>" />
                                    </td>
                                    <td id="div_area"><%=addr.SelectAddressNew("div_area", "Member_Address_State", "Member_Address_City", "Member_Address_County", Member_Address_State, Member_Address_City, Member_Address_County)%> <i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">收货地址：</td>
                                    <td width="801">
                                        <input type="hidden" name="Member_Address_Country" id="Member_Address_Country" value="CN" />
                                        <input name="Member_Address_StreetAddress" type="text" id="Member_Address_StreetAddress" style="width: 366px;" maxlength="100" value="<%=Member_Address_StreetAddress %>" />
                                        <i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">邮编：</td>
                                    <td width="801">
                                        <input name="Member_Address_Zip" type="text" onkeyup="if(isNaN($(this).val())){$(this).val('');}" id="Member_Address_Zip" value="<%=Member_Address_Zip %>" maxlength="10" />
                                        <i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">收货人姓名：</td>
                                    <td width="801">
                                        <input name="Member_Address_Name" type="text" id="Member_Address_Name" value="<%=Member_Address_Name %>"  maxlength="50" />
                                        <i>*</i></td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">联系电话：</td>
                                    <td width="801">
                                        <input name="Member_Address_Phone_Countrycode" type="hidden" class="txt_border" id="Member_Address_Phone_Countrycode" value="+86" />

                                        <input name="Member_Address_Phone_Number" type="text" id="Member_Address_Phone_Number"  maxlength="20" value="<%=Member_Address_Phone_Number %>" />
                                        <i>*</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">手机：</td>
                                    <td width="801">
                                        <input name="Member_Address_Mobile" type="text" onkeyup="if(isNaN($(this).val())){$(this).val('');}" id="Member_Address_Mobile" value="<%=Member_Address_Mobile %>" maxlength="20" />
                                    <%--    <i>*</i><em>电话与手机至少填写一项</em></td>--%>
                                            <i>*</i><em style="font-style:normal;margin-left:9px;">电话与手机至少填写一项</em></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div id="tip_check_mobile"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="92" class="name">&nbsp;</td>
                                    <td width="801">
                                        <a href="javascript:void(0);" onclick="$('#frm_address').submit();" class="a11"></a>
                                        <input type="hidden" name="action" id="action" value="<%=action %>" />
                                        <input type="hidden" name="Member_Address_ID"  id="Member_Address_ID" value="<%=member_address_id %>"  />
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
                        </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <!--会员中心主体 结束-->
        </div>
    </div>
    <!--主体 结束-->

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

  <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
