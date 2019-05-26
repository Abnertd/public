<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%--<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>--%>
<%@ Register Src="~/Public/smalltop_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<%
    CMS cms = new CMS();
    Glaer.Trade.Util.Tools.ITools tools;
    tools = Glaer.Trade.Util.Tools.ToolsFactory.CreateTools();
    //Public_Class pub = new Public_Class();


    int MemberFavNum = cms.MyFavProductsNum(0);
    Cart cart = new Cart();
    Supplier supplier = new Supplier();

    //Glaer.Trade.B2C.BLL.Sys.ISysMessage sysmessage = Glaer.Trade.B2C.BLL.Sys.SysMessageFactory.CreateSysMessage();
    Member member = new Member();
    int suppier_id = 0;


    string Feedback_CompanyName = "";
    string Feedback_Address = "";
    string Feedback_Name = "";
    string Feedback_Tel = "";




    //member.Member_Login_Check("/Financial/financialservice.aspx");


    //if (member == null)
    //{
    //    Response.Redirect("/member/index.aspx");
    //}
    //MemberInfo memberinfo = member.GetMemberByID();
    //if (memberinfo != null)
    //{
    //    suppier_id = memberinfo.Member_SupplierID;
    //    SupplierInfo supplierinfo = supplier.GetSupplierByID(suppier_id);
    //    if (supplierinfo != null)
    //    {
    //        Feedback_CompanyName = supplierinfo.Supplier_CompanyName;
    //        Feedback_Address = supplierinfo.Supplier_Address;
    //        Feedback_Name = supplierinfo.Supplier_Contactman;
    //        Feedback_Tel = supplierinfo.Supplier_CorporateMobile;
    //    }

    //}


    int fianicial_type = tools.CheckInt(Request["fianicial_type"]);


    string GetFinancialServiceContent = cms.GetFinancialServiceInfoByCateID();
    Session["Position"] = "financialservice";
%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>易耐网</title>
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="/scripts/supplier.js" type="text/javascript"></script>
    <style type="text/css">
        table tr td {
            text-align: center;
        }


        .uploadify {
            position: relative;
            margin-bottom: 1em;
            margin-top: 32px;
            padding-top: 32px;
        }
    </style>
    <!--滑动门 开始-->
    <script src="/scripts/hdtab.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var SDmodel = new scrollDoor();
            SDmodel.sd(["a01", "a02", "a03", "a04"], ["aa01", "aa02", "aa03", "aa04"], "on", " ");
            SDmodel.sd(["b01", "b02"], ["bb01", "bb02"], "on", " ");
        }
    </script>
    <!--滑动门 结束-->
    <script src="/scripts/1.js"></script>
    <!--弹出菜单 start-->
    <script>
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
     <script>
         //保证金金额判断
         function check_feedback_amount(object) {
             $.ajaxSetup({ async: false });
             $("#" + object + "_tip").load("/member/register_do.aspx?action=checkfeedback_amount&val=" + $("#" + object).val() + "&timer=" + Math.random());
             //if ($("#Feedback_Amount_tip").html().indexOf("cc0000") > 0) {
             //    $("#m_Feedback_Amount").hide();
             //    return false;
             //}
             //else {
             //    $("#m_Feedback_Amount").show();
             //    pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
             //    return true;
             //}
             if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                 $("#" + object + "_div").attr('class', 'login_txt_focus2');
                 return false;
             }
             else {
                 $("#" + object + "_div").attr('class', 'login_txt2');
             }
         }



         //检查电话号码
         function check_feedback_phone(object) {
             //alert($("#Feedback_Tel").val());
             $.ajaxSetup({ async: false });
             $("#" + object + "_tip").load("/member/register_do.aspx?action=checkfeedbackphone&val=" + $("#" + object).val() + "&timer=" + Math.random());
             if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
                 $("#" + object + "_div").attr('class', 'login_txt_focus2');
                 return false;
             }
             else {
                 $("#" + object + "_div").attr('class', 'login_txt2');
             }
         }
    </script>
    <!--弹出菜单 end-->
    <!--示范一个公告层 开始-->
    <%--<script type="text/javascript">
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
    </script>--%>
    <!--示范一个公告层 结束-->
</head>
<body>
        <div class="En_top">
        <div class="En_top_main">
            <div class="En_top_main_left">
                <img src="/images/en_logo1.png" width="204" height="60" />
            </div>
            <div class="En_top_main_right">
                <ul>
                    <li <%=(Convert.ToString(Session["Position"]) == "Home" ? " class=\"on\" " :" ") %>><a href="/Index.aspx" class="a_nav">首 页</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Category" ? " class=\"on\" " :" ") %>><a href="/product/category.aspx" class="a_nav">商城选购</a></li>

                    <li <%=(Convert.ToString(Session["Position"]) == "Bid" ? " class=\"on\" " :" ") %>><a href="/bid/">招标拍卖</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "Logistics" ? " class=\"on\" " :" ") %>><a href="/Logistics/">仓储物流</a></li>
                    <li <%=(Convert.ToString(Session["Position"]) == "financialservice" ? " class=\"on\" " :" ") %>><a href="/Financial/index.aspx" class="a_nav" style="color:#ea5514">金融中心</a></li>


                    <li <%=(Convert.ToString(Session["Position"]) == "IndustryInformation" ? " class=\"on\" " :" ") %>><a href="/article/Index.aspx" class="a_nav">行情资讯</a></li>
                </ul>
            </div>
        </div>
    </div>

  <%--  <uctop:top ID="top1" runat="server" />
    <a  id="head_box"/>--%>
    <!--主体 开始-->
    <div class="content" style="display: block;">

        <div class="parte">

            <div style="height: 825px; width: 1198px; text-align: center;">

                <form name="formadd" id="formadd" method="post" action="/member/feedback_do.aspx">
                    <p style="padding: 20px; color: #ff6600; font-size: 16px;">填写相关资料，我们收到之后第一时间给您反馈，解决您的资金需求。</p>
                    <table cellpadding="0" cellspacing="0" border="0" class="b18_table" style="width: 1180px;">
                        <col width="40%" />
                        <col />

                        <tr>
                            <td style="text-align: right;">融资项目：</td>

                            <td width="801" style="margin-left: 2px;">
                                <%if (fianicial_type == 2)
                                  { %>
                                <label style="float: left;">
                                    <input type="radio" value="2" name="Feedback_Type" checked="checked" />商业承兑融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="3" name="Feedback_Type" />应收账款融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="4" name="Feedback_Type" />货押融资</label>
                                <%}
                                  else if (fianicial_type == 3)
                                  {%>
                                <label style="float: left;">
                                    <input type="radio" value="2" name="Feedback_Type" />商业承兑融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="3" name="Feedback_Type" checked="checked" />应收账款融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="4" name="Feedback_Type" />货押融资</label>

                                <%  }
                                  else if (fianicial_type == 4)
                                  { %>
                                <label style="float: left;">
                                    <input type="radio" value="2" name="Feedback_Type" />商业承兑融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="3" name="Feedback_Type" />应收账款融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="4" name="Feedback_Type" checked="checked" />货押融资</label>
                                <%}
                                  else
                                  {%>
                                <label style="float: left;">
                                    <input type="radio" value="2" name="Feedback_Type" checked="checked" />商业承兑融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="3" name="Feedback_Type" />应收账款融资</label>
                                <label style="float: left;">
                                    <input type="radio" value="4" name="Feedback_Type" />货押融资</label>

                                <%} %>
                            </td>
                        </tr>



                        <tr>
                            <td style="text-align: right;">公司名称：</td>
                            <td colspan="2" style="text-align: left;">
                                <input name="Feedback_CompanyName" type="text" id="Feedback_CompanyName" placeholder="请输入公司名称" style="width: 300px; background: none !important" class="input04" maxlength="50" value="" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">公司地址：</td>
                            <td colspan="2" style="text-align: left;">
                                <input name="Feedback_Address" type="text" id="Feedback_Address" placeholder="请输入公司地址" style="width: 300px; background: none !important" class="input04" maxlength="50" value="" /></td>
                        </tr>


                        <tr>
                            <td style="text-align: right;">联系人：</td>
                            <td colspan="2" style="text-align: left;">
                                <input name="Feedback_Name" type="text" id="Feedback_Name" maxlength="50" value="" placeholder="请输入联系人" style="width: 300px; background: none !important" class="input04" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">融资金额：</td>
                            <td colspan="2" style="text-align: left;">
                                <input name="Feedback_Amount" type="text" id="Feedback_Amount" maxlength="50" value="" placeholder="请输入融资金额" onblur="check_feedback_amount('Feedback_Amount')" style="width: 300px; background: none !important" class="input04" />  <strong class="regtip" id="Feedback_Amount_tip" style="font-weight: 500"></strong></td>
                        
                             </tr>
                        <tr>
                            <td style="text-align: right;">联系电话：</td>
                            <td colspan="2" style="text-align: left;">
                                <input name="Feedback_Tel" type="text" id="Feedback_Tel" maxlength="50" value="" onblur="check_feedback_phone('Feedback_Tel')" placeholder="请输入联系电话" style="width: 300px; background: none !important" class="input04" />   <strong class="regtip" id="Feedback_Tel_tip" style="font-weight: 500"></strong></td>

                        </tr>



                        <tr>
                            <td style="text-align: right;">验证码：</td>


                            <td style="text-align: left;">
                                <input name="verifycode" type="text" id="verifycode" class="input05" placeholder="请输入验证码" onfocus="$('#var_img').attr('src','/Public/verifycode.aspx?timer='+Math.random());" size="10" maxlength="10" /><i>*</i><img title="看不清？换一张" alt="看不清？换一张" src="/public/verifycode.aspx" style="cursor: pointer; display: inline;" id="var_img" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());" align="absmiddle" /></td>

                        </tr>



                        <tr>
                            <td style="text-align: right;">上传附件：</td>

                            <td colspan="2" style="text-align: left;">
                                <iframe src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=attachment&formname=formadd&frmelement=Feedback_Attachment&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>"
                                    width="242" height="45" frameborder="0" scrolling="no" style="margin-left: 10px;"></iframe>

                            </td>
                        </tr>


                        <tr>

                            <td colspan="2" style="text-align: center;">
                                <textarea name="feedback_content" cols="50" style="width: 588px;" class="input06" rows="5" id="feedback_content"></textarea></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-top: 20px;">
                                <input name="action" type="hidden" id="action" value="add_fin"/>
                                <input name="Feedback_Attachment" type="hidden" id="Feedback_Attachment" />
                                <a href="javascript:void(0);" onclick="$('#formadd').submit();" class="btn03" style="width: 98px;">立即融资</a></td>
                        </tr>

                    </table>
                </form>
            </div>
            <div class="clear"></div>
        </div>


    </div>
    <!--主体 结束-->
    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
    <!--右侧滚动 开始-->
    <div class="right_scroll">
        <div class="scr_info01">
            <ul>
                <li>
                    <%int cart_amount = cart.My_Cart_Count(); %>
                    <div class="li_fox"><a href="/cart/my_cart.aspx"><strong><%=cart_amount %></strong>采购清单</a></div>
                </li>
                <li>
                    <div class="li_fox"><a href="/member/member_favorites.aspx" target="_blank"><strong><%=MemberFavNum %></strong>收藏</a></div>
                </li>
                <li>
                    <div class="li_fox"><a href="/member/message_list.aspx?action=list"><strong><%=new SysMessage().GetMessageNum(1)%></strong>消息</a></div>
                </li>
                <li>
                    <div class="li_fox">
                        <a href="#head_box">
                            <img src="/images/icon23.jpg" style="width: 22px; margin: 0 auto; margin-top: 18px;"></a>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <!--右侧滚动 结束-->
</body>
</html>
