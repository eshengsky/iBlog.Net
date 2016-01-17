$(function () {
    $(".my-nav-pills li:eq(3)").addClass("active").siblings().removeClass("active");

    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        if (scrollTop > 0) {
            $("#scrollTop").show();
        } else {
            $("#scrollTop").hide();
        }
    });

    $("#scrollTop a").on("click", function () {
        $("html,body").animate({ scrollTop: 0 }, 1000);
    });
});
