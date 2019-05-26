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

/*可控制左右无缝循环（需引用MSClass.js）*/
function srcoll_left_right_Control(control_enabel, direction, left_div, right_div, scroll_body, scroll_content, total_width, total_height, scroll_width, scroll_speed) {
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

    var MarqueeDivControl = new Marquee([scroll_body, scroll_content], direction, 0.2, total_width, total_height, scroll_speed, 3000, 3000, scroll_width);
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

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; AutosizeImage(x, 300, 300); x.src = a[i + 2]; AutosizeImage(x, 300, 300); MM_findObj("demo1").href = a[i + 2]; }
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
function pwStrength(pwd, obj1, obj2, obj3) {
    O_color = "#C4C4C4";
    L_color = "#e1000c";  //弱颜色
    M_color = "#e1000c";  //中颜色
    H_color = "#e1000c";  //强颜色
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

    //$("#" + obj1).attr("background", Lcolor);
    //$("#" + obj2).attr("background", Mcolor);
    //$("#" + obj3).attr("background", Hcolor);

    document.getElementById(obj1).style.background = Lcolor;
    document.getElementById(obj2).style.background = Mcolor;
    document.getElementById(obj3).style.background = Hcolor;
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

function RefillAddressNew(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=newfill&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

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

function RefillAddressSecond(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=newfillSecond&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

        success: function (data) {
            $("#" + targetdiv).html(data);
            $("#" + statename).val(statecode);
            $("#" + cityname).val(citycode);
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function DeliveryRefillAddress(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
    $.ajax({
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        url: encodeURI("/public/ajax_address.aspx?action=deliveryfill&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

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
    //if (mobile != "") {
    //    $("#mobile_tip").load("/member/register_do.aspx?action=checkmobile&val=" + mobile + "&timer=" + Math.random());
    //    if ($("#mobile_tip").html().indexOf("cc0000") > 0) {
    //        bz = false;
    //    }
    //    else {
    //        $('#mobile_tip').html("");
    //    }

    //}
    if (mobile != "") {
        $("#mobile_tip").load("/member/register_do.aspx?action=checkcartmobile&val=" + mobile + "&timer=" + Math.random());
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
//function AddCartAddress(type) {
//    var bz = true;
//    var name = $('#Orders_Address_Name').attr('value');
//    if (name == "") {
//        $('#name_tip').html("&nbsp;&nbsp;收货人姓名不能为空！");
//        bz = false;
//    }
//    else {
//        $('#name_tip').html("");
//    }
//    var state = $('#Member_Address_State').attr('value');
//    var city = $('#Member_Address_City').attr('value');
//    var county = $('#Member_Address_County').attr('value');
//    if (county == 0) {
//        $('#state_tip').html("&nbsp;&nbsp;地区信息不完整！");
//        bz = false;
//    }
//    else {
//        $('#state_tip').html("");
//    }
//    var streetaddress = $('#Orders_Address_StreetAddress').attr('value');
//    if (streetaddress == "") {
//        $('#address_tip').html("&nbsp;&nbsp;收货地址不能为空！");
//        bz = false;
//    }
//    else {
//        $('#address_tip').html("");
//    }
//    var mobile = $('#Orders_Address_Mobile').attr('value');
//    var number = $('#Orders_Address_Phone_Number').attr('value');
//    if (mobile == "" && number == "") {
//        $('#mobile_tip').html("&nbsp;&nbsp;固定电话和手机号码请至少填一项！");
//        bz = false;
//    }
//    else {
//        $('#mobile_tip').html("");
//    }
//    if (mobile != "") {
//        $("#mobile_tip").load("/member/register_do.aspx?action=checkmobile&val=" + mobile + "&timer=" + Math.random());
//        if ($("#mobile_tip").html().indexOf("ff0000") > 0) {
//            bz = false;
//        }
//        else {
//            $('#mobile_tip').html("");
//        }

//    }
//    else {
//        if (number != "") {
//            $("#mobile_tip").load("/member/register_do.aspx?action=checkphone&val=" + number + "&timer=" + Math.random());
//            if ($("#mobile_tip").html().indexOf("ff0000") > 0) {
//                bz = false;
//            }
//            else {
//                $('#mobile_tip').html("");
//            }
//        }

//    }

//    var zip = $('#Orders_Address_Zip').attr('value');
//    if (zip == "") {
//        $('#Zip_tip').html("&nbsp;&nbsp;邮政编码不能为空！");
//        bz = false;
//    }
//    else {
//        var reg = new RegExp("[0-9]{6}");
//        var vars = reg.exec(zip);
//        if (vars == null) {
//            $('#Zip_tip').html("&nbsp;&nbsp;邮政编码格式为6位数字！");
//            bz = false;
//        }
//        else {
//            $('#Zip_tip').html("");
//        }
//    }

//    if (bz == false) {
//        return false;
//    }
//    $.ajax({
//        type: "get",
//        global: false,
//        async: false,
//        dataType: "html",
//        url: encodeURI("/member/order_address_do.aspx?action=cart_address_add&type=" + type + "&Orders_Address_Name=" + name + "&Member_Address_State=" + state + "&Member_Address_City=" + city + "&Member_Address_County=" + county + "&Orders_Address_StreetAddress=" + streetaddress + "&Orders_Address_Mobile=" + mobile + "&Orders_Address_Phone_Number=" + number + "&Orders_Address_Zip=" + zip + "&timer=" + Math.random()),

//        success: function (data) {
//            if (type == 0) {
//                if (data == "") {
//                    alert("您当前已经存在了一个相同收货人姓名且相同收货地址的记录，不需要重复增加！");
//                    return false;
//                }
//                $('#ti311').load('/cart/pub_do.aspx?action=addresslist&timer=' + Math.random());
//            }
//            else if (type == 1) {
//                $('#xiugai_address').show();
//                $('#guanbi_address').hide();
//                $('#select_address').load('/cart/cart_do.aspx?action=selectaddress&time=' + Math.random());
//            }
//        },
//        error: function () {
//            alert("Error Script");
//        }
//    });
//}

function AddCartExt(obj) {

    var addcartURL = obj.href;
    addcartURL = addcartURL.replace(/&buy_amount=(\d)+$/, "");
    addcartURL = addcartURL.replace(/&addname=.+$/, "");
    obj.href = addcartURL + "&buy_amount=" + MM_findObj('buy_amount').value;

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
    if (src == "../images/sub.gif" || src == "/images/sub.gif") {
        $('#img_' + cate).attr("src", "../images/subj.gif");
        $('#ul_' + cate).css("display", "none");
    }
    else {
        $('#img_' + cate).attr("src", "../images/sub.gif");
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
    max: 9999999,
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



function homecountdown(settime, divname) {
    var hour = Math.floor((settime % (3600 * 24)) / 3600);
    var minute = Math.floor((settime % 3600) / 60);
    var second = (settime % 3600) % 60;

    if (hour < 10) hour = "0" + hour;
    if (minute < 10) minute = "0" + minute;
    if (second < 10) second = "0" + second;

    $("#" + divname + "_hour").html(hour);
    $("#" + divname + "_minute").html(minute);
    $("#" + divname + "_second").html(second);

    setTimeout("homecountdown(" + (settime - 1) + ",'" + divname + "')", 1000);
}

function videos_view(VideosFile) {
    var vhtml = "";
    vhtml = vhtml + "<object type=\"application/x-shockwave-flash\" data=\"/product/vcastr3.swf\" width=\"400\" height=\"400\" id=\"vcastr3\">";
    vhtml = vhtml + "	<param name=\"movie\" value=\"/product/vcastr3.swf\"/>";
    vhtml = vhtml + "	<param name=\"allowFullScreen\" value=\"true\" />";
    vhtml = vhtml + "	<param name=\"FlashVars\" value=\"xml=";
    vhtml = vhtml + "					<vcastr>";
    vhtml = vhtml + "						<channel>";
    vhtml = vhtml + "							<item>";
    vhtml = vhtml + "								<source>" + VideosFile + "</source>";
    vhtml = vhtml + "								<duration></duration>";
    vhtml = vhtml + "								<title></title>";
    vhtml = vhtml + "							</item>";
    vhtml = vhtml + "							<config><isautoplay>false</isautoplay><isloadbegin>false</isloadbegin></config>";
    vhtml = vhtml + "						</channel>";
    vhtml = vhtml + "					</vcastr>\"/>";
    vhtml = vhtml + "</object>";
    return vhtml;
}

function download_contract(object) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/download.aspx?Orders_ID=" + object,
        async: false,
        success: function (msg) {
            if (msg == "NoContract") {
                layer.alert('您下载的合同不存在，请稍后重试！', 3);
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}


function pages_change(pageurl, page) {
    window.location.href = pageurl + "&page=" + page;
}


function openShutManager(oSourceObj, oTargetObj, shutAble, oOpenTip, oShutTip) {
    var sourceObj = typeof oSourceObj == "string" ? document.getElementById(oSourceObj) : oSourceObj;
    var targetObj = typeof oTargetObj == "string" ? document.getElementById(oTargetObj) : oTargetObj;
    var openTip = oOpenTip || "";
    var shutTip = oShutTip || "";
    if (targetObj.style.display != "none") {
        if (shutAble) return;
        targetObj.style.display = "none";
        if (openTip && shutTip) {
            sourceObj.innerHTML = shutTip;
        }
    } else {
        targetObj.style.display = "block";
        if (openTip && shutTip) {
            sourceObj.innerHTML = openTip;
        }
    }
}


function SetCookie(name, value) {
    var hours = 24;
    var exp = new Date();
    exp.setTime(exp.getTime() + hours * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";path=/;expires=" + exp.toGMTString();
}

function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null)
        return unescape(arr[2]);
    return null;
}

function GetProductPrice() {
    if ($("#Product_PriceType2").prop("checked") == true) {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/supplier/product_do.aspx?action=getprice&fee=" + $('#Product_ManualFee').val() + "&weight=" + $('#Product_Weight').val(),
            async: false,
            success: function (msg) {
                $("#tr_price1").show();
                $("#span_price").html("￥" + msg);
                for (var i = 1; i <= 10; i++) {
                    $("#product_wholeprice_amount_" + i).attr("readonly", "readonly");
                    $("#product_wholeprice_amount_" + i).val(msg);
                }
            }
        })
    }
}


function set_form_filter(object, value, target) {
    $("#" + object + target).val(value);
    ajax_submit_filter_form(target);
}

function ajax_submit_filter_form(target) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/TradeIndex/iframe_product.aspx",
        data: $("#form_filter" + target).serialize(),
        async: false,
        success: function (msg) {
            $("#bb0" + target).html(msg);
        }
    })
}



function favorites_add_ajax(product_id, action) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/fav_do.aspx?action=" + action + "&id=" + product_id,
        async: false,
        shade: [0.5, '#000'],
        success: function (msg) {
            if (msg == "success") {
                layer.msg('信息收藏成功！', { icon: 1, time: 2000 });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        }
    })
}

function favorites_shop_ajax(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/fav_do.aspx?action=shop&id=" + id,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                alert("关注成功！");
            } else {
                alert(msg);
            }
        }
    })
}

function favorites_ajax(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/fav_do.aspx?action=product&id=" + id,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                alert("关注成功！");
            } else {
                alert(msg);
            }
        }
    })
}

function product_favorites(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/fav_do.aspx?action=product&id=" + id,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                alert("收藏成功！");

                //$("#favorites_count").load('/product/product_do.aspx?action=load_favcount&product_id=' + id);

            } else {
                alert(msg);
            }
        }
    })
}

function update_top_cartcount() {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/cart/pub_do.aspx?action=cartcount",
        async: false,
        success: function (msg) {
            $("#cart_count").html(msg);
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}


function update_erpparamsession(orderid) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/account_do.aspx?action=update_session&orders_id=" + orderid,
        async: false,
        success: function (msg) {
            NTKF_PARAM.erpparam = "orderid=" + msg;
        }
    })
}





function check_Cart_All() {
    if ($('#chk_all_messages').attr("checked")) {
        $("input[name='chk_messages']:enabled").attr("checked", true);
        //$("input[name='chk_cart_supplier']:enabled").attr("checked", true);
    }
    else {
        $("input[name='chk_messages']").attr("checked", false);
        //$("input[name='chk_cart_supplier']").attr("checked", false);
    }
}




//会员消息操作
function MoveMessagesByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要删除消息', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定要删除所选消息？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/member/Member_Message_do.aspx?action=messagesmove&chk_messages=" + messages_id,
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}

function AllMoveMessagesByID() {
    layer.confirm('确定要删除全部消息？', { icon: 3 },
       function () {
           $.ajax({
               cache: false,
               type: "POST",
               url: "/member/Member_Message_do.aspx?action=allmessagesmove",
               async: false,
               success: function (msg) {
                   if (msg == "success") {
                       window.location.reload();
                   } else {
                       layer.alert('操作失败，请稍后重试！', 3);
                       window.location.reload();
                   }
               },
               error: function (request) {
                   alert("Connection error");
               }
           })
       }
     );
}

function MessagesIsReadByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要标记已读消息', { icon: 2, time: 2000 });
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/member/Member_Message_do.aspx?action=messagesIsRead&chk_messages=" + messages_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location.reload();
                } else {
                    layer.alert('操作失败，请稍后重试！', 3);
                    window.location.reload();
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}

function AllMessagesIsReadByID() {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/Member_Message_do.aspx?action=allmessagesIsRead",
        async: false,
        success: function (msg) {

            if (msg == "success") {
                window.location.reload();
            } else {
                layer.alert('操作失败，请稍后重试！', 3);
                window.location.reload();
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function MessagesIsUnReadByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要删除消息', { icon: 2, time: 2000 });
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/member/Member_Message_do.aspx?action=messagesIsUnRead&chk_messages=" + messages_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location.reload();
                } else {
                    layer.alert('操作失败，请稍后重试！', 3);
                    window.location.reload();
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}





//商家消息操作
function SupplierMoveMessagesByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要删除消息', { icon: 2, time: 2000 });
        return false;
    } else {
        layer.confirm('确定要删除所选消息？', { icon: 3 },
            function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/supplier/Supplier_Shop_Message_do.aspx?action=suppliermessagesmove&chk_messages=" + messages_id,
                    async: false,
                    success: function (msg) {
                        if (msg == "success") {
                            window.location.reload();
                        } else {
                            layer.alert('操作失败，请稍后重试！', 3);
                            window.location.reload();
                        }
                    },
                    error: function (request) {
                        alert("Connection error");
                    }
                })
            }
        );
    }
}

function SupplierAllMoveMessagesByID() {

    layer.confirm('确定要删除全部消息？', { icon: 3 },
        function () {
            $.ajax({
                cache: false,
                type: "POST",
                url: "/supplier/Supplier_Shop_Message_do.aspx?action=supplierallmessagesmove",
                async: false,
                success: function (msg) {
                    if (msg == "success") {
                        window.location.reload();
                    } else {
                        layer.alert('操作失败，请稍后重试！', 3);
                        window.location.reload();
                    }
                },
                error: function (request) {
                    alert("Connection error");
                }
            })
        }
     );
}

function SupplierMessagesIsReadByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要标记已读消息', { icon: 2, time: 2000 });
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/supplier/Supplier_Shop_Message_do.aspx?action=suppliermessagesIsRead&chk_messages=" + messages_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location.reload();
                } else {
                    layer.alert('操作失败，请稍后重试！', 3);
                    window.location.reload();
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
}

function SupplierAllMessagesIsReadByID() {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/supplier/Supplier_Shop_Message_do.aspx?action=supplierallmessagesIsRead",
        async: false,
        success: function (msg) {

            if (msg == "success") {
                window.location.reload();
            } else {
                layer.alert('操作失败，请稍后重试！', 3);
                window.location.reload();
            }
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function SupplierMessagesIsUnReadByID() {
    var messages_id = "0";
    var r = $("input[name='chk_messages']");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            messages_id += "," + r[i].value;
        }
    }

    if (messages_id == "0") {
        layer.msg('请选择要删除消息', { icon: 2, time: 2000 });
        return false;
    } else {
        $.ajax({
            cache: false,
            type: "POST",
            url: "/supplier/Supplier_Shop_Message_do.aspx?action=suppliermessagesIsUnRead&chk_messages=" + messages_id,
            async: false,
            success: function (msg) {
                if (msg == "success") {
                    window.location.reload();
                } else {
                    layer.alert('操作失败，请稍后重试！', 3);
                    window.location.reload();
                }
            },
            error: function (request) {
                alert("Connection error");
            }
        })
    }
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