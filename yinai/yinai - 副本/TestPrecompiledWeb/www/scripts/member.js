//检查用户名
function check_member_nickname(object) //1
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checknickname&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        $("#" + object + "_div").attr('class', 'login_txt_focus2');
        return false;
    }
    else {
        $("#" + object + "_div").attr('class', 'login_txt2');
        return true;
    }
}


//检查邮箱
function check_member_email(object) 
{
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkemail&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        $("#" + object+"_div").attr('class','login_txt_focus2');
        return false;
    }
    else
    {
        $("#"+object+"_div").attr('class','login_txt2');
        return true;
    }
}

//检查密码
function check_member_pwd(object)
{
    $.ajaxSetup({async: false});
    $("#member_password_tip").load("/member/register_do.aspx?action=checkpwd&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#member_password_tip").html().indexOf("cc0000")>0)
    {
        $("#m_pwd_cipher").hide();
        return false;
    }
    else
    {
        $("#m_pwd_cipher").show();
        pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
        return true;
    }
}
//检查用户名
function check_real_name(object) {
    $.ajaxSetup({ async: false });
    $("#member_realname_tip").load("/member/register_do.aspx?action=checkrealname&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#member_realname_tip").html().indexOf("cc0000") > 0) {
        $("#m_real_name").hide();
        return false;
    }
    else {
        $("#m_real_name").show();
        pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
        return true;
    }
}


//检查用户名
function check_real_suppliername(object) {
    $.ajaxSetup({ async: false });
    $("#check_real_suppliername_tip").load("/member/register_do.aspx?action=checkrealsuppliername&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#check_real_suppliername_tip").html().indexOf("cc0000") > 0) {
        $("#m_member_company").hide();
        return false;
    }
    else {
        $("#m_member_company").show();
        pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
        return true;
    }
}



//检查用户名真实姓名  即 memberprofile里面联系人
function check_Member_Profile_Contact(object) {
    $.ajaxSetup({ async: false });
    $("#check_Member_Profile_Contact_tip").load("/member/register_do.aspx?action=checkMember_Profile_Contact&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#check_Member_Profile_Contact_tip").html().indexOf("cc0000") > 0) {
        $("#m_Member_Profile_Contact").hide();
        return false;
    }
    else {
        $("#m_Member_Profile_Contact").show();
        pwStrength($("#" + object).val(), 'strength_L', 'strength_M', 'strength_H');
        return true;
    }
}






//再次检查密码
function check_member_repwd(object)
{
$.ajaxSetup({async: false});
    $("#member_password_confirm_tip").load("/member/register_do.aspx?action=checkrepwd&val1="+$("#member_password").val()+"&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#member_password_confirm_tip").html().indexOf("cc0000")>0)
    {
    $("#member_password_confirm_div").attr('class','login_txt_focus2');
        return false;
    }
    else
    {
    $("#member_password_confirm_div").attr('class','login_txt2');
        return true;
    }
}
//Check_DriverMobile
//检查手机号
function check_member_mobile(object)
{

    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkmobile&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

//检查手机号
function check_driver_mobile(object) {

    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkdrivermobile&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

//检查电话号码
function check_member_phone(object)
{
    //alert($("#Feedback_Tel").val());
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkphone&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        $("#" + object+"_div").attr('class','login_txt_focus2');
        return false;
    }
    else
    {
        $("#"+object+"_div").attr('class','login_txt2');
        return true;
    }
}


//检查电话号码
function check_member_phone(object) {
    //alert($("#Feedback_Tel").val());
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkphone&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        $("#" + object + "_div").attr('class', 'login_txt_focus2');
        return false;
    }
    else {
        $("#" + object + "_div").attr('class', 'login_txt2');
        return true;
    }
}


////检查电话号码
//function check_feedback_phone(object) {
//    //alert($("#Feedback_Tel").val());
//    $.ajaxSetup({ async: false });
//    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkfeedbackphone&val=" + $("#" + object).val() + "&timer=" + Math.random());
//    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
//        $("#" + object + "_div").attr('class', 'login_txt_focus2');
//        return false;
//    }
//    else {
//        $("#" + object + "_div").attr('class', 'login_txt2');
//    }
//}

//检查电话号码
//function check_feedback_amount(object) {
//    //alert($("#Feedback_Tel").val());
//    $.ajaxSetup({ async: false });
//    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkfeedamount&val=" + $("#" + object).val() + "&timer=" + Math.random());
//    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
//        $("#" + object + "_div").attr('class', 'login_txt_focus2');
//        return false;
//    }
//    else {
//        $("#" + object + "_div").attr('class', 'login_txt2');
//    }
//}


//检查验证码
function check_member_verifycode(object)
{
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkverify&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function check_member_mobileverifycode(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkmobileverify&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_member_type(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkmembertype&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

//检查公司名称
function check_member_company(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkcompany&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        $("#" + object + "_div").attr('class', 'login_txt_focus2');
        return false;
    }
    else {
        $("#" + object + "_div").attr('class', 'login_txt2');
        return true;
    }
}

//检查联系人
function check_member_realname(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=contactname&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        $("#" + object + "_div").attr('class', 'login_txt_focus2');
        return false;
    }
    else {
        $("#" + object + "_div").attr('class', 'login_txt2');
        return true;
    }
}

//检查注册地址
function check_member_address(object)
{
    var obj_state = $("#member_state").val();
    var obj_city = $("#member_city").val();
    var obj_county = $("#member_county").val();

    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/register_do.aspx?action=address&state=" + obj_state + "&city=" + obj_city + "&county=" + obj_county + "&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        $("#" + object + "_div").attr('class', 'login_txt_focus2');
        return false;
    }
    else {
        $("#" + object + "_div").attr('class', 'login_txt2');
        return true;
    }
}

function check_member_checkprotocal(object)
{
    if(MM_findObj(object).checked)
    {
        $("#" + object + "_tip").load("/member/register_do.aspx?action=checkprotocal&val=1&timer=" + Math.random());
    }
    else
    {
        $("#" + object + "_tip").load("/member/register_do.aspx?action=checkprotocal&val=0&timer=" + Math.random());
    }
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function check_member_IsBlank(object,success,error) 
{
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkisblank&success="+success+"&error="+error+"&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("cc0000")>0)
    {
        $("#" + object+"_div").attr('class','login_txt_focus2');
        return false;
    }
    else
    {
        $("#"+object+"_div").attr('class','login_txt2');
        return true;
    }
}

//验证select
function check_select(object)
{
    var s= $("#"+object).value;
    alert(s);
}

function check_regform()
{
    $.ajaxSetup({async: false});
    var check_1=check_member_nickname('member_nickname');
    var check_2=check_member_email('member_email');
    var check_3=check_member_pwd('member_password');
    var check_4=check_member_repwd('member_password_confirm');
    var check_5 = check_member_verifycode('verifycode');
    var check_6 = check_member_company('member_company');
    //var check_7 = check_member_realname('member_realname');
    //var check_8 = check_member_address('member_address');
    var check_9 = check_member_loginmobile('member_mobile');
    var check_10 = check_member_type('member_type');

    var check_smscheckcode = check_sms_checkcode('mobile_verifycode', 'member_mobile');

    if (check_1 && check_2 && check_3 && check_4 && check_5 && check_6 && check_9 && check_smscheckcode) {
        return true;
    }
    else {
        return false;
    }
}

function check_regform_company()
{
    $.ajaxSetup({async: false});
    var check_1=check_member_nickname('member_nickname');
    var check_2=check_member_email('member_email');
    var check_3=check_member_pwd('member_password');
    var check_4=check_member_repwd('member_password_confirm');
    var check_5=check_member_verifycode('verifycode');
    var check_6=check_member_phone('Member_Phone_Number');
    var check_7=check_member_IsBlank('Member_Name','','');
    var check_8=check_member_email('member_email');
    var check_9=check_member_IsBlank('U_Member_CompanyName','','');
    if(check_1&&check_2&&check_3&&check_4&&check_5&&check_6&&check_7&&check_8&&check_9)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//登录
function MemberLogin() {
    var member_name = $('#textfield2').val();
    var member_password = $('#member_password11').val();
    var chk_UserName = $('#chk_UserName').val();
    var Trade_Verify = $('#Trade_Verify').val();
    var userremember = $('#userremember:checked').val();
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "json",
        url: encodeURI("/member/login_do.aspx?action=login&member_name=" + member_name + "&member_password=" + member_password + "&chk_UserName=" + chk_UserName + "&userremember=" + userremember + "&Trade_Verify=" + Trade_Verify + "&timer=" + Math.random()),
        success: function (data) {
            if (true) {

            }
            if (data.indexOf("成功") != -1) {
                data = data.toString().replace("成功", "");
                window.location.replace(data);
            }
            else {
                $('#var_img').attr('src', '/public/verifycode.aspx?timer=' + Math.random());
                $('#td_msg').html("<table border='0' cellspacing='2' cellpadding='0'><tr><td style=\"color:#cc0000;\"><img style=\"width:11px; height:11px;display:inline;border:0;\"  src='/images/tip-error.gif' hspace='5' align='absmiddle'>" + data + "&nbsp;&nbsp;</td></tr></table>");
            }
        },
        error: function() {
            alert("Error Script");
        }
    });
}

function Fast_MemberLogin() {
    var member_name = $('#UserName').val();
    if (member_name == "" || member_name == "手机/邮箱/用户名") {
        layer.tips('请输入用户名', '#UserName', {
            tips: 1
        });
        return false;
    }

    var member_password = $('#Password').val();
    if (member_password == "" || member_password == "密码") {
        layer.tips('请输入登录密码', '#Password', {
            tips: 1
        });
        return false;
    }

    $.ajax({
        type: "post",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/member/login_do.aspx?action=fastlogin&Member_UserName=" + member_name + "&Member_Password=" + member_password + "&timer=" + Math.random()),
        success: function (msg) {
            if (msg == "Failure") {

                $.ajax({
                    type: "post",
                    global: false,
                    async: false,
                    dataType: "html",
                    url: encodeURI("/supplier/login_do.aspx?action=fast_login&Supplier_Email=" + member_name + "&Supplier_Password=" + member_password + "&timer=" + Math.random()),
                    success: function (msg) {
                        if (msg == "Failure") {
                            layer.tips('登录失败,请检查用户名、密码', '#subimtBtn', {
                                tips: 1
                            });
                        } else {
                            location.href = msg;
                        }
                    }
                });
            } else {
                location.href = msg;
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

//删除订单
function DeleteOrder(orderid)
{
    $.ajax({
		type: "get", 
		global: false, 
		async: false,
		dataType: "html",
		url: encodeURI("/member/order_do.aspx?action=delorder&OrderID="+orderid+"&timer="+Math.random()),		
		success:function(data){		
		    alert(data);
		    history.go(0);
		},
		error: function (){
			alert("Error Script");
		}
    });
}

function showPart(thisobj, oid) {
    var imgobj = $(thisobj);

    if (imgobj.attr("src") == "/images/tb_23.jpg") {
        for (var ii = 1; ii <= 100; ii++) {
            $("#tr_sub_" + oid + "_" + ii).show();
        }
        imgobj.attr("src", "/images/tb_22.jpg");
    }
    else {
        for (var ii = 1; ii <= 100; ii++) {
            $("#tr_sub_" + oid + "_" + ii).hide();
        }
        imgobj.attr("src", "/images/tb_23.jpg");
    }
    
}

function check_profileform() {
    $.ajaxSetup({ async: false });
    var check_1 = check_NickName();
    var check_2 = check_Email();


    if (check_1 && check_2) {
        return true;
    }
    else {
        return false;
    }

}


//检查登陆手机号码
function check_member_loginmobile(object) {

    $("#" + object + "_tip").load("/member/register_do.aspx?action=checkloginmobile&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

//检查短信效验码
function check_sms_checkcode(object, sign) {
   
    $("#" + object + "_tip").load("/member/register_do.aspx?action=check_sms_checkcode&val=" + $("#" + object).val() + "&sign=" + $("#" + sign).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

//获取短信验证码
function get_sms_checkcode() {
    $.ajaxSetup({ async: false });
    var chkmobile = check_member_loginmobile('member_mobile');
    var verifycode = $("#verifycode").val();

    if (verifycode != "") {
        if (chkmobile) {
            $.get(encodeURI("/member/register_do.aspx?action=smscheckcode&phone=" + $("#member_mobile").val() + "&verifycode=" + $("#verifycode").val() + "&t=" + Math.random()), function (data) {

                if (data["result"] == "true") {

                    //$("#member_mobile_tip").html('校验码已发出，请注意查收短信。');
                    $("#btn_sms_checkcode").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#f5f5f5", "color": "#666", "border": "1px solid #dddddd" });

                    var maxSecond = 119;
                    var timer = setInterval(function () {
                        if (maxSecond <= 0) {
                            clearInterval(timer);

                            //$("#member_mobile_tip").empty();

                            $("#btn_sms_checkcode").bind("click", function () {
                                get_sms_checkcode();
                            }).html("获取短信验证码").removeAttr("style");
                        }
                        else {
                            $("#btn_sms_checkcode").html(maxSecond + "秒后重新获取");
                        }
                        maxSecond--;
                    }, 1000);
                }
                else {
                    alert(data["msg"]);
                }
            }, "json");
        }
    }
    else
    {
        alert("请输入图形验证码！");
    }
}

//获取短信验证码
//function getpass_sms_checkcode() {
//    $.ajaxSetup({ async: false });
//    $.get(encodeURI("/member/register_do.aspx?action=getpass_smscheckcode&t=" + Math.random()), function (data) {
//        if (data["result"] == "true") {
//            //$("#div_sms_checkcode").text(data["msg"]);
//            $("#smscheckcode_tip").html('校验码已发出，请注意查收短信。如果在<span id="sms_timer">120</span>秒内没有收到验证码，请重新验证');

//            $("#btn_sms_checkcode").unbind("click").removeAttr("onclick");
//            setTimeout(function () {
//                $("#btn_sms_checkcode").bind("click", function () {
//                    getpass_sms_checkcode();
//                });
//            }, 120000);

//            var timer = setInterval(function () {
//                if (parseInt($("#sms_timer").text()) <= 0) {
//                    clearInterval(timer);
//                    $("#verifycode_tip").empty();
//                }
//                else {
//                    $("#sms_timer").text(parseInt($("#sms_timer").text()) - 1);
//                }
//            }, 1000);
//        }
//        else {
//            alert(data["msg"]);
//        }
//    }, "json");

//}

//获取登录短信验证码
function getlogin_sms_checkcode() {
    $.ajaxSetup({ async: false });

    $.getJSON("/member/register_do.aspx?action=loginsmscheckcode&loginname=" + encodeURIComponent($("#Member_UserName").val()) + "&t=" + Math.random(), function (data) {

        if (data["result"] == "true") {
            var btn = $("#login_sms_checkcode").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#f5f5f5", "color": "#666", "border": "1px solid #dddddd" });

            var maxSecond = 119;
            var timer = setInterval(function () {
                if (maxSecond <= 0) {
                    clearInterval(timer);
                    btn.bind("click", function () {
                        get_sms_checkcode();
                    }).html("获取短信验证码").removeAttr("style");
                }
                else {
                    btn.html(maxSecond + "秒后重新获取");
                }
                maxSecond--;
            }, 1000);
        }
        else {
            alert(data["msg"]);
        }
    }, "json");
}


function Show_MerchantsReply_Dialog(merchants_id)
{
    layer.open({
        type: 2,
        title: '品牌加盟留言',
        shadeClose: true,
        move: false,
        shade: 0.5,
        area: ['800px', '550px'],
        content: '/member/account_do.aspx?action=show_reply_dialog&Message_MerchantsID=' + merchants_id
    });
}

function MerchantsReply_Add()
{
    var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引

    var params = $('#frm_reply').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/account_do.aspx?action=merchantsreply"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('加盟留言提交成功！', { icon: 1, time: 2000 }, function () { parent.layer.close(index); });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function orders_accept(orders_id)
{
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/orders_do.aspx?action=orderaccept"),
        data: "Orders_ID=" + orders_id,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('订单签收成功！', { icon: 1, time: 2000 }, function () { window.location.reload(); });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function orders_evaluate()
{
    var params = $('#frm_add').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/orders_do.aspx?action=review_add"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('操作成功！', { icon: 1, time: 2000 }, function () { window.location.href = '/member/order_list.aspx' });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}



//登录
function MemberLogin() {
    var member_name = $('#Member_UserName').val();
    var member_password = $('#Member_Password').val();
    //var chk_UserName = $('#Member_UserName').val();
    var verifycode = $("#verifycode").val();

    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/member/login_do.aspx?action=login&Member_UserName=" + member_name + "&Member_Password=" + member_password + "&verifycode=" + verifycode + "&timer=" + Math.random()),
        success: function (data) {
            if (data.indexOf("成功") != -1) {
                data = data.toString().replace("成功", "");
                window.location.replace(data);
            }
            else {
                //$('#td_msg').html(data);
                // $('#tr_logintype').show();

                alert(data);

                $('#var_img11').attr('src', '/public/verifycode.aspx?timer=' + Math.random());
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function erp_binding()
{
    var params = $("#frm_binding").serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/account_do.aspx?action=erp_binding"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('ERP用户绑定成功！', { icon: 1, time: 2000 }, function () { location.href='/member/' });
            }
            else
            {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}


function member_loan_apply(orders_sn)
{
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/credit_do.aspx?action=loan_apply&orders_sn="+orders_sn),
        async: false,
        success: function (msg) {
            if (msg == "T") {
                layer.msg('贷款申请成功！', { icon: 1, time: 2000 });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}




function check_loginform() {

    $.ajaxSetup({ async: false });
  
    var check_1 = check_member_loginname('Member_UserName');
   
    //var check_2 = check_member_loginPwd('Member_Password', 'Member_UserName');
    var check_2 = check_member_loginPwd('Member_Password');
    var check_3 = check_member_verifycode('verifycode');
    if (check_1 && check_2 && check_3 ) {
        return true;
    }
    else {
        return false;
    }
}

function check_member_loginname(object) {
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/login_do.aspx?action=checknickname&val=" + encodeURI($("#" + object).val()) + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}



//function check_member_loginPwd(object, object1) {
  
//    //alert($("#" + object1).val());
//    $.ajaxSetup({ async: false });
//    $("#" + object + "_tip").load("/member/login_do.aspx?action=checkpwd&val=" + encodeURI($("#" + object).val()) + "&val1=" + encodeURI($("#" + object1).val()) + "&timer=" + Math.random());
//    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
//        return false;
//    }
//    else {
//        return true;
//    }
//}

function check_member_loginPwd(object) {
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/login_do.aspx?action=checkpwd&val=" + encodeURI($("#" + object).val()) + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_member_verifycode(object) {
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/member/login_do.aspx?action=checkverify&val=" + encodeURI($("#" + object).val()) + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}


function get_sms_checkcodepwd() {
    $.ajaxSetup({ async: false });
    //var chkverifycode = check_member_verifycode('verifycode');
    var chkmobile = check_member_loginmobilepwd('member_phone');
    if (chkmobile) {

        $.ajax({
            type: "GET",

            async: false,
            url: "/member/login_do.aspx?action=smscheckcode&phone=" + $("#member_phone").val() + "&t=" + Math.random(),
            data: "{}",
            dataType: "json",
            success: function (data) {

                if (data.err != "") {

                    layer.msg(data.err);

                } else {

                    $("#btn_sms_checkcode").html('重新发送(<span id="sms_timer">60</span>)');

                    $("#btn_sms_checkcode").unbind("click").removeAttr("onclick");
                    setTimeout(function () {
                        $("#btn_sms_checkcode").bind("click", function () {
                            get_sms_checkcode();
                        });
                    }, 60000);

                    var timer = setInterval(function () {
                        if (parseInt($("#sms_timer").text()) <= 0) {
                            clearInterval(timer);
                            $("#btn_sms_checkcode").html('获取验证码');

                            //刷新验证码
                            //                            $('#var_img').attr('src', '/public/verifycode.aspx?timer=' + Math.random());
                            //                            $("#verifycode").val('');
                        }
                        else {
                            $("#sms_timer").text(parseInt($("#sms_timer").text()) - 1);
                        }
                    }, 1000);
                }
            }

        });

    }


}