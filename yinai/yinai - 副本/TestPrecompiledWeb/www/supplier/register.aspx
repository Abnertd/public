<%@ Page Language="C#" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Session["Home_Position"] = Session["Cur_Position"] = "";
    Public_Class pub = new Public_Class();
    if (Session["member_logined"] == "True")
    {
        Response.Redirect("/member/index.aspx");
    }
    if (Session["supplier_logined"] == "True")
    {
        Response.Redirect("/supplier/index.aspx");
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="会员注册 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script> 
    <style type="text/css">
        .regtip{ margin-top:12px; color:#FFFfff; font-weight:normal; padding-left:5px; line-height:30px;}
        
        .regtab { height:30px; padding-left:350px; border-bottom:solid 2px #339933; margin-top:5px; }
        .regtab li{ float:left; margin-left:10px; width:100px; background-color:#ececec; height:30px; line-height:30px; text-align:center; cursor:pointer;}
        .regtab li.on{font-weight:bold; background-color:#339933; color:#fff;}
    </style>
</head>
<body style="background-color:#cacaca;">
       <div class="web_box web_box02">
             <div class="lr">
                   <div class="lr_head">
                        <div class="lr_logo"><a href="/" ><img src="/images/logo02.jpg"></a></div>
                        <div class="lr_tel"><img src="/images/logo03.jpg"></div>
                        <div class="clear"></div>
                   </div>
                   <div class="lr_co">
                         <h2><span>我已是会员<a href="login.aspx">[立即登录]</a></span><img src="/images/h2_tit02.jpg"></h2>
                         <div class="lr_main02">
                          <form name="regform" id="regform" action="/supplier/register_do.aspx" method="post" onsubmit="return check_supplierregform();">
                               <ul>
                                    <li><span>用户名：</span><label>
                                   <input type="text" name="supplier_nickname" id="supplier_nickname" onblur="check_supplier_nickname('supplier_nickname');" class="input11"/>
                                    </label>
                                <strong class="regtip" id="supplier_nickname_tip">4-20位字符，可由中文、英文、数字及_、-组成</strong>
                                    </li><div class="clear"></div>

                                    <li><span>密  码：</span><label>
                                    <input type="password" name="supplier_password" id="supplier_password" class="input11" onblur="check_supplier_pwd('supplier_password');" />
                                   </label>
                                    <strong class="regtip" id="supplier_password_tip">请输入6～20位密码（A-Z，a-z，0-9，不要输入空格）</strong>
                                    </li><div class="clear"></div>
                                    <li><span>确认密码：</span><label>
                                    <input type="password" name="supplier_password_confirm" id="supplier_password_confirm" class="input11" onblur="check_supplier_repwd('supplier_password_confirm');" />
                                    </label><strong class="regtip" id="supplier_password_confirm_tip">请再次输入密码</strong>
                                    </li><div class="clear"></div>
                                     
                                    <li><span>E-mail：</span><label>
                                    <input type="text" name="supplier_email" id="supplier_email" onblur="check_supplier_email('supplier_email');" class="input11" />
                                    </label>
                                     <strong class="regtip" id="supplier_email_tip">请使用一个有效的邮箱</strong>
                                    </li>
                                    <div class="clear"></div>
                                    <li><span>单位名称：</span><label>
                                    <input type="text" name="supplier_companyname" id="supplier_companyname" onblur="check_supplier_companyname('supplier_companyname');" class="input11" />
                                    </label>
                                     <strong class="regtip" id="supplier_companyname_tip"></strong>
                                    </li>
                                    <div class="clear"></div>
                                    <li><span>联系人姓名：</span><label>
                                    <input type="text" name="Supplier_Contactman" id="Supplier_Contactman" onblur="check_IsBlank('Supplier_Contactman');" class="input11" />
                                    </label>
                                     <strong class="regtip" id="Supplier_Contactman_tip"></strong>
                                    </li>
                                    <div class="clear"></div>
                                    <li><span>联系人手机：</span><label>
                                    <input type="text" name="Supplier_Mobile" id="Supplier_Mobile" onblur="check_mobile('Supplier_Mobile');" class="input11" />
                                    </label>
                                     <strong class="regtip" id="Supplier_Mobile_tip"></strong>
                                    </li>
                                    <div class="clear"></div>

                                    <li><span>验证码：</span><label style=" width:165px;">
                                    <input type="text" name="verifycode" id="verifycode" style="width:150px;" onblur="check_supplier_verifycode('verifycode');" />
                                    </label>
                                    
                                    <img src="/Public/verifycode.aspx" id="var_img" align="absmiddle" style="width:65px; height:27px;"/>
                                    <a href="javascript:void(0);" onclick="$('#var_img').attr('src','../Public/verifycode.aspx?timer='+Math.random());">看不清？换一张</a>
                                    <strong class="regtip" id="verifycode_tip"></strong>
                            </li>
                            <div class="clear"></div>
                                    <li style=" padding:0 0 0 66px;"><input name="checkbox_agreement" tabindex="8" value="1" type="checkbox" id="checkbox2" checked="checked" />我已经阅读并同意 <a href="/about/index.aspx?sign=regprotocal" target="_blank">《用户协议》</a></li>
                                    <li style=" padding:0 0 0 70px;"> 
                                    <a href="javascript:void(0);" onclick="$('#regform').submit();" class="a08">提交注册</a></li>
                               </ul>
                               <input type="hidden" name="action" value="basicinfo" />
                                </form>
                         </div>
                   </div>
                   <div class="lr_bo">
                         <p>版权所有：易耐产业金服 京ICP备11020457号</p>
                         <span>版权所有：易耐产业金服 京ICP备11020457号</span>
                   </div>
             </div>
       </div>
 
    
</body>
</html>
