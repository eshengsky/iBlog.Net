var editor;
$(function () {
    $(".my-nav-pills li:eq(2)").addClass("active").siblings().removeClass("active");

    editor = ace.edit("editor");
    editor.session.setMode("ace/mode/html");
    editor.setFontSize(13);
    editor.setAutoScrollEditorIntoView(true);
    var html = "<!DOCTYPE html>"
        + "\r\n<html>"
        + "\r\n<head>"
        + "\r\n\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" \/>"
        + "\r\n\t<meta charset=\"utf-8\" \/>"
        + "\r\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" \/>"
        + "\r\n\t<title>测试页面</title>"
        + "\r\n\t<!--样式区-->"
        + "\r\n\t<style>"
        + "\r\n\t\tbody{"
        + "\r\n\t\t\tmargin:0;"
        + "\r\n\t\t}"
        + "\r\n\t</style>"
        + "\r\n</head>"
        + "\r\n<body>"
        + "\r\n\t<div style=\"text-align:center;color:green;\">"
        + "\r\n\t\t前台测试页面内容"
        + "\r\n\t</div>"
        + "\r\n\t<!--脚本区-->"
        + "\r\n\t<script src=\"//cdn.bootcss.com/jquery/1.11.3/jquery.min.js\"><\/script>"
        + "\r\n\t<script>"
        + "\r\n\t\t$(function(){"
        + "\r\n\t\t\t"
        + "\r\n\t\t});"
        + "\r\n\t<\/script>"
        + "\r\n</body>"
        + "\r\n</html>";
    editor.setValue(html);
    editor.clearSelection();
    editor.focus();

    $("#btnSubmit").on("click", function () {
        var $this = $(this);
        $this.attr("disabled", "disabled");
        $this.find(".fa").removeClass("fa-send").addClass("fa-circle-o-notch fa-spin");
        $.ajax({
            url: "/Tools/AddHtmlToSession",
            type: "Post",
            data: { "html": editor.getValue() },
            success: function () {
                $("#frame1")[0].contentWindow.location.reload();
            },
            complete: function () {
                $this.removeAttr("disabled");
                $this.find(".fa").removeClass("fa-circle-o-notch fa-spin").addClass("fa-send");
            }
        });
    });

    $("#btnSubmit").click();
});