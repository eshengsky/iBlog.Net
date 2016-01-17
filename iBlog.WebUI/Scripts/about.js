$(function () {
    $(".my-nav-pills li:eq(4)").addClass("active").siblings().removeClass("active");

    $("#job-title").cycleText();

    $(".fa-qrcode").mouseenter(function () {
        $(".profile-img").hide();
        $(".wechat-img").show();
    });

    $(".fa-qrcode").mouseleave(function () {
        $(".wechat-img").hide();
        $(".profile-img").show();
    });

    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        if (scrollTop > 0) {
            $("#scrollTop").show();
            $(".qrcontain").css("top", "-57px");
            $(".qrcontain .arrow").css("top", "52%");
        } else {
            $("#scrollTop").hide();
            $(".qrcontain").css("top", "-107px");
            $(".qrcontain .arrow").css("top", "86%");
        }
    });

    $("#qrBtn").on("click", function () {
        if ($("#ss_toggle").hasClass("close")) {
            $("#share-menu").css("transition", "none");
            $("#ss_toggle").click();
        }
        if ($(".qrcontain").is(":hidden")) {
            $(".qrcontain").removeClass("fadeOutLeft").addClass("fadeInLeft");
            $(".qrcontain").show();
            $("#qrBtn").addClass("opened");
        } else {
            $(".qrcontain").removeClass("fadeInLeft").addClass("fadeOutLeft");
            $(".qrcontain").one("webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend", function () {
                $(".qrcontain").hide();
            });
            $("#qrBtn").removeClass("opened");
        }
        $("#share-menu").css("transition", "all 1s ease 0s");
    });

    $("#scrollTop a").on("click", function () {
        $("html,body").animate({ scrollTop: 0 }, 1000);
    });

    var toggle = $('#ss_toggle');
    var menu = $('#share-menu');
    var rot;
    $('#ss_toggle').on('click', function (ev) {
        if (!$(".qrcontain").is(":hidden")) {
            $(".qrcontain").hide();
            $("#qrBtn").removeClass("opened");
        }
        rot = parseInt($(this).data('rot')) - 180;
        if (rot / 180 % 2 == 0) {
            menu.css('transform', 'rotate(' + rot + 'deg)');
            menu.css('webkitTransform', 'rotate(' + rot + 'deg)');
            toggle.parent().addClass('ss_active');
            toggle.addClass('close');
        } else {
            menu.css('transform', 'rotate(' + parseInt(rot - 30) + 'deg)');
            menu.css('webkitTransform', 'rotate(' + parseInt(rot - 30) + 'deg)');
            toggle.parent().removeClass('ss_active');
            toggle.removeClass('close');
        }
        $(this).data('rot', rot);
    });
    menu.on('transitionend webkitTransitionEnd oTransitionEnd', function () {
        if (rot / 180 % 2 == 0) {
            $("#share-menu i.fa").addClass('bounce');
        } else {
            $("#share-menu i.fa").removeClass('bounce');
        }
    });

    var img = document.createElement("img");
    img.src = logoPath;
    img.onload = function () {
        $("#qrcode").qrcode({
            text: window.location.href,
            size: "100",
            ecLevel: 'H',
            minVersion: 4,
            mode: 4,
            image: img,
            mSize: 0.3
        });
    }
});