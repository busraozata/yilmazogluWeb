/*=========================================================================================
    File Name: editor-quill.js
    Description: Quill is a modern rich text editor built for compatibility and extensibility.
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
    Author: Pixinvent
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/
(function (window, document, $) {
    "use strict";

    var activeTextArea = null;

    function tinyMceEditLink(editor) {
        editor.windowManager.oldOpen = editor.windowManager.open; // save for later
        editor.windowManager.open = function (t, r) {
            // replace with our own function
            var modal = this.oldOpen.apply(this, [t, r]); // call original

            if (t.title === "Insert/Edit Image") {
                $(".tox-dialog__body-nav").append(
                    '<div id="openMenu" class="tox-dialog__body-nav-item tox-tab">Select Image</div>'
                );

                $("#openMenu").on("click", function () {
                    modal.close();
                    activeTextArea = editor;
                    openImgSelectModal("tinymce");
                });
            }

            return modal;
        };
    }

    // Basic TineMCE
    tinymce.init({
        selector: ".tinymce",
        height: 400,
        plugins: [
            "advlist autolink lists link image charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "emoticons template paste textcolor colorpicker textpattern imagetools",
        ],
        images_file_types: "jpg,png",
        toolbar1:
            "insertfile undo redo | fontselect | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        toolbar2: "print preview media | forecolor backcolor emoticons",
        image_advtab: true,
        paste_data_images: true,
        images_upload_url: "/TinyMCEUploadImage",
        extended_valid_elements: "*[class]|*[data-*]",
        valid_elements: "*[*]",
        relative_urls: false,
        templates: [
            { title: "Test template 1", content: "Test 1" },
            { title: "Test template 2", content: "Test 2" },
        ],
        content_css: [
            "//fonts.googleapis.com/css?family=Lato:300,300i,400,400i",
            "//www.tinymce.com/css/codepen.min.css",
        ],
        setup: function (editor) {
            editor.on("init", function (e) {
                tinyMceEditLink(editor);
            });
        },
    });

    $(function () {
        $(".imgSelection").on("click", "img", function () {
            if ($("#imgPicker").attr("data-for") == "tinymce") {
                activeTextArea.insertContent(
                    '<br><img width="100%" src="' +
                    $(this).attr("src") +
                    '" alt="' +
                    $(this).attr("alt") +
                    '"/> <br>'
                );
                $("#imgPicker").modal("hide");
            }
        });
    });

    tinymce.init({
        selector: "textarea#custom-toolbar-button",
        toolbar: "customInsertButton customDateButton",

        content_style:
            "body { font-family:Helvetica,Arial,sans-serif; font-size:14px }",
    });

    // Inline Edit
    tinymce.init({
        selector: "h2.editable",
        inline: true,
        toolbar: "undo redo",
        menubar: false,
    });

    tinymce.init({
        selector: "div.editable",
        inline: true,
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime media table contextmenu paste",
        ],
        toolbar:
            "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    });

    // Classic TinyMCE
    tinymce.init({
        selector: ".tinymce-classic",
        height: 500,
        plugins: [
            "advlist autolink autosave link image lists charmap print preview hr anchor pagebreak spellchecker",
            "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern",
        ],

        toolbar1:
            "newdocument fullpage | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect",
        toolbar2:
            "cut copy paste | searchreplace | bullist numlist | outdent indent blockquote | undo redo | link unlink anchor image media code | insertdatetime preview | forecolor backcolor",
        toolbar3:
            "table | hr removeformat | subscript superscript | charmap emoticons | print fullscreen | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",

        menubar: false,
        toolbar_items_size: "small",

        style_formats: [
            {
                title: "Bold text",
                inline: "b",
            },
            {
                title: "Red text",
                inline: "span",
                styles: {
                    color: "#ff0000",
                },
            },
            {
                title: "Red header",
                block: "h1",
                styles: {
                    color: "#ff0000",
                },
            },
            {
                title: "Example 1",
                inline: "span",
                classes: "example1",
            },
            {
                title: "Example 2",
                inline: "span",
                classes: "example2",
            },
            {
                title: "Table styles",
            },
            {
                title: "Table row 1",
                selector: "tr",
                classes: "tablerow1",
            },
        ],

        templates: [
            {
                title: "Test template 1",
                content: "Test 1",
            },
            {
                title: "Test template 2",
                content: "Test 2",
            },
        ],
        content_css: ["//www.tinymce.com/css/codepen.min.css"],
    });

    // Custom ToolBar
    tinymce.init({
        selector: ".tinymce-toolbar",
        height: 350,
        toolbar: "mybutton",
        // menubar: false,
        setup: function (editor) {
            editor.ui.registry.addMenuButton("mybutton", {
                text: "My button",
                fetch: function (callback) {
                    var items = [
                        {
                            type: "menuitem",
                            text: "Menu item 1",
                            onAction: function () {
                                editor.insertContent("&nbsp;<em>You clicked menu item 1!</em>");
                            },
                        },
                        {
                            type: "nestedmenuitem",
                            text: "Menu item 2",
                            icon: "user",
                            getSubmenuItems: function () {
                                return [
                                    {
                                        type: "menuitem",
                                        text: "Sub menu item 1",
                                        icon: "unlock",
                                        onAction: function () {
                                            editor.insertContent(
                                                "&nbsp;<em>You clicked Sub menu item 1!</em>"
                                            );
                                        },
                                    },
                                    {
                                        type: "menuitem",
                                        text: "Sub menu item 2",
                                        icon: "lock",
                                        onAction: function () {
                                            editor.insertContent(
                                                "&nbsp;<em>You clicked Sub menu item 2!</em>"
                                            );
                                        },
                                    },
                                ];
                            },
                        },
                    ];
                    callback(items);
                },
            });
        },
        content_css: ["//www.tinymce.com/css/codepen.min.css"],
    });
})(window, document, jQuery);