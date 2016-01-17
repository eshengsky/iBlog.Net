$(function () {
    $(".my-nav-pills li:eq(0)").addClass("active").siblings().removeClass("active");

    uParse(".post-content", {
        rootPath: "/ueditor/"
    });

    $("#mian-context").find("img").wrap(function (i) {
        return "<a href=\"" + this.src + "\" data-lightbox=\"" + i + "\"></a>"
    });

    $("#mian-context").scrollNav({
        sections: "h2",
        subSections: "h3",
        showHeadline: true,
        headlineText: "文章目录",
        showTopLink: false,
        scrollOffset: 70,
        arrowKeys: true,
        insertTarget: "#control-wrap",
        insertLocation: "prependTo"
    });

    $(".close-menu").on("click", function () {
        $("#control-wrap").hide();
        $(".post-content").removeClass("col-md-9").addClass("col-md-12");
        setTimeout(function () {
            $(".btn-menu").css("margin-left", $(".post-content").width() + 31 + "px");
            $(".btn-menu").show();
        }, 310);
    });

    $(".btn-menu").on("click", function () {
        $(".btn-menu").hide();
        $(".post-content").removeClass("col-md-12").addClass("col-md-9");
        $("#control-wrap").show();
    });

    $(window).on("resize", function () {
        $.fn.scrollNav("resetPos");
    });
});

//Fixed Tool
$(function() {
    $(window).scroll(function() {
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