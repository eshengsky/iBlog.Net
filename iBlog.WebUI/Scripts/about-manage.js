$(function () {
    $("#side-menu>li:eq(5)").addClass("active");

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