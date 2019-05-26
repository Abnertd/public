function change_maincate(target_div,obj)
{
    $("#"+target_div).load("/supplier/product_do.aspx?action=change_maincate&target="+target_div+"&cate_id=" +$("#"+ obj).val()+"&timer=" + Math.random());
}

function change_mainpurchasecate(target_div, obj) {
    $("#" + target_div).load("/supplier/shopping_do.aspx?action=change_mainpurchasecate&target=" + target_div + "&cate_id=" + $("#" + obj).val() + "&timer=" + Math.random());
}

function check_supplier_nickname(object) //1
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checknickname&val=" + encodeURIComponent($("#" + object).val()) + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_supplier_email(object) //1
{
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkemail&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("ff0000")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function check_supplier_pwd(object)//1
{
    $.ajaxSetup({async: false});
    $("#supplier_password_tip").load("/supplier/register_do.aspx?action=checkpwd&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#supplier_password_tip").html().indexOf("ff0000")>0)
    {
        $("#s_pwd_cipher").hide();
        return false;
    }
    else
    {
        $("#s_pwd_cipher").show();
        pwStrength($("#" + object).val(), 's_strength_L', 's_strength_M', 's_strength_H');
        return true;
    }
}

function check_supplier_repwd(object)//1
{
$.ajaxSetup({async: false});
$("#supplier_password_confirm_tip").load("/supplier/register_do.aspx?action=checkrepwd&val1=" + $("#supplier_password").val() + "&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#supplier_password_confirm_tip").html().indexOf("ff0000") > 0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function check_supplier_companyname(object) //1
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkcompanyname&val=" + encodeURIComponent($("#" + object).val()) + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_supplierregform()
{
    $.ajaxSetup({ async: false });
    var check_5 = check_supplier_nickname('supplier_nickname');
    var check_1=check_supplier_email('supplier_email');
    var check_2=check_supplier_pwd('supplier_password');
    var check_3=check_supplier_repwd('supplier_password_confirm');
    var check_4 = check_supplier_sms_checkcode('supplier_mobileverify', 'Supplier_Mobile');
    var check_6 = check_supplier_companyname('supplier_companyname');
    var check_7 = check_supplier_verifycode('supplier_verifycode');
    var check_8 = check_supplier_loginmobile('Supplier_Mobile');
    var check_9 = check_supplier_type('supplier_type');

    if (check_1 && check_2 && check_3 && check_4 && check_5 && check_6 && check_7 && check_8 && check_9)
    {
        return true;
    }
    else
    {
        return false;
    }
}

function check_supplier_verifycode(object)  //1
{
    $.ajaxSetup({async: false});
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkverify&val=" + $("#" + object).val()+"&timer=" + Math.random());
    if($("#" + object + "_tip").html().indexOf("ff0000")>0)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function check_supplier_mobileverify(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkmobileverify&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_supplier_type(object)
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=check_suppliertype&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("ff0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_IsBlank(object) //2
{
    $.ajaxSetup({ async: false });
    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkisblank&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_mobile(object) {

    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkmobile&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function save_ordersaccompanying()
{
    var params = $('#formadd').serialize();

    $.ajax({
        cache: false,
        type: "post",
        global: false,
        url: encodeURI("/supplier/orders_do.aspx?action=accompanyingadd"),
        data: params,
        async: false,

        success: function (msg) {
            if (msg == "success") {
                layer.msg('操作成功！', { icon: 1, time: 2000 }, function () { window.location.reload() });
            } else if (msg == "delivery_success")
            {
                layer.msg('发货成功！', { icon: 1, time: 2000 }, function () { window.location='/supplier/order_list.aspx' });
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


function Check_Ordersaccompanying_Price(price,balance)
{
    if (price > balance)
    {
        layer.msg('您输入的金额大于订单未发货金额', { icon: 2, time: 2000 }, function () { $("#Accompanying_Price").val(balance) });
    }
}


//检查登陆手机号码
function check_supplier_loginmobile(object) {

    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=checkloginmobile&val=" + $("#" + object).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

//检查短信效验码
function check_supplier_sms_checkcode(object, sign) {

    $("#" + object + "_tip").load("/supplier/register_do.aspx?action=check_sms_checkcode&val=" + $("#" + object).val() + "&sign=" + $("#" + sign).val() + "&timer=" + Math.random());
    if ($("#" + object + "_tip").html().indexOf("cc0000") > 0) {
        return false;
    }
    else {
        return true;
    }
}

function check_Supplier_Margin()
{
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/supplier_do.aspx?action=check_supplier_margin"),
        async: false,
        success: function (msg) {
            if (msg == "True") {
                location.href = "/supplier/product_add.aspx";
            } else {
                layer.confirm('您还未充值保证金，是否去充值？', {
                    btn: ['充值', '稍后'], //按钮
                    shade: true //不显示遮罩
                }, function () {
                    location.href = "/supplier/supplier_margin_account.aspx";
                }, function (index) {
                    layer.close(index);
                });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

//获取短信验证码
//function get_supplier_sms_checkcode() {
//    $.ajaxSetup({ async: false });
//    var chkmobile = check_supplier_loginmobile('Supplier_Mobile');
//    var supplier_verifycode = $("#supplier_verifycode").val();

//    if (supplier_verifycode != "") {
//        if (chkmobile) {
//            $.get(encodeURI("/supplier/register_do.aspx?action=smscheckcode&phone=" + $("#Supplier_Mobile").val() + "&supplier_verifycode=" + $("#supplier_verifycode").val() + "&t=" + Math.random()), function (data) {

//                if (data["result"] == "true") {

//                    //$("#member_mobile_tip").html('校验码已发出，请注意查收短信。');
//                    $("#btn_supplier_sms_checkcode").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#f5f5f5", "color": "#666", "border": "1px solid #dddddd" });

//                    var maxSecond = 119;
//                    var timer = setInterval(function () {
//                        if (maxSecond <= 0) {
//                            clearInterval(timer);

//                            //$("#member_mobile_tip").empty();

//                            $("#btn_supplier_sms_checkcode").bind("click", function () {
//                                get_sms_checkcode();
//                            }).html("获取短信验证码").removeAttr("style");
//                        }
//                        else {
//                            $("#btn_supplier_sms_checkcode").html(maxSecond + "秒后重新获取");
//                        }
//                        maxSecond--;
//                    }, 1000);
//                }
//                else {
//                    alert(data["msg"]);
//                }
//            }, "json");
//        }
//    }
//    else
//    {
//        alert("请输入图形验证码！");
//    }
//}

//获取短信验证码
//function getpass_supplier_sms_checkcode() {
//    $.ajaxSetup({ async: false });
//    $.get(encodeURI("/supplier/register_do.aspx?action=getpass_smscheckcode&t=" + Math.random()), function (data) {
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
//function getlogin_supplier_sms_checkcode() {
//    $.ajaxSetup({ async: false });

//    $.getJSON("/supplier/register_do.aspx?action=loginsmscheckcode&loginname=" + encodeURIComponent($("#Supplier_Email").val()) + "&t=" + Math.random(), function (data) {

//        if (data["result"] == "true") {
//            var btn = $("#login_supplier_sms_checkcode").html("119秒后重新获取").unbind("click").removeAttr("onclick").css({ "background-color": "#f5f5f5", "color": "#666", "border": "1px solid #dddddd" });

//            var maxSecond = 119;
//            var timer = setInterval(function () {
//                if (maxSecond <= 0) {
//                    clearInterval(timer);
//                    btn.bind("click", function () {
//                        get_sms_checkcode();
//                    }).html("获取短信验证码").removeAttr("style");
//                }
//                else {
//                    btn.html(maxSecond + "秒后重新获取");
//                }
//                maxSecond--;
//            }, 1000);
//        }
//        else {
//            alert(data["msg"]);
//        }
//    }, "json");
//}

function supplier_acount_profile()
{
    var params = $('#frm_account_profile').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/account_do.aspx?action=account_profile"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                 window.location.href='/supplier/supplier_cert.aspx';
            }
            else if (msg == "failure") {
                layer.msg('您的资料保存失败，请稍后再试......', { icon: 2, time: 2000 });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        }
    })
}

function shop_certsave()
{
    var params = $('#formadd').serialize();

    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/account_do.aspx?action=shop_certsave"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {              
                layer.msg('资质上传完成，请耐心等待审核通过！', { icon: 1, time: 2000 }, function () { window.location.href='/supplier/index.aspx' });
            }
            else if (msg == "failure") {
                layer.msg('资质信息保存失败，请稍后再试......', { icon: 2, time: 2000 });
            }
            else {
                layer.msg(msg, { icon:2, time: 2000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function ajax_product_del(product_id)
{
    layer.confirm('商品删除后将无法恢复，您确认要删除吗?', { icon: 3, title: '提示' }, function (index) {
        
        $.ajax({
            cache: false,
            type: "POST",
            url: encodeURI("/supplier/Product_do.aspx?action=del&product_id=" + product_id),
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    layer.msg('删除成功！', { icon: 1, time: 2000 }, function () { window.location.reload() });
                }
                else {
                    layer.msg(msg, { icon: 2, time: 2000 });
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })

        layer.close(index);
    });
}

function ajax_merchants_del(merchants_id)
{
    layer.confirm('您确认要删除吗?', { icon: 3, title: '提示' }, function (index) {

        $.ajax({
            cache: false,
            type: "POST",
            url: encodeURI("/supplier/Merchants_do.aspx?action=move&Merchants_ID=" + merchants_id),
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    layer.msg('删除成功！', { icon: 1, time: 2000 }, function () { window.location.reload() });
                }
                else {
                    layer.msg(msg, { icon: 2, time: 2000 });
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
        layer.close(index);
    });
}


function Show_PurcharseReply_Dialog(Reply_PurchaseID)
{
     layer.open({
        type: 2,
        title: '报价留言',
        shadeClose: true,
        move: false,
        shade: 0.5,
        area: ['800px', '600px'],
        content: '/supplier/merchants_do.aspx?action=show_reply_dialog&Reply_PurchaseID='+Reply_PurchaseID
    });
}

function PurchaseReply_Add()
{
    var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引

    var params = $('#frm_reply').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/merchants_do.aspx?action=purchasereply"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('报价提交成功！', { icon: 1, time: 2000 }, function () { parent.layer.close(index); });
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


function LoadProductExtends(extend_id,extend_name,extend_value) {
    var extend = "0";
    var r = $("input[name='product_extend_" + extend_id + "']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            extend += "," + r[i].value;
        }
    }

    if (extend == "0") {
        $("#extend_div").hide();
        return false;
    } else {
        $("#Product_Extends").attr('value',extend);
        $.ajax({
            cache: false,
            type: "POST",
            url: encodeURI("/supplier/product_do.aspx?action=loadextend&extendValue=" + extend + "&extendName=" + extend_name + "&timer=" + Math.random()),
            async: false,
            success: function (msg) {
                $("#extend_div").show();
                $("#extend_div").html(msg);
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}


function check_product_code(code, id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/product_do.aspx?action=check_productcode&product_code=" + code + "&id=" + id + "&timer=" + Math.random()),
        async: false,
        success: function (msg) {
            if (msg != "success") {
                layer.msg(msg, { icon: 2 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}


function Product_Extend_Append(extend,object)
{
    if ($("#product_extend_" + extend + "_" + object).prop("checked")==true) {
        $.ajax({
            cache: false,
            type: "POST",
            url: encodeURI("/supplier/product_do.aspx?action=extend_append&Extend_ID=" + object + "&timer=" + Math.random()),
            async: false,
            success: function (msg) {
                $("#table_extend").append(msg);
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
    else {
        $("#hidden_extend_status_" + object).attr("value", "hide")
        $("#tr_extend_" + object).hide();
    }
}


function product_save_next()
{
    var params = $('#formadd').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/product_do.aspx?action=check_step"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                $("#formadd").submit();
            } else {
                layer.msg(msg, { icon: 2, time: 1000 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })


}

function product_save(action)
{
    var params = $('#formadd').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/supplier/product_do.aspx?action=" + action),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                if (action == "new") {
                    layer.msg('商品信息提交成功', { icon: 1, time: 1000 }, function () { window.location = '/supplier/product_add.aspx' });
                } else {
                    layer.msg('商品信息提交成功', { icon: 1, time: 1000 }, function () { window.location = '/supplier/product_list.aspx' });
                }
            }
            else {
                layer.msg(msg, { icon: 2 });
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}