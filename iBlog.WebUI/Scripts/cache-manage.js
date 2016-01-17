$(function () {
    $("#side-menu>li:eq(6)").addClass("active");

    $("#btnQuery").on("click", function () {
        var key = $("#cacheKey").val();
        if (key) {
            $.ajax({
                url: "/admin/getcachedata",
                type: "Post",
                data: {
                    key: key
                },
                success: function (data) {
                    $("#cacheContent").html(data);
                    $("#cacheContent").focus();
                },
                error: function () {
                    swal({
                        title: "获取失败！",
                        type: "error",
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            });
        }
    });

    $("#btnClear").on("click", function () {
        var key = $("#cacheKey").val();
        if (key) {
            $.ajax({
                url: "/admin/clearcache",
                type: "Post",
                data: {
                    key: key
                },
                success: function () {
                    swal({
                        title: "成功清除！",
                        type: "success",
                        showConfirmButton: false,
                        timer: 2000
                    });
                    $("#cacheContent").html("");
                    $("#cacheContent").focus();
                },
                error: function () {
                    swal({
                        title: "清除失败！",
                        type: "error",
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            });
        }
    });
});