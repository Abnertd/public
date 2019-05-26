function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

/*预加载图片*/
function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

/*选项卡切换*/
function Set_Tab(tab_name, tab_no, tab_all, class_hove, class_out) {
    var i;
    for (i = 1; i <= tab_all; i++) {
        if (i == tab_no) {
            $("#" + tab_name + "_" + i).attr("class", class_hove);
            $("#" + tab_name + "_" + i + "_content").show();
        }
        else {
            $("#" + tab_name + "_" + i).attr("class", class_out);
            $("#" + tab_name + "_" + i + "_content").hide();
        }
    }
}

/*选项卡切换*/
function Cert_Set_Tab(tab_name, tab_no, tab_all) {
    var i;
    for (i = 1; i <= tab_all; i++) {
        if (i == tab_no) {
            $("#" + tab_name + "_" + i + "").show();
        }
        else {
            $("#" + tab_name + "_" + i + "").hide();
        }
    }
}

/*可控制左右无缝循环（需引用MSClass.js）*/
function srcoll_left_right_Control(control_enabel, direction, left_div, right_div, scroll_body, scroll_content, total_width, total_height, scroll_width, scroll_speed, wait_time) {
    /************control_enabel：是否启用按钮控制ID************/
    /************direction：滚动方向：0上 1下 2左 3右************/
    /************left_div：左控制按钮ID************/
    /************right_div：右控制按钮ID************/
    /************scroll_body：循环主体容器ID************/
    /************scroll_content：循环主体内容容器ID************/
    /************total_width：循环体总宽度************/
    /************total_height：循环体总高度************/
    /************scroll_width：每次循环宽度（0为翻屏）************/
    /************scroll_speed：循环速度步长（越大越慢）************/
    /************wait_time：翻屏等待时间************/
    var MarqueeDivControl = new Marquee([scroll_body, scroll_content], direction, 0.2, total_width, total_height, scroll_speed, wait_time, 3000, scroll_width);
    if (control_enabel == true) {
        $("#" + left_div).click(function () { MarqueeDivControl.Run(3); });
        $("#" + right_div).click(function () { MarqueeDivControl.Run(2); });
    }
}
function Mouse_Scroll_AD(action, titobj_type, conobj_type, tit_obj, con_obj, class_hove, class_out, speed) {
    /************action：触发操作************/
    /************titobj_type：选项卡对象类型************/
    /************conobj_type：选项内容目标对象类型************/
    /************tit_obj：选项卡ID************/
    /************con_obj：选项内容ID************/
    /************class_hove：选择状态选项卡Css名称************/
    /************class_out：非选择状态选项卡Css名称************/
    /************speed：内容展示动画速度************/

    $("" + titobj_type + "[id='" + tit_obj + "']").attr("class", class_out);
    $("" + conobj_type + "[id='" + con_obj + "']").hide();
    $("" + titobj_type + "[id='" + tit_obj + "']").eq(0).attr("class", class_hove);
    $("" + conobj_type + "[id='" + con_obj + "']").eq(0).show();

    $("" + titobj_type + "[id='" + tit_obj + "']").each(function (i) {
        if (action == "click") {
            $(this).click(function () {
                $("" + titobj_type + "[id='" + tit_obj + "']").attr("class", class_out);
                $("" + conobj_type + "[id='" + con_obj + "']").hide();
                $(this).attr("class", class_hove);
                $("" + conobj_type + "[id='" + con_obj + "']").eq(i).show();
            });
        }
        else {
            $(this).mouseover(function () {
                $("" + titobj_type + "[id='" + tit_obj + "']").attr("class", class_out);
                $("" + conobj_type + "[id='" + con_obj + "']").hide();
                $(this).attr("class", class_hove);
                $("" + conobj_type + "[id='" + con_obj + "']").eq(i).show();
                lazyloadForPart($("" + conobj_type + "[id='" + con_obj + "']").eq(i));
            });
        }

    });
}
function filter_setvalue(obj, objvalue) {
    //alert(objvalue);
    //alert(obj);
    MM_findObj(obj).value = objvalue;
    setTimeout(filter_setvalue_do, 0);
}
function filter_setvalue_do() {
    MM_findObj("form_filter").submit();
}
function AutosizeImage(ImgD, maxwidth, maxheight) {
    var image = new Image();
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0) {
        flag = true;
        if (image.width / image.height >= maxwidth / maxheight) {
            if (image.width > maxwidth) {
                ImgD.width = maxwidth;
                ImgD.height = (image.height * maxwidth) / image.width;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }
        else {
            if (image.height > maxheight) {
                ImgD.height = maxheight;
                ImgD.width = (image.width * maxheight) / image.height;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }
    }
}

//function MM_swapImage() { //v3.0
//    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
//        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; AutosizeImage(x, 310, 310); x.src = a[i + 2]; AutosizeImage(x, 310, 310); MM_findObj("demo1").href = a[i + 2]; }
//}
//function MM_swapImage() { //v3.0
//    var i, j = 0, x, a = MM_swapImage.arguments;
//    if ((x = MM_findObj(a[0])) != null) {
//        x.src = a[1];
//        MM_findObj("displayphoto").href = a[1];
//    }
//}
function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments;
    if ((x = MM_findObj(a[0])) != null) {
        x.src = a[1];
        MM_findObj("displayphoto").href = a[1];
    }
}

function switchimgborder(objid) {
    if (MM_findObj("product_img_s_1") != null) { MM_findObj("product_img_s_1").className = ""; }
    if (MM_findObj("product_img_s_2") != null) { MM_findObj("product_img_s_2").className = ""; }
    if (MM_findObj("product_img_s_3") != null) { MM_findObj("product_img_s_3").className = ""; }
    if (MM_findObj("product_img_s_4") != null) { MM_findObj("product_img_s_4").className = ""; }
    if (MM_findObj("product_img_s_5") != null) { MM_findObj("product_img_s_5").className = ""; }
    if (MM_findObj(objid) != null) { MM_findObj(objid).className = "on"; }
}

function detail_option(cur_option) {
    for (var i = 1; i < 6; i++) {
        if (i == cur_option) {
            MM_findObj("but_detail" + i).className = "select_selected";
            MM_findObj("detail" + i).style.display = "";
        }
        else {
            MM_findObj("but_detail" + i).className = "select_uselected";
            MM_findObj("detail" + i).style.display = "none";
        }
    }
}

function detail_option1(cur_option) {
    for (var i = 1; i < 6; i++) {
        if (i == cur_option) {
            MM_findObj("but_tip" + i).className = "lu";
            MM_findObj("tip" + i).style.display = "";
        }
        else {
            MM_findObj("but_tip" + i).className = "lu2";
            MM_findObj("tip" + i).style.display = "none";
        }
    }
}

function detail_option2(cur_option) {
    for (var i = 1; i < 5; i++) {
        if (i == 1) {
            if (i == cur_option) {
                MM_findObj("but_type_" + i).className = "lu1";
                MM_findObj("type_" + i).style.display = "";
            }
            else {
                MM_findObj("but_type_" + i).className = "lu3";
                MM_findObj("type_" + i).style.display = "none";
            }
        }
        else {
            if (i == cur_option) {
                MM_findObj("but_type_" + i).className = "lu";
                MM_findObj("type_" + i).style.display = "";
            }
            else {
                MM_findObj("but_type_" + i).className = "lu2";
                MM_findObj("type_" + i).style.display = "none";
            }
        }
    }
}

function setUseful(review_id, status) {

    if (status) {
        $(".span_userful_" + review_id).load("/product/reviews_do.aspx?action=use&review_id=" + review_id + "&fresh=" + Math.random() + "");
        $("#span_userful_" + review_id).load("/product/reviews_do.aspx?action=use&review_id=" + review_id + "&fresh=" + Math.random() + "");
    }
    else {
        $(".span_userful_" + review_id).load("/product/reviews_do.aspx?action=useless&review_id=" + review_id + "&fresh=" + Math.random() + "");
        $("#span_userful_" + review_id).load("/product/reviews_do.aspx?action=useless&review_id=" + review_id + "&fresh=" + Math.random() + "");
    }
}

function setover(obj) {
    obj.css({ opacity: "0.5", filter: "alpha(opacity=50)" });
}
function setout(obj) {
    obj.css({ opacity: "1", filter: "alpha(opacity=100)" });
}

//CharMode函数
//测试某个字符是属于哪一类.
function CharMode(iN) {
    if (iN >= 48 && iN <= 57)//数字
        return 1;
    if (iN >= 65 && iN <= 90)//大写字母
        return 2;
    if (iN >= 97 && iN <= 122)//小写
        return 4;
    else
        return 8;//特殊字符
}

//bitTotal函数
//计算出当前密码当中一共有多少种模式
function bitTotal(num) {
    modes = 0;
    for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
    }
    return modes;
}

//checkStrong函数
//返回密码的强度级别

function checkStrong(sPW) {
    if (sPW.length <= 4)
        return 0;//密码太短
    Modes = 0;
    for (i = 0; i < sPW.length; i++) {
        //测试每一个字符的类别并统计一共有多少种模式.
        Modes |= CharMode(sPW.charCodeAt(i));
    }

    return bitTotal(Modes);

}

//pwStrength函数
//当用户放开键盘或密码输入框失去焦点时,根据不同的级别显示不同的颜色

function pwStrength(pwd) {
    O_color = "#C4C4C4";
    L_color = "#FF0000";  //弱颜色
    M_color = "#F79100";  //中颜色
    H_color = "#33CC00";  //强颜色
    if (pwd == null || pwd == "") {
        Lcolor = Mcolor = Hcolor = O_color;
    }
    else {
        S_level = checkStrong(pwd);
        switch (S_level) {
            case 0:
                Lcolor = Mcolor = Hcolor = O_color;
            case 1:
                Lcolor = L_color;
                Mcolor = Hcolor = O_color;
                break;
            case 2:
                Lcolor = Mcolor = M_color;
                Hcolor = O_color;
                break;
            default:
                Lcolor = Mcolor = Hcolor = H_color;
        }
    }

    document.getElementById("strength_L").style.background = Lcolor;
    document.getElementById("strength_M").style.background = Mcolor;
    document.getElementById("strength_H").style.background = Hcolor;
    return;
}

function RefillAddress(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=fill&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

        success: function (data) {
            $("#" + targetdiv).html(data);
            $("#" + statename).val(statecode);
            $("#" + cityname).val(citycode);
            $("#" + countyname).val(countycode);
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function AddCartAddress(type) {
    var bz = true;
    var name = $('#Orders_Address_Name').attr('value');
    if (name == "") {
        $('#name_tip').html("&nbsp;&nbsp;收货人姓名不能为空！");
        bz = false;
    }
    else {
        $('#name_tip').html("");
    }
    var state = $('#Member_Address_State').attr('value');
    var city = $('#Member_Address_City').attr('value');
    var county = $('#Member_Address_County').attr('value');
    if (county == 0) {
        $('#state_tip').html("&nbsp;&nbsp;地区信息不完整！");
        bz = false;
    }
    else {
        $('#state_tip').html("");
    }
    var streetaddress = $('#Orders_Address_StreetAddress').attr('value');
    if (streetaddress == "") {
        $('#address_tip').html("&nbsp;&nbsp;收货地址不能为空！");
        bz = false;
    }
    else {
        $('#address_tip').html("");
    }
    var mobile = $('#Orders_Address_Mobile').attr('value');
    var number = $('#Orders_Address_Phone_Number').attr('value');
    if (mobile == "" && number == "") {
        $('#mobile_tip').html("&nbsp;&nbsp;固定电话和手机号码请至少填一项！");
        bz = false;
    }
    else {
        $('#mobile_tip').html("");
    }
    if (mobile != "") {
        $("#mobile_tip").load("/member/register_do.aspx?action=checkmobile&val=" + mobile + "&timer=" + Math.random());
        if ($("#mobile_tip").html().indexOf("cc0000") > 0) {
            bz = false;
        }
        else {
            $('#mobile_tip').html("");
        }

    }
    else {
        if (number != "") {
            $("#mobile_tip").load("/member/register_do.aspx?action=checkphone&val=" + number + "&timer=" + Math.random());
            if ($("#mobile_tip").html().indexOf("cc0000") > 0) {
                bz = false;
            }
            else {
                $('#mobile_tip').html("");
            }
        }

    }

    var zip = $('#Orders_Address_Zip').attr('value');
    if (zip == "") {
        $('#Zip_tip').html("&nbsp;&nbsp;邮政编码不能为空！");
        bz = false;
    }
    else {
        var reg = new RegExp("[0-9]{6}");
        var vars = reg.exec(zip);
        if (vars == null) {
            $('#Zip_tip').html("&nbsp;&nbsp;邮政编码格式为6位数字！");
            bz = false;
        }
        else {
            $('#Zip_tip').html("");
        }
    }

    if (bz == false) {
        return false;
    }
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/member/order_address_do.aspx?action=cart_address_add&type=" + type + "&Orders_Address_Name=" + name + "&Member_Address_State=" + state + "&Member_Address_City=" + city + "&Member_Address_County=" + county + "&Orders_Address_StreetAddress=" + streetaddress + "&Orders_Address_Mobile=" + mobile + "&Orders_Address_Phone_Number=" + number + "&Orders_Address_Zip=" + zip + "&timer=" + Math.random()),

        success: function (data) {
            if (type == 0) {
                if (data == "") {
                    alert("您当前已经存在了一个相同收货人姓名且相同收货地址的记录，不需要重复增加！");
                    return false;
                }
                $('#ti311').load('/cart/pub_do.aspx?action=addresslist&timer=' + Math.random());
            }
            else if (type == 1) {
                $('#xiugai_address').show();
                $('#guanbi_address').hide();
                $('#select_address').load('/cart/cart_do.aspx?action=selectaddress&time=' + Math.random());
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function AddCartExt(obj) {

    obj.href = obj.href + "&buy_amount=" + MM_findObj('buy_amount').value;

}


function AddCartExt1(pid) {

    var selected_productid = $('#product_id').val();
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",

        url: encodeURI("/cart/cart_do.aspx?action=add1&passto=confirm&product_id=" + pid + "&apid=" + selected_productid + "&buy_amount=" + MM_findObj('buy_amount').value + "&time=" + Math.random()),
        success: function (data) {

            if (data == "True") {
                window.location.href = "/cart/order_confirm.aspx?OrdersGoodsArray=" + pid + "";
            } else {
                alert(data);
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function updatetime(settime, divname) {
    var i;
    var showthis;
    update(0, settime, divname);

}
//只用于倒计时效果 和正常时间有别
function update_limit(settime, divname) { //alert(showthis);
    if (0 > settime)
    {; }
    else
    {
        showthis = settime - 0;
        //MM_findObj(divname).innerHTML="距离结束时间还有： <font class='t12_red'><b>"+Math.floor(showthis/(3600*24))+"</b></font> 天 <font class='t12_red'><b>"+Math.floor((showthis%(3600*24))/3600)+"</b></font> 小时 <font class='t12_red'><b>"+Math.floor((showthis%3600)/60)+"</b></font> 分钟 <font class='t12_red'><b>"+(showthis%3600)%60+"</b></font> 秒"; }
        //var d=Math.floor(showthis/(3600*24));
        var h = Math.floor((showthis % (3600 * 23)) / 3600); //23h
        if (h < 10) { h = "0" + h; }
        var m = Math.floor((showthis % 3600) / 59);  //59m
        if (m < 10) { m = "0" + m; }
        var s = (showthis % 3600) % 50; //50s
        if (s < 10) { s = "0" + s; }
        MM_findObj(divname).innerHTML = "<a>" + h + "</a> <a>" + m + "</a> <a>" + s + "</a>";
    }
    setTimeout("update_limit(" + (settime - 1) + ",'" + divname + "')", 1000);
}


function updatetime1(settime, divname) {
    var i;
    var showthis;
    //alert(settime);
    for (i = 1; i <= 200; i++)
    { setTimeout("update1(" + i + "," + settime + ",'" + divname + "')", i * 1000); }

}
function update1(num, settime, divname) { //alert(showthis);
    if (num > settime)
    {; }
    else {
        showthis = settime - num;
        MM_findObj(divname).innerHTML = "<p class=\"p1\">还剩<span class=\"t12_red\">" + Math.floor(showthis / (3600 * 24)) + "</span>天<span class=\"t12_red\">" + Math.floor((showthis % (3600 * 24)) / 3600) + "</span>时<span class=\"t12_red\">" + Math.floor((showthis % 3600) / 60) + "</span>分<span class=\"t12_red\">" + (showthis % 3600) % 60 + "</span>秒</p>";
    }
}


function update_limit_1(settime, divname) { //alert(showthis);
    if (0 > settime)
    {; }
    else
    {
        showthis = settime - 0;
        //MM_findObj(divname).innerHTML="距离结束时间还有： <font class='t12_red'><b>"+Math.floor(showthis/(3600*24))+"</b></font> 天 <font class='t12_red'><b>"+Math.floor((showthis%(3600*24))/3600)+"</b></font> 小时 <font class='t12_red'><b>"+Math.floor((showthis%3600)/60)+"</b></font> 分钟 <font class='t12_red'><b>"+(showthis%3600)%60+"</b></font> 秒"; }
        //var d=Math.floor(showthis/(3600*24));
        var h = Math.floor((showthis % (3600 * 24)) / 3600);
        if (h < 10) { h = "0" + h; }
        var m = Math.floor((showthis % 3600) / 60);
        if (m < 10) { m = "0" + m; }
        var s = (showthis % 3600) % 60;
        if (s < 10) { s = "0" + s; }
        MM_findObj(divname).innerHTML = "<a>" + h + "</a> <a>" + m + "</a> <a>" + s + "</a>";
    }
    setTimeout("update_limit_1(" + (settime - 1) + ",'" + divname + "')", 1000);
}


function update(num, settime, divname) { //alert(showthis);
    if (num > settime)
    {; }
    else
    {
        showthis = settime - num;
        MM_findObj(divname).innerHTML = "距离结束时间还有： <font style=\"color:#cc0000; font-weight:bold;\">" + Math.floor(showthis / (3600 * 24)) + "</font> 天 <font style=\"color:#cc0000; font-weight:bold;\">" + Math.floor((showthis % (3600 * 24)) / 3600) + "</font> 小时 <font style=\"color:#cc0000; font-weight:bold;\">" + Math.floor((showthis % 3600) / 60) + "</font> 分钟 <font style=\"color:#cc0000; font-weight:bold;\">" + (showthis % 3600) % 60 + "</font> 秒";
    }
    setTimeout("updatetime(" + (settime - 1) + ",'" + divname + "')", 1000);
}

function updatetime_list(settime, divname) {
    var i;
    var showthis;
    for (i = 1; i <= 150; i++)
    { setTimeout("update_list(" + i + "," + settime + ",'" + divname + "')", i * 1000); }

}
function update_list(num, settime, divname) { //alert(divname);
    if (num > settime)
    {; }
    else
    {
        showthis = settime - num;
        MM_findObj(divname).innerHTML = "<font class='t12_red'><b>" + Math.floor(showthis / (3600 * 24)) + "</b></font>天<font class='t12_red'><b>" + Math.floor((showthis % (3600 * 24)) / 3600) + "</b></font>小时<font class='t12_red'><b>" + Math.floor((showthis % 3600) / 60) + "</b></font>分<font class='t12_red'><b>" + (showthis % 3600) % 60 + "</b></font>秒";
    }
}

//折叠
function pucker(cate) {
    var src = $('#img_' + cate).attr("src");
    if (src == "/images/4.jpg") {
        $('#img_' + cate).attr("src", "/images/5.jpg");
        $('#ul_' + cate).css("display", "none");
    }
    else {
        $('#img_' + cate).attr("src", "/images/4.jpg");
        $('#ul_' + cate).css("display", "block");
    }
}
//删除购物车
function Del_cart(url) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI(url + "&timer=" + Math.random()),
        success: function (data) {
            location.reload();
        },
        error: function () {
        }
    });
}
function Add_cart(url) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI(url + "&timer=" + Math.random()),
        success: function (data) {
            alert("此商品已成功添加到购物车！");
            location.reload();
        },
        error: function () {
        }
    });
}

//物流查询
function CheckDelivery(fahuo_id) {
    $('#wulucx_' + fahuo_id).attr('href', '/member/delivery_inquiry.aspx?fahuo_id=' + fahuo_id + '&time=' + Math.random());
}

//邮件订阅
function Email_Notify_Request() {
    var s = $('#email_addr').attr('value');
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/member/login_do.aspx?action=emailnotify&email_addr=" + s + "&timer=" + Math.random()),
        success: function (data) {
            alert(data);
            if (data.toString().indexOf('成功') > 0) {
                $('#email_addr').attr('value', '请输入要订阅的Email地址');
            }

        },
        error: function () {
        }
    });
}

function GetCateDivStyle(obj) {
    if (document.getElementById("div_Cate_ID_" + obj).style.display == 'none') {
        document.getElementById("div_Cate_ID_" + obj).style.display = '';
        $("#div_img_" + obj).attr('src', '/images/sub.gif');
    }
    else {
        document.getElementById("div_Cate_ID_" + obj).style.display = 'none';
        $("#div_img_" + obj).attr('src', '/images/subj.gif');
    }
}
//部分区域图片延迟加载
function lazyloadForPart(container) {
    container.find('img').each(function () {
        var original = $(this).attr("data-original");
        if (original) {
            $(this).attr('src', original).removeAttr('data-original');
        }
    });
}


var setAmount = {
    min: 1,
    max: 99999,
    reg: function (x) {
        return new RegExp("^[1-9]\\d*$").test(x);
    },
    amount: function (obj, mode) {
        var x = $(obj).val();
        if (this.reg(x)) {
            if (mode) {
                x++;
            } else {
                x--;
            }
        } else {
            alert("请输入正确的数量！");
            $(obj).val(this.min);
            $(obj).focus();
        }
        return x;
    },
    reduce: function (obj) {
        var x = this.amount(obj, false);
        if (x >= this.min) {
            $(obj).val(x);
        } else {
            alert("商品数量最少为" + this.min);
            $(obj).val(1);
            $(obj).focus();
        }
    },
    add: function (obj) {
        var x = this.amount(obj, true);
        if (x <= this.max) {
            $(obj).val(x);
        } else {
            alert("商品数量最多为" + this.max);
            $(obj).val(this.max);
            $(obj).focus();
        }
    },
    modify: function (obj) {
        var x = $(obj).val();
        if (x < this.min || x > this.max || !this.reg(x)) {
            alert("请输入正确的数量！");
            $(obj).val(this.min);
            $(obj).focus();
        }
    }
}

function CountCost() {
    var costfee = parseFloat($("#price_amount").val()) * parseFloat($("#buy_amount").val());
    costfee = Math.round(costfee * 100) / 100;

    $("#price_account").text(costfee);
}



function confirmdelete(gotourl) {
    if ($("#dialog-confirmdelete").length == 0) {
        $("body").append("<div id=\"dialog-confirmdelete\" title=\"确定要删除吗?\"><p>记录删除后将无法恢复，您确定要删除吗？</p>");
    }
    $("#dialog-confirmdelete").dialog({
        modal: true,
        width: 400,
        buttons: {
            "确认": function () {
                $(this).dialog("close");
                location = gotourl;
            },
            "取消": function () {
                $(this).dialog("close");
            }
        }
    });
    $("#dialog-confirmdelete").dialog({
        close: function () {
            $(this).dialog("destroy");
        }
    });
}

function isDigit(s) {
    var result = true;
    var patrn = /^(-?)[0-9]{1,20}$/;
    if (!patrn.exec(s)) {
        result = false;
    }

    if (s == '-') {
        result = true;
    }
    return result;
}

//清空浏览记录
function clear_product_view_history() {
    MM_findObj("div_product_view_history").innerHTML = "";
    SetCookie("product_viewhistory_zh-cn", "");
}

function SetCookie(name, value) {
    var hours = 24;
    var exp = new Date();
    exp.setTime(exp.getTime() + hours * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";domain=" + location.hostname.substring(location.hostname.indexOf('.')) + ";path=/;expires=" + exp.toGMTString();//domain=.shangpinyou.com;
}


function SupplierShopApplyCheck() {
    $.post("/supplier/account_do.aspx", "action=checkShopApply", function (data) {
        if (data == "success") {
            location.href = "/supplier/supplier_shop_Apply.aspx";
        }
        else if (data == "NoInfo") {
            location.href = "/supplier/index.aspx";
        }
        else {
            alert(data);
        }
    });
}

function fmoney(s, n) {
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    var l = s.split(".")[0].split("").reverse(),
   r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("") + "." + r;
}
