function GetMore(target,type)
{
    var pages = $("#currpage"+target).val();
    var pagecount = $("#pagecount" + target).val();
    var collection_id = $("#collection_id" + target).val();
    var cateprice_id = $("#cateprice_id" + target).val();

    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=getmore&page=" + pages + "&type=" + type + "&cateprice_id=" + cateprice_id + "&collection_id=" + collection_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            if (data) {
                pages++;
                $("#ul_productlist" + target).append(data);
                $("#currpage" + target).val(pages);

                if (pages > pagecount) {
                    $("#div_more" + target).hide();
                }
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function GetMore1(target, type) {
    var pages = $("#currpage" + target).val();
    var pagecount = $("#pagecount" + target).val();
    var defaultcate = $("#defaultcate" + target).val();

    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=getmore1&page=" + pages + "&type=" + type + "&defaultcate="+defaultcate+"&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            if (data) {
                pages++;
                $("#ul_productlist" + target).append(data);
                $("#currpage" + target).val(pages);

                if (pages > pagecount) {
                    $("#div_more" + target).hide();
                }
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function GetHotMore() {
    var pages = $("#currpage").val();
    var pagecount = $("#pagecount").val();
    var collection_id = $("#collection_id").val();
    var cateprice_id = $("#cateprice_id").val();

    $.ajax({
        url: encodeURI("/product/product_do.aspx?action=gethotmore&page=" + pages + "&type=" + type + "&cateprice_id=" + cateprice_id + "&collection_id=" + collection_id + "&timer=" + Math.random()),
        type: "get",
        global: false,
        async: false,
        dataType: "html",
        success: function (data) {
            if (data) {
                pages++;
                $("#ul_productlist").append(data);
                $("#currpage").val(pages);

                if (pages > pagecount) {
                    $("#div_more").hide();
                }
            }
        },
        error: function () {
            alert("Error Script");
        }
    });
}

function set_form_other_filter(object, value, target) {
    $("#" + object + target).val(value);
    ajax_submit_form(target);
}

function ajax_submit_form(target)
{
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/TradeIndex/iframe_new.aspx"),
        data: $('#form_filter' + target).serialize(),
        async: false,
        success: function (msg) {
            $("#bb0" + target).html(msg);
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}

function set_hot_form_filter(object, value) {
    $("#" + object).val(value);
    ajax_submit_hot_filter_form();
}

function ajax_submit_hot_filter_form() {
    $.ajax({
        cache: false,
        type: "POST",
        url: "/TradeIndex/iframe_today.aspx",
        data: $("#form_hot_filter").serialize(),
        async: false,
        success: function (msg) {
            $("#bb01").html(msg);
        }
    })
}