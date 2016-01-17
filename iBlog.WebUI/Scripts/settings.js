$(function () {
    $("#side-menu>li:eq(8)").addClass("active");

    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    elems.forEach(function (el) {
        var switchery = new Switchery(el, { color: '#1AB394' });
    });

    $(document).on({
        change: function () {
            $(this).prev(":hidden").val(this.checked);
        }
    }, ".js-switch");

    $(".fileupload").fileupload({
        url: "/admin/uploadimg",
        dataType: "text",
        done: function (e, data) {
            $(this).prev("img").attr("src", data.result);
            $(this).next(":hidden").val(data.result);
        }
    });
});

function onSuccess() {
    swal({
        title: "保存成功！",
        type: "success",
        showConfirmButton: false,
        timer: 2000
    });
}

function onFailure() {
    swal({
        title: "保存失败！",
        type: "error",
        showConfirmButton: false,
        timer: 2000
    });
}