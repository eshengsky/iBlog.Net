var editor;
var html;
var search;
$(function () {
    $(".my-nav-pills li:eq(2)").addClass("active").siblings().removeClass("active");

    editor = ace.edit("editor");
    editor.session.setMode("ace/mode/html");
    editor.setFontSize(13);
    editor.setAutoScrollEditorIntoView(true);
    editor.setValue("<div class=\"item\">"
                                + "\r\n\t<h4>"
                                + "\r\n\t\t<a title=\"{0}\" target=\"_blank\" href=\"{1}\">{0}<\/a>"
                                + "\r\n\t<\/h4>"
                                + "\r\n\t<p>{2}<\/p>"
                                + "\r\n<\/div>");
    editor.clearSelection();
    editor.focus();
    updateFlags();
    $(".vname:eq(0)").val("title");
    $(".vname:eq(1)").val("url");
    $(".vname:eq(2)").val("content");
    joinCode();

    editor.on("change", function () {
        updateFlags();
    });

    $(document).on({
        mouseenter: function () {
            var flag = $(this).find("td.flag").text();
            editor.findAll(flag);
        },
        mouseleave: function () {
            editor.clearSelection();
        }
    }, "#flagTable tr");
    $("#btnJoin").on("click", function () {
        joinCode();
    });
});

function updateFlags() {
    html = editor.getValue();
    search = html.match(/{\d+}/g);
    if (search) {
        search.forEach(function (item) {
            var td = $("#flagTable td.flag:contains('" + item + "')").length;
            if (td == 0) {
                $("#flagTable tbody").append("<tr><td class=\"flag\">" + item + "</td><td><input type=\"text\" class=\"vname form-control\"/></td></tr>");
            }
        });
    }
    var flags = $("#flagTable td.flag").toArray();
    if (flags.length > 0) {
        var flag;
        flags.forEach(function (item) {
            flag = $(item).text();
            var index = editor.getValue().indexOf(flag);
            if (index == -1) {
                $("#flagTable td.flag:contains('" + flag + "')").parent().remove();
            }
        });
    }
}

function joinCode() {
    var checked = true;
    $(".vname").each(function () {
        if (!$(this).val()) {
            checked = false;
        }
    });
    if (!checked) {
        $("#txtTip").fadeIn();
        setTimeout(function () {
            $("#txtTip").fadeOut();
        }, 2000);
        return;
    }
    var original = editor.getValue();
    var finalv = original
        .replace(/\//g, "\\\/")
        .replace(/'/g, "\\\'")
        .replace(/"/g, "\\\"")
        .replace(/^.*$/gm, function (match) {
            if (match) {
                return "+ \"" + match + "\"";
            } else {
                return "";
            }
        })
        .replace(/{\d+}/g, function (match) {
            var el = $("#flagTable td.flag:contains('" + match + "')");
            var val;
            if (el.length > 0) {
                val = el.next("td").find("input").val();
            } else {
                val = match;
            }
            return "\"\r\n+ " + val + "\r\n+ \"";
        })
        .replace("+ \"\"", "")
        .replace("+ ", "");
    $("#result").html(finalv);
}