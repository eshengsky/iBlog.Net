UE.registerUI('inserttree', function (editor, uiName) {
    var dialog = new UE.ui.Dialog({
        iframeUrl: '/ueditor/dialogs/inserttree/inserttree.html',
        editor: editor,
        name: uiName,
        title: "时间树"
    });

    var btn = new UE.ui.Button({
        name: 'my_' + uiName,
        title: "时间树",
        cssRules: 'background-position: -500px 0;',
        onclick: function () {
            dialog.render();
            dialog.open();
        }
    });

    editor.addListener('selectionchange', function () {
        var state = editor.queryCommandState(uiName);
        if (state == -1) {
            btn.setDisabled(true);
        } else {
            btn.setDisabled(false);
        }
    });

    return btn;
}, 63);