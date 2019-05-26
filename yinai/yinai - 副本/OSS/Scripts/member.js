function save_member_cert()
{
    var params = $("#formcert").serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/member_do.aspx?action=savecert&member_id=" + $("#member_id").val() + "&member_certtype=" + $("#member_certtype").val()),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('资质保存成功！', { icon: 1, time: 2000 }, function () { window.location.reload(); });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            layer.alert("链接错误，请稍后再试......");
        }
    })
}

function update_member_profile()
{
    var params = $("#formadd").serialize();
    $.ajax({
        cache: false,
        type: "POST",
        url: encodeURI("/member/member_do.aspx?action=updateprofile"),
        data: params,
        async: false,
        success: function (msg) {
            if (msg == "success") {
                layer.msg('资料更新成功！', { icon: 1, time: 2000 }, function () { window.location.reload(); });
            }
            else {
                layer.msg(msg, { icon: 2, time: 2000 });
            }
        },
        error: function (request) {
            layer.alert("链接错误，请稍后再试......");
        }
    })
}