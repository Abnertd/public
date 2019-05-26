<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Member member = new Member();
    Addr addr = new Addr();
    Session["Cur_Position"] = "";
    member.Member_Login_Check("/member/order_address.aspx");

    int address_id;
    string Member_Address_Country, Member_Address_State, Member_Address_City, Member_Address_County, Member_Address_StreetAddress;
    string Member_Address_Zip, Member_Address_Name, Member_Address_Phone_Countrycode, Member_Address_Phone_Number, Member_Address_Mobile;
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
            address_id = 0;
        }
    }
    if (address_id == 0)
    {
        pub.Msg("error", "错误信息", "信息不存在", false, "{back}");
    }
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="编辑地址 - 我是买家 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/Supplier.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/1.js"></script>
    <script src="/scripts/Supplier.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
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
    <style type="text/css">
        .yz_blk19_main table td{ padding:5px;}
    </style>
    <script type="text/javascript">
        function Check_Supplier_Address_Form() {

            var frm_address = $('#frm_address');
            $.post("order_address_do.aspx?action=check_supplier_address_form&t=" + Math.random(), frm_address.serialize(), function(data) {
                if (data == "success") {
                    frm_address.removeAttr("onsubmit");
                    frm_address.submit();
                }
                else {
                    alert(data);
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
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            您现在的位置 > <a href="/tradeindex.aspx">首页</a> > <a href="index.aspx">会员中心</a> > <a href="order_address.aspx">
                编辑地址</a></div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->

        
        <!--会员中心主体 开始-->
        <div class="parth">
            <div class="ph_left">
                <%=member.Member_Left_HTML(4, 7) %>
            </div>
            <div class="ph_right">
                <div class="blk13">
                    <h2>编辑地址</h2>
                    <div class="yz_blk19_main">
                      <form action="/member/order_address_do.aspx" method="post" name="frm_address" id="frm_address" onsubmit="return Check_Member_Address_Form()"> 
                      <table width="100%" border="0" cellpadding="5" cellspacing="0">
                          <tr>
                            <td align="right" class="t12_53">省份：
                                <input type="hidden" id="Member_Address_State" name="Member_Address_State" value="<%=Member_Address_State %>">
                                <input type="hidden" id="Member_Address_City" name="Member_Address_City" value="<%=Member_Address_City %>">
                                <input type="hidden" id="Member_Address_County" name="Member_Address_County" value="<%=Member_Address_County %>"> 
                            </td>
                            </td>
                            <td id="div_area"><%=addr.SelectAddress("div_area", "Member_Address_State", "Member_Address_City", "Member_Address_County", Member_Address_State, Member_Address_City, Member_Address_County)%> <span class="t12_red">*</span></td>
                          </tr>
                          <tr>
                            <td></td>
                            <td><div id="tip_check_member_address_area"></div></td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53">收货地址：</td>
                            <td><input type="hidden" name="Member_Address_Country" id="Member_Address_Country" value="CN" /><input name="Member_Address_StreetAddress" class="txt_border" type="text" id="Member_Address_StreetAddress" value="<%=Member_Address_StreetAddress %>" style=" width:366px;" maxlength="100"/> <span class="t12_red">*</span></td>
                          </tr>
                          <tr>
                            <td></td>
                            <td><div id="tip_check_Member_address"></div></td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53"> 邮编：</td>
                            <td><input name="Member_Address_Zip" type="text" class="txt_border" onkeyup="if(isNaN($(this).val())){$(this).val('');}" id="Member_Address_Zip" value="<%=Member_Address_Zip %>" size="10" maxlength="10"/> <span class="t12_red">*</span></td>
                          </tr>
                            <tr>
                            <td></td>
                            <td><div id="tip_check_zipcode"></div></td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53">收货人姓名：</td>
                            <td><input name="Member_Address_Name" type="text" class="txt_border" id="Member_Address_Name" size="20" value="<%=Member_Address_Name %>" maxlength="50"/> <span class="t12_red">*</span></td>
                          </tr>
                           <tr>
                            <td></td>
                            <td><div id="tip_check_name"></div></td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53">联系电话：</td>
                            <td><input name="Member_Address_Phone_Countrycode" type="hidden" id="Member_Address_Phone_Countrycode" value="+86" />
                                
                              <input name="Member_Address_Phone_Number" type="text" class="txt_border" id="Member_Address_Phone_Number" value="<%=Member_Address_Phone_Number %>" size="20" maxlength="20" /> <span class="t12_red">*</span>
                              </td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53">手机：</td>
                            <td><input name="Member_Address_Mobile" type="text" class="txt_border" onkeyup="if(isNaN($(this).val())){$(this).val('');}" id="Member_Address_Mobile" size="20" value="<%=Member_Address_Mobile %>" maxlength="20"/> 电话与手机至少填写一项<span class="t12_red">*</span></td>
                          </tr>
                           <tr>
                            <td></td>
                            <td><div id="tip_check_mobile"></div></td>
                          </tr>
                          <tr>
                            <td align="right" class="t12_53">&nbsp;</td>
                            <td>
                              <input name="btn_confirm" type="image" src="/images/save.jpg" align="absmiddle"/> 
                              <input type="hidden" name="action" id="action" value="address_edit" />
                              <input type="hidden" name="Member_Address_ID" id="" value="<%=address_id %>" />
			                  <span class="t12_red">请将带*信息填写完整</span>
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
    
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
