function Purchases_FormSubmit()
{
    var params = $('#frm_purchases').serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/statistics_do.aspx?action=purchases"),
        data: params,
        async: false,
        success: function (msg) {
            $("#div_purchases").html(msg);
        },
        error: function (request) {
            alert("Connection error");
        }
    })
}