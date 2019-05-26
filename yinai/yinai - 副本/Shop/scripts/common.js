
function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function filter_setvalue(obj, objvalue) {
    MM_findObj(obj).value = objvalue;
    setTimeout(filter_setvalue_do, 0);
}
function filter_setvalue_do() {
    MM_findObj("form_filter").submit();
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

function switchTag(content) {
    if (document.getElementById(content).className == "hidecontent") {
        document.getElementById(content).className = "";
    }
    else { document.getElementById(content).className = "hidecontent"; }
}


function favorites_add_ajax(product_id, action) {
    $.ajax({
        cache: false,
        type: "POST",
        url:"fav_do.aspx?action=" + action + "&id=" + product_id,
        async: false,
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


function ajax_getpurchasereply_count()
{
    $.ajax({
        cache: false,
        type: "POST",
        url: "/member/fav_do.aspx?action=" + action + "&id=" + product_id,
        async: false,
        success: function (msg) {
            
        }
    })
}

function favorites_shop_ajax(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "fav_do.aspx?action=shop&id=" + id,
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